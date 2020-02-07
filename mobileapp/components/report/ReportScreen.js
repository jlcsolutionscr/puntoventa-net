import React, { Component } from 'react'
import { connect } from 'react-redux'
import { bindActionCreators } from 'redux'

import { generateReportEmail, setReportMessage, setReportError } from '../../store/session/actions'

import { StyleSheet, View, Text } from 'react-native'
import SearchableDropdown from '../custom/SearchableDropdown'
import DatePicker from '../custom/DatePicker'
import Button from '../custom/Button'

class ReportScreen extends Component {
  constructor (props) {
    super(props)
    const today = new Date()
    const lastDayOfMonth = new Date(today.getFullYear(), today.getMonth() + 1, 0).getDate()
    const month = ((today.getMonth() + 1) < 10 ? '0' : '') + (today.getMonth() + 1)
    this.state = {
      reportName: '',
      startDate: `01/${month}/${today.getFullYear()}`,
      endDate: `${lastDayOfMonth}/${month}/${today.getFullYear()}`
    }
  }

  componentDidMount () {
    this.props.setReportMessage('')
    this.props.setReportError('')
  }

  render () {
    const { message, error, company } = this.props
    const reportList = company.ReportePorEmpresa.map((item, index) => {
      return { id: index, name: item.CatalogoReporte.NombreReporte }
    })
    const { reportName, startDate, endDate } = this.state
    const buttonEnabled = reportName != '' && startDate != '' && endDate != ''
    return (<View key='1' style={styles.container}>
      {message !== '' && <Text style={{color: 'green', textAlign: 'center'}}>{message}</Text>}
      {error !== '' && <Text style={{color: 'red', textAlign: 'center'}}>{error}</Text>}
      <SearchableDropdown
        label='Seleccione el tipo de reporte'
        items={reportList}
        onItemSelect={(item) => this.setState({reportName: item.name})}
      />
      <DatePicker
        label='Fecha inicial'
        value={startDate}
        onChange={(value) => this.setState({startDate: value})}
      />
      <DatePicker
        label='Fecha final'
        value={endDate}
        onChange={(value) => this.setState({endDate: value})}
      />
      <Button
        title="Enviar reporte"
        titleUpperCase
        disabled={!buttonEnabled}
        containerStyle={{marginTop: 20}} 
        onPress={() => this.handleOnPress()}
      />
    </View>)
  }

  handleOnPress () {
    const { reportName, startDate, endDate } = this.state
    this.props.generateReportEmail(reportName, startDate, endDate)
  }
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 10
  }
})

const mapStateToProps = (state) => {
  return {
    company: state.session.company,
    message: state.session.reportMessage,
    error: state.session.reportError
  }
}

const mapDispatchToProps = (dispatch) => {
  return bindActionCreators({
    generateReportEmail,
    setReportMessage,
    setReportError
  }, dispatch)
}

export default connect(mapStateToProps, mapDispatchToProps)(ReportScreen)
