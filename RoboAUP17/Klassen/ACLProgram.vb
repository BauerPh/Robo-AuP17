Imports System.IO
Imports Antlr4.Runtime
Imports Antlr4.Runtime.Misc
Imports ACLLexerParser
Imports System.Threading
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
    Private _progThread As Thread
    Private _teachPoints As New List(Of TeachPoint)
    Private _listBox As ListBox
    Private _progList As New List(Of ProgramEntry)
    Private _programSyntaxOkay As Boolean = False
    Private _programCompiled As Boolean = False
    Private _stopProgram As Boolean = False

    Friend Event Log(ByVal LogMsg As String, ByVal LogLvl As Logger.LogLevel)
    Friend Event DoJointMove(ByVal jointAngles As JointAngles, acc As Double, speed As Double)
    Friend Event DoCartMove(ByVal cartCoords As CartCoords, acc As Double, speed As Double)
    Friend Event DoServoMove(ByVal servoNr As Int32, prc As Double)
    Friend Event ProgramFinished()

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

#End Region

#Region "ACL-Programm"
    Friend Function CompileProgram(input As String, acc As Double, speed As Double) As Boolean
        Return _compileProgram(input, acc, speed)
    End Function
    Friend Sub RunProgram(input As String, acc As Double, speed As Double)
        If _compileProgram(input, acc, speed) Then
            ' Los gehts!
            _progThread = New Thread(AddressOf _runProgram)
            _progThread.IsBackground = True
            _progThread.Start()
            ProgramRunning = True
            RaiseEvent Log("[ACL] Programm gestartet", Logger.LogLevel.INFO)
        End If
    End Sub
    Friend Sub StopProgram()
        If Not _stopProgram And ProgramRunning Then
            _stopProgram = True
            RaiseEvent Log("[ACL] Programm wird beendet...", Logger.LogLevel.INFO)
        End If
    End Sub
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
    Private Function _compileProgram(input As String, acc As Double, speed As Double) As Boolean
        RaiseEvent Log("[ACL] erstelle Programm...", Logger.LogLevel.INFO)
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

            If _programCompiled Then
                RaiseEvent Log("[ACL] erfolgreich kompiliert", Logger.LogLevel.INFO)
            Else
                RaiseEvent Log("[ACL] Kompilieren fehlgeschlagen", Logger.LogLevel.ERR)
            End If
        Else
            RaiseEvent Log("[ACL] Syntaxfehler", Logger.LogLevel.ERR)
            _programCompiled = False
        End If

        ' Error Handler vom ErrorListener wieder entfernen
        RemoveHandler aclErrorListener.SyntaxErrorEvent, AddressOf _eSyntaxError

        Return _programCompiled
    End Function

    Private Sub _runProgram()
        Dim vke As Boolean = False

        For i = 0 To _progList.Count - 1
            If _stopProgram Then
                _stopProgram = False
                Exit For
            End If
            Select Case _progList(i).func
                Case progFunc.condition

                Case progFunc.cjump

                Case progFunc.jump

                Case progFunc.servoMove

                Case progFunc.jointMove
                    RaiseEvent DoJointMove(_progList(i).jointAngles, _progList(i).speed, _progList(i).acc)
                Case progFunc.cartMove
                    RaiseEvent DoCartMove(_progList(i).cartCoords, _progList(i).speed, _progList(i).acc)
                Case progFunc.delay

            End Select

            ' Warten bis Bewegung fertig ist
            While RobotMoving
                Thread.Sleep(100)
            End While
        Next

        ' Programm fertig
        ProgramRunning = False
        RaiseEvent Log("[ACL] Programm beendet", Logger.LogLevel.INFO)
        RaiseEvent ProgramFinished()
        _progThread.Abort()
    End Sub

    Private Sub _eSyntaxError(line As Integer, msg As String)
        _programSyntaxOkay = False
        RaiseEvent Log($"[ACL-PARSE] Zeile {line.ToString(),4:N}: {msg}", Logger.LogLevel.ERR)
    End Sub
    Private Sub _eCompileError(line As Integer, msg As String)
        _programCompiled = False
        RaiseEvent Log($"[ACL-COMPILE] Zeile {line.ToString(),4:N}: {msg}", Logger.LogLevel.ERR)
    End Sub
#End Region


End Class

' ACL Parser ErrorListener
Public Class MyErrorListener
    Inherits BaseErrorListener
    Friend Event SyntaxErrorEvent(ByVal line As Integer, msg As String)
    Public Overrides Sub SyntaxError(<NotNull> recognizer As IRecognizer, <Nullable> offendingSymbol As IToken, line As Integer, charPositionInLine As Integer, <NotNull> msg As String, <Nullable> e As RecognitionException)
        RaiseEvent SyntaxErrorEvent(line, msg)
        MyBase.SyntaxError(recognizer, offendingSymbol, line, charPositionInLine, msg, e)
    End Sub
