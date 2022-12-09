
import { createStore } from 'vuex'

import app from './modules/app'
import security from './modules/security'
import settings from './modules/settings'

import dataRef from './modules/options/dataRef'
import deadline from './modules/options/deadline'
import flowType from './modules/options/flowType'
import itemType from './modules/options/itemType'
import job from './modules/options/job'
import tag from './modules/options/tag'
import target from './modules/options/target'
import title from './modules/options/title'
import validation from './modules/options/validation'

import approveList from './modules/workboard/approveList'
import historyList from './modules/workboard/historyList'
import todoList from './modules/workboard/todoList'
import waitList from './modules/workboard/waitList'
import process from './modules/workboard/process'

import flowchart from './modules/flowchart'


const store = createStore({
  modules:{
   app,
   security,
   settings,
   dataRef,
   deadline,
   flowType,
   itemType,
   job,
   tag,
   target,
   title,
   validation,
   approveList,
   historyList,
   todoList,
   waitList,
   process,
   flowchart
  }
})
export default store