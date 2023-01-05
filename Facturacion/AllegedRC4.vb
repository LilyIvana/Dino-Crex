''' <summary>
''' Más información en http://www.jc-mouse.net/
''' </summary>
Public Class AllegedRC4

    ''' <summary>
    ''' Encripta usando AllegedRC4
    ''' </summary>
    ''' <param name="message">mensaje a encriptar</param>
    ''' <param name="key">llave de cifrado</param>
    ''' <param name="unscripted">TRUE sin guion separados</param>
    ''' <returns>mensaje encriptado</returns>
    Public Shared Function encryptMessageRC4(ByVal message As String, ByVal key As String, ByVal unscripted As Boolean) As String

        Dim state(256) As Integer
        Dim x As Integer = 0
        Dim y As Integer = 0
        Dim index1 As Integer = 0
        Dim index2 As Integer = 0
        Dim nmen As Integer
        Dim messageEncryption As String = ""

        For i = 0 To 255
            state(i) = i
        Next

        For i = 0 To 255
            index2 = (Asc(key(index1)) + state(i) + index2) Mod 256
            Dim aux As Integer = state(i)
            state(i) = state(index2)
            state(index2) = aux
            index1 = (index1 + 1) Mod Len(key)
        Next


        For i As Integer = 0 To Len(message) - 1
            x = (x + 1) Mod 256
            y = (state(x) + y) Mod 256
            Dim aux As Integer = state(x)
            state(x) = state(y)
            state(y) = aux
            nmen = Asc(message(i)) Xor state((state(x) + state(y)) Mod 256)
            Dim nmenHex As String = Hex(nmen)
            Dim hyphen As String = ""
            If Not unscripted Then hyphen = "-"
            If Len(nmenHex) = 1 Then nmenHex = "0" & nmenHex
            messageEncryption = messageEncryption & hyphen & nmenHex
        Next

        If unscripted Then
            Return messageEncryption
        Else
            Return messageEncryption.Substring(1, messageEncryption.Length - 1)
        End If
    End Function

End Class