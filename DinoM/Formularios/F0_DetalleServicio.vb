
Imports DevComponents.DotNetBar
Imports System.IO
Imports DevComponents.DotNetBar.SuperGrid
Imports GMap.NET.MapProviders
Imports GMap.NET
Imports GMap.NET.WindowsForms.Markers
Imports GMap.NET.WindowsForms
Imports GMap.NET.WindowsForms.ToolTips
Imports System.Drawing
Imports DevComponents.DotNetBar.Controls
Imports Logica.AccesoLogica
Public Class F0_DetalleServicio

    Public NombreServicio As String
    Public Detalle As String
    Public Precio As Double
    Public bandera As Boolean = False


    Private Sub F0_DetalleServicio_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lbServicio.Text = NombreServicio
        tbDetalle.Text = ""
        tbPrecio.Value = 0

        tbDetalle.Focus()
    End Sub

    Private Sub btnContinuar_Click(sender As Object, e As EventArgs) Handles btnContinuar.Click
        If (tbDetalle.Text.Trim = String.Empty) Then
            ToastNotification.Show(Me, "Debes Ingresar Un Detalle Valido", My.Resources.WARNING, 4000, eToastGlowColor.Red, eToastPosition.TopCenter)
            tbDetalle.Focus()
            Return

        End If
        If (tbPrecio.Value <= 0) Then
            ToastNotification.Show(Me, "Debes Ingresar Un Precio Valido", My.Resources.WARNING, 4000, eToastGlowColor.Red, eToastPosition.TopCenter)
            tbPrecio.Focus()

            Return

        End If

        Detalle = tbDetalle.Text
        Precio = tbPrecio.Value
        bandera = True
        Me.Close()


    End Sub

    Private Sub BtnSalir_Click(sender As Object, e As EventArgs) Handles BtnSalir.Click
        bandera = False
        Me.Close()

    End Sub

    Private Sub tbDetalle_KeyDown(sender As Object, e As KeyEventArgs) Handles tbDetalle.KeyDown
        If (e.KeyData = Keys.Enter) Then
            tbPrecio.Focus()
        End If
    End Sub

    Private Sub tbPrecio_KeyDown(sender As Object, e As KeyEventArgs) Handles tbPrecio.KeyDown
        If (e.KeyData = Keys.Enter) Then
            btnContinuar.Focus()
        End If
    End Sub
End Class