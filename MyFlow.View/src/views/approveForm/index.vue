<template>
    <el-row class="block" justify="space-between">
        <el-col :span="12">
            <el-button-group>
                <template v-for="(actionForm, index) in actionForms" :key="index">
                    <el-button :type="getButtonType(actionForm)" @click="hanldClick(actionForm)">
                        {{actionForm.ButtonName}}
                    </el-button>
                </template>
            </el-button-group>
        </el-col>
        <el-col :span="12" style="text-align:end" >
            <el-button type="warning" @click="comeback()">返回</el-button>
        </el-col>
    </el-row>
    <Details v-model="state.dialogVisible" :title="state.actionTitle">
        <ApproveForm :stageid="state.stageid" :action="state.action">
            <template v-slot:button="slotProps">
                <el-divider></el-divider>
                <el-button-group>
                    <el-button type="primary" @click="submit(slotProps.form, slotProps.file, slotProps.data)">送出</el-button>
                    <el-button type="danger" @click="cancel()">取消</el-button>
                </el-button-group>
            </template>        
        </ApproveForm>
    </Details>
    <ApplyForm class="disabled-form" :flowid="state.flowid" :disabled="true" v-if="!state.loading">
    </ApplyForm>
    <ProcessHistory :applyid="state.applyid" :apprid="state.apprid" :hidden="state.apprid" :disabled="true" v-if="!state.loading"/>  
</template>

<script>

import ApplyForm from "@/components/ApplyForm/index.vue"
import ApproveForm from "@/components/ApproveForm/index.vue"
import Details from "./details.vue"
import { ElMessage } from 'element-plus'
import { reactive, onBeforeMount, computed, defineAsyncComponent} from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { useStore } from 'vuex'

export default{
    components:{
        ApplyForm,
        ProcessHistory: defineAsyncComponent({
            loader: () => import("@/components/ProcessHistory/index.vue"),
            delay: 200,
        }),
        ApproveForm,
        Details
    },
    props:['flowid', 'disabled'],
    setup(){
        const route = useRoute()
        const router = useRouter()
        const state = reactive({
            stageid: route.params.stageid,
            flowid: route.params.flowid,
            applyid: route.params.applyid,
            apprid: route.params.apprid,
            dialogVisible: false,
            action:'',
            actionTitle: '',
            loading: true
        })

        const store = useStore();

        onBeforeMount(()=>{
            store.dispatch('process/load',  route.params.apprid )
            .then(()=>{
                state.loading = false
            })
        })

        const actionForms = computed(()=> store.getters['process/actionForms'])

        const agree = () =>{
            state.dialogVisible = true;
            state.action = 'agree';
            state.actionTitle = '同意送出';
        }

        const disagree = () =>{
            state.dialogVisible = true;
            state.action = 'disagree';
            state.actionTitle = '不同意送出';
        }

        const submit = (ref, file, data) => {
            ref.validate((valid) => {
                if (valid) {
                    new Promise((resolve, reject) => {
                        file.uploadFile()
                        .then(()=>{
                            var p = null;
                            if(state.action === 'agree'){
                                p = store.dispatch('process/agree', data)
                            } else if(state.action === 'disagree') {
                                p = store.dispatch('process/disagree', data)
                            } else {
                                alert(state.actionTitle + 'has a error')
                            }
                            p.then((res) =>{
                                if(res.Success){
                                    ElMessage.success('送出成功')
                                    state.dialogVisible = false;
                                    file.commit()
                                    router.go(-1)
                                } else {
                                    ElMessage.error('送出失敗')
                                    state.dialogVisible = false;
                                    file.rollback()
                                }
                            })
                        }).catch(e =>{
                            ElMessage.error('送出失敗')
                        })
                        resolve()
                    });
                } else {
                    console.log('error submit!!')
                    ElMessage.error('請補填必填欄位')
                    return false
                }
            });
        }


        const comeback = () =>{
            router.go(-1)
        }

        const cancel = () =>{
            state.dialogVisible = false;
            state.actionTitle = '';
        }

        const getButtonType = (key) =>{
            if(key.ActionType == 1){
                return 'primary'
            }
            else if(key.ActionType == 2){
                return 'danger'
            } else {
                return 'info'
            }
        }

        const hanldClick = (key) =>{
            if(key == 'SUCCESS'){
                return agree()
            }
            else if(key == 'CANCEL'){
                return disagree()
            } else {
                return () => {}
            }
        }

        return {
            state,
            agree,
            disagree,
            submit,
            cancel,
            comeback,
            actionForms,
            getButtonType,
            hanldClick
        }
    }
}

</script>