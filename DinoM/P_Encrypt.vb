Imports System.Security.Cryptography
Imports System.Text

Public Class P_Encrypt

    Private Const key As String = "ABCDEFG54669525PQRSTUVWXYZabcdef852846opqrstuvwxyz"

    Public Function EncryptKey(cadena As Byte()) As Byte()

        Dim keyArray As Byte()
        'Dim Arreglo_a_Cifrar As Byte() = UTF8Encoding.UTF8.GetBytes(cadena)
        Dim hashmd5 As MD5CryptoServiceProvider = New MD5CryptoServiceProvider()
        Dim tdes As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider()
        keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key))
        hashmd5.Clear()

        tdes.Key = keyArray

        tdes.Mode = CipherMode.ECB

        tdes.Padding = PaddingMode.PKCS7

        Dim cTransform As ICryptoTransform = tdes.CreateEncryptor()
        Dim ArrayResultado As Byte() = cTransform.TransformFinalBlock(cadena, 0, cadena.Length)
        tdes.Clear()
        Return ArrayResultado

    End Function

    Public Function DecryptKey(clave As Byte()) As Byte()
        Dim keyArray As Byte()

        Dim hashmd5 As MD5CryptoServiceProvider = New MD5CryptoServiceProvider()

        keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key))
        hashmd5.Clear()
        Dim tdes As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider()
        tdes.Key = keyArray

        tdes.Mode = CipherMode.ECB

        tdes.Padding = PaddingMode.PKCS7
        Dim cTransform As ICryptoTransform = tdes.CreateDecryptor()
        Dim resultArray As Byte() = cTransform.TransformFinalBlock(clave, 0, clave.Length)


        tdes.Clear()

        Return resultArray
    End Function

End Class
