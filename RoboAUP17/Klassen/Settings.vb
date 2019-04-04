Friend Class Settings
    Private Const cDefaultConfigFile As String = "RoboParameterDefault.xml"
    Private _actFilename As String

    Public Property configFileLoaded As Boolean = False
    Public Property JointParameter As JointParameter()
    Public Property ServoParameter As ServoParameter()

    Public Sub New()
        'Datensätze erzeugen
        JointParameter = New JointParameter(5) {} '6 Joints
        ServoParameter = New ServoParameter(2) {} '3 Servos
        'Lade letzte geöffnete Datei, wenn vorhanden!
        If System.IO.File.Exists(My.Settings.LastConfigFile) Then
            XMLReader(My.Settings.LastConfigFile)
            _actFilename = My.Settings.LastConfigFile
            configFileLoaded = True
        Else
            If Not loadDefaulSettings() Then
                If MessageBox.Show($"Die Konfigurationsdatei ""{cDefaultConfigFile}"" konnte nicht gefunden werden oder ist fehlerhaft. Soll sie gesucht werden?", "Konfigurationsdatei nicht gefunden!",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Error) = DialogResult.Yes Then
                    If loadSettings() Then
                        configFileLoaded = True
                    End If
                End If
            Else
                configFileLoaded = True
            End If
        End If
    End Sub
    Public Function getDefaulConfigFilename() As String
        Return cDefaultConfigFile
    End Function
    Public Function loadDefaulSettings() As Boolean
        If System.IO.File.Exists(cDefaultConfigFile) Then
            XMLReader(cDefaultConfigFile)
            _actFilename = cDefaultConfigFile
            Return True
        Else
            Return False
        End If
    End Function
    Public Function loadSettings() As Boolean
        Dim openFileDialog As New OpenFileDialog With {
            .Filter = "Konfig-Dateien (*.conf)|*.conf"
        }
        If openFileDialog.ShowDialog() = DialogResult.OK Then
            Try
                XMLReader(openFileDialog.FileName)
                My.Settings.LastConfigFile = openFileDialog.FileName
                My.Settings.Save()
                _actFilename = openFileDialog.FileName
                Return True
            Catch e As Exception
                Return False
            End Try
        End If
        Return False
    End Function

    Public Function saveSettings() As Boolean
        Dim saveFileDialog As New SaveFileDialog With {
            .Filter = "Konfig-Dateien (*.conf)|*.conf"
        }
        If saveFileDialog.ShowDialog() = DialogResult.OK Then
            Try
                XMLWriter(saveFileDialog.FileName)
                My.Settings.LastConfigFile = saveFileDialog.FileName
                My.Settings.Save()
                _actFilename = saveFileDialog.FileName
                Return True
            Catch e As Exception
                Return False
            End Try
        End If
        Return False
    End Function

    Public Function getActFilename() As String
        If _actFilename <> Nothing Then
            Return _actFilename
        Else
            Return ""
        End If
    End Function

    Private Sub XMLWriter(filename As String)
        Dim enc As New System.Text.UnicodeEncoding
        Dim XMLobj As Xml.XmlTextWriter = New Xml.XmlTextWriter(filename, enc)

        With XMLobj
            .Formatting = Xml.Formatting.Indented
            .Indentation = 4
            .WriteStartDocument()
            .WriteStartElement("Settings")
            For i = 0 To 5
                .WriteStartElement("J" & (i + 1).ToString())

                .WriteStartElement("mot")
                .WriteAttributeString("stepsPerRot", JointParameter(i).mot.stepsPerRot.ToString)
                .WriteAttributeString("gear", JointParameter(i).mot.gear.ToString)
                .WriteAttributeString("mode", JointParameter(i).mot.mode.ToString)
                .WriteAttributeString("dir", JointParameter(i).mot.dir.ToString)
                .WriteEndElement()

                .WriteStartElement("mech")
                .WriteAttributeString("gear", JointParameter(i).mech.gear.ToString)
                .WriteAttributeString("minAngle", JointParameter(i).mech.minAngle.ToString)
                .WriteAttributeString("maxAngle", JointParameter(i).mech.maxAngle.ToString)
                .WriteAttributeString("homePosAngle", JointParameter(i).mech.homePosAngle.ToString)
                .WriteAttributeString("parkPosAngle", JointParameter(i).mech.parkPosAngle.ToString)
                .WriteEndElement()

                .WriteStartElement("cal")
                .WriteAttributeString("dir", JointParameter(i).cal.dir.ToString)
                .WriteAttributeString("speedFast", JointParameter(i).cal.speedFast.ToString)
                .WriteAttributeString("speedSlow", JointParameter(i).cal.speedSlow.ToString)
                .WriteAttributeString("acc", JointParameter(i).cal.acc.ToString)
                .WriteEndElement()

                .WriteStartElement("profile")
                .WriteAttributeString("maxSpeed", JointParameter(i).profile.maxSpeed.ToString)
                .WriteAttributeString("maxAcc", JointParameter(i).profile.maxAcc.ToString)
                .WriteAttributeString("stopAcc", JointParameter(i).profile.stopAcc.ToString)
                .WriteEndElement()

                .WriteEndElement()
            Next
            For i = 0 To 2
                .WriteStartElement("SRV" & (i + 1).ToString())

                .WriteStartElement("angles")
                .WriteAttributeString("minAngle", ServoParameter(i).minAngle.ToString)
                .WriteAttributeString("maxAngle", ServoParameter(i).maxAngle.ToString)
                .WriteEndElement()

                .WriteEndElement()
            Next
            .WriteEndElement()
            .Close()
        End With
    End Sub

    Public Sub XMLReader(filename As String)
        Dim XMLReader As Xml.XmlReader = New Xml.XmlTextReader(filename)
        Dim i As Int32 'Index
        Dim joints As Boolean = False

        With XMLReader
            Do While .Read ' Es sind noch Daten vorhanden 
                If .NodeType = Xml.XmlNodeType.Element Then
                    Dim e As String = .Name 'Elementname
                    If e.Substring(0, 1) = "J" Then
                        i = CInt(e.Substring(1)) - 1 'Joint Nr
                        joints = True
                    ElseIf e.Substring(0, 3) = "SRV" Then
                        i = CInt(e.Substring(3)) - 1
                        joints = False
                    End If
                    If .AttributeCount > 0 Then 'sind überhaupt Attribute vorhanden?
                        While .MoveToNextAttribute 'Attribute durchlaufen
                            If joints Then
                                '********** JOINTS **********
                                Select Case e
                                    Case "mot"
                                        Select Case .Name
                                            Case "stepsPerRot"
                                                JointParameter(i).mot.stepsPerRot = CInt(.Value)
                                            Case "gear"
                                                JointParameter(i).mot.gear = CDec(.Value)
                                            Case "mode"
                                                JointParameter(i).mot.mode = CInt(.Value)
                                            Case "dir"
                                                JointParameter(i).mot.dir = CInt(.Value)
                                        End Select
                                    Case "mech"
                                        Select Case .Name
                                            Case "gear"
                                                JointParameter(i).mech.gear = CDec(.Value)
                                            Case "minAngle"
                                                JointParameter(i).mech.minAngle = CDec(.Value)
                                            Case "maxAngle"
                                                JointParameter(i).mech.maxAngle = CDec(.Value)
                                            Case "homePosAngle"
                                                JointParameter(i).mech.homePosAngle = CDec(.Value)
                                            Case "parkPosAngle"
                                                JointParameter(i).mech.parkPosAngle = CDec(.Value)
                                        End Select
                                    Case "cal"
                                        Select Case .Name
                                            Case "dir"
                                                JointParameter(i).cal.dir = CInt(.Value)
                                            Case "speedFast"
                                                JointParameter(i).cal.speedFast = CDec(.Value)
                                            Case "speedSlow"
                                                JointParameter(i).cal.speedSlow = CDec(.Value)
                                            Case "acc"
                                                JointParameter(i).cal.acc = CDec(.Value)
                                        End Select
                                    Case "profile"
                                        Select Case .Name
                                            Case "maxSpeed"
                                                JointParameter(i).profile.maxSpeed = CDec(.Value)
                                            Case "maxAcc"
                                                JointParameter(i).profile.maxAcc = CDec(.Value)
                                            Case "stopAcc"
                                                JointParameter(i).profile.stopAcc = CDec(.Value)
                                        End Select
                                End Select
                            Else
                                '********** SERVOS **********
                                Select Case e
                                    Case "angles"
                                        Select Case .Name
                                            Case "minAngle"
                                                ServoParameter(i).minAngle = CDec(.Value)
                                            Case "maxAngle"
                                                ServoParameter(i).maxAngle = CDec(.Value)
                                        End Select
                                End Select
                            End If
                        End While
                    End If
                End If
            Loop
            .Close()
        End With
    End Sub
End Class
