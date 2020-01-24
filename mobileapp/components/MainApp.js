import React, { Component } from 'react'
import { connect } from 'react-redux'
import { bindActionCreators } from 'redux'

import { validateAppState } from '../store/config/actions'
import { logOut } from '../store/session/actions'

import { Dimensions, StyleSheet, StatusBar, View, Text, BackHandler } from 'react-native'
import Modal, { ScaleAnimation, ModalFooter, ModalButton, ModalTitle } from 'react-native-modals'

const { width } = Dimensions.get('window')
const rem = width / 411.42857142857144

import SplashScreen from './custom/SplashScreen'
import LoginNavigator from './login/LoginNavigator'
import HomeNavigator from './home/HomeNavigator'
import Loader from './custom/Loader'

class MainApp extends Component {
  constructor(props) {
    super(props)
    this.state = {
      splashScreenDone: false
    }
  }

  componentDidMount () {
    this.props.validateAppState()
  }

  splashScreenOnCompleted () {
    this.setState({ splashScreenDone: true })
  }

  handleBackPress () {
    BackHandler.exitApp()
  }

  render() {
    const { appReady, company, loaderVisible, authorized, error, logOut } = this.props
    const { splashScreenDone } = this.state
    const rootComponent = (
      !splashScreenDone || !appReady ?
        null :
        authorized ?
          <HomeNavigator company={company} logOut={logOut} /> :
          <LoginNavigator />
    )
    const visibility = splashScreenDone && loaderVisible
    const modalVisible = error !== ''
    return (
      <View style={styles.container}>
        <StatusBar hidden={!splashScreenDone} />
        {!splashScreenDone && <SplashScreen onCompleted={this.splashScreenOnCompleted.bind(this)}/>}
        {rootComponent}
        <Modal
          modalAnimation={new ScaleAnimation()}
          visible={modalVisible}
          modalStyle={styles.modal}
          modalTitle={<ModalTitle title="JLC Solutions CR" />}
          footer={
            <ModalFooter>
              <ModalButton
                bordered
                textStyle={styles.modalButtonText}
                text="Recargar"
                onPress={() => {this.props.validateAppState()}}
              />
              <ModalButton
                bordered
                textStyle={styles.modalButtonText}
                text="Salir"
                onPress={() => {this.handleBackPress()}}
              />
            </ModalFooter>}
        >
          <View style={styles.dialogContentView}>
            <Text>{error}</Text>
          </View>
        </Modal>
        <Loader visible={visibility} />
      </View>
    )
  }
}

const styles = StyleSheet.create({
  container: {
    flex: 1
  },
  modal: {
    marginLeft: 10,
    marginRight: 10
  },
  dialogContentView: {
    padding: 20
  },
  modalButtonText: {
    fontFamily: 'Cochin',
    fontSize: (16 * rem)
  }
})

const mapStateToProps = (state) => {
  return {
    appReady: state.config.appReady,
    error: state.ui.error,
    loaderVisible: state.ui.loaderVisible,
    authorized: state.session.authorized,
    company: state.session.company
  }
}

const mapDispatchToProps = (dispatch) => {
  return bindActionCreators({
    validateAppState,
    logOut
  }, dispatch)
}

export default connect(mapStateToProps, mapDispatchToProps)(MainApp)
