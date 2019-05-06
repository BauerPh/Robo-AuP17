Friend Class panSettings
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
    ' Public
    ' -----------------------------------------------------------------------------
    Friend Enum selectedSetting
        RoboPar = 0
        DenHartPar
        Frames
        TCPSettings
    End Enum
    Friend Sub SetSelectedSetting(setting As selectedSetting)
        _selectedSetting = setting

        Select Case _selectedSetting
            Case selectedSetting.RoboPar
                If _actPropView > PropView.Servo3 Then
                    _setPropView(PropView.J1)
                Else
                    _refreshPropGrid()
                End If
            Case selectedSetting.DenHartPar
                If _actPropView > PropView.J6 Then
                    _setPropView(PropView.J1)
                Else
                    _refreshPropGrid()
                End If
            Case selectedSetting.Frames
                MessageBox.Show($"Die Frames (Toolframe und Workframe) sollten nicht geändert werden! Sie führen momentan noch zu unvorhersehbaren Auswirkungen auf die kinematischen Berechnungen!", "Warnung", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                If _actPropView < PropView.Servo3 Then
                    _setPropView(PropView.Toolframe)
                Else
                    _refreshPropGrid()
                End If
            Case selectedSetting.TCPSettings
                _refreshPropGrid()
        End Select

        _refreshButtons()
    End Sub

    ' -----------------------------------------------------------------------------
    ' Form Control
    ' -----------------------------------------------------------------------------
    Private Sub btnDenHartPar_Click(sender As Object, e As EventArgs) Handles btnDenHartPar.Click
        SetSelectedSetting(selectedSetting.DenHartPar)
    End Sub

    Private Sub btnFrames_Click(sender As Object, e As EventArgs) Handles btnFrames.Click
        SetSelectedSetting(selectedSetting.Frames)
    End Sub

    Private Sub btnRoboPar_Click(sender As Object, e As EventArgs) Handles btnRoboPar.Click
        SetSelectedSetting(selectedSetting.RoboPar)
    End Sub

    Private Sub btnTCPServer_Click(sender As Object, e As EventArgs) Handles btnTCPSettings.Click
        SetSelectedSetting(selectedSetting.TCPSettings)
    End Sub
    Private Sub propGridRoboPar_PropertyValueChanged(sender As Object, e As EventArgs) Handles propGridRoboPar.PropertyValueChanged
        'Objekte aktualisieren
        Select Case _selectedSetting
            Case selectedSetting.RoboPar
                If _actPropView > PropView.J6 Then
                    frmMain.RoboControl.Pref.SetServoParameter(_actPropView - 6, CType(propGridRoboPar.SelectedObject, ServoParameter))
                Else
                    frmMain.RoboControl.Pref.SetJointParameter(_actPropView, CType(propGridRoboPar.SelectedObject, JointParameter))
                End If
            Case selectedSetting.DenHartPar
                frmMain.RoboControl.Pref.SetDenavitHartenbergParameter(_actPropView, CType(propGridRoboPar.SelectedObject, DHParameter))
            Case selectedSetting.Frames
                If _actPropView = PropView.Toolframe Then
                    frmMain.RoboControl.Pref.SetToolframe(CType(propGridRoboPar.SelectedObject, CartCoords))
                Else
                    frmMain.RoboControl.Pref.SetWorkframe(CType(propGridRoboPar.SelectedObject, CartCoords))
                End If
            Case selectedSetting.TCPSettings
                frmMain.RoboControl.Pref.SetTcpParameter(CType(propGridRoboPar.SelectedObject, TCPParameter))
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
    Private Sub btnToolframe_Click(sender As Object, e As EventArgs) Handles btnToolframe.Click
        _setPropView(PropView.Toolframe)
    End Sub
    Private Sub btnWorkframe_Click(sender As Object, e As EventArgs) Handles btnWorkframe.Click
        _setPropView(PropView.Workframe)
    End Sub
    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        If frmMain.RoboControl.Pref.LoadSettings() Then
            _refreshPropGrid()
            _refreshFilename()
        End If
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If frmMain.RoboControl.Pref.SaveSettings() Then
            _refreshFilename()
        End If
    End Sub
    Private Sub btnDefaultConfig_Click(sender As Object, e As EventArgs) Handles btnDefaultConfig.Click
        If frmMain.RoboControl.Pref.LoadDefaulSettings() Then
            _refreshPropGrid()
            _refreshFilename()
        End If
    End Sub

    ' -----------------------------------------------------------------------------
    ' Private
    ' -----------------------------------------------------------------------------
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
        Toolframe
        Workframe
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
        btnToolframe.Checked = False
        btnWorkframe.Checked = False
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
            Case PropView.Toolframe
                btnToolframe.Checked = True
            Case PropView.Workframe
                btnWorkframe.Checked = True
        End Select
        _refreshPropGrid()
    End Sub
    Private Sub _refreshPropGrid()
        Select Case _selectedSetting
            Case selectedSetting.RoboPar
                If _actPropView > 5 Then
                    propGridRoboPar.SelectedObject = frmMain.RoboControl.Pref.ServoParameter(_actPropView - 6)
                Else
                    propGridRoboPar.SelectedObject = frmMain.RoboControl.Pref.JointParameter(_actPropView)
                End If
            Case selectedSetting.DenHartPar
                propGridRoboPar.SelectedObject = frmMain.RoboControl.Pref.DenavitHartenbergParameter(_actPropView)
            Case selectedSetting.Frames
                If _actPropView = PropView.Toolframe Then
                    propGridRoboPar.SelectedObject = frmMain.RoboControl.Pref.Toolframe
                Else
                    propGridRoboPar.SelectedObject = frmMain.RoboControl.Pref.Workframe
                End If
            Case selectedSetting.TCPSettings
                propGridRoboPar.SelectedObject = frmMain.RoboControl.Pref.TcpParameter
        End Select
    End Sub
    Private Sub _refreshButtons()
        btnRoboPar.Visible = False
        btnTCPSettings.Visible = False
        btnDenHartPar.Visible = False
        btnFrames.Visible = False
        btnJ1.Visible = False
        btnJ2.Visible = False
        btnJ3.Visible = False
        btnJ4.Visible = False
        btnJ5.Visible = False
        btnJ6.Visible = False
        btnServo1.Visible = False
        btnServo2.Visible = False
        btnServo3.Visible = False
        btnToolframe.Visible = False
        btnWorkframe.Visible = False
        ToolStrip3.Visible = False
        sepJ2.Visible = False
        sepJ3.Visible = False
        sepJ4.Visible = False
        sepJ5.Visible = False
        sepJ6.Visible = False
        sepServ1.Visible = False
        sepServ2.Visible = False
        sepServ3.Visible = False
        sepFrames.Visible = False
        Select Case _selectedSetting
            Case selectedSetting.RoboPar
                btnTCPSettings.Visible = True
                btnDenHartPar.Visible = True
                btnFrames.Visible = True
                btnJ1.Visible = True
                btnJ2.Visible = True
                btnJ3.Visible = True
                btnJ4.Visible = True
                btnJ5.Visible = True
                btnJ6.Visible = True
                btnServo1.Visible = True
                btnServo2.Visible = True
                btnServo3.Visible = True
                ToolStrip3.Visible = True
                sepJ2.Visible = True
                sepJ3.Visible = True
                sepJ4.Visible = True
                sepJ5.Visible = True
                sepJ6.Visible = True
                sepServ1.Visible = True
                sepServ2.Visible = True
                sepServ3.Visible = True
            Case selectedSetting.DenHartPar
                btnRoboPar.Visible = True
                btnTCPSettings.Visible = True
                btnFrames.Visible = True
                btnJ1.Visible = True
                btnJ2.Visible = True
                btnJ3.Visible = True
                btnJ4.Visible = True
                btnJ5.Visible = True
                btnJ6.Visible = True
                ToolStrip3.Visible = True
                sepJ2.Visible = True
                sepJ3.Visible = True
                sepJ4.Visible = True
                sepJ5.Visible = True
                sepJ6.Visible = True
            Case selectedSetting.Frames
                btnRoboPar.Visible = True
                btnTCPSettings.Visible = True
                btnDenHartPar.Visible = True
                btnToolframe.Visible = True
                btnWorkframe.Visible = True
                ToolStrip3.Visible = True
                sepFrames.Visible = True
            Case selectedSetting.TCPSettings
                btnRoboPar.Visible = True
                btnDenHartPar.Visible = True
                btnFrames.Visible = True
        End Select
    End Sub
    Private Sub _refreshFilename()
        Dim filenameSplit As String() = frmMain.RoboControl.Pref.GetActFilename.Split("\"c)
        lblFilename.Text = $"Geöffnete Parameterdatei: {filenameSplit(filenameSplit.Length - 1)}"
    End Sub
End Class