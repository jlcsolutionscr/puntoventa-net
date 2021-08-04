import React, { Component } from 'react'
import { StyleSheet, View, Text } from 'react-native'
import SearchableDropdown from 'react-native-searchable-dropdown'

class Dropdown extends Component {
  constructor(props) {
    super(props)
    this.state = {focus: false}
  }

  render () {
    const textInputProps = {
      underlineColorAndroid: "transparent",
      height: 45,
      style: styles.textInput
    }
    console.log('selectedItemId', this.props.selectedItemId)
    console.log('resetValue', this.props.resetValue)
    if (this.props.selectedItemId != null && !this.props.resetValue) {
      const item = this.props.items.find(element => element.id === this.props.selectedItemId)
      console.log('item', item)
      if (item && !this.state.focus) {
        textInputProps.value = item.name
        console.log('textInputProps.value', item.name)
      }
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
          onTextChange={this.props.onTextChange}
          containerStyle={{ padding: 0 }}
          itemStyle={styles.item}
          itemTextStyle={styles.itemText}
          itemsContainerStyle={styles.itemsContainer}
          items={this.props.items}
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
    fontSize: 16
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
    fontSize: 16
  },
  itemsContainer: {
    maxHeight: (45 * 2) + 4
  },
  textInput: {
    padding: 10,
    borderWidth: 1,
    borderColor: '#CCC',
    borderRadius: 4,
    fontFamily: 'Cochin',
    fontSize: 16
  }
})

export default Dropdown
