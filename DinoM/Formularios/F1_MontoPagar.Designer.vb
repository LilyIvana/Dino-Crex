<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F1_MontoPagar
    Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F1_MontoPagar))
        Dim cbCambioDolar_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim CbTipoDoc_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.BtnSalir = New DevComponents.DotNetBar.ButtonX()
        Me.btnContinuar = New DevComponents.DotNetBar.ButtonX()
        Me.txtMontoPagado1 = New DevComponents.DotNetBar.LabelX()
        Me.txtCambio1 = New DevComponents.DotNetBar.LabelX()
        Me.lbCambio = New DevComponents.DotNetBar.LabelX()
        Me.lbMontoPagado = New DevComponents.DotNetBar.LabelX()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lbEjemplo = New DevComponents.DotNetBar.LabelX()
        Me.tbNroTarjeta = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.lbNroTarjeta = New DevComponents.DotNetBar.LabelX()
        Me.btgrupo1 = New DevComponents.DotNetBar.ButtonX()
        Me.LabelX20 = New DevComponents.DotNetBar.LabelX()
        Me.ButtonX3 = New DevComponents.DotNetBar.ButtonX()
        Me.cbCambioDolar = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.tbMontoTarej = New DevComponents.Editors.DoubleInput()
        Me.ButtonX2 = New DevComponents.DotNetBar.ButtonX()
        Me.chbTarjeta = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.ButtonX1 = New DevComponents.DotNetBar.ButtonX()
        Me.tbMontoBs = New DevComponents.Editors.DoubleInput()
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX()
        Me.tbMontoDolar = New DevComponents.Editors.DoubleInput()
        Me.LabelX6 = New DevComponents.DotNetBar.LabelX()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX27 = New DevComponents.DotNetBar.LabelX()
        Me.TbEmail = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.CbTipoDoc = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.LabelX25 = New DevComponents.DotNetBar.LabelX()
        Me.tbRazonSocial = New System.Windows.Forms.TextBox()
        Me.tbNit = New System.Windows.Forms.TextBox()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.cbCambioDolar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbMontoTarej, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbMontoBs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbMontoDolar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.CbTipoDoc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.BackgroundImage = Global.DinoM.My.Resources.Resources.fondo1
        Me.GroupBox2.Controls.Add(Me.BtnSalir)
        Me.GroupBox2.Controls.Add(Me.btnContinuar)
        Me.GroupBox2.Controls.Add(Me.txtMontoPagado1)
        Me.GroupBox2.Controls.Add(Me.txtCambio1)
        Me.GroupBox2.Controls.Add(Me.lbCambio)
        Me.GroupBox2.Controls.Add(Me.lbMontoPagado)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.Gold
        Me.GroupBox2.Location = New System.Drawing.Point(0, 281)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(497, 154)
        Me.GroupBox2.TabIndex = 364
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Monto Venta/Cambio:"
        '
        'BtnSalir
        '
        Me.BtnSalir.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.BtnSalir.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground
        Me.BtnSalir.Font = New System.Drawing.Font("Calibri", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSalir.Image = Global.DinoM.My.Resources.Resources.cancel
        Me.BtnSalir.ImageFixedSize = New System.Drawing.Size(30, 30)
        Me.BtnSalir.Location = New System.Drawing.Point(247, 89)
        Me.BtnSalir.Margin = New System.Windows.Forms.Padding(2)
        Me.BtnSalir.Name = "BtnSalir"
        Me.BtnSalir.Size = New System.Drawing.Size(110, 39)
        Me.BtnSalir.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeMobile2014
        Me.BtnSalir.TabIndex = 371
        Me.BtnSalir.Text = "Salir"
        '
        'btnContinuar
        '
        Me.btnContinuar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnContinuar.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground
        Me.btnContinuar.Font = New System.Drawing.Font("Calibri", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnContinuar.Image = Global.DinoM.My.Resources.Resources.checked
        Me.btnContinuar.ImageFixedSize = New System.Drawing.Size(30, 30)
        Me.btnContinuar.Location = New System.Drawing.Point(368, 89)
        Me.btnContinuar.Margin = New System.Windows.Forms.Padding(2)
        Me.btnContinuar.Name = "btnContinuar"
        Me.btnContinuar.Size = New System.Drawing.Size(110, 39)
        Me.btnContinuar.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeMobile2014
        Me.btnContinuar.TabIndex = 3
        Me.btnContinuar.Text = "Continuar"
        '
        'txtMontoPagado1
        '
        Me.txtMontoPagado1.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtMontoPagado1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtMontoPagado1.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMontoPagado1.ForeColor = System.Drawing.Color.YellowGreen
        Me.txtMontoPagado1.Location = New System.Drawing.Point(137, 20)
        Me.txtMontoPagado1.Name = "txtMontoPagado1"
        Me.txtMontoPagado1.SingleLineColor = System.Drawing.SystemColors.Control
        Me.txtMontoPagado1.Size = New System.Drawing.Size(120, 40)
        Me.txtMontoPagado1.TabIndex = 369
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
        Me.txtCambio1.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCambio1.ForeColor = System.Drawing.Color.YellowGreen
        Me.txtCambio1.Location = New System.Drawing.Point(368, 21)
        Me.txtCambio1.Name = "txtCambio1"
        Me.txtCambio1.SingleLineColor = System.Drawing.SystemColors.Control
        Me.txtCambio1.Size = New System.Drawing.Size(110, 40)
        Me.txtCambio1.TabIndex = 368
        Me.txtCambio1.Text = "0.00"
        Me.txtCambio1.TextAlignment = System.Drawing.StringAlignment.Far
        '
        'lbCambio
        '
        Me.lbCambio.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.lbCambio.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lbCambio.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbCambio.ForeColor = System.Drawing.Color.White
        Me.lbCambio.Location = New System.Drawing.Point(282, 21)
        Me.lbCambio.Name = "lbCambio"
        Me.lbCambio.SingleLineColor = System.Drawing.SystemColors.Control
        Me.lbCambio.Size = New System.Drawing.Size(80, 40)
        Me.lbCambio.TabIndex = 367
        Me.lbCambio.Text = "Cambio Bs.:"
        '
        'lbMontoPagado
        '
        Me.lbMontoPagado.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.lbMontoPagado.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lbMontoPagado.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbMontoPagado.ForeColor = System.Drawing.Color.White
        Me.lbMontoPagado.Location = New System.Drawing.Point(11, 25)
        Me.lbMontoPagado.Name = "lbMontoPagado"
        Me.lbMontoPagado.SingleLineColor = System.Drawing.SystemColors.Control
        Me.lbMontoPagado.Size = New System.Drawing.Size(110, 33)
        Me.lbMontoPagado.TabIndex = 366
        Me.lbMontoPagado.Text = "Total Venta Bs. :"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.BackgroundImage = Global.DinoM.My.Resources.Resources.fondo1
        Me.GroupBox1.Controls.Add(Me.lbEjemplo)
        Me.GroupBox1.Controls.Add(Me.tbNroTarjeta)
        Me.GroupBox1.Controls.Add(Me.lbNroTarjeta)
        Me.GroupBox1.Controls.Add(Me.btgrupo1)
        Me.GroupBox1.Controls.Add(Me.LabelX20)
        Me.GroupBox1.Controls.Add(Me.ButtonX3)
        Me.GroupBox1.Controls.Add(Me.cbCambioDolar)
        Me.GroupBox1.Controls.Add(Me.tbMontoTarej)
        Me.GroupBox1.Controls.Add(Me.ButtonX2)
        Me.GroupBox1.Controls.Add(Me.chbTarjeta)
        Me.GroupBox1.Controls.Add(Me.ButtonX1)
        Me.GroupBox1.Controls.Add(Me.tbMontoBs)
        Me.GroupBox1.Controls.Add(Me.LabelX5)
        Me.GroupBox1.Controls.Add(Me.tbMontoDolar)
        Me.GroupBox1.Controls.Add(Me.LabelX6)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.Gold
        Me.GroupBox1.Location = New System.Drawing.Point(0, 140)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(497, 141)
        Me.GroupBox1.TabIndex = 362
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Efectivo:"
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
        Me.lbEjemplo.Location = New System.Drawing.Point(332, 117)
        Me.lbEjemplo.Name = "lbEjemplo"
        Me.lbEjemplo.SingleLineColor = System.Drawing.SystemColors.Control
        Me.lbEjemplo.Size = New System.Drawing.Size(130, 15)
        Me.lbEjemplo.TabIndex = 379
        Me.lbEjemplo.Text = "Ej:4041000000002321"
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
        Me.tbNroTarjeta.Location = New System.Drawing.Point(113, 110)
        Me.tbNroTarjeta.MaxLength = 16
        Me.tbNroTarjeta.Name = "tbNroTarjeta"
        Me.tbNroTarjeta.PreventEnterBeep = True
        Me.tbNroTarjeta.Size = New System.Drawing.Size(209, 24)
        Me.tbNroTarjeta.TabIndex = 378
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
        Me.lbNroTarjeta.Location = New System.Drawing.Point(10, 114)
        Me.lbNroTarjeta.Name = "lbNroTarjeta"
        Me.lbNroTarjeta.SingleLineColor = System.Drawing.SystemColors.Control
        Me.lbNroTarjeta.Size = New System.Drawing.Size(69, 18)
        Me.lbNroTarjeta.TabIndex = 377
        Me.lbNroTarjeta.Text = "Nro. Tarj:"
        '
        'btgrupo1
        '
        Me.btgrupo1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btgrupo1.BackColor = System.Drawing.Color.Transparent
        Me.btgrupo1.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat
        Me.btgrupo1.Image = Global.DinoM.My.Resources.Resources.add
        Me.btgrupo1.ImageFixedSize = New System.Drawing.Size(25, 23)
        Me.btgrupo1.Location = New System.Drawing.Point(239, 16)
        Me.btgrupo1.Name = "btgrupo1"
        Me.btgrupo1.Size = New System.Drawing.Size(28, 23)
        Me.btgrupo1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btgrupo1.TabIndex = 369
        Me.btgrupo1.Visible = False
        '
        'LabelX20
        '
        Me.LabelX20.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX20.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX20.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LabelX20.ForeColor = System.Drawing.Color.White
        Me.LabelX20.Location = New System.Drawing.Point(8, 18)
        Me.LabelX20.Name = "LabelX20"
        Me.LabelX20.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX20.Size = New System.Drawing.Size(90, 18)
        Me.LabelX20.TabIndex = 370
        Me.LabelX20.Text = "Tipo Cambio:"
        '
        'ButtonX3
        '
        Me.ButtonX3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonX3.BackColor = System.Drawing.Color.Transparent
        Me.ButtonX3.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat
        Me.ButtonX3.Image = CType(resources.GetObject("ButtonX3.Image"), System.Drawing.Image)
        Me.ButtonX3.ImageFixedSize = New System.Drawing.Size(25, 23)
        Me.ButtonX3.Location = New System.Drawing.Point(397, 16)
        Me.ButtonX3.Name = "ButtonX3"
        Me.ButtonX3.Size = New System.Drawing.Size(60, 20)
        Me.ButtonX3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.ButtonX3.TabIndex = 363
        Me.ButtonX3.Visible = False
        '
        'cbCambioDolar
        '
        Me.cbCambioDolar.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat
        Me.cbCambioDolar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        cbCambioDolar_DesignTimeLayout.LayoutString = resources.GetString("cbCambioDolar_DesignTimeLayout.LayoutString")
        Me.cbCambioDolar.DesignTimeLayout = cbCambioDolar_DesignTimeLayout
        Me.cbCambioDolar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbCambioDolar.Location = New System.Drawing.Point(113, 17)
        Me.cbCambioDolar.Name = "cbCambioDolar"
        Me.cbCambioDolar.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.cbCambioDolar.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.cbCambioDolar.ReadOnly = True
        Me.cbCambioDolar.SelectedIndex = -1
        Me.cbCambioDolar.SelectedItem = Nothing
        Me.cbCambioDolar.Size = New System.Drawing.Size(120, 22)
        Me.cbCambioDolar.TabIndex = 368
        Me.cbCambioDolar.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
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
        Me.tbMontoTarej.Location = New System.Drawing.Point(113, 77)
        Me.tbMontoTarej.MinValue = 0R
        Me.tbMontoTarej.Name = "tbMontoTarej"
        Me.tbMontoTarej.Size = New System.Drawing.Size(120, 26)
        Me.tbMontoTarej.TabIndex = 101
        Me.tbMontoTarej.WatermarkAlignment = DevComponents.Editors.eTextAlignment.Right
        '
        'ButtonX2
        '
        Me.ButtonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonX2.BackColor = System.Drawing.Color.Transparent
        Me.ButtonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat
        Me.ButtonX2.Image = CType(resources.GetObject("ButtonX2.Image"), System.Drawing.Image)
        Me.ButtonX2.ImageFixedSize = New System.Drawing.Size(25, 23)
        Me.ButtonX2.Location = New System.Drawing.Point(339, 17)
        Me.ButtonX2.Name = "ButtonX2"
        Me.ButtonX2.Size = New System.Drawing.Size(60, 20)
        Me.ButtonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.ButtonX2.TabIndex = 362
        Me.ButtonX2.Visible = False
        '
        'chbTarjeta
        '
        Me.chbTarjeta.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.chbTarjeta.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chbTarjeta.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbTarjeta.Location = New System.Drawing.Point(10, 78)
        Me.chbTarjeta.Name = "chbTarjeta"
        Me.chbTarjeta.Size = New System.Drawing.Size(87, 30)
        Me.chbTarjeta.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.chbTarjeta.TabIndex = 279
        Me.chbTarjeta.Text = "Tarjeta:"
        Me.chbTarjeta.TextColor = System.Drawing.Color.White
        '
        'ButtonX1
        '
        Me.ButtonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonX1.BackColor = System.Drawing.Color.Transparent
        Me.ButtonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat
        Me.ButtonX1.Image = CType(resources.GetObject("ButtonX1.Image"), System.Drawing.Image)
        Me.ButtonX1.ImageFixedSize = New System.Drawing.Size(25, 23)
        Me.ButtonX1.Location = New System.Drawing.Point(280, 16)
        Me.ButtonX1.Name = "ButtonX1"
        Me.ButtonX1.Size = New System.Drawing.Size(60, 20)
        Me.ButtonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.ButtonX1.TabIndex = 360
        Me.ButtonX1.Visible = False
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
        Me.tbMontoBs.Location = New System.Drawing.Point(113, 45)
        Me.tbMontoBs.MinValue = 0R
        Me.tbMontoBs.Name = "tbMontoBs"
        Me.tbMontoBs.Size = New System.Drawing.Size(120, 26)
        Me.tbMontoBs.TabIndex = 4
        Me.tbMontoBs.WatermarkAlignment = DevComponents.Editors.eTextAlignment.Right
        '
        'LabelX5
        '
        Me.LabelX5.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX5.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX5.ForeColor = System.Drawing.Color.White
        Me.LabelX5.Location = New System.Drawing.Point(9, 50)
        Me.LabelX5.Name = "LabelX5"
        Me.LabelX5.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX5.Size = New System.Drawing.Size(98, 18)
        Me.LabelX5.TabIndex = 64
        Me.LabelX5.Text = "M.Pagado Bs.:"
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
        Me.tbMontoDolar.Location = New System.Drawing.Point(331, 47)
        Me.tbMontoDolar.MinValue = 0R
        Me.tbMontoDolar.Name = "tbMontoDolar"
        Me.tbMontoDolar.Size = New System.Drawing.Size(147, 26)
        Me.tbMontoDolar.TabIndex = 100
        Me.tbMontoDolar.WatermarkAlignment = DevComponents.Editors.eTextAlignment.Right
        '
        'LabelX6
        '
        Me.LabelX6.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX6.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX6.ForeColor = System.Drawing.Color.White
        Me.LabelX6.Location = New System.Drawing.Point(262, 51)
        Me.LabelX6.Name = "LabelX6"
        Me.LabelX6.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX6.Size = New System.Drawing.Size(60, 18)
        Me.LabelX6.TabIndex = 66
        Me.LabelX6.Text = "Mont. $:"
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.BackgroundImage = Global.DinoM.My.Resources.Resources.fondo1
        Me.GroupBox3.Controls.Add(Me.LabelX1)
        Me.GroupBox3.Controls.Add(Me.LabelX27)
        Me.GroupBox3.Controls.Add(Me.TbEmail)
        Me.GroupBox3.Controls.Add(Me.CbTipoDoc)
        Me.GroupBox3.Controls.Add(Me.LabelX25)
        Me.GroupBox3.Controls.Add(Me.tbRazonSocial)
        Me.GroupBox3.Controls.Add(Me.tbNit)
        Me.GroupBox3.Controls.Add(Me.LabelX3)
        Me.GroupBox3.Controls.Add(Me.LabelX4)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox3.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.ForeColor = System.Drawing.Color.Gold
        Me.GroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(497, 140)
        Me.GroupBox3.TabIndex = 365
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Datos de Factura"
        '
        'LabelX1
        '
        Me.LabelX1.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Font = New System.Drawing.Font("Calibri", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX1.ForeColor = System.Drawing.Color.White
        Me.LabelX1.Location = New System.Drawing.Point(425, 20)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX1.Size = New System.Drawing.Size(58, 18)
        Me.LabelX1.TabIndex = 424
        Me.LabelX1.Text = "Presione Enter"
        '
        'LabelX27
        '
        Me.LabelX27.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX27.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX27.Font = New System.Drawing.Font("Calibri", 11.0!, System.Drawing.FontStyle.Bold)
        Me.LabelX27.ForeColor = System.Drawing.Color.White
        Me.LabelX27.Location = New System.Drawing.Point(10, 106)
        Me.LabelX27.Name = "LabelX27"
        Me.LabelX27.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX27.Size = New System.Drawing.Size(70, 23)
        Me.LabelX27.TabIndex = 423
        Me.LabelX27.Text = "Email:"
        '
        'TbEmail
        '
        Me.TbEmail.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.TbEmail.Border.Class = "TextBoxBorder"
        Me.TbEmail.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.TbEmail.Font = New System.Drawing.Font("Georgia", 10.0!, System.Drawing.FontStyle.Bold)
        Me.TbEmail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.TbEmail.Location = New System.Drawing.Point(124, 106)
        Me.TbEmail.Name = "TbEmail"
        Me.TbEmail.PreventEnterBeep = True
        Me.TbEmail.Size = New System.Drawing.Size(354, 23)
        Me.TbEmail.TabIndex = 3
        '
        'CbTipoDoc
        '
        Me.CbTipoDoc.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat
        Me.CbTipoDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        CbTipoDoc_DesignTimeLayout.LayoutString = resources.GetString("CbTipoDoc_DesignTimeLayout.LayoutString")
        Me.CbTipoDoc.DesignTimeLayout = CbTipoDoc_DesignTimeLayout
        Me.CbTipoDoc.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CbTipoDoc.Location = New System.Drawing.Point(123, 46)
        Me.CbTipoDoc.Name = "CbTipoDoc"
        Me.CbTipoDoc.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.CbTipoDoc.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.CbTipoDoc.SelectedIndex = -1
        Me.CbTipoDoc.SelectedItem = Nothing
        Me.CbTipoDoc.Size = New System.Drawing.Size(295, 23)
        Me.CbTipoDoc.TabIndex = 1
        Me.CbTipoDoc.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'LabelX25
        '
        Me.LabelX25.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX25.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX25.Font = New System.Drawing.Font("Calibri", 11.0!, System.Drawing.FontStyle.Bold)
        Me.LabelX25.ForeColor = System.Drawing.Color.White
        Me.LabelX25.Location = New System.Drawing.Point(10, 46)
        Me.LabelX25.Name = "LabelX25"
        Me.LabelX25.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX25.Size = New System.Drawing.Size(109, 23)
        Me.LabelX25.TabIndex = 421
        Me.LabelX25.Text = "Tipo Documento:"
        '
        'tbRazonSocial
        '
        Me.tbRazonSocial.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbRazonSocial.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbRazonSocial.Location = New System.Drawing.Point(123, 75)
        Me.tbRazonSocial.Margin = New System.Windows.Forms.Padding(2)
        Me.tbRazonSocial.Name = "tbRazonSocial"
        Me.tbRazonSocial.Size = New System.Drawing.Size(355, 24)
        Me.tbRazonSocial.TabIndex = 2
        '
        'tbNit
        '
        Me.tbNit.AcceptsTab = True
        Me.tbNit.Cursor = System.Windows.Forms.Cursors.Default
        Me.tbNit.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbNit.Location = New System.Drawing.Point(124, 16)
        Me.tbNit.Margin = New System.Windows.Forms.Padding(2)
        Me.tbNit.Name = "tbNit"
        Me.tbNit.Size = New System.Drawing.Size(294, 24)
        Me.tbNit.TabIndex = 0
        '
        'LabelX3
        '
        Me.LabelX3.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX3.Font = New System.Drawing.Font("Calibri", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX3.ForeColor = System.Drawing.Color.White
        Me.LabelX3.Location = New System.Drawing.Point(11, 22)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX3.Size = New System.Drawing.Size(65, 18)
        Me.LabelX3.TabIndex = 367
        Me.LabelX3.Text = "Nit:"
        '
        'LabelX4
        '
        Me.LabelX4.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX4.Font = New System.Drawing.Font("Calibri", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX4.ForeColor = System.Drawing.Color.White
        Me.LabelX4.Location = New System.Drawing.Point(10, 81)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX4.Size = New System.Drawing.Size(89, 18)
        Me.LabelX4.TabIndex = 366
        Me.LabelX4.Text = "Razón Social:"
        '
        'F1_MontoPagar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(497, 435)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "F1_MontoPagar"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "F1_MontoPagar"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.cbCambioDolar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbMontoTarej, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbMontoBs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbMontoDolar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.CbTipoDoc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents ButtonX3 As DevComponents.DotNetBar.ButtonX
    Friend WithEvents tbMontoTarej As DevComponents.Editors.DoubleInput
    Friend WithEvents ButtonX2 As DevComponents.DotNetBar.ButtonX
    Friend WithEvents chbTarjeta As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents ButtonX1 As DevComponents.DotNetBar.ButtonX
    Friend WithEvents tbMontoBs As DevComponents.Editors.DoubleInput
    Friend WithEvents LabelX5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbMontoDolar As DevComponents.Editors.DoubleInput
    Friend WithEvents LabelX6 As DevComponents.DotNetBar.LabelX
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents txtMontoPagado1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtCambio1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lbCambio As DevComponents.DotNetBar.LabelX
    Friend WithEvents lbMontoPagado As DevComponents.DotNetBar.LabelX
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbNit As TextBox
    Friend WithEvents BtnSalir As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnContinuar As DevComponents.DotNetBar.ButtonX
    Public WithEvents tbRazonSocial As TextBox
    Friend WithEvents btgrupo1 As DevComponents.DotNetBar.ButtonX
    Friend WithEvents LabelX20 As DevComponents.DotNetBar.LabelX
    Friend WithEvents cbCambioDolar As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Public WithEvents CbTipoDoc As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents LabelX25 As DevComponents.DotNetBar.LabelX
    Friend WithEvents TbEmail As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX27 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lbEjemplo As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbNroTarjeta As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents lbNroTarjeta As DevComponents.DotNetBar.LabelX
End Class
