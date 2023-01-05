<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class R_KardexCreditoPagos
    Inherits Modelo.ModeloR0


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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(R_KardexCreditoPagos))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.swdetresum = New DevComponents.DotNetBar.Controls.SwitchButton()
        Me.swCreditoCliente = New DevComponents.DotNetBar.Controls.SwitchButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.tbCuentas = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.tbCliente = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.CheckTodosCuenta = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.CheckUnaCuenta = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.tbcodCuenta = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.lbCuentas = New DevComponents.DotNetBar.LabelX()
        Me.tbCodigoCliente = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.lbcliente = New DevComponents.DotNetBar.LabelX()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.tbFechaF = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.tbFechaI = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.SuperTabPrincipal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuperTabPrincipal.SuspendLayout()
        Me.SuperTabControlPanelRegistro.SuspendLayout()
        Me.PanelSuperior.SuspendLayout()
        Me.PanelInferior.SuspendLayout()
        CType(Me.BubbleBarUsuario, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelToolBar1.SuspendLayout()
        Me.PanelPrincipal.SuspendLayout()
        Me.PanelUsuario.SuspendLayout()
        Me.MPanelUserAct.SuspendLayout()
        CType(Me.MEP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MGPFiltros.SuspendLayout()
        Me.PanelIzq.SuspendLayout()
        CType(Me.MPicture, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.tbFechaF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbFechaI, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SuperTabPrincipal.Size = New System.Drawing.Size(1816, 912)
        Me.SuperTabPrincipal.Controls.SetChildIndex(Me.SuperTabControlPanelBuscador, 0)
        Me.SuperTabPrincipal.Controls.SetChildIndex(Me.SuperTabControlPanelRegistro, 0)
        '
        'SuperTabControlPanelBuscador
        '
        Me.SuperTabControlPanelBuscador.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.SuperTabControlPanelBuscador.Size = New System.Drawing.Size(1144, 690)
        '
        'SuperTabControlPanelRegistro
        '
        Me.SuperTabControlPanelRegistro.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.SuperTabControlPanelRegistro.Size = New System.Drawing.Size(1781, 912)
        Me.SuperTabControlPanelRegistro.Controls.SetChildIndex(Me.PanelInferior, 0)
        Me.SuperTabControlPanelRegistro.Controls.SetChildIndex(Me.PanelIzq, 0)
        Me.SuperTabControlPanelRegistro.Controls.SetChildIndex(Me.PanelPrincipal, 0)
        '
        'PanelSuperior
        '
        Me.PanelSuperior.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelSuperior.Style.BackColor1.Color = System.Drawing.Color.Yellow
        Me.PanelSuperior.Style.BackColor2.Color = System.Drawing.Color.Khaki
        Me.PanelSuperior.Style.BackgroundImage = CType(resources.GetObject("PanelSuperior.Style.BackgroundImage"), System.Drawing.Image)
        Me.PanelSuperior.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelSuperior.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelSuperior.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelSuperior.Style.GradientAngle = 90
        '
        'PanelInferior
        '
        Me.PanelInferior.Location = New System.Drawing.Point(0, 868)
        Me.PanelInferior.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PanelInferior.Size = New System.Drawing.Size(1781, 44)
        Me.PanelInferior.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelInferior.Style.BackColor1.Color = System.Drawing.Color.Gold
        Me.PanelInferior.Style.BackColor2.Color = System.Drawing.Color.Gold
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
        Me.btnSalir.Text = "SALIRr"
        '
        'btnGenerar
        '
        '
        'PanelPrincipal
        '
        Me.PanelPrincipal.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PanelPrincipal.Size = New System.Drawing.Size(1297, 868)
        '
        'MPanelUserAct
        '
        Me.MPanelUserAct.Location = New System.Drawing.Point(1514, 0)
        Me.MPanelUserAct.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        '
        'MReportViewer
        '
        Me.MReportViewer.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.MReportViewer.Size = New System.Drawing.Size(1297, 868)
        '
        'MGPFiltros
        '
        Me.MGPFiltros.Controls.Add(Me.GroupBox2)
        Me.MGPFiltros.Controls.Add(Me.GroupBox1)
        Me.MGPFiltros.Size = New System.Drawing.Size(484, 779)
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
        '
        'PanelIzq
        '
        Me.PanelIzq.Size = New System.Drawing.Size(484, 868)
        Me.PanelIzq.Controls.SetChildIndex(Me.PanelSuperior, 0)
        Me.PanelIzq.Controls.SetChildIndex(Me.MGPFiltros, 0)
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.swdetresum)
        Me.GroupBox1.Controls.Add(Me.swCreditoCliente)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(478, 84)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Agrupados"
        '
        'swdetresum
        '
        '
        '
        '
        Me.swdetresum.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.swdetresum.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.swdetresum.Location = New System.Drawing.Point(128, 57)
        Me.swdetresum.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.swdetresum.Name = "swdetresum"
        Me.swdetresum.OffBackColor = System.Drawing.Color.DarkSlateGray
        Me.swdetresum.OffText = "RESUMEN"
        Me.swdetresum.OffTextColor = System.Drawing.Color.White
        Me.swdetresum.OnBackColor = System.Drawing.Color.Gold
        Me.swdetresum.OnText = "DETALLE"
        Me.swdetresum.Size = New System.Drawing.Size(216, 27)
        Me.swdetresum.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.swdetresum.TabIndex = 247
        Me.swdetresum.Value = True
        Me.swdetresum.ValueObject = "Y"
        Me.swdetresum.Visible = False
        '
        'swCreditoCliente
        '
        '
        '
        '
        Me.swCreditoCliente.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.swCreditoCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.swCreditoCliente.Location = New System.Drawing.Point(128, 25)
        Me.swCreditoCliente.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.swCreditoCliente.Name = "swCreditoCliente"
        Me.swCreditoCliente.OffBackColor = System.Drawing.Color.DarkSlateGray
        Me.swCreditoCliente.OffText = "UN PROVEEDOR"
        Me.swCreditoCliente.OffTextColor = System.Drawing.Color.White
        Me.swCreditoCliente.OnBackColor = System.Drawing.Color.Gold
        Me.swCreditoCliente.OnText = "TODOS PROVEEDORES"
        Me.swCreditoCliente.Size = New System.Drawing.Size(251, 27)
        Me.swCreditoCliente.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.swCreditoCliente.TabIndex = 6
        Me.swCreditoCliente.Value = True
        Me.swCreditoCliente.ValueObject = "Y"
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.tbCuentas)
        Me.GroupBox2.Controls.Add(Me.tbCliente)
        Me.GroupBox2.Controls.Add(Me.CheckTodosCuenta)
        Me.GroupBox2.Controls.Add(Me.CheckUnaCuenta)
        Me.GroupBox2.Controls.Add(Me.tbcodCuenta)
        Me.GroupBox2.Controls.Add(Me.lbCuentas)
        Me.GroupBox2.Controls.Add(Me.tbCodigoCliente)
        Me.GroupBox2.Controls.Add(Me.lbcliente)
        Me.GroupBox2.Controls.Add(Me.LabelX1)
        Me.GroupBox2.Controls.Add(Me.tbFechaF)
        Me.GroupBox2.Controls.Add(Me.LabelX4)
        Me.GroupBox2.Controls.Add(Me.tbFechaI)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(0, 84)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox2.Size = New System.Drawing.Size(478, 668)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Datos"
        '
        'tbCuentas
        '
        Me.tbCuentas.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.tbCuentas.Border.Class = "TextBoxBorder"
        Me.tbCuentas.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbCuentas.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbCuentas.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.tbCuentas.Location = New System.Drawing.Point(97, 204)
        Me.tbCuentas.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbCuentas.Name = "tbCuentas"
        Me.tbCuentas.PreventEnterBeep = True
        Me.tbCuentas.Size = New System.Drawing.Size(223, 26)
        Me.tbCuentas.TabIndex = 246
        '
        'tbCliente
        '
        Me.tbCliente.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.tbCliente.Border.Class = "TextBoxBorder"
        Me.tbCliente.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbCliente.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.tbCliente.Location = New System.Drawing.Point(96, 142)
        Me.tbCliente.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbCliente.Name = "tbCliente"
        Me.tbCliente.PreventEnterBeep = True
        Me.tbCliente.Size = New System.Drawing.Size(339, 26)
        Me.tbCliente.TabIndex = 245
        '
        'CheckTodosCuenta
        '
        '
        '
        '
        Me.CheckTodosCuenta.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.CheckTodosCuenta.Location = New System.Drawing.Point(392, 204)
        Me.CheckTodosCuenta.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CheckTodosCuenta.Name = "CheckTodosCuenta"
        Me.CheckTodosCuenta.Size = New System.Drawing.Size(73, 28)
        Me.CheckTodosCuenta.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.CheckTodosCuenta.TabIndex = 244
        Me.CheckTodosCuenta.Text = "Todos"
        '
        'CheckUnaCuenta
        '
        '
        '
        '
        Me.CheckUnaCuenta.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.CheckUnaCuenta.Location = New System.Drawing.Point(325, 204)
        Me.CheckUnaCuenta.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CheckUnaCuenta.Name = "CheckUnaCuenta"
        Me.CheckUnaCuenta.Size = New System.Drawing.Size(59, 28)
        Me.CheckUnaCuenta.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.CheckUnaCuenta.TabIndex = 240
        Me.CheckUnaCuenta.Text = "Una"
        '
        'tbcodCuenta
        '
        '
        '
        '
        Me.tbcodCuenta.Border.Class = "TextBoxBorder"
        Me.tbcodCuenta.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbcodCuenta.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbcodCuenta.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.tbcodCuenta.Location = New System.Drawing.Point(56, 202)
        Me.tbcodCuenta.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbcodCuenta.Name = "tbcodCuenta"
        Me.tbcodCuenta.PreventEnterBeep = True
        Me.tbcodCuenta.Size = New System.Drawing.Size(29, 26)
        Me.tbcodCuenta.TabIndex = 243
        Me.tbcodCuenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tbcodCuenta.Visible = False
        '
        'lbCuentas
        '
        Me.lbCuentas.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.lbCuentas.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lbCuentas.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbCuentas.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.lbCuentas.Location = New System.Drawing.Point(9, 172)
        Me.lbCuentas.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.lbCuentas.Name = "lbCuentas"
        Me.lbCuentas.SingleLineColor = System.Drawing.SystemColors.Control
        Me.lbCuentas.Size = New System.Drawing.Size(99, 28)
        Me.lbCuentas.TabIndex = 241
        Me.lbCuentas.Text = "Cuentas:"
        '
        'tbCodigoCliente
        '
        '
        '
        '
        Me.tbCodigoCliente.Border.Class = "TextBoxBorder"
        Me.tbCodigoCliente.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbCodigoCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbCodigoCliente.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.tbCodigoCliente.Location = New System.Drawing.Point(59, 138)
        Me.tbCodigoCliente.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbCodigoCliente.Name = "tbCodigoCliente"
        Me.tbCodigoCliente.PreventEnterBeep = True
        Me.tbCodigoCliente.Size = New System.Drawing.Size(29, 26)
        Me.tbCodigoCliente.TabIndex = 238
        Me.tbCodigoCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tbCodigoCliente.Visible = False
        '
        'lbcliente
        '
        Me.lbcliente.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.lbcliente.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lbcliente.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbcliente.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.lbcliente.Location = New System.Drawing.Point(12, 108)
        Me.lbcliente.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.lbcliente.Name = "lbcliente"
        Me.lbcliente.SingleLineColor = System.Drawing.SystemColors.Control
        Me.lbcliente.Size = New System.Drawing.Size(99, 28)
        Me.lbcliente.TabIndex = 236
        Me.lbcliente.Text = "Proveedor:"
        '
        'LabelX1
        '
        Me.LabelX1.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX1.Location = New System.Drawing.Point(92, 60)
        Me.LabelX1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX1.Size = New System.Drawing.Size(69, 28)
        Me.LabelX1.TabIndex = 235
        Me.LabelX1.Text = "Al:"
        '
        'tbFechaF
        '
        '
        '
        '
        Me.tbFechaF.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbFechaF.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaF.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown
        Me.tbFechaF.ButtonDropDown.Visible = True
        Me.tbFechaF.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbFechaF.IsPopupCalendarOpen = False
        Me.tbFechaF.Location = New System.Drawing.Point(193, 60)
        Me.tbFechaF.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        '
        '
        '
        '
        '
        '
        Me.tbFechaF.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaF.MonthCalendar.CalendarDimensions = New System.Drawing.Size(1, 1)
        Me.tbFechaF.MonthCalendar.ClearButtonVisible = True
        '
        '
        '
        Me.tbFechaF.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.tbFechaF.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90
        Me.tbFechaF.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.tbFechaF.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.tbFechaF.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.tbFechaF.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1
        Me.tbFechaF.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaF.MonthCalendar.DisplayMonth = New Date(2017, 2, 1, 0, 0, 0, 0)
        Me.tbFechaF.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday
        '
        '
        '
        Me.tbFechaF.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.tbFechaF.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.tbFechaF.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.tbFechaF.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaF.MonthCalendar.TodayButtonVisible = True
        Me.tbFechaF.Name = "tbFechaF"
        Me.tbFechaF.Size = New System.Drawing.Size(160, 26)
        Me.tbFechaF.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.tbFechaF.TabIndex = 234
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
        Me.LabelX4.Location = New System.Drawing.Point(88, 21)
        Me.LabelX4.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX4.Size = New System.Drawing.Size(99, 28)
        Me.LabelX4.TabIndex = 233
        Me.LabelX4.Text = "Fecha Del:"
        '
        'tbFechaI
        '
        '
        '
        '
        Me.tbFechaI.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbFechaI.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaI.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown
        Me.tbFechaI.ButtonDropDown.Visible = True
        Me.tbFechaI.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbFechaI.IsPopupCalendarOpen = False
        Me.tbFechaI.Location = New System.Drawing.Point(193, 22)
        Me.tbFechaI.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        '
        '
        '
        '
        '
        '
        Me.tbFechaI.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaI.MonthCalendar.CalendarDimensions = New System.Drawing.Size(1, 1)
        Me.tbFechaI.MonthCalendar.ClearButtonVisible = True
        '
        '
        '
        Me.tbFechaI.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.tbFechaI.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90
        Me.tbFechaI.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.tbFechaI.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.tbFechaI.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.tbFechaI.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1
        Me.tbFechaI.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaI.MonthCalendar.DisplayMonth = New Date(2017, 2, 1, 0, 0, 0, 0)
        Me.tbFechaI.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday
        '
        '
        '
        Me.tbFechaI.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.tbFechaI.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.tbFechaI.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.tbFechaI.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaI.MonthCalendar.TodayButtonVisible = True
        Me.tbFechaI.Name = "tbFechaI"
        Me.tbFechaI.Size = New System.Drawing.Size(160, 26)
        Me.tbFechaI.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.tbFechaI.TabIndex = 232
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 500
        '
        'R_KardexCreditoPagos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1816, 912)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "R_KardexCreditoPagos"
        Me.Text = "R_KardexCreditoPagos"
        Me.Controls.SetChildIndex(Me.SuperTabPrincipal, 0)
        CType(Me.SuperTabPrincipal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SuperTabPrincipal.ResumeLayout(False)
        Me.SuperTabControlPanelRegistro.ResumeLayout(False)
        Me.PanelSuperior.ResumeLayout(False)
        Me.PanelInferior.ResumeLayout(False)
        CType(Me.BubbleBarUsuario, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelToolBar1.ResumeLayout(False)
        Me.PanelPrincipal.ResumeLayout(False)
        Me.PanelUsuario.ResumeLayout(False)
        Me.PanelUsuario.PerformLayout()
        Me.MPanelUserAct.ResumeLayout(False)
        CType(Me.MEP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MGPFiltros.ResumeLayout(False)
        Me.PanelIzq.ResumeLayout(False)
        CType(Me.MPicture, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.tbFechaF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbFechaI, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents swdetresum As DevComponents.DotNetBar.Controls.SwitchButton
    Friend WithEvents swCreditoCliente As DevComponents.DotNetBar.Controls.SwitchButton
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents tbCuentas As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents tbCliente As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents CheckTodosCuenta As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents CheckUnaCuenta As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents tbcodCuenta As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents lbCuentas As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbCodigoCliente As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents lbcliente As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbFechaF As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbFechaI As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents Timer1 As Timer
End Class
