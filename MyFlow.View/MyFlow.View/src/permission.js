import router from './router'
import store from './store'
import { ElMessage } from 'element-plus'
import NProgress from 'nprogress' // progress bar
import 'nprogress/nprogress.css' // progress bar style
import { getTokenAsync } from '@/cookies' // get token from cookie
import getPageTitle from '@/utils/get-page-title'

NProgress.configure({ showSpinner: false }) // NProgress Configuration

const whiteList = ['login', 'auth-redirect', 'logout', '401', '404'] // no redirect whitelist

router.beforeEach(async(to, from) => {
  // start progress bar
  NProgress.start()
  // set page title
  document.title = getPageTitle(to.meta.title)

  // determine whether the user has logged in
  var token = store.getters['security/token'];
  var isAuthenticated = store.getters['security/token'] && store.getters['security/token'] !== ''

  if (whiteList.indexOf(to.name) >= 0){
    return true
  } else if (to.name !== 'login' && !isAuthenticated) {
     return '/login?redirect=' + to.name
  } else {
    try{
      let user = store.getters['security/user'];

      if(!user.Roles){
        await store.dispatch('security/getUserinfo')
      } 
      let roles  = store.getters['security/roles']
      if(roles !== null && roles.length > 0){
        const accessedRoutes = await store.dispatch('security/generateRoutes', roles)
        accessedRoutes.forEach(r => {
          router.addRoute(r)
        })
        if( to.meta && to.meta.roles){
          if(roles.some(o => to.meta.roles.indexOf(o) >= 0)){
            return true
          } else {
            return '/401'
          }
        } else {
          return true
        }
      } else {
        return '/401'
      }
    } catch(e){
      console.log(e)
      return '/404'
    }
  

  }

})

router.beforeResolve((to, from) =>{
});


router.afterEach(() => {
  // finish progress bar
  NProgress.done()
})
