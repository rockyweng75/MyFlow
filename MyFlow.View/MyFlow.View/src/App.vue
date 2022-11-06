<template>
  <router-view v-if="isRouterAlive"/>
</template>

<script>
import { provide, ref, nextTick } from 'vue'

export default {
  name: 'App',
  setup(){
    const isRouterAlive = ref(true)
    const reload = () =>{
      isRouterAlive.value = false;
      nextTick( ()=> { isRouterAlive.value = true } ) ;
    }
    provide('reload', reload)

    return {
      isRouterAlive,
      reload,
    }
  }
}
</script>
