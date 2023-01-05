Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports System.Data.OleDb
Public Class Pr_StockMinimo
    Public _nameButton As String
    Public _tab As SuperTabItem
    Dim bandera As Boolean = False

    Private Function GetDataExcel(
    ByVal fileName As String, ByVal sheetName As String) As DataTable

        ' Comprobamos los parámetros.
        '
        If ((String.IsNullOrEmpty(fileName)) OrElse
          (String.IsNullOrEmpty(sheetName))) Then _
          Throw New ArgumentNullException()

        Try
            Dim extension As String = IO.Path.GetExtension(fileName)

            Dim connString As String = "Data Source=" & fileName

            If (extension = ".xls") Then
                connString &= ";Provider=Microsoft.Jet.OLEDB.4.0;" &
                       "Extended Properties='Excel 8.0;HDR=YES;IMEX=1'"

            ElseIf (extension = ".xlsx") Then
                connString &= ";Provider=Microsoft.ACE.OLEDB.12.0;" &
                       "Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1'"
            Else
                Throw New ArgumentException(
                  "La extensión " & extension & " del archivo no está permitida.")
            End If

            Using conexion As New OleDbConnection(connString)

                Dim sql As String = "SELECT * FROM [" & sheetName & "$]"
                Dim adaptador As New OleDbDataAdapter(sql, conexion)

                Dim dt As New DataTable("Excel")

                adaptador.Fill(dt)

                Return dt

            End Using

        Catch ex As Exception
            Throw

        End Try

    End Function
    Private Sub Pr_StockMinimo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()
    End Sub

    Public Sub _prIniciarTodo()
        'L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        _prCargarComboLibreriaSucursal(cbAlmacen)
        _prCargarComboGrupos(cbGrupos)
        _PMIniciarTodo()
        Me.Text = "SALDOS DE PRODUCTOS"
        MReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        _IniciarComponentes()
        bandera = True
    End Sub


    Private Sub _prCargarComboGrupos(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnObtenerGruposLibreria()
        'a.ylcod2,yldes2
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("yccod3").Width = 60
            .DropDownList.Columns("yccod3").Caption = "COD"
            .DropDownList.Columns.Add("yldes2").Width = 250
            .DropDownList.Columns("yldes2").Caption = "GRUPOS"
            .ValueMember = "yccod3"
            .DisplayMember = "yldes2"
            .DataSource = dt
            .Refresh()
        End With
        If (dt.Rows.Count > 0) Then
            cbGrupos.SelectedIndex = 0
        End If
    End Sub
    Public Sub _PMIniciarTodo()

        'TxtNombreUsu.Text = MGlobal.gs_usuario
        'TxtNombreUsu.ReadOnly = True

        'Me.WindowState = FormWindowState.Maximized
        'Me.SupTabItemBusqueda.Visible = False

    End Sub
    Public Sub _IniciarComponentes()

    End Sub

    Private Sub _prCargarComboLibreriaSucursal(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnListarSucursales()
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("aanumi").Width = 60
            .DropDownList.Columns("aanumi").Caption = "COD"
            .DropDownList.Columns.Add("aabdes").Width = 300
            .DropDownList.Columns("aabdes").Caption = "SUCURSAL"
            .ValueMember = "aanumi"
            .DisplayMember = "aabdes"
            .DataSource = dt
            .Refresh()
        End With
        If (dt.Rows.Count > 0) Then
            cbAlmacen.SelectedIndex = 0
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btn_Generar.Click
        _prCargarReporte()
    End Sub

    Private Sub _prCargarReporte()
        Dim _dt As New DataTable
        _prInterpretarDatos(_dt)
        If (_dt.Rows.Count > 0) Then

            Dim objrep As New R_StockMinimo
            objrep.SetDataSource(_dt)

            objrep.SetParameterValue("usuario", L_Usuario)
            objrep.SetParameterValue("almacen", cbAlmacen.Text)
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

    Public Sub _prInterpretarDatos(ByRef _dt As DataTable)
        If (CheckTodosAlmacen.Checked And checkTodosGrupos.Checked) Then

            _dt = L_fnTodosAlmacenTodosLineasMenoresStock()


        End If
        'If (CheckTodosAlmacen.Checked And checkTodosGrupos.Checked) Then

        '    _dt = L_fnTodosAlmacenTodosLineas()


        'End If
        If (checkUnaAlmacen.Checked And checkTodosGrupos.Checked) Then
            _dt = L_fnUnaAlmacenTodosLineasMenoresStock(cbAlmacen.Value)
        End If
        'un almacen todos mayor a 0
        'If (checkUnaAlmacen.Checked And checkTodosGrupos.Checked) Then
        '    _dt = L_fnUnaAlmacenTodosLineasMayorCero(cbAlmacen.Value)
        'End If
        If (checkUnaGrupo.Checked And CheckTodosAlmacen.Checked) Then
            _dt = L_fnTodosAlmacenUnaLineasMenoresStock(cbGrupos.Value)

        End If
        If (checkUnaAlmacen.Checked And checkUnaGrupo.Checked) Then
            _dt = L_fnUnaAlmacenUnaLineasMenoresStock(cbGrupos.Value, cbAlmacen.Value)
        End If
        ' un almacen una linea y mayor a cero
        'If (checkUnaAlmacen.Checked And checkUnaGrupo.Checked) Then
        '    _dt = L_fnUnaAlmacenUnaLineasMayorCero(cbGrupos.Value, cbAlmacen.Value)
        'End If
    End Sub

    Private Sub btn_Salir_Click(sender As Object, e As EventArgs) Handles btn_Salir.Click
        _tab.Close()
    End Sub

    Private Sub checkUnaAlmacen_CheckedChanged(sender As Object, e As EventArgs) Handles checkUnaAlmacen.CheckedChanged
        cbAlmacen.ReadOnly = False
    End Sub

    Private Sub checkUnaGrupo_CheckedChanged(sender As Object, e As EventArgs) Handles checkUnaGrupo.CheckedChanged
        cbGrupos.ReadOnly = False
    End Sub

    Private Sub CheckTodosAlmacen_CheckedChanged(sender As Object, e As EventArgs) Handles CheckTodosAlmacen.CheckedChanged

        cbAlmacen.ReadOnly = True
    End Sub

    Private Sub checkTodosGrupos_CheckedChanged(sender As Object, e As EventArgs) Handles checkTodosGrupos.CheckedChanged
        cbGrupos.ReadOnly = True
    End Sub
End Class