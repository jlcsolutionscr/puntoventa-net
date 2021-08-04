import React, { Component } from 'react'

import { createAppContainer } from 'react-navigation'
import { createStackNavigator } from 'react-navigation-stack'

import { Dimensions, StyleSheet, View, TouchableOpacity, Image, Text } from 'react-native'
import AnimatedView from 'components/custom/AnimatedView'
import InvoiceNavigator from 'components/invoice/InvoiceNavigator'
import InvoiceListScreen from 'components/invoice-list/InvoiceListScreen'
import ProcessedNavigator from 'components/document/ProcessedNavigator'
import CustomerScreen from 'components/customer/CustomerScreen'
import ProductScreen from 'components/product/ProductScreen'
import ReportScreen from 'components/report/ReportScreen'
import ConfigScreen from 'components/config/ConfigScreen'
import HomeScreen from './screens/HomeScreen'

const { width } = Dimensions.get('window')

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
            headerTitle: () => (
              <View style={styles.headerContainer}>
                <View style={styles.headerTitle}>
                  <Image
                    source={require('assets/logo.png')}
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
                      source={require('assets/user.png')}
                      style={styles.userImage}
                    />
                  </View>
                  <View>
                    <Text style={styles.companyText}>{this.props.company.NombreComercial}</Text>
                    <Text style={styles.companyText}>{this.props.company.Identificacion}</Text>
                  </View>
                </View>
                <View style={styles.logout}>
                  <TouchableOpacity activeOpacity={0.5} onPress={() => this.props.logOut()}>
                    <Image
                      source={require('assets/account-lock.png')}
                      style={styles.logoutImage}
                    />
                  </TouchableOpacity>
                </View>
              </View>
            ),
            headerStyle: {
              height: 150,
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
        },
        Configuracion: {
          screen: ConfigScreen,
          path: 'Configuración',
          navigationOptions: () => ({
            headerTintColor: 'white',
            title: 'Configuración de parámetros',
            headerTitleStyle: styles.tabBarText,
            headerStyle: styles.background
          })
        }
      },
      {
        initialRouteName: 'Home',
        headerMode: 'screen',
        headerTitleAlign: 'center',
      }
    )
    
    const AppContainer = createAppContainer(AppNavigator)
    return (
      <AnimatedView>
        <AppContainer />
      </AnimatedView>
    )
  }
}

const styles = StyleSheet.create({
  headerContainer: {
    flex: 1,
    width: width
  },
  headerTitle: {
    alignItems: 'center'
  },
  logo: {
    width: 65,
    height: 65,
    position: 'absolute',
    top: '20%',
    left: 0,
  },
  headerContent: {
    flex: 1,
    flexDirection: 'row',
    marginTop: '5%',
    marginLeft: '3%'
  },
  title: {
    alignItems: 'center',
    marginTop: '5%',
    marginLeft: 0
  },
  titleText: {
    fontSize: 20,
    color: 'black',
    fontFamily: 'Cochin'
  },
  tabBarText: {
    fontSize: 18,
    color: 'white',
    fontFamily: 'Cochin'
  },
  companyText: {
    fontSize: 16,
    color: 'black',
    fontFamily: 'Cochin'
  },
  userIcon: {
    padding: 10
  },
  userImage: {
    width: 20,
    height: 20
  },
  logout: {
    position: 'absolute',
    padding: 0,
    top: '70%',
    left: width - (width * 0.2)
  },
  logoutImage: {
    width: 30,
    height: 30
  },
  background: {
    height: 40,
    backgroundColor: '#08415C'
  }
})

export default HomeNavigator
