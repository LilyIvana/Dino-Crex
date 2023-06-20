
Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports Janus.Windows.GridEX
Imports System.IO
Imports DevComponents.DotNetBar.SuperGrid
Imports DevComponents.DotNetBar.Controls

Public Class F1_SaldosExcel
    Dim _Inter As Integer = 0
#Region "Variables Locales"
    Dim RutaGlobal As String = gs_CarpetaRaiz
    Dim RutaTemporal As String = "C:\Temporal"
    Dim Modificado As Boolean = False
    Dim nameImg As String = "Default.jpg"
    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
    Public Limpiar As Boolean = False  'Bandera para indicar si limpiar todos los datos o mantener datos ya registrados
#End Region
#Region "Metodos Privados"
    Private Sub _prIniciarTodo()
        Me.Text = "SALDOS VALORADOS PARA EXPORTAR A EXCEL"
        _prCargarComboLibreriaSucursal(tbAlmacen)
        _prCargarComboLibreriaPrecioVenta(tbcatprecio)
        tbFechaF.Value = Now.Date

        Dim blah As New Bitmap(New Bitmap(My.Resources.producto), 20, 20)
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico

        btnImprimir.Visible = False
    End Sub
    Public Sub _prCargarComboLibreriaSucursal(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnListarSucursales()
        dt.Rows.Add(0, "TODOS")
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
            mCombo.SelectedIndex = 0
        End If
    End Sub

    Public Sub _prCargarComboLibreriaPrecioVenta(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_prListarCatPrecios()
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("ygnumi").Width = 60
            .DropDownList.Columns("ygnumi").Caption = "COD"
            .DropDownList.Columns.Add("ygdesc").Width = 500
            .DropDownList.Columns("ygdesc").Caption = "CATEGORÍA"
            .ValueMember = "ygnumi"
            .DisplayMember = "ygdesc"
            .DataSource = dt
            .Refresh()
        End With
        If (dt.Rows.Count > 0) Then
            mCombo.SelectedIndex = 0

        End If
    End Sub



    Private Sub _prCrearCarpetaReportes()
        Dim rutaDestino As String = RutaGlobal + "\Reporte\Reporte SaldoProductos\"

        If System.IO.Directory.Exists(RutaGlobal + "\Reporte\Reporte SaldoProductos\") = False Then
            If System.IO.Directory.Exists(RutaGlobal + "\Reporte") = False Then
                System.IO.Directory.CreateDirectory(RutaGlobal + "\Reporte")
                If System.IO.Directory.Exists(RutaGlobal + "\Reporte\Reporte SaldoProductos") = False Then
                    System.IO.Directory.CreateDirectory(RutaGlobal + "\Reporte\Reporte SaldoProductos")
                End If
            Else
                If System.IO.Directory.Exists(RutaGlobal + "\Reporte\Reporte SaldoProductos") = False Then
                    System.IO.Directory.CreateDirectory(RutaGlobal + "\Reporte\Reporte SaldoProductos")

                End If
            End If
        End If
    End Sub
    Private Sub _prCargarDatos()
        Dim _dt As New DataTable

        If (tbAlmacen.SelectedIndex >= 0 And tbcatprecio.SelectedIndex >= 0 And Checktodos.Checked And cbFechaAl.Checked = False) Then
            _dt = L_prReporteUtilidadNueva(tbAlmacen.Value, tbcatprecio.Value)
            '_dt = L_prReporteUtilidadNueva2(tbAlmacen.Value, tbcatprecio.Value)
        End If

        If (tbAlmacen.SelectedIndex >= 0 And tbcatprecio.SelectedIndex >= 0 And checkMayorCero.Checked And cbFechaAl.Checked = False) Then
            '_dt = L_prReporteUtilidadStockMayorCeroNuevo(tbAlmacen.Value, tbcatprecio.Value)
            _dt = L_prReporteUtilidadStockMayorCeroNuevo2(tbAlmacen.Value, tbcatprecio.Value)
        End If

        If (tbAlmacen.SelectedIndex >= 0 And tbcatprecio.SelectedIndex >= 0 And Checktodos.Checked And cbFechaAl.Checked) Then
            '_dt = L_prReporteUtilidadAlNueva(tbAlmacen.Value, tbcatprecio.Value, tbFechaF.Value.ToString("yyyy/MM/dd"))
            _dt = L_prReporteUtilidadNuevo(tbAlmacen.Value, tbcatprecio.Value, tbFechaF.Value.ToString("yyyy/MM/dd"))

        End If

        If (tbAlmacen.SelectedIndex >= 0 And tbcatprecio.SelectedIndex >= 0 And checkMayorCero.Checked And cbFechaAl.Checked) Then
            '_dt = L_prReporteUtilidadmayorNuevo(tbAlmacen.Value, tbcatprecio.Value, tbFechaF.Value.ToString("yyyy/MM/dd"))
            _dt = L_prReporteUtilidadmayorNuevo2(tbAlmacen.Value, tbcatprecio.Value, tbFechaF.Value.ToString("yyyy/MM/dd"))
        End If

        Dim table As DataTable
        If swEstado.Value = True Then
            table = _dt.Clone
            Dim row As DataRow() = _dt.Select("yfap=1")

            For Each ldrRow As DataRow In row
                table.ImportRow(ldrRow)
            Next
        Else
            table = _dt.Copy
        End If


        If table.Rows.Count > 0 Then
            JGrM_Buscador.DataSource = table
            JGrM_Buscador.RetrieveStructure()
            JGrM_Buscador.AlternatingColors = True

            With JGrM_Buscador.RootTable.Columns("abnumi")
                .Width = 90
                .Visible = False
            End With
            With JGrM_Buscador.RootTable.Columns("proveedor")
                .Width = 160
                .Visible = True
                .Caption = "PROVEEDOR"
            End With
            With JGrM_Buscador.RootTable.Columns("unidad")
                .Width = 70
                .Visible = True
                .Caption = "UNIDAD"
            End With
            With JGrM_Buscador.RootTable.Columns("abdesc")
                .Width = 180
                .Visible = True
                .Caption = "DEPÓSITO"
            End With
            With JGrM_Buscador.RootTable.Columns("yfnumi")
                .Width = 120
                .Caption = "CÓD. DYNASYS"
                .Visible = True
            End With
            With JGrM_Buscador.RootTable.Columns("yfcprod")
                .Width = 120
                .Caption = "CÓD. DELTA"
                .Visible = True
            End With
            With JGrM_Buscador.RootTable.Columns("yfcdprod1")
                .Width = 400
                .Caption = "PRODUCTO"
                .Visible = True
            End With
            With JGrM_Buscador.RootTable.Columns("yfMed")
                .Width = 100
                .Visible = False
            End With
            With JGrM_Buscador.RootTable.Columns("yfap")
                .Width = 100
                .Visible = False
            End With
            With JGrM_Buscador.RootTable.Columns("iccprod")
                .Width = 100
                .Visible = False
            End With
            With JGrM_Buscador.RootTable.Columns("iccven")
                .Width = 100
                .Caption = "STOCK"
                .Visible = True
                .FormatString = "0.00"
            End With
            With JGrM_Buscador.RootTable.Columns("yccod3")
                .Width = 100
                .Visible = False
            End With
            With JGrM_Buscador.RootTable.Columns("ycdes3")
                .Width = 380
                .Visible = False
            End With
            With JGrM_Buscador.RootTable.Columns("preciou")
                .Width = 100
                .Caption = "PRECIO"
                .Visible = True
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .FormatString = "0.00"
            End With
            With JGrM_Buscador.RootTable.Columns("total")
                .Width = 100
                .Caption = "TOTAL"
                .Visible = True
                .FormatString = "0.00"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .AggregateFunction = AggregateFunction.Sum
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
            End With

        Else
            JGrM_Buscador.ClearStructure()
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "No existe datos para mostrar".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.TopCenter)
        End If

    End Sub

#End Region

    Private Sub F1_Productos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()
    End Sub


    Private Sub ButtonX2_Click(sender As Object, e As EventArgs) Handles btExcel.Click
        _prCrearCarpetaReportes()
        Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
        If (P_ExportarExcel(RutaGlobal + "\Reporte\Reporte SaldoProductos")) Then
            ToastNotification.Show(Me, "EXPORTACIÓN DE LISTA DE PRODUCTOS EXITOSA..!!!",
                                       img, 2000,
                                       eToastGlowColor.Green,
                                       eToastPosition.BottomCenter)
        Else
            ToastNotification.Show(Me, "FALLO AL EXPORTACIÓN DE LISTA DE PRODUCTOS..!!!",
                                       My.Resources.WARNING, 2000,
                                       eToastGlowColor.Red,
                                       eToastPosition.BottomLeft)
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
                Dim _fila As Integer = JGrM_Buscador.GetRows.Length
                Dim _columna As Integer = JGrM_Buscador.RootTable.Columns.Count
                Dim _archivo As String = _ubicacion & "\SaldoProductos_" & Now.Date.Day &
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

                'Pbx_Precios.Visible = True
                'Pbx_Precios.Minimum = 1
                'Pbx_Precios.Maximum = Dgv_Precios.RowCount
                'Pbx_Precios.Value = 1

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


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        _Inter = _Inter + 1
        If _Inter = 1 Then
            Me.WindowState = FormWindowState.Normal

        Else
            Me.Opacity = 100
            Timer1.Enabled = False
        End If
    End Sub

    Private Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        _prCargarDatos()
    End Sub

    Private Sub btnExportarExcel_Click(sender As Object, e As EventArgs) Handles btnExportarExcel.Click
        _prCrearCarpetaReportes()
        Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
        If (P_ExportarExcel(RutaGlobal + "\Reporte\Reporte SaldoProductos")) Then
            ToastNotification.Show(Me, "EXPORTACIÓN DE SALDO DE PRODUCTOS EXITOSA..!!!",
                                       img, 2000,
                                       eToastGlowColor.Green,
                                       eToastPosition.BottomCenter)
        Else
            ToastNotification.Show(Me, "FALLÓ LA EXPORTACIÓN DE SALDO DE PRODUCTOS..!!!",
                                       My.Resources.WARNING, 2000,
                                       eToastGlowColor.Red,
                                       eToastPosition.BottomLeft)
        End If
    End Sub

    Private Sub cbFechaAl_CheckedChanged(sender As Object, e As EventArgs) Handles cbFechaAl.CheckedChanged
        If (cbFechaAl.Checked = True) Then
            tbFechaF.Enabled = True
        Else
            tbFechaF.Enabled = False
        End If
    End Sub
End Class