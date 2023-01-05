Imports GMap.NET.WindowsForms
Imports GMap.NET.WindowsForms.ToolTips
Imports System.Drawing
Imports DevComponents.DotNetBar.Controls
Imports System.Threading
Imports System.Drawing.Text
Imports Logica.AccesoLogica
Imports Janus.Windows.GridEX
Imports DevComponents.DotNetBar
Imports System.IO
Imports DevComponents.DotNetBar.SuperGrid
Imports GMap.NET.MapProviders
Imports GMap.NET
Imports GMap.NET.WindowsForms.Markers
Imports System.Reflection
Imports System.Runtime.InteropServices
Public Class F0_PagosCreditoCompra

#Region "Variables Globales"
    Dim precio As DataTable
    Public _nameButton As String
    Public _tab As SuperTabItem
    Dim Bin As New MemoryStream
#End Region
#Region "METODOS PRIVADOS"

    Private Sub _IniciarTodo()
        'Dim img As New Bitmap(My.Resources.delete, 28, 28)
        'img.Save(Bin, Imaging.ImageFormat.Png)

        'L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        'Me.WindowState = FormWindowState.Maximized
        _prAsignarPermisos()
        Me.Text = "PAGOS DE COMPRAS A CREDITO"
        Dim blah As New Bitmap(New Bitmap(My.Resources.cobro), 20, 20)
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico
        _prCargarTablaCreditos()
        grfactura.Focus()
        PanelNavegacion.Visible = False
    End Sub
    Private Function _fnBytesArchivo(img As Bitmap, ancho As Integer, alto As Integer) As Object
        Bin = New MemoryStream()
        Dim img2 As New Bitmap(img, ancho, alto)
        img2.Save(Bin, System.Drawing.Imaging.ImageFormat.Png)
        Return Bin.ToArray()
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


    Private Sub _prCargarTablaCreditos()
        '       A.tcnumi, sucursal, NroDoc,
        '   proveedor.yddesc,
        '   A.tctc1numi, A.tcfdoc, totalfactura, pendiente
        ', as PagoAc, NumeroRecibo
        Dim dt As New DataTable
        dt = L_fnObtenerLasComprasACredito()
        grfactura.DataSource = dt
        grfactura.RetrieveStructure()
        grfactura.AlternatingColors = True
        With grfactura.RootTable.Columns("tcnumi")
            .Width = 100
            .Visible = False
        End With
        With grfactura.RootTable.Columns("sucursal")
            .Width = 120
            .Visible = True
            .Caption = "SUCURSAL"
        End With
        With grfactura.RootTable.Columns("tctc1numi")
            .Width = 100
            .Visible = False
        End With
       


        With grfactura.RootTable.Columns("NroDoc")
            .Caption = "NRO DOC"
            .Width = 90
            .Visible = True
        End With

        With grfactura.RootTable.Columns("yddesc")
            .Caption = "PROVEEDOR"
            .Width = 230
            .Visible = True
        End With

        With grfactura.RootTable.Columns("tcfdoc")
            .Caption = "FECHA"
            .Width = 110
            .FormatString = "dd/MM/yyyy"
            .Visible = True
        End With
        With grfactura.RootTable.Columns("totalfactura")
            .Caption = "TOTAL FACT"
            .Width = 120
            .MaxLength = 100
            .FormatString = "0.00"
            .Visible = True
        End With
        With grfactura.RootTable.Columns("pendiente")
            .Caption = "PENDIENTE PAGO"
            .Width = 140
            .FormatString = "0.00"
            .MaxLength = 10
            .Visible = True
        End With
        With grfactura.RootTable.Columns("PagoAc")
            .Caption = "PAGO A/C"
            .Width = 120
            .FormatString = "0.00"
            .MaxLength = 10
            .Visible = True
        End With
        With grfactura.RootTable.Columns("NumeroRecibo")
            .Caption = "NRO RECIBO"
            .Width = 150
            .MaxLength = 20
            .Visible = True
        End With
        With grfactura
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges

        End With


    End Sub


    Private Sub _prCargarTablaPagos(_numi As Integer)

        Dim dt As New DataTable
        dt = L_fnObtenerLosPagosComprasCreditos(_numi)
        _prCargarIconDelete(dt)
        grpagos.DataSource = dt
        grpagos.RetrieveStructure()
        grpagos.AlternatingColors = True
        'A.tdnumi, A.tdtc12numi, A.tdnrodoc, A.tdfechaPago, A.tdmonto, A.tdnrorecibo, A.tdfact, A.tdhact, img 
        With grpagos.RootTable.Columns("tdnumi")
            .Width = 100
            .Visible = False
        End With
        With grpagos.RootTable.Columns("tdtc12numi")
            .Width = 100
            .Visible = False
        End With
        With grpagos.RootTable.Columns("tdnrodoc")
            .Caption = "NRO DOC"
            .Width = 90
            .Visible = True
        End With
        With grpagos.RootTable.Columns("tdfechaPago")
            .Caption = "FECHA PAGO"
            .Width = 120
            .Visible = True
        End With
        With grpagos.RootTable.Columns("tdmonto")
            .Caption = "MONTO"
            .Width = 180
            .FormatString = "0.00"
            .AggregateFunction = AggregateFunction.Sum
            .Visible = True
        End With
        With grpagos.RootTable.Columns("tdnrorecibo")
            .Caption = "NRO RECIBO"
            .Width = 180
            .Visible = True
        End With
        With grpagos.RootTable.Columns("tdfact")
            .Width = 100
            .Visible = False
        End With
        With grpagos.RootTable.Columns("tdhact")
            .Width = 100
            .Visible = False
        End With
        With grpagos.RootTable.Columns("tduact")
            .Width = 100
            .Visible = False
        End With
        With grpagos.RootTable.Columns("img")
            .Width = 80
            .Caption = "ELIMINAR"
            .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
            .Visible = True
        End With

        With grpagos
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007

            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            'Diseño de la tabla
            .VisualStyle = VisualStyle.Office2007
            .AlternatingColors = True
            .RecordNavigator = True
            .RecordNavigatorText = "VENTAS CREDITOS"
            .RowHeaders = InheritableBoolean.True
            .TotalRow = InheritableBoolean.True
            .TotalRowFormatStyle.BackColor = Color.Gold
            .TotalRowPosition = TotalRowPosition.BottomFixed
        End With


    End Sub
    Public Sub _prCargarIconDelete(ByRef dt As DataTable)

        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
            dt.Rows(i).Item("img") = _fnBytesArchivo(My.Resources.delete, 28, 28)
        Next
    End Sub
