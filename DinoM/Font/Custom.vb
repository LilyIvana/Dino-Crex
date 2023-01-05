Imports System.Drawing.Text
Imports System.IO
Imports System.Reflection
Imports System.Runtime.InteropServices

Namespace My.Resources

    Public Module Custom

#Region " Variables "

        Private PrivateFonts As PrivateFontCollection

#End Region

#Region " Methods "

        Public Function GetFont(ByVal size As Single, _
                                ByVal style As FontStyle) As Font
            'Cargo fuente
            LoadFont()

            'Devuelvo fuente
            Return New Font(PrivateFonts.Families.GetValue(0).ToString, size, style, GraphicsUnit.Point)
        End Function

        Private Sub LoadFont()
            'Inicializo colección
            If PrivateFonts Is Nothing Then
                PrivateFonts = New PrivateFontCollection
            Else 'Cancelo
                Return
            End If

            'Variables
            Dim fontData() As Byte = My.Resources.HOMINIS
            Dim memoryData As IntPtr = Marshal.AllocCoTaskMem(fontData.Length)

            'Copio fuente a la memoria
            Marshal.Copy(fontData, 0, memoryData, fontData.Length)

            'Agrego fuente a la colección
            PrivateFonts.AddMemoryFont(memoryData, fontData.Length)

            'Libero memoria
            Marshal.FreeCoTaskMem(memoryData)
        End Sub

#End Region

    End Module

End Namespace
