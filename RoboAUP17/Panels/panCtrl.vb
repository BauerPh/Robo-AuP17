
Friend Class PanCtrl
    ' -----------------------------------------------------------------------------
    ' TODO
    ' -----------------------------------------------------------------------------
    ' Slider Bewegung ändert numUpDown und umgekehrt!
    ' Min, Max-Werte für Slider aus Parameter holen, 
    '   dazu auch ein ParameterChangedEvent (in RobotParamter, Property Setter Funktion!) implementieren!
    '   Auch eine Setter/Getter Funktion in RoboControl.vb nötig, damit RoboParameter nicht Public sein müssen
    ' Slider, Buttons, Bewegungsbefehle an RoboControl senden
    ' Slider, Nums updaten wenn RoboControl sich ändert (Ereignisse, Callbacks??)

    ' -----------------------------------------------------------------------------
    ' Init Panel
    ' -----------------------------------------------------------------------------
    Private Sub PanCtrl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cbJointOrTCP.SelectedIndex = 0
        cbJogMode.SelectedIndex = 0
        cbMoveMode.SelectedIndex = 0

        _setFormValues()

        AddHandler frmMain.RoboControl.RoboPositionChanged, AddressOf _eNewPos
        AddHandler frmMain.RoboControl.RoboParameterChanged, AddressOf _eRoboParameterChanged
    End Sub

    ' -----------------------------------------------------------------------------
    ' Form Control
    ' -----------------------------------------------------------------------------
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
            lblCtrl1.Text = "J1:"
            lblCtrl2.Text = "J2:"
            lblCtrl3.Text = "J3:"
            lblCtrl4.Text = "J4:"
            lblCtrl5.Text = "J5:"
            lblCtrl6.Text = "J6:"
        Else
            ' TCP Mode
            cbJogMode.Visible = False
            lblCtrl1.Text = "X:"
            lblCtrl2.Text = "Y:"
            lblCtrl3.Text = "Z:"
            lblCtrl4.Text = "yaw:"
            lblCtrl5.Text = "pitch:"
            lblCtrl6.Text = "roll:"
        End If
    End Sub

    ' -----------------------------------------------------------------------------
    ' Helper Functions
    ' -----------------------------------------------------------------------------
    Private Sub _setFormValues()
        With frmMain.RoboControl.Par
            'Slider min/max
            _setTbMaxMin(tbCtrl1, CInt(.JointParameter(0).MechMaxAngle * 100.0), CInt(.JointParameter(0).MechMinAngle))
            _setTbMaxMin(tbCtrl2, CInt(.JointParameter(1).MechMaxAngle * 100.0), CInt(.JointParameter(1).MechMinAngle))
            _setTbMaxMin(tbCtrl3, CInt(.JointParameter(2).MechMaxAngle * 100.0), CInt(.JointParameter(2).MechMinAngle))
            _setTbMaxMin(tbCtrl4, CInt(.JointParameter(3).MechMaxAngle * 100.0), CInt(.JointParameter(3).MechMinAngle))
            _setTbMaxMin(tbCtrl5, CInt(.JointParameter(4).MechMaxAngle * 100.0), CInt(.JointParameter(4).MechMinAngle))
            _setTbMaxMin(tbCtrl6, CInt(.JointParameter(5).MechMaxAngle * 100.0), CInt(.JointParameter(5).MechMinAngle))
            'NumUpDown min/max
            _setNumMaxMin(numCtrl1, CDec(.JointParameter(0).MechMaxAngle), CDec(.JointParameter(0).MechMinAngle))
            _setNumMaxMin(numCtrl2, CDec(.JointParameter(1).MechMaxAngle), CDec(.JointParameter(1).MechMinAngle))
            _setNumMaxMin(numCtrl3, CDec(.JointParameter(2).MechMaxAngle), CDec(.JointParameter(2).MechMinAngle))
            _setNumMaxMin(numCtrl4, CDec(.JointParameter(3).MechMaxAngle), CDec(.JointParameter(3).MechMinAngle))
            _setNumMaxMin(numCtrl5, CDec(.JointParameter(4).MechMaxAngle), CDec(.JointParameter(4).MechMinAngle))
            _setNumMaxMin(numCtrl6, CDec(.JointParameter(5).MechMaxAngle), CDec(.JointParameter(5).MechMinAngle))
        End With
    End Sub
    Private Sub _setNumMaxMin(ByRef num As NumericUpDown, max As Decimal, min As Decimal)
        If num.Value < min Then
            num.Value = min
        End If
        num.Minimum = min
        If num.Value > max Then
            num.Value = max
        End If
        num.Maximum = max
    End Sub

    Private Sub _setTbMaxMin(ByRef tb As TrackBar, max As Int32, min As Int32)
        If tb.Value < min Then
            tb.Value = min
        End If
        tb.Minimum = min
        If tb.Value > max Then
            tb.Value = max
        End If
        tb.Maximum = max
    End Sub

    ' -----------------------------------------------------------------------------
    ' Events
    ' -----------------------------------------------------------------------------
    Private Sub _eNewPos()
        If InvokeRequired Then
            Invoke(Sub() _eNewPos())
            Return
        End If
        numCtrl1.Value = CDec(frmMain.RoboControl.PosJoint.Items(0))
        numCtrl2.Value = CDec(frmMain.RoboControl.PosJoint.Items(1))
        numCtrl3.Value = CDec(frmMain.RoboControl.PosJoint.Items(2))
        numCtrl4.Value = CDec(frmMain.RoboControl.PosJoint.Items(3))
        numCtrl5.Value = CDec(frmMain.RoboControl.PosJoint.Items(4))
        numCtrl6.Value = CDec(frmMain.RoboControl.PosJoint.Items(5))
    End Sub
    Private Sub _eRoboParameterChanged(joint As Boolean, servo As Boolean)
        If joint Then
            _setFormValues()
        End If
    End Sub
End Class