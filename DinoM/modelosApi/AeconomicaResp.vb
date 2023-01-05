Public Class AeconomicaResp
    Public Class Meta
        Public Property status As Boolean
        Public Property code As Integer
        Public Property message As String
    End Class

    Public Class Data
        Public Property codigoActividad As String
        Public Property descripcion As String
        Public Property tipoActividad As String
    End Class

    Public Class Aecono
        Public Property meta As Meta
        Public Property data As List(Of Data)
    End Class
End Class
