﻿Imports Logica.AccesoLogica
Imports Janus.Windows.GridEX.EditControls
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Imports Janus.Windows.GridEX
Imports System.IO

Module P_Global

#Region "Global"

    Private Declare Auto Function SetProcessWorkingSetSize Lib "kernel32.dll" (ByVal procHandle As IntPtr, ByVal min As Int32, ByVal max As Int32) As Boolean
    Public gs_SeparadorDecimal As Char = Application.CurrentCulture.NumberFormat.NumberDecimalSeparator
    Public Visualizador As Visualizador
    Public RutaGlobal As String = gs_CarpetaRaiz

    Public gi_fuenteTamano As Integer = 9

#End Region

#Region "Carpetas del sistema"

    Public gs_RutaImagenes As String = "C:\Imagenes"
    Public gs_RutaArchivos As String = "C:\Doc"

#End Region

#Region "Conexión a la Base de Datos"

    'Variables del archivo de configuración
    Public gs_Ip As String = "127.0.0.1"
    Public gs_UsuarioSql As String = "sa"
    Public gs_ClaveSql As String = "123"
    Public gs_NombreBD As String = "DBDinoM"
    Public gs_CarpetaRaiz As String = "C:\BD"

    Public gb_ConexionAbierta As Boolean = False

#End Region

#Region "Librerias"

    'Libreria de sistemas
    Public gi_LibSistema As Integer = 4
    Public gi_LibSISModulo As Integer = 1


#End Region

