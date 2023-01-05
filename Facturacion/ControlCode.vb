''' <summary>
''' Más información en http://www.jc-mouse.net/
''' </summary>
Public Class ControlCode

    ''' <summary>
    ''' Genera codigo de control
    ''' </summary>
    ''' <param name="authorizationNumber">Numero de autorizacion</param>        
    ''' <param name="invoiceNumber">Numero de factura</param>        
    ''' <param name="nitci">Número de Identificación Tributaria o Carnet de Identidad</param>        
    ''' <param name="dateOfTransaction">fecha de transaccion de la forma AAAAMMDD</param>        
    ''' <param name="transactionAmount">Monto de la transacción</param>        
    ''' <param name="dosageKey">Llave de dosificación</param>        
    ''' <returns>Codigo de Control generado de la forma 6A-DC-53-05-14</returns>
    Public Shared Function generateControlCode(ByVal authorizationNumber As String, ByVal invoiceNumber As String, ByVal nitci As String,
                           ByVal dateOfTransaction As String, ByVal transactionAmount As String, ByVal dosageKey As String) As String

        ''redondea monto de transaccion 
        transactionAmount = roundUp(transactionAmount)

        '' ======================== PASO 1 =============================
        invoiceNumber = addVerhoeffDigit(invoiceNumber, 2)
        nitci = addVerhoeffDigit(nitci, 2)
        dateOfTransaction = addVerhoeffDigit(dateOfTransaction, 2)
        transactionAmount = addVerhoeffDigit(transactionAmount, 2)

        Dim sumOfVariables As Long
        Try
            sumOfVariables = CLng(invoiceNumber) _
                                      + CLng(nitci) _
                                      + CLng(dateOfTransaction) _
                                      + CLng(transactionAmount)
        Catch ex As FormatException
            If (ex.Source <> vbNull) Then
                Console.WriteLine("FormatException source: {0}", ex.Source)
            End If
        End Try

        ''A la suma total se añde 5 digitos Verhoeff
        Dim sumOfVariables5Verhoeff As String = addVerhoeffDigit(sumOfVariables.ToString(), 5)

        '' ======================== PASO 2 =============================
        Dim fiveDigitsVerhoeff As String = sumOfVariables5Verhoeff.Substring(sumOfVariables5Verhoeff.Length - 5)
        Dim numbers(5) As Integer
        For i = 1 To 5
            numbers(i - 1) = Int32.Parse(fiveDigitsVerhoeff(i - 1).ToString()) + 1
        Next

        Dim string1 As String = dosageKey.Substring(0, numbers(0))
        Dim string2 As String = dosageKey.Substring(numbers(0), numbers(1))
        Dim string3 As String = dosageKey.Substring(numbers(0) + numbers(1), numbers(2))
        Dim string4 As String = dosageKey.Substring(numbers(0) + numbers(1) + numbers(2), numbers(3))
        Dim string5 = dosageKey.Substring(numbers(0) + numbers(1) + numbers(2) + numbers(3), numbers(4))

        Dim authorizationNumberDKey As String = authorizationNumber & string1
        Dim invoiceNumberdKey As String = invoiceNumber & string2
        Dim NITCIDKey As String = nitci & string3
        Dim dateOfTransactionDKey As String = dateOfTransaction & string4
        Dim transactionAmountDKey As String = transactionAmount & string5

        '' ======================== PASO 3 =============================

        Dim stringDKey As String = authorizationNumberDKey & invoiceNumberdKey & NITCIDKey & dateOfTransactionDKey & transactionAmountDKey
        Dim keyForEncryption As String = dosageKey + fiveDigitsVerhoeff
        Dim allegedRC4String As String = AllegedRC4.encryptMessageRC4(stringDKey, keyForEncryption, True)
        ''Console.WriteLine("stringDKey ----------------->" & stringDKey)

        '' ======================== PASO 4 =============================
        Dim totalAmount As Integer = 0
        Dim sp1 As Integer = 0
        Dim sp2 As Integer = 0
        Dim sp3 As Integer = 0
        Dim sp4 As Integer = 0
        Dim sp5 As Integer = 0

        Dim asciiBytes() As Byte = System.Text.Encoding.ASCII.GetBytes(allegedRC4String)
        Dim tmp As Integer = 1
        For i = 0 To asciiBytes.Length - 1
            totalAmount += asciiBytes(i)
            Select Case tmp
                Case 1
                    sp1 += asciiBytes(i)
                Case 2
                    sp2 += asciiBytes(i)
                Case 3
                    sp3 += asciiBytes(i)
                Case 4
                    sp4 += asciiBytes(i)
                Case 5
                    sp5 += asciiBytes(i)
            End Select
            If (tmp < 5) Then tmp += 1 Else tmp = 1
        Next

        '' ======================== PASO 5 =============================
        Dim tmp1 As Integer = totalAmount * sp1 \ numbers(0)
        Dim tmp2 As Integer = totalAmount * sp2 \ numbers(1)
        Dim tmp3 As Integer = totalAmount * sp3 \ numbers(2)
        Dim tmp4 As Integer = totalAmount * sp4 \ numbers(3)
        Dim tmp5 As Integer = totalAmount * sp5 \ numbers(4)

        Dim sumProduct As Integer = tmp1 + tmp2 + tmp3 + tmp4 + tmp5
        Dim base64SIN As String = Facturacion.Base64SIN.convertBase64(sumProduct) 'base64SIN.convertBase64(sumProduct)

        '' ======================== PASO 6 =============================
        Return AllegedRC4.encryptMessageRC4(base64SIN, String.Concat(dosageKey, fiveDigitsVerhoeff), False)

    End Function

    ''' <summary>
    ''' Agrega N digitos d Verhoeff a una cadena
    ''' </summary>
    ''' <param name="value">cadena donde agregar digitos de Verhoeff</param>
    ''' <param name="max">cantidad de digitos a agregar</param>
    ''' <returns>cadena original + N digitos de Verhoeff</returns>
    Private Shared Function addVerhoeffDigit(ByVal value As String, ByVal max As Integer) As String
        For i = 1 To max
            value = String.Concat(value, Verhoeff.generateVerhoeff(value))
        Next
        Return value
    End Function


    ''' <summary>
    ''' redonde un numero hacia arriba 
    ''' </summary>
    ''' <param name="value">cadena que contiene el numero a redondear de la forma 123 ; 123.6 123,6</param>
    ''' <returns>el numero redondeado</returns>
    Public Shared Function roundUp(ByVal value As String) As String
        Dim nfi As System.Globalization.NumberFormatInfo = New System.Globalization.CultureInfo("es-ES", False).NumberFormat
        If nfi.CurrencyDecimalSeparator = "," Then
            value = Replace(value, ".", ",")
        ElseIf nfi.CurrencyDecimalSeparator = "." Then
            value = Replace(value, ",", ".")
        End If

        Dim d As Decimal = System.Convert.ToDecimal(value)
        Return Math.Round(d, MidpointRounding.AwayFromZero).ToString
    End Function

End Class