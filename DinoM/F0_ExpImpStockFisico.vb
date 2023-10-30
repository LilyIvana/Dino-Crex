
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

Public Class F0_ExpImpStockFisico
    Dim _Inter As Integer = 0

    Dim RutaGlobal As String = gs_CarpetaRaiz
#Region "Variables Globales"
    Dim precio As DataTable
    Public _nameButton As String
    Public _modulo As SideNavItem
    Public _tab As SuperTabItem
    Public InventarioImport As New DataTable

#End Region
#Region "MEtodos Privados"
    Private Sub _IniciarTodo()

        'Me.WindowState = FormWindowState.Maximized

        _prAsignarPermisos()
        Me.Text = "CONTEO  FÍSICO  DE  PRODUCTOS"
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

    Public Sub _prCargarTabla(Usuario As String, fecha As String) ''Bandera = true si es que haiq cargar denuevo la tabla de Precio Bandera =false si solo cargar datos al Janus con el precio antepuesto

        Dim productos As DataTable = L_fnListarConteoUsuario(Usuario, fecha)


        grDatos.BoundMode = Janus.Data.BoundMode.Bound
        grDatos.DataSource = productos
        grDatos.RetrieveStructure()

        With grDatos.RootTable.Columns("yfnumi")
            .Caption = "COD DYN"
            .Width = 100
            .Visible = True
        End With
        With grDatos.RootTable.Columns("yfcprod")
            .Caption = "COD DELTA"
            .Width = 100
            .Visible = True
        End With
        With grDatos.RootTable.Columns("yfcbarra")
            .Caption = "COD. BARRAS"
            .Width = 150
            .Visible = True
        End With
        With grDatos.RootTable.Columns("yfcdprod1")
            .Caption = "PRODUCTO"
            .Width = 450
            .Visible = True
        End With

        With grDatos.RootTable.Columns("yfbactPrecio")
            .Caption = "ACTUALIZA PRECIO PDV?"
            .Width = 150
            .Visible = True
        End With
        With grDatos.RootTable.Columns("estado")
            .Caption = "ESTADO"
            .Width = 120
            .Visible = False
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
                Dim _stream As Stream
                Dim _escritor As StreamWriter
                Dim _fila As Integer = grDatos.GetRows.Length
                Dim _columna As Integer = grDatos.RootTable.Columns.Count
                Dim _archivo As String = _ubicacion & "\ListaDeProd_" & Now.Date.Day &
                    "." & Now.Date.Month & "." & Now.Date.Year & "_" & Now.Hour & "." & Now.Minute & "." & Now.Second & ".csv"
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

                'Pbx_Precios.Visible = True
                'Pbx_Precios.Minimum = 1
                'Pbx_Precios.Maximum = Dgv_Precios.RowCount
                'Pbx_Precios.Value = 1

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
            listEstCeldas.Add(New Modelo.Celda("ydnumi,", True, "COD USUARIO", 90))
            listEstCeldas.Add(New Modelo.Celda("yduser", True, "USUARIO", 120))
            listEstCeldas.Add(New Modelo.Celda("ydest", False, "ESTADO", 50))

            Dim ef = New Efecto
            ef.tipo = 3
            ef.dt = dt
            ef.SeleclCol = 1
            ef.listEstCeldas = listEstCeldas
            ef.alto = 50
            ef.ancho = 200
            ef.Context = "Seleccione Usuario".ToUpper
            ef.SeleclCol = 1
            ef.ShowDialog()
            Dim bandera As Boolean = False
            bandera = ef.band
            If (bandera = True) Then
                Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
                If (IsNothing(Row)) Then
                    tbUsuario.Focus()
                    Return
                End If

                tbUsuario.Text = Row.Cells("yduser").Value

                _prCargarTabla(tbUsuario.Text, tbFechaInv.Value.ToString("dd/MM/yyyy"))

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

            If openfile1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                folder = openfile1.FileName
            End If

            If True Then
                Dim pathconn As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & folder & ";Extended Properties='Excel 12.0 Xml;HDR=Yes'"
                'Dim pathconn As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & folder & ";"
                'Dim pathconn As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & folder & ";Extended Properties=""Excel 8.0;HDR=YES;"""

                Dim con As OleDbConnection = New OleDbConnection(pathconn)
                Dim MyDataAdapter As OleDbDataAdapter = New OleDbDataAdapter("Select * from [" & doc & "$]", con)
                con.Open()

                MyDataAdapter.Fill(InventarioImport)
                con.Close()

                'Dim dt = InventarioImport.Copy

                'Dim fecha = dt.Rows(0).Item("Fecha1")

            End If

        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub

    Private Sub MP_PasarDatos()
        Try
            Dim resp = InventarioImport.Rows(0).Item("RESPONSABLE")
            Dim TablaProductos As DataTable = L_fnMostrarProductosXresponsable(resp)
            Dim ProdFiltrado As DataTable
            Dim Numi As String
            Dim Tablaaux As DataTable = InventarioImport.Copy

            '''Validación para comprobar que no existan dos o mas filas con el mismo codigo
            'For k = 0 To InventarioImport.Rows.Count - 1
            '    Dim aux = Tablaaux.Select("Codigo=" + InventarioImport.Rows(k).Item("Codigo").ToString)
            '    If aux.Length > 1 Then
            '        ToastNotification.Show(Me, "No se puede realizar la importación porque el codigo:  ".ToUpper & InventarioImport.Rows(k).Item("Codigo").ToString & " existe  ".ToUpper & aux.Length.ToString & " veces en la lista".ToUpper,
            '                               My.Resources.WARNING, 5000, eToastGlowColor.Green, eToastPosition.BottomCenter)
            '        Exit Sub
            '    End If
            'Next


            If InventarioImport.Rows.Count = TablaProductos.Rows.Count Then
                '''Hago una copia para ir cambiando el codigo flex por el codigo del sistema
                'Dim InvProductos As DataTable = InventarioImport.Copy
                'For i = 0 To InventarioImport.Rows.Count - 1
                '    ProdFiltrado = L_fnProductoPorCacod(InventarioImport.Rows(i).Item("Codigo").ToString)
                '    If ProdFiltrado.Rows.Count > 0 Then
                '        Numi = ProdFiltrado.Rows(0).Item("canumi").ToString
                '        InvProductos.Rows(i).Item("Codigo") = Numi
                '    Else
                '        ToastNotification.Show(Me, "No se puede realizar la importación porque el código de producto: ".ToUpper & InventarioImport.Rows(i).Item("Codigo").ToString & " no pertenece a la lista de Productos de la base de datos".ToUpper,
                '                               My.Resources.WARNING, 5000, eToastGlowColor.Green, eToastPosition.BottomCenter)
                '        Exit Sub
                '    End If

                'Next
                'Dim importar As Boolean = L_fnImportarInventario(InvProductos)
                'If importar Then
                '    ToastNotification.Show(Me, "IMPORTACIÓN DEL INVENTARIO EXITOSA!!! ",
                '                  My.Resources.OK, 5000,
                '                  eToastGlowColor.Green,
                '                  eToastPosition.BottomCenter)
                'Else
                '    ToastNotification.Show(Me, "FALLÓ LA IMPORTACIÓN DEL INVENTARIO!!!",
                '                  My.Resources.WARNING, 4000,
                '                  eToastGlowColor.Red,
                '                  eToastPosition.BottomCenter)
                'End If
            Else
                ToastNotification.Show(Me, "No se puede realizar la importación porque la Lista del Inventario tiene que tener ".ToUpper & TablaProductos.Rows.Count & " registros".ToUpper,
                                       My.Resources.WARNING, 5000, eToastGlowColor.Green, eToastPosition.BottomCenter)
                Exit Sub
            End If


        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub
    Private Sub MostrarMensajeError(mensaje As String)
        ToastNotification.Show(Me,
                               mensaje.ToUpper,
                               My.Resources.WARNING,
                               5000,
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


    Private Sub grprecio_CellEdited(sender As Object, e As ColumnActionEventArgs) Handles grDatos.CellEdited
        If (_fnAccesible()) Then

            ''Habilitar solo las columnas de Precio, %, Monto y Observación
            'If (e.Column.Index > 1) Then
            '    Dim data As String = grprecio.GetValue(e.Column.Index - 1).ToString.Trim 'En esta columna obtengo un protocolo que me indica el estado del precio 0= no insertado 1= ya insertado , a la ves con un '-' me indica la posicion de ese dato en el Datatable que envio para grabarlo que esta en 'precio' Ejemplo:1-15 -> estado=1 posicion=15
            '    Dim estado As String = data.Substring(0, 1).Trim
            '    Dim pos As String = data.Substring(2, data.Length - 2)
            '    If (estado = 1 Or estado = 2) Then
            '        precio.Rows(pos).Item("estado") = 2
            '        precio.Rows(pos).Item("yhprecio") = grprecio.GetValue(e.Column.Index)
            '    Else
            '        If (estado = 0 Or estado = 3) Then
            '            precio.Rows(pos).Item("estado") = 3
            '            precio.Rows(pos).Item("yhprecio") = grprecio.GetValue(e.Column.Index)
            '        End If
            '    End If
            'End If


        End If
    End Sub

    Private Sub grprecio_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grDatos.EditingCell

        If btnGrabar.Enabled = False Then
            e.Cancel = True
            Return
        End If
        If (_fnAccesible() And IsNothing(grDatos.DataSource) = False) Then
            'Deshabilitar la columna de Productos y solo habilitar la de los precios
            If (e.Column.Index = grDatos.RootTable.Columns("yfcdprod1").Index Or
               e.Column.Index = grDatos.RootTable.Columns("yfcprod").Index Or
                e.Column.Index = grDatos.RootTable.Columns("yfnumi").Index Or
                e.Column.Index = grDatos.RootTable.Columns("yfcbarra").Index) Then
                e.Cancel = True
            Else
                e.Cancel = False
            End If
        Else
            e.Cancel = True
        End If
    End Sub
    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click

        Dim grabar As Boolean = L_fnActualizarProductoTY0052("", CType(grDatos.DataSource, DataTable))
        If (grabar) Then
            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "Condición de Productos Actualizados con éxito".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter
                                      )

            '_prCargarTabla(True)
            _prInhabiliitar()

        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "La Condición de Productos no pudo ser insertado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        End If

    End Sub


