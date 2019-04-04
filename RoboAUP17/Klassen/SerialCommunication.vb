Imports System.IO.Ports
Imports System.Timers

Friend Class SerialCommunication
    Private Const _cSendTimeoutMS As Int16 = 100S
    Private Const _cSendRepeats As Int16 = 3S
    Private WithEvents _tCheckConnection As New Timer

    Private WithEvents _SerialPort1 As New SerialPort()
    Private _repeatCounter As Int16 = 0S
    'CON
    Private _conWaitACK As Boolean = False
    Private WithEvents _tConWaitACK As New Timer
    'Messages
    Private _movWaitACK As Boolean = False
    Private _refWaitACK As Boolean = False
    Private WithEvents _tWaitACK As New Timer

    'Message Data
    Private _msgDataRcv As New classMsgData
    Private _msgDataSend As New classMsgData

    'Vars
    Private listSerialPort As New List(Of String)
    Private _connected As Boolean

    'Events
    Public Event SerialConnected(ByVal sender As Object, ByVal e As EventArgs)
    Public Event SerialDisconnected(ByVal sender As Object, ByVal e As EventArgs)
    Public Event Log(ByVal sender As Object, ByVal e As LogEventArgs)
    Public Event ComPortChange(ByVal sender As Object, ByVal e As ComPortChangeEventArgs)
    Public Event FIN_Received(ByVal sender As Object, ByVal e As EventArgs)
    Public Event POS_Received(ByVal sender As Object, ByVal e As POSReceivedEventArgs)
    Public Event LSS_Received(ByVal sender As Object, ByVal e As LSSReceivedEventArgs)
    Public Event ESS_Received(ByVal sender As Object, ByVal e As ESSReceivedEventArgs)
    Public Event RES_Received(ByVal sender As Object, ByVal e As EventArgs)
    Public Event ERR_Received(ByVal sender As Object, ByVal e As ERRReceivedEventArgs)

    Public Sub New()
        'Init Serial Port
        _SerialPort1.DataBits = 8
        _SerialPort1.StopBits = StopBits.One
        _SerialPort1.Parity = Parity.None
        _SerialPort1.BaudRate = 2000000
        _SerialPort1.ReceivedBytesThreshold = 3
        _SerialPort1.ReadTimeout = 1000
        'Timers
        _tConWaitACK.Interval = _cSendTimeoutMS
        _tWaitACK.Interval = _cSendTimeoutMS

        _tCheckConnection.Interval = 1000
        _tCheckConnection.Start()
    End Sub
    Public Sub connect(comPort As String)
        'Open Port
        _SerialPort1.PortName = comPort
        Try
            _SerialPort1.Open()
        Catch ex As Exception
            OnLog(New LogEventArgs("Verbindungsfehler!", Logger.LogLevel.ERR))
            disconnect()
            Return
        End Try

        'Check device
        _SerialPort1.DiscardInBuffer()
        sendCON()
    End Sub
    Public Sub disconnect()
        'Close Port
        If _SerialPort1.IsOpen Then _SerialPort1.Close()
        _connected = False
        OnSerialDisconnected(New EventArgs())
        OnLog(New LogEventArgs("Verbindung getrennt!", Logger.LogLevel.INFO))
    End Sub
    Public Sub resetDataSets()
        _msgDataSend.cnt = 0
    End Sub
    Public Sub addMOVDataSet(add As Boolean, nr As Int32, target As Int32, speed As Int32, accel As Int32, stopAccel As Int32)
        If speed = 0 Then speed = 1
        If accel = 0 Then accel = 1
        If add Then
            'Achs-Nr
            _msgDataSend.parset(_msgDataSend.cnt)(0) = nr
            'Ziel
            _msgDataSend.parset(_msgDataSend.cnt)(1) = target
            'Geschwindigkeit
            _msgDataSend.parset(_msgDataSend.cnt)(2) = speed
            'Beschleunigung
            _msgDataSend.parset(_msgDataSend.cnt)(3) = accel
            'Beschleunigung für Stop
            _msgDataSend.parset(_msgDataSend.cnt)(4) = stopAccel
            _msgDataSend.cnt += 1S
        End If
    End Sub
    Public Function sendMOV() As Boolean
        If _connected Then
            Dim tmpMsg As String = "<mov"
            For i = 0 To _msgDataSend.cnt - 1
                tmpMsg &= "#" & CStr(_msgDataSend.parset(i)(0)) & "," & CStr(_msgDataSend.parset(i)(1)) & "," &
                    CStr(_msgDataSend.parset(i)(2)) & "," & CStr(_msgDataSend.parset(i)(3)) & "," & CStr(_msgDataSend.parset(i)(4))
            Next
            tmpMsg &= ">"
            sendMsg(tmpMsg)
            _movWaitACK = True
            _tWaitACK.Start()
            Return True
        End If
        Return False
    End Function
    Public Sub addREFDataSet(add As Boolean, nr As Int32, dir As Int32, speedFast As Int32, speedSlow As Int32, accel As Int32, maxStepsBack As Int32, stopAccel As Int32)
        If add Then
            'Achs-Nr
            _msgDataSend.parset(_msgDataSend.cnt)(0) = nr
            'Richtung
            _msgDataSend.parset(_msgDataSend.cnt)(1) = dir
            'Geschwindigkeit schnell
            _msgDataSend.parset(_msgDataSend.cnt)(2) = speedFast
            'Geschwindigkeit langsam
            _msgDataSend.parset(_msgDataSend.cnt)(3) = speedSlow
            'Beschleunigung
            _msgDataSend.parset(_msgDataSend.cnt)(4) = accel
            'Maximale Schritte bei Referenz für zurück
            _msgDataSend.parset(_msgDataSend.cnt)(5) = maxStepsBack
            'Beschleunigung für Stop
            _msgDataSend.parset(_msgDataSend.cnt)(6) = stopAccel
            _msgDataSend.cnt += 1S
        End If
    End Sub
    Public Function sendREF() As Boolean
        If _connected Then
            Dim tmpMsg As String = "<ref"
            For i = 0 To _msgDataSend.cnt - 1
                tmpMsg &= "#" & CStr(_msgDataSend.parset(i)(0)) & "," & CStr(_msgDataSend.parset(i)(1)) & "," &
                    CStr(_msgDataSend.parset(i)(2)) & "," & CStr(_msgDataSend.parset(i)(3)) & "," & CStr(_msgDataSend.parset(i)(4)) & "," & CStr(_msgDataSend.parset(i)(5)) & "," & CStr(_msgDataSend.parset(i)(6))
            Next
            tmpMsg &= ">"
            sendMsg(tmpMsg)
            _refWaitACK = True
            _tWaitACK.Start()
            Return True
        End If
        Return False
    End Function
    Public Function sendSRV(srvNr As Int32, angle As Int32) As Boolean
        If _connected Then
            sendMsg("<srv#" & CStr(srvNr) & "," & CStr(angle) & ">")
            Return True
        End If
        Return False
    End Function
    Public Function sendWAI(time As Int32) As Boolean
        If _connected Then
            sendMsg("<wai#" & CStr(time) & ">")
            Return True
        End If
        Return False
    End Function
    Public Sub sendStop()
        If _connected Then
            sendMsg("!!!")
        End If
    End Sub

    '++++++++++++++++++++++++++++++ PRIVATE ++++++++++++++++++++++++++++++
    Private Sub sendCON()
        sendMsg("<con>")
        _conWaitACK = True
        _tConWaitACK.Start()
    End Sub
    Private Sub _tConWaitACK_Elapsed(sender As Object, e As ElapsedEventArgs) Handles _tConWaitACK.Elapsed
        _repeatCounter = _repeatCounter + 1S
        If _repeatCounter >= _cSendRepeats Then
            _repeatCounter = 0S
            _tConWaitACK.Stop()
            disconnect()
            _conWaitACK = False
            OnLog(New LogEventArgs("Falsches Gerät angeschlossen!", Logger.LogLevel.ERR))
        Else
            sendCON()
        End If
    End Sub
    Private Sub _tMovWaitACK_Elapsed(sender As Object, e As ElapsedEventArgs) Handles _tWaitACK.Elapsed
        _repeatCounter = _repeatCounter + 1S
        If _repeatCounter >= _cSendRepeats Then
            _repeatCounter = 0S
            _tWaitACK.Stop()
            _movWaitACK = False
            _refWaitACK = False
            OnLog(New LogEventArgs("keine Antwort!", Logger.LogLevel.ERR))
        Else
            If _movWaitACK Then
                sendMOV()
            ElseIf _refWaitACK Then
                sendREF()
            End If
        End If
    End Sub
    Private Sub rcvMsg(msg As String)
        OnLog(New LogEventArgs(msg, Logger.LogLevel.COMIN))

        If msg.Length < 3 Then
            Return
        End If

        Dim funcList() As String = {"ack", "fin", "pos", "ess", "lss", "res", "err"}
        parseMsg(msg, _msgDataRcv, funcList)

        Select Case _msgDataRcv.func
            Case "ack"
                rcvACK()
            Case "fin"
                OnFIN_Received(New EventArgs())
            Case "pos"
                rcvPOS()
            Case "ess"
                rcvESS()
            Case "lss"
                rcvLSS()
            Case "res"
                rcvRES()
            Case "err"
                rcvERR()
            Case Else

        End Select
    End Sub

    Private Sub rcvACK()
        If _conWaitACK Then
            _conWaitACK = False
            _tConWaitACK.Stop()
            _connected = True
            OnSerialConnected(New EventArgs())
            OnLog(New LogEventArgs("Verbunden!", Logger.LogLevel.INFO))
        ElseIf _movWaitACK Or _refWaitACK Then
            _movWaitACK = False
            _refWaitACK = False
            _tWaitACK.Stop()
        End If
    End Sub
    Private Sub rcvPOS()
        Dim tmpRefOkay(5) As Boolean
        Dim tmpPosSteps(5) As Int32

        For i = 0 To _msgDataRcv.cnt - 1
            Dim nr As Int32 = _msgDataRcv.parset(i)(0)
            tmpRefOkay(nr - 1) = If(_msgDataRcv.parset(i)(1) = 1, True, False)
            tmpPosSteps(nr - 1) = _msgDataRcv.parset(i)(2)
        Next
        OnPOS_Received(New POSReceivedEventArgs(tmpRefOkay, tmpPosSteps))
    End Sub
    Private Sub rcvLSS()
        Dim tmpState(5) As Boolean
        For i = 0 To 5
            Dim nr As Int32 = _msgDataRcv.parset(i)(0)
            tmpState(nr - 1) = If(_msgDataRcv.parset(i)(1) = 1, True, False)
        Next
        OnLSS_Received(New LSSReceivedEventArgs(tmpState))
    End Sub
    Private Sub rcvESS()
        Dim tmpState As Boolean = If(_msgDataRcv.parset(0)(0) = 1, True, False)
        OnESS_Received(New ESSReceivedEventArgs(tmpState))
    End Sub
    Private Sub rcvRES()
        OnRES_Received(New EventArgs())
    End Sub
    Private Sub rcvERR()
        Dim errnum As Int32 = _msgDataRcv.parset(0)(0)
        _movWaitACK = False
        _refWaitACK = False
        _tWaitACK.Stop()
        If errnum = 1 Then
            OnLog(New LogEventArgs("Error!", Logger.LogLevel.ERR))
        ElseIf errnum = 2 Then
            OnLog(New LogEventArgs("Parameter Error!", Logger.LogLevel.ERR))
        ElseIf errnum = 3 Then
            OnLog(New LogEventArgs("Referenz fehlt!", Logger.LogLevel.ERR))
        ElseIf errnum = 4 Then
            OnLog(New LogEventArgs("Referenz fehlgeschlagen!", Logger.LogLevel.ERR))
        End If
        OnERR_Received(New ERRReceivedEventArgs(errnum))
    End Sub
    Private Sub parseMsg(ByRef _msg As String, ByRef _msgData As classMsgData, ByRef functionList As String())
        _msgData.cnt = 0
        _msg.Trim()
        _msg.ToLower()
        If _msg.Length < 3 Then Return
        'auszuführende Funktion / Aktion
        _msgData.func = _msg.Substring(0, 3)

        'Prüfen ob sich das parsen lohnt
        If Array.IndexOf(functionList, _msgData.func) < 0 Then
            Return
        End If

        If _msg.Length < 4 Then Return
        Dim tmpCommand As String = _msg.Substring(4)
        'Parametersatz splitten
        Dim iParSet As Int32 = 0
        Dim lastParSetIndex As Int32 = 0
        While tmpCommand.IndexOf("#"c, lastParSetIndex) > 0 'es gibt noch ein '#' Zeichen
            Dim ParSetIndex As Int32 = tmpCommand.IndexOf("#"c, lastParSetIndex)
            Dim tmpParSet As String = tmpCommand.Substring(lastParSetIndex, ParSetIndex - lastParSetIndex)
            'Parameter splitten
            Dim iPar As Int32 = 0
            Dim lastParIndex = 0
            While tmpParSet.IndexOf(","c, lastParIndex) > 0 'es gibt noch ein ',' Zeichen
                Dim ParIndex As Int32 = tmpParSet.IndexOf(","c, lastParIndex)
                _msgData.parset(iParSet)(iPar) = CInt(tmpParSet.Substring(lastParIndex, ParIndex - lastParIndex))
                iPar += 1
                lastParIndex = ParIndex + 1
            End While
            'letzten Parameter berücksichtigen
            _msgData.parset(iParSet)(iPar) = CInt(tmpParSet.Substring(lastParIndex))

            'restliche Parameter mit 0 befüllen
            For i = iPar + 1 To 7
                _msgData.parset(iParSet)(i) = 0
            Next
            iParSet += 1
            lastParSetIndex = ParSetIndex + 1
            tmpParSet = ""
        End While
        'letzten Parametersatz berücksichtigen, wenn es einen gibt!
        If tmpCommand.Length() > 0 Then
            Dim tmpParSet As String = tmpCommand.Substring(lastParSetIndex)
            'Parameter splitten
            Dim iPar As Int32 = 0
            Dim lastParIndex = 0
            While tmpParSet.IndexOf(","c, lastParIndex) > 0 'es gibt noch ein ',' Zeichen
                Dim ParIndex As Int32 = tmpParSet.IndexOf(","c, lastParIndex)
                _msgData.parset(iParSet)(iPar) = CInt(tmpParSet.Substring(lastParIndex, ParIndex - lastParIndex))
                iPar += 1
                lastParIndex = ParIndex + 1
            End While
            'letzten Parameter berücksichtigen
            _msgData.parset(iParSet)(iPar) = CInt(tmpParSet.Substring(lastParIndex))
            'restliche Parameter mit 0 befüllen
            For i = iPar + 1 To 7
                _msgData.parset(iParSet)(i) = 0
            Next

            iParSet += 1
        End If
        _msgData.cnt = iParSet
        tmpCommand = ""
    End Sub
    Private Sub checkAvailablePorts()
        'Check Port changes
        If Not _connected Then
            If IO.Ports.SerialPort.GetPortNames.Length <> listSerialPort.Count Then
                listSerialPort.Clear()
                Dim serialPort() As String = IO.Ports.SerialPort.GetPortNames
                If serialPort.Length > 0 Then
                    For i = 0 To UBound(serialPort)
                        Dim found As Boolean = False
                        'Check duplicates
                        For j = 0 To listSerialPort.Count - 1
                            If listSerialPort(j) = serialPort(i) Then
                                found = True
                                Exit For
                            End If
                        Next
                        If Not found Then
                            listSerialPort.Add(serialPort(i))
                        End If
                    Next
                End If
                OnComPortChange(New ComPortChangeEventArgs(listSerialPort))
            End If
        End If
    End Sub

    Private Sub timerCheckConnection_Tick(sender As Object, e As EventArgs) Handles _tCheckConnection.Elapsed
        'Check Connecion
        If _connected And Not _SerialPort1.IsOpen Then
            disconnect()
        End If
        checkAvailablePorts()
    End Sub
    Private Sub sendMsg(msg As String)
        _SerialPort1.Write(msg)
        OnLog(New LogEventArgs(msg, Logger.LogLevel.COMOUT))
    End Sub
    Private Sub _SerialPort1_DataReceived(sender As Object, e As SerialDataReceivedEventArgs) Handles _SerialPort1.DataReceived
        While (_SerialPort1.BytesToRead > 0)
            rcvMsg(_SerialPort1.ReadLine)
        End While
    End Sub
    Protected Sub OnSerialConnected(e As EventArgs)
        RaiseEvent SerialConnected(Me, e)
    End Sub
    Protected Sub OnSerialDisconnected(e As EventArgs)
        RaiseEvent SerialDisconnected(Me, e)
    End Sub
    Protected Sub OnLog(e As LogEventArgs)
        RaiseEvent Log(Me, e)
    End Sub
    Protected Sub OnComPortChange(e As ComPortChangeEventArgs)
        RaiseEvent ComPortChange(Me, e)
    End Sub
    Protected Sub OnFIN_Received(e As EventArgs)
        RaiseEvent FIN_Received(Me, e)
    End Sub
    Protected Sub OnPOS_Received(e As POSReceivedEventArgs)
        RaiseEvent POS_Received(Me, e)
    End Sub
    Protected Sub OnLSS_Received(e As LSSReceivedEventArgs)
        RaiseEvent LSS_Received(Me, e)
    End Sub
    Protected Sub OnESS_Received(e As ESSReceivedEventArgs)
        RaiseEvent ESS_Received(Me, e)
    End Sub
    Protected Sub OnRES_Received(e As EventArgs)
        RaiseEvent RES_Received(Me, e)
    End Sub
    Protected Sub OnERR_Received(e As ERRReceivedEventArgs)
        RaiseEvent ERR_Received(Me, e)
    End Sub
