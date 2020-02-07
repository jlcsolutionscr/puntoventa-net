import React, { Component } from 'react'
import { connect } from 'react-redux'
import { bindActionCreators } from 'redux'

import {
  setParameters,
  setNewCantonList,
  setNewDistritoList,
  setNewBarrioList,
  setCustomer,
  getCustomer,
  saveCustomerItem
} from '../../store/customer/actions'

import { Dimensions, StyleSheet, View, ScrollView, Text } from 'react-native'

import SearchableDropdown from '../custom/SearchableDropdown'
import Dropdown from '../custom/Dropdown'
import DatePicker from '../custom/DatePicker'
import CheckBox from '../custom/CheckBox'
import TextField from '../custom/TextField'
import Button from '../custom/Button'

const { width, height } = Dimensions.get('window')
const rem = width / 411.42857142857144
const remY = height / 683.4285714285714

  
class CustomerScreen extends Component {
  constructor (props) {
    super(props)
    this.state = {
      idtipoidentificacion: 0,
      identificacion: '',
      nombreCliente: '',
      nombreComercial: '',
      idProvincia: 0,
      idCanton: 0,
      idDistrito: 0,
      idBarrio: 0,
      direccion: '',
      telefono: '',
      fax: '',
      correo: '',
      idTipoPrecio: 1,
      aplicaTarifaDif: false,
      idImpuesto: 8,
      idTipoExoneracion: 1,
      codigoDocumento: '',
      nombreInstitucion: '',
      fechaEmision: '01/01/2019',
      porcentajeExoneracion: '0'
    }
  }

  componentDidMount () {
    this.props.setParameters()
  }

  componentWillUnmount () {
    if (this.props.customer != null) this.props.setCustomer(null)
  }

  componentDidUpdate (prevProps) {
    const { customer } = this.props
    if (customer != null) {
      if (prevProps.customer == null || prevProps.customer.IdCliente != customer.IdCliente) {
        this.setState({
          idtipoidentificacion: customer.IdTipoIdentificacion,
          identificacion: customer.Identificacion,
          nombreCliente: customer.Nombre,
          nombreComercial: customer.NombreComercial,
          idProvincia: customer.IdProvincia,
          idCanton: customer.IdCanton,
          idDistrito: customer.IdDistrito,
          idBarrio: customer.IdBarrio,
          direccion: customer.Direccion,
          telefono: customer.Telefono,
          fax: customer.Fax,
          correo: customer.CorreoElectronico,
          idTipoPrecio: customer.IdTipoPrecio,
          aplicaTarifaDif: customer.AplicaTasaDiferenciada,
          idImpuesto: customer.IdImpuesto,
          idTipoExoneracion: customer.IdTipoExoneracion,
          codigoDocumento: customer.NumDocExoneracion,
          nombreInstitucion: customer.NombreInstExoneracion,
          fechaEmision: customer.FechaEmisionDoc.DateTime.substr(0, 10),
          porcentajeExoneracion: customer.PorcentajeExoneracion.toString()
        })
      }
    } else if (prevProps.customer != null) {
      this.state = {
        idtipoidentificacion: 0,
        identificacion: '',
        nombreCliente: '',
        nombreComercial: '',
        idProvincia: 0,
        idCanton: 0,
        idDistrito: 0,
        idBarrio: 0,
        direccion: '',
        telefono: '',
        fax: '',
        correo: '',
        idTipoPrecio: 1,
        aplicaTarifaDif: false,
        idImpuesto: 8,
        idTipoExoneracion: 1,
        nombreInstitucion: '',
        codigoDocumento: '',
        fechaEmision: '01/01/2019',
        porcentajeExoneracion: 0
      }
    }
  }

