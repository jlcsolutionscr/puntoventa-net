import React, { Component } from 'react'
import { connect } from 'react-redux'
import { bindActionCreators } from 'redux'

import { saveInvoice, setParameters, resetInvoice } from '../../../store/invoice/actions'

import { formatCurrency } from '../../../utils/formatHelper'

import { Dimensions, StyleSheet, View, Text, Animated } from 'react-native'
import Button from '../../custom/Button'
import SuccessfulIcon from '../../../assets/checked.png'

const { width, height } = Dimensions.get('window')
const rem = width / 411.42857142857144
const remY = height / 683.4285714285714
  
class Page3Screen extends Component {
  constructor () {
    super()
    this.springValue = new Animated.Value(0.3)
  }

  componentDidUpdate (prevProps) {
    if (this.props.successful && !prevProps.successful) {
      this.springValue.setValue(0.3)
      Animated.spring(
        this.springValue,
        {
          toValue: 1,
          friction: 3
        }
      ).start()
    }

  }

  render () {
    const { gravado, exonerado, excento, subTotal, impuesto, total, successful, error } = this.props
    const buttonDisabled = total == 0 || error != ''
    return (<View key='1' style={styles.container}>
      {error !== '' && <Text style={{color: 'red', textAlign: 'center'}}>{error}</Text>}
      <Text style={styles.title}>{'RESUMEN DE FACTURA'}</Text>
      <View style={styles.columnsAlign}>
        <Text style={styles.columnLeft}>{'Gravado'}</Text>
        <Text style={styles.columnRight}>{formatCurrency(gravado)}</Text>
        <Text style={styles.columnLeft}>{'Exonerado'}</Text>
        <Text style={styles.columnRight}>{formatCurrency(exonerado)}</Text>
        <Text style={styles.columnLeft}>{'Excento'}</Text>
        <Text style={styles.columnRight}>{formatCurrency(excento)}</Text>
        <Text style={styles.columnLeft}>{'SubTotal'}</Text>
        <Text style={styles.columnRight}>{formatCurrency(subTotal)}</Text>
        <Text style={styles.columnLeft}>{'Impuesto'}</Text>
        <Text style={styles.columnRight}>{formatCurrency(impuesto)}</Text>
        <Text style={styles.columnLeft}>{'Total'}</Text>
        <Text style={styles.columnRight}>{formatCurrency(total)}</Text>
      </View>
      <Button
        title={successful ? 'Nueva factura': 'Generar'}
        titleUpperCase
        disabled={buttonDisabled}
        onPress={() => this.handleOnPress()}
      />
      {successful && <View style={styles.success}><Animated.Image style={{transform: [{scale: this.springValue}]}} source={SuccessfulIcon} /></View>}
    </View>)
  }

  handleOnPress () {
    if (!this.props.successful) {
      this.props.saveInvoice()
    } else {
      this.props.setParameters()
      this.props.resetInvoice()
      this.props.navigation.navigate('CLIENTE')
    }
  }
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#FFFFFF',
    padding: 10
  },
  title: {
    fontSize: (16 * rem),
    textAlign: 'center',
    paddingTop: 50
  },
  columnsAlign: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    alignItems: 'center',
    paddingTop: 10,
    paddingBottom: 10
  },
  columnLeft: {
    fontSize: (16 * rem),
    width: '40%',
    paddingLeft: (60 * rem),
    textAlign: 'left'
  },
  columnRight: {
    fontSize: (16 * rem),
    width: '60%',
    paddingRight: (60 * rem),
    textAlign: 'right'
  },
  success: {
    alignItems: 'center',
    justifyContent: 'center',
    height: (160 * remY)
  }
})

const mapStateToProps = (state) => {
  return {
    gravado: state.invoice.gravado,
    exonerado: state.invoice.exonerado,
    excento: state.invoice.excento,
    subTotal: state.invoice.subTotal,
    impuesto: state.invoice.impuesto,
    total: state.invoice.total,
    successful: state.invoice.successful,
    error: state.invoice.error
  }
}

const mapDispatchToProps = (dispatch) => {
  return bindActionCreators({
    saveInvoice,
    setParameters,
    resetInvoice
  }, dispatch)
}

export default connect(mapStateToProps, mapDispatchToProps)(Page3Screen)
