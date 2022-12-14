<template>
    <ApplyForm :flowid="flowid">
        <template v-slot:button="slotProps">
            <el-form-item >
                <el-button type="primary" @click="submitForm(slotProps.form, slotProps.data)">送出</el-button>
                <el-button @click="resetForm(slotProps.form)">清空</el-button>
            </el-form-item>
        </template>
    </ApplyForm>
</template>
<script>

import ApplyForm from "@/components/ApplyForm/index.vue"
import { reactive, onBeforeMount, computed } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { useStore } from 'vuex'
import { ElMessage } from 'element-plus'

export default{
    components:{
        ApplyForm
    },
    setup(){
        const route = useRoute()
        const router = useRouter()

        const flowid = computed(()=>{
            return route.params.id
        })
        const store = useStore();

        const submitForm = (form, data) => {
            data.FlowId = flowid.value
            data.OrderId = 1
            form.validate((valid) => {
                if (valid) {
                    store.dispatch('process/submit', data)
                    .then(res=>{
                        if(res.Success){
                            ElMessage.success('送出成功')
                            router.push('/workboard/waitList')
                        } else {
                            if(res.Msgs){
                                res.Msgs.forEach(msg =>{
                                    setTimeout(()=>{
                                        ElMessage.error(msg)
                                    }, 100)
                                })
                            } else {
                                ElMessage.error(res.Msg)
                            }
                        }
                    })
                    .catch(e=>{
                        ElMessage.error('表單設定異常，請聯絡相關人員')
                    })
                } else {
                    console.log('error submit!!');
                    ElMessage.error('請補填必填欄位')
                    return false;
                }
            });
        }
        const resetForm = (refForm) =>{
            refForm.resetFields();
        }

        onBeforeMount(()=>{
            store.dispatch("process/initFormData")
        })

        return {
            flowid,
            submitForm,
            resetForm
        }
    }
}

</script>