#Region "Metodos"

    'Tipos de Modos
    '1 Valida que sea solo Numeros
    '2 Valida que sea solo Letras
    Public Sub g_prValidarTextBox(ByVal _Modo As Byte, ByRef ee As KeyPressEventArgs)
        Select Case _Modo
            Case 1
                If (Char.IsNumber(ee.KeyChar)) Then
                    ee.Handled = False
                ElseIf (Char.IsControl(ee.KeyChar)) Then
                    ee.Handled = False
                ElseIf (Char.IsPunctuation(ee.KeyChar)) Then
                    ee.Handled = False
                Else
                    ee.Handled = True
                End If
            Case 2
                If (Char.IsLetter(ee.KeyChar)) Then
                    ee.Handled = False
                ElseIf (Char.IsControl(ee.KeyChar)) Then
                    ee.Handled = False
                Else
                    ee.Handled = True
                End If
        End Select
    End Sub

    Public Sub g_prArmarComboLibreria(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo, cod1 As String, cod2 As String)
        Dim dt As New DataTable
        dt = L_prLibreriaDetalleGeneral(cod1, cod2)

        With mCombo
            .DropDownList.Columns.Clear()

            .DropDownList.Columns.Add("cenum").Width = 70
            .DropDownList.Columns("cenum").Caption = "COD"

            .DropDownList.Columns.Add("cedesc1").Width = 200
            .DropDownList.Columns("cedesc1").Caption = "DESCRIPCION"

            .ValueMember = "cenum"
            .DisplayMember = "cedesc1"
            .DataSource = dt
            .Refresh()
        End With
    End Sub

    Public Sub g_prArmarCombo(cbj As MultiColumnCombo, dtCombo As DataTable,
                              Optional anchoCodigo As Integer = 60, Optional anchoDesc As Integer = 200,
                              Optional nombreCodigo As String = "Código", Optional nombreDescripción As String = "Nombre")
        With cbj.DropDownList
            .Columns.Clear()

            .Columns.Add(dtCombo.Columns("cod").ToString).Width = anchoCodigo
            .Columns(0).Caption = nombreCodigo
            .Columns(0).Visible = True

            .Columns.Add(dtCombo.Columns("desc").ToString).Width = anchoDesc
            .Columns(1).Caption = nombreDescripción
            .Columns(1).Visible = True

            .ValueMember = dtCombo.Columns("cod").ToString
            .DisplayMember = dtCombo.Columns("desc").ToString
            .DataSource = dtCombo
            .Refresh()
        End With
    End Sub
    Public Sub _prCrearCarpetaReportesGlobal()
        Dim rutaDestino As String = RutaGlobal + "\Reporte\Reporte Productos\"

        If System.IO.Directory.Exists(RutaGlobal + "\Reporte\Reporte Productos\") = False Then
            If System.IO.Directory.Exists(RutaGlobal + "\Reporte") = False Then
                System.IO.Directory.CreateDirectory(RutaGlobal + "\Reporte")
                If System.IO.Directory.Exists(RutaGlobal + "\Reporte\Reporte Productos") = False Then
                    System.IO.Directory.CreateDirectory(RutaGlobal + "\Reporte\Reporte Productos")
                End If
            Else
                If System.IO.Directory.Exists(RutaGlobal + "\Reporte\Reporte Productos") = False Then
                    System.IO.Directory.CreateDirectory(RutaGlobal + "\Reporte\Reporte Productos")
                End If
            End If
        End If
    End Sub

    Public Function P_ExportarExcelGlobal(_ruta As String, JGrM_Buscador As GridEX, nombre As String) As Boolean
        Dim _ubicacion As String
        If (1 = 1) Then 'If(_directorio.ShowDialog = Windows.Forms.DialogResult.OK) Then
            _ubicacion = _ruta
            Try
                Dim _stream As Stream
                Dim _escritor As StreamWriter
                Dim _fila As Integer = JGrM_Buscador.GetRows.Length
                Dim _columna As Integer = JGrM_Buscador.RootTable.Columns.Count
                Dim _archivo As String = _ubicacion & "\" & nombre & "_" & Now.Date.Day &
                    "." & Now.Date.Month & "." & Now.Date.Year & "_" & Now.Hour & "." & Now.Minute & "." & Now.Second & ".csv"
                Dim _linea As String = ""
                Dim _filadata = 0, columndata As Int32 = 0
                File.Delete(_archivo)
                _stream = File.OpenWrite(_archivo)
                _escritor = New StreamWriter(_stream, System.Text.Encoding.UTF8)

                For Each _col As GridEXColumn In JGrM_Buscador.RootTable.Columns
                    If (_col.Visible) Then
                        _linea = _linea & _col.Caption & ";"
                    End If
                Next
                _linea = Mid(CStr(_linea), 1, _linea.Length - 1)
                _escritor.WriteLine(_linea)
                _linea = Nothing

                For Each _fil As GridEXRow In JGrM_Buscador.GetRows
                    For Each _col As GridEXColumn In JGrM_Buscador.RootTable.Columns
                        If (_col.Visible) Then
                            Dim data As String = CStr(_fil.Cells(_col.Key).Value)
                            data = data.Replace(";", ",")
                            _linea = _linea & data & ";"
                        End If
                    Next
                    _linea = Mid(CStr(_linea), 1, _linea.Length - 1)
                    _escritor.WriteLine(_linea)
                    _linea = Nothing

                Next
                _escritor.Close()

                Try
                    Dim ef = New Efecto
                    ef._archivo = _archivo

                    ef.tipo = 1
                    ef.Context = "Su archivo ha sido Guardado en la ruta: " + _archivo + vbLf + "DESEA ABRIR EL ARCHIVO?"
                    ef.Header = "PREGUNTA"
                    ef.ShowDialog()
                    Dim bandera As Boolean = False
                    bandera = ef.band
                    If (bandera = True) Then
                        Process.Start(_archivo)
                    End If

                    Return True
                Catch ex As Exception
                    MsgBox(ex.Message)
                    Return False
                End Try
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        End If
        Return False
    End Function


    Public Sub ArmarComboTipoDoc(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnTiposDocIdentidad()

        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("codigoClasificador").Width = 70
            .DropDownList.Columns("codigoClasificador").Caption = "COD"
            .DropDownList.Columns.Add("descripcion").Width = 500
            .DropDownList.Columns("descripcion").Caption = "DESCRIPCION"
            .ValueMember = "codigoClasificador"
            .DisplayMember = "descripcion"
            .DataSource = dt
            .Refresh()
        End With
    End Sub
#End Region

#Region "Configuracion del sistema"
    Public gs_llaveDinases As String = "carlosdinases123"
    Public gb_mostrarMapa As Boolean = True
    Public gi_userFuente As Integer = 8
    Public gs_user As String = "DEFAULT"
    Public gi_userNumi As Integer = 0
    Public gi_userRol As Integer = 0
    Public gi_userSuc As Integer = 0
    Public gi_Mayusculas As Integer = 0
    Public gi_Ver_Servicios As Integer = 0
    'configuracion del sistema tabla TCG011
    Public gd_notaAproTeo As Double = 0
    Public gs_NroCaja As Integer = 1

    Public gs_IPMaquina As String = "0.0.0.0"
    Public gs_UsuMaquina As String = "DEFAULT"
    Public gs_VersionSistema As String = "VERSIÓN 0.0.0"

#End Region

#Region "Toast"
    Public Function getMensaje(mensaje As String, Optional tam As String = "6") As String
        Dim menFinal As String = "<b><font size=" + Chr(34) + "+" + tam + Chr(34) + "><font color=" + Chr(34) + "#FF0000" + Chr(34) + "></font>" + mensaje + "</font></b>"
        Return menFinal
    End Function
#End Region

#Region "Ventanas"


    Public Function _fnCrearPanelVentanas(frm As Form) As Panel
        Dim panel As New Panel()
        panel.Name = "panelA"
        panel.Dock = DockStyle.Fill
        panel.BackColor = Color.White
        frm.TopLevel = False
        frm.Location = New Point(0, 0)
        frm.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        frm.Dock = panel.Dock
        panel.Controls.Add(frm)
        Return panel

    End Function
#End Region

#Region "Factura"

    Public gi_IVA As Decimal = 13 'Valor por defecto 13 = IVA actual Bolivia 2016
    Public gi_ICE As Decimal = 55 'Valor por defecto 55 = ICE actual Bolivia 2016

    'Parematros de la tabla TC0001
    Public gb_FacturaEmite As Boolean = True 'Emite factura? true=Sistema factura; false=Sistema no factura 
    Public gi_FacturaTipo As Byte = 2 'Tipo de Factura, 1=Ticket, 2=Hoja Carta
    Public gi_FacturaCantidadItems As Byte = 20 'Cantidad de items para la factura, 0 es sin limite
    Public gb_FacturaIncluirICE As Boolean = False 'Incluir en Importe ICE / IEHD / TASAS?, true=Si se incluye, false=No se incluye
    Public gb_CodigoBarra As Boolean = False 'False=No habilita la venta con lector de codigo de barra, True=Si habilita la venta con lector de codigo de barra
    Public gb_DetalleProducto As Boolean = False 'False=No habilita ingreso de detalle de producto, True=Si habilita el ingreso de detalle de producto
    Public gb_UbiLogo As String = ""
    Public gb_NotaAdicional As String = ""
    Public gb_TipoAyuda As Integer = 1
    Public gb_MostrarFamilia As Integer = 0 '0 no muestra grupo Familia en producto, 1 muestra grupo Familia en producto

    Public gb_cufSifac As String = ""

    ''Parámetros para facturación
    Public gb_email As String = ""
    Public gb_password As String = ""
    Public gb_url As String = ""
    Public gb_OnOff As Integer = 1 '1 manda a facturar online, 2 manda a facturar offline
    Public actEconomica As Integer

#End Region

End Module
