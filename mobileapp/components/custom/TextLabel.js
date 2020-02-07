import React from 'react'
import { Dimensions, StyleSheet, Text, View } from 'react-native'

const { width, height } = Dimensions.get('window')
const rem = width / 411.42857142857144
const remY = height / 683.4285714285714

import { formatCurrency, roundNumber } from '../../utils/formatHelper'

const TextLabel = (props) => {
  const containerStyle = {...styles.container, ...props.containerStyle}
  const displayText = props.value.toString() != ''
      ? props.currencyFormat
        ? formatCurrency(roundNumber(props.value, 2), 2)
        : props.value.toString()
    : ''
  return (
    <View style={containerStyle}>
      <Text style={styles.label}>
        {props.label}
      </Text>
      <Text style={styles.input}>
        {displayText}
      </Text>
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    padding: 10,
    paddingBottom: 5
  },
  label: {
    paddingBottom: 10,
    fontSize: (16 * rem)
  },
  input: {
    fontFamily: 'Cochin',
    fontSize: (16 * rem),
    borderColor: 'grey',
    borderWidth: 1,
    borderRadius: 4,
    padding: 10,
    height: (45 * remY)
  }
})

export default TextLabel
