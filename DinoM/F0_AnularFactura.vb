Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar.SuperGrid
Imports System.IO
Imports DevComponents.DotNetBar
Imports Janus.Windows.GridEX

'importando librerias api conexion
Imports Newtonsoft.Json
Imports DinoM.RespMotivoAnulacion
Imports DinoM.AnulacionResp

Imports System.Xml

Public Class F0_AnularFactura
    Dim _Inter As Integer = 0

#Region "Variables Globales"

    Dim _DuracionSms As Integer = 5
    Dim NroFactura As String
    Dim NroAutorizacion As String
    Public _nameButton As String
    Public _tab As SuperTabItem
    Public Programa As String

    Public tokenSifac As String
#End Region

#Region "Eventos"

    Private Sub P_AnularFactura_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        P_Inicio()
    End Sub

#End Region

#Region "Metodos"

    Private Sub P_Inicio()
        'L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)

        'Me.WindowState = FormWindowState.Maximized
        Me.Text = "A N U L A R   F A C T U R A"
        MSuperTabControl.SelectedTabIndex = 0

        btnNuevo.Visible = False
        btnModificar.Visible = False
        btnEliminar.Visible = False
        btnGrabar.Visible = False
        btnSalir.Visible = False
        btnImprimir.Visible = False

        btnPrimero.Visible = False
        btnAnterior.Visible = False
        btnSiguiente.Visible = False
        btnUltimo.Visible = False

        LblPaginacion.Visible = False
        BubbleBarUsuario.Visible = False

        btnGrabar.Enabled = False
        btnNuevo.Visible = False
        btnEliminar.Visible = False
        btnImprimir.Visible = False
        btnSalir.Dock = DockStyle.Left

        P_ArmarGrilla()

        Programa = P_Principal.btVentAnularFactura.Text


        tokenSifac = F0_Venta2.ObtToken()
        CodMotivoAnulacion(tokenSifac)
    End Sub

