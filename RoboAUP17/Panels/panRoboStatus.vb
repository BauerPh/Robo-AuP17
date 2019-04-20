Public Class panRoboStatus
    ' -----------------------------------------------------------------------------
    ' TODO
    ' -----------------------------------------------------------------------------
    ' Servos / Greifer

    ' -----------------------------------------------------------------------------
    ' Init Panel
    ' -----------------------------------------------------------------------------
    Private Sub panRoboStatus_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim ImgRef As New ImageList
        ImgRef.ImageSize = New Size(20, 20)
        ImgRef.Images.Add(My.Resources.ico_error)
        ImgRef.Images.Add(My.Resources.ico_ok)
        lblJ1HeadingImg.ImageList = ImgRef
        lblJ1HeadingImg.ImageIndex = 0
        lblJ2HeadingImg.ImageList = ImgRef
        lblJ2HeadingImg.ImageIndex = 1
        lblJ3HeadingImg.ImageList = ImgRef
        lblJ3HeadingImg.ImageIndex = 0
        lblJ4HeadingImg.ImageList = ImgRef
        lblJ4HeadingImg.ImageIndex = 0
        lblJ5HeadingImg.ImageList = ImgRef
        lblJ5HeadingImg.ImageIndex = 0
        lblJ6HeadingImg.ImageList = ImgRef
        lblJ6HeadingImg.ImageIndex = 0

        Dim ImgListLS As New ImageList
        ImgListLS.ImageSize = New Size(20, 20)
        ImgListLS.Images.Add(My.Resources.ico_nok)
        ImgListLS.Images.Add(My.Resources.ico_ok)
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
        ImgListES.ImageSize = New Size(20, 20)
        ImgListES.Images.Add(My.Resources.ico_estop)
        ImgListES.Images.Add(My.Resources.ico_ok)
        lblESState.ImageList = ImgListES
        lblESState.ImageIndex = 0


        AddHandler frmMain.RoboControl.RoboPositionChanged, AddressOf _eNewPos
        AddHandler frmMain.RoboControl.RoboServoChanged, AddressOf _eNewServo
        AddHandler frmMain.RoboControl.RoboParameterChanged, AddressOf _eRoboParameterChanged
        AddHandler frmMain.RoboControl.RoboRefStateChanged, AddressOf _eRoboRefStateChanged
        AddHandler frmMain.RoboControl.SerialConnected, AddressOf _eSerialConnectionStateChanged
        AddHandler frmMain.RoboControl.SerialDisconnected, AddressOf _eSerialConnectionStateChanged
        AddHandler frmMain.RoboControl.LimitSwitchStateChanged, AddressOf _eLimitSwitchStateChanged
        AddHandler frmMain.RoboControl.EmergencyStopStateChanged, AddressOf _eEmergencyStopStateChanged
    End Sub

    Private Sub TableLayoutPanel_CellPaint(sender As Object, e As TableLayoutCellPaintEventArgs) Handles TableLayoutPanel.CellPaint
        Dim lineRow() = {1, 2, 4, 5, 7}
        ' Rahmen Zeichnen
        If e.Column Mod 2 = 0 And e.Column <> 0 _
            And lineRow.Contains(e.Row) Then
            e.Graphics.DrawLine(Pens.Black, e.CellBounds.Location, New Point(e.CellBounds.Left, e.CellBounds.Bottom))
        End If
        If e.Row = 2 Or e.Row = 5 Then
            Dim pen As New Pen(Color.Black)
            pen.DashStyle = Drawing2D.DashStyle.Dot
            e.Graphics.DrawLine(pen, e.CellBounds.Location, New Point(e.CellBounds.Right, e.CellBounds.Top))
        End If
    End Sub

    ' -----------------------------------------------------------------------------
    ' Form Control
    ' -----------------------------------------------------------------------------
    ' ...

    ' -----------------------------------------------------------------------------
    ' Private
    ' -----------------------------------------------------------------------------
    Private Sub _setPosValues()
        If InvokeRequired Then
            Invoke(Sub() _setPosValues())
            Return
        End If

        lblJ1Val.Text = CStr(frmMain.RoboControl.PosJoint.J1)
        lblJ2Val.Text = CStr(frmMain.RoboControl.PosJoint.J2)
        lblJ3Val.Text = CStr(frmMain.RoboControl.PosJoint.J3)
        lblJ4Val.Text = CStr(frmMain.RoboControl.PosJoint.J4)
        lblJ5Val.Text = CStr(frmMain.RoboControl.PosJoint.J5)
        lblJ6Val.Text = CStr(frmMain.RoboControl.PosJoint.J6)
        lblXVal.Text = CStr(frmMain.RoboControl.PosCart.X)
        lblYVal.Text = CStr(frmMain.RoboControl.PosCart.Y)
        lblZVal.Text = CStr(frmMain.RoboControl.PosCart.Z)
        lblYawVal.Text = CStr(frmMain.RoboControl.PosCart.Yaw)
        lblPitchVal.Text = CStr(frmMain.RoboControl.PosCart.Pitch)
        lblRollVal.Text = CStr(frmMain.RoboControl.PosCart.Roll)
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
    End Sub

    ' Greifer (TODO)
    Private Sub _eNewServo()

    End Sub

    ' Referenz
    Private Sub _eRoboRefStateChanged()
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
    End Sub

    ' Serial
    Private Sub _eSerialConnectionStateChanged()
        If InvokeRequired Then
            Invoke(Sub() _eSerialConnectionStateChanged())
            Return
        End If

        lblSerialState.Text = If(SerialConnected, "verbunden", "nicht verbunden")
        lblSerialState.ForeColor = If(SerialConnected, Color.Green, Color.Red)
    End Sub

    ' Endschalter
    Private Sub _eLimitSwitchStateChanged(lssState As Boolean())
        If InvokeRequired Then
            Invoke(Sub() _eLimitSwitchStateChanged(lssState))
            Return
        End If

        lblJ1LSState.ImageIndex = If(lssState(0), 1, 0)
        lblJ2LSState.ImageIndex = If(lssState(1), 1, 0)
        lblJ3LSState.ImageIndex = If(lssState(2), 1, 0)
        lblJ4LSState.ImageIndex = If(lssState(3), 1, 0)
        lblJ5LSState.ImageIndex = If(lssState(4), 1, 0)
        lblJ6LSState.ImageIndex = If(lssState(5), 1, 0)
    End Sub

    ' Nothalt
    Private Sub _eEmergencyStopStateChanged(essState As Boolean)
        If InvokeRequired Then
            Invoke(Sub() _eEmergencyStopStateChanged(essState))
            Return
        End If

        lblESState.ImageIndex = If(essState, 1, 0)
    End Sub

End Class