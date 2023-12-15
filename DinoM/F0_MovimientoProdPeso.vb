
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

Public Class F0_MovimientoProdPeso
    Dim _Inter As Integer = 0
#Region "Variables Globales"
    Public _nameButton As String
    Public _tab As SuperTabItem
    Dim Lote As Boolean = False
    Public _modulo As SideNavItem
    Dim Table_producto As DataTable
    Dim FilaSelectLote As DataRow = Nothing
    Public Modificar As Boolean
    Public DesdeModulo As Boolean = False
    Public dtCompra As DataTable
    Public Observ As String
    Public prog As Integer ''1=Compra, 2=Venta, 3=Mov Ingreso, 4=Mov Salida

#End Region
#Region "Metodos Privados"
    Public Sub _IniciarTodo()
        MSuperTabControl.SelectedTabIndex = 0
        '' L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        _prValidarLote()
        btnGrabar.Enabled = False ''Para controlar que no cambie el motivo según lo seleccionado
        'Me.WindowState = FormWindowState.Maximized
        _prCargarComboLibreriaConcepto(cbConcepto)
        _prCargarComboLibreriaMotivo(cbMotivo, 10, 1)
        _prCargarComboLibreriaDeposito(cbAlmacenOrigen)
        _prCargarComboLibreriaDeposito(cbDepositoDestino)
        _prCargarMovimiento()
        _prInhabiliitar()


        'Dim blah As New Bitmap(New Bitmap(My.Resources.compra), 20, 20)
        'Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        'Me.Icon = ico
        _prAsignarPermisos()
        Me.Text = "MOVIMIENTO DE PRODUCTOS POR PESO"
        tbObservacion.MaxLength = 300

        If DesdeModulo = True Then
            btnNuevo.PerformClick()
            _prLlenarDetalle(dtCompra, prog)
            tbObservacion.Text = Observ
            cbConcepto.ReadOnly = True
            ''cbConcepto.Value = prog
        End If
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
    Public Sub _prLlenarDetalle(dt As DataTable, prog As Integer)
        If prog = 1 Then ''Para llenar detalle de Compra
            For i = 0 To dt.Rows.Count - 1
                CType(grdetalle.DataSource, DataTable).Rows(i).Item("igcprod") = dt.Rows(i).Item("cbty5prod")
                CType(grdetalle.DataSource, DataTable).Rows(i).Item("producto") = dt.Rows(i).Item("producto")
                CType(grdetalle.DataSource, DataTable).Rows(i).Item("yfcprod") = dt.Rows(i).Item("yfcprod")
                CType(grdetalle.DataSource, DataTable).Rows(i).Item("igcant") = 1

                _prAddDetalleVenta()
            Next
        ElseIf prog = 3 Or prog = 4 Then ''Para llenar detalle de Moviento Entrada y Salida
            For i = 0 To dt.Rows.Count - 1
                Dim dtStock = L_fnVerificarStockTI003(dt.Rows(i).Item("iccprod"))
                CType(grdetalle.DataSource, DataTable).Rows(i).Item("igcprod") = dt.Rows(i).Item("iccprod")
                CType(grdetalle.DataSource, DataTable).Rows(i).Item("producto") = dt.Rows(i).Item("producto")
                CType(grdetalle.DataSource, DataTable).Rows(i).Item("yfcprod") = dt.Rows(i).Item("yfcprod")
                CType(grdetalle.DataSource, DataTable).Rows(i).Item("igcant") = 1
                CType(grdetalle.DataSource, DataTable).Rows(i).Item("stock") = dtStock.Rows(0).Item("ihcven")

                _prAddDetalleVenta()
            Next
        ElseIf prog = 2 Then ''Para llenar detalle de Venta
            For i = 0 To dt.Rows.Count - 1
                Dim dtStock = L_fnVerificarStockTI003(dt.Rows(i).Item("tbty5prod"))
                CType(grdetalle.DataSource, DataTable).Rows(i).Item("igcprod") = dt.Rows(i).Item("tbty5prod")
                CType(grdetalle.DataSource, DataTable).Rows(i).Item("producto") = dt.Rows(i).Item("producto")
                CType(grdetalle.DataSource, DataTable).Rows(i).Item("yfcprod") = dt.Rows(i).Item("codigo")
                CType(grdetalle.DataSource, DataTable).Rows(i).Item("igcant") = 1
                CType(grdetalle.DataSource, DataTable).Rows(i).Item("stock") = dtStock.Rows(0).Item("ihcven")

                _prAddDetalleVenta()
            Next
        End If



    End Sub
    Private Sub _prCargarComboLibreriaDeposito(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnListarDepositosTI003()
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("aanumi").Width = 60
            .DropDownList.Columns("aanumi").Caption = "COD"
            .DropDownList.Columns.Add("aabdes").Width = 300
            .DropDownList.Columns("aabdes").Caption = "ALMACÉN"
            .ValueMember = "aanumi"
            .DisplayMember = "aabdes"
            .DataSource = dt
            .Refresh()
        End With
    End Sub
    Private Sub _prCargarComboLibreriaConcepto(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_prMovimientoConceptoTI003()

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
    Private Sub _prCargarComboLibreriaMotivo(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo, cod1 As String, cod2 As String)
        Dim dt As New DataTable
        dt = L_prLibreriaClienteLGeneral(cod1, cod2)
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("yccod3").Width = 70
            .DropDownList.Columns("yccod3").Caption = "COD"
            .DropDownList.Columns.Add("ycdes3").Width = 400
            .DropDownList.Columns("ycdes3").Caption = "DESCRIPCIÓN"
            .ValueMember = "yccod3"
            .DisplayMember = "ycdes3"
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
        btnGrabar.Enabled = False
        btnNuevo.Enabled = True
        btnEliminar.Enabled = True
        grmovimiento.Enabled = True

        tbCodigo.ReadOnly = True
        cbConcepto.ReadOnly = True
        cbMotivo.ReadOnly = True
        tbObservacion.ReadOnly = True
        tbFecha.IsInputReadOnly = True
        tbFecha.Enabled = False
        cbAlmacenOrigen.ReadOnly = True
        cbDepositoDestino.ReadOnly = True
        ''''''''''

        If (cbConcepto.Value = 6) Then ''''Movimiento 6=Traspaso Salida
            btnModificar.Enabled = False
        Else
            btnModificar.Enabled = True
        End If

        grdetalle.RootTable.Columns("img").Visible = False

        If (GPanelProductos.Visible = True) Then
            _DesHabilitarProductos()
        End If

        PanelInferior.Enabled = True
        FilaSelectLote = Nothing
    End Sub
    Private Sub _prhabilitar()

        tbObservacion.ReadOnly = False
        tbFecha.IsInputReadOnly = False
        tbFecha.Enabled = True
        cbAlmacenOrigen.ReadOnly = False
        cbDepositoDestino.ReadOnly = False
        grmovimiento.Enabled = False

        If (tbCodigo.Text.Length > 0) Then
            cbAlmacenOrigen.ReadOnly = True
            cbConcepto.ReadOnly = True
            cbMotivo.ReadOnly = True
        Else
            cbAlmacenOrigen.ReadOnly = False
            cbConcepto.ReadOnly = False
            cbMotivo.ReadOnly = False

        End If
        btnGrabar.Enabled = True
    End Sub
    Public Sub _prFiltrar()
        'cargo el buscador
        Dim _Mpos As Integer
        _prCargarMovimiento()
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

        If DesdeModulo = False Then
            If (CType(cbAlmacenOrigen.DataSource, DataTable).Rows.Count > 0) Then
                cbAlmacenOrigen.SelectedIndex = 0
            End If

            If (CType(cbConcepto.DataSource, DataTable).Rows.Count > 0) Then
                cbConcepto.SelectedIndex = 0
            End If
        Else
            If prog = 1 Or prog = 3 Then
                cbConcepto.Value = 1
            ElseIf prog = 2 Or prog = 4 Then
                cbConcepto.Value = 2
            End If
        End If

        cbMotivo.SelectedIndex = -1

        _prAddDetalleVenta()
        cbConcepto.Focus()
        FilaSelectLote = Nothing

        If grproducto.RowCount > 0 Then
            CType(grproducto.DataSource, DataTable).Rows.Clear()
        End If

    End Sub
    Public Sub _prMostrarRegistro(_N As Integer)
        With grmovimiento
            tbCodigo.Text = .GetValue("ifid")
            tbFecha.Value = .GetValue("iffdoc")
            cbConcepto.Value = .GetValue("ifconcep")
            tbObservacion.Text = .GetValue("ifobs")
            lbFecha.Text = CType(.GetValue("iffact"), Date).ToString("dd/MM/yyyy")
            lbHora.Text = .GetValue("ifhact").ToString
            lbUsuario.Text = .GetValue("ifuact").ToString
            cbAlmacenOrigen.Value = .GetValue("ifalm")
            cbDepositoDestino.Value = IIf(IsDBNull(.GetValue("ifdepdest")), 0, .GetValue("ifdepdest"))
        End With

        'If (cbConcepto.Value = 6) Then ''''Movimiento 6=Traspaso Salida Que en este caso no existe
        '    btnModificar.Enabled = False
        'Else
        '    btnModificar.Enabled = True
        'End If

        _prCargarDetalleVenta(tbCodigo.Text)
        LblPaginacion.Text = Str(grmovimiento.Row + 1) + "/" + grmovimiento.RowCount.ToString

    End Sub

    Private Sub _prCargarDetalleVenta(_numi As String)
        Dim dt As New DataTable
        dt = L_fnDetalleMovimientoTI003(_numi)
        grdetalle.DataSource = dt
        grdetalle.RetrieveStructure()
        grdetalle.AlternatingColors = True

        With grdetalle.RootTable.Columns("igid")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("igcprod")
            .Width = 120
            .Caption = "COD. DYNASYS"
            .Visible = True
        End With
        With grdetalle.RootTable.Columns("igibid")
            .Width = 90
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("yfcprod")
            .Width = 140
            .Caption = "COD. DELTA"
            .Visible = True
        End With
        With grdetalle.RootTable.Columns("producto")
            .Caption = "PRODUCTOS"
            .Width = 420
            .Visible = True
        End With
        With grdetalle.RootTable.Columns("Laboratorio")
            .Caption = "CALIBRE-GRAMAJE"
            .Width = 200
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("Presentacion")
            .Caption = "PROVEEDOR"
            .Width = 150
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("igcant")
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
            With grdetalle.RootTable.Columns("iglot")
                .Width = 120
                .Caption = "lote".ToUpper
                .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
                .Visible = True
            End With
            With grdetalle.RootTable.Columns("igfvenc")
                .Width = 120
                .Caption = "FECHA VENC.".ToUpper
                .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
                .FormatString = "yyyy/MM/dd"
                .Visible = True
            End With
        Else
            With grdetalle.RootTable.Columns("iglot")
                .Width = 120
                .Caption = "lote".ToUpper
                .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
                .Visible = False
            End With
            With grdetalle.RootTable.Columns("igfvenc")
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

            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .FilterMode = FilterMode.Automatic

            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
            .BoundMode = Janus.Data.BoundMode.Bound
            .RowHeaders = InheritableBoolean.True

            .RecordNavigator = True
            .RecordNavigatorText = "Productos"
        End With
    End Sub

    Private Sub _prCargarMovimiento()
        Dim dt As New DataTable
        dt = L_fnGeneralMovimientoTI003(IIf(swMostrar.Value = True, 1, 0))
        grmovimiento.DataSource = dt
        grmovimiento.RetrieveStructure()
        grmovimiento.AlternatingColors = True

        With grmovimiento.RootTable.Columns("ifid")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = True
        End With
        With grmovimiento.RootTable.Columns("iffdoc")
            .Width = 90
            .Visible = True
            .Caption = "FECHA"
            .FormatString = "yyyy/MM/dd"
        End With
        With grmovimiento.RootTable.Columns("ifconcep")
            .Width = 90
            .Visible = False
        End With
        With grmovimiento.RootTable.Columns("concepto")
            .Width = 160
            .Visible = True
            .Caption = "CONCEPTO"
        End With
        With grmovimiento.RootTable.Columns("ifobs")
            .Width = 800
            .Visible = True
            .Caption = "observacion".ToUpper
        End With
        With grmovimiento.RootTable.Columns("ifest")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grmovimiento.RootTable.Columns("ifalm")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grmovimiento.RootTable.Columns("ifiddc")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grmovimiento.RootTable.Columns("iffact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grmovimiento.RootTable.Columns("ifhact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grmovimiento.RootTable.Columns("ifuact")
            .Width = 100
            .Caption = "USUARIO"
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
        End With
        With grmovimiento.RootTable.Columns("ifdepdest")
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
        Dim _dtDetalle As DataTable = CType(grdetalle.DataSource, DataTable)
        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
            Dim sum As Integer = 0
            Dim codProducto As Integer = dt.Rows(i).Item("yfnumi")
            For j As Integer = 0 To _dtDetalle.Rows.Count - 1 Step 1

                Dim estado As Integer = _dtDetalle.Rows(j).Item("estado")
                If (estado = 0) Then
                    If (codProducto = _dtDetalle.Rows(j).Item("igcprod")) Then
                        sum = sum + _dtDetalle.Rows(j).Item("igcant")
                    End If
                End If
            Next
            dt.Rows(i).Item("stock") = dt.Rows(i).Item("stock") - sum
        Next

    End Sub
    Private Sub _prCargarProductos()
        Dim dt As New DataTable

        If (Lote = True And cbConcepto.Value <> 1) Then
            dt = L_prMovimientoListarProductosConLoteTI003(cbAlmacenOrigen.Value)  ''1=Almacen
            actualizarSaldoSinLote(dt)
        Else
            dt = L_prMovimientoListarProductosTI003(CType(grdetalle.DataSource, DataTable), cbAlmacenOrigen.Value)  ''1=Almacen
        End If

        If (grproducto.RowCount > 0 And cbConcepto.Value = 1) Then
            grproducto.DataSource = dt
            Return
        End If
        grproducto.DataSource = dt
        grproducto.RetrieveStructure()
        grproducto.AlternatingColors = True
        With grproducto.RootTable.Columns("yfnumi")
            .Width = 100
            .Caption = "COD. DYNASYS"
            .Visible = True

        End With
        With grproducto.RootTable.Columns("yfcprod")
            .Width = 120
            .Caption = "COD. DELTA"
            .Visible = True

        End With
        With grproducto.RootTable.Columns("yfcbarra")
            .Width = 120
            .Caption = "COD. BARRAS"
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
            .Caption = "CALIBRE-GRAMAJE"
        End With
        With grproducto.RootTable.Columns("Presentacion")
            .Width = 120
            .Visible = True
            .Caption = "PROVEEDOR"
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

            .RecordNavigator = True
            .RecordNavigatorText = "Productos"
        End With
        _prAplicarCondiccionJanusSinLote()
    End Sub
    Public Sub actualizarSaldo(ByRef dt As DataTable, CodProducto As Integer)

        Dim _detalle As DataTable = CType(grdetalle.DataSource, DataTable)

        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
            Dim lote As String = dt.Rows(i).Item("ihlot")
            Dim FechaVenc As Date = dt.Rows(i).Item("ihfven")
            Dim sum As Integer = 0
            For j As Integer = 0 To _detalle.Rows.Count - 1
                Dim estado As Integer = _detalle.Rows(j).Item("estado")
                If (estado = 0) Then
                    If (lote = _detalle.Rows(j).Item("iglot") And
                        FechaVenc = _detalle.Rows(j).Item("igfvenc") And CodProducto = _detalle.Rows(j).Item("igcprod")) Then
                        sum = sum + _detalle.Rows(j).Item("igcant")
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
        dt = L_fnListarLotesPorProductoMovimientoTI003(cbAlmacenOrigen.Value, CodProducto)  ''1=Almacen
        actualizarSaldo(dt, CodProducto)
        grproducto.DataSource = dt
        grproducto.RetrieveStructure()
        grproducto.AlternatingColors = True
        With grproducto.RootTable.Columns("yfcdprod1")
            .Width = 150
            .Visible = False
        End With
        With grproducto.RootTable.Columns("ihlot")
            .Width = 150
            .Caption = "LOTE"
            .Visible = True
        End With
        With grproducto.RootTable.Columns("ihfven")
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
        fc2 = New GridEXFormatCondition(grproducto.RootTable.Columns("ihfven"), ConditionOperator.LessThanOrEqualTo, Now.Date)
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
        Dim Bin As New MemoryStream
        Dim img As New Bitmap(My.Resources.delete, 28, 28)
        img.Save(Bin, Imaging.ImageFormat.Png)
        CType(grdetalle.DataSource, DataTable).Rows.Add(_fnSiguienteNumi() + 1, 0, 0, "", "", "", "", 0, "20500101", CDate("2050/01/01"), Bin.GetBuffer, 0, 0)
    End Sub
    Public Function _fnSiguienteNumi()
        Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)
        Dim mayor As Integer = 0
        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
            Dim data As Integer = IIf(IsDBNull(CType(grdetalle.DataSource, DataTable).Rows(i).Item("igid")), 0, CType(grdetalle.DataSource, DataTable).Rows(i).Item("igid"))
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
            Dim _numi As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("igid")
            If (_numi = numi) Then
                pos = i
                Return
            End If
        Next
    End Sub

    Public Function _fnExisteProducto(idprod As Integer) As Boolean
        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _idprod As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("igcprod")
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
                Dim lin As Integer = grdetalle.GetValue("igid")
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
        If cbConcepto.Value = 6 Then ''Concepto Traspaso
            If (cbMotivo.SelectedIndex < 0) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "Por Favor Seleccione un Motivo".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                cbMotivo.Focus()
                Return False
            End If
        End If

        If (cbAlmacenOrigen.SelectedIndex < 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Seleccione un Almacén".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            cbAlmacenOrigen.Focus()
            Return False
        End If
        If (tbObservacion.Text = String.Empty) Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor debe colocar una Observación".ToUpper, img, 2500, eToastGlowColor.Red, eToastPosition.BottomCenter)
            tbObservacion.Focus()
            Return False
        End If
        If (cbConcepto.SelectedIndex >= 0) Then
            If (cbConcepto.Value = 6) Then
                If (cbDepositoDestino.SelectedIndex < 0) Then
                    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                    ToastNotification.Show(Me, "Por Favor Seleccione un Almacén Destino".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
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
            If (CType(grdetalle.DataSource, DataTable).Rows(0).Item("igcprod") = 0) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "Por Favor Inserte un Detalle".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                grdetalle.Focus()

                Return False
            End If
        End If

        ''Para validar nuevamente Stock
        If Modificar = False Then

            If cbConcepto.Value = 2 Or cbConcepto.Value = 6 Then 'Conceptos 2=Salida y 6=Traspaso Salida 

                For i = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1
                    Dim CodPro As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("igcprod")
                    Dim dt = L_prMovimientoListarUnProductoTI003(cbAlmacenOrigen.Value, CodPro)
                    If dt.Rows.Count > 0 Then

                        Dim stock As Double = dt.Rows(0).Item("stock")

                        If stock = 0 Then
                            Dim img1 As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                            ToastNotification.Show(Me, "El Producto: ".ToUpper + CodPro.ToString +
                            " tiene stock : ".ToUpper + Str(stock).Trim + " no se puede hacer salidas de este producto.".ToUpper,
                              img1,
                              5000,
                              eToastGlowColor.Blue,
                              eToastPosition.TopCenter)

                            Return False
                        Else
                            If (CType(grdetalle.DataSource, DataTable).Rows(i).Item("estado") >= 0 And CType(grdetalle.DataSource, DataTable).Rows(i).Item("igcant") > stock) Then
                                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                                ToastNotification.Show(Me, "La cantidad que se quiere sacar en el Producto: ".ToUpper + CodPro.ToString +
                                " es mayor a la que existe en el stock solo puede Sacar : ".ToUpper + Str(stock).Trim,
                                  img,
                                  5000,
                                  eToastGlowColor.Blue,
                                  eToastPosition.TopCenter)

                                Return False

                            Else

                            End If
                        End If
                    End If
                Next

            End If
        End If

        Return True
    End Function

    Public Sub _GuardarNuevo()
        If (cbConcepto.Value = 6) Then
            '_prGuardarTraspaso()
            Return
        End If
        Dim numi As String = ""
        Dim res As Boolean = L_prMovimientoChoferGrabarTI003(numi, tbFecha.Value.ToString("yyyy/MM/dd"), cbConcepto.Value, tbObservacion.Text.Trim, cbAlmacenOrigen.Value, 0, 0, CType(grdetalle.DataSource, DataTable), 0)
        If res Then
            _prCargarMovimiento()
            _Limpiar()
            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "Código de Movimiento ".ToUpper + tbCodigo.Text + " Grabado con éxito.".ToUpper,
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
        Dim res As Boolean = L_prMovimientoModificarTI003(tbCodigo.Text, tbFecha.Value.ToString("yyyy/MM/dd"), cbConcepto.Value, tbObservacion.Text, cbAlmacenOrigen.Value, CType(grdetalle.DataSource, DataTable))
        If res Then
            _prCargarMovimiento()
            Modificar = False
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
            _fnObtenerFilaDetalle(pos, grdetalle.GetValue("igid"))

            Dim existe As Boolean = _fnExisteProducto(grproducto.GetValue("yfnumi"))
            If ((pos >= 0) And (Not existe)) Then
                If (cbConcepto.Value = 2 Or cbConcepto.Value = 6) And grproducto.GetValue("stock") = 0 Then
                    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                    ToastNotification.Show(Me, "No puede elegir un producto que tiene stock 0".ToUpper, img, 2500, eToastGlowColor.Red, eToastPosition.BottomCenter)
                Else

                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("igcprod") = grproducto.GetValue("yfnumi")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("producto") = grproducto.GetValue("yfcdprod1")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("stock") = grproducto.GetValue("stock")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("Laboratorio") = grproducto.GetValue("Laboratorio")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("Presentacion") = grproducto.GetValue("Presentacion")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("yfcprod") = grproducto.GetValue("yfcprod")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("igcant") = 1

                    _prAddDetalleVenta()
                    _prCargarProductos()

                    grproducto.Focus()
                    grproducto.MoveTo(grproducto.FilterRow)
                    grproducto.Col = 1

                End If

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
            Dim _idprod As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("igcprod")
            Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("estado")

            Dim _LoteDetalle As String = CType(grdetalle.DataSource, DataTable).Rows(i).Item("iglot")
            Dim _FechaVencDetalle As Date = CType(grdetalle.DataSource, DataTable).Rows(i).Item("igfvenc")
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

        Modificar = False
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _prSalir()
    End Sub

    Private Sub grdetalle_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grdetalle.EditingCell
        If (_fnAccesible()) Then

            'Habilitar solo las columnas de Precio, %, Monto y Observación
            If (e.Column.Index = grdetalle.RootTable.Columns("igcant").Index) Then
                e.Cancel = False
            Else
                If ((e.Column.Index = grdetalle.RootTable.Columns("iglot").Index Or
                    e.Column.Index = grdetalle.RootTable.Columns("igfvenc").Index) And (cbConcepto.Value = 1) And
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

                Else
                    Dim pos As Integer = -1
                    grdetalle.Row = grdetalle.RowCount - 1
                    _fnObtenerFilaDetalle(pos, grdetalle.GetValue("igid"))
                    Dim numiProd As Integer = FilaSelectLote.Item("yfnumi")
                    Dim lote As String = grproducto.GetValue("ihlot")
                    Dim FechaVenc As Date = grproducto.GetValue("ihfven")
                    If (Not _fnExisteProductoConLote(numiProd, lote, FechaVenc)) Then
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("igcprod") = FilaSelectLote.Item("yfnumi")
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("producto") = FilaSelectLote.Item("yfcdprod1")
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("igcant") = 1

                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("stock") = grproducto.GetValue("stock")

                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("iglot") = grproducto.GetValue("ihlot")
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("igfvenc") = grproducto.GetValue("ihfven")

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

        If (e.Column.Index = grdetalle.RootTable.Columns("igcant").Index) Then
            If (Not IsNumeric(grdetalle.GetValue("igcant")) Or grdetalle.GetValue("igcant").ToString = String.Empty) Then

                Dim lin As Integer = grdetalle.GetValue("igid")
                Dim pos As Integer = -1
                _fnObtenerFilaDetalle(pos, lin)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("igcant") = 1

                Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado")

                If (estado = 1) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = 2
                End If

            Else
                If (grdetalle.GetValue("igcant") > 0) Then
                    Dim lin As Integer = grdetalle.GetValue("igid")
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, lin)
                    Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado")

                    If (estado = 1) Then
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = 2
                    End If

                Else
                    Dim lin As Integer = grdetalle.GetValue("igid")
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, lin)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("igcant") = 1
                    Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado")

                    If (estado = 1) Then
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = 2
                    End If

                End If
            End If
        End If
    End Sub

    Private Sub grdetalle_CellEdited(sender As Object, e As ColumnActionEventArgs) Handles grdetalle.CellEdited
        If (e.Column.Index = grdetalle.RootTable.Columns("igcant").Index) Then
            If (Not IsNumeric(grdetalle.GetValue("igcant")) Or grdetalle.GetValue("igcant").ToString = String.Empty) Then

                grdetalle.SetValue("igcant", 1)
            Else
                If (grdetalle.GetValue("igcant") > 0) Then
                    Dim stock As Double = grdetalle.GetValue("stock")
                    If (grdetalle.GetValue("igcant") > stock And cbConcepto.Value <> 1) Then
                        Dim lin As Integer = grdetalle.GetValue("igid")
                        Dim pos As Integer = -1
                        _fnObtenerFilaDetalle(pos, lin)
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("igcant") = stock
                        grdetalle.SetValue("igcant", stock)
                        Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                        ToastNotification.Show(Me, "La cantidad que se quiere sacar es mayor a la que existe en el stock solo puede Sacar : ".ToUpper + Str(stock).Trim,
                          img,
                          5000,
                          eToastGlowColor.Blue,
                          eToastPosition.BottomLeft)
                    End If
                Else
                    grdetalle.SetValue("igcant", 1)

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
            If gi_userRol <> 1 Then
                Dim Diferencia As Integer = DateDiff(DateInterval.Day, tbFecha.Value, Now.Date)
                If Diferencia > 14 Then
                    Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)
                    ToastNotification.Show(Me, "No se puede modificar este registro".ToUpper,
                                           img, 2500, eToastGlowColor.Red, eToastPosition.TopCenter)
                    Exit Sub
                End If
            End If

            _prhabilitar()
            btnNuevo.Enabled = False
            btnModificar.Enabled = False
            btnEliminar.Enabled = False
            btnGrabar.Enabled = True

            PanelInferior.Enabled = False
            _prCargarIconELiminar()

            Modificar = True
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
            Dim res As Boolean = L_prMovimientoEliminarTI003(tbCodigo.Text)
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
                    lbDepositoOrigen.Text = "Almacén Origen"
                    lbDepositoDestino.Text = "Almacén Destino"
                    cbDepositoDestino.SelectedIndex = 1
                    'If (Not _fnAccesible()) Then
                    '    btnModificar.Enabled = False
                    'End If
                    btnModificar.Enabled = False
                    lbMotivo.Visible = True
                    cbMotivo.Visible = True

                Else
                    lbDepositoDestino.Visible = False
                    cbDepositoDestino.Visible = False
                    lbDepositoOrigen.Text = "Almacén:"
                    If (Not _fnAccesible()) Then
                        btnModificar.Enabled = True
                    End If
                    lbMotivo.Visible = False
                    cbMotivo.Visible = False
                End If
            Else
                btnModificar.Enabled = True
                lbDepositoDestino.Visible = False
                cbDepositoDestino.Visible = False
                lbDepositoOrigen.Text = "Almacén:"

                lbMotivo.Visible = False
                cbMotivo.Visible = False

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
            Dim dtDetalle As DataTable = L_fnDetalleMovimientoTI003(tbCodigo.Text)

            If Not IsNothing(P_Global.Visualizador) Then
                P_Global.Visualizador.Close()
            End If

            P_Global.Visualizador = New Visualizador
            Dim objrep As New R_MovimientoProdXpeso

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

    Private Sub swMostrar_ValueChanged(sender As Object, e As EventArgs) Handles swMostrar.ValueChanged
        _prCargarMovimiento()
    End Sub

    Private Sub cbMotivo_ValueChanged(sender As Object, e As EventArgs) Handles cbMotivo.ValueChanged
        If btnGrabar.Enabled = True Then
            If (cbConcepto.Value = 6) Then ''''Movimiento 6=Traspaso Salida
                If cbMotivo.Text <> String.Empty Then
                    tbObservacion.Text = cbMotivo.Text
                End If

            End If
        End If

    End Sub
#End Region
End Class