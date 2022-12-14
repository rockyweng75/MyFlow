import { fetchTargets } from '@/api/options/target'

const key = "targets"

const state = () => ({
  options:[]
})

const mutations = {
  setOptions: (state, list) => {
    state.options = list 
    sessionStorage.setItem(key, JSON.stringify(list))
  },
}

const actions = {
  getTargets({ commit }) {
    return new Promise((resolve, reject) => {
      var data = sessionStorage.getItem(key);
      if(data){
        commit('setOptions', JSON.parse(data))
        resolve()
      } else {
        fetchTargets().then(response => {
            commit('setOptions', response)
            resolve()
        }).catch(error => {
            reject(error)
        })
      }
    })
  },
}

const getters = {
  options:(state, getters) =>{
    return state.options;
  },
}

export default {
  namespaced: true,
  state,
  getters,
  mutations,
  actions
}
