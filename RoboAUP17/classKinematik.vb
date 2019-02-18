Imports RoboAUP17.Matrizen

Public Class classKinematik
    Private _DHParameter(5) As DHParams
    Private _transForwMatr(7) As Matrix4x4
    Private _workframe(5), _toolframe(5) As Double

    Public Structure DHParams
        Public alpha As Double
        Public d As Double
        Public a As Double
    End Structure

    Public Sub New(DenavitHartenbergParameter As DHParams())
        If UBound(DenavitHartenbergParameter) <> 5 Then
            Throw New Exception("Denavit Hartenberg Parameter für alle 6 Achsen erforderlich!")
        End If
        _DHParameter = DenavitHartenbergParameter
        For i As Int16 = 0 To 7
            _transForwMatr(i) = New Matrix4x4
        Next
    End Sub

    Public Sub SetWorkFrame(workframe As Double())
        If UBound(workframe) <> 5 Then
            Throw New Exception("Workframe muss aus 6 Werten bestehen!")
        End If
        _workframe = workframe
    End Sub

    Public Sub SetToolFrame(toolframe As Double())
        If UBound(toolframe) <> 5 Then
            Throw New Exception("Toolframe muss aus 6 Werten bestehen!")
        End If
        _toolframe = toolframe
    End Sub

    Public Function ForwardKin(joints As Double()) As Double()
        If UBound(joints) <> 5 Then
            Throw New Exception("Achswinkel für alle 6 Achsen erforderlich!")
        End If
        'Erstmal die Transformationsmatritzen für jede einzelne Achse berechnen
        'Work Frame
        _transForwMatr(0).val(0, 0) = 1
        _transForwMatr(0).val(1, 1) = 1
        _transForwMatr(0).val(2, 2) = 1
        _transForwMatr(0).val(3, 3) = 1

        'Transformationsmatrizen jeder Achse
        For i As Integer = 1 To 6
            _transForwMatr(i) = CalcDHTransMatrix(i, joints(i - 1))
        Next

        'Tool Frame
        _transForwMatr(7).val(0, 0) = 1
        _transForwMatr(7).val(1, 1) = 1
        _transForwMatr(7).val(2, 2) = 1
        _transForwMatr(7).val(3, 3) = 1

        'Matritzen multiplizieren
        Dim tmpMatr As Matrix4x4
        tmpMatr = _transForwMatr(1).Multiply(_transForwMatr(0))
        For i As Int16 = 2 To 7
            tmpMatr = _transForwMatr(i).Multiply(tmpMatr)
        Next

        'Ausgeben
        Dim erg(5) As Double
        'Position
        erg(0) = tmpMatr.val(0, 3) 'X
        erg(1) = tmpMatr.val(1, 3) 'Y
        erg(2) = tmpMatr.val(2, 3) 'Z
        'Ausrichtung
        Dim tmpP As Double = Math.Atan2(Math.Sqrt((tmpMatr.val(0, 2) ^ 2) + (tmpMatr.val(1, 2) ^ 2)), tmpMatr.val(2, 2) * -1)
        erg(3) = toGrad(Math.Atan2(tmpMatr.val(2, 0) / tmpP, tmpMatr.val(2, 1) / tmpP)) 'y
        erg(4) = toGrad(tmpP) 'p
        erg(5) = toGrad(Math.Atan2(tmpMatr.val(0, 2) / tmpP, tmpMatr.val(1, 2) / tmpP)) 'r

        Return erg
    End Function

    Public Function InversKin(coords As Double()) As Double()
        If UBound(coords) <> 5 Then
            Throw New Exception("XYZ und ypr-Winkel erforderlich!")
        End If
        'Hier inverse Kinematik berechnen


        Return coords
    End Function

    Private Function CalcDHTransMatrix(joint As Int16, theta As Double) As Matrix4x4 'Theta = Winkel einer Achse
        Dim dh As DHParams = _DHParameter(joint - 1)
        Dim thetaBog As Double = toBogenmass(theta)
        Dim erg As New Matrix4x4

        dh.alpha = toBogenmass(dh.alpha)

        erg.val(0, 0) = Math.Cos(thetaBog)
        erg.val(0, 1) = -Math.Sin(thetaBog) * Math.Cos(dh.alpha)
        erg.val(0, 2) = Math.Sin(thetaBog) * Math.Sin(dh.alpha)
        erg.val(0, 3) = dh.a * Math.Cos(thetaBog)

        erg.val(1, 0) = Math.Sin(thetaBog)
        erg.val(1, 1) = Math.Cos(thetaBog) * Math.Cos(dh.alpha)
        erg.val(1, 2) = -Math.Cos(thetaBog) * Math.Sin(dh.alpha)
        erg.val(1, 3) = dh.a * Math.Sin(thetaBog)

        erg.val(2, 0) = 0.0
        erg.val(2, 1) = Math.Sin(dh.alpha)
        erg.val(2, 2) = Math.Cos(dh.alpha)
        erg.val(2, 3) = dh.d

        erg.val(3, 0) = 0.0
        erg.val(3, 1) = 0.0
        erg.val(3, 2) = 0.0
        erg.val(3, 3) = 1.0

        Return erg
    End Function

    Private Function CalcFrameMatrix(frame As Double()) As Matrix4x4

    End Function

    Private Function toBogenmass(winkel As Double) As Double
        Return winkel * (Math.PI / 180)
    End Function

    Private Function toGrad(winkel As Double) As Double
        Return winkel * (180.0 / Math.PI)
    End Function
End Class

