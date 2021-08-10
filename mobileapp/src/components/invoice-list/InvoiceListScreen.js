import React, { Component } from 'react'
import { connect } from 'react-redux'
import { bindActionCreators } from 'redux'

import {
  setList,
  getListFirstPage,
  getListByPageNumber,
  revokeInvoice
} from 'store/invoice/actions'

import { formatCurrency } from 'utils/formatHelper'

import { Dimensions, StyleSheet, View, Text, ScrollView, Alert } from 'react-native'
import CircleButton from '../custom/CircleButton'
import IconButton from '../custom/IconButton'
import RemoveIcon from 'assets/minus-26-white.png'
import FirstIcon from 'assets/first.png'
import FirstDisabledIcon from 'assets/first-disabled.png'
import PreviousIcon from 'assets/previous.png'
import PreviousDisabledIcon from 'assets/previous-disabled.png'
import NextIcon from 'assets/next.png'
import NextDisabledIcon from 'assets/next-disabled.png'
import LastIcon from 'assets/last.png'
import LastDisabledIcon from 'assets/last-disabled.png'

const { height } = Dimensions.get('window')

class InvoiceListScreen extends Component {
  constructor (props) {
    super(props)
  }

  componentDidMount () {
    this.props.getListFirstPage()
  }

  componentWillUnmount () {
    this.props.setList([])
  }

  render () {
    const { list, listPage, listCount } = this.props
    const previousDisabled = listPage === 1
    const nextDisabled = listPage * 10 > listCount
    const lastPage = Math.ceil(listCount / 10)
    const rows = list.map((item, index) => {
      return <View key={index} style={[styles.columnsAlign, styles.contentRow]}>
        <View style={[styles.columnsAlign, {width: '93%'}]}>
          <Text style={{width: '10%', textAlign: 'left', fontSize: 11}}>{item.IdFactura}</Text>
          <Text style={{width: '25%', textAlign: 'center', fontSize: 11}}>{item.Fecha}</Text>
          <Text style={{width: '65%', textAlign: 'left', fontSize: 11}}>{item.NombreCliente}</Text>
          <Text style={{width: '20%', textAlign: 'right', fontSize: 11}}>{'IMPUESTO:'}</Text>
          <Text style={{width: '25%', textAlign: 'right', fontSize: 11}}>{formatCurrency(item.Impuesto)}</Text>
          <Text style={{width: '20%', textAlign: 'right', fontSize: 11}}>{'TOTAL:'}</Text>
          <Text style={{width: '25%', textAlign: 'right', fontSize: 11}}>{formatCurrency(item.Total)}</Text>
        </View>
        <View style={{width: '7%'}}><CircleButton size={20} disabled={item.Estado == 'Anulando'} iconButton={RemoveIcon} primaryColor={'red'} onPressButton={() => this.handleOnPress(item.IdFactura)}/></View>
      </View>
    })
    return (<View key='1' style={styles.container}>
      <View style={styles.table}>
        <ScrollView style={styles.content}>
          {rows}
        </ScrollView>
        <View style={[styles.columnsAlign, {justifyContent: 'center'}]}>
          <View style={{margin: 10}}>
            <IconButton
              size={(29)}
              iconSize={(15)}
              primaryColor='#CCCCC9'
              disabledColor={'#F5F5F5'}
              disabled={previousDisabled}
              iconButton={previousDisabled ? FirstDisabledIcon : FirstIcon}
              onPressButton={() => this.handleFirstPage()}
            />
          </View>
          <View style={{margin: 10}}>
            <IconButton
              size={(29)}
              iconSize={(15)}
              primaryColor='#CCCCC9'
              disabledColor={'#F5F5F5'}
              disabled={previousDisabled}
              iconButton={previousDisabled ? PreviousDisabledIcon : PreviousIcon}
              onPressButton={() => this.handlePreviousPage()}
            />
          </View>
          <Text style={{width: '30%', textAlign: 'center', fontSize: 12}}>{`Página ${listPage} de ${lastPage}`}</Text>
          <View style={{margin: 10}}>
            <IconButton
              size={(29)}
              iconSize={(15)}
              primaryColor='#CCCCC9'
              disabledColor={'#F5F5F5'}
              disabled={nextDisabled}
              iconButton={nextDisabled ? NextDisabledIcon : NextIcon}
              onPressButton={() => this.handleNextPage()}
            />
          </View>
          <View style={{margin: 10}}>
            <IconButton
              size={(29)}
              iconSize={(15)}
              primaryColor='#CCCCC9'
              disabledColor={'#F5F5F5'}
              disabled={nextDisabled}
              iconButton={nextDisabled ? LastDisabledIcon : LastIcon}
              onPressButton={() => this.handleLastPage()}
            />
          </View>
        </View>
      </View>
    </View>)
  }

  handleFirstPage() {
    this.props.getListFirstPage()
  }

  handlePreviousPage() {
    this.props.getListByPageNumber(this.props.listPage - 1)
  }

  handleNextPage() {
    this.props.getListByPageNumber(this.props.listPage + 1)
  }

  handleLastPage() {
    const { listCount, getListByPageNumber } = this.props
    const lastPage = Math.ceil(listCount / 10)
    getListByPageNumber(lastPage)
  }

  handleOnPress (id) {
    Alert.alert(
      'JLC Solutions',
      'Desea anular la factura?',
      [
        {text: 'NO'},
        {text: 'SI', onPress: () => this.props.revokeInvoice(id)}
      ]
    )
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
    marginTop: 10,
    borderTopWidth: 0.5
  },
  content: {
    height: height - 160,
    borderColor: '#08415C'
  },
  columnsAlign: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    alignItems: 'center'
  },
  contentRow: {
    borderBottomWidth: 0.5,
    padding: 7
  }
})

const mapStateToProps = (state) => {
  return {
    list: state.invoice.list,
    listPage: state.invoice.listPage,
    listCount: state.invoice.listCount,
    error: state.invoice.processedError
  }
}

const mapDispatchToProps = (dispatch) => {
  return bindActionCreators({
    setList,
    getListFirstPage,
    getListByPageNumber,
    revokeInvoice
  }, dispatch)
}

export default connect(mapStateToProps, mapDispatchToProps)(InvoiceListScreen)