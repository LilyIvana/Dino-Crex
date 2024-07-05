
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
Imports System.Data.OleDb
Imports System.Drawing.Printing

Imports System.Reflection
Imports System.Runtime.InteropServices
Imports Microsoft.Office.Interop


Public Class F0_ImportarPreciosImp
    Dim _Inter As Integer = 0

    Dim RutaGlobal As String = gs_CarpetaRaiz
#Region "Variables Globales"
    Dim precio As DataTable
    Public _nameButton As String
    Public _modulo As SideNavItem
    Public _tab As SuperTabItem
    Public ProductosImport As New DataTable

#End Region
#Region "MEtodos Privados"
    Private Sub _IniciarTodo()
        _prAsignarPermisos()
        Me.Text = "PRODUCTOS PARA IMPRIMIR PRECIOS"

        Dim blah As New Bitmap(New Bitmap(My.Resources.hojaruta), 20, 20)
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico
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


    Public Function _fnAccesible()
        Return btnGrabar.Enabled = True
    End Function

    Private Sub MP_ImportarExcel()
        Try
            Dim folder As String = ""
            Dim doc As String = "Hoja1"
            Dim openfile1 As OpenFileDialog = New OpenFileDialog()

            'Dim xlw As Excel.Workbook
            'Dim xlsheet As Excel.Worksheets
            'Dim xl As New Excel.Application

            If openfile1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                folder = openfile1.FileName

                'doc = Mid(openfile1.SafeFileName, 1, 31)
                'xlw = xl.Workbooks.Open("f:\Documentos\Excel\JSanchez06-11-23.xls")
                'xlsheet = xlw.worksheet(1)
                ''doc = xlsheet.ToString
                'xlsheet.Name = "hoja1"
                'xlw.Save()
            End If

            If True Then
                Dim pathconn As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & folder & ";Extended Properties='Excel 12.0 Xml;HDR=Yes'"
                'Dim pathconn As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & folder & ";"
                'Dim pathconn As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & folder & ";Extended Properties=""Excel 8.0;HDR=YES;"""

                Dim con As OleDbConnection = New OleDbConnection(pathconn)
                Dim MyDataAdapter As OleDbDataAdapter = New OleDbDataAdapter("Select * from [" & doc & "$]", con)
                con.Open()

                MyDataAdapter.Fill(ProductosImport)
                con.Close()

                'Dim dt = ProductosImport.Copy
                'Dim fecha = dt.Rows(0).Item("Fecha1")

            End If

        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub

    Private Sub MP_PasarDatos()
        Try
            If ProductosImport.Rows.Count > 0 Then
                If ProductosImport.Columns.Count = 6 Then
                    _prCargarTablaImport(ProductosImport)
                    L_fnBotonImportar(gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina, 0, "IMPRESION DE PRECIOS", "IMPRESIÓN DE PRECIOS")
                Else
                    ProductosImport.Reset()
                    ToastNotification.Show(Me, "No puede importar porque el excel tiene que tener 6 columnas, usted modificó el excel, corrija por favor".ToUpper,
                                         My.Resources.WARNING, 7000, eToastGlowColor.Green, eToastPosition.TopCenter)
                End If
            Else
                ToastNotification.Show(Me, "No se logró importar el excel".ToUpper,
                                     My.Resources.WARNING, 2700, eToastGlowColor.Green, eToastPosition.TopCenter)
            End If

        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub

    Public Sub _prCargarTablaImport(dtImport As DataTable) ''Bandera = true si es que haiq cargar denuevo la tabla de Precio Bandera =false si solo cargar datos al Janus con el precio antepuesto

        grDatos.BoundMode = Janus.Data.BoundMode.Bound
        grDatos.DataSource = dtImport
        grDatos.RetrieveStructure()

        With grDatos.RootTable.Columns("CODIGO DYNASYS")
            .Caption = "COD DYNASYS"
            .Width = 100
            .Visible = True
        End With
        With grDatos.RootTable.Columns("CODIGO CREX")
            .Caption = "COD DELTA"
            .Width = 100
            .Visible = True
        End With
        With grDatos.RootTable.Columns("DETALLE")
            .Caption = "DETALLE"
            .Width = 450
            .Visible = True
        End With
        With grDatos.RootTable.Columns("PRECIO A")
            .Caption = "PRECIO A"
            .Width = 100
            .Visible = True
            .TextAlignment = TextAlignment.Far
        End With
        With grDatos.RootTable.Columns("PRECIO B")
            .Caption = "PRECIO B"
            .Width = 100
            .Visible = True
            .TextAlignment = TextAlignment.Far
        End With
        With grDatos.RootTable.Columns("PRECIO C")
            .Caption = "PRECIO C"
            .Width = 100
            .Visible = True
            .TextAlignment = TextAlignment.Far
        End With

        'Habilitar Filtradores
        With grDatos
            .GroupByBoxVisible = False
            '.FilterRowFormatStyle.BackColor = Color.Blue
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            '.FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .FilterMode = FilterMode.Automatic
            'Diseño de la tabla
            .VisualStyle = VisualStyle.Office2007
            .SelectionMode = SelectionMode.SingleSelection
            .AlternatingColors = True

            .RecordNavigator = True
            .RecordNavigatorText = "Productos"
        End With

    End Sub
    Private Sub MostrarMensajeError(mensaje As String)
        ToastNotification.Show(Me,
                               mensaje.ToUpper,
                               My.Resources.WARNING,
                               5500,
                               eToastGlowColor.Red,
                               eToastPosition.TopCenter)
    End Sub

