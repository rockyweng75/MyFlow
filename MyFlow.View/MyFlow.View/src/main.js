import { createApp } from 'vue'
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'
import '@/styles/index.scss'
import router from './router'
import store from './store' 
import './permission'
import App from './App.vue'
import { VueWindowSizePlugin } from 'vue-window-size/option-api';
import moment from 'moment/min/moment-with-locales'
import { createI18n } from 'vue-i18n'
// import zh from "./lang/zh-TW.json";
// import en from "./lang/en-US.json";

const defaultI18n = 'zh-tw';
moment.locale(defaultI18n)

const i18n = createI18n({
    legacy: false,
    globalInjection: true,
    locale: localStorage.getItem("lang") ?? defaultI18n,
    fallbackLocale: defaultI18n,
    messages: {
    //   "zh-tw": zh,
    //   "en-us": en,
    }
  });

const app = createApp(App)
.use(store)
.use(router)
.use(ElementPlus)
.use(VueWindowSizePlugin)
.use(i18n)

app.config.globalProperties.$moment = moment;

app.config.globalProperties.$filters = {
    'formatDate': value =>{
        if (value) {
            return moment(String(value)).format('YYYY/MM/DD hh:mm')
        }
    }
}

app.mount('#app')

