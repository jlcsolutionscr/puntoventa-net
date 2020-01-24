import {
  SET_ERROR,
  SET_CUSTOMER_LIST,
  SET_ID_TYPE_LIST,
  SET_RENT_TYPE_LIST,
  SET_PROVINCIA_LIST,
  SET_CANTON_LIST,
  SET_DISTRITO_LIST,
  SET_BARRIO_LIST,
  SET_PRICE_TYPE_LIST,
  SET_EXONERATION_TYPE_LIST,
  SET_CUSTOMER
} from './types'

import {
  getCustomerList,
  getIdTypeList,
  getRentTypeList,
  getProvinciaList,
  getCantonList,
  getDistritoList,
  getBarrioList,
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

export const setProvinciaList = (newList) => {
  return {
    type: SET_PROVINCIA_LIST,
    payload: { newList }
  }
}

export const setCantonList = (newList) => {
  return {
    type: SET_CANTON_LIST,
    payload: { newList }
  }
}

export const setDistritoList = (newList) => {
  return {
    type: SET_DISTRITO_LIST,
    payload: { newList }
  }
}

export const setBarrioList = (newList) => {
  return {
    type: SET_BARRIO_LIST,
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
      if (provinciaList.length == 0) {
        const newList = await getProvinciaList(serviceURL, token)
        dispatch(setProvinciaList(newList))
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

export function setNewCantonList (idProvincia) {
  return async (dispatch, getState) => {
    const { serviceURL } = getState().config
    const { token } = getState().session
    dispatch(startLoader())
    dispatch(setCustomerError(''))
    try {
      let newList = await getCantonList(serviceURL, token, idProvincia)
      dispatch(setCantonList(newList))
      newList = await getDistritoList(serviceURL, token, idProvincia, 1)
      dispatch(setDistritoList(newList))
      newList = await getBarrioList(serviceURL, token, idProvincia, 1, 1)
      dispatch(setBarrioList(newList))
      dispatch(stopLoader())
    } catch (error) {
      dispatch(stopLoader())
      dispatch(setCustomerError(error))
    }
  }
}

export function setNewDistritoList (idProvincia, idCanton) {
  return async (dispatch, getState) => {
    const { serviceURL } = getState().config
    const { token } = getState().session
    dispatch(startLoader())
    dispatch(setCustomerError(''))
    try {
      let newList = await getDistritoList(serviceURL, token, idProvincia, idCanton)
      dispatch(setDistritoList(newList))
      newList = await getBarrioList(serviceURL, token, idProvincia, idCanton, 1)
      dispatch(setBarrioList(newList))
      dispatch(stopLoader())
    } catch (error) {
      dispatch(stopLoader())
      dispatch(setCustomerError(error))
    }
  }
}

export function setNewBarrioList (idProvincia, idCanton, idDistrito) {
  return async (dispatch, getState) => {
    const { serviceURL } = getState().config
    const { token } = getState().session
    dispatch(startLoader())
    dispatch(setCustomerError(''))
    try {
      const newList = await getBarrioList(serviceURL, token, idProvincia, idCanton, idDistrito)
      dispatch(setBarrioList(newList))
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
      let newList = await getCantonList(serviceURL, token, customer.IdProvincia)
      dispatch(setCantonList(newList))
      newList = await getDistritoList(serviceURL, token, customer.IdProvincia, customer.IdCanton)
      dispatch(setDistritoList(newList))
      newList = await getBarrioList(serviceURL, token, customer.IdProvincia, customer.IdCanton, customer.IdDistrito)
      dispatch(setBarrioList(newList))
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
