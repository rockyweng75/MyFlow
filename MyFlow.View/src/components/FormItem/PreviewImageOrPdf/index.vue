<template>
    <el-form-item :label="label" :prop="prop" class="image-list">
       <template
         v-for="item in modelValue" 
         :key="item.response" >
   
         <vue-pdf-embed 
           v-if="item.name.split('.').pop() === 'pdf'"
           :source="filePath + item.response" 
         >
         </vue-pdf-embed>
         <el-image 
           v-else
           style="width: 300px; height: 300px"
           :label="item.name"
           :src="filePath + item.response" 
           lazy />
   
       </template>
   
    </el-form-item>
   
     <!-- <el-form-item :label="label" :prop="prop">
       <a :href="filePath" target="_blank" class="el-button" style="margin-right:15px" >另開分頁檢視</a>
       <el-button type="primary" style="margin-bottom: 15px" @click="openPreviewDialog">另開視窗檢視</el-button>
       <div
         v-if="contentType === 'application/pdf'"
       >        
           <div class="pdf-container-now-page">
               <vue-pdf-embed :source="filePath" />
           </div>
         
       </div>
       <div
         v-else-if="contentType === 'image/png' || contentType === 'image/jpeg'"      
         style="padding: 10px 10px"
       >
         <el-image
           style="width: 80%; height: auto"
           :src="filePath"
           :preview-src-list="[filePath]"
           :initial-index="1"
         >
         </el-image>
       </div>
       <div v-else>
           <span>無此檔案，或此檔案格式無法預覽</span>
       </div>    
     </el-form-item>
     <file-preview-dialog 
           :filePath="filePath"
           :fileContentType="contentType"
           :isDialogVisible="isPreviewDialogVisible"
           @onCloseDialog="closePreviewDialog"
       /> -->
   </template>
   
   <script>
   import { reactive, toRefs, onBeforeMount } from "vue";
   import { ElMessage } from "element-plus";
   import VuePdfEmbed from "vue-pdf-embed";
   // import FilePreviewDialog from "/@/components/FilePreviewDialog/filePreviewDialog.vue";
   
   // function useHandlePreviewDialog(state){
   //     const openPreviewDialog = () =>{
   //         state.isPreviewDialogVisible = true;
   //         console.log("clicked");
   //     }
   //     return {
   //         openPreviewDialog,
   //     }
   // }
   
   export default {
     components: {
       VuePdfEmbed,
       // FilePreviewDialog
     },
     // props: ["modelValue", "label", "prop"],
     props:{
       modelValue: { type: Array },
       label: { type: String },
       prop: { type: String },
     },
     setup(props) {
       const state = reactive({
         // fileId: props.modelValue ? props.modelValue.fileId : "",
         // contentType: "",
         filePath: import.meta.env.VITE_API_BASE_URL + '/api/FileStorage/',
         // isPreviewDialogVisible: false,
       });
   
       // const closePreviewDialog = (visible) => {
       //     state.isPreviewDialogVisible = visible;
       // }
   
       return {
         ...toRefs(state),
         // ...useHandlePreviewDialog(state),
         // closePreviewDialog,
       };
     },
   };
   </script>
   
   <style scoped>
   .pdf-container-now-page{
       height: 65vh;
       width: 80%; 
       overflow:auto;
       border: 1px lightgray solid;
   }
   .image-list{
     height: 300px;
     overflow-y: auto;
   }
   </style>