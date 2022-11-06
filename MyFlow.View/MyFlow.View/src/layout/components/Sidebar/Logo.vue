<template>
  <div class="sidebar-logo-container" :class="{'collapse': collapse}">
    <transition-group name="sidebarLogoFade">
      <div v-if="collapse" key="collapse">
          <router-link class="sidebar-logo-link" to="/">
            <img v-if="logo" :src="logo" class="sidebar-logo">
          </router-link>
      </div>
      <div v-else key="expand">
        <div class="logo">
          <router-link class="sidebar-logo-link" to="/">
            <div class="row">
              <img v-if="logo" :src="logo" class="sidebar-logo">
              <h2 class="sidebar-title">{{ title }} </h2>
            </div>
          </router-link>
        </div>
        <el-dropdown trigger="click" class="sidebar-name">
          <div>
            {{userName}}
            <el-icon>
              <CaretBottom />
            </el-icon>
          </div>
          <template #dropdown>
          </template>
        </el-dropdown> 
      </div>
    </transition-group>
  </div>
</template>

<script>

import { mapGetters } from 'vuex'
import CaretBottom from '@element-plus/icons/lib/CaretBottom'

export default {
  name: 'SidebarLogo',
  components:{
    CaretBottom
  },
  props: {
    collapse: {
      type: Boolean,
      required: true
    }
  },
  watch:{
    collapse(){
      console.log('collapse' + this.collapse);
    }
  },
  computed:{
    ...mapGetters({
      userName: 'security/userName'
    })
  },
  data() {
    return {
      logo: '#',
      title: 'MyFlow',
    }
  }
}
</script>

<style lang="scss" scoped>
.sidebarLogoFade-enter-active {
  transition: opacity 1.5s;
}

.sidebarLogoFade-enter,
.sidebarLogoFade-leave-to {
  opacity: 0;
}

.sidebar-logo-container {
  position: relative;
  width: 100%;
  height: 165px;
  background: #1ab394 !important;
  overflow: hidden;
  box-shadow: 0 1px 1px -1px rgba(0, 0, 0, 0.34), 0 0 6px 0 rgba(0, 0, 0, 0.14);
  margin-bottom: 3px;

  & a{
    text-decoration: none;
  }

  & .logo{
      & .sidebar-logo-link {
        width: 100%;

        & .sidebar-logo {
          width: 32px;
          height: 32px;
          margin-top: 30px;      
          margin-left: 40px;      
          &:hover {
            background: #159178
          }
        }
      
        & .sidebar-title {
          display: inline-block;
          color: #fff;
          font-weight: 600;
          line-height: 50px;
          margin-top: 20px;      
          vertical-align: middle;  
          &:hover {
            background: #159178;
            text-decoration: none;
          }
        }
      }

  }
  
  & .sidebar-name {
    display: inline-block;
    margin: 0;
    color: #fff;
    font-weight: 600;
    line-height: 30px;
    font-size: 14px;
    font-family: Avenir, Helvetica Neue, Arial, Helvetica, sans-serif;
    margin-bottom: 30px;
    margin-left: 30px;
  }
}
</style>
