Imports Logica.AccesoLogica
Imports DevComponents.Editors
Imports DevComponents.DotNetBar.SuperGrid
Imports System.IO
Imports System.Drawing.Printing
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls

Public Class F0_LibroVenta
    Dim _Inter As Integer = 0

#Region "Variables Globales"

    Dim _DuracionSms As Integer = 5
    Dim _DsLV As DataTable
    Public _modulo As SideNavItem
    Public _nameButton As String
    Public _tab As SuperTabItem
#End Region

#Region "Eventos"

    Private Sub P_LibroVentas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        P_Inicio()
    End Sub

#End Region

#Region "Metodos"

    Private Sub P_Inicio()
        'L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)

        'Me.WindowState = FormWindowState.Maximized
        Me.Text = "L I B R O   D E   V E N T A S"

        btnNuevo.Visible = False
        btnModificar.Visible = False
        btnEliminar.Visible = False
        btnGrabar.Visible = False
        btnSalir.Visible = False
        btnImprimir.Visible = False

        btnPrimero.Visible = False
        btnAnterior.Visible = False
        btnSiguiente.Visible = False
        btnUltimo.Visible = False

        LblPaginacion.Visible = False
        BubbleBarUsuario.Visible = False

        P_ComboAnho()
        P_ComboRazonSocial()
        P_ArmarGrilla()
        If (Cb1Anho.Items.Count > 0) Then
            Cb1Anho.SelectedIndex = Cb1Anho.Items.Count - 1
        End If
        If (Cb2Meses.Items.Count > 0) Then
            Cb2Meses.SelectedIndex = Now.Date.Month - 1
        End If
        If (Cb3RazonSocial.Items.Count > 0) Then
            Cb3RazonSocial.SelectedIndex = 0
        End If

    End Sub

