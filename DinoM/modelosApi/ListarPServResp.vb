Public Class ListarPServResp
    Public Class Meta
        Public Property status As Boolean
        Public Property code As Integer
        Public Property message As String
        Public Property page As Integer
        Public Property pageSize As Integer
        Public Property registered As Integer
    End Class

    Public Class Data
        Public Property codigoActividad As String
        Public Property codigoProducto As String
        Public Property descripcionProducto As String
    End Class

    Public Class ProServ
        Public Property meta As Meta
        Public Property data As List(Of Data)
    End Class

    Public Class Meta1
        Public Property status As Boolean
        Public Property code As Integer
        Public Property message As String
    End Class

    Public Class Errors
        Public Property details As Object()
        Public Property siat As Object()
    End Class

    Public Class ProServ1
        Public Property meta As Meta
        Public Property errors As Errors
    End Class
End Class
