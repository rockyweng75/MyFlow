
const state = () => ({
  showDrawer: false
})

const mutations = {
  toggleDrawer: (state, bool) => {
    state.showDrawer = bool
  },
}

const getters = {
    showDrawer:(state, getters) => {
        return state.showDrawer
    },
}

const actions = {
//   openDrawer({commit}) {
//     return new Promise((resolve, reject) => {
//     fetchMemberList()
//       .then(response => {
//         commit('setList', response)
//         resolve()
//       }).catch(error => {
//         reject(error)
//       })
//     })
//   },
}

export default {
  namespaced: true,
  state,
  getters,
  mutations,
  actions
}
