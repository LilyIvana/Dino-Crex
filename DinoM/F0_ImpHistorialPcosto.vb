
Imports Logica.AccesoLogica
Imports Janus.Windows.GridEX
Imports DevComponents.DotNetBar
Imports System.IO
Imports DevComponents.DotNetBar.SuperGrid
Imports GMap.NET.MapProviders
Imports GMap.NET
Imports GMap.NET.WindowsForms.Markers
Imports GMap.NET.WindowsForms
Imports GMap.NET.WindowsForms.ToolTips
Imports System.Drawing
Imports DevComponents.DotNetBar.Controls
Imports System.Threading
Imports System.Drawing.Text
Imports System.Data.OleDb

Imports System.Reflection
Imports System.Runtime.InteropServices
Imports Microsoft.Office.Interop


Public Class F0_ImpHistorialPcosto
    Dim _Inter As Integer = 0

    Dim RutaGlobal As String = gs_CarpetaRaiz
#Region "Variables Globales"
    Dim precio As DataTable
    Public _nameButton As String
    Public _modulo As SideNavItem
    Public _tab As SuperTabItem
    Public CostosImport As New DataTable

#End Region
#Region "Métodos Privados"
    Private Sub _IniciarTodo()
        _prAsignarPermisos()
        Me.Text = "HISTORIAL COSTOS"
        tbFecha.Value = Now.Date
        dtFechaIni.Value = Now.Date
        dtFechaFin.Value = Now.Date

        Dim blah As New Bitmap(New Bitmap(My.Resources.hojaruta), 20, 20)
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico
        _prCargarComboLibreriaMotivo(cbMotivo, 11, 1)
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

    Private Sub _prInhabiliitar()

        btnModificar.Enabled = True
        btnGrabar.Enabled = False
        '_prCargarTabla(True)

    End Sub
    Private Sub _prhabilitar()
        btnGrabar.Enabled = True
    End Sub

    Public Function _fnAccesible()
        Return btnGrabar.Enabled = True
    End Function

    Public Function _fnSiguienteNumero(num As Integer)
        Return num + 1
    End Function


    Public Function P_ExportarExcel(_ruta As String) As Boolean
        Dim _ubicacion As String
        'Dim _directorio As New FolderBrowserDialog

        If (1 = 1) Then 'If(_directorio.ShowDialog = Windows.Forms.DialogResult.OK) Then
            '_ubicacion = _directorio.SelectedPath
            _ubicacion = _ruta
            Try
                grDatos.HeaderFormatStyle.IsReadOnly()
                Dim _stream As Stream
                Dim _escritor As StreamWriter
                Dim _fila As Integer = grDatos.GetRows.Length
                Dim _columna As Integer = grDatos.RootTable.Columns.Count
                Dim _archivo As String = _ubicacion & "\HistorialCostos" & "_" & Now.Date.Day &
                    "." & Now.Date.Month & "." & Now.Date.Year & "_" & Now.Hour & "." & Now.Minute & "." & Now.Second & ".csv"
                '"." & Now.Date.Month & "." & Now.Date.Year & "_" & Now.Hour & "." & Now.Minute & "." & Now.Second & ".csv"
                Dim _linea As String = ""
                Dim _filadata = 0, columndata As Int32 = 0
                File.Delete(_archivo)
                _stream = File.OpenWrite(_archivo)
                _escritor = New StreamWriter(_stream, System.Text.Encoding.UTF8)

                For Each _col As GridEXColumn In grDatos.RootTable.Columns
                    If (_col.Visible) Then
                        _linea = _linea & _col.Caption & ";"
                    End If
                Next
                _linea = Mid(CStr(_linea), 1, _linea.Length - 1)
                _escritor.WriteLine(_linea)
                _linea = Nothing

                For Each _fil As GridEXRow In grDatos.GetRows
                    For Each _col As GridEXColumn In grDatos.RootTable.Columns
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

    Private Sub _prCrearCarpetaReportes()

        Dim rutaDestino As String = RutaGlobal + "\Reporte\Reporte PreciosCosto\"

        If System.IO.Directory.Exists(RutaGlobal + "\Reporte\Reporte PreciosCosto\") = False Then
            If System.IO.Directory.Exists(RutaGlobal + "\Reporte") = False Then
                System.IO.Directory.CreateDirectory(RutaGlobal + "\PreciosCosto")
                If System.IO.Directory.Exists(RutaGlobal + "\Reporte\Reporte PreciosCosto") = False Then
                    System.IO.Directory.CreateDirectory(RutaGlobal + "\Reporte\Reporte PreciosCosto")
                End If
            Else
                If System.IO.Directory.Exists(RutaGlobal + "\Reporte\Reporte PreciosCosto") = False Then
                    System.IO.Directory.CreateDirectory(RutaGlobal + "\Reporte\Reporte PreciosCosto")

                End If
            End If
        End If
    End Sub


    Private Sub MP_ImportarExcel()
        Try
            Dim folder As String = ""
            Dim doc As String = "Hoja1"
            Dim openfile1 As OpenFileDialog = New OpenFileDialog()

            'Dim xlw As Excel.Workbook
            'Dim xlsheet As Excel.Worksheets
            'Dim xl As New Excel.Application

            If openfile1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                folder = openfile1.FileName

                'doc = Mid(openfile1.SafeFileName, 1, 31)
                'xlw = xl.Workbooks.Open("f:\Documentos\Excel\JSanchez06-11-23.xls")
                'xlsheet = xlw.worksheet(1)
                ''doc = xlsheet.ToString
                'xlsheet.Name = "hoja1"
                'xlw.Save()
            End If

            If True Then
                Dim pathconn As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & folder & ";Extended Properties='Excel 12.0 Xml;HDR=Yes'"
                'Dim pathconn As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & folder & ";"
                'Dim pathconn As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & folder & ";Extended Properties=""Excel 8.0;HDR=YES;"""

                Dim con As OleDbConnection = New OleDbConnection(pathconn)
                Dim MyDataAdapter As OleDbDataAdapter = New OleDbDataAdapter("Select * from [" & doc & "$]", con)
                con.Open()

                MyDataAdapter.Fill(CostosImport)
                con.Close()

                'Dim dt = CostosImport.Copy
                'Dim fecha = dt.Rows(0).Item("Fecha1")

            End If

        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub

    Private Sub MP_PasarDatos()
        Try
            If CostosImport.Rows.Count > 0 Then

                If CostosImport.Columns.Count = 6 Then
                    Dim Tablaaux As DataTable
                    ''Validaciones 
                    For i = 0 To CostosImport.Rows.Count - 1
                        'Validación para comprobar que el excel no contenga código dynasys vacio con letra o menor a 0
                        If IsDBNull(CostosImport.Rows(i).Item("CODDYNASYS")) Or (IIf(CostosImport.Rows(i).Item("CODDYNASYS").ToString = String.Empty, 0, CostosImport.Rows(i).Item("CODDYNASYS")) < 0) Then

                            ToastNotification.Show(Me, "No se puede realizar la importación porque alguna de las filas contiene el campo de código Dynasys  vacío, con letra o con valor negativo, verifique por favor.".ToUpper,
                                                       My.Resources.WARNING, 9500, eToastGlowColor.Green, eToastPosition.TopCenter)
                            Exit Sub
                        End If

                        Tablaaux = CostosImport.Copy
                        Dim aux = Tablaaux.Select("CODDYNASYS=" + CostosImport.Rows(i).Item("CODDYNASYS").ToString)
                        If aux.Length > 1 Then
                            ToastNotification.Show(Me, "No se puede realizar la importación porque el codigo:  ".ToUpper &
                           CostosImport.Rows(i).Item("CODDYNASYS").ToString & " existe  ".ToUpper & aux.Length.ToString & " veces en la lista".ToUpper,
                           My.Resources.WARNING, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)

                            Exit Sub
                        End If


                        If IsDBNull(CostosImport.Rows(i).Item("COSTO ANTIGUO")) Or (IIf(CostosImport.Rows(i).Item("COSTO ANTIGUO").ToString = String.Empty, 0, CostosImport.Rows(i).Item("COSTO ANTIGUO")) < 0) Or
                           IsDBNull(CostosImport.Rows(i).Item("COSTO NUEVO")) Or (IIf(CostosImport.Rows(i).Item("COSTO NUEVO").ToString = String.Empty, 0, CostosImport.Rows(i).Item("COSTO NUEVO")) < 0) Or
                           IsDBNull(CostosImport.Rows(i).Item("COSTO NUEVO PONDERADO")) Or (IIf(CostosImport.Rows(i).Item("COSTO NUEVO PONDERADO").ToString = String.Empty, 0, CostosImport.Rows(i).Item("COSTO NUEVO PONDERADO")) < 0) Then

                            ToastNotification.Show(Me, "No se puede realizar la importación porque el código Dynasys: ".ToUpper &
                            CostosImport.Rows(i).Item("CODDYNASYS") & " tiene una de los costos con valor negativo, vacío o con letra, corrija por favor".ToUpper,
                            My.Resources.WARNING, 8500, eToastGlowColor.Green, eToastPosition.TopCenter)

                            Exit Sub
                        End If
                    Next

                    _prCargarTablaImport(CostosImport)
                    btnGrabarImp.Visible = True

                Else
                    CostosImport.Reset()
                    ToastNotification.Show(Me, "No puede importar porque el excel tiene que tener 6 columnas, usted modificó el excel, corrija por favor".ToUpper,
                                         My.Resources.WARNING, 7000, eToastGlowColor.Green, eToastPosition.TopCenter)
                End If
            Else

                ToastNotification.Show(Me, "No se logró importar el excel".ToUpper,
                                     My.Resources.WARNING, 2700, eToastGlowColor.Green, eToastPosition.TopCenter)
            End If

        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub

    Public Sub _prCargarTablaImport(dtImport As DataTable) ''Bandera = true si es que haiq cargar denuevo la tabla de Precio Bandera =false si solo cargar datos al Janus con el precio antepuesto

        grDatos.BoundMode = Janus.Data.BoundMode.Bound
        grDatos.DataSource = dtImport
        grDatos.RetrieveStructure()

        With grDatos.RootTable.Columns("CODDYNASYS")
            .Caption = "COD DYNASYS"
            .Width = 100
            .Visible = True
        End With
        With grDatos.RootTable.Columns("CODDELTA")
            .Caption = "COD DELTA"
            .Width = 100
            .Visible = True
        End With

        With grDatos.RootTable.Columns("DETALLE")
            .Caption = "PRODUCTO"
            .Width = 350
            .Visible = True
        End With
        With grDatos.RootTable.Columns("COSTO ANTIGUO")
            .Caption = "COSTO ANTIGUO"
            .Width = 120
            .Visible = True
            .TextAlignment = TextAlignment.Far
            .FormatString = "0.00"
        End With
        With grDatos.RootTable.Columns("COSTO NUEVO")
            .Caption = "NUEVO COSTO"
            .Width = 120
            .Visible = True
            .TextAlignment = TextAlignment.Far
            .FormatString = "0.00"
        End With
        With grDatos.RootTable.Columns("COSTO NUEVO PONDERADO")
            .Caption = "NUEVO COSTO PONDERADO"
            .Width = 160
            .Visible = True
            .TextAlignment = TextAlignment.Far
            .FormatString = "0.00"
        End With

        'Habilitar Filtradores
        With grDatos
            .GroupByBoxVisible = False
            '.FilterRowFormatStyle.BackColor = Color.Blue
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            '.FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .FilterMode = FilterMode.Automatic
            'Diseño de la tabla
            .VisualStyle = VisualStyle.Office2007
            .SelectionMode = SelectionMode.SingleSelection
            .AlternatingColors = True

            .RecordNavigator = True
            .RecordNavigatorText = "COSTOS"
        End With

    End Sub
    Private Sub MostrarMensajeError(mensaje As String)
        ToastNotification.Show(Me,
                               mensaje.ToUpper,
                               My.Resources.WARNING,
                               5500,
                               eToastGlowColor.Red,
                               eToastPosition.TopCenter)
    End Sub


#End Region


#Region "Métodos Formulario"
    Private Sub F0_Precios_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _IniciarTodo()
        _prInhabiliitar()
    End Sub
    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        _prhabilitar()
        btnModificar.Enabled = False
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        If (_fnAccesible()) Then
            _prInhabiliitar()
        Else
            _modulo.Select()
            Me.Close()
        End If
    End Sub

#End Region


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        _Inter = _Inter + 1
        If _Inter = 1 Then
            Me.WindowState = FormWindowState.Normal
        Else
            Me.Opacity = 100
            Timer1.Enabled = False
        End If
    End Sub


    Private Sub btnImportar_Click(sender As Object, e As EventArgs) Handles btnImportar.Click
        CostosImport.Reset()
        CostosImport.Clear()
        CostosImport = New DataTable
        MP_ImportarExcel()
        MP_PasarDatos()
    End Sub

    Private Sub btnGrabarImp_Click(sender As Object, e As EventArgs) Handles btnGrabarImp.Click
        'Validar()
        If _ValidarCampos() = False Then
            Exit Sub
        End If

        If CostosImport.Rows.Count > 0 Then
            Dim fecha As Date = Now.Date.ToString("dd/MM/yyyy")
            Dim importar As Boolean = L_fnImportarHistorialCostos(CostosImport, tbFecha.Value, cbMotivo.Value, tbObservacion.Text.Trim, gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina)
            If importar Then
                ToastNotification.Show(Me, "IMPORTACIÓN DE COSTOS EXITOSA!!! ",
                          My.Resources.OK, 4000,
                          eToastGlowColor.Green,
                          eToastPosition.TopCenter)
                btnGrabarImp.Visible = False
                _Limpiar()
            Else
                ToastNotification.Show(Me, "FALLÓ LA IMPORTACIÓN DE COSTOS!!!",
                          My.Resources.WARNING, 4000,
                          eToastGlowColor.Red,
                          eToastPosition.TopCenter)
            End If

        Else
            ToastNotification.Show(Me, "NO PUEDE GRABAR SI NO HAY DATOS EN LA GRILLA!!!",
                             My.Resources.WARNING, 4000,
                             eToastGlowColor.Red,
                             eToastPosition.TopCenter)
        End If

    End Sub
    Public Function _ValidarCampos() As Boolean
        If tbFecha.Value > Now.Date Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "La fecha no puede ser mayor a la fecha actual".ToUpper, img, 2800, eToastGlowColor.Red, eToastPosition.BottomCenter)
            tbFecha.Focus()
            Return False
        End If

        If (cbMotivo.SelectedIndex < 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Seleccione un Motivo".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            cbMotivo.Focus()
            Return False
        End If

        If (tbObservacion.Text = String.Empty) Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor debe colocar una Observación".ToUpper, img, 2500, eToastGlowColor.Red, eToastPosition.BottomCenter)
            tbObservacion.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub _Limpiar()

        tbFecha.Value = Now.Date
        cbMotivo.SelectedIndex = -1
        tbObservacion.Clear()

        grDatos.ClearStructure()
    End Sub
    Private Sub grDatos_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grDatos.EditingCell
        e.Cancel = True
    End Sub

    Private Sub cbMotivo_ValueChanged(sender As Object, e As EventArgs) Handles cbMotivo.ValueChanged
        If btnGrabarImp.Visible = True Then
            If cbMotivo.Text <> String.Empty Then
                tbObservacion.Text = cbMotivo.Text
            End If
        End If
    End Sub

    Private Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        _prCargarHistorial()
    End Sub
    Private Sub _prCargarHistorial()
        Dim fechaDesde As DateTime = dtFechaIni.Value.ToString("yyyy/MM/dd")
        Dim fechaHasta As DateTime = dtFechaFin.Value.ToString("yyyy/MM/dd")

        Dim dt As DataTable = L_HistorialCostos(fechaDesde, fechaHasta)

        If dt.Rows.Count > 0 Then

            L_fnBotonGenerar(gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina, 0, "HISTORIAL COSTOS", "HISTORIAL COSTOS")

            grDatos.DataSource = dt
            grDatos.RetrieveStructure()
            grDatos.AlternatingColors = True

            With grDatos.RootTable.Columns("bfnumi")
                .Visible = False
            End With
            With grDatos.RootTable.Columns("bffecha")
                .Caption = "FECHA"
                .Width = 100
                .Visible = True
            End With
            With grDatos.RootTable.Columns("bfprod")
                .Caption = "COD DYNASYS"
                .Width = 100
                .Visible = True
            End With
            With grDatos.RootTable.Columns("CodDelta")
                .Caption = "COD DELTA"
                .Width = 100
                .Visible = True
            End With
            With grDatos.RootTable.Columns("yfcdprod1")
                .Caption = "PRODUCTO"
                .Width = 350
                .Visible = True
            End With
            With grDatos.RootTable.Columns("bfcostoant")
                .Caption = "COSTO ANTIGUO"
                .Width = 120
                .Visible = True
                .TextAlignment = TextAlignment.Far
                .FormatString = "0.00"
            End With
            With grDatos.RootTable.Columns("bfcostonue")
                .Caption = "NUEVO COSTO"
                .Width = 120
                .Visible = True
                .TextAlignment = TextAlignment.Far
                .FormatString = "0.00"
            End With
            With grDatos.RootTable.Columns("bfcostopond")
                .Caption = "NUEVO COSTO PONDERADO"
                .Width = 160
                .Visible = True
                .TextAlignment = TextAlignment.Far
                .FormatString = "0.00"
            End With
            With grDatos.RootTable.Columns("bfmotivo")
                .Visible = False
            End With
            With grDatos.RootTable.Columns("bfobs")
                .Caption = "OBSERVACIÓN 1"
                .Width = 170
                .Visible = True
            End With
            With grDatos.RootTable.Columns("bfobs2")
                .Caption = "OBSERVACIÓN 2"
                .Width = 170
                .Visible = True
            End With
            With grDatos.RootTable.Columns("bffact")
                .Visible = False
            End With
            With grDatos.RootTable.Columns("bfhact")
                .Visible = False
            End With
            With grDatos.RootTable.Columns("bfuact")
                .Caption = "USUARIO"
                .Width = 160
                .Visible = True
            End With

            'Habilitar Filtradores
            With grDatos
                .GroupByBoxVisible = False
                '.FilterRowFormatStyle.BackColor = Color.Blue
                .DefaultFilterRowComparison = FilterConditionOperator.Contains
                '.FilterMode = FilterMode.Automatic
                .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
                .FilterMode = FilterMode.Automatic
                'Diseño de la tabla
                .VisualStyle = VisualStyle.Office2007
                .SelectionMode = SelectionMode.SingleSelection
                .AlternatingColors = True

                .RecordNavigator = True
                .RecordNavigatorText = "COSTOS"
            End With

        Else
            grDatos.ClearStructure()
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "No existe datos para mostrar".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.TopCenter)
        End If

    End Sub

    Private Sub btnExportarExcel_Click(sender As Object, e As EventArgs) Handles btnExportarExcel.Click
        If grDatos.RowCount > 0 Then
            _prCrearCarpetaReportes()
            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            If (P_ExportarExcel(RutaGlobal + "\Reporte\Reporte PreciosCosto")) Then
                L_fnBotonExportar(gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina, 0, "HISTORIAL COSTOS", "HISTORIAL COSTOS")
                ToastNotification.Show(Me, "EXPORTACIÓN DE HISTORIAL DE COSTOS EXITOSA..!!!",
                                           img, 2000,
                                           eToastGlowColor.Green,
                                           eToastPosition.TopCenter)
            Else
                ToastNotification.Show(Me, "FALLÓ LA EXPORTACIÓN DE HISTORIAL DE COSTOS..!!!",
                                           My.Resources.WARNING, 2000,
                                           eToastGlowColor.Red,
                                           eToastPosition.TopCenter)
            End If
        Else
            ToastNotification.Show(Me, "NO EXISTE DATOS PARA EXPORTAR",
                     My.Resources.WARNING, 2000,
                     eToastGlowColor.Red,
                     eToastPosition.TopCenter)
        End If
    End Sub
End Class