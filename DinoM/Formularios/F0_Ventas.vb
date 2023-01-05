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

Public Class F0_Ventas

#Region "Variables Globales"
    Dim _CodCliente As Integer = 0
    Dim _CodEmpleado As Integer = 0
    Dim OcultarFact As Integer = 0
    Dim _codeBar As Integer = 1
    Dim _dias As Integer = 0
    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
    Dim FilaSelectLote As DataRow = Nothing
    Dim Table_Producto As DataTable
    Dim G_Lote As Boolean = False '1=igual a mostrar las columnas de lote y fecha de Vencimiento
    Dim dtProductoGoblal As DataTable

#End Region

#Region "Metodos Privados"
    Private Sub _IniciarTodo()
        L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        MSuperTabControl.SelectedTabIndex = 0
        Me.WindowState = FormWindowState.Maximized

        _prCargarComboLibreriaSucursal(cbSucursal)
        lbTipoMoneda.Visible = True
        swMoneda.Visible = True
        P_prCargarVariablesIndispensables()
        _prCargarVenta()
        _prInhabiliitar()
        grVentas.Focus()
        Me.Text = "VENTA DE SERVICIOS"
        Dim blah As New Bitmap(New Bitmap(My.Resources.compra), 20, 20)
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico
        _prAsignarPermisos()
        P_prCargarParametro()
        _prValidadFactura()
        _prCargarNameLabel()

    End Sub
    Public Sub _prCargarNameLabel()
        Dim dt As DataTable = L_fnNameLabel()
        If (dt.Rows.Count > 0) Then
            _codeBar = 1 'dt.Rows(0).Item("codeBar")
        End If
    End Sub
    Sub _prValidadFactura()
        'If (OcultarFact = 1) Then
        '    GroupPanelFactura2.Visible = False
        '    GroupPanelFactura.Visible = False
        'Else
        '    GroupPanelFactura2.Visible = True
        '    GroupPanelFactura.Visible = True
        'End If

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
        tbFechaVenc.IsInputReadOnly = True
        swMoneda.IsReadOnly = True
        swTipoVenta.IsReadOnly = True
        btnSearchCliente.Visible = False
        'Datos facturacion
        tbNroAutoriz.ReadOnly = True
        tbNroFactura.ReadOnly = True
        tbCodigoControl.ReadOnly = True
        dtiFechaFactura.IsInputReadOnly = True
        dtiFechaFactura.ButtonDropDown.Enabled = False

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

        TbNit.ReadOnly = True
        TbNombre1.ReadOnly = True
        TbNombre2.ReadOnly = True
        cbSucursal.ReadOnly = True
        FilaSelectLote = Nothing

        lbMDescuento.Visible = True
        lbPDescuento.Visible = True
        tbMdesc.Visible = True
        tbPdesc.Visible = True
    End Sub
    Private Sub _prhabilitar()
        grVentas.Enabled = False
        tbCodigo.ReadOnly = False
        ''  tbCliente.ReadOnly = False  por que solo podra seleccionar Cliente
        ''  tbVendedor.ReadOnly = False
        tbObservacion.ReadOnly = False
        tbFechaVenta.IsInputReadOnly = False
        tbFechaVenc.IsInputReadOnly = False
        swMoneda.IsReadOnly = False
        swTipoVenta.IsReadOnly = False
        btnGrabar.Enabled = True
        btnSearchCliente.Visible = True
        TbNit.ReadOnly = False
        TbNombre1.ReadOnly = False
        TbNombre2.ReadOnly = False

        'Datos facturacion
        tbNroAutoriz.ReadOnly = False
        tbNroFactura.ReadOnly = False
        tbCodigoControl.ReadOnly = False
        dtiFechaFactura.IsInputReadOnly = False

        If (tbCodigo.Text.Length > 0) Then
            cbSucursal.ReadOnly = True
        Else
            'cbSucursal.ReadOnly = False

        End If

        lbMDescuento.Visible = True
        lbPDescuento.Visible = True
        tbMdesc.Visible = True
        tbPdesc.Visible = True


    End Sub
    Public Sub _prFiltrar()
        'cargo el buscador
        Dim _Mpos As Integer
        _prCargarVenta()
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
        tbObservacion.Clear()
        swMoneda.Value = True
        swTipoVenta.Value = True
        _CodCliente = 0
        _CodEmpleado = 0
        tbFechaVenta.Value = Now.Date
        tbFechaVenc.Visible = False
        lbCredito.Visible = False
        _prCargarDetalleVenta(-1)
        MSuperTabControl.SelectedTabIndex = 0
        tbSubTotal.Value = 0
        tbPdesc.Value = 0
        tbMdesc.Value = 0
        tbIce.Value = 0
        tbtotal.Value = 0
        With grdetalle.RootTable.Columns("img")
            .Width = 40
            .Caption = "Eliminar"
            .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
            .Visible = True
        End With

        _prAddDetalleVenta()
        If (GPanelProductos.Visible = True) Then
            GPanelProductos.Visible = False
            PanelTotal.Visible = True
            PanelInferior.Visible = True
        End If
        tbCliente.Focus()

        TbNit.Clear()
        TbNombre1.Clear()
        TbNombre2.Clear()

        tbNroAutoriz.Clear()
        tbNroFactura.Clear()
        tbCodigoControl.Clear()
        dtiFechaFactura.Value = Now.Date
        If (CType(cbSucursal.DataSource, DataTable).Rows.Count > 0) Then
            cbSucursal.SelectedIndex = 0
        End If
        FilaSelectLote = Nothing

        tbCliente.Focus()
        Table_Producto = Nothing


    End Sub
    Public Sub _prMostrarRegistro(_N As Integer)
        '' grVentas.Row = _N
        '     a.tanumi ,a.taalm ,a.tafdoc ,a.taven ,vendedor .yddesc as vendedor ,a.tatven ,a.tafvcr ,a.taclpr,
        'cliente.yddesc as cliente ,a.tamon ,IIF(tamon=1,'Boliviano','Dolar') as moneda,a.taest ,a.taobs ,
        'a.tadesc ,a.tafact ,a.tahact ,a.tauact,(Sum(b.tbptot)-a.tadesc ) as total,taproforma,taCatPrecio

        With grVentas
            cbSucursal.Value = .GetValue("taalm")
            tbCodigo.Text = .GetValue("tanumi")
            tbFechaVenta.Value = .GetValue("tafdoc")
            _CodEmpleado = .GetValue("taven")
            tbVendedor.Text = .GetValue("vendedor")
            swTipoVenta.Value = .GetValue("tatven")
            _CodCliente = .GetValue("taclpr")
            tbCliente.Text = .GetValue("cliente")
            swMoneda.Value = .GetValue("tamon")
            tbObservacion.Text = .GetValue("taobs")



            tbFechaVenc.Value = .GetValue("tafvcr")


            'If (gb_FacturaEmite) Then
            Dim dt As DataTable = L_fnObtenerTabla("TFV001", "fvanitcli, fvadescli1, fvadescli2, fvaautoriz, fvanfac, fvaccont, fvafec", "fvanumi=" + tbCodigo.Text.Trim)
            If (dt.Rows.Count = 1) Then
                TbNit.Text = dt.Rows(0).Item("fvanitcli").ToString
                TbNombre1.Text = dt.Rows(0).Item("fvadescli1").ToString
                TbNombre2.Text = dt.Rows(0).Item("fvadescli2").ToString

                tbNroAutoriz.Text = dt.Rows(0).Item("fvaautoriz").ToString
                tbNroFactura.Text = dt.Rows(0).Item("fvanfac").ToString
                tbCodigoControl.Text = dt.Rows(0).Item("fvaccont").ToString
                dtiFechaFactura.Value = dt.Rows(0).Item("fvafec")
            Else
                TbNit.Clear()
                TbNombre1.Clear()
                TbNombre2.Clear()

                tbNroAutoriz.Clear()
                tbNroFactura.Clear()
                tbCodigoControl.Clear()
                dtiFechaFactura.Value = "2000/01/01"
            End If
            'End If

            lbFecha.Text = CType(.GetValue("tafact"), Date).ToString("dd/MM/yyyy")
            lbHora.Text = .GetValue("tahact").ToString
            lbUsuario.Text = .GetValue("tauact").ToString

        End With

        _prCargarDetalleVenta(tbCodigo.Text)
        tbMdesc.Value = grVentas.GetValue("tadesc")
        tbIce.Value = grVentas.GetValue("taice")
        _prCalcularPrecioTotal()
        LblPaginacion.Text = Str(grVentas.Row + 1) + "/" + grVentas.RowCount.ToString

    End Sub

    Private Sub _prCargarDetalleVenta(_numi As String)
        Dim dt As New DataTable
        dt = L_fnDetalleVentaServicios(_numi)
        grdetalle.DataSource = dt
        grdetalle.RetrieveStructure()
        grdetalle.AlternatingColors = True
        'a.tbnumi ,a.VentaServicioId  ,a.ServicioId,a.tbobs ,a.tbest ,a.tbcmin ,'' as unidad,
        'a.tbpbas , a.tbptot, a.tbporc, a.tbdesc, a.tbtotdesc, a.tbptot2, a.tbfact, a.tbhact, a.tbuact, 1 As estado, Cast(null As Image) as img

        With grdetalle.RootTable.Columns("tbnumi")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = False

        End With

        With grdetalle.RootTable.Columns("VentaServicioId")
            .Width = 90
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("ServicioId")
            .Width = 90
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("NombreServicio")
            .Width = 350
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .WordWrap = True
            .MaxLines = 6
            .Caption = "NombreServicio"
        End With

        With grdetalle.RootTable.Columns("tbobs")
            .Width = 600
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .WordWrap = True
            .MaxLines = 6
            .Caption = "Detalle"
        End With

        'a.tbnumi ,a.VentaServicioId  ,a.ServicioId,a.tbobs ,a.tbest ,a.tbcmin ,'' as unidad,
        'a.tbpbas , a.tbptot, a.tbporc, a.tbdesc, a.tbtotdesc, a.tbptot2, a.tbfact, a.tbhact, a.tbuact, 1 As estado, Cast(null As Image) as img

        With grdetalle.RootTable.Columns("tbest")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With


        With grdetalle.RootTable.Columns("tbcmin")
            .Width = 75
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .FormatString = "0.00"
            .Caption = "Cantidad"
        End With

        With grdetalle.RootTable.Columns("unidad")
            .Width = 70
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .Caption = "Unidad"
        End With
        With grdetalle.RootTable.Columns("tbpbas")
            .Width = 120
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Precio U."
        End With
        With grdetalle.RootTable.Columns("tbptot")
            .Width = 90
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .FormatString = "0.00"
            .Caption = "Sub Total"
        End With
        'a.tbnumi ,a.VentaServicioId  ,a.ServicioId,a.tbobs ,a.tbest ,a.tbcmin ,'' as unidad,
        'a.tbpbas , a.tbptot, a.tbporc, a.tbdesc, a.tbtotdesc, a.tbptot2, a.tbfact, a.tbhact, a.tbuact, 1 As estado, Cast(null As Image) as img
        With grdetalle.RootTable.Columns("tbporc")
            .Width = 80
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .FormatString = "0.00"
            .Caption = "P.Desc(%)"
        End With
        With grdetalle.RootTable.Columns("tbdesc")
            .Width = 80
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .FormatString = "0.00"
            .Caption = "M.Desc"
        End With
        With grdetalle.RootTable.Columns("tbtotdesc")
            .Width = 90
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Total"
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
            .Width = 40
            .Caption = "Eliminar"
            .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
            .Visible = False
        End With


        With grdetalle
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
        End With
    End Sub

    Private Sub _prCargarVenta()
        Dim dt As New DataTable
        dt = L_fnGeneralVentaServicios()
        grVentas.DataSource = dt
        grVentas.RetrieveStructure()
        grVentas.AlternatingColors = True
        'a.tanumi , a.taalm, a.tafdoc, a.taven, vendedor.yddesc As vendedor , a.tatven, a.tafvcr, a.taclpr,
        'cliente.ydrazonsocial As cliente , a.tamon, IIf(tamon = 1,'Boliviano','Dolar') as moneda,a.taest ,a.taobs ,
        'a.tadesc, a.taice, a.tafact, a.tahact, a.tauact, a.tatotal as total

        With grVentas.RootTable.Columns("tanumi")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = True

        End With

        With grVentas.RootTable.Columns("taalm")
            .Width = 90
            .Visible = False
        End With


        With grVentas.RootTable.Columns("tafdoc")
            .Width = 90
            .Visible = True
            .Caption = "FECHA"
        End With

        With grVentas.RootTable.Columns("taven")
            .Width = 160
            .Visible = False
        End With
        With grVentas.RootTable.Columns("vendedor")
            .Width = 250
            .Visible = True
            .Caption = "VENDEDOR".ToUpper
        End With

        'a.tanumi , a.taalm, a.tafdoc, a.taven, vendedor.yddesc As vendedor , a.tatven, a.tafvcr, a.taclpr,
        'cliente.ydrazonsocial As cliente , a.tamon, IIf(tamon = 1,'Boliviano','Dolar') as moneda,a.taest ,a.taobs ,
        'a.tadesc, a.taice, a.tafact, a.tahact, a.tauact, a.tatotal as total
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
            .Caption = "CLIENTE"
        End With

        With grVentas.RootTable.Columns("tamon")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("moneda")
            .Width = 150
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Caption = "MONEDA"
        End With
        'a.tanumi , a.taalm, a.tafdoc, a.taven, vendedor.yddesc As vendedor , a.tatven, a.tafvcr, a.taclpr,
        'cliente.ydrazonsocial As cliente , a.tamon, IIf(tamon = 1,'Boliviano','Dolar') as moneda,a.taest ,a.taobs ,
        'a.tadesc, a.taice, a.tafact, a.tahact, a.tauact, a.tatotal as total
        With grVentas.RootTable.Columns("taobs")
            .Width = 200
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Caption = "OBSERVACION"
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
        With grVentas.RootTable.Columns("total")
            .Width = 150
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



    Private Sub _prCargarProductos()
        If (cbSucursal.SelectedIndex < 0) Then
            Return
        End If

        Dim dtname As DataTable = L_fnNameLabel()
        Dim dt As New DataTable


        dt = L_fnGeneralServicios()  ''1=Almacen
        'Table_Producto = dt.Copy
        dtProductoGoblal = dt




        ''  actualizarSaldoSinLote(dt)
        grProductos.DataSource = dt
        grProductos.RetrieveStructure()
        grProductos.AlternatingColors = True

        '       'a.Id ,a.NombreServicio ,a.DetalleServicio ,a.Estado 

        With grProductos.RootTable.Columns("Id")
            .Width = 100
            .Caption = "Id"
            .MaxLines = 100
            .CellStyle.LineAlignment = TextAlignment.Near
            .WordWrap = True
            .Visible = True
        End With

        With grProductos.RootTable.Columns("NombreServicio")
            .Width = 300
            .Visible = True
            .MaxLines = 100
            .CellStyle.LineAlignment = TextAlignment.Near
            .WordWrap = True
            .Caption = "Nombre Servicio"
        End With





        With grProductos.RootTable.Columns("DetalleServicio")
            .Width = 200
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .WordWrap = True
            .MaxLines = 4
            .Caption = "DetalleServicio"
        End With

        With grProductos.RootTable.Columns("Estado")
            .Width = 120
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False

        End With
        With grProductos.RootTable.Columns("EstadoServicio")
            .Width = 120
            .Visible = False

        End With

        With grProductos
            '.DefaultFilterRowComparison = FilterConditionOperator.BeginsWith
            '.FilterMode = FilterMode.Automatic
            '.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
        End With

    End Sub







    Private Sub _prAddDetalleVenta()

        '        a.tbnumi , a.VentaServicioId, a.ServicioId, a.tbobs, a.tbest, a.tbcmin,'' as unidad,
        'a.tbpbas, a.tbptot, a.tbporc, a.tbdesc, a.tbtotdesc, a.tbptot2, a.tbfact, a.tbhact, a.tbuact, 1 As estado, Cast(null As Image) as img

        Dim Bin As New MemoryStream

        Dim img As New Bitmap(My.Resources.delete, 28, 28)

        img.Save(Bin, Imaging.ImageFormat.Png)
        CType(grdetalle.DataSource, DataTable).Rows.Add(_fnSiguienteNumi() + 1, 0, 0, "", "", 0, 0, "", 0, 0, 0, 0, 0, 0, Now.Date, "", "", 0, Bin.GetBuffer)
    End Sub

    Public Function _fnSiguienteNumi()
        Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)
        Dim rows() As DataRow = dt.Select("tbnumi=MAX(tbnumi)")
        If (rows.Count > 0) Then
            Return rows(rows.Count - 1).Item("tbnumi")
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
        _prCargarProductos()
        'grProductos.Focus()
        'grProductos.MoveTo(grProductos.FilterRow)
        'grProductos.Col = 2
        tbProducto.Clear()
        tbProducto.Focus()

        GPanelProductos.Height = 350
    End Sub
    Private Sub _HabilitarFocoDetalle(fila As Integer, idCategoria As Integer)
        _prCargarProductos()
        grdetalle.Focus()
        grdetalle.Row = fila
        grdetalle.Col = 2
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
            Dim _numi As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("tbnumi")
            If (_numi = numi) Then
                pos = i
                Return
            End If
        Next

    End Sub

    Public Sub _fnObtenerFilaDetalleProducto(ByRef pos As Integer, numi As Integer)
        For i As Integer = 0 To CType(grProductos.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _numi As Integer = CType(grProductos.DataSource, DataTable).Rows(i).Item("ServicioId")
            If (_numi = numi) Then
                pos = i
                Return
            End If
        Next

    End Sub

    Public Function _fnExisteProducto(idprod As Integer) As Boolean
        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _idprod As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("ServicioId")
            Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("estado")
            If (_idprod = idprod And estado >= 0) Then

                Return True
            End If
        Next
        Return False
    End Function


    Public Sub P_PonerTotal(rowIndex As Integer)
        If (rowIndex < grdetalle.RowCount) Then

            Dim lin As Integer = grdetalle.GetValue("tbnumi")
            Dim pos As Integer = -1
            _fnObtenerFilaDetalle(pos, lin)
            Dim cant As Double = grdetalle.GetValue("tbcmin")
            Dim uni As Double = grdetalle.GetValue("tbpbas")

            Dim MontoDesc As Double = grdetalle.GetValue("tbdesc")
            Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)
            If (pos >= 0) Then
                Dim TotalUnitario As Double = cant * uni

                'grDetalle.SetValue("lcmdes", montodesc)

                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot") = TotalUnitario
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = TotalUnitario - MontoDesc
                grdetalle.SetValue("tbptot", TotalUnitario)
                grdetalle.SetValue("tbtotdesc", TotalUnitario - MontoDesc)



                Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado")
                If (estado = 1) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = 2
                End If
            End If
            _prCalcularPrecioTotal()
        End If



    End Sub
    Public Sub _prCalcularPrecioTotal()


        Dim montodesc As Double = tbMdesc.Value
        Dim pordesc As Double = ((montodesc * 100) / grdetalle.GetTotal(grdetalle.RootTable.Columns("tbtotdesc"), AggregateFunction.Sum))
        tbPdesc.Value = pordesc
        tbSubTotal.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("tbtotdesc"), AggregateFunction.Sum)
        tbIce.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("tbptot2"), AggregateFunction.Sum) * (gi_ICE / 100)
        If (gb_FacturaIncluirICE = True) Then
            tbtotal.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("tbtotdesc"), AggregateFunction.Sum) - montodesc + tbIce.Value
        Else
            tbtotal.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("tbtotdesc"), AggregateFunction.Sum) - montodesc
        End If




    End Sub
    Public Sub _prEliminarFila()
        If (grdetalle.Row >= 0) Then
            If (grdetalle.RowCount >= 2) Then
                Dim estado As Integer = grdetalle.GetValue("estado")
                Dim pos As Integer = -1
                Dim lin As Integer = grdetalle.GetValue("tbnumi")
                _fnObtenerFilaDetalle(pos, lin)
                If (estado = 0) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = -2

                End If
                If (estado = 1) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = -1
                End If
                grdetalle.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grdetalle.RootTable.Columns("estado"), Janus.Windows.GridEX.ConditionOperator.GreaterThanOrEqualTo, 0))
                _prCalcularPrecioTotal()
                grdetalle.Select()
                grdetalle.Col = 5
                grdetalle.Row = grdetalle.RowCount - 1
            End If
        End If
        grdetalle.Refetch()
        grdetalle.Refresh()

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
        'Validar datos de factura
        'If (TbNit.Text = String.Empty) Then
        '    Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
        '    ToastNotification.Show(Me, "Por Favor ponga el nit del cliente.".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        '    tbVendedor.Focus()
        '    Return False
        'End If

        'If (TbNombre1.Text = String.Empty) Then
        '    Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
        '    ToastNotification.Show(Me, "Por Favor ponga la razon social del cliente.".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        '    tbVendedor.Focus()
        '    Return False
        'End If

        If (grdetalle.RowCount = 1) Then
            grdetalle.Row = grdetalle.RowCount - 1
            If (grdetalle.GetValue("ServicioId") = 0) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "Por Favor Seleccione  un detalle de producto".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                Return False
            End If

        End If
        Return True
    End Function

    Public Sub _GuardarNuevo()
        Dim numi As String = ""
        Dim res As Boolean = L_fnGrabarVentaServicios(numi, tbFechaVenta.Value.ToString("yyyy/MM/dd"), _CodEmpleado, IIf(swTipoVenta.Value = True, 1, 0), IIf(swTipoVenta.Value = True, Now.Date.ToString("yyyy/MM/dd"), tbFechaVenc.Value.ToString("yyyy/MM/dd")), _CodCliente, IIf(swMoneda.Value = True, 1, 0), tbObservacion.Text, tbMdesc.Value, tbIce.Value, tbtotal.Value, CType(grdetalle.DataSource, DataTable), cbSucursal.Value)


        If res Then
            'res = P_fnGrabarFacturarTFV001(numi)

            If (gb_FacturaEmite) Then
                If TbNit.Text <> String.Empty Then
                    P_fnGenerarFactura(numi)
                    '_prImiprimirNotaVenta(numi)
                Else
                    '_prImiprimirNotaVenta(numi)
                End If
            Else
                _prImiprimirNotaVenta(numi)
            End If

            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "Código de Venta ".ToUpper + tbCodigo.Text + " Grabado con Exito.".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter
                                      )
            _prImiprimirNotaVenta(numi)

            If swTipoVenta.Value = True Then

            End If

            _prCargarVenta()

            _Limpiar()
            Table_Producto = Nothing

        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "La Venta no pudo ser insertado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

        End If

    End Sub
    Public Sub _prImiprimirNotaVenta(numi As String)
        Dim ef = New Efecto


        ef.tipo = 2
        ef.Context = "MENSAJE PRINCIPAL".ToUpper
        ef.Header = "¿desea imprimir la nota de venta?".ToUpper
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then
            P_GenerarReporte(numi)
        End If
    End Sub
    Public Sub _prImiprimirFacturaPreimpresa(numi As String)
        Dim ef = New Efecto


        ef.tipo = 2
        ef.Context = "MENSAJE PRINCIPAL".ToUpper
        ef.Header = "¿desea imprimir la factura Preimpresa?".ToUpper
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then
            P_GenerarReporteFactura(numi)
        End If
    End Sub
    Private Sub _prGuardarModificado()
        Dim numi As String = ""
        Dim res As Boolean = L_fnModificarVentaServicios(numi, tbFechaVenta.Value.ToString("yyyy/MM/dd"), _CodEmpleado, IIf(swTipoVenta.Value = True, 1, 0), IIf(swTipoVenta.Value = True, Now.Date.ToString("yyyy/MM/dd"), tbFechaVenc.Value.ToString("yyyy/MM/dd")), _CodCliente, IIf(swMoneda.Value = True, 1, 0), tbObservacion.Text, tbMdesc.Value, tbIce.Value, tbtotal.Value, CType(grdetalle.DataSource, DataTable), cbSucursal.Value)
        If res Then

            'If (gb_FacturaEmite) Then
            '    L_fnEliminarDatos("TFV001", "fvanumi=" + tbCodigo.Text.Trim)
            '    L_fnEliminarDatos("TFV0011", "fvbnumi=" + tbCodigo.Text.Trim)
            '    P_fnGenerarFactura(tbCodigo.Text.Trim)
            'End If
            _prImiprimirNotaVenta(tbCodigo.Text)

            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "Código de Venta ".ToUpper + tbCodigo.Text + " Modificado con Exito.".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter
                                      )


            _prCargarVenta()

            _prSalir()


        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "La Venta no pudo ser Modificada".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

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
            _tab.Close()
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
    Public Sub _PrimerRegistro()
        Dim _MPos As Integer
        If grVentas.RowCount > 0 Then
            _MPos = 0
            ''   _prMostrarRegistro(_MPos)
            grVentas.Row = _MPos
        End If
    End Sub
    Public Sub InsertarProductosSinLote()

        '        a.tbnumi , a.VentaServicioId, a.ServicioId,NombreServicio, a.tbobs, a.tbest, a.tbcmin,'' as unidad,
        'a.tbpbas, a.tbptot, a.tbporc, a.tbdesc, a.tbtotdesc, a.tbptot2, a.tbfact, a.tbhact, a.tbuact, 1 As estado, Cast(null As Image) as img

        'a.Id , a.NombreServicio, a.DetalleServicio, a.Estado, cast(a.Estado As bit) as EstadoServicio



        Dim existe As Boolean = False
        ''_fnExisteProducto(grProductos.GetValue("Id"))
        Dim DetalleServicio As String = ""
        Dim PrecioServicio As Double = 0
        If ((Not existe)) Then
            Dim pos As Integer = -1
            grdetalle.Row = grdetalle.RowCount - 1
            If (grdetalle.GetValue("ServicioId") <> 0) Then
                _prAddDetalleVenta()
                grdetalle.Row = grdetalle.RowCount - 1
            End If

            _fnObtenerFilaDetalle(pos, grdetalle.GetValue("tbnumi"))

            If (pos >= 0) Then
                Dim ef = New Efecto
                ef.tipo = 8
                ef.NombreServicio = grProductos.GetValue("NombreServicio")

                ef.ShowDialog()
                Dim bandera As Boolean = False
                bandera = ef.band
                If (bandera = True) Then

                    DetalleServicio = ef.DetalleServicio
                    PrecioServicio = ef.PrecioServicio

                Else
                    ToastNotification.Show(Me, "No se Ingreso Detalle de Servicio ni Precio", My.Resources.WARNING, 4000, eToastGlowColor.Red, eToastPosition.TopCenter)

                    Return

                End If


                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("ServicioId") = grProductos.GetValue("Id")
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("NombreServicio") = grProductos.GetValue("NombreServicio")


                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbobs") = DetalleServicio


                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpbas") = PrecioServicio


                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot") = PrecioServicio
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = PrecioServicio
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbcmin") = 1

                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot2") = PrecioServicio


                _prCalcularPrecioTotal()


                '_DesHabilitarProductos()
                tbProducto.Focus()
            End If

        Else
            If (existe) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "El producto ya existe en el detalle".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

                tbProducto.Focus()
            End If
        End If
    End Sub

    Private Function P_fnGenerarFactura(numi As String) As Boolean
        Dim res As Boolean = False
        res = P_fnGrabarFacturarTFV001(numi) ' Grabar en la TFV001
        If (res) Then
            If (P_fnValidarFactura()) Then
                'Validar para facturar
                P_prImprimirFacturar(numi, True, True) '_Codigo de a tabla TV001
            Else
                'Volver todo al estada anterior
                ToastNotification.Show(Me, "No es posible facturar, vuelva a ingresar a la mesa he intente nuevamente!!!".ToUpper,
                                       My.Resources.OK,
                                       5 * 1000,
                                       eToastGlowColor.Red,
                                       eToastPosition.MiddleCenter)
            End If

            If (Not TbNit.Text.Trim.Equals("0")) Then
                L_Grabar_Nit(TbNit.Text.Trim, TbNombre1.Text.Trim, TbNombre2.Text.Trim, "", "")
            Else
                L_Grabar_Nit(TbNit.Text, "S/N", "", "", "")
            End If
        End If

        Return res
    End Function

    Private Function P_fnGrabarFacturarTFV001(numi As String) As Boolean
        Dim a As Double = CDbl(tbtotal.Value + tbMdesc.Value)
        Dim b As Double = CDbl(IIf(IsDBNull(tbIce.Value), 0, tbIce.Value)) 'Ya esta calculado el 55% del ICE
        Dim c As Double = CDbl("0")
        Dim d As Double = CDbl("0")
        Dim e As Double = a - b - c - d
        Dim f As Double = CDbl(tbMdesc.Value)
        Dim g As Double = e - f
        Dim h As Double = g * (gi_IVA / 100)

        Dim res As Boolean = False
        'Grabado de Cabesera Factura
        'L_Grabar_Factura(numi,
        '                dtiFechaFactura.Value.ToString("yyyy/MM/dd"),
        '                IIf(Val(tbNroFactura.Text) = 0, "0", tbNroFactura.Text),
        '                IIf(Val(tbNroAutoriz.Text) = 0, "0", tbNroAutoriz.Text),
        '                "1",
        '                TbNit.Text.Trim,
        '                "0",
        '                TbNombre1.Text,
        '                "",
        '                CStr(Format(a, "####0.00")),
        '                CStr(Format(b, "####0.00")),
        '                CStr(Format(c, "####0.00")),
        '                CStr(Format(d, "####0.00")),
        '                CStr(Format(e, "####0.00")),
        '                CStr(Format(f, "####0.00")),
        '                CStr(Format(g, "####0.00")),
        '                CStr(Format(h, "####0.00")),
        '                "",
        '                Now.Date.ToString("yyyy/MM/dd"),
        '                "''",
        '                "1",
        '                numi, "")

        'Grabado de Detalle de Factura
        grProductos.Update()

        'Dim s As String = ""
        For Each fil As GridEXRow In grdetalle.GetRows
            If (Not fil.Cells("tbcmin").Value.ToString.Trim.Equals("") And
                Not fil.Cells("ServicioId").Value.ToString.Trim.Equals("0")) Then
                's = fil.Cells("codP").Value
                's = fil.Cells("des").Value
                's = fil.Cells("can").Value
                's = fil.Cells("imp").Value

                Dim ServicioId As String = fil.Cells("ServicioId").Value.ToString.Trim
                Dim NombreServicio As String = fil.Cells("NombreServicio").Value.ToString.Trim + " : " + fil.Cells("tbobs").Value.ToString.Trim

                L_Grabar_Factura_Detalle(numi.ToString,
                                        ServicioId,
                                       NombreServicio,
                                        fil.Cells("tbcmin").Value.ToString.Trim,
                                        fil.Cells("tbpbas").Value.ToString.Trim,
                                        numi)
                res = True
            End If
        Next
        Return res
    End Function

    Private Function P_fnValidarFactura() As Boolean
        Return True
    End Function
    Private Sub ReimprimirFactura(numi As String, impFactura As Boolean, grabarPDF As Boolean)
        Try
            Dim _Fecha, _FechaAl As Date
            Dim _Ds, _Ds1, _Ds2, _Ds3 As New DataSet
            Dim _Autorizacion, _Nit, _Fechainv, _Total, _Key, _Cod_Control, _Hora,
                _Literal, _TotalDecimal, _TotalDecimal2 As String
            Dim I, _NumFac, _numidosif, _TotalCC As Integer
            Dim ice, _Desc, _TotalLi As Decimal
            Dim _VistaPrevia As Integer = 0


            _Desc = CDbl(tbMdesc.Value)
            If Not IsNothing(P_Global.Visualizador) Then
                P_Global.Visualizador.Close()
            End If

            _Ds1 = L_Dosificacion("2", "1", _Fecha)
            _Ds = L_Reporte_FacturaServicio(numi, numi)
            _Fecha = _Ds.Tables(0).Rows(0).Item("fvafec").ToString
            _Hora = _Ds.Tables(0).Rows(0).Item("fvahora").ToString
            _Autorizacion = _Ds1.Tables(0).Rows(0).Item("sbautoriz").ToString
            _NumFac = CInt(_Ds.Tables(0).Rows(0).Item("fvanfac").ToString)
            '_NumFac = CInt(_Ds1.Tables(0).Rows(0).Item("sbnfac"))
            _Nit = _Ds.Tables(0).Rows(0).Item("fvanitcli").ToString

            _Fechainv = Microsoft.VisualBasic.Right(_Fecha.ToShortDateString, 4) +
                        Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(_Fecha.ToShortDateString, 5), 2) +
                        Microsoft.VisualBasic.Left(_Fecha.ToShortDateString, 2)
            _Total = _Ds.Tables(0).Rows(0).Item("fvatotal").ToString
            '_Total = "45.82"
            ' _Total = _Ds.Tables(0).Rows(0).Item("fvastot").ToString
            ice = _Ds.Tables(0).Rows(0).Item("fvaimpsi")
            _numidosif = _Ds1.Tables(0).Rows(0).Item("sbnumi").ToString
            _Key = _Ds1.Tables(0).Rows(0).Item("sbkey")
            _FechaAl = _Ds1.Tables(0).Rows(0).Item("sbfal")

            'Dim maxNFac As Integer = L_fnObtenerMaxIdTabla("TFV001", "fvanfac", "fvaautoriz = " + _Autorizacion)
            '_NumFac = maxNFac + 1

            _TotalCC = Math.Round(CDbl(_Total), MidpointRounding.AwayFromZero)
            _Cod_Control = ControlCode.generateControlCode(_Autorizacion, _NumFac, _Nit, _Fechainv, CStr(_TotalCC), _Key)

            'Literal 
            '_TotalLi = "2740.38"
            _TotalLi = _Ds.Tables(0).Rows(0).Item("fvastot") - _Ds.Tables(0).Rows(0).Item("fvadesc")
            _TotalDecimal = _TotalLi - Math.Truncate(_TotalLi)
            '_TotalDecimal = "0.93"
            _TotalDecimal2 = CDbl(_TotalDecimal) * 100

            'Dim li As String = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(_Total) - CDbl(_TotalDecimal)) + " con " + IIf(_TotalDecimal2.Equals("0"), "00", _TotalDecimal2) + "/100 Bolivianos"
            _Literal = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(_TotalLi) - CDbl(_TotalDecimal)) + "  " + IIf(_TotalDecimal2.Equals("0"), "00", _TotalDecimal2) + "/100 Bolivianos"
            _Ds2 = L_Reporte_Factura_Cia("3")
            QrFactura.Text = _Ds2.Tables(0).Rows(0).Item("scnit").ToString + "|" + Str(_NumFac).Trim + "|" + _Autorizacion + "|" + _Fecha + "|" + _Total + "|" + _TotalLi.ToString + "|" + _Cod_Control + "|" + TbNit.Text.Trim + "|" + ice.ToString + "|0|0|" + Str(_Desc).Trim

            L_Modificar_Factura("fvanumi = " + CStr(numi),
                                "",
                                CStr(_NumFac),
                                CStr(_Autorizacion),
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                _Cod_Control,
                                _FechaAl.ToString("yyyy/MM/dd"),
                                "",
                                "",
                                CStr(numi))

            _Ds = L_Reporte_FacturaServicio(numi, numi)

            For I = 0 To _Ds.Tables(0).Rows.Count - 1
                _Ds.Tables(0).Rows(I).Item("fvaimgqr") = P_fnImageToByteArray(QrFactura.Image)
            Next
            If (impFactura) Then
                _Ds3 = L_ObtenerRutaImpresora("1") ' Datos de Impresion de Facturación
                If (_Ds3.Tables(0).Rows(0).Item("cbvp")) Then 'Vista Previa de la Ventana de Vizualización 1 = True 0 = False
                    P_Global.Visualizador = New Visualizador 'Comentar
                End If
                Dim objrep As Object = Nothing
                If (gi_FacturaTipo = 1) Then
                    'objrep = New R_FacturaG
                ElseIf (gi_FacturaTipo = 2) Then
                    objrep = New R_FacturaServicio

                    'If (Not _Ds.Tables(0).Rows.Count = gi_FacturaCantidadItems) Then
                    '    For index = _Ds.Tables(0).Rows.Count To gi_FacturaCantidadItems - 1
                    '        'Insertamos la primera fila con el saldo Inicial
                    '        Dim f As DataRow = _Ds.Tables(0).NewRow
                    '        f.ItemArray() = _Ds.Tables(0).Rows(0).ItemArray
                    '        f.Item("cantidad") = -1
                    '        _Ds.Tables(0).Rows.Add(f)
                    '    Next
                    'End If
                End If

                objrep.SetDataSource(_Ds.Tables(0))
                objrep.SetParameterValue("Hora", _Hora)
                objrep.SetParameterValue("Direccionpr", _Ds2.Tables(0).Rows(0).Item("scdir").ToString)
                objrep.SetParameterValue("Telefonopr", _Ds2.Tables(0).Rows(0).Item("sctelf").ToString)
                objrep.SetParameterValue("Literal1", _Literal)
                'objrep.SetParameterValue("Literal2", " ")
                'objrep.SetParameterValue("Literal3", " ")
                objrep.SetParameterValue("NroFactura", _NumFac)
                objrep.SetParameterValue("NroAutoriz", _Autorizacion)
                'objrep.SetParameterValue("ENombre", _Ds2.Tables(0).Rows(0).Item("scneg").ToString) '?
                objrep.SetParameterValue("ECasaMatriz", _Ds2.Tables(0).Rows(0).Item("scsuc").ToString)
                objrep.SetParameterValue("ECiudadPais", _Ds2.Tables(0).Rows(0).Item("scpai").ToString)
                objrep.SetParameterValue("Tipo", "ORIGINAL")
                'objrep.SetParameterValue("ESFC", _Ds1.Tables(0).Rows(0).Item("sbsfc").ToString)
                objrep.SetParameterValue("ENit", _Ds2.Tables(0).Rows(0).Item("scnit").ToString)
                objrep.SetParameterValue("EActividad", _Ds2.Tables(0).Rows(0).Item("scact").ToString)
                objrep.SetParameterValue("ESms", "''" + _Ds1.Tables(0).Rows(0).Item("sbnota").ToString + "''")
                objrep.SetParameterValue("ESms2", "''" + _Ds1.Tables(0).Rows(0).Item("sbnota2").ToString + "''")
                objrep.SetParameterValue("EDuenho", _Ds2.Tables(0).Rows(0).Item("scnom").ToString) '?
                objrep.SetParameterValue("URLImageLogo", gs_CarpetaRaiz + "\LogoFactura.jpg")
                'objrep.SetParameterValue("URLImageMarcaAgua", gs_CarpetaRaiz + "\MarcaAguaFactura.jpg")

                If (_Ds3.Tables(0).Rows(0).Item("cbvp")) Then 'Vista Previa de la Ventana de Vizualización 1 = True 0 = False
                    P_Global.Visualizador.CrGeneral.ReportSource = objrep 'Comentar
                    P_Global.Visualizador.ShowDialog() 'Comentar
                    P_Global.Visualizador.BringToFront() 'Comentar
                End If

                Dim pd As New PrintDocument()
                pd.PrinterSettings.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
                If (Not pd.PrinterSettings.IsValid) Then
                    ToastNotification.Show(Me, "La Impresora ".ToUpper + _Ds3.Tables(0).Rows(0).Item("cbrut").ToString + Chr(13) + "No Existe".ToUpper,
                                       My.Resources.WARNING, 5 * 1000,
                                       eToastGlowColor.Blue, eToastPosition.BottomRight)
                Else
                    objrep.PrintOptions.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString '"EPSON TM-T20II Receipt5 (1)"
                    objrep.PrintToPrinter(1, False, 1, 1)
                End If

                'If (grabarPDF) Then
                '    'Copia de Factura en PDF
                '    If (Not Directory.Exists(gs_CarpetaRaiz + "\Facturas")) Then
                '        Directory.CreateDirectory(gs_CarpetaRaiz + "\Facturas")
                '    End If
                '    objrep.ExportToDisk(ExportFormatType.PortableDocFormat, gs_CarpetaRaiz + "\Facturas\" + CStr(_NumFac) + "_" + CStr(_Autorizacion) + ".pdf")

                'End If
            End If
            'L_Actualiza_Dosificacion(_numidosif, _NumFac, numi)
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub

    Private Sub P_prImprimirFacturar(numi As String, impFactura As Boolean, grabarPDF As Boolean)
        Dim _Fecha, _FechaAl As Date
        Dim _Ds, _Ds1, _Ds2, _Ds3 As New DataSet
        Dim _Autorizacion, _Nit, _Fechainv, _Total, _Key, _Cod_Control, _Hora,
            _Literal, _TotalDecimal, _TotalDecimal2 As String
        Dim I, _NumFac, _numidosif, _TotalCC As Integer
        Dim ice, _Desc, _TotalLi As Decimal
        Dim _VistaPrevia As Integer = 0


        _Desc = CDbl(tbMdesc.Value)
        If Not IsNothing(P_Global.Visualizador) Then
            P_Global.Visualizador.Close()
        End If

        _Fecha = Now.Date '.ToString("dd/MM/yyyy")
        _Hora = Now.Hour.ToString + ":" + Now.Minute.ToString
        _Ds1 = L_Dosificacion("2", "1", _Fecha)

        _Ds = L_Reporte_FacturaServicio(numi, numi)
        _Autorizacion = _Ds1.Tables(0).Rows(0).Item("sbautoriz").ToString
        _NumFac = CInt(_Ds1.Tables(0).Rows(0).Item("sbnfac")) + 1
        _Nit = _Ds.Tables(0).Rows(0).Item("fvanitcli").ToString
        _Fechainv = Microsoft.VisualBasic.Right(_Fecha.ToShortDateString, 4) +
                    Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(_Fecha.ToShortDateString, 5), 2) +
                    Microsoft.VisualBasic.Left(_Fecha.ToShortDateString, 2)
        _Total = _Ds.Tables(0).Rows(0).Item("fvatotal").ToString
        ice = _Ds.Tables(0).Rows(0).Item("fvaimpsi")
        _numidosif = _Ds1.Tables(0).Rows(0).Item("sbnumi").ToString
        _Key = _Ds1.Tables(0).Rows(0).Item("sbkey")
        _FechaAl = _Ds1.Tables(0).Rows(0).Item("sbfal")

        Dim maxNFac As Integer = L_fnObtenerMaxIdTabla("TFV001", "fvanfac", "fvaautoriz = " + _Autorizacion)
        _NumFac = maxNFac + 1

        _TotalCC = Math.Round(CDbl(_Total), MidpointRounding.AwayFromZero)
        _Cod_Control = ControlCode.generateControlCode(_Autorizacion, _NumFac, _Nit, _Fechainv, CStr(_TotalCC), _Key)

        'Literal 
        _TotalLi = _Ds.Tables(0).Rows(0).Item("fvastot") - _Ds.Tables(0).Rows(0).Item("fvadesc")
        _TotalDecimal = _TotalLi - Math.Truncate(_TotalLi)
        _TotalDecimal2 = CDbl(_TotalDecimal) * 100

        'Dim li As String = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(_Total) - CDbl(_TotalDecimal)) + " con " + IIf(_TotalDecimal2.Equals("0"), "00", _TotalDecimal2) + "/100 Bolivianos"
        _Literal = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(_TotalLi) - CDbl(_TotalDecimal)) + " con " + IIf(_TotalDecimal2.Equals("0"), "00", _TotalDecimal2) + "/100 Bolivianos"
        _Ds2 = L_Reporte_Factura_Cia("3")
        QrFactura.Text = _Ds2.Tables(0).Rows(0).Item("scnit").ToString + "|" + Str(_NumFac).Trim + "|" + _Autorizacion + "|" + _Fecha + "|" + _Total + "|" + _TotalLi.ToString + "|" + _Cod_Control + "|" + TbNit.Text.Trim + "|" + ice.ToString + "|0|0|" + Str(_Desc).Trim

        L_Modificar_Factura("fvanumi = " + CStr(numi),
                            "",
                            CStr(_NumFac),
                            CStr(_Autorizacion),
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            _Cod_Control,
                            _FechaAl.ToString("yyyy/MM/dd"),
                            "",
                            "",
                            CStr(numi))

        _Ds = L_Reporte_FacturaServicio(numi, numi)

        For I = 0 To _Ds.Tables(0).Rows.Count - 1
            _Ds.Tables(0).Rows(I).Item("fvaimgqr") = P_fnImageToByteArray(QrFactura.Image)
        Next
        If (impFactura) Then
            _Ds3 = L_ObtenerRutaImpresora("1") ' Datos de Impresion de Facturación
            If (_Ds3.Tables(0).Rows(0).Item("cbvp")) Then 'Vista Previa de la Ventana de Vizualización 1 = True 0 = False
                P_Global.Visualizador = New Visualizador 'Comentar
            End If
            Dim objrep As Object = Nothing
            If (gi_FacturaTipo = 1) Then
                'objrep = New R_FacturaG
            ElseIf (gi_FacturaTipo = 2) Then
                objrep = New R_FacturaServicio

                'If (Not _Ds.Tables(0).Rows.Count = gi_FacturaCantidadItems) Then
                '    For index = _Ds.Tables(0).Rows.Count To gi_FacturaCantidadItems - 1
                '        'Insertamos la primera fila con el saldo Inicial
                '        Dim f As DataRow = _Ds.Tables(0).NewRow
                '        f.ItemArray() = _Ds.Tables(0).Rows(0).ItemArray
                '        f.Item("cantidad") = -1
                '        _Ds.Tables(0).Rows.Add(f)
                '    Next
                'End If
            End If

            objrep.SetDataSource(_Ds.Tables(0))
            objrep.SetParameterValue("Hora", _Hora)
            objrep.SetParameterValue("Direccionpr", _Ds2.Tables(0).Rows(0).Item("scdir").ToString)
            objrep.SetParameterValue("Telefonopr", _Ds2.Tables(0).Rows(0).Item("sctelf").ToString)
            objrep.SetParameterValue("Literal1", _Literal)
            'objrep.SetParameterValue("Literal2", " ")
            'objrep.SetParameterValue("Literal3", " ")
            objrep.SetParameterValue("NroFactura", _NumFac)
            objrep.SetParameterValue("NroAutoriz", _Autorizacion)
            'objrep.SetParameterValue("ENombre", _Ds2.Tables(0).Rows(0).Item("scneg").ToString) '?
            objrep.SetParameterValue("ECasaMatriz", _Ds2.Tables(0).Rows(0).Item("scsuc").ToString)
            objrep.SetParameterValue("ECiudadPais", _Ds2.Tables(0).Rows(0).Item("scpai").ToString)
            objrep.SetParameterValue("Tipo", "ORIGINAL")
            'objrep.SetParameterValue("ESFC", _Ds1.Tables(0).Rows(0).Item("sbsfc").ToString)
            objrep.SetParameterValue("ENit", _Ds2.Tables(0).Rows(0).Item("scnit").ToString)
            objrep.SetParameterValue("EActividad", _Ds2.Tables(0).Rows(0).Item("scact").ToString)
            objrep.SetParameterValue("ESms", "''" + _Ds1.Tables(0).Rows(0).Item("sbnota").ToString + "''")
            objrep.SetParameterValue("ESms2", "''" + _Ds1.Tables(0).Rows(0).Item("sbnota2").ToString + "''")
            objrep.SetParameterValue("EDuenho", _Ds2.Tables(0).Rows(0).Item("scnom").ToString) '?
            objrep.SetParameterValue("URLImageLogo", gs_CarpetaRaiz + "\LogoFactura.jpg")
            'objrep.SetParameterValue("URLImageMarcaAgua", gs_CarpetaRaiz + "\MarcaAguaFactura.jpg")

            If (_Ds3.Tables(0).Rows(0).Item("cbvp")) Then 'Vista Previa de la Ventana de Vizualización 1 = True 0 = False
                P_Global.Visualizador.CrGeneral.ReportSource = objrep 'Comentar
                P_Global.Visualizador.ShowDialog() 'Comentar
                P_Global.Visualizador.BringToFront() 'Comentar
            End If

            Dim pd As New PrintDocument()
            pd.PrinterSettings.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
            If (Not pd.PrinterSettings.IsValid) Then
                ToastNotification.Show(Me, "La Impresora ".ToUpper + _Ds3.Tables(0).Rows(0).Item("cbrut").ToString + Chr(13) + "No Existe".ToUpper,
                                       My.Resources.WARNING, 5 * 1000,
                                       eToastGlowColor.Blue, eToastPosition.BottomRight)
            Else
                objrep.PrintOptions.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString '"EPSON TM-T20II Receipt5 (1)"
                objrep.PrintToPrinter(1, False, 1, 1)
            End If

            If (grabarPDF) Then
                'Copia de Factura en PDF
                If (Not Directory.Exists(gs_CarpetaRaiz + "\Facturas")) Then
                    Directory.CreateDirectory(gs_CarpetaRaiz + "\Facturas")
                End If
                objrep.ExportToDisk(ExportFormatType.PortableDocFormat, gs_CarpetaRaiz + "\Facturas\" + CStr(_NumFac) + "_" + CStr(_Autorizacion) + ".pdf")

            End If
        End If
        L_Actualiza_Dosificacion(_numidosif, _NumFac, numi)
    End Sub

    Public Function P_fnImageToByteArray(ByVal imageIn As Image) As Byte()
        Dim ms As New System.IO.MemoryStream()
        imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
        Return ms.ToArray()
    End Function


    Private Function P_fnValidarFacturaVigente() As Boolean
        Dim est As String = L_fnObtenerDatoTabla("TFV001", "fvaest", "fvanumi=" + tbCodigo.Text.Trim)
        Return (est.Equals("True") Or est = String.Empty)
    End Function

    Private Sub P_prCargarVariablesIndispensables()
        If (gb_FacturaEmite) Then
            gi_IVA = CDbl(IIf(L_fnGetIVA().Rows(0).Item("scdebfis").ToString.Equals(""), gi_IVA, L_fnGetIVA().Rows(0).Item("scdebfis").ToString))
            gi_ICE = CDbl(IIf(L_fnGetICE().Rows(0).Item("scice").ToString.Equals(""), gi_ICE, L_fnGetICE().Rows(0).Item("scice").ToString))
        End If

    End Sub

    Private Sub P_prCargarParametro()
        'El sistema factura?
        GroupPanelFactura.Visible = True 'gb_FacturaEmite

        'Si factura, preguntar si, Se incluye el Importe ICE / IEHD / TASAS?
        If (gb_FacturaEmite) Then
            lbIce.Visible = gb_FacturaIncluirICE
            tbIce.Visible = gb_FacturaIncluirICE
        Else
            lbIce.Visible = False
            tbIce.Visible = False
        End If

    End Sub
    Private Sub P_GenerarReporte(numi As String)
        Dim dt As DataTable = L_fnVentaNotaDeVentaServicios(numi)

        Dim total As Decimal = dt.Compute("SUM(tbptot)", "")
        Dim totald As Double = (total / 6.96)
        Dim fechaven As String = dt.Rows(0).Item("FechaVenta")
        If Not IsNothing(P_Global.Visualizador) Then
            P_Global.Visualizador.Close()
        End If
        Dim ParteEntera As Long
        Dim ParteDecimal As Decimal
        ParteEntera = Int(total)
        ParteDecimal = Math.Round(total - ParteEntera, 2)
        Dim li As String = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(ParteEntera)) + " con " +
        IIf(ParteDecimal.ToString.Equals("0"), "00", ParteDecimal.ToString) + "/100 Bolivianos"

        ParteEntera = Int(totald)
        ParteDecimal = Math.Round(totald - ParteEntera, 2)
        Dim lid As String = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(ParteEntera)) + " con " +
        IIf(ParteDecimal.ToString.Equals("0"), "00", ParteDecimal.ToString) + "/100 Dolares"

        Dim dt2 As DataTable = L_fnNameReporte()

        P_Global.Visualizador = New Visualizador
        Dim _FechaAct As String
        Dim _FechaPar As String
        Dim _Fecha() As String
        Dim _Meses() As String = {"Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"}
        _FechaAct = fechaven
        '_Fecha = Split(_FechaAct, "-")
        '_FechaPar = "Cochabamba, " + _Fecha(0).Trim + " De " + _Meses(_Fecha(1) - 1).Trim + " Del " + _Fecha(2).Trim

        Dim objrep As New Rep_NotaVentaServicio

        objrep.SetDataSource(dt)
        objrep.SetParameterValue("Literal", li)

        P_Global.Visualizador.CrGeneral.ReportSource = objrep 'Comentar
        P_Global.Visualizador.Show() 'Comentar
        P_Global.Visualizador.BringToFront() 'Comentar


    End Sub

    Private Sub ponerDescripcionProducto(ByRef dt As DataTable)
        For Each fila As DataRow In dt.Rows
            Dim numi As Integer = fila.Item("codProducto")
            Dim dtDP As DataTable = L_fnDetalleProducto(numi)
            Dim des As String = fila.Item("producto") + vbNewLine + vbNewLine
            For Each fila2 As DataRow In dtDP.Rows
                des = des + fila2.Item("yfadesc").ToString + vbNewLine
            Next
            fila.Item("producto") = des
        Next
    End Sub

    Private Sub P_GenerarReporteFactura(numi As String)
        Dim dt As DataTable = L_fnVentaFactura(numi)
        Dim total As Double = dt.Compute("SUM(Total)", "")

        Dim ParteEntera As Long
        Dim ParteDecimal As Double
        ParteEntera = Int(total)
        ParteDecimal = total - ParteEntera
        Dim li As String = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(ParteEntera)) + " con " +
        IIf(ParteDecimal.ToString.Equals("0"), "00", ParteDecimal.ToString) + "/100 Bolivianos"





        Dim objrep As New R_FacturaFarmacia
        '' GenerarNro(_dt)
        ''objrep.SetDataSource(Dt1Kardex)
        'imprimir
        If PrintDialog1.ShowDialog = DialogResult.OK Then
            objrep.SetDataSource(dt)
            objrep.SetParameterValue("TotalEscrito", li)
            objrep.SetParameterValue("nit", TbNit.Text)
            objrep.SetParameterValue("Total", total)
            objrep.SetParameterValue("cliente", TbNombre1.Text + " " + TbNombre2.Text)
            objrep.PrintOptions.PrinterName = PrintDialog1.PrinterSettings.PrinterName

            objrep.PrintToPrinter(1, False, 1, 1)
            objrep.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
        End If
        'objrep.SetDataSource(dt)
        'objrep.SetParameterValue("TotalEscrito", li)
        'objrep.SetParameterValue("nit", TbNit.Text)
        'objrep.SetParameterValue("Total", total)
        'P_Global.Visualizador.CrGeneral.ReportSource = objrep 'Comentar
        'P_Global.Visualizador.Show() 'Comentar
        'P_Global.Visualizador.BringToFront() 'Comentar



    End Sub





