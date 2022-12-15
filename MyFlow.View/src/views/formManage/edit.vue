<template>
    <Form :formid="$route.params.id">
        <template v-slot:function="slotProps">
            <el-button-group>
                <el-button type="primary" v-on:click="submit(slotProps.form, slotProps.data, slotProps.items)">儲存</el-button>
                <el-button type="info" v-on:click="exit">返回</el-button>
            </el-button-group>
        </template>
    </Form>
</template>
<script>
import Form from "./form.vue"
import { useRouter } from 'vue-router'
import { useStore } from 'vuex'
import { inject } from 'vue'
import { ElMessage } from 'element-plus'

export default {
    components:{
        Form
    },
    setup(){
        const router = useRouter()
        const store = useStore()
        const exit = () =>{
            router.go(-1)
        }

        const reload = inject('reload')

        const submit = (form, formData, items)=>{
            formData.Items = items
            form.validate((valid) => {
                if (valid) {
                    store.dispatch('form/modify', formData)
                    .then(res=>{
                        ElMessage.success('儲存成功')
                        reload()
                    }).catch(e =>{
                        ElMessage.error('儲存失敗')
                    })
                } else {
                    console.log('error submit!!');
                    return false;
                }
            });
        }

        return {
            exit,
            submit
        }
    }
}

</script>