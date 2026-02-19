Imports Logica.AccesoLogica
Imports Janus.Windows.GridEX
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls

Public Class F0_Empresas
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

        Me.Text = "EMPRESAS VALES/GIFTCARD"
        _MaxLengthTextBox()
        _PFiltrar()
        _PCargarBuscador()
        _PInhabilitar()
        _PHabilitarFocus()
        _prAsignarPermisos()
        Dim blah As New Bitmap(New Bitmap(My.Resources.EMPRESA), 20, 20)
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico
        GroupPanel1.Style.BackColor = Color.FromArgb(13, 71, 161)
        GroupPanel1.Style.BackColor2 = Color.FromArgb(13, 71, 161)
        GroupPanel1.Style.TextColor = Color.White
        JGr_Buscador.Focus()
        swActivos.Value = True
    End Sub
    Public Sub _MaxLengthTextBox()
        Tb_Nombre.MaxLength = 120
        tbObservacion.MaxLength = 300
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
            Tb_Id.Text = .GetValue("cod").ToString
            Tb_Nombre.Text = .GetValue("descripcion").ToString
            cb_Tipo.Value = IIf(.GetValue("tipo") = 2, True, False)
            tbObservacion.Text = .GetValue("obs").ToString
            Tb_Estado.Value = CBool(.GetValue("estado"))


            If (IsDBNull(.GetValue("fact"))) Then
                lbFecha.Text = ""
            Else
                lbFecha.Text = CType(.GetValue("fact"), Date).ToString("dd/MM/yyyy")
            End If

            lbHora.Text = IIf(IsDBNull(.GetValue("hact")), "", .GetValue("hact").ToString)
            lbUsuario.Text = IIf(IsDBNull(.GetValue("uact")), "", .GetValue("uact").ToString)
        End With
    End Sub

    Private Sub _PInhabilitar()
        Tb_Id.ReadOnly = True
        Tb_Nombre.ReadOnly = True
        cb_Tipo.Enabled = False
        tbObservacion.ReadOnly = True
        Tb_Estado.IsReadOnly = True

        btnNuevo.Enabled = True
        btnModificar.Enabled = True
        btnEliminar.Enabled = True
        btnGrabar.Enabled = False

        JGr_Buscador.Enabled = True
        btnGrabar.Image = My.Resources.save

        _PLimpiarErrores()
    End Sub

    Private Sub _PLimpiarErrores()
        MEP.Clear()
        Tb_Nombre.BackColor = Color.White
        cb_Tipo.BackColor = Color.White
        tbObservacion.BackColor = Color.White
    End Sub

    Private Sub _PHabilitarFocus()
        MHighlighterFocus.SetHighlightOnFocus(Tb_Nombre, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        MHighlighterFocus.SetHighlightOnFocus(tbObservacion, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)

        Tb_Nombre.TabIndex = 1
        cb_Tipo.TabIndex = 2
        tbObservacion.TabIndex = 3
    End Sub

    Private Sub _PCargarBuscador()
        Dim dt = New DataTable

        dt = L_MostrarEmpresas(IIf(swActivos.Value = True, 1, 0))

        JGr_Buscador.BoundMode = BoundMode.Bound
        JGr_Buscador.DataSource = dt
        JGr_Buscador.RetrieveStructure()

        With JGr_Buscador.RootTable.Columns("cod")
            .Caption = "ID".ToUpper
            .Visible = True
        End With
        With JGr_Buscador.RootTable.Columns("descripcion")
            .Visible = True
            .Caption = "EMPRESA".ToUpper
            .Width = 410
        End With
        With JGr_Buscador.RootTable.Columns("tipo")
            .Visible = False
        End With
        With JGr_Buscador.RootTable.Columns("tventa")
            .Caption = "TIPO".ToUpper
            .Width = 90
        End With
        With JGr_Buscador.RootTable.Columns("obs")
            .Caption = "OBSERVACIÓN".ToUpper
            .Width = 600
            .Visible = True
        End With
        With JGr_Buscador.RootTable.Columns("cant")
            .Visible = False
        End With
        With JGr_Buscador.RootTable.Columns("estado")
            .Visible = False
        End With
        With JGr_Buscador.RootTable.Columns("est")
            .Caption = "ESTADO".ToUpper
            .Width = 90
            .Visible = True
        End With
        With JGr_Buscador.RootTable.Columns("fact")
            .Visible = False
        End With
        With JGr_Buscador.RootTable.Columns("hact")
            .Visible = False
        End With
        With JGr_Buscador.RootTable.Columns("uact")
            .Visible = False
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
        cb_Tipo.Enabled = True
        tbObservacion.ReadOnly = False
        Tb_Estado.IsReadOnly = False

        btnNuevo.Enabled = False
        btnModificar.Enabled = False
        btnEliminar.Enabled = False
        btnGrabar.Enabled = True

    End Sub

    Private Sub _PLimpiar()
        Tb_Id.Text = String.Empty
        Tb_Nombre.Text = String.Empty
        cb_Tipo.Value = True
        tbObservacion.Text = String.Empty
        Tb_Estado.Value = True
        LblPaginacion.Text = String.Empty
    End Sub

    Public Function P_Validar() As Boolean
        Dim _Error As Boolean = True
        MEP.Clear()
        If Tb_Nombre.Text.Trim = String.Empty Then
            Tb_Nombre.BackColor = Color.Red
            MEP.SetError(Tb_Nombre, "Ingrese nombre de la empresa!".ToUpper)
            _Error = False
        Else
            Tb_Nombre.BackColor = Color.White
            MEP.SetError(Tb_Nombre, String.Empty)
        End If

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
        Dim res As Boolean
        If P_Validar() Then

            If False Then
                btnGrabar.Tag = 1
                btnGrabar.Refresh()
                Exit Sub
            Else
                btnGrabar.Tag = 0
                btnGrabar.Refresh()
            End If

            If _Nuevo Then
                res = L_fnGrabarEmpresas(Tb_Id.Text, Tb_Nombre.Text, IIf(cb_Tipo.Value = True, 2, 7), IIf(Tb_Estado.Value = True, 1, 0), tbObservacion.Text.Trim,
                                  gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina)

                If res Then
                    Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
                    ToastNotification.Show(Me, "Código de Empresa ".ToUpper + Tb_Id.Text + " Grabado con éxito.".ToUpper,
                                          img, 2000, eToastGlowColor.Green, eToastPosition.TopCenter)
                    Tb_Nombre.Focus()
                    'actualizar el grid de buscador
                    _PCargarBuscador()
                    _PLimpiar()

                Else
                    Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                    ToastNotification.Show(Me, "La Empresa no pudo ser insertada".ToUpper,
                                           img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                End If

            Else
                res = L_fnModificarEmpresas(Tb_Id.Text, Tb_Nombre.Text, IIf(cb_Tipo.Value = True, 2, 7), IIf(Tb_Estado.Value = True, 1, 0), tbObservacion.Text.Trim,
                                  gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina)

                If res Then
                    Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
                    ToastNotification.Show(Me, "Código de Empresa ".ToUpper + Tb_Id.Text + " Modificado con éxito.".ToUpper,
                                           img, 2000, eToastGlowColor.Green, eToastPosition.TopCenter)
                    _PCargarBuscador()
                    _Nuevo = False 'aumentado danny
                    _PInhabilitar()
                    _PFiltrar()

                Else
                    Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                    ToastNotification.Show(Me, "La Empresa no pudo ser Modificada".ToUpper,
                                           img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                End If
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
            Dim mensajeError As String = ""
            Dim res As Boolean = L_fnEliminarEmpresa(Tb_Id.Text, mensajeError, gs_VersionSistema, gs_IPMaquina, gs_UsuMaquina)

            If res Then

                _PInhabilitar()
                _PFiltrar()
                _PCargarBuscador()
                Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
                ToastNotification.Show(Me, "Código de Empresa ".ToUpper + t + " eliminado con éxito.".ToUpper,
                                          img, 2000, eToastGlowColor.Green, eToastPosition.TopCenter)

            Else
                Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                ToastNotification.Show(Me, mensajeError, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            End If


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


    Private Sub BBtn_Anterior_Click(sender As Object, e As EventArgs) Handles btnAnterior.Click
        Dim _MPos As Integer = JGr_Buscador.Row
        If _MPos > 0 And JGr_Buscador.RowCount > 0 Then
            _MPos = _MPos - 1
            ''  _prMostrarRegistro(_MPos)
            JGr_Buscador.Row = _MPos
            LblPaginacion.Text = Str(_Pos + 1) + "/" + CType(JGr_Buscador.DataSource, DataTable).Rows.Count.ToString

        End If
    End Sub

    Private Sub BBtn_Siguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click
        Dim _pos As Integer = JGr_Buscador.Row
        If _pos < JGr_Buscador.RowCount - 1 Then
            _pos = JGr_Buscador.Row + 1
            '' _prMostrarRegistro(_pos)
            JGr_Buscador.Row = _pos
            LblPaginacion.Text = Str(_pos + 1) + "/" + JGr_Buscador.RowCount.ToString
        End If
    End Sub

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


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        _Inter = _Inter + 1
        If _Inter = 1 Then
            Me.WindowState = FormWindowState.Normal
        Else
            Me.Opacity = 100
            Timer1.Enabled = False
        End If
    End Sub

    Private Sub swActivos_ValueChanged(sender As Object, e As EventArgs) Handles swActivos.ValueChanged
        _PCargarBuscador()
    End Sub


End Class