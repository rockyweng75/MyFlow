import { fetchTags } from '@/api/options/tag'

const key = "eform-tags"
const state = () => ({
  tags:[]
})

const mutations = {
    setTags: (state, list) => {
      state.tags = list 
      sessionStorage.setItem(key, JSON.stringify(list))
    },
}

const actions = {
  getTags({ commit }) {
    return new Promise((resolve, reject) => {
      var data = sessionStorage.getItem(key);
      if(data){
        commit('setTags', JSON.parse(data))
        resolve()
      } else {
        fetchTags().then(response => {
          commit('setTags', response)
          resolve()
        }).catch(error => {
            reject(error)
        })
      }
    })
  },
}

const getters = {
  tags:(state, getters) =>{
    return state.tags;
  },
}

export default {
  namespaced: true,
  state,
  getters,
  mutations,
  actions
}
