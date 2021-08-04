import { createAppContainer } from 'react-navigation'
import { createStackNavigator } from 'react-navigation-stack'

const headerHeight = 50

import ProcessedListScreen from './screens/ProcessedListScreen'
import ProcessedNotifyScreen from './screens/ProcessedNotifyScreen'
import ProcessedResponseScreen from './screens/ProcessedResponseScreen'

const ProcessedStackNavigator = createStackNavigator ({
  ProcessedList: {
    screen: ProcessedListScreen,
    path: 'ProcessedList',
    navigationOptions: {
      headerShown: false
    }
  },
  ProcessedNotify: {
    screen: ProcessedNotifyScreen,
    path: 'ProcessedNotify',
    navigationOptions: () => ({
      headerTitle: 'Regresar al listado',
      headerTitleStyle: {fontFamily: 'Cochin', fontSize: 15},
      headerStyle: {height: headerHeight }
    })
  },
  ProcessedResponse: {
    screen: ProcessedResponseScreen,
    path: 'ProcessedResponse',
    navigationOptions: () => ({
      headerTitle: 'Regresar al listado',
      headerTitleStyle: {fontFamily: 'Cochin', fontSize: 15},
      headerStyle: {height: headerHeight }
    })
  }
})

const ProcessedContainer = createAppContainer(ProcessedStackNavigator)

export default ProcessedContainer
