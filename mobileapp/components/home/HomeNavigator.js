import React, { Component } from 'react'

import { createAppContainer } from 'react-navigation'
import { createStackNavigator } from 'react-navigation-stack'

import { Dimensions, StyleSheet, View, TouchableOpacity, Image, Text } from 'react-native'
import AnimatedView from '../custom/AnimatedView'
import HomeScreen from './screens/HomeScreen'
import InvoiceNavigator from '../invoice/InvoiceNavigator'
import InvoiceListScreen from '../invoice-list/InvoiceListScreen'
import ProcessedNavigator from '../document/ProcessedNavigator'
import CustomerScreen from '../customer/CustomerScreen'
import ProductScreen from '../product/ProductScreen'
import ReportScreen from '../report/ReportScreen'

const { width, height } = Dimensions.get('window')
const rem = width / 411.42857142857144
const remY = height / 683.4285714285714

class HomeNavigator extends Component {
  shouldComponentUpdate (newProps) {
    return false
  }

  render() {
    const AppNavigator = createStackNavigator(
      {
        Home: {
          screen: () => <HomeScreen company={this.props.company} />,
          path: 'Home',
          navigationOptions: () => ({
            headerTitle: (
              <View>
                <View style={styles.headerTitle}>
                  <Image
                    source={require('../../assets/logo.png')}
                    style={styles.logo}
                  />
                  <View style={styles.title}>
                    <Text style={styles.titleText}>JLC Solutions CR</Text>
                    <Text style={styles.titleText}>Facturación Electrónica</Text>
                  </View>
                </View>
                <View style={styles.headerContent}>
                  <View style={styles.userIcon}>
                    <Image
                      source={require('../../assets/user.png')}
                      style={styles.userImage}
                    />
                  </View>
                  <View>
                    <Text style={styles.companyText}>{this.props.company.NombreComercial}</Text>
                    <Text style={styles.companyText}>{this.props.company.Identificacion}</Text>
                  </View>
                </View>
                <View style={styles.logout}>
                  <TouchableOpacity activeOpacity={0.5} onPress={() => this.handleOnPress()}>
                    <Image
                      source={require('../../assets/account-lock.png')}
                      style={styles.logoutImage}
                    />
                  </TouchableOpacity>
                </View>
              </View>
            ),
            headerStyle: {
              height: (150 * remY),
              backgroundColor: 'white'
            }
          })
        },
        NuevaFactura: {
          screen: InvoiceNavigator,
          path: 'NuevaFactura',
          navigationOptions: () => ({
            headerTintColor: 'white',
            title: 'Factura electrónica',
            headerTitleStyle: styles.tabBarText,
            headerStyle: styles.background
          })
        },
        Facturas: {
          screen: InvoiceListScreen,
          path: 'Facturas',
          navigationOptions: () => ({
            headerTintColor: 'white',
            title: 'Facturas emitidas',
            headerTitleStyle: styles.tabBarText,
            headerStyle: styles.background
          })
        },
        Documentos: {
          screen: ProcessedNavigator,
          path: 'Documentos',
          navigationOptions: () => ({
            headerTintColor: 'white',
            title: 'Documentos electrónicos emitidos',
            headerTitleStyle: styles.tabBarText,
            headerStyle: styles.background
          })
        },
        Cliente: {
          screen: CustomerScreen,
          path: 'Cliente',
          navigationOptions: () => ({
            headerTintColor: 'white',
            title: 'Clientes',
            headerTitleStyle: styles.tabBarText,
            headerStyle: styles.background
          })
        },
        Producto: {
          screen: ProductScreen,
          path: 'Producto',
          navigationOptions: () => ({
            headerTintColor: 'white',
            title: 'Productos',
            headerTitleStyle: styles.tabBarText,
            headerStyle: styles.background
          })
        },
        Reporte: {
          screen: ReportScreen,
          path: 'Reporte',
          navigationOptions: () => ({
            headerTintColor: 'white',
            title: 'Generación de reportes',
            headerTitleStyle: styles.tabBarText,
            headerStyle: styles.background
          })
        }
      },
      {
        initialRouteName: 'Home',
        headerMode: 'screen',
        headerLayoutPreset: 'center'
      }
    )
    
    const AppContainer = createAppContainer(AppNavigator)
    return (
      <AnimatedView>
        <AppContainer />
      </AnimatedView>
    )
  }

  handleOnPress () {
    this.props.logOut()
  }
}

const styles = StyleSheet.create({
  headerTitle: {
    flex: 1,
    width: width,
    alignItems: 'center'
  },
  headerContent: {
    flex: 1,
    flexDirection: 'row',
    marginTop: 45,
    marginLeft: 45,
  },
  logo: {
    width: 75,
    height: 75,
    position: 'absolute',
    top: 15,
    left: 20
  },
  title: {
    alignItems: 'center',
    marginTop: 20,
    marginLeft: (30 * rem)
  },
  titleText: {
    fontSize: (20 * rem),
    color: 'black',
    fontFamily: 'Cochin'
  },
  tabBarText: {
    fontSize: (18 * rem),
    color: 'white',
    fontFamily: 'Cochin'
  },
  companyText: {
    fontSize: (16 * rem),
    color: 'black',
    fontFamily: 'Cochin'
  },
  userIcon: {
    padding: 10
  },
  userImage: {
    width: (20 * rem),
    height: (20 * rem)
  },
  logout: {
    position: 'absolute',
    padding: 0,
    top: (103 * rem),
    left: width - (54 * rem)
  },
  logoutImage: {
    width: (30 * rem),
    height: (30 * rem)
  },
  background: {
    height: (40 * remY),
    backgroundColor: '#08415C'
  }
})

export default HomeNavigator
