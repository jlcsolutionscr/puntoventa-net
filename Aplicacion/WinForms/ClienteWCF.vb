Imports System.Collections.Generic
Imports System.Threading.Tasks
Imports LeandroSoftware.AccesoDatos.Dominio.Entidades
Imports LeandroSoftware.AccesoDatos.TiposDatos
Imports LeandroSoftware.Core
Imports LeandroSoftware.Core.CommonTypes
Imports LeandroSoftware.Puntoventa.Core
Imports Newtonsoft.Json

Public Class ClienteWCF
    Private Shared serializer = New CustomJavascriptSerializer()
#Region "Variables"

#End Region
#Region "Métodos"
    Public Shared Async Function ValidarCredenciales(strIdentificacion As String, strUsuario As String, strClave As String) As Task(Of Usuario)
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ValidarCredenciales",
            .DatosPeticion = "{Identificacion: '" + strIdentificacion + "', Usuario: '" + strUsuario + "', Clave: '" + strClave + "'}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim usuario As Usuario = Nothing
        If strRespuesta <> "" Then
            usuario = serializer.Deserialize(Of Usuario)(strRespuesta)
        End If
        Return usuario
    End Function

    Public Shared Async Function ObtenerTipoCambioDolar() As Task(Of Decimal)
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerTipoCambioDolar",
            .DatosPeticion = ""
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim decTipoCambioDolar As Decimal = Nothing
        If strRespuesta <> "" Then
            decTipoCambioDolar = serializer.Deserialize(Of Decimal)(strRespuesta)
        End If
        Return decTipoCambioDolar
    End Function

    Public Shared Async Function ObtenerListaTipoIdentificacion() As Task(Of List(Of TipoIdentificacion))
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerListaTipoIdentificacion",
            .DatosPeticion = ""
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim listado As List(Of TipoIdentificacion) = New List(Of TipoIdentificacion)()
        If strRespuesta <> "" Then
            listado = serializer.Deserialize(Of List(Of TipoIdentificacion))(strRespuesta)
        End If
        Return listado
    End Function

    Public Shared Async Function ObtenerListaProvincias() As Task(Of List(Of Provincia))
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerListaProvincias",
            .DatosPeticion = ""
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim listado As List(Of Provincia) = New List(Of Provincia)()
        If strRespuesta <> "" Then
            listado = serializer.Deserialize(Of List(Of Provincia))(strRespuesta)
        End If
        Return listado
    End Function

    Public Shared Async Function ObtenerListaCantones(intIdProvincia As Integer) As Task(Of List(Of Canton))
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerListaCantones",
            .DatosPeticion = "{IdProvincia: " + intIdProvincia.ToString() + "}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim listado As List(Of Canton) = New List(Of Canton)()
        If strRespuesta <> "" Then
            listado = serializer.Deserialize(Of List(Of Canton))(strRespuesta)
        End If
        Return listado
    End Function

    Public Shared Async Function ObtenerListaDistritos(intIdProvincia As Integer, intIdCanton As Integer) As Task(Of List(Of Distrito))
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerListaDistritos",
            .DatosPeticion = "{IdProvincia: " + intIdProvincia.ToString() + ", IdCanton: " + intIdCanton.ToString() + "}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim listado As List(Of Distrito) = New List(Of Distrito)()
        If strRespuesta <> "" Then
            listado = serializer.Deserialize(Of List(Of Distrito))(strRespuesta)
        End If
        Return listado
    End Function

    Public Shared Async Function ObtenerListaBarrios(intIdProvincia As Integer, intIdCanton As Integer, intIdDistrito As Integer) As Task(Of List(Of Barrio))
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerListaBarrios",
            .DatosPeticion = "{IdProvincia: " + intIdProvincia.ToString() + ", IdCanton: " + intIdCanton.ToString() + ", IdDistrito: " + intIdDistrito.ToString() + "}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim listado As List(Of Barrio) = New List(Of Barrio)()
        If strRespuesta <> "" Then
            listado = serializer.Deserialize(Of List(Of Barrio))(strRespuesta)
        End If
        Return listado
    End Function

    Public Shared Async Function ObtenerListaTipoProducto() As Task(Of List(Of TipoProducto))
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerListaTipoProducto",
            .DatosPeticion = ""
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim listado As List(Of TipoProducto) = New List(Of TipoProducto)()
        If strRespuesta <> "" Then
            listado = serializer.Deserialize(Of List(Of TipoProducto))(strRespuesta)
        End If
        Return listado
    End Function

    Public Shared Async Function ObtenerListaTipoUnidad() As Task(Of List(Of TipoUnidad))
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerListaTipoUnidad",
            .DatosPeticion = ""
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim listado As List(Of TipoUnidad) = New List(Of TipoUnidad)()
        If strRespuesta <> "" Then
            listado = serializer.Deserialize(Of List(Of TipoUnidad))(strRespuesta)
        End If
        Return listado
    End Function

    Public Shared Async Function ObtenerListaFormaPagoEgreso() As Task(Of List(Of FormaPago))
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerListaFormaPagoEgreso",
            .DatosPeticion = ""
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim listado As List(Of FormaPago) = New List(Of FormaPago)()
        If strRespuesta <> "" Then
            listado = serializer.Deserialize(Of List(Of FormaPago))(strRespuesta)
        End If
        Return listado
    End Function

    Public Shared Async Function ObtenerListaFormaPagoFactura() As Task(Of List(Of FormaPago))
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerListaFormaPagoFactura",
            .DatosPeticion = ""
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim listado As List(Of FormaPago) = New List(Of FormaPago)()
        If strRespuesta <> "" Then
            listado = serializer.Deserialize(Of List(Of FormaPago))(strRespuesta)
        End If
        Return listado
    End Function

    Public Shared Async Function ObtenerListaRoles() As Task(Of List(Of Role))
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerListaRoles",
            .DatosPeticion = ""
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim listado As List(Of Role) = New List(Of Role)()
        If strRespuesta <> "" Then
            listado = serializer.Deserialize(Of List(Of Role))(strRespuesta)
        End If
        Return listado
    End Function

    Public Shared Async Function ObtenerListaTipodePrecio() As Task(Of List(Of TipodePrecio))
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerListaTipodePrecio",
            .DatosPeticion = ""
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim listado As List(Of TipodePrecio) = New List(Of TipodePrecio)()
        If strRespuesta <> "" Then
            listado = serializer.Deserialize(Of List(Of TipodePrecio))(strRespuesta)
        End If
        Return listado
    End Function

    Public Shared Async Function ObtenerListaTipoMoneda() As Task(Of List(Of TipoMoneda))
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerListaTipoMoneda",
            .DatosPeticion = ""
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim listado As List(Of TipoMoneda) = New List(Of TipoMoneda)()
        If strRespuesta <> "" Then
            listado = serializer.Deserialize(Of List(Of TipoMoneda))(strRespuesta)
        End If
        Return listado
    End Function

    Public Shared Async Function ObtenerListaCondicionVenta() As Task(Of List(Of CondicionVenta))
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerListaCondicionVenta",
            .DatosPeticion = ""
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim listado As List(Of CondicionVenta) = New List(Of CondicionVenta)()
        If strRespuesta <> "" Then
            listado = serializer.Deserialize(Of List(Of CondicionVenta))(strRespuesta)
        End If
        Return listado
    End Function

    Public Shared Async Function ObtenerListaBancoAdquiriente(intIdEmpresa As Integer, Optional strDescripcion As String = "") As Task(Of List(Of BancoAdquiriente))
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerListaBancoAdquiriente",
            .DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + ", Descripcion: '" + strDescripcion + "'}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim listado As List(Of BancoAdquiriente) = New List(Of BancoAdquiriente)()
        If strRespuesta <> "" Then
            listado = serializer.Deserialize(Of List(Of BancoAdquiriente))(strRespuesta)
        End If
        Return listado
    End Function

    Public Shared Async Function AgregarBancoAdquiriente(bancoAdquiriente As BancoAdquiriente) As Task(Of String)
        Dim strDatos As String = serializer.Serialize(bancoAdquiriente)
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "AgregarBancoAdquiriente",
            .DatosPeticion = strDatos
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Return strRespuesta
    End Function

    Public Shared Async Function ActualizarBancoAdquiriente(bancoAdquiriente As BancoAdquiriente) As Task
        Dim strDatos As String = serializer.Serialize(bancoAdquiriente)
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ActualizarBancoAdquiriente",
            .DatosPeticion = strDatos
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Await Utilitario.Ejecutar(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
    End Function

    Public Shared Async Function ObtenerBancoAdquiriente(intIdBanco As Integer) As Task(Of BancoAdquiriente)
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerBancoAdquiriente",
            .DatosPeticion = "{IdBancoAdquiriente: " + intIdBanco.ToString() + "}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim bancoAdquiriente As BancoAdquiriente = Nothing
        If strRespuesta <> "" Then
            bancoAdquiriente = serializer.Deserialize(Of BancoAdquiriente)(strRespuesta)
        End If
        Return bancoAdquiriente
    End Function

    Public Shared Async Function EliminarBancoAdquiriente(intIdBanco As Integer) As Task
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "EliminarBancoAdquiriente",
            .DatosPeticion = "{IdBancoAdquiriente: " + intIdBanco.ToString() + "}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Await Utilitario.Ejecutar(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
    End Function

    Public Shared Async Function ObtenerListaClientes(intIdEmpresa As Integer, intNumeroPagina As Integer, intFilasPorPagina As Integer, strNombre As String) As Task(Of List(Of Cliente))
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerListaClientes",
            .DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + ", NumeroPagina: " + intNumeroPagina.ToString() + ",FilasPorPagina: " + intFilasPorPagina.ToString() + ", Nombre: '" + strNombre + "'}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim listado As List(Of Cliente) = New List(Of Cliente)()
        If strRespuesta <> "" Then
            listado = serializer.Deserialize(Of List(Of Cliente))(strRespuesta)
        End If
        Return listado
    End Function

    Public Shared Async Function ObtenerTotalListaClientes(intIdEmpresa As Integer, strNombre As String) As Task(Of Integer)
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerTotalListaClientes",
            .DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + ", Nombre: '" + strNombre + "'}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim intCantidad As Integer = 0
        If strRespuesta <> "" Then
            intCantidad = serializer.Deserialize(Of Integer)(strRespuesta)
        End If
        Return intCantidad
    End Function

    Public Shared Async Function AgregarCliente(cliente As Cliente) As Task(Of String)
        Dim strDatos As String = serializer.Serialize(cliente)
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "AgregarCliente",
            .DatosPeticion = strDatos
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Return strRespuesta
    End Function

    Public Shared Async Function ActualizarCliente(cliente As Cliente) As Task
        Dim strDatos As String = serializer.Serialize(cliente)
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ActualizarCliente",
            .DatosPeticion = strDatos
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Await Utilitario.Ejecutar(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
    End Function

    Public Shared Async Function ObtenerCliente(intIdCliente As Integer) As Task(Of Cliente)
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerCliente",
            .DatosPeticion = "{IdCliente: " + intIdCliente.ToString() + "}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim cliente As Cliente = Nothing
        If strRespuesta <> "" Then
            cliente = serializer.Deserialize(Of Cliente)(strRespuesta)
        End If
        Return cliente
    End Function

    Public Shared Async Function EliminarCliente(intIdCliente As Integer) As Task
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "EliminarCliente",
            .DatosPeticion = "{IdCliente: " + intIdCliente.ToString() + "}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Await Utilitario.Ejecutar(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
    End Function

    Public Shared Async Function ValidaIdentificacionCliente(intIdEmpresa As Integer, strIdentificacion As String) As Task(Of Cliente)
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ValidaIdentificacionCliente",
            .DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + ", Identificacion: '" + strIdentificacion + "'}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim cliente As Cliente = Nothing
        If strRespuesta <> "" Then
            cliente = serializer.Deserialize(Of Cliente)(strRespuesta)
        End If
        Return cliente
    End Function


    Public Shared Async Function ObtenerListaLineas(intIdEmpresa As Integer, Optional strDescripcion As String = "") As Task(Of List(Of Linea))
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerListaLineas",
            .DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + ", Descripcion: '" + strDescripcion + "'}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim listado As List(Of Linea) = New List(Of Linea)()
        If strRespuesta <> "" Then
            listado = serializer.Deserialize(Of List(Of Linea))(strRespuesta)
        End If
        Return listado
    End Function

    Public Shared Async Function ObtenerListaLineasDeProducto(intIdEmpresa As Integer) As Task(Of List(Of Linea))
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerListaLineasDeProducto",
            .DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + "}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim listado As List(Of Linea) = New List(Of Linea)()
        If strRespuesta <> "" Then
            listado = serializer.Deserialize(Of List(Of Linea))(strRespuesta)
        End If
        Return listado
    End Function

    Public Shared Async Function ObtenerListaLineasDeServicio(intIdEmpresa As Integer) As Task(Of List(Of Linea))
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerListaLineasDeServicio",
            .DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + "}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim listado As List(Of Linea) = New List(Of Linea)()
        If strRespuesta <> "" Then
            listado = serializer.Deserialize(Of List(Of Linea))(strRespuesta)
        End If
        Return listado
    End Function

    Public Shared Async Function AgregarLinea(linea As Linea) As Task(Of String)
        Dim strDatos As String = serializer.Serialize(linea)
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "AgregarLinea",
            .DatosPeticion = strDatos
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Return strRespuesta
    End Function

    Public Shared Async Function ActualizarLinea(linea As Linea) As Task
        Dim strDatos As String = serializer.Serialize(linea)
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ActualizarLinea",
            .DatosPeticion = strDatos
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Await Utilitario.Ejecutar(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
    End Function

    Public Shared Async Function ObtenerLinea(intIdLinea As Integer) As Task(Of Linea)
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerLinea",
            .DatosPeticion = "{IdLinea: " + intIdLinea.ToString() + "}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim linea As Linea = Nothing
        If strRespuesta <> "" Then
            linea = serializer.Deserialize(Of Linea)(strRespuesta)
        End If
        Return linea
    End Function

    Public Shared Async Function EliminarLinea(intIdLinea As Integer) As Task
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "EliminarLinea",
            .DatosPeticion = "{IdLinea: " + intIdLinea.ToString() + "}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Await Utilitario.Ejecutar(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
    End Function

    Public Shared Async Function ObtenerListaProveedores(intIdEmpresa As Integer, intNumeroPagina As Integer, intFilasPorPagina As Integer, strNombre As String) As Task(Of List(Of Proveedor))
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerListaProveedores",
            .DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + ", NumeroPagina: " + intNumeroPagina.ToString() + ",FilasPorPagina: " + intFilasPorPagina.ToString() + ", Nombre: '" + strNombre + "'}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim listado As List(Of Proveedor) = New List(Of Proveedor)()
        If strRespuesta <> "" Then
            listado = serializer.Deserialize(Of List(Of Proveedor))(strRespuesta)
        End If
        Return listado
    End Function

    Public Shared Async Function ObtenerTotalListaProveedores(intIdEmpresa As Integer, strNombre As String) As Task(Of Integer)
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerTotalListaProveedores",
            .DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + ", Nombre: '" + strNombre + "'}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim intCantidad As Integer = 0
        If strRespuesta <> "" Then
            intCantidad = serializer.Deserialize(Of Integer)(strRespuesta)
        End If
        Return intCantidad
    End Function

    Public Shared Async Function AgregarProveedor(proveedor As Proveedor) As Task(Of String)
        Dim strDatos As String = serializer.Serialize(proveedor)
        Dim peticion As RequestDTO = New RequestDTO With {
                .NombreMetodo = "AgregarProveedor",
                .DatosPeticion = strDatos
            }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Return strRespuesta
    End Function

    Public Shared Async Function ActualizarProveedor(proveedor As Proveedor) As Task
        Dim strDatos As String = serializer.Serialize(proveedor)
        Dim peticion As RequestDTO = New RequestDTO With {
                .NombreMetodo = "ActualizarProveedor",
                .DatosPeticion = strDatos
            }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Await Utilitario.Ejecutar(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
    End Function

    Public Shared Async Function ObtenerProveedor(intIdProveedor As Integer) As Task(Of Proveedor)
        Dim peticion As RequestDTO = New RequestDTO With {
                .NombreMetodo = "ObtenerProveedor",
                .DatosPeticion = "{IdProveedor: " + intIdProveedor.ToString() + "}"
            }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim proveedor As Proveedor = Nothing
        If strRespuesta <> "" Then
            proveedor = serializer.Deserialize(Of Proveedor)(strRespuesta)
        End If
        Return proveedor
    End Function

    Public Shared Async Function EliminarProveedor(intIdProveedor As Integer) As Task
        Dim peticion As RequestDTO = New RequestDTO With {
                .NombreMetodo = "EliminarProveedor",
                .DatosPeticion = "{IdProveedor: " + intIdProveedor.ToString() + "}"
            }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Await Utilitario.Ejecutar(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
    End Function

    Public Shared Async Function ObtenerTotalListaProductos(intIdEmpresa As Integer, bolIncluyeServicios As Boolean, intIdLinea As Integer, strCodigo As String, strDescripcion As String) As Task(Of Integer)
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerTotalListaProductos",
            .DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + ", IncluyeServicios: '" + bolIncluyeServicios.ToString() + "', IdLinea: " + intIdLinea.ToString() + ", Codigo: '" + strCodigo + "', Descripcion: '" + strDescripcion + "'}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim intCantidad As Integer = 0
        If strRespuesta <> "" Then
            intCantidad = serializer.Deserialize(Of Integer)(strRespuesta)
        End If
        Return intCantidad
    End Function

    Public Shared Async Function ObtenerListaProductos(intIdEmpresa As Integer, intNumeroPagina As Integer, intFilasPorPagina As Integer, bolIncluyeServicios As Boolean, Optional intIdLinea As Integer = 0, Optional strCodigo As String = "", Optional strDescripcion As String = "") As Task(Of List(Of Producto))
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerListaProductos",
            .DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + ", NumeroPagina: " + intNumeroPagina.ToString() + ",FilasPorPagina: " + intFilasPorPagina.ToString() + ", IncluyeServicios: '" + bolIncluyeServicios.ToString() + "', IdLinea: " + intIdLinea.ToString() + ", Codigo: '" + strCodigo + "', Descripcion: '" + strDescripcion + "'}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim listado As List(Of Producto) = New List(Of Producto)()
        If strRespuesta <> "" Then
            listado = serializer.Deserialize(Of List(Of Producto))(strRespuesta)
        End If
        Return listado
    End Function

    Public Shared Async Function AgregarProducto(producto As Producto) As Task(Of String)
        Dim strDatos As String = serializer.Serialize(producto)
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "AgregarProducto",
            .DatosPeticion = strDatos
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Return strRespuesta
    End Function

    Public Shared Async Function ActualizarProducto(bancoAdquiriente As Producto) As Task
        Dim strDatos As String = serializer.Serialize(bancoAdquiriente)
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ActualizarProducto",
            .DatosPeticion = strDatos
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Await Utilitario.Ejecutar(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
    End Function

    Public Shared Async Function ObtenerProducto(intIdProducto As Integer) As Task(Of Producto)
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerProducto",
            .DatosPeticion = "{IdProducto: " + intIdProducto.ToString() + "}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim producto As Producto = Nothing
        If strRespuesta <> "" Then
            producto = serializer.Deserialize(Of Producto)(strRespuesta)
        End If
        Return producto
    End Function

    Public Shared Async Function ObtenerProductoPorCodigo(strCodigo As String) As Task(Of Producto)
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerProductoPorCodigo",
            .DatosPeticion = "{Codigo: '" + strCodigo + "'}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim producto As Producto = Nothing
        If strRespuesta <> "" Then
            producto = serializer.Deserialize(Of Producto)(strRespuesta)
        End If
        Return producto
    End Function

    Public Shared Async Function EliminarProducto(intIdProducto As Integer) As Task
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "EliminarProducto",
            .DatosPeticion = "{IdProducto: " + intIdProducto.ToString() + "}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Await Utilitario.Ejecutar(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
    End Function

    Public Shared Async Function ObtenerListaUsuarios(intIdEmpresa As Integer, strCodigo As String) As Task(Of List(Of Usuario))
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerListaUsuarios",
            .DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + ", Codigo: '" + strCodigo + "'}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim listado As List(Of Usuario) = New List(Of Usuario)()
        If strRespuesta <> "" Then
            listado = serializer.Deserialize(Of List(Of Usuario))(strRespuesta)
        End If
        Return listado
    End Function

    Public Shared Async Function AgregarUsuario(usuario As Usuario) As Task(Of String)
        Dim strDatos As String = serializer.Serialize(usuario)
        Dim peticion As RequestDTO = New RequestDTO With {
                .NombreMetodo = "AgregarUsuario",
                .DatosPeticion = strDatos
            }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Return strRespuesta
    End Function

    Public Shared Async Function ActualizarUsuario(usuario As Usuario) As Task
        Dim strDatos As String = serializer.Serialize(usuario)
        Dim peticion As RequestDTO = New RequestDTO With {
                .NombreMetodo = "ActualizarUsuario",
                .DatosPeticion = strDatos
            }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Await Utilitario.Ejecutar(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
    End Function

    Public Shared Async Function ObtenerUsuario(intIdUsuario As Integer) As Task(Of Usuario)
        Dim peticion As RequestDTO = New RequestDTO With {
                .NombreMetodo = "ObtenerUsuario",
                .DatosPeticion = "{IdUsuario: " + intIdUsuario.ToString() + "}"
            }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim usuario As Usuario = Nothing
        If strRespuesta <> "" Then
            usuario = serializer.Deserialize(Of Usuario)(strRespuesta)
        End If
        Return usuario
    End Function

    Public Shared Async Function EliminarUsuario(intIdUsuario As Integer) As Task
        Dim peticion As RequestDTO = New RequestDTO With {
                .NombreMetodo = "EliminarUsuario",
                .DatosPeticion = "{IdUsuario: " + intIdUsuario.ToString() + "}"
            }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Await Utilitario.Ejecutar(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
    End Function

    Public Shared Async Function ObtenerListaCuentasEgreso(intIdEmpresa As Integer, Optional strDescripcion As String = "") As Task(Of List(Of CuentaEgreso))
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerListaCuentasEgreso",
            .DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + ", Descripcion: '" + strDescripcion + "'}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim listado As List(Of CuentaEgreso) = New List(Of CuentaEgreso)()
        If strRespuesta <> "" Then
            listado = serializer.Deserialize(Of List(Of CuentaEgreso))(strRespuesta)
        End If
        Return listado
    End Function

    Public Shared Async Function AgregarCuentaEgreso(cuentaEgreso As CuentaEgreso) As Task(Of String)
        Dim strDatos As String = serializer.Serialize(cuentaEgreso)
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "AgregarCuentaEgreso",
            .DatosPeticion = strDatos
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Return strRespuesta
    End Function

    Public Shared Async Function ActualizarCuentaEgreso(cuentaEgreso As CuentaEgreso) As Task
        Dim strDatos As String = serializer.Serialize(cuentaEgreso)
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ActualizarCuentaEgreso",
            .DatosPeticion = strDatos
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Await Utilitario.Ejecutar(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
    End Function

    Public Shared Async Function ObtenerCuentaEgreso(intIdCuentaEgreso As Integer) As Task(Of CuentaEgreso)
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerCuentaEgreso",
            .DatosPeticion = "{IdCuentaEgreso: " + intIdCuentaEgreso.ToString() + "}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim cuentaEgreso As CuentaEgreso = Nothing
        If strRespuesta <> "" Then
            cuentaEgreso = serializer.Deserialize(Of CuentaEgreso)(strRespuesta)
        End If
        Return cuentaEgreso
    End Function

    Public Shared Async Function EliminarCuentaEgreso(intIdCuentaEgreso As Integer) As Task
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "EliminarCuentaEgreso",
            .DatosPeticion = "{IdCuentaEgreso: " + intIdCuentaEgreso.ToString() + "}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Await Utilitario.Ejecutar(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
    End Function

    Public Shared Async Function ObtenerListaCuentasBanco(intIdEmpresa As Integer, Optional strDescripcion As String = "") As Task(Of List(Of CuentaBanco))
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerListaCuentasBanco",
            .DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + ", Descripcion: '" + strDescripcion + "'}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim listado As List(Of CuentaBanco) = New List(Of CuentaBanco)()
        If strRespuesta <> "" Then
            listado = serializer.Deserialize(Of List(Of CuentaBanco))(strRespuesta)
        End If
        Return listado
    End Function

    Public Shared Async Function AgregarCuentaBanco(cuentaBanco As CuentaBanco) As Task(Of String)
        Dim strDatos As String = serializer.Serialize(cuentaBanco)
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "AgregarCuentaBanco",
            .DatosPeticion = strDatos
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Return strRespuesta
    End Function

    Public Shared Async Function ActualizarCuentaBanco(cuentaBanco As CuentaBanco) As Task
        Dim strDatos As String = serializer.Serialize(cuentaBanco)
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ActualizarCuentaBanco",
            .DatosPeticion = strDatos
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Await Utilitario.Ejecutar(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
    End Function

    Public Shared Async Function ObtenerCuentaBanco(intIdCuentaBanco As Integer) As Task(Of CuentaBanco)
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerCuentaBanco",
            .DatosPeticion = "{IdCuentaBanco: " + intIdCuentaBanco.ToString() + "}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim cuentaBanco As CuentaBanco = Nothing
        If strRespuesta <> "" Then
            cuentaBanco = serializer.Deserialize(Of CuentaBanco)(strRespuesta)
        End If
        Return cuentaBanco
    End Function

    Public Shared Async Function EliminarCuentaBanco(intIdCuentaBanco As Integer) As Task
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "EliminarCuentaBanco",
            .DatosPeticion = "{IdCuentaBanco: " + intIdCuentaBanco.ToString() + "}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Await Utilitario.Ejecutar(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
    End Function

    Public Shared Async Function ObtenerListaVendedores(intIdEmpresa As Integer, Optional strNombre As String = "") As Task(Of List(Of Vendedor))
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerListaVendedores",
            .DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + ", Nombre: '" + strNombre + "'}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim listado As List(Of Vendedor) = New List(Of Vendedor)()
        If strRespuesta <> "" Then
            listado = serializer.Deserialize(Of List(Of Vendedor))(strRespuesta)
        End If
        Return listado
    End Function

    Public Shared Async Function AgregarVendedor(vendedor As Vendedor) As Task(Of String)
        Dim strDatos As String = serializer.Serialize(vendedor)
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "AgregarVendedor",
            .DatosPeticion = strDatos
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Return strRespuesta
    End Function

    Public Shared Async Function ActualizarVendedor(vendedor As Vendedor) As Task
        Dim strDatos As String = serializer.Serialize(vendedor)
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ActualizarVendedor",
            .DatosPeticion = strDatos
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Await Utilitario.Ejecutar(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
    End Function

    Public Shared Async Function ObtenerVendedor(intIdVendedor As Integer) As Task(Of Vendedor)
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerVendedor",
            .DatosPeticion = "{IdVendedor: " + intIdVendedor.ToString() + "}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim vendedor As Vendedor = Nothing
        If strRespuesta <> "" Then
            vendedor = serializer.Deserialize(Of Vendedor)(strRespuesta)
        End If
        Return vendedor
    End Function

    Public Shared Async Function EliminarVendedor(intIdVendedor As Integer) As Task
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "EliminarVendedor",
            .DatosPeticion = "{IdVendedor: " + intIdVendedor.ToString() + "}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Await Utilitario.Ejecutar(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
    End Function

    Public Shared Async Function ObtenerTotalListaEgresos(intIdEmpresa As Integer, intIdEgreso As Integer, strBeneficiario As String, strDetalle As String) As Task(Of Integer)
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerTotalListaEgresos",
            .DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + ", IdEgreso: " + intIdEgreso.ToString() + ", Beneficiario: '" + strBeneficiario + "', Detalle: '" + strDetalle + "'}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim intCantidad As Integer = 0
        If strRespuesta <> "" Then
            intCantidad = serializer.Deserialize(Of Integer)(strRespuesta)
        End If
        Return intCantidad
    End Function

    Public Shared Async Function ObtenerListaEgresos(intIdEmpresa As Integer, intNumeroPagina As Integer, intFilasPorPagina As Integer, intIdEgreso As Integer, strBeneficiario As String, strDetalle As String) As Task(Of List(Of Egreso))
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerListaEgresos",
            .DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + ", NumeroPagina: " + intNumeroPagina.ToString() + ",FilasPorPagina: " + intFilasPorPagina.ToString() + ", IdEgreso: " + intIdEgreso.ToString() + ", Beneficiario: '" + strBeneficiario + "', Detalle: '" + strDetalle + "'}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim listado As List(Of Egreso) = New List(Of Egreso)()
        If strRespuesta <> "" Then
            listado = serializer.Deserialize(Of List(Of Egreso))(strRespuesta)
        End If
        Return listado
    End Function

    Public Shared Async Function AnularEgreso(intIdEgreso As Integer, intIdUsuario As Integer) As Task
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "AnularEgreso",
            .DatosPeticion = "{IdEgreso: " + intIdEgreso.ToString() + ", IdUsuario: " + intIdUsuario.ToString() + "}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Await Utilitario.Ejecutar(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
    End Function

    Public Shared Async Function ObtenerEgreso(intIdEgreso As Integer) As Task(Of Egreso)
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerEgreso",
            .DatosPeticion = "{IdEgreso: " + intIdEgreso.ToString() + "}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim egreso As Egreso = Nothing
        If strRespuesta <> "" Then
            egreso = serializer.Deserialize(Of Egreso)(strRespuesta)
        End If
        Return egreso
    End Function

    Public Shared Async Function AgregarEgreso(egreso As Egreso) As Task(Of String)
        Dim strDatos As String = serializer.Serialize(egreso)
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "AgregarEgreso",
            .DatosPeticion = strDatos
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Return strRespuesta
    End Function

    Public Shared Async Function ActualizarEgreso(egreso As Egreso) As Task
        Dim strDatos As String = serializer.Serialize(egreso)
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ActualizarEgreso",
            .DatosPeticion = strDatos
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Await Utilitario.Ejecutar(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
    End Function

    Public Shared Async Function ObtenerTotalListaFacturas(intIdEmpresa As Integer, intIdFactura As Integer, strNombre As String) As Task(Of Integer)
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerTotalListaFacturas",
            .DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + ", IdFactura: " + intIdFactura.ToString() + ", Nombre: '" + strNombre + "'}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim intCantidad As Integer = 0
        If strRespuesta <> "" Then
            intCantidad = serializer.Deserialize(Of Integer)(strRespuesta)
        End If
        Return intCantidad
    End Function

    Public Shared Async Function ObtenerListaFacturas(intIdEmpresa As Integer, intNumeroPagina As Integer, intFilasPorPagina As Integer, intIdFactura As Integer, strNombre As String) As Task(Of List(Of Factura))
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerListaFacturas",
            .DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + ", NumeroPagina: " + intNumeroPagina.ToString() + ",FilasPorPagina: " + intFilasPorPagina.ToString() + ", IdFactura: " + intIdFactura.ToString() + ", Nombre: '" + strNombre + "'}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim listado As List(Of Factura) = New List(Of Factura)()
        If strRespuesta <> "" Then
            listado = serializer.Deserialize(Of List(Of Factura))(strRespuesta)
        End If
        Return listado
    End Function

    Public Shared Async Function AnularFactura(intIdFactura As Integer, intIdUsuario As Integer) As Task
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "AnularFactura",
            .DatosPeticion = "{IdFactura: " + intIdFactura.ToString() + ", IdUsuario: " + intIdUsuario.ToString() + "}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Await Utilitario.Ejecutar(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
    End Function

    Public Shared Async Function ObtenerFactura(intIdFactura As Integer) As Task(Of Factura)
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerFactura",
            .DatosPeticion = "{IdFactura: " + intIdFactura.ToString() + "}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim factura As Factura = Nothing
        If strRespuesta <> "" Then
            factura = serializer.Deserialize(Of Factura)(strRespuesta)
        End If
        Return factura
    End Function

    Public Shared Async Function AgregarFactura(factura As Factura) As Task(Of String)
        Dim strDatos As String = serializer.Serialize(factura)
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "AgregarFactura",
            .DatosPeticion = strDatos
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Return strRespuesta
    End Function

    Public Shared Async Function ActualizarFactura(factura As Factura) As Task
        Dim strDatos As String = serializer.Serialize(factura)
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ActualizarFactura",
            .DatosPeticion = strDatos
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Await Utilitario.Ejecutar(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
    End Function

    Public Shared Async Function ObtenerDocumentoElectronico(intIdDocumento As Integer) As Task(Of DocumentoElectronico)
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerDocumentoElectronico",
            .DatosPeticion = "{IdDocumento: " + intIdDocumento.ToString() + "}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim documento As DocumentoElectronico = Nothing
        If strRespuesta <> "" Then
            documento = serializer.Deserialize(Of DocumentoElectronico)(strRespuesta)
        End If
        Return documento
    End Function

    Public Shared Async Function GeneraMensajeReceptor(strDatos As String, intIdEmpresa As Integer, intSucursal As Integer, intTerminal As Integer, intEstado As Integer) As Task
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "GeneraMensajeReceptor",
            .DatosPeticion = "{Datos: '" + strDatos + "', IdEmpresa: " + intIdEmpresa.ToString() + ", Sucursal: " + intSucursal.ToString() + ", Terminal: " + intTerminal.ToString() + ", Estado: " + intEstado.ToString() + "}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Await Utilitario.Ejecutar(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
    End Function

    Public Shared Async Function ObtenerTotalDocumentosElectronicosProcesados(intIdEmpresa As Integer) As Task(Of Integer)
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerTotalDocumentosElectronicosProcesados",
            .DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + "}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim intCantidad As Integer = 0
        If strRespuesta <> "" Then
            intCantidad = serializer.Deserialize(Of Integer)(strRespuesta)
        End If
        Return intCantidad
    End Function

    Public Shared Async Function ObtenerListaDocumentosElectronicosProcesados(intIdEmpresa As Integer, intNumeroPagina As Integer, intFilasPorPagina As Integer) As Task(Of List(Of DocumentoElectronico))
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerListaDocumentosElectronicosProcesados",
            .DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + ", NumeroPagina: " + intNumeroPagina.ToString() + ",FilasPorPagina: " + intFilasPorPagina.ToString() + "}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim listado As List(Of DocumentoElectronico) = New List(Of DocumentoElectronico)()
        If strRespuesta <> "" Then
            listado = serializer.Deserialize(Of List(Of DocumentoElectronico))(strRespuesta)
        End If
        Return listado
    End Function

    Public Shared Async Function ObtenerListaDocumentosElectronicosEnProceso(intIdEmpresa As Integer) As Task(Of List(Of DocumentoElectronico))
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ObtenerListaDocumentosElectronicosEnProceso",
            .DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + "}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
        strRespuesta = serializer.Deserialize(Of String)(strRespuesta)
        Dim listado As List(Of DocumentoElectronico) = New List(Of DocumentoElectronico)()
        If strRespuesta <> "" Then
            listado = serializer.Deserialize(Of List(Of DocumentoElectronico))(strRespuesta)
        End If
        Return listado
    End Function

    Public Shared Async Function ProcesarDocumentosElectronicosPendientes(intIdEmpresa As Integer) As Task
        Dim peticion As RequestDTO = New RequestDTO With {
            .NombreMetodo = "ProcesarDocumentosElectronicosPendientes",
            .DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + "}"
        }
        Dim strPeticion As String = serializer.Serialize(peticion)
        Await Utilitario.Ejecutar(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
    End Function
#End Region
End Class