  render () {
    const {
      error,
      customerList,
      idTypeList,
      provinciaList,
      cantonList,
      distritoList,
      barrioList,
      priceTypeList,
      rentTypeList,
      exonerationTypeList
    } = this.props
    const {
      idtipoidentificacion,
      identificacion,
      nombreCliente,
      nombreComercial,
      idProvincia,
      idCanton,
      idDistrito,
      idBarrio,
      direccion,
      telefono,
      fax,
      correo,
      idTipoPrecio,
      aplicaTarifaDif,
      idImpuesto,
      idTipoExoneracion,
      nombreInstitucion,
      codigoDocumento,
      fechaEmision,
      porcentajeExoneracion
    } = this.state
    const customers = customerList.map(item => {
      return { id: item.Id, name: item.Descripcion }
    })
    const idTypeItems = idTypeList.map(item => {
      return { value: item.Id, label: item.Descripcion }
    })
    const provincias = provinciaList.map(item => {
      return { id: item.Id, name: item.Descripcion }
    })
    const cantones = cantonList.map(item => {
      return { id: item.Id, name: item.Descripcion }
    })
    const distritos = distritoList.map(item => {
      return { id: item.Id, name: item.Descripcion }
    })
    const barrios = barrioList.map(item => {
      return { id: item.Id, name: item.Descripcion }
    })
    const rentTypeItems = []
    rentTypeList.forEach(item => {
      if (item.Id !== 1) rentTypeItems.push({ value: item.Id, label: item.Descripcion })
    })
    const priceTypeItems = priceTypeList.map(item => {
      return { value: item.Id, label: item.Descripcion }
    })
    const exonerationTypesItems = exonerationTypeList.map(item => {
      return { value: item.Id, label: item.Descripcion }
    })
    let idPlaceholder = ''
    let idMaxLength = 0
    switch (idtipoidentificacion) {
      case 0:
        idPlaceholder = '999999999'
        idMaxLength = 9
        break
      case 1:
        idPlaceholder = '9999999999'
        idMaxLength = 10
        break
      case 2:
        idPlaceholder = '999999999999'
        idMaxLength = 12
        break
      case 3:
        idPlaceholder = '9999999999'
        idMaxLength = 10
        break
      default:
        idPlaceholder = '999999999'
        idMaxLength = 9
    }
    const buttonEnabled = identificacion != null &&
      nombreCliente != '' &&
      idProvincia != null &&
      idCanton != null &&
      idDistrito != null &&
      idBarrio != null &&
      direccion != '' &&
      telefono != '' &&
      correo != '' &&
      idTipoPrecio != null &&
      idImpuesto != null &&
      idTipoExoneracion != null
    const intPorcExoneracion = porcentajeExoneracion != '' ? parseInt(porcentajeExoneracion) : 0
    const validExoneration = intPorcExoneracion == 0 || (intPorcExoneracion > 0 && codigoDocumento != '' && nombreInstitucion != '' && fechaEmision != '')
    return (<View key='1' style={styles.container}>
      {error !== '' && <Text style={{color: 'red', textAlign: 'center'}}>{error}</Text>}
      <SearchableDropdown
        label='Seleccione un cliente'
        items={customers}
        onItemSelect={(item) => this.props.getCustomer(item.id)}
      />
      <ScrollView keyboardShouldPersistTaps='handled'>
        <View key='2'>
          <Dropdown
            selectedValue={idtipoidentificacion}
            label='Seleccione el tipo de identificación'
            items={idTypeItems}
            resetValue={false}
            onValueChange={(itemValue, itemPosition) => this.setState({idtipoidentificacion: itemValue})}
          />
          <TextField
            label='Identificación'
            placeholder={idPlaceholder}
            maxLength={idMaxLength}
            keyboardType='numeric'
            value={identificacion}
            onChangeText={(identificacion) => this.setState({identificacion})}
          />
          <TextField
            label='Nombre'
            placeholder='Nombre del cliente'
            value={nombreCliente}
            onChangeText={(nombreCliente) => this.setState({nombreCliente})}
          />
          <TextField
            label='Nombre Comercial'
            placeholder='Nombre comercial'
            value={nombreComercial}
            onChangeText={(nombreComercial) => this.setState({nombreComercial})}
          />
          <SearchableDropdown
            label='Seleccione la provincia'
            items={provincias}
            selectedItemId={idProvincia}
            onItemSelect={(item) => this.setSelectedProvincia(item)}
          />
          <SearchableDropdown
            label='Seleccione el cantón'
            items={cantones}
            selectedItemId={idCanton}
            onItemSelect={(item) => this.setSelectedCanton(item)}
          />
          <SearchableDropdown
            label='Seleccione el distrito'
            items={distritos}
            selectedItemId={idDistrito}
            onItemSelect={(item) => this.setSelectedDistrito(item)}
          />
          <SearchableDropdown
            label='Seleccione el barrio'
            items={barrios}
            selectedItemId={idBarrio}
            onItemSelect={(item) => this.setState({ idBarrio: item.id})}
          />
          <TextField
            label='Dirección'
            placeholder='Dirección por señas'
            value={direccion}
            onChangeText={(direccion) => this.setState({direccion})}
          />
          <TextField
            label='Teléfono'
            placeholder='99999999'
            keyboardType='numeric'
            value={telefono}
            onChangeText={(telefono) => this.setState({telefono})}
          />
          <TextField
            label='Fax'
            placeholder='99999999'
            keyboardType='numeric'
            value={fax}
            onChangeText={(fax) => this.setState({fax})}
          />
          <TextField
            label='Correo'
            placeholder='xxx@yyyy.zzz'
            value={correo}
            onChangeText={(correo) => this.setState({correo})}
          />
          <Dropdown
            label='Seleccione el tipo de precio'
            selectedValue={idTipoPrecio}
            items={priceTypeItems}
            onValueChange={(itemValue, itemPosition) => this.setState({idTipoPrecio: itemValue})}
          />
          <CheckBox
            label='Aplica tarifa diferenciada'
            value={aplicaTarifaDif}
            onValueChange={() => this.handleCheckBoxChange()}
          />
          <Dropdown
            disabled={!aplicaTarifaDif}
            selectedValue={idImpuesto}
            items={rentTypeItems}
            onValueChange={(itemValue, itemPosition) => this.setState({idImpuesto: itemValue})}
          />
          <Dropdown
            label='Seleccione el tipo de exoneración'
            selectedValue={idTipoExoneracion}
            items={exonerationTypesItems}
            resetValue={false}
            onValueChange={(itemValue, itemPosition) => this.setState({idTipoExoneracion: itemValue})}
          />
          <TextField
            label='Código del documento'
            value={codigoDocumento}
            onChangeText={(documento) => this.setState({codigoDocumento: documento})}
          />
          <TextField
            label='Nombre de la institución'
            value={nombreInstitucion}
            onChangeText={(institucion) => this.setState({nombreInstitucion: institucion})}
          />
          <DatePicker
            label='Fecha de emisión'
            value={fechaEmision}
            onChange={(fecha) => this.setState({fechaEmision: fecha})}
          />
          <TextField
            label='Porcentaje de exoneración'
            keyboardType='numeric'
            value={porcentajeExoneracion}
            onChangeText={(porcentaje) => this.setState({porcentajeExoneracion: porcentaje})}
            onEndEditing={(e) => this.setState({porcentajeExoneracion: e.nativeEvent.text != '' ? e.nativeEvent.text : '0'})}
          />
          <Button
            title="Guardar"
            titleUpperCase
            disabled={!buttonEnabled || !validExoneration}
            containerStyle={{marginTop: 20}} 
            onPress={() => this.handleOnPress()}
          />
        </View>
      </ScrollView>
    </View>)
  }

