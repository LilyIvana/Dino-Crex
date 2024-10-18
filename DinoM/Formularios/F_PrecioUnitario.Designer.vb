<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class F_PrecioUnitario
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ReflectionLabel1 = New DevComponents.DotNetBar.Controls.ReflectionLabel()
        Me.lbProducto = New System.Windows.Forms.Label()
        Me.lbConversion = New System.Windows.Forms.Label()
        Me.tbPrecioUni = New DevComponents.Editors.DoubleInput()
        Me.btnOk = New DevComponents.DotNetBar.ButtonX()
        Me.Panel1.SuspendLayout()
        CType(Me.tbPrecioUni, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackgroundImage = Global.DinoM.My.Resources.Resources.fondo1
        Me.Panel1.Controls.Add(Me.ReflectionLabel1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(465, 55)
        Me.Panel1.TabIndex = 0
        '
        'ReflectionLabel1
        '
        Me.ReflectionLabel1.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.ReflectionLabel1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.ReflectionLabel1.Font = New System.Drawing.Font("Calibri", 16.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ReflectionLabel1.ForeColor = System.Drawing.Color.White
        Me.ReflectionLabel1.Location = New System.Drawing.Point(11, 10)
        Me.ReflectionLabel1.Margin = New System.Windows.Forms.Padding(2)
        Me.ReflectionLabel1.Name = "ReflectionLabel1"
        Me.ReflectionLabel1.Size = New System.Drawing.Size(278, 43)
        Me.ReflectionLabel1.TabIndex = 0
        Me.ReflectionLabel1.Text = "Información de Precio Unitario"
        '
        'lbProducto
        '
        Me.lbProducto.Font = New System.Drawing.Font("Calibri", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbProducto.ForeColor = System.Drawing.Color.Navy
        Me.lbProducto.Location = New System.Drawing.Point(11, 58)
        Me.lbProducto.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbProducto.Name = "lbProducto"
        Me.lbProducto.Size = New System.Drawing.Size(440, 70)
        Me.lbProducto.TabIndex = 1
        Me.lbProducto.Text = "Producto A"
        Me.lbProducto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbConversion
        '
        Me.lbConversion.BackColor = System.Drawing.Color.Transparent
        Me.lbConversion.Font = New System.Drawing.Font("Calibri", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbConversion.ForeColor = System.Drawing.Color.SteelBlue
        Me.lbConversion.Location = New System.Drawing.Point(95, 146)
        Me.lbConversion.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbConversion.Name = "lbConversion"
        Me.lbConversion.Size = New System.Drawing.Size(144, 26)
        Me.lbConversion.TabIndex = 3
        Me.lbConversion.Text = "Precio Unitario:"
        Me.lbConversion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbPrecioUni
        '
        '
        '
        '
        Me.tbPrecioUni.BackgroundStyle.BackColor = System.Drawing.Color.SteelBlue
        Me.tbPrecioUni.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbPrecioUni.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbPrecioUni.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.tbPrecioUni.Font = New System.Drawing.Font("Calibri", 20.0!, System.Drawing.FontStyle.Bold)
        Me.tbPrecioUni.ForeColor = System.Drawing.Color.White
        Me.tbPrecioUni.Increment = 1.0R
        Me.tbPrecioUni.IsInputReadOnly = True
        Me.tbPrecioUni.Location = New System.Drawing.Point(245, 138)
        Me.tbPrecioUni.MinValue = 0R
        Me.tbPrecioUni.Name = "tbPrecioUni"
        Me.tbPrecioUni.Size = New System.Drawing.Size(112, 40)
        Me.tbPrecioUni.TabIndex = 4
        Me.tbPrecioUni.WatermarkAlignment = DevComponents.Editors.eTextAlignment.Right
        '
        'btnOk
        '
        Me.btnOk.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnOk.BackColor = System.Drawing.Color.Transparent
        Me.btnOk.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb
        Me.btnOk.FadeEffect = False
        Me.btnOk.FocusCuesEnabled = False
        Me.btnOk.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOk.ImageFixedSize = New System.Drawing.Size(20, 20)
        Me.btnOk.Location = New System.Drawing.Point(188, 196)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnOk.Shape = New DevComponents.DotNetBar.RoundRectangleShapeDescriptor(2)
        Me.btnOk.Size = New System.Drawing.Size(76, 41)
        Me.btnOk.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnOk.TabIndex = 5
        Me.btnOk.Text = "OK"
        Me.btnOk.TextColor = System.Drawing.Color.White
        '
        'F_PrecioUnitario
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(465, 251)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.tbPrecioUni)
        Me.Controls.Add(Me.lbConversion)
        Me.Controls.Add(Me.lbProducto)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "F_PrecioUnitario"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "F_Cantidad"
        Me.Panel1.ResumeLayout(False)
        CType(Me.tbPrecioUni, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents lbProducto As Label
    Friend WithEvents ReflectionLabel1 As DevComponents.DotNetBar.Controls.ReflectionLabel
    Friend WithEvents lbConversion As Label
    Public WithEvents tbPrecioUni As DevComponents.Editors.DoubleInput
    Friend WithEvents btnOk As DevComponents.DotNetBar.ButtonX
End Class
