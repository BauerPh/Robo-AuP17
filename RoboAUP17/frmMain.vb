Imports System.IO
Imports System.Timers
Imports WeifenLuo.WinFormsUI.Docking

Public Class frmMain
#Region "Definitions"
    'Robotersteuerung
    Private WithEvents _aclProgram As New ACLProgram
    Private WithEvents _roboControl As New RobotControl

    Private _serialPortsAvailable As Boolean = False

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
    Private _dckPanProgLog As New panLog
    Private _dckPanVariables As New panTCPVariables
    Private _dckPanTeachpoints As New panTeachPoints
    Private _dckPanRoboStatus As New panRoboStatus
    Private _dckPanProgramTools As New panProgramTools
    Private _dckPanCtrl As New panCtrl
    Private _dckPanReference As New panReference
    Private _dckPanSettings As New panSettings

    ' Objekte
    Private WithEvents _ssHintTimer As New Timer
    Private _logger As New Logger(_dckPanLog.sciLog)
    Private _loggerComSerial As New Logger(_dckPanComLogSerial.sciLog)
    Private _loggerACLProg As New Logger(_dckPanProgLog.sciLog)
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

    Friend Event RefreshEvent()
    Friend Event SerialConnectionStateChanged()

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

    Friend Sub Log(msg As String, lvl As Logger.LogLevel)
        _logger.Log(msg, lvl)
    End Sub
#End Region

    ' -----------------------------------------------------------------------------
    ' Form Control
    ' -----------------------------------------------------------------------------
#Region "Form"
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
        _setLogLevel(My.Settings.LogLvl)

        'Configure Dock Panel
        SuspendLayout()
        _configureDockPanel()
        ResumeLayout()

        'Pass RoboControl Object to ACLProgram Object
        _aclProgram.SetRoboControlObject(_roboControl)
        _aclProgram.Init(numSpeed.Value, numAcc.Value)

        'Welcome Log
        ShowStatusStripHint("Anwendung gestartet...")
        _logger.Log("[MAIN] Hi!", Logger.LogLevel.INFO)

        AddHandler _aclProgram.TcpVariables.Connected, AddressOf _eTCPConnected
        AddHandler _aclProgram.TcpVariables.Disconnected, AddressOf _eTCPDisconnected
        AddHandler _dckPanCodeEditor.sciCodeEditor.TextChanged, AddressOf _eEditorTextChanged
    End Sub
    Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        My.Settings.Save()
        ' Ungespeicherte Änderungen prüfen
        If _roboControl.Pref.UnsavedChanges Then
            Dim erg As DialogResult = MessageBox.Show($"Einstellungen wurden geändert und nicht gespeichert. Wollen Sie die Änderungen vor dem Beenden speichern?", "Ungespeicherte Änderungen", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning)
            If erg = DialogResult.Cancel Then
                e.Cancel = True
            ElseIf erg = DialogResult.Yes Then
                If Not _roboControl.Pref.SaveSettings() Then
                    e.Cancel = True
                End If
            End If
        End If
        If _checkUnsavedChanges() Then
            e.Cancel = True
        End If
    End Sub
#End Region

#Region "Robo Steuerung"
    Private Sub tsBtnEStop_Click(sender As Object, e As EventArgs) Handles tsBtnEStop.Click
        _roboControl.FastStop()
        _aclProgram.StopProgram()
    End Sub
    Private Sub RoboAUP17_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Space Then
            _roboControl.FastStop()
            _aclProgram.StopProgram()
        End If
    End Sub
#End Region

