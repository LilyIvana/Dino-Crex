Imports DevComponents.DotNetBar.Controls
Imports DevComponents.DotNetBar
Public Class F_PrecioUnitario

    Public Producto As String
    Public bandera As Boolean
    Public PrecioUni As Decimal

    Private Sub F_PrecioUnitario_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lbProducto.Text = Producto
        tbPrecioUni.Value = PrecioUni
    End Sub

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        bandera = True
        Me.Close()
    End Sub
End Class