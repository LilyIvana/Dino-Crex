
Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports Janus.Windows.GridEX
Imports System.IO
Imports DevComponents.DotNetBar.SuperGrid
Imports DevComponents.DotNetBar.Controls
Imports System.Drawing.Printing
Imports iTextSharp.text.pdf

Public Class F1_PesajeProductos
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
    Public CodBarras As String

    'variables sifac
    Public tokenSifac As String
    Public CodActEco As String
    Public CodProSINs As String
    Public Ume As Integer
    Public preciosifac As Double
#End Region
#Region "Metodos Privados"
    Private Sub _prIniciarTodo()
        Me.Text = "PESAJE DE PRODUCTOS"

        _prAsignarPermisos()
        _PMIniciarTodo()
        _prCargarPesaje()
        Dim blah As New Bitmap(New Bitmap(My.Resources.producto), 20, 20)
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico

        'btExcel.Visible = False

    End Sub


    Public Sub _prStyleJanus()
        GroupPanelBuscador.Style.BackColor = Color.FromArgb(13, 71, 161)
        GroupPanelBuscador.Style.BackColor2 = Color.FromArgb(13, 71, 161)
        GroupPanelBuscador.Style.TextColor = Color.White
        JGrM_Buscador.RootTable.HeaderFormatStyle.FontBold = TriState.True
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

