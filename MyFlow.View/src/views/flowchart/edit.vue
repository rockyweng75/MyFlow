<template>
    <Form :flowid="$route.params.id">
        <template v-slot:function="slotProps">
            <el-button-group>
                <el-button type="primary" v-on:click="submit(slotProps.form, slotProps.data, slotProps.stages)">儲存</el-button>
                <el-button type="info" v-on:click="exit">返回</el-button>
            </el-button-group>
        </template>
    </Form>
</template>

<script>
import Form from "./form.vue"
import { useStore } from 'vuex'
import { useRouter } from 'vue-router'
import { reactive, onBeforeMount, ref, inject } from 'vue'
import { ElMessage } from 'element-plus'

export default{
    components:{
        Form
    },
    setup(){
        const store = useStore()
        const router = useRouter()

        const reload = inject('reload')
        const submit = (form, formData, stages)=>{
            form.validate((valid, vdata) => {
                formData.Stages = stages
                if (valid) {
                    store.dispatch('flowchart/modify', formData)
                    .then(res=>{
                        ElMessage.success('儲存成功')
                        reload()
                    }).catch(e =>{
                        ElMessage.error('儲存失敗')
                    })
                } else {
                    return false;
                }
            });
        }
        onBeforeMount(() =>{
        })

        const exit = () =>{
            router.go(-1)
        }
        return {
            submit,
            exit
        }
    }
}

</script>