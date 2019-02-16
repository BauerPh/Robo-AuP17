Imports WeifenLuo.WinFormsUI.Docking

Public Class Form1
    Dim dckPanel As DockPanel
    Public Sub New()
        MyBase.New()

        InitializeComponent()
        Me.SuspendLayout()
        'Create & Configure DockPanel
        dckPanel = New DockPanel
        With dckPanel
            .Theme = New VS2015DarkTheme
            .Parent = Me
            .Dock = DockStyle.Fill
            .BringToFront()
        End With
        'Add Panels
        Dim dock1 As New panDok
        dock1.Show(dckPanel, DockState.Document)

        Dim dock2 As New panProp
        dock2.Show(dckPanel, DockState.DockRight)
        Dim dock3 As New panProp
        dock3.Show(dckPanel, DockState.DockRight)
        Me.ResumeLayout()
    End Sub

End Class
