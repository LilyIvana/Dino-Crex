﻿
Imports System.Data
Imports System.Data.SqlClient
Imports Datos.AccesoDatos

Public Class AccesoLogica

    Public Shared L_Usuario As String = "DEFAULT"
    Public Shared L_IdUsuario As Integer = 0

#Region "CONFIGURACION DEL SISTEMA"

    Public Shared Function L_fnConfSistemaGeneral() As DataTable
        Dim _Tabla As DataTable
        Dim _Where, _campos As String
        Dim _dtConfSist As DataTable = D_Datos_Tabla("cnumi", "TC000", "1=1")

        _Where = "ccctc0=" + _dtConfSist.Rows(0).Item("cnumi").ToString
        _campos = "*"
        _Tabla = D_Datos_Tabla(_campos, "TC0001", _Where)
        Return _Tabla
    End Function
    Public Shared Function L_fnConfSistemaModificar(ByRef _numi As String) As Boolean
        Dim _Error As Boolean
        Dim Sql, _where As String

        Sql = "cnumi =" + _numi

        _where = "1=1"
        _Error = D_Modificar_Datos("TC000", Sql, _where)
        Return Not _Error
    End Function

    Public Shared Sub L_ModificarParametrosFact(_onoff As String)
        Dim _Err As Boolean
        Dim Sql, _where As String

        Sql = "OnOff = " + _onoff + ", fact = '" + Now.Date.ToString("yyyy/MM/dd") + "' , " +
             "hact = '" + Now.Hour.ToString("D2") + ":" + Now.Minute.ToString("D2") + "' , " +
             "uact = '" + L_Usuario + "' "


        _where = "estado =1 "
        _Err = D_Modificar_Datos("ParametrosFact", Sql, _where)
    End Sub
    Public Shared Function L_fnConfParametrosFacturacion() As DataTable
        Dim _Tabla As DataTable
        Dim _Where, _campos As String


        _Where = "estado=1"
        _campos = "*"
        _Tabla = D_Datos_Tabla(_campos, "ParametrosFact", _Where)
        Return _Tabla
    End Function
#End Region

#Region "METODOS PRIVADOS"
    Public Shared Sub L_prAbrirConexion(Optional Ip As String = "", Optional UsuarioSql As String = "", Optional ClaveSql As String = "", Optional NombreBD As String = "")
        D_abrirConexion(Ip, UsuarioSql, ClaveSql, NombreBD)
    End Sub
    Public Shared Sub L_prAbrirConexionBitacora(Optional Ip As String = "", Optional UsuarioSql As String = "", Optional ClaveSql As String = "", Optional NombreBD As String = "")
        D_abrirConexionHistorial(Ip, UsuarioSql, ClaveSql, NombreBD)
    End Sub

    Public Shared Function _fnsAuditoria() As String
        Return "'" + Date.Now.Date.ToString("yyyy/MM/dd") + "', '" + Now.Hour.ToString("00") + ":" + Now.Minute.ToString("00") + "' ,'" + L_Usuario + "'"
    End Function
#End Region

#Region "LIBRERIAS"


    Public Shared Function L_prLibreriaDetalleGeneral(_cod1 As String, _cod2 As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@cncod1", _cod1))
        _listParam.Add(New Datos.DParametro("@cncod2", _cod2))
        _listParam.Add(New Datos.DParametro("@cnuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC0051", _listParam)

        Return _Tabla
    End Function


#End Region

#Region "VALIDAR ELIMINACION"
    Public Shared Function L_fnbValidarEliminacion(_numi As String, _tablaOri As String, _campoOri As String, ByRef _respuesta As String) As Boolean
        Dim _Tabla As DataTable
        Dim _Where, _campos As String
        _Where = "bbtori='" + _tablaOri + "' and bbtran=1"
        _campos = "bbnumi,bbtran,bbtori,bbcori,bbtdes,bbcdes,bbprog"
        _Tabla = D_Datos_Tabla(_campos, "TB002", _Where)
        _respuesta = "no se puede eliminar el registro: ".ToUpper + _numi + " por que esta siendo usado en los siguientes programas: ".ToUpper + vbCrLf

        Dim result As Boolean = True
        For Each fila As DataRow In _Tabla.Rows
            If L_fnbExisteRegEnTabla(_numi, fila.Item("bbtdes").ToString, fila.Item("bbcdes").ToString) = True Then
                _respuesta = _respuesta + fila.Item("bbprog").ToString + vbCrLf
                result = False
            End If
        Next
        Return result
    End Function

    Private Shared Function L_fnbExisteRegEnTabla(_numiOri As String, _tablaDest As String, _campoDest As String) As Boolean
        Dim _Tabla As DataTable
        Dim _Where, _campos As String
        _Where = _campoDest + "=" + _numiOri
        _campos = _campoDest
        _Tabla = D_Datos_Tabla(_campos, _tablaDest, _Where)
        If _Tabla.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
#End Region

#Region "METODOS PARA EL CONTROL DE USUARIOS Y PRIVILEGIOS"

#Region "Formularios"
    Public Shared Function L_Formulario_General(_Modo As Integer, Optional _Cadena As String = "") As DataSet
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet
        Dim _Where As String
        If _Modo = 0 Then
            _Where = "ZY001.yanumi=ZY001.yanumi and ZY001.yamod=TY0031.yccod3 and TY0031.yccod1=4 AND TY0031.yccod2=1 "
        Else
            _Where = "ZY001.yanumi=ZY001.yanumi and ZY001.yamod=TY0031.yccod3 and TY0031.yccod1=4 AND TY0031.yccod2=1 " + _Cadena
        End If
        Dim con As String = "ZY001.yanumi,ZY001.yaprog,ZY001.yatit,ZY001.yamod,TY0031.ycdes3  " + "ZY001,TY0031  " + _Where
        _Tabla = D_Datos_Tabla("ZY001.yanumi,ZY001.yaprog,ZY001.yatit,ZY001.yamod,TY0031.ycdes3", "ZY001,TY0031", _Where + " order by yanumi")
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function

    Public Shared Sub L_Formulario_Grabar(ByRef _numi As String, _desc As String, _direc As String, _categ As String)
        Dim _Err As Boolean
        Dim _Tabla As DataTable
        _Tabla = D_Maximo("ZY001", "yanumi", "yanumi=yanumi")
        If Not IsDBNull(_Tabla.Rows(0).Item(0)) Then
            _numi = _Tabla.Rows(0).Item(0) + 1
        Else
            _numi = "1"
        End If

        Dim Sql As String
        Sql = _numi + ",'" + _desc + "','" + _direc + "'," + _categ
        _Err = D_Insertar_Datos("ZY001", Sql)
    End Sub

    Public Shared Sub L_Formulario_Modificar(_numi As String, _desc As String, _direc As String, _categ As String)
        Dim _Err As Boolean
        Dim Sql, _where As String

        Sql = "yaprog = '" + _desc + "' , " +
        "yatit = '" + _direc + "' , " +
        "yamod = " + _categ

        _where = "yanumi = " + _numi
        _Err = D_Modificar_Datos("ZY001", Sql, _where)
    End Sub

    Public Shared Sub L_Formulario_Borrar(_Id As String)
        Dim _Where As String
        Dim _Err As Boolean
        _Where = "yanumi = " + _Id
        _Err = D_Eliminar_Datos("ZY001", _Where)
    End Sub
#End Region

#Region "Roles"
    Public Shared Function L_Rol_General(_Modo As Integer, Optional _Cadena As String = "") As DataSet
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet
        Dim _Where As String
        If _Modo = 0 Then
            _Where = "ZY002.ybnumi=ZY002.ybnumi "
        Else
            _Where = "ZY002.ybnumi=ZY002.ybnumi " + _Cadena
        End If
        _Tabla = D_Datos_Tabla("ZY002.ybnumi,ZY002.ybrol", "ZY002", _Where + " order by ybnumi")
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function
    Public Shared Function L_RolDetalle_General(_Modo As Integer, Optional _idCabecera As String = "", Optional _idModulo As String = "") As DataTable
        Dim _Tabla As DataTable
        Dim _Where As String
        If _Modo = 0 Then
            _Where = " ycnumi = ycnumi"
        Else
            _Where = " ycnumi=" + _idCabecera + " and ZY001.yamod=" + _idModulo + " and ZY0021.ycyanumi=ZY001.yanumi"
        End If
        _Tabla = D_Datos_Tabla("ZY0021.ycnumi,ZY0021.ycyanumi,ZY0021.ycshow,ZY0021.ycadd,ZY0021.ycmod,ZY0021.ycdel,ZY001.yaprog,ZY001.yatit", "ZY0021,ZY001", _Where)
        Return _Tabla
    End Function

    Public Shared Function L_RolDetalle_General2(_Modo As Integer, Optional _idCabecera As String = "", Optional _where1 As String = "") As DataTable
        Dim _Tabla As DataTable
        Dim _Where As String
        If _Modo = 0 Then
            _Where = " ycnumi = ycnumi"
        Else
            _Where = " ycnumi=" + _idCabecera + " and " + _where1
        End If
        _Tabla = D_Datos_Tabla("ycnumi,ycyanumi,ycshow,ycadd,ycmod,ycdel", "ZY0021", _Where)
        Return _Tabla
    End Function

    Public Shared Function L_prRolDetalleGeneral(_numiRol As String, _idNombreButton As String) As DataTable
        Dim _Tabla As DataTable
        Dim _Where As String

        _Where = "ZY0021.ycnumi=" + _numiRol + " and ZY0021.ycyanumi=ZY001.yanumi and ZY001.yaprog='" + _idNombreButton + "'"

        _Tabla = D_Datos_Tabla("ycnumi,ycyanumi,ycshow,ycadd,ycmod,ycdel", "ZY0021,ZY001", _Where)
        Return _Tabla
    End Function

    Public Shared Sub L_Rol_Grabar(ByRef _numi As String, _rol As String)
        Dim _Actualizacion As String
        Dim _Err As Boolean
        Dim _Tabla As DataTable
        _Tabla = D_Maximo("ZY002", "ybnumi", "ybnumi=ybnumi")
        If Not IsDBNull(_Tabla.Rows(0).Item(0)) Then
            _numi = _Tabla.Rows(0).Item(0) + 1
        Else
            _numi = "1"
        End If

        _Actualizacion = "'" + Date.Now.Date.ToString("yyyy/MM/dd") + "', '" + Now.Hour.ToString + ":" + Now.Minute.ToString + "' ,'" + L_Usuario + "'"

        Dim Sql As String
        Sql = _numi + ",'" + _rol + "'," + _Actualizacion
        _Err = D_Insertar_Datos("ZY002", Sql)
    End Sub
    Public Shared Sub L_RolDetalle_Grabar(_idCabecera As String, _numi1 As Integer, _show As Boolean, _add As Boolean, _mod As Boolean, _del As Boolean)
        Dim _Err As Boolean
        Dim Sql As String
        Sql = _idCabecera & "," & _numi1 & ",'" & _show & "','" & _add & "','" & _mod & "','" & _del & "'"
        _Err = D_Insertar_Datos("ZY0021", Sql)
    End Sub
    Public Shared Sub L_Rol_Modificar(_numi As String, _desc As String)
        Dim _Err As Boolean
        Dim Sql, _where As String

        Sql = "ybrol = '" + _desc + "' "

        _where = "ybnumi = " + _numi
        _Err = D_Modificar_Datos("ZY002", Sql, _where)
    End Sub
    Public Shared Sub L_Rol_Borrar(_Id As String)
        Dim _Where As String
        Dim _Err As Boolean
        _Where = "ybnumi = " + _Id
        _Err = D_Eliminar_Datos("ZY002", _Where)
    End Sub
    Public Shared Sub L_RolDetalle_Modificar(_idCabecera As String, _numi1 As Integer, _show As Boolean, _add As Boolean, _mod As Boolean, _del As Boolean)
        Dim _Err As Boolean
        Dim Sql, _where As String

        Sql = "ycshow = '" & _show & "' , " & "ycadd = '" & _add & "' , " & "ycmod = '" & _mod & "' , " & "ycdel = '" & _del & "' "

        _where = "ycnumi = " & _idCabecera & " and ycyanumi = " & _numi1
        _Err = D_Modificar_Datos("ZY0021", Sql, _where)
    End Sub
    Public Shared Sub L_RolDetalle_Borrar(_Id As String, _Id1 As String)
        Dim _Where As String
        Dim _Err As Boolean

        _Where = "ycnumi = " + _Id + " and ycyanumi = " + _Id1
        _Err = D_Eliminar_Datos("ZY0021", _Where)
    End Sub
    Public Shared Sub L_RolDetalle_Borrar(_Id As String)
        Dim _Where As String
        Dim _Err As Boolean

        _Where = "ycnumi = " + _Id
        _Err = D_Eliminar_Datos("ZY0021", _Where)
    End Sub
#End Region

#Region "Usuarios"
    Public Shared Function L_Usuario_General(_Modo As Integer, Optional _Cadena As String = "") As DataSet
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet
        Dim _Where As String
        If _Modo = 0 Then
            _Where = "ZY003.ydnumi=ZY003.ydnumi and ZY002.ybnumi=ZY003.ydrol "
        Else
            _Where = "ZY003.ydnumi=ZY003.ydnumi and ZY002.ybnumi=ZY003.ydrol " + _Cadena
        End If
        _Tabla = D_Datos_Tabla("ZY003.ydnumi,ZY003.yduser,ZY003.ydpass,ZY003.ydest,ZY003.ydcant,ZY002.ybnumi,ZY002.ybrol", "ZY003,ZY002", _Where + " order by ydnumi")
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function


    Public Shared Function L_Usuario_General2(_Modo As Integer, Optional _Cadena As String = "") As DataSet
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet
        Dim _Where As String
        If _Modo = 0 Then
            _Where = "ZY003.ydnumi=ZY003.ydnumi and ZY002.ybnumi=ZY003.ydrol and TA001.aanumi=ZY003.ydsuc "
        Else
            _Where = "ZY003.ydnumi=ZY003.ydnumi and ZY002.ybnumi=ZY003.ydrol and TA001.aanumi=ZY003.ydsuc " + _Cadena
        End If

        _Tabla = D_Datos_Tabla("ZY003.ydnumi,ZY003.yduser,ZY003.ydpass,ZY003.ydest,ZY003.ydcant,ZY003.ydfontsize,ZY002.ybnumi,ZY002.ybrol,ZY003.ydsuc,ZY003.ydall,TA001.aabdes,ZY003.ydfact,ZY003.ybhact,ZY003.ybuact,ZY003.yd_numiVend", "ZY003,ZY002,TA001", _Where + " order by ydnumi")
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function

    Public Shared Function L_Usuario_General3(_Modo As Integer, Optional _Cadena As String = "") As DataSet
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet
        Dim _Where As String
        If _Modo = 0 Then
            _Where = "1=1"
        Else
            _Where = _Cadena
        End If
        _Tabla = D_Datos_Tabla("ZY003.ydnumi,ZY003.yduser", "ZY003", _Where + " order by yduser")
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function
    Public Shared Function L_Usuario_General4(_Modo As Integer, Optional _Cadena As String = "") As DataSet
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet
        Dim _Where As String
        If _Modo = 0 Then
            _Where = "ZY003.ydnumi=ZY003.ydnumi and ZY002.ybnumi=ZY003.ydrol and TA001.aanumi=ZY003.ydsuc and ZY003.ydest=1 "
        Else
            _Where = "ZY003.ydnumi=ZY003.ydnumi and ZY002.ybnumi=ZY003.ydrol and TA001.aanumi=ZY003.ydsuc " + _Cadena
        End If

        _Tabla = D_Datos_Tabla("ZY003.ydnumi,ZY003.yduser,ZY003.ydpass,ZY003.ydest,ZY003.ydcant,ZY003.ydfontsize,ZY002.ybnumi,ZY002.ybrol,ZY003.ydsuc,ZY003.ydall,TA001.aabdes,ZY003.ydfact,ZY003.ybhact,ZY003.ybuact,ZY003.yd_numiVend", "ZY003,ZY002,TA001", _Where + " order by ydnumi")
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function
    Public Shared Sub L_Usuario_Grabar(ByRef _numi As String, _user As String, _pass As String, _rol As String, _estado As String,
                                       _cantDias As String, _tamFuente As String, _suc As String, _allSuc As String,
                                       _numiVend As String, _version As String, _ip As String, _usumaquina As String)
        Dim _Actualizacion As String
        Dim _Err, _Err2, _Err3 As Boolean
        Dim _Tabla As DataTable
        _Tabla = D_Maximo("ZY003", "ydnumi", "ydnumi=ydnumi")
        If Not IsDBNull(_Tabla.Rows(0).Item(0)) Then
            _numi = _Tabla.Rows(0).Item(0) + 1
        Else
            _numi = "1"
        End If

        _Actualizacion = "'" + Date.Now.Date.ToString("yyyy/MM/dd") + "', '" + Now.Hour.ToString("D2") + ":" + Now.Minute.ToString("D2") + "' ,'" + L_Usuario + "'"

        Dim Sql, Sql2, Sql3 As String
        Sql = _numi + ",'" + _user + "'," + _rol + ",'" + _pass + "','" + _estado + "'," + _cantDias + "," + _tamFuente + "," + _suc + "," + _allSuc + "," + _Actualizacion + "," + _numiVend
        _Err = D_Insertar_Datos("ZY003", Sql)

        Sql2 = "'ZY003' ," + _numi + "," + "1" + "," + _Actualizacion
        _Err2 = D_Insertar_Datos("SI001", Sql2)

        Sql3 = "'ZY003' , 'USUARIOS' , " + "1" + ", 'NUEVO' , " + _numi + ", '" + _version + "', '" + _ip + "', '" + _usumaquina + "'," + "0" + ", '" + " " + "', '" + " " + "' ," + _Actualizacion
        _Err3 = D_Insertar_Datos("BitacoraGeneral", Sql3)
    End Sub
    Public Shared Sub L_Usuario_Modificar(_numi As String, _user As String, _pass As String, _rol As String, _estado As String,
                                          _cantDias As String, _tamFuente As String, _suc As String, _allSuc As String, 
                                          _numiVend As String, _version As String, _ip As String, _usumaquina As String)
        Dim _Err, _Err2, _Err3 As Boolean
        Dim Sql, Sql2, Sql3, _Actualizacion, _where As String

        Sql = "yduser = '" + _user + "' , " +
        "ydpass = '" + _pass + "' , " +
        "ydrol = " + _rol + " , " +
        "ydest = '" + _estado + "' , " +
        "ydcant = " + _cantDias + " , " +
        "ydfontsize = " + _tamFuente + " , " +
        "ydsuc = " + _suc + " , " +
        "ydall = " + _allSuc + " , " +
        "yd_numiVend = " + _numiVend

        _where = "ydnumi = " + _numi
        _Err = D_Modificar_Datos("ZY003", Sql, _where)

        _Actualizacion = "'" + Date.Now.Date.ToString("yyyy/MM/dd") + "', '" + Now.Hour.ToString("D2") + ":" + Now.Minute.ToString("D2") + "' ,'" + L_Usuario + "'"

        Sql2 = "'ZY003' ," + _numi + "," + "2" + "," + _Actualizacion
        _Err2 = D_Insertar_Datos("SI001", Sql2)

        Sql3 = "'ZY003' , 'USUARIOS' , " + "2" + ", 'MODIFICADO' , " + _numi + ", '" + _version + "', '" + _ip + "', '" + _usumaquina + "'," + "0" + ", '" + " " + "', '" + " " + "' ," + _Actualizacion
        _Err3 = D_Insertar_Datos("BitacoraGeneral", Sql3)
    End Sub
    Public Shared Sub L_Usuario_Borrar(_Id As String, _version As String, _ip As String, _usumaquina As String)
        Dim _Where, Sql2, Sql3, _Actualizacion As String
        Dim _Err, _Err2, _Err3 As Boolean
        _Where = "ydnumi = " + _Id
        _Err = D_Eliminar_Datos("ZY003", _Where)

        _Actualizacion = "'" + Date.Now.Date.ToString("yyyy/MM/dd") + "', '" + Now.Hour.ToString("D2") + ":" + Now.Minute.ToString("D2") + "' ,'" + L_Usuario + "'"

        Sql2 = "'ZY003' ," + _Id + "," + "-1" + "," + _Actualizacion
        _Err2 = D_Insertar_Datos("SI001", Sql2)

        Sql3 = "'ZY003' , 'USUARIOS' , " + "-1" + ", 'ELIMINADO' , " + _Id + ", '" + _version + "', '" + _ip + "', '" + _usumaquina + "'," + "0" + ", '" + " " + "', '" + " " + "' ," + _Actualizacion
        _Err3 = D_Insertar_Datos("BitacoraGeneral", Sql3)
    End Sub

    Public Shared Function L_Validar_Usuario2(_Nom As String, _Pass As String) As Boolean
        Dim _Tabla As DataTable
        _Tabla = D_Datos_Tabla("*", "ZY003", "yduser = '" + _Nom + "' AND ydpass = '" + _Pass + "'")
        If _Tabla.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function L_Validar_Usuario(_Nom As String, _Pass As String) As DataTable
        Dim _Tabla As DataTable
        _Tabla = D_Datos_Tabla("ydnumi,yduser,ydrol,ydpass,ydest,ydcant,ydfontsize,ydsuc", "ZY003", " yduser = '" + _Nom + "' AND ydpass = '" + _Pass + "'")
        Return _Tabla
    End Function

    Public Shared Function L_VerConfiguracion() As DataTable
        Dim _Tabla As DataTable
        _Tabla = D_Datos_Tabla("VerServicios", "SY000", "1=1")
        Return _Tabla
    End Function
    Public Shared Function TipoDescuentoEsXCantidad() As Boolean
        Dim _Tabla As DataTable
        _Tabla = D_Datos_Tabla("Count(*)", "SY000", "VerTipoDescuento = 0")
        If _Tabla.Rows.Count > 0 Then
            If _Tabla.Rows(0).Item(0) = 1 Then
                Return True
            Else
                Return False
            End If
        End If
    End Function
    Public Shared Function L_ListarUsuarios() As DataTable
        Dim _Tabla As DataTable
        Dim _Where As String
        _Where = "1=1 Order by ydnumi"

        _Tabla = D_Datos_Tabla("ydnumi,yduser,ydrol,yd_numiVend", "ZY003", _Where)
        Return _Tabla
    End Function
    Public Shared Function L_BuscarPoUsuario(_Nom As String) As DataTable
        Dim _Tabla As DataTable
        _Tabla = D_Datos_Tabla("ydnumi,yduser,ydrol, yd_numiVend", "ZY003", "yduser = '" + _Nom + "'")
        Return _Tabla
    End Function
#End Region

#End Region

#Region "TY005 PRODUCTOS"
    Public Shared Function L_prLibreriaGrabar(ByRef _numi As String, _cod1 As String, _cod2 As String, _desc1 As String, _desc2 As String) As Boolean
        Dim _Error As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@ylcod1", _cod1))
        _listParam.Add(New Datos.DParametro("@ylcod2", _cod2))
        _listParam.Add(New Datos.DParametro("@desc", _desc1))
        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _numi = _Tabla.Rows(0).Item(0)
            _Error = False
        Else
            _Error = True
        End If
        Return Not _Error
    End Function

    Public Shared Function L_prLibreriaGrabarGrupos(ByRef _numi As String, _cod1 As String, _cod2 As String, _desc1 As String, _desc2 As String, _cecon As Integer) As Boolean
        Dim _Error As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@ylcod1", _cod1))
        _listParam.Add(New Datos.DParametro("@ylcod2", _cod2))
        _listParam.Add(New Datos.DParametro("@cecon", _cecon))
        _listParam.Add(New Datos.DParametro("@desc", _desc1))
        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _numi = _Tabla.Rows(0).Item(0)
            _Error = False
        Else
            _Error = True
        End If
        Return Not _Error
    End Function

    Public Shared Function L_fnEliminarProducto(numi As String, ByRef mensaje As String, _version As String, _ip As String,
                                                _usumaquina As String) As Boolean
        Dim _resultado As Boolean
        If L_fnbValidarEliminacion(numi, "TY005", "yfnumi", mensaje) = True Then
            Dim _Tabla As DataTable
            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@yfnumi", numi))
            _listParam.Add(New Datos.DParametro("@version", _version))
            _listParam.Add(New Datos.DParametro("@ip", _ip))
            _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
            _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If
        Return _resultado
    End Function

    Public Shared Function L_fnGrabarProducto(ByRef _yfnumi As String, _yfcprod As String,
                                              _yfcbarra As String, _yfcdprod1 As String,
                                              _yfcdprod2 As String, _yfgr1 As Integer, _yfgr2 As Integer,
                                              _yfgr3 As Integer, _yfgr4 As Integer, _yfMed As Integer, _yfumin As Integer,
                                              _yfusup As Integer, _yfvsup As Double, _yfsmin As Integer, _yfap As Integer,
                                              _yfimg As String, TY0051 As DataTable, _yfdetpro As String, _yfgr5 As String,
                                              _ycodact As String, _ycodu As Integer, _ycodprosin As String, _ypreciosif As Double,
                                              _yfprefijo As String, _yfconv2 As Double, _yfconv3 As Double, _yfucompra As Integer,
                                              TY0053 As DataTable, _canje As String, _version As String, _ip As String, _usumaquina As String) As Boolean
        Dim _resultado As Boolean
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@yfnumi", _yfnumi))
        _listParam.Add(New Datos.DParametro("@yfcprod", _yfcprod))
        _listParam.Add(New Datos.DParametro("@yfcbarra", _yfcbarra))
        _listParam.Add(New Datos.DParametro("@yfcdprod1", _yfcdprod1))
        _listParam.Add(New Datos.DParametro("@yfcdprod2", _yfcdprod2))
        _listParam.Add(New Datos.DParametro("@yfgr1", _yfgr1))
        _listParam.Add(New Datos.DParametro("@yfgr2", _yfgr2))
        _listParam.Add(New Datos.DParametro("@yfgr3", _yfgr3))
        _listParam.Add(New Datos.DParametro("@yfgr4", _yfgr4))
        _listParam.Add(New Datos.DParametro("@yfMed", _yfMed))
        _listParam.Add(New Datos.DParametro("@yfumin", _yfumin))
        _listParam.Add(New Datos.DParametro("@yfusup", _yfusup))
        _listParam.Add(New Datos.DParametro("@yfvsup", _yfvsup))
        _listParam.Add(New Datos.DParametro("@yfmstk", 0))
        _listParam.Add(New Datos.DParametro("@yfclot", 0))
        _listParam.Add(New Datos.DParametro("@yfsmin", _yfsmin))
        _listParam.Add(New Datos.DParametro("@yfap", _yfap))
        _listParam.Add(New Datos.DParametro("@yfimg", _yfimg))
        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@yfdetpro", _yfdetpro))
        _listParam.Add(New Datos.DParametro("@yfgr5", _yfgr5))
        _listParam.Add(New Datos.DParametro("@yfcampo1", _yfprefijo))
        _listParam.Add(New Datos.DParametro("@yfcampo2", _yfconv2))
        _listParam.Add(New Datos.DParametro("@yfcampo3", _yfconv3))
        _listParam.Add(New Datos.DParametro("@yfresponsable", "NADIE"))
        _listParam.Add(New Datos.DParametro("@yflado", "S/L"))
        _listParam.Add(New Datos.DParametro("@yfordenacion", 0))
        _listParam.Add(New Datos.DParametro("@yfucompra", _yfucompra))
        _listParam.Add(New Datos.DParametro("@yfcampo4", _canje))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))

        _listParam.Add(New Datos.DParametro("@ycodact", _ycodact))
        _listParam.Add(New Datos.DParametro("@ygcodu", _ycodu))
        _listParam.Add(New Datos.DParametro("@ycodprosin", _ycodprosin))
        _listParam.Add(New Datos.DParametro("@ypreciosif", _ypreciosif))

        _listParam.Add(New Datos.DParametro("@TY0051", "", TY0051))
        _listParam.Add(New Datos.DParametro("@TY0053", "", TY0053))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _yfnumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnModificarProducto(ByRef _yfnumi As String, _yfcprod As String, _yfcbarra As String, _yfcdprod1 As String,
                                                 _yfcdprod2 As String, _yfgr1 As Integer, _yfgr2 As Integer, _yfgr3 As Integer, _yfgr4 As Integer,
                                                 _yfMed As Integer, _yfumin As Integer, _yfusup As Integer, _yfvsup As Double, _yfsmin As Integer,
                                                 _yfap As Integer, _yfimg As String, TY0051 As DataTable, _yfdetpro As String, _yfgr5 As String,
                                                 _ycodact As String, _ycodu As Integer, _ycodprosin As String, _ypreciosif As Double,
                                                 _yfprefijo As String, _yfconv2 As Double, _yfconv3 As Double, _yfucompra As Integer,
                                                 TY0053 As DataTable, _canje As String, _version As String, _ip As String, _usumaquina As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@yfnumi", _yfnumi))
        _listParam.Add(New Datos.DParametro("@yfcprod", _yfcprod))
        _listParam.Add(New Datos.DParametro("@yfcbarra", _yfcbarra))
        _listParam.Add(New Datos.DParametro("@yfcdprod1", _yfcdprod1))
        _listParam.Add(New Datos.DParametro("@yfcdprod2", _yfcdprod2))
        _listParam.Add(New Datos.DParametro("@yfgr1", _yfgr1))
        _listParam.Add(New Datos.DParametro("@yfgr2", _yfgr2))
        _listParam.Add(New Datos.DParametro("@yfgr3", _yfgr3))
        _listParam.Add(New Datos.DParametro("@yfgr4", _yfgr4))
        _listParam.Add(New Datos.DParametro("@yfMed", _yfMed))
        _listParam.Add(New Datos.DParametro("@yfumin", _yfumin))
        _listParam.Add(New Datos.DParametro("@yfusup", _yfusup))
        _listParam.Add(New Datos.DParametro("@yfvsup", _yfvsup))
        _listParam.Add(New Datos.DParametro("@yfmstk", 0))
        _listParam.Add(New Datos.DParametro("@yfclot", 0))
        _listParam.Add(New Datos.DParametro("@yfsmin", _yfsmin))
        _listParam.Add(New Datos.DParametro("@yfap", _yfap))
        _listParam.Add(New Datos.DParametro("@yfimg", _yfimg))
        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@yfdetpro", _yfdetpro))
        _listParam.Add(New Datos.DParametro("@yfgr5", _yfgr5))
        _listParam.Add(New Datos.DParametro("@yfcampo1", _yfprefijo))
        _listParam.Add(New Datos.DParametro("@yfcampo2", _yfconv2))
        _listParam.Add(New Datos.DParametro("@yfcampo3", _yfconv3))
        _listParam.Add(New Datos.DParametro("@yfucompra", _yfucompra))
        _listParam.Add(New Datos.DParametro("@yfcampo4", _canje))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))

        _listParam.Add(New Datos.DParametro("@ycodact", _ycodact))
        _listParam.Add(New Datos.DParametro("@ygcodu", _ycodu))
        _listParam.Add(New Datos.DParametro("@ycodprosin", _ycodprosin))
        _listParam.Add(New Datos.DParametro("@ypreciosif", _ypreciosif))

        _listParam.Add(New Datos.DParametro("@TY0051", "", TY0051))
        _listParam.Add(New Datos.DParametro("@TY0053", "", TY0053))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _yfnumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnGeneralProductos(mostrar As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@mostrar", mostrar))
        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnNameLabel() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnNameReporte() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 61))
        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prLibreriaClienteLGeneral(cod1 As Integer, cod2 As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@ylcod1", cod1))
        _listParam.Add(New Datos.DParametro("@ylcod2", cod2))
        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        Return _Tabla
    End Function


    Public Shared Function L_prListarBanco(cod1 As Integer, cod2 As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 15))
        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        Return _Tabla
    End Function



    Public Shared Function L_prLibreriaClienteLGeneralZonas() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prLibreriaClienteLGeneralMeses() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 10))
        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prLibreriaClienteLGeneralFrecVisita() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prLibreriaClienteLGeneralPrecios() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 8))
        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnDetalleProducto(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 11))
        _listParam.Add(New Datos.DParametro("@yfnumi", _numi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnCodigoBarra() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 12))
        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnCodigoBarraUno(yfnumi As String) As DataTable

        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 13))
        _listParam.Add(New Datos.DParametro("@yfnumi", yfnumi))
        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnBuscarProductoConversion(_Numi As String) As DataTable
        Dim _Tabla As DataTable
        Dim _Where As String = "yfnumi = " + _Numi + " ORDER BY yfnumi"
        _Tabla = D_Datos_Tabla("*", "TY005", _Where)
        Return _Tabla
    End Function
    Public Shared Function L_fnValidarCodBarras(_codBarras As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 16))
        _listParam.Add(New Datos.DParametro("@yfcbarra", _codBarras))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnConsultarNumi() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 17))
        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnActualizarProductoTY0052(_yfnumi As String, _Prod As DataTable, _version As String, _ip As String,
                                                        _usumaquina As String) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 18))
        _listParam.Add(New Datos.DParametro("@TY0052", "", _Prod))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _yfnumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_ProdQueNoEntraronXCompra() As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 19))
        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)
        Return _Tabla
    End Function

    Public Shared Function L_fnModificarProductoConteo(ByRef _yfnumi As String, _responsable As String, _lado As String,
                                                       _orden As Integer, _version As String, _ip As String, _usumaquina As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 20))
        _listParam.Add(New Datos.DParametro("@yfnumi", _yfnumi))
        _listParam.Add(New Datos.DParametro("@yfresponsable", _responsable))
        _listParam.Add(New Datos.DParametro("@yflado", _lado))
        _listParam.Add(New Datos.DParametro("@yfordenacion", _orden))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _yfnumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnGeneralProductosConteo() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 21))
        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnGeneralProductosConteoTodos() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 22))
        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnActualizarProductoTY005(_yfnumi As String, _Prod As DataTable, _version As String,
                                                       _ip As String, _usumaquina As String) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 23))
        _listParam.Add(New Datos.DParametro("@TY005", "", _Prod))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _yfnumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnExcelProductos(_version As String, _ip As String, _usumaquina As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 24))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnExcelProductosConteoIndividual(_version As String, _ip As String, _usumaquina As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 25))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnExcelProductosConteoLote(_version As String, _ip As String, _usumaquina As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 26))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnValidarDescripcionLibrerias(_ylcod1 As String, _ylcod2 As String, _desc As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 27))
        _listParam.Add(New Datos.DParametro("@ylcod1", _ylcod1))
        _listParam.Add(New Datos.DParametro("@ylcod2", _ylcod2))
        _listParam.Add(New Datos.DParametro("@desc", _desc))
        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        Return _Tabla
    End Function

