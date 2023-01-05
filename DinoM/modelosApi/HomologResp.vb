Public Class HomologResp
    Public Class Meta
        Public Property status As Boolean
        Public Property code As Integer
        Public Property message As String
    End Class

    Public Class Data
        Public Property details As String
        Public Property codigoEstado As Integer
        Public Property codigoRecepcion As String
        Public Property cuf As String
        Public Property codigoSector As String
        Public Property sucursal As Integer
        Public Property puntoVenta As Integer
        Public Property dataXml As String
        Public Property tipoEmision As Integer
    End Class

    Public Class HomoResp
        Public Property meta As Meta
        Public Property data As Data()
    End Class

    Public Class DataARA
        Public Property details As String
        Public Property codigoEstado As Integer
        Public Property codigoRecepcion As String
        Public Property cuf As String
        Public Property codigoSector As String
        Public Property sucursal As Integer
        Public Property puntoVenta As Integer
        Public Property dataXml As String
        Public Property tipoEmision As Integer
    End Class
End Class
