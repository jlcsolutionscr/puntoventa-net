import React, { Component } from 'react'
import { StyleSheet, View, Text, Button } from 'react-native'
import DateTimePicker from '@react-native-community/datetimepicker'

class DatePicker extends Component {
  constructor(props) {
    super(props)
    this.state = {show: false}
    this.handleOnChange = this.handleOnChange.bind(this)
  }

  render () {
    let currentDate = new Date()
    if (this.props.value != '') {
      const parts = this.props.value.split('/')
      currentDate = new Date(parts[2], parts[1] - 1, parts[0])
    }
    return (
      <View style={styles.container}>
        <Text style={styles.label}>
          {this.props.label}
        </Text>
        <Button
          disabled={this.props.disabled ? this.props.disabled : false}
          title={this.props.value}
          onPress={() => this.setState({show: true})}/>
        {this.state.show && <DateTimePicker
          mode='date'
          value={currentDate}
          is24Hour={true}
          display="default"
          onChange={this.handleOnChange} />
        }
      </View>
    )
  }

  handleOnChange (event, date) {
    this.setState({show: false})
    if (event.type != 'dismissed') {
      const dayFormatted = (date.getDate() < 10 ? '0' : '') + date.getDate()
      const monthFormatted = (date.getMonth() + 1 < 10 ? '0' : '') + (date.getMonth() + 1)
      const dateText = `${dayFormatted}/${monthFormatted}/${date.getFullYear()}`
      this.props.onChange(dateText)
    }
  }
}

const styles = StyleSheet.create({
    container: {
      padding: 10,
      paddingBottom: 5
    },
    label: {
      paddingBottom: 10,
      fontSize: 16
    }
  })

export default DatePicker