#End Region

#Region "TY004 CLIENTES"


    Public Shared Function L_fnEliminarClientes(numi As String, ByRef mensaje As String, _version As String, _ip As String,
                                               _usumaquina As String, _tipo As Integer) As Boolean
        Dim _resultado As Boolean
        If L_fnbValidarEliminacion(numi, "TY004", "ydnumi", mensaje) = True Then
            Dim _Tabla As DataTable
            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@ydnumi", numi))
            _listParam.Add(New Datos.DParametro("@version", _version))
            _listParam.Add(New Datos.DParametro("@ip", _ip))
            _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
            _listParam.Add(New Datos.DParametro("@ydtip", _tipo))
            _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_Mam_TY004", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If
        Return _resultado
    End Function
    Public Shared Function L_fnEliminarProveedores(numi As String, ByRef mensaje As String, _version As String, _ip As String, _usumaquina As String) As Boolean
        Dim _resultado As Boolean
        If L_fnbValidarEliminacion(numi, "TY004", "ydnumi", mensaje) = True Then
            Dim _Tabla As DataTable
            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -2))
            _listParam.Add(New Datos.DParametro("@ydnumi", numi))
            _listParam.Add(New Datos.DParametro("@version", _version))
            _listParam.Add(New Datos.DParametro("@ip", _ip))
            _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
            _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_Mam_TY004", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If
        Return _resultado
    End Function
    Public Shared Function L_fnEliminarClientesConDetalleZona(numi As String, ByRef mensaje As String, _version As String, _ip As String,
                                                              _usumaquina As String) As Boolean
        Dim _resultado As Boolean
        If L_fnbValidarEliminacion(numi, "TY004", "ydnumi", mensaje) = True Then
            Dim _Tabla As DataTable
            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", 7))
            _listParam.Add(New Datos.DParametro("@ydnumi", numi))
            _listParam.Add(New Datos.DParametro("@version", _version))
            _listParam.Add(New Datos.DParametro("@ip", _ip))
            _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
            _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_Mam_TY004", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If
        Return _resultado
    End Function


    Public Shared Function L_fnGrabarCLiente(ByRef _ydnumi As String,
                                             _ydcod As String, _ydrazonsocial As String, _yddesc As String,
                                             _ydnumiVendedor As Integer, _ydzona As Integer, _yddct As Integer,
                                             _yddctnum As String, _yddirec As String, _ydtelf1 As String,
                                             _ydtelf2 As String, _ydcat As Integer, _ydest As Integer,
                                             _ydlat As Double, _ydlongi As Double, _ydobs As String,
                                             _ydfnac As String, _ydnomfac As String, _ydtip As Integer,
                                             _ydnit As String, _yddias As String, _ydlcred As String,
                                             _ydfecing As String, _ydultvent As String, _ydimg As String, _ydrut As String,
                                             _ydcorreo As String, _tipoDoc As String, _version As String, _ip As String,
                                             _usumaquina As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@ydnumi", _ydnumi))
        _listParam.Add(New Datos.DParametro("@ydcod", _ydcod))
        _listParam.Add(New Datos.DParametro("@ydrazonsocioal", _ydrazonsocial))
        _listParam.Add(New Datos.DParametro("@yddesc", _yddesc))
        _listParam.Add(New Datos.DParametro("@ydnumivend", _ydnumiVendedor))
        _listParam.Add(New Datos.DParametro("@ydzona", _ydzona))
        _listParam.Add(New Datos.DParametro("@yddct", _yddct))
        _listParam.Add(New Datos.DParametro("@yddctnum", _yddctnum))
        _listParam.Add(New Datos.DParametro("@yddirec", _yddirec))
        _listParam.Add(New Datos.DParametro("@ydtelf1", _ydtelf1))
        _listParam.Add(New Datos.DParametro("@ydtelf2", _ydtelf2))
        _listParam.Add(New Datos.DParametro("@ydcat", _ydcat))
        _listParam.Add(New Datos.DParametro("@ydest", _ydest))
        _listParam.Add(New Datos.DParametro("@ydlat", _ydlat))
        _listParam.Add(New Datos.DParametro("@ydlongi", _ydlongi))
        _listParam.Add(New Datos.DParametro("@ydprconsu", 0))
        _listParam.Add(New Datos.DParametro("@ydobs", _ydobs))
        _listParam.Add(New Datos.DParametro("@ydfnac", _ydfnac))
        _listParam.Add(New Datos.DParametro("@ydnomfac", _ydnomfac))
        _listParam.Add(New Datos.DParametro("@ydtip", _ydtip))
        _listParam.Add(New Datos.DParametro("@ydnit", _ydnit))
        _listParam.Add(New Datos.DParametro("@yddias", _yddias))
        _listParam.Add(New Datos.DParametro("@ydlcred", _ydlcred))
        _listParam.Add(New Datos.DParametro("@ydfecing", _ydfecing))
        _listParam.Add(New Datos.DParametro("@ydultvent", _ydultvent))
        _listParam.Add(New Datos.DParametro("@ydimg", _ydimg))
        _listParam.Add(New Datos.DParametro("@ydrut", _ydrut))
        _listParam.Add(New Datos.DParametro("@ydcorreo", _ydcorreo))
        _listParam.Add(New Datos.DParametro("@tipoDoc", _tipoDoc))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY004", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _ydnumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function


    Public Shared Function L_fnGrabarCLienteConDetalleZonas(ByRef _ydnumi As String, _ydcod As String, _yddesc As String, _ydnumiVendedor As Integer,
                                                            _ydzona As Integer, _yddct As Integer, _yddctnum As String, _yddirec As String,
                                                            _ydtelf1 As String, _ydtelf2 As String, _ydcat As Integer, _ydest As Integer,
                                                            _ydlat As Double, _ydlongi As Double, _ydobs As String, _ydfnac As String,
                                                            _ydnomfac As String, _ydtip As Integer, _ydnit As String, _ydfecing As String,
                                                            _ydultvent As String, _ydimg As String, _detalle As DataTable,
                                                            _version As String, _ip As String, _usumaquina As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@ydnumi", _ydnumi))
        _listParam.Add(New Datos.DParametro("@ydcod", _ydcod))
        _listParam.Add(New Datos.DParametro("@yddesc", _yddesc))
        _listParam.Add(New Datos.DParametro("@ydnumivend", _ydnumiVendedor))
        _listParam.Add(New Datos.DParametro("@ydzona", _ydzona))
        _listParam.Add(New Datos.DParametro("@yddct", _yddct))
        _listParam.Add(New Datos.DParametro("@yddctnum", _yddctnum))
        _listParam.Add(New Datos.DParametro("@yddirec", _yddirec))
        _listParam.Add(New Datos.DParametro("@ydtelf1", _ydtelf1))
        _listParam.Add(New Datos.DParametro("@ydtelf2", _ydtelf2))
        _listParam.Add(New Datos.DParametro("@ydcat", _ydcat))
        _listParam.Add(New Datos.DParametro("@ydest", _ydest))
        _listParam.Add(New Datos.DParametro("@ydlat", _ydlat))
        _listParam.Add(New Datos.DParametro("@ydlongi", _ydlongi))
        _listParam.Add(New Datos.DParametro("@ydprconsu", 0))
        _listParam.Add(New Datos.DParametro("@ydobs", _ydobs))
        _listParam.Add(New Datos.DParametro("@ydfnac", _ydfnac))
        _listParam.Add(New Datos.DParametro("@ydnomfac", _ydnomfac))
        _listParam.Add(New Datos.DParametro("@ydtip", _ydtip))
        _listParam.Add(New Datos.DParametro("@ydnit", _ydnit))
        _listParam.Add(New Datos.DParametro("@ydfecing", _ydfecing))
        _listParam.Add(New Datos.DParametro("@ydultvent", _ydultvent))
        _listParam.Add(New Datos.DParametro("@ydimg", _ydimg))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@TZ0013", "", _detalle))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY004", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _ydnumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnModificarClientesConDetalleZonas(ByRef _ydnumi As String, _ydcod As String, _yddesc As String, _ydnumiVendedor As Integer,
                                            _ydzona As Integer, _yddct As Integer, _yddctnum As String, _yddirec As String, _ydtelf1 As String,
                                            _ydtelf2 As String, _ydcat As Integer, _ydest As Integer, _ydlat As Double, _ydlongi As Double, _ydobs As String,
                                            _ydfnac As String, _ydnomfac As String, _ydtip As Integer, _ydnit As String, _ydfecing As String,
                                            _ydultvent As String, _ydimg As String, _detalle As DataTable, _version As String, _ip As String,
                                            _usumaquina As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@ydnumi", _ydnumi))
        _listParam.Add(New Datos.DParametro("@ydcod", _ydcod))
        _listParam.Add(New Datos.DParametro("@yddesc", _yddesc))
        _listParam.Add(New Datos.DParametro("@ydnumivend", _ydnumiVendedor))
        _listParam.Add(New Datos.DParametro("@ydzona", _ydzona))
        _listParam.Add(New Datos.DParametro("@yddct", _yddct))
        _listParam.Add(New Datos.DParametro("@yddctnum", _yddctnum))
        _listParam.Add(New Datos.DParametro("@yddirec", _yddirec))
        _listParam.Add(New Datos.DParametro("@ydtelf1", _ydtelf1))
        _listParam.Add(New Datos.DParametro("@ydtelf2", _ydtelf2))
        _listParam.Add(New Datos.DParametro("@ydcat", _ydcat))
        _listParam.Add(New Datos.DParametro("@ydest", _ydest))
        _listParam.Add(New Datos.DParametro("@ydlat", _ydlat))
        _listParam.Add(New Datos.DParametro("@ydlongi", _ydlongi))
        _listParam.Add(New Datos.DParametro("@ydprconsu", 0))
        _listParam.Add(New Datos.DParametro("@ydobs", _ydobs))
        _listParam.Add(New Datos.DParametro("@ydfnac", _ydfnac))
        _listParam.Add(New Datos.DParametro("@ydnomfac", _ydnomfac))
        _listParam.Add(New Datos.DParametro("@ydtip", _ydtip))
        _listParam.Add(New Datos.DParametro("@ydnit", _ydnit))
        _listParam.Add(New Datos.DParametro("@ydfecing", _ydfecing))
        _listParam.Add(New Datos.DParametro("@ydultvent", _ydultvent))
        _listParam.Add(New Datos.DParametro("@ydimg", _ydimg))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@TZ0013", "", _detalle))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY004", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _ydnumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnModificarClientes(ByRef _ydnumi As String, _ydcod As String, _ydrazonSocial As String,
                                                 _yddesc As String, _ydnumiVendedor As Integer, _ydzona As Integer,
                                                _yddct As Integer, _yddctnum As String, _yddirec As String, _ydtelf1 As String,
                                                _ydtelf2 As String, _ydcat As Integer, _ydest As Integer, _ydlat As Double,
                                                _ydlongi As Double, _ydobs As String, _ydfnac As String, _ydnomfac As String,
                                                _ydtip As Integer, _ydnit As String, _yddias As String, _ydlcred As String,
                                                _ydfecing As String, _ydultvent As String, _ydimg As String, _ydrut As String,
                                                _version As String, _ip As String, _usumaquina As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@ydnumi", _ydnumi))
        _listParam.Add(New Datos.DParametro("@ydcod", _ydcod))
        _listParam.Add(New Datos.DParametro("@ydrazonsocioal", _ydrazonSocial))
        _listParam.Add(New Datos.DParametro("@yddesc", _yddesc))
        _listParam.Add(New Datos.DParametro("@ydnumivend", _ydnumiVendedor))
        _listParam.Add(New Datos.DParametro("@ydzona", _ydzona))
        _listParam.Add(New Datos.DParametro("@yddct", _yddct))
        _listParam.Add(New Datos.DParametro("@yddctnum", _yddctnum))
        _listParam.Add(New Datos.DParametro("@yddirec", _yddirec))
        _listParam.Add(New Datos.DParametro("@ydtelf1", _ydtelf1))
        _listParam.Add(New Datos.DParametro("@ydtelf2", _ydtelf2))
        _listParam.Add(New Datos.DParametro("@ydcat", _ydcat))
        _listParam.Add(New Datos.DParametro("@ydest", _ydest))
        _listParam.Add(New Datos.DParametro("@ydlat", _ydlat))
        _listParam.Add(New Datos.DParametro("@ydlongi", _ydlongi))
        _listParam.Add(New Datos.DParametro("@ydprconsu", 0))
        _listParam.Add(New Datos.DParametro("@ydobs", _ydobs))
        _listParam.Add(New Datos.DParametro("@ydfnac", _ydfnac))
        _listParam.Add(New Datos.DParametro("@ydnomfac", _ydnomfac))
        _listParam.Add(New Datos.DParametro("@ydtip", _ydtip))
        _listParam.Add(New Datos.DParametro("@ydnit", _ydnit))
        _listParam.Add(New Datos.DParametro("@yddias", _yddias))
        _listParam.Add(New Datos.DParametro("@ydlcred", _ydlcred))
        _listParam.Add(New Datos.DParametro("@ydfecing", _ydfecing))
        _listParam.Add(New Datos.DParametro("@ydultvent", _ydultvent))
        _listParam.Add(New Datos.DParametro("@ydimg", _ydimg))
        _listParam.Add(New Datos.DParametro("@ydrut", _ydrut))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TY004", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _ydnumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnGeneralClientes(tipo As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@ydtip", tipo))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY004", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnReporteRutasClientes(_numiVendedor As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 10))
        _listParam.Add(New Datos.DParametro("@ydnumi", _numiVendedor))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY004", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnReporteZonasVendedor(_numiVendedor As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 11))
        _listParam.Add(New Datos.DParametro("@ydnumi", _numiVendedor))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY004", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prMapaCLienteGeneral() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 8))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY004", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prMapaCLienteGeneralPorZona(_zona As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@ydzona", _zona))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY004", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnObtenerDetalleZonas(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@ydnumi", _numi))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY004", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnObtenerClientesporRazonSocialNit(_RazonSocial As String, _Nit As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 12))
        _listParam.Add(New Datos.DParametro("@ydrazonsocioal", _RazonSocial))
        _listParam.Add(New Datos.DParametro("@ydnit", _Nit))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY004", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_ClientesCel(tipo As String) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 13))
        _listParam.Add(New Datos.DParametro("@tipoCliente", tipo))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY004", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_fnExcelClientes(_version As String, _ip As String, _usumaquina As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 14))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY004", _listParam)

        Return _Tabla
    End Function
#End Region

#Region "TY006 Categorias"

    Public Shared Function L_fnGeneralProgramas() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@yguact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY006", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnGeneralProgramasCategorias(numicat As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 10))
        _listParam.Add(New Datos.DParametro("@yguact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@categoria", numicat))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY006", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnGeneralDetalleLibrerias(cod1 As String, cod2 As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 11))
        _listParam.Add(New Datos.DParametro("@yguact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@cod1", cod1))
        _listParam.Add(New Datos.DParametro("@cod2", cod2))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY006", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnGeneralCategorias() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@yguact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY006", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnGeneralSucursales() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@yguact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY006", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarProductos() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@yguact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY006", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarLotesPorProductoVenta(_almacen As Integer, _codproducto As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 8))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@producto", _codproducto))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarCategorias() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@yguact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY006", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnListarProductosConPrecios(_almacen As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@yguact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY006", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnGrabarCategorias(_ygnumi As String, cod As String, desc As String, tipo As Integer, margen As Decimal) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@ygcod", cod))
        _listParam.Add(New Datos.DParametro("@ygdesc", desc))
        _listParam.Add(New Datos.DParametro("@ygpcv", tipo))
        _listParam.Add(New Datos.DParametro("@ygmer", margen))
        _listParam.Add(New Datos.DParametro("@yguact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY006", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _ygnumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnGrabarPrecios(_ygnumi As String, _almacen As Integer, _precio As DataTable, _version As String,
                                             _ip As String, _usumaquina As String) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@TY007", "", _precio))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@yguact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY006", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _ygnumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnGrabarLibreriasPrograma(_ygnumi As String, _dt As DataTable) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 12))
        _listParam.Add(New Datos.DParametro("@TY0031", "", _dt))
        _listParam.Add(New Datos.DParametro("@yguact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY006", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _ygnumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnListarProductosParaActualizarPrecios() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 14))
        _listParam.Add(New Datos.DParametro("@yguact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY006", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnActualizarPreciosEnLote(_version As String, _ip As String, _usumaquina As String) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 15))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@yguact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY006", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_PrecioVentaMenorPrecioCosto(estado As String, tipoPrecio As Integer) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 16))
        _listParam.Add(New Datos.DParametro("@estado", estado))
        _listParam.Add(New Datos.DParametro("@tipoPrecio", tipoPrecio))
        _listParam.Add(New Datos.DParametro("@yguact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY006", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_fnExcelPrecios(_version As String, _ip As String, _usumaquina As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 17))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@yguact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY006", _listParam)

        Return _Tabla
    End Function
#End Region

#Region "TZ001 Zonas"
    Public Shared Function L_fnGeneralZona() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@zauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TZ001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarPuntosPorZona(_zanumi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@zanumi", _zanumi))
        _listParam.Add(New Datos.DParametro("@zauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TZ001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnGrabarZona(_zanumi As String, _zaciudad As Integer, _zaprovincia As Integer, _zazona As Integer, _zacolor As String, point As DataTable) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        '@zanumi,@zacity ,@zaprovi ,@zazona ,@zacolor   ,@newFecha,@newHora,@zauact
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@zanumi", _zanumi))
        _listParam.Add(New Datos.DParametro("@zacity", _zaciudad))
        _listParam.Add(New Datos.DParametro("@zaprovi", _zaprovincia))
        _listParam.Add(New Datos.DParametro("@zazona", _zazona))
        _listParam.Add(New Datos.DParametro("@zacolor", _zacolor))
        _listParam.Add(New Datos.DParametro("@zauact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@Tz0012", "", point))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TZ001", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _zanumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnModificaZona(_zanumi As String, _zaciudad As Integer, _zaprovincia As Integer, _zazona As Integer, _zacolor As String, point As DataTable) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@zanumi", _zanumi))
        _listParam.Add(New Datos.DParametro("@zacity", _zaciudad))
        _listParam.Add(New Datos.DParametro("@zaprovi", _zaprovincia))
        _listParam.Add(New Datos.DParametro("@zazona", _zazona))
        _listParam.Add(New Datos.DParametro("@zacolor", _zacolor))
        _listParam.Add(New Datos.DParametro("@zauact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@Tz0012", "", point))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TZ001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _zanumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnEliminarZona(numi As String, ByRef mensaje As String) As Boolean
        Dim _resultado As Boolean
        If L_fnbValidarEliminacion(numi, "TZ001", "zanumi", mensaje) = True Then
            Dim _Tabla As DataTable
            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@zanumi", numi))
            _listParam.Add(New Datos.DParametro("@zauact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_Mam_TZ001", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If
        Return _resultado
    End Function
#End Region

#Region "TV001 Ventas"
    Public Shared Function L_fnGeneralVenta(_mostrar As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@mostrar", _mostrar))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnGeneralVentaServicios() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("VentaServicio", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnDetalleVenta(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@tanumi", _numi))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnEliminarDescuento(Id As String, _codProd As Integer, _version As String, _ip As String, _usumaquina As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", -1))
        _listParam.Add(New Datos.DParametro("@Id", Id))
        _listParam.Add(New Datos.DParametro("@ProductoId", _codProd))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@Usuario", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Descuentos", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnEliminarCategoriaProveedorDescuento(Id As String, _codProv As Integer, _version As String, _ip As String, _usumaquina As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", -1))
        _listParam.Add(New Datos.DParametro("@Id", Id))
        _listParam.Add(New Datos.DParametro("@ProveedorID", _codProv))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@Usuario", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Descuento_Proveedor", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnListarProductosDescuentos() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@Usuario", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Descuentos", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnListarCategoriaProveedorDescuento() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@Usuario", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Descuento_Proveedor", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnListarDescuentos(ProductoId As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@Usuario", L_Usuario))
        _listParam.Add(New Datos.DParametro("@ProductoId", ProductoId))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Descuentos", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarDescuentosXProveedor(ProveedorId As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@Usuario", L_Usuario))
        _listParam.Add(New Datos.DParametro("@ProveedorID", ProveedorId))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Descuento_Proveedor", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnListarDescuentosTodos() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@Usuario", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Descuentos", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnExcelDescuentos(_version As String, _ip As String, _usumaquina As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@Usuario", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Descuentos", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnGrabarPreciosDescuentos(ByRef numi As String, codpro As String, desde As Integer, hasta As Integer,
                                                       precio As Double, _version As String, _ip As String, _usumaquina As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@ProductoId", codpro))
        _listParam.Add(New Datos.DParametro("@CantidadInicial", desde))
        _listParam.Add(New Datos.DParametro("@CantidadFinal", hasta))
        _listParam.Add(New Datos.DParametro("@Precio", precio))
        _listParam.Add(New Datos.DParametro("@FechaInicial", Now.Date.ToString("yyyy/MM/dd")))
        _listParam.Add(New Datos.DParametro("@FechaFinal", Now.Date.ToString("yyyy/MM/dd")))
        _listParam.Add(New Datos.DParametro("@Estado", 1))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@usuario", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Descuentos", _listParam)

        If _Tabla.Rows.Count > 0 Then
            numi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnGrabarCategoriaProveedorDescuentos(ByRef numi As String, codProveedor As String, desde As Integer, hasta As Integer,
                                                                  porcentaje As Double, estadoDescricion As String, estado As Integer,
                                                                  _version As String, _ip As String, _usumaquina As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@ProveedorID", codProveedor))
        _listParam.Add(New Datos.DParametro("@MontoInicial", desde))
        _listParam.Add(New Datos.DParametro("@MontoFinal", hasta))
        _listParam.Add(New Datos.DParametro("@DescuentoPorcentaje", porcentaje))
        _listParam.Add(New Datos.DParametro("@EstadoDescripcion", estadoDescricion))
        _listParam.Add(New Datos.DParametro("@FechaFin", Now))
        _listParam.Add(New Datos.DParametro("@Estado", estado))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@usuario", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Descuento_Proveedor", _listParam)

        If _Tabla.Rows.Count > 0 Then
            numi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnDetalleVentaServicios(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@tanumi", _numi))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("VentaServicio", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnVentaNotaDeVenta(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 10))
        _listParam.Add(New Datos.DParametro("@tanumi", _numi))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnVentaNotaDeVentaServicios(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@tanumi", _numi))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("VentaServicio", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnVentaFactura(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 13))
        _listParam.Add(New Datos.DParametro("@tanumi", _numi))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnListarProductos(_almacen As String, _cliente As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@cliente", _cliente))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Marco_TV001", _listParam)

        Return _Tabla
    End Function


    'funcion para llenar los datos de la grilla en la venta
    Public Shared Function L_fnListarProductosC(_yfcBarra As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 11))
        _listParam.Add(New Datos.DParametro("@yfcbarra", _yfcBarra))
        '_listParam.Add(New Datos.DParametro("@cliente", _cliente))
        '_listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        '_listParam.Add(New Datos.DParametro("@TV0011", "", _detalle))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)



        Return _Tabla

    End Function

    Public Shared Function L_fnListarProductosSinLote(_almacen As String, _cliente As String, _detalle As DataTable) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@cliente", _cliente))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        '_listParam.Add(New Datos.DParametro("@TV0011", "", _detalle))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)


        Return _Tabla
    End Function

    Public Shared Function L_fnListarProductosSinLoteUlt(_almacen As String, _cliente As String, _detalle As DataTable, _canje As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@cliente", _cliente))
        _listParam.Add(New Datos.DParametro("@canje", _canje))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("BuscarProd", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnListarProductosSinLoteUltProforma(_almacen As String, _cliente As String, _detalle As DataTable) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 10))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@cliente", _cliente))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("BuscarProdProforma", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnListarProductosSinLoteUltimaProforma(_almacen As String, _cliente As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 11))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@cliente", _cliente))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("BuscarProdProforma", _listParam)

        Return _Tabla
    End Function

    'Public Shared Function L_fnListarProductosSinLoteUltimaProforma(_almacen As String, _cliente As String) As DataTable
    '    Dim _Tabla As DataTable

    '    Dim _listParam As New List(Of Datos.DParametro)

    '    _listParam.Add(New Datos.DParametro("@tipo", 13))
    '    _listParam.Add(New Datos.DParametro("@almacen", _almacen))
    '    _listParam.Add(New Datos.DParametro("@cliente", _cliente))
    '    _listParam.Add(New Datos.DParametro("@pauact", L_Usuario))
    '    _Tabla = D_ProcedimientoConParam("sp_Mam_TP001", _listParam)

    '    Return _Tabla
    'End Function

    Public Shared Function L_fnListarProductosSinLoteNuevo(_almacen As String, _cliente As String, _detalle As DataTable) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@cliente", _cliente))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Marco_TV001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarClientes() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarProforma() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 11))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarProductoProforma(_panumi As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 12))
        _listParam.Add(New Datos.DParametro("@panumi", _panumi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarEmpleado() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarClientesUsuario(idUsuario As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 15))
        _listParam.Add(New Datos.DParametro("@ydNumiUsu", idUsuario))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        Return _Tabla
    End Function


    Public Shared Function L_fnGrabarVenta(ByRef _tanumi As String, _taidCorelativo As String, _tafdoc As String,
                                           _taven As Integer, _tatven As Integer, _tafvcr As String, _taclpr As Integer,
                                           _tamon As Integer, _taobs As String, _tadesc As Double, _taice As Double,
                                           _tatotal As Double, detalle As DataTable, _almacen As Integer, _taprforma As Integer,
                                           Monto As DataTable, _NroCaja As Integer, _programa As String, _Nit As String,
                                           _Rsocial As String, _Correo As String, _TipoDoc As String, _actualizar As Integer,
                                           _Complemento As String, _cel As String, _Nfac As String, _NAutoriz As String,
                                           _CodCli As String, _A As String, _B As String,
                                           _C As String, _D As String, _E As String, _F As String, _G As String, _H As String,
                                           _qrurl As String, _facturl As String, _2leyenda As String, _3leyenda As String,
                                           _cufd As String, _anhio As String, _FacturaEmite As String, _version As String,
                                           _ip As String, _usumaquina As String, _canje As Integer, _codigoficha As String) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@tanumi", _tanumi))
        _listParam.Add(New Datos.DParametro("@taproforma", _taprforma))
        _listParam.Add(New Datos.DParametro("@taidCore", _taidCorelativo))
        _listParam.Add(New Datos.DParametro("@taalm", _almacen))
        _listParam.Add(New Datos.DParametro("@tafdoc", _tafdoc))
        _listParam.Add(New Datos.DParametro("@taven", _taven))
        _listParam.Add(New Datos.DParametro("@tatven", _tatven))
        _listParam.Add(New Datos.DParametro("@tafvcr", _tafvcr))
        _listParam.Add(New Datos.DParametro("@taclpr", _taclpr))
        _listParam.Add(New Datos.DParametro("@tamon", _tamon))
        _listParam.Add(New Datos.DParametro("@taest", 1))
        _listParam.Add(New Datos.DParametro("@taobs", _taobs))
        _listParam.Add(New Datos.DParametro("@tadesc", _tadesc))
        _listParam.Add(New Datos.DParametro("@taice", _taice))
        _listParam.Add(New Datos.DParametro("@tatotal", _tatotal))
        _listParam.Add(New Datos.DParametro("@taNrocaja", _NroCaja))
        _listParam.Add(New Datos.DParametro("@bcprograma", _programa))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@Nit", _Nit))
        _listParam.Add(New Datos.DParametro("@correo", _Correo))
        _listParam.Add(New Datos.DParametro("@tipoDoc", _TipoDoc))
        _listParam.Add(New Datos.DParametro("@Rsocial", _Rsocial))
        _listParam.Add(New Datos.DParametro("@actualizar", _actualizar))
        _listParam.Add(New Datos.DParametro("@complemento", _Complemento))
        _listParam.Add(New Datos.DParametro("@FacturaEmite", _FacturaEmite))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@canje", _canje))
        _listParam.Add(New Datos.DParametro("@codficha", _codigoficha))
        ''Datos Factura
        _listParam.Add(New Datos.DParametro("@Nfac", _Nfac))
        _listParam.Add(New Datos.DParametro("@NAutoriz", _NAutoriz))
        _listParam.Add(New Datos.DParametro("@CodCli", _CodCli))
        _listParam.Add(New Datos.DParametro("@A", _A))
        _listParam.Add(New Datos.DParametro("@B", _B))
        _listParam.Add(New Datos.DParametro("@C", _C))
        _listParam.Add(New Datos.DParametro("@D", _D))
        _listParam.Add(New Datos.DParametro("@E", _E))
        _listParam.Add(New Datos.DParametro("@F", _F))
        _listParam.Add(New Datos.DParametro("@G", _G))
        _listParam.Add(New Datos.DParametro("@H", _H))
        _listParam.Add(New Datos.DParametro("@qrurl", _qrurl))
        _listParam.Add(New Datos.DParametro("@facturl", _facturl))
        _listParam.Add(New Datos.DParametro("@2leyenda", _2leyenda))
        _listParam.Add(New Datos.DParametro("@3leyenda", _3leyenda))
        _listParam.Add(New Datos.DParametro("@cufd", _cufd))
        _listParam.Add(New Datos.DParametro("@anhio", _anhio))


        _listParam.Add(New Datos.DParametro("@TV0011", "", detalle))
        _listParam.Add(New Datos.DParametro("@TV0014", "", Monto))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _tanumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnGrabarVentaServicios(ByRef _tanumi As String, _tafdoc As String, _taven As Integer, _tatven As Integer, _tafvcr As String, _taclpr As Integer,
                                           _tamon As Integer, _taobs As String,
                                           _tadesc As Double, _taice As Double,
                                           _tatotal As Double, detalle As DataTable, _almacen As Integer) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        '  (@tanumi,@taalm,@tafdoc ,@taven  ,@tatven,
        '@tafvcr ,@taclpr,@tamon ,@taest  ,@taobs ,@tadesc, @taice ,@tatotal ,@newFecha,@newHora,@tauact)
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@tanumi", _tanumi))

        _listParam.Add(New Datos.DParametro("@taalm", _almacen))
        _listParam.Add(New Datos.DParametro("@tafdoc", _tafdoc))
        _listParam.Add(New Datos.DParametro("@taven", _taven))
        _listParam.Add(New Datos.DParametro("@tatven", _tatven))
        _listParam.Add(New Datos.DParametro("@tafvcr", _tafvcr))
        _listParam.Add(New Datos.DParametro("@taclpr", _taclpr))
        _listParam.Add(New Datos.DParametro("@tamon", _tamon))
        _listParam.Add(New Datos.DParametro("@taest", 1))
        _listParam.Add(New Datos.DParametro("@taobs", _taobs))
        _listParam.Add(New Datos.DParametro("@tadesc", _tadesc))
        _listParam.Add(New Datos.DParametro("@taice", _taice))
        _listParam.Add(New Datos.DParametro("@tatotal", _tatotal))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@TV0011", "", detalle))
        _Tabla = D_ProcedimientoConParam("VentaServicio", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _tanumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnModificarVentaServicios(ByRef _tanumi As String, _tafdoc As String, _taven As Integer, _tatven As Integer, _tafvcr As String, _taclpr As Integer,
                                           _tamon As Integer, _taobs As String,
                                           _tadesc As Double, _taice As Double,
                                           _tatotal As Double, detalle As DataTable, _almacen As Integer) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        '  (@tanumi,@taalm,@tafdoc ,@taven  ,@tatven,
        '@tafvcr ,@taclpr,@tamon ,@taest  ,@taobs ,@tadesc, @taice ,@tatotal ,@newFecha,@newHora,@tauact)
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@tanumi", _tanumi))

        _listParam.Add(New Datos.DParametro("@taalm", _almacen))
        _listParam.Add(New Datos.DParametro("@tafdoc", _tafdoc))
        _listParam.Add(New Datos.DParametro("@taven", _taven))
        _listParam.Add(New Datos.DParametro("@tatven", _tatven))
        _listParam.Add(New Datos.DParametro("@tafvcr", _tafvcr))
        _listParam.Add(New Datos.DParametro("@taclpr", _taclpr))
        _listParam.Add(New Datos.DParametro("@tamon", _tamon))
        _listParam.Add(New Datos.DParametro("@taest", 1))
        _listParam.Add(New Datos.DParametro("@taobs", _taobs))
        _listParam.Add(New Datos.DParametro("@tadesc", _tadesc))
        _listParam.Add(New Datos.DParametro("@taice", _taice))
        _listParam.Add(New Datos.DParametro("@tatotal", _tatotal))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@TV0011", "", detalle))
        _Tabla = D_ProcedimientoConParam("VentaServicio", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _tanumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnModificarVenta(_tanumi As String, _tafdoc As String, _taven As Integer, _tatven As Integer, _tafvcr As String, _taclpr As Integer,
                                           _tamon As Integer, _taobs As String,
                                           _tadesc As Double, _taice As Double, _tatotal As Double, detalle As DataTable, _almacen As Integer, _taprforma As Integer, monto As DataTable) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@tanumi", _tanumi))
        _listParam.Add(New Datos.DParametro("@taalm", _almacen))
        _listParam.Add(New Datos.DParametro("@taproforma", _taprforma))
        _listParam.Add(New Datos.DParametro("@tafdoc", _tafdoc))
        _listParam.Add(New Datos.DParametro("@taven", _taven))
        _listParam.Add(New Datos.DParametro("@tatven", _tatven))
        _listParam.Add(New Datos.DParametro("@tafvcr", _tafvcr))
        _listParam.Add(New Datos.DParametro("@taclpr", _taclpr))
        _listParam.Add(New Datos.DParametro("@tamon", _tamon))
        _listParam.Add(New Datos.DParametro("@taest", 1))
        _listParam.Add(New Datos.DParametro("@taobs", _taobs))
        _listParam.Add(New Datos.DParametro("@tadesc", _tadesc))
        _listParam.Add(New Datos.DParametro("@taice", _taice))
        _listParam.Add(New Datos.DParametro("@tatotal", _tatotal))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@TV0011", "", detalle))
        _listParam.Add(New Datos.DParametro("@TV0014", "", monto))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _tanumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnEliminarVenta(numi As String, ByRef mensaje As String, programa As String, _version As String, _ip As String,
                                             _usumaquina As String, NroFact As String) As Boolean
        Dim _resultado As Boolean
        If L_fnbValidarEliminacion(numi, "TV001", "tanumi", mensaje) = True Then
            Dim _Tabla As DataTable
            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@tanumi", numi))
            _listParam.Add(New Datos.DParametro("@bcprograma", programa))
            _listParam.Add(New Datos.DParametro("@version", _version))
            _listParam.Add(New Datos.DParametro("@ip", _ip))
            _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
            _listParam.Add(New Datos.DParametro("@Nfac", NroFact))
            _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
            _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If
        Return _resultado
    End Function

    Public Shared Function L_fnInsertarFacturaAnuladaSifac(fecha As String, ncaja As Integer, nfact As Integer,
                                                           autoriz As String, estado As String) As DataTable

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 25))
        _listParam.Add(New Datos.DParametro("@tafdoc", fecha))
        _listParam.Add(New Datos.DParametro("@taNrocaja", ncaja))
        _listParam.Add(New Datos.DParametro("@nrofact", nfact))
        _listParam.Add(New Datos.DParametro("@nroautoriz", autoriz))
        _listParam.Add(New Datos.DParametro("@estado", estado))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnMostrarMontos(tanumi As Integer) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 14))
        _listParam.Add(New Datos.DParametro("@tanumi", tanumi))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)
        Return _Tabla
    End Function

    Public Shared Function L_fnVerificarPagos(numi As String) As Boolean
        Dim _resultado As Boolean
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 16))
        _listParam.Add(New Datos.DParametro("@tanumi", numi))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
        Else
            _resultado = False
        End If
        Return _resultado
    End Function

    Public Shared Function L_fnVerificarSiSeContabilizoVenta(_canumi As String) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 17))
        _listParam.Add(New Datos.DParametro("@tanumi", _canumi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnListarClientesVenta() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 18))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnVerificarCierreCaja(numi As String, tipo As String) As Boolean
        Dim _resultado As Boolean
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 19))
        _listParam.Add(New Datos.DParametro("@tanumi", numi))
        _listParam.Add(New Datos.DParametro("@tipoTCC0013", tipo))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
        Else
            _resultado = False
        End If
        Return _resultado
    End Function
    Public Shared Function L_fnObtenerMaxFact(_nrocaja As Integer, _anhio As Integer) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 22))
        _listParam.Add(New Datos.DParametro("@taNrocaja", _nrocaja))
        _listParam.Add(New Datos.DParametro("@anhio", _anhio))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_fnMostrarMontos2(tanumi As Integer) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 23))
        _listParam.Add(New Datos.DParametro("@tanumi", tanumi))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_fnRecuperarFactura(tanumi As Integer) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 24))
        _listParam.Add(New Datos.DParametro("@tanumi", tanumi))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)
        Return _Tabla
    End Function

    Public Shared Function L_prBitacora(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 25))
        _listParam.Add(New Datos.DParametro("@vcnumi", _numi))
        _listParam.Add(New Datos.DParametro("@vcuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TV002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarClientesVentaPrecioEspecial() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 26))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarClientesVentaPrecioPDV() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 27))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_ProductosVendidosConVales(fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 28))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_fnMostrarCanje(tanumi As Integer) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 29))
        _listParam.Add(New Datos.DParametro("@tanumi", tanumi))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_fnListarClientesVentaPrecioEspecialPDV() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 30))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        Return _Tabla
    End Function