End Class

Public Class classMsgData
    Public Property func As String
    Public Property cnt As Int32
    Public Property parset As Int32()()

    Public Sub New()
        parset = New Int32(5)() {}
        For i = 0 To 5
            parset(i) = New Int32(7) {}
        Next
    End Sub
End Class

'Event Parameter
Public Class ComPortChangeEventArgs : Inherits EventArgs
    Private _Ports As List(Of String)
    Public Sub New(Ports As List(Of String))
        _Ports = Ports
    End Sub
    Public ReadOnly Property Ports As List(Of String)
        Get
            Return _Ports
        End Get
    End Property
End Class

Public Class POSReceivedEventArgs : Inherits EventArgs
    Private _refOkay(5) As Boolean
    Private _posSteps(5) As Int32
    Public Sub New(refOkay As Boolean(), posSteps As Int32())
        _refOkay = refOkay
        _posSteps = posSteps
    End Sub
    Public ReadOnly Property refOkay As Boolean()
        Get
            Return _refOkay
        End Get
    End Property
    Public ReadOnly Property posSteps As Int32()
        Get
            Return _posSteps
        End Get
    End Property
End Class

Public Class LSSReceivedEventArgs : Inherits EventArgs
    Private _lssState(5) As Boolean
    Public Sub New(lssState As Boolean())
        _lssState = lssState
    End Sub
    Public ReadOnly Property lssState As Boolean()
        Get
            Return _lssState
        End Get
    End Property
End Class

Public Class ESSReceivedEventArgs : Inherits EventArgs
    Private _essState As Boolean
    Public Sub New(essState As Boolean)
        _essState = essState
    End Sub
    Public ReadOnly Property essState As Boolean
        Get
            Return _essState
        End Get
    End Property
End Class

Public Class ERRReceivedEventArgs : Inherits EventArgs
    Private _errnum As Int32
    Public Sub New(errnum As Int32)
        _errnum = errnum
    End Sub
    Public ReadOnly Property errnum As Int32
        Get
            Return _errnum
        End Get
    End Property
End Class
