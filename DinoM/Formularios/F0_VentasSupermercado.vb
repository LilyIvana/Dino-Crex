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
Imports System.Drawing.Printing
Imports CrystalDecisions.Shared
Imports Facturacion
Imports UTILITIES

'importando librerias api conexion
Imports Newtonsoft.Json
Imports DinoM.LoginResp
Imports DinoM.ConnSiat
Imports DinoM.RespMetodosPago
Imports DinoM.EmisorResp
Imports DinoM.RespTipoDoc
Imports DinoM.NitResp
Imports System.Xml

Public Class F0_VentasSupermercado

    Dim _CodCliente As Integer = 2
    Dim _CodEmpleado As Integer = 0
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
    Dim FilaSelectLote As DataRow = Nothing
    Dim Table_Producto As DataTable
    Dim G_Lote As Boolean = False '1=igual a mostrar las columnas de lote y fecha de Vencimiento
    Dim Sucursal As Integer = 1
    Dim OcultarFact As Integer = 0
    Dim _codeBar As Integer = 1
    Dim _dias As Integer = 0

    Public TotalBs As Double = 0
    Public TotalSus As Double = 0
    Public TotalTarjeta As Double = 0
    Public TipoCambio As Double = 0
    Dim ListImagenes As String()
    Dim contador As Integer = 0

    Dim dtDescuentos As DataTable = Nothing
    Public Programa As String

    Public IdNit As String

    'Token SIFAC
    Public tokenObtenido
    Public dtDetalle As DataTable
    Public dt As DataTable

    Public CodProducto As String
    Public Cantidad As Integer
    Public PrecioU As Double
    Public PrecioTot As Double
    Public NombreProd As String
    Public NroFact As Integer

    Public _Fecha As Date

    Public QrUrl As String
    Public FactUrl As String
    Public SegundaLeyenda As String
    Public TerceraLeyenda As String
    Public Cudf As String

    Public NroTarjeta As String

    Public CodExcepcion As Integer




