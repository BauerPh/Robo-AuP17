﻿Option Strict On
Public Class panTCPVariables
    ' -----------------------------------------------------------------------------
    ' Init Panel
    ' -----------------------------------------------------------------------------
    Private Sub panVariables_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect

        AddHandler frmMain.ACLProgram.TcpVariables.Connected, AddressOf _eConnected
        AddHandler frmMain.ACLProgram.TcpVariables.Disconnected, AddressOf _eDisconnected
        AddHandler frmMain.ACLProgram.TcpVariables.VariableChanged, AddressOf _eVariableChanged
        AddHandler frmMain.RoboControl.RoboParameterChanged, AddressOf _eRoboParameterChanged
    End Sub

    ' -----------------------------------------------------------------------------
    ' Form Control
    ' -----------------------------------------------------------------------------
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        frmMain.ACLProgram.TcpVariables.AddVariable(tbName.Text)
        _refreshDataGridView()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If dataGridView.SelectedRows.Count > 0 Then
            frmMain.ACLProgram.TcpVariables.RemoveVariable(CStr(dataGridView.SelectedRows.Item(0).Cells(0).Value))
            _refreshDataGridView()
        End If
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

    ' -----------------------------------------------------------------------------
    ' Events
    ' -----------------------------------------------------------------------------
    Private Sub _eConnected()
        If InvokeRequired Then
            Invoke(Sub() _eConnected())
            Return
        End If

        lblConnectStatus.Text = "Verbunden"
        lblConnectStatus.ForeColor = Color.Green
    End Sub
    Private Sub _eDisconnected()
        If InvokeRequired Then
            Invoke(Sub() _eDisconnected())
            Return
        End If

        lblConnectStatus.Text = "nicht verbunden"
        lblConnectStatus.ForeColor = Color.Red
    End Sub
    Private Sub _eVariableChanged(name As String, value As Integer)
        _refreshDataGridView()
    End Sub
    Private Sub _eRoboParameterChanged(parameterChanged As Settings.ParameterChangedParameter)
        Dim all As Boolean = parameterChanged = Settings.ParameterChangedParameter.All
        If parameterChanged = Settings.ParameterChangedParameter.TCPServerParameter Or all Then
            If frmMain.RoboControl.Pref.TCPServerParameter.Listen Then
                lblConnectStatus.Text = "nicht verbunden"
                lblConnectStatus.ForeColor = Color.Red
            Else
                lblConnectStatus.Text = "Server deaktiviert"
                lblConnectStatus.ForeColor = Color.Red
            End If
        End If
    End Sub
End Class