﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class panSettings
    Inherits WeifenLuo.WinFormsUI.Docking.DockContent

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(panSettings))
        Me.propGridRoboPar = New System.Windows.Forms.PropertyGrid()
        Me.ToolStripContainer = New System.Windows.Forms.ToolStripContainer()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lblFilename = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnLoad = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.btnDefaultConfig = New System.Windows.Forms.ToolStripButton()
        Me.ToolStrip2 = New System.Windows.Forms.ToolStrip()
        Me.btnDenHartPar = New System.Windows.Forms.ToolStripButton()
        Me.btnRoboPar = New System.Windows.Forms.ToolStripButton()
        Me.btnFrames = New System.Windows.Forms.ToolStripButton()
        Me.btnTCPSettings = New System.Windows.Forms.ToolStripButton()
        Me.ToolStrip3 = New System.Windows.Forms.ToolStrip()
        Me.btnJ1 = New System.Windows.Forms.ToolStripButton()
        Me.sepJ2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnJ2 = New System.Windows.Forms.ToolStripButton()
        Me.sepJ3 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnJ3 = New System.Windows.Forms.ToolStripButton()
        Me.sepJ4 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnJ4 = New System.Windows.Forms.ToolStripButton()
        Me.sepJ5 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnJ5 = New System.Windows.Forms.ToolStripButton()
        Me.sepJ6 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnJ6 = New System.Windows.Forms.ToolStripButton()
        Me.sepServ1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnServo1 = New System.Windows.Forms.ToolStripButton()
        Me.sepServ2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnServo2 = New System.Windows.Forms.ToolStripButton()
        Me.sepServ3 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnServo3 = New System.Windows.Forms.ToolStripButton()
        Me.btnToolframe = New System.Windows.Forms.ToolStripButton()
        Me.sepFrames = New System.Windows.Forms.ToolStripSeparator()
        Me.btnWorkframe = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripContainer.BottomToolStripPanel.SuspendLayout()
        Me.ToolStripContainer.ContentPanel.SuspendLayout()
        Me.ToolStripContainer.TopToolStripPanel.SuspendLayout()
        Me.ToolStripContainer.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.ToolStrip2.SuspendLayout()
        Me.ToolStrip3.SuspendLayout()
        Me.SuspendLayout()
        '
        'propGridRoboPar
        '
        Me.propGridRoboPar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.propGridRoboPar.Location = New System.Drawing.Point(0, 0)
        Me.propGridRoboPar.Name = "propGridRoboPar"
        Me.propGridRoboPar.PropertySort = System.Windows.Forms.PropertySort.Categorized
        Me.propGridRoboPar.Size = New System.Drawing.Size(613, 345)
        Me.propGridRoboPar.TabIndex = 0
        '
        'ToolStripContainer
        '
        '
        'ToolStripContainer.BottomToolStripPanel
        '
        Me.ToolStripContainer.BottomToolStripPanel.Controls.Add(Me.StatusStrip1)
        '
        'ToolStripContainer.ContentPanel
        '
        Me.ToolStripContainer.ContentPanel.Controls.Add(Me.propGridRoboPar)
        Me.ToolStripContainer.ContentPanel.Size = New System.Drawing.Size(613, 345)
        Me.ToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ToolStripContainer.Location = New System.Drawing.Point(0, 0)
        Me.ToolStripContainer.Name = "ToolStripContainer"
        Me.ToolStripContainer.Size = New System.Drawing.Size(613, 442)
        Me.ToolStripContainer.TabIndex = 1
        Me.ToolStripContainer.Text = "ToolStripContainer1"
        '
        'ToolStripContainer.TopToolStripPanel
        '
        Me.ToolStripContainer.TopToolStripPanel.Controls.Add(Me.ToolStrip1)
        Me.ToolStripContainer.TopToolStripPanel.Controls.Add(Me.ToolStrip2)
        Me.ToolStripContainer.TopToolStripPanel.Controls.Add(Me.ToolStrip3)
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Dock = System.Windows.Forms.DockStyle.None
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblFilename})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 0)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(613, 22)
        Me.StatusStrip1.TabIndex = 0
        '
        'lblFilename
        '
        Me.lblFilename.Name = "lblFilename"
        Me.lblFilename.Size = New System.Drawing.Size(147, 17)
        Me.lblFilename.Text = "geöffnete Parameterdatei: "
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnLoad, Me.btnSave, Me.btnDefaultConfig})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(613, 25)
        Me.ToolStrip1.Stretch = True
        Me.ToolStrip1.TabIndex = 1
        '
        'btnLoad
        '
        Me.btnLoad.Image = CType(resources.GetObject("btnLoad.Image"), System.Drawing.Image)
        Me.btnLoad.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(59, 22)
        Me.btnLoad.Text = "Laden"
        '
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(79, 22)
        Me.btnSave.Text = "Speichern"
        '
        'btnDefaultConfig
        '
        Me.btnDefaultConfig.Image = CType(resources.GetObject("btnDefaultConfig.Image"), System.Drawing.Image)
        Me.btnDefaultConfig.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDefaultConfig.Name = "btnDefaultConfig"
        Me.btnDefaultConfig.Size = New System.Drawing.Size(74, 22)
        Me.btnDefaultConfig.Text = "Standard"
        '
        'ToolStrip2
        '
        Me.ToolStrip2.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnDenHartPar, Me.btnRoboPar, Me.btnFrames, Me.btnTCPSettings})
        Me.ToolStrip2.Location = New System.Drawing.Point(0, 25)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.Size = New System.Drawing.Size(613, 25)
        Me.ToolStrip2.Stretch = True
        Me.ToolStrip2.TabIndex = 2
        '
        'btnDenHartPar
        '
        Me.btnDenHartPar.Image = CType(resources.GetObject("btnDenHartPar.Image"), System.Drawing.Image)
        Me.btnDenHartPar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDenHartPar.Name = "btnDenHartPar"
        Me.btnDenHartPar.Size = New System.Drawing.Size(132, 22)
        Me.btnDenHartPar.Text = "Denavit-Hartenberg"
        '
        'btnRoboPar
        '
        Me.btnRoboPar.Image = CType(resources.GetObject("btnRoboPar.Image"), System.Drawing.Image)
        Me.btnRoboPar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRoboPar.Name = "btnRoboPar"
        Me.btnRoboPar.Size = New System.Drawing.Size(52, 22)
        Me.btnRoboPar.Text = "Joint"
        '
        'btnFrames
        '
        Me.btnFrames.Image = CType(resources.GetObject("btnFrames.Image"), System.Drawing.Image)
        Me.btnFrames.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnFrames.Name = "btnFrames"
        Me.btnFrames.Size = New System.Drawing.Size(65, 22)
        Me.btnFrames.Text = "Frames"
        '
        'btnTCPSettings
        '
        Me.btnTCPSettings.Image = CType(resources.GetObject("btnTCPSettings.Image"), System.Drawing.Image)
        Me.btnTCPSettings.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnTCPSettings.Name = "btnTCPSettings"
        Me.btnTCPSettings.Size = New System.Drawing.Size(114, 22)
        Me.btnTCPSettings.Text = "TCP-Verbindung"
        '
        'ToolStrip3
        '
        Me.ToolStrip3.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip3.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip3.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnJ1, Me.sepJ2, Me.btnJ2, Me.sepJ3, Me.btnJ3, Me.sepJ4, Me.btnJ4, Me.sepJ5, Me.btnJ5, Me.sepJ6, Me.btnJ6, Me.sepServ1, Me.btnServo1, Me.sepServ2, Me.btnServo2, Me.sepServ3, Me.btnServo3, Me.btnToolframe, Me.sepFrames, Me.btnWorkframe})
        Me.ToolStrip3.Location = New System.Drawing.Point(0, 50)
        Me.ToolStrip3.Name = "ToolStrip3"
        Me.ToolStrip3.Size = New System.Drawing.Size(613, 25)
        Me.ToolStrip3.Stretch = True
        Me.ToolStrip3.TabIndex = 0
        '
        'btnJ1
        '
        Me.btnJ1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnJ1.Image = CType(resources.GetObject("btnJ1.Image"), System.Drawing.Image)
        Me.btnJ1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnJ1.Name = "btnJ1"
        Me.btnJ1.Size = New System.Drawing.Size(23, 22)
        Me.btnJ1.Text = "J1"
        '
        'sepJ2
        '
        Me.sepJ2.Name = "sepJ2"
        Me.sepJ2.Size = New System.Drawing.Size(6, 25)
        '
        'btnJ2
        '
        Me.btnJ2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnJ2.Image = CType(resources.GetObject("btnJ2.Image"), System.Drawing.Image)
        Me.btnJ2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnJ2.Name = "btnJ2"
        Me.btnJ2.Size = New System.Drawing.Size(23, 22)
        Me.btnJ2.Text = "J2"
        '
        'sepJ3
        '
        Me.sepJ3.Name = "sepJ3"
        Me.sepJ3.Size = New System.Drawing.Size(6, 25)
        '
        'btnJ3
        '
        Me.btnJ3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnJ3.Image = CType(resources.GetObject("btnJ3.Image"), System.Drawing.Image)
        Me.btnJ3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnJ3.Name = "btnJ3"
        Me.btnJ3.Size = New System.Drawing.Size(23, 22)
        Me.btnJ3.Text = "J3"
        '
        'sepJ4
        '
        Me.sepJ4.Name = "sepJ4"
        Me.sepJ4.Size = New System.Drawing.Size(6, 25)
        '
        'btnJ4
        '
        Me.btnJ4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnJ4.Image = CType(resources.GetObject("btnJ4.Image"), System.Drawing.Image)
        Me.btnJ4.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnJ4.Name = "btnJ4"
        Me.btnJ4.Size = New System.Drawing.Size(23, 22)
        Me.btnJ4.Text = "J4"
        '
        'sepJ5
        '
        Me.sepJ5.Name = "sepJ5"
        Me.sepJ5.Size = New System.Drawing.Size(6, 25)
        '
        'btnJ5
        '
        Me.btnJ5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnJ5.Image = CType(resources.GetObject("btnJ5.Image"), System.Drawing.Image)
        Me.btnJ5.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnJ5.Name = "btnJ5"
        Me.btnJ5.Size = New System.Drawing.Size(23, 22)
        Me.btnJ5.Text = "J5"
        '
        'sepJ6
        '
        Me.sepJ6.Name = "sepJ6"
        Me.sepJ6.Size = New System.Drawing.Size(6, 25)
        '
        'btnJ6
        '
        Me.btnJ6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnJ6.Image = CType(resources.GetObject("btnJ6.Image"), System.Drawing.Image)
        Me.btnJ6.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnJ6.Name = "btnJ6"
        Me.btnJ6.Size = New System.Drawing.Size(23, 22)
        Me.btnJ6.Text = "J6"
        '
        'sepServ1
        '
        Me.sepServ1.Name = "sepServ1"
        Me.sepServ1.Size = New System.Drawing.Size(6, 25)
        '
        'btnServo1
        '
        Me.btnServo1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnServo1.Image = CType(resources.GetObject("btnServo1.Image"), System.Drawing.Image)
        Me.btnServo1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnServo1.Name = "btnServo1"
        Me.btnServo1.Size = New System.Drawing.Size(39, 22)
        Me.btnServo1.Text = "Serv1"
        '
        'sepServ2
        '
        Me.sepServ2.Name = "sepServ2"
        Me.sepServ2.Size = New System.Drawing.Size(6, 25)
        '
        'btnServo2
        '
        Me.btnServo2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnServo2.Image = CType(resources.GetObject("btnServo2.Image"), System.Drawing.Image)
        Me.btnServo2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnServo2.Name = "btnServo2"
        Me.btnServo2.Size = New System.Drawing.Size(42, 22)
        Me.btnServo2.Text = "Serv 2"
        '
        'sepServ3
        '
        Me.sepServ3.Name = "sepServ3"
        Me.sepServ3.Size = New System.Drawing.Size(6, 25)
        '
        'btnServo3
        '
        Me.btnServo3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnServo3.Image = CType(resources.GetObject("btnServo3.Image"), System.Drawing.Image)
        Me.btnServo3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnServo3.Name = "btnServo3"
        Me.btnServo3.Size = New System.Drawing.Size(42, 22)
        Me.btnServo3.Text = "Serv 3"
        '
        'btnToolframe
        '
        Me.btnToolframe.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnToolframe.Image = CType(resources.GetObject("btnToolframe.Image"), System.Drawing.Image)
        Me.btnToolframe.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnToolframe.Name = "btnToolframe"
        Me.btnToolframe.Size = New System.Drawing.Size(65, 22)
        Me.btnToolframe.Text = "Toolframe"
        '
        'sepFrames
        '
        Me.sepFrames.Name = "sepFrames"
        Me.sepFrames.Size = New System.Drawing.Size(6, 25)
        '
        'btnWorkframe
        '
        Me.btnWorkframe.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnWorkframe.Image = CType(resources.GetObject("btnWorkframe.Image"), System.Drawing.Image)
        Me.btnWorkframe.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnWorkframe.Name = "btnWorkframe"
        Me.btnWorkframe.Size = New System.Drawing.Size(70, 22)
        Me.btnWorkframe.Text = "Workframe"
        '
        'panSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(613, 442)
        Me.Controls.Add(Me.ToolStripContainer)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "panSettings"
        Me.Text = "Settings"
        Me.ToolStripContainer.BottomToolStripPanel.ResumeLayout(False)
        Me.ToolStripContainer.BottomToolStripPanel.PerformLayout()
        Me.ToolStripContainer.ContentPanel.ResumeLayout(False)
        Me.ToolStripContainer.TopToolStripPanel.ResumeLayout(False)
        Me.ToolStripContainer.TopToolStripPanel.PerformLayout()
        Me.ToolStripContainer.ResumeLayout(False)
        Me.ToolStripContainer.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ToolStrip2.ResumeLayout(False)
        Me.ToolStrip2.PerformLayout()
        Me.ToolStrip3.ResumeLayout(False)
        Me.ToolStrip3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents propGridRoboPar As PropertyGrid
    Friend WithEvents ToolStripContainer As ToolStripContainer
    Friend WithEvents ToolStrip3 As ToolStrip
    Friend WithEvents btnJ1 As ToolStripButton
    Friend WithEvents sepJ2 As ToolStripSeparator
    Friend WithEvents btnJ2 As ToolStripButton
    Friend WithEvents btnJ3 As ToolStripButton
    Friend WithEvents btnJ4 As ToolStripButton
    Friend WithEvents btnJ5 As ToolStripButton
    Friend WithEvents btnJ6 As ToolStripButton
    Friend WithEvents btnServo1 As ToolStripButton
    Friend WithEvents btnServo2 As ToolStripButton
    Friend WithEvents btnServo3 As ToolStripButton
    Friend WithEvents sepJ3 As ToolStripSeparator
    Friend WithEvents sepJ4 As ToolStripSeparator
    Friend WithEvents sepJ5 As ToolStripSeparator
    Friend WithEvents sepJ6 As ToolStripSeparator
    Friend WithEvents sepServ1 As ToolStripSeparator
    Friend WithEvents sepServ2 As ToolStripSeparator
    Friend WithEvents sepServ3 As ToolStripSeparator
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents lblFilename As ToolStripStatusLabel
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents btnLoad As ToolStripButton
    Friend WithEvents btnSave As ToolStripButton
    Friend WithEvents btnDefaultConfig As ToolStripButton
    Friend WithEvents ToolStrip2 As ToolStrip
    Friend WithEvents btnDenHartPar As ToolStripButton
    Friend WithEvents btnRoboPar As ToolStripButton
    Friend WithEvents btnFrames As ToolStripButton
    Friend WithEvents btnTCPSettings As ToolStripButton
    Friend WithEvents btnToolframe As ToolStripButton
    Friend WithEvents sepFrames As ToolStripSeparator
    Friend WithEvents btnWorkframe As ToolStripButton
End Class
