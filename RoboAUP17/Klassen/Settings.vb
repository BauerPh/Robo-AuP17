Friend Class Settings
    ' -----------------------------------------------------------------------------
    ' Definitions
    ' -----------------------------------------------------------------------------
    Private Const cDefaultConfigFile As String = "RoboParameterDefault.xml"
    Private _configFileLoaded As Boolean = False

    Private _actFilename As String
    Private _jointParameter(5) As JointParameter
    Private _servoParameter(2) As ServoParameter
    Private _denavitHartenbergParameter(5) As DHParameter
    Private _toolframe As CartCoords
    Private _workframe As CartCoords
    Private _tcpServerParameter As TCPServerParameter
    Private _unsavedChanges As Boolean = False

    ' Properties
    Friend ReadOnly Property JointParameter As JointParameter()
        Get
            Return _jointParameter
        End Get
    End Property
    Friend ReadOnly Property ServoParameter As ServoParameter()
        Get
            Return _servoParameter
        End Get
    End Property
    Friend ReadOnly Property DenavitHartenbergParameter As DHParameter()
        Get
            Return _denavitHartenbergParameter
        End Get
    End Property
    Friend ReadOnly Property Toolframe As CartCoords
        Get
            Return _toolframe
        End Get
    End Property
    Friend ReadOnly Property Workframe As CartCoords
        Get
            Return _workframe
        End Get
    End Property
    Friend ReadOnly Property TCPServerParameter As TCPServerParameter
        Get
            Return _tcpServerParameter
        End Get
    End Property
    Public ReadOnly Property ConfigFileLoaded As Boolean
        Get
            Return _configFileLoaded
        End Get
    End Property
    Public ReadOnly Property UnsavedChanges As Boolean
        Get
            Return _unsavedChanges
        End Get
    End Property

    ' Events
    Friend Enum ParameterChangedParameter
        All = 0
        Joint
        Servo
        DenavitHartenbergParameter
        Toolframe
        Workframe
        TCPServerParameter
    End Enum
    Friend Event ParameterChanged(ByVal changedParameter As ParameterChangedParameter)
    Friend Event Log(ByVal LogMsg As String, ByVal LogLvl As Logger.LogLevel)

    ' -----------------------------------------------------------------------------
    ' Constructor
    ' -----------------------------------------------------------------------------
    Friend Sub New()
        'Lade letzte geöffnete Datei, wenn vorhanden!
        If _XMLReader(My.Settings.LastConfigFile) Then
            _configFileLoaded = True
            _actFilename = My.Settings.LastConfigFile
        Else
            If My.Settings.LastConfigFile.Length > 0 Then
                MessageBox.Show($"Die Parameterdatei ""{My.Settings.LastConfigFile}"" konnte nicht geladen werden. Es werden die Standardparameter geladen.", "Parameterdatei nicht gefunden!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
            If Not LoadDefaulSettings() Then
                If MessageBox.Show($"Die Parameterdatei ""{cDefaultConfigFile}"" konnte nicht gefunden werden oder ist fehlerhaft. Soll sie gesucht werden?", "Parameterdatei nicht gefunden!",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Error) = DialogResult.Yes Then
                    LoadSettings()
                End If
            End If
        End If
    End Sub

    ' -----------------------------------------------------------------------------
    ' Public
    ' -----------------------------------------------------------------------------
    Friend Sub SetJointParameter(index As Integer, jointParameter As JointParameter)
        _jointParameter(index) = jointParameter
        _unsavedChanges = True
        RaiseEvent ParameterChanged(ParameterChangedParameter.Joint)
    End Sub
    Friend Sub SetServoParameter(index As Integer, servoParameter As ServoParameter)
        _servoParameter(index) = servoParameter
        _unsavedChanges = True
        RaiseEvent ParameterChanged(ParameterChangedParameter.Servo)
    End Sub
    Friend Sub SetDenavitHartenbergParameter(index As Integer, dhParameter As DHParameter)
        _denavitHartenbergParameter(index) = dhParameter
        _unsavedChanges = True
        RaiseEvent ParameterChanged(ParameterChangedParameter.DenavitHartenbergParameter)
    End Sub
    Friend Sub SetToolframe(toolframe As CartCoords)
        _toolframe = toolframe
        _unsavedChanges = True
        RaiseEvent ParameterChanged(ParameterChangedParameter.Toolframe)
    End Sub
    Friend Sub SetWorkframe(workframe As CartCoords)
        _workframe = workframe
        _unsavedChanges = True
        RaiseEvent ParameterChanged(ParameterChangedParameter.Workframe)
    End Sub
    Friend Sub SetTCPServerParameter(tcpServerParameter As TCPServerParameter)
        _tcpServerParameter = tcpServerParameter
        _unsavedChanges = True
        RaiseEvent ParameterChanged(ParameterChangedParameter.TCPServerParameter)
    End Sub
    Friend Function LoadDefaulSettings() As Boolean
        If System.IO.File.Exists(cDefaultConfigFile) Then
            _XMLReader(cDefaultConfigFile)
            My.Settings.LastConfigFile = cDefaultConfigFile
            My.Settings.Save()
            _actFilename = cDefaultConfigFile
            RaiseEvent ParameterChanged(ParameterChangedParameter.All)
            RaiseEvent Log($"[Parameter] Standardparameter geladen", Logger.LogLevel.ERR)
            _configFileLoaded = True
            _unsavedChanges = False
            Return True
        Else
            Return False
        End If
    End Function
    Friend Function LoadSettings() As Boolean
        Dim openFileDialog As New OpenFileDialog With {
            .Filter = "Parameter-Dateien (*.xml)|*.xml"
        }
        If openFileDialog.ShowDialog() = DialogResult.OK Then
            Try
                If Not _XMLReader(openFileDialog.FileName) Then Return False
                My.Settings.LastConfigFile = openFileDialog.FileName
                My.Settings.Save()
                _actFilename = openFileDialog.FileName
                RaiseEvent ParameterChanged(ParameterChangedParameter.All)
                RaiseEvent Log("[Parameter] Parameterdatei geladen", Logger.LogLevel.INFO)
                _configFileLoaded = True
                _unsavedChanges = False
                Return True
            Catch e As Exception
                RaiseEvent Log($"[Parameter] Laden fehlgeschlagen, Fehler: {e.Message}", Logger.LogLevel.ERR)
                Return False
            End Try
        End If
        Return False
    End Function

    Friend Function SaveSettings() As Boolean
        Dim saveFileDialog As New SaveFileDialog With {
            .Filter = "Parameter-Dateien (*.xml)|*.xml"
        }
        If saveFileDialog.ShowDialog() = DialogResult.OK Then
            Try
                _XMLWriter(saveFileDialog.FileName)
                My.Settings.LastConfigFile = saveFileDialog.FileName
                My.Settings.Save()
                _actFilename = saveFileDialog.FileName
                RaiseEvent Log("[Parameter] Parameterdatei gespeichert", Logger.LogLevel.INFO)
                _unsavedChanges = False
                Return True
            Catch e As Exception
                RaiseEvent Log($"[Parameter] Speichern fehlgeschlagen, Fehler: {e.Message}", Logger.LogLevel.ERR)
                Return False
            End Try
        End If
        Return False
    End Function
    Friend Function GetDefaulConfigFilename() As String
        Return cDefaultConfigFile
    End Function
    Friend Function GetActFilename() As String
        If _actFilename <> Nothing Then
            Return _actFilename
        Else
            Return ""
        End If
    End Function

    ' -----------------------------------------------------------------------------
    ' Private
    ' -----------------------------------------------------------------------------
    Private Sub _XMLWriter(filename As String)
        Dim enc As New System.Text.UnicodeEncoding
        Using XMLTextWriter As New Xml.XmlTextWriter(filename, enc)
            With XMLTextWriter
                .Formatting = Xml.Formatting.Indented
                .Indentation = 4
                .WriteStartDocument()
                .WriteStartElement("Settings")
                'Joints
                For i = 0 To 5
                    .WriteStartElement("J" & (i + 1).ToString())

                    .WriteStartElement("mot")
                    .WriteAttributeString("stepsPerRot", _jointParameter(i).MotStepsPerRot.ToString)
                    .WriteAttributeString("gear", _jointParameter(i).MotGear.ToString)
                    .WriteAttributeString("mode", _jointParameter(i).MotMode.ToString)
                    .WriteAttributeString("dir", _jointParameter(i).MotDir.ToString)
                    .WriteEndElement()

                    .WriteStartElement("mech")
                    .WriteAttributeString("gear", _jointParameter(i).MechGear.ToString)
                    .WriteAttributeString("minAngle", _jointParameter(i).MechMinAngle.ToString)
                    .WriteAttributeString("maxAngle", _jointParameter(i).MechMaxAngle.ToString)
                    .WriteAttributeString("homePosAngle", _jointParameter(i).MechHomePosAngle.ToString)
                    .WriteAttributeString("parkPosAngle", _jointParameter(i).MechParkPosAngle.ToString)
                    .WriteEndElement()

                    .WriteStartElement("cal")
                    .WriteAttributeString("dir", _jointParameter(i).CalDir.ToString)
                    .WriteAttributeString("speedFast", _jointParameter(i).CalSpeedFast.ToString)
                    .WriteAttributeString("speedSlow", _jointParameter(i).CalSpeedSlow.ToString)
                    .WriteAttributeString("acc", _jointParameter(i).CalAcc.ToString)
                    .WriteEndElement()

                    .WriteStartElement("profile")
                    .WriteAttributeString("minSpeed", _jointParameter(i).ProfileMinSpeed.ToString)
                    .WriteAttributeString("maxSpeed", _jointParameter(i).ProfileMaxSpeed.ToString)
                    .WriteAttributeString("maxAcc", _jointParameter(i).ProfileMaxAcc.ToString)
                    .WriteAttributeString("stopAcc", _jointParameter(i).ProfileStopAcc.ToString)
                    .WriteEndElement()

                    .WriteStartElement("denavitHartenbergParameter")
                    .WriteAttributeString("alpha", _denavitHartenbergParameter(i).alpha.ToString)
                    .WriteAttributeString("d", _denavitHartenbergParameter(i).d.ToString)
                    .WriteAttributeString("a", _denavitHartenbergParameter(i).a.ToString)
                    .WriteEndElement()

                    .WriteEndElement()
                Next
                'Servos
                For i = 0 To 2
                    .WriteStartElement("SRV" & (i + 1).ToString())

                    .WriteAttributeString("available", _servoParameter(i).Available.ToString)

                    .WriteStartElement("angles")
                    .WriteAttributeString("minAngle", _servoParameter(i).MinAngle.ToString)
                    .WriteAttributeString("maxAngle", _servoParameter(i).MaxAngle.ToString)
                    .WriteEndElement()

                    .WriteEndElement()
                Next
                'Toolframe
                .WriteStartElement("toolframe")
                .WriteAttributeString("x", _toolframe.X.ToString)
                .WriteAttributeString("y", _toolframe.Y.ToString)
                .WriteAttributeString("z", _toolframe.Z.ToString)
                .WriteAttributeString("yaw", _toolframe.Yaw.ToString)
                .WriteAttributeString("pitch", _toolframe.Pitch.ToString)
                .WriteAttributeString("roll", _toolframe.Roll.ToString)
                .WriteEndElement()
                'Workframe
                .WriteStartElement("workframe")
                .WriteAttributeString("x", _workframe.X.ToString)
                .WriteAttributeString("y", _workframe.Y.ToString)
                .WriteAttributeString("z", _workframe.Z.ToString)
                .WriteAttributeString("yaw", _workframe.Yaw.ToString)
                .WriteAttributeString("pitch", _workframe.Pitch.ToString)
                .WriteAttributeString("roll", _workframe.Roll.ToString)
                .WriteEndElement()
                'TCP
                .WriteStartElement("tcpServerParameter")
                .WriteAttributeString("listen", _tcpServerParameter.Listen.ToString)
                .WriteAttributeString("port", _tcpServerParameter.Port.ToString)
                .WriteEndElement()
                'Settings
                .WriteEndElement()
                .Close()
            End With
        End Using
    End Sub

    Private Function _XMLReader(filename As String) As Boolean
        'Prüfe ob Datei existiert
        If Not System.IO.File.Exists(filename) Then Return False

        Using XMLReader As New Xml.XmlTextReader(filename)
            Dim i As Int32 'Index
            Dim setting As Integer = 0
            Dim valid As Boolean = False

            With XMLReader
                Do While .Read ' Es sind noch Daten vorhanden 
                    If .NodeType = Xml.XmlNodeType.Element Then
                        Dim e As String = .Name 'Elementname
                        If e = "Settings" And Not valid Then valid = True
                        If Not valid Then
                            .Close()
                            RaiseEvent Log($"[Parameter] Dies ist keine korrekte Parameterdatei oder die Datei ist beschädigt", Logger.LogLevel.ERR)
                            Return False
                        End If
                        If e.Substring(0, 1) = "J" Then
                            i = CInt(e.Substring(1)) - 1 'Joint Nr
                            setting = 1
                        ElseIf e.Substring(0, 3) = "SRV" Then
                            i = CInt(e.Substring(3)) - 1
                            setting = 2
                        ElseIf e = "toolframe" Then
                            setting = 3
                        ElseIf e = "workframe" Then
                            setting = 4
                        ElseIf e = "tcpServerParameter" Then
                            setting = 5
                        End If
                        If .AttributeCount > 0 Then 'sind überhaupt Attribute vorhanden?
                            While .MoveToNextAttribute 'Attribute durchlaufen
                                If setting = 1 Then
                                    '********** JOINTS **********
                                    Select Case e
                                        Case "mot"
                                            Select Case .Name
                                                Case "stepsPerRot"
                                                    _jointParameter(i).MotStepsPerRot = CInt(.Value)
                                                Case "gear"
                                                    _jointParameter(i).MotGear = CDbl(.Value)
                                                Case "mode"
                                                    _jointParameter(i).MotMode = CType([Enum].Parse(GetType(MotMode), .Value), MotMode)  'CType(CInt(.Value), motMode)
                                                Case "dir"
                                                    _jointParameter(i).MotDir = CType([Enum].Parse(GetType(MotDir), .Value), MotDir)
                                            End Select
                                        Case "mech"
                                            Select Case .Name
                                                Case "gear"
                                                    _jointParameter(i).MechGear = CDbl(.Value)
                                                Case "minAngle"
                                                    _jointParameter(i).MechMinAngle = CDbl(.Value)
                                                Case "maxAngle"
                                                    _jointParameter(i).MechMaxAngle = CDbl(.Value)
                                                Case "homePosAngle"
                                                    _jointParameter(i).MechHomePosAngle = CDbl(.Value)
                                                Case "parkPosAngle"
                                                    _jointParameter(i).MechParkPosAngle = CDbl(.Value)
                                            End Select
                                        Case "cal"
                                            Select Case .Name
                                                Case "dir"
                                                    _jointParameter(i).CalDir = CType([Enum].Parse(GetType(CalDir), .Value), CalDir)
                                                Case "speedFast"
                                                    _jointParameter(i).CalSpeedFast = CDbl(.Value)
                                                Case "speedSlow"
                                                    _jointParameter(i).CalSpeedSlow = CDbl(.Value)
                                                Case "acc"
                                                    _jointParameter(i).CalAcc = CDbl(.Value)
                                            End Select
                                        Case "profile"
                                            Select Case .Name
                                                Case "minSpeed"
                                                    _jointParameter(i).ProfileMinSpeed = CDbl(.Value)
                                                Case "maxSpeed"
                                                    _jointParameter(i).ProfileMaxSpeed = CDbl(.Value)
                                                Case "maxAcc"
                                                    _jointParameter(i).ProfileMaxAcc = CDbl(.Value)
                                                Case "stopAcc"
                                                    _jointParameter(i).ProfileStopAcc = CDbl(.Value)
                                            End Select
                                        Case "denavitHartenbergParameter"
                                            Select Case .Name
                                                Case "alpha"
                                                    _denavitHartenbergParameter(i).alpha = CDbl(.Value)
                                                Case "d"
                                                    _denavitHartenbergParameter(i).d = CDbl(.Value)
                                                Case "a"
                                                    _denavitHartenbergParameter(i).a = CDbl(.Value)
                                            End Select
                                    End Select
                                ElseIf setting = 2 Then
                                    '********** SERVOS **********
                                    Select Case e
                                        Case "angles"
                                            Select Case .Name
                                                Case "minAngle"
                                                    _servoParameter(i).MinAngle = CDbl(.Value)
                                                Case "maxAngle"
                                                    _servoParameter(i).MaxAngle = CDbl(.Value)
                                            End Select
                                        Case Else
                                            If .Name = "available" Then
                                                _servoParameter(i).Available = CBool(.Value)
                                            End If
                                    End Select
                                ElseIf setting = 3 Then
                                    '********** WORKFRAME **********
                                    Select Case .Name
                                        Case "x"
                                            _toolframe.X = CDbl(.Value)
                                        Case "y"
                                            _toolframe.Y = CDbl(.Value)
                                        Case "z"
                                            _toolframe.Z = CDbl(.Value)
                                        Case "yaw"
                                            _toolframe.Yaw = CDbl(.Value)
                                        Case "pitch"
                                            _toolframe.Pitch = CDbl(.Value)
                                        Case "roll"
                                            _toolframe.Roll = CDbl(.Value)
                                    End Select
                                ElseIf setting = 4 Then
                                    '********** TOOLFRAME **********
                                    Select Case .Name
                                        Case "x"
                                            _workframe.X = CDbl(.Value)
                                        Case "y"
                                            _workframe.Y = CDbl(.Value)
                                        Case "z"
                                            _workframe.Z = CDbl(.Value)
                                        Case "yaw"
                                            _workframe.Yaw = CDbl(.Value)
                                        Case "pitch"
                                            _workframe.Pitch = CDbl(.Value)
                                        Case "roll"
                                            _workframe.Roll = CDbl(.Value)
                                    End Select
                                ElseIf setting = 5 Then
                                    '********** TCP Server Parameter **********
                                    Select Case .Name
                                        Case "listen"
                                            _tcpServerParameter.Listen = CBool(.Value)
                                        Case "port"
                                            _tcpServerParameter.Port = CInt(.Value)
                                    End Select
                                End If
                            End While
                        End If
                    End If
                Loop
                .Close()
            End With
        End Using
        Return True
    End Function
End Class
