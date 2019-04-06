Friend Class PanRoboParameter
    ' -----------------------------------------------------------------------------
    ' TODO
    ' -----------------------------------------------------------------------------
    ' Speichern und Laden der Parameter

    ' -----------------------------------------------------------------------------
    ' Init Panel
    ' -----------------------------------------------------------------------------
    Private _initialized As Boolean = False
    Private _actPropView As PropView
    Private Sub PanRoboParameter_Load(sender As Object, e As EventArgs) Handles Me.Load
        _refreshFilename()
        _setPropView(PropView.J1)
    End Sub

    ' -----------------------------------------------------------------------------
    ' Form Control
    ' -----------------------------------------------------------------------------
    Private Sub propGridRoboPar_PropertyValueChanged(sender As Object, e As EventArgs) Handles propGridRoboPar.PropertyValueChanged
        'Objekt aktualisieren
        If _actPropView > 5 Then
            frmMain.RoboControl.Par.SetServoParameter(_actPropView - 6, CType(propGridRoboPar.SelectedObject, ServoParameter))
        Else
            frmMain.RoboControl.Par.SetJointParameter(_actPropView, CType(propGridRoboPar.SelectedObject, JointParameter))
        End If
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
    Private Sub BtnDefaultConfig_Click(sender As Object, e As EventArgs) Handles btnDefaultConfig.Click
        If frmMain.RoboControl.Par.LoadDefaulSettings() Then
            _refreshPropGrid()
            _refreshFilename()
        End If
    End Sub

    ' -----------------------------------------------------------------------------
    ' Help Functions
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
        If _actPropView > 5 Then
            propGridRoboPar.SelectedObject = frmMain.RoboControl.Par.ServoParameter(_actPropView - 6)
        Else
            propGridRoboPar.SelectedObject = frmMain.RoboControl.Par.JointParameter(_actPropView)
        End If
    End Sub
    Private Sub _refreshFilename()
        Dim filenameSplit As String() = frmMain.RoboControl.Par.GetActFilename.Split("\"c)
        lblFilename.Text = $"Geöffnete Parameterdatei: {filenameSplit(filenameSplit.Length - 1)}"
    End Sub
End Class