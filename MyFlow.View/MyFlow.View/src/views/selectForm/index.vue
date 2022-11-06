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
      <template v-for="form in forms" v-bind:key="form.name">
        <el-sub-menu :index="form.name" v-if="form.children" >
          <template #title>
            <i class="el-icon-circle-plus-outline"></i>
            <span>{{t(form.name)}}</span>
          </template>
          <template v-for="item in form.children" v-bind:key="item.name">
            <el-menu-item :index="'/applyForm/' + item.id">
              <template #title>
                <div class="link">
                  <i class="el-icon-caret-right"></i>
                  <span>{{item.name}}</span>
                </div>
              </template>
            </el-menu-item>
          </template>
        </el-sub-menu>
        <el-menu-item v-else :index="'/applyForm/' + form.id">
          <template #title>
            <div class="link">
              <i class="el-icon-caret-right"></i>
              <span>{{t(form.name)}}</span>
            </div>
          </template>
        </el-menu-item>
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
      return store.getters['flowchart/list']
    })

    const defaultOpeneds = computed(()=>{
      return store.getters['flowchart/defaultOpeneds']
    })
    
    onBeforeMount(() =>{
      store.dispatch('flowchart/getList')
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