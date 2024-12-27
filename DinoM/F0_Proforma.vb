Imports Logica.AccesoLogica
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
Imports System.Threading
Imports System.Drawing.Text
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Drawing.Printing
Imports CrystalDecisions.Shared
Imports Facturacion

Public Class F0_Proforma
    Dim _Inter As Integer = 0
#Region "Variables Globales"
    Dim _CodCliente As Integer = 0
    Dim _CodEmpleado As Integer = 0
    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
    Dim FilaSelectLote As DataRow = Nothing
    Dim Lote As Boolean = False '1=igual a mostrar las columnas de lote y fecha de Vencimiento
    Dim dtDescuentos As DataTable = Nothing
    Dim Table_Producto As DataTable

    Dim dtname As DataTable
#End Region

#Region "Métodos Privados"
    Private Sub _IniciarTodo()
        '' L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        MSuperTabControl.SelectedTabIndex = 0
        'Me.WindowState = FormWindowState.Maximized


        _prValidarLote()
        _prCargarComboLibreriaSucursal(cbSucursal)
        lbTipoMoneda.Visible = False
        swMoneda.Visible = False

        _prCargarProforma()
        _prInhabiliitar()
        grVentas.Focus()
        Me.Text = "PROFORMA"
        Dim blah As New Bitmap(New Bitmap(My.Resources.ic_p), 20, 20)
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico
        _prAsignarPermisos()

        dtname = L_fnNameLabel()
    End Sub
    Public Sub _prValidarLote()
        Dim dt As DataTable = L_fnPorcUtilidad()
        If (dt.Rows.Count > 0) Then
            Dim lot As Integer = dt.Rows(0).Item("VerLote")
            If (lot = 1) Then
                Lote = True
            Else
                Lote = False
            End If
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
    Private Sub _prAsignarPermisos()

        Dim dtRolUsu As DataTable = L_prRolDetalleGeneral(gi_userRol, _nameButton)

        Dim show As Boolean = dtRolUsu.Rows(0).Item("ycshow")
        Dim add As Boolean = dtRolUsu.Rows(0).Item("ycadd")
        Dim modif As Boolean = dtRolUsu.Rows(0).Item("ycmod")
        Dim del As Boolean = dtRolUsu.Rows(0).Item("ycdel")

        If add = False Then
            btnNuevo.Visible = False
        End If
        If modif = False Then
            btnModificar.Visible = False
        End If
        If del = False Then
            btnEliminar.Visible = False
        End If
    End Sub
    Private Sub _prInhabiliitar()
        tbCodigo.ReadOnly = True
        tbCliente.ReadOnly = True
        tbVendedor.ReadOnly = True
        tbObservacion.ReadOnly = True
        tbFechaVenta.IsInputReadOnly = True
        swMoneda.IsReadOnly = True

        btnModificar.Enabled = True
        btnGrabar.Enabled = False
        btnNuevo.Enabled = True
        btnEliminar.Enabled = True

        tbSubTotal.IsInputReadOnly = True
        tbIce.IsInputReadOnly = True
        tbtotal.IsInputReadOnly = True

        grVentas.Enabled = True
        PanelNavegacion.Enabled = True
        grdetalle.RootTable.Columns("img").Visible = False
        If (GPanelProductos.Visible = True) Then
            _DesHabilitarProductos()
        End If
        cbSucursal.ReadOnly = True
        FilaSelectLote = Nothing
    End Sub
    Private Sub _prhabilitar()
        grVentas.Enabled = False
        tbCodigo.ReadOnly = False
        ''  tbCliente.ReadOnly = False  por que solo podra seleccionar Cliente
        ''  tbVendedor.ReadOnly = False
        tbObservacion.ReadOnly = False
        tbFechaVenta.IsInputReadOnly = False
        swMoneda.IsReadOnly = False
        btnGrabar.Enabled = True

        If (tbCodigo.Text.Length > 0) Then
            cbSucursal.ReadOnly = True
        Else
            cbSucursal.ReadOnly = False
        End If
    End Sub
    Public Sub _prFiltrar()
        'cargo el buscador
        Dim _Mpos As Integer
        _prCargarProforma()
        If grVentas.RowCount > 0 Then
            _Mpos = 0
            grVentas.Row = _Mpos
        Else
            _Limpiar()
            LblPaginacion.Text = "0/0"
        End If
    End Sub
    Private Sub _Limpiar()
        tbCodigo.Clear()
        tbCliente.Clear()
        tbVendedor.Clear()
        TbNomCliente.Clear()
        tbContacto.Clear()
        tbTelf.Clear()
        tbObservacion.Clear()
        tbObservacion.Text = "ALGUNOS PRODUCTOS PUEDEN NO ESTAR DISPONIBLES. EL PRECIO PUEDE VARIAR SEGÚN STOCK."
        swMoneda.Value = True
        _CodCliente = 0
        _CodEmpleado = 0
        tbFechaVenta.Value = Now.Date
        _prCargarDetalleVenta(-1)
        MSuperTabControl.SelectedTabIndex = 0
        tbSubTotal.Value = 0
        tbPdesc.Value = 0
        tbMdesc.Value = 0
        tbIce.Value = 0
        tbtotal.Value = 0
        With grdetalle.RootTable.Columns("img")
            .Width = 80
            .Caption = "Eliminar".ToUpper
            .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
            .Visible = True
        End With
        _prAddDetalleVenta()
        If (GPanelProductos.Visible = True) Then
            GPanelProductos.Visible = False
            PanelTotal.Visible = True
            PanelInferior.Visible = True
        End If
        TbNomCliente.Focus()

        If (CType(cbSucursal.DataSource, DataTable).Rows.Count > 0) Then
            cbSucursal.SelectedIndex = 0
        End If

        FilaSelectLote = Nothing
        dtDescuentos = L_fnListarDescuentosTodos()
    End Sub
    Public Sub _prMostrarRegistro(_N As Integer)

        With grVentas
            cbSucursal.Value = .GetValue("paalm")
            tbCodigo.Text = .GetValue("panumi")
            tbFechaVenta.Value = .GetValue("pafdoc")
            _CodEmpleado = .GetValue("paven")
            tbVendedor.Text = .GetValue("vendedor")
            _CodCliente = .GetValue("paclpr")
            tbCliente.Text = .GetValue("cliente")
            swMoneda.Value = .GetValue("pamon")
            TbNomCliente.Text = .GetValue("pacliente")
            tbContacto.Text = .GetValue("pacontacto")
            tbTelf.Text = .GetValue("patelf")
            tbObservacion.Text = .GetValue("paobs")

            lbFecha.Text = CType(.GetValue("pafact"), Date).ToString("dd/MM/yyyy")
            lbHora.Text = .GetValue("pahact").ToString
            lbUsuario.Text = .GetValue("pauact").ToString
        End With

        _prCargarDetalleVenta(tbCodigo.Text)
        tbMdesc.Value = grVentas.GetValue("padesc")
        _prCalcularPrecioTotal()
        LblPaginacion.Text = Str(grVentas.Row + 1) + "/" + grVentas.RowCount.ToString

    End Sub

    Private Sub _prCargarDetalleVenta(_numi As String)
        Dim dt As New DataTable
        dt = L_fnDetalleProforma(_numi)
        grdetalle.DataSource = dt
        grdetalle.RetrieveStructure()
        grdetalle.AlternatingColors = True

        With grdetalle.RootTable.Columns("pbnumi")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("pbtp1numi")
            .Width = 90
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("pbty5prod")
            .Width = 80
            .Caption = "COD. DYNASYS"
            .Visible = True
        End With
        With grdetalle.RootTable.Columns("yfcprod")
            .Width = 80
            .Caption = "COD. DELTA"
            .Visible = True
        End With
        With grdetalle.RootTable.Columns("yfcbarra")
            .Width = 90
            .Caption = "COD. BARRAS"
            .Visible = True
        End With
        With grdetalle.RootTable.Columns("producto")
            .Caption = "PRODUCTOS"
            .Width = 400
            .Visible = True
            .MaxLines = 3
            .WordWrap = True
        End With
        With grdetalle.RootTable.Columns("pbest")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("pbcmin")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Cantidad".ToUpper
        End With
        With grdetalle.RootTable.Columns("pbumin")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("unidad")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .Caption = "Unidad".ToUpper
        End With
        With grdetalle.RootTable.Columns("pbpbas")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Precio U.".ToUpper
        End With
        With grdetalle.RootTable.Columns("pbptot")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Sub Total".ToUpper
        End With
        With grdetalle.RootTable.Columns("pbporc")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .FormatString = "0.00"
            .Caption = "P.Desc(%)".ToUpper
        End With
        With grdetalle.RootTable.Columns("pbdesc")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Descuento".ToUpper
        End With
        With grdetalle.RootTable.Columns("pbtotdesc")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Total".ToUpper
        End With
        With grdetalle.RootTable.Columns("pbfact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("pbhact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("pbuact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("estado")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("img")
            .Width = 80
            .Caption = "Eliminar".ToUpper
            .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("stock")
            .Width = 120
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("pbfamilia")
            .Visible = False
        End With
        With grdetalle
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007

            .RecordNavigator = True
            .RecordNavigatorText = "Productos"
        End With
    End Sub

    Private Sub _prCargarProforma()
        Dim dt As New DataTable
        dt = L_fnGeneralProforma(IIf(swMostrar.Value = True, 1, 0))
        grVentas.DataSource = dt
        grVentas.RetrieveStructure()
        grVentas.AlternatingColors = True

        With grVentas.RootTable.Columns("panumi")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = True
        End With
        With grVentas.RootTable.Columns("paalm")
            .Width = 90
            .Visible = False
        End With
        With grVentas.RootTable.Columns("pafdoc")
            .Width = 90
            .Visible = True
            .Caption = "FECHA"
        End With
        With grVentas.RootTable.Columns("paven")
            .Width = 160
            .Visible = False
        End With
        With grVentas.RootTable.Columns("vendedor")
            .Width = 250
            .Visible = False
            .Caption = "VENDEDOR".ToUpper
        End With
        With grVentas.RootTable.Columns("paclpr")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("cliente")
            .Width = 250
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
            .Caption = "CLIENTE"
        End With
        With grVentas.RootTable.Columns("pamon")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("moneda")
            .Width = 150
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
            .Caption = "MONEDA"
        End With
        With grVentas.RootTable.Columns("pacliente")
            .Width = 300
            .Visible = True
            .Caption = "CLIENTE"
        End With
        With grVentas.RootTable.Columns("pacontacto")
            .Width = 200
            .Visible = True
            .Caption = "CONTACTO"
        End With
        With grVentas.RootTable.Columns("patelf")
            .Width = 100
            .Visible = True
            .Caption = "TELÉFONO"
        End With
        With grVentas.RootTable.Columns("paobs")
            .Width = 300
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Caption = "OBSERVACION"
        End With
        With grVentas.RootTable.Columns("padesc")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("paest")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("pafact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("pahact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("pauact")
            .Width = 90
            .Caption = "USUARIO"
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
        End With
        With grVentas.RootTable.Columns("total")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .Caption = "TOTAL"
            .FormatString = "0.00"
        End With
        With grVentas
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla

        End With

        If (dt.Rows.Count <= 0) Then
            _prCargarDetalleVenta(-1)
        End If
    End Sub


    Public Sub actualizarSaldoSinLote(ByRef dt As DataTable)

        Dim _detalle As DataTable = CType(grdetalle.DataSource, DataTable)

        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
            Dim sum As Integer = 0
            Dim codProducto As Integer = dt.Rows(i).Item("yfnumi")
            For j As Integer = 0 To grdetalle.RowCount - 1 Step 1
                grdetalle.Row = j
                Dim estado As Integer = grdetalle.GetValue("estado")
                If (estado = 0) Then
                    If (codProducto = grdetalle.GetValue("pbty5prod")) Then
                        sum = sum + grdetalle.GetValue("pbcmin")
                    End If
                End If
            Next
            dt.Rows(i).Item("stock") = dt.Rows(i).Item("stock") - sum
        Next

    End Sub

    Private Sub _prCargarProductos(_cliente As String)
        If (cbSucursal.SelectedIndex < 0) Then
            Return
        End If
        'Dim dtname As DataTable = L_fnNameLabel()

        Dim dt1, dt As New DataTable
        'dt = L_fnListarProductosSinLoteProformaNuevo(cbSucursal.Value, _cliente)
        'dt = L_fnListarProductosSinLoteUltProforma(cbSucursal.Value, _cliente, CType(grdetalle.DataSource, DataTable))
        dt1 = L_fnListarProductosSinLoteUltimaProforma(cbSucursal.Value, _cliente)
        dt = dt1.Select("yhprecio>0").CopyToDataTable

        grProductos.DataSource = dt
        grProductos.RetrieveStructure()
        grProductos.AlternatingColors = True

        With grProductos.RootTable.Columns("yfnumi")
            .Width = 90
            .Caption = "COD.DYNASYS"
            .Visible = True
        End With
        With grProductos.RootTable.Columns("yfcprod")
            .Width = 90
            .Caption = "COD.DELTA"
            .Visible = True
        End With
        With grProductos.RootTable.Columns("yfcbarra")
            .Width = 120
            .Caption = "COD.BARRA"
            .Visible = True
        End With
        With grProductos.RootTable.Columns("yfcdprod1")
            .Width = 530
            .Visible = True
            .Caption = "DESCRIPCIÓN"
            .MaxLines = 3
            .WordWrap = True
        End With
        With grProductos.RootTable.Columns("yfcdprod2")
            .Width = 150
            .Visible = False
            .Caption = "Descripcion Corta"
        End With
        With grProductos.RootTable.Columns("yfvsup")
            .Width = 90
            .Visible = True
            .Caption = "Conversión".ToUpper
            .FormatString = "0.00"
        End With
        With grProductos.RootTable.Columns("yfgr1")
            .Width = 160
            .Visible = False
        End With
        With grProductos.RootTable.Columns("yfgr5")
            .Visible = False
        End With
        With grProductos.RootTable.Columns("grupo5")
            .Visible = False
        End With
        If (dtname.Rows.Count > 0) Then
            With grProductos.RootTable.Columns("grupo1")
                .Width = 150
                .Caption = dtname.Rows(0).Item("Grupo 1").ToString
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = True
            End With
            With grProductos.RootTable.Columns("grupo2")
                .Width = 120
                .Caption = dtname.Rows(0).Item("Grupo 2").ToString
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = False
            End With
            With grProductos.RootTable.Columns("grupo3")
                .Width = 120
                .Caption = dtname.Rows(0).Item("Grupo 3").ToString
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = False
            End With
            With grProductos.RootTable.Columns("grupo4")
                .Width = 120
                .Caption = dtname.Rows(0).Item("Grupo 4").ToString
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = False
            End With
        Else
            With grProductos.RootTable.Columns("grupo1")
                .Width = 120
                .Caption = "Grupo 1"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = True
            End With
            With grProductos.RootTable.Columns("grupo2")
                .Width = 120
                .Caption = "Grupo 2"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = True
            End With
            With grProductos.RootTable.Columns("grupo3")
                .Width = 120
                .Caption = "Grupo 3"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = True
            End With
            With grProductos.RootTable.Columns("grupo4")
                .Width = 120
                .Caption = "Grupo 4"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = True
            End With
        End If
        With grProductos.RootTable.Columns("yfgr2")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grProductos.RootTable.Columns("yfgr3")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grProductos.RootTable.Columns("yfgr4")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grProductos.RootTable.Columns("yfumin")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grProductos.RootTable.Columns("UnidMin")
            .Width = 120
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
            .Caption = "Unidad Min."
        End With
        With grProductos.RootTable.Columns("yhprecio")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Caption = "Precio".ToUpper
            .FormatString = "0.00"
        End With
        With grProductos.RootTable.Columns("pcos")
            .Width = 120
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
            .Caption = "Precio Costo"
            .FormatString = "0.00"
        End With
        With grProductos.RootTable.Columns("stock")
            .Width = 100
            .FormatString = "0.00"
            .Visible = True
            .Caption = "Stock".ToUpper
        End With
        With grProductos.RootTable.Columns("validacion")
            .Visible = False
        End With
        With grProductos.RootTable.Columns("DescuentoId")
            .Visible = False
        End With
        With grProductos.RootTable.Columns("grupoDesc")
            .Visible = False
        End With
        With grProductos.RootTable.Columns("ygcodsin")
            .Visible = False
        End With
        With grProductos.RootTable.Columns("ygcodu")
            .Visible = False
        End With
        With grProductos
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007

            .RecordNavigator = True
            .RecordNavigatorText = "Productos"
        End With
        _prAplicarCondiccionJanusSinLote()
    End Sub
    Public Sub _prAplicarCondiccionJanusSinLote()
        Dim fc As GridEXFormatCondition
        fc = New GridEXFormatCondition(grProductos.RootTable.Columns("stock"), ConditionOperator.Equal, 0)
        'fc.FormatStyle.FontBold = TriState.True
        fc.FormatStyle.ForeColor = Color.Red
        grProductos.RootTable.FormatConditions.Add(fc)
    End Sub


    Private Sub _prAddDetalleVenta()
        Dim Bin As New MemoryStream
        Dim img As New Bitmap(My.Resources.delete, 28, 28)
        img.Save(Bin, Imaging.ImageFormat.Png)
        CType(grdetalle.DataSource, DataTable).Rows.Add(_fnSiguienteNumi() + 1, 0, 0, "", 0, "", 0, 0, 0, "", 0, 0, 0, 0, 0, Now.Date, "", "", 0, Bin.GetBuffer, 0, 0)
    End Sub

    Public Function _fnSiguienteNumi()
        Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)
        Dim rows() As DataRow = dt.Select("pbnumi=MAX(pbnumi)")
        If (rows.Count > 0) Then
            Return rows(rows.Count - 1).Item("pbnumi")
        End If
        Return 1
    End Function
    Public Function _fnAccesible()
        Return tbFechaVenta.IsInputReadOnly = False
    End Function
    Private Sub _HabilitarProductos()
        GPanelProductos.Visible = True
        PanelTotal.Visible = False
        PanelInferior.Visible = False
        _prCargarProductos(Str(_CodCliente))
        grProductos.Focus()
        grProductos.MoveTo(grProductos.FilterRow)
        grProductos.Col = 2
    End Sub
    Private Sub _DesHabilitarProductos()
        GPanelProductos.Visible = False
        PanelTotal.Visible = True
        PanelInferior.Visible = True

        grdetalle.Select()
        grdetalle.Col = 5
        grdetalle.Row = grdetalle.RowCount - 1
    End Sub
    Public Sub _fnObtenerFilaDetalle(ByRef pos As Integer, numi As Integer)
        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _numi As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("pbnumi")
            If (_numi = numi) Then
                pos = i
                Return
            End If
        Next
    End Sub

    Public Function _fnExisteProducto(idprod As Integer) As Boolean
        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _idprod As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("pbty5prod")
            Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("estado")
            If (_idprod = idprod And estado >= 0) Then

                Return True
            End If
        Next
        Return False
    End Function


    Public Sub P_PonerTotal(rowIndex As Integer)
        If (rowIndex < grdetalle.RowCount) Then

            Dim lin As Integer = grdetalle.GetValue("pbnumi")
            Dim pos As Integer = -1
            _fnObtenerFilaDetalle(pos, lin)
            Dim cant As Double = grdetalle.GetValue("pbcmin")
            Dim uni As Double = grdetalle.GetValue("pbpbas")
            Dim MontoDesc As Double = grdetalle.GetValue("pbdesc")
            Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)
            If (pos >= 0) Then
                Dim TotalUnitario As Double = cant * uni

                'grDetalle.SetValue("lcmdes", montodesc)

                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbptot") = TotalUnitario
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbtotdesc") = TotalUnitario - MontoDesc
                grdetalle.SetValue("pbptot", TotalUnitario)
                grdetalle.SetValue("pbtotdesc", TotalUnitario - MontoDesc)

                Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado")
                If (estado = 1) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = 2
                End If
            End If
            _prCalcularPrecioTotal()
        End If

    End Sub
    Public Sub _prCalcularPrecioTotal()
        'Dim montodesc As Double = tbMdesc.Value
        'Dim pordesc As Double = ((montodesc * 100) / grdetalle.GetTotal(grdetalle.RootTable.Columns("pbtotdesc"), AggregateFunction.Sum))
        'tbPdesc.Value = pordesc
        'tbSubTotal.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("pbtotdesc"), AggregateFunction.Sum)

        'tbtotal.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("pbtotdesc"), AggregateFunction.Sum) - montodesc

        Dim TotalDescuento As Double = 0
        Dim Subtotal As Double = 0
        Dim Descuento As Double = 0

        Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)
        For i As Integer = 0 To dt.Rows.Count - 1 Step 1

            If (dt.Rows(i).Item("estado") >= 0) Then
                Subtotal = Subtotal + dt.Rows(i).Item("pbptot")
                TotalDescuento = TotalDescuento + dt.Rows(i).Item("pbtotdesc")
                Descuento += dt.Rows(i).Item("pbdesc")
            End If
        Next

        'grdetalle.UpdateData()
        Dim montoDo As Decimal
        Dim montodesc As Double = 0
        Dim pordesc As Double = ((montodesc * 100) / TotalDescuento)
        tbSubTotal.Value = Subtotal
        tbtotal.Value = TotalDescuento
        tbMdesc.Value = Descuento

    End Sub
    Public Sub _prEliminarFila()
        If (grdetalle.Row >= 0) Then
            If (grdetalle.RowCount >= 2) Then
                Dim estado As Integer = grdetalle.GetValue("estado")
                Dim pos As Integer = -1
                Dim lin As Integer = grdetalle.GetValue("pbnumi")
                _fnObtenerFilaDetalle(pos, lin)
                If (estado = 0) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = -2

                End If
                If (estado >= 1) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = -1
                End If
                grdetalle.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grdetalle.RootTable.Columns("estado"), Janus.Windows.GridEX.ConditionOperator.GreaterThanOrEqualTo, 0))

                If CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbfamilia") <> 1 Then
                    CalcularDescuentosCuandoSeEliminaProductoFamilia(pos)
                End If

                _prCalcularPrecioTotal()
                grdetalle.Select()
                grdetalle.Col = 5
                grdetalle.Row = grdetalle.RowCount - 1
            End If
        End If
    End Sub
    Public Function _ValidarCampos() As Boolean
        If (_CodCliente <= 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Seleccione un Cliente con Ctrl+Enter".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            tbCliente.Focus()
            Return False
        End If
        If (_CodEmpleado <= 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Seleccione un Vendedor con Ctrl+Enter".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            tbVendedor.Focus()
            Return False
        End If
        If (cbSucursal.SelectedIndex < 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Seleccione una Sucursal".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            cbSucursal.Focus()
            Return False
        End If
        If tbFechaVenta.Value > Now.Date Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "La fecha de proforma no puede ser mayor a la fecha actual".ToUpper, img, 2800, eToastGlowColor.Red, eToastPosition.BottomCenter)
            tbFechaVenta.Focus()
            Return False
        End If
        If (TbNomCliente.Text = String.Empty) Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor ingrese nombre del cliente".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            tbCliente.Focus()
            Return False
        End If
        If (tbContacto.Text = String.Empty) Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor ingrese nombre de contacto".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            tbCliente.Focus()
            Return False
        End If
        If (tbTelf.Text = String.Empty) Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor ingrese teléfono de contacto".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            tbCliente.Focus()
            Return False
        End If
        If (grdetalle.RowCount = 1) Then
            grdetalle.Row = grdetalle.RowCount - 1
            If (grdetalle.GetValue("pbty5prod") = 0) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "Por Favor introduzca productos al detalle".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                Return False
            End If
        End If
        Return True
    End Function

    Public Sub _GuardarNuevo()
        Dim numi As String = ""
        Dim res As Boolean = L_fnGrabarProforma(numi, tbFechaVenta.Value.ToString("yyyy/MM/dd"), _CodEmpleado, _CodCliente,
                                                IIf(swMoneda.Value = True, 1, 0), tbObservacion.Text.Trim.ToUpper, tbMdesc.Value, tbtotal.Value,
                                                CType(grdetalle.DataSource, DataTable), cbSucursal.Value, TbNomCliente.Text.Trim.ToUpper,
                                                tbContacto.Text.Trim.ToUpper, tbTelf.Text.Trim, gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina)

        If res Then
            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "Código de Proforma ".ToUpper + tbCodigo.Text + " Grabada con éxito.".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter)
            tbCodigo.Text = numi
            _prImprimirReporte()
            '_prExportarExcelProforma()
            _prCargarProforma()
            _Limpiar()
        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "La Proforma no pudo ser insertada".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        End If
    End Sub
    Private Sub _prGuardarModificado()
        Dim res As Boolean = L_fnModificarProforma(tbCodigo.Text, tbFechaVenta.Value.ToString("yyyy/MM/dd"), _CodEmpleado, _CodCliente,
                                                   IIf(swMoneda.Value = True, 1, 0), tbObservacion.Text.Trim.ToUpper, tbMdesc.Value, tbtotal.Value,
                                                   CType(grdetalle.DataSource, DataTable), cbSucursal.Value, TbNomCliente.Text.Trim.ToUpper,
                                                   tbContacto.Text.Trim.ToUpper, tbTelf.Text.Trim, gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina)
        If res Then

            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "Código de Proforma ".ToUpper + tbCodigo.Text + " Modificada con éxito.".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter
                                      )

            _prImprimirReporte()
            '_prExportarExcelProforma()
            _prCargarProforma()
            _prSalir()
        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "La Proforma no pudo ser Modificada".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        End If
    End Sub
    Private Sub _prSalir()
        If btnGrabar.Enabled = True Then
            _prInhabiliitar()
            If grVentas.RowCount > 0 Then
                _prMostrarRegistro(0)
            End If
        Else
            _modulo.Select()
            Me.Close()
        End If
    End Sub
    Public Sub _prCargarIconELiminar()
        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim Bin As New MemoryStream
            Dim img As New Bitmap(My.Resources.delete, 28, 28)
            img.Save(Bin, Imaging.ImageFormat.Png)
            CType(grdetalle.DataSource, DataTable).Rows(i).Item("img") = Bin.GetBuffer
            grdetalle.RootTable.Columns("img").Visible = True
        Next

    End Sub

    Private Sub RecalcularPrecios()
        dtDescuentos = L_fnListarDescuentosTodos()
        For i = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1
            Dim rowIndex As Integer = grdetalle.Row
            Dim codPro = CType(grdetalle.DataSource, DataTable).Rows(i).Item("pbty5prod")
            Dim dt As DataTable = L_fnListarUnProductoPrecioVenta(codPro.ToString)
            CType(grdetalle.DataSource, DataTable).Rows(i).Item("pbpbas") = dt.Rows(0).Item("PrecioV")
            CType(grdetalle.DataSource, DataTable).Rows(i).Item("pbptot") = CType(grdetalle.DataSource, DataTable).Rows(i).Item("pbpbas") * CType(grdetalle.DataSource, DataTable).Rows(i).Item("pbcmin")
            CalcularDescuentos(CType(grdetalle.DataSource, DataTable).Rows(i).Item("pbty5prod"), CType(grdetalle.DataSource, DataTable).Rows(i).Item("pbcmin"), CType(grdetalle.DataSource, DataTable).Rows(i).Item("pbpbas"), i)
            P_PonerTotal(rowIndex)
        Next

    End Sub

    Public Sub _PrimerRegistro()
        Dim _MPos As Integer
        If grVentas.RowCount > 0 Then
            _MPos = 0
            ''   _prMostrarRegistro(_MPos)
            grVentas.Row = _MPos
        End If
    End Sub
