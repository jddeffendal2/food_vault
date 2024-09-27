/* eslint-env node */
require('@rushstack/eslint-patch/modern-module-resolution')
require('@stylistic/eslint-plugin-js')

module.exports = {
  root: true,
  'extends': [
    'plugin:vue/vue3-essential',
    'eslint:recommended',
    '@vue/eslint-config-prettier'
  ],
  plugins: [
    "@stylistic/js"
  ],
  parserOptions: {
    ecmaVersion: 'latest'
  },
  rules: {
    "@stylistic/js/indent": ["error", 2],
    "@stylistic/js/semi": ["error", "never"],
    "@stylistic/js/quotes": ["error", "single", {
      "avoidEscape": true,
      "allowTemplateLiterals": true
    }]
  }
}
