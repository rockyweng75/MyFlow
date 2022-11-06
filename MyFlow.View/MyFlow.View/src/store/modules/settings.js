import variables from '/@/styles/element-variables.scss'
import defaultSettings from '/@/settings'

const { showSettings, tagsView, fixedHeader, sidebarLogo, logoUrl, defaultOpeneds} = defaultSettings

const state = {
  theme: variables.theme,
  showSettings: showSettings,
  tagsView: tagsView,
  fixedHeader: fixedHeader,
  sidebarLogo: sidebarLogo,
  logo: logoUrl,
  defaultOpeneds: defaultOpeneds

}

const mutations = {
  setSetting: (state, { key, value }) => {
    // eslint-disable-next-line no-prototype-builtins
    if (state.hasOwnProperty(key)) {
      state[key] = value
    }
  }
}

const actions = {
  changeSetting({ commit }, data) {
    commit('setSetting', data)
  }
}

const getters = {
    theme: (state) => state.theme,
    showSettings: (state)=> state.showSettings,
    tagsView: (state)=> state.tagsView,
    fixedHeader: (state)=> state.fixedHeader,
    sidebarLogo: (state)=> state.sidebarLogo,
    logo: (state)=> state.logoUrl,
    defaultOpeneds: (state)=>state.defaultOpeneds
}


export default {
  namespaced: true,
  state,
  getters,
  mutations,
  actions
}
