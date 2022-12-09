import { fetchItemTypes } from '@/api/options/itemType'

const state = () => ({
  itemTypes: [],
})

const mutations = {
  setItemTypes: (state, list) => {
    sessionStorage.setItem('eform-itemtype', JSON.stringify(list))
    state.itemTypes = list 
  },
}

const getters = {
  itemTypes:(state, getters)=>{
    return state.itemTypes
  },
}

const actions = {
  getItemTypes({commit}) {
    return new Promise((resolve, reject) => {
        var data = sessionStorage.getItem('eform-itemtype');
        if(data){
          var itemTypes = JSON.parse(data)
          commit('setItemTypes', itemTypes)
          resolve()
        } else {
          fetchItemTypes().then(response => {
            commit('setItemTypes', response)
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
