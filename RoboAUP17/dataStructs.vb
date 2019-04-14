Imports System.ComponentModel
Module dataStructs
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
        Private _x, _y, _z, _yaw, _pitch, _roll As Double
        Friend Sub New(X As Double, Y As Double, Z As Double, yaw As Double, pitch As Double, roll As Double)
            Me.X = X
            Me.Y = Y
            Me.Z = Z
            Me.Yaw = yaw
            Me.Pitch = pitch
            Me.Roll = roll
        End Sub
        Public Property X As Double
            Get
                Return _x
            End Get
            Set(value As Double)
                _x = value
            End Set
        End Property
        Public Property Y As Double
            Get
                Return _y
            End Get
            Set(value As Double)
                _y = value
            End Set
        End Property
        Public Property Z As Double
            Get
                Return _z
            End Get
            Set(value As Double)
                _z = value
            End Set
        End Property
        Public Property Yaw As Double
            Get
                Return _yaw
            End Get
            Set(value As Double)
                _yaw = value
            End Set
        End Property
        Public Property Pitch As Double
            Get
                Return _pitch
            End Get
            Set(value As Double)
                _pitch = value
            End Set
        End Property
        Public Property Roll As Double
            Get
                Return _roll
            End Get
            Set(value As Double)
                _roll = value
            End Set
        End Property
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
    End Structure

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

    ' -----------------------------------------------------------------------------
    ' ACL
    ' -----------------------------------------------------------------------------
    Friend Structure TeachPoint
        Implements IComparable(Of TeachPoint)

        Friend nr As Int32
        Friend name As String
        Friend cart As Boolean
        Friend jointAngles As JointAngles
        Friend tcpCoords As CartCoords

        Friend Function CompareTo(other As TeachPoint) As Integer _
            Implements IComparable(Of TeachPoint).CompareTo
            ' A null value means that this object is greater.
            Return nr.CompareTo(other.nr)
        End Function

        Public Overrides Function ToString() As String
            If cart Then
                Return $"{nr}: {name} (X: {tcpCoords.X}; Y: {tcpCoords.Y}; Z: {tcpCoords.Z}; yaw: {tcpCoords.Yaw}; pitch: {tcpCoords.Pitch}; roll: {tcpCoords.Roll})"
            Else
                Return $"{nr}: {name} (J1: {jointAngles.J1}; J2: {jointAngles.J2}; J3: {jointAngles.J3}; J4: {jointAngles.J4}; J5: {jointAngles.J5}; J6: {jointAngles.J6})"
            End If
        End Function
    End Structure

End Module
