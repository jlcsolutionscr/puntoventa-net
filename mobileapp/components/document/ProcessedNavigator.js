import { createAppContainer } from 'react-navigation'
import { createStackNavigator } from 'react-navigation-stack'

import { Dimensions } from 'react-native'
const { width, height } = Dimensions.get('window')
const rem = width / 411.42857142857144
const remY = height / 683.4285714285714
const headerHeight = (50 * remY)

import ProcessedListScreen from './screens/ProcessedListScreen'
import ProcessedNotifyScreen from './screens/ProcessedNotifyScreen'
import ProcessedResponseScreen from './screens/ProcessedResponseScreen'

const ProcessedStackNavigator = createStackNavigator ({
  ProcessedList: {
    screen: ProcessedListScreen,
    path: 'ProcessedList',
    navigationOptions: {
      header: null
    }
  },
  ProcessedNotify: {
    screen: ProcessedNotifyScreen,
    path: 'ProcessedNotify',
    navigationOptions: () => ({
      headerTitle: 'Regresar al listado',
      headerTitleStyle: {fontFamily: 'Cochin', fontSize: (15 * rem)},
      headerStyle: {background: 'white', height: headerHeight }
    })
  },
  ProcessedResponse: {
    screen: ProcessedResponseScreen,
    path: 'ProcessedResponse',
    navigationOptions: () => ({
      headerTitle: 'Regresar al listado',
      headerTitleStyle: {fontFamily: 'Cochin', fontSize: (15 * rem)},
      headerStyle: {background: 'white', height: headerHeight }
    })
  }
})

const ProcessedContainer = createAppContainer(ProcessedStackNavigator)

export default ProcessedContainer
