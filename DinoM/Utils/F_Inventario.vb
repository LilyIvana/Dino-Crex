Imports Logica.AccesoLogica
Imports Janus.Windows.GridEX
Imports DevComponents.DotNetBar
Imports System.IO
Imports DevComponents.DotNetBar.SuperGrid
Imports System.Drawing
Imports DevComponents.DotNetBar.Controls
Public Class F_Inventario


    Private Sub _IniciarTodo()
        Dim dbname As String = "DBDinom"
        L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, dbname)

        _prCargarComboLibreriaDeposito(cbDeposito)


        Me.Text = "Reconstruir Inventario"

    End Sub

    Private Sub _prCargarComboLibreriaDeposito(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnListarDepositos()
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

        If (dt.Rows.Count > 0) Then
            mCombo.SelectedIndex = 0

        End If
    End Sub
    Private Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        Resetear()
        btnEgresos.Enabled = True
        btnGenerar.Enabled = False
    End Sub
    Public Sub Resetear()
        If (cbDeposito.SelectedIndex >= 0) Then
            L_fnResetearTI001(cbDeposito.Value) '' En este metodo colocamos todos los productos con stock 0 en el deposito seleccionado
            ArmarMovimientosIngresosEgresos()
            ArmarCompras()



        End If
    End Sub

    Public Sub ArmarCompras()
        Dim dtCompras As DataTable = L_fnObtenerCabezeraCompras(cbDeposito.Value)
        For i As Integer = 0 To dtCompras.Rows.Count - 1 Step 1
            Dim dtDetalle As DataTable = L_fnObteniendoDetalleCompra(dtCompras.Rows(i).Item("canumi"))

            For j As Integer = 0 To dtDetalle.Rows.Count - 1 Step 1
                '(obs As String, deposito As Integer,
                '    cbnumi As Integer, fact As Date, hact As String, uact As String, cbty5prod As Integer,
                '    cbcmin As Double, cblote As String, cbfechavenc As Date)
                Try
                    Dim obs As String = Str(dtCompras.Rows(i).Item("canumi")) + " - I - Compra numiprod: " + Str(dtDetalle.Rows(j).Item("cbty5prod")) + "|" + dtCompras.Rows(i).Item("proveedor")

                    L_prGrabarTI002(obs, cbDeposito.Value, dtCompras.Rows(i).Item("canumi"), dtCompras.Rows(i).Item("cafact"),
                                dtCompras.Rows(i).Item("cahact"), dtCompras.Rows(i).Item("cauact"),
                                dtDetalle.Rows(j).Item("cbty5prod"), dtDetalle.Rows(j).Item("cbcmin"),
                                dtDetalle.Rows(j).Item("cblote"), dtDetalle.Rows(j).Item("cbfechavenc"))

                Catch ex As Exception
                    Dim mensaje As String = ex.Message

                End Try

            Next


        Next
        Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
        ToastNotification.Show(Me, "Compras Generado Correctamente".ToUpper, img, 2000, eToastGlowColor.Green, eToastPosition.BottomCenter)

    End Sub


    Public Sub ArmarMovimientosIngresosEgresos()
        Dim dtTI0021 As DataTable = L_fnObtenerTI0021(cbDeposito.Value)
        '' Eliminando Referencias
        L_fnEliminandoTI0021EgresoIngreso(cbDeposito.Value)
        '' Insertando Registros TI0021
        Dim Bin As New MemoryStream
        Dim img As New Bitmap(My.Resources.delete, 28, 28)
        img.Save(Bin, Imaging.ImageFormat.Png)
        For i As Integer = 0 To dtTI0021.Rows.Count - 1 Step 1
            Dim estado As Integer = dtTI0021.Rows(i).Item("estado")
            If (estado = 0) Then

                '              a.icid ,a.icibid ,a.iccprod ,b.yfcdprod1  as producto,a.iccant ,
                'a.iclot ,a.icfvenc ,Cast(null as image ) as img,1 as estado,
                '(Sum(inv.iccven )+a.iccant  ) as stock
                'a.icid ,a.icibid ,a.iccprod ,b.cadesc as producto,a.iccant ,Cast(null as image ) as img,1 as estado

                Dim detalleCopia As DataTable = dtTI0021.Copy
                detalleCopia.Rows.Clear()
                Dim numi As Integer = dtTI0021.Rows(i).Item("icibid")

                detalleCopia.Rows.Add(0, numi, dtTI0021.Rows(i).Item("iccprod"), "", "", "", "",
                                     dtTI0021.Rows(i).Item("iccant"),
                                      dtTI0021.Rows(i).Item("iclot"),
                                      dtTI0021.Rows(i).Item("icfvenc"), Bin.GetBuffer, estado, 0)
                L_prInsertarTi0021(numi, 10, detalleCopia, cbDeposito.Value)
            End If

        Next


    End Sub

    Public Sub ArmarVentas()
        Dim dtVentas As DataTable = L_fnObtenerCabezeraVentas(cbDeposito.Value)
        For i As Integer = 0 To dtVentas.Rows.Count - 1 Step 1
            Dim dtDetalle As DataTable = L_fnObteniendoDetalleVentas(dtVentas.Rows(i).Item("tanumi"))

            For j As Integer = 0 To dtDetalle.Rows.Count - 1 Step 1
                '(obs As String, deposito As Integer,
                '    cbnumi As Integer, fact As Date, hact As String, uact As String, cbty5prod As Integer,
                '    cbcmin As Double, cblote As String, cbfechavenc As Date)
                Try

                    Dim obs As String = Str(dtVentas.Rows(i).Item("tanumi")) + "- I - Venta numiprod: " + Str(dtDetalle.Rows(j).Item("tbty5prod")) + "|" + dtVentas.Rows(i).Item("cliente")

                    Dim dtSaldos As DataTable = L_fnObteniendoSaldosTI001(dtDetalle.Rows(j).Item("tbty5prod"), cbDeposito.Value)
                    Dim cantidad As Double = dtDetalle.Rows(j).Item("tbcmin")
                    Dim codProducto As Integer = dtDetalle.Rows(j).Item("tbty5prod")
                    Dim saldo As Double = cantidad

                    Dim k As Integer = 0
                    If (dtSaldos.Rows.Count <= 0) Then
                        L_prGrabarTI002Venta(obs, cbDeposito.Value, dtDetalle.Rows(j).Item("tbnumi"),
                         dtVentas.Rows(i).Item("tafact"),
                       dtVentas.Rows(i).Item("tahact"), dtVentas.Rows(i).Item("tauact"),
                       dtDetalle.Rows(j).Item("tbty5prod"), saldo,
                      "20170101", CDate("2017/01/01"))

                    Else
                        While (k <= dtSaldos.Rows.Count - 1 And saldo > 0)

                            Dim inventario As Double = dtSaldos.Rows(k).Item("iccven")
                            If (inventario >= saldo) Then

                                L_prGrabarTI002Venta(obs, cbDeposito.Value, dtDetalle.Rows(j).Item("tbnumi"),
                                  dtVentas.Rows(i).Item("tafact"),
                                dtVentas.Rows(i).Item("tahact"), dtVentas.Rows(i).Item("tauact"),
                                dtDetalle.Rows(j).Item("tbty5prod"), saldo,
                                dtSaldos.Rows(k).Item("iclot"), dtSaldos.Rows(k).Item("icfven"))

                                saldo = 0

                            Else
                                If (k < dtSaldos.Rows.Count - 1 Or inventario > 0) Then
                                    L_prGrabarTI002Venta(obs, cbDeposito.Value, dtDetalle.Rows(j).Item("tbnumi"),
                                dtVentas.Rows(i).Item("tafact"),
                              dtVentas.Rows(i).Item("tahact"), dtVentas.Rows(i).Item("tauact"),
                              dtDetalle.Rows(j).Item("tbty5prod"), inventario,
                              dtSaldos.Rows(k).Item("iclot"), dtSaldos.Rows(k).Item("icfven"))

                                    saldo = saldo - inventario

                                Else
                                    L_prGrabarTI002Venta(obs, cbDeposito.Value, dtDetalle.Rows(j).Item("tbnumi"),
                                dtVentas.Rows(i).Item("tafact"),
                              dtVentas.Rows(i).Item("tahact"), dtVentas.Rows(i).Item("tauact"),
                              dtDetalle.Rows(j).Item("tbty5prod"), saldo,
                              dtSaldos.Rows(k).Item("iclot"), dtSaldos.Rows(k).Item("icfven"))

                                    saldo = 0
                                End If



                            End If



                            k += 1

                        End While
                    End If



                Catch ex As Exception
                    Dim mensaje As String = ex.Message

                End Try

            Next


        Next


    End Sub

    Private Sub F_Inventario_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _IniciarTodo()
        btnEgresos.Enabled = False
        btnGenerar.Enabled = True

        'bw.RunWorkerAsync()
    End Sub




    Private Sub btnEgresos_Click(sender As Object, e As EventArgs) Handles btnEgresos.Click

        ArmarVentas()

        btnEgresos.Enabled = False
        btnGenerar.Enabled = True
    End Sub

    Private Sub cbDeposito_ValueChanged(sender As Object, e As EventArgs) Handles cbDeposito.ValueChanged

    End Sub
End Class