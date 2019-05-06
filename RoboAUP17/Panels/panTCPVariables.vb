Public Class panTCPVariables
    ' -----------------------------------------------------------------------------
    ' Init Panel
    ' -----------------------------------------------------------------------------
    Private Sub panVariables_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect

        _updateTCPInfo()

        AddHandler frmMain.ACLProgram.TcpVariables.Connected, AddressOf _eConnected
        AddHandler frmMain.ACLProgram.TcpVariables.Disconnected, AddressOf _eDisconnected
        AddHandler frmMain.ACLProgram.TcpVariables.ConnectError, AddressOf _eConnectError
        AddHandler frmMain.ACLProgram.TcpVariables.VariableChanged, AddressOf _eVariableChanged
        AddHandler frmMain.RoboControl.RoboParameterChanged, AddressOf _eRoboParameterChanged
        AddHandler frmMain.ACLProgram.ProgramUpdatedEvent, AddressOf _eProgramUpdated
    End Sub

    ' -----------------------------------------------------------------------------
    ' Form Control
    ' -----------------------------------------------------------------------------
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If frmMain.ACLProgram.TcpVariables.AddVariable(tbName.Text) Then
            frmMain.ACLProgram.UnsavedChanges = True
            _refreshDataGridView()
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If dataGridView.SelectedRows.Count > 0 Then
            If frmMain.ACLProgram.TcpVariables.RemoveVariable(CStr(dataGridView.SelectedRows.Item(0).Cells(0).Value)) Then
                frmMain.ACLProgram.UnsavedChanges = True
                _refreshDataGridView()
            End If
        End If
    End Sub

    Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click
        btnConnect.Enabled = False
        frmMain.Log("[TCP] Verbindung wird hergestellt...", Logger.LogLevel.INFO)
        If Not frmMain.ACLProgram.ConnectToTCPServer() Then
            frmMain.Log("[TCP] Server aktiviert oder bereits verbunden", Logger.LogLevel.ERR)
            frmMain.ShowStatusStripHint("TCP-Server aktiviert oder Client bereits verbunden")
        End If
    End Sub

    Private Sub btnDisconnect_Click(sender As Object, e As EventArgs) Handles btnDisconnect.Click
        btnDisconnect.Enabled = False
        frmMain.Log("[TCP] Verbindung wird getrennt...", Logger.LogLevel.INFO)
        frmMain.ACLProgram.TcpVariables.TerminateConnection()
    End Sub

    ' -----------------------------------------------------------------------------
    ' Private
    ' -----------------------------------------------------------------------------
    Private Sub _refreshDataGridView()
        If InvokeRequired Then
            Invoke(Sub() _refreshDataGridView())
            Return
        End If
        dataGridView.DataSource = frmMain.ACLProgram.TcpVariables.GetGridViewDataSource
    End Sub
    Private Sub _updateTCPInfo()
        Dim visible As Boolean = frmMain.RoboControl.Pref.TcpParameter.Mode = TCPMode.aktiv
        sepConnect.Visible = visible
        btnConnect.Visible = visible
        btnDisconnect.Visible = visible
        lblConnectStatusTitle.Text = If(frmMain.RoboControl.Pref.TcpParameter.Mode = TCPMode.aktiv, "TCP-Client:", "TCP-Server:")
    End Sub

    ' -----------------------------------------------------------------------------
    ' Events
    ' -----------------------------------------------------------------------------
    Private Sub _eConnected()
        If InvokeRequired Then
            Invoke(Sub() _eConnected())
            Return
        End If
        btnConnect.Enabled = False
        btnDisconnect.Enabled = True
        lblConnectStatus.Text = "Verbunden"
        lblConnectStatus.ForeColor = Color.Green
        frmMain.Log("[TCP] Verbunden!", Logger.LogLevel.INFO)
    End Sub
    Private Sub _eDisconnected()
        If InvokeRequired Then
            Invoke(Sub() _eDisconnected())
            Return
        End If
        btnConnect.Enabled = True
        btnDisconnect.Enabled = False
        lblConnectStatus.Text = "Getrennt"
        lblConnectStatus.ForeColor = Color.Red
        frmMain.Log("[TCP] Getrennt!", Logger.LogLevel.INFO)
    End Sub
    Private Sub _eConnectError()
        If InvokeRequired Then
            Invoke(Sub() _eConnectError())
            Return
        End If

        btnConnect.Enabled = True
        lblConnectStatus.Text = "Getrennt"
        lblConnectStatus.ForeColor = Color.Red
        frmMain.Log("[TCP] Verbindungsfehler!", Logger.LogLevel.ERR)
        frmMain.ShowStatusStripHint("TCP-Verbindung fehlgeschlagen")
    End Sub
    Private Sub _eVariableChanged(name As String, value As Double)
        If InvokeRequired Then
            Invoke(Sub() _eVariableChanged(name, value))
            Return
        End If
        frmMain.Log($"[TCP] Variable geändert: {name} = {value}", Logger.LogLevel.INFO)
        _refreshDataGridView()
    End Sub
    Private Sub _eRoboParameterChanged(parameterChanged As Settings.ParameterChangedParameter)
        Dim all As Boolean = parameterChanged = Settings.ParameterChangedParameter.All
        If parameterChanged = Settings.ParameterChangedParameter.TcpParameter Or all Then
            Dim enabled = frmMain.RoboControl.Pref.TcpParameter.Enabled Or frmMain.RoboControl.Pref.TcpParameter.Mode = TCPMode.aktiv
            If enabled Then
                lblConnectStatus.Text = "nicht verbunden"
                lblConnectStatus.ForeColor = Color.Red
            Else
                lblConnectStatus.Text = "Server deaktiviert"
                lblConnectStatus.ForeColor = Color.Red
            End If
            _updateTCPInfo()
        End If
    End Sub
    Private Sub _eProgramUpdated()
        _refreshDataGridView()
    End Sub
End Class