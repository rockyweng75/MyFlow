import { fetchDeadline } from '@/api/options/deadline'

const key = "deadline";
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
  getDeadlines({commit}) {
    return new Promise((resolve, reject) => {
        var data = sessionStorage.getItem(key);
        if(data){
          var options = JSON.parse(data)
          commit('setOptions', options)
          resolve()
        } else {
          fetchDeadline().then(response => {
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
