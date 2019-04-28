Imports RoboAuP17.Matrizen
Friend Class Kinematics
    Private _DHParameter(5) As DHParameter
    Private _workframe, _toolframe As CartCoords
    Private _initOkay As Boolean = False

#Region "Properties"
    Friend Property Workframe As CartCoords
        Get
            Return _workframe
        End Get
        Set(value As CartCoords)
            _workframe = value
        End Set
    End Property

    Friend Property Toolframe As CartCoords
        Get
            Return _toolframe
        End Get
        Set(value As CartCoords)
            _toolframe = value
        End Set
    End Property
#End Region

#Region "Public"
    ' -----------------------------------------------------------------------------
    ' Public
    ' -----------------------------------------------------------------------------
    Friend Sub SetDenavitHartenbergParameter(DenavitHartenbergParameter As DHParameter())
        If UBound(DenavitHartenbergParameter) <> 5 Then
            Throw New Exception("Denavit Hartenberg Parameter für alle 6 Achsen erforderlich!")
        End If
        _DHParameter = DenavitHartenbergParameter
        _initOkay = True
    End Sub
    Friend Function ForwardKin(joints As JointAngles) As CartCoords
        If Not _initOkay Then
            Throw New Exception("Denavit Hartenberg Parameter wurden noch nicht übergeben!")
        End If
        joints.J3 = joints.J3 - 90
        joints.J6 = joints.J6 + 180

        'Erstmal die Transformationsmatritzen für jede einzelne Achse berechnen
        Dim transForwMatr(7) As Matrix4x4
        'Work Frame
        transForwMatr(0) = CalcFrameMatrix(_workframe)
        'Transformationsmatrizen jeder Achse
        For i As Short = 1 To 6
            transForwMatr(i) = CalcDHTransMatrix(i, joints.Items(i - 1))
        Next
        'Tool Frame
        transForwMatr(7) = CalcFrameMatrix(_toolframe)

        'Matritzen multiplizieren
        Dim tmpMatr As Matrix4x4 = transForwMatr(0)
        For i As Int16 = 1 To 7
            tmpMatr *= transForwMatr(i)
        Next

        'Ausgeben
        Dim erg As CartCoords
        'Position
        erg.X = tmpMatr.val(3, 0)
        erg.Y = tmpMatr.val(3, 1)
        erg.Z = tmpMatr.val(3, 2)
        'Ausrichtung
        Dim tmpPitch As Double = Math.Atan2(Math.Sqrt((tmpMatr.val(2, 0) ^ 2) + (tmpMatr.val(2, 1) ^ 2)), tmpMatr.val(2, 2) * -1)
        erg.Yaw = ToDEG(Math.Atan2(tmpMatr.val(0, 2) / tmpPitch, tmpMatr.val(1, 2) / tmpPitch))
        erg.Pitch = ToDEG(tmpPitch)
        erg.Roll = ToDEG(Math.Atan2(tmpMatr.val(2, 0) / tmpPitch, tmpMatr.val(2, 1) / tmpPitch))

        Return erg
    End Function

    Friend Function InversKin(coords As CartCoords) As JointAngles
        If Not _initOkay Then
            Throw New Exception("Denavit Hartenberg Parameter wurden noch nicht übergeben!")
        End If
        Dim erg As New JointAngles

