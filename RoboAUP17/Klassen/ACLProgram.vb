Imports System.IO
Friend Class ACLProgram
    ' -----------------------------------------------------------------------------
    ' TODO
    ' -----------------------------------------------------------------------------
    ' Teachpunkte (wip...)
    ' Variablen
    ' TCP Variablen
    ' ACL-Programm
    ' Teachpunkte, Variablen, TCP Variablen und ACL-Programm in einer Datei speichern und laden!

    ' -----------------------------------------------------------------------------
    ' Definitions
    ' -----------------------------------------------------------------------------
    Private _teachPoints As New List(Of TeachPoint)

    Friend Event Log(ByVal LogMsg As String, ByVal LogLvl As Logger.LogLevel)
    ' -----------------------------------------------------------------------------
    ' Public
    ' -----------------------------------------------------------------------------
    'TEACHPUNKTE
    Friend Sub AddTeachPoint(tp As TeachPoint, nr As Int32)
        Dim i As Int32 = _teachPoints.FindIndex(Function(_tp As TeachPoint) _tp.nr = tp.nr)
        If i >= 0 Then
            If MessageBox.Show($"Teachpunkt {tp.nr} ({_teachPoints(i).name}) existiert bereits. Ersetzen?", "Teachpunkt ersetzen?", MessageBoxButtons.YesNo) _
             = Windows.Forms.DialogResult.No Then
                Return
            End If
            _teachPoints(i) = tp
        Else
            _teachPoints.Add(tp)
            'Liste Sortieren
            _teachPoints.Sort()
        End If
        RaiseEvent Log($"Teachpunkt {nr} hinzugefügt!", Logger.LogLevel.INFO)
    End Sub

    Friend Sub DeleteTeachPoint(index As Int32)
        If _teachPoints.Count > index And index >= 0 Then
            RaiseEvent Log($"Teachpunkt {_teachPoints(index).nr} wurde gelöscht!", Logger.LogLevel.INFO)
            _teachPoints.RemoveAt(index)
            RaiseEvent ProgChanged(New ProgChangedEventArgs(False, False, True, index - 1))
        End If
    End Sub

    Friend Function getTeachPointName(nr As Int32) As String
        Return _teachPoints.Find(Function(_tp As TeachPoint) _tp.nr = nr).name
    End Function

    Friend Function moveToTeachPoint(index As Int32, sync As Boolean) As Boolean
        If _teachPoints.Count > index And index >= 0 Then
            'Return doJointMov(sync, True, teachPoints(index).jointAngles(0), True, teachPoints(index).jointAngles(1), True,
            'teachPoints(index).jointAngles(2), True, teachPoints(index).jointAngles(3), True, teachPoints(index).jointAngles(4),
            '  True, teachPoints(index).jointAngles(5))
        End If
        Return False
    End Function


    ' -----------------------------------------------------------------------------
    ' AB HIER ALT!!
    ' -----------------------------------------------------------------------------

    ' -----------------------------------------------------------------------------
    ' Definitions
    ' -----------------------------------------------------------------------------
    Friend progList As New List(Of ProgramEntry)

    Friend progClipboard As New ProgramEntry
    Friend teachPointClipboard As New TeachPoint
    Private _programRunning, _loopSeq As Boolean
    Private _progIndex As Int32


    Friend Event ProgChanged(ByVal e As ProgChangedEventArgs)
    ' -----------------------------------------------------------------------------
    ' Public
    ' -----------------------------------------------------------------------------

    'PROGRAMM
    Friend Sub addProgItem(item As ProgramEntry, index As Int32)
        progList.Insert(index + 1, item)
        RaiseEvent ProgChanged(New ProgChangedEventArgs(False, True, False, index + 1))
    End Sub

    Friend Sub delProgItem(index As Int32)
        If index >= 0 And index < progList.Count Then
            progList.RemoveAt(index)
            RaiseEvent ProgChanged(New ProgChangedEventArgs(False, True, False, progList.Count - 1))
        End If
    End Sub

    Friend Sub replaceProgItem(item As ProgramEntry, index As Int32)
        If index >= 0 And index < progList.Count Then
            progList(index) = item
            RaiseEvent ProgChanged(New ProgChangedEventArgs(False, True, False, index))
        End If
    End Sub

    Friend Function copyProgItem(index As Int32) As Boolean
        If index >= 0 And index < progList.Count Then
            progClipboard = progList(index)
            Return True
        End If
        Return False
    End Function

    Friend Sub pasteProgItem(index As Int32)
        addProgItem(progClipboard, index)
    End Sub
    Friend Function isProgRunning() As Boolean
        Return _programRunning
    End Function

    Friend Sub executeProgramm(loopSeq As Boolean)
        If progList.Count > 0 Then
            _programRunning = True
            Me._loopSeq = loopSeq
            _progIndex = 0
            If Not executeProgItem(0) Then
                _programRunning = False
            End If
            RaiseEvent ProgChanged(New ProgChangedEventArgs(True, True, False, _progIndex))
        End If
    End Sub

    Friend Function executeProgItem(index As Int32) As Boolean
        If progList.Count > index And index >= 0 Then
            If progList(index).func = "pos" Then
                Dim tpI As Int32 = _teachPoints.FindIndex(Function(_tp As TeachPoint) _tp.nr = progList(index).tpnr)
                If tpI = -1 Then
                    RaiseEvent Log($"Teachpunkt {progList(index).tpnr} existierte nicht!", Logger.LogLevel.ERR)
                    Return False
                End If
                'setSpeedAndAcc(progList(index).speed, progList(index).acc)
                'Return doJointMov(progList(index).sync, True, teachPoints(tpI).jointAngles(0), True, teachPoints(tpI).jointAngles(1), True,
                'teachPoints(tpI).jointAngles(2), True, teachPoints(tpI).jointAngles(3), True, teachPoints(tpI).jointAngles(4),
                '     True, teachPoints(tpI).jointAngles(5))
            ElseIf progList(index).func = "srv" Then
                'Return movServo(progList(index).servoNum, CInt(Math.Round(progList(index).servoVal, 0)))
            ElseIf progList(index).func = "wai" Then
                'If com.sendWAI(progList(index).waitTimeMS) Then
                ''Log
                'RaiseEvent Log($"Waiting for {progList(index).waitTimeMS} milliseconds...", Logger.LogLevel.INFO)
                'Return True
                'End If
            End If
        End If
        Return False
    End Function

    Friend Sub stopProgram()
        If _programRunning Then
            _programRunning = False
        End If
    End Sub

    Friend Function saveProgram() As Boolean
        Dim saveFileDialog As New SaveFileDialog With {
           .Filter = "Positions-Dateien (*.pos)|*.pos"
       }
        If saveFileDialog.ShowDialog() = DialogResult.OK Then
            Dim objStreamWriter As StreamWriter
            objStreamWriter = New StreamWriter(saveFileDialog.FileName)
            'TeachPunkte
            For Each x As TeachPoint In _teachPoints
                objStreamWriter.WriteLine(x.name & ";tp;" & x.nr & ";" & x.jointAngles.Items(0) & ";" & x.jointAngles.Items(1) & ";" _
                         & x.jointAngles.Items(2) & ";" & x.jointAngles.Items(3) & ";" _
                         & x.jointAngles.Items(4) & ";" & x.jointAngles.Items(5))
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

    Friend Function loadProgram() As Boolean
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
                            item.jointAngles.SetByIndex(0, CDec(tmpSplit(3)))
                            item.jointAngles.SetByIndex(1, CDec(tmpSplit(4)))
                            item.jointAngles.SetByIndex(2, CDec(tmpSplit(5)))
                            item.jointAngles.SetByIndex(3, CDec(tmpSplit(6)))
                            item.jointAngles.SetByIndex(4, CDec(tmpSplit(7)))
                            item.jointAngles.SetByIndex(5, CDec(tmpSplit(8)))
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
                _teachPoints.Clear()
                For Each x As TeachPoint In tmpTpList
                    _teachPoints.Add(x)
                Next
                progList.Clear()
                For Each x As ProgramEntry In tmpSeqList
                    progList.Add(x)
                Next
                'und Changed Event auslösen
                RaiseEvent ProgChanged(New ProgChangedEventArgs(False, True, True, progList.Count - 1))
            Catch ex As Exception
                _teachPoints.Clear()
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

    Friend Sub fastStop()
        stopProgram() 'Sequenz stoppen
        'com.sendStop()
        'Log
        RaiseEvent Log("[Robo Control] Programm gestoppt", Logger.LogLevel.INFO)
    End Sub

    ' -----------------------------------------------------------------------------
    ' Private
    ' -----------------------------------------------------------------------------
    'TEACHPUNKTE
    Private Sub _swapTPNr(num1 As Int32, num2 As Int32)
        For Each x As ProgramEntry In progList
            If x.tpnr = num1 Then
                x.tpnr = num2
            ElseIf x.tpnr = num2 Then
                x.tpnr = num1
            End If
        Next
    End Sub
    Private Sub _changeTPNr(numOld As Int32, numNew As Int32)
        For Each x As ProgramEntry In progList
            If x.tpnr = numOld Then
                x.tpnr = numNew
            End If
        Next
    End Sub

    'EVENTS
    'Private Sub _eFINReceived() Handles com.FINReceived
    'If _programRunning Then
    '       _progIndex += 1
    'If _loopSeq And _progIndex >= progList.Count Then
    '           _progIndex = 0
    'End If
    'If _progIndex < progList.Count Then
    'If Not executeProgItem(_progIndex) Then
    '                _programRunning = False
    'End If
    'RaiseEvent ProgChanged(New ProgChangedEventArgs(True, True, False, _progIndex))
    'Else
    '           _programRunning = False
    'RaiseEvent ProgChanged(New ProgChangedEventArgs(False))
    'End If
    'End If
    ''Log
    'RaiseEvent Log("[Robo Control] Fertig...", Logger.LogLevel.INFO)
    'End Sub
