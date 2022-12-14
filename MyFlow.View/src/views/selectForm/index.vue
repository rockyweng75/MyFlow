<template>
  <el-card>
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

import { computed, onBeforeMount } from 'vue'
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
    
    onBeforeMount(() =>{
      store.dispatch('flowchart/getFlowcharts')
    })

    const { t, locale } = useI18n();

    return {
      forms,
      defaultOpeneds,
      t
    }
  },
}
</script>