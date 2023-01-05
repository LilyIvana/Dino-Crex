Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls

Public Class Pr_EstadoCuentasProveedores
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
        Me.Text = "REPORTE ESTADO DE CUENTAS DE PROVEEDORES"
        MReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None

    End Sub
    Private Sub P_prArmarAyudaProveedor()

        Dim dt As DataTable
        dt = L_fnListarProveedoresCreditos()
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
        ef.SeleclCol = 1
        ef.listEstCeldas = listEstCeldas
        ef.alto = 50
        ef.ancho = 200
        ef.Context = "Seleccione Proveedor".ToUpper
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then
            Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
            If (IsNothing(Row)) Then
                tbProveedor.Focus()
                Return
            End If
            tbCodigoProveedor.Text = Row.Cells("ydnumi").Value
            tbProveedor.Text = Row.Cells("yddesc").Value
            btnGenerar.Focus()
        End If
    End Sub
    Private Sub _prCargarReporte()
        Try
            Dt1Estado = New DataTable
            Dt2EstadoTotal = New DataTable

            Dt2EstadoTotal = L_prListarEstadoCuentasCompraTotal(tbCodigoProveedor.Text, tbFechaI.Value.ToString("yyyy/MM/dd"))
            Dt1Estado = L_prListarEstadoCuentasCompra(tbCodigoProveedor.Text, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"))

            If (Dt2EstadoTotal.Rows.Count = 0 And Dt1Estado.Rows.Count = 0) Then
                MReportViewer.ReportSource = Nothing
                ToastNotification.Show(Me, "NO HAY DATOS PARA LOS PARAMETROS SELECCIONADOS..!!!",
                           My.Resources.INFORMATION,
                            3500,
                           eToastGlowColor.Blue,
                           eToastPosition.BottomLeft)
                Exit Sub


            Else

                P_ArmarDatos()

                Dim objrep As New R_EstadoCuentasCompra
                objrep.SetDataSource(_dt)
                Dim fechaI As String = tbFechaI.Value.ToString("dd/MM/yyyy")
                Dim fechaF As String = tbFechaF.Value.ToString("dd/MM/yyyy")
                objrep.SetParameterValue("usuario", L_Usuario)
                objrep.SetParameterValue("fechaI", fechaI)
                objrep.SetParameterValue("fechaF", fechaF)
                objrep.SetParameterValue("Proveedor", tbProveedor.Text)
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

        If (Not IsDBNull(Dt2EstadoTotal.Compute("Sum(Importe)", "numiproveedor = " + tbCodigoProveedor.Text + " and concepto = 1"))) Then
            ingT = Dt2EstadoTotal.Compute("Sum(Importe)", "numiproveedor = " + tbCodigoProveedor.Text + " and concepto = 1")
        End If
        If (Not IsDBNull(Dt2EstadoTotal.Compute("Sum(Pagos)", "numiproveedor = " + tbCodigoProveedor.Text + " and concepto = 2"))) Then
            salT = Dt2EstadoTotal.Compute("Sum(Pagos)", "numiproveedor = " + tbCodigoProveedor.Text + " and concepto = 2")
        End If

        saldo = ingT - salT
        Dim ing As Double = 0
        Dim sal As Double = 0
        Dim saldoInicial As Double = 0
        'Sumar Importe
        ing = IIf(IsDBNull(Dt1Estado.Compute("Sum(Importe)", "numiproveedor = " + tbCodigoProveedor.Text + " and concepto = 1")), 0, Dt1Estado.Compute("Sum(Importe)", "numiproveedor = " + tbCodigoProveedor.Text + " and concepto = 1"))
        'Sumar Pagos
        sal = IIf(IsDBNull(Dt1Estado.Compute("Sum(Pagos)", "numiproveedor = " + tbCodigoProveedor.Text + " and concepto = 2")), 0, Dt1Estado.Compute("Sum(Pagos)", "numiproveedor = " + tbCodigoProveedor.Text + " and concepto = 2"))
        'Saldo  a partir de la fecha indicada
        saldoInicial = saldo
        'Insertamos la primera fila con el saldo 
        Dim f As DataRow

        f = Dt1Estado.NewRow
        f.Item(0) = tbCodigoProveedor.Text
        f.Item(1) = tbProveedor.Text
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
    Private Sub btBuscarProveedor_Click(sender As Object, e As EventArgs) Handles btBuscarProveedor.Click
        P_prArmarAyudaProveedor()
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
        If tbProveedor.Text = String.Empty Then
            ToastNotification.Show(Me, "Debe Seleccionar un Proveedor..!!!".ToUpper,
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