End Class



Friend Class ProgramEntry
    Friend comment As String
    Friend func As String
    'Position
    Friend tpnr As Int32
    Friend speed As Double
    Friend acc As Double
    Friend sync As Boolean
    'Servo
    Friend servoNum As Int32
    Friend servoVal As Int32
    'Wait
    Friend waitTimeMS As Int32
End Class

Friend Class ProgChangedEventArgs : Inherits EventArgs
    Private _actProgIndex As Int32
    Private _actTpIndex As Int32
    Private _running As Boolean = False
    Private _prog As Boolean = False
    Private _tp As Boolean

    Friend Sub New(running As Boolean, prog As Boolean, tp As Boolean, index As Int32)
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
    Friend Sub New(running As Boolean)
        _running = running
        _actProgIndex = -1
        _actTpIndex = -1
        _prog = True
        _tp = True
    End Sub
    Friend ReadOnly Property running As Boolean
        Get
            Return _running
        End Get
    End Property
    Friend ReadOnly Property prog As Boolean
        Get
            Return _prog
        End Get
    End Property
    Friend ReadOnly Property tp As Boolean
        Get
            Return _tp
        End Get
    End Property
    Friend ReadOnly Property actProgIndex As Int32
        Get
            Return _actProgIndex
        End Get
    End Property
    Friend ReadOnly Property actTpIndex As Int32
        Get
            Return _actTpIndex
        End Get
    End Property
End Class
