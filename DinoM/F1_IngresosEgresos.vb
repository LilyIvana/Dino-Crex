Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Imports System.IO
Imports Janus.Windows.GridEX

Public Class F1_IngresosEgresos

#Region "Variable Globales"
    Dim _Inter As Integer = 0
    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
    Dim RutaGlobal As String = gs_CarpetaRaiz
    Dim RutaTemporal As String = "C:\Temporal"
    Dim Modificado As Boolean = False
    Dim nameImg As String = "Default.jpg"
    Dim Socio As Boolean = False
    Dim NumiCuentaContable As Integer = 0
#End Region
#Region "METODOS PRIVADOS"

    Private Sub _prIniciarTodo()

        Me.Text = "I N G R E S O S / E G R E S O S"
        _prCargarComboLibreria(cbConcepto, 9, 1)
        _PMIniciarTodo()
        _prAsignarPermisos()
        _prCargarLengthTextBox()

        GroupPanelBuscador.Style.BackColor = Color.FromArgb(13, 71, 161)
        GroupPanelBuscador.Style.BackColor2 = Color.FromArgb(13, 71, 161)
        GroupPanelBuscador.Style.TextColor = Color.White
        Dim blah As Bitmap = My.Resources.checked
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico
        JGrM_Buscador.RootTable.HeaderFormatStyle.FontBold = TriState.True
        JGrM_Buscador.AlternatingColors = True
        btnModificar.Enabled = True
        btnEliminar.Enabled = True
    End Sub

    Public Sub _prCargarLengthTextBox()
        tbDescripcion.MaxLength = 200
        cbConcepto.MaxLength = 40
        tbObservacion.MaxLength = 300
    End Sub



    Private Function _fnActionNuevo() As Boolean
        'Funcion que me devuelve True si esta en la actividad crear nuevo Tipo de Equipo
        Return tbcodigo.Text.ToString.Equals("") And tbDescripcion.ReadOnly = False
    End Function

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

    Private Sub _prCargarComboLibreria(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo, cod1 As String, cod2 As String)
        Dim dt As New DataTable
        dt = L_prLibreriaClienteLGeneral(cod1, cod2)
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("yccod3").Width = 70
            .DropDownList.Columns("yccod3").Caption = "COD"
            .DropDownList.Columns.Add("ycdes3").Width = 200
            .DropDownList.Columns("ycdes3").Caption = "DESCRIPCION"
            .ValueMember = "yccod3"
            .DisplayMember = "ycdes3"
            .DataSource = dt
            .Refresh()
        End With
    End Sub
    Private Sub MostrarMensajeError(mensaje As String)
        ToastNotification.Show(Me,
                               mensaje.ToUpper,
                               My.Resources.WARNING,
                               4000,
                               eToastGlowColor.Red,
                               eToastPosition.TopCenter)
    End Sub