#End Region

#Region "TC001 Compras"
    Public Shared Function L_fnGeneralCompras(mostrar As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@mostrar", mostrar))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnDetalleCompra(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@canumi", _numi))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnDetalleCompraTFC001(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 10))
        _listParam.Add(New Datos.DParametro("@canumi", _numi))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnListarProductosCompra(_almacen As String, _catCosto As String) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@CatCosto", _catCosto))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Marco_TC001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarProductosCompraNueva(_almacen As String, _catCosto As String, _codProv As Integer) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@CatCosto", _catCosto))
        _listParam.Add(New Datos.DParametro("@yfgr1", _codProv))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Marco_TC001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarProductosPesaje(_almacen As String, _catCosto As String) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@CatCosto", _catCosto))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Marco_TC001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarProveedores() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarSucursales() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarDepositos() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 24))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnPorcUtilidad() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 8))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnGrabarCompra(_canumi As String, _caalm As Integer, _cafdoc As String, _caTy4prov As Integer, _catven As Integer,
                                            _cafvcr As String, _camon As Integer, _caobs As String, _caSubtotal As Double, _cadesc As Double,
                                            _cadescpro As Double, _caice As Double, _catotal As Double, detalle As DataTable, detalleCompra As DataTable,
                                            _emision As Integer, _numemision As Integer, _consigna As Integer, _retenc As Integer, _tipocambio As Double,
                                            _version As String, _ip As String, _usumaquina As String, dtActualizar As DataTable) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        '   @canumi ,@caalm,@cafdoc ,@caty4prov  ,@catven,
        '@cafvcr,@camon ,@caest  ,@caobs ,@cadesc ,@newFecha,@newHora,@cauact
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@canumi", _canumi))
        _listParam.Add(New Datos.DParametro("@caalm", _caalm))
        _listParam.Add(New Datos.DParametro("@cafdoc", _cafdoc))
        _listParam.Add(New Datos.DParametro("@caty4prov", _caTy4prov))
        _listParam.Add(New Datos.DParametro("@catven", _catven))
        _listParam.Add(New Datos.DParametro("@cafvcr", _cafvcr))
        _listParam.Add(New Datos.DParametro("@camon", _camon))
        _listParam.Add(New Datos.DParametro("@caest", 1))
        _listParam.Add(New Datos.DParametro("@caobs", _caobs))
        _listParam.Add(New Datos.DParametro("@casubtot", _caSubtotal))
        _listParam.Add(New Datos.DParametro("@cadesc", _cadesc))
        _listParam.Add(New Datos.DParametro("@cadescpro", _cadescpro))
        _listParam.Add(New Datos.DParametro("@caice", _caice))
        _listParam.Add(New Datos.DParametro("@catotal", _catotal))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@caemision", _emision))
        _listParam.Add(New Datos.DParametro("@canumemis", _numemision))
        _listParam.Add(New Datos.DParametro("@caconsigna", _consigna))
        _listParam.Add(New Datos.DParametro("@caretenc", _retenc))
        _listParam.Add(New Datos.DParametro("@catipocambio", _tipocambio))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))

        _listParam.Add(New Datos.DParametro("@TC0011", "", detalle))
        _listParam.Add(New Datos.DParametro("@TFC001", "", detalleCompra))
        _listParam.Add(New Datos.DParametro("@TC0011Act", "", dtActualizar))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _canumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_prCompraComprobanteGeneralPorNumi(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@canumi", _numi))
        ' _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnModificarCompra(_canumi As String, _caalm As Integer, _cafdoc As String, _caTy4prov As Integer, _catven As Integer,
                                               _cafvcr As String, _camon As Integer, _caobs As String, _caSubtotal As Double, _cadesc As Double,
                                               _cadescpro As Double, _caice As Double, _catotal As Double, detalle As DataTable, detalleCompra As DataTable,
                                               _emision As Integer, _numemision As Integer, _consigna As Integer, _retenc As Integer,
                                               _tipocambio As Double, _version As String, _ip As String, _usumaquina As String, dtActualizar As DataTable) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@canumi", _canumi))
        _listParam.Add(New Datos.DParametro("@caalm", _caalm))
        _listParam.Add(New Datos.DParametro("@cafdoc", _cafdoc))
        _listParam.Add(New Datos.DParametro("@caty4prov", _caTy4prov))
        _listParam.Add(New Datos.DParametro("@catven", _catven))
        _listParam.Add(New Datos.DParametro("@cafvcr", _cafvcr))
        _listParam.Add(New Datos.DParametro("@camon", _camon))
        _listParam.Add(New Datos.DParametro("@caest", 1))
        _listParam.Add(New Datos.DParametro("@caobs", _caobs))
        _listParam.Add(New Datos.DParametro("@casubtot", _caSubtotal))
        _listParam.Add(New Datos.DParametro("@cadesc", _cadesc))
        _listParam.Add(New Datos.DParametro("@cadescpro", _cadescpro))
        _listParam.Add(New Datos.DParametro("@caice", _caice))
        _listParam.Add(New Datos.DParametro("@catotal", _catotal))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@caemision", _emision))
        _listParam.Add(New Datos.DParametro("@canumemis", _numemision))
        _listParam.Add(New Datos.DParametro("@caconsigna", _consigna))
        _listParam.Add(New Datos.DParametro("@caretenc", _retenc))
        _listParam.Add(New Datos.DParametro("@catipocambio", _tipocambio))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))

        _listParam.Add(New Datos.DParametro("@TC0011", "", detalle))
        _listParam.Add(New Datos.DParametro("@TFC001", "", detalleCompra))
        _listParam.Add(New Datos.DParametro("@TC0011Act", "", dtActualizar))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _canumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnEliminarCompra(numi As String, ByRef mensaje As String, _version As String, _ip As String, _usumaquina As String) As Boolean
        Dim _resultado As Boolean
        If L_fnbValidarEliminacion(numi, "TC001", "canumi", mensaje) = True Then
            Dim _Tabla As DataTable
            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@canumi", numi))
            _listParam.Add(New Datos.DParametro("@version", _version))
            _listParam.Add(New Datos.DParametro("@ip", _ip))
            _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
            _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
            _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If
        Return _resultado
    End Function

    Public Shared Function L_fnVerificarSiSeContabilizo(_canumi As String) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 10))
        _listParam.Add(New Datos.DParametro("@canumi", _canumi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnVerificarPagosCompras(numi As String) As Boolean
        Dim _resultado As Boolean
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 11))
        _listParam.Add(New Datos.DParametro("@canumi", numi))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
        Else
            _resultado = False
        End If
        Return _resultado
    End Function
    Public Shared Function L_fnNotaCompras(_canumi As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 12))
        _listParam.Add(New Datos.DParametro("@canumi", _canumi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_ComprasDetalladas(fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 13))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_fnListarProveedoresNueva() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 14))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnMostrarComprasSinActualizarPrecios() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 15))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnMostrarDetalleCompraSinActualizarPrecios(almacen As String, CodCompra As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 16))
        _listParam.Add(New Datos.DParametro("@almacen", almacen))
        _listParam.Add(New Datos.DParametro("@canumi", CodCompra))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnActualizarTC0011a(_numi As String, _Prod As DataTable, _version As String, _ip As String,
                                                 _usumaquina As String) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 17))
        _listParam.Add(New Datos.DParametro("@canumi", _numi))
        _listParam.Add(New Datos.DParametro("@TC0011a", "", _Prod))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _numi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnDetalleCompra2(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 18))
        _listParam.Add(New Datos.DParametro("@canumi", _numi))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnMostrarSubidaPreciosTodasLasCompras() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 19))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnImpresionCompra(_version As String, _ip As String, _usumaquina As String, _codPro As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 20))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@canumi", _codPro))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnExcelCompra(_version As String, _ip As String, _usumaquina As String, _cod As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 21))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@canumi", _cod))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnMovProdxPeso(_version As String, _ip As String, _usumaquina As String, _cod As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 22))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@canumi", _cod))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnExcelUnaCompra(_version As String, _ip As String, _usumaquina As String, _cod As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 23))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@canumi", _cod))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)

        Return _Tabla
    End Function


    Public Shared Function L_fnArmarTablaProdDiferentesPrecioCosto(detalle As DataTable) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 24))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@TC0011", "", detalle))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)


        Return _Tabla
    End Function
    Public Shared Function L_fnVerificarComprasUnProducto(_cod As String, _fecha As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 25))
        _listParam.Add(New Datos.DParametro("@codPro", _cod))
        _listParam.Add(New Datos.DParametro("@fechaI", _fecha))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnProveedorConsignacion(_prov As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 26))
        _listParam.Add(New Datos.DParametro("@proveedor", _prov))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prListarEstadoCuentasCompraTotal(idProveedor As Integer, fechai As String) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@prov", idProveedor))
        _listParam.Add(New Datos.DParametro("@fechai", fechai))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_EstadoCuentas", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_prListarEstadoCuentasCompra(idProveedor As Integer, fechai As String, fechaf As String) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@prov", idProveedor))
        _listParam.Add(New Datos.DParametro("@fechai", fechai))
        _listParam.Add(New Datos.DParametro("@fechaf", fechaf))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_EstadoCuentas", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_fnEliminarCategoria(numi As String, ByRef mensaje As String) As Boolean
        Dim _resultado As Boolean
        If L_fnbValidarEliminacion(numi, "TY006", "ygnumi", mensaje) = True Then
            Dim _Tabla As DataTable
            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@ygnumi", numi))
            _listParam.Add(New Datos.DParametro("@yguact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_Mam_TY006", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If
        Return _resultado
    End Function
#End Region

#Region "TA002 Deposito"
    Public Shared Function L_fnEliminarDeposito(numi As String, ByRef mensaje As String) As Boolean
        Dim _resultado As Boolean
        If L_fnbValidarEliminacion(numi, "TA002", "abnumi", mensaje) = True Then
            Dim _Tabla As DataTable
            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@abnumi", numi))
            _listParam.Add(New Datos.DParametro("@abuact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_Mam_TA002", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If
        Return _resultado
    End Function



    Public Shared Function L_fnGrabarDeposito(ByRef _abnumi As String, _abdesc As String, _abdir As String, _abtelf As String, _ablat As Double, _ablong As Double, _abimg As String, _abest As Integer) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        '@abnumi ,@abdesc ,@abdir ,@abtelf ,@ablat ,@ablong,@abimg ,@abest ,@newFecha,@newHora,@abuact
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@abnumi", _abnumi))
        _listParam.Add(New Datos.DParametro("@abdesc", _abdesc))
        _listParam.Add(New Datos.DParametro("@abdir", _abdir))
        _listParam.Add(New Datos.DParametro("@abtelf", _abtelf))
        _listParam.Add(New Datos.DParametro("@ablat", _ablat))
        _listParam.Add(New Datos.DParametro("@ablong", _ablong))
        _listParam.Add(New Datos.DParametro("@abimg", _abimg))
        _listParam.Add(New Datos.DParametro("@abest", _abest))


        _listParam.Add(New Datos.DParametro("@abuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TA002", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _abnumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnModificarDepositos(ByRef _abnumi As String, _abdesc As String, _abdir As String, _abtelf As String, _ablat As Double, _ablong As Double, _abimg As String, _abest As Integer) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@abnumi", _abnumi))
        _listParam.Add(New Datos.DParametro("@abdesc", _abdesc))
        _listParam.Add(New Datos.DParametro("@abdir", _abdir))
        _listParam.Add(New Datos.DParametro("@abtelf", _abtelf))
        _listParam.Add(New Datos.DParametro("@ablat", _ablat))
        _listParam.Add(New Datos.DParametro("@ablong", _ablong))
        _listParam.Add(New Datos.DParametro("@abimg", _abimg))
        _listParam.Add(New Datos.DParametro("@abest", _abest))

        _listParam.Add(New Datos.DParametro("@abuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TA002", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _abnumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnGeneralDepositos() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@abuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TA002", _listParam)

        Return _Tabla
    End Function



#End Region

#Region "TA001 Almacen"
    Public Shared Function L_fnEliminarAlmacen(numi As String, ByRef mensaje As String) As Boolean
        Dim _resultado As Boolean
        If L_fnbValidarEliminacion(numi, "TA001", "abnumi", mensaje) = True Then
            Dim _Tabla As DataTable
            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@aanumi", numi))
            _listParam.Add(New Datos.DParametro("@aauact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_Mam_TA001", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If
        Return _resultado
    End Function

    Public Shared Function L_fnEliminarServicio(numi As String, ByRef mensaje As String) As Boolean
        Dim _resultado As Boolean
        Dim _Tabla As DataTable
            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
        _listParam.Add(New Datos.DParametro("@Id", numi))
        _listParam.Add(New Datos.DParametro("@aauact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_Servicios", _listParam)

        If _Tabla.Rows.Count > 0 Then
                _resultado = True
            Else
                _resultado = False
            End If

            Return _resultado
    End Function



    Public Shared Function L_fnGrabarAlmacen(ByRef _abnumi As String, _aata2dep As Integer, _aata2depVenta As Integer, _abdesc As String, _abdir As String, _abtelf As String, _ablat As Double, _ablong As Double, _abimg As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        '@aanumi ,@aata2dep,@aadesc ,@aadir ,@aatelf ,@aalat ,@aalong,@aaimg ,@newFecha,@newHora,@aauact

        'a.aanumi ,a.aabdes ,a.aadir ,a.aatel ,a.aalat ,a.aalong ,a.aaimg,aata2dep ,a.aafact ,a.aahact ,a.aauact
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@aanumi", _abnumi))
        _listParam.Add(New Datos.DParametro("@aata2dep", _aata2dep))
        _listParam.Add(New Datos.DParametro("@aata2depVenta", _aata2depVenta))
        _listParam.Add(New Datos.DParametro("@aadesc", _abdesc))
        _listParam.Add(New Datos.DParametro("@aadir", _abdir))
        _listParam.Add(New Datos.DParametro("@aatelf", _abtelf))
        _listParam.Add(New Datos.DParametro("@aalat", _ablat))
        _listParam.Add(New Datos.DParametro("@aalong", _ablong))
        _listParam.Add(New Datos.DParametro("@aaimg", _abimg))



        _listParam.Add(New Datos.DParametro("@aauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TA001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _abnumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnGrabarServicio(ByRef id As String, NombreServicio As String, DetalleServicio As String, Estado As Integer) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        '@Id,@NombreServicio ,@DetalleServicio ,@Estado  ,@newFecha,@aauact
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@Id", id))
        _listParam.Add(New Datos.DParametro("@NombreServicio", NombreServicio))
        _listParam.Add(New Datos.DParametro("@DetalleServicio", DetalleServicio))
        _listParam.Add(New Datos.DParametro("@Estado", Estado))
        _listParam.Add(New Datos.DParametro("@aauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Servicios", _listParam)

        If _Tabla.Rows.Count > 0 Then
            id = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnModificarServicio(ByRef id As String, NombreServicio As String, DetalleServicio As String, Estado As Integer) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        '@Id,@NombreServicio ,@DetalleServicio ,@Estado  ,@newFecha,@aauact
        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@Id", id))
        _listParam.Add(New Datos.DParametro("@NombreServicio", NombreServicio))
        _listParam.Add(New Datos.DParametro("@DetalleServicio", DetalleServicio))
        _listParam.Add(New Datos.DParametro("@Estado", Estado))
        _listParam.Add(New Datos.DParametro("@aauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Servicios", _listParam)

        If _Tabla.Rows.Count > 0 Then
            id = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function




    Public Shared Function L_fnModificarAlmacen(ByRef _abnumi As String, _aata2dep As Integer, _aata2depVenta As Integer, _abdesc As String, _abdir As String, _abtelf As String, _ablat As Double, _ablong As Double, _abimg As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@aanumi", _abnumi))
        _listParam.Add(New Datos.DParametro("@aata2dep", _aata2dep))
        _listParam.Add(New Datos.DParametro("@aata2depVenta", _aata2depVenta))
        _listParam.Add(New Datos.DParametro("@aadesc", _abdesc))
        _listParam.Add(New Datos.DParametro("@aadir", _abdir))
        _listParam.Add(New Datos.DParametro("@aatelf", _abtelf))
        _listParam.Add(New Datos.DParametro("@aalat", _ablat))
        _listParam.Add(New Datos.DParametro("@aalong", _ablong))
        _listParam.Add(New Datos.DParametro("@aaimg", _abimg))

        _listParam.Add(New Datos.DParametro("@aauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TA001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _abnumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnGeneralAlmacen() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@aauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TA001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnGeneralServicios() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@aauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Servicios", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarDeposito() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@aauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TA001", _listParam)

        Return _Tabla
    End Function


#End Region

#Region "TS002 Dosificacion"

    Public Shared Function L_fnEliminarDosificacion(numi As String, ByRef mensaje As String) As Boolean
        Dim _resultado As Boolean
        If L_fnbValidarEliminacion(numi, "TS002", "sbnumi", mensaje) = True Then
            Dim _Tabla As DataTable
            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@numi", numi))
            _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_go_TS002", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If
        Return _resultado
    End Function



    Public Shared Function L_fnGrabarDosificacion(ByRef numi As String, cia As Integer, alm As String, sfc As String,
                                                  autoriz As String, nfac As Double, key As String, fdel As String,
                                                  fal As String, nota As String, nota2 As String, est As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@numi", numi))
        _listParam.Add(New Datos.DParametro("@cia", cia))
        _listParam.Add(New Datos.DParametro("@alm", alm))
        _listParam.Add(New Datos.DParametro("@sfc", sfc))
        _listParam.Add(New Datos.DParametro("@autoriz", autoriz))
        _listParam.Add(New Datos.DParametro("@nfac", nfac))
        _listParam.Add(New Datos.DParametro("@key", key))
        _listParam.Add(New Datos.DParametro("@fdel", fdel))
        _listParam.Add(New Datos.DParametro("@fal", fal))
        _listParam.Add(New Datos.DParametro("@nota", nota))
        _listParam.Add(New Datos.DParametro("@nota2", nota2))
        _listParam.Add(New Datos.DParametro("@est", est))

        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_go_TS002", _listParam)

        If _Tabla.Rows.Count > 0 Then
            numi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnModificarDosificacion(ByRef numi As String, cia As Integer, alm As String, sfc As String,
                                                     autoriz As String, nfac As Double, key As String, fdel As String,
                                                     fal As String, nota As String, nota2 As String, est As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@numi", numi))
        _listParam.Add(New Datos.DParametro("@cia", cia))
        _listParam.Add(New Datos.DParametro("@alm", alm))
        _listParam.Add(New Datos.DParametro("@sfc", sfc))
        _listParam.Add(New Datos.DParametro("@autoriz", autoriz))
        _listParam.Add(New Datos.DParametro("@nfac", nfac))
        _listParam.Add(New Datos.DParametro("@key", key))
        _listParam.Add(New Datos.DParametro("@fdel", fdel))
        _listParam.Add(New Datos.DParametro("@fal", fal))
        _listParam.Add(New Datos.DParametro("@nota", nota))
        _listParam.Add(New Datos.DParametro("@nota2", nota2))
        _listParam.Add(New Datos.DParametro("@est", est))

        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_go_TS002", _listParam)

        If _Tabla.Rows.Count > 0 Then
            numi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnGeneralDosificacion() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_go_TS002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarCompaniaDosificacion() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_go_TS002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnListarAlmacenDosificacion() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_go_TS002", _listParam)

        Return _Tabla
    End Function

#End Region

#Region "Facturar"

    Public Shared Sub L_Grabar_Factura(_Numi As String, _Fecha As String, _Nfac As String, _NAutoriz As String, _Est As String,
                                       _NitCli As String, _CodCli As String, _DesCli1 As String, _DesCli2 As String,
                                       _A As String, _B As String, _C As String, _D As String, _E As String, _F As String,
                                       _G As String, _H As String, _CodCon As String, _FecLim As String,
                                       _Imgqr As String, _Alm As String, _Numi2 As String, _hora As String, _qrurl As String,
                                       _facturl As String, _2leyenda As String, _3leyenda As String, _cufd As String,
                                       _nrocaja As String, _anhio As String)
        Dim Sql As String
        Try
            Sql = "" + _Numi + ", " _
                + "'" + _Fecha + "', " _
                + "" + _Nfac + ", " _
                + "'" + _NAutoriz + "', " _
                + "" + _Est + ", " _
                + "'" + _NitCli + "', " _
                + "'" + _CodCli + "', " _
                + "'" + _DesCli1 + "', " _
                + "'" + _DesCli2 + "', " _
                + "" + _A + ", " _
                + "" + _B + ", " _
                + "" + _C + ", " _
                + "" + _D + ", " _
                + "" + _E + ", " _
                + "" + _F + ", " _
                + "" + _G + ", " _
                + "" + _H + ", " _
                + "'" + _CodCon + "', " _
                + "'" + _FecLim + "', " _
                + "" + _Imgqr + ", " _
                + "" + _Alm + ", " _
                + "" + _Numi2 + ", " _
                + "'" + _hora + "', " _
                + "'" + _qrurl + "', " _
                + "'" + _facturl + "', " _
                + "'" + _2leyenda + "', " _
                + "'" + _3leyenda + "', " _
                + "'" + _cufd + "', " _
                + "" + _nrocaja + ", " _
                + "" + _anhio + ""

            D_Insertar_Datos("TFV001", Sql)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Shared Sub L_Grabar_TPA001(_Numi As String, _Fecha As String, _Tipo As String, _CodCli As String, _Nomcli As String,
                                       _Emision As String, _Estado As String, _Total As String, _Moneda As String,
                                       _Tc As String, _Nscf As String, _NumAsiento As String)

        Dim Sql As String
        Try
            Sql = "" + _Numi + ", " _
                + "'" + _Fecha + "', " _
                + "" + _Tipo + ", " _
                + "" + _CodCli + ", " _
                + "'" + _Nomcli + "', " _
                + "" + _Emision + ", " _
                + "" + _Estado + ", " _
                + "" + _Total + ", " _
                + "" + _Moneda + ", " _
                + "" + _Tc + ", " _
                + "" + _Nscf + ", " _
                + "" + _NumAsiento + ""

            D_Insertar_Datos("BDDiconDinoCrex.dbo.TPA001", Sql)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Shared Sub L_Modificar_Factura(Where As String, Optional _Fecha As String = "",
                                          Optional _Nfact As String = "", Optional _NAutoriz As String = "",
                                          Optional _Est As String = "", Optional _NitCli As String = "",
                                          Optional _CodCli As String = "", Optional _DesCli1 As String = "",
                                          Optional _DesCli2 As String = "", Optional _A As String = "",
                                          Optional _B As String = "", Optional _C As String = "",
                                          Optional _D As String = "", Optional _E As String = "",
                                          Optional _F As String = "", Optional _G As String = "",
                                          Optional _H As String = "", Optional _CodCon As String = "",
                                          Optional _FecLim As String = "", Optional _Imgqr As String = "",
                                          Optional _Alm As String = "", Optional _Numi2 As String = "")
        Dim Sql As String
        Try
            Sql = IIf(_Fecha.Equals(""), "", "fvafec = '" + _Fecha + "', ") +
              IIf(_Nfact.Equals(""), "", "fvanfac = " + _Nfact + ", ") +
              IIf(_NAutoriz.Equals(""), "", "fvaautoriz = '" + _NAutoriz + "', ") +
              IIf(_Est.Equals(""), "", "fvaest = " + _Est) +
              IIf(_NitCli.Equals(""), "", "fvanitcli = '" + _NitCli + "', ") +
              IIf(_CodCli.Equals(""), "", "fvacodcli = " + _CodCli + ", ") +
              IIf(_DesCli1.Equals(""), "", "fvadescli1 = '" + _DesCli1 + "', ") +
              IIf(_DesCli2.Equals(""), "", "fvadescli2 = '" + _DesCli2 + "', ") +
              IIf(_A.Equals(""), "", "fvastot = " + _A + ", ") +
              IIf(_B.Equals(""), "", "fvaimpsi = " + _B + ", ") +
              IIf(_C.Equals(""), "", "fvaimpeo = " + _C + ", ") +
              IIf(_D.Equals(""), "", "fvaimptc = " + _D + ", ") +
              IIf(_E.Equals(""), "", "fvasubtotal = " + _E + ", ") +
              IIf(_F.Equals(""), "", "fvadesc = " + _F + ", ") +
              IIf(_G.Equals(""), "", "fvatotal = " + _G + ", ") +
              IIf(_H.Equals(""), "", "fvadebfis = " + _H + ", ") +
              IIf(_CodCon.Equals(""), "", "fvaccont = '" + _CodCon + "', ") +
              IIf(_FecLim.Equals(""), "", "fvaflim = '" + _FecLim + "', ") +
              IIf(_Imgqr.Equals(""), "", "fvaimgqr = '" + _Imgqr + "', ") +
              IIf(_Alm.Equals(""), "", "fvaalm = " + _Alm + ", ") +
              IIf(_Numi2.Equals(""), "", "fvanumi2 = " + _Numi2 + ", ")
            Sql = Sql.Trim
            If (Sql.Substring(Sql.Length - 1, 1).Equals(",")) Then
                Sql = Sql.Substring(0, Sql.Length - 1)
            End If

            D_Modificar_Datos("TFV001", Sql, Where)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Shared Sub L_Grabar_Factura_Detalle(_Numi As String, _CodProd As String, _DescProd As String, _Cant As String, _Precio As String, _Numi2 As String)
        Dim Sql As String
        Try
            Sql = _Numi + ", '" + _CodProd + "', '" + _DescProd + "', " + _Cant + ", " + _Precio + ", " + _Numi2

            D_Insertar_Datos("TFV0011", Sql)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Shared Function L_Reporte_Factura(_Numi As String, _Numi2 As String) As DataSet
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet
        Dim _Where As String
        _Where = " fvanumi = " + _Numi + " and fvanumi = " + _Numi2

        _Tabla = D_Datos_Tabla("*", "VR_GO_Factura", _Where)
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function

    Public Shared Function L_Reporte_FacturaNueva(_Numi As String, _Numi2 As String) As DataSet
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet
        Dim _Where As String
        _Where = " fvanumi = " + _Numi + " and fvanumi = " + _Numi2 + "  Order by tbnumi"

        _Tabla = D_Datos_Tabla("*", "VR_GO_Factura2", _Where)
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function

    ''VR_MAM_FacturaServicio
    Public Shared Function L_Reporte_FacturaServicio(_Numi As String, _Numi2 As String) As DataSet
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet
        Dim _Where As String
        _Where = " fvanumi = " + _Numi + " and fvanumi = " + _Numi2

        _Tabla = D_Datos_Tabla("*", "VR_MAM_FacturaServicio", _Where)
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function


    Public Shared Function L_Reporte_Factura_Cia(_Cia As String) As DataSet
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet
        Dim _Where As String
        _Where = " scnumi = " + _Cia

        _Tabla = D_Datos_Tabla("*", "TS003", _Where)
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function

    Public Shared Function L_ObtenerRutaImpresora(_NroImp As String, Optional tImp As String = "") As DataSet
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet
        Dim _Where As String
        If (Not _NroImp.Trim.Equals("")) Then
            _Where = " cbnumi = " + _NroImp + " and cbest = 1 order by cbnumi"
        Else
            _Where = " cbtimp = " + tImp + " and cbest = 1 order by cbnumi"
        End If
        _Tabla = D_Datos_Tabla("*", "TC002", _Where)
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function

    Public Shared Function L_fnGetIVA() As DataTable
        Dim _Tabla As DataTable
        Dim _Where As String
        _Where = "1 = 1"
        _Tabla = D_Datos_Tabla("scdebfis", "TS003", _Where)
        Return _Tabla
    End Function

    Public Shared Function L_fnGetICE() As DataTable
        Dim _Tabla As DataTable
        Dim _Where As String
        _Where = "1 = 1"
        _Tabla = D_Datos_Tabla("scice", "TS003", _Where)
        Return _Tabla
    End Function

    Public Shared Sub L_Grabar_Nit(_Nit As String, _Nom1 As String, _Nom2 As String, _TipoDoc As String,
                                   _Correo As String, _Complemento As String, _Cel As String)
        Dim _Err As Boolean
        Dim _Nom01, _Nom02, Correo, TipoDoc, Complem, cel As String
        Dim Sql As String
        _Nom01 = ""
        _Nom02 = ""
        Correo = ""
        TipoDoc = ""
        Complem = ""
        cel = ""
        L_Validar_Nit(_Nit, _Nom01, _Nom02, Correo, TipoDoc, "", _Complemento, cel)

        If _Nom01 = "" Then
            Sql = "'" + _Nit + "', '" + _Nom1 + "', '" + _Nom2 + "', '" + _TipoDoc + "','" + _Correo + "','" + _Complemento + "','" + _Cel + "'"
            _Err = D_Insertar_Datos("TS001", Sql)
        Else
            If (_Nom1 <> _Nom01 Or _Correo <> Correo Or _TipoDoc <> TipoDoc Or _Cel <> cel) Then
                Sql = "sanom1 = '" + _Nom1 + "' " + " , satipdoc = '" + _TipoDoc + "' " + " , sacorreo = '" + _Correo + "' " +
                       ", sanom2 = '" + _Nom2 + "' " + ", sacel = '" + _Cel + "' " + " , sacomplemento = '" + _Complemento + "' "
                _Err = D_Modificar_Datos("TS001", Sql, "sanit = '" + _Nit + "'" + " And sacomplemento = '" + _Complemento + "'")
            End If
        End If

    End Sub

    Public Shared Function L_Grabar_NitPrimero(_Nit As String, _Nom1 As String, _Nom2 As String, _TipoDoc As String,
                                               _Correo As String, _Complemento As String, _Cel As String) As Boolean
        Dim _Err As Boolean
        Dim resultado As Boolean
        Dim Sql As String

        Sql = "'" + _Nit + "', '" + _Nom1 + "', '" + _Nom2 + "', '" + _TipoDoc + "','" + _Correo + "','" + _Complemento + "','" + _Cel + "'"
        _Err = D_Insertar_Datos("TS001", Sql)
        If _Err = False Then
            resultado = True
        Else
            resultado = False
        End If
        Return resultado
    End Function
    Public Shared Sub L_Validar_Nit(_Nit As String, ByRef _Nom1 As String, ByRef _Nom2 As String,
                                    ByRef _Correo As String, ByRef _TipoDoc As String, ByRef _Id As String,
                                    ByRef _Complemento As String, ByRef _Cel As String)
        Dim _Tabla As DataTable

        _Tabla = D_Datos_Tabla("*", "TS001", "sanit = '" + _Nit + "'" + " And sacomplemento = '" + _Complemento + "'")

        If _Tabla.Rows.Count > 0 Then
            _Nom1 = _Tabla.Rows(0).Item(2)
            _Nom2 = IIf(_Tabla.Rows(0).Item(3).ToString.Trim.Equals(""), "", _Tabla.Rows(0).Item(3))
            _Correo = _Tabla.Rows(0).Item(5)
            _TipoDoc = _Tabla.Rows(0).Item(4)
            _Id = _Tabla.Rows(0).Item(0)
            _Complemento = _Tabla.Rows(0).Item(6)
            _Cel = _Tabla.Rows(0).Item(7)
        End If
    End Sub

    Public Shared Function L_Eliminar_Nit(_Nit As String) As Boolean
        Dim res As Boolean = False
        Try
            res = D_Eliminar_Datos("TS001", "sanit = " + _Nit)
        Catch ex As Exception
            res = False
        End Try
        Return res
    End Function

    Public Shared Sub L_Grabar_NitCompra(_Nit As String, _Nom1 As String, _CodProv As String)
        Dim _Err As Boolean
        Dim _Nom01 As String
        Dim _CodProv01 As Integer
        Dim Sql As String
        _Nom01 = ""
        _CodProv01 = 0

        L_Validar_NitCompra(_Nit, _Nom01, _CodProv)

        If _Nom01 = "" Then
            Sql = "'" + _Nit + "', '" + _Nom1 + "', " + _CodProv + ""
            _Err = D_Insertar_Datos("TS004", Sql)
        Else
            If (_Nom1 <> _Nom01) Then
                Sql = "sdnom1 = '" + _Nom1 + "' " + " , sdcodprov = " + _CodProv + ""
                _Err = D_Modificar_Datos("TS004", Sql, "sdnit = '" + _Nit + "'" + " And sdcodprov = " + _CodProv + "")
            End If
        End If


    End Sub
    Public Shared Sub L_Validar_NitCompra(_Nit As String, ByRef _Nom1 As String, ByRef _CodProv As String)
        Dim _Tabla As DataTable

        _Tabla = D_Datos_Tabla("*", "TS004", "sdnit = '" + _Nit + "'" + " And sdcodprov = " + _CodProv + "")

        If _Tabla.Rows.Count > 0 Then
            _Nom1 = _Tabla.Rows(0).Item(2)
            _CodProv = _Tabla.Rows(0).Item(3)

        End If
    End Sub

    Public Shared Function L_fnObtenerNitProv(_CodProv As String) As DataTable
        Dim _Tabla As DataTable
        _Tabla = D_Datos_Tabla("*", "TS004", " sdcodprov = " + _CodProv + "")
        Return _Tabla
    End Function

    Public Shared Function L_Dosificacion(_cia As String, _alm As String, _fecha As String) As DataSet
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet
        Dim _Where As String
        _fecha = Now.Date.ToString("yyyy/MM/dd")
        _Where = "sbcia = " + _cia + " AND sbalm = " + _alm + " AND sbfdel <= '" + _fecha + "' AND sbfal >= '" + _fecha + "' AND sbest = 1"

        _Tabla = D_Datos_Tabla("*", "TS002", _Where)
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function
    Public Shared Function L_DosificacionCajas(_cia As String, _alm As String, _fecha As String, _NroCaja As String) As DataSet
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet
        Dim _Where As String
        _fecha = Now.Date.ToString("yyyy/MM/dd")
        _Where = "sbcia = " + _cia + " AND sbalm = " + _alm + " AND sbsfc = " + _NroCaja + " AND sbfdel <= '" + _fecha + "' AND sbfal >= '" + _fecha + "' AND sbest = 1"

        _Tabla = D_Datos_Tabla("*", "TS002", _Where)
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function
    Public Shared Function L_DosificacionReImprimir(_cia As String, _alm As String, _fecha As String, _NroAut As String) As DataSet
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet
        Dim _Where As String
        _fecha = Now.Date.ToString("yyyy/MM/dd")
        _Where = "sbcia = " + _cia + " AND sbalm = " + _alm + " AND sbautoriz = " + _NroAut + " AND sbfdel <= '" + _fecha + "' AND sbfal >= '" + _fecha + "' "

        _Tabla = D_Datos_Tabla("*", "TS002", _Where)
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function

    Public Shared Sub L_Actualiza_Dosificacion(_Numi As String, _NumFac As String, _Numi2 As String)
        Dim _Err As Boolean
        Dim Sql, _where As String
        Sql = "sbnfac = " + _NumFac
        _where = "sbnumi = " + _Numi

        _Err = D_Modificar_Datos("TS002", Sql, _where)
    End Sub

    Public Shared Function L_fnObtenerMaxIdTabla(tabla As String, campo As String, where As String) As Long
        Dim Dt As DataTable = New DataTable
        Dt = D_Maximo(tabla, campo, where)

        If (Dt.Rows.Count > 0) Then
            If (Dt.Rows(0).Item(0).ToString.Equals("")) Then
                Return 0
            Else
                Return CLng(Dt.Rows(0).Item(0).ToString)
            End If
        Else
            Return 0
        End If
    End Function

    Public Shared Function L_fnObtenerTabla(tabla As String, campo As String, where As String) As DataTable
        Dim Dt As DataTable = D_Datos_Tabla(campo, tabla, where)
        Return Dt
    End Function

    Public Shared Function L_fnObtenerDatoTabla(tabla As String, campo As String, where As String) As String
        Dim Dt As DataTable = D_Datos_Tabla(campo, tabla, where)
        If (Dt.Rows.Count > 0) Then
            Return Dt.Rows(0).Item(campo).ToString
        Else
            Return ""
        End If
    End Function

    Public Shared Function L_fnEliminarDatos(Tabla As String, Where As String) As Boolean
        Return D_Eliminar_Datos(Tabla, Where)
    End Function

#End Region

#Region "Anular Factura"

    Public Shared Function L_Obtener_Facturas(mostrar As Boolean) As DataSet
        Dim _Tabla1 As New DataTable
        Dim _Tabla2 As New DataTable
        Dim _Ds As New DataSet
        Dim _Where As String

        'Cambiar la logica para visualizar las facturas esto en el programa de facturas
        If mostrar = True Then
            _Where = " 1 = 1 "
            _Tabla1 = D_Datos_Tabla("concat(fvanfac, '_', fvaautoriz) as Archivo, fvanumi as Codigo, fvanfac as [Nro Factura], " _
                         + "fvafec as Fecha, fvacodcli as [Cod Cliente], " _
                         + " fvadescli1 as [Nombre 1], fvadescli2 as [Nombre 2], fvanitcli as Nit, " _
                         + " fvastot as Subtotal, fvadesc as Descuento, fvatotal as Total, " _
                         + " fvaccont as [Cod Control], fvaflim as [Fec Limite], fvaest as Estado",
                         "TFV001", _Where)

            '_Tabla1 = L_FacturasTodas()
        Else
            _Where = " 1 = 1  Order By fvanumi desc"
            _Tabla1 = D_Datos_Tabla("Top(1500) concat(fvanfac, '_', fvaautoriz) as Archivo, fvanumi as Codigo, fvanfac as [Nro Factura], " _
                         + "fvafec as Fecha, fvacodcli as [Cod Cliente], " _
                         + " fvadescli1 as [Nombre 1], fvadescli2 as [Nombre 2], fvanitcli as Nit, " _
                         + " fvastot as Subtotal, fvadesc as Descuento, fvatotal as Total, " _
                         + " fvaccont as [Cod Control], fvaflim as [Fec Limite], fvaest as Estado",
                         "TFV001", _Where)

            '_Tabla1 = L_Facturas2500()

        End If

        Dim dtDetalle As DataTable = _Tabla1.DefaultView.ToTable(False, "Codigo", "Nro Factura")


        _Ds.Tables.Add(_Tabla1)

        '_Tabla2 = D_Datos_Tabla("concat(fvanfac, '_', fvaautoriz) as Archivo, fvbnumi as Codigo, fvbcprod as [Cod Producto], fvbdesprod as Descripcion, " _
        '                        + " fvbcant as Cantidad, fvbprecio as [Precio Unitario], (fvbcant * fvbprecio) as Precio",
        '                        "TFV001, TFV0011", "fvanumi = fvbnumi and fvanumi2 = fvbnumi2")

        '_Tabla2 = L_FacturasDet()
        _Tabla2 = L_FacturasDet2(dtDetalle)

        _Ds.Tables.Add(_Tabla2)
        _Ds.Relations.Add("1", _Tabla1.Columns("Archivo"), _Tabla2.Columns("Archivo"), False)
        Return _Ds
    End Function

    Public Shared Function L_Facturas2500() As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Sp_Mam_Facturas", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_FacturasTodas() As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Sp_Mam_Facturas", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_FacturasDet() As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Sp_Mam_Facturas", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_FacturasDet2(_detalle As DataTable) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@TFV001", "", _detalle))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Sp_Mam_Facturas", _listParam)
        Return _Tabla
    End Function

    Public Shared Function L_ObtenerDetalleFactura(_CodFact As String) As DataSet 'Modifcar para que solo Traiga los productos Con Stock
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet
        Dim _Where As String
        _Where = "fvbnumi = " + _CodFact
        _Tabla = D_Datos_Tabla("fvbcprod as codP, fvbcant as can, '1' as sto", "TFV0011", _Where)
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function

#End Region

#Region "Libro de ventas"
    Public Shared Function L_fnObtenerLibroVentaAmbosTipoFactura(_CodAlm As String, _fechai As String, _FechaF As String, TipoFactura As Integer) As DataTable
        Dim _Tabla As DataTable
        Dim _Where As String = ""

        If _CodAlm > 0 Then
            If (TipoFactura = 1) Then
                _Where = "sbcia=1 and fvaalm = " + _CodAlm + " and fvafec >= '" + _fechai + "' and fvafec <= '" + _FechaF + "' " + " ORDER BY fvanfac"

            End If
            If (TipoFactura = 2) Then
                _Where = "sbcia=2 and fvaalm = " + _CodAlm + " and fvafec >= '" + _fechai + "' and fvafec <= '" + _FechaF + "' " + " ORDER BY fvanfac"

            End If
            If (TipoFactura = 3) Then
                _Where = " fvaalm = " + _CodAlm + " and fvafec >= '" + _fechai + "' and fvafec <= '" + _FechaF + "' " + " ORDER BY fvanfac"

            End If
        End If
        If _CodAlm = 0 Then 'todas las sucursales
            If (TipoFactura = 1) Then
                _Where = "sbcia=1 and  fvafec >= '" + _fechai + "' and fvafec <= '" + _FechaF + "' " + " ORDER BY fvanfac"

            End If
            If (TipoFactura = 2) Then
                _Where = "sbcia=2 and  fvafec >= '" + _fechai + "' and fvafec <= '" + _FechaF + "' " + " ORDER BY fvanfac"

            End If
            If (TipoFactura = 3) Then
                _Where = " fvafec >= '" + _fechai + "' and fvafec <= '" + _FechaF + "' " + " ORDER BY fvanfac"

            End If


        End If
        If _CodAlm = -1 Then 'todas las sucursales menos la principal
            If (TipoFactura = 1) Then

                _Where = "sbcia=1 and fvaalm <>1 " + " and fvafec >= '" + _fechai + "' and fvafec <= '" + _FechaF + "' " + " ORDER BY fvanfac"

            End If
            If (TipoFactura = 2) Then
                _Where = "sbcia=2 and fvaalm <>1 " + " and fvafec >= '" + _fechai + "' and fvafec <= '" + _FechaF + "' " + " ORDER BY fvanfac"

            End If
            If (TipoFactura = 3) Then
                _Where = "fvaalm <>1 " + " and fvafec >= '" + _fechai + "' and fvafec <= '" + _FechaF + "' " + " ORDER BY fvanfac"

            End If


        End If

        Dim _select As String = "fvanumi, FORMAT(fvafec,'dd/MM/yyyy') as fvafec, fvanfac, fvaautoriz,fvaest, fvanitcli, fvadescli, fvastot, fvaimpsi, fvaimpeo, fvaimptc, fvasubtotal, fvadesc, fvatotal, fvadebfis, fvaccont,fvaflim,fvaalm,scneg, factura"

        _Tabla = D_Datos_Tabla(_select,
                               "VR_GO_LibroVenta2", _Where)
        Return _Tabla
    End Function
    Public Shared Function L_fnObtenerLibroVenta(_CodAlm As String, _Mes As String, _Anho As String) As DataTable
        Dim _Tabla As DataTable
        Dim _Where As String = "fvaalm = " + _CodAlm + "and Month(fvafec) = " + _Mes + " and Year(fvafec) = " + _Anho + " ORDER BY fvanfac"
        _Tabla = D_Datos_Tabla("*",
                               "VR_GO_LibroVenta", _Where)
        Return _Tabla
    End Function
    Public Shared Function L_fnObtenerLibroVenta2(_CodAlm As String, _fechai As String, _FechaF As String, factura As Integer, TipoFactura As Integer) As DataTable
        Dim _Tabla As DataTable
        Dim _Where As String = ""
        If _CodAlm > 0 Then
            If (TipoFactura = 1) Then

                _Where = " fvaalm = " + _CodAlm + " and fvafec >= '" + _fechai + "' and fvafec <= '" + _FechaF + "' and factura=" + Str(factura) + " ORDER BY fvanfac"

            End If
            If (TipoFactura = 2) Then
                _Where = " fvaalm = " + _CodAlm + " and fvafec >= '" + _fechai + "' and fvafec <= '" + _FechaF + "' and factura=" + Str(factura) + " ORDER BY fvanfac"

            End If
            If (TipoFactura = 3) Then
                _Where = " fvaalm = " + _CodAlm + " and fvafec >= '" + _fechai + "' and fvafec <= '" + _FechaF + "' and factura=" + Str(factura) + " ORDER BY fvanfac"

            End If



        End If
        If _CodAlm = 0 Then 'todas las sucursales

            If (TipoFactura = 1) Then


                _Where = " fvafec >= '" + _fechai + "' and fvafec <= '" + _FechaF + "' and factura=" + Str(factura) + " ORDER BY fvanfac"

            End If
            If (TipoFactura = 2) Then
                _Where = " fvafec >= '" + _fechai + "' and fvafec <= '" + _FechaF + "' and factura=" + Str(factura) + " ORDER BY fvanfac"

            End If
            If (TipoFactura = 3) Then
                _Where = "fvafec >= '" + _fechai + "' and fvafec <= '" + _FechaF + "' and factura=" + Str(factura) + " ORDER BY fvanfac"

            End If

        End If
        If _CodAlm = -1 Then 'todas las sucursales menos la principal
            If (TipoFactura = 1) Then


                _Where = " fvaalm <>1 " + " and fvafec >= '" + _fechai + "' and fvafec <= '" + _FechaF + "' and factura=" + Str(factura) + " ORDER BY fvanfac"
            End If
            If (TipoFactura = 2) Then
                _Where = " fvaalm <>1 " + " and fvafec >= '" + _fechai + "' and fvafec <= '" + _FechaF + "' and factura=" + Str(factura) + " ORDER BY fvanfac"

            End If
            If (TipoFactura = 3) Then
                _Where = "fvaalm <>1 " + " and fvafec >= '" + _fechai + "' and fvafec <= '" + _FechaF + "' and factura=" + Str(factura) + " ORDER BY fvanfac"

            End If


        End If
        Dim _select As String = "fvanumi, FORMAT(fvafec,'dd/MM/yyyy') as fvafec, fvanfac, fvaautoriz,fvaest, fvanitcli, fvadescli, fvastot, fvaimpsi, fvaimpeo, fvaimptc, fvasubtotal, fvadesc, fvatotal, fvadebfis, fvaccont,fvaflim,fvaalm, factura, fvanrocaja"

        _Tabla = D_Datos_Tabla(_select,
                               "VR_GO_LibroVenta2", _Where)
        Return _Tabla
    End Function

    Public Shared Function L_ObtenerAnhoFactura() As DataTable
        Dim _Tabla As DataTable
        Dim _Where As String = "1 = 1 ORDER BY year(fvafec)"
        _Tabla = D_Datos_Tabla("Distinct(year(fvafec)) AS anho",
                               "VR_GO_LibroVenta", _Where)
        Return _Tabla
    End Function

    Public Shared Function L_ObtenerSucursalesFactura() As DataTable
        Dim _Tabla As DataTable
        Dim _Where As String = "1 = 1 ORDER BY a.scneg"
        _Tabla = D_Datos_Tabla("a.scnumi, a.scneg, a.scnit",
                               "TS003 a", _Where)
        Return _Tabla
    End Function

#End Region

#Region "COBROS DE LAS VENTAS"
    Public Shared Function L_fnObtenerLasVentasACredito() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@tduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV00121", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnObtenerLosPagos(_numi As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@tduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@credito", _numi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV00121", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnObtenerLasVentasCreditoPorCliente(_numi As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 9))
        '_listParam.Add(New Datos.DParametro("@tduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@tenumi", _numi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV00121Cheque", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnObtenerLasVentasCreditoPorVendedorFecha(_numi As Integer, _fecha As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 11))
        '_listParam.Add(New Datos.DParametro("@tduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@tenumi", _numi))
        _listParam.Add(New Datos.DParametro("@tefdoc", _fecha))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV00121Cheque", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnGrabarPagos(_numi As String, dt As DataTable) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@tduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@tdnumi", _numi))
        _listParam.Add(New Datos.DParametro("@TV00121", "", dt))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV00121", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _numi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnEliminarPago(numi As String, ByRef mensaje As String) As Boolean
        Dim _resultado As Boolean
        If L_fnbValidarEliminacion(numi, "TV00121", "tdnumi", mensaje) = True Then
            Dim _Tabla As DataTable
            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@tdnumi", numi))
            _listParam.Add(New Datos.DParametro("@tduact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_Mam_TV00121", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If
        Return _resultado
    End Function
#End Region

#Region "COBROS DE LAS VENTAS CON CHEQUE"
    Public Shared Function L_fnCobranzasGeneral() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@teuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV00121Cheque", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnCobranzasObtenerLosPagos(_numi As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@teuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@tdnumi", _numi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV00121Cheque", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnCobranzasObtenerLasVentasACredito() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@teuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV00121Cheque", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnCobranzasDetalle(_numi As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@teuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@tenumi", _numi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV00121Cheque", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnCobranzasReporte(_numi As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 8))
        _listParam.Add(New Datos.DParametro("@teuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@tenumi", _numi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV00121Cheque", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnEliminarCobranza(numi As String, ByRef mensaje As String, _version As String, _ip As String, _usumaquina As String) As Boolean
        Dim _resultado As Boolean
        If L_fnbValidarEliminacion(numi, "TV0013", "tenumi", mensaje) = True Then
            Dim _Tabla As DataTable
            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@tenumi", numi))
            _listParam.Add(New Datos.DParametro("@version", _version))
            _listParam.Add(New Datos.DParametro("@ip", _ip))
            _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
            _listParam.Add(New Datos.DParametro("@teuact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_Mam_TV00121Cheque", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If
        Return _resultado
    End Function
    '@tenumi ,@tefdoc,@tety4vend ,@teobs ,@newFecha ,@newHora ,@teuact 
    Public Shared Function L_fnGrabarCobranza(_tenumi As String, _tefdoc As String, _tety4vend As Integer, _teobs As String, detalle As DataTable,
                                              _Nrocaja As Integer, _version As String, _ip As String, _usumaquina As String) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        '   @canumi ,@caalm,@cafdoc ,@caty4prov  ,@catven,
        '@cafvcr,@camon ,@caest  ,@caobs ,@cadesc ,@newFecha,@newHora,@cauact
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@tenumi", _tenumi))
        _listParam.Add(New Datos.DParametro("@tefdoc", _tefdoc))
        _listParam.Add(New Datos.DParametro("@tety4vend", _tety4vend))
        _listParam.Add(New Datos.DParametro("@teobs", _teobs))
        _listParam.Add(New Datos.DParametro("@teuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@teNrocaja", _Nrocaja))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))

        _listParam.Add(New Datos.DParametro("@TV00121", "", detalle))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV00121Cheque", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _tenumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnGrabarCobranza2(_tenumi As String, _tefdoc As String, _tety4vend As Integer, _teobs As String, detalle As DataTable) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        '   @canumi ,@caalm,@cafdoc ,@caty4prov  ,@catven,
        '@cafvcr,@camon ,@caest  ,@caobs ,@cadesc ,@newFecha,@newHora,@cauact
        _listParam.Add(New Datos.DParametro("@tipo", 10))
        _listParam.Add(New Datos.DParametro("@tenumi", _tenumi))
        _listParam.Add(New Datos.DParametro("@tefdoc", _tefdoc))
        _listParam.Add(New Datos.DParametro("@tety4vend", _tety4vend))
        _listParam.Add(New Datos.DParametro("@teobs", _teobs))
        _listParam.Add(New Datos.DParametro("@teuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@TV00121", "", detalle))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV00121Cheque", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _tenumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnModificarCobranza(_tenumi As String, _tefdoc As String, _tety4vend As Integer, _teobs As String, detalle As DataTable) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        '   @canumi ,@caalm,@cafdoc ,@caty4prov  ,@catven,
        '@cafvcr,@camon ,@caest  ,@caobs ,@cadesc ,@newFecha,@newHora,@cauact
        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@tenumi", _tenumi))
        _listParam.Add(New Datos.DParametro("@tefdoc", _tefdoc))
        _listParam.Add(New Datos.DParametro("@tety4vend", _tety4vend))
        _listParam.Add(New Datos.DParametro("@teobs", _teobs))
        _listParam.Add(New Datos.DParametro("@teuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@TV00121", "", detalle))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV00121Cheque", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _tenumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnVerificarSiSeContabilizoPagoVenta(_tenumi As String) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@tenumi", _tenumi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV00121Cheque", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
#End Region

#Region "PAGOS DE LAS COMPRAS CON CHEQUE"
    Public Shared Function L_fnCobranzasGeneralCompra() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@teuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC00121Cheque", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarPagosCompras(_credito As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@credito", _credito))
        _listParam.Add(New Datos.DParametro("@teuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC00121Cheque", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnCobranzasObtenerLosPagosCompra(_numi As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@teuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@tdnumi", _numi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC00121Cheque", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnCobranzasObtenerLasVentasACreditoCompras() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@teuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC00121Cheque", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnCobranzasDetalleCompras(_numi As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@teuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@tenumi", _numi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC00121Cheque", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnCobranzasReporteCompras(_numi As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 8))
        _listParam.Add(New Datos.DParametro("@teuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@tenumi", _numi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC00121Cheque", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnEliminarCobranzaCompras(numi As String, ByRef mensaje As String, _version As String, _ip As String, _usumaquina As String) As Boolean
        Dim _resultado As Boolean
        If L_fnbValidarEliminacion(numi, "TC0013", "tenumi", mensaje) = True Then
            Dim _Tabla As DataTable
            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@tenumi", numi))
            _listParam.Add(New Datos.DParametro("@version", _version))
            _listParam.Add(New Datos.DParametro("@ip", _ip))
            _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
            _listParam.Add(New Datos.DParametro("@teuact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_Mam_TC00121Cheque", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If
        Return _resultado
    End Function
    '@tenumi ,@tefdoc,@tety4vend ,@teobs ,@newFecha ,@newHora ,@teuact 
    Public Shared Function L_fnGrabarCobranzaCompras(_tenumi As String, _tefdoc As String, _tety4vend As Integer, _teobs As String,
                                                     detalle As DataTable, _version As String, _ip As String, _usumaquina As String) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        '   @canumi ,@caalm,@cafdoc ,@caty4prov  ,@catven,
        '@cafvcr,@camon ,@caest  ,@caobs ,@cadesc ,@newFecha,@newHora,@cauact
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@tenumi", _tenumi))
        _listParam.Add(New Datos.DParametro("@tefdoc", _tefdoc))
        _listParam.Add(New Datos.DParametro("@tety4vend", _tety4vend))
        _listParam.Add(New Datos.DParametro("@teobs", _teobs))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@teuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@TC00121", "", detalle))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC00121Cheque", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _tenumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnModificarCobranzaCompras(_tenumi As String, _tefdoc As String, _tety4vend As Integer, _teobs As String, detalle As DataTable) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        '   @canumi ,@caalm,@cafdoc ,@caty4prov  ,@catven,
        '@cafvcr,@camon ,@caest  ,@caobs ,@cadesc ,@newFecha,@newHora,@cauact
        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@tenumi", _tenumi))
        _listParam.Add(New Datos.DParametro("@tefdoc", _tefdoc))
        _listParam.Add(New Datos.DParametro("@tety4vend", _tety4vend))
        _listParam.Add(New Datos.DParametro("@teobs", _teobs))
        _listParam.Add(New Datos.DParametro("@teuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@TC00121", "", detalle))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC00121Cheque", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _tenumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnVerificarSiSeContabilizoPagoCompra(_tenumi As String) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@tenumi", _tenumi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC00121Cheque", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

#End Region

#Region "COBROS DE LAS COMPRAS"
    Public Shared Function L_fnObtenerLasComprasACredito() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@tduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC00121", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnObtenerLosPagosComprasCreditos(_numi As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@tduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@credito", _numi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC00121", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnGrabarPagosCompraCredito(_numi As String, dt As DataTable) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@tduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@tdnumi", _numi))
        _listParam.Add(New Datos.DParametro("@TC00121", "", dt))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC00121", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _numi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnEliminarPagoCompraCredito(numi As String, ByRef mensaje As String) As Boolean
        Dim _resultado As Boolean
        If L_fnbValidarEliminacion(numi, "TC00121", "tdnumi", mensaje) = True Then
            Dim _Tabla As DataTable
            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@tdnumi", numi))
            _listParam.Add(New Datos.DParametro("@tduact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_Mam_TC00121", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If
        Return _resultado
    End Function
#End Region

#Region "TI002 MOVIMIENTOS "
    Public Shared Function L_fnGeneralMovimiento(mostrar As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@mostrar", mostrar))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function


    Public Shared Function L_fnDetalleMovimiento(_ibid As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@ibid", _ibid))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnResetearTI001(deposito As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 35))
        _listParam.Add(New Datos.DParametro("@depositoInventario", deposito))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnObtenerCabezeraCompras(Deposito As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 36))
        _listParam.Add(New Datos.DParametro("@depositoInventario", Deposito))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnObtenerCabezeraVentas(Deposito As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 39))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@depositoInventario", Deposito))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnObtenerTI0021(Deposito As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 43))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@depositoInventario", Deposito))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnEliminandoTI0021EgresoIngreso(deposito As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 44))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@depositoInventario", deposito))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnObteniendoDetalleCompra(numi As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 37))
        _listParam.Add(New Datos.DParametro("@numi", numi))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnObteniendoDetalleVentas(numi As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 40))
        _listParam.Add(New Datos.DParametro("@numi", numi))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function



    Public Shared Function L_fnObteniendoSaldosTI001(producto As Integer, deposito As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 41))
        _listParam.Add(New Datos.DParametro("@cbty5prod", producto))
        _listParam.Add(New Datos.DParametro("@depositoInventario", deposito))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prMovimientoConcepto() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarSucursalesMovimiento() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prMovimientoListarProductos(dt As DataTable, _deposito As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", _deposito))
        _Tabla = D_ProcedimientoConParam("sp_Marco_TI002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prMovimientoListarUnProducto(_deposito As Integer, _CodPro As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", _deposito))
        _listParam.Add(New Datos.DParametro("@producto", _CodPro))
        _Tabla = D_ProcedimientoConParam("sp_Marco_TI002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prMovimientoListarProductosConLote(_deposito As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 31))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", _deposito))
        _Tabla = D_ProcedimientoConParam("sp_Marco_TI002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarLotesPorProductoMovimiento(_almacen As Integer, _codproducto As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 32))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@producto", _codproducto))
        _listParam.Add(New Datos.DParametro("ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prMovimientoChoferABMDetalle(numi As String, Type As Integer, detalle As DataTable) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", Type))
        _listParam.Add(New Datos.DParametro("@ibid", numi))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@TI0021", "", detalle))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prInsertarTi0021(numi As String, Type As Integer, detalle As DataTable, deposito As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", Type))
        _listParam.Add(New Datos.DParametro("@ibid", numi))
        _listParam.Add(New Datos.DParametro("@depositoInventario", deposito))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@TI0021", "", detalle))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function


    Public Shared Function L_prMovimientoChoferGrabar(ByRef _ibid As String, _ibfdoc As String, _ibconcep As Integer,
                                                      _ibobs As String, _almacen As Integer, _depositoDestino As Integer,
                                                      _ibidOrigen As Integer, _detalle As DataTable, _motivo As Integer,
                                                       _version As String, _ip As String, _usumaquina As String) As Boolean
        Dim _resultado As Boolean
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@ibid", _ibid))
        _listParam.Add(New Datos.DParametro("@ibfdoc", _ibfdoc))
        _listParam.Add(New Datos.DParametro("@ibconcep", _ibconcep))
        _listParam.Add(New Datos.DParametro("@ibobs", _ibobs))
        _listParam.Add(New Datos.DParametro("@ibest", 1))
        _listParam.Add(New Datos.DParametro("@ibalm", _almacen))
        _listParam.Add(New Datos.DParametro("@ibdepdest", _depositoDestino))
        _listParam.Add(New Datos.DParametro("@ibiddc", 0))
        _listParam.Add(New Datos.DParametro("@ibidOrigen", _ibidOrigen))
        _listParam.Add(New Datos.DParametro("@motivo", _motivo))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))

        _listParam.Add(New Datos.DParametro("@TI0021", "", _detalle))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)
        If _Tabla.Rows.Count > 0 Then
            _ibid = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function


    Public Shared Function L_prGrabarTI002(obs As String, deposito As Integer,
                    cbnumi As Integer, fact As Date, hact As String, uact As String, cbty5prod As Integer,
                    cbcmin As Double, cblote As String, cbfechavenc As Date) As Boolean
        Dim _resultado As Boolean
        Dim _Tabla As DataTable

        Try
            Dim _listParam As New List(Of Datos.DParametro)
            _listParam.Add(New Datos.DParametro("@tipo", 38))
            _listParam.Add(New Datos.DParametro("@obs", obs))
            _listParam.Add(New Datos.DParametro("@depositoInventario", deposito))
            _listParam.Add(New Datos.DParametro("@cbnumi", cbnumi))
            _listParam.Add(New Datos.DParametro("@fact", fact))
            _listParam.Add(New Datos.DParametro("@hact", hact))
            _listParam.Add(New Datos.DParametro("@uact", uact))
            _listParam.Add(New Datos.DParametro("@cbty5prod", cbty5prod))
            _listParam.Add(New Datos.DParametro("@cbcmin", cbcmin))
            _listParam.Add(New Datos.DParametro("@cblote", cblote))
            _listParam.Add(New Datos.DParametro("@cbfechavenc", cbfechavenc))
            '_listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
            _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)
            If _Tabla.Rows.Count > 0 Then

                _resultado = True
            Else
                _resultado = False
            End If
        Catch ex As Exception
            Dim mens As String = ex.Message
        End Try



        Return _resultado
    End Function

    Public Shared Function L_prGrabarTI002Venta(obs As String, deposito As Integer,
                    cbnumi As Integer, fact As Date, hact As String, uact As String, cbty5prod As Integer,
                    cbcmin As Double, cblote As String, cbfechavenc As Date) As Boolean
        Dim _resultado As Boolean
        Dim _Tabla As DataTable

        Try
            Dim _listParam As New List(Of Datos.DParametro)
            _listParam.Add(New Datos.DParametro("@tipo", 42))
            _listParam.Add(New Datos.DParametro("@obs", obs))
            _listParam.Add(New Datos.DParametro("@depositoInventario", deposito))
            _listParam.Add(New Datos.DParametro("@cbnumi", cbnumi))
            _listParam.Add(New Datos.DParametro("@fact", fact))
            _listParam.Add(New Datos.DParametro("@hact", hact))
            _listParam.Add(New Datos.DParametro("@uact", uact))
            _listParam.Add(New Datos.DParametro("@cbty5prod", cbty5prod))
            _listParam.Add(New Datos.DParametro("@cbcmin", cbcmin))
            _listParam.Add(New Datos.DParametro("@cblote", cblote))
            _listParam.Add(New Datos.DParametro("@cbfechavenc", cbfechavenc))
            '_listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
            _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)
            If _Tabla.Rows.Count > 0 Then

                _resultado = True
            Else
                _resultado = False
            End If
        Catch ex As Exception
            Dim mens As String = ex.Message
        End Try



        Return _resultado
    End Function

    Public Shared Function L_prMovimientoModificar(ByRef _ibid As String, _ibfdoc As String, _ibconcep As Integer, _ibobs As String,
                                                   _almacen As Integer, _detalle As DataTable, _motivo As Integer, _version As String,
                                                   _ip As String, _usumaquina As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@ibid", _ibid))
        _listParam.Add(New Datos.DParametro("@ibfdoc", _ibfdoc))
        _listParam.Add(New Datos.DParametro("@ibconcep", _ibconcep))
        _listParam.Add(New Datos.DParametro("@ibobs", _ibobs))
        _listParam.Add(New Datos.DParametro("@ibest", 1))
        _listParam.Add(New Datos.DParametro("@ibalm", _almacen))
        _listParam.Add(New Datos.DParametro("@ibiddc", 0))
        _listParam.Add(New Datos.DParametro("@motivo", _motivo))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))

        _listParam.Add(New Datos.DParametro("@TI0021", "", _detalle))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _ibid = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prMovimientoEliminar(numi As String, _ibconcep As Integer, _version As String, _ip As String, _usumaquina As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", -1))
        _listParam.Add(New Datos.DParametro("@ibid", numi))
        _listParam.Add(New Datos.DParametro("@ibconcep", _ibconcep))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnListarProductosKardex(_almacen As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)


        _listParam.Add(New Datos.DParametro("@tipo", 8))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarLotesProductos(codproducto As Integer, _almacen As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 28))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@producto", codproducto))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnObtenerSaldoProducto(_almacen As Integer, _codProducto As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 23))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@producto", _codProducto))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnObtenerHistorialProducto(_codProducto As Integer, FechaI As String, FechaF As String, _almacen As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@producto", _codProducto))
        _listParam.Add(New Datos.DParametro("@fechaI", FechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", FechaF))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnObtenerHistorialProductoporLote(_codProducto As Integer, FechaI As String, FechaF As String, _almacen As Integer, _Lote As String, _FechaVenc As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 30))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@producto", _codProducto))
        _listParam.Add(New Datos.DParametro("@fechaI", FechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", FechaF))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@lote", _Lote))
        _listParam.Add(New Datos.DParametro("@fechaVenc", _FechaVenc))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnObtenerKardexPorProducto(_codProducto As Integer, FechaI As String, FechaF As String, _almacen As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 25))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@producto", _codProducto))
        _listParam.Add(New Datos.DParametro("@fechaI", FechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", FechaF))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnObtenerProductoConMovimiento(FechaI As String, FechaF As String, _almacen As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 26))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@fechaI", FechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", FechaF))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnObtenerKardexGeneralProductos(FechaI As String, FechaF As String, _almacen As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 27))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@fechaI", FechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", FechaF))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnObtenerKardexGeneralProductosporLote(FechaI As String, FechaF As String, _almacen As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 33))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@fechaI", FechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", FechaF))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnObtenerHistorialProductoGeneral(_codProducto As Integer, FechaI As String, _almacen As Integer) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 20))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@producto", _codProducto))
        _listParam.Add(New Datos.DParametro("@fechaI", FechaI))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function



    Public Shared Function L_fnObtenerHistorialProductoGeneralPorLote(_codProducto As Integer, FechaI As String, _almacen As Integer, _Lote As String, FechaVenc As String) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 29))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@producto", _codProducto))
        _listParam.Add(New Datos.DParametro("@fechaI", FechaI))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@lote", _Lote))
        _listParam.Add(New Datos.DParametro("@fechaVenc", FechaVenc))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnActualizarSaldo(_Almacen As Integer, _CodProducto As String, _Cantidad As Double) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 21))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@producto", _CodProducto))
        _listParam.Add(New Datos.DParametro("@almacen", _Almacen))
        _listParam.Add(New Datos.DParametro("@cantidad", _Cantidad))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnActualizarSaldoNuevo(_Almacen As Integer, _CodProducto As String, _Cantidad As Double,
                                                    Lote As String, FechaVenc As Date) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 46))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@producto", _CodProducto))
        _listParam.Add(New Datos.DParametro("@almacen", _Almacen))
        _listParam.Add(New Datos.DParametro("@cantidad", _Cantidad))
        _listParam.Add(New Datos.DParametro("@cblote", Lote))
        _listParam.Add(New Datos.DParametro("@cbfechavenc", FechaVenc))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnStockActual() As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 22))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnStockActualLote() As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 34))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnMotivoTraspaso(_CodMovimiento As String) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 47))
        _listParam.Add(New Datos.DParametro("@ibid", _CodMovimiento))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnVerificacionStockProducto(_CodPro As String) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 48))
        _listParam.Add(New Datos.DParametro("@producto", _CodPro))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnMotivoIngresoSalida(_CodMovimiento As String, _cod1 As Integer, _cod2 As Integer, _concepto As Integer) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 49))
        _listParam.Add(New Datos.DParametro("@ibid", _CodMovimiento))
        _listParam.Add(New Datos.DParametro("@cod1", _cod1))
        _listParam.Add(New Datos.DParametro("@cod2", _cod2))
        _listParam.Add(New Datos.DParametro("@ibconcep", _concepto))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function
