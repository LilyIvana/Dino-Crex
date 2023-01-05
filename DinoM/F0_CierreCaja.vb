Imports Logica.AccesoLogica
Imports Janus.Windows.GridEX
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Imports System.IO
Imports Logica


Public Class F0_CierreCaja

#Region "VARIABLES GLOBALES"


    Dim Bin As New MemoryStream
    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
    Private boShow As Boolean = False
    Private boAdd As Boolean = False
    Private boModif As Boolean = False
    Private boDel As Boolean = False
    Private InDuracion As Byte = 5
    Private dtCortes As DataTable
    Dim _Inter As Integer = 0



#End Region

#Region "METODOS PRIVADOS"
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
    Private Sub MostrarMensajeError(mensaje As String)
        ToastNotification.Show(Me,
                               mensaje.ToUpper,
                               My.Resources.WARNING,
                               4000,
                               eToastGlowColor.Red,
                               eToastPosition.TopCenter)
    End Sub
    Private Sub _Limpiar()
        Try
            TbCodigo.Clear()
            tbFecha.Value = Now.Date
            tbFecha.Focus()
            lbNroCaja.Text = ""


            tbTContado.Value = 0
            tbTPagos.Value = 0
            tbTotalGral.Value = 0
            tbTIngresos.Value = 0
            tbTEgresos.Value = 0
            tbTotalGral.Value = 0

            tbTEfectivo.Value = 0
            tbTDeposito.Value = 0
            tbTTarjeta.Value = 0
            tbTDiferencia.Value = 0
            tbTCredito.Value = 0

            Tb_TipoCambio.Value = 0


            cbTurno.SelectedIndex = 0
            tbMontoInicial.Value = 0
            tbMontoI.Value = 0
            swEstado.Value = True
            tbObservacion.Clear()


            CargarDetalleVentasPagos("01-01-1900", 0)

            _LimpiarGrillas()

        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try

    End Sub
    Private Sub _LimpiarGrillas()
        _prDetalleCortes(-1)
        _prArmarCortes()
        _prDetalleDeposito(-1)
        _prLlenarDepositos(Dgv_Depositos.DataSource)
    End Sub

    Private Sub _prArmarCortes()
        Try
            dtCortes = New DataTable
            dtCortes.Clear()
            _prLlenarCortes(Dgv_Cortes.DataSource, 200, 100)
            _prLlenarCortes(Dgv_Cortes.DataSource, 100, 50)
            _prLlenarCortes(Dgv_Cortes.DataSource, 50, 20)
            _prLlenarCortes(Dgv_Cortes.DataSource, 20, 10)
            _prLlenarCortes(Dgv_Cortes.DataSource, 10, 5)
            _prLlenarCortes(Dgv_Cortes.DataSource, 5, 1)
            _prLlenarCortes(Dgv_Cortes.DataSource, 2, 0)
            _prLlenarCortes(Dgv_Cortes.DataSource, 1, 0)
            _prLlenarCortes(Dgv_Cortes.DataSource, 0.5, 0)
            _prLlenarCortes(Dgv_Cortes.DataSource, 0.2, 0)
            _prLlenarCortes(Dgv_Cortes.DataSource, 0.1, 0)

        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try

    End Sub
    Private Sub _prLlenarCortes(ByRef dtCortes As DataTable, CorteBolivianos As Decimal, CorteDolares As Decimal)
        dtCortes.Rows.Add(0, 0, 1, CorteBolivianos, Convert.ToDecimal(0), Convert.ToDecimal(0), CorteDolares, Convert.ToDecimal(0), Convert.ToDecimal(0), 0)
    End Sub
    Private Sub _prLlenarDepositos(ByRef dtDeposito As DataTable)
        dtDeposito.Rows.Add(0, 0, 1, cbbanco.Text, "Bs", "", DateTime.Today, 0.00, 0)
    End Sub
    Public Sub _prDetalleCortes(IdCaja As Integer)
        Try

            Dim dt As DataTable = L_fnDetalleCortes(IdCaja)

            Dgv_Cortes.BoundMode = Janus.Data.BoundMode.Bound
            Dgv_Cortes.DataSource = dt
            Dgv_Cortes.RetrieveStructure()

            With Dgv_Cortes.RootTable.Columns("cdnumi")
                .Visible = False
            End With

            With Dgv_Cortes.RootTable.Columns("cdccnumi")
                .Visible = False
            End With
            With Dgv_Cortes.RootTable.Columns("cdestado")
                .Visible = False
            End With

            With Dgv_Cortes.RootTable.Columns("cdCorteB")
                .Caption = "CORTE BS."
                .Width = 130
                .FormatString = "0.00"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .Visible = True
                .Position = 3
            End With
            With Dgv_Cortes.RootTable.Columns("cdCantB")
                .Caption = "CANTIDAD BS."
                .Width = 130
                .FormatString = "0"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .Visible = True
                .Position = 4
            End With
            With Dgv_Cortes.RootTable.Columns("cdTotalB")
                .Caption = "TOTAL BS."
                .Width = 170
                .FormatString = "0.00"
                .AggregateFunction = AggregateFunction.Sum
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .Visible = True
                .Position = 5
            End With
            With Dgv_Cortes.RootTable.Columns("cdCorteD")
                .Caption = "CORTE $us."
                .Width = 130
                .FormatString = "0"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .Visible = True
                .Position = 6
            End With

            With Dgv_Cortes.RootTable.Columns("cdCantD")
                .Caption = "CANTIDAD $us"
                .Width = 130
                .FormatString = "0"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .Visible = True
                .Position = 7
            End With
            With Dgv_Cortes.RootTable.Columns("cdTotalD")
                .Caption = "TOTAL $us."
                .Width = 170
                .FormatString = "0.00"
                .AggregateFunction = AggregateFunction.Sum
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .Visible = True
                .Position = 8
            End With
            With Dgv_Cortes.RootTable.Columns("estado")
                .Visible = False
            End With
            With Dgv_Cortes
                .GroupByBoxVisible = False
                'diseño de la grilla
                .TotalRow = InheritableBoolean.True
                .TotalRowFormatStyle.BackColor = Color.Gold
                .TotalRowPosition = TotalRowPosition.BottomFixed
                .VisualStyle = VisualStyle.Office2007

            End With
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try

    End Sub

    Private Sub _prDetalleDeposito(IdCaja As Integer)
        Try
            Dim dtDepositos As DataTable = L_fnDetalleDepositos(IdCaja)

            Dgv_Depositos.BoundMode = Janus.Data.BoundMode.Bound
            Dgv_Depositos.DataSource = dtDepositos
            Dgv_Depositos.RetrieveStructure()
            Dgv_Depositos.AlternatingColors = True

            With Dgv_Depositos.RootTable.Columns("cenumi")
                .Visible = False
            End With
            With Dgv_Depositos.RootTable.Columns("ceccnumi")
                .Visible = False
            End With
            With Dgv_Depositos.RootTable.Columns("ceEstado")
                .Visible = False
            End With
            With Dgv_Depositos.RootTable.Columns("ceBanco")
                .Caption = "BANCO"
                .EditType = EditType.MultiColumnDropDown
                .DropDown = cbbanco.DropDownList
                .Width = 250
                .Visible = True
                .Position = 3
            End With
            With Dgv_Depositos.RootTable.Columns("ceMoneda")
                .Caption = "MONEDA"
                .Width = 100
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = True
                .Position = 4
            End With
            With Dgv_Depositos.RootTable.Columns("ceNDepos")
                .Caption = "NRO. DEPOSITO"
                .Width = 180
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .Visible = True
                .Position = 5
            End With
            With Dgv_Depositos.RootTable.Columns("ceFecha")
                .Caption = "FECHA"
                .Width = 150
                .Visible = True
                .FormatString = "dd/MM/yyyy"

            End With
            With Dgv_Depositos.RootTable.Columns("ceMonto")
                .Caption = "MONTO BS"
                .Width = 150
                .AggregateFunction = AggregateFunction.Sum
                .FormatString = "0.00"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .Visible = True
                .Position = 7
            End With
            With Dgv_Depositos.RootTable.Columns("estado")
                .Visible = False
            End With
            With Dgv_Depositos
                .GroupByBoxVisible = False
                'diseño de la grilla
                .TotalRow = InheritableBoolean.True
                .TotalRowFormatStyle.BackColor = Color.Gold
                .TotalRowPosition = TotalRowPosition.BottomFixed
                .VisualStyle = VisualStyle.Office2007
            End With
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try

    End Sub


    Public Sub CargarDetalleVentasPagos(Fecha As Date, Nrocaja As Integer)
        Try
            Dim dt As New DataTable

            dt = L_fnDetalleVentasPagos(Fecha.ToString("yyyy/MM/dd"), Nrocaja)
            Dgv_VentasPagos.DataSource = dt
            Dgv_VentasPagos.RetrieveStructure()
            Dgv_VentasPagos.AlternatingColors = True

            With Dgv_VentasPagos.RootTable.Columns("tanumi")
                .Width = 130
                .Caption = "Nº VENTA/PAGO"
                .Visible = True
            End With

            With Dgv_VentasPagos.RootTable.Columns("contado")
                .Caption = "CONTADO BS."
                .Width = 120
                .Visible = True
                .FormatString = "0.00"
                .AggregateFunction = AggregateFunction.Sum
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            End With
            With Dgv_VentasPagos.RootTable.Columns("contado$")
                .Caption = "CONTADO $"
                .Width = 120
                .Visible = True
                .FormatString = "0.00"
                .AggregateFunction = AggregateFunction.Sum
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            End With
            With Dgv_VentasPagos.RootTable.Columns("credito")
                .Caption = "CRÉDITO"
                .Width = 120
                .Visible = True
                .FormatString = "0.00"
                .AggregateFunction = AggregateFunction.Sum
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            End With

            With Dgv_VentasPagos.RootTable.Columns("tarjeta")
                .Caption = "TARJETA"
                .Width = 120
                .Visible = True
                .FormatString = "0.00"
                .AggregateFunction = AggregateFunction.Sum
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            End With
            With Dgv_VentasPagos.RootTable.Columns("pagos")
                .Caption = "PAGOS"
                .Width = 120
                .Visible = True
                .FormatString = "0.00"
                .AggregateFunction = AggregateFunction.Sum
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            End With

            With Dgv_VentasPagos.RootTable.Columns("tipocambio")
                .Caption = "TIPO CAMBIO"
                .Width = 200
                .Visible = False
                .FormatString = "0.00"
                .AggregateFunction = AggregateFunction.Sum
            End With
            With Dgv_VentasPagos.RootTable.Columns("totalbs")
                .Caption = "TOTAL VENTA BS"
                .Width = 150
                .Visible = True
                .FormatString = "0.00"
                .AggregateFunction = AggregateFunction.Sum
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            End With
            With Dgv_VentasPagos.RootTable.Columns("total$")
                .Caption = "TOTAL $"
                .Width = 120
                .Visible = False
                .FormatString = "0.00"
                .AggregateFunction = AggregateFunction.Sum
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            End With
            With Dgv_VentasPagos
                .GroupByBoxVisible = False
                'diseño de la grilla
                .VisualStyle = VisualStyle.Office2007
                .TotalRow = InheritableBoolean.True
                .TotalRowFormatStyle.BackColor = Color.Gold
                .TotalRowPosition = TotalRowPosition.BottomFixed

            End With
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try

    End Sub

    Public Sub CargarDetalleVentasPagosPorIdCaja(Fecha As Date, idCaja As String)
        Try
            Dim dt As New DataTable

            dt = L_fnDetalleVentasPagosPorIdCaja(Fecha.ToString("yyyy/MM/dd"), idCaja)
            Dgv_VentasPagos.DataSource = dt
            Dgv_VentasPagos.RetrieveStructure()
            Dgv_VentasPagos.AlternatingColors = True

            With Dgv_VentasPagos.RootTable.Columns("tanumi")
                .Width = 130
                .Caption = "Nº VENTA/PAGO"
                .Visible = True
            End With

            With Dgv_VentasPagos.RootTable.Columns("contado")
                .Caption = "CONTADO BS."
                .Width = 120
                .Visible = True
                .FormatString = "0.00"
                .AggregateFunction = AggregateFunction.Sum
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            End With
            With Dgv_VentasPagos.RootTable.Columns("contado$")
                .Caption = "CONTADO $"
                .Width = 120
                .Visible = True
                .FormatString = "0.00"
                .AggregateFunction = AggregateFunction.Sum
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            End With
            With Dgv_VentasPagos.RootTable.Columns("credito")
                .Caption = "CRÉDITO"
                .Width = 120
                .Visible = True
                .FormatString = "0.00"
                .AggregateFunction = AggregateFunction.Sum
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            End With

            With Dgv_VentasPagos.RootTable.Columns("tarjeta")
                .Caption = "TARJETA"
                .Width = 120
                .Visible = True
                .FormatString = "0.00"
                .AggregateFunction = AggregateFunction.Sum
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            End With
            With Dgv_VentasPagos.RootTable.Columns("pagos")
                .Caption = "PAGOS"
                .Width = 120
                .Visible = True
                .FormatString = "0.00"
                .AggregateFunction = AggregateFunction.Sum
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            End With

            With Dgv_VentasPagos.RootTable.Columns("tipocambio")
                .Caption = "TIPO CAMBIO"
                .Width = 200
                .Visible = False
                .FormatString = "0.00"
                .AggregateFunction = AggregateFunction.Sum
            End With
            With Dgv_VentasPagos.RootTable.Columns("totalbs")
                .Caption = "TOTAL VENTA BS"
                .Width = 150
                .Visible = True
                .FormatString = "0.00"
                .AggregateFunction = AggregateFunction.Sum
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            End With
            With Dgv_VentasPagos.RootTable.Columns("total$")
                .Caption = "TOTAL $"
                .Width = 120
                .Visible = False
                .FormatString = "0.00"
                .AggregateFunction = AggregateFunction.Sum
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            End With
            With Dgv_VentasPagos
                .GroupByBoxVisible = False
                'diseño de la grilla
                .VisualStyle = VisualStyle.Office2007
                .TotalRow = InheritableBoolean.True
                .TotalRowFormatStyle.BackColor = Color.Gold
                .TotalRowPosition = TotalRowPosition.BottomFixed

            End With
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try

    End Sub
    Private Sub _prCargarComboBanco(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Try
            Dim dt As New DataTable
            dt = L_prListarSoloBanco()
            With mCombo
                .DropDownList.Columns.Clear()
                .DropDownList.Columns.Add("yccod3").Width = 70
                .DropDownList.Columns("yccod3").Caption = "COD"
                .DropDownList.Columns.Add("ycdes3").Width = 200
                .DropDownList.Columns("ycdes3").Caption = "DESCRIPCION"
                .ValueMember = "ycdes3"
                .DisplayMember = "ycdes3"
                .DataSource = dt
                .Refresh()
            End With
            cbbanco.SelectedIndex = 0
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try

    End Sub
    Private Sub _prCargarComboLibreria(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo, cod1 As String, cod2 As String)
        Dim dt As New DataTable
        dt = L_prLibreriaClienteLGeneral(cod1, cod2)
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("ycdes3").Width = 200
            .DropDownList.Columns("ycdes3").Caption = "DESCRIPCIÓN"
            .ValueMember = "yccod3"
            .DisplayMember = "ycdes3"
            .DataSource = dt
            .Refresh()
        End With
    End Sub
    Private Sub _prhabilitar()
        TbCodigo.ReadOnly = True
        tbFecha.IsInputReadOnly = False
        tbFecha.Enabled = True
        cbTurno.ReadOnly = False
        tbMontoInicial.IsInputReadOnly = False
        tbObservacion.ReadOnly = False

        btnGrabar.Enabled = True
        'btnCalcular.Enabled = True
    End Sub
    Private Sub _prInhabiliitar()
        Try
            TbCodigo.ReadOnly = True
            tbFecha.IsInputReadOnly = True
            Tb_TipoCambio.IsInputReadOnly = True
            tbFecha.Enabled = False
            cbTurno.ReadOnly = True
            tbMontoInicial.IsInputReadOnly = True
            tbObservacion.ReadOnly = True
            swEstado.IsReadOnly = True

            tbMontoI.IsInputReadOnly = True
            tbTCredito.IsInputReadOnly = True
            tbTTarjeta.IsInputReadOnly = True
            tbTDeposito.IsInputReadOnly = True
            tbTContado.IsInputReadOnly = True
            tbTotalGral.IsInputReadOnly = True
            tbTEfectivo.IsInputReadOnly = True
            tbTDiferencia.IsInputReadOnly = True
            tbTPagos.IsInputReadOnly = True

            btnModificar.Enabled = True
            btnGrabar.Enabled = False
            btnNuevo.Enabled = True
            btnEliminar.Enabled = True

            btnCalcular.Enabled = False
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try

    End Sub
    Private Sub _prCalcular(credito As Double, tipo As Integer)
        Try
            Dim totalCorteDol, totalCorteBol, TotalDeposito, TotalContado, ToTalGral As Double
            totalCorteBol = Dgv_Cortes.GetTotal(Dgv_Cortes.RootTable.Columns("cdTotalB"), AggregateFunction.Sum)
            If tipo = 1 Then
                credito = Dgv_VentasPagos.GetTotal(Dgv_VentasPagos.RootTable.Columns("credito"), AggregateFunction.Sum)
            Else
                credito = credito
            End If
            totalCorteDol = Dgv_Cortes.GetTotal(Dgv_Cortes.RootTable.Columns("cdTotalD"), AggregateFunction.Sum)
            TotalDeposito = Dgv_Depositos.GetTotal(Dgv_Depositos.RootTable.Columns("ceMonto"), AggregateFunction.Sum)
            'TotalContado = Dgv_VentasPagos.GetTotal(Dgv_VentasPagos.RootTable.Columns("totalbs"), AggregateFunction.Sum) - tbTCredito.Text

            tbTEfectivo.Value = totalCorteBol + (totalCorteDol * Tb_TipoCambio.Value)
            tbTDeposito.Value = TotalDeposito

            'ToTalGral = TotalContado + tbMontoI.Value
            'tbTContado.Value = TotalContado - tbTDeposito.Value
            'tbTotalGral.Value = ToTalGral - tbTDeposito.Value

            'tbTDiferencia.Value = tbTEfectivo.Value - tbTContado.Value
            tbTDiferencia.Value = (tbTEfectivo.Value + tbTDeposito.Value + tbTTarjeta.Value) - tbTotalGral.Value
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try

    End Sub

    Public Function _fnAccesible()
        Return tbFecha.IsInputReadOnly = False
    End Function
    Public Function _ValidarCampos() As Boolean
        'If (Tb_TipoCambio.Text <= 0) Then
        '    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
        '    ToastNotification.Show(Me, "Por Favor presione el boton de Calcular Ventas y/o Pagos del día".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        '    Return False
        'End If
        If (tbMontoInicial.Text < 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor introduzca monto inicial".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            Return False
        End If
        If tbTotalGral.Value > 0 And tbTEfectivo.Value = 0 Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor llene la tabla de Cortes".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            Return False
        End If
        Return True
    End Function
    Public Sub _GrabarNuevo()
        Try
            Dim numi As String = ""
            Dim res As Boolean = L_fnGrabarCaja(numi, tbFecha.Value.ToString("yyyy/MM/dd"), tbTotalGral.Value, tbTCredito.Value,
                                                tbTTarjeta.Value, tbTContado.Value, tbTDeposito.Value, tbTEfectivo.Value,
                                                tbTDiferencia.Value, tbTPagos.Value, cbTurno.Text, tbMontoInicial.Value, tbTIngresos.Value, tbTEgresos.Value, IIf(swEstado.Value = True, 1, 0), Tb_TipoCambio.Value, tbObservacion.Text, CType(Dgv_Cortes.DataSource, DataTable), CType(Dgv_Depositos.DataSource, DataTable), gs_NroCaja)
            If res Then

                Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
                ToastNotification.Show(Me, "Código de Cierre de Caja ".ToUpper + numi + " Grabado con éxito.".ToUpper,
                                          img, 2000,
                                          eToastGlowColor.Green,
                                          eToastPosition.TopCenter
                                          )

                _prCargarCierreCaja()
                '_Limpiar()

                _UltimoRegistro()
                _prSalir()

            Else
                Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                ToastNotification.Show(Me, "El Cierre de Caja no pudo ser insertado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub
    Public Sub _prGrabarModificado()
        Try
            Dim ef = New Efecto
            ef.tipo = 2
            ef.Context = "¿esta seguro de cerrar caja?".ToUpper
            ef.Header = "mensaje principal".ToUpper
            ef.ShowDialog()
            Dim bandera As Boolean = False
            bandera = ef.band
            If (bandera = True) Then
                Dim dv As DataView = New DataView(Dgv_VentasPagos.DataSource)
                Dim dtventas As DataTable = dv.ToTable(True, "tanumi", "tipocambio")
                Dim TContado As Double = tbTContado.Value - tbTTarjeta.Value
                Dim res As Boolean = L_fnModificarCaja(TbCodigo.Text, tbFecha.Value.ToString("yyyy/MM/dd"), tbTotalGral.Value, tbTCredito.Value,
                                                    tbTTarjeta.Value, TContado, tbTDeposito.Value, tbTEfectivo.Value,
                                                    tbTDiferencia.Value, tbTPagos.Value, cbTurno.Text, tbMontoInicial.Value, tbTIngresos.Value, tbTEgresos.Value, Tb_TipoCambio.Value, tbObservacion.Text, CType(Dgv_Cortes.DataSource, DataTable), CType(Dgv_Depositos.DataSource, DataTable), dtventas, gs_NroCaja)
                If res Then

                    Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
                    ToastNotification.Show(Me, "Código de Cierre de Caja ".ToUpper + TbCodigo.Text + " Modificado con éxito.".ToUpper,
                                              img, 2000,
                                              eToastGlowColor.Green,
                                              eToastPosition.TopCenter
                                              )

                    _prCargarCierreCaja()

                    _UltimoRegistro()
                    _prSalir()

                Else
                    Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                    ToastNotification.Show(Me, "El Cierre de Caja no pudo ser insertado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                End If

            End If

        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub
    Private Sub _prCargarCierreCaja()
        Try
            Dim dt As New DataTable
            dt = L_prCajaGeneral()
            Dgv_Buscador.DataSource = dt
            Dgv_Buscador.RetrieveStructure()
            Dgv_Buscador.AlternatingColors = True

            'olnumi , olnumichof, chofer, olnumiconci, olfecha, olfact, olhact, oluact
            With Dgv_Buscador.RootTable.Columns("ccnumi")
                .Width = 100
                .Caption = "CODIGO"
                .Visible = True
            End With
            With Dgv_Buscador.RootTable.Columns("ccFecha")
                .Width = 120
                .Visible = True
                .Caption = "FECHA CIERRE"
                .FormatString = "dd/MM/yyyy"
            End With
            With Dgv_Buscador.RootTable.Columns("ccTotalGral")
                .Width = 140
                .Visible = True
                .Caption = "TOTAL CAJA"
                .FormatString = "0.00"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            End With
            With Dgv_Buscador.RootTable.Columns("ccCredito")
                .Width = 130
                .Visible = False
                .FormatString = "0.00"
            End With
            With Dgv_Buscador.RootTable.Columns("ccTarjeta")
                .Width = 130
                .Visible = False
                .FormatString = "0.00"
            End With
            With Dgv_Buscador.RootTable.Columns("ccContadoBs")
                .Width = 130
                .Visible = False
                .FormatString = "0.00"
            End With
            With Dgv_Buscador.RootTable.Columns("ccDepositos")
                .Width = 130
                .Visible = False
                .FormatString = "0.00"
            End With
            With Dgv_Buscador.RootTable.Columns("ccEfectivoBs")
                .Width = 130
                .Visible = False
                .FormatString = "0.00"
            End With
            With Dgv_Buscador.RootTable.Columns("ccDiferencia")
                .Width = 130
                .Visible = False
                .FormatString = "0.00"
            End With
            With Dgv_Buscador.RootTable.Columns("ccPagos")
                .Width = 130
                .Visible = False
                .FormatString = "0.00"
            End With
            With Dgv_Buscador.RootTable.Columns("ccTipoCambio")
                .Width = 100
                .Visible = True
                .Caption = "TIPO CAMBIO"
                .FormatString = "0.00"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            End With
            With Dgv_Buscador.RootTable.Columns("ccTurno")
                .Width = 110
                .Caption = "TURNO"
                .Visible = True
            End With
            With Dgv_Buscador.RootTable.Columns("ccMInicial")
                .Width = 130
                .Visible = False
                .Caption = "MONTO INICIAL"
                .FormatString = "0.00"
            End With
            With Dgv_Buscador.RootTable.Columns("ccIngreso")
                .Width = 130
                .Visible = False
                .Caption = "INGRESOS"
                .FormatString = "0.00"
            End With
            With Dgv_Buscador.RootTable.Columns("ccEgreso")
                .Width = 130
                .Visible = False
                .Caption = "EGRESOS"
                .FormatString = "0.00"
            End With
            With Dgv_Buscador.RootTable.Columns("ccObs")
                .Width = 130
                .Visible = False
            End With
            With Dgv_Buscador.RootTable.Columns("ccEstado")
                .Width = 130
                .Visible = False
            End With
            With Dgv_Buscador.RootTable.Columns("caja")
                .Width = 100
                .Caption = "CAJA"
                .Visible = True
            End With
            With Dgv_Buscador.RootTable.Columns("ccNrocaja")
                .Width = 100
                .Caption = "NRO. CAJA"
                .Visible = True
            End With
            With Dgv_Buscador.RootTable.Columns("ccfact")
                .Width = 150
                .Caption = "FECHA REGISTRO"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .Visible = False
            End With

            With Dgv_Buscador.RootTable.Columns("cchact")
                .Width = 120
                .Caption = "HORA"
                .Visible = True
            End With
            With Dgv_Buscador.RootTable.Columns("ccuact")
                .Width = 150
                .Caption = "USUARIO"
                .Visible = True
            End With

            With Dgv_Buscador
                .DefaultFilterRowComparison = FilterConditionOperator.Contains
                .FilterMode = FilterMode.Automatic
                .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
                .GroupByBoxVisible = False
                'diseño de la grilla
                .VisualStyle = VisualStyle.Office2007
                .AllowEdit = InheritableBoolean.False
            End With

            'If (dt.Rows.Count <= 0) Then
            '    CargarDetalleVentasPagos()
            'End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub
    Public Sub _prFiltrar()
        'cargo el buscador
        Dim _Mpos As Integer
        _prCargarCierreCaja()
        If Dgv_Buscador.RowCount > 0 Then
            _Mpos = 0
            Dgv_Buscador.Row = _Mpos
        Else
            _Limpiar()
            LblPaginacion.Text = "0/0"
        End If
    End Sub
    Public Sub _prMostrarRegistro(_N As Integer)
        Try

            With Dgv_Buscador
                TbCodigo.Text = .GetValue("ccnumi")
                tbFecha.Value = .GetValue("ccfecha")
                Tb_TipoCambio.Value = .GetValue("ccTipoCambio")
                cbTurno.Text = .GetValue("ccTurno")
                tbMontoInicial.Value = .GetValue("ccMInicial")
                tbMontoI.Value = .GetValue("ccMInicial")
                tbTIngresos.Value = .GetValue("ccIngreso")
                tbTEgresos.Value = .GetValue("ccEgreso")
                swEstado.Value = .GetValue("ccEstado")
                tbObservacion.Text = .GetValue("ccObs")
                lbNroCaja.Text = .GetValue("ccNrocaja")

                'Montos del Detalle de Ventas y/o pagos
                tbTCredito.Value = .GetValue("ccCredito")
                tbTTarjeta.Value = .GetValue("ccTarjeta")
                tbTDeposito.Value = .GetValue("ccDepositos")
                tbTContado.Value = .GetValue("ccContadoBs") + .GetValue("ccTarjeta")
                tbTotalGral.Value = .GetValue("ccTotalGral")
                tbTEfectivo.Value = .GetValue("ccEfectivoBs")
                tbTDiferencia.Value = .GetValue("ccDiferencia")
                tbTPagos.Value = .GetValue("ccPagos")

                lbFecha.Text = CType(.GetValue("ccfact"), Date).ToString("dd/MM/yyyy")
                lbHora.Text = .GetValue("cchact").ToString
                lbUsuario.Text = .GetValue("ccuact").ToString

            End With


            _prDetalleCortes(TbCodigo.Text)
            _prDetalleDeposito(TbCodigo.Text)

            If swEstado.Value = True Then
                CargarDetalleVentasPagosPorIdCaja("01-01-1900", 0)
            Else
                CargarDetalleVentasPagosPorIdCaja(tbFecha.Value, TbCodigo.Text)
            End If


            '_prCalcular(tbTCredito.Value, 2)
            LblPaginacion.Text = Str(Dgv_Buscador.Row + 1) + "/" + Dgv_Buscador.RowCount.ToString
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try


    End Sub
    Private Sub _prSalir()
        If btnGrabar.Enabled = True Then
            _prInhabiliitar()
            If Dgv_Buscador.RowCount > 0 Then
                _prMostrarRegistro(0)
            End If
            btnSalir.Text = "SALIR"
        Else
            Me.Close()
            '_modulo.Select()
            '_tab.Close()
        End If
    End Sub
    Public Sub _PrimerRegistro()
        Dim _MPos As Integer
        If Dgv_Buscador.RowCount > 0 Then
            _MPos = 0

            Dgv_Buscador.Row = _MPos
        End If
    End Sub
    Public Sub _AnteriorRegistro()
        Dim _MPos As Integer = Dgv_Buscador.Row
        If _MPos > 0 And Dgv_Buscador.RowCount > 0 Then
            _MPos = _MPos - 1
            Dgv_Buscador.Row = _MPos
        End If
    End Sub
    Public Sub _SiguienteRegistro()
        Dim _pos As Integer = Dgv_Buscador.Row
        If _pos < Dgv_Buscador.RowCount - 1 Then
            _pos = Dgv_Buscador.Row + 1
            Dgv_Buscador.Row = _pos
        End If
    End Sub
    Public Sub _UltimoRegistro()
        Dim _pos As Integer = Dgv_Buscador.Row
        If Dgv_Buscador.RowCount > 0 Then
            _pos = Dgv_Buscador.RowCount - 1
            Dgv_Buscador.Row = _pos
        End If
    End Sub

    Private Sub _prGenerarReporte()
        Try
            Dim dtCortes As DataTable = CType(Dgv_Cortes.DataSource, DataTable)
            If Not IsNothing(P_Global.Visualizador) Then
                P_Global.Visualizador.Close()
            End If

            P_Global.Visualizador = New Visualizador
            Dim objrep As New R_CierreCaja

            objrep.SetDataSource(dtCortes)
            objrep.SetParameterValue("idcaja", TbCodigo.Text)
            objrep.SetParameterValue("fecha", tbFecha.Text)
            objrep.SetParameterValue("turno", cbTurno.Text)
            objrep.SetParameterValue("tipocambio", Tb_TipoCambio.Text)
            objrep.SetParameterValue("usuario", L_Usuario)
            objrep.SetParameterValue("NroCaja", lbNroCaja.Text)

            'Totales
            objrep.SetParameterValue("MInicial", tbMontoI.Text)
            objrep.SetParameterValue("TContadoTarjeta", tbTContado.Text)
            objrep.SetParameterValue("TPagos", tbTPagos.Text)
            objrep.SetParameterValue("TIngresos", tbTIngresos.Text)
            objrep.SetParameterValue("TEgresos", tbTEgresos.Text)
            objrep.SetParameterValue("TotalCaja", tbTotalGral.Text)
            objrep.SetParameterValue("TotalCortes", tbTEfectivo.Text)
            objrep.SetParameterValue("TotalDepositos", tbTDeposito.Text)
            objrep.SetParameterValue("TotalTarjeta", tbTTarjeta.Text)
            objrep.SetParameterValue("Diferencia", tbTDiferencia.Text)

            P_Global.Visualizador.CrGeneral.ReportSource = objrep
            P_Global.Visualizador.Show()
            P_Global.Visualizador.BringToFront()
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try

    End Sub
    Public Sub _fnObtenerFilaDetalle(ByRef pos As Integer, numi As Integer)
        For i As Integer = 0 To CType(Dgv_Cortes.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _numi As Integer = CType(Dgv_Cortes.DataSource, DataTable).Rows(i).Item("cdnumi")
            If (_numi = numi) Then
                pos = i
                Return
            End If
        Next

    End Sub
    Public Sub _fnObtenerFilaDetalleDepositos(ByRef pos As Integer, numi As Integer)
        For i As Integer = 0 To CType(Dgv_Depositos.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _numi As Integer = CType(Dgv_Depositos.DataSource, DataTable).Rows(i).Item("cenumi")
            If (_numi = numi) Then
                pos = i
                Return
            End If
        Next

    End Sub
#End Region

#Region "EVENTOS"
    Private Sub F0_CierreCaja_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Text = "CIERRE DE CAJA"
        Dim blah As New Bitmap(New Bitmap(My.Resources.VENT_PAGOS), 20, 20)
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico
        _prCargarComboBanco(cbbanco)
        _prCargarComboLibreria(cbTurno, 8, 1)
        _prInhabiliitar()
        _prCargarCierreCaja()
        MSuperTabControl.SelectedTabIndex = 0
        _prAsignarPermisos()
        'btnModificar.Visible = False
    End Sub
    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        Try
            _Limpiar()
            _prhabilitar()

            btnNuevo.Enabled = False
            btnModificar.Enabled = False
            btnEliminar.Enabled = False
            btnGrabar.Enabled = True

        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub
    Private Sub Dgv_Cortes_EditingCell(sender As Object, e As EditingCellEventArgs) Handles Dgv_Cortes.EditingCell
        If btnGrabar.Enabled = True Then
            If (e.Column.Index = Dgv_Cortes.RootTable.Columns("cdCantB").Index Or
                e.Column.Index = Dgv_Cortes.RootTable.Columns("cdCantD").Index) Then
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        Else
            e.Cancel = True
        End If
    End Sub
    Private Sub Dgv_VentasPagos_EditingCell(sender As Object, e As EditingCellEventArgs) Handles Dgv_VentasPagos.EditingCell
        e.Cancel = True
    End Sub

    Private Sub btnCalcular_Click(sender As Object, e As EventArgs) Handles btnCalcular.Click
        Try
            Dim dtIngEgre As DataTable
            Dim DtVerificar As DataTable = L_fnVerificarSiExisteCierreCaja(tbFecha.Value, TbCodigo.Text, gs_NroCaja)

            If DtVerificar.Rows.Count > 0 Then
                If DtVerificar.Rows(0).Item("ccEstado") = 0 Then
                    Throw New Exception("Ya existe Cierre de Caja de esta fecha: " + tbFecha.Value)
                Else
                    CargarDetalleVentasPagos(tbFecha.Value, gs_NroCaja)
                    dtIngEgre = L_prIngresoEgresoPorFecha(tbFecha.Value.ToString("yyyy/MM/dd"), gs_NroCaja)

                    If Dgv_VentasPagos.RowCount > 0 Then
                        Tb_TipoCambio.Text = (CType(Dgv_VentasPagos.DataSource, DataTable).Rows(0).Item("tipocambio"))
                        tbTCredito.Text = Dgv_VentasPagos.GetTotal(Dgv_VentasPagos.RootTable.Columns("credito"), AggregateFunction.Sum)
                        tbTTarjeta.Text = Dgv_VentasPagos.GetTotal(Dgv_VentasPagos.RootTable.Columns("tarjeta"), AggregateFunction.Sum)
                        tbTContado.Text = Dgv_VentasPagos.GetTotal(Dgv_VentasPagos.RootTable.Columns("totalbs"), AggregateFunction.Sum) - tbTCredito.Text
                        tbTPagos.Text = Dgv_VentasPagos.GetTotal(Dgv_VentasPagos.RootTable.Columns("pagos"), AggregateFunction.Sum)

                        tbTIngresos.Text = IIf(IsDBNull(dtIngEgre.Compute("Sum(ieMonto)", "ieTipo=1 and ieIdCaja=0")), 0, dtIngEgre.Compute("Sum(ieMonto)", "ieTipo=1 and ieIdCaja=0"))
                        tbTEgresos.Text = IIf(IsDBNull(dtIngEgre.Compute("Sum(ieMonto)", "ieTipo=0 and ieIdCaja=0")), 0, dtIngEgre.Compute("Sum(ieMonto)", "ieTipo=0 and ieIdCaja=0"))

                        tbTotalGral.Text = (tbMontoInicial.Value + tbTContado.Value + tbTPagos.Value + tbTIngresos.Value) - tbTEgresos.Value


                        tbTDeposito.Text = 0
                        tbTEfectivo.Text = 0
                        tbTDiferencia.Text = 0
                        '_LimpiarGrillas()
                    Else
                        Throw New Exception("No existen ventas y/o pagos de esta fecha o Ya existe Cierre de Caja de esta fecha, Verifique")
                    End If
                End If
            Else
                Throw New Exception("No puede calcular ventas y/o pagos  y cerrar caja que abrió con otro Nro. de caja, Verifique")

            End If

        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub

    Private Sub Dgv_Cortes_CellValueChanged(sender As Object, e As ColumnActionEventArgs) Handles Dgv_Cortes.CellValueChanged
        Try
            If (e.Column.Key = "cdCantB" Or e.Column.Key = "cdCantD") Then
                Dim lin As Integer = Dgv_Cortes.GetValue("cdnumi")
                Dim pos As Integer = -1
                _fnObtenerFilaDetalle(pos, lin)

                If (Not IsNumeric(Dgv_Cortes.GetValue("cdCantB")) Or Dgv_Cortes.GetValue("cdCantB").ToString = String.Empty) Then

                    Dgv_Cortes.SetValue("cdCantB", 0)
                    Dgv_Cortes.SetValue("cdTotalB", 0)
                    CType(Dgv_Cortes.DataSource, DataTable).Rows(pos).Item("cdCantB") = 0
                    CType(Dgv_Cortes.DataSource, DataTable).Rows(pos).Item("cdTotalB") = 0

                    _prCalcular(0, 1)
                Else

                    Dim CorteBo, CantidadBo, totalBo As Double
                    'CorteBo = Convert.ToDouble(Dgv_Cortes.CurrentRow.Cells("cdCorteB").Value)
                    CorteBo = Convert.ToDouble(Dgv_Cortes.GetValue("cdCorteB"))
                    CantidadBo = Dgv_Cortes.GetValue("cdCantB")
                    totalBo = CorteBo * CantidadBo
                    'Dgv_Cortes.CurrentRow.Cells("cdTotalB").Value = totalBo
                    Dgv_Cortes.SetValue("cdTotalB", totalBo)

                    CType(Dgv_Cortes.DataSource, DataTable).Rows(pos).Item("cdCantB") = CantidadBo
                    CType(Dgv_Cortes.DataSource, DataTable).Rows(pos).Item("cdTotalB") = totalBo

                    'Dgv_Cortes.UpdateData()
                    _prCalcular(0, 1)
                End If

                If (Not IsNumeric(Dgv_Cortes.GetValue("cdCantD")) Or Dgv_Cortes.GetValue("cdCantD").ToString = String.Empty) Then

                    Dgv_Cortes.SetValue("cdCantD", 0)
                    Dgv_Cortes.SetValue("cdTotalD", 0)

                    _prCalcular(0, 1)
                Else

                    Dim CorteDo, CantidadDo, totalDo As Double

                    'CorteDo = Convert.ToDouble(Dgv_Cortes.CurrentRow.Cells("cdCorteD").Value)
                    CorteDo = Convert.ToDouble(Dgv_Cortes.GetValue("cdCorteD"))
                    CantidadDo = Dgv_Cortes.GetValue("cdCantD")
                    totalDo = CorteDo * CantidadDo
                    'Dgv_Cortes.CurrentRow.Cells("cdTotalD").Value = totalDo
                    Dgv_Cortes.SetValue("cdTotalD", totalDo)

                    CType(Dgv_Cortes.DataSource, DataTable).Rows(pos).Item("cdCantD") = CantidadDo
                    CType(Dgv_Cortes.DataSource, DataTable).Rows(pos).Item("cdTotalD") = totalDo


                    _prCalcular(0, 1)
                End If
            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try

    End Sub

    Private Sub Dgv_Depositos_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgv_Depositos.KeyDown
        Try
            If (_fnAccesible()) Then
                If e.KeyData = Keys.Enter Then
                    If (Dgv_Depositos.Col = Dgv_Depositos.RootTable.Columns("ceBanco").Index) Then
                        Dgv_Depositos.UpdateData()
                        _prLlenarDepositos(Dgv_Depositos.DataSource)
                    End If
                End If
                If e.KeyData = Keys.Escape Then
                    If Dgv_Depositos.RowCount > 1 Then
                        Dim Id = Dgv_Depositos.GetValue("cenumi")
                        Dgv_Depositos.CurrentRow.Delete()
                        Dgv_Depositos.UpdateData()
                        _prCalcular(0, 1)

                    Else
                        Throw New Exception("Detalle de deposito no puede estar vacio")
                    End If
                End If
            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub


    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        If _ValidarCampos() = False Then
            Exit Sub
        End If
        If (TbCodigo.Text = String.Empty) Then
            Dim DtVerificar As DataTable = L_fnVerificarCajaAbierta(gs_NroCaja)
            If DtVerificar.Rows.Count > 0 Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "Existe Cierre de Caja abierta, termine de cerrar para abrir una nueva".ToUpper, img, 3000, eToastGlowColor.Red, eToastPosition.TopCenter)

                Exit Sub
            End If
            _GrabarNuevo()
        Else
            If (TbCodigo.Text <> String.Empty) Then
                _prGrabarModificado()
            End If
        End If
    End Sub
    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _prSalir()
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        Try
            If (Dgv_Buscador.RowCount > 0) Then
                Dim ef = New Efecto
                ef.tipo = 2
                ef.Context = "¿esta seguro de eliminar el registro?".ToUpper
                ef.Header = "mensaje principal".ToUpper
                ef.ShowDialog()
                Dim bandera As Boolean = False
                bandera = ef.band
                If (bandera = True) Then
                    Dim mensajeError As String = ""
                    Dim res As Boolean = L_fnEliminarCaja(TbCodigo.Text, tbFecha.Value)
                    If res Then
                        Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
                        ToastNotification.Show(Me, "Código de Caja ".ToUpper + TbCodigo.Text + " eliminado con Exito.".ToUpper,
                                                  img, 2000,
                                                  eToastGlowColor.Green,
                                                  eToastPosition.TopCenter)
                        _prFiltrar()
                    Else
                        Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                        ToastNotification.Show(Me, mensajeError, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                    End If
                End If
            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub

    Private Sub Dgv_Buscador_DoubleClick(sender As Object, e As EventArgs) Handles Dgv_Buscador.DoubleClick
        If (Dgv_Buscador.Row > -1) Then
            MSuperTabControl.SelectedTabIndex = 0
        End If
    End Sub

    Private Sub Dgv_Buscador_SelectionChanged(sender As Object, e As EventArgs) Handles Dgv_Buscador.SelectionChanged
        If (Dgv_Buscador.RowCount >= 0 And Dgv_Buscador.Row >= 0) Then
            _prMostrarRegistro(Dgv_Buscador.Row)
        End If
    End Sub

    Private Sub Dgv_Depositos_EditingCell(sender As Object, e As EditingCellEventArgs) Handles Dgv_Depositos.EditingCell
        If btnGrabar.Enabled = True Then
            e.Cancel = False
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub Dgv_Cortes_CellEdited(sender As Object, e As ColumnActionEventArgs) Handles Dgv_Cortes.CellEdited
        If btnGrabar.Enabled = True Then
            If (e.Column.Index = Dgv_Cortes.RootTable.Columns("cdCantB").Index Or e.Column.Index = Dgv_Cortes.RootTable.Columns("cdCantD").Index) Then
                If (Dgv_Cortes.GetValue("cdccnumi") <> 0) Then
                    Dgv_Cortes.SetValue("estado", 2)
                End If

            End If
        End If
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        Try
            If swEstado.Value = True Then
                _prhabilitar()
                btnNuevo.Enabled = False
                btnModificar.Enabled = False
                btnEliminar.Enabled = False
                btnGrabar.Enabled = True

                btnCalcular.Enabled = True
                'PanelNavegacion.Enabled = False
            Else
                Throw New Exception("Este cierre de caja no puede modificarse porque ya se encuentra 'Cerrada'")
            End If


        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub

    Private Sub Dgv_Buscador_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgv_Buscador.KeyDown
        If (e.KeyData = Keys.Enter) Then
            MSuperTabControl.SelectedTabIndex = 0
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub btnPrimero_Click(sender As Object, e As EventArgs) Handles btnPrimero.Click
        _PrimerRegistro()
    End Sub

    Private Sub btnAnterior_Click(sender As Object, e As EventArgs) Handles btnAnterior.Click
        _AnteriorRegistro()
    End Sub

    Private Sub btnSiguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click
        _SiguienteRegistro()
    End Sub

    Private Sub btnUltimo_Click(sender As Object, e As EventArgs) Handles btnUltimo.Click
        _UltimoRegistro()
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        If (TbCodigo.Text.Trim <> String.Empty) Then
            _prGenerarReporte()
        End If
    End Sub

    Private Sub Dgv_Depositos_CellValueChanged(sender As Object, e As ColumnActionEventArgs) Handles Dgv_Depositos.CellValueChanged
        If (e.Column.Key = "ceMonto") Then
            Dim lin As Integer = Dgv_Depositos.GetValue("cenumi")
            Dim pos As Integer = -1
            _fnObtenerFilaDetalleDepositos(pos, lin)

            If (Not IsNumeric(Dgv_Depositos.GetValue("ceMonto")) Or Dgv_Depositos.GetValue("ceMonto").ToString = String.Empty) Then

                Dgv_Depositos.SetValue("ceMonto", 0)
                CType(Dgv_Depositos.DataSource, DataTable).Rows(pos).Item("ceMonto") = 0

                _prCalcular(0, 1)
            Else

                Dim Monto As Double

                Monto = Convert.ToDouble(Dgv_Depositos.GetValue("ceMonto"))
                'Dgv_Depositos.SetValue("ceMonto", Monto)
                CType(Dgv_Depositos.DataSource, DataTable).Rows(pos).Item("ceMonto") = Monto

                _prCalcular(0, 1)
            End If

            'Dgv_Depositos.UpdateData()
            '_prCalcular(0, 1)

            If (Dgv_Depositos.GetValue("ceccnumi") <> 0) Then
                Dgv_Depositos.SetValue("estado", 2)
            End If

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




#End Region
End Class