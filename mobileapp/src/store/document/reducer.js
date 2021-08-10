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

export const documentReducer = (state = {}, { type, payload }) => {
  switch (type) {
    case SET_PROCESSED_LIST:
      return { ...state, processedList: payload.newList }
    case SET_PROCESSED_COUNT:
      return { ...state, processedCount: payload.count }
    case SET_PROCESSED_PAGE:
      return { ...state, processedPage: payload.page }
    case SET_PROCESSED_SELECTED:
      return { ...state, processedMessage: '', processedError: '', processedSelected: payload.item }
    case SET_PROCESSED_EMAIL:
      return { ...state, processedSelected: { ...state.processedSelected, CorreoNotificacion: payload.email } }
    case SET_PROCESSED_RESPONSE:
      return { ...state, processedResponse: payload.response }
    case SET_PROCESSED_MESSAGE:
      return { ...state, processedMessage: payload.message }
    case SET_PROCESSED_ERROR:
      return { ...state, processedError: payload.entity }
    case SET_PENDING_LIST:
      return { ...state, pendingList: payload.newList }
    default:
      return state
  }
}

export default documentReducer
