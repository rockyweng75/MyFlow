import { fetchForms, fetchApplyForm, fetchForm, fetchStage, create, update } from '../../api/form'

const state = () => ({
  formList: [],
  model:{
    FormName: '',
    FormType: null,
    ButtonName: '',
    Items: [{
      ItemType: '',
      ItemTitle: '',
      ItemValue: '',
      ItemDesc: '',
      ItemFormat: '',
      DataRef: ''
    }]
  },
  defaultOpeneds:[],
  selectedForm:{},
  stageForm: {},
})

const mutations = {
  setFormList: (state, list) => {
    state.formList = list 
  },
  setStageForm: (state, list) => {
    state.stageForm = list 
  },
  setSelectedForm: (state, form) => {
    state.selectedForm = form 
  },
  setDefaultOpeneds: (state, list) =>{
    state.defaultOpeneds = []
    list.forEach(form=>{
        if(form.children){
            state.defaultOpeneds.push(form.name)
        }
    })
  }
}

const getters = {
  forms:(state, getters)=>{
    return state.formList
  },
  selectedForm:(state, getters)=>{
    return state.selectedForm
  },
  items:(state, getters)=>{
    return getters.selectedForm.Items
  },
  stageForm:(state, getters)=>{
    return state.stageForm
  },
  stageItems:(state, getters)=>{
    return getters.stageForm.Items
  },
  defaultOpeneds: (state, getters)=>{
    return state.defaultOpeneds
  }
}

const actions = {
  getForms({commit}) {
    return new Promise((resolve, reject) => {
      fetchForms().then(response => {
        commit('setFormList', response)
        commit('setDefaultOpeneds', response)
        resolve(response)
      }).catch(error => {
        reject(error)
      })
    })
  },
  getForm({commit, state }, formId) {
    return new Promise((resolve, reject) => {
      if(formId){
        fetchForm(formId)
        .then(response => {
          commit('setSelectedForm', response)
          resolve()
        }).catch(error => {
          reject(error)
        })
      } else {
        commit('setSelectedForm', state.model)
        resolve()
      }
    })
  },
  getApplyForm({commit}, flowId) {
    return new Promise((resolve, reject) => {
      fetchApplyForm(flowId).then(response => {
          commit('setSelectedForm', response)
          resolve()
        }).catch(error => {
          reject(error)
        })
      })
  },
  getStage({commit}, model){
    return new Promise((resolve, reject) => {
      fetchStage(model).then(response => {
        commit('setStageForm', response)
        resolve()
      }).catch(error => {
        reject(error)
      })
    })
  },
  insert({commit}, formData){
    return new Promise((resolve, reject) => {
      create(formData).then(response => {
        resolve(response)
      }).catch(error => {
        reject(error)
      })
    })
  },
  modify({commit}, formData){
    return new Promise((resolve, reject) => {
      update(formData).then(response => {
        resolve(response)
      }).catch(error => {
        reject(error)
      })
    })
  }
}

export default {
  namespaced: true,
  state,
  getters,
  mutations,
  actions
}
