import { fetchBulletin, fetchBulletinList } from '@/api/bulletin'
import pagination from '../pagination'

const state = () => ({
  ...pagination.state,
  selected:{
  },
  pagination:{
    pageIndex: 1,
    pageSize: 5,
    total: 0
  }
})

const mutations = {
  ...pagination.mutations,
  pushList: (state, list) => {
    list.forEach(o=>{
      state.list.push(o)
    })
  },
  setSelected:(state, model) =>{
    state.selected = model
  }
}

const getters = {
  ...pagination.getters,
  selected:(state, getters) => {
    return state.selected
  },
}

const actions = {
  getOne({commit, state}, id){
    return new Promise((resolve, reject) => {
      fetchBulletin(id)
      .then(response => {
        commit('setSelected', response)
        resolve()
      }).catch(error => {
        reject(error)
      })
    })
  },
  getList({commit, state}) {
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
  load({commit, state}, pagination) {
    if(pagination) commit('setPageIndex', pagination.pageIndex)
    return new Promise((resolve, reject) => {
      fetchBulletinList(state.pagination)
      .then(response => {
        commit('pushList', response.Data)
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
