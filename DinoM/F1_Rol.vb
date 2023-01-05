Imports Logica.AccesoLogica
Imports Janus.Windows.GridEX
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls

Public Class F1_Rol
    Dim _Inter As Integer = 0

#Region "Atributos"
    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
#End Region
#Region "METODOS PRIVADOS"

    Private Sub _prIniciarTodo()
        Me.Text = "R O L E S "
        _prCargarGridModulos()

        _PMIniciarTodo()

        _prAsignarPermisos()
        grModulos.Focus()
        Dim blah As New Bitmap(New Bitmap(My.Resources.ic_r), 20, 20)
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico
        tbRol.MaxLength = 30
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

    Private Sub _prCargarGridModulos()
        Dim dt As New DataTable
        dt = L_prLibreriaDetalleGeneral(gi_LibSistema, gi_LibSISModulo)

        grModulos.DataSource = dt
        grModulos.RetrieveStructure()

        'dar formato a las columnas
        With grModulos.RootTable.Columns("cnnum")
            .Width = 50
            .Visible = False
        End With

        With grModulos.RootTable.Columns("cndesc1")
            .Caption = "MODULO"
            .Width = 240
        End With

        With grModulos.RootTable.Columns("cndesc2")
            .Visible = False
        End With

        With grModulos
            .GroupByBoxVisible = False
            'diseño de la grilla
            .AllowAddNew = InheritableBoolean.False
            .RowFormatStyle.BackColor = Color.AliceBlue
            .SelectionMode = SelectionMode.SingleSelection
            '.SelectedFormatStyle.BackColor = Color.LightGreen
            '.RowFormatStyle.BackColor = Color.MediumTurquoise
            .BoundMode = Janus.Data.BoundMode.Bound
            .RowHeaders = InheritableBoolean.True
        End With



    End Sub

    Private Sub _prCargarGridDetalle(numi As String)
        Dim dt As New DataTable
        dt = L_prRolDetalleGeneral(numi)

        grDetalle.DataSource = dt
        grDetalle.RetrieveStructure()

        'dar formato a las columnas
        With grDetalle.RootTable.Columns("ycline")
            .Width = 50
            .Visible = False
            .EditType = EditType.NoEdit
        End With
        With grDetalle.RootTable.Columns("yamod")
            .Width = 50
            .Visible = False
            .EditType = EditType.NoEdit
        End With

        With grDetalle.RootTable.Columns("ycnumi")
            .Width = 50
            .Visible = False
            .EditType = EditType.NoEdit
        End With

        With grDetalle.RootTable.Columns("ycyanumi")
            .Caption = "COD"
            .Width = 70
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .EditType = EditType.NoEdit
        End With

        With grDetalle.RootTable.Columns("yaprog")
            .Caption = "NOMBRE"
            .Width = 200
            .EditType = EditType.NoEdit
        End With
        With grDetalle.RootTable.Columns("yatit")
            .Caption = "PROGRAMA"
            .Width = 250
            .EditType = EditType.NoEdit
        End With

        With grDetalle.RootTable.Columns("ycshow")
            .Caption = "SHOW"
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        End With
        With grDetalle.RootTable.Columns("ycadd")
            .Caption = "ADD"
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        End With
        With grDetalle.RootTable.Columns("ycmod")
            .Caption = "EDIT"
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        End With
        With grDetalle.RootTable.Columns("ycdel")
            .Caption = "DEL"
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        End With

        With grDetalle.RootTable.Columns("estado")
            .Visible = False
        End With

        With grDetalle
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
            .AllowAddNew = InheritableBoolean.False

        End With

        Dim fc As GridEXFormatCondition
        fc = New GridEXFormatCondition(grDetalle.RootTable.Columns("estado"), ConditionOperator.Equal, 0)
        fc.FormatStyle.BackColor = Color.LightGreen
        grDetalle.RootTable.FormatConditions.Add(fc)

        'filtro por la primera fila del modulo
        If dt.Rows.Count > 0 Then
            Dim numiModulo As String = grModulos.GetValue("cnnum")
            Dim desc As String = grModulos.GetValue("cndesc1")
            grDetalle.RemoveFilters()
            grDetalle.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grDetalle.RootTable.Columns("yamod"), Janus.Windows.GridEX.ConditionOperator.Equal, numiModulo))
            GroupPanel2.Text = "privilegios del modulo ".ToUpper + desc
        End If

        'sobrecargo el metodo selection change
        AddHandler grModulos.SelectionChanged, AddressOf grModulos_SelectionChanged

    End Sub

    Private Sub _prSeleccionarTodos(columna As String)

        Dim dt As DataTable = CType(grDetalle.DataSource, DataTable)
        Dim numiModulo As String = grModulos.GetValue("cnnum")
        Dim filasFiltradas As DataRow() = dt.Select("yamod=" + numiModulo)
        For Each fila As DataRow In filasFiltradas
            fila.Item(columna) = 1
            If fila.Item("estado") = 1 Then
                fila.Item("estado") = 2
            End If
        Next
    End Sub


