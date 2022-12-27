import { fetchBulletinList } from '@/api/bulletin'
import pagination from '../pagination'

const state = () => ({
  ...pagination.state,
  pagination:{
    pageIndex: 2,
    pageSize: 5,
    total: 0
  }
})

const mutations = {
  ...pagination.mutations
}

const getters = {
  ...pagination.getters
}

const actions = {
  getList({commit, state}, pagination) {
    if(pagination) commit('setPageIndex', pagination.pageIndex)
    return new Promise((resolve, reject) => {
      fetchBulletinList(state.pagination)
      .then(response => {
        commit('setList', response.Data)
        commit('setPagination', response.Pager)
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