#End Region

#Region "Eventos Formulario"
    Private Sub F0_Ventas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _IniciarTodo()
    End Sub
    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        _Limpiar()
        _prhabilitar()

        btnNuevo.Enabled = False
        btnModificar.Enabled = False
        btnEliminar.Enabled = False
        btnGrabar.Enabled = True
        PanelNavegacion.Enabled = False

        'btnNuevo.Enabled = False
        'btnModificar.Enabled = False
        'btnEliminar.Enabled = False
        'GPanelProductos.Visible = False
        '_prhabilitar()

        '_Limpiar()
    End Sub
    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _prSalir()

    End Sub



    Private Sub tbCliente_KeyDown(sender As Object, e As KeyEventArgs) Handles tbCliente.KeyDown
        If (_fnAccesible()) Then
            If e.KeyData = Keys.Control + Keys.Enter Then

                Dim dt As DataTable

                dt = L_fnListarClientes()
                '              a.ydnumi, a.ydcod, a.yddesc, a.yddctnum, a.yddirec
                ',a.ydtelf1 ,a.ydfnac 

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
                    _dias = Row.Cells("yddias").Value

                    Dim numiVendedor As Integer = IIf(IsDBNull(Row.Cells("ydnumivend").Value), 0, Row.Cells("ydnumivend").Value)
                    If (numiVendedor > 0) Then
                        tbVendedor.Text = Row.Cells("vendedor").Value
                        _CodEmpleado = Row.Cells("ydnumivend").Value

                        grdetalle.Select()
                        Table_Producto = Nothing
                    Else
                        tbVendedor.Clear()
                        _CodEmpleado = 0
                        tbVendedor.Focus()
                        Table_Producto = Nothing

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
                    _CodEmpleado = Row.Cells("ydnumi").Value
                    tbVendedor.Text = Row.Cells("yddesc").Value
                    grdetalle.Select()

                End If

            End If

        End If
    End Sub
    Private Sub swTipoVenta_ValueChanged(sender As Object, e As EventArgs) Handles swTipoVenta.ValueChanged
        If (swTipoVenta.Value = False) Then
            lbCredito.Visible = True
            tbFechaVenc.Visible = True
            tbFechaVenc.Value = DateAdd(DateInterval.Day, _dias, Now.Date)
        Else
            lbCredito.Visible = False
            tbFechaVenc.Visible = False
        End If
    End Sub

    Private Sub grdetalle_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grdetalle.EditingCell
        If (_fnAccesible()) Then
            'Habilitar solo las columnas de Precio, %, Monto y Observación
            'If (e.Column.Index = grdetalle.RootTable.Columns("yfcbarra").Index Or
            If (e.Column.Index = grdetalle.RootTable.Columns("tbcmin").Index Or
                e.Column.Index = grdetalle.RootTable.Columns("tbporc").Index Or
                e.Column.Index = grdetalle.RootTable.Columns("tbpbas").Index Or
                e.Column.Index = grdetalle.RootTable.Columns("tbdesc").Index Or
                e.Column.Index = grdetalle.RootTable.Columns("tbobs").Index) Then
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
            If _codeBar = 1 Then
                If gb_CodigoBarra Then
                    grdetalle.Col = 3
                    grdetalle.Row = 0
                Else
                    grdetalle.Col = 5
                    grdetalle.Row = 0
                End If
            End If
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

            If (grdetalle.Col = grdetalle.RootTable.Columns("tbcmin").Index) Then
                If (grdetalle.GetValue("producto") <> String.Empty) Then

                    _HabilitarProductos()


                Else
                    ToastNotification.Show(Me, "Seleccione un Producto Por Favor", My.Resources.WARNING, 3000, eToastGlowColor.Red, eToastPosition.TopCenter)
                End If

            End If
            If (grdetalle.Col = grdetalle.RootTable.Columns("producto").Index) Then
                If (grdetalle.GetValue("producto") <> String.Empty) Then
                    _HabilitarProductos()
                Else
                    ToastNotification.Show(Me, "Seleccione un Producto Por Favor", My.Resources.WARNING, 3000, eToastGlowColor.Red, eToastPosition.TopCenter)
                End If

            End If
            'If (grdetalle.Col = grdetalle.RootTable.Columns("yfcbarra").Index) Then
            '    If (grdetalle.GetValue("yfcbarra").ToString().Trim() <> String.Empty) Then
            '        cargarProductos()
            '        If (grdetalle.Row = grdetalle.RowCount - 1) Then
            '            If (existeProducto(grdetalle.GetValue("yfcbarra").ToString)) Then
            '                If (Not verificarExistenciaUnica(grdetalle.GetValue("yfcbarra").ToString)) Then
            '                    ponerProducto(grdetalle.GetValue("yfcbarra").ToString)
            '                    _prAddDetalleVenta()
            '                Else
            '                    sumarCantidad(grdetalle.GetValue("yfcbarra").ToString)
            '                End If
            '            Else
            '                grdetalle.DataChanged = False
            '                ToastNotification.Show(Me, "El código de barra del producto no existe", My.Resources.WARNING, 3000, eToastGlowColor.Red, eToastPosition.TopCenter)
            '            End If
            '        Else
            '            grdetalle.DataChanged = False
            '            grdetalle.Row = grdetalle.RowCount - 1
            '            grdetalle.Col = grdetalle.RootTable.Columns("yfcbarra").Index
            '            ToastNotification.Show(Me, "El cursor debe situarse en la ultima fila", My.Resources.WARNING, 3000, eToastGlowColor.Red, eToastPosition.TopCenter)
            '        End If
            '    Else
            '        ToastNotification.Show(Me, "El código de barra no puede quedar vacio", My.Resources.WARNING, 3000, eToastGlowColor.Red, eToastPosition.TopCenter)
            '    End If

            'End If
            'opcion para cargar la grilla con el codigo de barra
            'If (grdetalle.Col = grdetalle.RootTable.Columns("yfcbarra").Index) Then

            '    If (grdetalle.GetValue("yfcbarra") <> String.Empty) Then
            '        _buscarRegistro(grdetalle.GetValue("yfcbarra"))


            '        '_prAddDetalleVenta()
            '        '_HabilitarProductos()
            '        ' MsgBox("hola de la grilla" + grdetalle.GetValue("yfcbarra") + t.Container.ToString)
            '        'ojo
            '    Else
            '        ToastNotification.Show(Me, "Seleccione un Producto Por Favor", My.Resources.WARNING, 3000, eToastGlowColor.Red, eToastPosition.TopCenter)
            '    End If

            'End If
