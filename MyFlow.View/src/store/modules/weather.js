import { fetchWeather } from '@/api/weather'

const state = () => ({
  model:{ },
})

const mutations = {
  setModel: (state, model) => {
    state.model = model 
  },
}

const getters = {
  model:(state, getters) => {
    return state.model
  },
}

const actions = {
  getOne({commit}, location) {
    return new Promise((resolve, reject) => {
    fetchWeather(location)
      .then(response => {
        commit('setModel', response)
        resolve()
      }).catch(error => {
        reject(error)
      })
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
