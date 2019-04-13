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
        Me.lblCtrl1 = New System.Windows.Forms.Label()
        Me.numCtrl1 = New System.Windows.Forms.NumericUpDown()
        Me.numCtrl2 = New System.Windows.Forms.NumericUpDown()
        Me.lblCtrl2 = New System.Windows.Forms.Label()
        Me.lblCtrl3 = New System.Windows.Forms.Label()
        Me.lblCtrl4 = New System.Windows.Forms.Label()
        Me.lblCtrl5 = New System.Windows.Forms.Label()
        Me.lblCtrl6 = New System.Windows.Forms.Label()
        Me.numCtrl3 = New System.Windows.Forms.NumericUpDown()
        Me.numCtrl4 = New System.Windows.Forms.NumericUpDown()
        Me.numCtrl5 = New System.Windows.Forms.NumericUpDown()
        Me.numCtrl6 = New System.Windows.Forms.NumericUpDown()
        Me.lblTargetAngle = New System.Windows.Forms.Label()
        Me.lblServo1 = New System.Windows.Forms.Label()
        Me.lblServo2 = New System.Windows.Forms.Label()
        Me.lblServo3 = New System.Windows.Forms.Label()
        Me.numServ1 = New System.Windows.Forms.NumericUpDown()
        Me.numServ2 = New System.Windows.Forms.NumericUpDown()
        Me.numServ3 = New System.Windows.Forms.NumericUpDown()
        Me.btnCtrl1Inc = New System.Windows.Forms.Button()
        Me.btnCtrl2Inc = New System.Windows.Forms.Button()
        Me.btnCtrl3Inc = New System.Windows.Forms.Button()
        Me.btnCtrl4Inc = New System.Windows.Forms.Button()
        Me.btnCtrl5Inc = New System.Windows.Forms.Button()
        Me.btnCtrl6Inc = New System.Windows.Forms.Button()
        Me.btnServ1Inc = New System.Windows.Forms.Button()
        Me.btnServ2Inc = New System.Windows.Forms.Button()
        Me.btnServ3Inc = New System.Windows.Forms.Button()
        Me.btnCtrl1Dec = New System.Windows.Forms.Button()
        Me.btnCtrl2Dec = New System.Windows.Forms.Button()
        Me.btnCtrl3Dec = New System.Windows.Forms.Button()
        Me.btnCtrl4Dec = New System.Windows.Forms.Button()
        Me.btnCtrl5Dec = New System.Windows.Forms.Button()
        Me.btnCtrl6Dec = New System.Windows.Forms.Button()
        Me.btnServ1Dec = New System.Windows.Forms.Button()
        Me.btnServ2Dec = New System.Windows.Forms.Button()
        Me.btnServ3Dec = New System.Windows.Forms.Button()
        Me.tbCtrl1 = New System.Windows.Forms.TrackBar()
        Me.tbCtrl2 = New System.Windows.Forms.TrackBar()
        Me.tbCtrl3 = New System.Windows.Forms.TrackBar()
        Me.tbCtrl4 = New System.Windows.Forms.TrackBar()
        Me.tbCtrl5 = New System.Windows.Forms.TrackBar()
        Me.tbCtrl6 = New System.Windows.Forms.TrackBar()
        Me.tbServ1 = New System.Windows.Forms.TrackBar()
        Me.tbServ2 = New System.Windows.Forms.TrackBar()
        Me.tbServ3 = New System.Windows.Forms.TrackBar()
        Me.lblCtrl1Unit = New System.Windows.Forms.Label()
        Me.lblCtrl2Unit = New System.Windows.Forms.Label()
        Me.lblCtrl3Unit = New System.Windows.Forms.Label()
        Me.lblCtrl4Unit = New System.Windows.Forms.Label()
        Me.lblCtrl5Unit = New System.Windows.Forms.Label()
        Me.lblCtrl6Unit = New System.Windows.Forms.Label()
        Me.lblSrv1Unit = New System.Windows.Forms.Label()
        Me.lblSrv2Unit = New System.Windows.Forms.Label()
        Me.lblSrv3Unit = New System.Windows.Forms.Label()
        Me.lblJog = New System.Windows.Forms.Label()
        Me.ToolStrip2 = New System.Windows.Forms.ToolStrip()
        Me.lblMode = New System.Windows.Forms.ToolStripLabel()
        Me.cbJointOrTCP = New System.Windows.Forms.ToolStripComboBox()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.lblSpeed = New System.Windows.Forms.ToolStripLabel()
        Me.numSpeed = New RoboAUP17.ToolStripNumericUpDown()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.lblAcc = New System.Windows.Forms.ToolStripLabel()
        Me.numAcc = New RoboAUP17.ToolStripNumericUpDown()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.lblMove = New System.Windows.Forms.ToolStripLabel()
        Me.cbMoveMode = New System.Windows.Forms.ToolStripComboBox()
        Me.btnMoveStart = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.lblJogInterval = New System.Windows.Forms.ToolStripLabel()
        Me.numJogInterval1 = New RoboAUP17.ToolStripNumericUpDown()
        Me.lblUnitMm = New System.Windows.Forms.ToolStripLabel()
        Me.numJogInterval2 = New RoboAUP17.ToolStripNumericUpDown()
        Me.lblUnitDeg = New System.Windows.Forms.ToolStripLabel()
        Me.cbJogMode = New System.Windows.Forms.ToolStripComboBox()
        Me.ToolStripContainer.ContentPanel.SuspendLayout()
        Me.ToolStripContainer.TopToolStripPanel.SuspendLayout()
        Me.ToolStripContainer.SuspendLayout()
        Me.TableLayoutPanel.SuspendLayout()
        CType(Me.numCtrl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numCtrl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numCtrl3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numCtrl4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numCtrl5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numCtrl6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numServ1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numServ2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numServ3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbCtrl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbCtrl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbCtrl3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbCtrl4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbCtrl5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbCtrl6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbServ1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbServ2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbServ3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip2.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStripContainer
        '
        Me.ToolStripContainer.BottomToolStripPanelVisible = False
        '
        'ToolStripContainer.ContentPanel
        '
        Me.ToolStripContainer.ContentPanel.Controls.Add(Me.TableLayoutPanel)
        Me.ToolStripContainer.ContentPanel.Size = New System.Drawing.Size(536, 358)
        Me.ToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ToolStripContainer.LeftToolStripPanelVisible = False
        Me.ToolStripContainer.Location = New System.Drawing.Point(0, 0)
        Me.ToolStripContainer.Name = "ToolStripContainer"
        Me.ToolStripContainer.RightToolStripPanelVisible = False
        Me.ToolStripContainer.Size = New System.Drawing.Size(536, 410)
        Me.ToolStripContainer.TabIndex = 0
        Me.ToolStripContainer.Text = "ToolStripContainer"
        '
        'ToolStripContainer.TopToolStripPanel
        '
        Me.ToolStripContainer.TopToolStripPanel.Controls.Add(Me.ToolStrip1)
        Me.ToolStripContainer.TopToolStripPanel.Controls.Add(Me.ToolStrip2)
        '
        'TableLayoutPanel
        '
        Me.TableLayoutPanel.AutoScroll = True
        Me.TableLayoutPanel.AutoScrollMinSize = New System.Drawing.Size(300, 0)
        Me.TableLayoutPanel.ColumnCount = 6
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75.0!))
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel.Controls.Add(Me.lblCtrl1, 0, 1)
        Me.TableLayoutPanel.Controls.Add(Me.numCtrl1, 1, 1)
        Me.TableLayoutPanel.Controls.Add(Me.numCtrl2, 1, 2)
        Me.TableLayoutPanel.Controls.Add(Me.lblCtrl2, 0, 2)
        Me.TableLayoutPanel.Controls.Add(Me.lblCtrl3, 0, 3)
        Me.TableLayoutPanel.Controls.Add(Me.lblCtrl4, 0, 4)
        Me.TableLayoutPanel.Controls.Add(Me.lblCtrl5, 0, 5)
        Me.TableLayoutPanel.Controls.Add(Me.lblCtrl6, 0, 6)
        Me.TableLayoutPanel.Controls.Add(Me.numCtrl3, 1, 3)
        Me.TableLayoutPanel.Controls.Add(Me.numCtrl4, 1, 4)
        Me.TableLayoutPanel.Controls.Add(Me.numCtrl5, 1, 5)
        Me.TableLayoutPanel.Controls.Add(Me.numCtrl6, 1, 6)
        Me.TableLayoutPanel.Controls.Add(Me.lblTargetAngle, 1, 0)
        Me.TableLayoutPanel.Controls.Add(Me.lblServo1, 0, 7)
        Me.TableLayoutPanel.Controls.Add(Me.lblServo2, 0, 8)
        Me.TableLayoutPanel.Controls.Add(Me.lblServo3, 0, 9)
        Me.TableLayoutPanel.Controls.Add(Me.numServ1, 1, 7)
        Me.TableLayoutPanel.Controls.Add(Me.numServ2, 1, 8)
        Me.TableLayoutPanel.Controls.Add(Me.numServ3, 1, 9)
        Me.TableLayoutPanel.Controls.Add(Me.btnCtrl1Inc, 5, 1)
        Me.TableLayoutPanel.Controls.Add(Me.btnCtrl2Inc, 5, 2)
        Me.TableLayoutPanel.Controls.Add(Me.btnCtrl3Inc, 5, 3)
        Me.TableLayoutPanel.Controls.Add(Me.btnCtrl4Inc, 5, 4)
        Me.TableLayoutPanel.Controls.Add(Me.btnCtrl5Inc, 5, 5)
        Me.TableLayoutPanel.Controls.Add(Me.btnCtrl6Inc, 5, 6)
        Me.TableLayoutPanel.Controls.Add(Me.btnServ1Inc, 5, 7)
        Me.TableLayoutPanel.Controls.Add(Me.btnServ2Inc, 5, 8)
        Me.TableLayoutPanel.Controls.Add(Me.btnServ3Inc, 5, 9)
        Me.TableLayoutPanel.Controls.Add(Me.btnCtrl1Dec, 4, 1)
        Me.TableLayoutPanel.Controls.Add(Me.btnCtrl2Dec, 4, 2)
        Me.TableLayoutPanel.Controls.Add(Me.btnCtrl3Dec, 4, 3)
        Me.TableLayoutPanel.Controls.Add(Me.btnCtrl4Dec, 4, 4)
        Me.TableLayoutPanel.Controls.Add(Me.btnCtrl5Dec, 4, 5)
        Me.TableLayoutPanel.Controls.Add(Me.btnCtrl6Dec, 4, 6)
        Me.TableLayoutPanel.Controls.Add(Me.btnServ1Dec, 4, 7)
        Me.TableLayoutPanel.Controls.Add(Me.btnServ2Dec, 4, 8)
        Me.TableLayoutPanel.Controls.Add(Me.btnServ3Dec, 4, 9)
        Me.TableLayoutPanel.Controls.Add(Me.tbCtrl1, 3, 1)
        Me.TableLayoutPanel.Controls.Add(Me.tbCtrl2, 3, 2)
        Me.TableLayoutPanel.Controls.Add(Me.tbCtrl3, 3, 3)
        Me.TableLayoutPanel.Controls.Add(Me.tbCtrl4, 3, 4)
        Me.TableLayoutPanel.Controls.Add(Me.tbCtrl5, 3, 5)
        Me.TableLayoutPanel.Controls.Add(Me.tbCtrl6, 3, 6)
        Me.TableLayoutPanel.Controls.Add(Me.tbServ1, 3, 7)
        Me.TableLayoutPanel.Controls.Add(Me.tbServ2, 3, 8)
        Me.TableLayoutPanel.Controls.Add(Me.tbServ3, 3, 9)
        Me.TableLayoutPanel.Controls.Add(Me.lblCtrl1Unit, 2, 1)
        Me.TableLayoutPanel.Controls.Add(Me.lblCtrl2Unit, 2, 2)
        Me.TableLayoutPanel.Controls.Add(Me.lblCtrl3Unit, 2, 3)
        Me.TableLayoutPanel.Controls.Add(Me.lblCtrl4Unit, 2, 4)
        Me.TableLayoutPanel.Controls.Add(Me.lblCtrl5Unit, 2, 5)
        Me.TableLayoutPanel.Controls.Add(Me.lblCtrl6Unit, 2, 6)
        Me.TableLayoutPanel.Controls.Add(Me.lblSrv1Unit, 2, 7)
        Me.TableLayoutPanel.Controls.Add(Me.lblSrv2Unit, 2, 8)
        Me.TableLayoutPanel.Controls.Add(Me.lblSrv3Unit, 2, 9)
        Me.TableLayoutPanel.Controls.Add(Me.lblJog, 4, 0)
        Me.TableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel.Name = "TableLayoutPanel"
        Me.TableLayoutPanel.RowCount = 11
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel.Size = New System.Drawing.Size(536, 358)
        Me.TableLayoutPanel.TabIndex = 0
        '
        'lblCtrl1
        '
        Me.lblCtrl1.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCtrl1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCtrl1.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblCtrl1.Location = New System.Drawing.Point(3, 28)
        Me.lblCtrl1.Margin = New System.Windows.Forms.Padding(3)
        Me.lblCtrl1.Name = "lblCtrl1"
        Me.lblCtrl1.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblCtrl1.Size = New System.Drawing.Size(44, 24)
        Me.lblCtrl1.TabIndex = 1
        Me.lblCtrl1.Text = "J1:"
        Me.lblCtrl1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'numCtrl1
        '
        Me.numCtrl1.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.numCtrl1.DecimalPlaces = 2
        Me.numCtrl1.Enabled = False
        Me.numCtrl1.Location = New System.Drawing.Point(53, 30)
        Me.numCtrl1.Maximum = New Decimal(New Integer() {360, 0, 0, 0})
        Me.numCtrl1.Minimum = New Decimal(New Integer() {360, 0, 0, -2147483648})
        Me.numCtrl1.Name = "numCtrl1"
        Me.numCtrl1.Size = New System.Drawing.Size(69, 20)
        Me.numCtrl1.TabIndex = 3
        '
        'numCtrl2
        '
        Me.numCtrl2.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.numCtrl2.DecimalPlaces = 2
        Me.numCtrl2.Enabled = False
        Me.numCtrl2.Location = New System.Drawing.Point(53, 60)
        Me.numCtrl2.Maximum = New Decimal(New Integer() {360, 0, 0, 0})
        Me.numCtrl2.Minimum = New Decimal(New Integer() {360, 0, 0, -2147483648})
        Me.numCtrl2.Name = "numCtrl2"
        Me.numCtrl2.Size = New System.Drawing.Size(69, 20)
        Me.numCtrl2.TabIndex = 8
        '
        'lblCtrl2
        '
        Me.lblCtrl2.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCtrl2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCtrl2.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblCtrl2.Location = New System.Drawing.Point(3, 58)
        Me.lblCtrl2.Margin = New System.Windows.Forms.Padding(3)
        Me.lblCtrl2.Name = "lblCtrl2"
        Me.lblCtrl2.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblCtrl2.Size = New System.Drawing.Size(44, 24)
        Me.lblCtrl2.TabIndex = 6
        Me.lblCtrl2.Text = "J2:"
        Me.lblCtrl2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCtrl3
        '
        Me.lblCtrl3.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCtrl3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCtrl3.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblCtrl3.Location = New System.Drawing.Point(3, 88)
        Me.lblCtrl3.Margin = New System.Windows.Forms.Padding(3)
        Me.lblCtrl3.Name = "lblCtrl3"
        Me.lblCtrl3.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblCtrl3.Size = New System.Drawing.Size(44, 24)
        Me.lblCtrl3.TabIndex = 13
        Me.lblCtrl3.Text = "J3:"
        Me.lblCtrl3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCtrl4
        '
        Me.lblCtrl4.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCtrl4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCtrl4.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblCtrl4.Location = New System.Drawing.Point(3, 118)
        Me.lblCtrl4.Margin = New System.Windows.Forms.Padding(3)
        Me.lblCtrl4.Name = "lblCtrl4"
        Me.lblCtrl4.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblCtrl4.Size = New System.Drawing.Size(44, 24)
        Me.lblCtrl4.TabIndex = 11
        Me.lblCtrl4.Text = "J4:"
        Me.lblCtrl4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCtrl5
        '
        Me.lblCtrl5.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCtrl5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCtrl5.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblCtrl5.Location = New System.Drawing.Point(3, 148)
        Me.lblCtrl5.Margin = New System.Windows.Forms.Padding(3)
        Me.lblCtrl5.Name = "lblCtrl5"
        Me.lblCtrl5.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblCtrl5.Size = New System.Drawing.Size(44, 24)
        Me.lblCtrl5.TabIndex = 10
        Me.lblCtrl5.Text = "J5:"
        Me.lblCtrl5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCtrl6
        '
        Me.lblCtrl6.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCtrl6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCtrl6.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblCtrl6.Location = New System.Drawing.Point(3, 178)
        Me.lblCtrl6.Margin = New System.Windows.Forms.Padding(3)
        Me.lblCtrl6.Name = "lblCtrl6"
        Me.lblCtrl6.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblCtrl6.Size = New System.Drawing.Size(44, 24)
        Me.lblCtrl6.TabIndex = 12
        Me.lblCtrl6.Text = "J6:"
        Me.lblCtrl6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'numCtrl3
        '
        Me.numCtrl3.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.numCtrl3.DecimalPlaces = 2
        Me.numCtrl3.Enabled = False
        Me.numCtrl3.Location = New System.Drawing.Point(53, 90)
        Me.numCtrl3.Maximum = New Decimal(New Integer() {360, 0, 0, 0})
        Me.numCtrl3.Minimum = New Decimal(New Integer() {360, 0, 0, -2147483648})
        Me.numCtrl3.Name = "numCtrl3"
        Me.numCtrl3.Size = New System.Drawing.Size(69, 20)
        Me.numCtrl3.TabIndex = 14
        '
        'numCtrl4
        '
        Me.numCtrl4.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.numCtrl4.DecimalPlaces = 2
        Me.numCtrl4.Enabled = False
        Me.numCtrl4.Location = New System.Drawing.Point(53, 120)
        Me.numCtrl4.Maximum = New Decimal(New Integer() {360, 0, 0, 0})
        Me.numCtrl4.Minimum = New Decimal(New Integer() {360, 0, 0, -2147483648})
        Me.numCtrl4.Name = "numCtrl4"
        Me.numCtrl4.Size = New System.Drawing.Size(69, 20)
        Me.numCtrl4.TabIndex = 15
        '
        'numCtrl5
        '
        Me.numCtrl5.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.numCtrl5.DecimalPlaces = 2
        Me.numCtrl5.Enabled = False
        Me.numCtrl5.Location = New System.Drawing.Point(53, 150)
        Me.numCtrl5.Maximum = New Decimal(New Integer() {360, 0, 0, 0})
        Me.numCtrl5.Minimum = New Decimal(New Integer() {360, 0, 0, -2147483648})
        Me.numCtrl5.Name = "numCtrl5"
        Me.numCtrl5.Size = New System.Drawing.Size(69, 20)
        Me.numCtrl5.TabIndex = 16
        '
        'numCtrl6
        '
        Me.numCtrl6.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.numCtrl6.DecimalPlaces = 2
        Me.numCtrl6.Enabled = False
        Me.numCtrl6.Location = New System.Drawing.Point(53, 180)
        Me.numCtrl6.Maximum = New Decimal(New Integer() {360, 0, 0, 0})
        Me.numCtrl6.Minimum = New Decimal(New Integer() {360, 0, 0, -2147483648})
        Me.numCtrl6.Name = "numCtrl6"
        Me.numCtrl6.Size = New System.Drawing.Size(69, 20)
        Me.numCtrl6.TabIndex = 17
        '
        'lblTargetAngle
        '
        Me.lblTargetAngle.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel.SetColumnSpan(Me.lblTargetAngle, 3)
        Me.lblTargetAngle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTargetAngle.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblTargetAngle.Location = New System.Drawing.Point(53, 3)
        Me.lblTargetAngle.Margin = New System.Windows.Forms.Padding(3)
        Me.lblTargetAngle.Name = "lblTargetAngle"
        Me.lblTargetAngle.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblTargetAngle.Size = New System.Drawing.Size(368, 19)
        Me.lblTargetAngle.TabIndex = 30
        Me.lblTargetAngle.Text = "Steuerung"
        Me.lblTargetAngle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblServo1
        '
        Me.lblServo1.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblServo1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblServo1.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblServo1.Location = New System.Drawing.Point(3, 208)
        Me.lblServo1.Margin = New System.Windows.Forms.Padding(3)
        Me.lblServo1.Name = "lblServo1"
        Me.lblServo1.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblServo1.Size = New System.Drawing.Size(44, 24)
        Me.lblServo1.TabIndex = 32
        Me.lblServo1.Text = "Ser1:"
        Me.lblServo1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblServo2
        '
        Me.lblServo2.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblServo2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblServo2.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblServo2.Location = New System.Drawing.Point(3, 238)
        Me.lblServo2.Margin = New System.Windows.Forms.Padding(3)
        Me.lblServo2.Name = "lblServo2"
        Me.lblServo2.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblServo2.Size = New System.Drawing.Size(44, 24)
        Me.lblServo2.TabIndex = 33
        Me.lblServo2.Text = "Ser2:"
        Me.lblServo2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblServo3
        '
        Me.lblServo3.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblServo3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblServo3.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblServo3.Location = New System.Drawing.Point(3, 268)
        Me.lblServo3.Margin = New System.Windows.Forms.Padding(3)
        Me.lblServo3.Name = "lblServo3"
        Me.lblServo3.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblServo3.Size = New System.Drawing.Size(44, 24)
        Me.lblServo3.TabIndex = 34
        Me.lblServo3.Text = "Ser3:"
        Me.lblServo3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'numServ1
        '
        Me.numServ1.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.numServ1.DecimalPlaces = 2
        Me.numServ1.Enabled = False
        Me.numServ1.Location = New System.Drawing.Point(53, 210)
        Me.numServ1.Name = "numServ1"
        Me.numServ1.Size = New System.Drawing.Size(69, 20)
        Me.numServ1.TabIndex = 35
        '
        'numServ2
        '
        Me.numServ2.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.numServ2.DecimalPlaces = 2
        Me.numServ2.Enabled = False
        Me.numServ2.Location = New System.Drawing.Point(53, 240)
        Me.numServ2.Name = "numServ2"
        Me.numServ2.Size = New System.Drawing.Size(69, 20)
        Me.numServ2.TabIndex = 36
        '
        'numServ3
        '
        Me.numServ3.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.numServ3.DecimalPlaces = 2
        Me.numServ3.Enabled = False
        Me.numServ3.Location = New System.Drawing.Point(53, 270)
        Me.numServ3.Name = "numServ3"
        Me.numServ3.Size = New System.Drawing.Size(69, 20)
        Me.numServ3.TabIndex = 37
        '
        'btnCtrl1Inc
        '
        Me.btnCtrl1Inc.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCtrl1Inc.Enabled = False
        Me.btnCtrl1Inc.Location = New System.Drawing.Point(481, 27)
        Me.btnCtrl1Inc.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCtrl1Inc.Name = "btnCtrl1Inc"
        Me.btnCtrl1Inc.Size = New System.Drawing.Size(53, 26)
        Me.btnCtrl1Inc.TabIndex = 4
        Me.btnCtrl1Inc.Text = "+"
        Me.btnCtrl1Inc.UseVisualStyleBackColor = True
        '
        'btnCtrl2Inc
        '
        Me.btnCtrl2Inc.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCtrl2Inc.Enabled = False
        Me.btnCtrl2Inc.Location = New System.Drawing.Point(481, 57)
        Me.btnCtrl2Inc.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCtrl2Inc.Name = "btnCtrl2Inc"
        Me.btnCtrl2Inc.Size = New System.Drawing.Size(53, 26)
        Me.btnCtrl2Inc.TabIndex = 9
        Me.btnCtrl2Inc.Text = "+"
        Me.btnCtrl2Inc.UseVisualStyleBackColor = True
        '
        'btnCtrl3Inc
        '
        Me.btnCtrl3Inc.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCtrl3Inc.Enabled = False
        Me.btnCtrl3Inc.Location = New System.Drawing.Point(481, 87)
        Me.btnCtrl3Inc.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCtrl3Inc.Name = "btnCtrl3Inc"
        Me.btnCtrl3Inc.Size = New System.Drawing.Size(53, 26)
        Me.btnCtrl3Inc.TabIndex = 27
        Me.btnCtrl3Inc.Text = "+"
        Me.btnCtrl3Inc.UseVisualStyleBackColor = True
        '
        'btnCtrl4Inc
        '
        Me.btnCtrl4Inc.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCtrl4Inc.Enabled = False
        Me.btnCtrl4Inc.Location = New System.Drawing.Point(481, 117)
        Me.btnCtrl4Inc.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCtrl4Inc.Name = "btnCtrl4Inc"
        Me.btnCtrl4Inc.Size = New System.Drawing.Size(53, 26)
        Me.btnCtrl4Inc.TabIndex = 26
        Me.btnCtrl4Inc.Text = "+"
        Me.btnCtrl4Inc.UseVisualStyleBackColor = True
        '
        'btnCtrl5Inc
        '
        Me.btnCtrl5Inc.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCtrl5Inc.Enabled = False
        Me.btnCtrl5Inc.Location = New System.Drawing.Point(481, 147)
        Me.btnCtrl5Inc.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCtrl5Inc.Name = "btnCtrl5Inc"
        Me.btnCtrl5Inc.Size = New System.Drawing.Size(53, 26)
        Me.btnCtrl5Inc.TabIndex = 28
        Me.btnCtrl5Inc.Text = "+"
        Me.btnCtrl5Inc.UseVisualStyleBackColor = True
        '
        'btnCtrl6Inc
        '
        Me.btnCtrl6Inc.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCtrl6Inc.Enabled = False
        Me.btnCtrl6Inc.Location = New System.Drawing.Point(481, 177)
        Me.btnCtrl6Inc.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCtrl6Inc.Name = "btnCtrl6Inc"
        Me.btnCtrl6Inc.Size = New System.Drawing.Size(53, 26)
        Me.btnCtrl6Inc.TabIndex = 29
        Me.btnCtrl6Inc.Text = "+"
        Me.btnCtrl6Inc.UseVisualStyleBackColor = True
        '
        'btnServ1Inc
        '
        Me.btnServ1Inc.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnServ1Inc.Enabled = False
        Me.btnServ1Inc.Location = New System.Drawing.Point(481, 207)
        Me.btnServ1Inc.Margin = New System.Windows.Forms.Padding(2)
        Me.btnServ1Inc.Name = "btnServ1Inc"
        Me.btnServ1Inc.Size = New System.Drawing.Size(53, 26)
        Me.btnServ1Inc.TabIndex = 44
        Me.btnServ1Inc.Text = "+"
        Me.btnServ1Inc.UseVisualStyleBackColor = True
        '
        'btnServ2Inc
        '
        Me.btnServ2Inc.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnServ2Inc.Enabled = False
        Me.btnServ2Inc.Location = New System.Drawing.Point(481, 237)
        Me.btnServ2Inc.Margin = New System.Windows.Forms.Padding(2)
        Me.btnServ2Inc.Name = "btnServ2Inc"
        Me.btnServ2Inc.Size = New System.Drawing.Size(53, 26)
        Me.btnServ2Inc.TabIndex = 45
        Me.btnServ2Inc.Text = "+"
        Me.btnServ2Inc.UseVisualStyleBackColor = True
        '
        'btnServ3Inc
        '
        Me.btnServ3Inc.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnServ3Inc.Enabled = False
        Me.btnServ3Inc.Location = New System.Drawing.Point(481, 267)
        Me.btnServ3Inc.Margin = New System.Windows.Forms.Padding(2)
        Me.btnServ3Inc.Name = "btnServ3Inc"
        Me.btnServ3Inc.Size = New System.Drawing.Size(53, 26)
        Me.btnServ3Inc.TabIndex = 46
        Me.btnServ3Inc.Text = "+"
        Me.btnServ3Inc.UseVisualStyleBackColor = True
        '
        'btnCtrl1Dec
        '
        Me.btnCtrl1Dec.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCtrl1Dec.Enabled = False
        Me.btnCtrl1Dec.Location = New System.Drawing.Point(426, 27)
        Me.btnCtrl1Dec.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCtrl1Dec.Name = "btnCtrl1Dec"
        Me.btnCtrl1Dec.Size = New System.Drawing.Size(51, 26)
        Me.btnCtrl1Dec.TabIndex = 2
        Me.btnCtrl1Dec.Text = "-"
        Me.btnCtrl1Dec.UseVisualStyleBackColor = True
        '
        'btnCtrl2Dec
        '
        Me.btnCtrl2Dec.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCtrl2Dec.Enabled = False
        Me.btnCtrl2Dec.Location = New System.Drawing.Point(426, 57)
        Me.btnCtrl2Dec.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCtrl2Dec.Name = "btnCtrl2Dec"
        Me.btnCtrl2Dec.Size = New System.Drawing.Size(51, 26)
        Me.btnCtrl2Dec.TabIndex = 7
        Me.btnCtrl2Dec.Text = "-"
        Me.btnCtrl2Dec.UseVisualStyleBackColor = True
        '
        'btnCtrl3Dec
        '
        Me.btnCtrl3Dec.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCtrl3Dec.Enabled = False
        Me.btnCtrl3Dec.Location = New System.Drawing.Point(426, 87)
        Me.btnCtrl3Dec.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCtrl3Dec.Name = "btnCtrl3Dec"
        Me.btnCtrl3Dec.Size = New System.Drawing.Size(51, 26)
        Me.btnCtrl3Dec.TabIndex = 22
        Me.btnCtrl3Dec.Text = "-"
        Me.btnCtrl3Dec.UseVisualStyleBackColor = True
        '
        'btnCtrl4Dec
        '
        Me.btnCtrl4Dec.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCtrl4Dec.Enabled = False
        Me.btnCtrl4Dec.Location = New System.Drawing.Point(426, 117)
        Me.btnCtrl4Dec.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCtrl4Dec.Name = "btnCtrl4Dec"
        Me.btnCtrl4Dec.Size = New System.Drawing.Size(51, 26)
        Me.btnCtrl4Dec.TabIndex = 24
        Me.btnCtrl4Dec.Text = "-"
        Me.btnCtrl4Dec.UseVisualStyleBackColor = True
        '
        'btnCtrl5Dec
        '
        Me.btnCtrl5Dec.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCtrl5Dec.Enabled = False
        Me.btnCtrl5Dec.Location = New System.Drawing.Point(426, 147)
        Me.btnCtrl5Dec.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCtrl5Dec.Name = "btnCtrl5Dec"
        Me.btnCtrl5Dec.Size = New System.Drawing.Size(51, 26)
        Me.btnCtrl5Dec.TabIndex = 23
        Me.btnCtrl5Dec.Text = "-"
        Me.btnCtrl5Dec.UseVisualStyleBackColor = True
        '
        'btnCtrl6Dec
        '
        Me.btnCtrl6Dec.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCtrl6Dec.Enabled = False
        Me.btnCtrl6Dec.Location = New System.Drawing.Point(426, 177)
        Me.btnCtrl6Dec.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCtrl6Dec.Name = "btnCtrl6Dec"
        Me.btnCtrl6Dec.Size = New System.Drawing.Size(51, 26)
        Me.btnCtrl6Dec.TabIndex = 25
        Me.btnCtrl6Dec.Text = "-"
        Me.btnCtrl6Dec.UseVisualStyleBackColor = True
        '
        'btnServ1Dec
        '
        Me.btnServ1Dec.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnServ1Dec.Enabled = False
        Me.btnServ1Dec.Location = New System.Drawing.Point(426, 207)
        Me.btnServ1Dec.Margin = New System.Windows.Forms.Padding(2)
        Me.btnServ1Dec.Name = "btnServ1Dec"
        Me.btnServ1Dec.Size = New System.Drawing.Size(51, 26)
        Me.btnServ1Dec.TabIndex = 41
        Me.btnServ1Dec.Text = "-"
        Me.btnServ1Dec.UseVisualStyleBackColor = True
        '
        'btnServ2Dec
        '
        Me.btnServ2Dec.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnServ2Dec.Enabled = False
        Me.btnServ2Dec.Location = New System.Drawing.Point(426, 237)
        Me.btnServ2Dec.Margin = New System.Windows.Forms.Padding(2)
        Me.btnServ2Dec.Name = "btnServ2Dec"
        Me.btnServ2Dec.Size = New System.Drawing.Size(51, 26)
        Me.btnServ2Dec.TabIndex = 42
        Me.btnServ2Dec.Text = "-"
        Me.btnServ2Dec.UseVisualStyleBackColor = True
        '
        'btnServ3Dec
        '
        Me.btnServ3Dec.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnServ3Dec.Enabled = False
        Me.btnServ3Dec.Location = New System.Drawing.Point(426, 267)
        Me.btnServ3Dec.Margin = New System.Windows.Forms.Padding(2)
        Me.btnServ3Dec.Name = "btnServ3Dec"
        Me.btnServ3Dec.Size = New System.Drawing.Size(51, 26)
        Me.btnServ3Dec.TabIndex = 43
        Me.btnServ3Dec.Text = "-"
        Me.btnServ3Dec.UseVisualStyleBackColor = True
        '
        'tbCtrl1
        '
        Me.tbCtrl1.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbCtrl1.AutoSize = False
        Me.tbCtrl1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.tbCtrl1.Enabled = False
        Me.tbCtrl1.LargeChange = 10
        Me.tbCtrl1.Location = New System.Drawing.Point(168, 28)
        Me.tbCtrl1.Maximum = 36000
        Me.tbCtrl1.Minimum = -36000
        Me.tbCtrl1.Name = "tbCtrl1"
        Me.tbCtrl1.Size = New System.Drawing.Size(253, 24)
        Me.tbCtrl1.TabIndex = 0
        Me.tbCtrl1.TickStyle = System.Windows.Forms.TickStyle.None
        '
        'tbCtrl2
        '
        Me.tbCtrl2.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbCtrl2.AutoSize = False
        Me.tbCtrl2.Enabled = False
        Me.tbCtrl2.LargeChange = 10
        Me.tbCtrl2.Location = New System.Drawing.Point(168, 58)
        Me.tbCtrl2.Maximum = 36000
        Me.tbCtrl2.Minimum = -36000
        Me.tbCtrl2.Name = "tbCtrl2"
        Me.tbCtrl2.Size = New System.Drawing.Size(253, 24)
        Me.tbCtrl2.TabIndex = 5
        Me.tbCtrl2.TickStyle = System.Windows.Forms.TickStyle.None
        '
        'tbCtrl3
        '
        Me.tbCtrl3.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbCtrl3.AutoSize = False
        Me.tbCtrl3.Enabled = False
        Me.tbCtrl3.LargeChange = 10
        Me.tbCtrl3.Location = New System.Drawing.Point(168, 88)
        Me.tbCtrl3.Maximum = 36000
        Me.tbCtrl3.Minimum = -36000
        Me.tbCtrl3.Name = "tbCtrl3"
        Me.tbCtrl3.Size = New System.Drawing.Size(253, 24)
        Me.tbCtrl3.TabIndex = 18
        Me.tbCtrl3.TickStyle = System.Windows.Forms.TickStyle.None
        '
        'tbCtrl4
        '
        Me.tbCtrl4.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbCtrl4.AutoSize = False
        Me.tbCtrl4.Enabled = False
        Me.tbCtrl4.LargeChange = 10
        Me.tbCtrl4.Location = New System.Drawing.Point(168, 118)
        Me.tbCtrl4.Maximum = 36000
        Me.tbCtrl4.Minimum = -36000
        Me.tbCtrl4.Name = "tbCtrl4"
        Me.tbCtrl4.Size = New System.Drawing.Size(253, 24)
        Me.tbCtrl4.TabIndex = 19
        Me.tbCtrl4.TickStyle = System.Windows.Forms.TickStyle.None
        '
        'tbCtrl5
        '
        Me.tbCtrl5.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbCtrl5.AutoSize = False
        Me.tbCtrl5.Enabled = False
        Me.tbCtrl5.LargeChange = 10
        Me.tbCtrl5.Location = New System.Drawing.Point(168, 148)
        Me.tbCtrl5.Maximum = 36000
        Me.tbCtrl5.Minimum = -36000
        Me.tbCtrl5.Name = "tbCtrl5"
        Me.tbCtrl5.Size = New System.Drawing.Size(253, 24)
        Me.tbCtrl5.TabIndex = 20
        Me.tbCtrl5.TickStyle = System.Windows.Forms.TickStyle.None
        '
        'tbCtrl6
        '
        Me.tbCtrl6.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbCtrl6.AutoSize = False
        Me.tbCtrl6.Enabled = False
        Me.tbCtrl6.LargeChange = 10
        Me.tbCtrl6.Location = New System.Drawing.Point(168, 178)
        Me.tbCtrl6.Maximum = 36000
        Me.tbCtrl6.Minimum = -36000
        Me.tbCtrl6.Name = "tbCtrl6"
        Me.tbCtrl6.Size = New System.Drawing.Size(253, 24)
        Me.tbCtrl6.TabIndex = 21
        Me.tbCtrl6.TickStyle = System.Windows.Forms.TickStyle.None
        '
        'tbServ1
        '
        Me.tbServ1.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbServ1.AutoSize = False
        Me.tbServ1.Enabled = False
        Me.tbServ1.LargeChange = 10
        Me.tbServ1.Location = New System.Drawing.Point(168, 208)
        Me.tbServ1.Maximum = 10000
        Me.tbServ1.Name = "tbServ1"
        Me.tbServ1.Size = New System.Drawing.Size(253, 24)
        Me.tbServ1.TabIndex = 38
        Me.tbServ1.TickStyle = System.Windows.Forms.TickStyle.None
        '
        'tbServ2
        '
        Me.tbServ2.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbServ2.AutoSize = False
        Me.tbServ2.Enabled = False
        Me.tbServ2.LargeChange = 10
        Me.tbServ2.Location = New System.Drawing.Point(168, 238)
        Me.tbServ2.Maximum = 10000
        Me.tbServ2.Name = "tbServ2"
        Me.tbServ2.Size = New System.Drawing.Size(253, 24)
        Me.tbServ2.TabIndex = 39
        Me.tbServ2.TickStyle = System.Windows.Forms.TickStyle.None
        '
        'tbServ3
        '
        Me.tbServ3.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbServ3.AutoSize = False
        Me.tbServ3.Enabled = False
        Me.tbServ3.LargeChange = 10
        Me.tbServ3.Location = New System.Drawing.Point(168, 268)
        Me.tbServ3.Maximum = 10000
        Me.tbServ3.Name = "tbServ3"
        Me.tbServ3.Size = New System.Drawing.Size(253, 24)
        Me.tbServ3.TabIndex = 40
        Me.tbServ3.TickStyle = System.Windows.Forms.TickStyle.None
        '
        'lblCtrl1Unit
        '
        Me.lblCtrl1Unit.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCtrl1Unit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCtrl1Unit.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblCtrl1Unit.Location = New System.Drawing.Point(128, 28)
        Me.lblCtrl1Unit.Margin = New System.Windows.Forms.Padding(3)
        Me.lblCtrl1Unit.Name = "lblCtrl1Unit"
        Me.lblCtrl1Unit.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblCtrl1Unit.Size = New System.Drawing.Size(34, 24)
        Me.lblCtrl1Unit.TabIndex = 47
        Me.lblCtrl1Unit.Text = "°"
        Me.lblCtrl1Unit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCtrl2Unit
        '
        Me.lblCtrl2Unit.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCtrl2Unit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCtrl2Unit.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblCtrl2Unit.Location = New System.Drawing.Point(128, 58)
        Me.lblCtrl2Unit.Margin = New System.Windows.Forms.Padding(3)
        Me.lblCtrl2Unit.Name = "lblCtrl2Unit"
        Me.lblCtrl2Unit.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblCtrl2Unit.Size = New System.Drawing.Size(34, 24)
        Me.lblCtrl2Unit.TabIndex = 48
        Me.lblCtrl2Unit.Text = "°"
        Me.lblCtrl2Unit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCtrl3Unit
        '
        Me.lblCtrl3Unit.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCtrl3Unit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCtrl3Unit.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblCtrl3Unit.Location = New System.Drawing.Point(128, 88)
        Me.lblCtrl3Unit.Margin = New System.Windows.Forms.Padding(3)
        Me.lblCtrl3Unit.Name = "lblCtrl3Unit"
        Me.lblCtrl3Unit.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblCtrl3Unit.Size = New System.Drawing.Size(34, 24)
        Me.lblCtrl3Unit.TabIndex = 49
        Me.lblCtrl3Unit.Text = "°"
        Me.lblCtrl3Unit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCtrl4Unit
        '
        Me.lblCtrl4Unit.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCtrl4Unit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCtrl4Unit.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblCtrl4Unit.Location = New System.Drawing.Point(128, 118)
        Me.lblCtrl4Unit.Margin = New System.Windows.Forms.Padding(3)
        Me.lblCtrl4Unit.Name = "lblCtrl4Unit"
        Me.lblCtrl4Unit.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblCtrl4Unit.Size = New System.Drawing.Size(34, 24)
        Me.lblCtrl4Unit.TabIndex = 50
        Me.lblCtrl4Unit.Text = "°"
        Me.lblCtrl4Unit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCtrl5Unit
        '
        Me.lblCtrl5Unit.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCtrl5Unit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCtrl5Unit.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblCtrl5Unit.Location = New System.Drawing.Point(128, 148)
        Me.lblCtrl5Unit.Margin = New System.Windows.Forms.Padding(3)
        Me.lblCtrl5Unit.Name = "lblCtrl5Unit"
        Me.lblCtrl5Unit.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblCtrl5Unit.Size = New System.Drawing.Size(34, 24)
        Me.lblCtrl5Unit.TabIndex = 51
        Me.lblCtrl5Unit.Text = "°"
        Me.lblCtrl5Unit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCtrl6Unit
        '
        Me.lblCtrl6Unit.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCtrl6Unit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCtrl6Unit.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblCtrl6Unit.Location = New System.Drawing.Point(128, 178)
        Me.lblCtrl6Unit.Margin = New System.Windows.Forms.Padding(3)
        Me.lblCtrl6Unit.Name = "lblCtrl6Unit"
        Me.lblCtrl6Unit.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblCtrl6Unit.Size = New System.Drawing.Size(34, 24)
        Me.lblCtrl6Unit.TabIndex = 52
        Me.lblCtrl6Unit.Text = "°"
        Me.lblCtrl6Unit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSrv1Unit
        '
        Me.lblSrv1Unit.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSrv1Unit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSrv1Unit.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblSrv1Unit.Location = New System.Drawing.Point(128, 208)
        Me.lblSrv1Unit.Margin = New System.Windows.Forms.Padding(3)
        Me.lblSrv1Unit.Name = "lblSrv1Unit"
        Me.lblSrv1Unit.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblSrv1Unit.Size = New System.Drawing.Size(34, 24)
        Me.lblSrv1Unit.TabIndex = 53
        Me.lblSrv1Unit.Text = "%"
        Me.lblSrv1Unit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSrv2Unit
        '
        Me.lblSrv2Unit.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSrv2Unit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSrv2Unit.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblSrv2Unit.Location = New System.Drawing.Point(128, 238)
        Me.lblSrv2Unit.Margin = New System.Windows.Forms.Padding(3)
        Me.lblSrv2Unit.Name = "lblSrv2Unit"
        Me.lblSrv2Unit.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblSrv2Unit.Size = New System.Drawing.Size(34, 24)
        Me.lblSrv2Unit.TabIndex = 54
        Me.lblSrv2Unit.Text = "%"
        Me.lblSrv2Unit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSrv3Unit
        '
        Me.lblSrv3Unit.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSrv3Unit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSrv3Unit.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblSrv3Unit.Location = New System.Drawing.Point(128, 268)
        Me.lblSrv3Unit.Margin = New System.Windows.Forms.Padding(3)
        Me.lblSrv3Unit.Name = "lblSrv3Unit"
        Me.lblSrv3Unit.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblSrv3Unit.Size = New System.Drawing.Size(34, 24)
        Me.lblSrv3Unit.TabIndex = 55
        Me.lblSrv3Unit.Text = "%"
        Me.lblSrv3Unit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblJog
        '
        Me.lblJog.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel.SetColumnSpan(Me.lblJog, 2)
        Me.lblJog.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJog.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblJog.Location = New System.Drawing.Point(427, 3)
        Me.lblJog.Margin = New System.Windows.Forms.Padding(3)
        Me.lblJog.Name = "lblJog"
        Me.lblJog.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblJog.Size = New System.Drawing.Size(106, 19)
        Me.lblJog.TabIndex = 31
        Me.lblJog.Text = "Jog"
        Me.lblJog.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ToolStrip2
        '
        Me.ToolStrip2.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblMode, Me.cbJointOrTCP, Me.ToolStripSeparator3, Me.lblSpeed, Me.numSpeed, Me.ToolStripSeparator1, Me.lblAcc, Me.numAcc})
        Me.ToolStrip2.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.Size = New System.Drawing.Size(536, 26)
        Me.ToolStrip2.Stretch = True
        Me.ToolStrip2.TabIndex = 1
        '
        'lblMode
        '
        Me.lblMode.Name = "lblMode"
        Me.lblMode.Size = New System.Drawing.Size(47, 23)
        Me.lblMode.Text = "Modus:"
        '
        'cbJointOrTCP
        '
        Me.cbJointOrTCP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbJointOrTCP.Items.AddRange(New Object() {"Joint", "TCP"})
        Me.cbJointOrTCP.Name = "cbJointOrTCP"
        Me.cbJointOrTCP.Size = New System.Drawing.Size(75, 26)
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 26)
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
        Me.numSpeed.Maximum = New Decimal(New Integer() {100, 0, 0, 0})
        Me.numSpeed.Minimum = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numSpeed.Name = "numSpeed"
        Me.numSpeed.Size = New System.Drawing.Size(56, 23)
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
        Me.numAcc.Maximum = New Decimal(New Integer() {100, 0, 0, 0})
        Me.numAcc.Minimum = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numAcc.Name = "numAcc"
        Me.numAcc.Size = New System.Drawing.Size(56, 23)
        Me.numAcc.Text = "25,00"
        Me.numAcc.Value = New Decimal(New Integer() {2500, 0, 0, 131072})
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblMove, Me.cbMoveMode, Me.btnMoveStart, Me.ToolStripSeparator2, Me.lblJogInterval, Me.numJogInterval1, Me.lblUnitMm, Me.numJogInterval2, Me.lblUnitDeg, Me.cbJogMode})
        Me.ToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 26)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(536, 26)
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
        Me.btnMoveStart.Enabled = False
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
        'numJogInterval1
        '
        Me.numJogInterval1.DecimalPlaces = 1
        Me.numJogInterval1.Maximum = New Decimal(New Integer() {100, 0, 0, 0})
        Me.numJogInterval1.Minimum = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.numJogInterval1.Name = "numJogInterval1"
        Me.numJogInterval1.Size = New System.Drawing.Size(50, 23)
        Me.numJogInterval1.Text = "5,0"
        Me.numJogInterval1.Value = New Decimal(New Integer() {50, 0, 0, 65536})
        '
        'lblUnitMm
        '
        Me.lblUnitMm.Name = "lblUnitMm"
        Me.lblUnitMm.Size = New System.Drawing.Size(29, 23)
        Me.lblUnitMm.Text = "mm"
        Me.lblUnitMm.Visible = False
        '
        'numJogInterval2
        '
        Me.numJogInterval2.DecimalPlaces = 1
        Me.numJogInterval2.Maximum = New Decimal(New Integer() {100, 0, 0, 0})
        Me.numJogInterval2.Minimum = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.numJogInterval2.Name = "numJogInterval2"
        Me.numJogInterval2.Size = New System.Drawing.Size(50, 23)
        Me.numJogInterval2.Text = "10,0"
        Me.numJogInterval2.Value = New Decimal(New Integer() {100, 0, 0, 65536})
        Me.numJogInterval2.Visible = False
        '
        'lblUnitDeg
        '
        Me.lblUnitDeg.Name = "lblUnitDeg"
        Me.lblUnitDeg.Size = New System.Drawing.Size(12, 23)
        Me.lblUnitDeg.Text = "°"
        Me.lblUnitDeg.Visible = False
        '
        'cbJogMode
        '
        Me.cbJogMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbJogMode.Items.AddRange(New Object() {"Grad", "Steps"})
        Me.cbJogMode.Name = "cbJogMode"
        Me.cbJogMode.Size = New System.Drawing.Size(75, 26)
        '
        'panCtrl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(536, 410)
        Me.Controls.Add(Me.ToolStripContainer)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "panCtrl"
        Me.Text = "Roboter Steuerung"
        Me.ToolStripContainer.ContentPanel.ResumeLayout(False)
        Me.ToolStripContainer.TopToolStripPanel.ResumeLayout(False)
        Me.ToolStripContainer.TopToolStripPanel.PerformLayout()
        Me.ToolStripContainer.ResumeLayout(False)
        Me.ToolStripContainer.PerformLayout()
        Me.TableLayoutPanel.ResumeLayout(False)
        CType(Me.numCtrl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numCtrl2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numCtrl3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numCtrl4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numCtrl5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numCtrl6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numServ1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numServ2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numServ3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbCtrl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbCtrl2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbCtrl3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbCtrl4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbCtrl5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbCtrl6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbServ1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbServ2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbServ3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip2.ResumeLayout(False)
        Me.ToolStrip2.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ToolStripContainer As ToolStripContainer
    Friend WithEvents TableLayoutPanel As TableLayoutPanel
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents tbCtrl1 As TrackBar
    Friend WithEvents lblCtrl1 As Label
    Friend WithEvents btnCtrl1Dec As Button
    Friend WithEvents btnCtrl1Inc As Button
    Friend WithEvents numCtrl1 As NumericUpDown
    Friend WithEvents ToolStripComboBox1 As ToolStripComboBox
    Friend WithEvents cbJogMode As ToolStripComboBox
    Friend WithEvents lblJogInterval As ToolStripLabel
    Friend WithEvents btnCtrl2Inc As Button
    Friend WithEvents btnCtrl2Dec As Button
    Friend WithEvents tbCtrl2 As TrackBar
    Friend WithEvents numCtrl2 As NumericUpDown
    Friend WithEvents lblCtrl2 As Label
    Friend WithEvents lblCtrl3 As Label
    Friend WithEvents lblCtrl4 As Label
    Friend WithEvents lblCtrl5 As Label
    Friend WithEvents lblCtrl6 As Label
    Friend WithEvents numCtrl3 As NumericUpDown
    Friend WithEvents numCtrl4 As NumericUpDown
    Friend WithEvents numCtrl5 As NumericUpDown
    Friend WithEvents numCtrl6 As NumericUpDown
    Friend WithEvents tbCtrl3 As TrackBar
    Friend WithEvents tbCtrl4 As TrackBar
    Friend WithEvents tbCtrl5 As TrackBar
    Friend WithEvents tbCtrl6 As TrackBar
    Friend WithEvents btnCtrl3Dec As Button
    Friend WithEvents btnCtrl4Dec As Button
    Friend WithEvents btnCtrl5Dec As Button
    Friend WithEvents btnCtrl6Dec As Button
    Friend WithEvents btnCtrl3Inc As Button
    Friend WithEvents btnCtrl4Inc As Button
    Friend WithEvents btnCtrl5Inc As Button
    Friend WithEvents btnCtrl6Inc As Button
    Friend WithEvents lblTargetAngle As Label
    Friend WithEvents lblJog As Label
    Friend WithEvents numJogInterval1 As ToolStripNumericUpDown
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
    Friend WithEvents lblMode As ToolStripLabel
    Friend WithEvents cbJointOrTCP As ToolStripComboBox
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents lblUnitMm As ToolStripLabel
    Friend WithEvents numJogInterval2 As ToolStripNumericUpDown
    Friend WithEvents lblUnitDeg As ToolStripLabel
    Friend WithEvents lblServo1 As Label
    Friend WithEvents lblServo2 As Label
    Friend WithEvents lblServo3 As Label
    Friend WithEvents numServ1 As NumericUpDown
    Friend WithEvents numServ2 As NumericUpDown
    Friend WithEvents numServ3 As NumericUpDown
    Friend WithEvents tbServ1 As TrackBar
    Friend WithEvents tbServ2 As TrackBar
    Friend WithEvents tbServ3 As TrackBar
    Friend WithEvents btnServ1Dec As Button
    Friend WithEvents btnServ2Dec As Button
    Friend WithEvents btnServ3Dec As Button
    Friend WithEvents btnServ1Inc As Button
    Friend WithEvents btnServ2Inc As Button
    Friend WithEvents btnServ3Inc As Button
    Friend WithEvents lblCtrl1Unit As Label
    Friend WithEvents lblCtrl2Unit As Label
    Friend WithEvents lblCtrl3Unit As Label
    Friend WithEvents lblCtrl4Unit As Label
    Friend WithEvents lblCtrl5Unit As Label
    Friend WithEvents lblCtrl6Unit As Label
    Friend WithEvents lblSrv1Unit As Label
    Friend WithEvents lblSrv2Unit As Label
    Friend WithEvents lblSrv3Unit As Label
End Class
