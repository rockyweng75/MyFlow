<template>
    <el-select v-model="state.formData" v-on:change="change">
        <el-option v-for="item in formTypes" v-bind:key="item.value" :label="item.key" :value="item.value"></el-option>
    </el-select>
</template>
<script>
import { reactive, onBeforeMount, computed } from 'vue'
import { useStore } from 'vuex'

export default{
    props:['modelValue', 'disabled'],
    emits:['update:modelValue'],
    setup(props, {emit}) {

        const store = useStore()
        const state = reactive({
            formData : props.modelValue
        })

        const formTypes = computed(()=>{
            return store.getters['form/formTypes']
        })

        onBeforeMount(() =>{
            store.dispatch('form/getFormTypes')
        })

        const change =()=>{
            emit('update:modelValue', state.formData)
        }

        return {
            state,
            formTypes,
            change
        }
    },
}
</script>
