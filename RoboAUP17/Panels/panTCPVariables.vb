Option Strict On
Public Class panTCPVariables
    ' -----------------------------------------------------------------------------
    ' TODO
    ' -----------------------------------------------------------------------------
    ' TCP Variablen anlegen, bearbeiten, löschen

    ' -----------------------------------------------------------------------------
    ' Init Panel
    ' -----------------------------------------------------------------------------

    ' -----------------------------------------------------------------------------
    ' Form Control
    ' -----------------------------------------------------------------------------

    ' -----------------------------------------------------------------------------
    ' Test
    ' -----------------------------------------------------------------------------
    Private Sub panVariables_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        frmMain.dictTest.Add("var1", 25.5)
        Dim _priceDataArray = From row In frmMain.dictTest Select New With {.Variable = row.Key, .Wert = row.Value}
        frmMain.dictTest.Add("var2", 26.5)
        dataGridView.DataSource = _priceDataArray.ToArray
        frmMain.dictTest.Add("var3", 27.5)
    End Sub

End Class