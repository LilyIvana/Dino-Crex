Public Class ListarFacturas
    Public Class Meta
        Public Property status As Boolean
        Public Property code As Integer
        Public Property message As String
        Public Property page As Integer
        Public Property pageSize As Integer
        Public Property registered As Integer

    End Class
    Public Ci As String = "nit-ci"
    Public Class Data
        Public Property id As String
        Public Property cuf As String
        Public Property numeroFactura As String
        Public Property tipoFactura As String
        Public Property fechaEmision As String
        Public Property actividad As String
        Public Property documentoSector As String
        Public Property sucursal As String
        Public Property puntoVenta As String
        Public Property razonSosial As String
        Public Property Ci As String
        Public Property usuario As String
        Public Property montoTotal As String
        Public Property estado As String



    End Class

    Public Class RespListarFacturas
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
    End Class
    Public Class ListarFacturasError
        Public Property meta As Meta
        Public Property errors As Errors
    End Class
End Class

