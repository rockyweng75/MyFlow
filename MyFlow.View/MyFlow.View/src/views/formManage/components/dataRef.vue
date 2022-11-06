<template>
    <el-select v-model="modelValue" v-on:change="change">
        <el-option v-for="item in dataRefs" v-bind:key="item.RefClass" :label="item.RefName" :value="item.RefClass"></el-option>
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

        const dataRefs = computed(()=>{
            return store.getters['dataRef/dataRefs']
        })

        onBeforeMount(() =>{
        })

        const change =()=>{
            emit('update:modelValue', state.formData)
        }

        return {
            state,
            dataRefs,
            change
        }
    },
}
</script>
