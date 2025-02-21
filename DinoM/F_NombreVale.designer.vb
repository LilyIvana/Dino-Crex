
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class F_NombreVale
    Inherits System.Windows.Forms.Form

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
        Me.tbNombre = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX6 = New DevComponents.DotNetBar.LabelX()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.ReflectionLabel1 = New DevComponents.DotNetBar.Controls.ReflectionLabel()
        Me.MEP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.MHighlighterFocus = New DevComponents.DotNetBar.Validator.Highlighter()
        Me.MFlyoutUsuario = New DevComponents.DotNetBar.Controls.Flyout(Me.components)
        Me.btnCancelar = New DevComponents.DotNetBar.ButtonX()
        Me.btnAceptar = New DevComponents.DotNetBar.ButtonX()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.tbNrovale = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.tbNombreCli = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.tbCICli = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX7 = New DevComponents.DotNetBar.LabelX()
        Me.tbMontoVale = New DevComponents.Editors.DoubleInput()
        Me.tbExcedente = New DevComponents.Editors.DoubleInput()
        Me.tbCantVale = New DevComponents.Editors.IntegerInput()
        Me.tbBeneficio = New DevComponents.Editors.DoubleInput()
        Me.tbTotalVenta = New DevComponents.Editors.DoubleInput()
        Me.tbObs = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX8 = New DevComponents.DotNetBar.LabelX()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MEP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbMontoVale, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbExcedente, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbCantVale, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbBeneficio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbTotalVenta, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tbNombre
        '
        Me.tbNombre.AcceptsTab = True
        '
        '
        '
        Me.tbNombre.Border.Class = "TextBoxBorder"
        Me.tbNombre.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbNombre.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbNombre.Location = New System.Drawing.Point(159, 80)
        Me.tbNombre.Name = "tbNombre"
        Me.tbNombre.PreventEnterBeep = True
        Me.tbNombre.Size = New System.Drawing.Size(447, 22)
        Me.tbNombre.TabIndex = 0
        '
        'LabelX6
        '
        Me.LabelX6.BackColor = System.Drawing.SystemColors.Control
        '
        '
        '
        Me.LabelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX6.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX6.Location = New System.Drawing.Point(16, 79)
        Me.LabelX6.Name = "LabelX6"
        Me.LabelX6.Size = New System.Drawing.Size(127, 23)
        Me.LabelX6.TabIndex = 145
        Me.LabelX6.Text = "NOMBRE EMPRESA:"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.DodgerBlue
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.ReflectionLabel1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(20, 5, 0, 5)
        Me.Panel1.Size = New System.Drawing.Size(643, 63)
        Me.Panel1.TabIndex = 157
        '
        'PictureBox1
        '
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Right
        Me.PictureBox1.Image = Global.DinoM.My.Resources.Resources.EMPRESA
        Me.PictureBox1.Location = New System.Drawing.Point(543, 5)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Padding = New System.Windows.Forms.Padding(0, 8, 0, 0)
        Me.PictureBox1.Size = New System.Drawing.Size(100, 53)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = False
        '
        'ReflectionLabel1
        '
        '
        '
        '
        Me.ReflectionLabel1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.ReflectionLabel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.ReflectionLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ReflectionLabel1.Location = New System.Drawing.Point(20, 5)
        Me.ReflectionLabel1.Name = "ReflectionLabel1"
        Me.ReflectionLabel1.Size = New System.Drawing.Size(242, 53)
        Me.ReflectionLabel1.TabIndex = 0
        Me.ReflectionLabel1.Text = "<b><font size=""11""><font color=""#313b42"">DATOS DE LOS VALES</font></font></b>"
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
        '
        'btnCancelar
        '
        Me.btnCancelar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnCancelar.BackColor = System.Drawing.Color.Transparent
        Me.btnCancelar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnCancelar.FadeEffect = False
        Me.btnCancelar.FocusCuesEnabled = False
        Me.btnCancelar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancelar.Image = Global.DinoM.My.Resources.Resources.cancel
        Me.btnCancelar.ImageFixedSize = New System.Drawing.Size(20, 20)
        Me.btnCancelar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top
        Me.btnCancelar.Location = New System.Drawing.Point(323, 379)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnCancelar.Shape = New DevComponents.DotNetBar.RoundRectangleShapeDescriptor(2)
        Me.btnCancelar.Size = New System.Drawing.Size(134, 42)
        Me.btnCancelar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnCancelar.TabIndex = 164
        Me.btnCancelar.Text = "CANCELAR"
        Me.btnCancelar.TextColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        '
        'btnAceptar
        '
        Me.btnAceptar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnAceptar.BackColor = System.Drawing.Color.Transparent
        Me.btnAceptar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnAceptar.FadeEffect = False
        Me.btnAceptar.FocusCuesEnabled = False
        Me.btnAceptar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAceptar.Image = Global.DinoM.My.Resources.Resources.checked
        Me.btnAceptar.ImageFixedSize = New System.Drawing.Size(20, 20)
        Me.btnAceptar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top
        Me.btnAceptar.Location = New System.Drawing.Point(173, 379)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnAceptar.Shape = New DevComponents.DotNetBar.RoundRectangleShapeDescriptor(2)
        Me.btnAceptar.Size = New System.Drawing.Size(134, 42)
        Me.btnAceptar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnAceptar.TabIndex = 6
        Me.btnAceptar.Text = "ACEPTAR"
        Me.btnAceptar.TextColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        '
        'LabelX1
        '
        Me.LabelX1.BackColor = System.Drawing.SystemColors.Control
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX1.Location = New System.Drawing.Point(16, 113)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.Size = New System.Drawing.Size(127, 23)
        Me.LabelX1.TabIndex = 165
        Me.LabelX1.Text = "NÚMERO VALE:"
        '
        'tbNrovale
        '
        Me.tbNrovale.AcceptsTab = True
        '
        '
        '
        Me.tbNrovale.Border.Class = "TextBoxBorder"
        Me.tbNrovale.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbNrovale.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbNrovale.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbNrovale.Location = New System.Drawing.Point(159, 114)
        Me.tbNrovale.Name = "tbNrovale"
        Me.tbNrovale.PreventEnterBeep = True
        Me.tbNrovale.Size = New System.Drawing.Size(447, 22)
        Me.tbNrovale.TabIndex = 1
        '
        'LabelX2
        '
        Me.LabelX2.BackColor = System.Drawing.SystemColors.Control
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX2.Location = New System.Drawing.Point(16, 147)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.Size = New System.Drawing.Size(127, 23)
        Me.LabelX2.TabIndex = 167
        Me.LabelX2.Text = "CANTIDAD VALES:"
        '
        'tbNombreCli
        '
        Me.tbNombreCli.AcceptsTab = True
        '
        '
        '
        Me.tbNombreCli.Border.Class = "TextBoxBorder"
        Me.tbNombreCli.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbNombreCli.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbNombreCli.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbNombreCli.Location = New System.Drawing.Point(159, 180)
        Me.tbNombreCli.Name = "tbNombreCli"
        Me.tbNombreCli.PreventEnterBeep = True
        Me.tbNombreCli.Size = New System.Drawing.Size(447, 22)
        Me.tbNombreCli.TabIndex = 3
        '
        'LabelX3
        '
        Me.LabelX3.BackColor = System.Drawing.SystemColors.Control
        '
        '
        '
        Me.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX3.Location = New System.Drawing.Point(16, 180)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.Size = New System.Drawing.Size(127, 23)
        Me.LabelX3.TabIndex = 169
        Me.LabelX3.Text = "NOMBRE CLIENTE:"
        '
        'tbCICli
        '
        Me.tbCICli.AcceptsTab = True
        '
        '
        '
        Me.tbCICli.Border.Class = "TextBoxBorder"
        Me.tbCICli.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbCICli.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbCICli.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbCICli.Location = New System.Drawing.Point(159, 212)
        Me.tbCICli.Name = "tbCICli"
        Me.tbCICli.PreventEnterBeep = True
        Me.tbCICli.Size = New System.Drawing.Size(148, 22)
        Me.tbCICli.TabIndex = 4
        '
        'LabelX4
        '
        Me.LabelX4.BackColor = System.Drawing.SystemColors.Control
        '
        '
        '
        Me.LabelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX4.Location = New System.Drawing.Point(16, 212)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.Size = New System.Drawing.Size(127, 23)
        Me.LabelX4.TabIndex = 171
        Me.LabelX4.Text = "CI CLIENTE:"
        '
        'LabelX5
        '
        Me.LabelX5.BackColor = System.Drawing.SystemColors.Control
        '
        '
        '
        Me.LabelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX5.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX5.Location = New System.Drawing.Point(16, 247)
        Me.LabelX5.Name = "LabelX5"
        Me.LabelX5.Size = New System.Drawing.Size(127, 23)
        Me.LabelX5.TabIndex = 173
        Me.LabelX5.Text = "MONTO VALE:"
        '
        'LabelX7
        '
        Me.LabelX7.BackColor = System.Drawing.SystemColors.Control
        '
        '
        '
        Me.LabelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX7.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX7.Location = New System.Drawing.Point(16, 280)
        Me.LabelX7.Name = "LabelX7"
        Me.LabelX7.Size = New System.Drawing.Size(127, 23)
        Me.LabelX7.TabIndex = 175
        Me.LabelX7.Text = "EXCEDENTE VALE:"
        '
        'tbMontoVale
        '
        '
        '
        '
        Me.tbMontoVale.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbMontoVale.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbMontoVale.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.tbMontoVale.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbMontoVale.ForeColor = System.Drawing.Color.Black
        Me.tbMontoVale.Increment = 1.0R
        Me.tbMontoVale.Location = New System.Drawing.Point(159, 245)
        Me.tbMontoVale.MinValue = 0R
        Me.tbMontoVale.Name = "tbMontoVale"
        Me.tbMontoVale.Size = New System.Drawing.Size(148, 24)
        Me.tbMontoVale.TabIndex = 5
        Me.tbMontoVale.WatermarkAlignment = DevComponents.Editors.eTextAlignment.Right
        '
        'tbExcedente
        '
        '
        '
        '
        Me.tbExcedente.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbExcedente.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbExcedente.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.tbExcedente.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbExcedente.ForeColor = System.Drawing.Color.Black
        Me.tbExcedente.Increment = 1.0R
        Me.tbExcedente.Location = New System.Drawing.Point(159, 278)
        Me.tbExcedente.MinValue = 0R
        Me.tbExcedente.Name = "tbExcedente"
        Me.tbExcedente.Size = New System.Drawing.Size(148, 24)
        Me.tbExcedente.TabIndex = 100
        Me.tbExcedente.WatermarkAlignment = DevComponents.Editors.eTextAlignment.Right
        '
        'tbCantVale
        '
        '
        '
        '
        Me.tbCantVale.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbCantVale.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbCantVale.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.tbCantVale.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.tbCantVale.Location = New System.Drawing.Point(159, 147)
        Me.tbCantVale.Name = "tbCantVale"
        Me.tbCantVale.Size = New System.Drawing.Size(148, 22)
        Me.tbCantVale.TabIndex = 2
        '
        'tbBeneficio
        '
        '
        '
        '
        Me.tbBeneficio.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbBeneficio.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbBeneficio.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.tbBeneficio.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbBeneficio.ForeColor = System.Drawing.Color.Black
        Me.tbBeneficio.Increment = 1.0R
        Me.tbBeneficio.Location = New System.Drawing.Point(323, 278)
        Me.tbBeneficio.MinValue = 0R
        Me.tbBeneficio.Name = "tbBeneficio"
        Me.tbBeneficio.Size = New System.Drawing.Size(148, 24)
        Me.tbBeneficio.TabIndex = 102
        Me.tbBeneficio.WatermarkAlignment = DevComponents.Editors.eTextAlignment.Right
        '
        'tbTotalVenta
        '
        '
        '
        '
        Me.tbTotalVenta.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbTotalVenta.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbTotalVenta.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.tbTotalVenta.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbTotalVenta.ForeColor = System.Drawing.Color.Black
        Me.tbTotalVenta.Increment = 1.0R
        Me.tbTotalVenta.Location = New System.Drawing.Point(323, 245)
        Me.tbTotalVenta.MinValue = 0R
        Me.tbTotalVenta.Name = "tbTotalVenta"
        Me.tbTotalVenta.Size = New System.Drawing.Size(148, 24)
        Me.tbTotalVenta.TabIndex = 101
        Me.tbTotalVenta.WatermarkAlignment = DevComponents.Editors.eTextAlignment.Right
        '
        'tbObs
        '
        Me.tbObs.AcceptsTab = True
        '
        '
        '
        Me.tbObs.Border.Class = "TextBoxBorder"
        Me.tbObs.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbObs.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbObs.Location = New System.Drawing.Point(159, 312)
        Me.tbObs.Multiline = True
        Me.tbObs.Name = "tbObs"
        Me.tbObs.PreventEnterBeep = True
        Me.tbObs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.tbObs.Size = New System.Drawing.Size(447, 42)
        Me.tbObs.TabIndex = 103
        '
        'LabelX8
        '
        Me.LabelX8.BackColor = System.Drawing.SystemColors.Control
        '
        '
        '
        Me.LabelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX8.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX8.Location = New System.Drawing.Point(16, 311)
        Me.LabelX8.Name = "LabelX8"
        Me.LabelX8.Size = New System.Drawing.Size(127, 23)
        Me.LabelX8.TabIndex = 183
        Me.LabelX8.Text = "OBSERVACIONES:"
        '
        'F_NombreVale
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(643, 438)
        Me.Controls.Add(Me.tbObs)
        Me.Controls.Add(Me.LabelX8)
        Me.Controls.Add(Me.tbTotalVenta)
        Me.Controls.Add(Me.tbBeneficio)
        Me.Controls.Add(Me.tbCantVale)
        Me.Controls.Add(Me.tbExcedente)
        Me.Controls.Add(Me.tbMontoVale)
        Me.Controls.Add(Me.LabelX7)
        Me.Controls.Add(Me.LabelX5)
        Me.Controls.Add(Me.tbCICli)
        Me.Controls.Add(Me.LabelX4)
        Me.Controls.Add(Me.tbNombreCli)
        Me.Controls.Add(Me.LabelX3)
        Me.Controls.Add(Me.LabelX2)
        Me.Controls.Add(Me.tbNrovale)
        Me.Controls.Add(Me.LabelX1)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnAceptar)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.tbNombre)
        Me.Controls.Add(Me.LabelX6)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "F_NombreVale"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CREAR NUEVO CLIENTE"
        Me.Panel1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MEP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbMontoVale, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbExcedente, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbCantVale, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbBeneficio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbTotalVenta, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tbNombre As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX6 As DevComponents.DotNetBar.LabelX
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ReflectionLabel1 As DevComponents.DotNetBar.Controls.ReflectionLabel
    Friend WithEvents btnAceptar As DevComponents.DotNetBar.ButtonX
    Friend WithEvents MEP As System.Windows.Forms.ErrorProvider
    Friend WithEvents MHighlighterFocus As DevComponents.DotNetBar.Validator.Highlighter
    Friend WithEvents MFlyoutUsuario As DevComponents.DotNetBar.Controls.Flyout
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents btnCancelar As DevComponents.DotNetBar.ButtonX
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX7 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbCICli As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbNombreCli As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbNrovale As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents tbExcedente As DevComponents.Editors.DoubleInput
    Friend WithEvents tbMontoVale As DevComponents.Editors.DoubleInput
    Friend WithEvents tbCantVale As DevComponents.Editors.IntegerInput
    Friend WithEvents tbBeneficio As DevComponents.Editors.DoubleInput
    Friend WithEvents tbTotalVenta As DevComponents.Editors.DoubleInput
    Friend WithEvents tbObs As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX8 As DevComponents.DotNetBar.LabelX
End Class
