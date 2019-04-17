Friend Class panReference
    ' -----------------------------------------------------------------------------
    ' TODO
    ' -----------------------------------------------------------------------------
    ' fertig?

    ' -----------------------------------------------------------------------------
    ' Init Panel
    ' -----------------------------------------------------------------------------
    Private Sub panReference_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler frmMain.RoboControl.SerialConnected, AddressOf _eRefresh
        AddHandler frmMain.RoboControl.SerialDisconnected, AddressOf _eRefresh
        AddHandler frmMain.RoboControl.RoboBusy, AddressOf _eRoboBusy
        AddHandler frmMain.ACLProgram.ProgramFinished, AddressOf _eRefresh
    End Sub

    ' -----------------------------------------------------------------------------
    ' Form Control
    ' -----------------------------------------------------------------------------
    Private Sub cbCheckedChanged(sender As Object, e As EventArgs) Handles cbSelJ1.CheckedChanged, cbSelJ2.CheckedChanged, cbSelJ3.CheckedChanged, cbSelJ4.CheckedChanged, cbSelJ5.CheckedChanged, cbSelJ6.CheckedChanged
        _enableDisableElements()
    End Sub

    ' -----------------------------------------------------------------------------
    ' Robot Control
    ' -----------------------------------------------------------------------------
    Private Sub btnRefStart_Click(sender As Object, e As EventArgs) Handles btnRefStart.Click
        frmMain.RoboControl.DoRef(cbSelJ1.Checked, cbSelJ2.Checked, cbSelJ3.Checked, cbSelJ4.Checked, cbSelJ5.Checked, cbSelJ6.Checked)
    End Sub
    Private Sub btnRefJ1_Click(sender As Object, e As EventArgs) Handles btnRefJ1.Click
        frmMain.RoboControl.DoRef(1)
    End Sub
    Private Sub btnRefJ2_Click(sender As Object, e As EventArgs) Handles btnRefJ2.Click
        frmMain.RoboControl.DoRef(2)
    End Sub
    Private Sub btnRefJ3_Click(sender As Object, e As EventArgs) Handles btnRefJ3.Click
        frmMain.RoboControl.DoRef(3)
    End Sub
    Private Sub btnRefJ4_Click(sender As Object, e As EventArgs) Handles btnRefJ4.Click
        frmMain.RoboControl.DoRef(4)
    End Sub
    Private Sub btnRefJ5_Click(sender As Object, e As EventArgs) Handles btnRefJ5.Click
        frmMain.RoboControl.DoRef(5)
    End Sub
    Private Sub btnRefJ6_Click(sender As Object, e As EventArgs) Handles btnRefJ6.Click
        frmMain.RoboControl.DoRef(6)
    End Sub
    ' -----------------------------------------------------------------------------
    ' Helper Functions
    ' -----------------------------------------------------------------------------
    Private Sub _enableDisableElements()
        If InvokeRequired Then
            Invoke(Sub() _enableDisableElements())
            Return
        End If

        Dim tmpEnabled As Boolean = SerialConnected And Not RobotBusy And Not ProgramRunning

        btnRefJ1.Enabled = tmpEnabled
        btnRefJ2.Enabled = tmpEnabled
        btnRefJ3.Enabled = tmpEnabled
        btnRefJ4.Enabled = tmpEnabled
        btnRefJ5.Enabled = tmpEnabled
        btnRefJ6.Enabled = tmpEnabled
        btnRefStart.Enabled = tmpEnabled And (cbSelJ1.Checked Or cbSelJ2.Checked Or cbSelJ3.Checked Or cbSelJ4.Checked Or cbSelJ5.Checked Or cbSelJ6.Checked)
    End Sub
    ' -----------------------------------------------------------------------------
    ' Events
    ' -----------------------------------------------------------------------------
    Private Sub _eRefresh()
        _enableDisableElements()
    End Sub
    Private Sub _eRoboBusy(busy As Boolean)
        _enableDisableElements()
    End Sub
End Class