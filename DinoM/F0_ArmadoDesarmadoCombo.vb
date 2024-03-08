Imports Logica.AccesoLogica
Imports Janus.Windows.GridEX
Imports DevComponents.DotNetBar
Imports System.IO
Imports DevComponents.DotNetBar.SuperGrid
Imports System.Drawing
Imports DevComponents.DotNetBar.Controls
Imports System.Threading
Imports System.Drawing.Text
Imports System.Reflection
Imports System.Runtime.InteropServices
Public Class F0_ArmadoDesarmadoCombo
#Region "Variables Globales"
    Dim _Inter As Integer = 0

    Private stEst As String = "1"
    Private stAlm As String = "1"
    Private stIddc As String = "0"

    Private boShow As Boolean = False
    Private boAdd As Boolean = False
    Private boModif As Boolean = False
    Private boDel As Boolean = False

    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
    Dim Table_producto As DataTable
    Dim FilaSelectLote As DataRow = Nothing
    Dim RutaGlobal As String = gs_CarpetaRaiz

    Dim FtTitulo As Font = New Font("Arial", gi_fuenteTamano + 1)
    Dim FtNormal As Font = New Font("Arial", gi_fuenteTamano)

    Dim DtBusqueda As DataTable
    Dim DtDetalle As DataTable
    Dim DtDetalle1 As DataTable
    Dim InDuracion As Byte = 5

    Dim BoNuevo As Boolean = False
    Dim BoModificar As Boolean = False
    Dim BoEliminar As Boolean = False
    Dim BoNavegar As Boolean = False
#End Region
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        _Inter = _Inter + 1
        If _Inter = 1 Then
            Me.WindowState = FormWindowState.Normal

        Else
            Me.Opacity = 100
            Timer1.Enabled = False
        End If
    End Sub

