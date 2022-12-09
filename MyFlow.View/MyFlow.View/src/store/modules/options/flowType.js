import { fetchFlowTypes } from '@/api/options/flowType'

const state = () => ({
  flowTypes: [],
})

const mutations = {
  setFlowTypes: (state, list) => {
    sessionStorage.setItem('eform-flowtype', JSON.stringify(list))
    state.flowTypes = list 
  },
}

const getters = {
  flowTypes:(state, getters)=>{
    return state.flowTypes
  },
}

const actions = {
  getFlowTypes({commit}) {
    return new Promise((resolve, reject) => {
        var data = sessionStorage.getItem('eform-flowtype');
        if(data){
          var flowTypes = JSON.parse(data)
          commit('setFlowTypes', flowTypes)
          resolve()
        } else {
          fetchFlowTypes().then(response => {
            commit('setFlowTypes', response)
            resolve()
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
