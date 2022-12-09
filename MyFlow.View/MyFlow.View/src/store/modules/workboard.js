// import { fetchTodoList, fetchForm, fetchWaitList, fetchHistoryList, fetchApproveList } from '../../api/workboard'

// const state = () => ({
//   todoList:[],
//   approveList:[],
//   historyList:[],
//   waitList:[],
//   pager: {
//     PageIndex: 1,
//     RowNumber: 10,
//     Total: 0
//   }
// })

// const mutations = {
//   setTodoList: (state, list) => {
//     state.todoList = list
//   },
//   setApproveList: (state, list) => {
//     state.approveList = list
//   },
//   setHistoryList: (state, list) => {
//     state.historyList = list
//   },
//   setWaitList: (state, list) => {
//     state.waitList = list
//   },
//   setPager: (state, pager) => {
//     state.pager = pager
//   },
//   setPageIndex: (state, index) => {
//     state.pager.PageIndex = index
//   },
// }

// const getters = {
//   todoList:(state, getters)=>{
//     return state.todoList
//   },
//   approveList:(state, getters)=>{
//     return state.approveList
//   },
//   historyList:(state, getters)=>{
//     return state.historyList
//   },
//   waitList:(state, getters)=>{
//     return state.waitList
//   },
//   pageIndex:(state, getters)=>{
//     return state.pager.PageIndex
//   },
//   rowNumber:(state, getters)=>{
//     return state.pager.RowNumber
//   },
//   total:(state, getters)=>{
//     return state.pager.Total
//   },
// }

// const actions = {
//   getTodoList({commit, state}) {
//     return new Promise((resolve, reject) => {
//       fetchTodoList(state.pager).then(response => {
//         commit('setTodoList', response.Data)
//         commit('setPager', response.Pager)
//         resolve()
//       }).catch(error => {
//         reject(error)
//       })
//     })
//   },
//   getApproveList({commit, state}) {
//     return new Promise((resolve, reject) => {
//       fetchApproveList(state.pager).then(response => {
//         commit('setApproveList', response.Data)
//         commit('setPager', response.Pager)
//         resolve()
//       }).catch(error => {
//         reject(error)
//       })
//     })
//   },
//   getHistoryList({commit, state}) {
//     return new Promise((resolve, reject) => {
//       fetchHistoryList(state.pager).then(response => {
//         commit('setHistoryList', response.Data)
//         commit('setPager', response.Pager)
//         resolve()
//       }).catch(error => {
//         reject(error)
//       })
//     })
//   },
//   getWaitList({commit, state}) {
//     return new Promise((resolve, reject) => {
//       fetchWaitList(state.pager).then(response => {
//         commit('setWaitList', response.Data)
//         commit('setPager', response.Pager)
//         resolve()
//       }).catch(error => {
//         reject(error)
//       })
//     })
//   },
//   loadForm({commit}, id) {
//     return new Promise((resolve, reject) => {
//       fetchForm(id).then(response => {
//         resolve(response)
//       }).catch(error => {
//         reject(error)
//       })
//     })
//   },
// }

// export default {
//   namespaced: true,
//   state,
//   getters,
//   mutations,
//   actions
// }
