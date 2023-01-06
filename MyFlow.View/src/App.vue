<template>
  <router-view v-if="isRouterAlive"/>
</template>

<script>
import { provide, ref, nextTick } from 'vue'
import { useStore } from 'vuex'
export default {
  name: 'App',
  setup(){
    const isRouterAlive = ref(true)
    const reload = () =>{
      isRouterAlive.value = false;
      nextTick( ()=> { isRouterAlive.value = true } ) ;
    }
    provide('reload', reload)

    // const store = useStore()
    // store.dispatch('websocket/initWebSocket')

    return {
      isRouterAlive,
      reload,
    }
  }
}
</script>
