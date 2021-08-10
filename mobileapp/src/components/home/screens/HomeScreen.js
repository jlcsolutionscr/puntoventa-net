import React from 'react'
import { withNavigationFocus } from 'react-navigation'

import { StyleSheet, View } from 'react-native'
import Button from '../../custom/Button'

const HomeScreen = (props) => {
  let generaFacturaDisabled = true
  let actualizaClienteDisabled = true
  let actualizaProductoDisabled = true
  let listadoDocumentosDisabled = true
  let configuracionDisabled = props.company.RegimenSimplificado
  props.company.Usuario.RolePorUsuario.forEach(item => {
    if (item.Role.MenuItem == 'MnuCapturaFactura') generaFacturaDisabled = false
    if (item.Role.MenuItem == 'MnuMantCliente') actualizaClienteDisabled = false
    if (item.Role.MenuItem == 'MnuMantProducto') actualizaProductoDisabled = false
    if (item.Role.MenuItem == 'MnuDocElectRDE') listadoDocumentosDisabled = false
  })
  return (
    <View style={styles.content}>
      <Button
        containerStyle={styles.buttonContainer}
        style={styles.button}
        titleUpperCase
        disabled={generaFacturaDisabled}
        title='Generar factura'
        onPress={() => props.navigation.navigate('NuevaFactura')}
      />
      <Button
        containerStyle={styles.buttonContainer}
        style={styles.button}
        titleUpperCase
        disabled={generaFacturaDisabled}
        title='Facturas emitidas'
        onPress={() => props.navigation.navigate('Facturas')}
      />
      <Button 
        containerStyle={styles.buttonContainer}
        style={styles.button}
        titleUpperCase
        disabled={listadoDocumentosDisabled}
        title='Gestión Documentos electrónicos'
        onPress={() => props.navigation.navigate('Documentos')}
      />
      <Button
        containerStyle={styles.buttonContainer}
        style={styles.button}
        titleUpperCase
        disabled={actualizaClienteDisabled}
        title='Actualizar clientes'
        onPress={() => props.navigation.navigate('Cliente')}
      />
      <Button
        containerStyle={styles.buttonContainer}
        style={styles.button}
        titleUpperCase
        disabled={actualizaProductoDisabled}
        title='Actualizar productos'
        onPress={() => props.navigation.navigate('Producto')}
      />
      <Button
        containerStyle={{...styles.buttonContainer}}
        style={styles.button}
        titleUpperCase
        title='Generar reportes'
        onPress={() => props.navigation.navigate('Reporte')}
      />
      <Button
        containerStyle={{...styles.buttonContainer, marginBottom: 0}}
        style={styles.button}
        titleUpperCase
        disabled={configuracionDisabled}
        title='Configurar parámetros'
        onPress={() => props.navigation.navigate('Configuracion')}
      />
    </View>
  )
}

const styles = StyleSheet.create({
  content: {
    backgroundColor: '#FFFFFF',
    padding: 10
  },
  buttonContainer: {
    padding: 0,
    marginBottom: 2
  },
  button: {
    backgroundColor: '#08415C',
    borderColor: '#08415C',
    borderRadius: 2,
    height: 60
  }
})

export default withNavigationFocus(HomeScreen)
