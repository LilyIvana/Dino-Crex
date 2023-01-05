Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Public Class Pr_VentasAtendidas
    Dim _Inter As Integer = 0

    Public _nameButton As String
    Public _tab As SuperTabItem
    Public Sub _prIniciarTodo()
        tbFechaI.Value = Now.Date
        tbFechaF.Value = Now.Date
        _PMIniciarTodo()

        Me.Text = "REPORTE VENTAS ATENDIDAS"
        MReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        _IniciarComponentes()
    End Sub
    Public Sub _IniciarComponentes()
        tbVendedor.ReadOnly = True
        tbAlmacen.ReadOnly = True
        tbVendedor.Enabled = False
        tbAlmacen.Enabled = False
        CheckTodosVendedor.CheckValue = True
        CheckTodosAlmacen.CheckValue = True
        ckTodosCliente.CheckValue = True
        If (gb_FacturaIncluirICE) Then
            swIce.Visible = True
        Else
            swIce.Visible = False
        End If

    End Sub
    Public Function _prValidadrFiltros() As DataTable

        Dim fechaDesde As DateTime = tbFechaI.Value.ToString("yyyy/MM/dd")
        Dim fechaHasta As DateTime = tbFechaF.Value.ToString("yyyy/MM/dd")
        Dim idVendedor As Integer = 0
        Dim idCliente As Integer = 0
        Dim idAlmacen As Integer = 0
        If CheckTodosAlmacen.Checked = False And CheckUnaALmacen.Checked = True And tbCodAlmacen.Text <> String.Empty Then
            idAlmacen = tbCodAlmacen.Text
        End If
        If CheckTodosVendedor.Checked = False And checkUnaVendedor.Checked = True And tbCodigoVendedor.Text <> String.Empty Then
            idVendedor = tbCodigoVendedor.Text
        End If
        If ckTodosCliente.Checked = False And ckUnoCliente.Checked = True And tbCodigoCliente.Text <> String.Empty Then
            idCliente = tbCodigoCliente.Text
        End If

        'Obtiene las ventas con y sin factura
        Dim ventasAtendidas As DataTable = L_BuscarVentasAtentidas(fechaDesde, fechaHasta, idAlmacen, idVendedor, idCliente)
        Return ventasAtendidas

    End Function
    Private Sub EjecutarReporte(objrep As Object, tabla As DataTable)
        objrep.SetDataSource(tabla)
        Dim fechaI As String = tbFechaI.Value.ToString("dd/MM/yyyy")
        Dim fechaF As String = tbFechaF.Value.ToString("dd/MM/yyyy")
        objrep.SetParameterValue("usuario", L_Usuario)
        objrep.SetParameterValue("fechaI", fechaI)
        objrep.SetParameterValue("fechaF", fechaF)
        MReportViewer.ReportSource = objrep
        MReportViewer.Show()
        MReportViewer.BringToFront()
    End Sub
    Private Sub _prCargarReporte()
        Dim _ventasAtendidas As New DataTable
        _ventasAtendidas = _prValidadrFiltros()
        If (_ventasAtendidas.Rows.Count > 0) Then
            If (swTipoVenta.Value = True) Then
                If swMostrar.Value = True Then
                    Dim objrep As New R_VentasAtendidasAlmacenVendedor
                    EjecutarReporte(objrep, _ventasAtendidas)
                Else
                    Dim objrep As New R_VentasAtendidasAlmacenVendedorRS
                    EjecutarReporte(objrep, _ventasAtendidas)
                End If
            Else
                If swMostrar.Value = True Then
                    Dim objrep As New R_VentasAtendidasVendedorAlmacen
                    EjecutarReporte(objrep, _ventasAtendidas)
                Else
                    Dim objrep As New R_VentasAtendidasVendedorAlmacenRS
                    EjecutarReporte(objrep, _ventasAtendidas)
                End If
            End If
        Else
            ToastNotification.Show(Me, "NO HAY DATOS PARA LOS PARAMETROS SELECCIONADOS..!!!",
                                       My.Resources.INFORMATION, 2000,
                                       eToastGlowColor.Blue,
                                       eToastPosition.BottomLeft)
            MReportViewer.ReportSource = Nothing
        End If
    End Sub
    Private Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        _prCargarReporte()
    End Sub

    Private Sub Pr_VentasAtendidas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()
    End Sub
    Private Sub checkUnaVendedor_CheckValueChanged(sender As Object, e As EventArgs) Handles checkUnaVendedor.CheckValueChanged
        If (checkUnaVendedor.Checked) Then
            CheckTodosVendedor.CheckValue = False
            tbVendedor.Enabled = True
            tbVendedor.BackColor = Color.White
            tbVendedor.Focus()
        End If
    End Sub

    Private Sub CheckTodosVendedor_CheckValueChanged(sender As Object, e As EventArgs) Handles CheckTodosVendedor.CheckValueChanged
        If (CheckTodosVendedor.Checked) Then
            checkUnaVendedor.CheckValue = False
            tbVendedor.Enabled = True
            tbVendedor.BackColor = Color.Gainsboro
            tbVendedor.Clear()
            tbCodigoVendedor.Clear()
        End If
    End Sub

    Private Sub CheckUnaALmacen_CheckValueChanged(sender As Object, e As EventArgs) Handles CheckUnaALmacen.CheckValueChanged
        If (CheckUnaALmacen.Checked) Then
            CheckTodosAlmacen.CheckValue = False
            tbAlmacen.Enabled = True
            tbAlmacen.BackColor = Color.White
            tbAlmacen.Focus()
            tbAlmacen.ReadOnly = False
            _prCargarComboLibreriaSucursal(tbAlmacen)
            If (CType(tbAlmacen.DataSource, DataTable).Rows.Count > 0) Then
                tbAlmacen.SelectedIndex = 0
            End If
        End If
    End Sub

    Private Sub ckTodosCliente_CheckValueChanged(sender As Object, e As EventArgs) Handles ckTodosCliente.CheckValueChanged
        If (ckTodosCliente.Checked) Then
            ckUnoCliente.CheckValue = False
            tbCliente.Enabled = True
            tbCliente.BackColor = Color.Gainsboro
            tbCliente.Clear()
            tbCodigoCliente.Clear()
        End If
    End Sub

    Private Sub ckUnoCliente_CheckedChanged(sender As Object, e As EventArgs) Handles ckUnoCliente.CheckedChanged
        If (ckUnoCliente.Checked) Then
            ckTodosCliente.CheckValue = False
            tbCliente.Enabled = True
            tbCliente.BackColor = Color.White
            tbCliente.Focus()
        End If
    End Sub
    Private Sub CheckTodosAlmacen_CheckValueChanged(sender As Object, e As EventArgs) Handles CheckTodosAlmacen.CheckValueChanged
        If (CheckTodosAlmacen.Checked) Then
            CheckUnaALmacen.CheckValue = False
            tbAlmacen.Enabled = True
            tbAlmacen.BackColor = Color.Gainsboro
            tbAlmacen.ReadOnly = True
            _prCargarComboLibreriaSucursal(tbAlmacen)
            CType(tbAlmacen.DataSource, DataTable).Rows.Clear()
            tbAlmacen.SelectedIndex = -1

        End If
    End Sub

    Private Sub _prCargarComboLibreriaSucursal(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnListarSucursales()
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("aanumi").Width = 60
            .DropDownList.Columns("aanumi").Caption = "COD"
            .DropDownList.Columns.Add("aabdes").Width = 500
            .DropDownList.Columns("aabdes").Caption = "SUCURSAL"
            .ValueMember = "aanumi"
            .DisplayMember = "aabdes"
            .DataSource = dt
            .Refresh()
        End With
    End Sub

    Private Sub tbVendedor_KeyDown_1(sender As Object, e As KeyEventArgs) Handles tbVendedor.KeyDown
        If (checkUnaVendedor.Checked) Then
            If e.KeyData = Keys.Control + Keys.Enter Then
                Dim dt As DataTable
                dt = L_fnListarEmpleado()
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
                ef.Context = "Seleccione Vendedor".ToUpper
                ef.ShowDialog()
                Dim bandera As Boolean = False
                bandera = ef.band
                If (bandera = True) Then
                    Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
                    If (IsNothing(Row)) Then
                        tbVendedor.Focus()
                        Return
                    End If
                    tbCodigoVendedor.Text = Row.Cells("ydnumi").Value
                    tbVendedor.Text = Row.Cells("yddesc").Value
                    btnGenerar.Focus()
                End If

            End If

        End If
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Me.Close()
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

    Private Sub tbCliente_KeyDown(sender As Object, e As KeyEventArgs) Handles tbCliente.KeyDown
        If (ckUnoCliente.Checked) Then
            If e.KeyData = Keys.Control + Keys.Enter Then

                Dim dt As DataTable
                'dt = L_fnListarClientes()
                dt = L_fnListarClientesVenta()

                Dim listEstCeldas As New List(Of Modelo.Celda)
                listEstCeldas.Add(New Modelo.Celda("ydnumi,", True, "ID", 50))
                listEstCeldas.Add(New Modelo.Celda("ydcod", False, "ID", 50))
                listEstCeldas.Add(New Modelo.Celda("ydrazonsocial", True, "RAZÓN SOCIAL", 180))
                listEstCeldas.Add(New Modelo.Celda("yddesc", True, "NOMBRE", 280))
                listEstCeldas.Add(New Modelo.Celda("yddctnum", True, "N. Documento".ToUpper, 150))
                listEstCeldas.Add(New Modelo.Celda("yddirec", True, "DIRECCIÓN", 220))
                listEstCeldas.Add(New Modelo.Celda("ydtelf1", True, "Teléfono".ToUpper, 200))
                listEstCeldas.Add(New Modelo.Celda("ydfnac", True, "F.Nacimiento".ToUpper, 150, "MM/dd,YYYY"))
                listEstCeldas.Add(New Modelo.Celda("ydnumivend,", False, "ID", 50))
                listEstCeldas.Add(New Modelo.Celda("vendedor,", False, "ID", 50))
                listEstCeldas.Add(New Modelo.Celda("yddias", False, "CRED", 50))
                listEstCeldas.Add(New Modelo.Celda("ydnomfac", False, "Nombre Factura", 50))
                listEstCeldas.Add(New Modelo.Celda("ydnit", False, "Nit/CI", 50))
                Dim ef = New Efecto
                ef.tipo = 3
                ef.dt = dt
                ef.SeleclCol = 2
                ef.listEstCeldas = listEstCeldas
                ef.alto = 50
                ef.ancho = 350
                ef.Context = "Seleccione Cliente".ToUpper
                ef.ShowDialog()
                Dim bandera As Boolean = False
                bandera = ef.band
                If (bandera = True) Then
                    Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row

                    tbCodigoCliente.Text = Row.Cells("ydnumi").Value
                    tbCliente.Text = Row.Cells("yddesc").Value
                End If
            End If
        End If
    End Sub
End Class