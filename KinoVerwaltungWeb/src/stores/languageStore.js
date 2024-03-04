import {defineStore} from 'pinia'

export const useLanguageStore = defineStore('language', {
  state: () => ({
    language: 'en',
    availableLanguages: ['en', 'de','fr','it']
  }),
  actions: {
    setLanguage(language) {
      this.language = language;
    }
  }
})
