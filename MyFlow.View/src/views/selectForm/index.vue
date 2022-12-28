<template>
  <el-card v-loading="loading">
    <template #header>
      選擇申請表單
    </template>
    <el-menu 
      v-if="forms.length != 0"
      :default-openeds="defaultOpeneds" 
      router
      class="link-menu">
      <template v-for="form in forms">
        <template v-if="form.children">
          <el-sub-menu :index="form.name" :key="form.name">
            <template #title>
              <i class="el-icon-circle-plus-outline"></i>
              <span>{{form.name}}</span>
            </template>
            <el-menu-item :index="'/applyForm/' + item.id" v-for="item in form.children" :key="item.name">
              <template #title>
                <div class="link">
                  <i class="el-icon-caret-right"></i>
                  <span>{{item.name}}</span>
                </div>
              </template>
            </el-menu-item>
          </el-sub-menu>
        </template>
        <template v-else>
          <el-menu-item :index="'/applyForm/' + form.id" :key="form.name">
            <template #title>
              <div class="link">
                <i class="el-icon-caret-right"></i>
                <span>{{form.name}}</span>
              </div>
            </template>
          </el-menu-item>
        </template>
      </template>
    </el-menu>
    <div v-else>
      <el-empty description="暫時無開放表單" />
    </div>
  </el-card>
</template>

<script>

import { computed, onBeforeMount, ref } from 'vue'
import { useStore } from 'vuex'
import { useI18n } from "vue-i18n";

export default {
  setup() {
    const store = useStore()
    const forms = computed(()=>{
      return store.getters['flowchart/flowList']
    })

    const defaultOpeneds = computed(()=>{
      return store.getters['flowchart/defaultOpeneds']
    })
    const loading = ref(true)

    onBeforeMount(async() =>{
      await store.dispatch('flowchart/getFlowcharts')
      loading.value = false
    })

    const { t, locale } = useI18n();

    return {
      forms,
      defaultOpeneds,
      t,
      loading
    }
  },
}
</script>
<style scoped>
  .link-menu {
    border-right: 3px solid var(--el-color-success);
    border-left: 3px solid var(--el-color-success);
  }

  .link-menu .el-menu-item {
    border-bottom: 1px solid var(--el-border-color);
    border-right: 0px;
  }

  .link-menu .el-sub-menu {
    border-bottom: 1px solid var(--el-border-color);
    background-color: var(--el-fill-color-light);
  }

  .link-menu .el-sub-menu.is-opened {
    border-bottom: 0px;

  }

  .link-menu .el-sub-menu.is-opened .el-menu-item {
    border-left: 4px solid var(--el-color-success);
    border-right: 4px solid var(--el-color-success);
  }


  .link-menu .el-sub-menu.is-opened >>> .el-sub-menu__title {
    border-bottom: 1px solid var(--el-border-color);
  }
</style>