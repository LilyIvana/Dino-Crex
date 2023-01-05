<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Descuentos
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
        Me.components = New System.ComponentModel.Container()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.GroupPanel2 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.grProducto = New Janus.Windows.GridEX.GridEX()
        Me.GroupPanel1 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.grdetalle = New Janus.Windows.GridEX.GridEX()
        Me.MenuEliminar = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.lbProducto = New DevComponents.DotNetBar.LabelX()
        Me.tbPrecio = New DevComponents.Editors.DoubleInput()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.tbHasta = New DevComponents.Editors.IntegerInput()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.tbDesde = New DevComponents.Editors.IntegerInput()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.EliminarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnGrabar = New DevComponents.DotNetBar.ButtonX()
        Me.btnNuevo = New DevComponents.DotNetBar.ButtonX()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonX1 = New DevComponents.DotNetBar.ButtonX()
        Me.Panel2.SuspendLayout()
        Me.GroupPanel2.SuspendLayout()
        CType(Me.grProducto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupPanel1.SuspendLayout()
        CType(Me.grdetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuEliminar.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.tbPrecio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbHasta, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbDesde, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.Controls.Add(Me.GroupPanel2)
        Me.Panel2.Controls.Add(Me.GroupPanel1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 61)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(800, 360)
        Me.Panel2.TabIndex = 0
        '
        'GroupPanel2
        '
        Me.GroupPanel2.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel2.Controls.Add(Me.grProducto)
        Me.GroupPanel2.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupPanel2.Location = New System.Drawing.Point(0, 0)
        Me.GroupPanel2.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupPanel2.Name = "GroupPanel2"
        Me.GroupPanel2.Size = New System.Drawing.Size(364, 360)
        '
        '
        '
        Me.GroupPanel2.Style.BackColor = System.Drawing.Color.Black
        Me.GroupPanel2.Style.BackColor2 = System.Drawing.Color.DarkBlue
        Me.GroupPanel2.Style.BackColorGradientAngle = 90
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
        Me.GroupPanel2.Style.Font = New System.Drawing.Font("Calibri", 10.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupPanel2.Style.TextColor = System.Drawing.Color.White
        '
        '
        '
        Me.GroupPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanel2.TabIndex = 1
        Me.GroupPanel2.Text = "LISTADO DE PRODUCTOS"
        '
        'grProducto
        '
        Me.grProducto.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grProducto.ColumnAutoResize = True
        Me.grProducto.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grProducto.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight
        Me.grProducto.Location = New System.Drawing.Point(0, 0)
        Me.grProducto.Margin = New System.Windows.Forms.Padding(2)
        Me.grProducto.Name = "grProducto"
        Me.grProducto.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.grProducto.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.grProducto.Size = New System.Drawing.Size(358, 334)
        Me.grProducto.TabIndex = 0
        '
        'GroupPanel1
        '
        Me.GroupPanel1.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel1.Controls.Add(Me.grdetalle)
        Me.GroupPanel1.Controls.Add(Me.Panel3)
        Me.GroupPanel1.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel1.Dock = System.Windows.Forms.DockStyle.Right
        Me.GroupPanel1.Font = New System.Drawing.Font("Calibri", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupPanel1.Location = New System.Drawing.Point(364, 0)
        Me.GroupPanel1.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupPanel1.Name = "GroupPanel1"
        Me.GroupPanel1.Size = New System.Drawing.Size(436, 360)
        '
        '
        '
        Me.GroupPanel1.Style.BackColor = System.Drawing.Color.DarkBlue
        Me.GroupPanel1.Style.BackColor2 = System.Drawing.Color.DarkBlue
        Me.GroupPanel1.Style.BackColorGradientAngle = 90
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
        Me.GroupPanel1.Style.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanel1.Style.TextColor = System.Drawing.Color.White
        Me.GroupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanel1.TabIndex = 0
        Me.GroupPanel1.Text = "Detalle Descuento"
        '
        'grdetalle
        '
        Me.grdetalle.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdetalle.ColumnAutoResize = True
        Me.grdetalle.ContextMenuStrip = Me.MenuEliminar
        Me.grdetalle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdetalle.Location = New System.Drawing.Point(0, 188)
        Me.grdetalle.Margin = New System.Windows.Forms.Padding(2)
        Me.grdetalle.Name = "grdetalle"
        Me.grdetalle.Size = New System.Drawing.Size(430, 144)
        Me.grdetalle.TabIndex = 1
        '
        'MenuEliminar
        '
        Me.MenuEliminar.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuEliminar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EliminarToolStripMenuItem})
        Me.MenuEliminar.Name = "MenuEliminar"
        Me.MenuEliminar.Size = New System.Drawing.Size(122, 30)
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.lbProducto)
        Me.Panel3.Controls.Add(Me.tbPrecio)
        Me.Panel3.Controls.Add(Me.LabelX3)
        Me.Panel3.Controls.Add(Me.tbHasta)
        Me.Panel3.Controls.Add(Me.LabelX1)
        Me.Panel3.Controls.Add(Me.tbDesde)
        Me.Panel3.Controls.Add(Me.LabelX2)
        Me.Panel3.Controls.Add(Me.btnGrabar)
        Me.Panel3.Controls.Add(Me.btnNuevo)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(430, 188)
        Me.Panel3.TabIndex = 0
        '
        'lbProducto
        '
        Me.lbProducto.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.lbProducto.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lbProducto.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbProducto.ForeColor = System.Drawing.Color.Black
        Me.lbProducto.Location = New System.Drawing.Point(20, 3)
        Me.lbProducto.Name = "lbProducto"
        Me.lbProducto.SingleLineColor = System.Drawing.SystemColors.Control
        Me.lbProducto.Size = New System.Drawing.Size(392, 56)
        Me.lbProducto.TabIndex = 231
        Me.lbProducto.TextAlignment = System.Drawing.StringAlignment.Center
        '
        'tbPrecio
        '
        '
        '
        '
        Me.tbPrecio.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbPrecio.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbPrecio.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.tbPrecio.Font = New System.Drawing.Font("Arial", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPrecio.Increment = 1.0R
        Me.tbPrecio.Location = New System.Drawing.Point(124, 104)
        Me.tbPrecio.Margin = New System.Windows.Forms.Padding(2)
        Me.tbPrecio.Name = "tbPrecio"
        Me.tbPrecio.Size = New System.Drawing.Size(80, 24)
        Me.tbPrecio.TabIndex = 7
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
        Me.LabelX3.Location = New System.Drawing.Point(74, 104)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX3.Size = New System.Drawing.Size(47, 16)
        Me.LabelX3.TabIndex = 230
        Me.LabelX3.Text = "Precio:"
        '
        'tbHasta
        '
        '
        '
        '
        Me.tbHasta.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbHasta.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbHasta.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.tbHasta.Font = New System.Drawing.Font("Arial", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbHasta.Location = New System.Drawing.Point(266, 65)
        Me.tbHasta.Margin = New System.Windows.Forms.Padding(2)
        Me.tbHasta.Name = "tbHasta"
        Me.tbHasta.Size = New System.Drawing.Size(82, 24)
        Me.tbHasta.TabIndex = 6
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
        Me.LabelX1.Location = New System.Drawing.Point(219, 67)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX1.Size = New System.Drawing.Size(43, 16)
        Me.LabelX1.TabIndex = 228
        Me.LabelX1.Text = "Hasta:"
        '
        'tbDesde
        '
        '
        '
        '
        Me.tbDesde.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbDesde.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbDesde.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.tbDesde.Font = New System.Drawing.Font("Arial", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbDesde.Location = New System.Drawing.Point(122, 65)
        Me.tbDesde.Margin = New System.Windows.Forms.Padding(2)
        Me.tbDesde.Name = "tbDesde"
        Me.tbDesde.Size = New System.Drawing.Size(82, 24)
        Me.tbDesde.TabIndex = 5
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
        Me.LabelX2.Location = New System.Drawing.Point(74, 67)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX2.Size = New System.Drawing.Size(45, 16)
        Me.LabelX2.TabIndex = 226
        Me.LabelX2.Text = "Desde:"
        '
        'EliminarToolStripMenuItem
        '
        Me.EliminarToolStripMenuItem.Image = Global.DinoM.My.Resources.Resources.trash
        Me.EliminarToolStripMenuItem.Name = "EliminarToolStripMenuItem"
        Me.EliminarToolStripMenuItem.Size = New System.Drawing.Size(121, 26)
        Me.EliminarToolStripMenuItem.Text = "Eliminar"
        '
        'btnGrabar
        '
        Me.btnGrabar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnGrabar.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground
        Me.btnGrabar.Font = New System.Drawing.Font("Calibri", 10.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGrabar.Image = Global.DinoM.My.Resources.Resources.save
        Me.btnGrabar.ImageFixedSize = New System.Drawing.Size(25, 25)
        Me.btnGrabar.ImageTextSpacing = 5
        Me.btnGrabar.Location = New System.Drawing.Point(247, 136)
        Me.btnGrabar.Margin = New System.Windows.Forms.Padding(2)
        Me.btnGrabar.Name = "btnGrabar"
        Me.btnGrabar.Size = New System.Drawing.Size(122, 38)
        Me.btnGrabar.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2013
        Me.btnGrabar.TabIndex = 8
        Me.btnGrabar.Text = "Grabar"
        Me.btnGrabar.TextColor = System.Drawing.Color.MidnightBlue
        '
        'btnNuevo
        '
        Me.btnNuevo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnNuevo.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground
        Me.btnNuevo.Font = New System.Drawing.Font("Calibri", 10.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNuevo.Image = Global.DinoM.My.Resources.Resources.add
        Me.btnNuevo.ImageFixedSize = New System.Drawing.Size(25, 25)
        Me.btnNuevo.ImageTextSpacing = 5
        Me.btnNuevo.Location = New System.Drawing.Point(82, 136)
        Me.btnNuevo.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNuevo.Name = "btnNuevo"
        Me.btnNuevo.Size = New System.Drawing.Size(122, 38)
        Me.btnNuevo.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2013
        Me.btnNuevo.TabIndex = 4
        Me.btnNuevo.Text = "Nuevo"
        Me.btnNuevo.TextColor = System.Drawing.Color.MidnightBlue
        '
        'Panel1
        '
        Me.Panel1.BackgroundImage = Global.DinoM.My.Resources.Resources.fondo1
        Me.Panel1.Controls.Add(Me.ButtonX1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(800, 61)
        Me.Panel1.TabIndex = 0
        '
        'ButtonX1
        '
        Me.ButtonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb
        Me.ButtonX1.Font = New System.Drawing.Font("Calibri", 10.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonX1.Image = Global.DinoM.My.Resources.Resources.atras1
        Me.ButtonX1.ImageFixedSize = New System.Drawing.Size(25, 25)
        Me.ButtonX1.ImageTextSpacing = 5
        Me.ButtonX1.Location = New System.Drawing.Point(21, 10)
        Me.ButtonX1.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonX1.Name = "ButtonX1"
        Me.ButtonX1.Size = New System.Drawing.Size(122, 38)
        Me.ButtonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2013
        Me.ButtonX1.TabIndex = 5
        Me.ButtonX1.Text = "Salir"
        Me.ButtonX1.TextColor = System.Drawing.Color.White
        '
        'Descuentos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 421)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "Descuentos"
        Me.Text = "Descuentos"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel2.ResumeLayout(False)
        Me.GroupPanel2.ResumeLayout(False)
        CType(Me.grProducto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupPanel1.ResumeLayout(False)
        CType(Me.grdetalle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuEliminar.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.tbPrecio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbHasta, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbDesde, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents GroupPanel1 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents grdetalle As Janus.Windows.GridEX.GridEX
    Friend WithEvents Panel3 As Panel
    Friend WithEvents GroupPanel2 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents grProducto As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnGrabar As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnNuevo As DevComponents.DotNetBar.ButtonX
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbDesde As DevComponents.Editors.IntegerInput
    Friend WithEvents tbHasta As DevComponents.Editors.IntegerInput
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbPrecio As DevComponents.Editors.DoubleInput
    Friend WithEvents lbProducto As DevComponents.DotNetBar.LabelX
    Friend WithEvents ButtonX1 As DevComponents.DotNetBar.ButtonX
    Friend WithEvents MenuEliminar As ContextMenuStrip
    Friend WithEvents EliminarToolStripMenuItem As ToolStripMenuItem
End Class
