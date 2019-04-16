Public Class panTeachPoints
    ' -----------------------------------------------------------------------------
    ' TODO
    ' -----------------------------------------------------------------------------
    ' 

    ' -----------------------------------------------------------------------------
    ' Init Panel
    ' -----------------------------------------------------------------------------
    Private Sub panTeachPoints_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        frmMain.ACLProgram.SetListBox(lbTeachPoints)

        _enableDisableElements()

        AddHandler frmMain.RoboControl.SerialConnected, AddressOf _eRefresh
        AddHandler frmMain.RoboControl.SerialDisconnected, AddressOf _eRefresh
        AddHandler frmMain.RoboControl.RoboMoveStarted, AddressOf _eRefresh
        AddHandler frmMain.RoboControl.RoboMoveFinished, AddressOf _eRefresh
        AddHandler frmMain.RoboControl.RoboRefStateChanged, AddressOf _eRefresh
        AddHandler frmMain.ACLProgram.ProgramFinished, AddressOf _eRefresh
    End Sub

    ' -----------------------------------------------------------------------------
    ' Form Control
    ' -----------------------------------------------------------------------------
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If frmMain.ACLProgram.AddTeachPoint(tbName.Text, frmMain.RoboControl.PosJoint, CInt(numNr.Value)) Then
            numNr.Value += 1
            tbName.Text = ""
        End If
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
    Private Sub btnMoveTo_Click(sender As Object, e As EventArgs)
        Dim tp As TeachPoint = frmMain.ACLProgram.GetTeachpointByIndex(lbTeachPoints.SelectedIndex)

        frmMain.RoboControl.SetSpeedAndAcc(numSpeed.Value, numAcc.Value)

        If tp.cart Then
            frmMain.RoboControl.DoTCPMov(tp.cartCoords)
        Else
            frmMain.RoboControl.DoJointMov(True, tp.jointAngles)
        End If
    End Sub
    Private Sub lbTeachPoints_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbTeachPoints.SelectedIndexChanged
        _enableDisableElements()
    End Sub

    ' -----------------------------------------------------------------------------
    ' Helper Functions
    ' -----------------------------------------------------------------------------
    Private Sub _enableDisableElements()
        If InvokeRequired Then
            Invoke(Sub() _enableDisableElements())
            Return
        End If

        btnMoveTo.Enabled = SerialConnected And Not RobotBusy And Not ProgramRunning And lbTeachPoints.SelectedIndex > -1
    End Sub

    ' -----------------------------------------------------------------------------
    ' Events
    ' -----------------------------------------------------------------------------
    Private Sub _eRefresh()
        _enableDisableElements()
    End Sub
End Class