<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F1_Zonas
    Inherits Modelo.ModeloF1

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F1_Zonas))
        Dim cbZona_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim cbProvincia_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim cbCiudad_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.PanelMapa = New System.Windows.Forms.Panel()
        Me.btLimpiar = New DevComponents.DotNetBar.ButtonX()
        Me.ButtonX4 = New DevComponents.DotNetBar.ButtonX()
        Me.ButtonX3 = New DevComponents.DotNetBar.ButtonX()
        Me.BtAdicionar = New DevComponents.DotNetBar.ButtonX()
        Me.Gmc_Cliente = New GMap.NET.WindowsForms.GMapControl()
        Me.GroupPanel3 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.tbColor = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.btZona = New DevComponents.DotNetBar.ButtonX()
        Me.cbZona = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.btProvincia = New DevComponents.DotNetBar.ButtonX()
        Me.cbProvincia = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.lbgrupo1 = New DevComponents.DotNetBar.LabelX()
        Me.btCiudad = New DevComponents.DotNetBar.ButtonX()
        Me.cbCiudad = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.tbCodigoOriginal = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.PanelZona = New System.Windows.Forms.Panel()
        Me.Zonas = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.PanelTable = New System.Windows.Forms.Panel()
        Me.grZona = New Janus.Windows.GridEX.GridEX()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.SuperTabPrincipal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuperTabPrincipal.SuspendLayout()
        Me.SuperTabControlPanelRegistro.SuspendLayout()
        Me.PanelSuperior.SuspendLayout()
        Me.PanelInferior.SuspendLayout()
        CType(Me.BubbleBarUsuario, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelToolBar1.SuspendLayout()
        Me.PanelToolBar2.SuspendLayout()
        Me.MPanelSup.SuspendLayout()
        Me.PanelPrincipal.SuspendLayout()
        Me.GroupPanelBuscador.SuspendLayout()
        CType(Me.JGrM_Buscador, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelUsuario.SuspendLayout()
        Me.PanelNavegacion.SuspendLayout()
        Me.MPanelUserAct.SuspendLayout()
        CType(Me.MEP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelMapa.SuspendLayout()
        Me.GroupPanel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.cbZona, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbProvincia, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbCiudad, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelZona.SuspendLayout()
        Me.Zonas.SuspendLayout()
        Me.PanelTable.SuspendLayout()
        CType(Me.grZona, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
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
        Me.SuperTabPrincipal.Size = New System.Drawing.Size(1362, 741)
        Me.SuperTabPrincipal.Controls.SetChildIndex(Me.SuperTabControlPanelBuscador, 0)
        Me.SuperTabPrincipal.Controls.SetChildIndex(Me.SuperTabControlPanelRegistro, 0)
        '
        'SuperTabControlPanelBuscador
        '
        Me.SuperTabControlPanelBuscador.Location = New System.Drawing.Point(0, 0)
        Me.SuperTabControlPanelBuscador.Size = New System.Drawing.Size(989, 507)
        '
        'SuperTabControlPanelRegistro
        '
        Me.SuperTabControlPanelRegistro.Size = New System.Drawing.Size(1330, 741)
        Me.SuperTabControlPanelRegistro.Controls.SetChildIndex(Me.PanelSuperior, 0)
        Me.SuperTabControlPanelRegistro.Controls.SetChildIndex(Me.PanelInferior, 0)
        Me.SuperTabControlPanelRegistro.Controls.SetChildIndex(Me.PanelPrincipal, 0)
        '
        'PanelSuperior
        '
        Me.PanelSuperior.Size = New System.Drawing.Size(1330, 72)
        Me.PanelSuperior.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelSuperior.Style.BackColor1.Color = System.Drawing.Color.DarkSlateGray
        Me.PanelSuperior.Style.BackColor2.Color = System.Drawing.Color.DarkSlateGray
        Me.PanelSuperior.Style.BackgroundImage = CType(resources.GetObject("PanelSuperior.Style.BackgroundImage"), System.Drawing.Image)
        Me.PanelSuperior.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelSuperior.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelSuperior.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelSuperior.Style.GradientAngle = 90
        '
        'PanelInferior
        '
        Me.PanelInferior.Location = New System.Drawing.Point(0, 705)
        Me.PanelInferior.Size = New System.Drawing.Size(1330, 36)
        Me.PanelInferior.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelInferior.Style.BackColor1.Color = System.Drawing.Color.DarkSlateGray
        Me.PanelInferior.Style.BackColor2.Color = System.Drawing.Color.DarkSlateGray
        Me.PanelInferior.Style.BackgroundImage = CType(resources.GetObject("PanelInferior.Style.BackgroundImage"), System.Drawing.Image)
        Me.PanelInferior.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelInferior.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelInferior.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelInferior.Style.GradientAngle = 90
        '
        'BubbleBarUsuario
        '
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
        Me.BubbleBarUsuario.MouseOverTabColors.BorderColor = System.Drawing.SystemColors.Highlight
        Me.BubbleBarUsuario.SelectedTabColors.BorderColor = System.Drawing.Color.Black
        '
        'btnSalir
        '
        '
        'btnGrabar
        '
        '
        'btnModificar
        '
        '
        'PanelToolBar2
        '
        Me.PanelToolBar2.Location = New System.Drawing.Point(1250, 0)
        '
        'MPanelSup
        '
        Me.MPanelSup.BackColor = System.Drawing.Color.Transparent
        Me.MPanelSup.Controls.Add(Me.TableLayoutPanel1)
        Me.MPanelSup.Size = New System.Drawing.Size(1330, 460)
        Me.MPanelSup.Controls.SetChildIndex(Me.PanelUsuario, 0)
        Me.MPanelSup.Controls.SetChildIndex(Me.TableLayoutPanel1, 0)
        '
        'PanelPrincipal
        '
        Me.PanelPrincipal.Size = New System.Drawing.Size(1330, 633)
        '
        'GroupPanelBuscador
        '
        Me.GroupPanelBuscador.Location = New System.Drawing.Point(0, 460)
        Me.GroupPanelBuscador.Size = New System.Drawing.Size(1330, 173)
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
        Me.GroupPanelBuscador.Style.Font = New System.Drawing.Font("Georgia", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        '
        'JGrM_Buscador
        '
        Me.JGrM_Buscador.HeaderFormatStyle.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.JGrM_Buscador.HeaderFormatStyle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.JGrM_Buscador.SelectedFormatStyle.BackColor = System.Drawing.Color.DodgerBlue
        Me.JGrM_Buscador.SelectedFormatStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.JGrM_Buscador.SelectedFormatStyle.ForeColor = System.Drawing.Color.White
        Me.JGrM_Buscador.Size = New System.Drawing.Size(1324, 150)
        '
        'btnImprimir
        '
        Me.btnImprimir.Visible = False
        '
        'MPanelUserAct
        '
        Me.MPanelUserAct.Location = New System.Drawing.Point(1130, 0)
        '
        'PanelMapa
        '
        Me.PanelMapa.BackColor = System.Drawing.Color.Transparent
        Me.PanelMapa.Controls.Add(Me.btLimpiar)
        Me.PanelMapa.Controls.Add(Me.ButtonX4)
        Me.PanelMapa.Controls.Add(Me.ButtonX3)
        Me.PanelMapa.Controls.Add(Me.BtAdicionar)
        Me.PanelMapa.Controls.Add(Me.Gmc_Cliente)
        Me.PanelMapa.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelMapa.Location = New System.Drawing.Point(402, 3)
        Me.PanelMapa.Name = "PanelMapa"
        Me.PanelMapa.Size = New System.Drawing.Size(925, 454)
        Me.PanelMapa.TabIndex = 21
        Me.PanelMapa.Visible = False
        '
        'btLimpiar
        '
        Me.btLimpiar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btLimpiar.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground
        Me.btLimpiar.Image = Global.DinoM.My.Resources.Resources.rubbish
        Me.btLimpiar.ImageFixedSize = New System.Drawing.Size(35, 35)
        Me.btLimpiar.Location = New System.Drawing.Point(7, 88)
        Me.btLimpiar.Name = "btLimpiar"
        Me.btLimpiar.Size = New System.Drawing.Size(40, 40)
        Me.btLimpiar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btLimpiar.TabIndex = 8
        Me.btLimpiar.Visible = False
        '
        'ButtonX4
        '
        Me.ButtonX4.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonX4.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground
        Me.ButtonX4.Image = Global.DinoM.My.Resources.Resources.zoomout
        Me.ButtonX4.ImageFixedSize = New System.Drawing.Size(35, 35)
        Me.ButtonX4.Location = New System.Drawing.Point(7, 46)
        Me.ButtonX4.Name = "ButtonX4"
        Me.ButtonX4.Size = New System.Drawing.Size(40, 40)
        Me.ButtonX4.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.ButtonX4.TabIndex = 7
        '
        'ButtonX3
        '
        Me.ButtonX3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonX3.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground
        Me.ButtonX3.Image = Global.DinoM.My.Resources.Resources.zoomin
        Me.ButtonX3.ImageFixedSize = New System.Drawing.Size(35, 35)
        Me.ButtonX3.Location = New System.Drawing.Point(7, 5)
        Me.ButtonX3.Name = "ButtonX3"
        Me.ButtonX3.Shape = New DevComponents.DotNetBar.RoundRectangleShapeDescriptor(2)
        Me.ButtonX3.Size = New System.Drawing.Size(40, 40)
        Me.ButtonX3.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeMobile2014
        Me.ButtonX3.TabIndex = 6
        '
        'BtAdicionar
        '
        Me.BtAdicionar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.BtAdicionar.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground
        Me.BtAdicionar.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtAdicionar.Image = Global.DinoM.My.Resources.Resources.street_map
        Me.BtAdicionar.ImageFixedSize = New System.Drawing.Size(40, 40)
        Me.BtAdicionar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top
        Me.BtAdicionar.Location = New System.Drawing.Point(52, 4)
        Me.BtAdicionar.Name = "BtAdicionar"
        Me.BtAdicionar.Shape = New DevComponents.DotNetBar.RoundRectangleShapeDescriptor(4)
        Me.BtAdicionar.Size = New System.Drawing.Size(69, 83)
        Me.BtAdicionar.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeMobile2014
        Me.BtAdicionar.SubItemsExpandWidth = 10
        Me.BtAdicionar.TabIndex = 2
        Me.BtAdicionar.Text = "Marcar Puntos"
        Me.BtAdicionar.TextColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.BtAdicionar.Visible = False
        '
        'Gmc_Cliente
        '
        Me.Gmc_Cliente.AutoScroll = True
        Me.Gmc_Cliente.AutoSize = True
        Me.Gmc_Cliente.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Gmc_Cliente.Bearing = 0!
        Me.Gmc_Cliente.CanDragMap = True
        Me.Gmc_Cliente.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Gmc_Cliente.EmptyTileColor = System.Drawing.Color.Navy
        Me.Gmc_Cliente.GrayScaleMode = False
        Me.Gmc_Cliente.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow
        Me.Gmc_Cliente.LevelsKeepInMemmory = 5
        Me.Gmc_Cliente.Location = New System.Drawing.Point(0, 0)
        Me.Gmc_Cliente.MarkersEnabled = True
        Me.Gmc_Cliente.MaxZoom = 2
        Me.Gmc_Cliente.MinZoom = 2
        Me.Gmc_Cliente.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter
        Me.Gmc_Cliente.Name = "Gmc_Cliente"
        Me.Gmc_Cliente.NegativeMode = False
        Me.Gmc_Cliente.PolygonsEnabled = True
        Me.Gmc_Cliente.RetryLoadTile = 0
        Me.Gmc_Cliente.RoutesEnabled = True
        Me.Gmc_Cliente.ScaleMode = GMap.NET.WindowsForms.ScaleModes.[Integer]
        Me.Gmc_Cliente.SelectedAreaFillColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(105, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.Gmc_Cliente.ShowTileGridLines = False
        Me.Gmc_Cliente.Size = New System.Drawing.Size(925, 454)
        Me.Gmc_Cliente.TabIndex = 0
        Me.Gmc_Cliente.Zoom = 0R
        '
        'GroupPanel3
        '
        Me.GroupPanel3.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel3.Controls.Add(Me.Panel4)
        Me.GroupPanel3.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupPanel3.Font = New System.Drawing.Font("Georgia", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupPanel3.Location = New System.Drawing.Point(0, 0)
        Me.GroupPanel3.Name = "GroupPanel3"
        Me.GroupPanel3.Size = New System.Drawing.Size(393, 171)
        '
        '
        '
        Me.GroupPanel3.Style.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.GroupPanel3.Style.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.GroupPanel3.Style.BackColorGradientAngle = 90
        Me.GroupPanel3.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel3.Style.BorderBottomWidth = 1
        Me.GroupPanel3.Style.BorderColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.GroupPanel3.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel3.Style.BorderLeftWidth = 1
        Me.GroupPanel3.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel3.Style.BorderRightWidth = 1
        Me.GroupPanel3.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel3.Style.BorderTopWidth = 1
        Me.GroupPanel3.Style.CornerDiameter = 4
        Me.GroupPanel3.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanel3.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanel3.Style.TextColor = System.Drawing.Color.White
        Me.GroupPanel3.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanel3.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanel3.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanel3.TabIndex = 1
        Me.GroupPanel3.Text = "DATOS GENERALES"
        '
        'Panel4
        '
        Me.Panel4.AutoScroll = True
        Me.Panel4.BackColor = System.Drawing.Color.White
        Me.Panel4.Controls.Add(Me.LabelX4)
        Me.Panel4.Controls.Add(Me.tbColor)
        Me.Panel4.Controls.Add(Me.LabelX3)
        Me.Panel4.Controls.Add(Me.btZona)
        Me.Panel4.Controls.Add(Me.cbZona)
        Me.Panel4.Controls.Add(Me.LabelX2)
        Me.Panel4.Controls.Add(Me.btProvincia)
        Me.Panel4.Controls.Add(Me.cbProvincia)
        Me.Panel4.Controls.Add(Me.lbgrupo1)
        Me.Panel4.Controls.Add(Me.btCiudad)
        Me.Panel4.Controls.Add(Me.cbCiudad)
        Me.Panel4.Controls.Add(Me.LabelX1)
        Me.Panel4.Controls.Add(Me.tbCodigoOriginal)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(387, 148)
        Me.Panel4.TabIndex = 0
        '
        'LabelX4
        '
        Me.LabelX4.AutoSize = True
        Me.LabelX4.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX4.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX4.Location = New System.Drawing.Point(193, 20)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX4.Size = New System.Drawing.Size(41, 16)
        Me.LabelX4.TabIndex = 237
        Me.LabelX4.Text = "Color:"
        Me.LabelX4.Visible = False
        '
        'tbColor
        '
        '
        '
        '
        Me.tbColor.Border.Class = "TextBoxBorder"
        Me.tbColor.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbColor.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbColor.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.tbColor.Location = New System.Drawing.Point(262, 16)
        Me.tbColor.Name = "tbColor"
        Me.tbColor.PreventEnterBeep = True
        Me.tbColor.Size = New System.Drawing.Size(63, 22)
        Me.tbColor.TabIndex = 236
        Me.tbColor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tbColor.Visible = False
        '
        'LabelX3
        '
        Me.LabelX3.AutoSize = True
        Me.LabelX3.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX3.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX3.Location = New System.Drawing.Point(37, 115)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX3.Size = New System.Drawing.Size(38, 16)
        Me.LabelX3.TabIndex = 234
        Me.LabelX3.Text = "Zona:"
        '
        'btZona
        '
        Me.btZona.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btZona.BackColor = System.Drawing.Color.Transparent
        Me.btZona.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat
        Me.btZona.Image = Global.DinoM.My.Resources.Resources.add
        Me.btZona.ImageFixedSize = New System.Drawing.Size(25, 23)
        Me.btZona.Location = New System.Drawing.Point(315, 111)
        Me.btZona.Name = "btZona"
        Me.btZona.Size = New System.Drawing.Size(28, 23)
        Me.btZona.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btZona.TabIndex = 235
        Me.btZona.Visible = False
        '
        'cbZona
        '
        Me.cbZona.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat
        Me.cbZona.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        cbZona_DesignTimeLayout.LayoutString = resources.GetString("cbZona_DesignTimeLayout.LayoutString")
        Me.cbZona.DesignTimeLayout = cbZona_DesignTimeLayout
        Me.cbZona.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbZona.Location = New System.Drawing.Point(106, 111)
        Me.cbZona.Name = "cbZona"
        Me.cbZona.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.cbZona.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.cbZona.SelectedIndex = -1
        Me.cbZona.SelectedItem = Nothing
        Me.cbZona.Size = New System.Drawing.Size(203, 22)
        Me.cbZona.TabIndex = 233
        Me.cbZona.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'LabelX2
        '
        Me.LabelX2.AutoSize = True
        Me.LabelX2.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX2.Location = New System.Drawing.Point(37, 84)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX2.Size = New System.Drawing.Size(66, 16)
        Me.LabelX2.TabIndex = 231
        Me.LabelX2.Text = "Provincia:"
        '
        'btProvincia
        '
        Me.btProvincia.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btProvincia.BackColor = System.Drawing.Color.Transparent
        Me.btProvincia.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat
        Me.btProvincia.Image = Global.DinoM.My.Resources.Resources.add
        Me.btProvincia.ImageFixedSize = New System.Drawing.Size(25, 23)
        Me.btProvincia.Location = New System.Drawing.Point(315, 80)
        Me.btProvincia.Name = "btProvincia"
        Me.btProvincia.Size = New System.Drawing.Size(28, 23)
        Me.btProvincia.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btProvincia.TabIndex = 232
        Me.btProvincia.Visible = False
        '
        'cbProvincia
        '
        Me.cbProvincia.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat
        Me.cbProvincia.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        cbProvincia_DesignTimeLayout.LayoutString = resources.GetString("cbProvincia_DesignTimeLayout.LayoutString")
        Me.cbProvincia.DesignTimeLayout = cbProvincia_DesignTimeLayout
        Me.cbProvincia.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbProvincia.Location = New System.Drawing.Point(106, 80)
        Me.cbProvincia.Name = "cbProvincia"
        Me.cbProvincia.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.cbProvincia.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.cbProvincia.SelectedIndex = -1
        Me.cbProvincia.SelectedItem = Nothing
        Me.cbProvincia.Size = New System.Drawing.Size(203, 22)
        Me.cbProvincia.TabIndex = 230
        Me.cbProvincia.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'lbgrupo1
        '
        Me.lbgrupo1.AutoSize = True
        Me.lbgrupo1.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.lbgrupo1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lbgrupo1.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbgrupo1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.lbgrupo1.Location = New System.Drawing.Point(37, 53)
        Me.lbgrupo1.Name = "lbgrupo1"
        Me.lbgrupo1.SingleLineColor = System.Drawing.SystemColors.Control
        Me.lbgrupo1.Size = New System.Drawing.Size(51, 16)
        Me.lbgrupo1.TabIndex = 228
        Me.lbgrupo1.Text = "Ciudad:"
        '
        'btCiudad
        '
        Me.btCiudad.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btCiudad.BackColor = System.Drawing.Color.Transparent
        Me.btCiudad.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat
        Me.btCiudad.Image = Global.DinoM.My.Resources.Resources.add
        Me.btCiudad.ImageFixedSize = New System.Drawing.Size(25, 23)
        Me.btCiudad.Location = New System.Drawing.Point(315, 49)
        Me.btCiudad.Name = "btCiudad"
        Me.btCiudad.Size = New System.Drawing.Size(28, 23)
        Me.btCiudad.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btCiudad.TabIndex = 229
        Me.btCiudad.Visible = False
        '
        'cbCiudad
        '
        Me.cbCiudad.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat
        Me.cbCiudad.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        cbCiudad_DesignTimeLayout.LayoutString = resources.GetString("cbCiudad_DesignTimeLayout.LayoutString")
        Me.cbCiudad.DesignTimeLayout = cbCiudad_DesignTimeLayout
        Me.cbCiudad.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbCiudad.Location = New System.Drawing.Point(106, 49)
        Me.cbCiudad.Name = "cbCiudad"
        Me.cbCiudad.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.cbCiudad.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.cbCiudad.SelectedIndex = -1
        Me.cbCiudad.SelectedItem = Nothing
        Me.cbCiudad.Size = New System.Drawing.Size(203, 22)
        Me.cbCiudad.TabIndex = 227
        Me.cbCiudad.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'LabelX1
        '
        Me.LabelX1.AutoSize = True
        Me.LabelX1.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX1.Location = New System.Drawing.Point(37, 20)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX1.Size = New System.Drawing.Size(50, 16)
        Me.LabelX1.TabIndex = 226
        Me.LabelX1.Text = "Código:"
        '
        'tbCodigoOriginal
        '
        '
        '
        '
        Me.tbCodigoOriginal.Border.Class = "TextBoxBorder"
        Me.tbCodigoOriginal.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbCodigoOriginal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbCodigoOriginal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.tbCodigoOriginal.Location = New System.Drawing.Point(106, 16)
        Me.tbCodigoOriginal.Name = "tbCodigoOriginal"
        Me.tbCodigoOriginal.PreventEnterBeep = True
        Me.tbCodigoOriginal.Size = New System.Drawing.Size(63, 22)
        Me.tbCodigoOriginal.TabIndex = 225
        Me.tbCodigoOriginal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'PanelZona
        '
        Me.PanelZona.Controls.Add(Me.Zonas)
        Me.PanelZona.Controls.Add(Me.GroupPanel3)
        Me.PanelZona.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelZona.Location = New System.Drawing.Point(3, 3)
        Me.PanelZona.Name = "PanelZona"
        Me.PanelZona.Size = New System.Drawing.Size(393, 454)
        Me.PanelZona.TabIndex = 20
        '
        'Zonas
        '
        Me.Zonas.CanvasColor = System.Drawing.SystemColors.Control
        Me.Zonas.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.Zonas.Controls.Add(Me.PanelTable)
        Me.Zonas.DisabledBackColor = System.Drawing.Color.Empty
        Me.Zonas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Zonas.Font = New System.Drawing.Font("Georgia", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Zonas.Location = New System.Drawing.Point(0, 171)
        Me.Zonas.Name = "Zonas"
        Me.Zonas.Size = New System.Drawing.Size(393, 283)
        '
        '
        '
        Me.Zonas.Style.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.Zonas.Style.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.Zonas.Style.BackColorGradientAngle = 90
        Me.Zonas.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.Zonas.Style.BorderBottomWidth = 1
        Me.Zonas.Style.BorderColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.Zonas.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.Zonas.Style.BorderLeftWidth = 1
        Me.Zonas.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.Zonas.Style.BorderRightWidth = 1
        Me.Zonas.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.Zonas.Style.BorderTopWidth = 1
        Me.Zonas.Style.CornerDiameter = 4
        Me.Zonas.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.Zonas.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.Zonas.Style.TextColor = System.Drawing.Color.White
        Me.Zonas.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.Zonas.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.Zonas.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.Zonas.TabIndex = 2
        Me.Zonas.Text = "ZONAS"
        '
        'PanelTable
        '
        Me.PanelTable.AutoScroll = True
        Me.PanelTable.BackColor = System.Drawing.Color.White
        Me.PanelTable.Controls.Add(Me.grZona)
        Me.PanelTable.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelTable.Location = New System.Drawing.Point(0, 0)
        Me.PanelTable.Name = "PanelTable"
        Me.PanelTable.Size = New System.Drawing.Size(387, 260)
        Me.PanelTable.TabIndex = 0
        '
        'grZona
        '
        Me.grZona.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grZona.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grZona.FlatBorderColor = System.Drawing.Color.DodgerBlue
        Me.grZona.FocusCellDisplayMode = Janus.Windows.GridEX.FocusCellDisplayMode.UseSelectedFormatStyle
        Me.grZona.FocusStyle = Janus.Windows.GridEX.FocusStyle.Solid
        Me.grZona.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grZona.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid
        Me.grZona.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.UseRowStyle
        Me.grZona.HeaderFormatStyle.Font = New System.Drawing.Font("Georgia", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grZona.HeaderFormatStyle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.grZona.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight
        Me.grZona.Location = New System.Drawing.Point(0, 0)
        Me.grZona.Name = "grZona"
        Me.grZona.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.grZona.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.grZona.SelectedFormatStyle.BackColor = System.Drawing.Color.Gold
        Me.grZona.Size = New System.Drawing.Size(387, 260)
        Me.grZona.TabIndex = 0
        Me.grZona.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.PanelZona, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.PanelMapa, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1330, 460)
        Me.TableLayoutPanel1.TabIndex = 22
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 500
        '
        'F1_Zonas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1362, 741)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "F1_Zonas"
        Me.Text = "F1_Zonas"
        Me.Controls.SetChildIndex(Me.SuperTabPrincipal, 0)
        CType(Me.SuperTabPrincipal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SuperTabPrincipal.ResumeLayout(False)
        Me.SuperTabControlPanelRegistro.ResumeLayout(False)
        Me.PanelSuperior.ResumeLayout(False)
        Me.PanelInferior.ResumeLayout(False)
        CType(Me.BubbleBarUsuario, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelToolBar1.ResumeLayout(False)
        Me.PanelToolBar2.ResumeLayout(False)
        Me.MPanelSup.ResumeLayout(False)
        Me.PanelPrincipal.ResumeLayout(False)
        Me.GroupPanelBuscador.ResumeLayout(False)
        CType(Me.JGrM_Buscador, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelUsuario.ResumeLayout(False)
        Me.PanelUsuario.PerformLayout()
        Me.PanelNavegacion.ResumeLayout(False)
        Me.MPanelUserAct.ResumeLayout(False)
        Me.MPanelUserAct.PerformLayout()
        CType(Me.MEP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelMapa.ResumeLayout(False)
        Me.PanelMapa.PerformLayout()
        Me.GroupPanel3.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.cbZona, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbProvincia, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbCiudad, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelZona.ResumeLayout(False)
        Me.Zonas.ResumeLayout(False)
        Me.PanelTable.ResumeLayout(False)
        CType(Me.grZona, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelMapa As System.Windows.Forms.Panel
    Friend WithEvents Gmc_Cliente As GMap.NET.WindowsForms.GMapControl
    Friend WithEvents GroupPanel3 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbCodigoOriginal As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents btZona As DevComponents.DotNetBar.ButtonX
    Friend WithEvents cbZona As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents btProvincia As DevComponents.DotNetBar.ButtonX
    Friend WithEvents cbProvincia As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents lbgrupo1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents btCiudad As DevComponents.DotNetBar.ButtonX
    Friend WithEvents cbCiudad As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents PanelZona As System.Windows.Forms.Panel
    Friend WithEvents Zonas As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents PanelTable As System.Windows.Forms.Panel
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbColor As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents grZona As Janus.Windows.GridEX.GridEX
    Friend WithEvents BtAdicionar As DevComponents.DotNetBar.ButtonX
    Friend WithEvents ButtonX4 As DevComponents.DotNetBar.ButtonX
    Friend WithEvents ButtonX3 As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btLimpiar As DevComponents.DotNetBar.ButtonX
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
    Friend WithEvents Timer1 As Timer
End Class