#End Region

    Private Sub F0_PagosCredito_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _IniciarTodo()
    End Sub



    Private Sub grfactura_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grfactura.EditingCell

        'Habilitar solo las columnas de Precio, %, Monto y Observación
        If (e.Column.Index = grfactura.RootTable.Columns("PagoAc").Index Or e.Column.Index = grfactura.RootTable.Columns("NumeroRecibo").Index) Then
            e.Cancel = False
        Else
            e.Cancel = True
        End If

    End Sub

    Private Sub grfactura_SelectionChanged(sender As Object, e As EventArgs) Handles grfactura.SelectionChanged
        'a.tcnumi,sucursal,NroDoc,as factura,a.tctv1numi ,a.tcty4clie ,cliente,a.tcty4vend,vendedor,a.tcfdoc ,totalfactura, pendiente, PagoAc, NumeroRecibo
        If (grfactura.Row >= 0) Then
            tbnrodoc.Text = grfactura.GetValue("NroDoc")
            tbfecha.Value = grfactura.GetValue("tcfdoc")
            tbproveedor.Text = grfactura.GetValue("yddesc")

            tbalmacen.Text = grfactura.GetValue("sucursal")
            _prCargarTablaPagos(grfactura.GetValue("tcnumi"))
        End If
    End Sub

    Private Sub grfactura_CellValueChanged(sender As Object, e As ColumnActionEventArgs) Handles grfactura.CellValueChanged
        If (e.Column.Index = grfactura.RootTable.Columns("PagoAc").Index) Then
            If (Not IsNumeric(grfactura.GetValue("PagoAc")) Or grfactura.GetValue("PagoAc").ToString = String.Empty) Then
                'grfactura.SetValue("PagoAc", 0)
                'grdetalle.SetValue("tbptot", grdetalle.GetValue("cbpcost"))
            Else
                If (grfactura.GetValue("PagoAc") > 0) Then
                    Dim deuda As Double = grfactura.GetValue("pendiente")
                    If (grfactura.GetValue("PagoAc") > deuda) Then
                        grfactura.SetValue("PagoAc", deuda)
                    End If
                Else
                    grfactura.SetValue("PagoAc", 0)

                End If
            End If
        End If



    End Sub

    Private Sub grfactura_CellEdited(sender As Object, e As ColumnActionEventArgs) Handles grfactura.CellEdited
        If (e.Column.Index = grfactura.RootTable.Columns("PagoAc").Index) Then
            If (Not IsNumeric(grfactura.GetValue("PagoAc")) Or grfactura.GetValue("PagoAc").ToString = String.Empty) Then
                grfactura.SetValue("PagoAc", 0)
                'grdetalle.SetValue("tbptot", grdetalle.GetValue("cbpcost"))
            End If

        End If

    End Sub
    Sub _prInterpretarDatos(ByRef dt As DataTable)
        '        A.tdnumi, A.tdtv12numi, A.tdnrodoc, A.tdfechaPago, A.tdmonto
        ',a.tdnrorecibo ,a.tdfact ,a.tdhact ,a.tduact 

        'a.tcnumi,sucursal,NroDoc,as factura,a.tctv1numi ,a.tcty4clie ,cliente,a.tcty4vend,vendedor,a.tcfdoc ,totalfactura, pendiente, PagoAc, NumeroRecibo
        Dim dtcobro As DataTable = CType(grfactura.DataSource, DataTable)
        For i As Integer = 0 To dtcobro.Rows.Count - 1 Step 1
            Dim pago As Double = dtcobro.Rows(i).Item("PagoAc")
            If (pago > 0) Then
                dt.Rows.Add(0, dtcobro.Rows(i).Item("tcnumi"), dtcobro.Rows(i).Item("NroDoc"),
                            dtcobro.Rows(i).Item("tcfdoc"), pago, dtcobro.Rows(i).Item("NumeroRecibo"))
            End If
        Next
    End Sub
    Sub _prGrabarCobro()
        Dim dtCobro As DataTable = L_fnObtenerLosPagosComprasCreditos(-1)
        _prInterpretarDatos(dtCobro)
        If (dtCobro.Rows.Count > 0) Then

            Dim res As Boolean = L_fnGrabarPagosCompraCredito("", dtCobro)


            If res Then

                Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
                ToastNotification.Show(Me, "EL PAGO HA SIDO GRABADO CON EXITO".ToUpper,
                                          img, 3500,
                                          eToastGlowColor.Green,
                                          eToastPosition.TopCenter
                                          )
                Dim pos As Integer = grfactura.Row
                If (pos <= grfactura.RowCount - 1) Then
                    grfactura.Row = pos
                End If
                _prCargarTablaCreditos()



                grfactura.Focus()


            Else
                Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
                ToastNotification.Show(Me, "EL COBRO NO PUDO GRABARSE CON EXITO".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

            End If


        Else
            Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
            ''''''NO EXISTE DATOS PARA INSERTAR EN LOS COBROS 
            ToastNotification.Show(Me, "NO EXISTE NINGUN PAGO INSERTADO PARA PODER GRABAR !!             ", img, 4000, eToastGlowColor.Orange, eToastPosition.TopCenter)

        End If
    End Sub
    Private Sub BtAdicionar_Click(sender As Object, e As EventArgs) Handles BtAdicionar.Click
        _prGrabarCobro()
    End Sub

    Private Sub grpagos_MouseClick(sender As Object, e As MouseEventArgs) Handles grpagos.MouseClick
        If (grpagos.CurrentColumn.Index = grpagos.RootTable.Columns("img").Index) Then
            _prEliminarFila()
        End If
    End Sub
    Sub _prELiminarFila()
        Dim ef = New Efecto


        ef.tipo = 2
        ef.Context = "¿esta seguro de eliminar el pago registrado?".ToUpper
        ef.Header = "mensaje principal".ToUpper
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then
            Dim mensajeError As String = ""
            Dim dta As DataTable = CType(grpagos.DataSource, DataTable)
            Dim res As Boolean = L_fnEliminarPagoCompraCredito(grpagos.GetValue("tdnumi"), mensajeError)
            If res Then


                Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)

                ToastNotification.Show(Me, "Código de cobro fue eliminado con exito".ToUpper,
                                          img, 2000,
                                          eToastGlowColor.Green,
                                          eToastPosition.TopCenter)

                Dim pos As Integer = grfactura.Row
                _prCargarTablaCreditos()
                If (grfactura.RowCount > 0) Then
                    grfactura.Row = pos
                End If

            Else
                Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
                ''''''NO EXISTE DATOS PARA INSERTAR EN LOS COBROS 
                ToastNotification.Show(Me, "El pago no pudo ser eliminado !!             ", img, 4000, eToastGlowColor.Red, eToastPosition.TopCenter)
            End If
        End If
    End Sub
End Class