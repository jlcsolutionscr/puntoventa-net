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

export const customerReducer = (state = {}, { type, payload }) => {
  switch (type) {
    case SET_ERROR:
      return { ...state, error: payload.error }
    case SET_CUSTOMER_LIST:
      return { ...state, customerList: payload.newList }
    case SET_ID_TYPE_LIST:
      return { ...state, idTypeList: payload.newList }
    case SET_RENT_TYPE_LIST:
        return { ...state, rentTypeList: payload.newList }
    case SET_PROVINCIA_LIST:
      return { ...state, provinciaList: payload.newList }
    case SET_CANTON_LIST:
      return { ...state, cantonList: payload.newList }
    case SET_DISTRITO_LIST:
      return { ...state, distritoList: payload.newList }
    case SET_BARRIO_LIST:
      return { ...state, barrioList: payload.newList }
    case SET_PRICE_TYPE_LIST:
      return { ...state, priceTypeList: payload.newList }
    case SET_EXONERATION_TYPE_LIST:
      return { ...state, exonerationTypeList: payload.list }
    case SET_CUSTOMER:
      return { ...state, entity: payload.entity }
    default:
      return state
  }
}

export default customerReducer
