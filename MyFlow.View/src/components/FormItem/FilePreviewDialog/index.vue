<template>
    <el-dialog
      v-if="modelValue"
      v-model="modelValue"
      title="預覽"
      width="50%"
      center
      @close="closeDialog(false)"
    >
      <div v-if="fileContentType === 'image/png' || fileContentType === 'image/jpeg'">
        <img
          :src="filePath"
          style="max-width: 100%; height: auto; border: 1px lightgray solid"
        />
      </div>
      <div v-else-if="fileContentType === 'application/pdf'" class="pdf-container">
        <vue-pdf-embed :source="filePath" />
      </div>
      <div v-else>
          <span>沒有此檔案類型的預覽方式</span>
      </div>
    </el-dialog>
  </template>
  
  <script>
  import { reactive, toRefs } from "vue";
  import VuePdfEmbed from "vue-pdf-embed";
  
  export default {
      components:{
          VuePdfEmbed
      },
    props: {
      modelValue: Boolean,
      filePath: String,
      fileContentType: String,
    },
    emits: ["update:modelValue", "onCloseDialog"],
    setup(props, { emit }) {
      const state = reactive({
          filePath: props.filePath,
          fileContentType: props.fileContentType,
      });
  
      const closeDialog = () =>{
          emit("update:modelValue", false);
          emit("onCloseDialog");
      }
  
      return {
        ...toRefs(state),
        closeDialog,
      };
    },
  };
  </script>
  
  <style>
  .el-dialog__header {
      background-color: #e1f5f4
  }
  .pdf-container{
    border: 1px lightgray solid;
  }
  </style>