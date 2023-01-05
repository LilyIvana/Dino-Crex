''' <summary>
''' Más información en http://www.jc-mouse.net/
''' </summary>
Public Class Base64SIN

    ''' <summary>
    ''' Codificador base64
    ''' </summary>
    ''' <param name="value">Numero a codificar</param>                
    ''' <returns>Cadena codificada</returns>
    Public Shared Function convertBase64(ByVal value As Integer) As String

        Dim dictionary = New String() {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
                                "A", "B", "C", "D", "E", "F", "G", "H", "I", "J",
                                "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T",
                                "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d",
                                "e", "f", "g", "h", "i", "j", "k", "l", "m", "n",
                                "o", "p", "q", "r", "s", "t", "u", "v", "w", "x",
                                "y", "z", "+", "/"}
        Dim quotient As Integer = 1
        Dim remainder As Integer
        Dim word As String = ""
        While quotient > 0
            quotient = value \ 64
            remainder = value Mod 64
            word = dictionary(remainder) & word
            value = quotient
        End While
        Return word
    End Function

End Class