#Region "Vorberechnungen"
        ' Vorberechnungen
        '------------------------------------
        ' Winkel in Bogenmaß umrechnen
        Dim coordRad As New CartCoords(coords.X, coords.Y, coords.Z, ToRAD(coords.Yaw), ToRAD(coords.Pitch), ToRAD(coords.Roll))

        ' Quadrant von J1 berechnen
        Dim J1Quadrant As Integer
        If coords.X > 0 Then
            If coords.Y > 0 Then
                J1Quadrant = 1
            Else
                J1Quadrant = 2
            End If
        Else
            If coords.Y > 0 Then
                J1Quadrant = 4
            Else
                J1Quadrant = 3
            End If
        End If

        ' TCP Matrix berechnen (R 0-T)
        Dim tcpFrame As New Matrix4x4
        tcpFrame.val(0, 0) = Math.Cos(coordRad.Yaw) * Math.Cos(coordRad.Roll) - Math.Cos(coordRad.Pitch) * Math.Sin(coordRad.Yaw) * Math.Sin(coordRad.Roll)
        tcpFrame.val(0, 1) = Math.Cos(coordRad.Pitch) * Math.Cos(coordRad.Roll) * Math.Sin(coordRad.Yaw) + Math.Cos(coordRad.Yaw) * Math.Sin(coordRad.Roll)
        tcpFrame.val(0, 2) = Math.Sin(coordRad.Yaw) * Math.Sin(coordRad.Pitch)
        tcpFrame.val(0, 3) = 0.0
        tcpFrame.val(1, 0) = Math.Cos(coordRad.Roll) * Math.Sin(coordRad.Yaw) + Math.Cos(coordRad.Yaw) * Math.Cos(coordRad.Pitch) * Math.Sin(coordRad.Roll)
        tcpFrame.val(1, 1) = Math.Cos(coordRad.Yaw) * Math.Cos(coordRad.Pitch) * Math.Cos(coordRad.Roll) - Math.Sin(coordRad.Yaw) * Math.Sin(coordRad.Roll)
        tcpFrame.val(1, 2) = Math.Cos(coordRad.Yaw) * Math.Sin(coordRad.Pitch)
        tcpFrame.val(1, 3) = 0.0
        tcpFrame.val(2, 0) = Math.Sin(coordRad.Pitch) * Math.Sin(coordRad.Roll)
        tcpFrame.val(2, 1) = Math.Cos(coordRad.Roll) * Math.Sin(coordRad.Pitch)
        tcpFrame.val(2, 2) = -Math.Cos(coordRad.Pitch)
        tcpFrame.val(2, 3) = 0.0
        tcpFrame.val(3, 0) = coords.X
        tcpFrame.val(3, 1) = coords.Y
        tcpFrame.val(3, 2) = coords.Z
        tcpFrame.val(3, 3) = 1.0

        ' Workframe & Toolframe berechnen
        Dim workframe As Matrix4x4 = CalcFrameMatrix(_workframe)
        Dim toolframe As Matrix4x4 = CalcFrameMatrix(_toolframe)

        ' Toolframe transponieren
        Dim toolframeInv As New Matrix4x4
        toolframeInv.val(0, 0) = toolframe.val(0, 0)
        toolframeInv.val(0, 1) = toolframe.val(1, 0)
        toolframeInv.val(0, 2) = toolframe.val(2, 0)
        toolframeInv.val(0, 3) = 0.0
        toolframeInv.val(1, 0) = toolframe.val(0, 1)
        toolframeInv.val(1, 1) = toolframe.val(1, 1)
        toolframeInv.val(1, 2) = toolframe.val(2, 1)
        toolframeInv.val(1, 3) = 0.0
        toolframeInv.val(2, 0) = toolframe.val(0, 2)
        toolframeInv.val(2, 1) = toolframe.val(1, 2)
        toolframeInv.val(2, 2) = toolframe.val(2, 2)
        toolframeInv.val(2, 3) = 0.0
        toolframeInv.val(3, 0) = toolframe.val(3, 0) * toolframeInv.val(0, 0) + toolframe.val(3, 1) * toolframeInv.val(1, 0) + toolframe.val(3, 2) * toolframeInv.val(2, 0)
        toolframeInv.val(3, 1) = toolframe.val(3, 0) * toolframeInv.val(0, 1) + toolframe.val(3, 1) * toolframeInv.val(1, 1) + toolframe.val(3, 2) * toolframeInv.val(2, 1)
        toolframeInv.val(3, 2) = toolframe.val(3, 0) * toolframeInv.val(0, 2) + toolframe.val(3, 1) * toolframeInv.val(1, 2) + toolframe.val(3, 2) * toolframeInv.val(2, 2)
        toolframeInv.val(3, 3) = 1.0

        ' Spherical Wrist Frame berechnen (R 0-5)
        Dim sphericalWristCenter As New Matrix4x4
        ' Workframe offset
        sphericalWristCenter = workframe * tcpFrame
        sphericalWristCenter.val(0, 0) *= -1
        ' Toolframe Offset
        sphericalWristCenter *= toolframeInv
        ' R 0-6 Offset (Joint 6 / inverse Denavit-Hartenberg Transformation)
        sphericalWristCenter *= CalcInvDHTransMatrix(6, 180)