#End Region

    Private Sub P_ArmarGrilla()

        DgdFactura.PrimaryGrid.Columns.Clear()
        'Alto de la Fila de Nombres de Columnas
        DgdFactura.PrimaryGrid.ColumnHeader.RowHeight = 25

        'Mostrar u Ocultar la Fila de Filtrado
        DgdFactura.PrimaryGrid.EnableColumnFiltering = True
        DgdFactura.PrimaryGrid.EnableFiltering = True
        DgdFactura.PrimaryGrid.EnableRowFiltering = True
        DgdFactura.PrimaryGrid.Filter.Visible = True

        'Para Mostrar u Ocultar la Columna de Cabesera de las Filas
        DgdFactura.PrimaryGrid.ShowRowHeaders = True

        'Para Mostrar el Indice de la Grilla
        DgdFactura.PrimaryGrid.RowHeaderIndexOffset = 1
        DgdFactura.PrimaryGrid.ShowRowGridIndex = True

        'Alto de las Filas
        DgdFactura.PrimaryGrid.DefaultRowHeight = 22

        'Alternar Colores de las Filas
        DgdFactura.PrimaryGrid.UseAlternateRowStyle = True

        'Para permitir o denegar el cambio de tamaño de la Filas
        DgdFactura.PrimaryGrid.AllowRowResize = False

        'Para que el Tamaño de las Columnas se pongan automaticamente
        'DgdFactura.PrimaryGrid.ColumnAutoSizeMode = ColumnAutoSizeMode.DisplayedCells

        DgdFactura.PrimaryGrid.SelectionGranularity = SelectionGranularity.RowWithCellHighlight

        Dim col As GridColumn

        'nfa
        col = New GridColumn("Archivo")
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 120
        DgdFactura.PrimaryGrid.Columns.Add(col)

        'Codigo
        col = New GridColumn("Codigo")
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = False
        col.Width = 70
        DgdFactura.PrimaryGrid.Columns.Add(col)

        'Nro. Factura
        col = New GridColumn("Nro Factura")
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 90
        DgdFactura.PrimaryGrid.Columns.Add(col)

        'Fecha
        col = New GridColumn("Fecha")
        col.EditorType = GetType(GridDateTimePickerEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 90
        DgdFactura.PrimaryGrid.Columns.Add(col)

        'Cod Cliente
        col = New GridColumn("Cod Cliente")
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = False
        col.Width = 80
        DgdFactura.PrimaryGrid.Columns.Add(col)

        'Nombre 1
        col = New GridColumn("Nombre 1")
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleLeft
        col.ReadOnly = True
        col.Visible = True
        col.Width = 150
        DgdFactura.PrimaryGrid.Columns.Add(col)

        'Nombre 2
        col = New GridColumn("Nombre 2")
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleLeft
        col.ReadOnly = True
        col.Visible = True
        col.Width = 150
        DgdFactura.PrimaryGrid.Columns.Add(col)

        'Nit
        col = New GridColumn("Nit")
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 100
        DgdFactura.PrimaryGrid.Columns.Add(col)

        'Subtotal
        col = New GridColumn("Subtotal")
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 80
        DgdFactura.PrimaryGrid.Columns.Add(col)

        'Descuento
        col = New GridColumn("Descuento")
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 80
        DgdFactura.PrimaryGrid.Columns.Add(col)

        'Total
        col = New GridColumn("Total")
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 80
        DgdFactura.PrimaryGrid.Columns.Add(col)

        'Cod Control
        col = New GridColumn("Cod Control")
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleLeft
        col.ReadOnly = True
        col.Visible = True
        col.Width = 100
        DgdFactura.PrimaryGrid.Columns.Add(col)

        'Fec Limite
        col = New GridColumn("Fec Limite")
        col.EditorType = GetType(GridDateTimePickerEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 100
        DgdFactura.PrimaryGrid.Columns.Add(col)

        'Estado
        col = New GridColumn("Estado")
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 60
        DgdFactura.PrimaryGrid.Columns.Add(col)

        P_LlenarDatosGrilla()

    End Sub

    Private Sub DgdFactura_RowActivated(sender As Object, e As GridRowActivatedEventArgs) Handles DgdFactura.RowActivated
        If (DgdFactura.PrimaryGrid.ActiveRow Is Nothing) Then
            ToastNotification.Show(Me, "Selecione un Fila de la gt_Tabla",
                                   My.Resources.INFORMATION, _DuracionSms * 1000,
                                   eToastGlowColor.Blue, eToastPosition.BottomLeft)
            Exit Sub
        End If
        If (DgdFactura.PrimaryGrid.ActiveRow.Index > -1) Then
            P_LlenarDatos(CType(e.NewActiveRow, GridRow))
        End If

    End Sub

    Private Sub P_LlenarDatos(fil As GridRow)
        If (fil.Cells.Count = 14) Then
            NroFactura = fil.Cells(0).Value.ToString.Split("_")(0)
            NroAutorizacion = fil.Cells(0).Value.ToString.Split("_")(1)
            Tb1Codigo.Text = fil.Cells(1).Value
            Tb2NroFactura.Text = fil.Cells(2).Value
            Tb2Fecha.Text = fil.Cells(3).Value
            Tb3CodCliente.Text = fil.Cells(4).Value
            Tb4Desc1.Text = fil.Cells(5).Value
            If (IsDBNull(fil.Cells(6).Value)) Then
                Tb5Desc2.Text = "0"
            Else
                Tb5Desc2.Text = fil.Cells(6).Value
            End If

            Tb6Nit.Text = fil.Cells(7).Value
            Tb7SubTotal.Text = fil.Cells(8).Value
            Tb8Descuento.Text = fil.Cells(9).Value
            Tb9Total.Text = fil.Cells(10).Value
            Tb10CodControl.Text = fil.Cells(11).Value
            Tb11FechaLim.Text = fil.Cells(12).Value
            Sb1Estado.Value = fil.Cells(13).Value

            If (Sb1Estado.Value) Then
                GroupPanelFactura.ColorTable = DevComponents.DotNetBar.Controls.ePanelColorTable.Green
                Sb1Estado.IsReadOnly = False
            Else
                GroupPanelFactura.ColorTable = DevComponents.DotNetBar.Controls.ePanelColorTable.Red
                Sb1Estado.IsReadOnly = True
            End If

            P_MostrarFactura(fil.Cells(0).Value.ToString, fil.Cells(1).Value.ToString)
        End If
    End Sub

    Private Sub P_MostrarFactura(Cod As String, Cod2 As String)
        'If (Not Directory.Exists(gs_CarpetaRaiz + "\Facturas")) Then
        '    ToastNotification.Show(Me, "La ruta de la carpeta " + gs_CarpetaRaiz + "\Facturas, No EXISTE!!!",
        '                           My.Resources.INFORMATION, _DuracionSms * 1000,
        '                           eToastGlowColor.Blue, eToastPosition.BottomLeft)
        'End If
        'Dim bool As Boolean
        'If (File.Exists(gs_CarpetaRaiz + "\Facturas\" + Cod + ".pdf")) Then
        '    ArFactura.LoadFile(gs_CarpetaRaiz + "\Facturas\" + Cod + ".pdf")
        '    ArFactura.setZoom(95)
        '    bool = True
        'ElseIf (File.Exists(gs_CarpetaRaiz + "\Facturas\" + Cod2 + ".pdf")) Then
        '    ArFactura.LoadFile(gs_CarpetaRaiz + "\Facturas\" + Cod2 + ".pdf")
        '    ArFactura.setZoom(95)
        '    bool = False
        'Else
        '    ToastNotification.Show(Me, "El archivo " + IIf(bool, Cod, Cod2) + ".pdf, No EXISTE!!!",
        '                           My.Resources.INFORMATION, _DuracionSms * 1000,
        '                           eToastGlowColor.Blue, eToastPosition.BottomLeft)
        'End If

    End Sub

    Private Sub Bt1Guardar_Click(sender As Object, e As EventArgs) Handles Bt1Guardar.Click
        If (Sb1Estado.Value) Then
            'If (MessageBox.Show("Esta seguro de poner VIGENTE la Factura " + Tb2NroFactura.Text + "?", "PREGUNTA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes) Then
            '    L_Modificar_Factura("fvanumi = " + Tb1Codigo.Text + " and fvanfac = " + NroFactura + " and fvaautoriz = " + NroAutorizacion, "", "", "", IIf(Sb1Estado.Value, "1", "0"))
            '    'P_ActStock()
            '    P_LlenarDatosGrilla()
            '    ToastNotification.Show(Me, "La Factura con Codigo " + Tb2NroFactura.Text + ", Se puso VIGENTE correctamente",
            '                           My.Resources.OK, _DuracionSms * 1000,
            '                           eToastGlowColor.Blue, eToastPosition.BottomLeft)
            'End If
            ToastNotification.Show(Me, "La Factura con Codigo " + Tb2NroFactura.Text + ", no se puede anular, debe cambiar el Estado a Anulada",
                                      My.Resources.WARNING, _DuracionSms * 1000,
                                      eToastGlowColor.Blue, eToastPosition.BottomLeft)
        Else

            Dim res1 As Boolean = L_fnVerificarPagos(Tb1Codigo.Text)
            If res1 Then
                Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)
                ToastNotification.Show(Me, "No se puede anular esta factura con código de venta:  ".ToUpper + Tb1Codigo.Text + ", porque tiene pagos realizados, por favor primero elimine los pagos correspondientes a esta venta".ToUpper,
                                                  img, 5000,
                                                  eToastGlowColor.Green,
                                                  eToastPosition.TopCenter)
                Exit Sub
            End If

            Dim res2 As Boolean = L_fnVerificarCierreCaja(Tb1Codigo.Text, "V")
            If res2 Then
                Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)

                ToastNotification.Show(Me, "No se puede anular esta factura con código de venta: ".ToUpper + Tb1Codigo.Text + ", porque ya se hizo cierre de caja, por favor primero elimine cierre de caja".ToUpper,
                                                  img, 5000,
                                                  eToastGlowColor.Green,
                                                  eToastPosition.TopCenter)
                Exit Sub
            End If

            Dim result As Boolean = L_fnVerificarSiSeContabilizoVenta(Tb1Codigo.Text)
            If result Then
                Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                ToastNotification.Show(Me, "La Factura y Venta no puede ser anulada porque ya fue contabilizada".ToUpper, img, 4500, eToastGlowColor.Red, eToastPosition.TopCenter)
                Exit Sub
            End If


            If (MessageBox.Show("Esta seguro de ANULAR la Factura " + Tb2NroFactura.Text + " y la Venta " + Tb1Codigo.Text + "?", "PREGUNTA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes) Then

                Dim Succes As Integer = AnularFactura(tokenSifac)
                If Succes = 200 Then
                    'Primero modifica factura correspondiente a la venta
                    L_Modificar_Factura("fvanumi = " + Tb1Codigo.Text + " and fvanfac = " + NroFactura + " and fvaautoriz = '" + NroAutorizacion + "'", "", "", "", IIf(Sb1Estado.Value, "1", "0"))
                    'Luego anula venta
                    Dim mensajeError As String = ""
                    Dim res As Boolean = L_fnEliminarVenta(Tb1Codigo.Text, mensajeError, Programa)

                    P_LlenarDatosGrilla()
                    ToastNotification.Show(Me, "La Factura: " + Tb2NroFactura.Text + " y Venta con código: " + Tb1Codigo.Text + " Se ANULARON correctamente",
                                       My.Resources.OK, _DuracionSms * 1000,
                                       eToastGlowColor.Blue, eToastPosition.BottomLeft)
                End If


            End If
        End If
    End Sub

    Private Sub Sb1Estado_ValueChanged(sender As Object, e As EventArgs) Handles Sb1Estado.ValueChanged
        If (Sb1Estado.Value) Then
            GroupPanelFactura.ColorTable = DevComponents.DotNetBar.Controls.ePanelColorTable.Green
        Else
            GroupPanelFactura.ColorTable = DevComponents.DotNetBar.Controls.ePanelColorTable.Red
        End If
    End Sub

    Private Sub P_LlenarDatosGrilla()
        DgdFactura.PrimaryGrid.Rows.Clear()

        DgdFactura.PrimaryGrid.DataSource = L_Obtener_Facturas()

        DgdFactura.PrimaryGrid.SetActiveRow(CType(DgdFactura.PrimaryGrid.ActiveRow, GridRow))
    End Sub

    'Private Sub P_ActStock()
    '    Dim _Ds As DataSet
    '    _Ds = L_ObtenerDetalleFactura(Tb1Codigo.Text)
    '    G_ActStock(_Ds.Tables(0), Not Sb1Estado.Value, 1)
    'End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click

    End Sub

    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click

    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click

    End Sub

    Private Sub btnPrimero_Click(sender As Object, e As EventArgs) Handles btnPrimero.Click

    End Sub

    Private Sub btnAnterior_Click(sender As Object, e As EventArgs) Handles btnAnterior.Click

    End Sub

    Private Sub btnSiguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click

    End Sub

    Private Sub btnUltimo_Click(sender As Object, e As EventArgs) Handles btnUltimo.Click

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

    Private Sub MostrarMensajeError(mensaje As String)
        ToastNotification.Show(Me,
                               mensaje.ToUpper,
                               My.Resources.WARNING,
                               5000,
                               eToastGlowColor.Red,
                               eToastPosition.TopCenter)

    End Sub

    Public Function CodMotivoAnulacion(tokenObtenido)

        Dim api = New DBApi()

        Dim url = "https://crex.sifac.nwc.com.bo/api/v2/motivo-anulacion"

        Dim headers = New List(Of Parametro) From {
            New Parametro("Authorization", "Bearer " + tokenObtenido),
            New Parametro("Content-Type", "Accept:application/json; charset=utf-8")
        }

        Dim parametros = New List(Of Parametro)

        Dim response = api.MGet(url, headers, parametros)

        Dim result = JsonConvert.DeserializeObject(Of Motivo)(response)

        Dim dt = result.data
        dt.RemoveAt(1)

        With CbMotivoA

            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("codigoClasificador").Width = 70
            .DropDownList.Columns("codigoClasificador").Caption = "COD"
            .DropDownList.Columns.Add("descripcion").Width = 500
            .DropDownList.Columns("descripcion").Caption = "DESCRIPCION"
            .ValueMember = "codigoClasificador"
            .DisplayMember = "descripcion"
            .DataSource = dt
            .Refresh()
        End With

        CbMotivoA.SelectedIndex = 1

        Return ""
    End Function

    Public Function AnularFactura(tokenObtenido)
        Try

            Dim api = New DBApi()

            Dim Aenvio = New AnulacionEnvio()
            Aenvio.cuf = NroAutorizacion
            Aenvio.codigoMotivo = CbMotivoA.Value

            Dim url = "https://crex.sifac.nwc.com.bo/api/v2/anular"

            Dim headers = New List(Of Parametro) From {
                New Parametro("Authorization", "Bearer " + tokenObtenido),
                New Parametro("Content-Type", "Accept:application/json; charset=utf-8")
            }

            Dim parametros = New List(Of Parametro)
            Dim response = api.Post(url, headers, parametros, Aenvio)

            Dim result = JsonConvert.DeserializeObject(Of RespuestaAnul)(response)
            Dim resultError = JsonConvert.DeserializeObject(Of error400)(response)

            Dim codigo = result.meta.code

            If codigo = 200 Then
                Dim details = result.data.details
                Dim notifi = New notifi

                notifi.tipo = 2
                notifi.Context = "SIFAC".ToUpper
                notifi.Header = "Proceso Exitoso - Código: " + codigo.ToString() & vbCrLf & " " & vbCrLf & details & vbCrLf & " " & vbCrLf & "Factura Anulada".ToUpper
                notifi.ShowDialog()

            ElseIf codigo = 400 Or codigo = 401 Or codigo = 404 Or codigo = 405 Or codigo = 422 Then
                Dim details = JsonConvert.SerializeObject(resultError.errors.details)
                Dim notifi = New notifi

                notifi.tipo = 2
                notifi.Context = "SIFAC".ToUpper
                notifi.Header = "Error de solicitud - Código: " + codigo.ToString() & vbCrLf & " " & vbCrLf & details & vbCrLf & " " & vbCrLf & "La factura no pudo anularse".ToUpper
                notifi.ShowDialog()

            ElseIf codigo = 406 Or codigo = 409 Or codigo = 500 Then

                Dim details = JsonConvert.SerializeObject(resultError.errors.details)
                Dim siat = JsonConvert.SerializeObject(resultError.errors.siat)
                Dim notifi = New notifi

                notifi.tipo = 2
                notifi.Context = "SIFAC".ToUpper
                notifi.Header = "Error de solicitud - Código: " + codigo.ToString() & vbCrLf & " " & vbCrLf & details & vbCrLf & siat & vbCrLf & " " & vbCrLf & "La factura no pudo anularse".ToUpper
                notifi.ShowDialog()

            End If

            Return codigo


        Catch ex As Exception
            MostrarMensajeError(ex.Message)
            Return 0
        End Try
    End Function

End Class