Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Public Class R_KardexCreditoPagos
    Dim _Inter As Integer = 0

    'gb_FacturaIncluirICE

    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem

    Public Sub _prIniciarTodo()
        L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        tbFechaI.Value = Now.Date
        tbFechaF.Value = Now.Date
        _PMIniciarTodo()
        Me.Text = "REPORTE DE ESTADOS DE CUENTAS POR PAGAR"
        MReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        _IniciarComponentes()
    End Sub
    Public Sub _IniciarComponentes()
        tbCliente.ReadOnly = True
        tbCliente.Visible = False
        tbCuentas.Visible = False
        lbcliente.Visible = False
        lbCuentas.Visible = False
        CheckTodosCuenta.CheckValue = True
        CheckUnaCuenta.Visible = False
        CheckTodosCuenta.Visible = False

    End Sub
    Public Sub _prInterpretarDatos(ByRef _dt As DataTable)
        If (swCreditoCliente.Value = True) Then
            _dt = L_prReporteCreditoGeneralCompras(tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"))
        Else
            If (CheckTodosCuenta.Checked And tbCodigoCliente.Text.Length > 0) Then
                _dt = L_prReporteCreditoProveedoresTodosCuentas(tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), tbCodigoCliente.Text)
            End If
            If (CheckUnaCuenta.Checked And tbCodigoCliente.Text.Length > 0 And tbcodCuenta.Text.Length > 0) Then
                _dt = L_prReporteCreditoProveedorUnaCuentas(tbcodCuenta.Text)
            End If

        End If
    End Sub
    Public Sub _prInterpretarDatos2(ByRef _dt As DataTable)
        If (swCreditoCliente.Value = True) Then
            _dt = L_prReporteCreditoGeneralRes(tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"))
        Else
            If (CheckTodosCuenta.Checked And tbCodigoCliente.Text.Length > 0) Then
                _dt = L_prReporteCreditoClienteRes(tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), tbCodigoCliente.Text)
            End If
            If (CheckUnaCuenta.Checked And tbCodigoCliente.Text.Length > 0 And tbcodCuenta.Text.Length > 0) Then
                _dt = L_prReporteCreditoClienteUnaCuentas(tbcodCuenta.Text)
            End If

        End If
    End Sub
    Private Sub _prCargarReporte()
        Dim _dt As New DataTable
        If swdetresum.Value Then
            _prInterpretarDatos(_dt)
        Else
            _prInterpretarDatos2(_dt)
        End If
        _prInterpretarDatos(_dt)
        If (_dt.Rows.Count > 0) Then

            Dim objrep As New R_HisotorialCobrosVentasCredito
            objrep.SetDataSource(_dt)
            Dim dt2 As DataTable = L_fnNameReporte()
            Dim ParEmp1 As String = ""
            Dim ParEmp2 As String = ""
            Dim ParEmp3 As String = ""
            Dim ParEmp4 As String = ""
            If (dt2.Rows.Count > 0) Then
                ParEmp1 = dt2.Rows(0).Item("Empresa1").ToString
                ParEmp2 = dt2.Rows(0).Item("Empresa2").ToString
                ParEmp3 = dt2.Rows(0).Item("Empresa3").ToString
                ParEmp4 = dt2.Rows(0).Item("Empresa4").ToString
            End If
            Dim fechaI As String = tbFechaI.Value.ToString("dd/MM/yyyy")
            Dim fechaF As String = tbFechaF.Value.ToString("dd/MM/yyyy")
            objrep.SetParameterValue("usuario", L_Usuario)
            objrep.SetParameterValue("fechaI", fechaI)
            objrep.SetParameterValue("fechaF", fechaF)
            objrep.SetParameterValue("Empresa", ParEmp1)
            objrep.SetParameterValue("Empresa1", ParEmp2)
            objrep.SetParameterValue("Empresa2", ParEmp3)
            objrep.SetParameterValue("Empresa3", ParEmp4)
            objrep.SetParameterValue("Name", "Proveedor:")
            objrep.SetParameterValue("Total", "Total Compra:")
            MReportViewer.ReportSource = objrep
            MReportViewer.Show()
            MReportViewer.BringToFront()
        Else
            ToastNotification.Show(Me, "NO HAY DATOS PARA LOS PARAMETROS SELECCIONADOS..!!!",
                                       My.Resources.INFORMATION, 2000,
                                       eToastGlowColor.Blue,
                                       eToastPosition.BottomLeft)
            MReportViewer.ReportSource = Nothing
        End If
    End Sub


    Private Sub R_KardexCreditoPagos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()
    End Sub
    Private Sub CheckUnaALmacen_CheckValueChanged(sender As Object, e As EventArgs) Handles CheckUnaCuenta.CheckValueChanged
        If (CheckUnaCuenta.Checked) Then
            CheckTodosCuenta.CheckValue = False
            tbCuentas.Enabled = True
            tbCuentas.BackColor = Color.White
            tbCuentas.Focus()
            tbCuentas.ReadOnly = False


        End If
    End Sub

    Private Sub CheckTodosAlmacen_CheckValueChanged(sender As Object, e As EventArgs) Handles CheckTodosCuenta.CheckValueChanged
        If (CheckTodosCuenta.Checked) Then
            CheckUnaCuenta.CheckValue = False
            tbCuentas.Enabled = True
            tbCuentas.BackColor = Color.Gainsboro
            tbCuentas.ReadOnly = True


        End If
    End Sub


    Private Sub tbVendedor_KeyDown_1(sender As Object, e As KeyEventArgs) Handles tbCliente.KeyDown
        If (swCreditoCliente.Value = False) Then
            If e.KeyData = Keys.Control + Keys.Enter Then
                Dim dt As DataTable
                dt = L_fnListarProveedoresCreditos()
                '              a.ydnumi, a.ydcod, a.yddesc, a.yddctnum, a.yddirec
                ',a.ydtelf1 ,a.ydfnac 
                Dim listEstCeldas As New List(Of Modelo.Celda)
                listEstCeldas.Add(New Modelo.Celda("ydnumi,", False, "ID", 50))
                listEstCeldas.Add(New Modelo.Celda("ydcod", True, "ID", 50))
                listEstCeldas.Add(New Modelo.Celda("yddesc", True, "NOMBRE", 280))
                listEstCeldas.Add(New Modelo.Celda("yddctnum", True, "N. Documento".ToUpper, 150))
                listEstCeldas.Add(New Modelo.Celda("yddirec", True, "DIRECCION", 220))
                listEstCeldas.Add(New Modelo.Celda("ydtelf1", True, "Telefono".ToUpper, 200))
                listEstCeldas.Add(New Modelo.Celda("ydfnac", True, "F.Nacimiento".ToUpper, 150, "MM/dd,YYYY"))
                Dim ef = New Efecto
                ef.tipo = 3
                ef.dt = dt
                ef.SeleclCol = 1
                ef.listEstCeldas = listEstCeldas
                ef.alto = 50
                ef.ancho = 350
                ef.Context = "Seleccione Proveedor".ToUpper
                ef.ShowDialog()
                Dim bandera As Boolean = False
                bandera = ef.band
                If (bandera = True) Then
                    Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
                    If (IsNothing(Row)) Then
                        tbCliente.Focus()
                        Return
                    End If
                    tbCodigoCliente.Text = Row.Cells("ydnumi").Value
                    tbCliente.Text = Row.Cells("yddesc").Value
                    btnGenerar.Focus()
                End If

            End If

        End If
    End Sub

    Private Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        'If swdetresum.Value = True Then
        _prCargarReporte()
        ' End If
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _modulo.Select()
        Me.Close()
    End Sub
    Sub _prHabilitar()
        tbCliente.ReadOnly = True
        tbCliente.Visible = True
        'tbCuentas.Visible = True
        lbcliente.Visible = True
        'lbCuentas.Visible = True
        'CheckTodosCuenta.CheckValue = True
        'CheckUnaCuenta.Visible = True
        'CheckTodosCuenta.Visible = True
        'tbCuentas.BackColor = Color.Gainsboro
        tbCodigoCliente.Clear()
        tbCliente.Clear()
        tbCliente.Focus()
        'tbcodCuenta.Clear()
        'tbCuentas.Clear()
    End Sub

    Private Sub swCreditoCliente_ValueChanged(sender As Object, e As EventArgs) Handles swCreditoCliente.ValueChanged
        If (swCreditoCliente.Value = True) Then
            _IniciarComponentes()
        Else
            _prHabilitar()
        End If
    End Sub

    Private Sub tbCuentas_KeyDown(sender As Object, e As KeyEventArgs) Handles tbCuentas.KeyDown
        If (swCreditoCliente.Value = False And tbCodigoCliente.Text.Length > 0) Then
            If e.KeyData = Keys.Control + Keys.Enter Then
                Dim dt As DataTable
                dt = L_prReporteCreditoListarCuentasPorProveedor(tbCodigoCliente.Text)
                'numiVenta,numeroFactura, fechaVenta,  FechaVencCredito, totalVenta
                Dim listEstCeldas As New List(Of Modelo.Celda)
                listEstCeldas.Add(New Modelo.Celda("tcnumi,", False, "CODIGO", 100))
                listEstCeldas.Add(New Modelo.Celda("numiVenta,", True, "DOC COMPRA", 100))
                listEstCeldas.Add(New Modelo.Celda("numeroFactura", True, "NRO FACTURA", 100))
                listEstCeldas.Add(New Modelo.Celda("fechaVenta", True, "FECHA COMPRA", 130, "yyyy/MM/dd"))
                listEstCeldas.Add(New Modelo.Celda("FechaVencCredito", True, "VENC.CREDITO".ToUpper, 130, "yyyy/MM/dd"))
                listEstCeldas.Add(New Modelo.Celda("totalVenta", True, "TOTAL COMPRA", 130, "0.00"))

                Dim ef = New Efecto
                ef.tipo = 3
                ef.dt = dt
                ef.SeleclCol = 1
                ef.listEstCeldas = listEstCeldas
                ef.alto = 50
                ef.ancho = 350
                ef.Context = "Seleccione Cuenta".ToUpper
                ef.ShowDialog()
                Dim bandera As Boolean = False
                bandera = ef.band
                If (bandera = True) Then
                    Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
                    If (IsNothing(Row)) Then
                        tbCuentas.Focus()
                        Return
                    End If
                    tbCuentas.Text = IIf(Not IsDBNull(Row.Cells("numeroFactura").Value), "FACTURA:" + Str(Row.Cells("numeroFactura").Value), "") + "  DOC VENTA: " + Str(Row.Cells("numiVenta").Value)
                    tbcodCuenta.Text = Row.Cells("tcnumi").Value
                    btnGenerar.Focus()
                End If

            End If

        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        _Inter = _Inter + 1
        If _Inter = 1 Then
            Me.WindowState = FormWindowState.Normal

        Else
            Me.Opacity = 100
            Timer1.Enabled = False
        End If
    End Sub
End Class