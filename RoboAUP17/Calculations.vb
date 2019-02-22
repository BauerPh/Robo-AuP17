Module Calculations
    Public Function AngleToSteps(angle As Double, motGear As Double, mechGear As Double, stepsPerRot As Int32, angleZeroOff As Double) As Int32
        Return CInt(Math.Truncate(stepsPerRot * motGear * mechGear * (angle + angleZeroOff) / 360.0))
    End Function

    Public Function StepsToAngle(steps As Int32, motGear As Double, mechGear As Double, stepsPerRot As Int32, angleZeroOff As Double) As Double
        Return Math.Round((CDec(steps) / stepsPerRot * 360.0 / motGear / mechGear - angleZeroOff) * 100.0) / 100.0
    End Function

    Public Function RoundUp(ByVal d As Double, ByVal digits As UInt16) As Double
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

    Public Function RoundDown(ByVal d As Double, ByVal digits As UInt16) As Double
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

    Public Function Constrain(ByVal x As Double, ByVal min As Double, ByVal max As Double) As Double
        If (x >= max) Then Return max
        If (x <= min) Then Return min
        Return x
    End Function
End Module
