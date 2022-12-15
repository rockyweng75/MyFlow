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

const getters = {
  optons:(state, getters) =>{
    return state.optons;
  },
}

const actions = {
  getList({ commit }) {
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



export default {
  namespaced: true,
  state,
  getters,
  mutations,
  actions
}
