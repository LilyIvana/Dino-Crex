Public Class AnulacionResp
    Public Class Meta
        Public Property status As Boolean
        Public Property code As Integer
        Public Property message As String
    End Class

    Public Class Datos
    End Class

    Public Class Data
        Public Property details As String
        Public Property datos As Datos
    End Class

    Public Class RespuestaAnul
        Public Property meta As Meta
        Public Property data As Data
    End Class

End Class
Public Class Meta1
    Public Property status As Boolean
    Public Property code As Integer
    Public Property message As String
End Class

Public Class Errors1
    Public Property details As Object()
    Public Property siat As Object()
End Class

Public Class error400
    Public Property meta As Meta1
    Public Property errors As Errors1
End Class