#Region "Funciones y Métodos"
    Private Sub P_prInicio()

        'Inicializar componentes
        P_prInicializarComponentes()

        'Habilitar/Deshabilitar compotentes del formulario
        P_prHDComponentes(False)

        'Armar todo las grillas
        BoNavegar = False
        P_prArmarGrillas()
        BoNavegar = True

        P_prActualizarPaginacion(0)
        P_prLlenarDatos(0)
    End Sub
    Private Sub P_prNuevoRegistro()
        P_prLimpiar()
        P_prEstadoNueModEli(1)
        P_prHDComponentes(BoNuevo)
        tbCodPack.ReadOnly = True
        tbProdPack.ReadOnly = True
        'dtiFechaDoc.Select()
        tbCodPack.Select()
        P_prAddFilaDetalle()
        'Ocultar el GroupPanel Desarmado
        GroupPanelDesarmado.Visible = False
        GroupPanelDatosDesarmado.Visible = False
    End Sub
    Private Sub P_prInicializarComponentes()
        'Form
        Me.Text = "ARMADO Y DESARMADO DE COMBOS"

        'Tab
        MSuperTabControl.SelectedTabIndex = 0

        'Visible
        btnImprimir.Visible = False


        'Enabled
        btnGrabar.Enabled = False

        'ReadOnly
        tbCodigo.ReadOnly = True

        'Grid Busqueda
        dgjBusqueda.AllowEdit = Janus.Data.InheritableBoolean.False

        _prAsignarPermisos()

    End Sub
    Private Sub _prAsignarPermisos()

        Dim dtRolUsu As DataTable = L_prRolDetalleGeneral(gi_userRol, _nameButton)

        Dim show As Boolean = dtRolUsu.Rows(0).Item("ycshow")
        Dim add As Boolean = dtRolUsu.Rows(0).Item("ycadd")
        Dim modif As Boolean = dtRolUsu.Rows(0).Item("ycmod")
        Dim del As Boolean = dtRolUsu.Rows(0).Item("ycdel")

        If add = False Then
            btnNuevo.Visible = False
        End If
        If modif = False Then
            btnModificar.Visible = False
        End If
        If del = False Then
            btnEliminar.Visible = False
        End If
    End Sub
    Private Sub P_prArmarGrillas()
        P_prArmarGrillaBusqueda()
        P_prArmarGrillaDetalle("-1")
        P_prArmarGrillaDetalleDesarmado("-1")
    End Sub
    Private Sub P_prActualizarPaginacion(ByVal index As Integer)
        LblPaginacion.Text = "Reg. " & index + 1 & " de " & dgjBusqueda.GetRows.Count
    End Sub
    Private Sub P_prLlenarDatos(ByVal index As Integer)
        If (index <= dgjBusqueda.GetRows.Count - 1 And index >= 0) Then
            If (BoNavegar) Then
                With dgjBusqueda
                    Me.tbCodigo.Text = .GetValue("id").ToString
                    Me.dtiFechaDoc.Value = .GetValue("fdoc")
                    Me.tbObs.Text = .GetValue("obs").ToString
                    Me.tbCodPack.Text = .GetValue("codpack").ToString
                    Me.tbProdPack.Text = .GetValue("yfcdprod1").ToString
                    Me.tbCantP.Value = .GetValue("cantP")
                    Me.tbPcosto.Text = .GetValue("pcosto").ToString
                    Me.tbCantNP.Value = .GetValue("cantNP")


                    stEst = .GetValue("est").ToString
                    stAlm = .GetValue("alm").ToString

                    labelStockCombo.Visible = False
                    lbStockCombo.Visible = False

                    P_prArmarGrillaDetalle(tbCodigo.Text)

                    If tbCantNP.Value = 0 Then
                        tbCantNP.Value = 0
                        GroupPanelDesarmado.Visible = False
                        GroupPanelDatosDesarmado.Visible = False
                        dtiFechaDesarm.Text = ""

                    Else
                        GroupPanelDatosDesarmado.Visible = True
                        GroupPanelDesarmado.Visible = True
                        tbCantNP.Value = .GetValue("cantNP")
                        dtiFechaDesarm.Value = .GetValue("fechaNP")
                        P_prArmarGrillaDetalleDesarmado(tbCodigo.Text)

                    End If

                    lbFecha.Text = CType(.GetValue("fact").ToString, Date).ToString("dd/MM/yyyy")
                    lbHora.Text = .GetValue("hact").ToString
                    lbUsuario.Text = .GetValue("uact").ToString
                End With

                P_prActualizarPaginacion(dgjBusqueda.Row)

                If (Not boModif And boAdd) Then
                    If (Now.Date = Me.dtiFechaDoc.Value) Then
                        btnModificar.Visible = True
                    Else
                        btnModificar.Visible = False
                    End If
                End If
            End If
        Else
            If (dgjBusqueda.GetRows.Count > 0) Then
                P_prMoverIndexActual()
            End If
        End If
    End Sub
    Private Sub P_prMoverIndexActual()
        Dim index As Integer = CInt(LblPaginacion.Text.Trim.Split(" ")(1).Trim)
        If (index < 0) Then
            index = 0
        End If
        If (index > dgjBusqueda.RowCount) Then
            index = dgjBusqueda.RowCount
        End If
        Try
            dgjBusqueda.MoveTo(index - 1)
            P_prLlenarDatos(dgjBusqueda.Row)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub P_prLimpiar()
        'TextBox
        tbCodigo.Clear()
        tbObs.Clear()
        tbCodPack.Clear()
        tbProdPack.Clear()
        tbCantP.Value = 0
        tbPcosto.Clear()
        'tbPcosto.Enabled = False
        'tbCodPack.Enabled = False
        'tbProdPack.Enabled = False
        tbCantNP.Value = 0

        'DateTimer
        dtiFechaDoc.Value = Now.Date
        dtiFechaDesarm.Text = ""

        'Grillas
        P_prArmarGrillaDetalle("-1")
        P_prArmarGrillaDetalleDesarmado("-1")



    End Sub
    Private Sub P_prArmarGrillaBusqueda()
        DtBusqueda = New DataTable
        DtBusqueda = L_fnMovimientoComboGeneral()

        dgjBusqueda.BoundMode = Janus.Data.BoundMode.Bound
        dgjBusqueda.DataSource = DtBusqueda
        dgjBusqueda.RetrieveStructure()

        'dar formato a las columnas
        With dgjBusqueda.RootTable.Columns("id")
            .Caption = "Código"
            .Width = 80
            .HeaderStyle.Font = FtTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.Font = FtNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
        End With

        With dgjBusqueda.RootTable.Columns("fdoc")
            .Caption = "Fecha"
            .Width = 100
            .HeaderStyle.Font = FtTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.Font = FtNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
        End With
        With dgjBusqueda.RootTable.Columns("obs")
            .Caption = "Observación"
            .Width = 300
            .HeaderStyle.Font = FtTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.Font = FtNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Position = 2
            '.CellStyle.BackColor = Color.AliceBlue
        End With

        With dgjBusqueda.RootTable.Columns("codpack")
            .Caption = "Cod. Combo"
            .Width = 80
            .HeaderStyle.Font = FtTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.Font = FtNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
        End With
        With dgjBusqueda.RootTable.Columns("yfcdprod1")
            .Caption = "Combo"
            .Width = 450
            .HeaderStyle.Font = FtTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.Font = FtNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
        End With
        With dgjBusqueda.RootTable.Columns("cantP")
            .Caption = "Cantidad Armada"
            .Width = 120
            .HeaderStyle.Font = FtTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.Font = FtNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
        End With

        With dgjBusqueda.RootTable.Columns("pcosto")
            .Visible = False
        End With

        With dgjBusqueda.RootTable.Columns("cantNP")
            .Caption = "Cantidad Desarmada"
            .Width = 120
            .HeaderStyle.Font = FtTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.Font = FtNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
        End With
        With dgjBusqueda.RootTable.Columns("fechaNP")
            .Visible = False
        End With
        With dgjBusqueda.RootTable.Columns("est")
            .Visible = False
        End With

        With dgjBusqueda.RootTable.Columns("alm")
            .Visible = False
        End With

        With dgjBusqueda.RootTable.Columns("fact")
            .Visible = False
        End With

        With dgjBusqueda.RootTable.Columns("hact")
            .Visible = False
        End With

        With dgjBusqueda.RootTable.Columns("uact")
            .Visible = False
        End With
        'Habilitar Filtradores
        With dgjBusqueda
            .GroupByBoxVisible = False
            '.FilterRowFormatStyle.BackColor = Color.Blue
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            'Diseño de la tabla
            .VisualStyle = VisualStyle.Office2007
            .SelectionMode = SelectionMode.MultipleSelection
            .AlternatingColors = True
            .RecordNavigator = True
        End With
    End Sub
    Private Sub P_prArmarGrillaDetalle(numi As String)
        DtDetalle = New DataTable
        DtDetalle = L_fnMovimientoComboDetalle(numi)

        dgjDetalle.BoundMode = Janus.Data.BoundMode.Bound
        dgjDetalle.DataSource = DtDetalle
        dgjDetalle.RetrieveStructure()

        'dar formato a las columnas
        With dgjDetalle.RootTable.Columns("ilid")
            .Visible = False
        End With
        With dgjDetalle.RootTable.Columns("iligid")
            .Visible = False
        End With
        With dgjDetalle.RootTable.Columns("ilcodpro")
            .Caption = "Cód Dynasys."
            .Width = 90
            .HeaderStyle.Font = FtTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.Font = FtNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
        End With

        With dgjDetalle.RootTable.Columns("yfcdprod1")
            .Caption = "Descripción"
            .Width = 285
            .HeaderStyle.Font = FtTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.Font = FtNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .WordWrap = True
            .MaxLines = 2
            '.CellStyle.BackColor = Color.AliceBlue
        End With

        With dgjDetalle.RootTable.Columns("ilcant")
            .Caption = "Cantidad"
            .Width = 70
            .HeaderStyle.Font = FtTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.Font = FtNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
        End With

        With dgjDetalle.RootTable.Columns("iltotcant")
            .Caption = "Total Cant."
            .Width = 70
            .HeaderStyle.Font = FtTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.Font = FtNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
        End With
        With dgjDetalle.RootTable.Columns("ilpcosto")
            .Caption = "P. Costo Un."
            .Width = 80
            .HeaderStyle.Font = FtTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.Font = FtNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
        End With

        With dgjDetalle.RootTable.Columns("stock")
            .Caption = "Stock"
            .Width = 50
            .HeaderStyle.Font = FtTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.Font = FtNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With

        With dgjDetalle.RootTable.Columns("estado")
            .Visible = False
            '.CellStyle.BackColor = Color.AliceBlue
        End With

        'Habilitar Filtradores
        With dgjDetalle
            .GroupByBoxVisible = False
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges

            'Diseño de la tabla
            .VisualStyle = VisualStyle.Office2007
            .SelectionMode = SelectionMode.MultipleSelection
            .AlternatingColors = True
            '.RecordNavigator = True
        End With
    End Sub
    Private Sub P_prArmarGrillaDetalleDesarmado(numi As String)
        DtDetalle1 = New DataTable
        DtDetalle1 = L_fnMovimientoComboDetalleTI0052(numi)

        dgjDesArmPack.BoundMode = Janus.Data.BoundMode.Bound
        dgjDesArmPack.DataSource = DtDetalle1
        dgjDesArmPack.RetrieveStructure()

        'dar formato a las columnas
        With dgjDesArmPack.RootTable.Columns("imid")
            .Visible = False
        End With
        With dgjDesArmPack.RootTable.Columns("imigid")
            .Visible = False
        End With
        With dgjDesArmPack.RootTable.Columns("imcodpro")
            .Caption = "Cód Prod."
            .Width = 90
            .HeaderStyle.Font = FtTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.Font = FtNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
        End With
        With dgjDesArmPack.RootTable.Columns("yfcdprod1")
            .Caption = "Descripción"
            .Width = 285
            .HeaderStyle.Font = FtTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.Font = FtNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .WordWrap = True
            .MaxLines = 2
            '.CellStyle.BackColor = Color.AliceBlue
        End With
        With dgjDesArmPack.RootTable.Columns("imcant")
            .Caption = "Cantidad"
            .Width = 80
            .HeaderStyle.Font = FtTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.Font = FtNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
        End With
        With dgjDesArmPack.RootTable.Columns("imtotcant")
            .Caption = "Total Cant."
            .Width = 80
            .HeaderStyle.Font = FtTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.Font = FtNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
        End With
        With dgjDesArmPack.RootTable.Columns("impcosto")
            .Caption = "P. Costo Un."
            .Width = 80
            .HeaderStyle.Font = FtTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.Font = FtNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
        End With
        With dgjDesArmPack.RootTable.Columns("stock")
            .Caption = "Stock"
            .Width = 50
            .HeaderStyle.Font = FtTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.Font = FtNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With
        With dgjDesArmPack.RootTable.Columns("estado")
            .Visible = False
            '.CellStyle.BackColor = Color.AliceBlue
        End With

        'Habilitar Filtradores
        With dgjDesArmPack
            .GroupByBoxVisible = False
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges

            'Diseño de la tabla
            .VisualStyle = VisualStyle.Office2007
            .SelectionMode = SelectionMode.MultipleSelection
            .AlternatingColors = True
            '.RecordNavigator = True
        End With
    End Sub
    Private Sub P_prEstadoNueModEli(value As Integer)
        BoNuevo = (value = 1)
        BoModificar = (value = 2)
        BoEliminar = (value = 3)

        btnNuevo.Enabled = (value = 4)
        btnModificar.Enabled = (value = 4)
        btnEliminar.Enabled = (value = 4)
        btnGrabar.Enabled = Not (value = 4)

        If (value = 4) Then
            btnSalir.Tooltip = "SALIR"
            btnSalir.Text = "SALIR"
        Else
            btnSalir.Tooltip = "CANCELAR"
            btnSalir.Text = "CANCELAR"
        End If

        btnPrimero.Enabled = (value = 4)
        btnAnterior.Enabled = (value = 4)
        btnSiguiente.Enabled = (value = 4)
        btnUltimo.Enabled = (value = 4)
        MSuperTabItemBusqueda.Visible = (value = 4)


        btnGrabar.Tooltip = IIf(value = 1, "GRABAR NUEVO REGISTRO", IIf(value = 2, "GRABAR MODIFICACIÓN DE REGISTRO", "GRABAR"))
        MRlAccion.Text = IIf(value = 1, "NUEVO", IIf(value = 2, "DESARMAR", ""))

        'Compentes

        If (MSuperTabControl.SelectedTabIndex = 1) Then
            MSuperTabControl.SelectedTabIndex = 0
        End If

    End Sub
    Private Sub P_prHDComponentes(ByVal flat As Boolean)
        'TextBox
        tbObs.ReadOnly = Not flat
        tbCodPack.ReadOnly = Not flat
        'tbCodPack.Enabled = flat
        tbProdPack.ReadOnly = Not flat
        tbCantP.IsInputReadOnly = Not flat
        tbPcosto.ReadOnly = Not flat
        tbCantNP.IsInputReadOnly = Not flat

        'DateTimer
        dtiFechaDoc.IsInputReadOnly = Not flat
        dtiFechaDoc.ButtonDropDown.Enabled = flat
        dtiFechaDesarm.IsInputReadOnly = Not flat
        dtiFechaDesarm.ButtonDropDown.Enabled = flat

        'Grillas
        dgjDetalle.AllowEdit = IIf(flat, 1, 2)
    End Sub
    Private Sub P_prAddFilaDetalle()
        Dim fil As DataRow
        fil = DtDetalle.NewRow
        fil.Item("ilid") = _fnSiguienteNumi() + 1
        fil.Item("iligid") = 0
        fil.Item("ilcodpro") = 0
        fil.Item("yfcdprod1") = "Nuevo"
        fil.Item("ilcant") = 0
        fil.Item("iltotcant") = 0
        fil.Item("ilpcosto") = 0
        fil.Item("stock") = 0
        fil.Item("estado") = 0
        DtDetalle.Rows.Add(fil)
    End Sub
    Public Function _fnSiguienteNumi()
        Dim dt As DataTable = CType(dgjDetalle.DataSource, DataTable)
        Dim mayor As Integer = 0
        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
            Dim data As Integer = IIf(IsDBNull(CType(dgjDetalle.DataSource, DataTable).Rows(i).Item("ilid")), 0, CType(dgjDetalle.DataSource, DataTable).Rows(i).Item("ilid"))
            If (data > mayor) Then
                mayor = data
            End If
        Next
        Return mayor
    End Function
    Public Function _fnSiguienteNumiTI0052()
        Dim dt As DataTable = CType(dgjDesArmPack.DataSource, DataTable)
        Dim mayor As Integer = 0
        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
            Dim data As Integer = IIf(IsDBNull(CType(dgjDesArmPack.DataSource, DataTable).Rows(i).Item("imid")), 0, CType(dgjDesArmPack.DataSource, DataTable).Rows(i).Item("imid"))
            If (data > mayor) Then
                mayor = data
            End If
        Next
        Return mayor
    End Function
    Sub _prCargarProductoCombo(numi As Integer)
        Dim dt As DataTable = L_fnDetalleCombo(numi)
        Dim pcostotot As Decimal = 0
        With dgjDetalle.RootTable.Columns("stock")
            .Caption = "Stock"
            .Width = 50
            .HeaderStyle.Font = FtTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.Font = FtNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
        End With
        If (dt.Rows.Count > 0) Then
            CType(dgjDetalle.DataSource, DataTable).Rows.Clear()
            For i As Integer = 0 To dt.Rows.Count - 1

                P_prAddFilaDetalle()
                dgjDetalle.Row = dgjDetalle.RowCount - 1

                dgjDetalle.SetValue("ilcodpro", dt.Rows(i).Item("yfcyfnumi1"))
                dgjDetalle.SetValue("yfcdprod1", dt.Rows(i).Item("yfcdprod1"))
                dgjDetalle.SetValue("ilcant", dt.Rows(i).Item("yfccant"))
                dgjDetalle.SetValue("ilpcosto", dt.Rows(i).Item("yhprecio"))
                dgjDetalle.SetValue("stock", dt.Rows(i).Item("iccven"))
                pcostotot = pcostotot + (dgjDetalle.GetValue("ilcant") * dgjDetalle.GetValue("ilpcosto"))
            Next
            CType(dgjDetalle.DataSource, DataTable).AcceptChanges()
            'Dim pcostotot As Decimal = (dgjDetalle.GetValue("ihcant") * dgjDetalle.GetValue("ihpcosto"))
            tbPcosto.Text = pcostotot
            'tbPcosto.Text = dgjDetalle.GetTotal(dgjDetalle.RootTable.Columns("ihpcosto"), AggregateFunction.Sum)

            'dgjDetalle.Select()
            tbCantP.Select()

        End If
    End Sub
    Private Sub P_prCancelarRegistro()
        If (Not btnGrabar.Enabled) Then
            Me.Close()
            _modulo.Select()
            '_tab.Close()
        Else
            P_prLimpiar()
            P_prHDComponentes(False)
            P_prLlenarDatos(dgjBusqueda.Row)
        End If
        P_prEstadoNueModEli(4)
    End Sub

    Private Sub P_prGrabarRegistro()
        Dim id As String
        Dim fdoc As String
        Dim obs As String
        Dim codpack As String
        Dim cantP As Integer
        Dim pcosto As String
        Dim cantNP As Integer
        Dim est As String
        Dim alm As String

        dgjDetalle.Refetch()

        If (BoNuevo) Then
            If (P_fnValidarGrabacion()) Then

                id = ""
                fdoc = dtiFechaDoc.Value.ToString("yyyy/MM/dd")
                obs = tbObs.Text.Trim
                codpack = tbCodPack.Text
                cantP = tbCantP.Value
                pcosto = tbPcosto.Text
                cantNP = tbCantNP.Value
                est = "1"
                alm = stAlm

                Dim dt As New DataTable
                dt = CType(dgjDetalle.DataSource, DataTable).DefaultView.ToTable(False, "ilid", "iligid", "ilcodpro", "yfcdprod1", "ilcant", "iltotcant", "ilpcosto", "stock", "estado")

                'Grabar
                Dim res As Boolean = L_fnMovimientoComboGrabar(id, fdoc, obs, codpack, cantP, pcosto, cantNP, est, alm, dt)

                If (res) Then
                    P_prLimpiar()
                    BoNavegar = False
                    P_prArmarGrillaBusqueda()
                    tbCodPack.Select()
                    BoNavegar = True

                    ToastNotification.Show(Me, "Código de Armado Combo ".ToUpper + tbCodigo.Text + " Grabado con éxito.".ToUpper,
                                       My.Resources.GRABACION_EXITOSA,
                                       InDuracion * 1000,
                                       eToastGlowColor.Green,
                                       eToastPosition.TopCenter)
                Else
                    ToastNotification.Show(Me, "No se pudo grabar el código de armado combo".ToUpper + tbCodigo.Text + ", intente nuevamente.".ToUpper,
                                       My.Resources.WARNING,
                                       InDuracion * 1000,
                                       eToastGlowColor.Red,
                                       eToastPosition.TopCenter)
                End If
            End If
        ElseIf (BoModificar) Then
            If (P_fnValidarGrabacionDesarmado()) Then
                Dim dt As New DataTable
                dt = CType(dgjDesArmPack.DataSource, DataTable).DefaultView.ToTable(False, "imid", "imigid", "imcodpro", "yfcdprod1", "imcant", "imtotcant", "impcosto", "stock", "estado")

                'Grabar
                Dim res As Boolean = L_fnGrabarDesarmado(tbCodigo.Text, tbCodPack.Text, tbCantNP.Value, dtiFechaDesarm.Value, dt)

                If (res) Then
                    P_prLimpiar()
                    BoNavegar = False
                    P_prArmarGrillaBusqueda()
                    tbCodPack.Select()
                    BoNavegar = True

                    ToastNotification.Show(Me, "Desarmado de Combo ".ToUpper + tbCodigo.Text + " Grabado con éxito.".ToUpper,
                                   My.Resources.GRABACION_EXITOSA,
                                   InDuracion * 1000,
                                   eToastGlowColor.Green,
                                   eToastPosition.TopCenter)
                Else
                    ToastNotification.Show(Me, "No se pudo grabar el desarmado del combo".ToUpper + tbCodigo.Text + ", intente nuevamente.".ToUpper,
                                   My.Resources.WARNING,
                                   InDuracion * 1000,
                                   eToastGlowColor.Red,
                                   eToastPosition.TopCenter)
                End If
            End If
        End If
    End Sub

    Private Function P_fnValidarGrabacion() As Boolean
        Dim res As Boolean = True
        MEP.Clear()

        If (tbCodPack.Text = String.Empty) Then
            tbCodPack.BackColor = Color.Red
            MEP.SetError(tbCodPack, "ingrese Código del producto combo!".ToUpper)
            res = False
        Else
            tbCodPack.BackColor = Color.White
            MEP.SetError(tbCodPack, "")
        End If

        If (tbCantP.Value <= 0) Then
            tbCantP.BackColor = Color.Red
            MEP.SetError(tbCantP, "La cantidad debe ser mayor a cero!".ToUpper)
            res = False
        Else
            tbCantP.BackColor = Color.White
            MEP.SetError(tbCantP, "")
        End If
        Return res
    End Function
    Private Function P_fnValidarGrabacionDesarmado() As Boolean
        Dim res As Boolean = True
        'MEP.Clear()

        If (tbCantNP.Value <= 0) Then
            tbCantNP.BackColor = Color.Red
            MEP.SetError(tbCantNP, "La cantidad debe ser mayor a cero!".ToUpper)
            res = False
        Else
            tbCantNP.BackColor = Color.White
            MEP.SetError(tbCantNP, "")
        End If
        Return res
    End Function

    Private Sub pr_Inhabilitar(ByVal flat As Boolean)
        'TextBox
        tbObs.ReadOnly = Not flat
        tbCodPack.ReadOnly = Not flat
        tbProdPack.ReadOnly = Not flat
        tbCantP.IsInputReadOnly = Not flat
        tbPcosto.ReadOnly = Not flat

        'DateTimer
        dtiFechaDoc.IsInputReadOnly = Not flat
        dtiFechaDoc.ButtonDropDown.Enabled = flat

        'Grillas
        dgjDetalle.AllowEdit = IIf(flat, 1, 2)
    End Sub

    Private Sub P_prModificarRegistro()
        pr_Inhabilitar(False)
        P_prEstadoNueModEli(2)
        tbCantNP.IsInputReadOnly = False
        dtiFechaDesarm.IsInputReadOnly = False


        pr_DesarmarCombo()
    End Sub

    Private Sub pr_DesarmarCombo()
        Dim dt As DataTable
        If tbCantNP.Value > 0 Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Usted ya desarmó el Combo, no puede volver a desarmar.".ToUpper, img, 6000, eToastGlowColor.Red, eToastPosition.TopCenter)
        Else

            Dim dt1 As DataTable = L_fnVerificarStockCombo(tbCodPack.Text)
            lbStockCombo.Text = dt1.Rows(0).Item("iccven").ToString
            labelStockCombo.Visible = True
            lbStockCombo.Visible = True
            dtiFechaDesarm.Value = Now.Date

            GroupPanelDesarmado.Visible = True
            GroupPanelDatosDesarmado.Visible = True
            tbCantNP.IsInputReadOnly = False
            tbCantNP.Select()
            dtiFechaDesarm.Visible = True
            dtiFechaDesarm.Value = Now.Date
            P_prArmarGrillaDetalleDesarmado(-1)
            dt = dgjDetalle.DataSource

            If (dt.Rows.Count > 0) Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    P_prAddFilaDetalleDesarmado()
                    dgjDesArmPack.Row = dgjDesArmPack.RowCount - 1

                    dgjDesArmPack.SetValue("imigid", dt.Rows(i).Item("iligid"))
                    dgjDesArmPack.SetValue("imcodpro", dt.Rows(i).Item("ilcodpro"))
                    dgjDesArmPack.SetValue("yfcdprod1", dt.Rows(i).Item("yfcdprod1"))
                    dgjDesArmPack.SetValue("imcant", dt.Rows(i).Item("ilcant"))
                    dgjDesArmPack.SetValue("impcosto", dt.Rows(i).Item("ilpcosto"))
                    'dgjDesArmPack.SetValue("stock", dt.Rows(i).Item("stock"))
                Next
                CType(dgjDesArmPack.DataSource, DataTable).AcceptChanges()

            End If
        End If
    End Sub

    Private Sub P_prAddFilaDetalleDesarmado()
        Dim fil As DataRow
        fil = DtDetalle1.NewRow
        fil.Item("imid") = _fnSiguienteNumiTI0052() + 1
        fil.Item("imigid") = 0
        fil.Item("imcodpro") = 0
        fil.Item("yfcdprod1") = "Nuevo"
        fil.Item("imcant") = 0
        fil.Item("imtotcant") = 0
        fil.Item("impcosto") = 0
        fil.Item("stock") = 0
        fil.Item("estado") = 0
        DtDetalle1.Rows.Add(fil)
    End Sub


