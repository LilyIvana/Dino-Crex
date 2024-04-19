
Imports Logica.AccesoLogica
Imports Janus.Windows.GridEX
Imports DevComponents.DotNetBar
Imports System.IO
Imports DevComponents.DotNetBar.SuperGrid

Public Class F_NombreProforma
    Public Cliente As Boolean = False
    Public NombreProforma As String = ""
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
            MEP.SetError(tbNombre, "Ingrese nombre del cliente!".ToUpper)
            _ok = False
        Else
            tbNombre.BackColor = Color.White
            MEP.SetError(tbNombre, "")
        End If

        Return _ok

    End Function
    Private Sub F_ClienteNuevoServicio_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _priniciarTodo()
        _prHabilitarFocus()
    End Sub



    Private Sub btnguardar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        If (_prValidar()) Then
            NombreProforma = tbNombre.Text
            Me.Close()
        End If
    End Sub


    Private Sub HabilitarEnter(sender As Object, e As KeyPressEventArgs)
        If (e.KeyChar = ChrW(Keys.Enter)) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub tbNombre_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbNombre.KeyPress
        HabilitarEnter(sender, e)
    End Sub


End Class