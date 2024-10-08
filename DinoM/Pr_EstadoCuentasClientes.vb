﻿Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls

Public Class Pr_EstadoCuentasClientes
#Region "VARIABLES GLOBALES"
    Dim _Inter As Integer = 0

    Public _nameButton As String
    Public _tab As SuperTabItem
    Dim titulo As String = ""
    Public _modulo As SideNavItem
    Dim Dt1Estado As DataTable
    Dim Dt2EstadoTotal As DataTable
    Dim _dt As New DataTable

#End Region

#Region "METODOS PRIVADOS"
    Public Sub _prIniciarTodo()
        tbFechaI.Value = Now.Date
        tbFechaF.Value = Now.Date
        'Me.WindowState = FormWindowState.Maximized
        Me.Text = "REPORTE ESTADO DE CUENTAS DE CLIENTES"
        MReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None

    End Sub
    Private Sub P_prArmarAyudaCliente()

        Dim dt As DataTable
        dt = L_fnListarClienteCreditos()
        '              a.ydnumi, a.ydcod, a.yddesc, a.yddctnum, a.yddirec
        ',a.ydtelf1 ,a.ydfnac 
        Dim listEstCeldas As New List(Of Modelo.Celda)
        listEstCeldas.Add(New Modelo.Celda("ydnumi,", False, "ID", 50))
        listEstCeldas.Add(New Modelo.Celda("ydcod", True, "ID", 50))
        listEstCeldas.Add(New Modelo.Celda("yddesc", True, "NOMBRE", 280))
        listEstCeldas.Add(New Modelo.Celda("yddctnum", True, "N. Documento".ToUpper, 150))
        listEstCeldas.Add(New Modelo.Celda("yddirec", True, "DIRECCION", 220))
        listEstCeldas.Add(New Modelo.Celda("ydtelf1", True, "Telefono".ToUpper, 200))
        listEstCeldas.Add(New Modelo.Celda("ydfnac", True, "F.Nacimiento".ToUpper, 150, "MM/dd,YYYY"))
        Dim ef = New Efecto
        ef.tipo = 3
        ef.dt = dt
        ef.SeleclCol = 2
        ef.listEstCeldas = listEstCeldas
        ef.alto = 50
        ef.ancho = 220
        ef.Context = "Seleccione Cliente".ToUpper
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then
            Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
            If (IsNothing(Row)) Then
                tbCliente.Focus()
                Return
            End If
            tbCodigoCliente.Text = Row.Cells("ydnumi").Value
            tbCliente.Text = Row.Cells("yddesc").Value
            btnGenerar.Focus()
        End If
    End Sub
    Private Sub _prCargarReporte()
        Try
            Dt1Estado = New DataTable
            Dt2EstadoTotal = New DataTable

            Dt2EstadoTotal = L_prListarEstadoCuentasClientesTotal(tbCodigoCliente.Text, tbFechaI.Value.ToString("yyyy/MM/dd"))
            Dt1Estado = L_prListarEstadoCuentasCliente(tbCodigoCliente.Text, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"))

            If (Dt2EstadoTotal.Rows.Count = 0 And Dt1Estado.Rows.Count = 0) Then
                MReportViewer.ReportSource = Nothing
                ToastNotification.Show(Me, "NO HAY DATOS PARA LOS PARAMETROS SELECCIONADOS..!!!",
                           My.Resources.INFORMATION,
                            3500,
                           eToastGlowColor.Blue,
                           eToastPosition.TopCenter)
                Exit Sub
            Else

                P_ArmarDatos()

                Dim objrep As New R_EstadoCuentasClientes
                objrep.SetDataSource(_dt)
                L_fnBotonGenerar(gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina, IIf(tbCodigoCliente.Text = String.Empty, 0, tbCodigoCliente.Text), "ESTADO CUENTAS CLIENTES", "ESTADO CUENTAS CLIENTES")

                Dim fechaI As String = tbFechaI.Value.ToString("dd/MM/yyyy")
                Dim fechaF As String = tbFechaF.Value.ToString("dd/MM/yyyy")
                objrep.SetParameterValue("usuario", L_Usuario)
                objrep.SetParameterValue("fechaI", fechaI)
                objrep.SetParameterValue("fechaF", fechaF)
                objrep.SetParameterValue("Cliente", tbCliente.Text)
                MReportViewer.ReportSource = objrep
                MReportViewer.Show()
                MReportViewer.BringToFront()

            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub
    Private Sub P_ArmarDatos()

        Dim saldo As Double = 0
        Dim ingT As Double = 0
        Dim salT As Double = 0

        If (Not IsDBNull(Dt2EstadoTotal.Compute("Sum(Importe)", "numicliente = " + tbCodigoCliente.Text + " and concepto = 1"))) Then
            ingT = Dt2EstadoTotal.Compute("Sum(Importe)", "numicliente = " + tbCodigoCliente.Text + " and concepto = 1")
        End If
        If (Not IsDBNull(Dt2EstadoTotal.Compute("Sum(Pagos)", "numicliente = " + tbCodigoCliente.Text + " and concepto = 2"))) Then
            salT = Dt2EstadoTotal.Compute("Sum(Pagos)", "numicliente = " + tbCodigoCliente.Text + " and concepto = 2")
        End If

        saldo = ingT - salT
        Dim ing As Double = 0
        Dim sal As Double = 0
        Dim saldoInicial As Double = 0
        'Sumar Importe
        ing = IIf(IsDBNull(Dt1Estado.Compute("Sum(Importe)", "numicliente = " + tbCodigoCliente.Text + " and concepto = 1")), 0, Dt1Estado.Compute("Sum(Importe)", "numicliente = " + tbCodigoCliente.Text + " and concepto = 1"))
        'Sumar Pagos
        sal = IIf(IsDBNull(Dt1Estado.Compute("Sum(Pagos)", "numicliente = " + tbCodigoCliente.Text + " and concepto = 2")), 0, Dt1Estado.Compute("Sum(Pagos)", "numicliente = " + tbCodigoCliente.Text + " and concepto = 2"))
        'Saldo  a partir de la fecha indicada
        saldoInicial = saldo
        'Insertamos la primera fila con el saldo 
        Dim f As DataRow

        f = Dt1Estado.NewRow
        f.Item(0) = tbCodigoCliente.Text
        f.Item(1) = tbCliente.Text
        f.Item(2) = ""
        f.Item(3) = 0
        f.Item(4) = "SALDO ANTERIOR"
        f.Item(5) = ""
        f.Item(6) = 0
        f.Item(7) = 0
        f.Item(8) = saldoInicial
        f.Item(9) = 0
        f.Item(10) = ""


        Dt1Estado.Rows.InsertAt(f, 0)

        For Each fil As DataRow In Dt1Estado.Rows
            Dim s As String = fil.Item("concepto").ToString
            If (fil.Item("concepto").ToString.Equals("1")) Then
                saldoInicial = saldoInicial + CDbl(fil.Item("Importe"))
                fil.Item("Saldos") = saldoInicial
            ElseIf (fil.Item("concepto").ToString.Equals("2")) Then
                saldoInicial = saldoInicial - CDbl(fil.Item("Pagos"))
                fil.Item("Saldos") = saldoInicial
            End If
        Next

        _dt = Dt1Estado
    End Sub
    Private Sub MostrarMensajeError(mensaje As String)
        ToastNotification.Show(Me,
                               mensaje.ToUpper,
                               My.Resources.WARNING,
                               4000,
                               eToastGlowColor.Red,
                               eToastPosition.TopCenter)

    End Sub
#End Region
#Region "EVENTOS"
    Private Sub Pr_EstadoCuentasProveedores_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()
    End Sub
    Private Sub btBuscarCliente_Click(sender As Object, e As EventArgs) Handles btBuscarCliente.Click
        P_prArmarAyudaCliente()
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
        If tbCliente.Text = String.Empty Then
            ToastNotification.Show(Me, "Debe Seleccionar un Cliente..!!!".ToUpper,
                                       My.Resources.INFORMATION, 2000,
                                       eToastGlowColor.Blue,
                                       eToastPosition.TopCenter)
        Else
            _prCargarReporte()
        End If

    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Close()
    End Sub

#End Region


End Class