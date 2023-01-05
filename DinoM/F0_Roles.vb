Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports Janus.Windows.GridEX
Imports DevComponents.DotNetBar.Controls

'DESARROLLADO POR: DANNY GUTIERREZ
Public Class F0_Roles

#Region "ATRIBUTOS"
    Private _Dsencabezado As DataSet
    Private moDataTableTipoDato As New DataTable
    Private mdtnColeccion As New Dictionary(Of Long, DataTable)
    Public _modulo As SideNavItem
    Private intModulo As Integer
    Private _Nuevo As Boolean
    Private _Pos As Integer
    Public _nameButton As String
    Public _tab As SuperTabItem
#End Region

#Region "METODOS PRIVADOS"
    Private Sub _PIniciarTodo()

        Me.Text = "ROLES"
        Me.WindowState = FormWindowState.Maximized

        _PCargarListModulo()

        _PFiltrar()
        _PInhabilitar()

        _PHabilitarFocus()

        _PCargarBuscador()
        _pCambiarFuente()

        _prAsignarPermisos()
        Dim blah As New Bitmap(New Bitmap(My.Resources.ROLES), 20, 20)


        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico

        GroupPanel1.Style.BackColor = Color.FromArgb(13, 71, 161)
        GroupPanel1.Style.BackColor2 = Color.FromArgb(13, 71, 161)
        GroupPanel1.Style.TextColor = Color.White
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

    Private Sub _pCambiarFuente()
        'Dim fuente As New Font("Tahoma", gi_fuenteTamano, FontStyle.Regular)
        'Dim xCtrl As Control
        'For Each xCtrl In PanelEx3.Controls
        '    Try
        '        xCtrl.Font = fuente
        '    Catch ex As Exception
        '    End Try
        'Next
        'For Each xCtrl In PanelEx4.Controls
        '    Try
        '        xCtrl.Font = fuente
        '    Catch ex As Exception
        '    End Try
        'Next

        'JGr_Detalle.Font = fuente

    End Sub

    Private Sub _PCargarListModulo()
        Dim dataTable As DataTable
        dataTable = L_prLibreriaDetalleGeneral(gi_LibSistema, gi_LibSISModulo)

        ListB_Empleados.ItemHeight = 30
        ListB_Empleados.SelectionMode = eSelectionMode.One
        ListB_Empleados.BackColor = Color.AliceBlue
        ListB_Empleados.DataSource = dataTable
        ListB_Empleados.DisplayMember = "cndesc1"
        ListB_Empleados.ValueMember = "cnnum"

        If dataTable.Rows.Count > 0 Then
            ListB_Empleados.SetSelected(0, False)
        End If
    End Sub

    Private Sub _PFiltrar()
        _Dsencabezado = New DataSet
        _Dsencabezado = L_Rol_General(0)
        _Pos = 0

        If _Dsencabezado.Tables(0).Rows.Count <> 0 Then
            _PMostrarRegistro(0)
            LblPaginacion.Text = Str(1) + "/" + _Dsencabezado.Tables(0).Rows.Count.ToString
            If _Dsencabezado.Tables(0).Rows.Count > 0 Then
                btnPrimero.Visible = True
                btnAnterior.Visible = True
                btnSiguiente.Visible = True
                btnUltimo.Visible = True
            End If
        End If
    End Sub

    Private Sub _PMostrarRegistro(_N As Integer)
        With _Dsencabezado.Tables(0).Rows(_N)
            Tb_Id.Text = .Item("ybnumi").ToString
            Tb_Nombre.Text = .Item("ybrol").ToString
        End With
    End Sub

    Private Sub _PInhabilitar()
        Tb_Id.ReadOnly = True
        Tb_Nombre.ReadOnly = True

        btnNuevo.Enabled = True
        btnModificar.Enabled = True
        btnEliminar.Enabled = True
        btnGrabar.Enabled = False

        Txt_Actualizar.Enabled = False
        ListB_Empleados.Enabled = False
        JGr_Detalle.Enabled = False

        btnGrabar.Image = My.Resources.save

        _PLimpiarErrores()
    End Sub

    Private Sub _PLimpiarErrores()
        MEP.Clear()
        Tb_Nombre.BackColor = Color.White
    End Sub

    Private Sub _PHabilitarFocus()
        MHighlighterFocus.SetHighlightOnFocus(Tb_Nombre, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)

        Tb_Nombre.TabIndex = 1
    End Sub

    Private Sub _PCargarBuscador()
        _Dsencabezado = New DataSet
        _Dsencabezado = L_Rol_General(0)

        JGr_Buscador.BoundMode = BoundMode.Bound
        JGr_Buscador.DataSource = _Dsencabezado.Tables(0) ' dt
        JGr_Buscador.RetrieveStructure()

        With JGr_Buscador.RootTable.Columns("ybnumi")
            .Caption = "Codigo".ToUpper
            .Width = 150
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.FontSize = gi_userFuente
        End With

        With JGr_Buscador.RootTable.Columns("ybrol")
            .Caption = "Rol".ToUpper
            .Width = 150
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.FontSize = gi_userFuente
        End With

        'Habilitar Filtradores
        With JGr_Buscador
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False

            'diseño de la grilla
            JGr_Buscador.VisualStyle = VisualStyle.Office2007
        End With
    End Sub

