
Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports Janus.Windows.GridEX
Imports System.IO
Imports DevComponents.DotNetBar.SuperGrid
Imports DevComponents.DotNetBar.Controls

Public Class F1_ListaClientesCel
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
        Me.Text = "LISTA DE CLIENTES CON CELULAR"


        Dim blah As New Bitmap(New Bitmap(My.Resources.cliente), 20, 20)
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


    Private Sub _prCrearCarpetaReportes()
        Dim rutaDestino As String = RutaGlobal + "\Reporte\Reporte ClienteDino\"

        If System.IO.Directory.Exists(RutaGlobal + "\Reporte\Reporte ClienteDino\") = False Then
            If System.IO.Directory.Exists(RutaGlobal + "\Reporte") = False Then
                System.IO.Directory.CreateDirectory(RutaGlobal + "\Reporte")
                If System.IO.Directory.Exists(RutaGlobal + "\Reporte\Reporte ClienteDino") = False Then
                    System.IO.Directory.CreateDirectory(RutaGlobal + "\Reporte\Reporte ClienteDino")
                End If
            Else
                If System.IO.Directory.Exists(RutaGlobal + "\Reporte\Reporte ClienteDino") = False Then
                    System.IO.Directory.CreateDirectory(RutaGlobal + "\Reporte\Reporte ClienteDino")

                End If
            End If
        End If
    End Sub
    Private Sub _prCargarClientes()
        Dim dt As DataTable
        If swClientes.Value = True Then ''Clientes Regulares del módulo venta rápida
            dt = L_ClientesCel(1)
        Else
            dt = L_ClientesCel(0)
        End If


        If dt.Rows.Count > 0 Then
            JGrM_Buscador.DataSource = dt
            JGrM_Buscador.RetrieveStructure()
            JGrM_Buscador.AlternatingColors = True

            With JGrM_Buscador.RootTable.Columns("Cod")
                .Visible = False
            End With
            With JGrM_Buscador.RootTable.Columns("ydcod")
                .Visible = False
            End With
            With JGrM_Buscador.RootTable.Columns("yddesc")
                .Width = 400
                If swClientes.Value = True Then
                    .Visible = False
                Else
                    .Visible = True
                End If
                .Caption = "CLIENTE"
            End With
            With JGrM_Buscador.RootTable.Columns("nit")
                .Width = 100
                .Visible = True
                .Caption = "CI/NIT"
            End With

            With JGrM_Buscador.RootTable.Columns("complemento")
                .Width = 120
                .Visible = True
                .Caption = "COMPLEMENTO"
            End With

            With JGrM_Buscador.RootTable.Columns("rsocial")
                .Width = 400
                .Caption = "RAZÓN SOCIAL"
                .Visible = True
            End With
            With JGrM_Buscador.RootTable.Columns("cel")
                .Width = 110
                .Caption = "CELULAR"
                .Visible = True
            End With
            With JGrM_Buscador.RootTable.Columns("sanom2")
                .Visible = False
            End With
            With JGrM_Buscador.RootTable.Columns("satipdoc")
                .Visible = False
            End With
            With JGrM_Buscador.RootTable.Columns("sacorreo")
                .Width = 300
                .Caption = "CORREO"
                If swClientes.Value = True Then
                    .Visible = True
                Else
                    .Visible = False
                End If

            End With
            With JGrM_Buscador.RootTable.Columns("cat")
                .Visible = False
            End With

            With JGrM_Buscador
                .DefaultFilterRowComparison = FilterConditionOperator.Contains
                .FilterMode = FilterMode.Automatic
                .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
                .GroupByBoxVisible = False

                'diseño de la grilla
                .RecordNavigator = True
                .RecordNavigatorText = "Datos"

            End With

        Else
            JGrM_Buscador.ClearStructure()
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "No existe datos para mostrar".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.TopCenter)
        End If

    End Sub
