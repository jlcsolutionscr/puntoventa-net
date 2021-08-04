import {
  SET_AUTHORIZED,
  SET_UNAUTHORIZED,
  SET_LOGIN_ERROR,
  SET_REPORT_MESSAGE,
  SET_REPORT_ERROR
} from './types'

const sessionReducer = (state = {}, { type, payload }) => {
  switch (type) {
    case SET_AUTHORIZED:
      return { ...state, authorized: payload.authorized, company: payload.company, token: payload.company.Usuario.Token }
    case SET_UNAUTHORIZED:
      return { ...state, authorized: false, company: null, token: null }
    case SET_LOGIN_ERROR:
      return { ...state, logInError: payload.error }
    case SET_REPORT_MESSAGE:
      return { ...state, reportMessage: payload.message }
    case SET_REPORT_ERROR:
      return { ...state, reportError: payload.error }
    default:
      return state
  }
}

export default sessionReducer
