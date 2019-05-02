Imports Antlr4.Runtime
Imports Antlr4.Runtime.Misc
Imports ACLLexerParser
Friend Class ACLCompiler
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