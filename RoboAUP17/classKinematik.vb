Imports RoboAUP17.Matrizen

Public Class classKinematik
    Private _DHParameter(5) As DHParams
    Private _transForwMatr(7) As Matrix4x4
    Private _workframe, _toolframe As CartCoords

#Region "DataStructures"
    ' -----------------------------------------------------------------------------
    ' Data Structures
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
        Public Sub New(X As Double, y As Double, Z As Double, yaw As Double, pitch As Double, roll As Double)
            Me.X = X
            Me.Y = y
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
#End Region

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
        _transForwMatr(0).val(0, 0) = 1
        _transForwMatr(0).val(1, 1) = 1
        _transForwMatr(0).val(2, 2) = 1
        _transForwMatr(0).val(3, 3) = 1

        'Transformationsmatrizen jeder Achse
        For i As Integer = 1 To 6
            _transForwMatr(i) = CalcDHTransMatrix(i, joints.ToArray(i - 1))
        Next

        'Tool Frame
        _transForwMatr(7).val(0, 0) = 1
        _transForwMatr(7).val(1, 1) = 1
        _transForwMatr(7).val(2, 2) = 1
        _transForwMatr(7).val(3, 3) = 1

        'Matritzen multiplizieren
        Dim tmpMatr As Matrix4x4
        tmpMatr = _transForwMatr(1).Multiply(_transForwMatr(0))
        For i As Int16 = 2 To 7
            tmpMatr = _transForwMatr(i).Multiply(tmpMatr)
        Next

        'Ausgeben
        Dim erg As CartCoords
        'Position
        erg.X = tmpMatr.val(0, 3)
        erg.Y = tmpMatr.val(1, 3)
        erg.Z = tmpMatr.val(2, 3)
        'Ausrichtung
        Dim tmpP As Double = Math.Atan2(Math.Sqrt((tmpMatr.val(0, 2) ^ 2) + (tmpMatr.val(1, 2) ^ 2)), tmpMatr.val(2, 2) * -1)
        erg.yaw = ToGrad(Math.Atan2(tmpMatr.val(2, 0) / tmpP, tmpMatr.val(2, 1) / tmpP))
        erg.pitch = ToGrad(tmpP) 'p
        erg.roll = ToGrad(Math.Atan2(tmpMatr.val(0, 2) / tmpP, tmpMatr.val(1, 2) / tmpP))

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
        Dim thetaBog As Double = ToBogenmass(theta)
        Dim erg As New Matrix4x4

        dh.alpha = ToBogenmass(dh.alpha)

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

    'TODO!!!
    Private Function CalcFrameMatrix(frame As Double()) As Matrix4x4
        Dim Matrix As New Matrix4x4
        Dim X As Double = frame(0)
        Dim Y As Double = frame(1)
        Dim yaw As Double = ToBogenmass(frame(3))
        Dim pitch As Double = ToBogenmass(frame(4))
        Dim roll As Double = ToBogenmass(frame(5))
        Matrix.val(0, 0) = Math.Cos(ToBogenmass(frame(5))) * Math.Cos(ToBogenmass(frame(4)))
        Matrix.val(0, 1) = -Math.Sin(ToBogenmass(frame(5))) * Math.Cos(ToBogenmass(frame(3))) + Math.Cos(ToBogenmass(frame(5))) * Math.Sin(ToBogenmass(frame(4))) * Math.Sin(ToBogenmass(frame(3)))
        Matrix.val(0, 2) = 0
        Matrix.val(0, 3) = 0

        Matrix.val(1, 0) = 0
        Matrix.val(1, 1) = 0
        Matrix.val(1, 2) = 0
        Matrix.val(1, 3) = 0

        Matrix.val(2, 0) = 0
        Matrix.val(2, 1) = 0
        Matrix.val(2, 2) = 0
        Matrix.val(2, 3) = 0

        Matrix.val(3, 0) = 0
        Matrix.val(3, 1) = 0
        Matrix.val(3, 2) = 0
        Matrix.val(3, 3) = 0

        Return Matrix
    End Function

    Private Function ToBogenmass(winkel As Double) As Double
        Return winkel * (Math.PI / 180)
    End Function

    Private Function ToGrad(winkel As Double) As Double
        Return winkel * (180.0 / Math.PI)
    End Function
#End Region

End Class

