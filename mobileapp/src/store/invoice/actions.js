import {
  SET_ERROR,
  SET_PAYMENT_METHOD_ID,
  SET_EXONERATION_TYPE,
  SET_EXONERATION_DESC,
  SET_EXONERATION_CODE,
  SET_EXONERATION_ENTITY,
  SET_EXONERATION_DATE,
  SET_EXONERATION_PERCENTAGE,
  SET_CUSTOMER,
  SET_CUSTOMER_NAME,
  SET_PRODUCT,
  RESET_PRODUCT,
  SET_PRODUCT_DESCRIPTION,
  SET_PRODUCT_QUANTITY,
  SET_PRODUCT_PRICE,
  SET_PRODUCTS,
  SET_SUMMARY,
  RESET_INVOICE,
  SUCCESSFUL_STATUS,
  SET_LIST,
  SET_LIST_COUNT,
  SET_LIST_PAGE
} from './types'

import { setCustomerList } from '../customer/actions'
import { setProductList } from '../product/actions'
import { startLoader, stopLoader } from '../ui/actions'

import {
  getCustomerList,
  getProductList,
  getCustomerEntity,
  getProductEntity,
  getCustomerPrice,
  getInvoiceSummary,
  saveInvoiceEntity,
  revokeInvoiceEntity,
  getProcessedInvoiceListCount,
  getProcessedInvoiceListPerPage
} from 'utils/domainHelper'

export const setInvoiceError = (error) => {
  return {
    type: SET_ERROR,
    payload: { error }
  }
}

export const setPaymentMethodId = (id) => {
  return {
    type: SET_PAYMENT_METHOD_ID,
    payload: { id }
  }
}

export const setExonerationType = (id) => {
  return {
    type: SET_EXONERATION_TYPE,
    payload: { id }
  }
}

export const setExonerationDesc = (desc) => {
  return {
    type: SET_EXONERATION_DESC,
    payload: { desc }
  }
}

export const setExonerationCode = (code) => {
  return {
    type: SET_EXONERATION_CODE,
    payload: { code }
  }
}

export const setExonerationEntity = (name) => {
  return {
    type: SET_EXONERATION_ENTITY,
    payload: { name }
  }
}

export const setExonerationDate = (date) => {
  return {
    type: SET_EXONERATION_DATE,
    payload: { date }
  }
}

export const setExonerationPercentage = (percentage) => {
  return {
    type: SET_EXONERATION_PERCENTAGE,
    payload: { percentage }
  }
}

export const setCustomer = (entity) => {
  return {
    type: SET_CUSTOMER,
    payload: { entity }
  }
}

export const setCustomerName = (name) => {
  return {
    type: SET_CUSTOMER_NAME,
    payload: { name }
  }
}

export const setProduct = (entity) => {
  return {
    type: SET_PRODUCT,
    payload: { entity }
  }
}

export const resetProduct = () => {
  return {
    type: RESET_PRODUCT
  }
}

export const setProductDescription = (description) => {
  return {
    type: SET_PRODUCT_DESCRIPTION,
    payload: { description }
  }
}

export const setProductQuantity = (quantity) => {
  return {
    type: SET_PRODUCT_QUANTITY,
    payload: { quantity }
  }
}

export const setProductPrice = (price) => {
  return {
    type: SET_PRODUCT_PRICE,
    payload: { price }
  }
}

export const setProducts = (newList) => {
  return {
    type: SET_PRODUCTS,
    payload: { newList }
  }
}

export const setSummary = (summary) => {
  return {
    type: SET_SUMMARY,
    payload: { summary }
  }
}

export const resetInvoice = () => {
  return {
    type: RESET_INVOICE
  }
}

export const setSuccessful = () => {
  return {
    type: SUCCESSFUL_STATUS
  }
}

export const setList = (newList) => {
  return {
    type: SET_LIST,
    payload: { newList }
  }
}

export const setListCount = (count) => {
  return {
    type: SET_LIST_COUNT,
    payload: { count }
  }
}

export const setListPage = (page) => {
  return {
    type: SET_LIST_PAGE,
    payload: { page }
  }
}

