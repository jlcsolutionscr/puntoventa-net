import React, { Component } from 'react'
import { connect } from 'react-redux'
import { bindActionCreators } from 'redux'

import { validateAppState } from 'store/config/actions'
import { logOut } from 'store/session/actions'

import { StyleSheet, StatusBar, SafeAreaView, View, Text, BackHandler } from 'react-native'
import Modal, { ScaleAnimation, ModalFooter, ModalButton, ModalTitle } from 'react-native-modals'

import SplashScreen from 'components/custom/SplashScreen'
import OutdatedScreen from 'components/outdated/OutdatedScreen'
import LoginNavigator from 'components/login/LoginNavigator'
import HomeNavigator from 'components/home/HomeNavigator'
import Loader from 'components/custom/Loader'

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
    let requiredConfig = false
    if (company && !company.RegimenSimplificado) {
      requiredConfig = company.UsuarioHacienda === "" ||
      company.ClaveHacienda === "" ||
      company.NombreCertificado === "" ||
      company.PinCertificado === ""
    }
    const rootComponent = (
      !splashScreenDone || appReady === 'pending' ?
        null :
        appReady === 'outdated' ?
          <OutdatedScreen messageId={1} handleBackPress={this.handleBackPress} /> :
          !authorized ?
            <LoginNavigator /> :
            !requiredConfig ?
              <HomeNavigator company={company} logOut={logOut} /> :
              <OutdatedScreen messageId={2} handleBackPress={logOut} />
    )
    const visibility = splashScreenDone && loaderVisible
    const modalVisible = error !== ''
    return (
      <SafeAreaView style={StyleSheet.absoluteFill}>
        <StatusBar currentHeight={40} hidden={!splashScreenDone} />
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
      </SafeAreaView>
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
    fontSize: 16
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
