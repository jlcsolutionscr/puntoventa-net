module.exports = {
  presets: ['module:metro-react-native-babel-preset'],
  plugins: [
    [
      'module-resolver',
      {
        root: ['./src'],
        alias: {
          '@app/components': './components',
          '@app/assets': './assets',
          '@app/store': './store',
          '@app/utils': './utils',
        },
      },
    ],
  ],
};
