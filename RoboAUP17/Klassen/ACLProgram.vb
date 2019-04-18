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
    ' Speichern und Laden (wip...) / Variablen fehlen noch!
    ' Variablen und TCP Variablen

    ' -----------------------------------------------------------------------------
    ' Definitions
    ' -----------------------------------------------------------------------------
    Private _settings As Settings

    Private _teachPoints As New List(Of TeachPoint)
    Private _listBox As ListBox
    Friend ReadOnly Property TcpVariables As New TCPVariables

    Private _progThread As Thread
    Private _compiledTeachPoints As New List(Of TeachPoint)
    Private _progList As New List(Of ProgramEntry)
    Private _programSyntaxOkay As Boolean = False
    Private _programCompiled As Boolean = False
    Private _stopProgram As Boolean = False
    Private _forceStopProgram As Boolean = False

    Private _filename As String = Nothing
    Private _unsavedChanges As Boolean = False
    Friend Property UnsavedChanges As Boolean
        Get
            Return _unsavedChanges
        End Get
        Set(value As Boolean)
            _unsavedChanges = value
        End Set
    End Property

    Friend Event Log(ByVal LogMsg As String, ByVal LogLvl As Logger.LogLevel)
    Friend Event DoJointMove(ByVal jointAngles As JointAngles, acc As Double, speed As Double)
    Friend Event DoCartMove(ByVal cartCoords As CartCoords, acc As Double, speed As Double)
    Friend Event DoServoMove(ByVal srvNr As Int32, prc As Double)
    Friend Event DoDelay(ByVal delay As Int32)
    Friend Event ProgramStarted()
    Friend Event ProgramFinished()
    Friend Event ProgramLineChanged(line As Int32)
    Friend Event ErrorLine(line As Int32)

    ' -----------------------------------------------------------------------------
    ' Public
    ' -----------------------------------------------------------------------------
#Region "Allgemein"
    Friend Sub SetSettingsObject(ByRef settings As Settings)
        _settings = settings
    End Sub

    Friend Sub New()

    End Sub

#End Region

#Region "Teachpunkte"
    Friend Sub ClearTeachpoints()
        _teachPoints.Clear()
        _printTeachpointToListBox()
    End Sub
    Friend Function GetTeachpointByIndex(index As Int32) As TeachPoint
        If _teachPoints.Count > index And index >= 0 Then
            Return _teachPoints(index)
        End If
        Return Nothing
    End Function
    Friend Function AddTeachPoint(name As String, cartCoords As CartCoords, tpNr As Int32) As Boolean
        Dim tp As TeachPoint
        tp.cart = True
        tp.cartCoords = cartCoords.Round(2)
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
                _unsavedChanges = True
            End If
        End If
    End Sub

    Friend Sub SetListBox(ByRef lb As ListBox)
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
            _unsavedChanges = True
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
            RaiseEvent ProgramStarted()
            RaiseEvent Log("[ACL] Programm gestartet", Logger.LogLevel.INFO)
        End If
    End Sub
    Friend Sub StopProgram()
        If Not _stopProgram And ProgramRunning Then
            _stopProgram = True
            RaiseEvent Log("[ACL] Programm wird beendet...", Logger.LogLevel.INFO)
        End If
    End Sub

    'Bitte nur verwenden, wenn zum Beispiel die Verbindung zum Robo unterbrochen wird!
    Friend Sub ForceStopProgram()
        If Not _forceStopProgram And ProgramRunning Then
            _forceStopProgram = True
            RaiseEvent Log("[ACL] Programm wird abgebrochen...", Logger.LogLevel.WARN)
        End If
    End Sub
#End Region