export function setParameters () {
  return async (dispatch, getState) => {
    const { serviceURL } = getState().config
    const { token, company } = getState().session
    const { customerList } = getState().customer
    const { productList } = getState().product
    dispatch(startLoader())
    dispatch(setInvoiceError(''))
    dispatch(setPaymentMethodId(1))
    try {
      const customer = await getCustomerEntity(serviceURL, token, 1)
      if (customer != null) {
        dispatch(setCustomer(customer))
      }
      if (customerList.length == 0) {
        const newList = await getCustomerList(serviceURL, token, company.IdEmpresa)
        dispatch(setCustomerList(newList))
      }
      if (productList.length == 0) {
        let newList = await getProductList(serviceURL, token, company.IdEmpresa, company.EquipoRegistrado.IdSucursal, '')
      dispatch(setProductList(newList))
      }
      dispatch(stopLoader())
    } catch (error) {
      dispatch(stopLoader())
      dispatch(setInvoiceError(error))
    }
  }
}

export function filterProductList (text) {
  return async (dispatch, getState) => {
    const { serviceURL } = getState().config
    const { token } = getState().session
    try {
      let newList = await getProductList(serviceURL, token, company.IdEmpresa, company.EquipoRegistrado.IdSucursal, text)
      dispatch(setProductList(newList))
    } catch (error) {
      dispatch(stopLoader())
      dispatch(setInvoiceError(error))
    }
  }
}

export function getCustomer (idCustomer) {
  return async (dispatch, getState) => {
    const { serviceURL } = getState().config
    const { token } = getState().session
    const { products } = getState().invoice
    dispatch(startLoader())
    dispatch(setInvoiceError(''))
    try {
      const customer = await getCustomerEntity(serviceURL, token, idCustomer)
      if (customer != null) {
        dispatch(setCustomer(customer))
        if (customer.PorcentajeExoneracion > 0) {
          dispatch(setExonerationType(customer.IdTipoExoneracion))
          dispatch(setExonerationDesc(customer.ParametroExoneracion.Descripcion))
          dispatch(setExonerationCode(customer.NumDocExoneracion))
          dispatch(setExonerationEntity(customer.NombreInstExoneracion))
          dispatch(setExonerationDate(customer.FechaEmisionDoc.DateTime.substr(0,10)))
        } else {
          dispatch(setExonerationType(1))
          dispatch(setExonerationDesc(''))
          dispatch(setExonerationCode(''))
          dispatch(setExonerationEntity(''))
          dispatch(setExonerationDate('01/01/2019'))
        }
        dispatch(setExonerationPercentage(customer.PorcentajeExoneracion))
        const summary = getInvoiceSummary(products, customer.PorcentajeExoneracion)
        dispatch(setSummary(summary))
      }
      dispatch(stopLoader())
    } catch (error) {
      dispatch(stopLoader())
      dispatch(setInvoiceError(error))
    }
  }
}

export function getProduct (idProduct) {
  return async (dispatch, getState) => {
    const { serviceURL } = getState().config
    const { token } = getState().session
    const { customer } = getState().invoice
    try {
      dispatch(setInvoiceError(''))
      const product = await getProductEntity(serviceURL, token, idProduct, company.EquipoRegistrado.IdSucursal)
      if (product != null) {
        let precio = product.PrecioVenta1
        if (customer != null) precio = getCustomerPrice(customer, product)
        product.PrecioVenta = precio
        dispatch(setProduct(product))
        dispatch(filterProductList(''))
      }
    } catch (error) {
      dispatch(setInvoiceError(error))
    }
  }
}

export function insertProduct () {
  return async (dispatch, getState) => {
    const { customer, product, products, productDescription, productQuantity, productPrice, exonerationPercentage } = getState().invoice
    try {
      dispatch(setInvoiceError(''))
      if (product != null && productDescription != '' && productQuantity > 0 &&  productPrice > 0) {
        let newProducts = null
        let tasaIva = product.ParametroImpuesto.TasaImpuesto
        if (tasaIva > 0 && customer.AplicaTasaDiferenciada) tasaIva = customer.ParametroImpuesto.TasaImpuesto
        const item = {
          IdProducto: product.IdProducto,
          Descripcion: productDescription,
          Cantidad: productQuantity,
          PrecioVenta: productPrice,
          Excento: tasaIva == 0,
          PrecioCosto: product.PrecioCosto,
          CostoInstalacion: 0,
          PorcentajeIVA: tasaIva
        }
        const index = products.findIndex(item => item.IdProducto == product.IdProducto)
        if (index >= 0) {
          newProducts = [...products.slice(0, index), item, ...products.slice(index + 1)]
        } else {
          newProducts = [...products, item]
        }
        dispatch(setProducts(newProducts))
        const summary = getInvoiceSummary(newProducts, exonerationPercentage)
        dispatch(setSummary(summary))
        dispatch(resetProduct())
      }
    } catch (error) {
      const errorMessage = error.message ? error.message : error
      dispatch(setInvoiceError(errorMessage))
    }
  }
}

