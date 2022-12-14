<template>
    <ul>
      <li v-for="item in modelValue" v-bind:key="item.response">
        <el-link type="primary" :href="fileApiBasePath + item.response" target="_blank">{{ item.name }}</el-link>
      </li>
    </ul>
  </template>
  
  <script>
  import {reactive, toRefs} from "vue"
  import {ElLink} from "element-plus"
  
  export default {
      components:{
          ElLink
      },
      // 進來的 modelValue 需是物件陣列
      props:{
          modelValue: { type: Array },
      },
      setup(props) {
          const state = reactive({
              // fileList: [...props.modelValue], // 解構 props.modelValue
              fileApiBasePath: import.meta.env.VITE_API_BASE_URL + "/api/FileStorage/",
          });
  
          return {
          ...toRefs(state),
          };
      },
  };
  </script>
  
  <style>
  </style>