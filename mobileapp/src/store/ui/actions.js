import {
  START_LOADER,
  STOP_LOADER,
  SET_MODAL_ERROR
} from './types'

export const startLoader = () => {
  return {
    type: START_LOADER
  }
}

export const stopLoader = () => {
  return {
    type: STOP_LOADER
  }
}

export const setModalError = (error) => {
  return {
    type: SET_MODAL_ERROR,
    payload: { error }
  }
}
