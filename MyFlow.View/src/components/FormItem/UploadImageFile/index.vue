<template>
    <div>
      <el-upload
        v-model:file-list="fileList"
        ref="uploadRef"
        :action="uploadPath"
        accept="image/png, image/jpeg, application/pdf"
        :limit="limitFileNumber"
        :auto-upload="false"
        :on-change="handleChange"
        :on-preview="handleOnPreview"
        :on-exceed="handleExceed"
        :on-success="handleSuccess"
        :on-error="handleError"
        :disabled="disabled"
        :multiple="isAllowMultiple"
        drag
      >
        <div class="uploader-dragger-container">
          <el-icon><UploadFilled /></el-icon>
          <div class="el-upload__text">拖曳檔案至此，或 <em>選擇檔案</em></div>
        </div>
  
        <!-- <el-button type="primary">選擇檔案</el-button> -->
        <template #tip>
          <div class="el-upload__tip">
            檔案格式僅接受 pdf、jpg、jpeg、png；可選擇 {{limitFileNumber}} 個檔案上傳 (每個 {{limitFileSize}}MB 以內)
          </div>
        </template>
      </el-upload>
  
      <file-preview-dialog
        v-if="isFilePreviewDialogVisible"
        :filePath="currentFilePath"
        :fileContentType="fileContentType"
        :isDialogVisible="isFilePreviewDialogVisible"
        @onCloseDialog="closeFilePreviewDialog"
      />
    </div>
  </template>
  
  <script>
  import { reactive, toRefs, ref } from "vue";
  import { useStore } from "vuex";
  import { UploadFilled } from "@element-plus/icons-vue";
  import { ElMessage } from "element-plus";
  import FilePreviewDialog from "../FilePreviewDialog/index.vue";
  
  
  function useHandleExceed(uploadRef, state) {
    const handleExceed = (fileList) => {
  
      const numberOfFile = fileList.length;
      const { limitFileNumber } = state;
      if (limitFileNumber == 1){ // 如果只限上傳一個檔案，則刷新list (清空原list並定位到新上傳這個檔案)
        uploadRef.value.clearFiles(); //官網範例 ==> 不用寫參數.....
        //   uploadRef.value.clearFiles(['ready']); // 在1.2.0-beta6實測 ==> 要加參數才能清空畫面上的list顯示
        uploadRef.value.handleStart(fileList[0]); // 把畫面的list重新定位到第一個檔案
      }else{
        if (numberOfFile > limitFileNumber){
          ElMessage.warning("檔案數量超過上限，請重新操作");
          uploadRef.value.clearFiles();
        }      
      }
      
    };
    return {
      handleExceed,
    };
  }
  
  function useHandleOnPreview(state) {
    const handleOnPreview = (file) => {
      // console.log("file.raw:", file.raw);
  
      const contentType = file.raw.type;
      var binaryData = [];
      binaryData.push(file.raw);
      const filePath = window.URL.createObjectURL(
        new Blob(binaryData, { type: contentType })
      );
      state.currentFilePath = filePath;
      state.fileContentType = contentType;
      state.isFilePreviewDialogVisible = true;
    };
    return {
      handleOnPreview,
    };
  }
  
  export default {
    components: {
      UploadFilled,
      FilePreviewDialog,
    },
    props: ["name", "disabled", "modelValue"],
    props:{
      disabled: { type: Boolean },
      modelValue: { type: Object }
    },
    emits: ["update:modelValue"],
    setup(props, { emit }) {
      const store = useStore();
      const uploadRef = ref();
      const state = reactive({
        uploadPath: import.meta.env.VITE_API_BASE_URL + "/api/FileStorage/TempSave",   
        fileList: [],
        currentFilePath: "",
        fileContentType: "",
        limitFileSize: 100, // MB
        limitFileNumber: 5, // 考慮由外部來設定上限值，且聯動更改 isAllowMultiple
        isAllowMultiple: true,
        isFilePreviewDialogVisible: false,
      });
  
      const closeFilePreviewDialog = (visible) => {
        state.isFilePreviewDialogVisible = false;
      };
  
      const success = ref(false)
      const uploading = ref(false)
  
      const upload = () => {
        return new Promise((resolve, reject) => {
          uploading.value = true;
          uploadRef.value.submit()
          let index = 0;
          let intervalId = setInterval(()=>{
            index++;
            uploading.value = !state.fileList.every(o => o.response);
            if(!uploading.value || index > 1000){
              window.clearInterval(intervalId)
              success.value ? resolve() : reject()
            }
          },10);
        });
      };
  
      const commit = () => {
        try{
          uploadRef.value.handleStart(uploadFiles =>{
            store.dispatch('fileStorage/confirm', uploadFiles)
          })
        }
        finally{}
      };
  
      const rollback = () => {
        try{
          uploadRef.value.handleStart(uploadFiles =>{
            store.dispatch('fileStorage/cancel', uploadFiles)
          })
        }
        finally{}
      };
  
      const handleError = () =>{
        success.value = false
        uploading.value = false;
        ElMessage.error('檔案上傳失敗')
        uploadRef.value.clearFiles();
      }
  
      const handleSuccess = (res) =>{
        success.value = true
        uploading.value = false;
      }
  
      const handleChange = (file) =>{
        const fileSize = file.size / 1024 / 1024; // MB
  
        const { limitFileSize } = state;
        if (fileSize > limitFileSize) {
          ElMessage.warning("檔案須為 " + limitFileSize + "MB以內");
          uploadRef.value.clearFiles();
        }
        emit("update:modelValue", state.fileList);
      }
  
      return {
        ...toRefs(state),
        uploadRef,
        ...useHandleExceed(uploadRef, state),
        ...useHandleOnPreview(state),
        handleSuccess,
        handleError,
        handleChange,
        closeFilePreviewDialog,
        upload,
        success,
        commit,
        rollback
      };
    },
  };
  </script>
  
  <style>
  .uploader-dragger-container {
    height: 180px;
    padding: 40px 0;
  }
  
  .uploader-icon {
    font-size: 55px;
    color: #8c939d;
    width: 60px;
    height: 60px;
    text-align: center;
  }
  </style>