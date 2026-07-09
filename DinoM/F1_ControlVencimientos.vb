
Imports System.IO
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Imports Janus.Windows.GridEX
Imports Logica.AccesoLogica
Imports Microsoft.Office.Interop
Imports System.Data.OleDb

Public Class F1_ControlVencimientos
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
    Public VencimientosImport As New DataTable
#End Region
#Region "Metodos Privados"
    Private Sub _prIniciarTodo()
        Me.Text = "CONTROL DE VENCIMIENTOS"
        tbFechaI.Value = Now.Date

        Dim blah As New Bitmap(New Bitmap(My.Resources.producto), 20, 20)
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico
        Dim dt As New DataTable
        dt = L_RepCodVenc(2) ''
        g_prArmarCombo(cbCodVenc, dt, 60, 120)
        btnImprimir.Visible = False
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
    Private Sub _prCargarDatos()
        Dim fecha As DateTime = tbFechaI.Value.ToString("yyyy/MM/dd")
        If cbCodVenc.Text = String.Empty Then
            ToastNotification.Show(Me, "DEBE SELECCIONAR UN CÓDIGO DE VENCIMIENTO!!!", My.Resources.WARNING, 2000, eToastGlowColor.Red, eToastPosition.TopCenter)
        Else
            Dim dt As DataTable = L_fnStockSistvsFisicoVenc(fecha)

            If dt.Rows.Count > 0 Then
                Dim table As DataTable

                table = dt.Clone
                Dim row As DataRow() = dt.Select("PrefVenc=" + "'" + cbCodVenc.Text + "'")

                For Each ldrRow As DataRow In row
                    table.ImportRow(ldrRow)
                Next

                L_fnBotonGenerar(gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina, 0, "CONTROL DE VENCIMIENTOS", "CONTROL DE VENCIMIENTOS")

                JGrM_Buscador.DataSource = table
                JGrM_Buscador.RetrieveStructure()
                JGrM_Buscador.AlternatingColors = True

                With JGrM_Buscador.RootTable.Columns("fecha")
                    .Width = 90
                    .Visible = True
                    .Caption = "FECHA"
                End With
                With JGrM_Buscador.RootTable.Columns("yfordenacion")
                    .Width = 90
                    .Visible = True
                    .Caption = "ORDEN"
                End With
                With JGrM_Buscador.RootTable.Columns("codDynasys")
                    .Width = 100
                    .Caption = "CODDYNASYS"
                    .Visible = True
                End With
                With JGrM_Buscador.RootTable.Columns("codDelta")
                    .Width = 100
                    .Caption = "COD DELTA"
                    .Visible = True
                End With
                With JGrM_Buscador.RootTable.Columns("codBarra")
                    .Width = 120
                    .Caption = "COD BARRAS"
                    .Visible = True
                End With
                With JGrM_Buscador.RootTable.Columns("codProv")
                    .Width = 100
                    .Caption = "COD PROVEEDOR"
                    .Visible = False
                End With
                With JGrM_Buscador.RootTable.Columns("pref")
                    .Width = 90
                    .Caption = "SIGLA DEMANDA"
                    .Visible = False
                End With
                With JGrM_Buscador.RootTable.Columns("PrefVenc")
                    .Width = 90
                    .Caption = "COD VENC"
                    .Visible = True
                End With
                With JGrM_Buscador.RootTable.Columns("dato1")
                    .Width = 90
                    .Caption = "COLOR"
                    .Visible = True
                End With
                With JGrM_Buscador.RootTable.Columns("prod")
                    .Width = 380
                    .Caption = "PRODUCTO"
                    .Visible = True
                    .WordWrap = True
                    .MaxLines = 2
                End With
                With JGrM_Buscador.RootTable.Columns("prov")
                    .Width = 130
                    .Caption = "PROVEEDOR"
                    .Visible = True
                End With
                With JGrM_Buscador.RootTable.Columns("StockFis")
                    .Width = 100
                    .Caption = "STOCK FISICO"
                    .Visible = True
                    .FormatString = "0.00"
                    .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                End With
                With JGrM_Buscador.RootTable.Columns("StockActual")
                    .Width = 100
                    .Caption = "STOCK ACTUAL"
                    .Visible = True
                    .FormatString = "0.00"
                    .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                End With
                With JGrM_Buscador.RootTable.Columns("fecha1")
                    .Width = 100
                    .Visible = True
                    .Caption = "F VENC 1"
                End With
                With JGrM_Buscador.RootTable.Columns("fecha2")
                    .Width = 100
                    .Visible = True
                    .Caption = "F VENC 2"
                End With
                With JGrM_Buscador.RootTable.Columns("fecha3")
                    .Width = 100
                    .Visible = True
                    .Caption = "F VENC 3"
                End With
                With JGrM_Buscador.RootTable.Columns("yfresponsable")
                    .Width = 120
                    .Visible = True
                    .Caption = "RESPONSABLE"
                End With
                With JGrM_Buscador.RootTable.Columns("yflado")
                    .Width = 100
                    .Visible = False
                    .Caption = "LADO"
                End With
                With JGrM_Buscador.RootTable.Columns("estado")
                    .Width = 100
                    .Visible = False
                    .Caption = "ESTADO"
                End With
                With JGrM_Buscador.RootTable.Columns("cant1")
                    .Width = 100
                    .Caption = "CANTIDAD1"
                    .Visible = True
                    .FormatString = "0.00"
                    .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                End With
                With JGrM_Buscador.RootTable.Columns("fecha1n")
                    .Width = 100
                    .Visible = True
                    .Caption = "F VENC NUEVO 1"
                End With
                With JGrM_Buscador.RootTable.Columns("fecha2n")
                    .Width = 100
                    .Visible = True
                    .Caption = "F VENC NUEVO 2"
                End With
                With JGrM_Buscador.RootTable.Columns("fecha3n")
                    .Width = 100
                    .Visible = True
                    .Caption = "F VENC NUEVO 3"
                End With
                With JGrM_Buscador.RootTable.Columns("Obs")
                    .Width = 150
                    .Visible = True
                    .Caption = "OBSERVACION"
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

            Else
                JGrM_Buscador.ClearStructure()
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "No existe datos para mostrar".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.TopCenter)
            End If
        End If
    End Sub
