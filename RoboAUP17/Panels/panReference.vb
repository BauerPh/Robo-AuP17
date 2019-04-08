﻿Friend Class panReference
    ' -----------------------------------------------------------------------------
    ' TODO
    ' -----------------------------------------------------------------------------
    ' ...

    Private _serialConnected As Boolean = False
    ' -----------------------------------------------------------------------------
    ' Init Panel
    ' -----------------------------------------------------------------------------
    Private Sub panReference_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler frmMain.RoboControl.SerialConnected, AddressOf _eComSerialConnected
        AddHandler frmMain.RoboControl.SerialDisconnected, AddressOf _eComSerialDisconnected
        AddHandler frmMain.RoboControl.RoboMoveStarted, AddressOf _eRoboMoveStarted
        AddHandler frmMain.RoboControl.RoboMoveFinished, AddressOf _eRoboMoveFinished
    End Sub

    ' -----------------------------------------------------------------------------
    ' Form Control
    ' -----------------------------------------------------------------------------
    Private Sub cbCheckedChanged(sender As Object, e As EventArgs) Handles cbSelJ1.CheckedChanged, cbSelJ2.CheckedChanged, cbSelJ3.CheckedChanged, cbSelJ4.CheckedChanged, cbSelJ5.CheckedChanged, cbSelJ6.CheckedChanged
        _enableDisableElements(False)
    End Sub
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
    Private Sub _enableDisableElements(disable As Boolean)
        If InvokeRequired Then
            Invoke(Sub() _enableDisableElements(disable))
            Return
        End If

        btnRefJ1.Enabled = _serialConnected And Not disable
        btnRefJ2.Enabled = _serialConnected And Not disable
        btnRefJ3.Enabled = _serialConnected And Not disable
        btnRefJ4.Enabled = _serialConnected And Not disable
        btnRefJ5.Enabled = _serialConnected And Not disable
        btnRefJ6.Enabled = _serialConnected And Not disable
        btnRefStart.Enabled = Not disable And _serialConnected And (cbSelJ1.Checked Or cbSelJ2.Checked Or cbSelJ3.Checked Or cbSelJ4.Checked Or cbSelJ5.Checked Or cbSelJ6.Checked)
    End Sub
    ' -----------------------------------------------------------------------------
    ' Events
    ' -----------------------------------------------------------------------------
    Private Sub _eComSerialConnected()
        _serialConnected = True
        _enableDisableElements(False)
    End Sub
    Private Sub _eComSerialDisconnected()
        _serialConnected = False
        _enableDisableElements(False)
    End Sub
    Private Sub _eRoboMoveStarted()
        _enableDisableElements(True)
    End Sub
    Private Sub _eRoboMoveFinished()
        _enableDisableElements(False)
    End Sub
End Class