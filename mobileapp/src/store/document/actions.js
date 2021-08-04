import {
  SET_PROCESSED_LIST,
  SET_PROCESSED_COUNT,
  SET_PROCESSED_PAGE,
  SET_PROCESSED_SELECTED,
  SET_PROCESSED_EMAIL,
  SET_PROCESSED_RESPONSE,
  SET_PROCESSED_MESSAGE,
  SET_PROCESSED_ERROR,
  SET_PENDING_LIST
} from './types'

import { startLoader, stopLoader } from '../ui/actions'

import {
  getProcessedDocumentListCount,
  getProcessedDocumentListPerPage,
  getDocumentEntity,
  sendDocumentByEmail
} from 'utils/domainHelper'

import { arrayToString, xmlToHtmlString } from 'utils/formatHelper'

export const setProcessedList = (newList) => {
  return {
    type: SET_PROCESSED_LIST,
    payload: { newList }
  }
}

export const setProcessedCount = (count) => {
  return {
    type: SET_PROCESSED_COUNT,
    payload: { count }
  }
}

export const setProcessedPage = (page) => {
  return {
    type: SET_PROCESSED_PAGE,
    payload: { page }
  }
}

export const setProcessedSelected = (item) => {
  return {
    type: SET_PROCESSED_SELECTED,
    payload: { item }
  }
}

export const setProcessedEmail = (email) => {
  return {
    type: SET_PROCESSED_EMAIL,
    payload: { email }
  }
}

export const setProcessedResponse = (response) => {
  return {
    type: SET_PROCESSED_RESPONSE,
    payload: { response }
  }
}

export const setProcessedMessage = (message) => {
  return {
    type: SET_PROCESSED_MESSAGE,
    payload: { message }
  }
}

export const setProcessedError = (error) => {
  return {
    type: SET_PROCESSED_ERROR,
    payload: { error }
  }
}

export const setPendingList = (newList) => {
  return {
    type: SET_PENDING_LIST,
    payload: { newList }
  }
}

export const getProcessedFirstPage = () => {
  return async (dispatch, getState) => {
    const { serviceURL } = getState().config
    const { token, company } = getState().session
    dispatch(startLoader())
    try {
      dispatch(setProcessedPage(1))
      const recordCount = await getProcessedDocumentListCount(serviceURL, token, company.IdEmpresa, company.EquipoRegistrado.IdSucursal)
      dispatch(setProcessedCount(recordCount))
      if (recordCount > 0) {
        const newList = await getProcessedDocumentListPerPage(serviceURL, token, company.IdEmpresa, company.EquipoRegistrado.IdSucursal, 1, 10)
        dispatch(setProcessedList(newList))
      } else {
        dispatch(setProcessedList([]))
      }
      dispatch(stopLoader())
    } catch (error) {
      dispatch(stopLoader())
    }
  }
}

export const getProcessedPageByPageNumber = (pageNumber) => {
  return async (dispatch, getState) => {
    const { serviceURL } = getState().config
    const { token, company } = getState().session
    dispatch(startLoader())
    try {
      const newList = await getProcessedDocumentListPerPage(serviceURL, token, company.IdEmpresa, company.EquipoRegistrado.IdSucursal, pageNumber, 10)
      dispatch(setProcessedPage(pageNumber))
      dispatch(setProcessedList(newList))
      dispatch(stopLoader())
    } catch (error) {
      dispatch(stopLoader())
    }
  }
}

export const sendNotification = (idDocument, emailTo) => {
  return async (dispatch, getState) => {
    const { serviceURL } = getState().config
    const { token } = getState().session
    dispatch(startLoader())
    dispatch(setProcessedMessage(''))
    dispatch(setProcessedError(''))
    try {
      await sendDocumentByEmail(serviceURL, token, idDocument, emailTo)
      dispatch(setProcessedMessage('Correo enviado satisfactoriamente.'))
      dispatch(stopLoader())
    } catch (error) {
      dispatch(setProcessedError(error))
      dispatch(stopLoader())
    }
  }
}

export const getProcessedResponse = (idDocument) => {
  return async (dispatch, getState) => {
    const { serviceURL } = getState().config
    const { token } = getState().session
    dispatch(startLoader())
    dispatch(setProcessedError(''))
    dispatch(setProcessedResponse(''))
    try {
      const response = await getDocumentEntity(serviceURL, token, idDocument)
      const texto = arrayToString(response.Respuesta)
      const html = xmlToHtmlString(texto)
      dispatch(setProcessedResponse(html))
      dispatch(stopLoader())
    } catch (error) {
      dispatch(setProcessedError(error))
      dispatch(stopLoader())
    }
  }
}
