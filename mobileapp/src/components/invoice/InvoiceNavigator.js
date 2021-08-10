import { createAppContainer } from 'react-navigation'
import { createMaterialTopTabNavigator } from 'react-navigation-tabs'

import Page1Screen from './screens/Page1Screen'
import Page2Screen from './screens/Page2Screen'
import Page3Screen from './screens/Page3Screen'

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
        height: 45,
        backgroundColor:'#08415C'
      },
      labelStyle: {
        fontSize: 16
      }
    }
  }
)

export default createAppContainer(tabNavigator)
