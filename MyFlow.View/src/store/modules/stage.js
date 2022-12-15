import { fetchStages, fetchStage } from '@/api/stage'

const state = () => ({
  list: [],
  model:{ },
})

const mutations = {
  setList: (state, list) => {
    state.list = list 
  },
  setModel: (state, model) => {
    state.model = model 
  },
}

const getters = {
  list:(state, getters)=>{
    return state.list
  },
  model:(state, getters) => {
    return state.model
  },
}

const actions = {
  getList({commit}, flowId) {
    return new Promise((resolve, reject) => {
      fetchStages(flowId)
      .then(response => {
          commit('setList', response)
          resolve()
      }).catch(error => {
        reject(error)
      })
    })
  },
  getOne({commit}, stageId) {
    return new Promise((resolve, reject) => {
      fetchStage(stageId)
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
