Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports System.Data.OleDb
Imports System.IO
Imports DevComponents.DotNetBar.Controls
Imports Janus.Windows.GridEX
Public Class Pr_StockMinimo
    Public _nameButton As String
    Public _tab As SuperTabItem
    Dim bandera As Boolean = False

    Private Function GetDataExcel(
    ByVal fileName As String, ByVal sheetName As String) As DataTable

        ' Comprobamos los parámetros.
        '
        If ((String.IsNullOrEmpty(fileName)) OrElse
          (String.IsNullOrEmpty(sheetName))) Then _
          Throw New ArgumentNullException()

        Try
            Dim extension As String = IO.Path.GetExtension(fileName)

            Dim connString As String = "Data Source=" & fileName

            If (extension = ".xls") Then
                connString &= ";Provider=Microsoft.Jet.OLEDB.4.0;" &
                       "Extended Properties='Excel 8.0;HDR=YES;IMEX=1'"

            ElseIf (extension = ".xlsx") Then
                connString &= ";Provider=Microsoft.ACE.OLEDB.12.0;" &
                       "Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1'"
            Else
                Throw New ArgumentException(
                  "La extensión " & extension & " del archivo no está permitida.")
            End If

            Using conexion As New OleDbConnection(connString)

                Dim sql As String = "SELECT * FROM [" & sheetName & "$]"
                Dim adaptador As New OleDbDataAdapter(sql, conexion)

                Dim dt As New DataTable("Excel")

                adaptador.Fill(dt)

                Return dt

            End Using

        Catch ex As Exception
            Throw
        End Try

    End Function
    Private Sub Pr_StockMinimo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()
    End Sub

    Public Sub _prIniciarTodo()
        'L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        _prCargarComboLibreriaSucursal(cbAlmacen)
        _prCargarComboGrupos(cbGrupos)
        _PMIniciarTodo()
        Me.Text = "SALDOS MENORES AL STOCK MIN."
        MReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        _IniciarComponentes()
        bandera = True
    End Sub


    Private Sub _prCargarComboGrupos(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnObtenerGruposLibreria()
        'a.ylcod2,yldes2
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("yccod3").Width = 60
            .DropDownList.Columns("yccod3").Caption = "COD"
            .DropDownList.Columns.Add("yldes2").Width = 250
            .DropDownList.Columns("yldes2").Caption = "GRUPOS"
            .ValueMember = "yccod3"
            .DisplayMember = "yldes2"
            .DataSource = dt
            .Refresh()
        End With
        If (dt.Rows.Count > 0) Then
            cbGrupos.SelectedIndex = 0
        End If
    End Sub
    Public Sub _PMIniciarTodo()

        'TxtNombreUsu.Text = MGlobal.gs_usuario
        'TxtNombreUsu.ReadOnly = True

        'Me.WindowState = FormWindowState.Maximized
        'Me.SupTabItemBusqueda.Visible = False

    End Sub
    Public Sub _IniciarComponentes()

    End Sub

    Private Sub _prCargarComboLibreriaSucursal(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnListarSucursales()
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
        If (dt.Rows.Count > 0) Then
            cbAlmacen.SelectedIndex = 0
        End If
    End Sub


    Private Sub _prCargarReporte()
        Dim _dt As New DataTable
        _prInterpretarDatos(_dt)
        If (_dt.Rows.Count > 0) Then
            L_fnBotonGenerar(gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina, IIf(checkTodosGrupos.Checked = True, 0, cbGrupos.Value), "SALDOS MENORES", "SALDOS MENORES AL STOCK MIN.")
            Dim objrep
            If CheckTodosAlmacen.Checked Then
                objrep = New R_StockMinimoTodosAlmacenes
            Else
                objrep = New R_StockMinimo
            End If

            objrep.SetDataSource(_dt)
            objrep.SetParameterValue("usuario", L_Usuario)
            objrep.SetParameterValue("almacen", cbAlmacen.Text)
            objrep.SetParameterValue("conteo", _dt.Rows.Count)
            MReportViewer.ReportSource = objrep
            MReportViewer.Show()
            MReportViewer.BringToFront()

            CargarGrilla(_dt)
        Else
            ToastNotification.Show(Me, "NO HAY DATOS PARA LOS PARAMETROS SELECCIONADOS..!!!",
                                       My.Resources.INFORMATION, 2500,
                                       eToastGlowColor.Blue,
                                       eToastPosition.TopCenter)
            MReportViewer.ReportSource = Nothing
            grBuscador.ClearStructure()
        End If

    End Sub

    Public Sub _prInterpretarDatos(ByRef _dt As DataTable)
        If (CheckTodosAlmacen.Checked And checkTodosGrupos.Checked) Then
            _dt = L_fnTodosAlmacenTodosLineasMenoresStock()
        End If
        'If (CheckTodosAlmacen.Checked And checkTodosGrupos.Checked) Then
        '    _dt = L_fnTodosAlmacenTodosLineas()
        'End If
        If (checkUnaAlmacen.Checked And checkTodosGrupos.Checked) Then
            _dt = L_fnUnaAlmacenTodosLineasMenoresStock(cbAlmacen.Value)
        End If
        'un almacen todos mayor a 0
        'If (checkUnaAlmacen.Checked And checkTodosGrupos.Checked) Then
        '    _dt = L_fnUnaAlmacenTodosLineasMayorCero(cbAlmacen.Value)
        'End If
        If (checkUnaGrupo.Checked And CheckTodosAlmacen.Checked) Then
            _dt = L_fnTodosAlmacenUnaLineasMenoresStock(cbGrupos.Value)
        End If
        If (checkUnaAlmacen.Checked And checkUnaGrupo.Checked) Then
            _dt = L_fnUnaAlmacenUnaLineasMenoresStock(cbGrupos.Value, cbAlmacen.Value)
        End If
        ' un almacen una linea y mayor a cero
        'If (checkUnaAlmacen.Checked And checkUnaGrupo.Checked) Then
        '    _dt = L_fnUnaAlmacenUnaLineasMayorCero(cbGrupos.Value, cbAlmacen.Value)
        'End If
    End Sub

    Private Sub CargarGrilla(ByRef _dt As DataTable)

        grBuscador.DataSource = _dt
        grBuscador.RetrieveStructure()
        grBuscador.AlternatingColors = True

        With grBuscador.RootTable.Columns("proveedor")
            .Width = 90
            .Visible = True
            .Caption = "PROVEEDOR"
        End With
        With grBuscador.RootTable.Columns("abnumi")
            .Width = 90
            .Visible = False
        End With
        With grBuscador.RootTable.Columns("almacen")
            .Width = 90
            .Visible = False

        End With
        With grBuscador.RootTable.Columns("CodigoProducto")
            .Width = 90
            .Visible = True
            .Caption = "COD DYNASYS"
        End With
        With grBuscador.RootTable.Columns("CodLinea")
            .Width = 90
            .Visible = True
            .Caption = "COD DELTA"
        End With
        With grBuscador.RootTable.Columns("Proveedor")
            .Width = 90
            .Visible = True
            .Caption = "PROVEEDOR"
        End With
        With grBuscador.RootTable.Columns("yfcdprod1")
            .Width = 90
            .Visible = True
            .Caption = "DESCRIPCIÓN"
        End With
        With grBuscador.RootTable.Columns("yfMed")
            .Visible = False
        End With
        With grBuscador.RootTable.Columns("yfap")
            .Visible = False
        End With
        With grBuscador.RootTable.Columns("iccprod")
            .Visible = False
        End With
        With grBuscador.RootTable.Columns("iccven")
            .Width = 90
            .Visible = True
            .Caption = "STOCK"
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        End With
        With grBuscador.RootTable.Columns("yccod3")
            .Visible = False
        End With
        With grBuscador.RootTable.Columns("ycdes3")
            .Visible = False
        End With
        With grBuscador.RootTable.Columns("linea")
            .Visible = False
        End With
        With grBuscador.RootTable.Columns("presentacion")
            .Visible = False
        End With
        With grBuscador.RootTable.Columns("yfcprod")
            .Visible = False
        End With

        With grBuscador
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            .TotalRow = InheritableBoolean.True
            .TotalRowFormatStyle.BackColor = Color.Gold
            .TotalRowPosition = TotalRowPosition.BottomFixed
            'diseño de la grilla

            .RecordNavigator = True
            .RecordNavigatorText = "Datos"
        End With
    End Sub
    Private Sub checkUnaAlmacen_CheckedChanged(sender As Object, e As EventArgs) Handles checkUnaAlmacen.CheckedChanged
        cbAlmacen.ReadOnly = False
    End Sub

    Private Sub checkUnaGrupo_CheckedChanged(sender As Object, e As EventArgs) Handles checkUnaGrupo.CheckedChanged
        cbGrupos.ReadOnly = False
    End Sub

    Private Sub CheckTodosAlmacen_CheckedChanged(sender As Object, e As EventArgs) Handles CheckTodosAlmacen.CheckedChanged
        cbAlmacen.ReadOnly = True
    End Sub

    Private Sub checkTodosGrupos_CheckedChanged(sender As Object, e As EventArgs) Handles checkTodosGrupos.CheckedChanged
        cbGrupos.ReadOnly = True
    End Sub

    Private Sub btn_Salir_Click_1(sender As Object, e As EventArgs) Handles btn_Salir.Click
        Me.Close()
    End Sub

    Private Sub btn_Generar_Click(sender As Object, e As EventArgs) Handles btn_Generar.Click
        _prCargarReporte()
    End Sub

    Private Sub btnExportar_Click(sender As Object, e As EventArgs) Handles btnExportar.Click
        If grBuscador.RowCount > 0 Then
            _prCrearCarpetaReportesGlobal()
            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            If (P_ExportarExcelGlobal(RutaGlobal + "\Reporte\Reporte Productos", grBuscador, "SaldosMenoresAlStockMin")) Then
                L_fnBotonExportar(gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina, IIf(checkTodosGrupos.Checked = True, 0, cbGrupos.Value), "SALDOS MENORES AL STOCK", "SALDOS MENORES AL STOCK MIN.")
                ToastNotification.Show(Me, "EXPORTACIÓN DE SALDOS MENORES AL STOCK MIN. EXITOSA..!!!",
                                           img, 2500,
                                           eToastGlowColor.Green,
                                           eToastPosition.TopCenter)
            Else
                ToastNotification.Show(Me, "FALLÓ LA EXPORTACIÓN DE SALDOS MENORES AL STOCK MIN...!!!",
                                           My.Resources.WARNING, 2500,
                                           eToastGlowColor.Red,
                                           eToastPosition.TopCenter)
            End If
        Else
            ToastNotification.Show(Me, "NO EXISTE DATOS PARA EXPORTAR, PRIMERO DEBE PRESIONAR EL BOTÓN GENERAR..!!!",
                                           My.Resources.WARNING, 2500,
                                           eToastGlowColor.Red,
                                           eToastPosition.TopCenter)
        End If
    End Sub
End Class