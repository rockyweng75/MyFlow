import { fetchActionClasses } from '@/api/options/actionClass'

const state = () => ({
  forward: [],
  backward: [],
  error: []
})

const mutations = {
  setForward: (state, list) => {
    state.forward = list.filter(o => o.ActionType == 1)[0].Options 
  },
  setBackward: (state, list) => {
    state.backward = list.filter(o => o.ActionType == 2)[0].Options
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
      fetchActionClasses()
      .then(response => {
        if(response != null){
          commit('setForward', response)
          commit('setBackward', response)
          commit('setError', response)
        }
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
