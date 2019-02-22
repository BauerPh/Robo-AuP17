Imports WeifenLuo.WinFormsUI.Docking
Public Class CustomFloatWindow
    Inherits FloatWindow

    Public Sub New(dockPanel As DockPanel, pane As DockPane)
        MyBase.New(dockPanel, pane)
        FormBorderStyle = FormBorderStyle.SizableToolWindow
        MinimumSize = New Size(150, 150)
    End Sub

    Public Sub New(dockPanel As DockPanel, pane As DockPane, bounds As Rectangle)
        MyBase.New(dockPanel, pane, bounds)
        FormBorderStyle = FormBorderStyle.SizableToolWindow
        MinimumSize = New Size(150, 150)
    End Sub
End Class

Public Class CustomFloatWindowFactory
    Implements DockPanelExtender.IFloatWindowFactory

    Public Function CreateFloatWindow(dockPanel As DockPanel, pane As DockPane, bounds As Rectangle) As FloatWindow Implements DockPanelExtender.IFloatWindowFactory.CreateFloatWindow
        Return New CustomFloatWindow(dockPanel, pane, bounds)
    End Function

    Public Function CreateFloatWindow(dockPanel As DockPanel, pane As DockPane) As FloatWindow Implements DockPanelExtender.IFloatWindowFactory.CreateFloatWindow
        Return New CustomFloatWindow(dockPanel, pane)
    End Function
End Class