Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports Janus.Windows.GridEX
Public Class Pr_ReporteVentasGrafico
    Dim _Inter As Integer = 0

    Public _nameButton As String
    Public _tab As SuperTabItem
    Public Sub _prIniciarTodo()
        ''  L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)

        Me.Text = "REPORTE GRAFICOS VENTAS"
        MReportViewerVendedor.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        MReportViewerAlmacen.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        MReportViewerRendimiento.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        'Me.WindowState = FormWindowState.Maximized
        _IniciarComponentes()
        _prCargarClientes()
        FechaIRendimiento.Value = Now.Date
        FechaFRendimiento.Value = Now.Date
    End Sub
    Public Sub _IniciarComponentes()
        ChechTodos.CheckValue = False
    End Sub
    Public Sub _prCargarClientes()
        'vendedor .ydnumi ,vendedor .ydcod ,vendedor .yddesc as vendedor,Cast(1 as bit)as estado
        Dim dt As New DataTable
        dt = L_prVentasGraficaListarVendedores()
        grvendedor.DataSource = dt
        grvendedor.RetrieveStructure()
        grvendedor.AlternatingColors = True
        With grvendedor.RootTable.Columns("ydnumi")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = False

        End With
        With grvendedor.RootTable.Columns("ydcod")
            .Width = 60
            .Caption = "COD"
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With

        With grvendedor.RootTable.Columns("vendedor")
            .Width = 180
            .Caption = "VENDEDOR"
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
        End With
        With grvendedor.RootTable.Columns("estado")
            .Width = 60
            .Caption = "Estado"
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .Visible = True
        End With

        With grvendedor
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla

        End With
    End Sub
    Private Sub ModeloHor_KeyPress(sender As Object, e As KeyPressEventArgs) Handles MyBase.KeyPress
        e.KeyChar = e.KeyChar.ToString.ToUpper
        If (e.KeyChar = ChrW(Keys.Enter)) Then
            e.Handled = True
            P_Moverenfoque()
        End If
    End Sub

    Private Sub P_Moverenfoque()
        SendKeys.Send("{TAB}")
    End Sub

    Private Sub grvendedor_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grvendedor.EditingCell
        If (e.Column.Index = grvendedor.RootTable.Columns("estado").Index) Then
            e.Cancel = False
        Else
            e.Cancel = True
        End If
    End Sub

    Sub _prGenerarReporteVendedoresMeses()
        Dim _dt As New DataTable
        _dt = L_prVentasGraficaVendedorMes(FechaIVendedor.SelectionRange.Start.ToString("dd/MM/yyyy"),
                                           FechaFVendedor.SelectionRange.Start.ToString("dd/MM/yyyy"))
        If (_dt.Rows.Count > 0) Then

            Dim objrep As New R_GraficaVendedorVentas
            objrep.SetDataSource(_dt)
            Dim fechaI As String = FechaIVendedor.SelectionRange.Start.ToString("dd/MM/yyyy")
            Dim fechaF As String = FechaFVendedor.SelectionRange.Start.ToString("dd/MM/yyyy")
            objrep.SetParameterValue("usuario", L_Usuario)
            objrep.SetParameterValue("fechaI", fechaI)
            objrep.SetParameterValue("fechaF", fechaF)
            MReportViewerVendedor.ReportSource = objrep
            MReportViewerVendedor.Show()
            MReportViewerVendedor.BringToFront()
        Else
            ToastNotification.Show(Me, "NO HAY DATOS PARA LOS PARAMETROS SELECCIONADOS..!!!",
                                    My.Resources.INFORMATION, 2000,
                                    eToastGlowColor.Blue,
                                    eToastPosition.BottomLeft)
            MReportViewerVendedor.ReportSource = Nothing
        End If

    End Sub
    Sub _prGenerarReporteVendedoresAlmacen()
        Dim _dt As New DataTable
        _dt = L_prVentasGraficaVendedorAlmacen(FechaIAlmacen.SelectionRange.Start.ToString("dd/MM/yyyy"),
                                           FechaFAlmacen.SelectionRange.Start.ToString("dd/MM/yyyy"))
        If (_dt.Rows.Count > 0) Then

            Dim objrep As New R_GraficaVendedorVentasAlmacen
            objrep.SetDataSource(_dt)
            Dim fechaI As String = FechaIAlmacen.SelectionRange.Start.ToString("dd/MM/yyyy")
            Dim fechaF As String = FechaFAlmacen.SelectionRange.Start.ToString("dd/MM/yyyy")
            objrep.SetParameterValue("usuario", L_Usuario)
            objrep.SetParameterValue("fechaI", fechaI)
            objrep.SetParameterValue("fechaF", fechaF)
            MReportViewerAlmacen.ReportSource = objrep
            MReportViewerAlmacen.Show()
            MReportViewerAlmacen.BringToFront()
        Else
            ToastNotification.Show(Me, "NO HAY DATOS PARA LOS PARAMETROS SELECCIONADOS..!!!",
                                    My.Resources.INFORMATION, 2000,
                                    eToastGlowColor.Blue,
                                    eToastPosition.BottomLeft)
            MReportViewerVendedor.ReportSource = Nothing
        End If

    End Sub
    Sub _prGenerarReporteVendedoresRendimiento()
        Dim _dt As New DataTable
        _dt = L_prVentasGraficaVendedorRendimiento(FechaIRendimiento.Value.ToString("dd/MM/yyyy"),
                                           FechaFRendimiento.Value.ToString("dd/MM/yyyy"),
                                           CType(grvendedor.DataSource, DataTable))
        If (_dt.Rows.Count > 0) Then

            Dim objrep As New R_GraficaVendedorVentasRendimiento
            objrep.SetDataSource(_dt)
            Dim fechaI As String = FechaIRendimiento.Value.ToString("dd/MM/yyyy")
            Dim fechaF As String = FechaFRendimiento.Value.ToString("dd/MM/yyyy")
            objrep.SetParameterValue("usuario", L_Usuario)
            objrep.SetParameterValue("fechaI", fechaI)
            objrep.SetParameterValue("fechaF", fechaF)
            MReportViewerRendimiento.ReportSource = objrep
            MReportViewerRendimiento.Show()
            MReportViewerRendimiento.BringToFront()
        Else
            ToastNotification.Show(Me, "NO HAY DATOS PARA LOS PARAMETROS SELECCIONADOS..!!!",
                                    My.Resources.INFORMATION, 2000,
                                    eToastGlowColor.Blue,
                                    eToastPosition.BottomLeft)
            MReportViewerVendedor.ReportSource = Nothing
        End If

    End Sub

    Private Sub btnGenerarVendedor_Click(sender As Object, e As EventArgs) Handles btnGenerarVendedor.Click
        _prGenerarReporteVendedoresMeses()
    End Sub

    Private Sub ButtonX1_Click(sender As Object, e As EventArgs) Handles ButtonX1.Click
        _prGenerarReporteVendedoresAlmacen()
    End Sub

    Private Sub ButtonX3_Click(sender As Object, e As EventArgs) Handles ButtonX3.Click
        _prGenerarReporteVendedoresRendimiento()
    End Sub


    Private Sub Pr_ReporteVentasGrafico_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()
    End Sub

    Private Sub ChechTodos_CheckValueChanged(sender As Object, e As EventArgs) Handles ChechTodos.CheckValueChanged
        If (ChechTodos.Checked) Then
            _prSeleccionar()

        Else
            _prDesSeleccionar()
        End If
    End Sub
    Public Sub _prSeleccionar()
        Dim dt As DataTable = CType(grvendedor.DataSource, DataTable)
        If (dt.Rows.Count > 0) Then
            For i As Integer = 0 To dt.Rows.Count - 1 Step 1
                CType(grvendedor.DataSource, DataTable).Rows(i).Item("estado") = 1
            Next
        End If
    End Sub
    Public Sub _prDesSeleccionar()
        Dim dt As DataTable = CType(grvendedor.DataSource, DataTable)
        If (dt.Rows.Count > 0) Then
            For i As Integer = 0 To dt.Rows.Count - 1 Step 1
                CType(grvendedor.DataSource, DataTable).Rows(i).Item("estado") = 0
            Next
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
End Class