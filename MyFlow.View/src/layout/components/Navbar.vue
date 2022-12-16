<template>
  <div class="wnavbar">
    <hamburger id="hamburger-container" :is-active="sidebar.opened" class="hamburger-container" @toggleClick="toggleSideBar" />

    <div class="right-menu">
        <ClearCache class="right-menu-item hover-effect" />
        <SelectLang class="right-menu-item hover-effect" />
        <Logout class="right-menu-item hover-effect" />
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
import Hamburger from '../components/Hamburger/index.vue'
import Logout from '../components/Logout/index.vue'
import SelectLang from '../components/SelectLang/index.vue'
import ClearCache from '../components/ClearCache/index.vue'

export default {
  components: {
    Hamburger,
    Logout,
    SelectLang,
    ClearCache
  },
  computed: {
    ...mapGetters({
      sidebar: 'app/sidebar',
      avatar: 'app/avatar',
      device: 'app/device'
    })
  },
  methods: {
    toggleSideBar() {
      this.$store.commit('app/toggleSidebar')
    },
    // async logout() {
    //   await this.$store.dispatch('user/logout')
    //   this.$router.push(`/login?redirect=${this.$route.fullPath}`)
    // }
  }
}
</script>

<style lang="scss" scoped>

  .wnavbar {
    width: 100%;
    height: 50px;
    overflow: hidden;
    position: relative;
    background: #2da7b8 !important;
    box-shadow: 0 2px 2px 0 rgba(0, 0, 0, 0.14), 0 3px 1px -2px rgba(0, 0, 0, 0.2), 0 1px 5px 0 rgba(0, 0, 0, 0.12);
    margin-bottom: 2px;

    .hamburger-container {
      line-height: 46px;
      height: 100%;
      float: left;
      cursor: pointer;
      transition: background .3s;
      -webkit-tap-highlight-color:transparent;
      &:hover {
        background: #159178
      }
    }

  .errLog-container {
    display: inline-block;
    vertical-align: top;
  }

  .right-menu {
    float: right;
    height: 100%;
    line-height: 50px;

    &:focus {
      outline: none;
    }

    .right-menu-item {
      display: inline-block;
      padding: 0 8px;
      height: 100%;
      font-size: 18px;
      font-weight: 600;
      color: #fff;
      vertical-align: text-bottom;

      &.hover-effect {
        cursor: pointer;
        transition: background .3s;
        &:hover {
          background: #159178
        }
      }
    }

    .avatar-container {
      margin-right: 30px;

      .avatar-wrapper {
        margin-top: 5px;
        position: relative;

        .user-avatar {
          cursor: pointer;
          width: 40px;
          height: 40px;
          border-radius: 10px;
        }

        .el-icon-caret-bottom {
          cursor: pointer;
          position: absolute;
          right: -20px;
          top: 25px;
          font-size: 12px;
        }
      }
    }
  }
}
</style>
