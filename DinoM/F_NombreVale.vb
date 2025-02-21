
Imports Logica.AccesoLogica
Imports Janus.Windows.GridEX
Imports DevComponents.DotNetBar
Imports System.IO
Imports DevComponents.DotNetBar.SuperGrid

Public Class F_NombreVale
    Public Cliente As Boolean = False
    Public NombreEmpresa As String = ""

    Public Aceptar As Boolean = False

    Public Sub _priniciarTodo()

        tbNombre.CharacterCasing = CharacterCasing.Upper
        _LengthTextBox()
    End Sub
    Public Sub _LengthTextBox()
        tbNombre.MaxLength = 100
    End Sub

    Public Sub _prHabilitarFocus()
        With MHighlighterFocus
            .SetHighlightOnFocus(tbNombre, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(btnAceptar, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        End With
    End Sub
    Public Function _prValidar() As Boolean
        Dim _ok As Boolean = True
        MEP.Clear()

        If tbNombre.Text = String.Empty Then
            tbNombre.BackColor = Color.Red
            MEP.SetError(tbNombre, "Ingrese nombre de la empresa!".ToUpper)
            _ok = False
        Else
            tbNombre.BackColor = Color.White
            MEP.SetError(tbNombre, "")
        End If

        If tbNrovale.Text = String.Empty Then
            tbNrovale.BackColor = Color.Red
            MEP.SetError(tbNrovale, "Ingrese el o los números de vales!".ToUpper)
            _ok = False
        Else
            tbNrovale.BackColor = Color.White
            MEP.SetError(tbNrovale, "")
        End If

        If tbCantVale.Text = String.Empty Or tbCantVale.Value = 0 Then
            tbCantVale.BackColor = Color.Red
            MEP.SetError(tbCantVale, "La cantidad de vales no puede estar vacío o ser 0!".ToUpper)
            _ok = False
        Else
            tbCantVale.BackColor = Color.White
            MEP.SetError(tbCantVale, "")
        End If

        If tbNombreCli.Text = String.Empty Then
            tbNombreCli.BackColor = Color.Red
            MEP.SetError(tbNombreCli, "Ingrese nombre del cliente!".ToUpper)
            _ok = False
        Else
            tbNombreCli.BackColor = Color.White
            MEP.SetError(tbNombreCli, "")
        End If

        If tbCICli.Text = String.Empty Then
            tbCICli.BackColor = Color.Red
            MEP.SetError(tbCICli, "Ingrese el Carnet de identidad del cliente!".ToUpper)
            _ok = False
        Else
            tbCICli.BackColor = Color.White
            MEP.SetError(tbCICli, "")
        End If

        If tbMontoVale.Text = String.Empty Or tbMontoVale.Value = 0 Then
            tbMontoVale.BackColor = Color.Red
            MEP.SetError(tbMontoVale, "El monto del vale no puede estar vacío o ser 0!".ToUpper)
            _ok = False
        Else
            tbMontoVale.BackColor = Color.White
            MEP.SetError(tbMontoVale, "")
        End If


        Return _ok

    End Function
    Private Sub F_ClienteNuevoServicio_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _priniciarTodo()
        _prHabilitarFocus()
    End Sub

    Private Sub btnguardar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        If (_prValidar()) Then
            NombreEmpresa = tbNombre.Text
            Aceptar = True
            Me.Close()
        End If
    End Sub

    Private Sub HabilitarEnter(sender As Object, e As KeyPressEventArgs)
        If (e.KeyChar = ChrW(Keys.Enter)) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub tbNombre_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbNombre.KeyPress, tbNrovale.KeyPress, tbNombreCli.KeyPress, tbMontoVale.KeyPress, tbCICli.KeyPress, tbCantVale.KeyPress
        HabilitarEnter(sender, e)
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Aceptar = False
        Me.Close()
    End Sub

    Private Sub tbMontoVale_ValueChanged(sender As Object, e As EventArgs) Handles tbMontoVale.ValueChanged
        If (tbTotalVenta.Value > tbMontoVale.Value) Then
            tbExcedente.Value = tbTotalVenta.Value - tbMontoVale.Value
            tbBeneficio.Value = 0
        ElseIf (tbTotalVenta.Value < tbMontoVale.Value) Then
            tbBeneficio.Value = (tbTotalVenta.Value - tbMontoVale.Value) * (-1)
            tbExcedente.Value = 0
        End If
    End Sub
End Class