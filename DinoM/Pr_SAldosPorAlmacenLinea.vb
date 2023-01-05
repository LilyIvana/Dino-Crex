Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports System.Data.OleDb
Public Class Pr_SAldosPorAlmacenLinea
    Dim _Inter As Integer = 0


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
    Public Sub _IniciarComponentes()



    End Sub
    Public Sub _prInterpretarDatos(ByRef _dt As DataTable)
        If (CheckTodosAlmacen.Checked And checkTodosGrupos.Checked And CheckMayorCero.Checked) Then

            _dt = L_fnTodosAlmacenTodosLineasMayorCero()


        End If
        If (CheckTodosAlmacen.Checked And checkTodosGrupos.Checked And CheckTodos.Checked) Then

            _dt = L_fnTodosAlmacenTodosLineas()


        End If
        If (checkUnaAlmacen.Checked And checkTodosGrupos.Checked And CheckTodos.Checked) Then
            _dt = L_fnUnaAlmacenTodosLineas(cbAlmacen.Value)
        End If
        'un almacen todos mayor a 0
        If (checkUnaAlmacen.Checked And checkTodosGrupos.Checked And CheckMayorCero.Checked) Then
            _dt = L_fnUnaAlmacenTodosLineasMayorCero(cbAlmacen.Value)
        End If
        If (checkUnaGrupo.Checked And CheckTodosAlmacen.Checked) Then
            _dt = L_fnTodosAlmacenUnaLineas(cbGrupos.Value)

        End If
        If (checkUnaAlmacen.Checked And checkUnaGrupo.Checked And CheckTodos.Checked) Then
            _dt = L_fnUnaAlmacenUnaLineas(cbGrupos.Value, cbAlmacen.Value)
        End If
        ' un almacen una linea y mayor a cero
        If (checkUnaAlmacen.Checked And checkUnaGrupo.Checked And CheckMayorCero.Checked) Then
            _dt = L_fnUnaAlmacenUnaLineasMayorCero(cbGrupos.Value, cbAlmacen.Value)
        End If
    End Sub
    Private Sub _prCargarReporte()
        Dim _dt As New DataTable
        _prInterpretarDatos(_dt)
        If (_dt.Rows.Count > 0) Then

            Dim objrep As New R_SaldosPorLinea
            objrep.SetDataSource(_dt)

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





    End Sub
    'Private Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
    '    '_prCargarReporte()
    '    Try

    '        Dim dt As DataTable = GetDataExcel( _
    '             "C:\Users\usuario\Google Drive\INFORMACION MARCO ANTONIO\Dinases\Base de Datos\Dino M\Fcia 10102017\Clientes.xlsx", "Hoja1")

    '        If (dt.Rows.Count > 0) Then

    '            For i As Integer = 0 To dt.Rows.Count - 1 Step 1
    '                Dim CodigoCliente As String = dt.Rows(i).Item(0)
    '                Dim RazonSocial As String = IIf(IsDBNull(dt.Rows(i).Item(1)), "", dt.Rows(i).Item(1))
    '                Dim Direccion As String = IIf(IsDBNull(dt.Rows(i).Item(2)), "", dt.Rows(i).Item(2))
    '                Dim Telefono As String = IIf(IsDBNull(dt.Rows(i).Item(3)), "", dt.Rows(i).Item(3))
    '                Dim nombre As String = IIf(IsDBNull(dt.Rows(i).Item(4)), "", dt.Rows(i).Item(4))
    '                Dim Telefono1 As String = ""
    '                Dim Telefono2 As String = ""
    '                If (Telefono.Contains("-")) Then
    '                    Dim index As Integer = Telefono.IndexOf("-")
    '                    Telefono1 = Telefono.Substring(0, index)
    '                    Telefono2 = Telefono.Substring(index + 1)


    '                Else
    '                    Telefono1 = Telefono
    '                End If

    '                Dim res As Boolean = L_fnGrabarCLiente("", CodigoCliente, RazonSocial, nombre, 0, 1, 1, "", Direccion, Telefono1, Telefono2, 70, 1, 0, 0, "", "2017/01/01", "", 1, "", Now.Date.ToString("yyyy/MM/dd"), Now.Date.ToString("yyyy/MM/dd"), "", 1)


    '                If res = False Then
    '                    MsgBox("Pos" + Str(i))

    '                End If
    '            Next


    '        End If


    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)

    '    End Try
    'End Sub


    Private Sub Pr_VentasAtendidas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()

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
    Private Sub _prCargarComboLibreriaPrecioCosto(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_prListarPrecioCosto()
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("ygnumi").Width = 60
            .DropDownList.Columns("ygnumi").Caption = "COD"
            .DropDownList.Columns.Add("ygdesc").Width = 500
            .DropDownList.Columns("ygdesc").Caption = "SUCURSAL"
            .ValueMember = "ygnumi"
            .DisplayMember = "ygdesc"
            .DataSource = dt
            .Refresh()
        End With
        If (dt.Rows.Count > 0) Then
            cbGrupos.SelectedIndex = 0
        End If
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click

        Me.Close()

    End Sub

    Private Sub swTipoVenta_ValueChanged(sender As Object, e As EventArgs)
        If (bandera = False) Then
            Return
        End If
        'If (swTipoVenta.Value = True) Then
        '    _prCargarComboLibreriaPrecioVenta(cbGrupos)
        'Else
        '    _prCargarComboLibreriaPrecioCosto(cbGrupos)

        'End If

    End Sub

    Sub _prInhabilitarAlmacen()
        cbAlmacen.Enabled = False
    End Sub
    Sub _prhabilitarAlmacen()
        cbAlmacen.Enabled = True
    End Sub

    Sub _prInhabilitarGrupos()
        cbGrupos.Enabled = False
    End Sub
    Sub _prhabilitarGrupos()
        cbGrupos.Enabled = True
    End Sub
    Private Sub CheckTodosVendedor_CheckValueChanged(sender As Object, e As EventArgs) Handles CheckTodosAlmacen.CheckValueChanged
        If (CheckTodosAlmacen.Checked) Then
            _prInhabilitarAlmacen()
        Else
            _prhabilitarAlmacen()
        End If

    End Sub
    'grup panel stock mayor a cero o todos


    Private Sub checkTodosGrupos_CheckValueChanged(sender As Object, e As EventArgs) Handles checkTodosGrupos.CheckValueChanged
        If (checkTodosGrupos.Checked) Then
            _prInhabilitarGrupos()
        Else
            _prhabilitarGrupos()
        End If
    End Sub

    Private Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        _prCargarReporte()
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
End Class