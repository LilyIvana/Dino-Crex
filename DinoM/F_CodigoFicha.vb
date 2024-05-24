
Imports Logica.AccesoLogica
Imports Janus.Windows.GridEX
Imports DevComponents.DotNetBar
Imports System.IO
Imports DevComponents.DotNetBar.SuperGrid

Public Class F_CodigoFicha
    Public Codigo As String = ""
    Public Aceptar As Boolean = False

    Public Sub _priniciarTodo()
        tbCodigo.CharacterCasing = CharacterCasing.Upper
        _LengthTextBox()
    End Sub
    Public Sub _LengthTextBox()
        tbCodigo.MaxLength = 100
    End Sub

    Public Sub _prHabilitarFocus()
        With MHighlighterFocus
            .SetHighlightOnFocus(tbCodigo, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(btnAceptar, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)

        End With
    End Sub
    Public Function _prValidar() As Boolean
        Dim _ok As Boolean = True
        MEP.Clear()

        If tbCodigo.Text = String.Empty Then
            tbCodigo.BackColor = Color.Red
            MEP.SetError(tbCodigo, "Ingrese código de la ficha!".ToUpper)
            _ok = False
        Else
            tbCodigo.BackColor = Color.White
            MEP.SetError(tbCodigo, "")
        End If

        Return _ok

    End Function
    Private Sub F_ClienteNuevoServicio_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _priniciarTodo()
        _prHabilitarFocus()
    End Sub


    Private Sub btnguardar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        If (_prValidar()) Then
            Codigo = tbCodigo.Text
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

    Private Sub tbNombre_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbCodigo.KeyPress
        HabilitarEnter(sender, e)
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Aceptar = False
        Me.Close()
    End Sub
End Class