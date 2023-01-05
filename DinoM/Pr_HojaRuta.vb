Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls

Public Class Pr_HojaRuta

    'gb_FacturaIncluirICE

    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem

    Public Sub _prIniciarTodo()
        _prCargarComboLibreriaMeses(cbZona)
        _PMIniciarTodo()
        'L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        Me.Text = "REPORTE HOJA DE RUTA"
        MReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        _IniciarComponentes()
        cbZona.Value = Now.Month
    End Sub
    Private Sub _prCargarComboLibreriaMeses(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_prLibreriaClienteLGeneralMeses()
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("yccod3").Width = 60
            .DropDownList.Columns("yccod3").Caption = "COD"
            .DropDownList.Columns.Add("ycdes3").Width = 500
            .DropDownList.Columns("ycdes3").Caption = "DESCRIPCION"
            .ValueMember = "yccod3"
            .DisplayMember = "ycdes3"
            .DataSource = dt
            .Refresh()
        End With
    End Sub
    Public Sub _IniciarComponentes()
        tbVendedor.Focus()
    End Sub
    Public Sub _prPonerIndiceReporte(ByRef _dt As DataTable)
        For i As Integer = 0 To _dt.Rows.Count - 1 Step 1
            _dt.Rows(i).Item("numi") = i + 1
        Next
    End Sub
    Public Sub _prObtenerPrimerDiaMes(ByRef _primerDia As Integer)
        Dim Primer As Date
        Dim Ultimo As Date
        Dim fecha As Date = DateSerial(Now.Year, cbZona.Value, 15)
        'Usamos la funcion DAteSerial para obtener el primero y el ultimo dia  
        Primer = DateSerial(Year(fecha), Month(fecha) + 0, 1)
        Dim primerdia As Integer = Primer.Day
        ''Aqui me obtiene 1=domingo 2=lunes 3=martes 4=miercoles
        Ultimo = DateSerial(Year(fecha), Month(fecha) + 1, 0)
        Dim ult As Integer = Ultimo.Day
        Dim b As Boolean = False
        While (primerdia <= ult And b = False)
            Dim numero As Integer = Weekday(DateSerial(Now.Year, cbZona.Value, primerdia))
            If (numero <> 1) Then
                If (primerdia + 28 <= ult) Then
                    b = True
                    _primerDia = primerdia
                End If
            End If
            primerdia += 1
        End While




    End Sub
    Public Sub _prInterpretarDatos(ByRef _dt As DataTable)
        If (tbCodigoVendedor.Text.Length > 0) Then
            _dt = L_fnReporteRutasClientes(tbCodigoVendedor.Text)
            _prPonerIndiceReporte(_dt)

        Else
            _dt = L_fnReporteRutasClientes("-1")
        End If
    End Sub
    Public Function _prObtenerDay(dia As Integer) As String
        If (dia < 10) Then
            Return "0" + Str(dia).Trim
        End If
        Return Str(dia).Trim
    End Function
    Public Function fn_ObtenerZonas() As String
        Dim dt As DataTable = L_fnReporteZonasVendedor(tbCodigoVendedor.Text)
        Dim zona As String = ""
        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
            Dim z As String = dt.Rows(i).Item("zona")
            If (i = 0) Then
                zona = zona + z
            Else
                zona = zona + " - " + z
            End If
        Next
        Return zona
    End Function
    Private Sub _prCargarReporte()
        Dim _dt As New DataTable
        Dim _primerDia As Integer
        _prObtenerPrimerDiaMes(_primerDia)

        _prInterpretarDatos(_dt)
        If (_dt.Rows.Count > 0) Then


            Dim zonas As String = fn_ObtenerZonas()

            Dim objrep As New R_HojaRuta
            objrep.SetDataSource(_dt)
            'Dim fechaI As String = tbFechaI.Value.ToString("dd/MM/yyyy")
            objrep.SetParameterValue("zona", zonas)
            objrep.SetParameterValue("vendedor", tbVendedor.Text)
            objrep.SetParameterValue("mes", cbZona.Text + Str(Now.Date.Year))
            objrep.SetParameterValue("semana1", _prObtenerDay(_primerDia) + "/" + Str(cbZona.Value).Trim + "/" + Str(Now.Year).Trim)

            objrep.SetParameterValue("semana2", _prObtenerDay(_primerDia + 7) + "/" + Str(cbZona.Value).Trim + "/" + Str(Now.Year).Trim)

            objrep.SetParameterValue("semana3", Str(_primerDia + 14) + "/" + Str(cbZona.Value).Trim + "/" + Str(Now.Year).Trim)
            objrep.SetParameterValue("semana4", Str(_primerDia + 21) + "/" + Str(cbZona.Value).Trim + "/" + Str(Now.Year).Trim)
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
    End Sub
    Private Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        _prCargarReporte()

    End Sub

    Private Sub Pr_VentasAtendidas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()

    End Sub






    Private Sub tbVendedor_KeyDown_1(sender As Object, e As KeyEventArgs) Handles tbVendedor.KeyDown

        If e.KeyData = Keys.Control + Keys.Enter Then
            Dim dt As DataTable
            dt = L_fnListarEmpleado()
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
            ef.ancho = 350
            ef.Context = "Seleccione Vendedor".ToUpper
            ef.ShowDialog()
            Dim bandera As Boolean = False
            bandera = ef.band
            If (bandera = True) Then
                Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
                If (IsNothing(Row)) Then
                    tbVendedor.Focus()
                    Return
                End If
                tbCodigoVendedor.Text = Row.Cells("ydnumi").Value
                tbVendedor.Text = Row.Cells("yddesc").Value
                btnGenerar.Focus()
            End If

        End If

    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click

        '  Public _modulo As SideNavItem
        _modulo.Select()
        _tab.Close()

    End Sub
End Class