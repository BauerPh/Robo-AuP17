Public Class panCtrl
    ' -----------------------------------------------------------------------------
    ' Formsteuerung
    ' -----------------------------------------------------------------------------
    Private Sub panCtrl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cbJointOrTCP.SelectedIndex = 0
        cbJogMode.SelectedIndex = 0
        cbMoveMode.SelectedIndex = 0
    End Sub

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
            lblCtrl1.Text = "J1"
            lblCtrl2.Text = "J2"
            lblCtrl3.Text = "J3"
            lblCtrl4.Text = "J4"
            lblCtrl5.Text = "J5"
            lblCtrl6.Text = "J6"
        Else
            ' TCP Mode
            cbJogMode.Visible = False
            lblCtrl1.Text = "X"
            lblCtrl2.Text = "Y"
            lblCtrl3.Text = "Z"
            lblCtrl4.Text = "yaw"
            lblCtrl5.Text = "pitch"
            lblCtrl6.Text = "roll"
        End If
    End Sub

End Class