#End Region

#Region "J1"
        ' J1 Winkel berechnen
        '------------------------------------
        Dim tmpJ1Angle As Double = Math.Atan(sphericalWristCenter.val(3, 1) / sphericalWristCenter.val(3, 0))
        ' Quadrant beachten
        If J1Quadrant = 3 Then
            erg.J1 = ToDEG(tmpJ1Angle) - 180.0
        ElseIf J1Quadrant = 4 Then
            erg.J1 = ToDEG(tmpJ1Angle) + 180.0
        Else
            erg.J1 = ToDEG(tmpJ1Angle)
        End If
#End Region

#Region "J3&J2"
        ' J3 & J2 Winkel berechnen
        '------------------------------------
        Dim a2a3, d4, pY, pXminusa1, paH As Double
        Dim angA, angB, angC, angD As Double

        ' Rechtwinkliges Dreieck
        pXminusa1 = Math.Sqrt(sphericalWristCenter.val(3, 1) ^ 2 + sphericalWristCenter.val(3, 0) ^ 2) - _DHParameter(0).a
        pY = sphericalWristCenter.val(3, 2) - _DHParameter(0).d
        paH = Math.Sqrt(pXminusa1 ^ 2 + pY ^ 2)

        a2a3 = _DHParameter(1).a + _DHParameter(2).a
        d4 = _DHParameter(3).d

        ' J3
        angC = ToDEG(Math.Acos((d4 ^ 2 + a2a3 ^ 2 - paH ^ 2) / (2 * Math.Abs(d4) * a2a3))) 'Kosinussatz
        erg.J3 = 180.0 - angC

        ' J2
        If pXminusa1 >= 0 Then
            ' Arm forward Konfiguration
            angA = ToDEG(Math.Atan(pY / pXminusa1)) 'Tangenz
            angB = ToDEG(Math.Acos((a2a3 ^ 2 + paH ^ 2 - d4 ^ 2) / (2 * a2a3 * paH))) 'Kosinussatz
            erg.J2 = -(angA + angB)
        Else
            ' Arm mid Konfiguration
            angA = ToDEG(Math.Acos((a2a3 ^ 2 + paH ^ 2 - d4 ^ 2) / (2 * a2a3 * paH))) 'Kosinussatz
            angB = ToDEG(Math.Atan(pY / -pXminusa1)) 'Tangenz
            angD = 90 - angA - angB
            erg.J2 = angD - 180
        End If
#End Region

        ' Vorberechnungen für J4 - J6
        '------------------------------------
#Region "Vorwärtstransformation zu J3"
        ' Transformationsmatritzen für J1 bis J3 berechnen
        Dim transForwMatr(3) As Matrix4x4
        transForwMatr(0) = CalcFrameMatrix(_workframe)
        transForwMatr(1) = CalcDHTransMatrix(1, erg.J1)
        transForwMatr(2) = CalcDHTransMatrix(2, erg.J2)
        transForwMatr(3) = CalcDHTransMatrix(3, erg.J3 - 90)
        ' Vorwärtstransformation bis J3 (R 0-3)
        Dim r03 As Matrix4x4 = transForwMatr(0)
        For i As Int16 = 1 To 3
            r03 *= transForwMatr(i)
        Next
#End Region

