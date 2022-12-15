<template>
    <el-select v-model="state.formData" v-on:change="change">
        <el-option v-for="item in data" v-bind:key="item.Key" :label="item.Key" :value="item.Value"></el-option>
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

        const data = computed(()=>{
            return store.getters['actionClass/error']
        })

        const change =()=>{
            emit('update:modelValue', state.formData)
        }

        return {
            state,
            data,
            change
        }
    },
}
</script>