#Region " Eventos-ListBox "
    Private Sub ListB_Empleados_ItemClick(sender As Object, e As EventArgs) Handles ListB_Empleados.ItemClick
        If ListB_Empleados.SelectedIndex >= 0 Then 'selecciono el item
            If moDataTableTipoDato.Rows.Count > 0 Then
                mdtnColeccion.Remove(intModulo)
                mdtnColeccion.Add(intModulo, moDataTableTipoDato)
            End If

            intModulo = ListB_Empleados.SelectedValue
            Call DataSetTable()

            If mdtnColeccion.ContainsKey(ListB_Empleados.SelectedValue) Then
                moDataTableTipoDato = mdtnColeccion.Item(ListB_Empleados.SelectedValue)
            Else
                Dim dtInsu As DataTable
                If Tb_Id.Text <> String.Empty Then
                    dtInsu = L_RolDetalle_General(-1, Tb_Id.Text, ListB_Empleados.SelectedValue)
                    If dtInsu.Rows.Count = 0 Then
                        dtInsu = L_Formulario_General(1, " and ZY001.yamod=" & ListB_Empleados.SelectedValue).Tables(0)
                        For Each campo As DataRow In dtInsu.Rows
                            moDataTableTipoDato.Rows.Add(RowNewTable(campo, True))
                        Next

                    Else
                        For Each campo As DataRow In dtInsu.Rows
                            moDataTableTipoDato.Rows.Add(RowNewTable(campo, False))
                        Next
                    End If
                Else
                    dtInsu = L_Formulario_General(1, " and ZY001.yamod=" & ListB_Empleados.SelectedValue).Tables(0)
                    For Each campo As DataRow In dtInsu.Rows
                        moDataTableTipoDato.Rows.Add(RowNewTable(campo, True))
                    Next
                End If
            End If

            JGr_Detalle.DataSource = moDataTableTipoDato
            JGr_Detalle.RetrieveStructure()

            Call grdIntTable()
        End If

    End Sub
#End Region

