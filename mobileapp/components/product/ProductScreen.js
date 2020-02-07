import React, { Component } from 'react'
import { connect } from 'react-redux'
import { bindActionCreators } from 'redux'

import {
  setParameters,
  getProduct,
  setProduct,
  saveProductItem
} from '../../store/product/actions'

import { StyleSheet, View, ScrollView, Text } from 'react-native'
import SearchableDropdown from '../custom/SearchableDropdown'
import Dropdown from '../custom/Dropdown'
import TextField from '../custom/TextField'
import Button from '../custom/Button'

class ProductScreen extends Component {
  constructor (props) {
    super(props)
    this.state = {
      tipo: 1,
      idLinea: 0,
      codigo: '',
      codigoProveedor: '',
      idProveedor: 0,
      descripcion: '',
      precioCosto: 0,
      precioVenta1: 0,
      precioVenta2: 0,
      precioVenta3: 0,
      precioVenta4: 0,
      precioVenta5: 0,
      idImpuesto: 8,
      observacion: ''
    }
  }

  componentDidMount () {
    this.props.setParameters()
  }

  componentWillUnmount () {
    if (this.props.product != null) this.props.setProduct(null)
  }

  componentDidUpdate (prevProps) {
    const { product } = this.props
    if (product != null) {
      if (prevProps.product == null || prevProps.product.IdProducto != product.IdProducto) {
        this.setState({
          tipo: product.Tipo,
          idLinea: product.IdLinea,
          codigo: product.Codigo,
          codigoProveedor: product.CodigoProveedor,
          idProveedor: product.IdProveedor,
          descripcion: product.Descripcion,
          precioCosto: product.PrecioCosto,
          precioVenta1: product.PrecioVenta1,
          precioVenta2: product.PrecioVenta2,
          precioVenta3: product.PrecioVenta3,
          precioVenta4: product.PrecioVenta4,
          precioVenta5: product.PrecioVenta5,
          idImpuesto: product.IdImpuesto,
          observacion: product.Observacion
        })
      }
    } else if (prevProps.product != null) {
      this.state = {
        tipo: 1,
        idLinea: 0,
        codigo: '',
        codigoProveedor: '',
        idProveedor: 0,
        descripcion: '',
        precioCosto: 0,
        precioVenta1: 0,
        precioVenta2: 0,
        precioVenta3: 0,
        precioVenta4: 0,
        precioVenta5: 0,
        idImpuesto: 8,
        observacion: ''
      }
    }
  }