#Region "Spherical Wrist Ausrichtung berechnen"
        ' 3x3 Teil von R 0-3 Matrix transponieren, restliche Felder bleiben 0
        Dim r03Trans As New Matrix4x4
        r03Trans.val(0, 0) = r03.val(0, 0)
        r03Trans.val(0, 1) = r03.val(1, 0)
        r03Trans.val(0, 2) = r03.val(2, 0)
        r03Trans.val(1, 0) = r03.val(0, 1)
        r03Trans.val(1, 1) = r03.val(1, 1)
        r03Trans.val(1, 2) = r03.val(2, 1)
        r03Trans.val(2, 0) = r03.val(0, 2)
        r03Trans.val(2, 1) = r03.val(1, 2)
        r03Trans.val(2, 2) = r03.val(2, 2)

        ' mit spherical wrist center multiplizieren
        Dim sphericalWristOrientation As Matrix4x4
        sphericalWristOrientation = r03Trans * sphericalWristCenter
#End Region

#Region "J5"
        ' J5 Winkel berechnen
        '------------------------------------
        Dim tmpJ5Angle As Double
        tmpJ5Angle = ToDEG(Math.Atan2(Math.Sqrt(1 - sphericalWristOrientation.val(2, 2) ^ 2), sphericalWristOrientation.val(2, 2)))
        If tmpJ5Angle > 0 Then
            erg.J5 = tmpJ5Angle
        Else
            erg.J5 = ToDEG(Math.Atan2(-Math.Sqrt(1 - sphericalWristOrientation.val(2, 2) ^ 2), sphericalWristOrientation.val(2, 2)))
        End If
#End Region

#Region "J4"
        ' J4 Winkel berechnen
        '------------------------------------
        If erg.J5 > 0 Then
            erg.J4 = ToDEG(Math.Atan2(sphericalWristOrientation.val(2, 1), sphericalWristOrientation.val(2, 0)))
        Else
            erg.J4 = ToDEG(Math.Atan2(-sphericalWristOrientation.val(2, 1), -sphericalWristOrientation.val(2, 0)))
        End If
#End Region

#Region "J6"
        ' J6 Winkel berechnen
        '------------------------------------
        If erg.J5 > 0 Then
            If sphericalWristOrientation.val(1, 2) < 0 Then
                erg.J6 = ToDEG(Math.Atan2(-sphericalWristOrientation.val(1, 2), sphericalWristOrientation.val(0, 2))) - 180
            Else
                erg.J6 = ToDEG(Math.Atan2(-sphericalWristOrientation.val(1, 2), sphericalWristOrientation.val(0, 2))) + 180
            End If
        Else
            If sphericalWristOrientation.val(1, 2) < 0 Then
                erg.J6 = ToDEG(Math.Atan2(sphericalWristOrientation.val(1, 2), -sphericalWristOrientation.val(0, 2))) + 180
            Else
                erg.J6 = ToDEG(Math.Atan2(sphericalWristOrientation.val(1, 2), -sphericalWristOrientation.val(0, 2))) - 180
            End If
        End If
#End Region
        Return erg
    End Function
#End Region

