Public Class panRoboStatus
    ' -----------------------------------------------------------------------------
    ' TODO
    ' -----------------------------------------------------------------------------
    ' Serielle Verbindung
    ' Referenzstatus
    ' Endschalter Zustand
    ' Nothalt Zustand

    ' -----------------------------------------------------------------------------
    ' Init Panel
    ' -----------------------------------------------------------------------------
    Private Sub panRoboStatus_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim ImgRef As New ImageList
        ImgRef.ImageSize = New Size(20, 20)
        ImgRef.Images.Add(My.Resources.ico_error)
        ImgRef.Images.Add(My.Resources.ico_ok)
        lblJ1HeadingImg.ImageList = ImgRef
        lblJ1HeadingImg.ImageIndex = 0
        lblJ2HeadingImg.ImageList = ImgRef
        lblJ2HeadingImg.ImageIndex = 1
        lblJ3HeadingImg.ImageList = ImgRef
        lblJ3HeadingImg.ImageIndex = 0
        lblJ4HeadingImg.ImageList = ImgRef
        lblJ4HeadingImg.ImageIndex = 0
        lblJ5HeadingImg.ImageList = ImgRef
        lblJ5HeadingImg.ImageIndex = 0
        lblJ6HeadingImg.ImageList = ImgRef
        lblJ6HeadingImg.ImageIndex = 0

        Dim ImgListEss As New ImageList
        ImgListEss.ImageSize = New Size(20, 20)
        ImgListEss.Images.Add(My.Resources.button_red)
        ImgListEss.Images.Add(My.Resources.button_green)
        lblJ1LSState.ImageList = ImgListEss
        lblJ1LSState.ImageIndex = 0
    End Sub

    Private Sub TableLayoutPanel_CellPaint(sender As Object, e As TableLayoutCellPaintEventArgs) Handles TableLayoutPanel.CellPaint
        Dim lineRow() = {1, 2, 4, 5, 7}
        ' Rahmen Zeichnen
        If e.Column Mod 2 = 0 And e.Column <> 0 _
            And lineRow.Contains(e.Row) Then
            e.Graphics.DrawLine(Pens.Black, e.CellBounds.Location, New Point(e.CellBounds.Left, e.CellBounds.Bottom))
        End If
        If e.Row = 2 Or e.Row = 5 Then
            Dim pen As New Pen(Color.Black)
            pen.DashStyle = Drawing2D.DashStyle.Dot
            e.Graphics.DrawLine(pen, e.CellBounds.Location, New Point(e.CellBounds.Right, e.CellBounds.Top))
        End If
    End Sub



    ' -----------------------------------------------------------------------------
    ' Form Control
    ' -----------------------------------------------------------------------------

End Class