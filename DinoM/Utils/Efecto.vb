
Imports System.ComponentModel
Imports System.Text
Imports DevComponents.DotNetBar
Imports Janus.Windows.GridEX.EditControls
Public Class Efecto
    Public _archivo As String
    Public band As Boolean = False
    Public Header As String = ""
    Public tipo As Integer = 0
    Public Context As String = ""
    Public listEstCeldas As List(Of Modelo.Celda)
    Public dt As DataTable
    Public alto As Integer
    Public ancho As Integer
    Public Row As Janus.Windows.GridEX.GridEXRow
    Public SeleclCol As Integer = -1

    Public TotalBs As Double = 0
    Public TotalSus As Double = 0
    Public TotalTarjeta As Double = 0
    Public TotalVenta As Double
    Public NameProducto As String = ""
    Public TipoCambio As Double = 0

    Public Stock As Double = 0
    Public Cantidad As Double = 0
    Public Nit As String = ""
    Public RazonSocial As String = ""
    Public Tdoc As New MultiColumnCombo
    Public Email As String = ""
    Public IdNit As String
    Public nroTarjeta As String
    Public CExc As Integer

    Public Conversion As Double = 0
    Public CantidadPrevia As Double = 0

    Public DetalleServicio As String = ""
    Public PrecioServicio As Double = 0

    Public NombreServicio As String = ""


    Private Sub Efecto_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized

        Select Case tipo
            Case 1
                 _prMostrarMensaje()
            Case 2
                _prMostrarMensajeDelete()
            Case 3
                _prMostrarFormAyuda()
            Case 6
                _prMostrarAyudaMontoPago()
            Case 7
                _prMostrarAyudaVentaCantidad()
            Case 8
                _prMostrarAyudaDetalleServicio()
            Case 4
                _prLogin()
        End Select
    End Sub
    Public Sub _prLogin()
        Dim Frm As New Login
        Frm.ShowDialog()
        Me.Close()
    End Sub
    Sub _prMostrarFormAyuda()

        Dim frmAyuda As Modelo.ModeloAyuda
        frmAyuda = New Modelo.ModeloAyuda(alto, ancho, dt, Context.ToUpper, listEstCeldas)
        If (SeleclCol >= 0) Then
            frmAyuda.Columna = SeleclCol
            frmAyuda._prSeleccionar()

        End If
        frmAyuda.ShowDialog()
        If frmAyuda.seleccionado = True Then
            Row = frmAyuda.filaSelect
            band = True
            Me.Close()
        Else
            band = False
            Me.Close()
        End If

    End Sub

    Sub _prMostrarAyudaMontoPago()

        Dim frmAyuda As F1_MontoPagar
        frmAyuda = New F1_MontoPagar

        frmAyuda.TotalVenta = TotalVenta
        frmAyuda.Nit = Nit
        frmAyuda.RazonSocial = RazonSocial
        With frmAyuda.CbTipoDoc
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("codigoClasificador").Width = 70
            .DropDownList.Columns("codigoClasificador").Caption = "COD"
            .DropDownList.Columns.Add("descripcion").Width = 400
            .DropDownList.Columns("descripcion").Caption = "DESCRIPCION"
            .ValueMember = "codigoClasificador"
            .DisplayMember = "descripcion"
            .DataSource = Tdoc.DataSource
            .Refresh()
        End With

        frmAyuda.TbEmail.Text = Email

        frmAyuda.ShowDialog()
        If frmAyuda.Bandera = True Then

            TotalBs = frmAyuda.TotalBs
            TotalSus = frmAyuda.TotalSus
            TotalTarjeta = frmAyuda.TotalTarjeta
            Nit = frmAyuda.Nit
            RazonSocial = frmAyuda.RazonSocial
            TipoCambio = frmAyuda.TipoCambio
            Tdoc.Value = frmAyuda.CbTipoDoc.Value
            Email = frmAyuda.TbEmail.Text
            IdNit = frmAyuda.IdNit
            nroTarjeta = frmAyuda.tbNroTarjeta.Text
            CExc = frmAyuda.CExcep

            band = True
            Me.Close()
        Else
            band = False
            Me.Close()
        End If

    End Sub

    Sub _prMostrarAyudaDetalleServicio()

        Dim frmAyuda As F0_DetalleServicio
        frmAyuda = New F0_DetalleServicio


        frmAyuda.NombreServicio = NombreServicio

        frmAyuda.ShowDialog()
        If frmAyuda.Bandera = True Then

            DetalleServicio = frmAyuda.Detalle
            PrecioServicio = frmAyuda.Precio

            band = True
            Me.Close()
        Else
            band = False
            Me.Close()
        End If

    End Sub
    Sub _prMostrarAyudaVentaCantidad()

        Dim frmAyuda As F_Cantidad
        frmAyuda = New F_Cantidad

        frmAyuda.Stock = Stock
        frmAyuda.Cantidad = Cantidad
        frmAyuda.Producto = NameProducto
        frmAyuda.Conversion = Conversion
        frmAyuda.CantidadPrevia = CantidadPrevia
        frmAyuda.ShowDialog()
        If frmAyuda.Bandera = True Then

            Cantidad = frmAyuda.Cantidad

            band = True
            Me.Close()
        Else
            band = False
            Me.Close()
        End If

    End Sub
    Sub _prMostrarMensaje()
        Dim blah As Bitmap = My.Resources.cuestion
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())

        If (MessageBox.Show(Context, Header, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes) Then
            'Process.Start(_archivo)
            band = True
            Me.Close()
        Else
            band = False
            Me.Close()


        End If
    End Sub
    Sub _prMostrarMensajeDelete()

        Dim info As New TaskDialogInfo(Context, eTaskDialogIcon.Delete, "advertencia".ToUpper, Header, eTaskDialogButton.Yes Or eTaskDialogButton.Cancel, eTaskDialogBackgroundColor.Default)
        Dim result As eTaskDialogResult = TaskDialog.Show(info)
        If result = eTaskDialogResult.Yes Then
            Dim mensajeError As String = ""
            band = True
            Me.Close()

        Else
            band = False
            Me.Close()

        End If
    End Sub
End Class