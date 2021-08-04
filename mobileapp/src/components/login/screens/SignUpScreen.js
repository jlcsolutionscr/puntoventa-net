import React, { Component } from 'react'
import { connect } from 'react-redux'
import { bindActionCreators } from 'redux'
import { withNavigationFocus } from 'react-navigation'

import { setAvailableTerminals, getAvailableTerminals, signUp, setSignUpError } from 'store/config/actions'

import { StyleSheet, ScrollView, View, Text } from 'react-native'
import Button from '../../custom/Button'
import TextField from '../../custom/TextField'
import Dropdown from '../../custom/Dropdown'

export class SignUpScreen extends Component {
  constructor(props) {
    super(props)
    this.state = {
      user: '',
      password: '',
      identifier: '',
      selecteDevice: null
    }
  }

  componentDidUpdate (nextProps) {
    if (nextProps.isFocused !== this.props.isFocused) {
      this.props.setAvailableTerminals([])
      this.setState({
        user: '',
        password: '',
        identifier: '',
        selecteDevice: null
      })
    }
  }

  render () {
    const { availableTerminalList, error } = this.props
    const { user, password, identifier, selecteDevice } = this.state
    const buttonLabel = availableTerminalList.length === 0 ? 'Obtener Terminales Disponibles' : 'Registrar Terminal'
    let buttonEnabled = false
    if (availableTerminalList.length === 0) {
      buttonEnabled = user != '' && password != '' && identifier != ''
    } else {
      buttonEnabled = user != '' && password != '' && identifier != '' && selecteDevice != null
    }
    const terminals = availableTerminalList.map((item, index) => {
      return { value: index, label: item.NombreSucursal }
    })
    return (<View key='2' style={StyleSheet.absoluteFill}>
      <View style={styles.header}>
        <Text style={styles.title}>Configuración</Text>
      </View>
      <ScrollView>
        <View style={styles.content}>
          <TextField
            label='Usuario'
            placeholder='Código de usuario'
            value={user}
            onChangeText={(user) => this.handleOnChange({user})}
          />
          <TextField
            label='Contraseña'
            placeholder='Contraseña'
            secureTextEntry
            value={password}
            onChangeText={(password) => this.handleOnChange({password})}
          />
          <TextField
            label='Identificación'
            placeholder='Identificación de la empresa'
            value={identifier}
            keyboardType='numeric'
            onChangeText={(identifier) => this.handleOnChange({identifier})}
          />
          <Dropdown
            selectedValue={selecteDevice}
            items={terminals}
            resetValue={false}
            onValueChange={(value, itemPosition) => this.handleOnChange({selecteDevice: value})}
          />
          <Button
            title={buttonLabel}
            titleUpperCase
            disabled={!buttonEnabled}
            containerStyle={{marginTop: 20 }}
            onPress={() => this.handleOnPress()}
          />
          {error !== '' && <Text style={{color: 'red', textAlign: 'center'}}>{error}</Text>}
        </View>
      </ScrollView>
    </View>)
  }

  handleOnChange(value) {
    this.props.setSignUpError('')
    this.setState(value)
  }

  async handleOnPress () {
    const { availableTerminalList, signUp, getAvailableTerminals, setSignUpError } = this.props
    const { identifier, user, password, selecteDevice } = this.state
    setSignUpError('')
    if (availableTerminalList.length > 0) {
      const selected = availableTerminalList[selecteDevice]
      signUp(user, password, identifier, selected.IdSucursal, selected.IdTerminal)
    } else {
      getAvailableTerminals(user, password, identifier)
    }
  }
}

const styles = StyleSheet.create({
  header: {
    height: 70,
    backgroundColor: '#08415C',
    justifyContent: 'center',
    alignItems: 'center'
  },
  title: {
    fontFamily: 'Cochin',
    fontSize: 20,
    fontWeight: 'bold',
    color: '#FFFFFF'
  },
  content: {
    backgroundColor: '#FAFAFA',
    padding: 20
  }
})

const mapStateToProps = (state) => {
  return {
    identifierList: state.config.identifierList,
    availableTerminalList: state.config.availableTerminalList,
    error: state.config.signUpError
  }
}

const mapDispatchToProps = (dispatch) => {
  return bindActionCreators({
    setAvailableTerminals,
    getAvailableTerminals,
    signUp,
    setSignUpError
  }, dispatch)
}

export default withNavigationFocus(connect(mapStateToProps, mapDispatchToProps)(SignUpScreen))
