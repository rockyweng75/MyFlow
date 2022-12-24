import Cookies from 'js-cookie'

const state = {
  sidebar: {
    opened: Cookies.get('sidebarStatus') ? !!+Cookies.get('sidebarStatus') : true,
    withoutAnimation: false
  },
  device: 'desktop',
  size: 'medium',
  windowsWidth: 960,
  lang: "zh-tw",
  avatar: {}
}

const mutations = {
  toggleSidebar: state => {
    state.sidebar.opened = !state.sidebar.opened
    state.sidebar.withoutAnimation = false
    if (state.sidebar.opened) {
      Cookies.set('sidebarStatus', 1)
    } else {
      Cookies.set('sidebarStatus', 0)
    }
  },
  closeSidebar: (state) => {
    Cookies.set('sidebarStatus', 0)
    state.sidebar.opened = false
  },
  toggleDevice: (state, device) => {
    state.device = device
  },
  setSize: (state, size) => {
    state.size = size
    Cookies.set('size', size)
  },
  setWindowsWidth: (state, width) => {
    if(width > 960){
      state.device = 'desktop'
    } else {
      state.device = 'mobile'
    }
    state.windowsWidth = width
  },
  setAvatar: (state, avatar) => {
    state.avatar = avatar
  }
}

const getters = {
    sidebar: (state) => state.sidebar,
    device: (state)=> state.device,
    size: (state)=> state.size,
    windowsWidth: (state)=> state.windowsWidth,
    lang: (state)=> state.lang,
    avatar: (state)=> state.avatar
}

const actions = {

}

export default {
  namespaced: true,
  state,
  getters,
  mutations,
  actions
}