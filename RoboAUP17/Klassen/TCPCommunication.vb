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
    Private _connected, _client, _server As Boolean

    Friend Event ListenerClientConnected()
    Friend Event ListenerClientDisconnected()
    Friend Event Connected()
    Friend Event Disconnected()
    Friend Event MessageReceived(msg As String)

    Friend Function Listen(port As Integer) As Boolean
        If _connected Or _client Then Return False

        _server = True
        _tcpListener = New TcpListener(Net.IPAddress.Any, port)
        _waitForConnection()

        Return True
    End Function
    Friend Function Connect(ip As Net.IPAddress, port As Integer) As Boolean
        If _connected Or _server Then Return False
        _client = True

        Return True
    End Function
    Friend Sub Terminate()
        _terminate()
    End Sub
    Friend Sub Send(msg As String)
        If _connected Then
            _networkStreamW.WriteLine(msg)
        End If
    End Sub





    ' Trennt alle Verbindungen und gibt Ressourcen wieder frei
    Private Sub _terminate()
        If _tcpListener IsNot Nothing Then
            _tcpListener.Stop()
            _tcpListener = Nothing
        End If
        If _tcpClient IsNot Nothing Then _tcpClient.Close()
        If _networkStreamR IsNot Nothing Then _networkStreamR.Close()
        If _networkStreamW IsNot Nothing Then _networkStreamW.Close()
        If _networkStream IsNot Nothing Then _networkStream.Close()
        _server = False
        _connected = False
    End Sub

    ' Startet den Listener und wartet auf eine Verbindung
    Private Sub _waitForConnection()
        _tcpListener.Start()
        _tcpListener.BeginAcceptTcpClient(AddressOf _connectedCallback, _tcpListener)
    End Sub

    ' Stopt den Listener (da Client verbunden) und erstellt die Datenstreams und den Handlethread
    Private Sub _connectedCallback(result As IAsyncResult)
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
    End Sub

    ' Wartet auf eingehende Daten
    Private Async Sub _handle()
        While True
            ' Check for received Message
            If _connected Then
                Dim msg As String = Await _networkStreamR.ReadLineAsync()
                If msg IsNot Nothing Then
                    If msg.Length > 0 Then
                        RaiseEvent MessageReceived(msg)
                        msg = Nothing
                    End If
                Else
                    _connected = False
                    RaiseEvent ListenerClientDisconnected()
                    _waitForConnection()
                End If
            End If
        End While
    End Sub
End Class
