import constantRoutes  from '@/router/constantRoutes'
import asyncRoutes from "@/router/asyncRoutes"
import { getToken as getCookies, setToken as setCookies, removeToken as removeCookies } from '@/cookies/index.js'
import { login, logout, getUser } from '@/api/security'

/**
 * Use meta.role to determine if the current user has permission
 * @param roles
 * @param route
 */
function hasPermission(roles, route) {
  if (route.meta && route.meta.roles) {
    return roles.some(role => route.meta.roles.includes(role))
  } else {
    return true
  }
}

/**
 * Filter asynchronous routing tables by recursion
 * @param routes asyncRoutes
 * @param roles
 */
export function filterAsyncRoutes(routes, roles, isChild) {
  const res = []

  routes.forEach(route => {
    const tmp = { ...route }
    if (hasPermission(roles, tmp)) {
      if (tmp.children) {
        tmp.children = filterAsyncRoutes(tmp.children, roles, true)
      }
      if(isChild) tmp.isChild = isChild
      res.push(tmp)
    }
  })
  return res
}

const state = {
  routes: [],
  addRoutes: [],
  user:{},
  roles:[],
  token: getCookies()
}

const mutations = {
  setRoutes: (state, routes) => {
    state.addRoutes = routes
    state.routes = constantRoutes.concat(routes)
  },
  setUser: (state, user) => {
    state.user = user
  },
  setToken: (state, token) => {
    state.token = token
    setCookies(token)
  },
  setRoles: (state, roles) => {
    state.roles = roles
  },
  removeToken:(state) =>{
    state.token = null
    removeCookies()
  }
}

const getters = {
    routes: (state) => state.routes,
    addRoutes: (state)=> state.addRoutes,
    user: (state)=> state.user,
    userName: (state)=> state.user.UserName,
    roles: (state)=> state.roles,
    token: (state)=> state.token
}

const actions = {
  generateRoutes({ commit }, roles) {
    return new Promise(resolve => {
      let accessedRoutes = filterAsyncRoutes(asyncRoutes, roles)
      commit('setRoutes', accessedRoutes)
      resolve(accessedRoutes)
    })
  },
  login({ commit }, data){
    return new Promise(resolve => {
      login(data).then(res =>{
        commit('setToken', res)
        resolve(res)
      })
    })
  },
  logout({ commit }){
    return new Promise(resolve => {
      logout().then(() =>{
        commit('removeToken')
        resolve()
      })
    })
  },
  getUserinfo({ commit, state }){
    return new Promise(resolve => {
      getUser(state.token).then(res =>{
        commit('setUser', res.user)
        commit('setRoles', res.user.Roles)
        resolve(res.roles)
      })
    })
  },
  resetToken({ commit }){
    return new Promise(resolve => {
      commit('removeToken')
      resolve()
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