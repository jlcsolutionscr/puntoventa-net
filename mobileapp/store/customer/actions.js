import {
  SET_ERROR,
  SET_CUSTOMER_LIST,
  SET_ID_TYPE_LIST,
  SET_RENT_TYPE_LIST,
  SET_PRICE_TYPE_LIST,
  SET_EXONERATION_TYPE_LIST,
  SET_CUSTOMER
} from './types'

import {
  getCustomerList,
  getIdTypeList,
  getRentTypeList,
  getPriceTypeList,
  getExonerationTypeList,
  getCustomerEntity,
  saveCustomerEntity
} from '../../utils/domainHelper'

import { startLoader, stopLoader } from '../ui/actions'

export const setCustomerError = (error) => {
  return {
    type: SET_ERROR,
    payload: { error }
  }
}

export const setCustomerList = (newList) => {
  return {
    type: SET_CUSTOMER_LIST,
    payload: { newList }
  }
}

export const setIdTypeList = (newList) => {
  return {
    type: SET_ID_TYPE_LIST,
    payload: { newList }
  }
}

export const setRentTypeList = (newList) => {
  return {
    type: SET_RENT_TYPE_LIST,
    payload: { newList }
  }
}

export const setPriceTypeList = (newList) => {
  return {
    type: SET_PRICE_TYPE_LIST,
    payload: { newList }
  }
}

export const setExonerationTypeList = (list) => {
  return {
    type: SET_EXONERATION_TYPE_LIST,
    payload: { list }
  }
}

export const setCustomer = (entity) => {
  return {
    type: SET_CUSTOMER,
    payload: { entity }
  }
}

export function setParameters () {
  return async (dispatch, getState) => {
    const { serviceURL } = getState().config
    const { token, company } = getState().session
    const { idTypeList, rentTypeList, priceTypeList, provinciaList, exonerationTypeList } = getState().customer
    dispatch(startLoader())
    dispatch(setCustomerError(''))
    try {
      const newList = await getCustomerList(serviceURL, token, company.IdEmpresa)
      dispatch(setCustomerList(newList))
      if (idTypeList.length == 0) {
        const newList = await getIdTypeList(serviceURL, token)
        dispatch(setIdTypeList(newList))
      }
      if (rentTypeList.length == 0) {
        const newList = await getRentTypeList(serviceURL, token)
        dispatch(setRentTypeList(newList))
      }
      if (priceTypeList.length == 0) {
        const newList = await getPriceTypeList(serviceURL, token)
        dispatch(setPriceTypeList(newList))
      }
      if (exonerationTypeList.length == 0) {
        const newList = await getExonerationTypeList(serviceURL, token)
        dispatch(setExonerationTypeList(newList))
      }
      dispatch(stopLoader())
    } catch (error) {
      dispatch(stopLoader())
      dispatch(setCustomerError(error))
    }
  }
}

export function getCustomer (idCustomer) {
  return async (dispatch, getState) => {
    const { serviceURL } = getState().config
    const { token } = getState().session
    dispatch(startLoader())
    dispatch(setCustomerError(''))
    try {
      const customer = await getCustomerEntity(serviceURL, token, idCustomer)
      dispatch(setCustomer(customer))
      dispatch(stopLoader())
    } catch (error) {
      dispatch(stopLoader())
      dispatch(setCustomer(null))
      dispatch(setCustomerError(error))
    }
  }
}

export function saveCustomerItem (customer) {
  return async (dispatch, getState) => {
    const { serviceURL } = getState().config
    const { token, company } = getState().session
    dispatch(startLoader())
    dispatch(setCustomerError(''))
    try {
      customer.IdEmpresa = company.IdEmpresa
      await saveCustomerEntity(serviceURL, token, customer)
      const newList = await getCustomerList(serviceURL, token, company.IdEmpresa)
      dispatch(setCustomerList(newList))
      dispatch(stopLoader())
    } catch (error) {
      dispatch(stopLoader())
      dispatch(setCustomerError(error))
    }
  }
}