#Region " DETALLE "
#Region " DataSet-Detalle "
    Private Sub DataSetTable()
        moDataTableTipoDato = New DataTable("Prueba")
        moDataTableTipoDato.Columns.Add("numi", Type.GetType("System.Int32"))
        moDataTableTipoDato.Columns.Add("numi1", Type.GetType("System.Int32"))
        moDataTableTipoDato.Columns.Add("Programa".ToUpper, Type.GetType("System.String"))
        moDataTableTipoDato.Columns.Add("Titulo".ToUpper, Type.GetType("System.String"))
        moDataTableTipoDato.Columns.Add("Show".ToUpper, Type.GetType("System.Boolean"))
        moDataTableTipoDato.Columns.Add("Add".ToUpper, Type.GetType("System.Boolean"))
        moDataTableTipoDato.Columns.Add("Mod".ToUpper, Type.GetType("System.Boolean"))
        moDataTableTipoDato.Columns.Add("Del".ToUpper, Type.GetType("System.Boolean"))
        moDataTableTipoDato.Columns.Add("Estado".ToUpper, Type.GetType("System.String"))
    End Sub

    Private Function RowNewTable(ByVal oDataRow As DataRow, ByVal boolSw As Boolean) As DataRow
        Dim oRow As DataRow
        oRow = moDataTableTipoDato.NewRow

        If boolSw Then
            oRow("numi") = 0
            oRow("numi1") = CInt(oDataRow("yanumi").ToString.Trim)
            oRow("Programa") = oDataRow("yaprog").ToString().Trim
            oRow("Titulo") = oDataRow("yatit").ToString.Trim
            oRow("Show") = False
            oRow("Add") = False
            oRow("Mod") = False
            oRow("Del") = False
            oRow("Estado") = "New"
        Else
            oRow("numi") = CInt(oDataRow("ycnumi").ToString().Trim)
            oRow("numi1") = CInt(oDataRow("ycyanumi").ToString.Trim)
            oRow("Programa") = oDataRow("yaprog").ToString().Trim
            oRow("Titulo") = oDataRow("yatit").ToString.Trim
            oRow("Show") = CBool(oDataRow("ycshow")) 'False ' oDataRow("IsKey")
            oRow("Add") = CBool(oDataRow("ycadd")) 'False ' oDataRow("IsKey")
            oRow("Mod") = CBool(oDataRow("ycmod")) 'False ' oDataRow("IsKey")
            oRow("Del") = CBool(oDataRow("ycdel")) 'False ' oDataRow("IsKey")
            oRow("Estado") = "Show"
        End If
        Return oRow
    End Function

    Private Sub EditarRow(ByRef oRow As DataRow)
        If CBool(JGr_Detalle.GetValue("Show")) <> CBool(oRow("Show")) Then
            If CStr(oRow("Estado")) <> "New" Then
                JGr_Detalle.SetValue("Estado", "Edit")
            End If
        End If

        If CBool(JGr_Detalle.GetValue("Add")) <> CBool(oRow("Add")) Then
            If CStr(oRow("Estado")) <> "New" Then
                JGr_Detalle.SetValue("Estado", "Edit")
            End If
        End If

        If CBool(JGr_Detalle.GetValue("Mod")) <> CBool(oRow("Mod")) Then
            If CStr(oRow("Estado")) <> "New" Then
                JGr_Detalle.SetValue("Estado", "Edit")
            End If
        End If

        If CBool(JGr_Detalle.GetValue("Del")) <> CBool(oRow("Del")) Then
            If CStr(oRow("Estado")) <> "New" Then
                JGr_Detalle.SetValue("Estado", "Edit")
            End If
        End If
    End Sub

    Private Sub DeleteRolDetalle()
        For Each oRow As DataRow In moDataTableTipoDato.Rows
            If oRow("Estado") <> "New" Then
                L_RolDetalle_Borrar(oRow("numi").ToString.Trim, oRow("numi1").ToString.Trim)
            End If
        Next
    End Sub
#End Region

