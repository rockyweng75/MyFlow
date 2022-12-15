import { fetchJobs } from '@/api/options/job'

const state = () => ({
  options:[]
})

const mutations = {
  setOptions: (state, list) => {
    state.options = list 
  },
}

const actions = {
  getList({ commit }) {
    return new Promise((resolve, reject) => {
      fetchJobs().then(response => {
          commit('setOptions', response)
          resolve()
      }).catch(error => {
          reject(error)
      })
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