salirIf:
        End If

        If (e.KeyData = Keys.Control + Keys.Enter And grdetalle.Row >= 0 And
            grdetalle.Col = grdetalle.RootTable.Columns("NombreServicio").Index) Then
            Dim indexfil As Integer = grdetalle.Row
            Dim indexcol As Integer = grdetalle.Col
            _HabilitarProductos()


        End If

        If (e.KeyData = Keys.Control + Keys.Enter And grdetalle.Row >= 0 And
            grdetalle.Col = grdetalle.RootTable.Columns("tbobs").Index) Then
            Dim indexfil As Integer = grdetalle.Row
            Dim indexcol As Integer = grdetalle.Col
            _HabilitarProductos()


        End If
        If (e.KeyData = Keys.Escape And grdetalle.Row >= 0) Then

            _prEliminarFila()


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


                ''''''''''''''''''''''''

                InsertarProductosSinLote()

                '''''''''''''''


            End If
        End If
        If e.KeyData = Keys.Escape Then
            _DesHabilitarProductos()
            FilaSelectLote = Nothing
        End If
    End Sub
    Private Sub grdetalle_CellValueChanged(sender As Object, e As ColumnActionEventArgs) Handles grdetalle.CellValueChanged
        If (e.Column.Index = grdetalle.RootTable.Columns("tbcmin").Index) Or (e.Column.Index = grdetalle.RootTable.Columns("tbpbas").Index) Then
            If (Not IsNumeric(grdetalle.GetValue("tbcmin")) Or grdetalle.GetValue("tbcmin").ToString = String.Empty) Then

                'grDetalle.GetRow(rowIndex).Cells("cant").Value = 1
                '  grDetalle.CurrentRow.Cells.Item("cant").Value = 1
                Dim lin As Integer = grdetalle.GetValue("tbnumi")
                Dim pos As Integer = -1
                _fnObtenerFilaDetalle(pos, lin)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbcmin") = 1
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpbas")

                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbporc") = 0
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbdesc") = 0
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpbas")
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot2") = 0
                'grdetalle.SetValue("tbcmin", 1)
                'grdetalle.SetValue("tbptot", grdetalle.GetValue("tbpbas"))
            Else
                If (grdetalle.GetValue("tbcmin") > 0) Then
                    Dim rowIndex As Integer = grdetalle.Row
                    Dim porcdesc As Double = grdetalle.GetValue("tbporc")
                    Dim montodesc As Double = ((grdetalle.GetValue("tbpbas") * grdetalle.GetValue("tbcmin")) * (porcdesc / 100))
                    Dim lin As Integer = grdetalle.GetValue("tbnumi")
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, lin)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbdesc") = montodesc
                    grdetalle.SetValue("tbdesc", montodesc)
                    P_PonerTotal(rowIndex)

                Else
                    Dim lin As Integer = grdetalle.GetValue("tbnumi")
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, lin)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbcmin") = 1
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpbas")
                    _prCalcularPrecioTotal()
                    'grdetalle.SetValue("tbcmin", 1)
                    'grdetalle.SetValue("tbptot", grdetalle.GetValue("tbpbas"))

                End If
            End If
        End If
        '''''''''''''''''''''PORCENTAJE DE DESCUENTO '''''''''''''''''''''
        If (e.Column.Index = grdetalle.RootTable.Columns("tbporc").Index) Then
            If (Not IsNumeric(grdetalle.GetValue("tbporc")) Or grdetalle.GetValue("tbporc").ToString = String.Empty) Then

                'grDetalle.GetRow(rowIndex).Cells("cant").Value = 1
                '  grDetalle.CurrentRow.Cells.Item("cant").Value = 1
                Dim lin As Integer = grdetalle.GetValue("tbnumi")
                Dim pos As Integer = -1
                _fnObtenerFilaDetalle(pos, lin)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbporc") = 0
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbdesc") = 0
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot")
                'grdetalle.SetValue("tbcmin", 1)
                'grdetalle.SetValue("tbptot", grdetalle.GetValue("tbpbas"))
            Else
                If (grdetalle.GetValue("tbporc") > 0) Then

                    Dim porcdesc As Double = grdetalle.GetValue("tbporc")
                    Dim montodesc As Double = (grdetalle.GetValue("tbptot") * (porcdesc / 100))
                    Dim lin As Integer = grdetalle.GetValue("tbnumi")
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, lin)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbdesc") = montodesc
                    grdetalle.SetValue("tbdesc", montodesc)

                    Dim rowIndex As Integer = grdetalle.Row
                    P_PonerTotal(rowIndex)

                Else
                    Dim lin As Integer = grdetalle.GetValue("tbnumi")
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, lin)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbporc") = 0
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbdesc") = 0
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot")
                    grdetalle.SetValue("tbporc", 0)
                    grdetalle.SetValue("tbdesc", 0)
                    grdetalle.SetValue("tbtotdesc", grdetalle.GetValue("tbptot"))
                    _prCalcularPrecioTotal()

                    ToastNotification.Show(Me, "El Porcentaje de Descuento Es Menor a 0", My.Resources.WARNING, 4000, eToastGlowColor.Red, eToastPosition.BottomLeft)
                    'grdetalle.SetValue("tbcmin", 1)
                    'grdetalle.SetValue("tbptot", grdetalle.GetValue("tbpbas"))

                End If
            End If
        End If


        '''''''''''''''''''''MONTO DE DESCUENTO '''''''''''''''''''''
        If (e.Column.Index = grdetalle.RootTable.Columns("tbdesc").Index) Then
            If (Not IsNumeric(grdetalle.GetValue("tbdesc")) Or grdetalle.GetValue("tbdesc").ToString = String.Empty) Then

                'grDetalle.GetRow(rowIndex).Cells("cant").Value = 1
                '  grDetalle.CurrentRow.Cells.Item("cant").Value = 1
                Dim lin As Integer = grdetalle.GetValue("tbnumi")
                Dim pos As Integer = -1
                _fnObtenerFilaDetalle(pos, lin)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbporc") = 0
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbdesc") = 0
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot")
                'grdetalle.SetValue("tbcmin", 1)
                'grdetalle.SetValue("tbptot", grdetalle.GetValue("tbpbas"))
            Else
                Dim montodesc As Double = grdetalle.GetValue("tbdesc")
                Dim pordesc As Double = ((montodesc * 100) / grdetalle.GetValue("tbptot"))

                If (grdetalle.GetValue("tbdesc") > 0) Then



                    Dim lin As Integer = grdetalle.GetValue("tbnumi")
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, lin)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbdesc") = montodesc
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbporc") = pordesc

                    grdetalle.SetValue("tbporc", pordesc)
                    Dim rowIndex As Integer = grdetalle.Row
                    P_PonerTotal(rowIndex)

                Else
                    Dim lin As Integer = grdetalle.GetValue("tbnumi")
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, lin)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbporc") = 0
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbdesc") = 0
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot")
                    grdetalle.SetValue("tbporc", 0)
                    grdetalle.SetValue("tbdesc", 0)
                    grdetalle.SetValue("tbtotdesc", grdetalle.GetValue("tbptot"))
                    _prCalcularPrecioTotal()

                    'grdetalle.SetValue("tbcmin", 1)
                    'grdetalle.SetValue("tbptot", grdetalle.GetValue("tbpbas"))

                End If
            End If
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
                    Dim montodesc As Double = (grdetalle.GetTotal(grdetalle.RootTable.Columns("tbtotdesc"), AggregateFunction.Sum) * (porcdesc / 100))
                    tbMdesc.Value = montodesc

                    tbIce.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("tbptot2"), AggregateFunction.Sum) * (gi_ICE / 100)

                    If (gb_FacturaIncluirICE = True) Then
                        tbtotal.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("tbtotdesc"), AggregateFunction.Sum) - montodesc + tbIce.Value
                    Else
                        tbtotal.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("tbtotdesc"), AggregateFunction.Sum) - montodesc
                    End If
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
                    Dim pordesc As Double = ((montodesc * 100) / grdetalle.GetTotal(grdetalle.RootTable.Columns("tbtotdesc"), AggregateFunction.Sum))
                    tbPdesc.Value = pordesc
                    tbIce.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("tbptot2"), AggregateFunction.Sum) * (gi_ICE / 100)
                    If (gb_FacturaIncluirICE = True) Then
                        tbtotal.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("tbtotdesc"), AggregateFunction.Sum) - montodesc + tbIce.Value
                    Else
                        tbtotal.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("tbtotdesc"), AggregateFunction.Sum) - montodesc
                    End If

                End If

            End If

            If (tbMdesc.Text = String.Empty) Then
                tbMdesc.Value = 0

            End If
        End If

    End Sub

    Private Sub grdetalle_CellEdited(sender As Object, e As ColumnActionEventArgs) Handles grdetalle.CellEdited
        If (e.Column.Index = grdetalle.RootTable.Columns("tbcmin").Index) Then
            If (Not IsNumeric(grdetalle.GetValue("tbcmin")) Or grdetalle.GetValue("tbcmin").ToString = String.Empty) Then

                grdetalle.SetValue("tbcmin", 1)
                grdetalle.SetValue("tbptot", grdetalle.GetValue("tbpbas"))
                grdetalle.SetValue("tbporc", 0)
                grdetalle.SetValue("tbdesc", 0)
                grdetalle.SetValue("tbtotdesc", grdetalle.GetValue("tbpbas"))


            Else
                If (grdetalle.GetValue("tbcmin") > 0) Then

                    Dim cant As Integer = grdetalle.GetValue("tbcmin")


                Else

                    grdetalle.SetValue("tbcmin", 1)
                    grdetalle.SetValue("tbptot", grdetalle.GetValue("tbpbas"))
                    grdetalle.SetValue("tbporc", 0)
                    grdetalle.SetValue("tbdesc", 0)
                    grdetalle.SetValue("tbtotdesc", grdetalle.GetValue("tbpbas"))

                End If
            End If
        End If
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
    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        If _ValidarCampos() = False Then
            Exit Sub
        End If

        If (tbCodigo.Text = String.Empty) Then
            _GuardarNuevo()
        Else
            If (tbCodigo.Text <> String.Empty) Then
                _prGuardarModificado()
                ''    _prInhabiliitar() RODRIGO RLA

            End If
        End If

    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        If (grVentas.RowCount > 0) Then
            If (gb_FacturaEmite) Then
                If (Not P_fnValidarFacturaVigente()) Then
                    Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)

                    ToastNotification.Show(Me, "No se puede modificar la venta con codigo ".ToUpper + tbCodigo.Text + ", su factura esta anulada.".ToUpper,
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
            _prCargarIconELiminar()
        End If
    End Sub
    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click

        If (gb_FacturaEmite) Then
            If (P_fnValidarFacturaVigente()) Then
                Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)

                ToastNotification.Show(Me, "No se puede eliminar la venta con codigo ".ToUpper + tbCodigo.Text + ", su factura esta vigente.".ToUpper,
                                          img, 2000,
                                          eToastGlowColor.Green,
                                          eToastPosition.TopCenter)
                Exit Sub
            End If
        End If

        Dim ef = New Efecto


        ef.tipo = 2
        ef.Context = "¿esta seguro de eliminar el registro?".ToUpper
        ef.Header = "mensaje principal".ToUpper
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then
            Dim mensajeError As String = ""
            Dim res As Boolean = L_fnEliminarVenta(tbCodigo.Text, mensajeError, P_Principal.btVentaServicios.Text)
            If res Then


                Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)

                ToastNotification.Show(Me, "Código de Venta ".ToUpper + tbCodigo.Text + " eliminado con Exito.".ToUpper,
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

    Private Sub TbNit_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TbNit.Validating
        Dim nom1, nom2 As String
        nom1 = ""
        nom2 = ""
        If (TbNit.Text.Trim = String.Empty) Then
            TbNit.Text = "0"
        End If
        L_Validar_Nit(TbNit.Text.Trim, nom1, nom2, "", "", "")
        TbNombre1.Text = nom1
        TbNombre2.Text = nom2
    End Sub
    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        'If (Not _fnAccesible()) Then
        '    P_GenerarReporte(tbCodigo.Text)

        'End If

        Try
            If (Not _fnAccesible()) Then

                If (gb_FacturaEmite) Then
                    If tbCodigo.Text = String.Empty Then
                        Throw New Exception("Venta no encontrada")
                    End If
                    If TbNit.Text = String.Empty Then
                        _prImiprimirNotaVenta(tbCodigo.Text)
                        Return
                    ElseIf (Not P_fnValidarFacturaVigente()) Then

                        Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)

                        ToastNotification.Show(Me, "No se puede imprimir la factura con numero ".ToUpper + tbNroFactura.Text + ", su factura esta anulada".ToUpper,
                                              img, 3000,
                                              eToastGlowColor.Green,
                                              eToastPosition.TopCenter)
                        Exit Sub
                    End If
                    ReimprimirFactura(tbCodigo.Text, True, True)
                    _prImiprimirNotaVenta(tbCodigo.Text)
                Else
                    _prImiprimirNotaVenta(tbCodigo.Text)
                End If
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
    Private Sub MostrarMensajeOk(mensaje As String)
        ToastNotification.Show(Me,
                               mensaje.ToUpper,
                               My.Resources.OK,
                               5000,
                               eToastGlowColor.Green,
                               eToastPosition.TopCenter)
    End Sub

    Private Sub TbNit_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TbNit.KeyPress
        g_prValidarTextBox(1, e)
    End Sub

    Private Sub swTipoVenta_KeyDown(sender As Object, e As KeyEventArgs) Handles swTipoVenta.KeyDown

    End Sub

    Private Sub ButtonX1_Click(sender As Object, e As EventArgs) Handles ButtonX1.Click
        If (Not _fnAccesible()) Then
            P_GenerarReporte(tbCodigo.Text)

        End If
    End Sub

    Private Sub swTipoVenta_Leave(sender As Object, e As EventArgs) Handles swTipoVenta.Leave
        grdetalle.Select()
    End Sub




    Private Sub grProductos_FormattingRow(sender As Object, e As RowLoadEventArgs) Handles grProductos.FormattingRow

    End Sub

    Private Sub cbSucursal_ValueChanged(sender As Object, e As EventArgs) Handles cbSucursal.ValueChanged
        Table_Producto = Nothing
    End Sub

    Private Sub s(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub btnSearchCliente_Click(sender As Object, e As EventArgs) Handles btnSearchCliente.Click
        Dim dt As DataTable

        dt = L_fnListarClientes()
        '              a.ydnumi, a.ydcod, a.yddesc, a.yddctnum, a.yddirec
        ',a.ydtelf1 ,a.ydfnac 

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
            _dias = Row.Cells("yddias").Value

            Dim numiVendedor As Integer = IIf(IsDBNull(Row.Cells("ydnumivend").Value), 0, Row.Cells("ydnumivend").Value)
            If (numiVendedor > 0) Then
                tbVendedor.Text = Row.Cells("vendedor").Value
                _CodEmpleado = Row.Cells("ydnumivend").Value

                grdetalle.Select()
                Table_Producto = Nothing
            Else
                tbVendedor.Clear()
                _CodEmpleado = 0
                tbVendedor.Focus()
                Table_Producto = Nothing

            End If
        End If

    End Sub

    Private Sub grdetalle_DoubleClick(sender As Object, e As EventArgs) Handles grdetalle.DoubleClick
        If (GPanelProductos.Visible = True) Then
            _DesHabilitarProductos()
        End If


    End Sub

    Private Sub grProductos_DoubleClick(sender As Object, e As EventArgs) Handles grProductos.DoubleClick
        Dim f, c As Integer
        c = grProductos.Col
        f = grProductos.Row
        If (f >= 0) Then


            InsertarProductosSinLote()



        End If
    End Sub

    Private Sub tbProducto_TextChanged(sender As Object, e As EventArgs) Handles tbProducto.TextChanged
        Dim dtProductoCopy As DataTable
        dtProductoCopy = dtProductoGoblal.Copy
        dtProductoCopy.Rows.Clear()
        Dim dt As DataTable = dtProductoGoblal.Copy

        Dim charSequence As String
        charSequence = tbProducto.Text.ToUpper
        If (charSequence.Trim <> String.Empty) Then
            Dim cantidad As Integer = 12
            Dim cont As Integer = 12

            'Split con array de delimitadores
            Dim delimitadores() As String = {" ", ".", ",", ";", "-"}
            Dim vectoraux() As String
            vectoraux = charSequence.Split(delimitadores, StringSplitOptions.None)

            'mostrar resultado
            'For Each item As String In vectoraux


            '    Console.WriteLine("'{0}'", item)
            'Next
            Dim cant As Integer = vectoraux.Length

            For i As Integer = 0 To dt.Rows.Count - 1 Step 1
                Dim nombre As String = dt.Rows(i).Item("NombreServicio").ToString.ToUpper
                Select Case cant
                    Case 1

                        If (nombre.Trim.Contains(vectoraux(0))) Then
                            dtProductoCopy.ImportRow(dt.Rows(i))
                            cont += 1
                        End If

                    Case 2
                        If (nombre.Trim.Contains(vectoraux(0)) And nombre.Trim.Contains(vectoraux(1))) Then
                            dtProductoCopy.ImportRow(dt.Rows(i))
                            cont += 1
                        End If
                    Case 3
                        If (nombre.Trim.Contains(vectoraux(0)) And nombre.Trim.Contains(vectoraux(1)) And nombre.Trim.Contains(vectoraux(2))) Then
                            dtProductoCopy.ImportRow(dt.Rows(i))
                            cont += 1
                        End If
                    Case 4
                        If (nombre.Trim.Contains(vectoraux(0)) And nombre.Trim.Contains(vectoraux(1)) And nombre.Trim.Contains(vectoraux(2)) And nombre.Trim.Contains(vectoraux(3))) Then
                            dtProductoCopy.ImportRow(dt.Rows(i))
                            cont += 1
                        End If
                    Case 5
                        If (nombre.Trim.Contains(vectoraux(0)) And nombre.Trim.Contains(vectoraux(1)) And nombre.Trim.Contains(vectoraux(2)) And nombre.Trim.Contains(vectoraux(3)) And nombre.Trim.Contains(vectoraux(4))) Then
                            dtProductoCopy.ImportRow(dt.Rows(i))
                            cont += 1
                        End If
                    Case 6
                        If (nombre.Trim.Contains(vectoraux(0)) And nombre.Trim.Contains(vectoraux(1)) And nombre.Trim.Contains(vectoraux(2)) And nombre.Trim.Contains(vectoraux(3)) And nombre.Trim.Contains(vectoraux(4)) And nombre.Trim.Contains(vectoraux(5))) Then
                            dtProductoCopy.ImportRow(dt.Rows(i))
                            cont += 1
                        End If

                    Case 7

                        If (nombre.Trim.Contains(vectoraux(0)) And nombre.Trim.Contains(vectoraux(1)) And nombre.Trim.Contains(vectoraux(2)) And nombre.Trim.Contains(vectoraux(3)) And nombre.Trim.Contains(vectoraux(4)) And nombre.Trim.Contains(vectoraux(5)) And nombre.Trim.Contains(vectoraux(6))) Then
                            dtProductoCopy.ImportRow(dt.Rows(i))
                            cont += 1
                        End If
                    Case 8
                        If (nombre.Trim.Contains(vectoraux(0)) And nombre.Trim.Contains(vectoraux(1)) And nombre.Trim.Contains(vectoraux(2)) And nombre.Trim.Contains(vectoraux(3)) And nombre.Trim.Contains(vectoraux(4)) And nombre.Trim.Contains(vectoraux(5)) And nombre.Trim.Contains(vectoraux(6)) And nombre.Trim.Contains(vectoraux(7))) Then
                            dtProductoCopy.ImportRow(dt.Rows(i))
                            cont += 1
                        End If
                    Case 9
                        If (nombre.Trim.Contains(vectoraux(0)) And nombre.Trim.Contains(vectoraux(1)) And nombre.Trim.Contains(vectoraux(2)) And nombre.Trim.Contains(vectoraux(3)) And nombre.Trim.Contains(vectoraux(4)) And nombre.Trim.Contains(vectoraux(5)) And nombre.Trim.Contains(vectoraux(6)) And nombre.Trim.Contains(vectoraux(7)) And nombre.Trim.Contains(vectoraux(8))) Then
                            dtProductoCopy.ImportRow(dt.Rows(i))
                            cont += 1
                        End If
                    Case 10
                        If (nombre.Trim.Contains(vectoraux(0)) And nombre.Trim.Contains(vectoraux(1)) And nombre.Trim.Contains(vectoraux(2)) And nombre.Trim.Contains(vectoraux(3)) And nombre.Trim.Contains(vectoraux(4)) And nombre.Trim.Contains(vectoraux(5)) And nombre.Trim.Contains(vectoraux(6)) And nombre.Trim.Contains(vectoraux(7)) And nombre.Trim.Contains(vectoraux(8)) And nombre.Trim.Contains(vectoraux(9))) Then
                            dtProductoCopy.ImportRow(dt.Rows(i))
                            cont += 1
                        End If

                    Case 11
                        If (nombre.Trim.Contains(vectoraux(0)) And nombre.Trim.Contains(vectoraux(1)) And nombre.Trim.Contains(vectoraux(2)) And nombre.Trim.Contains(vectoraux(3)) And nombre.Trim.Contains(vectoraux(4)) And nombre.Trim.Contains(vectoraux(5)) And nombre.Trim.Contains(vectoraux(6)) And nombre.Trim.Contains(vectoraux(7)) And nombre.Trim.Contains(vectoraux(8)) And nombre.Trim.Contains(vectoraux(9)) And nombre.Trim.Contains(vectoraux(10))) Then
                            dtProductoCopy.ImportRow(dt.Rows(i))
                            cont += 1
                        End If

                    Case 12
                        If (nombre.Trim.Contains(vectoraux(0)) And nombre.Trim.Contains(vectoraux(1)) And nombre.Trim.Contains(vectoraux(2)) And nombre.Trim.Contains(vectoraux(3)) And nombre.Trim.Contains(vectoraux(4)) And nombre.Trim.Contains(vectoraux(5)) And nombre.Trim.Contains(vectoraux(6)) And nombre.Trim.Contains(vectoraux(7)) And nombre.Trim.Contains(vectoraux(8)) And nombre.Trim.Contains(vectoraux(9)) And nombre.Trim.Contains(vectoraux(10)) And nombre.Trim.Contains(vectoraux(11))) Then
                            dtProductoCopy.ImportRow(dt.Rows(i))
                            cont += 1
                        End If


                    Case 13
                        If (nombre.Trim.Contains(vectoraux(0)) And nombre.Trim.Contains(vectoraux(1)) And nombre.Trim.Contains(vectoraux(2)) And nombre.Trim.Contains(vectoraux(3)) And nombre.Trim.Contains(vectoraux(4)) And nombre.Trim.Contains(vectoraux(5)) And nombre.Trim.Contains(vectoraux(6)) And nombre.Trim.Contains(vectoraux(7)) And nombre.Trim.Contains(vectoraux(8)) And nombre.Trim.Contains(vectoraux(9)) And nombre.Trim.Contains(vectoraux(10)) And nombre.Trim.Contains(vectoraux(11)) And nombre.Trim.Contains(vectoraux(12))) Then
                            dtProductoCopy.ImportRow(dt.Rows(i))
                            cont += 1
                        End If
                    Case 14
                        If (nombre.Trim.Contains(vectoraux(0)) And nombre.Trim.Contains(vectoraux(1)) And nombre.Trim.Contains(vectoraux(2)) And nombre.Trim.Contains(vectoraux(3)) And nombre.Trim.Contains(vectoraux(4)) And nombre.Trim.Contains(vectoraux(5)) And nombre.Trim.Contains(vectoraux(6)) And nombre.Trim.Contains(vectoraux(7)) And nombre.Trim.Contains(vectoraux(8)) And nombre.Trim.Contains(vectoraux(9)) And nombre.Trim.Contains(vectoraux(10)) And nombre.Trim.Contains(vectoraux(11)) And nombre.Trim.Contains(vectoraux(12)) And nombre.Trim.Contains(vectoraux(13))) Then
                            dtProductoCopy.ImportRow(dt.Rows(i))
                            cont += 1
                        End If
                    Case 15
                        If (nombre.Trim.Contains(vectoraux(0)) And nombre.Trim.Contains(vectoraux(1)) And nombre.Trim.Contains(vectoraux(2)) And nombre.Trim.Contains(vectoraux(3)) And nombre.Trim.Contains(vectoraux(4)) And nombre.Trim.Contains(vectoraux(5)) And nombre.Trim.Contains(vectoraux(6)) And nombre.Trim.Contains(vectoraux(7)) And nombre.Trim.Contains(vectoraux(8)) And nombre.Trim.Contains(vectoraux(9)) And nombre.Trim.Contains(vectoraux(10)) And nombre.Trim.Contains(vectoraux(11)) And nombre.Trim.Contains(vectoraux(12)) And nombre.Trim.Contains(vectoraux(13)) And nombre.Trim.Contains(vectoraux(14))) Then
                            dtProductoCopy.ImportRow(dt.Rows(i))
                            cont += 1
                        End If
                    Case 16
                        If (nombre.Trim.Contains(vectoraux(0)) And nombre.Trim.Contains(vectoraux(1)) And nombre.Trim.Contains(vectoraux(2)) And nombre.Trim.Contains(vectoraux(3)) And nombre.Trim.Contains(vectoraux(4)) And nombre.Trim.Contains(vectoraux(5)) And nombre.Trim.Contains(vectoraux(6)) And nombre.Trim.Contains(vectoraux(7)) And nombre.Trim.Contains(vectoraux(8)) And nombre.Trim.Contains(vectoraux(9)) And nombre.Trim.Contains(vectoraux(10)) And nombre.Trim.Contains(vectoraux(11)) And nombre.Trim.Contains(vectoraux(12)) And nombre.Trim.Contains(vectoraux(13)) And nombre.Trim.Contains(vectoraux(14)) And nombre.Trim.Contains(vectoraux(15))) Then
                            dtProductoCopy.ImportRow(dt.Rows(i))
                            cont += 1
                        End If
                    Case 17
                        If (nombre.Trim.Contains(vectoraux(0)) And nombre.Trim.Contains(vectoraux(1)) And nombre.Trim.Contains(vectoraux(2)) And nombre.Trim.Contains(vectoraux(3)) And nombre.Trim.Contains(vectoraux(4)) And nombre.Trim.Contains(vectoraux(5)) And nombre.Trim.Contains(vectoraux(6)) And nombre.Trim.Contains(vectoraux(7)) And nombre.Trim.Contains(vectoraux(8)) And nombre.Trim.Contains(vectoraux(9)) And nombre.Trim.Contains(vectoraux(10)) And nombre.Trim.Contains(vectoraux(11)) And nombre.Trim.Contains(vectoraux(12)) And nombre.Trim.Contains(vectoraux(13)) And nombre.Trim.Contains(vectoraux(14)) And nombre.Trim.Contains(vectoraux(15)) And nombre.Trim.Contains(vectoraux(16))) Then
                            dtProductoCopy.ImportRow(dt.Rows(i))
                            cont += 1
                        End If
                    Case 18
                        If (nombre.Trim.Contains(vectoraux(0)) And nombre.Trim.Contains(vectoraux(1)) And nombre.Trim.Contains(vectoraux(2)) And nombre.Trim.Contains(vectoraux(3)) And nombre.Trim.Contains(vectoraux(4)) And nombre.Trim.Contains(vectoraux(5)) And nombre.Trim.Contains(vectoraux(6)) And nombre.Trim.Contains(vectoraux(7)) And nombre.Trim.Contains(vectoraux(8)) And nombre.Trim.Contains(vectoraux(9)) And nombre.Trim.Contains(vectoraux(10)) And nombre.Trim.Contains(vectoraux(11)) And nombre.Trim.Contains(vectoraux(12)) And nombre.Trim.Contains(vectoraux(13)) And nombre.Trim.Contains(vectoraux(14)) And nombre.Trim.Contains(vectoraux(15)) And nombre.Trim.Contains(vectoraux(16)) And nombre.Trim.Contains(vectoraux(17))) Then
                            dtProductoCopy.ImportRow(dt.Rows(i))
                            cont += 1
                        End If



                End Select

            Next
            grProductos.DataSource = dtProductoCopy.Copy
        Else
            grProductos.DataSource = dtProductoGoblal.Copy
        End If



    End Sub

    Private Sub tbProducto_KeyDown(sender As Object, e As KeyEventArgs) Handles tbProducto.KeyDown
        If e.KeyData = Keys.Escape Then
            _DesHabilitarProductos()
            FilaSelectLote = Nothing
        End If
        If e.KeyData = Keys.Down Then
            grProductos.Focus()
        End If

    End Sub

#End Region
End Class