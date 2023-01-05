Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar

Public Class Pr_Ventas12Meses
    Dim _Inter As Integer = 0

    'gb_FacturaIncluirICE

    Public _nameButton As String
    Public _tab As SuperTabItem
    Dim titulo As String = ""

    Public Sub _prIniciarTodo()
        tbFechaI.Value = Now.Date
        tbFechaF.Value = Now.Date
        _PMIniciarTodo()
        L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        _prCargarComboLibreriaSucursal(cbConcepto)
        Me.Text = "REPORTE ESTADISTICOS DE VENTAS"
        MReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None

    End Sub
    Sub _prCargarTableCLIENTEVentas(ByRef dt As DataTable)
        dt = L_fnObtenerCLientes(tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"))

        For i As Integer = 0 To dt.Rows.Count - 1 Step 1

            Dim numi As Integer = dt.Rows(i).Item("numiVendedor")
            Dim Vendedor As String = dt.Rows(i).Item("vendedor")

            Dim mesInicial As Integer = Month(tbFechaI.Value)
            Dim mesFinal As Integer = Month(tbFechaF.Value)
            Dim fechaInicialMes As Date
            Dim FechaFinalMes As Date
            Dim Sumtotal As Double = 0
            For k As Integer = mesInicial To mesFinal Step 1
                If (k = mesInicial) Then
                    fechaInicialMes = tbFechaI.Value
                    If (mesInicial = mesFinal) Then
                        FechaFinalMes = tbFechaF.Value
                    Else
                        FechaFinalMes = UltimoDiaDelMes(tbFechaI.Value)
                    End If
                Else
                    If (k = mesFinal) Then
                        FechaFinalMes = tbFechaF.Value
                        fechaInicialMes = PrimerDiaDelMes(tbFechaF.Value)
                    Else
                        Dim ano = tbFechaF.Value
                        fechaInicialMes = DateSerial(ano.Year, k, 1)
                        FechaFinalMes = UltimoDiaDelMes(fechaInicialMes)

                    End If

                End If
                Dim aux As DataTable = L_fnObtenerVentasClientes(fechaInicialMes.ToString("yyyy/MM/dd"), FechaFinalMes.ToString("yyyy/MM/dd"), numi)
                Dim columnaMes As String = MonthName(k)
                If (aux.Rows.Count > 0) Then
                    dt.Rows(i).Item(columnaMes) = aux.Rows(0).Item("total")
                    Sumtotal = Sumtotal + aux.Rows(0).Item("total")
                End If
            Next
            dt.Rows(i).Item("total") = Sumtotal
        Next
        titulo = "REPORTE ESTADISTICO VENTAS POR CLIENTE"
    End Sub

    Sub _prCargarTableZONASVentas(ByRef dt As DataTable)
        dt = L_fnObtenerZonasVentasEstadistico(tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"))

        For i As Integer = 0 To dt.Rows.Count - 1 Step 1

            Dim numi As Integer = dt.Rows(i).Item("numiVendedor")
            Dim Vendedor As String = dt.Rows(i).Item("vendedor")

            Dim mesInicial As Integer = Month(tbFechaI.Value)
            Dim mesFinal As Integer = Month(tbFechaF.Value)
            Dim fechaInicialMes As Date
            Dim FechaFinalMes As Date
            Dim Sumtotal As Double = 0
            For k As Integer = mesInicial To mesFinal Step 1
                If (k = mesInicial) Then
                    fechaInicialMes = tbFechaI.Value
                    If (mesInicial = mesFinal) Then
                        FechaFinalMes = tbFechaF.Value
                    Else
                        FechaFinalMes = UltimoDiaDelMes(tbFechaI.Value)
                    End If
                Else
                    If (k = mesFinal) Then
                        FechaFinalMes = tbFechaF.Value
                        fechaInicialMes = PrimerDiaDelMes(tbFechaF.Value)
                    Else
                        Dim ano = tbFechaF.Value
                        fechaInicialMes = DateSerial(ano.Year, k, 1)
                        FechaFinalMes = UltimoDiaDelMes(fechaInicialMes)

                    End If

                End If
                Dim aux As DataTable = L_fnObtenerVentasZonasEstadistico(fechaInicialMes.ToString("yyyy/MM/dd"), FechaFinalMes.ToString("yyyy/MM/dd"), numi)
                Dim columnaMes As String = MonthName(k)
                If (aux.Rows.Count > 0) Then
                    dt.Rows(i).Item(columnaMes) = aux.Rows(0).Item("total")
                    Sumtotal = Sumtotal + aux.Rows(0).Item("total")
                End If
            Next
            dt.Rows(i).Item("total") = Sumtotal
        Next
        titulo = "REPORTE ESTADISTICO VENTAS POR ZONAS"
    End Sub

    Sub _prCargarTableCOBRANZASVentas(ByRef dt As DataTable)
        dt = L_fnObtenerCOBRANZASVentasEstadistico(tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"))

        For i As Integer = 0 To dt.Rows.Count - 1 Step 1

            Dim numi As Integer = dt.Rows(i).Item("numiVendedor")
            Dim Vendedor As String = dt.Rows(i).Item("vendedor")

            Dim mesInicial As Integer = Month(tbFechaI.Value)
            Dim mesFinal As Integer = Month(tbFechaF.Value)
            Dim fechaInicialMes As Date
            Dim FechaFinalMes As Date
            Dim Sumtotal As Double = 0
            For k As Integer = mesInicial To mesFinal Step 1
                If (k = mesInicial) Then
                    fechaInicialMes = tbFechaI.Value
                    If (mesInicial = mesFinal) Then
                        FechaFinalMes = tbFechaF.Value
                    Else
                        FechaFinalMes = UltimoDiaDelMes(tbFechaI.Value)
                    End If
                Else
                    If (k = mesFinal) Then
                        FechaFinalMes = tbFechaF.Value
                        fechaInicialMes = PrimerDiaDelMes(tbFechaF.Value)
                    Else
                        Dim ano = tbFechaF.Value
                        fechaInicialMes = DateSerial(ano.Year, k, 1)
                        FechaFinalMes = UltimoDiaDelMes(fechaInicialMes)

                    End If

                End If
                Dim aux As DataTable = L_fnObtenerVentasCOBRANZASEstadistico(fechaInicialMes.ToString("yyyy/MM/dd"), FechaFinalMes.ToString("yyyy/MM/dd"), numi)
                Dim columnaMes As String = MonthName(k)
                If (aux.Rows.Count > 0) Then
                    dt.Rows(i).Item(columnaMes) = aux.Rows(0).Item("total")
                    Sumtotal = Sumtotal + aux.Rows(0).Item("total")
                End If
            Next
            dt.Rows(i).Item("total") = Sumtotal
        Next
        titulo = "REPORTE ESTADISTICO COBRANZAS POR VENDEDOR"
    End Sub
    Sub _prCargarTablePRODUCTOSVentas(ByRef dt As DataTable)
        dt = L_fnObtenerProductosVentasEstadistico(tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"))

        For i As Integer = 0 To dt.Rows.Count - 1 Step 1

            Dim numi As Integer = dt.Rows(i).Item("numiVendedor")
            Dim Vendedor As String = dt.Rows(i).Item("vendedor")

            Dim mesInicial As Integer = Month(tbFechaI.Value)
            Dim mesFinal As Integer = Month(tbFechaF.Value)
            Dim fechaInicialMes As Date
            Dim FechaFinalMes As Date
            Dim Sumtotal As Double = 0
            For k As Integer = mesInicial To mesFinal Step 1
                If (k = mesInicial) Then
                    fechaInicialMes = tbFechaI.Value
                    If (mesInicial = mesFinal) Then
                        FechaFinalMes = tbFechaF.Value
                    Else
                        FechaFinalMes = UltimoDiaDelMes(tbFechaI.Value)
                    End If
                Else
                    If (k = mesFinal) Then
                        FechaFinalMes = tbFechaF.Value
                        fechaInicialMes = PrimerDiaDelMes(tbFechaF.Value)
                    Else
                        Dim ano = tbFechaF.Value
                        fechaInicialMes = DateSerial(ano.Year, k, 1)
                        FechaFinalMes = UltimoDiaDelMes(fechaInicialMes)

                    End If

                End If
                Dim aux As DataTable = L_fnObtenerVentasProductosEstadistico(fechaInicialMes.ToString("yyyy/MM/dd"), FechaFinalMes.ToString("yyyy/MM/dd"), numi)
                Dim columnaMes As String = MonthName(k)
                If (aux.Rows.Count > 0) Then
                    dt.Rows(i).Item(columnaMes) = aux.Rows(0).Item("total")
                    Sumtotal = Sumtotal + aux.Rows(0).Item("total")
                End If
            Next
            dt.Rows(i).Item("total") = Sumtotal
        Next
        titulo = "REPORTE ESTADISTICO VENTAS POR PRODUCTO"
    End Sub
    Sub _prCargarTableVendedorVentas(ByRef dt As DataTable)
        dt = L_fnObtenerVendedores(tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"))

        For i As Integer = 0 To dt.Rows.Count - 1 Step 1

            Dim numi As Integer = dt.Rows(i).Item("numiVendedor")
            Dim Vendedor As String = dt.Rows(i).Item("vendedor")

            Dim mesInicial As Integer = Month(tbFechaI.Value)
            Dim mesFinal As Integer = Month(tbFechaF.Value)
            Dim fechaInicialMes As Date
            Dim FechaFinalMes As Date
            Dim Sumtotal As Double = 0
            For k As Integer = mesInicial To mesFinal Step 1
                If (k = mesInicial) Then
                    fechaInicialMes = tbFechaI.Value
                    If (mesInicial = mesFinal) Then
                        FechaFinalMes = tbFechaF.Value
                    Else
                        FechaFinalMes = UltimoDiaDelMes(tbFechaI.Value)
                    End If
                Else
                    If (k = mesFinal) Then
                        FechaFinalMes = tbFechaF.Value
                        fechaInicialMes = PrimerDiaDelMes(tbFechaF.Value)
                    Else
                        Dim ano = tbFechaF.Value
                        fechaInicialMes = DateSerial(ano.Year, k, 1)
                        FechaFinalMes = UltimoDiaDelMes(fechaInicialMes)

                    End If

                End If
                Dim aux As DataTable = L_fnObtenerVentasVendedores(fechaInicialMes.ToString("yyyy/MM/dd"), FechaFinalMes.ToString("yyyy/MM/dd"), numi)
                Dim columnaMes As String = MonthName(k)
                If (aux.Rows.Count > 0) Then
                    dt.Rows(i).Item(columnaMes) = aux.Rows(0).Item("total")
                    Sumtotal = Sumtotal + aux.Rows(0).Item("total")
                End If
            Next
            dt.Rows(i).Item("total") = Sumtotal
        Next
        titulo = "REPORTE ESTADISTICO VENTAS POR VENDEDOR"
    End Sub

    Function PrimerDiaDelMes(ByVal dtmFecha As Date) As Date
        PrimerDiaDelMes = DateSerial(Year(dtmFecha), Month(dtmFecha), 1)
    End Function

    'para saber el ultimo dia del mes
    Function UltimoDiaDelMes(ByVal dtmFecha As Date) As Date
        UltimoDiaDelMes = DateSerial(Year(dtmFecha), Month(dtmFecha) + 1, 0)
    End Function

    Public Sub _prInterpretarDatos(ByRef _dt As DataTable)
        If (cbConcepto.Value = 1) Then

            _prCargarTableVendedorVentas(_dt)
        End If
        If (cbConcepto.Value = 2) Then

            _prCargarTableCLIENTEVentas(_dt)
        End If
        If (cbConcepto.Value = 3) Then

            _prCargarTablePRODUCTOSVentas(_dt)
        End If
        If (cbConcepto.Value = 4) Then

            _prCargarTableZONASVentas(_dt)
        End If
        If (cbConcepto.Value = 5) Then

            _prCargarTableCOBRANZASVentas(_dt)
        End If
    End Sub
    Private Sub _prCargarReporte()
        Dim _dt As New DataTable

        _prInterpretarDatos(_dt)
        If (_dt.Rows.Count > 0) Then
            _dt.DefaultView.Sort = "total DESC"
            _dt = _dt.DefaultView.ToTable
            Dim objrep As New R_VentasVendedor12Meses
            objrep.SetDataSource(_dt)
            Dim fechaI As String = tbFechaI.Value.ToString("dd/MM/yyyy")
            Dim fechaF As String = tbFechaF.Value.ToString("dd/MM/yyyy")
            objrep.SetParameterValue("usuario", L_Usuario)
            objrep.SetParameterValue("fechaI", fechaI)
            objrep.SetParameterValue("fechaF", fechaF)
            objrep.SetParameterValue("titulo", titulo)
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


    Function _CrearTable() As DataTable
        Dim dt As DataTable = New DataTable


        Dim numicol As New DataColumn("numi")
        numicol.DataType = GetType(Integer)
        Dim descripcion As New DataColumn("descripcion")
        descripcion.DataType = GetType(String)
        dt.Columns.Add(numicol)
        dt.Columns.Add(descripcion)

        dt.Rows.Add(1, "VENDEDORES")
        dt.Rows.Add(2, "CLIENTES")
        dt.Rows.Add(3, "PRODUCTOS")
        'dt.Rows.Add(4, "ZONAS")
        'dt.Rows.Add(5, "COBRADORES")
        Return dt

    End Function
    Private Sub _prCargarComboLibreriaSucursal(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = _CrearTable()
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("numi").Width = 60
            .DropDownList.Columns("numi").Caption = "COD"
            .DropDownList.Columns.Add("descripcion").Width = 500
            .DropDownList.Columns("descripcion").Caption = "DESCRIPCION"
            .ValueMember = "numi"
            .DisplayMember = "descripcion"
            .DataSource = dt
            .Refresh()
        End With
        If (CType(cbConcepto.DataSource, DataTable).Rows.Count > 0) Then
            cbConcepto.SelectedIndex = 0
        End If
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
End Class