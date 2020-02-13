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

export const configReducer = (state = {}, { type, payload }) => {
  switch (type) {
    case SET_SERVICE_URL:
      return { ...state, serviceURL: payload.serviceURL }
    case SET_APP_READY:
      return { ...state, deviceId: payload.deviceId, appReady: payload.status }
    case SET_DEVICE_REGISTERED:
      return { ...state, isDeviceRegistered: true }
    case SET_IDENTIFIER_LIST:
      return { ...state, identifierList: payload.identifierList }
    case SET_TERMINAL_LIST:
      return { ...state, availableTerminalList: payload.terminalList }
    case SET_SIGNUP_ERROR:
      return { ...state, signUpError: payload.error }
    case SET_CONFIG_ERROR:
      return { ...state, configError: payload.error }
    case SET_TERMINAL:
      return { ...state, terminal: payload.entity }
    default:
      return state
  }
}

export default configReducer
