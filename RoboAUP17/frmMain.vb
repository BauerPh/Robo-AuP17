Imports System.IO
Imports System.Timers
Imports WeifenLuo.WinFormsUI.Docking

Public Class frmMain
#Region "Definitions"
    'Test
    Friend dictTest As New Dictionary(Of String, Double)
    Friend settingsTest As New DHParameter

    'Robotersteuerung
    Private WithEvents _aclProgram As New ACLProgram
    Private WithEvents _roboControl As New RobotControl

    ' Konstanten
    Private Const _ssHintTimerInterval As Int32 = 4000
    Private Const _viewSettingsFilename As String = "ViewSettings.xml"
    Private Const _gitHubUrl As String = "https://github.com/BauerPh/RoboAUP17"
    Private Const _gitHubUrlArduinoFirmware As String = "https://github.com/BauerPh/RoboAUP17_Arduino"

    ' DockPanel
    Friend dckPanel As New DockPanel
    Private _dckPanCodeEditor As New panCodeEditor
    Private _dckPanLog As New panLog
    Private _dckPanComLogSerial As New panLog
    Private _dckPanComLogTCPIP As New panLog
    Private _dckPanVariables As New panVariables
    Private _dckPanTeachpoints As New panTeachPoints
    Private _dckPanRoboStatus As New panRoboStatus
    Private _dckPanProgramTools As New panProgramTools
    Private _dckPanJointCtrl As New panCtrl
    Private _dckPanReference As New panReference

    Private _dckPanSettings As New panSettings

    ' Objekte
    Private WithEvents _ssHintTimer As New Timer
    Private _logger As New Logger(_dckPanLog.sciLog)
    Private _loggerComSerial As New Logger(_dckPanComLogSerial.sciLog)
    Private _loggerComTCPIP As New Logger(_dckPanComLogTCPIP.sciLog)
#End Region
    ' Properties
    Friend ReadOnly Property ACLProgram As ACLProgram
        Get
            Return _aclProgram
        End Get
    End Property
    Friend ReadOnly Property RoboControl As RobotControl
        Get
            Return _roboControl
        End Get
    End Property

    ' -----------------------------------------------------------------------------
    ' Public
    ' -----------------------------------------------------------------------------
#Region "Public"
    Friend Sub ShowStatusStripHint(msg As String)
        ssLblStatus.Text = msg
        ' Timer starten bzw. neustarten
        If _ssHintTimer.Enabled Then
            _ssHintTimer.Stop()
        End If
        _ssHintTimer.Interval = _ssHintTimerInterval
        _ssHintTimer.Start()
    End Sub
#End Region

    ' -----------------------------------------------------------------------------
    ' Private
    ' -----------------------------------------------------------------------------
#Region "Robo Steuerung"
    Private Sub tsBtnEStop_Click(sender As Object, e As EventArgs) Handles tsBtnEStop.Click
        _roboControl.FastStop()
    End Sub
    Private Sub RoboTest_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Space Then
            _roboControl.FastStop()
        End If
    End Sub
#End Region

#Region "Private"
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Prüfen ob Konfigdatei geladen wurde
        If Not _roboControl.Pref.ConfigFileLoaded Then
            Application.Exit()
            Return
        End If

        Me.Text = $"Aup17 Robo v{My.Application.Info.Version.ToString}"

        'Maximiert starten wenn Ansicht so gespeichert wurde
        If My.Settings.StartMaximized Then
            Me.WindowState = FormWindowState.Maximized
        End If

        'Für Key Events
        KeyPreview = True

        'Set Log Lvl
        SetLogLevel(My.Settings.LogLvl)

        'Configure Dock Panel
        Me.SuspendLayout()
        ConfigureDockPanel()
        Me.ResumeLayout()

        'Welcome Log
        ShowStatusStripHint("Anwendung gestartet...")
        _logger.Log("[MAIN] Hi!", Logger.LogLevel.INFO)
    End Sub
    Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        My.Settings.Save()
        ' Ungespeicherte Änderungen prüfen
        If _roboControl.Pref.UnsavedChanges Then
            Dim erg As DialogResult = MessageBox.Show($"Einstellungen wurden geändert und nicht gespeichert. Wollen sie diese jetzt Speichern?", "Ungespeicherte Änderungen", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning)
            If erg = DialogResult.Cancel Then
                e.Cancel = True
            ElseIf erg = DialogResult.Yes Then
                If Not _roboControl.Pref.SaveSettings() Then
                    e.Cancel = True
                End If
            End If
        End If
    End Sub
    Private Sub tsBtnConnect_Click(sender As Object, e As EventArgs) Handles tsBtnConnect.Click
        _roboControl.SerialConnect(tsCbComPort.Text)
    End Sub
    Private Sub tsBtnDisconnect_Click(sender As Object, e As EventArgs) Handles tsBtnDisconnect.Click
        _roboControl.SerialDisconnect()
    End Sub
    ' ACL Program
    Private Sub tsBtnProgCheck_Click(sender As Object, e As EventArgs) Handles tsBtnProgCheck.Click
        _aclProgram.CompileProgram(_dckPanCodeEditor.sciCodeEditor.Text, numAcc.Value, numSpeed.Value)
    End Sub
