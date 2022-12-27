import { fetchMemberList } from '@/api/member'

const state = () => ({
  list:[]
})

const mutations = {
  setList: (state, list) => {
    state.list = list 
  },
}

const getters = {
  list:(state, getters) => {
    return state.list
  },
}

const actions = {
  getList({commit}) {
    return new Promise((resolve, reject) => {
    fetchMemberList()
      .then(response => {
        commit('setList', response)
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
