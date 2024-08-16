Imports System.Drawing.Printing
Imports System.IO
Imports CrystalDecisions.Shared
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Imports Facturacion
Imports Janus.Windows.GridEX
Imports Logica.AccesoLogica
Imports UTILITIES

'importando librerias api conexion
Imports Newtonsoft.Json
Imports DinoM.LoginResp
Imports DinoM.ConnSiat
Imports DinoM.RespMetodosPago
Imports DinoM.EmisorResp
Imports DinoM.RespTipoDoc
Imports DinoM.NitResp
Imports Modelo.ModeloAyuda
Imports System.Xml


Public Class F0_Proforma1
    Dim _inter As Integer = 0
#Region "variables globales"
    Dim _codcliente As Integer = 0
    Dim _codempleado As Integer = 0
    Dim ocultarfact As Integer = 0
    Dim _codebar As Integer = 1
    Dim _dias As Integer = 0
    Public _namebutton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
    Dim filaselectlote As DataRow = Nothing
    Dim table_producto As DataTable
    Dim g_lote As Boolean = False '1=igual a mostrar las columnas de lote y fecha de vencimiento
    Public codigoficha As String

    Dim dtdescuentos As DataTable = Nothing
    Dim configuraciondescuentoesxcantidad As Boolean = True
    Public programa As String
    Dim descuentoxproveedorlist As DataTable = New DataTable
    Dim listadescuento As List(Of DescuentoXCategoriaDescuento) = New List(Of DescuentoXCategoriaDescuento)


    'token sifac
    Public tokenobtenido
    Public dtdetalle As DataTable
    Public dt As DataTable

    Public codproducto As String
    Public cantidad As Integer
    Public preciou As Double
    Public preciotot As Double
    Public nombreprod As String
    Public nrofact As Integer
    Public nrotarjeta As String


    Public _fecha As Date

    Public qrurl As String
    Public facturl As String
    Public segundaleyenda As String
    Public terceraleyenda As String
    Public cudf As String

