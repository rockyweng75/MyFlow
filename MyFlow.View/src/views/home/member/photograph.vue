<template>
    <slot :click="clickEvent"/>
    <el-dialog 
        v-model="dialogVisible" 
        width="100%" 
        :before-close="handleClose">
        <template v-if="dialogVisible">
            <video
                :hidden="!videoVisable"
                width="320" 
                height="240" 
                autoplay 
                ref="video" >
            </video>
            <canvas
                :hidden="!canvasVisable"
                ref="canvas">
            </canvas>
            <el-button type="warning" v-on:click="catchPhoto" v-if="!hasCatch">拍照</el-button>
            <template v-else>
                <el-button type="success" v-on:click="confirm">確定</el-button>
                <el-button type="danger" v-on:click="reset">重拍</el-button>
            </template>

        </template>
    </el-dialog>
</template>
<script>

    import { reactive, computed, ref, onBeforeUpdate, watch, onBeforeUnmount } from 'vue'
    import { useStore } from 'vuex'
    import { ElMessage } from 'element-plus'

    // import { ArrowLeftBold, Paperclip, Camera, Location, MoreFilled, Select, UserFilled } from '@element-plus/icons-vue'

    export default({       
        props:['modelValue'],
        emits:['update:modelValue'],
        setup(props, {emit}){

            const dialogVisible = ref(false)

            const clickEvent = ()=>{
                dialogVisible.value = true
                openCamera()
            }

            const handleClose = () =>{
                closeCamera()
                dialogVisible.value = false
            }

            const video = ref({})
            const videoVisable = ref(true)
            const canvas = ref({})
            const canvasVisable = ref(false)
            const hasCatch = ref(false)

            const constraints = { //相機限制
                audio: false,
                video: {
                    facingMode: "user",  //開前鏡頭
                    // frameRate: { ideal: 10, max: 15 } }
                }
            };

            const ctx = ref({})
            const closeCamera= ()=>{
                if(video.value.srcObject){
                    video.value.srcObject.getTracks().forEach(function(track) {
                        track.stop();
                    });
                }
            }
            const openCamera = ()=>{
                navigator.mediaDevices
                    .getUserMedia(constraints)
                    .then(function success(stream) {
                        video.value.srcObject = stream;
                    })
                    .catch((e)=>{
                        console.error(e)
                        ElMessage.error('無法開啟攝影機');
                    });

            }

            const catchPhoto = ()=>{
                
                const width = 320;
                const height = 240;
                canvas.value.width = width;
                canvas.value.height = height;
                ctx.value = canvas.value.getContext("2d");
                ctx.value.drawImage(video.value, 0, 0, width, height);  // 每 16 毫秒將攝影機畫面「印」至 canvas
                videoVisable.value = false
                canvasVisable.value = true
                hasCatch.value = true
                closeCamera();
            }

            const confirm = () =>{
                hasCatch.value = false
                const data = canvas.value.toDataURL("image/png");
                emit('update:modelValue', data)
                dialogVisible.value = false
            }

            const reset = () =>{
                openCamera()
                hasCatch.value = false
                videoVisable.value = true
                canvasVisable.value = false
            }

            return {
                dialogVisible,
                clickEvent,
                handleClose,
                openCamera,
                closeCamera,
                catchPhoto,
                video,
                videoVisable,
                canvas,
                canvasVisable,
                hasCatch,
                confirm,
                reset
            }
        }
    })
</script>