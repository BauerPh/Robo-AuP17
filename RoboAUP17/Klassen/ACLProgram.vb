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

        Using objStreamWriter As New StreamWriter(tmpFilename)
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
        _filename = tmpFilename
        _UnsavedChanges = False
        _savedProgram = prog
        Return True
    End Function
    Public Function Load(ByRef prog As String) As Boolean
        Dim erg As Boolean = True
        Dim readProg As Boolean = False
        Dim openFileDialog As New OpenFileDialog With {
           .Filter = "RoboAUP17-Dateien (*.aup17)|*.aup17"
        }
        If openFileDialog.ShowDialog() = DialogResult.OK Then
            ' Teachpunkte & Variablen leeren
            _teachPoints.Clear()
            TcpVariables.Items.Clear()

            Using objStreamReader As New StreamReader(openFileDialog.FileName)
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

                    _printTeachpointToListBox()
                    _filename = openFileDialog.FileName
                    _UnsavedChanges = False
                    _savedProgram = prog
                    RaiseEvent ProgramUpdatedEvent()
                Catch ex As Exception
                    _teachPoints.Clear()
                    TcpVariables.Items.Clear()
                    prog = ""
                    erg = False
                Finally
                    'StreamReader schließen
                    objStreamReader.Close()
                End Try
            End Using
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
            Dim aclListener As New _ACLListener(_runtimeTeachPoints, TcpVariables, _progList, _MaxAcc, _MaxSpeed) ' Eigentlicher Compiler
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