  setSelectedProvincia (item) {
    if (this.state.idProvincia != item.id) {
      this.setState({
        idProvincia: item.id,
        idCanton: 1,
        idDistrito: 1,
        idBarrio: 1
      })
      this.props.setNewCantonList(item.id)
    }
  }

  setSelectedCanton (item) {
    if (this.state.idCanton != item.id) {
      this.setState({
        idCanton: item.id,
        idDistrito: 1,
        idBarrio: 1
      })
      this.props.setNewDistritoList(this.state.idProvincia, item.id)
    }
  }

  setSelectedDistrito (item) {
    if (this.state.idDistrito != item.id) {
      this.setState({
        idDistrito: item.id,
        idBarrio: 1
      })
      this.props.setNewBarrioList(this.state.idProvincia, this.state.idCanton, item.id)
    }
  }

  handleCheckBoxChange () {
    this.setState({
      aplicaTarifaDif: !this.state.aplicaTarifaDif,
      idImpuesto: !this.state.aplicaTarifaDif ? this.state.idImpuesto : 8
    })
  }

  handleOnPress() {
    const { idtipoidentificacion,
      identificacion,
      nombreCliente,
      nombreComercial,
      idProvincia,
      idCanton,
      idDistrito,
      idBarrio,
      direccion,
      telefono,
      fax,
      correo,
      idTipoPrecio,
      aplicaTarifaDif,
      idImpuesto,
      idTipoExoneracion,
      codigoDocumento,
      nombreInstitucion,
      fechaEmision,
      porcentajeExoneracion
    } = this.state
    const customer = {
      IdTipoIdentificacion: idtipoidentificacion,
      Identificacion: identificacion,
      Nombre: nombreCliente,
      NombreComercial: nombreComercial != null ? nombreComercial : '',
      IdProvincia: idProvincia,
      IdCanton: idCanton,
      IdDistrito: idDistrito,
      IdBarrio: idBarrio,
      Direccion: direccion,
      Telefono: telefono,
      Fax: fax != null ? fax : '',
      CorreoElectronico: correo,
      IdTipoPrecio: idTipoPrecio,
      AplicaTasaDiferenciada: aplicaTarifaDif,
      IdImpuesto: idImpuesto,
      IdTipoExoneracion: idTipoExoneracion,
      NumDocExoneracion: codigoDocumento,
      NombreInstExoneracion: nombreInstitucion,
      FechaEmisionDoc: {DateTime: fechaEmision + ' 22:59:59 GMT-07:00'},
      PorcentajeExoneracion: porcentajeExoneracion
    }
    if (this.props.customer) {
      customer.IdCliente = this.props.customer.IdCliente
    }
    this.props.saveCustomerItem(customer)
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
    customerList: state.customer.customerList,
    idTypeList: state.customer.idTypeList,
    provinciaList: state.customer.provinciaList,
    cantonList: state.customer.cantonList,
    distritoList: state.customer.distritoList,
    barrioList: state.customer.barrioList,
    priceTypeList: state.customer.priceTypeList,
    rentTypeList: state.customer.rentTypeList,
    exonerationTypeList: state.customer.exonerationTypeList,
    customer: state.customer.entity,
    error: state.customer.error
  }
}

const mapDispatchToProps = (dispatch) => {
  return bindActionCreators({
    setParameters,
    setNewCantonList,
    setNewDistritoList,
    setNewBarrioList,
    getCustomer,
    setCustomer,
    saveCustomerItem
  }, dispatch)
}

export default connect(mapStateToProps, mapDispatchToProps)(CustomerScreen)