End Class

' ACL Parser Listener ( ### COMPILER ###)
Public Class ACLListener
    Inherits ACLParserBaseListener
    Private _maxAcc, _maxSpeed, _acc, _speed As Double
    Private _ifStack As New Stack(Of Integer)
    Private _labels As New Dictionary(Of String, Integer)
    Private _gotos As New Dictionary(Of String, List(Of Integer))
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
    Public Overrides Sub EnterAcc(<NotNull> context As ACLParser.AccContext)
        Dim lineNr As Integer = context.ACC.Symbol.Line
        Dim acc As Integer = CInt(context.INTEGER.GetText)

        ' Acc prüfen
        If acc > 100 Or acc < 1 Then
            RaiseEvent CompileErrorEvent(lineNr, $"ACC muss zwischen 1 und 100 liegen")
        Else
            ' Speed
            _acc = (acc / 100) * _maxAcc
        End If

        MyBase.EnterAcc(context)
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
    Public Overrides Sub EnterIf(<NotNull> context As ACLParser.IfContext)
        Dim thisIndex As Int32 = _progList.Count
        ' Bedingter Sprung hinzufügen (Bedingung wird bei "EnterCondition" hinzugefügt)
        Dim progEntry As New ProgramEntry
        progEntry.func = progFunc.cjump
        progEntry.lineNr = context.IF.Symbol.Line
        progEntry.VKEFirst = True
        progEntry.jumpTrueTarget = thisIndex + 1 ' Auf nächsten Eintrag, wenn dieser hier hinzugefügt wurde
        progEntry.jumpFalseTarget = -1 ' Wird bei ANDIF, ORIF, ELSE oder ENDIF gesetzt!
        _progList.Add(progEntry)

        ' Index von diesem IF auf den Stack legen
        _ifStack.Push(thisIndex)

        MyBase.EnterIf(context)
    End Sub
    Public Overrides Sub EnterCondition(<NotNull> context As ACLParser.ConditionContext)
        ' Letztes Element holen
        Dim thisIndex As Int32 = _progList.Count - 1
        Dim progEntry As ProgramEntry = _progList(thisIndex)

        ' Prüfen ob Variable oder Wert
        Dim val1 As String = context.GetChild(0).GetText()
        Dim oper As String = context.GetChild(1).GetText()
        Dim val2 As String = context.GetChild(2).GetText()
        If IsNumeric(val1) Then
            progEntry.val1 = CInt(val1)
        Else
            ' Check Variable...
            progEntry.var1 = val1
        End If
        If IsNumeric(val2) Then
            progEntry.val2 = CInt(val2)
        Else
            ' Check Variable...
            progEntry.var2 = val2
        End If

        ' Operator
        Select Case oper
            Case ">"
                progEntry.compareOperator = progCompOperator.greater
            Case "<"
                progEntry.compareOperator = progCompOperator.less
            Case ">="
                progEntry.compareOperator = progCompOperator.greaterOrEqual
            Case "<="
                progEntry.compareOperator = progCompOperator.lessOrEqual
            Case "="
                progEntry.compareOperator = progCompOperator.equal
            Case "<>"
                progEntry.compareOperator = progCompOperator.notEqual
        End Select

        ' Zurückspeichern
        _progList(thisIndex) = progEntry

        MyBase.EnterCondition(context)
    End Sub
    Public Overrides Sub EnterAnd_or_if(<NotNull> context As ACLParser.And_or_ifContext)
        ' Bedingter Sprung hinzufügen (Bedingung wird bei "EnterCondition" hinzugefügt)
        Dim thisIndex As Int32 = _progList.Count
        Dim progEntry As New ProgramEntry
        progEntry.func = progFunc.cjump
        If context.ANDIF IsNot Nothing Then
            progEntry.lineNr = context.ANDIF.Symbol.Line
            progEntry.booleanOperator = progBoolOperator.and
        Else
            progEntry.lineNr = context.ORIF.Symbol.Line
            progEntry.booleanOperator = progBoolOperator.or
        End If
        progEntry.VKEFirst = False
        progEntry.jumpTrueTarget = thisIndex + 1 ' Auf nächsten Eintrag, wenn dieser hier hinzugefügt wurde
        progEntry.jumpFalseTarget = -1 ' Wird bei ANDIF, ORIF, ELSE oder ENDIF gesetzt!
        _progList.Add(progEntry)

        ' Eintrag vom IF vom Stack holen und durch Condition ersetzen
        Dim progListIfEntryNum = _ifStack.Pop
        progEntry = _progList(progListIfEntryNum)
        progEntry.func = progFunc.condition
        _progList(progListIfEntryNum) = progEntry

        _ifStack.Push(thisIndex) ' Index von diesem IF auf den Stack legen

        MyBase.EnterAnd_or_if(context)
    End Sub
    Public Overrides Sub EnterElse(<NotNull> context As ACLParser.ElseContext)
        Dim thisIndex As Int32 = _progList.Count

        ' Sprung hinzufügen
        Dim progEntry As New ProgramEntry
        progEntry.func = progFunc.jump
        progEntry.lineNr = context.ELSE.Symbol.Line
        progEntry.jumpTarget = -1 ' Wird bei ENDIF gesetzt
        _progList.Add(progEntry)

        ' Eintrag vom IF vom Stack holen und bearbeiten
        Dim progListIfEntryNum = _ifStack.Pop
        progEntry = _progList(progListIfEntryNum)
        progEntry.jumpFalseTarget = thisIndex + 1
        _progList(progListIfEntryNum) = progEntry

        ' Index von diesem ELSE auf den Stack legen
        _ifStack.Push(thisIndex)

        MyBase.EnterElse(context)
    End Sub
    Public Overrides Sub ExitIf(<NotNull> context As ACLParser.IfContext)
        Dim thisIndex As Int32 = _progList.Count

        ' NOOP hinzufügen
        Dim progEntry As New ProgramEntry
        progEntry.func = progFunc.noop
        progEntry.lineNr = context.ENDIF.Symbol.Line
        _progList.Add(progEntry)

        ' Eintrag vom IF vom Stack holen und bearbeiten
        Dim progListIfOrElseEntryNum = _ifStack.Pop
        progEntry = _progList(progListIfOrElseEntryNum)
        If progEntry.func = progFunc.cjump Then
            progEntry.jumpFalseTarget = thisIndex
        Else
            progEntry.jumpTarget = thisIndex
        End If
        _progList(progListIfOrElseEntryNum) = progEntry

        MyBase.ExitIf(context)
    End Sub
    Public Overrides Sub EnterLabel(<NotNull> context As ACLParser.LabelContext)
        Dim lineNr As Integer = context.LABEL.Symbol.Line
        Dim thisIndex As Int32 = _progList.Count
        ' Prüfen ob das Label schon existiert, wenn nicht => anlegen
        Dim labelText As String = context.IDENTIFIER.GetText
        If _labels.ContainsKey(labelText) Then
            RaiseEvent CompileErrorEvent(lineNr, $"Label ""{labelText}"" wurde nochmals definiert")
        Else
            ' NOOP hinzufügen
            Dim progEntry As New ProgramEntry
            progEntry.func = progFunc.noop
            progEntry.lineNr = lineNr
            _progList.Add(progEntry)

            ' Label anlgen und Prüfen ob es GOTOs gibt
            _labels.Add(labelText, thisIndex)
            If _gotos.ContainsKey(labelText) Then
                For Each item As Int32 In _gotos(labelText)
                    progEntry = _progList(item)
                    progEntry.jumpTarget = thisIndex
                    _progList(item) = progEntry
                Next
                _gotos.Remove(labelText)
            End If
        End If

        MyBase.EnterLabel(context)
    End Sub
    Public Overrides Sub EnterGoto(<NotNull> context As ACLParser.GotoContext)
        Dim lineNr As Integer = context.GOTO.Symbol.Line
        Dim thisIndex As Int32 = _progList.Count

        ' Sprung hinzufügen
        Dim progEntry As New ProgramEntry
        progEntry.func = progFunc.jump
        progEntry.lineNr = lineNr
        progEntry.jumpTarget = -1

        ' Prüfen ob es schon ein Label gibt
        Dim labelText As String = context.IDENTIFIER.GetText
        If _labels.ContainsKey(labelText) Then
            progEntry.jumpTarget = _labels(labelText)
        Else
            ' Goto zwischenspeichern
            If Not _gotos.ContainsKey(labelText) Then
                _gotos.Add(labelText, New List(Of Integer))
            End If
            _gotos(labelText).Add(thisIndex)
        End If

        _progList.Add(progEntry)

        MyBase.EnterGoto(context)
    End Sub

    Public Overrides Sub ExitRoot(<NotNull> context As ACLParser.RootContext)
        If _gotos.Count > 0 Then
            For Each item As KeyValuePair(Of String, List(Of Integer)) In _gotos
                RaiseEvent CompileErrorEvent(_progList(item.Value(0)).lineNr, $"Label ""{item.Key}"" wurde nicht definiert")
            Next
        End If

        MyBase.ExitRoot(context)
    End Sub
End Class