#End Region
#Region "METODOS SOBREESCRITOS"

    Public Overrides Sub _PMOHabilitar()
        swTipo.IsReadOnly = False
        dpFecha.Enabled = True
        tbDescripcion.ReadOnly = False
        cbConcepto.ReadOnly = False
        tbMonto.IsInputReadOnly = False
        tbObservacion.ReadOnly = False

    End Sub
    Public Overrides Sub _PMOInhabilitar()
        tbcodigo.ReadOnly = True
        tbIdCaja.ReadOnly = True
        swTipo.IsReadOnly = True
        dpFecha.Enabled = False
        tbDescripcion.ReadOnly = True
        cbConcepto.ReadOnly = True
        tbMonto.IsInputReadOnly = True
        tbObservacion.ReadOnly = True

    End Sub
    Public Overrides Sub _PMOHabilitarFocus()
        With MHighlighterFocus
            .SetHighlightOnFocus(tbDescripcion, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(cbConcepto, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(tbMonto, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(tbObservacion, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(tbcodigo, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(tbIdCaja, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        End With
    End Sub
    Public Overrides Sub _PMOLimpiar()

        tbcodigo.Text = ""
        tbIdCaja.Text = ""
        swTipo.Value = True
        dpFecha.Value = Now.Date
        tbDescripcion.Text = ""
        'cbConcepto.SelectedIndex = 0
        tbMonto.Value = 0
        tbObservacion.Text = ""

        tbDescripcion.Focus()
        lbNroCaja.Text = ""
    End Sub
    Public Overrides Sub _PMOLimpiarErrores()
        MEP.Clear()
        tbcodigo.BackColor = Color.White
        tbIdCaja.BackColor = Color.White
        tbDescripcion.BackColor = Color.White
        cbConcepto.BackColor = Color.White
        tbMonto.BackColor = Color.White
        tbObservacion.BackColor = Color.White
    End Sub
    Public Overrides Function _PMOValidarCampos() As Boolean
        Dim _ok As Boolean = True
        MEP.Clear()

        If tbDescripcion.Text = String.Empty Then
            tbDescripcion.BackColor = Color.Red
            MEP.SetError(tbDescripcion, "ingrese Dato en el campo Descripcion !".ToUpper)
            _ok = False
        Else
            tbDescripcion.BackColor = Color.White
            MEP.SetError(tbDescripcion, "")
        End If
        If (tbMonto.Value <= 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            MEP.SetError(tbMonto, "Por Favor introduzca monto !".ToUpper)
            _ok = False
        Else
            tbMonto.BackColor = Color.White
            MEP.SetError(tbMonto, "")
        End If


        MHighlighterFocus.UpdateHighlights()
        Return _ok
    End Function

    Public Overrides Function _PMOGetListEstructuraBuscador() As List(Of Modelo.Celda)
        Dim listEstCeldas As New List(Of Modelo.Celda)


        listEstCeldas.Add(New Modelo.Celda("ienumi", True, "Codigo", 120))
        listEstCeldas.Add(New Modelo.Celda("ieFecha", True, "Fecha", 100))
        listEstCeldas.Add(New Modelo.Celda("ieTipo", False))
        listEstCeldas.Add(New Modelo.Celda("Tipo", True, "Tipo", 120))
        listEstCeldas.Add(New Modelo.Celda("ieDescripcion", True, "Descripción", 350))
        listEstCeldas.Add(New Modelo.Celda("ieConcepto", False))
        listEstCeldas.Add(New Modelo.Celda("ycdes3", True, "Concepto", 250))
        listEstCeldas.Add(New Modelo.Celda("ieMonto", True, "Monto", 150, "0.00"))
        listEstCeldas.Add(New Modelo.Celda("ieObs", False))
        listEstCeldas.Add(New Modelo.Celda("ieEstado", False))
        listEstCeldas.Add(New Modelo.Celda("ieIdCaja", False))
        listEstCeldas.Add(New Modelo.Celda("iefact", False))
        listEstCeldas.Add(New Modelo.Celda("iehact", False))
        listEstCeldas.Add(New Modelo.Celda("ieuact", False))
        listEstCeldas.Add(New Modelo.Celda("NroCaja", True, "Nro. Caja", 100))

        Return listEstCeldas

    End Function
    Public Overrides Function _PMOGetTablaBuscador() As DataTable
        Dim dtBuscador As DataTable = L_prIngresoEgresoGeneral()
        Return dtBuscador
    End Function


    Public Overrides Sub _PMOMostrarRegistro(_N As Integer)
        JGrM_Buscador.Row = _MPos

        't.canumi , t.canombre, t.cacuenta, t.caobs, t.cafact, t.cahact, t.cauact 
        With JGrM_Buscador
            tbcodigo.Text = .GetValue("ienumi").ToString
            tbIdCaja.Text = .GetValue("ieIdCaja").ToString
            dpFecha.Value = .GetValue("ieFecha")
            swTipo.Value = .GetValue("ieTipo")
            tbDescripcion.Text = .GetValue("ieDescripcion").ToString
            cbConcepto.Value = .GetValue("ieConcepto")
            tbMonto.Value = .GetValue("ieMonto")
            tbObservacion.Text = .GetValue("ieObs").ToString
            lbNroCaja.Text = .GetValue("NroCaja")

            lbFecha.Text = CType(.GetValue("iefact"), Date).ToString("dd/MM/yyyy")
            lbHora.Text = .GetValue("iehact").ToString
            lbUsuario.Text = .GetValue("ieuact").ToString

            'diseño de la grilla para el Total
            .TotalRow = InheritableBoolean.True
            .TotalRowFormatStyle.BackColor = Color.Gold
            .TotalRowPosition = TotalRowPosition.BottomFixed
        End With
        With JGrM_Buscador.RootTable.Columns("ieMonto")
            .AggregateFunction = AggregateFunction.Sum
        End With

        LblPaginacion.Text = Str(_MPos + 1) + "/" + JGrM_Buscador.RowCount.ToString

    End Sub
    Public Overrides Function _PMOGrabarRegistro() As Boolean

        Dim tipo As Integer = IIf(swTipo.Value = True, 1, 0)
        Dim res As Boolean = L_prIngresoEgresoGrabar(tbcodigo.Text, dpFecha.Value, tipo, tbDescripcion.Text, cbConcepto.Value, tbMonto.Value, tbObservacion.Text, gs_NroCaja)
        If res Then
            Modificado = False
            _PMOLimpiar()
            ToastNotification.Show(Me, "Codigo de Ingreso/Egreso".ToUpper + tbcodigo.Text + " Grabado con éxito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
        End If
        Return res

    End Function
    Public Overrides Function _PMOModificarRegistro() As Boolean
        Dim res As Boolean
        Dim tipo As Integer = IIf(swTipo.Value = True, 1, 0)
        If (Modificado = False) Then
            res = L_prIngresoEgresoModificar(tbcodigo.Text, dpFecha.Value, tipo, tbDescripcion.Text, cbConcepto.Value, tbMonto.Value, tbObservacion.Text)

        Else
            res = L_prIngresoEgresoModificar(tbcodigo.Text, dpFecha.Value, tipo, tbDescripcion.Text, cbConcepto.Value, tbMonto.Value, tbObservacion.Text)
        End If
        If res Then
            Modificado = False
            _PMInhabilitar()
            _PMPrimerRegistro()
            ToastNotification.Show(Me, "Codigo de Ingreso/Egreso".ToUpper + tbcodigo.Text + " modificado con éxito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
        End If
        Return res

    End Function


    Public Overrides Sub _PMOEliminarRegistro()
        If tbIdCaja.Text = 0 Then
            Dim info As New TaskDialogInfo("¿esta seguro de eliminar el registro?".ToUpper, eTaskDialogIcon.Delete, "advertencia".ToUpper, "mensaje principal".ToUpper, eTaskDialogButton.Yes Or eTaskDialogButton.Cancel, eTaskDialogBackgroundColor.Blue)
            Dim result As eTaskDialogResult = TaskDialog.Show(info)
            If result = eTaskDialogResult.Yes Then
                Dim mensajeError As String = ""
                Dim res As Boolean = L_prIngresoEgresoBorrar(tbcodigo.Text, mensajeError)
                If res Then
                    ToastNotification.Show(Me, "Codigo de Ingreso/Egreso ".ToUpper + tbcodigo.Text + " eliminado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
                    _PMFiltrar()
                Else
                    ToastNotification.Show(Me, mensajeError, My.Resources.WARNING, 8000, eToastGlowColor.Red, eToastPosition.TopCenter)
                End If
            End If
        Else
            ToastNotification.Show(Me, "No puede Eliminar un Ingreso/Egreso, ya se hizo cierre de caja, por favor primero elimine cierre de caja".ToUpper, My.Resources.WARNING, 5000, eToastGlowColor.Red, eToastPosition.TopCenter)
        End If
    End Sub


#End Region
#Region "EVENTOS"
    Private Sub F1_IngresosEgresos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()

    End Sub
    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        tbDescripcion.Focus()
    End Sub
    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        If btnGrabar.Enabled = True Then
            _PMInhabilitar()
            _PMPrimerRegistro()
            PanelNavegacion.Enabled = True
        Else
            Close()
        End If
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

    Private Sub btConcepto_Click(sender As Object, e As EventArgs) Handles btConcepto.Click
        Dim numi As String = ""

        If L_prLibreriaGrabar(numi, "9", "1", cbConcepto.Text, "") Then
            _prCargarComboLibreria(cbConcepto, "9", "1")
            cbConcepto.SelectedIndex = CType(cbConcepto.DataSource, DataTable).Rows.Count - 1
        End If
    End Sub

    Private Sub cbConcepto_ValueChanged(sender As Object, e As EventArgs) Handles cbConcepto.ValueChanged
        If cbConcepto.SelectedIndex < 0 And cbConcepto.Text <> String.Empty Then
            btConcepto.Visible = True
        Else
            btConcepto.Visible = False
        End If
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        If tbIdCaja.Text > 0 Then
            ToastNotification.Show(Me, "No puede Modificar un Ingreso/Egreso, ya se hizo cierre de caja, por favor primero elimine cierre de caja".ToUpper, My.Resources.WARNING, 5000, eToastGlowColor.Red, eToastPosition.TopCenter)
            btnNuevo.Enabled = True
            btnModificar.Enabled = True
            btnGrabar.Enabled = False
            btnEliminar.Enabled = True
            _PMInhabilitar()
            '_PMFiltrar()
            'Exit Sub
        End If

    End Sub



#End Region
End Class