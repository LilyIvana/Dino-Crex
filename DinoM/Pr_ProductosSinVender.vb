Imports System.IO
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Imports Janus.Windows.GridEX
Imports Logica.AccesoLogica
Public Class Pr_ProductosSinVender
    Dim _Inter As Integer = 0

    'gb_FacturaIncluirICE

    Public _nameButton As String
    Public _tab As SuperTabItem
    Private idProveedor As Integer = 0
    Private idProducto As Integer = 0
    Public Sub _prIniciarTodo()
        tbFechaI.Value = Now.Date
        tbFechaF.Value = Now.Date
        _PMIniciarTodo()
        'L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        Me.Text = "REPORTE PRODUCTOS SIN VENDER"
        MReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        _IniciarComponentes()
        _prCargarComboLibreriaSucursal(tbAlmacen)
        _prCargarComboLibreria(cbProveedor, 1, 1)
    End Sub
    Public Sub _IniciarComponentes()
        tbAlmacen.ReadOnly = True
        tbAlmacen.Enabled = False
        CheckTodosAlmacen.CheckValue = True
        CheckTodosProveedor.Checked = True

    End Sub
    Public Sub _prInterpretarDatos(ByRef _dt As DataTable)
        Dim fechaDesde As DateTime = tbFechaI.Value.ToString("yyyy/MM/dd")
        Dim fechaHasta As DateTime = tbFechaF.Value.ToString("yyyy/MM/dd")
        Dim idproducto As Integer = 0, idProveedor As Integer = 0, idAlmacen As Integer = 0

        If tbAlmacen.SelectedIndex <> 0 Then idAlmacen = tbAlmacen.Value
        If cbProveedor.SelectedIndex <> -1 Then idProveedor = cbProveedor.Value
        If swStock.Value = True Then
            _dt = L_prProductosNoVendidos(fechaDesde, fechaHasta, idProveedor)
        Else
            '_dt = L_prProductosNoVendidos(fechaDesde, fechaHasta, idProveedor)
            '_dt = _dt.Select("Stock > 0 ", "yfcdprod1").CopyToDataTable
            _dt = L_prProductosNoVendidosStockMayor0(fechaDesde, fechaHasta, idProveedor)
        End If

    End Sub
    Private Sub _prCargarReporte()
        Dim _dt As New DataTable
        _prInterpretarDatos(_dt)
        If (_dt.Rows.Count > 0) Then
            Dim objrep As New R_ProductosNoVendidos2
            L_fnBotonGenerar(gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina, 0, "PRODUCTOS SIN VENDER", "PRODUCTOS SIN VENDER")

            objrep.SetDataSource(_dt)
            Dim fechaI As String = tbFechaI.Value.ToString("dd/MM/yyyy")
            Dim fechaF As String = tbFechaF.Value.ToString("dd/MM/yyyy")
            objrep.SetParameterValue("usuario", L_Usuario)
            objrep.SetParameterValue("fechaI", fechaI)
            objrep.SetParameterValue("fechaF", fechaF)
            objrep.SetParameterValue("conteo", _dt.Rows.Count)
            MReportViewer.ReportSource = objrep
            MReportViewer.Show()
            MReportViewer.BringToFront()

            CargarGrilla(_dt)
        Else
            ToastNotification.Show(Me, "NO HAY DATOS PARA LOS PARAMETROS SELECCIONADOS..!!!",
                                       My.Resources.INFORMATION, 2000,
                                       eToastGlowColor.Blue,
                                       eToastPosition.BottomLeft)
            MReportViewer.ReportSource = Nothing
        End If
    End Sub
    Private Sub CargarGrilla(ByRef _dt As DataTable)

        JGrM_Buscador.DataSource = _dt
        JGrM_Buscador.RetrieveStructure()
        JGrM_Buscador.AlternatingColors = True

        With JGrM_Buscador.RootTable.Columns("yfnumi")
            .Width = 90
            .Visible = True
            .Caption = "COD. DYNASYS"
        End With
        With JGrM_Buscador.RootTable.Columns("yfcprod")
            .Width = 90
            .Visible = True
            .Caption = "COD. DELTA"
        End With
        With JGrM_Buscador.RootTable.Columns("yfcdprod1")
            .Width = 90
            .Visible = True
            .Caption = "DESCRIPCIÓN"
        End With
        With JGrM_Buscador.RootTable.Columns("presentacion")
            .Visible = False
        End With
        With JGrM_Buscador.RootTable.Columns("unidad")
            .Width = 90
            .Visible = True
            .Caption = "UNIDAD"
        End With
        With JGrM_Buscador.RootTable.Columns("Proveedor")
            .Width = 90
            .Visible = True
            .Caption = "PROVEEDOR"
        End With
        With JGrM_Buscador.RootTable.Columns("PCosto")
            .Width = 90
            .Visible = True
            .Caption = "P. COSTO"
            .FormatString = "0.00"
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        End With
        With JGrM_Buscador.RootTable.Columns("PVenta")
            .Width = 90
            .Visible = True
            .Caption = "P. WHOLESALE"
            .FormatString = "0.00"
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        End With
        With JGrM_Buscador.RootTable.Columns("Preferencial")
            .Width = 90
            .Visible = True
            .Caption = "P. PREFERENCIAL"
            .FormatString = "0.00"
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        End With
        With JGrM_Buscador.RootTable.Columns("PDV")
            .Width = 90
            .Visible = True
            .Caption = "P. PDV"
        End With
        With JGrM_Buscador.RootTable.Columns("FechaUltVenta")
            .Width = 90
            .Visible = True
            .Caption = "F. ULT. VENTA"
        End With
        With JGrM_Buscador.RootTable.Columns("Stock")
            .Width = 90
            .Visible = True
            .Caption = "STOCK"
        End With
        With JGrM_Buscador
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
    Private Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        _prCargarReporte()
    End Sub

    Private Sub Pr_VentasAtendidas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()
    End Sub

    Private Sub CheckUnaALmacen_CheckValueChanged(sender As Object, e As EventArgs) Handles CheckUnaALmacen.CheckValueChanged
        If (CheckUnaALmacen.Checked) Then
            CheckTodosAlmacen.CheckValue = False
            tbAlmacen.Enabled = True
            tbAlmacen.BackColor = Color.White
            tbAlmacen.Focus()
            tbAlmacen.ReadOnly = False

            If (CType(tbAlmacen.DataSource, DataTable).Rows.Count > 0) Then
                tbAlmacen.SelectedIndex = 0

            End If
        End If
    End Sub

    Private Sub CheckTodosAlmacen_CheckValueChanged(sender As Object, e As EventArgs) Handles CheckTodosAlmacen.CheckValueChanged
        If (CheckTodosAlmacen.Checked) Then
            CheckUnaALmacen.CheckValue = False
            tbAlmacen.Enabled = True
            tbAlmacen.BackColor = Color.Gainsboro
            tbAlmacen.ReadOnly = True
            'CType(tbAlmacen.DataSource, DataTable).Rows.Clear()
            tbAlmacen.SelectedIndex = -1

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
    Private Sub _prCargarComboLibreriaProducto(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_prObtenerProductos()
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("idProducto").Width = 60
            .DropDownList.Columns("idProducto").Caption = "COD"
            .DropDownList.Columns.Add("Descripcion").Width = 500
            .DropDownList.Columns("Descripcion").Caption = "SUCURSAL"
            .ValueMember = "idProducto"
            .DisplayMember = "Descripcion"
            .DataSource = dt
            .Refresh()
        End With
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
    Private Sub _prCargarComboLibreria(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo, cod1 As String, cod2 As String)
        Dim dt As New DataTable
        dt = L_prLibreriaClienteLGeneral(cod1, cod2)
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("yccod3").Width = 70
            .DropDownList.Columns("yccod3").Caption = "COD"
            .DropDownList.Columns.Add("ycdes3").Width = 200
            .DropDownList.Columns("ycdes3").Caption = "DESCRIPCION"
            .ValueMember = "yccod3"
            .DisplayMember = "ycdes3"
            .DataSource = dt
            .Refresh()
        End With
    End Sub

    Private Sub CheckUnaProveedor_CheckValueChanged(sender As Object, e As EventArgs) Handles CheckUnaProveedor.CheckValueChanged
        If (CheckUnaProveedor.Checked) Then
            CheckTodosProveedor.CheckValue = False
            cbProveedor.Enabled = True
            cbProveedor.BackColor = Color.White
            cbProveedor.Focus()
            cbProveedor.ReadOnly = False
            If (CType(cbProveedor.DataSource, DataTable).Rows.Count > 0) Then
                cbProveedor.SelectedIndex = 0
            End If
        End If
    End Sub

    Private Sub CheckTodosProveedor_CheckValueChanged(sender As Object, e As EventArgs) Handles CheckTodosProveedor.CheckValueChanged
        If (CheckTodosProveedor.Checked) Then
            CheckUnaProveedor.CheckValue = False
            cbProveedor.Enabled = True
            cbProveedor.BackColor = Color.Gainsboro
            cbProveedor.ReadOnly = True
            cbProveedor.SelectedIndex = -1
            idProveedor = 0
        End If
    End Sub

    Private Sub btnExportar_Click(sender As Object, e As EventArgs) Handles btnExportar.Click
        _prCrearCarpetaReportes()
        Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
        If (P_ExportarExcel(RutaGlobal + "\Reporte\Reporte Productos")) Then
            L_fnBotonExportar(gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina, 0, "PRODUCTOS SIN VENDER", "PRODUCTOS SIN VENDER")
            ToastNotification.Show(Me, "EXPORTACIÓN DE PRODUCTOS SIN VENDER EXITOSA..!!!",
                                       img, 2000,
                                       eToastGlowColor.Green,
                                       eToastPosition.BottomCenter)
        Else
            ToastNotification.Show(Me, "FALLÓ LA EXPORTACIÓN DE PRODUCTOS SIN VENDER..!!!",
                                       My.Resources.WARNING, 2000,
                                       eToastGlowColor.Red,
                                       eToastPosition.BottomLeft)
        End If
    End Sub
    Public Function P_ExportarExcel(_ruta As String) As Boolean
        Dim _ubicacion As String
        If (1 = 1) Then 'If(_directorio.ShowDialog = Windows.Forms.DialogResult.OK) Then

            _ubicacion = _ruta
            Try
                Dim _stream As Stream
                Dim _escritor As StreamWriter
                Dim _fila As Integer = JGrM_Buscador.GetRows.Length
                Dim _columna As Integer = JGrM_Buscador.RootTable.Columns.Count
                Dim _archivo As String = _ubicacion & "\ProductosSinVender_" & Now.Date.Day &
                    "." & Now.Date.Month & "." & Now.Date.Year & "_" & Now.Hour & "." & Now.Minute & "." & Now.Second & ".csv"
                Dim _linea As String = ""
                Dim _filadata = 0, columndata As Int32 = 0
                File.Delete(_archivo)
                _stream = File.OpenWrite(_archivo)
                _escritor = New StreamWriter(_stream, System.Text.Encoding.UTF8)

                For Each _col As GridEXColumn In JGrM_Buscador.RootTable.Columns
                    If (_col.Visible) Then
                        _linea = _linea & _col.Caption & ";"
                    End If
                Next
                _linea = Mid(CStr(_linea), 1, _linea.Length - 1)
                _escritor.WriteLine(_linea)
                _linea = Nothing

                For Each _fil As GridEXRow In JGrM_Buscador.GetRows
                    For Each _col As GridEXColumn In JGrM_Buscador.RootTable.Columns
                        If (_col.Visible) Then
                            Dim data As String = CStr(_fil.Cells(_col.Key).Value)
                            data = data.Replace(";", ",")
                            _linea = _linea & data & ";"
                        End If
                    Next
                    _linea = Mid(CStr(_linea), 1, _linea.Length - 1)
                    _escritor.WriteLine(_linea)
                    _linea = Nothing

                Next
                _escritor.Close()

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

    Private Sub JGrM_Buscador_EditingCell(sender As Object, e As EditingCellEventArgs) Handles JGrM_Buscador.EditingCell
        e.Cancel = True
    End Sub
End Class