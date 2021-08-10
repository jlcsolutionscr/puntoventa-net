import React, { Component } from 'react'
import { connect } from 'react-redux'
import { bindActionCreators } from 'redux'

import {
  setProcessedList,
  getProcessedFirstPage,
  getProcessedPageByPageNumber,
  setProcessedSelected,
  getProcessedResponse
} from 'store/document/actions'

import { formatCurrency } from 'utils/formatHelper'

import { Dimensions, StyleSheet, View, Text, ScrollView } from 'react-native'
import IconButton from '../../custom/IconButton'
import FirstIcon from 'assets/first.png'
import FirstDisabledIcon from 'assets/first-disabled.png'
import PreviousIcon from 'assets/previous.png'
import PreviousDisabledIcon from 'assets/previous-disabled.png'
import NextIcon from 'assets/next.png'
import NextDisabledIcon from 'assets/next-disabled.png'
import LastIcon from 'assets/last.png'
import LastDisabledIcon from 'assets/last-disabled.png'
import EmailIcon from 'assets/email.png'
import EmailDisabledIcon from 'assets/email-disabled.png'
import InfoIcon from 'assets/info.png'

const { width, height } = Dimensions.get('window')

class ProcessedDocumentScreen extends Component {
  constructor (props) {
    super(props)
  }

  componentDidMount () {
    this.props.getProcessedFirstPage()
  }

  componentWillUnmount () {
    this.props.setProcessedList([])
  }

  render () {
    const { processedList, processedPage, processedCount } = this.props
    const previousDisabled = processedPage === 1
    const lastPage = Math.ceil(processedCount / 10)
    const nextDisabled = processedPage === lastPage
    const rows = processedList.map((item, index) => {
      const buttonDisabled = item.NombreReceptor == 'CLIENTE DE CONTADO' || item.EsMensajeReceptor == 'S' || item.EstadoEnvio != 'aceptado'
      return <View key={index} style={[styles.columnsAlign, styles.contentRow]}>
        <View style={[styles.columnsAlign, {width: '82%'}]}>
          <Text style={{width: '10%', textAlign: 'left', fontSize: 11}}>{item.IdDocumento}</Text>
          <Text style={{width: '44%', textAlign: 'left', fontSize: 11}}>{item.Consecutivo}</Text>
          <Text style={{width: '25%', textAlign: 'right', fontSize: 11}}>{item.Fecha}</Text>
          <Text style={{width: '21%', textAlign: 'right', fontSize: 11}}>{item.EstadoEnvio}</Text>
          <Text style={{width: '70%', textAlign: 'left', fontSize: 11}}>{item.NombreReceptor}</Text>
          <Text style={{width: '30%', textAlign: 'right', fontSize: 11}}>{formatCurrency(item.MontoTotal)}</Text>
        </View>
        <View style={{width: '9%', paddingLeft: 10}}>
          <IconButton
            disabled={buttonDisabled}
            size={25}
            iconSize={20}
            iconButton={buttonDisabled ? EmailDisabledIcon : EmailIcon}
            primaryColor={'transparent'}
            disabledColor={'transparent'}
            onPressButton={() => this.handleOnEmailPress(item)}/>
        </View>
        <View style={{width: '9%', paddingLeft: 10}}>
          <IconButton
            size={25}
            iconSize={20}
            iconButton={InfoIcon}
            primaryColor={'transparent'}
            disabledColor={'transparent'}
            onPressButton={() => this.handleOnDetailPress(item)}/>
        </View>
      </View>
    })
    return (<View key='1' style={styles.container}>
      <View style={styles.table}>
        <ScrollView style={styles.content}>
          {rows}
        </ScrollView>
        <View style={[styles.columnsAlign, {justifyContent: 'center'}]}>
          <View style={{margin: 10}}>
            <IconButton
              size={29}
              iconSize={15}
              primaryColor='#CCCCC9'
              disabledColor={'#F5F5F5'}
              disabled={previousDisabled}
              iconButton={previousDisabled ? FirstDisabledIcon : FirstIcon}
              onPressButton={() => this.handleFirstPage()}
            />
          </View>
          <View style={{margin: 10}}>
            <IconButton
              size={29}
              iconSize={15}
              primaryColor='#CCCCC9'
              disabledColor={'#F5F5F5'}
              disabled={previousDisabled}
              iconButton={previousDisabled ? PreviousDisabledIcon : PreviousIcon}
              onPressButton={() => this.handlePreviousPage()}
            />
          </View>
          <Text style={{width: '30%', textAlign: 'center', fontSize: 12}}>{`Página ${processedPage} de ${lastPage}`}</Text>
          <View style={{margin: 10}}>
            <IconButton
              size={29}
              iconSize={15}
              primaryColor='#CCCCC9'
              disabledColor={'#F5F5F5'}
              disabled={nextDisabled}
              iconButton={nextDisabled ? NextDisabledIcon : NextIcon}
              onPressButton={() => this.handleNextPage()}
            />
          </View>
          <View style={{margin: 10}}>
            <IconButton
              size={29}
              iconSize={15}
              primaryColor='#CCCCC9'
              disabledColor={'#F5F5F5'}
              disabled={nextDisabled}
              iconButton={nextDisabled ? LastDisabledIcon : LastIcon}
              onPressButton={() => this.handleLastPage()}
            />
          </View>
        </View>
      </View>
    </View>)
  }

  handleFirstPage () {
    this.props.getProcessedFirstPage()
  }

  handlePreviousPage() {
    this.props.getProcessedPageByPageNumber(this.props.processedPage - 1)
  }

  handleNextPage() {
    this.props.getProcessedPageByPageNumber(this.props.processedPage + 1)
  }

  handleLastPage() {
    const { processedCount, getProcessedPageByPageNumber } = this.props
    const lastPage = Math.ceil(processedCount / 10)
    getProcessedPageByPageNumber(lastPage)
  }

  handleOnEmailPress (item) {
    this.props.setProcessedSelected(item)
    this.props.navigation.navigate('ProcessedNotify')
  }

  handleOnDetailPress (item) {
    this.props.getProcessedResponse(item.IdDocumento)
    this.props.navigation.navigate('ProcessedResponse')
  }
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#FFFFFF',
    padding: 10
  },
  table: {
    flexDirection: 'column',
    marginTop: 10,
    borderTopWidth: 0.5
  },
  content: {
    height: height - 160,
    borderColor: '#08415C'
  },
  columnsAlign: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    alignItems: 'center'
  },
  contentRow: {
    borderBottomWidth: 0.5,
    padding: 7
  }
})

const mapStateToProps = (state) => {
  return {
    processedList: state.document.processedList,
    processedPage: state.document.processedPage,
    processedCount: state.document.processedCount
  }
}

const mapDispatchToProps = (dispatch) => {
  return bindActionCreators({
    setProcessedList,
    getProcessedFirstPage,
    getProcessedPageByPageNumber,
    setProcessedSelected,
    getProcessedResponse
  }, dispatch)
}

export default connect(mapStateToProps, mapDispatchToProps)(ProcessedDocumentScreen)