<template>
  <div class="logout">
      <el-row justify="center" align="middle">
          <!-- <el-col :span="24"> -->
            <h2>您已經成功登出系統!!</h2>
          <!-- </el-col> -->
      </el-row>
      <el-row justify="center" align="middle">
        <el-link href="./login"><h3>重新登入</h3></el-link>
      </el-row>
  </div>
</template>

<script>
import { useStore } from 'vuex'

export default {
    setup() {
        var store = useStore()
        store.dispatch('user/resetToken')
    },
}
</script>
<style scoped>
    .app-wrapper{
        padding-top: 20%;
    }
</style>