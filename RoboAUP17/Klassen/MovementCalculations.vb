Friend Class MovementCalculations
    ' -----------------------------------------------------------------------------
    ' Definitions
    ' -----------------------------------------------------------------------------
    Private _v_min(5) As Double
    Private _v_max(5) As Double
    Private _a_max(5) As Double
    Private _s(5) As Double
    Private _calculated As Boolean = False
    Private _a(5) As Double
    Private _v(5) As Double
    Private _v_min_calc(5) As Double

    ' -----------------------------------------------------------------------------
    ' Public
    ' -----------------------------------------------------------------------------
    Friend Function SetValues(index As Integer, v_max As Double, v_min As Double, a_max As Double) As Boolean
        If index < 0 OrElse index > 5 Then Return False
        _v_min(index) = v_min
        _v_max(index) = v_max
        _a_max(index) = a_max
        Return True
    End Function

    Friend Function SetDistance(index As Integer, dist As Double) As Boolean
        If index < 0 OrElse index > 5 Then Return False
        _s(index) = dist
        Return True
    End Function

    Friend Function GetVmin() As Double()
        If Not _calculated Then Return Nothing
        Dim newArray(5) As Double
        Array.Copy(_v_min_calc, newArray, _v_min_calc.Length)
        Return newArray
    End Function
    Friend Function GetV() As Double()
        If Not _calculated Then Return Nothing
        Dim newArray(5) As Double
        Array.Copy(_v, newArray, _v.Length)
        Return newArray
    End Function
    Friend Function GetA() As Double()
        If Not _calculated Then Return Nothing
        Dim newArray(5) As Double
        Array.Copy(_a, newArray, _a.Length)
        Return newArray
    End Function

    'Gibt True zurück wenn Berechnung erfolgreich war
    Friend Function Calculate() As Boolean
        _calculated = False
        Dim t As _times_t
        Dim p As _profile_t

        'Zeiten berechnen
        t = _calcMaxTimes(_v_min, _v_max, _a_max)
        'Prüfen ob Zeit = 0
        'If t.t_ac = 0 Then
        '    For i = 0 To 5
        '        _a(i) = 0
        '        _v(i) = 0
        '    Next
        '    Return False
        'End If
        'Geschwindigkeit und Beschleunigung berechnen
        p = _calcProfile(t, _v_min)
        If p.limit Then ' p.limit wird True wenn max V oder max A überschritten wurden
            'Nochmals mit den neuen Werten berechnen
            t = _calcMaxTimes(p.v_min, p.v, p.a)
            p = _calcProfile(t, p.v_min)
            If p.limit Then
                'Und ein letzter Versuch
                t = _calcMaxTimes(p.v_min, p.v, p.a)
                p = _calcProfile(t, p.v_min)
                If p.limit Then
                    For i = 0 To 5
                        _v(i) = 0
                        _a(i) = 0
                        Return False
                    Next
                End If
            End If
        End If
        'Fertig
        _v_min_calc = p.v_min
        _v = p.v
        _a = p.a
        _calculated = True
        Return True
    End Function

    ' -----------------------------------------------------------------------------
    ' Private
    ' -----------------------------------------------------------------------------
    Private Structure _times_t
        Friend t_ac As Double
        Friend t_const As Double
        Friend t_complete As Double
        Friend v_max_reached As Boolean
    End Structure

    Private Class _profile_t
        Friend v_min As Double()
        Friend v As Double()
        Friend a As Double()
        Friend limit As Boolean
        Friend Sub New()
            v_min = New Double(5) {}
            v = New Double(5) {}
            a = New Double(5) {}
        End Sub
    End Class

    Private Function _calcMaxTimes(ByRef v_min As Double(), ByRef v_max As Double(), ByRef a_max As Double()) As _times_t
        Dim t As _times_t
        'Benötigte Zeit jeder Achse berechnen
        For i = 0 To 5
            'Maximalwert ermitteln
            Dim tmpT As _times_t
            tmpT = _calcMoveTime(_s(i), v_min(i), v_max(i), a_max(i))
            If tmpT.t_complete >= t.t_complete Then
                t = tmpT
            End If
        Next
        Return t
    End Function
    Private Function _calcProfile(ByRef t As _times_t, v_min As Double()) As _profile_t
        Dim p As New _profile_t
        p.limit = False
        For i = 0 To 5
            ' Beschleunigung mit v_min ermitteln
            Dim tmpA As Double = (_s(i) - v_min(i) * (2 * t.t_ac + t.t_const)) / (t.t_ac * (t.t_ac + t.t_const))
            ' Neues v_min ermitteln
            If tmpA < 0 Then
                p.v_min(i) = _s(i) / (2 * t.t_ac + t.t_const) ' Annahme: a = 0
            Else
                p.v_min(i) = v_min(i)
            End If
            ' Beschleunigung mit neuem v_min ermitteln
            p.a(i) = Math.Round((_s(i) - p.v_min(i) * (2 * t.t_ac + t.t_const)) / (t.t_ac * (t.t_ac + t.t_const)), 2)
            ' Geschwindigkeit ermitteln
            p.v(i) = Math.Round((p.a(i) * t.t_ac + p.v_min(i)), 2)
            'Begrenzen
            If p.a(i) > _a_max(i) Then
                p.a(i) = _a_max(i)
                p.limit = True
            End If
            If p.v(i) > _v_max(i) Then
                p.v(i) = _v_max(i)
                p.limit = True
            End If
        Next
        Return p
    End Function
    Private Function _calcMoveTime(s As Double, v_min As Double, v As Double, a As Double) As _times_t
        'benötigte Strecke bis v_max mit maximaler Beschleunigung
        Dim s_acc_max As Double = (v ^ 2 - v_min ^ 2) / (2 * a)
        Dim s_acc As Double
        Dim t As _times_t
        'Zeit für Beschleunigung und für Fahrt mit konstanter Geschwindigkeit
        If s - 2 * s_acc_max > 0 Then 'v_max wird erreicht
            t.v_max_reached = True
            s_acc = s_acc_max
            t.t_const = (s - 2 * s_acc_max) / v
        Else 'v_max wird nicht erreicht
            t.v_max_reached = False
            s_acc = (s / 2)
            t.t_const = 0
        End If
        t.t_ac = (Math.Sqrt(2 * a * s_acc + v_min ^ 2) - v_min) / a

        'Benötigte Zeit für komplette Bewegung
        t.t_complete = 2 * t.t_ac + t.t_const
        Return t
    End Function
End Class