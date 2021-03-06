﻿Friend Class Logger
    ' -----------------------------------------------------------------------------
    ' Definitions
    ' -----------------------------------------------------------------------------
    Private _logBox As ScintillaNET.Scintilla
    Private _logLvl As Integer = 0

    ' -----------------------------------------------------------------------------
    ' Constructor
    ' -----------------------------------------------------------------------------
    Friend Sub New(ByRef LoggingBox As ScintillaNET.Scintilla)
        _logBox = LoggingBox
    End Sub

    ' -----------------------------------------------------------------------------
    ' Public
    ' -----------------------------------------------------------------------------
    Friend Sub Log(msg As String, lvl As LogLevel)
        'Check if we have to Log the Message
        If _logLvl <= lvl.ToInteger() Then
            'Handle Invoke
            If _logBox.InvokeRequired Then
                _logBox.Invoke(Sub() Log(msg, lvl))
                Return
            End If
            'Log
            _logBox.ReadOnly = False
            If _logBox.TextLength > 0 Then
                _logBox.AppendText(vbCrLf)
            End If
            _logBox.AppendText($"{Now().ToString("HH:mm:ss.fff")} [ {lvl.ToString().PadRight(7)} ] {msg}")
            _logBox.ReadOnly = True
            ' Scroll Logbox
            _logBox.Lines(_logBox.Lines.Count - 1).Goto()
            _logBox.ScrollCaret()
        End If
    End Sub
    Friend Sub ShowErrMsg(msg As String)
        Log(msg, LogLevel.ERR)
        MsgBox(msg, vbCritical)
    End Sub

    Friend Sub SetLogLvl(lvl As LogLevel)
        _logLvl = lvl.ToInteger()
    End Sub

    Friend Sub SetLogLvl(lvl As Integer)
        _logLvl = lvl
    End Sub

    'Error Levels
    Friend Class LogLevel
        Private _key As String
        Private _lvl As Integer

        Friend Shared ReadOnly DEBUG As LogLevel = New LogLevel("DEBUG", 0)
        Friend Shared ReadOnly INFO As LogLevel = New LogLevel("INFO", 1)
        Friend Shared ReadOnly WARN As LogLevel = New LogLevel("WARNING", 2)
        Friend Shared ReadOnly ERR As LogLevel = New LogLevel("ERROR", 3)
        Friend Shared ReadOnly COMIN As LogLevel = New LogLevel("COMIN", 1)
        Friend Shared ReadOnly COMOUT As LogLevel = New LogLevel("COMOUT", 1)

        Private Sub New(key As String, prio As Integer)
            Me._key = key
            Me._lvl = prio
        End Sub

        Public Overrides Function ToString() As String
            Return Me._key.ToUpper()
        End Function

        Friend Function ToInteger() As Integer
            Return Me._lvl
        End Function

        Public Shared Operator =(ByVal val1 As LogLevel, ByVal val2 As LogLevel) As Boolean
            Return val1._key = val2._key
        End Operator
        Public Shared Operator <>(ByVal val1 As LogLevel, ByVal val2 As LogLevel) As Boolean
            Return val1._key <> val2._key
        End Operator
    End Class
End Class