#Region "Speichern / Laden"
    Public Function Save(prog As String, Optional saveAs As Boolean = False) As Boolean
        Dim tmpFilename As String
        If _filename Is Nothing Or saveAs Then
            Dim saveFileDialog As New SaveFileDialog With {
                .Filter = "RoboAUP17-Dateien (*.aup17)|*.aup17"
            }
            If saveFileDialog.ShowDialog() = DialogResult.OK Then
                tmpFilename = saveFileDialog.FileName
            Else
                Return False
            End If
        Else
            tmpFilename = _filename
        End If

        Dim objStreamWriter As StreamWriter
        objStreamWriter = New StreamWriter(tmpFilename)
        'Teachpunkte
        For Each tp As TeachPoint In _teachPoints
            objStreamWriter.Write($"<tp>;{tp.nr};{tp.name};{tp.cart}")
            If tp.cart Then
                For i = 0 To 5
                    objStreamWriter.Write($";{tp.cartCoords.Items(i)}")
                Next
            Else
                For i = 0 To 5
                    objStreamWriter.Write($";{tp.jointAngles.Items(i)}")
                Next
            End If
            objStreamWriter.WriteLine()
        Next
        'Programm
        objStreamWriter.WriteLine("<program>")
        objStreamWriter.Write(prog)
        objStreamWriter.Close()
        _filename = tmpFilename
        _unsavedChanges = False
        Return True
    End Function
    Public Function Load(ByRef prog As String) As Boolean
        Dim erg As Boolean = True
        Dim readProg As Boolean = False
        Dim openFileDialog As New OpenFileDialog With {
           .Filter = "RoboAUP17-Dateien (*.aup17)|*.aup17"
        }
        If openFileDialog.ShowDialog() = DialogResult.OK Then
            ' Teachpunkte leeren
            _teachPoints.Clear()

            Dim objStreamReader As StreamReader
            objStreamReader = New StreamReader(openFileDialog.FileName)
            Try
                'Jede Zeile der Datei einlesen
                Dim strLine As String
                Do
                    strLine = objStreamReader.ReadLine
                    If Not strLine Is Nothing Then
                        Dim tmpSplit As String() = strLine.Split(";"c)
                        If tmpSplit(0) = "<tp>" Then
                            'TeachPunkt
                            Dim item As New TeachPoint
                            item.nr = CInt(tmpSplit(1))
                            item.name = tmpSplit(2)
                            item.cart = CBool(tmpSplit(3))
                            If item.cart Then
                                For i = 0 To 5
                                    item.cartCoords.SetByIndex(i, CDbl(tmpSplit(4 + i)))
                                Next
                            Else
                                For i = 0 To 5
                                    item.jointAngles.SetByIndex(i, CDbl(tmpSplit(4 + i)))
                                Next
                            End If
                            _teachPoints.Add(item)
                        ElseIf tmpSplit(0) = "<program>" Then
                            ' Programm einlesen
                            prog = objStreamReader.ReadToEnd()
                        End If
                    End If
                Loop Until strLine Is Nothing

                _printTeachpointToListBox()
                _filename = openFileDialog.FileName
                _unsavedChanges = False
            Catch ex As Exception
                _teachPoints.Clear()
                prog = ""
                erg = False
            Finally
                'StreamReader schließen
                objStreamReader.Close()
            End Try
        Else
            erg = False
        End If
        Return erg
    End Function
#End Region

    ' -----------------------------------------------------------------------------
    ' Private
    ' -----------------------------------------------------------------------------
