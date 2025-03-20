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
Imports System.Drawing.Printing
Imports CrystalDecisions.Shared
Imports Facturacion

Public Class F0_Vales
    Dim _Inter As Integer = 0
#Region "Variables Globales"
    Dim _CodCliente As Integer = 0
    Dim _CodEmpleado As Integer = 0
    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
    Dim FilaSelectLote As DataRow = Nothing
    Dim Lote As Boolean = False '1=igual a mostrar las columnas de lote y fecha de Vencimiento
    Dim dtDescuentos As DataTable = Nothing
    Dim Table_Producto As DataTable

    Dim dtname As DataTable
#End Region

#Region "Métodos Privados"
    Private Sub _IniciarTodo()
        MSuperTabControl.SelectedTabIndex = 0

        _prCargarVale()
        _prInhabilitar()
        grVales.Focus()
        Me.Text = "VALES"
        Dim blah As New Bitmap(New Bitmap(My.Resources.ic_p), 20, 20)
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico
        _prAsignarPermisos()

        dtname = L_fnNameLabel()
    End Sub

    Private Sub _prCargarComboLibreriaSucursal(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnListarSucursales()
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("aanumi").Width = 60
            .DropDownList.Columns("aanumi").Caption = "COD"
            .DropDownList.Columns.Add("aabdes").Width = 500
            .DropDownList.Columns("aabdes").Caption = "SUCURSAL"
            .ValueMember = "aanumi"
            .DisplayMember = "aabdes"
            .DataSource = dt
            .Refresh()
        End With
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
    Private Sub _prInhabilitar()
        tbCodigo.ReadOnly = True
        dtFecha.IsInputReadOnly = True
        dtFecha.Enabled = False
        tbEmpresa.ReadOnly = True
        tbNroVales.ReadOnly = True
        tbCantVale.IsInputReadOnly = True
        tbCliente.ReadOnly = True
        tbNit.ReadOnly = True
        tbMontoVale.IsInputReadOnly = True
        tbCodMovimiento.ReadOnly = True
        tbObservacion.ReadOnly = True
        'swMostrar.Enabled = False

        btnModificar.Enabled = True
        btnGrabar.Enabled = False
        btnNuevo.Enabled = True
        btnEliminar.Enabled = True

        tbSubTotal.IsInputReadOnly = True
        tbMdesc.IsInputReadOnly = True
        tbtotal.IsInputReadOnly = True

        grVales.Enabled = True
        PanelNavegacion.Enabled = True
        grdetalle.RootTable.Columns("img").Visible = False

        FilaSelectLote = Nothing
    End Sub
    Private Sub _prhabilitar()


        tbEmpresa.ReadOnly = False
        tbNroVales.ReadOnly = False
        tbCantVale.IsInputReadOnly = False
        tbCliente.ReadOnly = False
        tbNit.ReadOnly = False
        tbMontoVale.IsInputReadOnly = False
        tbObservacion.ReadOnly = False

        btnGrabar.Enabled = True
    End Sub
    Public Sub _prFiltrar()
        'cargo el buscador
        Dim _Mpos As Integer
        _prCargarVale()
        If grVales.RowCount > 0 Then
            _Mpos = 0
            grVales.Row = _Mpos
        Else
            _Limpiar()
            LblPaginacion.Text = "0/0"
        End If
    End Sub
    Private Sub _Limpiar()
        tbCodigo.Clear()
        tbCliente.Clear()
        tbObservacion.Clear()
        _CodCliente = 0
        _CodEmpleado = 0
        dtFecha.Value = Now.Date
        _prCargarDetalleVenta(-1)
        MSuperTabControl.SelectedTabIndex = 0

        tbSubTotal.Value = 0
        tbMdesc.Value = 0
        tbtotal.Value = 0

        With grdetalle.RootTable.Columns("img")
            .Width = 80
            .Caption = "Eliminar".ToUpper
            .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
            .Visible = True
        End With


        FilaSelectLote = Nothing
        'dtDescuentos = L_fnListarDescuentosTodos()
    End Sub
    Public Sub _prMostrarRegistro(_N As Integer)

        With grVales
            tbCodigo.Text = .GetValue("vanumi")
            tbCodMovimiento.Text = .GetValue("vaIdMovimiento")
            dtFecha.Value = .GetValue("vafdoc")
            tbEmpresa.Text = .GetValue("vaNombreEmpresa")
            tbNroVales.Text = .GetValue("vanrovales")
            tbCantVale.Text = .GetValue("vacantvales")
            tbCliente.Text = .GetValue("vacliente")
            tbNit.Text = .GetValue("vaCI")
            tbMontoVale.Value = .GetValue("vaMontoVale")
            tbExcedente.Value = .GetValue("vaExcedente")
            tbBeneficio.Value = .GetValue("vaBeneficio")
            tbObservacion.Text = .GetValue("vaobs")

            tbSubTotal.Value = grVales.GetValue("vasubtotal")
            tbMdesc.Value = grVales.GetValue("vadesc")
            tbtotal.Value = grVales.GetValue("vatotal")

            lbFecha.Text = CType(.GetValue("vafact"), Date).ToString("dd/MM/yyyy")
            lbHora.Text = .GetValue("vahact").ToString
            lbUsuario.Text = .GetValue("vauact").ToString
        End With

        _prCargarDetalleVenta(tbCodigo.Text)

        '_prCalcularPrecioTotal()
        LblPaginacion.Text = Str(grVales.Row + 1) + "/" + grVales.RowCount.ToString

    End Sub

    Private Sub _prCargarDetalleVenta(_numi As String)
        Dim dt As New DataTable
        dt = L_fnDetalleVale(_numi)
        grdetalle.DataSource = dt
        grdetalle.RetrieveStructure()
        grdetalle.AlternatingColors = True

        With grdetalle.RootTable.Columns("vbnumi")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("vbv1numi")
            .Width = 90
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("vbty5prod")
            .Width = 80
            .Caption = "COD. DYNASYS"
            .Visible = True
        End With
        With grdetalle.RootTable.Columns("codigo")
            .Width = 80
            .Caption = "COD. DELTA"
            .Visible = True
        End With
        With grdetalle.RootTable.Columns("yfcbarra")
            .Width = 90
            .Caption = "COD. BARRAS"
            .Visible = True
        End With
        With grdetalle.RootTable.Columns("producto")
            .Caption = "PRODUCTOS"
            .Width = 400
            .Visible = True
            .MaxLines = 3
            .WordWrap = True
        End With
        With grdetalle.RootTable.Columns("vbest")
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("vbcmin")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Cantidad".ToUpper
        End With
        With grdetalle.RootTable.Columns("vbumin")
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("unidad")
            .Width = 100
            .Visible = False
            .Caption = "Unidad".ToUpper
        End With
        With grdetalle.RootTable.Columns("vbpbas")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Precio U.".ToUpper
        End With
        With grdetalle.RootTable.Columns("vbptot")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Sub Total".ToUpper
        End With
        With grdetalle.RootTable.Columns("vbporc")
            .Visible = False
            .FormatString = "0.00"
        End With
        With grdetalle.RootTable.Columns("vbdesc")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Descuento".ToUpper
        End With
        With grdetalle.RootTable.Columns("vbtotdesc")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Total".ToUpper
        End With
        With grdetalle.RootTable.Columns("vbobs")
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("vbpcos")
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("vbptot2")
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("vbfact")
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("vbhact")
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("vbuact")
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("estado")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("img")
            .Width = 80
            .Caption = "Eliminar".ToUpper
            .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
            .Visible = False
        End With

        With grdetalle
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007

            .RecordNavigator = True
            .RecordNavigatorText = "Productos"
        End With
    End Sub

    Private Sub _prCargarVale()
        Dim dt As New DataTable
        dt = L_fnGeneralVale(IIf(swMostrar.Value = True, 1, 0))
        grVales.DataSource = dt
        grVales.RetrieveStructure()
        grVales.AlternatingColors = True

        With grVales.RootTable.Columns("vanumi")
            .Width = 80
            .Caption = "CÓDIGO"
            .Visible = True
        End With
        With grVales.RootTable.Columns("vaIdMovimiento")
            .Width = 80
            .Caption = "COD. MOVIMIENTO"
            .Visible = True
        End With
        With grVales.RootTable.Columns("vafdoc")
            .Width = 80
            .Visible = True
            .Caption = "FECHA"
        End With
        With grVales.RootTable.Columns("vaNombreEmpresa")
            .Width = 250
            .Visible = True
            .Caption = "NOMBRE EMPRESA"
        End With
        With grVales.RootTable.Columns("vanrovales")
            .Width = 130
            .Visible = True
            .Caption = "NRO. VALE"
        End With
        With grVales.RootTable.Columns("vacantvales")
            .Width = 70
            .Visible = True
            .Caption = "CANT. VALES"
        End With
        With grVales.RootTable.Columns("vaalm")
            .Visible = False
        End With
        With grVales.RootTable.Columns("vacliente")
            .Width = 200
            .Visible = True
            .Caption = "CLIENTE"
        End With
        With grVales.RootTable.Columns("vaCI")
            .Width = 80
            .Visible = True
            .Caption = "CI/NIT CLIENTE"
        End With
        With grVales.RootTable.Columns("vaMontoVale")
            .Width = 90
            .Visible = True
            .Caption = "MONTO VALE"
            .FormatString = "0.00"
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        End With
        With grVales.RootTable.Columns("vaExcedente")
            .Width = 90
            .Visible = True
            .Caption = "EXCEDENTE VALE"
            .FormatString = "0.00"
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        End With
        With grVales.RootTable.Columns("vaBeneficio")
            .Width = 90
            .Visible = True
            .Caption = "BENEFICIO VALE"
            .FormatString = "0.00"
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        End With
        With grVales.RootTable.Columns("vaest")
            .Visible = False
        End With
        With grVales.RootTable.Columns("vaobs")
            .Width = 150
            .Visible = True
            .Caption = "OBSERVACION"
        End With
        With grVales.RootTable.Columns("vasubtotal")
            .Width = 90
            .Visible = True
            .Caption = "SUBTOTAL"
            .FormatString = "0.00"
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        End With
        With grVales.RootTable.Columns("vadesc")
            .Width = 90
            .Visible = True
            .Caption = "DESCUENTO"
            .FormatString = "0.00"
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        End With
        With grVales.RootTable.Columns("vatotal")
            .Width = 90
            .Visible = True
            .Caption = "TOTAL"
            .FormatString = "0.00"
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        End With
        With grVales.RootTable.Columns("vaNrocaja")
            .Width = 60
            .Visible = True
            .Caption = "NRO. CAJA"
        End With
        With grVales.RootTable.Columns("vacampo1")
            .Visible = False
        End With
        With grVales.RootTable.Columns("vafact")
            .Visible = False
        End With
        With grVales.RootTable.Columns("vahact")
            .Visible = False
        End With
        With grVales.RootTable.Columns("vauact")
            .Width = 90
            .Caption = "USUARIO"
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
        End With

        With grVales
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla

        End With

        If (dt.Rows.Count <= 0) Then
            _prCargarDetalleVenta(-1)
        End If
    End Sub


    Public Sub actualizarSaldoSinLote(ByRef dt As DataTable)

        Dim _detalle As DataTable = CType(grdetalle.DataSource, DataTable)

        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
            Dim sum As Integer = 0
            Dim codProducto As Integer = dt.Rows(i).Item("yfnumi")
            For j As Integer = 0 To grdetalle.RowCount - 1 Step 1
                grdetalle.Row = j
                Dim estado As Integer = grdetalle.GetValue("estado")
                If (estado = 0) Then
                    If (codProducto = grdetalle.GetValue("pbty5prod")) Then
                        sum = sum + grdetalle.GetValue("pbcmin")
                    End If
                End If
            Next
            dt.Rows(i).Item("stock") = dt.Rows(i).Item("stock") - sum
        Next

    End Sub


    Private Sub _prAddDetalleVenta()
        Dim Bin As New MemoryStream
        Dim img As New Bitmap(My.Resources.delete, 28, 28)
        img.Save(Bin, Imaging.ImageFormat.Png)
        CType(grdetalle.DataSource, DataTable).Rows.Add(_fnSiguienteNumi() + 1, 0, 0, "", 0, "", 0, 0, 0, "", 0, 0, 0, 0, 0, Now.Date, "", "", 0, Bin.GetBuffer, 0, 0)
    End Sub

    Public Function _fnSiguienteNumi()
        Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)
        Dim rows() As DataRow = dt.Select("pbnumi=MAX(pbnumi)")
        If (rows.Count > 0) Then
            Return rows(rows.Count - 1).Item("pbnumi")
        End If
        Return 1
    End Function
    Public Function _fnAccesible()
        Return tbEmpresa.ReadOnly = False
    End Function

    Public Sub _fnObtenerFilaDetalle(ByRef pos As Integer, numi As Integer)
        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _numi As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("pbnumi")
            If (_numi = numi) Then
                pos = i
                Return
            End If
        Next
    End Sub

    Public Function _fnExisteProducto(idprod As Integer) As Boolean
        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _idprod As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("pbty5prod")
            Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("estado")
            If (_idprod = idprod And estado >= 0) Then

                Return True
            End If
        Next
        Return False
    End Function


    Public Sub P_PonerTotal(rowIndex As Integer)
        If (rowIndex < grdetalle.RowCount) Then

            Dim lin As Integer = grdetalle.GetValue("pbnumi")
            Dim pos As Integer = -1
            _fnObtenerFilaDetalle(pos, lin)
            Dim cant As Double = grdetalle.GetValue("pbcmin")
            Dim uni As Double = grdetalle.GetValue("pbpbas")
            Dim MontoDesc As Double = grdetalle.GetValue("pbdesc")
            Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)
            If (pos >= 0) Then
                Dim TotalUnitario As Double = cant * uni

                'grDetalle.SetValue("lcmdes", montodesc)

                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbptot") = TotalUnitario
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbtotdesc") = TotalUnitario - MontoDesc
                grdetalle.SetValue("pbptot", TotalUnitario)
                grdetalle.SetValue("pbtotdesc", TotalUnitario - MontoDesc)

                Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado")
                If (estado = 1) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = 2
                End If
            End If
            _prCalcularPrecioTotal()
        End If

    End Sub
    Public Sub _prCalcularPrecioTotal()
        'Dim montodesc As Double = tbMdesc.Value
        'Dim pordesc As Double = ((montodesc * 100) / grdetalle.GetTotal(grdetalle.RootTable.Columns("pbtotdesc"), AggregateFunction.Sum))
        'tbPdesc.Value = pordesc
        'tbSubTotal.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("pbtotdesc"), AggregateFunction.Sum)

        'tbtotal.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("pbtotdesc"), AggregateFunction.Sum) - montodesc

        Dim TotalDescuento As Double = 0
        Dim Subtotal As Double = 0
        Dim Descuento As Double = 0

        Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)
        For i As Integer = 0 To dt.Rows.Count - 1 Step 1

            If (dt.Rows(i).Item("estado") >= 0) Then
                Subtotal = Subtotal + dt.Rows(i).Item("pbptot")
                TotalDescuento = TotalDescuento + dt.Rows(i).Item("pbtotdesc")
                Descuento += dt.Rows(i).Item("pbdesc")
            End If
        Next

        'grdetalle.UpdateData()
        Dim montoDo As Decimal
        Dim montodesc As Double = 0
        Dim pordesc As Double = ((montodesc * 100) / TotalDescuento)
        tbSubTotal.Value = Subtotal
        tbtotal.Value = TotalDescuento
        tbMdesc.Value = Descuento

    End Sub

    Public Function _ValidarCampos() As Boolean
        If dtFecha.Value > Now.Date Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "La fecha no puede ser mayor a la fecha actual".ToUpper, img, 2800, eToastGlowColor.Red, eToastPosition.BottomCenter)
            dtFecha.Focus()
            Return False
        End If
        If (tbEmpresa.Text = String.Empty) Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor ingrese nombre de la empresa".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            tbEmpresa.Focus()
            Return False
        End If
        If (tbNroVales.Text = String.Empty) Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor ingrese número del vale".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            tbNroVales.Focus()
            Return False
        End If
        If (tbCantVale.Text = String.Empty) Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor ingrese cantidad de vales".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            tbCantVale.Focus()
            Return False
        End If
        If (tbCliente.Text = String.Empty) Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor ingrese nombre del cliente".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            tbCliente.Focus()
            Return False
        End If
        If (tbNit.Text = String.Empty) Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor ingrese el Ci/Nit del cliente".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            tbNit.Focus()
            Return False
        End If
        If (tbMontoVale.Text = String.Empty Or tbMontoVale.Value = 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor ingrese monto del vale".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            tbMontoVale.Focus()
            Return False
        End If
        'If (grdetalle.RowCount = 1) Then
        '    grdetalle.Row = grdetalle.RowCount - 1
        '    If (grdetalle.GetValue("pbty5prod") = 0) Then
        '        Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
        '        ToastNotification.Show(Me, "Por Favor introduzca productos al detalle".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        '        Return False
        '    End If
        'End If
        Return True
    End Function


    Private Sub _prGuardarModificado()
        Dim res As Boolean = L_fnModificarVale(tbCodigo.Text, tbEmpresa.Text, tbNroVales.Text, tbCantVale.Value, dtFecha.Value.ToString("yyyy/MM/dd"),
                                               tbCliente.Text, tbNit.Text, tbMontoVale.Value, tbExcedente.Value, tbBeneficio.Value,
                                               tbObservacion.Text.Trim.ToUpper, tbCodMovimiento.Text, gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina)
        If res Then

            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "Código de Vale ".ToUpper + tbCodigo.Text + " Modificado con éxito.".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter
                                      )

            _prCargarVale()
            _prSalir()
        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "El Vale no pudo ser Modificado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        End If
    End Sub
    Private Sub _prSalir()
        If btnGrabar.Enabled = True Then
            _prInhabilitar()
            If grVales.RowCount > 0 Then
                _prMostrarRegistro(0)
            End If
        Else
            _modulo.Select()
            Me.Close()
        End If
    End Sub
    Public Sub _prCargarIconELiminar()
        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim Bin As New MemoryStream
            Dim img As New Bitmap(My.Resources.delete, 28, 28)
            img.Save(Bin, Imaging.ImageFormat.Png)
            CType(grdetalle.DataSource, DataTable).Rows(i).Item("img") = Bin.GetBuffer
            grdetalle.RootTable.Columns("img").Visible = True
        Next

    End Sub


    Public Sub _PrimerRegistro()
        Dim _MPos As Integer
        If grVales.RowCount > 0 Then
            _MPos = 0
            ''   _prMostrarRegistro(_MPos)
            grVales.Row = _MPos
        End If
    End Sub
