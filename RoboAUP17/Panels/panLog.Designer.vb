<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class panLog
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
        Me.sciLog = New ScintillaNET.Scintilla()
        Me.SuspendLayout()
        '
        'sciLog
        '
        Me.sciLog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sciLog.Location = New System.Drawing.Point(0, 0)
        Me.sciLog.Name = "sciLog"
        Me.sciLog.ReadOnly = True
        Me.sciLog.ScrollWidth = 82
        Me.sciLog.Size = New System.Drawing.Size(984, 161)
        Me.sciLog.TabIndex = 0
        Me.sciLog.Text = "Logging usw..."
        '
        'panLog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(984, 161)
        Me.Controls.Add(Me.sciLog)
        Me.DockAreas = CType(((WeifenLuo.WinFormsUI.Docking.DockAreas.Float Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) _
            Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom), WeifenLuo.WinFormsUI.Docking.DockAreas)
        Me.Name = "panLog"
        Me.Text = "Ausgabe"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents sciLog As ScintillaNET.Scintilla
End Class
