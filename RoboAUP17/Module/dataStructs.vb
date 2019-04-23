Imports System.ComponentModel
Module dataStructs
#Region "Kinematik und Koordinaten"
    ' -----------------------------------------------------------------------------
    ' Kinematics and Coordinates
    ' -----------------------------------------------------------------------------
    Friend Structure DHParameter
        <Category("Denavit-Hartenberg-Parameter")>
        Public Property alpha As Double
        <Category("Denavit-Hartenberg-Parameter")>
        Public Property d As Double
        <Category("Denavit-Hartenberg-Parameter")>
        Public Property a As Double
    End Structure

    Friend Structure CartCoords
        Friend X, Y, Z, Yaw, Pitch, Roll As Double
        Friend Sub New(X As Double, Y As Double, Z As Double, yaw As Double, pitch As Double, roll As Double)
            Me.X = X
            Me.Y = Y
            Me.Z = Z
            Me.Yaw = yaw
            Me.Pitch = pitch
            Me.Roll = roll
        End Sub
        Friend Function Items(index As Int32) As Double
            Select Case index
                Case 0
                    Return X
                Case 1
                    Return Y
                Case 2
                    Return Z
                Case 3
                    Return Yaw
                Case 4
                    Return Pitch
                Case 5
                    Return Roll
                Case Else
                    Throw New Exception("Index out of range")
            End Select
        End Function
        Friend Sub SetByIndex(index As Integer, value As Double)
            Select Case index
                Case 0
                    X = value
                Case 1
                    Y = value
                Case 2
                    Z = value
                Case 3
                    Yaw = value
                Case 4
                    Pitch = value
                Case 5
                    Roll = value
                Case Else
                    Throw New Exception("Index out of range")
            End Select
        End Sub

        Friend Function Round(decPlaces As Integer) As CartCoords
            Dim coords As CartCoords
            For i = 0 To 5
                coords.SetByIndex(i, Math.Round(Items(i), decPlaces))
            Next
            Return coords
        End Function

        Public Shared Operator +(ByVal coords1 As CartCoords, ByVal coords2 As CartCoords) As CartCoords
            Dim erg As CartCoords
            For i = 0 To 5
                erg.SetByIndex(i, coords1.Items(i) + coords2.Items(i))
            Next
            Return erg
        End Operator
        Public Shared Operator -(ByVal coords1 As CartCoords, ByVal coords2 As CartCoords) As CartCoords
            Dim erg As CartCoords
            For i = 0 To 5
                erg.SetByIndex(i, coords1.Items(i) - coords2.Items(i))
            Next
            Return erg
        End Operator

    End Structure

    Friend Structure JointAngles
        Implements ICloneable
        Friend J1, J2, J3, J4, J5, J6 As Double
        Friend Sub New(J1 As Double, J2 As Double, J3 As Double, J4 As Double, J5 As Double, J6 As Double)
            Me.J1 = J1
            Me.J2 = J2
            Me.J3 = J3
            Me.J4 = J4
            Me.J5 = J5
            Me.J6 = J6
        End Sub
        Friend Function Items(index As Int32) As Double
            Select Case index
                Case 0
                    Return J1
                Case 1
                    Return J2
                Case 2
                    Return J3
                Case 3
                    Return J4
                Case 4
                    Return J5
                Case 5
                    Return J6
                Case Else
                    Throw New Exception("Index out of range")
            End Select
        End Function
        Friend Sub SetByIndex(index As Integer, value As Double)
            Select Case index
                Case 0
                    J1 = value
                Case 1
                    J2 = value
                Case 2
                    J3 = value
                Case 3
                    J4 = value
                Case 4
                    J5 = value
                Case 5
                    J6 = value
                Case Else
                    Throw New Exception("Index out of range")
            End Select
        End Sub
        Friend Function Clone() As Object Implements ICloneable.Clone
            Return New JointAngles(J1, J2, J3, J4, J5, J6)
        End Function

        Public Shared Operator +(ByVal coords1 As JointAngles, ByVal coords2 As JointAngles) As JointAngles
            Dim erg As JointAngles
            For i = 0 To 5
                erg.SetByIndex(i, coords1.Items(i) + coords2.Items(i))
            Next
            Return erg
        End Operator
        Public Shared Operator -(ByVal coords1 As JointAngles, ByVal coords2 As JointAngles) As JointAngles
            Dim erg As JointAngles
            For i = 0 To 5
                erg.SetByIndex(i, coords1.Items(i) - coords2.Items(i))
            Next
            Return erg
        End Operator
    End Structure
