<template>
    <el-select v-model="state.formData" v-on:change="change" :fit-input-width="true">
        <el-option v-for="item in titles" v-bind:key="item.Key" :label="item.Key" :value="item.Value"></el-option>
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

        const titles = computed(()=>{
            return store.getters['title/options']
        })

        onBeforeMount(() =>{
            store.dispatch('title/getList')
        })

        const change =()=>{
            emit('update:modelValue', state.formData)
        }

        return {
            state,
            titles,
            change
        }
    },
}
</script>
