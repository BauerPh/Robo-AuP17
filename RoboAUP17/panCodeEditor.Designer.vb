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
        Me.Scintilla1 = New ScintillaNET.Scintilla()
        Me.SuspendLayout()
        '
        'Scintilla1
        '
        Me.Scintilla1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Scintilla1.Location = New System.Drawing.Point(0, 0)
        Me.Scintilla1.Name = "Scintilla1"
        Me.Scintilla1.ScrollWidth = 23
        Me.Scintilla1.Size = New System.Drawing.Size(520, 475)
        Me.Scintilla1.TabIndex = 0
        Me.Scintilla1.Text = "IF..."
        '
        'panCodeEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(520, 475)
        Me.Controls.Add(Me.Scintilla1)
        Me.Name = "panCodeEditor"
        Me.Text = "ACL Programm"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Scintilla1 As ScintillaNET.Scintilla
End Class
