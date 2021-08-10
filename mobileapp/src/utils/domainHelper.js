import { get, getWithResponse, post, postWithResponse } from 'utils/requestHelper'
import CryptoJS from 'crypto-js'
import { roundNumber } from 'utils/formatHelper'

export async function getCompanyIdentifiers(serviceURL, deviceId) {
  try {
    const endpoint = serviceURL + "/obtenerlistadoempresas?dispositivo=" + deviceId
    console.log('endpoint', endpoint)
    const response = await getWithResponse(endpoint)
    if (response === null) return []
    return response
  } catch (e) {
    throw e.message
  }
}

export async function getTerminalsAvailablePerId(serviceURL, user, password, id) {
  try {
    const ecryptedPass = encryptString(password)
    const endpoint = serviceURL + "/obtenerlistadoterminalesdisponibles?usuario=" + user + "&clave=" + ecryptedPass + "&id=" + id + "&tipodispositivo=2"
    const response = await getWithResponse(endpoint)
    if (response === null) return []
    return response
  } catch (e) {
    throw e.message
  }
}

export async function registerDevice(serviceURL, user, password, id, branchId, terminalId, deviceId) {
  try {
    const ecryptedPass = encryptString(password)
    const endpoint = serviceURL + "/registrarterminal?usuario=" + user + "&clave=" + ecryptedPass + "&id=" + id + "&sucursal=" + branchId + "&terminal=" + terminalId + "&tipodispositivo=2&dispositivo=" + deviceId
    await get(endpoint)
  } catch (e) {
    throw e.message
  }
}

export async function validateCredentials(serviceURL, user, password, companyId, deviceId) {
  try {
    const ecryptedPass = encryptString(password)
    const endpoint = serviceURL + "/validarcredenciales?usuario=" + user + "&clave=" + ecryptedPass + "&idempresa=" + companyId + "&dispositivo=" + deviceId
    company = await getWithResponse(endpoint)
    return company
  } catch (e) {
    throw e.message
  }
}

export async function getConfiguration(serviceURL, token, companyId, branchId, terminalId) {
  try {
    const data = "{NombreMetodo: 'ObtenerTerminalPorSucursal', Parametros: {IdEmpresa: " + companyId + ", IdSucursal: " + branchId + ", IdTerminal: " + terminalId + "}}"
    const response = await postWithResponse(serviceURL + "/ejecutarconsulta", token, data)
    if (response === null) return []
    return response
  } catch (e) {
    throw e.message
  }
}

export async function saveConfiguration(serviceURL, token, terminal) {
  try {
    const entidad = JSON.stringify(terminal)
    const data = "{NombreMetodo: 'ActualizarTerminalPorSucursal', Entidad: " + entidad + "}"
    await post(serviceURL + "/ejecutar", token, data)
  } catch (e) {
    throw e.message
  }
}

export async function getIdTypeList(serviceURL, token) {
  try {
    const data = "{NombreMetodo: 'ObtenerListadoTipoIdentificacion'}"
    const response = await postWithResponse(serviceURL + "/ejecutarconsulta", token, data)
    if (response === null) return []
    return response
  } catch (e) {
    throw e.message
  }
}

export async function getRentTypeList(serviceURL, token) {
  try {
    const data = "{NombreMetodo: 'ObtenerListadoTipoImpuesto'}"
    const response = await postWithResponse(serviceURL + "/ejecutarconsulta", token, data)
    if (response === null) return []
    return response
  } catch (e) {
    throw e.message
  }
}

export async function getPriceTypeList(serviceURL, token) {
  try {
    const data = "{NombreMetodo: 'ObtenerListadoTipodePrecio'}"
    const response = await postWithResponse(serviceURL + "/ejecutarconsulta", token, data)
    if (response === null) return []
    return response
  } catch (e) {
    throw e.message
  }
}

export async function getCustomerList(serviceURL, token, companyId) {
  try {
    const data = "{NombreMetodo: 'ObtenerListadoClientes', Parametros: {IdEmpresa: " + companyId + ", NumeroPagina: 1, FilasPorPagina: 0}}"
    const response = await postWithResponse(serviceURL + "/ejecutarconsulta", token, data)
    if (response === null) return []
    return response
  } catch (e) {
    throw e.message
  }
}

