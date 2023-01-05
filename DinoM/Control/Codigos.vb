Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D

Imports iTextSharp.text.pdf
Imports iTextSharp.text.pdf.BarcodeCodabar
Public Class Codigos
    Public Shared Function codigo128(ByVal _code As String, Optional ByVal vertexto As Boolean = False, Optional ByVal Height As Single = 0)
        Dim barcode As New BarcodeCodabar
        barcode.StartStopText = True
        If Height <> 0 Then
            barcode.BarHeight = Height
        End If
        barcode.Code = _code
        Try
            Dim bm As New System.Drawing.Bitmap(barcode.CreateDrawingImage(Color.Black, Color.White))
            If vertexto = False Then
                Return bm
            Else
                'generando el texto
                Dim bmT As Image
                bmT = New Bitmap(500, 700 + 14)
                Dim g As Graphics = Graphics.FromImage(bmT)
                g.FillRectangle(New SolidBrush(Color.White), 0, 0, 500, 700 + 14)

                Dim pintarTexto As New Font("Arial", 8)
                Dim brocha As New SolidBrush(Color.Black)

                Dim stringSize As New SizeF
                stringSize = g.MeasureString(_code, pintarTexto)
                Dim centrox As Single = (bm.Width - stringSize.Width) / 2
                Dim x As Single = centrox
                Dim y As Single = bm.Height

                Dim drawformat As New StringFormat
                drawformat.FormatFlags = StringFormatFlags.NoWrap
                g.DrawImage(bm, 0, 0)

                Dim ncode As String = _code.Substring(1, _code.Length - 2)
                g.DrawString(ncode, pintarTexto, brocha, x, y, drawformat)
                Return bmT

            End If
        Catch ex As Exception
            Throw New Exception("Error al generar el codigo" & ex.ToString)
        End Try
    End Function

End Class
