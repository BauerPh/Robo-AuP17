Imports System.IO
Imports System.Timers
Imports WeifenLuo.WinFormsUI.Docking

Public Class frmMain
    'If InvokeRequired Then
    'Invoke(Sub() _ssHintTimer_Elapsed(sender, e))
    'Return
    'End If

    'Test
    Public dictTest As New Dictionary(Of String, Double)

    ' Konstanten
    Private Const _ssHintTimerInterval As Int32 = 4000
    Private Const _viewSettingsFilename As String = "ViewSettings.xml"

    ' Objekte
    Private WithEvents _ssHintTimer As New Timer
    ' DockPanel
    Public dckPanel As New DockPanel
    Private _dckPanCodeEditor As New panCodeEditor
    Private _dckPanLog As New panLog
    Private _dckPanComLogSerial As New panLog
    Private _dckPanComLogTCPIP As New panLog
    Private _dckPanVariables As New panVariables
    Private _dckPanTeachpoints As New panTeachPoints
    Private _dckPanRoboStatus As New panRoboStatus
    Private _dckPanProgramTools As New panProgramTools
    Private _dckPanTeachBox As New panTeachBox
    Private _dckPanCtrl As New panCtrl
    Private _dckPanReference As New panReference

    ' -----------------------------------------------------------------------------
    ' Constructor
    ' -----------------------------------------------------------------------------
    Public Sub New()
        MyBase.New()

        InitializeComponent()
        Me.SuspendLayout()
        ' Create & Configure DockPanel
        With dckPanel
            .Theme = New VS2015LightTheme
            .Parent = Me
            .Dock = DockStyle.Fill
            .BringToFront()
            .Theme.Extender.FloatWindowFactory = New CustomFloatWindowFactory
        End With

        _dckPanCodeEditor.HideOnClose = True
        _dckPanLog.HideOnClose = True
        _dckPanComLogSerial.HideOnClose = True
        _dckPanComLogTCPIP.HideOnClose = True
        _dckPanVariables.HideOnClose = True
        _dckPanTeachpoints.HideOnClose = True
        _dckPanRoboStatus.HideOnClose = True
        _dckPanProgramTools.HideOnClose = True
        _dckPanTeachBox.HideOnClose = True
        _dckPanCtrl.HideOnClose = True
        _dckPanReference.HideOnClose = True

        'Edit Names
        _dckPanLog.Text = "Robo Log"
        _dckPanComLogSerial.Text = "Serial Log"
        _dckPanComLogTCPIP.Text = "TCP/IP Log"

        'Check for saved View Settings
        If (File.Exists(_viewSettingsFilename)) Then
            dckPanel.LoadFromXml(_viewSettingsFilename, AddressOf GetContent)
        Else
            ' Add Panels
            _dckPanCodeEditor.Show(dckPanel, DockState.Document)
            _dckPanLog.Show(dckPanel, DockState.DockBottom)
            _dckPanComLogSerial.Show(dckPanel, DockState.DockBottom)
            _dckPanComLogTCPIP.Show(dckPanel, DockState.DockBottom)
            _dckPanVariables.Show(dckPanel, DockState.DockRight)
            _dckPanTeachpoints.Show(_dckPanVariables.Pane, DockAlignment.Bottom, 0.5)
            _dckPanRoboStatus.Show(dckPanel, DockState.DockLeft)
            _dckPanProgramTools.Show(_dckPanRoboStatus.Pane, DockAlignment.Bottom, 0.5)

            _dckPanTeachBox.Show(dckPanel, DockState.Float)
            _dckPanTeachBox.Hide()
            _dckPanCtrl.Show(dckPanel, DockState.Float)
            _dckPanCtrl.Hide()
            _dckPanReference.Show(dckPanel, DockState.Float)
            _dckPanReference.Hide()
        End If

        'activate default panels
        _dckPanLog.Activate()

        ShowStatusStripHint("Anwendung gestartet...")

        Me.ResumeLayout()

    End Sub

    ' -----------------------------------------------------------------------------
    ' Public
    ' -----------------------------------------------------------------------------
    Public Sub ShowStatusStripHint(msg As String)
        ssLblStatus.Text = msg
        ' Timer starten bzw. neustarten
        If _ssHintTimer.Enabled Then
            _ssHintTimer.Stop()
        End If
        _ssHintTimer.Interval = _ssHintTimerInterval
        _ssHintTimer.Start()
    End Sub

    ' -----------------------------------------------------------------------------
    ' Private
    ' -----------------------------------------------------------------------------
    Private Sub msSaveView_Click(sender As Object, e As EventArgs) Handles msSaveView.Click
        dckPanel.SaveAsXml(_viewSettingsFilename)
        My.Settings.StartMaximized = (WindowState = FormWindowState.Maximized)
        My.Settings.Save()
        ShowStatusStripHint("aktuelle Ansicht wurde gespeichert...")
    End Sub

    Private Sub msDefaulView_Click(sender As Object, e As EventArgs) Handles msDefaulView.Click
        File.Delete(_viewSettingsFilename)
        My.Settings.StartMaximized = False
        My.Settings.Save()
        MessageBox.Show("Standard wurde wiederhergestellt. Neustart erforderlich!", "Okay", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Maximiert starten wenn Ansicht so gespeichert wurde
        If My.Settings.StartMaximized Then
            WindowState = FormWindowState.Maximized
        End If
    End Sub

    Private Sub msShowVars_Click(sender As Object, e As EventArgs) Handles msShowVars.Click
        _dckPanVariables.Show()
    End Sub

    Private Sub msShowTeachpoints_Click(sender As Object, e As EventArgs) Handles msShowTeachpoints.Click
        _dckPanTeachpoints.Show()
    End Sub

    Private Sub msShowACLEditor_Click(sender As Object, e As EventArgs) Handles msShowACLEditor.Click
        _dckPanCodeEditor.Show()
    End Sub

    Private Sub msShowACLToolbox_Click(sender As Object, e As EventArgs) Handles msShowACLToolbox.Click

    End Sub

    Private Sub msShowTeachbox_Click(sender As Object, e As EventArgs) Handles msShowTeachbox.Click
        _dckPanTeachBox.Show()
    End Sub

    Private Sub msShowLog_Click(sender As Object, e As EventArgs) Handles msShowLog.Click
        _dckPanLog.Show()
    End Sub

    Private Sub msShowComLogSerial_Click(sender As Object, e As EventArgs) Handles msShowComLogSerial.Click
        _dckPanComLogSerial.Show()
    End Sub

    Private Sub msShowComLogTCPIP_Click(sender As Object, e As EventArgs) Handles msShowComLogTCPIP.Click
        _dckPanComLogTCPIP.Show()
    End Sub

    Private Sub msShowRoboStatus_Click(sender As Object, e As EventArgs) Handles msShowRoboStatus.Click
        _dckPanRoboStatus.Show()
    End Sub

    Private Sub msShowRoboReference_Click(sender As Object, e As EventArgs) Handles msShowRoboReference.Click
        _dckPanReference.Show()
    End Sub

    Private Sub msShowRoboCtrl_Click(sender As Object, e As EventArgs) Handles msShowRoboCtrl.Click
        _dckPanCtrl.Show()
    End Sub

    ' -----------------------------------------------------------------------------
    ' Helper Functions
    ' -----------------------------------------------------------------------------
    Private Function GetContent(persist As String) As IDockContent
        If persist.EndsWith("CodeEditor") Then
            Return _dckPanCodeEditor
        ElseIf persist.EndsWith("Robo Log") Then
            Return _dckPanLog
        ElseIf persist.EndsWith("Serial Log") Then
            Return _dckPanComLogSerial
        ElseIf persist.EndsWith("TCP/IP Log") Then
            Return _dckPanComLogTCPIP
        ElseIf persist.EndsWith("ComLogSerial") Then
            Return _dckPanComLogSerial
        ElseIf persist.EndsWith("ComLogTCPIP") Then
            Return _dckPanComLogTCPIP
        ElseIf persist.EndsWith("ProgramTools") Then
            Return _dckPanProgramTools
        ElseIf persist.EndsWith("RoboStatus") Then
            Return _dckPanRoboStatus
        ElseIf persist.EndsWith("TeachPoints") Then
            Return _dckPanTeachpoints
        ElseIf persist.EndsWith("Variables") Then
            Return _dckPanVariables
        ElseIf persist.EndsWith("TeachBox") Then
            Return _dckPanTeachBox
        ElseIf persist.EndsWith("Ctrl") Then
            Return _dckPanCtrl
        ElseIf persist.EndsWith("Reference") Then
            Return _dckPanReference
        End If
        Return New DockContent()
    End Function

    Private Sub _ssHintTimer_Elapsed(sender As Object, e As ElapsedEventArgs) Handles _ssHintTimer.Elapsed
        If InvokeRequired Then
            Invoke(Sub() _ssHintTimer_Elapsed(sender, e))
            Return
        End If

        _ssHintTimer.Stop()
        ssLblStatus.Text = ""
    End Sub
End Class
