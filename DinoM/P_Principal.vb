Imports Logica.AccesoLogica
Imports Modelo.MGlobal
Imports DevComponents.DotNetBar.Controls
Imports DevComponents.DotNetBar.Metro
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Rendering

Public Class P_Principal

#Region "Atributos"

#End Region

#Region "Metodos Privados"

    Public Sub New()
        _prCambiarStyle()
        ' This call is required by the designer.
        InitializeComponent()
        FP_Configuracion.Select ()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub _prIniciarTodo()
        'Leer Archivo de Configuración
        _prLeerArchivoConfig()

        L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        L_prAbrirConexionBitacora(gs_Ip, gs_UsuarioSql, gs_ClaveSql, "BDDiconDinoCrex")

        Me.WindowState = FormWindowState.Maximized

        'iniciar login de usuario
        _prLogin()

        If My.Computer.FileSystem.FileExists(gs_CarpetaRaiz + "\LogoIco.ico") Then
            Me.Icon = Icon.ExtractAssociatedIcon(gs_CarpetaRaiz + "\LogoIco.ico")
        End If



    End Sub
    Private Sub _prCambiarStyle()
        'tratar de cambiar estilo
        'RibbonPredefinedColorSchemes.ChangeOffice2007ColorTable(Me, DevComponents.DotNetBar.Rendering.eOffice2007ColorScheme.Silver, Color.FromArgb(0, 85, 139))

        RibbonPredefinedColorSchemes.ChangeOffice2007ColorTable(Me, DevComponents.DotNetBar.Rendering.eOffice2007ColorScheme.VistaGlass)
        'RibbonPredefinedColorSchemes.ChangeOffice2007ColorTable(Me, eStyle.VisualStudio2012Dark)
        'RibbonPredefinedColorSchemes.ChangeStyle(eStyle.VisualStudio2012Dark)

        'cambio de otros colores
        Dim table As Office2007ColorTable = CType(GlobalManager.Renderer, Office2007Renderer).ColorTable
        Dim ct As SideNavColorTable = table.SideNav
        ''  ct.TitleBackColor = Color.FromArgb(49, 59, 66)
        'ct.SideNavItem.MouseOver.BackColors = New Color() {Color.Red, Color.Yellow}
        ct.SideNavItem.MouseOver.BorderColors = New Color() {Color.FromArgb(49, 59, 66)} ' No border
        ct.SideNavItem.Selected.BackColors = New Color() {Color.Yellow}
        ''     ct.BorderColors = New Color() {Color.FromArgb(49, 59, 66)} ' Control border color

        ''  ct.PanelBackColor = Color.FromArgb(49, 59, 66)
    End Sub

    Private Sub _prLeerArchivoConfig()
        Dim Archivo() As String = IO.File.ReadAllLines(Application.StartupPath + "\CONFIG.TXT")
        gs_Ip = Archivo(0).Split("=")(1).Trim
        gs_UsuarioSql = Archivo(1).Split("=")(1).Trim
        gs_ClaveSql = Archivo(2).Split("=")(1).Trim
        gs_NombreBD = Archivo(3).Split("=")(1).Trim
        gs_CarpetaRaiz = Archivo(4).Split("=")(1).Trim
    End Sub

    Private Sub _prLogin()
        Dim ef = New Efecto
        ef.tipo = 4
        ef.ShowDialog()

        L_Usuario = gs_user
        Modelo.MGlobal.gs_usuario = gs_user

        lbUsuario.Text = gs_user
        lbUsuario.Font = New Font("Tahoma", 8, FontStyle.Bold)

        If gs_user = "DEFAULT" Then
            SideNav1.Enabled = False
        Else
            _PCargarPrivilegios()
            _prCargarConfiguracionSistema()
            SideNav1.Enabled = True
        End If

        P_prCargarParametros()
        _prValidarMayusculas()
    End Sub
    Public Sub _prValidarMayusculas()
        Dim dt As DataTable = L_fnPorcUtilidad()
        If (dt.Rows.Count > 0) Then
            Modelo.MGlobal.gs_mayusuculas = dt.Rows(0).Item("mayusculas")
        End If
    End Sub

    Private Sub P_prCargarParametros()
        Dim dtConfSistema As DataTable = L_fnConfSistemaGeneral()

        gb_FacturaEmite = dtConfSistema.Rows(0).Item("cccefac")
        gi_FacturaTipo = dtConfSistema.Rows(0).Item("ccctfac")
        gi_FacturaCantidadItems = dtConfSistema.Rows(0).Item("ccccite")
        gb_FacturaIncluirICE = dtConfSistema.Rows(0).Item("ccciice")
        'gi_codeBar = dtConfSistema.Rows(0).Item("ccciice")
        gb_CodigoBarra = dtConfSistema.Rows(0).Item("ccccbar")
        gb_DetalleProducto = dtConfSistema.Rows(0).Item("cccdetp")

        gb_UbiLogo = dtConfSistema.Rows(0).Item("cccubilogo")
        gb_NotaAdicional = dtConfSistema.Rows(0).Item("cccnotaadic")
        gb_TipoAyuda = dtConfSistema.Rows(0).Item("cccTipoAyuda")
        gb_MostrarFamilia = dtConfSistema.Rows(0).Item("cccMostrarFamilia")
    End Sub

    Private Sub _prCargarConfiguracionSistema()
        'Dim dtConf As DataTable = L_prConGlobalGeneral()
        'gd_notaAproTeo = dtConf.Rows(0).Item("gbaproteo")

    End Sub

    Private Sub _PCargarPrivilegios()
        Dim listaTabs As New List(Of DevComponents.DotNetBar.Metro.MetroTilePanel)
        listaTabs.Add(MetroTilePanel1)
        listaTabs.Add(MetroTilePanel2)
        listaTabs.Add(MetroTilePanel6)
        listaTabs.Add(MetroTilePanel7)
        listaTabs.Add(MetroTilePanelVentas)
        listaTabs.Add(MenuCreditos)


        Dim idRolUsu As String = gi_userRol

        Dim dtModulos As DataTable = L_prLibreriaDetalleGeneral(gi_LibSistema, gi_LibSISModulo)
        Dim listFormsModulo As New List(Of String)

        For i = 0 To dtModulos.Rows.Count - 1
            Dim dtDetRol As DataTable = L_RolDetalle_General(-1, idRolUsu, dtModulos.Rows(i).Item("cnnum"))
            listFormsModulo = New List(Of String)

            If dtDetRol.Rows.Count > 0 Then
                'cargo los nombres de los programas(botones) del modulo
                For Each fila As DataRow In dtDetRol.Rows
                    listFormsModulo.Add(fila.Item("yaprog").ToString.ToUpper)
                Next
                'recorro el modulo(tab) que corresponde
                For Each _item As DevComponents.DotNetBar.BaseItem In listaTabs.Item(i).Items
                    If TypeOf (_item) Is DevComponents.DotNetBar.Metro.MetroTileItem Then 'es un boton del modulo
                        Dim btn As DevComponents.DotNetBar.Metro.MetroTileItem = CType(_item, DevComponents.DotNetBar.Metro.MetroTileItem)
                        If listFormsModulo.Contains(btn.Name.ToUpper) Then 'si el nombre del boton pertenece a la lista de formularios del modulo
                            Dim Texto As String = btn.Text
                            Dim TTexto As String = btn.TitleText
                            Dim f As Integer = listFormsModulo.IndexOf(btn.Name.ToUpper)
                            If Texto = "" Then 'esta usando el Title Text
                                btn.TitleText = dtDetRol.Rows(f).Item("yatit").ToString.ToUpper
                            Else 'esta usando el Text
                                btn.Text = dtDetRol.Rows(f).Item("yatit").ToString.ToUpper
                            End If

                            If dtDetRol.Rows(f).Item("ycshow") = True Or dtDetRol.Rows(f).Item("ycadd") = True Or dtDetRol.Rows(f).Item("ycmod") = True Or dtDetRol.Rows(f).Item("ycdel") = True Then
                                btn.Visible = True
                            Else
                                btn.Visible = False
                            End If
                        Else 'si no pertenece lo oculto
                            btn.Visible = False
                        End If
                    Else 'seria un sub grupo en el modulo
                        'recorro todos los elementos del sub grupo
                        If TypeOf _item Is ItemContainer Then
                            For Each _subItem In _item.SubItems
                                Dim _subBtn As MetroTileItem = CType(_subItem, MetroTileItem)
                                If listFormsModulo.Contains(_subBtn.Name.ToUpper) Then
                                    Dim Texto As String = _subBtn.Text
                                    Dim TTexto As String = _subBtn.TitleText
                                    Dim f As Integer = listFormsModulo.IndexOf(_subBtn.Name.ToUpper)
                                    If Texto = "" Then 'esta usando el Title Text
                                        _subBtn.TitleText = dtDetRol.Rows(f).Item("yatit").ToString.ToUpper
                                    Else 'esta usando el Text
                                        _subBtn.Text = dtDetRol.Rows(f).Item("yatit").ToString.ToUpper
                                    End If

                                    If dtDetRol.Rows(f).Item("ycshow") = True Or dtDetRol.Rows(f).Item("ycadd") = True Or dtDetRol.Rows(f).Item("ycmod") = True Or dtDetRol.Rows(f).Item("ycdel") = True Then
                                        _subBtn.Visible = True
                                    Else
                                        _subBtn.Visible = False
                                    End If
                                Else
                                    _subBtn.Visible = False
                                End If
                            Next
                        End If

                    End If
                Next
            Else ' no exiten formulario registrados en el modulo pero igual hay que ocultar los botones y los subbotones que tenga
                For Each _item As DevComponents.DotNetBar.BaseItem In listaTabs.Item(i).Items
                    If TypeOf (_item) Is DevComponents.DotNetBar.Metro.MetroTileItem Then 'es un boton del modulo
                        Dim btn As DevComponents.DotNetBar.Metro.MetroTileItem = CType(_item, DevComponents.DotNetBar.Metro.MetroTileItem)
                        btn.Visible = False
                    Else 'seria un sub grupo en el modulo
                        'recorro todos los elementos del sub grupo
                        If TypeOf _item Is ItemContainer Then
                            For Each _subItem In _item.SubItems
                                Dim _subBtn As MetroTileItem = CType(_subItem, MetroTileItem)
                                _subBtn.Visible = False
                            Next
                        End If

                    End If
                Next

            End If

        Next

        'refrescar el formulario
        Me.Refresh()
    End Sub