export const removeProduct = (id) => {
  return (dispatch, getState) => {
    const { products, exonerationPercentage } = getState().invoice
    const index = products.findIndex(item => item.IdProducto == id)
    const newProducts = [...products.slice(0, index), ...products.slice(index + 1)]
    dispatch(setProducts(newProducts))
    const summary = getInvoiceSummary(newProducts, exonerationPercentage)
    dispatch(setSummary(summary))
  }
}

export function saveInvoice () {
  return async (dispatch, getState) => {
    const { serviceURL } = getState().config
    const { token, company } = getState().session
    const {
      paymentMethodId,
      customer,
      customerName,
      exonerationType,
      exonerationCode,
      exonerationEntity,
      exonerationDate,
      exonerationPercentage,
      products,
      excento,
      gravado,
      exonerado,
      impuesto,
      totalCosto,
      total
    } = getState().invoice
    dispatch(startLoader())
    dispatch(setInvoiceError(''))
    try {
      await saveInvoiceEntity(
        serviceURL,
        token,
        products,
        paymentMethodId,
        company,
        customer.IdCliente,
        customerName,
        excento,
        gravado,
        exonerado,
        impuesto,
        totalCosto,
        total,
        exonerationType,
        exonerationCode,
        exonerationEntity,
        exonerationDate,
        exonerationPercentage
      )
      dispatch(setSuccessful())
      dispatch(stopLoader())
    } catch (error) {
      dispatch(stopLoader())
      dispatch(setInvoiceError(error))
    }
  }
}

export const revokeInvoice = (idInvoice) => {
  return async (dispatch, getState) => {
    const { serviceURL } = getState().config
    const { token, company } = getState().session
    const { list } = getState().invoice
    dispatch(startLoader())
    try {
      await revokeInvoiceEntity(serviceURL, token, idInvoice, company.Usuario.IdUsuario)
      const index = list.findIndex(item => item.IdFactura == idInvoice)
      const newList = [...list.slice(0, index), { ...list[index], Estado: 'Anulando' }, ...list.slice(index + 1)]
      dispatch(setList(newList))
      dispatch(stopLoader())
    } catch (error) {
      dispatch(stopLoader())
    }
  }
}

export const getListFirstPage = () => {
  return async (dispatch, getState) => {
    const { serviceURL } = getState().config
    const { token, company } = getState().session
    dispatch(startLoader())
    try {
      dispatch(setListPage(1))
      const recordCount = await getProcessedInvoiceListCount(serviceURL, token, company.IdEmpresa, company.EquipoRegistrado.IdSucursal)
      dispatch(setListCount(recordCount))
      if (recordCount > 0) {
        const newList = await getProcessedInvoiceListPerPage(serviceURL, token, company.IdEmpresa, company.EquipoRegistrado.IdSucursal, 1, 10)
        dispatch(setList(newList))
      } else {
        dispatch(setList([]))
      }
      dispatch(stopLoader())
    } catch (error) {
      dispatch(setInvoiceError(error))
      dispatch(stopLoader())
    }
  }
}

export const getListByPageNumber = (pageNumber) => {
  return async (dispatch, getState) => {
    const { serviceURL } = getState().config
    const { token, company } = getState().session
    dispatch(startLoader())
    try {
      const newList = await getProcessedInvoiceListPerPage(serviceURL, token, company.IdEmpresa, company.EquipoRegistrado.IdSucursal, pageNumber, 10)
      dispatch(setListPage(pageNumber))
      dispatch(setList(newList))
      dispatch(stopLoader())
    } catch (error) {
      dispatch(setInvoiceError(error))
      dispatch(stopLoader())
    }
  }
}
