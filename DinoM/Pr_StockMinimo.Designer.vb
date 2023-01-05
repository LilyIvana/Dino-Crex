<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Pr_StockMinimo
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
        Dim cbAlmacen_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Pr_StockMinimo))
        Dim cbGrupos_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.cbAlmacen = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.cbGrupos = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.CheckTodosAlmacen = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.checkUnaAlmacen = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.checkTodosGrupos = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.checkUnaGrupo = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.MReportViewer = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.btn_Salir = New System.Windows.Forms.Button()
        Me.btn_Generar = New System.Windows.Forms.Button()
        CType(Me.cbAlmacen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbGrupos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'cbAlmacen
        '
        Me.cbAlmacen.BackColor = System.Drawing.Color.White
        cbAlmacen_DesignTimeLayout.LayoutString = resources.GetString("cbAlmacen_DesignTimeLayout.LayoutString")
        Me.cbAlmacen.DesignTimeLayout = cbAlmacen_DesignTimeLayout
        Me.cbAlmacen.DisabledBackColor = System.Drawing.Color.White
        Me.cbAlmacen.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAlmacen.Location = New System.Drawing.Point(106, 17)
        Me.cbAlmacen.Margin = New System.Windows.Forms.Padding(4)
        Me.cbAlmacen.Name = "cbAlmacen"
        Me.cbAlmacen.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.cbAlmacen.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.cbAlmacen.ReadOnly = True
        Me.cbAlmacen.SelectedIndex = -1
        Me.cbAlmacen.SelectedItem = Nothing
        Me.cbAlmacen.Size = New System.Drawing.Size(219, 26)
        Me.cbAlmacen.TabIndex = 248
        Me.cbAlmacen.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'cbGrupos
        '
        Me.cbGrupos.BackColor = System.Drawing.Color.White
        cbGrupos_DesignTimeLayout.LayoutString = resources.GetString("cbGrupos_DesignTimeLayout.LayoutString")
        Me.cbGrupos.DesignTimeLayout = cbGrupos_DesignTimeLayout
        Me.cbGrupos.DisabledBackColor = System.Drawing.Color.White
        Me.cbGrupos.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbGrupos.Location = New System.Drawing.Point(105, 88)
        Me.cbGrupos.Margin = New System.Windows.Forms.Padding(4)
        Me.cbGrupos.Name = "cbGrupos"
        Me.cbGrupos.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.cbGrupos.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.cbGrupos.ReadOnly = True
        Me.cbGrupos.SelectedIndex = -1
        Me.cbGrupos.SelectedItem = Nothing
        Me.cbGrupos.Size = New System.Drawing.Size(219, 26)
        Me.cbGrupos.TabIndex = 250
        Me.cbGrupos.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.CheckTodosAlmacen)
        Me.Panel1.Controls.Add(Me.checkUnaAlmacen)
        Me.Panel1.Location = New System.Drawing.Point(326, 6)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(160, 43)
        Me.Panel1.TabIndex = 256
        '
        'CheckTodosAlmacen
        '
        '
        '
        '
        Me.CheckTodosAlmacen.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.CheckTodosAlmacen.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.CheckTodosAlmacen.Checked = True
        Me.CheckTodosAlmacen.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckTodosAlmacen.CheckValue = "Y"
        Me.CheckTodosAlmacen.Location = New System.Drawing.Point(83, 9)
        Me.CheckTodosAlmacen.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckTodosAlmacen.Name = "CheckTodosAlmacen"
        Me.CheckTodosAlmacen.Size = New System.Drawing.Size(73, 28)
        Me.CheckTodosAlmacen.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.CheckTodosAlmacen.TabIndex = 252
        Me.CheckTodosAlmacen.Text = "Todos"
        '
        'checkUnaAlmacen
        '
        '
        '
        '
        Me.checkUnaAlmacen.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.checkUnaAlmacen.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.checkUnaAlmacen.Location = New System.Drawing.Point(16, 9)
        Me.checkUnaAlmacen.Margin = New System.Windows.Forms.Padding(4)
        Me.checkUnaAlmacen.Name = "checkUnaAlmacen"
        Me.checkUnaAlmacen.Size = New System.Drawing.Size(59, 28)
        Me.checkUnaAlmacen.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.checkUnaAlmacen.TabIndex = 251
        Me.checkUnaAlmacen.Text = "Una"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.checkTodosGrupos)
        Me.Panel2.Controls.Add(Me.checkUnaGrupo)
        Me.Panel2.Location = New System.Drawing.Point(325, 76)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(159, 43)
        Me.Panel2.TabIndex = 257
        '
        'checkTodosGrupos
        '
        '
        '
        '
        Me.checkTodosGrupos.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.checkTodosGrupos.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.checkTodosGrupos.Checked = True
        Me.checkTodosGrupos.CheckState = System.Windows.Forms.CheckState.Checked
        Me.checkTodosGrupos.CheckValue = "Y"
        Me.checkTodosGrupos.Location = New System.Drawing.Point(80, 9)
        Me.checkTodosGrupos.Margin = New System.Windows.Forms.Padding(4)
        Me.checkTodosGrupos.Name = "checkTodosGrupos"
        Me.checkTodosGrupos.Size = New System.Drawing.Size(73, 28)
        Me.checkTodosGrupos.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.checkTodosGrupos.TabIndex = 252
        Me.checkTodosGrupos.Text = "Todos"
        '
        'checkUnaGrupo
        '
        '
        '
        '
        Me.checkUnaGrupo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.checkUnaGrupo.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.checkUnaGrupo.Location = New System.Drawing.Point(13, 9)
        Me.checkUnaGrupo.Margin = New System.Windows.Forms.Padding(4)
        Me.checkUnaGrupo.Name = "checkUnaGrupo"
        Me.checkUnaGrupo.Size = New System.Drawing.Size(59, 28)
        Me.checkUnaGrupo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.checkUnaGrupo.TabIndex = 251
        Me.checkUnaGrupo.Text = "Una"
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
        Me.LabelX1.Location = New System.Drawing.Point(8, 90)
        Me.LabelX1.Margin = New System.Windows.Forms.Padding(4)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX1.Size = New System.Drawing.Size(89, 20)
        Me.LabelX1.TabIndex = 259
        Me.LabelX1.Text = "Proveedor:"
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
        Me.LabelX3.Location = New System.Drawing.Point(5, 20)
        Me.LabelX3.Margin = New System.Windows.Forms.Padding(4)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX3.Size = New System.Drawing.Size(76, 20)
        Me.LabelX3.TabIndex = 258
        Me.LabelX3.Text = "Almacen:"
        '
        'MReportViewer
        '
        Me.MReportViewer.ActiveViewIndex = -1
        Me.MReportViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.MReportViewer.Cursor = System.Windows.Forms.Cursors.Default
        Me.MReportViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MReportViewer.Location = New System.Drawing.Point(503, 0)
        Me.MReportViewer.Margin = New System.Windows.Forms.Padding(4)
        Me.MReportViewer.Name = "MReportViewer"
        Me.MReportViewer.Size = New System.Drawing.Size(806, 730)
        Me.MReportViewer.TabIndex = 0
        Me.MReportViewer.ToolPanelWidth = 267
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Gainsboro
        Me.Panel5.Controls.Add(Me.LabelX1)
        Me.Panel5.Controls.Add(Me.LabelX3)
        Me.Panel5.Controls.Add(Me.Panel2)
        Me.Panel5.Controls.Add(Me.Panel1)
        Me.Panel5.Controls.Add(Me.cbGrupos)
        Me.Panel5.Controls.Add(Me.cbAlmacen)
        Me.Panel5.Location = New System.Drawing.Point(0, 136)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(503, 594)
        Me.Panel5.TabIndex = 264
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.Panel5)
        Me.Panel6.Controls.Add(Me.Panel4)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(503, 730)
        Me.Panel6.TabIndex = 1
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Transparent
        Me.Panel4.BackgroundImage = Global.DinoM.My.Resources.Resources.fondo1
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.btn_Salir)
        Me.Panel4.Controls.Add(Me.btn_Generar)
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(503, 730)
        Me.Panel4.TabIndex = 263
        '
        'btn_Salir
        '
        Me.btn_Salir.BackColor = System.Drawing.Color.Transparent
        Me.btn_Salir.FlatAppearance.BorderSize = 0
        Me.btn_Salir.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Salir.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Salir.ForeColor = System.Drawing.Color.White
        Me.btn_Salir.Image = Global.DinoM.My.Resources.Resources.atras1
        Me.btn_Salir.Location = New System.Drawing.Point(231, -2)
        Me.btn_Salir.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_Salir.Name = "btn_Salir"
        Me.btn_Salir.Size = New System.Drawing.Size(96, 121)
        Me.btn_Salir.TabIndex = 262
        Me.btn_Salir.Text = "SALIR"
        Me.btn_Salir.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btn_Salir.UseVisualStyleBackColor = False
        '
        'btn_Generar
        '
        Me.btn_Generar.FlatAppearance.BorderSize = 0
        Me.btn_Generar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Generar.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Generar.ForeColor = System.Drawing.Color.White
        Me.btn_Generar.Image = Global.DinoM.My.Resources.Resources.pie_chart1
        Me.btn_Generar.Location = New System.Drawing.Point(106, 0)
        Me.btn_Generar.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_Generar.Name = "btn_Generar"
        Me.btn_Generar.Size = New System.Drawing.Size(122, 127)
        Me.btn_Generar.TabIndex = 261
        Me.btn_Generar.Text = "GENERAR"
        Me.btn_Generar.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btn_Generar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btn_Generar.UseVisualStyleBackColor = True
        '
        'Pr_StockMinimo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1309, 730)
        Me.Controls.Add(Me.MReportViewer)
        Me.Controls.Add(Me.Panel6)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Pr_StockMinimo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pr_StockMinimo"
        CType(Me.cbAlmacen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbGrupos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents cbAlmacen As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents cbGrupos As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents Panel1 As Panel
    Friend WithEvents CheckTodosAlmacen As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents checkUnaAlmacen As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents Panel2 As Panel
    Friend WithEvents checkTodosGrupos As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents checkUnaGrupo As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents MReportViewer As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents btn_Generar As Button
    Friend WithEvents btn_Salir As Button
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel6 As Panel
End Class
