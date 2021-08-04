import React from 'react'
import { StyleSheet, Text, View } from 'react-native'
import ReactCheckBox from 'react-native-check-box'

const CheckBox = (props) => {
  return (
    <View style={styles.container}>
      <ReactCheckBox style={styles.checkbox}
        disabled={props.disabled ? props.disabled : false}
        isChecked={props.value}
        onClick={() => props.onValueChange()}
      />
      <Text style={styles.label}>
        {props.label}
      </Text>
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    padding: 10,
    flexDirection: 'row'
  },
  label: {
    paddingLeft: 0,
    paddingTop: 5,
    fontSize: 16
  },
  checkbox: {
    paddingTop: 2,
    paddingRight: 10
  }
})

export default CheckBox