#End Region

#Region "MenuStrip Datei"
    Private Sub msExit_Click(sender As Object, e As EventArgs) Handles msExit.Click
        Application.Exit()
    End Sub
#End Region

#Region "MenuStrip Ansicht"
    Private Sub msSaveView_Click(sender As Object, e As EventArgs) Handles msSaveView.Click
        dckPanel.SaveAsXml(_viewSettingsFilename)
        My.Settings.StartMaximized = (WindowState = FormWindowState.Maximized)
        _logger.Log("[MAIN] aktuelle Ansicht wurde gespeichert...", Logger.LogLevel.INFO)
    End Sub

    Private Sub msDefaulView_Click(sender As Object, e As EventArgs) Handles msDefaulView.Click
        File.Delete(_viewSettingsFilename)
        My.Settings.StartMaximized = False
        MessageBox.Show("Standardansicht wurde wiederhergestellt. Neustart erforderlich!", "Okay", MessageBoxButtons.OK, MessageBoxIcon.Information)
        _logger.Log("[MAIN] Standardansicht wurde wiederhergestellt. Neustart erforderlich!", Logger.LogLevel.INFO)
    End Sub
#End Region
    Private Sub msRoboParameter_Click(sender As Object, e As EventArgs) Handles msRoboParameter.Click
        _dckPanSettings.Show()
        _dckPanSettings.SetSelecteSetting(panSettings.selectedSetting.RoboPar)
    End Sub

    Private Sub msTCPServer_Click(sender As Object, e As EventArgs) Handles msTCPServer.Click
        _dckPanSettings.Show()
        _dckPanSettings.SetSelecteSetting(panSettings.selectedSetting.TCPServer)
    End Sub

    Private Sub msDenavitHartPar_Click(sender As Object, e As EventArgs) Handles msDenavitHartPar.Click
        _dckPanSettings.Show()
        _dckPanSettings.SetSelecteSetting(panSettings.selectedSetting.DenHartPar)
    End Sub
    Private Sub msFrames_Click(sender As Object, e As EventArgs) Handles msFrames.Click
        _dckPanSettings.Show()
        _dckPanSettings.SetSelecteSetting(panSettings.selectedSetting.Frames)
    End Sub

#Region "MenuStrip Hilfe"
    Private Sub msGitHub_Click(sender As Object, e As EventArgs) Handles msGitHub.Click
        Process.Start(_gitHubUrl)
    End Sub
    Private Sub msArduinoFirmware_Click(sender As Object, e As EventArgs) Handles msArduinoFirmware.Click
        Process.Start(_gitHubUrlArduinoFirmware)
    End Sub
#End Region

#Region "Show Panels"
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
        _dckPanProgramTools.Show()
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
        _dckPanJointCtrl.Show()
    End Sub

#End Region