#End Region


#Region "Eventos Formulario"
    Private Sub F0_Ventas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _IniciarTodo()
    End Sub
    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        '_Limpiar()
        '_prhabilitar()

        'btnNuevo.Enabled = False
        'btnModificar.Enabled = False
        'btnEliminar.Enabled = False
        'btnGrabar.Enabled = True
        'PanelNavegacion.Enabled = False

    End Sub

    Private Sub mostrarmensajeerror(mensaje As String)
        ToastNotification.Show(Me,
                               mensaje.ToUpper,
                               My.Resources.WARNING,
                               5000,
                               eToastGlowColor.Red,
                               eToastPosition.TopCenter)

    End Sub
    Private Sub mostrarmensajeok(mensaje As String)
        ToastNotification.Show(Me,
                               mensaje.ToUpper,
                               My.Resources.OK,
                               5000,
                               eToastGlowColor.Green,
                               eToastPosition.TopCenter)
    End Sub
    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _prSalir()
    End Sub


    Private Sub grdetalle_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grdetalle.EditingCell
        'If (_fnAccesible()) Then
        '    'Habilitar solo las columnas de Precio, %, Monto y Observación
        '    If (e.Column.Index = grdetalle.RootTable.Columns("vbcmin").Index Or
        '        e.Column.Index = grdetalle.RootTable.Columns("yfcbarra").Index) Then
        '        e.Cancel = False
        '    Else
        '        e.Cancel = True
        '    End If
        'Else
        '    e.Cancel = True
        'End If
        e.Cancel = True
    End Sub

    Private Sub grdetalle_Enter(sender As Object, e As EventArgs) Handles grdetalle.Enter
        'If (_fnAccesible()) Then
        '    If (_CodCliente <= 0) Then
        '        ToastNotification.Show(Me, "           Antes de Continuar Por favor Seleccione un Cliente!!             ", My.Resources.WARNING, 4000, eToastGlowColor.Red, eToastPosition.TopCenter)
        '        tbCliente.Focus()
        '        Return
        '    End If

        '    grdetalle.Select()
        '    grdetalle.Col = 4
        '    grdetalle.Row = 0
        'End If
    End Sub
    Private Sub grdetalle_KeyDown(sender As Object, e As KeyEventArgs) Handles grdetalle.KeyDown
        '        If (Not _fnAccesible()) Then
        '            Return
        '        End If
        '        If (e.KeyData = Keys.Enter) Then
        '            Dim f, c As Integer
        '            c = grdetalle.Col
        '            f = grdetalle.Row


        '            grProductos.Focus()
        '            grProductos.MoveTo(grProductos.FilterRow)
        '            grProductos.Col = 2

        '            If (grdetalle.Col = grdetalle.RootTable.Columns("producto").Index) Then
        '                If (grdetalle.GetValue("producto") <> String.Empty) Then
        '                    _prAddDetalleVenta()
        '                    _HabilitarProductos()
        '                Else
        '                    ToastNotification.Show(Me, "Seleccione un Producto Por Favor", My.Resources.WARNING, 3000, eToastGlowColor.Red, eToastPosition.TopCenter)
        '                End If
        '            End If
        '            If (grdetalle.Col = grdetalle.RootTable.Columns("yfcbarra").Index) Then
        '                If (grdetalle.GetValue("yfcbarra").ToString().Trim() <> String.Empty) Then
        '                    cargarProductos()
        '                    If (grdetalle.Row = grdetalle.RowCount - 1) Then
        '                        Dim codigoBarrasCompleto = grdetalle.GetValue("yfcbarra").ToString
        '                        Dim DosPrimerosDigitos As String = Mid(codigoBarrasCompleto, 1, 2)

        '                        If DosPrimerosDigitos = "20" Then
        '                            Dim codigoBarrasProducto As Integer
        '                            Dim PesoGramos As Decimal

        '                            ''CUANDO EL CODIGO DE BARRAS TENGA 7 DIGITOS EJEM: 2000001
        '                            codigoBarrasProducto = Mid(codigoBarrasCompleto, 1, 7)
        '                            If (existeProducto(codigoBarrasProducto)) Then
        '                                PesoGramos = Mid(codigoBarrasCompleto, 8, 5)
        '                                Dim PesoKg As Decimal = PesoGramos / 1000
        '                                Dim resultado As Boolean = False

        '                                If (Not verificarExistenciaUnica(codigoBarrasProducto)) Then
        '                                    ponerProducto3(codigoBarrasProducto, PesoKg, -1, False, resultado)
        '                                    If resultado Then
        '                                        _prAddDetalleVenta()
        '                                    End If
        '                                Else
        '                                    ponerProducto3(codigoBarrasProducto, PesoKg, 0, True, resultado)
        '                                End If
        '                            Else
        '                                grdetalle.DataChanged = False
        '                                ToastNotification.Show(Me, "El código de barra del producto no existe".ToUpper, My.Resources.WARNING, 3000, eToastGlowColor.Red, eToastPosition.TopCenter)
        '                            End If

        '                        Else

        '                            If (existeProducto(grdetalle.GetValue("yfcbarra").ToString)) Then
        '                                If (Not verificarExistenciaUnica(grdetalle.GetValue("yfcbarra").ToString)) Then
        '                                    Dim resultado As Boolean = False
        '                                    ponerProducto(grdetalle.GetValue("yfcbarra").ToString, resultado)
        '                                    If resultado Then
        '                                        _prAddDetalleVenta()
        '                                    End If
        '                                Else
        '                                    'If (grdetalle.GetValue("producto").ToString <> String.Empty) Then
        '                                    sumarCantidad(grdetalle.GetValue("yfcbarra").ToString)
        '                                    'Else
        '                                    '    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
        '                                    '    ToastNotification.Show(Me, "El Producto: NO CUENTA CON STOCK DISPONIBLE", img, 5000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        '                                    '    FilaSelectLote = Nothing
        '                                    'End If
        '                                End If
        '                            Else
        '                                'grdetalle.SetValue("yfcbarra", "")
        '                                grdetalle.DataChanged = False
        '                                ToastNotification.Show(Me, "El código de barra del producto no existe, o no tiene precio, verifique!!!".ToUpper, My.Resources.WARNING, 3700, eToastGlowColor.Red, eToastPosition.TopCenter)
        '                            End If
        '                        End If
        '                    Else
        '                        grdetalle.DataChanged = False
        '                        grdetalle.Row = grdetalle.RowCount - 1
        '                        grdetalle.Col = grdetalle.RootTable.Columns("yfcbarra").Index
        '                        ToastNotification.Show(Me, "El cursor debe situarse en la ultima fila", My.Resources.WARNING, 3000, eToastGlowColor.Red, eToastPosition.TopCenter)
        '                    End If
        '                Else
        '                    ToastNotification.Show(Me, "El código de barra no puede quedar vacio", My.Resources.WARNING, 3000, eToastGlowColor.Red, eToastPosition.TopCenter)
        '                End If
        '            End If
        'salirIf:
        '        End If

        '        If (e.KeyData = Keys.Control + Keys.Enter And grdetalle.Row >= 0 And
        '            grdetalle.Col = grdetalle.RootTable.Columns("producto").Index) Then
        '            Dim indexfil As Integer = grdetalle.Row
        '            Dim indexcol As Integer = grdetalle.Col
        '            _HabilitarProductos()

        '        End If
        '        If (e.KeyData = Keys.Escape And grdetalle.Row >= 0) Then
        '            _prEliminarFila()
        '        End If

    End Sub

    Private Sub cargarProductos()
        Dim dt As DataTable
        dt = L_fnListarProductosSinLoteUltProforma(1, _CodCliente, CType(grdetalle.DataSource, DataTable))
        Table_Producto = dt.Copy
    End Sub
    Private Function existeProducto(codigo As String) As Boolean
        Return (Table_Producto.Select("yfcbarra='" + codigo.Trim() + "'", "").Count > 0)
    End Function
    Private Function verificarExistenciaUnica(codigo As String) As Boolean
        Dim cont As Integer = 0
        'grdetalle.DataChanged = True
        'CType(grdetalle.DataSource, DataTable).AcceptChanges()
        For Each fila As GridEXRow In grdetalle.GetRows()
            If (fila.Cells("yfcbarra").Value.ToString.Trim = codigo.Trim) Then
                cont += 1
            End If
        Next
        Return (cont >= 1)
    End Function


    Private Sub grdetalle_CellValueChanged(sender As Object, e As ColumnActionEventArgs) Handles grdetalle.CellValueChanged

        'If (e.Column.Index = grdetalle.RootTable.Columns("pbcmin").Index) Or (e.Column.Index = grdetalle.RootTable.Columns("pbpbas").Index) Then
        '    If (Not IsNumeric(grdetalle.GetValue("pbcmin")) Or grdetalle.GetValue("pbcmin").ToString = String.Empty) Then

        '        Dim lin As Integer = grdetalle.GetValue("pbnumi")
        '        Dim pos As Integer = -1
        '        _fnObtenerFilaDetalle(pos, lin)
        '        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbcmin") = 1
        '        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbptot") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbpbas")

        '        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbporc") = 0
        '        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbdesc") = 0
        '        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbtotdesc") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbpbas")

        '    Else
        '        If (grdetalle.GetValue("pbcmin") >= 0) Then

        '            Dim rowIndex As Integer = grdetalle.Row
        '            Dim porcdesc As Double = grdetalle.GetValue("pbporc")
        '            Dim montodesc As Double = ((grdetalle.GetValue("pbpbas") * grdetalle.GetValue("pbcmin")) * (porcdesc / 100))
        '            Dim lin As Integer = grdetalle.GetValue("pbnumi")
        '            Dim pos As Integer = -1
        '            _fnObtenerFilaDetalle(pos, lin)


        '            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbcmin") = grdetalle.GetValue("pbcmin")
        '            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbptot") = grdetalle.GetValue("pbpbas") * grdetalle.GetValue("pbcmin")
        '            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbtotdesc") = grdetalle.GetValue("pbpbas") * grdetalle.GetValue("pbcmin")
        '            'CalcularDescuentos(grdetalle.GetValue("pbty5prod"), grdetalle.GetValue("pbcmin"), grdetalle.GetValue("pbpbas"), pos)
        '            P_PonerTotal(rowIndex)
        '        Else
        '            Dim lin As Integer = grdetalle.GetValue("pbnumi")
        '            Dim pos As Integer = -1
        '            _fnObtenerFilaDetalle(pos, lin)
        '            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbcmin") = 1
        '            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbptot") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("pbpbas")
        '            'CalcularDescuentos(grdetalle.GetValue("pbty5prod"), grdetalle.GetValue("pbcmin"), grdetalle.GetValue("pbpbas"), pos)
        '            _prCalcularPrecioTotal()


        '        End If
        '    End If
        'End If



    End Sub


    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click

        If _ValidarCampos() = False Then
            Exit Sub
        End If

        If (tbCodigo.Text = String.Empty) Then

        Else
            If (tbCodigo.Text <> String.Empty) Then
                _prGuardarModificado()
                ''    _prInhabiliitar()
            End If
        End If

    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        If (grVales.RowCount > 0) Then
            _prhabilitar()
            btnNuevo.Enabled = False
            btnModificar.Enabled = False
            btnEliminar.Enabled = False
            btnGrabar.Enabled = True
            PanelNavegacion.Enabled = False
            '_prCargarIconELiminar()

        End If
    End Sub
    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click

        Dim ef = New Efecto
        ef.tipo = 2
        ef.Context = "¿esta seguro de eliminar el registro?".ToUpper
        ef.Header = "mensaje principal".ToUpper
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then
            Dim mensajeError As String = ""
            Dim res As Boolean = L_fnEliminarVale(tbCodigo.Text, tbCodMovimiento.Text, mensajeError, gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina)
            If res Then

                Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
                ToastNotification.Show(Me, "Código de Vale ".ToUpper + tbCodigo.Text + " eliminado con éxito.".ToUpper,
                                          img, 2000,
                                          eToastGlowColor.Green,
                                          eToastPosition.TopCenter)
                _prFiltrar()
            Else
                Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                ToastNotification.Show(Me, mensajeError, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            End If
        End If

    End Sub

    Private Sub grVentas_SelectionChanged(sender As Object, e As EventArgs) Handles grVales.SelectionChanged
        If (grVales.RowCount >= 0 And grVales.Row >= 0) Then
            _prMostrarRegistro(grVales.Row)
        End If
    End Sub

    Private Sub btnSiguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click
        Dim _pos As Integer = grVales.Row
        If _pos < grVales.RowCount - 1 And _pos >= 0 Then
            _pos = grVales.Row + 1
            '' _prMostrarRegistro(_pos)
            grVales.Row = _pos
        End If
    End Sub

    Private Sub btnUltimo_Click(sender As Object, e As EventArgs) Handles btnUltimo.Click
        Dim _pos As Integer = grVales.Row
        If grVales.RowCount > 0 Then
            _pos = grVales.RowCount - 1
            ''  _prMostrarRegistro(_pos)
            grVales.Row = _pos
        End If
    End Sub

    Private Sub btnAnterior_Click(sender As Object, e As EventArgs) Handles btnAnterior.Click
        Dim _MPos As Integer = grVales.Row
        If _MPos > 0 And grVales.RowCount > 0 Then
            _MPos = _MPos - 1
            ''  _prMostrarRegistro(_MPos)
            grVales.Row = _MPos
        End If
    End Sub

    Private Sub btnPrimero_Click(sender As Object, e As EventArgs) Handles btnPrimero.Click
        _PrimerRegistro()
    End Sub
    Private Sub grVentas_KeyDown(sender As Object, e As KeyEventArgs) Handles grVales.KeyDown
        If e.KeyData = Keys.Enter Then
            MSuperTabControl.SelectedTabIndex = 0
            grdetalle.Focus()

        End If
    End Sub
#End Region

    Public Function P_fnImageToByteArray(ByVal imageIn As Image) As Byte()
        Dim ms As New System.IO.MemoryStream()
        imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
        Return ms.ToArray()
    End Function


    Private Sub P_GenerarReporte(TipoRep As Integer, _grabar As Boolean, _numMov As Integer, _nombEmpresa As String)

        'Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)
        Dim dt As DataTable = L_fnDetalleValePorCodigo(tbCodigo.Text)
        'dt = dt.Select("estado=0").CopyToDataTable

        Dim total As Decimal = Convert.ToDecimal(tbtotal.Text)
        Dim totald As Double = (total / 6.96)
        Dim fechaven As String = dtFecha.Value.ToString("dd/MM/yyyy")

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
        IIf(ParteDecimal2.ToString.Equals("0"), "00", ParteDecimal2.ToString) + "/100 Dólares"
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
            ParEmp3 = dt2.Rows(0).Item("Empresa3").ToString
            ParEmp4 = dt2.Rows(0).Item("Empresa4").ToString
        End If

        Dim _Ds3 = L_ObtenerRutaImpresora("2") ' Datos de Impresion de Facturación
        If (_Ds3.Tables(0).Rows(0).Item("cbvp")) Then 'Vista Previa de la Ventana de Vizualización 1 = True 0 = False
            P_Global.Visualizador = New Visualizador 'Comentar
        End If

        Dim objrep As Object = Nothing

        objrep = New R_NotaVenta_7_5X100_2
        F0_VentasSupermercado.SetParametrosNotaVenta(dt, total, li, _Hora, _Ds2, _Ds3, 2, objrep, fechaven, _grabar, _numMov, _nombEmpresa)

    End Sub


    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        If (Not _fnAccesible()) Then
            P_GenerarReporte(2, False, tbCodMovimiento.Text, tbEmpresa.Text)

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

    Private Sub swMostrar_ValueChanged(sender As Object, e As EventArgs)
        _prCargarVale()
    End Sub

    Private Sub tbMontoVale_ValueChanged(sender As Object, e As EventArgs) Handles tbMontoVale.ValueChanged
        If (tbtotal.Value > tbMontoVale.Value) Then
            tbExcedente.Value = tbtotal.Value - tbMontoVale.Value
            tbBeneficio.Value = 0
        ElseIf (tbTotal.Value < tbMontoVale.Value) Then
            tbBeneficio.Value = (tbtotal.Value - tbMontoVale.Value) * (-1)
            tbExcedente.Value = 0
        End If
    End Sub

    Private Sub swMostrar_ValueChanged_1(sender As Object, e As EventArgs) Handles swMostrar.ValueChanged
        _prCargarVale()
    End Sub
End Class