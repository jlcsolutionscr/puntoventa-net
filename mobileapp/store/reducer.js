import { combineReducers } from 'redux'

import configReducer from './config/reducer'
import sessionReducer from './session/reducer'
import uiReducer from './ui/reducer'
import customerReducer from './customer/reducer'
import productReducer from './product/reducer'
import invoiceReducer from './invoice/reducer'
import documentReducer from './document/reducer'

export default combineReducers({
  config: configReducer,
  session: sessionReducer,
  ui: uiReducer,
  customer: customerReducer,
  product: productReducer,
  invoice: invoiceReducer,
  document: documentReducer
})