#End Region

#Region "Settings"
    ' -----------------------------------------------------------------------------
    ' Settings
    ' -----------------------------------------------------------------------------
    Friend Enum MotMode
        FULL = 0
        HALF
        MIC_4
        MIC_8
        MIC_16
    End Enum
    Friend Enum MotDir
        normal = 0
        invertiert
    End Enum
    Friend Enum CalDir
        min = 0
        max
    End Enum
    Friend Structure JointParameter
        <Category("Motor"),
            DisplayName("Schritte pro Umdrehung"),
            Description("Anzahl Vollschritte pro Motorumdrehung. Diese Angabe finden Sie im Datenblatt des Motors.")>
        Public Property MotStepsPerRot As Int32
        <Category("Motor"),
            DisplayName("Getriebeübersetzung"),
            Description("Diese Angabe finden Sie im Datenblatt des Motors oder des Getriebes.")>
        Public Property MotGear As Double
        <Category("Motor"),
            DisplayName("Betriebsart"),
            Description("Vollschritt, Halbschritt, ...")>
        Public Property MotMode As MotMode
        <Category("Motor"),
            DisplayName("Drehrichtung")>
        Public Property MotDir As MotDir
        <Category("Mechanik"),
            DisplayName("Getriebeübersetzung")>
        Public Property MechGear As Double
        <Category("Mechanik"),
            DisplayName("minimaler Winkel"),
            Description("der kleinste mögliche Winkel dieser Achse")>
        Public Property MechMinAngle As Double
        <Category("Mechanik"),
            DisplayName("maximaler Winkel"),
            Description("der größte mögliche Winkel dieser Achse")>
        Public Property MechMaxAngle As Double
        <Category("Mechanik"),
            DisplayName("Homeposition"),
            Description("Winkel der für die Homeposition angefahren werden soll.")>
        Public Property MechHomePosAngle As Double
        <Category("Mechanik"),
            DisplayName("Parkposition"),
            Description("Winkel der für die Parkposition angefahren werden soll.")>
        Public Property MechParkPosAngle As Double
        <Category("Referenzfahrt"),
            DisplayName("Richtung"),
            Description("In welcher Richtung der Endschalter sitzt.")>
        Public Property CalDir As CalDir
        <Category("Referenzfahrt"),
            DisplayName("Suchgeschwindigkeit"),
            Description("Geschwindigkeit mit welcher der Endschalter gesucht wird (schnell).")>
        Public Property CalSpeedFast As Double
        <Category("Referenzfahrt"),
            DisplayName("Referenziergeschwindigkeit"),
            Description("Geschwindigkeit mit welcher der Endschalter für den Referenzpunkt angefahren wird (langsam).")>
        Public Property CalSpeedSlow As Double
        <Category("Referenzfahrt"),
            DisplayName("Beschleunigung"),
            Description("Beschleunigung während einer Referenzfahrt")>
        Public Property CalAcc As Double
        <Category("Fahrprofil"),
            DisplayName("maximale Geschwindigkeit"),
            Description("die maximal mögliche Geschwindigkeit dieser Achse")>
        Public Property ProfileMaxSpeed As Double
        <Category("Fahrprofil"),
            DisplayName("maximale Beschleunigung"),
            Description("die maximal mögliche Beschleunigung dieser Achse")>
        Public Property ProfileMaxAcc As Double
        <Category("Fahrprofil"),
            DisplayName("Notstop Beschleunigung"),
            Description("Beschleunigung welche bei einem Notstopp zum Anhalten verwendet werden soll.")>
        Public Property ProfileStopAcc As Double
    End Structure

    Friend Structure ServoParameter
        <Category("Allgemein"),
            DisplayName("Angeschlossen")>
        Public Property Available As Boolean
        <Category("Winkel"),
            DisplayName("Minimaler Winkel")>
        Public Property MinAngle As Double
        <Category("Winkel"),
            DisplayName("Maximaler Winkel")>
        Public Property MaxAngle As Double
    End Structure

    Friend Structure TCPServerParameter
        <Category("Allgemein"),
            DisplayName("Aktiv")>
        Public Property Listen As Boolean
        <Category("Allgemein"),
            DisplayName("Port")>
        Public Property Port As Integer
    End Structure
#End Region

