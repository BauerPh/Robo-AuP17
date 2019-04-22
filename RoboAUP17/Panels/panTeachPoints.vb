Public Class panTeachPoints
    ' -----------------------------------------------------------------------------
    ' Init Panel
    ' -----------------------------------------------------------------------------
    Private Sub panTeachPoints_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        frmMain.ACLProgram.SetListBox(lbTeachPoints)

        cbTPMode.SelectedIndex = 0

        _enableDisableElements()

        AddHandler frmMain.RefreshEvent, AddressOf _eRefresh
    End Sub

    ' -----------------------------------------------------------------------------
    ' Form Control
    ' -----------------------------------------------------------------------------
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If cbTPMode.SelectedIndex = 0 Then
            If frmMain.ACLProgram.AddTeachPoint(tbName.Text, frmMain.RoboControl.PosCart, CInt(numNr.Value)) Then
                numNr.Value += 1
                tbName.Text = ""
            End If
        Else
            If frmMain.ACLProgram.AddTeachPoint(tbName.Text, frmMain.RoboControl.PosJoint, CInt(numNr.Value)) Then
                numNr.Value += 1
                tbName.Text = ""
            End If
        End If
    End Sub

    Private Sub btnRename_Click(sender As Object, e As EventArgs) Handles btnRename.Click
        frmMain.ACLProgram.RenameTeachPoint(lbTeachPoints.SelectedIndex, tbName.Text, CInt(numNr.Value))
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        frmMain.ACLProgram.DeleteTeachPoint(lbTeachPoints.SelectedIndex)
        If lbTeachPoints.SelectedIndex = -1 Then _enableDisableElements()
    End Sub

    Private Sub btnUp_Click(sender As Object, e As EventArgs) Handles btnUp.Click
        frmMain.ACLProgram.MoveTeachPoint(lbTeachPoints.SelectedIndex, True)
    End Sub

    Private Sub btnDown_Click(sender As Object, e As EventArgs) Handles btnDown.Click
        frmMain.ACLProgram.MoveTeachPoint(lbTeachPoints.SelectedIndex, False)
    End Sub
    Private Sub btnMoveTo_Click(sender As Object, e As EventArgs) Handles btnMoveTo.Click
        Dim tp As TeachPoint = frmMain.ACLProgram.GetTeachpointByIndex(lbTeachPoints.SelectedIndex)

        frmMain.RoboControl.SetSpeedAndAcc(numSpeed.Value, numAcc.Value)

        If tp.type Then
            frmMain.RoboControl.DoTCPMov(tp.cartCoords)
        Else
            frmMain.RoboControl.DoJointMov(True, tp.jointAngles)
        End If
    End Sub
    Private Sub lbTeachPoints_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbTeachPoints.SelectedIndexChanged
        _enableDisableElements()

        Dim tp As TeachPoint = frmMain.ACLProgram.GetTeachpointByIndex(lbTeachPoints.SelectedIndex)
        tbName.Text = tp.name
        numNr.Value = tp.nr
    End Sub

    ' -----------------------------------------------------------------------------
    ' Private
    ' -----------------------------------------------------------------------------
    Private Sub _enableDisableElements()
        If InvokeRequired Then
            Invoke(Sub() _enableDisableElements())
            Return
        End If

        Dim tmpEnabled As Boolean = SerialConnected And Not RobotBusy And Not ProgramRunning And frmMain.RoboControl.AllRefOkay

        btnMoveTo.Enabled = tmpEnabled And lbTeachPoints.SelectedIndex > -1
        btnAdd.Enabled = tmpEnabled
    End Sub

    ' -----------------------------------------------------------------------------
    ' Events
    ' -----------------------------------------------------------------------------
    Private Sub _eRefresh()
        _enableDisableElements()
    End Sub
End Class