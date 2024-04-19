
Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports Janus.Windows.GridEX
Imports System.IO
Imports DevComponents.DotNetBar.SuperGrid
Imports DevComponents.DotNetBar.Controls
Imports Newtonsoft.Json
Imports DinoM.AeconomicaResp
Imports DinoM.UmedidaResp
Imports DinoM.HomologResp
Imports DinoM.ListarPServResp

Public Class F1_ProductosConteo
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
        Me.Text = "PRODUCTOS CONTEO"
        'L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)

        _prCargarNameLabel()
        _prCargarComboLibreria(cbgrupo1, 1, 1)
        _prCargarComboLibreria(cbgrupo2, 1, 2)
        _prCargarComboLibreria(cbgrupo3, 1, 3)
        _prCargarComboLibreria(cbgrupo4, 1, 4)
        _prCargarComboLibreria(cbgrupo5, 1, 7)
        _prCargarComboLibreria(cbUMed, 1, 5)
        _prCargarComboLado(cbLado)
        _prAsignarPermisos()

        ''Mostrar u ocultar el grupo 4(Familia)
        If gb_MostrarFamilia = 0 Then
            lbgrupo4.Visible = False
            cbgrupo4.Visible = False
        Else
            lbgrupo4.Visible = True
            cbgrupo4.Visible = True
        End If
        _PMIniciarTodo()


        Dim blah As New Bitmap(New Bitmap(My.Resources.producto), 20, 20)
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico

        btnImprimir.Visible = False

    End Sub


    Public Sub _prStyleJanus()
        GroupPanelBuscador.Style.BackColor = Color.FromArgb(13, 71, 161)
        GroupPanelBuscador.Style.BackColor2 = Color.FromArgb(13, 71, 161)
        GroupPanelBuscador.Style.TextColor = Color.White
        JGrM_Buscador.RootTable.HeaderFormatStyle.FontBold = TriState.True
    End Sub
    Public Sub _prCargarNameLabel()
        Dim dt As DataTable = L_fnNameLabel()
        If (dt.Rows.Count > 0) Then
            lbgrupo1.Text = dt.Rows(0).Item("Grupo 1").ToString + ":"
            lbgrupo2.Text = dt.Rows(0).Item("Grupo 2").ToString + ":"
            lbgrupo3.Text = dt.Rows(0).Item("Grupo 3").ToString + ":"
            lbgrupo4.Text = dt.Rows(0).Item("Grupo 4").ToString + ":"

        End If
    End Sub


    Private Sub _prCargarComboLibreria(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo, cod1 As String, cod2 As String)
        Dim dt As New DataTable
        dt = L_prLibreriaClienteLGeneral(cod1, cod2)
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("yccod3").Width = 70
            .DropDownList.Columns("yccod3").Caption = "COD"
            .DropDownList.Columns.Add("ycdes3").Width = 200
            .DropDownList.Columns("ycdes3").Caption = "DESCRIPCION"
            .ValueMember = "yccod3"
            .DisplayMember = "ycdes3"
            .DataSource = dt
            .Refresh()
        End With
    End Sub

    Private Sub _prCargarComboLado(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable

        dt.Columns.Add("COD")
        dt.Columns.Add("LADO")

        dt.Rows.Add(1, "LADO A")
        dt.Rows.Add(2, "LADO B")
        dt.Rows.Add(3, "S/L")

        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("COD").Width = 50
            .DropDownList.Columns("COD").Caption = "COD"
            .DropDownList.Columns.Add("LADO").Width = 120
            .DropDownList.Columns("LADO").Caption = "LADO"
            .ValueMember = "COD"
            .DisplayMember = "LADO"
            .DataSource = dt
            .Refresh()
        End With

        'If dt.Rows.Count > 0 Then
        '    mCombo.SelectedIndex = 3
        'End If
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

#End Region
#Region "METODOS SOBRECARGADOS"

    Public Overrides Sub _PMOHabilitar()
        'tbResponsable.ReadOnly = False
        cbLado.ReadOnly = False
        'cbLado.Enabled = True
        'cbLado.ButtonEnabled = True

        tbOrden.IsInputReadOnly = False
        btnSearch.Visible = True
        lbInf.Visible = True

        _prCrearCarpetaImagenes()
        _prCrearCarpetaTemporal()

        btExcel.Visible = False
        btnImprimir.Visible = False


    End Sub

    Public Overrides Sub _PMOInhabilitar()
        tbCodigo.ReadOnly = True
        tbCodBarra.ReadOnly = True
        tbCodProd.ReadOnly = True
        tbDescPro.ReadOnly = True
        tbDescCort.ReadOnly = True
        tbResponsable.ReadOnly = True
        cbLado.ReadOnly = True
        'cbLado.Enabled = False
        'cbLado.ButtonEnabled = False
        tbOrden.IsInputReadOnly = True
        btnSearch.Visible = False
        lbInf.Visible = False

        cbgrupo1.ReadOnly = True
        cbgrupo2.ReadOnly = True
        cbgrupo3.ReadOnly = True
        cbgrupo4.ReadOnly = True
        cbgrupo5.ReadOnly = True
        cbUMed.ReadOnly = True

        swEstado.IsReadOnly = True

        _prStyleJanus()
        JGrM_Buscador.Focus()
        Limpiar = False
        btExcel.Visible = True
        btnImprimir.Visible = True

    End Sub



    Public Sub _prSeleccionarCombo(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        If (CType(mCombo.DataSource, DataTable).Rows.Count > 0) Then
            mCombo.SelectedIndex = 0
        Else
            mCombo.SelectedIndex = -1
        End If
    End Sub


    Public Overrides Sub _PMOLimpiarErrores()
        MEP.Clear()
        tbCodBarra.BackColor = Color.White
        tbDescPro.BackColor = Color.White
        tbDescCort.BackColor = Color.White
        cbgrupo1.BackColor = Color.White
        cbgrupo2.BackColor = Color.White
        cbgrupo3.BackColor = Color.White
        cbgrupo4.BackColor = Color.White
        cbgrupo5.BackColor = Color.White
    End Sub



    Public Overrides Function _PMOModificarRegistro() As Boolean
        Dim res As Boolean

        Dim nameImage As String = JGrM_Buscador.GetValue("yfimg")
        If (Modificado = False) Then
            res = L_fnModificarProductoConteo(tbCodigo.Text, tbResponsable.Text.Trim, cbLado.Text.Trim, tbOrden.Value,
                                              gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina)
        Else
            res = L_fnModificarProductoConteo(tbCodigo.Text, tbResponsable.Text.Trim, cbLado.Text.Trim, tbOrden.Value,
                                              gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina)
        End If
        If res Then

            If (Modificado = True) Then
                Modificado = False
            End If
            nameImg = "Default.jpg"

            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "Código de Producto ".ToUpper + tbCodigo.Text + " modificado con Exito.".ToUpper,
                                      img, 3000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter)

        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "EL producto no pudo ser modificado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

        End If
        _PMInhabilitar()
        _PMPrimerRegistro()
        PanelNavegacion.Enabled = True
        btnImprimir.Visible = False
        Return res
    End Function



    Public Overrides Function _PMOValidarCampos() As Boolean
        Dim _ok As Boolean = True
        MEP.Clear()

        If swEstado.Value = False Then
            swEstado.BackColor = Color.Red
            AddHandler swEstado.KeyDown, AddressOf TextBox_KeyDown
            MEP.SetError(swEstado, "Solo puede asignar responsable a los productos Activos, este producto se encuentra pasivo".ToUpper)
            _ok = False
        Else
            swEstado.BackColor = Color.White
            MEP.SetError(swEstado, "")
        End If


        If tbResponsable.Text = String.Empty Then
            tbResponsable.BackColor = Color.Red
            AddHandler tbResponsable.KeyDown, AddressOf TextBox_KeyDown
            MEP.SetError(tbResponsable, "ingrese el responsable de conteo del producto!".ToUpper)
            _ok = False
        Else
            tbResponsable.BackColor = Color.White
            MEP.SetError(tbResponsable, "")
        End If

        If cbLado.Text = String.Empty Then
            cbLado.BackColor = Color.Red
            AddHandler cbLado.KeyDown, AddressOf TextBox_KeyDown
            MEP.SetError(cbLado, "ingrese el responsable de conteo del producto!".ToUpper)
            _ok = False
        Else
            cbLado.BackColor = Color.White
            MEP.SetError(cbLado, "")
        End If

        If tbOrden.Value < 0 Or tbOrden.Text = String.Empty Then
            tbOrden.BackColor = Color.Red
            AddHandler tbOrden.KeyDown, AddressOf TextBox_KeyDown
            MEP.SetError(tbOrden, "ingrese el responsable de conteo del producto!".ToUpper)
            _ok = False
        Else
            tbOrden.BackColor = Color.White
            MEP.SetError(tbOrden, "")
        End If

        MHighlighterFocus.UpdateHighlights()
        Return _ok
    End Function

    Public Overrides Function _PMOGetTablaBuscador() As DataTable
        Dim dtBuscador As DataTable = L_fnGeneralProductosConteo()
        Return dtBuscador
    End Function

    Public Overrides Function _PMOGetListEstructuraBuscador() As List(Of Modelo.Celda)
        Dim listEstCeldas As New List(Of Modelo.Celda)
        'a.yfnumi, a.yfcprod, a.yfcbarra, a.yfcdprod1, a.yfcdprod2, a.yfgr1, a.yfgr2, a.yfgr3, a.yfgr4,
        'a.yfMed, a.yfumin, a.yfusup, a.yfmstk, a.yfclot, a.yfsmin, a.yfap, a.yfimg, a.yffact, a.yfhact, a.yfuact
        listEstCeldas.Add(New Modelo.Celda("yfnumi", True, "Cod. Dynasys".ToUpper, 80))
        listEstCeldas.Add(New Modelo.Celda("yfcprod", True, "Cod. Delta".ToUpper, 90))
        listEstCeldas.Add(New Modelo.Celda("yfcdprod2", True, "Cod. Proveedor".ToUpper, 90))
        listEstCeldas.Add(New Modelo.Celda("yfcbarra", True, "Cod.Barra".ToUpper, 110))
        listEstCeldas.Add(New Modelo.Celda("yfcampo1", True, "Prefijo Rotación".ToUpper, 90))
        listEstCeldas.Add(New Modelo.Celda("yfcdprod1", True, "Descripción Producto".ToUpper, 270))
        listEstCeldas.Add(New Modelo.Celda("yfgr1", False))
        listEstCeldas.Add(New Modelo.Celda("yfgr2", False))
        listEstCeldas.Add(New Modelo.Celda("yfgr3", False))
        listEstCeldas.Add(New Modelo.Celda("yfgr4", False))
        listEstCeldas.Add(New Modelo.Celda("yfgr5", False))
        listEstCeldas.Add(New Modelo.Celda("yfMed", False))
        listEstCeldas.Add(New Modelo.Celda("yfumin", False))
        listEstCeldas.Add(New Modelo.Celda("yfusup", False))
        listEstCeldas.Add(New Modelo.Celda("yfvsup", True, "Unidades por CAJ/PAQ/DISP".ToUpper, 100, Format("0.00")))
        listEstCeldas.Add(New Modelo.Celda("yfcampo2", True, "Displays por CAJ/PAQ".ToUpper, 100, Format("0.00")))
        listEstCeldas.Add(New Modelo.Celda("yfucompra", False, "CodUni".ToUpper, 100))
        listEstCeldas.Add(New Modelo.Celda("UCompra", True, "UniCompra".ToUpper, 100))
        listEstCeldas.Add(New Modelo.Celda("yfmstk", False))
        listEstCeldas.Add(New Modelo.Celda("yfclot", False))
        listEstCeldas.Add(New Modelo.Celda("ygcodact", True, "Cod. Actividad Económica".ToUpper, 70))
        listEstCeldas.Add(New Modelo.Celda("ygcodu", True, "Cod. Unidad Medida".ToUpper, 70))
        listEstCeldas.Add(New Modelo.Celda("ygcodsin", True, "Código SIN".ToUpper, 70))
        listEstCeldas.Add(New Modelo.Celda("ygprecio", True, "Precio Prod.".ToUpper, 70, Format("0.00")))
        listEstCeldas.Add(New Modelo.Celda("yfsmin", True, "Stock Mínimo".ToUpper, 100))
        listEstCeldas.Add(New Modelo.Celda("yfap", False))
        listEstCeldas.Add(New Modelo.Celda("yfimg", False))
        listEstCeldas.Add(New Modelo.Celda("yffact", False))
        listEstCeldas.Add(New Modelo.Celda("yfhact", False))
        listEstCeldas.Add(New Modelo.Celda("yfuact", False))
        listEstCeldas.Add(New Modelo.Celda("grupo1", True, lbgrupo1.Text.Substring(0, lbgrupo1.Text.Length - 1).ToUpper, 120))
        listEstCeldas.Add(New Modelo.Celda("grupo2", True, lbgrupo2.Text.Substring(0, lbgrupo2.Text.Length - 1).ToUpper, 140))
        listEstCeldas.Add(New Modelo.Celda("grupo3", True, lbgrupo3.Text.Substring(0, lbgrupo3.Text.Length - 1).ToUpper, 140))
        listEstCeldas.Add(New Modelo.Celda("grupo4", True, lbgrupo4.Text.Substring(0, lbgrupo4.Text.Length - 1).ToUpper, 140))
        listEstCeldas.Add(New Modelo.Celda("grupo5", True, "CATEGORÍA".ToUpper, 200))
        listEstCeldas.Add(New Modelo.Celda("Umedida", True, "GRUPO DESCT.".ToUpper, 150))
        listEstCeldas.Add(New Modelo.Celda("UnidMin", True, "UniVenta".ToUpper, 100))
        listEstCeldas.Add(New Modelo.Celda("Umax", True, "UniMáxima".ToUpper, 100))
        listEstCeldas.Add(New Modelo.Celda("yfdetprod", True, "Descripción Detallada".ToUpper, 150))
        listEstCeldas.Add(New Modelo.Celda("inf", True, "Stock".ToUpper, 100))
        listEstCeldas.Add(New Modelo.Celda("yfresponsable", True, "Responsable".ToUpper, 110))
        listEstCeldas.Add(New Modelo.Celda("yflado", True, "Lado".ToUpper, 100))
        listEstCeldas.Add(New Modelo.Celda("yfordenacion", True, "Ordenación".ToUpper, 90))
        listEstCeldas.Add(New Modelo.Celda("Estado", True, "Estado".ToUpper, 100))

        Return listEstCeldas
    End Function

    Public Overrides Sub _PMOMostrarRegistro(_N As Integer)
        JGrM_Buscador.Row = _MPos
        'a.yfnumi, a.yfcprod, a.yfcbarra, a.yfcdprod1, a.yfcdprod2, a.yfgr1, a.yfgr2, a.yfgr3, a.yfgr4,
        'a.yfMed, a.yfumin, a.yfusup,yfvsup, a.yfmstk, a.yfclot, a.yfsmin, a.yfap, a.yfimg, a.yffact, a.yfhact, a.yfuact
        Dim dt As DataTable = CType(JGrM_Buscador.DataSource, DataTable)
        Try
            tbCodigo.Text = JGrM_Buscador.GetValue("yfnumi").ToString
        Catch ex As Exception
            Exit Sub
        End Try
        With JGrM_Buscador
            tbCodigo.Text = .GetValue("yfnumi").ToString
            tbCodProd.Text = .GetValue("yfcprod").ToString
            tbCodBarra.Text = .GetValue("yfcbarra").ToString
            tbDescPro.Text = .GetValue("yfcdprod1").ToString
            tbDescCort.Text = .GetValue("yfcdprod2").ToString
            tbResponsable.Text = .GetValue("yfresponsable").ToString
            cbLado.Text = .GetValue("yflado").ToString
            tbOrden.Value = .GetValue("yfordenacion")

            cbgrupo1.Value = .GetValue("yfgr1")
            cbgrupo2.Value = .GetValue("yfgr2")
            cbgrupo3.Value = .GetValue("yfgr3")
            cbgrupo4.Value = .GetValue("yfgr4")
            cbgrupo5.Value = .GetValue("yfgr5")
            cbUMed.Value = .GetValue("yfMed")

            swEstado.Value = .GetValue("yfap")
            lbFecha.Text = CType(.GetValue("yffact"), Date).ToString("dd/MM/yyyy")
            lbHora.Text = .GetValue("yfhact").ToString
            lbUsuario.Text = .GetValue("yfuact").ToString

        End With
        Dim name As String = JGrM_Buscador.GetValue("yfimg")


        LblPaginacion.Text = Str(_MPos + 1) + "/" + JGrM_Buscador.RowCount.ToString
    End Sub

#End Region

    Private Sub F1_Productos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()
    End Sub


    Private Sub ButtonX2_Click(sender As Object, e As EventArgs) Handles btExcel.Click
        _prCrearCarpetaReportes()
        Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
        If (P_ExportarExcel(RutaGlobal + "\Reporte\Reporte Productos")) Then
            L_fnExcelProductosConteoIndividual(gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina)
            ToastNotification.Show(Me, "EXPORTACIÓN DE LISTA DE PRODUCTOS EXITOSA..!!!",
                                       img, 2000,
                                       eToastGlowColor.Green,
                                       eToastPosition.BottomCenter)
        Else
            ToastNotification.Show(Me, "FALLÓ LA EXPORTACIÓN DE LISTA DE PRODUCTOS..!!!",
                                       My.Resources.WARNING, 2000,
                                       eToastGlowColor.Red,
                                       eToastPosition.BottomCenter)
        End If
    End Sub


    Public Function P_ExportarExcel(_ruta As String) As Boolean
        Dim _ubicacion As String
        'Dim _directorio As New FolderBrowserDialog

        If (1 = 1) Then 'If(_directorio.ShowDialog = Windows.Forms.DialogResult.OK) Then
            '_ubicacion = _directorio.SelectedPath
            _ubicacion = _ruta
            Try
                Dim _stream As Stream
                Dim _escritor As StreamWriter
                Dim _fila As Integer = JGrM_Buscador.GetRows.Length
                Dim _columna As Integer = JGrM_Buscador.RootTable.Columns.Count
                Dim _archivo As String = _ubicacion & "\ListaDeProductosConteo_" & Now.Date.Day &
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
            btnImprimir.Visible = False
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
        'Dim dt As DataTable = L_fnReporteproducto()

        'If Not IsNothing(P_Global.Visualizador) Then
        '    P_Global.Visualizador.Close()
        'End If

        'P_Global.Visualizador = New Visualizador

        'Dim objrep As New R_Productos
        ''' GenerarNro(_dt)
        '''objrep.SetDataSource(Dt1Kardex)
        'objrep.SetDataSource(dt)

        'P_Global.Visualizador.CrGeneral.ReportSource = objrep 'Comentar
        'P_Global.Visualizador.Show() 'Comentar
        'P_Global.Visualizador.BringToFront() 'Comentar

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
        Buscador()
    End Sub
    Private Sub Buscador()
        Try
            Dim dt As DataTable
            dt = L_fnMostrarUsuariosConteo()
            dt.Rows.Add(0, "NADIE")
            Dim listEstCeldas As New List(Of Modelo.Celda)
            listEstCeldas.Add(New Modelo.Celda("ydnumi,", True, "COD USUARIO", 120))
            listEstCeldas.Add(New Modelo.Celda("yduser", True, "USUARIO", 250))
            listEstCeldas.Add(New Modelo.Celda("ydest", False, "ESTADO", 50))

            Dim ef = New Efecto
            ef.tipo = 3
            ef.dt = dt
            ef.SeleclCol = 1
            ef.listEstCeldas = listEstCeldas
            ef.alto = 150
            ef.ancho = 200
            ef.Context = "Seleccione Responsable".ToUpper
            ef.SeleclCol = 1
            ef.ShowDialog()
            Dim bandera As Boolean = False
            bandera = ef.band
            If (bandera = True) Then
                Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
                If (IsNothing(Row)) Then
                    tbResponsable.Focus()
                    Return
                End If

                tbResponsable.Text = Row.Cells("yduser").Value

            End If

        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub
    Private Sub MostrarMensajeError(mensaje As String)
        ToastNotification.Show(Me,
                               mensaje.ToUpper,
                               My.Resources.WARNING,
                               3000,
                               eToastGlowColor.Red,
                               eToastPosition.TopCenter)

    End Sub

End Class