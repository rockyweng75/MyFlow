<template>
    <el-row class="block" justify="space-between">
        <el-col :span="12">
            <el-button type="danger"
                v-if="canEdit"
                @click="revoke()">撤銷</el-button>
        </el-col>
        <el-col :span="12" style="text-align:end" >
            <el-button type="warning" @click="comeback()">返回</el-button>
        </el-col>
    </el-row>
    <!-- <Details v-model="state.dialogVisible" :title="state.actionTitle">
        <ApproveForm :stageid="state.stageid">
            <template v-slot:button="slotProps">
                <el-divider></el-divider>
                <el-button-group>
                    <el-button type="warning" @click="disagree(slotProps.data)">撤銷</el-button>
                    <el-button type="danger" @click="cancel()">取消</el-button>
                </el-button-group>
            </template>        
        </ApproveForm>
    </Details> -->
    <ApplyForm class="disabled-form" :flowid="state.flowid" :disabled="true" v-if="!state.loading">
    </ApplyForm>
    <ProcessHistory :applyid="state.applyid" :dataid="state.dataid" :disabled="true" v-if="!state.loading"/>  
</template>

<script>

import ApplyForm from "/@/components/ApplyForm/index.vue"
import ProcessHistory from "/@/components/ProcessHistory/index.vue"
import ApproveForm from "/@/components/ApproveForm/index.vue"
import Details from "./details.vue"
import { ElMessageBox, ElMessage } from 'element-plus'
import { reactive, onBeforeMount, computed, inject } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { useStore } from 'vuex'

export default{
    components:{
        ApplyForm,
        ProcessHistory,
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
            dataid: route.params.dataid,
            dialogVisible: false,
            actionTitle: '',
            loading: true,
            workitem: {},
            stageData:{}
        })

        const store = useStore();

        onBeforeMount(()=>{
            store.dispatch('workItem/load',  route.params.dataid )
            .then(()=>{
                state.loading = false
                state.workitem = store.getters['workItem/workitem']
                state.stageData = store.getters['workItem/stageData']
            })
        })

        const canEdit = computed(() =>{
            var user = store.getters['user/userInfo'] 
            var applyStatus = state.workitem.ApplyStatus
            var applyUser = state.workitem.ApplyUser
            return user.UserId == applyUser && applyStatus != '簽核完成' && applyStatus != '撤銷'
        })

        const reload = inject('reload')

        const revoke = () =>{
            ElMessageBox.confirm(
                '確認後，表單將失效，是否繼續進行？',
                'Warning',
                {
                    confirmButtonText: '確定',
                    cancelButtonText: '取消',
                }
            ).then(()=>{
                store.dispatch('action/cancel', state.stageData)
                .then(res=>{
                    if(res.Success){
                        ElMessage.success('撤銷成功')
                        reload()
                        // router.go(-1)
                    } else {
                        ElMessage.error('撤銷失敗')
                    }
                })
            })
        }

        const comeback = () =>{
            router.go(-1)
        }

        return {
            state,
            revoke,
            comeback,
            canEdit
        }
    }
}

</script>