#Region "Set Log Level"
    Private Sub msSetLogLvlDebug_Click(sender As Object, e As EventArgs) Handles msSetLogLvlDebug.Click
        SetLogLevel(Logger.LogLevel.DEBUG)
    End Sub

    Private Sub msSetLogLvlInfo_Click(sender As Object, e As EventArgs) Handles msSetLogLvlInfo.Click
        SetLogLevel(Logger.LogLevel.INFO)
    End Sub

    Private Sub msSetLogLvlWarning_Click(sender As Object, e As EventArgs) Handles msSetLogLvlWarning.Click
        SetLogLevel(Logger.LogLevel.WARN)
    End Sub

    Private Sub msSetLogLvlError_Click(sender As Object, e As EventArgs) Handles msSetLogLvlError.Click
        SetLogLevel(Logger.LogLevel.ERR)
    End Sub

    Private Sub SetLogLevel(lvl As Logger.LogLevel)
        SetLogLevel(lvl.ToInteger())
    End Sub

    Private Sub SetLogLevel(lvl As Integer)
        _logger.SetLogLvl(lvl)
        msSetLogLvlDebug.Image = Nothing
        msSetLogLvlInfo.Image = Nothing
        msSetLogLvlWarning.Image = Nothing
        msSetLogLvlError.Image = Nothing
        Select Case lvl
            Case Logger.LogLevel.DEBUG.ToInteger()
                msSetLogLvlDebug.Image = My.Resources.ico_checked
            Case Logger.LogLevel.INFO.ToInteger()
                msSetLogLvlInfo.Image = My.Resources.ico_checked
            Case Logger.LogLevel.WARN.ToInteger()
                msSetLogLvlWarning.Image = My.Resources.ico_checked
            Case Logger.LogLevel.ERR.ToInteger()
                msSetLogLvlError.Image = My.Resources.ico_checked
        End Select

        My.Settings.LogLvl = lvl
    End Sub
