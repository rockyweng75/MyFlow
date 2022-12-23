import { createRouter, createWebHistory, createWebHashHistory } from "vue-router"
import constantRoutes from "./constantRoutes"
// import asyncRoutes from "./asyncRoutes"

const base = import.meta.env.VITE_BASE
const router = createRouter({
    history: createWebHashHistory(base),
    routes: [...constantRoutes ]
})

export function resetRouter() {
    location.reload()
}

export default router
