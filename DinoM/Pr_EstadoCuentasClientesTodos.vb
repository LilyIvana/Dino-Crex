Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls

Public Class Pr_EstadoCuentasClientesTodos
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
        ef.SeleclCol = 1
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
            Dim _dt As New DataTable
            _prInterpretarDatos(_dt)

            If (_dt.Rows.Count > 0) Then

                Dim objrep As New R_EstadoCuentasClientesTodos
                objrep.SetDataSource(_dt)
                Dim fechaI As String = tbFechaI.Value.ToString("dd/MM/yyyy")
                Dim fechaF As String = tbFechaF.Value.ToString("dd/MM/yyyy")
                objrep.SetParameterValue("usuario", L_Usuario)

                MReportViewer.ReportSource = objrep
                MReportViewer.Show()
                MReportViewer.BringToFront()
            Else
                ToastNotification.Show(Me, "NO HAY DATOS PARA LOS PARAMETROS SELECCIONADOS..!!!",
                                       My.Resources.INFORMATION, 2000,
                                       eToastGlowColor.Blue,
                                       eToastPosition.BottomLeft)
                MReportViewer.ReportSource = Nothing
            End If

        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub
    Public Sub _prInterpretarDatos(ByRef _dt As DataTable)
        If (swCliente.Value = True) Then
            _dt = L_prListarEstadoCuentasClienteTodos()
        Else
            If (tbCodigoCliente.Text.Length > 0) Then
                _dt = L_prListarEstadoCuentasUnCliente(tbCodigoCliente.Text)
            End If
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
        If swCliente.Value = True Then
            _prCargarReporte()
        Else
            If tbCliente.Text = String.Empty Then
                ToastNotification.Show(Me, "Debe Seleccionar un Cliente..!!!".ToUpper,
                                           My.Resources.INFORMATION, 2000,
                                           eToastGlowColor.Blue,
                                           eToastPosition.TopCenter)
            Else
                _prCargarReporte()
            End If
        End If

    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Close()
    End Sub

    Private Sub swCliente_ValueChanged(sender As Object, e As EventArgs) Handles swCliente.ValueChanged
        If (swCliente.Value = True) Then
            lbcliente.Visible = False
            tbCodigoCliente.Visible = False
            tbCliente.Visible = False
            btBuscarCliente.Visible = False
        Else
            lbcliente.Visible = True
            tbCliente.Visible = True
            btBuscarCliente.Visible = True
        End If
    End Sub

#End Region


End Class