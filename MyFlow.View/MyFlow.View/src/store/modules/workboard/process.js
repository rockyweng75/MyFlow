import { fetchAgree, fetchDisagree, fetchSubmit, fetchCancel, fetchActionList } from '@/api/workboard/process'

const key = "e-from-action-list";

const state = () => ({
  forward: [],
  backward:[],
  error:[],
})

const mutations = {
  setForward: (state, list) => {
    state.forward = list 
  },
  setBackward: (state, list) => {
    state.backward = list 
  },
  setError: (state, list) => {
    state.error = list 
  },
  setAction: (state, list) => {
    state.forward = [];
    state.backward = [];
    state.error = [];
    sessionStorage.setItem(key, JSON.stringify(list))

    list.forEach(o =>{
      if(o.ActionType === 'Forward'){
        state.forward.push(o) 
      }
      if(o.ActionType === 'Backward'){
        state.backward.push(o) 
      }
      if(o.ActionType === 'Error'){
        state.error.push(o) 
      }
    })
    state.error = list 
  },
}

const getters = {
  forward:(state)=>{
    return state.forward
  },
  backward:(state)=>{
    return state.backward
  },
  error:(state)=>{
    return state.error
  },
}

const actions = {

  agree({commit}, formData){
    return new Promise((resolve, reject) => {
      fetchAgree(formData)
      .then(response => {
        resolve(response)
      }).catch(error => {
        reject(error)
      })
    })
  },
  disagree({commit}, formData){
    return new Promise((resolve, reject) => {
      fetchDisagree(formData)
      .then(response => {
        resolve(response)
      }).catch(error => {
        reject(error)
      })
    })
  },
  submit({commit}, formData){
    return new Promise((resolve, reject) => {
      fetchSubmit(formData)
      .then(response => {
        resolve(response)
      }).catch(error => {
        reject(error)
      })
    })
  },
  cancel({commit}, formData){
    return new Promise((resolve, reject) => {
      fetchCancel(formData)
      .then(response => {
        resolve(response)
      }).catch(error => {
        reject(error)
      })
    })
  },
  getActionList({commit}){
    return new Promise((resolve, reject) => {
      var tempsave = sessionStorage.getItem(key)
      if(tempsave){
        commit('setAction', JSON.parse(tempsave))
        resolve()
      } else {
        fetchActionList()
        .then(response => {
          commit('setAction', response)
          resolve(response)
        }).catch(error => {
          reject(error)
        })
      }
    })
  },
}

export default {
  namespaced: true,
  state,
  getters,
  mutations,
  actions
}