#End Region

#Region "ROLES CORRECTO"

    Public Shared Function L_prRolGeneral() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_ZY002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prRolDetalleGeneral(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@ybnumi", _numi))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_ZY002", _listParam)

        Return _Tabla
    End Function


    Public Shared Function L_prRolGrabar(ByRef _numi As String, _rol As String, _detalle As DataTable, _version As String,
                                         _ip As String, _usumaquina As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@ybnumi", _numi))
        _listParam.Add(New Datos.DParametro("@ybrol", _rol))
        _listParam.Add(New Datos.DParametro("@ZY0021", "", _detalle))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_ZY002", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _numi = _Tabla.Rows(0).Item(0)
            _resultado = True
            'L_prTipoCambioGrabarHistorial(_numi, _fecha, _dolar, _ufv, "TIPO DE CAMBIO", 1)
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prRolModificar(_numi As String, _rol As String, _detalle As DataTable, _version As String,
                                         _ip As String, _usumaquina As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@ybnumi", _numi))
        _listParam.Add(New Datos.DParametro("@ybrol", _rol))
        _listParam.Add(New Datos.DParametro("@ZY0021", "", _detalle))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_ZY002", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
            'L_prTipoCambioGrabarHistorial(_numi, _fecha, _dolar, _ufv, "TIPO DE CAMBIO", 2)
        Else
            _resultado = False
        End If

        Return _resultado
    End Function



    Public Shared Function L_prRolBorrar(_numi As String, ByRef _mensaje As String, _version As String, _ip As String,
                                         _usumaquina As String) As Boolean

        Dim _resultado As Boolean

        If L_fnbValidarEliminacion(_numi, "ZY002", "ybnumi", _mensaje) = True Then
            Dim _Tabla As DataTable

            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@ybnumi", _numi))
            _listParam.Add(New Datos.DParametro("@version", _version))
            _listParam.Add(New Datos.DParametro("@ip", _ip))
            _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
            _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_dg_ZY002", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
                'L_prTipoCambioGrabarHistorial(_numi, _fecha, _dolar, _ufv, "TIPO DE CAMBIO", 3)
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If

        Return _resultado
    End Function



#End Region
#Region "REPORTES VENTAS"
    Public Shared Function L_BuscarVentasAtentidas(fechaI As String, fechaF As String, idAlmacen As Integer, idVendedor As Integer, idCliente As Integer) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@Vendedor", idVendedor))
        _listParam.Add(New Datos.DParametro("@Cliente", idCliente))
        _listParam.Add(New Datos.DParametro("@almacen", idAlmacen))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReporteVentas", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_BuscarVentasCajerasProveedores(fechaI As String, fechaF As String, idAlmacen As Integer, idUsuario As Integer, idProveedor As Integer) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@usuario", idUsuario))
        _listParam.Add(New Datos.DParametro("@proveedor", idProveedor))
        _listParam.Add(New Datos.DParametro("@almacen", idAlmacen))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReporteVentas", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_BuscarVentasCajerasProveedoresProductos(fechaI As String, fechaF As String, idAlmacen As Integer, idUsuario As Integer, idProveedor As Integer) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@usuario", idUsuario))
        _listParam.Add(New Datos.DParametro("@proveedor", idProveedor))
        _listParam.Add(New Datos.DParametro("@almacen", idAlmacen))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReporteVentas", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_VentasProductos(fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReporteVentas", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_BuscarVentasCajerasProveedoresSinUsuario(fechaI As String, fechaF As String, idAlmacen As Integer, idProveedor As Integer) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@proveedor", idProveedor))
        _listParam.Add(New Datos.DParametro("@almacen", idAlmacen))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReporteVentas", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_BuscarVentasCajerasProveedoresProductosSinUsuario(fechaI As String, fechaF As String, idAlmacen As Integer, idProveedor As Integer) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@proveedor", idProveedor))
        _listParam.Add(New Datos.DParametro("@almacen", idAlmacen))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReporteVentas", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_DescuentoProductos() As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReporteVentas", _listParam)
        Return _Tabla
    End Function
    'Public Shared Function L_RepMixto(fechaI As String, fechaF As String) As DataTable
    '    Dim _Tabla As DataTable
    '    Dim _listParam As New List(Of Datos.DParametro)
    '    _listParam.Add(New Datos.DParametro("@tipo", 8))
    '    _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
    '    _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
    '    _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
    '    _Tabla = D_ProcedimientoConParam("Proc_ReporteVentas", _listParam)
    '    Return _Tabla
    'End Function

    Public Shared Function L_RepCierresCajaResumen(fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReporteVentas", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_RepCierresCajaDetallado(fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 10))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReporteVentas", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_PreciosProductosCajeros() As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 11))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReporteVentas", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_BuscarVentasAtendidas2(fechaI As String, fechaF As String, idAlmacen As Integer,
                                                    idVendedor As Integer, idCliente As Integer, nrocaja As Integer) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 12))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@vendedor", idVendedor))
        _listParam.Add(New Datos.DParametro("@cliente", idCliente))
        _listParam.Add(New Datos.DParametro("@almacen", idAlmacen))
        _listParam.Add(New Datos.DParametro("@nroCaja", nrocaja))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReporteVentas", _listParam)
        Return _Tabla
    End Function

    Public Shared Function L_FacturasAnuladas(fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 13))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReporteVentas", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_ProductosConsignacionFlow(NroConsig As String) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 14))
        _listParam.Add(New Datos.DParametro("@nroConsig", NroConsig))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReporteVentas", _listParam)
        Return _Tabla
    End Function
