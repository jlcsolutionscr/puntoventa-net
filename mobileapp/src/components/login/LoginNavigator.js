import React, { Component } from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'

import { Image } from 'react-native'
import { createAppContainer } from 'react-navigation'
import { createBottomTabNavigator } from 'react-navigation-tabs'
import AnimatedView from '../custom/AnimatedView'

import SignUpScreen from './screens/SignUpScreen'
import LoginScreen from './screens/LoginScreen'

class LoginRouter extends Component {
  resetScreenState () {

  }

  render () {
    let tabs = {
      SignUp: SignUpScreen
    }
    if (this.props.isDeviceRegistered) {
      tabs = {
        Login: LoginScreen,
        SignUp: SignUpScreen
      }
    }
    const TabNavigator = createBottomTabNavigator(
    tabs,
    {
      defaultNavigationOptions: ({ navigation }) => ({
        tabBarIcon: ({ focused, horizontal, tintColor }) => {
          const { routeName } = navigation.state;
          if (routeName === 'Login') {
            return (
              <Image
                source={require('assets/user-white.png')}
                style={{ width: 20, height: 20 }} />
            );
          } else {
            return (
              <Image
                source={require('assets/settings-white.png')}
                style={{ width: 20, height: 20 }} />
            );
          }
        },
      }),
      tabBarOptions: {
        style: {
          backgroundColor: 'black',
        },
        activeTintColor: 'white',
        inactiveTintColor: 'gray',
      },
    });
    const AppContainer = createAppContainer(TabNavigator)
    return (
      <AnimatedView>
        <AppContainer />
      </AnimatedView>
    )
  }
}

LoginScreen.propTypes = {
  configuration: PropTypes.object
}

const mapStateToProps = (state) => {
  return {
    identifierList: state.config.identifierList,
    isDeviceRegistered: state.config.isDeviceRegistered
  }
};

export default connect(mapStateToProps)(LoginRouter)
