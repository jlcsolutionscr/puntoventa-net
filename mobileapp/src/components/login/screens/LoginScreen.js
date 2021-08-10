import React, { Component } from 'react'
import { connect } from 'react-redux'
import { bindActionCreators } from 'redux'
import { withNavigationFocus } from 'react-navigation'

import { logIn, setLogInError } from 'store/session/actions'

import { StyleSheet, ScrollView, View, Text } from 'react-native'
import Button from '../../custom/Button'
import TextField from '../../custom/TextField'
import Dropdown from '../../custom/Dropdown'

class LoginScreen extends Component {
  constructor(props) {
    super(props)
    this.state = {
      user: '',
      password: '',
      companyId: this.props.identifierList.length ? this.props.identifierList[0].Id : ''
    }
  }

  componentDidUpdate (nextProps) {
    if (nextProps.isFocused !== this.props.isFocused) {
      this.setState({
        user: '',
        password: ''
      })
    }
  }

  render () {
    const { identifierList, error } = this.props
    const { user, password, companyId } = this.state
    const buttonEnabled = user != '' && password != '' && companyId != ''
    const identifiers = identifierList.map(item => {
      return { value: item.Id, label: item.Descripcion }
    })
    return (<View key='1' style={StyleSheet.absoluteFill}>
      <View style={styles.header}>
        <Text style={styles.title}>Autenticación</Text>
      </View>
      <ScrollView>
        <View style={styles.content}>
          <TextField
            label='Usuario'
            placeholder='Código de usuario'
            spellCheck={false}
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
          <Dropdown
            selectedValue={companyId}
            items={identifiers}
            resetValue={false}
            onValueChange={(value, itemPosition) => this.handleOnChange({companyId: value})}
          />
          <Button
            title="Ingresar"
            titleUpperCase
            disabled={!buttonEnabled}
            containerStyle={{marginTop: 20}} 
            onPress={() => this.handleOnPress()}
          />
          {error !== '' && <Text style={{color: 'red', textAlign: 'center'}}>{error}</Text>}
        </View>
      </ScrollView>
    </View>)
  }

  handleOnChange(value) {
    this.props.setLogInError('')
    this.setState(value)
  }

  async handleOnPress () {
    this.props.setLogInError('')
    this.props.logIn(this.state.user, this.state.password, this.state.companyId)
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
    error: state.session.logInError
  }
}

const mapDispatchToProps = (dispatch) => {
  return bindActionCreators({
    logIn,
    setLogInError
  }, dispatch)
}

export default withNavigationFocus(connect(mapStateToProps, mapDispatchToProps)(LoginScreen))