#Region "Metodos Privados"
    Private Sub _IniciarTodo()
        L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        'Me.WindowState = FormWindowState.Maximized
        ObtenerImagenes()
        _prValidarLote()
        'lbTipoMoneda.Visible = False
        P_prCargarVariablesIndispensables()
        Dim blah As New Bitmap(New Bitmap(My.Resources.compra), 20, 20)
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico
        P_prCargarParametro()
        tbTotal.Value = 0

        TimerImagenes.Start()

        lbNroCaja.Text = gs_NroCaja
        lbUsuario.Text = gs_user

        _CargarBanner()

        Programa = P_Principal.btnVentaRapida.Text
    End Sub
    Private Sub _CargarBanner()
        Dim ubicacion As String
        ubicacion = gs_CarpetaRaiz + "\Banner.jpg"
        ubicacion = gs_CarpetaRaiz + "\Banner.jpeg"
        ubicacion = gs_CarpetaRaiz + "\Banner.png"

        Try
            PictureBox2.Image = Image.FromFile(ubicacion)
        Catch ex As Exception
            MessageBox.Show("No se encontro el logo en la ubicación específicada" + ubicacion)
        End Try
    End Sub
    Public Sub CalcularDescuentosTotal()

        Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)

        Dim sumaDescuento As Double = 0

        For i As Integer = 0 To dt.Rows.Count - 1 Step 1

            If (dt.Rows(i).Item("estado") >= 0) Then

                sumaDescuento += dt.Rows(i).Item("tbdesc")

            End If

        Next

        tbDescuento.Value = sumaDescuento

    End Sub
    'Public Sub CalcularDescuentos(ProductoId As Integer, Cantidad As Integer, Precio As Decimal, Posicion As Integer)


    '    Dim fila As DataRow() = dtDescuentos.Select("ProductoId=" + Str(ProductoId).ToString.Trim + "", "")

    '    For Each dr As DataRow In fila

    '        Dim CantidadInicial As Integer = dr.Item("CantidadInicial")
    '        Dim CantidadFinal As Integer = dr.Item("CantidadFinal")
    '        Dim PrecioDescuento As Double = dr.Item("Precio")

    '        If (Cantidad >= CantidadInicial And Cantidad <= CantidadFinal) Then

    '            Dim SubTotalDescuento As Double = Cantidad * PrecioDescuento
    '            Dim Descuento As Double = (Cantidad * Precio) - SubTotalDescuento
    '            CType(grdetalle.DataSource, DataTable).Rows(Posicion).Item("tbdesc") = Descuento
    '            'grdetalle.SetValue("tbdesc", Descuento)
    '            CType(grdetalle.DataSource, DataTable).Rows(Posicion).Item("tbtotdesc") = (CType(grdetalle.DataSource, DataTable).Rows(Posicion).Item("tbpbas") * CType(grdetalle.DataSource, DataTable).Rows(Posicion).Item("tbcmin")) - Descuento

    '            ' grdetalle.SetValue("tbtotdesc", ((CType(grdetalle.DataSource, DataTable).Rows(Posicion).Item("tbpbas") * CType(grdetalle.DataSource, DataTable).Rows(Posicion).Item("tbcmin")) - Descuento))
    '            Return

    '        Else

    '            CType(grdetalle.DataSource, DataTable).Rows(Posicion).Item("tbdesc") = 0
    '            CType(grdetalle.DataSource, DataTable).Rows(Posicion).Item("tbtotdesc") = (CType(grdetalle.DataSource, DataTable).Rows(Posicion).Item("tbpbas") * CType(grdetalle.DataSource, DataTable).Rows(Posicion).Item("tbcmin"))


    '        End If

    '    Next

    'End Sub

    Public Sub CalcularDescuentos(ProductoId As Integer, Cantidad As Integer, Precio As Decimal, Posicion As Integer)
        Dim preciod, total1, total2, descuentof, cantf As Double

        Dim fila As DataRow() = dtDescuentos.Select("ProductoId=" + Str(ProductoId).ToString.Trim + "", "")

        'Cálculo de descuentos si es sin familia
        If CType(grdetalle.DataSource, DataTable).Rows(Posicion).Item("tbfamilia") = 1 Then

            For Each dr As DataRow In fila

                Dim CantidadInicial As Integer = dr.Item("CantidadInicial")
                Dim CantidadFinal As Integer = dr.Item("CantidadFinal")
                Dim PrecioDescuento As Double = dr.Item("Precio")

                If (Cantidad >= CantidadInicial And Cantidad <= CantidadFinal) Then

                    Dim SubTotalDescuento As Double = Cantidad * PrecioDescuento
                    Dim Descuento As Double = (Cantidad * Precio) - SubTotalDescuento
                    CType(grdetalle.DataSource, DataTable).Rows(Posicion).Item("tbdesc") = Descuento
                    'grdetalle.SetValue("tbdesc", Descuento)
                    CType(grdetalle.DataSource, DataTable).Rows(Posicion).Item("tbtotdesc") = (CType(grdetalle.DataSource, DataTable).Rows(Posicion).Item("tbpbas") * CType(grdetalle.DataSource, DataTable).Rows(Posicion).Item("tbcmin")) - Descuento

                    ' grdetalle.SetValue("tbtotdesc", ((CType(grdetalle.DataSource, DataTable).Rows(Posicion).Item("tbpbas") * CType(grdetalle.DataSource, DataTable).Rows(Posicion).Item("tbcmin")) - Descuento))
                    Return

                Else

                    CType(grdetalle.DataSource, DataTable).Rows(Posicion).Item("tbdesc") = 0
                    CType(grdetalle.DataSource, DataTable).Rows(Posicion).Item("tbtotdesc") = (CType(grdetalle.DataSource, DataTable).Rows(Posicion).Item("tbpbas") * CType(grdetalle.DataSource, DataTable).Rows(Posicion).Item("tbcmin"))


                End If

            Next
        Else
            'Dim tabla As DataTable = CType(grdetalle.DataSource, DataTable).Select("estado=0", "").CopyToDataTable
            'grdetalle.DataSource = tabla
            For i = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1
                'Cálculo de descuentos por familia
                Dim familia = CType(grdetalle.DataSource, DataTable).Rows(i).Item("tbfamilia")
                Dim cantnormal = CType(grdetalle.DataSource, DataTable).Rows(i).Item("tbcmin")
                Dim Estado = CType(grdetalle.DataSource, DataTable).Rows(i).Item("estado")
                Dim CodProd = CType(grdetalle.DataSource, DataTable).Rows(i).Item("tbty5prod")
                total1 = CType(grdetalle.DataSource, DataTable).Rows(i).Item("tbptot")

                If Estado = 0 And familia <> 1 Then
                    'Recorre el grid para hacer la suma de las cantidades por familia
                    For Each flia In grdetalle.GetRows
                        If familia = flia.Cells("tbfamilia").Value Then
                            cantf += flia.Cells("tbcmin").Value
                        End If
                    Next

                    'Consulta la tabla de descuentos para ver cual aplicará segun la cantidad ingresada
                    fila = dtDescuentos.Select("ProductoId=" + CodProd.ToString + "", "")

                    For Each preciodesc As DataRow In fila
                        If cantf >= preciodesc.Item("CantidadInicial") And cantf <= preciodesc.Item("CantidadFinal") Then
                            preciod = preciodesc.Item("Precio")
                            total2 = cantnormal * preciod
                        End If
                    Next
                    If total2 > 0 Then
                        descuentof = total1 - total2
                    Else
                        descuentof = 0
                    End If
                    CType(grdetalle.DataSource, DataTable).Rows(i).Item("tbdesc") = descuentof
                    CType(grdetalle.DataSource, DataTable).Rows(i).Item("tbtotdesc") = total1 - descuentof
                    cantf = 0
                    total2 = 0
                    descuentof = 0
                Else

                End If
            Next
        End If


    End Sub

    Public Sub CalcularDescuentosCuandoSeEliminaProductoFamilia(Posicion As Integer)
        Dim preciod, total1, total2, descuentof, cantf As Double

        Dim fila As DataRow()

        'Cálculo de descuentos si es sin familia
        If CType(grdetalle.DataSource, DataTable).Rows(Posicion).Item("tbfamilia") <> 1 Then

            For i = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1
                'Cálculo de descuentos por familia
                Dim familia = CType(grdetalle.DataSource, DataTable).Rows(i).Item("tbfamilia")
                Dim cantnormal = CType(grdetalle.DataSource, DataTable).Rows(i).Item("tbcmin")
                Dim Estado = CType(grdetalle.DataSource, DataTable).Rows(i).Item("estado")
                Dim CodProd = CType(grdetalle.DataSource, DataTable).Rows(i).Item("tbty5prod")
                total1 = CType(grdetalle.DataSource, DataTable).Rows(i).Item("tbptot")

                If Estado = 0 And familia <> 1 Then
                    'Recorre el grid para hacer la suma de las cantidades por familia
                    For Each flia In grdetalle.GetRows
                        If familia = flia.Cells("tbfamilia").Value Then
                            cantf += flia.Cells("tbcmin").Value
                        End If
                    Next

                    fila = dtDescuentos.Select("ProductoId=" + CodProd.ToString + "", "")
                    'Consulta la tabla de descuentos para ver cual aplicará segun la cantidad ingresada
                    For Each preciodesc As DataRow In fila
                        If cantf >= preciodesc.Item("CantidadInicial") And cantf <= preciodesc.Item("CantidadFinal") Then
                            preciod = preciodesc.Item("Precio")
                            total2 = cantnormal * preciod
                        End If
                    Next
                    If total2 > 0 Then
                        descuentof = total1 - total2
                    Else
                        descuentof = 0
                    End If
                    CType(grdetalle.DataSource, DataTable).Rows(i).Item("tbdesc") = descuentof
                    CType(grdetalle.DataSource, DataTable).Rows(i).Item("tbtotdesc") = total1 - descuentof
                    cantf = 0
                    total2 = 0
                    descuentof = 0
                Else

                End If
            Next
        End If


    End Sub

    Private Sub AsignarClienteEmpleado()
        Dim _tabla11 As DataTable = L_fnListarClientesUsuario(gi_userNumi)
        If _tabla11.Rows.Count > 0 Then
            lbCliente.Text = _tabla11.Rows(0).Item("yddesc")
            _CodCliente = _tabla11.Rows(0).Item("ydnumi") 'Codigo

            _CodEmpleado = _tabla11.Rows(0).Item("ydnumivend") 'Codigo
        Else
            Dim dt As DataTable
            dt = L_fnListarClientes()
            If dt.Rows.Count > 0 Then
                Dim fila As DataRow() = dt.Select("ydnumi =MIN(ydnumi)")
                lbCliente.Text = fila(0).ItemArray(3)
                _CodCliente = fila(0).ItemArray(0)

                _CodEmpleado = fila(0).ItemArray(8)
            End If
        End If
    End Sub

    Public Sub _prValidarLote()
        Dim dt As DataTable = L_fnPorcUtilidad()
        If (dt.Rows.Count > 0) Then
            Dim lot As Integer = dt.Rows(0).Item("VerLote")
            OcultarFact = dt.Rows(0).Item("VerFactManual")
            If (lot = 1) Then
                G_Lote = True
            Else
                G_Lote = False
            End If

        End If
    End Sub





    Private Sub _Limpiar()
        AsignarClienteEmpleado()
        tbProducto.Clear()
        tbTotal.Value = 0
        lbFecha.Text = Now.Date.ToString("dd/MM/yyyy")
        lbCliente.Text = "S/N"
        lbNit.Text = "0"


        _prCargarDetalleVenta(-1)

        'txtCambio1.Value = 0
        'txtMontoPagado1.Value = 0



        If (GPanelProductos.Visible = True) Then
            GPanelProductos.Visible = False

        End If
        With grdetalle.RootTable.Columns("img")
            .Width = 55
            .Caption = "Eliminar"
            .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
            .Visible = False
        End With
        _prAddDetalleVenta()

        'tbCliente.Focus()


        FilaSelectLote = Nothing

        ' tbCliente.Focus()
        Table_Producto = Nothing
        tbDescripcion.Clear()
        tbPrecio.Text = ""



        tbDescuento.Value = 0
        dtDescuentos = L_fnListarDescuentosTodos()

    End Sub


    Private Sub _prCargarDetalleVenta(_numi As String)
        Dim dt As New DataTable
        dt = L_fnDetalleVenta(_numi)
        grdetalle.DataSource = dt
        grdetalle.RetrieveStructure()

        ' a.tbnumi ,a.tbtv1numi ,a.tbty5prod ,b.yfcdprod1 as producto,a.tbest ,a.tbcmin ,a.tbumin ,Umin .ycdes3 as unidad,a.tbpbas ,a.tbptot,a.tbdesc ,a.tbobs ,
        'a.tbfact ,a.tbhact ,a.tbuact

        With grdetalle.RootTable.Columns("tbnumi")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = False
        End With

        With grdetalle.RootTable.Columns("tbtv1numi")
            .Width = 90
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("tbty5prod")
            .Width = 90
            .Visible = False
        End With
        'If _codeBar = 2 Then
        '    With grdetalle.RootTable.Columns("yfcbarra")
        '        .Caption = "Cod.Barra"
        '        .Width = 100
        '        .Visible = True

        '    End With
        'Else
        '    With grdetalle.RootTable.Columns("yfcbarra")
        '        .Caption = "Cod.Barra"
        '        .Width = 100
        '        .Visible = False
        '    End With
        'End If

        With grdetalle.RootTable.Columns("Codigo")
            .Caption = "Código".ToUpper
            .Width = 100
            .Visible = False
        End With

        With grdetalle.RootTable.Columns("yfcbarra")
            .Caption = "C.B.".ToUpper
            .Width = 40
            .Visible = False
        End With

        With grdetalle.RootTable.Columns("producto")
            .Caption = "Productos"
            .Width = 320
            .MaxLines = 3
            .WordWrap = True
            .Visible = True
        End With
        With grdetalle.RootTable.Columns("tbest")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With

        With grdetalle.RootTable.Columns("tbcmin")
            .Width = 70
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Cantidad"
        End With
        With grdetalle.RootTable.Columns("tbumin")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("unidad")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .Caption = "UN.".ToUpper
        End With
        With grdetalle.RootTable.Columns("tbpbas")
            .Width = 75
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Precio U."
        End With
        With grdetalle.RootTable.Columns("tbptot")
            .Width = 85
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "SubTotal"
        End With
        With grdetalle.RootTable.Columns("tbporc")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .FormatString = "0.00"
            .Caption = "P.Desc(%)".ToUpper
        End With
        With grdetalle.RootTable.Columns("tbdesc")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Desct."
        End With
        With grdetalle.RootTable.Columns("tbtotdesc")
            .Width = 70
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Total".ToUpper
        End With
        With grdetalle.RootTable.Columns("tbobs")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("tbpcos")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("tbptot2")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("tbfact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("tbhact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("tbuact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("estado")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("img")
            .Width = 30
            .Caption = "Eliminar"
            .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
            .Visible = False
        End With
        If (G_Lote = True) Then
            With grdetalle.RootTable.Columns("tblote")
                .Width = 120
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = False
                .Caption = "LOTE"
            End With
            With grdetalle.RootTable.Columns("tbfechaVenc")
                .Width = 120
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = False
                .Caption = "FECHA VENC."
                .FormatString = "yyyy/MM/dd"
            End With

        Else
            With grdetalle.RootTable.Columns("tblote")
                .Width = 120
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = False
                .Caption = "LOTE"
            End With
            With grdetalle.RootTable.Columns("tbfechaVenc")
                .Width = 120
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = False
                .Caption = "FECHA VENC."
                .FormatString = "yyyy/MM/dd"
            End With
        End If
        With grdetalle.RootTable.Columns("stock")
            .Width = 120
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("tbfamilia")
            .Width = 120
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("tbProveedorId")
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("ygcodsin")
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("ygcodu")
            .Visible = False
        End With
        With grdetalle
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
        End With

    End Sub


    Public Sub _prGuardar()
        If _ValidarCampos() = False Then
            Exit Sub
        End If
        Dim Succes As Integer = Emisor(tokenObtenido)
        If Succes = 200 Then
            _GuardarNuevo()
        End If

    End Sub
    Public Sub actualizarSaldoSinLote(ByRef dt As DataTable)
        'b.yfcdprod1 ,a.iclot ,a.icfven  ,a.iccven 

        '      a.tbnumi ,a.tbtv1numi ,a.tbty5prod ,b.yfcdprod1 as producto,a.tbest ,a.tbcmin ,a.tbumin ,Umin .ycdes3 as unidad,a.tbpbas ,a.tbptot ,a.tbobs ,
        'a.tbpcos,a.tblote ,a.tbfechaVenc , a.tbptot2, a.tbfact ,a.tbhact ,a.tbuact,1 as estado,Cast(null as Image) as img,
        'Cast (0 as decimal (18,2)) as stock
        Dim _detalle As DataTable = CType(grdetalle.DataSource, DataTable)

        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
            Dim sum As Integer = 0
            Dim codProducto As Integer = dt.Rows(i).Item("yfnumi")
            For j As Integer = 0 To grdetalle.RowCount - 1 Step 1
                grdetalle.Row = j
                Dim estado As Integer = grdetalle.GetValue("estado")
                If (estado = 0) Then
                    If (codProducto = grdetalle.GetValue("tbty5prod")) Then
                        sum = sum + grdetalle.GetValue("tbcmin")
                    End If
                End If
            Next
            dt.Rows(i).Item("stock") = dt.Rows(i).Item("stock") - sum
        Next

    End Sub

    Private Sub _prCargarProductos(_cliente As String)

        Dim dtname As DataTable = L_fnNameLabel()
        Dim dt As New DataTable

        If (G_Lote = True) Then
            dt = L_fnListarProductos(Sucursal, _cliente)  ''1=Almacen
            'Table_Producto = dt.Copy
        Else
            dt = L_fnListarProductosSinLote(Sucursal, _cliente, CType(grdetalle.DataSource, DataTable))  ''1=Almacen
            'Table_Producto = dt.Copy
        End If



        ''  actualizarSaldoSinLote(dt)
        grProductos.DataSource = dt
        grProductos.RetrieveStructure()
        grProductos.AlternatingColors = True

        '      a.yfnumi ,a.yfcprod ,a.yfcdprod1,a.yfcdprod2 ,a.yfgr1,gr1.ycdes3 as grupo1,a.yfgr2
        ',gr2.ycdes3 as grupo2 ,a.yfgr3,gr3.ycdes3 as grupo3,a.yfgr4 ,gr4 .ycdes3 as grupo4,a.yfumin ,Umin .ycdes3 as UnidMin
        ' ,b.yhprecio 

        With grProductos.RootTable.Columns("yfnumi")
            .Width = 100
            .Caption = "Código"
            .Visible = False

        End With
        With grProductos.RootTable.Columns("yfcprod")
            .Width = 60
            .Caption = "Código"
            .Visible = True
        End With
        With grProductos.RootTable.Columns("yfcbarra")
            .Width = 80
            .Caption = "Cod. Barra"
            .Visible = gb_CodigoBarra
        End With
        With grProductos.RootTable.Columns("yfcdprod1")
            .Width = 340
            .Visible = True
            .Caption = "Descripción"
        End With
        With grProductos.RootTable.Columns("yfvsup")
            .Width = 90
            .Visible = True
            .Caption = "Conversión"
            .FormatString = "0.00"
        End With
        With grProductos.RootTable.Columns("yfcdprod2")
            .Width = 150
            .Visible = False
            .Caption = "Descripcion Corta"
        End With
        With grProductos.RootTable.Columns("yfgr1")
            .Width = 160
            .Visible = False
        End With
        If (dtname.Rows.Count > 0) Then

            With grProductos.RootTable.Columns("grupo1")
                .Width = 120
                .Caption = dtname.Rows(0).Item("Grupo 1").ToString
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = False
            End With
            With grProductos.RootTable.Columns("grupo2")
                .Width = 120
                .Caption = dtname.Rows(0).Item("Grupo 2").ToString
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = False
            End With

            With grProductos.RootTable.Columns("grupo3")
                .Width = 120
                .Caption = dtname.Rows(0).Item("Grupo 3").ToString
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = False
            End With
            With grProductos.RootTable.Columns("grupo4")
                .Width = 120
                .Caption = dtname.Rows(0).Item("Grupo 4").ToString
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = False
            End With
        Else
            With grProductos.RootTable.Columns("grupo1")
                .Width = 120
                .Caption = "Grupo 1"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = False
            End With
            With grProductos.RootTable.Columns("grupo2")
                .Width = 120
                .Caption = "Grupo 2"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = False
            End With
            With grProductos.RootTable.Columns("grupo3")
                .Width = 120
                .Caption = "Grupo 3"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = False
            End With
            With grProductos.RootTable.Columns("grupo4")
                .Width = 120
                .Caption = "Grupo 4"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = False
            End With
        End If


        With grProductos.RootTable.Columns("yfgr2")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With

        With grProductos.RootTable.Columns("yfgr3")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grProductos.RootTable.Columns("validacion")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grProductos.RootTable.Columns("yfgr4")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grProductos.RootTable.Columns("yfgr5")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grProductos.RootTable.Columns("Grupo5")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grProductos.RootTable.Columns("yfumin")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grProductos.RootTable.Columns("UnidMin")
            .Width = 120
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
            .Caption = "Unidad Min."
        End With
        With grProductos.RootTable.Columns("yhprecio")
            .Width = 90
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Caption = "Precio"
            .FormatString = "0.00"
        End With
        With grProductos.RootTable.Columns("pcos")
            .Width = 120
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
            .Caption = "Precio Costo"
            .FormatString = "0.00"
        End With
        With grProductos.RootTable.Columns("stock")
            .Width = 100
            .FormatString = "0.00"
            .Visible = True
            .Caption = "Stock"
        End With
        With grProductos.RootTable.Columns("DescuentoId")
            .Visible = False
        End With
        With grProductos.RootTable.Columns("grupoDesc")
            .Width = 100
            .Visible = False
            .Caption = "Grupo Desc."
        End With
        With grProductos.RootTable.Columns("ygcodsin")
            .Visible = False
        End With
        With grProductos.RootTable.Columns("ygcodu")
            .Visible = False
        End With

        With grProductos
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
        End With
        _prAplicarCondiccionJanusSinLote()
    End Sub
    Public Sub _prAplicarCondiccionJanusSinLote()
        'Dim fc As GridEXFormatCondition
        'fc = New GridEXFormatCondition(grProductos.RootTable.Columns("stock"), ConditionOperator.Between, -9998 And 0)
        ''fc.FormatStyle.FontBold = TriState.True
        'fc.FormatStyle.ForeColor = Color.Red    'Color.Tan
        'grProductos.RootTable.FormatConditions.Add(fc)
        Dim fr As GridEXFormatCondition
        fr = New GridEXFormatCondition(grProductos.RootTable.Columns("validacion"), ConditionOperator.Equal, 1)
        fr.FormatStyle.ForeColor = Color.Red
        grProductos.RootTable.FormatConditions.Add(fr)
    End Sub


    Public Sub actualizarSaldo(ByRef dt As DataTable, CodProducto As Integer)
        'b.yfcdprod1 ,a.iclot ,a.icfven  ,a.iccven 

        '      a.tbnumi ,a.tbtv1numi ,a.tbty5prod ,b.yfcdprod1 as producto,a.tbest ,a.tbcmin ,a.tbumin ,Umin .ycdes3 as unidad,a.tbpbas ,a.tbptot ,a.tbobs ,
        'a.tbpcos,a.tblote ,a.tbfechaVenc , a.tbptot2, a.tbfact ,a.tbhact ,a.tbuact,1 as estado,Cast(null as Image) as img,
        'Cast (0 as decimal (18,2)) as stock
        Dim _detalle As DataTable = CType(grdetalle.DataSource, DataTable)

        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
            Dim lote As String = dt.Rows(i).Item("iclot")
            Dim FechaVenc As Date = dt.Rows(i).Item("icfven")
            Dim sum As Integer = 0
            For j As Integer = 0 To _detalle.Rows.Count - 1
                Dim estado As Integer = _detalle.Rows(j).Item("estado")
                If (estado = 0) Then
                    If (lote = _detalle.Rows(j).Item("tblote") And
                        FechaVenc = _detalle.Rows(j).Item("tbfechaVenc") And CodProducto = _detalle.Rows(j).Item("tbty5prod")) Then
                        sum = sum + _detalle.Rows(j).Item("tbcmin")
                    End If
                End If
            Next
            dt.Rows(i).Item("iccven") = dt.Rows(i).Item("iccven") - sum
        Next

    End Sub

    Private Sub _prCargarLotesDeProductos(CodProducto As Integer, nameProducto As String)
        If (Sucursal < 0) Then
            Return
        End If
        Dim dt As New DataTable
        GPanelProductos.Text = nameProducto
        dt = L_fnListarLotesPorProductoVenta(Sucursal, CodProducto)  ''1=Almacen
        actualizarSaldo(dt, CodProducto)
        grProductos.DataSource = dt
        grProductos.RetrieveStructure()
        grProductos.AlternatingColors = True
        With grProductos.RootTable.Columns("yfcdprod1")
            .Width = 150
            .Visible = False

        End With
        'b.yfcdprod1 ,a.iclot ,a.icfven  ,a.iccven 
        With grProductos.RootTable.Columns("iclot")
            .Width = 150
            .Caption = "LOTE"
            .Visible = True

        End With
        With grProductos.RootTable.Columns("icfven")
            .Width = 160
            .Caption = "FECHA VENCIMIENTO"
            .FormatString = "yyyy/MM/dd"
            .Visible = True

        End With

        With grProductos.RootTable.Columns("iccven")
            .Width = 150
            .Visible = True
            .Caption = "Stock"
            .FormatString = "0.00"
            .AggregateFunction = AggregateFunction.Sum
        End With
        With grProductos.RootTable.Columns("stockMinimo")
            .Width = 150
            .Visible = False

        End With
        With grProductos.RootTable.Columns("fechaVencimiento")
            .Width = 150
            .Visible = False

        End With

        With grProductos
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla
            .TotalRow = InheritableBoolean.True
            .TotalRowFormatStyle.BackColor = Color.Gold
            .TotalRowPosition = TotalRowPosition.BottomFixed
            .VisualStyle = VisualStyle.Office2007
        End With
        _prAplicarCondiccionJanusLote()

    End Sub
    Public Sub _prAplicarCondiccionJanusLote()

        Dim fc2 As GridEXFormatCondition
        fc2 = New GridEXFormatCondition(grProductos.RootTable.Columns("stockMinimo"), ConditionOperator.Equal, 1)
        fc2.FormatStyle.BackColor = Color.Red
        fc2.FormatStyle.FontBold = TriState.True
        fc2.FormatStyle.ForeColor = Color.White
        grProductos.RootTable.FormatConditions.Add(fc2)

        Dim fc As GridEXFormatCondition
        fc = New GridEXFormatCondition(grProductos.RootTable.Columns("fechaVencimiento"), ConditionOperator.Equal, 1)
        fc.FormatStyle.BackColor = Color.Gold
        fc.FormatStyle.FontBold = TriState.True
        fc.FormatStyle.ForeColor = Color.White
        grProductos.RootTable.FormatConditions.Add(fc)


    End Sub
    Private Sub _prAddDetalleVenta()
        '   a.tbnumi ,a.tbtv1numi ,a.tbty5prod ,b.yfcdprod1 as producto,a.tbest ,a.tbcmin ,a.tbumin ,Umin .ycdes3 as unidad,a.tbpbas ,a.tbptot ,a.tbobs ,
        'a.tbpcos,a.tblote ,a.tbfechaVenc , a.tbptot2, a.tbfact ,a.tbhact ,a.tbuact,1 as estado,Cast(null as Image) as img
        Dim Bin As New MemoryStream
        Dim img As New Bitmap(My.Resources.delete, 28, 28)
        img.Save(Bin, Imaging.ImageFormat.Png)
        CType(grdetalle.DataSource, DataTable).Rows.Add(_fnSiguienteNumi() + 1, 0, 0, "", 0, "", 0, 0, 0, "", 0, 0, 0, 0, 0, "", 0, "20500101", CDate("2050/01/01"), 0, Now.Date, "", "", 0, Bin.GetBuffer, 0, 0)
    End Sub

    Public Function _fnSiguienteNumi()
        Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)
        Dim rows() As DataRow = dt.Select("tbnumi=MAX(tbnumi)")
        If (rows.Count > 0) Then
            Return rows(rows.Count - 1).Item("tbnumi")
        End If
        Return 1
    End Function

    Private Sub _HabilitarProductos()
        GPanelProductos.Visible = True

        _prCargarProductos(Str(_CodCliente))
        grProductos.Focus()
        grProductos.MoveTo(grProductos.FilterRow)
        grProductos.Col = 2
    End Sub
    Private Sub _HabilitarFocoDetalle(fila As Integer)
        _prCargarProductos(Str(_CodCliente))
        grdetalle.Focus()
        grdetalle.Row = fila
        grdetalle.Col = grdetalle.RootTable.Columns("tbcmin").Index
    End Sub
    Private Sub _DesHabilitarProductos()
        GPanelProductos.Visible = False

        'tbProducto.Focus()
        grdetalle.Select()
        grdetalle.Col = grdetalle.RootTable.Columns("tbcmin").Index
        grdetalle.Row = grdetalle.RowCount - 1

    End Sub
    Public Sub _fnObtenerFilaDetalle(ByRef pos As Integer, numi As Integer)
        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _numi As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("tbnumi")
            If (_numi = numi) Then
                pos = i
                Return
            End If
        Next

    End Sub

    Public Sub _fnObtenerFilaDetalleProducto(ByRef pos As Integer, numi As Integer)
        For i As Integer = 0 To CType(grProductos.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _numi As Integer = CType(grProductos.DataSource, DataTable).Rows(i).Item("yfnumi")
            If (_numi = numi) Then
                pos = i
                Return
            End If
        Next

    End Sub

    Public Function _fnExisteProducto(idprod As Integer, ByRef PosData As Integer) As Boolean
        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _idprod As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("tbty5prod")
            Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("estado")
            If (_idprod = idprod And estado >= 0) Then
                PosData = i
                Return True
            End If
        Next
        PosData = -1
        Return False
    End Function

    Public Function _fnExisteProductoConLote(idprod As Integer, lote As String, fechaVenci As Date, ByRef Pos As Integer) As Boolean
        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _idprod As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("tbty5prod")
            Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("estado")
            '          a.tbnumi ,a.tbtv1numi ,a.tbty5prod ,b.yfcdprod1 as producto,a.tbest ,a.tbcmin ,a.tbumin ,Umin .ycdes3 as unidad,a.tbpbas ,a.tbptot ,a.tbobs ,
            'a.tbpcos,a.tblote ,a.tbfechaVenc , a.tbptot2, a.tbfact ,a.tbhact ,a.tbuact,1 as estado,Cast(null as Image) as img,
            'Cast (0 as decimal (18,2)) as stock
            Dim _LoteDetalle As String = CType(grdetalle.DataSource, DataTable).Rows(i).Item("tblote")
            Dim _FechaVencDetalle As Date = CType(grdetalle.DataSource, DataTable).Rows(i).Item("tbfechaVenc")
            If (_idprod = idprod And estado >= 0 And lote = _LoteDetalle And fechaVenci = _FechaVencDetalle) Then
                Pos = i
                Return True
            End If
        Next
        Pos = -1
        Return False
    End Function
    Public Sub P_PonerTotal(rowIndex As Integer)
        If (rowIndex < grdetalle.RowCount) Then
            'grdetalle.UpdateData()
            Dim lin As Integer = grdetalle.GetValue("tbnumi")
            Dim pos As Integer = -1
            _fnObtenerFilaDetalle(pos, lin)
            Dim cant As Double = grdetalle.GetValue("tbcmin")
            'Dim cantidad = Format(cant,"0.00")
            Dim uni As Double = grdetalle.GetValue("tbpbas")
            Dim cos As Double = grdetalle.GetValue("tbpcos")
            Dim MontoDesc As Double = grdetalle.GetValue("tbdesc")
            Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)
            If (pos >= 0) Then
                Dim TotalUnitario As Double = cant * uni
                Dim TotalCosto As Double = cant * cos
                'grDetalle.SetValue("lcmdes", montodesc)

                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot") = TotalUnitario
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = TotalUnitario - MontoDesc

                grdetalle.SetValue("tbptot", TotalUnitario)
                grdetalle.SetValue("tbtotdesc", TotalUnitario - MontoDesc)

                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot2") = TotalCosto
                grdetalle.SetValue("tbptot2", TotalCosto)

                Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado")
                If (estado = 1) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = 2
                End If
            End If
            _prCalcularPrecioTotal()
        End If
    End Sub

    Public Sub _prCalcularPrecioTotal()


        Dim TotalDescuento As Double = 0

        Dim Descuento As Double = 0
        Dim TotalCosto As Double = 0
        Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)
        For i As Integer = 0 To dt.Rows.Count - 1 Step 1

            If (dt.Rows(i).Item("estado") >= 0) Then
                TotalDescuento = TotalDescuento + dt.Rows(i).Item("tbtotdesc")
                TotalCosto = TotalDescuento + dt.Rows(i).Item("tbptot2")
                Descuento += dt.Rows(i).Item("tbdesc")
            End If
        Next


        'grdetalle.UpdateData()
        Dim montoDo As Decimal
        Dim montodesc As Double = 0
        Dim pordesc As Double = ((montodesc * 100) / TotalDescuento)
        tbTotal.Value = TotalDescuento
        tbDescuento.Value = Descuento


    End Sub
    Public Sub _prEliminarFila()
        If (grdetalle.Row >= 0) Then
            If (grdetalle.RowCount >= 2) Then
                Dim estado As Integer = grdetalle.GetValue("estado")
                Dim pos As Integer = -1
                Dim lin As Integer = grdetalle.GetValue("tbnumi")
                _fnObtenerFilaDetalle(pos, lin)
                If (estado = 0) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = -2
                End If
                If (estado = 1) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = -1
                End If

                'grdetalle.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grdetalle.RootTable.Columns("estado"), Janus.Windows.GridEX.ConditionOperator.GreaterThanOrEqualTo, -3))


                grdetalle.Select()
                grdetalle.UpdateData()
                grdetalle.Col = grdetalle.RootTable.Columns("yfcbarra").Index
                grdetalle.Row = grdetalle.RowCount - 1
                grdetalle.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grdetalle.RootTable.Columns("estado"), Janus.Windows.GridEX.ConditionOperator.GreaterThanOrEqualTo, 0))

                If CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbfamilia") <> 1 Then
                    CalcularDescuentosCuandoSeEliminaProductoFamilia(pos)
                End If

                _prCalcularPrecioTotal()
            End If
        End If
        'grdetalle.Refetch()
        'grdetalle.Refresh()

    End Sub

    Public Sub _prEliminarFilaDesdeCantidad(filaEliminar As DataRow())
        Try
            If (grdetalle.RowCount >= 2) Then
                'Dim estado As Integer = grdetalle.GetValue("estado")
                Dim estado As Integer = filaEliminar(0).Item("estado")
                Dim pos As Integer = -1
                'Dim lin As Integer = grdetalle.GetValue("tbnumi")
                Dim lin As Integer = filaEliminar(0).Item("tbnumi")

                _fnObtenerFilaDetalle(pos, lin)
                If (estado = 0) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = -2
                End If
                If (estado = 1) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = -1
                End If


                grdetalle.Select()
                grdetalle.UpdateData()
                grdetalle.Col = grdetalle.RootTable.Columns("yfcbarra").Index
                grdetalle.Row = grdetalle.RowCount - 1
                grdetalle.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grdetalle.RootTable.Columns("estado"), Janus.Windows.GridEX.ConditionOperator.GreaterThanOrEqualTo, 0))
                _prCalcularPrecioTotal()
            Else
                ToastNotification.Show(Me, "No se puede eliminar si solo existe un producto, primero agregue otro producto y luego elimine el que desee".ToUpper, My.Resources.WARNING, 4500, eToastGlowColor.Red, eToastPosition.TopCenter)
                Exit Sub
            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub
    Public Function _ValidarCampos() As Boolean
        Try

            ''Validar datos de factura
            'If (lbNit.Text = String.Empty) Then
            '    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            '    ToastNotification.Show(Me, "Por Favor ponga el nit del cliente.".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            '    lbNit.Focus()
            '    Return False
            'End If

            'If (lbCliente.Text = String.Empty) Then
            '    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            '    ToastNotification.Show(Me, "Por Favor ponga la razon social del cliente.".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            '    lbCliente.Focus()
            '    Return False
            'End If

            If (grdetalle.RowCount = 1) Then
                grdetalle.Row = grdetalle.RowCount - 1
                If (grdetalle.GetValue("tbty5prod") = 0) Then
                    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                    ToastNotification.Show(Me, "Por Favor Seleccione  un detalle de producto".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.TopCenter)
                    Return False
                End If

            End If

            ''Validación para controlar caducidad de Dosificacion
            'If lbNit.Text <> String.Empty Then
            '    Dim fecha As String = Now.Date
            '    Dim dtDosificacion As DataSet = L_DosificacionCajas("1", "1", fecha, gs_NroCaja)
            '    If dtDosificacion.Tables(0).Rows.Count = 0 Then
            '        'dtDosificacion.Tables.Cast(Of DataTable)().Any(Function(x) x.DefaultView.Count = 0)
            '        Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            '        ToastNotification.Show(Me, "La Dosificación para las facturas ya caducó, ingrese nueva dosificación".ToUpper, img, 3500, eToastGlowColor.Red, eToastPosition.TopCenter)
            '        Return False
            '    End If
            'End If


            Return True
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
            Return False
        End Try

    End Function


    Public Function rearmarDetalle() As DataTable
        Dim dt, dtDetalle, dtSaldos As DataTable
        Dim cantidadRepetido, contar, IdAux As Integer
        Dim ResultadoInventario = False

        dt = CType(grdetalle.DataSource, DataTable)
        'Ordena el detalle por codigo importante
        dt.DefaultView.Sort = "tbty5prod ASC"
        dt = dt.DefaultView.ToTable
        dtDetalle = dt.Copy
        dtDetalle.Clear()
        contar = 0
        Try
            For i As Integer = 0 To dt.Rows.Count - 1 Step 1
                Dim codProducto As Integer = dt.Rows(i).Item("tbty5prod")
                dt.DefaultView.RowFilter = "tbty5prod =  '" + codProducto.ToString() + "'"
                cantidadRepetido = dt.DefaultView.Count()
                If IdAux <> codProducto Then
                    contar = 1
                Else
                    contar += 1
                End If
                IdAux = codProducto

                'Evita llamar a saldo cada iteracion
                If contar = 1 Then
                    dtSaldos = L_fnObteniendoSaldosTI001(codProducto, 1)
                    dtSaldos.DefaultView.Sort = "icfven ASC"
                    dtSaldos = dtSaldos.DefaultView.ToTable
                End If
                'dtSaldos.DefaultView.RowFilter = "iccven >  '" + 0.ToString() + "'"
                'dtSaldos = dtSaldos.DefaultView.ToTable
                Dim cantidad As Double = dt.Rows(i).Item("tbcmin")
                Dim saldo As Double = cantidad
                Dim estado As Integer = dt.Rows(i).Item("estado")
                Dim k As Integer = 0
                If (estado >= 0) Then
                    If (dtSaldos.Rows.Count <= 0) Then
                        dtDetalle.ImportRow(dt.Rows(i))
                    Else
                        While (k <= dtSaldos.Rows.Count - 1 And saldo > 0)

                            Dim inventario As Double = dtSaldos.Rows(k).Item("iccven")
                            If (inventario >= saldo) Then
                                dtDetalle.ImportRow(dt.Rows(i))
                                Dim pos As Integer = dtDetalle.Rows.Count - 1

                                Dim precio As Double = dtDetalle.Rows(pos).Item("tbpbas")
                                Dim total As Decimal = CStr(Format(precio * saldo, "####0.00"))

                                dtDetalle.Rows(pos).Item("tbptot") = total

                                'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = total
                                'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbcmin") = saldo
                                dtDetalle.Rows(pos).Item("tbcmin") = saldo

                                Dim precioCosto As Double = dtDetalle.Rows(pos).Item("tbpcos")
                                dtDetalle.Rows(pos).Item("tbptot2") = precioCosto * saldo
                                dtDetalle.Rows(pos).Item("tblote") = dtSaldos.Rows(k).Item("iclot")
                                dtDetalle.Rows(pos).Item("tbfechaVenc") = dtSaldos.Rows(k).Item("icfven")
                                dtSaldos.Rows(k).Item("iccven") = inventario - saldo
                                saldo = 0

                            Else
                                'Cuando el Invetanrio es menor
                                If (k <= dtSaldos.Rows.Count - 1 And inventario > 0) Then

                                    dtDetalle.ImportRow(dt.Rows(i))
                                    Dim pos As Integer = dtDetalle.Rows.Count - 1

                                    Dim precio As Double = dtDetalle.Rows(pos).Item("tbpbas")
                                    Dim total As Decimal = CStr(Format(precio * saldo, "####0.00"))
                                    dtDetalle.Rows(pos).Item("tbptot") = total

                                    'Dim descuento As Double = (dt.Rows(i).Item("tbtotdesc") / dt.Rows(i).Item("tbcmin"))

                                    'dtDetalle.Rows(pos).Item("tbtotdesc") = total - (inventario * descuento)
                                    'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = total
                                    'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbcmin") = inventario
                                    dtDetalle.Rows(pos).Item("tbcmin") = saldo

                                    Dim precioCosto As Double = dtDetalle.Rows(pos).Item("tbpcos")
                                    dtDetalle.Rows(pos).Item("tbptot2") = precioCosto * saldo
                                    dtDetalle.Rows(pos).Item("tblote") = dtSaldos.Rows(k).Item("iclot")
                                    dtDetalle.Rows(pos).Item("tbfechaVenc") = dtSaldos.Rows(k).Item("icfven")


                                    'saldo = saldo - inventario
                                    'Actualiza el inventario en la Tabla
                                    dtSaldos.Rows(k).Item("iccven") = dtSaldos.Rows(k).Item("iccven") - saldo
                                    saldo = 0
                                End If
                            End If
                            k += 1
                        End While
                        If saldo <> 0 Then
                            dtDetalle.ImportRow(dt.Rows(i))
                            Dim pos As Integer = dtDetalle.Rows.Count - 1
                            Dim precio As Double = dtDetalle.Rows(pos).Item("tbpbas")
                            Dim total As Decimal = CStr(Format(precio * saldo, "####0.00"))
                            dtDetalle.Rows(pos).Item("tbptot") = total
                            dtDetalle.Rows(pos).Item("tbcmin") = saldo
                            Dim precioCosto As Double = dtDetalle.Rows(pos).Item("tbpcos")
                            dtDetalle.Rows(pos).Item("tbptot2") = precioCosto * saldo
                            dtDetalle.Rows(pos).Item("tblote") = dtSaldos.Rows(k - 1).Item("iclot")
                            dtDetalle.Rows(pos).Item("tbfechaVenc") = dtSaldos.Rows(k - 1).Item("icfven")
                            saldo = 0
                        End If
                    End If
                End If
            Next
            Return dtDetalle
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
            Return dtDetalle
        End Try
    End Function
    Private Function _prExisteStockParaProducto() As Boolean
        Dim dtSaldos As DataTable
        Dim aux As Integer = 0
        Dim detalle = CType(grdetalle.DataSource, DataTable)
        Dim Lmensaje As List(Of String) = New List(Of String)
        detalle.DefaultView.RowFilter = "estado >= '" + 0.ToString() + "'"
        detalle = detalle.DefaultView.ToTable
        For Each fila As DataRow In detalle.Rows
            Dim idProducto = fila.Item("tbty5prod")
            If aux <> idProducto Then
                dtSaldos = L_fnObteniendoSaldosTI001(fila.Item("tbty5prod"), 1)
                Dim inventario = dtSaldos.Compute("SUM(iccven)", String.Empty)

                detalle.DefaultView.RowFilter = "tbty5prod = '" + fila.Item("tbty5prod").ToString() + "'"
                Dim productoRepeditos = detalle.DefaultView.ToTable
                Dim saldo = productoRepeditos.Compute("SUM(tbcmin)", String.Empty)
                'Dim saldo = fila.Item("tbcmin")
                If inventario < saldo Then
                    Dim mensaje As String = "No existe stock para el producto: " + fila.Item("producto") + " stock actual = " + inventario.ToString()
                    Lmensaje.Add(mensaje)
                    'Throw New Exception("No existe stock para el producto:" + fila.Item("producto") + " stock actual =" + inventario)
                End If
            End If
            aux = idProducto
            'dtSaldos.Select = "icfven ASC"
            'dtSaldos = dtSaldos.DefaultView.ToTable
        Next
        If Lmensaje.Count > 0 Then
            Dim mensaje = ""
            For Each item As String In Lmensaje
                mensaje = mensaje + "- " + item + vbCrLf
            Next
            MostrarMensajeError(mensaje)
            Return False
        End If
        Return True
    End Function
    Private Sub _prInsertarMontoNuevo(ByRef tabla As DataTable)
        tabla.Rows.Add(0, TotalBs, TotalSus, TotalTarjeta, TipoCambio, NroTarjeta, 0)
    End Sub

    'Private Sub _prInsertarMontoModificar(ByRef tabla As DataTable)
    '    tabla.Rows.Add(tbCodigo.Text, TotalBs, TotalSus, TotalTarjeta, cbCambioDolar.Text, 2)
    'End Sub
    Public Sub _GuardarNuevo()
        Try
            Dim numi As String = ""
            Dim tabla As DataTable = L_fnMostrarMontos2(0)
            Dim factura = gb_FacturaEmite
            _prInsertarMontoNuevo(tabla)
            ''Verifica si existe estock para los productos
            'If _prExisteStockParaProducto() Then
            'prCargando.Visible = True
            'prCargando.Show()
            Dim dtUsuario = L_BuscarPoUsuario(L_Usuario)
            Dim Vendedor As Integer = dtUsuario.Rows(0).Item("yd_numiVend")

            'Dim dtDetalle As DataTable = rearmarDetalle()
            Dim dtDetalle As DataTable = CType(grdetalle.DataSource, DataTable)
            Dim res As Boolean = L_fnGrabarVenta(numi, "", Now.Date.ToString("yyyy/MM/dd"), Vendedor, 1, Now.Date.ToString("yyyy/MM/dd"), _CodCliente, 1, "",
                                                 tbDescuento.Value, 0, Str(tbTotal.Value), dtDetalle, Sucursal, 0, tabla, gs_NroCaja, Programa,
                                                 lbNit.Text, lbCliente.Text, TbEmailS.Text, CbTDoc.Value, 0)
            If res Then
                'res = P_fnGrabarFacturarTFV001(numi)
                'Emite factura
                If (gb_FacturaEmite) Then
                    If lbNit.Text <> String.Empty Then
                        P_fnGenerarFactura(numi)
                        '_prImiprimirNotaVenta(numi)
                    Else
                        _prImiprimirNotaVenta(numi)
                    End If
                Else
                    _prImiprimirNotaVenta(numi)
                End If

                Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
                ToastNotification.Show(Me, "Código de Venta ".ToUpper + numi + " Grabado con Exito.".ToUpper,
                                          img, 2000,
                                          eToastGlowColor.Green,
                                          eToastPosition.TopCenter
                                          )

                System.Diagnostics.Process.Start(FactUrl)

                'If (prCargando.IsRunning) Then

                '    prCargando.Dispose()
                'End If

                _Limpiar()
                Table_Producto = Nothing

            Else
                Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                ToastNotification.Show(Me, "La Venta no pudo ser insertado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

            End If
            'If (prCargando.IsRunning) Then

            '    prCargando.Dispose()
            'End If




            'End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try


    End Sub
    Public Sub _prImiprimirNotaVenta(numi As String)
        Dim ef = New Efecto


        ef.tipo = 2
        ef.Context = "MENSAJE PRINCIPAL".ToUpper
        ef.Header = "¿desea imprimir la nota de venta?".ToUpper
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then
            P_GenerarReporte(numi)
        End If
    End Sub
    Public Sub _prImiprimirFacturaPreimpresa(numi As String)
        Dim ef = New Efecto


        ef.tipo = 2
        ef.Context = "MENSAJE PRINCIPAL".ToUpper
        ef.Header = "¿desea imprimir la factura Preimpresa?".ToUpper
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then
            P_GenerarReporteFactura(numi)
        End If
    End Sub

    Public Sub _prCargarIconELiminar()
        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim Bin As New MemoryStream
            Dim img As New Bitmap(My.Resources.delete, 28, 28)
            img.Save(Bin, Imaging.ImageFormat.Png)
            CType(grdetalle.DataSource, DataTable).Rows(i).Item("img") = Bin.GetBuffer

        Next
        grdetalle.RootTable.Columns("img").Visible = False
    End Sub

    Public Sub InsertarProductosSinLote()
        Dim pos As Integer = -1

        Dim PosDataExistente As Integer = -1
        Dim existe As Boolean = _fnExisteProducto(grProductos.GetValue("yfnumi"), PosDataExistente)
        If ((Not existe)) Then
            grdetalle.Row = grdetalle.RowCount - 1
            If (grdetalle.GetValue("tbty5prod") <> 0) Then
                _prAddDetalleVenta()
            End If
            grdetalle.Row = grdetalle.RowCount - 1

            _fnObtenerFilaDetalle(pos, grdetalle.GetValue("tbnumi"))
            If (pos >= 0) Then
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbty5prod") = grProductos.GetValue("yfnumi")
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("codigo") = grProductos.GetValue("yfcprod")
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("yfcbarra") = grProductos.GetValue("yfcbarra")
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("producto") = grProductos.GetValue("yfcdprod1")
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbumin") = grProductos.GetValue("yfumin")
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbfamilia") = grProductos.GetValue("yfgr4")
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("unidad") = grProductos.GetValue("UnidMin")
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpbas") = grProductos.GetValue("yhprecio")
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot") = grProductos.GetValue("yhprecio")
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = grProductos.GetValue("yhprecio")
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("ygcodsin") = grProductos.GetValue("ygcodsin")
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("ygcodu") = grProductos.GetValue("ygcodu")
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbcmin") = 1
                'If (gb_FacturaIncluirICE) Then
                '    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos") = grProductos.GetValue("pcos")
                'Else
                '    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos") = 0
                'End If
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos") = grProductos.GetValue("pcos")
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot2") = grProductos.GetValue("pcos")

                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("stock") = grProductos.GetValue("stock")
                _prCalcularPrecioTotal()
                _DesHabilitarProductos()
                tbDescripcion.Text = grProductos.GetValue("yfcdprod1")
                tbPrecio.Value = grProductos.GetValue("yhprecio")

                'tbProducto.Clear()
                tbProducto.Text = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("yfcbarra")
                txtNumi.Text = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbnumi")
                VentanaCantidad(False)
                tbProducto.Focus()
            End If

        Else


            If (existe And PosDataExistente >= 0) Then
                'Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                'ToastNotification.Show(Me, "El producto ya existe en el detalle".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                'Dim cantidad As Double = CType(grdetalle.DataSource, DataTable).Rows(PosDataExistente).Item("tbcmin") + 1
                Dim cantidad As Double = CType(grdetalle.DataSource, DataTable).Rows(PosDataExistente).Item("tbcmin")
                CType(grdetalle.DataSource, DataTable).Rows(PosDataExistente).Item("tbcmin") = cantidad
                CType(grdetalle.DataSource, DataTable).Rows(PosDataExistente).Item("tbptot") = CType(grdetalle.DataSource, DataTable).Rows(PosDataExistente).Item("tbpbas") * cantidad
                CType(grdetalle.DataSource, DataTable).Rows(PosDataExistente).Item("tbtotdesc") = CType(grdetalle.DataSource, DataTable).Rows(PosDataExistente).Item("tbpbas") * cantidad
                CType(grdetalle.DataSource, DataTable).Rows(PosDataExistente).Item("tbptot2") = CType(grdetalle.DataSource, DataTable).Rows(PosDataExistente).Item("tbpcos") * cantidad

                _prCalcularPrecioTotal()
                _DesHabilitarProductos()


                tbDescripcion.Text = CType(grdetalle.DataSource, DataTable).Rows(PosDataExistente).Item("producto")
                tbPrecio.Value = CType(grdetalle.DataSource, DataTable).Rows(PosDataExistente).Item("tbpbas")
                FilaSelectLote = Nothing
                'tbProducto.Clear()
                tbProducto.Text = CType(grdetalle.DataSource, DataTable).Rows(PosDataExistente).Item("yfcbarra")
                txtNumi.Text = CType(grdetalle.DataSource, DataTable).Rows(PosDataExistente).Item("tbnumi")
                VentanaCantidad(True)
                tbProducto.Focus()
            End If
        End If
    End Sub
    Public Sub InsertarProductosConLote()
        Dim pos As Integer = -1
        grdetalle.Row = grdetalle.RowCount - 1



        _fnObtenerFilaDetalleProducto(pos, grProductos.GetValue("yfnumi"))
        Dim posProducto As Integer = grProductos.Row
        FilaSelectLote = CType(grProductos.DataSource, DataTable).Rows(pos)


        If (grProductos.GetValue("stock") > 0) Then
            _prCargarLotesDeProductos(grProductos.GetValue("yfnumi"), grProductos.GetValue("yfcdprod1"))
        Else
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "El Producto: ".ToUpper + grProductos.GetValue("yfcdprod1") + " NO CUENTA CON STOCK DISPONIBLE", img, 5000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            FilaSelectLote = Nothing
        End If

    End Sub
    Private Function P_fnGenerarFactura(numi As String) As Boolean
        Dim res As Boolean = False
        res = P_fnGrabarFacturarTFV001(numi) ' Grabar en la TFV001
        If (res) Then
            If (P_fnValidarFactura()) Then
                'Validar para facturar
                'P_prImprimirFacturar(numi, True, True) '_Codigo de a tabla TV001
            Else
                'Volver todo al estada anterior
                ToastNotification.Show(Me, "No es posible facturar, vuelva a ingresar he intente nuevamente!!!".ToUpper,
                                       My.Resources.OK,
                                       5 * 1000,
                                       eToastGlowColor.Red,
                                       eToastPosition.MiddleCenter)
            End If

            If (Not lbNit.Text.Trim.Equals("0")) Then
                L_Grabar_Nit(lbNit.Text.Trim, lbCliente.Text.Trim, "", CbTDoc.Value, TbEmailS.Text)
            Else
                L_Grabar_Nit(lbNit.Text, "S/N", "", "", "")
            End If
        End If

        Return res
    End Function

    Private Function P_fnGrabarFacturarTFV001(numi As String) As Boolean
        Dim a As Double = CDbl(Convert.ToDouble(Str(tbTotal.Value)))
        'Dim b As Double = CDbl(IIf(IsDBNull(tbIce.Value), 0, tbIce.Value)) 'Ya esta calculado el 55% del ICE
        Dim b As Double = CDbl(0)
        Dim c As Double = CDbl("0")
        Dim d As Double = CDbl("0")
        Dim e As Double = a - b - c - d
        Dim f As Double = 0
        Dim g As Double = e - f
        Dim h As Double = g * (gi_IVA / 100)

        Dim res As Boolean = False
        Dim _Hora As String = Now.Hour.ToString("D2") + ":" + Now.Minute.ToString("D2")
        Dim Anhio As Integer = Now.Date.Year
        'Grabado de Cabesera Factura
        L_Grabar_Factura(numi,
                        Now.Date.ToString("yyyy/MM/dd"),
                        NroFact,
                        gb_cufSifac,
                        "1",
                        lbNit.Text.Trim,
                        IdNit,
                        lbCliente.Text,
                        "",
                        CStr(Format(a, "####0.00")),
                        CStr(Format(b, "####0.00")),
                        CStr(Format(c, "####0.00")),
                        CStr(Format(d, "####0.00")),
                        CStr(Format(e, "####0.00")),
                        CStr(Format(f, "####0.00")),
                        CStr(Format(g, "####0.00")),
                        CStr(Format(h, "####0.00")),
                        "",
                        Now.Date.ToString("yyyy/MM/dd"),
                        "''",
                        Sucursal,
                        numi,
                        _Hora,
                        QrUrl,
                        FactUrl,
                        SegundaLeyenda,
                        TerceraLeyenda,
                        Cudf,
                        gs_NroCaja,
                        Anhio)


        'Grabar Nuevo y Modificado en la BDDiconDinoEco en la tabla TPA001
        If ("" = String.Empty) Then
            L_Grabar_TPA001(numi, Now.Date.ToString("yyyy/MM/dd"), "2", _CodCliente, lbCliente.Text, "1",
                        "1", CStr(Format(g, "####0.00")), "1", "6.96", "0", "0")
        End If


        'Grabado de Detalle de Factura
        grProductos.Update()

        'Dim s As String = ""
        For Each fil As GridEXRow In grdetalle.GetRows
            If (Not fil.Cells("tbcmin").Value.ToString.Trim.Equals("") And
                Not fil.Cells("tbty5prod").Value.ToString.Trim.Equals("0")) Then
                's = fil.Cells("codP").Value
                's = fil.Cells("des").Value
                's = fil.Cells("can").Value
                's = fil.Cells("imp").Value
                L_Grabar_Factura_Detalle(numi.ToString,
                                        fil.Cells("tbty5prod").Value.ToString.Trim,
                                        fil.Cells("producto").Value.ToString.Trim,
                                        fil.Cells("tbcmin").Value.ToString.Trim,
                                        fil.Cells("tbpbas").Value.ToString.Trim,
                                        numi)
                res = True
            End If
        Next
        Return res
    End Function

    Private Function P_fnValidarFactura() As Boolean
        Return True
    End Function

    Private Sub P_prImprimirFacturar(numi As String, impFactura As Boolean, grabarPDF As Boolean)
        Dim _Fecha, _FechaAl As Date
        Dim _Ds, _Ds1, _Ds2, _Ds3 As New DataSet
        Dim _Autorizacion, _Nit, _Fechainv, _Total, _Key, _Cod_Control, _Hora,
            _Literal, _TotalDecimal, _TotalDecimal2 As String
        Dim I, _NumFac, _numidosif, _TotalCC As Integer
        Dim ice, _Desc, _TotalLi As Decimal
        Dim _VistaPrevia As Integer = 0


        _Desc = CDbl(0)
        If Not IsNothing(P_Global.Visualizador) Then
            P_Global.Visualizador.Close()
        End If

        '_Fecha = Now.Date '.ToString("dd/MM/yyyy")
        _Fecha = Now.Date
        _Hora = Now.Hour.ToString("D2") + ":" + Now.Minute.ToString("D2")
        '_Ds1 = L_Dosificacion("1", "1", _Fecha)
        _Ds1 = L_DosificacionCajas("1", "1", _Fecha, gs_NroCaja)

        _Ds = L_Reporte_Factura(numi, numi)
        _Autorizacion = _Ds1.Tables(0).Rows(0).Item("sbautoriz").ToString
        _NumFac = CInt(_Ds1.Tables(0).Rows(0).Item("sbnfac")) + 1
        _Nit = _Ds.Tables(0).Rows(0).Item("fvanitcli").ToString
        _Fechainv = Microsoft.VisualBasic.Right(_Fecha.ToShortDateString, 4) +
                    Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(_Fecha.ToShortDateString, 5), 2) +
                    Microsoft.VisualBasic.Left(_Fecha.ToShortDateString, 2)
        _Total = _Ds.Tables(0).Rows(0).Item("fvatotal").ToString
        '_Total = _Ds.Tables(0).Rows(0).Item("fvastot").ToString
        ice = _Ds.Tables(0).Rows(0).Item("fvaimpsi")
        _numidosif = _Ds1.Tables(0).Rows(0).Item("sbnumi").ToString
        _Key = _Ds1.Tables(0).Rows(0).Item("sbkey")
        _FechaAl = _Ds1.Tables(0).Rows(0).Item("sbfal")

        Dim maxNFac As Integer = L_fnObtenerMaxIdTabla("TFV001", "fvanfac", "fvaautoriz = " + _Autorizacion)
        _NumFac = maxNFac + 1

        _TotalCC = Math.Round(CDbl(_Total), MidpointRounding.AwayFromZero)
        _Cod_Control = ControlCode.generateControlCode(_Autorizacion, _NumFac, _Nit, _Fechainv, CStr(_TotalCC), _Key)

        'Literal 
        _TotalLi = _Ds.Tables(0).Rows(0).Item("fvastot") - _Ds.Tables(0).Rows(0).Item("fvadesc")
        _TotalDecimal = _TotalLi - Math.Truncate(_TotalLi)
        _TotalDecimal2 = CDbl(_TotalDecimal) * 100

        'Dim li As String = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(_Total) - CDbl(_TotalDecimal)) + " con " + IIf(_TotalDecimal2.Equals("0"), "00", _TotalDecimal2) + "/100 Bolivianos"
        _Literal = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(_TotalLi) - CDbl(_TotalDecimal)) + "  " + IIf(_TotalDecimal2.Equals("0"), "00", _TotalDecimal2) + "/100 Bolivianos"
        _Ds2 = L_Reporte_Factura_Cia("2")
        QrFactura.Text = _Ds2.Tables(0).Rows(0).Item("scnit").ToString + "|" + Str(_NumFac).Trim + "|" + _Autorizacion + "|" + _Fecha + "|" + _Total + "|" + _TotalLi.ToString + "|" + _Cod_Control + "|" + lbNit.Text.Trim + "|" + ice.ToString + "|0|0|" + Str(_Desc).Trim

        L_Modificar_Factura("fvanumi = " + CStr(numi),
                            "",
                            CStr(_NumFac),
                            CStr(_Autorizacion),
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            _Cod_Control,
                            _FechaAl.ToString("yyyy/MM/dd"),
                            "",
                            "",
                            CStr(numi))

        _Ds = L_Reporte_Factura(numi, numi)
        If Not IsNothing(P_Global.Visualizador) Then
            P_Global.Visualizador.Close()
        End If

        For I = 0 To _Ds.Tables(0).Rows.Count - 1
            _Ds.Tables(0).Rows(I).Item("fvaimgqr") = P_fnImageToByteArray(QrFactura.Image)
        Next
        If (impFactura) Then
            Dim objrep As Object = Nothing
            Dim empresaId = ObtenerEmpresaHabilitada()
            Dim empresaHabilitada As DataTable = ObtenerEmpresaTipoReporte(empresaId, Convert.ToInt32(ENReporte.FACTURA))
            For Each fila As DataRow In empresaHabilitada.Rows
                Select Case fila.Item("TipoReporte").ToString
                    Case ENReporteTipo.FACTURA_Ticket
                        objrep = New R_Factura_7_5x100
                        SerPArametros(_Ds, _Ds1, _Ds2, _Autorizacion, _Hora, _Literal, _NumFac, objrep,
                                      fila.Item("TipoReporte").ToString, _Fecha, grabarPDF, _numidosif, numi)
                    Case ENReporteTipo.FACTURA_MediaCarta
                        objrep = New R_FacturaMediaCarta
                        SerPArametros(_Ds, _Ds1, _Ds2, _Autorizacion, _Hora, _Literal, _NumFac, objrep,
                                      fila.Item("TipoReporte").ToString, _Fecha, grabarPDF, _numidosif, numi)
                    Case ENReporteTipo.FACTURA_Carta
                        objrep = New R_FacturaCarta
                        SerPArametros(_Ds, _Ds1, _Ds2, _Autorizacion, _Hora, _Literal, _NumFac, objrep,
                                      fila.Item("TipoReporte").ToString, _Fecha, grabarPDF, _numidosif, numi)
                End Select
            Next
        End If
    End Sub
    Private Sub SerPArametros(_Ds As DataSet, ByRef _Ds1 As DataSet, _Ds2 As DataSet, ByRef _Autorizacion As String, ByRef _Hora As String, ByRef _Literal As String,
                              ByRef _NumFac As Integer, objrep As Object, tipoReporte As String, _fecha As String, grabarPDF As Boolean, _numidosif As String, numi As String)
        Select Case tipoReporte
            Case ENReporteTipo.FACTURA_Ticket
                objrep.SetDataSource(_Ds.Tables(0))
                objrep.SetParameterValue("Hora", _Hora)
                objrep.SetParameterValue("Direccionpr", _Ds2.Tables(0).Rows(0).Item("scdir").ToString)
                objrep.SetParameterValue("Telefonopr", _Ds2.Tables(0).Rows(0).Item("sctelf").ToString)
                objrep.SetParameterValue("Literal1", _Literal)
                objrep.SetParameterValue("Literal2", " ")
                objrep.SetParameterValue("Literal3", " ")
                objrep.SetParameterValue("NroFactura", _NumFac)
                objrep.SetParameterValue("NroAutoriz", _Autorizacion)
                objrep.SetParameterValue("ENombre", _Ds2.Tables(0).Rows(0).Item("scneg").ToString) '?
                objrep.SetParameterValue("ECasaMatriz", _Ds2.Tables(0).Rows(0).Item("scsuc").ToString)
                objrep.SetParameterValue("ECiudadPais", _Ds2.Tables(0).Rows(0).Item("scpai").ToString)
                objrep.SetParameterValue("ESFC", "SFC " + _Ds1.Tables(0).Rows(0).Item("sbsfc").ToString)
                objrep.SetParameterValue("ENit", _Ds2.Tables(0).Rows(0).Item("scnit").ToString)
                objrep.SetParameterValue("EActividad", _Ds2.Tables(0).Rows(0).Item("scact").ToString)
                objrep.SetParameterValue("EDuenho", _Ds2.Tables(0).Rows(0).Item("scnom").ToString)
                'objrep.SetParameterValue("ENota", "''" + "ESTA FACTURA CONTRIBUYE AL DESARROLLO DEL PAÍS EL USO ILÍCITO DE ÉSTA SERÁ SANCIONADO DE ACUERDO A LA LEY" + "''")
                objrep.SetParameterValue("ENota", _Ds1.Tables(0).Rows(0).Item("sbNota").ToString)
                objrep.SetParameterValue("ELey", _Ds1.Tables(0).Rows(0).Item("sbnota2").ToString)
                objrep.SetParameterValue("FechaLim", _Ds1.Tables(0).Rows(0).Item("sbfal"))
                objrep.SetParameterValue("Usuario", gs_user)
                objrep.SetParameterValue("TipoVenta", "CONTADO")
                objrep.SetParameterValue("PlazoPago", Now.Date)
            Case ENReporteTipo.FACTURA_MediaCarta
                objrep.SetDataSource(_Ds.Tables(0))
                Dim fechaLiteral = ObtenerFechaLiteral(_fecha, _Ds2.Tables(0).Rows(0).Item("scciu").ToString)
                objrep.SetParameterValue("Fecliteral", fechaLiteral)
                objrep.SetParameterValue("Direccionpr", _Ds2.Tables(0).Rows(0).Item("scdir").ToString)
                objrep.SetParameterValue("Literal1", _Literal)
                objrep.SetParameterValue("NroFactura", _NumFac)
                objrep.SetParameterValue("NroAutoriz", _Autorizacion)
                objrep.SetParameterValue("ENombre", _Ds2.Tables(0).Rows(0).Item("scneg").ToString)
                objrep.SetParameterValue("ECasaMatriz", _Ds2.Tables(0).Rows(0).Item("scsuc").ToString)
                objrep.SetParameterValue("ECiudadPais", _Ds2.Tables(0).Rows(0).Item("scpai").ToString)
                objrep.SetParameterValue("ESFC", "SFC " + _Ds1.Tables(0).Rows(0).Item("sbsfc").ToString)
                objrep.SetParameterValue("ENit", _Ds2.Tables(0).Rows(0).Item("scnit").ToString)
                objrep.SetParameterValue("EActividad", _Ds2.Tables(0).Rows(0).Item("scact").ToString)
                objrep.SetParameterValue("Tipo", "ORIGINAL")
                objrep.SetParameterValue("Logo", gb_UbiLogo)
                objrep.SetParameterValue("TipoPago", "CONTADO")
                objrep.SetParameterValue("Nota2", _Ds1.Tables(0).Rows(0).Item("sbnota").ToString)
            Case ENReporteTipo.FACTURA_Carta
                objrep.SetDataSource(_Ds.Tables(0))
                objrep.SetParameterValue("Hora", _Hora)
                objrep.SetParameterValue("Direccionpr", _Ds2.Tables(0).Rows(0).Item("scdir").ToString)
                objrep.SetParameterValue("Telefonopr", _Ds2.Tables(0).Rows(0).Item("sctelf").ToString)
                objrep.SetParameterValue("Literal1", _Literal)
                objrep.SetParameterValue("Literal2", " ")
                objrep.SetParameterValue("Literal3", " ")
                objrep.SetParameterValue("NroFactura", _NumFac)
                objrep.SetParameterValue("NroAutoriz", _Autorizacion)
                objrep.SetParameterValue("ENombre", _Ds2.Tables(0).Rows(0).Item("scneg").ToString) '?
                objrep.SetParameterValue("ECasaMatriz", _Ds2.Tables(0).Rows(0).Item("scsuc").ToString)
                objrep.SetParameterValue("ECiudadPais", _Ds2.Tables(0).Rows(0).Item("scpai").ToString)
                objrep.SetParameterValue("ESFC", "SFC " + _Ds1.Tables(0).Rows(0).Item("sbsfc").ToString)
                objrep.SetParameterValue("ENit", _Ds2.Tables(0).Rows(0).Item("scnit").ToString)
                objrep.SetParameterValue("EActividad", _Ds2.Tables(0).Rows(0).Item("scact").ToString)
                objrep.SetParameterValue("EDuenho", _Ds2.Tables(0).Rows(0).Item("scnom").ToString)
                objrep.SetParameterValue("ESms", "''" + _Ds1.Tables(0).Rows(0).Item("sbnota").ToString + "''")
                objrep.SetParameterValue("ESms2", "''" + _Ds1.Tables(0).Rows(0).Item("sbnota2").ToString + "''")

                objrep.SetParameterValue("Logo", gb_UbiLogo)
                objrep.SetParameterValue("URLImageMarcaAgua", gs_CarpetaRaiz + "\MarcaAguaFactura.jpg")
        End Select
        Dim _Ds3 As DataSet = L_ObtenerRutaImpresora("1") ' Datos de Impresion de Facturación
        If (_Ds3.Tables(0).Rows(0).Item("cbvp")) Then 'Vista Previa de la Ventana de Vizualización 1 = True 0 = False
            P_Global.Visualizador = New Visualizador
            P_Global.Visualizador.CrGeneral.ReportSource = objrep
            P_Global.Visualizador.ShowDialog()
            P_Global.Visualizador.BringToFront()
        Else
            Dim pd As New PrintDocument()
            Dim instance As New Printing.PrinterSettings
            Dim impresosaPredt As String = instance.PrinterName
            pd.PrinterSettings.PrinterName = impresosaPredt

            If (Not pd.PrinterSettings.IsValid) Then
                ToastNotification.Show(Me, "La Impresora ".ToUpper + impresosaPredt + Chr(13) + "No Existe".ToUpper,
                                       My.Resources.WARNING, 5000,
                                       eToastGlowColor.Blue, eToastPosition.BottomRight)
            Else
                objrep.PrintOptions.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString '"EPSON TM-T20II Receipt5 (1)"
                objrep.PrintToPrinter(1, True, 0, 0)
                'crystalReportDocument.PrintOptions.PrinterName = "your printer name"
                'objrep.PrintTicket("EPSON TM-T20II Receipt")
            End If
        End If
        If (grabarPDF) Then
            'Copia de Factura en PDF
            If (Not Directory.Exists(gs_CarpetaRaiz + "\Facturas")) Then
                Directory.CreateDirectory(gs_CarpetaRaiz + "\Facturas")
            End If
            objrep.ExportToDisk(ExportFormatType.PortableDocFormat, gs_CarpetaRaiz + "\Facturas\" + CStr(_NumFac) + "_" + CStr(_Autorizacion) + ".pdf")
            L_Actualiza_Dosificacion(_numidosif, _NumFac, numi)
        End If
    End Sub
    Private Function ObtenerFechaLiteral(Fecliteral As String, ciudad As String) As String
        Dim dia, mes, ano As Integer
        Dim mesl As String
        dia = Microsoft.VisualBasic.Left(Fecliteral, 2)
        mes = Microsoft.VisualBasic.Mid(Fecliteral, 4, 2)
        ano = Microsoft.VisualBasic.Mid(Fecliteral, 7, 4)
        If mes = 1 Then
            mesl = "Enero"
        End If
        If mes = 2 Then
            mesl = "Febrero"
        End If
        If mes = 3 Then
            mesl = "Marzo"
        End If
        If mes = 4 Then
            mesl = "Abril"
        End If
        If mes = 5 Then
            mesl = "Mayo"
        End If
        If mes = 6 Then
            mesl = "Junio"
        End If
        If mes = 7 Then
            mesl = "Julio"
        End If
        If mes = 8 Then
            mesl = "Agosto"
        End If
        If mes = 9 Then
            mesl = "Septiembre"
        End If
        If mes = 10 Then
            mesl = "Octubre"
        End If
        If mes = 11 Then
            mesl = "Noviembre"
        End If
        If mes = 12 Then
            mesl = "Diciembre"
        End If
        Fecliteral = ciudad + ", " + dia.ToString + " de " + mesl + " del " + ano.ToString
        Return Fecliteral
    End Function
    Private Sub ReimprimirFactura(numi As String, impFactura As Boolean, grabarPDF As Boolean)
        Try
            Dim _Fecha, _FechaAl As Date
            Dim _Ds, _Ds1, _Ds2, _Ds3 As New DataSet
            Dim _Autorizacion, _Nit, _Fechainv, _Total, _Key, _Cod_Control, _Hora,
                _Literal, _TotalDecimal, _TotalDecimal2 As String
            Dim I, _NumFac, _numidosif, _TotalCC As Integer
            Dim ice, _Desc, _TotalLi As Decimal
            Dim _VistaPrevia As Integer = 0


            _Desc = CDbl(0)
            If Not IsNothing(P_Global.Visualizador) Then
                P_Global.Visualizador.Close()
            End If

            _Ds1 = L_Dosificacion("1", "1", _Fecha)
            _Ds = L_Reporte_Factura(numi, numi)
            _Fecha = _Ds.Tables(0).Rows(0).Item("fvafec").ToString
            _Hora = _Ds.Tables(0).Rows(0).Item("fvahora").ToString
            _Autorizacion = _Ds1.Tables(0).Rows(0).Item("sbautoriz").ToString
            _NumFac = CInt(_Ds.Tables(0).Rows(0).Item("fvanfac").ToString)
            '_NumFac = CInt(_Ds1.Tables(0).Rows(0).Item("sbnfac"))
            _Nit = _Ds.Tables(0).Rows(0).Item("fvanitcli").ToString

            _Fechainv = Microsoft.VisualBasic.Right(_Fecha.ToShortDateString, 4) +
                        Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(_Fecha.ToShortDateString, 5), 2) +
                        Microsoft.VisualBasic.Left(_Fecha.ToShortDateString, 2)
            _Total = _Ds.Tables(0).Rows(0).Item("fvatotal").ToString
            '_Total = "45.82"
            ' _Total = _Ds.Tables(0).Rows(0).Item("fvastot").ToString
            ice = _Ds.Tables(0).Rows(0).Item("fvaimpsi")
            _numidosif = _Ds1.Tables(0).Rows(0).Item("sbnumi").ToString
            _Key = _Ds1.Tables(0).Rows(0).Item("sbkey")
            _FechaAl = _Ds1.Tables(0).Rows(0).Item("sbfal")

            'Dim maxNFac As Integer = L_fnObtenerMaxIdTabla("TFV001", "fvanfac", "fvaautoriz = " + _Autorizacion)
            '_NumFac = maxNFac + 1

            _TotalCC = Math.Round(CDbl(_Total), MidpointRounding.AwayFromZero)
            _Cod_Control = ControlCode.generateControlCode(_Autorizacion, _NumFac, _Nit, _Fechainv, CStr(_TotalCC), _Key)

            'Literal 
            '_TotalLi = "2740.38"
            _TotalLi = _Ds.Tables(0).Rows(0).Item("fvastot") - _Ds.Tables(0).Rows(0).Item("fvadesc")
            _TotalDecimal = _TotalLi - Math.Truncate(_TotalLi)
            '_TotalDecimal = "0.93"
            _TotalDecimal2 = CDbl(_TotalDecimal) * 100

            'Dim li As String = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(_Total) - CDbl(_TotalDecimal)) + " con " + IIf(_TotalDecimal2.Equals("0"), "00", _TotalDecimal2) + "/100 Bolivianos"
            _Literal = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(_TotalLi) - CDbl(_TotalDecimal)) + "  " + IIf(_TotalDecimal2.Equals("0"), "00", _TotalDecimal2) + "/100 Bolivianos"
            _Ds2 = L_Reporte_Factura_Cia("2")
            QrFactura.Text = _Ds2.Tables(0).Rows(0).Item("scnit").ToString + "|" + Str(_NumFac).Trim + "|" + _Autorizacion + "|" + _Fecha + "|" + _Total + "|" + _TotalLi.ToString + "|" + _Cod_Control + "|" + lbNit.Text.Trim + "|" + ice.ToString + "|0|0|" + Str(_Desc).Trim

            L_Modificar_Factura("fvanumi = " + CStr(numi),
                                "",
                                CStr(_NumFac),
                                CStr(_Autorizacion),
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                _Cod_Control,
                                _FechaAl.ToString("yyyy/MM/dd"),
                                "",
                                "",
                                CStr(numi))

            _Ds = L_Reporte_Factura(numi, numi)


            If Not IsNothing(P_Global.Visualizador) Then
                P_Global.Visualizador.Close()
            End If

            For I = 0 To _Ds.Tables(0).Rows.Count - 1
                _Ds.Tables(0).Rows(I).Item("fvaimgqr") = P_fnImageToByteArray(QrFactura.Image)
            Next
            If (impFactura) Then
                Dim objrep As Object = Nothing
                Dim empresaId = ObtenerEmpresaHabilitada()
                Dim empresaHabilitada As DataTable = ObtenerEmpresaTipoReporte(empresaId, Convert.ToInt32(ENReporte.FACTURA))
                For Each fila As DataRow In empresaHabilitada.Rows
                    Select Case fila.Item("TipoReporte").ToString
                        Case ENReporteTipo.FACTURA_Ticket
                            objrep = New R_Factura_7_5x100
                            SerPArametros(_Ds, _Ds1, _Ds2, _Autorizacion, _Hora, _Literal, _NumFac, objrep,
                                      fila.Item("TipoReporte").ToString, _Fecha, False, _numidosif, numi)
                        Case ENReporteTipo.FACTURA_MediaCarta
                            objrep = New R_FacturaMediaCarta
                            SerPArametros(_Ds, _Ds1, _Ds2, _Autorizacion, _Hora, _Literal, _NumFac, objrep,
                                      fila.Item("TipoReporte").ToString, _Fecha, False, _numidosif, numi)
                        Case ENReporteTipo.FACTURA_Carta
                            objrep = New R_FacturaCarta
                            SerPArametros(_Ds, _Ds1, _Ds2, _Autorizacion, _Hora, _Literal, _NumFac, objrep,
                                          fila.Item("TipoReporte").ToString, _Fecha, grabarPDF, _numidosif, numi)
                    End Select
                Next
            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub

    Public Function P_fnImageToByteArray(ByVal imageIn As Image) As Byte()
        Dim ms As New System.IO.MemoryStream()
        imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
        Return ms.ToArray()
    End Function




    Private Sub P_prCargarVariablesIndispensables()
        If (gb_FacturaEmite) Then
            gi_IVA = CDbl(IIf(L_fnGetIVA().Rows(0).Item("scdebfis").ToString.Equals(""), gi_IVA, L_fnGetIVA().Rows(0).Item("scdebfis").ToString))
            gi_ICE = CDbl(IIf(L_fnGetICE().Rows(0).Item("scice").ToString.Equals(""), gi_ICE, L_fnGetICE().Rows(0).Item("scice").ToString))
        End If

    End Sub

    Private Sub P_prCargarParametro()
        'El sistema factura?



    End Sub
    Private Sub P_GenerarReporte(numi As String)
        Dim dt As DataTable = L_fnVentaNotaDeVenta(numi)
        If (gb_DetalleProducto) Then
            ponerDescripcionProducto(dt)
        End If
        'Dim total As Decimal = dt.Compute("SUM(Total)", "")
        Dim total As Decimal = tbTotal.Value
        Dim totald As Double = (total / 6.96)
        Dim fechaven As String = dt.Rows(0).Item("fechaventa")
        If Not IsNothing(P_Global.Visualizador) Then
            P_Global.Visualizador.Close()
        End If
        Dim ParteEntera As Long
        Dim ParteDecimal As Decimal
        Dim ParteDecimal2 As Decimal
        ParteEntera = Int(total)
        ParteDecimal = total - Math.Truncate(total)
        ParteDecimal2 = CDbl(ParteDecimal) * 100


        Dim li As String = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(ParteEntera)) + " " +
        IIf(ParteDecimal2.ToString.Equals("0"), "00", ParteDecimal2.ToString) + "/100 Bolivianos"

        ParteEntera = Int(totald)
        ParteDecimal = totald - Math.Truncate(totald)
        ParteDecimal2 = CDbl(ParteDecimal) * 100

        Dim lid As String = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(ParteEntera)) + " " +
        IIf(ParteDecimal2.ToString.Equals("0"), "00", ParteDecimal2.ToString) + "/100 Dolares"
        Dim _Hora As String = Now.Hour.ToString + ":" + Now.Minute.ToString
        Dim _Ds2 = L_Reporte_Factura_Cia("2")
        Dim dt2 As DataTable = L_fnNameReporte()
        Dim ParEmp1 As String = ""
        Dim ParEmp2 As String = ""
        Dim ParEmp3 As String = ""
        Dim ParEmp4 As String = ""
        If (dt2.Rows.Count > 0) Then
            ParEmp1 = dt2.Rows(0).Item("Empresa1").ToString
            ParEmp2 = dt2.Rows(0).Item("Empresa2").ToString
            ParEmp2 = dt2.Rows(0).Item("Empresa2").ToString
            ParEmp2 = dt2.Rows(0).Item("Empresa2").ToString
            ParEmp3 = dt2.Rows(0).Item("Empresa3").ToString
            ParEmp4 = dt2.Rows(0).Item("Empresa4").ToString
        End If

        Dim _Ds3 = L_ObtenerRutaImpresora("2") ' Datos de Impresion de Facturación
        If (_Ds3.Tables(0).Rows(0).Item("cbvp")) Then 'Vista Previa de la Ventana de Vizualización 1 = True 0 = False
            P_Global.Visualizador = New Visualizador 'Comentar
        End If
        Dim _FechaAct As String
        Dim _FechaPar As String
        Dim _Fecha() As String
        Dim _Meses() As String = {"Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"}
        _FechaAct = fechaven
        _Fecha = Split(_FechaAct, "-")
        _FechaPar = "Cochabamba, " + _Fecha(0).Trim + " De " + _Meses(_Fecha(1) - 1).Trim + " Del " + _Fecha(2).Trim
        Dim objrep As Object = Nothing
        Dim empresaId = ObtenerEmpresaHabilitada()
        Dim empresaHabilitada As DataTable = ObtenerEmpresaTipoReporte(empresaId, Convert.ToInt32(ENReporte.NOTAVENTA))
        For Each fila As DataRow In empresaHabilitada.Rows
            Select Case fila.Item("TipoReporte").ToString
                Case ENReporteTipo.NOTAVENTA_Carta
                    objrep = New R_NotaVenta_Carta
                    SetParametrosNotaVenta(dt, total, li, _Hora, _Ds2, _Ds3, fila.Item("TipoReporte").ToString, objrep)
                Case ENReporteTipo.NOTAVENTA_Ticket
                    objrep = New R_NotaVenta_7_5X100
                    SetParametrosNotaVenta(dt, total, li, _Hora, _Ds2, _Ds3, fila.Item("TipoReporte").ToString, objrep)
            End Select
        Next
    End Sub
    Private Sub SetParametrosNotaVenta(dt As DataTable, total As Decimal, li As String, _Hora As String, _Ds2 As DataSet, _Ds3 As DataSet, tipoReporte As String, objrep As Object)

        Select Case tipoReporte
            Case ENReporteTipo.NOTAVENTA_Carta
                objrep.SetDataSource(dt)
                objrep.SetParameterValue("Literal", li)
                objrep.SetParameterValue("TipoVenta", "CONTADO")
                objrep.SetParameterValue("Logo", gb_UbiLogo)
                objrep.SetParameterValue("NotaAdicional1", gb_NotaAdicional)
                objrep.SetParameterValue("Descuento", "0")
                objrep.SetParameterValue("Total", total)
            Case ENReporteTipo.NOTAVENTA_Ticket
                objrep.SetDataSource(dt)
                objrep.SetParameterValue("ECasaMatriz", _Ds2.Tables(0).Rows(0).Item("scsuc").ToString)
                objrep.SetParameterValue("ECiudadPais", _Ds2.Tables(0).Rows(0).Item("scpai").ToString)
                objrep.SetParameterValue("EDuenho", _Ds2.Tables(0).Rows(0).Item("scnom").ToString) '?
                objrep.SetParameterValue("Direccionpr", _Ds2.Tables(0).Rows(0).Item("scdir").ToString)
                objrep.SetParameterValue("Hora", _Hora)
                objrep.SetParameterValue("ENombre", _Ds2.Tables(0).Rows(0).Item("scneg").ToString) '?
                objrep.SetParameterValue("Literal1", li)
        End Select
        If (_Ds3.Tables(0).Rows(0).Item("cbvp")) Then 'Vista Previa de la Ventana de Vizualización 1 = True 0 = False
            P_Global.Visualizador.CrGeneral.ReportSource = objrep 'Comentar
            P_Global.Visualizador.ShowDialog() 'Comentar
            P_Global.Visualizador.BringToFront() 'Comentar
        Else
            Dim pd As New PrintDocument()
            pd.PrinterSettings.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
            If (Not pd.PrinterSettings.IsValid) Then
                ToastNotification.Show(Me, "La Impresora ".ToUpper + _Ds3.Tables(0).Rows(0).Item("cbrut").ToString + Chr(13) + "No Existe".ToUpper,
                                       My.Resources.WARNING, 5 * 1000,
                                       eToastGlowColor.Blue, eToastPosition.BottomRight)
            Else
                objrep.PrintOptions.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
                objrep.PrintToPrinter(1, True, 0, 0)
            End If
        End If
    End Sub

    Private Sub ponerDescripcionProducto(ByRef dt As DataTable)
        For Each fila As DataRow In dt.Rows
            Dim numi As Integer = fila.Item("codProducto")
            Dim dtDP As DataTable = L_fnDetalleProducto(numi)
            Dim des As String = fila.Item("producto") + vbNewLine + vbNewLine
            For Each fila2 As DataRow In dtDP.Rows
                des = des + fila2.Item("yfadesc").ToString + vbNewLine
            Next
            fila.Item("producto") = des
        Next
    End Sub

    Private Sub P_GenerarReporteFactura(numi As String)
        Dim dt As DataTable = L_fnVentaFactura(numi)
        Dim total As Double = dt.Compute("SUM(Total)", "")

        Dim ParteEntera As Long
        Dim ParteDecimal As Double
        ParteEntera = Int(total)
        ParteDecimal = total - ParteEntera
        Dim li As String = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(ParteEntera)) + " con " +
        IIf(ParteDecimal.ToString.Equals("0"), "00", ParteDecimal.ToString) + "/100 Bolivianos"


        Dim objrep As New R_FacturaFarmacia
        '' GenerarNro(_dt)
        ''objrep.SetDataSource(Dt1Kardex)
        'imprimir
        If PrintDialog1.ShowDialog = DialogResult.OK Then
            objrep.SetDataSource(dt)
            objrep.SetParameterValue("TotalEscrito", li)
            objrep.SetParameterValue("nit", lbNit.Text)
            objrep.SetParameterValue("Total", total)
            objrep.SetParameterValue("cliente", lbCliente.Text + " ")
            objrep.PrintOptions.PrinterName = PrintDialog1.PrinterSettings.PrinterName

            objrep.PrintToPrinter(1, False, 1, 1)
            objrep.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
        End If
        'objrep.SetDataSource(dt)
        'objrep.SetParameterValue("TotalEscrito", li)
        'objrep.SetParameterValue("nit", TbNit.Text)
        'objrep.SetParameterValue("Total", total)
        'P_Global.Visualizador.CrGeneral.ReportSource = objrep 'Comentar
        'P_Global.Visualizador.Show() 'Comentar
        'P_Global.Visualizador.BringToFront() 'Comentar



    End Sub

    Sub _prCargarProductoDeLaProforma(numiProforma As Integer)
        Dim dt As DataTable = L_fnListarProductoProforma(numiProforma)

        '        a.pbnumi ,a.pbtp1numi ,a.pbty5prod ,producto ,a.pbcmin ,a.pbumin ,Umin .ycdes3 as unidad,
        'a.pbpbas ,a.pbptot,a.pbporc,a.pbdesc ,a.pbtotdesc,
        'stock, pcosto
        If (dt.Rows.Count > 0) Then
            CType(grdetalle.DataSource, DataTable).Rows.Clear()
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim numiproducto As Integer = dt.Rows(i).Item("pbty5prod")
                Dim nameproducto As String = dt.Rows(i).Item("producto")
                Dim lote As String = ""
                Dim FechaVenc As Date = Now.Date
                Dim cant As Double = dt.Rows(i).Item("pbcmin")
                Dim iccven As Double = 0
                _prPedirLotesProducto(lote, FechaVenc, iccven, numiproducto, nameproducto, cant)
                _prAddDetalleVenta()
                grdetalle.Row = grdetalle.RowCount - 1
                If (lote <> String.Empty) Then
                    If (cant <= iccven) Then

                        grdetalle.SetValue("tbptot", dt.Rows(i).Item("pbptot"))
                        grdetalle.SetValue("tbtotdesc", dt.Rows(i).Item("pbtotdesc"))
                        grdetalle.SetValue("tbdesc", dt.Rows(i).Item("pbdesc"))
                        grdetalle.SetValue("tbcmin", cant)
                        grdetalle.SetValue("tbptot2", dt.Rows(i).Item("pcosto") * cant)

                    Else
                        Dim tot As Double = dt.Rows(i).Item("pbpbas") * iccven
                        grdetalle.SetValue("tbptot", tot)
                        grdetalle.SetValue("tbtotdesc", tot)
                        grdetalle.SetValue("tbdesc", 0)
                        grdetalle.SetValue("tbcmin", iccven)
                        grdetalle.SetValue("tbptot2", dt.Rows(i).Item("pcosto") * iccven)
                    End If
                    grdetalle.SetValue("tbty5prod", numiproducto)
                    grdetalle.SetValue("producto", nameproducto)
                    grdetalle.SetValue("tbumin", dt.Rows(i).Item("pbumin"))
                    grdetalle.SetValue("unidad", dt.Rows(i).Item("unidad"))
                    grdetalle.SetValue("tbpbas", dt.Rows(i).Item("pbpbas"))


                    If (gb_FacturaIncluirICE) Then
                        grdetalle.SetValue("tbpcos", dt.Rows(i).Item("pcosto"))

                    Else
                        grdetalle.SetValue("tbpcos", 0)
                    End If

                    grdetalle.SetValue("tblote", lote)
                    grdetalle.SetValue("tbfechaVenc", FechaVenc)
                    grdetalle.SetValue("stock", iccven)

                End If

                'grdetalle.Refetch()
                'grdetalle.Refresh()


            Next

            grdetalle.Select()
            _prCalcularPrecioTotal()
        End If
    End Sub
    Public Sub _prPedirLotesProducto(ByRef lote As String, ByRef FechaVenc As Date, ByRef iccven As Double, CodProducto As Integer, nameProducto As String, cant As Integer)
        Dim dt As New DataTable
        dt = L_fnListarLotesPorProductoVenta(Sucursal, CodProducto)  ''1=Almacen
        'b.yfcdprod1 ,a.iclot ,a.icfven  ,a.iccven 
        Dim listEstCeldas As New List(Of Modelo.Celda)
        listEstCeldas.Add(New Modelo.Celda("yfcdprod1,", False, "", 150))
        listEstCeldas.Add(New Modelo.Celda("iclot", True, "LOTE", 150))
        listEstCeldas.Add(New Modelo.Celda("icfven", True, "FECHA VENCIMIENTO", 180, "dd/MM/yyyy"))
        listEstCeldas.Add(New Modelo.Celda("iccven", True, "Stock".ToUpper, 150, "0.00"))
        Dim ef = New Efecto
        ef.tipo = 3
        ef.dt = dt
        ef.SeleclCol = 2
        ef.listEstCeldas = listEstCeldas
        ef.alto = 50
        ef.ancho = 350
        ef.Context = "Producto ".ToUpper + nameProducto + "  cantidad=" + Str(cant)
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        'b.yfcdprod1 ,a.iclot ,a.icfven  ,a.iccven 
        If (bandera = True) Then
            Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
            lote = Row.Cells("iclot").Value
            FechaVenc = Row.Cells("icfven").Value
            iccven = Row.Cells("iccven").Value
        End If
    End Sub

    Private Sub MostrarMensajeError(mensaje As String)
        ToastNotification.Show(Me,
                               mensaje.ToUpper,
                               My.Resources.WARNING,
                               5000,
                               eToastGlowColor.Red,
                               eToastPosition.TopCenter)

    End Sub
    Private Sub MostrarMensajeOk(mensaje As String)
        ToastNotification.Show(Me,
                               mensaje.ToUpper,
                               My.Resources.OK,
                               5000,
                               eToastGlowColor.Green,
                               eToastPosition.TopCenter)
    End Sub
