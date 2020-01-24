import {
  SET_ERROR,
  SET_PAYMENT_BANK_ID,
  SET_EXONERATION_TYPE,
  SET_EXONERATION_DESC,
  SET_EXONERATION_CODE,
  SET_EXONERATION_ENTITY,
  SET_EXONERATION_DATE,
  SET_EXONERATION_PERCENTAGE,
  SET_CUSTOMER,
  SET_CUSTOMER_NAME,
  SET_PRODUCT,
  RESET_PRODUCT,
  SET_PRODUCT_DESCRIPTION,
  SET_PRODUCT_QUANTITY,
  SET_PRODUCT_PRICE,
  SET_PRODUCTS,
  SET_SUMMARY,
  RESET_INVOICE,
  SUCCESSFUL_STATUS,
  SET_LIST,
  SET_LIST_COUNT,
  SET_LIST_PAGE
} from './types'

export const invoiceReducer = (state = {}, { type, payload }) => {
  switch (type) {
    case SET_ERROR:
      return { ...state, error: payload.error }
    case SET_PAYMENT_BANK_ID:
      return { ...state, paymentBankId: payload.id }
    case SET_EXONERATION_TYPE:
      return { ...state, exonerationType: payload.id }
    case SET_EXONERATION_DESC:
      return { ...state, exonerationDesc: payload.desc }
    case SET_EXONERATION_CODE:
      return { ...state, exonerationCode: payload.code }
    case SET_EXONERATION_ENTITY:
      return { ...state, exonerationEntity: payload.name }
    case SET_EXONERATION_DATE:
      return { ...state, exonerationDate: payload.date }
    case SET_EXONERATION_PERCENTAGE:
      return { ...state, exonerationPercentage: payload.percentage }
    case SET_CUSTOMER:
      return { ...state, customer: payload.entity, customerName: payload.entity.Nombre }
    case SET_CUSTOMER_NAME:
      return { ...state, customerName: payload.name }
    case SET_PRODUCT:
      return { ...state,
        product: payload.entity,
        productDescription: payload.entity.Descripcion,
        productQuantity: 1,
        productPrice: payload.entity.PrecioVenta
      }
    case RESET_PRODUCT:
      return { ...state,
        product: null,
        productDescription: '',
        productQuantity: 1,
        productPrice: 0
      }
    case SET_PRODUCT_DESCRIPTION:
      return { ...state, productDescription: payload.description }
    case SET_PRODUCT_QUANTITY:
      return { ...state, productQuantity: payload.quantity }
    case SET_PRODUCT_PRICE:
      return { ...state, productPrice: payload.price }
    case SET_PRODUCTS:
      return { ...state, products: payload.newList }
    case SET_SUMMARY:
      return { ...state,
        gravado: payload.summary.gravado,
        exonerado: payload.summary.exonerado,
        excento: payload.summary.excento,
        subTotal: payload.summary.subTotal,
        impuesto: payload.summary.impuesto,
        total: payload.summary.total,
        totalCosto: payload.summary.totalCosto
      }
    case RESET_INVOICE:
      return { ...state,
        exonerationType: 1,
        exonerationDesc: '',
        exonerationCode: '',
        exonerationEntity: '',
        exonerationDate: '01/01/2019',
        exonerationPercentage: 0,
        product: null,
        productDescription: '',
        productQuantity: 1,
        productPrice: 0,
        products: [],
        gravado: 0,
        exonerado: 0,
        excento: 0,
        subTotal: 0,
        impuesto: 0,
        total: 0,
        totalCosto: 0,
        successful: false
      }
    case SUCCESSFUL_STATUS:
      return { ...state, successful: true }
    case SET_LIST:
      return { ...state, list: payload.newList }
    case SET_LIST_COUNT:
      return { ...state, listCount: payload.count }
    case SET_LIST_PAGE:
      return { ...state, listPage: payload.page }
    default:
      return state
  }
}

export default invoiceReducer
