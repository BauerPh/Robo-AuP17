Imports System.IO
Imports Antlr4.Runtime
Imports Antlr4.Runtime.Misc
Imports ACLLexerParser
Friend Class ACLProgram
    ' -----------------------------------------------------------------------------
    ' TODO
    ' -----------------------------------------------------------------------------
    ' ACL-Programm (wip...)
    ' Variablen
    ' TCP Variablen
    ' Teachpunkte, Variablen, TCP Variablen und ACL-Programm in einer Datei speichern und laden!

    ' -----------------------------------------------------------------------------
    ' Definitions
    ' -----------------------------------------------------------------------------
    Private _teachPoints As New List(Of TeachPoint)
    Private _listBox As ListBox
    Private _progList As New List(Of ProgramEntry)
    Private _programSyntaxOkay As Boolean = False
    Private _programCompiled As Boolean = False

    Friend Event Log(ByVal LogMsg As String, ByVal LogLvl As Logger.LogLevel)
    Friend Event DoMove(ByVal tp As TeachPoint)

    ' -----------------------------------------------------------------------------
    ' Public
    ' -----------------------------------------------------------------------------
#Region "Teachpunkte"
    Friend Function GetTeachpointByIndex(index As Int32) As TeachPoint
        If _teachPoints.Count > index And index >= 0 Then
            Return _teachPoints(index)
        End If
        Return Nothing
    End Function
    Friend Function AddTeachPoint(name As String, cartCoords As CartCoords, tpNr As Int32) As Boolean
        Dim tp As TeachPoint
        tp.cart = True
        tp.tcpCoords = cartCoords
        tp.name = name
        tp.nr = tpNr
        Return _addTeachPoint(tp)
    End Function
    Friend Function AddTeachPoint(name As String, jointAngles As JointAngles, tpNr As Int32) As Boolean
        Dim tp As TeachPoint
        tp.cart = False
        tp.jointAngles = jointAngles
        tp.name = name
        tp.nr = tpNr
        Return _addTeachPoint(tp)
    End Function
    Friend Sub MoveTeachPoint(index As Int32, up As Boolean)
        If _teachPoints.Count > index And index >= 0 Then
            Dim indexVal As Int32 = 1
            If up Then indexVal = -1

            If (up And index > 0) Or (Not up And index < _teachPoints.Count - 1) Then
                If _teachPoints(index + indexVal).nr = (_teachPoints(index).nr + indexVal) Then
                    'Nummer tauschen
                    Dim nr As Int32 = _teachPoints(index + indexVal).nr
                    Dim tp As TeachPoint = _teachPoints(index + indexVal)
                    tp.nr = _teachPoints(index).nr
                    _teachPoints(index + indexVal) = tp
                    tp = _teachPoints(index)
                    tp.nr = nr
                    _teachPoints(index) = tp
                Else
                    'Nummer inkrementieren / dekrementieren
                    Dim tp As TeachPoint = _teachPoints(index)
                    tp.nr += indexVal
                    _teachPoints(index) = tp
                End If
                _teachPoints.Sort()
                Dim selIndex As Int32 = _listBox.SelectedIndex + indexVal
                _printTeachpointToListBox()
                _listBox.SelectedIndex = selIndex
            End If
        End If
    End Sub

    Friend Sub SetListBox(lb As ListBox)
        _listBox = lb
    End Sub

    Friend Sub DeleteTeachPoint(index As Int32)
        If _teachPoints.Count > index And index >= 0 Then
            Dim tpNr As Int32 = _teachPoints(index).nr
            _teachPoints.RemoveAt(index)
            Dim selIndex As Int32 = _listBox.SelectedIndex - 1
            _printTeachpointToListBox()
            If selIndex < 0 And _listBox.Items.Count > 0 Then selIndex = 0
            _listBox.SelectedIndex = selIndex
            RaiseEvent Log($"[ACL] Teachpunkt {tpNr} wurde gelöscht!", Logger.LogLevel.INFO)
        End If
    End Sub

    Friend Sub MoveToTeachPoint(index As Int32, sync As Boolean)
        If _teachPoints.Count > index And index >= 0 Then
            RaiseEvent DoMove(_teachPoints(index))
        End If
    End Sub
