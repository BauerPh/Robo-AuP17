<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class panRoboStatus
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
        Me.numJ1 = New System.Windows.Forms.NumericUpDown()
        Me.numJ2 = New System.Windows.Forms.NumericUpDown()
        Me.numJ3 = New System.Windows.Forms.NumericUpDown()
        Me.numJ4 = New System.Windows.Forms.NumericUpDown()
        Me.numJ5 = New System.Windows.Forms.NumericUpDown()
        Me.numJ6 = New System.Windows.Forms.NumericUpDown()
        Me.lblX = New System.Windows.Forms.Label()
        Me.lblY = New System.Windows.Forms.Label()
        Me.lblZ = New System.Windows.Forms.Label()
        Me.lblYaw = New System.Windows.Forms.Label()
        Me.lblPitch = New System.Windows.Forms.Label()
        Me.lblRoll = New System.Windows.Forms.Label()
        Me.lblJ6 = New System.Windows.Forms.Label()
        Me.lblJ5 = New System.Windows.Forms.Label()
        Me.lblJ4 = New System.Windows.Forms.Label()
        Me.lblJ3 = New System.Windows.Forms.Label()
        Me.lblJ2 = New System.Windows.Forms.Label()
        Me.lblJ1 = New System.Windows.Forms.Label()
        Me.btnLos = New System.Windows.Forms.Button()
        CType(Me.numJ1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numJ2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numJ3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numJ4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numJ5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numJ6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'numJ1
        '
        Me.numJ1.DecimalPlaces = 4
        Me.numJ1.Increment = New Decimal(New Integer() {1, 0, 0, 262144})
        Me.numJ1.Location = New System.Drawing.Point(32, 27)
        Me.numJ1.Maximum = New Decimal(New Integer() {360, 0, 0, 0})
        Me.numJ1.Minimum = New Decimal(New Integer() {360, 0, 0, -2147483648})
        Me.numJ1.Name = "numJ1"
        Me.numJ1.Size = New System.Drawing.Size(102, 20)
        Me.numJ1.TabIndex = 0
        Me.numJ1.Value = New Decimal(New Integer() {1, 0, 0, 262144})
        '
        'numJ2
        '
        Me.numJ2.DecimalPlaces = 4
        Me.numJ2.Increment = New Decimal(New Integer() {1, 0, 0, 262144})
        Me.numJ2.Location = New System.Drawing.Point(32, 53)
        Me.numJ2.Maximum = New Decimal(New Integer() {360, 0, 0, 0})
        Me.numJ2.Minimum = New Decimal(New Integer() {360, 0, 0, -2147483648})
        Me.numJ2.Name = "numJ2"
        Me.numJ2.Size = New System.Drawing.Size(102, 20)
        Me.numJ2.TabIndex = 1
        Me.numJ2.Value = New Decimal(New Integer() {90, 0, 0, -2147483648})
        '
        'numJ3
        '
        Me.numJ3.DecimalPlaces = 4
        Me.numJ3.Increment = New Decimal(New Integer() {1, 0, 0, 262144})
        Me.numJ3.Location = New System.Drawing.Point(32, 79)
        Me.numJ3.Maximum = New Decimal(New Integer() {360, 0, 0, 0})
        Me.numJ3.Minimum = New Decimal(New Integer() {360, 0, 0, -2147483648})
        Me.numJ3.Name = "numJ3"
        Me.numJ3.Size = New System.Drawing.Size(102, 20)
        Me.numJ3.TabIndex = 2
        Me.numJ3.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'numJ4
        '
        Me.numJ4.DecimalPlaces = 4
        Me.numJ4.Increment = New Decimal(New Integer() {1, 0, 0, 262144})
        Me.numJ4.Location = New System.Drawing.Point(32, 105)
        Me.numJ4.Maximum = New Decimal(New Integer() {360, 0, 0, 0})
        Me.numJ4.Minimum = New Decimal(New Integer() {360, 0, 0, -2147483648})
        Me.numJ4.Name = "numJ4"
        Me.numJ4.Size = New System.Drawing.Size(102, 20)
        Me.numJ4.TabIndex = 3
        '
        'numJ5
        '
        Me.numJ5.DecimalPlaces = 4
        Me.numJ5.Increment = New Decimal(New Integer() {1, 0, 0, 262144})
        Me.numJ5.Location = New System.Drawing.Point(32, 131)
        Me.numJ5.Maximum = New Decimal(New Integer() {360, 0, 0, 0})
        Me.numJ5.Minimum = New Decimal(New Integer() {360, 0, 0, -2147483648})
        Me.numJ5.Name = "numJ5"
        Me.numJ5.Size = New System.Drawing.Size(102, 20)
        Me.numJ5.TabIndex = 4
        Me.numJ5.Value = New Decimal(New Integer() {1, 0, 0, 262144})
        '
        'numJ6
        '
        Me.numJ6.DecimalPlaces = 4
        Me.numJ6.Increment = New Decimal(New Integer() {1, 0, 0, 262144})
        Me.numJ6.Location = New System.Drawing.Point(32, 157)
        Me.numJ6.Maximum = New Decimal(New Integer() {360, 0, 0, 0})
        Me.numJ6.Minimum = New Decimal(New Integer() {360, 0, 0, -2147483648})
        Me.numJ6.Name = "numJ6"
        Me.numJ6.Size = New System.Drawing.Size(102, 20)
        Me.numJ6.TabIndex = 5
        '
        'lblX
        '
        Me.lblX.AutoSize = True
        Me.lblX.Location = New System.Drawing.Point(140, 29)
        Me.lblX.Name = "lblX"
        Me.lblX.Size = New System.Drawing.Size(39, 13)
        Me.lblX.TabIndex = 6
        Me.lblX.Text = "Label1"
        '
        'lblY
        '
        Me.lblY.AutoSize = True
        Me.lblY.Location = New System.Drawing.Point(140, 55)
        Me.lblY.Name = "lblY"
        Me.lblY.Size = New System.Drawing.Size(39, 13)
        Me.lblY.TabIndex = 7
        Me.lblY.Text = "Label2"
        '
        'lblZ
        '
        Me.lblZ.AutoSize = True
        Me.lblZ.Location = New System.Drawing.Point(140, 81)
        Me.lblZ.Name = "lblZ"
        Me.lblZ.Size = New System.Drawing.Size(39, 13)
        Me.lblZ.TabIndex = 8
        Me.lblZ.Text = "Label3"
        '
        'lblYaw
        '
        Me.lblYaw.AutoSize = True
        Me.lblYaw.Location = New System.Drawing.Point(140, 107)
        Me.lblYaw.Name = "lblYaw"
        Me.lblYaw.Size = New System.Drawing.Size(39, 13)
        Me.lblYaw.TabIndex = 9
        Me.lblYaw.Text = "Label4"
        '
        'lblPitch
        '
        Me.lblPitch.AutoSize = True
        Me.lblPitch.Location = New System.Drawing.Point(140, 133)
        Me.lblPitch.Name = "lblPitch"
        Me.lblPitch.Size = New System.Drawing.Size(39, 13)
        Me.lblPitch.TabIndex = 10
        Me.lblPitch.Text = "Label5"
        '
        'lblRoll
        '
        Me.lblRoll.AutoSize = True
        Me.lblRoll.Location = New System.Drawing.Point(140, 159)
        Me.lblRoll.Name = "lblRoll"
        Me.lblRoll.Size = New System.Drawing.Size(39, 13)
        Me.lblRoll.TabIndex = 11
        Me.lblRoll.Text = "Label6"
        '
        'lblJ6
        '
        Me.lblJ6.AutoSize = True
        Me.lblJ6.Location = New System.Drawing.Point(245, 159)
        Me.lblJ6.Name = "lblJ6"
        Me.lblJ6.Size = New System.Drawing.Size(39, 13)
        Me.lblJ6.TabIndex = 17
        Me.lblJ6.Text = "Label7"
        '
        'lblJ5
        '
        Me.lblJ5.AutoSize = True
        Me.lblJ5.Location = New System.Drawing.Point(245, 133)
        Me.lblJ5.Name = "lblJ5"
        Me.lblJ5.Size = New System.Drawing.Size(39, 13)
        Me.lblJ5.TabIndex = 16
        Me.lblJ5.Text = "Label8"
        '
        'lblJ4
        '
        Me.lblJ4.AutoSize = True
        Me.lblJ4.Location = New System.Drawing.Point(245, 107)
        Me.lblJ4.Name = "lblJ4"
        Me.lblJ4.Size = New System.Drawing.Size(39, 13)
        Me.lblJ4.TabIndex = 15
        Me.lblJ4.Text = "Label9"
        '
        'lblJ3
        '
        Me.lblJ3.AutoSize = True
        Me.lblJ3.Location = New System.Drawing.Point(245, 81)
        Me.lblJ3.Name = "lblJ3"
        Me.lblJ3.Size = New System.Drawing.Size(45, 13)
        Me.lblJ3.TabIndex = 14
        Me.lblJ3.Text = "Label10"
        '
        'lblJ2
        '
        Me.lblJ2.AutoSize = True
        Me.lblJ2.Location = New System.Drawing.Point(245, 55)
        Me.lblJ2.Name = "lblJ2"
        Me.lblJ2.Size = New System.Drawing.Size(45, 13)
        Me.lblJ2.TabIndex = 13
        Me.lblJ2.Text = "Label11"
        '
        'lblJ1
        '
        Me.lblJ1.AutoSize = True
        Me.lblJ1.Location = New System.Drawing.Point(245, 29)
        Me.lblJ1.Name = "lblJ1"
        Me.lblJ1.Size = New System.Drawing.Size(45, 13)
        Me.lblJ1.TabIndex = 12
        Me.lblJ1.Text = "Label12"
        '
        'btnLos
        '
        Me.btnLos.Location = New System.Drawing.Point(32, 199)
        Me.btnLos.Name = "btnLos"
        Me.btnLos.Size = New System.Drawing.Size(252, 28)
        Me.btnLos.TabIndex = 18
        Me.btnLos.Text = "Test"
        Me.btnLos.UseVisualStyleBackColor = True
        '
        'panRoboStatus
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(384, 361)
        Me.Controls.Add(Me.btnLos)
        Me.Controls.Add(Me.lblJ6)
        Me.Controls.Add(Me.lblJ5)
        Me.Controls.Add(Me.lblJ4)
        Me.Controls.Add(Me.lblJ3)
        Me.Controls.Add(Me.lblJ2)
        Me.Controls.Add(Me.lblJ1)
        Me.Controls.Add(Me.lblRoll)
        Me.Controls.Add(Me.lblPitch)
        Me.Controls.Add(Me.lblYaw)
        Me.Controls.Add(Me.lblZ)
        Me.Controls.Add(Me.lblY)
        Me.Controls.Add(Me.lblX)
        Me.Controls.Add(Me.numJ6)
        Me.Controls.Add(Me.numJ5)
        Me.Controls.Add(Me.numJ4)
        Me.Controls.Add(Me.numJ3)
        Me.Controls.Add(Me.numJ2)
        Me.Controls.Add(Me.numJ1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "panRoboStatus"
        Me.Text = "Roboter Status"
        CType(Me.numJ1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numJ2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numJ3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numJ4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numJ5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numJ6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents numJ1 As NumericUpDown
    Friend WithEvents numJ2 As NumericUpDown
    Friend WithEvents numJ3 As NumericUpDown
    Friend WithEvents numJ4 As NumericUpDown
    Friend WithEvents numJ5 As NumericUpDown
    Friend WithEvents numJ6 As NumericUpDown
    Friend WithEvents lblX As Label
    Friend WithEvents lblY As Label
    Friend WithEvents lblZ As Label
    Friend WithEvents lblYaw As Label
    Friend WithEvents lblPitch As Label
    Friend WithEvents lblRoll As Label
    Friend WithEvents lblJ6 As Label
    Friend WithEvents lblJ5 As Label
    Friend WithEvents lblJ4 As Label
    Friend WithEvents lblJ3 As Label
    Friend WithEvents lblJ2 As Label
    Friend WithEvents lblJ1 As Label
    Friend WithEvents btnLos As Button
End Class