export async function getCustomerEntity(serviceURL, token, idCustomer) {
  try {
    const data = "{NombreMetodo: 'ObtenerCliente', Parametros: {IdCliente: " + idCustomer + "}}"
    const response = await postWithResponse(serviceURL + "/ejecutarconsulta", token, data)
    if (response === null) return null
    return response
  } catch (e) {
    throw e.message
  }
}

export async function saveCustomerEntity(serviceURL, token, customer) {
  try {
    const entidad = JSON.stringify(customer)
    let data = ""
    if (customer.IdCliente) {
      console.log('Actualizando cliente', entidad)

      data = "{NombreMetodo: 'ActualizarCliente', Entidad: " + entidad + "}"
    } else {
      data = "{NombreMetodo: 'AgregarCliente', Entidad: " + entidad + "}"
    }
    const response = await post(serviceURL + "/ejecutar", token, data)
    if (response === null) return null
    return response
  } catch (e) {
    throw e.message
  }
}

export async function getProductTypeList(serviceURL, token) {
  try {
    const data = "{NombreMetodo: 'ObtenerListadoTipoProducto'}"
    const response = await postWithResponse(serviceURL + "/ejecutarconsulta", token, data)
    if (response === null) return []
    return response
  } catch (e) {
    throw e.message
  }
}

export async function getProductCategoryList(serviceURL, token, idCompany) {
  try {
    const data = "{NombreMetodo: 'ObtenerListadoLineas', Parametros: {IdEmpresa: " + idCompany + "}}"
    const response = await postWithResponse(serviceURL + "/ejecutarconsulta", token, data)
    if (response === null) return []
    return response
  } catch (e) {
    throw e.message
  }
}

export async function getProductProviderList(serviceURL, token, idCompany) {
  try {
    const data = "{NombreMetodo: 'ObtenerListadoProveedores', Parametros: {IdEmpresa: " + idCompany + ", NumeroPagina: 1, FilasPorPagina: 0}}"
    const response = await postWithResponse(serviceURL + "/ejecutarconsulta", token, data)
    if (response === null) return []
    return response
  } catch (e) {
    throw e.message
  }
}

export async function getProductList(serviceURL, token, idCompany, idBranch, filterText) {
  try {
    const data = "{NombreMetodo: 'ObtenerListadoProductos', Parametros: {IdEmpresa: " + idCompany + ", IdSucursal: " + idBranch + ", NumeroPagina: 1, FilasPorPagina: 50, IncluyeServicios: 'true', FiltraActivos: 'true', IdLinea: 0, Codigo: '', Descripcion: '" + filterText + "'}}"
    const response = await postWithResponse(serviceURL + "/ejecutarconsulta", token, data)
    if (response === null) return []
    return response
  } catch (e) {
    throw e.message
  }
}

export async function getProductEntity(serviceURL, token, idProduct, idBranch) {
  try {
    const data = "{NombreMetodo: 'ObtenerProducto', Parametros: {IdProducto: " + idProduct + ", IdSucursal: " + idBranch + "}}"
    const response = await postWithResponse(serviceURL + "/ejecutarconsulta", token, data)
    if (response === null) return null
    return response
  } catch (e) {
    throw e.message
  }
}

export async function saveProductEntity(serviceURL, token, product) {
  try {
    const entidad = JSON.stringify(product)
    let data = ""
    if (product.IdProducto)
      data = "{NombreMetodo: 'ActualizarProducto', Entidad: " + entidad + "}"
    else
      data = "{NombreMetodo: 'AgregarProducto', Entidad: " + entidad + "}"
    const response = await post(serviceURL + "/ejecutar", token, data)
    if (response === null) return null
    return response
  } catch (e) {
    throw e.message
  }
}

export async function getExonerationTypeList(serviceURL, token) {
  try {
    const data = "{NombreMetodo: 'ObtenerListadoTipoExoneracion'}"
    const response = await postWithResponse(serviceURL + "/ejecutarconsulta", token, data)
    if (response === null) return []
    return response
  } catch (e) {
    throw e.message
  }
}