#End Region

#Region "ACL-Programm"
    Friend Function CompileProgram(input As String, acc As Double, speed As Double) As Boolean
        RaiseEvent Log("[ACL] erstelle Programm...", Logger.LogLevel.ERR)
        _programSyntaxOkay = True
        ' Erstelle Input Stream
        Dim inputStream As AntlrInputStream = New AntlrInputStream(input)
        ' Erstelle Lexer
        Dim aclLexer As ACLLexer = New ACLLexer(inputStream)
        ' Erstelle Token Stream
        Dim comTokenStream As CommonTokenStream = New CommonTokenStream(aclLexer)
        ' Erstelle Parser
        Dim aclParser As ACLParser = New ACLParser(comTokenStream)
        aclParser.BuildParseTree = True
        ' Eigenen Error Listener hinzufügen, um Syntaxfehler zu erkennen
        aclParser.RemoveErrorListeners()
        Dim aclErrorListener As New MyErrorListener()
        AddHandler aclErrorListener.SyntaxErrorEvent, AddressOf _eSyntaxError
        aclParser.AddErrorListener(aclErrorListener)
        ' Parser starten
        Dim rootContext As ACLParser.RootContext = aclParser.root()

        ' ACL Programm kompilieren, wenn Syntax okay
        If _programSyntaxOkay Then
            Dim aclListener As New ACLListener(_teachPoints, _progList, acc, speed)
            AddHandler aclListener.CompileErrorEvent, AddressOf _eCompileError
            _programCompiled = True
            _progList.Clear()
            Tree.ParseTreeWalker.Default.Walk(aclListener, rootContext)
            RemoveHandler aclListener.CompileErrorEvent, AddressOf _eCompileError
        Else
            RaiseEvent Log("[ACL] Syntaxfehler", Logger.LogLevel.ERR)
            _programCompiled = False
        End If

        ' Error Handler vom ErrorListener wieder entfernen
        RemoveHandler aclErrorListener.SyntaxErrorEvent, AddressOf _eSyntaxError

        If _programCompiled Then
            RaiseEvent Log("[ACL] erfolgreich kompiliert", Logger.LogLevel.INFO)
        Else
            RaiseEvent Log("[ACL] Kompilieren fehlgeschlagen", Logger.LogLevel.ERR)
        End If
        Return _programCompiled
    End Function
#End Region

    ' -----------------------------------------------------------------------------
    ' Private
    ' -----------------------------------------------------------------------------
#Region "Teachpunkte"
    Private Function _addTeachPoint(tp As TeachPoint) As Boolean
        Dim i As Int32 = _teachPoints.FindIndex(Function(_tp As TeachPoint) _tp.nr = tp.nr)
        If i >= 0 Then
            If MessageBox.Show($"Teachpunkt {tp.nr} ({_teachPoints(i).name}) existiert bereits. Ersetzen?", "Teachpunkt ersetzen?", MessageBoxButtons.YesNo) _
             = Windows.Forms.DialogResult.No Then
                Return False
            End If
            _teachPoints(i) = tp
        Else
            _teachPoints.Add(tp)
            'Liste Sortieren
            _teachPoints.Sort()
        End If
        _printTeachpointToListBox()
        _listBox.SelectedIndex = _listBox.Items.Count - 1
        RaiseEvent Log($"[ACL] Teachpunkt {tp.nr} hinzugefügt!", Logger.LogLevel.INFO)
        Return True
    End Function
    Private Sub _printTeachpointToListBox()
        If _listBox IsNot Nothing Then
            _listBox.Items.Clear()
            For i = 0 To _teachPoints.Count - 1
                _listBox.Items.Add(_teachPoints(i).ToString)
            Next
        End If
    End Sub
