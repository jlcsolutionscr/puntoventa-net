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
} from 'store/invoice/actions'

import { formatCurrency, roundNumber } from 'utils/formatHelper'

import { PixelRatio, StyleSheet, ScrollView, View, Text } from 'react-native'
import SearchableDropdown from '../../custom/SearchableDropdown'
import TextField from '../../custom/TextField'
import CircleButton from '../../custom/CircleButton'
import removeIcon from 'assets/minus-26-white.png'

class Page2Screen extends Component {
  render () {
    const { productList, products, product, productDescription, productQuantity, productPrice, successful } = this.props
    const productOptions = productList.map(item => {
      return { id: item.Id, name: item.Descripcion }
    })
    const onProductSelected = (item) => {
      this.props.getProduct(item.id)
    }
    const rows = products.map((item, index) => {
      return <View key={index} style={[styles.columnsAlign, styles.contentRow]}>
        <Text style={{width: '8%', textAlign: 'left', fontSize: 11}}>{item.Cantidad}</Text>
        <Text style={{width: '58%', textAlign: 'left', fontSize: 11}}>{item.Descripcion}</Text>
        <Text style={{width: '25%', textAlign: 'right', fontSize: 11}}>{formatCurrency(roundNumber(item.Cantidad * item.PrecioVenta, 2), 2)}</Text>
        <View style={{width: '9%', alignItems: 'flex-end'}}><CircleButton size={20} iconButton={removeIcon} primaryColor={'red'} onPressButton={() => this.props.removeProduct(item.IdProducto)} /></View>
      </View>
    })
    let buttonEnabled = product != null && productDescription != '' && productQuantity > 0 && productPrice > 0 && successful == false
    return (<View key='1' style={styles.container}>
      <View>
        <SearchableDropdown
          label='Seleccione un producto'
          items={productOptions}
          resetValue
          onTextChange={(text) => this.props.filterProductList(text)}
          onItemSelect={(item) => onProductSelected(item)} />
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
    flex: 1
  },
  table: {
    flex: 1
  },
  content: {
    flex: 1,
  },
  sectionheaderText: {
    marginLeft: 15,
    color: 'black',
    fontSize: 13
  },
  columnsAlign: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    alignItems: 'center'
  },
  contentRow: {
    padding: 5
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
