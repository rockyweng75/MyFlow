import { fetchValidation } from '@/api/options/validation'

const key = 'eform-validation';

const state = () => ({
  validations: [],
})

const mutations = {
  setValidations: (state, list) => {
    sessionStorage.setItem(key, JSON.stringify(list))
    state.validations = list 
  },
}

const getters = {
  validations:(state, getters)=>{
    return state.validations
  },
}

const actions = {
  getValidations({commit}) {
    return new Promise((resolve, reject) => {
        var data = sessionStorage.getItem(key);
        if(data){
          var validation = JSON.parse(data)
          commit('setValidations', validation)
          resolve()
        } else {
          fetchValidation().then(response => {
            commit('setValidations', response)
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