#End Region


    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        _prCrearCarpetaReportes()
        Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
        If (P_ExportarExcel(RutaGlobal + "\Reporte\Reporte Productos")) Then
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

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        _Inter = _Inter + 1
        If _Inter = 1 Then
            Me.WindowState = FormWindowState.Normal

        Else
            Me.Opacity = 100
            Timer1.Enabled = False
        End If
    End Sub

    Private Sub grprecio_CellValueChanged(sender As Object, e As ColumnActionEventArgs) Handles grDatos.CellValueChanged
        Dim lin As Integer = grDatos.GetValue("yfnumi")
        Dim pos As Integer = -1
        _fnObtenerFilaDetalle(pos, lin)

        Dim estado As Integer = CType(grDatos.DataSource, DataTable).Rows(pos).Item("estado")
        If (estado = 1) Then
            CType(grDatos.DataSource, DataTable).Rows(pos).Item("estado") = 2
        End If
    End Sub
    Public Sub _fnObtenerFilaDetalle(ByRef pos As Integer, numi As Integer)
        For i As Integer = 0 To CType(grDatos.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _numi As Integer = CType(grDatos.DataSource, DataTable).Rows(i).Item("yfnumi")
            If (_numi = numi) Then
                pos = i
                Return
            End If
        Next

    End Sub



    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Buscador()
    End Sub

    Private Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click

    End Sub

    Private Sub btnImportar_Click(sender As Object, e As EventArgs) Handles btnImportar.Click
        InventarioImport.Clear()
        MP_ImportarExcel()
        MP_PasarDatos()
    End Sub
End Class