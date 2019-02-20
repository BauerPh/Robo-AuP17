Option Strict On

Imports RoboAUP17.Matrizen

Friend Class Kinematics
    Private _DHParameter(5) As DHParams
    Private _transForwMatr(7) As Matrix4x4
    Private _workframe, _toolframe As CartCoords

#Region "Properties"
    Public Property Workframe As CartCoords
        Get
            Return _workframe
        End Get
        Set(value As CartCoords)
            _workframe = value
        End Set
    End Property

    Public Property Toolframe As CartCoords
        Get
            Return _toolframe
        End Get
        Set(value As CartCoords)
            _toolframe = value
        End Set
    End Property
#End Region

#Region "Constructor"
    ' -----------------------------------------------------------------------------
    ' Constructor
    ' -----------------------------------------------------------------------------
    Public Sub New(DenavitHartenbergParameter As DHParams())
        If UBound(DenavitHartenbergParameter) <> 5 Then
            Throw New Exception("Denavit Hartenberg Parameter für alle 6 Achsen erforderlich!")
        End If
        _DHParameter = DenavitHartenbergParameter
        For i As Int16 = 0 To 7
            _transForwMatr(i) = New Matrix4x4
        Next
    End Sub
#End Region

#Region "Public"
    ' -----------------------------------------------------------------------------
    ' Public
    ' -----------------------------------------------------------------------------
    Public Function ForwardKin(joints As JointAngles) As CartCoords
        'Erstmal die Transformationsmatritzen für jede einzelne Achse berechnen
        'Work Frame
        _transForwMatr(0) = CalcFrameMatrix(_workframe)
        'Transformationsmatrizen jeder Achse
        For i As Short = 1 To 6
            _transForwMatr(i) = CalcDHTransMatrix(i, joints.ToArray(i - 1))
        Next
        'Tool Frame
        _transForwMatr(7) = CalcFrameMatrix(_toolframe)

        'Matritzen multiplizieren
        Dim tmpMatr As Matrix4x4
        tmpMatr = _transForwMatr(1) * _transForwMatr(0)
        For i As Int16 = 2 To 7
            tmpMatr = _transForwMatr(i) * tmpMatr
        Next

        'Ausgeben
        Dim erg As CartCoords
        'Position
        erg.X = tmpMatr.val(0, 3)
        erg.Y = tmpMatr.val(1, 3)
        erg.Z = tmpMatr.val(2, 3)
        'Ausrichtung
        Dim tmpPitch As Double = Math.Atan2(Math.Sqrt((tmpMatr.val(0, 2) ^ 2) + (tmpMatr.val(1, 2) ^ 2)), tmpMatr.val(2, 2) * -1)
        erg.yaw = ToDEG(Math.Atan2(tmpMatr.val(2, 0) / tmpPitch, tmpMatr.val(2, 1) / tmpPitch))
        erg.pitch = ToDEG(tmpPitch)
        erg.roll = ToDEG(Math.Atan2(tmpMatr.val(0, 2) / tmpPitch, tmpMatr.val(1, 2) / tmpPitch))

        Return erg
    End Function

    Public Function InversKin(coords As CartCoords) As JointAngles

        'Hier inverse Kinematik berechnen


        Return New JointAngles
    End Function
#End Region

#Region "Private"
    ' -----------------------------------------------------------------------------
    ' Private
    ' -----------------------------------------------------------------------------
    Private Function CalcDHTransMatrix(joint As Int16, theta As Double) As Matrix4x4 'Theta = Winkel einer Achse
        Dim dh As DHParams = _DHParameter(joint - 1)
        Dim thetaBog As Double = ToRAD(theta)
        Dim erg As New Matrix4x4

        dh.alpha = ToRAD(dh.alpha)

        erg.val(0, 0) = Math.Cos(thetaBog)
        erg.val(0, 1) = -Math.Sin(thetaBog) * Math.Cos(dh.alpha)
        erg.val(0, 2) = Math.Sin(thetaBog) * Math.Sin(dh.alpha)
        erg.val(0, 3) = dh.a * Math.Cos(thetaBog)

        erg.val(1, 0) = Math.Sin(thetaBog)
        erg.val(1, 1) = Math.Cos(thetaBog) * Math.Cos(dh.alpha)
        erg.val(1, 2) = -Math.Cos(thetaBog) * Math.Sin(dh.alpha)
        erg.val(1, 3) = dh.a * Math.Sin(thetaBog)

        erg.val(2, 0) = 0.0
        erg.val(2, 1) = Math.Sin(dh.alpha)
        erg.val(2, 2) = Math.Cos(dh.alpha)
        erg.val(2, 3) = dh.d

        erg.val(3, 0) = 0.0
        erg.val(3, 1) = 0.0
        erg.val(3, 2) = 0.0
        erg.val(3, 3) = 1.0

        Return erg
    End Function

    Private Function CalcFrameMatrix(frame As CartCoords) As Matrix4x4
        Dim Matrix As New Matrix4x4
        Dim yaw As Double = ToRAD(frame.yaw)
        Dim pitch As Double = ToRAD(frame.pitch)
        Dim roll As Double = ToRAD(frame.roll)

        Matrix.val(0, 0) = Math.Cos(roll) * Math.Cos(pitch)
        Matrix.val(0, 1) = -Math.Sin(roll) * Math.Cos(yaw) + Math.Cos(roll) * Math.Sin(pitch) * Math.Sin(yaw)
        Matrix.val(0, 2) = Math.Sin(roll) * Math.Sin(yaw) + Math.Cos(roll) * Math.Sin(pitch) * Math.Cos(yaw)
        Matrix.val(0, 3) = frame.X

        Matrix.val(1, 0) = Math.Sin(roll) * Math.Cos(pitch)
        Matrix.val(1, 1) = Math.Cos(roll) * Math.Cos(yaw) + Math.Sin(roll) * Math.Sin(pitch) * Math.Sin(yaw)
        Matrix.val(1, 2) = -Math.Cos(roll) * Math.Sin(yaw) + Math.Sin(roll) * Math.Sin(pitch) * Math.Cos(yaw)
        Matrix.val(1, 3) = frame.Y

        Matrix.val(2, 0) = -Math.Sin(roll)
        Matrix.val(2, 1) = Math.Cos(pitch) * Math.Sin(yaw)
        Matrix.val(2, 2) = Math.Cos(pitch) * Math.Cos(yaw)
        Matrix.val(2, 3) = frame.Z

        Matrix.val(3, 0) = 0.0
        Matrix.val(3, 1) = 0.0
        Matrix.val(3, 2) = 0.0
        Matrix.val(3, 3) = 1.0

        Return Matrix
    End Function

    Private Function ToRAD(winkel As Double) As Double
        Return winkel * (Math.PI / 180)
    End Function

    Private Function ToDEG(winkel As Double) As Double
        Return winkel * (180.0 / Math.PI)
    End Function
#End Region

End Class

