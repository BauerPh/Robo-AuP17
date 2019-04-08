Public Class panRoboStatus
    ' -----------------------------------------------------------------------------
    ' TODO
    ' -----------------------------------------------------------------------------
    ' ...

    ' -----------------------------------------------------------------------------
    ' Init Panel
    ' -----------------------------------------------------------------------------


    ' -----------------------------------------------------------------------------
    ' TEST
    ' -----------------------------------------------------------------------------
    Dim denHart(5) As DHParameter
    Dim kins As Kinematics


    Private Sub btnLos_Click(sender As Object, e As EventArgs) Handles btnLos.Click
        'Forward
        Dim joints As JointAngles
        Dim coords As CartCoords
        joints.J1 = numJ1.Value
        joints.J2 = numJ2.Value
        joints.J3 = numJ3.Value
        joints.J4 = numJ4.Value
        joints.J5 = numJ5.Value
        joints.J6 = numJ6.Value

        coords = kins.ForwardKin(joints)

        lblX.Text = coords.X.ToString
        lblY.Text = coords.Y.ToString
        lblZ.Text = coords.Z.ToString
        lblYaw.Text = coords.yaw.ToString
        lblPitch.Text = coords.pitch.ToString
        lblRoll.Text = coords.roll.ToString

        'Inverse
        joints = kins.InversKin(coords)

        lblJ1.Text = joints.J1.ToString
        lblJ2.Text = joints.J2.ToString
        lblJ3.Text = joints.J3.ToString
        lblJ4.Text = joints.J4.ToString
        lblJ5.Text = joints.J5.ToString
        lblJ6.Text = joints.J6.ToString
    End Sub

    Private Sub panRoboStatus_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'DH Params
        denHart(0) = New DHParameter
        denHart(0).alpha = -90.0
        denHart(0).d = 169.77
        denHart(0).a = 64.2
        denHart(1) = New DHParameter
        denHart(1).alpha = 0.0
        denHart(1).d = 0.0
        denHart(1).a = 305.0
        denHart(2) = New DHParameter
        denHart(2).alpha = 90.0
        denHart(2).d = 0.0
        denHart(2).a = 0.0
        denHart(3) = New DHParameter
        denHart(3).alpha = -90.0
        denHart(3).d = -222.63
        denHart(3).a = 0.0
        denHart(4) = New DHParameter
        denHart(4).alpha = 90.0
        denHart(4).d = 0.0
        denHart(4).a = 0.0
        denHart(5) = New DHParameter
        denHart(5).alpha = 0.0
        denHart(5).d = -36.25
        denHart(5).a = 0.0
        kins = New Kinematics(denHart)
    End Sub
End Class