Public Class MovementCalculations
    ' -----------------------------------------------------------------------------
    ' TODO
    ' -----------------------------------------------------------------------------
    ' ???

    ' -----------------------------------------------------------------------------
    ' Definitions
    ' -----------------------------------------------------------------------------
    Public v_max(5) As Double
    Public a_max(5) As Double
    Public s(5) As Double
    Public a(5) As Double
    Public v(5) As Double

    ' -----------------------------------------------------------------------------
    ' Public
    ' -----------------------------------------------------------------------------
    'Gibt True zurück wenn Berechnung erfolgreich war
    Public Function calculate() As Boolean
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
        Public t_ac As Double
        Public t_const As Double
        Public v_max_reached As Boolean
    End Structure

    Private Structure profile_t
        Public a As Double()
        Public v As Double()
    End Structure

    Private Sub calcMaxTimes(ByRef _t As times_t, ByRef _v_max As Double(), ByRef _a_max As Double())
        Dim t As times_t
        Dim t_ges, t_max As Double

        'Benötigte Zeit jeder Achse berechnen
        For i = 0 To 5
            'Maximalwert ermitteln
            t_ges = calcMoveTime(t, s(i), _v_max(i), _a_max(i))
            If t_ges >= t_max Then
                t_max = t_ges
                _t.t_const = t.t_const
                _t.t_ac = t.t_ac
                _t.v_max_reached = If(t.t_const = 0, False, True)
            End If
        Next
    End Sub
    Private Function calcProfile(ByRef _p As profile_t, ByRef _t As times_t) As Boolean
        Dim limit As Boolean = False
        For i = 0 To 5
            'Beschleunigung ermitteln
            _p.a(i) = Math.Round((s(i) / (_t.t_ac * (_t.t_ac + _t.t_const))) * 100.0) / 100.0
            'Geschwindigkeit ermitteln 
            _p.v(i) = Math.Round((_p.a(i) * _t.t_ac) * 100.0) / 100.0
            'Begrenzen
            If _p.a(i) > a_max(i) Then
                _p.a(i) = a_max(i)
                limit = True
            End If
            If _p.v(i) > v_max(i) Then
                _p.v(i) = v_max(i)
                limit = True
            End If
        Next
        Return Not limit
    End Function
    Private Function calcMoveTime(ByRef _t As times_t, _s As Double, _v_max As Double, _a_max As Double) As Double
        Dim s_acc_max As Double = _v_max ^ 2 / _a_max / 2 'benötigte Strecke bis v_max bei maximaler Beschleunigung
        'Zeit für Beschleunigung und für Fahrt mit konstanter Geschwindigkeit
        If _s - 2 * s_acc_max > 0 Then 'v_max wird erreicht
            _t.t_ac = Math.Sqrt(2 * s_acc_max / _a_max)
            _t.t_const = (_s - 2 * s_acc_max) / _v_max
            _t.v_max_reached = True
        Else 'v_max wird nicht erreicht
            _t.t_ac = Math.Sqrt(_s / _a_max)
            _t.t_const = 0
            _t.v_max_reached = False
        End If
        'Benötigte Zeit für komplette Bewegung
        Return 2 * _t.t_ac + _t.t_const
    End Function
End Class
