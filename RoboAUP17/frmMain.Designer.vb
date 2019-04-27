<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.ssLblStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.MenuStrip = New System.Windows.Forms.MenuStrip()
        Me.DateiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.msNew = New System.Windows.Forms.ToolStripMenuItem()
        Me.msOpen = New System.Windows.Forms.ToolStripMenuItem()
        Me.msSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.msSaveAs = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.msExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.BearbeitenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.msUndo = New System.Windows.Forms.ToolStripMenuItem()
        Me.msRedo = New System.Windows.Forms.ToolStripMenuItem()
        Me.AnsichtToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.msSaveView = New System.Windows.Forms.ToolStripMenuItem()
        Me.msDefaulView = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.RoboterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.msShowVars = New System.Windows.Forms.ToolStripMenuItem()
        Me.msShowTeachpoints = New System.Windows.Forms.ToolStripMenuItem()
        Me.msShowACLEditor = New System.Windows.Forms.ToolStripMenuItem()
        Me.msShowACLToolbox = New System.Windows.Forms.ToolStripMenuItem()
        Me.RoboterToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.msShowRoboStatus = New System.Windows.Forms.ToolStripMenuItem()
        Me.msShowRoboReference = New System.Windows.Forms.ToolStripMenuItem()
        Me.msShowRoboCtrl = New System.Windows.Forms.ToolStripMenuItem()
        Me.msShowLog = New System.Windows.Forms.ToolStripMenuItem()
        Me.msShowComLogSerial = New System.Windows.Forms.ToolStripMenuItem()
        Me.msShowProgLog = New System.Windows.Forms.ToolStripMenuItem()
        Me.EinstellungenToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.msDenavitHartPar = New System.Windows.Forms.ToolStripMenuItem()
        Me.msRoboParameter = New System.Windows.Forms.ToolStripMenuItem()
        Me.msFrames = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.msTCPServer = New System.Windows.Forms.ToolStripMenuItem()
        Me.HilfeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.msGitHub = New System.Windows.Forms.ToolStripMenuItem()
        Me.msArduinoFirmware = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.LoggingLevelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.msSetLogLvlDebug = New System.Windows.Forms.ToolStripMenuItem()
        Me.msSetLogLvlInfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.msSetLogLvlWarning = New System.Windows.Forms.ToolStripMenuItem()
        Me.msSetLogLvlError = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.msShowInfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsBtnOpen = New System.Windows.Forms.ToolStripButton()
        Me.tsBtnSave = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsCbComPort = New System.Windows.Forms.ToolStripComboBox()
        Me.tsBtnConnect = New System.Windows.Forms.ToolStripButton()
        Me.tsBtnDisconnect = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsBtnUndo = New System.Windows.Forms.ToolStripButton()
        Me.tsBtnRedo = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator()
        Me.lblSpeed = New System.Windows.Forms.ToolStripLabel()
        Me.numAcc = New RoboAUP17.ToolStripNumericUpDown()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.lblAcc = New System.Windows.Forms.ToolStripLabel()
        Me.numSpeed = New RoboAUP17.ToolStripNumericUpDown()
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsBtnProgCheck = New System.Windows.Forms.ToolStripButton()
        Me.tsBtnProgRun = New System.Windows.Forms.ToolStripButton()
        Me.tsBtnProgStop = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsBtnEStop = New System.Windows.Forms.ToolStripButton()
        Me.tsSepTCPServerStatus = New System.Windows.Forms.ToolStripSeparator()
        Me.tsLblTCPServerStatusTitle = New System.Windows.Forms.ToolStripLabel()
        Me.tsLblTcpServerStatus = New System.Windows.Forms.ToolStripLabel()
        Me.StatusStrip.SuspendLayout()
        Me.MenuStrip.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStrip
        '
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ssLblStatus})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 647)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(1367, 22)
        Me.StatusStrip.TabIndex = 0
        Me.StatusStrip.Text = "StatusStrip"
        '
        'ssLblStatus
        '
        Me.ssLblStatus.BackColor = System.Drawing.SystemColors.Control
        Me.ssLblStatus.Name = "ssLblStatus"
        Me.ssLblStatus.Size = New System.Drawing.Size(0, 17)
        '
        'MenuStrip
        '
        Me.MenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DateiToolStripMenuItem, Me.BearbeitenToolStripMenuItem, Me.AnsichtToolStripMenuItem, Me.EinstellungenToolStripMenuItem1, Me.HilfeToolStripMenuItem})
        Me.MenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip.Name = "MenuStrip"
        Me.MenuStrip.Size = New System.Drawing.Size(1367, 24)
        Me.MenuStrip.TabIndex = 1
        Me.MenuStrip.Text = "MenuStrip"
        '
        'DateiToolStripMenuItem
        '
        Me.DateiToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.msNew, Me.msOpen, Me.msSave, Me.msSaveAs, Me.ToolStripSeparator4, Me.msExit})
        Me.DateiToolStripMenuItem.Name = "DateiToolStripMenuItem"
        Me.DateiToolStripMenuItem.Size = New System.Drawing.Size(46, 20)
        Me.DateiToolStripMenuItem.Text = "Datei"
        '
        'msNew
        '
        Me.msNew.Image = CType(resources.GetObject("msNew.Image"), System.Drawing.Image)
        Me.msNew.Name = "msNew"
        Me.msNew.Size = New System.Drawing.Size(180, 22)
        Me.msNew.Text = "Neu"
        '
        'msOpen
        '
        Me.msOpen.Image = CType(resources.GetObject("msOpen.Image"), System.Drawing.Image)
        Me.msOpen.Name = "msOpen"
        Me.msOpen.Size = New System.Drawing.Size(180, 22)
        Me.msOpen.Text = "Öffnen"
        '
        'msSave
        '
        Me.msSave.Image = CType(resources.GetObject("msSave.Image"), System.Drawing.Image)
        Me.msSave.Name = "msSave"
        Me.msSave.Size = New System.Drawing.Size(180, 22)
        Me.msSave.Text = "Speichern"
        '
        'msSaveAs
        '
        Me.msSaveAs.Image = CType(resources.GetObject("msSaveAs.Image"), System.Drawing.Image)
        Me.msSaveAs.Name = "msSaveAs"
        Me.msSaveAs.Size = New System.Drawing.Size(180, 22)
        Me.msSaveAs.Text = "Speichern unter..."
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(177, 6)
        '
        'msExit
        '
        Me.msExit.Image = CType(resources.GetObject("msExit.Image"), System.Drawing.Image)
        Me.msExit.Name = "msExit"
        Me.msExit.Size = New System.Drawing.Size(180, 22)
        Me.msExit.Text = "Beenden"
        '
        'BearbeitenToolStripMenuItem
        '
        Me.BearbeitenToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.msUndo, Me.msRedo})
        Me.BearbeitenToolStripMenuItem.Name = "BearbeitenToolStripMenuItem"
        Me.BearbeitenToolStripMenuItem.Size = New System.Drawing.Size(75, 20)
        Me.BearbeitenToolStripMenuItem.Text = "Bearbeiten"
        '
        'msUndo
        '
        Me.msUndo.Enabled = False
        Me.msUndo.Image = CType(resources.GetObject("msUndo.Image"), System.Drawing.Image)
        Me.msUndo.Name = "msUndo"
        Me.msUndo.Size = New System.Drawing.Size(180, 22)
        Me.msUndo.Text = "Rückgängig"
        '
        'msRedo
        '
        Me.msRedo.Enabled = False
        Me.msRedo.Image = CType(resources.GetObject("msRedo.Image"), System.Drawing.Image)
        Me.msRedo.Name = "msRedo"
        Me.msRedo.Size = New System.Drawing.Size(180, 22)
        Me.msRedo.Text = "Wiederholen"
        '
        'AnsichtToolStripMenuItem
        '
        Me.AnsichtToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.msSaveView, Me.msDefaulView, Me.ToolStripSeparator3, Me.RoboterToolStripMenuItem})
        Me.AnsichtToolStripMenuItem.Name = "AnsichtToolStripMenuItem"
        Me.AnsichtToolStripMenuItem.Size = New System.Drawing.Size(59, 20)
        Me.AnsichtToolStripMenuItem.Text = "Ansicht"
        '
        'msSaveView
        '
        Me.msSaveView.Name = "msSaveView"
        Me.msSaveView.Size = New System.Drawing.Size(185, 22)
        Me.msSaveView.Text = "Ansicht speichern"
        '
        'msDefaulView
        '
        Me.msDefaulView.Name = "msDefaulView"
        Me.msDefaulView.Size = New System.Drawing.Size(185, 22)
        Me.msDefaulView.Text = "Ansicht zurücksetzen"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(182, 6)
        '
        'RoboterToolStripMenuItem
        '
        Me.RoboterToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.msShowVars, Me.msShowTeachpoints, Me.msShowACLEditor, Me.msShowACLToolbox, Me.RoboterToolStripMenuItem1, Me.msShowLog, Me.msShowComLogSerial, Me.msShowProgLog})
        Me.RoboterToolStripMenuItem.Name = "RoboterToolStripMenuItem"
        Me.RoboterToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.RoboterToolStripMenuItem.Text = "Anzeigen"
        '
        'msShowVars
        '
        Me.msShowVars.Name = "msShowVars"
        Me.msShowVars.Size = New System.Drawing.Size(180, 22)
        Me.msShowVars.Text = "Variablen"
        '
        'msShowTeachpoints
        '
        Me.msShowTeachpoints.Name = "msShowTeachpoints"
        Me.msShowTeachpoints.Size = New System.Drawing.Size(180, 22)
        Me.msShowTeachpoints.Text = "Teachpunkte"
        '
        'msShowACLEditor
        '
        Me.msShowACLEditor.Name = "msShowACLEditor"
        Me.msShowACLEditor.Size = New System.Drawing.Size(180, 22)
        Me.msShowACLEditor.Text = "ACL-Editor"
        '
        'msShowACLToolbox
        '
        Me.msShowACLToolbox.Enabled = False
        Me.msShowACLToolbox.Name = "msShowACLToolbox"
        Me.msShowACLToolbox.Size = New System.Drawing.Size(180, 22)
        Me.msShowACLToolbox.Text = "ACL-Toolbox"
        '
        'RoboterToolStripMenuItem1
        '
        Me.RoboterToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.msShowRoboStatus, Me.msShowRoboReference, Me.msShowRoboCtrl})
        Me.RoboterToolStripMenuItem1.Name = "RoboterToolStripMenuItem1"
        Me.RoboterToolStripMenuItem1.Size = New System.Drawing.Size(180, 22)
        Me.RoboterToolStripMenuItem1.Text = "Robotersteuerung"
        '
        'msShowRoboStatus
        '
        Me.msShowRoboStatus.Name = "msShowRoboStatus"
        Me.msShowRoboStatus.Size = New System.Drawing.Size(180, 22)
        Me.msShowRoboStatus.Text = "Status"
        '
        'msShowRoboReference
        '
        Me.msShowRoboReference.Name = "msShowRoboReference"
        Me.msShowRoboReference.Size = New System.Drawing.Size(180, 22)
        Me.msShowRoboReference.Text = "Referenz"
        '
        'msShowRoboCtrl
        '
        Me.msShowRoboCtrl.Name = "msShowRoboCtrl"
        Me.msShowRoboCtrl.Size = New System.Drawing.Size(180, 22)
        Me.msShowRoboCtrl.Text = "Robo Steuerung"
        '
        'msShowLog
        '
        Me.msShowLog.Name = "msShowLog"
        Me.msShowLog.Size = New System.Drawing.Size(180, 22)
        Me.msShowLog.Text = "Allgemeines Log"
        '
        'msShowComLogSerial
        '
        Me.msShowComLogSerial.Name = "msShowComLogSerial"
        Me.msShowComLogSerial.Size = New System.Drawing.Size(180, 22)
        Me.msShowComLogSerial.Text = "Comlog (Serial)"
        '
        'msShowProgLog
        '
        Me.msShowProgLog.Name = "msShowProgLog"
        Me.msShowProgLog.Size = New System.Drawing.Size(180, 22)
        Me.msShowProgLog.Text = "Programm Log"
        '
        'EinstellungenToolStripMenuItem1
        '
        Me.EinstellungenToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.msDenavitHartPar, Me.msRoboParameter, Me.msFrames, Me.ToolStripSeparator5, Me.msTCPServer})
        Me.EinstellungenToolStripMenuItem1.Name = "EinstellungenToolStripMenuItem1"
        Me.EinstellungenToolStripMenuItem1.Size = New System.Drawing.Size(90, 20)
        Me.EinstellungenToolStripMenuItem1.Text = "Einstellungen"
        '
        'msDenavitHartPar
        '
        Me.msDenavitHartPar.Image = CType(resources.GetObject("msDenavitHartPar.Image"), System.Drawing.Image)
        Me.msDenavitHartPar.Name = "msDenavitHartPar"
        Me.msDenavitHartPar.Size = New System.Drawing.Size(247, 22)
        Me.msDenavitHartPar.Text = "Denavit-Hartenberg-Parameter..."
        '
        'msRoboParameter
        '
        Me.msRoboParameter.Image = CType(resources.GetObject("msRoboParameter.Image"), System.Drawing.Image)
        Me.msRoboParameter.Name = "msRoboParameter"
        Me.msRoboParameter.Size = New System.Drawing.Size(247, 22)
        Me.msRoboParameter.Text = "Robo Parameter..."
        '
        'msFrames
        '
        Me.msFrames.Image = CType(resources.GetObject("msFrames.Image"), System.Drawing.Image)
        Me.msFrames.Name = "msFrames"
        Me.msFrames.Size = New System.Drawing.Size(247, 22)
        Me.msFrames.Text = "Toolframe / Workframe..."
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(244, 6)
        '
        'msTCPServer
        '
        Me.msTCPServer.Image = CType(resources.GetObject("msTCPServer.Image"), System.Drawing.Image)
        Me.msTCPServer.Name = "msTCPServer"
        Me.msTCPServer.Size = New System.Drawing.Size(247, 22)
        Me.msTCPServer.Text = "TCP-Server..."
        '
        'HilfeToolStripMenuItem
        '
        Me.HilfeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.msGitHub, Me.msArduinoFirmware, Me.ToolStripSeparator7, Me.LoggingLevelToolStripMenuItem, Me.ToolStripSeparator6, Me.msShowInfo})
        Me.HilfeToolStripMenuItem.Name = "HilfeToolStripMenuItem"
        Me.HilfeToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.HilfeToolStripMenuItem.Text = "Hilfe"
        '
        'msGitHub
        '
        Me.msGitHub.Image = CType(resources.GetObject("msGitHub.Image"), System.Drawing.Image)
        Me.msGitHub.Name = "msGitHub"
        Me.msGitHub.Size = New System.Drawing.Size(200, 22)
        Me.msGitHub.Text = "Aup17 Robo auf GitHub"
        '
        'msArduinoFirmware
        '
        Me.msArduinoFirmware.Image = CType(resources.GetObject("msArduinoFirmware.Image"), System.Drawing.Image)
        Me.msArduinoFirmware.Name = "msArduinoFirmware"
        Me.msArduinoFirmware.Size = New System.Drawing.Size(200, 22)
        Me.msArduinoFirmware.Text = "Arduino Firmware"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(197, 6)
        '
        'LoggingLevelToolStripMenuItem
        '
        Me.LoggingLevelToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.msSetLogLvlDebug, Me.msSetLogLvlInfo, Me.msSetLogLvlWarning, Me.msSetLogLvlError})
        Me.LoggingLevelToolStripMenuItem.Name = "LoggingLevelToolStripMenuItem"
        Me.LoggingLevelToolStripMenuItem.Size = New System.Drawing.Size(200, 22)
        Me.LoggingLevelToolStripMenuItem.Text = "Logging Level"
        '
        'msSetLogLvlDebug
        '
        Me.msSetLogLvlDebug.Name = "msSetLogLvlDebug"
        Me.msSetLogLvlDebug.Size = New System.Drawing.Size(137, 22)
        Me.msSetLogLvlDebug.Text = "Debug"
        '
        'msSetLogLvlInfo
        '
        Me.msSetLogLvlInfo.Name = "msSetLogLvlInfo"
        Me.msSetLogLvlInfo.Size = New System.Drawing.Size(137, 22)
        Me.msSetLogLvlInfo.Text = "Information"
        '
        'msSetLogLvlWarning
        '
        Me.msSetLogLvlWarning.Name = "msSetLogLvlWarning"
        Me.msSetLogLvlWarning.Size = New System.Drawing.Size(137, 22)
        Me.msSetLogLvlWarning.Text = "Warnung"
        '
        'msSetLogLvlError
        '
        Me.msSetLogLvlError.Name = "msSetLogLvlError"
        Me.msSetLogLvlError.Size = New System.Drawing.Size(137, 22)
        Me.msSetLogLvlError.Text = "Fehler"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(197, 6)
        '
        'msShowInfo
        '
        Me.msShowInfo.Enabled = False
        Me.msShowInfo.Image = CType(resources.GetObject("msShowInfo.Image"), System.Drawing.Image)
        Me.msShowInfo.Name = "msShowInfo"
        Me.msShowInfo.Size = New System.Drawing.Size(200, 22)
        Me.msShowInfo.Text = "Informationen"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsBtnOpen, Me.tsBtnSave, Me.ToolStripSeparator1, Me.tsCbComPort, Me.tsBtnConnect, Me.tsBtnDisconnect, Me.ToolStripSeparator2, Me.tsBtnUndo, Me.tsBtnRedo, Me.ToolStripSeparator9, Me.lblSpeed, Me.numAcc, Me.ToolStripSeparator8, Me.lblAcc, Me.numSpeed, Me.ToolStripSeparator11, Me.tsBtnProgCheck, Me.tsBtnProgRun, Me.tsBtnProgStop, Me.ToolStripSeparator10, Me.tsBtnEStop, Me.tsSepTCPServerStatus, Me.tsLblTCPServerStatusTitle, Me.tsLblTcpServerStatus})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 24)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1367, 26)
        Me.ToolStrip1.TabIndex = 2
        Me.ToolStrip1.Text = "ToolStrip"
        '
        'tsBtnOpen
        '
        Me.tsBtnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsBtnOpen.Image = CType(resources.GetObject("tsBtnOpen.Image"), System.Drawing.Image)
        Me.tsBtnOpen.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtnOpen.Name = "tsBtnOpen"
        Me.tsBtnOpen.Size = New System.Drawing.Size(23, 23)
        Me.tsBtnOpen.Text = "Öffnen"
        '
        'tsBtnSave
        '
        Me.tsBtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsBtnSave.Image = CType(resources.GetObject("tsBtnSave.Image"), System.Drawing.Image)
        Me.tsBtnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtnSave.Name = "tsBtnSave"
        Me.tsBtnSave.Size = New System.Drawing.Size(23, 23)
        Me.tsBtnSave.Text = "Speichern"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 26)
        '
        'tsCbComPort
        '
        Me.tsCbComPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.tsCbComPort.Name = "tsCbComPort"
        Me.tsCbComPort.Size = New System.Drawing.Size(121, 26)
        '
        'tsBtnConnect
        '
        Me.tsBtnConnect.Enabled = False
        Me.tsBtnConnect.Image = CType(resources.GetObject("tsBtnConnect.Image"), System.Drawing.Image)
        Me.tsBtnConnect.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtnConnect.Name = "tsBtnConnect"
        Me.tsBtnConnect.Size = New System.Drawing.Size(80, 23)
        Me.tsBtnConnect.Text = "Verbinden"
        '
        'tsBtnDisconnect
        '
        Me.tsBtnDisconnect.Image = CType(resources.GetObject("tsBtnDisconnect.Image"), System.Drawing.Image)
        Me.tsBtnDisconnect.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtnDisconnect.Name = "tsBtnDisconnect"
        Me.tsBtnDisconnect.Size = New System.Drawing.Size(70, 23)
        Me.tsBtnDisconnect.Text = "Trennen"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 26)
        '
        'tsBtnUndo
        '
        Me.tsBtnUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsBtnUndo.Enabled = False
        Me.tsBtnUndo.Image = CType(resources.GetObject("tsBtnUndo.Image"), System.Drawing.Image)
        Me.tsBtnUndo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtnUndo.Name = "tsBtnUndo"
        Me.tsBtnUndo.Size = New System.Drawing.Size(23, 23)
        Me.tsBtnUndo.Text = "Rückgängig"
        '
        'tsBtnRedo
        '
        Me.tsBtnRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsBtnRedo.Enabled = False
        Me.tsBtnRedo.Image = CType(resources.GetObject("tsBtnRedo.Image"), System.Drawing.Image)
        Me.tsBtnRedo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtnRedo.Name = "tsBtnRedo"
        Me.tsBtnRedo.Size = New System.Drawing.Size(23, 23)
        Me.tsBtnRedo.Text = "Wiederholen"
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(6, 26)
        '
        'lblSpeed
        '
        Me.lblSpeed.Name = "lblSpeed"
        Me.lblSpeed.Size = New System.Drawing.Size(80, 23)
        Me.lblSpeed.Text = "Geschw. (°/s):"
        '
        'numAcc
        '
        Me.numAcc.DecimalPlaces = 2
        Me.numAcc.Maximum = New Decimal(New Integer() {360, 0, 0, 0})
        Me.numAcc.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numAcc.Name = "numAcc"
        Me.numAcc.Size = New System.Drawing.Size(56, 23)
        Me.numAcc.Text = "100,00"
        Me.numAcc.Value = New Decimal(New Integer() {10000, 0, 0, 131072})
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(6, 26)
        '
        'lblAcc
        '
        Me.lblAcc.Name = "lblAcc"
        Me.lblAcc.Size = New System.Drawing.Size(77, 23)
        Me.lblAcc.Text = "Beschl. (°/s²):"
        '
        'numSpeed
        '
        Me.numSpeed.DecimalPlaces = 2
        Me.numSpeed.Maximum = New Decimal(New Integer() {360, 0, 0, 0})
        Me.numSpeed.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numSpeed.Name = "numSpeed"
        Me.numSpeed.Size = New System.Drawing.Size(56, 23)
        Me.numSpeed.Text = "50,00"
        Me.numSpeed.Value = New Decimal(New Integer() {5000, 0, 0, 131072})
        '
        'ToolStripSeparator11
        '
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        Me.ToolStripSeparator11.Size = New System.Drawing.Size(6, 26)
        '
        'tsBtnProgCheck
        '
        Me.tsBtnProgCheck.Image = CType(resources.GetObject("tsBtnProgCheck.Image"), System.Drawing.Image)
        Me.tsBtnProgCheck.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtnProgCheck.Name = "tsBtnProgCheck"
        Me.tsBtnProgCheck.Size = New System.Drawing.Size(122, 23)
        Me.tsBtnProgCheck.Text = "Programm prüfen"
        '
        'tsBtnProgRun
        '
        Me.tsBtnProgRun.Enabled = False
        Me.tsBtnProgRun.Image = CType(resources.GetObject("tsBtnProgRun.Image"), System.Drawing.Image)
        Me.tsBtnProgRun.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtnProgRun.Name = "tsBtnProgRun"
        Me.tsBtnProgRun.Size = New System.Drawing.Size(123, 23)
        Me.tsBtnProgRun.Text = "Programm starten"
        '
        'tsBtnProgStop
        '
        Me.tsBtnProgStop.Enabled = False
        Me.tsBtnProgStop.Image = CType(resources.GetObject("tsBtnProgStop.Image"), System.Drawing.Image)
        Me.tsBtnProgStop.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtnProgStop.Name = "tsBtnProgStop"
        Me.tsBtnProgStop.Size = New System.Drawing.Size(130, 23)
        Me.tsBtnProgStop.Text = "Programm stoppen"
        '
        'ToolStripSeparator10
        '
        Me.ToolStripSeparator10.Name = "ToolStripSeparator10"
        Me.ToolStripSeparator10.Size = New System.Drawing.Size(6, 26)
        '
        'tsBtnEStop
        '
        Me.tsBtnEStop.Image = CType(resources.GetObject("tsBtnEStop.Image"), System.Drawing.Image)
        Me.tsBtnEStop.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtnEStop.Name = "tsBtnEStop"
        Me.tsBtnEStop.Size = New System.Drawing.Size(132, 23)
        Me.tsBtnEStop.Text = "Not-Halt (Leertaste)"
        '
        'tsSepTCPServerStatus
        '
        Me.tsSepTCPServerStatus.Name = "tsSepTCPServerStatus"
        Me.tsSepTCPServerStatus.Size = New System.Drawing.Size(6, 26)
        '
        'tsLblTCPServerStatusTitle
        '
        Me.tsLblTCPServerStatusTitle.Name = "tsLblTCPServerStatusTitle"
        Me.tsLblTCPServerStatusTitle.Size = New System.Drawing.Size(67, 23)
        Me.tsLblTCPServerStatusTitle.Text = "TCP-Client:"
        '
        'tsLblTcpServerStatus
        '
        Me.tsLblTcpServerStatus.ForeColor = System.Drawing.Color.Red
        Me.tsLblTcpServerStatus.Name = "tsLblTcpServerStatus"
        Me.tsLblTcpServerStatus.Size = New System.Drawing.Size(52, 23)
        Me.tsLblTcpServerStatus.Text = "getrennt"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(1367, 669)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.StatusStrip)
        Me.Controls.Add(Me.MenuStrip)
        Me.DoubleBuffered = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.MenuStrip
        Me.MinimumSize = New System.Drawing.Size(300, 300)
        Me.Name = "frmMain"
        Me.Text = "Aup17 Robo"
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.MenuStrip.ResumeLayout(False)
        Me.MenuStrip.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents StatusStrip As StatusStrip
    Friend WithEvents MenuStrip As MenuStrip
    Friend WithEvents DateiToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BearbeitenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents ssLblStatus As ToolStripStatusLabel
    Friend WithEvents HilfeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents msShowInfo As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents tsCbComPort As ToolStripComboBox
    Friend WithEvents tsBtnDisconnect As ToolStripButton
    Friend WithEvents msOpen As ToolStripMenuItem
    Friend WithEvents msSave As ToolStripMenuItem
    Friend WithEvents msSaveAs As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents msNew As ToolStripMenuItem
    Friend WithEvents AnsichtToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents msSaveView As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents EinstellungenToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents msDenavitHartPar As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents msExit As ToolStripMenuItem
    Friend WithEvents msUndo As ToolStripMenuItem
    Friend WithEvents msRedo As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents msTCPServer As ToolStripMenuItem
    Friend WithEvents msGitHub As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As ToolStripSeparator
    Friend WithEvents tsBtnOpen As ToolStripButton
    Friend WithEvents tsBtnSave As ToolStripButton
    Friend WithEvents tsBtnConnect As ToolStripButton
    Friend WithEvents msDefaulView As ToolStripMenuItem
    Friend WithEvents RoboterToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents msShowVars As ToolStripMenuItem
    Friend WithEvents msShowTeachpoints As ToolStripMenuItem
    Friend WithEvents msShowACLEditor As ToolStripMenuItem
    Friend WithEvents msShowACLToolbox As ToolStripMenuItem
    Friend WithEvents RoboterToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents msShowRoboStatus As ToolStripMenuItem
    Friend WithEvents msShowRoboReference As ToolStripMenuItem
    Friend WithEvents msShowRoboCtrl As ToolStripMenuItem
    Friend WithEvents msShowLog As ToolStripMenuItem
    Friend WithEvents msShowComLogSerial As ToolStripMenuItem
    Friend WithEvents msShowProgLog As ToolStripMenuItem
    Friend WithEvents LoggingLevelToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents msSetLogLvlDebug As ToolStripMenuItem
    Friend WithEvents msSetLogLvlInfo As ToolStripMenuItem
    Friend WithEvents msSetLogLvlWarning As ToolStripMenuItem
    Friend WithEvents msSetLogLvlError As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As ToolStripSeparator
    Friend WithEvents msRoboParameter As ToolStripMenuItem
    Friend WithEvents tsBtnProgRun As ToolStripButton
    Friend WithEvents tsBtnProgStop As ToolStripButton
    Friend WithEvents tsBtnProgCheck As ToolStripButton
    Friend WithEvents ToolStripSeparator8 As ToolStripSeparator
    Friend WithEvents tsBtnEStop As ToolStripButton
    Friend WithEvents msArduinoFirmware As ToolStripMenuItem
    Friend WithEvents msFrames As ToolStripMenuItem
    Friend WithEvents lblSpeed As ToolStripLabel
    Friend WithEvents numSpeed As ToolStripNumericUpDown
    Friend WithEvents lblAcc As ToolStripLabel
    Friend WithEvents numAcc As ToolStripNumericUpDown
    Friend WithEvents ToolStripSeparator11 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator10 As ToolStripSeparator
    Friend WithEvents tsSepTCPServerStatus As ToolStripSeparator
    Friend WithEvents tsLblTCPServerStatusTitle As ToolStripLabel
    Friend WithEvents tsLblTcpServerStatus As ToolStripLabel
    Friend WithEvents tsBtnUndo As ToolStripButton
    Friend WithEvents tsBtnRedo As ToolStripButton
    Friend WithEvents ToolStripSeparator9 As ToolStripSeparator
End Class