export async function getPaymentBankId(serviceURL, token, idCompany, paymentMethod) {
  try {
    let data
    if (paymentMethod === 1 || paymentMethod === 2) {
      data = "{NombreMetodo: 'ObtenerListadoBancoAdquiriente', Parametros: {IdEmpresa: " + idCompany + "}}"
    } else {
      data = "{NombreMetodo: 'ObtenerListadoCuentasBanco', Parametros: {IdEmpresa: " + idCompany + "}}"
    }
    const response = await postWithResponse(serviceURL + "/ejecutarconsulta", token, data)
    if (response === null) return null
    if (response.length == 0) return null
    return response[0].Id
  } catch (e) {
    throw e.message
  }
}

export function getCustomerPrice(customer, product) {
  let customerPrice = 0
  switch (customer.IdTipoPrecio) {
    case 1:
      customerPrice = product.PrecioVenta1
      break
    case 2:
      customerPrice = product.PrecioVenta2
      break
    case 3:
      customerPrice = product.PrecioVenta3
      break
    case 4:
      customerPrice = product.PrecioVenta4
      break
    case 5:
      customerPrice = product.PrecioVenta5
      break
    default:
      customerPrice = product.PrecioVenta1
  }
  if (customer.AplicaTasaDiferenciada) {
    customerPrice = roundNumber(customerPrice / (1 + (product.ParametroImpuesto.TasaImpuesto / 100)), 3)
    customerPrice = roundNumber(customerPrice * (1 + (customer.ParametroImpuesto.TasaImpuesto / 100)), 2)
  }
  return customerPrice
}

export function getInvoiceSummary(products, exonerationPercentage) {
  try {
    let gravado = 0
    let exonerado = 0
    let excento = 0
    let subTotal = 0
    let impuesto = 0
    let total = 0
    let totalCosto = 0
    products.forEach(item => {
      const precioUnitario = roundNumber(item.PrecioVenta / (1 + (item.PorcentajeIVA / 100)), 3)
      if (item.PorcentajeIVA > 0) {
        let impuestoUnitario = precioUnitario * item.PorcentajeIVA / 100
        if (exonerationPercentage > 0) {
          const gravadoParcial = precioUnitario * (1 - (exonerationPercentage / 100))
          gravado += roundNumber(gravadoParcial, 2) * item.Cantidad,
          exonerado += roundNumber((precioUnitario - gravadoParcial), 2) * item.Cantidad
          impuestoUnitario = gravadoParcial * item.PorcentajeIVA / 100
        } else {
          gravado += roundNumber(precioUnitario, 2) * item.Cantidad
        }
        impuesto += roundNumber(impuestoUnitario, 2) * item.Cantidad
      } else {
        excento += roundNumber(precioUnitario, 2) * item.Cantidad
      }
    })
    subTotal = gravado + exonerado + excento
    total = subTotal + impuesto
    return {
      gravado: roundNumber(gravado, 2),
      exonerado: roundNumber(exonerado, 2),
      excento: roundNumber(excento, 2),
      subTotal: roundNumber(subTotal, 2),
      impuesto: roundNumber(impuesto, 2),
      total: roundNumber(total, 2),
      totalCosto: roundNumber(totalCosto, 2)
    }
  } catch (e) {
    throw e.message
  }
}

