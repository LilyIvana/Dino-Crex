<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class F0_MCompras
    Inherits Modelo.ModeloF0

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F0_MCompras))
        Dim cbSucursal_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.PanelDetalle = New System.Windows.Forms.Panel()
        Me.GroupPanel4 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.grdetalle = New Janus.Windows.GridEX.GridEX()
        Me.GPanelProductos = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.grProductos = New Janus.Windows.GridEX.GridEX()
        Me.PanelTotal = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.tbIce = New DevComponents.Editors.DoubleInput()
        Me.LabelX17 = New DevComponents.DotNetBar.LabelX()
        Me.tbDescPro = New DevComponents.Editors.DoubleInput()
        Me.LabelX15 = New DevComponents.DotNetBar.LabelX()
        Me.tbSubtotalC = New DevComponents.Editors.DoubleInput()
        Me.LabelX8 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX10 = New DevComponents.DotNetBar.LabelX()
        Me.tbPdesc = New DevComponents.Editors.DoubleInput()
        Me.tbMdesc = New DevComponents.Editors.DoubleInput()
        Me.tbtotal = New DevComponents.Editors.DoubleInput()
        Me.LabelX11 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX9 = New DevComponents.DotNetBar.LabelX()
        Me.PanelContentSup = New System.Windows.Forms.Panel()
        Me.GroupPanel1 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lbTipoCambio = New DevComponents.DotNetBar.LabelX()
        Me.tbTipoCambio = New DevComponents.Editors.DoubleInput()
        Me.LabelX14 = New DevComponents.DotNetBar.LabelX()
        Me.swMoneda = New DevComponents.DotNetBar.Controls.SwitchButton()
        Me.swRetencion = New DevComponents.DotNetBar.Controls.SwitchButton()
        Me.LabelX13 = New DevComponents.DotNetBar.LabelX()
        Me.swConsigna = New DevComponents.DotNetBar.Controls.SwitchButton()
        Me.LabelX12 = New DevComponents.DotNetBar.LabelX()
        Me.swEmision = New DevComponents.DotNetBar.Controls.SwitchButton()
        Me.LabelX16 = New DevComponents.DotNetBar.LabelX()
        Me.GroupPanelFactura2 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.lbNFactura = New DevComponents.DotNetBar.LabelX()
        Me.tbSACF = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.tbNFactura = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.lbSACF = New DevComponents.DotNetBar.LabelX()
        Me.lbNAutoriz = New DevComponents.DotNetBar.LabelX()
        Me.tbNDui = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.tbNAutorizacion = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.lbNDui = New DevComponents.DotNetBar.LabelX()
        Me.lbCodCtrl = New DevComponents.DotNetBar.LabelX()
        Me.tbCodControl = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.tbNitProv = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX6 = New DevComponents.DotNetBar.LabelX()
        Me.lbCtrlEnter = New DevComponents.DotNetBar.LabelX()
        Me.tbCodProv = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.cbSucursal = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.LabelX7 = New DevComponents.DotNetBar.LabelX()
        Me.lbCredito = New DevComponents.DotNetBar.LabelX()
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.tbFechaVenc = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.swTipoVenta = New DevComponents.DotNetBar.Controls.SwitchButton()
        Me.tbFechaVenta = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.tbObservacion = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.tbProveedor = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.tbCodigo = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.BUSCADOR = New DevComponents.DotNetBar.SuperTabItem()
        Me.SuperTabControlPanel2 = New DevComponents.DotNetBar.SuperTabControlPanel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.PanelVentas = New System.Windows.Forms.Panel()
        Me.GroupPanel2 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.grCompra = New Janus.Windows.GridEX.GridEX()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.cmOpcionesDetalle = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.INSERTARFILAToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PanelSuperior.SuspendLayout()
        Me.PanelInferior.SuspendLayout()
        CType(Me.BubbleBarUsuario, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelToolBar1.SuspendLayout()
        Me.PanelToolBar2.SuspendLayout()
        Me.PanelPrincipal.SuspendLayout()
        Me.PanelUsuario.SuspendLayout()
        Me.PanelNavegacion.SuspendLayout()
        Me.MPanelUserAct.SuspendLayout()
        CType(Me.MEP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelContent.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.MSuperTabControlPanel1.SuspendLayout()
        CType(Me.MSuperTabControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MSuperTabControl.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelDetalle.SuspendLayout()
        Me.GroupPanel4.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.grdetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GPanelProductos.SuspendLayout()
        Me.Panel7.SuspendLayout()
        CType(Me.grProductos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelTotal.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.tbIce, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbDescPro, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbSubtotalC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbPdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbMdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbtotal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelContentSup.SuspendLayout()
        Me.GroupPanel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.tbTipoCambio, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupPanelFactura2.SuspendLayout()
        CType(Me.cbSucursal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbFechaVenc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbFechaVenta, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuperTabControlPanel2.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.PanelVentas.SuspendLayout()
        Me.GroupPanel2.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.grCompra, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmOpcionesDetalle.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelSuperior
        '
        Me.PanelSuperior.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelSuperior.Style.BackColor1.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.PanelSuperior.Style.BackColor2.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.PanelSuperior.Style.BackgroundImage = CType(resources.GetObject("PanelSuperior.Style.BackgroundImage"), System.Drawing.Image)
        Me.PanelSuperior.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelSuperior.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelSuperior.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelSuperior.Style.GradientAngle = 90
        Me.PanelSuperior.StyleMouseDown.BackColor1.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.PanelSuperior.StyleMouseDown.BackColor2.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.PanelSuperior.StyleMouseOver.BackColor1.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.PanelSuperior.StyleMouseOver.BackColor2.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.PanelSuperior.StyleMouseOver.BackgroundImage = CType(resources.GetObject("PanelSuperior.StyleMouseOver.BackgroundImage"), System.Drawing.Image)
        '
        'PanelInferior
        '
        Me.PanelInferior.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelInferior.Style.BackColor1.Color = System.Drawing.Color.Transparent
        Me.PanelInferior.Style.BackColor2.Color = System.Drawing.Color.Transparent
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
        'TxtNombreUsu
        '
        Me.TxtNombreUsu.ReadOnly = True
        Me.TxtNombreUsu.Text = "DEFAULT"
        '
        'btnSalir
        '
        Me.btnSalir.TabIndex = 1
        '
        'btnGrabar
        '
        Me.btnGrabar.TabIndex = 0
        '
        'btnEliminar
        '
        Me.btnEliminar.TabIndex = 4
        '
        'btnModificar
        '
        Me.btnModificar.TabIndex = 3
        '
        'btnNuevo
        '
        Me.btnNuevo.TabIndex = 2
        Me.PanelPrincipal.Controls.SetChildIndex(Me.PanelInferior, 0)
        Me.PanelPrincipal.Controls.SetChildIndex(Me.PanelUsuario, 0)
        Me.PanelPrincipal.Controls.SetChildIndex(Me.PanelSuperior, 0)
        Me.PanelPrincipal.Controls.SetChildIndex(Me.Panel1, 0)
        '
        'btnImprimir
        '
        '
        'btnUltimo
        '
        '
        'btnSiguiente
        '
        '
        'btnAnterior
        '
        '
        'btnPrimero
        '
        '
        'MRlAccion
        '
        '
        '
        '
        Me.MRlAccion.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        'PanelContent
        '
        Me.PanelContent.Controls.Add(Me.PanelDetalle)
        Me.PanelContent.Controls.Add(Me.PanelTotal)
        Me.PanelContent.Controls.Add(Me.PanelContentSup)
        '
        'MSuperTabControl
        '
        '
        '
        '
        '
        '
        '
        Me.MSuperTabControl.ControlBox.CloseBox.Name = ""
        '
        '
        '
        Me.MSuperTabControl.ControlBox.MenuBox.Name = ""
        Me.MSuperTabControl.ControlBox.Name = ""
        Me.MSuperTabControl.ControlBox.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.MSuperTabControl.ControlBox.MenuBox, Me.MSuperTabControl.ControlBox.CloseBox})
        Me.MSuperTabControl.Controls.Add(Me.SuperTabControlPanel2)
        Me.MSuperTabControl.SelectedTabIndex = 1
        Me.MSuperTabControl.Tabs.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.BUSCADOR})
        Me.MSuperTabControl.Controls.SetChildIndex(Me.MSuperTabControlPanel1, 0)
        Me.MSuperTabControl.Controls.SetChildIndex(Me.SuperTabControlPanel2, 0)
        '
        'PanelDetalle
        '
        Me.PanelDetalle.BackColor = System.Drawing.Color.White
        Me.PanelDetalle.Controls.Add(Me.GroupPanel4)
        Me.PanelDetalle.Controls.Add(Me.GPanelProductos)
        Me.PanelDetalle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelDetalle.Location = New System.Drawing.Point(0, 230)
        Me.PanelDetalle.Name = "PanelDetalle"
        Me.PanelDetalle.Size = New System.Drawing.Size(1317, 337)
        Me.PanelDetalle.TabIndex = 3
        '
        'GroupPanel4
        '
        Me.GroupPanel4.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel4.Controls.Add(Me.Panel5)
        Me.GroupPanel4.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupPanel4.Font = New System.Drawing.Font("Georgia", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupPanel4.Location = New System.Drawing.Point(0, 0)
        Me.GroupPanel4.Name = "GroupPanel4"
        Me.GroupPanel4.Size = New System.Drawing.Size(1317, 186)
        '
        '
        '
        Me.GroupPanel4.Style.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.GroupPanel4.Style.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.GroupPanel4.Style.BackColorGradientAngle = 90
        Me.GroupPanel4.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel4.Style.BorderBottomWidth = 1
        Me.GroupPanel4.Style.BorderColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.GroupPanel4.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel4.Style.BorderLeftWidth = 1
        Me.GroupPanel4.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel4.Style.BorderRightWidth = 1
        Me.GroupPanel4.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel4.Style.BorderTopWidth = 1
        Me.GroupPanel4.Style.CornerDiameter = 4
        Me.GroupPanel4.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanel4.Style.Font = New System.Drawing.Font("Georgia", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupPanel4.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanel4.Style.TextColor = System.Drawing.Color.White
        Me.GroupPanel4.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanel4.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanel4.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanel4.TabIndex = 3
        Me.GroupPanel4.Text = "DETALLE DE LA COMPRA"
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.White
        Me.Panel5.Controls.Add(Me.grdetalle)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(0, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1311, 163)
        Me.Panel5.TabIndex = 0
        '
        'grdetalle
        '
        Me.grdetalle.BackColor = System.Drawing.Color.GhostWhite
        Me.grdetalle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdetalle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdetalle.HeaderFormatStyle.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdetalle.Location = New System.Drawing.Point(0, 0)
        Me.grdetalle.Name = "grdetalle"
        Me.grdetalle.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.grdetalle.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.grdetalle.Size = New System.Drawing.Size(1311, 163)
        Me.grdetalle.TabIndex = 3
        Me.grdetalle.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'GPanelProductos
        '
        Me.GPanelProductos.CanvasColor = System.Drawing.SystemColors.Control
        Me.GPanelProductos.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GPanelProductos.Controls.Add(Me.Panel7)
        Me.GPanelProductos.DisabledBackColor = System.Drawing.Color.Empty
        Me.GPanelProductos.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GPanelProductos.Font = New System.Drawing.Font("Georgia", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GPanelProductos.Location = New System.Drawing.Point(0, 186)
        Me.GPanelProductos.Name = "GPanelProductos"
        Me.GPanelProductos.Size = New System.Drawing.Size(1317, 151)
        '
        '
        '
        Me.GPanelProductos.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.GPanelProductos.Style.BackColorGradientAngle = 90
        Me.GPanelProductos.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.GPanelProductos.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GPanelProductos.Style.BorderBottomWidth = 1
        Me.GPanelProductos.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.GPanelProductos.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GPanelProductos.Style.BorderLeftWidth = 1
        Me.GPanelProductos.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GPanelProductos.Style.BorderRightWidth = 1
        Me.GPanelProductos.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GPanelProductos.Style.BorderTopWidth = 1
        Me.GPanelProductos.Style.CornerDiameter = 4
        Me.GPanelProductos.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GPanelProductos.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GPanelProductos.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.GPanelProductos.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GPanelProductos.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GPanelProductos.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GPanelProductos.TabIndex = 3
        Me.GPanelProductos.Text = "PRODUCTOS"
        Me.GPanelProductos.Visible = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.White
        Me.Panel7.Controls.Add(Me.grProductos)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel7.Location = New System.Drawing.Point(0, 0)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(1311, 128)
        Me.Panel7.TabIndex = 0
        '
        'grProductos
        '
        Me.grProductos.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grProductos.BackColor = System.Drawing.Color.GhostWhite
        Me.grProductos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grProductos.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grProductos.HeaderFormatStyle.Font = New System.Drawing.Font("Georgia", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grProductos.HeaderFormatStyle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.grProductos.Location = New System.Drawing.Point(0, 0)
        Me.grProductos.Name = "grProductos"
        Me.grProductos.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.grProductos.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.grProductos.Size = New System.Drawing.Size(1311, 128)
        Me.grProductos.TabIndex = 0
        Me.grProductos.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'PanelTotal
        '
        Me.PanelTotal.AutoScroll = True
        Me.PanelTotal.BackColor = System.Drawing.Color.SlateGray
        Me.PanelTotal.BackgroundImage = Global.DinoM.My.Resources.Resources.fondo1
        Me.PanelTotal.Controls.Add(Me.Panel4)
        Me.PanelTotal.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelTotal.Location = New System.Drawing.Point(0, 567)
        Me.PanelTotal.Name = "PanelTotal"
        Me.PanelTotal.Size = New System.Drawing.Size(1317, 51)
        Me.PanelTotal.TabIndex = 3
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Transparent
        Me.Panel4.Controls.Add(Me.tbIce)
        Me.Panel4.Controls.Add(Me.LabelX17)
        Me.Panel4.Controls.Add(Me.tbDescPro)
        Me.Panel4.Controls.Add(Me.LabelX15)
        Me.Panel4.Controls.Add(Me.tbSubtotalC)
        Me.Panel4.Controls.Add(Me.LabelX8)
        Me.Panel4.Controls.Add(Me.LabelX10)
        Me.Panel4.Controls.Add(Me.tbPdesc)
        Me.Panel4.Controls.Add(Me.tbMdesc)
        Me.Panel4.Controls.Add(Me.tbtotal)
        Me.Panel4.Controls.Add(Me.LabelX11)
        Me.Panel4.Controls.Add(Me.LabelX9)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel4.Location = New System.Drawing.Point(12, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1305, 51)
        Me.Panel4.TabIndex = 39
        '
        'tbIce
        '
        '
        '
        '
        Me.tbIce.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbIce.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbIce.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.tbIce.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbIce.Increment = 1.0R
        Me.tbIce.Location = New System.Drawing.Point(698, 18)
        Me.tbIce.MinValue = 0R
        Me.tbIce.Name = "tbIce"
        Me.tbIce.Size = New System.Drawing.Size(89, 21)
        Me.tbIce.TabIndex = 43
        Me.tbIce.WatermarkAlignment = DevComponents.Editors.eTextAlignment.Right
        '
        'LabelX17
        '
        Me.LabelX17.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX17.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX17.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX17.ForeColor = System.Drawing.Color.White
        Me.LabelX17.Location = New System.Drawing.Point(654, 19)
        Me.LabelX17.Name = "LabelX17"
        Me.LabelX17.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX17.Size = New System.Drawing.Size(31, 18)
        Me.LabelX17.TabIndex = 44
        Me.LabelX17.Text = "ICE:"
        '
        'tbDescPro
        '
        '
        '
        '
        Me.tbDescPro.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbDescPro.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbDescPro.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.tbDescPro.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbDescPro.Increment = 1.0R
        Me.tbDescPro.Location = New System.Drawing.Point(537, 18)
        Me.tbDescPro.MinValue = 0R
        Me.tbDescPro.Name = "tbDescPro"
        Me.tbDescPro.Size = New System.Drawing.Size(89, 21)
        Me.tbDescPro.TabIndex = 41
        Me.tbDescPro.WatermarkAlignment = DevComponents.Editors.eTextAlignment.Right
        '
        'LabelX15
        '
        Me.LabelX15.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX15.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX15.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX15.ForeColor = System.Drawing.Color.White
        Me.LabelX15.Location = New System.Drawing.Point(425, 19)
        Me.LabelX15.Name = "LabelX15"
        Me.LabelX15.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX15.Size = New System.Drawing.Size(106, 18)
        Me.LabelX15.TabIndex = 42
        Me.LabelX15.Text = "Descuento Pro.:"
        '
        'tbSubtotalC
        '
        '
        '
        '
        Me.tbSubtotalC.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbSubtotalC.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbSubtotalC.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.tbSubtotalC.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbSubtotalC.Increment = 1.0R
        Me.tbSubtotalC.Location = New System.Drawing.Point(95, 18)
        Me.tbSubtotalC.MinValue = 0R
        Me.tbSubtotalC.Name = "tbSubtotalC"
        Me.tbSubtotalC.Size = New System.Drawing.Size(89, 21)
        Me.tbSubtotalC.TabIndex = 39
        Me.tbSubtotalC.WatermarkAlignment = DevComponents.Editors.eTextAlignment.Right
        '
        'LabelX8
        '
        Me.LabelX8.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX8.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX8.ForeColor = System.Drawing.Color.White
        Me.LabelX8.Location = New System.Drawing.Point(1079, 14)
        Me.LabelX8.Name = "LabelX8"
        Me.LabelX8.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX8.Size = New System.Drawing.Size(86, 18)
        Me.LabelX8.TabIndex = 34
        Me.LabelX8.Text = "P. Descuento:"
        Me.LabelX8.Visible = False
        '
        'LabelX10
        '
        Me.LabelX10.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX10.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX10.ForeColor = System.Drawing.Color.White
        Me.LabelX10.Location = New System.Drawing.Point(18, 19)
        Me.LabelX10.Name = "LabelX10"
        Me.LabelX10.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX10.Size = New System.Drawing.Size(71, 18)
        Me.LabelX10.TabIndex = 40
        Me.LabelX10.Text = "Subtotal:"
        '
        'tbPdesc
        '
        '
        '
        '
        Me.tbPdesc.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbPdesc.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbPdesc.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.tbPdesc.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPdesc.Increment = 1.0R
        Me.tbPdesc.Location = New System.Drawing.Point(1171, 14)
        Me.tbPdesc.LockUpdateChecked = False
        Me.tbPdesc.MinValue = 0R
        Me.tbPdesc.Name = "tbPdesc"
        Me.tbPdesc.Size = New System.Drawing.Size(89, 21)
        Me.tbPdesc.TabIndex = 33
        Me.tbPdesc.Visible = False
        Me.tbPdesc.WatermarkAlignment = DevComponents.Editors.eTextAlignment.Right
        '
        'tbMdesc
        '
        '
        '
        '
        Me.tbMdesc.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbMdesc.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbMdesc.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.tbMdesc.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbMdesc.Increment = 1.0R
        Me.tbMdesc.Location = New System.Drawing.Point(308, 19)
        Me.tbMdesc.MinValue = 0R
        Me.tbMdesc.Name = "tbMdesc"
        Me.tbMdesc.Size = New System.Drawing.Size(89, 21)
        Me.tbMdesc.TabIndex = 35
        Me.tbMdesc.WatermarkAlignment = DevComponents.Editors.eTextAlignment.Right
        '
        'tbtotal
        '
        '
        '
        '
        Me.tbtotal.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbtotal.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbtotal.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.tbtotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbtotal.Increment = 1.0R
        Me.tbtotal.Location = New System.Drawing.Point(875, 19)
        Me.tbtotal.MinValue = 0R
        Me.tbtotal.Name = "tbtotal"
        Me.tbtotal.Size = New System.Drawing.Size(89, 21)
        Me.tbtotal.TabIndex = 37
        Me.tbtotal.WatermarkAlignment = DevComponents.Editors.eTextAlignment.Right
        '
        'LabelX11
        '
        Me.LabelX11.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX11.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX11.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX11.ForeColor = System.Drawing.Color.White
        Me.LabelX11.Location = New System.Drawing.Point(819, 21)
        Me.LabelX11.Name = "LabelX11"
        Me.LabelX11.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX11.Size = New System.Drawing.Size(44, 18)
        Me.LabelX11.TabIndex = 38
        Me.LabelX11.Text = "Total:"
        '
        'LabelX9
        '
        Me.LabelX9.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX9.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX9.ForeColor = System.Drawing.Color.White
        Me.LabelX9.Location = New System.Drawing.Point(204, 19)
        Me.LabelX9.Name = "LabelX9"
        Me.LabelX9.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX9.Size = New System.Drawing.Size(98, 18)
        Me.LabelX9.TabIndex = 36
        Me.LabelX9.Text = "Descuento Un.:"
        '
        'PanelContentSup
        '
        Me.PanelContentSup.BackColor = System.Drawing.Color.White
        Me.PanelContentSup.Controls.Add(Me.GroupPanel1)
        Me.PanelContentSup.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelContentSup.Location = New System.Drawing.Point(0, 0)
        Me.PanelContentSup.Name = "PanelContentSup"
        Me.PanelContentSup.Size = New System.Drawing.Size(1317, 230)
        Me.PanelContentSup.TabIndex = 4
        '
        'GroupPanel1
        '
        Me.GroupPanel1.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel1.Controls.Add(Me.Panel2)
        Me.GroupPanel1.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupPanel1.Font = New System.Drawing.Font("Georgia", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupPanel1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.GroupPanel1.Location = New System.Drawing.Point(0, 0)
        Me.GroupPanel1.Name = "GroupPanel1"
        Me.GroupPanel1.Size = New System.Drawing.Size(1317, 230)
        '
        '
        '
        Me.GroupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.MenuBackground
        Me.GroupPanel1.Style.BackColorGradientAngle = 90
        Me.GroupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.GroupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderBottomWidth = 1
        Me.GroupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.GroupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderLeftWidth = 1
        Me.GroupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderRightWidth = 1
        Me.GroupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderTopWidth = 1
        Me.GroupPanel1.Style.CornerDiameter = 4
        Me.GroupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.GroupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanel1.TabIndex = 228
        Me.GroupPanel1.Text = "DATOS GENERALES"
        '
        'Panel2
        '
        Me.Panel2.AutoScroll = True
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.Controls.Add(Me.lbTipoCambio)
        Me.Panel2.Controls.Add(Me.tbTipoCambio)
        Me.Panel2.Controls.Add(Me.LabelX14)
        Me.Panel2.Controls.Add(Me.swMoneda)
        Me.Panel2.Controls.Add(Me.swRetencion)
        Me.Panel2.Controls.Add(Me.LabelX13)
        Me.Panel2.Controls.Add(Me.swConsigna)
        Me.Panel2.Controls.Add(Me.LabelX12)
        Me.Panel2.Controls.Add(Me.swEmision)
        Me.Panel2.Controls.Add(Me.LabelX16)
        Me.Panel2.Controls.Add(Me.GroupPanelFactura2)
        Me.Panel2.Controls.Add(Me.tbNitProv)
        Me.Panel2.Controls.Add(Me.LabelX6)
        Me.Panel2.Controls.Add(Me.lbCtrlEnter)
        Me.Panel2.Controls.Add(Me.tbCodProv)
        Me.Panel2.Controls.Add(Me.cbSucursal)
        Me.Panel2.Controls.Add(Me.LabelX7)
        Me.Panel2.Controls.Add(Me.lbCredito)
        Me.Panel2.Controls.Add(Me.LabelX5)
        Me.Panel2.Controls.Add(Me.LabelX4)
        Me.Panel2.Controls.Add(Me.tbFechaVenc)
        Me.Panel2.Controls.Add(Me.swTipoVenta)
        Me.Panel2.Controls.Add(Me.tbFechaVenta)
        Me.Panel2.Controls.Add(Me.LabelX1)
        Me.Panel2.Controls.Add(Me.tbObservacion)
        Me.Panel2.Controls.Add(Me.LabelX3)
        Me.Panel2.Controls.Add(Me.tbProveedor)
        Me.Panel2.Controls.Add(Me.LabelX2)
        Me.Panel2.Controls.Add(Me.tbCodigo)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1311, 207)
        Me.Panel2.TabIndex = 234
        '
        'lbTipoCambio
        '
        Me.lbTipoCambio.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.lbTipoCambio.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lbTipoCambio.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTipoCambio.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.lbTipoCambio.Location = New System.Drawing.Point(415, 83)
        Me.lbTipoCambio.Name = "lbTipoCambio"
        Me.lbTipoCambio.SingleLineColor = System.Drawing.SystemColors.Control
        Me.lbTipoCambio.Size = New System.Drawing.Size(105, 23)
        Me.lbTipoCambio.TabIndex = 675
        Me.lbTipoCambio.Text = "Tipo de Cambio:"
        '
        'tbTipoCambio
        '
        '
        '
        '
        Me.tbTipoCambio.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbTipoCambio.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbTipoCambio.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.tbTipoCambio.Increment = 1.0R
        Me.tbTipoCambio.Location = New System.Drawing.Point(592, 83)
        Me.tbTipoCambio.Name = "tbTipoCambio"
        Me.tbTipoCambio.Size = New System.Drawing.Size(132, 22)
        Me.tbTipoCambio.TabIndex = 674
        '
        'LabelX14
        '
        Me.LabelX14.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX14.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX14.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX14.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX14.Location = New System.Drawing.Point(415, 57)
        Me.LabelX14.Name = "LabelX14"
        Me.LabelX14.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX14.Size = New System.Drawing.Size(90, 23)
        Me.LabelX14.TabIndex = 673
        Me.LabelX14.Text = "Tipo Moneda:"
        '
        'swMoneda
        '
        '
        '
        '
        Me.swMoneda.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.swMoneda.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.swMoneda.Location = New System.Drawing.Point(592, 57)
        Me.swMoneda.Name = "swMoneda"
        Me.swMoneda.OffBackColor = System.Drawing.Color.LawnGreen
        Me.swMoneda.OffText = "DOLAR"
        Me.swMoneda.OnBackColor = System.Drawing.Color.Gold
        Me.swMoneda.OnText = "BOLIVIANO"
        Me.swMoneda.Size = New System.Drawing.Size(120, 22)
        Me.swMoneda.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.swMoneda.TabIndex = 672
        Me.swMoneda.Value = True
        Me.swMoneda.ValueObject = "Y"
        '
        'swRetencion
        '
        '
        '
        '
        Me.swRetencion.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.swRetencion.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.swRetencion.Location = New System.Drawing.Point(592, 166)
        Me.swRetencion.Name = "swRetencion"
        Me.swRetencion.OffBackColor = System.Drawing.Color.RoyalBlue
        Me.swRetencion.OffText = "NO"
        Me.swRetencion.OnBackColor = System.Drawing.Color.LightSkyBlue
        Me.swRetencion.OnText = "SI"
        Me.swRetencion.Size = New System.Drawing.Size(65, 22)
        Me.swRetencion.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.swRetencion.TabIndex = 371
        Me.swRetencion.Visible = False
        '
        'LabelX13
        '
        '
        '
        '
        Me.LabelX13.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX13.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX13.Location = New System.Drawing.Point(415, 166)
        Me.LabelX13.Name = "LabelX13"
        Me.LabelX13.Size = New System.Drawing.Size(70, 23)
        Me.LabelX13.TabIndex = 372
        Me.LabelX13.Text = "Retención:"
        Me.LabelX13.Visible = False
        '
        'swConsigna
        '
        '
        '
        '
        Me.swConsigna.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.swConsigna.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.swConsigna.Location = New System.Drawing.Point(592, 139)
        Me.swConsigna.Name = "swConsigna"
        Me.swConsigna.OffBackColor = System.Drawing.Color.Teal
        Me.swConsigna.OffText = "NO"
        Me.swConsigna.OnBackColor = System.Drawing.Color.DodgerBlue
        Me.swConsigna.OnText = "SI"
        Me.swConsigna.Size = New System.Drawing.Size(65, 22)
        Me.swConsigna.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.swConsigna.TabIndex = 369
        Me.swConsigna.Visible = False
        '
        'LabelX12
        '
        '
        '
        '
        Me.LabelX12.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX12.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX12.Location = New System.Drawing.Point(415, 137)
        Me.LabelX12.Name = "LabelX12"
        Me.LabelX12.Size = New System.Drawing.Size(90, 23)
        Me.LabelX12.TabIndex = 370
        Me.LabelX12.Text = "Consignación:"
        Me.LabelX12.Visible = False
        '
        'swEmision
        '
        '
        '
        '
        Me.swEmision.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.swEmision.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.swEmision.Location = New System.Drawing.Point(592, 110)
        Me.swEmision.Name = "swEmision"
        Me.swEmision.OffBackColor = System.Drawing.Color.LawnGreen
        Me.swEmision.OffText = "RECIBO"
        Me.swEmision.OnBackColor = System.Drawing.Color.Gold
        Me.swEmision.OnText = "FACTURA"
        Me.swEmision.Size = New System.Drawing.Size(120, 22)
        Me.swEmision.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.swEmision.TabIndex = 367
        '
        'LabelX16
        '
        '
        '
        '
        Me.LabelX16.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX16.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX16.Location = New System.Drawing.Point(415, 110)
        Me.LabelX16.Name = "LabelX16"
        Me.LabelX16.Size = New System.Drawing.Size(60, 23)
        Me.LabelX16.TabIndex = 368
        Me.LabelX16.Text = "Emisión:"
        '
        'GroupPanelFactura2
        '
        Me.GroupPanelFactura2.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanelFactura2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanelFactura2.Controls.Add(Me.lbNFactura)
        Me.GroupPanelFactura2.Controls.Add(Me.tbSACF)
        Me.GroupPanelFactura2.Controls.Add(Me.tbNFactura)
        Me.GroupPanelFactura2.Controls.Add(Me.lbSACF)
        Me.GroupPanelFactura2.Controls.Add(Me.lbNAutoriz)
        Me.GroupPanelFactura2.Controls.Add(Me.tbNDui)
        Me.GroupPanelFactura2.Controls.Add(Me.tbNAutorizacion)
        Me.GroupPanelFactura2.Controls.Add(Me.lbNDui)
        Me.GroupPanelFactura2.Controls.Add(Me.lbCodCtrl)
        Me.GroupPanelFactura2.Controls.Add(Me.tbCodControl)
        Me.GroupPanelFactura2.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanelFactura2.Location = New System.Drawing.Point(781, 0)
        Me.GroupPanelFactura2.Name = "GroupPanelFactura2"
        Me.GroupPanelFactura2.Size = New System.Drawing.Size(440, 160)
        '
        '
        '
        Me.GroupPanelFactura2.Style.BackColor = System.Drawing.SystemColors.Control
        Me.GroupPanelFactura2.Style.BackColor2 = System.Drawing.SystemColors.Control
        Me.GroupPanelFactura2.Style.BackColorGradientAngle = 90
        Me.GroupPanelFactura2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanelFactura2.Style.BorderBottomWidth = 1
        Me.GroupPanelFactura2.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.GroupPanelFactura2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanelFactura2.Style.BorderLeftWidth = 1
        Me.GroupPanelFactura2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanelFactura2.Style.BorderRightWidth = 1
        Me.GroupPanelFactura2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanelFactura2.Style.BorderTopWidth = 1
        Me.GroupPanelFactura2.Style.CornerDiameter = 4
        Me.GroupPanelFactura2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanelFactura2.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanelFactura2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.GroupPanelFactura2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanelFactura2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanelFactura2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanelFactura2.TabIndex = 366
        Me.GroupPanelFactura2.Text = "DATOS FACTURACIÓN"
        '
        'lbNFactura
        '
        Me.lbNFactura.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.lbNFactura.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lbNFactura.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbNFactura.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.lbNFactura.Location = New System.Drawing.Point(24, 4)
        Me.lbNFactura.Name = "lbNFactura"
        Me.lbNFactura.SingleLineColor = System.Drawing.SystemColors.Control
        Me.lbNFactura.Size = New System.Drawing.Size(145, 23)
        Me.lbNFactura.TabIndex = 356
        Me.lbNFactura.Text = "Nro. Factura:"
        '
        'tbSACF
        '
        Me.tbSACF.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.tbSACF.Border.Class = "TextBoxBorder"
        Me.tbSACF.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbSACF.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbSACF.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.tbSACF.Location = New System.Drawing.Point(207, 110)
        Me.tbSACF.Name = "tbSACF"
        Me.tbSACF.PreventEnterBeep = True
        Me.tbSACF.Size = New System.Drawing.Size(180, 22)
        Me.tbSACF.TabIndex = 365
        Me.tbSACF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbNFactura
        '
        Me.tbNFactura.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.tbNFactura.Border.Class = "TextBoxBorder"
        Me.tbNFactura.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbNFactura.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbNFactura.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.tbNFactura.Location = New System.Drawing.Point(207, 3)
        Me.tbNFactura.Name = "tbNFactura"
        Me.tbNFactura.PreventEnterBeep = True
        Me.tbNFactura.Size = New System.Drawing.Size(180, 22)
        Me.tbNFactura.TabIndex = 355
        Me.tbNFactura.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbSACF
        '
        Me.lbSACF.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.lbSACF.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lbSACF.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbSACF.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.lbSACF.Location = New System.Drawing.Point(25, 110)
        Me.lbSACF.Name = "lbSACF"
        Me.lbSACF.SingleLineColor = System.Drawing.SystemColors.Control
        Me.lbSACF.Size = New System.Drawing.Size(153, 22)
        Me.lbSACF.TabIndex = 364
        Me.lbSACF.Text = "Sujeto a Créd. Fiscal:"
        '
        'lbNAutoriz
        '
        Me.lbNAutoriz.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.lbNAutoriz.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lbNAutoriz.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbNAutoriz.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.lbNAutoriz.Location = New System.Drawing.Point(25, 30)
        Me.lbNAutoriz.Name = "lbNAutoriz"
        Me.lbNAutoriz.SingleLineColor = System.Drawing.SystemColors.Control
        Me.lbNAutoriz.Size = New System.Drawing.Size(125, 22)
        Me.lbNAutoriz.TabIndex = 358
        Me.lbNAutoriz.Text = "Nro. Autorización:"
        '
        'tbNDui
        '
        Me.tbNDui.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.tbNDui.Border.Class = "TextBoxBorder"
        Me.tbNDui.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbNDui.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbNDui.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.tbNDui.Location = New System.Drawing.Point(207, 83)
        Me.tbNDui.Name = "tbNDui"
        Me.tbNDui.PreventEnterBeep = True
        Me.tbNDui.Size = New System.Drawing.Size(180, 22)
        Me.tbNDui.TabIndex = 363
        Me.tbNDui.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbNAutorizacion
        '
        Me.tbNAutorizacion.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.tbNAutorizacion.Border.Class = "TextBoxBorder"
        Me.tbNAutorizacion.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbNAutorizacion.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbNAutorizacion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.tbNAutorizacion.Location = New System.Drawing.Point(207, 29)
        Me.tbNAutorizacion.Name = "tbNAutorizacion"
        Me.tbNAutorizacion.PreventEnterBeep = True
        Me.tbNAutorizacion.Size = New System.Drawing.Size(180, 22)
        Me.tbNAutorizacion.TabIndex = 359
        Me.tbNAutorizacion.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbNDui
        '
        Me.lbNDui.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.lbNDui.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lbNDui.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbNDui.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.lbNDui.Location = New System.Drawing.Point(25, 84)
        Me.lbNDui.Name = "lbNDui"
        Me.lbNDui.SingleLineColor = System.Drawing.SystemColors.Control
        Me.lbNDui.Size = New System.Drawing.Size(105, 22)
        Me.lbNDui.TabIndex = 362
        Me.lbNDui.Text = "Número DUI:"
        '
        'lbCodCtrl
        '
        Me.lbCodCtrl.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.lbCodCtrl.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lbCodCtrl.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbCodCtrl.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.lbCodCtrl.Location = New System.Drawing.Point(24, 57)
        Me.lbCodCtrl.Name = "lbCodCtrl"
        Me.lbCodCtrl.SingleLineColor = System.Drawing.SystemColors.Control
        Me.lbCodCtrl.Size = New System.Drawing.Size(135, 23)
        Me.lbCodCtrl.TabIndex = 361
        Me.lbCodCtrl.Text = "Código de Control:"
        '
        'tbCodControl
        '
        Me.tbCodControl.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.tbCodControl.Border.Class = "TextBoxBorder"
        Me.tbCodControl.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbCodControl.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbCodControl.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.tbCodControl.Location = New System.Drawing.Point(207, 56)
        Me.tbCodControl.MaxLength = 14
        Me.tbCodControl.Name = "tbCodControl"
        Me.tbCodControl.PreventEnterBeep = True
        Me.tbCodControl.Size = New System.Drawing.Size(180, 22)
        Me.tbCodControl.TabIndex = 360
        '
        'tbNitProv
        '
        Me.tbNitProv.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.tbNitProv.Border.Class = "TextBoxBorder"
        Me.tbNitProv.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbNitProv.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbNitProv.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.tbNitProv.Location = New System.Drawing.Point(130, 64)
        Me.tbNitProv.Name = "tbNitProv"
        Me.tbNitProv.PreventEnterBeep = True
        Me.tbNitProv.Size = New System.Drawing.Size(150, 22)
        Me.tbNitProv.TabIndex = 354
        Me.tbNitProv.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LabelX6
        '
        Me.LabelX6.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX6.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX6.Location = New System.Drawing.Point(13, 63)
        Me.LabelX6.Name = "LabelX6"
        Me.LabelX6.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX6.Size = New System.Drawing.Size(105, 22)
        Me.LabelX6.TabIndex = 353
        Me.LabelX6.Text = "Nit Proveedor:"
        '
        'lbCtrlEnter
        '
        Me.lbCtrlEnter.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.lbCtrlEnter.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lbCtrlEnter.Font = New System.Drawing.Font("Georgia", 7.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbCtrlEnter.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.lbCtrlEnter.Location = New System.Drawing.Point(197, 24)
        Me.lbCtrlEnter.Name = "lbCtrlEnter"
        Me.lbCtrlEnter.SingleLineColor = System.Drawing.SystemColors.Control
        Me.lbCtrlEnter.Size = New System.Drawing.Size(65, 10)
        Me.lbCtrlEnter.TabIndex = 352
        Me.lbCtrlEnter.Text = "Ctrl+Enter:"
        '
        'tbCodProv
        '
        '
        '
        '
        Me.tbCodProv.Border.Class = "TextBoxBorder"
        Me.tbCodProv.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbCodProv.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbCodProv.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.tbCodProv.Location = New System.Drawing.Point(130, 37)
        Me.tbCodProv.Name = "tbCodProv"
        Me.tbCodProv.PreventEnterBeep = True
        Me.tbCodProv.Size = New System.Drawing.Size(63, 22)
        Me.tbCodProv.TabIndex = 671
        Me.tbCodProv.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cbSucursal
        '
        cbSucursal_DesignTimeLayout.LayoutString = resources.GetString("cbSucursal_DesignTimeLayout.LayoutString")
        Me.cbSucursal.DesignTimeLayout = cbSucursal_DesignTimeLayout
        Me.cbSucursal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbSucursal.Location = New System.Drawing.Point(130, 117)
        Me.cbSucursal.Name = "cbSucursal"
        Me.cbSucursal.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.cbSucursal.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.cbSucursal.SelectedIndex = -1
        Me.cbSucursal.SelectedItem = Nothing
        Me.cbSucursal.Size = New System.Drawing.Size(164, 22)
        Me.cbSucursal.TabIndex = 657
        Me.cbSucursal.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'LabelX7
        '
        Me.LabelX7.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX7.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX7.Location = New System.Drawing.Point(13, 117)
        Me.LabelX7.Name = "LabelX7"
        Me.LabelX7.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX7.Size = New System.Drawing.Size(85, 23)
        Me.LabelX7.TabIndex = 235
        Me.LabelX7.Text = "Sucursal:"
        '
        'lbCredito
        '
        Me.lbCredito.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.lbCredito.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lbCredito.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbCredito.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.lbCredito.Location = New System.Drawing.Point(415, 31)
        Me.lbCredito.Name = "lbCredito"
        Me.lbCredito.SingleLineColor = System.Drawing.SystemColors.Control
        Me.lbCredito.Size = New System.Drawing.Size(156, 23)
        Me.lbCredito.TabIndex = 233
        Me.lbCredito.Text = "Vencimiento de Crédito:"
        '
        'LabelX5
        '
        Me.LabelX5.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX5.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX5.Location = New System.Drawing.Point(415, 5)
        Me.LabelX5.Name = "LabelX5"
        Me.LabelX5.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX5.Size = New System.Drawing.Size(129, 23)
        Me.LabelX5.TabIndex = 232
        Me.LabelX5.Text = "Tipo Compra:"
        '
        'LabelX4
        '
        Me.LabelX4.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX4.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX4.Location = New System.Drawing.Point(13, 88)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX4.Size = New System.Drawing.Size(100, 23)
        Me.LabelX4.TabIndex = 231
        Me.LabelX4.Text = "Fecha Compra:"
        '
        'tbFechaVenc
        '
        '
        '
        '
        Me.tbFechaVenc.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbFechaVenc.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaVenc.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown
        Me.tbFechaVenc.ButtonDropDown.Visible = True
        Me.tbFechaVenc.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbFechaVenc.IsPopupCalendarOpen = False
        Me.tbFechaVenc.Location = New System.Drawing.Point(592, 31)
        '
        '
        '
        '
        '
        '
        Me.tbFechaVenc.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaVenc.MonthCalendar.CalendarDimensions = New System.Drawing.Size(1, 1)
        Me.tbFechaVenc.MonthCalendar.ClearButtonVisible = True
        '
        '
        '
        Me.tbFechaVenc.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.tbFechaVenc.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90
        Me.tbFechaVenc.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.tbFechaVenc.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.tbFechaVenc.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.tbFechaVenc.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1
        Me.tbFechaVenc.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaVenc.MonthCalendar.DisplayMonth = New Date(2017, 2, 1, 0, 0, 0, 0)
        Me.tbFechaVenc.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday
        '
        '
        '
        Me.tbFechaVenc.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.tbFechaVenc.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.tbFechaVenc.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.tbFechaVenc.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaVenc.MonthCalendar.TodayButtonVisible = True
        Me.tbFechaVenc.Name = "tbFechaVenc"
        Me.tbFechaVenc.Size = New System.Drawing.Size(120, 22)
        Me.tbFechaVenc.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.tbFechaVenc.TabIndex = 457
        Me.tbFechaVenc.Visible = False
        '
        'swTipoVenta
        '
        '
        '
        '
        Me.swTipoVenta.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.swTipoVenta.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.swTipoVenta.Location = New System.Drawing.Point(592, 5)
        Me.swTipoVenta.Name = "swTipoVenta"
        Me.swTipoVenta.OffBackColor = System.Drawing.Color.LawnGreen
        Me.swTipoVenta.OffText = "CREDITO"
        Me.swTipoVenta.OnBackColor = System.Drawing.Color.Gold
        Me.swTipoVenta.OnText = "CONTADO"
        Me.swTipoVenta.Size = New System.Drawing.Size(120, 22)
        Me.swTipoVenta.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.swTipoVenta.TabIndex = 5
        Me.swTipoVenta.Value = True
        Me.swTipoVenta.ValueObject = "Y"
        '
        'tbFechaVenta
        '
        '
        '
        '
        Me.tbFechaVenta.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbFechaVenta.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaVenta.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown
        Me.tbFechaVenta.ButtonDropDown.Visible = True
        Me.tbFechaVenta.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbFechaVenta.IsPopupCalendarOpen = False
        Me.tbFechaVenta.Location = New System.Drawing.Point(130, 90)
        '
        '
        '
        '
        '
        '
        Me.tbFechaVenta.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaVenta.MonthCalendar.CalendarDimensions = New System.Drawing.Size(1, 1)
        Me.tbFechaVenta.MonthCalendar.ClearButtonVisible = True
        '
        '
        '
        Me.tbFechaVenta.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.tbFechaVenta.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90
        Me.tbFechaVenta.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.tbFechaVenta.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.tbFechaVenta.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.tbFechaVenta.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1
        Me.tbFechaVenta.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaVenta.MonthCalendar.DisplayMonth = New Date(2017, 2, 1, 0, 0, 0, 0)
        Me.tbFechaVenta.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday
        '
        '
        '
        Me.tbFechaVenta.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.tbFechaVenta.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.tbFechaVenta.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.tbFechaVenta.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaVenta.MonthCalendar.TodayButtonVisible = True
        Me.tbFechaVenta.Name = "tbFechaVenta"
        Me.tbFechaVenta.Size = New System.Drawing.Size(120, 22)
        Me.tbFechaVenta.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.tbFechaVenta.TabIndex = 357
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
        Me.LabelX1.Location = New System.Drawing.Point(13, 14)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX1.Size = New System.Drawing.Size(50, 16)
        Me.LabelX1.TabIndex = 224
        Me.LabelX1.Text = "Código:"
        '
        'tbObservacion
        '
        Me.tbObservacion.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.tbObservacion.Border.Class = "TextBoxBorder"
        Me.tbObservacion.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbObservacion.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbObservacion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.tbObservacion.Location = New System.Drawing.Point(130, 146)
        Me.tbObservacion.MaxLength = 300
        Me.tbObservacion.Multiline = True
        Me.tbObservacion.Name = "tbObservacion"
        Me.tbObservacion.PreventEnterBeep = True
        Me.tbObservacion.Size = New System.Drawing.Size(265, 47)
        Me.tbObservacion.TabIndex = 2
        '
        'LabelX3
        '
        Me.LabelX3.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX3.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX3.Location = New System.Drawing.Point(13, 142)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX3.Size = New System.Drawing.Size(85, 23)
        Me.LabelX3.TabIndex = 226
        Me.LabelX3.Text = "Observación:"
        '
        'tbProveedor
        '
        Me.tbProveedor.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.tbProveedor.Border.Class = "TextBoxBorder"
        Me.tbProveedor.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbProveedor.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbProveedor.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.tbProveedor.Location = New System.Drawing.Point(196, 37)
        Me.tbProveedor.Name = "tbProveedor"
        Me.tbProveedor.PreventEnterBeep = True
        Me.tbProveedor.Size = New System.Drawing.Size(200, 22)
        Me.tbProveedor.TabIndex = 1
        '
        'LabelX2
        '
        Me.LabelX2.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX2.Location = New System.Drawing.Point(12, 38)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX2.Size = New System.Drawing.Size(75, 23)
        Me.LabelX2.TabIndex = 225
        Me.LabelX2.Text = "Proveedor:"
        '
        'tbCodigo
        '
        '
        '
        '
        Me.tbCodigo.Border.Class = "TextBoxBorder"
        Me.tbCodigo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbCodigo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbCodigo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.tbCodigo.Location = New System.Drawing.Point(130, 8)
        Me.tbCodigo.Name = "tbCodigo"
        Me.tbCodigo.PreventEnterBeep = True
        Me.tbCodigo.Size = New System.Drawing.Size(63, 22)
        Me.tbCodigo.TabIndex = 0
        Me.tbCodigo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'BUSCADOR
        '
        Me.BUSCADOR.AttachedControl = Me.SuperTabControlPanel2
        Me.BUSCADOR.GlobalItem = False
        Me.BUSCADOR.Name = "BUSCADOR"
        Me.BUSCADOR.Text = "BUSCADOR"
        '
        'SuperTabControlPanel2
        '
        Me.SuperTabControlPanel2.Controls.Add(Me.TableLayoutPanel1)
        Me.SuperTabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SuperTabControlPanel2.Location = New System.Drawing.Point(0, 0)
        Me.SuperTabControlPanel2.Name = "SuperTabControlPanel2"
        Me.SuperTabControlPanel2.Size = New System.Drawing.Size(1317, 618)
        Me.SuperTabControlPanel2.TabIndex = 0
        Me.SuperTabControlPanel2.TabItem = Me.BUSCADOR
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.PanelVentas, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 618.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1317, 618)
        Me.TableLayoutPanel1.TabIndex = 2
        '
        'PanelVentas
        '
        Me.PanelVentas.AutoScroll = True
        Me.PanelVentas.BackColor = System.Drawing.Color.White
        Me.PanelVentas.Controls.Add(Me.GroupPanel2)
        Me.PanelVentas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelVentas.Location = New System.Drawing.Point(3, 3)
        Me.PanelVentas.Name = "PanelVentas"
        Me.PanelVentas.Size = New System.Drawing.Size(1311, 612)
        Me.PanelVentas.TabIndex = 0
        '
        'GroupPanel2
        '
        Me.GroupPanel2.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel2.Controls.Add(Me.Panel6)
        Me.GroupPanel2.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupPanel2.Font = New System.Drawing.Font("Georgia", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupPanel2.Location = New System.Drawing.Point(0, 0)
        Me.GroupPanel2.Name = "GroupPanel2"
        Me.GroupPanel2.Size = New System.Drawing.Size(1311, 612)
        '
        '
        '
        Me.GroupPanel2.Style.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.GroupPanel2.Style.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.GroupPanel2.Style.BackColorGradientAngle = 90
        Me.GroupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel2.Style.BorderBottomWidth = 1
        Me.GroupPanel2.Style.BorderColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.GroupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel2.Style.BorderLeftWidth = 1
        Me.GroupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel2.Style.BorderRightWidth = 1
        Me.GroupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel2.Style.BorderTopWidth = 1
        Me.GroupPanel2.Style.CornerDiameter = 4
        Me.GroupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanel2.Style.Font = New System.Drawing.Font("Georgia", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupPanel2.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanel2.Style.TextColor = System.Drawing.Color.White
        Me.GroupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanel2.TabIndex = 3
        Me.GroupPanel2.Text = "BUSCADOR   COMPRAS"
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.White
        Me.Panel6.Controls.Add(Me.grCompra)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1305, 589)
        Me.Panel6.TabIndex = 0
        '
        'grCompra
        '
        Me.grCompra.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grCompra.BackColor = System.Drawing.Color.GhostWhite
        Me.grCompra.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grCompra.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None
        Me.grCompra.FocusStyle = Janus.Windows.GridEX.FocusStyle.Solid
        Me.grCompra.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grCompra.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.UseRowStyle
        Me.grCompra.HeaderFormatStyle.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grCompra.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight
        Me.grCompra.Location = New System.Drawing.Point(0, 0)
        Me.grCompra.Name = "grCompra"
        Me.grCompra.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.grCompra.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.grCompra.SelectedFormatStyle.BackColor = System.Drawing.Color.DodgerBlue
        Me.grCompra.SelectedFormatStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grCompra.SelectedFormatStyle.ForeColor = System.Drawing.Color.White
        Me.grCompra.SelectOnExpand = False
        Me.grCompra.Size = New System.Drawing.Size(1305, 589)
        Me.grCompra.TabIndex = 0
        Me.grCompra.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 500
        '
        'cmOpcionesDetalle
        '
        Me.cmOpcionesDetalle.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.cmOpcionesDetalle.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.INSERTARFILAToolStripMenuItem})
        Me.cmOpcionesDetalle.Name = "ContextMenuStrip1"
        Me.cmOpcionesDetalle.Size = New System.Drawing.Size(151, 26)
        '
        'INSERTARFILAToolStripMenuItem
        '
        Me.INSERTARFILAToolStripMenuItem.Name = "INSERTARFILAToolStripMenuItem"
        Me.INSERTARFILAToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
        Me.INSERTARFILAToolStripMenuItem.Text = "INSERTAR FILA"
        '
        'F0_MCompras
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1350, 729)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "F0_MCompras"
        Me.Text = "F0_MCompras"
        Me.Controls.SetChildIndex(Me.PanelPrincipal, 0)
        Me.PanelSuperior.ResumeLayout(False)
        Me.PanelInferior.ResumeLayout(False)
        CType(Me.BubbleBarUsuario, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelToolBar1.ResumeLayout(False)
        Me.PanelToolBar2.ResumeLayout(False)
        Me.PanelPrincipal.ResumeLayout(False)
        Me.PanelUsuario.ResumeLayout(False)
        Me.PanelUsuario.PerformLayout()
        Me.PanelNavegacion.ResumeLayout(False)
        Me.MPanelUserAct.ResumeLayout(False)
        Me.MPanelUserAct.PerformLayout()
        CType(Me.MEP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelContent.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.MSuperTabControlPanel1.ResumeLayout(False)
        CType(Me.MSuperTabControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MSuperTabControl.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelDetalle.ResumeLayout(False)
        Me.GroupPanel4.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        CType(Me.grdetalle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GPanelProductos.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        CType(Me.grProductos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelTotal.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        CType(Me.tbIce, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbDescPro, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbSubtotalC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbPdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbMdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbtotal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelContentSup.ResumeLayout(False)
        Me.GroupPanel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.tbTipoCambio, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupPanelFactura2.ResumeLayout(False)
        CType(Me.cbSucursal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbFechaVenc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbFechaVenta, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SuperTabControlPanel2.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.PanelVentas.ResumeLayout(False)
        Me.GroupPanel2.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        CType(Me.grCompra, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmOpcionesDetalle.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelDetalle As System.Windows.Forms.Panel
    Friend WithEvents GroupPanel4 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents grdetalle As Janus.Windows.GridEX.GridEX
    Friend WithEvents GPanelProductos As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents grProductos As Janus.Windows.GridEX.GridEX
    Friend WithEvents PanelTotal As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents tbMdesc As DevComponents.Editors.DoubleInput
    Friend WithEvents tbtotal As DevComponents.Editors.DoubleInput
    Friend WithEvents LabelX8 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX11 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbPdesc As DevComponents.Editors.DoubleInput
    Friend WithEvents LabelX9 As DevComponents.DotNetBar.LabelX
    Friend WithEvents PanelContentSup As System.Windows.Forms.Panel
    Friend WithEvents GroupPanel1 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents cbSucursal As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents LabelX7 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lbCredito As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbFechaVenc As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents swTipoVenta As DevComponents.DotNetBar.Controls.SwitchButton
    Friend WithEvents tbFechaVenta As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbObservacion As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbProveedor As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbCodigo As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents SuperTabControlPanel2 As DevComponents.DotNetBar.SuperTabControlPanel
    Friend WithEvents BUSCADOR As DevComponents.DotNetBar.SuperTabItem
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents PanelVentas As System.Windows.Forms.Panel
    Friend WithEvents GroupPanel2 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents grCompra As Janus.Windows.GridEX.GridEX
    Friend WithEvents tbCodProv As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents lbCtrlEnter As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbSACF As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents lbSACF As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbNDui As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents lbNDui As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbCodControl As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents lbCodCtrl As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbNAutorizacion As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents lbNAutoriz As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbNFactura As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents lbNFactura As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbNitProv As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX6 As DevComponents.DotNetBar.LabelX
    Friend WithEvents GroupPanelFactura2 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents swEmision As DevComponents.DotNetBar.Controls.SwitchButton
    Friend WithEvents LabelX16 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbSubtotalC As DevComponents.Editors.DoubleInput
    Friend WithEvents LabelX10 As DevComponents.DotNetBar.LabelX
    Friend WithEvents swConsigna As DevComponents.DotNetBar.Controls.SwitchButton
    Friend WithEvents LabelX12 As DevComponents.DotNetBar.LabelX
    Friend WithEvents swRetencion As DevComponents.DotNetBar.Controls.SwitchButton
    Friend WithEvents LabelX13 As DevComponents.DotNetBar.LabelX
    Friend WithEvents Timer1 As Timer
    Friend WithEvents LabelX14 As DevComponents.DotNetBar.LabelX
    Friend WithEvents swMoneda As DevComponents.DotNetBar.Controls.SwitchButton
    Public WithEvents tbTipoCambio As DevComponents.Editors.DoubleInput
    Friend WithEvents lbTipoCambio As DevComponents.DotNetBar.LabelX
    Friend WithEvents cmOpcionesDetalle As ContextMenuStrip
    Friend WithEvents INSERTARFILAToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents tbIce As DevComponents.Editors.DoubleInput
    Friend WithEvents LabelX17 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbDescPro As DevComponents.Editors.DoubleInput
    Friend WithEvents LabelX15 As DevComponents.DotNetBar.LabelX
End Class