#End Region

#Region "ACL-Programm"
    Private Sub _eSyntaxError(line As Integer, msg As String)
        _programSyntaxOkay = False
        RaiseEvent Log($"[ACL-PARSE] Zeile {line.ToString(),4:N}: {msg}", Logger.LogLevel.ERR)
    End Sub
    Private Sub _eCompileError(line As Integer, msg As String)
        _programCompiled = False
        RaiseEvent Log($"[ACL-COMPILE] Zeile {line.ToString(),4:N}: {msg}", Logger.LogLevel.ERR)
    End Sub
#End Region


#Region "ALT"
    ' -----------------------------------------------------------------------------
    ' Definitions
    ' -----------------------------------------------------------------------------
    '    Friend progClipboard As New ProgramEntry
    '    Friend teachPointClipboard As New TeachPoint
    '    Private _programRunning, _loopSeq As Boolean
    '    Private _progIndex As Int32


    '    Friend Event ProgChanged(ByVal e As ProgChangedEventArgs)
    '    ' -----------------------------------------------------------------------------
    '    ' Public
    '    ' -----------------------------------------------------------------------------

    '    'PROGRAMM
    '    Friend Sub addProgItem(item As ProgramEntry, index As Int32)
    '        _progList.Insert(index + 1, item)
    '        RaiseEvent ProgChanged(New ProgChangedEventArgs(False, True, False, index + 1))
    '    End Sub

    '    Friend Sub delProgItem(index As Int32)
    '        If index >= 0 And index < _progList.Count Then
    '            _progList.RemoveAt(index)
    '            RaiseEvent ProgChanged(New ProgChangedEventArgs(False, True, False, _progList.Count - 1))
    '        End If
    '    End Sub

    '    Friend Sub replaceProgItem(item As ProgramEntry, index As Int32)
    '        If index >= 0 And index < _progList.Count Then
    '            _progList(index) = item
    '            RaiseEvent ProgChanged(New ProgChangedEventArgs(False, True, False, index))
    '        End If
    '    End Sub

    '    Friend Function copyProgItem(index As Int32) As Boolean
    '        If index >= 0 And index < _progList.Count Then
    '            progClipboard = _progList(index)
    '            Return True
    '        End If
    '        Return False
    '    End Function

    '    Friend Sub pasteProgItem(index As Int32)
    '        addProgItem(progClipboard, index)
    '    End Sub
    '    Friend Function isProgRunning() As Boolean
    '        Return _programRunning
    '    End Function

    '    Friend Sub executeProgramm(loopSeq As Boolean)
    '        If _progList.Count > 0 Then
    '            _programRunning = True
    '            Me._loopSeq = loopSeq
    '            _progIndex = 0
    '            If Not executeProgItem(0) Then
    '                _programRunning = False
    '            End If
    '            RaiseEvent ProgChanged(New ProgChangedEventArgs(True, True, False, _progIndex))
    '        End If
    '    End Sub

    '    Friend Function executeProgItem(index As Int32) As Boolean
    '        If _progList.Count > index And index >= 0 Then
    '            If _progList(index).func = "pos" Then
    '                Dim tpI As Int32 = _teachPoints.FindIndex(Function(_tp As TeachPoint) _tp.nr = _progList(index).tpnr)
    '                If tpI = -1 Then
    '                    RaiseEvent Log($"Teachpunkt {_progList(index).tpnr} existierte nicht!", Logger.LogLevel.ERR)
    '                    Return False
    '                End If
    '                'setSpeedAndAcc(progList(index).speed, progList(index).acc)
    '                'Return doJointMov(progList(index).sync, True, teachPoints(tpI).jointAngles(0), True, teachPoints(tpI).jointAngles(1), True,
    '                'teachPoints(tpI).jointAngles(2), True, teachPoints(tpI).jointAngles(3), True, teachPoints(tpI).jointAngles(4),
    '                '     True, teachPoints(tpI).jointAngles(5))
    '            ElseIf _progList(index).func = "srv" Then
    '                'Return movServo(progList(index).servoNum, CInt(Math.Round(progList(index).servoVal, 0)))
    '            ElseIf _progList(index).func = "wai" Then
    '                'If com.sendWAI(progList(index).waitTimeMS) Then
    '                ''Log
    '                'RaiseEvent Log($"Waiting for {progList(index).waitTimeMS} milliseconds...", Logger.LogLevel.INFO)
    '                'Return True
    '                'End If
    '            End If
    '        End If
    '        Return False
    '    End Function

    '    Friend Sub stopProgram()
    '        If _programRunning Then
    '            _programRunning = False
    '        End If
    '    End Sub

    '    Friend Function saveProgram() As Boolean
    '        Dim saveFileDialog As New SaveFileDialog With {
    '           .Filter = "Positions-Dateien (*.pos)|*.pos"
    '       }
    '        If saveFileDialog.ShowDialog() = DialogResult.OK Then
    '            Dim objStreamWriter As StreamWriter
    '            objStreamWriter = New StreamWriter(saveFileDialog.FileName)
    '            'TeachPunkte
    '            For Each x As TeachPoint In _teachPoints
    '                objStreamWriter.WriteLine(x.name & ";tp;" & x.nr & ";" & x.jointAngles.Items(0) & ";" & x.jointAngles.Items(1) & ";" _
    '                         & x.jointAngles.Items(2) & ";" & x.jointAngles.Items(3) & ";" _
    '                         & x.jointAngles.Items(4) & ";" & x.jointAngles.Items(5))
    '            Next
    '            'Programm
    '            For Each x As ProgramEntry In _progList
    '                If x.func = "pos" Then
    '                    objStreamWriter.WriteLine(x.comment & ";pos;" & x.tpnr & ";" _
    '                         & x.speed & ";" & x.acc & ";" & x.sync)
    '                ElseIf x.func = "srv" Then
    '                    objStreamWriter.WriteLine(x.comment & ";srv;" & x.servoNum & ";" & x.servoVal)
    '                ElseIf x.func = "wai" Then
    '                    objStreamWriter.WriteLine(x.comment & ";wai;" & x.waitTimeMS)
    '                End If
    '            Next
    '            objStreamWriter.Close()
    '            Return True
    '        Else
    '            Return False
    '        End If
    '    End Function

    '    Friend Function loadProgram() As Boolean
    '        Dim tmpErg As Boolean = True
    '        Dim openFileDialog As New OpenFileDialog With {
    '           .Filter = "Positions-Dateien (*.pos)|*.pos"
    '       }
    '        If openFileDialog.ShowDialog() = DialogResult.OK Then
    '            Dim objStreamReader As StreamReader
    '            objStreamReader = New StreamReader(openFileDialog.FileName)
    '            Try
    '                'Jede Zeile der pos Datei einlesen
    '                Dim strLine As String
    '                Dim tmpSeqList As New List(Of ProgramEntry)
    '                Dim tmpTpList As New List(Of TeachPoint)
    '                Do
    '                    strLine = objStreamReader.ReadLine
    '                    If Not strLine Is Nothing Then
    '                        Dim tmpSplit As String() = strLine.Split(";"c)
    '                        Dim func As String = tmpSplit(1)

    '                        If func = "tp" Then
    '                            'TeachPunkte
    '                            Dim item As New TeachPoint
    '                            item.name = tmpSplit(0)
    '                            item.nr = CInt(tmpSplit(2))
    '                            item.jointAngles.SetByIndex(0, CDec(tmpSplit(3)))
    '                            item.jointAngles.SetByIndex(1, CDec(tmpSplit(4)))
    '                            item.jointAngles.SetByIndex(2, CDec(tmpSplit(5)))
    '                            item.jointAngles.SetByIndex(3, CDec(tmpSplit(6)))
    '                            item.jointAngles.SetByIndex(4, CDec(tmpSplit(7)))
    '                            item.jointAngles.SetByIndex(5, CDec(tmpSplit(8)))
    '                            tmpTpList.Add(item)
    '                        Else
    '                            'Programm
    '                            Dim item As New ProgramEntry
    '                            item.comment = tmpSplit(0)
    '                            If func = "pos" Then
    '                                item.func = "pos"
    '                                item.tpnr = CInt(tmpSplit(2))
    '                                item.speed = CDec(tmpSplit(3))
    '                                item.acc = CDec(tmpSplit(4))
    '                                item.sync = CBool(tmpSplit(5))
    '                            ElseIf func = "srv" Then
    '                                item.func = "srv"
    '                                item.servoNum = CInt(tmpSplit(2))
    '                                item.servoVal = CInt(tmpSplit(3))
    '                            ElseIf func = "wai" Then
    '                                item.func = "wai"
    '                                item.waitTimeMS = CInt(tmpSplit(2))
    '                            End If
    '                            tmpSeqList.Add(item)
    '                        End If
    '                    End If
    '                Loop Until strLine Is Nothing
    '                'Listen kopieren
    '                _teachPoints.Clear()
    '                For Each x As TeachPoint In tmpTpList
    '                    _teachPoints.Add(x)
    '                Next
    '                _progList.Clear()
    '                For Each x As ProgramEntry In tmpSeqList
    '                    _progList.Add(x)
    '                Next
    '                'und Changed Event auslösen
    '                RaiseEvent ProgChanged(New ProgChangedEventArgs(False, True, True, _progList.Count - 1))
    '            Catch ex As Exception
    '                _teachPoints.Clear()
    '                _progList.Clear()
    '                tmpErg = False
    '            Finally
    '                'StreamReader schließen
    '                objStreamReader.Close()
    '            End Try
    '        Else
    '            tmpErg = False
    '        End If
    '        Return tmpErg
    '    End Function

    '    Friend Sub fastStop()
    '        stopProgram() 'Sequenz stoppen
    '        'com.sendStop()
    '        'Log
    '        RaiseEvent Log("[Robo Control] Programm gestoppt", Logger.LogLevel.INFO)
    '    End Sub

    '    ' -----------------------------------------------------------------------------
    '    ' Private
    '    ' -----------------------------------------------------------------------------
    '    'TEACHPUNKTE
    '    Private Sub _swapTPNr(num1 As Int32, num2 As Int32)
    '        For Each x As ProgramEntry In _progList
    '            If x.tpnr = num1 Then
    '                x.tpnr = num2
    '            ElseIf x.tpnr = num2 Then
    '                x.tpnr = num1
    '            End If
    '        Next
    '    End Sub
    '    Private Sub _changeTPNr(numOld As Int32, numNew As Int32)
    '        For Each x As ProgramEntry In _progList
    '            If x.tpnr = numOld Then
    '                x.tpnr = numNew
    '            End If
    '        Next
    '    End Sub

    '    'EVENTS
    '    'Private Sub _eFINReceived() Handles com.FINReceived
    '    'If _programRunning Then
    '    '       _progIndex += 1
    '    'If _loopSeq And _progIndex >= progList.Count Then
    '    '           _progIndex = 0
    '    'End If
    '    'If _progIndex < progList.Count Then
    '    'If Not executeProgItem(_progIndex) Then
    '    '                _programRunning = False
    '    'End If
    '    'RaiseEvent ProgChanged(New ProgChangedEventArgs(True, True, False, _progIndex))
    '    'Else
    '    '           _programRunning = False
    '    'RaiseEvent ProgChanged(New ProgChangedEventArgs(False))
    '    'End If
    '    'End If
    '    ''Log
    '    'RaiseEvent Log("[Robo Control] Fertig...", Logger.LogLevel.INFO)
    '    'End Sub
