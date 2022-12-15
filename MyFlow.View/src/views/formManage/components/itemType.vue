<template>
    <el-select v-model="state.formData" v-on:change="change">
        <el-option v-for="item in itemTypes" v-bind:key="item.Value" :label="item.Key" :value="item.Value"></el-option>
    </el-select>
</template>
<script>
import { reactive, computed, onBeforeUpdate, onBeforeMount } from 'vue'
import { useStore } from 'vuex'

export default{
    props:['modelValue', 'disabled'],
    emits:['update:modelValue', 'update:itemType'],
    setup(props, {emit}) {

        const store = useStore()

        const state = reactive({
            formData : props.modelValue
        })

        const itemTypes = computed(()=>{
            return store.getters['itemType/options']
        })

        onBeforeMount(() =>{
            if(itemTypes.value.length == 0){
                store.dispatch('itemType/getList')
            }
        })

        const change =()=>{
            emit('update:modelValue', state.formData)
            emit('update:itemType', state.formData)
        }

        onBeforeUpdate(()=>{
            state.formData = props.modelValue
        })
        return {
            state,
            itemTypes,
            change
        }
    },
}
</script>
