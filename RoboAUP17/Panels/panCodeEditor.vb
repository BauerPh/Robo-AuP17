Imports ScintillaNET
Public Class panCodeEditor
    ' -----------------------------------------------------------------------------
    ' TODO
    ' -----------------------------------------------------------------------------
    ' Syntax Highlighting
    Private _lastHighlightedLineIndex As Int32 = 0
    Private _lastHighlightedErrorLineIndex As Int32 = 0


    ' -----------------------------------------------------------------------------
    ' Init Panel
    ' -----------------------------------------------------------------------------
    Private maxLineNumberCharLength As Int32
    Private Sub panCodeEditor_Load(sender As Object, e As EventArgs) Handles Me.Load
        sciCodeEditor.Margins(0).Width = sciCodeEditor.TextWidth(Style.LineNumber, "99")
        sciCodeEditor.Styles(Style.Default).Font = "Courier New"
        sciCodeEditor.Styles(Style.Default).Size = 12

        sciCodeEditor.Markers(0).Symbol = MarkerSymbol.Arrow
        sciCodeEditor.Markers(0).SetBackColor(Color.Yellow)
        sciCodeEditor.Markers(1).Symbol = MarkerSymbol.Background
        sciCodeEditor.Markers(1).SetBackColor(Color.LightYellow)
        sciCodeEditor.Markers(2).Symbol = MarkerSymbol.Background
        sciCodeEditor.Markers(2).SetBackColor(Color.Red)

        AddHandler frmMain.ACLProgram.ProgramStarted, AddressOf _eProgramStarted
        AddHandler frmMain.ACLProgram.ProgramFinished, AddressOf _eProgramFinished
        AddHandler frmMain.ACLProgram.ProgramLineChanged, AddressOf _eProgramLineChanged
        AddHandler frmMain.ACLProgram.CompileErrorLine, AddressOf _eCompileErrorLine
    End Sub

    ' -----------------------------------------------------------------------------
    ' Form Control
    ' -----------------------------------------------------------------------------
    Private Sub sciCodeEditor_TextChanged(sender As Object, e As EventArgs) Handles sciCodeEditor.TextChanged
        ' Ungespeicherte Änderungen!
        frmMain.ACLProgram.UnsavedChanges = True
        ' Remove marker
        sciCodeEditor.Lines(_lastHighlightedErrorLineIndex).MarkerDelete(2)

        ' Calculate Line Number Width
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

        sciCodeEditor.Lines(_lastHighlightedLineIndex).MarkerDelete(0)
        sciCodeEditor.Lines(_lastHighlightedLineIndex).MarkerDelete(1)
        sciCodeEditor.ReadOnly = False
    End Sub
    Private Sub _eProgramLineChanged(line As Int32)
        If InvokeRequired Then
            Invoke(Sub() _eProgramLineChanged(line))
            Return
        End If

        sciCodeEditor.Lines(_lastHighlightedLineIndex).MarkerDelete(0)
        sciCodeEditor.Lines(_lastHighlightedLineIndex).MarkerDelete(1)
        _lastHighlightedLineIndex = line - 1
        sciCodeEditor.Lines(_lastHighlightedLineIndex).MarkerAdd(0)
        sciCodeEditor.Lines(_lastHighlightedLineIndex).MarkerAdd(1)
    End Sub

    Private Sub _eCompileErrorLine(line As Int32)
        If InvokeRequired Then
            Invoke(Sub() _eProgramLineChanged(line))
            Return
        End If

        sciCodeEditor.Lines(line - 1).MarkerAdd(2)
        _lastHighlightedErrorLineIndex = line - 1
    End Sub
End Class