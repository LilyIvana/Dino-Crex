
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

Public Class F1_Productos
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
        Me.Text = "PRODUCTOS"
        'L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        _prMaxLength()
        _prCargarNameLabel()
        _prCargarComboLibreria(cbgrupo1, 1, 1)
        _prCargarComboLibreria(cbgrupo2, 1, 2)
        _prCargarComboLibreria(cbgrupo3, 1, 3)
        _prCargarComboLibreria(cbgrupo4, 1, 4)
        _prCargarComboLibreria(cbgrupo5, 1, 7)
        _prCargarComboLibreria(cbUMed, 1, 5)
        _prCargarComboLibreria(cbUniVenta, 1, 6)
        _prCargarComboLibreria(cbUnidMaxima, 1, 6)
        _prAsignarPermisos()
        armarGrillaDetalleProducto(0)

        ''Mostrar u ocultar el grupo 4(Familia)
        If gb_MostrarFamilia = 0 Then
            lbgrupo4.Visible = False
            cbgrupo4.Visible = False
        Else
            lbgrupo4.Visible = True
            cbgrupo4.Visible = True
        End If
        _PMIniciarTodo()

        'Ocultar/Mostrar ingreso de detalle de producto
        SuperTabItem_DetalleProducto.Visible = gb_DetalleProducto

        Dim blah As New Bitmap(New Bitmap(My.Resources.producto), 20, 20)
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico

        btnImprimir.Visible = False
        SuperTabControl_Imagenes_DetalleProducto.SelectedTabIndex = 2
    End Sub

    Private Sub armarGrillaDetalleProducto(numi As Integer)
        Dim dt As New DataTable
        dt = L_fnDetalleProducto(numi)
        dgjDetalleProducto.DataSource = dt
        dgjDetalleProducto.RetrieveStructure()
        dgjDetalleProducto.AlternatingColors = True

        With dgjDetalleProducto.RootTable.Columns("yfanumi")
            .Visible = False
        End With

        With dgjDetalleProducto.RootTable.Columns("yfayfnumi")
            .Visible = False
        End With
        With dgjDetalleProducto.RootTable.Columns("nro")
            .Caption = "Nro."
            .Width = 45
            .Visible = True
        End With
        With dgjDetalleProducto.RootTable.Columns("yfasim")
            .Caption = "Símbolo"
            .Width = 80
            .Visible = False
        End With
        With dgjDetalleProducto.RootTable.Columns("yfadesc")
            .Caption = "Descripción"
            .Width = 400
            .Visible = True
        End With
        With dgjDetalleProducto.RootTable.Columns("estado")
            .Visible = False
        End With
        With dgjDetalleProducto.RootTable.Columns.Add("delete", Janus.Windows.GridEX.ColumnType.Image)
            .HeaderAlignment = TextAlignment.Center
            .Image = New Bitmap(My.Resources.eliminar, New Size(15, 15))
            .Caption = "Quitar".ToUpper
            .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
            .Width = 80
            .Visible = False
        End With

        With dgjDetalleProducto
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
        End With
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
    Public Sub _prMaxLength()
        'tbCodProd.MaxLength = 25
        'tbCodBarra.MaxLength = 15
        'tbDescPro.MaxLength = 50
        'tbDescCort.MaxLength = 15
        cbgrupo1.MaxLength = 40
        cbgrupo2.MaxLength = 40
        cbgrupo3.MaxLength = 40
        cbgrupo4.MaxLength = 40
        cbUMed.MaxLength = 10
        cbUniVenta.MaxLength = 2
        cbUnidMaxima.MaxLength = 2
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
    Private Sub _fnMoverImagenRuta(Folder As String, name As String)
        'copio la imagen en la carpeta del sistema
        If (Not name.Equals("Default.jpg") And File.Exists(RutaTemporal + name)) Then

            Dim img As New Bitmap(New Bitmap(RutaTemporal + name), 500, 300)

            UsImg.pbImage.Image.Dispose()
            UsImg.pbImage.Image = Nothing
            Try
                My.Computer.FileSystem.CopyFile(RutaTemporal + name,
     Folder + name, overwrite:=True)

            Catch ex As System.IO.IOException


            End Try



        End If
    End Sub
