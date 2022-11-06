<template>
    <el-select v-model="state.formData" v-on:change="change">
        <el-option v-for="item in itemTypes" v-bind:key="item.value" :label="item.key" :value="item.value"></el-option>
    </el-select>
</template>
<script>
import { reactive, ref, onUpdated, computed, onBeforeUpdate } from 'vue'
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
            return store.getters['itemType/itemTypes']
        })

        const change =()=>{
            emit('update:modelValue', state.formData)
            emit('update:itemType', state.formData)
        }

        // onUpdated(()=>{
        //     console.log('onUpdated', state.formData)
        //     console.log('onUpdated', props)

        //                 // state.formData = props.modelValue

        // })

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
