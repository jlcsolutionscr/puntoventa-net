import {
  SET_ERROR,
  SET_PRODUCT_LIST,
  SET_TYPE_LIST,
  SET_CATEGORY_LIST,
  SET_PROVIDER_LIST,
  SET_RENT_TYPE_LIST,
  SET_PRODUCT
} from './types'

import {
  getProductList,
  getProductTypeList,
  getProductCategoryList,
  getProductProviderList,
  getRentTypeList,
  getProductEntity,
  saveProductEntity
} from '../../utils/domainHelper'

import { startLoader, stopLoader } from '../ui/actions'

export const setProductError = (error) => {
  return {
    type: SET_ERROR,
    payload: { error }
  }
}

export const setProductList = (newList) => {
  return {
    type: SET_PRODUCT_LIST,
    payload: { newList }
  }
}

export const setTypeList = (newList) => {
  return {
    type: SET_TYPE_LIST,
    payload: { newList }
  }
}

export const setCategoryList = (newList) => {
  return {
    type: SET_CATEGORY_LIST,
    payload: { newList }
  }
}

export const setProviderList = (newList) => {
  return {
    type: SET_PROVIDER_LIST,
    payload: { newList }
  }
}

export const setRentTypeList = (newList) => {
  return {
    type: SET_RENT_TYPE_LIST,
    payload: { newList }
  }
}

export const setProduct = (entity) => {
  return {
    type: SET_PRODUCT,
    payload: { entity }
  }
}

export function setParameters () {
  return async (dispatch, getState) => {
    const { serviceURL } = getState().config
    const { token, company } = getState().session
    const { rentTypeList } = getState().product
    dispatch(startLoader())
    dispatch(setProductError(''))
    try {
      let newList = await getProductList(serviceURL, token, company.IdEmpresa, company.EquipoRegistrado.IdSucursal, '')
      dispatch(setProductList(newList))
      newList = await getProductTypeList(serviceURL, token)
      dispatch(setTypeList(newList))
      newList = await getProductCategoryList(serviceURL, token, company.IdEmpresa)
      dispatch(setCategoryList(newList))
      newList = await getProductProviderList(serviceURL, token, company.IdEmpresa)
      dispatch(setProviderList(newList))
      if (rentTypeList.length == 0) {
        newList = await getRentTypeList(serviceURL, token)
        dispatch(setRentTypeList(newList))
      }
      dispatch(stopLoader())
    } catch (error) {
      dispatch(stopLoader())
      dispatch(setProductError(error))
    }
  }
}

export function getProduct (idProduct) {
  return async (dispatch, getState) => {
    const { serviceURL } = getState().config
    const { token, company } = getState().session
    dispatch(startLoader())
    dispatch(setProductError(''))
    try {
      const product = await getProductEntity(serviceURL, token, idProduct, company.EquipoRegistrado.IdSucursal)
      dispatch(setProduct(product))
      let newList = await getProductCategoryList(serviceURL, token, company.IdEmpresa)
      dispatch(setCategoryList(newList))
      dispatch(stopLoader())
    } catch (error) {
      dispatch(stopLoader())
      dispatch(setProduct(null))
      dispatch(setProductError(error))
    }
  }
}

export function saveProductItem (product) {
  return async (dispatch, getState) => {
    const { serviceURL } = getState().config
    const { token, company } = getState().session
    dispatch(startLoader())
    dispatch(setProductError(''))
    try {
      product.IdEmpresa = company.IdEmpresa
      await saveProductEntity(serviceURL, token, product)
      const newList = await getProductList(serviceURL, token, company.IdEmpresa, company.EquipoRegistrado.IdSucursal)
      dispatch(setProductList(newList))
      dispatch(stopLoader())
    } catch (error) {
      dispatch(stopLoader())
      dispatch(setProductError(error))
    }
  }
}
