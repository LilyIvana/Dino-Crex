Public Class DParametro
    Public nombre As String
    Public tipo As String
    Public valor As String
    Public tamano As Integer
    Public detalle As DataTable

    Public Sub New(nom As String, val As String, Optional _detalle As DataTable = Nothing)
        nombre = nom
        valor = val
        detalle = _detalle
    End Sub

    Public Sub New(nom As String, ti As String, val As String, Optional tam As Integer = -1)
        nombre = nom
        tipo = ti
        valor = val
        tamano = tam
    End Sub
End Class
