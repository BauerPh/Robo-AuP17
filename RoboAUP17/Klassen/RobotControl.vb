Imports System.IO

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

    ' Programm
    Public progList As New List(Of ProgramEntry)
    Public teachPoints As New List(Of TeachPoint)
    Public progClipboard As New ProgramEntry
    Public teachPointClipboard As New TeachPoint
    Private _programRunning, _loopSeq As Boolean
    Private _progIndex As Int32

    ' Settings
    Public pref As New Settings
    ' Serial Communication
    Public WithEvents com As New SerialCommunication

    Public pos(5) As Double
    Public target(5) As Double

    Public Event Log(ByVal sender As Object, ByVal e As LogEventArgs)
    Public Event NewPos(ByVal sender As Object, ByVal e As NewPosEventArgs)
    Public Event SeqListChanged(ByVal sender As Object, ByVal e As ProgChangedEventArgs)

    ' -----------------------------------------------------------------------------
    ' Public
    ' -----------------------------------------------------------------------------
    'TEACHPUNKTE
    Public Sub addTeachPoint(tp As TeachPoint, nr As Int32)
        Dim i As Int32 = teachPoints.FindIndex(Function(_tp As TeachPoint) _tp.nr = tp.nr)
        If i >= 0 Then
            If MessageBox.Show($"Teachpunkt {tp.nr} ({teachPoints(i).name}) existiert bereits. Ersetzen?", "Teachpunkt ersetzen?", MessageBoxButtons.YesNo) _
             = Windows.Forms.DialogResult.No Then
                Return
            End If
            teachPoints(i) = tp
            OnProgChanged(New ProgChangedEventArgs(False, True, True, -1))
        Else
            teachPoints.Add(tp)
            'Liste Sortieren
            teachPoints.Sort()
            OnProgChanged(New ProgChangedEventArgs(False, False, True, teachPoints.Count - 1))
        End If
        OnLog(New LogEventArgs($"Teachpunkt {nr} hinzugefügt!", Logger.LogLevel.INFO))
    End Sub

    Public Sub deleteTeachPoint(index As Int32)
        If teachPoints.Count > index And index >= 0 Then
            'Prüfen ob Teachpunkt benutzt wird
            If progList.Exists(Function(_seq As ProgramEntry) _seq.tpnr = teachPoints(index).nr) Then
                OnLog(New LogEventArgs($"Teachpunkt {teachPoints(index).nr} wird verwendet und kann nicht gelöscht werden!", Logger.LogLevel.ERR))
            Else
                OnLog(New LogEventArgs($"Teachpunkt {teachPoints(index).nr} wurde gelöscht!", Logger.LogLevel.INFO))
                teachPoints.RemoveAt(index)
            End If
            OnProgChanged(New ProgChangedEventArgs(False, False, True, index - 1))
        End If
    End Sub

    Public Function getTeachPointName(nr As Int32) As String
        Return teachPoints.Find(Function(_tp As TeachPoint) _tp.nr = nr).name
    End Function

    Public Function moveToTeachPoint(index As Int32, sync As Boolean) As Boolean
        If teachPoints.Count > index And index >= 0 Then
            Return doMov(sync, True, teachPoints(index).jointAngles(0), True, teachPoints(index).jointAngles(1), True,
              teachPoints(index).jointAngles(2), True, teachPoints(index).jointAngles(3), True, teachPoints(index).jointAngles(4),
              True, teachPoints(index).jointAngles(5))
        End If
        Return False
    End Function

    Public Sub moveTPUpDown(down As Boolean, index As Int32)
        Dim tmpNewIndex As Int32
        If teachPoints.Count > index And index >= 0 Then
            If down Then
                If index < teachPoints.Count - 1 Then 'Es gibt noch mind. einen weiteren Eintrag => also tauschen
                    If teachPoints(index + 1).nr = teachPoints(index).nr + 1 Then
                        teachPoints(index).nr += 1
                        teachPoints(index + 1).nr -= 1
                        swapTPNr(teachPoints(index).nr, teachPoints(index + 1).nr)
                        tmpNewIndex = index + 1
                    Else
                        teachPoints(index).nr += 1
                        changeTPNr(teachPoints(index).nr - 1, teachPoints(index).nr)
                        tmpNewIndex = index
                    End If
                Else
                    teachPoints(index).nr += 1
                    changeTPNr(teachPoints(index).nr - 1, teachPoints(index).nr)
                    tmpNewIndex = index
                End If
            Else
                If teachPoints(index).nr > 0 Then
                    If index > 0 Then
                        If teachPoints(index - 1).nr = teachPoints(index).nr - 1 Then
                            teachPoints(index).nr -= 1
                            teachPoints(index - 1).nr += 1
                            swapTPNr(teachPoints(index).nr, teachPoints(index - 1).nr)
                            tmpNewIndex = index - 1
                        Else
                            teachPoints(index).nr -= 1
                            changeTPNr(teachPoints(index).nr + 1, teachPoints(index).nr)
                            tmpNewIndex = index
                        End If
                    Else
                        teachPoints(index).nr -= 1
                        changeTPNr(teachPoints(index).nr + 1, teachPoints(index).nr)
                        tmpNewIndex = index
                    End If
                End If
            End If
            'Liste neu sortieren
            teachPoints.Sort()
            'TPs Updaten
            OnProgChanged(New ProgChangedEventArgs(False, False, True, tmpNewIndex))
        End If
    End Sub

    'PROGRAMM
    Public Sub addProgItem(item As ProgramEntry, index As Int32)
        progList.Insert(index + 1, item)
        OnProgChanged(New ProgChangedEventArgs(False, True, False, index + 1))
    End Sub

    Public Sub delProgItem(index As Int32)
        If index >= 0 And index < progList.Count Then
            progList.RemoveAt(index)
            OnProgChanged(New ProgChangedEventArgs(False, True, False, progList.Count - 1))
        End If
    End Sub

    Public Sub replaceProgItem(item As ProgramEntry, index As Int32)
        If index >= 0 And index < progList.Count Then
            progList(index) = item
            OnProgChanged(New ProgChangedEventArgs(False, True, False, index))
        End If
    End Sub

    Public Function copyProgItem(index As Int32) As Boolean
        If index >= 0 And index < progList.Count Then
            progClipboard = progList(index)
            Return True
        End If
        Return False
    End Function

    Public Sub pasteProgItem(index As Int32)
        addProgItem(progClipboard, index)
    End Sub
    Public Function isProgRunning() As Boolean
        Return _programRunning
    End Function

    Public Sub executeProgramm(loopSeq As Boolean)
        If progList.Count > 0 Then
            _programRunning = True
            Me._loopSeq = loopSeq
            _progIndex = 0
            If Not executeProgItem(0) Then
                _programRunning = False
            End If
            OnProgChanged(New ProgChangedEventArgs(True, True, False, _progIndex))
        End If
    End Sub

    Public Function executeProgItem(index As Int32) As Boolean
        If progList.Count > index And index >= 0 Then
            If progList(index).func = "pos" Then
                Dim tpI As Int32 = teachPoints.FindIndex(Function(_tp As TeachPoint) _tp.nr = progList(index).tpnr)
                If tpI = -1 Then
                    OnLog(New LogEventArgs($"Teachpunkt {progList(index).tpnr} existierte nicht!", Logger.LogLevel.ERR))
                    Return False
                End If
                setSpeedAndAcc(progList(index).speed, progList(index).acc)
                Return doMov(progList(index).sync, True, teachPoints(tpI).jointAngles(0), True, teachPoints(tpI).jointAngles(1), True,
                      teachPoints(tpI).jointAngles(2), True, teachPoints(tpI).jointAngles(3), True, teachPoints(tpI).jointAngles(4),
                      True, teachPoints(tpI).jointAngles(5))
            ElseIf progList(index).func = "srv" Then
                Return movServo(progList(index).servoNum, CInt(Math.Round(progList(index).servoVal, 0)))
            ElseIf progList(index).func = "wai" Then
                If com.sendWAI(progList(index).waitTimeMS) Then
                    'Log
                    OnLog(New LogEventArgs($"Waiting for {progList(index).waitTimeMS} milliseconds...", Logger.LogLevel.INFO))
                    Return True
                End If
            End If
        End If
        Return False
    End Function

    Public Sub stopProgram()
        If _programRunning Then
            _programRunning = False
        End If
    End Sub

    Public Function saveProgram() As Boolean
        Dim saveFileDialog As New SaveFileDialog With {
           .Filter = "Positions-Dateien (*.pos)|*.pos"
       }
        If saveFileDialog.ShowDialog() = DialogResult.OK Then
            Dim objStreamWriter As StreamWriter
            objStreamWriter = New StreamWriter(saveFileDialog.FileName)
            'TeachPunkte
            For Each x As TeachPoint In teachPoints
                objStreamWriter.WriteLine(x.name & ";tp;" & x.nr & ";" & x.jointAngles(0) & ";" & x.jointAngles(1) & ";" _
                         & x.jointAngles(2) & ";" & x.jointAngles(3) & ";" _
                         & x.jointAngles(4) & ";" & x.jointAngles(5))
            Next
            'Programm
            For Each x As ProgramEntry In progList
                If x.func = "pos" Then
                    objStreamWriter.WriteLine(x.comment & ";pos;" & x.tpnr & ";" _
                         & x.speed & ";" & x.acc & ";" & x.sync)
                ElseIf x.func = "srv" Then
                    objStreamWriter.WriteLine(x.comment & ";srv;" & x.servoNum & ";" & x.servoVal)
                ElseIf x.func = "wai" Then
                    objStreamWriter.WriteLine(x.comment & ";wai;" & x.waitTimeMS)
                End If
            Next
            objStreamWriter.Close()
            Return True
        Else
            Return False
        End If
    End Function

    Public Function loadProgram() As Boolean
        Dim tmpErg As Boolean = True
        Dim openFileDialog As New OpenFileDialog With {
           .Filter = "Positions-Dateien (*.pos)|*.pos"
       }
        If openFileDialog.ShowDialog() = DialogResult.OK Then
            Dim objStreamReader As StreamReader
            objStreamReader = New StreamReader(openFileDialog.FileName)
            Try
                'Jede Zeile der pos Datei einlesen
                Dim strLine As String
                Dim tmpSeqList As New List(Of ProgramEntry)
                Dim tmpTpList As New List(Of TeachPoint)
                Do
                    strLine = objStreamReader.ReadLine
                    If Not strLine Is Nothing Then
                        Dim tmpSplit As String() = strLine.Split(";"c)
                        Dim func As String = tmpSplit(1)

                        If func = "tp" Then
                            'TeachPunkte
                            Dim item As New TeachPoint
                            item.name = tmpSplit(0)
                            item.nr = CInt(tmpSplit(2))
                            item.jointAngles(0) = CDec(tmpSplit(3))
                            item.jointAngles(1) = CDec(tmpSplit(4))
                            item.jointAngles(2) = CDec(tmpSplit(5))
                            item.jointAngles(3) = CDec(tmpSplit(6))
                            item.jointAngles(4) = CDec(tmpSplit(7))
                            item.jointAngles(5) = CDec(tmpSplit(8))
                            tmpTpList.Add(item)
                        Else
                            'Programm
                            Dim item As New ProgramEntry
                            item.comment = tmpSplit(0)
                            If func = "pos" Then
                                item.func = "pos"
                                item.tpnr = CInt(tmpSplit(2))
                                item.speed = CDec(tmpSplit(3))
                                item.acc = CDec(tmpSplit(4))
                                item.sync = CBool(tmpSplit(5))
                            ElseIf func = "srv" Then
                                item.func = "srv"
                                item.servoNum = CInt(tmpSplit(2))
                                item.servoVal = CInt(tmpSplit(3))
                            ElseIf func = "wai" Then
                                item.func = "wai"
                                item.waitTimeMS = CInt(tmpSplit(2))
                            End If
                            tmpSeqList.Add(item)
                        End If
                    End If
                Loop Until strLine Is Nothing
                'Listen kopieren
                teachPoints.Clear()
                For Each x As TeachPoint In tmpTpList
                    teachPoints.Add(x)
                Next
                progList.Clear()
                For Each x As ProgramEntry In tmpSeqList
                    progList.Add(x)
                Next
                'und Changed Event auslösen
                OnProgChanged(New ProgChangedEventArgs(False, True, True, progList.Count - 1))
            Catch ex As Exception
                teachPoints.Clear()
                progList.Clear()
                tmpErg = False
            Finally
                'StreamReader schließen
                objStreamReader.Close()
            End Try
        Else
            tmpErg = False
        End If
        Return tmpErg
    End Function

    Public Sub eFIN_Received(sender As Object, e As EventArgs) Handles com.FIN_Received
        If _programRunning Then
            _progIndex += 1
            If _loopSeq And _progIndex >= progList.Count Then
                _progIndex = 0
            End If
            If _progIndex < progList.Count Then
                If Not executeProgItem(_progIndex) Then
                    _programRunning = False
                End If
                OnProgChanged(New ProgChangedEventArgs(True, True, False, _progIndex))
            Else
                _programRunning = False
                OnProgChanged(New ProgChangedEventArgs(False))
            End If
        End If
        'Log
        OnLog(New LogEventArgs("Finish...", Logger.LogLevel.INFO))
    End Sub

    'ROBOT MOVEMENTS
    Public Sub setSpeedAndAcc(speed As Double, acc As Double)
        _actV = speed
        _actA = acc
    End Sub

    Public Function doJog(nr As Int32, jogval As Double) As Boolean
        Dim tmpV(5) As Double
        Dim tmpA(5) As Double
        'aktuelle Geschwindigkeit und Beschleunigung berechnen
        calcSpeedAcc(tmpV, tmpA)
        'Datensatz zusammenstellen
        com.resetDataSets()
        target(nr - 1) = Constrain(pos(nr - 1) + jogval, pref.JointParameter(nr - 1).mechMinAngle, pref.JointParameter(nr - 1).mechMaxAngle)
        com.addMOVDataSet(True, nr, calcTargetToSteps(target(nr - 1), nr), calcSpeedAccToSteps(tmpV(nr - 1), nr), calcSpeedAccToSteps(tmpA(nr - 1), nr), calcSpeedAccToSteps(pref.JointParameter(nr - 1).profileStopAcc, nr))
        'Telegramm senden
        If com.sendMOV() Then
            'Log
            OnLog(New LogEventArgs($"Moving Axis...", Logger.LogLevel.INFO))
            Return True
        Else Return False
        End If
    End Function

    Public Function doJog(nr As Int32, jogval As Int32) As Boolean
        'Jog steps
        Dim tmpJogval As Double = StepsToAngle(jogval, pref.JointParameter(nr - 1).motGear, pref.JointParameter(nr - 1).mechGear, pref.JointParameter(nr - 1).motStepsPerRot << pref.JointParameter(nr - 1).motMode, 0)
        'Datensatz zusammenstellen
        Return doJog(nr, tmpJogval)
    End Function

    Public Function doRef(J1 As Boolean, J2 As Boolean, J3 As Boolean, J4 As Boolean, J5 As Boolean, J6 As Boolean) As Boolean
        'Datensätze sammeln
        com.resetDataSets()
        Dim enabled() As Boolean = {J1, J2, J3, J4, J5, J6}
        For i = 0 To 5
            Dim tmpMaxStepsBack As Int32 = AngleToSteps(_constRefMaxAngleBack, pref.JointParameter(i).motGear, pref.JointParameter(i).mechGear, pref.JointParameter(i).motStepsPerRot << pref.JointParameter(i).motMode, 0)
            com.addREFDataSet(enabled(i), i + 1, pref.JointParameter(i).calDir Xor pref.JointParameter(i).motDir, calcSpeedAccToSteps(pref.JointParameter(i).calSpeedFast, i + 1), calcSpeedAccToSteps(pref.JointParameter(i).calSpeedSlow, i + 1), calcSpeedAccToSteps(pref.JointParameter(i).calAcc, i + 1), tmpMaxStepsBack, calcSpeedAccToSteps(pref.JointParameter(i).profileStopAcc, i + 1))
        Next
        'Telegram senden
        If com.sendREF() Then
            'Log
            OnLog(New LogEventArgs($"Referenz läuft...", Logger.LogLevel.INFO))
            Return True
        Else Return False
        End If
    End Function

    Public Function doMov(sync As Boolean, J1_en As Boolean, J1_target As Double, J2_en As Boolean, J2_target As Double, J3_en As Boolean, J3_target As Double, J4_en As Boolean, J4_target As Double, J5_en As Boolean, J5_target As Double, J6_en As Boolean, J6_target As Double) As Boolean
        'Daten aufbereiten
        Dim tmpV(5) As Double
        Dim tmpA(5) As Double
        Dim enabled() As Boolean = {J1_en, J2_en, J3_en, J4_en, J5_en, J6_en}
        target = {J1_target, J2_target, J3_target, J4_target, J5_target, J6_target}
        'Prüfen ob Ziel schon erreicht (keine Fahrt mehr notwendig)
        For i = 0 To 5
            If Math.Abs(target(i) - pos(i)) < 0.01 Then
                enabled(i) = False
                target(i) = pos(i)
            End If
        Next
        'aktuelle Geschwindigkeit und Beschleunigung berechnen
        calcSpeedAcc(tmpV, tmpA)
        'Synchronisierte Bewegung berechnen, falls gewünscht
        If sync Then
            For i = 0 To 5
                _oSyncMov.v_max(i) = tmpV(i)
                _oSyncMov.a_max(i) = tmpA(i)
                _oSyncMov.s(i) = If(enabled(i), Math.Abs(target(i) - pos(i)), 0)
            Next
            If Not _oSyncMov.calculate() Then
                OnLog(New LogEventArgs("Berechnung für synchrone Bewegung fehlgeschlagen!", Logger.LogLevel.ERR))
                Array.Copy(pos, target, pos.Length()) ' Ziel auf aktuelle Position setzen
            Else
                'Geschwindigkeit und Beschleunigung zuweisen
                Array.Copy(_oSyncMov.v, tmpV, _oSyncMov.v.Length)
                Array.Copy(_oSyncMov.a, tmpA, _oSyncMov.a.Length)
            End If
        End If
        'Datensätze sammeln
        com.resetDataSets()
        For i = 0 To 5
            com.addMOVDataSet(enabled(i), i + 1, calcTargetToSteps(target(i), i + 1), calcSpeedAccToSteps(tmpV(i), i + 1), calcSpeedAccToSteps(tmpA(i), i + 1), calcSpeedAccToSteps(pref.JointParameter(i).profileStopAcc, i + 1))
        Next
        'Telegram senden
        If com.sendMOV() Then
            'Log
            OnLog(New LogEventArgs($"Moving Axis...", Logger.LogLevel.INFO))
            Return True
        Else Return False
        End If
    End Function
    Public Function doHome(sync As Boolean, J1_en As Boolean, J2_en As Boolean, J3_en As Boolean, J4_en As Boolean, J5_en As Boolean, J6_en As Boolean) As Boolean
        Return doMov(sync, J1_en, pref.JointParameter(0).mechHomePosAngle, J2_en, pref.JointParameter(1).mechHomePosAngle, J3_en, pref.JointParameter(2).mechHomePosAngle, J4_en, pref.JointParameter(3).mechHomePosAngle, J5_en, pref.JointParameter(4).mechHomePosAngle, J6_en, pref.JointParameter(5).mechHomePosAngle)
    End Function
    Public Function doPark(sync As Boolean, J1_en As Boolean, J2_en As Boolean, J3_en As Boolean, J4_en As Boolean, J5_en As Boolean, J6_en As Boolean) As Boolean
        Return doMov(sync, J1_en, pref.JointParameter(0).mechParkPosAngle, J2_en, pref.JointParameter(1).mechParkPosAngle, J3_en, pref.JointParameter(2).mechParkPosAngle, J4_en, pref.JointParameter(3).mechParkPosAngle, J5_en, pref.JointParameter(4).mechParkPosAngle, J6_en, pref.JointParameter(5).mechParkPosAngle)
    End Function
    Public Function movServo(srvNr As Int32, angle As Int32) As Boolean
        If com.sendSRV(srvNr, angle) Then
            'Log
            OnLog(New LogEventArgs($"Moving Servo {srvNr} to {angle}...", Logger.LogLevel.INFO))
            Return True
        Else Return False
        End If
    End Function
    Public Sub fastStop()
        stopProgram() 'Sequenz stoppen
        com.sendStop()
        'Log
        OnLog(New LogEventArgs("Programm gestoppt!", Logger.LogLevel.INFO))
    End Sub

    'CALCULATIONS
    Public Function calcServoAngle(srvNr As Int32, prc As Double) As Int32
        Return CInt((pref.ServoParameter(srvNr - 1).maxAngle - pref.ServoParameter(srvNr - 1).minAngle) * (prc / 100.0) + pref.ServoParameter(srvNr - 1).minAngle)
    End Function

    ' -----------------------------------------------------------------------------
    ' Private
    ' -----------------------------------------------------------------------------
    'TEACHPUNKTE
    Private Sub swapTPNr(num1 As Int32, num2 As Int32)
        For Each x As ProgramEntry In progList
            If x.tpnr = num1 Then
                x.tpnr = num2
            ElseIf x.tpnr = num2 Then
                x.tpnr = num1
            End If
        Next
    End Sub
    Private Sub changeTPNr(numOld As Int32, numNew As Int32)
        For Each x As ProgramEntry In progList
            If x.tpnr = numOld Then
                x.tpnr = numNew
            End If
        Next
    End Sub

    'EVENT
    Private Sub ePOS_Received(sender As Object, e As POSReceivedEventArgs) Handles com.POS_Received
        For i = 0 To 5
            pos(i) = StepsToAngle(e.posSteps(i), pref.JointParameter(i).motGear, pref.JointParameter(i).mechGear, pref.JointParameter(i).motStepsPerRot << pref.JointParameter(i).motMode, If(pref.JointParameter(i).calDir = 0, pref.JointParameter(i).mechMinAngle * -1, pref.JointParameter(i).mechMaxAngle * -1))
        Next
        OnNewPos(New NewPosEventArgs(e.refOkay, pos))
    End Sub

    'CALCULATIONS
    Private Sub calcSpeedAcc(ByRef v() As Double, ByRef a() As Double)
        'Maximale Geschwindigkeit und Beschleunigung jeder Achse ermitteln (Maxwert oder aktueller Wert, je nachdem welcher kleiner ist!)
        For i = 0 To 5
            If pref.JointParameter(i).profileMaxSpeed < _actV Then
                v(i) = pref.JointParameter(i).profileMaxSpeed
            Else
                v(i) = _actV
            End If

            If pref.JointParameter(i).profileMaxAcc < _actA Then
                a(i) = pref.JointParameter(i).profileMaxAcc
            Else
                a(i) = _actA
            End If
        Next
    End Sub

    Private Function calcTargetToSteps(target As Double, nr As Int32) As Int32
        Return If(pref.JointParameter(nr - 1).motDir = 1, -1, 1) * AngleToSteps(target, pref.JointParameter(nr - 1).motGear, pref.JointParameter(nr - 1).mechGear, pref.JointParameter(nr - 1).motStepsPerRot << pref.JointParameter(nr - 1).motMode, If(pref.JointParameter(nr - 1).calDir = 0, pref.JointParameter(nr - 1).mechMinAngle * -1, pref.JointParameter(nr - 1).mechMaxAngle * -1))
    End Function

    Private Function calcSpeedAccToSteps(speedAcc As Double, nr As Int32) As Int32
        Return AngleToSteps(speedAcc, pref.JointParameter(nr - 1).motGear, pref.JointParameter(nr - 1).mechGear, pref.JointParameter(nr - 1).motStepsPerRot, 0)
    End Function

    'EVENTS
    Protected Sub OnNewPos(e As NewPosEventArgs)
        RaiseEvent NewPos(Me, e)
    End Sub

    Protected Sub OnLog(e As LogEventArgs)
        RaiseEvent Log(Me, e)
    End Sub

    Protected Sub OnProgChanged(e As ProgChangedEventArgs)
        RaiseEvent SeqListChanged(Me, e)
    End Sub
