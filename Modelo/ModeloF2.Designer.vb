<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ModeloF2
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.SuperTabPrincipal = New DevComponents.DotNetBar.SuperTabControl()
        Me.SuperTabControlPanelRegistro = New DevComponents.DotNetBar.SuperTabControlPanel()
        Me.PanelPrincipal = New System.Windows.Forms.Panel()
        Me.GroupPanelBuscador = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.JGrM_Buscador = New Janus.Windows.GridEX.GridEX()
        Me.MPanelSup = New System.Windows.Forms.Panel()
        Me.PanelUsuario = New System.Windows.Forms.Panel()
        Me.lbHora = New System.Windows.Forms.Label()
        Me.lbFecha = New System.Windows.Forms.Label()
        Me.lbUsuario = New System.Windows.Forms.Label()
        Me.lblHora = New System.Windows.Forms.Label()
        Me.lblFecha = New System.Windows.Forms.Label()
        Me.lblUsuario = New System.Windows.Forms.Label()
        Me.PanelInferior = New DevComponents.DotNetBar.PanelEx()
        Me.MPanelUserAct = New System.Windows.Forms.Panel()
        Me.BubbleBarUsuario = New DevComponents.DotNetBar.BubbleBar()
        Me.BubbleBarTabUsuario = New DevComponents.DotNetBar.BubbleBarTab(Me.components)
        Me.BBtnUsuario = New DevComponents.DotNetBar.BubbleButton()
        Me.TxtNombreUsu = New System.Windows.Forms.TextBox()
        Me.PanelNavegacion = New System.Windows.Forms.Panel()
        Me.LblPaginacion = New System.Windows.Forms.Label()
        Me.btnUltimo = New DevComponents.DotNetBar.ButtonX()
        Me.btnSiguiente = New DevComponents.DotNetBar.ButtonX()
        Me.btnAnterior = New DevComponents.DotNetBar.ButtonX()
        Me.btnPrimero = New DevComponents.DotNetBar.ButtonX()
        Me.PanelSuperior = New DevComponents.DotNetBar.PanelEx()
        Me.PanelToolBar2 = New System.Windows.Forms.Panel()
        Me.btnImprimir = New DevComponents.DotNetBar.ButtonX()
        Me.PanelToolBar1 = New System.Windows.Forms.Panel()
        Me.btnSalir = New DevComponents.DotNetBar.ButtonX()
        Me.btnGrabar = New DevComponents.DotNetBar.ButtonX()
        Me.btnEliminar = New DevComponents.DotNetBar.ButtonX()
        Me.btnModificar = New DevComponents.DotNetBar.ButtonX()
        Me.btnNuevo = New DevComponents.DotNetBar.ButtonX()
        Me.SupTabItemRegistro = New DevComponents.DotNetBar.SuperTabItem()
        Me.SuperTabControlPanelBuscador = New DevComponents.DotNetBar.SuperTabControlPanel()
        Me.SupTabItemBusqueda = New DevComponents.DotNetBar.SuperTabItem()
        Me.MEP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.MHighlighterFocus = New DevComponents.DotNetBar.Validator.Highlighter()
        Me.MFlyoutUsuario = New DevComponents.DotNetBar.Controls.Flyout(Me.components)
        CType(Me.SuperTabPrincipal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuperTabPrincipal.SuspendLayout()
        Me.SuperTabControlPanelRegistro.SuspendLayout()
        Me.PanelPrincipal.SuspendLayout()
        Me.GroupPanelBuscador.SuspendLayout()
        CType(Me.JGrM_Buscador, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MPanelSup.SuspendLayout()
        Me.PanelUsuario.SuspendLayout()
        Me.PanelInferior.SuspendLayout()
        Me.MPanelUserAct.SuspendLayout()
        CType(Me.BubbleBarUsuario, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelNavegacion.SuspendLayout()
        Me.PanelSuperior.SuspendLayout()
        Me.PanelToolBar2.SuspendLayout()
        Me.PanelToolBar1.SuspendLayout()
        CType(Me.MEP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SuperTabPrincipal
        '
        '
        '
        '
        '
        '
        '
        Me.SuperTabPrincipal.ControlBox.CloseBox.Name = ""
        '
        '
        '
        Me.SuperTabPrincipal.ControlBox.MenuBox.Name = ""
        Me.SuperTabPrincipal.ControlBox.Name = ""
        Me.SuperTabPrincipal.ControlBox.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.SuperTabPrincipal.ControlBox.MenuBox, Me.SuperTabPrincipal.ControlBox.CloseBox})
        Me.SuperTabPrincipal.Controls.Add(Me.SuperTabControlPanelRegistro)
        Me.SuperTabPrincipal.Controls.Add(Me.SuperTabControlPanelBuscador)
        Me.SuperTabPrincipal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SuperTabPrincipal.Location = New System.Drawing.Point(0, 0)
        Me.SuperTabPrincipal.Name = "SuperTabPrincipal"
        Me.SuperTabPrincipal.ReorderTabsEnabled = True
        Me.SuperTabPrincipal.SelectedTabFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.SuperTabPrincipal.SelectedTabIndex = 0
        Me.SuperTabPrincipal.Size = New System.Drawing.Size(884, 561)
        Me.SuperTabPrincipal.TabFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SuperTabPrincipal.TabIndex = 0
        Me.SuperTabPrincipal.Tabs.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.SupTabItemRegistro, Me.SupTabItemBusqueda})
        Me.SuperTabPrincipal.Text = "SuperTabControl1"
        '
        'SuperTabControlPanelRegistro
        '
        Me.SuperTabControlPanelRegistro.Controls.Add(Me.PanelPrincipal)
        Me.SuperTabControlPanelRegistro.Controls.Add(Me.PanelInferior)
        Me.SuperTabControlPanelRegistro.Controls.Add(Me.PanelSuperior)
        Me.SuperTabControlPanelRegistro.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SuperTabControlPanelRegistro.Location = New System.Drawing.Point(0, 25)
        Me.SuperTabControlPanelRegistro.Name = "SuperTabControlPanelRegistro"
        Me.SuperTabControlPanelRegistro.Size = New System.Drawing.Size(884, 536)
        Me.SuperTabControlPanelRegistro.TabIndex = 1
        Me.SuperTabControlPanelRegistro.TabItem = Me.SupTabItemRegistro
        '
        'PanelPrincipal
        '
        Me.PanelPrincipal.BackColor = System.Drawing.SystemColors.Control
        Me.PanelPrincipal.Controls.Add(Me.GroupPanelBuscador)
        Me.PanelPrincipal.Controls.Add(Me.MPanelSup)
        Me.PanelPrincipal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelPrincipal.Location = New System.Drawing.Point(0, 72)
        Me.PanelPrincipal.Name = "PanelPrincipal"
        Me.PanelPrincipal.Size = New System.Drawing.Size(884, 428)
        Me.PanelPrincipal.TabIndex = 0
        '
        'GroupPanelBuscador
        '
        Me.GroupPanelBuscador.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanelBuscador.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanelBuscador.Controls.Add(Me.JGrM_Buscador)
        Me.GroupPanelBuscador.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanelBuscador.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupPanelBuscador.Location = New System.Drawing.Point(0, 184)
        Me.GroupPanelBuscador.Name = "GroupPanelBuscador"
        Me.GroupPanelBuscador.Size = New System.Drawing.Size(884, 244)
        '
        '
        '
        Me.GroupPanelBuscador.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.GroupPanelBuscador.Style.BackColorGradientAngle = 90
        Me.GroupPanelBuscador.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.GroupPanelBuscador.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanelBuscador.Style.BorderBottomWidth = 1
        Me.GroupPanelBuscador.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.GroupPanelBuscador.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanelBuscador.Style.BorderLeftWidth = 1
        Me.GroupPanelBuscador.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanelBuscador.Style.BorderRightWidth = 1
        Me.GroupPanelBuscador.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanelBuscador.Style.BorderTopWidth = 1
        Me.GroupPanelBuscador.Style.CornerDiameter = 4
        Me.GroupPanelBuscador.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanelBuscador.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanelBuscador.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.GroupPanelBuscador.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanelBuscador.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanelBuscador.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanelBuscador.TabIndex = 1
        Me.GroupPanelBuscador.Text = "B U S C A D O R"
        '
        'JGrM_Buscador
        '
        Me.JGrM_Buscador.Dock = System.Windows.Forms.DockStyle.Fill
        Me.JGrM_Buscador.Location = New System.Drawing.Point(0, 0)
        Me.JGrM_Buscador.Name = "JGrM_Buscador"
        Me.JGrM_Buscador.Size = New System.Drawing.Size(878, 223)
        Me.JGrM_Buscador.TabIndex = 0
        '
        'MPanelSup
        '
        Me.MPanelSup.Controls.Add(Me.PanelUsuario)
        Me.MPanelSup.Dock = System.Windows.Forms.DockStyle.Top
        Me.MPanelSup.Location = New System.Drawing.Point(0, 0)
        Me.MPanelSup.Name = "MPanelSup"
        Me.MPanelSup.Size = New System.Drawing.Size(884, 184)
        Me.MPanelSup.TabIndex = 0
        '
        'PanelUsuario
        '
        Me.PanelUsuario.Controls.Add(Me.lbHora)
        Me.PanelUsuario.Controls.Add(Me.lbFecha)
        Me.PanelUsuario.Controls.Add(Me.lbUsuario)
        Me.PanelUsuario.Controls.Add(Me.lblHora)
        Me.PanelUsuario.Controls.Add(Me.lblFecha)
        Me.PanelUsuario.Controls.Add(Me.lblUsuario)
        Me.PanelUsuario.Location = New System.Drawing.Point(652, 29)
        Me.PanelUsuario.Name = "PanelUsuario"
        Me.PanelUsuario.Size = New System.Drawing.Size(220, 100)
        Me.PanelUsuario.TabIndex = 19
        Me.PanelUsuario.TabStop = True
        Me.PanelUsuario.Visible = False
        '
        'lbHora
        '
        Me.lbHora.AutoSize = True
        Me.lbHora.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbHora.Location = New System.Drawing.Point(115, 65)
        Me.lbHora.Name = "lbHora"
        Me.lbHora.Size = New System.Drawing.Size(79, 18)
        Me.lbHora.TabIndex = 6
        Me.lbHora.Text = "USUARIO:"
        '
        'lbFecha
        '
        Me.lbFecha.AutoSize = True
        Me.lbFecha.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbFecha.Location = New System.Drawing.Point(115, 42)
        Me.lbFecha.Name = "lbFecha"
        Me.lbFecha.Size = New System.Drawing.Size(79, 18)
        Me.lbFecha.TabIndex = 5
        Me.lbFecha.Text = "USUARIO:"
        '
        'lbUsuario
        '
        Me.lbUsuario.AutoSize = True
        Me.lbUsuario.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbUsuario.Location = New System.Drawing.Point(115, 19)
        Me.lbUsuario.Name = "lbUsuario"
        Me.lbUsuario.Size = New System.Drawing.Size(79, 18)
        Me.lbUsuario.TabIndex = 4
        Me.lbUsuario.Text = "USUARIO:"
        '
        'lblHora
        '
        Me.lblHora.AutoSize = True
        Me.lblHora.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHora.Location = New System.Drawing.Point(31, 65)
        Me.lblHora.Name = "lblHora"
        Me.lblHora.Size = New System.Drawing.Size(60, 18)
        Me.lblHora.TabIndex = 2
        Me.lblHora.Text = "HORA:"
        '
        'lblFecha
        '
        Me.lblFecha.AutoSize = True
        Me.lblFecha.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFecha.Location = New System.Drawing.Point(31, 43)
        Me.lblFecha.Name = "lblFecha"
        Me.lblFecha.Size = New System.Drawing.Size(68, 18)
        Me.lblFecha.TabIndex = 1
        Me.lblFecha.Text = "FECHA:"
        '
        'lblUsuario
        '
        Me.lblUsuario.AutoSize = True
        Me.lblUsuario.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUsuario.Location = New System.Drawing.Point(31, 19)
        Me.lblUsuario.Name = "lblUsuario"
        Me.lblUsuario.Size = New System.Drawing.Size(87, 18)
        Me.lblUsuario.TabIndex = 0
        Me.lblUsuario.Text = "USUARIO:"
        '
        'PanelInferior
        '
        Me.PanelInferior.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelInferior.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelInferior.Controls.Add(Me.MPanelUserAct)
        Me.PanelInferior.Controls.Add(Me.PanelNavegacion)
        Me.PanelInferior.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelInferior.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelInferior.Location = New System.Drawing.Point(0, 500)
        Me.PanelInferior.Name = "PanelInferior"
        Me.PanelInferior.Size = New System.Drawing.Size(884, 36)
        Me.PanelInferior.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelInferior.Style.BackColor1.Color = System.Drawing.Color.Gold
        Me.PanelInferior.Style.BackColor2.Color = System.Drawing.Color.Gold
        Me.PanelInferior.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelInferior.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelInferior.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelInferior.Style.GradientAngle = 90
        Me.PanelInferior.TabIndex = 4
        '
        'MPanelUserAct
        '
        Me.MPanelUserAct.Controls.Add(Me.BubbleBarUsuario)
        Me.MPanelUserAct.Controls.Add(Me.TxtNombreUsu)
        Me.MPanelUserAct.Dock = System.Windows.Forms.DockStyle.Right
        Me.MPanelUserAct.Location = New System.Drawing.Point(684, 0)
        Me.MPanelUserAct.Name = "MPanelUserAct"
        Me.MPanelUserAct.Size = New System.Drawing.Size(200, 36)
        Me.MPanelUserAct.TabIndex = 21
        '
        'BubbleBarUsuario
        '
        Me.BubbleBarUsuario.Alignment = DevComponents.DotNetBar.eBubbleButtonAlignment.Bottom
        Me.BubbleBarUsuario.AntiAlias = True
        '
        '
        '
        Me.BubbleBarUsuario.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.BubbleBarUsuario.ButtonBackAreaStyle.BackColor = System.Drawing.Color.Transparent
        Me.BubbleBarUsuario.ButtonBackAreaStyle.BorderBottomWidth = 1
        Me.BubbleBarUsuario.ButtonBackAreaStyle.BorderColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.BubbleBarUsuario.ButtonBackAreaStyle.BorderLeftWidth = 1
        Me.BubbleBarUsuario.ButtonBackAreaStyle.BorderRightWidth = 1
        Me.BubbleBarUsuario.ButtonBackAreaStyle.BorderTopWidth = 1
        Me.BubbleBarUsuario.ButtonBackAreaStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.BubbleBarUsuario.ButtonBackAreaStyle.PaddingBottom = 3
        Me.BubbleBarUsuario.ButtonBackAreaStyle.PaddingLeft = 3
        Me.BubbleBarUsuario.ButtonBackAreaStyle.PaddingRight = 3
        Me.BubbleBarUsuario.ButtonBackAreaStyle.PaddingTop = 3
        Me.BubbleBarUsuario.Dock = System.Windows.Forms.DockStyle.Left
        Me.BubbleBarUsuario.ImageSizeNormal = New System.Drawing.Size(24, 24)
        Me.BubbleBarUsuario.Location = New System.Drawing.Point(0, 0)
        Me.BubbleBarUsuario.MouseOverTabColors.BorderColor = System.Drawing.SystemColors.Highlight
        Me.BubbleBarUsuario.Name = "BubbleBarUsuario"
        Me.BubbleBarUsuario.SelectedTab = Me.BubbleBarTabUsuario
        Me.BubbleBarUsuario.SelectedTabColors.BorderColor = System.Drawing.Color.Black
        Me.BubbleBarUsuario.Size = New System.Drawing.Size(50, 36)
        Me.BubbleBarUsuario.TabIndex = 13
        Me.BubbleBarUsuario.Tabs.Add(Me.BubbleBarTabUsuario)
        Me.BubbleBarUsuario.TabsVisible = False
        Me.BubbleBarUsuario.Text = "BubbleBar5"
        '
        'BubbleBarTabUsuario
        '
        Me.BubbleBarTabUsuario.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.BubbleBarTabUsuario.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(228, Byte), Integer))
        Me.BubbleBarTabUsuario.Buttons.AddRange(New DevComponents.DotNetBar.BubbleButton() {Me.BBtnUsuario})
        Me.BubbleBarTabUsuario.DarkBorderColor = System.Drawing.Color.FromArgb(CType(CType(190, Byte), Integer), CType(CType(105, Byte), Integer), CType(CType(105, Byte), Integer), CType(CType(105, Byte), Integer))
        Me.BubbleBarTabUsuario.LightBorderColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BubbleBarTabUsuario.Name = "BubbleBarTabUsuario"
        Me.BubbleBarTabUsuario.PredefinedColor = DevComponents.DotNetBar.eTabItemColor.Blue
        Me.BubbleBarTabUsuario.Text = "BubbleBarTab3"
        Me.BubbleBarTabUsuario.TextColor = System.Drawing.Color.Black
        '
        'BBtnUsuario
        '
        Me.BBtnUsuario.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BBtnUsuario.Image = Global.Modelo.My.Resources.Resources.usuario
        Me.BBtnUsuario.ImageLarge = Global.Modelo.My.Resources.Resources.usuario
        Me.BBtnUsuario.Name = "BBtnUsuario"
        '
        'TxtNombreUsu
        '
        Me.TxtNombreUsu.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNombreUsu.Location = New System.Drawing.Point(59, 5)
        Me.TxtNombreUsu.Multiline = True
        Me.TxtNombreUsu.Name = "TxtNombreUsu"
        Me.TxtNombreUsu.Size = New System.Drawing.Size(135, 27)
        Me.TxtNombreUsu.TabIndex = 14
        '
        'PanelNavegacion
        '
        Me.PanelNavegacion.Controls.Add(Me.LblPaginacion)
        Me.PanelNavegacion.Controls.Add(Me.btnUltimo)
        Me.PanelNavegacion.Controls.Add(Me.btnSiguiente)
        Me.PanelNavegacion.Controls.Add(Me.btnAnterior)
        Me.PanelNavegacion.Controls.Add(Me.btnPrimero)
        Me.PanelNavegacion.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelNavegacion.Location = New System.Drawing.Point(0, 0)
        Me.PanelNavegacion.Name = "PanelNavegacion"
        Me.PanelNavegacion.Size = New System.Drawing.Size(288, 36)
        Me.PanelNavegacion.TabIndex = 20
        '
        'LblPaginacion
        '
        Me.LblPaginacion.Dock = System.Windows.Forms.DockStyle.Right
        Me.LblPaginacion.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPaginacion.Location = New System.Drawing.Point(192, 0)
        Me.LblPaginacion.Name = "LblPaginacion"
        Me.LblPaginacion.Size = New System.Drawing.Size(96, 36)
        Me.LblPaginacion.TabIndex = 22
        Me.LblPaginacion.Text = "12345/12345"
        Me.LblPaginacion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnUltimo
        '
        Me.btnUltimo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnUltimo.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange
        Me.btnUltimo.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnUltimo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUltimo.Image = Global.Modelo.My.Resources.Resources.ultimo
        Me.btnUltimo.ImageFixedSize = New System.Drawing.Size(30, 30)
        Me.btnUltimo.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top
        Me.btnUltimo.Location = New System.Drawing.Point(129, 0)
        Me.btnUltimo.Name = "btnUltimo"
        Me.btnUltimo.Size = New System.Drawing.Size(43, 36)
        Me.btnUltimo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnUltimo.TabIndex = 14
        '
        'btnSiguiente
        '
        Me.btnSiguiente.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnSiguiente.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange
        Me.btnSiguiente.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnSiguiente.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSiguiente.Image = Global.Modelo.My.Resources.Resources.siguiente
        Me.btnSiguiente.ImageFixedSize = New System.Drawing.Size(30, 30)
        Me.btnSiguiente.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top
        Me.btnSiguiente.Location = New System.Drawing.Point(86, 0)
        Me.btnSiguiente.Name = "btnSiguiente"
        Me.btnSiguiente.Size = New System.Drawing.Size(43, 36)
        Me.btnSiguiente.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnSiguiente.TabIndex = 13
        '
        'btnAnterior
        '
        Me.btnAnterior.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnAnterior.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange
        Me.btnAnterior.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnAnterior.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAnterior.Image = Global.Modelo.My.Resources.Resources.anterior
        Me.btnAnterior.ImageFixedSize = New System.Drawing.Size(30, 30)
        Me.btnAnterior.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top
        Me.btnAnterior.Location = New System.Drawing.Point(43, 0)
        Me.btnAnterior.Name = "btnAnterior"
        Me.btnAnterior.Size = New System.Drawing.Size(43, 36)
        Me.btnAnterior.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnAnterior.TabIndex = 12
        '
        'btnPrimero
        '
        Me.btnPrimero.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnPrimero.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange
        Me.btnPrimero.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnPrimero.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrimero.Image = Global.Modelo.My.Resources.Resources.inicio
        Me.btnPrimero.ImageFixedSize = New System.Drawing.Size(30, 30)
        Me.btnPrimero.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top
        Me.btnPrimero.Location = New System.Drawing.Point(0, 0)
        Me.btnPrimero.Name = "btnPrimero"
        Me.btnPrimero.Size = New System.Drawing.Size(43, 36)
        Me.btnPrimero.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnPrimero.TabIndex = 11
        '
        'PanelSuperior
        '
        Me.PanelSuperior.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelSuperior.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP
        Me.PanelSuperior.Controls.Add(Me.PanelToolBar2)
        Me.PanelSuperior.Controls.Add(Me.PanelToolBar1)
        Me.PanelSuperior.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelSuperior.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelSuperior.Location = New System.Drawing.Point(0, 0)
        Me.PanelSuperior.Name = "PanelSuperior"
        Me.PanelSuperior.Size = New System.Drawing.Size(884, 72)
        Me.PanelSuperior.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelSuperior.Style.BackColor1.Color = System.Drawing.Color.Yellow
        Me.PanelSuperior.Style.BackColor2.Color = System.Drawing.Color.Khaki
        Me.PanelSuperior.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelSuperior.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelSuperior.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelSuperior.Style.GradientAngle = 90
        Me.PanelSuperior.TabIndex = 0
        '
        'PanelToolBar2
        '
        Me.PanelToolBar2.Controls.Add(Me.btnImprimir)
        Me.PanelToolBar2.Dock = System.Windows.Forms.DockStyle.Right
        Me.PanelToolBar2.Location = New System.Drawing.Point(804, 0)
        Me.PanelToolBar2.Name = "PanelToolBar2"
        Me.PanelToolBar2.Size = New System.Drawing.Size(80, 72)
        Me.PanelToolBar2.TabIndex = 6
        '
        'btnImprimir
        '
        Me.btnImprimir.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnImprimir.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange
        Me.btnImprimir.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnImprimir.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnImprimir.Image = Global.Modelo.My.Resources.Resources.PRINT
        Me.btnImprimir.ImageFixedSize = New System.Drawing.Size(48, 48)
        Me.btnImprimir.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top
        Me.btnImprimir.Location = New System.Drawing.Point(8, 0)
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.Size = New System.Drawing.Size(72, 72)
        Me.btnImprimir.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnImprimir.TabIndex = 11
        Me.btnImprimir.Text = "IMPRIMIR"
        Me.btnImprimir.TextColor = System.Drawing.Color.Black
        '
        'PanelToolBar1
        '
        Me.PanelToolBar1.Controls.Add(Me.btnSalir)
        Me.PanelToolBar1.Controls.Add(Me.btnGrabar)
        Me.PanelToolBar1.Controls.Add(Me.btnEliminar)
        Me.PanelToolBar1.Controls.Add(Me.btnModificar)
        Me.PanelToolBar1.Controls.Add(Me.btnNuevo)
        Me.PanelToolBar1.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelToolBar1.Location = New System.Drawing.Point(0, 0)
        Me.PanelToolBar1.Name = "PanelToolBar1"
        Me.PanelToolBar1.Size = New System.Drawing.Size(376, 72)
        Me.PanelToolBar1.TabIndex = 5
        '
        'btnSalir
        '
        Me.btnSalir.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnSalir.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange
        Me.btnSalir.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnSalir.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSalir.Image = Global.Modelo.My.Resources.Resources.SALIR2
        Me.btnSalir.ImageFixedSize = New System.Drawing.Size(48, 48)
        Me.btnSalir.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top
        Me.btnSalir.Location = New System.Drawing.Point(288, 0)
        Me.btnSalir.Name = "btnSalir"
        Me.btnSalir.Size = New System.Drawing.Size(72, 72)
        Me.btnSalir.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnSalir.TabIndex = 10
        Me.btnSalir.Text = "SALIR"
        Me.btnSalir.TextColor = System.Drawing.Color.Black
        '
        'btnGrabar
        '
        Me.btnGrabar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnGrabar.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange
        Me.btnGrabar.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnGrabar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGrabar.Image = Global.Modelo.My.Resources.Resources.save
        Me.btnGrabar.ImageFixedSize = New System.Drawing.Size(48, 48)
        Me.btnGrabar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top
        Me.btnGrabar.Location = New System.Drawing.Point(216, 0)
        Me.btnGrabar.Name = "btnGrabar"
        Me.btnGrabar.Size = New System.Drawing.Size(72, 72)
        Me.btnGrabar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnGrabar.TabIndex = 9
        Me.btnGrabar.Text = "GRABAR"
        Me.btnGrabar.TextColor = System.Drawing.Color.Black
        '
        'btnEliminar
        '
        Me.btnEliminar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnEliminar.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange
        Me.btnEliminar.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnEliminar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEliminar.Image = Global.Modelo.My.Resources.Resources.ELIMINAR
        Me.btnEliminar.ImageFixedSize = New System.Drawing.Size(48, 48)
        Me.btnEliminar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top
        Me.btnEliminar.Location = New System.Drawing.Point(144, 0)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.Size = New System.Drawing.Size(72, 72)
        Me.btnEliminar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnEliminar.TabIndex = 8
        Me.btnEliminar.Text = "ELIMNAR"
        Me.btnEliminar.TextColor = System.Drawing.Color.Black
        '
        'btnModificar
        '
        Me.btnModificar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnModificar.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange
        Me.btnModificar.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnModificar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnModificar.Image = Global.Modelo.My.Resources.Resources.EDITAR3
        Me.btnModificar.ImageFixedSize = New System.Drawing.Size(48, 48)
        Me.btnModificar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top
        Me.btnModificar.Location = New System.Drawing.Point(72, 0)
        Me.btnModificar.Name = "btnModificar"
        Me.btnModificar.Size = New System.Drawing.Size(72, 72)
        Me.btnModificar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnModificar.TabIndex = 7
        Me.btnModificar.Text = "MODIFICAR"
        Me.btnModificar.TextColor = System.Drawing.Color.Black
        '
        'btnNuevo
        '
        Me.btnNuevo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnNuevo.BackColor = System.Drawing.Color.Transparent
        Me.btnNuevo.ColorTable = DevComponents.DotNetBar.eButtonColor.Magenta
        Me.btnNuevo.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnNuevo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNuevo.Image = Global.Modelo.My.Resources.Resources.NUEVO
        Me.btnNuevo.ImageFixedSize = New System.Drawing.Size(48, 48)
        Me.btnNuevo.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top
        Me.btnNuevo.Location = New System.Drawing.Point(0, 0)
        Me.btnNuevo.Name = "btnNuevo"
        Me.btnNuevo.Size = New System.Drawing.Size(72, 72)
        Me.btnNuevo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnNuevo.TabIndex = 6
        Me.btnNuevo.Text = "NUEVO"
        Me.btnNuevo.TextColor = System.Drawing.Color.Black
        '
        'SupTabItemRegistro
        '
        Me.SupTabItemRegistro.AttachedControl = Me.SuperTabControlPanelRegistro
        Me.SupTabItemRegistro.GlobalItem = False
        Me.SupTabItemRegistro.Name = "SupTabItemRegistro"
        Me.SupTabItemRegistro.Text = "REGISTRO"
        '
        'SuperTabControlPanelBuscador
        '
        Me.SuperTabControlPanelBuscador.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SuperTabControlPanelBuscador.Location = New System.Drawing.Point(0, 25)
        Me.SuperTabControlPanelBuscador.Name = "SuperTabControlPanelBuscador"
        Me.SuperTabControlPanelBuscador.Size = New System.Drawing.Size(884, 536)
        Me.SuperTabControlPanelBuscador.TabIndex = 0
        Me.SuperTabControlPanelBuscador.TabItem = Me.SupTabItemBusqueda
        '
        'SupTabItemBusqueda
        '
        Me.SupTabItemBusqueda.AttachedControl = Me.SuperTabControlPanelBuscador
        Me.SupTabItemBusqueda.GlobalItem = False
        Me.SupTabItemBusqueda.Name = "SupTabItemBusqueda"
        Me.SupTabItemBusqueda.Text = "BUSQUEDA"
        '
        'MEP
        '
        Me.MEP.ContainerControl = Me
        '
        'MHighlighterFocus
        '
        Me.MHighlighterFocus.ContainerControl = Me
        '
        'MFlyoutUsuario
        '
        Me.MFlyoutUsuario.DropShadow = False
        Me.MFlyoutUsuario.Parent = Me
        Me.MFlyoutUsuario.TargetControl = Me.BubbleBarUsuario
        '
        'ModeloF1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(884, 561)
        Me.Controls.Add(Me.SuperTabPrincipal)
        Me.KeyPreview = True
        Me.Name = "ModeloF1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "{...TITULO...}"
        CType(Me.SuperTabPrincipal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SuperTabPrincipal.ResumeLayout(False)
        Me.SuperTabControlPanelRegistro.ResumeLayout(False)
        Me.PanelPrincipal.ResumeLayout(False)
        Me.GroupPanelBuscador.ResumeLayout(False)
        CType(Me.JGrM_Buscador, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MPanelSup.ResumeLayout(False)
        Me.PanelUsuario.ResumeLayout(False)
        Me.PanelUsuario.PerformLayout()
        Me.PanelInferior.ResumeLayout(False)
        Me.MPanelUserAct.ResumeLayout(False)
        Me.MPanelUserAct.PerformLayout()
        CType(Me.BubbleBarUsuario, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelNavegacion.ResumeLayout(False)
        Me.PanelSuperior.ResumeLayout(False)
        Me.PanelToolBar2.ResumeLayout(False)
        Me.PanelToolBar1.ResumeLayout(False)
        CType(Me.MEP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Protected WithEvents SuperTabPrincipal As DevComponents.DotNetBar.SuperTabControl
    Protected WithEvents SuperTabControlPanelBuscador As DevComponents.DotNetBar.SuperTabControlPanel
    Protected WithEvents SupTabItemBusqueda As DevComponents.DotNetBar.SuperTabItem
    Protected WithEvents SuperTabControlPanelRegistro As DevComponents.DotNetBar.SuperTabControlPanel
    Protected WithEvents SupTabItemRegistro As DevComponents.DotNetBar.SuperTabItem
    Protected WithEvents PanelSuperior As DevComponents.DotNetBar.PanelEx
    Protected WithEvents PanelInferior As DevComponents.DotNetBar.PanelEx
    Protected WithEvents BubbleBarUsuario As DevComponents.DotNetBar.BubbleBar
    Protected WithEvents BubbleBarTabUsuario As DevComponents.DotNetBar.BubbleBarTab
    Protected WithEvents BBtnUsuario As DevComponents.DotNetBar.BubbleButton
    Protected WithEvents TxtNombreUsu As System.Windows.Forms.TextBox
    Protected WithEvents PanelToolBar1 As System.Windows.Forms.Panel
    Protected WithEvents btnSalir As DevComponents.DotNetBar.ButtonX
    Protected WithEvents btnGrabar As DevComponents.DotNetBar.ButtonX
    Protected WithEvents btnEliminar As DevComponents.DotNetBar.ButtonX
    Protected WithEvents btnModificar As DevComponents.DotNetBar.ButtonX
    Protected WithEvents btnNuevo As DevComponents.DotNetBar.ButtonX
    Protected WithEvents PanelToolBar2 As System.Windows.Forms.Panel
    Protected WithEvents MPanelSup As System.Windows.Forms.Panel
    Protected WithEvents PanelPrincipal As System.Windows.Forms.Panel
    Protected WithEvents GroupPanelBuscador As DevComponents.DotNetBar.Controls.GroupPanel
    Protected WithEvents JGrM_Buscador As Janus.Windows.GridEX.GridEX
    Protected WithEvents PanelUsuario As System.Windows.Forms.Panel
    Protected WithEvents lbHora As System.Windows.Forms.Label
    Protected WithEvents lbFecha As System.Windows.Forms.Label
    Protected WithEvents lbUsuario As System.Windows.Forms.Label
    Protected WithEvents lblHora As System.Windows.Forms.Label
    Protected WithEvents lblFecha As System.Windows.Forms.Label
    Protected WithEvents lblUsuario As System.Windows.Forms.Label
    Protected WithEvents btnImprimir As DevComponents.DotNetBar.ButtonX
    Protected WithEvents PanelNavegacion As System.Windows.Forms.Panel
    Protected WithEvents btnUltimo As DevComponents.DotNetBar.ButtonX
    Protected WithEvents btnSiguiente As DevComponents.DotNetBar.ButtonX
    Protected WithEvents btnAnterior As DevComponents.DotNetBar.ButtonX
    Protected WithEvents btnPrimero As DevComponents.DotNetBar.ButtonX
    Protected WithEvents MPanelUserAct As System.Windows.Forms.Panel
    Protected WithEvents LblPaginacion As System.Windows.Forms.Label
    Protected WithEvents MEP As System.Windows.Forms.ErrorProvider
    Protected WithEvents MHighlighterFocus As DevComponents.DotNetBar.Validator.Highlighter
    Protected WithEvents MFlyoutUsuario As DevComponents.DotNetBar.Controls.Flyout
End Class
