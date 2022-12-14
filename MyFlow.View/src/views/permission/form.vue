<template>
    <slot name="function" :form="refForm" :data="state.formData" :items="state.items"/>
    <el-card v-if="state.formData">
        <template #header>
            編輯表單 
        </template>
        <el-form :model="state.formData" ref="refForm">
            <el-form-item label="員編" 
                        :prop="'UserId'"
                        :rules="{ required: true, message: '請輸入員編', trigger: 'blur' }">
                <el-input v-model="state.formData.UserId"
                    placeholder="請輸入員編"
                    clearable
                    v-on:change="change">
                </el-input>
            </el-form-item>
            <el-form-item label="姓名" >
                <el-input v-model="state.formData.UserName"
                    clearable
                    readonly>
                </el-input>
            </el-form-item>
            <!-- <el-form-item label="系所" >
                <el-input v-model="state.formData.DeptCode"
                    clearable
                    readonly>
                </el-input>
            </el-form-item> -->
            <el-form-item label="職稱" >
                <el-input v-model="state.formData.PosiDesc"
                    clearable
                    readonly>
                </el-input>
            </el-form-item>
            <el-form-item label="授權角色" 
                        :prop="'Role'"
                        :rules="{ required: true, message: '請輸入授權角色', trigger: 'blur' }">
                <Role v-model="state.roleClass">
                </Role>
            </el-form-item>
            <el-form-item label="授權系所" 
                v-if="keyinAuthDept"
                :prop="'SappRoleParam'"
                :rules="{ required: true, message: '請輸入授權系所', trigger: 'blur' }">
                <AuthDept v-model="state.formData.SappRoleParam"></AuthDept>
            </el-form-item>
        </el-form>
    </el-card>
</template>

<script>
import { reactive, onBeforeMount, ref, watch, computed } from 'vue'
import { useRouter } from 'vue-router'
import { useStore } from 'vuex'
import Role from "./components/role.vue"
import AuthDept from "./components/authDept.vue"


export default{
    components:{
        Role,
        AuthDept
    },
    props:['id'],
    setup(props){

        const refForm = ref({})
        const router = useRouter()
        const store = useStore();

        const state = reactive({
            formData: null,
            roleClass:{
                Value: null,
                IsAuthDept: false
            }
        })

        watch(() => state.roleClass.Value, () =>{
            state.formData.Role = state.roleClass.Value
        })

        onBeforeMount(()=>{
            store.dispatch("permission/getUser", props.id).then(() =>{
                // 需copy to new object，直接使用vuex object會發生錯誤

                state.formData = {...store.getters['permission/user']}
                let rules = store.getters['permission/roles'] 
                state.roleClass = { 
                    Value: state.formData.Role, 
                    IsAuthDept: rules.filter(o => o.Value == state.formData.Role)[0].IsAuthDept
                 }
            })
        })

        const change = () =>
        {
            if(state.formData.UserId.length == 9)
            {
                store.dispatch("permission/getUserinfo", state.formData.UserId)
                .then(res =>{
                    state.formData.UserName = res.UserName
                    state.formData.DeptCode = res.DeptCode
                    state.formData.PosiDesc = res.JobTitle

                })
            }
        }

        const keyinAuthDept = computed(() =>{
            return state.roleClass && state.roleClass.IsAuthDept;
        })

        const exit = () =>{
            router.go(-1)
        }
        return {
            refForm,
            state,
            exit,
            change,
            keyinAuthDept
        }
    }
}

</script>
<style scoped>
    .el-button--mini.is-circle{
        padding: 7px;
    }
</style>