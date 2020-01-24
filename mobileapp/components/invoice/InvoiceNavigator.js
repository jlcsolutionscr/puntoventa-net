import { createAppContainer } from 'react-navigation'
import { createMaterialTopTabNavigator } from 'react-navigation-tabs'

import Page1Screen from './screens/Page1Screen'
import Page2Screen from './screens/Page2Screen'
import Page3Screen from './screens/Page3Screen'

import { Dimensions } from 'react-native'
const { width, height } = Dimensions.get('window')
const rem = width / 411.42857142857144
const remY = height / 683.4285714285714

const tabNavigator = createMaterialTopTabNavigator(
  {  
    'CLIENTE': Page1Screen,
    'DETALLE': Page2Screen,
    'GENERAR': Page3Screen
  },
  {
    tabBarOptions: {
      activeTintColor: 'white',
      style: {
        height: (45 * remY),
        backgroundColor:'#08415C'
      },
      labelStyle: {
        fontSize: (16 * rem)
      }
    }
  }
)

export default createAppContainer(tabNavigator)
