import Config from 'react-native-config'
import { getAppstoreAppMetadata } from 'react-native-appstore-version-checker'

import {
  SET_SERVICE_URL,
  SET_APP_READY,
  SET_DEVICE_REGISTERED,
  SET_IDENTIFIER_LIST,
  SET_TERMINAL_LIST,
  SET_SIGNUP_ERROR,
  SET_CONFIG_ERROR,
  SET_TERMINAL
} from './types'

import {
  getLatestAppVersion,
  getCompanyIdentifiers,
  getTerminalsAvailablePerId,
  registerDevice,
  getConfiguration,
  saveConfiguration
} from 'utils/domainHelper'

import { startLoader, stopLoader, setModalError } from '../ui/actions'

import DeviceInfo from 'react-native-device-info'

export const setServiceURL = (serviceURL) => {
  return {
    type: SET_SERVICE_URL,
    payload: { serviceURL }
  }
}

export const setAppReady = (deviceId, status) => {
  return {
    type: SET_APP_READY,
    payload: { deviceId, status }
  }
}

export const setDeviceRegistered = () => {
  return {
    type: SET_DEVICE_REGISTERED
  }
}

export const setIdentifierList = (identifierList) => {
  return {
    type: SET_IDENTIFIER_LIST,
    payload: { identifierList }
  }
}

export const setAvailableTerminals = (terminalList) => {
  return {
    type: SET_TERMINAL_LIST,
    payload: { terminalList }
  }
}

export const setSignUpError = (error) => {
  return {
    type: SET_SIGNUP_ERROR,
    payload: { error }
  }
}

export const setConfigError = (message) => {
  return {
    type: SET_CONFIG_ERROR,
    payload: { message }
  }
}

export const setTerminal = (entity) => {
  return {
    type: SET_TERMINAL,
    payload: { entity }
  }
}

export function validateAppState () {
  return async (dispatch) => {
    const serviceURL = Config.SERVER_URL
    dispatch(setServiceURL(serviceURL))
    dispatch(startLoader())
    dispatch(setModalError(''))
    try {
      const metadata = await getAppstoreAppMetadata("com.jlcfacturacion")
      const deviceId = await DeviceInfo.getAndroidId()
      const currentVersion = await DeviceInfo.getVersion()
      if (metadata.version !== currentVersion) {
        dispatch(setAppReady(deviceId, 'outdated'))
      } else {
        const identifierList = await getCompanyIdentifiers(serviceURL, deviceId)
        if (identifierList.length > 0) dispatch(setDeviceRegistered())
        dispatch(setIdentifierList(identifierList))
        dispatch(setAppReady(deviceId, 'ready'))
      }
      dispatch(stopLoader())
    } catch (error) {
      dispatch(stopLoader())
      dispatch(setModalError(error))
    }
  }
}

export function getCompanyList () {
  return async (dispatch, getState) => {
    const { serviceURL, deviceId } = getState().config
    dispatch(startLoader())
    dispatch(setModalError(''))
    try {
      const identifierList = await getCompanyIdentifiers(serviceURL, deviceId)
      dispatch(setIdentifierList(identifierList))
      dispatch(stopLoader())
    } catch (error) {
      dispatch(stopLoader())
      dispatch(setModalError(error))
    }
  }
}

export function getAvailableTerminals(user, password, id) {
  return async (dispatch, getState) => {
    const { serviceURL } = getState().config
    dispatch(startLoader())
    try {
      const devicesList = await getTerminalsAvailablePerId(serviceURL, user, password, id)
      dispatch(setAvailableTerminals(devicesList))
      dispatch(stopLoader())
      if (devicesList.length == 0) {
        dispatch(setAvailableTerminals([]))
        dispatch(setSignUpError("No existen terminales disponibles para su empresa. Por favor contacte con su proveedor."))
      }
    } catch (error) {
      dispatch(setAvailableTerminals([]))
      dispatch(stopLoader())
      dispatch(setSignUpError(error))
    }
  }
}

export function signUp (user, password, id, branchId, terminalId) {
  return async (dispatch, getState) => {
    const { serviceURL, deviceId } = getState().config
    dispatch(startLoader())
    try {
      await registerDevice(serviceURL, user, password, id, branchId, terminalId, deviceId)
      dispatch(setDeviceRegistered())
      dispatch(setAvailableTerminals([]))
      setTimeout(async () => {
        try {
          const identifierList = await getCompanyIdentifiers(serviceURL, deviceId)
          dispatch(setIdentifierList(identifierList))
          dispatch(stopLoader())
        } catch (error) {
          dispatch(stopLoader())
          dispatch(setModalError(error))
        }
      }, 500)
    } catch (error) {
      dispatch(stopLoader())
      dispatch(setSignUpError(error))
    }
  }
}


export function getConfig()
{
  return async (dispatch, getState) => {
    const { serviceURL } = getState().config
    const { token, company } = getState().session
    dispatch(startLoader())
    dispatch(setConfigError(''))
    try {
      const entity = await getConfiguration(serviceURL, token, company.IdEmpresa, company.EquipoRegistrado.IdSucursal, company.EquipoRegistrado.IdTerminal)
      dispatch(setTerminal(entity))
      dispatch(stopLoader())
    } catch (error) {
      dispatch(setConfigError(error))
      dispatch(stopLoader())
      
    }
  }
}

export function saveConfig(terminal)
{
  return async (dispatch, getState) => {
    const { serviceURL } = getState().config
    const { token } = getState().session
    dispatch(startLoader())
    dispatch(setConfigError(''))
    try {
      await saveConfiguration(serviceURL, token, terminal)
      dispatch(setTerminal(entity))
      dispatch(stopLoader())
    } catch (error) {
      dispatch(setConfigError(error))
      dispatch(stopLoader())
      
    }
  }
}
