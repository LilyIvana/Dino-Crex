﻿
Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports Janus.Windows.GridEX
Imports System.IO
Imports DevComponents.DotNetBar.SuperGrid
Imports DevComponents.DotNetBar.Controls

Public Class F1_ExcelCierresCaja
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
        Me.Text = "REPORTE CIERRES DE CAJA PARA EXPORTAR"
        tbFechaI.Value = Now.Date
        tbFechaF.Value = Now.Date

        Dim blah As New Bitmap(New Bitmap(My.Resources.producto), 20, 20)
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
    Private Sub _prCrearCarpetaTemporal()

        If System.IO.Directory.Exists(RutaTemporal) = False Then
            System.IO.Directory.CreateDirectory(RutaTemporal)
        Else
            Try
                My.Computer.FileSystem.DeleteDirectory(RutaTemporal, FileIO.DeleteDirectoryOption.DeleteAllContents)
                My.Computer.FileSystem.CreateDirectory(RutaTemporal)
                'My.Computer.FileSystem.DeleteDirectory(RutaTemporal, FileIO.UIOption.AllDialogs, FileIO.RecycleOption.SendToRecycleBin)
                'System.IO.Directory.CreateDirectory(RutaTemporal)

            Catch ex As Exception

            End Try

        End If

    End Sub
    Private Sub _prCrearCarpetaImagenes()
        Dim rutaDestino As String = RutaGlobal + "\Imagenes\Imagenes ProductoDino\"

        If System.IO.Directory.Exists(RutaGlobal + "\Imagenes\Imagenes ProductoDino\") = False Then
            If System.IO.Directory.Exists(RutaGlobal + "\Imagenes") = False Then
                System.IO.Directory.CreateDirectory(RutaGlobal + "\Imagenes")
                If System.IO.Directory.Exists(RutaGlobal + "\Imagenes\Imagenes ProductoDino") = False Then
                    System.IO.Directory.CreateDirectory(RutaGlobal + "\Imagenes\Imagenes ProductoDino")
                End If
            Else
                If System.IO.Directory.Exists(RutaGlobal + "\Imagenes\Imagenes ProductoDino") = False Then
                    System.IO.Directory.CreateDirectory(RutaGlobal + "\Imagenes\Imagenes ProductoDino")

                End If
            End If
        End If
    End Sub

    Private Sub _prCrearCarpetaReportes()
        Dim rutaDestino As String = RutaGlobal + "\Reporte\Reporte CierreCajas\"

        If System.IO.Directory.Exists(RutaGlobal + "\Reporte\Reporte CierreCajas\") = False Then
            If System.IO.Directory.Exists(RutaGlobal + "\Reporte") = False Then
                System.IO.Directory.CreateDirectory(RutaGlobal + "\Reporte")
                If System.IO.Directory.Exists(RutaGlobal + "\Reporte\Reporte CierreCajas") = False Then
                    System.IO.Directory.CreateDirectory(RutaGlobal + "\Reporte\Reporte CierreCajas")
                End If
            Else
                If System.IO.Directory.Exists(RutaGlobal + "\Reporte\Reporte CierreCajas") = False Then
                    System.IO.Directory.CreateDirectory(RutaGlobal + "\Reporte\Reporte CierreCajas")

                End If
            End If
        End If
    End Sub
    Private Sub _prCargarVenta()
        Dim fechaDesde As DateTime = tbFechaI.Value.ToString("yyyy/MM/dd")
        Dim fechaHasta As DateTime = tbFechaF.Value.ToString("yyyy/MM/dd")
        Dim dt As DataTable
        If swTipo.Value = True Then
            dt = L_RepCierresCajaResumen(fechaDesde, fechaHasta)
        Else
            dt = L_RepCierresCajaDetallado(fechaDesde, fechaHasta)
        End If

        If dt.Rows.Count > 0 And swTipo.Value = True Then
            L_fnBotonGenerar(gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina, 0, "RESÚMEN CIERRES DE CAJA", "RESÚMEN CIERRES DE CAJA")

            JGrM_Buscador.DataSource = dt
            JGrM_Buscador.RetrieveStructure()
            JGrM_Buscador.AlternatingColors = True

            With JGrM_Buscador.RootTable.Columns("FechaVenta")
                .Width = 90
                .Visible = True
                .Caption = "FECHA"
            End With
            With JGrM_Buscador.RootTable.Columns("NroCaja")
                .Width = 90
                .Caption = "NRO. CAJA"
                .Visible = True
            End With
            With JGrM_Buscador.RootTable.Columns("Usuario")
                .Width = 100
                .Visible = True
                .Caption = "USUARIO"
            End With
            With JGrM_Buscador.RootTable.Columns("Turno")
                .Width = 100
                .Visible = True
                .Caption = "TURNO"
            End With
            With JGrM_Buscador.RootTable.Columns("Inicial")
                .Width = 100
                .Caption = "MONTO INICIAL"
                .Visible = True
                .FormatString = "0.00"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .AggregateFunction = AggregateFunction.Sum
            End With
            With JGrM_Buscador.RootTable.Columns("Contado")
                .Width = 120
                .Caption = "CONTADO BS."
                .Visible = True
                .FormatString = "0.00"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .AggregateFunction = AggregateFunction.Sum
            End With
            With JGrM_Buscador.RootTable.Columns("Credito")
                .Width = 120
                .Caption = "CRÉDITO"
                .Visible = True
                .FormatString = "0.00"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .AggregateFunction = AggregateFunction.Sum
            End With
            With JGrM_Buscador.RootTable.Columns("Tarjeta")
                .Width = 120
                .Caption = "TARJETA"
                .Visible = True
                .FormatString = "0.00"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .AggregateFunction = AggregateFunction.Sum
            End With
            With JGrM_Buscador.RootTable.Columns("Qr")
                .Width = 120
                .Caption = "QR"
                .Visible = True
                .FormatString = "0.00"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .AggregateFunction = AggregateFunction.Sum
            End With
            With JGrM_Buscador.RootTable.Columns("Pagos")
                .Width = 120
                .Caption = "PAGOS"
                .Visible = True
                .FormatString = "0.00"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .AggregateFunction = AggregateFunction.Sum
            End With
            With JGrM_Buscador.RootTable.Columns("Ingreso")
                .Width = 120
                .Caption = "INGRESOS"
                .Visible = True
                .FormatString = "0.00"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .AggregateFunction = AggregateFunction.Sum
            End With
            With JGrM_Buscador.RootTable.Columns("Egreso")
                .Width = 120
                .Caption = "EGRESOS"
                .Visible = True
                .FormatString = "0.00"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .AggregateFunction = AggregateFunction.Sum
            End With
            With JGrM_Buscador.RootTable.Columns("TotalVenta")
                .Width = 120
                .Caption = "TOTAL VENTA BS."
                .Visible = True
                .FormatString = "0.00"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .AggregateFunction = AggregateFunction.Sum
            End With
            With JGrM_Buscador.RootTable.Columns("Total")
                .Width = 150
                .Caption = "TOTAL CAJA BS."
                .Visible = True
                .FormatString = "0.00"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .AggregateFunction = AggregateFunction.Sum
            End With
            With JGrM_Buscador.RootTable.Columns("Cortes")
                .Width = 120
                .Caption = "TOTAL CORTES BS."
                .Visible = True
                .FormatString = "0.00"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .AggregateFunction = AggregateFunction.Sum
            End With
            With JGrM_Buscador.RootTable.Columns("Depositos")
                .Width = 120
                .Caption = "DEPÓSITOS"
                .Visible = True
                .FormatString = "0.00"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .AggregateFunction = AggregateFunction.Sum
            End With
            With JGrM_Buscador.RootTable.Columns("Diferencia")
                .Width = 120
                .Caption = "DIFERENCIA"
                .Visible = True
                .FormatString = "0.00"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .AggregateFunction = AggregateFunction.Sum
            End With
            With JGrM_Buscador.RootTable.Columns("Obs")
                .Width = 250
                .Caption = "OBSERVACIÓN"
                .Visible = True
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

        ElseIf dt.Rows.Count > 0 And swTipo.Value = False Then
            JGrM_Buscador.DataSource = dt
            JGrM_Buscador.RetrieveStructure()
            JGrM_Buscador.AlternatingColors = True

            With JGrM_Buscador.RootTable.Columns("FechaVenta")
                .Width = 90
                .Visible = True
                .Caption = "FECHA"
            End With
            With JGrM_Buscador.RootTable.Columns("NroCaja")
                .Width = 90
                .Caption = "NRO. CAJA"
                .Visible = True
            End With
            With JGrM_Buscador.RootTable.Columns("Usuario")
                .Width = 100
                .Visible = True
                .Caption = "USUARIO"
            End With
            With JGrM_Buscador.RootTable.Columns("Turno")
                .Width = 100
                .Visible = True
                .Caption = "TURNO"
            End With
            With JGrM_Buscador.RootTable.Columns("IdVenta")
                .Width = 100
                .Visible = True
                .Caption = "IDVENTA/PAGO"
            End With
            With JGrM_Buscador.RootTable.Columns("NroFactura")
                .Width = 120
                .Caption = "NRO. FACTURA"
                .Visible = True
            End With
            With JGrM_Buscador.RootTable.Columns("Nit")
                .Width = 100
                .Caption = "NIT"
                .Visible = True
            End With
            With JGrM_Buscador.RootTable.Columns("RazonSocial")
                .Width = 150
                .Caption = "RAZÓN SOCIAL"
                .Visible = True
            End With
            With JGrM_Buscador.RootTable.Columns("Contado")
                .Width = 120
                .Caption = "CONTADO BS."
                .Visible = True
                .FormatString = "0.00"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .AggregateFunction = AggregateFunction.Sum
            End With
            With JGrM_Buscador.RootTable.Columns("Contado$")
                .Width = 120
                .Caption = "CONTADO $"
                .Visible = True
                .FormatString = "0.00"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .AggregateFunction = AggregateFunction.Sum
            End With
            With JGrM_Buscador.RootTable.Columns("Credito")
                .Width = 120
                .Caption = "CRÉDITO"
                .Visible = True
                .FormatString = "0.00"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .AggregateFunction = AggregateFunction.Sum
            End With
            With JGrM_Buscador.RootTable.Columns("Tarjeta")
                .Width = 120
                .Caption = "TARJETA"
                .Visible = True
                .FormatString = "0.00"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .AggregateFunction = AggregateFunction.Sum
            End With
            With JGrM_Buscador.RootTable.Columns("Qr")
                .Width = 120
                .Caption = "QR"
                .Visible = True
                .FormatString = "0.00"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .AggregateFunction = AggregateFunction.Sum
            End With
            With JGrM_Buscador.RootTable.Columns("Pagos")
                .Width = 120
                .Caption = "PAGOS"
                .Visible = True
                .FormatString = "0.00"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .AggregateFunction = AggregateFunction.Sum
            End With
            With JGrM_Buscador.RootTable.Columns("TipoPago")
                .Width = 120
                .Caption = "TIPO PAGO"
                .Visible = True
            End With
            With JGrM_Buscador.RootTable.Columns("CodCliente")
                .Visible = False
            End With
            With JGrM_Buscador.RootTable.Columns("Cliente")
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
                Dim _archivo As String = _ubicacion & "\REPORTECIERRECAJAS_" & Now.Date.Day &
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
        _prCargarVenta()
    End Sub

    Private Sub btnExportarExcel_Click(sender As Object, e As EventArgs) Handles btnExportarExcel.Click
        _prCrearCarpetaReportes()
        Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
        If (P_ExportarExcel(RutaGlobal + "\Reporte\Reporte Cierrecajas")) Then
            L_fnBotonExportar(gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina, 0, "RESÚMEN CIERRES DE CAJA", "RESÚMEN CIERRES DE CAJA")

            ToastNotification.Show(Me, "EXPORTACIÓN DE REP. CIERRE DE CAJA EXITOSA...!!!",
                                       img, 2000,
                                       eToastGlowColor.Green,
                                       eToastPosition.BottomCenter)
        Else
            ToastNotification.Show(Me, "FALLÓ LA EXPORTACIÓN DE REP. CIERRE DE CAJA...!!!",
                                       My.Resources.WARNING, 2000,
                                       eToastGlowColor.Red,
                                       eToastPosition.BottomLeft)
        End If
    End Sub
End Class