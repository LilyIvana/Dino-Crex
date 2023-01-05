Public Class ModeloR0

#Region "Metodos Privados"
    Public Sub _PMIniciarTodo()

        'TxtNombreUsu.Text = MGlobal.gs_usuario
        'TxtNombreUsu.ReadOnly = True

        'Me.WindowState = FormWindowState.Maximized
        Me.SupTabItemBusqueda.Visible = False

    End Sub

#End Region
    Private Sub ModeloHor_KeyPress(sender As Object, e As KeyPressEventArgs) Handles MyBase.KeyPress
        e.KeyChar = e.KeyChar.ToString.ToUpper
        If (e.KeyChar = ChrW(Keys.Enter)) Then
            e.Handled = True
            P_Moverenfoque()
        End If
    End Sub

    Private Sub P_Moverenfoque()
        SendKeys.Send("{TAB}")
    End Sub

    Private Sub MFlyoutUsuario_PrepareContent(sender As Object, e As EventArgs) Handles MFlyoutUsuario.PrepareContent
        If sender Is BubbleBarUsuario And btnGenerar.Enabled = False Then
            MFlyoutUsuario.BorderColor = Color.FromArgb(&HC0, 0, 0)
            MFlyoutUsuario.Content = PanelUsuario
        End If
    End Sub

    Private Sub ModeloR0_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class