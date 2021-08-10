import React from 'react'
import { StyleSheet, View, Text } from 'react-native'
import { Picker } from  '@react-native-picker/picker'

const Dropdown = (props) => {
  const pickers = props.items.map((item, index) => {
    return <Picker.Item key={index} value={item.value} label={item.label} />
  })
  return (
    <View style={styles.container}>
      {props.label && <Text style={styles.label}>
        {props.label}
      </Text>}
      <Picker
        enabled={props.disabled ? !props.disabled : true}
        selectedValue={props.selectedValue}
        style={styles.item}
        onValueChange={props.onValueChange}
      >
        {pickers}
      </Picker>
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
  item: {
    height: 50,
    padding: 10
  }
})

export default Dropdown
