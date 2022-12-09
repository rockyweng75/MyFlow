import { fetchTitles } from '@/api/options/title'

const key = "eform-titles"

const state = () => ({
  titles:[]
})

const mutations = {
    setTitles: (state, list) => {
      state.titles = list 
      sessionStorage.setItem(key, JSON.stringify(list));
    },
}

const actions = {
  getTitles({ commit }) {
    return new Promise((resolve, reject) => {
      var data = sessionStorage.getItem(key);
      if(data){
        commit('setTitles', JSON.parse(data))
        resolve()
      } else {
        fetchTitles().then(response => {
          commit('setTitles', response)
          resolve()
        }).catch(error => {
            reject(error)
        })
      }

    })
  },
}

const getters = {
  titles:(state, getters) =>{
    return state.titles;
  },
}

export default {
  namespaced: true,
  state,
  getters,
  mutations,
  actions
}
