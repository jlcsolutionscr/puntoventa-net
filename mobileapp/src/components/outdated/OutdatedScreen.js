import React from 'react'

import { StyleSheet, View, Text, Linking } from 'react-native'
import Button from '../custom/Button'

const OutdatedScreen = (props) => {
  return (
    <View style={styles.content}>
      <Text style={styles.text}>
        {props.messageId === 1
          ? 'Existe una actualización pendiente. Ingrese al Google Play Store para continuar'
          : 'La información de la empresa se encuentra incompleta. Por favor verifique su configuración'}
      </Text>
      <Button
        containerStyle={styles.buttonContainer}
        style={styles.button}
        titleUpperCase
        title={props.messageId === 1 ? 'Cerrar App' : 'Regresar'}
        onPress={() => props.handleBackPress()}
      />
      {props.messageId === 1 && <Button
        containerStyle={styles.buttonContainer}
        style={styles.button}
        titleUpperCase
        title='Ingresar a Play Store'
        onPress={() => Linking.openURL('market://details?id=com.jlcfacturacion')}
      />}
    </View>
  )
}

const styles = StyleSheet.create({
  content: {
    flex: 1, 
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#08415C'
  },
  text: {
    color: 'white',
    textAlign: 'center',
    fontSize: 20,
    marginBottom: 20
  },
  buttonContainer: {
    padding: 0,
    marginBottom: 10
  },
  button: {
    backgroundColor: '#909596',
    borderColor: '#909596',
    borderRadius: 2,
    paddingLeft: 60,
    paddingRight: 60,
    height: 40
  }
})

export default OutdatedScreen
