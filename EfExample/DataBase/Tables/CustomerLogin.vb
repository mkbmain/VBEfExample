' our tables are just standard vb models
Public Class CustomerLogin
    Public Property Id() As Integer
    Public Property CustomerId() As Integer
    Public Property CreatedAt() As DateTime
    Public Property Success As Boolean

    Public Overridable Property Customer() As Customer
End Class