#End Region

    Private Sub P_Principal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()
    End Sub
    Private Sub P_Principal_MouseClick(sender As Object, e As MouseEventArgs) Handles MyBase.MouseClick
        _prLogin()
    End Sub

    Private Sub P_Principal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles MyBase.KeyPress
        _prLogin()
    End Sub

    Private Sub rmSesion_ItemClick(sender As Object, e As EventArgs) Handles rmSesion.ItemClick
        Dim item As RadialMenuItem = TryCast(sender, RadialMenuItem)
        If item IsNot Nothing AndAlso (Not String.IsNullOrEmpty(item.Text)) Then
            Select Case item.Name
                Case "btCerrarSesion"
                    _prLogin()
                Case "btSalir"
                    Me.Close()
                Case "btAbout"
                    'Dim frm As New P_Acerca
                    'frm.ShowDialog()
            End Select
        End If
    End Sub



    Private Sub btConfRoles_Click(sender As Object, e As EventArgs) Handles btConfRoles.Click
        'SideNav1.IsMenuExpanded = False
        'Ventana.Select()
        ''Dim frm As New F0_Roles
        Dim frm As New F1_Rol
        frm._nameButton = btConfRoles.Name
        frm._modulo = FP_Configuracion
        'Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        'tab3.RecalcSize()
        'tab3.ThemeAware = True
        'tab3.ShowSubItems = True
        'tab3.UpdateBindings()
        'frm._tab = tab3
        'Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        'superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        'tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        'tab3.Text = frm.Text
        'tab3.Icon = frm.Icon
    End Sub

    Private Sub btConfUsuarios_Click(sender As Object, e As EventArgs) Handles btConfUsuarios.Click
        'SideNav1.IsMenuExpanded = False
        'Ventana.Select()
        Dim frm As New F0_Usuarios
        frm._nameButton = btConfUsuarios.Name
        frm._modulo = FP_Configuracion
        'Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        'frm._tab = tab3
        'Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        'superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        'tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        'tab3.Text = frm.Text
        'tab3.Icon = frm.Icon

    End Sub



    Private Sub Ventana_Click(sender As Object, e As EventArgs) Handles Ventana.Click
        SideNav1.IsMenuExpanded = False
    End Sub

    Private Sub SideNav1_IsMenuExpandedChanged(sender As Object, e As EventArgs) Handles SideNav1.IsMenuExpandedChanged
        If (SideNav1.IsMenuExpanded = True) Then
            FP_Configuracion.Select()

        End If
    End Sub

    Private Sub SideNavItem3_Click(sender As Object, e As EventArgs) Handles SideNavItem3.Click
        rmSesion.IsOpen = True
        rmSesion.MenuLocation = New Point(Me.Width / 2, Me.Height / 3)
        SideNav_Conf.Select()

    End Sub

    Private Sub rmSesion_MenuClosed(sender As Object, e As EventArgs) Handles rmSesion.MenuClosed
        FP_Configuracion.Select()

    End Sub

    Private Sub btConfFabrica_Click(sender As Object, e As EventArgs) Handles btConfCliente.Click
        'SideNav1.IsMenuExpanded = False
        'Ventana.Select()
        F1_Clientes.AllowTransparency = True
        Dim frm As New F1_Clientes
        frm._Tipo = 1
        frm._nameButton = btConfCliente.Name
        frm._modulo = FP_Configuracion
        'Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        'frm._tab = tab3
        'Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        'superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        'tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        'tab3.Text = frm.Text
        'tab3.Icon = frm.Icon

    End Sub

    Private Sub btConfTipoEquipo_Click(sender As Object, e As EventArgs) Handles btConfProducto.Click
        'SideNav1.IsMenuExpanded = False
        'Ventana.Select()
        Dim frm As New F1_Productos
        frm._nameButton = btConfProducto.Name
        frm._modulo = FP_Configuracion
        'Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        'frm._tab = tab3
        'Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        'superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        'tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        'tab3.Text = frm.Text
        'tab3.Icon = frm.Icon
    End Sub

    Private Sub btConfPrecio_Click(sender As Object, e As EventArgs) Handles btConfPrecio.Click
        'SideNav1.IsMenuExpanded = False
        'Ventana.Select()
        Dim frm As New F0_Precios
        frm._nameButton = btConfPrecio.Name
        frm._modulo = FP_Configuracion
        'Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        'frm._tab = tab3
        'Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        'superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        'tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        'tab3.Text = frm.Text
        'tab3.Icon = frm.Icon
    End Sub

    Private Sub btCatCatalogo_Click(sender As Object, e As EventArgs) Handles btZonas.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New F1_Zonas
        frm._nameButton = btZonas.Name
        frm._modulo = FP_ZONAS
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
        tab3.Icon = frm.Icon
    End Sub



    Private Sub MetroTileItem12_Click(sender As Object, e As EventArgs) Handles btInvDeposito.Click
        'SideNav1.IsMenuExpanded = False
        'Ventana.Select()
        Dim frm As New F1_Deposito
        frm._nameButton = btInvDeposito.Name
        frm._modulo = FP_INVENTARIO
        'Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        'frm._tab = tab3
        'Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        'superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        'tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        'tab3.Text = frm.Text
        'tab3.Icon = frm.Icon
    End Sub

    Private Sub btInvAmacen_Click(sender As Object, e As EventArgs) Handles btInvAmacen.Click
        'SideNav1.IsMenuExpanded = False
        'Ventana.Select()
        Dim frm As New F1_Almacen
        frm._nameButton = btInvAmacen.Name
        frm._modulo = FP_INVENTARIO
        'Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        'frm._tab = tab3
        'Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        'superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        'tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        'tab3.Text = frm.Text
        'tab3.Icon = frm.Icon
    End Sub

    Private Sub btComProveedor_Click(sender As Object, e As EventArgs) Handles btComProveedor.Click
        'SideNav1.IsMenuExpanded = False
        'Ventana.Select()
        'Dim frm As New F1_Proveedor
        'frm._Tipo = 3
        'frm._nameButton = btComProveedor.Name
        'frm._modulo = FP_COMPRAS
        'Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        'frm._tab = tab3
        'Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        'superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        'tab3.AttachedControl.Controls.Add(panel)
        'frm.Show()
        'tab3.Text = "PROVEEDOR"
        'Dim blah As New Bitmap(New Bitmap(My.Resources.user), 20, 20)
        'Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        'Me.Icon = ico
        'tab3.Icon = ico

        Dim frm As New F1_Proveedor
        frm._Tipo = 3
        frm._nameButton = btComProveedor.Name
        frm._modulo = FP_COMPRAS
        frm.Show()
    End Sub

    Private Sub btComCompra_Click(sender As Object, e As EventArgs) Handles btComCompra.Click
        'SideNav1.IsMenuExpanded = False
        'Ventana.Select()
        Dim frm As New F0_MCompras
        frm._nameButton = btConfCliente.Name
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        frm._modulo = FP_COMPRAS
        'Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        'superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        'tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        'tab3.Text = frm.Text
        'Dim blah As New Bitmap(New Bitmap(My.Resources.compra), 20, 20)
        'Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        'tab3.Icon = ico

    End Sub

    Private Sub btInvVentas_Click(sender As Object, e As EventArgs) Handles btInvVentas.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New F0_Venta2
        frm._nameButton = btConfCliente.Name
        frm._modulo = FP_INVENTARIO
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = "VENTAS"
        tab3.Icon = frm.Icon
    End Sub


    Private Sub btConfDosificacion_Click(sender As Object, e As EventArgs) Handles btConfDosificacion.Click
        'SideNav1.IsMenuExpanded = False
        'Ventana.Select()
        Dim frm As New F1_Dosificacion
        frm._nameButton = btZonas.Name
        frm._modulo = FP_Configuracion
        'Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        'frm._tab = tab3
        'Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        'superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        'tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        'tab3.Text = frm.Text
        'Dim blah As New Bitmap(New Bitmap(My.Resources.Mensaje), 20, 20)
        'Dim ico As Icon = Icon.FromHandle(blah.GetHicon())

        'tab3.Icon = ico
    End Sub

