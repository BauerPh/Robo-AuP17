Imports WeifenLuo.WinFormsUI.Docking
Imports System.IO

Public Class frmMain
    'Test
    Public dictTest As New Dictionary(Of String, Double)
    Public dckPanel As New DockPanel

    Dim dckPanCodeEditor As New panCodeEditor
    Dim dckPanLog As New panLog
    Dim dckPanVariables As New panVariables
    Dim dckPanTeachpoints As New panTeachPoints
    Dim dckPanRoboStatus As New panRoboStatus
    Dim dckPanProgramTools As New panProgramTools
    Dim dckPanTeachBox As New panTeachBox

    Public Sub New()
        MyBase.New()

        InitializeComponent()
        Me.SuspendLayout()
        'Create & Configure DockPanel
        With dckPanel
            .Theme = New VS2015LightTheme
            .Parent = Me
            .Dock = DockStyle.Fill
            .BringToFront()
        End With

        dckPanCodeEditor.HideOnClose = True
        dckPanLog.HideOnClose = True
        dckPanVariables.HideOnClose = True
        dckPanTeachpoints.HideOnClose = True
        dckPanRoboStatus.HideOnClose = True
        dckPanProgramTools.HideOnClose = True
        dckPanTeachBox.HideOnClose = True

        If (File.Exists("ViewDefault.xml")) Then
            dckPanel.LoadFromXml("ViewDefault.xml", AddressOf GetContent)
        Else
            'Add Panels
            dckPanCodeEditor.Show(dckPanel, DockState.Document)
            dckPanLog.Show(dckPanel, DockState.DockBottom)
            dckPanVariables.Show(dckPanel, DockState.DockRight)
            dckPanTeachpoints.Show(dckPanVariables.Pane, DockAlignment.Bottom, 0.5)
            dckPanRoboStatus.Show(dckPanel, DockState.DockLeft)
            dckPanProgramTools.Show(dckPanRoboStatus.Pane, DockAlignment.Bottom, 0.5)
            dckPanTeachBox.Show(dckPanel, DockState.Float)
            dckPanTeachBox.Hide()
        End If

        Me.ResumeLayout()
    End Sub

    Private Sub msSaveView_Click(sender As Object, e As EventArgs) Handles msSaveView.Click
        dckPanel.SaveAsXml("ViewDefault.xml")
        MessageBox.Show("aktuelle Ansicht wurde gespeichert!", "Okay", MessageBoxButtons.OK)
    End Sub

    Private Sub msDefaulView_Click(sender As Object, e As EventArgs) Handles msDefaulView.Click
        File.Delete("ViewDefault.xml")
        MessageBox.Show("Standard wurde wiederhergestellt. Neustart erforderlich!", "Okay", MessageBoxButtons.OK)
    End Sub

    Private Sub msShowPanVariable_Click(sender As Object, e As EventArgs) Handles msShowPanVariable.Click
        dckPanVariables.Show()
    End Sub

    Private Sub msShowPanTeachPoints_Click(sender As Object, e As EventArgs) Handles msShowPanTeachPoints.Click
        dckPanTeachpoints.Show()
    End Sub

    Private Sub msShowPanCodeEditor_Click(sender As Object, e As EventArgs) Handles msShowPanCodeEditor.Click
        dckPanCodeEditor.Show()
    End Sub

    Private Sub msShowPanTeachBox_Click(sender As Object, e As EventArgs) Handles msShowPanTeachBox.Click
        dckPanTeachBox.Show()
    End Sub

    Private Sub msShowPanRoboStatus_Click(sender As Object, e As EventArgs) Handles msShowPanRoboStatus.Click
        dckPanRoboStatus.Show()
    End Sub

    Private Sub msShowPanProgramTools_Click(sender As Object, e As EventArgs) Handles msShowPanProgramTools.Click
        dckPanProgramTools.Show()
    End Sub

    Private Sub msShowPanLog_Click(sender As Object, e As EventArgs) Handles msShowPanLog.Click
        dckPanLog.Show()
    End Sub

    ' -----------------------------------------------------------------------------
    ' Helper Functions
    ' -----------------------------------------------------------------------------
    Private Function GetContent(persist As String) As IDockContent
        If persist.EndsWith("CodeEditor") Then
            Return dckPanCodeEditor
        ElseIf persist.EndsWith("Log") Then
            Return dckPanLog
        ElseIf persist.EndsWith("ProgramTools") Then
            Return dckPanProgramTools
        ElseIf persist.EndsWith("RoboStatus") Then
            Return dckPanRoboStatus
        ElseIf persist.EndsWith("TeachPoints") Then
            Return dckPanTeachpoints
        ElseIf persist.EndsWith("Variables") Then
            Return dckPanVariables
        ElseIf persist.EndsWith("TeachBox") Then
            Return dckPanTeachBox
        End If
        Return New DockContent()
    End Function
End Class
