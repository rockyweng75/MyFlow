<template>
  <el-card v-loading="loading">
    <template #header>
      <el-row justify="space-between">
        <el-col :span="12">
          選擇申請表單
        </el-col>
        <el-col :span="12" style="text-align:end">
          <el-button type="success" plain round v-on:click="toCreate">新增</el-button>
        </el-col>
      </el-row>  
    </template>
    <el-menu 
      v-if="forms.length != 0"
      :default-openeds="defaultOpeneds" 
      router
      class="link-menu">
      <template v-for="form in forms" v-bind:key="form.name">
        <el-sub-menu :index="form.name" v-if="form.children" >
          <template #title>
            <i class="el-icon-circle-plus-outline"></i>
            <span>{{form.name}}</span>
          </template>
          <template v-for="item in form.children" v-bind:key="item.name">
            <el-menu-item :index="'/formManage/edit/' + item.id">
              <template #title>
                <div class="link">
                  <i class="el-icon-caret-right"></i>
                  <span>{{item.name}}</span>
                </div>
              </template>
            </el-menu-item>
          </template>
        </el-sub-menu>
        <el-menu-item v-else :index="'/formManage/edit/' + form.id">
          <template #title>
            <div class="link">
              <i class="el-icon-caret-right"></i>
              <span>{{form.name}}</span>
            </div>
          </template>
        </el-menu-item>
      </template>
    </el-menu>
    <div v-else>
      <el-empty description="暫無表單" />
    </div>
  </el-card>
</template>

<script>

import { computed, onBeforeMount, ref } from 'vue'
import { useStore } from 'vuex'
import { useRouter } from 'vue-router'

export default {
  setup() {
    const store = useStore()
    const forms = computed(()=>{
      return store.getters['form/forms']
    })

    const defaultOpeneds = computed(()=>{
      return store.getters['form/defaultOpeneds']
    })
    
    const loading = ref(true)
    onBeforeMount(async() =>{
      await store.dispatch('form/getForms')
      loading.value = false
    })

    const router = useRouter()
    const toCreate = ()=>{
      router.push('/formManage/create')
    }
    
    return {
      forms,
      defaultOpeneds,
      toCreate,
      loading
    }
  },
}
</script>

<style scoped>
  .link-menu {
    border-right: 0px;
    border-left: 3px solid var(--el-color-primary-light-7);
  }

  .link-menu .el-menu-item {
    border-bottom: 1px solid var(--el-color-primary-light-7);
    border-right: 0px;
  }

  .link-menu .el-sub-menu {
    border-bottom: 1px solid var(--el-color-primary-light-7);

  }

  .link-menu .el-sub-menu.is-opened {
    border-bottom: 0px;
  }

  .link-menu .el-sub-menu.is-opened .el-menu-item {
    border-left: 4px solid var(--el-color-primary-light-7);
  }


  .link-menu .el-sub-menu.is-opened >>> .el-sub-menu__title {
    border-bottom: 1px solid var(--el-color-primary-light-7);
  }
</style>