export async function saveInvoiceEntity(serviceURL, token, products, paymentMethodId, company, idCustomer, customerName, excento, gravado, exonerado, impuesto, totalCosto, total, exonerationType, exonerationCode, exonerationEntity, exonerationDate, exonerationPercentage) {
  try {
    const bankId = await getPaymentBankId(serviceURL, token, company.IdEmpresa, paymentMethodId)
    const detalleFactura = []
    products.forEach(item => {
      const detalle = {
        IdFactura: 0,
        IdProducto: item.IdProducto,
        Descripcion: item.Descripcion,
        Cantidad: item.Cantidad,
        PrecioVenta: roundNumber(item.PrecioVenta / (1 + (item.PorcentajeIVA / 100)), 3),
        Excento: item.Excento,
        PrecioCosto: item.PrecioCosto,
        CostoInstalacion: 0,
        PorcentajeIVA: item.PorcentajeIVA
      }
      detalleFactura.push(detalle)
    })
    const desglosePagoFactura = [{
      IdConsecutivo: 0,
      IdFactura: 0,
      IdFormaPago: paymentMethodId,
      IdTipoMoneda: company.IdTipoMoneda,
      IdCuentaBanco: bankId,
      TipoTarjeta: '',
      NroMovimiento: '',
      MontoLocal: total,
      MontoForaneo: total
    }]
    const fechaFactura = new Date()
    const dd = (fechaFactura.getDate() < 10 ? '0' : '') + fechaFactura.getDate()
    const MM = ((fechaFactura.getMonth() + 1) < 10 ? '0' : '') + (fechaFactura.getMonth() + 1)
    const HH = (fechaFactura.getHours() < 10 ? '0' : '') + fechaFactura.getHours()
    const mm = (fechaFactura.getMinutes() < 10 ? '0' : '') + fechaFactura.getMinutes()
    const ss = (fechaFactura.getSeconds() < 10 ? '0' : '') + fechaFactura.getSeconds()
    const timeString = dd + '/' + MM + '/' + fechaFactura.getFullYear() + ' ' + HH + ':' + mm + ':' + ss + ' GMT-07:00'
    const factura = {
      IdEmpresa: company.IdEmpresa,
      IdSucursal: company.EquipoRegistrado.IdSucursal,
      IdTerminal: company.EquipoRegistrado.IdTerminal,
      IdFactura: 0,
      IdUsuario: company.Usuario.IdUsuario,
      IdTipoMoneda: 1,
      TipoDeCambioDolar: 0,
      IdCliente: idCustomer,
      NombreCliente: customerName,
      IdCondicionVenta: 1,
      PlazoCredito: 0,
      Fecha: {DateTime: timeString},
      TextoAdicional: '',
      IdVendedor: 1,
      Excento: excento,
      Gravado: gravado,
      Exonerado: exonerado,
      Descuento: 0,
      Impuesto: impuesto,
      TotalCosto: totalCosto,
      MontoPagado: total,
      IdTipoExoneracion: exonerationType,
      NumDocExoneracion: exonerationCode,
      NombreInstExoneracion: exonerationEntity,
      FechaEmisionDoc: {DateTime: exonerationDate + ' 22:59:59 GMT-07:00'},
      PorcentajeExoneracion: exonerationPercentage,
      IdCxC: 0,
      IdAsiento: 0,
      IdMovBanco: 0,
      IdOrdenServicio: 0,
      IdProforma: 0,
      DetalleFactura: detalleFactura,
      DesglosePagoFactura: desglosePagoFactura
    }
    let data = "{NombreMetodo: 'AgregarFactura', Entidad: " + JSON.stringify(factura) + "}"
    await post(serviceURL + "/ejecutarconsulta", token, data)
  } catch (e) {
    throw e.message
  }
}


export async function revokeInvoiceEntity(serviceURL, token, idInvoice, idUser) {
  try {
    const data = "{NombreMetodo: 'AnularFactura', Parametros: {IdFactura: " + idInvoice + ", IdUsuario: " + idUser + "}}"
    await post(serviceURL + "/ejecutar", token, data)
  } catch (e) {
    throw e.message
  }
}

export async function getInvoiceDetails(serviceURL, token, idInvoice) {
  try {
    const data = "{NombreMetodo: 'ObtenerFactura', Parametros: {IdFactura: " + idInvoice + "}}"
    const response = await postWithResponse(serviceURL + "/ejecutarconsulta", token, data)
    if (response === null) return null
    return response
  } catch (e) {
    throw e.message
  }
}

export async function getProcessedInvoiceListCount(serviceURL, token, idCompany, idBranch) {
  try {
    const data = "{NombreMetodo: 'ObtenerTotalListaFacturas', Parametros: {IdEmpresa: " + idCompany + ", IdSucursal: " + idBranch + "}}"
    const response = await postWithResponse(serviceURL + "/ejecutarconsulta", token, data)
    if (response === null) return null
    return response
  } catch (e) {
    throw e.message
  }
}

