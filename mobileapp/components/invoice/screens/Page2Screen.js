import React, { Component } from 'react'
import { connect } from 'react-redux'
import { bindActionCreators } from 'redux'

import {
  filterProductList,
  getProduct,
  setProductDescription,
  setProductQuantity,
  setProductPrice,
  insertProduct,
  removeProduct
} from '../../../store/invoice/actions'

import { formatCurrency, roundNumber } from '../../../utils/formatHelper'

import { Dimensions, StyleSheet, ScrollView, View, Text } from 'react-native'
import SearchableDropdown from '../../custom/SearchableDropdown'
import TextField from '../../custom/TextField'
import CircleButton from '../../custom/CircleButton'
import removeIcon from '../../../assets/minus-26-white.png'

const { width, height } = Dimensions.get('window')
const rem = width / 411.42857142857144
const remY = height / 683.4285714285714

class Page2Screen extends Component {
  render () {
    const { productList, products, product, productDescription, productQuantity, productPrice, successful } = this.props
    const productOptions = productList.map(item => {
      return { id: item.Id, name: item.Descripcion }
    })
    const rows = products.map((item, index) => {
      return <View key={index} style={[styles.columnsAlign, styles.contentRow]}>
        <Text style={{width: '8%', textAlign: 'left', fontSize: (11 * rem)}}>{item.Cantidad}</Text>
        <Text style={{width: '58%', textAlign: 'left', fontSize: (11 * rem)}}>{item.Descripcion}</Text>
        <Text style={{width: '25%', textAlign: 'right', fontSize: (11 * rem)}}>{formatCurrency(roundNumber(item.Cantidad * item.PrecioVenta, 2), 2)}</Text>
        <View style={{width: '9%', alignItems: 'flex-end'}}><CircleButton size={(15 * rem)} iconButton={removeIcon} primaryColor={'red'} onPressButton={() => this.props.removeProduct(item.IdProducto)} /></View>
      </View>
    })
    let buttonEnabled = false
    if (product != null && productDescription != '' && productQuantity > 0 && productPrice > 0 && successful == false) buttonEnabled = true
    return (<View key='1' style={styles.container}>
      <SearchableDropdown
        label='Seleccione un producto'
        items={productOptions}
        resetValue
        onTextChange={(text) => this.props.filterProductList(text)}
        onItemSelect={(item) => this.props.getProduct(item.id)} />
      <TextField
        label='Descripción'
        value={productDescription}
        onChangeText={(value) => this.props.setProductDescription(value)}
      />
      <View style={styles.columnsAlign}>
        <TextField
          containerStyle={{width: '25%'}}
          label='Cantidad'
          currencyFormat
          keyboardType='numeric'
          value={productQuantity}
          onChangeText={(value) => this.props.setProductQuantity(value)}
        />
        <TextField
          containerStyle={{width: '50%'}}
          label='Precio'
          currencyFormat
          keyboardType='numeric'
          value={productPrice}
          onChangeText={(value) => this.props.setProductPrice(value)}
        />
        <View style={{paddingRight: 40, paddingTop: 30, width: '25%'}}>
          <CircleButton
            disabled={!buttonEnabled}
            onPressButton={() => this.props.insertProduct()}
          />
        </View>
      </View>
      <View style={styles.table}>
        <View style={styles.sectionheader}>
          <Text
            style={styles.sectionheaderText}>
            LINEAS DE FACTURA
          </Text>
        </View>
        <ScrollView style={styles.content}>
          {rows}
        </ScrollView>
      </View>
    </View>)
  }
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#FFFFFF',
    padding: 10
  },
  table: {
    flexDirection: 'column',
    padding: 0
  },
  sectionheader: {
    backgroundColor: '#08415C',
    borderTopLeftRadius: 2,
    borderTopRightRadius: 2,
    borderColor: '#08415C',
    borderTopWidth: (1 * remY),
    borderLeftWidth: (1 * remY),
    borderRightWidth: (1 * remY),
    paddingTop: 5,
    paddingBottom: 5,
    marginTop: 10,
    alignItems: 'center'
  },
  content: {
    minHeight: (200 * remY),
    height: (200 * remY),
    borderColor: '#08415C',
    borderLeftWidth: 1,
    borderRightWidth: 1,
    borderBottomLeftRadius: 2,
    borderBottomRightRadius: 2,
    borderBottomWidth: 1
  },
  sectionheaderText: {
    color: 'white',
    fontSize: (13 * rem)
  },
  columnsAlign: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    alignItems: 'center'
  },
  contentRow: {
    padding: 10
  }
})

const mapStateToProps = (state) => {
  return {
    productList: state.product.productList,
    products: state.invoice.products,
    product: state.invoice.product,
    productDescription: state.invoice.productDescription,
    productQuantity: state.invoice.productQuantity,
    productPrice: state.invoice.productPrice,
    successful: state.invoice.successful
  }
}

const mapDispatchToProps = (dispatch) => {
  return bindActionCreators({
    filterProductList,
    getProduct,
    setProductDescription,
    setProductQuantity,
    setProductPrice,
    insertProduct,
    removeProduct
  }, dispatch)
}

export default connect(mapStateToProps, mapDispatchToProps)(Page2Screen)