#Region "ACL Programm"
    ' ACL Program
    Private Sub tsBtnProgCheck_Click(sender As Object, e As EventArgs) Handles tsBtnProgCheck.Click
        _dckPanCodeEditor.RemoveMarker()
        _aclProgram.CompileProgram(_dckPanCodeEditor.sciCodeEditor.Text)
    End Sub
    Private Sub tsBtnProgRun_Click(sender As Object, e As EventArgs) Handles tsBtnProgRun.Click
        _dckPanCodeEditor.RemoveMarker()
        _aclProgram.RunProgram(_dckPanCodeEditor.sciCodeEditor.Text)
    End Sub
    Private Sub tsBtnProgStop_Click(sender As Object, e As EventArgs) Handles tsBtnProgStop.Click
        tsBtnProgStop.Enabled = False
        _aclProgram.StopProgram()
    End Sub
    Private Sub tsBtnSave_Click(sender As Object, e As EventArgs) Handles tsBtnSave.Click
        If _aclProgram.Save(_dckPanCodeEditor.sciCodeEditor.Text) Then
            _dckPanCodeEditor.sciCodeEditor.EmptyUndoBuffer()
            _updateUndoRedoButtons()
        End If
    End Sub
    Private Sub tsBtnOpen_Click(sender As Object, e As EventArgs) Handles tsBtnOpen.Click
        If Not _checkUnsavedChanges() Then
            If _aclProgram.Load(_dckPanCodeEditor.sciCodeEditor.Text) Then
                _dckPanCodeEditor.sciCodeEditor.EmptyUndoBuffer()
                _updateUndoRedoButtons()
            End If
        End If
    End Sub
    Private Sub numSpeed_ValueChanged(sender As Object, e As EventArgs) Handles numSpeed.ValueChanged
        _aclProgram.MaxSpeed = numSpeed.Value
    End Sub
    Private Sub numAcc_ValueChanged(sender As Object, e As EventArgs) Handles numAcc.ValueChanged
        _aclProgram.MaxAcc = numAcc.Value
    End Sub
#End Region

#Region "Serial Connection"
    Private Sub tsBtnConnect_Click(sender As Object, e As EventArgs) Handles tsBtnConnect.Click
        _roboControl.SerialConnect(tsCbComPort.Text)
    End Sub
    Private Sub tsBtnDisconnect_Click(sender As Object, e As EventArgs) Handles tsBtnDisconnect.Click
        _roboControl.SerialDisconnect()
    End Sub
#End Region

#Region "MenuStrip Datei"
    Private Sub msNew_Click(sender As Object, e As EventArgs) Handles msNew.Click
        If Not _checkUnsavedChanges() Then
            _aclProgram.ClearProgram()
            _dckPanCodeEditor.sciCodeEditor.ClearAll()
            _dckPanCodeEditor.sciCodeEditor.EmptyUndoBuffer()
            _updateUndoRedoButtons()
        End If
    End Sub
    Private Sub msOpen_Click(sender As Object, e As EventArgs) Handles msOpen.Click
        If Not _checkUnsavedChanges() Then
            If _aclProgram.Load(_dckPanCodeEditor.sciCodeEditor.Text) Then
                _dckPanCodeEditor.sciCodeEditor.EmptyUndoBuffer()
                _updateUndoRedoButtons()
            End If
        End If
    End Sub
    Private Sub msSave_Click(sender As Object, e As EventArgs) Handles msSave.Click
        If _aclProgram.Save(_dckPanCodeEditor.sciCodeEditor.Text) Then
            _dckPanCodeEditor.sciCodeEditor.EmptyUndoBuffer()
            _updateUndoRedoButtons()
        End If
    End Sub
    Private Sub msSaveAs_Click(sender As Object, e As EventArgs) Handles msSaveAs.Click
        If _aclProgram.Save(_dckPanCodeEditor.sciCodeEditor.Text, True) Then
            _dckPanCodeEditor.sciCodeEditor.EmptyUndoBuffer()
            _updateUndoRedoButtons()
        End If
    End Sub
    Private Sub msExit_Click(sender As Object, e As EventArgs) Handles msExit.Click
        Application.Exit()
    End Sub
#End Region