export async function getProcessedInvoiceListPerPage(serviceURL, token, idCompany, idBranch, pageNumber, rowPerPage) {
  try {
    const data = "{NombreMetodo: 'ObtenerListadoFacturas', Parametros: {IdEmpresa: " + idCompany + ", IdSucursal: " + idBranch + ", NumeroPagina: " + pageNumber + ",FilasPorPagina: " + rowPerPage + "}}"
    const response = await postWithResponse(serviceURL + "/ejecutarconsulta", token, data)
    if (response === null) return []
    return response
  } catch (e) {
    throw e.message
  }
}

export async function getProcessedDocumentListCount(serviceURL, token, idCompany, idBranch) {
  try {
    const data = "{NombreMetodo: 'ObtenerTotalDocumentosElectronicosProcesados', Parametros: {IdEmpresa: " + idCompany + ", IdSucursal: " + idBranch + "}}"
    const response = await postWithResponse(serviceURL + "/ejecutarconsulta", token, data)
    if (response === null) return null
    return response
  } catch (e) {
    throw e.message
  }
}

export async function getProcessedDocumentListPerPage(serviceURL, token, idCompany, idBranch, pageNumber, rowPerPage) {
  try {
    const data = "{NombreMetodo: 'ObtenerListadoDocumentosElectronicosProcesados', Parametros: {IdEmpresa: " + idCompany + ", IdSucursal: " + idBranch + ", NumeroPagina: " + pageNumber + ",FilasPorPagina: " + rowPerPage + "}}"
    const response = await postWithResponse(serviceURL + "/ejecutarconsulta", token, data)
    if (response === null) return []
    return response
  } catch (e) {
    throw e.message
  }
}

export async function getDocumentEntity(serviceURL, token, idDocument) {
  try {
    const data = "{NombreMetodo: 'ObtenerDocumentoElectronico', Parametros: {IdDocumento: " + idDocument + "}}"
    const response = await postWithResponse(serviceURL + "/ejecutarconsulta", token, data)
    if (response === null) return null
    return response
  } catch (e) {
    throw e.message
  }
}

export async function sendDocumentByEmail(serviceURL, token, idDocument, emailTo) {
  try {
    data = "{NombreMetodo: 'EnviarNotificacionDocumentoElectronico', Parametros: {IdDocumento: " + idDocument + ", CorreoReceptor: '" + emailTo + "'}}"
    await post(serviceURL + "/ejecutar", token, data)
  } catch (e) {
    throw e.message
  }
}

export async function sendReportEmail(serviceURL, token, idCompany, idBranch, reportName, startDate, endDate) {
  try {
    if (reportName != '') {
      data = "{NombreMetodo: 'EnviarReportePorCorreoElectronico', Parametros: {IdEmpresa: " + idCompany + ", IdSucursal: " + idBranch + ", NombreReporte: '" + reportName + "', FechaInicial: '" + startDate + "', FechaFinal: '" + endDate + "', FormatoReporte: 'PDF'}}"
      await post(serviceURL + "/ejecutar", token, data)
    }
  } catch (e) {
    throw e.message
  }
}

function encryptString(plainText) {
  const phrase = 'Po78]Rba[%J=[14[*'
  const data = CryptoJS.enc.Utf8.parse(plainText)
  const passHash = CryptoJS.enc.Utf8.parse(phrase)
  const iv = CryptoJS.enc.Utf8.parse('@1B2c3D4e5F6g7H8')
  const saltKey = CryptoJS.enc.Utf8.parse('S@LT&KEY')
  const key128Bits1000Iterations = CryptoJS.PBKDF2(passHash, saltKey, { keySize: 256 / 32, iterations: 1000 })
  const encrypted = CryptoJS.AES.encrypt(data, key128Bits1000Iterations, { mode: CryptoJS.mode.CBC, iv: iv, padding: CryptoJS.pad.ZeroPadding })
  const encryptedText = encrypted.ciphertext.toString(CryptoJS.enc.Base64)
  return encryptedText
}