#End Region

    Private Sub F1_Productos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()
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
                Dim _archivo As String = _ubicacion & "\ControlVencimientos" & ".csv"
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
        If JGrM_Buscador.RowCount > 0 Then
            _prCrearCarpetaReportes()
            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            If (P_ExportarExcel(RutaGlobal + "\Reporte\Reporte Productos")) Then
                L_fnBotonExportar(gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina, 0, "CONTROL DE VENCIMIENTOS", "CONTROL DE VENCIMIENTOS")

                ToastNotification.Show(Me, "EXPORTACIÓN DE CONTROL DE VENCIMIENTOS EXITOSA..!!!",
                                           img, 2000,
                                           eToastGlowColor.Green,
                                           eToastPosition.BottomCenter)
            Else
                ToastNotification.Show(Me, "FALLÓ LA EXPORTACIÓN DE CONTROL DE VENCIMIENTOS..!!!",
                                           My.Resources.WARNING, 2000,
                                           eToastGlowColor.Red,
                                           eToastPosition.BottomLeft)
            End If

        Else
            ToastNotification.Show(Me, "PRIMERO DEBE GENERAR EL REPORTE Y OBTENER DATOS ANTES DE EXPORTAR..!!!",
                                           My.Resources.WARNING, 2000,
                                           eToastGlowColor.Red,
                                           eToastPosition.BottomLeft)
        End If

    End Sub

    Private Sub btnImportar_Click(sender As Object, e As EventArgs) Handles btnImportar.Click
        ''VencimientosImport.Reset()
        VencimientosImport.Clear()
        MP_ImportarExcel()
        MP_PasarDatos()
    End Sub
    Private Sub MP_ImportarExcel()
        Try
            Dim folder As String = ""
            Dim doc As String = "ControlVencimientos"
            Dim openfile1 As OpenFileDialog = New OpenFileDialog()

            If openfile1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                folder = openfile1.FileName
            End If

            If True Then
                Dim pathconn As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & folder & ";Extended Properties='Excel 12.0 Xml;HDR=Yes'"

                Dim con As OleDbConnection = New OleDbConnection(pathconn)
                Dim MyDataAdapter As OleDbDataAdapter = New OleDbDataAdapter("Select * from [" & doc & "$]", con)
                con.Open()

                MyDataAdapter.Fill(VencimientosImport)
                con.Close()
            End If

        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub
    Private Sub MostrarMensajeError(mensaje As String)
        ToastNotification.Show(Me,
                               mensaje.ToUpper,
                               My.Resources.WARNING,
                               5500,
                               eToastGlowColor.Red,
                               eToastPosition.TopCenter)
    End Sub
    Private Sub MP_PasarDatos()
        Try
            If VencimientosImport.Rows.Count > 0 Then

                If VencimientosImport.Columns.Count = 20 Then

                    'Dim resp = VencimientosImport.Rows(0).Item("RESPONSABLE")
                    'Dim TablaProductos As DataTable = L_fnMostrarProductosXresponsable(resp)
                    Dim Tablaaux As DataTable = VencimientosImport.Copy

                    ''Validación para comprobar que no existan dos o mas filas con el mismo codigo
                    For k = 0 To VencimientosImport.Rows.Count - 1
                        Dim aux = Tablaaux.Select("CODDYNASYS=" + VencimientosImport.Rows(k).Item("CODDYNASYS").ToString)
                        If aux.Length > 1 Then
                            ToastNotification.Show(Me, "No se puede realizar la importación porque el codigo dynasys:  ".ToUpper & VencimientosImport.Rows(k).Item("CODDYNASYS").ToString & " existe  ".ToUpper & aux.Length.ToString & " veces en la lista".ToUpper,
                                                   My.Resources.WARNING, 5000, eToastGlowColor.Green, eToastPosition.BottomCenter)
                            Exit Sub
                        End If
                    Next

                    ''Validación para comprobar todos los campos de cantidades menos  el campo Total Cantidad
                    For i = 0 To VencimientosImport.Rows.Count - 1
                        If IsDBNull(VencimientosImport.Rows(i).Item("CANTIDAD1")) Or (IIf(VencimientosImport.Rows(i).Item("CANTIDAD1").ToString = String.Empty, 0, VencimientosImport.Rows(i).Item("CANTIDAD1")) < 0) Then
                            ToastNotification.Show(Me, "No se puede realizar la importación porque el codigo Dynasys: ".ToUpper & VencimientosImport.Rows(i).Item("CODDYNASYS") & " tiene una de las cantidades con valor negativo, vacío o con letra, corrija por favor".ToUpper,
                                                       My.Resources.WARNING, 7500, eToastGlowColor.Green, eToastPosition.BottomCenter)
                            Exit Sub
                        End If
                    Next

                    ''Validación para comprobar fechas de vencimiento
                    For k = 0 To VencimientosImport.Rows.Count - 1

                        If IsDBNull(VencimientosImport.Rows(k).Item("FECHA")) Or IsDBNull(VencimientosImport.Rows(k).Item("F VENC 1")) Or IsDBNull(VencimientosImport.Rows(k).Item("F VENC 2")) Or
                                IsDBNull(VencimientosImport.Rows(k).Item("F VENC 3")) Or IsDBNull(VencimientosImport.Rows(k).Item("F VENC NUEVO 1")) Or
                                IsDBNull(VencimientosImport.Rows(k).Item("F VENC NUEVO 2")) Or IsDBNull(VencimientosImport.Rows(k).Item("F VENC NUEVO 3")) Then
                            ToastNotification.Show(Me, "No se puede realizar la importación porque el codigo Dynasys: ".ToUpper & VencimientosImport.Rows(k).Item("CODDYNASYS") &
                                " tiene una de las fechas de vencimimiento vacío o con un formato incorrecto, el formato es (dd/MM/aaaa) , corrija por favor".ToUpper,
                                                       My.Resources.WARNING, 8500, eToastGlowColor.Green, eToastPosition.BottomCenter)
                            Exit Sub
                        End If
                    Next

                    _prCargarTablaImport(VencimientosImport)
                    btnGuardar.Visible = True

                Else
                    VencimientosImport.Reset()
                    ToastNotification.Show(Me, "No puede importar porque el excel tiene que tener 20 columnas, usted modificó el excel, corrija por favor".ToUpper,
                                         My.Resources.WARNING, 6000, eToastGlowColor.Green, eToastPosition.TopCenter)
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

        JGrM_Buscador.BoundMode = Janus.Data.BoundMode.Bound
        JGrM_Buscador.DataSource = dtImport
        JGrM_Buscador.RetrieveStructure()

        With JGrM_Buscador.RootTable.Columns("FECHA")
            .Caption = "FECHA"
            .Width = 90
            .Visible = True
        End With
        With JGrM_Buscador.RootTable.Columns("ORDEN")
            .Caption = "ORDEN"
            .Width = 90
            .Visible = True
        End With
        With JGrM_Buscador.RootTable.Columns("CODDYNASYS")
            .Caption = "COD DYNASYS"
            .Width = 100
            .Visible = True
        End With
        With JGrM_Buscador.RootTable.Columns("COD DELTA")
            .Caption = "COD DELTA"
            .Width = 100
            .Visible = True
        End With
        With JGrM_Buscador.RootTable.Columns("COD BARRAS")
            .Caption = "COD BARRAS"
            .Width = 150
            .Visible = True
        End With
        With JGrM_Buscador.RootTable.Columns("PRODUCTO")
            .Caption = "PRODUCTO"
            .Width = 350
            .Visible = True
        End With

        'Habilitar Filtradores
        With JGrM_Buscador
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
            .RecordNavigatorText = "Datos"
        End With

    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If VencimientosImport.Rows.Count > 0 Then

            Dim respons As String = L_Usuario
            'Dim fecha As Date = Now.Date.ToString("dd/MM/yyyy")
            Dim fecha As String = VencimientosImport.Rows(0).Item("FECHA")
            'Dim CodVenc As String = VencimientosImport.Rows(0).Item("COD VENC")
            Dim dtVerificar = L_fnVerificarGrabadoControlVenc(respons, fecha)

            If dtVerificar.Rows.Count > 0 Then
                ToastNotification.Show(Me, "YA EXISTE EL CONTROL DE VENCIMIENTOS PARA HOY, NO PUEDE GRABAR 2 VECES!!!",
                My.Resources.WARNING, 4000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                btnGuardar.Visible = False
                JGrM_Buscador.ClearStructure()
                Exit Sub
            Else
                Dim importar As Boolean = L_fnImportarControlVenc(VencimientosImport, gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina)
                If importar Then
                    ToastNotification.Show(Me, "IMPORTACIÓN DE CONTROL DE VENCIMIENTOS EXITOSA!!! ",
                              My.Resources.OK, 5000,
                              eToastGlowColor.Green,
                              eToastPosition.BottomCenter)
                    btnGuardar.Visible = False
                    JGrM_Buscador.ClearStructure()
                Else
                    ToastNotification.Show(Me, "FALLÓ LA IMPORTACIÓN DE CONTROL DE VENCIMIENTOS!!!",
                              My.Resources.WARNING, 4000,
                              eToastGlowColor.Red,
                              eToastPosition.BottomCenter)
                End If
            End If

        Else
            ToastNotification.Show(Me, "NO PUEDE GRABAR SI NO HAY DATOS EN LA GRILLA!!!",
                             My.Resources.WARNING, 4000,
                             eToastGlowColor.Red,
                             eToastPosition.TopCenter)
        End If
    End Sub
End Class