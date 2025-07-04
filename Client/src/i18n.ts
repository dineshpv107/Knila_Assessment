import i18next from 'i18next';
import HttpBackend from 'i18next-http-backend';
import LanguageDetector from 'i18next-browser-languagedetector';

const i18nInitPromise = i18next
    .use(HttpBackend)
    .use(LanguageDetector)
    .init({
        fallbackLng: 'en',
        debug: true,
        backend: {
            loadPath: '/assets/i18n/{{lng}}.json',
        },
        interpolation: {
            escapeValue: false,
        },
    });

export default i18nInitPromise;
