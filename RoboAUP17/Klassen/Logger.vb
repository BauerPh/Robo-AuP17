Friend Class Logger
    ' -----------------------------------------------------------------------------
    ' TODO
    ' -----------------------------------------------------------------------------
    ' fertig

    ' -----------------------------------------------------------------------------
    ' Definitions
    ' -----------------------------------------------------------------------------
    Private _logBox As ScintillaNET.Scintilla
    Private _logLvl As Integer = 0

    ' -----------------------------------------------------------------------------
    ' Constructor
    ' -----------------------------------------------------------------------------
    Public Sub New(ByRef LoggingBox As ScintillaNET.Scintilla)
        _logBox = LoggingBox
    End Sub

    ' -----------------------------------------------------------------------------
    ' Public
    ' -----------------------------------------------------------------------------
    Public Sub Log(msg As String, lvl As LogLevel)
        'Check if we have to Log the Message
        If _logLvl <= lvl.ToInteger() Then
            'Handle Invoke
            If _logBox.InvokeRequired Then
                _logBox.Invoke(Sub() Log(msg, lvl))
                Return
            End If
            'Log
            _logBox.AddText(Now().ToString("HH:mm:ss.fff") & " [" & lvl.ToString() & "] " & msg & vbCrLf)
        End If
    End Sub
    Public Sub ShowErrMsg(msg As String)
        Log(msg, LogLevel.ERR)
        MsgBox(msg, vbCritical)
    End Sub

    Public Sub SetLogLvl(lvl As LogLevel)
        _logLvl = lvl.ToInteger()
    End Sub

    Public Sub SetLogLvl(lvl As Integer)
        _logLvl = lvl
    End Sub

    'Error Levels
    Public Class LogLevel
        Private _key As String
        Private _lvl As Integer

        Public Shared ReadOnly DEBUG As LogLevel = New LogLevel("DEBUG", 0)
        Public Shared ReadOnly INFO As LogLevel = New LogLevel("INFO", 1)
        Public Shared ReadOnly WARN As LogLevel = New LogLevel("WARNING", 2)
        Public Shared ReadOnly ERR As LogLevel = New LogLevel("ERROR", 3)
        Public Shared ReadOnly COMIN As LogLevel = New LogLevel("COMIN", 1)
        Public Shared ReadOnly COMOUT As LogLevel = New LogLevel("COMOUT", 1)

        Private Sub New(key As String, prio As Integer)
            Me._key = key
            Me._lvl = prio
        End Sub

        Public Overrides Function ToString() As String
            Return Me._key.ToUpper()
        End Function

        Public Function ToInteger() As Integer
            Return Me._lvl
        End Function
    End Class
End Class

' Log Event Parameter
Friend Class LogEventArgs : Inherits EventArgs
    Private _LogMsg As String
    Private _LogLvl As Logger.LogLevel

    Public Sub New(LogMsg As String, LogLvl As Logger.LogLevel)
        _LogMsg = LogMsg
        _LogLvl = LogLvl
    End Sub
    Public ReadOnly Property LogMsg As String
        Get
            Return _LogMsg
        End Get
    End Property
    Public ReadOnly Property LogLvl As Logger.LogLevel
        Get
            Return _LogLvl
        End Get
    End Property
End Class
