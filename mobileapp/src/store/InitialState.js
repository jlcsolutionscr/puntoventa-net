export const INITIAL_STATE = {
  ui: {
    loading: true,
    loaderVisible: false,
    splashScreenDone: false,
    error: ''
  },
  config: {
    appReady: 'pending',
    deviceId: null,
    serviceURL: '',
    isDeviceRegistered: false,
    identifierList: [],
    availableTerminalList: [],
    signUpError: '',
    configError: '',
    terminal: null
  },
  session: {
    authorized: false,
    company: null,
    token: null,
    logInError: '',
    reportMessage: '',
    reportError: ''
  },
  customer: {
    entity: null,
    customerList: [],
    idTypeList: [],
    provinciaList: [],
    cantonList: [],
    distritoList: [],
    barrioList: [],
    rentTypeList: [],
    priceTypeList: [],
    exonerationTypeList: [],
    error: ''
  },
  product: {
    entity: null,
    productList: [],
    typeList: [],
    categoryList: [],
    providerList: [],
    rentTypeList: [],
    error: ''
  },
  invoice: {
    paymentMethodId: 1,
    customer: null,
    product: null,
    customerName: '',
    productDescription: '',
    productQuantity: 1,
    productPrice: 0,
    exonerationType: 1,
    exonerationDesc: '',
    exonerationCode: '',
    exonerationEntity: '',
    exonerationDate: '01/01/2019',
    exonerationPercentage: 0,
    products: [],
    gravado: 0,
    exonerado: 0,
    excento: 0,
    subTotal: 0,
    impuesto: 0,
    total: 0,
    totalCosto: 0,
    error: '',
    successful: false,
    list: [],
    listCount: 0,
    listPage: 1
  },
  document: {
    processedList: [],
    processedCount: 0,
    processedPage: 1,
    processedSelected: null,
    processedResponse: '',
    processedMessage: '',
    processedError: '',
    pendingList: []
  }
}