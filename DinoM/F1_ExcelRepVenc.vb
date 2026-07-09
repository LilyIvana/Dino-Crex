
Imports System.IO
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Imports Janus.Windows.GridEX
Imports Logica.AccesoLogica

Public Class F1_ExcelRepVenc
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
        Me.Text = "REPORTE VENCIMIENTOS"
        tbFechaI.Value = Now.Date
        tbFechaF.Value = Now.Date

        Dim blah As New Bitmap(New Bitmap(My.Resources.producto), 20, 20)
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
    Private Sub _prCrearCarpetaImagenes()
        Dim rutaDestino As String = RutaGlobal + "\Imagenes\Imagenes ProductoDino\"

        If System.IO.Directory.Exists(RutaGlobal + "\Imagenes\Imagenes ProductoDino\") = False Then
            If System.IO.Directory.Exists(RutaGlobal + "\Imagenes") = False Then
                System.IO.Directory.CreateDirectory(RutaGlobal + "\Imagenes")
                If System.IO.Directory.Exists(RutaGlobal + "\Imagenes\Imagenes ProductoDino") = False Then
                    System.IO.Directory.CreateDirectory(RutaGlobal + "\Imagenes\Imagenes ProductoDino")
                End If
            Else
                If System.IO.Directory.Exists(RutaGlobal + "\Imagenes\Imagenes ProductoDino") = False Then
                    System.IO.Directory.CreateDirectory(RutaGlobal + "\Imagenes\Imagenes ProductoDino")

                End If
            End If
        End If
    End Sub

    Private Sub _prCrearCarpetaReportes()
        Dim rutaDestino As String = RutaGlobal + "\Reporte\Reporte Productos\"

        If System.IO.Directory.Exists(RutaGlobal + "\Reporte\Reporte Productos\") = False Then
            If System.IO.Directory.Exists(RutaGlobal + "\Reporte") = False Then
                System.IO.Directory.CreateDirectory(RutaGlobal + "\Reporte")
                If System.IO.Directory.Exists(RutaGlobal + "\Reporte\Reporte Productos") = False Then
                    System.IO.Directory.CreateDirectory(RutaGlobal + "\Reporte\Reporte Productos")
                End If
            Else
                If System.IO.Directory.Exists(RutaGlobal + "\Reporte\Reporte Productos") = False Then
                    System.IO.Directory.CreateDirectory(RutaGlobal + "\Reporte\Reporte Productos")

                End If
            End If
        End If
    End Sub
    Private Sub _prCargar()
        Dim fechaDesde As DateTime = tbFechaI.Value.ToString("yyyy/MM/dd")
        Dim fechaHasta As DateTime = tbFechaF.Value.ToString("yyyy/MM/dd")
        Dim dt As DataTable

        dt = L_RepVencimientos(fechaDesde, fechaHasta)

        If dt.Rows.Count > 0 Then

            L_fnBotonGenerar(gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina, 0, "REPORTE VENCIMIENTOS", "REPORTE VENCIMIENTOS")

            JGrM_Buscador.DataSource = dt
            JGrM_Buscador.RetrieveStructure()
            JGrM_Buscador.AlternatingColors = True

            With JGrM_Buscador.RootTable.Columns("chnumi")
                .Visible = False
            End With
            With JGrM_Buscador.RootTable.Columns("chfingreso")
                .Width = 90
                .Visible = True
                .Caption = "FECHA INV."
            End With
            With JGrM_Buscador.RootTable.Columns("chorden")
                .Width = 60
                .Visible = True
                .Caption = "ORDEN"
            End With
            With JGrM_Buscador.RootTable.Columns("chprod")
                .Width = 100
                .Caption = "COD. DYNASYS"
                .Visible = True
            End With
            With JGrM_Buscador.RootTable.Columns("codDelta")
                .Width = 100
                .Caption = "COD. DELTA"
                .Visible = True
            End With
            With JGrM_Buscador.RootTable.Columns("pref")
                .Visible = False
            End With
            With JGrM_Buscador.RootTable.Columns("PrefVenc")
                .Width = 100
                .Caption = "COD. VENC."
                .Visible = True
            End With
            With JGrM_Buscador.RootTable.Columns("dato1")
                .Width = 80
                .Caption = "COLOR"
                .Visible = True
            End With
            With JGrM_Buscador.RootTable.Columns("prod")
                .Width = 350
                .Caption = "DESCRIPCIÓN"
                .Visible = True
            End With
            With JGrM_Buscador.RootTable.Columns("prov")
                .Width = 150
                .Visible = True
                .Caption = "PROVEEDOR"
            End With
            With JGrM_Buscador.RootTable.Columns("chfisico")
                .Visible = False
            End With
            With JGrM_Buscador.RootTable.Columns("chStockAct")
                .Visible = False
            End With
            With JGrM_Buscador.RootTable.Columns("chfecha1")
                .Width = 90
                .Visible = True
                .Caption = "F. VENC.1 INV."
            End With
            With JGrM_Buscador.RootTable.Columns("chfecha2")
                .Width = 90
                .Visible = True
                .Caption = "F. VENC.2 INV."
            End With
            With JGrM_Buscador.RootTable.Columns("chfecha3")
                .Width = 90
                .Visible = True
                .Caption = "F. VENC.3 INV."
            End With
            With JGrM_Buscador.RootTable.Columns("chresponsable1")
                .Width = 90
                .Visible = True
                .Caption = "RESPONSABLE1"
            End With
            With JGrM_Buscador.RootTable.Columns("StockActual")
                .Width = 100
                .Caption = "STOCK ACTUAL SISTEMA"
                .FormatString = "0.00"
                .Visible = True
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            End With
            With JGrM_Buscador.RootTable.Columns("chcant1")
                .Width = 100
                .Caption = "CANTIDAD1"
                .Visible = True
                .FormatString = "0.00"
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            End With
            With JGrM_Buscador.RootTable.Columns("chfechan1")
                .Width = 90
                .Visible = True
                .Caption = "F. NUEVO VENC.1"
            End With
            With JGrM_Buscador.RootTable.Columns("chfechan2")
                .Width = 90
                .Visible = True
                .Caption = "F. NUEVO VENC.2"
            End With
            With JGrM_Buscador.RootTable.Columns("chfechan3")
                .Width = 90
                .Visible = True
                .Caption = "F. NUEVO VENC.3"
            End With
            With JGrM_Buscador.RootTable.Columns("chobs")
                .Width = 120
                .Visible = True
                .Caption = "OBSERVACIÓN"
            End With
            With JGrM_Buscador.RootTable.Columns("chresponsable2")
                .Width = 90
                .Visible = True
                .Caption = "RESPONSABLE2"
            End With
            With JGrM_Buscador.RootTable.Columns("chfact")
                .Width = 80
                .Visible = True
                .Caption = "FECHA SUBIDO AL SISTEMA"
            End With
            With JGrM_Buscador.RootTable.Columns("chhact")
                .Visible = False
            End With
            With JGrM_Buscador.RootTable.Columns("chuact")
                .Visible = False
            End With
            With JGrM_Buscador.RootTable.Columns("accion")
                .Width = 80
                .Visible = True
                .Caption = "ACCIÓN"
            End With


            With JGrM_Buscador
                .DefaultFilterRowComparison = FilterConditionOperator.Contains
                .FilterMode = FilterMode.Automatic
                .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
                .GroupByBoxVisible = False
                .TotalRow = InheritableBoolean.True
                .TotalRowFormatStyle.BackColor = Color.Gold
                .TotalRowPosition = TotalRowPosition.BottomFixed

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

    Private Sub F1_Productos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()
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
        _prCargar()
    End Sub

    Private Sub btnExportarExcel_Click(sender As Object, e As EventArgs) Handles btnExportarExcel.Click
        _prCrearCarpetaReportes()
        Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
        If (P_ExportarExcelGlobal(RutaGlobal + "\Reporte\Reporte Productos", JGrM_Buscador, "RepVencimientos")) Then
            L_fnBotonExportar(gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina, 0, "REPORTE VENCIMIENTOS", "REPORTE VENCIMIENTOS")

            ToastNotification.Show(Me, "EXPORTACIÓN DE REPORTE VENCIMIENTOS EXITOSA..!!!",
                                       img, 2000,
                                       eToastGlowColor.Green,
                                       eToastPosition.TopCenter)
        Else
            ToastNotification.Show(Me, "FALLÓ LA EXPORTACIÓN DE REPORTE VENCIMIENTOS..!!!",
                                       My.Resources.WARNING, 2000,
                                       eToastGlowColor.Red,
                                       eToastPosition.TopCenter)
        End If
    End Sub
End Class