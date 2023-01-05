Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Public Class Pr_KardexProductos
    Dim _Inter As Integer = 0

    'gb_FacturaIncluirICE 
    Public _nameButton As String
    Public _tab As SuperTabItem
    Dim Lote As Boolean = False

    Public Sub _prIniciarTodo()
        tbFechaI.Value = Now.Date
        tbFechaF.Value = Now.Date
        _prValidarLote()
        _PMIniciarTodo()
        'L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        Me.Text = "REPORTE KARDEX DE PRODUCTOS"
        MReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        _IniciarComponentes()
        _prCargarComboLibreriaDeposito(cbAlmacen)
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
                CheckGeneralLote.Visible = True
            Else
                Lote = False
                CheckGeneralLote.Visible = False
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
    Public Sub _IniciarComponentes()

        checkDetallado.CheckValue = False
        checkGeneral.CheckValue = True


    End Sub
    Public Sub _prInterpretarDatos(ByRef _dt As DataTable)
        If (checkGeneral.Checked) Then
            _prObtenerKardexGeneral(_dt)
        End If
        If (CheckGeneralLote.Checked) Then
            _prObtenerKardexGeneralLote(_dt)
            'checkDetallado
        End If
        If (checkDetallado.Checked) Then

            '_dt = L_prVentasVsCostosPorVendedorTodosAlmacen(tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString
            _prObtenerDetalle(_dt)

        End If

    End Sub
    Public Sub _prObtenerKardexGeneral(ByRef _dt As DataTable)
        Dim dtaux As DataTable = L_fnObtenerKardexGeneralProductos(tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbAlmacen.Value) ''Aqui obtengo todos los productos con movimientos


        '' as SaldoAnterior,
        '' as Entradas,'' as Salidas,'' as SaldoFinal
        For i As Integer = 0 To dtaux.Rows.Count - 1 Step 1
            Dim numipro As Integer = dtaux.Rows(i).Item("yfnumi")
            Dim descprod As String = dtaux.Rows(i).Item("yfcdprod1")
            Dim UnidProd As String = dtaux.Rows(i).Item("Unidad")
            Dim saldoAnt As String = ""
            Dim Entrada As String = ""
            Dim Salida As String = ""
            Dim SaldoFinal As String = ""
            Dim dtkardex As DataTable = P_ArmarGrillaDatosGeneral(numipro, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbAlmacen.Value, descprod, UnidProd,
                                                                  saldoAnt, Entrada, Salida, SaldoFinal)
            dtaux.Rows(i).Item("SaldoAnterior") = saldoAnt
            dtaux.Rows(i).Item("Entradas") = Entrada
            dtaux.Rows(i).Item("Salidas") = Salida
            dtaux.Rows(i).Item("SaldoFinal") = SaldoFinal
        Next
        _dt = dtaux
    End Sub



    Public Sub _prObtenerKardexGeneralLote(ByRef _dt As DataTable)
        Dim dtaux As DataTable = L_fnObtenerKardexGeneralProductosporLote(tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbAlmacen.Value) ''Aqui obtengo todos los productos con movimientos


        '' as SaldoAnterior,
        '' as Entradas,'' as Salidas,'' as SaldoFinal
        For i As Integer = 0 To dtaux.Rows.Count - 1 Step 1
            Dim numipro As Integer = dtaux.Rows(i).Item("yfnumi")
            Dim descprod As String = dtaux.Rows(i).Item("yfcdprod1")
            Dim UnidProd As String = dtaux.Rows(i).Item("Unidad")
            Dim saldoAnt As String = ""
            Dim Entrada As String = ""
            Dim Salida As String = ""
            Dim SaldoFinal As String = ""
            Dim lote As String = dtaux.Rows(i).Item("iclot")
            Dim FechaVenc As Date = dtaux.Rows(i).Item("icfven")
            Dim dtkardex As DataTable = P_ArmarGrillaDatosGeneralLote(numipro, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbAlmacen.Value, descprod, UnidProd,
                                                                  saldoAnt, Entrada, Salida, SaldoFinal,
                                                                  lote, FechaVenc)
            dtaux.Rows(i).Item("SaldoAnterior") = saldoAnt
            dtaux.Rows(i).Item("Entradas") = Entrada
            dtaux.Rows(i).Item("Salidas") = Salida
            dtaux.Rows(i).Item("SaldoFinal") = SaldoFinal
        Next
        _dt = dtaux
    End Sub
    Public Sub _prObtenerDetalle(ByRef _dt As DataTable)
        Dim dtaux As DataTable = L_fnObtenerProductoConMovimiento(tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbAlmacen.Value) ''Aqui obtengo todos los productos con movimientos
        _dt = L_fnObtenerKardexPorProducto(-1, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbAlmacen.Value)
        For i As Integer = 0 To dtaux.Rows.Count - 1 Step 1
            Dim numipro As Integer = dtaux.Rows(i).Item("yfnumi")
            Dim descprod As String = dtaux.Rows(i).Item("yfcdprod1")
            Dim UnidProd As String = dtaux.Rows(i).Item("ycdes3")
            Dim dtkardex As DataTable = P_ArmarGrillaDatos(numipro, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbAlmacen.Value, descprod, UnidProd)

            _dt.Merge(dtkardex)

        Next

    End Sub

    Private Function P_ArmarGrillaDatosGeneral(codprod As Integer, fechaI As String, fechaF As String, almacen As Integer, DescProd As String, UnidPro As String, ByRef SaldoAnt As String,
                                              ByRef Entradas As String, ByRef Salidas As String,
                                              ByRef SaldoFinal As String) As DataTable
        Dim Dt1Kardex = New DataTable
        Dim Dt2KardexTotal = New DataTable

        Dt2KardexTotal = L_fnObtenerHistorialProductoGeneral(codprod, fechaI, almacen)
        Dt1Kardex = L_fnObtenerKardexPorProducto(codprod, fechaI, fechaF, almacen)
        If (Dt1Kardex.Rows.Count > 0) Then
            P_ArmarKardexGeneral(Dt1Kardex, Dt2KardexTotal, codprod, DescProd, UnidPro,
                          SaldoAnt, Entradas, Salidas, SaldoFinal)

        End If
        Return Dt1Kardex
    End Function

    Private Function P_ArmarGrillaDatosGeneralLote(codprod As Integer, fechaI As String, fechaF As String, almacen As Integer, DescProd As String, UnidPro As String, ByRef SaldoAnt As String,
                                            ByRef Entradas As String, ByRef Salidas As String,
                                            ByRef SaldoFinal As String, Lote As String, FechaVenc As Date) As DataTable
        Dim Dt1Kardex = New DataTable
        Dim Dt2KardexTotal = New DataTable

        Dt2KardexTotal = L_fnObtenerHistorialProductoGeneralPorLote(codprod, fechaI, almacen, Lote, FechaVenc.ToString("yyyy/MM/dd"))
        Dt1Kardex = L_fnObtenerHistorialProductoporLote(codprod, fechaI, fechaF, almacen, Lote, FechaVenc.ToString("yyyy/MM/dd"))
        If (Dt1Kardex.Rows.Count > 0) Then
            P_ArmarKardexGeneral(Dt1Kardex, Dt2KardexTotal, codprod, DescProd, UnidPro,
                          SaldoAnt, Entradas, Salidas, SaldoFinal)

        End If
        Return Dt1Kardex
    End Function
    Private Sub P_ArmarKardexGeneral(ByRef Dt1Kardex As DataTable, ByRef Dt2KardexTotal As DataTable,
                                  codprod As Integer, Descprod As String, Unidad As String, ByRef SaldoAnt As String,
                                 ByRef Entradas As String, ByRef Salidas As String, ByRef SaldoFinal As String)

        Dim saldo As Double = 0
        Dim ingT As Double = 0
        Dim salT As Double = 0

        If (Not IsDBNull(Dt2KardexTotal.Compute("Sum(entrada)+Sum(salida)", "cprod = " + Str(codprod) + " and concep = 1 or concep=3"))) Then
            ingT = Dt2KardexTotal.Compute("Sum(entrada)+Sum(salida)", "cprod = " + Str(codprod) + " and concep = 1 or concep=3")
        End If
        If (Not IsDBNull(Dt2KardexTotal.Compute("Sum(entrada)+Sum(salida)", "cprod = " + Str(codprod) + " and concep = 2 or concep=4"))) Then
            salT = Dt2KardexTotal.Compute("Sum(entrada)+Sum(salida)", "cprod = " + Str(codprod) + " and concep = 2 or concep=4")
        End If
        saldo = ingT + salT
        Dim ing As Double = 0
        Dim sal As Double = 0
        Dim saldoInicial As Double = 0
        'Sumar ingreso de inventario
        ing = IIf(IsDBNull(Dt1Kardex.Compute("Sum(entrada)", " concep = 1 or concep=3")), 0, Dt1Kardex.Compute("Sum(entrada)", "concep = 1 or concep=3"))
        'Sumar salida de inventario
        sal = IIf(IsDBNull(Dt1Kardex.Compute("Sum(salida)", " concep = 2 or concep=4")), 0, Dt1Kardex.Compute("Sum(salida)", "concep = 2 or concep=4"))
        SaldoAnt = Str(saldo).Trim
        Entradas = Str(ing).Trim
        Salidas = Str(Math.Abs(sal)).Trim
        SaldoFinal = Str(Math.Abs((ing + saldo) + sal)).Trim

    End Sub
    Private Function P_ArmarGrillaDatos(codprod As Integer, fechaI As String, fechaF As String, almacen As Integer, DescProd As String, UnidPro As String) As DataTable
        Dim Dt1Kardex = New DataTable
        Dim Dt2KardexTotal = New DataTable

        Dt2KardexTotal = L_fnObtenerHistorialProductoGeneral(codprod, fechaI, almacen)
        Dt1Kardex = L_fnObtenerKardexPorProducto(codprod, fechaI, fechaF, almacen)
        If (Dt1Kardex.Rows.Count > 0) Then
            P_ArmarKardex(Dt1Kardex, Dt2KardexTotal, codprod, DescProd, UnidPro)

        End If
        Return Dt1Kardex
    End Function
    Private Sub P_ArmarKardex(ByRef Dt1Kardex As DataTable, ByRef Dt2KardexTotal As DataTable,
                                   codprod As Integer, Descprod As String, Unidad As String)

        Dim saldo As Double = 0
        Dim ingT As Double = 0
        Dim salT As Double = 0

        If (Not IsDBNull(Dt2KardexTotal.Compute("Sum(entrada)+Sum(salida)", "cprod = " + Str(codprod) + " and concep = 1 or concep=3"))) Then
            ingT = Dt2KardexTotal.Compute("Sum(entrada)+Sum(salida)", "cprod = " + Str(codprod) + " and concep = 1 or concep=3")
        End If
        If (Not IsDBNull(Dt2KardexTotal.Compute("Sum(entrada)+Sum(salida)", "cprod = " + Str(codprod) + " and concep = 2 or concep=4"))) Then
            salT = Dt2KardexTotal.Compute("Sum(entrada)+Sum(salida)", "cprod = " + Str(codprod) + " and concep = 2 or concep=4")
        End If
        saldo = ingT + salT
        Dim ing As Double = 0
        Dim sal As Double = 0
        Dim saldoInicial As Double = 0
        'Sumar ingreso de inventario
        ing = IIf(IsDBNull(Dt1Kardex.Compute("Sum(entrada)+Sum(salida)", "cprod = " + Str(codprod) + " and concep = 1 or concep=3")), 0, Dt1Kardex.Compute("Sum(entrada)+Sum(salida)", "cprod = " + Str(codprod) + " and concep = 1 or concep=3"))
        'Sumar salida de inventario
        sal = IIf(IsDBNull(Dt1Kardex.Compute("Sum(entrada)+Sum(salida)", "cprod = " + Str(codprod) + " and concep = 2 or concep=4")), 0, Dt1Kardex.Compute("Sum(entrada)+Sum(salida)", "cprod = " + Str(codprod) + " and concep = 2 or concep=4"))
        'Saldo inicial al partir de la fecha indicada
        saldoInicial = saldo '+ sal + ing
        'Insertamos la primera fila con el saldo Inicial
        Dim f As DataRow
        'Dim st As System.Type

        'D.yfnumi, D.yfcdprod1, Umin.ycdes3, id, almacen, fdoc, concep, descConcep,
        '                    obs, est, alm, id2,
        '                  cprod, descProd, desc2Prod, entrada, salida, saldo, nombreCliente, fechaR
        'f = Dt1Kardex.NewRow
        'f.Item(0) = codprod
        'f.Item(1) = Descprod
        'f.Item(2) = Unidad
        'f.Item(3) = 0
        'f.Item(4) = 0
        ''f.Item(4) = ""
        'f.Item(5) = tbFechaI.Value.ToShortDateString
        'f.Item(6) = tbFechaF.Value.ToShortDateString
        'f.Item(7) = "SALDO INICIAL"
        'f.Item(8) = "Saldo Inicial"
        'f.Item(11) = 1
        'f.Item(12) = 1
        'f.Item(13) = 0
        'f.Item(14) = codprod
        'f.Item(15) = Descprod
        'f.Item(16) = ""
        'f.Item(17) = 0
        'f.Item(18) = 0
        'f.Item(19) = saldoInicial
        'f.Item(20) = ""

        'Dt1Kardex.Rows.InsertAt(f, 0)

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


    Private Sub _prCargarReporte()
        Dim _dt As New DataTable
        _prInterpretarDatos(_dt)
        If (_dt.Rows.Count > 0) Then

            If (checkGeneral.Checked) Then

                Dim objrep As New R_KardexInventarioGeneralResumen
                objrep.SetDataSource(_dt)
                Dim fechaI As String = tbFechaI.Value.ToString("dd/MM/yyyy")
                Dim fechaF As String = tbFechaF.Value.ToString("dd/MM/yyyy")
                objrep.SetParameterValue("deposito", cbAlmacen.Text)
                objrep.SetParameterValue("FechaIni", fechaI)
                objrep.SetParameterValue("FechaFin", fechaF)
                MReportViewer.ReportSource = objrep
                MReportViewer.Show()
                MReportViewer.BringToFront()
            Else
                If (CheckGeneralLote.Checked) Then
                    Dim objrep As New R_KardexInventarioGeneralResumenPorLotes
                    objrep.SetDataSource(_dt)
                    Dim fechaI As String = tbFechaI.Value.ToString("dd/MM/yyyy")
                    Dim fechaF As String = tbFechaF.Value.ToString("dd/MM/yyyy")
                    objrep.SetParameterValue("deposito", cbAlmacen.Text)
                    objrep.SetParameterValue("FechaIni", fechaI)
                    objrep.SetParameterValue("FechaFin", fechaF)
                    MReportViewer.ReportSource = objrep
                    MReportViewer.Show()
                    MReportViewer.BringToFront()
                Else
                    If (Lote = True) Then
                        Dim objrep As New R_KardexInventarioProductoAgrupado
                        objrep.SetDataSource(_dt)
                        Dim fechaI As String = tbFechaI.Value.ToString("dd/MM/yyyy")
                        Dim fechaF As String = tbFechaF.Value.ToString("dd/MM/yyyy")
                        objrep.SetParameterValue("deposito", cbAlmacen.Text)
                        objrep.SetParameterValue("FechaIni", fechaI)
                        objrep.SetParameterValue("FechaFin", fechaF)
                        MReportViewer.ReportSource = objrep
                        MReportViewer.Show()
                        MReportViewer.BringToFront()
                    Else
                        Dim objrep As New R_KardexInventarioProductoAgrupadoSinLote
                        objrep.SetDataSource(_dt)
                        Dim fechaI As String = tbFechaI.Value.ToString("dd/MM/yyyy")
                        Dim fechaF As String = tbFechaF.Value.ToString("dd/MM/yyyy")
                        objrep.SetParameterValue("deposito", cbAlmacen.Text)
                        objrep.SetParameterValue("FechaIni", fechaI)
                        objrep.SetParameterValue("FechaFin", fechaF)
                        MReportViewer.ReportSource = objrep
                        MReportViewer.Show()
                        MReportViewer.BringToFront()
                    End If

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

    Private Sub Pr_VentasAtendidas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()

    End Sub







    Private Sub CheckUnaALmacen_CheckValueChanged(sender As Object, e As EventArgs)
        'If (CheckUnaALmacen.Checked) Then
        '    CheckTodosAlmacen.CheckValue = False
        '    tbAlmacen.Enabled = True
        '    tbAlmacen.BackColor = Color.White
        '    tbAlmacen.Focus()
        '    tbAlmacen.ReadOnly = False
        '    _prCargarComboLibreriaSucursal(tbAlmacen)
        '    If (CType(tbAlmacen.DataSource, DataTable).Rows.Count > 0) Then
        '        tbAlmacen.SelectedIndex = 0

        '    End If
        'End If
    End Sub

    Private Sub CheckTodosAlmacen_CheckValueChanged(sender As Object, e As EventArgs)
        'If (CheckTodosAlmacen.Checked) Then
        '    CheckUnaALmacen.CheckValue = False
        '    tbAlmacen.Enabled = True
        '    tbAlmacen.BackColor = Color.Gainsboro
        '    tbAlmacen.ReadOnly = True
        '    _prCargarComboLibreriaSucursal(tbAlmacen)
        '    CType(tbAlmacen.DataSource, DataTable).Rows.Clear()
        '    tbAlmacen.SelectedIndex = -1

        'End If
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

    'Private Sub CheckTodosVendedor_CheckedChanged(sender As Object, e As EventArgs) Handles checkDetallado.CheckedChanged
    '    If (checkDetallado.Checked) Then
    '        checkGeneral.CheckValue = False
    '    Else
    '        checkGeneral.CheckValue = True
    '    End If
    'End Sub

    'Private Sub checkGeneral_CheckValueChanged(sender As Object, e As EventArgs) Handles checkGeneral.CheckValueChanged
    '    If (checkGeneral.Checked) Then
    '        checkDetallado.CheckValue = False
    '    Else
    '        checkDetallado.CheckValue = True
    '    End If
    'End Sub
End Class