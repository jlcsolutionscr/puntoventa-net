import React from 'react'
import { StyleSheet, Text, View } from 'react-native'

import { formatCurrency, roundNumber } from 'utils/formatHelper'

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
    fontSize: 16
  },
  input: {
    fontFamily: 'Cochin',
    fontSize: 16,
    borderColor: 'grey',
    borderWidth: 1,
    borderRadius: 4,
    padding: 10,
    height: 45
  }
})

export default TextLabel
