Imports ScintillaNET
Public Class panCodeEditor
    ' -----------------------------------------------------------------------------
    ' TODO
    ' -----------------------------------------------------------------------------
    ' Syntax Highlighting
    Private _lastHighlightedLine As Int32 = 0


    ' -----------------------------------------------------------------------------
    ' Init Panel
    ' -----------------------------------------------------------------------------
    Private maxLineNumberCharLength As Int32
    Private Sub panCodeEditor_Load(sender As Object, e As EventArgs) Handles Me.Load
        sciCodeEditor.Margins(0).Width = sciCodeEditor.TextWidth(Style.LineNumber, "99")
        sciCodeEditor.Styles(Style.Default).Font = "Courier New"
        sciCodeEditor.Styles(Style.Default).Size = 12

        sciCodeEditor.Markers(0).Symbol = MarkerSymbol.Background
        sciCodeEditor.Markers(0).SetBackColor(Color.Red)

        AddHandler frmMain.ACLProgram.ProgramStarted, AddressOf _eProgramStarted
        AddHandler frmMain.ACLProgram.ProgramFinished, AddressOf _eProgramFinished
        AddHandler frmMain.ACLProgram.ProgramLineChanged, AddressOf _eProgramLineChanged
    End Sub

    ' -----------------------------------------------------------------------------
    ' Form Control
    ' -----------------------------------------------------------------------------
    Private Sub sciCodeEditor_TextChanged(sender As Object, e As EventArgs) Handles sciCodeEditor.TextChanged
        'Calculate Line Number Width
        Dim maxLineNumberCharLength = sciCodeEditor.Lines.Count.ToString().Length
        If (maxLineNumberCharLength = Me.maxLineNumberCharLength) Then
            Return
        End If
        sciCodeEditor.Margins(0).Width = sciCodeEditor.TextWidth(ScintillaNET.Style.LineNumber, New String("9"c, maxLineNumberCharLength + 1))
        Me.maxLineNumberCharLength = maxLineNumberCharLength
    End Sub

    ' -----------------------------------------------------------------------------
    ' Events
    ' -----------------------------------------------------------------------------
    Private Sub _eProgramStarted()
        If InvokeRequired Then
            Invoke(Sub() _eProgramStarted())
            Return
        End If

        sciCodeEditor.ReadOnly = True
    End Sub
    Private Sub _eProgramFinished()
        If InvokeRequired Then
            Invoke(Sub() _eProgramFinished())
            Return
        End If

        sciCodeEditor.Lines(_lastHighlightedLine).MarkerDelete(0)
        sciCodeEditor.ReadOnly = False
    End Sub
    Private Sub _eProgramLineChanged(line As Int32)
        If InvokeRequired Then
            Invoke(Sub() _eProgramLineChanged(line))
            Return
        End If

        sciCodeEditor.Lines(_lastHighlightedLine).MarkerDelete(0)
        _lastHighlightedLine = line - 1
        sciCodeEditor.Lines(_lastHighlightedLine).MarkerAdd(0)
    End Sub
End Class