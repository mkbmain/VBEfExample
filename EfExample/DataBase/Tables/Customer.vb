' our tables are just standard vb models
Public Class Customer
    Public Sub New()
        CustomerLogins = New HashSet(Of CustomerLogin)()
    End Sub
    Public Overridable Property CustomerLogins() As ICollection(Of CustomerLogin)
    Public Property Id() As Integer
    Public Property Name() As String
    Public Property DateOfBirth() As Date
End Class