#Region "MenuStrip Bearbeiten"
    Private Sub Undo_Click(sender As Object, e As EventArgs) Handles msUndo.Click, tsBtnUndo.Click
        _dckPanCodeEditor.sciCodeEditor.Undo()
    End Sub
    Private Sub Redo_Click(sender As Object, e As EventArgs) Handles msRedo.Click, tsBtnRedo.Click
        _dckPanCodeEditor.sciCodeEditor.Redo()
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

#Region "MenuStrip Einstellungen"
    Private Sub msRoboParameter_Click(sender As Object, e As EventArgs) Handles msRoboParameter.Click
        _dckPanSettings.Show()
        _dckPanSettings.SetSelectedSetting(panSettings.selectedSetting.RoboPar)
    End Sub

    Private Sub msTCPServer_Click(sender As Object, e As EventArgs) Handles msTCPServer.Click
        _dckPanSettings.Show()
        _dckPanSettings.SetSelectedSetting(panSettings.selectedSetting.TCPServer)
    End Sub

    Private Sub msDenavitHartPar_Click(sender As Object, e As EventArgs) Handles msDenavitHartPar.Click
        _dckPanSettings.Show()
        _dckPanSettings.SetSelectedSetting(panSettings.selectedSetting.DenHartPar)
    End Sub
    Private Sub msFrames_Click(sender As Object, e As EventArgs) Handles msFrames.Click
        _dckPanSettings.Show()
        _dckPanSettings.SetSelectedSetting(panSettings.selectedSetting.Frames)
    End Sub
#End Region

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

    Private Sub msShowComLogTCPIP_Click(sender As Object, e As EventArgs) Handles msShowProgLog.Click
        _dckPanProgLog.Show()
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
        _setLogLevel(lvl.ToInteger())
    End Sub
