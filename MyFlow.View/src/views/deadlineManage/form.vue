<template>
    <slot name="function" :form="refForm" :data="state.formData"/>
    <el-card v-if="state.formData">
        <template #header>
            編輯設定
        </template>
        <el-form :model="state" ref="refForm">
            <el-form-item label="學年" 
                    prop="formData.AcadYear"
                    :rules="{ required: true, message: '請選擇學年' , trigger: 'blur' }">
               <el-input v-model="state.formData.AcadYear"
                    readonly>
                </el-input>
            </el-form-item>
            <el-form-item label="學期" 
                    prop="formData.SemeType"
                    :rules="{ required: true, message: '請輸入學期', trigger: 'blur' }">
                <el-input v-model="state.formData.SemeType"
                    readonly>
                </el-input>
            </el-form-item>
            <el-form-item label="流程編號" 
                    prop="formData.FlowId"
                    :rules="{ required: true, message: '請輸入流程編號', trigger: 'blur' }">
                <el-input v-model="state.formData.FlowId"
                    readonly>
                </el-input>
            </el-form-item>
            <el-form-item label="流程名稱" 
                    prop="formData.FlowName"
                    :rules="{ required: true, message: '請輸入流程名稱', trigger: 'blur' }">
                <el-input v-model="state.formData.FlowName"
                    readonly>
                </el-input>
            </el-form-item>
            <el-form-item label="作業類型" 
                    prop="formData.FlowType"
                    :rules="{ required: true, message: '請選擇作業類型' , trigger: 'blur' }">
                <FlowType v-model="state.formData.FlowType"
                    placeholder="請輸入類型"
                    disabled>
                </FlowType>
            </el-form-item>
            <el-form-item label="開始時間" 
                    prop="formData.BeginDate"
                    :rules="{ required: true, message: '請輸入開始時間', trigger: 'blur' }">
                <el-date-picker v-model="state.formData.BeginDate"
                    type="datetime"
                    :placeholder="'請輸入開始時間'" 
                    format="YYYY-MM-DD HH:mm:ss"
                    value-format="YYYY-MM-DD HH:mm:ss"
                    clearable>
                </el-date-picker>
            </el-form-item>
            <el-form-item label="結束時間" 
                    prop="formData.EndDate"
                    :rules="{ required: true, message: '請輸入結束時間', trigger: 'blur' }">
                <el-date-picker v-model="state.formData.EndDate"
                    type="datetime"
                    :placeholder="'請輸入結束時間'" 
                    format="YYYY-MM-DD HH:mm:ss"
                    value-format="YYYY-MM-DD HH:mm:ss"
                    clearable></el-date-picker>
            </el-form-item>
            <el-form-item label="延長天數" 
                        :prop="'formData.PlusDays'">
                <el-input-number v-model="state.formData.PlusDays"
                    :placeholder="'請輸入結束時間'"
                    clearable>
                </el-input-number>
            </el-form-item>
        </el-form>
    </el-card>
</template>

<script>
import { reactive, onBeforeMount, ref, computed  } from 'vue'
import { useStore } from 'vuex'
import FlowType from '/@/views/flowchart/components/flowType.vue'
export default {
    components:{
        FlowType
    },
    props:['acadYear', 'semeType', 'flowId', 'id'],
    setup(props){
        const refForm = ref({})
        const store = useStore();

        const state = reactive({
            formData: null,
        })

        onBeforeMount(() =>{
            store.dispatch('deadline/getDeadline', { 
                AcadYear: props.acadYear,
                SemeType: props.semeType,
                FlowId: props.flowId,
                DeadlineId: props.id
            }).then((res)=>{
                state.formData = {... store.getters['deadline/deadline']}
            })      
            store.dispatch('flowType/getFlowTypes')
        })
 
        return {
            refForm,
            state
        }
    }
}

</script>
