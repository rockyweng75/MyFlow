<template>
    <el-select v-model="state.formData" v-on:change="change" :disabled="disabled">
        <el-option v-for="item in actionTypes" v-bind:key="item.Value" :label="item.Key" :value="item.Value"></el-option>
    </el-select>
</template>
<script>
import { reactive, onBeforeMount, computed } from 'vue'
import { useStore } from 'vuex'

export default{
    inheritAttrs: false,
    props:['modelValue', 'disabled'],
    emits:['update:modelValue'],
    setup(props, {emit}) {

        const store = useStore()

        const state = reactive({
            formData : props.modelValue
        })

        const actionTypes = computed(()=>{
            return store.getters['actionType/options']
        })

        onBeforeMount(() =>{
            store.dispatch('actionType/getList')
        })

        const change = ()=>{
            emit('update:modelValue', state.formData)
        }

        return {
            state,
            actionTypes,
            change
        }
    },
}
</script>
