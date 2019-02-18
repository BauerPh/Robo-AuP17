Option Strict On
Namespace Matrizen

    Public Class Matrix4x4
        Implements ICloneable
        Public val(3, 3) As Double

        Public Function Multiply(M As Matrix4x4) As Matrix4x4
            Dim erg As New Matrix4x4
            For j As Integer = 0 To 3
                For i As Integer = 0 To 3
                    For k As Integer = 0 To 3
                        erg.val(i, j) += val(k, j) * M.val(i, k)
                    Next
                Next
            Next
            Return erg
        End Function

        Public Function Add(M As Matrix4x4) As Matrix4x4
            Dim erg As New Matrix4x4
            For i As Integer = 0 To 3
                For j As Integer = 0 To 3
                    erg.val(i, j) = val(i, j) + M.val(i, j)
                Next
            Next
            Return erg
        End Function

        Public Overridable Function Clone() As Object Implements ICloneable.Clone
            Dim temp As New Matrix4x4
            temp.val = CType(val.Clone, Double(,))
            Return temp
        End Function
    End Class

End Namespace
