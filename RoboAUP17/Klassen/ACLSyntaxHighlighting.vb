Imports ScintillaNET
Public Class ACLSyntaxHighlighting
    Friend Const StyleDefault As Integer = 0
    Friend Const StyleKeyword As Integer = 1
    Friend Const StyleIdentifier As Integer = 2
    Friend Const StyleNumber As Integer = 3
    Friend Const StyleString As Integer = 4
    Friend Const StyleComment As Integer = 5

    Private Enum STATE
        UNKNOWN = 0
        IDENTIFIER
        NUMBER
        [STRING]
        COMMENTSTART
        COMMENT
    End Enum

    Private _keywords As HashSet(Of String)

    Friend Sub New(keywords As String)
        Dim list As IEnumerable(Of String) = keywords.Split(" "c).Where(Function(val As String) (val IsNot Nothing) AndAlso (val.Length > 0))
        _keywords = New HashSet(Of String)(list)
    End Sub

    Friend Sub Style(scintilla As Scintilla, startPos As Integer, endPos As Integer)
        Dim line = scintilla.LineFromPosition(startPos)
        startPos = scintilla.Lines(line).Position

        Dim length As Integer = 0
        Dim state As STATE = STATE.UNKNOWN
        Dim CRDetected As Boolean = False
        Dim WaitForSpace As Boolean = False
        ' Start styling
        scintilla.StartStyling(startPos)
        While startPos < endPos
            Dim c As Char = ChrW(scintilla.GetCharAt(startPos))

REPROCESS:
            Select Case state
                Case STATE.UNKNOWN
                    If c = """" Then
                        scintilla.SetStyling(1, StyleString)
                        state = STATE.STRING
                    ElseIf Char.IsDigit(c) Then
                        state = STATE.NUMBER
                        GoTo REPROCESS
                    ElseIf Char.IsLetter(c) Then
                        state = STATE.IDENTIFIER
                        GoTo REPROCESS
                    ElseIf c = "/" Then
                        state = STATE.COMMENTSTART
                    Else
                        scintilla.SetStyling(1, StyleDefault)
                    End If

                Case STATE.IDENTIFIER
                    If Char.IsLetterOrDigit(c) Then
                        length += 1
                    Else
                        Dim style As Integer = StyleIdentifier
                        If _keywords.Contains(scintilla.GetTextRange(startPos - length, length)) Then style = StyleKeyword
                        scintilla.SetStyling(length, style)
                        length = 0
                        state = STATE.UNKNOWN
                        GoTo REPROCESS
                    End If

                Case STATE.NUMBER
                    If Char.IsDigit(c) Then
                        scintilla.SetStyling(1, StyleNumber)
                    Else
                        state = STATE.UNKNOWN
                        GoTo REPROCESS
                    End If

                Case STATE.STRING
                    If c = """" Then
                        scintilla.SetStyling(1, StyleString)
                        state = STATE.UNKNOWN
                    Else
                        scintilla.SetStyling(1, StyleString)
                    End If

                Case STATE.COMMENTSTART
                    If c = "/"c Then
                        scintilla.SetStyling(2, StyleComment)
                        state = STATE.COMMENT
                    Else
                        scintilla.SetStyling(2, StyleDefault)
                        state = STATE.UNKNOWN
                    End If

                Case STATE.COMMENT
                    scintilla.SetStyling(1, StyleComment)
                    If c = vbCr Then
                        CRDetected = True
                    ElseIf CRDetected AndAlso c = vbLf Then
                        CRDetected = False
                        state = STATE.UNKNOWN
                    Else
                        CRDetected = False
                    End If

            End Select
            startPos += 1
        End While
    End Sub
End Class