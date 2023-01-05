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

Public Class F0_Factura
#Region "Variables Globales"
    Dim _CodCliente As Integer = 0
    Dim _CodEmpleado As Integer = 0
    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
    Dim FilaSelectLote As DataRow = Nothing
    Dim Lote As Boolean = False '1=igual a mostrar las columnas de lote y fecha de Vencimiento
#End Region
#Region "Metodos Privados"
    Private Sub _IniciarTodo()
        ''   L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        MSuperTabControl.SelectedTabIndex = 0
        'Me.WindowState = FormWindowState.Maximized
        _prValidarLote()
        _prCargarComboLibreriaSucursal(cbSucursal)
        lbTipoMoneda.Visible = False
        swMoneda.Visible = False

        _prCargarFactura()
        _prInhabiliitar()
        grVentas.Focus()
        Me.Text = "FACTURACION"
        Dim blah As New Bitmap(New Bitmap(My.Resources.ic_p), 20, 20)
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico
        _prAsignarPermisos()


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
        tbnit.ReadOnly = True
        tbnombfact.ReadOnly = True
        tbCodigo.ReadOnly = True
        tbCliente.ReadOnly = True
        tbVendedor.ReadOnly = True
        tbObservacion.ReadOnly = True
        tbFechaVenta.IsInputReadOnly = True
        swMoneda.IsReadOnly = True

        tbMdesc.IsInputReadOnly = True
        tbPdesc.IsInputReadOnly = True



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
        tbnit.ReadOnly = False
        tbnombfact.ReadOnly = False
        ''  tbCliente.ReadOnly = False  por que solo podra seleccionar Cliente
        ''  tbVendedor.ReadOnly = False
        tbObservacion.ReadOnly = False
        tbFechaVenta.IsInputReadOnly = False
        swMoneda.IsReadOnly = False
        btnGrabar.Enabled = True

        tbMdesc.IsInputReadOnly = False
        tbPdesc.IsInputReadOnly = False

        If (tbCodigo.Text.Length > 0) Then
            cbSucursal.ReadOnly = True
        Else
            cbSucursal.ReadOnly = False

        End If
    End Sub
    Public Sub _prFiltrar()
        'cargo el buscador
        Dim _Mpos As Integer
        _prCargarFactura()
        If grVentas.RowCount > 0 Then
            _Mpos = 0
            grVentas.Row = _Mpos
        Else
            _Limpiar()
            LblPaginacion.Text = "0/0"
        End If
    End Sub
    Private Sub _Limpiar()
        tbnit.Clear()
        tbnombfact.Clear()
        tbnroFact.Clear()
        tbdespacho.Clear()

        tbCodigo.Clear()
        tbCliente.Clear()
        tbVendedor.Clear()
        tbObservacion.Clear()
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
        tbdespacho.Focus()


        If (CType(cbSucursal.DataSource, DataTable).Rows.Count > 0) Then
            cbSucursal.SelectedIndex = 0
        End If
        FilaSelectLote = Nothing
        tbnroFact.Text = _prObtenerNroFactura()
    End Sub
    Public Sub _prMostrarRegistro(_N As Integer)
        '' grVentas.Row = _N
        '     a.fanumi ,a.faalm ,a.fafdoc ,a.faven ,vendedor .yddesc as vendedor,a.faclpr,
        'cliente.yddesc as cliente ,a.famon ,IIF(famon=1,'Boliviano','Dolar') as moneda,a.faest ,a.faobs ,
        'a.fadesc, a.fafact ,a.fahact ,a.fauact,a.fanit ,a.fanombfact ,a.fatotal as total
        'a.fadesc, a.fafact ,a.fahact ,a.fauact,a.fanit ,a.fanombfact ,a.fatotal as total,a.fatv1numi as nrodespacho,a.fanrofact as nrofactura
        With grVentas
            tbdespacho.Text = .GetValue("nrodespacho")
            tbnroFact.Text = .GetValue("nrofactura")
            tbnit.Text = .GetValue("fanit")
            tbnombfact.Text = .GetValue("fanombfact")
            cbSucursal.Value = .GetValue("faalm")
            tbCodigo.Text = .GetValue("fanumi")
            tbFechaVenta.Value = .GetValue("fafdoc")
            _CodEmpleado = .GetValue("faven")
            tbVendedor.Text = .GetValue("vendedor")
            _CodCliente = .GetValue("faclpr")
            tbCliente.Text = .GetValue("cliente")
            swMoneda.Value = .GetValue("famon")
            tbObservacion.Text = .GetValue("faobs")
            tbnit.Text = .GetValue("fanit")
            tbnombfact.Text = .GetValue("fanombfact")

            lbFecha.Text = CType(.GetValue("fafact"), Date).ToString("dd/MM/yyyy")
            lbHora.Text = .GetValue("fahact").ToString
            lbUsuario.Text = .GetValue("fauact").ToString

        End With

        _prCargarDetalleVenta(tbCodigo.Text)
        tbMdesc.Value = grVentas.GetValue("fadesc")
        _prCalcularPrecioTotal()
        LblPaginacion.Text = Str(grVentas.Row + 1) + "/" + grVentas.RowCount.ToString

    End Sub

    Private Sub _prCargarDetalleVenta(_numi As String)
        Dim dt As New DataTable
        dt = L_fnDetalleFactura(_numi)
        grdetalle.DataSource = dt
        grdetalle.RetrieveStructure()
        grdetalle.AlternatingColors = True
        '        a.fbnumi ,a.fbtf1numi ,a.fbty5prod ,b.yfcdprod1 as producto,a.fbest ,a.fbcmin ,a.fbumin ,Umin .ycdes3 as unidad,
        'a.fbpbas ,a.fbptot,a.fbporc,a.fbdesc ,a.fbtotdesc,a.fblote ,a.fbfechaVenc , a.fbfact ,a.fbhact ,a.fbuact,1 as estado,Cast(null as Image) as img,
        '		(Sum(inv.iccven )+a.fbcmin ) as stock

        With grdetalle.RootTable.Columns("fbnumi")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = False

        End With

        With grdetalle.RootTable.Columns("fbtf1numi")
            .Width = 90
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("fbty5prod")
            .Width = 90
            .Visible = False
        End With

        With grdetalle.RootTable.Columns("producto")
            .Caption = "PRODUCTOS"
            .Width = 250
            .Visible = True


        End With

        With grdetalle.RootTable.Columns("fbest")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With

        With grdetalle.RootTable.Columns("fbcmin")
            .Width = 160
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Cant. Minima".ToUpper
        End With
        With grdetalle.RootTable.Columns("fbumin")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("unidad")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .Caption = "Unidad".ToUpper
        End With


        With grdetalle.RootTable.Columns("fbpbas")
            .Width = 120
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Precio U.".ToUpper
        End With
        With grdetalle.RootTable.Columns("fbptot")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Sub Total".ToUpper
        End With
        With grdetalle.RootTable.Columns("fbporc")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "P.Desc(%)".ToUpper
        End With
        With grdetalle.RootTable.Columns("fbdesc")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "M.Desc".ToUpper
        End With

        With grdetalle.RootTable.Columns("fbtotdesc")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Total".ToUpper
        End With
        With grdetalle.RootTable.Columns("fblote")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("fbfechaVenc")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("fbfact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("fbhact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("fbuact")
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
        With grdetalle
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
        End With
    End Sub

    Private Sub _prCargarFactura()
        Dim dt As New DataTable
        dt = L_fnGeneralFactura()
        grVentas.DataSource = dt
        grVentas.RetrieveStructure()
        grVentas.AlternatingColors = True
        '      a.fanumi ,a.faalm ,a.fafdoc ,a.faven ,vendedor .yddesc as vendedor,a.faclpr,
        'cliente.yddesc as cliente ,a.famon ,IIF(famon=1,'Boliviano','Dolar') as moneda,a.faest ,a.faobs ,
        'a.fadesc, a.fafact ,a.fahact ,a.fauact,a.fanit ,a.fanombfact ,a.fatotal as total

        With grVentas.RootTable.Columns("fanumi")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = True

        End With
        With grVentas.RootTable.Columns("fanit")
            .Width = 90
            .Caption = "NIT"
            .Visible = True

        End With
        With grVentas.RootTable.Columns("fanombfact")
            .Width = 160
            .Caption = "NOMB FACT"
            .Visible = True

        End With
        With grVentas.RootTable.Columns("nrodespacho")
            .Width = 120
            .Caption = "NRO DESPACHO"
            .Visible = True

        End With
        With grVentas.RootTable.Columns("nrofactura")
            .Width = 90
            .Caption = "NRO FACT."
            .Visible = True

        End With
        With grVentas.RootTable.Columns("faalm")
            .Width = 90
            .Visible = False
        End With
        With grVentas.RootTable.Columns("fafdoc")
            .Width = 90
            .Visible = True
            .Caption = "FECHA"
        End With

        With grVentas.RootTable.Columns("faven")
            .Width = 160
            .Visible = False
        End With
        With grVentas.RootTable.Columns("vendedor")
            .Width = 200
            .Visible = True
            .Caption = "VENDEDOR".ToUpper
        End With

        With grVentas.RootTable.Columns("faclpr")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("cliente")
            .Width = 200
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Caption = "CLIENTE"
        End With

        With grVentas.RootTable.Columns("famon")
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
        With grVentas.RootTable.Columns("faobs")
            .Width = 200
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
            .Caption = "OBSERVACION"
        End With
        With grVentas.RootTable.Columns("fadesc")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("faest")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With

        With grVentas.RootTable.Columns("fafact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("fahact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("fauact")
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


    Public Sub actualizarSaldoSinLote(ByRef dt As DataTable)
        'b.yfcdprod1 ,a.iclot ,a.icfven  ,a.iccven 

        '      a.tbnumi ,a.tbtv1numi ,a.tbty5prod ,b.yfcdprod1 as producto,a.tbest ,a.tbcmin ,a.tbumin ,Umin .ycdes3 as unidad,a.tbpbas ,a.tbptot ,a.tbobs ,
        'a.tbpcos,a.tblote ,a.tbfechaVenc , a.tbptot2, a.tbfact ,a.tbhact ,a.tbuact,1 as estado,Cast(null as Image) as img,
        'Cast (0 as decimal (18,2)) as stock
        Dim _detalle As DataTable = CType(grdetalle.DataSource, DataTable)

        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
            Dim sum As Integer = 0
            Dim codProducto As Integer = dt.Rows(i).Item("yfnumi")
            For j As Integer = 0 To grdetalle.RowCount - 1 Step 1
                grdetalle.Row = j
                Dim estado As Integer = grdetalle.GetValue("estado")
                If (estado = 0) Then
                    If (codProducto = grdetalle.GetValue("fbty5prod")) Then
                        sum = sum + grdetalle.GetValue("fbcmin")
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
        Dim dtname As DataTable = L_fnNameLabel()
        Dim dt As New DataTable
        dt = L_fnListarProductosSinLoteFactura(cbSucursal.Value, _cliente, CType(grdetalle.DataSource, DataTable))
        actualizarSaldoSinLote(dt)
        grProductos.DataSource = dt
        grProductos.RetrieveStructure()
        grProductos.AlternatingColors = True

        '      a.yfnumi ,a.yfcprod ,a.yfcdprod1,a.yfcdprod2 ,a.yfgr1,gr1.ycdes3 as grupo1,a.yfgr2
        ',gr2.ycdes3 as grupo2 ,a.yfgr3,gr3.ycdes3 as grupo3,a.yfgr4 ,gr4 .ycdes3 as grupo4,a.yfumin ,Umin .ycdes3 as UnidMin
        ' ,b.yhprecio 

        With grProductos.RootTable.Columns("yfnumi")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = False

        End With
        With grProductos.RootTable.Columns("yfcprod")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = False

        End With

        With grProductos.RootTable.Columns("yfcdprod1")
            .Width = 200
            .Visible = True
            .Caption = "Descripcion"
        End With
        With grProductos.RootTable.Columns("yfcdprod2")
            .Width = 150
            .Visible = True
            .Caption = "Descripcion Corta"
        End With


        With grProductos.RootTable.Columns("yfgr1")
            .Width = 160
            .Visible = False
        End With
        If (dtname.Rows.Count > 0) Then

            With grProductos.RootTable.Columns("grupo1")
                .Width = 120
                .Caption = dtname.Rows(0).Item("Grupo 1").ToString
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = True
            End With
            With grProductos.RootTable.Columns("grupo2")
                .Width = 120
                .Caption = dtname.Rows(0).Item("Grupo 2").ToString
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = True
            End With

            With grProductos.RootTable.Columns("grupo3")
                .Width = 120
                .Caption = dtname.Rows(0).Item("Grupo 3").ToString
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = True
            End With
            With grProductos.RootTable.Columns("grupo4")
                .Width = 120
                .Caption = dtname.Rows(0).Item("Grupo 4").ToString
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = True
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
            .Visible = True
            .Caption = "Unidad Min."
        End With
        With grProductos.RootTable.Columns("yhprecio")
            .Width = 120
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Caption = "Precio"
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
            .Width = 120
            .FormatString = "0.00"
            .Visible = True
            .Caption = "Stock"
        End With

        With grProductos
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
        End With
        _prAplicarCondiccionJanusSinLote()
    End Sub
    Public Sub _prAplicarCondiccionJanusSinLote()
        Dim fc As GridEXFormatCondition
        fc = New GridEXFormatCondition(grProductos.RootTable.Columns("stock"), ConditionOperator.Equal, 0)
        'fc.FormatStyle.FontBold = TriState.True
        fc.FormatStyle.ForeColor = Color.Tan
        grProductos.RootTable.FormatConditions.Add(fc)
    End Sub


    Private Sub _prAddDetalleVenta()
   

        '        a.fbnumi ,a.fbtf1numi ,a.fbty5prod ,b.yfcdprod1 as producto,a.fbest ,a.fbcmin ,a.fbumin ,Umin .ycdes3 as unidad,
        'a.fbpbas ,a.fbptot,a.fbporc,a.fbdesc ,a.fbtotdesc,a.fblote ,a.fbfechaVenc , a.fbfact ,a.fbhact ,a.fbuact,1 as estado,Cast(null as Image) as img,
        '		(Sum(inv.iccven )+a.fbcmin ) as stock

        Dim Bin As New MemoryStream
        Dim img As New Bitmap(My.Resources.delete, 28, 28)
        img.Save(Bin, Imaging.ImageFormat.Png)
        CType(grdetalle.DataSource, DataTable).Rows.Add(_fnSiguienteNumi() + 1, 0, 0, "", 0, 0, 0, "", 0, 0, 0, 0, 0, "20170101", CDate("2017/01/01"), Now.Date, "", "", 0, Bin.GetBuffer, 0)
    End Sub

    Public Function _fnSiguienteNumi()
        Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)
        Dim rows() As DataRow = dt.Select("fbnumi=MAX(fbnumi)")
        If (rows.Count > 0) Then
            Return rows(rows.Count - 1).Item("fbnumi")
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
            Dim _numi As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("fbnumi")
            If (_numi = numi) Then
                pos = i
                Return
            End If
        Next

    End Sub

    Public Function _fnExisteProducto(idprod As Integer) As Boolean
        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _idprod As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("fbty5prod")
            Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("estado")
            If (_idprod = idprod And estado >= 0) Then

                Return True
            End If
        Next
        Return False
    End Function


    Public Sub P_PonerTotal(rowIndex As Integer)
        If (rowIndex < grdetalle.RowCount) Then

            Dim lin As Integer = grdetalle.GetValue("fbnumi")
            Dim pos As Integer = -1
            _fnObtenerFilaDetalle(pos, lin)
            Dim cant As Double = grdetalle.GetValue("fbcmin")
            Dim uni As Double = grdetalle.GetValue("fbpbas")
            Dim MontoDesc As Double = grdetalle.GetValue("fbdesc")
            Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)
            If (pos >= 0) Then
                Dim TotalUnitario As Double = cant * uni

                'grDetalle.SetValue("lcmdes", montodesc)

                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("fbptot") = TotalUnitario
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("fbtotdesc") = TotalUnitario - MontoDesc
                grdetalle.SetValue("fbptot", TotalUnitario)
                grdetalle.SetValue("fbtotdesc", TotalUnitario - MontoDesc)


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
        Dim pordesc As Double = ((montodesc * 100) / grdetalle.GetTotal(grdetalle.RootTable.Columns("fbtotdesc"), AggregateFunction.Sum))
        tbPdesc.Value = pordesc
        tbSubTotal.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("fbtotdesc"), AggregateFunction.Sum)

        tbtotal.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("fbtotdesc"), AggregateFunction.Sum) - montodesc





    End Sub
    Public Sub _prEliminarFila()
        If (grdetalle.Row >= 0) Then
            If (grdetalle.RowCount >= 2) Then
                Dim estado As Integer = grdetalle.GetValue("estado")
                Dim pos As Integer = -1
                Dim lin As Integer = grdetalle.GetValue("fbnumi")
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


    End Sub
    Public Function _ValidarCampos() As Boolean
        If (_CodCliente <= 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Seleccione un Cliente con Ctrl+Enter".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            tbCliente.Focus()
            Return False

        End If
        If (tbnroFact.Text.Length <= 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor inserte el numero de la factura".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            tbnroFact.Focus()

            Return False

        End If
        If (tbnit.Text.Length <= 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor inserte el numero del Nit".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            tbnroFact.Focus()

            Return False

        End If
        If (tbnombfact.Text.Length <= 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor inserte el nombre al que se le efectuara la factura".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            tbnombfact.Focus()

            Return False

        End If
        If (_CodEmpleado <= 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Seleccione un Vendedor con Ctrl+Enter".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            tbVendedor.Focus()
            Return False
        End If
        If (cbSucursal.SelectedIndex < 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Seleccione una Sucursal".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            cbSucursal.Focus()
            Return False
        End If
        'Validar datos de factura


        If (grdetalle.RowCount = 1) Then
            grdetalle.Row = grdetalle.RowCount - 1
            If (grdetalle.GetValue("fbty5prod") = 0) Then
                Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
                ToastNotification.Show(Me, "Por Favor Seleccione  un detalle de producto".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                Return False
            End If

        End If
        Return True
    End Function

    Public Sub _GuardarNuevo()
        Dim numi As String = ""
        'ByRef _fanumi As String, _fatv1numi As Integer, _fanroFact As String, _fafdoc As String, _faven As Integer, _faclpr As Integer,
        '                                   _famon As Integer, _faobs As String,
        '                                   _fadesc As Double,
        '                                   _fatotal As Double, _fanit As String, _fanombFact As String, detalle As DataTable, _almacen As Integer
        Dim res As Boolean = L_fnGrabarFactura(numi, IIf(tbdespacho.Text = "", 0, tbdespacho.Text), tbnroFact.Text, tbFechaVenta.Value.ToString("yyyy/MM/dd"), _CodEmpleado, _CodCliente, IIf(swMoneda.Value = True, 1, 0), tbObservacion.Text, tbMdesc.Value, tbtotal.Value, tbnit.Text, tbnombfact.Text, CType(grdetalle.DataSource, DataTable), cbSucursal.Value)


        If res Then
            P_fnGenerarFactura(IIf(tbdespacho.Text = "", 0, tbdespacho.Text))


            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "Código de Factura ".ToUpper + tbCodigo.Text + " Grabado con Exito.".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter
                                      )
            tbCodigo.Text = numi
            _prImiprimirFacturaPreimpresa(numi)


            _prCargarFactura()

            _Limpiar()

        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "La Proforma no pudo ser insertado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

        End If

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
        _Ds1 = L_Dosificacion("1", "1", _Fecha)

        _Ds = L_Reporte_Factura(numi, numi)
        _Autorizacion = _Ds1.Tables(0).Rows(0).Item("sbautoriz").ToString
        _NumFac = CInt(_Ds1.Tables(0).Rows(0).Item("sbnfac")) + 1
        _Nit = _Ds.Tables(0).Rows(0).Item("fvanitcli").ToString
        _Fechainv = Microsoft.VisualBasic.Right(_Fecha.ToShortDateString, 4) + _
                    Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(_Fecha.ToShortDateString, 5), 2) + _
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
        '_Literal = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(_TotalLi) - CDbl(_TotalDecimal)) + " con " + IIf(_TotalDecimal2.Equals("0"), "00", _TotalDecimal2) + "/100 Bolivianos"
        '_Ds2 = L_Reporte_Factura_Cia("1")
        'QrFactura.Text = _Ds2.Tables(0).Rows(0).Item("scnit").ToString + "|" + Str(_NumFac).Trim + "|" + _Autorizacion + "|" + _Fecha + "|" + _Total + "|" + _TotalLi.ToString + "|" + _Cod_Control + "|" + TbNit.Text.Trim + "|" + ice.ToString + "|0|0|" + Str(_Desc).Trim

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

        '_Ds = L_Reporte_Factura(numi, numi)

        'For I = 0 To _Ds.Tables(0).Rows.Count - 1
        '    _Ds.Tables(0).Rows(I).Item("fvaimgqr") = P_fnImageToByteArray(QrFactura.Image)
        'Next
        'If (impFactura) Then
        '    _Ds3 = L_ObtenerRutaImpresora("1") ' Datos de Impresion de Facturación
        '    If (_Ds3.Tables(0).Rows(0).Item("cbvp")) Then 'Vista Previa de la Ventana de Vizualización 1 = True 0 = False
        '        P_Global.Visualizador = New Visualizador 'Comentar
        '    End If
        '    Dim objrep As Object = Nothing
        '    If (gi_FacturaTipo = 1) Then
        '        'objrep = New R_FacturaG
        '    ElseIf (gi_FacturaTipo = 2) Then
        '        objrep = New R_FacturaCarta
        '        If (Not _Ds.Tables(0).Rows.Count = gi_FacturaCantidadItems) Then
        '            For index = _Ds.Tables(0).Rows.Count To gi_FacturaCantidadItems - 1
        '                'Insertamos la primera fila con el saldo Inicial
        '                Dim f As DataRow = _Ds.Tables(0).NewRow
        '                f.ItemArray() = _Ds.Tables(0).Rows(0).ItemArray
        '                f.Item("fvbcant") = -1
        '                _Ds.Tables(0).Rows.Add(f)
        '            Next
        '        End If
        '    End If

        '    objrep.SetDataSource(_Ds.Tables(0))
        '    objrep.SetParameterValue("Hora", _Hora)
        '    objrep.SetParameterValue("Direccionpr", _Ds2.Tables(0).Rows(0).Item("scdir").ToString)
        '    objrep.SetParameterValue("Telefonopr", _Ds2.Tables(0).Rows(0).Item("sctelf").ToString)
        '    objrep.SetParameterValue("Literal1", _Literal)
        '    objrep.SetParameterValue("Literal2", " ")
        '    objrep.SetParameterValue("Literal3", " ")
        '    objrep.SetParameterValue("NroFactura", _NumFac)
        '    objrep.SetParameterValue("NroAutoriz", _Autorizacion)
        '    objrep.SetParameterValue("ENombre", _Ds2.Tables(0).Rows(0).Item("scneg").ToString) '?
        '    objrep.SetParameterValue("ECasaMatriz", _Ds2.Tables(0).Rows(0).Item("scsuc").ToString)
        '    objrep.SetParameterValue("ECiudadPais", _Ds2.Tables(0).Rows(0).Item("scpai").ToString)
        '    objrep.SetParameterValue("ESFC", _Ds1.Tables(0).Rows(0).Item("sbsfc").ToString)
        '    objrep.SetParameterValue("ENit", _Ds2.Tables(0).Rows(0).Item("scnit").ToString)
        '    objrep.SetParameterValue("EActividad", _Ds2.Tables(0).Rows(0).Item("scact").ToString)
        '    objrep.SetParameterValue("ESms", "''" + _Ds1.Tables(0).Rows(0).Item("sbnota").ToString + "''")
        '    objrep.SetParameterValue("ESms2", "''" + _Ds1.Tables(0).Rows(0).Item("sbnota2").ToString + "''")
        '    objrep.SetParameterValue("EDuenho", _Ds2.Tables(0).Rows(0).Item("scnom").ToString) '?
        '    objrep.SetParameterValue("URLImageLogo", gs_CarpetaRaiz + "\LogoFactura.jpg")
        '    objrep.SetParameterValue("URLImageMarcaAgua", gs_CarpetaRaiz + "\MarcaAguaFactura.jpg")

        '    If (_Ds3.Tables(0).Rows(0).Item("cbvp")) Then 'Vista Previa de la Ventana de Vizualización 1 = True 0 = False
        '        P_Global.Visualizador.CrGeneral.ReportSource = objrep 'Comentar
        '        P_Global.Visualizador.ShowDialog() 'Comentar
        '        P_Global.Visualizador.BringToFront() 'Comentar
        '    End If

        '    Dim pd As New PrintDocument()
        '    pd.PrinterSettings.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
        '    If (Not pd.PrinterSettings.IsValid) Then
        '        ToastNotification.Show(Me, "La Impresora ".ToUpper + _Ds3.Tables(0).Rows(0).Item("cbrut").ToString + Chr(13) + "No Existe".ToUpper,
        '                               My.Resources.WARNING, 5 * 1000,
        '                               eToastGlowColor.Blue, eToastPosition.BottomRight)
        '    Else
        '        objrep.PrintOptions.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString '"EPSON TM-T20II Receipt5 (1)"
        '        objrep.PrintToPrinter(1, False, 1, 1)
        '    End If

        '    If (grabarPDF) Then
        '        'Copia de Factura en PDF
        '        If (Not Directory.Exists(gs_CarpetaRaiz + "\Facturas")) Then
        '            Directory.CreateDirectory(gs_CarpetaRaiz + "\Facturas")
        '        End If
        '        objrep.ExportToDisk(ExportFormatType.PortableDocFormat, gs_CarpetaRaiz + "\Facturas\" + CStr(_NumFac) + "_" + CStr(_Autorizacion) + ".pdf")

        '    End If
        'End If
        L_Actualiza_Dosificacion(_numidosif, _NumFac, numi)
    End Sub
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
        Dim _Hora As String = Now.Hour.ToString + ":" + Now.Minute.ToString
        'Grabado de Cabesera Factura
        'L_Grabar_Factura(numi,
        '                Now.Date.ToString("yyyy/MM/dd"),
        '                "0",
        '                "0",
        '                "1",
        '                tbnit.Text.Trim,
        '                "0",
        '                tbnombfact.Text,
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
        '                "0",
        '                numi,
        '                 _Hora)

        'Grabado de Detalle de Factura
        grProductos.Update()

        'Dim s As String = ""
        For Each fil As GridEXRow In grdetalle.GetRows
            If (Not fil.Cells("fbcmin").Value.ToString.Trim.Equals("") And
                Not fil.Cells("fbty5prod").Value.ToString.Trim.Equals("0")) Then
                's = fil.Cells("codP").Value
                's = fil.Cells("des").Value
                's = fil.Cells("can").Value
                's = fil.Cells("imp").Value
                L_Grabar_Factura_Detalle(numi.ToString,
                                        fil.Cells("fbty5prod").Value.ToString.Trim,
                                        fil.Cells("producto").Value.ToString.Trim,
                                        fil.Cells("fbcmin").Value.ToString.Trim,
                                        fil.Cells("fbpbas").Value.ToString.Trim,
                                        numi)
                res = True
            End If
        Next
        Return res
    End Function
    Private Function P_fnGenerarFactura(numi As String) As Boolean
        Dim res As Boolean = False
        res = P_fnGrabarFacturarTFV001(numi) ' Grabar en la TFV001
        If (res) Then

            'Validar para facturar
            P_prImprimirFacturar(numi, True, True) '_Codigo de a tabla TV001
            If (Not tbnit.Text.Trim.Equals("0")) Then
                L_Grabar_Nit(tbnit.Text.Trim, tbnombfact.Text.Trim, "", "", "")
            Else
                L_Grabar_Nit(tbnit.Text, "S/N", "", "", "")
            End If
        End If

        Return res
    End Function
    Private Sub _prGuardarModificado()
        Dim res As Boolean = L_fnModificarFactura(tbCodigo.Text, tbdespacho.Text, tbnroFact.Text, tbFechaVenta.Value.ToString("yyyy/MM/dd"), _CodEmpleado, _CodCliente, IIf(swMoneda.Value = True, 1, 0), tbObservacion.Text, tbMdesc.Value, tbtotal.Value, tbnit.Text, tbnombfact.Text, CType(grdetalle.DataSource, DataTable), cbSucursal.Value)
        If res Then

            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "Código de Factura ".ToUpper + tbCodigo.Text + " Modificado con Exito.".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter
                                      )

            _prImiprimirFacturaPreimpresa(tbCodigo.Text)

            _prCargarFactura()

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
            CType(grdetalle.DataSource, DataTable).Rows(i).Item("img") = Bin.ToArray()

        Next
        grdetalle.RootTable.Columns("img").Visible = True
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
        Dim pos As Integer = -1
        grdetalle.Row = grdetalle.RowCount - 1
        _fnObtenerFilaDetalle(pos, grdetalle.GetValue("fbnumi"))
        Dim existe As Boolean = _fnExisteProducto(grProductos.GetValue("yfnumi"))
        If ((pos >= 0) And (Not existe)) Then
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("fbty5prod") = grProductos.GetValue("yfnumi")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("producto") = grProductos.GetValue("yfcdprod1")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("fbumin") = grProductos.GetValue("yfumin")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("unidad") = grProductos.GetValue("UnidMin")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("fbpbas") = grProductos.GetValue("yhprecio")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("fbptot") = grProductos.GetValue("yhprecio")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("fbtotdesc") = grProductos.GetValue("yhprecio")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("fbcmin") = 1
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("stock") = grProductos.GetValue("stock")
            _prCalcularPrecioTotal()
            _DesHabilitarProductos()
        Else
            If (existe) Then
                Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
                ToastNotification.Show(Me, "El producto ya existe en el detalle".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            End If
        End If
    End Sub
    Sub _prImprimirReporte()
        Dim ef = New Efecto


        ef.tipo = 2
        ef.Context = "mensaje principal".ToUpper
        ef.Header = "¿DESEA IMPRIMIR REPORTE DE LA PROFORMA INSERTADA?".ToUpper
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then
            P_GenerarReporte()



        End If
    End Sub
    Public Function P_fnImageToByteArray(ByVal imageIn As Image) As Byte()
        Dim ms As New System.IO.MemoryStream()
        imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
        Return ms.ToArray()
    End Function

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
    Private Sub P_GenerarReporteFactura(numi As String)
        Dim dt As DataTable = L_fnImprimirFactura(numi)
        Dim total As Double = dt.Compute("SUM(Total)", "")

        Dim ParteEntera As Long
        Dim ParteDecimal As Double
        ParteEntera = Int(tbtotal.Value)
        ParteDecimal = tbtotal.Value - ParteEntera
        Dim li As String = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(ParteEntera)) + " con " +
        IIf(ParteDecimal.ToString.Equals("0"), "00", ParteDecimal.ToString) + "/100 Bolivianos"





        Dim objrep As New R_FacturaFarmacia
        '' GenerarNro(_dt)
        ''objrep.SetDataSource(Dt1Kardex)
        'imprimir
        If PrintDialog1.ShowDialog = DialogResult.OK Then
            objrep.SetDataSource(dt)
            objrep.SetParameterValue("TotalEscrito", li)
            objrep.SetParameterValue("nit", tbnit.Text)
            objrep.SetParameterValue("Total", tbtotal.Value)
            objrep.SetParameterValue("cliente", tbnombfact.Text)
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
    Private Sub P_GenerarReporte()
        Dim dt As DataTable = L_fnReporteProforma(tbCodigo.Text)

        If Not IsNothing(P_Global.Visualizador) Then
            P_Global.Visualizador.Close()
        End If

        P_Global.Visualizador = New Visualizador

        Dim objrep As New R_Proforma
        '' GenerarNro(_dt)
        ''objrep.SetDataSource(Dt1Kardex)
        objrep.SetDataSource(dt)
        objrep.SetParameterValue("usuario", gs_user)
        P_Global.Visualizador.CrGeneral.ReportSource = objrep 'Comentar
        P_Global.Visualizador.Show() 'Comentar
        P_Global.Visualizador.BringToFront() 'Comentar



    End Sub
    Public Sub _prCagarDetalleVenta(_numi As Integer)
        Dim dt As DataTable = L_fnDetalleVenta(_numi)

        '        a.tbnumi ,a.tbtv1numi ,a.tbty5prod ,b.yfcdprod1 as producto,a.tbest ,a.tbcmin ,a.tbumin ,Umin .ycdes3 as unidad,
        'a.tbpbas ,a.tbptot,a.tbporc,a.tbdesc ,a.tbtotdesc,a.tbobs ,
        '		a.tbpcos,a.tblote ,a.tbfechaVenc , a.tbptot2, a.tbfact ,a.tbhact ,a.tbuact,1 as estado,Cast(null as Image) as img,
        '		(Sum(inv.iccven )+a.tbcmin ) as stock
        CType(grdetalle.DataSource, DataTable).Rows.Clear()

        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
            Dim numiprod As Integer = 0
            Dim cantidad As Double = 0
            Dim descuento As Double = 0
            Dim subtotal As Double = 0
            Dim producto As String = dt.Rows(i).Item("producto")
            numiprod = dt.Rows(i).Item("tbty5prod")
            Dim umin As Integer = dt.Rows(i).Item("tbumin")
            Dim Unidad As String = dt.Rows(i).Item("unidad")
            If (Not _fnExisteProducto(numiprod)) Then

                Dim rows() As DataRow = dt.Select("tbty5prod=" + Str(numiprod))
                For k As Integer = 0 To rows.Count - 1 Step 1
                    cantidad = cantidad + rows(k).Item("tbcmin")
                    descuento = descuento + rows(k).Item("tbdesc")
                    subtotal = subtotal + rows(k).Item("tbtotdesc")

                Next
                'a.fbnumi ,a.fbtf1numi ,a.fbty5prod ,b.yfcdprod1 as producto,a.fbest ,a.fbcmin ,a.fbumin ,Umin .ycdes3 as unidad,
                'a.fbpbas ,a.fbptot,a.fbporc,a.fbdesc ,a.fbtotdesc,a.fblote ,a.fbfechaVenc , a.fbfact ,a.fbhact ,a.fbuact,1 as estado,Cast(null as Image) as img,
                '		(Sum(inv.iccven )+a.fbcmin ) as stock

                Dim pbas As Double = dt.Rows(i).Item("tbpbas")
                Dim montodesc As Double = descuento
                Dim pordesc As Double = ((montodesc * 100) / (cantidad * pbas))
                Dim Bin As New MemoryStream
                Dim img As New Bitmap(My.Resources.delete, 28, 28)
                img.Save(Bin, Imaging.ImageFormat.Png)
                CType(grdetalle.DataSource, DataTable).Rows.Add(_fnSiguienteNumi() + 1, _numi, numiprod, producto, 0, cantidad, umin, Unidad, pbas, (cantidad * pbas), pordesc, montodesc, subtotal, "20170101", CDate("2017/01/01"), Now.Date, "", "", 0, Bin.GetBuffer, 1000)

            End If


        Next

        _prCalcularPrecioTotal()

    End Sub

#End Region
#Region "Eventos Formulario"
    Private Sub F0_Ventas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _IniciarTodo()
    End Sub

    Function _prObtenerNroFactura() As Integer
        Dim dt As DataTable = L_fnObtenerNroFactura()
        If (dt.Rows.Count > 0) Then
            Dim nrofact As Integer = dt.Rows(0).Item("sbnfac")
            Return nrofact + 1
        End If
        Return 0

    End Function
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
                listEstCeldas.Add(New Modelo.Celda("ydrazonsocial", True, "RAZON SOCIAL", 280))
                listEstCeldas.Add(New Modelo.Celda("yddesc", True, "NOMBRE", 280))
                listEstCeldas.Add(New Modelo.Celda("yddctnum", True, "N. Documento".ToUpper, 150))
                listEstCeldas.Add(New Modelo.Celda("yddirec", True, "DIRECCION", 220))
                listEstCeldas.Add(New Modelo.Celda("ydtelf1", True, "Telefono".ToUpper, 200))
                listEstCeldas.Add(New Modelo.Celda("ydfnac", True, "F.Nacimiento".ToUpper, 150, "MM/dd,YYYY"))
                listEstCeldas.Add(New Modelo.Celda("ydnumivend", False, "", 50))
                listEstCeldas.Add(New Modelo.Celda("vendedor,", False, "", 50))
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
                    tbCliente.Text = Row.Cells("yddesc").Value
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
                    tbObservacion.Focus()

                End If

            End If

        End If
    End Sub

    Private Sub grdetalle_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grdetalle.EditingCell
        If (_fnAccesible()) Then
            'Habilitar solo las columnas de Precio, %, Monto y Observación
            If (e.Column.Index = grdetalle.RootTable.Columns("fbcmin").Index Or
                e.Column.Index = grdetalle.RootTable.Columns("fbporc").Index Or
                e.Column.Index = grdetalle.RootTable.Columns("fbdesc").Index) Then
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
            grdetalle.Col = 1
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

            If (grdetalle.Col = grdetalle.RootTable.Columns("fbcmin").Index) Then
                If (grdetalle.GetValue("producto") <> String.Empty) Then
                    _prAddDetalleVenta()
                    _HabilitarProductos()
                Else
                    ToastNotification.Show(Me, "Seleccione un Producto Por Favor", My.Resources.WARNING, 3000, eToastGlowColor.Red, eToastPosition.TopCenter)
                End If

            End If
            If (grdetalle.Col = grdetalle.RootTable.Columns("producto").Index) Then
                If (grdetalle.GetValue("producto") <> String.Empty) Then
                    _prAddDetalleVenta()
                    _HabilitarProductos()
                Else
                    ToastNotification.Show(Me, "Seleccione un Producto Por Favor", My.Resources.WARNING, 3000, eToastGlowColor.Red, eToastPosition.TopCenter)
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
        If (e.Column.Index = grdetalle.RootTable.Columns("fbcmin").Index) Then
            If (Not IsNumeric(grdetalle.GetValue("fbcmin")) Or grdetalle.GetValue("fbcmin").ToString = String.Empty) Then

                'grDetalle.GetRow(rowIndex).Cells("cant").Value = 1
                '  grDetalle.CurrentRow.Cells.Item("cant").Value = 1
                Dim lin As Integer = grdetalle.GetValue("fbnumi")
                Dim pos As Integer = -1
                _fnObtenerFilaDetalle(pos, lin)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("fbcmin") = 1
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("fbptot") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("fbpbas")

                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("fbporc") = 0
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("fbdesc") = 0
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("fbtotdesc") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("fbpbas")
                'grdetalle.SetValue("fbcmin", 1)
                'grdetalle.SetValue("fbptot", grdetalle.GetValue("fbpbas"))
            Else
                If (grdetalle.GetValue("fbcmin") > 0) Then
                    Dim rowIndex As Integer = grdetalle.Row
                    Dim porcdesc As Double = grdetalle.GetValue("fbporc")
                    Dim montodesc As Double = ((grdetalle.GetValue("fbpbas") * grdetalle.GetValue("fbcmin")) * (porcdesc / 100))
                    Dim lin As Integer = grdetalle.GetValue("fbnumi")
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, lin)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("fbdesc") = montodesc
                    grdetalle.SetValue("fbdesc", montodesc)
                    P_PonerTotal(rowIndex)

                Else
                    Dim lin As Integer = grdetalle.GetValue("fbnumi")
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, lin)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("fbcmin") = 1
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("fbptot") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("fbpbas")
                    _prCalcularPrecioTotal()
                    'grdetalle.SetValue("pbcmin", 1)
                    'grdetalle.SetValue("pbptot", grdetalle.GetValue("pbpbas"))

                End If
            End If
        End If
        '''''''''''''''''''''PORCENTAJE DE DESCUENTO '''''''''''''''''''''
        If (e.Column.Index = grdetalle.RootTable.Columns("fbporc").Index) Then
            If (Not IsNumeric(grdetalle.GetValue("fbporc")) Or grdetalle.GetValue("fbporc").ToString = String.Empty) Then

                'grDetalle.GetRow(rowIndex).Cells("cant").Value = 1
                '  grDetalle.CurrentRow.Cells.Item("cant").Value = 1
                Dim lin As Integer = grdetalle.GetValue("fbnumi")
                Dim pos As Integer = -1
                _fnObtenerFilaDetalle(pos, lin)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("fbporc") = 0
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("fbdesc") = 0
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("fbtotdesc") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("fbptot")
                'grdetalle.SetValue("pbcmin", 1)
                'grdetalle.SetValue("pbptot", grdetalle.GetValue("pbpbas"))
            Else
                If (grdetalle.GetValue("fbporc") > 0 And grdetalle.GetValue("fbporc") <= 100) Then

                    Dim porcdesc As Double = grdetalle.GetValue("fbporc")
                    Dim montodesc As Double = (grdetalle.GetValue("fbptot") * (porcdesc / 100))
                    Dim lin As Integer = grdetalle.GetValue("fbnumi")
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, lin)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("fbdesc") = montodesc
                    grdetalle.SetValue("fbdesc", montodesc)

                    Dim rowIndex As Integer = grdetalle.Row
                    P_PonerTotal(rowIndex)

                Else
                    Dim lin As Integer = grdetalle.GetValue("fbnumi")
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, lin)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("fbporc") = 0
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("fbdesc") = 0
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("fbtotdesc") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("fbptot")
                    grdetalle.SetValue("fbporc", 0)
                    grdetalle.SetValue("fbdesc", 0)
                    grdetalle.SetValue("fbtotdesc", grdetalle.GetValue("fbptot"))
                    _prCalcularPrecioTotal()
                    'grdetalle.SetValue("pbcmin", 1)
                    'grdetalle.SetValue("pbptot", grdetalle.GetValue("pbpbas"))

                End If
            End If
        End If


        '''''''''''''''''''''MONTO DE DESCUENTO '''''''''''''''''''''
        If (e.Column.Index = grdetalle.RootTable.Columns("fbdesc").Index) Then
            If (Not IsNumeric(grdetalle.GetValue("fbdesc")) Or grdetalle.GetValue("fbdesc").ToString = String.Empty) Then

                'grDetalle.GetRow(rowIndex).Cells("cant").Value = 1
                '  grDetalle.CurrentRow.Cells.Item("cant").Value = 1
                Dim lin As Integer = grdetalle.GetValue("fbnumi")
                Dim pos As Integer = -1
                _fnObtenerFilaDetalle(pos, lin)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("fbporc") = 0
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("fbdesc") = 0
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("fbtotdesc") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("fbptot")
                'grdetalle.SetValue("fbcmin", 1)
                'grdetalle.SetValue("fbptot", grdetalle.GetValue("fbfbas"))
            Else
                If (grdetalle.GetValue("fbdesc") > 0 And grdetalle.GetValue("fbdesc") <= grdetalle.GetValue("fbptot")) Then

                    Dim montodesc As Double = grdetalle.GetValue("fbdesc")
                    Dim pordesc As Double = ((montodesc * 100) / grdetalle.GetValue("fbptot"))

                    Dim lin As Integer = grdetalle.GetValue("fbnumi")
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, lin)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("fbdesc") = montodesc
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("fbporc") = pordesc

                    grdetalle.SetValue("fbporc", pordesc)
                    Dim rowIndex As Integer = grdetalle.Row
                    P_PonerTotal(rowIndex)

                Else
                    Dim lin As Integer = grdetalle.GetValue("fbnumi")
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, lin)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("fbporc") = 0
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("fbdesc") = 0
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("fbtotdesc") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("fbptot")
                    grdetalle.SetValue("fbporc", 0)
                    grdetalle.SetValue("fbdesc", 0)
                    grdetalle.SetValue("fbtotdesc", grdetalle.GetValue("fbptot"))
                    _prCalcularPrecioTotal()
                    'grdetalle.SetValue("fbcmin", 1)
                    'grdetalle.SetValue("fbptot", grdetalle.GetValue("fbfbas"))

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
                    Dim montodesc As Double = (grdetalle.GetTotal(grdetalle.RootTable.Columns("fbtotdesc"), AggregateFunction.Sum) * (porcdesc / 100))
                    tbMdesc.Value = montodesc

                    tbtotal.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("fbtotdesc"), AggregateFunction.Sum) - montodesc
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
                    Dim pordesc As Double = ((montodesc * 100) / grdetalle.GetTotal(grdetalle.RootTable.Columns("fbtotdesc"), AggregateFunction.Sum))
                    tbPdesc.Value = pordesc

                    tbtotal.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("fbtotdesc"), AggregateFunction.Sum) - montodesc

                End If

            End If

            If (tbMdesc.Text = String.Empty) Then
                tbMdesc.Value = 0

            End If
        End If

    End Sub


    Private Sub grdetalle_CellEdited(sender As Object, e As ColumnActionEventArgs) Handles grdetalle.CellEdited
        If (e.Column.Index = grdetalle.RootTable.Columns("fbcmin").Index) Then
            If (Not IsNumeric(grdetalle.GetValue("fbcmin")) Or grdetalle.GetValue("fbcmin").ToString = String.Empty) Then

                grdetalle.SetValue("fbcmin", 1)
                grdetalle.SetValue("fbptot", grdetalle.GetValue("fbpbas"))
                grdetalle.SetValue("fbporc", 0)
                grdetalle.SetValue("fbdesc", 0)
                grdetalle.SetValue("fbtotdesc", grdetalle.GetValue("fbpbas"))


            Else
                If (grdetalle.GetValue("fbcmin") > 0) Then

                    Dim cant As Integer = grdetalle.GetValue("fbcmin")
                    Dim stock As Integer = grdetalle.GetValue("stock")
                    If (cant > stock) Then
                        Dim lin As Integer = grdetalle.GetValue("fbnumi")
                        Dim pos As Integer = -1
                        _fnObtenerFilaDetalle(pos, lin)
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("fbcmin") = 1
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("fbptot") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("fbpbas")


                        Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
                        ToastNotification.Show(Me, "La cantidad de la venta no debe ser mayor al del stock" & vbCrLf &
                        "Stock=" + Str(stock).ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                        grdetalle.SetValue("fbcmin", 1)
                        grdetalle.SetValue("fbptot", grdetalle.GetValue("fbpbas"))

                        _prCalcularPrecioTotal()
                    Else
                        If (cant = stock) Then


                            'grdetalle.SelectedFormatStyle.ForeColor = Color.Blue
                            'grdetalle.CurrentRow.Cells.Item(e.Column).FormatStyle = New GridEXFormatStyle
                            'grdetalle.CurrentRow.Cells(e.Column).FormatStyle.BackColor = Color.Pink
                            'grdetalle.CurrentRow.Cells.Item(e.Column).FormatStyle.BackColor = Color.DodgerBlue
                            'grdetalle.CurrentRow.Cells.Item(e.Column).FormatStyle.ForeColor = Color.White
                            'grdetalle.CurrentRow.Cells.Item(e.Column).FormatStyle.FontBold = TriState.True
                        End If
                    End If

                Else

                    grdetalle.SetValue("fbcmin", 1)
                    grdetalle.SetValue("fbptot", grdetalle.GetValue("fbpbas"))
                    grdetalle.SetValue("fbporc", 0)
                    grdetalle.SetValue("fbdesc", 0)
                    grdetalle.SetValue("fbtotdesc", grdetalle.GetValue("fbpbas"))

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
            Dim res As Boolean = L_fnEliminarFactura(tbCodigo.Text, mensajeError)
            If res Then


                Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)

                ToastNotification.Show(Me, "Código de Factura ".ToUpper + tbCodigo.Text + " eliminado con Exito.".ToUpper,
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

    Private Sub swTipoVenta_KeyDown(sender As Object, e As KeyEventArgs)
        If (e.KeyData = Keys.Enter) Then

            grdetalle.Select()

            grdetalle.Col = 3
            grdetalle.Row = 0

            'grdetalle.Focus()
        End If
    End Sub

    Private Sub ButtonX1_Click(sender As Object, e As EventArgs)
        If (Not _fnAccesible()) Then

            _prImiprimirFacturaPreimpresa(tbCodigo.Text)

        End If
    End Sub

    Private Sub cbSucursal_Leave(sender As Object, e As EventArgs) Handles cbSucursal.Leave
        grdetalle.Select()
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        If (Not _fnAccesible()) Then

            _prImiprimirFacturaPreimpresa(tbCodigo.Text)

        End If
    End Sub

    Private Sub tbdespacho_KeyDown(sender As Object, e As KeyEventArgs) Handles tbdespacho.KeyDown
        If (_fnAccesible()) Then
            If e.KeyData = Keys.Control + Keys.Enter Then

                Dim dt As DataTable

                dt = L_fnListarVentasFactura()
                '         a.tanumi ,a.taalm  ,a.taven ,vendedor .yddesc as vendedor,a.taclpr,
                'cliente.yddesc as cliente ,a.tamon ,IIF(tamon=1,'Boliviano','Dolar') as moneda  ,
                'a.tadesc  ,a.tatotal as total,cliente .ydnit as Nit,cliente .ydnomfac  as NombreFactura

                Dim listEstCeldas As New List(Of Modelo.Celda)
                listEstCeldas.Add(New Modelo.Celda("tanumi,", True, "NRO DESPACHO", 120))
                listEstCeldas.Add(New Modelo.Celda("taalm", False, "", 50))
                listEstCeldas.Add(New Modelo.Celda("taven", False, "", 50))
                listEstCeldas.Add(New Modelo.Celda("vendedor", True, "VENDEDOR", 200))
                listEstCeldas.Add(New Modelo.Celda("taclpr", False, "", 50))
                listEstCeldas.Add(New Modelo.Celda("cliente", True, "CLIENTE", 200))
                listEstCeldas.Add(New Modelo.Celda("tamon", False, "", 50))
                listEstCeldas.Add(New Modelo.Celda("moneda", False, "", 50))
                listEstCeldas.Add(New Modelo.Celda("tadesc", False, "", 50))
                listEstCeldas.Add(New Modelo.Celda("total", True, "TOTAL", 180))

                listEstCeldas.Add(New Modelo.Celda("Nit", False, "", 50))
                listEstCeldas.Add(New Modelo.Celda("NombreFactura", False, "", 50))
                Dim ef = New Efecto
                ef.tipo = 3
                ef.dt = dt
                ef.SeleclCol = 2
                ef.listEstCeldas = listEstCeldas
                ef.alto = 50
                ef.ancho = 350
                ef.Context = "Seleccione DESPACHO".ToUpper
                ef.ShowDialog()
                Dim bandera As Boolean = False
                bandera = ef.band
                If (bandera = True) Then
                    Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row

                    _CodCliente = Row.Cells("taclpr").Value
                    tbCliente.Text = Row.Cells("cliente").Value
                    _CodEmpleado = Row.Cells("taven").Value
                    tbVendedor.Text = Row.Cells("vendedor").Value
                    cbSucursal.Value = Row.Cells("taalm").Value
                    tbdespacho.Text = Row.Cells("tanumi").Value
                    swMoneda.Value = Row.Cells("tamon").Value
                    tbnit.Text = IIf(IsDBNull(Row.Cells("Nit").Value), "", Row.Cells("Nit").Value)
                    tbnombfact.Text = IIf(IsDBNull(Row.Cells("NombreFactura").Value), "", Row.Cells("NombreFactura").Value)
                    tbMdesc.Value = Row.Cells("tadesc").Value
                    _prCagarDetalleVenta(Row.Cells("tanumi").Value)

                    grdetalle.Select()

                End If

            End If


        End If


    End Sub


#End Region
   
   
End Class