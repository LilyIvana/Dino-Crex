﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
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
        Me.swMostrar = New DevComponents.DotNetBar.Controls.SwitchButton()
        Me.GroupPanelBuscador = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.grBuscador = New Janus.Windows.GridEX.GridEX()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.btn_Generar = New DevComponents.DotNetBar.ButtonX()
        Me.btn_Salir = New DevComponents.DotNetBar.ButtonX()
        Me.btnExportar = New DevComponents.DotNetBar.ButtonX()
        CType(Me.cbAlmacen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbGrupos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.GroupPanelBuscador.SuspendLayout()
        CType(Me.grBuscador, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel6.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'cbAlmacen
        '
        Me.cbAlmacen.BackColor = System.Drawing.Color.White
        cbAlmacen_DesignTimeLayout.LayoutString = resources.GetString("cbAlmacen_DesignTimeLayout.LayoutString")
        Me.cbAlmacen.DesignTimeLayout = cbAlmacen_DesignTimeLayout
        Me.cbAlmacen.DisabledBackColor = System.Drawing.Color.White
        Me.cbAlmacen.Enabled = False
        Me.cbAlmacen.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAlmacen.Location = New System.Drawing.Point(80, 14)
        Me.cbAlmacen.Name = "cbAlmacen"
        Me.cbAlmacen.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.cbAlmacen.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.cbAlmacen.ReadOnly = True
        Me.cbAlmacen.SelectedIndex = -1
        Me.cbAlmacen.SelectedItem = Nothing
        Me.cbAlmacen.Size = New System.Drawing.Size(164, 22)
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
        Me.cbGrupos.Location = New System.Drawing.Point(79, 72)
        Me.cbGrupos.Name = "cbGrupos"
        Me.cbGrupos.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.cbGrupos.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.cbGrupos.ReadOnly = True
        Me.cbGrupos.SelectedIndex = -1
        Me.cbGrupos.SelectedItem = Nothing
        Me.cbGrupos.Size = New System.Drawing.Size(164, 22)
        Me.cbGrupos.TabIndex = 250
        Me.cbGrupos.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.CheckTodosAlmacen)
        Me.Panel1.Controls.Add(Me.checkUnaAlmacen)
        Me.Panel1.Location = New System.Drawing.Point(244, 5)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(120, 35)
        Me.Panel1.TabIndex = 256
        Me.Panel1.Visible = False
        '
        'CheckTodosAlmacen
        '
        '
        '
        '
        Me.CheckTodosAlmacen.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.CheckTodosAlmacen.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.CheckTodosAlmacen.Location = New System.Drawing.Point(62, 7)
        Me.CheckTodosAlmacen.Name = "CheckTodosAlmacen"
        Me.CheckTodosAlmacen.Size = New System.Drawing.Size(55, 23)
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
        Me.checkUnaAlmacen.Checked = True
        Me.checkUnaAlmacen.CheckState = System.Windows.Forms.CheckState.Checked
        Me.checkUnaAlmacen.CheckValue = "Y"
        Me.checkUnaAlmacen.Location = New System.Drawing.Point(12, 7)
        Me.checkUnaAlmacen.Name = "checkUnaAlmacen"
        Me.checkUnaAlmacen.Size = New System.Drawing.Size(44, 23)
        Me.checkUnaAlmacen.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.checkUnaAlmacen.TabIndex = 251
        Me.checkUnaAlmacen.Text = "Una"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.checkTodosGrupos)
        Me.Panel2.Controls.Add(Me.checkUnaGrupo)
        Me.Panel2.Location = New System.Drawing.Point(244, 62)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(119, 35)
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
        Me.checkTodosGrupos.Location = New System.Drawing.Point(60, 7)
        Me.checkTodosGrupos.Name = "checkTodosGrupos"
        Me.checkTodosGrupos.Size = New System.Drawing.Size(55, 23)
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
        Me.checkUnaGrupo.Location = New System.Drawing.Point(10, 7)
        Me.checkUnaGrupo.Name = "checkUnaGrupo"
        Me.checkUnaGrupo.Size = New System.Drawing.Size(44, 23)
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
        Me.LabelX1.Location = New System.Drawing.Point(6, 73)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX1.Size = New System.Drawing.Size(72, 16)
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
        Me.LabelX3.Location = New System.Drawing.Point(4, 16)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX3.Size = New System.Drawing.Size(61, 16)
        Me.LabelX3.TabIndex = 258
        Me.LabelX3.Text = "Almacen:"
        '
        'MReportViewer
        '
        Me.MReportViewer.ActiveViewIndex = -1
        Me.MReportViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.MReportViewer.Cursor = System.Windows.Forms.Cursors.Default
        Me.MReportViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MReportViewer.Location = New System.Drawing.Point(377, 0)
        Me.MReportViewer.Name = "MReportViewer"
        Me.MReportViewer.Size = New System.Drawing.Size(967, 696)
        Me.MReportViewer.TabIndex = 0
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Gainsboro
        Me.Panel5.Controls.Add(Me.swMostrar)
        Me.Panel5.Controls.Add(Me.GroupPanelBuscador)
        Me.Panel5.Controls.Add(Me.LabelX1)
        Me.Panel5.Controls.Add(Me.LabelX3)
        Me.Panel5.Controls.Add(Me.Panel2)
        Me.Panel5.Controls.Add(Me.Panel1)
        Me.Panel5.Controls.Add(Me.cbGrupos)
        Me.Panel5.Controls.Add(Me.cbAlmacen)
        Me.Panel5.Location = New System.Drawing.Point(0, 110)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(377, 591)
        Me.Panel5.TabIndex = 264
        '
        'swMostrar
        '
        Me.swMostrar.BackColor = System.Drawing.Color.LightGray
        '
        '
        '
        Me.swMostrar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.swMostrar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.swMostrar.Location = New System.Drawing.Point(58, 124)
        Me.swMostrar.Name = "swMostrar"
        Me.swMostrar.OffBackColor = System.Drawing.Color.Goldenrod
        Me.swMostrar.OffText = "SOLO PROVEEDORES"
        Me.swMostrar.OffTextColor = System.Drawing.Color.White
        Me.swMostrar.OnBackColor = System.Drawing.Color.Green
        Me.swMostrar.OnText = "TODOS LOS PRODUCTOS"
        Me.swMostrar.OnTextColor = System.Drawing.Color.White
        Me.swMostrar.Size = New System.Drawing.Size(250, 22)
        Me.swMostrar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.swMostrar.TabIndex = 261
        Me.swMostrar.Value = True
        Me.swMostrar.ValueObject = "Y"
        '
        'GroupPanelBuscador
        '
        Me.GroupPanelBuscador.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanelBuscador.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanelBuscador.Controls.Add(Me.grBuscador)
        Me.GroupPanelBuscador.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanelBuscador.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupPanelBuscador.Location = New System.Drawing.Point(79, 172)
        Me.GroupPanelBuscador.Name = "GroupPanelBuscador"
        Me.GroupPanelBuscador.Size = New System.Drawing.Size(200, 85)
        '
        '
        '
        Me.GroupPanelBuscador.Style.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.GroupPanelBuscador.Style.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.GroupPanelBuscador.Style.BackColorGradientAngle = 90
        Me.GroupPanelBuscador.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanelBuscador.Style.BorderBottomWidth = 1
        Me.GroupPanelBuscador.Style.BorderColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.GroupPanelBuscador.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanelBuscador.Style.BorderLeftWidth = 1
        Me.GroupPanelBuscador.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanelBuscador.Style.BorderRightWidth = 1
        Me.GroupPanelBuscador.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanelBuscador.Style.BorderTopWidth = 1
        Me.GroupPanelBuscador.Style.CornerDiameter = 4
        Me.GroupPanelBuscador.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanelBuscador.Style.Font = New System.Drawing.Font("Georgia", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupPanelBuscador.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanelBuscador.Style.TextColor = System.Drawing.Color.White
        Me.GroupPanelBuscador.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanelBuscador.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanelBuscador.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanelBuscador.TabIndex = 260
        Me.GroupPanelBuscador.Text = "B U S C A D O R"
        Me.GroupPanelBuscador.Visible = False
        '
        'grBuscador
        '
        Me.grBuscador.BackColor = System.Drawing.Color.WhiteSmoke
        Me.grBuscador.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grBuscador.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grBuscador.HeaderFormatStyle.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grBuscador.HeaderFormatStyle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.grBuscador.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight
        Me.grBuscador.Location = New System.Drawing.Point(0, 0)
        Me.grBuscador.Name = "grBuscador"
        Me.grBuscador.SelectedFormatStyle.BackColor = System.Drawing.Color.DodgerBlue
        Me.grBuscador.SelectedFormatStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grBuscador.SelectedFormatStyle.ForeColor = System.Drawing.Color.White
        Me.grBuscador.Size = New System.Drawing.Size(194, 62)
        Me.grBuscador.TabIndex = 0
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.Panel5)
        Me.Panel6.Controls.Add(Me.Panel4)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(377, 696)
        Me.Panel6.TabIndex = 1
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Transparent
        Me.Panel4.BackgroundImage = Global.DinoM.My.Resources.Resources.fondo1
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.Panel3)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(377, 696)
        Me.Panel4.TabIndex = 263
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.btn_Generar)
        Me.Panel3.Controls.Add(Me.btn_Salir)
        Me.Panel3.Controls.Add(Me.btnExportar)
        Me.Panel3.Location = New System.Drawing.Point(34, 3)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(303, 100)
        Me.Panel3.TabIndex = 263
        '
        'btn_Generar
        '
        Me.btn_Generar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btn_Generar.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange
        Me.btn_Generar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Generar.Image = Global.DinoM.My.Resources.Resources.pie_chart1
        Me.btn_Generar.ImageFixedSize = New System.Drawing.Size(55, 50)
        Me.btn_Generar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top
        Me.btn_Generar.Location = New System.Drawing.Point(23, 12)
        Me.btn_Generar.Name = "btn_Generar"
        Me.btn_Generar.Size = New System.Drawing.Size(80, 85)
        Me.btn_Generar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btn_Generar.TabIndex = 265
        Me.btn_Generar.Text = "GENERAR"
        Me.btn_Generar.TextColor = System.Drawing.Color.White
        '
        'btn_Salir
        '
        Me.btn_Salir.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btn_Salir.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange
        Me.btn_Salir.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Salir.Image = Global.DinoM.My.Resources.Resources.atras1
        Me.btn_Salir.ImageFixedSize = New System.Drawing.Size(55, 50)
        Me.btn_Salir.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top
        Me.btn_Salir.Location = New System.Drawing.Point(201, 13)
        Me.btn_Salir.Name = "btn_Salir"
        Me.btn_Salir.Size = New System.Drawing.Size(80, 85)
        Me.btn_Salir.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btn_Salir.TabIndex = 264
        Me.btn_Salir.Text = "SALIR"
        Me.btn_Salir.TextColor = System.Drawing.Color.White
        '
        'btnExportar
        '
        Me.btnExportar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnExportar.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange
        Me.btnExportar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExportar.Image = Global.DinoM.My.Resources.Resources.sheets
        Me.btnExportar.ImageFixedSize = New System.Drawing.Size(44, 48)
        Me.btnExportar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top
        Me.btnExportar.Location = New System.Drawing.Point(110, 14)
        Me.btnExportar.Name = "btnExportar"
        Me.btnExportar.Size = New System.Drawing.Size(80, 85)
        Me.btnExportar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnExportar.TabIndex = 263
        Me.btnExportar.Text = "EXPORTAR"
        Me.btnExportar.TextColor = System.Drawing.Color.White
        '
        'Pr_StockMinimo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1344, 696)
        Me.Controls.Add(Me.MReportViewer)
        Me.Controls.Add(Me.Panel6)
        Me.Name = "Pr_StockMinimo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pr_StockMinimo"
        CType(Me.cbAlmacen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbGrupos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.GroupPanelBuscador.ResumeLayout(False)
        CType(Me.grBuscador, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel6.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
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
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel6 As Panel
    Protected WithEvents GroupPanelBuscador As DevComponents.DotNetBar.Controls.GroupPanel
    Protected WithEvents grBuscador As Janus.Windows.GridEX.GridEX
    Friend WithEvents Panel3 As Panel
    Protected WithEvents btnExportar As DevComponents.DotNetBar.ButtonX
    Protected WithEvents btn_Salir As DevComponents.DotNetBar.ButtonX
    Protected WithEvents btn_Generar As DevComponents.DotNetBar.ButtonX
    Friend WithEvents swMostrar As DevComponents.DotNetBar.Controls.SwitchButton
End Class
