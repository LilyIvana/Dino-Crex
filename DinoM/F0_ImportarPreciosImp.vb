
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

Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared


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
        With grDatos.RootTable.Columns("CODIGO DELTA")
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
                    P_GenerarReporte(1, dt, "5", False) ''Imprime 1 precio
                ElseIf Ini = Fin Then
                    P_GenerarReporte(2, dt, "5", False) ''Imprime 2 precios
                ElseIf Ini <> Fin Then
                    P_GenerarReporte(3, dt, "5", False) ''Imprime 3 precios
                End If

                L_fnBotonImprimir(gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina, Cod, "IMPRESION DE PRECIOS", "IMPRESIÓN DE PRECIOS")
            Next

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
            If tipoRep = 1 Then ''Imprime 1 precio
                objrep = New R_ImpresionPrecios1
            ElseIf tipoRep = 2 Then ''Imprime 2 precios
                objrep = New R_ImpresionPrecios2
            ElseIf tipoRep = 3 Then ''Imprime 3 precios
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

                _prCrearCarpetaReportesPrecios()
                Dim ubicacion = RutaGlobal + "\Reporte\Reporte Precios"
                Dim _archivo As String = ubicacion & "\Precio" & Now.Date.Day &
                    "." & Now.Date.Month & "." & Now.Date.Year & "_" & Now.Hour & "." & Now.Minute & "." & Now.Second & ".doc"
                objrep.ExportToDisk(ExportFormatType.WordForWindows, _archivo)

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

    Public Sub P_GenerarReporteOtrosFormatos(tipoRep As Integer, dt As DataTable, nroImp As String, visualiza As Boolean, Optional ByVal tipo As Integer = 0)
        Try
            Dim _Ds3 = L_ObtenerRutaImpresora(nroImp) ' Datos de Impresión de Precios
            Dim visualizar As Boolean
            If Not IsNothing(P_Global.Visualizador) Then
                P_Global.Visualizador.Close()
            End If
            Dim objrep
            P_Global.Visualizador = New Visualizador
            If tipoRep = 1 Then ''Imprime nombres (7x1.5cm)
                objrep = New R_ImpresionPrecioFrio1Vertical

            ElseIf tipoRep = 2 Then ''Imprime nombres (12.4x1.5cm)
                objrep = New R_ImpresionPrecioFrio2Vertical
            ElseIf tipoRep = 3 Then ''Imprime precios (4.5 x 3.5cm)
                If tipo = 1 Then ''Imprime 1 precio
                    objrep = New R_ImpresionPrecios1Peq
                ElseIf tipo = 2 Then ''Imprime 2 precios
                    objrep = New R_ImpresionPrecios2Peq
                ElseIf tipo = 3 Then ''Imprime 3 precios
                    objrep = New R_ImpresionPrecios3Peq
                End If
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

                _prCrearCarpetaReportesPrecios()
                Dim ubicacion = RutaGlobal + "\Reporte\Reporte Precios"
                Dim _archivo As String = ubicacion & "\Precio" & Now.Date.Day &
                    "." & Now.Date.Month & "." & Now.Date.Year & "_" & Now.Hour & "." & Now.Minute & "." & Now.Second & ".doc"
                objrep.ExportToDisk(ExportFormatType.WordForWindows, _archivo)

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
            Else
                Dim pd As New PrintDocument()
                pd.PrinterSettings.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
                If (Not pd.PrinterSettings.IsValid) Then
                    ToastNotification.Show(Me, "La Impresora ".ToUpper + _Ds3.Tables(0).Rows(0).Item("cbrut").ToString + Chr(13) + "No Existe".ToUpper,
                                       My.Resources.WARNING, 4 * 1000,
                                       eToastGlowColor.Blue, eToastPosition.BottomRight)
                Else
                    objrep.PrintOptions.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
                    objrep.PrintOptions.PaperSource = 3
                    objrep.PrintToPrinter(1, True, 0, 0)
                End If
            End If

        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub
    Private Sub _prCrearCarpetaReportesPrecios()
        Dim rutaDestino As String = RutaGlobal + "\Reporte\Reporte Precios\"

        If System.IO.Directory.Exists(RutaGlobal + "\Reporte\Reporte Precios\") = False Then
            If System.IO.Directory.Exists(RutaGlobal + "\Reporte") = False Then
                System.IO.Directory.CreateDirectory(RutaGlobal + "\Reporte")
                If System.IO.Directory.Exists(RutaGlobal + "\Reporte\Reporte Precios") = False Then
                    System.IO.Directory.CreateDirectory(RutaGlobal + "\Reporte\Reporte Precios")
                End If
            Else
                If System.IO.Directory.Exists(RutaGlobal + "\Reporte\Reporte Precios") = False Then
                    System.IO.Directory.CreateDirectory(RutaGlobal + "\Reporte\Reporte Precios")

                End If
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

    Private Sub btnImportar_Click(sender As Object, e As EventArgs) Handles btnImportar.Click
        'ProductosImport.Reset()
        ProductosImport.Clear()
        MP_ImportarExcel()
        MP_PasarDatos()
    End Sub

    Private Sub grDatos_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grDatos.EditingCell
        e.Cancel = True
    End Sub

    Private Sub btnActualizarPrecios_Click(sender As Object, e As EventArgs) Handles btnActualizarPrecios.Click
        Try
            If grDatos.RowCount > 0 Then
                For i = 0 To grDatos.RowCount - 1
                    Dim Cod As String = (CType(grDatos.DataSource, DataTable).Rows(i).Item("CODIGO DYNASYS")).ToString
                    Dim dt = L_fnImpresionPreciosUno(Cod)

                    CType(grDatos.DataSource, DataTable).Rows(i).Item("CODIGO DELTA") = dt.Rows(0).Item("CodigoExterno")
                    CType(grDatos.DataSource, DataTable).Rows(i).Item("DETALLE") = dt.Rows(0).Item("NombreProducto")
                    CType(grDatos.DataSource, DataTable).Rows(i).Item("PRECIO A") = dt.Rows(0).Item("PrecioVenta")
                    CType(grDatos.DataSource, DataTable).Rows(i).Item("PRECIO B") = dt.Rows(0).Item("PrecioEspecial")
                    CType(grDatos.DataSource, DataTable).Rows(i).Item("PRECIO C") = dt.Rows(0).Item("PrecioPDV")
                Next

            Else
                ToastNotification.Show(Me, "NO EXISTE DATOS PARA RECARGAR PRECIOS",
                           My.Resources.WARNING, 2300,
                           eToastGlowColor.Red,
                           eToastPosition.TopCenter)
            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub

    Private Sub btnImprimirFrio1_Click(sender As Object, e As EventArgs) Handles btnImprimirFrio.Click
        If grDatos.RowCount > 0 Then
            Dim dt, dtAux As DataTable
            dt = L_fnImpresionPreciosUno(0)
            dt.Clear()
            For i = 0 To grDatos.RowCount - 1
                Dim Cod As String = (CType(grDatos.DataSource, DataTable).Rows(i).Item("CODIGO DYNASYS")).ToString
                dtAux = L_fnImpresionPreciosUno(Cod)
                dt.Rows.Add(dtAux.Rows(0).ItemArray)
                L_fnBotonImprimir(gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina, Cod, "IMPRESION DE PRECIOS", "IMPRESIÓN DE PRECIOS FRIO")
            Next
            If swMedida.Value = True Then
                P_GenerarReporteOtrosFormatos(1, dt, "5", False) ''Imprime mas chico (7x1.5cm)
            Else
                P_GenerarReporteOtrosFormatos(2, dt, "5", False) ''Imprime mas chico (12.4x1.5cm)
            End If

        Else
            ToastNotification.Show(Me, "NO EXISTE DATOS PARA IMPRIMIR",
                       My.Resources.WARNING, 2300,
                       eToastGlowColor.Red,
                       eToastPosition.TopCenter)
        End If
    End Sub

    Private Sub btnImprimirPrecioPeq_Click(sender As Object, e As EventArgs) Handles btnImprimirPrecioPeq.Click
        If grDatos.RowCount > 0 Then
            Dim dt1, dt2, dt3, dtAux As DataTable
            dt1 = L_fnImpresionPreciosUno(0)
            dt1.Clear()
            dt2 = L_fnImpresionPreciosUno(0)
            dt2.Clear()
            dt3 = L_fnImpresionPreciosUno(0)
            dt3.Clear()
            For i = 0 To grDatos.RowCount - 1
                Dim Cod As String = (CType(grDatos.DataSource, DataTable).Rows(i).Item("CODIGO DYNASYS")).ToString
                dtAux = L_fnImpresionPreciosUno(Cod)

                Dim Ini As Integer = dtAux.Rows(0).Item("CantIni")
                Dim Fin As Integer = dtAux.Rows(0).Item("CantFin")

                If Ini = 0 And Fin = 0 Then
                    dt1.Rows.Add(dtAux.Rows(0).ItemArray) ''Imprime 1 precio
                ElseIf Ini = Fin Then
                    dt2.Rows.Add(dtAux.Rows(0).ItemArray) ''Imprime 2 precios
                ElseIf Ini <> Fin Then

                    dt3.Rows.Add(dtAux.Rows(0).ItemArray) ''Imprime 3 precios
                End If

                'dt.Rows.Add(dtAux.Rows(0).ItemArray)
                L_fnBotonImprimir(gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina, Cod, "IMPRESION DE PRECIOS", "IMPRESIÓN DE PRECIOS PEQUEÑOS")
            Next

            If dt1.Rows.Count > 0 Then
                P_GenerarReporteOtrosFormatos(3, dt1, "5", False, 1) ''Imprime 1 precio (4.5 x 3.5cm)
            End If

            If dt2.Rows.Count > 0 Then
                P_GenerarReporteOtrosFormatos(3, dt2, "5", False, 2) ''Imprime 2 precios (4.5 x 3.5cm)
            End If

            If dt3.Rows.Count > 0 Then
                P_GenerarReporteOtrosFormatos(3, dt3, "5", False, 3) ''Imprime 3 precios (4.5 x 3.5cm)
            End If




        Else
            ToastNotification.Show(Me, "NO EXISTE DATOS PARA IMPRIMIR",
                       My.Resources.WARNING, 2300,
                       eToastGlowColor.Red,
                       eToastPosition.TopCenter)
        End If
    End Sub
End Class