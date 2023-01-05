Public Class Celda
    Public campo As String
    Public titulo As String
    Public tamano As Integer
    Public visible As Boolean
    Public formato As String = String.Empty
    Public Sub New(cam As String, vis As Boolean, Optional tit As String = "", Optional tam As Integer = 0, Optional formato1 As String = "")
        campo = cam
        titulo = tit
        tamano = tam
        visible = vis
        formato = formato1
    End Sub
End Class
