<template>
  <div class="top-wrapper">
    <el-dropdown trigger="click" class="top-drop">
      <div class="top-drop-label">
        中文/English
      </div>
      <template #dropdown>
        <el-dropdown-item  @click="click('zh-tw')">
          中文
        </el-dropdown-item>
        <el-dropdown-item  @click="click('en-us')">
          English
        </el-dropdown-item>
      </template>
    </el-dropdown> 
  </div>
</template>

<script>
import { computed, watch, onBeforeMount, getCurrentInstance, inject } from 'vue'

import { useStore } from 'vuex'
import { ElMessage } from 'element-plus'
import { useI18n } from 'vue-i18n'
import zh from "/@/lang/zh-TW.json";
import en from "/@/lang/en-US.json";

export default {
  name: 'logout',
  setup(){
    const store = useStore()
    const click = (lang)=>{
      store.dispatch("app/ChangeLang", lang)
      .then(()=>{
        ElMessage('尚未開放! not ready!');
      });
    }
    const lang = computed(()=>{
      return store.getters['app/lang']
    })

    const moment = getCurrentInstance().appContext.config.globalProperties.$moment;

    // const { locale } = useI18n({
    //   messages: {
    //     "zh-tw": zh,
    //     "en-us": en,
    //   }
    // })

    onBeforeMount(()=>{
      moment.locale(lang.value)
      // locale.value = lang.value
    })

    const reload = inject("reload")
    watch(lang, (newVal)=>{
      reload()
    })
    return {
      click,
      lang
    }
  }

}
</script>

<style lang="scss" scoped>
  .top-drop{
    vertical-align: initial;
  }
  .top-drop-label{
    font-size: 18px;
    color: #fff;
  }
</style>