#End Region

    ' -----------------------------------------------------------------------------
    ' Private
    ' -----------------------------------------------------------------------------
    Private Function _checkUnsavedChanges() As Boolean
        _aclProgram.CheckUnsavedChanges(_dckPanCodeEditor.sciCodeEditor.Text)

        Dim cancel As Boolean = False
        If _aclProgram.UnsavedChanges Then
            Dim erg As DialogResult = MessageBox.Show($"Sollen die Änderungen am Programm noch gespeichert werden?", "Ungespeicherte Änderungen", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning)
            If erg = DialogResult.Cancel Then
                cancel = True
            ElseIf erg = DialogResult.Yes Then
                If Not _aclProgram.Save(_dckPanCodeEditor.sciCodeEditor.Text) Then
                    cancel = True
                End If
            End If
        End If

        Return cancel
    End Function
    Private Sub _setLogLevel(lvl As Integer)
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
    Private Sub _configureDockPanel()
        ' Create & Configure DockPanel
        With dckPanel
            .Theme = New VS2015LightTheme
            .Parent = Me
            .Dock = DockStyle.Fill
            .BringToFront()
            .Theme.Extender.FloatWindowFactory = New CustomFloatWindowFactory
            .DockLeftPortion = 0.22
            .DockRightPortion = 0.22
            .DockTopPortion = 0.22
            .DockBottomPortion = 0.22
        End With

        _dckPanCodeEditor.HideOnClose = True
        _dckPanLog.HideOnClose = True
        _dckPanComLogSerial.HideOnClose = True
        _dckPanProgLog.HideOnClose = True
        _dckPanVariables.HideOnClose = True
        _dckPanTeachpoints.HideOnClose = True
        _dckPanRoboStatus.HideOnClose = True
        _dckPanProgramTools.HideOnClose = True
        _dckPanCtrl.HideOnClose = True
        _dckPanReference.HideOnClose = True
        _dckPanSettings.HideOnClose = True

        'Edit Names
        _dckPanLog.Text = "Robo Log"
        _dckPanComLogSerial.Text = "Serial Log"
        _dckPanProgLog.Text = "Programm Log"

        'Check for saved View Settings
        If (File.Exists(_viewSettingsFilename)) Then
            dckPanel.LoadFromXml(_viewSettingsFilename, AddressOf _getContent)
            _logger.Log($"[MAIN] ""{_viewSettingsFilename}"" geladen", Logger.LogLevel.DEBUG)
        Else
            ' Add Panels
            ' Document
            _dckPanCodeEditor.Show(dckPanel, DockState.Document)

            ' Right
            _dckPanRoboStatus.Show(dckPanel, DockState.DockRight)
            _dckPanReference.Show(_dckPanRoboStatus.Pane, DockAlignment.Bottom, 0.16)
            _dckPanCtrl.Show(_dckPanRoboStatus.Pane, DockAlignment.Bottom, 0.6)

            ' Left
            _dckPanTeachpoints.Show(dckPanel, DockState.DockLeft)
            _dckPanSettings.Show(_dckPanTeachpoints.Pane, DockAlignment.Bottom, 0.5)
            _dckPanVariables.Show(_dckPanTeachpoints.Pane, DockAlignment.Bottom, 0.5)
            _dckPanSettings.Hide()

            ' Float
            _dckPanProgramTools.Show(dckPanel, DockState.Float)
            _dckPanProgramTools.Hide()

            ' Bottom
            _dckPanLog.Show(dckPanel, DockState.DockBottom)
            _dckPanComLogSerial.Show(dckPanel, DockState.DockBottom)
            _dckPanProgLog.Show(dckPanel, DockState.DockBottom)

            _logger.Log("[MAIN] Standardansicht geladen", Logger.LogLevel.DEBUG)
        End If

        'activate default panels
        _dckPanLog.Activate()
    End Sub

    Private Function _getContent(persist As String) As IDockContent
        If persist.EndsWith("CodeEditor") Then
            Return _dckPanCodeEditor
        ElseIf persist.EndsWith("Ctrl") Then
            Return _dckPanCtrl
        ElseIf persist.EndsWith("Robo Log") Then
            Return _dckPanLog
        ElseIf persist.EndsWith("Serial Log") Then
            Return _dckPanComLogSerial
        ElseIf persist.EndsWith("Programm Log") Then
            Return _dckPanProgLog
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

    Private Sub _enableDisableElements()
        If InvokeRequired Then
            Invoke(Sub() _enableDisableElements())
            Return
        End If

        tsBtnConnect.Enabled = Not SerialConnected And _serialPortsAvailable
        tsBtnDisconnect.Enabled = Not ProgramRunning
        tsBtnProgCheck.Enabled = Not ProgramRunning
        tsBtnProgRun.Enabled = SerialConnected And Not RobotBusy And Not ProgramRunning And _roboControl.AllRefOkay
        tsBtnProgStop.Enabled = ProgramRunning
    End Sub

    Private Sub _updateUndoRedoButtons()
        tsBtnUndo.Enabled = _dckPanCodeEditor.sciCodeEditor.CanUndo
        msUndo.Enabled = _dckPanCodeEditor.sciCodeEditor.CanUndo
        tsBtnRedo.Enabled = _dckPanCodeEditor.sciCodeEditor.CanRedo
        msRedo.Enabled = _dckPanCodeEditor.sciCodeEditor.CanRedo
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
            _serialPortsAvailable = True
        Else
            _serialPortsAvailable = False
        End If

        _enableDisableElements()
    End Sub
    Private Sub _eEditorTextChanged(sender As Object, e As EventArgs)
        _updateUndoRedoButtons()
    End Sub
    Private Sub _eComSerialConnected() Handles _roboControl.SerialConnected
        SerialConnected = True
        RobotBusy = False
        RaiseEvent SerialConnectionStateChanged()
        RaiseEvent RefreshEvent()

        _enableDisableElements()
    End Sub
    Private Sub _eComSerialDisconnected() Handles _roboControl.SerialDisconnected
        SerialConnected = False
        RobotBusy = False

        _aclProgram.ForceStopProgram() ' Kill Thread
        RaiseEvent SerialConnectionStateChanged()
        RaiseEvent RefreshEvent()

        _enableDisableElements()
    End Sub
    Private Sub _eRoboBusy(busy As Boolean, delay As Boolean) Handles _roboControl.RoboBusy
        RobotBusy = busy
        RaiseEvent RefreshEvent()

        _enableDisableElements()
    End Sub
    Private Sub _eRefresh() Handles _aclProgram.ProgramStarted, _aclProgram.ProgramFinished, _roboControl.RoboRefStateChanged
        RaiseEvent RefreshEvent()

        _enableDisableElements()
    End Sub
    Private Sub _eRoboParameterChanged(parameterChanged As Settings.ParameterChangedParameter) Handles _roboControl.RoboParameterChanged
        Dim all As Boolean = parameterChanged = Settings.ParameterChangedParameter.All
        If parameterChanged = Settings.ParameterChangedParameter.TCPServerParameter Or all Then
            If _roboControl.Pref.TCPServerParameter.Listen Then
                tsLblTcpServerStatus.Visible = True
                tsLblTCPServerStatusTitle.Visible = True
                tsSepTCPServerStatus.Visible = True
            Else
                tsLblTcpServerStatus.Visible = False
                tsLblTCPServerStatusTitle.Visible = False
                tsSepTCPServerStatus.Visible = False
            End If
            _aclProgram.Init()
        End If
    End Sub

    ' ACL-Program
    Private Sub _eProgramUpdated() Handles _aclProgram.ProgramUpdatedEvent
        numSpeed.Value = CDec(_aclProgram.MaxSpeed)
        numAcc.Value = CDec(_aclProgram.MaxAcc)
    End Sub
    Private Sub _eDoJointMove(jointAngles As JointAngles, acc As Double, speed As Double) Handles _aclProgram.DoJointMove
        _roboControl.SetSpeedAndAcc(speed, acc)
        _roboControl.DoJointMov(True, jointAngles)
    End Sub
    Private Sub _eDoCartMove(cartCoords As CartCoords, acc As Double, speed As Double) Handles _aclProgram.DoCartMove
        _roboControl.SetSpeedAndAcc(speed, acc)
        _roboControl.DoTCPMov(cartCoords)
    End Sub
    Private Sub _eDoRef(all As Boolean, axis() As Boolean) Handles _aclProgram.DoRef
        If all Then
            _roboControl.DoRef(True, True, True, True, True, True)
        Else
            _roboControl.DoRef(axis(0), axis(1), axis(2), axis(3), axis(4), axis(5))
        End If
    End Sub
    Private Sub _eDoServoMove(srvNr As Int32, prc As Double, speed As Int32) Handles _aclProgram.DoServoMove
        _roboControl.MoveServoPrc(srvNr, prc, speed)
    End Sub
    Private Sub _eDoDelay(delay As Int32) Handles _aclProgram.DoDelay
        _roboControl.DoDelay(delay)
    End Sub
    Private Sub _eDoPrint(msg As String) Handles _aclProgram.DoPrint
        _loggerACLProg.Log(msg, Logger.LogLevel.INFO)
    End Sub

    ' TCP Server
    Private Sub _eTCPConnected()
        If InvokeRequired Then
            Invoke(Sub() _eTCPConnected())
            Return
        End If

        tsLblTcpServerStatus.Text = "verbunden"
        tsLblTcpServerStatus.ForeColor = Color.Green
    End Sub
    Private Sub _eTCPDisconnected()
        If InvokeRequired Then
            Invoke(Sub() _eTCPDisconnected())
            Return
        End If

        tsLblTcpServerStatus.Text = "getrennt"
        tsLblTcpServerStatus.ForeColor = Color.Red
    End Sub
End Class