Imports System.ComponentModel
Module dataStructs
    ' -----------------------------------------------------------------------------
    ' Kinematics and Coordinates
    ' -----------------------------------------------------------------------------
    Public Structure DHParams
        Public alpha, d, a As Double
        Public Sub New(alpha As Double, d As Double, a As Double)
            Me.alpha = alpha
            Me.d = d
            Me.a = a
        End Sub
    End Structure

    Public Structure CartCoords
        Public X, Y, Z, yaw, pitch, roll As Double
        Public Sub New(X As Double, Y As Double, Z As Double, yaw As Double, pitch As Double, roll As Double)
            Me.X = X
            Me.Y = Y
            Me.Z = Z
            Me.yaw = yaw
            Me.pitch = pitch
            Me.roll = roll
        End Sub
        Public Function ToArray() As Double()
            Return New Double(5) {X, Y, Z, yaw, pitch, roll}
        End Function
    End Structure

    Public Structure JointAngles
        Public J1, J2, J3, J4, J5, J6 As Double
        Public Sub New(J1 As Double, J2 As Double, J3 As Double, J4 As Double, J5 As Double, J6 As Double)
            Me.J1 = J1
            Me.J2 = J2
            Me.J3 = J3
            Me.J4 = J4
            Me.J5 = J5
            Me.J6 = J6
        End Sub
        Public Function ToArray() As Double()
            Return New Double(5) {J1, J2, J3, J4, J5, J6}
        End Function
    End Structure

    ' -----------------------------------------------------------------------------
    ' Settings
    ' -----------------------------------------------------------------------------
    Public Enum motMode
        FULL = 0
        HALF
        MIC_4
        MIC_8
        MIC_16
    End Enum
    Public Enum motDir
        normal = 0
        invertiert
    End Enum
    Public Enum calDir
        min = 0
        max
    End Enum
    Public Structure JointParameter
        <Category("Motor"),
            DisplayName("Schritte pro Umdrehung"),
            Description("Anzahl Vollschritte pro Motorumdrehung. Diese Angabe finden Sie im Datenblatt des Motors.")>
        Public Property motStepsPerRot As Int32
        <Category("Motor"),
            DisplayName("Getriebeübersetzung"),
            Description("Diese Angabe finden Sie im Datenblatt des Motors oder des Getriebes.")>
        Public Property motGear As Double
        <Category("Motor"),
            DisplayName("Betriebsart"),
            Description("Vollschritt, Halbschritt, ...")>
        Public Property motMode As motMode
        <Category("Motor"),
            DisplayName("Drehrichtung")>
        Public Property motDir As motDir
        <Category("Mechanik"),
            DisplayName("Getriebeübersetzung")>
        Public Property mechGear As Double
        <Category("Mechanik"),
            DisplayName("minimaler Winkel"),
            Description("der kleinste mögliche Winkel dieser Achse")>
        Public Property mechMinAngle As Double
        <Category("Mechanik"),
            DisplayName("maximaler Winkel"),
            Description("der größte mögliche Winkel dieser Achse")>
        Public Property mechMaxAngle As Double
        <Category("Mechanik"),
            DisplayName("Homeposition"),
            Description("Winkel der für die Homeposition angefahren werden soll.")>
        Public Property mechHomePosAngle As Double
        <Category("Mechanik"),
            DisplayName("Parkposition"),
            Description("Winkel der für die Parkposition angefahren werden soll.")>
        Public Property mechParkPosAngle As Double
        <Category("Referenzfahrt"),
            DisplayName("Richtung"),
            Description("In welcher Richtung der Endschalter sitzt.")>
        Public Property calDir As calDir
        <Category("Referenzfahrt"),
            DisplayName("Suchgeschwindigkeit"),
            Description("Geschwindigkeit mit welcher der Endschalter gesucht wird (schnell).")>
        Public Property calSpeedFast As Double
        <Category("Referenzfahrt"),
            DisplayName("Referenziergeschwindigkeit"),
            Description("Geschwindigkeit mit welcher der Endschalter für den Referenzpunkt angefahren wird (langsam).")>
        Public Property calSpeedSlow As Double
        <Category("Referenzfahrt"),
            DisplayName("Beschleunigung"),
            Description("Beschleunigung während einer Referenzfahrt")>
        Public Property calAcc As Double
        <Category("Fahrprofil"),
            DisplayName("maximale Geschwindigkeit"),
            Description("die maximal mögliche Geschwindigkeit dieser Achse")>
        Public Property profileMaxSpeed As Double
        <Category("Fahrprofil"),
            DisplayName("maximale Beschleunigung"),
            Description("die maximal mögliche Beschleunigung dieser Achse")>
        Public Property profileMaxAcc As Double
        <Category("Fahrprofil"),
            DisplayName("Notstop Beschleunigung"),
            Description("Beschleunigung welche bei einem Notstopp zum Anhalten verwendet werden soll.")>
        Public Property profileStopAcc As Double
    End Structure

    Public Structure ServoParameter
        Public Property minAngle As Double
        Public Property maxAngle As Double
    End Structure

End Module
