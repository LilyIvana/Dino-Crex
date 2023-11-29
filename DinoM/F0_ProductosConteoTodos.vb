
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

Public Class F0_ProductosConteoTodos
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
        _prCargarComboLibreriaResp(cbResp)
        _prCargarComboLado(cbLado)

        _prAsignarPermisos()
        Me.Text = "MODIFICAR PRODUCTOS CONTEO"
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

    Public Sub _prCargarTabla(bandera As Boolean) ''Bandera = true si es que haiq cargar denuevo la tabla de Precio Bandera =false si solo cargar datos al Janus con el precio antepuesto
        If (cbAlmacen.SelectedIndex >= 0) Then
            'Dim productos As DataTable = L_fnListarProductos()
            Dim productos As DataTable = L_fnGeneralProductosConteoTodos()


            grproductos.BoundMode = Janus.Data.BoundMode.Bound
            grproductos.DataSource = productos
            grproductos.RetrieveStructure()

            With grproductos.RootTable.Columns("yfnumi")
                .Caption = "COD DYNASYS"
                .Width = 90
                .Visible = True
            End With
            With grproductos.RootTable.Columns("yfcprod")
                .Caption = "COD DELTA"
                .Width = 90
                .Visible = True
            End With
            With grproductos.RootTable.Columns("yfcdprod2")
                .Caption = "COD PROVEEDOR"
                .Width = 90
                .Visible = True
            End With
            With grproductos.RootTable.Columns("yfcbarra")
                .Caption = "COD. BARRAS"
                .Width = 120
                .Visible = True
            End With
            With grproductos.RootTable.Columns("yfcdprod1")
                .Caption = "PRODUCTO"
                .Width = 450
                .Visible = True
            End With
            With grproductos.RootTable.Columns("yfgr1")
                .Caption = "COD. PROV"
                .Width = 90
                .Visible = False
            End With
            With grproductos.RootTable.Columns("grupo1")
                .Caption = "PROVEEDOR"
                .Width = 120
                .Visible = True
            End With
            With grproductos.RootTable.Columns("inf")
                .Caption = "STOCK"
                .Width = 80
                .Visible = True
            End With
            With grproductos.RootTable.Columns("yfresponsable")
                .Caption = "RESPONSABLE"
                .Width = 100
                .Visible = True
                .EditType = EditType.MultiColumnDropDown
                .DropDown = cbResp.DropDownList
            End With
            With grproductos.RootTable.Columns("yflado")
                .Caption = "LADO"
                .Width = 90
                .Visible = True
                .EditType = EditType.MultiColumnDropDown
                .DropDown = cbLado.DropDownList
            End With
            With grproductos.RootTable.Columns("yfordenacion")
                .Caption = "ORDENACIÓN"
                .Width = 90
                .Visible = True
            End With
            With grproductos.RootTable.Columns("est")
                .Caption = "ESTADO"
                .Width = 100
                .Visible = True
            End With
            With grproductos.RootTable.Columns("estado")
                .Caption = "ESTADO"
                .Width = 100
                .Visible = False
            End With
            'Habilitar Filtradores
            With grproductos
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

    Private Sub _prCargarComboLibreriaResp(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnMostrarUsuariosConteo()
        dt.Rows.Add(0, "NADIE")
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("ydnumi").Width = 70
            .DropDownList.Columns("ydnumi").Caption = "COD"
            .DropDownList.Columns.Add("yduser").Width = 200
            .DropDownList.Columns("yduser").Caption = "RESPONSABLE"
            .ValueMember = "yduser"
            .DisplayMember = "yduser"
            .DataSource = dt
            .Refresh()
        End With
    End Sub
    Private Sub _prCargarComboLado(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable

        dt.Columns.Add("COD")
        dt.Columns.Add("LADO")

        dt.Rows.Add(1, "LADO A")
        dt.Rows.Add(2, "LADO B")
        dt.Rows.Add(3, "S/L")

        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("COD").Width = 50
            .DropDownList.Columns("COD").Caption = "COD"
            .DropDownList.Columns.Add("LADO").Width = 120
            .DropDownList.Columns("LADO").Caption = "LADO"
            .ValueMember = "LADO"
            .DisplayMember = "LADO"
            .DataSource = dt
            .Refresh()
        End With

        'If dt.Rows.Count > 0 Then
        '    mCombo.SelectedIndex = 3
        'End If
    End Sub
    Private Sub _prInhabiliitar()

        btnModificar.Enabled = True
        btnGrabar.Enabled = False
        _prCargarTabla(True)

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

    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        If (_fnAccesible()) Then
            _prInhabiliitar()
        Else
            _modulo.Select()
            Me.Close()
        End If
    End Sub



    Private Sub grproductos_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grproductos.EditingCell

        If btnGrabar.Enabled = False Then
            e.Cancel = True
            Return
        End If
        If (_fnAccesible() And IsNothing(grproductos.DataSource) = False) Then
            'Deshabilitar la columna de Productos y solo habilitar la de los precios
            If (e.Column.Index = grproductos.RootTable.Columns("yfnumi").Index Or
                e.Column.Index = grproductos.RootTable.Columns("yfcprod").Index Or
                e.Column.Index = grproductos.RootTable.Columns("yfcdprod2").Index Or
                e.Column.Index = grproductos.RootTable.Columns("yfcbarra").Index Or
                e.Column.Index = grproductos.RootTable.Columns("yfcdprod1").Index Or
                e.Column.Index = grproductos.RootTable.Columns("grupo1").Index Or
                e.Column.Index = grproductos.RootTable.Columns("inf").Index Or
                e.Column.Index = grproductos.RootTable.Columns("Est").Index) Then
                e.Cancel = True
            Else
                e.Cancel = False
            End If
        Else
            e.Cancel = True
        End If
    End Sub
    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click

        Dim grabar As Boolean = L_fnActualizarProductoTY005("", CType(grproductos.DataSource, DataTable))
        If (grabar) Then
            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "Asignación de Productos Conteo Actualizados con éxito".ToUpper,
                                      img, 3500,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter
                                      )

            _prCargarTabla(True)
            _prInhabiliitar()

        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "Asignación de Productos Conteo no pudo ser insertado".ToUpper, img, 2500, eToastGlowColor.Red, eToastPosition.TopCenter)
        End If

    End Sub

    Private Sub cbAlmacen_ValueChanged(sender As Object, e As EventArgs) Handles cbAlmacen.ValueChanged
        _prCargarTabla(True) ''Si el selecciona otra sucursal cambia sus precio por sucursales
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
                Dim _fila As Integer = grproductos.GetRows.Length
                Dim _columna As Integer = grproductos.RootTable.Columns.Count
                Dim _archivo As String = _ubicacion & "\ListaDeProdConteo_" & Now.Date.Day &
                    "." & Now.Date.Month & "." & Now.Date.Year & "_" & Now.Hour & "." & Now.Minute & "." & Now.Second & ".csv"
                Dim _linea As String = ""
                Dim _filadata = 0, columndata As Int32 = 0
                File.Delete(_archivo)
                _stream = File.OpenWrite(_archivo)
                _escritor = New StreamWriter(_stream, System.Text.Encoding.UTF8)

                For Each _col As GridEXColumn In grproductos.RootTable.Columns
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

                For Each _fil As GridEXRow In grproductos.GetRows
                    For Each _col As GridEXColumn In grproductos.RootTable.Columns
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
            ToastNotification.Show(Me, "EXPORTACIÓN DE LISTA DE PRODUCTOS CONTEO EXITOSA..!!!",
                                       img, 2000,
                                       eToastGlowColor.Green,
                                       eToastPosition.BottomCenter)
        Else
            ToastNotification.Show(Me, "FALLÓ LA EXPORTACIÓN DE LISTA DE PRODUCTOS CONTEO..!!!",
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

    Private Sub grproductos_CellValueChanged(sender As Object, e As ColumnActionEventArgs) Handles grproductos.CellValueChanged
        Dim lin As Integer = grproductos.GetValue("yfnumi")
        Dim pos As Integer = -1
        _fnObtenerFilaDetalle(pos, lin)

        If (_fnAccesible()) Then
            If (e.Column.Index = grproductos.RootTable.Columns("yfordenacion").Index) Then
                If (Not IsNumeric(grproductos.GetValue("yfordenacion")) Or grproductos.GetValue("yfordenacion").ToString = String.Empty) Then
                    CType(grproductos.DataSource, DataTable).Rows(pos).Item("yfordenacion") = 0
                    grproductos.SetValue("yfordenacion", 0)

                End If
            End If
        End If

        Dim estado As Integer = CType(grproductos.DataSource, DataTable).Rows(pos).Item("estado")
        If (estado = 1) Then
            CType(grproductos.DataSource, DataTable).Rows(pos).Item("estado") = 2
        End If
    End Sub
    Public Sub _fnObtenerFilaDetalle(ByRef pos As Integer, numi As Integer)
        For i As Integer = 0 To CType(grproductos.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _numi As Integer = CType(grproductos.DataSource, DataTable).Rows(i).Item("yfnumi")
            If (_numi = numi) Then
                pos = i
                Return
            End If
        Next

    End Sub

End Class