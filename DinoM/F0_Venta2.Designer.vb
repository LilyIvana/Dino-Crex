<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class F0_Venta2
    Inherits Modelo.ModeloF0
    'Inherits System.Windows.Forms.Form

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
        Dim cbSucursal_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim cbCambioDolar_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim CbTipoDoc_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F0_Venta2))
        Me.GroupPanel2 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.TbEmail = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX27 = New DevComponents.DotNetBar.LabelX()
        Me.lbNroCaja = New System.Windows.Forms.Label()
        Me.LabelX24 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX23 = New DevComponents.DotNetBar.LabelX()
        Me.tbObservacion = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.lblObservacion = New DevComponents.DotNetBar.LabelX()
        Me.LabelX21 = New DevComponents.DotNetBar.LabelX()
        Me.txtEstado = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.lbCredito = New DevComponents.DotNetBar.LabelX()
        Me.tbFechaVenc = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.swTipoVenta = New DevComponents.DotNetBar.Controls.SwitchButton()
        Me.LabelX17 = New DevComponents.DotNetBar.LabelX()
        Me.QrFactura = New Gma.QrCodeNet.Encoding.Windows.Forms.QrCodeImgControl()
        Me.TbNombre2 = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.cbSucursal = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.tbFechaVenta = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.lbCtrlEnter = New DevComponents.DotNetBar.LabelX()
        Me.TbNombre1 = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX10 = New DevComponents.DotNetBar.LabelX()
        Me.tbVendedor = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.tbCliente = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.tbCodigo = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.lbFVenta = New DevComponents.DotNetBar.LabelX()
        Me.lbIdVenta = New DevComponents.DotNetBar.LabelX()
        Me.tbNit = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.GroupCobranza = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.tbMdesc = New DevComponents.Editors.DoubleInput()
        Me.tbSubTotal = New DevComponents.Editors.DoubleInput()
        Me.txtMontoPagado1 = New DevComponents.DotNetBar.LabelX()
        Me.txtCambio1 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX22 = New DevComponents.DotNetBar.LabelX()
        Me.tbTotalDo = New DevComponents.DotNetBar.LabelX()
        Me.LabelX13 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX20 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX19 = New DevComponents.DotNetBar.LabelX()
        Me.tbTotalBs = New DevComponents.DotNetBar.LabelX()
        Me.LabelX18 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX12 = New DevComponents.DotNetBar.LabelX()
        Me.cbCambioDolar = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.btgrupo1 = New DevComponents.DotNetBar.ButtonX()
        Me.txtCambio = New DevComponents.Editors.DoubleInput()
        Me.txtMontoPagado = New DevComponents.Editors.DoubleInput()
        Me.lbCambio = New DevComponents.DotNetBar.LabelX()
        Me.lbMontoPagado = New DevComponents.DotNetBar.LabelX()
        Me.tbIce = New DevComponents.Editors.DoubleInput()
        Me.lbIce = New DevComponents.DotNetBar.LabelX()
        Me.tbPrueba = New DevComponents.Editors.DoubleInput()
        Me.LabelX8 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX11 = New DevComponents.DotNetBar.LabelX()
        Me.tbPdesc = New DevComponents.Editors.DoubleInput()
        Me.LabelX9 = New DevComponents.DotNetBar.LabelX()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.tbNroTarjeta = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.lbNroTarjeta = New DevComponents.DotNetBar.LabelX()
        Me.tbMontoTarej = New DevComponents.Editors.DoubleInput()
        Me.chbTarjeta = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.tbMontoBs = New DevComponents.Editors.DoubleInput()
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX()
        Me.tbMontoDolar = New DevComponents.Editors.DoubleInput()
        Me.LabelX6 = New DevComponents.DotNetBar.LabelX()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupPanelFactura2 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.tbCodigoControl = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX16 = New DevComponents.DotNetBar.LabelX()
        Me.dtiFechaFactura = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.tbNroFactura = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.tbNroAutoriz = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX15 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX14 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX7 = New DevComponents.DotNetBar.LabelX()
        Me.swMoneda = New DevComponents.DotNetBar.Controls.SwitchButton()
        Me.gpDetalleVenta = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.GPanelProductos = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.grProductos = New Janus.Windows.GridEX.GridEX()
        Me.grdetalle = New Janus.Windows.GridEX.GridEX()
        Me.SuperTabItem1 = New DevComponents.DotNetBar.SuperTabItem()
        Me.SuperTabControlPanel1 = New DevComponents.DotNetBar.SuperTabControlPanel()
        Me.GroupPanel3 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.grVentas = New Janus.Windows.GridEX.GridEX()
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        Me.btnDuplicar = New DevComponents.DotNetBar.ButtonX()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.PanelFondoDetalle = New System.Windows.Forms.Panel()
        Me.PanelFacturaYCobranza = New System.Windows.Forms.Panel()
        Me.PanelEncabezado = New System.Windows.Forms.Panel()
        Me.btnBitacora = New DevComponents.DotNetBar.ButtonX()
        Me.SwDescuentoProveedor = New DevComponents.DotNetBar.Controls.SwitchButton()
        Me.CbTipoDoc = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.LabelX25 = New DevComponents.DotNetBar.LabelX()
        Me.Label1Conn = New System.Windows.Forms.Label()
        Me.lbEjemplo = New DevComponents.DotNetBar.LabelX()
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
        Me.GroupPanel2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.tbFechaVenc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.QrFactura, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbSucursal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbFechaVenta, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupCobranza.SuspendLayout()
        CType(Me.tbMdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbSubTotal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbCambioDolar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCambio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMontoPagado, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbIce, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbPrueba, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbPdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.tbMontoTarej, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbMontoBs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbMontoDolar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupPanelFactura2.SuspendLayout()
        CType(Me.dtiFechaFactura, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gpDetalleVenta.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.GPanelProductos.SuspendLayout()
        Me.Panel7.SuspendLayout()
        CType(Me.grProductos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuperTabControlPanel1.SuspendLayout()
        Me.GroupPanel3.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.grVentas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelFondoDetalle.SuspendLayout()
        Me.PanelFacturaYCobranza.SuspendLayout()
        Me.PanelEncabezado.SuspendLayout()
        CType(Me.CbTipoDoc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelSuperior
        '
        Me.PanelSuperior.Controls.Add(Me.SwDescuentoProveedor)
        Me.PanelSuperior.Controls.Add(Me.LabelX3)
        Me.PanelSuperior.Controls.Add(Me.tbNit)
        Me.PanelSuperior.Controls.Add(Me.CbTipoDoc)
        Me.PanelSuperior.Controls.Add(Me.LabelX25)
        Me.PanelSuperior.Margin = New System.Windows.Forms.Padding(2)
        Me.PanelSuperior.Size = New System.Drawing.Size(1319, 72)
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
        Me.PanelSuperior.Controls.SetChildIndex(Me.PanelToolBar1, 0)
        Me.PanelSuperior.Controls.SetChildIndex(Me.PanelToolBar2, 0)
        Me.PanelSuperior.Controls.SetChildIndex(Me.MRlAccion, 0)
        Me.PanelSuperior.Controls.SetChildIndex(Me.PictureBox1, 0)
        Me.PanelSuperior.Controls.SetChildIndex(Me.LabelX25, 0)
        Me.PanelSuperior.Controls.SetChildIndex(Me.CbTipoDoc, 0)
        Me.PanelSuperior.Controls.SetChildIndex(Me.tbNit, 0)
        Me.PanelSuperior.Controls.SetChildIndex(Me.LabelX3, 0)
        Me.PanelSuperior.Controls.SetChildIndex(Me.SwDescuentoProveedor, 0)
        '
        'PanelInferior
        '
        Me.PanelInferior.Controls.Add(Me.Label1Conn)
        Me.PanelInferior.Location = New System.Drawing.Point(0, 670)
        Me.PanelInferior.Margin = New System.Windows.Forms.Padding(2)
        Me.PanelInferior.Size = New System.Drawing.Size(1319, 39)
        Me.PanelInferior.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelInferior.Style.BackColor1.Color = System.Drawing.Color.Transparent
        Me.PanelInferior.Style.BackColor2.Color = System.Drawing.Color.Transparent
        Me.PanelInferior.Style.BackgroundImage = CType(resources.GetObject("PanelInferior.Style.BackgroundImage"), System.Drawing.Image)
        Me.PanelInferior.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelInferior.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelInferior.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelInferior.Style.GradientAngle = 90
        Me.PanelInferior.Controls.SetChildIndex(Me.PanelNavegacion, 0)
        Me.PanelInferior.Controls.SetChildIndex(Me.MPanelUserAct, 0)
        Me.PanelInferior.Controls.SetChildIndex(Me.Label1Conn, 0)
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
        Me.TxtNombreUsu.Margin = New System.Windows.Forms.Padding(4)
        Me.TxtNombreUsu.ReadOnly = True
        Me.TxtNombreUsu.Size = New System.Drawing.Size(135, 32)
        Me.TxtNombreUsu.Text = "DEFAULT"
        '
        'btnSalir
        '
        '
        'btnGrabar
        '
        '
        'btnEliminar
        '
        Me.btnEliminar.Text = "ANULAR"
        '
        'btnModificar
        '
        '
        'btnNuevo
        '
        '
        'PanelToolBar2
        '
        Me.PanelToolBar2.Controls.Add(Me.btnDuplicar)
        Me.PanelToolBar2.Controls.Add(Me.btnBitacora)
        Me.PanelToolBar2.Location = New System.Drawing.Point(1059, 0)
        Me.PanelToolBar2.Margin = New System.Windows.Forms.Padding(2)
        Me.PanelToolBar2.Size = New System.Drawing.Size(260, 72)
        Me.PanelToolBar2.Controls.SetChildIndex(Me.btnImprimir, 0)
        Me.PanelToolBar2.Controls.SetChildIndex(Me.btnBitacora, 0)
        Me.PanelToolBar2.Controls.SetChildIndex(Me.btnDuplicar, 0)
        '
        'PanelPrincipal
        '
        Me.PanelPrincipal.Margin = New System.Windows.Forms.Padding(2)
        Me.PanelPrincipal.Size = New System.Drawing.Size(1319, 709)
        Me.PanelPrincipal.Controls.SetChildIndex(Me.PanelInferior, 0)
        Me.PanelPrincipal.Controls.SetChildIndex(Me.PanelUsuario, 0)
        Me.PanelPrincipal.Controls.SetChildIndex(Me.PanelSuperior, 0)
        Me.PanelPrincipal.Controls.SetChildIndex(Me.Panel1, 0)
        '
        'btnImprimir
        '
        Me.btnImprimir.Size = New System.Drawing.Size(260, 72)
        '
        'btnUltimo
        '
        Me.btnUltimo.Margin = New System.Windows.Forms.Padding(2)
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
        'MPanelUserAct
        '
        Me.MPanelUserAct.Location = New System.Drawing.Point(1119, 0)
        Me.MPanelUserAct.Margin = New System.Windows.Forms.Padding(2)
        '
        'MRlAccion
        '
        '
        '
        '
        Me.MRlAccion.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.MRlAccion.Margin = New System.Windows.Forms.Padding(2)
        Me.MRlAccion.Size = New System.Drawing.Size(683, 72)
        Me.MRlAccion.Visible = False
        '
        'PanelContent
        '
        Me.PanelContent.Controls.Add(Me.PanelEncabezado)
        Me.PanelContent.Margin = New System.Windows.Forms.Padding(2)
        Me.PanelContent.Size = New System.Drawing.Size(1286, 598)
        Me.PanelContent.TabIndex = 3
        '
        'Panel1
        '
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel1.Size = New System.Drawing.Size(1319, 598)
        '
        'MSuperTabControlPanel1
        '
        Me.MSuperTabControlPanel1.Margin = New System.Windows.Forms.Padding(2)
        Me.MSuperTabControlPanel1.Size = New System.Drawing.Size(1286, 598)
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
        Me.MSuperTabControl.Controls.Add(Me.SuperTabControlPanel1)
        Me.MSuperTabControl.Margin = New System.Windows.Forms.Padding(2)
        Me.MSuperTabControl.Size = New System.Drawing.Size(1319, 598)
        Me.MSuperTabControl.Tabs.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.SuperTabItem1})
        Me.MSuperTabControl.Controls.SetChildIndex(Me.SuperTabControlPanel1, 0)
        Me.MSuperTabControl.Controls.SetChildIndex(Me.MSuperTabControlPanel1, 0)
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(866, 0)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(4)
        '
        'GroupPanel2
        '
        Me.GroupPanel2.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel2.Controls.Add(Me.Panel2)
        Me.GroupPanel2.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupPanel2.Location = New System.Drawing.Point(0, 0)
        Me.GroupPanel2.Name = "GroupPanel2"
        Me.GroupPanel2.Size = New System.Drawing.Size(1286, 130)
        '
        '
        '
        Me.GroupPanel2.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.GroupPanel2.Style.BackColorGradientAngle = 90
        Me.GroupPanel2.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.GroupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel2.Style.BorderBottomWidth = 1
        Me.GroupPanel2.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.GroupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel2.Style.BorderLeftWidth = 1
        Me.GroupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel2.Style.BorderRightWidth = 1
        Me.GroupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel2.Style.BorderTopWidth = 1
        Me.GroupPanel2.Style.CornerDiameter = 4
        Me.GroupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanel2.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanel2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.GroupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanel2.TabIndex = 230
        Me.GroupPanel2.Text = "DATOS GENERALES"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.TbEmail)
        Me.Panel2.Controls.Add(Me.LabelX27)
        Me.Panel2.Controls.Add(Me.lbNroCaja)
        Me.Panel2.Controls.Add(Me.LabelX24)
        Me.Panel2.Controls.Add(Me.LabelX23)
        Me.Panel2.Controls.Add(Me.tbObservacion)
        Me.Panel2.Controls.Add(Me.lblObservacion)
        Me.Panel2.Controls.Add(Me.LabelX21)
        Me.Panel2.Controls.Add(Me.txtEstado)
        Me.Panel2.Controls.Add(Me.lbCredito)
        Me.Panel2.Controls.Add(Me.tbFechaVenc)
        Me.Panel2.Controls.Add(Me.LabelX1)
        Me.Panel2.Controls.Add(Me.swTipoVenta)
        Me.Panel2.Controls.Add(Me.LabelX17)
        Me.Panel2.Controls.Add(Me.QrFactura)
        Me.Panel2.Controls.Add(Me.TbNombre2)
        Me.Panel2.Controls.Add(Me.cbSucursal)
        Me.Panel2.Controls.Add(Me.tbFechaVenta)
        Me.Panel2.Controls.Add(Me.lbCtrlEnter)
        Me.Panel2.Controls.Add(Me.TbNombre1)
        Me.Panel2.Controls.Add(Me.LabelX4)
        Me.Panel2.Controls.Add(Me.LabelX10)
        Me.Panel2.Controls.Add(Me.tbVendedor)
        Me.Panel2.Controls.Add(Me.tbCliente)
        Me.Panel2.Controls.Add(Me.LabelX2)
        Me.Panel2.Controls.Add(Me.tbCodigo)
        Me.Panel2.Controls.Add(Me.lbFVenta)
        Me.Panel2.Controls.Add(Me.lbIdVenta)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1280, 107)
        Me.Panel2.TabIndex = 0
        '
        'TbEmail
        '
        Me.TbEmail.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.TbEmail.Border.Class = "TextBoxBorder"
        Me.TbEmail.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.TbEmail.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold)
        Me.TbEmail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.TbEmail.Location = New System.Drawing.Point(286, 38)
        Me.TbEmail.Name = "TbEmail"
        Me.TbEmail.PreventEnterBeep = True
        Me.TbEmail.Size = New System.Drawing.Size(225, 21)
        Me.TbEmail.TabIndex = 389
        '
        'LabelX27
        '
        Me.LabelX27.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX27.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX27.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX27.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX27.Location = New System.Drawing.Point(206, 37)
        Me.LabelX27.Name = "LabelX27"
        Me.LabelX27.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX27.Size = New System.Drawing.Size(70, 23)
        Me.LabelX27.TabIndex = 390
        Me.LabelX27.Text = "Email:"
        '
        'lbNroCaja
        '
        Me.lbNroCaja.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lbNroCaja.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.lbNroCaja.Location = New System.Drawing.Point(983, 66)
        Me.lbNroCaja.Name = "lbNroCaja"
        Me.lbNroCaja.Size = New System.Drawing.Size(60, 17)
        Me.lbNroCaja.TabIndex = 386
        Me.lbNroCaja.Text = "Label1"
        Me.lbNroCaja.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelX24
        '
        Me.LabelX24.AutoSize = True
        Me.LabelX24.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX24.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX24.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX24.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX24.Location = New System.Drawing.Point(902, 67)
        Me.LabelX24.Name = "LabelX24"
        Me.LabelX24.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX24.Size = New System.Drawing.Size(65, 16)
        Me.LabelX24.TabIndex = 385
        Me.LabelX24.Text = "Nro. Caja:"
        '
        'LabelX23
        '
        Me.LabelX23.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX23.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX23.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX23.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX23.Location = New System.Drawing.Point(6, 65)
        Me.LabelX23.Name = "LabelX23"
        Me.LabelX23.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX23.Size = New System.Drawing.Size(84, 23)
        Me.LabelX23.TabIndex = 363
        Me.LabelX23.Text = "Observación:"
        '
        'tbObservacion
        '
        Me.tbObservacion.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.tbObservacion.Border.Class = "TextBoxBorder"
        Me.tbObservacion.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbObservacion.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbObservacion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.tbObservacion.Location = New System.Drawing.Point(93, 71)
        Me.tbObservacion.Multiline = True
        Me.tbObservacion.Name = "tbObservacion"
        Me.tbObservacion.PreventEnterBeep = True
        Me.tbObservacion.Size = New System.Drawing.Size(785, 32)
        Me.tbObservacion.TabIndex = 362
        '
        'lblObservacion
        '
        Me.lblObservacion.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.lblObservacion.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblObservacion.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblObservacion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.lblObservacion.Location = New System.Drawing.Point(8, 80)
        Me.lblObservacion.Name = "lblObservacion"
        Me.lblObservacion.SingleLineColor = System.Drawing.SystemColors.Control
        Me.lblObservacion.Size = New System.Drawing.Size(60, 23)
        Me.lblObservacion.TabIndex = 361
        Me.lblObservacion.Text = "N. Fac:"
        Me.lblObservacion.Visible = False
        '
        'LabelX21
        '
        Me.LabelX21.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX21.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX21.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX21.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX21.Location = New System.Drawing.Point(809, 8)
        Me.LabelX21.Name = "LabelX21"
        Me.LabelX21.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX21.Size = New System.Drawing.Size(60, 23)
        Me.LabelX21.TabIndex = 360
        Me.LabelX21.Text = "ESTADO:"
        '
        'txtEstado
        '
        Me.txtEstado.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtEstado.Border.Class = "TextBoxBorder"
        Me.txtEstado.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtEstado.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEstado.ForeColor = System.Drawing.Color.Black
        Me.txtEstado.Location = New System.Drawing.Point(784, 35)
        Me.txtEstado.Name = "txtEstado"
        Me.txtEstado.PreventEnterBeep = True
        Me.txtEstado.Size = New System.Drawing.Size(95, 21)
        Me.txtEstado.TabIndex = 359
        Me.txtEstado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
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
        Me.lbCredito.Location = New System.Drawing.Point(885, 34)
        Me.lbCredito.Name = "lbCredito"
        Me.lbCredito.SingleLineColor = System.Drawing.SystemColors.Control
        Me.lbCredito.Size = New System.Drawing.Size(90, 23)
        Me.lbCredito.TabIndex = 358
        Me.lbCredito.Text = "Venc. Crédito:"
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
        Me.tbFechaVenc.Font = New System.Drawing.Font("Georgia", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbFechaVenc.IsPopupCalendarOpen = False
        Me.tbFechaVenc.Location = New System.Drawing.Point(986, 34)
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
        Me.tbFechaVenc.Size = New System.Drawing.Size(120, 26)
        Me.tbFechaVenc.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.tbFechaVenc.TabIndex = 0
        Me.tbFechaVenc.Visible = False
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
        Me.LabelX1.Location = New System.Drawing.Point(900, 10)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX1.Size = New System.Drawing.Size(75, 23)
        Me.LabelX1.TabIndex = 356
        Me.LabelX1.Text = "Tipo Venta:"
        '
        'swTipoVenta
        '
        '
        '
        '
        Me.swTipoVenta.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.swTipoVenta.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.swTipoVenta.Location = New System.Drawing.Point(985, 9)
        Me.swTipoVenta.Name = "swTipoVenta"
        Me.swTipoVenta.OffBackColor = System.Drawing.Color.LawnGreen
        Me.swTipoVenta.OffText = "CREDITO"
        Me.swTipoVenta.OnBackColor = System.Drawing.Color.Gold
        Me.swTipoVenta.OnText = "CONTADO"
        Me.swTipoVenta.Size = New System.Drawing.Size(120, 22)
        Me.swTipoVenta.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.swTipoVenta.TabIndex = 0
        Me.swTipoVenta.Value = True
        Me.swTipoVenta.ValueObject = "Y"
        '
        'LabelX17
        '
        Me.LabelX17.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX17.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX17.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX17.Location = New System.Drawing.Point(1137, 5)
        Me.LabelX17.Name = "LabelX17"
        Me.LabelX17.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX17.Size = New System.Drawing.Size(90, 23)
        Me.LabelX17.TabIndex = 271
        Me.LabelX17.Text = "SUCURSAL:"
        Me.LabelX17.Visible = False
        '
        'QrFactura
        '
        Me.QrFactura.ErrorCorrectLevel = Gma.QrCodeNet.Encoding.ErrorCorrectionLevel.M
        Me.QrFactura.Image = CType(resources.GetObject("QrFactura.Image"), System.Drawing.Image)
        Me.QrFactura.Location = New System.Drawing.Point(1188, 3)
        Me.QrFactura.Margin = New System.Windows.Forms.Padding(2)
        Me.QrFactura.Name = "QrFactura"
        Me.QrFactura.QuietZoneModule = Gma.QrCodeNet.Encoding.Windows.Render.QuietZoneModules.Two
        Me.QrFactura.Size = New System.Drawing.Size(95, 90)
        Me.QrFactura.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.QrFactura.TabIndex = 22
        Me.QrFactura.TabStop = False
        Me.QrFactura.Text = "QrCodeImgControl1"
        Me.QrFactura.Visible = False
        '
        'TbNombre2
        '
        Me.TbNombre2.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.TbNombre2.Border.Class = "TextBoxBorder"
        Me.TbNombre2.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.TbNombre2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TbNombre2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.TbNombre2.Location = New System.Drawing.Point(57, 80)
        Me.TbNombre2.Name = "TbNombre2"
        Me.TbNombre2.PreventEnterBeep = True
        Me.TbNombre2.Size = New System.Drawing.Size(30, 22)
        Me.TbNombre2.TabIndex = 0
        Me.TbNombre2.Visible = False
        '
        'cbSucursal
        '
        cbSucursal_DesignTimeLayout.LayoutString = resources.GetString("cbSucursal_DesignTimeLayout.LayoutString")
        Me.cbSucursal.DesignTimeLayout = cbSucursal_DesignTimeLayout
        Me.cbSucursal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbSucursal.Location = New System.Drawing.Point(1112, 36)
        Me.cbSucursal.Name = "cbSucursal"
        Me.cbSucursal.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.cbSucursal.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.cbSucursal.SelectedIndex = -1
        Me.cbSucursal.SelectedItem = Nothing
        Me.cbSucursal.Size = New System.Drawing.Size(140, 22)
        Me.cbSucursal.TabIndex = 270
        Me.cbSucursal.Visible = False
        Me.cbSucursal.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
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
        Me.tbFechaVenta.Font = New System.Drawing.Font("Georgia", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbFechaVenta.IsPopupCalendarOpen = False
        Me.tbFechaVenta.Location = New System.Drawing.Point(93, 34)
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
        Me.tbFechaVenta.Size = New System.Drawing.Size(100, 26)
        Me.tbFechaVenta.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.tbFechaVenta.TabIndex = 0
        '
        'lbCtrlEnter
        '
        Me.lbCtrlEnter.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.lbCtrlEnter.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lbCtrlEnter.Font = New System.Drawing.Font("Georgia", 7.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbCtrlEnter.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.lbCtrlEnter.Location = New System.Drawing.Point(721, 0)
        Me.lbCtrlEnter.Name = "lbCtrlEnter"
        Me.lbCtrlEnter.SingleLineColor = System.Drawing.SystemColors.Control
        Me.lbCtrlEnter.Size = New System.Drawing.Size(60, 10)
        Me.lbCtrlEnter.TabIndex = 352
        Me.lbCtrlEnter.Text = "Ctrl+Enter"
        '
        'TbNombre1
        '
        Me.TbNombre1.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.TbNombre1.Border.Class = "TextBoxBorder"
        Me.TbNombre1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.TbNombre1.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TbNombre1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.TbNombre1.Location = New System.Drawing.Point(285, 8)
        Me.TbNombre1.Name = "TbNombre1"
        Me.TbNombre1.PreventEnterBeep = True
        Me.TbNombre1.Size = New System.Drawing.Size(225, 21)
        Me.TbNombre1.TabIndex = 2
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
        Me.LabelX4.Location = New System.Drawing.Point(195, 7)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX4.Size = New System.Drawing.Size(90, 23)
        Me.LabelX4.TabIndex = 279
        Me.LabelX4.Text = "Razon Social:"
        '
        'LabelX10
        '
        Me.LabelX10.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX10.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX10.Location = New System.Drawing.Point(520, 36)
        Me.LabelX10.Name = "LabelX10"
        Me.LabelX10.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX10.Size = New System.Drawing.Size(80, 23)
        Me.LabelX10.TabIndex = 269
        Me.LabelX10.Text = "Vendedor:"
        '
        'tbVendedor
        '
        Me.tbVendedor.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.tbVendedor.Border.Class = "TextBoxBorder"
        Me.tbVendedor.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbVendedor.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbVendedor.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.tbVendedor.Location = New System.Drawing.Point(601, 37)
        Me.tbVendedor.Name = "tbVendedor"
        Me.tbVendedor.PreventEnterBeep = True
        Me.tbVendedor.Size = New System.Drawing.Size(180, 21)
        Me.tbVendedor.TabIndex = 0
        '
        'tbCliente
        '
        Me.tbCliente.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.tbCliente.Border.Class = "TextBoxBorder"
        Me.tbCliente.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbCliente.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbCliente.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.tbCliente.Location = New System.Drawing.Point(601, 10)
        Me.tbCliente.Name = "tbCliente"
        Me.tbCliente.PreventEnterBeep = True
        Me.tbCliente.Size = New System.Drawing.Size(180, 21)
        Me.tbCliente.TabIndex = 0
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
        Me.LabelX2.Location = New System.Drawing.Point(520, 9)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX2.Size = New System.Drawing.Size(60, 23)
        Me.LabelX2.TabIndex = 268
        Me.LabelX2.Text = "Cliente:"
        '
        'tbCodigo
        '
        Me.tbCodigo.BackColor = System.Drawing.Color.LightGray
        '
        '
        '
        Me.tbCodigo.Border.Class = "TextBoxBorder"
        Me.tbCodigo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbCodigo.DisabledBackColor = System.Drawing.Color.White
        Me.tbCodigo.Enabled = False
        Me.tbCodigo.Font = New System.Drawing.Font("Georgia", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbCodigo.ForeColor = System.Drawing.Color.Black
        Me.tbCodigo.Location = New System.Drawing.Point(93, 4)
        Me.tbCodigo.Name = "tbCodigo"
        Me.tbCodigo.PreventEnterBeep = True
        Me.tbCodigo.Size = New System.Drawing.Size(80, 26)
        Me.tbCodigo.TabIndex = 0
        Me.tbCodigo.TabStop = False
        '
        'lbFVenta
        '
        Me.lbFVenta.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.lbFVenta.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lbFVenta.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbFVenta.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.lbFVenta.Location = New System.Drawing.Point(6, 36)
        Me.lbFVenta.Name = "lbFVenta"
        Me.lbFVenta.SingleLineColor = System.Drawing.SystemColors.Control
        Me.lbFVenta.Size = New System.Drawing.Size(88, 23)
        Me.lbFVenta.TabIndex = 263
        Me.lbFVenta.Text = "Fecha Venta:"
        '
        'lbIdVenta
        '
        Me.lbIdVenta.AutoSize = True
        Me.lbIdVenta.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.lbIdVenta.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lbIdVenta.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbIdVenta.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.lbIdVenta.Location = New System.Drawing.Point(9, 9)
        Me.lbIdVenta.Name = "lbIdVenta"
        Me.lbIdVenta.SingleLineColor = System.Drawing.SystemColors.Control
        Me.lbIdVenta.Size = New System.Drawing.Size(59, 16)
        Me.lbIdVenta.TabIndex = 262
        Me.lbIdVenta.Text = "Id Venta:"
        '
        'tbNit
        '
        Me.tbNit.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.tbNit.Border.Class = "TextBoxBorder"
        Me.tbNit.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbNit.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbNit.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.tbNit.Location = New System.Drawing.Point(556, 12)
        Me.tbNit.Name = "tbNit"
        Me.tbNit.PreventEnterBeep = True
        Me.tbNit.Size = New System.Drawing.Size(278, 21)
        Me.tbNit.TabIndex = 1
        '
        'LabelX3
        '
        Me.LabelX3.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX3.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX3.ForeColor = System.Drawing.Color.White
        Me.LabelX3.Location = New System.Drawing.Point(438, 10)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX3.Size = New System.Drawing.Size(60, 23)
        Me.LabelX3.TabIndex = 277
        Me.LabelX3.Text = "Nit/Ci:"
        '
        'GroupCobranza
        '
        Me.GroupCobranza.BackColor = System.Drawing.Color.White
        Me.GroupCobranza.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupCobranza.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupCobranza.Controls.Add(Me.tbMdesc)
        Me.GroupCobranza.Controls.Add(Me.tbSubTotal)
        Me.GroupCobranza.Controls.Add(Me.txtMontoPagado1)
        Me.GroupCobranza.Controls.Add(Me.txtCambio1)
        Me.GroupCobranza.Controls.Add(Me.LabelX22)
        Me.GroupCobranza.Controls.Add(Me.tbTotalDo)
        Me.GroupCobranza.Controls.Add(Me.LabelX13)
        Me.GroupCobranza.Controls.Add(Me.LabelX20)
        Me.GroupCobranza.Controls.Add(Me.LabelX19)
        Me.GroupCobranza.Controls.Add(Me.tbTotalBs)
        Me.GroupCobranza.Controls.Add(Me.LabelX18)
        Me.GroupCobranza.Controls.Add(Me.LabelX12)
        Me.GroupCobranza.Controls.Add(Me.cbCambioDolar)
        Me.GroupCobranza.Controls.Add(Me.btgrupo1)
        Me.GroupCobranza.Controls.Add(Me.txtCambio)
        Me.GroupCobranza.Controls.Add(Me.txtMontoPagado)
        Me.GroupCobranza.Controls.Add(Me.lbCambio)
        Me.GroupCobranza.Controls.Add(Me.lbMontoPagado)
        Me.GroupCobranza.Controls.Add(Me.tbIce)
        Me.GroupCobranza.Controls.Add(Me.lbIce)
        Me.GroupCobranza.Controls.Add(Me.tbPrueba)
        Me.GroupCobranza.Controls.Add(Me.LabelX8)
        Me.GroupCobranza.Controls.Add(Me.LabelX11)
        Me.GroupCobranza.Controls.Add(Me.tbPdesc)
        Me.GroupCobranza.Controls.Add(Me.LabelX9)
        Me.GroupCobranza.Controls.Add(Me.GroupBox1)
        Me.GroupCobranza.Controls.Add(Me.GroupBox2)
        Me.GroupCobranza.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupCobranza.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupCobranza.Font = New System.Drawing.Font("Georgia", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupCobranza.Location = New System.Drawing.Point(0, 115)
        Me.GroupCobranza.Name = "GroupCobranza"
        Me.GroupCobranza.Size = New System.Drawing.Size(425, 353)
        '
        '
        '
        Me.GroupCobranza.Style.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.GroupCobranza.Style.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.GroupCobranza.Style.BackColorGradientAngle = 90
        Me.GroupCobranza.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupCobranza.Style.BorderBottomWidth = 1
        Me.GroupCobranza.Style.BorderColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.GroupCobranza.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupCobranza.Style.BorderLeftWidth = 1
        Me.GroupCobranza.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupCobranza.Style.BorderRightWidth = 1
        Me.GroupCobranza.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupCobranza.Style.BorderTopWidth = 1
        Me.GroupCobranza.Style.CornerDiameter = 4
        Me.GroupCobranza.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupCobranza.Style.Font = New System.Drawing.Font("Georgia", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupCobranza.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupCobranza.Style.TextColor = System.Drawing.Color.White
        Me.GroupCobranza.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupCobranza.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupCobranza.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupCobranza.TabIndex = 0
        '
        'tbMdesc
        '
        '
        '
        '
        Me.tbMdesc.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbMdesc.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbMdesc.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.tbMdesc.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbMdesc.ForeColor = System.Drawing.Color.Black
        Me.tbMdesc.Increment = 1.0R
        Me.tbMdesc.Location = New System.Drawing.Point(219, 65)
        Me.tbMdesc.MinValue = 0R
        Me.tbMdesc.Name = "tbMdesc"
        Me.tbMdesc.Size = New System.Drawing.Size(200, 26)
        Me.tbMdesc.TabIndex = 410
        Me.tbMdesc.WatermarkAlignment = DevComponents.Editors.eTextAlignment.Right
        '
        'tbSubTotal
        '
        '
        '
        '
        Me.tbSubTotal.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbSubTotal.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbSubTotal.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.tbSubTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbSubTotal.ForeColor = System.Drawing.Color.Black
        Me.tbSubTotal.Increment = 1.0R
        Me.tbSubTotal.Location = New System.Drawing.Point(219, 35)
        Me.tbSubTotal.MinValue = 0R
        Me.tbSubTotal.Name = "tbSubTotal"
        Me.tbSubTotal.Size = New System.Drawing.Size(200, 26)
        Me.tbSubTotal.TabIndex = 409
        Me.tbSubTotal.WatermarkAlignment = DevComponents.Editors.eTextAlignment.Right
        '
        'txtMontoPagado1
        '
        Me.txtMontoPagado1.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtMontoPagado1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtMontoPagado1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMontoPagado1.ForeColor = System.Drawing.Color.Red
        Me.txtMontoPagado1.Location = New System.Drawing.Point(94, 276)
        Me.txtMontoPagado1.Name = "txtMontoPagado1"
        Me.txtMontoPagado1.SingleLineColor = System.Drawing.SystemColors.Control
        Me.txtMontoPagado1.Size = New System.Drawing.Size(120, 40)
        Me.txtMontoPagado1.Style = DevComponents.DotNetBar.eDotNetBarStyle.VS2005
        Me.txtMontoPagado1.TabIndex = 365
        Me.txtMontoPagado1.Text = "0.00"
        Me.txtMontoPagado1.TextAlignment = System.Drawing.StringAlignment.Far
        '
        'txtCambio1
        '
        Me.txtCambio1.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtCambio1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtCambio1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCambio1.ForeColor = System.Drawing.Color.Red
        Me.txtCambio1.Location = New System.Drawing.Point(302, 275)
        Me.txtCambio1.Name = "txtCambio1"
        Me.txtCambio1.SingleLineColor = System.Drawing.SystemColors.Control
        Me.txtCambio1.Size = New System.Drawing.Size(110, 40)
        Me.txtCambio1.TabIndex = 364
        Me.txtCambio1.Text = "0.00"
        Me.txtCambio1.TextAlignment = System.Drawing.StringAlignment.Far
        '
        'LabelX22
        '
        Me.LabelX22.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.LabelX22.BackgroundStyle.BackColor2 = System.Drawing.SystemColors.Highlight
        Me.LabelX22.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.LabelX22.BackgroundStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.LabelX22.BackgroundStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.LabelX22.BackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.LabelX22.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX22.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX22.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.LabelX22.Location = New System.Drawing.Point(101, 124)
        Me.LabelX22.Name = "LabelX22"
        Me.LabelX22.SingleLineColor = System.Drawing.Color.Black
        Me.LabelX22.Size = New System.Drawing.Size(115, 25)
        Me.LabelX22.TabIndex = 359
        Me.LabelX22.Text = "US$:"
        Me.LabelX22.TextAlignment = System.Drawing.StringAlignment.Far
        '
        'tbTotalDo
        '
        Me.tbTotalDo.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.tbTotalDo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbTotalDo.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbTotalDo.ForeColor = System.Drawing.Color.Red
        Me.tbTotalDo.Location = New System.Drawing.Point(219, 124)
        Me.tbTotalDo.Name = "tbTotalDo"
        Me.tbTotalDo.SingleLineColor = System.Drawing.SystemColors.Control
        Me.tbTotalDo.Size = New System.Drawing.Size(200, 25)
        Me.tbTotalDo.TabIndex = 358
        Me.tbTotalDo.Text = "0.00"
        Me.tbTotalDo.TextAlignment = System.Drawing.StringAlignment.Far
        '
        'LabelX13
        '
        Me.LabelX13.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.LabelX13.BackgroundStyle.BackColor2 = System.Drawing.SystemColors.Highlight
        Me.LabelX13.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.LabelX13.BackgroundStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.LabelX13.BackgroundStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.LabelX13.BackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.LabelX13.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX13.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.LabelX13.Location = New System.Drawing.Point(101, 95)
        Me.LabelX13.Name = "LabelX13"
        Me.LabelX13.SingleLineColor = System.Drawing.Color.Black
        Me.LabelX13.Size = New System.Drawing.Size(115, 25)
        Me.LabelX13.TabIndex = 52
        Me.LabelX13.Text = "BS:"
        Me.LabelX13.TextAlignment = System.Drawing.StringAlignment.Far
        '
        'LabelX20
        '
        Me.LabelX20.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX20.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX20.Font = New System.Drawing.Font("Georgia", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelX20.ForeColor = System.Drawing.Color.White
        Me.LabelX20.Location = New System.Drawing.Point(4, 10)
        Me.LabelX20.Name = "LabelX20"
        Me.LabelX20.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX20.Size = New System.Drawing.Size(140, 18)
        Me.LabelX20.TabIndex = 357
        Me.LabelX20.Text = "TIPO DE CAMBIO:"
        '
        'LabelX19
        '
        Me.LabelX19.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX19.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX19.Font = New System.Drawing.Font("Georgia", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX19.ForeColor = System.Drawing.Color.White
        Me.LabelX19.Location = New System.Drawing.Point(3, 96)
        Me.LabelX19.Name = "LabelX19"
        Me.LabelX19.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX19.Size = New System.Drawing.Size(100, 60)
        Me.LabelX19.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010
        Me.LabelX19.TabIndex = 356
        Me.LabelX19.Text = "TOTAL:"
        '
        'tbTotalBs
        '
        Me.tbTotalBs.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.tbTotalBs.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbTotalBs.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbTotalBs.ForeColor = System.Drawing.Color.Red
        Me.tbTotalBs.Location = New System.Drawing.Point(219, 95)
        Me.tbTotalBs.Name = "tbTotalBs"
        Me.tbTotalBs.SingleLineColor = System.Drawing.SystemColors.Control
        Me.tbTotalBs.Size = New System.Drawing.Size(200, 25)
        Me.tbTotalBs.TabIndex = 355
        Me.tbTotalBs.Text = "0.00"
        Me.tbTotalBs.TextAlignment = System.Drawing.StringAlignment.Far
        '
        'LabelX18
        '
        Me.LabelX18.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX18.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX18.Font = New System.Drawing.Font("Georgia", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX18.ForeColor = System.Drawing.Color.White
        Me.LabelX18.Location = New System.Drawing.Point(7, 327)
        Me.LabelX18.Name = "LabelX18"
        Me.LabelX18.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX18.Size = New System.Drawing.Size(200, 15)
        Me.LabelX18.TabIndex = 354
        Me.LabelX18.Text = "Ctrl+S para Cobrar"
        '
        'LabelX12
        '
        Me.LabelX12.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX12.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX12.Font = New System.Drawing.Font("Georgia", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX12.ForeColor = System.Drawing.Color.White
        Me.LabelX12.Location = New System.Drawing.Point(222, 327)
        Me.LabelX12.Name = "LabelX12"
        Me.LabelX12.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX12.Size = New System.Drawing.Size(200, 15)
        Me.LabelX12.TabIndex = 353
        Me.LabelX12.Text = "Ctrl+A para Guardar"
        '
        'cbCambioDolar
        '
        Me.cbCambioDolar.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat
        Me.cbCambioDolar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        cbCambioDolar_DesignTimeLayout.LayoutString = resources.GetString("cbCambioDolar_DesignTimeLayout.LayoutString")
        Me.cbCambioDolar.DesignTimeLayout = cbCambioDolar_DesignTimeLayout
        Me.cbCambioDolar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbCambioDolar.Location = New System.Drawing.Point(219, 7)
        Me.cbCambioDolar.Name = "cbCambioDolar"
        Me.cbCambioDolar.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.cbCambioDolar.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.cbCambioDolar.SelectedIndex = -1
        Me.cbCambioDolar.SelectedItem = Nothing
        Me.cbCambioDolar.Size = New System.Drawing.Size(144, 22)
        Me.cbCambioDolar.TabIndex = 8
        Me.cbCambioDolar.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'btgrupo1
        '
        Me.btgrupo1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btgrupo1.BackColor = System.Drawing.Color.Transparent
        Me.btgrupo1.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat
        Me.btgrupo1.Image = Global.DinoM.My.Resources.Resources.add
        Me.btgrupo1.ImageFixedSize = New System.Drawing.Size(25, 23)
        Me.btgrupo1.Location = New System.Drawing.Point(371, 6)
        Me.btgrupo1.Name = "btgrupo1"
        Me.btgrupo1.Size = New System.Drawing.Size(28, 23)
        Me.btgrupo1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btgrupo1.TabIndex = 282
        Me.btgrupo1.Visible = False
        '
        'txtCambio
        '
        '
        '
        '
        Me.txtCambio.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.txtCambio.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtCambio.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.txtCambio.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCambio.Increment = 1.0R
        Me.txtCambio.Location = New System.Drawing.Point(302, 295)
        Me.txtCambio.LockUpdateChecked = False
        Me.txtCambio.MinValue = 0R
        Me.txtCambio.Name = "txtCambio"
        Me.txtCambio.Size = New System.Drawing.Size(110, 22)
        Me.txtCambio.TabIndex = 7
        Me.txtCambio.Visible = False
        Me.txtCambio.WatermarkAlignment = DevComponents.Editors.eTextAlignment.Right
        '
        'txtMontoPagado
        '
        '
        '
        '
        Me.txtMontoPagado.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.txtMontoPagado.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtMontoPagado.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.txtMontoPagado.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMontoPagado.Increment = 1.0R
        Me.txtMontoPagado.Location = New System.Drawing.Point(94, 295)
        Me.txtMontoPagado.LockUpdateChecked = False
        Me.txtMontoPagado.MinValue = 0R
        Me.txtMontoPagado.Name = "txtMontoPagado"
        Me.txtMontoPagado.Size = New System.Drawing.Size(120, 22)
        Me.txtMontoPagado.TabIndex = 6
        Me.txtMontoPagado.Visible = False
        Me.txtMontoPagado.WatermarkAlignment = DevComponents.Editors.eTextAlignment.Right
        '
        'lbCambio
        '
        Me.lbCambio.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.lbCambio.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lbCambio.Font = New System.Drawing.Font("Georgia", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbCambio.ForeColor = System.Drawing.Color.White
        Me.lbCambio.Location = New System.Drawing.Point(223, 281)
        Me.lbCambio.Name = "lbCambio"
        Me.lbCambio.SingleLineColor = System.Drawing.SystemColors.Control
        Me.lbCambio.Size = New System.Drawing.Size(65, 18)
        Me.lbCambio.TabIndex = 59
        Me.lbCambio.Text = "Cambio:"
        '
        'lbMontoPagado
        '
        Me.lbMontoPagado.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.lbMontoPagado.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lbMontoPagado.Font = New System.Drawing.Font("Georgia", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbMontoPagado.ForeColor = System.Drawing.Color.White
        Me.lbMontoPagado.Location = New System.Drawing.Point(6, 282)
        Me.lbMontoPagado.Name = "lbMontoPagado"
        Me.lbMontoPagado.SingleLineColor = System.Drawing.SystemColors.Control
        Me.lbMontoPagado.Size = New System.Drawing.Size(101, 18)
        Me.lbMontoPagado.TabIndex = 58
        Me.lbMontoPagado.Text = "M.Pagado:"
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
        Me.tbIce.Location = New System.Drawing.Point(342, 96)
        Me.tbIce.MinValue = 0R
        Me.tbIce.Name = "tbIce"
        Me.tbIce.Size = New System.Drawing.Size(80, 21)
        Me.tbIce.TabIndex = 44
        Me.tbIce.Visible = False
        Me.tbIce.WatermarkAlignment = DevComponents.Editors.eTextAlignment.Right
        '
        'lbIce
        '
        Me.lbIce.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.lbIce.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lbIce.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbIce.ForeColor = System.Drawing.Color.White
        Me.lbIce.Location = New System.Drawing.Point(230, 102)
        Me.lbIce.Name = "lbIce"
        Me.lbIce.SingleLineColor = System.Drawing.SystemColors.Control
        Me.lbIce.Size = New System.Drawing.Size(44, 18)
        Me.lbIce.TabIndex = 51
        Me.lbIce.Text = "ICE:"
        Me.lbIce.Visible = False
        '
        'tbPrueba
        '
        '
        '
        '
        Me.tbPrueba.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbPrueba.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.tbPrueba.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPrueba.Increment = 1.0R
        Me.tbPrueba.Location = New System.Drawing.Point(326, 116)
        Me.tbPrueba.MinValue = 0R
        Me.tbPrueba.Name = "tbPrueba"
        Me.tbPrueba.Size = New System.Drawing.Size(100, 21)
        Me.tbPrueba.TabIndex = 45
        Me.tbPrueba.Visible = False
        Me.tbPrueba.WatermarkAlignment = DevComponents.Editors.eTextAlignment.Right
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
        Me.LabelX8.Location = New System.Drawing.Point(238, 89)
        Me.LabelX8.Name = "LabelX8"
        Me.LabelX8.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX8.Size = New System.Drawing.Size(86, 18)
        Me.LabelX8.TabIndex = 48
        Me.LabelX8.Text = "P. Descuento:"
        Me.LabelX8.Visible = False
        '
        'LabelX11
        '
        Me.LabelX11.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX11.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX11.Font = New System.Drawing.Font("Georgia", 16.25!, System.Drawing.FontStyle.Bold)
        Me.LabelX11.FontBold = True
        Me.LabelX11.ForeColor = System.Drawing.Color.White
        Me.LabelX11.Location = New System.Drawing.Point(3, 38)
        Me.LabelX11.Name = "LabelX11"
        Me.LabelX11.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX11.Size = New System.Drawing.Size(210, 18)
        Me.LabelX11.TabIndex = 50
        Me.LabelX11.Text = "SUBTOTAL BS.:"
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
        Me.tbPdesc.Location = New System.Drawing.Point(326, 86)
        Me.tbPdesc.LockUpdateChecked = False
        Me.tbPdesc.MinValue = 0R
        Me.tbPdesc.Name = "tbPdesc"
        Me.tbPdesc.Size = New System.Drawing.Size(100, 21)
        Me.tbPdesc.TabIndex = 46
        Me.tbPdesc.Visible = False
        Me.tbPdesc.WatermarkAlignment = DevComponents.Editors.eTextAlignment.Right
        '
        'LabelX9
        '
        Me.LabelX9.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX9.Font = New System.Drawing.Font("Georgia", 16.25!, System.Drawing.FontStyle.Bold)
        Me.LabelX9.ForeColor = System.Drawing.Color.White
        Me.LabelX9.Location = New System.Drawing.Point(2, 67)
        Me.LabelX9.Name = "LabelX9"
        Me.LabelX9.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX9.Size = New System.Drawing.Size(210, 18)
        Me.LabelX9.TabIndex = 49
        Me.LabelX9.Text = "DESCUENTO BS.:"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.lbEjemplo)
        Me.GroupBox1.Controls.Add(Me.tbNroTarjeta)
        Me.GroupBox1.Controls.Add(Me.lbNroTarjeta)
        Me.GroupBox1.Controls.Add(Me.tbMontoTarej)
        Me.GroupBox1.Controls.Add(Me.chbTarjeta)
        Me.GroupBox1.Controls.Add(Me.tbMontoBs)
        Me.GroupBox1.Controls.Add(Me.LabelX5)
        Me.GroupBox1.Controls.Add(Me.tbMontoDolar)
        Me.GroupBox1.Controls.Add(Me.LabelX6)
        Me.GroupBox1.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.Gold
        Me.GroupBox1.Location = New System.Drawing.Point(1, 154)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(415, 105)
        Me.GroupBox1.TabIndex = 361
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Efectivo:"
        '
        'tbNroTarjeta
        '
        Me.tbNroTarjeta.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.tbNroTarjeta.Border.Class = "TextBoxBorder"
        Me.tbNroTarjeta.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbNroTarjeta.Font = New System.Drawing.Font("Georgia", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbNroTarjeta.ForeColor = System.Drawing.Color.Black
        Me.tbNroTarjeta.Location = New System.Drawing.Point(93, 74)
        Me.tbNroTarjeta.MaxLength = 16
        Me.tbNroTarjeta.Name = "tbNroTarjeta"
        Me.tbNroTarjeta.PreventEnterBeep = True
        Me.tbNroTarjeta.Size = New System.Drawing.Size(180, 24)
        Me.tbNroTarjeta.TabIndex = 375
        Me.tbNroTarjeta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbNroTarjeta
        '
        Me.lbNroTarjeta.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.lbNroTarjeta.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lbNroTarjeta.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbNroTarjeta.ForeColor = System.Drawing.Color.White
        Me.lbNroTarjeta.Location = New System.Drawing.Point(6, 78)
        Me.lbNroTarjeta.Name = "lbNroTarjeta"
        Me.lbNroTarjeta.SingleLineColor = System.Drawing.SystemColors.Control
        Me.lbNroTarjeta.Size = New System.Drawing.Size(69, 18)
        Me.lbNroTarjeta.TabIndex = 374
        Me.lbNroTarjeta.Text = "Nro. Tarj:"
        '
        'tbMontoTarej
        '
        '
        '
        '
        Me.tbMontoTarej.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbMontoTarej.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbMontoTarej.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.tbMontoTarej.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbMontoTarej.ForeColor = System.Drawing.Color.Black
        Me.tbMontoTarej.Increment = 1.0R
        Me.tbMontoTarej.Location = New System.Drawing.Point(93, 44)
        Me.tbMontoTarej.MinValue = 0R
        Me.tbMontoTarej.Name = "tbMontoTarej"
        Me.tbMontoTarej.Size = New System.Drawing.Size(120, 26)
        Me.tbMontoTarej.TabIndex = 5
        Me.tbMontoTarej.WatermarkAlignment = DevComponents.Editors.eTextAlignment.Right
        '
        'chbTarjeta
        '
        Me.chbTarjeta.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.chbTarjeta.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chbTarjeta.Font = New System.Drawing.Font("Georgia", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbTarjeta.Location = New System.Drawing.Point(4, 43)
        Me.chbTarjeta.Name = "chbTarjeta"
        Me.chbTarjeta.Size = New System.Drawing.Size(87, 30)
        Me.chbTarjeta.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.chbTarjeta.TabIndex = 279
        Me.chbTarjeta.Text = "Tarjeta:"
        Me.chbTarjeta.TextColor = System.Drawing.Color.White
        '
        'tbMontoBs
        '
        '
        '
        '
        Me.tbMontoBs.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbMontoBs.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbMontoBs.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.tbMontoBs.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbMontoBs.ForeColor = System.Drawing.Color.Black
        Me.tbMontoBs.Increment = 1.0R
        Me.tbMontoBs.Location = New System.Drawing.Point(93, 14)
        Me.tbMontoBs.MinValue = 0R
        Me.tbMontoBs.Name = "tbMontoBs"
        Me.tbMontoBs.Size = New System.Drawing.Size(120, 26)
        Me.tbMontoBs.TabIndex = 3
        Me.tbMontoBs.WatermarkAlignment = DevComponents.Editors.eTextAlignment.Right
        '
        'LabelX5
        '
        Me.LabelX5.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX5.Font = New System.Drawing.Font("Georgia", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX5.ForeColor = System.Drawing.Color.White
        Me.LabelX5.Location = New System.Drawing.Point(3, 17)
        Me.LabelX5.Name = "LabelX5"
        Me.LabelX5.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX5.Size = New System.Drawing.Size(85, 18)
        Me.LabelX5.TabIndex = 64
        Me.LabelX5.Text = "M.Pagado:"
        '
        'tbMontoDolar
        '
        '
        '
        '
        Me.tbMontoDolar.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbMontoDolar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbMontoDolar.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.tbMontoDolar.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbMontoDolar.ForeColor = System.Drawing.Color.Black
        Me.tbMontoDolar.Increment = 1.0R
        Me.tbMontoDolar.Location = New System.Drawing.Point(300, 15)
        Me.tbMontoDolar.MinValue = 0R
        Me.tbMontoDolar.Name = "tbMontoDolar"
        Me.tbMontoDolar.Size = New System.Drawing.Size(110, 26)
        Me.tbMontoDolar.TabIndex = 4
        Me.tbMontoDolar.WatermarkAlignment = DevComponents.Editors.eTextAlignment.Right
        '
        'LabelX6
        '
        Me.LabelX6.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX6.Font = New System.Drawing.Font("Georgia", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX6.ForeColor = System.Drawing.Color.White
        Me.LabelX6.Location = New System.Drawing.Point(222, 18)
        Me.LabelX6.Name = "LabelX6"
        Me.LabelX6.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX6.Size = New System.Drawing.Size(70, 18)
        Me.LabelX6.TabIndex = 66
        Me.LabelX6.Text = "Mont. $:"
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.Gold
        Me.GroupBox2.Location = New System.Drawing.Point(1, 260)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(415, 60)
        Me.GroupBox2.TabIndex = 363
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Monto Pagado/Cambio:"
        '
        'GroupPanelFactura2
        '
        Me.GroupPanelFactura2.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanelFactura2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanelFactura2.Controls.Add(Me.tbCodigoControl)
        Me.GroupPanelFactura2.Controls.Add(Me.LabelX16)
        Me.GroupPanelFactura2.Controls.Add(Me.dtiFechaFactura)
        Me.GroupPanelFactura2.Controls.Add(Me.tbNroFactura)
        Me.GroupPanelFactura2.Controls.Add(Me.tbNroAutoriz)
        Me.GroupPanelFactura2.Controls.Add(Me.LabelX15)
        Me.GroupPanelFactura2.Controls.Add(Me.LabelX14)
        Me.GroupPanelFactura2.Controls.Add(Me.LabelX7)
        Me.GroupPanelFactura2.Controls.Add(Me.swMoneda)
        Me.GroupPanelFactura2.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanelFactura2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupPanelFactura2.Location = New System.Drawing.Point(0, 0)
        Me.GroupPanelFactura2.Name = "GroupPanelFactura2"
        Me.GroupPanelFactura2.Size = New System.Drawing.Size(425, 115)
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
        Me.GroupPanelFactura2.TabIndex = 237
        Me.GroupPanelFactura2.Text = "DATOS FACTURACIÓN"
        '
        'tbCodigoControl
        '
        '
        '
        '
        Me.tbCodigoControl.Border.Class = "TextBoxBorder"
        Me.tbCodigoControl.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbCodigoControl.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbCodigoControl.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.tbCodigoControl.Location = New System.Drawing.Point(316, 58)
        Me.tbCodigoControl.Name = "tbCodigoControl"
        Me.tbCodigoControl.PreventEnterBeep = True
        Me.tbCodigoControl.Size = New System.Drawing.Size(100, 22)
        Me.tbCodigoControl.TabIndex = 243
        Me.tbCodigoControl.TabStop = False
        Me.tbCodigoControl.Tag = "70"
        Me.tbCodigoControl.Visible = False
        '
        'LabelX16
        '
        Me.LabelX16.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX16.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX16.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX16.Location = New System.Drawing.Point(260, 60)
        Me.LabelX16.Name = "LabelX16"
        Me.LabelX16.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX16.Size = New System.Drawing.Size(50, 23)
        Me.LabelX16.TabIndex = 242
        Me.LabelX16.Tag = "70"
        Me.LabelX16.Text = "Código de Control:"
        Me.LabelX16.Visible = False
        '
        'dtiFechaFactura
        '
        '
        '
        '
        Me.dtiFechaFactura.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.dtiFechaFactura.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtiFechaFactura.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown
        Me.dtiFechaFactura.ButtonDropDown.Visible = True
        Me.dtiFechaFactura.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtiFechaFactura.IsPopupCalendarOpen = False
        Me.dtiFechaFactura.Location = New System.Drawing.Point(139, 32)
        '
        '
        '
        '
        '
        '
        Me.dtiFechaFactura.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtiFechaFactura.MonthCalendar.CalendarDimensions = New System.Drawing.Size(1, 1)
        Me.dtiFechaFactura.MonthCalendar.ClearButtonVisible = True
        '
        '
        '
        Me.dtiFechaFactura.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.dtiFechaFactura.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90
        Me.dtiFechaFactura.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.dtiFechaFactura.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.dtiFechaFactura.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.dtiFechaFactura.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1
        Me.dtiFechaFactura.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtiFechaFactura.MonthCalendar.DisplayMonth = New Date(2017, 2, 1, 0, 0, 0, 0)
        Me.dtiFechaFactura.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday
        '
        '
        '
        Me.dtiFechaFactura.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.dtiFechaFactura.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.dtiFechaFactura.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.dtiFechaFactura.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtiFechaFactura.MonthCalendar.TodayButtonVisible = True
        Me.dtiFechaFactura.Name = "dtiFechaFactura"
        Me.dtiFechaFactura.Size = New System.Drawing.Size(120, 22)
        Me.dtiFechaFactura.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.dtiFechaFactura.TabIndex = 241
        Me.dtiFechaFactura.TabStop = False
        '
        'tbNroFactura
        '
        '
        '
        '
        Me.tbNroFactura.Border.Class = "TextBoxBorder"
        Me.tbNroFactura.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbNroFactura.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbNroFactura.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.tbNroFactura.Location = New System.Drawing.Point(139, 62)
        Me.tbNroFactura.Name = "tbNroFactura"
        Me.tbNroFactura.PreventEnterBeep = True
        Me.tbNroFactura.Size = New System.Drawing.Size(120, 22)
        Me.tbNroFactura.TabIndex = 240
        Me.tbNroFactura.TabStop = False
        Me.tbNroFactura.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbNroAutoriz
        '
        '
        '
        '
        Me.tbNroAutoriz.Border.Class = "TextBoxBorder"
        Me.tbNroAutoriz.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbNroAutoriz.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbNroAutoriz.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.tbNroAutoriz.Location = New System.Drawing.Point(139, 4)
        Me.tbNroAutoriz.Name = "tbNroAutoriz"
        Me.tbNroAutoriz.PreventEnterBeep = True
        Me.tbNroAutoriz.Size = New System.Drawing.Size(200, 22)
        Me.tbNroAutoriz.TabIndex = 239
        Me.tbNroAutoriz.TabStop = False
        '
        'LabelX15
        '
        Me.LabelX15.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX15.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX15.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX15.Location = New System.Drawing.Point(3, 61)
        Me.LabelX15.Name = "LabelX15"
        Me.LabelX15.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX15.Size = New System.Drawing.Size(130, 23)
        Me.LabelX15.TabIndex = 237
        Me.LabelX15.Text = "Nro. Factura:"
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
        Me.LabelX14.Location = New System.Drawing.Point(3, 32)
        Me.LabelX14.Name = "LabelX14"
        Me.LabelX14.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX14.Size = New System.Drawing.Size(130, 23)
        Me.LabelX14.TabIndex = 233
        Me.LabelX14.Text = "Fecha Factura:"
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
        Me.LabelX7.Location = New System.Drawing.Point(3, 3)
        Me.LabelX7.Name = "LabelX7"
        Me.LabelX7.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX7.Size = New System.Drawing.Size(130, 23)
        Me.LabelX7.TabIndex = 232
        Me.LabelX7.Text = "Nro de Autorización:"
        '
        'swMoneda
        '
        '
        '
        '
        Me.swMoneda.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.swMoneda.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.swMoneda.Location = New System.Drawing.Point(356, 4)
        Me.swMoneda.Name = "swMoneda"
        Me.swMoneda.OffBackColor = System.Drawing.Color.LawnGreen
        Me.swMoneda.OffText = "DOLAR"
        Me.swMoneda.OnBackColor = System.Drawing.Color.Gold
        Me.swMoneda.OnText = "BOLIVIANO"
        Me.swMoneda.Size = New System.Drawing.Size(50, 22)
        Me.swMoneda.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.swMoneda.TabIndex = 272
        Me.swMoneda.Value = True
        Me.swMoneda.ValueObject = "Y"
        Me.swMoneda.Visible = False
        '
        'gpDetalleVenta
        '
        Me.gpDetalleVenta.BackColor = System.Drawing.Color.White
        Me.gpDetalleVenta.CanvasColor = System.Drawing.SystemColors.Control
        Me.gpDetalleVenta.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.gpDetalleVenta.Controls.Add(Me.Panel5)
        Me.gpDetalleVenta.DisabledBackColor = System.Drawing.Color.Empty
        Me.gpDetalleVenta.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gpDetalleVenta.Font = New System.Drawing.Font("Georgia", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gpDetalleVenta.Location = New System.Drawing.Point(0, 0)
        Me.gpDetalleVenta.Name = "gpDetalleVenta"
        Me.gpDetalleVenta.Size = New System.Drawing.Size(861, 468)
        '
        '
        '
        Me.gpDetalleVenta.Style.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.gpDetalleVenta.Style.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.gpDetalleVenta.Style.BackColorGradientAngle = 90
        Me.gpDetalleVenta.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.gpDetalleVenta.Style.BorderBottomWidth = 1
        Me.gpDetalleVenta.Style.BorderColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.gpDetalleVenta.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.gpDetalleVenta.Style.BorderLeftWidth = 1
        Me.gpDetalleVenta.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.gpDetalleVenta.Style.BorderRightWidth = 1
        Me.gpDetalleVenta.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.gpDetalleVenta.Style.BorderTopWidth = 1
        Me.gpDetalleVenta.Style.CornerDiameter = 4
        Me.gpDetalleVenta.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.gpDetalleVenta.Style.Font = New System.Drawing.Font("Georgia", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gpDetalleVenta.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.gpDetalleVenta.Style.TextColor = System.Drawing.Color.White
        Me.gpDetalleVenta.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.gpDetalleVenta.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.gpDetalleVenta.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.gpDetalleVenta.TabIndex = 3
        Me.gpDetalleVenta.Text = "DETALLE DE LA VENTA"
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.White
        Me.Panel5.Controls.Add(Me.GPanelProductos)
        Me.Panel5.Controls.Add(Me.grdetalle)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(0, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(855, 445)
        Me.Panel5.TabIndex = 0
        '
        'GPanelProductos
        '
        Me.GPanelProductos.CanvasColor = System.Drawing.SystemColors.Control
        Me.GPanelProductos.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GPanelProductos.Controls.Add(Me.Panel7)
        Me.GPanelProductos.DisabledBackColor = System.Drawing.Color.Empty
        Me.GPanelProductos.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GPanelProductos.Font = New System.Drawing.Font("Georgia", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GPanelProductos.Location = New System.Drawing.Point(0, 45)
        Me.GPanelProductos.Name = "GPanelProductos"
        Me.GPanelProductos.Size = New System.Drawing.Size(855, 400)
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
        Me.GPanelProductos.TabIndex = 4
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
        Me.Panel7.Size = New System.Drawing.Size(849, 377)
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
        Me.grProductos.Size = New System.Drawing.Size(849, 377)
        Me.grProductos.TabIndex = 0
        Me.grProductos.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'grdetalle
        '
        Me.grdetalle.BackColor = System.Drawing.Color.GhostWhite
        Me.grdetalle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdetalle.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdetalle.HeaderFormatStyle.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold)
        Me.grdetalle.Location = New System.Drawing.Point(0, 0)
        Me.grdetalle.Name = "grdetalle"
        Me.grdetalle.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.grdetalle.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.grdetalle.Size = New System.Drawing.Size(855, 445)
        Me.grdetalle.TabIndex = 3
        Me.grdetalle.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'SuperTabItem1
        '
        Me.SuperTabItem1.AttachedControl = Me.SuperTabControlPanel1
        Me.SuperTabItem1.GlobalItem = False
        Me.SuperTabItem1.Name = "SuperTabItem1"
        Me.SuperTabItem1.Text = "BUSCAR"
        '
        'SuperTabControlPanel1
        '
        Me.SuperTabControlPanel1.Controls.Add(Me.GroupPanel3)
        Me.SuperTabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SuperTabControlPanel1.Location = New System.Drawing.Point(0, 0)
        Me.SuperTabControlPanel1.Name = "SuperTabControlPanel1"
        Me.SuperTabControlPanel1.Size = New System.Drawing.Size(1283, 530)
        Me.SuperTabControlPanel1.TabIndex = 0
        Me.SuperTabControlPanel1.TabItem = Me.SuperTabItem1
        '
        'GroupPanel3
        '
        Me.GroupPanel3.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel3.Controls.Add(Me.Panel6)
        Me.GroupPanel3.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupPanel3.Font = New System.Drawing.Font("Georgia", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupPanel3.Location = New System.Drawing.Point(0, 0)
        Me.GroupPanel3.Name = "GroupPanel3"
        Me.GroupPanel3.Size = New System.Drawing.Size(1283, 530)
        '
        '
        '
        Me.GroupPanel3.Style.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.GroupPanel3.Style.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.GroupPanel3.Style.BackColorGradientAngle = 90
        Me.GroupPanel3.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel3.Style.BorderBottomWidth = 1
        Me.GroupPanel3.Style.BorderColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.GroupPanel3.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel3.Style.BorderLeftWidth = 1
        Me.GroupPanel3.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel3.Style.BorderRightWidth = 1
        Me.GroupPanel3.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel3.Style.BorderTopWidth = 1
        Me.GroupPanel3.Style.CornerDiameter = 4
        Me.GroupPanel3.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanel3.Style.Font = New System.Drawing.Font("Georgia", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.GroupPanel3.TabIndex = 4
        Me.GroupPanel3.Text = "BUSCADOR  VENTAS"
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.White
        Me.Panel6.Controls.Add(Me.grVentas)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1277, 507)
        Me.Panel6.TabIndex = 0
        '
        'grVentas
        '
        Me.grVentas.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grVentas.BackColor = System.Drawing.Color.GhostWhite
        Me.grVentas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grVentas.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None
        Me.grVentas.FocusStyle = Janus.Windows.GridEX.FocusStyle.Solid
        Me.grVentas.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grVentas.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.UseRowStyle
        Me.grVentas.HeaderFormatStyle.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grVentas.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight
        Me.grVentas.Location = New System.Drawing.Point(0, 0)
        Me.grVentas.Name = "grVentas"
        Me.grVentas.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.grVentas.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.grVentas.SelectedFormatStyle.BackColor = System.Drawing.Color.DodgerBlue
        Me.grVentas.SelectedFormatStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grVentas.SelectedFormatStyle.ForeColor = System.Drawing.Color.White
        Me.grVentas.SelectOnExpand = False
        Me.grVentas.Size = New System.Drawing.Size(1277, 507)
        Me.grVentas.TabIndex = 0
        Me.grVentas.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'btnDuplicar
        '
        Me.btnDuplicar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnDuplicar.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange
        Me.btnDuplicar.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnDuplicar.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDuplicar.Image = Global.DinoM.My.Resources.Resources._14
        Me.btnDuplicar.ImageFixedSize = New System.Drawing.Size(48, 48)
        Me.btnDuplicar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top
        Me.btnDuplicar.Location = New System.Drawing.Point(180, 0)
        Me.btnDuplicar.Name = "btnDuplicar"
        Me.btnDuplicar.Size = New System.Drawing.Size(80, 72)
        Me.btnDuplicar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnDuplicar.TabIndex = 13
        Me.btnDuplicar.Text = "DUPLICAR"
        Me.btnDuplicar.TextColor = System.Drawing.Color.White
        Me.btnDuplicar.Visible = False
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'PanelFondoDetalle
        '
        Me.PanelFondoDetalle.Controls.Add(Me.gpDetalleVenta)
        Me.PanelFondoDetalle.Controls.Add(Me.PanelFacturaYCobranza)
        Me.PanelFondoDetalle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelFondoDetalle.Location = New System.Drawing.Point(0, 130)
        Me.PanelFondoDetalle.Name = "PanelFondoDetalle"
        Me.PanelFondoDetalle.Size = New System.Drawing.Size(1286, 468)
        Me.PanelFondoDetalle.TabIndex = 5
        '
        'PanelFacturaYCobranza
        '
        Me.PanelFacturaYCobranza.Controls.Add(Me.GroupCobranza)
        Me.PanelFacturaYCobranza.Controls.Add(Me.GroupPanelFactura2)
        Me.PanelFacturaYCobranza.Dock = System.Windows.Forms.DockStyle.Right
        Me.PanelFacturaYCobranza.Location = New System.Drawing.Point(861, 0)
        Me.PanelFacturaYCobranza.Name = "PanelFacturaYCobranza"
        Me.PanelFacturaYCobranza.Size = New System.Drawing.Size(425, 468)
        Me.PanelFacturaYCobranza.TabIndex = 239
        '
        'PanelEncabezado
        '
        Me.PanelEncabezado.Controls.Add(Me.PanelFondoDetalle)
        Me.PanelEncabezado.Controls.Add(Me.GroupPanel2)
        Me.PanelEncabezado.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelEncabezado.Location = New System.Drawing.Point(0, 0)
        Me.PanelEncabezado.Name = "PanelEncabezado"
        Me.PanelEncabezado.Size = New System.Drawing.Size(1286, 598)
        Me.PanelEncabezado.TabIndex = 1
        '
        'btnBitacora
        '
        Me.btnBitacora.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnBitacora.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange
        Me.btnBitacora.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnBitacora.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBitacora.Image = Global.DinoM.My.Resources.Resources.BUSQUEDA
        Me.btnBitacora.ImageFixedSize = New System.Drawing.Size(48, 48)
        Me.btnBitacora.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top
        Me.btnBitacora.Location = New System.Drawing.Point(0, 0)
        Me.btnBitacora.Name = "btnBitacora"
        Me.btnBitacora.Size = New System.Drawing.Size(88, 72)
        Me.btnBitacora.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnBitacora.TabIndex = 18
        Me.btnBitacora.Text = "DESCUENTOS"
        Me.btnBitacora.TextColor = System.Drawing.Color.White
        '
        'SwDescuentoProveedor
        '
        '
        '
        '
        Me.SwDescuentoProveedor.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.SwDescuentoProveedor.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SwDescuentoProveedor.Location = New System.Drawing.Point(884, 38)
        Me.SwDescuentoProveedor.Name = "SwDescuentoProveedor"
        Me.SwDescuentoProveedor.OffBackColor = System.Drawing.Color.LawnGreen
        Me.SwDescuentoProveedor.OffText = "DESC. MANUAL"
        Me.SwDescuentoProveedor.OnBackColor = System.Drawing.Color.Gold
        Me.SwDescuentoProveedor.OnText = "DESC. AUTOMATICO"
        Me.SwDescuentoProveedor.Size = New System.Drawing.Size(170, 28)
        Me.SwDescuentoProveedor.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.SwDescuentoProveedor.TabIndex = 387
        Me.SwDescuentoProveedor.Value = True
        Me.SwDescuentoProveedor.ValueObject = "Y"
        Me.SwDescuentoProveedor.Visible = False
        '
        'CbTipoDoc
        '
        Me.CbTipoDoc.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat
        Me.CbTipoDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        CbTipoDoc_DesignTimeLayout.LayoutString = resources.GetString("CbTipoDoc_DesignTimeLayout.LayoutString")
        Me.CbTipoDoc.DesignTimeLayout = CbTipoDoc_DesignTimeLayout
        Me.CbTipoDoc.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CbTipoDoc.Location = New System.Drawing.Point(556, 39)
        Me.CbTipoDoc.Name = "CbTipoDoc"
        Me.CbTipoDoc.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.CbTipoDoc.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.CbTipoDoc.SelectedIndex = -1
        Me.CbTipoDoc.SelectedItem = Nothing
        Me.CbTipoDoc.Size = New System.Drawing.Size(278, 22)
        Me.CbTipoDoc.TabIndex = 418
        Me.CbTipoDoc.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'LabelX25
        '
        Me.LabelX25.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX25.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX25.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX25.ForeColor = System.Drawing.Color.White
        Me.LabelX25.Location = New System.Drawing.Point(438, 38)
        Me.LabelX25.Name = "LabelX25"
        Me.LabelX25.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX25.Size = New System.Drawing.Size(112, 23)
        Me.LabelX25.TabIndex = 419
        Me.LabelX25.Text = "Tipo Documento:"
        '
        'Label1Conn
        '
        Me.Label1Conn.AutoSize = True
        Me.Label1Conn.BackColor = System.Drawing.Color.Green
        Me.Label1Conn.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1Conn.ForeColor = System.Drawing.Color.White
        Me.Label1Conn.Location = New System.Drawing.Point(652, 9)
        Me.Label1Conn.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1Conn.Name = "Label1Conn"
        Me.Label1Conn.Size = New System.Drawing.Size(14, 20)
        Me.Label1Conn.TabIndex = 414
        Me.Label1Conn.Text = "."
        Me.Label1Conn.Visible = False
        '
        'lbEjemplo
        '
        Me.lbEjemplo.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.lbEjemplo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lbEjemplo.Font = New System.Drawing.Font("Georgia", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbEjemplo.ForeColor = System.Drawing.Color.White
        Me.lbEjemplo.Location = New System.Drawing.Point(279, 81)
        Me.lbEjemplo.Name = "lbEjemplo"
        Me.lbEjemplo.SingleLineColor = System.Drawing.SystemColors.Control
        Me.lbEjemplo.Size = New System.Drawing.Size(130, 15)
        Me.lbEjemplo.TabIndex = 376
        Me.lbEjemplo.Text = "Ej:4041000000002321"
        '
        'F0_Venta2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1319, 709)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "F0_Venta2"
        Me.Text = "F0_Venta2"
        Me.Controls.SetChildIndex(Me.PanelPrincipal, 0)
        Me.PanelSuperior.ResumeLayout(False)
        Me.PanelSuperior.PerformLayout()
        Me.PanelInferior.ResumeLayout(False)
        Me.PanelInferior.PerformLayout()
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
        Me.GroupPanel2.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.tbFechaVenc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.QrFactura, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbSucursal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbFechaVenta, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupCobranza.ResumeLayout(False)
        Me.GroupCobranza.PerformLayout()
        CType(Me.tbMdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbSubTotal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbCambioDolar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCambio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMontoPagado, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbIce, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbPrueba, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbPdesc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.tbMontoTarej, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbMontoBs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbMontoDolar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupPanelFactura2.ResumeLayout(False)
        CType(Me.dtiFechaFactura, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gpDetalleVenta.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.GPanelProductos.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        CType(Me.grProductos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdetalle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SuperTabControlPanel1.ResumeLayout(False)
        Me.GroupPanel3.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        CType(Me.grVentas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelFondoDetalle.ResumeLayout(False)
        Me.PanelFacturaYCobranza.ResumeLayout(False)
        Me.PanelEncabezado.ResumeLayout(False)
        CType(Me.CbTipoDoc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents gpDetalleVenta As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents GPanelProductos As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents grProductos As Janus.Windows.GridEX.GridEX
    Friend WithEvents grdetalle As Janus.Windows.GridEX.GridEX
    Friend WithEvents GroupPanel2 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents GroupCobranza As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents txtCambio As DevComponents.Editors.DoubleInput
    Friend WithEvents txtMontoPagado As DevComponents.Editors.DoubleInput
    Friend WithEvents lbCambio As DevComponents.DotNetBar.LabelX
    Friend WithEvents lbMontoPagado As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX13 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbIce As DevComponents.Editors.DoubleInput
    Friend WithEvents lbIce As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbPrueba As DevComponents.Editors.DoubleInput
    Friend WithEvents LabelX8 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX11 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbPdesc As DevComponents.Editors.DoubleInput
    Friend WithEvents LabelX9 As DevComponents.DotNetBar.LabelX
    Friend WithEvents Panel2 As Panel
    Friend WithEvents lbCtrlEnter As DevComponents.DotNetBar.LabelX
    Friend WithEvents TbNombre1 As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbNit As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lbTipoMoneda As DevComponents.DotNetBar.LabelX
    Friend WithEvents swMoneda As DevComponents.DotNetBar.Controls.SwitchButton
    Friend WithEvents LabelX17 As DevComponents.DotNetBar.LabelX
    Friend WithEvents cbSucursal As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents LabelX10 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbVendedor As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents tbCliente As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbCodigo As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents lbFVenta As DevComponents.DotNetBar.LabelX
    Friend WithEvents lbIdVenta As DevComponents.DotNetBar.LabelX
    Friend WithEvents GroupPanelFactura2 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents tbCodigoControl As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX16 As DevComponents.DotNetBar.LabelX
    Friend WithEvents dtiFechaFactura As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents tbNroFactura As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents tbNroAutoriz As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX15 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX14 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX7 As DevComponents.DotNetBar.LabelX
    Friend WithEvents SuperTabControlPanel1 As DevComponents.DotNetBar.SuperTabControlPanel
    Friend WithEvents SuperTabItem1 As DevComponents.DotNetBar.SuperTabItem
    Friend WithEvents GroupPanel3 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents grVentas As Janus.Windows.GridEX.GridEX
    Friend WithEvents tbFechaVenta As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents TbNombre2 As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents QrFactura As Gma.QrCodeNet.Encoding.Windows.Forms.QrCodeImgControl
    Friend WithEvents PrintDialog1 As PrintDialog
    Friend WithEvents LabelX6 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbMontoDolar As DevComponents.Editors.DoubleInput
    Friend WithEvents tbMontoBs As DevComponents.Editors.DoubleInput
    Friend WithEvents LabelX5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbMontoTarej As DevComponents.Editors.DoubleInput
    Friend WithEvents chbTarjeta As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents swTipoVenta As DevComponents.DotNetBar.Controls.SwitchButton
    Friend WithEvents lbCredito As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbFechaVenc As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents btgrupo1 As DevComponents.DotNetBar.ButtonX
    Friend WithEvents cbCambioDolar As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents LabelX12 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX18 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbTotalBs As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX22 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbTotalDo As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX20 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX19 As DevComponents.DotNetBar.LabelX
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents txtMontoPagado1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtCambio1 As DevComponents.DotNetBar.LabelX
    Protected WithEvents btnDuplicar As DevComponents.DotNetBar.ButtonX
    Friend WithEvents LabelX21 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtEstado As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents Timer1 As Timer
    Friend WithEvents tbMdesc As DevComponents.Editors.DoubleInput
    Friend WithEvents tbSubTotal As DevComponents.Editors.DoubleInput
    Friend WithEvents tbObservacion As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents lblObservacion As DevComponents.DotNetBar.LabelX
    Friend WithEvents PanelFondoDetalle As Panel
    Friend WithEvents PanelEncabezado As Panel
    Friend WithEvents PanelFacturaYCobranza As Panel
    Friend WithEvents LabelX23 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lbNroCaja As Label
    Friend WithEvents LabelX24 As DevComponents.DotNetBar.LabelX
    Protected WithEvents btnBitacora As DevComponents.DotNetBar.ButtonX
    Friend WithEvents SwDescuentoProveedor As DevComponents.DotNetBar.Controls.SwitchButton
    Public WithEvents CbTipoDoc As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents LabelX25 As DevComponents.DotNetBar.LabelX
    Friend WithEvents TbEmail As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX27 As DevComponents.DotNetBar.LabelX
    Friend WithEvents Label1Conn As Label
    Friend WithEvents lbNroTarjeta As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbNroTarjeta As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents lbEjemplo As DevComponents.DotNetBar.LabelX
End Class
