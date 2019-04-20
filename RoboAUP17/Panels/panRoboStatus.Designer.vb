<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class panRoboStatus
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
        Me.lblCoordsHeading = New System.Windows.Forms.Label()
        Me.lblRollHeading = New System.Windows.Forms.Label()
        Me.lblPitchHeading = New System.Windows.Forms.Label()
        Me.lblYawHeading = New System.Windows.Forms.Label()
        Me.lblZHeading = New System.Windows.Forms.Label()
        Me.lblYHeading = New System.Windows.Forms.Label()
        Me.lblXHeading = New System.Windows.Forms.Label()
        Me.lblJointsHeading = New System.Windows.Forms.Label()
        Me.lblJ1Heading = New System.Windows.Forms.Label()
        Me.TableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.lblJ3Heading = New System.Windows.Forms.Label()
        Me.lblJ3HeadingImg = New System.Windows.Forms.Label()
        Me.lblJ2Heading = New System.Windows.Forms.Label()
        Me.lblJ2HeadingImg = New System.Windows.Forms.Label()
        Me.lblJ1HeadingImg = New System.Windows.Forms.Label()
        Me.lblJ4Heading = New System.Windows.Forms.Label()
        Me.lblJ4HeadingImg = New System.Windows.Forms.Label()
        Me.lblJ5Heading = New System.Windows.Forms.Label()
        Me.lblJ6Heading = New System.Windows.Forms.Label()
        Me.lblJ5HeadingImg = New System.Windows.Forms.Label()
        Me.lblJ6HeadingImg = New System.Windows.Forms.Label()
        Me.lblJ1Val = New System.Windows.Forms.Label()
        Me.lblXVal = New System.Windows.Forms.Label()
        Me.lblLimitSwitchesHeading = New System.Windows.Forms.Label()
        Me.lblLimitJ1Heading = New System.Windows.Forms.Label()
        Me.lblLimitJ2Heading = New System.Windows.Forms.Label()
        Me.lblLimitJ3Heading = New System.Windows.Forms.Label()
        Me.lblLimitJ4Heading = New System.Windows.Forms.Label()
        Me.lblLimitJ5Heading = New System.Windows.Forms.Label()
        Me.lblLimitJ6Heading = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.lblESSHeading = New System.Windows.Forms.Label()
        Me.lblJ1LSState = New System.Windows.Forms.Label()
        Me.lblRS232Heading = New System.Windows.Forms.Label()
        Me.lblESState = New System.Windows.Forms.Label()
        Me.lblSerialState = New System.Windows.Forms.Label()
        Me.lblJ2Val = New System.Windows.Forms.Label()
        Me.lblJ3Val = New System.Windows.Forms.Label()
        Me.lblJ4Val = New System.Windows.Forms.Label()
        Me.lblJ5Val = New System.Windows.Forms.Label()
        Me.lblJ6Val = New System.Windows.Forms.Label()
        Me.lblYVal = New System.Windows.Forms.Label()
        Me.lblZVal = New System.Windows.Forms.Label()
        Me.lblYawVal = New System.Windows.Forms.Label()
        Me.lblPitchVal = New System.Windows.Forms.Label()
        Me.lblRollVal = New System.Windows.Forms.Label()
        Me.lblJ2LSState = New System.Windows.Forms.Label()
        Me.lblJ3LSState = New System.Windows.Forms.Label()
        Me.lblJ4LSState = New System.Windows.Forms.Label()
        Me.lblJ5LSState = New System.Windows.Forms.Label()
        Me.lblJ6LSState = New System.Windows.Forms.Label()
        Me.TableLayoutPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblCoordsHeading
        '
        Me.lblCoordsHeading.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCoordsHeading.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TableLayoutPanel.SetColumnSpan(Me.lblCoordsHeading, 12)
        Me.lblCoordsHeading.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCoordsHeading.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblCoordsHeading.Location = New System.Drawing.Point(3, 75)
        Me.lblCoordsHeading.Name = "lblCoordsHeading"
        Me.lblCoordsHeading.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblCoordsHeading.Size = New System.Drawing.Size(493, 25)
        Me.lblCoordsHeading.TabIndex = 15
        Me.lblCoordsHeading.Text = "aktuelle Koordinaten"
        Me.lblCoordsHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblRollHeading
        '
        Me.lblRollHeading.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel.SetColumnSpan(Me.lblRollHeading, 2)
        Me.lblRollHeading.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRollHeading.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblRollHeading.Location = New System.Drawing.Point(411, 100)
        Me.lblRollHeading.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.lblRollHeading.Name = "lblRollHeading"
        Me.lblRollHeading.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblRollHeading.Size = New System.Drawing.Size(87, 25)
        Me.lblRollHeading.TabIndex = 14
        Me.lblRollHeading.Text = "Roll"
        Me.lblRollHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPitchHeading
        '
        Me.lblPitchHeading.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel.SetColumnSpan(Me.lblPitchHeading, 2)
        Me.lblPitchHeading.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPitchHeading.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblPitchHeading.Location = New System.Drawing.Point(329, 100)
        Me.lblPitchHeading.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.lblPitchHeading.Name = "lblPitchHeading"
        Me.lblPitchHeading.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblPitchHeading.Size = New System.Drawing.Size(80, 25)
        Me.lblPitchHeading.TabIndex = 13
        Me.lblPitchHeading.Text = "Pitch"
        Me.lblPitchHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblYawHeading
        '
        Me.lblYawHeading.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel.SetColumnSpan(Me.lblYawHeading, 2)
        Me.lblYawHeading.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblYawHeading.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblYawHeading.Location = New System.Drawing.Point(247, 100)
        Me.lblYawHeading.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.lblYawHeading.Name = "lblYawHeading"
        Me.lblYawHeading.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblYawHeading.Size = New System.Drawing.Size(80, 25)
        Me.lblYawHeading.TabIndex = 12
        Me.lblYawHeading.Text = "Yaw"
        Me.lblYawHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblZHeading
        '
        Me.lblZHeading.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel.SetColumnSpan(Me.lblZHeading, 2)
        Me.lblZHeading.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblZHeading.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblZHeading.Location = New System.Drawing.Point(165, 100)
        Me.lblZHeading.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.lblZHeading.Name = "lblZHeading"
        Me.lblZHeading.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblZHeading.Size = New System.Drawing.Size(80, 25)
        Me.lblZHeading.TabIndex = 11
        Me.lblZHeading.Text = "Z [mm]"
        Me.lblZHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblYHeading
        '
        Me.lblYHeading.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel.SetColumnSpan(Me.lblYHeading, 2)
        Me.lblYHeading.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblYHeading.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblYHeading.Location = New System.Drawing.Point(83, 100)
        Me.lblYHeading.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.lblYHeading.Name = "lblYHeading"
        Me.lblYHeading.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblYHeading.Size = New System.Drawing.Size(80, 25)
        Me.lblYHeading.TabIndex = 10
        Me.lblYHeading.Text = "Y [mm]"
        Me.lblYHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblXHeading
        '
        Me.lblXHeading.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel.SetColumnSpan(Me.lblXHeading, 2)
        Me.lblXHeading.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblXHeading.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblXHeading.Location = New System.Drawing.Point(1, 100)
        Me.lblXHeading.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.lblXHeading.Name = "lblXHeading"
        Me.lblXHeading.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblXHeading.Size = New System.Drawing.Size(80, 25)
        Me.lblXHeading.TabIndex = 9
        Me.lblXHeading.Text = "X [mm]"
        Me.lblXHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblJointsHeading
        '
        Me.lblJointsHeading.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblJointsHeading.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TableLayoutPanel.SetColumnSpan(Me.lblJointsHeading, 12)
        Me.lblJointsHeading.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJointsHeading.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblJointsHeading.Location = New System.Drawing.Point(3, 0)
        Me.lblJointsHeading.Name = "lblJointsHeading"
        Me.lblJointsHeading.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblJointsHeading.Size = New System.Drawing.Size(493, 25)
        Me.lblJointsHeading.TabIndex = 2
        Me.lblJointsHeading.Text = "Joints"
        Me.lblJointsHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblJ1Heading
        '
        Me.lblJ1Heading.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblJ1Heading.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJ1Heading.Location = New System.Drawing.Point(1, 25)
        Me.lblJ1Heading.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.lblJ1Heading.Name = "lblJ1Heading"
        Me.lblJ1Heading.Size = New System.Drawing.Size(40, 25)
        Me.lblJ1Heading.TabIndex = 3
        Me.lblJ1Heading.Text = "J1"
        Me.lblJ1Heading.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TableLayoutPanel
        '
        Me.TableLayoutPanel.AutoScroll = True
        Me.TableLayoutPanel.AutoScrollMinSize = New System.Drawing.Size(400, 0)
        Me.TableLayoutPanel.ColumnCount = 12
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.333333!))
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.333333!))
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.333333!))
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.333333!))
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.333333!))
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.333333!))
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.333333!))
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.333333!))
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.333333!))
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.333333!))
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.333333!))
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.333333!))
        Me.TableLayoutPanel.Controls.Add(Me.lblJ3Heading, 4, 1)
        Me.TableLayoutPanel.Controls.Add(Me.lblJ3HeadingImg, 5, 1)
        Me.TableLayoutPanel.Controls.Add(Me.lblJ2Heading, 2, 1)
        Me.TableLayoutPanel.Controls.Add(Me.lblJ2HeadingImg, 3, 1)
        Me.TableLayoutPanel.Controls.Add(Me.lblJointsHeading, 0, 0)
        Me.TableLayoutPanel.Controls.Add(Me.lblXHeading, 0, 4)
        Me.TableLayoutPanel.Controls.Add(Me.lblYHeading, 2, 4)
        Me.TableLayoutPanel.Controls.Add(Me.lblZHeading, 4, 4)
        Me.TableLayoutPanel.Controls.Add(Me.lblYawHeading, 6, 4)
        Me.TableLayoutPanel.Controls.Add(Me.lblPitchHeading, 8, 4)
        Me.TableLayoutPanel.Controls.Add(Me.lblRollHeading, 10, 4)
        Me.TableLayoutPanel.Controls.Add(Me.lblCoordsHeading, 0, 3)
        Me.TableLayoutPanel.Controls.Add(Me.lblJ1Heading, 0, 1)
        Me.TableLayoutPanel.Controls.Add(Me.lblJ1HeadingImg, 1, 1)
        Me.TableLayoutPanel.Controls.Add(Me.lblJ4Heading, 6, 1)
        Me.TableLayoutPanel.Controls.Add(Me.lblJ4HeadingImg, 7, 1)
        Me.TableLayoutPanel.Controls.Add(Me.lblJ5Heading, 8, 1)
        Me.TableLayoutPanel.Controls.Add(Me.lblJ6Heading, 10, 1)
        Me.TableLayoutPanel.Controls.Add(Me.lblJ5HeadingImg, 9, 1)
        Me.TableLayoutPanel.Controls.Add(Me.lblJ6HeadingImg, 11, 1)
        Me.TableLayoutPanel.Controls.Add(Me.lblJ1Val, 0, 2)
        Me.TableLayoutPanel.Controls.Add(Me.lblXVal, 0, 5)
        Me.TableLayoutPanel.Controls.Add(Me.lblLimitSwitchesHeading, 0, 6)
        Me.TableLayoutPanel.Controls.Add(Me.lblLimitJ1Heading, 0, 7)
        Me.TableLayoutPanel.Controls.Add(Me.lblLimitJ2Heading, 2, 7)
        Me.TableLayoutPanel.Controls.Add(Me.lblLimitJ3Heading, 4, 7)
        Me.TableLayoutPanel.Controls.Add(Me.lblLimitJ4Heading, 6, 7)
        Me.TableLayoutPanel.Controls.Add(Me.lblLimitJ5Heading, 8, 7)
        Me.TableLayoutPanel.Controls.Add(Me.lblLimitJ6Heading, 10, 7)
        Me.TableLayoutPanel.Controls.Add(Me.Label18, 0, 8)
        Me.TableLayoutPanel.Controls.Add(Me.lblESSHeading, 0, 9)
        Me.TableLayoutPanel.Controls.Add(Me.lblJ1LSState, 1, 7)
        Me.TableLayoutPanel.Controls.Add(Me.lblRS232Heading, 4, 9)
        Me.TableLayoutPanel.Controls.Add(Me.lblESState, 2, 9)
        Me.TableLayoutPanel.Controls.Add(Me.lblSerialState, 6, 9)
        Me.TableLayoutPanel.Controls.Add(Me.lblJ2Val, 2, 2)
        Me.TableLayoutPanel.Controls.Add(Me.lblJ3Val, 4, 2)
        Me.TableLayoutPanel.Controls.Add(Me.lblJ4Val, 6, 2)
        Me.TableLayoutPanel.Controls.Add(Me.lblJ5Val, 8, 2)
        Me.TableLayoutPanel.Controls.Add(Me.lblJ6Val, 10, 2)
        Me.TableLayoutPanel.Controls.Add(Me.lblYVal, 2, 5)
        Me.TableLayoutPanel.Controls.Add(Me.lblZVal, 4, 5)
        Me.TableLayoutPanel.Controls.Add(Me.lblYawVal, 6, 5)
        Me.TableLayoutPanel.Controls.Add(Me.lblPitchVal, 8, 5)
        Me.TableLayoutPanel.Controls.Add(Me.lblRollVal, 10, 5)
        Me.TableLayoutPanel.Controls.Add(Me.lblJ2LSState, 3, 7)
        Me.TableLayoutPanel.Controls.Add(Me.lblJ3LSState, 5, 7)
        Me.TableLayoutPanel.Controls.Add(Me.lblJ4LSState, 7, 7)
        Me.TableLayoutPanel.Controls.Add(Me.lblJ5LSState, 9, 7)
        Me.TableLayoutPanel.Controls.Add(Me.lblJ6LSState, 11, 7)
        Me.TableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel.Name = "TableLayoutPanel"
        Me.TableLayoutPanel.RowCount = 11
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel.Size = New System.Drawing.Size(499, 467)
        Me.TableLayoutPanel.TabIndex = 0
        '
        'lblJ3Heading
        '
        Me.lblJ3Heading.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblJ3Heading.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJ3Heading.Location = New System.Drawing.Point(165, 25)
        Me.lblJ3Heading.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.lblJ3Heading.Name = "lblJ3Heading"
        Me.lblJ3Heading.Size = New System.Drawing.Size(40, 25)
        Me.lblJ3Heading.TabIndex = 19
        Me.lblJ3Heading.Text = "J3"
        Me.lblJ3Heading.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblJ3HeadingImg
        '
        Me.lblJ3HeadingImg.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblJ3HeadingImg.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJ3HeadingImg.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblJ3HeadingImg.Location = New System.Drawing.Point(205, 25)
        Me.lblJ3HeadingImg.Margin = New System.Windows.Forms.Padding(0, 0, 1, 0)
        Me.lblJ3HeadingImg.Name = "lblJ3HeadingImg"
        Me.lblJ3HeadingImg.Size = New System.Drawing.Size(40, 25)
        Me.lblJ3HeadingImg.TabIndex = 20
        Me.lblJ3HeadingImg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblJ2Heading
        '
        Me.lblJ2Heading.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblJ2Heading.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJ2Heading.Location = New System.Drawing.Point(83, 25)
        Me.lblJ2Heading.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.lblJ2Heading.Name = "lblJ2Heading"
        Me.lblJ2Heading.Size = New System.Drawing.Size(40, 25)
        Me.lblJ2Heading.TabIndex = 17
        Me.lblJ2Heading.Text = "J2"
        Me.lblJ2Heading.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblJ2HeadingImg
        '
        Me.lblJ2HeadingImg.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblJ2HeadingImg.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJ2HeadingImg.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblJ2HeadingImg.Location = New System.Drawing.Point(123, 25)
        Me.lblJ2HeadingImg.Margin = New System.Windows.Forms.Padding(0, 0, 1, 0)
        Me.lblJ2HeadingImg.Name = "lblJ2HeadingImg"
        Me.lblJ2HeadingImg.Size = New System.Drawing.Size(40, 25)
        Me.lblJ2HeadingImg.TabIndex = 18
        Me.lblJ2HeadingImg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblJ1HeadingImg
        '
        Me.lblJ1HeadingImg.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblJ1HeadingImg.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJ1HeadingImg.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblJ1HeadingImg.Location = New System.Drawing.Point(41, 25)
        Me.lblJ1HeadingImg.Margin = New System.Windows.Forms.Padding(0, 0, 1, 0)
        Me.lblJ1HeadingImg.Name = "lblJ1HeadingImg"
        Me.lblJ1HeadingImg.Size = New System.Drawing.Size(40, 25)
        Me.lblJ1HeadingImg.TabIndex = 16
        Me.lblJ1HeadingImg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblJ4Heading
        '
        Me.lblJ4Heading.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblJ4Heading.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJ4Heading.Location = New System.Drawing.Point(247, 25)
        Me.lblJ4Heading.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.lblJ4Heading.Name = "lblJ4Heading"
        Me.lblJ4Heading.Size = New System.Drawing.Size(40, 25)
        Me.lblJ4Heading.TabIndex = 21
        Me.lblJ4Heading.Text = "J4"
        Me.lblJ4Heading.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblJ4HeadingImg
        '
        Me.lblJ4HeadingImg.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblJ4HeadingImg.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJ4HeadingImg.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblJ4HeadingImg.Location = New System.Drawing.Point(287, 25)
        Me.lblJ4HeadingImg.Margin = New System.Windows.Forms.Padding(0, 0, 1, 0)
        Me.lblJ4HeadingImg.Name = "lblJ4HeadingImg"
        Me.lblJ4HeadingImg.Size = New System.Drawing.Size(40, 25)
        Me.lblJ4HeadingImg.TabIndex = 22
        Me.lblJ4HeadingImg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblJ5Heading
        '
        Me.lblJ5Heading.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblJ5Heading.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJ5Heading.Location = New System.Drawing.Point(329, 25)
        Me.lblJ5Heading.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.lblJ5Heading.Name = "lblJ5Heading"
        Me.lblJ5Heading.Size = New System.Drawing.Size(40, 25)
        Me.lblJ5Heading.TabIndex = 23
        Me.lblJ5Heading.Text = "J5"
        Me.lblJ5Heading.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblJ6Heading
        '
        Me.lblJ6Heading.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblJ6Heading.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJ6Heading.Location = New System.Drawing.Point(411, 25)
        Me.lblJ6Heading.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.lblJ6Heading.Name = "lblJ6Heading"
        Me.lblJ6Heading.Size = New System.Drawing.Size(40, 25)
        Me.lblJ6Heading.TabIndex = 24
        Me.lblJ6Heading.Text = "J6"
        Me.lblJ6Heading.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblJ5HeadingImg
        '
        Me.lblJ5HeadingImg.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblJ5HeadingImg.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJ5HeadingImg.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblJ5HeadingImg.Location = New System.Drawing.Point(369, 25)
        Me.lblJ5HeadingImg.Margin = New System.Windows.Forms.Padding(0, 0, 1, 0)
        Me.lblJ5HeadingImg.Name = "lblJ5HeadingImg"
        Me.lblJ5HeadingImg.Size = New System.Drawing.Size(40, 25)
        Me.lblJ5HeadingImg.TabIndex = 25
        Me.lblJ5HeadingImg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblJ6HeadingImg
        '
        Me.lblJ6HeadingImg.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblJ6HeadingImg.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJ6HeadingImg.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblJ6HeadingImg.Location = New System.Drawing.Point(451, 25)
        Me.lblJ6HeadingImg.Margin = New System.Windows.Forms.Padding(0, 0, 1, 0)
        Me.lblJ6HeadingImg.Name = "lblJ6HeadingImg"
        Me.lblJ6HeadingImg.Size = New System.Drawing.Size(47, 25)
        Me.lblJ6HeadingImg.TabIndex = 26
        Me.lblJ6HeadingImg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblJ1Val
        '
        Me.lblJ1Val.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel.SetColumnSpan(Me.lblJ1Val, 2)
        Me.lblJ1Val.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJ1Val.Location = New System.Drawing.Point(1, 51)
        Me.lblJ1Val.Margin = New System.Windows.Forms.Padding(1)
        Me.lblJ1Val.Name = "lblJ1Val"
        Me.lblJ1Val.Size = New System.Drawing.Size(80, 23)
        Me.lblJ1Val.TabIndex = 27
        Me.lblJ1Val.Text = "0 °"
        Me.lblJ1Val.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblXVal
        '
        Me.lblXVal.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel.SetColumnSpan(Me.lblXVal, 2)
        Me.lblXVal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblXVal.Location = New System.Drawing.Point(1, 126)
        Me.lblXVal.Margin = New System.Windows.Forms.Padding(1)
        Me.lblXVal.Name = "lblXVal"
        Me.lblXVal.Size = New System.Drawing.Size(80, 23)
        Me.lblXVal.TabIndex = 28
        Me.lblXVal.Text = "0"
        Me.lblXVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLimitSwitchesHeading
        '
        Me.lblLimitSwitchesHeading.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblLimitSwitchesHeading.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TableLayoutPanel.SetColumnSpan(Me.lblLimitSwitchesHeading, 12)
        Me.lblLimitSwitchesHeading.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLimitSwitchesHeading.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblLimitSwitchesHeading.Location = New System.Drawing.Point(3, 150)
        Me.lblLimitSwitchesHeading.Name = "lblLimitSwitchesHeading"
        Me.lblLimitSwitchesHeading.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblLimitSwitchesHeading.Size = New System.Drawing.Size(493, 25)
        Me.lblLimitSwitchesHeading.TabIndex = 29
        Me.lblLimitSwitchesHeading.Text = "Endschalter"
        Me.lblLimitSwitchesHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLimitJ1Heading
        '
        Me.lblLimitJ1Heading.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblLimitJ1Heading.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLimitJ1Heading.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblLimitJ1Heading.Location = New System.Drawing.Point(1, 175)
        Me.lblLimitJ1Heading.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.lblLimitJ1Heading.Name = "lblLimitJ1Heading"
        Me.lblLimitJ1Heading.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblLimitJ1Heading.Size = New System.Drawing.Size(39, 25)
        Me.lblLimitJ1Heading.TabIndex = 32
        Me.lblLimitJ1Heading.Text = "J1:"
        Me.lblLimitJ1Heading.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLimitJ2Heading
        '
        Me.lblLimitJ2Heading.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblLimitJ2Heading.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLimitJ2Heading.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblLimitJ2Heading.Location = New System.Drawing.Point(83, 175)
        Me.lblLimitJ2Heading.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.lblLimitJ2Heading.Name = "lblLimitJ2Heading"
        Me.lblLimitJ2Heading.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblLimitJ2Heading.Size = New System.Drawing.Size(39, 25)
        Me.lblLimitJ2Heading.TabIndex = 33
        Me.lblLimitJ2Heading.Text = "J2:"
        Me.lblLimitJ2Heading.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLimitJ3Heading
        '
        Me.lblLimitJ3Heading.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblLimitJ3Heading.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLimitJ3Heading.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblLimitJ3Heading.Location = New System.Drawing.Point(165, 175)
        Me.lblLimitJ3Heading.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.lblLimitJ3Heading.Name = "lblLimitJ3Heading"
        Me.lblLimitJ3Heading.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblLimitJ3Heading.Size = New System.Drawing.Size(39, 25)
        Me.lblLimitJ3Heading.TabIndex = 34
        Me.lblLimitJ3Heading.Text = "J3:"
        Me.lblLimitJ3Heading.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLimitJ4Heading
        '
        Me.lblLimitJ4Heading.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblLimitJ4Heading.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLimitJ4Heading.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblLimitJ4Heading.Location = New System.Drawing.Point(247, 175)
        Me.lblLimitJ4Heading.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.lblLimitJ4Heading.Name = "lblLimitJ4Heading"
        Me.lblLimitJ4Heading.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblLimitJ4Heading.Size = New System.Drawing.Size(39, 25)
        Me.lblLimitJ4Heading.TabIndex = 35
        Me.lblLimitJ4Heading.Text = "J4:"
        Me.lblLimitJ4Heading.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLimitJ5Heading
        '
        Me.lblLimitJ5Heading.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblLimitJ5Heading.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLimitJ5Heading.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblLimitJ5Heading.Location = New System.Drawing.Point(329, 175)
        Me.lblLimitJ5Heading.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.lblLimitJ5Heading.Name = "lblLimitJ5Heading"
        Me.lblLimitJ5Heading.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblLimitJ5Heading.Size = New System.Drawing.Size(39, 25)
        Me.lblLimitJ5Heading.TabIndex = 36
        Me.lblLimitJ5Heading.Text = "J5:"
        Me.lblLimitJ5Heading.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLimitJ6Heading
        '
        Me.lblLimitJ6Heading.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblLimitJ6Heading.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLimitJ6Heading.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblLimitJ6Heading.Location = New System.Drawing.Point(411, 175)
        Me.lblLimitJ6Heading.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.lblLimitJ6Heading.Name = "lblLimitJ6Heading"
        Me.lblLimitJ6Heading.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblLimitJ6Heading.Size = New System.Drawing.Size(39, 25)
        Me.lblLimitJ6Heading.TabIndex = 37
        Me.lblLimitJ6Heading.Text = "J6:"
        Me.lblLimitJ6Heading.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label18
        '
        Me.Label18.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TableLayoutPanel.SetColumnSpan(Me.Label18, 12)
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Label18.Location = New System.Drawing.Point(3, 200)
        Me.Label18.Name = "Label18"
        Me.Label18.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.Label18.Size = New System.Drawing.Size(493, 25)
        Me.Label18.TabIndex = 38
        Me.Label18.Text = "Sonstiges"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblESSHeading
        '
        Me.lblESSHeading.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel.SetColumnSpan(Me.lblESSHeading, 2)
        Me.lblESSHeading.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblESSHeading.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblESSHeading.Location = New System.Drawing.Point(1, 225)
        Me.lblESSHeading.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.lblESSHeading.Name = "lblESSHeading"
        Me.lblESSHeading.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblESSHeading.Size = New System.Drawing.Size(80, 25)
        Me.lblESSHeading.TabIndex = 39
        Me.lblESSHeading.Text = "Nothalt:"
        Me.lblESSHeading.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblJ1LSState
        '
        Me.lblJ1LSState.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblJ1LSState.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJ1LSState.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblJ1LSState.Location = New System.Drawing.Point(42, 176)
        Me.lblJ1LSState.Margin = New System.Windows.Forms.Padding(1)
        Me.lblJ1LSState.Name = "lblJ1LSState"
        Me.lblJ1LSState.Size = New System.Drawing.Size(39, 23)
        Me.lblJ1LSState.TabIndex = 41
        Me.lblJ1LSState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblRS232Heading
        '
        Me.lblRS232Heading.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel.SetColumnSpan(Me.lblRS232Heading, 2)
        Me.lblRS232Heading.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRS232Heading.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblRS232Heading.Location = New System.Drawing.Point(165, 225)
        Me.lblRS232Heading.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.lblRS232Heading.Name = "lblRS232Heading"
        Me.lblRS232Heading.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblRS232Heading.Size = New System.Drawing.Size(80, 25)
        Me.lblRS232Heading.TabIndex = 40
        Me.lblRS232Heading.Text = "RS232:"
        Me.lblRS232Heading.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblESState
        '
        Me.lblESState.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel.SetColumnSpan(Me.lblESState, 2)
        Me.lblESState.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblESState.Location = New System.Drawing.Point(83, 226)
        Me.lblESState.Margin = New System.Windows.Forms.Padding(1)
        Me.lblESState.Name = "lblESState"
        Me.lblESState.Size = New System.Drawing.Size(80, 23)
        Me.lblESState.TabIndex = 43
        Me.lblESState.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSerialState
        '
        Me.lblSerialState.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel.SetColumnSpan(Me.lblSerialState, 4)
        Me.lblSerialState.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSerialState.ForeColor = System.Drawing.Color.Red
        Me.lblSerialState.Location = New System.Drawing.Point(247, 226)
        Me.lblSerialState.Margin = New System.Windows.Forms.Padding(1)
        Me.lblSerialState.Name = "lblSerialState"
        Me.lblSerialState.Size = New System.Drawing.Size(162, 23)
        Me.lblSerialState.TabIndex = 42
        Me.lblSerialState.Text = "nicht verbunden"
        Me.lblSerialState.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblJ2Val
        '
        Me.lblJ2Val.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel.SetColumnSpan(Me.lblJ2Val, 2)
        Me.lblJ2Val.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJ2Val.Location = New System.Drawing.Point(83, 51)
        Me.lblJ2Val.Margin = New System.Windows.Forms.Padding(1)
        Me.lblJ2Val.Name = "lblJ2Val"
        Me.lblJ2Val.Size = New System.Drawing.Size(80, 23)
        Me.lblJ2Val.TabIndex = 44
        Me.lblJ2Val.Text = "0 °"
        Me.lblJ2Val.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblJ3Val
        '
        Me.lblJ3Val.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel.SetColumnSpan(Me.lblJ3Val, 2)
        Me.lblJ3Val.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJ3Val.Location = New System.Drawing.Point(165, 51)
        Me.lblJ3Val.Margin = New System.Windows.Forms.Padding(1)
        Me.lblJ3Val.Name = "lblJ3Val"
        Me.lblJ3Val.Size = New System.Drawing.Size(80, 23)
        Me.lblJ3Val.TabIndex = 45
        Me.lblJ3Val.Text = "0 °"
        Me.lblJ3Val.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblJ4Val
        '
        Me.lblJ4Val.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel.SetColumnSpan(Me.lblJ4Val, 2)
        Me.lblJ4Val.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJ4Val.Location = New System.Drawing.Point(247, 51)
        Me.lblJ4Val.Margin = New System.Windows.Forms.Padding(1)
        Me.lblJ4Val.Name = "lblJ4Val"
        Me.lblJ4Val.Size = New System.Drawing.Size(80, 23)
        Me.lblJ4Val.TabIndex = 46
        Me.lblJ4Val.Text = "0 °"
        Me.lblJ4Val.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblJ5Val
        '
        Me.lblJ5Val.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel.SetColumnSpan(Me.lblJ5Val, 2)
        Me.lblJ5Val.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJ5Val.Location = New System.Drawing.Point(329, 51)
        Me.lblJ5Val.Margin = New System.Windows.Forms.Padding(1)
        Me.lblJ5Val.Name = "lblJ5Val"
        Me.lblJ5Val.Size = New System.Drawing.Size(80, 23)
        Me.lblJ5Val.TabIndex = 47
        Me.lblJ5Val.Text = "0 °"
        Me.lblJ5Val.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblJ6Val
        '
        Me.lblJ6Val.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel.SetColumnSpan(Me.lblJ6Val, 2)
        Me.lblJ6Val.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJ6Val.Location = New System.Drawing.Point(411, 51)
        Me.lblJ6Val.Margin = New System.Windows.Forms.Padding(1)
        Me.lblJ6Val.Name = "lblJ6Val"
        Me.lblJ6Val.Size = New System.Drawing.Size(87, 23)
        Me.lblJ6Val.TabIndex = 48
        Me.lblJ6Val.Text = "0 °"
        Me.lblJ6Val.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblYVal
        '
        Me.lblYVal.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel.SetColumnSpan(Me.lblYVal, 2)
        Me.lblYVal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblYVal.Location = New System.Drawing.Point(83, 126)
        Me.lblYVal.Margin = New System.Windows.Forms.Padding(1)
        Me.lblYVal.Name = "lblYVal"
        Me.lblYVal.Size = New System.Drawing.Size(80, 23)
        Me.lblYVal.TabIndex = 49
        Me.lblYVal.Text = "0"
        Me.lblYVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblZVal
        '
        Me.lblZVal.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel.SetColumnSpan(Me.lblZVal, 2)
        Me.lblZVal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblZVal.Location = New System.Drawing.Point(165, 126)
        Me.lblZVal.Margin = New System.Windows.Forms.Padding(1)
        Me.lblZVal.Name = "lblZVal"
        Me.lblZVal.Size = New System.Drawing.Size(80, 23)
        Me.lblZVal.TabIndex = 50
        Me.lblZVal.Text = "0"
        Me.lblZVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblYawVal
        '
        Me.lblYawVal.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel.SetColumnSpan(Me.lblYawVal, 2)
        Me.lblYawVal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblYawVal.Location = New System.Drawing.Point(247, 126)
        Me.lblYawVal.Margin = New System.Windows.Forms.Padding(1)
        Me.lblYawVal.Name = "lblYawVal"
        Me.lblYawVal.Size = New System.Drawing.Size(80, 23)
        Me.lblYawVal.TabIndex = 51
        Me.lblYawVal.Text = "0 °"
        Me.lblYawVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPitchVal
        '
        Me.lblPitchVal.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel.SetColumnSpan(Me.lblPitchVal, 2)
        Me.lblPitchVal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPitchVal.Location = New System.Drawing.Point(329, 126)
        Me.lblPitchVal.Margin = New System.Windows.Forms.Padding(1)
        Me.lblPitchVal.Name = "lblPitchVal"
        Me.lblPitchVal.Size = New System.Drawing.Size(80, 23)
        Me.lblPitchVal.TabIndex = 52
        Me.lblPitchVal.Text = "0 °"
        Me.lblPitchVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblRollVal
        '
        Me.lblRollVal.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel.SetColumnSpan(Me.lblRollVal, 2)
        Me.lblRollVal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRollVal.Location = New System.Drawing.Point(411, 126)
        Me.lblRollVal.Margin = New System.Windows.Forms.Padding(1)
        Me.lblRollVal.Name = "lblRollVal"
        Me.lblRollVal.Size = New System.Drawing.Size(87, 23)
        Me.lblRollVal.TabIndex = 53
        Me.lblRollVal.Text = "0 °"
        Me.lblRollVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblJ2LSState
        '
        Me.lblJ2LSState.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblJ2LSState.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJ2LSState.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblJ2LSState.Location = New System.Drawing.Point(124, 176)
        Me.lblJ2LSState.Margin = New System.Windows.Forms.Padding(1)
        Me.lblJ2LSState.Name = "lblJ2LSState"
        Me.lblJ2LSState.Size = New System.Drawing.Size(39, 23)
        Me.lblJ2LSState.TabIndex = 54
        Me.lblJ2LSState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblJ3LSState
        '
        Me.lblJ3LSState.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblJ3LSState.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJ3LSState.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblJ3LSState.Location = New System.Drawing.Point(206, 176)
        Me.lblJ3LSState.Margin = New System.Windows.Forms.Padding(1)
        Me.lblJ3LSState.Name = "lblJ3LSState"
        Me.lblJ3LSState.Size = New System.Drawing.Size(39, 23)
        Me.lblJ3LSState.TabIndex = 55
        Me.lblJ3LSState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblJ4LSState
        '
        Me.lblJ4LSState.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblJ4LSState.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJ4LSState.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblJ4LSState.Location = New System.Drawing.Point(288, 176)
        Me.lblJ4LSState.Margin = New System.Windows.Forms.Padding(1)
        Me.lblJ4LSState.Name = "lblJ4LSState"
        Me.lblJ4LSState.Size = New System.Drawing.Size(39, 23)
        Me.lblJ4LSState.TabIndex = 56
        Me.lblJ4LSState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblJ5LSState
        '
        Me.lblJ5LSState.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblJ5LSState.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJ5LSState.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblJ5LSState.Location = New System.Drawing.Point(370, 176)
        Me.lblJ5LSState.Margin = New System.Windows.Forms.Padding(1)
        Me.lblJ5LSState.Name = "lblJ5LSState"
        Me.lblJ5LSState.Size = New System.Drawing.Size(39, 23)
        Me.lblJ5LSState.TabIndex = 57
        Me.lblJ5LSState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblJ6LSState
        '
        Me.lblJ6LSState.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblJ6LSState.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJ6LSState.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblJ6LSState.Location = New System.Drawing.Point(452, 176)
        Me.lblJ6LSState.Margin = New System.Windows.Forms.Padding(1)
        Me.lblJ6LSState.Name = "lblJ6LSState"
        Me.lblJ6LSState.Size = New System.Drawing.Size(46, 23)
        Me.lblJ6LSState.TabIndex = 58
        Me.lblJ6LSState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'panRoboStatus
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(499, 467)
        Me.Controls.Add(Me.TableLayoutPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "panRoboStatus"
        Me.Text = "Roboter Status"
        Me.TableLayoutPanel.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lblCoordsHeading As Label
    Friend WithEvents TableLayoutPanel As TableLayoutPanel
    Friend WithEvents lblJ1Heading As Label
    Friend WithEvents lblJointsHeading As Label
    Friend WithEvents lblXHeading As Label
    Friend WithEvents lblYHeading As Label
    Friend WithEvents lblZHeading As Label
    Friend WithEvents lblYawHeading As Label
    Friend WithEvents lblPitchHeading As Label
    Friend WithEvents lblRollHeading As Label
    Friend WithEvents lblJ1HeadingImg As Label
    Friend WithEvents lblJ3Heading As Label
    Friend WithEvents lblJ3HeadingImg As Label
    Friend WithEvents lblJ2Heading As Label
    Friend WithEvents lblJ2HeadingImg As Label
    Friend WithEvents lblJ4Heading As Label
    Friend WithEvents lblJ4HeadingImg As Label
    Friend WithEvents lblJ5Heading As Label
    Friend WithEvents lblJ6Heading As Label
    Friend WithEvents lblJ5HeadingImg As Label
    Friend WithEvents lblJ6HeadingImg As Label
    Friend WithEvents lblJ1Val As Label
    Friend WithEvents lblXVal As Label
    Friend WithEvents lblLimitSwitchesHeading As Label
    Friend WithEvents lblLimitJ1Heading As Label
    Friend WithEvents lblLimitJ2Heading As Label
    Friend WithEvents lblLimitJ3Heading As Label
    Friend WithEvents lblLimitJ4Heading As Label
    Friend WithEvents lblLimitJ5Heading As Label
    Friend WithEvents lblLimitJ6Heading As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents lblESSHeading As Label
    Friend WithEvents lblRS232Heading As Label
    Friend WithEvents lblJ1LSState As Label
    Friend WithEvents lblSerialState As Label
    Friend WithEvents lblESState As Label
    Friend WithEvents lblJ2Val As Label
    Friend WithEvents lblJ3Val As Label
    Friend WithEvents lblJ4Val As Label
    Friend WithEvents lblJ5Val As Label
    Friend WithEvents lblJ6Val As Label
    Friend WithEvents lblYVal As Label
    Friend WithEvents lblZVal As Label
    Friend WithEvents lblYawVal As Label
    Friend WithEvents lblPitchVal As Label
    Friend WithEvents lblRollVal As Label
    Friend WithEvents lblJ2LSState As Label
    Friend WithEvents lblJ3LSState As Label
    Friend WithEvents lblJ4LSState As Label
    Friend WithEvents lblJ5LSState As Label
    Friend WithEvents lblJ6LSState As Label
End Class
