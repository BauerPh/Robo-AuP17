
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class panTeachPoints
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(panTeachPoints))
        Me.ToolStripContainer = New System.Windows.Forms.ToolStripContainer()
        Me.lbTeachPoints = New System.Windows.Forms.ListBox()
        Me.ToolStrip2 = New System.Windows.Forms.ToolStrip()
        Me.lblSpeed = New System.Windows.Forms.ToolStripLabel()
        Me.numSpeed = New RoboAUP17.ToolStripNumericUpDown()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.lblAcc = New System.Windows.Forms.ToolStripLabel()
        Me.numAcc = New RoboAUP17.ToolStripNumericUpDown()
        Me.btnMoveTo = New System.Windows.Forms.ToolStripButton()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.lblName = New System.Windows.Forms.ToolStripLabel()
        Me.tbName = New System.Windows.Forms.ToolStripTextBox()
        Me.numNr = New RoboAUP17.ToolStripNumericUpDown()
        Me.cbTPMode = New System.Windows.Forms.ToolStripComboBox()
        Me.btnAdd = New System.Windows.Forms.ToolStripButton()
        Me.btnRename = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnUp = New System.Windows.Forms.ToolStripButton()
        Me.btnDown = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripContainer.ContentPanel.SuspendLayout()
        Me.ToolStripContainer.TopToolStripPanel.SuspendLayout()
        Me.ToolStripContainer.SuspendLayout()
        Me.ToolStrip2.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStripContainer
        '
        '
        'ToolStripContainer.ContentPanel
        '
        Me.ToolStripContainer.ContentPanel.Controls.Add(Me.lbTeachPoints)
        Me.ToolStripContainer.ContentPanel.Size = New System.Drawing.Size(402, 362)
        Me.ToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ToolStripContainer.Location = New System.Drawing.Point(0, 0)
        Me.ToolStripContainer.Name = "ToolStripContainer"
        Me.ToolStripContainer.Size = New System.Drawing.Size(402, 414)
        Me.ToolStripContainer.TabIndex = 0
        Me.ToolStripContainer.Text = "ToolStripContainer"
        '
        'ToolStripContainer.TopToolStripPanel
        '
        Me.ToolStripContainer.TopToolStripPanel.Controls.Add(Me.ToolStrip2)
        Me.ToolStripContainer.TopToolStripPanel.Controls.Add(Me.ToolStrip1)
        '
        'lbTeachPoints
        '
        Me.lbTeachPoints.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lbTeachPoints.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbTeachPoints.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTeachPoints.FormattingEnabled = True
        Me.lbTeachPoints.ItemHeight = 16
        Me.lbTeachPoints.Location = New System.Drawing.Point(0, 0)
        Me.lbTeachPoints.Name = "lbTeachPoints"
        Me.lbTeachPoints.Size = New System.Drawing.Size(402, 362)
        Me.lbTeachPoints.TabIndex = 0
        '
        'ToolStrip2
        '
        Me.ToolStrip2.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblSpeed, Me.numSpeed, Me.ToolStripSeparator3, Me.lblAcc, Me.numAcc, Me.btnMoveTo})
        Me.ToolStrip2.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.Size = New System.Drawing.Size(402, 26)
        Me.ToolStrip2.Stretch = True
        Me.ToolStrip2.TabIndex = 1
        '
        'lblSpeed
        '
        Me.lblSpeed.Name = "lblSpeed"
        Me.lblSpeed.Size = New System.Drawing.Size(80, 23)
        Me.lblSpeed.Text = "Geschw. (°/s):"
        '
        'numSpeed
        '
        Me.numSpeed.DecimalPlaces = 2
        Me.numSpeed.Maximum = New Decimal(New Integer() {360, 0, 0, 0})
        Me.numSpeed.Minimum = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numSpeed.Name = "numSpeed"
        Me.numSpeed.Size = New System.Drawing.Size(56, 23)
        Me.numSpeed.Text = "50,00"
        Me.numSpeed.Value = New Decimal(New Integer() {5000, 0, 0, 131072})
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 26)
        '
        'lblAcc
        '
        Me.lblAcc.Name = "lblAcc"
        Me.lblAcc.Size = New System.Drawing.Size(77, 23)
        Me.lblAcc.Text = "Beschl. (°/s²):"
        '
        'numAcc
        '
        Me.numAcc.DecimalPlaces = 2
        Me.numAcc.Maximum = New Decimal(New Integer() {360, 0, 0, 0})
        Me.numAcc.Minimum = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numAcc.Name = "numAcc"
        Me.numAcc.Size = New System.Drawing.Size(56, 23)
        Me.numAcc.Text = "50,00"
        Me.numAcc.Value = New Decimal(New Integer() {5000, 0, 0, 131072})
        '
        'btnMoveTo
        '
        Me.btnMoveTo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnMoveTo.Image = CType(resources.GetObject("btnMoveTo.Image"), System.Drawing.Image)
        Me.btnMoveTo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnMoveTo.Name = "btnMoveTo"
        Me.btnMoveTo.Size = New System.Drawing.Size(23, 23)
        Me.btnMoveTo.Text = "Teachpunkt anfahren"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblName, Me.tbName, Me.numNr, Me.cbTPMode, Me.btnAdd, Me.btnRename, Me.ToolStripSeparator2, Me.btnDelete, Me.ToolStripSeparator1, Me.btnUp, Me.btnDown})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 26)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(402, 26)
        Me.ToolStrip1.Stretch = True
        Me.ToolStrip1.TabIndex = 0
        '
        'lblName
        '
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(42, 23)
        Me.lblName.Text = "Name:"
        '
        'tbName
        '
        Me.tbName.Name = "tbName"
        Me.tbName.Size = New System.Drawing.Size(100, 26)
        '
        'numNr
        '
        Me.numNr.DecimalPlaces = 0
        Me.numNr.Maximum = New Decimal(New Integer() {100, 0, 0, 0})
        Me.numNr.Minimum = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numNr.Name = "numNr"
        Me.numNr.Size = New System.Drawing.Size(41, 23)
        Me.numNr.Text = "0"
        Me.numNr.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'cbTPMode
        '
        Me.cbTPMode.AutoSize = False
        Me.cbTPMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTPMode.DropDownWidth = 60
        Me.cbTPMode.Items.AddRange(New Object() {"Cart", "Joint"})
        Me.cbTPMode.Name = "cbTPMode"
        Me.cbTPMode.Size = New System.Drawing.Size(60, 23)
        '
        'btnAdd
        '
        Me.btnAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnAdd.Image = CType(resources.GetObject("btnAdd.Image"), System.Drawing.Image)
        Me.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(23, 23)
        Me.btnAdd.Text = "Hinzufügen"
        '
        'btnRename
        '
        Me.btnRename.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnRename.Image = CType(resources.GetObject("btnRename.Image"), System.Drawing.Image)
        Me.btnRename.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRename.Name = "btnRename"
        Me.btnRename.Size = New System.Drawing.Size(23, 23)
        Me.btnRename.Text = "Umbenennen"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 26)
        '
        'btnDelete
        '
        Me.btnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(23, 23)
        Me.btnDelete.Text = "Löschen"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 26)
        '
        'btnUp
        '
        Me.btnUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnUp.Image = CType(resources.GetObject("btnUp.Image"), System.Drawing.Image)
        Me.btnUp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(23, 23)
        Me.btnUp.Text = "Hoch"
        '
        'btnDown
        '
        Me.btnDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnDown.Image = CType(resources.GetObject("btnDown.Image"), System.Drawing.Image)
        Me.btnDown.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(23, 23)
        Me.btnDown.Text = "Runter"
        '
        'panTeachPoints
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(402, 414)
        Me.Controls.Add(Me.ToolStripContainer)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "panTeachPoints"
        Me.Text = "Teachpunkte"
        Me.ToolStripContainer.ContentPanel.ResumeLayout(False)
        Me.ToolStripContainer.TopToolStripPanel.ResumeLayout(False)
        Me.ToolStripContainer.TopToolStripPanel.PerformLayout()
        Me.ToolStripContainer.ResumeLayout(False)
        Me.ToolStripContainer.PerformLayout()
        Me.ToolStrip2.ResumeLayout(False)
        Me.ToolStrip2.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ToolStripContainer As ToolStripContainer
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents btnAdd As ToolStripButton
    Friend WithEvents btnDelete As ToolStripButton
    Friend WithEvents tbName As ToolStripTextBox
    Friend WithEvents lbTeachPoints As ListBox
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents btnUp As ToolStripButton
    Friend WithEvents btnDown As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents numNr As ToolStripNumericUpDown
    Friend WithEvents ToolStrip2 As ToolStrip
    Friend WithEvents lblSpeed As ToolStripLabel
    Friend WithEvents numSpeed As ToolStripNumericUpDown
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents lblAcc As ToolStripLabel
    Friend WithEvents numAcc As ToolStripNumericUpDown
    Friend WithEvents btnMoveTo As ToolStripButton
    Friend WithEvents lblName As ToolStripLabel
    Friend WithEvents cbTPMode As ToolStripComboBox
    Friend WithEvents btnRename As ToolStripButton
End Class
