Friend Class Logger
    Private _logBox As RichTextBox

    Public Class ErrorLevel
        Private Key As String

        Public Shared ReadOnly ERR As ErrorLevel = New ErrorLevel(" ERROR ")
        Public Shared ReadOnly WARN As ErrorLevel = New ErrorLevel("WARNING")
        Public Shared ReadOnly INFO As ErrorLevel = New ErrorLevel(" INFO  ")
        Public Shared ReadOnly COMIN As ErrorLevel = New ErrorLevel(" COM IN")
        Public Shared ReadOnly COMOUT As ErrorLevel = New ErrorLevel("COM OUT")

        Private Sub New(key As String)
            Me.Key = key
        End Sub

        Public Overrides Function ToString() As String
            Return Me.Key
        End Function
    End Class

    Public Sub New(ByRef LoggingBox As RichTextBox)
        _logBox = LoggingBox
    End Sub


    Public Sub doLog(msg As String, lvl As ErrorLevel)
        'Handle Invoke
        If _logBox.InvokeRequired Then
            _logBox.Invoke(Sub() doLog(msg, lvl))
            Return
        End If

        _logBox.Text &= Now().ToString("HH:mm:ss.fff") & " [" & lvl.ToString.ToUpper() & "] " & msg & vbCrLf
    End Sub
    Public Sub showErrMsg(msg As String)
        doLog(msg, ErrorLevel.ERR)
        MsgBox(msg, vbCritical)
    End Sub
End Class