#End Region
#Region "Eventos"
    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        P_prNuevoRegistro()
    End Sub

    Private Sub F0_ArmadoDesarmadoCombo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        P_prInicio()
    End Sub

    Private Sub tbCodPack_KeyDown(sender As Object, e As KeyEventArgs) Handles tbCodPack.KeyDown
        If (e.KeyData = Keys.Control + Keys.Enter) Then
            Dim dt As DataTable
            dt = L_fnProductosCombo()

            Dim listEstCeldas As New List(Of Modelo.Celda)
            listEstCeldas.Add(New Modelo.Celda("yfnumi", True, "COD. DYN", 80))
            listEstCeldas.Add(New Modelo.Celda("yfcprod", True, "COD. DELTA", 90))
            listEstCeldas.Add(New Modelo.Celda("producto", True, "PRODUCTO", 520))

            Dim ef = New Efecto
            ef.tipo = 3
            ef.dt = dt
            ef.SeleclCol = 2
            ef.listEstCeldas = listEstCeldas
            ef.alto = 150
            ef.ancho = 250
            ef.Context = "Seleccione Productos".ToUpper
            ef.ShowDialog()
            Dim bandera As Boolean = False
            bandera = ef.band
            If (bandera = True) Then
                Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
                tbCodPack.ReadOnly = True
                tbProdPack.ReadOnly = True
                tbPcosto.ReadOnly = True
                tbCodPack.Text = Row.Cells("yfnumi").Value
                tbProdPack.Text = Row.Cells("producto").Value
                _prCargarProductoCombo(Row.Cells("yfnumi").Value)
            End If
        End If
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        P_prCancelarRegistro()
    End Sub

    Private Sub tbCantP_ValueChanged(sender As Object, e As EventArgs) Handles tbCantP.ValueChanged
        Dim dt As DataTable = CType(dgjDetalle.DataSource, DataTable)
        If (tbCantP.Focused) Then
            If (dt.Rows.Count > 0) Then
                For i As Integer = 0 To dt.Rows.Count - 1

                    dgjDetalle.Row = i
                    'dgjDetalle.SetValue("ihtotcant", dgjDetalle.GetValue("ihcant") * tbCantArm.Value)
                    dgjDetalle.SetValue("iltotcant", dt.Rows(i).Item("ilcant") * tbCantP.Value)
                    If (dgjDetalle.GetValue("iltotcant") > dt.Rows(i).Item("stock")) Then

                        Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                        MessageBox.Show(Me, "La cantidad para armar el combo no debe ser mayor al del Stock, Producto: " + (dgjDetalle.GetValue("yfcdprod1")) & vbCrLf &
                        "Stock=" + Str(dgjDetalle.GetValue("stock")).ToUpper, "AVISO!!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                        tbCantP.Value = 0

                    End If
                Next

            End If
            dgjDetalle.UpdateData()
        End If
    End Sub

    Private Sub dgjDetalle_EditingCell(sender As Object, e As EditingCellEventArgs) Handles dgjDetalle.EditingCell
        e.Cancel = True
    End Sub

    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        P_prGrabarRegistro()
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        P_prModificarRegistro()
    End Sub

    Private Sub tbCantNP_ValueChanged(sender As Object, e As EventArgs) Handles tbCantNP.ValueChanged
        Dim dt As DataTable = CType(dgjDesArmPack.DataSource, DataTable)
        Dim dt1 As DataTable = L_fnVerificarStockCombo(tbCodPack.Text)
        If (tbCantNP.Focused) Then

            If (dt1.Rows.Count > 0) Then
                If (tbCantNP.Value > dt1.Rows(0).Item("iccven")) Then
                    Dim stock1 As Double = dt1.Rows(0).Item("iccven")
                    ToastNotification.Show(Me, "La cantidad no puede ser mayor al stock actual del combo. Stock Combo:".ToUpper + stock1.ToString,
                                           My.Resources.WARNING,
                                           InDuracion * 1100,
                                           eToastGlowColor.Red,
                                           eToastPosition.TopCenter)
                    tbCantNP.Value = 0
                Else
                    If (dt.Rows.Count > 0) Then
                        For i As Integer = 0 To dt.Rows.Count - 1
                            dgjDesArmPack.Row = i
                            dgjDesArmPack.SetValue("imtotcant", dt.Rows(i).Item("imcant") * tbCantNP.Value)
                        Next

                    End If
                    dgjDesArmPack.UpdateData()

                End If
            End If
        End If
    End Sub

    Private Sub dgjDesArmPack_EditingCell(sender As Object, e As EditingCellEventArgs) Handles dgjDesArmPack.EditingCell
        e.Cancel = True
    End Sub

    Private Sub btnPrimero_Click(sender As Object, e As EventArgs) Handles btnPrimero.Click
        If (dgjBusqueda.RowCount > 0) Then
            dgjBusqueda.MoveFirst()
        End If
    End Sub

    Private Sub btnAnterior_Click(sender As Object, e As EventArgs) Handles btnAnterior.Click
        If (dgjBusqueda.RowCount > 0) Then
            dgjBusqueda.MovePrevious()
        End If
    End Sub

    Private Sub btnSiguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click
        If (dgjBusqueda.RowCount > 0) Then
            dgjBusqueda.MoveNext()
        End If
    End Sub

    Private Sub btnUltimo_Click(sender As Object, e As EventArgs) Handles btnUltimo.Click
        If (dgjBusqueda.RowCount > 0) Then
            dgjBusqueda.MoveLast()
        End If
    End Sub

    Private Sub dgjBusqueda_SelectionChanged(sender As Object, e As EventArgs) Handles dgjBusqueda.SelectionChanged
        If (dgjBusqueda.Row > -1 And (Not BoNuevo And Not BoModificar)) Then
            P_prLlenarDatos(dgjBusqueda.Row)
        End If
    End Sub

    Private Sub dgjBusqueda_DoubleClick(sender As Object, e As EventArgs) Handles dgjBusqueda.DoubleClick
        If (dgjBusqueda.Row > -1) Then
            MSuperTabControl.SelectedTabIndex = 0
        End If
    End Sub

    Private Sub dgjBusqueda_KeyDown(sender As Object, e As KeyEventArgs) Handles dgjBusqueda.KeyDown
        If (e.KeyData = Keys.Enter) Then
            MSuperTabControl.SelectedTabIndex = 0
            e.SuppressKeyPress = True
        End If
    End Sub


#End Region
End Class