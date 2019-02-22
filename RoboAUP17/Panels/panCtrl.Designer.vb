<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class panCtrl
    Inherits WeifenLuo.WinFormsUI.Docking.DockContent

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(panCtrl))
        Me.ToolStripContainer = New System.Windows.Forms.ToolStripContainer()
        Me.TableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.btnCtrlJ1Inc = New System.Windows.Forms.Button()
        Me.lblCtrlJ1 = New System.Windows.Forms.Label()
        Me.btnCtrlJ1Dec = New System.Windows.Forms.Button()
        Me.tbCtrlJ1 = New System.Windows.Forms.TrackBar()
        Me.numCtrlJ1Target = New System.Windows.Forms.NumericUpDown()
        Me.btnCtrlJ2Inc = New System.Windows.Forms.Button()
        Me.btnCtrlJ2Dec = New System.Windows.Forms.Button()
        Me.tbCtrlJ2 = New System.Windows.Forms.TrackBar()
        Me.numCtrlJ2Target = New System.Windows.Forms.NumericUpDown()
        Me.lblCtrlJ2 = New System.Windows.Forms.Label()
        Me.lblCtrlJ3 = New System.Windows.Forms.Label()
        Me.lblCtrlJ4 = New System.Windows.Forms.Label()
        Me.lblCtrlJ5 = New System.Windows.Forms.Label()
        Me.lblCtrlJ6 = New System.Windows.Forms.Label()
        Me.numCtrlJ3Target = New System.Windows.Forms.NumericUpDown()
        Me.numCtrlJ4Target = New System.Windows.Forms.NumericUpDown()
        Me.numCtrlJ5Target = New System.Windows.Forms.NumericUpDown()
        Me.numCtrlJ6Target = New System.Windows.Forms.NumericUpDown()
        Me.tbCtrlJ3 = New System.Windows.Forms.TrackBar()
        Me.tbCtrlJ4 = New System.Windows.Forms.TrackBar()
        Me.tbCtrlJ5 = New System.Windows.Forms.TrackBar()
        Me.tbCtrlJ6 = New System.Windows.Forms.TrackBar()
        Me.btnCtrlJ3Dec = New System.Windows.Forms.Button()
        Me.btnCtrlJ4Dec = New System.Windows.Forms.Button()
        Me.btnCtrlJ5Dec = New System.Windows.Forms.Button()
        Me.btnCtrlJ6Dec = New System.Windows.Forms.Button()
        Me.btnCtrlJ3Inc = New System.Windows.Forms.Button()
        Me.btnCtrlJ4Inc = New System.Windows.Forms.Button()
        Me.btnCtrlJ5Inc = New System.Windows.Forms.Button()
        Me.btnCtrlJ6Inc = New System.Windows.Forms.Button()
        Me.lblTargetAngle = New System.Windows.Forms.Label()
        Me.lblJog = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.lblMove = New System.Windows.Forms.ToolStripLabel()
        Me.cbMoveMode = New System.Windows.Forms.ToolStripComboBox()
        Me.btnMoveStart = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.lblJogInterval = New System.Windows.Forms.ToolStripLabel()
        Me.numJogInterval = New RoboAUP17.ToolStripNumericUpDown()
        Me.cbJogMode = New System.Windows.Forms.ToolStripComboBox()
        Me.ToolStrip2 = New System.Windows.Forms.ToolStrip()
        Me.lblSpeed = New System.Windows.Forms.ToolStripLabel()
        Me.numSpeed = New RoboAUP17.ToolStripNumericUpDown()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.lblAcc = New System.Windows.Forms.ToolStripLabel()
        Me.numAcc = New RoboAUP17.ToolStripNumericUpDown()
        Me.ToolStripContainer.ContentPanel.SuspendLayout()
        Me.ToolStripContainer.TopToolStripPanel.SuspendLayout()
        Me.ToolStripContainer.SuspendLayout()
        Me.TableLayoutPanel.SuspendLayout()
        CType(Me.tbCtrlJ1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numCtrlJ1Target, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbCtrlJ2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numCtrlJ2Target, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numCtrlJ3Target, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numCtrlJ4Target, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numCtrlJ5Target, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numCtrlJ6Target, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbCtrlJ3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbCtrlJ4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbCtrlJ5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbCtrlJ6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.ToolStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStripContainer
        '
        Me.ToolStripContainer.BottomToolStripPanelVisible = False
        '
        'ToolStripContainer.ContentPanel
        '
        Me.ToolStripContainer.ContentPanel.Controls.Add(Me.TableLayoutPanel)
        Me.ToolStripContainer.ContentPanel.Size = New System.Drawing.Size(511, 258)
        Me.ToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ToolStripContainer.LeftToolStripPanelVisible = False
        Me.ToolStripContainer.Location = New System.Drawing.Point(0, 0)
        Me.ToolStripContainer.Name = "ToolStripContainer"
        Me.ToolStripContainer.RightToolStripPanelVisible = False
        Me.ToolStripContainer.Size = New System.Drawing.Size(511, 310)
        Me.ToolStripContainer.TabIndex = 0
        Me.ToolStripContainer.Text = "ToolStripContainer"
        '
        'ToolStripContainer.TopToolStripPanel
        '
        Me.ToolStripContainer.TopToolStripPanel.Controls.Add(Me.ToolStrip2)
        Me.ToolStripContainer.TopToolStripPanel.Controls.Add(Me.ToolStrip1)
        '
        'TableLayoutPanel
        '
        Me.TableLayoutPanel.AutoScroll = True
        Me.TableLayoutPanel.AutoScrollMinSize = New System.Drawing.Size(300, 0)
        Me.TableLayoutPanel.ColumnCount = 5
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65.0!))
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel.Controls.Add(Me.btnCtrlJ1Inc, 4, 1)
        Me.TableLayoutPanel.Controls.Add(Me.lblCtrlJ1, 0, 1)
        Me.TableLayoutPanel.Controls.Add(Me.btnCtrlJ1Dec, 3, 1)
        Me.TableLayoutPanel.Controls.Add(Me.tbCtrlJ1, 2, 1)
        Me.TableLayoutPanel.Controls.Add(Me.numCtrlJ1Target, 1, 1)
        Me.TableLayoutPanel.Controls.Add(Me.btnCtrlJ2Inc, 4, 2)
        Me.TableLayoutPanel.Controls.Add(Me.btnCtrlJ2Dec, 3, 2)
        Me.TableLayoutPanel.Controls.Add(Me.tbCtrlJ2, 2, 2)
        Me.TableLayoutPanel.Controls.Add(Me.numCtrlJ2Target, 1, 2)
        Me.TableLayoutPanel.Controls.Add(Me.lblCtrlJ2, 0, 2)
        Me.TableLayoutPanel.Controls.Add(Me.lblCtrlJ3, 0, 3)
        Me.TableLayoutPanel.Controls.Add(Me.lblCtrlJ4, 0, 4)
        Me.TableLayoutPanel.Controls.Add(Me.lblCtrlJ5, 0, 5)
        Me.TableLayoutPanel.Controls.Add(Me.lblCtrlJ6, 0, 6)
        Me.TableLayoutPanel.Controls.Add(Me.numCtrlJ3Target, 1, 3)
        Me.TableLayoutPanel.Controls.Add(Me.numCtrlJ4Target, 1, 4)
        Me.TableLayoutPanel.Controls.Add(Me.numCtrlJ5Target, 1, 5)
        Me.TableLayoutPanel.Controls.Add(Me.numCtrlJ6Target, 1, 6)
        Me.TableLayoutPanel.Controls.Add(Me.tbCtrlJ3, 2, 3)
        Me.TableLayoutPanel.Controls.Add(Me.tbCtrlJ4, 2, 4)
        Me.TableLayoutPanel.Controls.Add(Me.tbCtrlJ5, 2, 5)
        Me.TableLayoutPanel.Controls.Add(Me.tbCtrlJ6, 2, 6)
        Me.TableLayoutPanel.Controls.Add(Me.btnCtrlJ3Dec, 3, 3)
        Me.TableLayoutPanel.Controls.Add(Me.btnCtrlJ4Dec, 3, 4)
        Me.TableLayoutPanel.Controls.Add(Me.btnCtrlJ5Dec, 3, 5)
        Me.TableLayoutPanel.Controls.Add(Me.btnCtrlJ6Dec, 3, 6)
        Me.TableLayoutPanel.Controls.Add(Me.btnCtrlJ3Inc, 4, 3)
        Me.TableLayoutPanel.Controls.Add(Me.btnCtrlJ4Inc, 4, 4)
        Me.TableLayoutPanel.Controls.Add(Me.btnCtrlJ5Inc, 4, 5)
        Me.TableLayoutPanel.Controls.Add(Me.btnCtrlJ6Inc, 4, 6)
        Me.TableLayoutPanel.Controls.Add(Me.lblTargetAngle, 1, 0)
        Me.TableLayoutPanel.Controls.Add(Me.lblJog, 3, 0)
        Me.TableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel.Name = "TableLayoutPanel"
        Me.TableLayoutPanel.RowCount = 8
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel.Size = New System.Drawing.Size(511, 258)
        Me.TableLayoutPanel.TabIndex = 0
        '
        'btnCtrlJ1Inc
        '
        Me.btnCtrlJ1Inc.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCtrlJ1Inc.Location = New System.Drawing.Point(456, 27)
        Me.btnCtrlJ1Inc.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCtrlJ1Inc.Name = "btnCtrlJ1Inc"
        Me.btnCtrlJ1Inc.Size = New System.Drawing.Size(53, 26)
        Me.btnCtrlJ1Inc.TabIndex = 4
        Me.btnCtrlJ1Inc.Text = "+"
        Me.btnCtrlJ1Inc.UseVisualStyleBackColor = True
        '
        'lblCtrlJ1
        '
        Me.lblCtrlJ1.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCtrlJ1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCtrlJ1.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblCtrlJ1.Location = New System.Drawing.Point(3, 28)
        Me.lblCtrlJ1.Margin = New System.Windows.Forms.Padding(3)
        Me.lblCtrlJ1.Name = "lblCtrlJ1"
        Me.lblCtrlJ1.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblCtrlJ1.Size = New System.Drawing.Size(59, 24)
        Me.lblCtrlJ1.TabIndex = 1
        Me.lblCtrlJ1.Text = "Joint 1:"
        Me.lblCtrlJ1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnCtrlJ1Dec
        '
        Me.btnCtrlJ1Dec.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCtrlJ1Dec.Location = New System.Drawing.Point(401, 27)
        Me.btnCtrlJ1Dec.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCtrlJ1Dec.Name = "btnCtrlJ1Dec"
        Me.btnCtrlJ1Dec.Size = New System.Drawing.Size(51, 26)
        Me.btnCtrlJ1Dec.TabIndex = 2
        Me.btnCtrlJ1Dec.Text = "-"
        Me.btnCtrlJ1Dec.UseVisualStyleBackColor = True
        '
        'tbCtrlJ1
        '
        Me.tbCtrlJ1.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbCtrlJ1.AutoSize = False
        Me.tbCtrlJ1.LargeChange = 100
        Me.tbCtrlJ1.Location = New System.Drawing.Point(179, 28)
        Me.tbCtrlJ1.Maximum = 36000
        Me.tbCtrlJ1.Minimum = -36000
        Me.tbCtrlJ1.Name = "tbCtrlJ1"
        Me.tbCtrlJ1.Size = New System.Drawing.Size(217, 24)
        Me.tbCtrlJ1.TabIndex = 0
        Me.tbCtrlJ1.TickStyle = System.Windows.Forms.TickStyle.None
        '
        'numCtrlJ1Target
        '
        Me.numCtrlJ1Target.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.numCtrlJ1Target.DecimalPlaces = 2
        Me.numCtrlJ1Target.Location = New System.Drawing.Point(68, 30)
        Me.numCtrlJ1Target.Maximum = New Decimal(New Integer() {360, 0, 0, 0})
        Me.numCtrlJ1Target.Minimum = New Decimal(New Integer() {360, 0, 0, -2147483648})
        Me.numCtrlJ1Target.Name = "numCtrlJ1Target"
        Me.numCtrlJ1Target.Size = New System.Drawing.Size(105, 20)
        Me.numCtrlJ1Target.TabIndex = 3
        '
        'btnCtrlJ2Inc
        '
        Me.btnCtrlJ2Inc.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCtrlJ2Inc.Location = New System.Drawing.Point(456, 57)
        Me.btnCtrlJ2Inc.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCtrlJ2Inc.Name = "btnCtrlJ2Inc"
        Me.btnCtrlJ2Inc.Size = New System.Drawing.Size(53, 26)
        Me.btnCtrlJ2Inc.TabIndex = 9
        Me.btnCtrlJ2Inc.Text = "+"
        Me.btnCtrlJ2Inc.UseVisualStyleBackColor = True
        '
        'btnCtrlJ2Dec
        '
        Me.btnCtrlJ2Dec.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCtrlJ2Dec.Location = New System.Drawing.Point(401, 57)
        Me.btnCtrlJ2Dec.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCtrlJ2Dec.Name = "btnCtrlJ2Dec"
        Me.btnCtrlJ2Dec.Size = New System.Drawing.Size(51, 26)
        Me.btnCtrlJ2Dec.TabIndex = 7
        Me.btnCtrlJ2Dec.Text = "-"
        Me.btnCtrlJ2Dec.UseVisualStyleBackColor = True
        '
        'tbCtrlJ2
        '
        Me.tbCtrlJ2.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbCtrlJ2.AutoSize = False
        Me.tbCtrlJ2.LargeChange = 100
        Me.tbCtrlJ2.Location = New System.Drawing.Point(179, 58)
        Me.tbCtrlJ2.Maximum = 36000
        Me.tbCtrlJ2.Minimum = -36000
        Me.tbCtrlJ2.Name = "tbCtrlJ2"
        Me.tbCtrlJ2.Size = New System.Drawing.Size(217, 24)
        Me.tbCtrlJ2.TabIndex = 5
        Me.tbCtrlJ2.TickStyle = System.Windows.Forms.TickStyle.None
        '
        'numCtrlJ2Target
        '
        Me.numCtrlJ2Target.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.numCtrlJ2Target.DecimalPlaces = 2
        Me.numCtrlJ2Target.Location = New System.Drawing.Point(68, 60)
        Me.numCtrlJ2Target.Maximum = New Decimal(New Integer() {360, 0, 0, 0})
        Me.numCtrlJ2Target.Minimum = New Decimal(New Integer() {360, 0, 0, -2147483648})
        Me.numCtrlJ2Target.Name = "numCtrlJ2Target"
        Me.numCtrlJ2Target.Size = New System.Drawing.Size(105, 20)
        Me.numCtrlJ2Target.TabIndex = 8
        '
        'lblCtrlJ2
        '
        Me.lblCtrlJ2.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCtrlJ2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCtrlJ2.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblCtrlJ2.Location = New System.Drawing.Point(3, 58)
        Me.lblCtrlJ2.Margin = New System.Windows.Forms.Padding(3)
        Me.lblCtrlJ2.Name = "lblCtrlJ2"
        Me.lblCtrlJ2.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblCtrlJ2.Size = New System.Drawing.Size(59, 24)
        Me.lblCtrlJ2.TabIndex = 6
        Me.lblCtrlJ2.Text = "Joint 2:"
        Me.lblCtrlJ2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCtrlJ3
        '
        Me.lblCtrlJ3.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCtrlJ3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCtrlJ3.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblCtrlJ3.Location = New System.Drawing.Point(3, 88)
        Me.lblCtrlJ3.Margin = New System.Windows.Forms.Padding(3)
        Me.lblCtrlJ3.Name = "lblCtrlJ3"
        Me.lblCtrlJ3.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblCtrlJ3.Size = New System.Drawing.Size(59, 24)
        Me.lblCtrlJ3.TabIndex = 13
        Me.lblCtrlJ3.Text = "Joint 3:"
        Me.lblCtrlJ3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCtrlJ4
        '
        Me.lblCtrlJ4.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCtrlJ4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCtrlJ4.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblCtrlJ4.Location = New System.Drawing.Point(3, 118)
        Me.lblCtrlJ4.Margin = New System.Windows.Forms.Padding(3)
        Me.lblCtrlJ4.Name = "lblCtrlJ4"
        Me.lblCtrlJ4.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblCtrlJ4.Size = New System.Drawing.Size(59, 24)
        Me.lblCtrlJ4.TabIndex = 11
        Me.lblCtrlJ4.Text = "Joint 4:"
        Me.lblCtrlJ4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCtrlJ5
        '
        Me.lblCtrlJ5.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCtrlJ5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCtrlJ5.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblCtrlJ5.Location = New System.Drawing.Point(3, 148)
        Me.lblCtrlJ5.Margin = New System.Windows.Forms.Padding(3)
        Me.lblCtrlJ5.Name = "lblCtrlJ5"
        Me.lblCtrlJ5.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblCtrlJ5.Size = New System.Drawing.Size(59, 24)
        Me.lblCtrlJ5.TabIndex = 10
        Me.lblCtrlJ5.Text = "Joint 5:"
        Me.lblCtrlJ5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCtrlJ6
        '
        Me.lblCtrlJ6.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCtrlJ6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCtrlJ6.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblCtrlJ6.Location = New System.Drawing.Point(3, 178)
        Me.lblCtrlJ6.Margin = New System.Windows.Forms.Padding(3)
        Me.lblCtrlJ6.Name = "lblCtrlJ6"
        Me.lblCtrlJ6.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblCtrlJ6.Size = New System.Drawing.Size(59, 24)
        Me.lblCtrlJ6.TabIndex = 12
        Me.lblCtrlJ6.Text = "Joint 6:"
        Me.lblCtrlJ6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'numCtrlJ3Target
        '
        Me.numCtrlJ3Target.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.numCtrlJ3Target.DecimalPlaces = 2
        Me.numCtrlJ3Target.Location = New System.Drawing.Point(68, 90)
        Me.numCtrlJ3Target.Maximum = New Decimal(New Integer() {360, 0, 0, 0})
        Me.numCtrlJ3Target.Minimum = New Decimal(New Integer() {360, 0, 0, -2147483648})
        Me.numCtrlJ3Target.Name = "numCtrlJ3Target"
        Me.numCtrlJ3Target.Size = New System.Drawing.Size(105, 20)
        Me.numCtrlJ3Target.TabIndex = 14
        '
        'numCtrlJ4Target
        '
        Me.numCtrlJ4Target.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.numCtrlJ4Target.DecimalPlaces = 2
        Me.numCtrlJ4Target.Location = New System.Drawing.Point(68, 120)
        Me.numCtrlJ4Target.Maximum = New Decimal(New Integer() {360, 0, 0, 0})
        Me.numCtrlJ4Target.Minimum = New Decimal(New Integer() {360, 0, 0, -2147483648})
        Me.numCtrlJ4Target.Name = "numCtrlJ4Target"
        Me.numCtrlJ4Target.Size = New System.Drawing.Size(105, 20)
        Me.numCtrlJ4Target.TabIndex = 15
        '
        'numCtrlJ5Target
        '
        Me.numCtrlJ5Target.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.numCtrlJ5Target.DecimalPlaces = 2
        Me.numCtrlJ5Target.Location = New System.Drawing.Point(68, 150)
        Me.numCtrlJ5Target.Maximum = New Decimal(New Integer() {360, 0, 0, 0})
        Me.numCtrlJ5Target.Minimum = New Decimal(New Integer() {360, 0, 0, -2147483648})
        Me.numCtrlJ5Target.Name = "numCtrlJ5Target"
        Me.numCtrlJ5Target.Size = New System.Drawing.Size(105, 20)
        Me.numCtrlJ5Target.TabIndex = 16
        '
        'numCtrlJ6Target
        '
        Me.numCtrlJ6Target.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.numCtrlJ6Target.DecimalPlaces = 2
        Me.numCtrlJ6Target.Location = New System.Drawing.Point(68, 180)
        Me.numCtrlJ6Target.Maximum = New Decimal(New Integer() {360, 0, 0, 0})
        Me.numCtrlJ6Target.Minimum = New Decimal(New Integer() {360, 0, 0, -2147483648})
        Me.numCtrlJ6Target.Name = "numCtrlJ6Target"
        Me.numCtrlJ6Target.Size = New System.Drawing.Size(105, 20)
        Me.numCtrlJ6Target.TabIndex = 17
        '
        'tbCtrlJ3
        '
        Me.tbCtrlJ3.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbCtrlJ3.AutoSize = False
        Me.tbCtrlJ3.LargeChange = 100
        Me.tbCtrlJ3.Location = New System.Drawing.Point(179, 88)
        Me.tbCtrlJ3.Maximum = 36000
        Me.tbCtrlJ3.Minimum = -36000
        Me.tbCtrlJ3.Name = "tbCtrlJ3"
        Me.tbCtrlJ3.Size = New System.Drawing.Size(217, 24)
        Me.tbCtrlJ3.TabIndex = 18
        Me.tbCtrlJ3.TickStyle = System.Windows.Forms.TickStyle.None
        '
        'tbCtrlJ4
        '
        Me.tbCtrlJ4.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbCtrlJ4.AutoSize = False
        Me.tbCtrlJ4.LargeChange = 100
        Me.tbCtrlJ4.Location = New System.Drawing.Point(179, 118)
        Me.tbCtrlJ4.Maximum = 36000
        Me.tbCtrlJ4.Minimum = -36000
        Me.tbCtrlJ4.Name = "tbCtrlJ4"
        Me.tbCtrlJ4.Size = New System.Drawing.Size(217, 24)
        Me.tbCtrlJ4.TabIndex = 19
        Me.tbCtrlJ4.TickStyle = System.Windows.Forms.TickStyle.None
        '
        'tbCtrlJ5
        '
        Me.tbCtrlJ5.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbCtrlJ5.AutoSize = False
        Me.tbCtrlJ5.LargeChange = 100
        Me.tbCtrlJ5.Location = New System.Drawing.Point(179, 148)
        Me.tbCtrlJ5.Maximum = 36000
        Me.tbCtrlJ5.Minimum = -36000
        Me.tbCtrlJ5.Name = "tbCtrlJ5"
        Me.tbCtrlJ5.Size = New System.Drawing.Size(217, 24)
        Me.tbCtrlJ5.TabIndex = 20
        Me.tbCtrlJ5.TickStyle = System.Windows.Forms.TickStyle.None
        '
        'tbCtrlJ6
        '
        Me.tbCtrlJ6.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbCtrlJ6.AutoSize = False
        Me.tbCtrlJ6.LargeChange = 100
        Me.tbCtrlJ6.Location = New System.Drawing.Point(179, 178)
        Me.tbCtrlJ6.Maximum = 36000
        Me.tbCtrlJ6.Minimum = -36000
        Me.tbCtrlJ6.Name = "tbCtrlJ6"
        Me.tbCtrlJ6.Size = New System.Drawing.Size(217, 24)
        Me.tbCtrlJ6.TabIndex = 21
        Me.tbCtrlJ6.TickStyle = System.Windows.Forms.TickStyle.None
        '
        'btnCtrlJ3Dec
        '
        Me.btnCtrlJ3Dec.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCtrlJ3Dec.Location = New System.Drawing.Point(401, 87)
        Me.btnCtrlJ3Dec.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCtrlJ3Dec.Name = "btnCtrlJ3Dec"
        Me.btnCtrlJ3Dec.Size = New System.Drawing.Size(51, 26)
        Me.btnCtrlJ3Dec.TabIndex = 22
        Me.btnCtrlJ3Dec.Text = "-"
        Me.btnCtrlJ3Dec.UseVisualStyleBackColor = True
        '
        'btnCtrlJ4Dec
        '
        Me.btnCtrlJ4Dec.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCtrlJ4Dec.Location = New System.Drawing.Point(401, 117)
        Me.btnCtrlJ4Dec.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCtrlJ4Dec.Name = "btnCtrlJ4Dec"
        Me.btnCtrlJ4Dec.Size = New System.Drawing.Size(51, 26)
        Me.btnCtrlJ4Dec.TabIndex = 24
        Me.btnCtrlJ4Dec.Text = "-"
        Me.btnCtrlJ4Dec.UseVisualStyleBackColor = True
        '
        'btnCtrlJ5Dec
        '
        Me.btnCtrlJ5Dec.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCtrlJ5Dec.Location = New System.Drawing.Point(401, 147)
        Me.btnCtrlJ5Dec.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCtrlJ5Dec.Name = "btnCtrlJ5Dec"
        Me.btnCtrlJ5Dec.Size = New System.Drawing.Size(51, 26)
        Me.btnCtrlJ5Dec.TabIndex = 23
        Me.btnCtrlJ5Dec.Text = "-"
        Me.btnCtrlJ5Dec.UseVisualStyleBackColor = True
        '
        'btnCtrlJ6Dec
        '
        Me.btnCtrlJ6Dec.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCtrlJ6Dec.Location = New System.Drawing.Point(401, 177)
        Me.btnCtrlJ6Dec.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCtrlJ6Dec.Name = "btnCtrlJ6Dec"
        Me.btnCtrlJ6Dec.Size = New System.Drawing.Size(51, 26)
        Me.btnCtrlJ6Dec.TabIndex = 25
        Me.btnCtrlJ6Dec.Text = "-"
        Me.btnCtrlJ6Dec.UseVisualStyleBackColor = True
        '
        'btnCtrlJ3Inc
        '
        Me.btnCtrlJ3Inc.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCtrlJ3Inc.Location = New System.Drawing.Point(456, 87)
        Me.btnCtrlJ3Inc.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCtrlJ3Inc.Name = "btnCtrlJ3Inc"
        Me.btnCtrlJ3Inc.Size = New System.Drawing.Size(53, 26)
        Me.btnCtrlJ3Inc.TabIndex = 27
        Me.btnCtrlJ3Inc.Text = "+"
        Me.btnCtrlJ3Inc.UseVisualStyleBackColor = True
        '
        'btnCtrlJ4Inc
        '
        Me.btnCtrlJ4Inc.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCtrlJ4Inc.Location = New System.Drawing.Point(456, 117)
        Me.btnCtrlJ4Inc.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCtrlJ4Inc.Name = "btnCtrlJ4Inc"
        Me.btnCtrlJ4Inc.Size = New System.Drawing.Size(53, 26)
        Me.btnCtrlJ4Inc.TabIndex = 26
        Me.btnCtrlJ4Inc.Text = "+"
        Me.btnCtrlJ4Inc.UseVisualStyleBackColor = True
        '
        'btnCtrlJ5Inc
        '
        Me.btnCtrlJ5Inc.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCtrlJ5Inc.Location = New System.Drawing.Point(456, 147)
        Me.btnCtrlJ5Inc.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCtrlJ5Inc.Name = "btnCtrlJ5Inc"
        Me.btnCtrlJ5Inc.Size = New System.Drawing.Size(53, 26)
        Me.btnCtrlJ5Inc.TabIndex = 28
        Me.btnCtrlJ5Inc.Text = "+"
        Me.btnCtrlJ5Inc.UseVisualStyleBackColor = True
        '
        'btnCtrlJ6Inc
        '
        Me.btnCtrlJ6Inc.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCtrlJ6Inc.Location = New System.Drawing.Point(456, 177)
        Me.btnCtrlJ6Inc.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCtrlJ6Inc.Name = "btnCtrlJ6Inc"
        Me.btnCtrlJ6Inc.Size = New System.Drawing.Size(53, 26)
        Me.btnCtrlJ6Inc.TabIndex = 29
        Me.btnCtrlJ6Inc.Text = "+"
        Me.btnCtrlJ6Inc.UseVisualStyleBackColor = True
        '
        'lblTargetAngle
        '
        Me.lblTargetAngle.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel.SetColumnSpan(Me.lblTargetAngle, 2)
        Me.lblTargetAngle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTargetAngle.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblTargetAngle.Location = New System.Drawing.Point(68, 3)
        Me.lblTargetAngle.Margin = New System.Windows.Forms.Padding(3)
        Me.lblTargetAngle.Name = "lblTargetAngle"
        Me.lblTargetAngle.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblTargetAngle.Size = New System.Drawing.Size(328, 19)
        Me.lblTargetAngle.TabIndex = 30
        Me.lblTargetAngle.Text = "Zielwinkel"
        Me.lblTargetAngle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblJog
        '
        Me.lblJog.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel.SetColumnSpan(Me.lblJog, 2)
        Me.lblJog.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJog.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblJog.Location = New System.Drawing.Point(402, 3)
        Me.lblJog.Margin = New System.Windows.Forms.Padding(3)
        Me.lblJog.Name = "lblJog"
        Me.lblJog.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblJog.Size = New System.Drawing.Size(106, 19)
        Me.lblJog.TabIndex = 31
        Me.lblJog.Text = "Jog"
        Me.lblJog.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblMove, Me.cbMoveMode, Me.btnMoveStart, Me.ToolStripSeparator2, Me.lblJogInterval, Me.numJogInterval, Me.cbJogMode})
        Me.ToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(511, 26)
        Me.ToolStrip1.Stretch = True
        Me.ToolStrip1.TabIndex = 0
        '
        'lblMove
        '
        Me.lblMove.Name = "lblMove"
        Me.lblMove.Size = New System.Drawing.Size(66, 23)
        Me.lblMove.Text = "Bewegung:"
        '
        'cbMoveMode
        '
        Me.cbMoveMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMoveMode.Items.AddRange(New Object() {"getriggert", "direkt"})
        Me.cbMoveMode.Name = "cbMoveMode"
        Me.cbMoveMode.Size = New System.Drawing.Size(75, 26)
        '
        'btnMoveStart
        '
        Me.btnMoveStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnMoveStart.Image = CType(resources.GetObject("btnMoveStart.Image"), System.Drawing.Image)
        Me.btnMoveStart.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnMoveStart.Name = "btnMoveStart"
        Me.btnMoveStart.Size = New System.Drawing.Size(23, 23)
        Me.btnMoveStart.Text = "Start"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 26)
        '
        'lblJogInterval
        '
        Me.lblJogInterval.Name = "lblJogInterval"
        Me.lblJogInterval.Size = New System.Drawing.Size(67, 23)
        Me.lblJogInterval.Text = "Joginterval:"
        '
        'numJogInterval
        '
        Me.numJogInterval.DecimalPlaces = 1
        Me.numJogInterval.Maximum = New Decimal(New Integer() {100, 0, 0, 0})
        Me.numJogInterval.Minimum = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numJogInterval.Name = "numJogInterval"
        Me.numJogInterval.Size = New System.Drawing.Size(50, 23)
        Me.numJogInterval.Text = "0,0"
        Me.numJogInterval.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'cbJogMode
        '
        Me.cbJogMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbJogMode.Items.AddRange(New Object() {"Grad", "Steps"})
        Me.cbJogMode.Name = "cbJogMode"
        Me.cbJogMode.Size = New System.Drawing.Size(75, 26)
        '
        'ToolStrip2
        '
        Me.ToolStrip2.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblSpeed, Me.numSpeed, Me.ToolStripSeparator1, Me.lblAcc, Me.numAcc})
        Me.ToolStrip2.Location = New System.Drawing.Point(0, 26)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.Size = New System.Drawing.Size(511, 26)
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
        Me.numSpeed.Maximum = New Decimal(New Integer() {50, 0, 0, 0})
        Me.numSpeed.Minimum = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numSpeed.Name = "numSpeed"
        Me.numSpeed.Size = New System.Drawing.Size(50, 23)
        Me.numSpeed.Text = "25,00"
        Me.numSpeed.Value = New Decimal(New Integer() {2500, 0, 0, 131072})
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 26)
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
        Me.numAcc.Maximum = New Decimal(New Integer() {50, 0, 0, 0})
        Me.numAcc.Minimum = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numAcc.Name = "numAcc"
        Me.numAcc.Size = New System.Drawing.Size(50, 23)
        Me.numAcc.Text = "25,00"
        Me.numAcc.Value = New Decimal(New Integer() {2500, 0, 0, 131072})
        '
        'panCtrl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(511, 310)
        Me.Controls.Add(Me.ToolStripContainer)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "panCtrl"
        Me.Text = "Robo Steuerung"
        Me.ToolStripContainer.ContentPanel.ResumeLayout(False)
        Me.ToolStripContainer.TopToolStripPanel.ResumeLayout(False)
        Me.ToolStripContainer.TopToolStripPanel.PerformLayout()
        Me.ToolStripContainer.ResumeLayout(False)
        Me.ToolStripContainer.PerformLayout()
        Me.TableLayoutPanel.ResumeLayout(False)
        CType(Me.tbCtrlJ1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numCtrlJ1Target, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbCtrlJ2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numCtrlJ2Target, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numCtrlJ3Target, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numCtrlJ4Target, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numCtrlJ5Target, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numCtrlJ6Target, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbCtrlJ3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbCtrlJ4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbCtrlJ5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbCtrlJ6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ToolStrip2.ResumeLayout(False)
        Me.ToolStrip2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ToolStripContainer As ToolStripContainer
    Friend WithEvents TableLayoutPanel As TableLayoutPanel
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents tbCtrlJ1 As TrackBar
    Friend WithEvents lblCtrlJ1 As Label
    Friend WithEvents btnCtrlJ1Dec As Button
    Friend WithEvents btnCtrlJ1Inc As Button
    Friend WithEvents numCtrlJ1Target As NumericUpDown
    Friend WithEvents ToolStripComboBox1 As ToolStripComboBox
    Friend WithEvents cbJogMode As ToolStripComboBox
    Friend WithEvents lblJogInterval As ToolStripLabel
    Friend WithEvents btnCtrlJ2Inc As Button
    Friend WithEvents btnCtrlJ2Dec As Button
    Friend WithEvents tbCtrlJ2 As TrackBar
    Friend WithEvents numCtrlJ2Target As NumericUpDown
    Friend WithEvents lblCtrlJ2 As Label
    Friend WithEvents lblCtrlJ3 As Label
    Friend WithEvents lblCtrlJ4 As Label
    Friend WithEvents lblCtrlJ5 As Label
    Friend WithEvents lblCtrlJ6 As Label
    Friend WithEvents numCtrlJ3Target As NumericUpDown
    Friend WithEvents numCtrlJ4Target As NumericUpDown
    Friend WithEvents numCtrlJ5Target As NumericUpDown
    Friend WithEvents numCtrlJ6Target As NumericUpDown
    Friend WithEvents tbCtrlJ3 As TrackBar
    Friend WithEvents tbCtrlJ4 As TrackBar
    Friend WithEvents tbCtrlJ5 As TrackBar
    Friend WithEvents tbCtrlJ6 As TrackBar
    Friend WithEvents btnCtrlJ3Dec As Button
    Friend WithEvents btnCtrlJ4Dec As Button
    Friend WithEvents btnCtrlJ5Dec As Button
    Friend WithEvents btnCtrlJ6Dec As Button
    Friend WithEvents btnCtrlJ3Inc As Button
    Friend WithEvents btnCtrlJ4Inc As Button
    Friend WithEvents btnCtrlJ5Inc As Button
    Friend WithEvents btnCtrlJ6Inc As Button
    Friend WithEvents lblTargetAngle As Label
    Friend WithEvents lblJog As Label
    Friend WithEvents numJogInterval As ToolStripNumericUpDown
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents lblMove As ToolStripLabel
    Friend WithEvents cbMoveMode As ToolStripComboBox
    Friend WithEvents btnMoveStart As ToolStripButton
    Friend WithEvents ToolStrip2 As ToolStrip
    Friend WithEvents lblSpeed As ToolStripLabel
    Friend WithEvents lblAcc As ToolStripLabel
    Friend WithEvents numSpeed As ToolStripNumericUpDown
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents numAcc As ToolStripNumericUpDown
End Class
