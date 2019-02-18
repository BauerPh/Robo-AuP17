Imports WeifenLuo.WinFormsUI.Docking

Public Class frmMain
    Public dictTest As New Dictionary(Of String, Double)

    Dim dckPanel As DockPanel
    Dim dckPanCodeEditor As New panCodeEditor
    Dim dckPanLog As New panLog
    Dim dckPanVariables As New panVariables
    Dim dckPanTeachpoints As New panTeachPoints
    Dim dckPanRoboStatus As New panRoboStatus
    Dim dckPanProgramTools As New panProgramTools
    Public Sub New()
        MyBase.New()

        InitializeComponent()
        Me.SuspendLayout()
        'Create & Configure DockPanel
        dckPanel = New DockPanel
        With dckPanel
            .Theme = New VS2015LightTheme
            .Parent = Me
            .Dock = DockStyle.Fill
            .BringToFront()
        End With
        'Add Panels
        dckPanCodeEditor.Show(dckPanel, DockState.Document)
        dckPanLog.Show(dckPanel, DockState.DockBottom)
        dckPanVariables.Show(dckPanel, DockState.DockRight)
        dckPanTeachpoints.Show(dckPanel, DockState.DockRight)
        dckPanRoboStatus.Show(dckPanel, DockState.DockLeft)
        dckPanProgramTools.Show(dckPanel, DockState.DockLeft)

        Me.ResumeLayout()
    End Sub

End Class
