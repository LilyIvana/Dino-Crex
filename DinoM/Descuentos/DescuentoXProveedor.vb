Imports Logica.AccesoLogica
Imports Janus.Windows.GridEX
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Public Class DescuentoXProveedor

    Dim _inter As Integer = 0
#Region "Variables Globales"

    Dim _Pos As Integer
    Dim _Nuevo As Boolean
    Dim _Dsencabezado As DataSet

    Dim _BindingSource As BindingSource
    Dim _Modificar As Boolean

    Dim modif As Boolean = True

    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem

    Dim dtPreciosDesc As New DataTable

#End Region
    Private Sub DescuentoXProveedor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _PIniciarTodo()
    End Sub
    Private Sub _PIniciarTodo()

        'L_prAbrirConexion()
        Me.Text = " P R E C I O S  -  D E S C U E N T O S POR PROVEEDOR "
        'Me.WindowState = FormWindowState.Maximized

        _prCargarProductos()

        btnGrabar.Visible = False
        btnNuevo.Visible = True

        tbDesde.IsInputReadOnly = True
        tbHasta.IsInputReadOnly = True
        tbPrecio.IsInputReadOnly = True

    End Sub
    Private Sub _prCargarProductos()
        Dim dt As New DataTable
        dt = L_fnListarCategoriaProveedorDescuento()
        grProducto.DataSource = dt
        grProducto.RetrieveStructure()
        grProducto.AlternatingColors = True
        With grProducto.RootTable.Columns("ProveedorID")
            .Width = 100
            .Caption = "Id"
            .WordWrap = True
            .MaxLines = 3
            .Visible = True
        End With

        With grProducto.RootTable.Columns("Descipcion")
            .Caption = "Descipcion"
            .Width = 250
            .WordWrap = True
            .MaxLines = 3
            .Visible = True
        End With
        With grProducto.RootTable.Columns("ESTADO")
            .Caption = "Estado"
            .Width = 250
            .WordWrap = True
            .MaxLines = 3
            .Visible = True
        End With

        With grProducto
            .GroupByBoxVisible = False
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .VisualStyle = VisualStyle.Office2007
        End With
        If (dt.Rows.Count > 0) Then
            grProducto.Row = 0
        End If
    End Sub

    Private Sub _prCargarDescuentos(ProveedorId As Integer)
        Dim dt As New DataTable
        dt = L_fnListarDescuentosXProveedor(ProveedorId)
        grdetalle.DataSource = dt
        grdetalle.RetrieveStructure()
        grdetalle.AlternatingColors = True

        With grdetalle.RootTable.Columns("id")
            .Width = 100
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("ProveedorId")
            .Width = 100
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("Proveedor")
            .Width = 100
            .Visible = False
        End With

        With grdetalle.RootTable.Columns("MontoInicial")
            .Caption = "Desde"
            .Width = 100
            .Visible = True
            .FormatString = "0"
        End With


        With grdetalle.RootTable.Columns("MontoFinal")
            .Caption = "Hasta"
            .Width = 100
            .Visible = True
            .FormatString = "0"
        End With

        With grdetalle.RootTable.Columns("DescuentoPorcentaje")
            .Caption = "Porcentaje Desc."
            .Width = 100
            .Visible = True
            .FormatString = "0.00"
        End With
        With grdetalle.RootTable.Columns("FechaFin")
            .Width = 90
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("Estado")
            .Width = 90
            .Visible = False
        End With
        With grdetalle
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
        End With



    End Sub

    Private Sub grProducto_SelectionChanged(sender As Object, e As EventArgs) Handles grProducto.SelectionChanged
        If (grProducto.Row >= 0) Then

            lbProducto.Text = grProducto.GetValue("Descipcion")
            _prCargarDescuentos(grProducto.GetValue("ProveedorID"))

        End If
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        tbDesde.IsInputReadOnly = False
        tbHasta.IsInputReadOnly = False
        tbPrecio.IsInputReadOnly = False


        tbDesde.Value = 0
        tbHasta.Value = 0
        tbPrecio.Value = 0
        tbDesde.Focus()

        btnNuevo.Visible = False
        btnGrabar.Visible = True

    End Sub

    Function validarDescuento(ByRef posicion As Integer) As Boolean

        Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)

        For i As Integer = 0 To dt.Rows.Count - 1 Step 1

            Dim estadoRegistro As Integer = dt.Rows(i).Item("estado")
            If (estadoRegistro >= 0) Then
                If (tbDesde.Value >= dt.Rows(i).Item("MontoInicial") And tbDesde.Value <= dt.Rows(i).Item("MontoFinal")) Then
                    posicion = i
                    Return True
                End If
                If (tbHasta.Value >= dt.Rows(i).Item("MontoInicial") And tbHasta.Value <= dt.Rows(i).Item("MontoFinal")) Then
                    posicion = i
                    Return True
                End If
            End If

        Next
        Return False



    End Function
    Public Function _ValidarCampos() As Boolean
        If (grProducto.Row < 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor debe dar click a en la Categoria de Proveedor".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            Return False
        End If
        If (tbDesde.Value > tbHasta.Value) Then
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "La cantidad Hasta es mayor al Desde".ToUpper, img, 3000, eToastGlowColor.Red, eToastPosition.BottomCenter)

            Return False
        End If

        If tbDesde.Value.ToString = String.Empty Or tbHasta.Value.ToString = String.Empty Or tbPrecio.Value.ToString = String.Empty Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Los campos no pueden estar vacios por favor coloque datos mayores a 0".ToUpper, img, 4000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            Return False
        Else
            If (tbDesde.Value <= 0 Or tbHasta.Value <= 0 Or tbPrecio.Value <= 0) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "Las campos no pueden ser 0 por favor coloque datos mayores a 0".ToUpper, img, 4000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                Return False
            End If
        End If
        Dim posicion As Integer = -1
        If (validarDescuento(posicion)) Then
            Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)

            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "Ya existe un Descuento Programado con los datos a Insertar, Desde = " + Str(dt.Rows(posicion).Item("MontoInicial")) + "  Hasta " + Str(dt.Rows(posicion).Item("MontoFinal")), img, 4500, eToastGlowColor.Red, eToastPosition.BottomCenter)
            Return False
        End If

        Return True
    End Function

    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        Dim numi As String = ""
        If (_ValidarCampos()) Then
            'Grabar
            Dim estadoDescripcion As String = "Habilitado"
            Dim estado As Integer = 1
            'Es descuento especial?
            If grProducto.GetValue("ProveedorID") = 1000 Then
                estadoDescripcion = "Desc. Esp."
                estado = 2
            End If
            Dim res As Boolean = L_fnGrabarCategoriaProveedorDescuentos(numi, grProducto.GetValue("ProveedorID"), tbDesde.Value, tbHasta.Value, tbPrecio.Value, estadoDescripcion, estado)

            If (res) Then
                ToastNotification.Show(Me, "Descuento por Categoria de Proveedor Grabado con éxito.".ToUpper,
                                   My.Resources.GRABACION_EXITOSA,
                                   3000,
                                   eToastGlowColor.Green,
                                   eToastPosition.TopCenter)




                btnNuevo.Visible = True
                btnGrabar.Visible = False

                tbDesde.Value = 0
                tbHasta.Value = 0
                tbPrecio.Value = 0

                _prCargarDescuentos(grProducto.GetValue("ProveedorID"))

            Else
                ToastNotification.Show(Me, "No se pudo grabar los descuentos.".ToUpper,
                                   My.Resources.WARNING,
                                   3000,
                                   eToastGlowColor.Red,
                                   eToastPosition.TopCenter)
            End If
        End If
    End Sub

    Private Sub ButtonX1_Click(sender As Object, e As EventArgs) Handles ButtonX1.Click
        Me.Close()

    End Sub


    Private Sub EliminarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EliminarToolStripMenuItem.Click
        If (grdetalle.Row >= 0) Then
            L_fnEliminarCategoriaProveedorDescuento(grdetalle.GetValue("id"))

            ToastNotification.Show(Me, "Descuento Eliminado Correctamente.".ToUpper,
                                My.Resources.GRABACION_EXITOSA,
                                3000,
                                eToastGlowColor.Green,
                                eToastPosition.TopCenter)

            _prCargarDescuentos(grProducto.GetValue("ProveedorID"))

        End If
    End Sub
End Class
