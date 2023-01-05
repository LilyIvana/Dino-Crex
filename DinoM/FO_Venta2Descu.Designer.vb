<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FO_Venta2Descu
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
        Me.gpDetalleVenta = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.grDescuento = New Janus.Windows.GridEX.GridEX()
        Me.gpDetalleVenta.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.grDescuento, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.gpDetalleVenta.Size = New System.Drawing.Size(583, 450)
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
        Me.gpDetalleVenta.TabIndex = 4
        Me.gpDetalleVenta.Text = "DESCUENTO"
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.White
        Me.Panel5.Controls.Add(Me.grDescuento)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(0, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(577, 427)
        Me.Panel5.TabIndex = 0
        '
        'grDescuento
        '
        Me.grDescuento.BackColor = System.Drawing.Color.GhostWhite
        Me.grDescuento.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grDescuento.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grDescuento.HeaderFormatStyle.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold)
        Me.grDescuento.Location = New System.Drawing.Point(0, 0)
        Me.grDescuento.Name = "grDescuento"
        Me.grDescuento.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.grDescuento.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.grDescuento.Size = New System.Drawing.Size(577, 427)
        Me.grDescuento.TabIndex = 3
        Me.grDescuento.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'FO_Venta2Descu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(583, 450)
        Me.Controls.Add(Me.gpDetalleVenta)
        Me.MaximizeBox = False
        Me.Name = "FO_Venta2Descu"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Descuentos"
        Me.gpDetalleVenta.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        CType(Me.grDescuento, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents gpDetalleVenta As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents grDescuento As Janus.Windows.GridEX.GridEX
End Class
