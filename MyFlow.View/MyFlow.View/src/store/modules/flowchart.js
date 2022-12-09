import { fetchFlowcharts, fetchFlowchartsByAdmin, fetchFlowchart, create, update } from '@/api/flowchart'

const state = () => ({
  flowList: [],
  flowchart:{ },
  model:{
    FlowName:'',
    FlowType:'',
    AdminUser:'',
  },
  defaultOpenedList: [],
})

const mutations = {
  setFlowList: (state, list) => {
    state.flowList = list 
  },
  setFlowchart: (state, flow) => {
    state.flowchart = flow 
  },
  setDefaultOpenedList: (state, list) => {
    state.defaultOpenedList = []
    console.log(list)
    list.forEach(form=>{
        if(form.children){
            state.defaultOpenedList.push(form.name)
        }
    })
  },
}

const getters = {
  flowList:(state, getters)=>{
    return state.flowList
  },
  flowchart:(state, getters) => {
    return state.flowchart
  },
  items:(state, getters)=>{
    return getters.selectedForm.items
  },
  defaultOpeneds:(state, getters)=>{
    return state.defaultOpenedList
  }
}

const actions = {
  getFlowcharts({commit}) {
    return new Promise((resolve, reject) => {
        fetchFlowcharts()
        .then(response => {
            console.log(response)
          commit('setFlowList', response)
          commit('setDefaultOpenedList', response)
          resolve()
        }).catch(error => {
          reject(error)
        })
      })
  },
  getFlowchartsByAdmin({commit}) {
    return new Promise((resolve, reject) => {
        fetchFlowchartsByAdmin().then(response => {
          commit('setFlowList', response)
          commit('setDefaultOpenedList', response)
          resolve()
        }).catch(error => {
          reject(error)
        })
      })
  },
  getFlowchart({commit, state}, flowId) {
    return new Promise((resolve, reject) => {
      if(flowId){
        fetchFlowchart(flowId)
        .then(response => {
            commit('setFlowchart', response)
            resolve(response)
        }).catch(error => {
          reject(error)
        })
      } else {
        commit('setFlowchart', state.model)
        resolve()
      }
    })
  }, 
  insert({commit}, formData){
    return new Promise((resolve, reject) => {
      create(formData).then(response => {
        resolve(response)
      }).catch(error => {
        reject(error)
      })
    })
  },
  modify({commit}, formData){
    return new Promise((resolve, reject) => {
      update(formData).then(response => {
        resolve(response)
      }).catch(error => {
        reject(error)
      })
    })
  }
}

export default {
  namespaced: true,
  state,
  getters,
  mutations,
  actions
}