#End Region

    Private Sub F1_DesctoProductos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()
    End Sub



    Public Function P_ExportarExcel(_ruta As String, _nombre As String) As Boolean
        Dim _ubicacion As String

        If (1 = 1) Then 'If(_directorio.ShowDialog = Windows.Forms.DialogResult.OK) Then

            _ubicacion = _ruta
            Try
                Dim _stream As Stream
                Dim _escritor As StreamWriter
                Dim _fila As Integer = JGrM_Buscador.GetRows.Length
                Dim _columna As Integer = JGrM_Buscador.RootTable.Columns.Count
                Dim _archivo As String = _ubicacion & "\" & _nombre & "_" & Now.Date.Day &
                    "." & Now.Date.Month & "." & Now.Date.Year & "_" & Now.Hour & "." & Now.Minute & "." & Now.Second & ".csv"
                Dim _linea As String = ""
                Dim _filadata = 0, columndata As Int32 = 0
                File.Delete(_archivo)
                _stream = File.OpenWrite(_archivo)
                _escritor = New StreamWriter(_stream, System.Text.Encoding.UTF8)

                For Each _col As GridEXColumn In JGrM_Buscador.RootTable.Columns
                    If (_col.Visible) Then
                        _linea = _linea & _col.Caption & ";"
                    End If
                Next
                _linea = Mid(CStr(_linea), 1, _linea.Length - 1)
                _escritor.WriteLine(_linea)
                _linea = Nothing

                'Pbx_Precios.Visible = True
                'Pbx_Precios.Minimum = 1
                'Pbx_Precios.Maximum = Dgv_Precios.RowCount
                'Pbx_Precios.Value = 1

                For Each _fil As GridEXRow In JGrM_Buscador.GetRows
                    For Each _col As GridEXColumn In JGrM_Buscador.RootTable.Columns
                        If (_col.Visible) Then
                            Dim data As String = CStr(_fil.Cells(_col.Key).Value)
                            data = data.Replace(";", ",")
                            _linea = _linea & data & ";"
                        End If
                    Next
                    _linea = Mid(CStr(_linea), 1, _linea.Length - 1)
                    _escritor.WriteLine(_linea)
                    _linea = Nothing
                    'Pbx_Precios.Value += 1
                Next
                _escritor.Close()
                'Pbx_Precios.Visible = False
                Try
                    Dim ef = New Efecto
                    ef._archivo = _archivo

                    ef.tipo = 1
                    ef.Context = "Su archivo ha sido Guardado en la ruta: " + _archivo + vbLf + "DESEA ABRIR EL ARCHIVO?"
                    ef.Header = "PREGUNTA"
                    ef.ShowDialog()
                    Dim bandera As Boolean = False
                    bandera = ef.band
                    If (bandera = True) Then
                        Process.Start(_archivo)
                    End If

                    'If (MessageBox.Show("Su archivo ha sido Guardado en la ruta: " + _archivo + vbLf + "DESEA ABRIR EL ARCHIVO?", "PREGUNTA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes) Then
                    '    Process.Start(_archivo)
                    'End If
                    Return True
                Catch ex As Exception
                    MsgBox(ex.Message)
                    Return False
                End Try
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        End If
        Return False
    End Function





    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        _Inter = _Inter + 1
        If _Inter = 1 Then
            Me.StartPosition = FormStartPosition.CenterParent
            Me.WindowState = FormWindowState.Normal
        Else
            Me.Opacity = 100
            Timer1.Enabled = False
        End If
    End Sub

    Private Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        _prCargarClientes()
    End Sub

    Private Sub btnExportarExcel_Click(sender As Object, e As EventArgs) Handles btnExportarExcel.Click
        Dim nombre As String
        _prCrearCarpetaReportes()
        Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
        If swClientes.Value = True Then
            nombre = "ListaClientesRegulares"
        Else
            nombre = "ListaMóduloClientes"
        End If
        If (P_ExportarExcel(RutaGlobal + "\Reporte\Reporte ClienteDino", nombre)) Then
            ToastNotification.Show(Me, "EXPORTACIÓN DE CLIENTES EXITOSA..!!!",
                                       img, 2000,
                                       eToastGlowColor.Green,
                                       eToastPosition.BottomCenter)
        Else
            ToastNotification.Show(Me, "FALLÓ LA EXPORTACIÓN DE CLIENTES..!!!",
                                       My.Resources.WARNING, 2000,
                                       eToastGlowColor.Red,
                                       eToastPosition.BottomLeft)
        End If
    End Sub
End Class