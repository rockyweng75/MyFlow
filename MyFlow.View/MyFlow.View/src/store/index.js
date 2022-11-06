
import { createStore } from 'vuex'

import app from './modules/app'
import security from './modules/security'
import settings from './modules/settings'


const store = createStore({
  modules:{
   app,
   security,
   settings 
  }
})
export default store