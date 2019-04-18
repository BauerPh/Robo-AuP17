Imports System.Net.Sockets
Imports System.IO
Imports System.Threading

Public Class TCPCommunication
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

    Friend Event ListenerClientConnected()
    Friend Event ListenerClientDisconnected()
    Friend Event Connected()
    Friend Event Disconnected()
    Friend Event ConnectError()
    Friend Event MessageReceived(msg As String)

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
    Friend Sub Send(msg As String)
        If _connected Then
            _networkStreamW.WriteLine(msg)
            _networkStreamW.Flush()
        End If
    End Sub

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
        End If
    End Sub

    ' Wartet auf eingehende Daten
    Private Async Sub _handle()
        While True
            ' Check for received Message
            If _connected Then
                Try
                    Dim msg As String = Await _networkStreamR.ReadLineAsync()
                    If msg IsNot Nothing Then
                        If msg.Length > 0 Then
                            RaiseEvent MessageReceived(msg)
                            msg = Nothing
                        End If
                    Else
                        Throw New Exception 'Disconnected
                    End If
                Catch
                    _connected = False
                    If _client Then
                        RaiseEvent Disconnected()
                    Else
                        RaiseEvent ListenerClientDisconnected()
                        _waitForConnection()
                    End If
                End Try
            End If
        End While
    End Sub
End Class