#End Region
#Region "metodos privados"
    Private Sub _iniciartodo()
        'L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        MSuperTabControl.SelectedTabIndex = 0

        _prvalidarlote()
        _prcargarcombolibreriasucursal(cbSucursal)
        _prcargarcombolibreria(cbCambioDolar, 7, 1)
        cbCambioDolar.SelectedIndex = CType(cbCambioDolar.DataSource, DataTable).Rows.Count - 1

        swMoneda.Visible = False
        btnGrabar.Enabled = False
        _prcargarProforma()
        _prinhabiliitar()
        grVentas.Focus()
        Me.Text = "PROFORMA"
        Dim blah As New Bitmap(New Bitmap(My.Resources.Kardex2), 20, 20)
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico
        _prasignarpermisos()
        _prcargarnamelabel()

        descuentoxproveedorlist = ObtenerDescuentoPorProveedor()
        configuraciondescuentoesxcantidad = TipoDescuentoEsXCantidad()
        'SwDescuentoProveedor.Visible = IIf(configuraciondescuentoesxcantidad, False, True)
        programa = P_Principal.btVentProforma.Text
    End Sub

    Public Sub _prcargarnamelabel()
        Dim dt As DataTable = L_fnNameLabel()
        If (dt.Rows.Count > 0) Then
            _codebar = 1 'dt.rows(0).item("codebar")
        End If
    End Sub
    Private Sub _prcargarcombocanje(mcombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable

        dt.Columns.Add("cod")
        dt.Columns.Add("desc")
        dt.Rows.Add(1, "si")
        dt.Rows.Add(2, "no")
        dt.Rows.Add(3, "ambos")

        With mcombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("cod").Width = 35
            .DropDownList.Columns("cod").Caption = "cod"
            .DropDownList.Columns.Add("desc").Width = 90
            .DropDownList.Columns("desc").Caption = "descripcion"
            .ValueMember = "cod"
            .DisplayMember = "desc"
            .DataSource = dt
            .Refresh()
        End With

        If dt.Rows.Count > 0 Then
            mcombo.SelectedIndex = 1
        End If
    End Sub

    Public Sub _prvalidarlote()
        Dim dt As DataTable = L_fnPorcUtilidad()
        If (dt.Rows.Count > 0) Then
            Dim lot As Integer = dt.Rows(0).Item("verlote")
            ocultarfact = dt.Rows(0).Item("verfactmanual")
            If (lot = 1) Then
                g_lote = True
            Else
                g_lote = False
            End If

        End If
    End Sub
    Private Sub _prcargarcombolibreriasucursal(mcombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnListarSucursales()
        With mcombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("aanumi").Width = 60
            .DropDownList.Columns("aanumi").Caption = "cod"
            .DropDownList.Columns.Add("aabdes").Width = 500
            .DropDownList.Columns("aabdes").Caption = "almacén"
            .ValueMember = "aanumi"
            .DisplayMember = "aabdes"
            .DataSource = dt
            .Refresh()
        End With
    End Sub
    Private Sub _prasignarpermisos()

        Dim dtrolusu As DataTable = L_prRolDetalleGeneral(gi_userRol, _namebutton)

        Dim show As Boolean = dtrolusu.Rows(0).Item("ycshow")
        Dim add As Boolean = dtrolusu.Rows(0).Item("ycadd")
        Dim modif As Boolean = dtrolusu.Rows(0).Item("ycmod")
        Dim del As Boolean = dtrolusu.Rows(0).Item("ycdel")

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
    Private Sub _prinhabiliitar()
        tbCodigo.ReadOnly = True
        tbFechaVenta.IsInputReadOnly = True
        tbFechaVenta.Enabled = False
        TbNomCliente.ReadOnly = True
        tbContacto.ReadOnly = True
        tbTelf.ReadOnly = True
        tbObservacion.ReadOnly = True

        tbCliente.ReadOnly = True
        tbVendedor.ReadOnly = True
        cbSucursal.ReadOnly = True
        swMoneda.IsReadOnly = True

        btnModificar.Enabled = True
        btnGrabar.Enabled = False
        btnNuevo.Enabled = True

        SwDescuentoProveedor.Enabled = False

        If grVentas.GetValue("taest") = 1 Then
            btnEliminar.Enabled = True
        Else
            btnEliminar.Enabled = False
        End If

        cbCambioDolar.ReadOnly = True
        tbSubTotal.IsInputReadOnly = True
        tbMdesc.IsInputReadOnly = True

        grVentas.Enabled = True
        PanelNavegacion.Enabled = True
        grdetalle.RootTable.Columns("img").Visible = False
        If (GPanelProductos.Visible = True) Then
            _deshabilitarproductos()
        End If

        filaselectlote = Nothing

    End Sub
    Private Sub _prhabilitar()

        grVentas.Enabled = False
        tbCodigo.ReadOnly = False
        tbFechaVenta.IsInputReadOnly = False
        tbFechaVenta.Enabled = True
        TbNomCliente.ReadOnly = False
        tbContacto.ReadOnly = False
        tbTelf.ReadOnly = False
        tbObservacion.ReadOnly = False

        swMoneda.IsReadOnly = False

        btnGrabar.Enabled = True

        If (tbCodigo.Text.Length > 0) Then
            cbSucursal.ReadOnly = True
        Else
            cbSucursal.ReadOnly = False
        End If

        dtdescuentos = L_fnListarDescuentosTodos()

    End Sub


    Public Sub _prfiltrar()
        'cargo el buscador
        Dim _mpos As Integer
        _prcargarProforma()
        If grVentas.RowCount > 0 Then
            _mpos = 0
            grVentas.Row = _mpos
        Else
            _limpiar()
            LblPaginacion.Text = "0/0"
        End If
    End Sub
    Private Sub _limpiar()

        tbCodigo.Clear()
        tbFechaVenta.Value = Now.Date
        TbNomCliente.Clear()
        tbContacto.Clear()
        tbTelf.Clear()
        tbObservacion.Clear()

        swMoneda.Value = True

        _prcargardetalleventa(-1)
        MSuperTabControl.SelectedTabIndex = 0
        tbSubTotal.Value = 0
        tbPdesc.Value = 0
        tbMdesc.Value = 0

        tbTotalBs.Text = "0.00"
        tbTotalDo.Text = "0.00"

        With grdetalle.RootTable.Columns("img")
            .Width = 80
            .Caption = "eliminar".ToUpper
            .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
            .Visible = True
        End With
        _pradddetalleventa()
        If (GPanelProductos.Visible = True) Then
            GPanelProductos.Visible = False
            PanelInferior.Visible = True
        End If


        If (CType(cbSucursal.DataSource, DataTable).Rows.Count > 0) Then
            cbSucursal.SelectedIndex = 0
        End If
        filaselectlote = Nothing
        table_producto = Nothing
        cbCambioDolar.SelectedIndex = CType(cbCambioDolar.DataSource, DataTable).Rows.Count - 1

    End Sub
    Public Sub _prmostrarregistro(_n As Integer)
        With grVentas
            cbSucursal.Value = .GetValue("taalm")
            tbCodigo.Text = .GetValue("tanumi")
            tbFechaVenta.Value = .GetValue("tafdoc")
            _codempleado = .GetValue("taven")
            tbVendedor.Text = .GetValue("vendedor")
            _codcliente = .GetValue("taclpr")
            tbCliente.Text = .GetValue("cliente")
            swMoneda.Value = .GetValue("tamon")
            tbObservacion.Text = .GetValue("taobs")

            lbFecha.Text = CType(.GetValue("tafact"), Date).ToString("dd/mm/yyyy")
            lbHora.Text = .GetValue("tahact").ToString
            lbUsuario.Text = .GetValue("tauact").ToString

        End With

        _prcargardetalleventa(tbCodigo.Text)
        tbMdesc.Value = grVentas.GetValue("tadesc")

        _prcalcularpreciototal()

        LblPaginacion.Text = Str(grVentas.Row + 1) + "/" + grVentas.RowCount.ToString

    End Sub

    Private Sub _prcargardetalleventa(ByVal _numi As String, Optional ByVal detalle As DataTable = Nothing)
        Dim dt As New DataTable
        dt = IIf(detalle Is Nothing, L_fnDetalleVenta(_numi), detalle)
        grdetalle.DataSource = dt
        grdetalle.RetrieveStructure()
        grdetalle.AlternatingColors = True

        With grdetalle.RootTable.Columns("tbnumi")
            .Width = 100
            .Caption = "codigo"
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("tbtv1numi")
            .Width = 90
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("tbty5prod")
            .Width = 55
            .Visible = True
            .Caption = "cod dynasys"
        End With
        'if _codebar = 2 then
        '    with grdetalle.roottable.columns("yfcbarra")
        '        .caption = "cod.barra"
        '        .width = 100
        '        .visible = true

        '    end with
        'else
        '    with grdetalle.roottable.columns("yfcbarra")
        '        .caption = "cod.barra"
        '        .width = 100
        '        .visible = false
        '    end with
        'end if

        With grdetalle.RootTable.Columns("codigo")
            .Caption = "código".ToUpper
            .Width = 100
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("yfcbarra")
            .Caption = "cod.barra".ToUpper
            .Width = 90
            .Visible = gb_CodigoBarra
        End With
        With grdetalle.RootTable.Columns("producto")
            .Caption = "productos".ToUpper
            .Width = 320
            .Visible = True
            .WordWrap = True
            .MaxLines = 2
        End With
        With grdetalle.RootTable.Columns("tbest")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("tbcmin")
            .Width = 60
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "cantidad".ToUpper
        End With
        With grdetalle.RootTable.Columns("tbumin")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("unidad")
            .Width = 35
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .Caption = "un.".ToUpper
        End With
        With grdetalle.RootTable.Columns("tbpbas")
            .Width = 75
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "precio u.".ToUpper
        End With
        With grdetalle.RootTable.Columns("tbptot")
            .Width = 80
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "subtotal".ToUpper
        End With
        With grdetalle.RootTable.Columns("tbporc")
            .Width = 80
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .FormatString = "0.00"
            .Caption = "p.desc(%)".ToUpper
        End With
        With grdetalle.RootTable.Columns("tbdesc")
            .Width = 60
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "m.desc".ToUpper
        End With
        With grdetalle.RootTable.Columns("tbtotdesc")
            .Width = 80
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "total".ToUpper
        End With
        With grdetalle.RootTable.Columns("tbobs")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("tbpcos")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("tbptot2")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("tbfact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("tbhact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("tbuact")
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
            .Caption = "eliminar".ToUpper
            .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
            .Visible = False
        End With
        If (g_lote = True) Then
            With grdetalle.RootTable.Columns("tblote")
                .Width = 120
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = False
                .Caption = "lote"
            End With
            With grdetalle.RootTable.Columns("tbfechavenc")
                .Width = 120
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = False
                .Caption = "fecha venc."
                .FormatString = "yyyy/mm/dd"
            End With
        Else
            With grdetalle.RootTable.Columns("tblote")
                .Width = 120
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = False
                .Caption = "lote"
            End With
            With grdetalle.RootTable.Columns("tbfechavenc")
                .Width = 120
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = False
                .Caption = "fecha venc."
                .FormatString = "yyyy/mm/dd"
            End With
        End If
        With grdetalle.RootTable.Columns("stock")
            .Width = 120
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("tbfamilia")
            .Width = 120
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("tbproveedorid")
            .Width = 120
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("ygcodsin")
            .Width = 120
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("ygcodu")
            .Width = 120
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007

            .RecordNavigator = True
            .RecordNavigatorText = "productos"
        End With
    End Sub

    Private Sub _prcargarProforma()
        Dim dt As New DataTable
        dt = L_fnGeneralVenta(If(swMostrar.Value = True, 1, 0))
        dt.Clear()
        grVentas.DataSource = dt
        grVentas.RetrieveStructure()
        grVentas.AlternatingColors = True

        With grVentas.RootTable.Columns("tanumi")
            .Width = 100
            .Caption = "id venta"
            .Visible = True
        End With
        With grVentas.RootTable.Columns("taalm")
            .Width = 90
            .Visible = False
        End With
        With grVentas.RootTable.Columns("taproforma")
            .Width = 90
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tafdoc")
            .Width = 90
            .Visible = True
            .Caption = "fecha"
        End With
        With grVentas.RootTable.Columns("taven")
            .Width = 160
            .Visible = False
        End With
        With grVentas.RootTable.Columns("vendedor")
            .Width = 250
            .Visible = True
            .Caption = "vendedor".ToUpper
        End With
        With grVentas.RootTable.Columns("tatven")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tafvcr")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("taclpr")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("cliente")
            .Width = 250
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Caption = "cliente"
        End With
        With grVentas.RootTable.Columns("tamon")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("moneda")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
            .Caption = "moneda"
        End With
        With grVentas.RootTable.Columns("taobs")
            .Width = 200
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Caption = "observacion"
        End With
        With grVentas.RootTable.Columns("tadesc")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("taest")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("taice")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tafact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tahact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tauact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("nrocaja")
            .Width = 100
            .Caption = "nro. caja"
            .Visible = True
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        End With
        With grVentas.RootTable.Columns("total")
            .Width = 150
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .Caption = "total"
            .FormatString = "0.00"
            .AggregateFunction = AggregateFunction.Sum
        End With
        With grVentas
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla
            .TotalRow = InheritableBoolean.True
            .TotalRowFormatStyle.BackColor = Color.Gold
            .TotalRowPosition = TotalRowPosition.BottomFixed

            .RecordNavigator = True
            .RecordNavigatorText = "ventas"
        End With

        If (dt.Rows.Count <= 0) Then
            _prcargardetalleventa(-1)
        End If
    End Sub
    Public Sub _prguardar()

        If (tbCodigo.Text = String.Empty) Then
            _guardarnuevo()
        Else
            If (tbCodigo.Text <> String.Empty) Then
                _prguardarmodificado()
                ''    _prinhabiliitar() rodrigo rla
            End If
        End If
    End Sub
    Public Sub actualizarsaldosinlote(ByRef dt As DataTable)
        Dim _detalle As DataTable = CType(grdetalle.DataSource, DataTable)

        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
            Dim sum As Integer = 0
            Dim codproducto As Integer = dt.Rows(i).Item("yfnumi")
            For j As Integer = 0 To grdetalle.RowCount - 1 Step 1
                grdetalle.Row = j
                Dim estado As Integer = grdetalle.GetValue("estado")
                If (estado = 0) Then
                    If (codproducto = grdetalle.GetValue("tbty5prod")) Then
                        sum = sum + grdetalle.GetValue("tbcmin")
                    End If
                End If
            Next
            dt.Rows(i).Item("stock") = dt.Rows(i).Item("stock") - sum
        Next

    End Sub

    Private Sub _prcargarproductos(_cliente As String)
        If (cbSucursal.SelectedIndex < 0) Then
            Return
        End If
        Dim nombregrupos As DataTable = L_fnNameLabel()
        Dim dt As New DataTable

        If (g_lote = True) Then
            dt = L_fnListarProductos(cbSucursal.Value, _cliente)
        Else
            'dt = L_fnListarProductosSinLoteUlt(cbSucursal.Value, _cliente, CType(grdetalle.DataSource, DataTable), cbcanje.value)
            dt = L_fnListarProductosSinLoteUltProforma(cbSucursal.Value, _cliente, CType(grdetalle.DataSource, DataTable))
        End If

        grProductos.DataSource = dt
        grProductos.RetrieveStructure()
        grProductos.AlternatingColors = True

        If gb_TipoAyuda = ENProductoAyuda.SUPERMERCADO Then
            armargrillaproducto(nombregrupos, False)
        ElseIf gb_TipoAyuda = ENProductoAyuda.FARMACIA Then
            armargrillaproducto(nombregrupos, True)
        End If
        _praplicarcondiccionjanussinlote()
    End Sub

    Private Sub armargrillaproducto(dtname As DataTable, visualizargrupo As Boolean)
        With grProductos.RootTable.Columns("yfnumi")
            .Width = 70
            .Caption = "cód. dynasys".ToUpper
            .Visible = True
        End With
        With grProductos.RootTable.Columns("yfcprod")
            .Width = 90
            .Caption = "cód.delta".ToUpper
            .Visible = True
        End With
        With grProductos.RootTable.Columns("yfcbarra")
            .Width = 90
            .Caption = "cod. barra".ToUpper
            .Visible = gb_CodigoBarra
        End With
        With grProductos.RootTable.Columns("yfcdprod1")
            .Width = IIf(visualizargrupo, 360, 380)
            .Visible = True
            .Caption = "descripción".ToUpper
            .WordWrap = True
            .MaxLines = 20
        End With
        With grProductos.RootTable.Columns("yfcdprod2")
            .Width = 150
            .Visible = False
            .Caption = "descripcion corta".ToUpper
        End With
        With grProductos.RootTable.Columns("yfvsup")
            .Width = 90
            .Visible = True
            .Caption = "conversión".ToUpper
            .FormatString = "0.00"
        End With
        With grProductos.RootTable.Columns("yfgr1")
            .Width = 160
            .Visible = False
        End With
        With grProductos.RootTable.Columns("yfgr5")
            .Width = 160
            .Visible = False
        End With
        With grProductos.RootTable.Columns("grupo5")
            .Caption = "categoria"
            .Width = 120
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            '.visible = visualizargrupo
            .Visible = False
            .WordWrap = True
            .MaxLines = 20
        End With
        If (dtname.Rows.Count > 0) Then

            With grProductos.RootTable.Columns("grupo1")
                .Width = 100
                .Caption = dtname.Rows(0).Item("grupo 1").ToString
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = True
                .WordWrap = False
                .MaxLines = 20
            End With
            With grProductos.RootTable.Columns("grupo2")
                .Width = 120
                .Caption = dtname.Rows(0).Item("grupo 2").ToString
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                '.visible = visualizargrupo
                .Visible = False
                .WordWrap = True
                .MaxLines = 20
            End With
            With grProductos.RootTable.Columns("grupo3")
                .Width = 120
                .Caption = dtname.Rows(0).Item("grupo 3").ToString
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = False
            End With
            With grProductos.RootTable.Columns("grupo4")
                .Width = 100
                .Caption = dtname.Rows(0).Item("grupo 4").ToString.ToUpper
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = visualizargrupo
                .WordWrap = True
                .Visible = False
                .MaxLines = 20
            End With
        Else
            With grProductos.RootTable.Columns("grupo1")
                .Width = 120
                .Caption = "grupo 1"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = False
                .WordWrap = True
                .MaxLines = False
            End With
            With grProductos.RootTable.Columns("grupo2")
                .Width = 120
                .Caption = "grupo 2"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = visualizargrupo
                .WordWrap = True
                .MaxLines = 20
            End With
            With grProductos.RootTable.Columns("grupo3")
                .Width = 120
                .Caption = "grupo 3"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = False
            End With
            With grProductos.RootTable.Columns("grupo4")
                .Width = 120
                .Caption = "grupo 4"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = visualizargrupo
                .WordWrap = True
                .MaxLines = 20
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
        With grProductos.RootTable.Columns("validacion")
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
        With grProductos.RootTable.Columns("unidmin")
            .Width = 80
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = IIf(visualizargrupo, False, True)
            .Caption = "u. min."
        End With
        With grProductos.RootTable.Columns("yhprecio")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Caption = "precio".ToUpper
            .FormatString = "0.00"
        End With
        With grProductos.RootTable.Columns("pcos")
            .Width = 70
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
            .Caption = "precio costo".ToUpper
            .FormatString = "0.00"
        End With
        With grProductos.RootTable.Columns("stock")
            .Width = 70
            .FormatString = "0.00"
            .Visible = True
            .Caption = "stock".ToUpper
        End With
        With grProductos.RootTable.Columns("descuentoid")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grProductos.RootTable.Columns("grupodesc")
            .Width = 100
            .Visible = False
            .Caption = "grupo desc.".ToUpper
        End With
        With grProductos.RootTable.Columns("ygcodsin")
            .Width = 0
            .Visible = False
            .Caption = "codsin"
        End With
        With grProductos.RootTable.Columns("ygcodu")
            .Visible = False
        End With
        With grProductos
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            .ColumnAutoResize = True
            .AutoScrollMargin = AutoScrollPosition
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
            .RecordNavigator = True
            .RecordNavigatorText = "Productos"
        End With
    End Sub

    Public Sub _praplicarcondiccionjanussinlote()
        'dim fc as gridexformatcondition
        'fc = new gridexformatcondition(grproductos.roottable.columns("stock"), conditionoperator.between, -9998 and 0)
        ''fc.formatstyle.fontbold = tristate.true
        'fc.formatstyle.forecolor = color.red    'color.tan
        'grproductos.roottable.formatconditions.add(fc)
        Dim fr As GridEXFormatCondition
        fr = New GridEXFormatCondition(grProductos.RootTable.Columns("validacion"), ConditionOperator.Equal, 1)
        fr.FormatStyle.ForeColor = Color.Red
        grProductos.RootTable.FormatConditions.Add(fr)
    End Sub


    Public Sub actualizarsaldo(ByRef dt As DataTable, codproducto As Integer)

        Dim _detalle As DataTable = CType(grdetalle.DataSource, DataTable)

        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
            Dim lote As String = dt.Rows(i).Item("iclot")
            Dim fechavenc As Date = dt.Rows(i).Item("icfven")
            Dim sum As Integer = 0
            For j As Integer = 0 To _detalle.Rows.Count - 1
                Dim estado As Integer = _detalle.Rows(j).Item("estado")
                If (estado = 0) Then
                    If (lote = _detalle.Rows(j).Item("tblote") And
                        fechavenc = _detalle.Rows(j).Item("tbfechavenc") And codproducto = _detalle.Rows(j).Item("tbty5prod")) Then
                        sum = sum + _detalle.Rows(j).Item("tbcmin")
                    End If
                End If
            Next
            dt.Rows(i).Item("iccven") = dt.Rows(i).Item("iccven") - sum
        Next

    End Sub

    Private Sub _prcargarlotesdeproductos(codproducto As Integer, nameproducto As String)
        If (cbSucursal.SelectedIndex < 0) Then
            Return
        End If
        Dim dt As New DataTable
        GPanelProductos.Text = nameproducto
        dt = L_fnListarLotesPorProductoVenta(cbSucursal.Value, codproducto)  ''1=almacen
        actualizarsaldo(dt, codproducto)
        grProductos.DataSource = dt
        grProductos.RetrieveStructure()
        grProductos.AlternatingColors = True

        With grProductos.RootTable.Columns("yfcdprod1")
            .Width = 150
            .Visible = False
        End With
        With grProductos.RootTable.Columns("iclot")
            .Width = 150
            .Caption = "lote"
            .Visible = True
        End With
        With grProductos.RootTable.Columns("icfven")
            .Width = 160
            .Caption = "fecha vencimiento"
            .FormatString = "yyyy/mm/dd"
            .Visible = True
        End With
        With grProductos.RootTable.Columns("iccven")
            .Width = 150
            .Visible = True
            .Caption = "stock"
            .FormatString = "0.00"
            .AggregateFunction = AggregateFunction.Sum
        End With
        With grProductos.RootTable.Columns("stockminimo")
            .Width = 150
            .Visible = False
        End With
        With grProductos.RootTable.Columns("fechavencimiento")
            .Width = 150
            .Visible = False
        End With
        With grProductos.RootTable.Columns("descuentoid")
            .Visible = False
        End With

        With grProductos
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla
            .TotalRow = InheritableBoolean.True
            .TotalRowFormatStyle.BackColor = Color.Gold
            .TotalRowPosition = TotalRowPosition.BottomFixed
            .VisualStyle = VisualStyle.Office2007
        End With
        _praplicarcondiccionjanuslote()
    End Sub
    Public Sub _praplicarcondiccionjanuslote()

        Dim fc2 As GridEXFormatCondition
        fc2 = New GridEXFormatCondition(grProductos.RootTable.Columns("stockminimo"), ConditionOperator.Equal, 1)
        fc2.FormatStyle.BackColor = Color.Red
        fc2.FormatStyle.FontBold = TriState.True
        fc2.FormatStyle.ForeColor = Color.White
        grProductos.RootTable.FormatConditions.Add(fc2)

        Dim fc As GridEXFormatCondition
        fc = New GridEXFormatCondition(grProductos.RootTable.Columns("fechavencimiento"), ConditionOperator.Equal, 1)
        fc.FormatStyle.BackColor = Color.Gold
        fc.FormatStyle.FontBold = TriState.True
        fc.FormatStyle.ForeColor = Color.White
        grProductos.RootTable.FormatConditions.Add(fc)

    End Sub
    Private Sub _pradddetalleventa()
        Dim bin As New MemoryStream
        Dim img As New Bitmap(My.Resources.delete, 28, 28)
        img.Save(bin, Imaging.ImageFormat.Png)
        CType(grdetalle.DataSource, DataTable).Rows.Add(_fnsiguientenumi() + 1, 0, 0, "", 0, "", 0, 0, 0, "", 0, 0, 0, 0, 0, "", 0, "20500101", CDate("2050/01/01"), 0, Now.Date, "", "", 0, bin.GetBuffer, 0, 0, 0, "", "")
    End Sub

    Public Function _fnsiguientenumi()
        Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)
        Dim rows() As DataRow = dt.Select("tbnumi=max(tbnumi)")
        If (rows.Count > 0) Then
            Return rows(rows.Count - 1).Item("tbnumi")
        End If
        Return 1
    End Function
    Public Function _fnaccesible()
        Return tbFechaVenta.IsInputReadOnly = False
    End Function
    Private Sub _habilitarproductos()
        GPanelProductos.Visible = True
        PanelInferior.Visible = False
        _prcargarproductos(Str(_codcliente))
        grProductos.Focus()
        grProductos.MoveTo(grProductos.FilterRow)
        grProductos.Col = 2
    End Sub
    Private Sub _habilitarfocodetalle(fila As Integer)
        _prcargarproductos(Str(_codcliente))
        grdetalle.Focus()
        grdetalle.Row = fila
        grdetalle.Col = 2
    End Sub
    Private Sub _deshabilitarproductos()
        GPanelProductos.Visible = False
        PanelInferior.Visible = True

        grdetalle.Select()
        grdetalle.Col = 5
        grdetalle.Row = grdetalle.RowCount - 1
    End Sub
    Public Sub _fnobtenerfiladetalle(ByRef pos As Integer, numi As Integer)
        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _numi As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("tbnumi")
            If (_numi = numi) Then
                pos = i
                Return
            End If
        Next
    End Sub

    Public Sub _fnobtenerfiladetalleproducto(ByRef pos As Integer, numi As Integer)
        For i As Integer = 0 To CType(grProductos.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _numi As Integer = CType(grProductos.DataSource, DataTable).Rows(i).Item("yfnumi")
            If (_numi = numi) Then
                pos = i
                Return
            End If
        Next
    End Sub

    Public Function _fnexisteproducto(idprod As Integer) As Boolean
        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _idprod As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("tbty5prod")
            Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("estado")
            If (_idprod = idprod And estado >= 0) Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Function _fnexisteproductoconlote(idprod As Integer, lote As String, fechavenci As Date) As Boolean
        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _idprod As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("tbty5prod")
            Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("estado")

            Dim _lotedetalle As String = CType(grdetalle.DataSource, DataTable).Rows(i).Item("tblote")
            Dim _fechavencdetalle As Date = CType(grdetalle.DataSource, DataTable).Rows(i).Item("tbfechavenc")
            If (_idprod = idprod And estado >= 0 And lote = _lotedetalle And fechavenci = _fechavencdetalle) Then
                Return True
            End If
        Next
        Return False
    End Function
    Public Sub p_ponertotal(rowindex As Integer)

        calculardescuentostotal()

        If (rowindex < grdetalle.RowCount) Then
            'grdetalle.updatedata()
            Dim lin As Integer = grdetalle.GetValue("tbnumi")
            Dim pos As Integer = -1
            _fnobtenerfiladetalle(pos, lin)
            Dim cant As Double = grdetalle.GetValue("tbcmin")
            'dim cantidad = format(cant,"0.00")
            Dim uni As Double = grdetalle.GetValue("tbpbas")
            Dim cos As Double = grdetalle.GetValue("tbpcos")
            Dim montodesc As Double = grdetalle.GetValue("tbdesc")
            Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)
            If (pos >= 0) Then
                Dim totalunitario As Double = cant * uni
                Dim totalcosto As Double = cant * cos
                'grdetalle.setvalue("lcmdes", montodesc)

                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot") = Format(totalunitario, "#.#0")
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = Format((totalunitario - montodesc), "#.#0")

                grdetalle.SetValue("tbptot", Format(totalunitario, "#.#0"))
                grdetalle.SetValue("tbtotdesc", Format((totalunitario - montodesc), "#.#0"))

                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot2") = totalcosto
                grdetalle.SetValue("tbptot2", totalcosto)

                Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado")
                If (estado = 1) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = 2
                End If
            End If
            _prcalcularpreciototal()
        End If
    End Sub

    Public Sub _prcalcularpreciototal()
        Dim totaldescuento As Double = 0
        Dim totalcosto As Double = 0
        Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)
        For i As Integer = 0 To dt.Rows.Count - 1 Step 1

            If (dt.Rows(i).Item("estado") >= 0) Then
                totaldescuento = totaldescuento + dt.Rows(i).Item("tbptot")
                totalcosto = totalcosto + dt.Rows(i).Item("tbptot2")
            End If
        Next

        'grdetalle.updatedata()
        Dim montodo As Decimal
        Dim montodesc As Double = tbMdesc.Value
        Dim pordesc As Double = ((montodesc * 100) / totaldescuento)
        tbPdesc.Value = pordesc
        Dim subtotal = Convert.ToDouble(Format(totaldescuento, "0.00"))
        tbSubTotal.Value = subtotal

        'tbtotalbs.text = total.tostring()
        tbTotalBs.Text = tbSubTotal.Value - montodesc
        montodo = Convert.ToDecimal(tbTotalBs.Text) / IIf(cbCambioDolar.Text = "", 1, Convert.ToDecimal(cbCambioDolar.Text))
        tbTotalDo.Text = Format(montodo, "0.00")

        'calcularcambio()
    End Sub
    Public Sub _preliminarfila()
        If (grdetalle.Row >= 0) Then
            If (grdetalle.RowCount >= 2) Then
                Dim estado As Integer = grdetalle.GetValue("estado")
                Dim pos As Integer = -1
                Dim lin As Integer = grdetalle.GetValue("tbnumi")
                _fnobtenerfiladetalle(pos, lin)
                If (estado = 0) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = -2
                End If
                If (estado = 1) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = -1
                End If

                'grdetalle.roottable.applyfilter(new janus.windows.gridex.gridexfiltercondition(grdetalle.roottable.columns("estado"), janus.windows.gridex.conditionoperator.greaterthanorequalto, -3))

                grdetalle.Select()
                grdetalle.UpdateData()
                grdetalle.Col = grdetalle.RootTable.Columns("yfcbarra").Index
                grdetalle.Row = grdetalle.RowCount - 1
                grdetalle.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grdetalle.RootTable.Columns("estado"), Janus.Windows.GridEX.ConditionOperator.GreaterThanOrEqualTo, 0))
                _prcalcularpreciototal()
            End If
        End If
        'grdetalle.refetch()
        'grdetalle.refresh()
    End Sub
    Public Function _validarcampos() As Boolean
        Try
            If (_codcliente <= 0) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "por favor seleccione un cliente con ctrl+enter".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                tbCliente.Focus()
                Return False
            End If
            If (_codempleado <= 0) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "por favor seleccione un vendedor con ctrl+enter".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                tbVendedor.Focus()
                Return False
            End If
            If (cbSucursal.SelectedIndex < 0) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "por favor seleccione una sucursal".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                cbSucursal.Focus()
                Return False
            End If


            If (TbNomCliente.Text = String.Empty) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "por favor ponga la razón social del cliente.".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                TbNomCliente.Focus()
                Return False
            End If




            If (grdetalle.RowCount = 1) Then
                grdetalle.Row = grdetalle.RowCount - 1
                If (grdetalle.GetValue("tbty5prod") = 0) Then
                    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                    ToastNotification.Show(Me, "por favor seleccione  un detalle de producto".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                    Return False
                End If
            End If

            Return True
        Catch ex As Exception
            mostrarmensajeerror(ex.Message)
            Return False
        End Try
    End Function
    Private Sub _prinsertarmontonuevo(ByRef tabla As DataTable)
        'tabla.Rows.Add(0, tbmontobs.value, tbmontodolar.value, tbmontotarej.value, cbCambioDolar.Text, nrotarjeta, tbmontoqr.text, 0)
    End Sub
    Private Sub _prinsertarmontomodificar(ByRef tabla As DataTable)
        'tabla.Rows.Add(tbCodigo.Text, tbmontobs.value, tbmontodolar.value, tbmontotarej.value, cbCambioDolar.Text, 2)
    End Sub
    Public Function rearmardetalle() As DataTable
        Dim dt, dtdetalle, dtsaldos As DataTable
        Dim cantidadrepetido, contar, idaux As Integer
        Dim resultadoinventario = False

        dt = CType(grdetalle.DataSource, DataTable)
        'ordena el detalle por codigo importante
        dt.DefaultView.Sort = "tbty5prod asc"
        dt = dt.DefaultView.ToTable
        dtdetalle = dt.Copy
        dtdetalle.Clear()
        contar = 0
        Try
            For i As Integer = 0 To dt.Rows.Count - 1 Step 1
                Dim codproducto As Integer = dt.Rows(i).Item("tbty5prod")
                dt.DefaultView.RowFilter = "tbty5prod =  '" + codproducto.ToString() + "'"
                cantidadrepetido = dt.DefaultView.Count()
                If idaux <> codproducto Then
                    contar = 1
                Else
                    contar += 1
                End If
                idaux = codproducto

                'evita llamar a saldo cada iteracion
                If contar = 1 Then
                    dtsaldos = L_fnObteniendoSaldosTI001(codproducto, 1)
                    dtsaldos.DefaultView.Sort = "icfven asc"
                    dtsaldos = dtsaldos.DefaultView.ToTable
                End If
                'dtsaldos.defaultview.rowfilter = "iccven >  '" + 0.tostring() + "'"
                'dtsaldos = dtsaldos.defaultview.totable
                Dim cantidad As Double = dt.Rows(i).Item("tbcmin")
                Dim saldo As Double = cantidad
                Dim estado As Integer = dt.Rows(i).Item("estado")
                Dim k As Integer = 0
                If (estado >= 0) Then
                    If (dtsaldos.Rows.Count <= 0) Then
                        dtdetalle.ImportRow(dt.Rows(i))
                    Else
                        While (k <= dtsaldos.Rows.Count - 1 And saldo > 0)

                            Dim inventario As Double = dtsaldos.Rows(k).Item("iccven")
                            If (inventario >= saldo) Then
                                dtdetalle.ImportRow(dt.Rows(i))
                                Dim pos As Integer = dtdetalle.Rows.Count - 1

                                Dim precio As Double = dtdetalle.Rows(pos).Item("tbpbas")
                                Dim total As Decimal = CStr(Format(precio * saldo, "####0.00"))

                                dtdetalle.Rows(pos).Item("tbptot") = total
                                dtdetalle.Rows(pos).Item("tbtotdesc") = total - dtdetalle.Rows(pos).Item("tbdesc")
                                'ctype(grdetalle.datasource, datatable).rows(pos).item("tbtotdesc") = total
                                'ctype(grdetalle.datasource, datatable).rows(pos).item("tbcmin") = saldo
                                dtdetalle.Rows(pos).Item("tbcmin") = saldo

                                Dim preciocosto As Double = dtdetalle.Rows(pos).Item("tbpcos")
                                dtdetalle.Rows(pos).Item("tbptot2") = preciocosto * saldo
                                dtdetalle.Rows(pos).Item("tblote") = dtsaldos.Rows(k).Item("iclot")
                                dtdetalle.Rows(pos).Item("tbfechavenc") = dtsaldos.Rows(k).Item("icfven")
                                dtsaldos.Rows(k).Item("iccven") = inventario - saldo
                                saldo = 0

                            Else
                                'cuando el invetanrio es menor
                                If (k <= dtsaldos.Rows.Count - 1 And inventario > 0) Then

                                    dtdetalle.ImportRow(dt.Rows(i))
                                    Dim pos As Integer = dtdetalle.Rows.Count - 1

                                    Dim precio As Double = dtdetalle.Rows(pos).Item("tbpbas")
                                    Dim total As Decimal = CStr(Format(precio * inventario, "####0.00"))
                                    dtdetalle.Rows(pos).Item("tbptot") = total
                                    dtdetalle.Rows(pos).Item("tbtotdesc") = total - dtdetalle.Rows(pos).Item("tbdesc")
                                    'ctype(grdetalle.datasource, datatable).rows(pos).item("tbtotdesc") = total
                                    'ctype(grdetalle.datasource, datatable).rows(pos).item("tbcmin") = inventario
                                    dtdetalle.Rows(pos).Item("tbcmin") = inventario

                                    Dim preciocosto As Double = dtdetalle.Rows(pos).Item("tbpcos")
                                    dtdetalle.Rows(pos).Item("tbptot2") = preciocosto * inventario
                                    dtdetalle.Rows(pos).Item("tblote") = dtsaldos.Rows(k).Item("iclot")
                                    dtdetalle.Rows(pos).Item("tbfechavenc") = dtsaldos.Rows(k).Item("icfven")

                                    saldo = saldo - inventario
                                    'actualiza el inventario en la tabla
                                    dtsaldos.Rows(k).Item("iccven") = dtsaldos.Rows(k).Item("iccven") - inventario
                                End If
                            End If
                            k += 1
                        End While
                        If saldo <> 0 Then
                            dtdetalle.ImportRow(dt.Rows(i))
                            Dim pos As Integer = dtdetalle.Rows.Count - 1
                            Dim precio As Double = dtdetalle.Rows(pos).Item("tbpbas")
                            Dim total As Decimal = CStr(Format(precio * saldo, "####0.00"))
                            dtdetalle.Rows(pos).Item("tbptot") = total
                            dtdetalle.Rows(pos).Item("tbtotdesc") = total - dtdetalle.Rows(pos).Item("tbdesc")
                            dtdetalle.Rows(pos).Item("tbcmin") = saldo
                            Dim preciocosto As Double = dtdetalle.Rows(pos).Item("tbpcos")
                            dtdetalle.Rows(pos).Item("tbptot2") = preciocosto * saldo
                            dtdetalle.Rows(pos).Item("tblote") = dtsaldos.Rows(k - 1).Item("iclot")
                            dtdetalle.Rows(pos).Item("tbfechavenc") = dtsaldos.Rows(k - 1).Item("icfven")
                            saldo = 0
                        End If
                    End If
                End If
            Next
            Return dtdetalle
        Catch ex As Exception
            mostrarmensajeerror(ex.Message)
            Return dtdetalle
        End Try
    End Function
    Private Function _prexistestockparaproducto() As Boolean
        Dim dtsaldos As DataTable
        Dim aux As Integer = 0
        Dim detalle = CType(grdetalle.DataSource, DataTable)
        Dim lmensaje As List(Of String) = New List(Of String)
        detalle.DefaultView.RowFilter = "estado >= '" + 0.ToString() + "'"
        detalle = detalle.DefaultView.ToTable
        For Each fila As DataRow In detalle.Rows
            Dim idproducto = fila.Item("tbty5prod")
            If aux <> idproducto Then
                dtsaldos = L_fnObteniendoSaldosTI001(fila.Item("tbty5prod"), 1)
                Dim inventario = dtsaldos.Compute("sum(iccven)", String.Empty)

                detalle.DefaultView.RowFilter = "tbty5prod = '" + fila.Item("tbty5prod").ToString() + "'"
                Dim productorepeditos = detalle.DefaultView.ToTable
                Dim saldo = productorepeditos.Compute("sum(tbcmin)", String.Empty)
                'dim saldo = fila.item("tbcmin")
                If inventario < saldo Then
                    Dim mensaje As String = "no existe stock para el producto: " + fila.Item("producto") + " stock actual = " + inventario.ToString()
                    lmensaje.Add(mensaje)
                    'throw new exception("no existe stock para el producto:" + fila.item("producto") + " stock actual =" + inventario)
                End If
            End If
            aux = idproducto
            'dtsaldos.select = "icfven asc"
            'dtsaldos = dtsaldos.defaultview.totable
        Next
        If lmensaje.Count > 0 Then
            Dim mensaje = ""
            For Each item As String In lmensaje
                mensaje = mensaje + "- " + item + vbCrLf
            Next
            mostrarmensajeerror(mensaje)
            Return False
        End If
        Return True
    End Function

    Public Sub _guardarnuevo()
        Try
            Dim numi As String = ""
            Dim tabla As DataTable = L_fnMostrarMontos2(0)
            Dim factura = gb_FacturaEmite
            _prinsertarmontonuevo(tabla)

            ''verifica si existe estock para los productos
            'if _prexistestockparaproducto() then

            'dim dtdetalle as datatable = rearmardetalle()
            Dim dtdetalle As DataTable = CType(grdetalle.DataSource, DataTable)

            ''datos para facturar
            Dim a As Double = CDbl(Convert.ToDouble(tbTotalBs.Text) + tbMdesc.Value)
            Dim b As Double = CDbl(0)
            Dim c As Double = CDbl("0")
            Dim d As Double = CDbl("0")
            Dim e As Double = a - b - c - d
            Dim f As Double = CDbl(tbMdesc.Value)
            Dim g As Double = e - f
            Dim h As Double = g * (gi_IVA / 100)


            Dim res As Boolean
            '= L_fnGrabarVenta(numi, "", tbFechaVenta.Value.ToString("yyyy/mm/dd"), _codempleado, IIf(swTipoVenta.Value = True, 1, 0),
            '                                     IIf(swTipoVenta.Value = True, Now.Date.ToString("yyyy/mm/dd"), tbFechaVenc.Value.ToString("yyyy/mm/dd")),
            '                                     _codcliente, IIf(swmoneda.value = True, 1, 0), tbObservacion.Text.Trim, tbMdesc.Value, tbIce.Value,
            '                                     tbTotalBs.Text, dtdetalle, cbSucursal.Value, 0, tabla, gs_NroCaja, programa, tbnit.text, TbNombre1.Text,
            '                                     TbEmail.Text, cbtipodoc.value, 1, tbcomplemento.text, tbcel.text, nrofact, gb_cufSifac, "a-" + _codcliente.ToString,
            '                                     CStr(Format(a, "####0.00")), CStr(Format(b, "####0.00")), CStr(Format(c, "####0.00")), CStr(Format(d, "####0.00")),
            '                                     CStr(Format(e, "####0.00")), CStr(Format(f, "####0.00")), CStr(Format(g, "####0.00")), CStr(Format(h, "####0.00")),
            '                                     qrurl, facturl, segundaleyenda, terceraleyenda, cudf, anhio, IIf(gb_FacturaEmite = True, 1, 0), gs_VersionSistema,
            '                                     gs_IPMaquina, gs_UsuMaquina, cbcanje.value, codigoficha)

            If res Then


                _primiprimirnotaventa(numi)

                Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
                ToastNotification.Show(Me, "código de venta ".ToUpper + tbCodigo.Text + " grabado con exito.".ToUpper,
                                          img, 2000,
                                          eToastGlowColor.Green,
                                          eToastPosition.TopCenter
                                          )


                _prcargarProforma()
                _limpiar()
                btnEliminar.Enabled = False
                asignarclienteempleado()
                tbCliente.Select()
                table_producto = Nothing

            Else
                Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                ToastNotification.Show(Me, "la venta no pudo ser insertado, intente nuevamente".ToUpper, img, 2500, eToastGlowColor.Red, eToastPosition.BottomCenter)

            End If
        Catch ex As Exception
            mostrarmensajeerror(ex.Message)
        End Try

    End Sub
    Public Sub _primiprimirnotaventa(numi As String)
        Dim ef = New Efecto

        ef.tipo = 2
        ef.Context = "mensaje principal".ToUpper
        ef.Header = "¿desea imprimir la nota de venta?".ToUpper
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then
            p_generarreporte(numi)
        End If
    End Sub
    Public Sub _primiprimirfacturapreimpresa(numi As String)
        Dim ef = New Efecto

        ef.tipo = 2
        ef.Context = "mensaje principal".ToUpper
        ef.Header = "¿desea imprimir la factura preimpresa?".ToUpper
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then
            p_generarreportefactura(numi)
        End If
    End Sub
    Private Sub _prguardarmodificado()
        Dim tabla As DataTable = L_fnMostrarMontos(0)
        _prinsertarmontomodificar(tabla)
        Dim dtdetalle As DataTable = rearmardetalle()
        Dim res As Boolean
        '= L_fnModificarVenta(tbCodigo.Text, tbFechaVenta.Value.ToString("yyyy/mm/dd"), _codempleado, IIf(swTipoVenta.Value = True, 1, 0),
        '                                        IIf(swTipoVenta.Value = True, Now.Date.ToString("yyyy/mm/dd"), tbFechaVenc.Value.ToString("yyyy/mm/dd")),
        '                                        _codcliente, IIf(swMoneda.Value = True, 1, 0), tbObservacion.Text.Trim, tbMdesc.Value, tbIce.Value, tbTotalBs.Text, dtdetalle, cbSucursal.Value, 0, tabla)
        If res Then

            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "código de venta ".ToUpper + tbCodigo.Text + " modificado con exito.".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter
                                      )
            _prcargarProforma()
            _prsalir()
        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "la venta no pudo ser modificada".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        End If
    End Sub
    Private Sub _prsalir()
        If btnGrabar.Enabled = True Then
            _prinhabiliitar()
            If grVentas.RowCount > 0 Then
                _prmostrarregistro(0)
            End If
        Else
            Me.Close()
            '_modulo.select()
        End If
    End Sub
    Public Sub _prcargariconeliminar()
        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim bin As New MemoryStream
            Dim img As New Bitmap(My.Resources.delete, 28, 28)
            img.Save(bin, Imaging.ImageFormat.Png)
            CType(grdetalle.DataSource, DataTable).Rows(i).Item("img") = bin.GetBuffer
            grdetalle.RootTable.Columns("img").Visible = True
        Next
    End Sub
    Public Sub _primerregistro()
        Dim _mpos As Integer
        If grVentas.RowCount > 0 Then
            _mpos = 0
            ''   _prmostrarregistro(_mpos)
            grVentas.Row = _mpos
        End If
    End Sub
    Public Sub insertarproductossinlote()
        Dim pos As Integer = -1
        grdetalle.Row = grdetalle.RowCount - 1
        _fnobtenerfiladetalle(pos, grdetalle.GetValue("tbnumi"))
        Dim existe As Boolean = _fnexisteproducto(grProductos.GetValue("yfnumi"))
        If ((pos >= 0) And (Not existe)) Then
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbty5prod") = grProductos.GetValue("yfnumi")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("codigo") = grProductos.GetValue("yfcprod")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("yfcbarra") = grProductos.GetValue("yfcbarra")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("producto") = grProductos.GetValue("yfcdprod1")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbumin") = grProductos.GetValue("yfumin")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("unidad") = grProductos.GetValue("unidmin")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpbas") = grProductos.GetValue("yhprecio")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot") = grProductos.GetValue("yhprecio")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = grProductos.GetValue("yhprecio")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbfamilia") = grProductos.GetValue("yfgr4")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbproveedorid") = grProductos.GetValue("descuentoid")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("ygcodsin") = grProductos.GetValue("ygcodsin")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("ygcodu") = grProductos.GetValue("ygcodu")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbcmin") = 1
            'if (gb_facturaincluirice) then
            '    ctype(grdetalle.datasource, datatable).rows(pos).item("tbpcos") = grproductos.getvalue("pcos")
            'else
            '    ctype(grdetalle.datasource, datatable).rows(pos).item("tbpcos") = 0
            'end if
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos") = grProductos.GetValue("pcos")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot2") = grProductos.GetValue("pcos")

            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("stock") = grProductos.GetValue("stock")
            _prcalcularpreciototal()
            _deshabilitarproductos()
        Else
            If (existe) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "el producto ya existe en el detalle, modifique la cantidad".ToUpper, img, 2500, eToastGlowColor.Red, eToastPosition.BottomCenter)
            End If
        End If
    End Sub
    Public Sub insertarproductosconlote()
        Dim pos As Integer = -1
        grdetalle.Row = grdetalle.RowCount - 1
        _fnobtenerfiladetalleproducto(pos, grProductos.GetValue("yfnumi"))
        Dim posproducto As Integer = grProductos.Row
        filaselectlote = CType(grProductos.DataSource, DataTable).Rows(pos)

        If (grProductos.GetValue("stock") > 0) Then
            _prcargarlotesdeproductos(grProductos.GetValue("yfnumi"), grProductos.GetValue("yfcdprod1"))
        Else
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "el producto: ".ToUpper + grProductos.GetValue("yfcdprod1") + " no cuenta con stock disponible", img, 5000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            filaselectlote = Nothing
        End If
    End Sub


    Private Function p_fnvalidarfactura() As Boolean
        Return True
    End Function



    Private Sub serparametrosfactura(_ds As DataSet, ByRef _ds1 As DataSet, _ds2 As DataSet, ByRef _autorizacion As String, ByRef _hora As String, ByRef _literal As String,
                              ByRef _numfac As Integer, objrep As Object, tiporeporte As String, _fecha As String, grabarpdf As Boolean, _numidosif As String, numi As String, tipofactura As Integer)
        Select Case tiporeporte
            Case ENReporteTipo.FACTURA_Ticket
                objrep.setdatasource(_ds.Tables(0))
                objrep.setparametervalue("hora", _hora)
                objrep.setparametervalue("direccionpr", _ds2.Tables(0).Rows(0).Item("scdir").ToString)
                objrep.setparametervalue("telefonopr", _ds2.Tables(0).Rows(0).Item("sctelf").ToString)
                objrep.setparametervalue("literal1", _literal)
                objrep.setparametervalue("literal2", " ")
                objrep.setparametervalue("literal3", " ")
                objrep.setparametervalue("nrofactura", _numfac)
                objrep.setparametervalue("nroautoriz", _autorizacion)
                objrep.setparametervalue("enombre", _ds2.Tables(0).Rows(0).Item("scneg").ToString) '?
                objrep.setparametervalue("ecasamatriz", _ds2.Tables(0).Rows(0).Item("scsuc").ToString)
                objrep.setparametervalue("eciudadpais", _ds2.Tables(0).Rows(0).Item("scpai").ToString)
                objrep.setparametervalue("esfc", "sfc " + _ds1.Tables(0).Rows(0).Item("sbsfc").ToString)
                objrep.setparametervalue("enit", _ds2.Tables(0).Rows(0).Item("scnit").ToString)
                objrep.setparametervalue("eactividad", _ds2.Tables(0).Rows(0).Item("scact").ToString)
                objrep.setparametervalue("eduenho", _ds2.Tables(0).Rows(0).Item("scnom").ToString)
                'objrep.setparametervalue("enota", "''" + "esta factura contribuye al desarrollo del país el uso ilícito de ésta será sancionado de acuerdo a la ley" + "''")
                objrep.setparametervalue("enota", _ds1.Tables(0).Rows(0).Item("sbnota").ToString)
                objrep.setparametervalue("eley", _ds1.Tables(0).Rows(0).Item("sbnota2").ToString)
                objrep.setparametervalue("fechalim", _ds1.Tables(0).Rows(0).Item("sbfal"))
                objrep.setparametervalue("usuario", gs_user)

            Case ENReporteTipo.FACTURA_MediaCarta
                objrep = New R_FacturaMediaCarta
                objrep.setdatasource(_ds.Tables(0))
                Dim fechaliteral = obtenerfechaliteral(_fecha, _ds2.Tables(0).Rows(0).Item("scciu").ToString)
                objrep.setparametervalue("fecliteral", fechaliteral)
                objrep.setparametervalue("direccionpr", _ds2.Tables(0).Rows(0).Item("scdir").ToString)
                objrep.setparametervalue("literal1", _literal)
                objrep.setparametervalue("nrofactura", _numfac)
                objrep.setparametervalue("nroautoriz", _autorizacion)
                objrep.setparametervalue("enombre", _ds2.Tables(0).Rows(0).Item("scneg").ToString)
                objrep.setparametervalue("ecasamatriz", _ds2.Tables(0).Rows(0).Item("scsuc").ToString)
                objrep.setparametervalue("eciudadpais", _ds2.Tables(0).Rows(0).Item("scpai").ToString)
                objrep.setparametervalue("esfc", "sfc " + _ds1.Tables(0).Rows(0).Item("sbsfc").ToString)
                objrep.setparametervalue("enit", _ds2.Tables(0).Rows(0).Item("scnit").ToString)
                objrep.setparametervalue("eactividad", _ds2.Tables(0).Rows(0).Item("scact").ToString)
                objrep.setparametervalue("tipo", "original")
                objrep.setparametervalue("logo", gb_UbiLogo)
                objrep.setparametervalue("nota2", _ds1.Tables(0).Rows(0).Item("sbnota").ToString)
                objrep.setparametervalue("eley", _ds1.Tables(0).Rows(0).Item("sbnota2").ToString)
            Case ENReporteTipo.FACTURA_Carta
                objrep = New R_FacturaCarta
                objrep.setdatasource(_ds.Tables(0))
                objrep.setparametervalue("hora", _hora)
                objrep.setparametervalue("direccionpr", _ds2.Tables(0).Rows(0).Item("scdir").ToString)
                objrep.setparametervalue("telefonopr", _ds2.Tables(0).Rows(0).Item("sctelf").ToString)
                objrep.setparametervalue("literal1", _literal)
                objrep.setparametervalue("literal2", " ")
                objrep.setparametervalue("literal3", " ")
                objrep.setparametervalue("nrofactura", _numfac)
                objrep.setparametervalue("nroautoriz", _autorizacion)
                objrep.setparametervalue("enombre", _ds2.Tables(0).Rows(0).Item("scneg").ToString) '?
                objrep.setparametervalue("ecasamatriz", _ds2.Tables(0).Rows(0).Item("scsuc").ToString)
                objrep.setparametervalue("eciudadpais", _ds2.Tables(0).Rows(0).Item("scpai").ToString)
                objrep.setparametervalue("esfc", "sfc " + _ds1.Tables(0).Rows(0).Item("sbsfc").ToString)
                objrep.setparametervalue("enit", _ds2.Tables(0).Rows(0).Item("scnit").ToString)
                objrep.setparametervalue("eactividad", _ds2.Tables(0).Rows(0).Item("scact").ToString)
                objrep.setparametervalue("eduenho", _ds2.Tables(0).Rows(0).Item("scnom").ToString)
                objrep.setparametervalue("esms", "''" + _ds1.Tables(0).Rows(0).Item("sbnota").ToString + "''")
                objrep.setparametervalue("esms2", "''" + _ds1.Tables(0).Rows(0).Item("sbnota2").ToString + "''")

                objrep.setparametervalue("urlimagelogo", gb_UbiLogo)
                objrep.setparametervalue("urlimagemarcaagua", gs_CarpetaRaiz + "\marcaaguafactura.jpg")
                If tipofactura = 1 Then
                    objrep.setparametervalue("tipo", "original")
                Else
                    objrep.setparametervalue("tipo", "copia")
                End If

        End Select
        Dim _ds3 As DataSet = L_ObtenerRutaImpresora("1") ' datos de impresion de facturación
        If (_ds3.Tables(0).Rows(0).Item("cbvp")) Then 'vista previa de la ventana de vizualización 1 = true 0 = false
            P_Global.Visualizador = New Visualizador
            P_Global.Visualizador.CrGeneral.ReportSource = objrep
            P_Global.Visualizador.ShowDialog()
            P_Global.Visualizador.BringToFront()
        Else
            Dim pd As New PrintDocument()
            Dim instance As New Printing.PrinterSettings
            Dim impresosapredt As String = instance.PrinterName
            pd.PrinterSettings.PrinterName = impresosapredt

            If (Not pd.PrinterSettings.IsValid) Then
                ToastNotification.Show(Me, "la impresora ".ToUpper + impresosapredt + Chr(13) + "no existe".ToUpper,
                                       My.Resources.WARNING, 5000,
                                       eToastGlowColor.Blue, eToastPosition.BottomRight)
            Else
                objrep.printoptions.printername = _ds3.Tables(0).Rows(0).Item("cbrut").ToString '"epson tm-t20ii receipt5 (1)"
                objrep.printtoprinter(1, True, 0, 0)
                'crystalreportdocument.printoptions.printername = "your printer name"
                'objrep.printticket(impresosapredt)
            End If
        End If
        If (grabarpdf) Then
            'copia de factura en pdf
            If (Not Directory.Exists(gs_CarpetaRaiz + "\facturas")) Then
                Directory.CreateDirectory(gs_CarpetaRaiz + "\facturas")
            End If
            objrep.exporttodisk(ExportFormatType.PortableDocFormat, gs_CarpetaRaiz + "\facturas\" + CStr(_numfac) + "_" + CStr(_autorizacion) + ".pdf")
            L_Actualiza_Dosificacion(_numidosif, _numfac, numi)
        End If
    End Sub

    Private Function obtenerfechaliteral(fecliteral As String, ciudad As String) As String
        Dim dia, mes, ano As Integer
        Dim mesl As String
        dia = Microsoft.VisualBasic.Left(fecliteral, 2)
        mes = Microsoft.VisualBasic.Mid(fecliteral, 4, 2)
        ano = Microsoft.VisualBasic.Mid(fecliteral, 7, 4)
        If mes = 1 Then
            mesl = "enero"
        End If
        If mes = 2 Then
            mesl = "febrero"
        End If
        If mes = 3 Then
            mesl = "marzo"
        End If
        If mes = 4 Then
            mesl = "abril"
        End If
        If mes = 5 Then
            mesl = "mayo"
        End If
        If mes = 6 Then
            mesl = "junio"
        End If
        If mes = 7 Then
            mesl = "julio"
        End If
        If mes = 8 Then
            mesl = "agosto"
        End If
        If mes = 9 Then
            mesl = "septiembre"
        End If
        If mes = 10 Then
            mesl = "octubre"
        End If
        If mes = 11 Then
            mesl = "noviembre"
        End If
        If mes = 12 Then
            mesl = "diciembre"
        End If
        fecliteral = ciudad + ", " + dia.ToString + " de " + mesl + " del " + ano.ToString
        Return fecliteral
    End Function


    Public Function p_fnimagetobytearray(ByVal imagein As Image) As Byte()
        Dim ms As New System.IO.MemoryStream()
        imagein.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
        Return ms.ToArray()
    End Function


    Private Function p_fnvalidarfacturavigente() As Boolean
        Dim est As String = L_fnObtenerDatoTabla("tfv001", "fvaest", "fvanumi=" + tbCodigo.Text.Trim)
        Return (est.Equals("true"))
    End Function


    Private Sub p_generarreporte(numi As String)
        Dim dt As DataTable = L_fnVentaNotaDeVenta(numi)
        If (gb_DetalleProducto) Then
            ponerdescripcionproducto(dt)
        End If
        'dim total as decimal = dt.compute("sum(total)", "")
        Dim total As Decimal = Convert.ToDecimal(tbTotalBs.Text)
        Dim totald As Double = (total / 6.96)
        Dim fechaven As String = dt.Rows(0).Item("fechaventa")
        If Not IsNothing(P_Global.Visualizador) Then
            P_Global.Visualizador.Close()
        End If
        Dim parteentera As Long
        Dim partedecimal As Decimal
        Dim partedecimal2 As Decimal
        parteentera = Int(total)
        partedecimal = total - Math.Truncate(total)
        partedecimal2 = CDbl(partedecimal) * 100


        Dim li As String = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(parteentera)) + " " +
        IIf(partedecimal2.ToString.Equals("0"), "00", partedecimal2.ToString) + "/100 bolivianos"

        parteentera = Int(totald)
        partedecimal = totald - Math.Truncate(totald)
        partedecimal2 = CDbl(partedecimal) * 100

        Dim lid As String = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(parteentera)) + " " +
        IIf(partedecimal2.ToString.Equals("0"), "00", partedecimal2.ToString) + "/100 dolares"
        Dim _hora As String = Now.Hour.ToString + ":" + Now.Minute.ToString
        Dim _ds2 = L_Reporte_Factura_Cia("2")
        Dim dt2 As DataTable = L_fnNameReporte()
        Dim paremp1 As String = ""
        Dim paremp2 As String = ""
        Dim paremp3 As String = ""
        Dim paremp4 As String = ""
        If (dt2.Rows.Count > 0) Then
            paremp1 = dt2.Rows(0).Item("empresa1").ToString
            paremp2 = dt2.Rows(0).Item("empresa2").ToString
            paremp3 = dt2.Rows(0).Item("empresa3").ToString
            paremp4 = dt2.Rows(0).Item("empresa4").ToString
        End If

        Dim _ds3 = L_ObtenerRutaImpresora("2") ' datos de impresion de facturación
        If (_ds3.Tables(0).Rows(0).Item("cbvp")) Then 'vista previa de la ventana de vizualización 1 = true 0 = false
            P_Global.Visualizador = New Visualizador 'comentar
        End If
        Dim _fechaact As String
        Dim _fechapar As String
        Dim _fecha() As String
        Dim _meses() As String = {"enero", "febrero", "marzo", "abril", "mayo", "junio", "julio", "agosto", "septiembre", "octubre", "noviembre", "diciembre"}
        _fechaact = fechaven
        _fecha = Split(_fechaact, "-")
        _fechapar = "cochabamba, " + _fecha(0).Trim + " de " + _meses(_fecha(1) - 1).Trim + " del " + _fecha(2).Trim
        Dim objrep As Object = Nothing
        Dim empresaid = ObtenerEmpresaHabilitada()
        Dim empresahabilitada As DataTable = ObtenerEmpresaTipoReporte(empresaid, Convert.ToInt32(ENReporte.NOTAVENTA))
        For Each fila As DataRow In empresahabilitada.Rows
            Select Case fila.Item("tiporeporte").ToString
                Case ENReporteTipo.NOTAVENTA_Carta
                    objrep = New R_NotaVenta_Carta
                    setparametrosnotaventa(dt, total, li, _hora, _ds2, _ds3, fila.Item("tiporeporte").ToString, objrep)
                Case ENReporteTipo.NOTAVENTA_Ticket
                    objrep = New R_NotaVenta_7_5X100
                    setparametrosnotaventa(dt, total, li, _hora, _ds2, _ds3, fila.Item("tiporeporte").ToString, objrep)
            End Select
        Next
    End Sub

    Private Sub setparametrosnotaventa(dt As DataTable, total As Decimal, li As String, _hora As String, _ds2 As DataSet, _ds3 As DataSet, tiporeporte As String, objrep As Object)

        Select Case tiporeporte
            Case ENReporteTipo.NOTAVENTA_Carta
                objrep.setdatasource(dt)
                objrep.setparametervalue("literal", li)
                objrep.setparametervalue("logo", gb_UbiLogo)
                objrep.setparametervalue("notaadicional1", gb_NotaAdicional)
                objrep.setparametervalue("descuento", tbMdesc.Value)
                objrep.setparametervalue("total", total)
            Case ENReporteTipo.NOTAVENTA_Ticket
                objrep.setdatasource(dt)
                objrep.setparametervalue("ecasamatriz", _ds2.Tables(0).Rows(0).Item("scsuc").ToString)
                objrep.setparametervalue("eciudadpais", _ds2.Tables(0).Rows(0).Item("scpai").ToString)
                objrep.setparametervalue("eduenho", _ds2.Tables(0).Rows(0).Item("scnom").ToString) '?
                objrep.setparametervalue("direccionpr", _ds2.Tables(0).Rows(0).Item("scdir").ToString)
                objrep.setparametervalue("hora", _hora)
                objrep.setparametervalue("enombre", _ds2.Tables(0).Rows(0).Item("scneg").ToString) '?
                objrep.setparametervalue("literal1", li)
        End Select
        If (_ds3.Tables(0).Rows(0).Item("cbvp")) Then 'vista previa de la ventana de vizualización 1 = true 0 = false
            P_Global.Visualizador.CrGeneral.ReportSource = objrep 'comentar
            P_Global.Visualizador.ShowDialog() 'comentar
            P_Global.Visualizador.BringToFront() 'comentar
        Else
            Dim pd As New PrintDocument()
            pd.PrinterSettings.PrinterName = _ds3.Tables(0).Rows(0).Item("cbrut").ToString
            If (Not pd.PrinterSettings.IsValid) Then
                ToastNotification.Show(Me, "la impresora ".ToUpper + _ds3.Tables(0).Rows(0).Item("cbrut").ToString + Chr(13) + "no existe".ToUpper,
                                       My.Resources.WARNING, 5 * 1000,
                                       eToastGlowColor.Blue, eToastPosition.BottomRight)
            Else
                objrep.printoptions.printername = _ds3.Tables(0).Rows(0).Item("cbrut").ToString
                objrep.printtoprinter(1, True, 0, 0)
            End If
        End If
    End Sub

    Private Sub ponerdescripcionproducto(ByRef dt As DataTable)
        For Each fila As DataRow In dt.Rows
            Dim numi As Integer = fila.Item("codproducto")
            Dim dtdp As DataTable = L_fnDetalleProducto(numi)
            Dim des As String = fila.Item("producto") + vbNewLine + vbNewLine
            For Each fila2 As DataRow In dtdp.Rows
                des = des + fila2.Item("yfadesc").ToString + vbNewLine
            Next
            fila.Item("producto") = des
        Next
    End Sub

    Private Sub p_generarreportefactura(numi As String)
        Dim dt As DataTable = L_fnVentaFactura(numi)
        Dim total As Double = dt.Compute("sum(total)", "")

        Dim parteentera As Long
        Dim partedecimal As Double
        parteentera = Int(total)
        partedecimal = total - parteentera
        Dim li As String = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(parteentera)) + " con " +
        IIf(partedecimal.ToString.Equals("0"), "00", partedecimal.ToString) + "/100 bolivianos"


        Dim objrep As New R_FacturaFarmacia
        '' generarnro(_dt)
        ''objrep.setdatasource(dt1kardex)
        'imprimir
        If PrintDialog1.ShowDialog = DialogResult.OK Then
            objrep.SetDataSource(dt)
            objrep.SetParameterValue("totalescrito", li)
            objrep.SetParameterValue("total", total)
            objrep.SetParameterValue("cliente", TbNomCliente.Text + " " + tbTelf.Text)
            objrep.PrintOptions.PrinterName = PrintDialog1.PrinterSettings.PrinterName

            objrep.PrintToPrinter(1, False, 1, 1)
            objrep.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
        End If



    End Sub

    Sub _prcargarproductodelaproforma(numiproforma As Integer)
        Dim dt As DataTable = L_fnListarProductoProforma(numiproforma)

        '        a.pbnumi ,a.pbtp1numi ,a.pbty5prod ,producto ,a.pbcmin ,a.pbumin ,umin .ycdes3 as unidad,
        'a.pbpbas ,a.pbptot,a.pbporc,a.pbdesc ,a.pbtotdesc,
        'stock, pcosto
        If (dt.Rows.Count > 0) Then
            CType(grdetalle.DataSource, DataTable).Rows.Clear()
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim numiproducto As Integer = dt.Rows(i).Item("pbty5prod")
                Dim nameproducto As String = dt.Rows(i).Item("producto")
                Dim lote As String = ""
                Dim fechavenc As Date = Now.Date
                Dim cant As Double = dt.Rows(i).Item("pbcmin")
                Dim iccven As Double = 0
                _prpedirlotesproducto(lote, fechavenc, iccven, numiproducto, nameproducto, cant)
                _pradddetalleventa()
                grdetalle.Row = grdetalle.RowCount - 1
                If (lote <> String.Empty) Then
                    If (cant <= iccven) Then

                        grdetalle.SetValue("tbptot", dt.Rows(i).Item("pbptot"))
                        grdetalle.SetValue("tbtotdesc", dt.Rows(i).Item("pbtotdesc"))
                        grdetalle.SetValue("tbdesc", dt.Rows(i).Item("pbdesc"))
                        grdetalle.SetValue("tbcmin", cant)
                        grdetalle.SetValue("tbptot2", dt.Rows(i).Item("pcosto") * cant)

                    Else
                        Dim tot As Double = dt.Rows(i).Item("pbpbas") * iccven
                        grdetalle.SetValue("tbptot", tot)
                        grdetalle.SetValue("tbtotdesc", tot)
                        grdetalle.SetValue("tbdesc", 0)
                        grdetalle.SetValue("tbcmin", iccven)
                        grdetalle.SetValue("tbptot2", dt.Rows(i).Item("pcosto") * iccven)
                    End If
                    grdetalle.SetValue("tbty5prod", numiproducto)
                    grdetalle.SetValue("producto", nameproducto)
                    grdetalle.SetValue("tbumin", dt.Rows(i).Item("pbumin"))
                    grdetalle.SetValue("unidad", dt.Rows(i).Item("unidad"))
                    grdetalle.SetValue("tbpbas", dt.Rows(i).Item("pbpbas"))


                    If (gb_FacturaIncluirICE) Then
                        grdetalle.SetValue("tbpcos", dt.Rows(i).Item("pcosto"))

                    Else
                        grdetalle.SetValue("tbpcos", 0)
                    End If

                    grdetalle.SetValue("tblote", lote)
                    grdetalle.SetValue("tbfechavenc", fechavenc)
                    grdetalle.SetValue("stock", iccven)

                End If

                'grdetalle.refetch()
                'grdetalle.refresh()


            Next

            grdetalle.Select()
            _prcalcularpreciototal()
        End If
    End Sub
    Public Sub _prpedirlotesproducto(ByRef lote As String, ByRef fechavenc As Date, ByRef iccven As Double, codproducto As Integer, nameproducto As String, cant As Integer)
        Dim dt As New DataTable
        dt = L_fnListarLotesPorProductoVenta(cbSucursal.Value, codproducto)  ''1=almacen
        'b.yfcdprod1 ,a.iclot ,a.icfven  ,a.iccven 
        Dim listestceldas As New List(Of Modelo.Celda)
        listestceldas.Add(New Modelo.Celda("yfcdprod1,", False, "", 150))
        listestceldas.Add(New Modelo.Celda("iclot", True, "lote", 150))
        listestceldas.Add(New Modelo.Celda("icfven", True, "fecha vencimiento", 180, "dd/mm/yyyy"))
        listestceldas.Add(New Modelo.Celda("iccven", True, "stock".ToUpper, 150, "0.00"))
        Dim ef = New Efecto
        ef.tipo = 3
        ef.dt = dt
        ef.SeleclCol = 2
        ef.listEstCeldas = listestceldas
        ef.alto = 50
        ef.ancho = 350
        ef.Context = "producto ".ToUpper + nameproducto + "  cantidad=" + Str(cant)
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        'b.yfcdprod1 ,a.iclot ,a.icfven  ,a.iccven 
        If (bandera = True) Then
            Dim row As Janus.Windows.GridEX.GridEXRow = ef.Row
            lote = row.Cells("iclot").Value
            fechavenc = row.Cells("icfven").Value
            iccven = row.Cells("iccven").Value
        End If
    End Sub
    Private Sub asignarclienteempleado()
        Try
            Dim _tabla11 As DataTable = L_fnListarClientesUsuario(gi_userNumi)
            If _tabla11.Rows.Count > 0 Then
                tbCliente.Text = _tabla11.Rows(0).Item("yddesc")
                _codcliente = _tabla11.Rows(0).Item("ydnumi") 'codigo
                tbVendedor.Text = _tabla11.Rows(0).Item("vendedor") 'codigo
                _codempleado = _tabla11.Rows(0).Item("ydnumivend") 'codigo
            Else
                Dim dt As DataTable
                dt = L_fnListarClientes()
                If dt.Rows.Count > 0 Then
                    Dim fila As DataRow() = dt.Select("ydnumi =min(ydnumi)")
                    tbCliente.Text = fila(0).ItemArray(3)
                    _codcliente = fila(0).ItemArray(0)
                    tbVendedor.Text = fila(0).ItemArray(9)
                    _codempleado = fila(0).ItemArray(8)
                End If
            End If
        Catch ex As Exception
            mostrarmensajeerror("debe asiganar un vendedor al cliente")
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
#End Region