#End Region

End Class

#Region "ALT"


'Friend Class ProgChangedEventArgs : Inherits EventArgs
'    Private _actProgIndex As Int32
'    Private _actTpIndex As Int32
'    Private _running As Boolean = False
'    Private _prog As Boolean = False
'    Private _tp As Boolean

'    Friend Sub New(running As Boolean, prog As Boolean, tp As Boolean, index As Int32)
'        _running = running
'        _prog = prog
'        _tp = tp
'        If prog Then
'            _actProgIndex = index
'            _actTpIndex = -1
'        ElseIf tp Then
'            _actProgIndex = -1
'            _actTpIndex = index
'        Else
'            _actProgIndex = -1
'            _actTpIndex = -1
'        End If
'    End Sub
'End Class
#End Region

' ACL Parser ErrorListener
Public Class MyErrorListener
    Inherits BaseErrorListener
    Friend Event SyntaxErrorEvent(ByVal line As Integer, msg As String)
    Public Overrides Sub SyntaxError(<NotNull> recognizer As IRecognizer, <Nullable> offendingSymbol As IToken, line As Integer, charPositionInLine As Integer, <NotNull> msg As String, <Nullable> e As RecognitionException)
        RaiseEvent SyntaxErrorEvent(line, msg)
        MyBase.SyntaxError(recognizer, offendingSymbol, line, charPositionInLine, msg, e)
    End Sub
