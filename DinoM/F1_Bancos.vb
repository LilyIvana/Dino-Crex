Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Imports System.IO
Imports Janus.Windows.GridEX

Public Class F1_Bancos

#Region "Variable Globales"
    Dim _Inter As Integer = 0
    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
    Dim RutaGlobal As String = gs_CarpetaRaiz
    Dim RutaTemporal As String = "C:\Temporal"
    Dim Modificado As Boolean = False
    Dim nameImg As String = "Default.jpg"
    Dim Socio As Boolean = False
    Dim NumiCuentaContable As Integer = 0
#End Region


#Region "METODOS PRIVADOS"
    Private Sub _prCrearCarpetaTemporal()

        If System.IO.Directory.Exists(RutaTemporal) = False Then
            System.IO.Directory.CreateDirectory(RutaTemporal)
        Else
            My.Computer.FileSystem.DeleteDirectory(RutaTemporal, FileIO.DeleteDirectoryOption.DeleteAllContents)
            My.Computer.FileSystem.CreateDirectory(RutaTemporal)
            'My.Computer.FileSystem.DeleteDirectory(RutaTemporal, FileIO.UIOption.AllDialogs, FileIO.RecycleOption.SendToRecycleBin)
            'System.IO.Directory.CreateDirectory(RutaTemporal)

        End If

    End Sub
    Private Sub _prCrearCarpetaImagenes()
        Dim rutaDestino As String = RutaGlobal + "\Imagenes\Imagenes Banco\"

        If System.IO.Directory.Exists(RutaGlobal + "\Imagenes\Imagenes Banco\") = False Then
            If System.IO.Directory.Exists(RutaGlobal + "\Imagenes") = False Then
                System.IO.Directory.CreateDirectory(RutaGlobal + "\Imagenes")
                If System.IO.Directory.Exists(RutaGlobal + "\Imagenes\Imagenes Banco") = False Then
                    System.IO.Directory.CreateDirectory(RutaGlobal + "\Imagenes\Imagenes Banco")
                End If
            Else
                If System.IO.Directory.Exists(RutaGlobal + "\Imagenes\Imagenes Banco") = False Then
                    System.IO.Directory.CreateDirectory(RutaGlobal + "\Imagenes\Imagenes Banco")

                End If
            End If
        End If
    End Sub

    Public Sub _prDibujarImagenes()
        Dim length As Integer = CType(JGrM_Buscador.DataSource, DataTable).Rows.Count
        For i As Integer = 0 To length - 1 Step 1
            Dim nameImagen As String = CType(JGrM_Buscador.DataSource, DataTable).Rows(i).Item("caimage")
            If (nameImagen.Equals("Default.jpg")) Then
                Dim Bin As New MemoryStream
                Dim img As New Bitmap(My.Resources.imageDefault, 130, 80)
                img.Save(Bin, Imaging.ImageFormat.Jpeg)
                Bin.Dispose()

                CType(JGrM_Buscador.DataSource, DataTable).Rows(i).Item("img") = Bin.GetBuffer
            Else
                Dim Bin As New MemoryStream
                If (File.Exists(RutaGlobal + "\Imagenes\Imagenes Banco" + nameImagen)) Then
                    Dim img As New Bitmap(New Bitmap(RutaGlobal + "\Imagenes\Imagenes Banco" + nameImagen), 130, 80)
                    img.Save(Bin, Imaging.ImageFormat.Jpeg)
                    CType(JGrM_Buscador.DataSource, DataTable).Rows(i).Item("img") = Bin.GetBuffer
                    Bin.Dispose()
                Else

                    Dim img As New Bitmap(My.Resources.imageDefault, 130, 80)
                    img.Save(Bin, Imaging.ImageFormat.Jpeg)
                    Bin.Dispose()

                    CType(JGrM_Buscador.DataSource, DataTable).Rows(i).Item("img") = Bin.GetBuffer

                End If
            End If


        Next
    End Sub
    Private Sub _fnMoverImagenRuta(Folder As String, name As String)
        'copio la imagen en la carpeta del sistema
        If (Not name.Equals("Default.jpg") And File.Exists(RutaTemporal + name)) Then


            Try
                My.Computer.FileSystem.CopyFile(RutaTemporal + name,
     Folder + name, overwrite:=True)

            Catch ex As System.IO.IOException


            End Try



        End If
    End Sub
    Private Sub _prIniciarTodo()

        Me.Text = "B A N C O S"
        _PMIniciarTodo()
        _prAsignarPermisos()
        _prCargarLengthTextBox()
        GroupPanelBuscador.Style.BackColor = Color.FromArgb(13, 71, 161)
        GroupPanelBuscador.Style.BackColor2 = Color.FromArgb(13, 71, 161)
        GroupPanelBuscador.Style.TextColor = Color.White
        Dim blah As Bitmap = My.Resources.checked
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico
        JGrM_Buscador.RootTable.HeaderFormatStyle.FontBold = TriState.True
        JGrM_Buscador.AlternatingColors = True
        btnModificar.Enabled = True
        btnEliminar.Enabled = True
    End Sub

    Public Sub _prCargarLengthTextBox()
        tbNombre.MaxLength = 200
        tbnroCuentaBancaria.MaxLength = 60
        tbObservacion.MaxLength = 200
    End Sub



    Private Function _fnActionNuevo() As Boolean
        'Funcion que me devuelve True si esta en la actividad crear nuevo Tipo de Equipo
        Return tbcodigo.Text.ToString.Equals("") And tbNombre.ReadOnly = False
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



#End Region

#Region "METODOS SOBREESCRITOS"



    Public Overrides Sub _PMOHabilitar()
        tbNombre.ReadOnly = False
        tbnroCuentaBancaria.ReadOnly = False
        swEstado.IsReadOnly = False
        tbObservacion.ReadOnly = False
        _prCrearCarpetaImagenes()
        _prCrearCarpetaTemporal()

        btnImg.Visible = True

    End Sub
    Public Overrides Sub _PMOInhabilitar()
        tbNombre.ReadOnly = True
        tbnroCuentaBancaria.ReadOnly = True
        swEstado.IsReadOnly = True
        tbObservacion.ReadOnly = True
        tbcodigo.ReadOnly = True
        btnImg.Visible = False
    End Sub
    Public Overrides Sub _PMOHabilitarFocus()
        With MHighlighterFocus
            .SetHighlightOnFocus(tbNombre, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(tbnroCuentaBancaria, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(tbObservacion, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(tbcodigo, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)

        End With
    End Sub
    Public Overrides Sub _PMOLimpiar()


        tbcodigo.Text = ""
        tbNombre.Text = ""
        tbnroCuentaBancaria.Text = ""
        NumiCuentaContable = 0
        tbObservacion.Text = ""

        tbNombre.Focus()
        pbImg.Image = My.Resources.imageDefault
    End Sub
    Public Overrides Sub _PMOLimpiarErrores()
        MEP.Clear()
        tbcodigo.BackColor = Color.White
        tbNombre.BackColor = Color.White
        tbnroCuentaBancaria.BackColor = Color.White
        tbObservacion.BackColor = Color.White
    End Sub
    Public Overrides Function _PMOValidarCampos() As Boolean
        Dim _ok As Boolean = True
        MEP.Clear()

        'If tbCi.Text = String.Empty Then
        '    tbCi.BackColor = Color.Red
        '    MEP.SetError(tbCi, "ingrese Cedula de Identidad!".ToUpper)
        '    _ok = False
        'Else
        '    tbCi.BackColor = Color.White
        '    MEP.SetError(tbCi, "")
        'End If

        'If tbFnac.Text = String.Empty Then
        '    tbFnac.BackColor = Color.Red
        '    MEP.SetError(tbFnac, "Seleccione su fecha de nacimiento!".ToUpper)
        '    _ok = False
        'Else
        '    tbFnac.BackColor = Color.White
        '    MEP.SetError(tbFnac, "")
        'End If

        If tbNombre.Text = String.Empty Then
            tbNombre.BackColor = Color.Red
            MEP.SetError(tbNombre, "ingrese Dato en el campo Nombre !".ToUpper)
            _ok = False
        Else
            tbNombre.BackColor = Color.White
            MEP.SetError(tbNombre, "")
        End If

        'If tbDir.Text = String.Empty Then
        '    tbDir.BackColor = Color.Red
        '    MEP.SetError(tbDir, "ingrese su Direccion de domicilio o lugar donde vive!".ToUpper)
        '    _ok = False
        'Else
        '    tbDir.BackColor = Color.White
        '    MEP.SetError(tbDir, "")
        'End If
        'If tbEmail.Text = String.Empty Then
        '    tbEmail.BackColor = Color.Red
        '    MEP.SetError(tbEmail, "ingrese su direccion de correo electronico!".ToUpper)
        '    _ok = False
        'Else
        '    tbEmail.BackColor = Color.White
        '    MEP.SetError(tbEmail, "")
        'End If
        'If tbTel1.Text = String.Empty Then
        '    tbTel1.BackColor = Color.Red
        '    MEP.SetError(tbTel1, "ingrese su numero de Telefono o Celular!".ToUpper)
        '    _ok = False
        'Else
        '    tbTel1.BackColor = Color.White
        '    MEP.SetError(tbTel1, "")
        'End If


        MHighlighterFocus.UpdateHighlights()
        Return _ok
    End Function

    Public Overrides Function _PMOGetListEstructuraBuscador() As List(Of Modelo.Celda)
        Dim listEstCeldas As New List(Of Modelo.Celda)

        't.canumi , t.canombre, t.cacuenta, t.caobs, t.cafact, t.cahact, t.cauact 
        listEstCeldas.Add(New Modelo.Celda("canumi", True, "Codigo", 150))
        listEstCeldas.Add(New Modelo.Celda("canombre", True, "Nombre", 250))
        listEstCeldas.Add(New Modelo.Celda("canrocuenta", True, "Nro Cuenta", 180))

        listEstCeldas.Add(New Modelo.Celda("caobs", False))
        listEstCeldas.Add(New Modelo.Celda("caestado", False))
        listEstCeldas.Add(New Modelo.Celda("img", True, "IMAGEN", 150))
        listEstCeldas.Add(New Modelo.Celda("cafact", False))
        listEstCeldas.Add(New Modelo.Celda("caimage", False))
        listEstCeldas.Add(New Modelo.Celda("cahact", False))
        listEstCeldas.Add(New Modelo.Celda("cauact", False))

        Return listEstCeldas


    End Function
    Public Overrides Function _PMOGetTablaBuscador() As DataTable
        Dim dtBuscador As DataTable = L_prBancoGeneral()
        Return dtBuscador
    End Function



    Public Overrides Sub _PMOMostrarRegistro(_N As Integer)
        JGrM_Buscador.Row = _MPos

        't.canumi , t.canombre, t.cacuenta, t.caobs, t.cafact, t.cahact, t.cauact 
        With JGrM_Buscador
            tbcodigo.Text = .GetValue("canumi").ToString
            tbNombre.Text = .GetValue("canombre").ToString
            tbnroCuentaBancaria.Text = .GetValue("canrocuenta").ToString

            tbObservacion.Text = .GetValue("caobs").ToString
            lbFecha.Text = CType(.GetValue("cafact"), Date).ToString("dd/MM/yyyy")
            lbHora.Text = .GetValue("cahact").ToString
            lbUsuario.Text = .GetValue("cauact").ToString
            swEstado.Value = .GetValue("caestado")
        End With
        Dim name As String = JGrM_Buscador.GetValue("caimage")
        If name.Equals("Default.jpg") Or Not File.Exists(RutaGlobal + "\Imagenes\Imagenes Banco" + name) Then

            Dim im As New Bitmap(My.Resources.imageDefault)
            pbImg.Image = im
        Else
            If (File.Exists(RutaGlobal + "\Imagenes\Imagenes Banco" + name)) Then
                Dim Bin As New MemoryStream
                Dim im As New Bitmap(New Bitmap(RutaGlobal + "\Imagenes\Imagenes Banco" + name), 180, 157)
                im.Save(Bin, System.Drawing.Imaging.ImageFormat.Jpeg)
                pbImg.Image = Image.FromStream(Bin)
                Bin.Dispose()

            End If
        End If
        _prDibujarImagenes()
        LblPaginacion.Text = Str(_MPos + 1) + "/" + JGrM_Buscador.RowCount.ToString




    End Sub
    Public Overrides Function _PMOGrabarRegistro() As Boolean


        Dim estado As Integer = IIf(swEstado.Value = True, 1, 0)
        Dim res As Boolean = L_prBancoGrabar(tbcodigo.Text, tbNombre.Text, tbnroCuentaBancaria.Text, tbObservacion.Text, nameImg, estado)
        If res Then

            Modificado = False
            _fnMoverImagenRuta(RutaGlobal + "\Imagenes\Imagenes Banco", nameImg)
            nameImg = "Default.jpg"
            _PMOLimpiar()
            ToastNotification.Show(Me, "Codigo de BANCO ".ToUpper + tbcodigo.Text + " Grabado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
        End If
        Return res

    End Function
    Public Overrides Function _PMOModificarRegistro() As Boolean
        Dim tipo As Integer = 1
        Dim nsoc As Integer = 1
        Dim res As Boolean
        Dim nameImage As String = JGrM_Buscador.GetValue("caimage")

        Dim estado As Integer = IIf(swEstado.Value = True, 1, 0)
        If (Modificado = False) Then
            res = L_prBancoModificar(tbcodigo.Text, tbNombre.Text, tbnroCuentaBancaria.Text, tbObservacion.Text, nameImg, estado)

        Else
            res = L_prBancoModificar(tbcodigo.Text, tbNombre.Text, tbnroCuentaBancaria.Text, tbObservacion.Text, nameImg, estado)
        End If





        If res Then


            If (Modificado = True) Then
                _fnMoverImagenRuta(RutaGlobal + "\Imagenes\Imagenes Banco", nameImg)

            End If
            Modificado = False
            _PMInhabilitar()
            _PMPrimerRegistro()
            ToastNotification.Show(Me, "Codigo de BANCO ".ToUpper + tbcodigo.Text + " modificado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
        End If
        Return res
    End Function
    Public Sub _PrEliminarImage()

        If (Not (_fnActionNuevo()) And (File.Exists(RutaGlobal + "\Imagenes\Imagenes Banco\Imagen_" + tbcodigo.Text + ".jpg"))) Then
            pbImg.Dispose()
            pbImg = Nothing
            Try
                My.Computer.FileSystem.DeleteFile(RutaGlobal + "\Imagenes\Imagenes Banco\Imagen_" + tbcodigo.Text + ".jpg")
            Catch ex As Exception

            End Try


        End If
    End Sub

    Public Overrides Sub _PMOEliminarRegistro()
        Dim info As New TaskDialogInfo("¿esta seguro de eliminar el registro?".ToUpper, eTaskDialogIcon.Delete, "advertencia".ToUpper, "mensaje principal".ToUpper, eTaskDialogButton.Yes Or eTaskDialogButton.Cancel, eTaskDialogBackgroundColor.Blue)
        Dim result As eTaskDialogResult = TaskDialog.Show(info)
        If result = eTaskDialogResult.Yes Then
            Dim mensajeError As String = ""
            Dim res As Boolean = L_prBancoBorrar(tbcodigo.Text, mensajeError)
            If res Then

                _PrEliminarImage()
                ToastNotification.Show(Me, "Codigo de Banco ".ToUpper + tbcodigo.Text + " eliminado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
                _PMFiltrar()
            Else
                ToastNotification.Show(Me, mensajeError, My.Resources.WARNING, 8000, eToastGlowColor.Red, eToastPosition.TopCenter)
            End If
        End If
    End Sub
    Private Function _fnCopiarImagenRutaDefinida() As String
        'copio la imagen en la carpeta del sistema

        Dim file As New OpenFileDialog()
        file.Filter = "Ficheros JPG o JPEG o PNG|*.jpg;*.jpeg;*.png" &
                      "|Ficheros GIF|*.gif" &
                      "|Ficheros BMP|*.bmp" &
                      "|Ficheros PNG|*.png" &
                      "|Ficheros TIFF|*.tif"
        If file.ShowDialog() = DialogResult.OK Then
            Dim ruta As String = file.FileName


            If file.CheckFileExists = True Then
                Dim img As New Bitmap(New Bitmap(ruta), 500, 300)
                Dim imgM As New Bitmap(New Bitmap(ruta), 180, 157)
                Dim Bin As New MemoryStream
                imgM.Save(Bin, System.Drawing.Imaging.ImageFormat.Jpeg)
                Dim a As Object = file.GetType.ToString
                If (_fnActionNuevo()) Then

                    Dim pos, mayor As Integer
                    Dim length As Integer = CType(JGrM_Buscador.DataSource, DataTable).Rows.Count
                    _prFabrica_ObtenerMayorDataSource(mayor, pos, length)
                    nameImg = "\Imagen_" + Str(mayor + 1).Trim + ".jpg"

                    pbImg.Image = Image.FromStream(Bin)

                    img.Save(RutaTemporal + nameImg, System.Drawing.Imaging.ImageFormat.Jpeg)
                    img.Dispose()
                Else

                    nameImg = "\Imagen_" + Str(tbcodigo.Text).Trim + ".jpg"


                    pbImg.Image = Image.FromStream(Bin)
                    img.Save(RutaTemporal + nameImg, System.Drawing.Imaging.ImageFormat.Jpeg)
                    Modificado = True
                    img.Dispose()

                End If
            End If

            Return nameImg
        End If

        Return "default.jpg"
    End Function
    Private Sub _prFabrica_ObtenerMayorDataSource(ByRef _mayor As Integer, ByRef _posicion As Integer, ByVal _lenght As Integer)

        _posicion = -1
        _mayor = 0
        If (_lenght >= 1) Then
            _posicion = 0
            _mayor = CType(JGrM_Buscador.DataSource, DataTable).Rows(_lenght - 1).Item("canumi")
        End If


    End Sub

    Private Sub F1_Bancos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()

    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        tbNombre.Focus()

    End Sub

    Private Sub ButtonX1_Click(sender As Object, e As EventArgs) Handles btnImg.Click
        _fnCopiarImagenRutaDefinida()
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        If btnGrabar.Enabled = True Then
            _PMInhabilitar()
            _PMPrimerRegistro()

        Else
            Close()
        End If
    End Sub

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