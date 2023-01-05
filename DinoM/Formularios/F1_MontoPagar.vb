Imports Janus.Windows.GridEX
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

'importando librerias api conexion
Imports Newtonsoft.Json
Imports System.Xml
Public Class F1_MontoPagar

    Public TotalVenta As Double
    Public Bandera As Boolean = False
    Public TotalBs As Double = 0
    Public TotalSus As Double = 0
    Public TotalTarjeta As Double = 0
    Public Nit As String = ""
    Public RazonSocial As String = ""
    Public TipoCambio As Double = 0
    Public tipoDoc As ComboBox
    Public email As String = ""
    Public IdNit As String = ""

    Public CExcep As Integer
    Public NuevoCliente As Boolean = False

    Private Sub F1_MontoPagar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prCargarComboLibreria(cbCambioDolar, 7, 1)
        cbCambioDolar.SelectedIndex = CType(cbCambioDolar.DataSource, DataTable).Rows.Count - 1

        tbNit.Focus()
        tbNit.Select()
        txtMontoPagado1.Text = "0.00"
        txtCambio1.Text = "0.00"
        tbMontoBs.Value = 0
        tbMontoDolar.Value = 0
        tbMontoTarej.Value = 0
        tbMontoTarej.Enabled = False
        tbNit.Text = Nit
        tbRazonSocial.Text = RazonSocial

        chbTarjeta.Checked = False
        tbNroTarjeta.Text = ""
        lbNroTarjeta.Visible = False
        tbNroTarjeta.Visible = False
        lbEjemplo.Visible = False

        'tbNit.Focus()

    End Sub
    Private Sub _prCargarComboLibreria(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo, cod1 As String, cod2 As String)
        Dim dt As New DataTable
        dt = L_prLibreriaClienteLGeneral(cod1, cod2)
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("yccod3").Width = 70
            .DropDownList.Columns("yccod3").Caption = "COD"
            .DropDownList.Columns.Add("ycdes3").Width = 200
            .DropDownList.Columns("ycdes3").Caption = "DESCRIPCION"
            .ValueMember = "yccod3"
            .DisplayMember = "ycdes3"
            .DataSource = dt
            .Refresh()
        End With
    End Sub
    Private Sub tbMontoBs_ValueChanged(sender As Object, e As EventArgs) Handles tbMontoBs.ValueChanged
        tbMontoDolar.Value = 0
        tbMontoTarej.Value = 0

        Dim diferencia As Double = tbMontoBs.Value - TotalVenta
        If (diferencia >= 0) Then
            txtMontoPagado1.Text = TotalVenta.ToString
            txtCambio1.Text = Math.Round(diferencia, 2).ToString

        Else
            txtMontoPagado1.Text = "0.00"
            txtCambio1.Text = "0.00"
        End If

    End Sub

    Private Sub tbMontoDolar_ValueChanged(sender As Object, e As EventArgs) Handles tbMontoDolar.ValueChanged
        tbMontoBs.Value = 0
        tbMontoTarej.Value = 0
        Dim diferencia As Double = (tbMontoDolar.Value * cbCambioDolar.Text) - TotalVenta
        If (diferencia >= 0) Then
            txtMontoPagado1.Text = TotalVenta.ToString
            txtCambio1.Text = diferencia.ToString

        Else
            txtMontoPagado1.Text = "0.00"
            txtCambio1.Text = "0.00"
        End If
    End Sub

    Private Sub tbMontoTarej_ValueChanged(sender As Object, e As EventArgs) Handles tbMontoTarej.ValueChanged
        tbMontoDolar.Value = 0
        tbMontoBs.Value = 0

        Dim diferencia As Double = tbMontoTarej.Value - TotalVenta
        If (diferencia >= 0) Then
            txtMontoPagado1.Text = TotalVenta.ToString
            txtCambio1.Text = diferencia.ToString

        Else
            txtMontoPagado1.Text = "0.00"
            txtCambio1.Text = "0.00"
        End If
    End Sub

    Private Sub tbMontoBs_KeyDown(sender As Object, e As KeyEventArgs) Handles tbMontoBs.KeyDown

        If (e.KeyData = Keys.Up) Then
            tbRazonSocial.Focus()
        End If
        If (e.KeyData = Keys.Right) Then
            tbMontoDolar.Focus()
        End If
        If (e.KeyData = Keys.Enter) Then
            'tbMontoDolar.Focus()
            btnContinuar.Focus()
        End If
        If (e.KeyData = Keys.Down) Then
            tbMontoTarej.Focus()
        End If
        If (e.KeyData = Keys.Escape) Then
            TotalBs = 0
            TotalSus = 0
            TotalTarjeta = 0
            Bandera = False
            Me.Close()


        End If
        If (e.KeyData = Keys.Control + Keys.S) Then
            If (tbMontoTarej.Value + tbMontoDolar.Value + tbMontoBs.Value >= TotalVenta) Then
                Bandera = True
                TotalBs = tbMontoBs.Value
                TotalSus = tbMontoDolar.Value
                TotalTarjeta = tbMontoTarej.Value
                Nit = tbNit.Text
                RazonSocial = tbRazonSocial.Text
                Me.Close()

            Else
                ToastNotification.Show(Me, "Debe Ingresar un Monto a Cobrar Valido igual o mayor A = " + Str(TotalVenta), My.Resources.WARNING, 4000, eToastGlowColor.Red, eToastPosition.TopCenter)
            End If


        End If
    End Sub

    Private Sub tbMontoDolar_KeyDown(sender As Object, e As KeyEventArgs) Handles tbMontoDolar.KeyDown
        If (e.KeyData = Keys.Left) Then
            tbMontoBs.Focus()
        End If
        If (e.KeyData = Keys.Enter) Then
            tbMontoTarej.Focus()
        End If
        If (e.KeyData = Keys.Down) Then
            tbMontoTarej.Focus()
        End If
        If (e.KeyData = Keys.Escape) Then
            TotalBs = 0
            TotalSus = 0
            TotalTarjeta = 0
            Bandera = False
            Me.Close()


        End If
        If (e.KeyData = Keys.Control + Keys.S) Then
            If (tbMontoTarej.Value + tbMontoDolar.Value + tbMontoBs.Value >= TotalVenta) Then
                Bandera = True
                TotalBs = tbMontoBs.Value
                TotalSus = tbMontoDolar.Value
                TotalTarjeta = tbMontoTarej.Value
                Nit = tbNit.Text
                RazonSocial = tbRazonSocial.Text
                Me.Close()

            Else
                ToastNotification.Show(Me, "Debe Ingresar un Monto a Cobrar Valido igual o mayor A = " + Str(TotalVenta), My.Resources.WARNING, 4000, eToastGlowColor.Red, eToastPosition.TopCenter)
            End If
        End If
    End Sub

    Private Sub tbMontoTarej_KeyDown(sender As Object, e As KeyEventArgs) Handles tbMontoTarej.KeyDown
        If (e.KeyData = Keys.Up) Then
            tbMontoBs.Focus()
        End If
        If (e.KeyData = Keys.Enter) Then
            btnContinuar.Focus()
        End If
        If (e.KeyData = Keys.Left) Then
            tbMontoDolar.Focus()
        End If

        If (e.KeyData = Keys.Escape) Then
            TotalBs = 0
            TotalSus = 0
            TotalTarjeta = 0
            Bandera = False
            Me.Close()


        End If
        If (e.KeyData = Keys.Control + Keys.S) Then
            If (tbMontoTarej.Value + tbMontoDolar.Value + tbMontoBs.Value >= TotalVenta) Then
                Bandera = True
                TotalBs = tbMontoBs.Value
                TotalSus = tbMontoDolar.Value
                TotalTarjeta = tbMontoTarej.Value
                Nit = tbNit.Text
                RazonSocial = tbRazonSocial.Text
                Me.Close()

            Else
                ToastNotification.Show(Me, "Debe Ingresar un Monto a Cobrar Valido igual o mayor A = " + Str(TotalVenta), My.Resources.WARNING, 4000, eToastGlowColor.Red, eToastPosition.TopCenter)
            End If
        End If
    End Sub

    Private Sub tbNit_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles tbNit.Validating
        Dim nom1, nom2, correo, tipoDoc, id As String
        nom1 = ""
        nom2 = ""
        correo = ""
        tipoDoc = ""
        id = ""
        If (tbNit.Text.Trim <> String.Empty) Then
            L_Validar_Nit(tbNit.Text.Trim, nom1, nom2, correo, tipoDoc, id) ''falta validar
            If nom1 = "" Then
                CiNitNuevo()
                'tbRazonSocial.Focus()
                L_Validar_Nit(tbNit.Text.Trim, nom1, nom2, correo, tipoDoc, id)
                IdNit = id

            Else
                tbRazonSocial.Text = nom1
                TbEmail.Text = correo
                CbTipoDoc.Value = tipoDoc
                IdNit = id
            End If
        End If
    End Sub
    Private Sub CiNitNuevo()
        Dim frm As New F_CiNitNuevo
        Dim dt As DataTable
        frm.tbNit.Text = tbNit.Text
        With frm.CbTDoc
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("codigoClasificador").Width = 70
            .DropDownList.Columns("codigoClasificador").Caption = "COD"
            .DropDownList.Columns.Add("descripcion").Width = 500
            .DropDownList.Columns("descripcion").Caption = "DESCRIPCION"
            .ValueMember = "codigoClasificador"
            .DisplayMember = "descripcion"
            .DataSource = CbTipoDoc.DataSource
            .Refresh()
        End With
        frm.CbTDoc.Value = CbTipoDoc.Value
        frm.ShowDialog()


        If (frm.Cliente = True) Then '

            tbRazonSocial.Text = frm.Razonsocial
            TbEmail.Text = frm.Correo
            CbTipoDoc.Value = frm.CbTDoc.Value
            NuevoCliente = frm.NuevoCli
            'dt = L_fnObtenerClientesporRazonSocialNit(frm.Razonsocial, frm.Nit)
            'If (dt.Rows.Count > 0) Then

            'End If
        End If
    End Sub
    Private Sub tbRazonSocial_KeyDown(sender As Object, e As KeyEventArgs) Handles tbRazonSocial.KeyDown
        If (e.KeyData = Keys.Up) Then
            tbNit.Focus()
        End If
        If (e.KeyData = Keys.Down) Then
            TbEmail.Focus()
        End If
        If (e.KeyData = Keys.Enter) Then
            TbEmail.Focus()
        End If

        If (e.KeyData = Keys.Control + Keys.S) Then
            If (tbMontoTarej.Value + tbMontoDolar.Value + tbMontoBs.Value >= TotalVenta) Then
                Bandera = True
                TotalBs = tbMontoBs.Value
                TotalSus = tbMontoDolar.Value
                TotalTarjeta = tbMontoTarej.Value
                Nit = tbNit.Text
                RazonSocial = tbRazonSocial.Text
                Me.Close()

            Else
                ToastNotification.Show(Me, "Debe Ingresar un Monto a Cobrar Valido igual o mayor A = " + Str(TotalVenta), My.Resources.WARNING, 4000, eToastGlowColor.Red, eToastPosition.TopCenter)
            End If
        End If
    End Sub

    Private Sub tbNit_KeyDown(sender As Object, e As KeyEventArgs) Handles tbNit.KeyDown
        If (e.KeyData = Keys.Down) Then
            tbRazonSocial.Focus()
        End If
        If (e.KeyData = Keys.Enter) Then
            tbRazonSocial.Focus()
        End If
        If (e.KeyData = Keys.Control + Keys.S) Then
            If (tbMontoTarej.Value + tbMontoDolar.Value + tbMontoBs.Value >= TotalVenta) Then
                Bandera = True
                TotalBs = tbMontoBs.Value
                TotalSus = tbMontoDolar.Value
                TotalTarjeta = tbMontoTarej.Value
                Nit = tbNit.Text
                RazonSocial = tbRazonSocial.Text
                Me.Close()

            Else
                ToastNotification.Show(Me, "Debe Ingresar un Monto a Cobrar Valido igual o mayor A = " + Str(TotalVenta), My.Resources.WARNING, 4000, eToastGlowColor.Red, eToastPosition.TopCenter)
            End If
        End If

    End Sub

    Private Sub btnContinuar_Click(sender As Object, e As EventArgs) Handles btnContinuar.Click
        Try

            If tbNit.Text = String.Empty Or tbRazonSocial.Text = String.Empty Then
                ToastNotification.Show(Me, "Debe Ingresar Nit y Razón Social obligatoriamente!!!", My.Resources.WARNING, 3500, eToastGlowColor.Red, eToastPosition.TopCenter)
                Exit Sub
            End If
            If (CbTipoDoc.SelectedIndex < 0) Then
                ToastNotification.Show(Me, "Por Favor Seleccione Tipo de Documento!!!", My.Resources.WARNING, 3500, eToastGlowColor.Red, eToastPosition.TopCenter)
                CbTipoDoc.Focus()
                Exit Sub
            End If

            If NuevoCliente = False Then
                Dim tokenSifac As String = F0_VentasSupermercado.ObtToken()
                Dim code = F0_VentasSupermercado.VerifConexion(tokenSifac)
                If (code = 200) Then
                    If (CbTipoDoc.Value = 5) Then ''El tipo de Doc. es Nit
                        Dim Succes As Integer = F0_VentasSupermercado.VerificarNit(tokenSifac, tbNit.Text)
                        If Succes <> 200 Then
                            'If F0_VentasSupermercado.CodExcepcion = 1 Then
                            '    'Prosigue normal con el grabado
                            '    CExcep = F0_VentasSupermercado.CodExcepcion
                            'Else
                            '    CExcep = F0_VentasSupermercado.CodExcepcion
                            '    Exit Sub
                            'End If
                            Exit Sub
                        End If
                    End If
                End If
            End If

            If (chbTarjeta.Checked = True) Then

                If tbNroTarjeta.Text = String.Empty Or tbNroTarjeta.Text = "0" Then
                    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                    ToastNotification.Show(Me, "Debe colocar el Nro. de Tarjeta".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                    tbNroTarjeta.Focus()
                    Exit Sub
                End If
            End If

            If (tbMontoTarej.Value + (tbMontoDolar.Value * cbCambioDolar.Text) + tbMontoBs.Value >= TotalVenta) Then
                Bandera = True
                TotalBs = tbMontoBs.Value
                TotalSus = tbMontoDolar.Value
                TotalTarjeta = tbMontoTarej.Value
                Nit = tbNit.Text
                RazonSocial = tbRazonSocial.Text
                TipoCambio = cbCambioDolar.Text
                'tipoDoc = CbTipoDoc.Value
                'email = TbEmail.Text
                Me.Close()

            Else
                ToastNotification.Show(Me, "Debe Ingresar un Monto a Cobrar Valido igual o mayor A = " + Str(TotalVenta), My.Resources.WARNING, 4000, eToastGlowColor.Red, eToastPosition.TopCenter)
                tbMontoBs.Focus()
            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub
    Private Sub MostrarMensajeError(mensaje As String)
        ToastNotification.Show(Me,
                               mensaje.ToUpper,
                               My.Resources.WARNING,
                               5000,
                               eToastGlowColor.Red,
                               eToastPosition.TopCenter)

    End Sub
    Private Sub BtnSalir_Click(sender As Object, e As EventArgs) Handles BtnSalir.Click
        TotalBs = 0
        TotalSus = 0
        TotalTarjeta = 0
        Bandera = False
        Me.Close()

    End Sub

    Private Sub chbTarjeta_CheckedChanged(sender As Object, e As EventArgs) Handles chbTarjeta.CheckedChanged
        If chbTarjeta.Checked Then
            tbMontoBs.Value = 0
            tbMontoDolar.Value = 0
            tbMontoTarej.Enabled = True
            tbMontoTarej.Value = Convert.ToDecimal(TotalVenta)
            tbMontoBs.Enabled = False
            tbMontoDolar.Enabled = False
            tbMontoTarej.IsInputReadOnly = True
            lbNroTarjeta.Visible = True
            tbNroTarjeta.Visible = True
            lbEjemplo.Visible = True

            tbMontoTarej.Focus()
        Else
            tbMontoBs.Enabled = True
            tbMontoDolar.Enabled = True
            tbMontoTarej.Value = 0
            lbNroTarjeta.Visible = False
            tbNroTarjeta.Visible = False
            lbEjemplo.Visible = False
        End If
    End Sub

    Private Sub cbCambioDolar_ValueChanged(sender As Object, e As EventArgs) Handles cbCambioDolar.ValueChanged
        If cbCambioDolar.SelectedIndex < 0 And cbCambioDolar.Text <> String.Empty Then
            btgrupo1.Visible = True
        Else
            btgrupo1.Visible = False
        End If
    End Sub

    Private Sub btgrupo1_Click(sender As Object, e As EventArgs) Handles btgrupo1.Click
        Dim numi As String = ""

        If L_prLibreriaGrabar(numi, "7", "1", cbCambioDolar.Text, "") Then
            _prCargarComboLibreria(cbCambioDolar, "7", "1")
            cbCambioDolar.SelectedIndex = CType(cbCambioDolar.DataSource, DataTable).Rows.Count - 1
        End If
    End Sub

    Private Sub tbNit_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbNit.KeyPress
        If Not IsNumeric(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
            ToastNotification.Show(Me, "Solo puede digitar números".ToUpper, My.Resources.WARNING, 1200, eToastGlowColor.Red, eToastPosition.TopCenter)

        End If
    End Sub

    Private Sub tbRazonSocial_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbRazonSocial.KeyPress
        If Char.IsLetter(e.KeyChar) Or Char.IsPunctuation(e.KeyChar) Or Char.IsWhiteSpace(e.KeyChar) Or Convert.ToChar(Keys.Back) = (e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub


    Private Sub TbEmail_KeyDown(sender As Object, e As KeyEventArgs) Handles TbEmail.KeyDown
        If (e.KeyData = Keys.Up) Then
            tbRazonSocial.Focus()
        End If
        If (e.KeyData = Keys.Down) Then
            tbMontoBs.Focus()
        End If
        If (e.KeyData = Keys.Enter) Then
            tbMontoBs.Focus()
        End If
    End Sub

    Private Sub tbNroTarjeta_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbNroTarjeta.KeyPress
        g_prValidarTextBox(1, e)
    End Sub
End Class