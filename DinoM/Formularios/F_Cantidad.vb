Imports DevComponents.DotNetBar.Controls
Imports DevComponents.DotNetBar
Public Class F_Cantidad

    Public Stock As Decimal
    Public Cantidad As Decimal
    Public Producto As String
    Public bandera As Boolean
    Public Conversion As Decimal
    Public CantidadPrevia As Decimal


    Private Sub F_Cantidad_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        tbCantidad.Value = Cantidad
        tbCantidad.Focus()
        lbStock.Text = "Stock Disponible = " + Str(Stock)
        lbProducto.Text = Producto
        tbConversion.Value = Conversion
        tbCantidadPrevia.Value = CantidadPrevia
    End Sub



    Private Sub tbCantidad_KeyDown(sender As Object, e As KeyEventArgs) Handles tbCantidad.KeyDown
        If (e.KeyData = Keys.Escape) Then
            bandera = False
            Me.Close()
        End If
        If (e.KeyData = Keys.Enter) Then
            '''Para controlar que no deje vender cuando no hay stock o la cantidad es mayor al stock que hay
            'If (tbCantidad.Value > Stock) Then
            '    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            '    ToastNotification.Show(Me, "La cantidad Ingresada " + Str(tbCantidad.Value) + " Es superior a la cantidad Disponible del Producto : " + Str(Stock), img, 5000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            '    Return

            'Else
            '    Cantidad = tbCantidad.Value
            '    bandera = True
            '    Me.Close()
            'End If

            Cantidad = tbCantidad.Value
            bandera = True
            Me.Close()
        End If

    End Sub

    Private Sub tbCantCajas_KeyDown(sender As Object, e As KeyEventArgs) Handles tbCantCajas.KeyDown

        If (e.KeyData = Keys.Enter) Then
            tbCantidad.Text = tbCantCajas.Value * tbConversion.Value
            tbCantidad.Focus()
        End If
    End Sub
End Class