import React, { Component } from 'react'
import { Dimensions, StyleSheet, View, Text } from 'react-native'
import SearchableDropdown from 'react-native-searchable-dropdown'

const { width, height } = Dimensions.get('window')
const rem = width / 411.42857142857144
const remY = height / 683.4285714285714

class Dropdown extends Component {
  constructor(props) {
    super(props)
    this.state = {focus: false}
  }

  render () {
    const textInputProps = {
      underlineColorAndroid: "transparent",
      height: (45 * remY),
      style: styles.textInput
    }
    if (this.props.selectedItemId != null) {
      const item = this.props.items.find(element => element.id === this.props.selectedItemId)
      if (item && !this.state.focus) textInputProps.value = item.name
    }
    return (
      <View style={styles.container}>
        <Text style={styles.label}>
          {this.props.label}
        </Text>
        <SearchableDropdown
          disabled={this.props.disabled ? this.props.disabled : false}
          onFocus={() => this.setState({focus: true})}
          onBlur={() => this.setState({focus: false})}
          onItemSelect={this.props.onItemSelect}
          containerStyle={{ padding: 0 }}
          itemStyle={styles.item}
          itemTextStyle={styles.itemText}
          itemsContainerStyle={styles.itemsContainer}
          items={this.props.items}
          resetValue={this.props.resetValue}
          textInputProps={textInputProps}
          listProps={{nestedScrollEnabled: true}}
        />
      </View>
    )
  }
}

Dropdown.defaultProps = {
  defaultIndex: 0
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
  item: {
    padding: 10,
    marginTop: 2,
    backgroundColor: '#ddd',
    borderColor: '#bbb',
    borderWidth: 1,
    borderRadius: 4
  },
  itemText: {
    color: '#222',
    fontFamily: 'Cochin',
    fontSize: (16 * rem)
  },
  itemsContainer: {
    maxHeight: ((45 * remY) * 3) + 4
  },
  textInput: {
    padding: 10,
    borderWidth: 1,
    borderColor: '#CCC',
    borderRadius: 4,
    fontFamily: 'Cochin',
    fontSize: (16 * rem)
  }
})

export default Dropdown
