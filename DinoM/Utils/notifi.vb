
Imports System.ComponentModel
Imports System.Text
Imports DevComponents.DotNetBar
Public Class notifi
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





    Private Sub notifi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized

        Select Case tipo
            Case 1
                _prMostrarMensaje1()
            Case 2
                _prMostrarMensajeDe()
            Case 3
                _prMostrarFormAyuda1()
            Case 4
                _prLogin()
        End Select
    End Sub
    Public Sub _prLogin()
        Dim Frm As New Login
        Frm.ShowDialog()
        Me.Close()
    End Sub
    Sub _prMostrarFormAyuda1()

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
    Sub _prMostrarMensaje1()
        Dim blah As Bitmap = My.Resources.cuestion
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())

        If (MessageBox.Show(Context, Header, MessageBoxButtons.OK, MessageBoxIcon.Information) = Windows.Forms.DialogResult.OK) Then
            'Process.Start(_archivo)
            band = True
            Me.Close()
        Else
            band = False
            Me.Close()


        End If
    End Sub
    Sub _prMostrarMensajeDe()

        Dim info As New TaskDialogInfo(Context, eTaskDialogIcon.Information, "Notificación".ToUpper, Header, eTaskDialogButton.Ok, eTaskDialogBackgroundColor.Default)

        Dim result As eTaskDialogResult = TaskDialog.Show(info)
        If result = eTaskDialogResult.Ok Then
            Dim mensajeError As String = ""
            band = True
            Me.Close()

        Else
            band = False
            Me.Close()

        End If
    End Sub
End Class