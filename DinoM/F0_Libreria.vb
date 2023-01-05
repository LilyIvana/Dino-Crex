
Imports Logica.AccesoLogica
Imports Janus.Windows.GridEX
Imports DevComponents.DotNetBar
Imports System.IO
Imports DevComponents.DotNetBar.SuperGrid
Imports GMap.NET.MapProviders
Imports GMap.NET
Imports GMap.NET.WindowsForms.Markers
Imports GMap.NET.WindowsForms
Imports GMap.NET.WindowsForms.ToolTips
Imports System.Drawing
Imports DevComponents.DotNetBar.Controls
Imports System.Threading
Imports System.Drawing.Text

Imports System.Reflection
Imports System.Runtime.InteropServices

Public Class F0_Libreria
    Dim _Inter As Integer = 0
#Region "Variables Globales"
    Dim precio As DataTable
    Dim Bin As New MemoryStream
    Public _nameButton As String
    Public _modulo As SideNavItem
    Public _tab As SuperTabItem
#End Region
#Region "MEtodos Privados"
    Private Sub _IniciarTodo()

        'L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        'Me.WindowState = FormWindowState.Maximized
        _prCargarComboLibreria(cbPrograma)
        _prAsignarPermisos()
        Me.Text = "LIBRERIAS"
        Dim blah As New Bitmap(New Bitmap(My.Resources.precio), 20, 20)
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico
        tblibreria.MaxLength = 40
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
    Public Sub _prCargarPrecios()

    End Sub

    Private Sub _prCargarComboLibreria(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnGeneralProgramas()

        'a.ylcod1 ,a.yldes1 
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("ylcod1").Width = 70
            .DropDownList.Columns("ylcod1").Caption = "COD"
            .DropDownList.Columns.Add("yldes1").Width = 200
            .DropDownList.Columns("yldes1").Caption = "DESCRIPCION"
            .ValueMember = "ylcod1"
            .DisplayMember = "yldes1"
            .DataSource = dt
            .Refresh()
        End With
        If (CType(cbPrograma.DataSource, DataTable).Rows.Count > 0) Then
            cbPrograma.SelectedIndex = 0
        End If
    End Sub
    Private Sub _prCargarComboLibreriaCategoriaPrograma(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        If (cbPrograma.SelectedIndex < 0) Then
            Return
        End If
        dt = L_fnGeneralProgramasCategorias(cbPrograma.Value)

        'a.ylcod2 ,a.yldes2 
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("ylcod2").Width = 70
            .DropDownList.Columns("ylcod2").Caption = "COD"
            .DropDownList.Columns.Add("yldes2").Width = 200
            .DropDownList.Columns("yldes2").Caption = "DESCRIPCION"
            .ValueMember = "ylcod2"
            .DisplayMember = "yldes2"
            .DataSource = dt
            .Refresh()
        End With
        If (CType(cbCategoria.DataSource, DataTable).Rows.Count > 0) Then
            cbCategoria.SelectedIndex = 0
        End If
        _prCargarTablaLibrerias()
    End Sub
    Private Sub _prInhabiliitar()
        lblibreria.Visible = False
        tblibreria.Visible = False
        btnAgregarlibreria.Visible = False

        btnModificar.Enabled = True
        btnGrabar.Enabled = False
        If (CType(grLibreria.DataSource, DataTable).Rows.Count > 0) Then
            grLibreria.RootTable.Columns("img").Visible = False
        End If
    End Sub
    Private Sub _prhabilitar()
        lblibreria.Visible = True
        tblibreria.Visible = True
        btnAgregarlibreria.Visible = True
        btnGrabar.Enabled = True
        tblibreria.Clear()
        'If (CType(grLibreria.DataSource, DataTable).Rows.Count > 0) Then
        '    grLibreria.RootTable.Columns("img").Visible = True
        'End If
    End Sub
    Private Function _fnBytesArchivo(img As Bitmap, ancho As Integer, alto As Integer) As Object
        Bin = New MemoryStream()
        Dim img2 As New Bitmap(img, ancho, alto)
        img2.Save(Bin, System.Drawing.Imaging.ImageFormat.Png)
        Return Bin.ToArray()
    End Function
    Public Sub _prCargarIconDelete(ByRef dt As DataTable)
        Bin = New MemoryStream()
        Dim img2 As New Bitmap(My.Resources.delete, 28, 28)
        img2.Save(Bin, System.Drawing.Imaging.ImageFormat.Png)

        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
            dt.Rows(i).Item("img") = Bin.ToArray()
        Next
    End Sub
    Private Sub _prCargarTablaLibrerias()
        Dim dt As New DataTable
        If (cbPrograma.SelectedIndex < 0 And cbCategoria.SelectedIndex < 0) Then
            Return

        End If
        dt = L_fnGeneralDetalleLibrerias(cbPrograma.Value, cbCategoria.Value)
        _prCargarIconDelete(dt)
        grLibreria.DataSource = dt
        grLibreria.RetrieveStructure()
        grLibreria.AlternatingColors = True

        'dar formato a las columnas
        'a.yccod1 ,a.yccod2 ,a.yccod3  ,a.ycdes3,1 as estado,Cast(null as image) as img 
        With grLibreria.RootTable.Columns("yccod1")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = False

        End With
        With grLibreria.RootTable.Columns("yccod2")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = False

        End With

        With grLibreria.RootTable.Columns("yccod3")
            .Width = 90
            .Visible = True
            .Caption = "CODIGO"
        End With

        With grLibreria.RootTable.Columns("ycdes3")
            .Caption = "DESCRIPCION"
            .Width = 200
            .Visible = True
            .MaxLength = 40

        End With
        With grLibreria.RootTable.Columns("estado")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With

        If (_fnAccesible()) Then
            With grLibreria.RootTable.Columns("img")
                .Width = 80
                .Caption = "Eliminar".ToUpper
                .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
                .Visible = True
            End With
        Else
            With grLibreria.RootTable.Columns("img")
                .Width = 80
                .Caption = "Eliminar".ToUpper
                .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
                .Visible = False
            End With
        End If



        With grLibreria
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            .RecordNavigator = True
            .RecordNavigatorText = "LIBRERIAS"
            .RowHeaders = InheritableBoolean.True
            'diseño de la grilla
            grLibreria.VisualStyle = VisualStyle.Office2007

        End With


    End Sub

    Public Function _fnAccesible()
        Return btnGrabar.Enabled = True
    End Function


    Public Function _prValidarCategoria() As Boolean
        Dim _ok As Boolean = True
        MEP.Clear()

        MHighlighterFocus.UpdateHighlights()
        Return _ok
    End Function

    Private Sub _prGrabarCategorias()
        'If (_prValidarCategoria()) Then



        '    Dim grabar As Boolean = L_fnGrabarCategorias("", letra, tbDescripcion.Text, IIf(swEstado.Value = True, 1, 0))
        '    If (grabar) Then
        '        Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
        '        ToastNotification.Show(Me, "categoria Grabado con Exito.".ToUpper,
        '                                  img, 2000,
        '                                  eToastGlowColor.Green,
        '                                  eToastPosition.TopCenter
        '                                  )
        '        _prLimpiar()

        '        _prCargarTablaCategorias()

        '        _prCargarDatosTablaPrecios()


        '    Else
        '        Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
        '        ToastNotification.Show(Me, "La categoria no pudo ser insertado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        '    End If
        'End If

    End Sub



#End Region


#Region "MEtodoso Formulario"
    Private Sub F0_Precios_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _IniciarTodo()
        _prInhabiliitar()
    End Sub
    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        _prhabilitar()
        btnModificar.Enabled = False

    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        If (_fnAccesible()) Then
            _prInhabiliitar()
        Else
            _modulo.Select()
            Me.Close()
        End If
    End Sub
    Private Sub btnAgregar_Click(sender As Object, e As EventArgs)
        _prGrabarCategorias()


    End Sub
    Private Sub TextBox_KeyDown(sender As Object, e As KeyEventArgs)
        Dim tb As TextBoxX = CType(sender, TextBoxX)
        If tb.Text = String.Empty Then

        Else
            tb.BackColor = Color.White
            MEP.SetError(tb, "")
        End If
    End Sub
    Private Sub grprecio_CellEdited(sender As Object, e As ColumnActionEventArgs) Handles grLibreria.CellEdited
        If (_fnAccesible()) Then
            grLibreria.SetValue("estado", 2)
        End If
    End Sub

    Private Sub grprecio_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grLibreria.EditingCell
        If (_fnAccesible()) Then
            'Deshabilitar la columna de Productos y solo habilitar la de los precios
            If (e.Column.Index = grLibreria.RootTable.Columns("ycdes3").Index) Then
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        Else
            e.Cancel = True
        End If
    End Sub
    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click

        Dim grabar As Boolean = L_fnGrabarLibreriasPrograma("", CType(grLibreria.DataSource, DataTable))
        If (grabar) Then
            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "categoria Grabado con Exito.".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter
                                      )


            _prInhabiliitar()

        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "La categoria no pudo ser insertado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        End If

    End Sub


#End Region


    Private Sub cbCategoria_ValueChanged(sender As Object, e As EventArgs) Handles cbCategoria.ValueChanged
        _prCargarTablaLibrerias()

    End Sub

    Private Sub cbPrograma_ValueChanged(sender As Object, e As EventArgs) Handles cbPrograma.ValueChanged
        _prCargarComboLibreriaCategoriaPrograma(cbCategoria)
    End Sub

    Private Sub btnAgregarlibreria_Click(sender As Object, e As EventArgs) Handles btnAgregarlibreria.Click
        If (tblibreria.Text.Length > 0) Then
            Dim Bin As New MemoryStream
            Dim img As New Bitmap(My.Resources.delete, 28, 28)
            img.Save(Bin, Imaging.ImageFormat.Png)
            'a.yccod1 ,a.yccod2 ,a.yccod3  ,a.ycdes3,1 as estado,Cast(null as image) as img   from TY0031 as a
            CType(grLibreria.DataSource, DataTable).Rows.Add(Str(cbPrograma.Value), Str(cbCategoria.Value), _fnSiguienteNumi() + 1, tblibreria.Text, 0, Bin.ToArray())
            tblibreria.Clear()
            tblibreria.Focus()

        End If
    End Sub
    Public Function _fnSiguienteNumi()
        Dim dt As DataTable = CType(grLibreria.DataSource, DataTable)
        Dim rows() As DataRow = dt.Select("yccod3=MAX(yccod3)")
        If (rows.Count > 0) Then
            Return rows(rows.Count - 1).Item("yccod3")
        End If
        Return 1
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
End Class