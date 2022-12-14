<template>
    <el-select v-model="state.formData" v-on:change="change">
        <el-option-group
            v-for="group in authDept"
            :key="group.Academic"
            :label="group.DeptCname"
        >
            <el-option
                v-for="item in group.children"
                :key="item.DeptCode"
                :label="item.DeptCname"
                :value="item.DeptCode"
            >
            </el-option>
        </el-option-group>
    </el-select>
</template>
<script>
import { reactive, computed } from 'vue'
import { useStore } from 'vuex'

export default{
    props:['modelValue', 'disabled'],
    emits:['update:modelValue'],
    setup(props, {emit}) {

        const store = useStore()

        const state = reactive({
            formData : props.modelValue
        })

        const authDept = computed(()=>{
            return store.getters['dept/authDeptList']
        })
        console.log(authDept)

        const change = () =>{
            emit('update:modelValue', state.formData)
        }

        return {
            state,
            authDept,
            change
        }
    },
}
</script>
