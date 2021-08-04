import React from "react";
import { Image, PixelRatio, ScrollView, StyleSheet, Text, View } from "react-native";

const size = 50;

const PixelRatioApp = () => (
  <ScrollView style={styles.scrollContainer}>
    <View style={styles.container}>
      <Text>Current Pixel Ratio is:</Text>
      <Text style={styles.value}>{PixelRatio.get()}</Text>
    </View>
    <View style={styles.container}>
      <Text>Current Font Scale is:</Text>
      <Text style={styles.value}>{PixelRatio.getFontScale()}</Text>
    </View>
    <View style={styles.container}>
      <Text>On this device images with a layout width of</Text>
      <Text style={styles.value}>{size} px</Text>
      <Image
        source={require('assets/logo.png')}
        style={{ width: size, height: size }}
      />
    </View>
    <View style={styles.container}>
      <Text>require images with a pixel width of</Text>
      <Text style={styles.value}>
        {PixelRatio.getPixelSizeForLayoutSize(size)} px
      </Text>
      <Image
        source={require('assets/logo.png')}
        style={styles.logo}
      />
    </View>
  </ScrollView>
);

const styles = StyleSheet.create({
  scrollContainer: {
    flex: 1,
  },
  container: {
    justifyContent: 'center',
    alignItems: 'center'
  },
  value: {
    fontSize: 24,
    marginBottom: 12,
    marginTop: 4
  },
  logo: {
    width: PixelRatio.getPixelSizeForLayoutSize(size),
    height: PixelRatio.getPixelSizeForLayoutSize(size)
  }
});

export default PixelRatioApp;
