<template>
  <div :class="{'has-logo':showLogo}">
    <!-- <logo v-if="showLogo" :collapse="isCollapse" /> -->
    <el-scrollbar wrap-class="scrollbar-wrapper">
      <el-menu
        :default-active="activeMenu"
        :collapse="isCollapse"
        :background-color="variables.menuBg"
        :default-openeds="['/workboard']"
        :text-color="variables.menuText"
        :unique-opened="false"
        :active-text-color="variables.menuActiveText"
        :collapse-transition="false"
        mode="vertical"
      >
        <SidebarItem v-for="route in permission_routes" :key="route.path" :item="route" :base-path="route.path" />
      </el-menu>
    </el-scrollbar>

  </div>
</template>

<script>
import { mapGetters } from 'vuex'
import Logo from './Logo.vue'
import variables from '/@/styles/variables.scss'
import SidebarItem from './SidebarItem.vue'

export default {
  components: { SidebarItem },
  computed: {
    ...mapGetters({
      permission_routes: 'security/routes',
      sidebar: 'app/sidebar'
    }),
    activeMenu() {

      const route = this.$route
      const { meta, path } = route
      // if set path, the sidebar will highlight the path you set
      if (meta.activeMenu) {
        return meta.activeMenu
      }
      return path
    },
    defaultOpeneds() {
      return this.$store.getters['settings/defaultOpeneds']
    },
    showLogo() {
      return this.$store.getters['settings/sidebarLogo']
    },
    variables() {
      return variables
    },
    isCollapse() {
      return !this.sidebar.opened
    }
  }
}
</script>

<style lang="scss" scoped>
  // .sidebar-container{
  //   width: 180px;
  // }
</style>