#Region " Grid-Detalle "
    Private Sub grdIntTable()
        With JGr_Detalle
            .RootTable.Columns("numi").Visible = False
            .RootTable.Columns("numi1").Visible = False

            .RootTable.Columns("Programa").Caption = "Programa".ToUpper
            .RootTable.Columns("Programa").Width = 200
            .RootTable.Columns("Programa").CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .RootTable.Columns("Programa").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .RootTable.Columns("Programa").EditType = Janus.Windows.GridEX.EditType.NoEdit

            .RootTable.Columns("Titulo").Caption = "Titulo".ToUpper
            .RootTable.Columns("Titulo").Width = 200
            .RootTable.Columns("Titulo").CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .RootTable.Columns("Titulo").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .RootTable.Columns("Titulo").EditType = Janus.Windows.GridEX.EditType.NoEdit

            .RootTable.Columns("Show").Caption = "Show".ToUpper
            .RootTable.Columns("Show").Width = 80
            .RootTable.Columns("Show").CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .RootTable.Columns("Show").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            .RootTable.Columns("Add").Caption = "Add".ToUpper
            .RootTable.Columns("Add").Width = 80
            .RootTable.Columns("Add").CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .RootTable.Columns("Add").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            .RootTable.Columns("Mod").Caption = "Mod".ToUpper
            .RootTable.Columns("Mod").Width = 80
            .RootTable.Columns("Mod").CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .RootTable.Columns("Mod").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            .RootTable.Columns("Del").Caption = "Del".ToUpper
            .RootTable.Columns("Del").Width = 80
            .RootTable.Columns("Del").CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .RootTable.Columns("Del").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            .RootTable.Columns("Estado").Visible = False
        End With

        Dim fc As Janus.Windows.GridEX.GridEXFormatCondition

        fc = New Janus.Windows.GridEX.GridEXFormatCondition(JGr_Detalle.RootTable.Columns("Estado"), Janus.Windows.GridEX.ConditionOperator.Equal, "Edit")
        fc.FormatStyle.BackColor = Color.LightYellow 'Color.LightGreen ' Color.DarkRed
        JGr_Detalle.RootTable.FormatConditions.Add(fc)

        fc = New Janus.Windows.GridEX.GridEXFormatCondition(JGr_Detalle.RootTable.Columns("Estado"), Janus.Windows.GridEX.ConditionOperator.Equal, "New")
        fc.FormatStyle.BackColor = Color.LightGreen ' Color.DarkRed
        JGr_Detalle.RootTable.FormatConditions.Add(fc)

        fc = New Janus.Windows.GridEX.GridEXFormatCondition(JGr_Detalle.RootTable.Columns("Estado"), Janus.Windows.GridEX.ConditionOperator.Equal, "Show")
        fc.FormatStyle.BackColor = Color.White 'Color.LightGreen ' Color.DarkRed
        JGr_Detalle.RootTable.FormatConditions.Add(fc)
    End Sub

    Private Sub JGr_Detalle_CellUpdated(sender As Object, e As ColumnActionEventArgs) Handles JGr_Detalle.CellUpdated
        If moDataTableTipoDato.Rows.Count = JGr_Detalle.GetRows.Count Then
            EditarRow(moDataTableTipoDato.Rows(JGr_Detalle.GetRow.Position))
            Call grdIntTable()
        End If
    End Sub
#End Region
#End Region

#End Region


