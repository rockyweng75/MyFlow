import { fetchList, fetchListByAdmin, fetchOne } from '/@/api/flowchart'
import paginationStore from '../pagination'
const state = {
    ...paginationStore,
    selected: {},
    defaultOpeneds: false,
}

const mutations = {
    ...paginationStore,
    setSelected: (state, data) => {
        state.selected = data
    }
}

const getters = {
    ...paginationStore,
    selected: (state) => state.selected,
    defaultOpeneds: (state) => state.defaultOpeneds,
}

const actions = {
    getList({ commit }) {
        return new Promise((resolve, reject) => {
            fetchList()
            .then(res=>{
                commit('setPagination', res)
                commit('setList', res)
                resolve()
            }).catch(error => {
                reject(error)
            })
        })
    },
    getListByAdmin({ commit }) {
        return new Promise((resolve, reject)  => {
            fetchListByAdmin()
            .then(res=>{
                commit('setPagination', res)
                commit('setList', res)
            }).catch(error => {
                reject(error)
            })
        })
    },
    getOne({ commit }, id) {
        return new Promise(resolve => {
            fetchOne(id)
            .then(res=>{
                commit('setSelected', res)
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