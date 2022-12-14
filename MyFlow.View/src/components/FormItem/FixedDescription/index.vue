<template>
    <el-card style="margin-bottom:30px;white-space: pre-wrap">
      <ol v-if="Array.isArray(msgs)">
          <template v-for="(_key,index) in msgs" :key="index">
            <li v-if="typeof _key === 'string'" v-html="_key"></li>
            <li v-if="typeof _key === 'object'">
              <div v-html="_key.label"></div>
              <template v-if="_key.children">
                <ol class="child">
                  <li v-for="_child in _key.children" :key="_child" v-html="_child.label"></li>
                </ol>
              </template>
            </li>
          </template>
      </ol>
      <div v-html="modelValue.ItemDesc" v-else></div>
    </el-card>
  </template>
  
  <script>
  import { ref, onBeforeMount, onBeforeUpdate } from 'vue'
  export default {
    props:{
      modelValue: { type: Object },
    },
    setup(props) {
      const msgs = ref({})
      onBeforeMount(()=>{
        if(props.modelValue.ItemFormat === 'validJson'){
          msgs.value = JSON.parse(props.modelValue.ItemDesc)
        }
      })
      onBeforeUpdate(()=>{
        if(props.modelValue.ItemFormat === 'validJson'){
          msgs.value = JSON.parse(props.modelValue.ItemDesc)
        }
      })
      return {
        msgs
      };
    },
  };
  </script>
  <style scoped>
    ol {
      list-style-type: cjk-ideographic;
    }
  
    ol ol{
      list-style-type: decimal;
    }
  </style>>