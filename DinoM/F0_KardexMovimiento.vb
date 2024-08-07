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

Public Class F0_KardexMovimiento
    Dim _Inter As Integer = 0
#Region "Variables Globales"

    Dim Dt1Kardex As DataTable
    Dim Dt2KardexTotal As DataTable
    Dim _DuracionSms As Integer = 5
    Dim Lote As Boolean = False
#End Region
#Region "Variables Globales"
    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
    Dim RutaGlobal As String = gs_CarpetaRaiz

#End Region
    Private Sub _IniciarTodo()
        ''L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        _prCargarComboLibreriaDeposito(cbAlmacen)
        _prValidarLote()
        'Me.WindowState = FormWindowState.Maximized
        _prInhabiliitar()
        ''_prAsignarPermisos()
        Me.Text = "KARDEX PRODUCTO"
        PanelToolBar1.Visible = False
        btnImprimir.Visible = False
        PanelInferior.Visible = False
        tbCodigo.Focus()
        _CrearDiseno()
        _prAsignarPermisos()
        If (CType(cbAlmacen.DataSource, DataTable).Rows.Count > 0) Then
            cbAlmacen.SelectedIndex = 0

        End If
    End Sub
    Public Sub _prValidarLote()
        Dim dt As DataTable = L_fnPorcUtilidad()
        If (dt.Rows.Count > 0) Then
            Dim lot As Integer = dt.Rows(0).Item("VerLote")
            If (lot = 1) Then
                Lote = True
                tblote.Visible = True
                lblote.Visible = True
            Else
                Lote = False
                tblote.Visible = False
                lblote.Visible = False
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
            .DropDownList.Columns.Add("aabdes").Width = 300
            .DropDownList.Columns("aabdes").Caption = "ALMACÉN"
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
    Public Sub _CrearDiseno()
        GroupPanelDatos.Style.BackColor = Color.FromArgb(13, 71, 161)
        GroupPanelDatos.Style.BackColor2 = Color.FromArgb(13, 71, 161)
        GroupPanelDatos.Style.TextColor = Color.White

        GroupPanelKardex.Style.BackColor = Color.FromArgb(13, 71, 161)
        GroupPanelKardex.Style.BackColor2 = Color.FromArgb(13, 71, 161)
        GroupPanelKardex.Style.TextColor = Color.White

    End Sub
    Private Sub _prInhabiliitar()
        tbCodigo.ReadOnly = True
        tbproducto.ReadOnly = True
        tbFechaF.Value = Now.Date
        tbFechaI.Value = Now.Date
    End Sub



    Private Sub tbCodigo_KeyDown(sender As Object, e As KeyEventArgs) Handles tbCodigo.KeyDown

        If (cbAlmacen.SelectedIndex < 0) Then
            Return

        End If
        If e.KeyData = Keys.Control + Keys.Enter Then

            Dim dt As DataTable

            dt = L_fnListarProductosKardex(cbAlmacen.Value)
            ' a.yfnumi ,a.yfcdprod1 as producto,a.yfcdprod2 as descripcioncorta,a.yfcprod,b.iccven as stock 

            Dim listEstCeldas As New List(Of Modelo.Celda)
            listEstCeldas.Add(New Modelo.Celda("yfnumi", True, "COD. DYN", 80))
            listEstCeldas.Add(New Modelo.Celda("yfcprod", True, "COD. DELTA", 90))
            listEstCeldas.Add(New Modelo.Celda("yfcbarra", True, "COD. BARRAS", 110))
            listEstCeldas.Add(New Modelo.Celda("producto", True, "PRODUCTO", 520))
            listEstCeldas.Add(New Modelo.Celda("descripcioncorta", False, "DESC. CORTA".ToUpper, 150))
            listEstCeldas.Add(New Modelo.Celda("stock", True, "Stock".ToUpper, 100))
            Dim ef = New Efecto
            ef.tipo = 3
            ef.dt = dt
            ef.SeleclCol = 1
            ef.listEstCeldas = listEstCeldas
            ef.alto = 50
            ef.ancho = 250
            ef.Context = "Seleccione PRODUCTO".ToUpper
            ef.ShowDialog()
            Dim bandera As Boolean = False
            bandera = ef.band
            If (bandera = True) Then
                Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
                ' a.yfnumi ,a.yfcdprod1 as producto,a.yfcdprod2 as descripcioncorta,a.yfcprod,b.iccven as stock 
                If (IsNothing(Row)) Then
                    Return
                End If
                tbCodigo.Text = ""
                P_ArmarGrillaDatos()
                tbCodigo.Text = Row.Cells("yfnumi").Value
                tbproducto.Text = Row.Cells("producto").Value
                tbsaldo.Text = Row.Cells("stock").Value
                tbFechaI.Focus()
                tblote.Clear()
                tbFechaVenc.Clear()

            End If

        End If

    End Sub

    Private Sub F0_KardexMovimiento_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _IniciarTodo()

    End Sub

    Private Sub BtGenerar_Click(sender As Object, e As EventArgs) Handles BtGenerar.Click
        P_GenerarKardexCliente()
    End Sub

    Private Sub P_GenerarKardexCliente()
        P_ArmarGrillaDatos()
        L_fnGenerarKardex(gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina, tbCodigo.Text)
    End Sub
    Private Sub P_ArmarKardex()

        Dim saldo As Double = 0
        Dim ingT As Double = 0
        Dim salT As Double = 0

        If (Not IsDBNull(Dt2KardexTotal.Compute("Sum(entrada)+Sum(salida)", "cprod = " + tbCodigo.Text + " and concep = 1 or concep=3"))) Then
            ingT = Dt2KardexTotal.Compute("Sum(entrada)+Sum(salida)", "cprod = " + tbCodigo.Text + " and concep = 1 or concep=3")
        End If
        If (Not IsDBNull(Dt2KardexTotal.Compute("Sum(entrada)+Sum(salida)", "cprod = " + tbCodigo.Text + " and concep = 2 or concep=4"))) Then
            salT = Dt2KardexTotal.Compute("Sum(entrada)+Sum(salida)", "cprod = " + tbCodigo.Text + " and concep = 2 or concep=4")
        End If
        saldo = ingT + salT
        Dim ing As Double = 0
        Dim sal As Double = 0
        Dim saldoInicial As Double = 0
        'Sumar ingreso de inventario
        ing = IIf(IsDBNull(Dt1Kardex.Compute("Sum(entrada)+Sum(salida)", "cprod = " + tbCodigo.Text + " and concep = 1 or concep=3")), 0, Dt1Kardex.Compute("Sum(entrada)+Sum(salida)", "cprod = " + tbCodigo.Text + " and concep = 1 or concep=3"))
        'Sumar salida de inventario
        sal = IIf(IsDBNull(Dt1Kardex.Compute("Sum(entrada)+Sum(salida)", "cprod = " + tbCodigo.Text + " and concep = 2 or concep=4")), 0, Dt1Kardex.Compute("Sum(entrada)+Sum(salida)", "cprod = " + tbCodigo.Text + " and concep = 2 or concep=4"))
        'Saldo inicial al partir de la fecha indicada
        saldoInicial = saldo '+ sal + ing
        'Insertamos la primera fila con el saldo Inicial
        Dim f As DataRow
        'Dim st As System.Type
        f = Dt1Kardex.NewRow
        f.Item(0) = 0
        f.Item(1) = ""
        f.Item(2) = tbFechaI.Value.ToShortDateString
        f.Item(3) = 0
        f.Item(4) = "SALDO INICIAL"
        f.Item(5) = "Saldo Inicial"
        f.Item(6) = ""
        ''f.Item(7) = tbFechaI.Value.ToShortDateString
        f.Item(8) = 1
        f.Item(9) = 1
        f.Item(10) = 0
        f.Item(11) = tbCodigo.Text
        f.Item(12) = tbproducto.Text
        f.Item(13) = ""
        f.Item(14) = 0
        f.Item(15) = 0
        f.Item(16) = saldoInicial
        f.Item(17) = ""


        Dt1Kardex.Rows.InsertAt(f, 0)

        For Each fil As DataRow In Dt1Kardex.Rows
            Dim s As String = fil.Item("concep").ToString
            If (fil.Item("concep").ToString.Equals("1") Or fil.Item("concep").ToString.Equals("3")) Then '
                saldoInicial = saldoInicial + CDbl(fil.Item("entrada"))
                fil.Item("saldo") = saldoInicial
            ElseIf (fil.Item("concep").ToString.Equals("2") Or fil.Item("concep").ToString.Equals("4")) Then
                saldoInicial = saldoInicial + CDbl(fil.Item("salida"))
                fil.Item("saldo") = saldoInicial
            End If
        Next

    End Sub
    Public Sub _prInterpretarDatosLotes()
        If ((Lote = False) Or (Lote = True And tblote.Text.Length <= 0)) Then

            If (tbCodigo.Text.Length > 0 And cbAlmacen.SelectedIndex >= 0) Then
                Dt2KardexTotal = L_fnObtenerHistorialProductoGeneral(tbCodigo.Text, tbFechaI.Value.ToString("yyyy/MM/dd"), cbAlmacen.Value)
                Dt1Kardex = L_fnObtenerHistorialProducto(tbCodigo.Text, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbAlmacen.Value)
                If (Dt1Kardex.Rows.Count > 0) Then
                    P_ArmarKardex()
                Else
                    ToastNotification.Show(Me, "No hay kardex para el rango de fecha".ToUpper,
                           My.Resources.INFORMATION,
                           _DuracionSms * 1000,
                           eToastGlowColor.Blue,
                           eToastPosition.BottomLeft)
                End If
            Else
                Dt1Kardex = L_fnObtenerHistorialProducto("-1", tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbAlmacen.Value)
            End If
        Else
            If ((Lote = True And tblote.Text.Length > 0)) Then
                If (tbCodigo.Text.Length > 0 And cbAlmacen.SelectedIndex >= 0 And tblote.Text.Length > 0) Then
                    Dt2KardexTotal = L_fnObtenerHistorialProductoGeneralPorLote(tbCodigo.Text, tbFechaI.Value.ToString("yyyy/MM/dd"), cbAlmacen.Value,
                                        tblote.Text, tbFechaVenc.Text)
                    Dt1Kardex = L_fnObtenerHistorialProductoporLote(tbCodigo.Text, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbAlmacen.Value, tblote.Text, tbFechaVenc.Text)
                    If (Dt1Kardex.Rows.Count > 0) Then
                        P_ArmarKardex()
                    Else
                        ToastNotification.Show(Me, "No hay kardex para el rango de fecha".ToUpper,
                               My.Resources.INFORMATION,
                               _DuracionSms * 1000,
                               eToastGlowColor.Blue,
                               eToastPosition.BottomLeft)
                    End If
                Else
                    Dt1Kardex = L_fnObtenerHistorialProductoporLote("-1", tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbAlmacen.Value, tblote.Text, tbFechaVenc.Text)
                End If
            End If
        End If


    End Sub
    Private Sub ActualizarSaldosDesiguales()
        If (cbAlmacen.SelectedIndex < 0) Then
            Return

        End If
        If (Dt1Kardex.Rows.Count > 0) Then

            Dim saldoAct As Double = IIf(tbsaldo.Text = String.Empty, 0, tbsaldo.Text)
            Dim SaldoTabla As Double = IIf(IsDBNull(Dt1Kardex.Rows(Dt1Kardex.Rows.Count - 1).Item("saldo")), 0, Dt1Kardex.Rows(Dt1Kardex.Rows.Count - 1).Item("saldo"))
            Dim Lote As String = Dt1Kardex.Rows(Dt1Kardex.Rows.Count - 1).Item("Lote")
            Dim FechaVenc As String = Dt1Kardex.Rows(Dt1Kardex.Rows.Count - 1).Item("FechaVenc")
            If (saldoAct = SaldoTabla) Then
                Return
            End If
            If (Now.Date.ToString("yyyy/MM/dd").Equals(tbFechaF.Value.ToString("yyyy/MM/dd")) And tblote.Text = String.Empty) Then
                L_fnActualizarSaldoNuevo(cbAlmacen.Value, tbCodigo.Text, Dt1Kardex.Rows(Dt1Kardex.Rows.Count - 1).Item("saldo").ToString, Lote, FechaVenc)
                tbsaldo.Text = Dt1Kardex.Rows(Dt1Kardex.Rows.Count - 1).Item("saldo").ToString

            End If
        End If
    End Sub
    Private Sub P_ArmarGrillaDatos()
        Dt1Kardex = New DataTable
        Dt2KardexTotal = New DataTable

        _prInterpretarDatosLotes()
        Dgj1Datos.BoundMode = Janus.Data.BoundMode.Bound
        Dgj1Datos.DataSource = Dt1Kardex
        Dgj1Datos.RetrieveStructure()

        'dar formato a las columnas
        With Dgj1Datos.RootTable.Columns(0)
            .Caption = "CÓDIGO"
            .Key = "id"
            .Width = 70
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.FontSize = 9
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
        End With
        With Dgj1Datos.RootTable.Columns(1)
            .Caption = "ALMACEN"
            .Key = "almacen"
            .Width = 90
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.FontSize = 9
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
        End With
        With Dgj1Datos.RootTable.Columns(2)
            .Caption = "Fecha".ToUpper
            .Key = "fdoc"
            .FormatString = "yyyy/MM/dd"
            .Width = 120
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.FontSize = 9
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
        End With

        With Dgj1Datos.RootTable.Columns(3)
            .Caption = ""
            .Key = "concep"
            .Width = 0
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.FontSize = 9
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
            '.CellStyle.BackColor = Color.AliceBlue
        End With

        With Dgj1Datos.RootTable.Columns(4)
            .Caption = "Concepto".ToUpper
            .Key = "descConcep"
            .Width = 180
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.FontSize = 9
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
        End With
        With Dgj1Datos.RootTable.Columns(5)
            .Caption = "Observación".ToUpper
            .Key = "obs"
            .Width = 520
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.FontSize = 9
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .WordWrap = True
            .MaxLines = 3
            '.CellStyle.BackColor = Color.AliceBlue
        End With

        ''''''''''''''

        With Dgj1Datos.RootTable.Columns(6)
            .Caption = "LOTE"
            .Width = 100
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .CellStyle.FontSize = 9
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
        End With
        With Dgj1Datos.RootTable.Columns(7)
            .Caption = "F. VENC"
            .Width = 100
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .CellStyle.FontSize = 9
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
        End With
        If (Lote = False) Then
            Dgj1Datos.RootTable.Columns(7).Visible = False
            Dgj1Datos.RootTable.Columns(6).Visible = False
        End If
        With Dgj1Datos.RootTable.Columns(8)
            .Caption = ""
            .Key = "est"
            .Width = 0
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.FontSize = 9
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            '.CellStyle.BackColor = Color.AliceBlue
        End With
        With Dgj1Datos.RootTable.Columns(9)
            .Caption = ""
            .Key = "alm"
            .Width = 0
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.FontSize = 9
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
            '.CellStyle.BackColor = Color.AliceBlue
        End With
        With Dgj1Datos.RootTable.Columns(10)
            .Caption = ""
            .Key = "id2"
            .Width = 0
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.FontSize = 9
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            '.CellStyle.BackColor = Color.AliceBlue
        End With
        With Dgj1Datos.RootTable.Columns(11)
            .Caption = "Código"
            .Key = "cprod"
            .Width = 80
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.FontSize = 9
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            '.CellStyle.BackColor = Color.AliceBlue
            .FormatString = "0.00"
        End With
        With Dgj1Datos.RootTable.Columns(12)
            .Caption = "Producto"
            .Key = "descProd"
            .Width = 200
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.FontSize = 9
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
            '.CellStyle.BackColor = Color.AliceBlue
        End With
        With Dgj1Datos.RootTable.Columns(13)
            .Caption = ""
            .Key = "desc2Prod"
            .Width = 0
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.FontSize = 9
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            '.CellStyle.BackColor = Color.AliceBlue
        End With
        With Dgj1Datos.RootTable.Columns(14)
            .Caption = "ENTRADA"
            .Key = "entrada"
            .Width = 80
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.FontSize = 9
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
            If IsDBNull(Dgj1Datos.RootTable.Columns(14)) Then
                .FormatString = "''"
            Else
                .FormatString = "0.00"
            End If

            .AggregateFunction = AggregateFunction.Sum
        End With
        With Dgj1Datos.RootTable.Columns(15)
            .Caption = "SALIDA"
            .Key = "salida"
            .Width = 80
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.FontSize = 9
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
            .FormatString = "0.00"
            .AggregateFunction = AggregateFunction.Sum
        End With
        With Dgj1Datos.RootTable.Columns(16)
            .Caption = "SALDO"
            .Key = "saldo"
            .Width = 80
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.FontSize = 9
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
            .FormatString = "0.00"
        End With
        With Dgj1Datos.RootTable.Columns(17)
            .Caption = "Cliente"
            .Key = "cliente"
            .Width = 300
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.FontSize = 9
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
            .Position = 2
        End With
        With Dgj1Datos.RootTable.Columns(18)
            .Caption = "Cliente"
            .Key = "cliente"
            .Width = 300
            .Visible = False
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.FontSize = 9
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
            .Position = 2
        End With
        'Habilitar Filtradores
        With Dgj1Datos
            .GroupByBoxVisible = False
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            'Diseño de la tabla
            .VisualStyle = VisualStyle.Office2007
            .AlternatingColors = True
            .RecordNavigator = True
            .RecordNavigatorText = "Movimiento"
            .RowHeaders = InheritableBoolean.True

            .TotalRow = InheritableBoolean.True
            .TotalRowFormatStyle.BackColor = Color.Gold
            .TotalRowPosition = TotalRowPosition.BottomFixed


        End With
        _prAplicarCondiccionJanus()
    End Sub
    Public Sub _prAplicarCondiccionJanus()
        Dim fc As GridEXFormatCondition
        fc = New GridEXFormatCondition(Dgj1Datos.RootTable.Columns("saldo"), ConditionOperator.LessThan, 0)
        fc.FormatStyle.BackColor = Color.Red
        fc.FormatStyle.ForeColor = Color.White
        fc.FormatStyle.FontBold = TriState.True
        fc.FormatStyle.FontName = "Open Sans"




        Dgj1Datos.RootTable.FormatConditions.Add(fc)

    End Sub

    Private Sub btImprimir_Click(sender As Object, e As EventArgs) Handles btImprimir.Click
        If (Dgj1Datos.GetRows.Count > 0) Then
            P_GenerarReporte()
            L_fnImpresionKardex(gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina, tbCodigo.Text)
        Else
            ToastNotification.Show(Me, "No hay kardex para el rango de fecha".ToUpper,
                       My.Resources.INFORMATION,
                       _DuracionSms * 1000,
                       eToastGlowColor.Blue,
                       eToastPosition.BottomLeft)
        End If
    End Sub

    Private Sub P_GenerarReporte()

        If Not IsNothing(P_Global.Visualizador) Then
            P_Global.Visualizador.Close()
        End If

        P_Global.Visualizador = New Visualizador
        If (Lote = True) Then
            Dim objrep As New R_KardexInventarioProducto
            '' GenerarNro(_dt)
            ''objrep.SetDataSource(Dt1Kardex)
            objrep.SetDataSource(CType(Dgj1Datos.DataSource, DataTable))
            objrep.SetParameterValue("FechaIni", tbFechaI.Value.ToString("yyyy/MM/dd"))
            objrep.SetParameterValue("FechaFin", tbFechaF.Value.ToString("yyyy/MM/dd"))
            objrep.SetParameterValue("Saldo", tbsaldo.Text)
            objrep.SetParameterValue("producto", tbproducto.Text)
            objrep.SetParameterValue("codProducto", tbCodigo.Text)
            objrep.SetParameterValue("deposito", cbAlmacen.Text)
            'MReportViewer.ReportSource = objrep
            'MReportViewer.Show()
            'MReportViewer.BringToFront()
            P_Global.Visualizador.CrGeneral.ReportSource = objrep 'Comentar
            P_Global.Visualizador.Show() 'Comentar
            P_Global.Visualizador.BringToFront() 'Comentar
        Else
            Dim objrep As New R_KardexInventarioProductoSinLote
            '' GenerarNro(_dt)
            ''objrep.SetDataSource(Dt1Kardex)
            objrep.SetDataSource(CType(Dgj1Datos.DataSource, DataTable))
            objrep.SetParameterValue("FechaIni", tbFechaI.Value.ToString("yyyy/MM/dd"))
            objrep.SetParameterValue("FechaFin", tbFechaF.Value.ToString("yyyy/MM/dd"))
            objrep.SetParameterValue("Saldo", tbsaldo.Text)
            objrep.SetParameterValue("producto", tbproducto.Text)
            objrep.SetParameterValue("codProducto", tbCodigo.Text)
            objrep.SetParameterValue("deposito", cbAlmacen.Text)
            'MReportViewer.ReportSource = objrep

            'MReportViewer.Show()
            'MReportViewer.BringToFront()

            P_Global.Visualizador.CrGeneral.ReportSource = objrep 'Comentar
            P_Global.Visualizador.Show() 'Comentar
            P_Global.Visualizador.BringToFront() 'Comentar
        End If

    End Sub

    Private Sub btActualizar_Click(sender As Object, e As EventArgs) Handles btActualizar.Click
        If (cbAlmacen.SelectedIndex < 0) Then
            Return

        End If
        If (Dt1Kardex.Rows.Count > 0) Then

            Dim saldoAct As Double = IIf(tbsaldo.Text = String.Empty, 0, tbsaldo.Text)
            Dim SaldoTabla As Double = IIf(IsDBNull(Dt1Kardex.Rows(Dt1Kardex.Rows.Count - 1).Item("saldo")), 0, Dt1Kardex.Rows(Dt1Kardex.Rows.Count - 1).Item("saldo"))
            If (saldoAct = SaldoTabla) Then
                Return
            End If
            If (Now.Date.ToString("yyyy/MM/dd").Equals(tbFechaF.Value.ToString("yyyy/MM/dd")) And tblote.Text = String.Empty) Then
                L_fnActualizarSaldo(cbAlmacen.Value, tbCodigo.Text, Dt1Kardex.Rows(Dt1Kardex.Rows.Count - 1).Item("saldo").ToString)

                tbsaldo.Text = Dt1Kardex.Rows(Dt1Kardex.Rows.Count - 1).Item("saldo").ToString

                ToastNotification.Show(Me, "Saldo actualizado Correctamente!!!".ToUpper,
                       My.Resources.OK,
                       _DuracionSms * 200,
                       eToastGlowColor.Green,
                       eToastPosition.MiddleCenter)
            Else
                ToastNotification.Show(Me, "Para actualizar el saldo, la fecha final tiene que ser la de hoy!!!".ToUpper,
                       My.Resources.INFORMATION,
                       _DuracionSms * 1000,
                       eToastGlowColor.Red,
                       eToastPosition.MiddleCenter)

            End If
        End If
    End Sub

    Private Sub cbAlmacen_ValueChanged(sender As Object, e As EventArgs) Handles cbAlmacen.ValueChanged
        If (tbCodigo.Text.Length > 0 And cbAlmacen.SelectedIndex >= 0) Then
            Dim dt As DataTable = L_fnObtenerSaldoProducto(cbAlmacen.Value, tbCodigo.Text)
            If (dt.Rows.Count > 0) Then
                tbsaldo.Text = dt.Rows(0).Item("stock")
                'CType(Dgj1Datos.DataSource, DataTable).Rows.Clear()
                P_ArmarGrillaDatos()

            End If
        End If
        'tbCodigo.Clear()
        'tbproducto.Clear()
        'tbsaldo.Clear()

    End Sub

    Private Sub TextBoxX1_KeyDown(sender As Object, e As KeyEventArgs) Handles tblote.KeyDown
        If (cbAlmacen.SelectedIndex < 0 Or tbCodigo.Text.Length <= 0) Then
            Return

        End If
        If e.KeyData = Keys.Control + Keys.Enter Then

            Dim dt As DataTable

            dt = L_fnListarLotesProductos(tbCodigo.Text, cbAlmacen.Value)
            'a.iclot ,a.icfven  ,a.iccven 

            Dim listEstCeldas As New List(Of Modelo.Celda)
            listEstCeldas.Add(New Modelo.Celda("iclot", True, "LOTE", 120))
            listEstCeldas.Add(New Modelo.Celda("icfven", True, "Fecha Venc.".ToUpper, 120,
                                               "yyyy/MM/dd"))
            listEstCeldas.Add(New Modelo.Celda("iccven", True, "Stock", 120))

            Dim ef = New Efecto
            ef.tipo = 3
            ef.dt = dt
            ef.SeleclCol = 2
            ef.listEstCeldas = listEstCeldas
            ef.alto = 50
            ef.ancho = 350
            ef.Context = "Seleccione Lote del producto".ToUpper
            ef.ShowDialog()
            Dim bandera As Boolean = False
            bandera = ef.band
            If (bandera = True) Then
                Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
                ' a.yfnumi ,a.yfcdprod1 as producto,a.yfcdprod2 as descripcioncorta,a.yfcprod,b.iccven as stock 
                If (IsNothing(Row)) Then
                    Return
                End If

                CType(Dgj1Datos.DataSource, DataTable).Rows.Clear()

                tblote.Text = Row.Cells("iclot").Value
                tbsaldo.Text = Row.Cells("iccven").Value
                tbFechaI.Focus()
                tbFechaVenc.Text = Row.Cells("icfven").Value
            End If

        End If
    End Sub

    Private Sub tblote_TextChanged(sender As Object, e As EventArgs) Handles tblote.TextChanged
        If (tblote.Text = String.Empty) Then
            Dim dt As DataTable = L_fnObtenerSaldoProducto(cbAlmacen.Value, tbCodigo.Text)
            If (dt.Rows.Count > 0) Then
                tbsaldo.Text = dt.Rows(0).Item("stock")
                CType(Dgj1Datos.DataSource, DataTable).Rows.Clear()


            End If
        End If
    End Sub
    Private Sub _prSalir()


        _modulo.Select()
        Me.Close()


    End Sub
    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _prSalir()
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

    Private Sub btExportar_Click(sender As Object, e As EventArgs) Handles btExportar.Click
        _prCrearCarpetaReportes()
        Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
        If (P_ExportarExcel(RutaGlobal + "\Reporte\Reporte Productos")) Then
            L_fnExportarKardex(gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina, tbCodigo.Text)
            ToastNotification.Show(Me, "EXPORTACIÓN DE KARDEX DE PRODUCTOS EXITOSA..!!!",
                                       img, 2000,
                                       eToastGlowColor.Green,
                                       eToastPosition.BottomCenter)
        Else
            ToastNotification.Show(Me, "FALLÓ LA EXPORTACIÓN DE KARDEX DE PRODUCTOS..!!!",
                                       My.Resources.WARNING, 2000,
                                       eToastGlowColor.Red,
                                       eToastPosition.BottomLeft)
        End If
    End Sub
    Private Sub _prCrearCarpetaReportes()
        Dim rutaDestino As String = RutaGlobal + "\Reporte\Reporte Productos\"

        If System.IO.Directory.Exists(RutaGlobal + "\Reporte\Reporte Productos\") = False Then
            If System.IO.Directory.Exists(RutaGlobal + "\Reporte") = False Then
                System.IO.Directory.CreateDirectory(RutaGlobal + "\Reporte")
                If System.IO.Directory.Exists(RutaGlobal + "\Reporte\Reporte Productos") = False Then
                    System.IO.Directory.CreateDirectory(RutaGlobal + "\Reporte\Reporte Productos")
                End If
            Else
                If System.IO.Directory.Exists(RutaGlobal + "\Reporte\Reporte Productos") = False Then
                    System.IO.Directory.CreateDirectory(RutaGlobal + "\Reporte\Reporte Productos")

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
                Dim _fila As Integer = Dgj1Datos.GetRows.Length
                Dim _columna As Integer = Dgj1Datos.RootTable.Columns.Count
                Dim _archivo As String = _ubicacion & "\KardexProductos_" & Now.Date.Day &
                    "." & Now.Date.Month & "." & Now.Date.Year & "_" & Now.Hour & "." & Now.Minute & "." & Now.Second & ".csv"
                Dim _linea As String = ""
                Dim _filadata = 0, columndata As Int32 = 0
                File.Delete(_archivo)
                _stream = File.OpenWrite(_archivo)
                _escritor = New StreamWriter(_stream, System.Text.Encoding.UTF8)

                For Each _col As GridEXColumn In Dgj1Datos.RootTable.Columns
                    If (_col.Visible) Then
                        _linea = _linea & _col.Caption & ";"
                    End If
                Next
                _linea = Mid(CStr(_linea), 1, _linea.Length - 1)
                _escritor.WriteLine(_linea)
                _linea = Nothing


                For Each _fil As GridEXRow In Dgj1Datos.GetRows
                    For Each _col As GridEXColumn In Dgj1Datos.RootTable.Columns
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

    Private Sub btSalir_Click(sender As Object, e As EventArgs) Handles btSalir.Click
        Me.Close()
    End Sub

End Class