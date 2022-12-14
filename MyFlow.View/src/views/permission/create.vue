<template>
    <Form>
        <template v-slot:function="slotProps">
            <el-button-group>
                <el-button type="primary" v-on:click="submit(slotProps.form, slotProps.data)">儲存</el-button>
                <el-button type="info" v-on:click="exit">返回</el-button>
            </el-button-group>
        </template>
    </Form>
</template>

<script>
import { reactive, onBeforeMount, onMounted, computed } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { useStore } from 'vuex'
import Form from "./form.vue"
import { ElMessage } from 'element-plus'

export default{
    components:{
        Form
    },
    setup(){
        const route = useRoute()
        const router = useRouter()
        const state = reactive({
            actionTitle: '',
            formData: {}
        })

        const store = useStore();

        const exit = () =>{
            router.go(-1)
        }

        const submit = (form, formData)=>{
            form.validate((valid, vdata) => {
                if (valid) {
                    store.dispatch('permission/insert', formData)
                    .then(res=>{
                        ElMessage.success('儲存成功')
                        router.push('/permission/index')
                    }).catch(e =>{
                        ElMessage.error('儲存失敗')
                    })

                } else {
                    console.log('error submit!!');
                    return false;
                }
            });
        }

        onBeforeMount(() =>{
            store.dispatch("permission/getRoles")
            store.dispatch("dept/getAuthDeptList")
        })

        return {
            exit,
            submit
        }
    }
}
</script>