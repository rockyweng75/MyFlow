import { fetchTargets } from '@/api/options/target'

const key = "eform-targets"

const state = () => ({
  targets:[]
})

const mutations = {
    setTargets: (state, list) => {
      state.targets = list 
      sessionStorage.setItem(key, JSON.stringify(list))
    },
}

const actions = {
  getTargets({ commit }) {
    return new Promise((resolve, reject) => {
      var data = sessionStorage.getItem(key);
      if(data){
        commit('setTargets', JSON.parse(data))
        resolve()
      } else {
        fetchTargets().then(response => {
            commit('setTargets', response)
            resolve()
        }).catch(error => {
            reject(error)
        })
      }
    })
  },
}

const getters = {
  targets:(state, getters) =>{
    return state.targets;
  },
}

export default {
  namespaced: true,
  state,
  getters,
  mutations,
  actions
}