End Class

Public Class TeachPoint
    Implements IComparable(Of TeachPoint)

    Public nr As Int32
    Public name As String
    Public jointAngles(5) As Double

    Public Function CompareTo(other As TeachPoint) As Integer _
           Implements IComparable(Of TeachPoint).CompareTo
        ' A null value means that this object is greater.
        If other Is Nothing Then
            Return 1
        Else
            Return Me.nr.CompareTo(other.nr)
        End If
    End Function
End Class

Public Class ProgramEntry
    Public comment As String
    Public func As String
    'Position
    Public tpnr As Int32
    Public speed As Double
    Public acc As Double
    Public sync As Boolean
    'Servo
    Public servoNum As Int32
    Public servoVal As Int32
    'Wait
    Public waitTimeMS As Int32
End Class

Public Class ProgChangedEventArgs : Inherits EventArgs
    Private _actProgIndex As Int32
    Private _actTpIndex As Int32
    Private _running As Boolean = False
    Private _prog As Boolean = False
    Private _tp As Boolean

    Public Sub New(running As Boolean, prog As Boolean, tp As Boolean, index As Int32)
        _running = running
        _prog = prog
        _tp = tp
        If prog Then
            _actProgIndex = index
            _actTpIndex = -1
        ElseIf tp Then
            _actProgIndex = -1
            _actTpIndex = index
        Else
            _actProgIndex = -1
            _actTpIndex = -1
        End If
    End Sub
    Public Sub New(running As Boolean)
        _running = running
        _actProgIndex = -1
        _actTpIndex = -1
        _prog = True
        _tp = True
    End Sub
    Public ReadOnly Property running As Boolean
        Get
            Return _running
        End Get
    End Property
    Public ReadOnly Property prog As Boolean
        Get
            Return _prog
        End Get
    End Property
    Public ReadOnly Property tp As Boolean
        Get
            Return _tp
        End Get
    End Property
    Public ReadOnly Property actProgIndex As Int32
        Get
            Return _actProgIndex
        End Get
    End Property
    Public ReadOnly Property actTpIndex As Int32
        Get
            Return _actTpIndex
        End Get
    End Property
End Class

Public Class NewPosEventArgs : Inherits EventArgs
    Private _refOkay(5) As Boolean
    Private _pos(5) As Double
    Public Sub New(refOkay As Boolean(), pos As Double())
        _refOkay = refOkay
        _pos = pos
    End Sub
    Public ReadOnly Property refOkay As Boolean()
        Get
            Return _refOkay
        End Get
    End Property
    Public ReadOnly Property posDeg As Double()
        Get
            Return _pos
        End Get
    End Property
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