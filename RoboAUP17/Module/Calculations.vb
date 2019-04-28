Module Calculations
    Friend Function AngleToSteps(angle As Double, motGear As Double, mechGear As Double, stepsPerRot As Int32, angleZeroOff As Double) As Int32
        Return CInt(Math.Round(stepsPerRot * motGear * mechGear * (angle + angleZeroOff) / 360.0))
    End Function

    Friend Function StepsToAngle(steps As Int32, motGear As Double, mechGear As Double, stepsPerRot As Int32, angleZeroOff As Double, min As Double, max As Double) As Double
        Dim val As Double = CDec(steps) / stepsPerRot * 360.0 / motGear / mechGear - angleZeroOff
        If val > max Then
            Return RoundDown(val, 2)
        ElseIf val < min Then
            Return RoundUp(val, 2)
        Else
            Return Math.Round(val, 2)
        End If
    End Function

    Friend Function RoundUp(ByVal d As Double, ByVal digits As UInt16) As Double
        Dim retval As Double = Math.Round(d, digits)
        If retval < d Then
            If digits > 0 Then
                Return retval + (1 / (10 ^ digits))
            Else
                Return retval + 1
            End If
        Else
            Return retval
        End If
    End Function

    Friend Function RoundDown(ByVal d As Double, ByVal digits As UInt16) As Double
        Dim retval As Double = Math.Round(d, digits)
        If retval > d Then
            If digits > 0 Then
                Return retval - (1 / (10 ^ digits))
            Else
                Return retval - 1
            End If
        Else
            Return retval
        End If
    End Function

    Friend Function Constrain(ByVal x As Double, ByVal min As Double, ByVal max As Double) As Double
        If (x >= max) Then Return max
        If (x <= min) Then Return min
        Return x
    End Function
End Module
