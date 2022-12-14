import { fetchActionClasses } from '@/api/options/actionClass'

const state = () => ({
  forward: [],
  backward: [],
  error: []
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
}

const getters = {
  forward:(state, getters)=>{
    return state.forward
  },
  backward:(state, getters)=>{
    return state.backward
  },
  error:(state, getters)=>{
    return state.error
  },
}

const actions = {
  getList({commit}) {
    return new Promise((resolve, reject) => {
      fetchActionClasses().then(response => {
        commit('setOptions', response)
        resolve()
      }).catch(error => {
        reject(error)
      })
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
