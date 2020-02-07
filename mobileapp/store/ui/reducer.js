import {
  START_LOADER,
  STOP_LOADER,
  SET_MODAL_ERROR
} from './types'

const configReducer = (state = {}, { type, payload }) => {
  switch (type) {
    case START_LOADER:
      return { ...state, loaderVisible: true }
    case STOP_LOADER:
      return { ...state, loaderVisible: false }
    case SET_MODAL_ERROR:
      return { ...state, error: payload.error }
    default:
      return state
  }
}

export default configReducer
