Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.SuperGrid
Imports System.Text
Imports DevComponents.Editors

Imports System.Windows.Forms.DataVisualization.Charting
Public Class F0_RotacionProductos
    Dim _Inter As Integer = 0
#Region "VARIABLES GLOBALES"
    Dim _ListMeses As List(Of String())
    Dim _ruta As String = gs_CarpetaRaiz + "\Imagenes"
    Dim _ListPunto As List(Of Double)
    Dim _Rand As New Random()
    Public _nameButton As String
    Public _tab As SuperTabItem
#End Region
    Public Sub _prIniciarTodo()
        L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)

        Me.Text = "REPORTE GRAFICOS VENTAS"

        'Me.WindowState = FormWindowState.Maximized
        _IniciarComponentes()

    End Sub
    Public Sub _IniciarComponentes()
        _prCargarComboLibreriaSucursal(cbAlmacen)
        _prCargarComboLibreriaMeses(cbMeses)
        _prCargarComboLibreriaAno(cbAno)
        tbcantMeses.Value = 1
        tbCantProducto.Value = 2
    End Sub

    Function _prCargarAnos() As DataTable
        Dim dt As DataTable = New DataTable()
        Dim ano As Integer = Date.Now.Year - 1
        Dim numicol As New DataColumn("numi")
        numicol.DataType = GetType(Integer)
        Dim mescol As New DataColumn("ano")
        mescol.DataType = GetType(String)
        dt.Columns.Add(numicol)
        dt.Columns.Add(mescol)

        dt.Rows.Add(ano, "" + Str(ano).Trim)
        dt.Rows.Add(ano + 1, "" + Str(ano + 1).Trim)

        Return dt
    End Function
    Function _prCargarMeses() As DataTable
        Dim dt As DataTable = New DataTable()

        Dim numicol As New DataColumn("numi")
        numicol.DataType = GetType(Integer)
        Dim mescol As New DataColumn("mes")
        mescol.DataType = GetType(String)
        dt.Columns.Add(numicol)
        dt.Columns.Add(mescol)

        dt.Rows.Add(1, "ENERO")
        dt.Rows.Add(2, "FEBRERO")
        dt.Rows.Add(3, "MARZO")
        dt.Rows.Add(4, "ABRIL")
        dt.Rows.Add(5, "MAYO")
        dt.Rows.Add(6, "JUNIO")
        dt.Rows.Add(7, "JULIO")
        dt.Rows.Add(8, "AGOSTO")
        dt.Rows.Add(9, "SEPTIEMBRE")
        dt.Rows.Add(10, "OCTUBRE")
        dt.Rows.Add(11, "NOVIEMBRE")
        dt.Rows.Add(12, "DICIEMBRE")
        Return dt
    End Function
    Private Sub _prCargarComboLibreriaMeses(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = _prCargarMeses()
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("numi").Width = 60
            .DropDownList.Columns("numi").Caption = "COD"
            .DropDownList.Columns.Add("mes").Width = 160
            .DropDownList.Columns("mes").Caption = "MES"
            .DropDownList.Columns("numi").Visible = False
            .ValueMember = "numi"
            .DisplayMember = "mes"
            .DataSource = dt
            .Refresh()
        End With

        If (CType(cbMeses.DataSource, DataTable).Rows.Count > 0) Then
            cbMeses.Value = Month(Now.Date)

        End If
    End Sub
    Private Sub _prCargarComboLibreriaAno(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = _prCargarAnos()
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("numi").Width = 60
            .DropDownList.Columns("numi").Caption = "COD"
            .DropDownList.Columns.Add("ano").Width = 120
            .DropDownList.Columns("ano").Caption = "MES"
            .ValueMember = "numi"
            .DropDownList.Columns("numi").Visible = False
            .DisplayMember = "ano"
            .DataSource = dt
            .Refresh()
        End With
        If (CType(cbAno.DataSource, DataTable).Rows.Count > 0) Then
            cbAno.SelectedIndex = 0
        End If
    End Sub
    Private Sub _prCargarComboLibreriaSucursal(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnListarSucursales()
        dt.Rows.Add(50, "TODOS")
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("aanumi").Width = 60
            .DropDownList.Columns("aanumi").Caption = "COD"
            .DropDownList.Columns.Add("aabdes").Width = 500
            .DropDownList.Columns("aabdes").Caption = "SUCURSAL"
            .ValueMember = "aanumi"
            .DisplayMember = "aabdes"
            .DataSource = dt
            .Refresh()
        End With
        If (CType(cbAlmacen.DataSource, DataTable).Rows.Count > 0) Then
            cbAlmacen.Value = 50
        End If
    End Sub
    Private Sub F0_RotacionProductos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()
    End Sub
    Public Function P_Validar() As Boolean
        If (cbAlmacen.SelectedIndex < 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "SELECCIONE UNA SUCURSAL".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Orange,
                                      eToastPosition.TopCenter
                                      )
            cbAlmacen.Focus()
            Return False
        End If
        If (cbMeses.SelectedIndex < 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "SELECCIONE UN MES".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Orange,
                                      eToastPosition.TopCenter
                                      )
            cbMeses.Focus()
            Return False
        End If
        If (cbAno.SelectedIndex < 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "SELECCIONE UN Año".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Orange,
                                      eToastPosition.TopCenter
                                      )
            cbAno.Focus()
            Return False
        End If
        If (tbcantMeses.Value <= 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Inserte una cantidad de meses valida".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Orange,
                                      eToastPosition.TopCenter
                                      )
            tbcantMeses.Focus()
            Return False
        End If
        If (tbCantProducto.Value <= 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Inserte una cantidad de productos valida".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Orange,
                                      eToastPosition.TopCenter
                                      )
            tbCantProducto.Focus()
            Return False
        End If
        Return True
    End Function


    Private Sub P_LlenarListaMeses()
        _ListMeses = New List(Of String())
        Dim _NroMes As Integer = (cbMeses.Value - CInt(tbcantMeses.Value)) + 1
        Dim _MesInicio As Integer = 0
        Dim _MesUltimo As Integer = cbMeses.Value
        Dim ii As Integer = 1
        If (_NroMes >= 0) Then
            ii = _NroMes
        End If
        If (_NroMes < 0) Then
            Dim i As Integer = _NroMes
            While (i < 0)
                Select Case i
                    Case -12
                        _ListMeses.Add({cbAno.Text, "ENERO", "1"})
                    Case -11
                        _ListMeses.Add({cbAno.Text, "FEBRERO", "2"})
                    Case -10
                        _ListMeses.Add({cbAno.Text, "MARZO", "3"})
                    Case -9
                        _ListMeses.Add({cbAno.Text, "ABRIL", "4"})
                    Case -8
                        _ListMeses.Add({cbAno.Text, "MAYO", "5"})
                    Case -7
                        _ListMeses.Add({cbAno.Text, "JUNIO", "6"})
                    Case -6
                        _ListMeses.Add({cbAno.Text, "JULIO", "7"})
                    Case -5
                        _ListMeses.Add({cbAno.Text, "AGOSTO", "8"})
                    Case -4
                        _ListMeses.Add({cbAno.Text, "SEPTIEMBRE", "9"})
                    Case -3
                        _ListMeses.Add({cbAno.Text, "OCTUBRE", "10"})
                    Case -2
                        _ListMeses.Add({cbAno.Text, "NOVIEMBRE", "11"})
                    Case -1
                        _ListMeses.Add({cbAno.Text, "DICIEMBRE", "12"})
                End Select
                i += 1
            End While
        End If
        While (ii <= _MesUltimo)
            If (ii = 0) Then
                _ListMeses.Add({CStr(CInt(cbAno.Text)), CType(cbMeses.DataSource, DataTable).Rows(ii).Item("mes"), CStr(ii)})
            Else
                _ListMeses.Add({CStr(CInt(cbAno.Text)), CType(cbMeses.DataSource, DataTable).Rows(ii - 1).Item("mes"), CStr(ii)})
            End If

            ii += 1
        End While
    End Sub

    Private Sub P_ArmarGrillaRotacion()
        Dgs_Rotacion.PrimaryGrid.Columns.Clear()
        'Alto de la Fila de Nombres de Columnas
        Dgs_Rotacion.PrimaryGrid.ColumnHeader.RowHeight = 25

        'Mostrar u Ocultar la Fila de Filtrado
        Dgs_Rotacion.PrimaryGrid.EnableColumnFiltering = True
        Dgs_Rotacion.PrimaryGrid.EnableFiltering = True
        Dgs_Rotacion.PrimaryGrid.EnableRowFiltering = True
        Dgs_Rotacion.PrimaryGrid.Filter.Visible = True

        'Para Mostrar u Ocultar la Columna de Cabesera de las Filas
        Dgs_Rotacion.PrimaryGrid.ShowRowHeaders = True

        'Para Mostrar el Indice de la Grilla
        Dgs_Rotacion.PrimaryGrid.RowHeaderIndexOffset = 1
        Dgs_Rotacion.PrimaryGrid.ShowRowGridIndex = True

        'Alto de las Filas
        Dgs_Rotacion.PrimaryGrid.DefaultRowHeight = 100

        'Alternar Colores de las Filas
        Dgs_Rotacion.PrimaryGrid.UseAlternateRowStyle = True

        Dim _col As GridColumn

        'Codigo Producto
        _col = New GridColumn
        _col.Name = "codP"
        _col.HeaderText = "cod".ToUpper
        _col.Visible = True
        _col.Width = 50
        Dgs_Rotacion.PrimaryGrid.Columns.Add(_col)

        'Producto
        _col = New GridColumn
        _col.Name = "prod"
        _col.HeaderText = "producto".ToUpper
        _col.Visible = True
        _col.Width = 250
        Dgs_Rotacion.PrimaryGrid.Columns.Add(_col)

        'Imagen
        _col = New GridColumn
        _col.Name = "imag"
        _col.HeaderText = "imagen".ToUpper
        _col.Visible = True
        _col.Width = 100
        Dgs_Rotacion.PrimaryGrid.Columns.Add(_col)

        'Creacion de Columnas de Meses
        Dim ii As Integer = 1
        For Each _LMes As String() In _ListMeses 'Pos  0 = Año; 1 = Mes; 2 = NroMes
            _col = New GridColumn
            _col.Name = "mes" + CStr(ii)
            _col.HeaderText = _LMes(1).ToUpper + " " + _LMes(0).ToUpper
            _col.Visible = True
            _col.Width = 100
            Dgs_Rotacion.PrimaryGrid.Columns.Add(_col)
            ii += 1
        Next

        'Chart Resumen lineas
        _col = New GridColumn
        _col.Name = "char1"
        _col.HeaderText = "".ToUpper
        _col.Visible = True
        _col.Width = 200
        Dgs_Rotacion.PrimaryGrid.Columns.Add(_col)

        'Chart Resumen Pie
        _col = New GridColumn
        _col.Name = "char2"
        _col.HeaderText = "".ToUpper
        _col.Visible = True
        _col.Width = 70
        Dgs_Rotacion.PrimaryGrid.Columns.Add(_col)

        'Suma de Todos los Meses
        _col = New GridColumn
        _col.Name = "total"
        _col.HeaderText = "total".ToUpper
        _col.Visible = True
        _col.Width = 100
        Dgs_Rotacion.PrimaryGrid.Columns.Add(_col)

    End Sub
    Private Function P_SumarP(j As Integer, ListCant As List(Of Integer())) As Integer
        Dim res As Integer = 0
        For Each item As Integer() In ListCant
            res = res + item(j)
        Next
        Return res
    End Function
    Function _fnObtenerTableMes()
        Dim dt As DataTable = New DataTable
        Dim mescol As New DataColumn("mes")
        mescol.DataType = GetType(Integer)
        dt.Columns.Add(mescol)

        For Each _LMes As String() In _ListMeses 'Pos  0 = Año; 1 = Mes; 2 = NroMes
            Dim mes As Integer = _LMes(2)
            dt.Rows.Add(mes)

        Next
        Return dt
    End Function
    Private Sub P_LlenarRotacion()

        Dim _Dt As New DataTable
        Dim _fdgs As GridRow
        Dim _cdgs As GridCell

        Dim _CodA As String = cbAlmacen.Value
        Dim _Mes As String = cbMeses.Value
        Dim _Anho As String = cbAno.Value
        Dim tableMeses As DataTable = _fnObtenerTableMes()
        _Dt = L_prMovimientoProductoGeneral(_CodA, _Mes, _Anho, tableMeses)

        Dim i As Integer = 1
        Dim _ListCant As New List(Of Integer())
        Dgs_Rotacion.PrimaryGrid.Rows.Clear()
        For Each _fdt As DataRow In _Dt.Rows
            _fdgs = New GridRow

            'Codigo Producto
            _cdgs = New GridCell
            _cdgs.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
            _cdgs.EditorType = GetType(GridTextBoxXEditControl)
            _cdgs.CellStyles.Default.Background.Color1 = Color.Empty
            _cdgs.ReadOnly = True
            _cdgs.Visible = True
            If (i < CInt(tbCantProducto.Value)) Then
                _cdgs.Value = _fdt.Item("CodProducto")
            Else
                _cdgs.Value = ""
            End If
            _fdgs.Cells.Add(_cdgs)

            'Producto
            _cdgs = New GridCell
            _cdgs.CellStyles.Default.Alignment = Style.Alignment.MiddleLeft
            _cdgs.EditorType = GetType(GridTextBoxXEditControl)
            _cdgs.CellStyles.Default.Background.Color1 = Color.Empty
            _cdgs.ReadOnly = True
            _cdgs.Visible = True
            If (i < CInt(tbCantProducto.Text)) Then
                _cdgs.Value = _fdt.Item("Producto")
            Else
                _cdgs.Value = "RESTO DE PRODUCTOS".ToUpper
            End If
            _fdgs.Cells.Add(_cdgs)

            'Imagen
            _cdgs = New GridCell
            _cdgs.CellStyles.Default.Alignment = Style.Alignment.MiddleCenter
            _cdgs.EditorType = GetType(GridTextBoxXEditControl)
            _cdgs.CellStyles.Default.Background.Color1 = Color.Empty
            _cdgs.ReadOnly = True
            _cdgs.Visible = True
            Dim _Img As String
            If (i < CInt(tbCantProducto.Value)) Then
                Dim table As DataTable = L_prMovimientoProductoObtenerImagenProducto(_fdt.Item("CodProducto"))
                _Img = IIf(table.Rows.Count <= 0, "", table.Rows(0).Item("yfimg"))
            Else
                _Img = ""
            End If
            If (Not _Img.Equals("")) Then
                _cdgs.CellStyles.Default.Alignment = Style.Alignment.MiddleCenter
                _cdgs.Value = ""
                Dim img As Bitmap
                Dim img2 As Bitmap
                If (IO.File.Exists(_ruta + "\Imagenes ProductoDino" + _Img)) Then
                    img = New Bitmap(_ruta + "\Imagenes ProductoDino" + _Img)
                    img2 = New Bitmap(img, 80, 80)
                    _cdgs.CellStyles.Default.Image = img2
                    _cdgs.CellStyles.Default.ImageAlignment = Style.Alignment.MiddleCenter
                End If
            Else
                _cdgs.Value = "S/I"
            End If
            _fdgs.Cells.Add(_cdgs)




            Dim _Cant1 As Integer
            ' = _fdt.Item("Cantidad")
            Dim _AcuP As Integer = 0
            Dim _CantT As Integer = 0
            Dim _CantMeses As Integer = 0
            Dim sb As New StringBuilder()
            Dim j As Integer = 0
            Dim _Vector(CInt(tbcantMeses.Value) - 1) As Integer

            For Each _LMes As String() In _ListMeses 'Pos  0 = Año; 1 = Mes; 2 = NroMes
                _Cant1 = 0
                _CantT = 0
                _cdgs = New GridCell
                _cdgs.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
                _cdgs.EditorType = GetType(GridTextBoxXEditControl)
                _cdgs.CellStyles.Default.Background.Color1 = Color.Empty
                _cdgs.ReadOnly = True
                _cdgs.Visible = True
                Dim tabla = L_prMovimientoProductoCantPorProducto(_fdt.Item("CodProducto"), _CodA, CStr(_LMes(2)), _LMes(0))
                If (IsDBNull(tabla) Or tabla.Rows.Count = 0) Then
                    _Cant1 = 0
                Else
                    For k As Integer = 0 To tabla.Rows.Count - 1 Step 1
                        _Cant1 = _Cant1 + tabla.Rows(k).Item("Cantidad")
                    Next

                End If

                '_Cant1 = CInt(L_GetProductoCantidad(_fdt.Item("CodProducto"), _CodA, CStr(_LMes(2) + 1), _LMes(0)))
                Dim tabla2 As DataTable = L_prMovimientoProductoCantPorProducto("", _CodA, CStr(_LMes(2)), _LMes(0))
                If (IsDBNull(tabla2) Or tabla2.Rows.Count = 0) Then
                    _CantT = 0
                Else

                    For k As Integer = 0 To tabla2.Rows.Count - 1 Step 1
                        _CantT = _CantT + IIf(IsDBNull(tabla2.Rows(k).Item("Cantidad")), 0, tabla2.Rows(k).Item("Cantidad"))
                    Next
                End If

                '_CantT = IIf(tabla2.Rows.Count <= 0, "0", tabla2.Rows(0).Item("Cantidad"))
                If (i < CInt(tbCantProducto.Value)) Then
                    _cdgs.Value = _Cant1
                    _Vector(j) = _Cant1
                Else
                    _Cant1 = _CantT - P_SumarP(j, _ListCant)
                    _cdgs.Value = _Cant1
                End If
                _CantMeses += _Cant1
                _fdgs.Cells.Add(_cdgs)

                sb.Append(_Cant1)
                sb.Append(" "c)
                j += 1
            Next
            _ListCant.Add(_Vector)

            'Chart Resumen Lineas
            _cdgs = New GridCell
            _cdgs.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
            _cdgs.EditorType = GetType(MyGridMicroChartEditControlLinea)
            _cdgs.CellStyles.Default.Background.Color1 = Color.Empty
            _cdgs.ReadOnly = True
            _cdgs.Visible = True
            _cdgs.Value = sb.ToString()
            _fdgs.Cells.Add(_cdgs)

            'Chart Resumen Pie
            _cdgs = New GridCell
            _cdgs.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
            _cdgs.EditorType = GetType(MyGridMicroChartEditControlPie)
            _cdgs.CellStyles.Default.Background.Color1 = Color.Empty
            _cdgs.ReadOnly = True
            _cdgs.Visible = True
            _cdgs.Value = sb.ToString()

            _fdgs.Cells.Add(_cdgs)

            'Suma de Todos los Meses
            _cdgs = New GridCell
            _cdgs.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
            _cdgs.EditorType = GetType(GridTextBoxXEditControl)
            _cdgs.CellStyles.Default.Background.Color1 = Color.Empty
            _cdgs.ReadOnly = True
            _cdgs.Visible = True
            _cdgs.Value = _CantMeses
            _fdgs.Cells.Add(_cdgs)
            Dim ob As Object = _fdgs.Cells("char2")
            Dgs_Rotacion.PrimaryGrid.Rows.Add(_fdgs)
            i += 1
            If (i = CInt(tbCantProducto.Text) + 1) Then
                Exit For
            End If


        Next

    End Sub
    Private Sub ButtonX1_Click(sender As Object, e As EventArgs) Handles ButtonX1.Click
        If (P_Validar()) Then
            P_LlenarListaMeses()
            P_ArmarGrillaRotacion()
            P_LlenarRotacion()
            P_Graficar()


            'Dim panel As GridPanel = Dgs_Rotacion.PrimaryGrid

            'Dim column As GridColumn = panel.Columns("char2")
            'Dim ad As Object = panel.GetElementAt(0, "char2")
        End If
    End Sub

    Private Sub P_Graficar()
        'CARGAR LOS DATOS
        Dim _CodA As String = cbAlmacen.Value
        Dim _Mes As String = cbMeses.Value
        Dim _Anho As String = cbAno.Value

        Dim _DtPie As DataTable = New DataTable()
        _DtPie.Columns.Add("prod")
        _DtPie.Columns.Add("cant")

        Dim _DtColumn As DataTable = New DataTable()
        _DtColumn.Columns.Add("prod")
        _DtColumn.Columns.Add("cant")

        Dim _i As Integer = 1
        Dim _CodP As String = ""
        Dim _Prod As String = ""
        Dim _AcuP As Integer = 0
        Dim _AcuT As Integer = 0
        Dim cantidad As Integer = 0
        For Each _fdgs As GridRow In Dgs_Rotacion.PrimaryGrid.Rows
            cantidad = cantidad + 1
            _CodP = _fdgs.Cells("codP").Value
            _Prod = _fdgs.Cells("prod").Value
            _AcuT = _AcuT + _fdgs.Cells("total").Value
            If (_i < CInt(tbCantProducto.Value)) Then
                Dim _CantPUni As Integer = 0

                For Each _fs As String() In _ListMeses

                    Dim tabla = L_prMovimientoProductoCantPorProducto(_CodP, _CodA, CStr(CInt(_fs(2))), _fs(0))
                    If (IsDBNull(tabla) Or tabla.Rows.Count = 0) Then

                    Else

                        For k As Integer = 0 To tabla.Rows.Count - 1 Step 1

                            _CantPUni = _CantPUni + tabla.Rows(k).Item("Cantidad")
                        Next
                    End If
                    '_CantPUni = _CantPUni + CInt(L_GetProductoCantidad(_CodP, _CodA, CStr(CInt(_fs(2)) + 1), _fs(0)))

                    Dim tabla2 = L_prMovimientoProductoCantPorProducto("", _CodA, CStr(CInt(_fs(2))), _fs(0))
                    If (IsDBNull(tabla) Or tabla.Rows.Count = 0) Then

                    Else
                        'For k As Integer = 0 To tabla2.Rows.Count - 1 Step 1

                        '    _AcuT = _AcuT + tabla2.Rows(k).Item("Cantidad")
                        'Next


                    End If
                    '_AcuT = _AcuT + CInt(L_GetProductoCantidad("", _CodA, CStr(CInt(_fs(2)) + 1), _fs(0)))
                Next
                _DtPie.Rows.Add({_Prod, _CantPUni})
                _AcuP += CInt(_CantPUni)
            End If
            _i += 1
        Next
        If (cantidad = tbCantProducto.Value) Then
            _DtPie.Rows.Add({"RESTO DE PRODUCTOS", CStr(_AcuT - _AcuP)})
        End If


        'GRAFICAR PIE
        Ch_Pie.Series.Clear()
        Dim _SPie As Series = New Series("Se_Pie")
        _SPie.IsValueShownAsLabel = True
        _SPie.ChartType = DataVisualization.Charting.SeriesChartType.Pie
        Dim _p As String
        Dim _c As String
        Dim _l As String
        Dim _Dpoint As DataPoint
        For Each _fdt2 As DataRow In _DtPie.Rows
            _p = _fdt2.Item("prod").ToString
            _c = _fdt2.Item("cant").ToString
            _l = CStr(Format((CDbl(_fdt2.Item("cant").ToString) / _AcuT) * 100, "0.00")) + " %"
            _Dpoint = New DataPoint()
            _Dpoint.SetValueXY(CInt(_c), CInt(_c))
            _Dpoint.IsValueShownAsLabel = True
            _Dpoint.Label = _l
            _Dpoint.LegendText = _p
            _Dpoint.ToolTip = _p
            '_Dpoint.Color = System.Drawing.Color.Green
            _SPie.Points.Add(_Dpoint)
        Next
        Ch_Pie.Series.Add(_SPie)

        'PONER TITULO PIE
        Ch_Pie.Titles.Clear()
        Dim _STitle As Title = New Title("Se_Pie")
        _STitle.Text = "R E S U M E N"
        _STitle.TextStyle = DataVisualization.Charting.TextStyle.Shadow
        Ch_Pie.Titles.Add(_STitle)


        'GRAFICAR BARRA
        Ch_Barra.Series.Clear()
        Dim _SBarra As Series
        For Each _fdgs As GridRow In Dgs_Rotacion.PrimaryGrid.Rows
            _SBarra = New Series()
            _SBarra.IsValueShownAsLabel = True
            _SBarra.ChartType = DataVisualization.Charting.SeriesChartType.Column
            _SBarra.LegendText = _fdgs.Item("prod").Value.ToString
            Dim j As Integer = 1
            For Each _ItemMes As String() In _ListMeses
                Dim _KeyMes As String = "mes" + CStr(j)
                _p = _ItemMes(1) + " " + _ItemMes(0)
                _c = _fdgs.Item(_KeyMes).Value.ToString

                _Dpoint = New DataPoint()
                _Dpoint.SetValueXY(_p, CInt(_c))
                '_Dpoint.IsValueShownAsLabel = True
                '_Dpoint.Label = _c
                '_Dpoint.LabelAngle = 90
                '_Dpoint.LegendText = _p
                _Dpoint.ToolTip = _p
                '_Dpoint.Color = System.Drawing.Color.Green
                _SBarra.Points.Add(_Dpoint)
                j += 1
            Next
            _SBarra.LabelAngle = 90
            Ch_Barra.Series.Add(_SBarra)

        Next


        Ch_Barra.Titles.Clear()
        _STitle = New Title("Se_Barra")
        _STitle.Text = "R E S U M E N  P O R  M E S"
        _STitle.TextStyle = DataVisualization.Charting.TextStyle.Shadow
        Ch_Barra.Titles.Add(_STitle)

    End Sub

#Region "MyGridMicroChartEditControlLinea"
    Private Class MyGridMicroChartEditControlLinea
        Inherits GridMicroChartEditControl
        Public Sub New()
            ChartType = eMicroChartType.Line
        End Sub
        Private Sub AddPuntos()
            Dim _Dt As New DataTable
            '_Dt = A la Consulta
            Dim _List As New List(Of Double)
            _List.Add(1.3)
            _List.Add(5.5)
            _List.Add(3.3)
            DataPoints = _List
        End Sub
    End Class

#End Region

#Region "MyGridMicroChartEditControlPie"
    Private Class MyGridMicroChartEditControlPie
        Inherits GridMicroChartEditControl
        Public Sub New()
            ChartType = eMicroChartType.Pie
        End Sub
        Private Sub AddPuntos()
            Dim _Dt As New DataTable
            '_Dt = A la Consulta
            Dim _List As New List(Of Double)
            _List.Add(1.3)
            _List.Add(5.5)
            _List.Add(3.3)
            DataPoints = _List
        End Sub
    End Class

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        _Inter = _Inter + 1
        If _Inter = 1 Then
            Me.WindowState = FormWindowState.Normal

        Else
            Me.Opacity = 100
            Timer1.Enabled = False
        End If

    End Sub

#End Region
End Class