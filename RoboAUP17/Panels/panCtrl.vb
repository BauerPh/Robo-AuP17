Public Class panCtrl
    Private Sub panCtrl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cbJogMode.SelectedIndex = 0
        cbMoveMode.SelectedIndex = 0
    End Sub

    Private Sub cbMoveMode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbMoveMode.SelectedIndexChanged
        If cbMoveMode.SelectedIndex = 0 Then
            btnMoveStart.Visible = True
        Else
            btnMoveStart.Visible = False
        End If
    End Sub
End Class