Namespace Matrizen
    ' -----------------------------------------------------------------------------
    ' TODO
    ' -----------------------------------------------------------------------------
    ' fertig

    Friend Class Matrix4x4
        Implements ICloneable
        Friend val(3, 3) As Double

        Public Shared Operator +(M1 As Matrix4x4, M2 As Matrix4x4) As Matrix4x4
            Dim erg As New Matrix4x4
            For i As Integer = 0 To 3
                For j As Integer = 0 To 3
                    erg.val(i, j) = M1.val(i, j) + M2.val(i, j)
                Next
            Next
            Return erg
        End Operator

        Public Shared Operator *(M1 As Matrix4x4, M2 As Matrix4x4) As Matrix4x4
            Dim erg As New Matrix4x4
            For j As Integer = 0 To 3
                For i As Integer = 0 To 3
                    For k As Integer = 0 To 3
                        erg.val(i, j) += M1.val(k, j) * M2.val(i, k)
                    Next
                Next
            Next
            Return erg
        End Operator

        Friend Overridable Function Clone() As Object Implements ICloneable.Clone
            Dim temp As New Matrix4x4
            temp.val = CType(val.Clone, Double(,))
            Return temp
        End Function
    End Class

End Namespace
