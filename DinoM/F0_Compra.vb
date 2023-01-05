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
Public Class F0_Compra
    Dim _Inter As Integer = 0

#Region "Variables Globales"
    Dim _CodProveedor As Integer = 0
    Dim _numiCatCosto As Integer = 0
    Public _nameButton As String
    Public _tab As SuperTabItem
    Dim _PorcentajeUtil As Double = 0 '' En esta varible obtendre de la libreria el porcentaje de utilidad para la venta 
    Dim _estadoPor As Integer ''En esta variable me dira si sera visible o no las columnas de porcentaje de utilidad y precio de venta
    Public _detalleCompras As DataTable
#End Region

#Region "Metodos Privados"
    Private Sub _IniciarTodo()
        'L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        _prCargarComboLibreriaSucursal(cbSucursal)
        _prObtenerPorcentajeUtilidad()
        ' Me.WindowState = FormWindowState.Maximized
        _prCargarCompra()
        _prInhabiliitar()
        grCompra.Focus()
        _prAsignarPermisos()
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
            .DropDownList.Columns("aabdes").Caption = "SUCURSAL"
            .ValueMember = "aanumi"
            .DisplayMember = "aabdes"
            .DataSource = dt
            .Refresh()
        End With
    End Sub
    Private Sub _prInhabiliitar()
        tbCodigo.ReadOnly = True
        tbProveedor.ReadOnly = True
        tbObservacion.ReadOnly = True
        tbFechaVenta.IsInputReadOnly = True
        tbFechaVenc.IsInputReadOnly = True
        cbSucursal.ReadOnly = True
        swTipoVenta.IsReadOnly = True
        ''''''''''
        btnModificar.Enabled = True
        btnGrabar.Enabled = False
        btnNuevo.Enabled = True
        btnEliminar.Enabled = True

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
        tbFechaVenta.IsInputReadOnly = False
        tbFechaVenc.IsInputReadOnly = False
        cbSucursal.ReadOnly = False
        swTipoVenta.IsReadOnly = False
        btnGrabar.Enabled = True
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
        tbObservacion.Clear()
        If (CType(cbSucursal.DataSource, DataTable).Rows.Count > 0) Then
            cbSucursal.SelectedIndex = 0
        Else
            cbSucursal.SelectedIndex = -1
        End If
        swTipoVenta.Value = True
        _CodProveedor = 0
        tbFechaVenta.Value = Now.Date
        tbFechaVenc.Visible = False
        lbCredito.Visible = False
        _prCargarDetalleVenta(-1)

        MSuperTabControl.SelectedTabIndex = 0

        tbPdesc.Value = 0
        tbMdesc.Value = 0
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
        tbProveedor.Focus()

    End Sub
    Public Sub _prMostrarRegistro(_N As Integer)
        '' grVentas.Row = _N
        'a.canumi ,a.caalm ,a.cafdoc ,a.caty4prov ,proveedor .yddesc as proveedor ,a.catven ,a.cafvcr ,
        'a.camon ,IIF(camon=1,'Boliviano','Dolar') as moneda,a.caest ,a.caobs ,
        'a.cadesc ,a.cafact ,a.cahact ,a.cauact,(Sum(b.cbptot)-a.cadesc ) as total

        With grCompra
            tbCodigo.Text = .GetValue("canumi")
            tbFechaVenta.Value = .GetValue("cafdoc")
            _CodProveedor = .GetValue("caty4prov")
            swTipoVenta.Value = .GetValue("catven")
            tbProveedor.Text = .GetValue("proveedor")
            cbSucursal.Value = .GetValue("caalm")
            tbObservacion.Text = .GetValue("caobs")


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

        _prCargarDetalleVenta(tbCodigo.Text)
        tbMdesc.Value = grCompra.GetValue("cadesc")
        _prCalcularPrecioTotal()
        LblPaginacion.Text = Str(grCompra.Row + 1) + "/" + grCompra.RowCount.ToString

    End Sub

    Private Sub _prCargarDetalleVenta(_numi As String)
        Dim dt As New DataTable
        dt = L_fnDetalleCompra(_numi)
        grdetalle.DataSource = dt
        grdetalle.RetrieveStructure()
        grdetalle.AlternatingColors = True
        '     a.cbnumi ,a.cbtv1numi ,a.cbty5prod ,b.yfcdprod1 as producto,a.cbest ,a.cbcmin ,a.cbumin ,Umin .ycdes3 as unidad,a.cbpcost 
        ',a.cbutven ,a.cbprven  ,a.cbptot ,a.cbobs ,
        'a.cbfact ,a.cbhact ,a.cbuact,1 as escado,Cast(null as Image) as img

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
            .Width = 90
            .Visible = False
        End With

        With grdetalle.RootTable.Columns("producto")
            .Caption = "PRODUCTOS"
            .Width = 250
            .Visible = True

        End With
        With grdetalle.RootTable.Columns("cbest")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With

        With grdetalle.RootTable.Columns("cbcmin")
            .Width = 160
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Cant. Minima".ToUpper
        End With
        With grdetalle.RootTable.Columns("cbumin")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("unidad")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Caption = "Unidad".ToUpper
        End With
        With grdetalle.RootTable.Columns("cbpcost")
            .Width = 120
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Precio U.".ToUpper
        End With
        If (_estadoPor = 1) Then
            With grdetalle.RootTable.Columns("cbutven")
                .Width = 120
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = True
                .FormatString = "0.00"
                .Caption = "Utilidad.".ToUpper
            End With
            With grdetalle.RootTable.Columns("cbprven")
                .Width = 120
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = True
                .FormatString = "0.00"
                .Caption = "Precio Venta.".ToUpper
            End With
        Else
            With grdetalle.RootTable.Columns("cbutven")
                .Width = 120
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = False
                .FormatString = "0.00"
                .Caption = "Utilidad.".ToUpper
            End With
            With grdetalle.RootTable.Columns("cbprven")
                .Width = 120
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = False
                .FormatString = "0.00"
                .Caption = "Precio Venta.".ToUpper
            End With
        End If

        With grdetalle.RootTable.Columns("cbptot")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Sub Total".ToUpper
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
        With grdetalle.RootTable.Columns("img")
            .Width = 80
            .Caption = "Eliminar".ToUpper
            .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
            .Visible = False
        End With
        With grdetalle
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
        End With
    End Sub

    Private Sub _prCargarCompra()
        Dim dt As New DataTable
        dt = L_fnGeneralCompras()
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

        With grCompra.RootTable.Columns("caty4prov")
            .Width = 160
            .Visible = False
        End With
        With grCompra.RootTable.Columns("proveedor")
            .Width = 250
            .Visible = True
            .Caption = "proveedor".ToUpper
        End With


        With grCompra.RootTable.Columns("catven")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With

        With grCompra.RootTable.Columns("cafvcr")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        '     a.canumi ,a.caalm ,a.cafdoc ,a.caty4prov ,proveedor .yddesc as proveedor ,a.catven ,a.cafvcr ,
        'a.camon ,IIF(camon=1,'Boliviano','Dolar') as moneda,a.caest ,a.caobs ,
        'a.cadesc ,a.cafact ,a.cahact ,a.cauact,(Sum(b.cbptot)-a.cadesc ) as total



        With grCompra.RootTable.Columns("camon")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grCompra.RootTable.Columns("moneda")
            .Width = 150
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Caption = "MONEDA"
        End With
        With grCompra.RootTable.Columns("caobs")
            .Width = 200
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Caption = "OBSERVACION"
        End With
        With grCompra.RootTable.Columns("cadesc")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
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
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grCompra.RootTable.Columns("total")
            .Width = 150
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Caption = "TOTAL"
            .FormatString = "0.00"
        End With
        With grCompra
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
        End With

        If (dt.Rows.Count <= 0) Then
            _prCargarDetalleVenta(-1)
        End If
    End Sub
    Private Sub _prCargarProductos(_cliente As String)

        Dim dtname As DataTable = L_fnNameLabel()
        Dim dt As New DataTable
        If (cbSucursal.SelectedIndex < 0) Then
            Return
        End If
        dt = L_fnListarProductosCompra(cbSucursal.Value, 73)  ''1=Almacen  73=Cat Precio Costo
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
        '     a.cbnumi ,a.cbtv1numi ,a.cbty5prod ,b.yfcdprod1 as producto,a.cbest ,a.cbcmin ,a.cbumin ,Umin .ycdes3 as unidad,a.cbpcost 
        ',a.cbutven ,a.cbprven  ,a.cbptot ,a.cbobs ,
        'a.cbfact ,a.cbhact ,a.cbuact,1 as escado,Cast(null as Image) as img
        Dim Bin As New MemoryStream
        Dim img As New Bitmap(My.Resources.delete, 28, 28)
        img.Save(Bin, Imaging.ImageFormat.Png)
        CType(grdetalle.DataSource, DataTable).Rows.Add(_fnSiguienteNumi() + 1, 0, 0, "", 0, 0, 0, "",
                                                        0, 0, 0, 0, "", Now.Date, "", "", 0, Bin.GetBuffer)
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
        GPanelProductos.Visible = True
        PanelTotal.Visible = False
        PanelInferior.Visible = False
        _prCargarProductos(73) ''''Aqui poner el Primer Precio de Costo
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
            End If
            _prCalcularPrecioTotal()
        End If



    End Sub
    Public Sub _prCalcularPrecioTotal()
        Dim montodesc As Double = tbMdesc.Value
        Dim pordesc As Double = ((montodesc * 100) / grdetalle.GetTotal(grdetalle.RootTable.Columns("cbptot"), AggregateFunction.Sum))
        tbPdesc.Value = pordesc
        tbtotal.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("cbptot"), AggregateFunction.Sum) - montodesc
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

        If (grdetalle.RowCount > 0) Then
            grdetalle.Row = grdetalle.RowCount - 1
            If (grdetalle.GetValue("cbty5prod") = 0) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "Por Favor Seleccione  un detalle de producto".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                Return False
            End If

        End If
        If (cbSucursal.SelectedIndex < 0) Then


            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Seleccione una Sucursal".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            tbProveedor.Focus()
            Return False


        End If
        Return True
    End Function

    Public Sub _GuardarNuevo()


        ''_canumi As String, _caalm As Integer, _cafdoc As String, _caTy4prov As Integer, _catven As Integer, _cafvcr As String,
        ''                                   _camon As Integer, _caobs As String,
        ''                                   _cadesc As Double, detalle As DataTable

        'Dim res As Boolean = L_fnGrabarCompra("", cbSucursal.Value, tbFechaVenta.Value.ToString("yyyy/MM/dd"), _CodProveedor, IIf(swTipoVenta.Value = True, 1, 0), IIf(swTipoVenta.Value = True, Now.Date.ToString("yyyy/MM/dd"), tbFechaVenc.Value.ToString("yyyy/MM/dd")), 0, tbObservacion.Text, tbMdesc.Value, tbtotal.Value, CType(grdetalle.DataSource, DataTable), _detalleCompras, IIf(swEmision.Value = True, 1, 2), tbNFactura.Text)


        'If res Then

        '    Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
        '    ToastNotification.Show(Me, "Código de Compra ".ToUpper + tbCodigo.Text + " Grabado con Exito.".ToUpper,
        '                              img, 2000,
        '                              eToastGlowColor.Green,
        '                              eToastPosition.TopCenter
        '                              )

        '    _prCargarCompra()

        '    _Limpiar()

        'Else
        '    Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
        '    ToastNotification.Show(Me, "La Compra no pudo ser insertado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

        'End If

    End Sub
    Private Sub _prGuardarModificado()
        'Dim res As Boolean = L_fnModificarCompra("", cbSucursal.Value, tbFechaVenta.Value.ToString("yyyy/MM/dd"), _CodProveedor, IIf(swTipoVenta.Value = True, 1, 0), IIf(swTipoVenta.Value = True, Now.Date.ToString("yyyy/MM/dd"), tbFechaVenc.Value.ToString("yyyy/MM/dd")), cbSucursal.Value, tbObservacion.Text, tbMdesc.Value, tbtotal.Value, CType(grdetalle.DataSource, DataTable), _detalleCompras, IIf(swEmision.Value = True, 1, 2), tbNFactura.Text)
        'If res Then

        '    Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
        '    ToastNotification.Show(Me, "Código de Compra ".ToUpper + tbCodigo.Text + " Modificado con Exito.".ToUpper,
        '                              img, 2000,
        '                              eToastGlowColor.Green,
        '                              eToastPosition.TopCenter
        '                              )
        '    _prCargarCompra()

        '    _prSalir()


        'Else
        '    Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
        '    ToastNotification.Show(Me, "La Compra no pudo ser Modificada".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

        'End If
    End Sub
    Private Sub _prSalir()
        If btnGrabar.Enabled = True Then
            _prInhabiliitar()
            If grCompra.RowCount > 0 Then

                _prMostrarRegistro(0)

            End If
        Else

            'Me.Close()
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
        If grCompra.RowCount > 0 Then
            _MPos = 0
            ''   _prMostrarRegistro(_MPos)
            grCompra.Row = _MPos
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
    Private Sub tbProveedor_KeyDown(sender As Object, e As KeyEventArgs) Handles tbProveedor.KeyDown
        If (_fnAccesible()) Then
            If e.KeyData = Keys.Control + Keys.Enter Then

                Dim dt As DataTable

                dt = L_fnListarProveedores()
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

                    _CodProveedor = Row.Cells("ydnumi").Value
                    tbProveedor.Text = Row.Cells("yddesc").Value
                    tbObservacion.Focus()

                End If

            End If

        End If




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
            'Habilitar solo las columnas de Precio, %, Monto y Observación
            If (e.Column.Index = grdetalle.RootTable.Columns("cbcmin").Index) Then
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
            If (_CodProveedor <= 0) Then
                ToastNotification.Show(Me, "           Antes de Continuar Por favor Seleccione un Proveedor!!             ", My.Resources.WARNING, 4000, eToastGlowColor.Red, eToastPosition.TopCenter)
                tbProveedor.Focus()

                Return
            End If


            grdetalle.Select()
            grdetalle.Col = 3
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
                grdetalle.Row = grdetalle.RowCount - 1
                _fnObtenerFilaDetalle(pos, grdetalle.GetValue("cbnumi"))
                Dim existe As Boolean = _fnExisteProducto(grProductos.GetValue("yfnumi"))
                If (pos >= 0) Then ''And (Not existe)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbty5prod") = grProductos.GetValue("yfnumi")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("producto") = grProductos.GetValue("yfcdprod1")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbumin") = grProductos.GetValue("yfumin")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("unidad") = grProductos.GetValue("UnidMin")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpcost") = grProductos.GetValue("yhprecio")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbptot") = grProductos.GetValue("yhprecio")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbcmin") = 1
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbutven") = _PorcentajeUtil
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbprven") = grProductos.GetValue("yhprecio") + ((_PorcentajeUtil / 100) * grProductos.GetValue("yhprecio"))
                    _prCalcularPrecioTotal()
                    _DesHabilitarProductos()
                    'Else
                    '    If (existe) Then
                    '        Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                    '        ToastNotification.Show(Me, "El producto ya existe en el detalle".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                    '    End If
                End If
            End If
        End If
        If e.KeyData = Keys.Escape Then
            _DesHabilitarProductos()
        End If
    End Sub
    Private Sub grdetalle_CellValueChanged(sender As Object, e As ColumnActionEventArgs) Handles grdetalle.CellValueChanged
        If (e.Column.Index = grdetalle.RootTable.Columns("cbcmin").Index) Then
            If (Not IsNumeric(grdetalle.GetValue("cbcmin")) Or grdetalle.GetValue("cbcmin").ToString = String.Empty) Then

                'grDetalle.GetRow(rowIndex).Cells("cant").Value = 1
                '  grDetalle.CurrentRow.Cells.Item("cant").Value = 1
                Dim lin As Integer = grdetalle.GetValue("cbnumi")
                Dim pos As Integer = -1
                _fnObtenerFilaDetalle(pos, lin)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbcmin") = 1
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbptot") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpcost")
                'grdetalle.SetValue("tbcmin", 1)
                'grdetalle.SetValue("tbptot", grdetalle.GetValue("cbpcost"))
            Else
                If (grdetalle.GetValue("cbcmin") > 0) Then
                    Dim rowIndex As Integer = grdetalle.Row
                    P_PonerTotal(rowIndex)

                Else
                    Dim lin As Integer = grdetalle.GetValue("cbnumi")
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, lin)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbcmin") = 1
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbptot") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpcost")
                    _prCalcularPrecioTotal()
                    'grdetalle.SetValue("tbcmin", 1)
                    'grdetalle.SetValue("tbptot", grdetalle.GetValue("cbpcost"))

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
        If (grCompra.RowCount > 0) Then
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
            Dim res As Boolean = L_fnEliminarCompra(tbCodigo.Text, mensajeError)
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
        If (grCompra.RowCount >= 0 And grCompra.Row >= 0) Then

            _prMostrarRegistro(grCompra.Row)
        End If


    End Sub

    Private Sub btnSiguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click
        Dim _pos As Integer = grCompra.Row
        If _pos < grCompra.RowCount - 1 Then
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
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        _Inter = _Inter + 1
        If _Inter = 1 Then
            Me.WindowState = FormWindowState.Normal

        Else
            Me.Opacity = 100
            Timer1.Enabled = False
        End If
    End Sub

#End Region










End Class