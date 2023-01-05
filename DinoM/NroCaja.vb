
Imports DevComponents.DotNetBar
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports Logica.AccesoLogica
Imports Janus.Windows.GridEX
Public Class NroCaja
    Public Sub _habilitarFocus()
        With Highlighter1
            .SetHighlightOnFocus(tbNroCaja, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(btnAceptar, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        End With
    End Sub
    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _habilitarFocus()

        btnAceptar.TextAlign = ContentAlignment.MiddleCenter
        btnAceptar.ForeColor = Color.White
        Me.Text = "NRO. CAJA"

        Me.Opacity = 0.01
        Timer1.Interval = 10
        Timer1.Enabled = True

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If Me.Opacity < 1 Then
            Me.Opacity += 0.05
        Else
            Timer1.Enabled = False
        End If
    End Sub



    Private Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        If tbNroCaja.Value = 0 Then
            ToastNotification.Show(Me, "El Nro. de Caja debe ser mayor a 0".ToUpper, My.Resources.WARNING, 1200, eToastGlowColor.Red, eToastPosition.TopCenter)
            Exit Sub
        End If
        gs_NroCaja = tbNroCaja.Value
        _prDesvenecerPantalla()
        Close()
    End Sub

    Private Sub _prDesvenecerPantalla()
        Dim a, b As Decimal
        For a = 40 To 0 Step -1
            b = a / 100
            Me.Opacity = b
            Me.Refresh()
        Next
    End Sub

    Private Sub Login_KeyPress(sender As Object, e As KeyPressEventArgs) Handles MyBase.KeyPress
        'If (e.KeyChar = ChrW(Keys.Escape)) Then
        '    _prDesvenecerPantalla()
        '    Me.Close()
        'End If
    End Sub

    Private Sub tbNroCaja_KeyDown(sender As Object, e As KeyEventArgs) Handles tbNroCaja.KeyDown
        If (e.KeyData = Keys.Enter) Then
            btnAceptar.Focus()
        End If
    End Sub

End Class