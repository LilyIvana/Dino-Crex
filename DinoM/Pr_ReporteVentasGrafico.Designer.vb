<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Pr_ReporteVentasGrafico
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
        Me.PanelData = New System.Windows.Forms.Panel()
        Me.SideNav1 = New DevComponents.DotNetBar.Controls.SideNav()
        Me.SideNavPanel2 = New DevComponents.DotNetBar.Controls.SideNavPanel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.MReportViewerAlmacen = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.GroupPanel2 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.ButtonX1 = New DevComponents.DotNetBar.ButtonX()
        Me.FechaFAlmacen = New System.Windows.Forms.MonthCalendar()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.FechaIAlmacen = New System.Windows.Forms.MonthCalendar()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.SideNavPanel3 = New DevComponents.DotNetBar.Controls.SideNavPanel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.MReportViewerRendimiento = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.GroupPanel4 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.grvendedor = New Janus.Windows.GridEX.GridEX()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.ChechTodos = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.ButtonX3 = New DevComponents.DotNetBar.ButtonX()
        Me.GroupPanel3 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.FechaFRendimiento = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.FechaIRendimiento = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX6 = New DevComponents.DotNetBar.LabelX()
        Me.SideNavPanel1 = New DevComponents.DotNetBar.Controls.SideNavPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.MReportViewerVendedor = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.GroupPanel1 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.btnGenerarVendedor = New DevComponents.DotNetBar.ButtonX()
        Me.FechaFVendedor = New System.Windows.Forms.MonthCalendar()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.FechaIVendedor = New System.Windows.Forms.MonthCalendar()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.SideNavItem1 = New DevComponents.DotNetBar.Controls.SideNavItem()
        Me.Separator1 = New DevComponents.DotNetBar.Separator()
        Me.SideNavItem2 = New DevComponents.DotNetBar.Controls.SideNavItem()
        Me.almacen = New DevComponents.DotNetBar.Controls.SideNavItem()
        Me.SideNavItem4 = New DevComponents.DotNetBar.Controls.SideNavItem()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.PanelData.SuspendLayout()
        Me.SideNav1.SuspendLayout()
        Me.SideNavPanel2.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.GroupPanel2.SuspendLayout()
        Me.SideNavPanel3.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.GroupPanel4.SuspendLayout()
        CType(Me.grvendedor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel10.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.GroupPanel3.SuspendLayout()
        CType(Me.FechaFRendimiento, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FechaIRendimiento, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SideNavPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.GroupPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelData
        '
        Me.PanelData.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.PanelData.Controls.Add(Me.SideNav1)
        Me.PanelData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelData.Location = New System.Drawing.Point(0, 0)
        Me.PanelData.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.PanelData.Name = "PanelData"
        Me.PanelData.Size = New System.Drawing.Size(1362, 741)
        Me.PanelData.TabIndex = 2
        '
        'SideNav1
        '
        Me.SideNav1.Controls.Add(Me.SideNavPanel3)
        Me.SideNav1.Controls.Add(Me.SideNavPanel2)
        Me.SideNav1.Controls.Add(Me.SideNavPanel1)
        Me.SideNav1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SideNav1.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.SideNavItem1, Me.Separator1, Me.SideNavItem2, Me.almacen, Me.SideNavItem4})
        Me.SideNav1.Location = New System.Drawing.Point(0, 0)
        Me.SideNav1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.SideNav1.Name = "SideNav1"
        Me.SideNav1.Padding = New System.Windows.Forms.Padding(1)
        Me.SideNav1.Size = New System.Drawing.Size(1362, 741)
        Me.SideNav1.TabIndex = 0
        Me.SideNav1.Text = "SideNav1"
        '
        'SideNavPanel2
        '
        Me.SideNavPanel2.Controls.Add(Me.Panel4)
        Me.SideNavPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SideNavPanel2.Location = New System.Drawing.Point(191, 36)
        Me.SideNavPanel2.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.SideNavPanel2.Name = "SideNavPanel2"
        Me.SideNavPanel2.Size = New System.Drawing.Size(1167, 704)
        Me.SideNavPanel2.TabIndex = 6
        Me.SideNavPanel2.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Transparent
        Me.Panel4.Controls.Add(Me.Panel5)
        Me.Panel4.Controls.Add(Me.Panel6)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1167, 704)
        Me.Panel4.TabIndex = 1
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.MReportViewerAlmacen)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(199, 0)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(968, 704)
        Me.Panel5.TabIndex = 1
        '
        'MReportViewerAlmacen
        '
        Me.MReportViewerAlmacen.ActiveViewIndex = -1
        Me.MReportViewerAlmacen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.MReportViewerAlmacen.Cursor = System.Windows.Forms.Cursors.Default
        Me.MReportViewerAlmacen.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MReportViewerAlmacen.Location = New System.Drawing.Point(0, 0)
        Me.MReportViewerAlmacen.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.MReportViewerAlmacen.Name = "MReportViewerAlmacen"
        Me.MReportViewerAlmacen.Size = New System.Drawing.Size(968, 704)
        Me.MReportViewerAlmacen.TabIndex = 0
        Me.MReportViewerAlmacen.ToolPanelWidth = 150
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.GroupPanel2)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(199, 704)
        Me.Panel6.TabIndex = 0
        '
        'GroupPanel2
        '
        Me.GroupPanel2.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel2.Controls.Add(Me.ButtonX1)
        Me.GroupPanel2.Controls.Add(Me.FechaFAlmacen)
        Me.GroupPanel2.Controls.Add(Me.LabelX2)
        Me.GroupPanel2.Controls.Add(Me.FechaIAlmacen)
        Me.GroupPanel2.Controls.Add(Me.LabelX3)
        Me.GroupPanel2.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupPanel2.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupPanel2.Location = New System.Drawing.Point(0, 0)
        Me.GroupPanel2.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupPanel2.Name = "GroupPanel2"
        Me.GroupPanel2.Size = New System.Drawing.Size(199, 704)
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
        Me.GroupPanel2.TabIndex = 0
        Me.GroupPanel2.Text = "ELEGIR FECHAS"
        '
        'ButtonX1
        '
        Me.ButtonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange
        Me.ButtonX1.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonX1.Image = Global.DinoM.My.Resources.Resources.reload_5
        Me.ButtonX1.ImageFixedSize = New System.Drawing.Size(48, 48)
        Me.ButtonX1.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top
        Me.ButtonX1.Location = New System.Drawing.Point(49, 431)
        Me.ButtonX1.Name = "ButtonX1"
        Me.ButtonX1.Size = New System.Drawing.Size(80, 72)
        Me.ButtonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.ButtonX1.TabIndex = 238
        Me.ButtonX1.Text = "GENERAR"
        Me.ButtonX1.TextColor = System.Drawing.Color.Black
        '
        'FechaFAlmacen
        '
        Me.FechaFAlmacen.Location = New System.Drawing.Point(7, 253)
        Me.FechaFAlmacen.Margin = New System.Windows.Forms.Padding(7, 7, 7, 7)
        Me.FechaFAlmacen.Name = "FechaFAlmacen"
        Me.FechaFAlmacen.TabIndex = 237
        Me.FechaFAlmacen.TitleBackColor = System.Drawing.Color.Maroon
        Me.FechaFAlmacen.TitleForeColor = System.Drawing.Color.Maroon
        Me.FechaFAlmacen.TrailingForeColor = System.Drawing.Color.Maroon
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
        Me.LabelX2.Location = New System.Drawing.Point(3, 219)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX2.Size = New System.Drawing.Size(74, 23)
        Me.LabelX2.TabIndex = 236
        Me.LabelX2.Text = "AL:"
        '
        'FechaIAlmacen
        '
        Me.FechaIAlmacen.Location = New System.Drawing.Point(7, 37)
        Me.FechaIAlmacen.Margin = New System.Windows.Forms.Padding(7, 7, 7, 7)
        Me.FechaIAlmacen.Name = "FechaIAlmacen"
        Me.FechaIAlmacen.TabIndex = 235
        Me.FechaIAlmacen.TitleBackColor = System.Drawing.Color.Maroon
        Me.FechaIAlmacen.TitleForeColor = System.Drawing.Color.Maroon
        Me.FechaIAlmacen.TrailingForeColor = System.Drawing.Color.Maroon
        '
        'LabelX3
        '
        Me.LabelX3.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX3.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX3.Location = New System.Drawing.Point(3, 3)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX3.Size = New System.Drawing.Size(74, 23)
        Me.LabelX3.TabIndex = 234
        Me.LabelX3.Text = "Fecha Del:"
        '
        'SideNavPanel3
        '
        Me.SideNavPanel3.Controls.Add(Me.Panel7)
        Me.SideNavPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SideNavPanel3.Location = New System.Drawing.Point(191, 36)
        Me.SideNavPanel3.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.SideNavPanel3.Name = "SideNavPanel3"
        Me.SideNavPanel3.Size = New System.Drawing.Size(1167, 704)
        Me.SideNavPanel3.TabIndex = 10
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Transparent
        Me.Panel7.Controls.Add(Me.Panel8)
        Me.Panel7.Controls.Add(Me.Panel9)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel7.Location = New System.Drawing.Point(0, 0)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(1167, 704)
        Me.Panel7.TabIndex = 1
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.MReportViewerRendimiento)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel8.Location = New System.Drawing.Point(239, 0)
        Me.Panel8.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(928, 704)
        Me.Panel8.TabIndex = 1
        '
        'MReportViewerRendimiento
        '
        Me.MReportViewerRendimiento.ActiveViewIndex = -1
        Me.MReportViewerRendimiento.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.MReportViewerRendimiento.Cursor = System.Windows.Forms.Cursors.Default
        Me.MReportViewerRendimiento.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MReportViewerRendimiento.Location = New System.Drawing.Point(0, 0)
        Me.MReportViewerRendimiento.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.MReportViewerRendimiento.Name = "MReportViewerRendimiento"
        Me.MReportViewerRendimiento.Size = New System.Drawing.Size(928, 704)
        Me.MReportViewerRendimiento.TabIndex = 0
        Me.MReportViewerRendimiento.ToolPanelWidth = 150
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.GroupPanel4)
        Me.Panel9.Controls.Add(Me.GroupPanel3)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel9.Location = New System.Drawing.Point(0, 0)
        Me.Panel9.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(239, 704)
        Me.Panel9.TabIndex = 0
        '
        'GroupPanel4
        '
        Me.GroupPanel4.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel4.Controls.Add(Me.grvendedor)
        Me.GroupPanel4.Controls.Add(Me.Panel10)
        Me.GroupPanel4.Controls.Add(Me.Panel11)
        Me.GroupPanel4.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupPanel4.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupPanel4.Location = New System.Drawing.Point(0, 152)
        Me.GroupPanel4.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupPanel4.Name = "GroupPanel4"
        Me.GroupPanel4.Size = New System.Drawing.Size(239, 552)
        '
        '
        '
        Me.GroupPanel4.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.GroupPanel4.Style.BackColorGradientAngle = 90
        Me.GroupPanel4.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.GroupPanel4.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel4.Style.BorderBottomWidth = 1
        Me.GroupPanel4.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.GroupPanel4.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel4.Style.BorderLeftWidth = 1
        Me.GroupPanel4.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel4.Style.BorderRightWidth = 1
        Me.GroupPanel4.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel4.Style.BorderTopWidth = 1
        Me.GroupPanel4.Style.CornerDiameter = 4
        Me.GroupPanel4.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanel4.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanel4.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.GroupPanel4.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanel4.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanel4.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanel4.TabIndex = 1
        Me.GroupPanel4.Text = "ELEGIR VENDEDOR"
        '
        'grvendedor
        '
        Me.grvendedor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grvendedor.Font = New System.Drawing.Font("Open Sans", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grvendedor.HeaderFormatStyle.Font = New System.Drawing.Font("Open Sans", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grvendedor.Location = New System.Drawing.Point(0, 39)
        Me.grvendedor.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.grvendedor.Name = "grvendedor"
        Me.grvendedor.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.grvendedor.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.grvendedor.Size = New System.Drawing.Size(233, 410)
        Me.grvendedor.TabIndex = 3
        Me.grvendedor.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Panel10
        '
        Me.Panel10.Controls.Add(Me.ChechTodos)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel10.Location = New System.Drawing.Point(0, 0)
        Me.Panel10.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(233, 39)
        Me.Panel10.TabIndex = 2
        '
        'ChechTodos
        '
        Me.ChechTodos.AutoSize = True
        '
        '
        '
        Me.ChechTodos.BackgroundStyle.BackColor = System.Drawing.Color.Transparent
        Me.ChechTodos.BackgroundStyle.BackColorBlend.AddRange(New DevComponents.DotNetBar.BackgroundColorBlend() {New DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.Blue, 0!)})
        Me.ChechTodos.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.ChechTodos.CheckSignSize = New System.Drawing.Size(20, 20)
        Me.ChechTodos.Location = New System.Drawing.Point(19, 12)
        Me.ChechTodos.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.ChechTodos.Name = "ChechTodos"
        Me.ChechTodos.Size = New System.Drawing.Size(142, 22)
        Me.ChechTodos.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeMobile2014
        Me.ChechTodos.TabIndex = 1
        Me.ChechTodos.Text = "Seleccionar Todos"
        '
        'Panel11
        '
        Me.Panel11.Controls.Add(Me.ButtonX3)
        Me.Panel11.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel11.Location = New System.Drawing.Point(0, 449)
        Me.Panel11.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(233, 81)
        Me.Panel11.TabIndex = 4
        '
        'ButtonX3
        '
        Me.ButtonX3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonX3.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange
        Me.ButtonX3.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonX3.Image = Global.DinoM.My.Resources.Resources.reload_5
        Me.ButtonX3.ImageFixedSize = New System.Drawing.Size(48, 48)
        Me.ButtonX3.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top
        Me.ButtonX3.Location = New System.Drawing.Point(77, 5)
        Me.ButtonX3.Name = "ButtonX3"
        Me.ButtonX3.Size = New System.Drawing.Size(80, 72)
        Me.ButtonX3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.ButtonX3.TabIndex = 239
        Me.ButtonX3.Text = "GENERAR"
        Me.ButtonX3.TextColor = System.Drawing.Color.Black
        '
        'GroupPanel3
        '
        Me.GroupPanel3.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel3.Controls.Add(Me.FechaFRendimiento)
        Me.GroupPanel3.Controls.Add(Me.FechaIRendimiento)
        Me.GroupPanel3.Controls.Add(Me.LabelX5)
        Me.GroupPanel3.Controls.Add(Me.LabelX6)
        Me.GroupPanel3.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupPanel3.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupPanel3.Location = New System.Drawing.Point(0, 0)
        Me.GroupPanel3.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupPanel3.Name = "GroupPanel3"
        Me.GroupPanel3.Size = New System.Drawing.Size(239, 152)
        '
        '
        '
        Me.GroupPanel3.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.GroupPanel3.Style.BackColorGradientAngle = 90
        Me.GroupPanel3.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.GroupPanel3.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel3.Style.BorderBottomWidth = 1
        Me.GroupPanel3.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.GroupPanel3.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel3.Style.BorderLeftWidth = 1
        Me.GroupPanel3.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel3.Style.BorderRightWidth = 1
        Me.GroupPanel3.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel3.Style.BorderTopWidth = 1
        Me.GroupPanel3.Style.CornerDiameter = 4
        Me.GroupPanel3.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanel3.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanel3.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.GroupPanel3.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanel3.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanel3.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanel3.TabIndex = 0
        Me.GroupPanel3.Text = "ELEGIR FECHAS"
        '
        'FechaFRendimiento
        '
        '
        '
        '
        Me.FechaFRendimiento.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.FechaFRendimiento.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.FechaFRendimiento.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown
        Me.FechaFRendimiento.ButtonDropDown.Visible = True
        Me.FechaFRendimiento.Font = New System.Drawing.Font("Open Sans Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FechaFRendimiento.IsPopupCalendarOpen = False
        Me.FechaFRendimiento.Location = New System.Drawing.Point(26, 85)
        '
        '
        '
        '
        '
        '
        Me.FechaFRendimiento.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.FechaFRendimiento.MonthCalendar.CalendarDimensions = New System.Drawing.Size(1, 1)
        Me.FechaFRendimiento.MonthCalendar.ClearButtonVisible = True
        '
        '
        '
        Me.FechaFRendimiento.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.FechaFRendimiento.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90
        Me.FechaFRendimiento.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.FechaFRendimiento.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.FechaFRendimiento.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.FechaFRendimiento.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1
        Me.FechaFRendimiento.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.FechaFRendimiento.MonthCalendar.DisplayMonth = New Date(2017, 2, 1, 0, 0, 0, 0)
        Me.FechaFRendimiento.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday
        '
        '
        '
        Me.FechaFRendimiento.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.FechaFRendimiento.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.FechaFRendimiento.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.FechaFRendimiento.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.FechaFRendimiento.MonthCalendar.TodayButtonVisible = True
        Me.FechaFRendimiento.Name = "FechaFRendimiento"
        Me.FechaFRendimiento.Size = New System.Drawing.Size(120, 25)
        Me.FechaFRendimiento.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.FechaFRendimiento.TabIndex = 239
        '
        'FechaIRendimiento
        '
        '
        '
        '
        Me.FechaIRendimiento.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.FechaIRendimiento.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.FechaIRendimiento.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown
        Me.FechaIRendimiento.ButtonDropDown.Visible = True
        Me.FechaIRendimiento.Font = New System.Drawing.Font("Open Sans Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FechaIRendimiento.IsPopupCalendarOpen = False
        Me.FechaIRendimiento.Location = New System.Drawing.Point(26, 25)
        '
        '
        '
        '
        '
        '
        Me.FechaIRendimiento.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.FechaIRendimiento.MonthCalendar.CalendarDimensions = New System.Drawing.Size(1, 1)
        Me.FechaIRendimiento.MonthCalendar.ClearButtonVisible = True
        '
        '
        '
        Me.FechaIRendimiento.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.FechaIRendimiento.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90
        Me.FechaIRendimiento.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.FechaIRendimiento.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.FechaIRendimiento.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.FechaIRendimiento.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1
        Me.FechaIRendimiento.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.FechaIRendimiento.MonthCalendar.DisplayMonth = New Date(2017, 2, 1, 0, 0, 0, 0)
        Me.FechaIRendimiento.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday
        '
        '
        '
        Me.FechaIRendimiento.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.FechaIRendimiento.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.FechaIRendimiento.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.FechaIRendimiento.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.FechaIRendimiento.MonthCalendar.TodayButtonVisible = True
        Me.FechaIRendimiento.Name = "FechaIRendimiento"
        Me.FechaIRendimiento.Size = New System.Drawing.Size(120, 25)
        Me.FechaIRendimiento.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.FechaIRendimiento.TabIndex = 238
        '
        'LabelX5
        '
        Me.LabelX5.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX5.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX5.Location = New System.Drawing.Point(3, 56)
        Me.LabelX5.Name = "LabelX5"
        Me.LabelX5.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX5.Size = New System.Drawing.Size(74, 23)
        Me.LabelX5.TabIndex = 236
        Me.LabelX5.Text = "AL:"
        '
        'LabelX6
        '
        Me.LabelX6.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX6.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX6.Location = New System.Drawing.Point(3, 3)
        Me.LabelX6.Name = "LabelX6"
        Me.LabelX6.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX6.Size = New System.Drawing.Size(74, 23)
        Me.LabelX6.TabIndex = 234
        Me.LabelX6.Text = "Fecha Del:"
        '
        'SideNavPanel1
        '
        Me.SideNavPanel1.Controls.Add(Me.Panel1)
        Me.SideNavPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SideNavPanel1.Location = New System.Drawing.Point(167, 32)
        Me.SideNavPanel1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.SideNavPanel1.Name = "SideNavPanel1"
        Me.SideNavPanel1.Size = New System.Drawing.Size(608, 704)
        Me.SideNavPanel1.TabIndex = 2
        Me.SideNavPanel1.Visible = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(608, 704)
        Me.Panel1.TabIndex = 0
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.MReportViewerVendedor)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(199, 0)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(409, 704)
        Me.Panel3.TabIndex = 1
        '
        'MReportViewerVendedor
        '
        Me.MReportViewerVendedor.ActiveViewIndex = -1
        Me.MReportViewerVendedor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.MReportViewerVendedor.Cursor = System.Windows.Forms.Cursors.Default
        Me.MReportViewerVendedor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MReportViewerVendedor.Location = New System.Drawing.Point(0, 0)
        Me.MReportViewerVendedor.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.MReportViewerVendedor.Name = "MReportViewerVendedor"
        Me.MReportViewerVendedor.Size = New System.Drawing.Size(409, 704)
        Me.MReportViewerVendedor.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.GroupPanel1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(199, 704)
        Me.Panel2.TabIndex = 0
        '
        'GroupPanel1
        '
        Me.GroupPanel1.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel1.Controls.Add(Me.btnGenerarVendedor)
        Me.GroupPanel1.Controls.Add(Me.FechaFVendedor)
        Me.GroupPanel1.Controls.Add(Me.LabelX1)
        Me.GroupPanel1.Controls.Add(Me.FechaIVendedor)
        Me.GroupPanel1.Controls.Add(Me.LabelX4)
        Me.GroupPanel1.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupPanel1.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupPanel1.Location = New System.Drawing.Point(0, 0)
        Me.GroupPanel1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupPanel1.Name = "GroupPanel1"
        Me.GroupPanel1.Size = New System.Drawing.Size(199, 704)
        '
        '
        '
        Me.GroupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.GroupPanel1.Style.BackColorGradientAngle = 90
        Me.GroupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
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
        Me.GroupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
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
        Me.GroupPanel1.Text = "ELEGIR FECHAS"
        '
        'btnGenerarVendedor
        '
        Me.btnGenerarVendedor.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnGenerarVendedor.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange
        Me.btnGenerarVendedor.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGenerarVendedor.Image = Global.DinoM.My.Resources.Resources.reload_5
        Me.btnGenerarVendedor.ImageFixedSize = New System.Drawing.Size(48, 48)
        Me.btnGenerarVendedor.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top
        Me.btnGenerarVendedor.Location = New System.Drawing.Point(49, 435)
        Me.btnGenerarVendedor.Name = "btnGenerarVendedor"
        Me.btnGenerarVendedor.Size = New System.Drawing.Size(80, 72)
        Me.btnGenerarVendedor.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnGenerarVendedor.TabIndex = 239
        Me.btnGenerarVendedor.Text = "GENERAR"
        Me.btnGenerarVendedor.TextColor = System.Drawing.Color.Black
        '
        'FechaFVendedor
        '
        Me.FechaFVendedor.Location = New System.Drawing.Point(7, 253)
        Me.FechaFVendedor.Margin = New System.Windows.Forms.Padding(7, 7, 7, 7)
        Me.FechaFVendedor.Name = "FechaFVendedor"
        Me.FechaFVendedor.TabIndex = 237
        Me.FechaFVendedor.TitleBackColor = System.Drawing.Color.Maroon
        Me.FechaFVendedor.TitleForeColor = System.Drawing.Color.Maroon
        Me.FechaFVendedor.TrailingForeColor = System.Drawing.Color.Maroon
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
        Me.LabelX1.Location = New System.Drawing.Point(3, 219)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX1.Size = New System.Drawing.Size(74, 23)
        Me.LabelX1.TabIndex = 236
        Me.LabelX1.Text = "AL:"
        '
        'FechaIVendedor
        '
        Me.FechaIVendedor.Location = New System.Drawing.Point(7, 37)
        Me.FechaIVendedor.Margin = New System.Windows.Forms.Padding(7, 7, 7, 7)
        Me.FechaIVendedor.Name = "FechaIVendedor"
        Me.FechaIVendedor.TabIndex = 235
        Me.FechaIVendedor.TitleBackColor = System.Drawing.Color.Maroon
        Me.FechaIVendedor.TitleForeColor = System.Drawing.Color.Maroon
        Me.FechaIVendedor.TrailingForeColor = System.Drawing.Color.Maroon
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
        Me.LabelX4.Location = New System.Drawing.Point(3, 3)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX4.Size = New System.Drawing.Size(74, 23)
        Me.LabelX4.TabIndex = 234
        Me.LabelX4.Text = "Fecha Del:"
        '
        'SideNavItem1
        '
        Me.SideNavItem1.IsSystemMenu = True
        Me.SideNavItem1.Name = "SideNavItem1"
        Me.SideNavItem1.Symbol = ""
        Me.SideNavItem1.Text = "Menu"
        '
        'Separator1
        '
        Me.Separator1.FixedSize = New System.Drawing.Size(3, 1)
        Me.Separator1.Name = "Separator1"
        Me.Separator1.Padding.Bottom = 2
        Me.Separator1.Padding.Left = 6
        Me.Separator1.Padding.Right = 6
        Me.Separator1.Padding.Top = 2
        Me.Separator1.SeparatorOrientation = DevComponents.DotNetBar.eDesignMarkerOrientation.Vertical
        '
        'SideNavItem2
        '
        Me.SideNavItem2.Name = "SideNavItem2"
        Me.SideNavItem2.Panel = Me.SideNavPanel1
        Me.SideNavItem2.Symbol = ""
        Me.SideNavItem2.Text = "VENTAS VENDEDOR"
        '
        'almacen
        '
        Me.almacen.Name = "almacen"
        Me.almacen.Panel = Me.SideNavPanel2
        Me.almacen.Symbol = "58723"
        Me.almacen.SymbolSet = DevComponents.DotNetBar.eSymbolSet.Material
        Me.almacen.Text = "VENTAS ALMACEN"
        '
        'SideNavItem4
        '
        Me.SideNavItem4.Checked = True
        Me.SideNavItem4.Name = "SideNavItem4"
        Me.SideNavItem4.Panel = Me.SideNavPanel3
        Me.SideNavItem4.Symbol = "59621"
        Me.SideNavItem4.SymbolSet = DevComponents.DotNetBar.eSymbolSet.Material
        Me.SideNavItem4.Text = "RENDIMIENTO VENDEDOR"
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 500
        '
        'Pr_ReporteVentasGrafico
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1362, 741)
        Me.Controls.Add(Me.PanelData)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "Pr_ReporteVentasGrafico"
        Me.Opacity = 0.05R
        Me.Text = "Pr_ReporteVentasGrafico"
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
        Me.PanelData.ResumeLayout(False)
        Me.SideNav1.ResumeLayout(False)
        Me.SideNav1.PerformLayout()
        Me.SideNavPanel2.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.GroupPanel2.ResumeLayout(False)
        Me.SideNavPanel3.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.GroupPanel4.ResumeLayout(False)
        CType(Me.grvendedor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel10.ResumeLayout(False)
        Me.Panel10.PerformLayout()
        Me.Panel11.ResumeLayout(False)
        Me.GroupPanel3.ResumeLayout(False)
        CType(Me.FechaFRendimiento, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FechaIRendimiento, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SideNavPanel1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.GroupPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelData As System.Windows.Forms.Panel
    Friend WithEvents SideNav1 As DevComponents.DotNetBar.Controls.SideNav
    Friend WithEvents SideNavPanel3 As DevComponents.DotNetBar.Controls.SideNavPanel
    Friend WithEvents SideNavPanel2 As DevComponents.DotNetBar.Controls.SideNavPanel
    Friend WithEvents SideNavPanel1 As DevComponents.DotNetBar.Controls.SideNavPanel
    Private WithEvents SideNavItem1 As DevComponents.DotNetBar.Controls.SideNavItem
    Friend WithEvents Separator1 As DevComponents.DotNetBar.Separator
    Private WithEvents SideNavItem2 As DevComponents.DotNetBar.Controls.SideNavItem
    Private WithEvents almacen As DevComponents.DotNetBar.Controls.SideNavItem
    Private WithEvents SideNavItem4 As DevComponents.DotNetBar.Controls.SideNavItem
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents MReportViewerVendedor As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents GroupPanel1 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents FechaIVendedor As System.Windows.Forms.MonthCalendar
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents FechaFVendedor As System.Windows.Forms.MonthCalendar
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents MReportViewerRendimiento As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents GroupPanel3 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents LabelX5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX6 As DevComponents.DotNetBar.LabelX
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents MReportViewerAlmacen As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents GroupPanel2 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents FechaFAlmacen As System.Windows.Forms.MonthCalendar
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents FechaIAlmacen As System.Windows.Forms.MonthCalendar
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents FechaFRendimiento As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents FechaIRendimiento As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents GroupPanel4 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents ChechTodos As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents grvendedor As Janus.Windows.GridEX.GridEX
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Protected WithEvents ButtonX1 As DevComponents.DotNetBar.ButtonX
    Protected WithEvents btnGenerarVendedor As DevComponents.DotNetBar.ButtonX
    Protected WithEvents ButtonX3 As DevComponents.DotNetBar.ButtonX
    Friend WithEvents Timer1 As Timer
End Class
