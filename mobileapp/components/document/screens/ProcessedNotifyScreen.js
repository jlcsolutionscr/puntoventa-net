import React, { Component } from 'react'
import { connect } from 'react-redux'
import { bindActionCreators } from 'redux'

import {
  setProcessedEmail,
  sendNotification
} from '../../../store/document/actions'

import { Dimensions, StyleSheet, View, ScrollView, Text } from 'react-native'
import Button from '../../custom/Button'
import TextField from '../../custom/TextField'

const { width } = Dimensions.get('window')
const rem = width / 411.42857142857144

class ProcessedNotifyScreen extends Component {
  render () {
    const { message, error, processedSelected } = this.props
    return (<View key='1' style={styles.container}>
      {message !== '' && <Text style={{color: 'green', textAlign: 'center'}}>{message}</Text>}
      {error !== '' && <Text style={{color: 'red', textAlign: 'center'}}>{error}</Text>}
      <ScrollView keyboardShouldPersistTaps='handled'>
        <View key='2' style={styles.content}>
          <TextField
            label='Correo para notificación'
            placeholder='xxx@yyyy.zzz'
            value={processedSelected ? processedSelected.CorreoNotificacion : ''}
            onChangeText={(correo) => this.props.setProcessedEmail(correo)}
          />
          <Button
            title="Enviar notificacion"
            titleUpperCase
            disabled={processedSelected == null || processedSelected.CorreoNotificacion == ''}
            onPress={() => this.props.sendNotification(processedSelected.IdDocumento, processedSelected.CorreoNotificacion)}
          />
        </View>
      </ScrollView>
    </View>)
  }
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 10
  },
  label: {
    paddingLeft: 20,
    fontSize: (16 * rem)
  },
  text: {
    fontFamily: 'Cochin',
    fontSize: (16 * rem),
    borderColor: 'grey',
    borderWidth: 1,
    borderRadius: 4,
    padding: 10,
    margin: 10
  }
})

const mapStateToProps = (state) => {
  return {
    processedSelected: state.document.processedSelected,
    message: state.document.processedMessage,
    error: state.document.processedError
  }
}

const mapDispatchToProps = (dispatch) => {
  return bindActionCreators({
    setProcessedEmail,
    sendNotification
  }, dispatch)
}

export default connect(mapStateToProps, mapDispatchToProps)(ProcessedNotifyScreen)
