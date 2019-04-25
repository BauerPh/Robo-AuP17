Imports System.Net.Sockets
Imports System.IO
Imports System.Threading

Public Class TCPCommunication
    ' -----------------------------------------------------------------------------
    ' Definitions
    ' -----------------------------------------------------------------------------
    Private _threadHandle As New Thread(AddressOf _handle)
    Private _tcpListener As TcpListener
    Private _tcpClient As TcpClient
    Private _networkStream As NetworkStream
    Private _networkStreamR As StreamReader
    Private _networkStreamW As StreamWriter
    Private _connected, _connecting, _listen, _client, _server As Boolean
    Private _ip As Net.IPAddress
    Private _port As Integer
    Private _host As String
    Private _msgEnding As String = vbCrLf

    Friend Property MessageEndString As String
        Get
            Return _msgEnding
        End Get
        Set(value As String)
            _msgEnding = value
        End Set
    End Property

    Friend Event ListenerClientConnected()
    Friend Event ListenerClientDisconnected()
    Friend Event Connected()
    Friend Event Disconnected()
    Friend Event ConnectError()
    Friend Event MessageReceived(msg As String)

    ' -----------------------------------------------------------------------------
    ' Public
    ' -----------------------------------------------------------------------------
    Friend Sub New()

    End Sub
    Friend Sub New(endString As String)
        _msgEnding = endString
    End Sub
    Friend Function Listen(port As Integer) As Boolean
        If _connected Or _client Then Return False
        _server = True
        _tcpListener = New TcpListener(Net.IPAddress.Any, port)
        _listen = True
        _waitForConnection()
        Return True
    End Function
    Friend Function Connect(ip As Net.IPAddress, port As Integer) As Boolean
        If _connected Or _server Then Return False
        _ip = ip
        _port = port
        _connect()
        Return True
    End Function
    Friend Function Connect(host As String, port As Integer) As Boolean
        If _connected Or _server Then Return False
        _host = host
        _port = port
        _connect()
        Return True
    End Function
    Friend Sub Terminate()
        _terminate()
    End Sub
    Friend Function Send(msg As String) As Boolean
        If _connected Then
            Try
                _networkStreamW.Write(msg)
                _networkStreamW.Flush()
            Catch ex As IO.IOException
                Return False
            End Try
            Return True
        End If
        Return False
    End Function

    ' -----------------------------------------------------------------------------
    ' Private
    ' -----------------------------------------------------------------------------
    ' Stellt eine Verbindung zu einem TCP Server her
    Private Async Sub _connect()
        If _connecting Then Return
        _connecting = True
        _client = True
        _tcpClient = New TcpClient
        Try
            If _ip IsNot Nothing Then
                Await _tcpClient.ConnectAsync(_ip, _port)
            Else
                Await _tcpClient.ConnectAsync(_host, _port)
            End If
        Catch e As Exception
            _connecting = False
            RaiseEvent ConnectError()
            Return
        End Try
        ' Connected
        _networkStream = _tcpClient.GetStream()
        _networkStreamR = New StreamReader(_networkStream)
        _networkStreamW = New StreamWriter(_networkStream)
        'Start Thread (wenn noch nicht gestartet)
        If _threadHandle.ThreadState = ThreadState.Unstarted Then
            _threadHandle.IsBackground = True
            _threadHandle.Start()
        End If
        _connecting = False
        _connected = True
        RaiseEvent Connected()
    End Sub
    ' Trennt alle Verbindungen und gibt Ressourcen wieder frei
    Private Sub _terminate()
        _server = False
        _connected = False
        _listen = False
        _connecting = False
        If _tcpListener IsNot Nothing Then
            _tcpListener.Stop()
            _tcpListener = Nothing
        End If
        If _tcpClient IsNot Nothing Then _tcpClient.Close()
        If _networkStreamR IsNot Nothing Then _networkStreamR.Close()
        If _networkStreamW IsNot Nothing Then _networkStreamW.Close()
        If _networkStream IsNot Nothing Then _networkStream.Close()
    End Sub

    ' Startet den Listener und wartet auf eine Verbindung
    Private Sub _waitForConnection()
        If _listen Then
            _tcpListener.Start()
            _tcpListener.BeginAcceptTcpClient(AddressOf _connectedCallback, _tcpListener)
        End If
    End Sub

    ' Stopt den Listener (da Client verbunden) und erstellt die Datenstreams und den Handlethread
    Private Sub _connectedCallback(result As IAsyncResult)
        If _listen Then
            Try
                _tcpClient = _tcpListener.EndAcceptTcpClient(result)
                _tcpListener.Stop()
                _networkStream = _tcpClient.GetStream()
                _networkStreamR = New StreamReader(_networkStream)
                _networkStreamW = New StreamWriter(_networkStream)
                'Start Thread (wenn noch nicht gestartet)
                If _threadHandle.ThreadState = ThreadState.Unstarted Then
                    _threadHandle.IsBackground = True
                    _threadHandle.Start()
                End If
                _connected = True
                RaiseEvent ListenerClientConnected()
            Catch e As ObjectDisposedException
                _waitForConnection()
            End Try
        End If
    End Sub

    ' Prüft die Verbindung und wartet auf eingehende Daten
    Private Sub _handle()
        Dim msg As String = ""
        While True
            If _connected Then
                ' Verbindung prüfen
                If Not _isConnected(_tcpClient) Then
                    _connected = False
                    If _client Then
                        RaiseEvent Disconnected()
                    Else
                        RaiseEvent ListenerClientDisconnected()
                        _waitForConnection()
                    End If
                End If
                Try
                    ' eingehende Nachricht
                    While Not _networkStreamR.EndOfStream
                        Dim c As Char = ChrW(_networkStreamR.Read)
                        msg &= c
                        If msg.EndsWith(_msgEnding) Then
                            RaiseEvent MessageReceived(msg.Substring(0, msg.Length - _msgEnding.Length))
                            msg = ""
                        End If
                    End While
                Catch e As IO.IOException ' Wird geworfen, wenn auf _networkStreamR zugegriffen wird und gleichzeitig die Verbindung vom Partner getrennt wird
                    msg = ""
                End Try
            End If
        End While
    End Sub

    ' Prüft on Client noch verbunden ist
    Private Function _isConnected(client As TcpClient) As Boolean
        Try
            ' Poll(1, SelectMode.SelectRead) ist true, wenn:
            ' Verbindung noch nicht aufgebaut oder
            ' Daten verfügbar oder
            ' Verbindung unterbrochen
            Return Not (client.Client.Poll(1, SelectMode.SelectRead) And (client.Available = 0))
        Catch e As SocketException
            Return False
        End Try
    End Function
End Class