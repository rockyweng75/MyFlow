<template>
    <div v-on:click="click">
      <el-icon>
        <Delete style="width: 1em; height: 1em; margin-top: 8px;"></Delete>
      </el-icon>
      清除暫存
    </div>
</template>

<script>
import Delete from '@element-plus/icons/lib/Delete'
import { ElMessage } from 'element-plus'

export default {
  name: 'logout',
  components:{
    Delete
  },
  methods: {
    click() {
        localStorage.clear();
        sessionStorage.clear();
        ElMessage.success('success');
    },
  }
}
</script>

<style scoped>
</style>
