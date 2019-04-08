Friend Class panSettings
    ' -----------------------------------------------------------------------------
    ' TODO
    ' -----------------------------------------------------------------------------
    ' TCP-Server einstellungen
    ' Denavit Hartenberg Parameter updaten bei PropGrid Change

    ' -----------------------------------------------------------------------------
    ' Init Panel
    ' -----------------------------------------------------------------------------
    Private _selectedSetting As selectedSetting = selectedSetting.RoboPar
    Private _actPropView As PropView
    Private Sub PanRoboParameter_Load(sender As Object, e As EventArgs) Handles Me.Load
        _refreshFilename()
        _setPropView(PropView.J1)
        _refreshButtons()
    End Sub

    ' -----------------------------------------------------------------------------
    ' Form Control
    ' -----------------------------------------------------------------------------
    Private Sub btnDenHartPar_Click(sender As Object, e As EventArgs) Handles btnDenHartPar.Click
        _selectedSetting = selectedSetting.DenHartPar
        If _actPropView > PropView.J6 Then
            _setPropView(PropView.J1)
        Else
            _refreshPropGrid()
        End If
        _refreshButtons()
    End Sub
    Private Sub btnRoboPar_Click(sender As Object, e As EventArgs) Handles btnRoboPar.Click
        _selectedSetting = selectedSetting.RoboPar
        _refreshPropGrid()
        _refreshButtons()
    End Sub

    Private Sub btnTCPServer_Click(sender As Object, e As EventArgs) Handles btnTCPServer.Click
        _selectedSetting = selectedSetting.TCPServer
        _refreshPropGrid()
        _refreshButtons()
    End Sub
    Private Sub propGridRoboPar_PropertyValueChanged(sender As Object, e As EventArgs) Handles propGridRoboPar.PropertyValueChanged
        'Objekt aktualisieren
        Select Case _selectedSetting
            Case selectedSetting.RoboPar
                If _actPropView > 5 Then
                    frmMain.RoboControl.Par.SetServoParameter(_actPropView - 6, CType(propGridRoboPar.SelectedObject, ServoParameter))
                Else
                    frmMain.RoboControl.Par.SetJointParameter(_actPropView, CType(propGridRoboPar.SelectedObject, JointParameter))
                End If
            Case selectedSetting.DenHartPar
                frmMain.RoboControl.Par.SetDenavitHartenbergParameter(_actPropView, CType(propGridRoboPar.SelectedObject, DHParameter))
            Case selectedSetting.TCPServer
                'TODO
        End Select
    End Sub
    Private Sub btnJ1_Click(sender As Object, e As EventArgs) Handles btnJ1.Click
        _setPropView(PropView.J1)
    End Sub

    Private Sub btnJ2_Click(sender As Object, e As EventArgs) Handles btnJ2.Click
        _setPropView(PropView.J2)
    End Sub

    Private Sub btnJ3_Click(sender As Object, e As EventArgs) Handles btnJ3.Click
        _setPropView(PropView.J3)
    End Sub

    Private Sub btnJ4_Click(sender As Object, e As EventArgs) Handles btnJ4.Click
        _setPropView(PropView.J4)
    End Sub

    Private Sub btnJ5_Click(sender As Object, e As EventArgs) Handles btnJ5.Click
        _setPropView(PropView.J5)
    End Sub

    Private Sub btnJ6_Click(sender As Object, e As EventArgs) Handles btnJ6.Click
        _setPropView(PropView.J6)
    End Sub

    Private Sub btnServo1_Click(sender As Object, e As EventArgs) Handles btnServo1.Click
        _setPropView(PropView.Servo1)
    End Sub

    Private Sub btnServo2_Click(sender As Object, e As EventArgs) Handles btnServo2.Click
        _setPropView(PropView.Servo2)
    End Sub

    Private Sub btnServo3_Click(sender As Object, e As EventArgs) Handles btnServo3.Click
        _setPropView(PropView.Servo3)
    End Sub
    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        If frmMain.RoboControl.Par.LoadSettings() Then
            _refreshPropGrid()
            _refreshFilename()
        End If
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If frmMain.RoboControl.Par.SaveSettings() Then
            _refreshFilename()
        End If
    End Sub
    Private Sub btnDefaultConfig_Click(sender As Object, e As EventArgs) Handles btnDefaultConfig.Click
        If frmMain.RoboControl.Par.LoadDefaulSettings() Then
            _refreshPropGrid()
            _refreshFilename()
        End If
    End Sub

    ' -----------------------------------------------------------------------------
    ' Help Functions
    ' -----------------------------------------------------------------------------
    Private Enum selectedSetting
        RoboPar = 0
        DenHartPar
        TCPServer
    End Enum
    Private Enum PropView
        J1
        J2
        J3
        J4
        J5
        J6
        Servo1
        Servo2
        Servo3
    End Enum
    Private Sub _setPropView(nr As PropView)
        _actPropView = nr
        ' Check Button
        btnJ1.Checked = False
        btnJ2.Checked = False
        btnJ3.Checked = False
        btnJ4.Checked = False
        btnJ5.Checked = False
        btnJ6.Checked = False
        btnServo1.Checked = False
        btnServo2.Checked = False
        btnServo3.Checked = False
        Select Case nr
            Case PropView.J1
                btnJ1.Checked = True
            Case PropView.J2
                btnJ2.Checked = True
            Case PropView.J3
                btnJ3.Checked = True
            Case PropView.J4
                btnJ4.Checked = True
            Case PropView.J5
                btnJ5.Checked = True
            Case PropView.J6
                btnJ6.Checked = True
            Case PropView.Servo1
                btnServo1.Checked = True
            Case PropView.Servo2
                btnServo2.Checked = True
            Case PropView.Servo3
                btnServo3.Checked = True
        End Select
        _refreshPropGrid()
    End Sub
    Private Sub _refreshPropGrid()
        Select Case _selectedSetting
            Case selectedSetting.RoboPar
                If _actPropView > 5 Then
                    propGridRoboPar.SelectedObject = frmMain.RoboControl.Par.ServoParameter(_actPropView - 6)
                Else
                    propGridRoboPar.SelectedObject = frmMain.RoboControl.Par.JointParameter(_actPropView)
                End If
            Case selectedSetting.DenHartPar
                propGridRoboPar.SelectedObject = frmMain.RoboControl.Par.DenavitHartenbergParameter(_actPropView)
            Case selectedSetting.TCPServer
                propGridRoboPar.SelectedObject = Nothing
        End Select
    End Sub
    Private Sub _refreshButtons()
        Select Case _selectedSetting
            Case selectedSetting.RoboPar
                btnRoboPar.Visible = False
                btnTCPServer.Visible = True
                btnDenHartPar.Visible = True
                btnJ1.Visible = True
                btnJ2.Visible = True
                btnJ3.Visible = True
                btnJ4.Visible = True
                btnJ5.Visible = True
                btnJ6.Visible = True
                btnServo1.Visible = True
                btnServo2.Visible = True
                btnServo3.Visible = True
                ToolStrip2.Visible = True
                sepJ6.Visible = True
                sepServ1.Visible = True
                sepServ2.Visible = True
                sepServ3.Visible = True
            Case selectedSetting.DenHartPar
                btnRoboPar.Visible = True
                btnTCPServer.Visible = True
                btnDenHartPar.Visible = False
                btnJ1.Visible = True
                btnJ2.Visible = True
                btnJ3.Visible = True
                btnJ4.Visible = True
                btnJ5.Visible = True
                btnJ6.Visible = True
                btnServo1.Visible = False
                btnServo2.Visible = False
                btnServo3.Visible = False
                ToolStrip2.Visible = True
                sepServ1.Visible = False
                sepServ2.Visible = False
                sepServ3.Visible = False
            Case selectedSetting.TCPServer
                btnRoboPar.Visible = True
                btnTCPServer.Visible = False
                btnDenHartPar.Visible = True
                ToolStrip2.Visible = False
        End Select
    End Sub
    Private Sub _refreshFilename()
        Dim filenameSplit As String() = frmMain.RoboControl.Par.GetActFilename.Split("\"c)
        lblFilename.Text = $"Geöffnete Parameterdatei: {filenameSplit(filenameSplit.Length - 1)}"
    End Sub
End Class