Friend Class panCtrl
    ' -----------------------------------------------------------------------------
    ' TODO
    ' -----------------------------------------------------------------------------
    ' Speed, Acc, JogInterval in MySettings speichern
    ' alten Jog-Interval für jeden Mode speichern und wiederherstellen
    ' Checkbox für SyncMove
    ' Home & Parkposition anfahren

    Private _tcpMode As Boolean = False
    Private _moveModeDirect As Boolean = False
    Private _posReceived As Boolean = False
    Private _doMoveAfterServoMove As Boolean = False

    ' -----------------------------------------------------------------------------
    ' Init Panel
    ' -----------------------------------------------------------------------------
    Private Sub PanCtrl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cbJogMode.SelectedIndex = 0
        cbMoveMode.SelectedIndex = 0

        _refreshJointTcpMode()
        _hideShowServo()

        AddHandler frmMain.RoboControl.RoboPositionChanged, AddressOf _eNewPos
        AddHandler frmMain.RoboControl.RoboServoChanged, AddressOf _eNewServo
        AddHandler frmMain.RoboControl.RoboParameterChanged, AddressOf _eRoboParameterChanged
        AddHandler frmMain.RoboControl.SerialConnected, AddressOf _eRefresh
        AddHandler frmMain.RoboControl.SerialDisconnected, AddressOf _eRefresh
        AddHandler frmMain.RoboControl.RoboBusy, AddressOf _eRoboBusy
        AddHandler frmMain.RoboControl.RoboRefStateChanged, AddressOf _eRefresh
        AddHandler frmMain.ACLProgram.ProgramFinished, AddressOf _eRefresh
    End Sub

    ' -----------------------------------------------------------------------------
    ' Robot Control
    ' -----------------------------------------------------------------------------
    Private Sub btnMoveStart_Click(sender As Object, e As EventArgs) Handles btnMoveStart.Click
        Dim ServoMoved As Boolean = False

        ' Move Servos
        If _doServo(1, numServ1.Value) Then
            ServoMoved = True
        End If
        If _doServo(2, numServ2.Value) Then
            ServoMoved = True
        End If
        If _doServo(3, numServ3.Value) Then
            ServoMoved = True
        End If

        ' Move Axis
        If ServoMoved Then
            _doMoveAfterServoMove = True
        Else
            _doMove()
        End If
    End Sub
    Private Sub btnCtrl1Dec_Click(sender As Object, e As EventArgs) Handles btnCtrl1Dec.Click
        _doJog(1, True)
    End Sub
    Private Sub btnCtrl1Inc_Click(sender As Object, e As EventArgs) Handles btnCtrl1Inc.Click
        _doJog(1, False)
    End Sub
    Private Sub btnCtrl2Dec_Click(sender As Object, e As EventArgs) Handles btnCtrl2Dec.Click
        _doJog(2, True)
    End Sub
    Private Sub btnCtrl2Inc_Click(sender As Object, e As EventArgs) Handles btnCtrl2Inc.Click
        _doJog(2, False)
    End Sub
    Private Sub btnCtrl3Dec_Click(sender As Object, e As EventArgs) Handles btnCtrl3Dec.Click
        _doJog(3, True)
    End Sub
    Private Sub btnCtrl3Inc_Click(sender As Object, e As EventArgs) Handles btnCtrl3Inc.Click
        _doJog(3, False)
    End Sub
    Private Sub btnCtrl4Dec_Click(sender As Object, e As EventArgs) Handles btnCtrl4Dec.Click
        _doJog(4, True)
    End Sub
    Private Sub btnCtrl4Inc_Click(sender As Object, e As EventArgs) Handles btnCtrl4Inc.Click
        _doJog(4, False)
    End Sub
    Private Sub btnCtrl5Dec_Click(sender As Object, e As EventArgs) Handles btnCtrl5Dec.Click
        _doJog(5, True)
    End Sub
    Private Sub btnCtrl5Inc_Click(sender As Object, e As EventArgs) Handles btnCtrl5Inc.Click
        _doJog(5, False)
    End Sub
    Private Sub btnCtrl6Dec_Click(sender As Object, e As EventArgs) Handles btnCtrl6Dec.Click
        _doJog(6, True)
    End Sub
    Private Sub btnCtrl6Inc_Click(sender As Object, e As EventArgs) Handles btnCtrl6Inc.Click
        _doJog(6, False)
    End Sub
    Private Sub tbCtrlX_MouseUp(sender As Object, e As EventArgs) Handles tbCtrl1.MouseUp, tbCtrl2.MouseUp, tbCtrl3.MouseUp, tbCtrl4.MouseUp, tbCtrl5.MouseUp, tbCtrl6.MouseUp
        If _moveModeDirect Then
            _doMove()
        End If
    End Sub
    Private Sub tbServ1_MouseUp(sender As Object, e As MouseEventArgs) Handles tbServ1.MouseUp
        If _moveModeDirect Then
            _doServo(1, numServ1.Value)
        End If
    End Sub
    Private Sub tbServ2_MouseUp(sender As Object, e As MouseEventArgs) Handles tbServ2.MouseUp
        If _moveModeDirect Then
            _doServo(2, numServ2.Value)
        End If
    End Sub
    Private Sub tbServ3_MouseUp(sender As Object, e As MouseEventArgs) Handles tbServ3.MouseUp
        If _moveModeDirect Then
            _doServo(3, numServ3.Value)
        End If
    End Sub
    Private Sub btnServ1Inc_Click(sender As Object, e As EventArgs) Handles btnServ1Inc.Click
        _doJogServo(1, numServ1, numJogInterval1.Value)
    End Sub
    Private Sub btnServ1Dec_Click(sender As Object, e As EventArgs) Handles btnServ1Dec.Click
        _doJogServo(1, numServ1, -numJogInterval1.Value)
    End Sub
    Private Sub btnServ2Inc_Click(sender As Object, e As EventArgs) Handles btnServ2Inc.Click
        _doJogServo(2, numServ2, numJogInterval1.Value)
    End Sub
    Private Sub btnServ2Dec_Click(sender As Object, e As EventArgs) Handles btnServ2Dec.Click
        _doJogServo(2, numServ2, -numJogInterval1.Value)
    End Sub
    Private Sub btnServ3Inc_Click(sender As Object, e As EventArgs) Handles btnServ3Inc.Click
        _doJogServo(3, numServ3, numJogInterval1.Value)
    End Sub
    Private Sub btnServ3Dec_Click(sender As Object, e As EventArgs) Handles btnServ3Dec.Click
        _doJogServo(3, numServ3, -numJogInterval1.Value)
    End Sub

    ' -----------------------------------------------------------------------------
    ' Form Control
    ' -----------------------------------------------------------------------------
    Private Sub cbMoveMode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbMoveMode.SelectedIndexChanged
        If cbMoveMode.SelectedIndex = 0 Then
            ' getriggert
            _moveModeDirect = False
            btnMoveStart.Visible = True
            tbCtrl1.BackColor = Color.FromKnownColor(KnownColor.ControlLightLight)
            tbCtrl2.BackColor = Color.FromKnownColor(KnownColor.ControlLightLight)
            tbCtrl3.BackColor = Color.FromKnownColor(KnownColor.ControlLightLight)
            tbCtrl4.BackColor = Color.FromKnownColor(KnownColor.ControlLightLight)
            tbCtrl5.BackColor = Color.FromKnownColor(KnownColor.ControlLightLight)
            tbCtrl6.BackColor = Color.FromKnownColor(KnownColor.ControlLightLight)
            tbServ1.BackColor = Color.FromKnownColor(KnownColor.ControlLightLight)
            tbServ2.BackColor = Color.FromKnownColor(KnownColor.ControlLightLight)
            tbServ3.BackColor = Color.FromKnownColor(KnownColor.ControlLightLight)
        Else
            ' direkt
            _moveModeDirect = True
            btnMoveStart.Visible = False
            tbCtrl1.BackColor = Color.Yellow
            tbCtrl2.BackColor = Color.Yellow
            tbCtrl3.BackColor = Color.Yellow
            tbCtrl4.BackColor = Color.Yellow
            tbCtrl5.BackColor = Color.Yellow
            tbCtrl6.BackColor = Color.Yellow
            tbServ1.BackColor = Color.Yellow
            tbServ2.BackColor = Color.Yellow
            tbServ3.BackColor = Color.Yellow
        End If
        _enableDisableElements()
    End Sub

    Private Sub btnJointMode_Click(sender As Object, e As EventArgs) Handles btnJointMode.Click
        _tcpMode = False
        btnMoveStart.Visible = Not _moveModeDirect
        _refreshJointTcpMode()
    End Sub
    Private Sub btnTCPMode_Click(sender As Object, e As EventArgs) Handles btnTCPMode.Click
        _tcpMode = True
        btnMoveStart.Visible = True
        _refreshJointTcpMode()
    End Sub
    Private Sub cbJogMode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbJogMode.SelectedIndexChanged
        numJogInterval1.DecimalPlaces = If(cbJogMode.SelectedIndex = 0, 1, 0)
        numJogInterval1.Maximum = If(cbJogMode.SelectedIndex = 0, 100, 10000)
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
    Private Sub tbServ1_Scroll(sender As Object, e As EventArgs) Handles tbServ1.Scroll
        numServ1.Value = CDec(tbServ1.Value) / CDec(100)
    End Sub
    Private Sub tbServ2_Scroll(sender As Object, e As EventArgs) Handles tbServ2.Scroll
        numServ2.Value = CDec(tbServ2.Value) / CDec(100)
    End Sub
    Private Sub tbServ3_Scroll(sender As Object, e As EventArgs) Handles tbServ3.Scroll
        numServ3.Value = CDec(tbServ3.Value) / CDec(100)
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
    Private Sub numServ1_ValueChanged(sender As Object, e As EventArgs) Handles numServ1.ValueChanged
        tbServ1.Value = CInt(numServ1.Value * 100.0)
    End Sub
    Private Sub numServ2_ValueChanged(sender As Object, e As EventArgs) Handles numServ2.ValueChanged
        tbServ2.Value = CInt(numServ2.Value * 100.0)
    End Sub
    Private Sub numServ3_ValueChanged(sender As Object, e As EventArgs) Handles numServ3.ValueChanged
        tbServ3.Value = CInt(numServ3.Value * 100.0)
    End Sub

    ' -----------------------------------------------------------------------------
    ' Private
    ' -----------------------------------------------------------------------------
    Private Sub _refreshJointTcpMode()
        btnJointMode.Visible = _tcpMode
        btnTCPMode.Visible = Not _tcpMode

        lblCtrl1.Text = If(_tcpMode, "X:", "J1:")
        lblCtrl2.Text = If(_tcpMode, "Y:", "J2:")
        lblCtrl3.Text = If(_tcpMode, "Z:", "J3:")
        lblCtrl4.Text = If(_tcpMode, "yaw:", "J4:")
        lblCtrl5.Text = If(_tcpMode, "pitch:", "J5:")
        lblCtrl6.Text = If(_tcpMode, "roll:", "J6:")
        lblCtrl1Unit.Text = If(_tcpMode, "mm", "°")
        lblCtrl2Unit.Text = If(_tcpMode, "mm", "°")
        lblCtrl3Unit.Text = If(_tcpMode, "mm", "°")

        cbJogMode.Visible = Not _tcpMode
        lblMove.Visible = Not _tcpMode
        cbMoveMode.Visible = Not _tcpMode
        numJogInterval2.Visible = _tcpMode
        lblUnitDeg.Visible = _tcpMode
        lblUnitMm.Visible = _tcpMode

        tbCtrl1.Visible = Not _tcpMode
        tbCtrl2.Visible = Not _tcpMode
        tbCtrl3.Visible = Not _tcpMode
        tbCtrl4.Visible = Not _tcpMode
        tbCtrl5.Visible = Not _tcpMode
        tbCtrl6.Visible = Not _tcpMode
        tbServ1.Visible = Not _tcpMode
        tbServ2.Visible = Not _tcpMode
        tbServ3.Visible = Not _tcpMode
        TableLayoutPanel.ColumnStyles(3).Width = If(_tcpMode, 0, 60)

        ' TODO: alten Jog-Interval für jeden Mode speicher und wiederherstellen
        ' das gleiche auch mit Geschwindigkeit und Beschleunigung!
        numJogInterval1.Value = If(_tcpMode, 20, 5)

        'Min/Max Werte aktualisieren
        _setMinMaxValues()
        'Pos Werte aktualisieren
        _setPosValues()
        'Felder aktualisieren
        _enableDisableElements()
    End Sub
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
            With frmMain.RoboControl.Pref
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
    Private Sub _hideShowServo()
        If InvokeRequired Then
            Invoke(Sub() _hideShowServo())
            Return
        End If

        Dim tmpVisible As Boolean

        tmpVisible = frmMain.RoboControl.Pref.ServoParameter(0).Available
        lblServo1.Visible = tmpVisible
        numServ1.Visible = tmpVisible
        lblSrv1Unit.Visible = tmpVisible
        tbServ1.Visible = tmpVisible
        btnServ1Dec.Visible = tmpVisible
        btnServ1Inc.Visible = tmpVisible
        TableLayoutPanel.RowStyles(7).Height = If(tmpVisible, 30, 0)

        tmpVisible = frmMain.RoboControl.Pref.ServoParameter(1).Available
        lblServo2.Visible = tmpVisible
        numServ2.Visible = tmpVisible
        lblSrv2Unit.Visible = tmpVisible
        tbServ2.Visible = tmpVisible
        btnServ2Dec.Visible = tmpVisible
        btnServ2Inc.Visible = tmpVisible
        TableLayoutPanel.RowStyles(8).Height = If(tmpVisible, 30, 0)

        tmpVisible = frmMain.RoboControl.Pref.ServoParameter(2).Available
        lblServo3.Visible = tmpVisible
        numServ3.Visible = tmpVisible
        lblSrv3Unit.Visible = tmpVisible
        tbServ3.Visible = tmpVisible
        btnServ3Dec.Visible = tmpVisible
        btnServ3Inc.Visible = tmpVisible
        TableLayoutPanel.RowStyles(9).Height = If(tmpVisible, 30, 0)
    End Sub
    Private Sub _enableDisableElements()
        If InvokeRequired Then
            Invoke(Sub() _enableDisableElements())
            Return
        End If

        Dim tmpEnabled As Boolean
        ' Im TCP Mode nur aktiv, wenn alle Achsen referenziert sind!
        tmpEnabled = SerialConnected And Not RobotBusy And Not ProgramRunning And (frmMain.RoboControl.AllRefOkay Or Not _tcpMode)

        btnCtrl1Dec.Enabled = tmpEnabled And frmMain.RoboControl.RefOkay(0)
        btnCtrl1Inc.Enabled = tmpEnabled And frmMain.RoboControl.RefOkay(0)
        numCtrl1.Enabled = tmpEnabled And frmMain.RoboControl.RefOkay(0) And Not _moveModeDirect
        tbCtrl1.Enabled = tmpEnabled And frmMain.RoboControl.RefOkay(0)
        btnCtrl2Dec.Enabled = tmpEnabled And frmMain.RoboControl.RefOkay(1)
        btnCtrl2Inc.Enabled = tmpEnabled And frmMain.RoboControl.RefOkay(1)
        numCtrl2.Enabled = tmpEnabled And frmMain.RoboControl.RefOkay(1) And Not _moveModeDirect
        tbCtrl2.Enabled = tmpEnabled And frmMain.RoboControl.RefOkay(1)
        btnCtrl3Dec.Enabled = tmpEnabled And frmMain.RoboControl.RefOkay(2)
        btnCtrl3Inc.Enabled = tmpEnabled And frmMain.RoboControl.RefOkay(2)
        numCtrl3.Enabled = tmpEnabled And frmMain.RoboControl.RefOkay(2) And Not _moveModeDirect
        tbCtrl3.Enabled = tmpEnabled And frmMain.RoboControl.RefOkay(2)
        btnCtrl4Dec.Enabled = tmpEnabled And frmMain.RoboControl.RefOkay(3)
        btnCtrl4Inc.Enabled = tmpEnabled And frmMain.RoboControl.RefOkay(3)
        numCtrl4.Enabled = tmpEnabled And frmMain.RoboControl.RefOkay(3) And Not _moveModeDirect
        tbCtrl4.Enabled = tmpEnabled And frmMain.RoboControl.RefOkay(3)
        btnCtrl5Dec.Enabled = tmpEnabled And frmMain.RoboControl.RefOkay(4)
        btnCtrl5Inc.Enabled = tmpEnabled And frmMain.RoboControl.RefOkay(4)
        numCtrl5.Enabled = tmpEnabled And frmMain.RoboControl.RefOkay(4) And Not _moveModeDirect
        tbCtrl5.Enabled = tmpEnabled And frmMain.RoboControl.RefOkay(4)
        btnCtrl6Dec.Enabled = tmpEnabled And frmMain.RoboControl.RefOkay(5)
        btnCtrl6Inc.Enabled = tmpEnabled And frmMain.RoboControl.RefOkay(5)
        numCtrl6.Enabled = tmpEnabled And frmMain.RoboControl.RefOkay(5) And Not _moveModeDirect
        tbCtrl6.Enabled = tmpEnabled And frmMain.RoboControl.RefOkay(5)

        ' Start Button (Sichtbar wenn mindestens eine Achse referenziert ist)
        If tmpEnabled Then
            tmpEnabled = False
            For i = 0 To 5
                If frmMain.RoboControl.RefOkay(i) Then
                    tmpEnabled = True
                    Exit For
                End If
            Next
        End If
        btnMoveStart.Enabled = tmpEnabled

        ' Servos
        tmpEnabled = SerialConnected And Not RobotBusy And Not ProgramRunning

        btnServ1Dec.Enabled = tmpEnabled
        btnServ1Inc.Enabled = tmpEnabled
        numServ1.Enabled = tmpEnabled
        tbServ1.Enabled = tmpEnabled
        btnServ2Dec.Enabled = tmpEnabled
        btnServ2Inc.Enabled = tmpEnabled
        numServ2.Enabled = tmpEnabled
        tbServ2.Enabled = tmpEnabled
        btnServ3Dec.Enabled = tmpEnabled
        btnServ3Inc.Enabled = tmpEnabled
        numServ3.Enabled = tmpEnabled
        tbServ3.Enabled = tmpEnabled
    End Sub
    Private Sub _setPosValues(Optional srv As Boolean = False)
        If InvokeRequired Then
            Invoke(Sub() _setPosValues(srv))
            Return
        End If

        If _posReceived And Not srv Then ' Nur wenn es welche gibt
            If _tcpMode Then
                numCtrl1.Value = CDec(frmMain.RoboControl.PosCart.X)
                numCtrl2.Value = CDec(frmMain.RoboControl.PosCart.Y)
                numCtrl3.Value = CDec(frmMain.RoboControl.PosCart.Z)
                numCtrl4.Value = CDec(frmMain.RoboControl.PosCart.Yaw)
                numCtrl5.Value = CDec(frmMain.RoboControl.PosCart.Pitch)
                numCtrl6.Value = CDec(frmMain.RoboControl.PosCart.Roll)
            Else
                numCtrl1.Value = CDec(frmMain.RoboControl.PosJoint.J1)
                numCtrl2.Value = CDec(frmMain.RoboControl.PosJoint.J2)
                numCtrl3.Value = CDec(frmMain.RoboControl.PosJoint.J3)
                numCtrl4.Value = CDec(frmMain.RoboControl.PosJoint.J4)
                numCtrl5.Value = CDec(frmMain.RoboControl.PosJoint.J5)
                numCtrl6.Value = CDec(frmMain.RoboControl.PosJoint.J6)
            End If
        End If
        ' Servos
        If srv Then
            If frmMain.RoboControl.Pref.ServoParameter(0).Available Then
                numServ1.Value = CDec(frmMain.RoboControl.PosServo(0))
            End If
            If frmMain.RoboControl.Pref.ServoParameter(1).Available Then
                numServ1.Value = CDec(frmMain.RoboControl.PosServo(1))
            End If
            If frmMain.RoboControl.Pref.ServoParameter(2).Available Then
                numServ1.Value = CDec(frmMain.RoboControl.PosServo(2))
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
    Private Sub _setSpeedAndAcc()
        frmMain.RoboControl.SetSpeedAndAcc(numSpeed.Value, numAcc.Value)
    End Sub
    Private Sub _doMove()
        If InvokeRequired Then
            Invoke(Sub() _doMove())
            Return
        End If

        _setSpeedAndAcc()
        If _tcpMode Then
            frmMain.RoboControl.DoTCPMov(numCtrl1.Value, numCtrl2.Value, numCtrl3.Value, numCtrl4.Value, numCtrl5.Value, numCtrl6.Value)
        Else
            frmMain.RoboControl.DoJointMov(True, numCtrl1.Value, numCtrl2.Value, numCtrl3.Value, numCtrl4.Value, numCtrl5.Value, numCtrl6.Value)
        End If
    End Sub
    Private Sub _doJog(nr As Int32, neg As Boolean)
        Dim jogInt1 As Double = If(neg, numJogInterval1.Value * -1, numJogInterval1.Value)
        Dim jogInt2 As Double = If(neg, numJogInterval2.Value * -1, numJogInterval2.Value)
        _setSpeedAndAcc()
        If _tcpMode Then
            If nr <= 3 Then
                'mm
                frmMain.RoboControl.DoJogCart(nr, jogInt1)
            Else
                'Grad
                frmMain.RoboControl.DoJogCart(nr, jogInt2)
            End If
        Else
            If cbJogMode.SelectedIndex = 0 Then
                ' Degree
                frmMain.RoboControl.DoJog(nr, jogInt1)
            Else
                ' Steps
                frmMain.RoboControl.DoJog(nr, CInt(jogInt1))
            End If
        End If
    End Sub

    Private Function _doServo(nr As Int32, prc As Double) As Boolean
        If frmMain.RoboControl.Pref.ServoParameter(nr - 1).Available And frmMain.RoboControl.PosServo(nr - 1) <> prc Then
            If frmMain.RoboControl.MoveServoPrc(nr, prc) Then Return True
        End If
        Return False
    End Function
    Private Sub _doJogServo(srvNr As Int32, numUpDown As NumericUpDown, increment As Decimal)
        If numUpDown.Value = numUpDown.Minimum And increment <= 0 Then Return
        If numUpDown.Value = numUpDown.Maximum And increment > 0 Then Return

        Dim newVal As Double = numUpDown.Value + increment

        If newVal < numUpDown.Minimum Then
            numUpDown.Value = numUpDown.Minimum
        ElseIf newVal > numUpDown.Maximum Then
            numUpDown.Value = numUpDown.Maximum
        Else
            numUpDown.Value = CDec(newVal)
        End If
        _doServo(srvNr, numUpDown.Value)
    End Sub

    ' -----------------------------------------------------------------------------
    ' Events
    ' -----------------------------------------------------------------------------
    Private Sub _eNewPos()
        _posReceived = True
        _setPosValues()
    End Sub

    Private Sub _eNewServo()
        _setPosValues(True)
    End Sub

    Private Sub _eRoboParameterChanged(parameterChanged As Settings.ParameterChangedParameter)
        Dim all As Boolean = parameterChanged = Settings.ParameterChangedParameter.All
        If parameterChanged = Settings.ParameterChangedParameter.Joint Or all Then
            _setMinMaxValues()
        End If
        If parameterChanged = Settings.ParameterChangedParameter.Servo Or all Then
            _hideShowServo()
            _setPosValues(True)
        End If
        If parameterChanged = Settings.ParameterChangedParameter.DenavitHartenbergParameter Or
                parameterChanged = Settings.ParameterChangedParameter.Toolframe Or
                parameterChanged = Settings.ParameterChangedParameter.Workframe Or all Then
            _setPosValues()
        End If
    End Sub
    Private Sub _eRefresh()
        _enableDisableElements()
    End Sub
    Private Sub _eRoboBusy(busy As Boolean)
        ' Achsen Bewegen falls Servos erst angesteuert wurden
        If _doMoveAfterServoMove And busy Then
            _doMoveAfterServoMove = False
            _doMove()
        End If
        _enableDisableElements()
    End Sub

End Class