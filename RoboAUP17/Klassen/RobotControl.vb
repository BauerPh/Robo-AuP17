Imports RoboAUP17

Friend Class RobotControl
    ' -----------------------------------------------------------------------------
    ' TODO
    ' -----------------------------------------------------------------------------
    ' ???

    ' -----------------------------------------------------------------------------
    ' Definitions
    ' -----------------------------------------------------------------------------
    Private Const _constRefMaxAngleBack As Int32 = 10 'Bei Referenzfahrt

    ' Movement
    Private _oSyncMov As New MovementCalculations
    Private _actV As Double
    Private _actA As Double

    ' Parameter
    Private _par As New RoboParameter
    ' Serial Communication
    Private WithEvents _com As New SerialCommunication

    Private _refOkay(5) As Boolean
    Private _posSteps(5) As Int32
    Private _posJoint As JointAngles
    Private _posCart As CartCoords
    Private _targetJoint As JointAngles

    Public ReadOnly Property RefOkay As Boolean()
        Get
            Return _refOkay
        End Get
    End Property

    Public ReadOnly Property PosSteps As Integer()
        Get
            Return _posSteps
        End Get
    End Property

    Friend ReadOnly Property PosJoint As JointAngles
        Get
            Return _posJoint
        End Get
    End Property

    Friend ReadOnly Property PosCart As CartCoords
        Get
            Return _posCart
        End Get
    End Property

    Friend ReadOnly Property TargetJoint As JointAngles
        Get
            Return _targetJoint
        End Get
    End Property

    Friend Property Par As RoboParameter
        Get
            Return _par
        End Get
        Set(value As RoboParameter)
            _par = value
        End Set
    End Property

    ' Events
    Friend Event SerialComPortChanged(ByVal ports As List(Of String))
    Friend Event SerialConnected()
    Friend Event SerialDisconnected()
    Friend Event Log(ByVal LogMsg As String, ByVal LogLvl As Logger.LogLevel)
    Friend Event RoboMoveFinished()
    Friend Event RoboPositionChanged()
    Friend Event LimitSwitchStateChanged(ByVal lssState As Boolean())
    Friend Event EmergencyStopStateChanged(ByVal essState As Boolean)
    Friend Event RoboRefStateChanged(ByVal refState As Boolean())

    ' -----------------------------------------------------------------------------
    ' Public
    ' -----------------------------------------------------------------------------
    'SERIAL
    Friend Sub SerialConnect(comPort As String)
        _com.Connect(comPort)
    End Sub
    Friend Sub SerialDisconnect()
        _com.Disconnect()
    End Sub
    'ROBOT MOVEMENTS
    Friend Sub SetSpeedAndAcc(speed As Double, acc As Double)
        _actV = speed
        _actA = acc
    End Sub
    ' Jog Angle
    Friend Function DoJog(nr As Int32, jogval As Double) As Boolean
        Dim tmpV(5) As Double
        Dim tmpA(5) As Double
        'aktuelle Geschwindigkeit und Beschleunigung berechnen
        _calcSpeedAcc(tmpV, tmpA)
        'Datensatz zusammenstellen
        _com.ClearMsgDataSend()
        Dim tmpNr As Int32 = nr - 1
        _targetJoint.setByIndex(tmpNr, Constrain(_posJoint.items(tmpNr) + jogval, _par.JointParameter(tmpNr).mechMinAngle, _par.JointParameter(tmpNr).mechMaxAngle))
        _com.AddMOVDataSet(True, nr, _calcTargetToSteps(_targetJoint.items(tmpNr), nr), _calcSpeedAccToSteps(tmpV(tmpNr), nr), _calcSpeedAccToSteps(tmpA(tmpNr), nr), _calcSpeedAccToSteps(_par.JointParameter(tmpNr).profileStopAcc, nr))
        'Telegramm senden
        If _com.SendMOV() Then
            'Log
            RaiseEvent Log($"Moving Axis...", Logger.LogLevel.INFO)
            Return True
        Else Return False
        End If
    End Function
    ' Jog Steps
    Friend Function DoJog(nr As Int32, jogval As Int32) As Boolean
        'Jog steps
        Dim tmpJogval As Double = StepsToAngle(jogval, _par.JointParameter(nr - 1).motGear, _par.JointParameter(nr - 1).mechGear, _par.JointParameter(nr - 1).motStepsPerRot << _par.JointParameter(nr - 1).motMode, 0)
        'Datensatz zusammenstellen
        Return DoJog(nr, tmpJogval)
    End Function

    Friend Function DoRef(J1 As Boolean, J2 As Boolean, J3 As Boolean, J4 As Boolean, J5 As Boolean, J6 As Boolean) As Boolean
        'Datensätze sammeln
        _com.ClearMsgDataSend()
        Dim enabled() As Boolean = {J1, J2, J3, J4, J5, J6}
        For i = 0 To 5
            Dim tmpMaxStepsBack As Int32 = AngleToSteps(_constRefMaxAngleBack, _par.JointParameter(i).motGear, _par.JointParameter(i).mechGear, _par.JointParameter(i).motStepsPerRot << _par.JointParameter(i).motMode, 0)
            _com.AddREFDataSet(enabled(i), i + 1, _par.JointParameter(i).calDir Xor _par.JointParameter(i).motDir, _calcSpeedAccToSteps(_par.JointParameter(i).calSpeedFast, i + 1), _calcSpeedAccToSteps(_par.JointParameter(i).calSpeedSlow, i + 1), _calcSpeedAccToSteps(_par.JointParameter(i).calAcc, i + 1), tmpMaxStepsBack, _calcSpeedAccToSteps(_par.JointParameter(i).profileStopAcc, i + 1))
        Next
        'Telegram senden
        If _com.SendREF() Then
            'Log
            RaiseEvent Log($"Referenz läuft...", Logger.LogLevel.INFO)
            Return True
        Else Return False
        End If
    End Function

    Friend Function DoJointMov(sync As Boolean, J1_en As Boolean, J1_target As Double, J2_en As Boolean, J2_target As Double, J3_en As Boolean, J3_target As Double, J4_en As Boolean, J4_target As Double, J5_en As Boolean, J5_target As Double, J6_en As Boolean, J6_target As Double) As Boolean
        'Daten aufbereiten
        Dim tmpV(5) As Double
        Dim tmpA(5) As Double
        Dim enabled() As Boolean = {J1_en, J2_en, J3_en, J4_en, J5_en, J6_en}
        _targetJoint = New JointAngles(J1_target, J2_target, J3_target, J4_target, J5_target, J6_target)
        'Prüfen ob Ziel schon erreicht (keine Fahrt mehr notwendig)
        For i = 0 To 5
            If Math.Abs(_targetJoint.items(i) - _posJoint.items(i)) < 0.01 Then
                enabled(i) = False
                _targetJoint.setByIndex(i, _posJoint.items(i))
            End If
        Next
        'aktuelle Geschwindigkeit und Beschleunigung berechnen
        _calcSpeedAcc(tmpV, tmpA)
        'Synchronisierte Bewegung berechnen, falls gewünscht
        If sync Then
            For i = 0 To 5
                _oSyncMov.v_max(i) = tmpV(i)
                _oSyncMov.a_max(i) = tmpA(i)
                _oSyncMov.s(i) = If(enabled(i), Math.Abs(_targetJoint.items(i) - _posJoint.items(i)), 0)
            Next
            If Not _oSyncMov.calculate() Then
                RaiseEvent Log("[Robo Control] Berechnung für synchrone Bewegung fehlgeschlagen", Logger.LogLevel.ERR)
                _targetJoint = CType(_posJoint.Clone(), JointAngles) ' Ziel auf aktuelle Position setzen
            Else
                'Geschwindigkeit und Beschleunigung zuweisen
                Array.Copy(_oSyncMov.v, tmpV, _oSyncMov.v.Length)
                Array.Copy(_oSyncMov.a, tmpA, _oSyncMov.a.Length)
            End If
        End If
        'Datensätze sammeln
        _com.ClearMsgDataSend()
        For i = 0 To 5
            _com.AddMOVDataSet(enabled(i), i + 1, _calcTargetToSteps(_targetJoint.items(i), i + 1), _calcSpeedAccToSteps(tmpV(i), i + 1), _calcSpeedAccToSteps(tmpA(i), i + 1), _calcSpeedAccToSteps(_par.JointParameter(i).profileStopAcc, i + 1))
        Next
        'Telegram senden
        If _com.SendMOV() Then
            'Log
            RaiseEvent Log("[Robo Control] Bewege Achsen...", Logger.LogLevel.INFO)
            Return True
        Else Return False
        End If
    End Function
    Friend Function DoHome(sync As Boolean, J1_en As Boolean, J2_en As Boolean, J3_en As Boolean, J4_en As Boolean, J5_en As Boolean, J6_en As Boolean) As Boolean
        Return DoJointMov(sync, J1_en, _par.JointParameter(0).mechHomePosAngle, J2_en, _par.JointParameter(1).mechHomePosAngle, J3_en, _par.JointParameter(2).mechHomePosAngle, J4_en, _par.JointParameter(3).mechHomePosAngle, J5_en, _par.JointParameter(4).mechHomePosAngle, J6_en, _par.JointParameter(5).mechHomePosAngle)
    End Function
    Friend Function DoPark(sync As Boolean, J1_en As Boolean, J2_en As Boolean, J3_en As Boolean, J4_en As Boolean, J5_en As Boolean, J6_en As Boolean) As Boolean
        Return DoJointMov(sync, J1_en, _par.JointParameter(0).mechParkPosAngle, J2_en, _par.JointParameter(1).mechParkPosAngle, J3_en, _par.JointParameter(2).mechParkPosAngle, J4_en, _par.JointParameter(3).mechParkPosAngle, J5_en, _par.JointParameter(4).mechParkPosAngle, J6_en, _par.JointParameter(5).mechParkPosAngle)
    End Function
    Friend Function MoveServo(srvNr As Int32, angle As Int32) As Boolean
        If _com.SendSRV(srvNr, angle) Then
            'Log
            RaiseEvent Log($"[Robo Control] Bewege Servo {srvNr}, Ziel: {angle}...", Logger.LogLevel.INFO)
            Return True
        Else Return False
        End If
    End Function


    ' -----------------------------------------------------------------------------
    ' Private
    ' -----------------------------------------------------------------------------
    Private Sub _checkRefStateChange()
        Static refOkayOld(5) As Boolean
        For i = 0 To 5
            If _refOkay(i) <> refOkayOld(i) Then
                RaiseEvent RoboRefStateChanged(_refOkay)
                Exit For
            End If
        Next
        _refOkay.CopyTo(refOkayOld, 0)
    End Sub
    'CALCULATIONS
    Private Function _calcServoAngle(srvNr As Int32, prc As Double) As Int32
        Return CInt((_par.ServoParameter(srvNr - 1).maxAngle - _par.ServoParameter(srvNr - 1).minAngle) * (prc / 100.0) + _par.ServoParameter(srvNr - 1).minAngle)
    End Function
    Private Sub _calcSpeedAcc(ByRef v() As Double, ByRef a() As Double)
        'Maximale Geschwindigkeit und Beschleunigung jeder Achse ermitteln (Maxwert oder aktueller Wert, je nachdem welcher kleiner ist!)
        For i = 0 To 5
            If _par.JointParameter(i).profileMaxSpeed < _actV Then
                v(i) = _par.JointParameter(i).profileMaxSpeed
            Else
                v(i) = _actV
            End If

            If _par.JointParameter(i).profileMaxAcc < _actA Then
                a(i) = _par.JointParameter(i).profileMaxAcc
            Else
                a(i) = _actA
            End If
        Next
    End Sub

    Private Function _calcTargetToSteps(target As Double, nr As Int32) As Int32
        Dim tmpNr As Int32 = nr - 1
        Return If(_par.JointParameter(tmpNr).motDir = 1, -1, 1) * AngleToSteps(target, _par.JointParameter(tmpNr).motGear, _par.JointParameter(tmpNr).mechGear, _par.JointParameter(tmpNr).motStepsPerRot << _par.JointParameter(tmpNr).motMode, If(_par.JointParameter(tmpNr).calDir = 0, _par.JointParameter(tmpNr).mechMinAngle * -1, _par.JointParameter(tmpNr).mechMaxAngle * -1))
    End Function

    Private Function _calcSpeedAccToSteps(speedAcc As Double, nr As Int32) As Int32
        Dim tmpNr As Int32 = nr - 1
        Return AngleToSteps(speedAcc, _par.JointParameter(tmpNr).motGear, _par.JointParameter(tmpNr).mechGear, _par.JointParameter(tmpNr).motStepsPerRot, 0)
    End Function
    ' -----------------------------------------------------------------------------
    ' Events
    ' -----------------------------------------------------------------------------
    'Public Event ERRReceived(ByVal errnum As Int32)
    'Serial
    Private Sub _eSerialComPortChange(ports As List(Of String)) Handles _com.ComPortChange
        RaiseEvent SerialComPortChanged(ports)
    End Sub
    Private Sub _eSerialConnected() Handles _com.SerialConnected
        RaiseEvent SerialConnected()
    End Sub
    Private Sub _eSerialDisconnected() Handles _com.SerialDisconnected
        RaiseEvent SerialDisconnected()
    End Sub
    Private Sub _eLog(LogMsg As String, LogLvl As Logger.LogLevel) Handles _com.Log
        'Log Event durchleiten
        RaiseEvent Log(LogMsg, LogLvl)
    End Sub
    Private Sub _eFINReceived() Handles _com.FINReceived
        RaiseEvent RoboMoveFinished()
    End Sub
    Private Sub _ePOSReceived(refOkay As Boolean(), posSteps As Int32()) Handles _com.POSReceived
        For i = 0 To 5
            _posJoint.setByIndex(i, StepsToAngle(posSteps(i), _par.JointParameter(i).motGear, _par.JointParameter(i).mechGear, _par.JointParameter(i).motStepsPerRot << _par.JointParameter(i).motMode, If(_par.JointParameter(i).calDir = 0, _par.JointParameter(i).mechMinAngle * -1, _par.JointParameter(i).mechMaxAngle * -1)))
        Next
        refOkay.CopyTo(_refOkay, 0)
        _checkRefStateChange()
        RaiseEvent RoboPositionChanged()
    End Sub
    Private Sub _eLSSReceived(lssState As Boolean()) Handles _com.LSSReceived
        RaiseEvent LimitSwitchStateChanged(lssState)
    End Sub
    Private Sub _eESSReceived(essState As Boolean) Handles _com.ESSReceived
        RaiseEvent EmergencyStopStateChanged(essState)
    End Sub
    Private Sub _eRESReceived() Handles _com.RESReceived
        For i = 0 To 5
            _refOkay(i) = False
        Next
        _checkRefStateChange()
    End Sub
End Class

Public Class classPos
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