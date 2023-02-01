import { createRouter, createWebHistory, createWebHashHistory } from "vue-router"
import constantRoutes from "./constantRoutes"
import asyncRoutes from "./asyncRoutes"

const base = import.meta.env.VITE_BASE
const router = createRouter({
    history: createWebHistory(base),
    routes: [...constantRoutes, ...asyncRoutes ]
})

export function resetRouter() {
    location.reload()
    // const resetWhiteNameList = ['Redirect', 'Login', 'NoFind', 'Root']
    // router.getRoutes().forEach((route) => {
    //   const { name } = route
    //   if (name && !resetWhiteNameList.includes(name as string)) {
    //     router.hasRoute(name) && router.removeRoute(name)
    //   }
    // })
}

export default router
