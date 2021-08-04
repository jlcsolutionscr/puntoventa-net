import React from 'react'
import { View, TouchableOpacity, StyleSheet, Image } from 'react-native'

import iconAdd from 'assets/plus-26-white.png'

const IconButton = (props) => {
  const { size, iconSize, primaryColor, disabledColor } = props
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
      borderRadius: 2,
      backgroundColor: props.disabled ? disabledColor : primaryColor
    },
    centerImage: {
      width: iconSize,
      height: iconSize
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

IconButton.defaultProps = {
  size: 30,
  iconSize: 25,
  iconButton: iconAdd,
  primaryColor: 'transparent',
  disabledColor: '#909596'
}

export default IconButton
