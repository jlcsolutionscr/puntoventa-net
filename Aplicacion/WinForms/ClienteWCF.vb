Imports System.Collections.Generic
Imports System.Threading.Tasks
Imports System.Web.Script.Serialization
Imports LeandroSoftware.AccesoDatos.Dominio.Entidades
Imports LeandroSoftware.AccesoDatos.TiposDatos
Imports LeandroSoftware.Core
Imports LeandroSoftware.Core.CommonTypes

Public Class ClienteWCF
    Private Shared serializer = New JavaScriptSerializer()
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

    Public Shared Async Function ObtenerListaVendedores(intIdEmpresa As Integer, Optional ByVal strNombre As String = "") As Task(Of List(Of Vendedor))
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

    Public Shared Async Function ObtenerListaBancoAdquiriente(intIdEmpresa As Integer, strDescripcion As String) As Task(Of List(Of BancoAdquiriente))
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
        Dim bancoAdquiriente As BancoAdquiriente = New BancoAdquiriente()
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
        Dim intCantidad As Integer = Nothing
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
        Dim cliente As Cliente = New Cliente()
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


    Public Shared Async Function ObtenerListaLineas(intIdEmpresa As Integer, strDescripcion As String) As Task(Of List(Of Linea))
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
        Dim linea As Linea = New Linea()
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
        Dim intCantidad As Integer = Nothing
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
        Dim proveedor As Proveedor = New Proveedor()
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
        Dim usuario As Usuario = New Usuario()
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
#End Region
End Class