#Region "Modulo Venta"

    Private Sub btVentVenta_Click(sender As Object, e As EventArgs) Handles btVentVenta.Click
        'SideNav1.IsMenuExpanded = False
        'Ventana.Select()
        Dim frm As New F0_Venta2
        frm._nameButton = btVentVenta.Name
        'Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        'frm._tab = tab3
        frm._modulo = FP_VENTAS
        'Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        'superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        'tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        'tab3.Text = frm.Text
        'tab3.Icon = frm.Icon
    End Sub

    Private Sub btVentAnularFactura_Click(sender As Object, e As EventArgs) Handles btVentAnularFactura.Click
        'SideNav1.IsMenuExpanded = False
        'Ventana.Select()
        Dim frm As New F0_AnularFactura
        'frm._nameButton = btVentAnularFactura.Name
        'Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)

        'Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        'superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        'tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        'tab3.Text = frm.Text
        'Dim blah As New Bitmap(New Bitmap(My.Resources.ic_a), 20, 20)
        'Dim ico As Icon = Icon.FromHandle(blah.GetHicon())

        'tab3.Icon = ico
    End Sub

    Private Sub btVentLibroVenta_Click(sender As Object, e As EventArgs) Handles btVentLibroVenta.Click
        'SideNav1.IsMenuExpanded = False
        'Ventana.Select()
        Dim frm As New F0_LibroVenta2
        'frm._nameButton = btVentLibroVenta.Name
        'Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        'frm._tab = tab3
        'frm._modulo = FP_VENTAS
        'Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        'superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        'tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        'tab3.Text = frm.Text
        'Dim blah As New Bitmap(New Bitmap(My.Resources.libros), 20, 20)
        'Dim ico As Icon = Icon.FromHandle(blah.GetHicon())

        'tab3.Icon = ico
    End Sub

