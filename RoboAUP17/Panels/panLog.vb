Friend Class panLog
    ' -----------------------------------------------------------------------------
    ' TODO
    ' -----------------------------------------------------------------------------
    ' fertig?

    ' -----------------------------------------------------------------------------
    ' Init Panel
    ' -----------------------------------------------------------------------------
    Private Sub panLog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        sciLog.Styles(ScintillaNET.Style.Default).Font = "Courier New"
    End Sub

    ' -----------------------------------------------------------------------------
    ' Persist String for View Settings (XML-File)
    ' -----------------------------------------------------------------------------
    Protected Overrides Function GetPersistString() As String
        Return Me.Text
    End Function
    ' -----------------------------------------------------------------------------
    ' Form Control
    ' -----------------------------------------------------------------------------
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        sciLog.ReadOnly = False
        sciLog.ClearAll()
        sciLog.ReadOnly = True
    End Sub
End Class