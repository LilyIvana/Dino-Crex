
Imports Logica.AccesoLogica
Imports Janus.Windows.GridEX
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Imports System.IO


Public Class DescuentosCajeros
    Dim _inter As Integer = 0
#Region "Variables Globales"

    Dim _Pos As Integer
    Dim _Nuevo As Boolean
    Dim _Dsencabezado As DataSet

    Dim _BindingSource As BindingSource
    Dim _Modificar As Boolean

    Dim modif As Boolean = True

    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem

    Dim dtPreciosDesc As New DataTable
    Dim RutaGlobal As String = gs_CarpetaRaiz

#End Region
    Private Sub _PIniciarTodo()

        'L_prAbrirConexion()


        Me.Text = "CONSULTA  PRECIOS  CAJEROS "
        'Me.WindowState = FormWindowState.Maximized
        _prCargarProductos()

        btnGrabar.Visible = False
        btnNuevo.Visible = False

        tbDesde.IsInputReadOnly = True
        tbHasta.IsInputReadOnly = True
        tbPrecio.IsInputReadOnly = True

    End Sub
    Private Sub _prCargarProductos()
        Dim dt As New DataTable
        dt = L_fnListarProductosDescuentos()
        grProducto.DataSource = dt
        grProducto.RetrieveStructure()
        grProducto.AlternatingColors = True


        With grProducto.RootTable.Columns("ProductoId")
            .Width = 50
            .Caption = "Cod.Dynasys"
            .WordWrap = True
            .MaxLines = 3
            .Visible = True
        End With

        With grProducto.RootTable.Columns("CodigoExterno")
            .Caption = "Cod.Delta"
            .Width = 50
            .WordWrap = True
            .MaxLines = 3
            .Visible = True
        End With
        With grProducto.RootTable.Columns("CodigoBarra")
            .Caption = "Cod.Barra"
            .Width = 80
            .WordWrap = True
            .MaxLines = 3
            .Visible = True
        End With
        With grProducto.RootTable.Columns("NombreProducto")
            .Caption = "Productos"
            .Width = 230
            .Visible = True
            .WordWrap = True
            .MaxLines = 3
        End With
        With grProducto.RootTable.Columns("ycdes3")
            .Caption = "Proveedor"
            .Width = 100
            .Visible = True
            .WordWrap = True
            .MaxLines = 3
        End With
        With grProducto.RootTable.Columns("Stock")
            .Caption = "Stock"
            .Width = 50
            .Visible = True
            .FormatString = "0.00"
        End With
        With grProducto.RootTable.Columns("PrecioCosto")
            .Caption = "Precio Costo"
            .Width = 50
            .Visible = False
            .FormatString = "0.00"
        End With
        With grProducto.RootTable.Columns("PrecioVenta")
            .Caption = "Precio Wholesale"
            .Width = 60
            .Visible = True
            .FormatString = "0.00"
        End With
        With grProducto.RootTable.Columns("PrecioEspecial")
            .Caption = "Precio Preferencial"
            .Width = 60
            .Visible = True
            .FormatString = "0.00"
        End With
        With grProducto.RootTable.Columns("PrecioPDV")
            .Caption = "Precio PDV"
            .Width = 50
            .Visible = True
            .FormatString = "0.00"
        End With

        With grProducto
            .GroupByBoxVisible = False
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges

            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007

            .RecordNavigator = True
            .RecordNavigatorText = "Productos"
        End With


        If (dt.Rows.Count > 0) Then
            grProducto.Row = 0
        End If
    End Sub

    Private Sub _prCargarDescuentos(ProductoId As Integer)
        Dim dt As New DataTable
        dt = L_fnListarDescuentos(ProductoId)
        grdetalle.DataSource = dt

        grdetalle.RetrieveStructure()
        grdetalle.AlternatingColors = True


        With grdetalle.RootTable.Columns("id")
            .Width = 100
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("ProductoId")
            .Width = 100
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("Productos")
            .Width = 100
            .Visible = False
        End With

        With grdetalle.RootTable.Columns("CantidadInicial")
            .Caption = "Desde"
            .Width = 100
            .Visible = True
            .FormatString = "0"
        End With


        With grdetalle.RootTable.Columns("CantidadFinal")
            .Caption = "Hasta"
            .Width = 100
            .Visible = True
            .FormatString = "0"
        End With

        With grdetalle.RootTable.Columns("Precio")
            .Caption = "Precio"
            .Width = 100
            .Visible = True
            .FormatString = "0.00"
        End With



        With grdetalle.RootTable.Columns("FechaDesde")
            .Width = 90
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("FechaHasta")
            .Width = 90
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("Estado")
            .Width = 90
            .Visible = False
        End With
        With grdetalle
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
        End With



    End Sub

    Private Sub grProducto_SelectionChanged(sender As Object, e As EventArgs) Handles grProducto.SelectionChanged
        If (grProducto.Row >= 0) Then

            lbProducto.Text = grProducto.GetValue("NombreProducto")
            _prCargarDescuentos(grProducto.GetValue("ProductoId"))

        End If
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        tbDesde.IsInputReadOnly = False
        tbHasta.IsInputReadOnly = False
        tbPrecio.IsInputReadOnly = False


        tbDesde.Value = 0
        tbHasta.Value = 0
        tbPrecio.Value = 0
        tbDesde.Focus()

        btnNuevo.Visible = False
        btnGrabar.Visible = True

    End Sub

    Function validarDescuento(ByRef posicion As Integer) As Boolean

        Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)

        For i As Integer = 0 To dt.Rows.Count - 1 Step 1

            Dim estadoRegistro As Integer = dt.Rows(i).Item("estado")
            If (estadoRegistro >= 0) Then
                If (tbDesde.Value >= dt.Rows(i).Item("CantidadInicial") And tbDesde.Value <= dt.Rows(i).Item("CantidadFinal")) Then
                    posicion = i
                    Return True
                End If
                If (tbHasta.Value >= dt.Rows(i).Item("CantidadInicial") And tbHasta.Value <= dt.Rows(i).Item("CantidadFinal")) Then
                    posicion = i
                    Return True
                End If
            End If

        Next
        Return False



    End Function
    Public Function _ValidarCampos() As Boolean
        If (grProducto.Row < 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor debe dar click a un producto".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            Return False
        End If
        If (tbDesde.Value > tbHasta.Value) Then
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "La cantidad Hasta es mayor al Desde".ToUpper, img, 3000, eToastGlowColor.Red, eToastPosition.BottomCenter)

            Return False

        End If



        If tbDesde.Value.ToString = String.Empty Or tbHasta.Value.ToString = String.Empty Or tbPrecio.Value.ToString = String.Empty Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Los campos no pueden estar vacios por favor coloque datos mayores a 0".ToUpper, img, 4000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            Return False
        Else
            If (tbDesde.Value <= 0 Or tbHasta.Value <= 0 Or tbPrecio.Value <= 0) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "Las campos no pueden ser 0 por favor coloque datos mayores a 0".ToUpper, img, 4000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                Return False
            End If
        End If
        Dim posicion As Integer = -1
        If (validarDescuento(posicion)) Then
            Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)

            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "Ya existe un Descuento Programado con los datos a Insertar, Desde = " + Str(dt.Rows(posicion).Item("CantidadInicial")) + "  Hasta " + Str(dt.Rows(posicion).Item("CantidadFinal")), img, 4500, eToastGlowColor.Red, eToastPosition.BottomCenter)
            Return False
        End If

        Return True
    End Function

    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        Dim numi As String = ""
        If (_ValidarCampos()) Then
            'Grabar
            Dim res As Boolean = L_fnGrabarPreciosDescuentos(numi, grProducto.GetValue("ProductoId"), tbDesde.Value, tbHasta.Value,
                                                             tbPrecio.Value, gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina)

            If (res) Then
                ToastNotification.Show(Me, "Descuento de Producto Grabado con éxito.".ToUpper,
                                   My.Resources.GRABACION_EXITOSA,
                                   3000,
                                   eToastGlowColor.Green,
                                   eToastPosition.TopCenter)




                btnNuevo.Visible = True
                btnGrabar.Visible = False

                tbDesde.Value = 0
                tbHasta.Value = 0
                tbPrecio.Value = 0

                _prCargarDescuentos(grProducto.GetValue("ProductoId"))

            Else
                ToastNotification.Show(Me, "No se pudo grabar los descuentos.".ToUpper,
                                   My.Resources.WARNING,
                                   3000,
                                   eToastGlowColor.Red,
                                   eToastPosition.TopCenter)
            End If
        End If
    End Sub

    Private Sub ButtonX1_Click(sender As Object, e As EventArgs) Handles ButtonX1.Click
        Me.Close()

    End Sub

    Private Sub Descuentos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _PIniciarTodo()

    End Sub

    Private Sub EliminarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EliminarToolStripMenuItem.Click
        If (grdetalle.Row >= 0) Then
            L_fnEliminarDescuento(grdetalle.GetValue("id"), grdetalle.GetValue("ProductoId"), gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina)



            ToastNotification.Show(Me, "Descuento Eliminado Correctamente.".ToUpper,
                                My.Resources.GRABACION_EXITOSA,
                                3000,
                                eToastGlowColor.Green,
                                eToastPosition.TopCenter)

            _prCargarDescuentos(grProducto.GetValue("ProductoId"))

        End If
    End Sub

    Private Sub btnExportar_Click(sender As Object, e As EventArgs) Handles btnExportar.Click
        _prCrearCarpetaReportes()
        Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
        If (P_ExportarExcel(RutaGlobal + "\Reporte\Reporte Productos")) Then
            L_fnRepConsultaPreciosCajerosExcel(gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina)
            ToastNotification.Show(Me, "EXPORTACIÓN DE LISTA DE PRODUCTOS EXITOSA..!!!",
                                       img, 2000,
                                       eToastGlowColor.Green,
                                       eToastPosition.BottomCenter)
        Else
            ToastNotification.Show(Me, "FALLÓ LA EXPORTACIÓN DE LISTA DE PRODUCTOS..!!!",
                                       My.Resources.WARNING, 2000,
                                       eToastGlowColor.Red,
                                       eToastPosition.BottomLeft)
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
    Public Function P_ExportarExcel(_ruta As String) As Boolean
        Dim _ubicacion As String

        If (1 = 1) Then 'If(_directorio.ShowDialog = Windows.Forms.DialogResult.OK) Then
            '_ubicacion = _directorio.SelectedPath
            _ubicacion = _ruta
            Try
                Dim _stream As Stream
                Dim _escritor As StreamWriter
                Dim _fila As Integer = grProducto.GetRows.Length
                Dim _columna As Integer = grProducto.RootTable.Columns.Count
                Dim _archivo As String = _ubicacion & "\ListaDePreciosCajeros_" & Now.Date.Day &
                    "." & Now.Date.Month & "." & Now.Date.Year & "_" & Now.Hour & "." & Now.Minute & "." & Now.Second & ".csv"
                Dim _linea As String = ""
                Dim _filadata = 0, columndata As Int32 = 0
                File.Delete(_archivo)
                _stream = File.OpenWrite(_archivo)
                _escritor = New StreamWriter(_stream, System.Text.Encoding.UTF8)

                For Each _col As GridEXColumn In grProducto.RootTable.Columns
                    If (_col.Visible) Then
                        _linea = _linea & _col.Caption & ";"
                    End If
                Next
                _linea = Mid(CStr(_linea), 1, _linea.Length - 1)
                _escritor.WriteLine(_linea)
                _linea = Nothing


                For Each _fil As GridEXRow In grProducto.GetRows
                    For Each _col As GridEXColumn In grProducto.RootTable.Columns
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

    Private Sub btActualizar_Click(sender As Object, e As EventArgs) Handles btActualizar.Click
        _PIniciarTodo()

        Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
        ToastNotification.Show(Me, "Datos actualizados!!!".ToUpper,
                                  img, 2000,
                                  eToastGlowColor.Green,
                                  eToastPosition.TopCenter
                                  )
    End Sub
End Class