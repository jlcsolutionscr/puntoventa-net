import React from 'react'
import { Dimensions, StyleSheet, View, TouchableOpacity, Text } from 'react-native'


const { width, height } = Dimensions.get('window')
const rem = width / 411.42857142857144
const remY = height / 683.4285714285714

const Button = (props) => {
  const containerStyles = {
    ...styles.container,
    ...props.containerStyle
  }
  const borderColor = props.disabled ?
    '#909596' :
    props.style && props.style.borderColor ?
      props.style.borderColor :
      '#282E2A'
  const backgroundColor = props.disabled ?
    '#909596' :
    props.style && props.style.backgroundColor ?
      props.style.borderColor :
      '#282E2A'
  const elementStyles = {
    ...styles.button,
    ...props.style,
    borderColor,
    backgroundColor
  }
  const label = props.titleUpperCase ? props.title.toUpperCase() : props.title
  return (
    <View style={containerStyles}>
      <TouchableOpacity
        style={elementStyles}
        disabled={props.disabled ? props.disabled : false}
        activeOpacity={0.8}
        onPress={props.onPress}>
        <Text style={styles.text}>{label}</Text>
      </TouchableOpacity>
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    justifyContent: 'center',
    padding: 10
  },
  button: {
    justifyContent: 'center',
    alignItems: 'center',
    borderWidth: 1,
    borderRadius: 4,
    height: (50 * remY)
  },
  text: {
    color: 'white',
    fontFamily: 'Cochin',
    fontSize: (16 * rem)
  }
})

export default Button
