<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class panCodeEditor
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
        Me.sciCodeEditor = New ScintillaNET.Scintilla()
        Me.SuspendLayout()
        '
        'sciCodeEditor
        '
        Me.sciCodeEditor.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.sciCodeEditor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sciCodeEditor.Location = New System.Drawing.Point(0, 0)
        Me.sciCodeEditor.Name = "sciCodeEditor"
        Me.sciCodeEditor.ScrollWidth = 23
        Me.sciCodeEditor.Size = New System.Drawing.Size(884, 561)
        Me.sciCodeEditor.TabIndex = 0
        '
        'panCodeEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(884, 561)
        Me.Controls.Add(Me.sciCodeEditor)
        Me.DockAreas = CType((WeifenLuo.WinFormsUI.Docking.DockAreas.Float Or WeifenLuo.WinFormsUI.Docking.DockAreas.Document), WeifenLuo.WinFormsUI.Docking.DockAreas)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "panCodeEditor"
        Me.Text = "ACL Programm"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents sciCodeEditor As ScintillaNET.Scintilla
End Class