End Class

' ACL Parser Listener
Public Class ACLListener
    Inherits ACLParserBaseListener
    Private _maxAcc, _maxSpeed, _acc, _speed As Double
    Private _tp As List(Of TeachPoint)
    Private _progList As List(Of ProgramEntry)

    Friend Event CompileErrorEvent(ByVal line As Integer, ByVal msg As String)
    Friend Sub New(ByRef tp As List(Of TeachPoint), ByRef progList As List(Of ProgramEntry), ByVal maxAcc As Double, ByVal maxSpeed As Double)
        _maxAcc = maxAcc
        _maxSpeed = maxSpeed
        _acc = maxAcc
        _speed = maxSpeed
        _tp = tp
        _progList = progList
    End Sub

    Public Overrides Sub EnterMove(<NotNull> context As ACLParser.MoveContext)
        Dim lineNr As Integer = context.MOVE.Symbol.Line
        Dim tpNr As Integer = CInt(context.INTEGER.GetText)

        ' Teachpunkt suchen
        Dim found As Boolean = False
        For Each tp As TeachPoint In _tp
            If tp.nr = tpNr Then
                ' Move hinzufügen
                Dim progEntry As New ProgramEntry
                progEntry.lineNr = lineNr
                progEntry.sync = True
                progEntry.acc = _acc
                progEntry.speed = _speed
                If tp.cart Then
                    progEntry.func = progFunc.cartMove
                    progEntry.cartCoords = tp.tcpCoords
                Else
                    progEntry.func = progFunc.jointMove
                    progEntry.jointAngles = tp.jointAngles
                End If
                _progList.Add(progEntry)
                found = True
                Exit For
            End If
        Next

        If Not found Then
            RaiseEvent CompileErrorEvent(lineNr, $"Teachpunkt {tpNr} nicht gefunden")
        End If

        MyBase.EnterMove(context)
    End Sub
    Public Overrides Sub EnterSpeed(<NotNull> context As ACLParser.SpeedContext)
        Dim lineNr As Integer = context.SPEED.Symbol.Line
        Dim speed As Integer = CInt(context.INTEGER.GetText)

        ' Speed prüfen
        If speed > 100 Or speed < 1 Then
            RaiseEvent CompileErrorEvent(lineNr, $"SPEED muss zwischen 1 und 100 liegen")
        Else
            ' Speed
            _speed = (speed / 100) * _maxSpeed
        End If

        MyBase.EnterSpeed(context)
    End Sub
End Class