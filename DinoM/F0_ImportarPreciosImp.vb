
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


Public Class F0_ImportarPreciosImp
    Dim _Inter As Integer = 0

    Dim RutaGlobal As String = gs_CarpetaRaiz
#Region "Variables Globales"
    Dim precio As DataTable
    Public _nameButton As String
    Public _modulo As SideNavItem
    Public _tab As SuperTabItem
    Public ProductosImport As New DataTable

#End Region
#Region "MEtodos Privados"
    Private Sub _IniciarTodo()
        _prAsignarPermisos()
        Me.Text = "PRODUCTOS PARA IMPRIMIR PRECIOS"

        Dim blah As New Bitmap(New Bitmap(My.Resources.hojaruta), 20, 20)
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico
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


    Public Function P_ExportarExcel(_ruta As String, nombre As String) As Boolean
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
                Dim _archivo As String = _ubicacion & "\ListaConteo_" & nombre & "_" & Now.Date.Day &
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

        Dim rutaDestino As String = RutaGlobal + "\Reporte\Reporte Precios\"

        If System.IO.Directory.Exists(RutaGlobal + "\Reporte\Reporte Precios\") = False Then
            If System.IO.Directory.Exists(RutaGlobal + "\Reporte") = False Then
                System.IO.Directory.CreateDirectory(RutaGlobal + "\Reporte")
                If System.IO.Directory.Exists(RutaGlobal + "\Reporte\Reporte Precios") = False Then
                    System.IO.Directory.CreateDirectory(RutaGlobal + "\Reporte\Reporte Precios")
                End If
            Else
                If System.IO.Directory.Exists(RutaGlobal + "\Reporte\Reporte Precios") = False Then
                    System.IO.Directory.CreateDirectory(RutaGlobal + "\Reporte\Reporte Precios")

                End If
            End If
        End If
    End Sub

    Private Sub Buscador()
        Try
            Dim dt As DataTable
            dt = L_fnMostrarUsuariosConteo()

            Dim listEstCeldas As New List(Of Modelo.Celda)
            listEstCeldas.Add(New Modelo.Celda("ydnumi,", True, "COD USUARIO", 120))
            listEstCeldas.Add(New Modelo.Celda("yduser", True, "USUARIO", 250))
            listEstCeldas.Add(New Modelo.Celda("ydest", False, "ESTADO", 50))

            Dim ef = New Efecto
            ef.tipo = 3
            ef.dt = dt
            ef.SeleclCol = 1
            ef.listEstCeldas = listEstCeldas
            ef.alto = 150
            ef.ancho = 200
            ef.Context = "Seleccione Usuario".ToUpper
            ef.SeleclCol = 1
            ef.ShowDialog()
            Dim bandera As Boolean = False
            bandera = ef.band
            If (bandera = True) Then
                Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
                If (IsNothing(Row)) Then

                    Return
                End If

                '_prCargarTabla("", "")

            End If

        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
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

                MyDataAdapter.Fill(ProductosImport)
                con.Close()

                'Dim dt = ProductosImport.Copy
                'Dim fecha = dt.Rows(0).Item("Fecha1")

            End If

        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub

    Private Sub MP_PasarDatos()
        Try
            If ProductosImport.Rows.Count > 0 Then
                If ProductosImport.Columns.Count = 6 Then
                    _prCargarTablaImport(ProductosImport)

                Else
                    ProductosImport.Reset()
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

        With grDatos.RootTable.Columns("CODIGO DYNASYS")
            .Caption = "COD DYNASYS"
            .Width = 100
            .Visible = True
        End With
        With grDatos.RootTable.Columns("CODIGO CREX")
            .Caption = "COD DELTA"
            .Width = 100
            .Visible = True
        End With
        With grDatos.RootTable.Columns("DETALLE")
            .Caption = "DETALLE"
            .Width = 450
            .Visible = True
        End With
        With grDatos.RootTable.Columns("PRECIO A")
            .Caption = "PRECIO A"
            .Width = 100
            .Visible = True
            .TextAlignment = TextAlignment.Far
        End With
        With grDatos.RootTable.Columns("PRECIO B")
            .Caption = "PRECIO B"
            .Width = 100
            .Visible = True
            .TextAlignment = TextAlignment.Far
        End With
        With grDatos.RootTable.Columns("PRECIO C")
            .Caption = "PRECIO C"
            .Width = 100
            .Visible = True
            .TextAlignment = TextAlignment.Far
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
            .RecordNavigatorText = "Productos"
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


    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        If grDatos.RowCount > 0 Then
            For i = 0 To grDatos.RowCount - 1

            Next
        Else
            ToastNotification.Show(Me, "NO EXISTE DATOS PARA IMPRIMIR",
                       My.Resources.WARNING, 2000,
                       eToastGlowColor.Red,
                       eToastPosition.TopCenter)
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

    Private Sub btnSearch_Click(sender As Object, e As EventArgs)
        Buscador()
    End Sub

    Private Sub btnImportar_Click(sender As Object, e As EventArgs) Handles btnImportar.Click
        ProductosImport.Reset()
        ProductosImport.Clear()
        MP_ImportarExcel()
        MP_PasarDatos()
    End Sub

    Private Sub btnGrabarImp_Click(sender As Object, e As EventArgs)
        If ProductosImport.Rows.Count > 0 Then

            Dim respons As String = ProductosImport.Rows(0).Item("RESPONSABLE").ToString
            Dim fecha As Date = Now.Date.ToString("dd/MM/yyyy")
            Dim dtVerificar = L_fnVerificarGrabadoConteo(respons, fecha)
            If dtVerificar.Rows.Count > 0 Then
                ToastNotification.Show(Me, "YA EXISTE EL CONTEO DE HOY, NO PUEDE GRABAR 2 VECES!!!",
              My.Resources.WARNING, 4000,
              eToastGlowColor.Red,
              eToastPosition.BottomCenter)

                grDatos.ClearStructure()
                Exit Sub
            Else
                Dim importar As Boolean = L_fnImportarInventarioFisico(ProductosImport, gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina)
                If importar Then

                    ToastNotification.Show(Me, "IMPORTACIÓN DEL INVENTARIO FÍSICO EXITOSA!!! ",
                              My.Resources.OK, 5000,
                              eToastGlowColor.Green,
                              eToastPosition.BottomCenter)

                Else
                    ToastNotification.Show(Me, "FALLÓ LA IMPORTACIÓN DEL INVENTARIO FÍSICO!!!",
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

    Private Sub grDatos_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grDatos.EditingCell
        e.Cancel = True
    End Sub
End Class