  render () {
    const { error, productList, typeList, categoryList, providerList, rentTypeList } = this.props
    const {
      tipo,
      idLinea,
      codigo,
      codigoProveedor,
      idProveedor,
      descripcion,
      precioCosto,
      precioVenta1,
      precioVenta2,
      precioVenta3,
      precioVenta4,
      precioVenta5,
      idImpuesto,
      observacion
    } = this.state
    const products = productList.map(item => {
      return { id: item.Id, name: item.Descripcion }
    })
    const productTypes = typeList.map(item => {
      return { value: item.Id, label: item.Descripcion }
    })
    const categories = categoryList.map(item => {
      return { value: item.Id, label: item.Descripcion }
    })
    const providers = providerList.map(item => {
      return { id: item.Id, name: item.Descripcion }
    })
    const rentTypes = rentTypeList.map(item => {
      return { value: item.Id, label: item.Descripcion }
    })
    const buttonEnabled = tipo != null &&
      idLinea != null &&
      codigo != '' &&
      idProveedor != null &&
      descripcion != '' &&
      precioCosto != null &&
      precioVenta1 > 0 &&
      precioVenta2 != null &&
      precioVenta3 != null &&
      precioVenta4 != null &&
      precioVenta5 != null &&
      idImpuesto != null
    return (<View key='1' style={styles.container}>
      {error !== '' && <Text style={{color: 'red', textAlign: 'center'}}>{error}</Text>}
      <SearchableDropdown label='Seleccione un producto' items={products} onItemSelect={(item) => this.props.getProduct(item.id)} />
      <ScrollView keyboardShouldPersistTaps='handled'>
        <View key='2' style={styles.content}>
          <Dropdown
            selectedValue={tipo}
            label='Seleccione el tipo de producto'
            items={productTypes}
            resetValue={false}
            onValueChange={(value, itemPosition) => this.setState({ tipo: value })}
          />
          <Dropdown
            selectedValue={idLinea}
            label='Seleccione la línea del producto'
            items={categories}
            onValueChange={(value, itemPosition) => this.handleOnChange({idLinea: value})}
          />
          <SearchableDropdown
            label='Seleccione el proveedor'
            items={providers}
            selectedItemId={idProveedor}
            onItemSelect={(item) => this.handleOnChange({idProveedor: item.id})}
          />
          <TextField
            label='Código del producto'
            value={codigo}
            onChangeText={(value) => this.handleOnChange({codigo: value})}
          />
          <TextField
            label='Código proveedor del producto'
            value={codigoProveedor}
            onChangeText={(value) => this.handleOnChange({codigoProveedor: value})}
          />
          <TextField
            label='Descripción del producto'
            value={descripcion}
            onChangeText={(value) => this.handleOnChange({descripcion: value})}
          />
          <TextField
            label='Precio de costo'
            keyboardType='numeric'
            currencyFormat
            value={precioCosto}
            onChangeText={(value) => this.handleOnChange({precioCosto: value})}
          />
          <TextField
            label='Precio de venta 1'
            keyboardType='numeric'
            currencyFormat
            value={precioVenta1}
            onChangeText={(value) => this.handleOnChange({precioVenta1: value})}
          />
          <TextField
            label='Precio de venta 2'
            keyboardType='numeric'
            currencyFormat
            value={precioVenta2}
            onChangeText={(value) => this.handleOnChange({precioVenta2: value})}
          />
          <TextField
            label='Precio de venta 3'
            keyboardType='numeric'
            currencyFormat
            value={precioVenta3}
            onChangeText={(value) => this.handleOnChange({precioVenta3: value})}
          />
          <TextField
            label='Precio de venta 4'
            keyboardType='numeric'
            currencyFormat
            value={precioVenta4}
            onChangeText={(value) => this.handleOnChange({precioVenta4: value})}
          />
          <TextField
            label='Precio de venta 5'
            keyboardType='numeric'
            currencyFormat
            value={precioVenta5}
            onChangeText={(value) => this.handleOnChange({precioVenta5: value})}
          />
          <Dropdown
            selectedValue={idImpuesto}
            label='Seleccione el tipo de impuesto'
            items={rentTypes}
            onValueChange={(value, itemPosition) => this.handleOnChange({idImpuesto: value})}
          />
          <TextField
            label='Observaciones del producto'
            value={observacion}
            onChangeText={(value) => this.handleOnChange({observacion: value})}
          />
          <Button
            title="Guardar"
            titleUpperCase
            disabled={!buttonEnabled}
            containerStyle={{marginTop: 20}} 
            onPress={() => this.handleOnPress()}
          />
        </View>
      </ScrollView>
    </View>)
  }

  handleOnChange(value) {
    this.setState(value)
  }

  handleOnPress() {
    const {
      tipo,
      idLinea,
      codigo,
      codigoProveedor,
      idProveedor,
      descripcion,
      precioCosto,
      precioVenta1,
      precioVenta2,
      precioVenta3,
      precioVenta4,
      precioVenta5,
      idImpuesto,
      observacion
    } = this.state
    const product = {
      Tipo: tipo,
      IdLinea: idLinea,
      Codigo: codigo,
      CodigoProveedor: codigoProveedor,
      IdProveedor: idProveedor,
      Descripcion: descripcion,
      PrecioCosto: precioCosto,
      PrecioVenta1: precioVenta1,
      PrecioVenta2: precioVenta2,
      PrecioVenta3: precioVenta3,
      PrecioVenta4: precioVenta4,
      PrecioVenta5: precioVenta5,
      IdImpuesto: idImpuesto,
      Observacion: observacion,
      IndExistencia: 0,
      Imagen: null,
      Activo: true
    }
    if (this.props.product) {
      product.IdProducto = this.props.product.IdProducto
    }
    this.props.saveProductItem(product)
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
    productList: state.product.productList,
    typeList: state.product.typeList,
    categoryList: state.product.categoryList,
    providerList: state.product.providerList,
    rentTypeList: state.product.rentTypeList,
    product: state.product.entity,
    error: state.product.error
  }
}

const mapDispatchToProps = (dispatch) => {
  return bindActionCreators({
    setParameters,
    getProduct,
    setProduct,
    saveProductItem
  }, dispatch)
}

export default connect(mapStateToProps, mapDispatchToProps)(ProductScreen)
