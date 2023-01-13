<template>
    <el-drawer v-model="drawer" 
        :size="drawerSize"
        :withHeader="false"
        :destroy-on-close="true"
        class="chat-drawer">
        <el-row>
            <el-button link @click="close" style="width: 30px">
                <el-icon color="black"><ArrowLeftBold /></el-icon>
            </el-button>
            <h4>{{ state.Target }}</h4>
        </el-row>
        <div class="chat-list">
            <div class="user" 
                :class="row.From === 'Target' ? 'remote': 'local'" 
                 v-for="(row, index) in messageList" :key="index">
                <div class="avatar">
                    <el-avatar :icon="UserFilled" />
                    <div class="name">{{row.Name}}</div>
                </div>
                <div class="message-box">{{ row.Message }}</div>
            </div>
        </div>
        <el-row class="chat-inputs">
            <el-col :span="isMobile ? 3 : 1">
                <el-button type="primary" link>
                    <el-icon :size="28">
                        <MoreFilled />
                    </el-icon>
                </el-button>
            </el-col>
            <el-col :span="isMobile ? 3 : 1">
                <el-upload
                    v-model:file-list="state.FileList"
                    action=""
                    multiple
                >
                    <el-button type="primary" link>
                        <el-icon :size="28">
                            <Paperclip />
                        </el-icon>
                    </el-button>
                </el-upload>
            </el-col>
            <el-col :span="isMobile ? 3 : 1">
                <Photograph v-model="Photo">
                    <template v-slot="slotProps">
                        <el-button type="primary" link v-on:click="slotProps.click">
                            <el-icon :size="28" >
                                <Camera />
                            </el-icon>
                        </el-button>
                    </template>
                </Photograph>
            </el-col>
            <!-- <el-col :span="2">
                <el-button type="primary" link>
                    <el-icon :size="28">
                        <Location />
                    </el-icon>
                </el-button>
            </el-col> -->
            <el-col :span="isMobile ? 14 : 18">
                <el-input v-model="state.Message">
                    <template #append>
                        <el-button :disabled="state.Message === ''" v-on:click="sendMessage">
                            <el-icon>
                                <Select />
                            </el-icon>
                        </el-button>
                    </template>
                </el-input>
            </el-col>
        </el-row>
    </el-drawer>
</template>
<script>
    import { reactive, computed, ref, onBeforeUpdate, watch } from 'vue'
    import { useStore } from 'vuex'
    import { ArrowLeftBold, Paperclip, Camera, Location, MoreFilled, Select, UserFilled } from '@element-plus/icons-vue'
    import Photograph from './photograph.vue';
    import { ElMessage } from 'element-plus';

    export default({
        props:['modelValue', 'target'],
        emits:['update:modelValue'],
        components:{
            ArrowLeftBold,
            Paperclip,
            Camera,
            Location,
            MoreFilled,
            Select,
            UserFilled,
            Photograph
        },
        setup(props, {emit}){
            const store = useStore()
            const state = reactive({
                Target: 'Test',
                Message: '',
                FileList: [],
                Photo: ''
            })
            const chatForm = ref({})

            const isMobile = computed(() =>{
                return store.getters['app/device'] === 'mobile'
            })

            const drawerSize = computed(() =>{
                if( store.getters['app/device'] === 'mobile'){
                    return '100%'
                } else {
                    return '50%'
                }
            })

            const messageList = computed(() =>{
                return [
                    { 
                        From: 'Target',
                        Avatar: '',
                        Name: 'You',
                        Message: 'Hello~',
                        Images: []
                    },
                    {
                        From: 'Custom',
                        Name: 'Me',
                        Avatar: '',
                        Message: 'Hi',
                        Images: []
                    }
                ]
            })

            const close = () =>{
                drawer.value = false
            }

           
            const drawer = computed({
                get() {
                    return store.getters['chat/showDrawer']
                },
                // setter
                set(newValue) {
                    store.commit('chat/toggleDrawer', newValue)
                }
            })

            
            const sendMessage = () =>{
                ElMessage.warning('暫時不支援')
            }

            return {
                state,
                chatForm,
                drawer,
                close,
                drawerSize,
                messageList,
                isMobile,
                sendMessage
            }
        }
    });
</script>
<style scoped lang="scss">
    .chat-drawer{
        background-color: var(--el-color-info-light-6);
    }
    .chat-list{
        background-color: var(--el-color-info-light-8);
        /*color: white; */
        border: solid 1px black;
        border-radius: 1ch;
        height: 400px;
        padding: 7px 2px 7px 2px;
        box-shadow: 0 0 10px #aaa;
        overflow-y: auto;
    }
    .chat-inputs{
        background-color: var(--el-color-info-light-8);
        border: solid 1px black;
        border-radius: 1ch;
        margin-top: 1em;
        padding-top: 12px;
    }

    .user {
        display: flex;
        align-items: flex-start;
        margin-bottom: 20px;

        .avatar {
            width: 60px;
            text-align: center;
            flex-shrink: 0;
        }

        .message-box {
            background-color: #aaa;
            padding: 16px;
            border-radius: 10px;
            position: relative;
        }
    }
 
    .remote {
        .message-box {
            margin-left: 20px;
            margin-right: 80px;
            color: #eee;
            background-color: #4179f1;
            &::before {
                border-right: 10px solid #4179f1;
                left: -10px;
            }
        }
    }

    .local {
        justify-content: flex-end;
        .message-box {
            margin-right: 20px;
            margin-left: 80px;
            order: -1;
            background-color: #fff;
            color: #333;
            &::before {
                border-left: 10px solid #fff;
                right: -10px;
            }
        }
    }

    .remote,
    .local {
        & .message-box::before {
            content: "";
            position: absolute;
            top: 20px;
            border-top: 10px solid transparent;
            border-bottom: 10px solid transparent;
        }
        .message-box {
            font-weight: 300;
            box-shadow: 0 0 10px #888;
        }
    }
   
</style>