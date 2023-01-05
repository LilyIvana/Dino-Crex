<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_Inventario
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
        Dim cbDeposito_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F_Inventario))
        Me.btnGenerar = New DevComponents.DotNetBar.ButtonX()
        Me.cbDeposito = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.lbDepositoOrigen = New DevComponents.DotNetBar.LabelX()
        Me.btnEgresos = New DevComponents.DotNetBar.ButtonX()
        CType(Me.cbDeposito, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnGenerar
        '
        Me.btnGenerar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnGenerar.BackColor = System.Drawing.Color.White
        Me.btnGenerar.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground
        Me.btnGenerar.FadeEffect = False
        Me.btnGenerar.FocusCuesEnabled = False
        Me.btnGenerar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGenerar.Image = Global.DinoM.My.Resources.Resources.GRABACION_EXITOSA
        Me.btnGenerar.ImageFixedSize = New System.Drawing.Size(20, 20)
        Me.btnGenerar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top
        Me.btnGenerar.Location = New System.Drawing.Point(71, 147)
        Me.btnGenerar.Name = "btnGenerar"
        Me.btnGenerar.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnGenerar.Size = New System.Drawing.Size(160, 55)
        Me.btnGenerar.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010
        Me.btnGenerar.TabIndex = 264
        Me.btnGenerar.Text = "Ejecutar Ingresos"
        '
        'cbDeposito
        '
        cbDeposito_DesignTimeLayout.LayoutString = resources.GetString("cbDeposito_DesignTimeLayout.LayoutString")
        Me.cbDeposito.DesignTimeLayout = cbDeposito_DesignTimeLayout
        Me.cbDeposito.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbDeposito.Location = New System.Drawing.Point(167, 79)
        Me.cbDeposito.Name = "cbDeposito"
        Me.cbDeposito.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.cbDeposito.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.cbDeposito.SelectedIndex = -1
        Me.cbDeposito.SelectedItem = Nothing
        Me.cbDeposito.Size = New System.Drawing.Size(176, 22)
        Me.cbDeposito.TabIndex = 265
        Me.cbDeposito.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'lbDepositoOrigen
        '
        Me.lbDepositoOrigen.AutoSize = True
        Me.lbDepositoOrigen.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.lbDepositoOrigen.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lbDepositoOrigen.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbDepositoOrigen.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.lbDepositoOrigen.Location = New System.Drawing.Point(167, 59)
        Me.lbDepositoOrigen.Name = "lbDepositoOrigen"
        Me.lbDepositoOrigen.SingleLineColor = System.Drawing.SystemColors.Control
        Me.lbDepositoOrigen.Size = New System.Drawing.Size(62, 16)
        Me.lbDepositoOrigen.TabIndex = 266
        Me.lbDepositoOrigen.Text = "Deposito:"
        '
        'btnEgresos
        '
        Me.btnEgresos.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnEgresos.BackColor = System.Drawing.Color.White
        Me.btnEgresos.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground
        Me.btnEgresos.Enabled = False
        Me.btnEgresos.FadeEffect = False
        Me.btnEgresos.FocusCuesEnabled = False
        Me.btnEgresos.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEgresos.Image = Global.DinoM.My.Resources.Resources.GRABACION_EXITOSA
        Me.btnEgresos.ImageFixedSize = New System.Drawing.Size(20, 20)
        Me.btnEgresos.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top
        Me.btnEgresos.Location = New System.Drawing.Point(259, 147)
        Me.btnEgresos.Name = "btnEgresos"
        Me.btnEgresos.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnEgresos.Size = New System.Drawing.Size(160, 55)
        Me.btnEgresos.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010
        Me.btnEgresos.TabIndex = 267
        Me.btnEgresos.Text = "Ejecutar Egresos"
        '
        'F_Inventario
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(562, 359)
        Me.Controls.Add(Me.btnEgresos)
        Me.Controls.Add(Me.cbDeposito)
        Me.Controls.Add(Me.lbDepositoOrigen)
        Me.Controls.Add(Me.btnGenerar)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "F_Inventario"
        Me.Text = "F_Inventario"
        CType(Me.cbDeposito, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnGenerar As DevComponents.DotNetBar.ButtonX
    Friend WithEvents cbDeposito As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents lbDepositoOrigen As DevComponents.DotNetBar.LabelX
    Friend WithEvents btnEgresos As DevComponents.DotNetBar.ButtonX
End Class
