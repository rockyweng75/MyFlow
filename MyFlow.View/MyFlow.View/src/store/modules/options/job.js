import { fetchJobs } from '@/api/options/job'

const state = () => ({
  jobs:[]
})

const mutations = {
    setJobs: (state, list) => {
        state.jobs = list 
    },
}

const actions = {
  getJobs({ commit }) {
    return new Promise((resolve, reject) => {
      fetchJobs().then(response => {
          commit('setJobs', response)
          resolve()
      }).catch(error => {
          reject(error)
      })
    })
  },
}

const getters = {
  jobs:(state, getters) =>{
    return state.jobs;
  },
}

export default {
  namespaced: true,
  state,
  getters,
  mutations,
  actions
}
