Public Class Parametro
    Public Property Clave As String
    Public Property Valor As String

    Public Sub New()

    End Sub

    Public Sub New(_clave As String, _valor As String)
        Clave = _clave
        Valor = _valor
    End Sub
End Class