#End Region
    Private Sub F0_VentasSupermercado_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _IniciarTodo()
        _Limpiar()
        tbProducto.Focus()

        tokenObtenido = ObtToken()
        'Dim code

        'MetPago(tokenObtenido)
        CodTipoDocumento(tokenObtenido)
        'code = VerifConexion(tokenObtenido)
        'If (code = 200) Then Label1Conn.Text = "Conectado con Siat" Else Label1Conn.Text = "No conectado con Siat"
    End Sub

    Private Sub tbProducto_KeyDown(sender As Object, e As KeyEventArgs) Handles tbProducto.KeyDown
        If e.KeyData = Keys.Control + Keys.C Then

            Dim dt As DataTable
            'dt = L_fnListarClientes()
            dt = L_fnListarClientesVenta()

            Dim listEstCeldas As New List(Of Modelo.Celda)
            listEstCeldas.Add(New Modelo.Celda("ydnumi,", True, "ID", 50))
            listEstCeldas.Add(New Modelo.Celda("ydcod", False, "ID", 50))
            listEstCeldas.Add(New Modelo.Celda("ydrazonsocial", True, "RAZÓN SOCIAL", 180))
            listEstCeldas.Add(New Modelo.Celda("yddesc", True, "NOMBRE", 280))
            listEstCeldas.Add(New Modelo.Celda("yddctnum", True, "N. Documento".ToUpper, 150))
            listEstCeldas.Add(New Modelo.Celda("yddirec", True, "DIRECCIÓN", 220))
            listEstCeldas.Add(New Modelo.Celda("ydtelf1", True, "Teléfono".ToUpper, 200))
            listEstCeldas.Add(New Modelo.Celda("ydfnac", True, "F.Nacimiento".ToUpper, 150, "MM/dd,YYYY"))
            listEstCeldas.Add(New Modelo.Celda("ydnumivend,", False, "ID", 50))
            listEstCeldas.Add(New Modelo.Celda("vendedor,", False, "ID", 50))
            listEstCeldas.Add(New Modelo.Celda("yddias", False, "CRED", 50))
            listEstCeldas.Add(New Modelo.Celda("ydnomfac", False, "Nombre Factura", 50))
            listEstCeldas.Add(New Modelo.Celda("ydnit", False, "Nit/CI", 50))
            Dim ef = New Efecto
            ef.tipo = 3
            ef.dt = dt
            ef.SeleclCol = 2
            ef.listEstCeldas = listEstCeldas
            ef.alto = 50
            ef.ancho = 350
            ef.Context = "Seleccione Cliente".ToUpper
            ef.ShowDialog()
            Dim bandera As Boolean = False
            bandera = ef.band
            If (bandera = True) Then
                Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row

                _CodCliente = Row.Cells("ydnumi").Value
                lbCliente.Text = Row.Cells("ydrazonsocial").Value
                _dias = Row.Cells("yddias").Value
                lbNit.Text = Row.Cells("ydnit").Value

                Dim numiVendedor As Integer = IIf(IsDBNull(Row.Cells("ydnumivend").Value), 0, Row.Cells("ydnumivend").Value)

                Table_Producto = Nothing

                If (numiVendedor > 0) Then
                    _CodEmpleado = Row.Cells("ydnumivend").Value
                Else
                    _CodEmpleado = 0
                    Table_Producto = Nothing
                End If

            End If
            tbProducto.Focus()
        End If
        If e.KeyData = Keys.Control + Keys.Enter Then

            _HabilitarProductos()

        End If
        If (e.KeyData = Keys.Control + Keys.S) Then
            If _ValidarCampos() = False Then
                Exit Sub
            End If
            Dim ef = New Efecto
            ef.tipo = 6
            ef.TotalVenta = Math.Round(tbTotal.Value, 2)
            ef.Nit = lbNit.Text
            ef.RazonSocial = lbCliente.Text

            With ef.Tdoc
                .DropDownList.Columns.Clear()
                .DropDownList.Columns.Add("codigoClasificador").Width = 70
                .DropDownList.Columns("codigoClasificador").Caption = "COD"
                .DropDownList.Columns.Add("descripcion").Width = 500
                .DropDownList.Columns("descripcion").Caption = "DESCRIPCION"
                .ValueMember = "codigoClasificador"
                .DisplayMember = "descripcion"
                .DataSource = CbTDoc.DataSource
                .Refresh()
            End With

            ef.ShowDialog()
            Dim bandera As Boolean = False
            bandera = ef.band
            If (bandera = True) Then

                TotalBs = ef.TotalBs
                TotalSus = ef.TotalSus
                TotalTarjeta = ef.TotalTarjeta
                lbNit.Text = ef.Nit
                lbCliente.Text = ef.RazonSocial
                TipoCambio = ef.TipoCambio
                CbTDoc.Value = ef.Tdoc.Value
                TbEmailS.Text = ef.Email
                IdNit = ef.IdNit
                NroTarjeta = ef.nroTarjeta
                CodExcepcion = ef.CExc

                _prGuardar()
            Else
                ToastNotification.Show(Me, "No se Ingreso Monto a Pagar ", My.Resources.WARNING, 4000, eToastGlowColor.Red, eToastPosition.TopCenter)

            End If

        End If
        If (e.KeyData = Keys.Enter) Then
            If (tbProducto.Text.Trim.Length < 2) Then
                Return

            End If
            cargarProductos()
            Dim codigoBarrasCompleto = tbProducto.Text.Trim
            Dim primerDigito As String = Mid(codigoBarrasCompleto, 1, 1)

            'If primerDigito = "2" Then
            '    Dim codigoBarrasProducto As Integer
            '    Dim totalEntero, totalDecimal, total2, total As Decimal
            '    codigoBarrasProducto = Mid(codigoBarrasCompleto, 1, 6)
            '    'CUANDO EL COD BARRA TENGA 6 DIGITOS  EJEM: 200001
            '    If (existeProducto(codigoBarrasProducto)) Then

            '        totalEntero = Mid(codigoBarrasCompleto, 7, 4)
            '        totalDecimal = Mid(codigoBarrasCompleto, 11, 2)
            '        total2 = CDbl(totalEntero).ToString() + "." + CDbl(totalDecimal).ToString()
            '        total = CDbl(total2)
            '        If (Not verificarExistenciaUnica(codigoBarrasProducto)) Then
            '            ponerProducto2(codigoBarrasProducto, total, -1)

            '        Else
            '            ponerProducto2(codigoBarrasProducto, total, grdetalle.RowCount - 1)
            '        End If

            '    Else
            '        ''CUANDO EL CODIGO DE BARRAS TENGA 7 DIGITOS EJEM: 2000001
            '        codigoBarrasProducto = Mid(codigoBarrasCompleto, 1, 7)
            '        If (existeProducto(codigoBarrasProducto)) Then
            '            totalEntero = Mid(codigoBarrasCompleto, 8, 3)
            '            totalDecimal = Mid(codigoBarrasCompleto, 11, 2)
            '            total2 = CDbl(totalEntero).ToString() + "." + CDbl(totalDecimal).ToString()
            '            total = CDbl(total2)
            '            If (Not verificarExistenciaUnica(codigoBarrasProducto)) Then
            '                ponerProducto2(codigoBarrasProducto, total, -1)

            '            Else
            '                ponerProducto2(codigoBarrasProducto, total, grdetalle.RowCount - 1)

            '            End If
            '        Else
            '            grdetalle.DataChanged = False
            '            ToastNotification.Show(Me, "El código de barra del producto no existe", My.Resources.WARNING, 3000, eToastGlowColor.Red, eToastPosition.TopCenter)
            '        End If
            '    End If
            'Else

            If (existeProducto(tbProducto.Text.Trim)) Then
                If (Not verificarExistenciaUnica(tbProducto.Text.Trim)) Then
                    Dim resultado As Boolean = False
                    ponerProducto(tbProducto.Text.Trim, resultado)

                    'VentanaCantidad()
                    VentanaCantidad(False)
                Else
                    'If (grdetalle.GetValue("producto").ToString <> String.Empty) Then

                    'sumarCantidad(tbProducto.Text.Trim)
                    'VentanaCantidad()
                    VentanaCantidad(True)

                    'Else
                    '    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                    '    ToastNotification.Show(Me, "El Producto: NO CUENTA CON STOCK DISPONIBLE", img, 5000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                    '    FilaSelectLote = Nothing
                    'End If
                End If
            Else
                grdetalle.DataChanged = False
                ToastNotification.Show(Me, "El código de barra del producto no existe o no tiene precio, verifique!!!".ToUpper, My.Resources.WARNING, 3300, eToastGlowColor.Red, eToastPosition.TopCenter)
            End If

            'End If

            tbProducto.Clear()
            tbProducto.Focus()
        End If
        If e.KeyData = Keys.Escape Then
            Me.Close()

        End If
        If e.KeyData = Keys.Down Then
            grdetalle.Focus()

        End If
    End Sub

    Private Sub sumarCantidad(codigo As String)
        Try
            Dim fila As DataRow() = CType(grdetalle.DataSource, DataTable).Select("yfcbarra='" + codigo.Trim + "'", "")
            If (fila.Count > 0) Then
                grdetalle.UpdateData()
                Dim pos1 As Integer = -1
                _fnObtenerFilaDetalle(pos1, fila(0).Item("tbnumi"))
                Dim cant As Integer = grdetalle.GetRow(pos1).Cells("tbcmin").Value + 1
                Dim stock As Integer = grdetalle.GetRow(pos1).Cells("stock").Value
                'If (cant > stock) Then
                Dim lin As Integer = grdetalle.GetRow(pos1).Cells("tbnumi").Value
                'Dim pos2 As Integer = -1
                '_fnObtenerFilaDetalle(pos2, lin)
                CType(grdetalle.DataSource, DataTable).Rows(pos1).Item("tbcmin") = cant
                CType(grdetalle.DataSource, DataTable).Rows(pos1).Item("tbptot") = CType(grdetalle.DataSource, DataTable).Rows(pos1).Item("tbpbas") * cant
                CType(grdetalle.DataSource, DataTable).Rows(pos1).Item("tbptot2") = grdetalle.GetRow(pos1).Cells("tbpcos").Value * cant
                CType(grdetalle.DataSource, DataTable).Rows(pos1).Item("tbtotdesc") = CType(grdetalle.DataSource, DataTable).Rows(pos1).Item("tbpbas") * cant
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                'ToastNotification.Show(Me, "La cantidad de la venta no debe ser mayor al del stock" & vbCrLf &
                '        "Stock=" + Str(stock).ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                'grdetalle.SetValue("yfcbarra", "")
                'grdetalle.SetValue("tbcmin", 0)
                'grdetalle.SetValue("tbptot", 0)
                'grdetalle.SetValue("tbptot2", 0)
                grdetalle.DataChanged = True
                'grdetalle.Refetch()
                grdetalle.Refresh()
                '_prCalcularPrecioTotal()
                'Else
                '    If (cant = stock) Then
                '        'grdetalle.SelectedFormatStyle.ForeColor = Color.Blue
                '        'grdetalle.CurrentRow.Cells.Item(e.Column).FormatStyle = New GridEXFormatStyle
                '        'grdetalle.CurrentRow.Cells(e.Column).FormatStyle.BackColor = Color.Pink
                '        'grdetalle.CurrentRow.Cells.Item(e.Column).FormatStyle.BackColor = Color.DodgerBlue
                '        'grdetalle.CurrentRow.Cells.Item(e.Column).FormatStyle.ForeColor = Color.White
                '        'grdetalle.CurrentRow.Cells.Item(e.Column).FormatStyle.FontBold = TriState.True
                '    End If
                'End If

                tbDescripcion.Text = CType(grdetalle.DataSource, DataTable).Rows(pos1).Item("producto")
                CalcularDescuentos(CType(grdetalle.DataSource, DataTable).Rows(pos1).Item("tbty5prod"), cant, CType(grdetalle.DataSource, DataTable).Rows(pos1).Item("tbpbas"), pos1)

                _prCalcularPrecioTotal()
            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub

    Private Sub ponerProducto(codigo As String, ByRef resultado As Boolean)
        Try



            If (grdetalle.GetValue("tbty5prod") > 0) Then
                _prAddDetalleVenta()
            End If
            Dim pos = grdetalle.RowCount - 1
            grdetalle.Row = grdetalle.RowCount - 1
            Dim fila As DataRow() = Table_Producto.Select("yfcbarra='" + codigo.Trim + "'", "")
            If (fila.Count > 0) Then
                ''Si tiene stock > 0
                'If fila(0).ItemArray(20) > 0 Then

                '    _fnObtenerFilaDetalle(pos, grdetalle.GetValue("tbnumi"))
                '    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbty5prod") = fila(0).ItemArray(0)
                '    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("codigo") = fila(0).ItemArray(1)
                '    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("yfcbarra") = fila(0).ItemArray(2)
                '    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("producto") = fila(0).ItemArray(3)
                '    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbfamilia") = fila(0).ItemArray(14)
                '    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbumin") = fila(0).ItemArray(16)
                '    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("unidad") = fila(0).ItemArray(17)
                '    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpbas") = fila(0).ItemArray(18)
                '    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot") = fila(0).ItemArray(18)
                '    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = fila(0).ItemArray(18)
                '    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbcmin") = 1
                '    If (gb_FacturaIncluirICE) Then
                '        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos") = fila(0).ItemArray(19)
                '    Else
                '        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos") = 0
                '    End If
                '    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos") = fila(0).ItemArray(19)
                '    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot2") = fila(0).ItemArray(19)
                '    'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot2") = fila(0).ItemArray(17)
                '    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("stock") = fila(0).ItemArray(20)
                '    'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tblote") = grProductos.GetValue("iclot")
                '    'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbfechaVenc") = grProductos.GetValue("icfven")
                '    'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("stock") = grProductos.GetValue("iccven")


                '    CalcularDescuentos(grdetalle.GetValue("tbty5prod"), 1, grdetalle.GetValue("tbpbas"), pos)
                '    _prCalcularPrecioTotal()
                '    tbDescripcion.Text = fila(0).ItemArray(3)
                '    tbPrecio.Value = fila(0).ItemArray(18)
                '    resultado = True
                'Else
                '    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                '    ToastNotification.Show(Me, "El Producto: ".ToUpper + fila(0).ItemArray(3) + " NO CUENTA CON STOCK DISPONIBLE", img, 5000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                '    FilaSelectLote = Nothing
                '    resultado = False
                'End If
                _fnObtenerFilaDetalle(pos, grdetalle.GetValue("tbnumi"))
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbty5prod") = fila(0).ItemArray(0)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("codigo") = fila(0).ItemArray(1)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("yfcbarra") = fila(0).ItemArray(2)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("producto") = fila(0).ItemArray(3)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbfamilia") = fila(0).ItemArray(14)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbumin") = fila(0).ItemArray(16)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("unidad") = fila(0).ItemArray(17)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpbas") = fila(0).ItemArray(18)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot") = fila(0).ItemArray(18)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = fila(0).ItemArray(18)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("ygcodsin") = fila(0).ItemArray(24)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("ygcodu") = fila(0).ItemArray(25)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbcmin") = 1
                If (gb_FacturaIncluirICE) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos") = fila(0).ItemArray(19)
                Else
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos") = 0
                End If
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos") = fila(0).ItemArray(19)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot2") = fila(0).ItemArray(19)
                'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot2") = fila(0).ItemArray(17)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("stock") = fila(0).ItemArray(20)
                'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tblote") = grProductos.GetValue("iclot")
                'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbfechaVenc") = grProductos.GetValue("icfven")
                'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("stock") = grProductos.GetValue("iccven")


                CalcularDescuentos(grdetalle.GetValue("tbty5prod"), 1, grdetalle.GetValue("tbpbas"), pos)
                _prCalcularPrecioTotal()
                tbDescripcion.Text = fila(0).ItemArray(3)
                tbPrecio.Value = fila(0).ItemArray(18)
                resultado = True

            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub

    Private Sub ponerProducto2(codigo As String, total As Decimal, pos As Integer)
        Try

            grdetalle.Row = grdetalle.RowCount - 1
            Dim fila As DataRow() = Table_Producto.Select("yfcbarra='" + codigo.Trim + "'", "")
            If (fila.Count > 0) Then
                If pos = -1 Then
                    If (grdetalle.GetValue("tbty5prod") > 0) Then
                        _prAddDetalleVenta()

                        grdetalle.Row = grdetalle.RowCount - 1
                    End If
                    _fnObtenerFilaDetalle(pos, grdetalle.GetValue("tbnumi"))
                End If
                Dim cantidad = Format(total / CDbl(fila(0).ItemArray(15)), "0.00")
                Dim precio = fila(0).ItemArray(15)
                total = cantidad * precio
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbty5prod") = fila(0).ItemArray(0)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("codigo") = fila(0).ItemArray(1)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("yfcbarra") = fila(0).ItemArray(2)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("producto") = fila(0).ItemArray(3)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbumin") = fila(0).ItemArray(13)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("unidad") = fila(0).ItemArray(14)
                'tbcmin
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpbas") = precio
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot") = total
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = total
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbcmin") = cantidad
                If (gb_FacturaIncluirICE) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos") = fila(0).ItemArray(17)
                Else
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos") = 0
                End If
                ''Modif
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos") = fila(0).ItemArray(16)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot2") = fila(0).ItemArray(16) * cantidad
                '
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("stock") = fila(0).ItemArray(17) - cantidad
                'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tblote") = grProductos.GetValue("iclot")
                'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbfechaVenc") = grProductos.GetValue("icfven")
                'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("stock") = grProductos.GetValue("iccven")
                tbDescripcion.Text = fila(0).ItemArray(3)
                tbPrecio.Value = precio
                CalcularDescuentos(grdetalle.GetValue("tbty5prod"), cantidad, grdetalle.GetValue("tbpbas"), pos)
                _prCalcularPrecioTotal()
            End If
        Catch ex As Exception
            Throw New Exception
        End Try

    End Sub
    Private Function verificarExistenciaUnica(codigo As String) As Boolean
        Dim cont As Integer = 0
        For Each fila As GridEXRow In grdetalle.GetRows()
            If (fila.Cells("yfcbarra").Value.ToString.Trim = codigo.Trim) Then
                cont += 1
            End If
        Next
        Return (cont >= 1)
    End Function
    Private Function existeProducto(codigo As String) As Boolean
        Return (Table_Producto.Select("yfcbarra='" + codigo.Trim() + "'", "").Count > 0)
    End Function
    Private Sub cargarProductos()
        Dim dt As DataTable
        If (G_Lote = True) Then
            dt = L_fnListarProductos(Sucursal, Str(_CodCliente))  ''1=Almacen
            Table_Producto = dt.Copy
        Else
            dt = L_fnListarProductosSinLote(Sucursal, Str(_CodCliente), CType(grdetalle.DataSource, DataTable).Clone)  ''1=Almacen
            Table_Producto = dt.Copy
        End If
    End Sub
    Private Sub grdetalle_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grdetalle.EditingCell

        'Habilitar solo las columnas de Precio, %, Monto y Observación
        'If (e.Column.Index = grdetalle.RootTable.Columns("yfcbarra").Index Or
        If (e.Column.Index = grdetalle.RootTable.Columns("yfcbarra").Index Or
                e.Column.Index = grdetalle.RootTable.Columns("tbcmin").Index Or
                e.Column.Index = grdetalle.RootTable.Columns("tbporc").Index Or
                e.Column.Index = grdetalle.RootTable.Columns("tbpbas").Index Or
                e.Column.Index = grdetalle.RootTable.Columns("tbdesc").Index) Then
            e.Cancel = True
        Else
            e.Cancel = True
        End If


    End Sub

    Private Sub grdetalle_KeyDown(sender As Object, e As KeyEventArgs) Handles grdetalle.KeyDown
        If (e.KeyData = Keys.Escape And grdetalle.Row >= 0) Then
            _prEliminarFila()
        End If

        If (e.KeyData = Keys.Control + Keys.C And grdetalle.Row >= 0) Then
            tbProducto.Text = grdetalle.CurrentRow.Cells.Item("yfcbarra").Value
            'VentanaCantidad()
            VentanaCantidad(True)
            tbProducto.Clear()

        End If

        If (e.KeyData = Keys.Enter) Then
            If (grdetalle.Col = grdetalle.RootTable.Columns("tbcmin").Index) Then
                tbProducto.Focus()
            End If
        End If
    End Sub
    Private Sub grProductos_KeyDown(sender As Object, e As KeyEventArgs) Handles grProductos.KeyDown
        Try

            If (e.KeyData = Keys.Enter) Then
                Dim f, c As Integer
                c = grProductos.Col
                f = grProductos.Row
                If (f >= 0) Then

                    If (IsNothing(FilaSelectLote)) Then
                        ''''''''''''''''''''''''
                        If (G_Lote = True) Then
                            InsertarProductosConLote()
                        Else
                            InsertarProductosSinLote()
                            'VentanaCantidad()
                            tbProducto.Clear()
                        End If
                        '''''''''''''''
                    Else

                        '_fnExisteProductoConLote()


                        Dim numiProd = FilaSelectLote.Item("yfnumi")
                        Dim lote As String = grProductos.GetValue("iclot")
                        Dim FechaVenc As Date = grProductos.GetValue("icfven")
                        Dim PosDataExistente As Integer = -1
                        If (Not _fnExisteProductoConLote(numiProd, lote, FechaVenc, PosDataExistente)) Then
                            Dim pos As Integer = -1
                            grdetalle.Row = grdetalle.RowCount - 1
                            If (grdetalle.GetValue("tbty5prod") <> 0) Then
                                _prAddDetalleVenta()
                            End If
                            grdetalle.Row = grdetalle.RowCount - 1
                            _fnObtenerFilaDetalle(pos, grdetalle.GetValue("tbnumi"))
                            'b.yfcdprod1, a.iclot, a.icfven, a.iccven
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbty5prod") = FilaSelectLote.Item("yfnumi")
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("codigo") = FilaSelectLote.Item("yfcprod")
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("yfcbarra") = FilaSelectLote.Item("yfcbarra")
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("producto") = FilaSelectLote.Item("yfcdprod1")
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbumin") = FilaSelectLote.Item("yfumin")
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("unidad") = FilaSelectLote.Item("UnidMin")
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbfamilia") = FilaSelectLote.Item("yfgr4")
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpbas") = FilaSelectLote.Item("yhprecio")
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot") = FilaSelectLote.Item("yhprecio")
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = FilaSelectLote.Item("yhprecio")
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("ygcodsin") = FilaSelectLote.Item("ygcodsin")
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("ygcodu") = FilaSelectLote.Item("ygcodu")
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbcmin") = 1
                            'If (gb_FacturaIncluirICE) Then
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos") = FilaSelectLote.Item("pcos")
                            'Else
                            '    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos") = 0
                            'End If
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot2") = FilaSelectLote.Item("pcos")

                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tblote") = grProductos.GetValue("iclot")
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbfechaVenc") = grProductos.GetValue("icfven")
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("stock") = grProductos.GetValue("iccven")



                            _prCalcularPrecioTotal()
                            _DesHabilitarProductos()


                            tbDescripcion.Text = FilaSelectLote.Item("yfcdprod1")
                            tbPrecio.Value = FilaSelectLote.Item("yhprecio")
                            FilaSelectLote = Nothing

                            tbProducto.Text = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("yfcbarra")
                            txtNumi.Text = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbnumi")
                            'VentanaCantidad()
                            VentanaCantidad(False)
                            tbProducto.Clear()
                            txtNumi.Clear()
                            tbProducto.Focus()
                        Else
                            'Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                            'ToastNotification.Show(Me, "El producto con el lote ya existe modifique su cantidad".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)producto
                            If (PosDataExistente >= 0) Then

                                'Dim cantidad As Double = CType(grdetalle.DataSource, DataTable).Rows(PosDataExistente).Item("tbcmin") + 1
                                Dim cantidad As Double = CType(grdetalle.DataSource, DataTable).Rows(PosDataExistente).Item("tbcmin")
                                CType(grdetalle.DataSource, DataTable).Rows(PosDataExistente).Item("tbcmin") = cantidad
                                CType(grdetalle.DataSource, DataTable).Rows(PosDataExistente).Item("tbptot") = CType(grdetalle.DataSource, DataTable).Rows(PosDataExistente).Item("tbpbas") * cantidad
                                CType(grdetalle.DataSource, DataTable).Rows(PosDataExistente).Item("tbtotdesc") = CType(grdetalle.DataSource, DataTable).Rows(PosDataExistente).Item("tbpbas") * cantidad
                                CType(grdetalle.DataSource, DataTable).Rows(PosDataExistente).Item("tbptot2") = CType(grdetalle.DataSource, DataTable).Rows(PosDataExistente).Item("tbpcos") * cantidad


                                _prCalcularPrecioTotal()
                                _DesHabilitarProductos()


                                tbDescripcion.Text = CType(grdetalle.DataSource, DataTable).Rows(PosDataExistente).Item("producto")
                                tbPrecio.Value = CType(grdetalle.DataSource, DataTable).Rows(PosDataExistente).Item("tbpbas")
                                FilaSelectLote = Nothing
                                tbProducto.Text = CType(grdetalle.DataSource, DataTable).Rows(PosDataExistente).Item("yfcbarra")
                                txtNumi.Text = CType(grdetalle.DataSource, DataTable).Rows(PosDataExistente).Item("tbnumi")
                                'VentanaCantidad()
                                VentanaCantidad(True)
                                tbProducto.Clear()
                                txtNumi.Clear()
                                tbProducto.Focus()
                            End If

                        End If
                    End If
                End If
            End If

            If e.KeyData = Keys.Escape Then
                _DesHabilitarProductos()
                FilaSelectLote = Nothing
            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub

    Private Sub VentanaCantidad()
        Dim ef = New Efecto
        ef.tipo = 7

        'ef.Stock = grdetalle.GetValue("stock")
        'ef.Cantidad = grdetalle.GetValue("tbcmin")
        'ef.NameProducto = grdetalle.GetValue("producto")
        'Dim Cantidad As Double = grdetalle.GetValue("tbcmin")
        Dim fila As DataRow()
        If (tbProducto.Text.Trim.Length <= 0) Then
            fila = CType(grdetalle.DataSource, DataTable).Select("tbnumi=" + txtNumi.Text.Trim + " and estado=0", "")
        Else
            fila = CType(grdetalle.DataSource, DataTable).Select("yfcbarra='" + tbProducto.Text.Trim + "' and estado=0", "")
        End If


        Dim dtProdConversion As DataTable = L_fnBuscarProductoConversion(fila(0).Item("tbty5prod"))
        ef.Stock = fila(0).Item("stock")
        ef.Cantidad = fila(0).Item("tbcmin")

        ef.NameProducto = fila(0).Item("producto")
        ef.Conversion = dtProdConversion.Rows(0).Item("yfvsup")
        Dim Cantidad As Double = fila(0).Item("tbcmin")

        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then

            Cantidad = ef.Cantidad

            If (Cantidad > 0) Then
                'Dim lin As Integer = grdetalle.GetValue("tbnumi")
                Dim lin As Integer = fila(0).Item("tbnumi")
                Dim pos As Integer = -1
                _fnObtenerFilaDetalle(pos, lin)

                'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbcmin") = Cantidad
                'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot") = grdetalle.GetValue("tbpbas") * Cantidad
                'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = grdetalle.GetValue("tbpbas") * Cantidad
                'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot2") = grdetalle.GetValue("tbpcos") * Cantidad

                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbcmin") = Cantidad
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot") = fila(0).Item("tbpbas") * Cantidad
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = fila(0).Item("tbpbas") * Cantidad
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot2") = fila(0).Item("tbpcos") * Cantidad


                'CalcularDescuentos(grdetalle.GetValue("tbty5prod"), Cantidad, grdetalle.GetValue("tbpbas"), pos)
                CalcularDescuentos(fila(0).Item("tbty5prod"), Cantidad, fila(0).Item("tbpbas"), pos)

                _prCalcularPrecioTotal()
                tbProducto.Focus()

            End If

        Else
            _prEliminarFilaDesdeCantidad(fila)
            'ToastNotification.Show(Me, "Se procederá a eliminar el producto".ToUpper, My.Resources.WARNING, 1200, eToastGlowColor.Red, eToastPosition.TopCenter)

        End If
        tbProducto.Focus()

    End Sub
    Private Sub VentanaCantidad(Sumar As Boolean)
        Dim ef = New Efecto
        ef.tipo = 7

        Dim fila As DataRow()
        If (tbProducto.Text.Trim.Length <= 0) Then
            fila = CType(grdetalle.DataSource, DataTable).Select("tbnumi=" + txtNumi.Text.Trim + " and estado=0", "")
        Else
            fila = CType(grdetalle.DataSource, DataTable).Select("yfcbarra='" + tbProducto.Text.Trim + "' and estado=0", "")
        End If


        Dim dtProdConversion As DataTable = L_fnBuscarProductoConversion(fila(0).Item("tbty5prod"))
        ef.Stock = fila(0).Item("stock")
        If Sumar Then
            'ef.Cantidad = fila(0).Item("tbcmin")
            ef.Cantidad = 1
            ef.CantidadPrevia = fila(0).Item("tbcmin")
        Else
            ef.Cantidad = 1
        End If

        ef.NameProducto = fila(0).Item("producto")
        ef.Conversion = dtProdConversion.Rows(0).Item("yfvsup")
        Dim Cantidad As Double = fila(0).Item("tbcmin")

        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then
            If Sumar Then
                Cantidad = ef.Cantidad + Cantidad
            Else
                Cantidad = ef.Cantidad
            End If

            If (Cantidad > 0) Then
                'Dim lin As Integer = grdetalle.GetValue("tbnumi")
                Dim lin As Integer = fila(0).Item("tbnumi")
                Dim pos As Integer = -1
                _fnObtenerFilaDetalle(pos, lin)

                'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbcmin") = Cantidad
                'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot") = grdetalle.GetValue("tbpbas") * Cantidad
                'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = grdetalle.GetValue("tbpbas") * Cantidad
                'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot2") = grdetalle.GetValue("tbpcos") * Cantidad

                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbcmin") = Cantidad
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot") = fila(0).Item("tbpbas") * Cantidad
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = fila(0).Item("tbpbas") * Cantidad
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot2") = fila(0).Item("tbpcos") * Cantidad


                'CalcularDescuentos(grdetalle.GetValue("tbty5prod"), Cantidad, grdetalle.GetValue("tbpbas"), pos)
                CalcularDescuentos(fila(0).Item("tbty5prod"), Cantidad, fila(0).Item("tbpbas"), pos)

                _prCalcularPrecioTotal()
                tbProducto.Focus()

            End If

        Else
            If Sumar Then
                'No sucederá ni sumará nada en caso que apreten Scape
            Else
                _prEliminarFilaDesdeCantidad(fila)
            End If

            'ToastNotification.Show(Me, "Se procederá a eliminar el producto".ToUpper, My.Resources.WARNING, 1200, eToastGlowColor.Red, eToastPosition.TopCenter)
        End If
        tbProducto.Focus()

    End Sub
    Private Sub grdetalle_CellValueChanged(sender As Object, e As ColumnActionEventArgs) Handles grdetalle.CellValueChanged
        Try
            If (e.Column.Index = grdetalle.RootTable.Columns("tbcmin").Index) Or (e.Column.Index = grdetalle.RootTable.Columns("tbpbas").Index) Then
                If (Not IsNumeric(grdetalle.GetValue("tbcmin")) Or grdetalle.GetValue("tbcmin").ToString = String.Empty) Then

                    'grDetalle.GetRow(rowIndex).Cells("cant").Value = 1
                    '  grDetalle.CurrentRow.Cells.Item("cant").Value = 1
                    Dim lin As Integer = grdetalle.GetValue("tbnumi")
                    Dim pos As Integer = -1
                    Dim rowIndex As Integer = grdetalle.Row
                    _fnObtenerFilaDetalle(pos, lin)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbcmin") = 1
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpbas")

                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbporc") = 0
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbdesc") = 0
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpbas")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot2") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos")
                    grdetalle.SetValue("tbcmin", 1)
                    grdetalle.SetValue("tbptot", grdetalle.GetValue("tbpbas"))
                    P_PonerTotal(rowIndex)
                Else
                    If (grdetalle.GetValue("tbcmin") > 0) Then
                        Dim rowIndex As Integer = grdetalle.Row
                        Dim porcdesc As Double = grdetalle.GetValue("tbporc")
                        Dim montodesc As Double = ((grdetalle.GetValue("tbpbas") * grdetalle.GetValue("tbcmin")) * (porcdesc / 100))
                        Dim lin As Integer = grdetalle.GetValue("tbnumi")
                        Dim pos As Integer = -1
                        _fnObtenerFilaDetalle(pos, lin)
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot") = grdetalle.GetValue("tbpbas") * grdetalle.GetValue("tbcmin")

                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbdesc") = montodesc
                        grdetalle.SetValue("tbdesc", montodesc)
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = (grdetalle.GetValue("tbpbas") * grdetalle.GetValue("tbcmin")) - montodesc

                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot2") = grdetalle.GetValue("tbpcos") * grdetalle.GetValue("tbcmin")

                        P_PonerTotal(rowIndex)

                    Else
                        Dim lin As Integer = grdetalle.GetValue("tbnumi")
                        Dim pos As Integer = -1
                        _fnObtenerFilaDetalle(pos, lin)
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbcmin") = 1
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpbas")

                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbporc") = 0
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbdesc") = 0
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpbas")
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot2") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos")
                        grdetalle.SetValue("tbcmin", 1)
                        grdetalle.SetValue("tbptot", grdetalle.GetValue("tbpbas"))
                        _prCalcularPrecioTotal()
                        'grdetalle.SetValue("tbcmin", 1)
                        'grdetalle.SetValue("tbptot", grdetalle.GetValue("tbpbas"))

                    End If
                End If
            End If
            '''''''''''''''''''''PORCENTAJE DE DESCUENTO '''''''''''''''''''''
            If (e.Column.Index = grdetalle.RootTable.Columns("tbporc").Index) Then
                If (Not IsNumeric(grdetalle.GetValue("tbporc")) Or grdetalle.GetValue("tbporc").ToString = String.Empty) Then

                    'grDetalle.GetRow(rowIndex).Cells("cant").Value = 1
                    '  grDetalle.CurrentRow.Cells.Item("cant").Value = 1
                    Dim lin As Integer = grdetalle.GetValue("tbnumi")
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, lin)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbporc") = 0
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbdesc") = 0
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot")
                    'grdetalle.SetValue("tbcmin", 1)
                    'grdetalle.SetValue("tbptot", grdetalle.GetValue("tbpbas"))
                Else
                    If (grdetalle.GetValue("tbporc") > 0 And grdetalle.GetValue("tbporc") <= 100) Then

                        Dim porcdesc As Double = grdetalle.GetValue("tbporc")
                        Dim montodesc As Double = (grdetalle.GetValue("tbptot") * (porcdesc / 100))
                        Dim lin As Integer = grdetalle.GetValue("tbnumi")
                        Dim pos As Integer = -1
                        _fnObtenerFilaDetalle(pos, lin)
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbdesc") = montodesc
                        grdetalle.SetValue("tbdesc", montodesc)

                        Dim rowIndex As Integer = grdetalle.Row
                        P_PonerTotal(rowIndex)

                    Else
                        Dim lin As Integer = grdetalle.GetValue("tbnumi")
                        Dim pos As Integer = -1
                        _fnObtenerFilaDetalle(pos, lin)
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbporc") = 0
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbdesc") = 0
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot")
                        grdetalle.SetValue("tbporc", 0)
                        grdetalle.SetValue("tbdesc", 0)
                        grdetalle.SetValue("tbtotdesc", grdetalle.GetValue("tbptot"))
                        _prCalcularPrecioTotal()
                        'grdetalle.SetValue("tbcmin", 1)
                        'grdetalle.SetValue("tbptot", grdetalle.GetValue("tbpbas"))

                    End If
                End If
            End If


            '''''''''''''''''''''MONTO DE DESCUENTO '''''''''''''''''''''
            If (e.Column.Index = grdetalle.RootTable.Columns("tbdesc").Index) Then
                If (Not IsNumeric(grdetalle.GetValue("tbdesc")) Or grdetalle.GetValue("tbdesc").ToString = String.Empty) Then

                    'grDetalle.GetRow(rowIndex).Cells("cant").Value = 1
                    '  grDetalle.CurrentRow.Cells.Item("cant").Value = 1
                    Dim lin As Integer = grdetalle.GetValue("tbnumi")
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, lin)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbporc") = 0
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbdesc") = 0
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot")
                    'grdetalle.SetValue("tbcmin", 1)
                    'grdetalle.SetValue("tbptot", grdetalle.GetValue("tbpbas"))
                Else
                    If (grdetalle.GetValue("tbdesc") > 0 And grdetalle.GetValue("tbdesc") <= grdetalle.GetValue("tbptot")) Then

                        Dim montodesc As Double = grdetalle.GetValue("tbdesc")
                        Dim pordesc As Double = ((montodesc * 100) / grdetalle.GetValue("tbptot"))

                        Dim lin As Integer = grdetalle.GetValue("tbnumi")
                        Dim pos As Integer = -1
                        _fnObtenerFilaDetalle(pos, lin)
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbdesc") = montodesc
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbporc") = pordesc

                        grdetalle.SetValue("tbporc", pordesc)
                        Dim rowIndex As Integer = grdetalle.Row
                        P_PonerTotal(rowIndex)

                    Else
                        Dim lin As Integer = grdetalle.GetValue("tbnumi")
                        Dim pos As Integer = -1
                        _fnObtenerFilaDetalle(pos, lin)
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbporc") = 0
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbdesc") = 0
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot")
                        grdetalle.SetValue("tbporc", 0)
                        grdetalle.SetValue("tbdesc", 0)
                        grdetalle.SetValue("tbtotdesc", grdetalle.GetValue("tbptot"))
                        _prCalcularPrecioTotal()
                        'grdetalle.SetValue("tbcmin", 1)
                        'grdetalle.SetValue("tbptot", grdetalle.GetValue("tbpbas"))

                    End If
                End If
            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub
    Private Sub grdetalle_MouseClick(sender As Object, e As MouseEventArgs) Handles grdetalle.MouseClick
        Try

            If (grdetalle.RowCount >= 2) Then
                If (grdetalle.CurrentColumn.Index = grdetalle.RootTable.Columns("img").Index) Then
                    _prEliminarFila()
                    tbProducto.Focus()
                End If
            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try

    End Sub

    Private Sub tbProducto_TextChanged(sender As Object, e As EventArgs) Handles tbProducto.TextChanged
        Dim tbP As TextBox = sender


    End Sub

    Private Sub ModificarCantidadMenu_Click(sender As Object, e As EventArgs) Handles ModificarCantidadMenu.Click
        If (grdetalle.Row >= 0) Then
            Dim ef = New Efecto
            ef.tipo = 7
            ef.Stock = grdetalle.GetValue("stock")
            ef.Cantidad = grdetalle.GetValue("tbcmin")
            ef.NameProducto = grdetalle.GetValue("producto")
            Dim Cantidad As Double = grdetalle.GetValue("tbcmin")
            ef.ShowDialog()
            Dim bandera As Boolean = False
            bandera = ef.band
            If (bandera = True) Then

                Cantidad = ef.Cantidad
                If (Cantidad > 0) Then
                    Dim lin As Integer = grdetalle.GetValue("tbnumi")
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, lin)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbcmin") = Cantidad
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot") = grdetalle.GetValue("tbpbas") * Cantidad
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = grdetalle.GetValue("tbpbas") * Cantidad
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot2") = grdetalle.GetValue("tbpcos") * Cantidad



                    CalcularDescuentos(grdetalle.GetValue("tbty5prod"), Cantidad, grdetalle.GetValue("tbpbas"), pos)
                    _prCalcularPrecioTotal()
                    tbProducto.Focus()

                End If

            Else
                ToastNotification.Show(Me, "No se Modificó Cantidad del Producto", My.Resources.WARNING, 4000, eToastGlowColor.Red, eToastPosition.TopCenter)

            End If
            tbProducto.Focus()
        End If

    End Sub

    Private Sub EliminarProductoMenu_Click(sender As Object, e As EventArgs) Handles EliminarProductoMenu.Click
        If (grdetalle.Row >= 0) Then
            _prEliminarFila()
            tbProducto.Focus()
        End If
    End Sub


    Public Sub ObtenerImagenes()
        Dim RutaGlobal As String = gs_CarpetaRaiz

        _prCrearCarpetaTemporal(RutaGlobal + "\SuperMercado")
        ListImagenes = Directory.GetFiles(RutaGlobal + "\SuperMercado", "*.jpg")
        'If (ListImagenes.Count > 0) Then
        '    pictureImagen.ImageLocation = ListImagenes(0)
        'End If

    End Sub
    Private Sub _prCrearCarpetaTemporal(RutaTemporal As String)

        If System.IO.Directory.Exists(RutaTemporal) = False Then
            System.IO.Directory.CreateDirectory(RutaTemporal)


        End If

    End Sub


    Private Sub TimerImagenes_Tick(sender As Object, e As EventArgs) Handles TimerImagenes.Tick

        If (contador <= ListImagenes.Count - 1) Then
            pictureImagen.ImageLocation = ListImagenes(contador)
            contador += 1
        Else
            contador = 0
        End If



    End Sub

    Private Function quitarUltimaFilaVacia(tabla As DataTable) As DataTable
        If tabla.Rows.Count > 0 Then
            If (tabla.Rows(tabla.Rows.Count - 1).Item("producto").ToString() = String.Empty) Then
                tabla.Rows.RemoveAt(tabla.Rows.Count - 1)
                tabla.AcceptChanges()
            End If
        End If
        Return tabla
    End Function




    'Funciones de conexion con sifac para facturacion

    Public Shared Function ObtToken()
        Dim api = New DBApi()

        Dim Lenvio = New LoginEnvio()
        Lenvio.email = "lilian@dynasys.com.bo"
        Lenvio.password = "123456Lily*"

        Dim url = "https://crex.sifac.nwc.com.bo/api/v2/login"

        Dim headers = New List(Of Parametro) From {
            New Parametro("Authorization", "bearer "),
            New Parametro("Content-Type", "Accept:application/json; charset=utf-8")
        }

        Dim parametros = New List(Of Parametro)

        Dim response = api.Post(url, headers, parametros, Lenvio)
        Dim json = JsonConvert.SerializeObject(Lenvio)
        ''MsgBox(json)

        Dim result = JsonConvert.DeserializeObject(Of RespuestLogin)(response)
        Dim Token As String
        Dim json1 = JsonConvert.SerializeObject(response)
        '' MsgBox(json1)
        Token = result.data.access_token.ToString
        Return Token
    End Function
    Public Function VerifConexion(tokenObtenido)

        Dim api = New DBApi()

        Dim url = "https://crex.sifac.nwc.com.bo/api/v2/conexion-siat"

        Dim headers = New List(Of Parametro) From {
            New Parametro("Authorization", "Bearer " + tokenObtenido),
            New Parametro("Content-Type", "Accept:application/json; charset=utf-8")
        }

        Dim parametros = New List(Of Parametro)

        Dim response = api.MGet(url, headers, parametros)

        Dim result = JsonConvert.DeserializeObject(Of SiatConn)(response)

        Dim Codigoconn As String
        Codigoconn = result.meta.code
        'Dim json = JsonConvert.SerializeObject(result)
        'MsgBox(json)
        Return Codigoconn
    End Function
    Public Function MetPago(tokenObtenido)

        Dim api = New DBApi()

        Dim url = "https://crex.sifac.nwc.com.bo/api/v2/metodos-pago"

        Dim headers = New List(Of Parametro) From {
            New Parametro("Authorization", "Bearer " + tokenObtenido),
            New Parametro("Content-Type", "Accept:application/json; charset=utf-8")
        }

        Dim parametros = New List(Of Parametro)

        Dim response = api.MGet(url, headers, parametros)

        Dim result = JsonConvert.DeserializeObject(Of MetodoPago)(response)

        Return ""
    End Function

    Public Function CodTipoDocumento(tokenObtenido)

        Dim api = New DBApi()

        Dim url = "https://crex.sifac.nwc.com.bo/api/v2/tipo-documento"

        Dim headers = New List(Of Parametro) From {
            New Parametro("Authorization", "Bearer " + tokenObtenido),
            New Parametro("Content-Type", "Accept:application/json; charset=utf-8")
        }

        Dim parametros = New List(Of Parametro)

        Dim response = api.MGet(url, headers, parametros)

        Dim result = JsonConvert.DeserializeObject(Of TipoDocumento)(response)

        With CbTDoc
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("codigoClasificador").Width = 70
            .DropDownList.Columns("codigoClasificador").Caption = "COD"
            .DropDownList.Columns.Add("descripcion").Width = 500
            .DropDownList.Columns("descripcion").Caption = "DESCRIPCION"
            .ValueMember = "codigoClasificador"
            .DisplayMember = "descripcion"
            .DataSource = result.data
            .Refresh()
        End With


        Return ""
    End Function


    Public Function Emisor(tokenObtenido)

        Dim api = New DBApi()
        Dim Emenvio = New EmisorEnvio.Emisor()

        'Dim TMetPago = CbMetPago.SelectedIndex + 1 'obtiene el 'Codigo Metodo de pago' 
        Dim TDoc = CbTDoc.Value 'obtiene el 'Codigo Tipo de documento' 

        Dim d As Integer = CodExcepcion
        'Dim tabla As DataTable = rearmarDetalle()
        Dim dtDetalle As DataTable = quitarUltimaFilaVacia(CType(grdetalle.DataSource, DataTable))

        dtDetalle = dtDetalle.Select("estado=0").CopyToDataTable

        Dim array(dtDetalle.Rows.Count - 1) As EmisorEnvio.Detalle
        Dim val = 0
        PrecioTot = 0
        For Each row In dtDetalle.Rows
            Dim EmenvioDetalle = New EmisorEnvio.Detalle()

            EmenvioDetalle.codigoProductoSin = (row("ygcodsin").ToString)
            EmenvioDetalle.codigoProducto = (row("tbty5prod").ToString)
            EmenvioDetalle.descripcion = (row("producto").ToString)
            EmenvioDetalle.unidadMedida = Convert.ToInt32(row("ygcodu"))
            EmenvioDetalle.cantidad = (row("tbcmin"))
            EmenvioDetalle.precioUnitario = (row("tbpbas"))
            EmenvioDetalle.montoDescuento = Math.Round((row("tbdesc")), 2)
            EmenvioDetalle.subTotal = (row("tbtotdesc"))
            EmenvioDetalle.numeroSerie = "0"
            EmenvioDetalle.numeroImei = "0"

            PrecioTot = PrecioTot + (row("tbtotdesc")) 'total
            'CodProducto = (row("tbty5prod").ToString) 'cod producto
            'Cantidad = (row("tbcmin").ToString) ' cantidad
            'PrecioU = (row("tbpbas").ToString) ' precio u 

            'NombreProd = (row("producto").ToString) ' nombreproducto  ydrazonsocial
            'razonsocial = (row("ydrazonsocial").ToString)
            'nroNit = (row("ydnit").ToString)

            array(val) = EmenvioDetalle
            'vector = array
            val = val + 1

        Next
        Dim NumFactura As Integer
        Dim email As String
        Dim CodMetPago As Integer
        Dim NTarjeta As String

        If TbEmailS.Text = String.Empty Then
            email = "cliente@crex.com.bo"
            TbEmailS.Text = email
        Else
            email = TbEmailS.Text
        End If

        If NroTarjeta <> "" And TotalTarjeta > 0 Then
            CodMetPago = 2
            NTarjeta = NroTarjeta
        Else
            CodMetPago = 1
            NTarjeta = ""
        End If


        Dim dtmax = L_fnObtenerMaxFact(gs_NroCaja, Convert.ToInt32(Now.Date.Year))
        If dtmax.Rows.Count = 0 Then
            NumFactura = 1
        Else
            Dim maxNFac As Integer = dtmax.Rows(0).Item("fvanfac")
            NumFactura = maxNFac + 1
        End If

        Emenvio.numeroFactura = NumFactura
        Emenvio.nombreRazonSocial = lbCliente.Text.ToString()
        Emenvio.codigoTipoDocumentoIdentidad = TDoc
        Emenvio.numeroDocumento = lbNit.Text.ToString()
        Emenvio.complemento = "" '---------------------------------
        Emenvio.codigoCliente = "B-" + IdNit
        Emenvio.codigoMetodoPago = CodMetPago
        Emenvio.numeroTarjeta = NTarjeta '---------------------
        Emenvio.codigoPuntoVenta = gs_NroCaja '--------------------
        Emenvio.codigoDocumentoSector = 1 '-------------------
        Emenvio.codigoMoneda = 1 'falta
        Emenvio.tipoCambio = 1 'CDbl(cbCambioDolar.Text) '--------------------
        Emenvio.descuentoAdicional = 0 '-------------------
        Emenvio.montoTotal = Math.Round((PrecioTot - Emenvio.descuentoAdicional), 2)
        Emenvio.montoTotalSujetoIva = Math.Round((PrecioTot - Emenvio.descuentoAdicional), 2)
        Emenvio.montoTotalMoneda = Math.Round((PrecioTot - Emenvio.descuentoAdicional), 2)
        Emenvio.montoGiftCard = 0 '----------------
        Emenvio.codigoExcepcion = 0 '---------------
        Emenvio.usuario = gs_user
        Emenvio.email = email
        Emenvio.actividadEconomica = 471110 'Actividad económica una sola para todos los productos
        Emenvio.detalles = array
        Dim json = JsonConvert.SerializeObject(Emenvio)
        Dim url = "https://crex.sifac.nwc.com.bo/api/v2/emision"

        Dim headers = New List(Of Parametro) From {
            New Parametro("Authorization", "Bearer " + tokenObtenido),
            New Parametro("Content-Type", "Accept:application/json; charset=utf-8")
        }

        Dim parametros = New List(Of Parametro)

        Dim response = api.Post(url, headers, parametros, Emenvio)

        Dim result = JsonConvert.DeserializeObject(Of RespEmisor)(response)
        Dim resultError = JsonConvert.DeserializeObject(Of Resp400)(response)

        Dim codigo = result.meta.code
        Dim xml As String
        If codigo = 200 Then
            Dim details = result.data.details
            xml = result.data.dataXml
            Dim doc As XmlDocument = New XmlDocument()
            doc.LoadXml(xml)
            Dim nodo = doc.DocumentElement("cabecera")
            gb_cufSifac = nodo.ChildNodes.Item(5).InnerText 'extrae dato CUF de xml sifac

            NroFact = result.data.numeroFactura
            QrUrl = result.data.qrUrl
            FactUrl = result.data.facturaUrl
            SegundaLeyenda = result.data.leyenda
            TerceraLeyenda = result.data.terceraLeyenda
            Cudf = result.data.cufd

            Dim notifi = New notifi

            notifi.tipo = 2
            notifi.Context = "SIFAC".ToUpper
            notifi.Header = "Proceso Exitoso - Código: " + codigo.ToString() & vbCrLf & " " & vbCrLf & details & vbCrLf & " " & vbCrLf & "Factura enviada al Siat".ToUpper
            notifi.ShowDialog()
            'MsgBox("Proceso Exitoso: " + codigo.ToString() & vbCrLf & " " & vbCrLf & details & vbCrLf & " " & vbCrLf & "Factura enviada al Siat")
        ElseIf codigo = 400 Or codigo = 500 Then

            Dim details = JsonConvert.SerializeObject(resultError.errors.details)
            Dim siat = JsonConvert.SerializeObject(resultError.errors.siat)
            Dim notifi = New notifi

            notifi.tipo = 2
            notifi.Context = "SIFAC".ToUpper
            notifi.Header = "Error de solicitud - Código: " + codigo.ToString() & vbCrLf & " " & vbCrLf & details & vbCrLf & siat & vbCrLf & " " & vbCrLf & "La factura no pudo enviarse al Siat".ToUpper
            notifi.ShowDialog()
            'MsgBox("Error de solicitud: " + codigo.ToString() & vbCrLf & " " & vbCrLf & details & vbCrLf & siat & vbCrLf & " " & vbCrLf & "La factura no pudo enviarse al Siat")
        ElseIf codigo = 401 Or codigo = 404 Or codigo = 405 Or codigo = 422 Then
            Dim details = JsonConvert.SerializeObject(resultError.errors.details)
            Dim notifi = New notifi

            notifi.tipo = 2
            notifi.Context = "SIFAC".ToUpper
            notifi.Header = "Error de solicitud - Código: " + codigo.ToString() & vbCrLf & " " & vbCrLf & details & vbCrLf & " " & vbCrLf & "La factura no pudo enviarse al Siat".ToUpper
            notifi.ShowDialog()
            'MsgBox("Error de solicitud: " + codigo.ToString() & vbCrLf & " " & vbCrLf & details & vbCrLf & " " & vbCrLf & "La factura no pudo enviarse al Siat")
        End If

        Return codigo
    End Function

    Public Function VerificarNit(tokenObtenido As String, nitCli As String)
        Try

            Dim api = New DBApi()
            Dim nit As String = nitCli.Trim

            Dim url = "https://crex.sifac.nwc.com.bo/api/v2/nit/" + nit + ""

            Dim headers = New List(Of Parametro) From {
                New Parametro("Authorization", "Bearer " + tokenObtenido),
                New Parametro("Content-Type", "Accept:application/json; charset=utf-8")
            }

            Dim parametros = New List(Of Parametro)

            Dim response = api.MGet(url, headers, parametros)

            Dim result = JsonConvert.DeserializeObject(Of RespuestNit)(response)
            Dim resultError = JsonConvert.DeserializeObject(Of Resp400)(response)

            Dim codigo = result.meta.code

            If codigo = 200 Then
                Dim details = result.data.details

                'Dim notifi = New notifi

                'notifi.tipo = 2
                'notifi.Context = "SIFAC".ToUpper
                'notifi.Header = "Proceso Exitoso - Código: " + codigo.ToString() & vbCrLf & " " & vbCrLf & details & vbCrLf & " " & vbCrLf & "Validado correctamente".ToUpper
                'notifi.ShowDialog()

            ElseIf codigo = 400 Then

                Dim details = JsonConvert.SerializeObject(resultError.errors.details)
                Dim notifi = New notifi

                notifi.tipo = 2
                notifi.Context = "SIFAC".ToUpper
                notifi.Header = "Error de solicitud - Código: " + codigo.ToString() & vbCrLf & " " & vbCrLf & details & vbCrLf & " " & vbCrLf & " Verifique".ToUpper
                notifi.ShowDialog()

                'Dim bandera As Boolean = False
                'bandera = notifi.band
                'If (bandera = True) Then

                '    CodExcepcion = 0
                'Else
                '     CodExcepcion = 1
                'End If

            ElseIf codigo = 500 Then

                    Dim details = JsonConvert.SerializeObject(resultError.errors.details)
                    Dim siat = JsonConvert.SerializeObject(resultError.errors.siat)
                    Dim notifi = New notifi

                    notifi.tipo = 2
                    notifi.Context = "SIFAC".ToUpper
                    notifi.Header = "Error de solicitud - Código: " + codigo.ToString() & vbCrLf & " " & vbCrLf & details & vbCrLf & siat & vbCrLf & " " & vbCrLf & " Verifique".ToUpper
                    notifi.ShowDialog()

                ElseIf codigo = 401 Or codigo = 404 Or codigo = 405 Then
                    Dim details = JsonConvert.SerializeObject(resultError.errors.details)
                Dim notifi = New notifi

                notifi.tipo = 2
                notifi.Context = "SIFAC".ToUpper
                notifi.Header = "Error de solicitud - Código: " + codigo.ToString() & vbCrLf & " " & vbCrLf & details & vbCrLf & " " & vbCrLf & "No se pudo validar".ToUpper
                notifi.ShowDialog()

            End If

            Return codigo

        Catch ex As Exception
            MostrarMensajeError(ex.Message)
            Return 0
        End Try

    End Function



End Class