#End Region
#Region "METODOS SOBRECARGADOS"

    Public Overrides Sub _PMOHabilitar()
        'tbCodProd.ReadOnly = False
        'tbDescPro.ReadOnly = False
        'tbPrecio.ReadOnly = False

        dtFecha.Enabled = True
        tbPesoReal.IsInputReadOnly = False
        dtFechaVenc.Enabled = True

        btnSearch.Visible = True
        lbAyuda.Visible = True

    End Sub

    Public Overrides Sub _PMOInhabilitar()

        dtFecha.Enabled = False
        tbPesoReal.IsInputReadOnly = True
        dtFechaVenc.Enabled = False

        btnSearch.Visible = False
        lbAyuda.Visible = False

        _prStyleJanus()
        JGrM_Buscador.Focus()
        Limpiar = False

    End Sub

    Public Overrides Sub _PMOLimpiar()
        ''_PMOMostrarRegistro(JGrM_Buscador.RowCount - 1)
        _PMOMostrarRegistro(0)

        tbCodigo.Clear()
        dtFecha.Value = Now.Date
        If Limpiar = False Then
            tbCodProd.Clear()
            tbDescPro.Clear()
            tbPrecio.Text = 0
            dtFechaVenc.Value = DateAdd(DateInterval.Day, 5, Now.Date)
        End If


        tbPesoReal.Value = 0
        'dtFechaVenc.Value = DateAdd(DateInterval.Day, 5, Now.Date)
        tbPesoSistema.Value = 0
        tbTotal.Value = 0
        tbCodBarraSist.Clear()
        tbCodBarraImp.Clear()

        tbCodProd.Focus()


    End Sub


    Public Overrides Function _PMOGrabarRegistro() As Boolean

        Dim res As Boolean

        res = L_fnGrabarPesajeProducto(tbCodigo.Text, dtFecha.Value, tbCodProd.Text, tbPrecio.Text, tbPesoReal.Value,
                                       dtFechaVenc.Value, tbPesoSistema.Value, tbTotal.Value, tbCodBarraSist.Text,
                                       tbCodBarraImp.Text, gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina)
        If res Then

            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "Código de Pesaje de Producto ".ToUpper + tbCodigo.Text + " Grabado con Exito.".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter
                                      )
            ImprimirCodigoBarras(tbCodigo.Text)
            tbCodigo.Focus()
            Limpiar = True
        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "El pesaje del producto no pudo ser insertado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

        End If
        Return res

    End Function

    Public Overrides Function _PMOModificarRegistro() As Boolean
        Dim res As Boolean
        res = L_fnModificarPesajeProducto(tbCodigo.Text, dtFecha.Value, tbCodProd.Text, tbPrecio.Text, tbPesoReal.Value,
                                       dtFechaVenc.Value, tbPesoSistema.Value, tbTotal.Value, tbCodBarraSist.Text,
                                       tbCodBarraImp.Text, gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina)

        If res Then

            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "Código de Pesaje de Producto ".ToUpper + tbCodigo.Text + " modificado con éxito.".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter)

            ImprimirCodigoBarras(tbCodigo.Text)

        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "El pesaje de producto no pudo ser modificado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

        End If
        _PMInhabilitar()
        _PMPrimerRegistro()
        PanelNavegacion.Enabled = True
        Return res
    End Function

    Public Overrides Sub _PMOEliminarRegistro()

        Dim ef = New Efecto
        ef.tipo = 2
        ef.Context = "¿esta seguro de eliminar el registro?".ToUpper
        ef.Header = "mensaje principal".ToUpper
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then
            Dim mensajeError As String = ""
            Dim res As Boolean = L_fnEliminarPesajeProducto(tbCodigo.Text, mensajeError, gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina)
            If res Then
                Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)

                ToastNotification.Show(Me, "Código de Pesaje de Producto ".ToUpper + tbCodigo.Text + " eliminado con {éxito.".ToUpper,
                                          img, 2000,
                                          eToastGlowColor.Green,
                                          eToastPosition.TopCenter)

                _PMFiltrar()
            Else
                Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                ToastNotification.Show(Me, mensajeError, img, 3000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            End If
        End If

    End Sub
    Public Overrides Function _PMOValidarCampos() As Boolean
        Dim _ok As Boolean = True
        MEP.Clear()

        If tbCodProd.Text = String.Empty Then
            tbCodProd.BackColor = Color.Red
            AddHandler tbCodProd.KeyDown, AddressOf TextBox_KeyDown
            MEP.SetError(tbCodProd, "Debe elegir un Producto!".ToUpper)
            _ok = False
        Else
            tbCodProd.BackColor = Color.White
            MEP.SetError(tbCodProd, "")
        End If

        If tbPesoReal.Value = 0 Then
            tbPesoReal.BackColor = Color.Red
            AddHandler tbPesoReal.KeyDown, AddressOf TextBox_KeyDown
            MEP.SetError(tbPesoReal, "Debe colocar el peso real!".ToUpper)
            _ok = False
        Else
            tbPesoReal.BackColor = Color.White
            MEP.SetError(tbPesoReal, "")
        End If

        If dtFechaVenc.Value <= dtFecha.Value Then
            dtFechaVenc.BackColor = Color.Red
            AddHandler dtFechaVenc.KeyDown, AddressOf TextBox_KeyDown
            MEP.SetError(dtFechaVenc, "La Fecha de Vencimiento no puede ser menor o igual a la fecha Actual!".ToUpper)
            _ok = False
        Else
            dtFechaVenc.BackColor = Color.White
            MEP.SetError(dtFechaVenc, "")
        End If

        MHighlighterFocus.UpdateHighlights()
        Return _ok
    End Function

    Public Overrides Function _PMOGetTablaBuscador() As DataTable
        Dim dtBuscador As DataTable = L_fnGeneralPesajeProductos(IIf(swMostrar.Value = True, 1, 0))

        Return dtBuscador
    End Function

    Public Overrides Function _PMOGetListEstructuraBuscador() As List(Of Modelo.Celda)
        Dim listEstCeldas As New List(Of Modelo.Celda)

        listEstCeldas.Add(New Modelo.Celda("pcnumi", True, "Código".ToUpper, 80))
        listEstCeldas.Add(New Modelo.Celda("pcfecha", True, "F. Ingreso".ToUpper, 80, "dd/MM/yyyy"))
        listEstCeldas.Add(New Modelo.Celda("pccodPro", True, "Cod.Producto".ToUpper, 90))
        listEstCeldas.Add(New Modelo.Celda("yfcdprod1", True, "Descripción".ToUpper, 390))
        listEstCeldas.Add(New Modelo.Celda("pcprecio", True, "Precio Bs./KG".ToUpper, 90, "0.00"))
        listEstCeldas.Add(New Modelo.Celda("pcpesoreal", True, "Peso Real".ToUpper, 90, "0.000"))
        listEstCeldas.Add(New Modelo.Celda("pcfvenc", True, "F. Vencimiento".ToUpper, 80, "dd/MM/yyyy"))
        listEstCeldas.Add(New Modelo.Celda("pcpeso", True, "Peso Sistema".ToUpper, 90, "0.00"))
        listEstCeldas.Add(New Modelo.Celda("pctotal", True, "Total Bs.".ToUpper, 90, "0.00"))
        listEstCeldas.Add(New Modelo.Celda("pccbarra", False, "Cod. Barras".ToUpper, 100))
        listEstCeldas.Add(New Modelo.Celda("pccbarraimp", True, "Cod. Barras Impresión".ToUpper, 120))

        listEstCeldas.Add(New Modelo.Celda("pcfact", False))
        listEstCeldas.Add(New Modelo.Celda("pchact", False))
        listEstCeldas.Add(New Modelo.Celda("pcuact", True, "Usuario".ToUpper, 100))

        Return listEstCeldas

    End Function

    Public Overrides Sub _PMOMostrarRegistro(_N As Integer)
        'JGrM_Buscador.Row = _MPos
        JGrM_Buscador.Row = _N
        Dim dt As DataTable = CType(JGrM_Buscador.DataSource, DataTable)
        Try
            tbCodigo.Text = JGrM_Buscador.GetValue("pcnumi").ToString
        Catch ex As Exception
            Exit Sub
        End Try
        With JGrM_Buscador
            tbCodigo.Text = .GetValue("pcnumi").ToString
            dtFecha.Value = .GetValue("pcfecha")
            tbCodProd.Text = .GetValue("pccodPro").ToString
            tbDescPro.Text = .GetValue("yfcdprod1").ToString
            tbPrecio.Text = .GetValue("pcprecio").ToString
            tbPesoReal.Value = .GetValue("pcpesoreal")
            dtFechaVenc.Value = .GetValue("pcfvenc")
            tbPesoSistema.Value = .GetValue("pcpeso")
            tbTotal.Value = .GetValue("pctotal")
            tbCodBarraSist.Text = .GetValue("pccbarra").ToString
            tbCodBarraImp.Text = .GetValue("pccbarraimp").ToString

            lbFecha.Text = CType(.GetValue("pcfact"), Date).ToString("dd/MM/yyyy")
            lbHora.Text = .GetValue("pchact").ToString
            lbUsuario.Text = .GetValue("pcuact").ToString


        End With

        '_PMOMostrarRegistro(JGrM_Buscador.RowCount - 1)
        LblPaginacion.Text = Str(_MPos + 1) + "/" + JGrM_Buscador.RowCount.ToString
    End Sub

#End Region

    Private Sub F1_Productos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()
    End Sub
    Private Sub _prCargarPesaje()
        Dim dt As New DataTable
        dt = L_fnGeneralPesajeProductos(IIf(swMostrar.Value = True, 1, 0))

        JGrM_Buscador.DataSource = dt
        JGrM_Buscador.RetrieveStructure()
        JGrM_Buscador.AlternatingColors = True

        With JGrM_Buscador.RootTable.Columns("pcnumi")
            .Width = 80
            .Visible = True
            .Caption = "Código".ToUpper
        End With
        With JGrM_Buscador.RootTable.Columns("pcfecha")
            .Width = 80
            .Visible = True
            .Caption = "F. Ingreso".ToUpper
            .FormatString = "dd/MM/yyyy"
        End With
        With JGrM_Buscador.RootTable.Columns("pccodPro")
            .Width = 90
            .Visible = True
            .Caption = "Cod.Dynasys".ToUpper
        End With
        With JGrM_Buscador.RootTable.Columns("yfcdprod1")
            .Width = 390
            .Visible = True
            .Caption = "Descripción".ToUpper
        End With
        With JGrM_Buscador.RootTable.Columns("pcprecio")
            .Width = 90
            .Visible = True
            .Caption = "Precio Bs./KG".ToUpper
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .FormatString = "0.00"
        End With
        With JGrM_Buscador.RootTable.Columns("pcpesoreal")
            .Width = 90
            .Visible = True
            .Caption = "Peso Real".ToUpper
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .FormatString = "0.000"
        End With
        With JGrM_Buscador.RootTable.Columns("pcfvenc")
            .Width = 80
            .Visible = True
            .Caption = "F. Vencimiento".ToUpper
            .FormatString = "dd/MM/yyyy"
        End With
        With JGrM_Buscador.RootTable.Columns("pcpeso")
            .Width = 90
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .Caption = "Peso Sistema".ToUpper
            .FormatString = "0.00"
            .AggregateFunction = AggregateFunction.Sum
        End With
        With JGrM_Buscador.RootTable.Columns("pctotal")
            .Width = 90
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .Caption = "Total Bs.".ToUpper
            .FormatString = "0.00"
        End With
        With JGrM_Buscador.RootTable.Columns("pccbarra")
            .Visible = False
        End With
        With JGrM_Buscador.RootTable.Columns("pccbarraimp")
            .Width = 120
            .Visible = True
            .Caption = "Cod. Barras Impresión".ToUpper
        End With
        With JGrM_Buscador.RootTable.Columns("pcfact")
            .Visible = False
        End With
        With JGrM_Buscador.RootTable.Columns("pchact")
            .Visible = False
        End With
        With JGrM_Buscador.RootTable.Columns("pcuact")
            .Width = 100
            .Visible = True
            .Caption = "Usuario".ToUpper
        End With
        With JGrM_Buscador
            '.DefaultFilterRowComparison = FilterConditionOperator.Contains
            '.FilterMode = FilterMode.Automatic
            '.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            '.GroupByBoxVisible = False
            ''diseño de la grilla
            '.VisualStyle = VisualStyle.Office2007
            '.TotalRow = InheritableBoolean.True
            '.TotalRowFormatStyle.BackColor = Color.Gold
            '.TotalRowPosition = TotalRowPosition.BottomFixed


            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
            .TotalRow = InheritableBoolean.True
            .TotalRowFormatStyle.BackColor = Color.Gold
            .TotalRowPosition = TotalRowPosition.BottomFixed

        End With

    End Sub
    Private Sub btExcel_Click(sender As Object, e As EventArgs) Handles btExcel.Click
        _prCrearCarpetaReportes()
        Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
        If (P_ExportarExcel(RutaGlobal + "\Reporte\Reporte Productos")) Then
            L_fnExportarPesaje(gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina)
            ToastNotification.Show(Me, "EXPORTACIÓN DE PESAJE DE PRODUCTOS EXITOSA..!!!",
                                       img, 2000,
                                       eToastGlowColor.Green,
                                       eToastPosition.BottomCenter)
        Else
            ToastNotification.Show(Me, "FALLO AL EXPORTACIÓN DE PESAJE DE PRODUCTOS..!!!",
                                       My.Resources.WARNING, 2000,
                                       eToastGlowColor.Red,
                                       eToastPosition.BottomLeft)
        End If
    End Sub


    Public Function P_ExportarExcel(_ruta As String) As Boolean
        Dim _ubicacion As String
        'Dim _directorio As New FolderBrowserDialog

        If (1 = 1) Then 'If(_directorio.ShowDialog = Windows.Forms.DialogResult.OK) Then
            _ubicacion = _ruta
            Try
                Dim _stream As Stream
                Dim _escritor As StreamWriter
                Dim _fila As Integer = JGrM_Buscador.GetRows.Length
                Dim _columna As Integer = JGrM_Buscador.RootTable.Columns.Count
                Dim _archivo As String = _ubicacion & "\ListaPesajeDeProductos_" & Now.Date.Day &
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
                Next
                _escritor.Close()

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

    Private Sub JGrM_Buscador_DoubleClick(sender As Object, e As EventArgs) Handles JGrM_Buscador.DoubleClick
        If (MPanelSup.Visible = True) Then
            JGrM_Buscador.GroupByBoxVisible = True
            MPanelSup.Visible = False
            JGrM_Buscador.UseGroupRowSelector = True

        Else
            JGrM_Buscador.GroupByBoxVisible = False
            JGrM_Buscador.UseGroupRowSelector = True
            MPanelSup.Visible = True
        End If
    End Sub



    Private Sub JGrM_Buscador_KeyDown(sender As Object, e As KeyEventArgs) Handles JGrM_Buscador.KeyDown
        If e.KeyData = Keys.Enter Then
            If (MPanelSup.Visible = True) Then
                JGrM_Buscador.GroupByBoxVisible = True
                MPanelSup.Visible = False
                JGrM_Buscador.UseGroupRowSelector = True

            Else
                JGrM_Buscador.GroupByBoxVisible = False
                JGrM_Buscador.UseGroupRowSelector = True
                MPanelSup.Visible = True
            End If
        End If
    End Sub

    Private Sub TextBox_KeyDown(sender As Object, e As KeyEventArgs)
        Dim tb As TextBoxX = CType(sender, TextBoxX)
        If tb.Text = String.Empty Then

        Else
            tb.BackColor = Color.White
            MEP.SetError(tb, "")
        End If
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        If btnGrabar.Enabled = True Then
            _PMInhabilitar()
            _PMPrimerRegistro()
            PanelNavegacion.Enabled = True
            _prCargarPesaje()
        Else
            '  Public _modulo As SideNavItem
            _modulo.Select()
            Me.Close()
        End If
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        P_GenerarReporte()
    End Sub
    Private Sub P_GenerarReporte()
        ImprimirCodigoBarras(tbCodigo.Text)
        L_fnImpresionPesaje(gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina, tbCodigo.Text)
    End Sub
    Private Sub codigoBarrasImprimir()
        Dim dt As DataTable
        If (MessageBox.Show("DESEA IMPRIMIR CODIGO DE BARRAS PARA TODOS LOS PRODUCTOS?", "PREGUNTA...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes) Then
            'Process.Start(_archivo)
            dt = L_fnCodigoBarra()
        Else
            dt = L_fnCodigoBarraUno(tbCodigo.Text)
        End If
        For i As Integer = 0 To dt.Rows.Count - 1
            Dim codigo As String = "0000" + dt.Rows(i).Item("yfnumi").ToString
            Dim bm As Bitmap = Nothing
            bm = Codigos.codigo128("A" & codigo & "B", False, 20)
            If Not IsNothing(bm) Then
                Dim Bin As New MemoryStream
                bm.Save(Bin, Imaging.ImageFormat.Png)
                dt.Rows(i).Item("img") = Bin.GetBuffer
            End If
        Next
        If Not IsNothing(P_Global.Visualizador) Then
            P_Global.Visualizador.Close()
        End If
        P_Global.Visualizador = New Visualizador
        Dim objrep As New R_CodigoBarras
        ' GenerarNro(_dt)
        'objrep.SetDataSource(Dt1Kardex)

        objrep.SetDataSource(dt)
        P_Global.Visualizador.CrGeneral.ReportSource = objrep 'Comentar
        P_Global.Visualizador.Show() 'Comentar
        P_Global.Visualizador.BringToFront() 'Comentar
    End Sub

    Private Sub ImprimirCodigoBarras(CodigoPro As String)
        Dim dt As DataTable

        dt = L_fnCodigoBarraUnProducto(CodigoPro)

        Dim bcode As New Barcode128
        bcode.BarHeight = 50
        bcode.Code = tbCodBarraImp.Text
        bcode.GenerateChecksum = True
        bcode.CodeType = Barcode.CODE128


        For i As Integer = 0 To dt.Rows.Count - 1
            Dim codigo As String = dt.Rows(i).Item("pccbarraimp").ToString
            Dim bm As Bitmap = (bcode.CreateDrawingImage(Color.Black, Color.White))
            Dim img As Image
            img = New Bitmap(bm.Width, bm.Height)
            Dim g As Graphics = Graphics.FromImage(img)
            g.FillRectangle(New SolidBrush(Color.White), 0, 0, bm.Width, bm.Height)
            g.DrawImage(bm, 0, 0)
            Dim Bin As New MemoryStream
            bm.Save(Bin, Imaging.ImageFormat.Png)
            dt.Rows(i).Item("img") = Bin.GetBuffer
            pbCodB.Image = img
        Next


        If Not IsNothing(P_Global.Visualizador) Then
            P_Global.Visualizador.Close()
        End If
        P_Global.Visualizador = New Visualizador
        Dim objrep As New R_StickerCodigoBarras4

        objrep.SetDataSource(dt)
        objrep.SetParameterValue("CodBarra", tbCodBarraImp.Text)
        objrep.SetParameterValue("FVenc", dtFechaVenc.Text)


        Dim _Ds3 = L_ObtenerRutaImpresora("3") ' Datos de Impresion de Facturación
        If (_Ds3.Tables(0).Rows(0).Item("cbvp")) Then 'Vista Previa de la Ventana de Vizualización 1 = True 0 = False
            P_Global.Visualizador.CrGeneral.ReportSource = objrep 'Comentar
            P_Global.Visualizador.ShowDialog() 'Comentar
            P_Global.Visualizador.BringToFront() 'Comentar
        Else
            Dim pd As New PrintDocument()
            pd.PrinterSettings.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
            If (Not pd.PrinterSettings.IsValid) Then
                ToastNotification.Show(Me, "La Impresora ".ToUpper + _Ds3.Tables(0).Rows(0).Item("cbrut").ToString + Chr(13) + "No Existe".ToUpper,
                                       My.Resources.WARNING, 5 * 1000,
                                       eToastGlowColor.Blue, eToastPosition.BottomRight)
            Else
                objrep.PrintOptions.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
                objrep.PrintToPrinter(1, True, 0, 0)
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


    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If (_fnAccesible()) Then

            Dim dt As DataTable
            'dt = L_fnListarProductosCompra(1, 70)
            dt = L_fnListarProductosPesaje(1, 70)


            Dim listEstCeldas As New List(Of Modelo.Celda)
            listEstCeldas.Add(New Modelo.Celda("yfnumi,", True, "Cód. Dynasys", 90))
            listEstCeldas.Add(New Modelo.Celda("yfcprod", True, "Cód. Delta", 90))
            listEstCeldas.Add(New Modelo.Celda("yfcbarra", True, "Cód. Barra", 110))
            listEstCeldas.Add(New Modelo.Celda("yfcdprod1", True, "Descripción", 280))
            listEstCeldas.Add(New Modelo.Celda("yfcdprod2", False, "", 50))
            listEstCeldas.Add(New Modelo.Celda("yfgr1", False, "", 50))
            listEstCeldas.Add(New Modelo.Celda("grupo1", True, "Proveedor", 120))
            listEstCeldas.Add(New Modelo.Celda("yfgr2", False, "", 50))
            listEstCeldas.Add(New Modelo.Celda("grupo2", False, "", 50))
            listEstCeldas.Add(New Modelo.Celda("yfgr3", False, "", 50))
            listEstCeldas.Add(New Modelo.Celda("grupo3", False, "", 50))
            listEstCeldas.Add(New Modelo.Celda("yfgr4", False, "", 50))
            listEstCeldas.Add(New Modelo.Celda("grupo4", False, "", 50))
            listEstCeldas.Add(New Modelo.Celda("yfumin", False, "", 50))
            listEstCeldas.Add(New Modelo.Celda("UnidMin", True, "Unidad", 50))
            listEstCeldas.Add(New Modelo.Celda("yhprecio", False, "Precio Costo", 100))
            listEstCeldas.Add(New Modelo.Celda("venta", True, "Precio Venta", 100))
            listEstCeldas.Add(New Modelo.Celda("stock", True, "Stock", 100))

            Dim ef = New Efecto
            ef.tipo = 3
            ef.dt = dt
            ef.SeleclCol = 3
            ef.listEstCeldas = listEstCeldas
            ef.alto = 50
            ef.ancho = 250
            ef.Context = "Seleccione Producto".ToUpper
            ef.ShowDialog()
            Dim bandera As Boolean = False
            bandera = ef.band
            If (bandera = True) Then
                Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
                If (IsNothing(Row)) Then
                    tbCodProd.Focus()
                    Return
                End If

                tbCodProd.Text = Row.Cells("yfnumi").Value
                tbDescPro.Text = Row.Cells("yfcdprod1").Value
                tbPrecio.Text = Row.Cells("venta").Value

                tbPesoReal.Value = 0
                dtFechaVenc.Value = DateAdd(DateInterval.Day, 5, Now.Date)
                tbPesoSistema.Value = 0
                tbTotal.Value = 0
                tbCodBarraSist.Clear()
                tbCodBarraImp.Clear()

                tbPesoReal.Focus()
            End If

        End If
    End Sub
    Function _fnAccesible() As Boolean
        Return tbPesoReal.IsInputReadOnly = False
    End Function

    Private Sub tbPesoReal_ValueChanged(sender As Object, e As EventArgs) Handles tbPesoReal.ValueChanged
        If _fnAccesible() Then
            If tbPesoReal.Value < 10 Then

                tbPesoSistema.Value = Format(tbPesoReal.Value, "#.#0")
                tbTotal.Value = Format((tbPesoSistema.Value * tbPrecio.Text), "#.#0")
                Dim pesoGr As Integer = (tbPesoSistema.Value * 1000)


                Dim LenCod As Integer = Len(tbCodProd.Text)
                Dim LenPeso As Integer = Len(pesoGr.ToString)

                Dim CodProducto As String
                Dim barra1 As String
                Dim barra2 As String
                Select Case LenCod
                    Case 1
                        CodProducto = "0000" & tbCodProd.Text
                    Case 2
                        CodProducto = "000" & tbCodProd.Text

                        CodProducto = "00" & tbCodProd.Text
                    Case 4
                        CodProducto = "0" & tbCodProd.Text
                    Case 5
                        CodProducto = tbCodProd.Text
                End Select

                barra1 = 20 & CodProducto

                Select Case LenPeso
                    Case 2
                        barra2 = "000" & pesoGr.ToString
                    Case 3
                        barra2 = "00" & pesoGr.ToString
                    Case 4
                        barra2 = "0" & pesoGr.ToString
                    Case 5
                        barra2 = pesoGr.ToString
                End Select


                tbCodBarraImp.Text = barra1 & barra2
                tbCodBarraSist.Text = barra1
            Else

                ToastNotification.Show(Me, "El peso no puede ser mayor a 10".ToUpper,
                                      My.Resources.WARNING, 3 * 1000,
                                      eToastGlowColor.Blue, eToastPosition.TopCenter)
                tbPesoReal.Value = 0

            End If
        End If

        'Dim aux = Replace(tbPesoSistema.Text, ".", "")
        'tbCodBarraSist.Text = aux

    End Sub

    Private Sub swMostrar_ValueChanged(sender As Object, e As EventArgs) Handles swMostrar.ValueChanged
        _prCargarPesaje()
    End Sub

End Class