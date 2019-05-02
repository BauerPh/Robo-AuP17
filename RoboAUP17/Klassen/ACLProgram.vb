Imports System.IO
Imports Antlr4.Runtime
Imports Antlr4.Runtime.Misc
Imports ACLLexerParser
Imports System.Threading
Friend Class ACLProgram
    ' -----------------------------------------------------------------------------
    ' Definitions
    ' -----------------------------------------------------------------------------
#Region "Definitions"
    Private _savedProgram As String

    Private _robotControl As RobotControl

    Private _teachPoints As New List(Of TeachPoint)
    Private _listBox As ListBox
    Friend ReadOnly Property TcpVariables As New TCPVariables

    Private _progThread As Thread
    Private _runtimeTeachPoints As New List(Of RuntimeTeachPoint)
    Private _progList As New List(Of ProgramEntry)
    Private _programSyntaxOkay As Boolean = False
    Private _programCompiled As Boolean = False
    Private _stopProgram As Boolean = False
    Private _forceStopProgram As Boolean = False

    Private _filename As String = Nothing
    Public Property UnsavedChanges As Boolean = False
    Public Property MaxSpeed As Double
    Public Property MaxAcc As Double

    Friend Event Log(ByVal LogMsg As String, ByVal LogLvl As Logger.LogLevel)
    Friend Event DoJointMove(ByVal jointAngles As JointAngles, acc As Double, speed As Double)
    Friend Event DoCartMove(ByVal cartCoords As CartCoords, acc As Double, speed As Double)
    Friend Event DoRef(ByVal all As Boolean, ByVal axis() As Boolean)
    Friend Event DoServoMove(ByVal srvNr As Int32, prc As Double, speed As Int32)
    Friend Event DoDelay(ByVal delay As Int32)
    Friend Event DoPrint(ByVal msg As String)
    Friend Event ProgramStarted()
    Friend Event ProgramFinished()
    Friend Event ProgramLineChanged(line As Int32)
    Friend Event ErrorLine(line As Int32)
    Friend Event ProgramUpdatedEvent()
#End Region

    ' -----------------------------------------------------------------------------
    ' Public
    ' -----------------------------------------------------------------------------
#Region "Allgemein"
    Friend Sub Init(Optional maxSpeed As Double = -1, Optional maxAcc As Double = -1)
        TcpVariables.TerminateConnection()
        If _robotControl.Pref.TCPServerParameter.Listen Then
            TcpVariables.Listen(_robotControl.Pref.TCPServerParameter.Port)
        End If
        If maxSpeed > 0 And maxAcc > 0 Then
            _MaxSpeed = maxSpeed
            _MaxAcc = maxAcc
        End If
    End Sub
    Friend Sub SetRoboControlObject(ByRef roboControl As RobotControl)
        _robotControl = roboControl
    End Sub
    Friend Sub ClearProgram()
        _teachPoints.Clear()
        TcpVariables.Items.Clear()
        _printTeachpointToListBox()
        _savedProgram = ""
        _filename = Nothing
        RaiseEvent ProgramUpdatedEvent()
    End Sub
    Friend Sub CheckUnsavedChanges(prog As String)
        If _savedProgram <> prog Then
            _UnsavedChanges = True
        End If
    End Sub
#End Region

