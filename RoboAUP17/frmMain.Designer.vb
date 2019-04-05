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
        Me.msShowTeachbox = New System.Windows.Forms.ToolStripMenuItem()
        Me.msShowLog = New System.Windows.Forms.ToolStripMenuItem()
        Me.msShowComLogSerial = New System.Windows.Forms.ToolStripMenuItem()
        Me.msShowComLogTCPIP = New System.Windows.Forms.ToolStripMenuItem()
        Me.EinstellungenToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.msDenavitHartPar = New System.Windows.Forms.ToolStripMenuItem()
        Me.msRoboParameter = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.msTCPServer = New System.Windows.Forms.ToolStripMenuItem()
        Me.HilfeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.msGitHub = New System.Windows.Forms.ToolStripMenuItem()
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
        Me.tsBtnProgRun = New System.Windows.Forms.ToolStripButton()
        Me.tsBtnProgStop = New System.Windows.Forms.ToolStripButton()
        Me.tsBtnProgCheck = New System.Windows.Forms.ToolStripButton()
        Me.tsBtnEStop = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
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
        Me.StatusStrip.Size = New System.Drawing.Size(1113, 22)
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
        Me.MenuStrip.Size = New System.Drawing.Size(1113, 24)
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
        Me.msNew.Size = New System.Drawing.Size(166, 22)
        Me.msNew.Text = "Neu"
        '
        'msOpen
        '
        Me.msOpen.Image = CType(resources.GetObject("msOpen.Image"), System.Drawing.Image)
        Me.msOpen.Name = "msOpen"
        Me.msOpen.Size = New System.Drawing.Size(166, 22)
        Me.msOpen.Text = "Öffnen"
        '
        'msSave
        '
        Me.msSave.Image = CType(resources.GetObject("msSave.Image"), System.Drawing.Image)
        Me.msSave.Name = "msSave"
        Me.msSave.Size = New System.Drawing.Size(166, 22)
        Me.msSave.Text = "Speichern"
        '
        'msSaveAs
        '
        Me.msSaveAs.Image = CType(resources.GetObject("msSaveAs.Image"), System.Drawing.Image)
        Me.msSaveAs.Name = "msSaveAs"
        Me.msSaveAs.Size = New System.Drawing.Size(166, 22)
        Me.msSaveAs.Text = "Speichern unter..."
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(163, 6)
        '
        'msExit
        '
        Me.msExit.Image = CType(resources.GetObject("msExit.Image"), System.Drawing.Image)
        Me.msExit.Name = "msExit"
        Me.msExit.Size = New System.Drawing.Size(166, 22)
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
        Me.msUndo.Image = CType(resources.GetObject("msUndo.Image"), System.Drawing.Image)
        Me.msUndo.Name = "msUndo"
        Me.msUndo.Size = New System.Drawing.Size(141, 22)
        Me.msUndo.Text = "Rückgängig"
        '
        'msRedo
        '
        Me.msRedo.Image = CType(resources.GetObject("msRedo.Image"), System.Drawing.Image)
        Me.msRedo.Name = "msRedo"
        Me.msRedo.Size = New System.Drawing.Size(141, 22)
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
        Me.RoboterToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.msShowVars, Me.msShowTeachpoints, Me.msShowACLEditor, Me.msShowACLToolbox, Me.RoboterToolStripMenuItem1, Me.msShowTeachbox, Me.msShowLog, Me.msShowComLogSerial, Me.msShowComLogTCPIP})
        Me.RoboterToolStripMenuItem.Name = "RoboterToolStripMenuItem"
        Me.RoboterToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.RoboterToolStripMenuItem.Text = "Anzeigen"
        '
        'msShowVars
        '
        Me.msShowVars.Name = "msShowVars"
        Me.msShowVars.Size = New System.Drawing.Size(169, 22)
        Me.msShowVars.Text = "Variablen"
        '
        'msShowTeachpoints
        '
        Me.msShowTeachpoints.Name = "msShowTeachpoints"
        Me.msShowTeachpoints.Size = New System.Drawing.Size(169, 22)
        Me.msShowTeachpoints.Text = "Teachpunkte"
        '
        'msShowACLEditor
        '
        Me.msShowACLEditor.Name = "msShowACLEditor"
        Me.msShowACLEditor.Size = New System.Drawing.Size(169, 22)
        Me.msShowACLEditor.Text = "ACL-Editor"
        '
        'msShowACLToolbox
        '
        Me.msShowACLToolbox.Name = "msShowACLToolbox"
        Me.msShowACLToolbox.Size = New System.Drawing.Size(169, 22)
        Me.msShowACLToolbox.Text = "ACL-Toolbox"
        '
        'RoboterToolStripMenuItem1
        '
        Me.RoboterToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.msShowRoboStatus, Me.msShowRoboReference, Me.msShowRoboCtrl})
        Me.RoboterToolStripMenuItem1.Name = "RoboterToolStripMenuItem1"
        Me.RoboterToolStripMenuItem1.Size = New System.Drawing.Size(169, 22)
        Me.RoboterToolStripMenuItem1.Text = "Robotersteuerung"
        '
        'msShowRoboStatus
        '
        Me.msShowRoboStatus.Name = "msShowRoboStatus"
        Me.msShowRoboStatus.Size = New System.Drawing.Size(159, 22)
        Me.msShowRoboStatus.Text = "Status"
        '
        'msShowRoboReference
        '
        Me.msShowRoboReference.Name = "msShowRoboReference"
        Me.msShowRoboReference.Size = New System.Drawing.Size(159, 22)
        Me.msShowRoboReference.Text = "Referenz"
        '
        'msShowRoboCtrl
        '
        Me.msShowRoboCtrl.Name = "msShowRoboCtrl"
        Me.msShowRoboCtrl.Size = New System.Drawing.Size(159, 22)
        Me.msShowRoboCtrl.Text = "Robo Steuerung"
        '
        'msShowTeachbox
        '
        Me.msShowTeachbox.Name = "msShowTeachbox"
        Me.msShowTeachbox.Size = New System.Drawing.Size(169, 22)
        Me.msShowTeachbox.Text = "Teachbox"
        '
        'msShowLog
        '
        Me.msShowLog.Name = "msShowLog"
        Me.msShowLog.Size = New System.Drawing.Size(169, 22)
        Me.msShowLog.Text = "Logausgabe"
        '
        'msShowComLogSerial
        '
        Me.msShowComLogSerial.Name = "msShowComLogSerial"
        Me.msShowComLogSerial.Size = New System.Drawing.Size(169, 22)
        Me.msShowComLogSerial.Text = "Comlog (Serial)"
        '
        'msShowComLogTCPIP
        '
        Me.msShowComLogTCPIP.Name = "msShowComLogTCPIP"
        Me.msShowComLogTCPIP.Size = New System.Drawing.Size(169, 22)
        Me.msShowComLogTCPIP.Text = "Comlog (TCP/IP)"
        '
        'EinstellungenToolStripMenuItem1
        '
        Me.EinstellungenToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.msDenavitHartPar, Me.msRoboParameter, Me.ToolStripSeparator5, Me.msTCPServer})
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
        Me.HilfeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.msGitHub, Me.ToolStripSeparator7, Me.LoggingLevelToolStripMenuItem, Me.ToolStripSeparator6, Me.msShowInfo})
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
        Me.msShowInfo.Image = CType(resources.GetObject("msShowInfo.Image"), System.Drawing.Image)
        Me.msShowInfo.Name = "msShowInfo"
        Me.msShowInfo.Size = New System.Drawing.Size(200, 22)
        Me.msShowInfo.Text = "Informationen"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsBtnOpen, Me.tsBtnSave, Me.ToolStripSeparator1, Me.tsCbComPort, Me.tsBtnConnect, Me.tsBtnDisconnect, Me.ToolStripSeparator2, Me.tsBtnProgCheck, Me.tsBtnProgRun, Me.tsBtnProgStop, Me.ToolStripSeparator8, Me.tsBtnEStop})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 24)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1113, 25)
        Me.ToolStrip1.TabIndex = 2
        Me.ToolStrip1.Text = "ToolStrip"
        '
        'tsBtnOpen
        '
        Me.tsBtnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsBtnOpen.Image = CType(resources.GetObject("tsBtnOpen.Image"), System.Drawing.Image)
        Me.tsBtnOpen.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtnOpen.Name = "tsBtnOpen"
        Me.tsBtnOpen.Size = New System.Drawing.Size(23, 22)
        Me.tsBtnOpen.Text = "Öffnen"
        '
        'tsBtnSave
        '
        Me.tsBtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsBtnSave.Image = CType(resources.GetObject("tsBtnSave.Image"), System.Drawing.Image)
        Me.tsBtnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtnSave.Name = "tsBtnSave"
        Me.tsBtnSave.Size = New System.Drawing.Size(23, 22)
        Me.tsBtnSave.Text = "Speichern"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'tsCbComPort
        '
        Me.tsCbComPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.tsCbComPort.Name = "tsCbComPort"
        Me.tsCbComPort.Size = New System.Drawing.Size(121, 25)
        '
        'tsBtnConnect
        '
        Me.tsBtnConnect.Image = CType(resources.GetObject("tsBtnConnect.Image"), System.Drawing.Image)
        Me.tsBtnConnect.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtnConnect.Name = "tsBtnConnect"
        Me.tsBtnConnect.Size = New System.Drawing.Size(80, 22)
        Me.tsBtnConnect.Text = "Verbinden"
        '
        'tsBtnDisconnect
        '
        Me.tsBtnDisconnect.Image = CType(resources.GetObject("tsBtnDisconnect.Image"), System.Drawing.Image)
        Me.tsBtnDisconnect.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtnDisconnect.Name = "tsBtnDisconnect"
        Me.tsBtnDisconnect.Size = New System.Drawing.Size(70, 22)
        Me.tsBtnDisconnect.Text = "Trennen"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'tsBtnProgRun
        '
        Me.tsBtnProgRun.Image = CType(resources.GetObject("tsBtnProgRun.Image"), System.Drawing.Image)
        Me.tsBtnProgRun.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtnProgRun.Name = "tsBtnProgRun"
        Me.tsBtnProgRun.Size = New System.Drawing.Size(123, 22)
        Me.tsBtnProgRun.Text = "Programm starten"
        '
        'tsBtnProgStop
        '
        Me.tsBtnProgStop.Image = CType(resources.GetObject("tsBtnProgStop.Image"), System.Drawing.Image)
        Me.tsBtnProgStop.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtnProgStop.Name = "tsBtnProgStop"
        Me.tsBtnProgStop.Size = New System.Drawing.Size(130, 22)
        Me.tsBtnProgStop.Text = "Programm stoppen"
        '
        'tsBtnProgCheck
        '
        Me.tsBtnProgCheck.Image = CType(resources.GetObject("tsBtnProgCheck.Image"), System.Drawing.Image)
        Me.tsBtnProgCheck.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtnProgCheck.Name = "tsBtnProgCheck"
        Me.tsBtnProgCheck.Size = New System.Drawing.Size(122, 22)
        Me.tsBtnProgCheck.Text = "Programm prüfen"
        '
        'tsBtnEStop
        '
        Me.tsBtnEStop.Image = CType(resources.GetObject("tsBtnEStop.Image"), System.Drawing.Image)
        Me.tsBtnEStop.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtnEStop.Name = "tsBtnEStop"
        Me.tsBtnEStop.Size = New System.Drawing.Size(132, 22)
        Me.tsBtnEStop.Text = "Not-Halt (Leertaste)"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(6, 25)
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(1113, 669)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.StatusStrip)
        Me.Controls.Add(Me.MenuStrip)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.MenuStrip
        Me.MinimumSize = New System.Drawing.Size(300, 300)
        Me.Name = "frmMain"
        Me.Text = "Aup17 Robo v1.0"
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
    Friend WithEvents msShowTeachbox As ToolStripMenuItem
    Friend WithEvents msShowLog As ToolStripMenuItem
    Friend WithEvents msShowComLogSerial As ToolStripMenuItem
    Friend WithEvents msShowComLogTCPIP As ToolStripMenuItem
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
End Class
