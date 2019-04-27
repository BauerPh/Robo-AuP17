Public Class panRoboStatus
    ' -----------------------------------------------------------------------------
    ' TODO
    ' -----------------------------------------------------------------------------
    ' Servos / Greifer
    Private Const _constInRefTooltipText As String = "Achse referenziert"
    Private Const _constNoRefTooltipText As String = "Achse nicht referenziert"
    Private Const _constLimitSwitchActuated As String = "Endschalter betätigt oder nicht angeschlossen"
    Private Const _constLimitSwitchNotActuated As String = "Endschalter nicht betätigt"
    Private Const _constEstopActuated As String = "Nothalt betätigt"
    Private Const _constEstopNotActuated As String = "Nothalt nicht betätigt"

    Private _limitSwitchToolTip As New ToolTip
    Private _eStopToolTip As New ToolTip

    ' -----------------------------------------------------------------------------
    ' Init Panel
    ' -----------------------------------------------------------------------------
    Private Sub panRoboStatus_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim ImgRef As New ImageList
        ImgRef.ImageSize = New Size(22, 22)
        ImgRef.Images.Add(My.Resources.ico_error)
        ImgRef.Images.Add(My.Resources.ico_checked)
        lblJ1HeadingImg.ImageList = ImgRef
        lblJ2HeadingImg.ImageList = ImgRef
        lblJ3HeadingImg.ImageList = ImgRef
        lblJ4HeadingImg.ImageList = ImgRef
        lblJ5HeadingImg.ImageList = ImgRef
        lblJ6HeadingImg.ImageList = ImgRef

        Dim ImgListLS As New ImageList
        ImgListLS.ImageSize = New Size(17, 17)
        ImgListLS.Images.Add(My.Resources.ico_unknown)
        ImgListLS.Images.Add(My.Resources.ico_not_actuated)
        ImgListLS.Images.Add(My.Resources.ico_actuated)
        lblJ1LSState.ImageList = ImgListLS
        lblJ1LSState.ImageIndex = 0
        lblJ2LSState.ImageList = ImgListLS
        lblJ2LSState.ImageIndex = 0
        lblJ3LSState.ImageList = ImgListLS
        lblJ3LSState.ImageIndex = 0
        lblJ4LSState.ImageList = ImgListLS
        lblJ4LSState.ImageIndex = 0
        lblJ5LSState.ImageList = ImgListLS
        lblJ5LSState.ImageIndex = 0
        lblJ6LSState.ImageList = ImgListLS
        lblJ6LSState.ImageIndex = 0

        Dim ImgListES As New ImageList
        ImgListES.ImageSize = New Size(22, 22)
        ImgListES.Images.Add(My.Resources.ico_help)
        ImgListES.Images.Add(My.Resources.ico_estop)
        ImgListES.Images.Add(My.Resources.ico_checked)
        lblESState.ImageList = ImgListES
        lblESState.ImageIndex = 0


        AddHandler frmMain.RoboControl.RoboPositionChanged, AddressOf _eNewPos
        AddHandler frmMain.RoboControl.RoboServoChanged, AddressOf _eNewServo
        AddHandler frmMain.RoboControl.RoboParameterChanged, AddressOf _eRoboParameterChanged
        AddHandler frmMain.RoboControl.RoboRefStateChanged, AddressOf _eRoboRefStateChanged
        AddHandler frmMain.SerialConnectionStateChanged, AddressOf _eSerialConnectionStateChanged
        AddHandler frmMain.RoboControl.LimitSwitchStateChanged, AddressOf _eLimitSwitchStateChanged
        AddHandler frmMain.RoboControl.EmergencyStopStateChanged, AddressOf _eEmergencyStopStateChanged
    End Sub


    Private Sub TableLayoutPanel_CellPaint(sender As Object, e As TableLayoutCellPaintEventArgs) Handles TableLayoutPanel.CellPaint
        Dim lineRow() = {1, 2, 4, 5, 10}
        Dim dottedLineRow() = {2, 5, 8}
        ' Rahmen Zeichnen
        If ((e.Column Mod 2 = 0 And lineRow.Contains(e.Row)) _
            Or (e.Column Mod 4 = 0 And (e.Row = 7 Or e.Row = 8))) _
             And e.Column <> 0 Then
            e.Graphics.DrawLine(Pens.Black, e.CellBounds.Location, New Point(e.CellBounds.Left, e.CellBounds.Bottom))
        End If
        If dottedLineRow.Contains(e.Row) Then
            Dim pen As New Pen(Color.Black)
            pen.DashStyle = Drawing2D.DashStyle.Dot
            e.Graphics.DrawLine(pen, e.CellBounds.Location, New Point(e.CellBounds.Right, e.CellBounds.Top))
        End If
    End Sub

    ' -----------------------------------------------------------------------------
    ' Private
    ' -----------------------------------------------------------------------------
    Private Sub _setPosValues()
        If InvokeRequired Then
            Invoke(Sub() _setPosValues())
            Return
        End If

        lblJ1Val.Text = CStr(Math.Round(frmMain.RoboControl.PosJoint.J1, 2)) & " °"
        lblJ2Val.Text = CStr(Math.Round(frmMain.RoboControl.PosJoint.J2, 2)) & " °"
        lblJ3Val.Text = CStr(Math.Round(frmMain.RoboControl.PosJoint.J3, 2)) & " °"
        lblJ4Val.Text = CStr(Math.Round(frmMain.RoboControl.PosJoint.J4, 2)) & " °"
        lblJ5Val.Text = CStr(Math.Round(frmMain.RoboControl.PosJoint.J5, 2)) & " °"
        lblJ6Val.Text = CStr(Math.Round(frmMain.RoboControl.PosJoint.J6, 2)) & " °"
        lblXVal.Text = CStr(Math.Round(frmMain.RoboControl.PosCart.X, 2))
        lblYVal.Text = CStr(Math.Round(frmMain.RoboControl.PosCart.Y, 2))
        lblZVal.Text = CStr(Math.Round(frmMain.RoboControl.PosCart.Z, 2))
        lblYawVal.Text = CStr(Math.Round(frmMain.RoboControl.PosCart.Yaw, 2)) & " °"
        lblPitchVal.Text = CStr(Math.Round(frmMain.RoboControl.PosCart.Pitch, 2)) & " °"
        lblRollVal.Text = CStr(Math.Round(frmMain.RoboControl.PosCart.Roll, 2)) & " °"
    End Sub

    Private Sub _setServoValues()
        If InvokeRequired Then
            Invoke(Sub() _setServoValues())
            Return
        End If

        lblServo1Val.Text = If(frmMain.RoboControl.Pref.ServoParameter(0).Available, CStr(Math.Round(frmMain.RoboControl.PosServo(0), 2)) & " %", "n.V.")
        lblServo2Val.Text = If(frmMain.RoboControl.Pref.ServoParameter(1).Available, CStr(Math.Round(frmMain.RoboControl.PosServo(1), 2)) & " %", "n.V.")
        lblServo3Val.Text = If(frmMain.RoboControl.Pref.ServoParameter(2).Available, CStr(Math.Round(frmMain.RoboControl.PosServo(2), 2)) & " %", "n.V.")
    End Sub

    Private Sub _refreshRefState()
        If InvokeRequired Then
            Invoke(Sub() _eRoboRefStateChanged())
            Return
        End If

        lblJ1HeadingImg.ImageIndex = If(frmMain.RoboControl.RefOkay(0), 1, 0)
        lblJ2HeadingImg.ImageIndex = If(frmMain.RoboControl.RefOkay(1), 1, 0)
        lblJ3HeadingImg.ImageIndex = If(frmMain.RoboControl.RefOkay(2), 1, 0)
        lblJ4HeadingImg.ImageIndex = If(frmMain.RoboControl.RefOkay(3), 1, 0)
        lblJ5HeadingImg.ImageIndex = If(frmMain.RoboControl.RefOkay(4), 1, 0)
        lblJ6HeadingImg.ImageIndex = If(frmMain.RoboControl.RefOkay(5), 1, 0)

        Dim tt As ToolTip = New ToolTip()
        tt.SetToolTip(lblJ1HeadingImg, If(frmMain.RoboControl.RefOkay(0), _constInRefTooltipText, _constNoRefTooltipText))
        tt.SetToolTip(lblJ2HeadingImg, If(frmMain.RoboControl.RefOkay(1), _constInRefTooltipText, _constNoRefTooltipText))
        tt.SetToolTip(lblJ3HeadingImg, If(frmMain.RoboControl.RefOkay(2), _constInRefTooltipText, _constNoRefTooltipText))
        tt.SetToolTip(lblJ4HeadingImg, If(frmMain.RoboControl.RefOkay(3), _constInRefTooltipText, _constNoRefTooltipText))
        tt.SetToolTip(lblJ5HeadingImg, If(frmMain.RoboControl.RefOkay(4), _constInRefTooltipText, _constNoRefTooltipText))
        tt.SetToolTip(lblJ6HeadingImg, If(frmMain.RoboControl.RefOkay(5), _constInRefTooltipText, _constNoRefTooltipText))
    End Sub

    ' -----------------------------------------------------------------------------
    ' Events
    ' -----------------------------------------------------------------------------
    ' Position
    Private Sub _eNewPos()
        _setPosValues()
    End Sub
    Private Sub _eRoboParameterChanged(parameterChanged As Settings.ParameterChangedParameter)
        Dim all As Boolean = parameterChanged = Settings.ParameterChangedParameter.All
        If parameterChanged = Settings.ParameterChangedParameter.DenavitHartenbergParameter Or
                parameterChanged = Settings.ParameterChangedParameter.Toolframe Or
                parameterChanged = Settings.ParameterChangedParameter.Workframe Or all Then
            _setPosValues()
        End If
        If parameterChanged = Settings.ParameterChangedParameter.Servo Or all Then
            _setServoValues()
        End If
    End Sub

    ' Greifer
    Private Sub _eNewServo()
        _setServoValues()
    End Sub

    ' Referenz
    Private Sub _eRoboRefStateChanged()
        _refreshRefState()
    End Sub

    ' Serial
    Private Sub _eSerialConnectionStateChanged()
        If InvokeRequired Then
            Invoke(Sub() _eSerialConnectionStateChanged())
            Return
        End If

        lblSerialState.Text = If(SerialConnected, "verbunden", "nicht verbunden")
        lblSerialState.ForeColor = If(SerialConnected, Color.Green, Color.Red)

        If Not SerialConnected Then
            lblJ1LSState.ImageIndex = 0
            lblJ2LSState.ImageIndex = 0
            lblJ3LSState.ImageIndex = 0
            lblJ4LSState.ImageIndex = 0
            lblJ5LSState.ImageIndex = 0
            lblJ6LSState.ImageIndex = 0
            _limitSwitchToolTip.RemoveAll()

            lblJ1HeadingImg.ImageIndex = -1
            lblJ2HeadingImg.ImageIndex = -1
            lblJ3HeadingImg.ImageIndex = -1
            lblJ4HeadingImg.ImageIndex = -1
            lblJ5HeadingImg.ImageIndex = -1
            lblJ6HeadingImg.ImageIndex = -1
        Else
            _refreshRefState()
        End If
    End Sub

    ' Endschalter
    Private Sub _eLimitSwitchStateChanged(lssState As Boolean())
        If InvokeRequired Then
            Invoke(Sub() _eLimitSwitchStateChanged(lssState))
            Return
        End If

        lblJ1LSState.ImageIndex = If(lssState(0), 1, 2)
        lblJ2LSState.ImageIndex = If(lssState(1), 1, 2)
        lblJ3LSState.ImageIndex = If(lssState(2), 1, 2)
        lblJ4LSState.ImageIndex = If(lssState(3), 1, 2)
        lblJ5LSState.ImageIndex = If(lssState(4), 1, 2)
        lblJ6LSState.ImageIndex = If(lssState(5), 1, 2)

        _limitSwitchToolTip.SetToolTip(lblJ1LSState, If(lssState(0), _constLimitSwitchActuated, _constLimitSwitchNotActuated))
        _limitSwitchToolTip.SetToolTip(lblJ2LSState, If(lssState(1), _constLimitSwitchActuated, _constLimitSwitchNotActuated))
        _limitSwitchToolTip.SetToolTip(lblJ3LSState, If(lssState(2), _constLimitSwitchActuated, _constLimitSwitchNotActuated))
        _limitSwitchToolTip.SetToolTip(lblJ4LSState, If(lssState(3), _constLimitSwitchActuated, _constLimitSwitchNotActuated))
        _limitSwitchToolTip.SetToolTip(lblJ5LSState, If(lssState(4), _constLimitSwitchActuated, _constLimitSwitchNotActuated))
        _limitSwitchToolTip.SetToolTip(lblJ6LSState, If(lssState(5), _constLimitSwitchActuated, _constLimitSwitchNotActuated))
    End Sub

    ' Nothalt
    Private Sub _eEmergencyStopStateChanged(essState As Boolean)
        If InvokeRequired Then
            Invoke(Sub() _eEmergencyStopStateChanged(essState))
            Return
        End If

        lblESState.ImageIndex = If(essState, 1, 2)

        _eStopToolTip.SetToolTip(lblESState, If(essState, _constEstopActuated, _constEstopNotActuated))
    End Sub
End Class