#Region "Private"
    ' -----------------------------------------------------------------------------
    ' Private
    ' -----------------------------------------------------------------------------
    Private Function CalcDHTransMatrix(joint As Int16, theta As Double) As Matrix4x4 'Theta = Winkel einer Achse
        Dim dh As DHParameter = _DHParameter(joint - 1)
        Dim thetaBog As Double = ToRAD(theta)
        Dim erg As New Matrix4x4

        dh.alpha = ToRAD(dh.alpha)

        erg.val(0, 0) = Math.Cos(thetaBog)
        erg.val(0, 1) = Math.Sin(thetaBog)
        erg.val(0, 2) = 0.0
        erg.val(0, 3) = 0.0
        erg.val(1, 0) = -Math.Sin(thetaBog) * Math.Cos(dh.alpha)
        erg.val(1, 1) = Math.Cos(thetaBog) * Math.Cos(dh.alpha)
        erg.val(1, 2) = Math.Sin(dh.alpha)
        erg.val(1, 3) = 0.0
        erg.val(2, 0) = Math.Sin(thetaBog) * Math.Sin(dh.alpha)
        erg.val(2, 1) = -Math.Cos(thetaBog) * Math.Sin(dh.alpha)
        erg.val(2, 2) = Math.Cos(dh.alpha)
        erg.val(2, 3) = 0.0
        erg.val(3, 0) = dh.a * Math.Cos(thetaBog)
        erg.val(3, 1) = dh.a * Math.Sin(thetaBog)
        erg.val(3, 2) = dh.d
        erg.val(3, 3) = 1.0

        Return erg
    End Function

    Private Function CalcInvDHTransMatrix(joint As Int16, theta As Double) As Matrix4x4 'Theta = Winkel einer Achse
        Dim dh As DHParameter = _DHParameter(joint - 1)
        Dim thetaBog As Double = ToRAD(theta)
        Dim erg As New Matrix4x4

        dh.alpha = ToRAD(dh.alpha)

        erg.val(0, 0) = Math.Cos(thetaBog)
        erg.val(0, 1) = -Math.Sin(thetaBog) * Math.Cos(dh.alpha)
        erg.val(0, 2) = Math.Sin(dh.alpha) * Math.Sin(thetaBog)
        erg.val(0, 3) = 0.0

        erg.val(1, 0) = Math.Sin(thetaBog)
        erg.val(1, 1) = Math.Cos(thetaBog) * Math.Cos(dh.alpha)
        erg.val(1, 2) = -Math.Cos(thetaBog) * Math.Sin(dh.alpha)
        erg.val(1, 3) = 0.0

        erg.val(2, 0) = 0.0
        erg.val(2, 1) = Math.Sin(dh.alpha)
        erg.val(2, 2) = Math.Cos(dh.alpha)
        erg.val(2, 3) = 0.0

        erg.val(3, 0) = -dh.a
        erg.val(3, 1) = -dh.d * Math.Sin(dh.alpha)
        erg.val(3, 2) = -dh.d * Math.Cos(dh.alpha)
        erg.val(3, 3) = 1.0

        Return erg
    End Function

    Private Function CalcFrameMatrix(frame As CartCoords) As Matrix4x4
        Dim Matrix As New Matrix4x4
        Dim yaw As Double = ToRAD(frame.Yaw)
        Dim pitch As Double = ToRAD(frame.Pitch)
        Dim roll As Double = ToRAD(frame.Roll)

        Matrix.val(0, 0) = Math.Cos(roll) * Math.Cos(pitch)
        Matrix.val(0, 1) = Math.Sin(roll) * Math.Cos(pitch)
        Matrix.val(0, 2) = -Math.Sin(roll)
        Matrix.val(0, 3) = 0.0
        Matrix.val(1, 0) = -Math.Sin(roll) * Math.Cos(yaw) + Math.Cos(roll) * Math.Sin(pitch) * Math.Sin(yaw)
        Matrix.val(1, 1) = Math.Cos(roll) * Math.Cos(yaw) + Math.Sin(roll) * Math.Sin(pitch) * Math.Sin(yaw)
        Matrix.val(1, 2) = Math.Cos(pitch) * Math.Sin(yaw)
        Matrix.val(1, 3) = 0.0
        Matrix.val(2, 0) = Math.Sin(roll) * Math.Sin(yaw) + Math.Cos(roll) * Math.Sin(pitch) * Math.Cos(yaw)
        Matrix.val(2, 1) = -Math.Cos(roll) * Math.Sin(yaw) + Math.Sin(roll) * Math.Sin(pitch) * Math.Cos(yaw)
        Matrix.val(2, 2) = Math.Cos(pitch) * Math.Cos(yaw)
        Matrix.val(2, 3) = 0.0
        Matrix.val(3, 0) = frame.X
        Matrix.val(3, 1) = frame.Y
        Matrix.val(3, 2) = frame.Z
        Matrix.val(3, 3) = 1.0

        Return Matrix
    End Function

    Private Function ToRAD(winkel As Double) As Double
        Return winkel * (Math.PI / 180)
    End Function

    Private Function ToDEG(winkel As Double) As Double
        Return winkel * (180.0 / Math.PI)
    End Function
#End Region

End Class