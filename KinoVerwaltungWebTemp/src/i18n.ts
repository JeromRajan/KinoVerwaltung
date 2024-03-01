import {createI18n} from 'vue-i18n'
import en from './locales/en.json'
import de from './locales/de.json'
import fr from './locales/fr.json'
import { useLanguageStore } from '@/stores/languageStore'

export function installI18n(app: any) {
  const i18n = createI18n({
    locale: useLanguageStore().language, // set language
    fallbackLocale: 'de', // set fallback locale
    messages: {
      'en' : en,
      'de' : de,
      'fr' : fr
    }
  });
  app.use(i18n);
  return i18n;
}
