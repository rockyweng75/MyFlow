<template>
    <div v-on:click="click">
      <el-icon>
        <SwitchButton style="width: 1em; height: 1em; margin-top: 8px;"></SwitchButton>
      </el-icon>
      登出
    </div>
</template>

<script>
import SwitchButton from '@element-plus/icons/lib/SwitchButton'

export default {
  name: 'logout',
  components:{
    SwitchButton
  },
  methods: {
    click() {
        this.$router.push("/logout")

      // this.$store("user/logout").then(()=>{
      // });
    },
  }
}
</script>

<style scoped>
</style>
