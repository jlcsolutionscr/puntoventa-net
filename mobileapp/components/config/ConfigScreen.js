import React, { Component } from 'react'
import { connect } from 'react-redux'
import { bindActionCreators } from 'redux'

import {
  setTerminal,
  getConfig,
  saveConfig
} from '../../store/config/actions'

import { StyleSheet, View, ScrollView, Text } from 'react-native'

import TextField from '../custom/TextField'
import Button from '../custom/Button'
  
class ConfigScreen extends Component {
  constructor (props) {
    super(props)
    this.state = {
      UltimoDocFE: 0,
      UltimoDocNC: 0,
      UltimoDocND: 0,
      UltimoDocTE: 0,
      UltimoDocMR: 0,
      UltimoDocFEC: 0
    }
  }

  componentDidMount () {
    this.props.getConfig()
  }

  componentWillUnmount () {
    if (this.props.terminal != null) this.props.setTerminal(null)
  }

  componentDidUpdate (prevProps) {
    const { terminal } = this.props
    if (terminal != null && prevProps.terminal == null) {
      this.setState({
        UltimoDocFE: terminal.UltimoDocFE,
        UltimoDocNC: terminal.UltimoDocNC,
        UltimoDocND: terminal.UltimoDocND,
        UltimoDocTE: terminal.UltimoDocTE,
        UltimoDocMR: terminal.UltimoDocMR,
        UltimoDocFEC: terminal.UltimoDocFEC
      })
    }
  }

  render () {
    const {
      error
    } = this.props
    const {
      UltimoDocFE,
      UltimoDocNC,
      UltimoDocND,
      UltimoDocTE,
      UltimoDocMR,
      UltimoDocFEC
    } = this.state
    const buttonEnabled = UltimoDocFE != null && UltimoDocNC != null && UltimoDocND != null &&
      UltimoDocTE != null && UltimoDocMR != null && UltimoDocFEC != null 
    return (<View key='1' style={styles.container}>
      {error !== '' && <Text style={{color: 'red', textAlign: 'center'}}>{error}</Text>}
      <ScrollView keyboardShouldPersistTaps='handled'>
        <View key='2'>
          <TextField
            label='Ultima Factura Electrónica'
            keyboardType='numeric'
            value={UltimoDocFE}
            onChangeText={(UltimoDocFE) => this.setState({UltimoDocFE})}
          />
          <TextField
            label='Ultima Nota de Crédito'
            keyboardType='numeric'
            value={UltimoDocNC}
            onChangeText={(UltimoDocNC) => this.setState({UltimoDocNC})}
          />
          <TextField
            label='Ultima Nota de Débito'
            keyboardType='numeric'
            value={UltimoDocND}
            onChangeText={(UltimoDocND) => this.setState({UltimoDocND})}
          />
          <TextField
            label='Ultimo Tiquete Electrónico'
            keyboardType='numeric'
            value={UltimoDocTE}
            onChangeText={(UltimoDocTE) => this.setState({UltimoDocTE})}
          />
          <TextField
            label='Ultimo Mensaje Receptor'
            keyboardType='numeric'
            value={UltimoDocMR}
            onChangeText={(UltimoDocMR) => this.setState({UltimoDocMR})}
          />
          <TextField
            label='Ultima Factura de Compra'
            keyboardType='numeric'
            value={UltimoDocFEC}
            onChangeText={(UltimoDocFEC) => this.setState({UltimoDocFEC})}
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

  handleOnPress() {
    const { UltimoDocFE,
      UltimoDocNC,
      UltimoDocND,
      UltimoDocTE,
      UltimoDocMR,
      UltimoDocFEC
    } = this.state
    const newTerminal = {
      ...this.props.terminal,
      UltimoDocFE,
      UltimoDocNC,
      UltimoDocND,
      UltimoDocTE,
      UltimoDocMR,
      UltimoDocFEC
    }
    this.props.saveConfig(newTerminal)
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
    terminal: state.config.terminal,
    error: state.config.error
  }
}

const mapDispatchToProps = (dispatch) => {
  return bindActionCreators({
    setTerminal,
    getConfig,
    saveConfig
  }, dispatch)
}

export default connect(mapStateToProps, mapDispatchToProps)(ConfigScreen)
