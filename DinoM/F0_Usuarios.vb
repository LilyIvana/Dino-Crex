Imports Logica.AccesoLogica
Imports Janus.Windows.GridEX
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
'DESARROLLADO POR: DANNY GUTIERREZ
Public Class F0_Usuarios
    Dim _Inter As Integer = 0

#Region "ATRIBUTOS"
    Dim _Dsencabezado As DataSet
    Dim _Nuevo As Boolean
    Private _Pos As Integer
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
    Public _nameButton As String
    Dim NumiVendedor As Integer = 0
#End Region

#Region "METODOS PRIVADOS"
    Private Sub _PIniciarTodo()

        Me.Text = "USUARIOS"
        'Me.WindowState = FormWindowState.Maximized

        _PCargarComboRol(JMC_Categoria)
        _prCargarComboSucursal()
        _MaxLengthTextBox()
        _PFiltrar()
        _PCargarBuscador()
        _PInhabilitar()

        _PHabilitarFocus()



        _pCambiarFuente()

        _prAsignarPermisos()
        Dim blah As New Bitmap(New Bitmap(My.Resources.user), 20, 20)


        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico
        GroupPanel1.Style.BackColor = Color.FromArgb(13, 71, 161)
        GroupPanel1.Style.BackColor2 = Color.FromArgb(13, 71, 161)
        GroupPanel1.Style.TextColor = Color.White
        JGr_Buscador.Focus()

    End Sub
    Public Sub _MaxLengthTextBox()
        Tb_Nombre.MaxLength = 10
        TextBoxX1.MaxLength = 20
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

    End Sub

    Private Sub _PCargarComboRol(ByVal cb As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim _Ds As New DataSet
        _Ds = L_Rol_General(0)

        cb.DropDownList.Columns.Clear()

        cb.DropDownList.Columns.Add(_Ds.Tables(0).Columns("ybnumi").ToString).Width = 60
        cb.DropDownList.Columns(0).Caption = "Código".ToUpper
        cb.DropDownList.Columns.Add(_Ds.Tables(0).Columns("ybrol").ToString).Width = 250
        cb.DropDownList.Columns(1).Caption = "Rol".ToUpper

        cb.ValueMember = _Ds.Tables(0).Columns("ybnumi").ToString
        cb.DisplayMember = _Ds.Tables(0).Columns("ybrol").ToString
        cb.DataSource = _Ds.Tables(0)
        cb.Refresh()


    End Sub

    Private Sub _prCargarComboSucursal()
        Dim dt As New DataTable
        dt = L_fnListarSucursales()

        With tbSuc
            .DropDownList.Columns.Clear()

            .DropDownList.Columns.Add("aanumi").Width = 70
            .DropDownList.Columns("aanumi").Caption = "COD"

            .DropDownList.Columns.Add("aabdes").Width = 200
            .DropDownList.Columns("aabdes").Caption = "descripcion".ToUpper

            .ValueMember = "aanumi"
            .DisplayMember = "aabdes"
            .DataSource = dt
            .Refresh()
        End With
    End Sub

    Private Sub _PFiltrar()
        _Dsencabezado = New DataSet
        _Dsencabezado = L_Usuario_General2(0)
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
        Dim dt As DataTable = CType(JGr_Buscador.DataSource, DataTable)
        If (IsNothing(CType(JGr_Buscador.DataSource, DataTable))) Then
            Return
        End If
        With JGr_Buscador
            Tb_Id.Text = .GetValue("ydnumi").ToString
            Tb_Nombre.Text = .GetValue("yduser").ToString
            TextBoxX1.Text = .GetValue("ydpass").ToString
            Tb_Estado.Value = CBool(.GetValue("ydest").ToString)
            tbTodasSucursales.Value = CBool(IIf(.GetValue("ydall") = 1, True, False))
            Tb_DiasPedidos.Value = CInt(.GetValue("ydcant").ToString)
            Tb_Cajero.Value = IIf(.GetValue("ydfontsize") = 0, False, True)

            JMC_Categoria.Value = CInt(.GetValue("ybnumi").ToString)
            tbSuc.Value = CInt(.GetValue("ydsuc").ToString)
            NumiVendedor = CInt(.GetValue("yd_numiVend").ToString)
            Dim _tabla As DataTable = L_fnListarEmpleado()
            If _tabla.Rows.Count > 0 Then
                Dim fila As DataRow() = _tabla.Select("ydnumi= " + NumiVendedor.ToString, "")
                If fila.Count > 0 Then
                    tbVendedor.Text = fila(0).ItemArray(2)
                Else
                    tbVendedor.Text = ""
                End If
            End If
            If (IsDBNull(.GetValue("ydfact"))) Then
                lbFecha.Text = ""
            Else
                lbFecha.Text = CType(.GetValue("ydfact"), Date).ToString("dd/MM/yyyy")
            End If
            ''  lbFecha.Text = IIf(IsDBNull(.GetValue("ydfact")), "", CType(.GetValue("ydfact"), Date).ToString("dd/MM/yyyy"))
            lbHora.Text = IIf(IsDBNull(.GetValue("ybhact")), "", .GetValue("ybhact").ToString)
            lbUsuario.Text = IIf(IsDBNull(.GetValue("ybuact")), "", .GetValue("ybuact").ToString)
        End With
    End Sub

    Private Sub _PInhabilitar()
        Tb_Id.ReadOnly = True
        Tb_Nombre.ReadOnly = True
        TextBoxX1.ReadOnly = True
        Tb_DiasPedidos.Enabled = False
        Tb_Cajero.Enabled = False
        tbSuc.ReadOnly = True
        btnSearch.Visible = False
        tbVendedor.ReadOnly = True

        btnNuevo.Enabled = True
        btnModificar.Enabled = True
        btnEliminar.Enabled = True
        btnGrabar.Enabled = False

        JMC_Categoria.ReadOnly = True
        Tb_Estado.IsReadOnly = True
        tbTodasSucursales.IsReadOnly = True
        JGr_Buscador.Enabled = True

        btnGrabar.Image = My.Resources.save

        _PLimpiarErrores()
    End Sub

    Private Sub _PLimpiarErrores()
        MEP.Clear()
        Tb_Nombre.BackColor = Color.White
        TextBoxX1.BackColor = Color.White
        Tb_DiasPedidos.BackColor = Color.White
        Tb_Cajero.BackColor = Color.White
        JMC_Categoria.BackColor = Color.White
        tbSuc.BackColor = Color.White
    End Sub

    Private Sub _PHabilitarFocus()
        MHighlighterFocus.SetHighlightOnFocus(Tb_Nombre, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        MHighlighterFocus.SetHighlightOnFocus(TextBoxX1, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        MHighlighterFocus.SetHighlightOnFocus(tbSuc, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)

        Tb_Nombre.TabIndex = 1
        TextBoxX1.TabIndex = 2
        JMC_Categoria.TabIndex = 3
        Tb_Estado.TabIndex = 4
        Tb_DiasPedidos.TabIndex = 5
        Tb_Cajero.TabIndex = 6
    End Sub

    Private Sub _PCargarBuscador()
        _Dsencabezado = New DataSet
        _Dsencabezado = L_Usuario_General2(0)

        JGr_Buscador.BoundMode = BoundMode.Bound
        JGr_Buscador.DataSource = _Dsencabezado.Tables(0) ' _Dsencabezado.Tables(0) ' dt
        JGr_Buscador.RetrieveStructure()

        With JGr_Buscador.RootTable.Columns("ydnumi")
            .Visible = False
        End With
        With JGr_Buscador.RootTable.Columns("ydall")
            .Visible = False
        End With
        With JGr_Buscador.RootTable.Columns("yduser")
            '   .Visible = False
            .Caption = "Usuario".ToUpper
            .Width = 150
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center

        End With

        With JGr_Buscador.RootTable.Columns("ydpass")
            .Visible = False
            '.Caption = "Contraseña"
            '.Width = 150
            '.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            '.CellStyle.FontSize = gi_fuenteTamano
        End With

        With JGr_Buscador.RootTable.Columns("ydest")
            .Caption = "Estado".ToUpper
            .Width = 150
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center

        End With

        With JGr_Buscador.RootTable.Columns("ybnumi")
            .Visible = False
        End With

        With JGr_Buscador.RootTable.Columns("ydcant")
            .Caption = "Dias Venc.".ToUpper
            .Width = 150
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

        End With

        With JGr_Buscador.RootTable.Columns("ydfontsize")
            .Caption = "Caja".ToUpper
            .Width = 130
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False

        End With

        With JGr_Buscador.RootTable.Columns("ybrol")
            .Caption = "Rol".ToUpper
            .Width = 150
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center

        End With

        With JGr_Buscador.RootTable.Columns("ydsuc")
            .Visible = False
        End With

        With JGr_Buscador.RootTable.Columns("ydfact")
            .Visible = False
        End With
        With JGr_Buscador.RootTable.Columns("ybhact")
            .Visible = False
        End With
        With JGr_Buscador.RootTable.Columns("ybuact")
            .Visible = False
        End With
        With JGr_Buscador.RootTable.Columns("yd_numiVend")
            .Visible = False
        End With
        With JGr_Buscador.RootTable.Columns("aabdes")
            .Caption = "Sucursal".ToUpper
            .Width = 250
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center

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

#End Region

    Private Sub F0_Usuarios_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _PIniciarTodo()
    End Sub


#Region " Evento-Button "
#Region " Metodo-Button "
    Private Sub _PHabilitar()
        Tb_Nombre.ReadOnly = False
        TextBoxX1.ReadOnly = False
        JMC_Categoria.ReadOnly = False
        Tb_Estado.IsReadOnly = True
        tbTodasSucursales.IsReadOnly = False
        Tb_DiasPedidos.Enabled = True
        Tb_Cajero.Enabled = True
        tbSuc.ReadOnly = False
        btnSearch.Visible = True
        tbVendedor.ReadOnly = False
        btnNuevo.Enabled = False
        btnModificar.Enabled = False
        btnEliminar.Enabled = False
        btnGrabar.Enabled = True
    End Sub

    Private Sub _PLimpiar()
        Tb_Id.Text = String.Empty
        Tb_Nombre.Text = String.Empty
        TextBoxX1.Text = String.Empty
        JMC_Categoria.Value = Nothing
        JMC_Categoria.SelectedIndex = -1
        Tb_Estado.Value = True
        tbTodasSucursales.Value = False
        Tb_DiasPedidos.Value = 0
        Tb_Cajero.Value = False
        tbSuc.SelectedIndex = -1
        tbVendedor.Clear()
        LblPaginacion.Text = String.Empty
    End Sub

    Public Function P_Validar() As Boolean
        Dim _Error As Boolean = True
        MEP.Clear()
        If Tb_Nombre.Text.Trim = String.Empty Then
            Tb_Nombre.BackColor = Color.Red
            MEP.SetError(Tb_Nombre, "Ingrese usuario!".ToUpper)
            _Error = False
        Else
            Tb_Nombre.BackColor = Color.White
            MEP.SetError(Tb_Nombre, String.Empty)
        End If

        If TextBoxX1.Text.Trim = String.Empty Then
            TextBoxX1.BackColor = Color.Red
            MEP.SetError(TextBoxX1, "Ingrese contraseña!".ToUpper)
            _Error = False
        Else
            TextBoxX1.BackColor = Color.White
            MEP.SetError(TextBoxX1, String.Empty)
        End If

        If JMC_Categoria.SelectedIndex < 0 Then
            JMC_Categoria.BackColor = Color.Red   'error de validacion
            MEP.SetError(JMC_Categoria, "Seleccione un rol!".ToUpper)
            _Error = False
        Else
            JMC_Categoria.BackColor = Color.White
            MEP.SetError(JMC_Categoria, String.Empty)
        End If

        'If tbSuc.SelectedIndex < 0 Then
        '    tbSuc.BackColor = Color.Red   'error de validacion
        '    MEP.SetError(tbSuc, "Seleccione una sucursal!".ToUpper)
        '    _Error = False
        'Else
        '    tbSuc.BackColor = Color.White
        '    MEP.SetError(tbSuc, String.Empty)
        'End If

        If Tb_DiasPedidos.Text.Trim = String.Empty Then
            Tb_DiasPedidos.BackColor = Color.Red
            MEP.SetError(Tb_DiasPedidos, "Ingrese dias vencimiento!".ToUpper)
            _Error = False
        Else
            Tb_DiasPedidos.BackColor = Color.White
            MEP.SetError(Tb_DiasPedidos, String.Empty)
        End If

        'If Tb_Cajero.Text.Trim = String.Empty Then
        '    Tb_Cajero.BackColor = Color.Red
        '    MEP.SetError(Tb_Cajero, "Ingrese el tamaño de fuente!".ToUpper)
        '    _Error = False
        'Else
        '    Tb_Cajero.BackColor = Color.White
        '    MEP.SetError(Tb_Cajero, String.Empty)
        'End If

        MHighlighterFocus.UpdateHighlights()
        Return _Error
    End Function
#End Region

#Region " Nuevo-Button "
    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        _PNuevoRegistro()
        JGr_Buscador.Enabled = False
    End Sub

    Private Sub _PNuevoRegistro()
        _PHabilitar()
        'btnNuevo.Enabled = True

        _PLimpiar()
        Tb_Nombre.Focus()
        _Nuevo = True
    End Sub

#Region " Grabar-Button "
    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
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
                L_Usuario_Grabar(Tb_Id.Text, Tb_Nombre.Text, TextBoxX1.Text, JMC_Categoria.Value, Tb_Estado.Value, Tb_DiasPedidos.Value, IIf(Tb_Cajero.Value = False, 0, 1), tbSuc.Value, IIf(tbTodasSucursales.Value = True, "1", "0"), NumiVendedor)

                Tb_Nombre.Focus()
                ToastNotification.Show(Me, "Codigo Usuario ".ToUpper + Tb_Id.Text + " Grabado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)

                'actualizar el grid de buscador
                _PCargarBuscador()
                _PLimpiar()
            Else
                L_Usuario_Modificar(Tb_Id.Text, Tb_Nombre.Text, TextBoxX1.Text, JMC_Categoria.Value, Tb_Estado.Value, Tb_DiasPedidos.Value, IIf(Tb_Cajero.Value = False, 0, 1), tbSuc.Value, IIf(tbTodasSucursales.Value = True, "1", "0"), NumiVendedor)

                ToastNotification.Show(Me, "Codigo Usuario ".ToUpper + Tb_Id.Text + " Modificado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)

                _PCargarBuscador()
                _Nuevo = False 'aumentado danny
                _PInhabilitar()
                _PFiltrar()
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
            _PCargarBuscador()
        Else
            _modulo.Select()
            Me.Close()

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
        Dim ef = New Efecto


        ef.tipo = 2
        ef.Context = "¿esta seguro de eliminar el registro?".ToUpper
        ef.Header = "mensaje principal".ToUpper
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then
            Dim t As String = Tb_Id.Text
            L_Usuario_Borrar(Tb_Id.Text)

            _PInhabilitar()
            _PFiltrar()
            _PCargarBuscador()

            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)

            ToastNotification.Show(Me, "Código de Usuario ".ToUpper + t + " eliminado con Exito.".ToUpper,
                                          img, 2000,
                                          eToastGlowColor.Green,
                                          eToastPosition.TopCenter)

            'Else
            '    Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            '    ToastNotification.Show(Me, mensajeError, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        Else
            _PInhabilitar()
        End If







    End Sub
#End Region
#End Region
#End Region

    Private Sub BBtn_Inicio_Click(sender As Object, e As EventArgs) Handles btnPrimero.Click

        _PPrimerRegistro()

    End Sub

    Private Sub _PPrimerRegistro()
        Dim _MPos As Integer
        If JGr_Buscador.RowCount > 0 Then
            _MPos = 0
            ''   _prMostrarRegistro(_MPos)
            JGr_Buscador.Row = _MPos
        End If
        LblPaginacion.Text = Str(1) + "/" + CType(JGr_Buscador.DataSource, DataTable).Rows.Count.ToString
    End Sub

    Private Sub BBtn_Ultimo_Click(sender As Object, e As EventArgs) Handles btnUltimo.Click
        Dim _pos As Integer = JGr_Buscador.Row
        If JGr_Buscador.RowCount > 0 Then
            _pos = JGr_Buscador.RowCount - 1
            ''  _prMostrarRegistro(_pos)
            JGr_Buscador.Row = _pos
            LblPaginacion.Text = Str(JGr_Buscador.RowCount).Trim + "/" + Str(JGr_Buscador.RowCount).Trim
        End If
    End Sub

    'Private Sub _PUltimoRegistro()
    '    _Pos = _Dsencabezado.Tables(0).Rows.Count - 1
    '    _PMostrarRegistro(_Dsencabezado.Tables(0).Rows.Count - 1)
    '    LblPaginacion.Text = Str(_Dsencabezado.Tables(0).Rows.Count) + "/" + _Dsencabezado.Tables(0).Rows.Count.ToString
    'End Sub

    Private Sub BBtn_Anterior_Click(sender As Object, e As EventArgs) Handles btnAnterior.Click
        Dim _MPos As Integer = JGr_Buscador.Row
        If _MPos > 0 And JGr_Buscador.RowCount > 0 Then
            _MPos = _MPos - 1
            ''  _prMostrarRegistro(_MPos)
            JGr_Buscador.Row = _MPos
            LblPaginacion.Text = Str(_Pos + 1) + "/" + CType(JGr_Buscador.DataSource, DataTable).Rows.Count.ToString

        End If
    End Sub

    'Private Sub _PAnteriorRegistro()
    '    If _Pos > 0 Then
    '        _Pos = _Pos - 1
    '        _PMostrarRegistro(_Pos)
    '        LblPaginacion.Text = Str(_Pos + 1) + "/" + _Dsencabezado.Tables(0).Rows.Count.ToString
    '    End If
    'End Sub

    Private Sub BBtn_Siguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click
        Dim _pos As Integer = JGr_Buscador.Row
        If _pos < JGr_Buscador.RowCount - 1 Then
            _pos = JGr_Buscador.Row + 1
            '' _prMostrarRegistro(_pos)
            JGr_Buscador.Row = _pos
            LblPaginacion.Text = Str(_pos + 1) + "/" + JGr_Buscador.RowCount.ToString
        End If
    End Sub

    'Private Sub _PSiguienteRegistro()
    '    If _Pos < _Dsencabezado.Tables(0).Rows.Count - 1 Then
    '        _Pos = _Pos + 1
    '        _PMostrarRegistro(_Pos)
    '        LblPaginacion.Text = Str(_Pos + 1) + "/" + _Dsencabezado.Tables(0).Rows.Count.ToString
    '    End If
    'End Sub

    Private Sub JGr_Buscador_SelectionChanged(sender As Object, e As EventArgs) Handles JGr_Buscador.SelectionChanged
        If JGr_Buscador.Row >= 0 Then
            _PMostrarRegistro(JGr_Buscador.Row)
            LblPaginacion.Text = Str(JGr_Buscador.Row + 1) + "/" + _Dsencabezado.Tables(0).Rows.Count.ToString
        End If
    End Sub

    Private Sub JGr_Buscador_EditingCell(sender As Object, e As EditingCellEventArgs) Handles JGr_Buscador.EditingCell
        e.Cancel = True
    End Sub

    Private Function SuperTabControl1() As Object
        Throw New NotImplementedException
    End Function

    Private Function SuperTabControlPanel1() As Object
        Throw New NotImplementedException
    End Function
    Function _fnAccesible() As Boolean
        Return Tb_Nombre.ReadOnly = False
    End Function
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If (_fnAccesible()) Then
            Dim dt As DataTable
            dt = L_fnListarEmpleado()
            '              a.ydnumi, a.ydcod, a.yddesc, a.yddctnum, a.yddirec
            ',a.ydtelf1 ,a.ydfnac 

            Dim listEstCeldas As New List(Of Modelo.Celda)
            listEstCeldas.Add(New Modelo.Celda("ydnumi,", False, "ID", 50))
            listEstCeldas.Add(New Modelo.Celda("ydcod", True, "ID", 50))
            listEstCeldas.Add(New Modelo.Celda("yddesc", True, "NOMBRE", 280))
            listEstCeldas.Add(New Modelo.Celda("yddctnum", True, "N. Documento".ToUpper, 150))
            listEstCeldas.Add(New Modelo.Celda("yddirec", True, "DIRECCION", 220))
            listEstCeldas.Add(New Modelo.Celda("ydtelf1", True, "Telefono".ToUpper, 200))
            listEstCeldas.Add(New Modelo.Celda("ydfnac", True, "F.Nacimiento".ToUpper, 150, "MM/dd,YYYY"))
            Dim ef = New Efecto
            ef.tipo = 3
            ef.dt = dt
            ef.SeleclCol = 1
            ef.listEstCeldas = listEstCeldas
            ef.alto = 50
            ef.ancho = 350
            ef.Context = "Seleccione Vendedor".ToUpper
            ef.ShowDialog()
            Dim bandera As Boolean = False
            bandera = ef.band
            If (bandera = True) Then
                Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
                If (IsNothing(Row)) Then
                    tbVendedor.Focus()
                    Return
                End If
                NumiVendedor = Row.Cells("ydnumi").Value
                tbVendedor.Text = Row.Cells("yddesc").Value
            End If
        End If
    End Sub

    Private Sub tbSuc_ValueChanged(sender As Object, e As EventArgs) Handles tbSuc.ValueChanged

    End Sub

    Private Sub JMC_Categoria_ValueChanged(sender As Object, e As EventArgs) Handles JMC_Categoria.ValueChanged

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