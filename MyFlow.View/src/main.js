import { createApp } from 'vue'
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'
import '@/styles/index.scss'
import router from './router'
import store from './store' 
import './permission'
import App from './App.vue'
import { VueWindowSizePlugin } from 'vue-window-size/option-api';
import dayjs from 'dayjs'
import relativeTime from 'dayjs/plugin/relativeTime'
import 'dayjs/locale/zh-tw'
import CKEditor from '@ckeditor/ckeditor5-vue'

import { createI18n } from 'vue-i18n'
// import zh from "./lang/zh-TW.json";
// import en from "./lang/en-US.json";

const defaultI18n = 'zh-tw';
dayjs.locale(defaultI18n)
dayjs.extend(relativeTime)

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
.use(CKEditor)
.use(i18n)

app.config.globalProperties.$dayjs = (value) => { return dayjs(value) }  

app.config.globalProperties.$filters = {
    'formatDate': value =>{
        if (value) {
            return dayjs(value).format('YYYY/MM/DD hh:mm')
        }
    },
}

app.mount('#app')

