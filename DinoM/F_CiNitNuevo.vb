
Imports Logica.AccesoLogica
Imports Janus.Windows.GridEX
Imports DevComponents.DotNetBar
Imports System.IO
Imports DevComponents.DotNetBar.SuperGrid



Public Class F_CiNitNuevo

    Public Razonsocial As String = ""
    Public Nit As String = ""
    Public Correo As String = ""
    Public TipoDoc As String = ""
    Public Cliente As Boolean = False
    Public NuevoCli As Boolean = False




    Public Sub _priniciarTodo()
        tbRazonSocial.CharacterCasing = CharacterCasing.Upper
        tbNit.CharacterCasing = CharacterCasing.Upper

        _LengthTextBox()
    End Sub
    Public Sub _LengthTextBox()
        tbRazonSocial.MaxLength = 200
        tbNit.MaxLength = 20
        TbEmailN.MaxLength = 200
    End Sub

    Public Sub _prHabilitarFocus()
        With MHighlighterFocus
            .SetHighlightOnFocus(tbRazonSocial, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(tbNit, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(TbEmailN, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(btnguardar, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(btnsalir, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        End With
    End Sub
    Public Function _prValidar() As Boolean
        Dim _ok As Boolean = True
        MEP.Clear()


        If tbRazonSocial.Text = String.Empty Then
            tbRazonSocial.BackColor = Color.Red
            MEP.SetError(tbRazonSocial, "Ingrese Razón Social!".ToUpper)
            _ok = False
        Else
            tbRazonSocial.BackColor = Color.White
            MEP.SetError(tbRazonSocial, "")
        End If
        If tbNit.Text = String.Empty Then
            tbRazonSocial.BackColor = Color.Red
            MEP.SetError(tbNit, "Ingrese Nit!".ToUpper)
            _ok = False
        Else
            tbNit.BackColor = Color.White
            MEP.SetError(tbNit, "")
        End If
        'If TbEmailN.Text = String.Empty Then
        '    TbEmailN.BackColor = Color.Red
        '    MEP.SetError(TbEmailN, "Ingrese Correo Electrónico!".ToUpper)
        '    _ok = False
        'Else
        '    TbEmailN.BackColor = Color.White
        '    MEP.SetError(TbEmailN, "")

        '    MHighlighterFocus.UpdateHighlights()
        'End If

        If (CbTDoc.SelectedIndex < 0) Then
            CbTDoc.BackColor = Color.Red
            MEP.SetError(CbTDoc, "Por Favor Seleccione Tipo de Documento!!!".ToUpper)
            _ok = False
        Else
            CbTDoc.BackColor = Color.White
            MEP.SetError(CbTDoc, "")
        End If

        If TbEmailN.Text = String.Empty Then
            TbEmailN.Text = "cliente@crex.com.bo"
        End If

        Dim tokenSifac As String = F0_VentasSupermercado.ObtToken()
        Dim code = F0_VentasSupermercado.VerifConexion(tokenSifac)
        If (code = 200) Then
            If (CbTDoc.Value = 5) Then ''El tipo de Doc. es Nit
                Dim Succes As Integer = F0_VentasSupermercado.VerificarNit(tokenSifac, tbNit.Text)
                If Succes <> 200 Then
                    'If F0_VentasSupermercado.CodExcepcion = 1 Then
                    '    _ok = True
                    'Else
                    '    _ok = False
                    'End If
                    _ok = False
                End If
            End If
        End If

        Return _ok

    End Function
    Private Sub F_ClienteNuevoServicio_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _priniciarTodo()
        _prHabilitarFocus()
    End Sub



    Private Sub btnguardar_Click(sender As Object, e As EventArgs) Handles btnguardar.Click
        If (_prValidar()) Then


            Dim res As Boolean = L_Grabar_NitPrimero(tbNit.Text, tbRazonSocial.Text, "", CbTDoc.Value, TbEmailN.Text)

            If res Then
                ToastNotification.Show(Me, "Los datos se grabarono con éxito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
                Cliente = True
                Razonsocial = tbRazonSocial.Text
                Nit = tbNit.Text
                Correo = TbEmailN.Text
                NuevoCli = True
                'F1_MontoPagar.NuevoCliente = NuevoCli
                Me.Close()
            End If
        End If
    End Sub


    Private Sub btnsalir_Click(sender As Object, e As EventArgs) Handles btnsalir.Click
        Me.Close()
    End Sub


    Private Sub HabilitarEnter(sender As Object, e As KeyPressEventArgs)
        If (e.KeyChar = ChrW(Keys.Enter)) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub tbNombre_KeyPress(sender As Object, e As KeyPressEventArgs)
        HabilitarEnter(sender, e)
    End Sub

    Private Sub tbRazonSocial_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbRazonSocial.KeyPress
        HabilitarEnter(sender, e)
    End Sub

    Private Sub TbEmailN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TbEmailN.KeyPress
        HabilitarEnter(sender, e)
    End Sub



End Class