#End Region

#Region "METODOS SOBRECARGADOS"
    Public Overrides Sub _PMOHabilitar()

        tbRol.ReadOnly = False

        grModulos.ContextMenuStrip = msModulos
    End Sub

    Public Overrides Sub _PMOInhabilitar()
        tbNumi.ReadOnly = True
        tbRol.ReadOnly = True

        grModulos.ContextMenuStrip = Nothing
    End Sub

    Public Overrides Sub _PMOLimpiar()
        tbNumi.Text = ""
        tbRol.Text = ""

        'VACIO EL DETALLE
        _prCargarGridDetalle(-1)

    End Sub

    Public Overrides Sub _PMOLimpiarErrores()
        MEP.Clear()
        tbRol.BackColor = Color.White

    End Sub

    Public Overrides Function _PMOGrabarRegistro() As Boolean

        Dim dtDetalle As DataTable = CType(grDetalle.DataSource, DataTable).DefaultView.ToTable(True, "ycline", "ycnumi", "ycyanumi", "ycshow", "ycadd", "ycmod", "ycdel", "estado")
        Dim res As Boolean = L_prRolGrabar(tbNumi.Text, tbRol.Text, dtDetalle)
        If res Then
            ToastNotification.Show(Me, "Codigo de Rol ".ToUpper + tbNumi.Text + " Grabado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
        End If
        Return res

    End Function

    Public Overrides Function _PMOModificarRegistro() As Boolean

        Dim dtDetalle As DataTable = CType(grDetalle.DataSource, DataTable).DefaultView.ToTable(True, "ycline", "ycnumi", "ycyanumi", "ycshow", "ycadd", "ycmod", "ycdel", "estado")
        Dim res As Boolean = L_prRolModificar(tbNumi.Text, tbRol.Text, dtDetalle)
        If res Then

            ToastNotification.Show(Me, "Codigo de Personal ".ToUpper + tbNumi.Text + " modificado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
            _PSalirRegistro()
        End If
        Return res
    End Function

    Public Overrides Sub _PMOEliminarRegistro()
        Dim info As New TaskDialogInfo("eliminacion".ToUpper, eTaskDialogIcon.Delete, "¿esta seguro de eliminar el registro?".ToUpper, "".ToUpper, eTaskDialogButton.Yes Or eTaskDialogButton.Cancel, eTaskDialogBackgroundColor.Blue)
        Dim result As eTaskDialogResult = TaskDialog.Show(info)
        If result = eTaskDialogResult.Yes Then
            Dim mensajeError As String = ""
            Dim res As Boolean = L_prRolBorrar(tbNumi.Text, mensajeError)
            If res Then
                ToastNotification.Show(Me, "Codigo de Rol ".ToUpper + tbNumi.Text + " eliminado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
                _PMFiltrar()
            Else
                ToastNotification.Show(Me, mensajeError, My.Resources.WARNING, 8000, eToastGlowColor.Red, eToastPosition.TopCenter)
            End If
        End If
    End Sub
    Public Overrides Function _PMOValidarCampos() As Boolean
        Dim _ok As Boolean = True
        MEP.Clear()

        If tbRol.Text = String.Empty Then
            tbRol.BackColor = Color.Red
            MEP.SetError(tbRol, "ingrese la descripcion del rol!".ToUpper)
            _ok = False
        Else
            tbRol.BackColor = Color.White
            MEP.SetError(tbRol, "")
        End If

        MHighlighterFocus.UpdateHighlights()
        Return _ok
    End Function

    Public Overrides Function _PMOGetTablaBuscador() As DataTable

        Dim dtBuscador As DataTable = L_prRolGeneral()
        Return dtBuscador
    End Function

    Public Overrides Function _PMOGetListEstructuraBuscador() As List(Of Modelo.Celda)
        Dim listEstCeldas As New List(Of Modelo.Celda)
        listEstCeldas.Add(New Modelo.Celda("ybnumi", True, "ID", 70))
        listEstCeldas.Add(New Modelo.Celda("ybrol", True, "ROL", 120))
        listEstCeldas.Add(New Modelo.Celda("ybfact", False))
        listEstCeldas.Add(New Modelo.Celda("ybhact", False))
        listEstCeldas.Add(New Modelo.Celda("ybuact", False))
        Return listEstCeldas
    End Function

    Public Overrides Sub _PMOMostrarRegistro(_N As Integer)
        JGrM_Buscador.Row = _MPos

        With JGrM_Buscador
            tbNumi.Text = .GetValue("ybnumi").ToString
            tbRol.Text = .GetValue("ybrol").ToString


            lbFecha.Text = CType(.GetValue("ybfact"), Date).ToString("dd/MM/yyyy")
            lbHora.Text = .GetValue("ybhact").ToString
            lbUsuario.Text = .GetValue("ybuact").ToString

            'CARGAR DETALLE
            _prCargarGridDetalle(tbNumi.Text)
        End With

        LblPaginacion.Text = Str(_MPos + 1) + "/" + JGrM_Buscador.RowCount.ToString

    End Sub
    Public Overrides Sub _PMOHabilitarFocus()


    End Sub

    Private Sub _PSalirRegistro()
        If btnGrabar.Enabled = True Then
            _PMInhabilitar()
            _PMPrimerRegistro()

        Else
            '  Public _modulo As SideNavItem
            _modulo.Select()
            Me.Close()
        End If
    End Sub
#End Region

    Private Sub grModulos_SelectionChanged(sender As Object, e As EventArgs)
        Dim numiModulo As String = grModulos.GetValue("cnnum")
        Dim desc As String = grModulos.GetValue("cndesc1")
        grDetalle.RemoveFilters()
        grDetalle.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grDetalle.RootTable.Columns("yamod"), Janus.Windows.GridEX.ConditionOperator.Equal, numiModulo))
        GroupPanel2.Text = "privilegios del modulo ".ToUpper + desc
    End Sub

    Private Sub F1_Rol_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()
    End Sub

    Private Sub grDetalle_CellEdited(sender As Object, e As ColumnActionEventArgs) Handles grDetalle.CellEdited
        Dim estado As Integer = grDetalle.GetValue("estado")
        If estado = 1 Then
            grDetalle.SetValue("estado", 2)

        End If

    End Sub

    Private Sub grDetalle_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grDetalle.EditingCell
        If btnGrabar.Enabled Then
            If e.Column.Key <> "ycshow" And e.Column.Key <> "ycadd" And e.Column.Key <> "ycmod" And e.Column.Key <> "ycdel" Then
                e.Cancel = True
            End If
        Else
            e.Cancel = True
        End If


    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _PSalirRegistro()
    End Sub



    Private Sub SELECCIONARTODOSSHOWToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SELECCIONARTODOSSHOWToolStripMenuItem.Click
        _prSeleccionarTodos("ycshow")
    End Sub

    Private Sub SELECCIONARTODOSADDToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SELECCIONARTODOSADDToolStripMenuItem.Click
        _prSeleccionarTodos("ycadd")
    End Sub

    Private Sub SELECCIONARTODOSEDITToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SELECCIONARTODOSEDITToolStripMenuItem.Click
        _prSeleccionarTodos("ycmod")
    End Sub

    Private Sub SELECCIONARTODOSDELToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SELECCIONARTODOSDELToolStripMenuItem.Click
        _prSeleccionarTodos("ycdel")
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        _Inter = _Inter + 1
        If _Inter = 1 Then
            Me.WindowState = FormWindowState.Normal
        Else
            Me.Opacity = 100
            Timer1.Enabled = False
        End If
    End Sub


End Class