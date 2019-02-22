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

    ' -----------------------------------------------------------------------------
    ' Settings
    ' -----------------------------------------------------------------------------
    Public Structure JointParameter
        Public nr As Int32
        Public mot As MotorParameter
        Public mech As MechanicalParameter
        Public cal As CalibrationParameter
        Public profile As ProfileParameter
    End Structure
    Public Structure ServoParameter
        Public minAngle As Double
        Public maxAngle As Double
    End Structure


    Public Structure MotorParameter
        Public stepsPerRot As Int32
        Public gear As Double
        Public mode As Int32
        Public dir As Int32
    End Structure

    Public Structure MechanicalParameter
        Public gear As Double
        Public minAngle As Double
        Public maxAngle As Double
        Public homePosAngle As Double
        Public parkPosAngle As Double
    End Structure

    Public Structure CalibrationParameter
        Public dir As Int32
        Public speedFast As Double
        Public speedSlow As Double
        Public acc As Double
    End Structure

    Public Structure ProfileParameter
        Public maxSpeed As Double
        Public maxAcc As Double
        Public stopAcc As Double
    End Structure
End Module
