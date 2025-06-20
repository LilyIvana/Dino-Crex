﻿Imports Logica.AccesoLogica
Imports Janus.Windows.GridEX
Imports DevComponents.DotNetBar
Imports System.IO
Imports DevComponents.DotNetBar.SuperGrid
Imports System.Drawing
Imports DevComponents.DotNetBar.Controls
Imports System.Threading
Imports System.Drawing.Text
Imports System.Reflection
Imports System.Runtime.InteropServices
Public Class F0_MCompras
    Dim _Inter As Integer = 0

#Region "Variables Globales"
    Dim _CodProveedor As Integer = 0
    Dim _numiCatCosto As Integer = 0
    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
    Public Table_Producto As DataTable
    Dim _PorcentajeUtil As Double = 0 '' En esta varible obtendre de la libreria el porcentaje de utilidad para la venta 
    Dim _estadoPor As Integer ''En esta variable me dira si sera visible o no las columnas de porcentaje de utilidad y precio de venta
    Dim Lote As Boolean = False
    Public _detalleCompras As DataTable 'Almacena el detalle para insertar a la tabla TPA001 del BDDiconDinoEco

    Dim RutaGlobal As String = gs_CarpetaRaiz

#End Region

#Region "Metodos Privados"
    Private Sub _IniciarTodo()
        'L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        _prValidarLote()
        _prCargarComboLibreriaSucursal(cbSucursal)
        _prObtenerPorcentajeUtilidad()
        'Me.WindowState = FormWindowState.Maximized
        _prCargarCompra()
        _prInhabiliitar()
        grCompra.Focus()
        _prAsignarPermisos()
        Me.Text = "COMPRAS"
        PanelDetalle.Height = 250
        MSuperTabControl.SelectedTabIndex = 0


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
    Public Sub _prObtenerPorcentajeUtilidad()
        ''''En este procedimiento obtendre el porcentaje de utilidad que esta en la tabla de configuraciones
        Dim dt As DataTable = L_fnPorcUtilidad()
        If (dt.Rows.Count > 0) Then
            _PorcentajeUtil = dt.Rows(0).Item("PorcUtil")
            _estadoPor = dt.Rows(0).Item("VerPorc")
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
            .DropDownList.Columns("aabdes").Caption = "ALMACÉN"
            .ValueMember = "aanumi"
            .DisplayMember = "aabdes"
            .DataSource = dt
            .Refresh()
        End With
    End Sub
    Private Sub _prInhabiliitar()
        tbCodigo.ReadOnly = True
        tbProveedor.ReadOnly = True
        tbCodProv.ReadOnly = True
        tbObservacion.ReadOnly = True
        tbFechaVenta.IsInputReadOnly = True
        tbFechaVenc.IsInputReadOnly = True
        cbSucursal.ReadOnly = True
        swTipoVenta.IsReadOnly = True
        swMostrarProdProv.IsReadOnly = True

        tbNitProv.ReadOnly = True
        tbRazonSocial.ReadOnly = True
        swEmision.IsReadOnly = True
        swConsigna.IsReadOnly = True
        swRetencion.IsReadOnly = True
        swMoneda.IsReadOnly = True
        tbTipoCambio.IsInputReadOnly = True

        tbNFactura.ReadOnly = True
        tbNAutorizacion.ReadOnly = True
        tbCodControl.ReadOnly = True
        tbNDui.ReadOnly = True
        tbSACF.ReadOnly = True

        ''''''''''
        btnModificar.Enabled = True
        btnGrabar.Enabled = False
        btnNuevo.Enabled = True
        btnEliminar.Enabled = True

        tbSubtotalC.IsInputReadOnly = True
        tbMdesc.IsInputReadOnly = True
        tbtotal.IsInputReadOnly = True

        grCompra.Enabled = True
        PanelNavegacion.Enabled = True
        grdetalle.RootTable.Columns("img").Visible = False
        If (GPanelProductos.Visible = True) Then
            _DesHabilitarProductos()
        End If
    End Sub
    Private Sub _prhabilitar()
        grCompra.Enabled = False
        tbCodigo.ReadOnly = False
        ''  tbCliente.ReadOnly = False  por que solo podra seleccionar Cliente
        ''  tbVendedor.ReadOnly = False
        tbObservacion.ReadOnly = False
        swMostrarProdProv.IsReadOnly = False
        tbFechaVenta.IsInputReadOnly = False
        tbFechaVenc.IsInputReadOnly = False
        'If (tbCodigo.Text.Length > 0) Then
        '    cbSucursal.ReadOnly = True
        'Else
        '    cbSucursal.ReadOnly = False
        'End If

        swTipoVenta.IsReadOnly = False
        btnGrabar.Enabled = True

        tbNitProv.ReadOnly = False
        tbRazonSocial.ReadOnly = False
        swEmision.IsReadOnly = False
        swConsigna.IsReadOnly = False
        swRetencion.IsReadOnly = False
        tbNFactura.ReadOnly = False
        tbNAutorizacion.ReadOnly = False
        tbCodControl.ReadOnly = False
        tbNDui.ReadOnly = False
        tbSACF.ReadOnly = False

        tbMdesc.IsInputReadOnly = False

        swMoneda.IsReadOnly = False
        tbTipoCambio.IsInputReadOnly = False

        grdetalle.ContextMenuStrip = cmOpcionesDetalle
    End Sub
    Public Sub _prFiltrar()
        'cargo el buscador
        Dim _Mpos As Integer
        _prCargarCompra()
        If grCompra.RowCount > 0 Then
            _Mpos = 0
            grCompra.Row = _Mpos
        Else
            _Limpiar()
            LblPaginacion.Text = "0/0"
        End If
    End Sub
    Private Sub _Limpiar()
        tbCodigo.Clear()
        tbProveedor.Clear()
        tbNitProv.Clear()
        tbRazonSocial.Clear()
        tbObservacion.Clear()
        swMostrarProdProv.Value = True
        If (CType(cbSucursal.DataSource, DataTable).Rows.Count > 0) Then
            cbSucursal.SelectedIndex = 0
        Else
            cbSucursal.SelectedIndex = -1
        End If
        swTipoVenta.Value = False
        _CodProveedor = 0
        tbFechaVenta.Value = Now.Date
        tbFechaVenc.Value = Now.Date
        tbFechaVenc.Visible = True
        lbCredito.Visible = True
        tbCodProv.Clear()
        swEmision.Value = True
        swConsigna.Value = False
        swRetencion.Value = False
        swMoneda.Value = True
        tbTipoCambio.Value = 0

        tbNFactura.Clear()
        tbNAutorizacion.Clear()
        tbCodControl.Clear()
        tbNDui.Clear()
        tbSACF.Clear()

        _prCargarDetalleVenta(-1)

        MSuperTabControl.SelectedTabIndex = 0

        tbPdesc.Value = 0
        tbMdesc.Value = 0
        tbtotal.Value = 0
        tbSubtotalC.Value = 0
        tbDescPro.Value = 0
        tbIce.Value = 0
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
        tbProveedor.Focus()
        Table_Producto = Nothing

        'Validar si es recibo o factura
        If swEmision.Value = False Then
            lbNFactura.Text = "Nro. Documento:"
            GroupPanelFactura2.Text = "DOCUMENTO"
            lbNAutoriz.Visible = False
            tbNAutorizacion.Visible = False
            lbCodCtrl.Visible = False
            tbCodControl.Visible = False
            lbNDui.Visible = False
            tbNDui.Visible = False
            lbSACF.Visible = False
            tbSACF.Visible = False
        Else
            lbNFactura.Text = "Nro. Factura:"
            GroupPanelFactura2.Text = "DATOS FACTURACIÓN"
            lbNAutoriz.Visible = True
            tbNAutorizacion.Visible = True
            lbCodCtrl.Visible = True
            tbCodControl.Visible = True
            lbNDui.Visible = True
            tbNDui.Visible = True
            lbSACF.Visible = True
            tbSACF.Visible = True
        End If

        If swMoneda.Value = True Then
            lbTipoCambio.Visible = False
            tbTipoCambio.Visible = False
            tbTipoCambio.Value = 1
        Else
            lbTipoCambio.Visible = True
            tbTipoCambio.Visible = True
        End If
    End Sub
    Public Sub _prMostrarRegistro(_N As Integer)

        With grCompra
            tbCodigo.Text = .GetValue("canumi")
            tbFechaVenta.Value = .GetValue("cafdoc")
            _CodProveedor = .GetValue("caty4prov")
            swTipoVenta.Value = .GetValue("catven")
            tbProveedor.Text = .GetValue("proveedor")
            cbSucursal.Value = .GetValue("caalm")
            tbObservacion.Text = .GetValue("caobs")
            tbCodProv.Text = .GetValue("caty4prov").ToString
            swEmision.Value = .GetValue("caemision")
            tbNFactura.Text = .GetValue("canumemis")
            'tbNitProv.Text = .GetValue("yddctnum")
            swConsigna.Value = .GetValue("caconsigna")
            swRetencion.Value = .GetValue("caretenc")
            swMoneda.Value = .GetValue("camon")
            tbTipoCambio.Value = .GetValue("catipocambio")

            'If (swTipoVenta.Value = False) Then
            tbFechaVenc.Value = .GetValue("cafvcr")
            'Else
            '    lbCredito.Visible = False
            '    tbFechaVenc.Visible = False
            'End If

            lbFecha.Text = CType(.GetValue("cafact"), Date).ToString("dd/MM/yyyy")
            lbHora.Text = .GetValue("cahact").ToString
            lbUsuario.Text = .GetValue("cauact").ToString
        End With
        Dim dtConsignacion = L_fnProveedorConsignacion(tbCodProv.Text)
        If dtConsignacion.Rows.Count > 0 Then
            btnDuplicar.Visible = True
        Else
            btnDuplicar.Visible = False
        End If

        _prCargarDetalleVenta(tbCodigo.Text)
        _prCargarDetalleVenta2(tbCodigo.Text)
        tbMdesc.Value = grCompra.GetValue("cadesc")

        If swRetencion.Value = False Then
            tbSubtotalC.Value = grCompra.GetValue("casubtot")
            tbMdesc.Value = grCompra.GetValue("cadesc")
            tbDescPro.Value = grCompra.GetValue("cadescpro")
            tbIce.Value = grCompra.GetValue("caice")
            tbtotal.Value = grCompra.GetValue("total")
        Else
            tbtotal.Value = grCompra.GetValue("total")
            tbSubtotalC.Value = tbtotal.Value + tbMdesc.Value
        End If

        LblPaginacion.Text = Str(grCompra.Row + 1) + "/" + grCompra.RowCount.ToString

        If swEmision.Value = True Then
            _prCargarFacturacion(tbCodigo.Text)

            lbNFactura.Text = "Nro. Factura:"
            GroupPanelFactura2.Text = "DATOS FACTURACIÓN"
            lbNAutoriz.Visible = True
            tbNAutorizacion.Visible = True
            lbCodCtrl.Visible = True
            tbCodControl.Visible = True
            lbNDui.Visible = True
            tbNDui.Visible = True
            lbSACF.Visible = True
            tbSACF.Visible = True
        Else
            lbNFactura.Text = "Nro. Documento:"
            GroupPanelFactura2.Text = "DOCUMENTO"
            lbNAutoriz.Visible = False
            tbNAutorizacion.Visible = False
            lbCodCtrl.Visible = False
            tbCodControl.Visible = False
            lbNDui.Visible = False
            tbNDui.Visible = False
            lbSACF.Visible = False
            tbSACF.Visible = False

            tbNitProv.Text = ""
            tbRazonSocial.Text = ""
        End If

        If swMoneda.Value = True Then
            lbTipoCambio.Visible = False
            tbTipoCambio.Visible = False
        Else
            lbTipoCambio.Visible = True
            tbTipoCambio.Visible = True
        End If
    End Sub

    Private Sub _prCargarDetalleVenta(_numi As String)
        Dim dt As New DataTable
        dt = L_fnDetalleCompra(_numi)
        grdetalle.DataSource = dt
        grdetalle.RetrieveStructure()
        grdetalle.AlternatingColors = True

        If (Lote = True) Then
            With grdetalle.RootTable.Columns("cblote")
                .Width = 150
                .Caption = "LOTE"
                .Visible = True
                .MaxLength = 50
            End With
            With grdetalle.RootTable.Columns("cbfechavenc")
                .Width = 120
                .Caption = "FECHA VENC."
                .Visible = True
                .FormatString = "dd/MM/yyyy"
            End With
        Else
            With grdetalle.RootTable.Columns("cblote")
                .Width = 150
                .Caption = "LOTE"
                .Visible = False
                .MaxLength = 50
            End With
            With grdetalle.RootTable.Columns("cbfechavenc")
                .Width = 120
                .Caption = "FECHA VENC."
                .Visible = False
                .FormatString = "dd/MM/yyyy"
            End With
        End If

        With grdetalle.RootTable.Columns("cbnumi")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("cbtv1numi")
            .Width = 90
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("cbty5prod")
            .Width = 80
            .Caption = "COD. DYNASYS"
            .Visible = True
        End With
        With grdetalle.RootTable.Columns("yfcprod")
            .Width = 80
            .Caption = "COD. DELTA"
            .Visible = True
        End With
        With grdetalle.RootTable.Columns("producto")
            .Caption = "PRODUCTOS"
            .Width = 320
            .Visible = True
            .WordWrap = True
            .MaxLines = 2
        End With
        With grdetalle.RootTable.Columns("cbest")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("cbcmin")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Cantidad Un.".ToUpper
        End With
        With grdetalle.RootTable.Columns("cbumin")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("unidad")
            .Width = 80
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .Caption = "Unidad".ToUpper
        End With
        With grdetalle.RootTable.Columns("cbpcost")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Precio Un.".ToUpper
        End With
        If (_estadoPor = 1) Then
            With grdetalle.RootTable.Columns("cbutven")
                .Width = 110
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .Visible = True
                .FormatString = "0.00"
                .Caption = "Utilidad (%)".ToUpper
            End With
            With grdetalle.RootTable.Columns("cbprven")
                .Width = 120
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .Visible = True
                .FormatString = "0.00"
                .Caption = "Precio Venta".ToUpper
            End With
        Else
            With grdetalle.RootTable.Columns("cbutven")
                .Width = 120
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .Visible = False
                .FormatString = "0.00"
                .Caption = "Utilidad.".ToUpper
            End With
            With grdetalle.RootTable.Columns("cbprven")
                .Width = 120
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .Visible = False
                .FormatString = "0.00"
                .Caption = "Precio Venta.".ToUpper
            End With
        End If

        With grdetalle.RootTable.Columns("cbptot")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Sub Total".ToUpper
            '.AggregateFunction = AggregateFunction.Sum
        End With
        With grdetalle.RootTable.Columns("cbdesc")
            .Width = 110
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Descto. Un.".ToUpper
            '.AggregateFunction = AggregateFunction.Sum
        End With
        With grdetalle.RootTable.Columns("cbdescpro")
            .Width = 110
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Descto Pro.".ToUpper
            '.AggregateFunction = AggregateFunction.Sum
        End With
        With grdetalle.RootTable.Columns("cbice")
            .Width = 90
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .Caption = "ICE".ToUpper
            '.AggregateFunction = AggregateFunction.Sum            '
            .FormatString = "0.00"
        End With
        With grdetalle.RootTable.Columns("cbtotal")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Total".ToUpper
            '.AggregateFunction = AggregateFunction.Sum
        End With
        With grdetalle.RootTable.Columns("cbpcostoun")
            .Width = 120
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "P.Costo Un.".ToUpper
        End With
        With grdetalle.RootTable.Columns("cbobs")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("cbfact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("cbhact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("cbuact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("estado")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("costo")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("img")
            .Width = 80
            .Caption = "Eliminar".ToUpper
            .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("venta")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With
        With grdetalle
            .GroupByBoxVisible = False
            'diseño de la grilla
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .FilterMode = FilterMode.Automatic

            'If tbObservacion.ReadOnly = True Then
            '    .DefaultFilterRowComparison = FilterConditionOperator.Contains
            '    .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            '    .FilterMode = FilterMode.Automatic
            'Else
            '    .DefaultFilterRowComparison = False
            '    .FilterRowUpdateMode = False
            '    .FilterMode = False
            'End If

            .VisualStyle = VisualStyle.Office2007

            .TotalRow = InheritableBoolean.True
            .TotalRowFormatStyle.BackColor = Color.Gold
            .TotalRowPosition = TotalRowPosition.BottomFixed

            .RecordNavigator = True
            .RecordNavigatorText = "Productos"
        End With
    End Sub
    Private Sub _prCargarDetalleVenta2(_numi As String)
        Dim dt As New DataTable
        dt = L_fnDetalleCompra2(_numi)
        grdetalle2.DataSource = dt
        grdetalle2.RetrieveStructure()
        grdetalle2.AlternatingColors = True

        If (Lote = True) Then
            With grdetalle2.RootTable.Columns("cblote")
                .Width = 150
                .Caption = "LOTE"
                .Visible = True
                .MaxLength = 50
            End With
            With grdetalle2.RootTable.Columns("cbfechavenc")
                .Width = 120
                .Caption = "FECHA VENC."
                .Visible = True
                .FormatString = "dd/MM/yyyy"
            End With
        Else
            With grdetalle2.RootTable.Columns("cblote")
                .Width = 150
                .Caption = "LOTE"
                .Visible = False
                .MaxLength = 50
            End With
            With grdetalle2.RootTable.Columns("cbfechavenc")
                .Width = 120
                .Caption = "FECHA VENC."
                .Visible = False
                .FormatString = "dd/MM/yyyy"
            End With
        End If
        With grdetalle2.RootTable.Columns("cbnumi")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = False
        End With
        With grdetalle2.RootTable.Columns("cbtv1numi")
            .Width = 100
            .Caption = "COD. COMPRA"
            .Visible = True
        End With
        With grdetalle2.RootTable.Columns("caty4prov")
            .Width = 90
            .Visible = False
        End With
        With grdetalle2.RootTable.Columns("proveedor")
            .Width = 120
            .Caption = "PROVEEDOR"
            .Visible = True
        End With
        With grdetalle2.RootTable.Columns("FechaCompra")
            .Width = 100
            .Caption = "FECHA COMPRA"
            .Visible = True
        End With
        With grdetalle2.RootTable.Columns("canumemis")
            .Width = 100
            .Caption = "NRO. FACT/DOC"
            .Visible = True
        End With
        With grdetalle2.RootTable.Columns("cbty5prod")
            .Width = 80
            .Caption = "COD. DYNASYS"
            .Visible = True
        End With
        With grdetalle2.RootTable.Columns("yfcprod")
            .Width = 90
            .Caption = "COD. DELTA"
            .Visible = True
        End With
        With grdetalle2.RootTable.Columns("yfcbarra")
            .Width = 100
            .Caption = "COD. BARRAS"
            .Visible = True
        End With
        With grdetalle2.RootTable.Columns("producto")
            .Caption = "PRODUCTOS"
            .Width = 280
            .Visible = True
        End With
        With grdetalle2.RootTable.Columns("cbest")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With
        With grdetalle2.RootTable.Columns("cbcmin")
            .Width = 120
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Cantidad Un.".ToUpper
        End With
        With grdetalle2.RootTable.Columns("cbumin")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With
        With grdetalle2.RootTable.Columns("unidad")
            .Width = 80
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .Caption = "Unidad".ToUpper
        End With
        With grdetalle2.RootTable.Columns("cbpcost")
            .Width = 110
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Precio Un.".ToUpper
        End With
        If (_estadoPor = 1) Then
            With grdetalle2.RootTable.Columns("cbutven")
                .Width = 110
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .Visible = True
                .FormatString = "0.00"
                .Caption = "Utilidad (%)".ToUpper
            End With
            With grdetalle2.RootTable.Columns("cbprven")
                .Width = 120
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .Visible = True
                .FormatString = "0.00"
                .Caption = "Precio Venta".ToUpper
            End With
        Else
            With grdetalle2.RootTable.Columns("cbutven")
                .Width = 120
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .Visible = False
                .FormatString = "0.00"
                .Caption = "Utilidad.".ToUpper
            End With
            With grdetalle2.RootTable.Columns("cbprven")
                .Width = 120
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .Visible = False
                .FormatString = "0.00"
                .Caption = "Precio Venta.".ToUpper
            End With
        End If
        With grdetalle2.RootTable.Columns("cbptot")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Sub Total".ToUpper
            '.AggregateFunction = AggregateFunction.Sum
        End With
        With grdetalle2.RootTable.Columns("cbdesc")
            .Width = 130
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Descuento Un.".ToUpper
            '.AggregateFunction = AggregateFunction.Sum
        End With
        With grdetalle2.RootTable.Columns("cbdescpro")
            .Width = 130
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Descuento Pro.".ToUpper
            '.AggregateFunction = AggregateFunction.Sum
        End With
        With grdetalle2.RootTable.Columns("cbice")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .Caption = "ICE".ToUpper
            '.AggregateFunction = AggregateFunction.Sum            '
            .FormatString = "0.00"
        End With
        With grdetalle2.RootTable.Columns("cbtotal")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Total".ToUpper
            '.AggregateFunction = AggregateFunction.Sum
        End With
        With grdetalle2.RootTable.Columns("cbpcostoun")
            .Width = 120
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "P.Costo Un.".ToUpper
        End With
        With grdetalle2.RootTable.Columns("cbobs")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle2.RootTable.Columns("cbfact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle2.RootTable.Columns("cbhact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle2.RootTable.Columns("cbuact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle2.RootTable.Columns("estado")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle2.RootTable.Columns("costo")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With
        With grdetalle2.RootTable.Columns("img")
            .Width = 80
            .Caption = "Eliminar".ToUpper
            .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
            .Visible = False
        End With
        With grdetalle2.RootTable.Columns("venta")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With
        With grdetalle2.RootTable.Columns("caobs")
            .Width = 130
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Caption = "OBSERVACIÓN"
            .Visible = True
        End With
        With grdetalle2
            .GroupByBoxVisible = False
            'diseño de la grilla
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .FilterMode = FilterMode.Automatic
            .VisualStyle = VisualStyle.Office2007

            .TotalRow = InheritableBoolean.True
            .TotalRowFormatStyle.BackColor = Color.Gold
            .TotalRowPosition = TotalRowPosition.BottomFixed

            .RecordNavigator = True
            .RecordNavigatorText = "Productos"
        End With
    End Sub
    Private Sub _prCargarFacturacion(_numi As String)
        Dim dtC As New DataTable
        dtC = L_prCompraComprobanteGeneralPorNumi(_numi)

        If dtC.Rows.Count = 0 Then
            tbNAutorizacion.Text = ""
            tbCodControl.Text = ""
            tbNDui.Text = ""
            tbSACF.Text = ""

            tbNitProv.Text = ""
            tbRazonSocial.Text = ""
        Else
            tbNAutorizacion.Text = dtC.Rows(0).Item("fcaautoriz")
            tbCodControl.Text = dtC.Rows(0).Item("fcaccont")
            tbNDui.Text = dtC.Rows(0).Item("fcandui")
            tbSACF.Text = tbtotal.Text - dtC.Rows(0).Item("fcanscf")

            tbNitProv.Text = dtC.Rows(0).Item("fcanit")
            tbRazonSocial.Text = dtC.Rows(0).Item("fcarsocial")
        End If

    End Sub

    Private Sub _prCargarCompra()
        Dim dt As New DataTable
        dt = L_fnGeneralCompras(IIf(swMostrar.Value = True, 1, 0))
        grCompra.DataSource = dt
        grCompra.RetrieveStructure()
        grCompra.AlternatingColors = True

        With grCompra.RootTable.Columns("canumi")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = True
        End With
        With grCompra.RootTable.Columns("caalm")
            .Width = 90
            .Visible = False
        End With
        With grCompra.RootTable.Columns("cafdoc")
            .Width = 90
            .Visible = True
            .Caption = "FECHA"
        End With
        With grCompra.RootTable.Columns("canumemis")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .Caption = "Nro. Fact/Doc".ToUpper
        End With
        With grCompra.RootTable.Columns("caty4prov")
            .Width = 160
            .Visible = False
        End With
        With grCompra.RootTable.Columns("ydcod")
            .Width = 160
            .Visible = False
        End With
        With grCompra.RootTable.Columns("proveedor")
            .Width = 180
            .Visible = True
            .Caption = "proveedor".ToUpper
        End With
        With grCompra.RootTable.Columns("yddctnum")
            .Width = 100
            .Visible = False
            .Caption = "Ci/Nit".ToUpper
        End With
        With grCompra.RootTable.Columns("Nit")
            .Width = 100
            .Visible = True
            .Caption = "Nit".ToUpper
        End With
        With grCompra.RootTable.Columns("rSocial")
            .Width = 140
            .Visible = True
            .Caption = "Razón Social".ToUpper
        End With
        With grCompra.RootTable.Columns("catven")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grCompra.RootTable.Columns("tcompra")
            .Width = 90
            .Caption = "Tipo Compra".ToUpper
            .Visible = True
        End With
        With grCompra.RootTable.Columns("cafvcr")
            .Width = 90
            .Visible = True
            .Caption = "F.VENC. CREDITO"
        End With
        With grCompra.RootTable.Columns("camon")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grCompra.RootTable.Columns("moneda")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
            .Caption = "MONEDA"
        End With
        With grCompra.RootTable.Columns("caobs")
            .Width = 280
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Caption = "OBSERVACION"
        End With
        With grCompra.RootTable.Columns("caest")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grCompra.RootTable.Columns("cafact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grCompra.RootTable.Columns("cahact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grCompra.RootTable.Columns("cauact")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Caption = "USUARIO"
        End With
        With grCompra.RootTable.Columns("casubtot")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .Caption = "SUBTOTAL"
            .FormatString = "0.00"
        End With
        With grCompra.RootTable.Columns("cadesc")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .Caption = "DESCUENTO UN."
            .FormatString = "0.00"
        End With
        With grCompra.RootTable.Columns("cadescpro")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .Caption = "DESCUENTO PRO."
            .FormatString = "0.00"
        End With
        With grCompra.RootTable.Columns("caice")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .Caption = "ICE"
            .FormatString = "0.00"
        End With
        With grCompra.RootTable.Columns("total")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .Caption = "TOTAL"
            .FormatString = "0.00"
            .AggregateFunction = AggregateFunction.Sum
        End With
        With grCompra.RootTable.Columns("caemision")
            .Width = 80
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .Caption = "Emision"
        End With
        With grCompra.RootTable.Columns("caconsigna")
            .Width = 120
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .Caption = "Consigna"
        End With
        With grCompra.RootTable.Columns("caretenc")
            .Width = 120
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .Caption = "Retención"
        End With
        With grCompra.RootTable.Columns("catipocambio")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With
        With grCompra.RootTable.Columns("obs2")
            .Visible = False
        End With
        With grCompra
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
            .TotalRow = InheritableBoolean.True
            .TotalRowFormatStyle.BackColor = Color.Gold
            .TotalRowPosition = TotalRowPosition.BottomFixed

            .RecordNavigator = True
            .RecordNavigatorText = "Compras"
        End With
        _prAplicarCondiccionJanus()

        If (dt.Rows.Count <= 0) Then
            _prCargarDetalleVenta(-1)
        End If
    End Sub
    Public Sub _prAplicarCondiccionJanus()
        Dim fr As GridEXFormatCondition
        fr = New GridEXFormatCondition(grCompra.RootTable.Columns("obs2"), ConditionOperator.Equal, "PR ")
        fr.FormatStyle.ForeColor = Color.Red
        grCompra.RootTable.FormatConditions.Add(fr)
    End Sub
    Private Sub _prCargarProductos(_cliente As String)

        Dim dtname As DataTable = L_fnNameLabel()
        Dim dt As New DataTable
        If (cbSucursal.SelectedIndex < 0) Then
            Return
        End If
        If swMostrarProdProv.Value = True Then
            dt = L_fnListarProductosCompraNueva(cbSucursal.Value, 73, Convert.ToInt64(tbCodProv.Text))
        Else
            dt = L_fnListarProductosCompra(cbSucursal.Value, 73)
        End If
        ''1=Almacen  73=Cat Precio Costo
        grProductos.DataSource = dt
        grProductos.RetrieveStructure()
        grProductos.AlternatingColors = True

        With grProductos.RootTable.Columns("yfnumi")
            .Width = 80
            .Caption = "COD. DYNASYS"
            .Visible = True
        End With
        With grProductos.RootTable.Columns("yfcprod")
            .Width = 80
            .Caption = "COD. DELTA"
            .Visible = True
        End With
        With grProductos.RootTable.Columns("yfcbarra")
            .Width = 110
            .Caption = "COD. BARRA"
            .Visible = True
        End With
        With grProductos.RootTable.Columns("yfcdprod1")
            .Width = 450
            .Visible = True
            .Caption = "DESCRIPCIÓN"
            .WordWrap = True
            .MaxLines = 2
        End With
        With grProductos.RootTable.Columns("yfcdprod2")
            .Width = 120
            .Visible = True
            .Caption = "COD. PROVEEDOR"
        End With
        With grProductos.RootTable.Columns("yfgr1")
            .Width = 160
            .Visible = False
        End With
        If (dtname.Rows.Count > 0) Then
            With grProductos.RootTable.Columns("grupo1")
                .Width = 100
                .Caption = dtname.Rows(0).Item("Grupo 1").ToString
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = True
            End With
            With grProductos.RootTable.Columns("grupo2")
                .Width = 100
                .Caption = dtname.Rows(0).Item("Grupo 2").ToString
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = True
            End With

            With grProductos.RootTable.Columns("grupo3")
                .Width = 100
                .Caption = dtname.Rows(0).Item("Grupo 3").ToString
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = True
            End With
            With grProductos.RootTable.Columns("grupo4")
                .Width = 100
                .Caption = dtname.Rows(0).Item("Grupo 4").ToString
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = True
            End With
        Else
            With grProductos.RootTable.Columns("grupo1")
                .Width = 100
                .Caption = "Grupo 1"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = True
            End With
            With grProductos.RootTable.Columns("grupo2")
                .Width = 100
                .Caption = "Grupo 2"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = True
            End With
            With grProductos.RootTable.Columns("grupo3")
                .Width = 100
                .Caption = "Grupo 3"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = True
            End With
            With grProductos.RootTable.Columns("grupo4")
                .Width = 100
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
            .Width = 80
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Caption = "UNIDAD MIN."
        End With
        With grProductos.RootTable.Columns("yhprecio")
            .Width = 120
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Caption = "PRECIO COSTO"
            .FormatString = "0.00"
        End With
        With grProductos.RootTable.Columns("venta")
            .Width = 120
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Caption = "PRECIO VENTA"
            .FormatString = "0.00"
        End With
        With grProductos.RootTable.Columns("stock")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Caption = "STOCK"
            .FormatString = "0.00"
        End With

        With grProductos
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
        End With
    End Sub
    Private Sub _prAddDetalleVenta()

        Dim Bin As New MemoryStream
        Dim img As New Bitmap(My.Resources.delete, 28, 28)
        img.Save(Bin, Imaging.ImageFormat.Png)
        CType(grdetalle.DataSource, DataTable).Rows.Add(_fnSiguienteNumi() + 1, 0, 0, "", "", 0, 0, 0, "",
                                                        0, "20500101", CDate("2050/01/01"), 0, 0, 0, 0, 0, 0, 0, 0, "", Now.Date, "", "", 0, Bin.GetBuffer, 0, 0)
    End Sub

    Public Function _fnSiguienteNumi()
        Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)
        Dim rows() As DataRow = dt.Select("cbnumi=MAX(cbnumi)")
        If (rows.Count > 0) Then
            Return rows(rows.Count - 1).Item("cbnumi")
        End If
        Return 1
    End Function
    Public Function _fnAccesible()
        Return tbFechaVenta.IsInputReadOnly = False
    End Function
    Private Sub _HabilitarProductos()
        'GPanelProductos.Height = 300
        GPanelProductos.Visible = True
        PanelTotal.Visible = False
        PanelInferior.Visible = False
        _prCargarProductos(73) ''''Aqui poner el Primer Precio de Costo
        grProductos.Focus()
        grProductos.MoveTo(grProductos.FilterRow)
        grProductos.Col = 2
        PanelDetalle.Height = 370
        'GPanelProductos.Height = 260
        'grProductos.Height = 260
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
            Dim _numi As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbnumi")
            If (_numi = numi) Then
                pos = i
                Return
            End If
        Next
    End Sub

    Public Function _fnExisteProducto(idprod As Integer) As Boolean
        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _idprod As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbty5prod")
            Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("estado")
            If (_idprod = idprod And estado >= 0) Then
                Return True
            End If
        Next
        Return False
    End Function
    Public Sub P_PonerTotal(rowIndex As Integer)
        If (rowIndex < grdetalle.RowCount) Then

            Dim lin As Integer = grdetalle.GetValue("cbnumi")
            Dim pos As Integer = -1
            _fnObtenerFilaDetalle(pos, lin)
            Dim cant As Double = grdetalle.GetValue("cbcmin")
            Dim uni As Double = grdetalle.GetValue("cbpcost")
            If (pos >= 0) Then
                Dim TotalUnitario As Double = cant * uni
                'grDetalle.SetValue("lcmdes", montodesc)

                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbptot") = TotalUnitario
                grdetalle.SetValue("cbptot", TotalUnitario)
                Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado")
                If (estado = 1) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = 2
                End If
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbprven") = (uni + (uni * (grdetalle.GetValue("cbutven") / 100))) * tbTipoCambio.Value
                grdetalle.SetValue("cbprven", (uni + (uni * (grdetalle.GetValue("cbutven") / 100))) * tbTipoCambio.Value)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("venta") = (uni + (uni * (grdetalle.GetValue("cbutven") / 100))) * tbTipoCambio.Value
                grdetalle.SetValue("cbprven", (uni + (uni * (grdetalle.GetValue("cbutven") / 100))) * tbTipoCambio.Value)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbdesc") = grdetalle.GetValue("cbdesc")
                grdetalle.SetValue("cbdesc", grdetalle.GetValue("cbdesc"))
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbdescpro") = grdetalle.GetValue("cbdescpro")
                grdetalle.SetValue("cbdescpro", grdetalle.GetValue("cbdescpro"))
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbice") = grdetalle.GetValue("cbice")
                grdetalle.SetValue("cbice", grdetalle.GetValue("cbice"))
                Dim SubTot As Double = TotalUnitario - grdetalle.GetValue("cbdesc") - grdetalle.GetValue("cbdescpro")
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbtotal") = SubTot + grdetalle.GetValue("cbice")
                grdetalle.SetValue("cbtotal", (SubTot + grdetalle.GetValue("cbice")))

                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpcostoun") = grdetalle.GetValue("cbtotal") / grdetalle.GetValue("cbcmin")
                grdetalle.SetValue("cbpcostoun", (grdetalle.GetValue("cbtotal") / grdetalle.GetValue("cbcmin")))
            End If
            _prCalcularPrecioTotal()
        End If
    End Sub

    Public Sub P_CalcularPrecioU(rowIndex As Integer)
        If (rowIndex < grdetalle.RowCount) Then

            Dim lin As Integer = grdetalle.GetValue("cbnumi")
            Dim pos As Integer = -1
            _fnObtenerFilaDetalle(pos, lin)
            Dim cant As Double = grdetalle.GetValue("cbcmin")

            If (pos >= 0) Then
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbptot") = grdetalle.GetValue("cbptot")
                grdetalle.SetValue("cbptot", grdetalle.GetValue("cbptot"))

                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpcost") = grdetalle.GetValue("cbptot") / grdetalle.GetValue("cbcmin")
                grdetalle.SetValue("cbpcost", grdetalle.GetValue("cbptot") / grdetalle.GetValue("cbcmin"))
                Dim uni As Double = grdetalle.GetValue("cbpcost")
                Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado")
                If (estado = 1) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = 2
                End If
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbprven") = (uni + (uni * (grdetalle.GetValue("cbutven") / 100))) * tbTipoCambio.Value
                grdetalle.SetValue("cbprven", (uni + (uni * (grdetalle.GetValue("cbutven") / 100))) * tbTipoCambio.Value)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("venta") = (uni + (uni * (grdetalle.GetValue("cbutven") / 100))) * tbTipoCambio.Value
                grdetalle.SetValue("cbprven", (uni + (uni * (grdetalle.GetValue("cbutven") / 100))) * tbTipoCambio.Value)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbdesc") = grdetalle.GetValue("cbdesc")
                grdetalle.SetValue("cbdesc", grdetalle.GetValue("cbdesc"))
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbdescpro") = grdetalle.GetValue("cbdescpro")
                grdetalle.SetValue("cbdescpro", grdetalle.GetValue("cbdescpro"))
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbice") = grdetalle.GetValue("cbice")
                grdetalle.SetValue("cbice", grdetalle.GetValue("cbice"))
                Dim SubTot As Double = grdetalle.GetValue("cbptot") - grdetalle.GetValue("cbdesc") - grdetalle.GetValue("cbdescpro")
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbtotal") = SubTot + grdetalle.GetValue("cbice")
                grdetalle.SetValue("cbtotal", (SubTot + grdetalle.GetValue("cbice")))

                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpcostoun") = grdetalle.GetValue("cbtotal") / grdetalle.GetValue("cbcmin")
                grdetalle.SetValue("cbpcostoun", (grdetalle.GetValue("cbtotal") / grdetalle.GetValue("cbcmin")))
            End If
            _prCalcularPrecioTotal()
        End If
    End Sub
    Public Sub _prCalcularPrecioTotal()
        Dim ret As Double
        tbMdesc.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("cbdesc"), AggregateFunction.Sum)
        tbDescPro.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("cbdescpro"), AggregateFunction.Sum)
        tbIce.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("cbice"), AggregateFunction.Sum)

        Dim montodesc As Double = tbMdesc.Value
        Dim pordesc As Double = ((montodesc * 100) / grdetalle.GetTotal(grdetalle.RootTable.Columns("cbptot"), AggregateFunction.Sum))
        tbPdesc.Value = pordesc
        tbtotal.Value = (grdetalle.GetTotal(grdetalle.RootTable.Columns("cbptot"), AggregateFunction.Sum) - montodesc - tbDescPro.Value) + tbIce.Value

        'Agregado para que Muestre el Subtotal de la compra
        tbSubtotalC.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("cbptot"), AggregateFunction.Sum)

        If swRetencion.Value = True Then
            ret = tbSubtotalC.Value * 0.08
            tbSubtotalC.Text = tbSubtotalC.Value - ret
            tbtotal.Text = tbSubtotalC.Text
        End If
    End Sub
    Public Sub _prEliminarFila()
        If (grdetalle.Row >= 0) Then
            If (grdetalle.RowCount >= 2) Then
                Dim estado As Integer = grdetalle.GetValue("estado")
                Dim pos As Integer = -1
                Dim lin As Integer = grdetalle.GetValue("cbnumi")
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
        If (_CodProveedor <= 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Seleccione un Proveedor con Ctrl+Enter".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            tbProveedor.Focus()
            Return False
        End If
        If (grdetalle.RowCount = 1) Then
            grdetalle.Row = grdetalle.RowCount - 1
            If (grdetalle.GetValue("cbty5prod") = 0) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "Por Favor Seleccione  un detalle de producto".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                Return False
            End If
        End If
        If (cbSucursal.SelectedIndex < 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Seleccione un Almacén".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            tbProveedor.Focus()
            Return False
        End If
        If swEmision.Value = True Then
            If (tbNitProv.Text = String.Empty Or tbNitProv.Text = "0") Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "Por favor debe llenar el Nit".ToUpper, img, 3000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                tbNitProv.Focus()
                Return False
            End If
            If (tbRazonSocial.Text = String.Empty) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "Por favor debe llenar la Razón Social".ToUpper, img, 3000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                tbRazonSocial.Focus()
                Return False
            End If

            If (tbNFactura.Text = String.Empty) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "Por favor debe llenar el ".ToUpper + lbNFactura.Text.ToUpper, img, 3000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                tbNFactura.Focus()
                Return False
            End If
            If (tbNAutorizacion.Text = String.Empty) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "Por Favor debe llenar el número de autorización".ToUpper, img, 3000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                tbNAutorizacion.Focus()
                Return False
            End If
            If (tbCodControl.Text = String.Empty) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "Por Favor debe llenar el código de control".ToUpper, img, 3000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                tbCodControl.Focus()
                Return False
            End If
        Else
            If (tbNFactura.Text = String.Empty) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "Por favor debe llenar el ".ToUpper + lbNFactura.Text.ToUpper, img, 3000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                tbNFactura.Focus()
                Return False
            End If
        End If
        If tbFechaVenta.Value > Now.Date Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "La fecha de compra no puede ser mayor a la fecha actual".ToUpper, img, 2800, eToastGlowColor.Red, eToastPosition.BottomCenter)
            tbFechaVenta.Focus()
            Return False
        End If

        ''Controla que no se metan un mismo producto con el mismo lote y fecha de vencimiento
        Dim dt1 As DataTable = CType(grdetalle.DataSource, DataTable)
        For i As Integer = 0 To grdetalle.RowCount - 1 Step 1
            Dim _idprod As String = CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbty5prod")
            Dim _Lote As String = CType(grdetalle.DataSource, DataTable).Rows(i).Item("cblote")
            Dim _Fecha As String = CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbfechavenc")
            Dim _estado As String = 0

            Dim query = dt1.Select("cbty5prod='" + _idprod + "' And cblote='" + _Lote + "' And cbfechavenc='" + _Fecha + "' And estado>='" + _estado + "'")

            If query.Count >= 2 Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "No puede registrar mas de un producto con el mismo lote y fecha de vencimiento, favor modificar".ToUpper, img, 4000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                Return False
            End If
        Next
        If tbSubtotalC.Value = 0 Or tbtotal.Value = 0 Or tbSACF.Text = "0" Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "El Subtotal, Total o Sujeto a Crédito fiscal no pueden ser 0 ".ToUpper, img, 2800, eToastGlowColor.Red, eToastPosition.BottomCenter)
            Return False
        End If

        If (swEmision.Value = False) Then
            Dim ef = New Efecto
            ef.tipo = 2
            ef.Context = "Mensaje Principal".ToUpper
            ef.Header = "¿esta seguro que ésta compra será con Recibo?".ToUpper
            ef.ShowDialog()
            Dim bandera As Boolean = False
            bandera = ef.band
            If (bandera = True) Then
                Return True
            Else
                Return False
            End If
        End If

        Return True
    End Function

    Public Sub _GuardarNuevo()
        Try
            RecuperarDatosTFC001()  'Recupera datos para grabar en la BDDiconDino en la Tabla TFC001

            ''Recorrido para detectar si se esta modificando registros antiguos y no los tome como cambio de precio para el control de verificar precio costo
            Dim dt As DataTable = L_fnArmarTablaProdDiferentesPrecioCosto(CType(grdetalle.DataSource, DataTable))
            Dim _dtActualizar As New DataTable
            _dtActualizar.Columns.Add("Prod")
            _dtActualizar.Columns.Add("Act")
            For i = 0 To dt.Rows.Count - 1
                Dim dtResult As DataTable = L_fnVerificarComprasUnProducto(dt.Rows(i).Item("cbty5prod"), tbFechaVenta.Value.ToString("yyyy/MM/dd"))
                If dtResult.Rows.Count > 0 Then
                    _dtActualizar.Rows.Add(dt.Rows(i).Item("cbty5prod"), 1)
                End If
            Next

            Dim res As Boolean = L_fnGrabarCompra("", cbSucursal.Value, tbFechaVenta.Value.ToString("yyyy/MM/dd"),
                                                  _CodProveedor, IIf(swTipoVenta.Value = True, 1, 0), IIf(swTipoVenta.Value = True,
                                                  Now.Date.ToString("yyyy/MM/dd"), tbFechaVenc.Value.ToString("yyyy/MM/dd")),
                                                  IIf(swMoneda.Value = True, 1, 0), tbObservacion.Text.Trim, tbSubtotalC.Value, tbMdesc.Value,
                                                  tbDescPro.Value, tbIce.Value, tbtotal.Value, CType(grdetalle.DataSource, DataTable),
                                                  _detalleCompras, IIf(swEmision.Value = True, 1, 0),
                                                  tbNFactura.Text, IIf(swConsigna.Value = True, 1, 0),
                                                  IIf(swRetencion.Value = True, 1, 0), IIf(swMoneda.Value = True, 1, tbTipoCambio.Value),
                                                  gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina, _dtActualizar)
            If res Then
                Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
                ToastNotification.Show(Me, "Código de Compra ".ToUpper + tbCodigo.Text + " Grabado con éxito.".ToUpper,
                                          img, 2000,
                                          eToastGlowColor.Green,
                                          eToastPosition.TopCenter
                                          )
                If Not (tbNitProv.Text = String.Empty Or tbNitProv.Text = "0") Then
                    L_Grabar_NitCompra(tbNitProv.Text.Trim, tbRazonSocial.Text.Trim, Convert.ToInt64(tbCodProv.Text))
                End If

                _prCargarCompra()
                _Limpiar()
            Else
                Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                ToastNotification.Show(Me, "La Compra no pudo ser insertado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub
    Public Sub RecuperarDatosTFC001()
        _detalleCompras = L_prCompraComprobanteGeneralPorNumi(-1)
        '_ValidarDatosFacturacion()
        Dim ffec As String
        Dim fnit As String
        Dim frsocial As String
        Dim fnro As String
        Dim fndui As String
        Dim fautoriz As String
        Dim fmonto As String
        Dim fmonto2 As String
        Dim fccont As String
        Dim sujetoCreditoFiscal As String
        Dim nosujetoCreditoFiscal As String
        Dim subTotal As String
        Dim subTotal2 As String
        Dim fdesc As String
        Dim importeBaseCreditoFiscal As String
        Dim creditoFiscal As String
        Dim importeBaseCreditoFiscal2 As String
        Dim creditoFiscal2 As String

        If swEmision.Value = True Then
            ffec = tbFechaVenta.Value.ToString("yyyy/MM/dd")
            fnit = tbNitProv.Text
            frsocial = tbRazonSocial.Text
            fnro = tbNFactura.Text
            If tbNDui.Text = String.Empty Then
                tbNDui.Text = 0
            End If
            fndui = tbNDui.Text
            fautoriz = tbNAutorizacion.Text
            'fmonto = tbtotal.Value.ToString + tbMdesc.Value
            fmonto = tbSubtotalC.Value
            fmonto2 = tbSubtotalC.Value + tbIce.Value
            'If tbSACF.Text = String.Empty Then
            '    tbSACF.Text = fmonto
            'End If
            sujetoCreditoFiscal = tbSACF.Text

            'If sujetoCreditoFiscal = String.Empty Then
            '    sujetoCreditoFiscal = fmonto
            'End If
            nosujetoCreditoFiscal = tbtotal.Value.ToString - sujetoCreditoFiscal
            subTotal = (fmonto + tbIce.Value) - nosujetoCreditoFiscal
            subTotal2 = (fmonto2 - tbIce.Value) - nosujetoCreditoFiscal
            'If tbMdesc.Value = String.Empty Then
            '    tbMdesc.Value = 0
            'End If

            fdesc = tbMdesc.Value + tbDescPro.Value
            'tbImporteBaseCreditoFiscal.Value = TbSubTotal.Value - TbdDescuento.Value
            importeBaseCreditoFiscal = subTotal - fdesc
            creditoFiscal = importeBaseCreditoFiscal * 0.13
            importeBaseCreditoFiscal2 = subTotal2 - fdesc
            creditoFiscal2 = importeBaseCreditoFiscal2 * 0.13
            fccont = tbCodControl.Text
            Dim numi As String = ""

            _detalleCompras.Rows.Add(1, ffec, fnit, frsocial, fnro, fndui, fautoriz, fmonto, fmonto2, tbIce.Value, 0, 0, 0, nosujetoCreditoFiscal, 0, 0, subTotal,
                                     subTotal2, fdesc, 0, importeBaseCreditoFiscal, creditoFiscal, importeBaseCreditoFiscal2, creditoFiscal2, "INTERNO/ACTIVIDADES GRAVADAS",
                                     fccont, "SI", "CONSOLIDADO", 0)

        Else
            'ffec = tbFechaVenta.Value.ToString("yyyy/MM/dd")
            'fnit = tbNitProv.Text
            'frsocial = tbProveedor.Text
            'fnro = tbNFactura.Text
            'fndui = 0
            'fautoriz = 0
            'fmonto = tbtotal.Value.ToString
            'sujetoCreditoFiscal = tbSACF.Text
            'nosujetoCreditoFiscal = 0
            'subTotal = fmonto
            'fdesc = tbMdesc.Value.ToString
            'importeBaseCreditoFiscal = fmonto - fdesc
            'creditoFiscal = 0
            'fccont = 0
            'Dim numi As String = ""

            '_detalleCompras.Rows.Add(1, ffec, fnit, frsocial, fnro, fndui, fautoriz, fmonto, nosujetoCreditoFiscal, subTotal, fdesc, importeBaseCreditoFiscal, creditoFiscal, fccont, 1, 0, 0)

        End If
    End Sub

    Private Sub _prGuardarModificado()
        RecuperarDatosTFC001()


        ''Recorrido para detectar si se esta modificando registros antiguos y no los tome como cambio de precio para el control de verificar precio costo
        Dim dt As DataTable = L_fnArmarTablaProdDiferentesPrecioCosto(CType(grdetalle.DataSource, DataTable))
        Dim _dtActualizar As New DataTable
        _dtActualizar.Columns.Add("Prod")
        _dtActualizar.Columns.Add("Act")
        For i = 0 To dt.Rows.Count - 1
            Dim dtResult As DataTable = L_fnVerificarComprasUnProducto(dt.Rows(i).Item("cbty5prod"), tbFechaVenta.Value.ToString("yyyy/MM/dd"))
            If dtResult.Rows.Count > 0 Then
                _dtActualizar.Rows.Add(dt.Rows(i).Item("cbty5prod"), 1)
            End If
        Next

        Dim res As Boolean = L_fnModificarCompra(tbCodigo.Text, cbSucursal.Value, tbFechaVenta.Value.ToString("yyyy/MM/dd"), _CodProveedor,
                                                 IIf(swTipoVenta.Value = True, 1, 0), IIf(swTipoVenta.Value = True, Now.Date.ToString("yyyy/MM/dd"),
                                                 tbFechaVenc.Value.ToString("yyyy/MM/dd")), IIf(swMoneda.Value = True, 1, 0), tbObservacion.Text.Trim,
                                                 tbSubtotalC.Value, tbMdesc.Value, tbDescPro.Value, tbIce.Value, tbtotal.Value,
                                                 CType(grdetalle.DataSource, DataTable), _detalleCompras, IIf(swEmision.Value = True, 1, 0),
                                                 tbNFactura.Text, IIf(swConsigna.Value = True, 1, 0), IIf(swRetencion.Value = True, 1, 0),
                                                 IIf(swMoneda.Value = True, 1, tbTipoCambio.Value), gs_VersionSistema, gs_IPMaquina,
                                                 gs_UsuMaquina, _dtActualizar)
        If res Then

            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "Código de Compra ".ToUpper + tbCodigo.Text + " Modificado con Exito.".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter
                                      )

            If Not (tbNitProv.Text = String.Empty Or tbNitProv.Text = "0") Then
                L_Grabar_NitCompra(tbNitProv.Text.Trim, tbRazonSocial.Text.Trim, Convert.ToInt64(tbCodProv.Text))
            End If

            _prCargarCompra()
            _prSalir()
        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "La Compra no pudo ser Modificada".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        End If
    End Sub
    Private Sub _prSalir()
        If btnGrabar.Enabled = True Then
            _prInhabiliitar()
            If grCompra.RowCount > 0 Then
                _prMostrarRegistro(0)
            End If
        Else
            Me.Close()
            _modulo.Select()
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
    Public Sub _prActualizarAlgunosCampos()
        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            CType(grdetalle.DataSource, DataTable).Rows(i).Item("estado") = 0
        Next
    End Sub
    Public Sub _PrimerRegistro()
        Dim _MPos As Integer
        If grCompra.RowCount > 0 Then
            _MPos = 0
            ''   _prMostrarRegistro(_MPos)
            grCompra.Row = _MPos
        End If
    End Sub

    Private Sub P_GenerarReporteCompra()
        Dim dt As DataTable = L_fnNotaCompras(tbCodigo.Text)
        'Dim dt2 = L_DatosEmpresa("1")
        Dim _TotalLi As Decimal
        Dim _Literal, _TotalDecimal, _TotalDecimal2, moneda As String

        'Literal 
        _TotalLi = dt.Rows(0).Item("total")
        _TotalDecimal = _TotalLi - Math.Truncate(_TotalLi)
        _TotalDecimal2 = CDbl(_TotalDecimal) * 100

        If swMoneda.Value = True Then
            moneda = "Bolivianos"
        Else
            moneda = "Dólares"
        End If

        _Literal = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(_TotalLi) - CDbl(_TotalDecimal)) + "  " + IIf(_TotalDecimal2.Equals("0"), "00", _TotalDecimal2) + "/100 " + moneda

        If Not IsNothing(P_Global.Visualizador) Then
            P_Global.Visualizador.Close()
        End If

        P_Global.Visualizador = New Visualizador

        Dim objrep As New R_NotaCompra
        objrep.SetDataSource(dt)
        objrep.SetParameterValue("Literal", _Literal)
        objrep.SetParameterValue("Emision", IIf(swEmision.Value = True, "NRO. FACTURA:", "NRO. DOCUMENTO:"))
        objrep.SetParameterValue("Nit", tbNitProv.Text)
        objrep.SetParameterValue("RazonSocial", tbRazonSocial.Text)

        P_Global.Visualizador.CrGeneral.ReportSource = objrep 'Comentar
        P_Global.Visualizador.ShowDialog() 'Comentar
        P_Global.Visualizador.BringToFront()

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

        With grdetalle
            If tbObservacion.ReadOnly = True Then
                .DefaultFilterRowComparison = FilterConditionOperator.Contains
                .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
                .FilterMode = FilterMode.Automatic
            Else
                .DefaultFilterRowComparison = False
                .FilterRowUpdateMode = False
                .FilterMode = False
            End If
        End With
    End Sub
    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _prSalir()
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
    Private Sub tbProveedor_KeyDown(sender As Object, e As KeyEventArgs) Handles tbProveedor.KeyDown
        Try
            If (_fnAccesible()) Then
                If e.KeyData = Keys.Control + Keys.Enter Then

                    Dim dt As DataTable
                    dt = L_fnListarProveedores()
                    'dt = L_fnListarProveedoresNueva()

                    If dt.Rows.Count = 0 Then
                        Throw New Exception("Lista de proveedores vacia")
                    End If
                    Dim listEstCeldas As New List(Of Modelo.Celda)

                    listEstCeldas.Add(New Modelo.Celda("ydnumi,", True, "COD SISTEMA", 90))
                    listEstCeldas.Add(New Modelo.Celda("ydcod", True, "COD PROV.", 90))
                    listEstCeldas.Add(New Modelo.Celda("yddesc", True, "NOMBRE", 280))
                    listEstCeldas.Add(New Modelo.Celda("yddctnum", True, "N. Documento".ToUpper, 150))
                    listEstCeldas.Add(New Modelo.Celda("yddirec", True, "DIRECCION", 220))
                    listEstCeldas.Add(New Modelo.Celda("ydtelf1", True, "Telefono".ToUpper, 200))
                    listEstCeldas.Add(New Modelo.Celda("ydfnac", False, "F.Nacimiento".ToUpper, 150, "MM/dd,YYYY"))
                    listEstCeldas.Add(New Modelo.Celda("ydobs", True, "OBSERVACIÓN".ToUpper, 200))

                    Dim ef = New Efecto
                    ef.tipo = 3
                    ef.dt = dt
                    ef.SeleclCol = 2
                    ef.listEstCeldas = listEstCeldas
                    ef.alto = 50
                    ef.ancho = 250
                    ef.Context = "Seleccione Proveedor".ToUpper
                    ef.ShowDialog()
                    Dim bandera As Boolean = False
                    bandera = ef.band
                    If (bandera = True) Then
                        Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row

                        _CodProveedor = Row.Cells("ydnumi").Value
                        tbProveedor.Text = Row.Cells("yddesc").Value
                        tbCodProv.Text = Row.Cells("ydnumi").Text
                        tbNitProv.Text = Row.Cells("yddctnum").Value
                        tbNitProv.Focus()

                        '_CodProveedor = Row.Cells("yccod3").Value
                        'tbProveedor.Text = Row.Cells("ycdes3").Value
                        'tbCodProv.Text = Row.Cells("yccod3").Text
                        'tbNitProv.Focus()
                    End If
                End If
            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub

    Private Sub swTipoVenta_ValueChanged(sender As Object, e As EventArgs) Handles swTipoVenta.ValueChanged
        If (swTipoVenta.Value = False) Then
            lbCredito.Visible = True
            tbFechaVenc.Visible = True
            tbFechaVenc.Value = Now.Date
        Else
            lbCredito.Visible = False
            tbFechaVenc.Visible = False
        End If
    End Sub

    Private Sub grdetalle_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grdetalle.EditingCell
        If (_fnAccesible()) Then

            If (_estadoPor = 0) Then
                If (e.Column.Index = grdetalle.RootTable.Columns("cbcmin").Index Or e.Column.Index = grdetalle.RootTable.Columns("cbpcost").Index Or
                    e.Column.Index = grdetalle.RootTable.Columns("cbptot").Index Or e.Column.Index = grdetalle.RootTable.Columns("cbdesc").Index Or
                    e.Column.Index = grdetalle.RootTable.Columns("cbice").Index) Then
                    e.Cancel = False
                Else
                    e.Cancel = True
                End If
            Else
                If (e.Column.Index = grdetalle.RootTable.Columns("cbcmin").Index Or e.Column.Index = grdetalle.RootTable.Columns("cbpcost").Index Or
                    e.Column.Index = grdetalle.RootTable.Columns("cbptot").Index Or e.Column.Index = grdetalle.RootTable.Columns("cbutven").Index Or
                    e.Column.Index = grdetalle.RootTable.Columns("cbprven").Index Or e.Column.Index = grdetalle.RootTable.Columns("cblote").Index Or
                    e.Column.Index = grdetalle.RootTable.Columns("cbfechavenc").Index Or e.Column.Index = grdetalle.RootTable.Columns("cbdesc").Index Or
                    e.Column.Index = grdetalle.RootTable.Columns("cbice").Index) Then
                    e.Cancel = False
                Else
                    e.Cancel = True
                End If
            End If
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub grdetalle_Enter(sender As Object, e As EventArgs) Handles grdetalle.Enter
        If (_fnAccesible()) Then
            If (_CodProveedor <= 0) Then
                ToastNotification.Show(Me, "           Antes de Continuar Por favor Seleccione un Proveedor!!             ", My.Resources.WARNING, 4000, eToastGlowColor.Red, eToastPosition.TopCenter)
                tbProveedor.Focus()
                Return
            End If
            If (tbTipoCambio.Value <= 0) Then
                ToastNotification.Show(Me, "           Antes de continuar por favor introduzca Tipo de Cambio mayor a 0!!             ", My.Resources.WARNING, 4000, eToastGlowColor.Red, eToastPosition.TopCenter)
                tbProveedor.Focus()
                Return
            End If
            'grdetalle.Select()
            'grdetalle.Col = 2
            'grdetalle.Row = 0
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

            If (grdetalle.Col = grdetalle.RootTable.Columns("cbcmin").Index) Then
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
                Dim pos As Integer = -1
                'grdetalle.Row = grdetalle.RowCount - 1
                grdetalle.Row = grdetalle.Row
                _fnObtenerFilaDetalle(pos, grdetalle.GetValue("cbnumi"))
                Dim existe As Boolean = _fnExisteProducto(grProductos.GetValue("yfnumi"))
                If ((pos >= 0) And (Not existe)) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbty5prod") = grProductos.GetValue("yfnumi")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("yfcprod") = grProductos.GetValue("yfcprod")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("producto") = grProductos.GetValue("yfcdprod1")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbumin") = grProductos.GetValue("yfumin")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("unidad") = grProductos.GetValue("UnidMin")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpcost") = grProductos.GetValue("yhprecio")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbptot") = grProductos.GetValue("yhprecio")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbcmin") = 1
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbtotal") = grProductos.GetValue("yhprecio")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpcostoun") = grProductos.GetValue("yhprecio")

                    Dim PrecioVenta As Double = IIf(IsDBNull(grProductos.GetValue("venta")), 0, grProductos.GetValue("venta"))
                    If (PrecioVenta > 0) Then
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbprven") = PrecioVenta
                        Dim montodesc As Double = PrecioVenta - grProductos.GetValue("yhprecio")
                        Dim precio As Integer = IIf(IsDBNull(grProductos.GetValue("yhprecio")), 0, grProductos.GetValue("yhprecio"))
                        If (precio = 0) Then
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbutven") = 100
                        Else
                            Dim pordesc As Double = ((montodesc * 100) / grProductos.GetValue("yhprecio"))
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbutven") = pordesc
                        End If
                    Else
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbutven") = _PorcentajeUtil
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbprven") = (grProductos.GetValue("yhprecio") + ((grProductos.GetValue("yhprecio")) * (_PorcentajeUtil / 100)))
                    End If

                    _prCalcularPrecioTotal()
                    PanelDetalle.Height = 250
                    _DesHabilitarProductos()
                Else
                    If (existe) Then
                        Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                        ToastNotification.Show(Me, "El producto ya existe en el detalle, modifique su cantidad".ToUpper, img, 2200, eToastGlowColor.Red, eToastPosition.BottomCenter)
                    End If
                End If
            End If
        End If
        If e.KeyData = Keys.Escape Then
            _DesHabilitarProductos()
        End If
    End Sub
    Private Sub grdetalle_CellValueChanged(sender As Object, e As ColumnActionEventArgs) Handles grdetalle.CellValueChanged
        If tbTipoCambio.Value > 0 Then
            Dim lin As Integer = grdetalle.GetValue("cbnumi")
            Dim pos As Integer = -1
            _fnObtenerFilaDetalle(pos, lin)
            If (e.Column.Index = grdetalle.RootTable.Columns("cbcmin").Index) Then
                If (Not IsNumeric(grdetalle.GetValue("cbcmin")) Or grdetalle.GetValue("cbcmin").ToString = String.Empty) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbcmin") = 1
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbptot") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpcost")
                Else
                    If (grdetalle.GetValue("cbcmin") > 0) Then
                        Dim rowIndex As Integer = grdetalle.Row
                        P_PonerTotal(rowIndex)
                    Else
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbcmin") = 1
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbptot") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpcost")
                        _prCalcularPrecioTotal()
                    End If
                End If
            End If

            ''''''''''''''''''''''PRECIO UNITARIO  ''''''''''''''''''''''''''''''''''''''''''
            If (e.Column.Index = grdetalle.RootTable.Columns("cbpcost").Index) Then
                If (Not IsNumeric(grdetalle.GetValue("cbpcost")) Or grdetalle.GetValue("cbpcost").ToString = String.Empty) Then
                    Dim cantidad As Double = grdetalle.GetValue("cbcmin")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpcost") = 0
                    grdetalle.SetValue("cbpcost", 0)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbptot") = cantidad * CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpcost")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbprven") = _PorcentajeUtil * CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpcost")
                Else
                    If (grdetalle.GetValue("cbpcost") >= 0) Then
                        Dim rowIndex As Integer = grdetalle.Row
                        P_PonerTotal(rowIndex)
                    Else
                        Dim cantidad As Double = grdetalle.GetValue("cbcmin")
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpcost") = 0
                        grdetalle.SetValue("cbpcost", 0)
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbptot") = cantidad * CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpcost")
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbprven") = _PorcentajeUtil * CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpcost")
                    End If
                End If
            End If

            ''''''''''''''''''''''SUBTOTAL  ''''''''''''''''''''''''''''''''''''''''''
            If (e.Column.Index = grdetalle.RootTable.Columns("cbptot").Index) Then
                If (Not IsNumeric(grdetalle.GetValue("cbptot")) Or grdetalle.GetValue("cbptot").ToString = String.Empty) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbptot") = 0
                    grdetalle.SetValue("cbptot", 0)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbtotal") = (CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbptot") - CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbdesc") - CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbdescpro")) + CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbice")
                    grdetalle.SetValue("cbtotal", (CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbptot") - CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbdesc") - CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbdescpro")) + CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbice"))
                Else
                    If (grdetalle.GetValue("cbptot") > 0) Then
                        Dim rowIndex As Integer = grdetalle.Row
                        P_CalcularPrecioU(rowIndex)
                    Else
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbptot") = 0
                        grdetalle.SetValue("cbptot", 0)
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbtotal") = (CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbptot") - CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbdesc") - CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbdescpro")) + CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbice")
                        grdetalle.SetValue("cbtotal", (CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbptot") - CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbdesc") - CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbdescpro")) + CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbice"))

                    End If
                End If
            End If

            ''''''''''''''''''''''DESCUENTO UNITARIO ''''''''''''''''''''''''''''''''''''''''''
            If (e.Column.Index = grdetalle.RootTable.Columns("cbdesc").Index) Then
                If (Not IsNumeric(grdetalle.GetValue("cbdesc")) Or grdetalle.GetValue("cbdesc").ToString = String.Empty) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbdesc") = 0
                    grdetalle.SetValue("cbdesc", 0)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbtotal") = (CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbptot") - CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbdescpro")) + CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbice")
                    grdetalle.SetValue("cbtotal", (CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbptot") - CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbdescpro")) + CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbice"))
                Else
                    If (grdetalle.GetValue("cbdesc") >= 0 And grdetalle.GetValue("cbdesc") <= grdetalle.GetValue("cbptot")) Then
                        Dim rowIndex As Integer = grdetalle.Row
                        P_PonerTotal(rowIndex)
                    Else
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbdesc") = 0
                        grdetalle.SetValue("cbdesc", 0)
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbtotal") = (CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbptot") - CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbdescpro")) + CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbice")
                        grdetalle.SetValue("cbtotal", (CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbptot") - CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbdescpro")) + CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbice"))

                        _prCalcularPrecioTotal()
                    End If
                End If
            End If

            ''''''''''''''''''''''ICE ''''''''''''''''''''''''''''''''''''''''''
            If (e.Column.Index = grdetalle.RootTable.Columns("cbice").Index) Then
                If (Not IsNumeric(grdetalle.GetValue("cbice")) Or grdetalle.GetValue("cbice").ToString = String.Empty) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbice") = 0
                    grdetalle.SetValue("cbice", 0)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbtotal") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbptot") - CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbdesc") - CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbdescpro")
                    grdetalle.SetValue("cbtotal", CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbptot") - CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbdesc") - CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbdescpro"))

                Else
                    If (grdetalle.GetValue("cbice") >= 0 And grdetalle.GetValue("cbice") <= grdetalle.GetValue("cbptot")) Then
                        Dim rowIndex As Integer = grdetalle.Row
                        P_PonerTotal(rowIndex)
                    Else
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbice") = 0
                        grdetalle.SetValue("cbice", 0)
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbtotal") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbptot") - CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbdesc") - CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbdescpro")
                        grdetalle.SetValue("cbtotal", CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbptot") - CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbdesc") - CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbdescpro"))

                        _prCalcularPrecioTotal()
                    End If
                End If
            End If

            ''''''''''''''''''PRECIO VENTA '''''''''   CONTINUARA  '''''''''''''
            'Habilitar solo las columnas de Precio, %, Monto y Observación
            If (e.Column.Index = grdetalle.RootTable.Columns("cbprven").Index) Then
                If (Not IsNumeric(grdetalle.GetValue("cbprven")) Or grdetalle.GetValue("cbprven").ToString = String.Empty) Then

                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbprven") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("venta")
                    Dim montodesc As Double = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("venta") - grdetalle.GetValue("cbpcost")
                    Dim pordesc As Double = ((montodesc * 100) / CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpcost"))
                Else
                    If (grdetalle.GetValue("cbprven") > 0) Then
                        'Dim montodesc As Double = grdetalle.GetValue("cbprven") - CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpcost")
                        'Dim pordesc As Double = ((montodesc * 100) / CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpcost"))
                        'grdetalle.SetValue("cbutven", pordesc)

                        Dim montodesc As Double = grdetalle.GetValue("cbprven") - (grdetalle.GetValue("cbpcost") * tbTipoCambio.Value)
                        Dim pordesc As Double = ((montodesc * 100) / (grdetalle.GetValue("cbpcost") * tbTipoCambio.Value))
                        grdetalle.SetValue("cbutven", pordesc)
                    Else
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbprven") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("venta")
                        Dim montodesc As Double = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("venta") - CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpcost")
                        Dim pordesc As Double = ((montodesc * 100) / CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpcost"))
                    End If
                End If
            End If

            ''''''''''''''''''PORCENTAJE PRECIO VENTA '''''''''   CONTINUARA  '''''''''''''
            'Habilitar solo las columnas de Precio, %, Monto y Observación

            If (e.Column.Index = grdetalle.RootTable.Columns("cbutven").Index) Then
                Dim venta As Double = IIf(IsDBNull(CType(grdetalle.DataSource, DataTable).Rows(pos).Item("venta")), 0, CType(grdetalle.DataSource, DataTable).Rows(pos).Item("venta"))
                Dim PrecioCosto As Double = IIf(IsDBNull(grdetalle.GetValue("cbpcost")), 0, (grdetalle.GetValue("cbpcost") * tbTipoCambio.Value))
                If (Not IsNumeric(grdetalle.GetValue("cbutven")) Or grdetalle.GetValue("cbutven").ToString = String.Empty) Then

                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbprven") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("venta")
                    Dim montodesc As Double = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("venta") - grdetalle.GetValue("cbpcost")
                    Dim pordesc As Double = ((montodesc * 100) / CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpcost"))
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbutven") = pordesc
                Else
                    If (grdetalle.GetValue("cbutven") > 0) Then
                        Dim porcentaje As Double = grdetalle.GetValue("cbutven")
                        Dim monto As Double = ((grdetalle.GetValue("cbpcost") * tbTipoCambio.Value) * (porcentaje / 100))
                        Dim precioventa As Double = monto + (grdetalle.GetValue("cbpcost") * tbTipoCambio.Value)
                        grdetalle.SetValue("cbprven", precioventa)
                    Else
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbprven") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("venta")
                        Dim montodesc As Double = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("venta") - CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpcost")
                        Dim pordesc As Double = ((montodesc * 100) / CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpcost"))
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbutven") = pordesc
                    End If
                End If
            End If
            Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado")
            If (estado = 1) Then
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = 2
            End If
        Else
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "El tipo de cambio debe ser mayor a 0".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
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
                    Dim montodesc As Double = (grdetalle.GetTotal(grdetalle.RootTable.Columns("cbptot"), AggregateFunction.Sum) * (porcdesc / 100))
                    tbMdesc.Value = montodesc
                    tbtotal.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("cbptot"), AggregateFunction.Sum) - montodesc
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
                    Dim pordesc As Double = ((montodesc * 100) / grdetalle.GetTotal(grdetalle.RootTable.Columns("cbptot"), AggregateFunction.Sum))
                    tbPdesc.Value = pordesc
                    tbtotal.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("cbptot"), AggregateFunction.Sum) - montodesc

                End If

            End If

            If (tbMdesc.Text = String.Empty) Then
                tbMdesc.Value = 0

            End If
        End If

    End Sub


    Private Sub grdetalle_CellEdited(sender As Object, e As ColumnActionEventArgs) Handles grdetalle.CellEdited
        If (e.Column.Index = grdetalle.RootTable.Columns("cbcmin").Index) Then
            If (Not IsNumeric(grdetalle.GetValue("cbcmin")) Or grdetalle.GetValue("cbcmin").ToString = String.Empty) Then


                grdetalle.SetValue("cbcmin", 1)
                grdetalle.SetValue("cbptot", grdetalle.GetValue("cbpcost"))
            Else
                If (grdetalle.GetValue("cbcmin") > 0) Then


                Else

                    grdetalle.SetValue("cbcmin", 1)
                    grdetalle.SetValue("cbptot", grdetalle.GetValue("cbpcost"))

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
        If (swTipoVenta.Value = False) Then
            Dim res1 As Boolean = L_fnVerificarPagosCompras(tbCodigo.Text)
            If res1 Then
                Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)
                ToastNotification.Show(Me, "No se puede modificar la Compra con código ".ToUpper + tbCodigo.Text + ", porque tiene pagos realizados, primero elimine los pagos correspondientes a esta compra".ToUpper,
                                          img, 6000,
                                          eToastGlowColor.Green,
                                          eToastPosition.TopCenter)

                Exit Sub
            End If
        End If

        Dim res As Boolean = L_fnVerificarSiSeContabilizo(tbCodigo.Text)
        If res Then
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "La Compra no puede ser Modificada porque ya fue contabilizada".ToUpper, img, 3500, eToastGlowColor.Red, eToastPosition.TopCenter)
        Else
            If (grCompra.RowCount > 0) Then
                _prhabilitar()
                btnNuevo.Enabled = False
                btnModificar.Enabled = False
                btnEliminar.Enabled = False
                btnGrabar.Enabled = True

                PanelNavegacion.Enabled = False
                _prCargarIconELiminar()


                With grdetalle
                    If tbObservacion.ReadOnly = True Then
                        .DefaultFilterRowComparison = FilterConditionOperator.Contains
                        .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
                        .FilterMode = FilterMode.Automatic
                    Else
                        .DefaultFilterRowComparison = False
                        .FilterRowUpdateMode = False
                        .FilterMode = False
                    End If
                End With
            End If
        End If
    End Sub
    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        'Dim result As Boolean = L_fnVerificarSiSeContabilizo(tbCodigo.Text)
        'If result Then
        '    Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
        '    ToastNotification.Show(Me, "La Compra no puede ser Eliminada porque ya fue contabilizada".ToUpper, img, 3500, eToastGlowColor.Red, eToastPosition.TopCenter)
        'Else
        '    Dim ef = New Efecto
        '    ef.tipo = 2
        '    ef.Context = "¿esta seguro de eliminar el registro?".ToUpper
        '    ef.Header = "mensaje principal".ToUpper
        '    ef.ShowDialog()
        '    Dim bandera As Boolean = False
        '    bandera = ef.band
        '    If (bandera = True) Then
        '        Dim mensajeError As String = ""
        '        Dim res As Boolean = L_fnEliminarCompra(tbCodigo.Text, mensajeError)
        '        If res Then

        '            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
        '            ToastNotification.Show(Me, "Código de Compra ".ToUpper + tbCodigo.Text + " eliminado con Exito.".ToUpper,
        '                                      img, 2000,
        '                                      eToastGlowColor.Green,
        '                                      eToastPosition.TopCenter)

        '            _prFiltrar()

        '        Else
        '            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
        '            ToastNotification.Show(Me, mensajeError, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        '        End If
        '    End If
        'End If
        If (swTipoVenta.Value = False) Then
            Dim res1 As Boolean = L_fnVerificarPagosCompras(tbCodigo.Text)
            If res1 Then
                Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)
                ToastNotification.Show(Me, "No se puede eliminar la Compra con código ".ToUpper + tbCodigo.Text + ", porque tiene pagos realizados, por favor primero elimine los pagos correspondientes a esta compra".ToUpper,
                                          img, 5000,
                                          eToastGlowColor.Green,
                                          eToastPosition.TopCenter)

                Exit Sub
            End If
        End If


        Dim result As Boolean = L_fnVerificarSiSeContabilizo(tbCodigo.Text)
        If result Then
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "La Compra no puede ser Eliminada porque ya fue contabilizada".ToUpper, img, 4500, eToastGlowColor.Red, eToastPosition.TopCenter)
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
            Dim res As Boolean = L_fnEliminarCompra(tbCodigo.Text, mensajeError, gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina)
            If res Then

                Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
                ToastNotification.Show(Me, "Código de Compra ".ToUpper + tbCodigo.Text + " eliminado con Exito.".ToUpper,
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

    Private Sub grVentas_SelectionChanged(sender As Object, e As EventArgs) Handles grCompra.SelectionChanged
        If (grCompra.RowCount > 0 And grCompra.Row >= 0) Then
            _prMostrarRegistro(grCompra.Row)
        End If


    End Sub

    Private Sub btnSiguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click
        Dim _pos As Integer = grCompra.Row
        If _pos < grCompra.RowCount - 1 And _pos >= 0 Then
            _pos = grCompra.Row + 1
            '' _prMostrarRegistro(_pos)
            grCompra.Row = _pos
        End If
    End Sub

    Private Sub btnUltimo_Click(sender As Object, e As EventArgs) Handles btnUltimo.Click
        Dim _pos As Integer = grCompra.Row
        If grCompra.RowCount > 0 Then
            _pos = grCompra.RowCount - 1
            ''  _prMostrarRegistro(_pos)
            grCompra.Row = _pos
        End If
    End Sub

    Private Sub btnAnterior_Click(sender As Object, e As EventArgs) Handles btnAnterior.Click
        Dim _MPos As Integer = grCompra.Row
        If _MPos > 0 And grCompra.RowCount > 0 Then
            _MPos = _MPos - 1
            ''  _prMostrarRegistro(_MPos)
            grCompra.Row = _MPos
        End If
    End Sub

    Private Sub btnPrimero_Click(sender As Object, e As EventArgs) Handles btnPrimero.Click
        _PrimerRegistro()
    End Sub
    Private Sub grVentas_KeyDown(sender As Object, e As KeyEventArgs) Handles grCompra.KeyDown
        If e.KeyData = Keys.Enter Then
            MSuperTabControl.SelectedTabIndex = 0
            grdetalle.Focus()

        End If
    End Sub


    Private Sub cbSucursal_KeyDown(sender As Object, e As KeyEventArgs) Handles cbSucursal.KeyDown
        If (_fnAccesible()) Then
            If e.KeyData = Keys.Enter Then
                grdetalle.Focus()
                'grdetalle.Select()
                'grdetalle.Col = 3
                'grdetalle.Row = 0
            End If
        End If

    End Sub

    Private Sub tbtotal_ValueChanged(sender As Object, e As EventArgs) Handles tbtotal.ValueChanged
        tbSACF.Text = tbtotal.Text
    End Sub

    Private Sub swEmision_ValueChanged(sender As Object, e As EventArgs) Handles swEmision.ValueChanged
        If swEmision.Value = False Then
            lbNFactura.Text = "Nro. Recibo:"
            GroupPanelFactura2.Text = "DATOS RECIBO"
            lbNAutoriz.Visible = False
            tbNAutorizacion.Visible = False
            lbCodCtrl.Visible = False
            tbCodControl.Visible = False
            lbNDui.Visible = False
            tbNDui.Visible = False
            lbSACF.Visible = False
            tbSACF.Visible = False
        Else
            lbNFactura.Text = "Nro. Factura:"
            GroupPanelFactura2.Text = "DATOS FACTURACIÓN"
            lbNAutoriz.Visible = True
            tbNAutorizacion.Visible = True
            lbCodCtrl.Visible = True
            tbCodControl.Visible = True
            lbNDui.Visible = True
            tbNDui.Visible = True
            lbSACF.Visible = True
            tbSACF.Visible = True

        End If
    End Sub

    Private Sub tbNFactura_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbNFactura.KeyPress
        g_prValidarTextBox(1, e)
    End Sub



    Private Sub tbNDui_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbNDui.KeyPress
        g_prValidarTextBox(1, e)
    End Sub

    Private Sub tbSACF_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbSACF.KeyPress
        g_prValidarTextBox(1, e)
    End Sub

    Private Sub swRetencion_ValueChanged(sender As Object, e As EventArgs) Handles swRetencion.ValueChanged
        If swRetencion.Value = False Or swRetencion.Value = True Then
            _prCalcularPrecioTotal()
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

    Private Sub swMoneda_ValueChanged(sender As Object, e As EventArgs) Handles swMoneda.ValueChanged
        If swMoneda.Value = True Then
            lbTipoCambio.Visible = False
            tbTipoCambio.Visible = False
            tbTipoCambio.Value = 1
        Else
            lbTipoCambio.Visible = True
            tbTipoCambio.Visible = True
            tbTipoCambio.Value = 0
        End If
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        P_GenerarReporteCompra()
        L_fnImpresionCompra(gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina, tbCodigo.Text)
    End Sub

    Private Sub cbSucursal_ValueChanged(sender As Object, e As EventArgs) Handles cbSucursal.ValueChanged
        If (_fnAccesible() And tbCodigo.Text = String.Empty) Then
            CType(grdetalle.DataSource, DataTable).Rows.Clear()
            _prAddDetalleVenta()
            _DesHabilitarProductos()
        End If
    End Sub

    Private Sub INSERTARFILAToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles INSERTARFILAToolStripMenuItem.Click
        _prAddFilaDetalle()
    End Sub
    Private Sub _prAddFilaDetalle()

        If grdetalle.Row >= 0 Then
            Dim Bin As New MemoryStream
            Dim img As New Bitmap(My.Resources.delete, 28, 28)
            img.Save(Bin, Imaging.ImageFormat.Png)

            Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)
            Dim fila As DataRow = dt.NewRow
            fila.Item("cbnumi") = _fnSiguienteNumi() + 1
            fila.Item("cbtv1numi") = 0
            fila.Item("cbty5prod") = 0
            fila.Item("yfcprod") = ""
            fila.Item("producto") = ""
            fila.Item("cbest") = 0
            fila.Item("cbcmin") = 0
            fila.Item("cbumin") = 0
            fila.Item("unidad") = ""
            fila.Item("cbpcost") = 0
            fila.Item("cblote") = "20500101"
            fila.Item("cbfechavenc") = CDate("2050/01/01")
            fila.Item("cbutven") = 0
            fila.Item("cbprven") = 0
            fila.Item("cbptot") = 0
            fila.Item("cbdesc") = 0
            fila.Item("cbdescpro") = 0
            fila.Item("cbice") = 0
            fila.Item("cbtotal") = 0
            fila.Item("cbpcostoun") = 0
            fila.Item("cbobs") = ""
            fila.Item("cbfact") = Now.Date
            fila.Item("cbhact") = ""
            fila.Item("cbuact") = ""
            fila.Item("estado") = 0
            fila.Item("img") = Bin.GetBuffer
            fila.Item("costo") = 0
            fila.Item("venta") = 0


            dt.Rows.InsertAt(fila, grdetalle.Row)


            grdetalle.MovePrevious()
            _HabilitarProductos()

        End If


        'CType(grdetalle.DataSource, DataTable).Rows.Add(_fnSiguienteNumi() + 1, 0, 0, "", "", 0, 0, 0, "",
        '0, "20500101", CDate("2050/01/01"), 0, 0, 0, 0, 0, 0, 0, 0, "", Now.Date, "", "", 0, Bin.GetBuffer, 0, 0)
        'grdetalle.MovePrevious()
    End Sub

    Private Sub tbDescPro_ValueChanged(sender As Object, e As EventArgs) Handles tbDescPro.ValueChanged
        Dim descuento As Double = tbDescPro.Value
        Dim TotalBruto As Double = tbSubtotalC.Value
        If (descuento >= 0) Then
            CalcularDescuento01(TotalBruto, descuento)
        End If
    End Sub

    Public Sub CalcularDescuento01(TotalBruto As Double, descuento As Double)

        Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)

        Dim Cantidad As Double
        Dim Descuento01 As Double
        Dim ImporteBruto As Double
        Dim CostoTotal As Double
        Dim PrecioCosto As Double

        If (dt.Rows.Count > 0 And tbObservacion.ReadOnly = False) Then
            For i As Integer = 0 To dt.Rows.Count - 1 Step 1
                Cantidad = dt.Rows(i).Item("cbcmin")
                ImporteBruto = dt.Rows(i).Item("cbptot")
                If (ImporteBruto > 0 And Cantidad > 0 And TotalBruto > 0) Then
                    Descuento01 = ((ImporteBruto * descuento) / TotalBruto)
                    CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbdescpro") = Descuento01


                    '''''''Aqui Calculo el descuento
                    CostoTotal = ((dt.Rows(i).Item("cbptot") - (dt.Rows(i).Item("cbdesc") + dt.Rows(i).Item("cbdescpro"))) + dt.Rows(i).Item("cbice"))
                    PrecioCosto = CostoTotal / dt.Rows(i).Item("cbcmin")

                    If (PrecioCosto > 0 And Cantidad > 0) Then
                        CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbtotal") = CostoTotal
                        CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbpcostoun") = PrecioCosto
                    End If
                    '''''''''
                End If
            Next

            tbtotal.Value = (tbSubtotalC.Value - (tbMdesc.Value + tbDescPro.Value)) + tbIce.Value

        End If
    End Sub

    Private Sub tbIce_ValueChanged(sender As Object, e As EventArgs) Handles tbIce.ValueChanged
        If (tbIce.Focused) Then
            Dim ice As Double = tbIce.Value
            Dim TotalBruto As Double = tbSubtotalC.Value
            If (ice >= 0) Then
                CalcularIcePro(TotalBruto, ice)
            End If
        Else
            'Dim ice As Double = tbIce.Value
            'Dim TotalBruto As Double = tbSubtotalC.Value
            'If (ice >= 0) Then
            '    CalcularIcePro(TotalBruto, ice)
            'End If
        End If

    End Sub

    Public Sub CalcularIcePro(TotalBruto As Double, ice As Double)

        Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)

        Dim Cantidad As Double
        Dim IcePro As Double
        Dim ImporteBruto As Double
        Dim CostoTotal As Double
        Dim PrecioCosto As Double

        If (dt.Rows.Count > 0 And tbObservacion.ReadOnly = False) Then
            For i As Integer = 0 To dt.Rows.Count - 1 Step 1
                Cantidad = dt.Rows(i).Item("cbcmin")
                ImporteBruto = dt.Rows(i).Item("cbptot")
                If (ImporteBruto > 0 And Cantidad > 0 And TotalBruto > 0) Then
                    IcePro = ((ImporteBruto * ice) / TotalBruto)
                    CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbice") = IcePro


                    '''''''Aqui Calculo el descuento
                    CostoTotal = (dt.Rows(i).Item("cbptot") - (dt.Rows(i).Item("cbdesc") + dt.Rows(i).Item("cbdescpro"))) + dt.Rows(i).Item("cbice")
                    PrecioCosto = CostoTotal / dt.Rows(i).Item("cbcmin")

                    If (PrecioCosto > 0 And Cantidad > 0) Then
                        CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbtotal") = CostoTotal
                        CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbpcostoun") = PrecioCosto
                    End If
                    '''''''''
                End If
            Next

            tbtotal.Value = (tbSubtotalC.Value - (tbMdesc.Value + tbDescPro.Value)) + tbIce.Value

        End If
    End Sub

    Private Sub tbNitProv_KeyDown(sender As Object, e As KeyEventArgs) Handles tbNitProv.KeyDown

        If (_fnAccesible()) Then
            If e.KeyData = Keys.Control + Keys.Enter Then
                If tbCodProv.Text <> String.Empty Then
                    Dim dt As DataTable
                    dt = L_fnObtenerNitProv(tbCodProv.Text)

                    If dt.Rows.Count = 0 Then
                        ''Throw New Exception("Lista de nit vacia")
                        Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                        ToastNotification.Show(Me, "Lista de nit vacia".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

                    End If
                    Dim listEstCeldas As New List(Of Modelo.Celda)

                    listEstCeldas.Add(New Modelo.Celda("sdnumi,", False, "COD", 90))
                    listEstCeldas.Add(New Modelo.Celda("sdnit", True, "NIT", 90))
                    listEstCeldas.Add(New Modelo.Celda("sdnom1", True, "RAZÓN SOCIAL", 350))
                    listEstCeldas.Add(New Modelo.Celda("sdcodprov", False, "COD PROV".ToUpper, 150))

                    Dim ef = New Efecto
                    ef.tipo = 3
                    ef.dt = dt
                    ef.SeleclCol = 2
                    ef.listEstCeldas = listEstCeldas
                    ef.alto = 50
                    ef.ancho = 250
                    ef.Context = "Seleccione Nit".ToUpper
                    ef.ShowDialog()
                    Dim bandera As Boolean = False
                    bandera = ef.band
                    If (bandera = True) Then
                        Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row

                        tbNitProv.Text = Row.Cells("sdnit").Value
                        tbRazonSocial.Text = Row.Cells("sdnom1").Value

                        tbObservacion.Focus()
                    End If
                Else
                    ToastNotification.Show(Me, "PRIMERO DEBE SELECCIONAR UN PROVEEDOR...!!!",
                                      My.Resources.WARNING, 2000,
                                      eToastGlowColor.Red,
                                      eToastPosition.BottomCenter
                                      )
                End If
            End If
        End If

        If (e.KeyData = Keys.Enter) Then
            If btnGrabar.Enabled = True Then
                Dim nom1 As String
                nom1 = ""
                If (tbNitProv.Text.Trim <> String.Empty) Then
                    L_Validar_NitCompra(tbNitProv.Text.Trim, nom1, tbCodProv.Text)
                    tbRazonSocial.Text = nom1
                End If
            End If
        End If

    End Sub

    Private Sub btnExportar_Click(sender As Object, e As EventArgs) Handles btnExportar.Click
        _prCrearCarpetaReportes()
        Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
        If (P_ExportarExcel(RutaGlobal + "\Reporte\Reporte Compras")) Then
            L_fnExcelCompra(gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina, tbCodigo.Text)
            ToastNotification.Show(Me, "EXPORTACIÓN DE DETALLE DE LA COMPRA EXITOSA...!!!",
                                       img, 2000,
                                       eToastGlowColor.Green,
                                       eToastPosition.BottomCenter)
        Else
            ToastNotification.Show(Me, "FALLÓ LA EXPORTACIÓN DE EL DETALLE DE LA COMPRA...!!!",
                                       My.Resources.WARNING, 2000,
                                       eToastGlowColor.Red,
                                       eToastPosition.BottomLeft)
        End If
    End Sub

#End Region

    Private Sub _prCrearCarpetaReportes()

        Dim rutaDestino As String = RutaGlobal + "\Reporte\Reporte Compras\"

        If System.IO.Directory.Exists(RutaGlobal + "\Reporte\Reporte Compras\") = False Then
            If System.IO.Directory.Exists(RutaGlobal + "\Reporte") = False Then
                System.IO.Directory.CreateDirectory(RutaGlobal + "\Reporte")
                If System.IO.Directory.Exists(RutaGlobal + "\Reporte\Reporte Compras") = False Then
                    System.IO.Directory.CreateDirectory(RutaGlobal + "\Reporte\Reporte Compras")
                End If
            Else
                If System.IO.Directory.Exists(RutaGlobal + "\Reporte\Reporte Compras") = False Then
                    System.IO.Directory.CreateDirectory(RutaGlobal + "\Reporte\Reporte Compras")

                End If
            End If
        End If
    End Sub
    Public Function P_ExportarExcel(_ruta As String) As Boolean
        Dim _ubicacion As String
        'Dim _directorio As New FolderBrowserDialog

        If (1 = 1) Then 'If(_directorio.ShowDialog = Windows.Forms.DialogResult.OK) Then
            '_ubicacion = _directorio.SelectedPath
            _ubicacion = _ruta
            Try
                Dim _stream As Stream
                Dim _escritor As StreamWriter
                Dim _fila As Integer = grdetalle2.GetRows.Length
                Dim _columna As Integer = grdetalle2.RootTable.Columns.Count
                Dim _archivo As String = _ubicacion & "\ListaDetalleCompra_" & Now.Date.Day &
                    "." & Now.Date.Month & "." & Now.Date.Year & "_" & Now.Hour & "." & Now.Minute & "." & Now.Second & ".csv"
                Dim _linea As String = ""
                Dim _filadata = 0, columndata As Int32 = 0
                File.Delete(_archivo)
                _stream = File.OpenWrite(_archivo)
                _escritor = New StreamWriter(_stream, System.Text.Encoding.UTF8)

                For Each _col As GridEXColumn In grdetalle2.RootTable.Columns
                    If (_col.Visible) Then
                        _linea = _linea & _col.Caption & ";"
                    End If
                Next
                _linea = Mid(CStr(_linea), 1, _linea.Length - 1)
                _escritor.WriteLine(_linea)
                _linea = Nothing

                'Pbx_Precios.Visible = True
                'Pbx_Precios.Minimum = 1
                'Pbx_Precios.Maximum = Dgv_Precios.RowCount
                'Pbx_Precios.Value = 1

                For Each _fil As GridEXRow In grdetalle2.GetRows
                    For Each _col As GridEXColumn In grdetalle2.RootTable.Columns
                        If (_col.Visible) Then
                            Dim data As String = CStr(_fil.Cells(_col.Key).Value)
                            data = data.Replace(";", ",")
                            _linea = _linea & data & ";"
                        End If
                    Next
                    _linea = Mid(CStr(_linea), 1, _linea.Length - 1)
                    _escritor.WriteLine(_linea)
                    _linea = Nothing
                    'Pbx_Precios.Value += 1
                Next
                _escritor.Close()
                'Pbx_Precios.Visible = False
                Try
                    Dim ef = New Efecto
                    ef._archivo = _archivo

                    ef.tipo = 1
                    ef.Context = "Su archivo ha sido Guardado en la ruta: " + _archivo + vbLf + "DESEA ABRIR EL ARCHIVO?"
                    ef.Header = "PREGUNTA"
                    ef.ShowDialog()
                    Dim bandera As Boolean = False
                    bandera = ef.band
                    If (bandera = True) Then
                        Process.Start(_archivo)
                    End If

                    'If (MessageBox.Show("Su archivo ha sido Guardado en la ruta: " + _archivo + vbLf + "DESEA ABRIR EL ARCHIVO?", "PREGUNTA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes) Then
                    '    Process.Start(_archivo)
                    'End If
                    Return True
                Catch ex As Exception
                    MsgBox(ex.Message)
                    Return False
                End Try
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        End If
        Return False
    End Function

    Public Function P_ExportarExcelCompras(_ruta As String) As Boolean
        Dim _ubicacion As String
        'Dim _directorio As New FolderBrowserDialog

        If (1 = 1) Then 'If(_directorio.ShowDialog = Windows.Forms.DialogResult.OK) Then
            '_ubicacion = _directorio.SelectedPath
            _ubicacion = _ruta
            Try
                Dim _stream As Stream
                Dim _escritor As StreamWriter
                Dim _fila As Integer = grCompra.GetRows.Length
                Dim _columna As Integer = grCompra.RootTable.Columns.Count
                Dim _archivo As String = _ubicacion & "\ListaCompras_" & Now.Date.Day &
                    "." & Now.Date.Month & "." & Now.Date.Year & "_" & Now.Hour & "." & Now.Minute & "." & Now.Second & ".csv"
                Dim _linea As String = ""
                Dim _filadata = 0, columndata As Int32 = 0
                File.Delete(_archivo)
                _stream = File.OpenWrite(_archivo)
                _escritor = New StreamWriter(_stream, System.Text.Encoding.UTF8)

                For Each _col As GridEXColumn In grCompra.RootTable.Columns
                    If (_col.Visible) Then
                        _linea = _linea & _col.Caption & ";"
                    End If
                Next
                _linea = Mid(CStr(_linea), 1, _linea.Length - 1)
                _escritor.WriteLine(_linea)
                _linea = Nothing

                'Pbx_Precios.Visible = True
                'Pbx_Precios.Minimum = 1
                'Pbx_Precios.Maximum = Dgv_Precios.RowCount
                'Pbx_Precios.Value = 1

                For Each _fil As GridEXRow In grCompra.GetRows
                    For Each _col As GridEXColumn In grCompra.RootTable.Columns
                        If (_col.Visible) Then
                            Dim data As String = CStr(_fil.Cells(_col.Key).Value)
                            data = data.Replace(";", ",")
                            _linea = _linea & data & ";"
                        End If
                    Next
                    _linea = Mid(CStr(_linea), 1, _linea.Length - 1)
                    _escritor.WriteLine(_linea)
                    _linea = Nothing
                    'Pbx_Precios.Value += 1
                Next
                _escritor.Close()
                'Pbx_Precios.Visible = False
                Try
                    Dim ef = New Efecto
                    ef._archivo = _archivo

                    ef.tipo = 1
                    ef.Context = "Su archivo ha sido Guardado en la ruta: " + _archivo + vbLf + "DESEA ABRIR EL ARCHIVO?"
                    ef.Header = "PREGUNTA"
                    ef.ShowDialog()
                    Dim bandera As Boolean = False
                    bandera = ef.band
                    If (bandera = True) Then
                        Process.Start(_archivo)
                    End If

                    'If (MessageBox.Show("Su archivo ha sido Guardado en la ruta: " + _archivo + vbLf + "DESEA ABRIR EL ARCHIVO?", "PREGUNTA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes) Then
                    '    Process.Start(_archivo)
                    'End If
                    Return True
                Catch ex As Exception
                    MsgBox(ex.Message)
                    Return False
                End Try
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        End If
        Return False
    End Function

    'Private Sub btnMovXpeso_Click(sender As Object, e As EventArgs) Handles btnMovXpeso.Click
    '    Try
    '        Dim dt As DataTable = CType(grdetalle.DataSource, DataTable).Copy
    '        If Not (dt.Columns.Contains("gramaje")) Then
    '            dt.Columns.Add("gramaje")
    '        End If

    '        For i = 0 To dt.Rows.Count - 1
    '            Dim dt1 As DataTable = L_fnVerificarProdTI003(dt.Rows(i).Item("cbty5prod"))
    '            dt.Rows(i).Item("gramaje") = dt1.Rows(0).Item("ycdes3")
    '        Next
    '        Dim dt2 = dt.Copy
    '        dt2.Clear()

    '        For i = 0 To dt.Rows.Count - 1
    '            If dt.Rows(i).Item("gramaje").ToString = "X KILO" Then
    '                dt2.Rows.Add(dt.Rows(i).ItemArray)
    '            End If
    '        Next

    '        If dt2.Rows.Count > 0 Then
    '            L_fnMovProdxPeso(gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina, tbCodigo.Text)
    '            Dim frm As New F0_MovimientoProdPeso
    '            frm._nameButton = P_Principal.btInvMovimientoProdPeso.Name
    '            frm.DesdeModulo = True
    '            frm._modulo = P_Principal.FP_INVENTARIO
    '            'frm.dtCompra = CType(grdetalle.DataSource, DataTable).Copy
    '            frm.dtCompra = dt2.Copy
    '            frm.prog = 1
    '            frm._IniciarTodo()
    '            frm.Observ = "INGRESO DESDE COMPRA " + tbCodigo.Text + "-" + tbProveedor.Text

    '            frm.StartPosition = FormStartPosition.WindowsDefaultLocation
    '            frm.WindowState = FormWindowState.Minimized
    '            frm.ShowDialog()
    '        Else
    '            ToastNotification.Show(Me, "NO EXISTE PRODUCTOS X KILO PARA REGISTRAR..!!!",
    '                   My.Resources.WARNING, 2000,
    '                   eToastGlowColor.Red,
    '                   eToastPosition.TopCenter)
    '        End If

    '    Catch ex As Exception
    '        MostrarMensajeError(ex.Message)
    '    End Try
    'End Sub

    Private Sub swMostrar_ValueChanged(sender As Object, e As EventArgs) Handles swMostrar.ValueChanged
        _prCargarCompra()
    End Sub

    Private Sub btnDuplicar_Click(sender As Object, e As EventArgs) Handles btnDuplicar.Click
        If btnGrabar.Enabled = False Then
            Dim fechaVenta, proveedor, observacion, codProv, fechaVenc As String
            Dim tipoventa, emision, moneda As Boolean
            Dim sucursal As Integer
            Dim detalle As DataTable
            fechaVenta = tbFechaVenta.Value.ToString
            _CodProveedor = Convert.ToInt32(tbCodProv.Text)
            tipoventa = swTipoVenta.Value
            proveedor = tbProveedor.Text
            sucursal = cbSucursal.Value
            observacion = tbObservacion.Text
            codProv = tbCodProv.Text
            emision = swEmision.Value
            moneda = swMoneda.Value
            fechaVenc = tbFechaVenc.Value.ToString
            detalle = CType(grdetalle.DataSource, DataTable).Copy

            btnNuevo.PerformClick()

            tbFechaVenta.Value = fechaVenta
            tbCodProv.Text = codProv
            _CodProveedor = codProv
            tbProveedor.Text = proveedor

            grdetalle.DataSource = detalle.Copy
            CType(grdetalle.DataSource, DataTable).AcceptChanges()
            grdetalle.DataChanged = True
            grdetalle.Refresh()

            '_prCargarIconELiminar()
            For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
                Dim Bin As New MemoryStream
                Dim img As New Bitmap(My.Resources.delete, 28, 28)
                img.Save(Bin, Imaging.ImageFormat.Png)
                CType(grdetalle.DataSource, DataTable).Rows(i).Item("img") = Bin.GetBuffer
                grdetalle.RootTable.Columns("img").Visible = True
                CType(grdetalle.DataSource, DataTable).Rows(i).Item("estado") = 0
            Next
        Else
            ToastNotification.Show(Me, "EL REGISTRO DEBE ESTAR GRABADO PARA PODER DUPLICARLO...!!!",
                           My.Resources.WARNING, 2000,
                           eToastGlowColor.Red,
                           eToastPosition.TopCenter)
        End If



    End Sub

    Private Sub btnExportarCompras_Click(sender As Object, e As EventArgs) Handles btnExportarCompras.Click
        _prCrearCarpetaReportes()
        Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
        If (P_ExportarExcelCompras(RutaGlobal + "\Reporte\Reporte Compras")) Then
            L_fnBotonExportar(gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina, 0, "COMPRAS", "COMPRAS GENERAL")
            ToastNotification.Show(Me, "EXPORTACIÓN DE LAS COMPRAS EXITOSA...!!!",
                                       img, 2000,
                                       eToastGlowColor.Green,
                                       eToastPosition.BottomCenter)
        Else
            ToastNotification.Show(Me, "FALLÓ LA EXPORTACIÓN DE LAS COMPRAS...!!!",
                                       My.Resources.WARNING, 2000,
                                       eToastGlowColor.Red,
                                       eToastPosition.BottomLeft)
        End If
    End Sub
End Class