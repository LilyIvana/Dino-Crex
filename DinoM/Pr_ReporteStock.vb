Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Public Class Pr_ReporteStock
    Dim _Inter As Integer = 0

    'gb_FacturaIncluirICE 
    Public _nameButton As String
    Public _tab As SuperTabItem
    Dim bandera As Boolean = False

    Public Sub _prIniciarTodo()
        L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        _prCargarComboLibreriaSucursal(tbAlmacen)
        _prCargarComboLibreriaPrecioVenta(tbcatprecio)

        _PMIniciarTodo()
        Me.Text = "DIFERENCIA DE STOCK DE PRODUCTOS"
        MReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        _IniciarComponentes()
        bandera = True
    End Sub
    Public Sub _IniciarComponentes()

    End Sub
    Public Sub _prInterpretarDatos(ByRef _dt As DataTable)
        If swConsolidado.Value = False Then
            tbAlmacen.Value = 1
            _dt = L_prReporteStockConsolidado(tbAlmacen.Value, tbcatprecio.Value, date1.Text)
        Else
            If (tbAlmacen.SelectedIndex >= 0 And tbcatprecio.SelectedIndex >= 0 And checkMayorCero.Checked And CheckBox1.Checked = False) Then
                _dt = L_prReporteProductosStockMayorCero(tbAlmacen.Value, tbcatprecio.Value)
            End If
            If (tbAlmacen.SelectedIndex >= 0 And tbcatprecio.SelectedIndex >= 0 And checkMayorCero.Checked And CheckBox1.Checked) Then
                _dt = L_prReporteProductoMayorFecha(tbAlmacen.Value, tbcatprecio.Value, date1.Text)
            End If

            'If (tbAlmacen.SelectedIndex >= 0 And tbcatprecio.SelectedIndex >= 0 And checkMayorCero.Checked And CheckBox1.Checked) Then
            '    _dt = L_prReporteStockConsolidado(tbAlmacen.Value, tbcatprecio.Value, date1.Text)
            'End If
        End If

    End Sub
    Private Sub _prCargarReporte()
        Dim _dt As New DataTable
        _prInterpretarDatos(_dt)

        'If (CheckBox1.Checked = True) Then
        '    _prObtenerKardexGeneral(_dt)
        'End If

        If (_dt.Rows.Count > 0) Then

            Dim objrep As New R_StockDiferencia
            objrep.SetDataSource(_dt)

            objrep.SetParameterValue("usuario", L_Usuario)
            objrep.SetParameterValue("categoria", tbAlmacen.Text + " - " + tbcatprecio.Text)
            objrep.SetParameterValue("fechafin", date1.Text + " - " + date1.Text)
            MReportViewer.ReportSource = objrep
            MReportViewer.Show()
            MReportViewer.BringToFront()



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
        If (dt.Rows.Count > 0) Then
            tbAlmacen.SelectedIndex = 0
        End If
    End Sub

    Private Sub _prCargarComboLibreriaPrecioVenta(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_prListarPrecioVenta()
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("ygnumi").Width = 60
            .DropDownList.Columns("ygnumi").Caption = "COD"
            .DropDownList.Columns.Add("ygdesc").Width = 500
            .DropDownList.Columns("ygdesc").Caption = "SUCURSAL"
            .ValueMember = "ygnumi"
            .DisplayMember = "ygdesc"
            .DataSource = dt
            .Refresh()
        End With
        If (dt.Rows.Count > 0) Then
            tbcatprecio.SelectedIndex = 0
        End If
    End Sub
    Private Sub _prCargarComboLibreriaPrecioCosto(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_prListarPrecioCosto()
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("ygnumi").Width = 60
            .DropDownList.Columns("ygnumi").Caption = "COD"
            .DropDownList.Columns.Add("ygdesc").Width = 500
            .DropDownList.Columns("ygdesc").Caption = "SUCURSAL"
            .ValueMember = "ygnumi"
            .DisplayMember = "ygdesc"
            .DataSource = dt
            .Refresh()
        End With
        If (dt.Rows.Count > 0) Then
            tbcatprecio.SelectedIndex = 0
        End If
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click

        Me.Close()

    End Sub

    Private Sub swTipoVenta_ValueChanged(sender As Object, e As EventArgs) Handles swTipoVenta.ValueChanged
        If (bandera = False) Then
            Return
        End If
        If (swTipoVenta.Value = True) Then
            _prCargarComboLibreriaPrecioVenta(tbcatprecio)
        Else
            _prCargarComboLibreriaPrecioCosto(tbcatprecio)

        End If
    End Sub


    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If (CheckBox1.Checked = True) Then
            date1.Enabled = True
        Else
            date1.Enabled = False
        End If

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

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        _Inter = _Inter + 1
        If _Inter = 1 Then
            Me.WindowState = FormWindowState.Normal

        Else
            Me.Opacity = 100
            Timer1.Enabled = False
        End If
    End Sub
End Class