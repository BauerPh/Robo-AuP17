Public Class panRoboParameter
    ' -----------------------------------------------------------------------------
    ' TODO
    ' -----------------------------------------------------------------------------
    ' ...

    ' -----------------------------------------------------------------------------
    ' Init Panel
    ' -----------------------------------------------------------------------------
    Private _initialized As Boolean = False
    Private _actBtnNr As btnNr
    Private Sub panRoboParameter_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        If Not _initialized Then
            _initialized = True
            propGridRoboPar.SelectedObject = frmMain.roboControl.Par.JointParameter(0)
            checkButton(btnNr.J1)
        End If
    End Sub

    Private Sub propGridRoboPar_PropertyValueChanged(sender As Object, e As EventArgs) Handles propGridRoboPar.PropertyValueChanged
        'Objekt aktualisieren
        If _actBtnNr > 5 Then
            frmMain.roboControl.Par.ServoParameter(_actBtnNr - 6) = CType(propGridRoboPar.SelectedObject, ServoParameter)
        Else
            frmMain.roboControl.Par.JointParameter(_actBtnNr) = CType(propGridRoboPar.SelectedObject, JointParameter)
        End If
    End Sub

    ' -----------------------------------------------------------------------------
    ' Form Control
    ' -----------------------------------------------------------------------------
    Private Sub btnJ1_Click(sender As Object, e As EventArgs) Handles btnJ1.Click
        propGridRoboPar.SelectedObject = frmMain.roboControl.Par.JointParameter(0)
        checkButton(btnNr.J1)
    End Sub

    Private Sub btnJ2_Click(sender As Object, e As EventArgs) Handles btnJ2.Click
        propGridRoboPar.SelectedObject = frmMain.roboControl.Par.JointParameter(1)
        checkButton(btnNr.J2)
    End Sub

    Private Sub btnJ3_Click(sender As Object, e As EventArgs) Handles btnJ3.Click
        propGridRoboPar.SelectedObject = frmMain.roboControl.Par.JointParameter(2)
        checkButton(btnNr.J3)
    End Sub

    Private Sub btnJ4_Click(sender As Object, e As EventArgs) Handles btnJ4.Click
        propGridRoboPar.SelectedObject = frmMain.roboControl.Par.JointParameter(3)
        checkButton(btnNr.J4)
    End Sub

    Private Sub btnJ5_Click(sender As Object, e As EventArgs) Handles btnJ5.Click
        propGridRoboPar.SelectedObject = frmMain.roboControl.Par.JointParameter(4)
        checkButton(btnNr.J5)
    End Sub

    Private Sub btnJ6_Click(sender As Object, e As EventArgs) Handles btnJ6.Click
        propGridRoboPar.SelectedObject = frmMain.roboControl.Par.JointParameter(5)
        checkButton(btnNr.J6)
    End Sub

    Private Sub btnServo1_Click(sender As Object, e As EventArgs) Handles btnServo1.Click
        propGridRoboPar.SelectedObject = frmMain.roboControl.Par.ServoParameter(0)
        checkButton(btnNr.Servo1)
    End Sub

    Private Sub btnServo2_Click(sender As Object, e As EventArgs) Handles btnServo2.Click
        propGridRoboPar.SelectedObject = frmMain.roboControl.Par.ServoParameter(1)
        checkButton(btnNr.Servo2)
    End Sub

    Private Sub btnServo3_Click(sender As Object, e As EventArgs) Handles btnServo3.Click
        propGridRoboPar.SelectedObject = frmMain.roboControl.Par.ServoParameter(2)
        checkButton(btnNr.Servo3)
    End Sub

    ' -----------------------------------------------------------------------------
    ' Help Functions
    ' -----------------------------------------------------------------------------
    Private Enum btnNr
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
    Private Sub checkButton(nr As btnNr)
        _actBtnNr = nr
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
            Case btnNr.J1
                btnJ1.Checked = True
            Case btnNr.J2
                btnJ2.Checked = True
            Case btnNr.J3
                btnJ3.Checked = True
            Case btnNr.J4
                btnJ4.Checked = True
            Case btnNr.J5
                btnJ5.Checked = True
            Case btnNr.J6
                btnJ6.Checked = True
            Case btnNr.Servo1
                btnServo1.Checked = True
            Case btnNr.Servo2
                btnServo2.Checked = True
            Case btnNr.Servo3
                btnServo3.Checked = True
        End Select
    End Sub
End Class