#End Region

    Private Sub P_ComboAnho()
        Cb1Anho.Items.Clear()
        Dim _Dt As DataTable = L_ObtenerAnhoFactura()
        Dim _item As ComboItem
        For Each _f As DataRow In _Dt.Rows
            _item = New ComboItem()
            _item.Text = _f.Item("anho").ToString
            _item.Value = _f.Item("anho").ToString
            Cb1Anho.Items.Add(_item)
        Next
    End Sub

    Private Sub P_ArmarGrilla()

        DgdLCV.PrimaryGrid.Columns.Clear()
        'Alto de la Fila de Nombres de Columnas
        DgdLCV.PrimaryGrid.ColumnHeader.RowHeight = 25

        'Mostrar u Ocultar la Fila de Filtrado
        DgdLCV.PrimaryGrid.EnableColumnFiltering = True
        DgdLCV.PrimaryGrid.EnableFiltering = True
        DgdLCV.PrimaryGrid.EnableRowFiltering = True
        DgdLCV.PrimaryGrid.Filter.Visible = True

        'Para Mostrar u Ocultar la Columna de Cabesera de las Filas
        DgdLCV.PrimaryGrid.ShowRowHeaders = True

        'Para Mostrar el Indice de la Grilla
        DgdLCV.PrimaryGrid.RowHeaderIndexOffset = 1
        DgdLCV.PrimaryGrid.ShowRowGridIndex = True

        'Alto de las Filas
        DgdLCV.PrimaryGrid.DefaultRowHeight = 22

        'Alternar Colores de las Filas
        DgdLCV.PrimaryGrid.UseAlternateRowStyle = True

        'Para permitir o denegar el cambio de tamaño de la Filas
        DgdLCV.PrimaryGrid.AllowRowResize = False

        'Para que el Tamaño de las Columnas se pongan automaticamente
        'DgdLCV.PrimaryGrid.ColumnAutoSizeMode = ColumnAutoSizeMode.DisplayedCells

        DgdLCV.PrimaryGrid.SelectionGranularity = SelectionGranularity.RowWithCellHighlight

        Dim col As GridColumn

        ''Nro
        'col = New GridColumn("Nro")
        'col.HeaderText = "Nro"
        'col.EditorType = GetType(GridTextBoxXEditControl)
        'col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        'col.ReadOnly = True
        'col.Visible = True
        'col.Width = 60
        'DgdLCV.PrimaryGrid.Columns.Add(col)

        'Codigo
        col = New GridColumn("fvanumi")
        col.HeaderText = "Codigo"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = False
        col.Width = 80
        DgdLCV.PrimaryGrid.Columns.Add(col)

        'Fecha
        col = New GridColumn("fvafec")
        col.HeaderText = "Fecha"
        col.EditorType = GetType(GridDateTimePickerEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 100
        DgdLCV.PrimaryGrid.Columns.Add(col)

        'Nro de Factura
        col = New GridColumn("fvanfac")
        col.HeaderText = "Factura"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 80
        DgdLCV.PrimaryGrid.Columns.Add(col)

        'Nro Autorizacion
        col = New GridColumn("fvaautoriz")
        col.HeaderText = "Autorizacion"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 100
        DgdLCV.PrimaryGrid.Columns.Add(col)

        'Estado
        col = New GridColumn("fvaest")
        col.HeaderText = "Estado"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleCenter
        col.ReadOnly = True
        col.Visible = True
        col.Width = 80
        DgdLCV.PrimaryGrid.Columns.Add(col)

        'Nit
        col = New GridColumn("fvanitcli")
        col.HeaderText = "NIT"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 150
        DgdLCV.PrimaryGrid.Columns.Add(col)

        'Razon Social
        col = New GridColumn("fvadescli")
        col.HeaderText = "Razon Social"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleLeft
        col.ReadOnly = True
        col.Visible = True
        col.Width = 250
        DgdLCV.PrimaryGrid.Columns.Add(col)

        'A
        col = New GridColumn("fvastot")
        col.HeaderText = "A"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 80
        DgdLCV.PrimaryGrid.Columns.Add(col)

        'B
        col = New GridColumn("fvaimpsi")
        col.HeaderText = "B"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 80
        DgdLCV.PrimaryGrid.Columns.Add(col)

        'C
        col = New GridColumn("fvaimpeo")
        col.HeaderText = "C"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleLeft
        col.ReadOnly = True
        col.Visible = True
        col.Width = 80
        DgdLCV.PrimaryGrid.Columns.Add(col)

        'D
        col = New GridColumn("fvaimptc")
        col.HeaderText = "D"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 80
        DgdLCV.PrimaryGrid.Columns.Add(col)

        'E
        col = New GridColumn("fvasubtotal")
        col.HeaderText = "E"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 80
        DgdLCV.PrimaryGrid.Columns.Add(col)

        'F
        col = New GridColumn("fvadesc")
        col.HeaderText = "F"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 80
        DgdLCV.PrimaryGrid.Columns.Add(col)

        'G
        col = New GridColumn("fvatotal")
        col.HeaderText = "G"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 80
        DgdLCV.PrimaryGrid.Columns.Add(col)

        'H
        col = New GridColumn("fvadebfis")
        col.HeaderText = "H"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 80
        DgdLCV.PrimaryGrid.Columns.Add(col)

        'Codigo de Control
        col = New GridColumn("fvaccont")
        col.HeaderText = "Cod Control"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleLeft
        col.ReadOnly = True
        col.Visible = True
        col.Width = 100
        DgdLCV.PrimaryGrid.Columns.Add(col)

        'Fecha Limite
        col = New GridColumn("fvaflim")
        col.HeaderText = ""
        col.EditorType = GetType(GridDateTimePickerEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleLeft
        col.ReadOnly = True
        col.Visible = False
        col.Width = 0
        DgdLCV.PrimaryGrid.Columns.Add(col)

        'Codigo de Almacen
        col = New GridColumn("fvaalm")
        col.HeaderText = ""
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleLeft
        col.ReadOnly = True
        col.Visible = False
        col.Width = 0
        DgdLCV.PrimaryGrid.Columns.Add(col)

        'Nombre Almacen
        col = New GridColumn("scneg")
        col.HeaderText = ""
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleLeft
        col.ReadOnly = True
        col.Visible = False
        col.Width = 0
        DgdLCV.PrimaryGrid.Columns.Add(col)

    End Sub

    Private Sub P_ComboRazonSocial()
        Cb3RazonSocial.Items.Clear()
        Dim _Dt As DataTable = L_ObtenerSucursalesFactura()
        Dim _item As ComboItem
        For Each _f As DataRow In _Dt.Rows
            _item = New ComboItem()
            _item.Text = _f.Item("scneg").ToString
            _item.Value = "1" '_f.Item("yacia").ToString
            _item.Tag = _f.Item("scnit").ToString
            Cb3RazonSocial.Items.Add(_item)
        Next
    End Sub

    Private Sub Cb3RazonSocial_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cb3RazonSocial.SelectedIndexChanged
        Dim _item As ComboItem = Cb3RazonSocial.SelectedItem
        Tb1CodRazonSocial.Text = _item.Value
        Tb2NitRazonSocial.Text = _item.Tag
    End Sub

    Private Sub Bt1Generar_Click(sender As Object, e As EventArgs) Handles Bt1Generar.Click
        P_LlenarDatosGrilla()
    End Sub

    Private Sub Bt2Reporte_Click(sender As Object, e As EventArgs) Handles Bt2Reporte.Click
        P_GenerarReporte()
    End Sub

    Private Sub Bt3Excel_Click(sender As Object, e As EventArgs) Handles Bt3Excel.Click
        P_GenerarExcel()
    End Sub

    Private Sub Bt4Txt_Click(sender As Object, e As EventArgs) Handles Bt4Txt.Click
        P_GenerarTxt()
    End Sub

    Private Sub P_LlenarDatosGrilla()
        _DsLV = New DataTable
        _DsLV = L_fnObtenerLibroVenta(Tb1CodRazonSocial.Text, CStr(Cb2Meses.SelectedIndex + 1), Cb1Anho.Text)

        For Each fil As DataRow In _DsLV.Rows
            If (fil.Item("fvaest").ToString.Equals("A")) Then
                fil.Item("fvadescli") = "ANULADA"
                fil.Item("fvanitcli") = "0"
                fil.Item("fvastot") = 0
                fil.Item("fvasubtotal") = 0
                fil.Item("fvadesc") = 0
                fil.Item("fvatotal") = 0
                fil.Item("fvadebfis") = 0
            End If
        Next

        DgdLCV.PrimaryGrid.Rows.Clear()

        DgdLCV.PrimaryGrid.DataSource = _DsLV

        DgdLCV.PrimaryGrid.SetActiveRow(CType(DgdLCV.PrimaryGrid.ActiveRow, GridRow))
    End Sub

    Private Sub P_GenerarReporte()
        Dim _Ds1 As DataSet
        If Not IsNothing(P_Global.Visualizador) Then
            P_Global.Visualizador.Close()
        End If

        _Ds1 = L_ObtenerRutaImpresora("1") ' Datos de Impresion
        If (_Ds1.Tables(0).Rows(0).Item("cbvp")) Then 'Vista Previa de la Ventana de Vizualización 1 = True 0 = False
            P_Global.Visualizador = New Visualizador 'Comentar
        End If

        Dim objrep As New R_LibroVenta
        objrep.SetDataSource(_DsLV)
        objrep.SetParameterValue("Anho", Cb1Anho.Text)
        objrep.SetParameterValue("Mes", Cb2Meses.Text)
        objrep.SetParameterValue("RazonSocial", Cb3RazonSocial.Text)
        objrep.SetParameterValue("Nit", Tb2NitRazonSocial.Text)

        If (_Ds1.Tables(0).Rows(0).Item("cbvp")) Then 'Vista Previa de la Ventana de Vizualización 1 = True 0 = False
            P_Global.Visualizador.CrGeneral.ReportSource = objrep 'Comentar
            P_Global.Visualizador.ShowDialog() 'Comentar
            P_Global.Visualizador.BringToFront() 'Comentar
        End If

        Dim pd As New PrintDocument()
        pd.PrinterSettings.PrinterName = _Ds1.Tables(0).Rows(0).Item("cbrut").ToString
        If (Not pd.PrinterSettings.IsValid) Then
            ToastNotification.Show(Me, "La Impresora ".ToUpper + _Ds1.Tables(0).Rows(0).Item("cbrut").ToString + Chr(13) + "No Existe".ToUpper,
                                   My.Resources.INFORMATION, _DuracionSms * 1000,
                                   eToastGlowColor.Blue, eToastPosition.BottomLeft)
        Else
            objrep.PrintOptions.PrinterName = _Ds1.Tables(0).Rows(0).Item("cbrut").ToString '"EPSON TM-T20II Receipt5 (1)"
            objrep.PrintToPrinter(1, False, 1, 1)
        End If

        'Preguntando si existe el Directorio
        If (Not Directory.Exists(gs_CarpetaRaiz + "\LCV")) Then
            Directory.CreateDirectory(gs_CarpetaRaiz + "\LCV")
        End If
        'Exportando a Excel el Reporte
        objrep.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, gs_CarpetaRaiz + "\LCV\LCV_" + Cb3RazonSocial.Text + ".pdf")
    End Sub

    Private Sub P_GenerarExcel()
        If (P_Exportar(".csv")) Then
            ToastNotification.Show(Me, "EXPORTACIÓN DE LISTA DE PRECIOS EXITOSA..!!!",
                                       My.Resources.OK, _DuracionSms * 1000,
                                       eToastGlowColor.Green,
                                       eToastPosition.BottomLeft)
        Else
            ToastNotification.Show(Me, "FALLO AL EXPORTACIÓN DE LISTA DE PRECIOS..!!!",
                                       My.Resources.WARNING, _DuracionSms * 1000,
                                       eToastGlowColor.Red,
                                       eToastPosition.BottomLeft)
        End If
    End Sub

    Private Sub P_GenerarTxt()
        If (P_Exportar(".txt")) Then
            ToastNotification.Show(Me, "EXPORTACIÓN DE LISTA DE PRECIOS EXITOSA..!!!",
                                       My.Resources.OK, _DuracionSms * 1000,
                                       eToastGlowColor.Green,
                                       eToastPosition.BottomLeft)
        Else
            ToastNotification.Show(Me, "FALLO AL EXPORTACIÓN DE LISTA DE PRECIOS..!!!",
                                       My.Resources.WARNING, _DuracionSms * 1000,
                                       eToastGlowColor.Red,
                                       eToastPosition.BottomLeft)
        End If
    End Sub

    Private Function P_Exportar(ext As String) As Boolean
        Dim _ubicacion As String
        'Dim _directorio As New FolderBrowserDialog

        If (1 = 1) Then 'If(_directorio.ShowDialog = Windows.Forms.DialogResult.OK) Then
            '_ubicacion = _directorio.SelectedPath
            _ubicacion = gs_CarpetaRaiz
            Try
                Dim _stream As Stream
                Dim _escritor As StreamWriter
                Dim _fila As Integer = DgdLCV.PrimaryGrid.Rows.Count
                Dim _columna As Integer = DgdLCV.PrimaryGrid.Columns.Count
                Dim _archivo As String = _ubicacion & "\LCV_" & Now.Date.Day &
                    "." & Now.Date.Month & "." & Now.Date.Year & "_" & Now.Hour & "." & Now.Minute & "." & Now.Second & ext
                Dim _linea As String = ""
                Dim _filadata = 0, columndata As Int32 = 0
                File.Delete(_archivo)
                _stream = File.OpenWrite(_archivo)
                _escritor = New StreamWriter(_stream, System.Text.Encoding.UTF8)

                For Each _col As GridColumn In DgdLCV.PrimaryGrid.Columns
                    If (_col.Visible) Then
                        _linea = _linea & _col.HeaderText & IIf(ext.Equals(".txt"), "|", ";")
                    End If
                Next
                _linea = Mid(CStr(_linea), 1, _linea.Length - 1)
                _escritor.WriteLine(_linea)
                _linea = Nothing

                'Pbx_Precios.Visible = True
                'Pbx_Precios.Minimum = 1
                'Pbx_Precios.Maximum = Dgv_Precios.RowCount
                'Pbx_Precios.Value = 1

                For Each _fil As GridRow In DgdLCV.PrimaryGrid.Rows
                    For Each _col As GridColumn In DgdLCV.PrimaryGrid.Columns
                        If (_col.Visible) Then
                            _linea = _linea & CStr(_fil.Cells(_col.Name).Value) & IIf(ext.Equals(".txt"), "|", ";")
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
                    If (MessageBox.Show("DESEA ABRIR EL ARCHIVO?", "PREGUNTA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes) Then
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
End Class