#End Region
#Region "REPORTE MIXTO"
    Public Shared Function L_RepMixto(fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Sp_Mam_ReporteMixto", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_RepMixtoVentas(fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Sp_Mam_ReporteMixto", _listParam)
        Return _Tabla
    End Function

    Public Shared Function L_RepMixtoCompras(fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Sp_Mam_ReporteMixto", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_RepMixtoMovSalida(fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Sp_Mam_ReporteMixto", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_RepMixtoMovEntrada(fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Sp_Mam_ReporteMixto", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_RepMixtoTraspasoSalida(fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Sp_Mam_ReporteMixto", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_RepMixtoTraspasoIngreso(fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Sp_Mam_ReporteMixto", _listParam)
        Return _Tabla
    End Function
#End Region
#Region "REPORTES VS VENTAS"


    Public Shared Function L_prVentasVsCostosGeneralAlmacenVendedor(fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("Proc_ReporteVentasVsCostos", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prVentasVsProductosGeneral(fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("Proc_ReporteVentasVsCostos", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prVentasVsProductosTodosALmacenesPrecio(fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("Proc_ReporteVentasVsCostos", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prVentasVsProductos(fechaI As String, fechaF As String, almacen As String, proveedor As String, producto As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@almacen", almacen))
        _listParam.Add(New Datos.DParametro("@proveedor", proveedor))
        _listParam.Add(New Datos.DParametro("@producto", producto))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReporteVentasVsCostos", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prObtenerProductos() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReporteVentasVsCostos", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prVentasVsProductosUnaALmacenesPrecio(fechaI As String, fechaF As String,
                                                                   _almacen As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("Proc_ReporteVentasVsCostos", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prVentasVsCostosPorVendedorTodosAlmacen(fechaI As String, fechaF As String, _numiVendedor As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@vendedor", _numiVendedor))
        _Tabla = D_ProcedimientoConParam("Proc_ReporteVentasVsCostos", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prVentasVsCostosTodosVendedorUnaAlmacen(fechaI As String, fechaF As String, _numiAlmacen As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", _numiAlmacen))
        _Tabla = D_ProcedimientoConParam("Proc_ReporteVentasVsCostos", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prVentasVsCostosUnaVendedorUnaAlmacen(fechaI As String, fechaF As String, _numiAlmacen As String, _numiVendedor As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", _numiAlmacen))
        _listParam.Add(New Datos.DParametro("@vendedor", _numiVendedor))
        _Tabla = D_ProcedimientoConParam("Proc_ReporteVentasVsCostos", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prProductosNoVendidos(fechaI As String, fechaF As String, proveedor As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 8))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@proveedor", proveedor))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReporteVentasVsCostos", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prProductosNoVendidosStockMayor0(fechaI As String, fechaF As String, proveedor As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@proveedor", proveedor))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReporteVentasVsCostos", _listParam)

        Return _Tabla
    End Function

#End Region
#Region "REPORTES GRAFICOS DE VENTAS"


    Public Shared Function L_prVentasGraficaVendedorMes(fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasGraficas", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prVentasGraficaVendedorAlmacen(fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasGraficas", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prVentasGraficaVendedorRendimiento(fechaI As String, fechaF As String, dt As DataTable) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@cliente", "", dt))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasGraficas", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prMovimientoProductoGeneral(CodAlmacen As String, Mes As String, Anho As String, dt As DataTable) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        If (CodAlmacen.Equals("50")) Then

            _listParam.Add(New Datos.DParametro("@tipo", 9))
            _listParam.Add(New Datos.DParametro("@Mes", Mes))
            _listParam.Add(New Datos.DParametro("@Anho", Anho))
            _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
            _listParam.Add(New Datos.DParametro("@Meses", "", dt))
        Else
            _listParam.Add(New Datos.DParametro("@tipo", 10))
            _listParam.Add(New Datos.DParametro("@Mes", Mes))
            _listParam.Add(New Datos.DParametro("@Anho", Anho))
            _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
            _listParam.Add(New Datos.DParametro("@almacen", CodAlmacen))
            _listParam.Add(New Datos.DParametro("@Meses", "", dt))
        End If

        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasGraficas", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prMovimientoProductoCantPorProducto(CodProducto As String, CodAlmacen As String, Mes As String, Anho As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        If (CodProducto.Equals("")) Then
            If (CodAlmacen.Equals("50")) Then
                _listParam.Add(New Datos.DParametro("@tipo", 8))
                _listParam.Add(New Datos.DParametro("@Mes", Mes))
                _listParam.Add(New Datos.DParametro("@Anho", Anho))
                _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
            Else
                _listParam.Add(New Datos.DParametro("@tipo", 12))
                _listParam.Add(New Datos.DParametro("@Mes", Mes))
                _listParam.Add(New Datos.DParametro("@almacen", CodAlmacen))
                _listParam.Add(New Datos.DParametro("@Anho", Anho))
                _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
            End If

        Else

            If (CodAlmacen.Equals("50")) Then
                _listParam.Add(New Datos.DParametro("@tipo", 6))
                _listParam.Add(New Datos.DParametro("@Mes", Mes))
                _listParam.Add(New Datos.DParametro("@Anho", Anho))
                _listParam.Add(New Datos.DParametro("@producto", CodProducto))
                _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
            Else
                _listParam.Add(New Datos.DParametro("@tipo", 11))
                _listParam.Add(New Datos.DParametro("@Mes", Mes))
                _listParam.Add(New Datos.DParametro("@Anho", Anho))
                _listParam.Add(New Datos.DParametro("@almacen", CodAlmacen))
                _listParam.Add(New Datos.DParametro("@producto", CodProducto))
                _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
            End If




        End If


        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasGraficas", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prMovimientoProductoObtenerImagenProducto(CodProducto As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@producto", CodProducto))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasGraficas", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prVentasGraficaListarVendedores() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasGraficas", _listParam)

        Return _Tabla
    End Function
#End Region

#Region "REPORTE DE CREDITOS"


    Public Shared Function L_prReporteCreditoGeneral(fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function


    Public Shared Function L_prReporteCreditoGeneralCompras(fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 14))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prReporteCreditoGeneralRes(fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 111))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prReporteCreditoClienteRes(fechaI As String, fechaF As String, codcli As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 112))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@cliente", codcli))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prReporteCreditoResumen(fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prListarPrecioVenta() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prListarPrecioCosto() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 8))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prReporteUtilidad(_codAlmacen As Integer, _codCat As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", _codAlmacen))
        _listParam.Add(New Datos.DParametro("@catPrecio", _codCat))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prReporteUtilidadal(_codAlmacen As Integer, _codCat As Integer, _date1 As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 91))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", _codAlmacen))
        _listParam.Add(New Datos.DParametro("@catPrecio", _codCat))
        _listParam.Add(New Datos.DParametro("@fechaF", _date1))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prReporteUtilidadmayor(_codAlmacen As Integer, _codCat As Integer, _date1 As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 92))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", _codAlmacen))
        _listParam.Add(New Datos.DParametro("@catPrecio", _codCat))
        _listParam.Add(New Datos.DParametro("@fechaF", _date1))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prReporteUtilidadConsolidado(_codAlmacen As Integer, _codCat As Integer, _date1 As String, MayorCero As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 93))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", _codAlmacen))
        _listParam.Add(New Datos.DParametro("@catPrecio", _codCat))
        _listParam.Add(New Datos.DParametro("@fechaF", _date1))
        _listParam.Add(New Datos.DParametro("@MayorCero", MayorCero))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prReporteUtilidadStockMayorCero(_codAlmacen As Integer, _codCat As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 13))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", _codAlmacen))
        _listParam.Add(New Datos.DParametro("@catPrecio", _codCat))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prReporteCreditoClienteTodosCuentas(fechaI As String, fechaF As String, _numiCliente As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@cliente", _numiCliente))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prReporteCreditoProveedoresTodosCuentas(fechaI As String, fechaF As String, _numiCliente As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 15))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@cliente", _numiCliente))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prReporteCreditoClienteUnaCuentas(_numiCredito As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@codCredito", _numiCredito))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prReporteCreditoProveedorUnaCuentas(_numiCredito As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 16))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@codCredito", _numiCredito))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prReporteCreditoListarCuentasPorCliente(_numiCliente As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@cliente", _numiCliente))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function


    Public Shared Function L_prReporteCreditoListarCuentasPorProveedor(_numiCliente As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 18))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@cliente", _numiCliente))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarClienteCreditos() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnListarProveedoresCreditos() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 17))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnReporteMorosidadTodosAlmacenVendedor() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 10))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnReporteMorosidadTodosAlmacenUnVendedor(numiVendedor As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 11))
        _listParam.Add(New Datos.DParametro("@vendedor", numiVendedor))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnReporteMorosidadUnAlmacenUnVendedor(numiVendedor As Integer, numialmacen As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 12))
        _listParam.Add(New Datos.DParametro("@vendedor", numiVendedor))
        _listParam.Add(New Datos.DParametro("@almacen", numialmacen))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prReporteStockConsolidado(_codAlmacen As Integer, _codCat As Integer, _date1 As Date) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 19))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", _codAlmacen))
        _listParam.Add(New Datos.DParametro("@catPrecio", _codCat))
        _listParam.Add(New Datos.DParametro("@fechaf", _date1))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prReporteProductosStockMayorCero(_codAlmacen As Integer, _codCat As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 20))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", _codAlmacen))
        _listParam.Add(New Datos.DParametro("@catPrecio", _codCat))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prReporteProductoMayorFecha(_codAlmacen As Integer, _codCat As Integer, _date1 As Date) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 21))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", _codAlmacen))
        _listParam.Add(New Datos.DParametro("@catPrecio", _codCat))
        _listParam.Add(New Datos.DParametro("@fechaf", _date1))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prListarCatPrecios() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 22))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prListarEstadoCuentasClientesTotal(idCliente As Integer, fechai As String) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@cliente", idCliente))
        _listParam.Add(New Datos.DParametro("@fechai", fechai))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_EstadoCuentas", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_prListarEstadoCuentasCliente(idCliente As Integer, fechai As String, fechaf As String) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@cliente", idCliente))
        _listParam.Add(New Datos.DParametro("@fechai", fechai))
        _listParam.Add(New Datos.DParametro("@fechaf", fechaf))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_EstadoCuentas", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_prListarEstadoCuentasClienteTodos() As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_EstadoCuentas", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_prListarEstadoCuentasUnCliente(idCliente As String) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@cliente", idCliente))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_EstadoCuentas", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_prListarEstadoCuentasProveedoresTodos() As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_EstadoCuentas", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_prListarEstadoCuentasUnProveedor(idProveedor As String) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 8))
        _listParam.Add(New Datos.DParametro("@prov", idProveedor))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_EstadoCuentas", _listParam)
        Return _Tabla
    End Function