#Region " Evento-Button "
#Region " Metodo-Button "
    Private Sub _PHabilitar()
        Tb_Nombre.ReadOnly = False

        btnNuevo.Enabled = False
        btnModificar.Enabled = False
        btnEliminar.Enabled = False
        btnGrabar.Enabled = True

        Txt_Actualizar.Enabled = True
        ListB_Empleados.Enabled = True
        JGr_Detalle.Enabled = True
    End Sub

    Private Sub _PLimpiar()
        Tb_Id.Text = String.Empty
        Tb_Nombre.Text = String.Empty
        intModulo = 0

        LblPaginacion.Text = String.Empty
        moDataTableTipoDato.Rows.Clear()
        mdtnColeccion.Clear()
        JGr_Detalle.ClearStructure()

        Call _PCargarListModulo()
    End Sub

    Public Function P_Validar() As Boolean
        Dim _Error As Boolean = True
        MEP.Clear()
        If Tb_Nombre.Text.Trim = String.Empty Then
            Tb_Nombre.BackColor = Color.Red
            MEP.SetError(Tb_Nombre, "Ingrese rol!")
            _Error = False
        Else
            Tb_Nombre.BackColor = Color.White
            MEP.SetError(Tb_Nombre, String.Empty)
        End If

        MHighlighterFocus.UpdateHighlights()
        Return _Error
    End Function

    Private Sub Detalle_Grabar()
        Dim table As DataTable = ListB_Empleados.DataSource
        For Each oRow As DataRow In table.Rows
            If mdtnColeccion.ContainsKey(CInt(oRow("cnnum"))) Then
                Dim Detalle As DataTable = mdtnColeccion.Item(CInt(oRow("cnnum")))
                Dim numi1 As Integer
                Dim show, add, mod1, del As Boolean
                For Each oDataRow As DataRow In Detalle.Rows
                    If oDataRow("Estado") = "Edit" Then
                        numi1 = oDataRow("numi1")
                        show = oDataRow("Show")
                        add = oDataRow("Add")
                        mod1 = oDataRow("Mod")
                        del = oDataRow("Del")

                        L_RolDetalle_Modificar(Tb_Id.Text, numi1, show, add, mod1, del)
                    ElseIf oDataRow("Estado") = "New" Then
                        numi1 = oDataRow("numi1")
                        show = oDataRow("Show")
                        add = oDataRow("Add")
                        mod1 = oDataRow("Mod")
                        del = oDataRow("Del")

                        L_RolDetalle_Grabar(Tb_Id.Text, numi1, show, add, mod1, del)
                    End If
                Next
            End If
        Next
    End Sub
#End Region

#Region " Actualizar-Button "
    Private Sub Txt_Actualizar_Click(sender As Object, e As EventArgs) Handles Txt_Actualizar.Click
        If ListB_Empleados.SelectedIndex >= 0 Then 'selecciono el item
            Call DeleteRolDetalle()
            Call DataSetTable()

            Dim dtInsu As DataTable
            dtInsu = L_Formulario_General(1, " and ZY001.yamod=" & ListB_Empleados.SelectedValue).Tables(0)
            For Each campo As DataRow In dtInsu.Rows
                moDataTableTipoDato.Rows.Add(RowNewTable(campo, True))
            Next

            JGr_Detalle.DataSource = moDataTableTipoDato
            JGr_Detalle.RetrieveStructure()

            Call grdIntTable()
        End If
    End Sub
#End Region

#Region " Nuevo-Button "
    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        _PNuevoRegistro()
        JGr_Buscador.Enabled = False
    End Sub

    Private Sub _PNuevoRegistro()
        _PHabilitar()
        btnNuevo.Enabled = True

        _PLimpiar()
        Tb_Nombre.Focus()
        _Nuevo = True
    End Sub
#End Region

