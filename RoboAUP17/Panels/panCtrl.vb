
Public Class panCtrl
    ' -----------------------------------------------------------------------------
    ' TODO
    ' -----------------------------------------------------------------------------
    ' Formsteuerung
    ' Slider, Buttons, Bewegungsbefehle an RoboControl senden
    ' Slider, Nums updaten wenn RoboControl sich ändert (Ereignisse, Callbacks??)

    ' -----------------------------------------------------------------------------
    ' Init Panel
    ' -----------------------------------------------------------------------------
    Private Sub panCtrl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cbJointOrTCP.SelectedIndex = 0
        cbJogMode.SelectedIndex = 0
        cbMoveMode.SelectedIndex = 0


        AddHandler frmMain.roboControl.RoboPositionChanged, AddressOf eNewPos
    End Sub

    ' -----------------------------------------------------------------------------
    ' Form Control
    ' -----------------------------------------------------------------------------
    Private Sub cbMoveMode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbMoveMode.SelectedIndexChanged
        If cbMoveMode.SelectedIndex = 0 Then
            ' getriggert
            btnMoveStart.Visible = True
        Else
            ' direkt
            btnMoveStart.Visible = False
        End If
    End Sub

    Private Sub cbJointOrTCP_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbJointOrTCP.SelectedIndexChanged
        If cbJointOrTCP.SelectedIndex = 0 Then
            ' Joint Mode
            cbJogMode.Visible = True
            lblCtrl1.Text = "J1:"
            lblCtrl2.Text = "J2:"
            lblCtrl3.Text = "J3:"
            lblCtrl4.Text = "J4:"
            lblCtrl5.Text = "J5:"
            lblCtrl6.Text = "J6:"
        Else
            ' TCP Mode
            cbJogMode.Visible = False
            lblCtrl1.Text = "X:"
            lblCtrl2.Text = "Y:"
            lblCtrl3.Text = "Z:"
            lblCtrl4.Text = "yaw:"
            lblCtrl5.Text = "pitch:"
            lblCtrl6.Text = "roll:"
        End If
    End Sub

    ' -----------------------------------------------------------------------------
    ' Events
    ' -----------------------------------------------------------------------------
    Private Sub eNewPos()
        If InvokeRequired Then
            Invoke(Sub() eNewPos())
            Return
        End If
        numCtrl1.Value = CDec(frmMain.roboControl.PosJoint.items(0))
        numCtrl2.Value = CDec(frmMain.roboControl.PosJoint.items(1))
        numCtrl3.Value = CDec(frmMain.roboControl.PosJoint.items(2))
        numCtrl4.Value = CDec(frmMain.roboControl.PosJoint.items(3))
        numCtrl5.Value = CDec(frmMain.roboControl.PosJoint.items(4))
        numCtrl6.Value = CDec(frmMain.roboControl.PosJoint.items(5))
    End Sub
End Class