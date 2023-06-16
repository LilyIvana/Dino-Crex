
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

Imports System.Reflection
Imports System.Runtime.InteropServices

Public Class F0_VerificarPrecioCompra
    Dim _Inter As Integer = 0

    Dim RutaGlobal As String = gs_CarpetaRaiz
#Region "Variables Globales"
    Dim precio As DataTable
    Public _nameButton As String
    Public _modulo As SideNavItem
    Public _tab As SuperTabItem
#End Region
#Region "MEtodos Privados"
    Private Sub _IniciarTodo()

        'Me.WindowState = FormWindowState.Maximized

        _prCargarComboLibreria(cbAlmacen)

        _prAsignarPermisos()
        Me.Text = "VERIFICACIÓN Y ACTUALIZACIÓN DE PRECIOS DE COSTO DE LAS COMPRAS"
        Dim blah As New Bitmap(New Bitmap(My.Resources.precio), 20, 20)
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

    Public Sub _prCargarTabla(bandera As Boolean, almacen As String, CodCompra As String) ''Bandera = true si es que haiq cargar denuevo la tabla de Precio Bandera =false si solo cargar datos al Janus con el precio antepuesto
        If (cbAlmacen.SelectedIndex >= 0) Then

            Dim productos As DataTable = L_fnMostrarDetalleCompraSinActualizarPrecios(almacen, CodCompra)


            grprecio.BoundMode = Janus.Data.BoundMode.Bound
            grprecio.DataSource = productos
            grprecio.RetrieveStructure()


            With grprecio.RootTable.Columns("cbnumi")
                .Width = 100
                .Visible = False
            End With
            With grprecio.RootTable.Columns("cbtv1numi")
                .Width = 100
                .Visible = False
            End With
            With grprecio.RootTable.Columns("cbty5prod")
                .Caption = "COD DYNASYS"
                .Width = 100
                .Visible = True
            End With
            With grprecio.RootTable.Columns("yfcprod")
                .Caption = "COD DELTA"
                .Width = 100
                .Visible = True
            End With
            With grprecio.RootTable.Columns("yfcbarra")
                .Caption = "COD. BARRAS"
                .Width = 120
                .Visible = True
            End With
            With grprecio.RootTable.Columns("producto")
                .Caption = "PRODUCTO"
                .Width = 450
                .Visible = True
            End With
            With grprecio.RootTable.Columns("cbumin")
                .Width = 100
                .Visible = False
            End With
            With grprecio.RootTable.Columns("unidad")
                .Width = 100
                .Visible = False
            End With
            With grprecio.RootTable.Columns("PrecioAntiguo")
                .Caption = "PRECIO ANTIGUO"
                .Width = 150
                .FormatString = "0.00"
                .Visible = True
            End With
            With grprecio.RootTable.Columns("PrecioNuevo")
                .Caption = "PRECIO NUEVO"
                .Width = 150
                .FormatString = "0.00"
                .Visible = True
            End With
            With grprecio.RootTable.Columns("cbaest")
                .Caption = "ACTUALIZA PRECIO COSTO?"
                .Width = 180
                .Visible = True
            End With

            'Habilitar Filtradores
            With grprecio
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
                .RecordNavigatorText = "Productos-Precios Costo"
            End With
        End If
    End Sub
    Private Sub _prCargarComboLibreria(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnGeneralSucursales()
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("aanumi").Width = 70
            .DropDownList.Columns("aanumi").Caption = "COD"
            .DropDownList.Columns.Add("aabdes").Width = 200
            .DropDownList.Columns("aabdes").Caption = "DESCRIPCION"
            .ValueMember = "aanumi"
            .DisplayMember = "aabdes"
            .DataSource = dt
            .Refresh()
        End With
        If (CType(cbAlmacen.DataSource, DataTable).Rows.Count > 0) Then
            cbAlmacen.SelectedIndex = 0
        End If
    End Sub
    Private Sub _prInhabiliitar()

        btnModificar.Enabled = True
        btnGrabar.Enabled = False
        tbCodCompra.Clear()
        tbProveedor.Clear()
        _prCargarTabla(False, cbAlmacen.Value, -1)


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

#End Region


#Region "MEtodoso Formulario"
    Private Sub F0_Precios_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _IniciarTodo()
        _prInhabiliitar()
    End Sub
    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        _prhabilitar()
        btnModificar.Enabled = False
        tbCodCompra.Focus()

    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        If (_fnAccesible()) Then
            _prInhabiliitar()
        Else
            _modulo.Select()
            Me.Close()
        End If
    End Sub


    Private Sub grprecio_CellEdited(sender As Object, e As ColumnActionEventArgs) Handles grprecio.CellEdited
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

    Private Sub grprecio_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grprecio.EditingCell

        If btnGrabar.Enabled = False Then
            e.Cancel = True
            Return
        End If
        If (_fnAccesible() And IsNothing(grprecio.DataSource) = False) Then
            'Deshabilitar la columna de Productos y solo habilitar la de los precios
            If (e.Column.Index = grprecio.RootTable.Columns("yfcdprod1").Index Or
               e.Column.Index = grprecio.RootTable.Columns("yfcprod").Index Or
                e.Column.Index = grprecio.RootTable.Columns("yfnumi").Index Or
                e.Column.Index = grprecio.RootTable.Columns("yfcbarra").Index) Then
                e.Cancel = True
            Else
                e.Cancel = False
            End If
        Else
            e.Cancel = True
        End If
    End Sub
    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click

        Dim grabar As Boolean = L_fnActualizarTC0011a(tbCodCompra.Text, CType(grprecio.DataSource, DataTable))
        If (grabar) Then
            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "Precios de Costo actualizados con éxito".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter
                                      )

            _prCargarTabla(True, cbAlmacen.Value, tbCodCompra.Text)
            _prInhabiliitar()

        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "Precios de Costo no pudieron ser actualizados".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        End If

    End Sub