#Region "Teachpunkte"
    Private Function _addTeachPoint(tp As TeachPoint) As Boolean
        Dim i As Int32 = _teachPoints.FindIndex(Function(_tp As TeachPoint) _tp.nr = tp.nr)
        If i >= 0 Then
            If MessageBox.Show($"Teachpunkt {tp.nr} ({_teachPoints(i).name}) existiert bereits. Ersetzen?", "Teachpunkt ersetzen?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) _
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
        _unsavedChanges = True
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
        Dim inputStream As AntlrInputStream = New AntlrInputStream(input + vbCrLf)
        ' Erstelle Lexer
        Dim aclLexer As ACLLexer = New ACLLexer(inputStream)
        ' Erstelle Token Stream
        Dim comTokenStream As CommonTokenStream = New CommonTokenStream(aclLexer)
        ' Erstelle Parser
        Dim aclParser As ACLParser = New ACLParser(comTokenStream)
        aclParser.BuildParseTree = True
        ' Eigenen Error Listener hinzufügen, um Syntaxfehler zu erkennen
        aclParser.RemoveErrorListeners()
        Dim aclErrorListener As New _myErrorListener()
        AddHandler aclErrorListener.SyntaxErrorEvent, AddressOf _eSyntaxError
        aclParser.AddErrorListener(aclErrorListener)
        ' Parser starten
        Dim rootContext As ACLParser.RootContext = aclParser.root()

        'Debug print Tokens
        For i = 0 To comTokenStream.Size - 1
            RaiseEvent Log($"[ACL-LEXER] {If(comTokenStream.Get(i).Text.StartsWith(vbCrLf), "", comTokenStream.Get(i).Text)} => {aclLexer.Vocabulary.GetSymbolicName(comTokenStream.Get(i).Type)}", Logger.LogLevel.DEBUG)
        Next

        ' ACL Programm kompilieren, wenn Syntax okay
        If _programSyntaxOkay Then
            ' Teachpoints kopieren (deep copy!)
            _compiledTeachPoints = _teachPoints.Select(Function(item) CType(item.Clone(), TeachPoint)).ToList()
            Dim aclListener As New _ACLListener(_compiledTeachPoints, _progList, acc, speed) ' Eigentlicher Compiler
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
        _stopProgram = False
        _forceStopProgram = False

        Dim vke As Boolean = False
        Dim calcBuffer As Int32
        Dim rtVariables As New Dictionary(Of String, Variable)
        Dim rtTeachpoints As New Dictionary(Of String, TeachPoint)

        Dim i As Integer = 0
        While i < _progList.Count
            Dim cmd As ProgramEntry = _progList(i)
            If _stopProgram Or _forceStopProgram Then
                _stopProgram = False
                _forceStopProgram = False
                Exit While
            End If

            ' Aktuelle Line ausgeben
            RaiseEvent ProgramLineChanged(cmd.lineNr)

            Select Case cmd.func
                Case progFunc.noop
                    ' -------------------------------------
                    ' NOOP
                    ' -------------------------------------
                    i += 1
                Case progFunc.condition
                    ' -------------------------------------
                    ' CONDITION
                    ' -------------------------------------
                    vke = _checkCondition(cmd, rtVariables, vke)
                    i += 1
                Case progFunc.calculation
                    ' -------------------------------------
                    ' CALCULATION
                    ' -------------------------------------
                    'Get val1
                    Dim val1 As Int32
                    If cmd.var1 IsNot Nothing Then
                        val1 = rtVariables(cmd.var1).intVal
                    Else
                        val1 = cmd.val1
                    End If
                    'Get val2
                    Dim val2 As Int32
                    If cmd.var2 IsNot Nothing Then
                        val2 = rtVariables(cmd.var2).intVal
                    Else
                        val2 = cmd.val2
                    End If
                    ' Berechnen
                    Select Case cmd.mathOperator
                        Case progMathOperator.plus
                            calcBuffer = val1 + val2
                        Case progMathOperator.minus
                            calcBuffer = val1 - val2
                        Case progMathOperator.mult
                            calcBuffer = val1 * val2
                        Case progMathOperator.div
                            calcBuffer = val1 \ val2
                        Case progMathOperator.exp
                            calcBuffer = CInt(val1 ^ val2)
                        Case progMathOperator.mod
                            calcBuffer = val1 Mod val2
                    End Select
                    i += 1
                Case progFunc.cjump
                    ' -------------------------------------
                    ' CONDITIONED JUMP
                    ' -------------------------------------
                    vke = _checkCondition(cmd, rtVariables, vke)
                    i = If(vke, cmd.jumpTrueTarget, cmd.jumpFalseTarget)
                Case progFunc.jump
                    ' -------------------------------------
                    ' JUMP
                    ' -------------------------------------
                    i = cmd.jumpTarget
                Case progFunc.servoMove
                    ' -------------------------------------
                    ' SERVO
                    ' -------------------------------------
                    If Not _settings.ServoParameter(cmd.servoNum - 1).Available Then
                        _runtimeError(cmd.lineNr, $"Servo {cmd.servoNum} ist nicht aktiviert")
                        Exit While 'Programm beenden
                    End If
                    RaiseEvent DoServoMove(cmd.servoNum, cmd.servoVal)
                    i += 1
                Case progFunc.move
                    ' -------------------------------------
                    ' MOVE
                    ' -------------------------------------
                    ' Teachpoint suchen und anfahren
                    Dim found As Boolean = False
                    For Each tp As TeachPoint In _compiledTeachPoints
                        If cmd.teachPoint = tp.nr Then
                            If tp.cart Then
                                RaiseEvent DoCartMove(tp.cartCoords, cmd.speed, cmd.acc)
                            Else
                                RaiseEvent DoJointMove(tp.jointAngles, cmd.speed, cmd.acc)
                            End If
                            found = True
                            Exit For
                        End If
                    Next
                    If Not found Then
                        ' Dürfte niemanls passieren!
                        StopProgram()
                    End If
                    i += 1
                Case progFunc.delay
                    ' -------------------------------------
                    ' DELAY
                    ' -------------------------------------
                    RaiseEvent DoDelay(cmd.delayTimeMS)
                    i += 1
                Case progFunc.defVar
                    ' -------------------------------------
                    ' DEFINE VARIABLE
                    ' -------------------------------------
                    If rtVariables.ContainsKey(cmd.varName) Then
                        _runtimeError(cmd.lineNr, $"Variable {cmd.varName} wurde nochmals definiert")
                        Exit While 'Programm beenden
                    End If
                    rtVariables.Add(cmd.varName, New Variable(varType.int, cmd.lineNr))
                    i += 1
                Case progFunc.delVar
                    ' -------------------------------------
                    ' DELETE VARIABLE
                    ' -------------------------------------
                    If Not rtVariables.ContainsKey(cmd.varName) Then
                        _runtimeError(cmd.lineNr, $"Variable {cmd.varName} ist an dieser Stelle nicht definiert")
                        Exit While 'Programm beenden
                    End If
                    rtVariables.Remove(cmd.varName)
                    i += 1
                Case progFunc.setVar
                    ' -------------------------------------
                    ' SET VARIABLE
                    ' -------------------------------------
                    If Not rtVariables.ContainsKey(cmd.varName) Then
                        _runtimeError(cmd.lineNr, $"Variable {cmd.varName} ist an dieser Stelle nicht definiert")
                        Exit While 'Programm beenden
                    End If
                    Dim var As Variable = rtVariables(cmd.varName)
                    If cmd.varVariable IsNot Nothing Then
                        var.intVal = rtVariables(cmd.varVariable).intVal
                    Else
                        var.intVal = cmd.varValue
                    End If
                    rtVariables(cmd.varName) = var
                    i += 1
                Case progFunc.setVarToBuffer
                    ' -------------------------------------
                    ' SET VARIABLE TO BUFFER
                    ' -------------------------------------
                    If Not rtVariables.ContainsKey(cmd.varName) Then
                        _runtimeError(cmd.lineNr, $"Variable {cmd.varName} ist an dieser Stelle nicht definiert")
                        Exit While 'Programm beenden
                    End If
                    Dim var As Variable = rtVariables(cmd.varName)
                    var.intVal = calcBuffer
                    rtVariables(cmd.varName) = var
                    i += 1
            End Select

            ' Warten bis Bewegung fertig ist oder erzwungenes Beenden
            While RobotBusy And Not _forceStopProgram
                Thread.Sleep(5)
            End While
        End While

        ' Programm fertig
        ProgramRunning = False
        RaiseEvent ProgramFinished()
        RaiseEvent Log("[ACL] Programm beendet", Logger.LogLevel.INFO)
    End Sub
    Private Function _checkCondition(cmd As ProgramEntry, ByRef rtVariables As Dictionary(Of String, Variable), vke As Boolean) As Boolean
        'Get val1
        Dim val1 As Int32
        If cmd.var1 IsNot Nothing Then
            val1 = rtVariables(cmd.var1).intVal
        Else
            val1 = cmd.val1
        End If
        'Get val2
        Dim val2 As Int32
        If cmd.var2 IsNot Nothing Then
            val2 = rtVariables(cmd.var2).intVal
        Else
            val2 = cmd.val2
        End If

        ' VKE Berechnen
        Dim tmpVKE As Boolean
        Select Case cmd.compareOperator
            Case progCompOperator.equal
                tmpVKE = (val1 = val2)
            Case progCompOperator.greater
                tmpVKE = (val1 > val2)
            Case progCompOperator.less
                tmpVKE = (val1 < val2)
            Case progCompOperator.greaterOrEqual
                tmpVKE = (val1 >= val2)
            Case progCompOperator.lessOrEqual
                tmpVKE = (val1 <= val2)
            Case progCompOperator.notEqual
                tmpVKE = (val1 <> val2)
        End Select

        ' Mit vorherigem VKE verknüpfen
        If Not cmd.VKEFirst Then
            Select Case cmd.booleanOperator
                Case progBoolOperator.and
                    tmpVKE = tmpVKE And vke
                Case progBoolOperator.or
                    tmpVKE = tmpVKE Or vke
            End Select
        End If
        Return tmpVKE
    End Function
    Private Sub _runtimeError(lineNr As Integer, msg As String)
        RaiseEvent ErrorLine(lineNr)
        RaiseEvent Log($"[ACL-RT] Zeile {lineNr.ToString,3:N}: {msg}", Logger.LogLevel.ERR)
    End Sub

    Private Sub _eSyntaxError(line As Integer, msg As String)
        If _programSyntaxOkay Then
            RaiseEvent ErrorLine(line)
        End If
        _programSyntaxOkay = False
        RaiseEvent Log($"[ACL-PARSE] Zeile {line.ToString(),3:N}: {msg}", Logger.LogLevel.ERR)
    End Sub
    Private Sub _eCompileError(line As Integer, msg As String)
        If _programSyntaxOkay And _programCompiled Then
            RaiseEvent ErrorLine(line)
        End If
        _programCompiled = False
        RaiseEvent Log($"[ACL-COMPILE] Zeile {line.ToString(),3:N}: {msg}", Logger.LogLevel.ERR)
    End Sub
#End Region

    ' ACL Parser ErrorListener
    Private Class _myErrorListener
        Inherits BaseErrorListener
        Friend Event SyntaxErrorEvent(ByVal line As Integer, msg As String)
        Public Overrides Sub SyntaxError(<NotNull> recognizer As IRecognizer, <Nullable> offendingSymbol As IToken, line As Integer, charPositionInLine As Integer, <NotNull> msg As String, <Nullable> e As RecognitionException)
            RaiseEvent SyntaxErrorEvent(line, msg)
            MyBase.SyntaxError(recognizer, offendingSymbol, line, charPositionInLine, msg, e)
        End Sub
    End Class

    ' ACL Parser Listener ( ### COMPILER ###)
    Private Class _ACLListener
        Inherits ACLParserBaseListener
        Private _maxAcc, _maxSpeed, _acc, _speed As Double
        Private _ifStack As New Stack(Of Integer)
        Private _labels As New Dictionary(Of String, Integer)
        Private _gotos As New Dictionary(Of String, List(Of Integer))
        Private _variables As New Dictionary(Of String, Variable)
        Private _tmpTp As New Dictionary(Of String, TeachPoint)
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

            ' Teachpunkt suchen und move Befehl hinzufgen
            Dim found As Boolean = False
            For Each tp As TeachPoint In _tp
                If tp.nr = tpNr Then
                    ' Move hinzufügen
                    Dim progEntry As New ProgramEntry With {
                        .func = progFunc.move,
                        .lineNr = lineNr,
                        .sync = True,
                        .acc = _acc,
                        .speed = _speed,
                        .teachPoint = tp.nr
                    }
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
        Public Overrides Sub EnterOpenclose(<NotNull> context As ACLParser.OpencloseContext)
            Dim lineNr As Integer = CType(context.GetChild(0), Tree.ITerminalNode).Symbol.Line
            Dim servoNr As Int32 = CInt(context.GetChild(1).GetText())
            If servoNr < 1 Or servoNr > 3 Then
                RaiseEvent CompileErrorEvent(lineNr, $"Servonummer muss zwischen 1 und 3 liegen")
            End If
            ' Servomove hinzufügen
            Dim thisIndex As Int32 = _progList.Count
            Dim progEntry As New ProgramEntry
            progEntry.func = progFunc.servoMove
            progEntry.lineNr = lineNr
            progEntry.servoNum = servoNr
            If CType(context.GetChild(0), Tree.ITerminalNode).Symbol.Type = ACLLexer.OPEN Then
                progEntry.servoVal = 100
            Else
                progEntry.servoVal = 0
            End If
            _progList.Add(progEntry)

            MyBase.EnterOpenclose(context)
        End Sub
        Public Overrides Sub EnterJaw(<NotNull> context As ACLParser.JawContext)
            Dim lineNr As Integer = context.JAW.Symbol.Line
            Dim servoNr As Int32 = CInt(context.GetChild(1).GetText())
            Dim servoVal As Int32 = CInt(context.GetChild(2).GetText())
            If servoNr < 1 Or servoNr > 3 Then
                RaiseEvent CompileErrorEvent(lineNr, $"Servonummer muss zwischen 1 und 3 liegen")
            End If
            If servoVal < 1 Or servoVal > 100 Then
                RaiseEvent CompileErrorEvent(lineNr, $"Servowert muss zwischen 1 und 100 liegen")
            End If
            ' Servomove hinzufügen
            Dim thisIndex As Int32 = _progList.Count
            Dim progEntry As New ProgramEntry
            progEntry.func = progFunc.servoMove
            progEntry.lineNr = lineNr
            progEntry.servoNum = servoNr
            progEntry.servoVal = servoVal
            _progList.Add(progEntry)

            MyBase.EnterJaw(context)
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
                If Not _variables.ContainsKey(val1) Then
                    RaiseEvent CompileErrorEvent(progEntry.lineNr, $"Variable ""{val1}"" wurde nicht definiert")
                Else
                    progEntry.var1 = val1
                End If
            End If
            If IsNumeric(val2) Then
                progEntry.val2 = CInt(val2)
            ElseIf context.BOOL IsNot Nothing Then
                ' Bool
                progEntry.val2 = If(context.BOOL.GetText = "FALSE", 0, 1)
            Else
                ' Variable
                If Not _variables.ContainsKey(val2) Then
                    RaiseEvent CompileErrorEvent(progEntry.lineNr, $"Variable ""{val2}"" wurde nicht definiert")
                Else
                    progEntry.var2 = val2
                End If
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
        Public Overrides Sub EnterIf(<NotNull> context As ACLParser.IfContext)
            Dim thisIndex As Int32 = _progList.Count
            ' Bedingter Sprung hinzufügen (Bedingung wird bei "EnterCondition" hinzugefügt)
            Dim progEntry As New ProgramEntry With {
                .func = progFunc.cjump,
                .lineNr = context.IF.Symbol.Line,
                .VKEFirst = True,
                .jumpTrueTarget = thisIndex + 1, ' Auf nächsten Eintrag, wenn dieser hier hinzugefügt wurde
                .jumpFalseTarget = -1 ' Wird bei ANDIF, ORIF, ELSE oder ENDIF gesetzt!
            }
            _progList.Add(progEntry)

            ' Index von diesem IF auf den Stack legen
            _ifStack.Push(thisIndex)

            MyBase.EnterIf(context)
        End Sub
        Public Overrides Sub EnterAnd_or_if(<NotNull> context As ACLParser.And_or_ifContext)
            ' Bedingter Sprung hinzufügen (Bedingung wird bei "EnterCondition" hinzugefügt)
            Dim thisIndex As Int32 = _progList.Count
            Dim progEntry As New ProgramEntry
            progEntry.func = progFunc.cjump
            progEntry.lineNr = CType(context.GetChild(0), Tree.ITerminalNode).Symbol.Line
            If CType(context.GetChild(0), Tree.ITerminalNode).Symbol.Type = ACLLexer.ANDIF Then
                progEntry.booleanOperator = progBoolOperator.and
            Else
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
        Public Overrides Sub EnterFor(<NotNull> context As ACLParser.ForContext)
            'TODO
            RaiseEvent CompileErrorEvent(context.FOR.Symbol.Line, """FOR"" noch nicht implementiert")
            MyBase.EnterFor(context)
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
            Dim progEntry As New ProgramEntry With {
                .func = progFunc.jump,
                .lineNr = lineNr,
                .jumpTarget = -1
            }

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
        Public Overrides Sub EnterDelay(<NotNull> context As ACLParser.DelayContext)
            Dim delay As Int32 = CInt(context.GetChild(1).GetText())

            ' Delay hinzufügen
            Dim progEntry As New ProgramEntry With {
                .func = progFunc.delay,
                .lineNr = context.DELAY.Symbol.Line,
                .delayTimeMS = delay * 10 ' Hundertstel werden angegeben!
            }
            _progList.Add(progEntry)

            MyBase.EnterDelay(context)
        End Sub
        Public Overrides Sub EnterWait(<NotNull> context As ACLParser.WaitContext)
            'TODO
            ' Soll nur bei TCP Variablen funktionieren: evtl neuer Befehl

            ' NOOP hinzufügen
            Dim progEntry As New ProgramEntry
            progEntry.func = progFunc.noop
            progEntry.lineNr = context.WAIT.Symbol.Line
            _progList.Add(progEntry)

            RaiseEvent CompileErrorEvent(context.WAIT.Symbol.Line, """WAIT"" noch nicht implementiert")
            MyBase.EnterWait(context)
        End Sub
        Public Overrides Sub EnterDefine(<NotNull> context As ACLParser.DefineContext)
            Dim lineNr As Integer = CType(context.GetChild(0), Tree.ITerminalNode).Symbol.Line

            For i = 0 To context.IDENTIFIER.Length - 1
                Dim varName As String = context.IDENTIFIER(i).GetText()
                'Prüfen ob es diese Variable schon gibt
                If _variables.ContainsKey(varName) Then
                    RaiseEvent CompileErrorEvent(lineNr, $"Variable ""{varName}"" wurde schon in Zeile {_variables(varName).defLine} definiert")
                Else
                    ' Variable hinzufügen
                    _variables.Add(varName, New Variable(varType.int, lineNr))
                    ' DEFVAR hinzufügen
                    Dim progEntry As New ProgramEntry
                    progEntry.func = progFunc.defVar
                    progEntry.lineNr = lineNr
                    progEntry.varName = varName
                    _progList.Add(progEntry)
                End If
            Next

            MyBase.EnterDefine(context)
        End Sub
        Public Overrides Sub EnterDelvar(<NotNull> context As ACLParser.DelvarContext)
            Dim lineNr As Integer = CType(context.GetChild(0), Tree.ITerminalNode).Symbol.Line
            Dim varName As String = context.IDENTIFIER.GetText()
            'Prüfen ob es diese Variable gibt
            If _variables.ContainsKey(varName) Then
                'Variable entfernen
                _variables.Remove(varName)
                ' DELVAR
                Dim progEntry As New ProgramEntry
                progEntry.func = progFunc.delVar
                progEntry.lineNr = lineNr
                progEntry.varName = varName
                _progList.Add(progEntry)
            Else
                RaiseEvent CompileErrorEvent(lineNr, $"Variable ""{varName}"" wurde nicht definiert")
            End If

            MyBase.EnterDelvar(context)
        End Sub
        Public Overrides Sub ExitSetvar(<NotNull> context As ACLParser.SetvarContext)
            Dim lineNr As Integer = context.SET.Symbol.Line
            Dim varName As String = context.IDENTIFIER(0).GetText()

            'Prüfen ob es diese Variable gibt und vom Typ Int ist
            If _variables.ContainsKey(varName) Then
                Dim progEntry As New ProgramEntry
                progEntry.lineNr = lineNr
                progEntry.varName = varName
                progEntry.func = progFunc.setVar
                ' Prüfen welcher Wert zugewiesen werden soll
                If context.SIGNEDINT IsNot Nothing Or context.INTEGER IsNot Nothing Then
                    ' Integer
                    progEntry.varValue = CInt(context.GetChild(3).GetText)
                ElseIf context.BOOL IsNot Nothing Then
                    ' Bool
                    progEntry.varValue = If(context.BOOL.GetText = "FALSE", 0, 1)
                ElseIf context.calculation IsNot Nothing Then
                    ' Calculation
                    progEntry.func = progFunc.setVarToBuffer
                Else
                    ' Variable
                    progEntry.varVariable = context.GetChild(3).GetText
                End If

                _progList.Add(progEntry)
            Else
                RaiseEvent CompileErrorEvent(lineNr, $"Variable ""{varName}"" wurde nicht definiert")
            End If

            MyBase.EnterSetvar(context)
        End Sub
        Public Overrides Sub EnterCalculation(<NotNull> context As ACLParser.CalculationContext)
            Dim lineNr As Integer = CType(context.GetChild(0), Tree.ITerminalNode).Symbol.Line

            ' CALC hinzufügen
            Dim progEntry As New ProgramEntry
            progEntry.func = progFunc.calculation
            progEntry.lineNr = lineNr

            ' Prüfen ob Variable oder Wert
            Dim val1 As String = context.GetChild(0).GetText()
            Dim oper As String = context.GetChild(1).GetText()
            Dim val2 As String = context.GetChild(2).GetText()
            If IsNumeric(val1) Then
                progEntry.val1 = CInt(val1)
            Else
                If Not _variables.ContainsKey(val1) Then
                    RaiseEvent CompileErrorEvent(lineNr, $"Variable ""{val1}"" wurde nicht definiert")
                Else
                    progEntry.var1 = val1
                End If
            End If
            If IsNumeric(val2) Then
                progEntry.val2 = CInt(val2)
            Else
                If Not _variables.ContainsKey(val2) Then
                    RaiseEvent CompileErrorEvent(lineNr, $"Variable ""{val2}"" wurde nicht definiert")
                Else
                    progEntry.var2 = val2
                End If
            End If

            ' Operator
            Select Case oper
                Case "+"
                    progEntry.mathOperator = progMathOperator.plus
                Case "-"
                    progEntry.mathOperator = progMathOperator.minus
                Case "*"
                    progEntry.mathOperator = progMathOperator.mult
                Case "/"
                    progEntry.mathOperator = progMathOperator.div
                Case "EXP"
                    progEntry.mathOperator = progMathOperator.exp
                Case "MOD"
                    progEntry.mathOperator = progMathOperator.mod
            End Select

            _progList.Add(progEntry)

            MyBase.EnterCalculation(context)
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
End Class