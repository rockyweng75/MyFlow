import { fetchTitles } from '@/api/options/title'

const key = "titles"

const state = () => ({
  optons:[]
})

const mutations = {
    setOptions: (state, list) => {
      state.optons = list 
      sessionStorage.setItem(key, JSON.stringify(list));
    },
}

const actions = {
  getTitles({ commit }) {
    return new Promise((resolve, reject) => {
      var data = sessionStorage.getItem(key);
      if(data){
        commit('setOptions', JSON.parse(data))
        resolve()
      } else {
        fetchTitles().then(response => {
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
