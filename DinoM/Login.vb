
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
Public Class Login
    Public Sub _habilitarFocus()
        With Highlighter1
            .SetHighlightOnFocus(tbUsuario, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(tbPassword, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(btnIngresar, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)

        End With
    End Sub
    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _habilitarFocus()

        btnIngresar.TextAlign = ContentAlignment.MiddleCenter
        btnIngresar.ForeColor = Color.White
        tbUsuario.Multiline = False
        tbPassword.Multiline = False


        'Dim blah As Bitmap = My.Resources
        'Dim ico As Icon = Icon.FromHandle(blah.GetHicon())

        'Me.Icon = ico
        Me.Text = "L O G I N"

        tbUsuario.CharacterCasing = CharacterCasing.Upper


        Me.Opacity = 0.01
        Timer1.Interval = 10
        Timer1.Enabled = True

        _CargarLogo()

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If Me.Opacity < 1 Then
            Me.Opacity += 0.05
        Else
            Timer1.Enabled = False
        End If
    End Sub

    Private Sub tbname_KeyDown(sender As Object, e As KeyEventArgs) Handles tbUsuario.KeyDown
        If (e.KeyData = Keys.Enter) Then
            tbPassword.Focus()

        End If
    End Sub

    Private Sub tbpass_KeyDown(sender As Object, e As KeyEventArgs) Handles tbPassword.KeyDown
        If (e.KeyData = Keys.Enter) Then
            btnIngresar.Focus()
        End If
    End Sub


    Private Sub btnIngresar_Click_1(sender As Object, e As EventArgs) Handles btnIngresar.Click
        If tbUsuario.Text = "" Then
            ToastNotification.Show(Me, "No Puede Dejar Nombre en Blanco..!!!".ToUpper, My.Resources.WARNING, 1000, eToastGlowColor.Red, eToastPosition.BottomLeft)
            Exit Sub
        End If
        If tbPassword.Text = "" Then
            ToastNotification.Show(Me, "No Puede Dejar Password en Blanco..!!!".ToUpper, My.Resources.WARNING, 1000, eToastGlowColor.Red, eToastPosition.BottomLeft)
            Exit Sub
        End If
        Dim dtUsuario As DataTable = L_Validar_Usuario(tbUsuario.Text, tbPassword.Text)
        If dtUsuario.Rows.Count = 0 Then
            ToastNotification.Show(Me, "Codigo de Usuario y Password Incorrecto..!!!".ToUpper, My.Resources.WARNING, 1000, eToastGlowColor.Red, eToastPosition.BottomLeft)
        Else


            gs_user = tbUsuario.Text
            gi_userNumi = dtUsuario.Rows(0).Item("ydnumi")
            gi_userRol = dtUsuario.Rows(0).Item("ydrol")
            gi_userSuc = dtUsuario.Rows(0).Item("ydsuc")
            'gb_userTodasSuc = IIf(dtUsuario.Rows(0).Item("ydall") = 1, True, False)
            P_Principal.lbUsuario.Text = gs_user
            _prDesvenecerPantalla()

            Try
                Dim dt As DataTable = L_VerConfiguracion()
                gi_Ver_Servicios = dt.Rows(0).Item("VerServicios")
            Catch ex As Exception

            End Try

            If dtUsuario.Rows(0).Item("ydfontsize") = 1 Then
                Dim frm As New NroCaja
                frm.ShowDialog()
            End If

            Close()

        End If
    End Sub
    Private Sub _CargarLogo()
        Try
            Dim dtConfSistema As DataTable = L_fnConfSistemaGeneral()
            gb_UbiLogo = dtConfSistema.Rows(0).Item("cccubilogo")

            PictureBox1.Image = Image.FromFile(gb_UbiLogo.ToString)
        Catch ex As Exception
            MessageBox.Show("No se encontro el logo en la ubicación específicada")
        End Try
    End Sub
    Private Sub _prDesvenecerPantalla()
        Dim a, b As Decimal
        For a = 40 To 0 Step -1
            b = a / 100
            Me.Opacity = b
            Me.Refresh()
        Next
    End Sub

    Private Sub Login_KeyPress(sender As Object, e As KeyPressEventArgs) Handles MyBase.KeyPress, tbPassword.KeyPress, tbUsuario.KeyPress
        If (e.KeyChar = ChrW(Keys.Escape)) Then
            _prDesvenecerPantalla()
            Me.Close()
        End If
    End Sub
End Class