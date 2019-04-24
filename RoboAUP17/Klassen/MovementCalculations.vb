Friend Class MovementCalculations
    ' -----------------------------------------------------------------------------
    ' Definitions
    ' -----------------------------------------------------------------------------
    Friend v_max(5) As Double
    Friend v_min(5) As Double
    Friend a_max(5) As Double
    Friend s(5) As Double
    Friend a(5) As Double
    Friend v(5) As Double

    ' -----------------------------------------------------------------------------
    ' Public
    ' -----------------------------------------------------------------------------
    'Gibt True zurück wenn Berechnung erfolgreich war
    Friend Function calculate() As Boolean
        Dim t As times_t
        Dim p As profile_t
        p.a = New Double(5) {}
        p.v = New Double(5) {}

        'Zeiten berechnen
        calcMaxTimes(t, v_max, a_max)
        'Prüfen ob Zeit = 0
        If t.t_ac = 0 Then
            For i = 0 To 5
                a(i) = 0
                v(i) = 0
            Next
            Return False
        End If
        'Geschwindigkeit und Beschleunigung berechnen
        If Not calcProfile(p, t) Then 'calcProfile wird False wenn max V oder max A überschritten wurden
            'Nochmals Zeiten mit neuen Beschleunigungswerten berechnen
            calcMaxTimes(t, p.v, p.a)
            'und nochmal Geschwindigkeit und Beschleunigung berechnen
            If Not calcProfile(p, t) Then
                For i = 0 To 5
                    v(i) = 0
                    a(i) = 0
                    Return False
                Next
            End If
            'Ausgeben
        End If
        v = p.v
        a = p.a
        Return True
    End Function

    ' -----------------------------------------------------------------------------
    ' Private
    ' -----------------------------------------------------------------------------
    Private Structure times_t
        Friend t_ac As Double
        Friend t_const As Double
        Friend v_max_reached As Boolean
    End Structure

    Private Structure profile_t
        Friend v_min As Double()
        Friend a As Double()
        Friend v As Double()
    End Structure

    Private Sub calcMaxTimes(ByRef times As times_t, ByRef v_max As Double(), ByRef a_max As Double())
        Dim t As times_t
        Dim t_ges, t_max As Double

        'Benötigte Zeit jeder Achse berechnen
        For i = 0 To 5
            'Maximalwert ermitteln
            t_ges = calcMoveTime(t, s(i), v_min(i), v_max(i), a_max(i))
            If t_ges >= t_max Then
                t_max = t_ges
                times.t_const = t.t_const
                times.t_ac = t.t_ac
                times.v_max_reached = If(t.t_const = 0, False, True)
            End If
        Next
    End Sub
    Private Function calcProfile(ByRef prof As profile_t, ByRef times As times_t) As Boolean
        Dim limit As Boolean = False
        For i = 0 To 5
            'Beschleunigung ermitteln
            prof.a(i) = Math.Round((s(i) / (times.t_ac * (times.t_ac + times.t_const))) * 100.0) / 100.0
            'Geschwindigkeit ermitteln
            prof.v(i) = Math.Round((prof.a(i) * times.t_ac) * 100.0) / 100.0
            'Begrenzen
            If prof.a(i) > a_max(i) Then
                prof.a(i) = a_max(i)
                limit = True
            End If
            If prof.v(i) > v_max(i) Then
                prof.v(i) = v_max(i)
                limit = True
            End If
        Next
        Return Not limit
    End Function
    Private Function calcMoveTime(ByRef t As times_t, s As Double, v_min As Double, v_max As Double, a_max As Double) As Double
        'benötigte Zeit bis v_max mit maximaler Beschleunigung
        Dim t_ac As Double = (v_max - v_min) / a_max
        'dafür benötigte Strecke
        Dim s_acc_max As Double = ((a_max * t_ac ^ 2) / 2) + v_min * t_ac

        'Zeit für Beschleunigung und für Fahrt mit konstanter Geschwindigkeit
        If s - 2 * s_acc_max > 0 Then 'v_max wird erreicht
            t.t_ac = t_ac
            t.t_const = (s - 2 * s_acc_max) / v_max
            t.v_max_reached = True
        Else 'v_max wird nicht erreicht
            t.t_ac = (Math.Sqrt(2 * a_max * (s / 2) + v_min ^ 2) - v_min) / a_max
            t.t_const = 0
            t.v_max_reached = False
        End If
        'Benötigte Zeit für komplette Bewegung
        Return 2 * t.t_ac + t.t_const
    End Function
End Class
