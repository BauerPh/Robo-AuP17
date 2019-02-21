Option Strict On

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
    Private _dckPanVariables As New panVariables
    Private _dckPanTeachpoints As New panTeachPoints
    Private _dckPanRoboStatus As New panRoboStatus
    Private _dckPanProgramTools As New panProgramTools
    Private _dckPanTeachBox As New panTeachBox
    Private _dckPanCtrl As New panCtrl

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
            'Extender.FloatWindowFactory = new CustomFloatWindowFactory();
            .Theme.Extender.FloatWindowFactory = New CustomFloatWindowFactory
        End With

        _dckPanCodeEditor.HideOnClose = True
        _dckPanLog.HideOnClose = True
        _dckPanVariables.HideOnClose = True
        _dckPanTeachpoints.HideOnClose = True
        _dckPanRoboStatus.HideOnClose = True
        _dckPanProgramTools.HideOnClose = True
        _dckPanTeachBox.HideOnClose = True
        _dckPanCtrl.HideOnClose = True

        If (File.Exists(_viewSettingsFilename)) Then
            dckPanel.LoadFromXml(_viewSettingsFilename, AddressOf GetContent)
        Else
            ' Add Panels
            _dckPanCodeEditor.Show(dckPanel, DockState.Document)
            _dckPanLog.Show(dckPanel, DockState.DockBottom)
            _dckPanVariables.Show(dckPanel, DockState.DockRight)
            _dckPanTeachpoints.Show(_dckPanVariables.Pane, DockAlignment.Bottom, 0.5)
            _dckPanRoboStatus.Show(dckPanel, DockState.DockLeft)
            _dckPanProgramTools.Show(_dckPanRoboStatus.Pane, DockAlignment.Bottom, 0.5)

            _dckPanTeachBox.Show(dckPanel, DockState.Float)
            _dckPanTeachBox.Hide()
            _dckPanCtrl.Show(dckPanel, DockState.Float)
            _dckPanCtrl.Hide()
        End If

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

    Private Sub msShowPanVariable_Click(sender As Object, e As EventArgs) Handles msShowPanVariable.Click
        _dckPanVariables.Show()
    End Sub

    Private Sub msShowPanTeachPoints_Click(sender As Object, e As EventArgs) Handles msShowPanTeachPoints.Click
        _dckPanTeachpoints.Show()
    End Sub

    Private Sub msShowPanCodeEditor_Click(sender As Object, e As EventArgs) Handles msShowPanCodeEditor.Click
        _dckPanCodeEditor.Show()
    End Sub

    Private Sub msShowPanTeachBox_Click(sender As Object, e As EventArgs) Handles msShowPanTeachBox.Click
        _dckPanTeachBox.Show()
    End Sub

    Private Sub msShowPanRoboStatus_Click(sender As Object, e As EventArgs) Handles msShowPanRoboStatus.Click
        _dckPanRoboStatus.Show()
    End Sub

    Private Sub msShowPanProgramTools_Click(sender As Object, e As EventArgs) Handles msShowPanProgramTools.Click
        _dckPanProgramTools.Show()
    End Sub

    Private Sub msShowPanLog_Click(sender As Object, e As EventArgs) Handles msShowPanLog.Click
        _dckPanLog.Show()
    End Sub

    Private Sub msShowPanCtrl_Click(sender As Object, e As EventArgs) Handles msShowPanCtrl.Click
        _dckPanCtrl.Show()
    End Sub

    ' -----------------------------------------------------------------------------
    ' Helper Functions
    ' -----------------------------------------------------------------------------
    Private Function GetContent(persist As String) As IDockContent
        If persist.EndsWith("CodeEditor") Then
            Return _dckPanCodeEditor
        ElseIf persist.EndsWith("Log") Then
            Return _dckPanLog
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

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Maximiert starten wenn Ansicht so gespeichert wurde
        If My.Settings.StartMaximized Then
            WindowState = FormWindowState.Maximized
        End If
    End Sub
End Class
