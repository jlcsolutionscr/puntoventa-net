import React, { Component } from 'react'
import { connect } from 'react-redux'

import { StyleSheet, ScrollView } from 'react-native'

import HTMLView from 'react-native-render-html'
  
class ProcessedResponseScreen extends Component {
  render () {
    const { processedResponse } = this.props
    return (
      <ScrollView style={styles.container}>
        <HTMLView html={processedResponse} />
      </ScrollView>
    )
  }
}
const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 10
  },
  
})

const mapStateToProps = (state) => {
  return {
    processedResponse: state.document.processedResponse
  }
}

export default connect(mapStateToProps, null)(ProcessedResponseScreen)
