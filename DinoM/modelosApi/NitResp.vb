Public Class NitResp
    Public Class Meta
        Public Property code As Integer
        Public Property message As String
    End Class

    Public Class Data
        Public Property details As String

    End Class

    Public Class RespuestNit
        Public Property meta As Meta
        Public Property data As Data
    End Class

End Class