#End Region


#Region "Eventos Formulario"
    Private Sub F0_Ventas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _IniciarTodo()
    End Sub
    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        _Limpiar()
        _prhabilitar()
        AsignarCliente()

        'tbCliente.Text = "CLIENTES VARIOS"
        '_CodCliente = 2
        'tbVendedor.Text = "VENDEDOR1"
        '_CodEmpleado = 1

        btnNuevo.Enabled = False
        btnModificar.Enabled = False
        btnEliminar.Enabled = False
        btnGrabar.Enabled = True
        PanelNavegacion.Enabled = False

    End Sub
    Private Sub AsignarCliente()
        Try
            Dim dt As DataTable
            dt = L_fnListarClientes()
            If dt.Rows.Count > 0 Then
                Dim fila As DataRow() = dt.Select("ydnumi =MIN(ydnumi)")
                tbCliente.Text = fila(0).ItemArray(3)
                _CodCliente = fila(0).ItemArray(0)
                tbVendedor.Text = fila(0).ItemArray(9)
                _CodEmpleado = fila(0).ItemArray(8)
            End If

        Catch ex As Exception
            mostrarmensajeerror("Debe asignar un cliente")
        End Try

    End Sub
    Private Sub mostrarmensajeerror(mensaje As String)
        ToastNotification.Show(Me,
                               mensaje.ToUpper,
                               My.Resources.WARNING,
                               5000,
                               eToastGlowColor.Red,
                               eToastPosition.TopCenter)

    End Sub
    Private Sub mostrarmensajeok(mensaje As String)
        ToastNotification.Show(Me,
                               mensaje.ToUpper,
                               My.Resources.OK,
                               5000,
                               eToastGlowColor.Green,
                               eToastPosition.TopCenter)
    End Sub
    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _prSalir()
    End Sub

    Private Sub tbCliente_KeyDown(sender As Object, e As KeyEventArgs) Handles tbCliente.KeyDown
        If (_fnAccesible()) Then
            If e.KeyData = Keys.Control + Keys.Enter Then
                Dim dt As DataTable
                dt = L_fnListarClientes()

                Dim listEstCeldas As New List(Of Modelo.Celda)
                listEstCeldas.Add(New Modelo.Celda("ydnumi,", False, "ID", 50))
                listEstCeldas.Add(New Modelo.Celda("ydcod", True, "ID", 50))
                listEstCeldas.Add(New Modelo.Celda("ydrazonsocial", True, "RAZON SOCIAL", 180))
                listEstCeldas.Add(New Modelo.Celda("yddesc", True, "NOMBRE", 280))
                listEstCeldas.Add(New Modelo.Celda("yddctnum", True, "N. Documento".ToUpper, 150))
                listEstCeldas.Add(New Modelo.Celda("yddirec", True, "DIRECCION", 220))
                listEstCeldas.Add(New Modelo.Celda("ydtelf1", True, "Telefono".ToUpper, 200))
                listEstCeldas.Add(New Modelo.Celda("ydfnac", True, "F.Nacimiento".ToUpper, 150, "MM/dd,YYYY"))
                listEstCeldas.Add(New Modelo.Celda("ydnumivend,", False, "ID", 50))
                listEstCeldas.Add(New Modelo.Celda("vendedor,", False, "ID", 50))
                listEstCeldas.Add(New Modelo.Celda("yddias", False, "CRED", 50))
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

                    _CodCliente = Row.Cells("ydnumi").Value
                    tbCliente.Text = Row.Cells("ydrazonsocial").Value
                    '_dias = Row.Cells("yddias").Value

                    Dim numiVendedor As Integer = IIf(IsDBNull(Row.Cells("ydnumivend").Value), 0, Row.Cells("ydnumivend").Value)
                    If (numiVendedor > 0) Then
                        tbVendedor.Text = Row.Cells("vendedor").Value
                        _CodEmpleado = Row.Cells("ydnumivend").Value

                        grdetalle.Select()
                    Else
                        tbVendedor.Clear()
                        _CodEmpleado = 0
                        tbVendedor.Focus()

                    End If
                End If
            End If
        End If

    End Sub
    Private Sub tbVendedor_KeyDown(sender As Object, e As KeyEventArgs) Handles tbVendedor.KeyDown
        If (_fnAccesible()) Then
            If e.KeyData = Keys.Control + Keys.Enter Then

                Dim dt As DataTable
                dt = L_fnListarEmpleado()

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
                    _CodEmpleado = Row.Cells("ydnumi").Value
                    tbVendedor.Text = Row.Cells("yddesc").Value
                    tbObservacion.Focus()

                End If
            End If
        End If
    End Sub


    Private Sub grdetalle_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grdetalle.EditingCell
        If (_fnAccesible()) Then
            'Habilitar solo las columnas de Precio, %, Monto y Observación
            If (e.Column.Index = grdetalle.RootTable.Columns("pbcmin").Index Or
                e.Column.Index = grdetalle.RootTable.Columns("yfcbarra").Index) Then
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        Else
            e.Cancel = True
        End If

    End Sub

    Private Sub grdetalle_Enter(sender As Object, e As EventArgs) Handles grdetalle.Enter

        If (_fnAccesible()) Then
            If (_CodCliente <= 0) Then
                ToastNotification.Show(Me, "           Antes de Continuar Por favor Seleccione un Cliente!!             ", My.Resources.WARNING, 4000, eToastGlowColor.Red, eToastPosition.TopCenter)
                tbCliente.Focus()
                Return
            End If
            If (_CodEmpleado <= 0) Then
                ToastNotification.Show(Me, "           Antes de Continuar Por favor Seleccione un Vendedor!!             ", My.Resources.WARNING, 4000, eToastGlowColor.Red, eToastPosition.TopCenter)
                tbVendedor.Focus()
                Return
            End If

            grdetalle.Select()
            grdetalle.Col = 4
            grdetalle.Row = 0
        End If
    End Sub
    Private Sub grdetalle_KeyDown(sender As Object, e As KeyEventArgs) Handles grdetalle.KeyDown
        If (Not _fnAccesible()) Then
            Return
        End If
        If (e.KeyData = Keys.Enter) Then
            Dim f, c As Integer
            c = grdetalle.Col
            f = grdetalle.Row


            grProductos.Focus()
            grProductos.MoveTo(grProductos.FilterRow)
            grProductos.Col = 2

            'If (grdetalle.Col = grdetalle.RootTable.Columns("pbcmin").Index) Then
            '    If (grdetalle.GetValue("producto") <> String.Empty) Then
            '        _prAddDetalleVenta()
            '        _HabilitarProductos()
            '    Else
            '        ToastNotification.Show(Me, "Seleccione un Producto Por Favor", My.Resources.WARNING, 3000, eToastGlowColor.Red, eToastPosition.TopCenter)
            '    End If
            'End If
            If (grdetalle.Col = grdetalle.RootTable.Columns("producto").Index) Then
                If (grdetalle.GetValue("producto") <> String.Empty) Then
                    _prAddDetalleVenta()
                    _HabilitarProductos()
                Else
                    ToastNotification.Show(Me, "Seleccione un Producto Por Favor", My.Resources.WARNING, 3000, eToastGlowColor.Red, eToastPosition.TopCenter)
                End If
            End If
            If (grdetalle.Col = grdetalle.RootTable.Columns("yfcbarra").Index) Then
                If (grdetalle.GetValue("yfcbarra").ToString().Trim() <> String.Empty) Then
                    cargarProductos()
                    If (grdetalle.Row = grdetalle.RowCount - 1) Then
                        Dim codigoBarrasCompleto = grdetalle.GetValue("yfcbarra").ToString
                        Dim DosPrimerosDigitos As String = Mid(codigoBarrasCompleto, 1, 2)

                        If DosPrimerosDigitos = "20" Then
                            Dim codigoBarrasProducto As Integer
                            Dim PesoGramos As Decimal

                            ''CUANDO EL CODIGO DE BARRAS TENGA 7 DIGITOS EJEM: 2000001
                            codigoBarrasProducto = Mid(codigoBarrasCompleto, 1, 7)
                            If (existeProducto(codigoBarrasProducto)) Then
                                PesoGramos = Mid(codigoBarrasCompleto, 8, 5)
                                Dim PesoKg As Decimal = PesoGramos / 1000
                                Dim resultado As Boolean = False

                                If (Not verificarExistenciaUnica(codigoBarrasProducto)) Then
                                    ponerProducto3(codigoBarrasProducto, PesoKg, -1, False, resultado)
                                    If resultado Then
                                        _prAddDetalleVenta()
                                    End If
                                Else
                                    ponerProducto3(codigoBarrasProducto, PesoKg, 0, True, resultado)
                                End If
                            Else
                                grdetalle.DataChanged = False
                                ToastNotification.Show(Me, "El código de barra del producto no existe".ToUpper, My.Resources.WARNING, 3000, eToastGlowColor.Red, eToastPosition.TopCenter)
                            End If

                        Else

                            If (existeProducto(grdetalle.GetValue("yfcbarra").ToString)) Then
                                If (Not verificarExistenciaUnica(grdetalle.GetValue("yfcbarra").ToString)) Then
                                    Dim resultado As Boolean = False
                                    ponerProducto(grdetalle.GetValue("yfcbarra").ToString, resultado)
                                    If resultado Then
                                        _prAddDetalleVenta()
                                    End If
                                Else
                                    'If (grdetalle.GetValue("producto").ToString <> String.Empty) Then
                                    sumarCantidad(grdetalle.GetValue("yfcbarra").ToString)
                                    'Else
                                    '    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                                    '    ToastNotification.Show(Me, "El Producto: NO CUENTA CON STOCK DISPONIBLE", img, 5000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                                    '    FilaSelectLote = Nothing
                                    'End If
                                End If
                            Else
                                'grdetalle.SetValue("yfcbarra", "")
                                grdetalle.DataChanged = False
                                ToastNotification.Show(Me, "El código de barra del producto no existe, o no tiene precio, verifique!!!".ToUpper, My.Resources.WARNING, 3700, eToastGlowColor.Red, eToastPosition.TopCenter)
                            End If
                        End If
                    Else
                        grdetalle.DataChanged = False
                        grdetalle.Row = grdetalle.RowCount - 1
                        grdetalle.Col = grdetalle.RootTable.Columns("yfcbarra").Index
                        ToastNotification.Show(Me, "El cursor debe situarse en la ultima fila", My.Resources.WARNING, 3000, eToastGlowColor.Red, eToastPosition.TopCenter)
                    End If
                Else
                    ToastNotification.Show(Me, "El código de barra no puede quedar vacio", My.Resources.WARNING, 3000, eToastGlowColor.Red, eToastPosition.TopCenter)
                End If
            End If
salirIf:
        End If

        If (e.KeyData = Keys.Control + Keys.Enter And grdetalle.Row >= 0 And
            grdetalle.Col = grdetalle.RootTable.Columns("producto").Index) Then
            Dim indexfil As Integer = grdetalle.Row
            Dim indexcol As Integer = grdetalle.Col
            _HabilitarProductos()

        End If
        If (e.KeyData = Keys.Escape And grdetalle.Row >= 0) Then
            _prEliminarFila()
        End If

    End Sub

    Private Sub cargarProductos()
        Dim dt As DataTable
        dt = L_fnListarProductosSinLoteUltProforma(cbSucursal.Value, _CodCliente, CType(grdetalle.DataSource, DataTable))
        Table_Producto = dt.Copy
    End Sub
    Private Function existeProducto(codigo As String) As Boolean
        Return (Table_Producto.Select("yfcbarra='" + codigo.Trim() + "'", "").Count > 0)
    End Function
    Private Function verificarExistenciaUnica(codigo As String) As Boolean
        Dim cont As Integer = 0
        'grdetalle.DataChanged = True
        'CType(grdetalle.DataSource, DataTable).AcceptChanges()
        For Each fila As GridEXRow In grdetalle.GetRows()
            If (fila.Cells("yfcbarra").Value.ToString.Trim = codigo.Trim) Then
                cont += 1
            End If
        Next
        Return (cont >= 1)
    End Function

    Private Sub ponerProducto(codigo As String, ByRef resultado As Boolean)
        Try
            grdetalle.DataChanged = True
            CType(grdetalle.DataSource, DataTable).AcceptChanges()
            Dim fila As DataRow() = Table_Producto.Select("yfcbarra='" + codigo.Trim + "'", "")
            If (fila.Count > 0) Then
                'Si tiene stock > 0

                Dim pos As Integer = -1
                _fnObtenerFilaDetalle(pos, grdetalle.GetValue("pbnumi"))
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbty5prod") = fila(0).ItemArray(0)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("yfcprod") = fila(0).ItemArray(1)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("yfcbarra") = fila(0).ItemArray(2)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("producto") = fila(0).ItemArray(3)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbfamilia") = fila(0).ItemArray(14)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbumin") = fila(0).ItemArray(16)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("unidad") = fila(0).ItemArray(17)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbpbas") = fila(0).ItemArray(18)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbptot") = fila(0).ItemArray(18)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbtotdesc") = fila(0).ItemArray(18)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbcmin") = 1

                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("stock") = fila(0).ItemArray(20)

                CalcularDescuentos(CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbty5prod"), 1, CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbpbas"), pos)
                _prCalcularPrecioTotal()
                resultado = True

            End If
        Catch ex As Exception
            mostrarmensajeerror(ex.Message)
        End Try
    End Sub
    Private Sub ponerProducto3(codigo As String, Peso As Decimal, pos As Integer, Sumar As Boolean, ByRef resultado As Boolean)
        Try
            Dim cantidad, precio, total As Decimal
            grdetalle.Row = grdetalle.RowCount - 1
            Dim fila As DataRow() = Table_Producto.Select("yfcbarra='" + codigo.Trim + "'", "")
            Dim ProductoExistente As DataRow() = CType(grdetalle.DataSource, DataTable).Select("yfcbarra='" + codigo.Trim + "' and estado=0", "")

            If (fila.Count > 0) Then

                If Sumar = False Then
                    cantidad = Format(Peso, "0.00")
                    precio = fila(0).Item("yhprecio")
                    total = Format(cantidad * precio, "0.00")
                Else
                    cantidad = Format((ProductoExistente(0).Item("pbcmin") + Peso), "0.00")
                    precio = fila(0).Item("yhprecio")
                    total = Format(cantidad * precio, "0.00")
                    _fnObtenerFilaDetalle(pos, ProductoExistente(0).Item("pbnumi"))
                End If

                If pos = -1 Then
                    If (grdetalle.GetValue("pbty5prod") > 0) Then
                        _prAddDetalleVenta()
                        grdetalle.Row = grdetalle.RowCount - 1
                    End If
                    _fnObtenerFilaDetalle(pos, grdetalle.GetValue("pbnumi"))
                End If

                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbty5prod") = fila(0).Item("yfnumi")
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("yfcprod") = fila(0).Item("yfcprod")
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("yfcbarra") = fila(0).Item("yfcbarra")
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("producto") = fila(0).Item("yfcdprod1")
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbumin") = fila(0).Item("yfumin")
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("unidad") = fila(0).Item("UnidMin")
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbfamilia") = fila(0).Item("yfgr4")
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbpbas") = precio
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbptot") = total
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbtotdesc") = total
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbcmin") = cantidad
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("stock") = fila(0).Item("stock")

                grdetalle.SetValue("yfcbarra", fila(0).Item("yfcbarra"))
                CalcularDescuentos(CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbty5prod"), cantidad, precio, pos)
                _prCalcularPrecioTotal()
                resultado = True

                If Sumar = True Then
                    Dim posicion As Integer = CType(grdetalle.DataSource, DataTable).Rows.Count - 1
                    CType(grdetalle.DataSource, DataTable).Rows(posicion).Item("yfcbarra") = ""
                    grdetalle.SetValue("yfcbarra", "")
                End If

            End If
        Catch ex As Exception
            Throw New Exception
        End Try

    End Sub
    Private Sub sumarCantidad(codigo As String)
        Try
            Dim fila As DataRow() = CType(grdetalle.DataSource, DataTable).Select(" estado>=0 and yfcbarra='" + codigo.Trim + "'", "")
            If (fila.Count > 0) Then
                grdetalle.UpdateData()
                Dim pos1 As Integer = -1
                _fnObtenerFilaDetalle(pos1, fila(0).Item("pbnumi"))
                Dim cant As Integer = fila(0).Item("pbcmin") + 1

                If (cant > 0) Then

                    CType(grdetalle.DataSource, DataTable).Rows(pos1).Item("pbcmin") = cant
                    CType(grdetalle.DataSource, DataTable).Rows(pos1).Item("pbptot") = Format((CType(grdetalle.DataSource, DataTable).Rows(pos1).Item("pbpbas") * cant), "#.#0")
                    CType(grdetalle.DataSource, DataTable).Rows(pos1).Item("pbtotdesc") = Format((CType(grdetalle.DataSource, DataTable).Rows(pos1).Item("pbpbas") * cant), "#.#0")

                    CalcularDescuentos(CType(grdetalle.DataSource, DataTable).Rows(pos1).Item("pbty5prod"), cant, CType(grdetalle.DataSource, DataTable).Rows(pos1).Item("pbpbas"), pos1)
                    _prCalcularPrecioTotal()

                    CType(grdetalle.DataSource, DataTable).Rows(grdetalle.RowCount - 1).Item("yfcbarra") = ""
                    grdetalle.SetValue("yfcbarra", "")
                    grdetalle.SetValue("pbcmin", 0)
                    grdetalle.SetValue("pbptot", 0)
                    grdetalle.DataChanged = True
                    grdetalle.Refresh()

                Else
                    grdetalle.SetValue("yfcbarra", "")
                    grdetalle.SetValue("pbcmin", 0)
                    grdetalle.SetValue("pbptot", 0)
                    grdetalle.DataChanged = True
                    grdetalle.Refresh()
                End If
            End If
        Catch ex As Exception
            mostrarmensajeerror(ex.Message)
        End Try
    End Sub
    Public Sub InsertarProductosSinLote()
        Dim pos As Integer = -1
        grdetalle.Row = grdetalle.RowCount - 1
        _fnObtenerFilaDetalle(pos, grdetalle.GetValue("pbnumi"))
        Dim existe As Boolean = _fnExisteProducto(grProductos.GetValue("yfnumi"))
        If ((pos >= 0) And (Not existe)) Then
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbty5prod") = grProductos.GetValue("yfnumi")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("yfcprod") = grProductos.GetValue("yfcprod")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("yfcbarra") = grProductos.GetValue("yfcbarra")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("producto") = grProductos.GetValue("yfcdprod1")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbumin") = grProductos.GetValue("yfumin")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("unidad") = grProductos.GetValue("UnidMin")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbpbas") = grProductos.GetValue("yhprecio")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbptot") = grProductos.GetValue("yhprecio")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbtotdesc") = grProductos.GetValue("yhprecio")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbcmin") = 1
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("stock") = grProductos.GetValue("stock")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbfamilia") = grProductos.GetValue("yfgr4")

            CalcularDescuentos(CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbty5prod"), 1, CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbpbas"), pos)
            _prCalcularPrecioTotal()
            '_DesHabilitarProductos()

            _prAddDetalleVenta()

            grdetalle.Select()
            grdetalle.Col = 6
            grdetalle.Row = grdetalle.RowCount - 2

        Else
            If (existe) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "El producto ya existe en el detalle, modifique la cantidad".ToUpper, img, 3000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            End If
        End If
    End Sub

    Private Sub grProductos_KeyDown(sender As Object, e As KeyEventArgs) Handles grProductos.KeyDown
        If (Not _fnAccesible()) Then
            Return
        End If
        If (e.KeyData = Keys.Enter) Then
            Dim f, c As Integer
            c = grProductos.Col
            f = grProductos.Row
            If (f >= 0) Then
                InsertarProductosSinLote()
            End If
        End If
        If e.KeyData = Keys.Escape Then
            _DesHabilitarProductos()
            FilaSelectLote = Nothing
        End If
    End Sub
    Private Sub grdetalle_CellValueChanged(sender As Object, e As ColumnActionEventArgs) Handles grdetalle.CellValueChanged

        If (e.Column.Index = grdetalle.RootTable.Columns("pbcmin").Index) Or (e.Column.Index = grdetalle.RootTable.Columns("pbpbas").Index) Then
            If (Not IsNumeric(grdetalle.GetValue("pbcmin")) Or grdetalle.GetValue("pbcmin").ToString = String.Empty) Then

                'grDetalle.GetRow(rowIndex).Cells("cant").Value = 1
                '  grDetalle.CurrentRow.Cells.Item("cant").Value = 1
                Dim lin As Integer = grdetalle.GetValue("pbnumi")
                Dim pos As Integer = -1
                _fnObtenerFilaDetalle(pos, lin)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbcmin") = 1
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbptot") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbpbas")

                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbporc") = 0
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbdesc") = 0
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbtotdesc") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbpbas")
                'grdetalle.SetValue("pbcmin", 1)
                'grdetalle.SetValue("pbptot", grdetalle.GetValue("pbpbas"))
            Else
                If (grdetalle.GetValue("pbcmin") >= 0) Then
                    'grdetalle.Row = grdetalle.RowCount - 1
                    Dim rowIndex As Integer = grdetalle.Row
                    Dim porcdesc As Double = grdetalle.GetValue("pbporc")
                    Dim montodesc As Double = ((grdetalle.GetValue("pbpbas") * grdetalle.GetValue("pbcmin")) * (porcdesc / 100))
                    Dim lin As Integer = grdetalle.GetValue("pbnumi")
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, lin)
                    'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbdesc") = montodesc
                    'grdetalle.SetValue("pbdesc", montodesc)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbcmin") = grdetalle.GetValue("pbcmin")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbptot") = grdetalle.GetValue("pbpbas") * grdetalle.GetValue("pbcmin")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbtotdesc") = grdetalle.GetValue("pbpbas") * grdetalle.GetValue("pbcmin")
                    CalcularDescuentos(grdetalle.GetValue("pbty5prod"), grdetalle.GetValue("pbcmin"), grdetalle.GetValue("pbpbas"), pos)
                    P_PonerTotal(rowIndex)
                Else
                    Dim lin As Integer = grdetalle.GetValue("pbnumi")
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, lin)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbcmin") = 1
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbptot") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbpbas")
                    CalcularDescuentos(grdetalle.GetValue("pbty5prod"), grdetalle.GetValue("pbcmin"), grdetalle.GetValue("pbpbas"), pos)
                    _prCalcularPrecioTotal()
                    'grdetalle.SetValue("pbcmin", 1)
                    'grdetalle.SetValue("pbptot", grdetalle.GetValue("pbpbas"))

                End If
            End If
        End If

        '''''''''''''''''''''MONTO DE DESCUENTO '''''''''''''''''''''
        If (e.Column.Index = grdetalle.RootTable.Columns("pbdesc").Index) Then
            If (Not IsNumeric(grdetalle.GetValue("pbdesc")) Or grdetalle.GetValue("pbdesc").ToString = String.Empty) Then

                'grDetalle.GetRow(rowIndex).Cells("cant").Value = 1
                '  grDetalle.CurrentRow.Cells.Item("cant").Value = 1
                Dim lin As Integer = grdetalle.GetValue("pbnumi")
                Dim pos As Integer = -1
                _fnObtenerFilaDetalle(pos, lin)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbporc") = 0
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbdesc") = 0
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbtotdesc") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbptot")
                'grdetalle.SetValue("pbcmin", 1)
                'grdetalle.SetValue("pbptot", grdetalle.GetValue("pbpbas"))
            Else
                If (grdetalle.GetValue("pbdesc") > 0 And grdetalle.GetValue("pbdesc") <= grdetalle.GetValue("pbptot")) Then

                    Dim montodesc As Double = grdetalle.GetValue("pbdesc")
                    Dim pordesc As Double = ((montodesc * 100) / grdetalle.GetValue("pbptot"))

                    Dim lin As Integer = grdetalle.GetValue("pbnumi")
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, lin)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbdesc") = montodesc
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbporc") = pordesc

                    grdetalle.SetValue("pbporc", pordesc)
                    Dim rowIndex As Integer = grdetalle.Row
                    P_PonerTotal(rowIndex)

                Else
                    Dim lin As Integer = grdetalle.GetValue("pbnumi")
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, lin)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbporc") = 0
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbdesc") = 0
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbtotdesc") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbptot")
                    grdetalle.SetValue("pbporc", 0)
                    grdetalle.SetValue("pbdesc", 0)
                    grdetalle.SetValue("pbtotdesc", grdetalle.GetValue("pbptot"))
                    _prCalcularPrecioTotal()
                    'grdetalle.SetValue("pbcmin", 1)
                    'grdetalle.SetValue("pbptot", grdetalle.GetValue("pbpbas"))

                End If
            End If
        End If

    End Sub
    Public Sub CalcularDescuentos(ProductoId As Integer, Cantidad As Integer, Precio As Decimal, Posicion As Integer)
        Dim preciod, total1, total2, descuentof, cantf As Double

        Dim fila As DataRow() = dtDescuentos.Select("ProductoId=" + Str(ProductoId).ToString.Trim + "", "")

        'Cálculo de descuentos si es sin familia
        If CType(grdetalle.DataSource, DataTable).Rows(Posicion).Item("pbfamilia") = 1 Then

            For Each dr As DataRow In fila

                Dim CantidadInicial As Integer = dr.Item("CantidadInicial")
                Dim CantidadFinal As Integer = dr.Item("CantidadFinal")
                Dim PrecioDescuento As Decimal = dr.Item("Precio")

                If (Cantidad >= CantidadInicial And Cantidad <= CantidadFinal) Then

                    Dim SubTotalDescuento As Decimal = Format((Cantidad * PrecioDescuento), "#.#0")
                    Dim Descuento As Decimal = Format((Cantidad * Precio), "#.#0") - SubTotalDescuento
                    CType(grdetalle.DataSource, DataTable).Rows(Posicion).Item("pbdesc") = Descuento
                    'grdetalle.SetValue("pbdesc", Descuento)
                    CType(grdetalle.DataSource, DataTable).Rows(Posicion).Item("pbtotdesc") = Format((CType(grdetalle.DataSource, DataTable).Rows(Posicion).Item("pbpbas") * CType(grdetalle.DataSource, DataTable).Rows(Posicion).Item("pbcmin")) - Descuento, "#.#0")
                    'grdetalle.SetValue("pbtotdesc", ((CType(grdetalle.DataSource, DataTable).Rows(Posicion).Item("pbpbas") * CType(grdetalle.DataSource, DataTable).Rows(Posicion).Item("pbcmin")) - Descuento))
                    Return

                Else
                    CType(grdetalle.DataSource, DataTable).Rows(Posicion).Item("pbdesc") = 0
                    CType(grdetalle.DataSource, DataTable).Rows(Posicion).Item("pbtotdesc") = Format((CType(grdetalle.DataSource, DataTable).Rows(Posicion).Item("pbpbas") * CType(grdetalle.DataSource, DataTable).Rows(Posicion).Item("pbcmin")), "#.#0")

                End If

            Next
        Else
            'Dim tabla As DataTable = CType(grdetalle.DataSource, DataTable).Select("estado>=0", "").CopyToDataTable
            'grdetalle.DataSource = tabla
            For i = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1
                'Cálculo de descuentos por familia
                Dim familia = CType(grdetalle.DataSource, DataTable).Rows(i).Item("pbfamilia")
                Dim cantnormal = CType(grdetalle.DataSource, DataTable).Rows(i).Item("pbcmin")
                Dim Estado = CType(grdetalle.DataSource, DataTable).Rows(i).Item("estado")
                Dim CodProd = CType(grdetalle.DataSource, DataTable).Rows(i).Item("pbty5prod")
                total1 = CType(grdetalle.DataSource, DataTable).Rows(i).Item("pbptot")

                If Estado >= 0 And familia <> 1 Then
                    'Recorre el grid para hacer la suma de las cantidades por familia

                    For Each flia In grdetalle.GetRows
                        If familia = flia.Cells("pbfamilia").Value Then
                            cantf += flia.Cells("pbcmin").Value
                        End If
                    Next
                    'For j = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1
                    '    If familia = CType(grdetalle.DataSource, DataTable).Rows(j).Item("pbfamilia") Then
                    '        cantf += CType(grdetalle.DataSource, DataTable).Rows(j).Item("pbcmin")
                    '    End If
                    'Next


                    'Consulta la tabla de descuentos para ver cual aplicará segun la cantidad ingresada
                    fila = dtDescuentos.Select("ProductoId=" + CodProd.ToString + "", "")

                    For Each preciodesc As DataRow In fila
                        If cantf >= preciodesc.Item("CantidadInicial") And cantf <= preciodesc.Item("CantidadFinal") Then
                            preciod = preciodesc.Item("Precio")
                            total2 = Format(cantnormal * preciod, "#.#0")
                        End If
                    Next
                    If total2 > 0 Then
                        descuentof = total1 - total2
                    Else
                        descuentof = 0
                    End If
                    CType(grdetalle.DataSource, DataTable).Rows(i).Item("pbdesc") = descuentof
                    CType(grdetalle.DataSource, DataTable).Rows(i).Item("pbtotdesc") = total1 - descuentof
                    cantf = 0
                    total2 = 0
                    descuentof = 0
                Else

                End If
            Next
        End If
    End Sub

    Public Sub CalcularDescuentosCuandoSeEliminaProductoFamilia(Posicion As Integer)
        Dim preciod, total1, total2, descuentof, cantf As Double

        Dim fila As DataRow()

        'Cálculo de descuentos si es sin familia
        If CType(grdetalle.DataSource, DataTable).Rows(Posicion).Item("pbfamilia") <> 1 Then
            'Dim tabla As DataTable = CType(grdetalle.DataSource, DataTable).Select("estado>=0", "").CopyToDataTable
            'grdetalle.DataSource = tabla
            For i = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1
                'Cálculo de descuentos por familia
                Dim familia = CType(grdetalle.DataSource, DataTable).Rows(i).Item("pbfamilia")
                Dim cantnormal = CType(grdetalle.DataSource, DataTable).Rows(i).Item("pbcmin")
                Dim Estado = CType(grdetalle.DataSource, DataTable).Rows(i).Item("estado")
                Dim CodProd = CType(grdetalle.DataSource, DataTable).Rows(i).Item("pbty5prod")
                total1 = CType(grdetalle.DataSource, DataTable).Rows(i).Item("pbptot")

                If Estado >= 0 And familia <> 1 Then
                    'Recorre el grid para hacer la suma de las cantidades por familia
                    'For Each flia In grdetalle.GetRows
                    '    If familia = flia.Cells("pbfamilia").Value Then
                    '        cantf += flia.Cells("pbcmin").Value
                    '    End If
                    'Next

                    'For j = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1
                    '    If familia = CType(grdetalle.DataSource, DataTable).Rows(j).Item("pbfamilia") Then
                    '        cantf += CType(grdetalle.DataSource, DataTable).Rows(j).Item("pbcmin")
                    '    End If
                    'Next
                    For Each flia In grdetalle.GetRows
                        If familia = flia.Cells("pbfamilia").Value Then
                            cantf += flia.Cells("pbcmin").Value
                        End If
                    Next

                    fila = dtDescuentos.Select("ProductoId=" + CodProd.ToString + "", "")
                    'Consulta la tabla de descuentos para ver cual aplicará segun la cantidad ingresada
                    For Each preciodesc As DataRow In fila
                        If cantf >= preciodesc.Item("CantidadInicial") And cantf <= preciodesc.Item("CantidadFinal") Then
                            preciod = preciodesc.Item("Precio")
                            total2 = cantnormal * preciod
                        End If
                    Next
                    If total2 > 0 Then
                        descuentof = total1 - total2
                    Else
                        descuentof = 0
                    End If
                    CType(grdetalle.DataSource, DataTable).Rows(i).Item("pbdesc") = descuentof
                    CType(grdetalle.DataSource, DataTable).Rows(i).Item("pbtotdesc") = total1 - descuentof
                    cantf = 0
                    total2 = 0
                    descuentof = 0
                Else

                End If
            Next
        End If

    End Sub
    Private Sub tbPdesc_ValueChanged(sender As Object, e As EventArgs) Handles tbPdesc.ValueChanged
        If (tbPdesc.Focused) Then
            If (Not tbPdesc.Text = String.Empty And Not tbtotal.Text = String.Empty) Then
                If (tbPdesc.Value = 0 Or tbPdesc.Value > 100) Then
                    tbPdesc.Value = 0
                    tbMdesc.Value = 0

                    _prCalcularPrecioTotal()

                Else

                    Dim porcdesc As Double = tbPdesc.Value
                    Dim montodesc As Double = (grdetalle.GetTotal(grdetalle.RootTable.Columns("pbptot"), AggregateFunction.Sum) * (porcdesc / 100))
                    tbMdesc.Value = montodesc

                    tbtotal.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("pbptot"), AggregateFunction.Sum) - montodesc
                End If
            End If
            If (tbPdesc.Text = String.Empty) Then
                tbMdesc.Value = 0
            End If
        End If
    End Sub

    Private Sub tbMdesc_ValueChanged(sender As Object, e As EventArgs) Handles tbMdesc.ValueChanged
        If (tbMdesc.Focused) Then

            Dim total As Double = tbtotal.Value
            If (Not tbMdesc.Text = String.Empty And Not tbMdesc.Text = String.Empty) Then
                If (tbMdesc.Value = 0 Or tbMdesc.Value > total) Then
                    tbMdesc.Value = 0
                    tbPdesc.Value = 0
                    _prCalcularPrecioTotal()
                Else
                    Dim montodesc As Double = tbMdesc.Value
                    Dim pordesc As Double = ((montodesc * 100) / grdetalle.GetTotal(grdetalle.RootTable.Columns("pbptot"), AggregateFunction.Sum))
                    tbPdesc.Value = pordesc

                    tbtotal.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("tbptot"), AggregateFunction.Sum) - montodesc
                End If
            End If

            If (tbMdesc.Text = String.Empty) Then
                tbMdesc.Value = 0
            End If
        End If
    End Sub


    Private Sub grdetalle_CellEdited(sender As Object, e As ColumnActionEventArgs) Handles grdetalle.CellEdited
        'If (e.Column.Index = grdetalle.RootTable.Columns("pbcmin").Index) Then
        '    If (Not IsNumeric(grdetalle.GetValue("pbcmin")) Or grdetalle.GetValue("pbcmin").ToString = String.Empty) Then

        '        grdetalle.SetValue("pbcmin", 1)
        '        grdetalle.SetValue("pbptot", grdetalle.GetValue("pbpbas"))
        '        grdetalle.SetValue("pbporc", 0)
        '        grdetalle.SetValue("pbdesc", 0)
        '        grdetalle.SetValue("pbtotdesc", grdetalle.GetValue("pbpbas"))


        '    Else
        '        If (grdetalle.GetValue("pbcmin") > 0) Then

        '            Dim cant As Integer = grdetalle.GetValue("pbcmin")
        '            Dim stock As Integer = grdetalle.GetValue("stock")
        '        Else

        '            grdetalle.SetValue("pbcmin", 1)
        '            grdetalle.SetValue("pbptot", grdetalle.GetValue("pbpbas"))
        '            grdetalle.SetValue("pbporc", 0)
        '            grdetalle.SetValue("pbdesc", 0)
        '            grdetalle.SetValue("pbtotdesc", grdetalle.GetValue("pbpbas"))

        '        End If
        '    End If
        'End If
    End Sub
    Private Sub grdetalle_MouseClick(sender As Object, e As MouseEventArgs) Handles grdetalle.MouseClick
        If (Not _fnAccesible()) Then
            Return
        End If
        If (grdetalle.RowCount >= 2) Then
            If (grdetalle.CurrentColumn.Index = grdetalle.RootTable.Columns("img").Index) Then
                _prEliminarFila()
            End If
        End If


    End Sub

    Sub _prImprimirReporte()
        'Dim ef = New Efecto
        'ef.tipo = 2
        'ef.Context = "mensaje principal".ToUpper
        'ef.Header = "¿DESEA IMPRIMIR REPORTE DE LA PROFORMA INSERTADA?".ToUpper
        'ef.ShowDialog()
        'Dim bandera As Boolean = False
        'bandera = ef.band
        'If (bandera = True) Then
        '    P_GenerarReporte(True)
        'End If

        P_GenerarReporte(True)
    End Sub
    Sub _prExportarExcelProforma()
        If (Not Directory.Exists(gs_CarpetaRaiz + "\Proformas")) Then
            Directory.CreateDirectory(gs_CarpetaRaiz + "\Proformas")
        End If
        Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)

        If (P_ExportarExcelGlobal(gs_CarpetaRaiz + "\Proformas", grdetalle, "Proforma")) Then
            L_fnBotonImprimirExportar(gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina, 0, "PROFORMA", "PROFORMA")
            ToastNotification.Show(Me, "SE EXPORTÓ EL DETALLE DE LA PROFORMA DE FORMA ÉXITOSA..!!!",
                                       img, 3000,
                                       eToastGlowColor.Green,
                                       eToastPosition.BottomCenter)
        Else
            ToastNotification.Show(Me, "FALLÓ LA EXPORTACIÓN DEL DETALLE DE LA PROFORMA..!!!",
                                       My.Resources.WARNING, 3000,
                                       eToastGlowColor.Red,
                                       eToastPosition.BottomCenter)
        End If
    End Sub


    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click

        If _ValidarCampos() = False Then
            Exit Sub
        End If

        If (tbCodigo.Text = String.Empty) Then
            _GuardarNuevo()
        Else
            If (tbCodigo.Text <> String.Empty) Then
                _prGuardarModificado()
                ''    _prInhabiliitar()
            End If
        End If

    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        If (grVentas.RowCount > 0) Then
            _prhabilitar()
            btnNuevo.Enabled = False
            btnModificar.Enabled = False
            btnEliminar.Enabled = False
            btnGrabar.Enabled = True
            PanelNavegacion.Enabled = False
            _prCargarIconELiminar()

            tbFechaVenta.Value = Now.Date
            RecalcularPrecios()
        End If
    End Sub
    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click

        Dim ef = New Efecto
        ef.tipo = 2
        ef.Context = "¿esta seguro de eliminar el registro?".ToUpper
        ef.Header = "mensaje principal".ToUpper
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then
            Dim mensajeError As String = ""
            Dim res As Boolean = L_fnEliminarProforma(tbCodigo.Text, mensajeError, gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina)
            If res Then

                Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
                ToastNotification.Show(Me, "Código de Proforma ".ToUpper + tbCodigo.Text + " eliminada con éxito.".ToUpper,
                                          img, 2000,
                                          eToastGlowColor.Green,
                                          eToastPosition.TopCenter)
                _prFiltrar()
            Else
                Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                ToastNotification.Show(Me, mensajeError, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            End If
        End If

    End Sub

    Private Sub grVentas_SelectionChanged(sender As Object, e As EventArgs) Handles grVentas.SelectionChanged
        If (grVentas.RowCount >= 0 And grVentas.Row >= 0) Then
            _prMostrarRegistro(grVentas.Row)
        End If
    End Sub

    Private Sub btnSiguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click
        Dim _pos As Integer = grVentas.Row
        If _pos < grVentas.RowCount - 1 And _pos >= 0 Then
            _pos = grVentas.Row + 1
            '' _prMostrarRegistro(_pos)
            grVentas.Row = _pos
        End If
    End Sub

    Private Sub btnUltimo_Click(sender As Object, e As EventArgs) Handles btnUltimo.Click
        Dim _pos As Integer = grVentas.Row
        If grVentas.RowCount > 0 Then
            _pos = grVentas.RowCount - 1
            ''  _prMostrarRegistro(_pos)
            grVentas.Row = _pos
        End If
    End Sub

    Private Sub btnAnterior_Click(sender As Object, e As EventArgs) Handles btnAnterior.Click
        Dim _MPos As Integer = grVentas.Row
        If _MPos > 0 And grVentas.RowCount > 0 Then
            _MPos = _MPos - 1
            ''  _prMostrarRegistro(_MPos)
            grVentas.Row = _MPos
        End If
    End Sub

    Private Sub btnPrimero_Click(sender As Object, e As EventArgs) Handles btnPrimero.Click
        _PrimerRegistro()
    End Sub
    Private Sub grVentas_KeyDown(sender As Object, e As KeyEventArgs) Handles grVentas.KeyDown
        If e.KeyData = Keys.Enter Then
            MSuperTabControl.SelectedTabIndex = 0
            grdetalle.Focus()

        End If
    End Sub
#End Region

    Public Function P_fnImageToByteArray(ByVal imageIn As Image) As Byte()
        Dim ms As New System.IO.MemoryStream()
        imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
        Return ms.ToArray()
    End Function

    Private Sub swTipoVenta_KeyDown(sender As Object, e As KeyEventArgs)
        If (e.KeyData = Keys.Enter) Then
            grdetalle.Select()
            grdetalle.Col = 3
            grdetalle.Row = 0
            'grdetalle.Focus()
        End If
    End Sub

    Private Sub P_GenerarReporte(pdf As Boolean)
        Dim dt As DataTable = L_fnReporteProforma(tbCodigo.Text)

        'Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)
        'dt = dt.Select("estado=0").CopyToDataTable
        Dim _Ds2 = L_Reporte_Factura_Cia("2")

        If Not IsNothing(P_Global.Visualizador) Then
            P_Global.Visualizador.Close()
        End If

        P_Global.Visualizador = New Visualizador

        Dim objrep As New R_Proforma1
        objrep.SetDataSource(dt)

        objrep.SetParameterValue("ECasaMatriz", _Ds2.Tables(0).Rows(0).Item("scsuc").ToString)
        objrep.SetParameterValue("ECiudadPais", _Ds2.Tables(0).Rows(0).Item("scpai").ToString)
        objrep.SetParameterValue("EDuenho", _Ds2.Tables(0).Rows(0).Item("scnom").ToString)
        objrep.SetParameterValue("Direccionpr", _Ds2.Tables(0).Rows(0).Item("scdir").ToString)
        objrep.SetParameterValue("Fecha", tbFechaVenta.Value.ToString("dd/MM/yyyy"))
        objrep.SetParameterValue("nombreCliente", TbNomCliente.Text)
        objrep.SetParameterValue("NombreContacto", tbContacto.Text)
        objrep.SetParameterValue("TelfContacto", tbTelf.Text)
        objrep.SetParameterValue("Obs", tbObservacion.Text)
        objrep.SetParameterValue("ENombre", _Ds2.Tables(0).Rows(0).Item("scneg").ToString)
        objrep.SetParameterValue("codigo", tbCodigo.Text)
        objrep.SetParameterValue("Usuario", L_Usuario)

        P_Global.Visualizador.CrGeneral.ReportSource = objrep 'Comentar
        P_Global.Visualizador.ShowDialog() 'Comentar
        P_Global.Visualizador.BringToFront() 'Comentar

        If pdf Then
            'Copia de Proforma en PDF
            If (Not Directory.Exists(gs_CarpetaRaiz + "\Proformas")) Then
                Directory.CreateDirectory(gs_CarpetaRaiz + "\Proformas")
            End If
            objrep.ExportToDisk(ExportFormatType.PortableDocFormat, gs_CarpetaRaiz + "\Proformas\" + CStr(TbNomCliente.Text) + "_" + CStr(Now.Date.Day) +
                    "." + CStr(Now.Date.Month) + "." + CStr(Now.Date.Year) + "_" + CStr(Now.Hour) + "." + CStr(Now.Minute) + "." + CStr(Now.Second) + ".pdf")

        End If
    End Sub

    Private Sub cbSucursal_Leave(sender As Object, e As EventArgs) Handles cbSucursal.Leave
        grdetalle.Select()
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        If (Not _fnAccesible()) Then
            P_GenerarReporte(False)
            _prExportarExcelProforma()
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

    Private Sub swMostrar_ValueChanged(sender As Object, e As EventArgs) Handles swMostrar.ValueChanged
        _prCargarProforma()
    End Sub


End Class