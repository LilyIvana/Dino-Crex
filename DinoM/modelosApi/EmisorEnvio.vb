Public Class EmisorEnvio
    Public Class Detalle
        Public Property codigoProductoSin As String
        Public Property codigoProducto As String
        Public Property descripcion As String
        Public Property unidadMedida As Integer
        Public Property cantidad As Integer
        Public Property precioUnitario As Double
        Public Property montoDescuento As Double
        Public Property subTotal As Double
        Public Property numeroSerie As String
        Public Property numeroImei As String
    End Class

    Public Class Emisor
        Public Property numeroFactura As Integer
        Public Property nombreRazonSocial As String
        Public Property codigoTipoDocumentoIdentidad As Integer
        Public Property numeroDocumento As String
        Public Property complemento As String
        Public Property codigoCliente As String
        Public Property codigoMetodoPago As Integer
        Public Property numeroTarjeta As String
        Public Property codigoPuntoVenta As Integer
        Public Property codigoDocumentoSector As Integer
        Public Property codigoMoneda As Integer
        Public Property tipoCambio As Double
        Public Property montoTotal As Double
        Public Property montoTotalSujetoIva As Double
        Public Property montoTotalMoneda As Double
        Public Property montoGiftCard As Double
        Public Property descuentoAdicional As Double
        Public Property codigoExcepcion As Integer
        Public Property usuario As String
        Public Property email As String
        Public Property actividadEconomica As Integer
        Public Property detalles As Detalle()
    End Class
End Class
