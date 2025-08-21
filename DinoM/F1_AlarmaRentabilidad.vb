
Imports System.IO
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Imports Janus.Windows.GridEX
Imports Logica.AccesoLogica

Public Class F1_AlarmaRentabilidad
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
        Me.Text = "ALARMA DE RENTABILIDAD"
        tbMargenMin.Value = 7
        tbMargenMax.Value = 25

        Dim blah As New Bitmap(New Bitmap(My.Resources.alarma), 20, 20)
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico

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
        Dim MargenMin As Integer = tbMargenMin.Value
        Dim MargenMax As Integer = tbMargenMax.Value
        Dim dt, table, alarma As New DataTable
        If swStock.Value = True Then
            dt = L_CalculoRentabilidad()
        Else
            dt = L_CalculoRentabilidad()
            table = dt.Clone
            Dim row As DataRow() = dt.Select("Stock>0")

            For Each ldrRow As DataRow In row
                table.ImportRow(ldrRow)
            Next
            dt = table.Copy
        End If

        If dt.Rows.Count > 0 Then
            For i = 0 To dt.Rows.Count - 1
                If dt.Rows(i).Item("PrecioVentaNetoMin") > 0 Then
                    dt.Rows(i).Item("MargenMin") = Format((((dt.Rows(i).Item("PrecioVentaNetoMin") - dt.Rows(i).Item("PrecioCostoNeto")) / dt.Rows(i).Item("PrecioVentaNetoMin")) * 100), "#.#0")
                Else
                    dt.Rows(i).Item("MargenMin") = 0.00
                End If
                If dt.Rows(i).Item("PrecioVentaNetoMax") > 0 Then
                    dt.Rows(i).Item("MargenMax") = Format((((dt.Rows(i).Item("PrecioVentaNetoMax") - dt.Rows(i).Item("PrecioCostoNeto")) / dt.Rows(i).Item("PrecioVentaNetoMax")) * 100), "#.#0")
                Else
                    dt.Rows(i).Item("MargenMax") = 0.00
                End If


                ''Alertas
                If dt.Rows(i).Item("MargenMin") >= MargenMin And dt.Rows(i).Item("MargenMin") <= MargenMax Then
                    dt.Rows(i).Item("AlertaMin") = "NO"
                Else
                    dt.Rows(i).Item("AlertaMin") = "SI"
                End If

                If dt.Rows(i).Item("MargenMax") >= MargenMin And dt.Rows(i).Item("MargenMax") <= MargenMax Then
                    dt.Rows(i).Item("AlertaMax") = "NO"
                Else
                    dt.Rows(i).Item("AlertaMax") = "SI"
                End If

                If dt.Rows(i).Item("AlertaMin") = "SI" Or dt.Rows(i).Item("AlertaMax") = "SI" Then
                    dt.Rows(i).Item("AlertaFinal") = "SI"
                Else
                    dt.Rows(i).Item("AlertaFinal") = "NO"
                End If

            Next

            If swAlarma.Value = True Then
                alarma = dt.Clone
                Dim row As DataRow() = dt.Select("AlertaFinal='SI'")

                For Each ldrRow As DataRow In row
                    alarma.ImportRow(ldrRow)
                Next
                dt = alarma.Copy
            End If

            JGrM_Buscador.DataSource = dt
            JGrM_Buscador.RetrieveStructure()
            JGrM_Buscador.AlternatingColors = True

            With JGrM_Buscador.RootTable.Columns("ycdes3")
                .Width = 130
                .Visible = True
                .Caption = "PROVEEDOR"
            End With
            With JGrM_Buscador.RootTable.Columns("ProductoId")
                .Width = 100
                .Caption = "COD. DYNASYS"
                .Visible = True
            End With
            With JGrM_Buscador.RootTable.Columns("CodigoExterno")
                .Width = 100
                .Caption = "COD. DELTA"
                .Visible = True
            End With
            With JGrM_Buscador.RootTable.Columns("CodigoBarra")
                .Width = 100
                .Caption = "COD. BARRAS"
                .Visible = True
            End With
            With JGrM_Buscador.RootTable.Columns("NombreProducto")
                .Width = 400
                .Caption = "DESCRIPCIÓN"
                .Visible = True
            End With
            With JGrM_Buscador.RootTable.Columns("Stock")
                .Width = 130
                .Caption = "STOCK ACTUAL"
                .Visible = True
                .FormatString = "0.00"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            End With
            With JGrM_Buscador.RootTable.Columns("PrecioCosto")
                .Width = 150
                .Caption = "PRECIO COSTO BRUTO"
                .Visible = True
                .FormatString = "0.00"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            End With
            With JGrM_Buscador.RootTable.Columns("PrecioVenta")
                .Width = 150
                .Caption = "(A) PRECIO WHOLESALE BRUTO "
                .Visible = True
                .FormatString = "0.00"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            End With
            With JGrM_Buscador.RootTable.Columns("PrecioEspecial")
                .Width = 150
                .Caption = "(B) PRECIO PREFERENCIAL BRUTO"
                .Visible = True
                .FormatString = "0.00"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                '.AggregateFunction = AggregateFunction.Sum
            End With
            With JGrM_Buscador.RootTable.Columns("PrecioPDV")
                .Width = 150
                .Caption = "(C) PRECIO PDV BRUTO"
                .Visible = True
                .FormatString = "0.00"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                '.AggregateFunction = AggregateFunction.Sum
            End With
            With JGrM_Buscador.RootTable.Columns("ObsCompra")
                .Width = 120
                .Caption = "OBS. COMPRA"
                .Visible = True
            End With
            With JGrM_Buscador.RootTable.Columns("PrecioCostoNeto")
                .Width = 120
                .Caption = "PRECIO COSTO NETO"
                .Visible = True
                .FormatString = "0.00"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                '.AggregateFunction = AggregateFunction.Sum
            End With
            With JGrM_Buscador.RootTable.Columns("PrecioVentaNetoMin")
                .Width = 120
                .Caption = "(C) PRECIO MIN. PDV NETO"
                .Visible = True
                .FormatString = "0.00"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            End With
            With JGrM_Buscador.RootTable.Columns("PrecioVentaNetoMax")
                .Width = 120
                .Caption = "(A) PRECIO MAX. WHOLESALE NETO"
                .Visible = True
                .FormatString = "0.00"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            End With
            With JGrM_Buscador.RootTable.Columns("MargenMin")
                .Width = 150
                .Caption = "MARGEN (C) PRECIO MÍN. %"
                .Visible = True
                .FormatString = "0.00"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            End With
            With JGrM_Buscador.RootTable.Columns("MargenMax")
                .Width = 150
                .Caption = "MARGEN (A) PRECIO MÁX. %"
                .Visible = True
                .FormatString = "0.00"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            End With
            With JGrM_Buscador.RootTable.Columns("AlertaMin")
                .Width = 120
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .Caption = "ALERTA (C) PRECIO MÍN."
                .Visible = True
            End With
            With JGrM_Buscador.RootTable.Columns("AlertaMax")
                .Width = 120
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .Caption = "ALERTA (A) PRECIO MAX."
                .Visible = True
            End With
            With JGrM_Buscador.RootTable.Columns("AlertaFinal")
                .Width = 120
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .Caption = "ALERTA FINAL"
                .Visible = False
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
            _prAplicarCondiccionJanus()
            L_fnBotonGenerar(gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina, 0, "ALARMA RENTABILIDAD", "ALARMA RENTABILIDAD")
        Else
            JGrM_Buscador.ClearStructure()
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "No existe datos para mostrar".ToUpper, img, 3000, eToastGlowColor.Red, eToastPosition.TopCenter)
        End If

    End Sub
    Public Sub _prAplicarCondiccionJanus()
        Dim fr As GridEXFormatCondition
        fr = New GridEXFormatCondition(JGrM_Buscador.RootTable.Columns("AlertaMin"), ConditionOperator.Equal, "SI")
        fr.FormatStyle.ForeColor = Color.Red
        JGrM_Buscador.RootTable.FormatConditions.Add(fr)

        Dim fr1 As GridEXFormatCondition
        fr1 = New GridEXFormatCondition(JGrM_Buscador.RootTable.Columns("AlertaMax"), ConditionOperator.Equal, "SI")
        fr1.FormatStyle.ForeColor = Color.Red
        JGrM_Buscador.RootTable.FormatConditions.Add(fr1)
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
                Dim _archivo As String = _ubicacion & "\AlarmaRentabilidad" & "_" & Now.Date.Day &
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
        If tbMargenMin.Value > 0 And tbMargenMax.Value > 0 Then
            _prCargarDatos()
        Else
            ToastNotification.Show(Me, "Debe llenar los campos requeridos que se encuentran con * !!!".ToUpper,
                           My.Resources.WARNING, 2500,
                           eToastGlowColor.Red,
                           eToastPosition.TopCenter)
        End If
    End Sub

    Private Sub btnExportarExcel_Click(sender As Object, e As EventArgs) Handles btnExportarExcel.Click
        _prCrearCarpetaReportes()
        Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
        If (P_ExportarExcel(RutaGlobal + "\Reporte\Reporte Productos")) Then
            L_fnBotonExportar(gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina, 0, "ALARMA RENTABILIDAD", "ALARMA RENTABILIDAD")
            ToastNotification.Show(Me, "EXPORTACIÓN DE ALARMA DE RENTABILIDAD EXITOSA..!!!",
                                       img, 2000,
                                       eToastGlowColor.Green,
                                       eToastPosition.BottomCenter)
        Else
            ToastNotification.Show(Me, "FALLÓ LA EXPORTACIÓN DE ALARMA DE RENTABILIDAD..!!!",
                                       My.Resources.WARNING, 2000,
                                       eToastGlowColor.Red,
                                       eToastPosition.BottomLeft)
        End If
    End Sub

End Class