#Region "Teachpunkte"
    Friend Function GetTeachpointByIndex(index As Int32) As TeachPoint
        If _teachPoints.Count > index And index >= 0 Then
            Return _teachPoints(index)
        End If
        Return Nothing
    End Function
    Friend Function AddTeachPoint(name As String, cartCoords As CartCoords, tpNr As Int32) As Boolean
        Dim tp As TeachPoint
        tp.type = True
        tp.cartCoords = cartCoords.Round(2)
        tp.name = name

        tp.nr = tpNr

        Return _addTeachPoint(tp)
    End Function
    Friend Function AddTeachPoint(name As String, jointAngles As JointAngles, tpNr As Int32) As Boolean
        Dim tp As TeachPoint
        tp.type = False
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
                    indexVal = 0 'damit der selbe TP markiert wird
                End If
                _teachPoints.Sort()
                Dim selIndex As Int32 = _listBox.SelectedIndex + indexVal
                _printTeachpointToListBox()
                _listBox.SelectedIndex = selIndex
                _UnsavedChanges = True
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
            _UnsavedChanges = True
            RaiseEvent Log($"[ACL] Teachpunkt {tpNr} wurde gelöscht!", Logger.LogLevel.INFO)
        End If
    End Sub

    Friend Sub RenameTeachPoint(index As Int32, name As String, nr As Integer)
        If _teachPoints.Count > index And index >= 0 Then
            If TeachpointExists(nr) And nr <> _teachPoints(index).nr Then
                MessageBox.Show($"Nummer existiert bereits.", "Umbenennen fehlgeschlagen", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' Teachpunkt bearbeiten
            Dim tp As TeachPoint = _teachPoints(index)
            tp.name = name
            tp.nr = nr
            _teachPoints(index) = tp

            'Liste Sortieren
            _teachPoints.Sort()
            'und ausgeben
            _printTeachpointToListBox()
            _UnsavedChanges = True
        End If
    End Sub

    Friend Function TeachpointExists(tpNr As Integer) As Boolean
        Return _teachPoints.FindIndex(Function(_tp As TeachPoint) _tp.nr = tpNr) >= 0
    End Function

    Friend Function GetNextFreeTeachPointNum() As Integer
        If _teachPoints.Count > 0 Then
            Return _teachPoints(_teachPoints.Count - 1).nr + 1
        Else
            Return 0
        End If
    End Function

#End Region

#Region "ACL-Programm"
    Friend Function CompileProgram(input As String) As Boolean
        Return _compileProgram(input)
    End Function
    Friend Sub RunProgram(input As String)
        If _compileProgram(input) Then
            ' Runtime Teachpoints nochmals kopieren
            _runtimeTeachPoints = _teachPoints.Select(Function(item) item.GetRuntimeTeachPoint()).ToList()
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
        Dim filename As String
        If _filename Is Nothing Or saveAs Then
            Using saveFileDialog As New SaveFileDialog With {
                .Filter = "RoboAUP17-Dateien (*.aup17)|*.aup17"
            }
                If saveFileDialog.ShowDialog() = DialogResult.OK Then
                    filename = saveFileDialog.FileName
                Else
                    Return False
                End If
            End Using
        Else
            filename = _filename
        End If

        _saveProgram(prog, filename)

        _filename = filename
        _UnsavedChanges = False
        _savedProgram = prog
        Return True
    End Function
    Public Function Load(ByRef prog As String) As Boolean
        Dim filename As String
        ' Benutzer nach Datei fragen
        Using openFileDialog As New OpenFileDialog With {
            .Filter = "RoboAUP17-Dateien (*.aup17)|*.aup17"
        }
            If openFileDialog.ShowDialog() = DialogResult.OK Then
                filename = openFileDialog.FileName
            Else
                Return False
            End If
        End Using
        ' Versuchen Programm zu laden
        If Not _loadProgram(prog, filename) Then
            Return False
        End If
        ' alles okay
        _printTeachpointToListBox()
        _filename = filename
        _UnsavedChanges = False
        _savedProgram = prog
        RaiseEvent ProgramUpdatedEvent()
        Return True
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
        _UnsavedChanges = True
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
    Private Function _compileProgram(input As String) As Boolean
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
        Dim aclErrorListener As New _errorListener()
        AddHandler aclErrorListener.SyntaxErrorEvent, AddressOf _eSyntaxError
        aclParser.AddErrorListener(aclErrorListener)
        ' Parser starten
        Dim rootContext As ACLParser.RootContext = aclParser.root()

        ' ACL Programm kompilieren, wenn Syntax okay
        If _programSyntaxOkay Then
            ' Teachpoints kopieren (deep copy!)
            _runtimeTeachPoints = _teachPoints.Select(Function(item) item.GetRuntimeTeachPoint()).ToList()
            Dim aclCompiler As New ACLCompiler(_runtimeTeachPoints, TcpVariables, _progList, _MaxAcc, _MaxSpeed) ' Eigentlicher Compiler
            AddHandler aclCompiler.CompileErrorEvent, AddressOf _eCompileError
            _programCompiled = True
            _progList.Clear()
            Tree.ParseTreeWalker.Default.Walk(aclCompiler, rootContext)
            RemoveHandler aclCompiler.CompileErrorEvent, AddressOf _eCompileError

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
        Dim calcBuffer As Double
        Dim rtVariables As New Dictionary(Of String, Variable)

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
                Case ProgFunc.noop
                    ' -------------------------------------
                    ' NOOP
                    ' -------------------------------------
                    i += 1
                Case ProgFunc.condition
                    ' -------------------------------------
                    ' CONDITION
                    ' -------------------------------------
                    vke = _checkCondition(cmd, rtVariables, vke)
                    i += 1
                Case ProgFunc.calculation
                    ' -------------------------------------
                    ' CALCULATION
                    ' -------------------------------------
                    'Get val1
                    Dim val1 As Double
                    If Not _getCmdVal(cmd.calcVar1, cmd.lineNr, rtVariables, val1) Then
                        val1 = cmd.calcVal1
                    End If
                    'Get val2
                    Dim val2 As Double
                    If Not _getCmdVal(cmd.calcVar2, cmd.lineNr, rtVariables, val2) Then
                        val2 = cmd.calcVal2
                    End If
                    ' Berechnen
                    Select Case cmd.calcMathOp
                        Case ProgMathOperator.plus
                            calcBuffer = val1 + val2
                        Case ProgMathOperator.minus
                            calcBuffer = val1 - val2
                        Case ProgMathOperator.mult
                            calcBuffer = val1 * val2
                        Case ProgMathOperator.div
                            calcBuffer = val1 / val2
                        Case ProgMathOperator.exp
                            calcBuffer = val1 ^ val2
                        Case ProgMathOperator.mod
                            calcBuffer = val1 Mod val2
                    End Select
                    If [Double].IsInfinity(calcBuffer) Or [Double].IsNaN(calcBuffer) Then
                        _runtimeError(cmd.lineNr, $"Die Berechnung hat zu einem Fehler geführt (Infinity oder NaN)")
                    End If
                    i += 1
                Case ProgFunc.cjump
                    ' -------------------------------------
                    ' CONDITIONED JUMP
                    ' -------------------------------------
                    vke = _checkCondition(cmd, rtVariables, vke)
                    i = If(vke, cmd.jumpTrueTarget, cmd.jumpFalseTarget)
                Case ProgFunc.jump
                    ' -------------------------------------
                    ' JUMP
                    ' -------------------------------------
                    i = cmd.jumpTarget
                Case ProgFunc.servoMove
                    ' -------------------------------------
                    ' SERVO
                    ' -------------------------------------
                    If Not _robotControl.Pref.ServoParameter(cmd.servoNum - 1).Available Then
                        _runtimeError(cmd.lineNr, $"Servo {cmd.servoNum} ist nicht aktiviert")
                        Exit While 'Programm beenden
                    End If
                    'Get Servoval
                    Dim srvVal As Double
                    If Not _getCmdVal(cmd.servoVar, cmd.lineNr, rtVariables, srvVal) Then
                        srvVal = cmd.servoVal
                    End If
                    If srvVal > 100 Or srvVal < 0 Then
                        _runtimeError(cmd.lineNr, $"Servowert muss zwischen 0 und 100 liegen")
                        Exit While 'Programm beenden
                    End If
                    RaiseEvent DoServoMove(cmd.servoNum, srvVal, cmd.servoSpeed)
                    i += 1
                Case ProgFunc.move
                    ' -------------------------------------
                    ' MOVE
                    ' -------------------------------------
                    ' Teachpoint suchen und anfahren
                    Dim tpIndex As Integer
                    If cmd.moveTpIdentifier IsNot Nothing Then
                        tpIndex = _runtimeTeachPoints.FindIndex(Function(_tp As RuntimeTeachPoint) _tp.identifier = cmd.moveTpIdentifier)
                    Else
                        tpIndex = _runtimeTeachPoints.FindIndex(Function(_tp As RuntimeTeachPoint) _tp.tp.nr = cmd.moveTpNr)
                    End If

                    If tpIndex >= 0 Then
                        If Not _runtimeTeachPoints(tpIndex).initialized Then
                            _runtimeError(cmd.lineNr, $"Position ""{_runtimeTeachPoints(tpIndex).identifier}"" ist definiert wurde aber noch nicht mit einer Position beschrieben")
                            Exit While 'Programm beenden
                        End If
                        Dim tp As TeachPoint = _runtimeTeachPoints(tpIndex).tp
                        If tp.type Then
                            If Not _robotControl.CheckCartCoords(tp.cartCoords) Then
                                _runtimeError(cmd.lineNr, $"Koordinaten nicht erreichbar. Entweder ergab die kinematische Berechnung nicht für jede Achse ein Ergebnis oder das Achslimit einer oder mehrerer Achsen wurde überschritten.")
                            Else
                                RaiseEvent DoCartMove(tp.cartCoords, cmd.moveSpeed, cmd.moveAcc)
                            End If
                        Else
                            If Not _robotControl.CheckJointAngles(tp.jointAngles) Then
                                _runtimeError(cmd.lineNr, $"Koordinaten nicht erreichbar. Das Achslimit einer oder mehrerer Achsen wurde überschritten.")
                            Else
                                RaiseEvent DoJointMove(tp.jointAngles, cmd.moveSpeed, cmd.moveAcc)
                            End If
                        End If
                    Else
                        ' Dürfte niemals passieren, da beim compilen schon abgefragt!
                        _runtimeError(cmd.lineNr, "Unmöglicher Fehler ist aufgetreten.... ohje.")
                        StopProgram()
                    End If
                    i += 1
                Case ProgFunc.home
                    ' -------------------------------------
                    ' HOME
                    ' -------------------------------------
                    RaiseEvent DoRef(cmd.refAll, cmd.refAxis)
                    i += 1
                Case ProgFunc.delay
                    ' -------------------------------------
                    ' DELAY
                    ' -------------------------------------
                    RaiseEvent DoDelay(cmd.delayTimeMS)
                    i += 1
                Case ProgFunc.defVar
                    ' -------------------------------------
                    ' DEFINE VARIABLE
                    ' -------------------------------------
                    If rtVariables.ContainsKey(cmd.varName) Then
                        _runtimeError(cmd.lineNr, $"Variable ""{cmd.varName}"" wurde nochmals definiert")
                        Exit While 'Programm beenden
                    End If
                    If TcpVariables.Exists(cmd.varName) Then
                        _runtimeError(cmd.lineNr, $"Variable ""{cmd.varName}"" wurde bereits als TCP-Variable definiert")
                        Exit While 'Programm beenden
                    End If
                    rtVariables.Add(cmd.varName, New Variable(cmd.lineNr))
                    i += 1
                Case ProgFunc.delVar
                    ' -------------------------------------
                    ' DELETE VARIABLE
                    ' -------------------------------------
                    If Not rtVariables.ContainsKey(cmd.varName) Then
                        _runtimeError(cmd.lineNr, $"Variable ""{cmd.varName}"" ist an dieser Stelle nicht definiert")
                        Exit While 'Programm beenden
                    End If
                    rtVariables.Remove(cmd.varName)
                    i += 1
                Case ProgFunc.setVar
                    ' -------------------------------------
                    ' SET VARIABLE
                    ' -------------------------------------
                    If Not _checkVar(cmd.varName, rtVariables) Then
                        _runtimeError(cmd.lineNr, $"Variable ""{cmd.varName}"" ist an dieser Stelle nicht definiert")
                        Exit While 'Programm beenden
                    End If
                    ' Wert holen
                    Dim val As Double
                    If Not _getCmdVal(cmd.varVariable, cmd.lineNr, rtVariables, val) Then
                        val = cmd.varValue
                    End If
                    ' Variable setzen
                    If Not _setVar(cmd.varName, val, cmd.lineNr, rtVariables) Then
                        Exit While 'Programm beenden
                    End If
                    i += 1
                Case ProgFunc.setVarToBuffer
                    ' -------------------------------------
                    ' SET VARIABLE TO BUFFER
                    ' -------------------------------------
                    If Not _checkVar(cmd.varName, rtVariables) Then
                        _runtimeError(cmd.lineNr, $"Variable ""{cmd.varName}"" ist an dieser Stelle nicht definiert")
                        Exit While 'Programm beenden
                    End If
                    ' Variable setzen
                    If Not _setVar(cmd.varName, calcBuffer, cmd.lineNr, rtVariables) Then
                        Exit While 'Programm beenden
                    End If
                    i += 1
                Case ProgFunc.setVarToPosition
                    ' -------------------------------------
                    ' SET VARIABLE TO POSITION
                    ' -------------------------------------
                    ' Variable prüfen
                    If Not _checkVar(cmd.varName, rtVariables) Then
                        _runtimeError(cmd.lineNr, $"Variable ""{cmd.varName}"" ist an dieser Stelle nicht definiert")
                        Exit While 'Programm beenden
                    End If
                    ' Position prüfen
                    Dim index As Integer = _getPosIndex(cmd.posIdentifer, cmd.lineNr)
                    If index = -1 Then
                        _runtimeError(cmd.lineNr, $"Position ""{cmd.posIdentifer}"" wurde nicht definiert")
                        Exit While 'Programm beenden
                    End If
                    Dim tp As RuntimeTeachPoint = _runtimeTeachPoints(index)
                    ' Variable setzen
                    Dim varVal As Double = 0
                    Select Case cmd.varSetVarToPosFunc
                        Case VarToPosFunc.joint
                            If Not tp.tp.type Then
                                varVal = tp.tp.jointAngles.Items(cmd.posAxisOrCoord - 1)
                            Else
                                _runtimeError(cmd.lineNr, $"Der Teachpunkt (""Kartesisch"") und die ausgeführte Operation sind nicht vom gleichen Typ")
                            End If
                        Case VarToPosFunc.cart
                            If tp.tp.type Then
                                varVal = tp.tp.cartCoords.Items(cmd.posAxisOrCoord - 1)
                            Else
                                _runtimeError(cmd.lineNr, $"Der Teachpunkt (""Achswinkel"") und die ausgeführte Operation sind nicht vom gleichen Typ")
                            End If
                        Case VarToPosFunc.type
                            varVal = If(tp.tp.type, 1, 0)
                    End Select
                    If Not _setVar(cmd.varName, varVal, cmd.lineNr, rtVariables) Then
                        Exit While 'Programm beenden
                    End If
                    i += 1
                Case ProgFunc.defPos
                    ' -------------------------------------
                    ' DEFINE POSITION
                    ' -------------------------------------
                    If _getPosIndex(cmd.posIdentifer, cmd.lineNr) >= 0 Then
                        _runtimeError(cmd.lineNr, $"Position ""{cmd.posIdentifer}"" wurde bereits definiert")
                        Exit While 'Programm beenden
                    End If
                    Dim tp As New RuntimeTeachPoint
                    tp.identifier = cmd.posIdentifer
                    tp.initialized = False
                    tp.tp.nr = -1
                    _runtimeTeachPoints.Add(tp)
                    i += 1
                Case ProgFunc.delPos
                    ' -------------------------------------
                    ' DELETE POSITION
                    ' -------------------------------------
                    Dim index As Integer = _getPosIndex(cmd.posIdentifer, cmd.lineNr)
                    If index = -1 Then
                        _runtimeError(cmd.lineNr, $"Position ""{cmd.posIdentifer}"" wurde nicht definiert")
                        Exit While 'Programm beenden
                    End If
                    _runtimeTeachPoints.RemoveAt(index)
                    i += 1
                Case ProgFunc.undefPos
                    ' -------------------------------------
                    ' UNDEFINE POSITION
                    ' -------------------------------------
                    Dim index As Integer = _getPosIndex(cmd.posIdentifer, cmd.lineNr)
                    If index = -1 Then
                        _runtimeError(cmd.lineNr, $"Position ""{cmd.posIdentifer}"" wurde nicht definiert")
                        Exit While 'Programm beenden
                    End If
                    Dim tp As RuntimeTeachPoint = _runtimeTeachPoints(index)
                    tp.initialized = False
                    _runtimeTeachPoints(index) = tp
                    i += 1
                Case ProgFunc.recordPos
                    ' -------------------------------------
                    ' RECORD POSITION
                    ' -------------------------------------
                    Dim index As Integer = _getPosIndex(cmd.posIdentifer, cmd.lineNr)
                    If index = -1 Then
                        _runtimeError(cmd.lineNr, $"Position ""{cmd.posIdentifer}"" wurde nicht definiert")
                        Exit While 'Programm beenden
                    End If
                    Dim tp As RuntimeTeachPoint = _runtimeTeachPoints(index)

                    tp.initialized = True
                    tp.tp.type = cmd.posType

                    ' Aktuelle Position speichern
                    If cmd.posType Then
                        tp.tp.cartCoords = _robotControl.PosCart
                    Else
                        tp.tp.jointAngles = _robotControl.PosJoint
                    End If

                    _runtimeTeachPoints(index) = tp
                    i += 1
                Case ProgFunc.changePos
                    ' -------------------------------------
                    ' CHANGE POSITION
                    ' -------------------------------------
                    Dim index As Integer = _getPosIndex(cmd.posIdentifer, cmd.lineNr)
                    If index = -1 Then
                        _runtimeError(cmd.lineNr, $"{If(IsNumeric(cmd.posIdentifer), $"Teachpunkt {cmd.posIdentifer}", $"Position ""{cmd.posIdentifer}""")} wurde nicht definiert")
                        Exit While 'Programm beenden
                    End If
                    ' Wert holen
                    Dim val As Double
                    If Not _getCmdVal(cmd.varVariable, cmd.lineNr, rtVariables, val) Then
                        val = cmd.varValue
                    End If

                    ' Position bearbeiten
                    If Not _editPos(index, cmd, val) Then
                        Exit While
                    End If
                    i += 1
                Case ProgFunc.copyPos
                    ' -------------------------------------
                    ' COPY POSITION
                    ' -------------------------------------
                    Dim index1 As Integer = _getPosIndex(cmd.posIdentifer, cmd.lineNr)
                    Dim index2 As Integer = _getPosIndex(cmd.posCopyPos, cmd.lineNr)
                    If index1 = -1 Then
                        _runtimeError(cmd.lineNr, $"Position ""{cmd.posIdentifer}"" wurde nicht definiert")
                        Exit While 'Programm beenden
                    End If
                    If index2 = -1 Then
                        _runtimeError(cmd.lineNr, $"{If(IsNumeric(cmd.posCopyPos), $"Teachpunkt {cmd.posCopyPos}", $"Position ""{cmd.posCopyPos}""")}  wurde nicht definiert")
                        Exit While 'Programm beenden
                    End If
                    ' Position kopieren
                    Dim tp As RuntimeTeachPoint = _runtimeTeachPoints(index1)
                    Dim copyTp As RuntimeTeachPoint = _runtimeTeachPoints(index2)
                    tp.initialized = copyTp.initialized
                    tp.tp.cartCoords = CType(copyTp.tp.cartCoords.Clone, CartCoords)
                    tp.tp.jointAngles = CType(copyTp.tp.jointAngles.Clone, JointAngles)
                    tp.tp.type = copyTp.tp.type
                    _runtimeTeachPoints(index1) = tp
                    i += 1
                Case ProgFunc.print
                    ' -------------------------------------
                    ' PRINT
                    ' -------------------------------------
                    Dim txt As String = ""
                    For j = 0 To cmd.printVal.Count - 1
                        Dim pv As PrintVal = cmd.printVal(j)
                        Dim value As Double
                        If pv.isVar Then
                            If Not _getCmdVal(pv.val, cmd.lineNr, rtVariables, value) Then
                                _runtimeError(cmd.lineNr, $"Der Wert der Variable konnte nicht ermittelt werden.")
                            Else
                                txt = txt & value.ToString().Replace(",", ".")
                            End If
                        Else
                            txt = txt & pv.val
                        End If
                    Next
                    RaiseEvent DoPrint(txt)
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
    Private Function _checkCondition(cmd As ProgramEntry, ByRef rtVariables As Dictionary(Of String, Variable), ByRef vke As Boolean) As Boolean
        'Get val1
        Dim val1 As Double
        If Not _getCmdVal(cmd.calcVar1, cmd.lineNr, rtVariables, val1) Then
            val1 = cmd.calcVal1
        End If
        'Get val2
        Dim val2 As Double
        If Not _getCmdVal(cmd.calcVar2, cmd.lineNr, rtVariables, val2) Then
            val2 = cmd.calcVal2
        End If

        ' VKE Berechnen
        Dim tmpVKE As Boolean
        Select Case cmd.calcCompareOp
            Case ProgCompOperator.equal
                tmpVKE = (val1 = val2)
            Case ProgCompOperator.greater
                tmpVKE = (val1 > val2)
            Case ProgCompOperator.less
                tmpVKE = (val1 < val2)
            Case ProgCompOperator.greaterOrEqual
                tmpVKE = (val1 >= val2)
            Case ProgCompOperator.lessOrEqual
                tmpVKE = (val1 <= val2)
            Case ProgCompOperator.notEqual
                tmpVKE = (val1 <> val2)
        End Select

        ' Mit vorherigem VKE verknüpfen
        If Not cmd.VKEFirst Then
            Select Case cmd.calcBoolOp
                Case ProgBoolOperator.and
                    tmpVKE = tmpVKE And vke
                Case ProgBoolOperator.or
                    tmpVKE = tmpVKE Or vke
            End Select
        End If
        Return tmpVKE
    End Function
    Private Function _getCmdVal(var As String, line As Integer, ByRef rtVariables As Dictionary(Of String, Variable), ByRef val As Double) As Boolean
        If var IsNot Nothing Then
            ' RT Var
            If rtVariables.ContainsKey(var) Then
                val = rtVariables(var).val
            Else
                ' TCP Var
                If Not TcpVariables.GetVariable(var, val) Then
                    _runtimeError(line, $"TCP-Variable {var} ist nicht mehr vorhanden")
                End If
            End If
            Return True
        Else
            Return False
        End If
    End Function
    Private Function _checkVar(name As String, ByRef rtvars As Dictionary(Of String, Variable)) As Boolean
        Return rtvars.ContainsKey(name) Or TcpVariables.Exists(name)
    End Function
    Private Function _setVar(ByVal name As String, ByVal val As Double, ByVal lineNr As Integer, ByRef vars As Dictionary(Of String, Variable)) As Boolean
        ' Variable setzen
        If vars.ContainsKey(name) Then
            Dim var As Variable = vars(name)
            var.val = val
            vars(name) = var
        Else
            ' TCP-Variable
            If Not TcpVariables.SetVariable(name, val) Then
                _runtimeError(lineNr, $"TCP-Variable ""{name}"" wurde nicht gefunden")
                Return False
            End If
        End If
        Return True
    End Function
    Private Function _getPosIndex(identifier As String, lineNr As Integer) As Integer
        If IsNumeric(identifier) Then
            Try
                Return _runtimeTeachPoints.FindIndex(Function(_tp As RuntimeTeachPoint) _tp.tp.nr = CInt(identifier))
            Catch e As OverflowException
                _runtimeError(lineNr, $"Es sind nur Werte zwischen {-2 ^ 31} und {2 ^ 31 - 1} möglich (32-Bit Integer)")
                Return -1
            End Try
        Else
            Return _runtimeTeachPoints.FindIndex(Function(_tp As RuntimeTeachPoint) _tp.identifier = identifier)
        End If
    End Function
    Private Function _editPos(ByVal index As Integer, ByRef cmd As ProgramEntry, val As Double) As Boolean
        Dim tp As RuntimeTeachPoint = _runtimeTeachPoints(index)
        If Not tp.initialized Then
            tp.initialized = True
            tp.tp.type = cmd.posType
        Else
            If tp.tp.type <> cmd.posType Then
                _runtimeError(cmd.lineNr, $"Der Teachpunkt ({If(tp.tp.type, "Kartesisch", "Achswinkel")}) und die ausgeführte Operation ({If(cmd.posType, "Kartesisch", "Achswinkel")}) sind nicht vom gleichen Typ")
                Return False
            End If
        End If
        Dim axis As Integer = cmd.posAxisOrCoord - 1
        If cmd.posType Then
            If cmd.posShift Then
                tp.tp.cartCoords.SetByIndex(axis, tp.tp.cartCoords.Items(axis) + val)
            Else
                tp.tp.cartCoords.SetByIndex(axis, val)
            End If
        Else
            If cmd.posShift Then
                tp.tp.jointAngles.SetByIndex(axis, tp.tp.jointAngles.Items(axis) + val)
            Else
                tp.tp.jointAngles.SetByIndex(axis, val)
            End If
        End If
        _runtimeTeachPoints(index) = tp
        Return True
    End Function

    Private Sub _runtimeError(lineNr As Integer, msg As String)
        RaiseEvent ErrorLine(lineNr)
        RaiseEvent Log($"[ACL-RT] Zeile {lineNr.ToString,3:N}: {msg}", Logger.LogLevel.ERR)
        _forceStopProgram = True
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

