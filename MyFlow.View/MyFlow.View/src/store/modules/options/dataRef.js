import { fetchDataRefs } from '@/api/options/dataRef'

const state = () => ({
  dataRefs: [],
})

const mutations = {
  setDataRefs: (state, list) => {
    sessionStorage.setItem('dataRef', JSON.stringify(list))
    state.itemTypes = list 
  },
}

const getters = {
  dataRefs:(state, getters)=>{
    return state.itemTypes
  },
}

const actions = {
  getDataRefs({commit}) {
    return new Promise((resolve, reject) => {
        var data = sessionStorage.getItem('dataRef');
        if(data){
          var dataRefs = JSON.parse(data)
          commit('setDataRefs', dataRefs)
          resolve()
        } else {
          fetchDataRefs().then(response => {
            commit('setDataRefs', response)
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
