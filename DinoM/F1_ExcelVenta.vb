
Imports System.IO
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Imports Janus.Windows.GridEX
Imports Logica.AccesoLogica

Public Class F1_ExcelVenta
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
        Me.Text = "VENTAS DETALLADAS POR PRODUCTO"
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
    Private Sub _prCargarVenta()
        Dim fechaDesde As DateTime = tbFechaI.Value.ToString("yyyy/MM/dd")
        Dim fechaHasta As DateTime = tbFechaF.Value.ToString("yyyy/MM/dd")
        Dim dt, dtVentas As DataTable


        If swTipo.Value = True Then ''VENTAS DETALLADAS
            dt = L_VentasProductos(fechaDesde, fechaHasta)
            dtVentas = L_BuscarVentasAtendidas2(fechaDesde, fechaHasta, 0, 0, 0, 0)
        Else '' VENTAS GENERALES
            dt = L_VentasGenerales(fechaDesde, fechaHasta)
        End If


        If dt.Rows.Count > 0 Then
            If swTipo.Value = True Then

                Dim TotalVentasDetalladas, TotalVentas, TotalContado, TotalCredito As Double
                TotalVentasDetalladas = dt.Compute("Sum(Total)", "")
                TotalContado = dtVentas.Compute("Sum(TotalContado)", "")
                TotalCredito = dtVentas.Compute("Sum(TotalCredito)", "")
                TotalVentas = TotalContado + TotalCredito

                If TotalVentasDetalladas <> TotalVentas Then
                    JGrM_Buscador.ClearStructure()
                    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                    ToastNotification.Show(Me, "Existe diferencia de totales entre este reporte y el reporte de ventas, verificar cierres de caja!!!".ToUpper,
                                           img, 6000, eToastGlowColor.Red, eToastPosition.TopCenter)
                Else
                    L_fnBotonGenerar(gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina, 0, "VENTAS DETALLADAS POR PRODUCTO", "VENTAS DETALLADAS POR PRODUCTO")

                    JGrM_Buscador.DataSource = dt
                    JGrM_Buscador.RetrieveStructure()
                    JGrM_Buscador.AlternatingColors = True

                    With JGrM_Buscador.RootTable.Columns("FechaVenta")
                        .Width = 90
                        .Visible = True
                        .Caption = "FECHA"
                    End With
                    With JGrM_Buscador.RootTable.Columns("Dia")
                        .Width = 60
                        .Visible = True
                        .Caption = "DÍA"
                    End With
                    With JGrM_Buscador.RootTable.Columns("Mes")
                        .Width = 60
                        .Visible = True
                        .Caption = "MES"
                    End With
                    With JGrM_Buscador.RootTable.Columns("Anio")
                        .Width = 60
                        .Visible = True
                        .Caption = "AÑO"
                    End With
                    With JGrM_Buscador.RootTable.Columns("Autorizacion")
                        .Width = 170
                        .Caption = "COD. AUTORIZACIÓN"
                        .Visible = True
                        .FormatString = "0"
                    End With
                    With JGrM_Buscador.RootTable.Columns("CodControl")
                        .Width = 100
                        .Visible = False
                    End With
                    With JGrM_Buscador.RootTable.Columns("NroCaja")
                        .Width = 90
                        .Caption = "NRO. CAJA"
                        .Visible = True
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
                    With JGrM_Buscador.RootTable.Columns("tbty5prod")
                        .Width = 100
                        .Caption = "COD. DYNASYS"
                        .Visible = True
                    End With
                    With JGrM_Buscador.RootTable.Columns("CodDelta")
                        .Width = 100
                        .Caption = "COD. PRODUCTO"
                        .Visible = True
                    End With
                    With JGrM_Buscador.RootTable.Columns("yfcdprod1")
                        .Width = 380
                        .Caption = "DESCRIPCIÓN"
                        .Visible = True
                    End With
                    With JGrM_Buscador.RootTable.Columns("Unidad")
                        .Width = 100
                        .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        .Caption = "UNIDAD"
                        .Visible = True
                    End With
                    With JGrM_Buscador.RootTable.Columns("Conversion")
                        .Width = 90
                        .Caption = "CONVERSIÓN"
                        .Visible = True
                        .FormatString = "0.00"
                        .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    End With
                    With JGrM_Buscador.RootTable.Columns("Cantidad")
                        .Width = 100
                        .Caption = "CANTIDAD"
                        .Visible = True
                        .FormatString = "0.00"
                        .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        .AggregateFunction = AggregateFunction.Sum
                    End With
                    With JGrM_Buscador.RootTable.Columns("PrecioBase")
                        .Width = 120
                        .Caption = "PRECIO BASE"
                        .Visible = True
                        .FormatString = "0.00"
                        .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    End With
                    With JGrM_Buscador.RootTable.Columns("PrecioVendido")
                        .Width = 120
                        .Caption = "PRECIO VENDIDO"
                        .Visible = True
                        .FormatString = "0.00"
                        .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    End With
                    With JGrM_Buscador.RootTable.Columns("Descuento")
                        .Width = 100
                        .Caption = "DESCUENTO"
                        .Visible = True
                        .FormatString = "0.00"
                        .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    End With
                    With JGrM_Buscador.RootTable.Columns("Total")
                        .Width = 150
                        .Caption = "TOTAL VENTA"
                        .Visible = True
                        .FormatString = "0.00"
                        .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        .AggregateFunction = AggregateFunction.Sum
                    End With
                    With JGrM_Buscador.RootTable.Columns("Giftcard")
                        .Width = 150
                        .Caption = "GIFTCARD"
                        .Visible = True
                        .FormatString = "0.00"
                        .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        .AggregateFunction = AggregateFunction.Sum
                    End With
                    With JGrM_Buscador.RootTable.Columns("Importe")
                        .Width = 150
                        .Caption = "IMPORTE VENTA"
                        .Visible = True
                        .FormatString = "0.00"
                        .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        .AggregateFunction = AggregateFunction.Sum
                    End With
                    With JGrM_Buscador.RootTable.Columns("PrecioCosto")
                        .Width = 100
                        .Caption = "COSTO"
                        .Visible = True
                        .FormatString = "0.00"
                        .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    End With
                    With JGrM_Buscador.RootTable.Columns("TotalCosto")
                        .Width = 100
                        .Caption = "TOTAL COSTO"
                        .Visible = True
                        .FormatString = "0.00"
                        .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    End With
                    With JGrM_Buscador.RootTable.Columns("Vendedor")
                        .Width = 100
                        .Visible = True
                        .Caption = "VENDEDOR"
                    End With
                    With JGrM_Buscador.RootTable.Columns("Usuario")
                        .Width = 100
                        .Visible = True
                        .Caption = "USUARIO"
                    End With
                    With JGrM_Buscador.RootTable.Columns("yccod3")
                        .Width = 90
                        .Visible = False
                    End With
                    With JGrM_Buscador.RootTable.Columns("Proveedor")
                        .Width = 150
                        .Visible = True
                        .Caption = "PROVEEDOR"
                    End With
                    With JGrM_Buscador.RootTable.Columns("Marca")
                        .Width = 120
                        .Visible = True
                        .Caption = "MARCA"
                    End With
                    With JGrM_Buscador.RootTable.Columns("Categoria")
                        .Width = 150
                        .Visible = True
                        .Caption = "CATEGORÍA"
                    End With
                    With JGrM_Buscador.RootTable.Columns("Gramaje")
                        .Width = 120
                        .Visible = True
                        .Caption = "GRAMAJE"
                    End With
                    With JGrM_Buscador.RootTable.Columns("yfdetprod")
                        .Width = 220
                        .Visible = True
                        .Caption = "DESCRIPCIÓN DETALLADA"
                    End With
                    With JGrM_Buscador.RootTable.Columns("yfcbarra")
                        .Width = 150
                        .Visible = True
                        .Caption = "CÓDIGO DE BARRAS"
                    End With
                    With JGrM_Buscador.RootTable.Columns("CodOrigen")
                        .Width = 120
                        .Visible = True
                        .Caption = "COD. PROVEEDOR"
                    End With
                    With JGrM_Buscador.RootTable.Columns("pref")
                        .Width = 90
                        .Visible = True
                        .Caption = "SIGLA DEMANDA"
                    End With
                    With JGrM_Buscador.RootTable.Columns("PrefVenc")
                        .Width = 90
                        .Visible = True
                        .Caption = "COD. VENC."
                    End With
                    With JGrM_Buscador.RootTable.Columns("OBSERVACION")
                        .Width = 120
                        .Visible = True
                        .Caption = "OBSERVACIÓN"
                    End With
                    With JGrM_Buscador.RootTable.Columns("StockAct")
                        .Width = 100
                        .Visible = True
                        .Caption = "STOCK ACTUAL"
                        .FormatString = "0.00"
                    End With
                    With JGrM_Buscador.RootTable.Columns("tahact")
                        .Width = 80
                        .Visible = True
                        .Caption = "HORA"
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
                End If

            Else
                L_fnBotonGenerar(gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina, 0, "VENTAS GENERALES", "VENTAS GENERALES")

                JGrM_Buscador.DataSource = dt
                JGrM_Buscador.RetrieveStructure()
                JGrM_Buscador.AlternatingColors = True

                With JGrM_Buscador.RootTable.Columns("FechaVenta")
                    .Width = 90
                    .Visible = True
                    .Caption = "FECHA"
                End With
                With JGrM_Buscador.RootTable.Columns("Dia")
                    .Width = 60
                    .Visible = True
                    .Caption = "DÍA"
                End With
                With JGrM_Buscador.RootTable.Columns("Mes")
                    .Width = 60
                    .Visible = True
                    .Caption = "MES"
                End With
                With JGrM_Buscador.RootTable.Columns("Anio")
                    .Width = 60
                    .Visible = True
                    .Caption = "AÑO"
                End With
                With JGrM_Buscador.RootTable.Columns("IDVENTA")
                    .Width = 100
                    .Visible = True
                    .Caption = "IDVENTA"
                End With
                With JGrM_Buscador.RootTable.Columns("Autorizacion")
                    .Width = 170
                    .Caption = "COD. AUTORIZACIÓN"
                    .Visible = True
                    .FormatString = "0"
                End With
                With JGrM_Buscador.RootTable.Columns("NroCaja")
                    .Width = 90
                    .Caption = "NRO. CAJA"
                    .Visible = True
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
                With JGrM_Buscador.RootTable.Columns("SubTotal")
                    .Width = 120
                    .Caption = "SUBTOTAL"
                    .Visible = True
                    .FormatString = "0.00"
                    .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                End With
                With JGrM_Buscador.RootTable.Columns("Descuento")
                    .Width = 100
                    .Caption = "DESCUENTO"
                    .Visible = True
                    .FormatString = "0.00"
                    .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                End With
                With JGrM_Buscador.RootTable.Columns("Total")
                    .Width = 150
                    .Caption = "TOTAL VENTA"
                    .Visible = True
                    .FormatString = "0.00"
                    .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    .AggregateFunction = AggregateFunction.Sum
                End With
                With JGrM_Buscador.RootTable.Columns("Giftcard")
                    .Width = 150
                    .Caption = "GIFTCARD"
                    .Visible = True
                    .FormatString = "0.00"
                    .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    .AggregateFunction = AggregateFunction.Sum
                End With
                With JGrM_Buscador.RootTable.Columns("Importe")
                    .Width = 150
                    .Caption = "IMPORTE VENTA"
                    .Visible = True
                    .FormatString = "0.00"
                    .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    .AggregateFunction = AggregateFunction.Sum
                End With
                With JGrM_Buscador.RootTable.Columns("Vendedor")
                    .Width = 100
                    .Visible = True
                    .Caption = "VENDEDOR"
                End With
                With JGrM_Buscador.RootTable.Columns("Usuario")
                    .Width = 100
                    .Visible = True
                    .Caption = "USUARIO"
                End With
                With JGrM_Buscador.RootTable.Columns("TURNO")
                    .Width = 100
                    .Visible = True
                    .Caption = "TURNO"
                End With
                With JGrM_Buscador.RootTable.Columns("TIPOPAGO")
                    .Width = 130
                    .Visible = True
                    .Caption = "TIPO PAGO"
                End With
                With JGrM_Buscador.RootTable.Columns("CODCLIENTE")
                    .Width = 100
                    .Visible = True
                    .Caption = "COD CLIENTE"
                End With
                With JGrM_Buscador.RootTable.Columns("CLIENTE")
                    .Width = 100
                    .Visible = True
                    .Caption = "CLIENTE"
                End With
                With JGrM_Buscador.RootTable.Columns("VENDEDORASIGNADO")
                    .Width = 100
                    .Visible = True
                    .Caption = "VENDEDOR ASIGNADO"
                End With
                With JGrM_Buscador.RootTable.Columns("OBSERVACION")
                    .Width = 120
                    .Visible = True
                    .Caption = "OBSERVACIÓN"
                End With
                With JGrM_Buscador.RootTable.Columns("taEmpresa")
                    .Width = 100
                    .Visible = True
                    .Caption = "EMPRESA"
                End With
                With JGrM_Buscador.RootTable.Columns("taTipoVenta")
                    .Visible = False
                End With
                With JGrM_Buscador.RootTable.Columns("descripcion")
                    .Width = 100
                    .Visible = True
                    .Caption = "TIPO VENTA"
                End With
                With JGrM_Buscador.RootTable.Columns("taCantidad")
                    .Width = 100
                    .Visible = True
                    .Caption = "CANTIDAD"
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


            End If

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
        If (P_ExportarExcelGlobal(RutaGlobal + "\Reporte\Reporte Productos", JGrM_Buscador, IIf(swTipo.Value = True, "VentasDetalladasProductos", "VentasGenerales"))) Then
            L_fnBotonExportar(gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina, 0, IIf(swTipo.Value = True, "VENTAS DETALLADAS POR PRODUCTO", "VENTAS GENERALES"), IIf(swTipo.Value = True, "VENTAS DETALLADAS POR PRODUCTO", "VENTAS GENERALES"))

            ToastNotification.Show(Me, "EXPORTACIÓN DE " + IIf(swTipo.Value = True, "VENTAS DETALLADAS POR PRODUCTO", "VENTAS GENERALES") + " EXITOSA..!!!",
                                       img, 2000,
                                       eToastGlowColor.Green,
                                       eToastPosition.TopCenter)
        Else
            ToastNotification.Show(Me, "FALLÓ LA EXPORTACIÓN DE " + IIf(swTipo.Value = True, "VENTAS DETALLADAS POR PRODUCTOS", "VENTAS GENERALES") + " ..!!!",
                                       My.Resources.WARNING, 2000,
                                       eToastGlowColor.Red,
                                       eToastPosition.TopCenter)
        End If
    End Sub
End Class