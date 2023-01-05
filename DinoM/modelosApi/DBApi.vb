Imports System.Net
Imports RestSharp

Public Class DBApi
    'funcion Post Api
    Public Function Post(url As String, headers As List(Of parametro), parametros As List(Of parametro), objeto As Object) As String

        'ServicePointManager.Expect100Continue = True
        'ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        Dim client = New RestClient()
        client.BaseUrl = New Uri(url)

        Dim request = New RestRequest()
        request.Method = Method.POST

        For Each item As parametro In headers
            request.AddHeader(item.Clave, item.Valor)
        Next

        For Each parametro As parametro In parametros
            request.AddParameter(parametro.Clave, parametro.Valor)
        Next


        If (parametros.Count = 0) Then
            request.AddJsonBody(objeto)
        End If

        Dim response = client.Execute(request).Content.ToString()

        Return response
    End Function
    'funcion GET Api
    Public Function MGet(url As String, headers As List(Of parametro), param_encode As List(Of parametro)) As String
        ServicePointManager.Expect100Continue = True
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        Dim client = New RestClient()
        client.BaseUrl = New Uri(url)

        Dim request = New RestRequest()
        request.Method = Method.GET

        For Each header As parametro In headers
            request.AddHeader(header.Clave, header.Valor)
        Next

        For Each parametro As parametro In param_encode
            request.AddParameter(parametro.Clave, parametro.Valor)
        Next

        Dim response = client.Execute(request).Content.ToString()

        Return response
    End Function
End Class
