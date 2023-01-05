Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Public Class Pr_ReporteVentasCajeras

    Dim _Inter As Integer = 0

    Public _nameButton As String
    Public _tab As SuperTabItem
    Public Sub _prIniciarTodo()
        tbFechaI.Value = Now.Date
        tbFechaF.Value = Now.Date
        _PMIniciarTodo()

        Me.Text = "REPORTE VENTAS CAJAS-PROVEEDORES"
        MReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        _IniciarComponentes()
    End Sub
    Public Sub _IniciarComponentes()

        tbAlmacen.ReadOnly = True
        tbAlmacen.Enabled = False
        CheckTodosAlmacen.CheckValue = True
        tbProveedor.ReadOnly = True
        tbProveedor.Enabled = False
        ckTodosProveedor.CheckValue = True
        tbUsuario.ReadOnly = True
        tbUsuario.Enabled = False
        CheckTodosUsuario.CheckValue = True
        If (gb_FacturaIncluirICE) Then
            'swIce.Visible = True
        Else
            'swIce.Visible = False
        End If

    End Sub
    Public Function _prValidadrFiltros() As DataTable

        Dim fechaDesde As DateTime = tbFechaI.Value.ToString("yyyy/MM/dd")
        Dim fechaHasta As DateTime = tbFechaF.Value.ToString("yyyy/MM/dd")
        Dim idUsuario As Integer = 0
        Dim idProveedor As Integer = 0
        Dim idAlmacen As Integer = 0
        Dim ventasAtendidas As DataTable

        If CheckTodosAlmacen.Checked = False And CheckUnaALmacen.Checked = True And tbCodAlmacen.Text <> String.Empty Then
            idAlmacen = tbCodAlmacen.Text
        End If
        If CheckTodosUsuario.Checked = False And checkUnUsuario.Checked = True And tbUsuario.Text <> String.Empty Then
            idUsuario = tbCodigoUsuario.Text
        End If
        If ckTodosProveedor.Checked = False And ckUnoProveedor.Checked = True And tbCodigoProveedor.Text <> String.Empty Then
            idProveedor = tbCodigoProveedor.Text
        End If

        If swFiltroUsuarios.Value = True Then
            If swProducto.Value = True Then
                ventasAtendidas = L_BuscarVentasCajerasProveedoresProductos(fechaDesde, fechaHasta, idAlmacen, idUsuario, idProveedor)
            Else
                ventasAtendidas = L_BuscarVentasCajerasProveedores(fechaDesde, fechaHasta, idAlmacen, idUsuario, idProveedor)
            End If
        Else
            If swProducto.Value = True Then
                ventasAtendidas = L_BuscarVentasCajerasProveedoresProductosSinUsuario(fechaDesde, fechaHasta, idAlmacen, idProveedor)
            Else
                ventasAtendidas = L_BuscarVentasCajerasProveedoresSinUsuario(fechaDesde, fechaHasta, idAlmacen, idProveedor)
            End If
        End If


        Return ventasAtendidas

    End Function
    Private Sub _prCargarReporte()
        Dim _ventasAtendidas As New DataTable
        _ventasAtendidas = _prValidadrFiltros()
        If (_ventasAtendidas.Rows.Count > 0) Then
            If swFiltroUsuarios.Value = True Then
                If (swProducto.Value = True) Then
                    Dim objrep As New R_VentasCajeraProveedorProd
                    objrep.SetDataSource(_ventasAtendidas)
                    Dim fechaI As String = tbFechaI.Value.ToString("dd/MM/yyyy")
                    Dim fechaF As String = tbFechaF.Value.ToString("dd/MM/yyyy")
                    objrep.SetParameterValue("usuario", L_Usuario)
                    objrep.SetParameterValue("fechaI", fechaI)
                    objrep.SetParameterValue("fechaF", fechaF)
                    MReportViewer.ReportSource = objrep
                    MReportViewer.Show()
                    MReportViewer.BringToFront()
                Else
                    Dim objrep As New R_VentasCajeraProveedor
                    'Dim objrep As New R_VentasCajeraProveedorSinUsuario
                    objrep.SetDataSource(_ventasAtendidas)
                    Dim fechaI As String = tbFechaI.Value.ToString("dd/MM/yyyy")
                    Dim fechaF As String = tbFechaF.Value.ToString("dd/MM/yyyy")
                    objrep.SetParameterValue("usuario", L_Usuario)
                    objrep.SetParameterValue("fechaI", fechaI)
                    objrep.SetParameterValue("fechaF", fechaF)
                    MReportViewer.ReportSource = objrep
                    MReportViewer.Show()
                    MReportViewer.BringToFront()
                End If
            Else
                If (swProducto.Value = True) Then
                    Dim objrep As New R_VentasCajeraProveedorProdSinUsuario
                    objrep.SetDataSource(_ventasAtendidas)
                    Dim fechaI As String = tbFechaI.Value.ToString("dd/MM/yyyy")
                    Dim fechaF As String = tbFechaF.Value.ToString("dd/MM/yyyy")
                    objrep.SetParameterValue("usuario", L_Usuario)
                    objrep.SetParameterValue("fechaI", fechaI)
                    objrep.SetParameterValue("fechaF", fechaF)
                    MReportViewer.ReportSource = objrep
                    MReportViewer.Show()
                    MReportViewer.BringToFront()
                Else
                    Dim objrep As New R_VentasCajeraProveedorSinUsuario
                    objrep.SetDataSource(_ventasAtendidas)
                    Dim fechaI As String = tbFechaI.Value.ToString("dd/MM/yyyy")
                    Dim fechaF As String = tbFechaF.Value.ToString("dd/MM/yyyy")
                    objrep.SetParameterValue("usuario", L_Usuario)
                    objrep.SetParameterValue("fechaI", fechaI)
                    objrep.SetParameterValue("fechaF", fechaF)
                    MReportViewer.ReportSource = objrep
                    MReportViewer.Show()
                    MReportViewer.BringToFront()
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

    Private Sub Pr_VentasCajeras_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()
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

    Private Sub ckTodosCliente_CheckValueChanged(sender As Object, e As EventArgs) Handles ckTodosProveedor.CheckValueChanged
        If (ckTodosProveedor.Checked) Then
            ckUnoProveedor.CheckValue = False
            tbProveedor.Enabled = True
            tbProveedor.BackColor = Color.Gainsboro
            tbProveedor.Clear()
            tbCodigoProveedor.Clear()
        End If
    End Sub

    Private Sub ckUnoCliente_CheckedChanged(sender As Object, e As EventArgs) Handles ckUnoProveedor.CheckedChanged
        If (ckUnoProveedor.Checked) Then
            ckTodosProveedor.CheckValue = False
            tbProveedor.Enabled = True
            tbProveedor.BackColor = Color.White
            tbProveedor.Focus()
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

    Private Sub tbProveedor_KeyDown(sender As Object, e As KeyEventArgs) Handles tbProveedor.KeyDown
        If (ckUnoProveedor.Checked) Then
            If e.KeyData = Keys.Control + Keys.Enter Then

                Dim dt As DataTable
                dt = L_prLibreriaClienteLGeneral(1, 1)

                Dim listEstCeldas As New List(Of Modelo.Celda)
                listEstCeldas.Add(New Modelo.Celda("yccod3,", True, "CÓDIGO", 70))
                listEstCeldas.Add(New Modelo.Celda("ycdes3", True, "PROVEEDOR", 200))

                Dim ef = New Efecto
                ef.tipo = 3
                ef.dt = dt
                ef.SeleclCol = 2
                ef.listEstCeldas = listEstCeldas
                ef.alto = 50
                ef.ancho = 350
                ef.Context = "Seleccione Proveedor".ToUpper
                ef.ShowDialog()
                Dim bandera As Boolean = False
                bandera = ef.band
                If (bandera = True) Then
                    Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
                    tbCodigoProveedor.Text = Row.Cells("yccod3").Value
                    tbProveedor.Text = Row.Cells("ycdes3").Value
                End If
            End If
        End If
    End Sub

    Private Sub tbUsuario_KeyDown(sender As Object, e As KeyEventArgs) Handles tbUsuario.KeyDown
        If (checkUnUsuario.Checked) Then
            If e.KeyData = Keys.Control + Keys.Enter Then
                Dim dt As DataTable
                dt = L_ListarUsuarios()

                Dim listEstCeldas As New List(Of Modelo.Celda)
                listEstCeldas.Add(New Modelo.Celda("ydnumi,", True, "ID", 50))
                listEstCeldas.Add(New Modelo.Celda("yduser", True, "USUARIO", 180))
                listEstCeldas.Add(New Modelo.Celda("ydrol", False, "IDROL".ToUpper, 150))
                listEstCeldas.Add(New Modelo.Celda("yd_numiVend", False, "IDVENDEDOR", 220))

                Dim ef = New Efecto
                ef.tipo = 3
                ef.dt = dt
                ef.SeleclCol = 1
                ef.listEstCeldas = listEstCeldas
                ef.alto = 50
                ef.ancho = 250
                ef.Context = "Seleccione Usuario".ToUpper
                ef.ShowDialog()
                Dim bandera As Boolean = False
                bandera = ef.band
                If (bandera = True) Then
                    Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
                    If (IsNothing(Row)) Then
                        tbUsuario.Focus()
                        Return
                    End If
                    tbCodigoUsuario.Text = Row.Cells("ydnumi").Value
                    tbUsuario.Text = Row.Cells("yduser").Value
                    btnGenerar.Focus()
                End If

            End If

        End If
    End Sub

    Private Sub checkUnUsuario_CheckValueChanged(sender As Object, e As EventArgs) Handles checkUnUsuario.CheckValueChanged
        If (checkUnUsuario.Checked) Then
            CheckTodosUsuario.CheckValue = False
            tbUsuario.Enabled = True
            tbUsuario.BackColor = Color.White
            tbUsuario.Focus()
        End If
    End Sub

    Private Sub CheckTodosUsuario_CheckValueChanged(sender As Object, e As EventArgs) Handles CheckTodosUsuario.CheckValueChanged
        If (CheckTodosUsuario.Checked) Then
            checkUnUsuario.CheckValue = False
            tbUsuario.Enabled = True
            tbUsuario.BackColor = Color.Gainsboro
            tbUsuario.Clear()
            tbCodigoUsuario.Clear()
        End If
    End Sub

    Private Sub swFiltroUsuarios_ValueChanged(sender As Object, e As EventArgs) Handles swFiltroUsuarios.ValueChanged
        If swFiltroUsuarios.Value = False Then
            checkUnUsuario.Enabled = False
            CheckTodosUsuario.Enabled = False
        Else
            checkUnUsuario.Enabled = True
            CheckTodosUsuario.Enabled = True
        End If
    End Sub
End Class