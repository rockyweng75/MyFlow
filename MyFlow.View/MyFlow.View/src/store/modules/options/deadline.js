import { fetchDeadline } from '@/api/options/deadline'

const state = () => ({
  deadlines: [],
})

const mutations = {
  setDeadlines: (state, list) => {
    sessionStorage.setItem('deadline', JSON.stringify(list))
    state.deadlines = list 
  },
}

const getters = {
  deadlines:(state, getters)=>{
    return state.deadlines
  },
}

const actions = {
  getDeadlines({commit}) {
    return new Promise((resolve, reject) => {
        var data = sessionStorage.getItem('deadline');
        if(data){
          var deadline = JSON.parse(data)
          commit('setDeadlines', deadline)
          resolve()
        } else {
          fetchDeadline().then(response => {
            commit('setDeadlines', response)
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
