import React, { Component } from 'react'
import { connect } from 'react-redux'
import { bindActionCreators } from 'redux'

import {
  setParameters,
  resetInvoice,
  getCustomer,
  setCustomerName
} from '../../../store/invoice/actions'

import { StyleSheet, View, ScrollView } from 'react-native'
import SearchableDropdown from '../../custom/SearchableDropdown'
import TextField from '../../custom/TextField'
import TextLabel from '../../custom/TextLabel'

class Page1Screen extends Component {
  constructor (props) {
    super(props)
    this.state = {
      selected: null,
      product: null,
      password: ''
    }
  }

  componentDidMount () {
    this.props.setParameters()
  }

  componentWillUnmount () {
    this.props.resetInvoice()
  }

  render () {
    const {
      customer,
      customerName,
      customerList,
      exonerationDesc,
      exonerationCode,
      exonerationEntity,
      exonerationDate,
      exonerationPercentage,
    } = this.props
    let customers = customerList.map(item => {
      return { id: item.Id, name: item.Descripcion }
    })
    customers = [{id: 1, name: 'CLIENTE DE CONTADO'}, ...customers]
    return (<View key='1' style={styles.container}>
      <ScrollView keyboardShouldPersistTaps='handled'>
        <SearchableDropdown
          label='Seleccione un cliente'
          items={customers}
          selectedItemId={customer != null ? customer.IdCliente : null}
          onItemSelect={(item) => this.props.getCustomer(item.id)}
        />
        <TextField
          editable={customer !== null && customer.IdCliente === 1}
          label='Nombre del cliente'
          value={customerName}
          onChangeText={(value) => this.props.setCustomerName(value)}
        />
        <TextLabel
          label='Tipo de exoneración'
          value={exonerationDesc}
        />
        <TextLabel
          label='Código del documento'
          value={exonerationCode}
        />
        <TextLabel
          label='Nombre de la institución'
          value={exonerationEntity}
        />
        <TextLabel
          label='Fecha de emisión'
          value={exonerationDate}
        />
        <TextLabel
          label='Porcentaje de exoneración'
          value={exonerationPercentage}
        />
      </ScrollView>
    </View>)
  }
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 10
  }
})

const mapStateToProps = (state) => {
  return {
    customer: state.invoice.customer,
    customerName: state.invoice.customerName,
    customerList: state.customer.customerList,
    productList: state.product.productList,
    exonerationDesc: state.invoice.exonerationDesc,
    exonerationCode: state.invoice.exonerationCode,
    exonerationEntity: state.invoice.exonerationEntity,
    exonerationDate: state.invoice.exonerationDate,
    exonerationPercentage: state.invoice.exonerationPercentage
  }
}

const mapDispatchToProps = (dispatch) => {
  return bindActionCreators({
    setParameters,
    resetInvoice,
    getCustomer,
    setCustomerName
  }, dispatch)
}

export default connect(mapStateToProps, mapDispatchToProps)(Page1Screen)
