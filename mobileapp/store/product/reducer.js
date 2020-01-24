import {
  SET_ERROR,
  SET_PRODUCT_LIST,
  SET_TYPE_LIST,
  SET_CATEGORY_LIST,
  SET_PROVIDER_LIST,
  SET_RENT_TYPE_LIST,
  SET_PRODUCT
} from './types'

export const productReducer = (state = {}, { type, payload }) => {
  switch (type) {
    case SET_ERROR:
      return { ...state, error: payload.error }
    case SET_PRODUCT_LIST:
      return { ...state, productList: payload.newList }
    case SET_TYPE_LIST:
      return { ...state, typeList: payload.newList }
    case SET_CATEGORY_LIST:
        return { ...state, categoryList: payload.newList }
    case SET_PROVIDER_LIST:
      return { ...state, providerList: payload.newList }
    case SET_RENT_TYPE_LIST:
      return { ...state, rentTypeList: payload.newList }
    case SET_PRODUCT:
      return { ...state, entity: payload.entity }
    default:
      return state
  }
}

export default productReducer
