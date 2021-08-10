import React from 'react'
import { StyleSheet, View, TouchableOpacity, Image } from 'react-native'


import iconAdd from 'assets/plus-26-white.png'

const CircleButton = (props) => {
  const { size, primaryColor } = props
  const styles = StyleSheet.create({
    container: {
      flexDirection: 'column',
      alignItems: 'center',
      justifyContent: 'center'
    },
    button: {
      alignItems: 'center',
      justifyContent: 'center',
      width: size,
      height: size,
      borderRadius: 360,
      backgroundColor: props.disabled ? '#909596' : primaryColor
    },
    centerImage: {
      width: size - 5,
      height: size - 5
    }
  })
  return (
    <View style={styles.container}>
      <TouchableOpacity
        onPress={props.onPressButton}
        disabled={props.disabled ? props.disabled : false}
        style={styles.button}>
        <Image source={props.iconButton} style={styles.centerImage} />
      </TouchableOpacity>
    </View>
  )
}

CircleButton.defaultProps = {
  size: 30,
  iconButton: iconAdd,
  primaryColor: '#54AEEC'
}

export default CircleButton