#Region "Speichern / Laden"
    Private Sub _saveProgram(prog As String, filename As String)
        Using objStreamWriter As New StreamWriter(filename)
            'Max Speed / Acc
            objStreamWriter.WriteLine($"<maxSpeedAcc>;{_MaxSpeed};{_MaxAcc}")
            'Teachpunkte
            For Each tp As TeachPoint In _teachPoints
                objStreamWriter.Write($"<tp>;{tp.nr};{tp.name};{tp.type}")
                If tp.type Then
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
            'Variablen
            For Each var As KeyValuePair(Of String, Double) In TcpVariables.Items
                objStreamWriter.WriteLine($"<var>;{var.Key}")
            Next
            'Programm
            objStreamWriter.WriteLine("<program>")
            objStreamWriter.Write(prog)
            objStreamWriter.Close()
        End Using
    End Sub

    Private Function _loadProgram(ByRef prog As String, filename As String) As Boolean
        Dim retVal As Boolean = True
        ' Teachpunkte & Variablen leeren
        _teachPoints.Clear()
        TcpVariables.Items.Clear()

        Using objStreamReader As New StreamReader(filename)
            Try
                'Jede Zeile der Datei einlesen
                Dim strLine As String
                Do
                    strLine = objStreamReader.ReadLine
                    If Not strLine Is Nothing Then
                        Dim tmpSplit As String() = strLine.Split(";"c)
                        If tmpSplit(0) = "<maxSpeedAcc>" Then
                            'maxSpeed maxAcc
                            _MaxSpeed = CDbl(tmpSplit(1))
                            _MaxAcc = CDbl(tmpSplit(2))
                        ElseIf tmpSplit(0) = "<tp>" Then
                            'TeachPunkt
                            Dim item As New TeachPoint
                            item.nr = CInt(tmpSplit(1))
                            item.name = tmpSplit(2)
                            item.type = CBool(tmpSplit(3))
                            If item.type Then
                                For i = 0 To 5
                                    item.cartCoords.SetByIndex(i, CDbl(tmpSplit(4 + i)))
                                Next
                            Else
                                For i = 0 To 5
                                    item.jointAngles.SetByIndex(i, CDbl(tmpSplit(4 + i)))
                                Next
                            End If
                            _teachPoints.Add(item)
                        ElseIf tmpSplit(0) = "<var>" Then
                            'Variable
                            TcpVariables.AddVariable(tmpSplit(1))
                        ElseIf tmpSplit(0) = "<program>" Then
                            ' Programm einlesen
                            prog = objStreamReader.ReadToEnd()
                        End If
                    End If
                Loop Until strLine Is Nothing
            Catch ex As Exception
                _teachPoints.Clear()
                TcpVariables.Items.Clear()
                prog = ""
                retVal = False
            Finally
                'StreamReader schließen
                objStreamReader.Close()
            End Try
        End Using
        Return retVal
    End Function
#End Region

#Region "ErrorListener für Syntax Errors"
    Private Class _errorListener
        Inherits BaseErrorListener
        Friend Event SyntaxErrorEvent(ByVal line As Integer, msg As String)
        Public Overrides Sub SyntaxError(<NotNull> recognizer As IRecognizer, <Nullable> offendingSymbol As IToken, line As Integer, charPositionInLine As Integer, <NotNull> msg As String, <Nullable> e As RecognitionException)
            RaiseEvent SyntaxErrorEvent(line, msg)
            MyBase.SyntaxError(recognizer, offendingSymbol, line, charPositionInLine, msg, e)
        End Sub
    End Class
#End Region
End Class