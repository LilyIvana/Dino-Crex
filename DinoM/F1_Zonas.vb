

Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports Janus.Windows.GridEX
Imports System.IO
Imports DevComponents.DotNetBar.SuperGrid
Imports GMap.NET.MapProviders
Imports GMap.NET
Imports GMap.NET.WindowsForms.Markers
Imports GMap.NET.WindowsForms
Imports GMap.NET.WindowsForms.ToolTips
Imports DevComponents.DotNetBar.Controls

Public Class F1_Zonas
#Region "Variables Locales"
#Region "MApas"
    Dim _Punto As Integer
    Dim _ListPuntos As List(Of PointLatLng) = New List(Of PointLatLng)

    Dim _Overlay As GMapOverlay
    Dim _latitud As Double = 0
    Dim _longitud As Double = 0
#End Region
    Public _nameButton As String
    Public _tab As SuperTabItem
    Dim _TablePoint As DataTable
    Dim _color As String = ""
    Public _modulo As SideNavItem
    Dim _AddPoint As Boolean = False
    Dim _Inter As Integer = 0

#End Region
#Region "Metodos Privados"
    Private Sub _prIniciarTodo()
        Me.Text = "ZONAS"
        _prInicarMapa()
        '' L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        _prMaxLength()
        _prCargarComboLibreria(cbCiudad, 3, 1)
        _prCargarComboLibreria(cbProvincia, 3, 2)
        _prCargarComboLibreria(cbZona, 3, 3)
        _prAsignarPermisos()
        _PMIniciarTodo()
        _prCargarZonasTable()
        grZona.Focus()
        Dim blah As New Bitmap(New Bitmap(My.Resources.zona), 20, 20)
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico

    End Sub
    Public Sub _prCargarZonasTable()
        grZona.BoundMode = Janus.Data.BoundMode.Bound
        grZona.DataSource = JGrM_Buscador.DataSource
        grZona.RetrieveStructure()

        With grZona.RootTable.Columns("zanumi")
            .Caption = "CODIGO"
            .Width = 60
            .Visible = True
            .FormatString = "0"
        End With
        With grZona.RootTable.Columns("zacity")
            .Caption = "Código"
            .Width = 150
            .Visible = False
            .FormatString = "0"
        End With
        With grZona.RootTable.Columns("ciudad")
            .Caption = "CIUDAD"
            .Width = 150
            .Visible = True

        End With
        With grZona.RootTable.Columns("zaprovi")
            .Caption = "Código"
            .Width = 150
            .Visible = False
            .FormatString = "0"
        End With
        With grZona.RootTable.Columns("provincia")
            .Caption = "PROVINCIA"
            .Width = 100
            .Visible = True

        End With

        With grZona.RootTable.Columns("zazona")
            .Caption = "Código"
            .Width = 90
            .Visible = False
            .FormatString = "0"
        End With
        With grZona.RootTable.Columns("zona")
            .Caption = "ZONA"
            .Width = 300
            .Visible = True

        End With
        With grZona.RootTable.Columns("zacolor")
            .Caption = "COLOR"
            .Width = 150
            .Visible = False
        End With
        With grZona.RootTable.Columns("zafact")
            .Width = 150
            .Visible = False
        End With
        With grZona.RootTable.Columns("zahact")
            .Width = 150
            .Visible = False
        End With
        With grZona.RootTable.Columns("zauact")
            .Width = 150
            .Visible = False
        End With

        With grZona
            .GroupByBoxVisible = False
            '.FilterRowFormatStyle.BackColor = Color.Blue
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            '.FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            'Diseño de la tabla
            .VisualStyle = VisualStyle.Office2007
            .SelectionMode = SelectionMode.SingleSelection
            .AlternatingColors = True
        End With
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
    Public Sub _prMaxLength()
        cbCiudad.MaxLength = 40
        cbProvincia.MaxLength = 40
        cbZona.MaxLength = 40
    End Sub

    Private Sub P_IniciarMap()
        Gmc_Cliente.DragButton = MouseButtons.Left
        Gmc_Cliente.CanDragMap = True
        Gmc_Cliente.MapProvider = GMapProviders.GoogleMap
        If (_latitud <> 0 And _longitud <> 0) Then

            Gmc_Cliente.Position = New PointLatLng(_latitud, _longitud)
        Else

            _Overlay.Markers.Clear()
            Gmc_Cliente.Position = New PointLatLng(-17.4120653, -66.1825898)
        End If

        Gmc_Cliente.MinZoom = 0
        Gmc_Cliente.MaxZoom = 24
        Gmc_Cliente.Zoom = 15.5
        Gmc_Cliente.AutoScroll = True

        GMapProvider.Language = LanguageType.Spanish
    End Sub
    Public Sub _prInicarMapa()
        _Punto = 0
        '_ListPuntos = New List(Of PointLatLng)
        _Overlay = New GMapOverlay("points")
        Gmc_Cliente.Overlays.Add(_Overlay)
        P_IniciarMap()
    End Sub
    Public Sub _prSeleccionarCombo(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        If (CType(mCombo.DataSource, DataTable).Rows.Count > 0) Then
            mCombo.SelectedIndex = 0
        Else
            mCombo.SelectedIndex = -1
        End If
    End Sub

#End Region

#Region "METODOS SOBRECARGADOS"

    Public Overrides Sub _PMOHabilitar()

        cbCiudad.ReadOnly = False
        cbProvincia.ReadOnly = False
        cbZona.ReadOnly = False
        BtAdicionar.Visible = True
        btLimpiar.Visible = True
        grZona.Enabled = False

    End Sub

    Public Overrides Sub _PMOInhabilitar()
        tbCodigoOriginal.ReadOnly = True
        cbCiudad.ReadOnly = True
        cbProvincia.ReadOnly = True
        cbZona.ReadOnly = True
        tbColor.ReadOnly = True
        BtAdicionar.Visible = False
        btLimpiar.Visible = False

        _prCargarZonasTable()
        grZona.Enabled = True
        If (grZona.RowCount > 0) Then
            grZona.Row = 0
        End If
        grZona.Focus()
    End Sub

    Public Overrides Sub _PMOLimpiar()
        tbCodigoOriginal.Clear()

        _Overlay.Markers.Clear()
        _latitud = 0
        _longitud = 0
        _TablePoint = L_fnListarPuntosPorZona(-1)
        _Overlay.Polygons.Clear()
        _AddPoint = False
        tbColor.Clear()
        _ListPuntos.Clear()
        _prSeleccionarCombo(cbCiudad)
        _prSeleccionarCombo(cbProvincia)
        _prSeleccionarCombo(cbZona)
        _prCargarZonasTable()

    End Sub

    Public Overrides Sub _PMOLimpiarErrores()
        MEP.Clear()
        cbCiudad.BackColor = Color.White
        cbProvincia.BackColor = Color.White
        cbZona.BackColor = Color.White
    End Sub

    Public Overrides Function _PMOGrabarRegistro() As Boolean


        Dim res As Boolean = L_fnGrabarZona(tbCodigoOriginal.Text, cbCiudad.Value, cbProvincia.Value, cbZona.Value, tbColor.Text, _TablePoint)


        If res Then

            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "Código de Zona ".ToUpper + tbCodigoOriginal.Text + " Grabado con Exito.".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter
                                      )

            ''cbProvincia.Focus()
        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "La Zona no pudo ser insertado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

        End If
        Return res

    End Function

    Public Overrides Function _PMOModificarRegistro() As Boolean
        Dim res As Boolean
        res = L_fnModificaZona(tbCodigoOriginal.Text, cbCiudad.Value, cbProvincia.Value, cbZona.Value, tbColor.Text, _TablePoint)
        If res Then
            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "Código de Zona ".ToUpper + tbCodigoOriginal.Text + " modificado con Exito.".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter)

        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "La Zona no pudo ser modificado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

        End If
        _PMFiltrar()
        _prCargarZonasTable()
        _PMInhabilitar()
        _PMPrimerRegistro()
        Return res

    End Function

    Public Sub _prSalir()
        If btnGrabar.Enabled = True Then
            _PMInhabilitar()
            _PMPrimerRegistro()

        Else
            _tab.Close()
        End If
    End Sub

    Public Function _fnAccesible() As Boolean
        Return cbZona.ReadOnly = False
    End Function

    Public Overrides Sub _PMOEliminarRegistro()

        Dim ef = New Efecto


        ef.tipo = 2
        ef.Context = "¿esta seguro de eliminar la Zona?".ToUpper
        ef.Header = "mensaje principal".ToUpper
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then
            Dim mensajeError As String = ""
            Dim res As Boolean = L_fnEliminarZona(tbCodigoOriginal.Text, mensajeError)
            If res Then


                Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)

                ToastNotification.Show(Me, "Código de Zona ".ToUpper + tbCodigoOriginal.Text + " eliminado con Exito.".ToUpper,
                                          img, 2000,
                                          eToastGlowColor.Green,
                                          eToastPosition.TopCenter)

                _PMFiltrar()
                _prCargarZonasTable()

            Else
                Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                ToastNotification.Show(Me, mensajeError, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            End If
        End If


    End Sub
    Public Overrides Function _PMOValidarCampos() As Boolean
        Dim _ok As Boolean = True
        MEP.Clear()

        If cbCiudad.SelectedIndex < 0 Then
            cbCiudad.BackColor = Color.Red
            MEP.SetError(cbCiudad, "Seleccione una ciudad!".ToUpper)
            _ok = False
        Else
            cbCiudad.BackColor = Color.White
            MEP.SetError(cbCiudad, "")
        End If
        If cbProvincia.SelectedIndex < 0 Then
            cbProvincia.BackColor = Color.Red
            MEP.SetError(cbProvincia, "Seleccione una provincia!".ToUpper)
            _ok = False
        Else
            cbProvincia.BackColor = Color.White
            MEP.SetError(cbProvincia, "")
        End If
        If cbZona.SelectedIndex < 0 Then
            cbZona.BackColor = Color.Red
            MEP.SetError(cbZona, "Seleccione una zona!".ToUpper)
            _ok = False
        Else
            cbZona.BackColor = Color.White
            MEP.SetError(cbZona, "")
        End If


        MHighlighterFocus.UpdateHighlights()
        Return _ok
    End Function

    Public Overrides Function _PMOGetTablaBuscador() As DataTable
        Dim dtBuscador As DataTable = L_fnGeneralZona()
        Return dtBuscador
    End Function

    Public Overrides Function _PMOGetListEstructuraBuscador() As List(Of Modelo.Celda)
        Dim listEstCeldas As New List(Of Modelo.Celda)
        '      a.zanumi,a.zacity,ciudad .ycdes3 as ciudad,a.zaprovi ,provincia.ycdes3 as provincia 
        ',a.zazona ,zona .ycdes3 as zona,a.zacolor ,a.zafact ,a.zahact ,a.zauact

        listEstCeldas.Add(New Modelo.Celda("zanumi", True, "Código".ToUpper, 80))
        listEstCeldas.Add(New Modelo.Celda("zacity", False))
        listEstCeldas.Add(New Modelo.Celda("ciudad", True, "Ciudad".ToUpper, 250))

        listEstCeldas.Add(New Modelo.Celda("zaprovi", False))
        listEstCeldas.Add(New Modelo.Celda("provincia", True, "Provincia".ToUpper, 250))

        listEstCeldas.Add(New Modelo.Celda("zazona", False))
        listEstCeldas.Add(New Modelo.Celda("zona", True, "Zona".ToUpper, 250))

        listEstCeldas.Add(New Modelo.Celda("zacolor", False))
        listEstCeldas.Add(New Modelo.Celda("zafact", False))
        listEstCeldas.Add(New Modelo.Celda("zahact", False))
        listEstCeldas.Add(New Modelo.Celda("zauact", False))
        Return listEstCeldas
    End Function

    Public Overrides Sub _PMOMostrarRegistro(_N As Integer)
        JGrM_Buscador.Row = _MPos
        '      a.zanumi,a.zacity,ciudad .ycdes3 as ciudad,a.zaprovi ,provincia.ycdes3 as provincia 
        ',a.zazona ,zona .ycdes3 as zona,a.zacolor ,a.zafact ,a.zahact ,a.zauact
        Dim dt As DataTable = CType(JGrM_Buscador.DataSource, DataTable)
        Try
            tbCodigoOriginal.Text = JGrM_Buscador.GetValue("zanumi").ToString
        Catch ex As Exception
            Exit Sub
        End Try
        With JGrM_Buscador
            tbCodigoOriginal.Text = .GetValue("zanumi").ToString
            cbCiudad.Value = .GetValue("zacity")
            cbProvincia.Value = .GetValue("zaprovi")
            cbZona.Value = .GetValue("zazona")
            tbColor.Text = .GetValue("zacolor")
            lbFecha.Text = CType(.GetValue("zafact"), Date).ToString("dd/MM/yyyy")
            lbHora.Text = .GetValue("zahact").ToString
            lbUsuario.Text = .GetValue("zauact").ToString

        End With
        _TablePoint = L_fnListarPuntosPorZona(tbCodigoOriginal.Text)
        _prdibujarPoligonoRegistrado(tbColor.Text)
        '_dibujarUbicacion(JGrM_Buscador.GetValue("yddesc").ToString, JGrM_Buscador.GetValue("yddctnum").ToString)
        LblPaginacion.Text = Str(_MPos + 1) + "/" + JGrM_Buscador.RowCount.ToString

    End Sub

    Public Sub _prdibujarPoligonoRegistrado(_color As String)
        Try

            Dim lati, longi As Double
            _ListPuntos.Clear()
            For i = 0 To _TablePoint.Rows.Count - 1
                lati = _TablePoint.Rows(i).Item("zblat")
                longi = _TablePoint.Rows(i).Item("zblongi")
                Dim plg As PointLatLng = New PointLatLng(lati, longi)
                _ListPuntos.Add(plg)


            Next
            If _TablePoint.Rows.Count > 0 Then
                'posicionar en la zona

                Dim latiZona, longiZona As Double
                latiZona = _TablePoint.Rows(0).Item("zblat")
                longiZona = _TablePoint.Rows(0).Item("zblongi")
                Gmc_Cliente.Position = New PointLatLng(latiZona, longiZona)

                ColorDialog1.Color = ColorTranslator.FromHtml(tbColor.Text)

                tbColor.BackColor = ColorDialog1.Color
                tbColor.Refresh()
                Dim polygon As New GMapPolygon(_ListPuntos, "mypolygon")
                'agregar color
                polygon.Fill = New SolidBrush(Color.FromArgb(50, ColorDialog1.Color))
                polygon.Stroke = New Pen(Color.FromArgb(211, 28, 112), 1.5)
                _Overlay.Polygons.Clear()
                _Overlay.Polygons.Add(polygon)
            Else
                _Overlay.Polygons.Clear()
            End If
        Catch ex As Exception

        End Try


    End Sub
    Private Sub P_AgregarPunto(pointLatLng As PointLatLng, _nombre As String, _ci As String)
        If (Not IsNothing(_Overlay)) Then
            'añadir puntos
            'Dim markersOverlay As New GMapOverlay("markers")
            Dim marker As New GMarkerGoogle(pointLatLng, My.Resources.markerIcono)
            'añadir tooltip
            Dim mode As MarkerTooltipMode = MarkerTooltipMode.OnMouseOver
            marker.ToolTip = New GMapBaloonToolTip(marker)
            marker.ToolTipMode = mode
            Dim ToolTipBackColor As New SolidBrush(Color.Blue)
            marker.ToolTip.Fill = ToolTipBackColor
            marker.ToolTip.Foreground = Brushes.White
            'If (Not _nombre.ToString = String.Empty) Then
            '    marker.ToolTipText = "CLIENTE: " + _nombre & vbNewLine & " CI:" + _ci
            'End If
            _Overlay.Markers.Add(marker)
            'mapa.Overlays.Add(markersOverlay)
            Gmc_Cliente.Position = pointLatLng
        End If
    End Sub
    Public Overrides Sub _PMOHabilitarFocus()

        'With MHighlighterFocus
        '    .SetHighlightOnFocus(tbCodigo, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(tbCodProd, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(tbStockMinimo, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(tbCodBarra, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)

        '    .SetHighlightOnFocus(tbDescPro, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(tbDescCort, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(cbgrupo1, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(cbgrupo2, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(cbgrupo3, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(cbgrupo4, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(cbUMed, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(swEstado, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(cbUniVenta, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(cbUnidMaxima, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        '    .SetHighlightOnFocus(tbConversion, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)


        'End With
    End Sub

#End Region



    Private Sub F1_Zonas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GroupPanelBuscador.Visible = False
        MPanelSup.Dock = DockStyle.Fill
        _prIniciarTodo()

    End Sub

    Private Sub Gmc_Cliente_MouseClick(sender As Object, e As MouseEventArgs) Handles Gmc_Cliente.MouseClick
        If _AddPoint = True Then
            Dim gm As GMapControl = CType(sender, GMapControl)
            Dim hj As MouseEventArgs = CType(e, MouseEventArgs)
            Dim plg As PointLatLng = gm.FromLocalToLatLng(hj.X, hj.Y)

            _AgregarPunto(plg)
            _ListPuntos.Add(plg)
            _TablePoint.Rows.Add(1, 1, plg.Lat, plg.Lng, 0)
        End If

    End Sub
    Private Sub _AgregarPunto(pointLatLng As PointLatLng, Optional etiqueta As String = "")
        Dim marker As GMarkerGoogle
        ''Dim markersOverlay As New GMapOverlay("markers")
        If (IsNothing(_ListPuntos)) Then

            marker = New GMarkerGoogle(pointLatLng, My.Resources.markerf)
        Else
            If (_ListPuntos.Count < 2 And _ListPuntos.Count >= 0) Then
                marker = New GMarkerGoogle(pointLatLng, My.Resources.markerf)
            End If
            If (_ListPuntos.Count >= 2) Then
                Dim markerA As New GMarkerGoogle(_ListPuntos(_ListPuntos.Count - 1), My.Resources.mark)
                Dim mode As MarkerTooltipMode = MarkerTooltipMode.OnMouseOver

                markerA.ToolTip = New GMapBaloonToolTip(markerA)

                _Overlay.Markers.Add(markerA)
            End If
            marker = New GMarkerGoogle(pointLatLng, My.Resources.markerf)
        End If



        'añadir tooltip
        If etiqueta <> "" Then
            Dim mode As MarkerTooltipMode = MarkerTooltipMode.OnMouseOver

            marker.ToolTip = New GMapBaloonToolTip(marker)

            marker.ToolTipMode = mode
            Dim ToolTipBackColor As New SolidBrush(Color.Red)
            marker.ToolTip.Fill = ToolTipBackColor
            marker.ToolTipText = etiqueta
        End If
        _Overlay.Markers.Add(marker)
        'mapa.Overlays.Add(markersOverlay)
    End Sub

    Public Function _fnEsNuevo()
        Return tbCodigoOriginal.Text = String.Empty And cbCiudad.ReadOnly = False
    End Function
    Public Function _fnModificar()
        Return tbCodigoOriginal.Text <> String.Empty And cbCiudad.ReadOnly = False
    End Function
    Private Sub ButtonX1_Click(sender As Object, e As EventArgs) Handles btLimpiar.Click
        If (_fnEsNuevo()) Then
            _ListPuntos.Clear()
            _Overlay.Markers.Clear()
            _TablePoint.Clear()
        Else
            If (_fnModificar()) Then
                _ListPuntos.Clear()
                _Overlay.Markers.Clear()
                _prCambiarEstadoPuntos(1, -1)
                _prCambiarEstadoPuntos(0, -2)
            End If
        End If

    End Sub
    Public Sub _prCambiarEstadoPuntos(estado As String, valor As Integer)
        Dim result As DataRow() = _TablePoint.Select("estado=" + estado)
        For i As Integer = 0 To result.Length - 1 Step 1
            Dim rowIndex As Integer = _TablePoint.Rows.IndexOf(result(i))
            _TablePoint.Rows(rowIndex).Item("estado") = valor
        Next
    End Sub

    Private Sub BtAdicionar_Click(sender As Object, e As EventArgs) Handles BtAdicionar.Click
        If (_fnEsNuevo()) Then
            If (_AddPoint = False) Then
                _AddPoint = True
                _ListPuntos.Clear()
                _Overlay.Markers.Clear()
                BtAdicionar.Text = "Finalizar Marcado"
            Else
                _AddPoint = False
                BtAdicionar.Text = "Marcar Puntos"
                _DibujarPoligono()
            End If
        Else
            If (_AddPoint = False) Then
                _AddPoint = True
                _ListPuntos.Clear()
                _Overlay.Markers.Clear()
                _prCambiarEstadoPuntos(1, -1)
                _prCambiarEstadoPuntos(0, -2)
                BtAdicionar.Text = "Finalizar Marcado"
            Else
                _AddPoint = False
                BtAdicionar.Text = "Marcar Puntos"
                _DibujarPoligono()
            End If
        End If

    End Sub
    Public Sub _DibujarPoligono()
        Dim polygon As New GMapPolygon(_ListPuntos, "mypolygon")
        'agregar color
        If ColorDialog1.ShowDialog() = DialogResult.OK Then
            polygon.Fill = New SolidBrush(Color.FromArgb(50, ColorDialog1.Color))
            Dim hexa As String = String.Format("#{0:X2}{1:X2}{2:X2}", ColorDialog1.Color.R, ColorDialog1.Color.G, ColorDialog1.Color.B)
            tbColor.Text = hexa
            tbColor.BackColor = ColorDialog1.Color
        Else
            polygon.Fill = New SolidBrush(Color.FromArgb(50, Color.Blue))
            Dim hexa As String = String.Format("#{0:X2}{1:X2}{2:X2}", ColorDialog1.Color.R, ColorDialog1.Color.G, ColorDialog1.Color.B)
            tbColor.Text = hexa
            tbColor.BackColor = ColorDialog1.Color
        End If

        'polygon.Fill = New SolidBrush(Color.FromArgb(50, Color.Black))
        polygon.Stroke = New Pen(Color.FromArgb(211, 28, 112), 1.5)
        _Overlay.Polygons.Clear()
        _Overlay.Polygons.Add(polygon)
        'añadir tooltip a poligono

        ''mapa.Overlays.Add(polyOverlay)

        _Overlay.Markers.Clear()

    End Sub

    Private Sub ButtonX3_Click(sender As Object, e As EventArgs) Handles ButtonX3.Click
        If (Gmc_Cliente.Zoom <= Gmc_Cliente.MaxZoom) Then
            Gmc_Cliente.Zoom = Gmc_Cliente.Zoom + 1
        End If
    End Sub

    Private Sub ButtonX4_Click(sender As Object, e As EventArgs) Handles ButtonX4.Click
        If (Gmc_Cliente.Zoom >= Gmc_Cliente.MinZoom) Then
            Gmc_Cliente.Zoom = Gmc_Cliente.Zoom - 1
        End If
    End Sub

    Private Sub grZona_TabIndexChanged(sender As Object, e As EventArgs) Handles grZona.TabIndexChanged
        Dim i As Integer = 0
    End Sub

    Private Sub grZona_SelectionChanged(sender As Object, e As EventArgs) Handles grZona.SelectionChanged
        Try
            If (Not IsDBNull(grZona)) Then
                If (grZona.Row >= 0) Then
                    JGrM_Buscador.Row = grZona.Row
                End If
            End If
        Catch ex As Exception

        End Try



    End Sub

    Private Sub cbCiudad_ValueChanged(sender As Object, e As EventArgs) Handles cbCiudad.ValueChanged
        If cbCiudad.SelectedIndex < 0 And cbCiudad.Text <> String.Empty Then
            btCiudad.Visible = True
        Else
            btCiudad.Visible = False
        End If
    End Sub

    Private Sub cbProvincia_ValueChanged(sender As Object, e As EventArgs) Handles cbProvincia.ValueChanged
        If cbProvincia.SelectedIndex < 0 And cbProvincia.Text <> String.Empty Then
            btProvincia.Visible = True
        Else
            btProvincia.Visible = False
        End If
    End Sub

    Private Sub cbZona_ValueChanged(sender As Object, e As EventArgs) Handles cbZona.ValueChanged
        If cbZona.SelectedIndex < 0 And cbZona.Text <> String.Empty Then
            btZona.Visible = True
        Else
            btZona.Visible = False
        End If
    End Sub

    Private Sub btCiudad_Click(sender As Object, e As EventArgs) Handles btCiudad.Click
        Dim numi As String = ""

        If L_prLibreriaGrabar(numi, "3", "1", cbCiudad.Text, "") Then
            _prCargarComboLibreria(cbCiudad, "3", "1")
            cbCiudad.SelectedIndex = CType(cbCiudad.DataSource, DataTable).Rows.Count - 1
        End If
    End Sub

    Private Sub btProvincia_Click(sender As Object, e As EventArgs) Handles btProvincia.Click
        Dim numi As String = ""
        If L_prLibreriaGrabar(numi, "3", "2", cbProvincia.Text, "") Then
            _prCargarComboLibreria(cbProvincia, "3", "2")
            cbProvincia.SelectedIndex = CType(cbProvincia.DataSource, DataTable).Rows.Count - 1
        End If
    End Sub

    Private Sub btZona_Click(sender As Object, e As EventArgs) Handles btZona.Click
        Dim numi As String = ""
        If L_prLibreriaGrabar(numi, "3", "3", cbZona.Text, "") Then
            _prCargarComboLibreria(cbZona, "3", "3")
            cbZona.SelectedIndex = CType(cbZona.DataSource, DataTable).Rows.Count - 1
        End If
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        If btnGrabar.Enabled = True Then
            _PMInhabilitar()
            _PMPrimerRegistro()

        Else
            '  Public _modulo As SideNavItem
            '_modulo.Select()
            '_tab.Close()
            Me.Close()
        End If
    End Sub

    Private Sub JGrM_Buscador_SelectionChanged(sender As Object, e As EventArgs) Handles JGrM_Buscador.SelectionChanged
        Try
            If (Not IsDBNull(JGrM_Buscador)) Then
                If (JGrM_Buscador.Row >= 0) Then
                    grZona.Row = JGrM_Buscador.Row
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click

    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click

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