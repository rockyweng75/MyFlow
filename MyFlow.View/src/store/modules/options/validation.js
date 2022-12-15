import { fetchValidation } from '@/api/options/validation'

const key = 'validation';

const state = () => ({
  options: [],
})

const mutations = {
  setOptions: (state, list) => {
    sessionStorage.setItem(key, JSON.stringify(list))
    state.options = list 
  },
}

const getters = {
  options:(state, getters)=>{
    return state.options
  },
}

const actions = {
  getList({commit}) {
    return new Promise((resolve, reject) => {
        var data = sessionStorage.getItem(key);
        if(data){
          var validation = JSON.parse(data)
          commit('setOptions', validation)
          resolve()
        } else {
          fetchValidation().then(response => {
            commit('setOptions', response)
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
