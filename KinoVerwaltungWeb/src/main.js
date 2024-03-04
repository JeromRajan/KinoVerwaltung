import './assets/main.css'

import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import router from './router'

// Vuetify
import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'

// i18n
import { installI18n } from './i18n'
import {useLanguageStore} from '@/stores/languageStore'

import '@mdi/font/css/materialdesignicons.css'

// Create the Vuetify instance
const vuetify = createVuetify({
  components,
  directives,
})

// Create the app
const app = createApp(App)

// Use the plugins
app.use(vuetify)
app.use(createPinia())
app.use(router)

// Global variables
const globals = app.config.globalProperties
globals.BASE_URL = 'https://localhost:44336/api'
export { globals }

// i18n
const i18n = installI18n(app)

app.mount('#app')

const languageStore = useLanguageStore();

languageStore.$subscribe((_, state) =>
{
  console.info('Language changed to', state.language);
  if (state.language === 'en' || state.language === 'de' || state.language === 'fr' || state.language === 'it') {
    i18n.global.locale = state.language;
  } else {
    console.error('Invalid language:', state.language);
  };
})
