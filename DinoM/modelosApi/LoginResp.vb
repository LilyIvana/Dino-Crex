Public Class LoginResp
    Public Class Meta
        Public Property code As Integer
        Public Property message As String
    End Class

    Public Class Data
        Public Property access_token As String
        Public Property token_type As String
        Public Property expires_at As String
    End Class

    Public Class RespuestLogin
        Public Property meta As Meta
        Public Property data As Data
    End Class

End Class
