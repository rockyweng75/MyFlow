
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
import actionClass from './modules/options/actionClass'
import switchClass from './modules/options/switchClass'
import actionType from './modules/options/actionType'
import formType from './modules/options/formType'

import approveList from './modules/workboard/approveList'
import historyList from './modules/workboard/historyList'
import todoList from './modules/workboard/todoList'
import waitList from './modules/workboard/waitList'
import process from './modules/workboard/process'

import flowchart from './modules/flowchart'
import form from './modules/form'
import stage from './modules/stage'
import weather from './modules/weather'
import member from './modules/member'
import bulletin from './modules/bulletin'

import chat from './modules/chat'

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
   switchClass,
   actionType,
   formType,
   approveList,
   historyList,
   todoList,
   waitList,
   process,
   flowchart,
   form,
   actionClass,
   stage,
   weather,
   member,
   bulletin,
   chat
  }
})
export default store