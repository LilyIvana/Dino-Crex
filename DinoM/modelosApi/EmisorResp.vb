Public Class EmisorResp
    Public Class Meta
        Public Property code As Integer
        Public Property message As String
    End Class

    Public Class Data
        Public Property details As String
        Public Property codigoEstado As String
        Public Property codigoRecepcion As String
        Public Property cuf As String
        Public Property codigoSector As String
        Public Property numeroFactura As Integer
        Public Property sucursal As Integer
        Public Property puntoVenta As Integer
        Public Property dataXml As String
        Public Property tipoEmision As Integer
        Public Property qrUrl As String
        Public Property facturaUrl As String
        Public Property leyenda As String
        Public Property terceraLeyenda As String
        Public Property cufd As String


    End Class

    Public Class RespEmisor
        Public Property meta As Meta
        Public Property data As Data
    End Class


End Class
Public Class Meta
    Public Property status As Boolean
    Public Property code As Integer
    Public Property message As String
End Class

Public Class Errors
    Public Property details As Object()
    Public Property siat As Object()
End Class

Public Class Resp400
    'Public Property meta As Meta
    Public Property errors As Errors
End Class

