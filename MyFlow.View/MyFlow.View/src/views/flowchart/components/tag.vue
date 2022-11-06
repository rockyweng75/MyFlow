<template>
    <el-select v-model="state.formData" v-on:change="change" :fit-input-width="true">
        <el-option v-for="item in tags" v-bind:key="item.Name" :label="item.Name" :value="item.Class"></el-option>
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

        const tags = computed(()=>{
            return store.getters['tag/tags']
        })

        onBeforeMount(() =>{
            store.dispatch('tag/getTags')
        })

        const change =()=>{
            emit('update:modelValue', state.formData)
        }

        return {
            state,
            tags,
            change
        }
    },
}
</script>
