import { state, getters, mutations } from "../filterCrud"
import { fetchApproveList, fetchDistincts } from '@/api/workboard/approveList'

const actions = {
  getApproveList({commit, state}) {
    return new Promise((resolve, reject) => {
      var order = {};      
      order[state.order.prop] = state.order.order;
      fetchApproveList({
        ...state.pager,
        SFilter: {...state.filter },
        SOrder: { ...state.order }
      })
      .then(response => {
        commit('setDataList', response.Data)
        commit('setPager', response.Pager)
        resolve()
      }).catch(error => {
        reject(error)
      })
    })
  },
  getDistinctList({commit}, column) {
    return new Promise((resolve, reject) => {
      fetchDistincts(column).then(res=>{
        resolve(res)
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