#End Region


#Region "Métodos Formulario"
    Private Sub F0_Precios_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _IniciarTodo()
    End Sub

#End Region

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        If grDatos.RowCount > 0 Then
            For i = 0 To grDatos.RowCount - 1
                Dim Cod As String = (CType(grDatos.DataSource, DataTable).Rows(i).Item("CODIGO DYNASYS")).ToString
                Dim dt = L_fnImpresionPreciosUno(Cod)
                Dim Ini As Integer = dt.Rows(0).Item("CantIni")
                Dim Fin As Integer = dt.Rows(0).Item("CantFin")

                If Ini = 0 And Fin = 0 Then
                    P_GenerarReporte(3, dt, "5", False) ''Imprime 1 precio
                ElseIf Ini = Fin Then
                    P_GenerarReporte(2, dt, "5", False) ''Imprime 2 precios
                ElseIf Ini <> Fin Then
                    P_GenerarReporte(1, dt, "5", False) ''Imprime 3 precios
                End If
            Next
            L_fnBotonImprimir(gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina, 0, "IMPRESION DE PRECIOS", "IMPRESIÓN DE PRECIOS")
        Else
            ToastNotification.Show(Me, "NO EXISTE DATOS PARA IMPRIMIR",
                       My.Resources.WARNING, 2300,
                       eToastGlowColor.Red,
                       eToastPosition.TopCenter)
        End If
    End Sub
    Public Sub P_GenerarReporte(tipoRep As Integer, dt As DataTable, nroImp As String, visualiza As Boolean)
        Try
            Dim _Ds3 = L_ObtenerRutaImpresora(nroImp) ' Datos de Impresión de Precios
            Dim visualizar As Boolean
            If Not IsNothing(P_Global.Visualizador) Then
                P_Global.Visualizador.Close()
            End If
            Dim objrep
            P_Global.Visualizador = New Visualizador
            If tipoRep = 1 Then ''Imprime 3 precios
                objrep = New R_ImpresionPrecios1
            ElseIf tipoRep = 2 Then ''Imprime 2 precios
                objrep = New R_ImpresionPrecios2
            ElseIf tipoRep = 3 Then ''Imprime 1 precio
                objrep = New R_ImpresionPrecios3
            End If

            objrep.SetDataSource(dt)

            If visualiza Then
                visualizar = True
            Else
                visualizar = _Ds3.Tables(0).Rows(0).Item("cbvp")
            End If


            If (visualizar) Then 'Vista Previa de la Ventana de Vizualización 1 = True 0 = False
                P_Global.Visualizador.CrGeneral.ReportSource = objrep
                P_Global.Visualizador.ShowDialog()
                P_Global.Visualizador.BringToFront()
            Else
                Dim pd As New PrintDocument()
                pd.PrinterSettings.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
                If (Not pd.PrinterSettings.IsValid) Then
                    ToastNotification.Show(Me, "La Impresora ".ToUpper + _Ds3.Tables(0).Rows(0).Item("cbrut").ToString + Chr(13) + "No Existe".ToUpper,
                                       My.Resources.WARNING, 4 * 1000,
                                       eToastGlowColor.Blue, eToastPosition.BottomRight)
                Else
                    objrep.PrintOptions.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
                    objrep.PrintOptions.PaperSource = 2
                    objrep.PrintToPrinter(1, True, 0, 0)
                End If
            End If

        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
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

    Private Sub btnImportar_Click(sender As Object, e As EventArgs) Handles btnImportar.Click
        ProductosImport.Reset()
        ProductosImport.Clear()
        MP_ImportarExcel()
        MP_PasarDatos()
    End Sub

    Private Sub grDatos_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grDatos.EditingCell
        e.Cancel = True
    End Sub
End Class