#End Region
#Region "METODOS SOBRECARGADOS"

    Public Overrides Sub _PMOHabilitar()
        tbCodBarra.ReadOnly = False
        tbCodProd.ReadOnly = False
        tbDescPro.ReadOnly = False
        tbDescDet.ReadOnly = False
        tbDescCort.ReadOnly = False
        cbgrupo1.ReadOnly = False
        cbgrupo2.ReadOnly = False
        cbgrupo3.ReadOnly = False
        cbgrupo4.ReadOnly = False
        cbgrupo5.ReadOnly = False
        cbUMed.ReadOnly = False
        swEstado.IsReadOnly = False
        cbUniVenta.ReadOnly = False
        cbUnidMaxima.ReadOnly = False
        tbConversion.IsInputReadOnly = False

        CbAeconomica.ReadOnly = False
        CbUmedida.ReadOnly = False
        CbProdServ.ReadOnly = False
        TbPrecioPsifac.ReadOnly = False

        _prCrearCarpetaImagenes()
        _prCrearCarpetaTemporal()
        BtAdicionar.Visible = True
        tbStockMinimo.IsInputReadOnly = False
        btExcel.Visible = False
        btnImprimir.Visible = False
        dgjDetalleProducto.AllowEdit = InheritableBoolean.True
        dgjDetalleProducto.RootTable.Columns("delete").Visible = True
        adicionarFilaDetalleProducto()
    End Sub

    Public Overrides Sub _PMOInhabilitar()
        tbCodigo.ReadOnly = True
        tbCodBarra.ReadOnly = True
        tbCodProd.ReadOnly = True
        tbDescPro.ReadOnly = True
        tbDescCort.ReadOnly = True
        tbDescDet.ReadOnly = True
        cbgrupo1.ReadOnly = True
        cbgrupo2.ReadOnly = True
        cbgrupo3.ReadOnly = True
        cbgrupo4.ReadOnly = True
        cbgrupo5.ReadOnly = True
        cbUMed.ReadOnly = True
        swEstado.IsReadOnly = True
        cbUniVenta.ReadOnly = True
        cbUnidMaxima.ReadOnly = True
        tbConversion.IsInputReadOnly = True
        tbStockMinimo.IsInputReadOnly = True
        BtAdicionar.Visible = False

        CbAeconomica.ReadOnly = True
        CbUmedida.ReadOnly = True
        CbProdServ.ReadOnly = True
        TbPrecioPsifac.ReadOnly = True

        _prStyleJanus()
        JGrM_Buscador.Focus()
        Limpiar = False
        btExcel.Visible = True
        btnImprimir.Visible = True
        dgjDetalleProducto.AllowEdit = InheritableBoolean.False
        dgjDetalleProducto.RootTable.Columns("delete").Visible = False
    End Sub

    Public Overrides Sub _PMOLimpiar()
        tbCodigo.Clear()
        tbCodBarra.Clear()
        tbCodProd.Clear()
        tbDescPro.Clear()
        tbDescDet.Clear()
        tbDescCort.Clear()

        CbAeconomica.SelectedIndex = -1
        CbUmedida.SelectedIndex = -1
        CbProdServ.SelectedIndex = -1
        TbPrecioPsifac.Clear()

        If (Limpiar = False) Then
            _prSeleccionarCombo(cbgrupo1)
            _prSeleccionarCombo(cbgrupo2)
            _prSeleccionarCombo(cbgrupo3)
            _prSeleccionarCombo(cbgrupo4)
            _prSeleccionarCombo(cbgrupo5)
            _prSeleccionarCombo(cbUMed)
            _prSeleccionarCombo(cbUnidMaxima)
            _prSeleccionarCombo(cbUniVenta)
            swEstado.Value = True
            tbConversion.Value = 1

            tbStockMinimo.Value = 0
        End If
        tbCodProd.Focus()
        UsImg.pbImage.Image = My.Resources.pantalla

        armarGrillaDetalleProducto(0)
    End Sub

    Private Sub adicionarFilaDetalleProducto()
        CType(dgjDetalleProducto.DataSource, DataTable).Rows.Add({0, 0, 1, "", "", 0})
        Dim i As Integer = 0
        For Each fila As DataRow In CType(dgjDetalleProducto.DataSource, DataTable).Rows
            fila.Item("nro") = i + 1
            i += 1
        Next
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
        tbDescPro.BackColor = Color.White
        tbDescDet.BackColor = Color.White
        tbDescCort.BackColor = Color.White
        cbgrupo1.BackColor = Color.White
        cbgrupo2.BackColor = Color.White
        cbgrupo3.BackColor = Color.White
        cbgrupo4.BackColor = Color.White
        cbgrupo5.BackColor = Color.White
    End Sub

    Public Overrides Function _PMOGrabarRegistro() As Boolean

        Dim res As Boolean
        Dim dtNumi = L_fnConsultarNumi()
        tbCodigo.Text = dtNumi.Rows(0).Item("newNumi")

        'Dim succes = Homologar(tokenSifac)
        'If succes = 200 Then
        res = L_fnGrabarProducto(tbCodigo.Text, tbCodProd.Text, tbCodBarra.Text, tbDescPro.Text,
                                                tbDescCort.Text, cbgrupo1.Value, cbgrupo2.Value, cbgrupo3.Value,
                                                cbgrupo4.Value, cbUMed.Value, cbUniVenta.Value, cbUnidMaxima.Value,
                                                tbConversion.Text,
                                                IIf(tbStockMinimo.Text = String.Empty, 0, tbStockMinimo.Text),
                                                IIf(swEstado.Value = True, 1, 0), nameImg,
                                                quitarUltimaFilaVacia(CType(dgjDetalleProducto.DataSource, DataTable).DefaultView.ToTable(False, "yfanumi", "yfayfnumi", "yfasim", "yfadesc", "estado")),
                                                tbDescDet.Text, cbgrupo5.Value, CbAeconomica.Value, CbUmedida.Value, CbProdServ.Value, TbPrecioPsifac.Text)

        'Else
        '    res = False
        'End If
        If res Then
            Modificado = False
            _fnMoverImagenRuta(RutaGlobal + "\Imagenes\Imagenes ProductoDino", nameImg)
            nameImg = "Default.jpg"

            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "Código de Producto ".ToUpper + tbCodigo.Text + " Grabado con Exito.".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter
                                      )
            tbCodigo.Focus()
            Limpiar = True
        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "El producto no pudo ser insertado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

        End If
        Return res

    End Function

    Public Overrides Function _PMOModificarRegistro() As Boolean
        Dim res As Boolean

        Dim nameImage As String = JGrM_Buscador.GetValue("yfimg")
        If (Modificado = False) Then
            res = L_fnModificarProducto(tbCodigo.Text, tbCodProd.Text, tbCodBarra.Text, tbDescPro.Text, tbDescCort.Text, cbgrupo1.Value, cbgrupo2.Value, cbgrupo3.Value,
                                        cbgrupo4.Value, cbUMed.Value, cbUniVenta.Value, cbUnidMaxima.Value, tbConversion.Text,
                                        IIf(tbStockMinimo.Text = String.Empty, 0, tbStockMinimo.Text), IIf(swEstado.Value = True, 1, 0), nameImage,
                                        quitarUltimaFilaVacia(CType(dgjDetalleProducto.DataSource, DataTable).DefaultView.ToTable(False, "yfanumi", "yfayfnumi", "yfasim", "yfadesc", "estado")),
                                        tbDescDet.Text, cbgrupo5.Value, CbAeconomica.Value, CbUmedida.Value, CbProdServ.Value, TbPrecioPsifac.Text)
        Else
            res = L_fnModificarProducto(tbCodigo.Text, tbCodProd.Text, tbCodBarra.Text, tbDescPro.Text, tbDescCort.Text, cbgrupo1.Value, cbgrupo2.Value, cbgrupo3.Value,
                                        cbgrupo4.Value, cbUMed.Value, cbUniVenta.Value, cbUnidMaxima.Value, tbConversion.Text,
                                        tbStockMinimo.Text, IIf(swEstado.Value = True, 1, 0), nameImg,
                                        quitarUltimaFilaVacia(CType(dgjDetalleProducto.DataSource, DataTable).DefaultView.ToTable(False, "yfanumi", "yfayfnumi", "yfasim", "yfadesc", "estado")),
                                        tbDescDet.Text, cbgrupo5.Value, CbAeconomica.Value, CbUmedida.Value, CbProdServ.Value, TbPrecioPsifac.Text)
        End If
        If res Then

            If (Modificado = True) Then
                _fnMoverImagenRuta(RutaGlobal + "\Imagenes\Imagenes ProductoDino", nameImg)
                Modificado = False
            End If
            nameImg = "Default.jpg"

            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "Código de Producto ".ToUpper + tbCodigo.Text + " modificado con Exito.".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter)

        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "EL producto no pudo ser modificado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

        End If
        _PMInhabilitar()
        _PMPrimerRegistro()
        PanelNavegacion.Enabled = True
        Return res
    End Function

    Private Function quitarUltimaFilaVacia(tabla As DataTable) As DataTable
        If tabla.Rows.Count > 0 Then
            If (tabla.Rows(tabla.Rows.Count - 1).Item("yfadesc").ToString() = String.Empty) Then
                tabla.Rows.RemoveAt(tabla.Rows.Count - 1)
                tabla.AcceptChanges()
            End If
        End If
        Return tabla
    End Function

    Public Sub _PrEliminarImage()

        If (Not (_fnActionNuevo()) And (File.Exists(RutaGlobal + "\Imagenes\Imagenes ProductoDino\Imagen_" + tbCodigo.Text + ".jpg"))) Then
            UsImg.pbImage.Image.Dispose()
            UsImg.pbImage.Image = Nothing
            Try
                My.Computer.FileSystem.DeleteFile(RutaGlobal + "\Imagenes\Imagenes ProductoDino\Imagen_" + tbCodigo.Text + ".jpg")
            Catch ex As Exception

            End Try


        End If
    End Sub
    Public Function _fnActionNuevo() As Boolean
        Return tbCodigo.Text = String.Empty And tbCodBarra.ReadOnly = False
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
            Dim res As Boolean = L_fnEliminarProducto(tbCodigo.Text, mensajeError)
            If res Then
                _PrEliminarImage()

                Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)

                ToastNotification.Show(Me, "Código de Producto ".ToUpper + tbCodigo.Text + " eliminado con Exito.".ToUpper,
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

        If tbDescPro.Text = String.Empty Then
            tbDescPro.BackColor = Color.Red
            AddHandler tbDescPro.KeyDown, AddressOf TextBox_KeyDown
            MEP.SetError(tbDescPro, "ingrese el descripcion del producto!".ToUpper)
            _ok = False
        Else
            tbDescPro.BackColor = Color.White
            MEP.SetError(tbDescPro, "")
        End If
        'If tbDescCort.Text = String.Empty Then
        '    tbDescCort.BackColor = Color.Red
        '    MEP.SetError(tbDescCort, "ingrese la Descripcion Corta del Producto!".ToUpper)
        '    AddHandler tbDescCort.KeyDown, AddressOf TextBox_KeyDown
        '    _ok = False
        'Else
        '    tbDescCort.BackColor = Color.White
        '    MEP.SetError(tbDescCort, "")
        'End If

        If cbgrupo1.SelectedIndex < 0 Then
            cbgrupo1.BackColor = Color.Red
            MEP.SetError(cbgrupo1, "Selecciones grupo del producto!".ToUpper)
            _ok = False
        Else
            cbgrupo1.BackColor = Color.White
            MEP.SetError(cbgrupo1, "")
        End If

        If cbgrupo2.SelectedIndex < 0 Then
            cbgrupo2.BackColor = Color.Red
            MEP.SetError(cbgrupo2, "Selecciones grupo del producto!".ToUpper)
            _ok = False
        Else
            cbgrupo2.BackColor = Color.White
            MEP.SetError(cbgrupo2, "")
        End If
        If cbgrupo3.SelectedIndex < 0 Then
            cbgrupo3.BackColor = Color.Red
            MEP.SetError(cbgrupo3, "Selecciones grupo del producto!".ToUpper)
            _ok = False
        Else
            cbgrupo3.BackColor = Color.White
            MEP.SetError(cbgrupo3, "")
        End If
        If cbgrupo4.SelectedIndex < 0 Then
            cbgrupo4.BackColor = Color.Red
            MEP.SetError(cbgrupo4, "Selecciones grupo del producto!".ToUpper)
            _ok = False
        Else
            cbgrupo4.BackColor = Color.White
            MEP.SetError(cbgrupo4, "")
        End If
        If cbUMed.SelectedIndex < 0 Then
            cbUMed.BackColor = Color.Red
            MEP.SetError(cbUMed, "Selecciones Unidad De Medida Del Producto!".ToUpper)
            _ok = False
        Else
            cbUMed.BackColor = Color.White
            MEP.SetError(cbUMed, "")
        End If
        If swEstado.Value = False Then
            tbCodBarra.Text = ""
        Else
            tbCodBarra.BackColor = Color.White
            MEP.SetError(tbCodBarra, "")
        End If
        If tbCodBarra.Text <> String.Empty Then
            If CodBarras <> tbCodBarra.Text Then
                Dim dt = L_fnValidarCodBarras(tbCodBarra.Text)
                If dt.Rows.Count > 0 Then
                    tbCodBarra.BackColor = Color.Red
                    MEP.SetError(tbCodBarra, "Este código de barras ya existe en otro producto!".ToUpper)
                    _ok = False
                Else
                    tbCodBarra.BackColor = Color.White
                    MEP.SetError(tbCodBarra, "")
                End If
            Else
                tbCodBarra.BackColor = Color.White
                MEP.SetError(tbCodBarra, "")
            End If
        Else
            tbCodBarra.BackColor = Color.White
            MEP.SetError(tbCodBarra, "")
        End If

        ''Sifac
        If CbAeconomica.SelectedIndex < 0 Then
            CbAeconomica.BackColor = Color.Red
            MEP.SetError(CbAeconomica, "Seleccione la Actividad Económica!".ToUpper)
            _ok = False
        Else
            CbAeconomica.BackColor = Color.White
            MEP.SetError(CbAeconomica, "")
        End If
        If CbUmedida.SelectedIndex < 0 Then
            CbUmedida.BackColor = Color.Red
            MEP.SetError(CbUmedida, "Seleccione la Unidad de Medida!".ToUpper)
            _ok = False
        Else
            CbUmedida.BackColor = Color.White
            MEP.SetError(CbUmedida, "")
        End If
        If CbProdServ.SelectedIndex < 0 Then
            CbProdServ.BackColor = Color.Red
            MEP.SetError(CbProdServ, "Seleccione el Codigo SIN!".ToUpper)
            _ok = False
        Else
            CbProdServ.BackColor = Color.White
            MEP.SetError(CbProdServ, "")
        End If
        If TbPrecioPsifac.Text = String.Empty Or TbPrecioPsifac.Text <= "0" Then
            TbPrecioPsifac.BackColor = Color.Red
            MEP.SetError(TbPrecioPsifac, "Introduzca precio mayor a 0!".ToUpper)
            _ok = False
        Else
            TbPrecioPsifac.BackColor = Color.White
            MEP.SetError(TbPrecioPsifac, "")
        End If





        MHighlighterFocus.UpdateHighlights()
        Return _ok
    End Function

    Public Overrides Function _PMOGetTablaBuscador() As DataTable
        Dim dtBuscador As DataTable = L_fnGeneralProductos()
        Return dtBuscador
    End Function

    Public Overrides Function _PMOGetListEstructuraBuscador() As List(Of Modelo.Celda)
        Dim listEstCeldas As New List(Of Modelo.Celda)
        'a.yfnumi, a.yfcprod, a.yfcbarra, a.yfcdprod1, a.yfcdprod2, a.yfgr1, a.yfgr2, a.yfgr3, a.yfgr4,
        'a.yfMed, a.yfumin, a.yfusup, a.yfmstk, a.yfclot, a.yfsmin, a.yfap, a.yfimg, a.yffact, a.yfhact, a.yfuact
        listEstCeldas.Add(New Modelo.Celda("yfnumi", True, "Código".ToUpper, 80))
        listEstCeldas.Add(New Modelo.Celda("yfcprod", True, "Cod.Producto".ToUpper, 100))
        listEstCeldas.Add(New Modelo.Celda("yfcdprod2", True, "Cod. Proveedor".ToUpper, 140))
        listEstCeldas.Add(New Modelo.Celda("yfcbarra", True, "Cod.Barra".ToUpper, 140))
        listEstCeldas.Add(New Modelo.Celda("yfcdprod1", True, "Descripcion Producto".ToUpper, 250))
        listEstCeldas.Add(New Modelo.Celda("yfgr1", False))
        listEstCeldas.Add(New Modelo.Celda("yfgr2", False))
        listEstCeldas.Add(New Modelo.Celda("yfgr3", False))
        listEstCeldas.Add(New Modelo.Celda("yfgr4", False))
        listEstCeldas.Add(New Modelo.Celda("yfgr5", False))
        listEstCeldas.Add(New Modelo.Celda("yfMed", False))
        listEstCeldas.Add(New Modelo.Celda("yfumin", False))
        listEstCeldas.Add(New Modelo.Celda("yfusup", False))
        listEstCeldas.Add(New Modelo.Celda("yfvsup", True, "Conversión".ToUpper, 100, Format("0.00")))
        listEstCeldas.Add(New Modelo.Celda("yfmstk", False))
        listEstCeldas.Add(New Modelo.Celda("yfclot", False))
        listEstCeldas.Add(New Modelo.Celda("ygcodact", False))
        listEstCeldas.Add(New Modelo.Celda("ygcodu", False))
        listEstCeldas.Add(New Modelo.Celda("ygcodsin", False))
        listEstCeldas.Add(New Modelo.Celda("ygprecio", False))
        listEstCeldas.Add(New Modelo.Celda("yfsmin", True, "Stock Mínimo".ToUpper, 100))
        listEstCeldas.Add(New Modelo.Celda("yfap", False))
        listEstCeldas.Add(New Modelo.Celda("yfimg", False))
        listEstCeldas.Add(New Modelo.Celda("yffact", False))
        listEstCeldas.Add(New Modelo.Celda("yfhact", False))
        listEstCeldas.Add(New Modelo.Celda("yfuact", False))
        listEstCeldas.Add(New Modelo.Celda("grupo1", True, lbgrupo1.Text.Substring(0, lbgrupo1.Text.Length - 1).ToUpper, 150))
        listEstCeldas.Add(New Modelo.Celda("grupo2", True, lbgrupo2.Text.Substring(0, lbgrupo2.Text.Length - 1).ToUpper, 150))
        listEstCeldas.Add(New Modelo.Celda("grupo3", True, lbgrupo3.Text.Substring(0, lbgrupo3.Text.Length - 1).ToUpper, 150))
        listEstCeldas.Add(New Modelo.Celda("grupo4", True, lbgrupo4.Text.Substring(0, lbgrupo4.Text.Length - 1).ToUpper, 150))
        listEstCeldas.Add(New Modelo.Celda("grupo5", True, "CATEGORÍA".ToUpper, 200))
        listEstCeldas.Add(New Modelo.Celda("Umedida", True, "GRUPO DESCT.".ToUpper, 150))
        listEstCeldas.Add(New Modelo.Celda("UnidMin", True, "UniVenta".ToUpper, 100))
        listEstCeldas.Add(New Modelo.Celda("Umax", True, "UniMaxima".ToUpper, 100))
        listEstCeldas.Add(New Modelo.Celda("yfdetprod", False, "Descripcion".ToUpper, 150))
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
            tbDescDet.Text = .GetValue("yfdetprod").ToString
            cbgrupo1.Value = .GetValue("yfgr1")
            cbgrupo2.Value = .GetValue("yfgr2")
            cbgrupo3.Value = .GetValue("yfgr3")
            cbgrupo4.Value = .GetValue("yfgr4")
            cbgrupo5.Value = .GetValue("yfgr5")
            cbUMed.Value = .GetValue("yfMed")
            cbUniVenta.Value = .GetValue("yfumin")
            cbUnidMaxima.Value = .GetValue("yfusup")
            tbConversion.Value = .GetValue("yfvsup")
            tbStockMinimo.Text = .GetValue("yfsmin")
            swEstado.Value = .GetValue("yfap")
            lbFecha.Text = CType(.GetValue("yffact"), Date).ToString("dd/MM/yyyy")
            lbHora.Text = .GetValue("yfhact").ToString
            lbUsuario.Text = .GetValue("yfuact").ToString

            CbAeconomica.Value = .GetValue("ygcodact")
            CbUmedida.Value = .GetValue("ygcodu")
            CbProdServ.Value = .GetValue("ygcodsin")
            TbPrecioPsifac.Text = .GetValue("ygprecio").ToString

        End With
        Dim name As String = JGrM_Buscador.GetValue("yfimg")
        If name.Equals("Default.jpg") Or Not File.Exists(RutaGlobal + "\Imagenes\Imagenes ProductoDino" + name) Then

            Dim im As New Bitmap(My.Resources.pantalla)
            UsImg.pbImage.Image = im
        Else
            If (File.Exists(RutaGlobal + "\Imagenes\Imagenes ProductoDino" + name)) Then
                Dim Bin As New MemoryStream
                Dim im As New Bitmap(New Bitmap(RutaGlobal + "\Imagenes\Imagenes ProductoDino" + name))
                im.Save(Bin, System.Drawing.Imaging.ImageFormat.Jpeg)
                UsImg.pbImage.SizeMode = PictureBoxSizeMode.StretchImage
                UsImg.pbImage.Image = Image.FromStream(Bin)
                Bin.Dispose()

            End If
        End If
        If (gb_DetalleProducto) Then
            armarGrillaDetalleProducto(CInt(tbCodigo.Text))
        End If
        LblPaginacion.Text = Str(_MPos + 1) + "/" + JGrM_Buscador.RowCount.ToString
    End Sub
    Public Overrides Sub _PMOHabilitarFocus()

        'With MHighlighterFocus
        '    .SetHighlightOnFocus(tbCodigo, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(tbCodProd, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(tbStockMinimo, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(tbCodBarra, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)

        '    .SetHighlightOnFocus(tbDescPro, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(tbDescCort, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(cbgrupo1, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(cbgrupo2, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(cbgrupo3, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(cbgrupo4, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(cbUMed, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(swEstado, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(cbUniVenta, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(cbUnidMaxima, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(tbConversion, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)


        'End With
    End Sub

#End Region

    Private Sub F1_Productos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tokenSifac = F0_Venta2.ObtToken()
        UnidadMedida(tokenSifac)
        CbUmedida.SelectedIndex = -1
        ActividadesEconomicas(tokenSifac)
        CbAeconomica.SelectedIndex = -1
        ListarProductoServicio(tokenSifac)
        CbProdServ.SelectedIndex = -1
        _prIniciarTodo()
    End Sub



    Private Function _fnCopiarImagenRutaDefinida() As String
        'copio la imagen en la carpeta del sistema

        Dim file As New OpenFileDialog()
        file.Filter = "Ficheros JPG o JPEG o PNG|*.jpg;*.jpeg;*.png" &
                      "|Ficheros GIF|*.gif" &
                      "|Ficheros BMP|*.bmp" &
                      "|Ficheros PNG|*.png" &
                      "|Ficheros TIFF|*.tif"
        If file.ShowDialog() = DialogResult.OK Then
            Dim ruta As String = file.FileName


            If file.CheckFileExists = True Then
                Dim img As New Bitmap(New Bitmap(ruta))
                Dim imgM As New Bitmap(New Bitmap(ruta))
                Dim Bin As New MemoryStream
                imgM.Save(Bin, System.Drawing.Imaging.ImageFormat.Jpeg)
                Dim a As Object = file.GetType.ToString
                If (_fnActionNuevo()) Then

                    Dim mayor As Integer
                    mayor = JGrM_Buscador.GetTotal(JGrM_Buscador.RootTable.Columns("yfnumi"), AggregateFunction.Max)
                    nameImg = "\Imagen_" + Str(mayor + 1).Trim + ".jpg"
                    UsImg.pbImage.SizeMode = PictureBoxSizeMode.StretchImage
                    UsImg.pbImage.Image = Image.FromStream(Bin)

                    img.Save(RutaTemporal + nameImg, System.Drawing.Imaging.ImageFormat.Jpeg)
                    img.Dispose()
                Else

                    nameImg = "\Imagen_" + Str(tbCodigo.Text).Trim + ".jpg"


                    UsImg.pbImage.Image = Image.FromStream(Bin)
                    img.Save(RutaTemporal + nameImg, System.Drawing.Imaging.ImageFormat.Jpeg)
                    Modificado = True
                    img.Dispose()

                End If
            End If

            Return nameImg
        End If

        Return "default.jpg"
    End Function

    Private Sub BtAdicionar_Click(sender As Object, e As EventArgs) Handles BtAdicionar.Click
        _fnCopiarImagenRutaDefinida()
        btnGrabar.Focus()
    End Sub

    Private Sub cbgrupo3_ValueChanged(sender As Object, e As EventArgs) Handles cbgrupo3.ValueChanged
        If cbgrupo3.SelectedIndex < 0 And cbgrupo3.Text <> String.Empty Then
            btgrupo3.Visible = True
        Else
            btgrupo3.Visible = False
        End If
    End Sub

    Private Sub cbgrupo4_ValueChanged(sender As Object, e As EventArgs) Handles cbgrupo4.ValueChanged
        If cbgrupo4.SelectedIndex < 0 And cbgrupo4.Text <> String.Empty Then
            btgrupo4.Visible = True
        Else
            btgrupo4.Visible = False
        End If
    End Sub

    Private Sub cbgrupo1_ValueChanged(sender As Object, e As EventArgs) Handles cbgrupo1.ValueChanged
        If cbgrupo1.SelectedIndex < 0 And cbgrupo1.Text <> String.Empty Then
            btgrupo1.Visible = True
        Else
            btgrupo1.Visible = False
        End If
    End Sub

    Private Sub cbgrupo2_ValueChanged(sender As Object, e As EventArgs) Handles cbgrupo2.ValueChanged
        If cbgrupo2.SelectedIndex < 0 And cbgrupo2.Text <> String.Empty Then
            btgrupo2.Visible = True
        Else
            btgrupo2.Visible = False
        End If
    End Sub

    Private Sub cbUMed_ValueChanged(sender As Object, e As EventArgs) Handles cbUMed.ValueChanged
        If cbUMed.SelectedIndex < 0 And cbUMed.Text <> String.Empty Then
            btUMedida.Visible = True
        Else
            btUMedida.Visible = False
        End If
    End Sub

    Private Sub cbUniVenta_ValueChanged(sender As Object, e As EventArgs) Handles cbUniVenta.ValueChanged
        If cbUniVenta.SelectedIndex < 0 And cbUniVenta.Text <> String.Empty Then
            btUniVenta.Visible = True
        Else
            btUniVenta.Visible = False
        End If
    End Sub

    Private Sub cbUnidMaxima_ValueChanged(sender As Object, e As EventArgs) Handles cbUnidMaxima.ValueChanged
        If cbUnidMaxima.SelectedIndex < 0 And cbUnidMaxima.Text <> String.Empty Then
            btUniMaxima.Visible = True
        Else
            btUniMaxima.Visible = False
        End If
    End Sub

    Private Sub btgrupo3_Click(sender As Object, e As EventArgs) Handles btgrupo3.Click
        Dim numi As String = ""

        If L_prLibreriaGrabarGrupos(numi, "1", "3", cbgrupo3.Text, "", 103) Then
            _prCargarComboLibreria(cbgrupo3, "1", "3")
            cbgrupo3.SelectedIndex = CType(cbgrupo3.DataSource, DataTable).Rows.Count - 1
        End If
    End Sub

    Private Sub btgrupo1_Click(sender As Object, e As EventArgs) Handles btgrupo1.Click
        Dim numi As String = ""

        If L_prLibreriaGrabarGrupos(numi, "1", "1", cbgrupo1.Text, "", 0) Then
            _prCargarComboLibreria(cbgrupo1, "1", "1")
            cbgrupo1.SelectedIndex = CType(cbgrupo1.DataSource, DataTable).Rows.Count - 1
        End If
    End Sub

    Private Sub btgrupo2_Click(sender As Object, e As EventArgs) Handles btgrupo2.Click
        Dim numi As String = ""

        If L_prLibreriaGrabarGrupos(numi, "1", "2", cbgrupo2.Text, "", 102) Then
            _prCargarComboLibreria(cbgrupo2, "1", "2")
            cbgrupo2.SelectedIndex = CType(cbgrupo2.DataSource, DataTable).Rows.Count - 1
        End If
    End Sub

    Private Sub btgrupo4_Click(sender As Object, e As EventArgs) Handles btgrupo4.Click
        Dim numi As String = ""

        If L_prLibreriaGrabarGrupos(numi, "1", "4", cbgrupo4.Text, "", 104) Then
            _prCargarComboLibreria(cbgrupo4, "1", "4")
            cbgrupo4.SelectedIndex = CType(cbgrupo4.DataSource, DataTable).Rows.Count - 1
        End If
    End Sub

    Private Sub btUMedida_Click(sender As Object, e As EventArgs) Handles btUMedida.Click
        Dim numi As String = ""

        If L_prLibreriaGrabarGrupos(numi, "1", "5", cbUMed.Text, "", 105) Then
            _prCargarComboLibreria(cbUMed, "1", "5")
            cbUMed.SelectedIndex = CType(cbUMed.DataSource, DataTable).Rows.Count - 1
        End If
    End Sub

    Private Sub btUniVenta_Click(sender As Object, e As EventArgs) Handles btUniVenta.Click
        Dim numi As String = ""

        If L_prLibreriaGrabarGrupos(numi, "1", "6", cbUniVenta.Text, "", 106) Then
            _prCargarComboLibreria(cbUniVenta, "1", "6")
            _prCargarComboLibreria(cbUnidMaxima, "1", "6")
            cbUniVenta.SelectedIndex = CType(cbUniVenta.DataSource, DataTable).Rows.Count - 1
        End If
    End Sub

    Private Sub btUniMaxima_Click(sender As Object, e As EventArgs) Handles btUniMaxima.Click
        Dim numi As String = ""

        If L_prLibreriaGrabarGrupos(numi, "1", "6", cbUnidMaxima.Text, "", 106) Then
            _prCargarComboLibreria(cbUnidMaxima, "1", "6")
            _prCargarComboLibreria(cbUniVenta, "1", "6")
            cbUnidMaxima.SelectedIndex = CType(cbUnidMaxima.DataSource, DataTable).Rows.Count - 1
        End If
    End Sub
    Private Sub ButtonX2_Click(sender As Object, e As EventArgs) Handles btExcel.Click
        _prCrearCarpetaReportes()
        Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
        If (P_ExportarExcel(RutaGlobal + "\Reporte\Reporte Productos")) Then
            ToastNotification.Show(Me, "EXPORTACIÓN DE LISTA DE PRODUCTOS EXITOSA..!!!",
                                       img, 2000,
                                       eToastGlowColor.Green,
                                       eToastPosition.BottomCenter)
        Else
            ToastNotification.Show(Me, "FALLO AL EXPORTACIÓN DE LISTA DE PRODUCTOS..!!!",
                                       My.Resources.WARNING, 2000,
                                       eToastGlowColor.Red,
                                       eToastPosition.BottomLeft)
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
                Dim _archivo As String = _ubicacion & "\ListaDeProductos_" & Now.Date.Day &
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
    Private Sub dgjDetalleProducto_EditingCell(sender As Object, e As EditingCellEventArgs) Handles dgjDetalleProducto.EditingCell
        If (e.Column.Index = dgjDetalleProducto.RootTable.Columns("yfadesc").Index) Then
            e.Cancel = False
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub dgjDetalleProducto_CellEdited(sender As Object, e As ColumnActionEventArgs) Handles dgjDetalleProducto.CellEdited
        If (e.Column.Index = dgjDetalleProducto.RootTable.Columns("yfadesc").Index) Then
            If (dgjDetalleProducto.GetValue("yfadesc").ToString.Length > 100) Then
                ToastNotification.Show(Me, "La descripción no tiene que ser mayor a 100 caracteres".ToUpper,
                                      My.Resources.WARNING, 2000,
                                      eToastGlowColor.Red,
                                      eToastPosition.TopCenter
                                      )
                dgjDetalleProducto.SetValue("yfadesc", dgjDetalleProducto.GetValue("yfadesc").ToString.Substring(0, 100))
                dgjDetalleProducto.DataChanged = True
            End If

            'Si el estado es igual a 1, cambiarlo a 2
            If (CInt(dgjDetalleProducto.GetValue("estado")) = 1) Then
                dgjDetalleProducto.SetValue("estado", 2)
                dgjDetalleProducto.DataChanged = True
            End If

            'Si el la ultima fila, agregar una fila nueva
            If (dgjDetalleProducto.Row = dgjDetalleProducto.RowCount - 1) Then
                adicionarFilaDetalleProducto()
            End If
        End If
    End Sub

    Private Sub dgjDetalleProducto_Click(sender As Object, e As EventArgs) Handles dgjDetalleProducto.Click
        If (dgjDetalleProducto.CurrentColumn.Key = "delete") Then
            dgjDetalleProducto.CurrentRow.Delete()
            dgjDetalleProducto.Refetch()
            dgjDetalleProducto.Refresh()
        End If
    End Sub

    Private Sub cbgrupo5_ValueChanged(sender As Object, e As EventArgs) Handles cbgrupo5.ValueChanged
        If cbgrupo5.SelectedIndex < 0 And cbgrupo5.Text <> String.Empty Then
            btgrupo5.Visible = True
        Else
            btgrupo5.Visible = False
        End If
    End Sub

    Private Sub btgrupo5_Click(sender As Object, e As EventArgs) Handles btgrupo5.Click
        Dim numi As String = ""

        If L_prLibreriaGrabarGrupos(numi, "1", "7", cbgrupo5.Text, "", 0) Then
            _prCargarComboLibreria(cbgrupo5, "1", "7")
            cbgrupo5.SelectedIndex = CType(cbgrupo5.DataSource, DataTable).Rows.Count - 1
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

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        CodBarras = tbCodBarra.Text
    End Sub



    ''Sifac
    Public Function ActividadesEconomicas(tokenObtenido)

        Dim api = New DBApi()

        Dim url = "https://crex.sifac.nwc.com.bo/api/v2/actividades-economicas"

        Dim headers = New List(Of Parametro) From {
            New Parametro("Authorization", "Bearer " + tokenObtenido),
            New Parametro("Content-Type", "Accept:application/json; charset=utf-8")
        }

        Dim parametros = New List(Of Parametro)

        Dim response = api.MGet(url, headers, parametros)

        Dim result = JsonConvert.DeserializeObject(Of Aecono)(response)

        With CbAeconomica
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("codigoActividad").Width = 70
            .DropDownList.Columns("codigoActividad").Caption = "COD"
            .DropDownList.Columns.Add("descripcion").Width = 1000
            .DropDownList.Columns("descripcion").Caption = "DESCRIPCION"
            .ValueMember = "codigoActividad"
            .DisplayMember = "descripcion"
            .DataSource = result.data
            .Refresh()
        End With

    End Function

    Public Function UnidadMedida(tokenObtenido)

        Dim api = New DBApi()

        Dim url = "https://crex.sifac.nwc.com.bo/api/v2/unidad-medida"

        Dim headers = New List(Of Parametro) From {
            New Parametro("Authorization", "Bearer " + tokenObtenido),
            New Parametro("Content-Type", "Accept:application/json; charset=utf-8")
        }

        Dim parametros = New List(Of Parametro)

        Dim response = api.MGet(url, headers, parametros)

        Dim result = JsonConvert.DeserializeObject(Of Umedida)(response)


        With CbUmedida
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("codigoClasificador").Width = 70
            .DropDownList.Columns("codigoClasificador").Caption = "COD"
            .DropDownList.Columns.Add("descripcion").Width = 350
            .DropDownList.Columns("descripcion").Caption = "DESCRIPCION"
            .ValueMember = "codigoClasificador"
            .DisplayMember = "descripcion"
            .DataSource = result.data
            .Refresh()
        End With
        Return ""
    End Function
    Public Function ListarProductoServicio(tokenObtenido As String, Optional ae As Integer = 5)
        Dim pagina = "1"
        Dim cantpag = "1000"
        Dim api = New DBApi()

        Dim url = "https://crex.sifac.nwc.com.bo/api/v2/productos-servicios/1/1000"

        Dim headers = New List(Of Parametro) From {
            New Parametro("Authorization", "Bearer " + tokenObtenido),
            New Parametro("Content-Type", "Accept:application/json; charset=utf-8")
        }

        Dim parametros = New List(Of Parametro)

        Dim response = api.MGet(url, headers, parametros)

        Dim result = JsonConvert.DeserializeObject(Of ProServ)(response)
        Dim resultError = JsonConvert.DeserializeObject(Of ProServ1)(response)

        Dim codigo = result.meta.code



        'Dim json = JsonConvert.SerializeObject(result)
        'MsgBox(json)
        'For Each y In result.data

        'Next
        ' Mid(cadena, 8, 6)
        If codigo = 200 Then

            With CbProdServ
                .DropDownList.Columns.Clear()
                .DropDownList.Columns.Add("codigoActividad").Width = 80
                .DropDownList.Columns("codigoActividad").Caption = "COD"
                .DropDownList.Columns.Add("codigoProducto").Width = 80
                .DropDownList.Columns("codigoProducto").Caption = "COD. PROD"
                .DropDownList.Columns.Add("descripcionProducto").Width = 1150
                .DropDownList.Columns("descripcionProducto").Caption = "DESCRIPCION"
                .ValueMember = "codigoProducto"
                .DisplayMember = "descripcionProducto"
                .DataSource = result.data
                .Refresh()
            End With



            'CbProdServ.DropDownList.FilterRow = " company_Type=1"
        ElseIf codigo = 406 Or codigo = 409 Or codigo = 500 Then

            Dim details = JsonConvert.SerializeObject(resultError.errors.details)
            Dim siat = JsonConvert.SerializeObject(resultError.errors.siat)
            Dim notifi = New notifi

            notifi.tipo = 2
            notifi.Context = "SIFAC".ToUpper
            notifi.Header = "Error de solicitud - Código: " + codigo.ToString() & vbCrLf & " " & vbCrLf & details & vbCrLf & siat & vbCrLf & " " & vbCrLf & "Espere".ToUpper
            notifi.ShowDialog()
            'MsgBox("Error de solicitud: " + codigo.ToString() & vbCrLf & " " & vbCrLf & details & vbCrLf & siat & vbCrLf & " " & vbCrLf & "La factura no pudo enviarse al Siat")
        ElseIf codigo = 400 Or codigo = 401 Or codigo = 404 Or codigo = 405 Or codigo = 422 Then
            Dim details = JsonConvert.SerializeObject(resultError.errors.details)
            Dim notifi = New notifi

            notifi.tipo = 2
            notifi.Context = "SIFAC".ToUpper
            notifi.Header = "Error de solicitud - Código: " + codigo.ToString() & vbCrLf & " " & vbCrLf & details & vbCrLf & " " & vbCrLf & "Espere".ToUpper
            notifi.ShowDialog()
            'MsgBox("Error de solicitud: " + codigo.ToString() & vbCrLf & " " & vbCrLf & details & vbCrLf & " " & vbCrLf & "La factura no pudo enviarse al Siat")
        End If
        Return ""
    End Function
    Public Function Homologar(tokenObtenido)

        Dim api = New DBApi()

        Dim Aeco = CbAeconomica.Text 'obtiene el 'Codigo Metodo de pago' 
        Dim delimitador() As String = {"|"}
        Dim vectoraux() As String
        vectoraux = Aeco.Split(delimitador, StringSplitOptions.None)

        CodActEco = CbAeconomica.Value 'vectoraux(0) 'asignando el valor del array a un string

        Ume = CbUmedida.Value 'SelectedIndex + 1 'obtiene el 'Codigo Tipo de documento' 

        Dim CodProdSIN = CbProdServ.Text
        Dim delimitador1() As String = {"|"}
        Dim vectoraux1() As String
        vectoraux1 = CodProdSIN.Split(delimitador, StringSplitOptions.None)

        CodProSINs = CbProdServ.Value ' vectoraux1(1) 'asignando el valor del array a un string

        preciosifac = CDbl(TbPrecioPsifac.Text)

        Dim Henvio = New HomologEnvio.HomoEnv()

        Henvio.codigoActividad = CodActEco
        Henvio.codigoProductoSin = CodProSINs
        Henvio.codigoProducto = tbCodigo.Text
        Henvio.codigoDocumentoSector = 1
        Henvio.descripcion = tbDescPro.Text
        Henvio.precio = preciosifac
        Henvio.unidadMedida = Ume
        Henvio.numeroImei = ""
        Henvio.numeroSerie = ""
        Henvio.unidadMedidaExtraccion = 0
        Henvio.descripcionLeyes = ""
        Henvio.codigoNandina = ""
        Henvio.tipo = ""
        Henvio.formula = ""

        Dim url = "https://crex.sifac.nwc.com.bo/api/v2/homologar"

        Dim headers = New List(Of Parametro) From {
            New Parametro("Authorization", "Bearer " + tokenObtenido),
            New Parametro("Content-Type", "Accept:application/json; charset=utf-8")
        }

        Dim parametros = New List(Of Parametro)

        Dim response = api.Post(url, headers, parametros, Henvio)

        Dim result = JsonConvert.DeserializeObject(Of HomoResp)(response)
        Dim resultError = JsonConvert.DeserializeObject(Of Resp400)(response)

        Dim codigo = result.meta.code
        'Dim xml As String
        If codigo = 200 Then
            Dim pos = result.data
            Dim contpos = JsonConvert.SerializeObject(pos(0))
            Dim contposOBJ = JsonConvert.DeserializeObject(Of DataARA)(contpos)
            Dim details = contposOBJ.details
            'xml = result.data.dataXml
            'Dim doc As XmlDocument = New XmlDocument()
            'doc.LoadXml(xml)
            'Dim nodo = doc.DocumentElement("cabecera")
            'gb_cufSifac = nodo.ChildNodes.Item(5).InnerText 'extrae dato CUF de xml sifac

            Dim notifi = New notifi

            notifi.tipo = 2
            notifi.Context = "SIFAC".ToUpper
            notifi.Header = "Proceso Exitoso - Código: " + codigo.ToString() & vbCrLf & " " & vbCrLf & details & vbCrLf & " " & vbCrLf & "Producto Homologado".ToUpper
            notifi.ShowDialog()
            'MsgBox("Proceso Exitoso: " + codigo.ToString() & vbCrLf & " " & vbCrLf & details & vbCrLf & " " & vbCrLf & "Factura enviada al Siat")
        ElseIf codigo = 406 Or codigo = 409 Or codigo = 500 Then

            Dim details = JsonConvert.SerializeObject(resultError.errors.details)
            Dim siat = JsonConvert.SerializeObject(resultError.errors.siat)
            Dim notifi = New notifi

            notifi.tipo = 2
            notifi.Context = "SIFAC".ToUpper
            notifi.Header = "Error de solicitud - Código: " + codigo.ToString() & vbCrLf & " " & vbCrLf & details & vbCrLf & siat & vbCrLf & " " & vbCrLf & "Producto no Homologado".ToUpper
            notifi.ShowDialog()
            'MsgBox("Error de solicitud: " + codigo.ToString() & vbCrLf & " " & vbCrLf & details & vbCrLf & siat & vbCrLf & " " & vbCrLf & "La factura no pudo enviarse al Siat")
        ElseIf codigo = 400 Or codigo = 401 Or codigo = 404 Or codigo = 405 Or codigo = 422 Then
            Dim details = JsonConvert.SerializeObject(resultError.errors.details)
            Dim notifi = New notifi

            notifi.tipo = 2
            notifi.Context = "SIFAC".ToUpper
            notifi.Header = "Error de solicitud - Código: " + codigo.ToString() & vbCrLf & " " & vbCrLf & details & vbCrLf & " " & vbCrLf & "Producto no Homologado".ToUpper
            notifi.ShowDialog()
            'MsgBox("Error de solicitud: " + codigo.ToString() & vbCrLf & " " & vbCrLf & details & vbCrLf & " " & vbCrLf & "La factura no pudo enviarse al Siat")
        End If

        Return codigo
    End Function

    Private Sub TbPrecioPsifac_KeyPress(sender As Object, e As KeyPressEventArgs)
        g_prValidarTextBox(1, e)
    End Sub
End Class