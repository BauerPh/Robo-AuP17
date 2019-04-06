Imports System.IO.Ports
Imports System.Timers

Friend Class SerialCommunication
    ' -----------------------------------------------------------------------------
    ' TODO
    ' -----------------------------------------------------------------------------
    ' ???

    ' -----------------------------------------------------------------------------
    ' Definitions
    ' -----------------------------------------------------------------------------
    Private Const _cSendTimeoutMS As Int16 = 100S
    Private Const _cSendRepeats As Int16 = 3S
    Private WithEvents _tCheckConnection As New Timer

    Private WithEvents _SerialPort As New SerialPort()
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
    Friend Event ComPortChange(ByVal ports As List(Of String))
    Friend Event SerialConnected()
    Friend Event SerialDisconnected()
    Friend Event Log(ByVal LogMsg As String, ByVal LogLvl As Logger.LogLevel)
    Friend Event FINReceived()
    Friend Event POSReceived(ByVal refOkay As Boolean(), ByVal posSteps As Int32())
    Friend Event LSSReceived(ByVal lssState As Boolean())
    Friend Event ESSReceived(ByVal essState As Boolean)
    Friend Event RESReceived()
    Friend Event ERRReceived(ByVal errnum As Int32)

    ' -----------------------------------------------------------------------------
    ' Constructor
    ' -----------------------------------------------------------------------------
    Friend Sub New()
        'Init Serial Port
        _SerialPort.DataBits = 8
        _SerialPort.StopBits = StopBits.One
        _SerialPort.Parity = Parity.None
        _SerialPort.BaudRate = 115200
        _SerialPort.ReceivedBytesThreshold = 3
        _SerialPort.ReadTimeout = 1000
        'Timers
        _tConWaitACK.Interval = _cSendTimeoutMS
        _tWaitACK.Interval = _cSendTimeoutMS

        _tCheckConnection.Interval = 1000
        _tCheckConnection.Start()
    End Sub

    ' -----------------------------------------------------------------------------
    ' Public
    ' -----------------------------------------------------------------------------
    Friend Sub Connect(comPort As String)
        If _connected Then
            RaiseEvent Log($"[Serial] Bereits über {comPort} verbunden", Logger.LogLevel.WARN)
            Return
        End If
        'Open Port
        _SerialPort.PortName = comPort
        Try
            _SerialPort.Open()
        Catch ex As Exception
            RaiseEvent Log($"[Serial] Fehler beim öffnen von {comPort}: {ex.Message}", Logger.LogLevel.ERR)
            Disconnect()
            Return
        End Try

        'Check device
        _SerialPort.DiscardInBuffer()
        _sendCON()
    End Sub
    Friend Sub Disconnect()
        If Not _SerialPort.IsOpen And Not _connected Then
            RaiseEvent Log($"[Serial] COM-Port ist nicht verbunden", Logger.LogLevel.INFO)
            Return
        End If
        'Close Port
        If _SerialPort.IsOpen Then _SerialPort.Close()
        _connected = False
        RaiseEvent SerialDisconnected()
        RaiseEvent Log($"[Serial] Verbindung über {_SerialPort.PortName} getrennt", Logger.LogLevel.INFO)
    End Sub
    Friend Sub ClearMsgDataSend()
        _clearMsgData(_msgDataSend)
    End Sub
    Friend Sub AddMOVDataSet(add As Boolean, nr As Int32, target As Int32, speed As Int32, accel As Int32, stopAccel As Int32)
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
    Friend Function SendMOV() As Boolean
        If _connected Then
            _sendDataSets("mov", 4)
            _movWaitACK = True
            _tWaitACK.Start()
            Return True
        End If
        Return False
    End Function
    Friend Sub AddREFDataSet(add As Boolean, nr As Int32, dir As Int32, speedFast As Int32, speedSlow As Int32, accel As Int32, maxStepsBack As Int32, stopAccel As Int32)
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
    Friend Function SendREF() As Boolean
        If _connected Then
            _sendDataSets("ref", 6)
            _refWaitACK = True
            _tWaitACK.Start()
            Return True
        End If
        Return False
    End Function
    Friend Function SendSRV(srvNr As Int32, angle As Int32) As Boolean
        If _connected Then
            _sendMsg($"<srv#{srvNr},{angle}>")
            Return True
        End If
        Return False
    End Function
    Friend Function SendWAI(time As Int32) As Boolean
        If _connected Then
            _sendMsg($"<wai#{time}>")
            Return True
        End If
        Return False
    End Function
    Friend Sub SendStop()
        If _connected Then
            _sendMsg("!!!")
        End If
    End Sub

    ' -----------------------------------------------------------------------------
    ' Private
    ' -----------------------------------------------------------------------------
    Private Sub _clearMsgData(ByRef msgData As classMsgData)
        msgData.cnt = 0
        msgData.func = ""
        For i = 0 To 5
            For j = 0 To 7
                msgData.parset(i)(j) = 0
            Next
        Next
    End Sub
    Private Sub _sendCON()
        _sendMsg("<con>")
        _conWaitACK = True
        _tConWaitACK.Start()
    End Sub
    Private Sub _sendDataSets(func As String, parMaxIndex As Int32)
        Dim tmpMsg As String = $"<{func}"
        For i = 0 To _msgDataSend.cnt - 1
            tmpMsg &= "#"
            For j = 0 To parMaxIndex
                tmpMsg &= $"{_msgDataSend.parset(i)(j)}"
                If j < parMaxIndex Then
                    tmpMsg &= ","
                End If
            Next
        Next
        tmpMsg &= ">"
        _sendMsg(tmpMsg)
    End Sub
    Private Sub _tConWaitACK_Elapsed(sender As Object, e As ElapsedEventArgs) Handles _tConWaitACK.Elapsed
        _repeatCounter = _repeatCounter + 1S
        If _repeatCounter >= _cSendRepeats Then
            _repeatCounter = 0S
            _tConWaitACK.Stop()
            Disconnect()
            _conWaitACK = False
            RaiseEvent Log("[Serial] Timeout beim Verbindungsaufbau (keine Antwort vom Arduino erhalten)", Logger.LogLevel.ERR)
        Else
            _sendCON()
        End If
    End Sub
    Private Sub _tMovWaitACK_Elapsed(sender As Object, e As ElapsedEventArgs) Handles _tWaitACK.Elapsed
        _repeatCounter = _repeatCounter + 1S
        If _repeatCounter >= _cSendRepeats Then
            _repeatCounter = 0S
            _tWaitACK.Stop()
            _movWaitACK = False
            _refWaitACK = False
            RaiseEvent Log("[Serial] Timeout beim Senden (keine Antwort vom Arduino erhalten)", Logger.LogLevel.ERR)
        Else
            If _movWaitACK Then
                SendMOV()
            ElseIf _refWaitACK Then
                SendREF()
            End If
        End If
    End Sub
    Private Sub _rcvMsg(msg As String)
        RaiseEvent Log(msg, Logger.LogLevel.COMIN)

        Dim funcList() As String = {"ack", "fin", "pos", "ess", "lss", "res", "err"}
        If Not _parseMsg(msg, _msgDataRcv, funcList) Then
            Return
        End If

        Select Case _msgDataRcv.func
            Case "ack"
                _rcvACK()
            Case "fin"
                RaiseEvent FINReceived()
            Case "pos"
                _rcvPOS()
            Case "ess"
                _rcvESS()
            Case "lss"
                _rcvLSS()
            Case "res"
                _rcvRES()
            Case "err"
                _rcvERR()
            Case Else

        End Select
    End Sub
    Private Sub _rcvACK()
        If _conWaitACK Then
            _conWaitACK = False
            _tConWaitACK.Stop()
            _connected = True
            RaiseEvent SerialConnected()
            RaiseEvent Log($"[Serial] Verbindung über {_SerialPort.PortName} erfolgreich hergestellt", Logger.LogLevel.INFO)
        ElseIf _movWaitACK Or _refWaitACK Then
            _movWaitACK = False
            _refWaitACK = False
            _tWaitACK.Stop()
        End If
    End Sub
    Private Sub _rcvPOS()
        Dim tmpRefOkay(5) As Boolean
        Dim tmpPosSteps(5) As Int32

        For i = 0 To _msgDataRcv.cnt - 1
            Dim nr As Int32 = _msgDataRcv.parset(i)(0) - 1
            tmpRefOkay(nr) = If(_msgDataRcv.parset(i)(1) = 1, True, False)
            tmpPosSteps(nr) = _msgDataRcv.parset(i)(2)
        Next
        RaiseEvent POSReceived(tmpRefOkay, tmpPosSteps)
    End Sub
    Private Sub _rcvLSS()
        Dim tmpState(5) As Boolean
        For i = 0 To 5
            Dim nr As Int32 = _msgDataRcv.parset(i)(0) - 1
            tmpState(nr) = If(_msgDataRcv.parset(i)(1) = 1, True, False)
        Next
        RaiseEvent LSSReceived(tmpState)
    End Sub
    Private Sub _rcvESS()
        Dim tmpState As Boolean = If(_msgDataRcv.parset(0)(0) = 1, True, False)
        RaiseEvent ESSReceived(tmpState)
    End Sub
    Private Sub _rcvRES()
        RaiseEvent RESReceived()
    End Sub
    Private Sub _rcvERR()
        Dim errnum As Int32 = _msgDataRcv.parset(0)(0)
        _movWaitACK = False
        _refWaitACK = False
        _tWaitACK.Stop()
        If errnum = 1 Then
            RaiseEvent Log("[Serial] Unbekannter Befehl", Logger.LogLevel.ERR)
        ElseIf errnum = 2 Then
            RaiseEvent Log("[Serial] Parameter fehlerhaft", Logger.LogLevel.ERR)
        ElseIf errnum = 3 Then
            RaiseEvent Log("[Serial] Roboter nicht in Referenz", Logger.LogLevel.ERR)
        ElseIf errnum = 4 Then
            RaiseEvent Log("[Serial] Referenzfahrt fehlgeschlagen", Logger.LogLevel.ERR)
        End If
        RaiseEvent ERRReceived(errnum)
    End Sub
    Private Function _parseMsg(ByRef msg As String, ByRef msgData As classMsgData, ByRef functionList As String()) As Boolean

        msg.Trim()
        msg.ToLower()

        _clearMsgData(msgData)

        'Länge prüfen
        If msg.Length < 3 Then Return False
        'auszuführende Funktion / Aktion
        msgData.func = msg.Substring(0, 3)
        'Prüfen ob sich das parsen lohnt
        If Array.IndexOf(functionList, msgData.func) < 0 Then Return False
        'Prüfen ob Parameter folgen
        If msg.Length < 5 Then Return True 'keine Parameter
        Dim tmpMsg As String = msg.Substring(4)

        Dim iParSet As Int32 = 0
        Dim iPar As Int32 = 0
        Dim tmpValue As String = ""
        For i = 0 To tmpMsg.Length - 1
            Dim c As Char = tmpMsg(i)
            If c = "#"c Or c = ","c Then
                'Wert speichern
                msgData.parset(iParSet)(iPar) = CInt(tmpValue)
                tmpValue = ""
                'Zähler anpassen
                If c = "#"c Then
                    iParSet += 1
                    iPar = 0
                Else
                    iPar += 1
                End If
            Else
                'Prüfen ob Zahl (erstes Zeichen darf auch ein '-'-Zeichen sein.)
                If (c >= "0"c And c <= "9"c) Or (tmpValue.Length = 0 And c = "-"c) Then
                    tmpValue += c
                Else Return False
                End If
            End If
        Next
        'Letzten Wert speichern
        msgData.parset(iParSet)(iPar) = CInt(tmpValue)

        msgData.cnt = iParSet + 1

        Return True
    End Function
    Private Sub _checkAvailablePorts()
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
                RaiseEvent ComPortChange(listSerialPort)
            End If
        End If
    End Sub
    Private Sub _timerCheckConnection_Tick(sender As Object, e As EventArgs) Handles _tCheckConnection.Elapsed
        'Check Connecion
        If _connected And Not _SerialPort.IsOpen Then
            Disconnect()
        End If
        _checkAvailablePorts()
    End Sub
    Private Sub _sendMsg(msg As String)
        _SerialPort.Write(msg)
        RaiseEvent Log(msg, Logger.LogLevel.COMOUT)
    End Sub
    Private Sub _SerialPort1_DataReceived(sender As Object, e As SerialDataReceivedEventArgs) Handles _SerialPort.DataReceived
        While (_SerialPort.BytesToRead > 0)
            _rcvMsg(_SerialPort.ReadLine)
        End While
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
