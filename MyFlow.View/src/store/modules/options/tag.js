import { fetchTags } from '@/api/options/tag'

const key = "tags"
const state = () => ({
  options:[]
})

const mutations = {
  setOptions: (state, list) => {
    state.options = list 
    sessionStorage.setItem(key, JSON.stringify(list))
  },
}

const actions = {
  getTags({ commit }) {
    return new Promise((resolve, reject) => {
      var data = sessionStorage.getItem(key);
      if(data){
        commit('setOptions', JSON.parse(data))
        resolve()
      } else {
        fetchTags().then(response => {
          commit('setOptions', response)
          resolve()
        }).catch(error => {
            reject(error)
        })
      }
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
