Public Class ConvertirLiteral

    Public Shared Function A_fnConvertirLiteral(ByVal lNumero As Long) As String
        On Error GoTo FuncErr
        Dim sLiteral As String

        'Revisamos el rango
        If lNumero >= 1 And lNumero <= 999999 Then

            'Conseguimos el literal
            sLiteral = A_fnMillares(lNumero)

            'Cambiamos la primera letra a mayúscula
            A_fnConvertirLiteral = Trim(UCase(Left(sLiteral, 1)) & Mid(sLiteral, 2))
        Else

        End If
        Exit Function
FuncErr:
    End Function

    Private Shared Function A_fnMillares(ByVal nNumero As Long) As String
        If nNumero < 1000 Then
            A_fnMillares = A_fnCentenas(nNumero)
        ElseIf nNumero >= 1000 And nNumero < 2000 Then
            A_fnMillares = "mil " & A_fnCentenas(nNumero - Int(nNumero / 1000) * 1000)
        Else
            A_fnMillares = A_fnCentenas(Int(nNumero / 1000), True) & " mil " & A_fnCentenas(nNumero - Int(nNumero / 1000) * 1000)
        End If
    End Function

    Private Shared Function A_fnCentenas(ByVal nNumero As Long, Optional ByVal bMl As Boolean = False) As String
        If nNumero < 100 Then
            A_fnCentenas = A_fnDecenas(nNumero, bMl)
        ElseIf nNumero >= 100 And nNumero < 200 Then
            A_fnCentenas = "cien" & IIf((nNumero - 100) = 0, "", "to " & A_fnDecenas(nNumero - 100, bMl))
        ElseIf nNumero >= 200 And nNumero < 300 Then
            A_fnCentenas = "doscientos" & IIf((nNumero - 200) = 0, "", " " & A_fnDecenas(nNumero - 200, bMl))
        ElseIf nNumero >= 300 And nNumero < 400 Then
            A_fnCentenas = "trescientos" & IIf((nNumero - 300) = 0, "", " " & A_fnDecenas(nNumero - 300, bMl))
        ElseIf nNumero >= 400 And nNumero < 500 Then
            A_fnCentenas = "cuatrocientos" & IIf((nNumero - 400) = 0, "", " " & A_fnDecenas(nNumero - 400, bMl))
        ElseIf nNumero >= 500 And nNumero < 600 Then
            A_fnCentenas = "quinientos" & IIf((nNumero - 500) = 0, "", " " & A_fnDecenas(nNumero - 500, bMl))
        ElseIf nNumero >= 600 And nNumero < 700 Then
            A_fnCentenas = "seiscientos" & IIf((nNumero - 600) = 0, "", " " & A_fnDecenas(nNumero - 600, bMl))
        ElseIf nNumero >= 700 And nNumero < 800 Then
            A_fnCentenas = "setecientos" & IIf((nNumero - 700) = 0, "", " " & A_fnDecenas(nNumero - 700, bMl))
        ElseIf nNumero >= 800 And nNumero < 900 Then
            A_fnCentenas = "ochocientos" & IIf((nNumero - 800) = 0, "", " " & A_fnDecenas(nNumero - 800, bMl))
        ElseIf nNumero >= 900 And nNumero < 1000 Then
            A_fnCentenas = "novecientos" & IIf((nNumero - 900) = 0, "", " " & A_fnDecenas(nNumero - 900, bMl))
        End If
    End Function

    Private Shared Function A_fnDecenas(ByVal nNumero As Long, Optional ByVal bMl As Boolean = False) As String
        If nNumero < 10 Then
            A_fnDecenas = fnUnidades(nNumero)
        ElseIf nNumero = 10 Then
            A_fnDecenas = "diez"
        ElseIf nNumero = 11 Then
            A_fnDecenas = "once"
        ElseIf nNumero = 12 Then
            A_fnDecenas = "doce"
        ElseIf nNumero = 13 Then
            A_fnDecenas = "trece"
        ElseIf nNumero = 14 Then
            A_fnDecenas = "catorce"
        ElseIf nNumero = 15 Then
            A_fnDecenas = "quince"
        ElseIf nNumero >= 16 And nNumero < 20 Then
            A_fnDecenas = "dieci" & fnUnidades(nNumero - 10)
        ElseIf nNumero >= 20 And nNumero < 30 Then
            A_fnDecenas = "veint" & IIf((nNumero - 20) = 0, "e", "i" & fnUnidades(nNumero - 20, bMl))
        ElseIf nNumero >= 30 And nNumero < 40 Then
            A_fnDecenas = "treinta" & IIf((nNumero - 30) = 0, "", " y " & fnUnidades(nNumero - 30, bMl))
        ElseIf nNumero >= 40 And nNumero < 50 Then
            A_fnDecenas = "cuarenta" & IIf((nNumero - 40) = 0, "", " y " & fnUnidades(nNumero - 40, bMl))
        ElseIf nNumero >= 50 And nNumero < 60 Then
            A_fnDecenas = "cincuenta" & IIf((nNumero - 50) = 0, "", " y " & fnUnidades(nNumero - 50, bMl))
        ElseIf nNumero >= 60 And nNumero < 70 Then
            A_fnDecenas = "sesenta" & IIf((nNumero - 60) = 0, "", " y " & fnUnidades(nNumero - 60, bMl))
        ElseIf nNumero >= 70 And nNumero < 80 Then
            A_fnDecenas = "setenta" & IIf((nNumero - 70) = 0, "", " y " & fnUnidades(nNumero - 70, bMl))
        ElseIf nNumero >= 80 And nNumero < 90 Then
            A_fnDecenas = "ochenta" & IIf((nNumero - 80) = 0, "", " y " & fnUnidades(nNumero - 80, bMl))
        ElseIf nNumero >= 90 And nNumero < 100 Then
            A_fnDecenas = "noventa" & IIf((nNumero - 90) = 0, "", " y " & fnUnidades(nNumero - 90, bMl))
        End If
    End Function

    Private Shared Function fnUnidades(ByVal nNumero As Long, Optional ByVal bMl As Boolean = False) As String
        Select Case nNumero
            Case 1
                If bMl Then
                    fnUnidades = "un"
                Else
                    fnUnidades = "uno"
                End If
            Case 2
                fnUnidades = "dos"
            Case 3
                fnUnidades = "tres"
            Case 4
                fnUnidades = "cuatro"
            Case 5
                fnUnidades = "cinco"
            Case 6
                fnUnidades = "seis"
            Case 7
                fnUnidades = "siete"
            Case 8
                fnUnidades = "ocho"
            Case 9
                fnUnidades = "nueve"
        End Select
    End Function

End Class
