import {
  SET_AUTHORIZED,
  SET_UNAUTHORIZED,
  SET_LOGIN_ERROR,
  SET_REPORT_MESSAGE,
  SET_REPORT_ERROR
} from './types'

import { startLoader, stopLoader } from '../ui/actions'

import { validateCredentials, sendReportEmail } from '../../utils/domainHelper'

export const setAuthorized = (company, authorized) => {
  return {
    type: SET_AUTHORIZED,
    payload: { company, authorized }
  }
}

export const setUnauthorized = () => {
  return {
    type: SET_UNAUTHORIZED
  }
}

export const setLogInError = (error) => {
  return {
    type: SET_LOGIN_ERROR,
    payload: { error }
  }
}

export const setReportMessage = (message) => {
  return {
    type: SET_REPORT_MESSAGE,
    payload: { message }
  }
}

export const setReportError = (error) => {
  return {
    type: SET_REPORT_ERROR,
    payload: { error }
  }
}

export function logIn (user, password, companyId) {
  return async (dispatch, getState) => {
    const { serviceURL, deviceId } = getState().config
    dispatch(startLoader())
    try {
      const company = await validateCredentials(serviceURL, user, password, companyId, deviceId)
      dispatch(setAuthorized(company, true))
      dispatch(stopLoader())
    } catch (error) {
      dispatch(stopLoader())
      dispatch(setLogInError(error))
    }
  }
}

export function logOut () {
  return async (dispatch) => {
    dispatch(setUnauthorized())
  }
}

export function generateReportEmail (reportName, startDate, endDate) {
  return async (dispatch, getState) => {
    const { serviceURL } = getState().config
    const { token, company } = getState().session
    dispatch(startLoader())
    dispatch(setReportMessage(''))
    dispatch(setReportError(''))
    try {
      await sendReportEmail(serviceURL, token, company.IdEmpresa, reportName, startDate, endDate)
      dispatch(setReportMessage('Reporte enviado al correo satisfactoriamente.'))
      dispatch(stopLoader())
    } catch (error) {
      dispatch(setReportError(error))
      dispatch(stopLoader())
      
    }
  }
}