#End Region

    ' -----------------------------------------------------------------------------
    ' Helper Functions
    ' -----------------------------------------------------------------------------
    Private Sub ConfigureDockPanel()
        ' Create & Configure DockPanel
        With dckPanel
            .Theme = New VS2015LightTheme
            .Parent = Me
            .Dock = DockStyle.Fill
            .BringToFront()
            .Theme.Extender.FloatWindowFactory = New CustomFloatWindowFactory
            .DockLeftPortion = 0.22
            .DockRightPortion = 0.24
            .DockTopPortion = 0.25
            .DockBottomPortion = 0.25
        End With

        _dckPanCodeEditor.HideOnClose = True
        _dckPanLog.HideOnClose = True
        _dckPanComLogSerial.HideOnClose = True
        _dckPanComLogTCPIP.HideOnClose = True
        _dckPanVariables.HideOnClose = True
        _dckPanTeachpoints.HideOnClose = True
        _dckPanRoboStatus.HideOnClose = True
        _dckPanProgramTools.HideOnClose = True
        _dckPanJointCtrl.HideOnClose = True
        _dckPanReference.HideOnClose = True
        _dckPanSettings.HideOnClose = True

        'Edit Names
        _dckPanLog.Text = "Robo Log"
        _dckPanComLogSerial.Text = "Serial Log"
        _dckPanComLogTCPIP.Text = "TCP/IP Log"

        'Check for saved View Settings
        If (File.Exists(_viewSettingsFilename)) Then
            dckPanel.LoadFromXml(_viewSettingsFilename, AddressOf GetContent)
            _logger.Log($"[MAIN] ""{_viewSettingsFilename}"" geladen", Logger.LogLevel.DEBUG)
        Else
            ' Add Panels
            _dckPanCodeEditor.Show(dckPanel, DockState.Document)

            _dckPanRoboStatus.Show(dckPanel, DockState.DockLeft)
            _dckPanReference.Show(_dckPanRoboStatus.Pane, DockAlignment.Bottom, 0.16)
            _dckPanJointCtrl.Show(_dckPanRoboStatus.Pane, DockAlignment.Bottom, 0.4)

            _dckPanVariables.Show(dckPanel, DockState.DockRight)
            _dckPanTeachpoints.Show(dckPanel, DockState.DockRight)
            _dckPanSettings.Show(_dckPanTeachpoints.Pane, DockAlignment.Bottom, 0.5)

            _dckPanLog.Show(dckPanel, DockState.DockBottom)
            _dckPanComLogSerial.Show(dckPanel, DockState.DockBottom)
            _dckPanComLogTCPIP.Show(dckPanel, DockState.DockBottom)

            _dckPanProgramTools.Show(dckPanel, DockState.Float)
            _dckPanProgramTools.Hide()

            _logger.Log("[MAIN] Standardansicht geladen", Logger.LogLevel.DEBUG)
        End If

        'activate default panels
        _dckPanLog.Activate()
    End Sub

    Private Function GetContent(persist As String) As IDockContent
        If persist.EndsWith("CodeEditor") Then
            Return _dckPanCodeEditor
        ElseIf persist.EndsWith("Ctrl") Then
            Return _dckPanJointCtrl
        ElseIf persist.EndsWith("Robo Log") Then
            Return _dckPanLog
        ElseIf persist.EndsWith("Serial Log") Then
            Return _dckPanComLogSerial
        ElseIf persist.EndsWith("TCP/IP Log") Then
            Return _dckPanComLogTCPIP
        ElseIf persist.EndsWith("ProgramTools") Then
            Return _dckPanProgramTools
        ElseIf persist.EndsWith("Reference") Then
            Return _dckPanReference
        ElseIf persist.EndsWith("Settings") Then
            Return _dckPanSettings
        ElseIf persist.EndsWith("RoboStatus") Then
            Return _dckPanRoboStatus
        ElseIf persist.EndsWith("TeachPoints") Then
            Return _dckPanTeachpoints
        ElseIf persist.EndsWith("Variables") Then
            Return _dckPanVariables
        End If
        Return New DockContent()
    End Function

    'Timer to clear statusStrip hint message
    Private Sub _ssHintTimer_Elapsed(sender As Object, e As ElapsedEventArgs) Handles _ssHintTimer.Elapsed
        If InvokeRequired Then
            Invoke(Sub() _ssHintTimer_Elapsed(sender, e))
            Return
        End If

        _ssHintTimer.Stop()
        ssLblStatus.Text = ""
    End Sub

    ' -----------------------------------------------------------------------------
    ' Events
    ' -----------------------------------------------------------------------------
    Private Sub _eLog(LogMsg As String, LogLvl As Logger.LogLevel) Handles _roboControl.Log, _aclProgram.Log
        If LogLvl = Logger.LogLevel.COMIN Or LogLvl = Logger.LogLevel.COMOUT Then
            _loggerComSerial.Log(LogMsg, LogLvl)
        Else
            _logger.Log(LogMsg, LogLvl)
        End If
    End Sub
    Private Sub _eSerialComPortChanged(ports As List(Of String)) Handles _roboControl.SerialComPortChanged
        If InvokeRequired Then
            Invoke(Sub() _eSerialComPortChanged(ports))
            Return
        End If

        tsCbComPort.Items.Clear()
        If ports.Count > 0 Then
            For i = 0 To ports.Count - 1
                tsCbComPort.Items.Add(ports(i))
            Next
            tsCbComPort.SelectedIndex = 0
            tsBtnConnect.Enabled = True
        Else
            tsBtnConnect.Enabled = False
        End If
    End Sub
    Private Sub _eComSerialConnected() Handles _roboControl.SerialConnected
        If InvokeRequired Then
            Invoke(Sub() _eComSerialConnected())
            Return
        End If
        tsBtnConnect.Enabled = False
        tsBtnProgRun.Enabled = True

        SerialConnected = True
        RobotMoving = False
    End Sub
    Private Sub _eComSerialDisconnected() Handles _roboControl.SerialDisconnected
        If InvokeRequired Then
            Invoke(Sub() _eComSerialDisconnected())
            Return
        End If
        tsBtnConnect.Enabled = True
        tsBtnProgRun.Enabled = False
        tsBtnProgStop.Enabled = False

        SerialConnected = False
        RobotMoving = False
    End Sub
    Private Sub _eRoboMoveStarted() Handles _roboControl.RoboMoveStarted
        RobotMoving = True
    End Sub
    Private Sub _eRoboMoveFinished() Handles _roboControl.RoboMoveFinished
        RobotMoving = False
    End Sub

End Class