#End Region

    Public Function P_ExportarExcel(_ruta As String) As Boolean
        Dim _ubicacion As String
        'Dim _directorio As New FolderBrowserDialog

        If (1 = 1) Then 'If(_directorio.ShowDialog = Windows.Forms.DialogResult.OK) Then
            '_ubicacion = _directorio.SelectedPath
            _ubicacion = _ruta
            Try
                Dim _stream As Stream
                Dim _escritor As StreamWriter
                Dim _fila As Integer = grprecio.GetRows.Length
                Dim _columna As Integer = grprecio.RootTable.Columns.Count
                Dim _archivo As String = _ubicacion & "\ListaDeProd_" & Now.Date.Day &
                    "." & Now.Date.Month & "." & Now.Date.Year & "_" & Now.Hour & "." & Now.Minute & "." & Now.Second & ".csv"
                Dim _linea As String = ""
                Dim _filadata = 0, columndata As Int32 = 0
                File.Delete(_archivo)
                _stream = File.OpenWrite(_archivo)
                _escritor = New StreamWriter(_stream, System.Text.Encoding.UTF8)

                For Each _col As GridEXColumn In grprecio.RootTable.Columns
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

                For Each _fil As GridEXRow In grprecio.GetRows
                    For Each _col As GridEXColumn In grprecio.RootTable.Columns
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

    Private Sub grprecio_CellValueChanged(sender As Object, e As ColumnActionEventArgs) Handles grprecio.CellValueChanged
        Dim lin As Integer = grprecio.GetValue("yfnumi")
        Dim pos As Integer = -1
        _fnObtenerFilaDetalle(pos, lin)

        Dim estado As Integer = CType(grprecio.DataSource, DataTable).Rows(pos).Item("estado")
        If (estado = 1) Then
            CType(grprecio.DataSource, DataTable).Rows(pos).Item("estado") = 2
        End If
    End Sub
    Public Sub _fnObtenerFilaDetalle(ByRef pos As Integer, numi As Integer)
        For i As Integer = 0 To CType(grprecio.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _numi As Integer = CType(grprecio.DataSource, DataTable).Rows(i).Item("yfnumi")
            If (_numi = numi) Then
                pos = i
                Return
            End If
        Next

    End Sub


    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Buscador()
    End Sub

    Private Sub Buscador()
        Try
            If (_fnAccesible()) Then

                Dim dt As DataTable

                dt = L_fnMostrarComprasSinActualizarPrecios()

                Dim listEstCeldas As New List(Of Modelo.Celda)
                listEstCeldas.Add(New Modelo.Celda("canumi,", True, "Cód. Compra", 120))
                listEstCeldas.Add(New Modelo.Celda("caalm", False, "", 50))
                listEstCeldas.Add(New Modelo.Celda("cafdoc", True, "Fecha", 100))
                listEstCeldas.Add(New Modelo.Celda("caty4prov", False, "", 50))
                listEstCeldas.Add(New Modelo.Celda("proveedor", True, "Proveedor", 250))

                Dim ef = New Efecto
                ef.tipo = 3
                ef.dt = dt
                ef.SeleclCol = 1
                ef.listEstCeldas = listEstCeldas
                ef.alto = 50
                ef.ancho = 200
                ef.Context = "Seleccione Compra".ToUpper
                ef.SeleclCol = 1
                ef.ShowDialog()
                Dim bandera As Boolean = False
                bandera = ef.band
                If (bandera = True) Then
                    Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
                    If (IsNothing(Row)) Then
                        tbCodCompra.Focus()
                        Return
                    End If

                    tbCodCompra.Text = Row.Cells("canumi").Value
                    tbProveedor.Text = Row.Cells("proveedor").Value

                    _prCargarTabla(True, cbAlmacen.Value, tbCodCompra.Text)

                End If

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
    Private Sub tbCodCompra_KeyDown(sender As Object, e As KeyEventArgs) Handles tbCodCompra.KeyDown
        Try
            If (_fnAccesible()) Then
                If e.KeyData = Keys.Control + Keys.Enter Then
                    Buscador()
                End If
            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub


End Class