#End Region

#Region "TP001 PROFORMA"
    Public Shared Function L_fnGeneralProforma(mostrar As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@mostrar", mostrar))
        _listParam.Add(New Datos.DParametro("@pauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TP001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnReporteProforma(_numi As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 10))
        _listParam.Add(New Datos.DParametro("@panumi", _numi))
        _listParam.Add(New Datos.DParametro("@pauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TP001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnReporteproducto() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 11))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnObtenerNroFactura() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 12))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TF001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnDetalleProforma(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@panumi", _numi))
        _listParam.Add(New Datos.DParametro("@pauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TP001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarProductosProforma(_almacen As String, _cliente As String, _detalle As DataTable) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@cliente", _cliente))
        _listParam.Add(New Datos.DParametro("@pauact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@TP0011", "", _detalle))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TP001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarProductosSinLoteProforma(_almacen As String, _cliente As String, _detalle As DataTable) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@cliente", _cliente))
        _listParam.Add(New Datos.DParametro("@pauact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@TP0011", "", _detalle))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TP001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarProductosSinLoteProformaNuevo(_almacen As String, _cliente As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@cliente", _cliente))
        _listParam.Add(New Datos.DParametro("@pauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TP001_Nuevo", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarClientesProforma() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TP001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarEmpleadoProforma() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TP001", _listParam)

        Return _Tabla
    End Function


    Public Shared Function L_fnGrabarProforma(ByRef _panumi As String, _pafdoc As String, _paven As Integer, _paclpr As Integer,
                                              _pamon As Integer, _paobs As String, _padesc As Double, _patotal As Double,
                                              detalle As DataTable, _almacen As Integer, _cliente As String, _contacto As String,
                                              _telf As String, _version As String, _ip As String, _usumaquina As String) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@panumi", _panumi))
        _listParam.Add(New Datos.DParametro("@paalm", _almacen))
        _listParam.Add(New Datos.DParametro("@pafdoc", _pafdoc))
        _listParam.Add(New Datos.DParametro("@paven", _paven))
        _listParam.Add(New Datos.DParametro("@paclpr", _paclpr))
        _listParam.Add(New Datos.DParametro("@pamon", _pamon))
        _listParam.Add(New Datos.DParametro("@paest", 1))
        _listParam.Add(New Datos.DParametro("@paobs", _paobs))
        _listParam.Add(New Datos.DParametro("@padesc", _padesc))
        _listParam.Add(New Datos.DParametro("@patotal", _patotal))
        _listParam.Add(New Datos.DParametro("@pacliente", _cliente))
        _listParam.Add(New Datos.DParametro("@pacontacto", _contacto))
        _listParam.Add(New Datos.DParametro("@patelf", _telf))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@pauact", L_Usuario))

        _listParam.Add(New Datos.DParametro("@TP0011", "", detalle))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TP001", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _panumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnModificarProforma(ByRef _panumi As String, _pafdoc As String, _paven As Integer, _paclpr As Integer,
                                                _pamon As Integer, _paobs As String, _padesc As Double, _patotal As Double,
                                                detalle As DataTable, _almacen As Integer, _cliente As String, _contacto As String,
                                              _telf As String, _version As String, _ip As String, _usumaquina As String) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@panumi", _panumi))
        _listParam.Add(New Datos.DParametro("@paalm", _almacen))
        _listParam.Add(New Datos.DParametro("@pafdoc", _pafdoc))
        _listParam.Add(New Datos.DParametro("@paven", _paven))
        _listParam.Add(New Datos.DParametro("@paclpr", _paclpr))
        _listParam.Add(New Datos.DParametro("@pamon", _pamon))
        _listParam.Add(New Datos.DParametro("@paest", 1))
        _listParam.Add(New Datos.DParametro("@paobs", _paobs))
        _listParam.Add(New Datos.DParametro("@padesc", _padesc))
        _listParam.Add(New Datos.DParametro("@patotal", _patotal))
        _listParam.Add(New Datos.DParametro("@pacliente", _cliente))
        _listParam.Add(New Datos.DParametro("@pacontacto", _contacto))
        _listParam.Add(New Datos.DParametro("@patelf", _telf))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@pauact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@TP0011", "", detalle))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TP001", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _panumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnEliminarProforma(numi As String, ByRef mensaje As String, _version As String, _ip As String,
                                                _usumaquina As String) As Boolean
        Dim _resultado As Boolean
        If L_fnbValidarEliminacion(numi, "TP001", "panumi", mensaje) = True Then
            Dim _Tabla As DataTable
            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@panumi", numi))
            _listParam.Add(New Datos.DParametro("@version", _version))
            _listParam.Add(New Datos.DParametro("@ip", _ip))
            _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
            _listParam.Add(New Datos.DParametro("@pauact", L_Usuario))
            _Tabla = D_ProcedimientoConParam("sp_Mam_TP001", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If
        Return _resultado
    End Function
    Public Shared Function L_fnListarUnProductoPrecioVenta(_codPro As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 12))
        _listParam.Add(New Datos.DParametro("@producto", _codPro))
        _listParam.Add(New Datos.DParametro("@pauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TP001", _listParam)

        Return _Tabla
    End Function
#End Region
#Region "VENTAS ESTADISTICOS"
    Public Shared Function L_fnObtenerVendedores(_fechaI As String, _fechaF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@fechaI", _fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", _fechaF))

        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasEstadisticos", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnObtenerVentasVendedores(_fechaI As String, _fechaF As String, _numiVendedor As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@fechaI", _fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", _fechaF))
        _listParam.Add(New Datos.DParametro("@vendedor", _numiVendedor))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasEstadisticos", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnObtenerCLientes(_fechaI As String, _fechaF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@fechaI", _fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", _fechaF))

        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasEstadisticos", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnObtenerVentasClientes(_fechaI As String, _fechaF As String, _numiCliente As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@fechaI", _fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", _fechaF))
        _listParam.Add(New Datos.DParametro("@cliente", _numiCliente))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasEstadisticos", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnObtenerProductosVentasEstadistico(_fechaI As String, _fechaF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@fechaI", _fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", _fechaF))

        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasEstadisticos", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnObtenerVentasProductosEstadistico(_fechaI As String, _fechaF As String, _numiProducto As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@fechaI", _fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", _fechaF))
        _listParam.Add(New Datos.DParametro("@producto", _numiProducto))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasEstadisticos", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnObtenerZonasVentasEstadistico(_fechaI As String, _fechaF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@fechaI", _fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", _fechaF))

        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasEstadisticos", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnObtenerVentasZonasEstadistico(_fechaI As String, _fechaF As String, _numiZona As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 8))
        _listParam.Add(New Datos.DParametro("@fechaI", _fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", _fechaF))
        _listParam.Add(New Datos.DParametro("@zona", _numiZona))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasEstadisticos", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnObtenerCOBRANZASVentasEstadistico(_fechaI As String, _fechaF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@fechaI", _fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", _fechaF))

        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasEstadisticos", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnObtenerVentasCOBRANZASEstadistico(_fechaI As String, _fechaF As String, _numiVendedor As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 10))
        _listParam.Add(New Datos.DParametro("@fechaI", _fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", _fechaF))
        _listParam.Add(New Datos.DParametro("@vendedor", _numiVendedor))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasEstadisticos", _listParam)

        Return _Tabla
    End Function
#End Region

#Region "TF001 FACTURA"
    Public Shared Function L_fnGeneralFactura() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@fauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TF001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnDetalleFactura(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@fanumi", _numi))
        _listParam.Add(New Datos.DParametro("@fauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TF001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarProductosFactura(_almacen As String, _cliente As String, _detalle As DataTable) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@cliente", _cliente))
        _listParam.Add(New Datos.DParametro("@fauact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@TF0011", "", _detalle))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TF001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarProductosSinLoteFactura(_almacen As String, _cliente As String, _detalle As DataTable) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@cliente", _cliente))
        _listParam.Add(New Datos.DParametro("@fauact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@TF0011", "", _detalle))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TF001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarClientesFactura() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TF001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarEmpleadoFactura() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TF001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarVentasFactura() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 10))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TF001", _listParam)

        Return _Tabla
    End Function


    Public Shared Function L_fnGrabarFactura(ByRef _fanumi As String, _fatv1numi As Integer, _fanroFact As String, _fafdoc As String, _faven As Integer, _faclpr As Integer,
                                           _famon As Integer, _faobs As String,
                                           _fadesc As Double,
                                           _fatotal As Double, _fanit As String, _fanombFact As String, detalle As DataTable, _almacen As Integer) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        '     @panumi,@paalm,@pafdoc ,@paven ,@paclpr,
        '@pamon ,@paest  ,@paobs ,@padesc  ,@patotal ,@newFecha,@newHora,@pauact

        '     @fanumi,@fatv1numi ,@fanrofact ,@faalm,@fafdoc ,@faven ,@faclpr,
        '@famon ,@faest  ,@faobs ,@fadesc  ,@fatotal ,@fanit ,@fanombfact ,@newFecha,@newHora,@fauact
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@fanumi", _fanumi))
        _listParam.Add(New Datos.DParametro("@fatv1numi", _fatv1numi))
        _listParam.Add(New Datos.DParametro("@fanrofact", _fanroFact))
        _listParam.Add(New Datos.DParametro("@faalm", _almacen))
        _listParam.Add(New Datos.DParametro("@fafdoc", _fafdoc))
        _listParam.Add(New Datos.DParametro("@faven", _faven))
        _listParam.Add(New Datos.DParametro("@faclpr", _faclpr))
        _listParam.Add(New Datos.DParametro("@famon", _famon))
        _listParam.Add(New Datos.DParametro("@faest", 1))
        _listParam.Add(New Datos.DParametro("@faobs", _faobs))
        _listParam.Add(New Datos.DParametro("@fadesc", _fadesc))
        _listParam.Add(New Datos.DParametro("@fatotal", _fatotal))
        _listParam.Add(New Datos.DParametro("@fauact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@fanit", _fanit))
        _listParam.Add(New Datos.DParametro("@fanombfact", _fanombFact))
        _listParam.Add(New Datos.DParametro("@TF0011", "", detalle))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TF001", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _fanumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnModificarFactura(ByRef _fanumi As String, _fatv1numi As Integer, _fanroFact As String, _fafdoc As String, _faven As Integer, _faclpr As Integer,
                                           _famon As Integer, _faobs As String,
                                           _fadesc As Double,
                                           _fatotal As Double, _fanit As String, _fanombFact As String, detalle As DataTable, _almacen As Integer) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@fanumi", _fanumi))
        _listParam.Add(New Datos.DParametro("@fatv1numi", _fatv1numi))
        _listParam.Add(New Datos.DParametro("@fanrofact", _fanroFact))
        _listParam.Add(New Datos.DParametro("@faalm", _almacen))
        _listParam.Add(New Datos.DParametro("@fafdoc", _fafdoc))
        _listParam.Add(New Datos.DParametro("@faven", _faven))
        _listParam.Add(New Datos.DParametro("@faclpr", _faclpr))
        _listParam.Add(New Datos.DParametro("@famon", _famon))
        _listParam.Add(New Datos.DParametro("@faest", 1))
        _listParam.Add(New Datos.DParametro("@faobs", _faobs))
        _listParam.Add(New Datos.DParametro("@fadesc", _fadesc))
        _listParam.Add(New Datos.DParametro("@fatotal", _fatotal))
        _listParam.Add(New Datos.DParametro("@fauact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@fanit", _fanit))
        _listParam.Add(New Datos.DParametro("@fanombfact", _fanombFact))
        _listParam.Add(New Datos.DParametro("@TF0011", "", detalle))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TF001", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _fanumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnImprimirFactura(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 11))
        _listParam.Add(New Datos.DParametro("@fanumi", _numi))
        _listParam.Add(New Datos.DParametro("@fauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TF001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnEliminarFactura(numi As String, ByRef mensaje As String) As Boolean
        Dim _resultado As Boolean
        If L_fnbValidarEliminacion(numi, "TF001", "fanumi", mensaje) = True Then
            Dim _Tabla As DataTable
            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@fanumi", numi))
            _listParam.Add(New Datos.DParametro("@fauact", L_Usuario))
            _Tabla = D_ProcedimientoConParam("sp_Mam_TF001", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If
        Return _resultado
    End Function
#End Region

#Region "SALDO INICIAL DE CLIENTES"
    Public Shared Function L_fnSaldoGeneral() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@tfuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV002", _listParam)

        Return _Tabla
    End Function


    Public Shared Function L_fnSaldoDetalle(_numi As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@tfuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@tfnumi", _numi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV002", _listParam)

        Return _Tabla
    End Function



    Public Shared Function L_fnEliminarSaldos(numi As String, ByRef mensaje As String) As Boolean
        Dim _resultado As Boolean
        If L_fnbValidarEliminacion(numi, "TV002", "tfnumi", mensaje) = True Then
            Dim _Tabla As DataTable
            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@tfnumi", numi))
            _listParam.Add(New Datos.DParametro("@tfuact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_Mam_TV002", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If
        Return _resultado
    End Function

    Public Shared Function L_fnGrabarDetalle(_tfnumi As String, _type As Integer, _detalle As DataTable) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        '   @canumi ,@caalm,@cafdoc ,@caty4prov  ,@catven,
        '@cafvcr,@camon ,@caest  ,@caobs ,@cadesc ,@newFecha,@newHora,@cauact
        _listParam.Add(New Datos.DParametro("@tipo", _type))
        _listParam.Add(New Datos.DParametro("@tfnumi", _tfnumi))
        _listParam.Add(New Datos.DParametro("@TV0012", "", _detalle))
        _listParam.Add(New Datos.DParametro("@tfuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV002", _listParam)
        If _Tabla.Rows.Count > 0 Then
            _tfnumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnGrabarSaldos(_tfnumi As String, _tfobservacion As String, _tffecha As String) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        '   @canumi ,@caalm,@cafdoc ,@caty4prov  ,@catven,
        '@cafvcr,@camon ,@caest  ,@caobs ,@cadesc ,@newFecha,@newHora,@cauact
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@tfnumi", _tfnumi))
        _listParam.Add(New Datos.DParametro("@tffecha", _tffecha))
        _listParam.Add(New Datos.DParametro("@tfobservacion", _tfobservacion))
        _listParam.Add(New Datos.DParametro("@tfuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV002", _listParam)
        If _Tabla.Rows.Count > 0 Then
            _tfnumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnModificarSaldos(_tfnumi As String, _tfobservacion As String, _tffecha As String) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        '   @canumi ,@caalm,@cafdoc ,@caty4prov  ,@catven,
        '@cafvcr,@camon ,@caest  ,@caobs ,@cadesc ,@newFecha,@newHora,@cauact
        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@tfnumi", _tfnumi))
        _listParam.Add(New Datos.DParametro("@tffecha", _tffecha))
        _listParam.Add(New Datos.DParametro("@tfobservacion", _tfobservacion))
        _listParam.Add(New Datos.DParametro("@tfuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV002", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _tfnumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
#End Region

#Region "REPORTE DE SALDOS DE PRODUCTO AGRUPADOS POR LINEAS"
    Public Shared Function L_fnObtenerGruposLibreria() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_SaldosProducto", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnTodosAlmacenTodosLineas() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_SaldosProducto", _listParam)

        Return _Tabla
    End Function
    'funcion para obetener los productos menores al stock
    Public Shared Function L_fnTodosAlmacenTodosLineasMenoresStock() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 22))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_SaldosProducto", _listParam)

        Return _Tabla
    End Function
    'funcion para obtener mayores a cero Efrain
    Public Shared Function L_fnTodosAlmacenTodosLineasMayorCero() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_SaldosProducto", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnUnaAlmacenTodosLineas(numialmacen As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", numialmacen))
        _Tabla = D_ProcedimientoConParam("sp_Mam_SaldosProducto", _listParam)

        Return _Tabla
    End Function
    'funcion para obtener un Almacen todas las lineas canidad menores al stock
    Public Shared Function L_fnUnaAlmacenTodosLineasMenoresStock(numialmacen As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 33))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", numialmacen))
        _Tabla = D_ProcedimientoConParam("sp_Mam_SaldosProducto", _listParam)

        Return _Tabla
    End Function
    'un almacen todos linea y mayor a cero Efrain
    Public Shared Function L_fnUnaAlmacenTodosLineasMayorCero(numialmacen As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", numialmacen))
        _Tabla = D_ProcedimientoConParam("sp_Mam_SaldosProducto", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnTodosAlmacenUnaLineas(numiLinea As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@linea", numiLinea))
        _Tabla = D_ProcedimientoConParam("sp_Mam_SaldosProducto", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnTodosAlmacenUnaLineasMenoresStock(numiLinea As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 44))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@linea", numiLinea))
        _Tabla = D_ProcedimientoConParam("sp_Mam_SaldosProducto", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnUnaAlmacenUnaLineas(numiLinea As Integer, CodAlmacen As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@linea", numiLinea))
        _listParam.Add(New Datos.DParametro("@almacen", CodAlmacen))
        _Tabla = D_ProcedimientoConParam("sp_Mam_SaldosProducto", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnUnaAlmacenUnaLineasMenoresStock(numiLinea As Integer, CodAlmacen As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 55))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@linea", numiLinea))
        _listParam.Add(New Datos.DParametro("@almacen", CodAlmacen))
        _Tabla = D_ProcedimientoConParam("sp_Mam_SaldosProducto", _listParam)

        Return _Tabla
    End Function
    ' funcion mayor un almacen una linea y stock mayor a cero
    Public Shared Function L_fnUnaAlmacenUnaLineasMayorCero(numiLinea As Integer, CodAlmacen As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 8))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@linea", numiLinea))
        _listParam.Add(New Datos.DParametro("@almacen", CodAlmacen))
        _Tabla = D_ProcedimientoConParam("sp_Mam_SaldosProducto", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnSaldosMenoresAlStockProv(numialmacen As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", numialmacen))
        _Tabla = D_ProcedimientoConParam("sp_Mam_SaldosProducto", _listParam)

        Return _Tabla
    End Function
#End Region

#Region "CIERRE DE CAJA TCC001"

    Public Shared Function L_fnEliminarCaja(numi As String, fecha As String, _version As String, _ip As String, _usumaquina As String) As Boolean
        Dim _resultado As Boolean
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", -1))
        _listParam.Add(New Datos.DParametro("@ccnumi", numi))
        _listParam.Add(New Datos.DParametro("@fecha", fecha))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@ccuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TCC001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            numi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnGrabarCaja(ByRef _ccnumi As String, _fecha As String, _TotalGral As Decimal, _Credito As Decimal,
                                          _Tarjeta As Decimal, _QR As Decimal, _ContadoBs As Decimal, _Depositos As Decimal, _Efectivo As Decimal,
                                          _Diferencia As Decimal, _Pagos As Decimal, _Turno As String, _MInicial As Decimal,
                                          _Ingresos As Decimal, _Egresos As Decimal, _Estado As Integer, _TipoCambio As Decimal,
                                          _Obs As String, _TCC0011 As DataTable, _TCC0012 As DataTable, _NroCaja As Integer, _version As String,
                                          _ip As String, _usumaquina As String) As Boolean
        Dim _resultado As Boolean
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@ccnumi", _ccnumi))
        _listParam.Add(New Datos.DParametro("@fecha", _fecha))
        _listParam.Add(New Datos.DParametro("@TotalGral", _TotalGral))
        _listParam.Add(New Datos.DParametro("@Credito", _Credito))
        _listParam.Add(New Datos.DParametro("@Tarjeta", _Tarjeta))
        _listParam.Add(New Datos.DParametro("@QR", _QR))
        _listParam.Add(New Datos.DParametro("@ContadoBs", _ContadoBs))
        _listParam.Add(New Datos.DParametro("@Depositos", _Depositos))
        _listParam.Add(New Datos.DParametro("@Efectivo", _Efectivo))
        _listParam.Add(New Datos.DParametro("@Diferencia", _Diferencia))
        _listParam.Add(New Datos.DParametro("@Pagos", _Pagos))
        _listParam.Add(New Datos.DParametro("@Turno", _Turno))
        _listParam.Add(New Datos.DParametro("@MInicial", _MInicial))
        _listParam.Add(New Datos.DParametro("@Estado", _Estado))
        _listParam.Add(New Datos.DParametro("@Ingreso", _Ingresos))
        _listParam.Add(New Datos.DParametro("@Egreso", _Egresos))
        _listParam.Add(New Datos.DParametro("@TipoCambio", _TipoCambio))
        _listParam.Add(New Datos.DParametro("@Obs", _Obs))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@ccuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@Nrocaja", _NroCaja))
        _listParam.Add(New Datos.DParametro("@TCC0011", "", _TCC0011))
        _listParam.Add(New Datos.DParametro("@TCC0012", "", _TCC0012))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TCC001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _ccnumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnModificarCaja(ByRef _ccnumi As String, _fecha As String, _TotalGral As Decimal, _Credito As Decimal,
                                          _Tarjeta As Decimal, _QR As Decimal, _ContadoBs As Decimal, _Depositos As Decimal, _Efectivo As Decimal,
                                          _Diferencia As Decimal, _Pagos As Decimal, _Turno As String, _MInicial As Decimal,
                                          _Ingresos As Decimal, _Egresos As Decimal, _TipoCambio As Decimal, _Obs As String,
                                          _TCC0011 As DataTable, _TCC0012 As DataTable, _TCC0013 As DataTable, _NroCaja As Integer,
                                          _version As String, _ip As String, _usumaquina As String) As Boolean
        Dim _resultado As Boolean
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@ccnumi", _ccnumi))
        _listParam.Add(New Datos.DParametro("@fecha", _fecha))
        _listParam.Add(New Datos.DParametro("@TotalGral", _TotalGral))
        _listParam.Add(New Datos.DParametro("@Credito", _Credito))
        _listParam.Add(New Datos.DParametro("@Tarjeta", _Tarjeta))
        _listParam.Add(New Datos.DParametro("@QR", _QR))
        _listParam.Add(New Datos.DParametro("@ContadoBs", _ContadoBs))
        _listParam.Add(New Datos.DParametro("@Depositos", _Depositos))
        _listParam.Add(New Datos.DParametro("@Efectivo", _Efectivo))
        _listParam.Add(New Datos.DParametro("@Diferencia", _Diferencia))
        _listParam.Add(New Datos.DParametro("@Pagos", _Pagos))
        _listParam.Add(New Datos.DParametro("@Turno", _Turno))
        _listParam.Add(New Datos.DParametro("@MInicial", _MInicial))
        _listParam.Add(New Datos.DParametro("@Estado", 0))
        _listParam.Add(New Datos.DParametro("@Ingreso", _Ingresos))
        _listParam.Add(New Datos.DParametro("@Egreso", _Egresos))
        _listParam.Add(New Datos.DParametro("@TipoCambio", _TipoCambio))
        _listParam.Add(New Datos.DParametro("@Obs", _Obs))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@ccuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@Nrocaja", _NroCaja))
        _listParam.Add(New Datos.DParametro("@TCC0011", "", _TCC0011))
        _listParam.Add(New Datos.DParametro("@TCC0012", "", _TCC0012))
        _listParam.Add(New Datos.DParametro("@TCC0013", "", _TCC0013))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TCC001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _ccnumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prCajaGeneral() As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@ccuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TCC001", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_fnDetalleVentasPagos(fecha As String, Nrocaja As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@fecha", fecha))
        _listParam.Add(New Datos.DParametro("@Nrocaja", Nrocaja))
        _listParam.Add(New Datos.DParametro("@ccuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TCC001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnDetalleCortes(idCaja As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@ccnumi", idCaja))
        _listParam.Add(New Datos.DParametro("@ccuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TCC001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnDetalleDepositos(idCaja As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@ccnumi", idCaja))
        _listParam.Add(New Datos.DParametro("@ccuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TCC001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prListarSoloBanco() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@ccuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TCC001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnVerificarSiExisteCierreCaja(fecha As String, ccnumi As String, Nrocaja As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 8))
        _listParam.Add(New Datos.DParametro("@fecha", fecha))
        _listParam.Add(New Datos.DParametro("@ccnumi", ccnumi))
        _listParam.Add(New Datos.DParametro("@Nrocaja", Nrocaja))
        _listParam.Add(New Datos.DParametro("@ccuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TCC001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnDetalleVentasPagosPorIdCaja(fecha As String, ccnumi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@fecha", fecha))
        _listParam.Add(New Datos.DParametro("@ccnumi", ccnumi))
        _listParam.Add(New Datos.DParametro("@ccuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TCC001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnVerificarCajaAbierta(Nrocaja As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 10))
        _listParam.Add(New Datos.DParametro("@Nrocaja", Nrocaja))
        _listParam.Add(New Datos.DParametro("@ccuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TCC001", _listParam)

        Return _Tabla
    End Function
#End Region

#Region "BANCOS"

    Public Shared Function L_prBancoGeneral() As DataTable
        Dim _Tabla As DataTable
        't.canumi , t.canombre, t.cacuenta, t.caobs, t.cafact, t.cahact, t.cauact 
        Dim _listPalam As New List(Of Datos.DParametro)

        _listPalam.Add(New Datos.DParametro("@tipo", 3))
        _listPalam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_BA001", _listPalam)

        Return _Tabla
    End Function
    Public Shared Function L_prBancoGrabar(ByRef _canumi As String, _canombre As String, _cacuenta As String, _caobs As String,
                                           _img As String, _estado As Integer, _version As String, _ip As String, _usumaquina As String) As Boolean
        Dim _resultado As Boolean
        Dim _Tabla As DataTable
        Dim _listPalam As New List(Of Datos.DParametro)

        _listPalam.Add(New Datos.DParametro("@tipo", 1))
        _listPalam.Add(New Datos.DParametro("@canumi", _canumi))
        _listPalam.Add(New Datos.DParametro("@canombre", _canombre))
        _listPalam.Add(New Datos.DParametro("@canrocuenta", _cacuenta))
        _listPalam.Add(New Datos.DParametro("@caobs", _caobs))
        _listPalam.Add(New Datos.DParametro("@caestado", _estado))
        _listPalam.Add(New Datos.DParametro("@caimg", _img))
        _listPalam.Add(New Datos.DParametro("@version", _version))
        _listPalam.Add(New Datos.DParametro("@ip", _ip))
        _listPalam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listPalam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_BA001", _listPalam)

        If _Tabla.Rows.Count > 0 Then
            _canumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prBancoBorrar(_numi As String, ByRef _mensaje As String, _version As String, _ip As String,
                                           _usumaquina As String) As Boolean

        Dim _resultado As Boolean

        If L_fnbValidarEliminacion(_numi, "BA001", "canumi", _mensaje) = True Then
            Dim _Tabla As DataTable

            Dim _listPalam As New List(Of Datos.DParametro)

            _listPalam.Add(New Datos.DParametro("@tipo", -1))
            _listPalam.Add(New Datos.DParametro("@canumi", _numi))
            _listPalam.Add(New Datos.DParametro("@version", _version))
            _listPalam.Add(New Datos.DParametro("@ip", _ip))
            _listPalam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
            _listPalam.Add(New Datos.DParametro("@cauact", L_Usuario))
            _Tabla = D_ProcedimientoConParam("sp_Mam_BA001", _listPalam)

            If _Tabla.Rows.Count > 0 Then
                _numi = _Tabla.Rows(0).Item(0)
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prBancoModificar(ByRef _canumi As String, _canombre As String, _cacuenta As String, _caobs As String,
                                           _img As String, _estado As Integer, _version As String, _ip As String, _usumaquina As String) As Boolean
        Dim _resultado As Boolean
        Dim _Tabla As DataTable
        Dim _listPalam As New List(Of Datos.DParametro)
        't.canumi , t.canombre, t.cacuenta, t.caobs, t.cafact, t.cahact, t.cauact 
        _listPalam.Add(New Datos.DParametro("@tipo", 2))
        _listPalam.Add(New Datos.DParametro("@canumi", _canumi))
        _listPalam.Add(New Datos.DParametro("@canombre", _canombre))
        _listPalam.Add(New Datos.DParametro("@canrocuenta", _cacuenta))
        _listPalam.Add(New Datos.DParametro("@caobs", _caobs))
        _listPalam.Add(New Datos.DParametro("@caestado", _estado))
        _listPalam.Add(New Datos.DParametro("@caimg", _img))
        _listPalam.Add(New Datos.DParametro("@version", _version))
        _listPalam.Add(New Datos.DParametro("@ip", _ip))
        _listPalam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listPalam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_BA001", _listPalam)

        If _Tabla.Rows.Count > 0 Then
            _canumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

#End Region
#Region "INGRESOS/EGRESOS"

    Public Shared Function L_prIngresoEgresoGeneral() As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@ieuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TIE001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prIngresoEgresoGrabar(ByRef _ienumi As String, _ieFecha As String, _ieTipo As String,
                                           _ieDescripcion As String, _ieConcepto As String, _ieMonto As Decimal,
                                           _ieObs As String, _NroCaja As Integer, _version As String, _ip As String,
                                           _usumaquina As String) As Boolean
        Dim _resultado As Boolean
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@ienumi", _ienumi))
        _listParam.Add(New Datos.DParametro("@ieFecha", _ieFecha))
        _listParam.Add(New Datos.DParametro("@ieTipo", _ieTipo))
        _listParam.Add(New Datos.DParametro("@ieDescripcion", _ieDescripcion))
        _listParam.Add(New Datos.DParametro("@ieConcepto", _ieConcepto))
        _listParam.Add(New Datos.DParametro("@ieMonto", _ieMonto))
        _listParam.Add(New Datos.DParametro("@ieObs", _ieObs))
        _listParam.Add(New Datos.DParametro("@ieEstado", 1))
        _listParam.Add(New Datos.DParametro("@Nrocaja", _NroCaja))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@ieuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TIE001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _ienumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prIngresoEgresoBorrar(_numi As String, ByRef _mensaje As String, _version As String, _ip As String, _usumaquina As String) As Boolean

        Dim _resultado As Boolean

        If L_fnbValidarEliminacion(_numi, "TIE001", "ienumi", _mensaje) = True Then
            Dim _Tabla As DataTable

            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@ienumi", _numi))
            _listParam.Add(New Datos.DParametro("@version", _version))
            _listParam.Add(New Datos.DParametro("@ip", _ip))
            _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
            _listParam.Add(New Datos.DParametro("@ieuact", L_Usuario))
            _Tabla = D_ProcedimientoConParam("sp_Mam_TIE001", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _numi = _Tabla.Rows(0).Item(0)
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prIngresoEgresoModificar(ByRef _ienumi As String, _ieFecha As String, _ieTipo As String,
                                           _ieDescripcion As String, _ieConcepto As String, _ieMonto As Decimal,
                                           _ieObs As String, _version As String, _ip As String, _usumaquina As String) As Boolean
        Dim _resultado As Boolean
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@ienumi", _ienumi))
        _listParam.Add(New Datos.DParametro("@ieFecha", _ieFecha))
        _listParam.Add(New Datos.DParametro("@ieTipo", _ieTipo))
        _listParam.Add(New Datos.DParametro("@ieDescripcion", _ieDescripcion))
        _listParam.Add(New Datos.DParametro("@ieConcepto", _ieConcepto))
        _listParam.Add(New Datos.DParametro("@ieMonto", _ieMonto))
        _listParam.Add(New Datos.DParametro("@ieObs", _ieObs))
        _listParam.Add(New Datos.DParametro("@ieEstado", 2))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@ieuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TIE001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _ienumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_prIngresoEgresoPorFecha(fecha As String, NroCaja As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@ieFecha", fecha))
        _listParam.Add(New Datos.DParametro("@Nrocaja", NroCaja))
        _listParam.Add(New Datos.DParametro("@ieuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TIE001", _listParam)

        Return _Tabla
    End Function
#End Region
#Region "Empresa tipo de reporte"
    Public Shared Function ObtenerEmpresaHabilitada() As Integer
        Dim _Tabla As DataTable
        Dim _resultado As Integer
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _Tabla = D_ProcedimientoConParam("sp_EmpresaReporte", _listParam)
        If _Tabla.Rows.Count > 0 Then
            _resultado = _Tabla.Rows(0).Item("Id")
        Else
            _resultado = 0
        End If
        Return _resultado
    End Function
    Public Shared Function ObtenerEmpresaTipoReporte(empresaId As Integer, reporteId As Integer) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@empresaId", empresaId))
        _listParam.Add(New Datos.DParametro("@reporteId", reporteId))
        _Tabla = D_ProcedimientoConParam("sp_EmpresaReporte", _listParam)
        Return _Tabla
    End Function
#End Region

#Region "Descuento por Proveedor"
    Public Shared Function ObtenerDescuentoPorProveedor() As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 21))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)
        Return _Tabla
    End Function
    Public Shared Function ObtenerProveedorIDXProducto(productoId As Integer) As Integer
        Dim _Tabla As DataTable
        Dim _resultado As Integer
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 22))
        _listParam.Add(New Datos.DParametro("@producto", productoId))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = _Tabla.Rows(0).Item("ProveedorId")
        Else
            _resultado = 0
        End If
        Return _resultado

    End Function
#End Region
#Region "Pesaje de Productos"
    Public Shared Function L_fnGrabarPesajeProducto(ByRef _pcnumi As String, _pcfecha As String, _pccodPro As String,
                                          _pcprecio As Double, _pcpesoreal As Double, _pcfvenc As String, _pcpeso As Double,
                                          _pctotal As Double, _pccbarra As String, _pccbarraimp As String, _version As String,
                                          _ip As String, _usumaquina As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@pcnumi", _pcnumi))
        _listParam.Add(New Datos.DParametro("@pcfecha", _pcfecha))
        _listParam.Add(New Datos.DParametro("@pccodPro", _pccodPro))
        _listParam.Add(New Datos.DParametro("@pcprecio", _pcprecio))
        _listParam.Add(New Datos.DParametro("@pcpesoreal", _pcpesoreal))
        _listParam.Add(New Datos.DParametro("@pcfvenc", _pcfvenc))
        _listParam.Add(New Datos.DParametro("@pcpeso", _pcpeso))
        _listParam.Add(New Datos.DParametro("@pctotal", _pctotal))
        _listParam.Add(New Datos.DParametro("@pccbarra", _pccbarra))
        _listParam.Add(New Datos.DParametro("@pccbarraimp", _pccbarraimp))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@pcuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TPesaje001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _pcnumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnModificarPesajeProducto(ByRef _pcnumi As String, _pcfecha As String, _pccodPro As String,
                                          _pcprecio As Double, _pcpesoreal As Double, _pcfvenc As String, _pcpeso As Double,
                                          _pctotal As Double, _pccbarra As String, _pccbarraimp As String, _version As String,
                                          _ip As String, _usumaquina As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@pcnumi", _pcnumi))
        _listParam.Add(New Datos.DParametro("@pcfecha", _pcfecha))
        _listParam.Add(New Datos.DParametro("@pccodPro", _pccodPro))
        _listParam.Add(New Datos.DParametro("@pcprecio", _pcprecio))
        _listParam.Add(New Datos.DParametro("@pcpesoreal", _pcpesoreal))
        _listParam.Add(New Datos.DParametro("@pcfvenc", _pcfvenc))
        _listParam.Add(New Datos.DParametro("@pcpeso", _pcpeso))
        _listParam.Add(New Datos.DParametro("@pctotal", _pctotal))
        _listParam.Add(New Datos.DParametro("@pccbarra", _pccbarra))
        _listParam.Add(New Datos.DParametro("@pccbarraimp", _pccbarraimp))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@pcuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TPesaje001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _pcnumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnGeneralPesajeProductos(mostrar As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@mostrar", mostrar))
        _listParam.Add(New Datos.DParametro("@pcuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TPesaje001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnEliminarPesajeProducto(numi As String, ByRef mensaje As String, _version As String, _ip As String,
                                                      _usumaquina As String) As Boolean
        Dim _resultado As Boolean
        If L_fnbValidarEliminacion(numi, "TPesaje001", "pcnumi", mensaje) = True Then
            Dim _Tabla As DataTable
            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@pcnumi", numi))
            _listParam.Add(New Datos.DParametro("@version", _version))
            _listParam.Add(New Datos.DParametro("@ip", _ip))
            _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
            _listParam.Add(New Datos.DParametro("@pcuact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_Mam_TPesaje001", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If
        Return _resultado
    End Function
    Public Shared Function L_fnCodigoBarraUnProducto(pcnumi As String) As DataTable

        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@pcnumi", pcnumi))
        _listParam.Add(New Datos.DParametro("@pcuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TPesaje001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnImpresionPesaje(_version As String, _ip As String, _usumaquina As String, _codPro As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@pcnumi", _codPro))
        _listParam.Add(New Datos.DParametro("@pcuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TPesaje001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnExportarPesaje(_version As String, _ip As String, _usumaquina As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@pcuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TPesaje001", _listParam)

        Return _Tabla
    End Function
#End Region

#Region "Reporte Saldos"
    Public Shared Function L_prReporteUtilidadNueva(_codAlmacen As Integer, _codCat As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", _codAlmacen))
        _listParam.Add(New Datos.DParametro("@catPrecio", _codCat))
        _Tabla = D_ProcedimientoConParam("Mam_ReporteSaldos", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prReporteUtilidadAlNueva(_codAlmacen As Integer, _codCat As Integer, _date1 As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", _codAlmacen))
        _listParam.Add(New Datos.DParametro("@catPrecio", _codCat))
        _listParam.Add(New Datos.DParametro("@fechaF", _date1))
        _Tabla = D_ProcedimientoConParam("Mam_ReporteSaldos", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prReporteUtilidadmayorNuevo(_codAlmacen As Integer, _codCat As Integer, _date1 As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", _codAlmacen))
        _listParam.Add(New Datos.DParametro("@catPrecio", _codCat))
        _listParam.Add(New Datos.DParametro("@fechaF", _date1))
        _Tabla = D_ProcedimientoConParam("Mam_ReporteSaldos", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prReporteUtilidadConsolidadoNuevo(_codAlmacen As Integer, _codCat As Integer, _date1 As String, MayorCero As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", _codAlmacen))
        _listParam.Add(New Datos.DParametro("@catPrecio", _codCat))
        _listParam.Add(New Datos.DParametro("@fechaF", _date1))
        _listParam.Add(New Datos.DParametro("@MayorCero", MayorCero))
        _Tabla = D_ProcedimientoConParam("Mam_ReporteSaldos", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prReporteUtilidadStockMayorCeroNuevo(_codAlmacen As Integer, _codCat As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", _codAlmacen))
        _listParam.Add(New Datos.DParametro("@catPrecio", _codCat))
        _Tabla = D_ProcedimientoConParam("Mam_ReporteSaldos", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prReporteUtilidadNuevo(_codAlmacen As Integer, _codCat As Integer, _date1 As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", _codAlmacen))
        _listParam.Add(New Datos.DParametro("@catPrecio", _codCat))
        _listParam.Add(New Datos.DParametro("@fechaF", _date1))
        _Tabla = D_ProcedimientoConParam("Mam_ReporteSaldos", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prReporteUtilidadmayorNuevo2(_codAlmacen As Integer, _codCat As Integer, _date1 As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", _codAlmacen))
        _listParam.Add(New Datos.DParametro("@catPrecio", _codCat))
        _listParam.Add(New Datos.DParametro("@fechaF", _date1))
        _Tabla = D_ProcedimientoConParam("Mam_ReporteSaldos", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prReporteUtilidadStockMayorCeroNuevo2(_codAlmacen As Integer, _codCat As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 8))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", _codAlmacen))
        _listParam.Add(New Datos.DParametro("@catPrecio", _codCat))
        _Tabla = D_ProcedimientoConParam("Mam_ReporteSaldos", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prReporteUtilidadNueva2(_codAlmacen As Integer, _codCat As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", _codAlmacen))
        _listParam.Add(New Datos.DParametro("@catPrecio", _codCat))
        _Tabla = D_ProcedimientoConParam("Mam_ReporteSaldos", _listParam)

        Return _Tabla
    End Function

#End Region


#Region "Cálculo Pedido Sugerido"
    Public Shared Function L_CalculoPedidoSugerido(fechaI As String, fechaF As String, CodProv As String, nroSemVentas As Integer, nroSemPedido As Integer) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@proveedor", CodProv))
        _listParam.Add(New Datos.DParametro("@nroSemVentas", nroSemVentas))
        _listParam.Add(New Datos.DParametro("@nroSemPedido", nroSemPedido))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("ProcCalculoPedSugerido", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_RevStockVentaProv(fechaI As String, fechaF As String, nroSemVentas As Integer, nroSemPedido As Integer) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@nroSemVentas", nroSemVentas))
        _listParam.Add(New Datos.DParametro("@nroSemPedido", nroSemPedido))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("ProcCalculoPedSugerido", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_CalculoPedidoSugeridoDias(fechaI As String, fechaF As String, CodProv As String, nroDiasVentas As Integer, nroDiasPedido As Integer) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@proveedor", CodProv))
        _listParam.Add(New Datos.DParametro("@nroDiasVentas", nroDiasVentas))
        _listParam.Add(New Datos.DParametro("@nroDiasPedido", nroDiasPedido))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("ProcCalculoPedSugerido", _listParam)
        Return _Tabla
    End Function
#End Region

#Region "Conteo Físico"
    Public Shared Function L_fnImportarInventarioFisico(_dtConteo As DataTable, _version As String, _ip As String, _usumaquina As String) As Boolean
        Dim _resultado As Boolean
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@TConteo001", "", _dtConteo))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@cguact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("ProcConteoFisico", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnVerificarGrabadoConteo(resp As String, fecha As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@responsable", resp))
        _listParam.Add(New Datos.DParametro("@fechaInv", fecha))
        _listParam.Add(New Datos.DParametro("@cguact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("ProcConteoFisico", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarConteoUsuario(usu As String, fecha As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@responsable", usu))
        _listParam.Add(New Datos.DParametro("@fechaInv", fecha))
        _listParam.Add(New Datos.DParametro("@cguact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("ProcConteoFisico", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnMostrarUsuariosConteo() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _Tabla = D_ProcedimientoConParam("ProcConteoFisico", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnMostrarProductosXresponsable(responsable As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@responsable", responsable))
        _listParam.Add(New Datos.DParametro("@cguact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("ProcConteoFisico", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnStockSistemavsFisico(fecha As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@fechaInv", fecha))
        _listParam.Add(New Datos.DParametro("@cguact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("ProcConteoFisico", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnExcelConteoFisico(_version As String, _ip As String, _usumaquina As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@cguact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("ProcConteoFisico", _listParam)

        Return _Tabla
    End Function
#End Region

#Region "Movimiento de Productos por Peso TI003"

    Public Shared Function L_prMovimientoEliminarTI003(numi As String, _version As String, _ip As String, _usumaquina As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", -1))
        _listParam.Add(New Datos.DParametro("@ibid", numi))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("ProcMam_TI003", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_prMovimientoChoferGrabarTI003(ByRef _ibid As String, _ibfdoc As String, _ibconcep As Integer,
                                                      _ibobs As String, _almacen As Integer, _depositoDestino As Integer,
                                                      _ibidOrigen As Integer, _detalle As DataTable, _motivo As Integer,
                                                      _version As String, _ip As String, _usumaquina As String) As Boolean
        Dim _resultado As Boolean
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@ibid", _ibid))
        _listParam.Add(New Datos.DParametro("@ibfdoc", _ibfdoc))
        _listParam.Add(New Datos.DParametro("@ibconcep", _ibconcep))
        _listParam.Add(New Datos.DParametro("@ibobs", _ibobs))
        _listParam.Add(New Datos.DParametro("@ibest", 1))
        _listParam.Add(New Datos.DParametro("@ibalm", _almacen))
        _listParam.Add(New Datos.DParametro("@ibdepdest", _depositoDestino))
        _listParam.Add(New Datos.DParametro("@ibiddc", 0))
        _listParam.Add(New Datos.DParametro("@ibidOrigen", _ibidOrigen))
        _listParam.Add(New Datos.DParametro("@motivo", _motivo))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))

        _listParam.Add(New Datos.DParametro("@TI0021", "", _detalle))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("ProcMam_TI003", _listParam)
        If _Tabla.Rows.Count > 0 Then
            _ibid = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_prMovimientoModificarTI003(ByRef _ibid As String, _ibfdoc As String, _ibconcep As Integer, _ibobs As String,
                                                        _almacen As Integer, _detalle As DataTable, _version As String, _ip As String,
                                                        _usumaquina As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@ibid", _ibid))
        _listParam.Add(New Datos.DParametro("@ibfdoc", _ibfdoc))
        _listParam.Add(New Datos.DParametro("@ibconcep", _ibconcep))
        _listParam.Add(New Datos.DParametro("@ibobs", _ibobs))
        _listParam.Add(New Datos.DParametro("@ibest", 1))
        _listParam.Add(New Datos.DParametro("@ibalm", _almacen))
        _listParam.Add(New Datos.DParametro("@ibiddc", 0))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))

        _listParam.Add(New Datos.DParametro("@TI0021", "", _detalle))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("ProcMam_TI003", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _ibid = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnGeneralMovimientoTI003(mostrar As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@mostrar", mostrar))
        _Tabla = D_ProcedimientoConParam("ProcMam_TI003", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnDetalleMovimientoTI003(_ibid As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@ibid", _ibid))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("ProcMam_TI003", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnVerificarStockTI003(_CodPro As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@cbty5prod", _CodPro))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("ProcMam_TI003", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnVerificarProdTI003(_CodPro As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@cbty5prod", _CodPro))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("ProcMam_TI003", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prMovimientoConceptoTI003() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("ProcMam_TI003", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarProductosKardexTI003(_almacen As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)


        _listParam.Add(New Datos.DParametro("@tipo", 8))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _Tabla = D_ProcedimientoConParam("ProcMam_TI003", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnObtenerHistorialProductoTI003(_codProducto As Integer, FechaI As String, FechaF As String, _almacen As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@producto", _codProducto))
        _listParam.Add(New Datos.DParametro("@fechaI", FechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", FechaF))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _Tabla = D_ProcedimientoConParam("ProcMam_TI003", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnObtenerHistorialProductoGeneralTI003(_codProducto As Integer, FechaI As String, _almacen As Integer) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 20))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@producto", _codProducto))
        _listParam.Add(New Datos.DParametro("@fechaI", FechaI))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _Tabla = D_ProcedimientoConParam("ProcMam_TI003", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarDepositosTI003() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 24))
        _Tabla = D_ProcedimientoConParam("ProcMam_TI003", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnObtenerHistorialProductoGeneralPorLoteTI003(_codProducto As Integer, FechaI As String, _almacen As Integer, _Lote As String, FechaVenc As String) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 29))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@producto", _codProducto))
        _listParam.Add(New Datos.DParametro("@fechaI", FechaI))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@lote", _Lote))
        _listParam.Add(New Datos.DParametro("@fechaVenc", FechaVenc))
        _Tabla = D_ProcedimientoConParam("ProcMam_TI003", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnObtenerHistorialProductoporLoteTI003(_codProducto As Integer, FechaI As String, FechaF As String, _almacen As Integer, _Lote As String, _FechaVenc As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 30))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@producto", _codProducto))
        _listParam.Add(New Datos.DParametro("@fechaI", FechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", FechaF))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@lote", _Lote))
        _listParam.Add(New Datos.DParametro("@fechaVenc", _FechaVenc))
        _Tabla = D_ProcedimientoConParam("ProcMam_TI003", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarLotesPorProductoMovimientoTI003(_almacen As Integer, _codproducto As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 32))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@producto", _codproducto))
        _listParam.Add(New Datos.DParametro("ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("ProcMam_TI003", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prMovimientoListarProductosTI003(dt As DataTable, _deposito As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", _deposito))
        _Tabla = D_ProcedimientoConParam("sp_Marco_TI002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prMovimientoListarUnProductoTI003(_deposito As Integer, _CodPro As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 8))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", _deposito))
        _listParam.Add(New Datos.DParametro("@producto", _CodPro))
        _Tabla = D_ProcedimientoConParam("sp_Marco_TI002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prMovimientoListarProductosConLoteTI003(_deposito As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 32))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", _deposito))
        _Tabla = D_ProcedimientoConParam("sp_Marco_TI002", _listParam)

        Return _Tabla
    End Function
#End Region
#Region "TABLA PARA ALMACENAR COMBOS TY0053"

    Public Shared Function L_fnListarProductosActivos() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)


        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("Proc_TY0053", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnProductosCombo(codigo As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@yfnumi", codigo))
        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("Proc_TY0053", _listParam)

        Return _Tabla
    End Function
#End Region
#Region "ARMAR Y DESARMAR COMBOS"

    Public Shared Function L_fnMovimientoComboGrabar(ByRef id As String, fdoc As String, obs As String, codpack As String, cantP As Integer,
                                                     pcosto As String, cantNP As Integer, est As String, alm As String, TI0051 As DataTable,
                                                     _version As String, _ip As String, _usumaquina As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@id", id))
        _listParam.Add(New Datos.DParametro("@fdoc", fdoc))
        _listParam.Add(New Datos.DParametro("@obs", obs))
        _listParam.Add(New Datos.DParametro("@codpack", codpack))
        _listParam.Add(New Datos.DParametro("@cantP", cantP))
        _listParam.Add(New Datos.DParametro("@pcosto", pcosto))
        _listParam.Add(New Datos.DParametro("@cantNP", cantNP))
        _listParam.Add(New Datos.DParametro("@est", 1))
        _listParam.Add(New Datos.DParametro("@alm", alm))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))

        _listParam.Add(New Datos.DParametro("@TI0051", "", TI0051))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("Proc_TI005", _listParam)

        If _Tabla.Rows.Count > 0 Then
            id = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnMovimientoComboGeneral() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_TI005", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnMovimientoComboDetalle(id As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@id", id))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("Proc_TI005", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnDetalleCombo(id As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@id", id))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("Proc_TI005", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnMovimientoComboDetalleTI0052(id As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@id", id))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("Proc_TI005", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnGrabarDesarmado(ByRef id As String, codpack As String, cantNP As Integer, fechaNP As String, TI0052 As DataTable,
                                               _version As String, _ip As String, _usumaquina As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@id", id))
        _listParam.Add(New Datos.DParametro("@codpack", codpack))
        _listParam.Add(New Datos.DParametro("@cantNP", cantNP))
        _listParam.Add(New Datos.DParametro("@fechaNP", fechaNP))
        _listParam.Add(New Datos.DParametro("@TI0052", "", TI0052))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("Proc_TI005", _listParam)

        If _Tabla.Rows.Count > 0 Then
            id = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnVerificarStockCombo(codpack As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 8))
        _listParam.Add(New Datos.DParametro("@codpack", codpack))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("Proc_TI005", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnProductosCombo() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("Proc_TI005", _listParam)

        Return _Tabla
    End Function
#End Region

#Region "REGISTRAR TFV0012"
    Public Shared Function L_fnRegistrarTFV0012(numi As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@tanumi", numi))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_TFV0012", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_FacturasAnuladasFechaAnul(fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_TFV0012", _listParam)
        Return _Tabla
    End Function
#End Region
#Region "REGISTRO DE BITÁCORAS"
    Public Shared Function L_fnRepConsultaPreciosCajeros(_version As String, _ip As String, _usumaquina As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReportesGenerados", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnRepConsultaPreciosCajerosExcel(_version As String, _ip As String, _usumaquina As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReportesGenerados", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnExcelTodasLasCompras(_version As String, _ip As String, _usumaquina As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReportesGenerados", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnRepConsultaPVentaMenorPCosto(_version As String, _ip As String, _usumaquina As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReportesGenerados", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnExcelPVentaMenorPCosto(_version As String, _ip As String, _usumaquina As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReportesGenerados", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnRepConsultaListaClientesCel(_version As String, _ip As String, _usumaquina As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReportesGenerados", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnExcelListaClientesCel(_version As String, _ip As String, _usumaquina As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReportesGenerados", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnGenerarKardex(_version As String, _ip As String, _usumaquina As String, _codPro As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 8))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@cod", _codPro))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReportesGenerados", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnImpresionKardex(_version As String, _ip As String, _usumaquina As String, _codPro As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@cod", _codPro))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReportesGenerados", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnExportarKardex(_version As String, _ip As String, _usumaquina As String, _codPro As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 10))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@cod", _codPro))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReportesGenerados", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnRepConsultaSaldoProd(_version As String, _ip As String, _usumaquina As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 11))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReportesGenerados", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnRepConsultaSaldosValorados(_version As String, _ip As String, _usumaquina As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 12))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReportesGenerados", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnRepConsultaSaldosMenores(_version As String, _ip As String, _usumaquina As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 13))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReportesGenerados", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnRepConsultaSaldosExcel(_version As String, _ip As String, _usumaquina As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 14))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReportesGenerados", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnExcelSaldosValorados(_version As String, _ip As String, _usumaquina As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 15))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReportesGenerados", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnRepProdNoEntraronCompra(_version As String, _ip As String, _usumaquina As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 16))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReportesGenerados", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnExcelProdNoEntraronCompra(_version As String, _ip As String, _usumaquina As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 17))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReportesGenerados", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnRepPedidoSugeridoSemanas(_version As String, _ip As String, _usumaquina As String, codProveedor As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 18))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@cod", codProveedor))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReportesGenerados", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnExcelPedidoSugeridoSemanas(_version As String, _ip As String, _usumaquina As String, codProveedor As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 19))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@cod", codProveedor))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReportesGenerados", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnRepRevStockVentaProv(_version As String, _ip As String, _usumaquina As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 20))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReportesGenerados", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnExcelRevStockVentaProv(_version As String, _ip As String, _usumaquina As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 21))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReportesGenerados", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnRepStockSistVsFisico(_version As String, _ip As String, _usumaquina As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 22))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReportesGenerados", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnExcelStockSistVsFisico(_version As String, _ip As String, _usumaquina As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 23))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReportesGenerados", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnRepPedidoSugeridoDias(_version As String, _ip As String, _usumaquina As String, codProveedor As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 24))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@cod", codProveedor))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReportesGenerados", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnExcelPedidoSugeridoDias(_version As String, _ip As String, _usumaquina As String, codProveedor As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 25))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@cod", codProveedor))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReportesGenerados", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnGenerarKardexProdxPeso(_version As String, _ip As String, _usumaquina As String, _codPro As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 26))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@cod", _codPro))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReportesGenerados", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnExportarKardexProdxPeso(_version As String, _ip As String, _usumaquina As String, _codPro As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 27))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@cod", _codPro))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReportesGenerados", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnBotonImprimir(_version As String, _ip As String, _usumaquina As String, _codPro As String, _tabla1 As String,
                                             _modulo As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 28))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@cod", _codPro))
        _listParam.Add(New Datos.DParametro("@tabla", _tabla1))
        _listParam.Add(New Datos.DParametro("@modulo", _modulo))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReportesGenerados", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnBotonExportar(_version As String, _ip As String, _usumaquina As String, _codPro As String, _tabla1 As String,
                                         _modulo As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 29))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@cod", _codPro))
        _listParam.Add(New Datos.DParametro("@tabla", _tabla1))
        _listParam.Add(New Datos.DParametro("@modulo", _modulo))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReportesGenerados", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnBotonMovProdxPeso(_version As String, _ip As String, _usumaquina As String, _codPro As String, _tabla1 As String,
                                     _modulo As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 30))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@cod", _codPro))
        _listParam.Add(New Datos.DParametro("@tabla", _tabla1))
        _listParam.Add(New Datos.DParametro("@modulo", _modulo))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReportesGenerados", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnBotonGenerar(_version As String, _ip As String, _usumaquina As String, _codPro As String, _tabla1 As String,
                                 _modulo As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 31))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@cod", _codPro))
        _listParam.Add(New Datos.DParametro("@tabla", _tabla1))
        _listParam.Add(New Datos.DParametro("@modulo", _modulo))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReportesGenerados", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnBotonImprimirExportar(_version As String, _ip As String, _usumaquina As String, _codPro As String, _tabla1 As String,
                                         _modulo As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 32))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@cod", _codPro))
        _listParam.Add(New Datos.DParametro("@tabla", _tabla1))
        _listParam.Add(New Datos.DParametro("@modulo", _modulo))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReportesGenerados", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnBotonImportar(_version As String, _ip As String, _usumaquina As String, _codPro As String, _tabla1 As String,
                                             _modulo As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 33))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@cod", _codPro))
        _listParam.Add(New Datos.DParametro("@tabla", _tabla1))
        _listParam.Add(New Datos.DParametro("@modulo", _modulo))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReportesGenerados", _listParam)

        Return _Tabla
    End Function


    Public Shared Function L_RepBitacora(fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_ReporteBitacora", _listParam)
        Return _Tabla
    End Function
#End Region

#Region "IMPRESIÓN DE PRECIOS"
    Public Shared Function L_fnImpresionPrecios() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@yguact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_Precios", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnImpresionPreciosUno(codProd As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@codProd", codProd))
        _listParam.Add(New Datos.DParametro("@yguact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_Precios", _listParam)

        Return _Tabla
    End Function

#End Region

#Region "TIPOS DE DOCUMENTOS"

    Public Shared Function L_fnTiposDocIdentidad() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_TipoDoc", _listParam)

        Return _Tabla
    End Function
#End Region

#Region "VALES"
    Public Shared Function L_fnEliminarVale(numi As String, codMov As String, ByRef mensaje As String, _version As String, _ip As String,
                                                _usumaquina As String) As Boolean
        Dim _resultado As Boolean
        If L_fnbValidarEliminacion(numi, "TVale001", "vanumi", mensaje) = True Then
            Dim _Tabla As DataTable
            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@vanumi", numi))
            _listParam.Add(New Datos.DParametro("@idMovimiento", codMov))
            _listParam.Add(New Datos.DParametro("@version", _version))
            _listParam.Add(New Datos.DParametro("@ip", _ip))
            _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
            _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
            _Tabla = D_ProcedimientoConParam("Proc_TVale001", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If
        Return _resultado
    End Function
    Public Shared Function L_fnGrabarVales(ByRef _idVale As String, _idMov As String, _nombEmp As String, _nroVale As String,
                                           _cantvale As Integer, _alm As Integer, _fecha As String, _nombCli As String, _ciCli As String,
                                           _montoVale As Double, _excedente As Double, _beneficio As Double, _obs As String, _subtotal As Double,
                                           _desc As Double, _total As Double, _NroCaja As Integer, _version As String, _ip As String,
                                           _usumaquina As String, detalle As DataTable) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@vanumi", _idVale))
        _listParam.Add(New Datos.DParametro("@idMovimiento", _idMov))
        _listParam.Add(New Datos.DParametro("@nombEmp", _nombEmp))
        _listParam.Add(New Datos.DParametro("@nroVale", _nroVale))
        _listParam.Add(New Datos.DParametro("@cantVales", _cantvale))
        _listParam.Add(New Datos.DParametro("@alm", _alm))
        _listParam.Add(New Datos.DParametro("@fdoc", _fecha))
        _listParam.Add(New Datos.DParametro("@nombCli", _nombCli))
        _listParam.Add(New Datos.DParametro("@ci", _ciCli))
        _listParam.Add(New Datos.DParametro("@MontoVale", _montoVale))
        _listParam.Add(New Datos.DParametro("@excedente", _excedente))
        _listParam.Add(New Datos.DParametro("@beneficio", _beneficio))
        _listParam.Add(New Datos.DParametro("@obs", _obs))
        _listParam.Add(New Datos.DParametro("@subtotal", _subtotal))
        _listParam.Add(New Datos.DParametro("@desc", _desc))
        _listParam.Add(New Datos.DParametro("@total", _total))
        _listParam.Add(New Datos.DParametro("@Nrocaja", _NroCaja))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))


        _listParam.Add(New Datos.DParametro("@TV0011", "", detalle))
        _Tabla = D_ProcedimientoConParam("Proc_TVale001", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _idVale = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnModificarVale(ByRef _vanumi As String, _empresa As String, _nrovales As String, _cantvales As Integer,
                                             vafdoc As String, _cliente As String, _ci As String, _montovale As Double, _excedente As Double,
                                             _beneficio As Double, _vaobs As String, _codMov As String, _version As String, _ip As String, _usumaquina As String) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@vanumi", _vanumi))
        _listParam.Add(New Datos.DParametro("@nombEmp", _empresa))
        _listParam.Add(New Datos.DParametro("@nroVale", _nrovales))
        _listParam.Add(New Datos.DParametro("@cantVales", _cantvales))
        _listParam.Add(New Datos.DParametro("@fdoc", vafdoc))
        _listParam.Add(New Datos.DParametro("@nombCli", _cliente))
        _listParam.Add(New Datos.DParametro("@ci", _ci))
        _listParam.Add(New Datos.DParametro("@MontoVale", _montovale))
        _listParam.Add(New Datos.DParametro("@excedente", _excedente))
        _listParam.Add(New Datos.DParametro("@beneficio", _beneficio))
        _listParam.Add(New Datos.DParametro("@obs", _vaobs))
        _listParam.Add(New Datos.DParametro("@idMovimiento", _codMov))
        _listParam.Add(New Datos.DParametro("@version", _version))
        _listParam.Add(New Datos.DParametro("@ip", _ip))
        _listParam.Add(New Datos.DParametro("@usumaquina", _usumaquina))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("Proc_TVale001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _vanumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnGeneralVale(mostrar As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@mostrar", mostrar))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_TVale001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnDetalleVale(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@vanumi", _numi))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_TVale001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_ValeProductosGeneral(fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_TVale001", _listParam)
        Return _Tabla
    End Function

    Public Shared Function L_ValeProductosDetallado(fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_TVale001", _listParam)
        Return _Tabla
    End Function

    Public Shared Function L_fnDetalleValePorCodigo(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@vanumi", _numi))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Proc_TVale001", _listParam)

        Return _Tabla
    End Function
#End Region

End Class