#End Region

    Private Sub btInvMovimiento_Click(sender As Object, e As EventArgs) Handles btInvMovimiento.Click
        'SideNav1.IsMenuExpanded = False
        'Ventana.Select()
        Dim frm As New F0_Movimiento
        frm._nameButton = btInvMovimiento.Name
        frm._modulo = FP_INVENTARIO
        'Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        'frm._tab = tab3
        'Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        'superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        'tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        'tab3.Text = frm.Text
        'Dim blah As New Bitmap(New Bitmap(My.Resources.Mensaje), 20, 20)
        'Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        'tab3.Icon = ico
    End Sub

    Private Sub superTabControl3_TabItemClose(sender As Object, e As SuperTabStripTabItemCloseEventArgs) Handles superTabControl3.TabItemClose
        Dim cantidad As Integer = superTabControl3.Tabs.Count
        If cantidad = 1 Then
            FP_Configuracion.Select()
        End If
    End Sub

    Private Sub MetroTileItem16_Click(sender As Object, e As EventArgs) Handles btInvKardex.Click
        'SideNav1.IsMenuExpanded = False
        'Ventana.Select()
        Dim frm As New F0_KardexMovimiento
        frm._nameButton = btInvKardex.Name
        frm._modulo = FP_INVENTARIO
        'Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        'frm._tab = tab3
        'Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        'superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        'tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        'tab3.Text = frm.Text
        'Dim blah As New Bitmap(New Bitmap(My.Resources.movimiento), 20, 20)
        'Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        'tab3.Icon = ico
    End Sub

    Private Sub btInvSaldo_Click(sender As Object, e As EventArgs) Handles btInvSaldo.Click

        'SideNav1.IsMenuExpanded = False
        'Ventana.Select()
        Dim frm As New Pr_SAldosPorAlmacenLinea
        'Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        'frm._tab = tab3
        'Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        'superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        'tab3.AttachedControl.Controls.Add(panel)
        frm.Show()

        'tab3.Text = "SALDO PRODUCTO"
        'Dim blah As New Bitmap(New Bitmap(My.Resources.check_mark), 20, 20)
        'Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        'tab3.Icon = ico


        'Dim form As New PR_StockActualProductos
        'form.pTitulo = "S A L D O   A C T U A L   D E   P R O D U C T O S"
        'form.pTipo = 2
        'form.Show()
    End Sub

    Private Sub btVentReporteAtendidas_Click(sender As Object, e As EventArgs) Handles btVentReporteAtendidas.Click
        'SideNav1.IsMenuExpanded = False
        'Ventana.Select()
        Dim frm As New Pr_VentasAtendidas
        'Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        'frm._tab = tab3

        'Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        'superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        'tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        'tab3.Text = "VENTAS ATENDIDAS"
        'Dim blah As New Bitmap(New Bitmap(My.Resources.check_mark), 20, 20)
        'Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        'tab3.Icon = ico
    End Sub

    Private Sub btVentReporteVentaVsCosto_Click(sender As Object, e As EventArgs) Handles btVentReporteVentaVsCosto.Click
        'SideNav1.IsMenuExpanded = False
        'Ventana.Select()
        Dim frm As New Pr_VentasVsCostos
        'Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        'frm._tab = tab3
        'Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        'superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        'tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        'tab3.Text = "VENTAS Y COSTOS"
        'Dim blah As New Bitmap(New Bitmap(My.Resources.check_mark), 20, 20)
        'Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        'tab3.Icon = ico
    End Sub

    Private Sub btZonaMapaCliente_Click(sender As Object, e As EventArgs) Handles btZonaMapaCliente.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New F1_MapaCLientes
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)

        frm.Show()
        tab3.Text = "MAPAS CLIENTES"
        Dim blah As New Bitmap(New Bitmap(My.Resources.MapaClientes), 20, 20)
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        tab3.Icon = ico
    End Sub

    

    Private Sub btHojaRuta_Click(sender As Object, e As EventArgs) Handles btZonaReporteRuta.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New Pr_HojaRuta
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._modulo = FP_ZONAS
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = "REPORTE HOJA DE RUTA"
        Dim blah As New Bitmap(New Bitmap(My.Resources.hojaruta), 20, 20)
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        tab3.Icon = ico
    End Sub

    Private Sub btInvKardexReporte_Click(sender As Object, e As EventArgs) Handles btInvKardexReporte.Click
        'SideNav1.IsMenuExpanded = False
        'Ventana.Select()
        Dim frm As New Pr_KardexProductos
        'Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        'frm._tab = tab3
        'Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        'superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        'tab3.AttachedControl.Controls.Add(panel)

        frm.Show()
        'tab3.Text = "REPORTE KARDEX PRODUCTO"
        'Dim blah As New Bitmap(New Bitmap(My.Resources._14), 20, 20)
        'Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        'tab3.Icon = ico
    End Sub

    Private Sub btVentReporteProducto_Click(sender As Object, e As EventArgs) Handles btVentReporteProducto.Click
        'SideNav1.IsMenuExpanded = False
        'Ventana.Select()
        Dim frm As New Pr_ProductosVentas
        'Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        'frm._tab = tab3
        'Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        'superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        'tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        'tab3.Text = "PRODUCTOS MAS VENDIDOS"
        'Dim blah As New Bitmap(New Bitmap(My.Resources.check_mark), 20, 20)
        'Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        'tab3.Icon = ico
    End Sub

    Private Sub MetroTileItem11_Click(sender As Object, e As EventArgs) Handles btnCredPago.Click
        'SideNav1.IsMenuExpanded = False
        'Ventana.Select()
        Dim frm As New F0_PagosCredito
        frm._nameButton = btInvMovimiento.Name
        frm._modulo = FP_CREDITOS
        'Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        'frm._tab = tab3
        'Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        'superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        'tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        'tab3.Text = frm.Text
        'tab3.Icon = frm.Icon
    End Sub

   
    Private Sub btnCredEstCuenta_Click(sender As Object, e As EventArgs) Handles btnCredEstCuenta.Click
        'SideNav1.IsMenuExpanded = False
        'Ventana.Select()
        Dim frm As New Pr_KardexCredito
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        frm._modulo = FP_CREDITOS
        'Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        'superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        'tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        'tab3.Text = "REPORTE ESTADO DE CUENTAS"
        'Dim blah As New Bitmap(New Bitmap(My.Resources.rp_estadoCuentas), 20, 20)
        'Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        'tab3.Icon = ico
    End Sub

    Private Sub btInvUtilidad_Click(sender As Object, e As EventArgs) Handles btInvUtilidad.Click
        'SideNav1.IsMenuExpanded = False
        'Ventana.Select()
        Dim frm As New Pr_StockUtilidad
        'Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        'frm._tab = tab3
        'Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        'superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        'tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        'tab3.Text = "UTILIDAD DE PRODUCTOS"
        'Dim blah As New Bitmap(New Bitmap(My.Resources.rp_estadoCuentas), 20, 20)
        'Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        'tab3.Icon = ico
    End Sub

    Private Sub MetroTileItem13_Click(sender As Object, e As EventArgs) Handles btnCredInfMorosidad.Click
        'SideNav1.IsMenuExpanded = False
        'Ventana.Select()
        Dim frm As New Pr_ReporteMorosidadGeneral
        'Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        'frm._tab = tab3

        'Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        'superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        'tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        'tab3.Text = "INFORME DE MOROSIDAD"
        'Dim blah As New Bitmap(New Bitmap(My.Resources.hojaruta), 20, 20)
        'Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        'tab3.Icon = ico
    End Sub

    Private Sub btComPagosCredito_Click(sender As Object, e As EventArgs) Handles btComPagosCredito.Click
        'SideNav1.IsMenuExpanded = False
        'Ventana.Select()
        Dim frm As New F0_PagosCreditoCompraUlt
        frm._nameButton = btInvMovimiento.Name
        frm._modulo = FP_COMPRAS
        'Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        'frm._tab = tab3
        'Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        'superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        'tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        'tab3.Text = frm.Text
        'tab3.Icon = frm.Icon
    End Sub

    Private Sub MetroTileItem11_Click_1(sender As Object, e As EventArgs) Handles btInvSaldoLote.Click
        'SideNav1.IsMenuExpanded = False
        'Ventana.Select()
        Dim frm As New PR_StockActualProductosLotes
        'Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        'Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        'superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        'tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        'tab3.Text = "SALDO PRODUCTO POR LOTES"
        'Dim blah As New Bitmap(New Bitmap(My.Resources.check_mark), 20, 20)
        'Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        'tab3.Icon = ico
    End Sub

    Private Sub btVentProforma_Click(sender As Object, e As EventArgs) Handles btVentProforma.Click
        'SideNav1.IsMenuExpanded = False
        'Ventana.Select()
        Dim frm As New F0_Proforma
        frm._nameButton = btVentProforma.Name
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        frm._modulo = FP_VENTAS
        'Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        'superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        'tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        'tab3.Text = frm.Text
        'tab3.Icon = frm.Icon
    End Sub

    Private Sub btVentGrafica_Click(sender As Object, e As EventArgs) Handles btVentGrafica.Click
        'SideNav1.IsMenuExpanded = False
        'Ventana.Select()
        Dim frm As New Pr_ReporteVentasGrafico
        'frm._nameButton = btVentProforma.Name
        'Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        'frm._tab = tab3
        'Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        'superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        'tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        'tab3.Text = frm.Text
        'Dim blah As New Bitmap(New Bitmap(My.Resources.grafica), 20, 20)
        'Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        'tab3.Icon = ico
    End Sub

    Private Sub MetroTileItem11_Click_2(sender As Object, e As EventArgs) Handles btVentRotProd.Click
        'SideNav1.IsMenuExpanded = False
        'Ventana.Select()
        Dim frm As New F0_RotacionProductos
        'frm._nameButton = btVentRotProd.Name
        'Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        'frm._tab = tab3
        'Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        'superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        'tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        'tab3.Text = frm.Text
        'Dim blah As New Bitmap(New Bitmap(My.Resources.rotacionproducto), 20, 20)
        'Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        'tab3.Icon = ico
    End Sub

    Private Sub btComVendedor_Click_1(sender As Object, e As EventArgs) Handles btComVendedor.Click
        'SideNav1.IsMenuExpanded = False
        'Ventana.Select()
        Dim frm As New F1_Vendedor
        frm._Tipo = 2
        frm._nameButton = btComVendedor.Name
        frm._modulo = FP_VENTAS
        'Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        'frm._tab = tab3
        'Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        'superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        'tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        'tab3.Text = "VENDEDOR"
        'Dim blah As New Bitmap(New Bitmap(My.Resources.vendedor), 20, 20)
        'Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        'tab3.Icon = ico
    End Sub

    Private Sub btVentEstad_Click(sender As Object, e As EventArgs) Handles btVentEstad.Click
        'SideNav1.IsMenuExpanded = False
        'Ventana.Select()
        Dim frm As New Pr_Ventas12Meses
        'frm._nameButton = btVentEstad.Name
        'Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        'frm._tab = tab3
        'Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        'superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        'tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        'tab3.Text = "ESTADISTICO VENTAS"
        'Dim blah As New Bitmap(New Bitmap(My.Resources.estadistico), 20, 20)
        'Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        'tab3.Icon = ico
    End Sub

    Private Sub btVentFactura_Click(sender As Object, e As EventArgs) Handles btVentFactura.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New F0_Factura
        frm._nameButton = btVentFactura.Name
        frm._modulo = FP_VENTAS
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
        Dim blah As New Bitmap(New Bitmap(My.Resources.rotacionproducto), 20, 20)
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        tab3.Icon = ico
    End Sub

    Private Sub btConfAccesorio_Click(sender As Object, e As EventArgs) Handles btConfLibreria.Click
        'SideNav1.IsMenuExpanded = False
        'Ventana.Select()
        Dim frm As New F0_Libreria
        frm._nameButton = btConfLibreria.Name
        frm._modulo = FP_Configuracion
        'Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        'frm._tab = tab3
        'Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        'superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        'tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        'tab3.Text = frm.Text
        'Dim blah As New Bitmap(New Bitmap(My.Resources.check_mark), 20, 20)
        'Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        'tab3.Icon = ico
    End Sub

    Private Sub FP_INVENTARIO_Click(sender As Object, e As EventArgs) Handles FP_INVENTARIO.Click

    End Sub

    Private Sub btlvSaldoMinimo_Click(sender As Object, e As EventArgs) Handles btlvSaldoMinimo.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New Pr_StockMinimo
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()

        tab3.Text = "SALDO PRODUCTO MINIMO"
        Dim blah As New Bitmap(New Bitmap(My.Resources.check_mark), 20, 20)
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        tab3.Icon = ico
    End Sub

    Private Sub btnCredPagoCliente_Click(sender As Object, e As EventArgs) Handles btnCredPagoCliente.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New F0_Cobrar_Cliente
        frm._nameButton = btInvMovimiento.Name
        frm._modulo = FP_CREDITOS
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
        tab3.Icon = frm.Icon
    End Sub

    Private Sub btnCredPagoClienteVendedor_Click(sender As Object, e As EventArgs) Handles btnCredPagoClienteVendedor.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New F0_Cobrar_Vendedor
        frm._nameButton = btInvMovimiento.Name
        frm._modulo = FP_CREDITOS
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
        tab3.Icon = frm.Icon
    End Sub

    Private Sub FP_VENTAS_Click(sender As Object, e As EventArgs) Handles FP_VENTAS.Click

    End Sub

    Private Sub btnCodControl_Click(sender As Object, e As EventArgs) Handles btnCodControl.Click
        'SideNav1.IsMenuExpanded = False
        'Ventana.Select()
        'Dim frm As New F0_CodigoControl
        'frm._nameButton = btnCodControl.Name
        'frm._modulo = FP_Configuracion
        'Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        'frm._tab = tab3
        'Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        'superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        'tab3.AttachedControl.Controls.Add(panel)
        'frm.Show()
        'tab3.Text = frm.Text
        'tab3.Icon = frm.Icon

        Dim frm As New F0_CodigoControl
        frm._nameButton = btnCodControl.Name
        frm.Show()
    End Sub

    Private Sub FP_CREDITOS_Click(sender As Object, e As EventArgs) Handles FP_CREDITOS.Click

    End Sub

    Private Sub btComEstadoCuentas_Click(sender As Object, e As EventArgs) Handles btComEstadoCuentas.Click
        'SideNav1.IsMenuExpanded = False
        'Ventana.Select()
        Dim frm As New R_KardexCreditoPagos
        'Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        'frm._tab = tab3
        frm._modulo = FP_COMPRAS
        'Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        'superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        'tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        'tab3.Text = "REPORTE ESTADO DE CUENTAS"
        'Dim blah As New Bitmap(New Bitmap(My.Resources.rp_estadoCuentas), 20, 20)
        'Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        'tab3.Icon = ico
    End Sub

    Private Sub btCierreCaja_Click(sender As Object, e As EventArgs) Handles btCierreCaja.Click
        Dim frm As New F0_CierreCaja
        frm._nameButton = btCierreCaja.Name
        frm.Show()
    End Sub

    Private Sub btZonas1_Click(sender As Object, e As EventArgs) Handles btZonas1.Click

        Dim frm As New F1_Zonas
        frm._nameButton = btZonas1.Name
        frm.Show()
    End Sub

    Private Sub btEstadoCuentasProveedores_Click(sender As Object, e As EventArgs) Handles btEstadoCuentasProveedores.Click
        Dim frm As New Pr_EstadoCuentasProveedores
        frm._nameButton = btEstadoCuentasProveedores.Name
        'frm._modulo = FP_COMPRAS
        frm.Show()
    End Sub

    Private Sub btEstadoCuentasClientes_Click(sender As Object, e As EventArgs) Handles btEstadoCuentasClientes.Click
        Dim frm As New Pr_EstadoCuentasClientes
        frm._nameButton = btEstadoCuentasClientes.Name
        frm.Show()
    End Sub

    Private Sub btEstadoCuentasTodos_Click(sender As Object, e As EventArgs) Handles btEstadoCuentasClientesTodos.Click
        Dim frm As New Pr_EstadoCuentasClientesTodos
        frm._nameButton = btEstadoCuentasClientesTodos.Name
        frm.Show()
    End Sub

    Private Sub btEstadoCuentasProveedoresTodos_Click(sender As Object, e As EventArgs) Handles btEstadoCuentasProveedoresTodos.Click
        Dim frm As New Pr_EstadoCuentasProveedoresTodos
        frm._nameButton = btEstadoCuentasProveedoresTodos.Name
        'frm._modulo = FP_COMPRAS
        frm.Show()
    End Sub

    Private Sub btBancos_Click(sender As Object, e As EventArgs) Handles btBancos.Click
        Dim frm As New F1_Bancos
        frm._nameButton = btBancos.Name
        frm.Show()
    End Sub

    Private Sub btIngresoEgreso_Click(sender As Object, e As EventArgs) Handles btIngresoEgreso.Click
        Dim frm As New F1_IngresosEgresos
        frm._nameButton = btIngresoEgreso.Name
        frm.Show()
    End Sub

    Private Sub btnVentaRapida_Click(sender As Object, e As EventArgs) Handles btnVentaRapida.Click
        Dim frm As New F0_VentasSupermercado

        frm.Show()
    End Sub

    Private Sub btServicios_Click(sender As Object, e As EventArgs) Handles btServicios.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New F1_Servicios
        frm._nameButton = btServicios.Name
        frm._modulo = FP_Configuracion
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
        tab3.Icon = frm.Icon
    End Sub

    Private Sub btVentaServicios_Click(sender As Object, e As EventArgs) Handles btVentaServicios.Click
        SideNav1.IsMenuExpanded = False
        Ventana.Select()
        Dim frm As New F0_Ventas
        frm._nameButton = btVentaServicios.Name
        frm._modulo = FP_VENTAS
        Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
        frm._tab = tab3
        Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
        superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
        tab3.AttachedControl.Controls.Add(panel)
        frm.Show()
        tab3.Text = frm.Text
        tab3.Icon = frm.Icon
    End Sub

    Private Sub btConfDescuentos_Click(sender As Object, e As EventArgs) Handles btConfDescuentos.Click
        Dim frm As New Descuentos
        frm.Show()
    End Sub

    Private Sub btInvStockDif_Click(sender As Object, e As EventArgs) Handles btInvStockDif.Click
        Dim frm As New Pr_ReporteStock
        frm.Show()
    End Sub

    Private Sub btVentasCajeras_Click(sender As Object, e As EventArgs) Handles btVentasCajeras.Click
        Dim frm As New Pr_ReporteVentasCajeras
        frm.Show()
    End Sub

    Private Sub btVentasExportar_Click(sender As Object, e As EventArgs) Handles btVentasExportar.Click
        Dim frm As New F1_ExcelVenta
        frm.Show()
    End Sub

    Private Sub btDescuentoProd_Click(sender As Object, e As EventArgs) Handles btDescuentoProd.Click
        Dim frm As New F1_DescuentoProd
        frm.Show()
    End Sub

    Private Sub btConfDescuentosProveedor_Click(sender As Object, e As EventArgs) Handles btConfDescuentosProveedor.Click
        Dim frm As New DescuentoXProveedor
        frm.Show()
    End Sub

    Private Sub btComprasDet_Click(sender As Object, e As EventArgs) Handles btComprasDet.Click
        Dim frm As New F1_ExcelCompra
        frm.Show()
    End Sub

    Private Sub btProdSinVender_Click(sender As Object, e As EventArgs) Handles btProdSinVender.Click
        Dim frm As New Pr_ProductosSinVender
        frm.Show()
    End Sub

    Private Sub btRepMixtoDet_Click(sender As Object, e As EventArgs) Handles btRepMixtoDet.Click
        Dim frm As New F1_ExcelMixto
        frm.Show()
    End Sub




    'Private Sub btnCredPagoCliente_Click(sender As Object, e As EventArgs) Handles btnCredPagoCliente.Click
    '    SideNav1.IsMenuExpanded = False
    '    Ventana.Select()
    '    Dim frm As New F0_Cobrar_Cliente
    '    frm._nameButton = btInvMovimiento.Name
    '    frm._modulo = FP_CREDITOS
    '    Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
    '    frm._tab = tab3
    '    Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
    '    superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
    '    tab3.AttachedControl.Controls.Add(panel)
    '    frm.Show()
    '    tab3.Text = frm.Text
    '    tab3.Icon = frm.Icon
    'End Sub

    'Private Sub btnCredPagoClienteVendedor_Click(sender As Object, e As EventArgs) Handles btnCredPagoClienteVendedor.Click
    '    SideNav1.IsMenuExpanded = False
    '    Ventana.Select()
    '    Dim frm As New F0_Cobrar_Vendedor
    '    frm._nameButton = btInvMovimiento.Name
    '    frm._modulo = FP_CREDITOS
    '    Dim tab3 As SuperTabItem = superTabControl3.CreateTab(frm.Text)
    '    frm._tab = tab3
    '    Dim panel As Panel = P_Global._fnCrearPanelVentanas(frm)
    '    superTabControl3.SelectedTabIndex = superTabControl3.Tabs.Count - 1
    '    tab3.AttachedControl.Controls.Add(panel)
    '    frm.Show()
    '    tab3.Text = frm.Text
    '    tab3.Icon = frm.Icon
    'End Sub
End Class