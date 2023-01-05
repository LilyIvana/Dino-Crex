
Imports Logica.AccesoLogica
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

Public Class F0_Movimiento
    Dim _Inter As Integer = 0
#Region "Variables Globales"
    Public _nameButton As String
    Public _tab As SuperTabItem
    Dim Lote As Boolean = False
    Public _modulo As SideNavItem
    Dim Table_producto As DataTable
    Dim FilaSelectLote As DataRow = Nothing
#End Region
#Region "Metodos Privados"
    Private Sub _IniciarTodo()
        MSuperTabControl.SelectedTabIndex = 0
        '' L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        _prValidarLote()
        'Me.WindowState = FormWindowState.Maximized
        _prCargarComboLibreriaConcepto(cbConcepto)
        _prCargarComboLibreriaDeposito(cbAlmacenOrigen)
        _prCargarComboLibreriaDeposito(cbDepositoDestino)
        _prCargarVenta()
        _prInhabiliitar()
        'Dim blah As New Bitmap(New Bitmap(My.Resources.compra), 20, 20)
        'Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        'Me.Icon = ico
        _prAsignarPermisos()
        Me.Text = "MOVIMIENTO PRODUCTOS"
        tbObservacion.MaxLength = 100
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
    Private Sub _prCargarComboLibreriaDeposito(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnListarDepositos()
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
    Private Sub _prCargarComboLibreriaConcepto(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_prMovimientoConcepto()
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("cpnumi").Width = 60
            .DropDownList.Columns("cpnumi").Caption = "COD"
            .DropDownList.Columns.Add("cpdesc").Width = 250
            .DropDownList.Columns("cpdesc").Caption = "CONCEPTO"
            .ValueMember = "cpnumi"
            .DisplayMember = "cpdesc"
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

        cbConcepto.ReadOnly = True
        tbObservacion.ReadOnly = True
        tbFecha.IsInputReadOnly = True
        cbAlmacenOrigen.ReadOnly = True
        ''''''''''
        btnModificar.Enabled = True
        btnGrabar.Enabled = False
        btnNuevo.Enabled = True
        btnEliminar.Enabled = True
        grmovimiento.Enabled = True

        grdetalle.RootTable.Columns("img").Visible = False


        If (GPanelProductos.Visible = True) Then
            _DesHabilitarProductos()
        End If

        PanelInferior.Enabled = True
        FilaSelectLote = Nothing
    End Sub
    Private Sub _prhabilitar()
        'cbConcepto.ReadOnly = False
        tbObservacion.ReadOnly = False
        tbFecha.IsInputReadOnly = False
        cbAlmacenOrigen.ReadOnly = False
        grmovimiento.Enabled = False
        ''  tbCliente.ReadOnly = False  por que solo podra seleccionar Cliente
        ''  tbVendedor.ReadOnly = False
        If (tbCodigo.Text.Length > 0) Then
            cbAlmacenOrigen.ReadOnly = True
            cbConcepto.ReadOnly = True
        Else
            cbAlmacenOrigen.ReadOnly = False
            cbConcepto.ReadOnly = False

        End If
        btnGrabar.Enabled = True
    End Sub
    Public Sub _prFiltrar()
        'cargo el buscador
        Dim _Mpos As Integer
        _prCargarVenta()
        If grmovimiento.RowCount > 0 Then
            _Mpos = 0
            grmovimiento.Row = _Mpos
        Else
            _Limpiar()
            LblPaginacion.Text = "0/0"
        End If
    End Sub
    Private Sub _Limpiar()
        tbCodigo.Clear()
        tbObservacion.Clear()
        tbFecha.Value = Now.Date
        _prCargarDetalleVenta(-1)


        With grdetalle.RootTable.Columns("img")
            .Width = 80
            .Caption = "Eliminar"
            .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
            .Visible = True
        End With

        If (GPanelProductos.Visible = True) Then
            GPanelProductos.Visible = False
            PanelInferior.Visible = True
        End If
        If (CType(cbAlmacenOrigen.DataSource, DataTable).Rows.Count > 0) Then
            cbAlmacenOrigen.SelectedIndex = 0

        End If
        If (CType(cbConcepto.DataSource, DataTable).Rows.Count > 0) Then
            cbConcepto.SelectedIndex = 0

        End If
        _prAddDetalleVenta()
        cbConcepto.Focus()
        FilaSelectLote = Nothing

        If grproducto.RowCount > 0 Then
            CType(grproducto.DataSource, DataTable).Rows.Clear()
        End If

    End Sub
    Public Sub _prMostrarRegistro(_N As Integer)
        '      a.ibid ,a.ibfdoc ,a.ibconcep ,b.cpdesc as concepto,a.ibobs ,a.ibest ,a.ibalm ,a.ibiddc 
        ',a.ibfact ,a.ibhact ,a.ibuact,ibdepdest
        With grmovimiento
            tbCodigo.Text = .GetValue("ibid")
            tbFecha.Value = .GetValue("ibfdoc")
            cbConcepto.Value = .GetValue("ibconcep")
            tbObservacion.Text = .GetValue("ibobs")
            lbFecha.Text = CType(.GetValue("ibfact"), Date).ToString("dd/MM/yyyy")
            lbHora.Text = .GetValue("ibhact").ToString
            lbUsuario.Text = .GetValue("ibuact").ToString
            cbAlmacenOrigen.Value = .GetValue("ibalm")
            cbDepositoDestino.Value = IIf(IsDBNull(.GetValue("ibdepdest")), 0, .GetValue("ibdepdest"))
        End With

        _prCargarDetalleVenta(tbCodigo.Text)
        LblPaginacion.Text = Str(grmovimiento.Row + 1) + "/" + grmovimiento.RowCount.ToString

    End Sub

    Private Sub _prCargarDetalleVenta(_numi As String)
        Dim dt As New DataTable
        dt = L_fnDetalleMovimiento(_numi)
        grdetalle.DataSource = dt
        grdetalle.RetrieveStructure()
        grdetalle.AlternatingColors = True
        'a.icid ,a.icibid ,a.iccprod ,b.yfcdprod1  as producto,a.iccant ,
        '	a.iclot ,a.icfvenc ,Cast(null as image ) as img,1 as estado,
        '	(Sum(inv.iccven )+a.iccant  ) as stock
        With grdetalle.RootTable.Columns("icid")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = False

        End With
        With grdetalle.RootTable.Columns("iccprod")
            .Width = 90
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("icibid")
            .Width = 90
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("yfcprod")
            .Width = 140
            .Caption = "COD. PRODUCTO"
            .Visible = True
        End With
        With grdetalle.RootTable.Columns("producto")
            .Caption = "PRODUCTOS"
            .Width = 250
            .Visible = True


        End With
        With grdetalle.RootTable.Columns("Laboratorio")
            .Caption = "ORIGEN"
            .Width = 200
            .Visible = True


        End With
        With grdetalle.RootTable.Columns("Presentacion")
            .Caption = "PRESENTACION"
            .Width = 150
            .Visible = True


        End With
        With grdetalle.RootTable.Columns("iccant")
            .Width = 160
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Cantidad".ToUpper
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
        If (Lote = True) Then
            With grdetalle.RootTable.Columns("iclot")
                .Width = 120
                .Caption = "lote".ToUpper
                .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
                .Visible = True
            End With
            With grdetalle.RootTable.Columns("icfvenc")
                .Width = 120
                .Caption = "FECHA VENC.".ToUpper
                .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
                .FormatString = "yyyy/MM/dd"
                .Visible = True
            End With
        Else

            With grdetalle.RootTable.Columns("iclot")
                .Width = 120
                .Caption = "lote".ToUpper
                .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
                .Visible = False
            End With
            With grdetalle.RootTable.Columns("icfvenc")
                .Width = 120
                .Caption = "FECHA VENC.".ToUpper
                .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
                .FormatString = "yyyy/MM/dd"
                .Visible = False
            End With


        End If
        With grdetalle.RootTable.Columns("stock")
            .Width = 120
            .Caption = "stock".ToUpper
            .Visible = False
        End With
        With grdetalle
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
            .BoundMode = Janus.Data.BoundMode.Bound
            .RowHeaders = InheritableBoolean.True
        End With
    End Sub

    Private Sub _prCargarVenta()
        Dim dt As New DataTable
        dt = L_fnGeneralMovimiento()
        grmovimiento.DataSource = dt
        grmovimiento.RetrieveStructure()
        grmovimiento.AlternatingColors = True


        With grmovimiento.RootTable.Columns("ibid")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = True

        End With

        With grmovimiento.RootTable.Columns("ibfdoc")
            .Width = 90
            .Visible = True
            .Caption = "FECHA"
            .FormatString = "yyyy/MM/dd"
        End With
        With grmovimiento.RootTable.Columns("ibconcep")
            .Width = 90
            .Visible = False
        End With

        With grmovimiento.RootTable.Columns("concepto")
            .Width = 160
            .Visible = True
            .Caption = "CONCEPTO"
        End With
        With grmovimiento.RootTable.Columns("ibobs")
            .Width = 250
            .Visible = True
            .Caption = "observacion".ToUpper
        End With


        With grmovimiento.RootTable.Columns("ibest")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With

        With grmovimiento.RootTable.Columns("ibalm")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grmovimiento.RootTable.Columns("ibiddc")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        '      a.ibid ,a.ibfdoc ,a.ibconcep ,b.cpdesc as concepto,a.ibobs ,a.ibest ,a.ibalm ,a.ibiddc 
        ',a.ibfact ,a.ibhact ,a.ibuact

        With grmovimiento.RootTable.Columns("ibfact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grmovimiento.RootTable.Columns("ibhact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grmovimiento.RootTable.Columns("ibuact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With

        With grmovimiento.RootTable.Columns("ibdepdest")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grmovimiento
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
    Public Sub actualizarSaldoSinLote(ByRef dt As DataTable)
        'b.yfcdprod1 ,a.iclot ,a.icfven  ,a.iccven 

        'a.yfnumi  ,a.yfcdprod1  ,a.yfcdprod2,Sum(b.iccven ) as stock
        'Dim _detalle As DataTable = CType(grdetalle.DataSource, DataTable)
        Dim _dtDetalle As DataTable = CType(grdetalle.DataSource, DataTable)
        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
            Dim sum As Integer = 0
            Dim codProducto As Integer = dt.Rows(i).Item("yfnumi")
            For j As Integer = 0 To _dtDetalle.Rows.Count - 1 Step 1

                Dim estado As Integer = _dtDetalle.Rows(j).Item("estado")
                If (estado = 0) Then
                    If (codProducto = _dtDetalle.Rows(j).Item("iccprod")) Then
                        sum = sum + _dtDetalle.Rows(j).Item("iccant")
                    End If
                End If
            Next
            dt.Rows(i).Item("stock") = dt.Rows(i).Item("stock") - sum
        Next

    End Sub
    Private Sub _prCargarProductos()
        Dim dt As New DataTable

        If (Lote = True And cbConcepto.Value <> 1) Then
            dt = L_prMovimientoListarProductosConLote(cbAlmacenOrigen.Value)  ''1=Almacen
            actualizarSaldoSinLote(dt)
        Else
            dt = L_prMovimientoListarProductos(CType(grdetalle.DataSource, DataTable), cbAlmacenOrigen.Value)  ''1=Almacen
        End If

        'a.yfnumi  ,a.yfcdprod1  ,a.yfcdprod2 
        If (grproducto.RowCount > 0 And cbConcepto.Value = 1) Then
            grproducto.DataSource = dt
            Return
        End If
        grproducto.DataSource = dt
        grproducto.RetrieveStructure()
        grproducto.AlternatingColors = True
        With grproducto.RootTable.Columns("yfnumi")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = False

        End With
        With grproducto.RootTable.Columns("yfcprod")
            .Width = 120
            .Caption = "COD. PRODUCTO"
            .Visible = True

        End With
        With grproducto.RootTable.Columns("yfcdprod1")
            .Width = 350
            .Caption = "PRODUCTOS"
            .Visible = True

        End With

        With grproducto.RootTable.Columns("yfcdprod2")
            .Width = 250
            .Visible = True
            .Caption = "COD. PROVEEDOR"
        End With

        With grproducto.RootTable.Columns("Laboratorio")
            .Width = 200
            .Visible = True
            .Caption = "ORIGEN"
        End With
        With grproducto.RootTable.Columns("Presentacion")
            .Width = 120
            .Visible = True
            .Caption = "PRESENTACION"
        End With
        With grproducto.RootTable.Columns("yfcdprod2")
            .Width = 200
            .Visible = True
            .Caption = "COD. PROVEEDOR"
        End With
        With grproducto.RootTable.Columns("stock")
            .Width = 150
            .Visible = True
            .FormatString = "0.00"
            .Caption = "STOCK"
        End With
        With grproducto
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
        End With
        _prAplicarCondiccionJanusSinLote()
    End Sub
    Public Sub actualizarSaldo(ByRef dt As DataTable, CodProducto As Integer)
        'b.yfcdprod1 ,a.iclot ,a.icfven  ,a.iccven 

        '      a.tbnumi ,a.tbtv1numi ,a.tbty5prod ,b.yfcdprod1 as producto,a.tbest ,a.tbcmin ,a.tbumin ,Umin .ycdes3 as unidad,a.tbpbas ,a.tbptot ,a.tbobs ,
        'a.tbpcos,a.tblote ,a.tbfechaVenc , a.tbptot2, a.tbfact ,a.tbhact ,a.tbuact,1 as estado,Cast(null as Image) as img,
        'Cast (0 as decimal (18,2)) as stock
        Dim _detalle As DataTable = CType(grdetalle.DataSource, DataTable)

        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
            Dim lote As String = dt.Rows(i).Item("iclot")
            Dim FechaVenc As Date = dt.Rows(i).Item("icfven")
            Dim sum As Integer = 0
            For j As Integer = 0 To _detalle.Rows.Count - 1
                Dim estado As Integer = _detalle.Rows(j).Item("estado")
                If (estado = 0) Then
                    If (lote = _detalle.Rows(j).Item("iclot") And
                        FechaVenc = _detalle.Rows(j).Item("icfvenc") And CodProducto = _detalle.Rows(j).Item("iccprod")) Then
                        sum = sum + _detalle.Rows(j).Item("iccant")
                    End If
                End If
            Next
            dt.Rows(i).Item("stock") = dt.Rows(i).Item("stock") - sum
        Next

    End Sub
    Private Sub _prCargarLotesDeProductos(CodProducto As Integer, nameProducto As String)
        If (cbAlmacenOrigen.SelectedIndex < 0) Then
            Return
        End If
        Dim dt As New DataTable
        GPanelProductos.Text = nameProducto
        dt = L_fnListarLotesPorProductoMovimiento(cbAlmacenOrigen.Value, CodProducto)  ''1=Almacen
        actualizarSaldo(dt, CodProducto)
        grproducto.DataSource = dt
        grproducto.RetrieveStructure()
        grproducto.AlternatingColors = True
        With grproducto.RootTable.Columns("yfcdprod1")
            .Width = 150
            .Visible = False

        End With
        'b.yfcdprod1 ,a.iclot ,a.icfven  ,a.iccven 
        With grproducto.RootTable.Columns("iclot")
            .Width = 150
            .Caption = "LOTE"
            .Visible = True

        End With
        With grproducto.RootTable.Columns("icfven")
            .Width = 160
            .Caption = "FECHA VENCIMIENTO"
            .FormatString = "yyyy/MM/dd"
            .Visible = True

        End With

        With grproducto.RootTable.Columns("stock")
            .Width = 150
            .Visible = True
            .Caption = "Stock"
            .FormatString = "0.00"
            .AggregateFunction = AggregateFunction.Sum
        End With


        With grproducto
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
        _prAplicarCondiccionJanusLote()

    End Sub
    Public Sub _prAplicarCondiccionJanusLote()
        Dim fc As GridEXFormatCondition
        fc = New GridEXFormatCondition(grproducto.RootTable.Columns("stock"), ConditionOperator.Equal, 0)
        fc.FormatStyle.BackColor = Color.Gold
        fc.FormatStyle.FontBold = TriState.True
        fc.FormatStyle.ForeColor = Color.White
        grproducto.RootTable.FormatConditions.Add(fc)

        Dim fc2 As GridEXFormatCondition
        fc2 = New GridEXFormatCondition(grproducto.RootTable.Columns("icfven"), ConditionOperator.LessThanOrEqualTo, Now.Date)
        fc2.FormatStyle.BackColor = Color.Red
        fc2.FormatStyle.FontBold = TriState.True
        fc2.FormatStyle.ForeColor = Color.White
        grproducto.RootTable.FormatConditions.Add(fc2)
    End Sub
    Public Sub _prAplicarCondiccionJanusSinLote()
        Dim fc As GridEXFormatCondition
        fc = New GridEXFormatCondition(grproducto.RootTable.Columns("stock"), ConditionOperator.Equal, 0)
        'fc.FormatStyle.FontBold = TriState.True
        fc.FormatStyle.ForeColor = Color.Tan
        grproducto.RootTable.FormatConditions.Add(fc)
    End Sub
    Private Sub _prAddDetalleVenta()
        'If grdetalle.RowCount > 0 Then
        '    Return
        'End If
        Dim Bin As New MemoryStream
        Dim img As New Bitmap(My.Resources.delete, 28, 28)
        img.Save(Bin, Imaging.ImageFormat.Png)
        CType(grdetalle.DataSource, DataTable).Rows.Add(_fnSiguienteNumi() + 1, 0, 0, "", "", "", "", 0, "20500101", CDate("2050/01/01"), Bin.GetBuffer, 0, 0)
    End Sub
    Public Function _fnSiguienteNumi()
        Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)
        Dim mayor As Integer = 0
        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
            Dim data As Integer = IIf(IsDBNull(CType(grdetalle.DataSource, DataTable).Rows(i).Item("icid")), 0, CType(grdetalle.DataSource, DataTable).Rows(i).Item("icid"))
            If (data > mayor) Then
                mayor = data

            End If
        Next
        Return mayor
    End Function
    Public Function _fnAccesible()
        Return tbFecha.IsInputReadOnly = False
    End Function
    Private Sub _HabilitarProductos()
        GPanelProductos.Visible = True

        PanelInferior.Visible = False
        _prCargarProductos()
        grproducto.Focus()
        grproducto.MoveTo(grproducto.FilterRow)
        grproducto.Col = 1
    End Sub
    Private Sub _DesHabilitarProductos()
        If (GPanelProductos.Visible = True) Then
            GPanelProductos.Visible = False
            PanelInferior.Visible = True
            grdetalle.Select()
            grdetalle.Col = 4
            grdetalle.Row = grdetalle.RowCount - 1
        End If
        FilaSelectLote = Nothing
    End Sub
    Public Sub _fnObtenerFilaDetalle(ByRef pos As Integer, numi As Integer)
        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _numi As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("icid")
            If (_numi = numi) Then
                pos = i
                Return
            End If
        Next

    End Sub

    Public Function _fnExisteProducto(idprod As Integer) As Boolean
        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _idprod As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("iccprod")
            Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("estado")
            If (_idprod = idprod And estado >= 0) Then

                Return True
            End If
        Next
        Return False
    End Function
    Public Sub _prEliminarFila()
        If (grdetalle.Row >= 0) Then
            If (grdetalle.RowCount >= 2) Then
                Dim estado As Integer = grdetalle.GetValue("estado")
                Dim pos As Integer = -1
                Dim lin As Integer = grdetalle.GetValue("icid")
                _fnObtenerFilaDetalle(pos, lin)
                If (estado = 0) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = -2

                End If
                If (estado = 1) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = -1
                End If
                grdetalle.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grdetalle.RootTable.Columns("estado"), Janus.Windows.GridEX.ConditionOperator.GreaterThanOrEqualTo, 0))

                grdetalle.Select()
                grdetalle.Col = 4
                grdetalle.Row = grdetalle.RowCount - 1
            End If
        End If


    End Sub
    Public Function _ValidarCampos() As Boolean
        If (cbConcepto.SelectedIndex < 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Seleccione un Concepto".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            cbConcepto.Focus()
            Return False

        End If
        If (cbAlmacenOrigen.SelectedIndex < 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Seleccione un Deposito".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            cbAlmacenOrigen.Focus()
            Return False
        End If
        If (cbConcepto.SelectedIndex >= 0) Then
            If (cbConcepto.Value = 6) Then
                If (cbDepositoDestino.SelectedIndex < 0) Then
                    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                    ToastNotification.Show(Me, "Por Favor Seleccione un Deposito Desitno".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                    cbDepositoDestino.Focus()
                    Return False
                End If
            End If
        End If
        If (grdetalle.RowCount <= 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Inserte un Detalle".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            grdetalle.Focus()

            Return False

        End If
        If (grdetalle.RowCount = 1) Then
            If (CType(grdetalle.DataSource, DataTable).Rows(0).Item("iccprod") = 0) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "Por Favor Inserte un Detalle".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                grdetalle.Focus()

                Return False
            End If
        End If
        Return True
    End Function

    Sub _prGuardarTraspaso()
        Dim numi As String = ""
        Dim res As Boolean = L_prMovimientoChoferGrabar(numi, tbFecha.Value.ToString("yyyy/MM/dd"), cbConcepto.Value, tbObservacion.Text, cbAlmacenOrigen.Value, cbDepositoDestino.Value, 0, CType(grdetalle.DataSource, DataTable))
        If res Then

            Dim numDestino As String = ""
            Dim resDestino As Boolean = L_prMovimientoChoferGrabar(numDestino, tbFecha.Value.ToString("yyyy/MM/dd"), 5, tbObservacion.Text, cbDepositoDestino.Value, cbAlmacenOrigen.Value, numi, CType(grdetalle.DataSource, DataTable))
            If resDestino Then

                _prCargarVenta()

                _Limpiar()
                Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
                ToastNotification.Show(Me, "Código de Movimiento ".ToUpper + tbCodigo.Text + " Grabado con Exito.".ToUpper,
                                          img, 2000,
                                          eToastGlowColor.Green,
                                          eToastPosition.TopCenter
                                          )
            End If

        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "El Movimiento no pudo ser insertado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        End If
    End Sub
    Public Sub _GuardarNuevo()
        'ByRef _ibid As String, _ibfdoc As String, _ibconcep As Integer, _ibobs As String, _almacen As Integer

        If (cbConcepto.Value = 6) Then
            _prGuardarTraspaso()
            Return

        End If
        Dim numi As String = ""
        Dim res As Boolean = L_prMovimientoChoferGrabar(numi, tbFecha.Value.ToString("yyyy/MM/dd"), cbConcepto.Value, tbObservacion.Text, cbAlmacenOrigen.Value, 0, 0, CType(grdetalle.DataSource, DataTable))
        If res Then

            _prCargarVenta()

            _Limpiar()
            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "Código de Movimiento ".ToUpper + tbCodigo.Text + " Grabado con Exito.".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter
                                      )
        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "El Movimiento no pudo ser insertado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        End If


    End Sub
    Private Sub _prGuardarModificado()
        Dim res As Boolean = L_prMovimientoModificar(tbCodigo.Text, tbFecha.Value.ToString("yyyy/MM/dd"), cbConcepto.Value, tbObservacion.Text, cbAlmacenOrigen.Value, CType(grdetalle.DataSource, DataTable))
        If res Then

            _prCargarVenta()

            _prSalir()

            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "Código de Movimiento ".ToUpper + tbCodigo.Text + " Modificado con Exito.".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter
                                      )

        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "La Venta no pudo ser Modificada".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

        End If
    End Sub
    Private Sub _prSalir()
        If btnGrabar.Enabled = True Then
            _prInhabiliitar()
            If grmovimiento.RowCount > 0 Then

                _prMostrarRegistro(0)

            End If
        Else

            _modulo.Select()
            Me.Close()

        End If
    End Sub
    Public Sub _prCargarIconELiminar()
        If (cbConcepto.Value <> 3) Then
            For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
                Dim Bin As New MemoryStream
                Dim img As New Bitmap(My.Resources.delete, 28, 28)
                img.Save(Bin, Imaging.ImageFormat.Png)
                CType(grdetalle.DataSource, DataTable).Rows(i).Item("img") = Bin.GetBuffer
                grdetalle.RootTable.Columns("img").Visible = True
            Next
        End If
    End Sub
    Public Sub _PrimerRegistro()
        Dim _MPos As Integer
        If grmovimiento.RowCount > 0 Then
            _MPos = 0
            ''   _prMostrarRegistro(_MPos)
            grmovimiento.Row = _MPos
        End If
    End Sub

    Public Sub InsertarProductosSinLote()
        Try
            Dim pos As Integer = -1
            grdetalle.Row = grdetalle.RowCount - 1
            _fnObtenerFilaDetalle(pos, grdetalle.GetValue("icid"))

            Dim existe As Boolean = _fnExisteProducto(grproducto.GetValue("yfnumi"))
            If ((pos >= 0) And (Not existe)) Then
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("iccprod") = grproducto.GetValue("yfnumi")
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("producto") = grproducto.GetValue("yfcdprod1")
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("stock") = grproducto.GetValue("stock")
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("Laboratorio") = grproducto.GetValue("Laboratorio")
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("Presentacion") = grproducto.GetValue("Presentacion")
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("yfcprod") = grproducto.GetValue("yfcprod")
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("iccant") = 1

                ''    _DesHabilitarProductos()

                _prAddDetalleVenta()

                _prCargarProductos()
                'grproducto.RemoveFilters()
                grproducto.Focus()
                grproducto.MoveTo(grproducto.FilterRow)
                grproducto.Col = 1
            Else
                If (existe) Then
                    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                    ToastNotification.Show(Me, "El producto ya existe en el detalle".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                    grproducto.RemoveFilters()
                    grproducto.Focus()
                    grproducto.MoveTo(grproducto.FilterRow)
                    grproducto.Col = 1
                End If

            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try

    End Sub
    Public Sub InsertarProductosConLote()

        'Dim pos As Integer = -1
        'grdetalle.Row = grdetalle.RowCount - 1
        '_fnObtenerFilaDetalle(pos, grdetalle.GetValue("icid"))
        'Dim posProducto As Integer = grproducto.Row
        'FilaSelectLote = CType(grproducto.DataSource, DataTable).Rows(posProducto)



        'If (grproducto.GetValue("stock") > 0) Then
        '    _prCargarLotesDeProductos(grproducto.GetValue("yfnumi"), grproducto.GetValue("yfcdprod1"))
        '    'FilaSelectLote = CType(grproducto.DataSource, DataTable).Rows(0)
        'Else
        '    Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
        '    ToastNotification.Show(Me, "El Producto: ".ToUpper + grproducto.GetValue("yfcdprod1") + " NO CUENTA CON STOCK DISPONIBLE", img, 5000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        '    FilaSelectLote = Nothing
        'End If

        Dim pos As Integer = -1
        grdetalle.Row = grdetalle.RowCount - 1
        _fnObtenerFilaDetalleProducto(pos, grproducto.GetValue("yfnumi"))
        Dim posProducto As Integer = grproducto.Row
        FilaSelectLote = CType(grproducto.DataSource, DataTable).Rows(pos)
        If (grproducto.GetValue("stock") > 0) Then
            _prCargarLotesDeProductos(grproducto.GetValue("yfnumi"), grproducto.GetValue("yfcdprod1"))
        Else
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "El Producto: ".ToUpper + grproducto.GetValue("yfcdprod1") + " NO CUENTA CON STOCK DISPONIBLE", img, 5000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            FilaSelectLote = Nothing
        End If
    End Sub
    Public Function _fnExisteProductoConLote(idprod As Integer, lote As String, fechaVenci As Date) As Boolean
        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _idprod As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("iccprod")
            Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("estado")
            '          a.tbnumi ,a.tbtv1numi ,a.tbty5prod ,b.yfcdprod1 as producto,a.tbest ,a.tbcmin ,a.tbumin ,Umin .ycdes3 as unidad,a.tbpbas ,a.tbptot ,a.tbobs ,
            'a.tbpcos,a.tblote ,a.tbfechaVenc , a.tbptot2, a.tbfact ,a.tbhact ,a.tbuact,1 as estado,Cast(null as Image) as img,
            'Cast (0 as decimal (18,2)) as stock
            Dim _LoteDetalle As String = CType(grdetalle.DataSource, DataTable).Rows(i).Item("iclot")
            Dim _FechaVencDetalle As Date = CType(grdetalle.DataSource, DataTable).Rows(i).Item("icfvenc")
            If (_idprod = idprod And estado >= 0 And lote = _LoteDetalle And fechaVenci = _FechaVencDetalle) Then

                Return True
            End If
        Next
        Return False
    End Function
    Public Sub _fnObtenerFilaDetalleProducto(ByRef pos As Integer, numi As Integer)
        For i As Integer = 0 To CType(grproducto.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _numi As Integer = CType(grproducto.DataSource, DataTable).Rows(i).Item("yfnumi")
            If (_numi = numi) Then
                pos = i
                Return
            End If
        Next

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
#End Region

#Region "Eventos Formulario"



    Private Sub F0_Movimiento_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _IniciarTodo()
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        _Limpiar()
        _prhabilitar()

        btnNuevo.Enabled = False
        btnModificar.Enabled = False
        btnEliminar.Enabled = False
        btnGrabar.Enabled = True
        PanelInferior.Enabled = False
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _prSalir()
    End Sub

    Private Sub grdetalle_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grdetalle.EditingCell
        If (_fnAccesible()) Then

            'Habilitar solo las columnas de Precio, %, Monto y Observación
            If (e.Column.Index = grdetalle.RootTable.Columns("iccant").Index) Then
                e.Cancel = False
            Else
                If ((e.Column.Index = grdetalle.RootTable.Columns("iclot").Index Or
                    e.Column.Index = grdetalle.RootTable.Columns("icfvenc").Index) And (cbConcepto.Value = 1) And
                Lote = True) Then
                    e.Cancel = False
                Else
                    e.Cancel = True
                End If
            End If


        Else
            e.Cancel = True
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

            If (grdetalle.Col = grdetalle.RootTable.Columns("iccant").Index) Then
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


    Private Sub grproducto_KeyDown(sender As Object, e As KeyEventArgs) Handles grproducto.KeyDown
        If (Not _fnAccesible()) Then
            Return
        End If
        If (e.KeyData = Keys.Enter) Then
            Dim f, c As Integer
            c = grproducto.Col
            f = grproducto.Row
            If (f >= 0) Then

                If (IsNothing(FilaSelectLote)) Then
                    ''''''''''''''''''''''''
                    If (Lote = True) Then
                        If (cbConcepto.Value = 1) Then ''' Aqui pregunto si es con lote y tambien si es una 
                            ''entrada debe solo insertar solo el producto y no seguir con los lotes 
                            InsertarProductosSinLote()

                        Else
                            InsertarProductosConLote()
                        End If

                    Else

                        InsertarProductosSinLote()
                    End If
                    '''''''''''''''
                Else

                    '      a.icid ,a.icibid ,a.iccprod ,b.yfcdprod1  as producto,a.iccant ,
                    'a.iclot ,a.icfvenc ,Cast(null as image ) as img,1 as estado,
                    '(Sum(inv.iccven )+a.iccant  ) as stock

                    'a.yfnumi  ,a.yfcdprod1  ,a.yfcdprod2,Sum(b.iccven ) as stock 
                    '_fnExisteProductoConLote()
                    'b.yfcdprod1 ,a.iclot ,a.icfven  ,Sum(a.iccven)as stock 
                    Dim pos As Integer = -1
                    grdetalle.Row = grdetalle.RowCount - 1
                    _fnObtenerFilaDetalle(pos, grdetalle.GetValue("icid"))
                    Dim numiProd As Integer = FilaSelectLote.Item("yfnumi")
                    Dim lote As String = grproducto.GetValue("iclot")
                    Dim FechaVenc As Date = grproducto.GetValue("icfven")
                    If (Not _fnExisteProductoConLote(numiProd, lote, FechaVenc)) Then
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("iccprod") = FilaSelectLote.Item("yfnumi")
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("producto") = FilaSelectLote.Item("yfcdprod1")
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("iccant") = 1

                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("stock") = grproducto.GetValue("stock")

                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("iclot") = grproducto.GetValue("iclot")
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("icfvenc") = grproducto.GetValue("icfven")

                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("Laboratorio") = FilaSelectLote.Item("Laboratorio")
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("Presentacion") = FilaSelectLote.Item("Presentacion")
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("yfcprod") = FilaSelectLote.Item("yfcprod")

                        FilaSelectLote = Nothing
                        _DesHabilitarProductos()

                    Else
                        Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                        ToastNotification.Show(Me, "El producto con el lote ya existe modifique su cantidad".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                    End If



                End If


            End If
        End If
        If e.KeyData = Keys.Escape Then
            CType(grproducto.DataSource, DataTable).Rows.Clear()

            _DesHabilitarProductos()
        End If
    End Sub

    Private Sub grdetalle_CellValueChanged(sender As Object, e As ColumnActionEventArgs) Handles grdetalle.CellValueChanged

        If (e.Column.Index = grdetalle.RootTable.Columns("iccant").Index) Then
            If (Not IsNumeric(grdetalle.GetValue("iccant")) Or grdetalle.GetValue("iccant").ToString = String.Empty) Then

                'grDetalle.GetRow(rowIndex).Cells("cant").Value = 1
                '  grDetalle.CurrentRow.Cells.Item("cant").Value = 1
                Dim lin As Integer = grdetalle.GetValue("icid")
                Dim pos As Integer = -1
                _fnObtenerFilaDetalle(pos, lin)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("iccant") = 1

                Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado")

                If (estado = 1) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = 2
                End If

            Else
                If (grdetalle.GetValue("iccant") > 0) Then
                    Dim lin As Integer = grdetalle.GetValue("icid")
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, lin)
                    Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado")

                    If (estado = 1) Then
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = 2
                    End If

                Else
                    Dim lin As Integer = grdetalle.GetValue("icid")
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, lin)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("iccant") = 1
                    Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado")

                    If (estado = 1) Then
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = 2
                    End If

                End If
            End If
        End If
    End Sub

    Private Sub grdetalle_CellEdited(sender As Object, e As ColumnActionEventArgs) Handles grdetalle.CellEdited
        If (e.Column.Index = grdetalle.RootTable.Columns("iccant").Index) Then
            If (Not IsNumeric(grdetalle.GetValue("iccant")) Or grdetalle.GetValue("iccant").ToString = String.Empty) Then

                grdetalle.SetValue("iccant", 1)
            Else
                If (grdetalle.GetValue("iccant") > 0) Then
                    Dim stock As Double = grdetalle.GetValue("stock")
                    If (grdetalle.GetValue("iccant") > stock And cbConcepto.Value <> 1) Then
                        Dim lin As Integer = grdetalle.GetValue("icid")
                        Dim pos As Integer = -1
                        _fnObtenerFilaDetalle(pos, lin)
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("iccant") = stock
                        grdetalle.SetValue("iccant", stock)
                        Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                        ToastNotification.Show(Me, "La cantidad que se quiere sacar es mayor a la que existe en el stock solo puede Sacar : ".ToUpper + Str(stock).Trim,
                          img,
                          5000,
                          eToastGlowColor.Blue,
                          eToastPosition.BottomLeft)
                    End If
                Else

                    grdetalle.SetValue("iccant", 1)

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
        If (grmovimiento.RowCount > 0) Then
            _prhabilitar()
            btnNuevo.Enabled = False
            btnModificar.Enabled = False
            btnEliminar.Enabled = False
            btnGrabar.Enabled = True

            PanelInferior.Enabled = False
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
            Dim res As Boolean = L_prMovimientoEliminar(tbCodigo.Text)
            If res Then


                Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)

                ToastNotification.Show(Me, "Código de Movimiento ".ToUpper + tbCodigo.Text + " eliminado con Exito.".ToUpper,
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

    Private Sub grmovimiento_SelectionChanged(sender As Object, e As EventArgs) Handles grmovimiento.SelectionChanged
        If (grmovimiento.RowCount >= 0 And grmovimiento.Row >= 0) Then

            _prMostrarRegistro(grmovimiento.Row)
        End If
    End Sub

    Private Sub btnSiguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click
        Dim _pos As Integer = grmovimiento.Row
        If _pos < grmovimiento.RowCount - 1 Then
            _pos = grmovimiento.Row + 1
            '' _prMostrarRegistro(_pos)
            grmovimiento.Row = _pos
        End If
    End Sub

    Private Sub btnPrimero_Click(sender As Object, e As EventArgs) Handles btnPrimero.Click
        _PrimerRegistro()
    End Sub

    Private Sub btnUltimo_Click(sender As Object, e As EventArgs) Handles btnUltimo.Click
        Dim _pos As Integer = grmovimiento.Row
        If grmovimiento.RowCount > 0 Then
            _pos = grmovimiento.RowCount - 1
            ''  _prMostrarRegistro(_pos)
            grmovimiento.Row = _pos
        End If
    End Sub

    Private Sub btnAnterior_Click(sender As Object, e As EventArgs) Handles btnAnterior.Click
        Dim _MPos As Integer = grmovimiento.Row
        If _MPos > 0 And grmovimiento.RowCount > 0 Then
            _MPos = _MPos - 1
            ''  _prMostrarRegistro(_MPos)
            grmovimiento.Row = _MPos
        End If
    End Sub

    Private Sub grdetalle_Enter(sender As Object, e As EventArgs) Handles grdetalle.Enter
        If (grdetalle.RowCount > 0) Then
            grdetalle.Select()
            grdetalle.Col = 3
            grdetalle.Row = 0
        End If
    End Sub

    Private Sub grmovimiento_KeyDown(sender As Object, e As KeyEventArgs) Handles grmovimiento.KeyDown
        If e.KeyData = Keys.Enter Then
            MSuperTabControl.SelectedTabIndex = 0
            grdetalle.Focus()

        End If
    End Sub

    Private Sub cbAlmacen_KeyDown(sender As Object, e As KeyEventArgs) Handles cbAlmacenOrigen.KeyDown
        If (_fnAccesible()) Then
            If e.KeyData = Keys.Enter Then
                grdetalle.Focus()
                'grdetalle.Select()
                grdetalle.Col = 2
                grdetalle.Row = 0
            End If
        End If
    End Sub

    Private Sub cbConcepto_ValueChanged(sender As Object, e As EventArgs) Handles cbConcepto.ValueChanged


        If (cbConcepto.SelectedIndex >= 0) Then
            If (cbConcepto.Value = 6) Then ''''Movimiento 6=Traspaso Salida
                If (CType(cbAlmacenOrigen.DataSource, DataTable).Rows.Count > 1) Then
                    lbDepositoDestino.Visible = True
                    cbDepositoDestino.Visible = True
                    lbDepositoOrigen.Text = "Deposito Origen"
                    lbDepositoDestino.Text = "Deposito Destino"
                    cbDepositoDestino.SelectedIndex = 1
                    If (Not _fnAccesible()) Then
                        btnModificar.Enabled = False
                    End If
                Else
                    lbDepositoDestino.Visible = False
                    cbDepositoDestino.Visible = False
                    lbDepositoOrigen.Text = "Deposito:"
                    If (Not _fnAccesible()) Then
                        btnModificar.Enabled = True
                    End If
                End If
            Else
                btnModificar.Enabled = True
                lbDepositoDestino.Visible = False
                cbDepositoDestino.Visible = False
                lbDepositoOrigen.Text = "Deposito:"

            End If
            If (_fnAccesible() And tbCodigo.Text = String.Empty) Then
                CType(grdetalle.DataSource, DataTable).Rows.Clear()
                _prAddDetalleVenta()
                _DesHabilitarProductos()
            End If
        End If
    End Sub

    Private Sub cbAlmacenOrigen_ValueChanged(sender As Object, e As EventArgs) Handles cbAlmacenOrigen.ValueChanged
        If (_fnAccesible() And tbCodigo.Text = String.Empty) Then
            CType(grdetalle.DataSource, DataTable).Rows.Clear()
            _prAddDetalleVenta()
            _DesHabilitarProductos()

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

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        P_GenerarReporte()
    End Sub
    Private Sub P_GenerarReporte()
        Try
            Dim dtDetalle As DataTable = L_fnDetalleMovimiento(tbCodigo.Text)

            If Not IsNothing(P_Global.Visualizador) Then
                P_Global.Visualizador.Close()
            End If

            P_Global.Visualizador = New Visualizador
            Dim objrep As New R_Movimiento

            objrep.SetDataSource(dtDetalle)
            objrep.SetParameterValue("idMovimiento", tbCodigo.Text)
            objrep.SetParameterValue("fecha", tbFecha.Text)
            objrep.SetParameterValue("concepto", cbConcepto.Text)
            objrep.SetParameterValue("depositoOrigen", cbAlmacenOrigen.Text)
            objrep.SetParameterValue("depositoDestino", cbDepositoDestino.Text)
            objrep.SetParameterValue("obs", tbObservacion.Text)
            objrep.SetParameterValue("usuario", L_Usuario)

            P_Global.Visualizador.CrGeneral.ReportSource = objrep
            P_Global.Visualizador.Show()
            P_Global.Visualizador.BringToFront()
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub
#End Region
End Class