Imports System.Windows.Forms.Design

<ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)>
Partial Public Class ToolStripNumericUpDown
    Inherits ToolStripControlHost

    'Reference to NumericUpDown Object
    Private WithEvents _num As NumericUpDown

    'Constructor
    Public Sub New()
        MyBase.New(New NumericUpDown())
        InitializeComponent()
        _num = CType(Control, NumericUpDown)
    End Sub

    'Value Property
    Public Property Value() As Decimal
        Get
            Return _num.Value
        End Get
        Set(ByVal value As Decimal)
            _num.Value = value
        End Set
    End Property

    'Minimum Property
    Public Property Minimum() As Decimal
        Get
            Return _num.Minimum
        End Get
        Set(value As Decimal)
            _num.Minimum = value
        End Set
    End Property

    'Maximum Property
    Public Property Maximum() As Decimal
        Get
            Return _num.Maximum
        End Get
        Set(value As Decimal)
            _num.Maximum = value
        End Set
    End Property

    'Maximum Property
    Public Property DecimalPlaces() As Integer
        Get
            Return _num.DecimalPlaces
        End Get
        Set(value As Integer)
            _num.DecimalPlaces = value
        End Set
    End Property

    'Value Changed Event
    Public Event ValueChanged As EventHandler
    Private Sub _num_ValueChanged(sender As Object, e As EventArgs) Handles _num.ValueChanged
        RaiseEvent ValueChanged(sender, e)
    End Sub
End Class