#Region " Grabar-Button "
    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        JGr_Detalle.UpdateData()
        If moDataTableTipoDato.Rows.Count > 0 Then
            mdtnColeccion.Remove(ListB_Empleados.SelectedValue) 'es solo en caso edit
            mdtnColeccion.Add(ListB_Empleados.SelectedValue, moDataTableTipoDato)
        End If
        _PGrabarRegistro()
    End Sub

    Private Sub _PGrabarRegistro()
        Dim _Error As Boolean = False
        If P_Validar() Then

            If False Then 'btnGrabar.Tag = 0
                btnGrabar.Tag = 1
                btnGrabar.Refresh()
                Exit Sub
            Else
                btnGrabar.Tag = 0
                btnGrabar.Refresh()
            End If

            If _Nuevo Then
                L_Rol_Grabar(Tb_Id.Text, Tb_Nombre.Text)
                Call Detalle_Grabar()

                'actualizar el grid de buscador
                _PCargarBuscador()

                Tb_Nombre.Focus()
                ToastNotification.Show(Me, "Codigo Rol " + Tb_Id.Text + " Grabado con Exito.", My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
                _PLimpiar()
            Else
                L_Rol_Modificar(Tb_Id.Text, Tb_Nombre.Text)
                Call Detalle_Grabar()

                _PCargarBuscador()
                ToastNotification.Show(Me, "Codigo Rol " + Tb_Id.Text + " Modificado con Exito.", My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)

                _Nuevo = False 'aumentado danny
                _PInhabilitar()
                _PFiltrar()

                intModulo = 0
                JGr_Detalle.ClearStructure()
                moDataTableTipoDato.Rows.Clear()
                mdtnColeccion.Clear()

                JGr_Detalle.Update()
                Call _PCargarListModulo()
            End If
        End If
    End Sub
#End Region

#Region " Cancelar-Button "
    Private Sub BBtn_Cancelar_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _PSalirRegistro()

    End Sub

    Private Sub _PSalirRegistro()
        If btnGrabar.Enabled = True Then
            _PLimpiar()
            _PInhabilitar()
            _PFiltrar()
        Else

            _modulo.Select()
            _tab.Close()
        End If
    End Sub
#End Region

#Region " Modificar-Button "
    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        _PModificarRegistro()
        JGr_Buscador.Enabled = False
    End Sub

    Private Sub _PModificarRegistro()
        _Nuevo = False
        _PHabilitar()
        'btnModificar.Enabled = True 'aumentado para q funcione con el modelo de guido
    End Sub
#End Region

#Region " Eliminar-Button "
    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        _PEliminarRegistro()
    End Sub

    Private Sub _PEliminarRegistro()
        Dim _Result As MsgBoxResult
        _Result = MsgBox("Esta seguro de Eliminar el Registro?".ToUpper, MsgBoxStyle.YesNo, "Advertencia".ToUpper)
        If _Result = MsgBoxResult.Yes Then
            L_Rol_Borrar(Tb_Id.Text)
            'borro el detalle del encabezado
            L_RolDetalle_Borrar(Tb_Id.Text)

            _PInhabilitar()
            _PFiltrar()
            _PCargarBuscador()
        Else
            _PInhabilitar()
        End If
    End Sub
#End Region
#End Region

    Private Sub btnPrimero_Click(sender As Object, e As EventArgs) Handles btnPrimero.Click
        _PPrimerRegistro()
    End Sub

    Private Sub _PPrimerRegistro()
        _Pos = 0
        _PMostrarRegistro(0)
        LblPaginacion.Text = Str(1) + "/" + _Dsencabezado.Tables(0).Rows.Count.ToString
    End Sub

    Private Sub btnUltimo_Click(sender As Object, e As EventArgs) Handles btnUltimo.Click
        _PUltimoRegistro()
    End Sub

    Private Sub _PUltimoRegistro()
        _Pos = _Dsencabezado.Tables(0).Rows.Count - 1
        _PMostrarRegistro(_Dsencabezado.Tables(0).Rows.Count - 1)
        LblPaginacion.Text = Str(_Dsencabezado.Tables(0).Rows.Count) + "/" + _Dsencabezado.Tables(0).Rows.Count.ToString
    End Sub

    Private Sub btnAnterior_Click(sender As Object, e As EventArgs) Handles btnAnterior.Click
        _PAnteriorRegistro()
    End Sub

    Private Sub _PAnteriorRegistro()
        If _Pos > 0 Then
            _Pos = _Pos - 1
            _PMostrarRegistro(_Pos)
            LblPaginacion.Text = Str(_Pos + 1) + "/" + _Dsencabezado.Tables(0).Rows.Count.ToString
        End If
    End Sub

    Private Sub btnSiguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click
        _PSiguienteRegistro()
    End Sub

    Private Sub _PSiguienteRegistro()
        If _Pos < _Dsencabezado.Tables(0).Rows.Count - 1 Then
            _Pos = _Pos + 1
            _PMostrarRegistro(_Pos)
            LblPaginacion.Text = Str(_Pos + 1) + "/" + _Dsencabezado.Tables(0).Rows.Count.ToString
        End If
    End Sub

    Private Sub F0_Roles_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _PIniciarTodo()
    End Sub
End Class