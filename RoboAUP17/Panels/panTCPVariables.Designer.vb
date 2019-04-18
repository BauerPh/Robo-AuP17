<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class panTCPVariables
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(panTCPVariables))
        Me.dataGridView = New System.Windows.Forms.DataGridView()
        Me.ToolStripContainer = New System.Windows.Forms.ToolStripContainer()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.lblName = New System.Windows.Forms.ToolStripLabel()
        Me.tbName = New System.Windows.Forms.ToolStripTextBox()
        Me.btnAdd = New System.Windows.Forms.ToolStripButton()
        Me.btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.lblConnectStatusTitle = New System.Windows.Forms.ToolStripLabel()
        Me.lblConnectStatus = New System.Windows.Forms.ToolStripLabel()
        CType(Me.dataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStripContainer.ContentPanel.SuspendLayout()
        Me.ToolStripContainer.TopToolStripPanel.SuspendLayout()
        Me.ToolStripContainer.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'dataGridView
        '
        Me.dataGridView.AllowUserToAddRows = False
        Me.dataGridView.AllowUserToDeleteRows = False
        Me.dataGridView.AllowUserToResizeRows = False
        Me.dataGridView.BackgroundColor = System.Drawing.SystemColors.ControlLightLight
        Me.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dataGridView.Location = New System.Drawing.Point(0, 0)
        Me.dataGridView.Name = "dataGridView"
        Me.dataGridView.ReadOnly = True
        Me.dataGridView.Size = New System.Drawing.Size(384, 336)
        Me.dataGridView.TabIndex = 0
        '
        'ToolStripContainer
        '
        '
        'ToolStripContainer.ContentPanel
        '
        Me.ToolStripContainer.ContentPanel.Controls.Add(Me.dataGridView)
        Me.ToolStripContainer.ContentPanel.Size = New System.Drawing.Size(384, 336)
        Me.ToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ToolStripContainer.Location = New System.Drawing.Point(0, 0)
        Me.ToolStripContainer.Name = "ToolStripContainer"
        Me.ToolStripContainer.Size = New System.Drawing.Size(384, 361)
        Me.ToolStripContainer.TabIndex = 1
        Me.ToolStripContainer.Text = "ToolStripContainer1"
        '
        'ToolStripContainer.TopToolStripPanel
        '
        Me.ToolStripContainer.TopToolStripPanel.Controls.Add(Me.ToolStrip1)
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblName, Me.tbName, Me.btnAdd, Me.btnDelete, Me.ToolStripSeparator1, Me.lblConnectStatusTitle, Me.lblConnectStatus})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(384, 25)
        Me.ToolStrip1.Stretch = True
        Me.ToolStrip1.TabIndex = 0
        '
        'lblName
        '
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(42, 22)
        Me.lblName.Text = "Name:"
        '
        'tbName
        '
        Me.tbName.Name = "tbName"
        Me.tbName.Size = New System.Drawing.Size(100, 25)
        '
        'btnAdd
        '
        Me.btnAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnAdd.Image = CType(resources.GetObject("btnAdd.Image"), System.Drawing.Image)
        Me.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(23, 22)
        Me.btnAdd.Text = "Hinzufügen"
        '
        'btnDelete
        '
        Me.btnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(23, 22)
        Me.btnDelete.Text = "Löschen"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'lblConnectStatusTitle
        '
        Me.lblConnectStatusTitle.Name = "lblConnectStatusTitle"
        Me.lblConnectStatusTitle.Size = New System.Drawing.Size(67, 22)
        Me.lblConnectStatusTitle.Text = "TCP-Client:"
        '
        'lblConnectStatus
        '
        Me.lblConnectStatus.ForeColor = System.Drawing.Color.Red
        Me.lblConnectStatus.Name = "lblConnectStatus"
        Me.lblConnectStatus.Size = New System.Drawing.Size(94, 22)
        Me.lblConnectStatus.Text = "nicht verbunden"
        '
        'panTCPVariables
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(384, 361)
        Me.Controls.Add(Me.ToolStripContainer)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "panTCPVariables"
        Me.Text = "TCP-Variablen"
        CType(Me.dataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStripContainer.ContentPanel.ResumeLayout(False)
        Me.ToolStripContainer.TopToolStripPanel.ResumeLayout(False)
        Me.ToolStripContainer.TopToolStripPanel.PerformLayout()
        Me.ToolStripContainer.ResumeLayout(False)
        Me.ToolStripContainer.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents dataGridView As DataGridView
    Friend WithEvents ToolStripContainer As ToolStripContainer
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents lblName As ToolStripLabel
    Friend WithEvents tbName As ToolStripTextBox
    Friend WithEvents btnAdd As ToolStripButton
    Friend WithEvents btnDelete As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents lblConnectStatus As ToolStripLabel
    Friend WithEvents lblConnectStatusTitle As ToolStripLabel
End Class