#Region "ACL"
    ' -----------------------------------------------------------------------------
    ' ACL
    ' -----------------------------------------------------------------------------
    Friend Structure TeachPoint
        Implements IComparable(Of TeachPoint)
        Implements ICloneable

        Friend nr As Int32
        Friend name As String
        Friend type As Boolean 'True = Kartesisch; False = JointAngles
        Friend jointAngles As JointAngles
        Friend cartCoords As CartCoords

        Friend Function CompareTo(other As TeachPoint) As Integer _
            Implements IComparable(Of TeachPoint).CompareTo
            ' A null value means that this object is greater.
            Return nr.CompareTo(other.nr)
        End Function

        Public Overrides Function ToString() As String
            If type Then
                Return $"{nr}: {name} (X: {cartCoords.X}; Y: {cartCoords.Y}; Z: {cartCoords.Z}; yaw: {cartCoords.Yaw}; pitch: {cartCoords.Pitch}; roll: {cartCoords.Roll})"
            Else
                Return $"{nr}: {name} (J1: {jointAngles.J1}; J2: {jointAngles.J2}; J3: {jointAngles.J3}; J4: {jointAngles.J4}; J5: {jointAngles.J5}; J6: {jointAngles.J6})"
            End If
        End Function

        Friend Function Clone() As Object Implements ICloneable.Clone
            Return New TeachPoint With {
                .nr = nr,
                .name = name,
                .type = type,
                .jointAngles = jointAngles,
                .cartCoords = cartCoords
            }
        End Function

        Friend Function GetRuntimeTeachPoint() As RuntimeTeachPoint
            Return New RuntimeTeachPoint With {
                .tp = CType(Clone(), TeachPoint),
                .initialized = True,
                .identifier = Nothing
            }
        End Function
    End Structure

    Friend Structure RuntimeTeachPoint
        Implements ICloneable
        Friend tp As TeachPoint
        Friend initialized As Boolean
        Friend identifier As String

        Friend Function Clone() As Object Implements ICloneable.Clone
            Return New RuntimeTeachPoint With {
                .tp = CType(tp.Clone(), TeachPoint),
                .initialized = initialized,
                .identifier = identifier
            }
        End Function
    End Structure

    Friend Enum varType
        int = 0
    End Enum
    Friend Structure Variable
        Friend type As varType
        Friend intVal As Int32
        Friend defLine As Int32

        Friend Sub New(type As varType, defLine As Int32)
            Me.type = type
            Me.defLine = defLine
        End Sub
    End Structure

    Friend Enum progFunc
        noop = 0
        move
        servoMove
        delay
        cjump
        jump
        condition
        calculation
        defVar
        delVar
        setVar
        setVarToBuffer
        setVarToPosition
        defPos
        delPos
        undefPos
        recordPos
        changePos
        copyPos
    End Enum
    Friend Enum progCompOperator
        equal = 0
        greater
        less
        greaterOrEqual
        lessOrEqual
        notEqual
    End Enum
    Friend Enum progBoolOperator
        [nothing] = 0
        [and]
        [or]
    End Enum
    Friend Enum progMathOperator
        plus = 0
        minus
        mult
        div
        exp
        [mod]
    End Enum
    Friend Structure ProgramEntry
        Friend lineNr As Int32
        Friend func As progFunc
        ' Axis Control
        Friend moveTpNr As Int32
        Friend moveTpIdentifier As String
        Friend moveSpeed As Double
        Friend moveAcc As Double
        Friend moveSync As Boolean
        ' Servo
        Friend servoNum As Int32
        Friend servoVal As Int32
        ' Delay
        Friend delayTimeMS As Int32
        ' Bedingungen und Berechnungen
        Friend calcVar1 As String
        Friend calcVar2 As String
        Friend calcVal1 As Int32
        Friend calcVal2 As Int32
        Friend calcCompareOp As progCompOperator
        Friend calcBoolOp As progBoolOperator
        Friend calcMathOp As progMathOperator
        Friend VKEFirst As Boolean
        ' Jumps
        Friend jumpTarget As Int32
        Friend jumpTrueTarget As Int32
        Friend jumpFalseTarget As Int32
        ' Variablen
        Friend varName As String
        Friend varValue As Int32
        Friend varVariable As String
        ' Positionen
        Friend posIdentifer As String
        Friend posNum As Integer
        Friend posType As Boolean
        Friend posChangeAxisOrCoord As Int32 ' X = 1, Y = 2, Z = 3, yaw = 4, pitch = 5, roll = 6
        Friend posShift As Boolean
        Friend posCopyPos As String
    End Structure
#End Region
End Module
