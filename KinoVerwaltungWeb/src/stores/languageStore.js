import {defineStore} from 'pinia'

export const useLanguageStore = defineStore('language', {
  state: () => ({
    language: 'en',
    availableLanguages: ['en', 'de','fr']
  }),
  actions: {
    setLanguage(language) {
      this.language = language;
    }
  }
})
