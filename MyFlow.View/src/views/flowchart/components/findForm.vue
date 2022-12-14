<template>
    <el-select v-model="state.formData" filterable placeholder="請輸入表單ID" v-on:change="change">
        <el-option-group
            v-for="group in state.forms"
            :key="group.name"
            :label="group.name"
            >
            <el-option
                v-for="item in group.children"
                :key="item.id"
                :label="item.name"
                :value="item.id"
            >
            </el-option>
        </el-option-group>
    </el-select>
</template>
<script>
import { ref, reactive, onBeforeMount, onBeforeUpdate } from 'vue'
import { useStore } from 'vuex'

export default{
    props:['modelValue', 'disabled'],
    emits:['update:modelValue'],
    setup(props, {emit}) {
        const store = useStore()
        const state = reactive({
            formData : '',
            forms : store.getters['form/forms']
        })
        onBeforeMount(() =>{
            state.formData = props.modelValue
        })

        onBeforeUpdate(() =>{
            state.formData = props.modelValue
        })

        const change = () =>{
            emit('update:modelValue', state.formData)
        }
        return {
            state,
           // forms,
            change,
        }
    },
}
</script>