#Region "eventos formulario"
    Private Sub f0_ventas_load(sender As Object, e As EventArgs) Handles MyBase.Load
        _iniciartodo()
    End Sub
    Private Sub btnnuevo_click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        _limpiar()
        _prhabilitar()
        asignarclienteempleado()
        btnNuevo.Enabled = False
        btnModificar.Enabled = False
        btnEliminar.Enabled = False
        btnGrabar.Enabled = True
        PanelNavegacion.Enabled = False

        SwDescuentoProveedor.Enabled = True

        tbCliente.Select()
        'tbnit.select()
    End Sub
    Private Sub btnsalir_click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _prsalir()

    End Sub

    Private Sub tbcliente_keydown(sender As Object, e As KeyEventArgs) Handles tbCliente.KeyDown
        If (_fnaccesible()) Then
            If e.KeyData = Keys.Control + Keys.Enter Then

                Dim dt As DataTable
                'dt = l_fnlistarclientesventa()
                dt = L_fnListarClientesVentaPrecioPDV()

                Dim listestceldas As New List(Of Modelo.Celda)
                listestceldas.Add(New Modelo.Celda("ydnumi,", True, "id", 50))
                listestceldas.Add(New Modelo.Celda("ydcod", True, "cod. cli", 100))
                listestceldas.Add(New Modelo.Celda("ydrazonsocial", True, "razón social", 180))
                listestceldas.Add(New Modelo.Celda("yddesc", True, "nombre", 280))
                listestceldas.Add(New Modelo.Celda("yddctnum", False, "n. documento".ToUpper, 150))
                listestceldas.Add(New Modelo.Celda("yddirec", True, "dirección", 220))
                listestceldas.Add(New Modelo.Celda("ydtelf1", True, "teléfono".ToUpper, 100))
                listestceldas.Add(New Modelo.Celda("ydfnac", False, "f.nacimiento".ToUpper, 150, "mm/dd,yyyy"))
                listestceldas.Add(New Modelo.Celda("ydnumivend,", False, "id", 50))
                listestceldas.Add(New Modelo.Celda("vendedor,", False, "id", 50))
                listestceldas.Add(New Modelo.Celda("yddias", False, "cred", 50))
                listestceldas.Add(New Modelo.Celda("ydnomfac", False, "nombre factura", 50))
                listestceldas.Add(New Modelo.Celda("ydnit", True, "nit/ci", 90))
                listestceldas.Add(New Modelo.Celda("email", False, "email", 50))
                listestceldas.Add(New Modelo.Celda("yddct", False, "tipodoc", 50))
                listestceldas.Add(New Modelo.Celda("ydobs", False, "complemento", 50))

                Dim ef = New Efecto
                ef.tipo = 3
                ef.dt = dt
                ef.SeleclCol = 3
                ef.listEstCeldas = listestceldas
                ef.alto = 50
                ef.ancho = 200
                ef.Context = "seleccione cliente".ToUpper
                ef.ShowDialog()

                Dim bandera As Boolean = False
                bandera = ef.band
                If (bandera = True) Then
                    Dim row As Janus.Windows.GridEX.GridEXRow = ef.Row

                    _codcliente = row.Cells("ydnumi").Value
                    tbCliente.Text = row.Cells("yddesc").Value
                    _dias = row.Cells("yddias").Value
                    TbNomCliente.Text = row.Cells("ydnomfac").Value
                    tbContacto.Text = row.Cells("email").Value

                    ''tbcel.text = row.cells("ydtelf1").value.tostring

                    Dim numivendedor As Integer = IIf(IsDBNull(row.Cells("ydnumivend").Value), 0, row.Cells("ydnumivend").Value)
                    If (numivendedor > 0) Then
                        tbVendedor.Text = row.Cells("vendedor").Value
                        _codempleado = row.Cells("ydnumivend").Value

                        grdetalle.Select()
                        table_producto = Nothing
                    Else
                        tbVendedor.Clear()
                        _codempleado = 0
                        tbVendedor.Focus()
                        table_producto = Nothing

                    End If
                End If
            End If
        End If
    End Sub
    Private Sub tbvendedor_keydown(sender As Object, e As KeyEventArgs) Handles tbVendedor.KeyDown

        If (_fnaccesible()) Then
            If e.KeyData = Keys.Control + Keys.Enter Then

                Dim dt As DataTable

                dt = L_fnListarEmpleado()
                '              a.ydnumi, a.ydcod, a.yddesc, a.yddctnum, a.yddirec
                ',a.ydtelf1 ,a.ydfnac 

                Dim listestceldas As New List(Of Modelo.Celda)
                listestceldas.Add(New Modelo.Celda("ydnumi,", False, "id", 50))
                listestceldas.Add(New Modelo.Celda("ydcod", True, "id", 50))
                listestceldas.Add(New Modelo.Celda("yddesc", True, "nombre", 280))
                listestceldas.Add(New Modelo.Celda("yddctnum", True, "n. documento".ToUpper, 150))
                listestceldas.Add(New Modelo.Celda("yddirec", True, "direccion", 220))
                listestceldas.Add(New Modelo.Celda("ydtelf1", True, "telefono".ToUpper, 200))
                listestceldas.Add(New Modelo.Celda("ydfnac", True, "f.nacimiento".ToUpper, 150, "mm/dd,yyyy"))
                Dim ef = New Efecto
                ef.tipo = 3
                ef.dt = dt
                ef.SeleclCol = 1
                ef.listEstCeldas = listestceldas
                ef.alto = 50
                ef.ancho = 350
                ef.Context = "seleccione vendedor".ToUpper
                ef.ShowDialog()
                Dim bandera As Boolean = False
                bandera = ef.band
                If (bandera = True) Then
                    Dim row As Janus.Windows.GridEX.GridEXRow = ef.Row
                    If (IsNothing(row)) Then
                        tbVendedor.Focus()
                        Return

                    End If
                    _codempleado = row.Cells("ydnumi").Value
                    tbVendedor.Text = row.Cells("yddesc").Value
                    grdetalle.Select()

                End If

            End If

        End If
    End Sub


    Private Sub grdetalle_editingcell(sender As Object, e As EditingCellEventArgs) Handles grdetalle.EditingCell
        If (_fnaccesible()) Then
            'habilitar solo las columnas de precio, %, monto y observación
            ''if (e.column.index = grdetalle.roottable.columns("yfcbarra").index or
            'if (e.column.index = grdetalle.roottable.columns("tbcmin").index) then
            '    ''e.column.index = grdetalle.roottable.columns("tbdesc").index
            '    e.cancel = false
            'else
            '    e.cancel = true
            'end if

            If gs_NroCaja = 7 Then
                If (e.Column.Index = grdetalle.RootTable.Columns("tbcmin").Index Or
                    e.Column.Index = grdetalle.RootTable.Columns("tbpbas").Index Or
                    e.Column.Index = grdetalle.RootTable.Columns("yfcbarra").Index) Then
                    e.Cancel = False
                Else
                    e.Cancel = True
                End If
            Else
                If (e.Column.Index = grdetalle.RootTable.Columns("tbcmin").Index Or
                    e.Column.Index = grdetalle.RootTable.Columns("yfcbarra").Index) Then
                    e.Cancel = False
                Else
                    e.Cancel = True
                End If
            End If
        Else
            e.Cancel = True
        End If

    End Sub
    Private Sub grdetalle_keydown(sender As Object, e As KeyEventArgs) Handles grdetalle.KeyDown
        '        if (not _fnaccesible()) then
        '            return
        '        end if
        '        if (e.keydata = keys.enter) then
        '            dim f, c as integer
        '            c = grdetalle.col
        '            f = grdetalle.row

        '            if (grdetalle.col = grdetalle.roottable.columns("tbcmin").index) then
        '                if (grdetalle.getvalue("producto") <> string.empty) then
        '                    _pradddetalleventa()
        '                    _habilitarproductos()
        '                else
        '                    toastnotification.show(me, "seleccione un producto por favor", my.resources.warning, 3000, etoastglowcolor.red, etoastposition.topcenter)
        '                end if

        '            end if
        '            if (grdetalle.col = grdetalle.roottable.columns("producto").index) then
        '                if (grdetalle.getvalue("producto") <> string.empty) then
        '                    _pradddetalleventa()
        '                    _habilitarproductos()
        '                else
        '                    toastnotification.show(me, "seleccione un producto por favor", my.resources.warning, 3000, etoastglowcolor.red, etoastposition.topcenter)
        '                end if

        '            end if
        '            if (grdetalle.col = grdetalle.roottable.columns("yfcbarra").index) then
        '                if (grdetalle.getvalue("yfcbarra").tostring().trim() <> string.empty) then
        '                    cargarproductos()
        '                    if (grdetalle.row = grdetalle.rowcount - 1) then
        '                        if (existeproducto(grdetalle.getvalue("yfcbarra").tostring)) then
        '                            if (not verificarexistenciaunica(grdetalle.getvalue("yfcbarra").tostring)) then
        '                                ponerproducto(grdetalle.getvalue("yfcbarra").tostring)
        '                                _pradddetalleventa()
        '                            else
        '                                sumarcantidad(grdetalle.getvalue("yfcbarra").tostring)
        '                            end if
        '                        else
        '                            grdetalle.datachanged = false
        '                            toastnotification.show(me, "el código de barra del producto no existe", my.resources.warning, 3000, etoastglowcolor.red, etoastposition.topcenter)
        '                        end if
        '                    else
        '                        grdetalle.datachanged = false
        '                        grdetalle.row = grdetalle.rowcount - 1
        '                        grdetalle.col = grdetalle.roottable.columns("yfcbarra").index
        '                        toastnotification.show(me, "el cursor debe situarse en la ultima fila", my.resources.warning, 3000, etoastglowcolor.red, etoastposition.topcenter)
        '                    end if
        '                else
        '                    toastnotification.show(me, "el código de barra no puede quedar vacio", my.resources.warning, 3000, etoastglowcolor.red, etoastposition.topcenter)
        '                end if

        '            end if
        '            'opcion para cargar la grilla con el codigo de barra
        '            'if (grdetalle.col = grdetalle.roottable.columns("yfcbarra").index) then

        '            '    if (grdetalle.getvalue("yfcbarra") <> string.empty) then
        '            '        _buscarregistro(grdetalle.getvalue("yfcbarra"))


        '            '        '_pradddetalleventa()
        '            '        '_habilitarproductos()
        '            '        ' msgbox("hola de la grilla" + grdetalle.getvalue("yfcbarra") + t.container.tostring)
        '            '        'ojo
        '            '    else
        '            '        toastnotification.show(me, "seleccione un producto por favor", my.resources.warning, 3000, etoastglowcolor.red, etoastposition.topcenter)
        '            '    end if

        '            'end if
        'salirif:
        '        end if

        '        if (e.keydata = keys.control + keys.enter and grdetalle.row >= 0 and
        '            grdetalle.col = grdetalle.roottable.columns("producto").index) then
        '            dim indexfil as integer = grdetalle.row
        '            dim indexcol as integer = grdetalle.col
        '            _habilitarproductos()

        '        end if
        '        if (e.keydata = keys.escape and grdetalle.row >= 0) then

        '            _preliminarfila()


        '        end if
        '        if (e.keydata = keys.control + keys.s) then
        '            tbmontobs.select()
        '        end if

        Try
            If (Not _fnaccesible()) Then
                Return
            End If

            If (e.KeyData = Keys.Enter) Then
                Dim f, c As Integer
                c = grdetalle.Col
                f = grdetalle.Row

                If (grdetalle.Col = grdetalle.RootTable.Columns("tbcmin").Index) Then
                    If (grdetalle.GetValue("producto") <> String.Empty) Then
                        _pradddetalleventa()
                        _habilitarproductos()
                    Else
                        ToastNotification.Show(Me, "seleccione un producto por favor", My.Resources.WARNING, 3000, eToastGlowColor.Red, eToastPosition.TopCenter)
                    End If

                End If
                If (grdetalle.Col = grdetalle.RootTable.Columns("producto").Index) Then
                    If (grdetalle.GetValue("producto") <> String.Empty) Then
                        _pradddetalleventa()
                        _habilitarproductos()
                    Else
                        ToastNotification.Show(Me, "seleccione un producto por favor", My.Resources.WARNING, 3000, eToastGlowColor.Red, eToastPosition.TopCenter)
                    End If

                End If
                If (grdetalle.Col = grdetalle.RootTable.Columns("yfcbarra").Index) Then
                    If (grdetalle.GetValue("yfcbarra").ToString().Trim() <> String.Empty) Then
                        cargarproductos()
                        If (grdetalle.Row = grdetalle.RowCount - 1) Then
                            Dim codigobarrascompleto = grdetalle.GetValue("yfcbarra").ToString
                            Dim dosprimerosdigitos As String = Mid(codigobarrascompleto, 1, 2)

                            If dosprimerosdigitos = "20" Then
                                Dim codigobarrasproducto As Integer
                                Dim pesogramos As Decimal

                                ''cuando el codigo de barras tenga 7 digitos ejem: 2000001
                                codigobarrasproducto = Mid(codigobarrascompleto, 1, 7)
                                If (existeproducto(codigobarrasproducto)) Then
                                    pesogramos = Mid(codigobarrascompleto, 8, 5)
                                    Dim pesokg As Decimal = pesogramos / 1000
                                    Dim resultado As Boolean = False

                                    If (Not verificarexistenciaunica(codigobarrasproducto)) Then

                                        ponerproducto3(codigobarrasproducto, pesokg, -1, False, resultado)
                                        If resultado Then
                                            _pradddetalleventa()
                                        End If

                                    Else

                                        ponerproducto3(codigobarrasproducto, pesokg, 0, True, resultado)

                                    End If
                                Else
                                    grdetalle.DataChanged = False
                                    ToastNotification.Show(Me, "el código de barra del producto no existe o no tiene stock".ToUpper, My.Resources.WARNING, 3000, eToastGlowColor.Red, eToastPosition.TopCenter)
                                End If


                                ''antes estaba este código pero ya no es funcional
                                'dim primerdigito as string = mid(codigobarrascompleto, 1, 1)
                                'if primerdigito = "2" then
                                '    dim codigobarrasproducto as integer
                                '    dim totalentero, totaldecimal, total2, total as decimal
                                '    codigobarrasproducto = mid(codigobarrascompleto, 1, 6)
                                '    'cuando el cod barra tenga 6 digitos  ejem: 200001
                                '    if (existeproducto(codigobarrasproducto)) then

                                '        totalentero = mid(codigobarrascompleto, 7, 4)
                                '        totaldecimal = mid(codigobarrascompleto, 11, 2)
                                '        total2 = cdbl(totalentero).tostring() + "." + cdbl(totaldecimal).tostring()
                                '        total = cdbl(total2)
                                '        if (not verificarexistenciaunica(codigobarrasproducto)) then
                                '            ponerproducto2(codigobarrasproducto, total, -1)
                                '            _pradddetalleventa()
                                '        else
                                '            ponerproducto2(codigobarrasproducto, total, grdetalle.rowcount - 1)
                                '            _pradddetalleventa()
                                '        end if

                                '    else
                                '        ''cuando el codigo de barras tenga 7 digitos ejem: 2000001
                                '        codigobarrasproducto = mid(codigobarrascompleto, 1, 7)
                                '        if (existeproducto(codigobarrasproducto)) then
                                '            totalentero = mid(codigobarrascompleto, 8, 3)
                                '            totaldecimal = mid(codigobarrascompleto, 11, 2)
                                '            total2 = cdbl(totalentero).tostring() + "." + cdbl(totaldecimal).tostring()
                                '            total = cdbl(total2)
                                '            if (not verificarexistenciaunica(codigobarrasproducto)) then
                                '                ponerproducto2(codigobarrasproducto, total, -1)
                                '                _pradddetalleventa()
                                '            else
                                '                ponerproducto2(codigobarrasproducto, total, grdetalle.rowcount - 1)
                                '                _pradddetalleventa()
                                '            end if
                                '        else
                                '            grdetalle.datachanged = false
                                '            toastnotification.show(me, "el código de barra del producto no existe", my.resources.warning, 3000, etoastglowcolor.red, etoastposition.topcenter)
                                '        end if
                                '    end if
                            Else

                                If (existeproducto(grdetalle.GetValue("yfcbarra").ToString)) Then
                                    If (Not verificarexistenciaunica(grdetalle.GetValue("yfcbarra").ToString)) Then
                                        Dim resultado As Boolean = False
                                        ponerproducto(grdetalle.GetValue("yfcbarra").ToString, resultado)
                                        If resultado Then
                                            _pradddetalleventa()
                                        End If
                                    Else
                                        'if (grdetalle.getvalue("producto").tostring <> string.empty) then
                                        sumarcantidad(grdetalle.GetValue("yfcbarra").ToString)
                                        'else
                                        '    dim img as bitmap = new bitmap(my.resources.mensaje, 50, 50)
                                        '    toastnotification.show(me, "el producto: no cuenta con stock disponible", img, 5000, etoastglowcolor.red, etoastposition.bottomcenter)
                                        '    filaselectlote = nothing
                                        'end if
                                    End If
                                Else
                                    'grdetalle.setvalue("yfcbarra", "")
                                    grdetalle.DataChanged = False
                                    ToastNotification.Show(Me, "el código de barra del producto no existe, no se tiene stock o no tiene precio, verifique!!!".ToUpper, My.Resources.WARNING, 3700, eToastGlowColor.Red, eToastPosition.TopCenter)
                                End If
                            End If
                        Else
                            grdetalle.DataChanged = False
                            grdetalle.Row = grdetalle.RowCount - 1
                            grdetalle.Col = grdetalle.RootTable.Columns("yfcbarra").Index
                            ToastNotification.Show(Me, "el cursor debe situarse en la ultima fila", My.Resources.WARNING, 3000, eToastGlowColor.Red, eToastPosition.TopCenter)
                        End If
                    Else
                        ToastNotification.Show(Me, "el código de barra no puede quedar vacio", My.Resources.WARNING, 3000, eToastGlowColor.Red, eToastPosition.TopCenter)
                    End If
                End If


                'opcion para cargar la grilla con el codigo de barra
                'if (grdetalle.col = grdetalle.roottable.columns("yfcbarra").index) then

                '    if (grdetalle.getvalue("yfcbarra") <> string.empty) then
                '        _buscarregistro(grdetalle.getvalue("yfcbarra"))


                '        '_pradddetalleventa()
                '        '_habilitarproductos()
                '        ' msgbox("hola de la grilla" + grdetalle.getvalue("yfcbarra") + t.container.tostring)
                '        'ojo
                '    else
                '        toastnotification.show(me, "seleccione un producto por favor", my.resources.warning, 3000, etoastglowcolor.red, etoastposition.topcenter)
                '    end if

                'end if
salirif:
            End If

            If (e.KeyData = Keys.Control + Keys.Enter And grdetalle.Row >= 0 And
                grdetalle.Col = grdetalle.RootTable.Columns("producto").Index) Then
                Dim indexfil As Integer = grdetalle.Row
                Dim indexcol As Integer = grdetalle.Col
                _habilitarproductos()

            End If
            If (e.KeyData = Keys.Escape And grdetalle.Row >= 0) Then

                _preliminarfila()
                calculodescuentoxproveedor()

            End If

        Catch ex As Exception
            mostrarmensajeerror(ex.Message)
        End Try
    End Sub

    Private Sub ponerproducto2(codigo As String, total As Decimal, pos As Integer)
        Try
            grdetalle.DataChanged = True
            CType(grdetalle.DataSource, DataTable).AcceptChanges()
            Dim fila As DataRow() = table_producto.Select("yfcbarra='" + codigo.Trim + "'", "")
            If (fila.Count > 0) Then
                If pos = -1 Then
                    If (grdetalle.GetValue("tbty5prod") <= 0) Then
                        _pradddetalleventa()
                        grdetalle.Row = grdetalle.RowCount - 1
                    End If
                    _fnobtenerfiladetalle(pos, grdetalle.GetValue("tbnumi"))
                End If
                Dim cantidad = Format(total / CDbl(fila(0).ItemArray(15)), "0.00")
                Dim precio = fila(0).ItemArray(15)
                total = cantidad * precio
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbty5prod") = fila(0).ItemArray(0)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("codigo") = fila(0).ItemArray(1)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("yfcbarra") = fila(0).ItemArray(2)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("producto") = fila(0).ItemArray(3)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbumin") = fila(0).ItemArray(13)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("unidad") = fila(0).ItemArray(14)
                'tbcmin
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpbas") = precio
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot") = total
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = total
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbcmin") = cantidad
                If (gb_FacturaIncluirICE) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos") = fila(0).ItemArray(17)
                Else
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos") = 0
                End If
                ''modif
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos") = fila(0).ItemArray(16)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot2") = fila(0).ItemArray(16) * cantidad
                '
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("stock") = fila(0).ItemArray(17) - cantidad
                'ctype(grdetalle.datasource, datatable).rows(pos).item("tblote") = grproductos.getvalue("iclot")
                'ctype(grdetalle.datasource, datatable).rows(pos).item("tbfechavenc") = grproductos.getvalue("icfven")
                'ctype(grdetalle.datasource, datatable).rows(pos).item("stock") = grproductos.getvalue("iccven")
                _prcalcularpreciototal()
            End If
        Catch ex As Exception
            Throw New Exception
        End Try

    End Sub

    Private Sub ponerproducto3(codigo As String, peso As Decimal, pos As Integer, sumar As Boolean, ByRef resultado As Boolean)
        Try
            Dim cantidad, precio, total As Decimal
            grdetalle.Row = grdetalle.RowCount - 1
            Dim fila As DataRow() = table_producto.Select("yfcbarra='" + codigo.Trim + "'", "")
            Dim productoexistente As DataRow() = CType(grdetalle.DataSource, DataTable).Select("yfcbarra='" + codigo.Trim + "' and estado=0", "")

            If (fila.Count > 0) Then

                If sumar = False Then
                    cantidad = Format(peso, "0.00")
                    precio = fila(0).Item("yhprecio")
                    total = Format(cantidad * precio, "0.00")
                Else
                    cantidad = Format((productoexistente(0).Item("tbcmin") + peso), "0.00")
                    precio = fila(0).Item("yhprecio")
                    total = Format(cantidad * precio, "0.00")
                    _fnobtenerfiladetalle(pos, productoexistente(0).Item("tbnumi"))
                End If
                If cantidad <= fila(0).Item("stock") Then
                    If pos = -1 Then
                        If (grdetalle.GetValue("tbty5prod") > 0) Then
                            _pradddetalleventa()

                            grdetalle.Row = grdetalle.RowCount - 1
                        End If
                        _fnobtenerfiladetalle(pos, grdetalle.GetValue("tbnumi"))
                    End If


                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbty5prod") = fila(0).Item("yfnumi")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("codigo") = fila(0).Item("yfcprod")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("yfcbarra") = fila(0).Item("yfcbarra")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("producto") = fila(0).Item("yfcdprod1")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbumin") = fila(0).Item("yfumin")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("unidad") = fila(0).Item("unidmin")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbfamilia") = fila(0).Item("yfgr4")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpbas") = precio
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot") = total
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = total
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("ygcodsin") = fila(0).Item("ygcodsin")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("ygcodu") = fila(0).Item("ygcodu")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbcmin") = cantidad
                    If (gb_FacturaIncluirICE) Then
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos") = fila(0).Item("pcos")
                    Else
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos") = 0
                    End If

                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos") = fila(0).Item("pcos")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot2") = fila(0).Item("pcos") * cantidad
                    '
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("stock") = fila(0).Item("stock")
                    'ctype(grdetalle.datasource, datatable).rows(pos).item("tblote") = grproductos.getvalue("iclot")
                    'ctype(grdetalle.datasource, datatable).rows(pos).item("tbfechavenc") = grproductos.getvalue("icfven")
                    'ctype(grdetalle.datasource, datatable).rows(pos).item("stock") = grproductos.getvalue("iccven")

                    grdetalle.SetValue("yfcbarra", fila(0).Item("yfcbarra"))
                    _prcalcularpreciototal()
                    resultado = True

                    If sumar = True Then
                        Dim posicion As Integer = CType(grdetalle.DataSource, DataTable).Rows.Count - 1
                        CType(grdetalle.DataSource, DataTable).Rows(posicion).Item("yfcbarra") = ""
                        grdetalle.SetValue("yfcbarra", "")
                    End If

                    'ctype(grdetalle.datasource, datatable).rows(pos).item("yfcbarra") = ""


                Else
                    resultado = False
                    grdetalle.SetValue("yfcbarra", "")
                    grdetalle.SetValue("tbcmin", 0)
                    grdetalle.SetValue("tbptot", 0)
                    grdetalle.SetValue("tbptot2", 0)
                    grdetalle.DataChanged = True
                    grdetalle.Refresh()
                    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                    ToastNotification.Show(Me, "la cantidad del producto es superior a la cantidad disponible que es: ".ToUpper + fila(0).Item("stock").ToString + " , primero se debe regularizar el stock".ToUpper, img, 7500, eToastGlowColor.Red, eToastPosition.TopCenter)
                End If

            End If
        Catch ex As Exception
            Throw New Exception
        End Try

    End Sub
    Private Sub cargarproductos()
        Dim dt As DataTable
        If (g_lote = True) Then
            dt = L_fnListarProductos(cbSucursal.Value, Str(_codcliente))  ''1=almacen
            table_producto = dt.Copy
        Else
            'dt = L_fnListarProductosSinLoteUlt(cbSucursal.Value, Str(_codcliente), CType(grdetalle.DataSource, DataTable), cbcanje.value)
            dt = L_fnListarProductosSinLoteUltProforma(cbSucursal.Value, _codcliente, CType(grdetalle.DataSource, DataTable))
            table_producto = dt.Copy
        End If
    End Sub
    Private Function existeproducto(codigo As String) As Boolean
        Return (table_producto.Select("yfcbarra='" + codigo.Trim() + "'", "").Count > 0)
    End Function

    Private Function verificarexistenciaunica(codigo As String) As Boolean
        Dim cont As Integer = 0
        For Each fila As GridEXRow In grdetalle.GetRows()
            If (fila.Cells("yfcbarra").Value.ToString.Trim = codigo.Trim) Then
                cont += 1
            End If
        Next
        Return (cont >= 1)
    End Function

    Private Sub ponerproducto(codigo As String, ByRef resultado As Boolean)
        Try
            grdetalle.DataChanged = True
            CType(grdetalle.DataSource, DataTable).AcceptChanges()
            Dim fila As DataRow() = table_producto.Select("yfcbarra='" + codigo.Trim + "'", "")
            If (fila.Count > 0) Then
                'si tiene stock > 0
                If fila(0).ItemArray(20) > 0 Then
                    Dim pos As Integer = -1
                    _fnobtenerfiladetalle(pos, grdetalle.GetValue("tbnumi"))
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbty5prod") = fila(0).ItemArray(0)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("codigo") = fila(0).ItemArray(1)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("yfcbarra") = fila(0).ItemArray(2)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("producto") = fila(0).ItemArray(3)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbfamilia") = fila(0).ItemArray(14)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbumin") = fila(0).ItemArray(16)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("unidad") = fila(0).ItemArray(17)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpbas") = fila(0).ItemArray(18)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot") = fila(0).ItemArray(18)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = fila(0).ItemArray(18)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("ygcodsin") = fila(0).ItemArray(24)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("ygcodu") = fila(0).ItemArray(25)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbcmin") = 1
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbproveedorid") = fila(0).ItemArray(8)
                    'if (gb_facturaincluirice) then
                    '    ctype(grdetalle.datasource, datatable).rows(pos).item("tbpcos") = fila(0).itemarray(19)
                    'else
                    '    ctype(grdetalle.datasource, datatable).rows(pos).item("tbpcos") = 0
                    'end if
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos") = fila(0).ItemArray(19)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot2") = fila(0).ItemArray(19)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("stock") = fila(0).ItemArray(20)
                    'ctype(grdetalle.datasource, datatable).rows(pos).item("tblote") = grproductos.getvalue("iclot")
                    'ctype(grdetalle.datasource, datatable).rows(pos).item("tbfechavenc") = grproductos.getvalue("icfven")
                    'ctype(grdetalle.datasource, datatable).rows(pos).item("stock") = grproductos.getvalue("iccven")

                    'calculardescuentos(grdetalle.getvalue("tbty5prod"), 1, grdetalle.getvalue("tbpbas"), pos)
                    _prcalcularpreciototal()
                    resultado = True
                Else
                    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                    ToastNotification.Show(Me, "el producto: ".ToUpper + fila(0).ItemArray(3) + " no cuenta con stock disponible", img, 5000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                    filaselectlote = Nothing
                    resultado = False
                End If
            End If
        Catch ex As Exception
            mostrarmensajeerror(ex.Message)
        End Try
    End Sub

    Private Sub sumarcantidad(codigo As String)
        Try
            Dim fila As DataRow() = CType(grdetalle.DataSource, DataTable).Select(" estado=0 and yfcbarra='" + codigo.Trim + "'", "")
            If (fila.Count > 0) Then
                grdetalle.UpdateData()
                Dim pos1 As Integer = -1
                _fnobtenerfiladetalle(pos1, fila(0).Item("tbnumi"))
                Dim cant As Integer = fila(0).Item("tbcmin") + 1
                Dim stock As Integer = fila(0).Item("stock")
                If (cant > 0 And cant <= stock) Then
                    'dim lin as integer = grdetalle.getrow(pos1).cells("tbnumi").value
                    'dim pos2 as integer = -1
                    '_fnobtenerfiladetalle(pos2, lin)
                    CType(grdetalle.DataSource, DataTable).Rows(pos1).Item("tbcmin") = cant
                    CType(grdetalle.DataSource, DataTable).Rows(pos1).Item("tbptot") = Format((CType(grdetalle.DataSource, DataTable).Rows(pos1).Item("tbpbas") * cant), "#.#0")
                    CType(grdetalle.DataSource, DataTable).Rows(pos1).Item("tbtotdesc") = Format((CType(grdetalle.DataSource, DataTable).Rows(pos1).Item("tbpbas") * cant), "#.#0")
                    CType(grdetalle.DataSource, DataTable).Rows(pos1).Item("tbptot2") = Format((CType(grdetalle.DataSource, DataTable).Rows(pos1).Item("tbpcos") * cant), "#.#0")

                    grdetalle.SetValue("yfcbarra", "")
                    grdetalle.SetValue("tbcmin", 0)
                    grdetalle.SetValue("tbptot", 0)
                    grdetalle.SetValue("tbptot2", 0)
                    grdetalle.DataChanged = True
                    grdetalle.Refresh()

                    _prcalcularpreciototal()
                Else
                    grdetalle.SetValue("yfcbarra", "")
                    grdetalle.SetValue("tbcmin", 0)
                    grdetalle.SetValue("tbptot", 0)
                    grdetalle.SetValue("tbptot2", 0)
                    grdetalle.DataChanged = True
                    grdetalle.Refresh()
                    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                    ToastNotification.Show(Me, "la cantidad es superior a la cantidad disponible que es: ".ToUpper + Str(stock) + " , primero se debe regularizar el stock".ToUpper, img, 5000, eToastGlowColor.Red, eToastPosition.TopCenter)


                End If
            End If
        Catch ex As Exception
            mostrarmensajeerror(ex.Message)
        End Try
    End Sub

    Private Sub _buscarregistro(cbarra As String)
        Dim _t As DataTable
        _t = L_fnListarProductosC(cbarra)
        If _t.Rows.Count > 0 Then
            CType(grdetalle.DataSource, DataTable).Rows(0).Item("producto") = _t.Rows(0).Item("yfcdprod1")
            CType(grdetalle.DataSource, DataTable).Rows(0).Item("tbcmin") = 1
            CType(grdetalle.DataSource, DataTable).Rows(0).Item("unidad") = _t.Rows(0).Item("uni")

        Else
            MsgBox("codigo de producto no exite")
        End If
    End Sub
    Private Sub grproductos_keydown(sender As Object, e As KeyEventArgs) Handles grProductos.KeyDown
        Try
            If (Not _fnaccesible()) Then
                Return
            End If
            If (e.KeyData = Keys.Enter) Then
                Dim f, c As Integer
                c = grProductos.Col
                f = grProductos.Row
                If (f >= 0) Then

                    If (IsNothing(filaselectlote)) Then
                        ''''''''''''''''''''''''
                        If (g_lote = True) Then
                            insertarproductosconlote()
                        Else
                            insertarproductossinlote()
                        End If
                        '''''''''''''''
                    Else

                        '_fnexisteproductoconlote()
                        Dim pos As Integer = -1
                        grdetalle.Row = grdetalle.RowCount - 1
                        _fnobtenerfiladetalle(pos, grdetalle.GetValue("tbnumi"))
                        Dim numiprod = filaselectlote.Item("yfnumi")
                        Dim lote As String = grProductos.GetValue("iclot")
                        Dim fechavenc As Date = grProductos.GetValue("icfven")
                        If (Not _fnexisteproductoconlote(numiprod, lote, fechavenc)) Then
                            'b.yfcdprod1, a.iclot, a.icfven, a.iccven
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbty5prod") = filaselectlote.Item("yfnumi")
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("codigo") = filaselectlote.Item("yfcprod")
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("yfcbarra") = filaselectlote.Item("yfcbarra")
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("producto") = filaselectlote.Item("yfcdprod1")
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbfamilia") = filaselectlote.Item("yfgr4")
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbumin") = filaselectlote.Item("yfumin")
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("unidad") = filaselectlote.Item("unidmin")
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpbas") = filaselectlote.Item("yhprecio")
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot") = filaselectlote.Item("yhprecio")
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = filaselectlote.Item("yhprecio")
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("ygcodsin") = filaselectlote.Item("ygcodsin")
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("ygcodu") = filaselectlote.Item("ygcodu")
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbcmin") = 1
                            'if (gb_facturaincluirice) then
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos") = filaselectlote.Item("pcos")
                            'else
                            '    ctype(grdetalle.datasource, datatable).rows(pos).item("tbpcos") = 0
                            'end if
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot2") = filaselectlote.Item("pcos")

                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tblote") = grProductos.GetValue("iclot")
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbfechavenc") = grProductos.GetValue("icfven")
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("stock") = grProductos.GetValue("iccven")
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbproveedorid") = grProductos.GetValue("yfgr1")
                            _prcalcularpreciototal()
                            _deshabilitarproductos()
                            filaselectlote = Nothing
                        Else
                            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                            ToastNotification.Show(Me, "el producto con el lote ya existe modifique su cantidad".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                        End If



                    End If

                End If
            End If
            If e.KeyData = Keys.Escape Then
                _deshabilitarproductos()
                filaselectlote = Nothing
                'calculodescuentoxproveedor()
            End If
        Catch ex As Exception
            mostrarmensajeerror(ex.Message)
        End Try
    End Sub


    Public Sub calculardescuentostotal()

        Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)

        Dim sumadescuento As Double = 0

        For i As Integer = 0 To dt.Rows.Count - 1 Step 1

            If (dt.Rows(i).Item("estado") >= 0) Then

                sumadescuento += dt.Rows(i).Item("tbdesc")

            End If

        Next

        tbMdesc.Value = sumadescuento

    End Sub
    Public Sub calculardescuentos(productoid As Integer, cantidad As Integer, precio As Integer, posicion As Integer)
        If configuraciondescuentoesxcantidad = False Then
            Return
        End If

        Dim fila As DataRow() = dtdescuentos.Select("productoid=" + Str(productoid).ToString.Trim + "", "")

        For Each dr As DataRow In fila

            Dim cantidadinicial As Integer = dr.Item("cantidadinicial")
            Dim cantidadfinal As Integer = dr.Item("cantidadfinal")
            Dim preciodescuento As Double = dr.Item("precio")

            If (cantidad >= cantidadinicial And cantidad <= cantidadfinal) Then

                Dim subtotaldescuento As Double = cantidad * preciodescuento
                Dim descuento As Double = (cantidad * precio) - subtotaldescuento
                CType(grdetalle.DataSource, DataTable).Rows(posicion).Item("tbdesc") = descuento
                grdetalle.SetValue("tbdesc", descuento)
                CType(grdetalle.DataSource, DataTable).Rows(posicion).Item("tbtotdesc") = (grdetalle.GetValue("tbpbas") * grdetalle.GetValue("tbcmin")) - descuento

                grdetalle.SetValue("tbtotdesc", ((grdetalle.GetValue("tbpbas") * grdetalle.GetValue("tbcmin")) - descuento))
                Return

            End If

        Next

    End Sub
    Private Sub grdetalle_cellvaluechanged(sender As Object, e As ColumnActionEventArgs) Handles grdetalle.CellValueChanged
        Try
            If (e.Column.Index = grdetalle.RootTable.Columns("tbcmin").Index) Or (e.Column.Index = grdetalle.RootTable.Columns("tbpbas").Index) Then
                If (Not IsNumeric(grdetalle.GetValue("tbcmin")) Or grdetalle.GetValue("tbcmin").ToString = String.Empty) Then

                    'l_fnlistardescuentostodos
                    Dim lin As Integer = grdetalle.GetValue("tbnumi")
                    Dim pos As Integer = -1
                    Dim rowindex As Integer = grdetalle.Row
                    _fnobtenerfiladetalle(pos, lin)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbcmin") = 1
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpbas")

                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbporc") = 0
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbdesc") = 0
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpbas")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot2") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos")
                    grdetalle.SetValue("tbcmin", 1)
                    grdetalle.SetValue("tbptot", grdetalle.GetValue("tbpbas"))


                    calculardescuentos(grdetalle.GetValue("tbty5prod"), 1, grdetalle.GetValue("tbpbas"), pos)

                    p_ponertotal(rowindex)
                Else
                    Dim stock As Double = grdetalle.GetValue("stock")
                    If (grdetalle.GetValue("tbcmin") >= 0) Then
                        If (grdetalle.GetValue("tbcmin") <= stock) Then

                            Dim rowindex As Integer = grdetalle.Row
                            Dim porcdesc As Double = grdetalle.GetValue("tbporc")
                            Dim montodesc As Double = ((grdetalle.GetValue("tbpbas") * grdetalle.GetValue("tbcmin")) * (porcdesc / 100))
                            Dim lin As Integer = grdetalle.GetValue("tbnumi")
                            Dim pos As Integer = -1
                            _fnobtenerfiladetalle(pos, lin)
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot") = Format(grdetalle.GetValue("tbpbas") * grdetalle.GetValue("tbcmin"), "#.#0")

                            'ctype(grdetalle.datasource, datatable).rows(pos).item("tbdesc") = montodesc
                            'grdetalle.setvalue("tbdesc", montodesc)
                            'ctype(grdetalle.datasource, datatable).rows(pos).item("tbtotdesc") = (grdetalle.getvalue("tbpbas") * grdetalle.getvalue("tbcmin")) - montodesc

                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot2") = grdetalle.GetValue("tbpcos") * grdetalle.GetValue("tbcmin")

                            calculardescuentos(grdetalle.GetValue("tbty5prod"), grdetalle.GetValue("tbcmin"), grdetalle.GetValue("tbpbas"), pos)

                            p_ponertotal(rowindex)
                            calculodescuentoxproveedor()

                        Else
                            Dim rowindex As Integer = grdetalle.Row
                            Dim lin As Integer = grdetalle.GetValue("tbnumi")
                            Dim pos As Integer = -1
                            _fnobtenerfiladetalle(pos, lin)
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbcmin") = 1
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpbas")

                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbporc") = 0
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbdesc") = 0
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpbas")
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot2") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos")
                            grdetalle.SetValue("tbcmin", 1)
                            grdetalle.SetValue("tbptot", grdetalle.GetValue("tbpbas"))

                            calculardescuentos(grdetalle.GetValue("tbty5prod"), grdetalle.GetValue("tbcmin"), grdetalle.GetValue("tbpbas"), pos)


                            _prcalcularpreciototal()
                            p_ponertotal(rowindex)
                            calculodescuentoxproveedor()

                            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                            ToastNotification.Show(Me, "la cantidad ingresada es superior a la cantidad disponible que es: ".ToUpper + Str(stock) + " , primero se debe regularizar el stock".ToUpper, img, 6000, eToastGlowColor.Red, eToastPosition.TopCenter)

                        End If
                    Else
                        Dim rowindex As Integer = grdetalle.Row
                        Dim lin As Integer = grdetalle.GetValue("tbnumi")
                        Dim pos As Integer = -1
                        _fnobtenerfiladetalle(pos, lin)
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbcmin") = 1
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpbas")

                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbporc") = 0
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbdesc") = 0
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpbas")
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot2") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos")
                        grdetalle.SetValue("tbcmin", 1)
                        grdetalle.SetValue("tbptot", grdetalle.GetValue("tbpbas"))

                        calculardescuentos(grdetalle.GetValue("tbty5prod"), grdetalle.GetValue("tbcmin"), grdetalle.GetValue("tbpbas"), pos)


                        _prcalcularpreciototal()
                        p_ponertotal(rowindex)
                        calculodescuentoxproveedor()
                        'grdetalle.setvalue("tbcmin", 1)
                        'grdetalle.setvalue("tbptot", grdetalle.getvalue("tbpbas"))

                    End If
                End If
            End If
            '''''''''''''''''''''porcentaje de descuento '''''''''''''''''''''
            If (e.Column.Index = grdetalle.RootTable.Columns("tbporc").Index) Then
                If (Not IsNumeric(grdetalle.GetValue("tbporc")) Or grdetalle.GetValue("tbporc").ToString = String.Empty) Then

                    'grdetalle.getrow(rowindex).cells("cant").value = 1
                    '  grdetalle.currentrow.cells.item("cant").value = 1
                    Dim lin As Integer = grdetalle.GetValue("tbnumi")
                    Dim pos As Integer = -1
                    _fnobtenerfiladetalle(pos, lin)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbporc") = 0
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbdesc") = 0
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot")
                    'grdetalle.setvalue("tbcmin", 1)
                    'grdetalle.setvalue("tbptot", grdetalle.getvalue("tbpbas"))
                Else
                    If (grdetalle.GetValue("tbporc") > 0 And grdetalle.GetValue("tbporc") <= 100) Then

                        Dim porcdesc As Double = grdetalle.GetValue("tbporc")
                        Dim montodesc As Double = (grdetalle.GetValue("tbptot") * (porcdesc / 100))
                        Dim lin As Integer = grdetalle.GetValue("tbnumi")
                        Dim pos As Integer = -1
                        _fnobtenerfiladetalle(pos, lin)
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbdesc") = montodesc
                        grdetalle.SetValue("tbdesc", montodesc)

                        Dim rowindex As Integer = grdetalle.Row
                        p_ponertotal(rowindex)
                        calculodescuentoxproveedor()
                    Else
                        Dim lin As Integer = grdetalle.GetValue("tbnumi")
                        Dim pos As Integer = -1
                        _fnobtenerfiladetalle(pos, lin)
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbporc") = 0
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbdesc") = 0
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot")
                        grdetalle.SetValue("tbporc", 0)
                        grdetalle.SetValue("tbdesc", 0)
                        grdetalle.SetValue("tbtotdesc", grdetalle.GetValue("tbptot"))
                        _prcalcularpreciototal()
                        'grdetalle.setvalue("tbcmin", 1)
                        'grdetalle.setvalue("tbptot", grdetalle.getvalue("tbpbas"))

                    End If
                End If
            End If


            '''''''''''''''''''''monto de descuento '''''''''''''''''''''
            If (e.Column.Index = grdetalle.RootTable.Columns("tbdesc").Index) Then
                If (Not IsNumeric(grdetalle.GetValue("tbdesc")) Or grdetalle.GetValue("tbdesc").ToString = String.Empty) Then

                    'grdetalle.getrow(rowindex).cells("cant").value = 1
                    '  grdetalle.currentrow.cells.item("cant").value = 1
                    Dim lin As Integer = grdetalle.GetValue("tbnumi")
                    Dim pos As Integer = -1
                    _fnobtenerfiladetalle(pos, lin)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbporc") = 0
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbdesc") = 0
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot")
                    'grdetalle.setvalue("tbcmin", 1)
                    'grdetalle.setvalue("tbptot", grdetalle.getvalue("tbpbas"))
                Else
                    If (grdetalle.GetValue("tbdesc") > 0 And grdetalle.GetValue("tbdesc") <= grdetalle.GetValue("tbptot")) Then

                        Dim montodesc As Double = grdetalle.GetValue("tbdesc")
                        Dim pordesc As Double = ((montodesc * 100) / grdetalle.GetValue("tbptot"))

                        Dim lin As Integer = grdetalle.GetValue("tbnumi")
                        Dim pos As Integer = -1
                        _fnobtenerfiladetalle(pos, lin)
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbdesc") = montodesc
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbporc") = pordesc

                        grdetalle.SetValue("tbporc", pordesc)
                        Dim rowindex As Integer = grdetalle.Row
                        p_ponertotal(rowindex)

                    Else
                        Dim lin As Integer = grdetalle.GetValue("tbnumi")
                        Dim pos As Integer = -1
                        _fnobtenerfiladetalle(pos, lin)
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbporc") = 0
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbdesc") = 0
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot")
                        grdetalle.SetValue("tbporc", 0)
                        grdetalle.SetValue("tbdesc", 0)
                        grdetalle.SetValue("tbtotdesc", grdetalle.GetValue("tbptot"))
                        _prcalcularpreciototal()
                        'grdetalle.setvalue("tbcmin", 1)
                        'grdetalle.setvalue("tbptot", grdetalle.getvalue("tbpbas"))

                    End If
                End If
            End If
        Catch ex As Exception
            mostrarmensajeerror(ex.Message)
        End Try
    End Sub
    Private Sub tbpdesc_valuechanged(sender As Object, e As EventArgs) Handles tbPdesc.ValueChanged
        Try
            If (tbPdesc.Focused) Then
                If (Not tbPdesc.Text = String.Empty And Not tbTotalBs.Text = String.Empty) Then
                    If (tbPdesc.Value = 0 Or tbPdesc.Value > 100) Then
                        tbPdesc.Value = 0
                        tbMdesc.Value = 0

                        _prcalcularpreciototal()


                    Else

                        Dim porcdesc As Double = tbPdesc.Value
                        Dim montodesc As Double = (grdetalle.GetTotal(grdetalle.RootTable.Columns("tbtotdesc"), AggregateFunction.Sum) * (porcdesc / 100))
                        tbMdesc.Value = montodesc

                        tbTotalBs.Text = grdetalle.GetTotal(grdetalle.RootTable.Columns("tbtotdesc"), AggregateFunction.Sum) - montodesc

                    End If


                End If
                If (tbPdesc.Text = String.Empty) Then
                    tbMdesc.Value = 0

                End If
            End If
        Catch ex As Exception
            mostrarmensajeerror(ex.Message)
        End Try
    End Sub

    Private Sub tbmdesc_valuechanged(sender As Object, e As EventArgs) Handles tbMdesc.ValueChanged

        Try
            If (tbMdesc.Focused) Then

                Dim subtotal As Double = Convert.ToDouble(tbSubTotal.Value)
                If (Not tbMdesc.Text = String.Empty And Not tbMdesc.Text = String.Empty) Then
                    If (tbMdesc.Value = 0 Or tbMdesc.Value > subtotal) Then
                        tbMdesc.Value = 0
                        tbPdesc.Value = 0
                        _prcalcularpreciototal()
                    Else
                        Dim montodesc As Double = tbMdesc.Value
                        Dim pordesc As Double = ((montodesc * 100) / grdetalle.GetTotal(grdetalle.RootTable.Columns("tbtotdesc"), AggregateFunction.Sum))
                        tbPdesc.Value = pordesc

                        tbTotalBs.Text = grdetalle.GetTotal(grdetalle.RootTable.Columns("tbtotdesc"), AggregateFunction.Sum) - montodesc
                    End If

                End If

                If (tbMdesc.Text = String.Empty) Then
                    tbMdesc.Value = 0

                End If
            End If
        Catch ex As Exception
            mostrarmensajeerror(ex.Message)
        End Try

    End Sub

    Private Sub grdetalle_celledited_1(sender As Object, e As ColumnActionEventArgs) Handles grdetalle.CellEdited
        'try
        '    if (e.column.index = grdetalle.roottable.columns("tbcmin").index) then
        '        if (not isnumeric(grdetalle.getvalue("tbcmin")) or grdetalle.getvalue("tbcmin").tostring = string.empty) then

        '            grdetalle.setvalue("tbcmin", 1)
        '            grdetalle.setvalue("tbptot", grdetalle.getvalue("tbpbas"))
        '            grdetalle.setvalue("tbporc", 0)
        '            grdetalle.setvalue("tbdesc", 0)
        '            grdetalle.setvalue("tbtotdesc", grdetalle.getvalue("tbpbas"))


        '        else
        '            if (grdetalle.getvalue("tbcmin") > 0) then

        '                dim cant as integer = grdetalle.getvalue("tbcmin")

        '                dim stock as double = grdetalle.getvalue("stock")
        '                if (cant > stock) and stock <> -9999 then
        '                    dim lin as integer = grdetalle.getvalue("tbnumi")
        '                    dim pos as integer = -1
        '                    _fnobtenerfiladetalle(pos, lin)
        '                    ctype(grdetalle.datasource, datatable).rows(pos).item("tbcmin") = 1
        '                    ctype(grdetalle.datasource, datatable).rows(pos).item("tbptot") = ctype(grdetalle.datasource, datatable).rows(pos).item("tbpbas")
        '                    ctype(grdetalle.datasource, datatable).rows(pos).item("tbptot2") = grdetalle.getvalue("tbpcos") * 1
        '                    dim img as bitmap = new bitmap(my.resources.mensaje, 50, 50)
        '                    toastnotification.show(me, "la cantidad de la venta no debe ser mayor al del stock" & vbcrlf &
        '                    "stock=" + str(stock).toupper, img, 2000, etoastglowcolor.red, etoastposition.bottomcenter)
        '                    grdetalle.setvalue("tbcmin", 1)
        '                    grdetalle.setvalue("tbptot", grdetalle.getvalue("tbpbas"))
        '                    grdetalle.setvalue("tbptot2", grdetalle.getvalue("tbpcos") * 1)

        '                    _prcalcularpreciototal()
        '                else
        '                    if (cant = stock) then


        '                        'grdetalle.selectedformatstyle.forecolor = color.blue
        '                        'grdetalle.currentrow.cells.item(e.column).formatstyle = new gridexformatstyle
        '                        'grdetalle.currentrow.cells(e.column).formatstyle.backcolor = color.pink
        '                        'grdetalle.currentrow.cells.item(e.column).formatstyle.backcolor = color.dodgerblue
        '                        'grdetalle.currentrow.cells.item(e.column).formatstyle.forecolor = color.white
        '                        'grdetalle.currentrow.cells.item(e.column).formatstyle.fontbold = tristate.true
        '                    end if
        '                end if

        '            else

        '                grdetalle.setvalue("tbcmin", 1)
        '                grdetalle.setvalue("tbptot", grdetalle.getvalue("tbpbas"))
        '                grdetalle.setvalue("tbporc", 0)
        '                grdetalle.setvalue("tbdesc", 0)
        '                grdetalle.setvalue("tbtotdesc", grdetalle.getvalue("tbpbas"))

        '            end if
        '        end if
        '    end if
        'catch ex as exception
        '    mostrarmensajeerror(ex.message)
        'end try
    End Sub
    Private Sub grdetalle_mouseclick(sender As Object, e As MouseEventArgs) Handles grdetalle.MouseClick
        Try
            If (Not _fnaccesible()) Then
                Return
            End If
            If (grdetalle.RowCount >= 2) Then
                Try
                    If (grdetalle.CurrentColumn.Index = grdetalle.RootTable.Columns("img").Index) Then
                        _preliminarfila()
                        calculodescuentoxproveedor()
                    End If
                Catch ex As Exception
                End Try
            End If
        Catch ex As Exception
            mostrarmensajeerror(ex.Message)
        End Try

    End Sub
    Private Sub btngrabar_click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        If _validarcampos() = False Then
            Exit Sub
        End If
        _prguardar()

    End Sub

    Private Sub btnmodificar_click(sender As Object, e As EventArgs) Handles btnModificar.Click
        Try
            If (grVentas.RowCount > 0) Then
                If (gb_FacturaEmite) Then
                    If (Not p_fnvalidarfacturavigente()) Then
                        Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)

                        ToastNotification.Show(Me, "no se puede modificar la venta con codigo ".ToUpper + tbCodigo.Text + ", su factura esta anulada.".ToUpper,
                                                  img, 2000,
                                                  eToastGlowColor.Green,
                                                  eToastPosition.TopCenter)
                        Exit Sub
                    End If
                End If

                _prhabilitar()
                btnNuevo.Enabled = False
                btnModificar.Enabled = False
                btnEliminar.Enabled = False
                btnGrabar.Enabled = True

                PanelNavegacion.Enabled = False
                _prcargariconeliminar()
            End If
        Catch ex As Exception
            mostrarmensajeerror(ex.Message)
        End Try
    End Sub
    Private Sub btneliminar_click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        Try

            If (gb_FacturaEmite) Then
                If (p_fnvalidarfacturavigente()) Then
                    Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)

                    ToastNotification.Show(Me, "no se puede anular la venta con código ".ToUpper + tbCodigo.Text + ", su factura esta vigente, por favor primero anule la factura".ToUpper,
                                                  img, 3000,
                                                  eToastGlowColor.Green,
                                                  eToastPosition.TopCenter)
                    Exit Sub
                End If
            End If

            Dim res2 As Boolean = L_fnVerificarCierreCaja(tbCodigo.Text, "v")
            If res2 Then
                Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)

                ToastNotification.Show(Me, "no se puede anular la venta con código ".ToUpper + tbCodigo.Text + ", ya se hizo cierre de caja, por favor primero elimine cierre de caja".ToUpper,
                                                  img, 5000,
                                                  eToastGlowColor.Green,
                                                  eToastPosition.TopCenter)
                Exit Sub
            End If


            Dim result As Boolean = L_fnVerificarSiSeContabilizoVenta(tbCodigo.Text)
            If result Then
                Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                ToastNotification.Show(Me, "la venta no puede ser anulada porque ya fue contabilizada".ToUpper, img, 4500, eToastGlowColor.Red, eToastPosition.TopCenter)
                Exit Sub
            End If
            Dim ef = New Efecto
            ef.tipo = 2
            ef.Context = "¿esta seguro de eliminar el registro?".ToUpper
            ef.Header = "mensaje principal".ToUpper
            ef.ShowDialog()
            Dim bandera As Boolean = False
            bandera = ef.band
            If (bandera = True) Then
                Dim mensajeerror As String = ""
                Dim res As Boolean = L_fnEliminarVenta(tbCodigo.Text, mensajeerror, programa, gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina, 0)
                If res Then
                    Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
                    ToastNotification.Show(Me, "código de venta ".ToUpper + tbCodigo.Text + " eliminado con exito.".ToUpper,
                                              img, 2000,
                                              eToastGlowColor.Green,
                                              eToastPosition.TopCenter)
                    _prfiltrar()
                Else
                    Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                    ToastNotification.Show(Me, mensajeerror, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                End If
            End If
        Catch ex As Exception
            mostrarmensajeerror(ex.Message)
        End Try
    End Sub

    Private Sub grventas_selectionchanged(sender As Object, e As EventArgs) Handles grVentas.SelectionChanged
        If (grVentas.RowCount >= 0 And grVentas.Row >= 0) Then
            _prmostrarregistro(grVentas.Row)
        End If
    End Sub

    Private Sub btnsiguiente_click(sender As Object, e As EventArgs) Handles btnSiguiente.Click
        Dim _pos As Integer = grVentas.Row
        If _pos < grVentas.RowCount - 1 And _pos >= 0 Then
            _pos = grVentas.Row + 1
            '' _prmostrarregistro(_pos)
            grVentas.Row = _pos
        End If
    End Sub

    Private Sub btnultimo_click(sender As Object, e As EventArgs) Handles btnUltimo.Click
        Dim _pos As Integer = grVentas.Row
        If grVentas.RowCount > 0 Then
            _pos = grVentas.RowCount - 1
            ''  _prmostrarregistro(_pos)
            grVentas.Row = _pos
        End If
    End Sub

    Private Sub btnanterior_click(sender As Object, e As EventArgs) Handles btnAnterior.Click
        Dim _mpos As Integer = grVentas.Row
        If _mpos > 0 And grVentas.RowCount > 0 Then
            _mpos = _mpos - 1
            ''  _prmostrarregistro(_mpos)
            grVentas.Row = _mpos
        End If
    End Sub

    Private Sub btnprimero_click(sender As Object, e As EventArgs) Handles btnPrimero.Click
        _primerregistro()
    End Sub
    Private Sub grventas_keydown(sender As Object, e As KeyEventArgs) Handles grVentas.KeyDown
        If e.KeyData = Keys.Enter Then
            MSuperTabControl.SelectedTabIndex = 0
            grdetalle.Focus()

        End If
    End Sub

    Private Sub tbnit_validating(sender As Object, e As System.ComponentModel.CancelEventArgs)
        'if btngrabar.enabled = true then

        '    dim nom1, nom2, correo, tipodoc as string
        '    nom1 = ""
        '    nom2 = ""
        '    correo = ""
        '    tipodoc = ""
        '    if (tbnit.text.trim <> string.empty) then
        '        l_validar_nit(tbnit.text.trim, nom1, nom2, correo, tipodoc, "")

        '        if nom1 = "" then
        '            clientenuevo()
        '            tbnombre1.focus()
        '        else
        '            tbnombre1.text = nom1
        '            tbnombre2.text = nom2
        '            tbemail.text = correo
        '            cbtipodoc.value = tipodoc
        '            dim dt as datatable = l_fnobtenerclientesporrazonsocialnit(tbnombre1.text, tbnit.text)
        '            if dt.rows.count > 0 then
        '                tbcliente.text = dt.rows(0).item("yddesc")
        '                _codcliente = dt.rows(0).item("ydnumi")
        '                _dias = dt.rows(0).item("yddias")
        '                dim numivendedor as integer = iif(isdbnull(dt.rows(0).item("ydnumivend")), 0, dt.rows(0).item("ydnumivend"))
        '                if (numivendedor > 0) then
        '                    tbvendedor.text = dt.rows(0).item("vendedor")
        '                    _codempleado = dt.rows(0).item("ydnumivend")
        '                    grdetalle.select()
        '                    table_producto = nothing
        '                else
        '                    tbvendedor.clear()
        '                    _codempleado = 0
        '                    tbvendedor.focus()
        '                    table_producto = nothing
        '                end if


        '            end if

        '        end if

        '    end if

        'end if

    End Sub
    Private Sub btnimprimir_click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        Try
            If (Not _fnaccesible()) Then
                'dim dt as datatable = l_fnrecuperarfactura(tbcodigo.text)
                'dim url as string = dt.rows(0).item("fvafacturl").tostring
                'system.diagnostics.process.start(url)
                '_primiprimirnotaventa(tbcodigo.text)

                F0_VentasSupermercado.P_prImprimirFacturaNueva(tbCodigo.Text, True, False)
                _primiprimirnotaventa(tbCodigo.Text)
                L_fnBotonImprimir(gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina, tbCodigo.Text, "tv001", "venta")

                'if (gb_facturaemite) then
                '    if tbcodigo.text = string.empty then
                '        throw new exception("venta no encontrada")
                '    end if
                '    if tbnit.text = string.empty then
                '        _primiprimirnotaventa(tbcodigo.text)
                '        return
                '    elseif (not p_fnvalidarfacturavigente()) then

                '        dim img as bitmap = new bitmap(my.resources.warning, 50, 50)

                '        toastnotification.show(me, "no se puede imprimir la factura con numero ".toupper + tbnrofactura.text + ", su factura esta anulada".toupper,
                '                              img, 3000,
                '                              etoastglowcolor.green,
                '                              etoastposition.topcenter)
                '        exit sub
                '    end if
                '    reimprimirfactura(tbcodigo.text, true, true)
                '    _primiprimirnotaventa(tbcodigo.text)
                'else
                '    _primiprimirnotaventa(tbcodigo.text)
                'end if
            End If

        Catch ex As Exception
            mostrarmensajeerror(ex.Message)
        End Try
    End Sub

    Private Sub tbnit_keypress(sender As Object, e As KeyPressEventArgs)
        g_prValidarTextBox(1, e)
    End Sub

    Private Sub swtipoventa_leave(sender As Object, e As EventArgs)
        grdetalle.Select()
    End Sub

    Private Sub cbsucursal_valuechanged(sender As Object, e As EventArgs) Handles cbSucursal.ValueChanged
        table_producto = Nothing
    End Sub

    Private Sub tbnit_leave(sender As Object, e As EventArgs) Handles TbNomCliente.Leave
        'grdetalle.select()
    End Sub




    Private Sub cbcambiodolar_valuechanged_1(sender As Object, e As EventArgs) Handles cbCambioDolar.ValueChanged
        If cbCambioDolar.SelectedIndex < 0 And cbCambioDolar.Text <> String.Empty Then
            btgrupo1.Visible = True
        Else
            btgrupo1.Visible = False
        End If
    End Sub
    Private Sub btgrupo1_click(sender As Object, e As EventArgs) Handles btgrupo1.Click
        Dim numi As String = ""

        If L_prLibreriaGrabar(numi, "7", "1", cbCambioDolar.Text, "") Then
            _prcargarcombolibreria(cbCambioDolar, "7", "1")
            cbCambioDolar.SelectedIndex = CType(cbCambioDolar.DataSource, DataTable).Rows.Count - 1
        End If
    End Sub
    Private Sub _prcargarcombolibreria(mcombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo, cod1 As String, cod2 As String)
        Dim dt As New DataTable
        dt = L_prLibreriaClienteLGeneral(cod1, cod2)
        With mcombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("yccod3").Width = 70
            .DropDownList.Columns("yccod3").Caption = "cod"
            .DropDownList.Columns.Add("ycdes3").Width = 200
            .DropDownList.Columns("ycdes3").Caption = "descripcion"
            .ValueMember = "yccod3"
            .DisplayMember = "ycdes3"
            .DataSource = dt
            .Refresh()
        End With
    End Sub

    Private Sub tbmontobs_keydown(sender As Object, e As KeyEventArgs)
        If (e.KeyData = Keys.Control + Keys.A) Then
            btnGrabar.PerformClick()
            '_prguardar()
        End If
    End Sub

    Private Sub tbmontodolar_keydown(sender As Object, e As KeyEventArgs)
        If (e.KeyData = Keys.Control + Keys.A) Then
            btnGrabar.PerformClick()
            '_prguardar()
        End If
    End Sub
    Private Sub tbmontotarej_keydown(sender As Object, e As KeyEventArgs)
        If (e.KeyData = Keys.Control + Keys.A) Then
            btnGrabar.PerformClick()
            '_prguardar()
        End If
    End Sub
    Private Sub tbmontoqr_keydown(sender As Object, e As KeyEventArgs)
        If (e.KeyData = Keys.Control + Keys.A) Then
            btnGrabar.PerformClick()
            '_prguardar()
        End If
    End Sub

    Private Sub grouppanel1_click(sender As Object, e As EventArgs) Handles GroupCobranza.Click

    End Sub

    Private Sub grdetalle_enter(sender As Object, e As EventArgs) Handles grdetalle.Enter
        Try
            If (_fnaccesible()) Then
                If (_codcliente <= 0) Then
                    ToastNotification.Show(Me, "           antes de continuar por favor seleccione un cliente!!             ", My.Resources.WARNING, 4000, eToastGlowColor.Red, eToastPosition.TopCenter)
                    tbCliente.Focus()

                    Return
                End If
                If (_codempleado <= 0) Then


                    ToastNotification.Show(Me, "           antes de continuar por favor seleccione un vendedor!!             ", My.Resources.WARNING, 4000, eToastGlowColor.Red, eToastPosition.TopCenter)
                    tbVendedor.Focus()
                    Return

                End If

                grdetalle.Select()
                If _codebar = 1 Then
                    If gb_CodigoBarra Then
                        grdetalle.Col = 4
                        grdetalle.Row = 0
                    Else
                        grdetalle.Col = 5
                        grdetalle.Row = 0
                    End If
                End If
            End If
        Catch ex As Exception
            mostrarmensajeerror(ex.Message)
        End Try
    End Sub

    'private sub btnduplicar_click(sender as object, e as eventargs) handles btnduplicar.click
    '    try
    '        dim nit as string = tbnit.text
    '        dim razon_social as string = tbnombre1.text
    '        dim cliente as string = tbcliente.text
    '        dim vend as string = tbvendedor.text
    '        dim tipoventa as boolean = swtipoventa.value
    '        dim fechavenc as date = tbfechavenc.value
    '        dim totalbs as string = tbtotalbs.text
    '        dim totaldo as string = tbtotaldo.text

    '        dim table as datatable = grdetalle.datasource

    '        btnnuevo.performclick()
    '        tbnit.text = nit
    '        tbnombre1.text = razon_social
    '        tbcliente.text = cliente
    '        tbvendedor.text = vend
    '        swtipoventa.value = tipoventa
    '        tbfechavenc.value = fechavenc
    '        tbtotalbs.text = totalbs
    '        tbtotaldo.text = totaldo
    '        txtestado.clear()
    '        'txtestado.text = "vigente"
    '        'grdetalle.datasource = table

    '        'dim _detalle1 as datatable = grdetalle.datasource

    '        'for j as integer = 0 to grdetalle.rowcount - 1 step 1
    '        '    grdetalle.row = j
    '        '    _detalle1.rows(j).item("tbtv1numi") = 0
    '        '    _detalle1.rows(j).item("estado") = 0
    '        'next
    '        'grdetalle.datasource = _detalle1


    '        for j as integer = 0 to table.rows.count - 1 step 1

    '            table.rows(j).item("tbtv1numi") = 0
    '            table.rows(j).item("estado") = 0
    '        next
    '        grdetalle.datasource = table
    '        _prcargariconeliminar()
    '    catch ex as exception
    '        mostrarmensajeerror(ex.message)
    '    end try
    'end sub

    Private Sub timer1_tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        _inter = _inter + 1
        If _inter = 1 Then
            Me.WindowState = FormWindowState.Normal
        Else
            Me.Opacity = 100
            Timer1.Enabled = False
        End If
    End Sub

    Private Sub btnbitacora_click(sender As Object, e As EventArgs)
        Try
            Dim formulariodescuento As FO_Venta2Descu = New FO_Venta2Descu
            If listadescuento.Count() = 0 Then Throw New Exception("lista de descuento vacia")
            formulariodescuento.descuento = listadescuento
            formulariodescuento.Show()
        Catch ex As Exception
            mostrarmensajeerror(ex.Message)
        End Try

    End Sub


    Private Sub calculodescuentoxproveedor()
        If configuraciondescuentoesxcantidad = True Then
            Return
        End If

        If grdetalle.RowCount < 0 Or descuentoxproveedorlist.Rows.Count < 0 Or descuentoxproveedorlist.Rows.Count = 0 Or SwDescuentoProveedor.Value = False Then
            Return
        End If
        Dim productoid As Integer = 0
        Dim montodescuento As Decimal = 0, subtotaldescuento As Decimal = 0
        Dim totalxproveedor As Decimal = 0, subtotalventa As Decimal = 0, totalacumuladodescespecial As Decimal = 0

        Dim detalle = CType(grdetalle.DataSource, DataTable)
        Dim detallelista As List(Of DataRow) = detalle.AsEnumerable().ToList()
        Dim descuentoxproveedorlista As List(Of DataRow) = descuentoxproveedorlist.AsEnumerable().ToList()
        listadescuento = New List(Of DescuentoXCategoriaDescuento)

        Dim proveedoridarray = (From proc In detallelista
                                Where proc.ItemArray(ENDetalleVenta.estadoDetalle) <> EnEstadoFormulario.Eliminado
                                Select proc.ItemArray(ENDetalleVenta.proveedorId)).Distinct().ToArray()
        Dim existeproveedorentabladescuento As Boolean = False

        For Each proveedorid As Integer In proveedoridarray

            existeproveedorentabladescuento = (From desc In descuentoxproveedorlista
                                               Where desc.ItemArray(ENDescuentoXProveedor.proveedorId) = proveedorid).Count() > 0

            totalxproveedor = obtenersumatotalxproveedor(detallelista, proveedorid)
            If existeproveedorentabladescuento Then

                Dim porcentajedescuento = obtenerporcentajedescuento(descuentoxproveedorlista, totalxproveedor, False, proveedorid)

                If porcentajedescuento.length <> 0 Then
                    montodescuento = (totalxproveedor * porcentajedescuento(0)) / 100
                    subtotaldescuento += montodescuento
                    Call armardscuento(proveedorid, montodescuento)

                Else
                    totalacumuladodescespecial += totalxproveedor

                End If
            End If

            'obtiene la suma total por proveedor
            subtotalventa += totalxproveedor
        Next
        'calculo descuento especial
        If totalacumuladodescespecial <> 0 Then
            Dim porcentajedescuento = obtenerporcentajedescuento(descuentoxproveedorlista, totalacumuladodescespecial, True)
            If porcentajedescuento.length <> 0 Then
                montodescuento = (totalacumuladodescespecial * porcentajedescuento(0)) / 100
                subtotaldescuento += montodescuento
                Call armardscuento(1000, montodescuento)
            End If
        End If

        Dim montodo As Decimal
        tbSubTotal.Value = subtotalventa
        tbMdesc.Value = subtotaldescuento
        tbTotalBs.Text = Format(subtotalventa - subtotaldescuento, "0.00")
        montodo = Convert.ToDecimal(tbTotalBs.Text) / IIf(cbCambioDolar.Text = "", 1, Convert.ToDecimal(cbCambioDolar.Text))
        tbTotalDo.Text = Format(montodo, "0.00")
        'calcularcambio()

    End Sub
    Private Function actualizarcantidad(ByVal detallelista As List(Of DataRow),
                                        ByVal estado As EnEstadoFormulario) As List(Of DataRow)

        For Each fila As DataRow In detallelista.Where(Function(x) x.ItemArray(ENDetalleVenta.estado) >= estado)

            fila.SetField(ENDetalleVenta.cantidad, Convert.ToInt32(fila.ItemArray(ENDetalleVenta.total) / fila.ItemArray(ENDetalleVenta.precio)))
        Next
        Return detallelista
    End Function


    Private Function insertarmontodescuento(ByVal detallelista As List(Of DataRow),
                                            ByVal montodescuento As Decimal,
                                            ByVal estado As EnEstadoFormulario,
                                            Optional ByVal descuentoid As Integer = 0) As List(Of DataRow)
        Dim descuento As List(Of DescuentoXCategoriaDescuento) = New List(Of DescuentoXCategoriaDescuento)


        'inserta el monto de descuento en grupo correspondiente

        Dim cantidaddefilas As Integer = 0
        Dim promedio As Decimal = 0
        If descuentoid = 0 Then
            'insertar monto en detalle grupo normal
            For Each fila As DataRow In detallelista.Where(Function(x) x.ItemArray(ENDetalleVenta.estado) >= estado)
                'fila.setfield(endetalleventa.estado, estado)

                fila.SetField(ENDetalleVenta.descuento, montodescuento)
                fila.SetField(ENDetalleVenta.totalDescuento, fila.ItemArray(ENDetalleVenta.total) - montodescuento)
                fila.SetField(ENDetalleVenta.cantidad, Convert.ToInt32(fila.ItemArray(ENDetalleVenta.total) / fila.ItemArray(ENDetalleVenta.precio)))
            Next
        Else
            'insertar monto en detalle grupo especial
            For Each fila As DataRow In detallelista.Where(Function(x) x.ItemArray(ENDetalleVenta.estado) >= estado _
                                                                   And x.ItemArray(ENDetalleVenta.proveedorId) = descuentoid)

                cantidaddefilas = detallelista.Where(Function(x) x.ItemArray(ENDetalleVenta.estado) >= estado _
                                                                 And x.ItemArray(ENDetalleVenta.proveedorId) = descuentoid).Count()
                promedio = montodescuento / cantidaddefilas

                fila.SetField(ENDetalleVenta.descuento, promedio)
                fila.SetField(ENDetalleVenta.totalDescuento, fila.ItemArray(ENDetalleVenta.total) - promedio)
                fila.SetField(ENDetalleVenta.cantidad, Convert.ToInt32(fila.ItemArray(ENDetalleVenta.total) / fila.ItemArray(ENDetalleVenta.precio)))
                'fila.itemarray(endetalleventa.totaldescuento) = fila.itemarray(endetalleventa.total) - montodescuento
            Next
        End If

        Return detallelista
    End Function
    Private Function marcarestadodescuento(ByVal detallelista As List(Of DataRow),
                                           ByVal descuentoid As Integer,
                                           ByVal estado As EnEstadoFormulario) As List(Of DataRow)
        'marca el estado de los desccuentos especciales
        For Each fila As DataRow In detallelista.Where(Function(x) x.ItemArray(ENDetalleVenta.estado) >= estado _
                                                               And x.ItemArray(ENDetalleVenta.proveedorId) = descuentoid).ToList()
            Dim a = fila.ItemArray(ENDetalleVenta.total)
            fila.SetField(ENDetalleVenta.estado, EnEstadoFormulario.MarcaDescuento)
            fila.SetField(ENDetalleVenta.cantidad, Convert.ToInt32(fila.ItemArray(ENDetalleVenta.total) / fila.ItemArray(ENDetalleVenta.precio)))
            'fila.itemarray(endetalleventa.estado) = enestadoformulario.marcadescuento
            fila.ItemArray(ENDetalleVenta.estado) = 6
        Next
        Return detallelista
    End Function
    Private Function obtenerporcentajedescuento(listadescuento As List(Of DataRow), total As Decimal, esdescuentoespecial As Boolean, Optional proveedorid As Integer = 0) As Object

        If esdescuentoespecial Then
            'obtiene el porcentaje de descuento especial
            Return (From desc In listadescuento
                    Where desc.ItemArray(ENDescuentoXProveedor.Estado) = 2 _
                     And desc.ItemArray(ENDescuentoXProveedor.MontoInicial) <= total _
                     And desc.ItemArray(ENDescuentoXProveedor.MontoFinal) >= total
                    Select desc.ItemArray(ENDescuentoXProveedor.DescuentoPorcentaje)).ToArray()
        Else
            'obtiene el porcentaje de descuento por proveedor

            Return (From desc In listadescuento
                    Where desc.ItemArray(ENDescuentoXProveedor.proveedorId) = proveedorid _
                     And desc.ItemArray(ENDescuentoXProveedor.MontoInicial) <= total _
                     And desc.ItemArray(ENDescuentoXProveedor.MontoFinal) >= total
                    Select desc.ItemArray(ENDescuentoXProveedor.DescuentoPorcentaje)).ToArray()
        End If
    End Function
    Private Sub armardscuento(ByVal descuentoid As Integer, cantidad As Decimal)

        Dim descuentoxproveedorlista As List(Of DataRow) = descuentoxproveedorlist.AsEnumerable().ToList()
        Dim libreriadescuento As List(Of DataRow) = L_prLibreriaClienteLGeneral(1, 5).AsEnumerable().ToList()
        Dim descuento As DescuentoXCategoriaDescuento = New DescuentoXCategoriaDescuento

        Dim newrow As DataRow
        If listadescuento.Where(Function(x) x.DescuentoId = descuentoid).Count() > 0 Then
            For Each fila As DescuentoXCategoriaDescuento In listadescuento.Where(Function(x) x.DescuentoId = descuentoid)
                fila.Cantidad = cantidad
            Next
        Else
            If descuentoid = 1000 Then
                descuento.DescuentoId = descuentoid
                descuento.Descripcion = "descuento especial"
                descuento.Cantidad = cantidad
            Else
                descuento.DescuentoId = descuentoid
                descuento.Descripcion = libreriadescuento.Where(Function(x) x.ItemArray(0) = descuentoid).FirstOrDefault().ItemArray(1)
                descuento.Cantidad = cantidad
            End If
            listadescuento.Add(descuento)
        End If
    End Sub
    Private Function obtenersumatotalxproveedor(detallelista As List(Of DataRow), proveedorid As Integer) As Decimal
        Dim descuentototal As Decimal?
        descuentototal = (From proc In detallelista
                          Where proc.ItemArray(ENDetalleVenta.estado) >= EnEstadoFormulario.Nuevo _
                            And proc.ItemArray(ENDetalleVenta.proveedorId) = proveedorid
                          Select Convert.ToDecimal(proc.ItemArray(ENDetalleVenta.totalDescuento))).Sum()
        Return IIf(descuentototal.HasValue, descuentototal.Value, 0)

    End Function

    Private Function quitarultimafilavacia(tabla As DataTable) As DataTable
        If tabla.Rows.Count > 0 Then
            If (tabla.Rows(tabla.Rows.Count - 1).Item("producto").ToString() = String.Empty) Then
                tabla.Rows.RemoveAt(tabla.Rows.Count - 1)
                tabla.AcceptChanges()
            End If
        End If
        Return tabla
    End Function


    Private Sub swmostrar_valuechanged(sender As Object, e As EventArgs) Handles swMostrar.ValueChanged
        _prcargarProforma()
    End Sub


#End Region

End Class