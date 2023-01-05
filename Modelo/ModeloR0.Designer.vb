<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ModeloR0
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
        Dim SuperTabColorTable1 As DevComponents.DotNetBar.Rendering.SuperTabColorTable = New DevComponents.DotNetBar.Rendering.SuperTabColorTable()
        Dim SuperTabLinearGradientColorTable1 As DevComponents.DotNetBar.Rendering.SuperTabLinearGradientColorTable = New DevComponents.DotNetBar.Rendering.SuperTabLinearGradientColorTable()
        Me.SuperTabPrincipal = New DevComponents.DotNetBar.SuperTabControl()
        Me.SuperTabControlPanelRegistro = New DevComponents.DotNetBar.SuperTabControlPanel()
        Me.PanelPrincipal = New System.Windows.Forms.Panel()
        Me.MReportViewer = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.PanelUsuario = New System.Windows.Forms.Panel()
        Me.lbHora = New System.Windows.Forms.Label()
        Me.lbFecha = New System.Windows.Forms.Label()
        Me.lbUsuario = New System.Windows.Forms.Label()
        Me.lblHora = New System.Windows.Forms.Label()
        Me.lblFecha = New System.Windows.Forms.Label()
        Me.lblUsuario = New System.Windows.Forms.Label()
        Me.PanelIzq = New System.Windows.Forms.Panel()
        Me.MGPFiltros = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.PanelSuperior = New DevComponents.DotNetBar.PanelEx()
        Me.PanelToolBar1 = New System.Windows.Forms.Panel()
        Me.btnSalir = New DevComponents.DotNetBar.ButtonX()
        Me.btnGenerar = New DevComponents.DotNetBar.ButtonX()
        Me.PanelInferior = New DevComponents.DotNetBar.PanelEx()
        Me.MPicture = New System.Windows.Forms.PictureBox()
        Me.MPanelUserAct = New System.Windows.Forms.Panel()
        Me.BubbleBarUsuario = New DevComponents.DotNetBar.BubbleBar()
        Me.BubbleBarTabUsuario = New DevComponents.DotNetBar.BubbleBarTab(Me.components)
        Me.BBtnUsuario = New DevComponents.DotNetBar.BubbleButton()
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
        Me.PanelUsuario.SuspendLayout()
        Me.PanelIzq.SuspendLayout()
        Me.PanelSuperior.SuspendLayout()
        Me.PanelToolBar1.SuspendLayout()
        Me.PanelInferior.SuspendLayout()
        CType(Me.MPicture, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MPanelUserAct.SuspendLayout()
        CType(Me.BubbleBarUsuario, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SuperTabPrincipal.Font = New System.Drawing.Font("Georgia", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SuperTabPrincipal.HorizontalText = False
        Me.SuperTabPrincipal.Location = New System.Drawing.Point(0, 0)
        Me.SuperTabPrincipal.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.SuperTabPrincipal.Name = "SuperTabPrincipal"
        Me.SuperTabPrincipal.ReorderTabsEnabled = True
        Me.SuperTabPrincipal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.SuperTabPrincipal.SelectedTabFont = New System.Drawing.Font("Georgia", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SuperTabPrincipal.SelectedTabIndex = 0
        Me.SuperTabPrincipal.Size = New System.Drawing.Size(1800, 897)
        Me.SuperTabPrincipal.TabAlignment = DevComponents.DotNetBar.eTabStripAlignment.Right
        Me.SuperTabPrincipal.TabFont = New System.Drawing.Font("Georgia", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SuperTabPrincipal.TabIndex = 0
        Me.SuperTabPrincipal.Tabs.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.SupTabItemRegistro, Me.SupTabItemBusqueda})
        SuperTabLinearGradientColorTable1.Colors = New System.Drawing.Color() {System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))}
        SuperTabColorTable1.Background = SuperTabLinearGradientColorTable1
        Me.SuperTabPrincipal.TabStripColor = SuperTabColorTable1
        Me.SuperTabPrincipal.TabStyle = DevComponents.DotNetBar.eSuperTabStyle.OfficeMobile2014
        Me.SuperTabPrincipal.Text = "SuperTabControl1"
        '
        'SuperTabControlPanelRegistro
        '
        Me.SuperTabControlPanelRegistro.Controls.Add(Me.PanelPrincipal)
        Me.SuperTabControlPanelRegistro.Controls.Add(Me.PanelIzq)
        Me.SuperTabControlPanelRegistro.Controls.Add(Me.PanelInferior)
        Me.SuperTabControlPanelRegistro.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SuperTabControlPanelRegistro.Location = New System.Drawing.Point(0, 0)
        Me.SuperTabControlPanelRegistro.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.SuperTabControlPanelRegistro.Name = "SuperTabControlPanelRegistro"
        Me.SuperTabControlPanelRegistro.Size = New System.Drawing.Size(1765, 897)
        Me.SuperTabControlPanelRegistro.TabIndex = 1
        Me.SuperTabControlPanelRegistro.TabItem = Me.SupTabItemRegistro
        '
        'PanelPrincipal
        '
        Me.PanelPrincipal.BackColor = System.Drawing.SystemColors.Control
        Me.PanelPrincipal.Controls.Add(Me.MReportViewer)
        Me.PanelPrincipal.Controls.Add(Me.PanelUsuario)
        Me.PanelPrincipal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelPrincipal.Location = New System.Drawing.Point(484, 0)
        Me.PanelPrincipal.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.PanelPrincipal.Name = "PanelPrincipal"
        Me.PanelPrincipal.Size = New System.Drawing.Size(1281, 853)
        Me.PanelPrincipal.TabIndex = 0
        '
        'MReportViewer
        '
        Me.MReportViewer.ActiveViewIndex = -1
        Me.MReportViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.MReportViewer.Cursor = System.Windows.Forms.Cursors.Default
        Me.MReportViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MReportViewer.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MReportViewer.Location = New System.Drawing.Point(0, 0)
        Me.MReportViewer.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MReportViewer.Name = "MReportViewer"
        Me.MReportViewer.Size = New System.Drawing.Size(1281, 853)
        Me.MReportViewer.TabIndex = 20
        Me.MReportViewer.ToolPanelWidth = 267
        '
        'PanelUsuario
        '
        Me.PanelUsuario.Controls.Add(Me.lbHora)
        Me.PanelUsuario.Controls.Add(Me.lbFecha)
        Me.PanelUsuario.Controls.Add(Me.lbUsuario)
        Me.PanelUsuario.Controls.Add(Me.lblHora)
        Me.PanelUsuario.Controls.Add(Me.lblFecha)
        Me.PanelUsuario.Controls.Add(Me.lblUsuario)
        Me.PanelUsuario.Location = New System.Drawing.Point(609, 55)
        Me.PanelUsuario.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.PanelUsuario.Name = "PanelUsuario"
        Me.PanelUsuario.Size = New System.Drawing.Size(293, 123)
        Me.PanelUsuario.TabIndex = 19
        Me.PanelUsuario.TabStop = True
        Me.PanelUsuario.Visible = False
        '
        'lbHora
        '
        Me.lbHora.AutoSize = True
        Me.lbHora.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbHora.Location = New System.Drawing.Point(153, 80)
        Me.lbHora.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbHora.Name = "lbHora"
        Me.lbHora.Size = New System.Drawing.Size(98, 24)
        Me.lbHora.TabIndex = 6
        Me.lbHora.Text = "USUARIO:"
        '
        'lbFecha
        '
        Me.lbFecha.AutoSize = True
        Me.lbFecha.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbFecha.Location = New System.Drawing.Point(153, 52)
        Me.lbFecha.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbFecha.Name = "lbFecha"
        Me.lbFecha.Size = New System.Drawing.Size(98, 24)
        Me.lbFecha.TabIndex = 5
        Me.lbFecha.Text = "USUARIO:"
        '
        'lbUsuario
        '
        Me.lbUsuario.AutoSize = True
        Me.lbUsuario.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbUsuario.Location = New System.Drawing.Point(153, 23)
        Me.lbUsuario.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbUsuario.Name = "lbUsuario"
        Me.lbUsuario.Size = New System.Drawing.Size(98, 24)
        Me.lbUsuario.TabIndex = 4
        Me.lbUsuario.Text = "USUARIO:"
        '
        'lblHora
        '
        Me.lblHora.AutoSize = True
        Me.lblHora.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHora.Location = New System.Drawing.Point(41, 80)
        Me.lblHora.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHora.Name = "lblHora"
        Me.lblHora.Size = New System.Drawing.Size(75, 24)
        Me.lblHora.TabIndex = 2
        Me.lblHora.Text = "HORA:"
        '
        'lblFecha
        '
        Me.lblFecha.AutoSize = True
        Me.lblFecha.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFecha.Location = New System.Drawing.Point(41, 53)
        Me.lblFecha.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFecha.Name = "lblFecha"
        Me.lblFecha.Size = New System.Drawing.Size(86, 24)
        Me.lblFecha.TabIndex = 1
        Me.lblFecha.Text = "FECHA:"
        '
        'lblUsuario
        '
        Me.lblUsuario.AutoSize = True
        Me.lblUsuario.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUsuario.Location = New System.Drawing.Point(41, 23)
        Me.lblUsuario.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblUsuario.Name = "lblUsuario"
        Me.lblUsuario.Size = New System.Drawing.Size(106, 24)
        Me.lblUsuario.TabIndex = 0
        Me.lblUsuario.Text = "USUARIO:"
        '
        'PanelIzq
        '
        Me.PanelIzq.BackColor = System.Drawing.Color.White
        Me.PanelIzq.Controls.Add(Me.MGPFiltros)
        Me.PanelIzq.Controls.Add(Me.PanelSuperior)
        Me.PanelIzq.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelIzq.Location = New System.Drawing.Point(0, 0)
        Me.PanelIzq.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.PanelIzq.Name = "PanelIzq"
        Me.PanelIzq.Size = New System.Drawing.Size(484, 853)
        Me.PanelIzq.TabIndex = 20
        '
        'MGPFiltros
        '
        Me.MGPFiltros.CanvasColor = System.Drawing.SystemColors.Control
        Me.MGPFiltros.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.MGPFiltros.DisabledBackColor = System.Drawing.Color.Empty
        Me.MGPFiltros.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MGPFiltros.Font = New System.Drawing.Font("Georgia", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MGPFiltros.Location = New System.Drawing.Point(0, 89)
        Me.MGPFiltros.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MGPFiltros.Name = "MGPFiltros"
        Me.MGPFiltros.Size = New System.Drawing.Size(484, 764)
        '
        '
        '
        Me.MGPFiltros.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.MGPFiltros.Style.BackColorGradientAngle = 90
        Me.MGPFiltros.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.MGPFiltros.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.MGPFiltros.Style.BorderBottomWidth = 1
        Me.MGPFiltros.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.MGPFiltros.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.MGPFiltros.Style.BorderLeftWidth = 1
        Me.MGPFiltros.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.MGPFiltros.Style.BorderRightWidth = 1
        Me.MGPFiltros.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.MGPFiltros.Style.BorderTopWidth = 1
        Me.MGPFiltros.Style.CornerDiameter = 4
        Me.MGPFiltros.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.MGPFiltros.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.MGPFiltros.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.MGPFiltros.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.MGPFiltros.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.MGPFiltros.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.MGPFiltros.TabIndex = 11
        Me.MGPFiltros.Text = "F I L T R O S"
        '
        'PanelSuperior
        '
        Me.PanelSuperior.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelSuperior.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP
        Me.PanelSuperior.Controls.Add(Me.PanelToolBar1)
        Me.PanelSuperior.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelSuperior.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelSuperior.Location = New System.Drawing.Point(0, 0)
        Me.PanelSuperior.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.PanelSuperior.Name = "PanelSuperior"
        Me.PanelSuperior.Size = New System.Drawing.Size(484, 89)
        Me.PanelSuperior.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelSuperior.Style.BackColor1.Color = System.Drawing.Color.Yellow
        Me.PanelSuperior.Style.BackColor2.Color = System.Drawing.Color.Khaki
        Me.PanelSuperior.Style.BackgroundImage = Global.Modelo.My.Resources.Resources.fondo
        Me.PanelSuperior.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelSuperior.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelSuperior.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelSuperior.Style.GradientAngle = 90
        Me.PanelSuperior.TabIndex = 0
        '
        'PanelToolBar1
        '
        Me.PanelToolBar1.Controls.Add(Me.btnSalir)
        Me.PanelToolBar1.Controls.Add(Me.btnGenerar)
        Me.PanelToolBar1.Location = New System.Drawing.Point(132, 0)
        Me.PanelToolBar1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.PanelToolBar1.Name = "PanelToolBar1"
        Me.PanelToolBar1.Size = New System.Drawing.Size(191, 89)
        Me.PanelToolBar1.TabIndex = 5
        '
        'btnSalir
        '
        Me.btnSalir.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnSalir.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange
        Me.btnSalir.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnSalir.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSalir.Image = Global.Modelo.My.Resources.Resources.atras
        Me.btnSalir.ImageFixedSize = New System.Drawing.Size(48, 48)
        Me.btnSalir.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top
        Me.btnSalir.Location = New System.Drawing.Point(96, 0)
        Me.btnSalir.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnSalir.Name = "btnSalir"
        Me.btnSalir.Size = New System.Drawing.Size(96, 89)
        Me.btnSalir.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnSalir.TabIndex = 10
        Me.btnSalir.Text = "SALIR"
        Me.btnSalir.TextColor = System.Drawing.Color.White
        '
        'btnGenerar
        '
        Me.btnGenerar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnGenerar.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange
        Me.btnGenerar.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnGenerar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGenerar.Image = Global.Modelo.My.Resources.Resources.pie_chart
        Me.btnGenerar.ImageFixedSize = New System.Drawing.Size(48, 48)
        Me.btnGenerar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top
        Me.btnGenerar.Location = New System.Drawing.Point(0, 0)
        Me.btnGenerar.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnGenerar.Name = "btnGenerar"
        Me.btnGenerar.Size = New System.Drawing.Size(96, 89)
        Me.btnGenerar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnGenerar.TabIndex = 9
        Me.btnGenerar.Text = "GENERAR"
        Me.btnGenerar.TextColor = System.Drawing.Color.White
        '
        'PanelInferior
        '
        Me.PanelInferior.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelInferior.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelInferior.Controls.Add(Me.MPicture)
        Me.PanelInferior.Controls.Add(Me.MPanelUserAct)
        Me.PanelInferior.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelInferior.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelInferior.Location = New System.Drawing.Point(0, 853)
        Me.PanelInferior.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.PanelInferior.Name = "PanelInferior"
        Me.PanelInferior.Size = New System.Drawing.Size(1765, 44)
        Me.PanelInferior.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelInferior.Style.BackColor1.Color = System.Drawing.Color.Gold
        Me.PanelInferior.Style.BackColor2.Color = System.Drawing.Color.Gold
        Me.PanelInferior.Style.BackgroundImage = Global.Modelo.My.Resources.Resources.fondoPanel
        Me.PanelInferior.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelInferior.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelInferior.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelInferior.Style.GradientAngle = 90
        Me.PanelInferior.TabIndex = 4
        '
        'MPicture
        '
        Me.MPicture.Dock = System.Windows.Forms.DockStyle.Left
        Me.MPicture.Location = New System.Drawing.Point(0, 0)
        Me.MPicture.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MPicture.Name = "MPicture"
        Me.MPicture.Size = New System.Drawing.Size(251, 44)
        Me.MPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.MPicture.TabIndex = 22
        Me.MPicture.TabStop = False
        '
        'MPanelUserAct
        '
        Me.MPanelUserAct.Controls.Add(Me.BubbleBarUsuario)
        Me.MPanelUserAct.Dock = System.Windows.Forms.DockStyle.Right
        Me.MPanelUserAct.Location = New System.Drawing.Point(1498, 0)
        Me.MPanelUserAct.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MPanelUserAct.Name = "MPanelUserAct"
        Me.MPanelUserAct.Size = New System.Drawing.Size(267, 44)
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
        Me.BubbleBarUsuario.Dock = System.Windows.Forms.DockStyle.Right
        Me.BubbleBarUsuario.ImageSizeNormal = New System.Drawing.Size(24, 24)
        Me.BubbleBarUsuario.Location = New System.Drawing.Point(-9, 0)
        Me.BubbleBarUsuario.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BubbleBarUsuario.MouseOverTabColors.BorderColor = System.Drawing.SystemColors.Highlight
        Me.BubbleBarUsuario.Name = "BubbleBarUsuario"
        Me.BubbleBarUsuario.SelectedTab = Me.BubbleBarTabUsuario
        Me.BubbleBarUsuario.SelectedTabColors.BorderColor = System.Drawing.Color.Black
        Me.BubbleBarUsuario.Size = New System.Drawing.Size(276, 44)
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
        Me.BBtnUsuario.Image = Global.Modelo.My.Resources.Resources.man_18
        Me.BBtnUsuario.ImageLarge = Global.Modelo.My.Resources.Resources.man_18
        Me.BBtnUsuario.Name = "BBtnUsuario"
        '
        'SupTabItemRegistro
        '
        Me.SupTabItemRegistro.AttachedControl = Me.SuperTabControlPanelRegistro
        Me.SupTabItemRegistro.GlobalItem = False
        Me.SupTabItemRegistro.Name = "SupTabItemRegistro"
        Me.SupTabItemRegistro.Text = "REPORTE"
        '
        'SuperTabControlPanelBuscador
        '
        Me.SuperTabControlPanelBuscador.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SuperTabControlPanelBuscador.Location = New System.Drawing.Point(0, 0)
        Me.SuperTabControlPanelBuscador.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.SuperTabControlPanelBuscador.Name = "SuperTabControlPanelBuscador"
        Me.SuperTabControlPanelBuscador.Size = New System.Drawing.Size(1136, 690)
        Me.SuperTabControlPanelBuscador.TabIndex = 0
        Me.SuperTabControlPanelBuscador.TabItem = Me.SupTabItemBusqueda
        '
        'SupTabItemBusqueda
        '
        Me.SupTabItemBusqueda.AttachedControl = Me.SuperTabControlPanelBuscador
        Me.SupTabItemBusqueda.GlobalItem = False
        Me.SupTabItemBusqueda.Name = "SupTabItemBusqueda"
        Me.SupTabItemBusqueda.Text = "BUSQUEDA"
        Me.SupTabItemBusqueda.Visible = False
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
        'ModeloR0
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1800, 897)
        Me.Controls.Add(Me.SuperTabPrincipal)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "ModeloR0"
        Me.Opacity = 0.05R
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "{...TITULO...}"
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
        CType(Me.SuperTabPrincipal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SuperTabPrincipal.ResumeLayout(False)
        Me.SuperTabControlPanelRegistro.ResumeLayout(False)
        Me.PanelPrincipal.ResumeLayout(False)
        Me.PanelUsuario.ResumeLayout(False)
        Me.PanelUsuario.PerformLayout()
        Me.PanelIzq.ResumeLayout(False)
        Me.PanelSuperior.ResumeLayout(False)
        Me.PanelToolBar1.ResumeLayout(False)
        Me.PanelInferior.ResumeLayout(False)
        CType(Me.MPicture, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MPanelUserAct.ResumeLayout(False)
        CType(Me.BubbleBarUsuario, System.ComponentModel.ISupportInitialize).EndInit()
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
    Protected WithEvents PanelToolBar1 As System.Windows.Forms.Panel
    Protected WithEvents btnSalir As DevComponents.DotNetBar.ButtonX
    Protected WithEvents btnGenerar As DevComponents.DotNetBar.ButtonX
    Protected WithEvents PanelPrincipal As System.Windows.Forms.Panel
    Protected WithEvents PanelUsuario As System.Windows.Forms.Panel
    Protected WithEvents lbHora As System.Windows.Forms.Label
    Protected WithEvents lbFecha As System.Windows.Forms.Label
    Protected WithEvents lbUsuario As System.Windows.Forms.Label
    Protected WithEvents lblHora As System.Windows.Forms.Label
    Protected WithEvents lblFecha As System.Windows.Forms.Label
    Protected WithEvents lblUsuario As System.Windows.Forms.Label
    Protected WithEvents MPanelUserAct As System.Windows.Forms.Panel
    Protected WithEvents MEP As System.Windows.Forms.ErrorProvider
    Protected WithEvents MHighlighterFocus As DevComponents.DotNetBar.Validator.Highlighter
    Protected WithEvents MFlyoutUsuario As DevComponents.DotNetBar.Controls.Flyout
    Friend WithEvents BBtnUsuario As DevComponents.DotNetBar.BubbleButton
    Protected WithEvents MReportViewer As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Protected WithEvents MGPFiltros As DevComponents.DotNetBar.Controls.GroupPanel
    Protected WithEvents PanelIzq As System.Windows.Forms.Panel
    Protected WithEvents MPicture As System.Windows.Forms.PictureBox
End Class
