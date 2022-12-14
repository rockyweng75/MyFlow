import { fetchAgree, fetchDisagree, fetchSubmit, fetchCancel, fetchActionList, fetchLoad, fetchApprove, fetchProcessHistory } from '@/api/workboard/process'

const key = "e-from-action-list";

const state = () => ({
  forward: [],
  backward:[],
  error:[],
  applyData: { },
  approveData: { },
  actionForm: [],
  processHistorys: []
})

const mutations = {
  setForward: (state, list) => {
    state.forward = list 
  },
  setBackward: (state, list) => {
    state.backward = list 
  },
  setError: (state, list) => {
    state.error = list 
  },
  setAction: (state, list) => {
    state.forward = [];
    state.backward = [];
    state.error = [];
    sessionStorage.setItem(key, JSON.stringify(list))

    list.forEach(o =>{
      if(o.ActionType === 'Forward'){
        state.forward.push(o) 
      }
      if(o.ActionType === 'Backward'){
        state.backward.push(o) 
      }
      if(o.ActionType === 'Error'){
        state.error.push(o) 
      }
    })
    state.error = list 
  },
  setApplyData: (state, obj) => {
    if(obj.FormDataObj){
      state.applyData = obj.FormDataObj
      state.applyData.ApplyId = obj.ApplyId
    } else {
      state.applyData = obj
    }
  },
  setApproveData: (state, obj) => {
    if(obj.FormDataObj){
      state.approveData = obj.FormDataObj
      state.approveData.DataId = obj.DataId
    } else {
      state.approveData = obj
    }
  },
  setActionForms: (state, obj) => {
    state.actionForms = obj
  },
  setProcessHistorys:(state, obj) =>{
    state.processHistorys = obj
  }
}

const getters = {
  forward:(state)=>{
    return state.forward
  },
  backward:(state)=>{
    return state.backward
  },
  error:(state)=>{
    return state.error
  },
  applyData:(state)=>{
    return state.applyData
  },
  approveData:(state)=>{
    return state.approveData
  },
  actionForms:(state)=>{
    return state.actionForms
  },
  processHistorys:(state) =>{
    return state.processHistorys
  }
}

const actions = {

  agree({commit}, formData){
    return new Promise((resolve, reject) => {
      fetchAgree(formData)
      .then(response => {
        resolve(response)
      }).catch(error => {
        reject(error)
      })
    })
  },
  disagree({commit}, formData){
    return new Promise((resolve, reject) => {
      fetchDisagree(formData)
      .then(response => {
        resolve(response)
      }).catch(error => {
        reject(error)
      })
    })
  },
  submit({commit}, formData){
    return new Promise((resolve, reject) => {
      fetchSubmit(formData)
      .then(response => {
        resolve(response)
      }).catch(error => {
        reject(error)
      })
    })
  },
  cancel({commit}, formData){
    return new Promise((resolve, reject) => {
      fetchCancel(formData)
      .then(response => {
        resolve(response)
      }).catch(error => {
        reject(error)
      })
    })
  },
  getActionList({commit}){
    return new Promise((resolve, reject) => {
      var tempsave = sessionStorage.getItem(key)
      if(tempsave){
        commit('setAction', JSON.parse(tempsave))
        resolve()
      } else {
        fetchActionList()
        .then(response => {
          commit('setAction', response)
          resolve(response)
        }).catch(error => {
          reject(error)
        })
      }
    })
  },
  load({commit}, dataid){
    return new Promise((resolve, reject) => {
      fetchLoad(dataid)
      .then(response => {
          commit('setApplyData', response.ApplyData)
          commit('setApproveData', response.ApproveData)
          commit('setActionForms', response.ActionForms)
          resolve()
      }).catch(error => {
        reject(error)
      })
      }
    )
  },
  initFormData({commit}) {
    commit('setApplyData', {})
    commit('setApproveData', {})
    commit('setActionForms', [])
  },
  // loadApplyHistory({commit}, applyId){
  //   return new Promise((resolve, reject) => {
  //     fetchApplyHistory(applyId).then(response => {
  //       commit('setApplyHistory', response)
  //       resolve()
  //     }).catch(error => {
  //       reject(error)
  //     })
  //   })
  // },
  loadApproveData({commit}, applyId){
    return new Promise((resolve, reject) => {
      fetchApprove(applyId)
      .then(response => {
        commit('setApplyData', response.ApplyData)
        commit('setApproveData', response.ApproveData)
        commit('setActionForms', response.ActionForms)
        resolve()
      }).catch(error => {
        reject(error)
      })
    })
  },
  loadProcessHistory({commit}, applyId){
    return new Promise((resolve, reject) => {
      fetchProcessHistory(applyId)
      .then(response => {
        commit('setProcessHistorys', response)
        resolve()
      }).catch(error => {
        reject(error)
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