#Region "ParserListener (Compiler)"
    Private Class _ACLListener
        Inherits ACLParserBaseListener
        Private _maxAcc, _maxSpeed, _acc, _speed As Double
        Private _stack As New Stack(Of Integer)
        Private _labels As New Dictionary(Of String, Integer)
        Private _gotos As New Dictionary(Of String, List(Of Integer))
        Private _variables As New Dictionary(Of String, Variable)

        ' von Extern!
        Private _tp As List(Of RuntimeTeachPoint)
        Private _tcpVars As TCPVariables
        Private _progList As List(Of ProgramEntry)

        Friend Event CompileErrorEvent(ByVal line As Integer, ByVal msg As String)

        Friend Sub New(ByRef tp As List(Of RuntimeTeachPoint), ByRef tcpVars As TCPVariables, ByRef progList As List(Of ProgramEntry), ByVal maxAcc As Double, ByVal maxSpeed As Double)
            _maxAcc = maxAcc
            _maxSpeed = maxSpeed
            _acc = maxAcc
            _speed = maxSpeed
            _tp = tp
            _progList = progList
            _tcpVars = tcpVars
        End Sub
        'Condition
        Public Overrides Sub EnterCondition(<NotNull> context As ACLParser.ConditionContext)
            ' Letztes Element holen
            Dim thisIndex As Int32 = _progList.Count - 1
            Dim progEntry As ProgramEntry = _progList(thisIndex)

            ' Prüfen ob Variable oder Wert
            Dim val1 As String = context.GetChild(0).GetText()
            Dim oper As String = context.GetChild(1).GetText()
            Dim val2 As String = context.GetChild(2).GetText()
            If IsNumeric(val1) Then
                progEntry.calcVal1 = _getDouble(val1)
            Else
                If Not _checkVar(val1) Then
                    RaiseEvent CompileErrorEvent(progEntry.lineNr, $"Variable ""{val1}"" wurde nicht definiert")
                Else
                    progEntry.calcVar1 = val1
                End If
            End If
            If IsNumeric(val2) Then
                progEntry.calcVal2 = _getDouble(val2)
            ElseIf context.BOOL IsNot Nothing Then
                ' Bool
                progEntry.calcVal2 = If(context.BOOL.GetText = "FALSE", 0, 1)
            Else
                ' Variable
                If Not _checkVar(val2) Then
                    RaiseEvent CompileErrorEvent(progEntry.lineNr, $"Variable ""{val2}"" wurde nicht definiert")
                Else
                    progEntry.calcVar2 = val2
                End If
            End If

            ' Operator
            Select Case oper
                Case ">"
                    progEntry.calcCompareOp = ProgCompOperator.greater
                Case "<"
                    progEntry.calcCompareOp = ProgCompOperator.less
                Case ">="
                    progEntry.calcCompareOp = ProgCompOperator.greaterOrEqual
                Case "<="
                    progEntry.calcCompareOp = ProgCompOperator.lessOrEqual
                Case "="
                    progEntry.calcCompareOp = ProgCompOperator.equal
                Case "<>"
                    progEntry.calcCompareOp = ProgCompOperator.notEqual
            End Select

            ' Zurückspeichern
            _progList(thisIndex) = progEntry

            MyBase.EnterCondition(context)
        End Sub
        'Calculation
        Public Overrides Sub EnterCalculation(<NotNull> context As ACLParser.CalculationContext)
            Dim lineNr As Integer = CType(context.GetChild(0), Tree.ITerminalNode).Symbol.Line

            ' CALC hinzufügen
            Dim progEntry As New ProgramEntry
            progEntry.func = ProgFunc.calculation
            progEntry.lineNr = lineNr

            ' Prüfen ob Variable oder Wert
            Dim val1 As String = context.GetChild(0).GetText()
            Dim oper As String = context.GetChild(1).GetText()
            Dim val2 As String = context.GetChild(2).GetText()
            If IsNumeric(val1) Then
                progEntry.calcVal1 = _getDouble(val1)
            Else
                If Not _checkVar(val1) Then
                    RaiseEvent CompileErrorEvent(lineNr, $"Variable ""{val1}"" wurde nicht definiert")
                Else
                    progEntry.calcVar1 = val1
                End If
            End If
            If IsNumeric(val2) Then
                progEntry.calcVal2 = _getDouble(val2)
            Else
                If Not _checkVar(val2) Then
                    RaiseEvent CompileErrorEvent(lineNr, $"Variable ""{val2}"" wurde nicht definiert")
                Else
                    progEntry.calcVar2 = val2
                End If
            End If

            ' Operator
            Select Case oper
                Case "+"
                    progEntry.calcMathOp = ProgMathOperator.plus
                Case "-"
                    progEntry.calcMathOp = ProgMathOperator.minus
                Case "*"
                    progEntry.calcMathOp = ProgMathOperator.mult
                Case "/"
                    progEntry.calcMathOp = ProgMathOperator.div
                Case "^"
                    progEntry.calcMathOp = ProgMathOperator.exp
                Case "%"
                    progEntry.calcMathOp = ProgMathOperator.mod
            End Select

            _progList.Add(progEntry)

            MyBase.EnterCalculation(context)
        End Sub

        ' ********************************************************
        ' Axis Control
        ' ********************************************************
        'MOVE
        Public Overrides Sub EnterMove(<NotNull> context As ACLParser.MoveContext)
            Dim lineNr As Integer = context.MOVE.Symbol.Line
            Dim tpNr As Integer = -1
            Dim tpIdentifier As String = Nothing
            If context.INTEGER IsNot Nothing Then
                If Not _getAndCheckInt(context.INTEGER.GetText, tpNr) Then
                    RaiseEvent CompileErrorEvent(lineNr, $"Es sind nur Werte zwischen {[Int32].MinValue} und {[Int32].MaxValue} möglich (32-Bit Integer)")
                End If
            Else
                tpIdentifier = context.IDENTIFIER.GetText()
            End If

            ' Teachpunkt suchen und move Befehl hinzufgen
            Dim tpIndex As Integer
            If tpNr >= 0 Then
                tpIndex = _tp.FindIndex(Function(_tp As RuntimeTeachPoint) _tp.tp.nr = tpNr)
            Else
                tpIndex = _tp.FindIndex(Function(_tp As RuntimeTeachPoint) _tp.identifier = tpIdentifier)
            End If
            If tpIndex >= 0 Then
                ' Move hinzufügen
                Dim progEntry As New ProgramEntry With {
                    .func = ProgFunc.move,
                    .lineNr = lineNr,
                    .moveAcc = _acc,
                    .moveSpeed = _speed
                }
                If tpNr >= 0 Then
                    progEntry.moveTpNr = tpNr
                Else
                    progEntry.moveTpIdentifier = tpIdentifier
                End If
                _progList.Add(progEntry)
            Else
                RaiseEvent CompileErrorEvent(lineNr, $"{If(tpNr >= 0, $"Teachpunkt {tpNr}", $"Position ""{tpIdentifier}""")} nicht gefunden")
            End If

            MyBase.EnterMove(context)
        End Sub
        'HOME
        Public Overrides Sub EnterHome(<NotNull> context As ACLParser.HomeContext)
            Dim lineNr As Integer = context.HOME.Symbol.Line
            ' Achsennummer
            Dim axis(5) As Boolean
            Dim refAll As Boolean = False
            If context.INTEGER.Length > 0 Then
                If context.INTEGER.Length > 6 Then
                    RaiseEvent CompileErrorEvent(lineNr, $"Es sind maximal 6 Werte erlaubt")
                Else
                    For i = 0 To context.INTEGER.Length - 1
                        Dim num As Integer
                        If _getAndCheckInt(context.INTEGER(i).GetText, num, 1, 6) Then
                            axis(num - 1) = True
                        Else
                            RaiseEvent CompileErrorEvent(lineNr, $"Es sind nur Werte zwischen 1 und 6 erlaubt")
                        End If
                    Next
                End If
            Else
                refAll = True
            End If
            ' home hinzufügen
            Dim progEntry As New ProgramEntry With {
                    .func = ProgFunc.home,
                    .refAll = refAll,
                    .refAxis = axis,
                    .lineNr = context.HOME.Symbol.Line
            }
            _progList.Add(progEntry)

            MyBase.EnterHome(context)
        End Sub
        'OPEN / CLOSE
        Public Overrides Sub EnterOpenclose(<NotNull> context As ACLParser.OpencloseContext)
            Dim lineNr As Integer = CType(context.GetChild(0), Tree.ITerminalNode).Symbol.Line
            Dim servoNr As Int32
            If context.INTEGER Is Nothing Then
                servoNr = 1
            Else
                If Not _getAndCheckInt(context.INTEGER.GetText, servoNr, 1, 3) Then
                    RaiseEvent CompileErrorEvent(lineNr, $"Servonummer muss zwischen 1 und 3 liegen")
                End If
            End If
            ' Servomove hinzufügen
            Dim thisIndex As Int32 = _progList.Count
            Dim progEntry As New ProgramEntry
            progEntry.func = ProgFunc.servoMove
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
        'JAW
        Public Overrides Sub EnterJaw(<NotNull> context As ACLParser.JawContext)
            Dim lineNr As Integer = context.JAW.Symbol.Line
            Dim servoNr As Int32
            If Not _getAndCheckInt(context.GetChild(1).GetText(), servoNr, 1, 3) Then
                RaiseEvent CompileErrorEvent(lineNr, $"Servonummer muss zwischen 1 und 3 liegen")
            End If
            Dim servoSpeed As Int32
            If context.GetChild(3) IsNot Nothing Then
                If Not _getAndCheckInt(context.GetChild(3).GetText(), servoSpeed, 1, 100) Then
                    RaiseEvent CompileErrorEvent(lineNr, $"Servogeschwindigkeit muss zwischen 1 und 100 liegen")
                End If
            Else
                servoSpeed = 100
            End If

            ' Servomove hinzufügen
            Dim thisIndex As Int32 = _progList.Count
            Dim progEntry As New ProgramEntry
            progEntry.func = ProgFunc.servoMove
            progEntry.lineNr = lineNr
            progEntry.servoNum = servoNr
            progEntry.servoSpeed = servoSpeed

            Dim servoVal As String = context.GetChild(2).GetText()
            If IsNumeric(servoVal) Then
                progEntry.servoVal = _getDouble(servoVal)
                If progEntry.servoVal > 100 Or progEntry.servoVal < 0 Then
                    RaiseEvent CompileErrorEvent(lineNr, $"Servowert muss zwischen 0 und 100 liegen")
                End If
            Else
                If Not _checkVar(servoVal) Then
                    RaiseEvent CompileErrorEvent(lineNr, $"Variable ""{servoVal}"" wurde nicht definiert")
                Else
                    progEntry.servoVar = servoVal
                End If
            End If
            _progList.Add(progEntry)

            MyBase.EnterJaw(context)
        End Sub
        'ACC
        Public Overrides Sub EnterAcc(<NotNull> context As ACLParser.AccContext)
            Dim lineNr As Integer = context.ACC.Symbol.Line
            Dim acc As Integer
            If Not _getAndCheckInt(context.INTEGER.GetText, acc, 1, 100) Then
                RaiseEvent CompileErrorEvent(lineNr, $"ACC muss zwischen 1 und 100 liegen")
            Else
                _acc = (acc / 100) * _maxAcc
            End If

            MyBase.EnterAcc(context)
        End Sub
        'SPEED
        Public Overrides Sub EnterSpeed(<NotNull> context As ACLParser.SpeedContext)
            Dim lineNr As Integer = context.SPEED.Symbol.Line
            Dim speed As Integer
            If Not _getAndCheckInt(context.INTEGER.GetText, speed, 1, 100) Then
                RaiseEvent CompileErrorEvent(lineNr, $"SPEED muss zwischen 1 und 100 liegen")
            Else
                _speed = (speed / 100) * _maxSpeed
            End If

            MyBase.EnterSpeed(context)
        End Sub

        ' ********************************************************
        ' Program Flow
        ' ********************************************************
        'IF
        Public Overrides Sub EnterIf(<NotNull> context As ACLParser.IfContext)
            Dim thisIndex As Int32 = _progList.Count
            ' Bedingter Sprung hinzufügen (Bedingung wird bei "EnterCondition" hinzugefügt)
            Dim progEntry As New ProgramEntry With {
                .func = ProgFunc.cjump,
                .lineNr = context.IF.Symbol.Line,
                .VKEFirst = True,
                .jumpTrueTarget = thisIndex + 1, ' Auf nächsten Eintrag, wenn dieser hier hinzugefügt wurde
                .jumpFalseTarget = -1 ' Wird bei ANDIF, ORIF, ELSE oder ENDIF gesetzt!
            }
            _progList.Add(progEntry)

            ' Index von diesem IF auf den Stack legen
            _stack.Push(thisIndex)

            MyBase.EnterIf(context)
        End Sub
        'ANDIF / ORIF
        Public Overrides Sub EnterAnd_or_if(<NotNull> context As ACLParser.And_or_ifContext)
            ' Bedingter Sprung hinzufügen (Bedingung wird bei "EnterCondition" hinzugefügt)
            Dim thisIndex As Int32 = _progList.Count
            Dim progEntry As New ProgramEntry
            progEntry.func = ProgFunc.cjump
            progEntry.lineNr = CType(context.GetChild(0), Tree.ITerminalNode).Symbol.Line
            If CType(context.GetChild(0), Tree.ITerminalNode).Symbol.Type = ACLLexer.ANDIF Then
                progEntry.calcBoolOp = ProgBoolOperator.and
            Else
                progEntry.calcBoolOp = ProgBoolOperator.or
            End If
            progEntry.VKEFirst = False
            progEntry.jumpTrueTarget = thisIndex + 1 ' Auf nächsten Eintrag, wenn dieser hier hinzugefügt wurde
            progEntry.jumpFalseTarget = -1 ' Wird bei ANDIF, ORIF, ELSE oder ENDIF gesetzt!
            _progList.Add(progEntry)

            ' Eintrag vom IF vom Stack holen und durch Condition ersetzen
            Dim progListIfEntryNum = _stack.Pop
            progEntry = _progList(progListIfEntryNum)
            progEntry.func = ProgFunc.condition
            _progList(progListIfEntryNum) = progEntry

            _stack.Push(thisIndex) ' Index von diesem IF auf den Stack legen

            MyBase.EnterAnd_or_if(context)
        End Sub
        'ELSE
        Public Overrides Sub EnterElse(<NotNull> context As ACLParser.ElseContext)
            Dim thisIndex As Int32 = _progList.Count

            ' Sprung hinzufügen
            Dim progEntry As New ProgramEntry
            progEntry.func = ProgFunc.jump
            progEntry.lineNr = context.ELSE.Symbol.Line
            progEntry.jumpTarget = -1 ' Wird bei ENDIF gesetzt
            _progList.Add(progEntry)

            ' Eintrag vom IF vom Stack holen und bearbeiten
            Dim progListIfEntryNum = _stack.Pop
            progEntry = _progList(progListIfEntryNum)
            progEntry.jumpFalseTarget = thisIndex + 1
            _progList(progListIfEntryNum) = progEntry

            ' Index von diesem ELSE auf den Stack legen
            _stack.Push(thisIndex)

            MyBase.EnterElse(context)
        End Sub
        'ENDIF (Exit IF)
        Public Overrides Sub ExitIf(<NotNull> context As ACLParser.IfContext)
            Dim thisIndex As Int32 = _progList.Count

            ' NOOP hinzufügen
            Dim progEntry As New ProgramEntry
            progEntry.func = ProgFunc.noop
            progEntry.lineNr = context.ENDIF.Symbol.Line
            _progList.Add(progEntry)

            ' Eintrag vom IF vom Stack holen und bearbeiten
            Dim progListIfOrElseEntryNum = _stack.Pop
            progEntry = _progList(progListIfOrElseEntryNum)
            If progEntry.func = ProgFunc.cjump Then
                progEntry.jumpFalseTarget = thisIndex
            Else
                progEntry.jumpTarget = thisIndex
            End If
            _progList(progListIfOrElseEntryNum) = progEntry

            MyBase.ExitIf(context)
        End Sub
        'FOR
        Public Overrides Sub EnterFor(<NotNull> context As ACLParser.ForContext)
            Dim lineNr As Integer = context.FOR.Symbol.Line
            Dim countVarName As String = context.GetChild(1).GetText()
            Dim valFrom As Double = 0
            Dim varFrom As String = Nothing
            Dim valTo As Double = 0
            Dim valVarTo As String = Nothing

            ' Startwert
            _getVarVal(context.GetChild(3).GetText(), lineNr, valFrom, varFrom)
            ' Endwert
            _getVarVal(context.GetChild(5).GetText(), lineNr, valTo, valVarTo)

            '-----------------------------------------------
            ' Variable mit Startwert initialisieren
            _setVar(countVarName, lineNr, valFrom, varFrom, False)

            ' Bedingter Sprung erstellen (Bedingung: Zählvariable <= Ende)
            Dim thisIndex As Int32 = _progList.Count
            Dim progEntry As New ProgramEntry With {
                .func = ProgFunc.cjump,
                .lineNr = lineNr,
                .VKEFirst = True,
                .jumpTrueTarget = thisIndex + 1, ' Auf nächsten Eintrag, wenn dieser hier hinzugefügt wurde
                .jumpFalseTarget = -1, ' Wird bei ENDFOR gesetzt!
                .calcVar1 = countVarName,
                .calcCompareOp = ProgCompOperator.lessOrEqual,
                .calcVal2 = valTo,
                .calcVar2 = valVarTo
            }

            _progList.Add(progEntry)
            ' Index von diesem Sprung auf den Stack legen
            _stack.Push(thisIndex)

            MyBase.EnterFor(context)
        End Sub
        'ENDFOR
        Public Overrides Sub ExitFor(<NotNull> context As ACLParser.ForContext)
            Dim lineNr As Integer = context.ENDFOR.Symbol.Line
            Dim countVarName As String = context.GetChild(1).GetText()

            ' Zählvariable hochzählen
            Dim progEntry As New ProgramEntry With {
                .func = ProgFunc.calculation,
                .lineNr = lineNr,
                .calcVar1 = countVarName,
                .calcVal2 = 1,
                .calcMathOp = ProgMathOperator.plus
            }
            _progList.Add(progEntry)
            progEntry = New ProgramEntry With {
                .func = ProgFunc.setVarToBuffer,
                .varName = countVarName,
                .lineNr = lineNr
            }
            _progList.Add(progEntry)

            ' Sprungziel für FOR setzen
            Dim thisIndex As Int32 = _progList.Count
            Dim progListForEntryNum = _stack.Pop ' For index vom Stack holen
            ' Rücksprung setzen
            progEntry = New ProgramEntry With {
                .func = ProgFunc.jump,
                .lineNr = lineNr,
                .jumpTarget = progListForEntryNum
            }
            _progList.Add(progEntry)
            ' NOOP hinzufügen
            progEntry = New ProgramEntry With {
                .func = ProgFunc.noop,
                .lineNr = lineNr
            }
            _progList.Add(progEntry)
            ' For Sprungziel bearbeiten
            progEntry = _progList(progListForEntryNum)
            progEntry.jumpFalseTarget = thisIndex + 1
            _progList(progListForEntryNum) = progEntry

            MyBase.ExitFor(context)
        End Sub
        'LABEL
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
                progEntry.func = ProgFunc.noop
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
        'GOTO
        Public Overrides Sub EnterGoto(<NotNull> context As ACLParser.GotoContext)
            Dim lineNr As Integer = context.GOTO.Symbol.Line
            Dim thisIndex As Int32 = _progList.Count

            ' Sprung hinzufügen
            Dim progEntry As New ProgramEntry With {
                .func = ProgFunc.jump,
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

        ' ********************************************************
        ' Program Control
        ' ********************************************************
        'DELAY
        Public Overrides Sub EnterDelay(<NotNull> context As ACLParser.DelayContext)
            Dim lineNr As Integer = context.DELAY.Symbol.Line
            Dim delay As Int32 = 1
            If Not _getAndCheckInt(context.INTEGER.GetText(), delay, 1, 360000) Then
                RaiseEvent CompileErrorEvent(lineNr, $"Es sind nur Werte zwischen 1 und 360.000 (1 Stunde) möglich")
            End If

            ' Delay hinzufügen
            Dim progEntry As New ProgramEntry With {
                .func = ProgFunc.delay,
                .lineNr = lineNr,
                .delayTimeMS = delay * 10 ' Hundertstel werden angegeben!
            }
            _progList.Add(progEntry)

            MyBase.EnterDelay(context)
        End Sub
        'WAIT
        Public Overrides Sub EnterWait(<NotNull> context As ACLParser.WaitContext)
            Dim thisIndex As Int32 = _progList.Count
            ' Bedingter Sprung hinzufügen (Bedingung wird bei "EnterCondition" hinzugefügt)
            Dim progEntry As New ProgramEntry With {
                .func = ProgFunc.cjump,
                .lineNr = context.WAIT.Symbol.Line,
                .VKEFirst = True,
                .jumpTrueTarget = thisIndex + 1, ' Weiter bei True
                .jumpFalseTarget = thisIndex ' Auf sich selbst setzen solange false
            }
            _progList.Add(progEntry)

            MyBase.EnterWait(context)
        End Sub

        ' ********************************************************
        ' Variable Definition and Manipulation
        ' ********************************************************
        'DEFINE / GLOBAL
        Public Overrides Sub EnterDefine(<NotNull> context As ACLParser.DefineContext)
            Dim lineNr As Integer = CType(context.GetChild(0), Tree.ITerminalNode).Symbol.Line

            For i = 0 To context.IDENTIFIER.Length - 1
                _defineVar(context.IDENTIFIER(i).GetText(), lineNr)
            Next

            MyBase.EnterDefine(context)
        End Sub
        'DELVAR
        Public Overrides Sub EnterDelvar(<NotNull> context As ACLParser.DelvarContext)
            Dim lineNr As Integer = CType(context.GetChild(0), Tree.ITerminalNode).Symbol.Line
            Dim varName As String = context.IDENTIFIER.GetText()
            'Prüfen ob es diese Variable gibt
            If _variables.ContainsKey(varName) Then
                'Variable entfernen
                _variables.Remove(varName)
                ' DELVAR
                Dim progEntry As New ProgramEntry
                progEntry.func = ProgFunc.delVar
                progEntry.lineNr = lineNr
                progEntry.varName = varName
                _progList.Add(progEntry)
            Else
                If _tcpVars.Exists(varName) Then
                    RaiseEvent CompileErrorEvent(lineNr, "TCP Variablen können nicht im Programm gelöscht werden.")
                Else
                    RaiseEvent CompileErrorEvent(lineNr, $"Variable ""{varName}"" wurde nicht definiert.")
                End If
            End If

            MyBase.EnterDelvar(context)
        End Sub
        'SET
        Public Overrides Sub ExitSetvar(<NotNull> context As ACLParser.SetvarContext)
            Dim lineNr As Integer = context.SET.Symbol.Line
            Dim varName As String = context.IDENTIFIER(0).GetText()
            Dim val As Double = 0
            Dim var As String = Nothing
            Dim calculation As Boolean = False

            ' Prüfen welcher Wert zugewiesen werden soll
            If context.BOOL IsNot Nothing Then
                ' Bool
                val = If(context.BOOL.GetText = "FALSE", 0, 1)
            ElseIf context.calculation IsNot Nothing Then
                ' Calculation
                calculation = True
            Else
                ' Variable oder Integer
                _getVarVal(context.GetChild(3).GetText(), lineNr, val, var)
            End If

            _setVar(varName, lineNr, val, var, calculation)

            MyBase.EnterSetvar(context)
        End Sub

        ' ********************************************************
        ' Position Definition and Manipulation
        ' ********************************************************
        'DEFP
        Public Overrides Sub EnterDefp(<NotNull> context As ACLParser.DefpContext)
            Dim lineNr As Integer = context.DEFP.Symbol.Line
            Dim identifier As String = context.IDENTIFIER.GetText()
            ' Prüfen ob Position schon erstellt wurde
            If _getPosIndex(identifier, lineNr) = -1 Then
                ' Teachpunkt anlegen
                Dim tp As New RuntimeTeachPoint
                tp.identifier = identifier
                tp.tp.nr = -1
                _tp.Add(tp)

                ' defPos erstellen
                Dim progEntry As New ProgramEntry
                progEntry.func = ProgFunc.defPos
                progEntry.lineNr = lineNr
                progEntry.posIdentifer = identifier
                _progList.Add(progEntry)
            Else
                RaiseEvent CompileErrorEvent(lineNr, $"Position ""{identifier}"" wurde bereits definiert.")
            End If

            MyBase.EnterDefp(context)
        End Sub
        'DELP
        Public Overrides Sub EnterDelp(<NotNull> context As ACLParser.DelpContext)
            Dim lineNr As Integer = context.DELP.Symbol.Line
            Dim identifier As String = context.IDENTIFIER.GetText()
            ' Prüfen ob Position schon erstellt wurde
            Dim index As Integer = _getPosIndex(identifier, lineNr)
            If index >= 0 Then
                ' Teachpunkt löschen
                _tp.RemoveAt(index)
                ' delPos erstellen
                Dim progEntry As New ProgramEntry
                progEntry.func = ProgFunc.delPos
                progEntry.lineNr = lineNr
                progEntry.posIdentifer = identifier
                _progList.Add(progEntry)
            Else
                RaiseEvent CompileErrorEvent(lineNr, $"Position ""{identifier}"" wurde nicht definiert.")
            End If

            MyBase.EnterDelp(context)
        End Sub
        'UNDEF
        Public Overrides Sub EnterUndef(<NotNull> context As ACLParser.UndefContext)
            Dim lineNr As Integer = context.UNDEF.Symbol.Line
            Dim identifier As String = context.IDENTIFIER.GetText()
            ' Prüfen ob Position schon erstellt wurde
            Dim index As Integer = _getPosIndex(identifier, lineNr)
            If index >= 0 Then
                ' Teachpunkt initialized wegnehmen
                Dim tp As RuntimeTeachPoint = _tp(index)
                tp.initialized = False
                _tp(index) = tp
                ' undefPos erstellen
                Dim progEntry As New ProgramEntry
                progEntry.func = ProgFunc.undefPos
                progEntry.lineNr = lineNr
                progEntry.posIdentifer = identifier
                _progList.Add(progEntry)
            Else
                RaiseEvent CompileErrorEvent(lineNr, $"Position ""{identifier}"" wurde nicht definiert.")
            End If

            MyBase.EnterUndef(context)
        End Sub
        'HERE
        Public Overrides Sub EnterHere(<NotNull> context As ACLParser.HereContext)
            Dim lineNr As Integer = context.HERE.Symbol.Line
            Dim identifier As String = context.IDENTIFIER.GetText()
            ' Prüfen ob Position schon erstellt wurde
            Dim index As Integer = _getPosIndex(identifier, lineNr)
            If index >= 0 Then
                ' recordPos erstellen
                Dim progEntry As New ProgramEntry
                progEntry.func = ProgFunc.recordPos
                progEntry.lineNr = lineNr
                progEntry.posIdentifer = identifier
                progEntry.posType = False
                _progList.Add(progEntry)
            Else
                RaiseEvent CompileErrorEvent(lineNr, $"Position ""{identifier}"" wurde nicht definiert.")
            End If

            MyBase.EnterHere(context)
        End Sub
        'TEACH
        Public Overrides Sub EnterTeach(<NotNull> context As ACLParser.TeachContext)
            Dim lineNr As Integer = context.TEACH.Symbol.Line
            Dim identifier As String = context.IDENTIFIER.GetText()
            ' Prüfen ob Position schon erstellt wurde
            Dim index As Integer = _getPosIndex(identifier, lineNr)
            If index >= 0 Then
                ' recordPos erstellen
                Dim progEntry As New ProgramEntry
                progEntry.func = ProgFunc.recordPos
                progEntry.lineNr = lineNr
                progEntry.posIdentifer = identifier
                progEntry.posType = True
                _progList.Add(progEntry)
            Else
                RaiseEvent CompileErrorEvent(lineNr, $"Position ""{identifier}"" wurde nicht definiert.")
            End If

            MyBase.EnterTeach(context)
        End Sub
        'SETPV
        Public Overrides Sub EnterSetpv(<NotNull> context As ACLParser.SetpvContext)
            Dim lineNr As Integer = context.SETPV.Symbol.Line
            Dim identifier As String = context.GetChild(1).GetText()
            Dim val As Double = 0
            Dim var As String = Nothing
            Dim axis As Integer = 0
            If Not _getAndCheckInt(context.GetChild(2).GetText(), axis, 1, 6) Then
                RaiseEvent CompileErrorEvent(lineNr, "Achsennummer muss zwischen 1 und 6 liegen")
            End If

            _getVarVal(context.GetChild(3).GetText(), lineNr, val, var)

            ' Prüfen ob Position schon erstellt wurde
            Dim index As Integer = _getPosIndex(identifier, lineNr)
            If index >= 0 Then
                If axis > 0 Then
                    ' changePos erstellen
                    Dim progEntry As New ProgramEntry
                    progEntry.func = ProgFunc.changePos
                    progEntry.lineNr = lineNr
                    progEntry.posIdentifer = identifier
                    progEntry.posType = False
                    progEntry.varValue = val
                    progEntry.varVariable = var
                    progEntry.posAxisOrCoord = axis
                    _progList.Add(progEntry)
                End If
            Else
                RaiseEvent CompileErrorEvent(lineNr, $"{If(IsNumeric(identifier), $"Teachpunkt {identifier}", $"Position ""{identifier}""")} {identifier} wurde nicht definiert.")
            End If

            MyBase.EnterSetpv(context)
        End Sub
        'SETPVC
        Enum enumCoord
            X = 1
            Y
            Z
            yaw
            pitch
            roll
        End Enum
        Public Overrides Sub EnterSetpvc(<NotNull> context As ACLParser.SetpvcContext)
            Dim lineNr As Integer = context.SETPVC.Symbol.Line
            Dim identifier As String = context.GetChild(1).GetText()
            Dim val As Double = 0
            Dim var As String = Nothing
            Dim coord As Integer = 0
            Try
                coord = CType([Enum].Parse(GetType(enumCoord), context.GetChild(2).GetText(), False), enumCoord)
            Catch e As ArgumentException
                RaiseEvent CompileErrorEvent(lineNr, $"Es sind nur die Werte: ""X, Y, Z, yaw, pitch, roll"" gültig.")
            End Try

            _getVarVal(context.GetChild(3).GetText(), lineNr, val, var)

            ' Prüfen ob Position schon erstellt wurde
            Dim index As Integer = _getPosIndex(identifier, lineNr)
            If index >= 0 Then
                If coord > 0 Then
                    ' changePos erstellen
                    Dim progEntry As New ProgramEntry
                    progEntry.func = ProgFunc.changePos
                    progEntry.lineNr = lineNr
                    progEntry.posIdentifer = identifier
                    progEntry.posType = True
                    progEntry.varValue = val
                    progEntry.varVariable = var
                    progEntry.posAxisOrCoord = coord
                    _progList.Add(progEntry)
                End If
            Else
                RaiseEvent CompileErrorEvent(lineNr, $"{If(IsNumeric(identifier), $"Position ""{identifier}""", $"Teachpunkt {identifier}")} wurde nicht definiert.")
            End If

            MyBase.EnterSetpvc(context)
        End Sub
        'SHIFT
        Public Overrides Sub EnterShift(<NotNull> context As ACLParser.ShiftContext)
            Dim lineNr As Integer = context.SHIFT.Symbol.Line
            Dim identifier As String = context.GetChild(1).GetText()
            Dim val As Double = 0
            Dim var As String = Nothing
            Dim axis As Integer = 0
            If Not _getAndCheckInt(context.GetChild(3).GetText(), axis, 1, 6) Then
                RaiseEvent CompileErrorEvent(lineNr, "Achsennummer muss zwischen 1 und 6 liegen")
            End If

            _getVarVal(context.GetChild(4).GetText(), lineNr, val, var)

            ' Prüfen ob Position schon erstellt wurde
            Dim index As Integer = _getPosIndex(identifier, lineNr)
            If index >= 0 Then
                If axis > 0 Then
                    ' changePos erstellen
                    Dim progEntry As New ProgramEntry
                    progEntry.func = ProgFunc.changePos
                    progEntry.lineNr = lineNr
                    progEntry.posShift = True
                    progEntry.posIdentifer = identifier
                    progEntry.posType = False
                    progEntry.varValue = val
                    progEntry.varVariable = var
                    progEntry.posAxisOrCoord = axis
                    _progList.Add(progEntry)
                End If
            Else
                RaiseEvent CompileErrorEvent(lineNr, $"{If(IsNumeric(identifier), $"Teachpunkt {identifier}", $"Position ""{identifier}""")} wurde nicht definiert.")
            End If

            MyBase.EnterShift(context)
        End Sub
        'SHIFTC
        Public Overrides Sub EnterShiftc(<NotNull> context As ACLParser.ShiftcContext)
            Dim lineNr As Integer = context.SHIFTC.Symbol.Line
            Dim identifier As String = context.GetChild(1).GetText()
            Dim val As Double = 0
            Dim var As String = Nothing
            Dim coord As Integer = 0
            Try
                coord = CType([Enum].Parse(GetType(enumCoord), context.GetChild(3).GetText(), False), enumCoord)
            Catch e As ArgumentException
                RaiseEvent CompileErrorEvent(lineNr, $"Es sind nur die Werte: ""X, Y, Z, yaw, pitch, roll"" gültig.")
            End Try

            _getVarVal(context.GetChild(4).GetText(), lineNr, val, var)

            ' Prüfen ob Position schon erstellt wurde
            Dim index As Integer = _getPosIndex(identifier, lineNr)
            If index >= 0 Then
                If coord > 0 Then
                    ' changePos erstellen
                    Dim progEntry As New ProgramEntry
                    progEntry.func = ProgFunc.changePos
                    progEntry.lineNr = lineNr
                    progEntry.posShift = True
                    progEntry.posIdentifer = identifier
                    progEntry.posType = True
                    progEntry.varValue = val
                    progEntry.varVariable = var
                    progEntry.posAxisOrCoord = coord
                    _progList.Add(progEntry)
                End If
            Else
                RaiseEvent CompileErrorEvent(lineNr, $"{If(IsNumeric(identifier), $"Teachpunkt {identifier}", $"Position ""{identifier}""")} wurde nicht definiert.")
            End If

            MyBase.EnterShiftc(context)
        End Sub
        'SETP
        Public Overrides Sub EnterSetp(<NotNull> context As ACLParser.SetpContext)
            Dim lineNr As Integer = context.SETP.Symbol.Line
            Dim identifier1 As String = context.IDENTIFIER(0).GetText
            Dim identifier2 As String = context.GetChild(3).GetText
            ' Prüfen ob Positionen existieren
            Dim index As Integer = _getPosIndex(identifier1, lineNr)
            If index = -1 Then
                RaiseEvent CompileErrorEvent(lineNr, $"Position ""{identifier1}"" wurde nicht definiert.")
            End If
            index = _getPosIndex(identifier2, lineNr)
            If index = -1 Then
                RaiseEvent CompileErrorEvent(lineNr, $"{If(IsNumeric(identifier2), $"Teachpunkt {identifier2}", $"Position ""{identifier2}""")} wurde nicht definiert.")
            End If
            ' copyPos erstellen
            Dim progEntry As New ProgramEntry
            progEntry.func = ProgFunc.copyPos
            progEntry.posIdentifer = identifier1
            progEntry.posCopyPos = identifier2
            _progList.Add(progEntry)

            MyBase.EnterSetp(context)
        End Sub
        'SET
        Public Overrides Sub EnterSetpos(<NotNull> context As ACLParser.SetposContext)
            Dim lineNr As Integer = context.SET.Symbol.Line
            ' Variable prüfen
            Dim varName As String = context.IDENTIFIER(0).GetText
            If Not _checkVar(varName) Then
                RaiseEvent CompileErrorEvent(lineNr, $"Variable ""{varName}"" wurde nicht definiert")
            End If
            ' Position/Teachpunkt prüfen
            Dim pos As String = context.GetChild(4).GetText
            Dim index As Integer = _getPosIndex(pos, lineNr)
            If index >= 0 Then
                Dim progEntry As New ProgramEntry
                progEntry.func = ProgFunc.setVarToPosition
                progEntry.lineNr = lineNr
                progEntry.varName = varName
                If IsNumeric(pos) Then
                    progEntry.posNum = CInt(pos)
                Else
                    progEntry.posIdentifer = pos
                End If
                'Function
                If context.PVAL IsNot Nothing Then
                    progEntry.varSetVarToPosFunc = VarToPosFunc.joint
                    If Not _getAndCheckInt(context.GetChild(5).GetText(), progEntry.posAxisOrCoord, 1, 6) Then
                        RaiseEvent CompileErrorEvent(lineNr, "Achsennummer muss zwischen 1 und 6 liegen")
                    End If
                ElseIf context.PVALC IsNot Nothing Then
                    progEntry.varSetVarToPosFunc = VarToPosFunc.cart
                    Try
                        progEntry.posAxisOrCoord = CType([Enum].Parse(GetType(enumCoord), context.GetChild(5).GetText(), False), enumCoord)
                    Catch e As ArgumentException
                        RaiseEvent CompileErrorEvent(lineNr, $"Es sind nur die Werte: ""X, Y, Z, yaw, pitch, roll"" gültig.")
                    End Try
                ElseIf context.PSTATUS IsNot Nothing Then
                    progEntry.varSetVarToPosFunc = VarToPosFunc.type
                End If
                _progList.Add(progEntry)
            Else
                RaiseEvent CompileErrorEvent(lineNr, $"{If(IsNumeric(pos), $"Teachpunkt {pos}", $"Position ""{pos}""")} wurde nicht definiert.")
            End If

            MyBase.EnterSetpos(context)
        End Sub

        ' ********************************************************
        ' User Interface
        ' ********************************************************
        'PRINT
        Public Overrides Sub EnterPrint(<NotNull> context As ACLParser.PrintContext)
            Dim lineNr As Integer = context.PRINT.Symbol.Line
            ' PRINT hinzufügen
            Dim progEntry As New ProgramEntry
            progEntry.func = ProgFunc.print
            progEntry.lineNr = lineNr
            progEntry.printVal = New List(Of PrintVal)
            For i = 1 To context.ChildCount - 1
                Dim pv As PrintVal
                pv.val = context.GetChild(i).GetText
                ' Prüfen ob Variable oder String
                If pv.val.StartsWith(""""c) Then
                    pv.val = pv.val.Substring(1, pv.val.Length - 2) ' "" entfernen
                    pv.isVar = False
                Else
                    If Not _checkVar(pv.val) Then
                        RaiseEvent CompileErrorEvent(lineNr, $"Variable ""{pv.val}"" wurde nicht definiert")
                    Else
                        pv.isVar = True
                    End If
                End If
                progEntry.printVal.Add(pv)
            Next
            _progList.Add(progEntry)

            MyBase.EnterPrint(context)
        End Sub

        'Ende
        Public Overrides Sub ExitRoot(<NotNull> context As ACLParser.RootContext)
            If _gotos.Count > 0 Then
                For Each item As KeyValuePair(Of String, List(Of Integer)) In _gotos
                    RaiseEvent CompileErrorEvent(_progList(item.Value(0)).lineNr, $"Label ""{item.Key}"" wurde nicht definiert")
                Next
            End If

            MyBase.ExitRoot(context)
        End Sub


        'Private
        Private Function _defineVar(name As String, lineNr As Integer) As Boolean
            'Prüfen ob es diese Variable schon gibt
            If _variables.ContainsKey(name) Then
                RaiseEvent CompileErrorEvent(lineNr, $"Variable ""{name}"" wurde schon In Zeile {_variables(name).defLine} definiert")
                    Return False
            ElseIf _tcpVars.Exists(name) Then
                RaiseEvent CompileErrorEvent(lineNr, $"Variable ""{name}"" wurde bereits als TCP-Variable definiert")
                Return False
            Else
                ' Variable hinzufügen
                _variables.Add(name, New Variable(lineNr))
                ' DEFVAR hinzufügen
                Dim progEntry As New ProgramEntry
                progEntry.func = ProgFunc.defVar
                progEntry.lineNr = lineNr
                progEntry.varName = name
                _progList.Add(progEntry)
                Return True
            End If
        End Function

        Private Function _setVar(name As String, lineNr As Integer, val As Double, var As String, calculation As Boolean) As Boolean
            If _checkVar(name) Then
                Dim progEntry As New ProgramEntry With {
                    .lineNr = lineNr,
                    .varName = name,
                    .func = ProgFunc.setVar,
                    .varVariable = var,
                    .varValue = val
                }
                ' Prüfen ob Berechnung
                If calculation Then
                    progEntry.func = ProgFunc.setVarToBuffer
                End If
                _progList.Add(progEntry)
            Else
                RaiseEvent CompileErrorEvent(lineNr, $"Variable ""{name}"" wurde nicht definiert")
                Return False
            End If
            Return True
        End Function

        Private Function _checkVar(name As String) As Boolean
            Return _variables.ContainsKey(name) Or _tcpVars.Exists(name)
        End Function

        Private Function _getPosIndex(identifier As String, lineNr As Integer) As Integer
            If IsNumeric(identifier) Then
                Try
                    Return _tp.FindIndex(Function(_tp As RuntimeTeachPoint) _tp.tp.nr = CInt(identifier))
                Catch e As OverflowException
                    RaiseEvent CompileErrorEvent(lineNr, $"Es sind nur Werte zwischen {-2 ^ 31} und {2 ^ 31 - 1} möglich (32-Bit Integer)")
                    Return -1
                End Try
            Else
                Return _tp.FindIndex(Function(_tp As RuntimeTeachPoint) _tp.identifier = identifier)
            End If
        End Function

        Private Sub _getVarVal(ByVal text As String, ByVal lineNr As Integer, ByRef val As Double, ByRef var As String)
            If IsNumeric(text) Then
                ' Integer or Double
                val = _getDouble(text)
                var = Nothing
            Else
                ' Variable
                var = text
                If Not _checkVar(var) Then
                    RaiseEvent CompileErrorEvent(lineNr, $"Variable ""{var}"" wurde nicht definiert")
                End If
            End If
        End Sub

        Private Function _getDouble(ByVal text As String) As Double
            Return CDbl(text.Replace(".", ","))
        End Function

        Private Function _getAndCheckInt(ByVal text As String, ByRef val As Integer, Optional ByVal min As Integer = [Int32].MinValue, Optional ByVal max As Integer = [Int32].MaxValue) As Boolean
            Dim tmpVal As Integer
            Try
                tmpVal = CInt(text)
            Catch e As OverflowException
                Return False
            End Try
            If tmpVal > max Or tmpVal < min Then
                Return False
            Else
                val = tmpVal
            End If
            Return True
        End Function

    End Class
#End Region

End Class