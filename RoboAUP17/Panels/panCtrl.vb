Friend Class panCtrl
    ' -----------------------------------------------------------------------------
    ' TODO
    ' -----------------------------------------------------------------------------
    ' Bewegungsbefehle an RoboControl senden + Geschwindigkeit und Beschleunigung setzen
    ' Während Bewegung Buttons deaktivieren???
    Private _tcpMode As Boolean = False
    Private _posReceived As Boolean = False

    ' -----------------------------------------------------------------------------
    ' Init Panel
    ' -----------------------------------------------------------------------------
    Private Sub PanCtrl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cbJointOrTCP.SelectedIndex = 0
        cbJogMode.SelectedIndex = 0
        cbMoveMode.SelectedIndex = 0

        _setMinMaxValues()

        AddHandler frmMain.RoboControl.RoboPositionChanged, AddressOf _eNewPos
        AddHandler frmMain.RoboControl.RoboParameterChanged, AddressOf _eRoboParameterChanged
        AddHandler frmMain.RoboControl.SerialDisconnected, AddressOf _eComSerialDisconnected
        AddHandler frmMain.RoboControl.RoboMoveStarted, AddressOf _eRoboMoveStarted
        AddHandler frmMain.RoboControl.RoboMoveFinished, AddressOf _eRoboMoveFinished
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
            _tcpMode = False
            btnMoveStart.Visible = (cbMoveMode.SelectedIndex = 0)
            cbJogMode.Visible = True
            lblMove.Visible = True
            cbMoveMode.Visible = True
            numJogInterval2.Visible = False
            lblUnitDeg.Visible = False
            lblUnitMm.Visible = False
            lblCtrl1.Text = "J1:"
            lblCtrl2.Text = "J2:"
            lblCtrl3.Text = "J3:"
            lblCtrl4.Text = "J4:"
            lblCtrl5.Text = "J5:"
            lblCtrl6.Text = "J6:"

            tbCtrl1.Visible = True
            tbCtrl2.Visible = True
            tbCtrl3.Visible = True
            tbCtrl4.Visible = True
            tbCtrl5.Visible = True
            tbCtrl6.Visible = True
            TableLayoutPanel.ColumnStyles(1).Width = 25
            TableLayoutPanel.ColumnStyles(2).Width = 50
            TableLayoutPanel.ColumnStyles(3).Width = 12.5
            TableLayoutPanel.ColumnStyles(4).Width = 12.5

            'Min/Max Werte aktualisieren
            _setMinMaxValues()
            'Pos Werte aktualisieren
            _setPosValues()
        Else
            ' TCP Mode
            _tcpMode = True
            btnMoveStart.Visible = True
            cbJogMode.Visible = False
            lblMove.Visible = False
            cbMoveMode.Visible = False
            numJogInterval2.Visible = True
            lblUnitDeg.Visible = True
            lblUnitMm.Visible = True
            lblCtrl1.Text = "X:"
            lblCtrl2.Text = "Y:"
            lblCtrl3.Text = "Z:"
            lblCtrl4.Text = "yaw:"
            lblCtrl5.Text = "pitch:"
            lblCtrl6.Text = "roll:"

            tbCtrl1.Visible = False
            tbCtrl2.Visible = False
            tbCtrl3.Visible = False
            tbCtrl4.Visible = False
            tbCtrl5.Visible = False
            tbCtrl6.Visible = False
            TableLayoutPanel.ColumnStyles(1).Width = 40
            TableLayoutPanel.ColumnStyles(2).Width = 0
            TableLayoutPanel.ColumnStyles(3).Width = 30
            TableLayoutPanel.ColumnStyles(4).Width = 30

            'Min/Max Werte aktualisieren
            _setMinMaxValues()
            'Pos Werte aktualisieren
            _setPosValues()
        End If
    End Sub
    ' Trackbars
    Private Sub tbCtrl1_Scroll(sender As Object, e As EventArgs) Handles tbCtrl1.Scroll
        numCtrl1.Value = CDec(tbCtrl1.Value) / CDec(100)
    End Sub
    Private Sub tbCtrl2_Scroll(sender As Object, e As EventArgs) Handles tbCtrl2.Scroll
        numCtrl2.Value = CDec(tbCtrl2.Value) / CDec(100)
    End Sub
    Private Sub tbCtrl3_Scroll(sender As Object, e As EventArgs) Handles tbCtrl3.Scroll
        numCtrl3.Value = CDec(tbCtrl3.Value) / CDec(100)
    End Sub
    Private Sub tbCtrl4_Scroll(sender As Object, e As EventArgs) Handles tbCtrl4.Scroll
        numCtrl4.Value = CDec(tbCtrl4.Value) / CDec(100)
    End Sub
    Private Sub tbCtrl5_Scroll(sender As Object, e As EventArgs) Handles tbCtrl5.Scroll
        numCtrl5.Value = CDec(tbCtrl5.Value) / CDec(100)
    End Sub
    Private Sub tbCtrl6_Scroll(sender As Object, e As EventArgs) Handles tbCtrl6.Scroll
        numCtrl6.Value = CDec(tbCtrl6.Value) / CDec(100)
    End Sub

    ' Num UpDowns
    Private Sub numCtrl1_ValueChanged(sender As Object, e As EventArgs) Handles numCtrl1.ValueChanged
        If Not _tcpMode Then tbCtrl1.Value = CInt(numCtrl1.Value * 100.0)
    End Sub
    Private Sub numCtrl2_ValueChanged(sender As Object, e As EventArgs) Handles numCtrl2.ValueChanged
        If Not _tcpMode Then tbCtrl2.Value = CInt(numCtrl2.Value * 100.0)
    End Sub
    Private Sub numCtrl3_ValueChanged(sender As Object, e As EventArgs) Handles numCtrl3.ValueChanged
        If Not _tcpMode Then tbCtrl3.Value = CInt(numCtrl3.Value * 100.0)
    End Sub
    Private Sub numCtrl4_ValueChanged(sender As Object, e As EventArgs) Handles numCtrl4.ValueChanged
        If Not _tcpMode Then tbCtrl4.Value = CInt(numCtrl4.Value * 100.0)
    End Sub
    Private Sub numCtrl5_ValueChanged(sender As Object, e As EventArgs) Handles numCtrl5.ValueChanged
        If Not _tcpMode Then tbCtrl5.Value = CInt(numCtrl5.Value * 100.0)
    End Sub
    Private Sub numCtrl6_ValueChanged(sender As Object, e As EventArgs) Handles numCtrl6.ValueChanged
        If Not _tcpMode Then tbCtrl6.Value = CInt(numCtrl6.Value * 100.0)
    End Sub

    ' -----------------------------------------------------------------------------
    ' Helper Functions
    ' -----------------------------------------------------------------------------
    Private Sub _setMinMaxValues()
        If _tcpMode Then
            numCtrl1.Minimum = -1000
            numCtrl1.Maximum = 2000
            numCtrl2.Minimum = -1000
            numCtrl2.Maximum = 2000
            numCtrl3.Minimum = -1000
            numCtrl3.Maximum = 2000
            numCtrl4.Minimum = -360
            numCtrl4.Maximum = 360
            numCtrl5.Minimum = -360
            numCtrl5.Maximum = 360
            numCtrl6.Minimum = -360
            numCtrl6.Maximum = 360
        Else
            With frmMain.RoboControl.Par
                'Slider min/max
                _setTbMaxMin(tbCtrl1, CInt(.JointParameter(0).MechMaxAngle * 100.0), CInt(.JointParameter(0).MechMinAngle * 100.0))
                _setTbMaxMin(tbCtrl2, CInt(.JointParameter(1).MechMaxAngle * 100.0), CInt(.JointParameter(1).MechMinAngle * 100.0))
                _setTbMaxMin(tbCtrl3, CInt(.JointParameter(2).MechMaxAngle * 100.0), CInt(.JointParameter(2).MechMinAngle * 100.0))
                _setTbMaxMin(tbCtrl4, CInt(.JointParameter(3).MechMaxAngle * 100.0), CInt(.JointParameter(3).MechMinAngle * 100.0))
                _setTbMaxMin(tbCtrl5, CInt(.JointParameter(4).MechMaxAngle * 100.0), CInt(.JointParameter(4).MechMinAngle * 100.0))
                _setTbMaxMin(tbCtrl6, CInt(.JointParameter(5).MechMaxAngle * 100.0), CInt(.JointParameter(5).MechMinAngle * 100.0))
                'NumUpDown min/max
                _setNumMaxMin(numCtrl1, CDec(.JointParameter(0).MechMaxAngle), CDec(.JointParameter(0).MechMinAngle))
                _setNumMaxMin(numCtrl2, CDec(.JointParameter(1).MechMaxAngle), CDec(.JointParameter(1).MechMinAngle))
                _setNumMaxMin(numCtrl3, CDec(.JointParameter(2).MechMaxAngle), CDec(.JointParameter(2).MechMinAngle))
                _setNumMaxMin(numCtrl4, CDec(.JointParameter(3).MechMaxAngle), CDec(.JointParameter(3).MechMinAngle))
                _setNumMaxMin(numCtrl5, CDec(.JointParameter(4).MechMaxAngle), CDec(.JointParameter(4).MechMinAngle))
                _setNumMaxMin(numCtrl6, CDec(.JointParameter(5).MechMaxAngle), CDec(.JointParameter(5).MechMinAngle))
            End With
        End If
    End Sub
    Private Sub _enableDisableElements(disable As Boolean)
        If InvokeRequired Then
            Invoke(Sub() _enableDisableElements(disable))
            Return
        End If

        btnCtrl1Dec.Enabled = Not disable And frmMain.RoboControl.RefOkay(0)
        btnCtrl1Inc.Enabled = Not disable And frmMain.RoboControl.RefOkay(0)
        numCtrl1.Enabled = Not disable And frmMain.RoboControl.RefOkay(0)
        tbCtrl1.Enabled = Not disable And frmMain.RoboControl.RefOkay(0)
        btnCtrl2Dec.Enabled = Not disable And frmMain.RoboControl.RefOkay(1)
        btnCtrl2Inc.Enabled = Not disable And frmMain.RoboControl.RefOkay(1)
        numCtrl2.Enabled = Not disable And frmMain.RoboControl.RefOkay(1)
        tbCtrl2.Enabled = Not disable And frmMain.RoboControl.RefOkay(1)
        btnCtrl3Dec.Enabled = Not disable And frmMain.RoboControl.RefOkay(2)
        btnCtrl3Inc.Enabled = Not disable And frmMain.RoboControl.RefOkay(2)
        numCtrl3.Enabled = Not disable And frmMain.RoboControl.RefOkay(2)
        tbCtrl3.Enabled = Not disable And frmMain.RoboControl.RefOkay(2)
        btnCtrl4Dec.Enabled = Not disable And frmMain.RoboControl.RefOkay(3)
        btnCtrl4Inc.Enabled = Not disable And frmMain.RoboControl.RefOkay(3)
        numCtrl4.Enabled = Not disable And frmMain.RoboControl.RefOkay(3)
        tbCtrl4.Enabled = Not disable And frmMain.RoboControl.RefOkay(3)
        btnCtrl5Dec.Enabled = Not disable And frmMain.RoboControl.RefOkay(4)
        btnCtrl5Inc.Enabled = Not disable And frmMain.RoboControl.RefOkay(4)
        numCtrl5.Enabled = Not disable And frmMain.RoboControl.RefOkay(4)
        tbCtrl5.Enabled = Not disable And frmMain.RoboControl.RefOkay(4)
        btnCtrl6Dec.Enabled = Not disable And frmMain.RoboControl.RefOkay(5)
        btnCtrl6Inc.Enabled = Not disable And frmMain.RoboControl.RefOkay(5)
        numCtrl6.Enabled = Not disable And frmMain.RoboControl.RefOkay(5)
        tbCtrl6.Enabled = Not disable And frmMain.RoboControl.RefOkay(5)

        ' Start Button
        Dim tmpEnabled As Boolean = False
        If Not disable Then
            For i = 0 To 5
                If frmMain.RoboControl.RefOkay(i) Then
                    tmpEnabled = True
                    Exit For
                End If
            Next
        End If
        btnMoveStart.Enabled = tmpEnabled
    End Sub
    Private Sub _setPosValues()
        If InvokeRequired Then
            Invoke(Sub() _setPosValues())
            Return
        End If

        If _posReceived Then ' Nur wenn es welche gibt
            If _tcpMode Then
                numCtrl1.Value = CDec(frmMain.RoboControl.PosCart.X)
                numCtrl2.Value = CDec(frmMain.RoboControl.PosCart.Y)
                numCtrl3.Value = CDec(frmMain.RoboControl.PosCart.Z)
                numCtrl4.Value = CDec(frmMain.RoboControl.PosCart.yaw)
                numCtrl5.Value = CDec(frmMain.RoboControl.PosCart.pitch)
                numCtrl6.Value = CDec(frmMain.RoboControl.PosCart.roll)
            Else
                numCtrl1.Value = CDec(frmMain.RoboControl.PosJoint.J1)
                numCtrl2.Value = CDec(frmMain.RoboControl.PosJoint.J2)
                numCtrl3.Value = CDec(frmMain.RoboControl.PosJoint.J3)
                numCtrl4.Value = CDec(frmMain.RoboControl.PosJoint.J4)
                numCtrl5.Value = CDec(frmMain.RoboControl.PosJoint.J5)
                numCtrl6.Value = CDec(frmMain.RoboControl.PosJoint.J6)
            End If
        End If
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

        _posReceived = True
        _setPosValues()

        _enableDisableElements(False)
    End Sub

    Private Sub _eRoboParameterChanged(joint As Boolean, servo As Boolean, dh As Boolean)
        If joint Then
            _setMinMaxValues()
        End If
    End Sub

    Private Sub _eComSerialDisconnected()
        _enableDisableElements(True)
    End Sub

    Private Sub _eRoboMoveStarted()
        _enableDisableElements(True)
    End Sub

    Private Sub _eRoboMoveFinished()
        _enableDisableElements(False)
    End Sub
End Class