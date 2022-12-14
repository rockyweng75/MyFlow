<template>
    <el-checkbox-group v-model="state.val" v-on:change="handleChange" :disabled="disabled || readonly">
        <el-checkbox v-for="data in options" v-bind:key="data.key" :label="data.value">{{data.key}}</el-checkbox>
    </el-checkbox-group>
</template>
<script>
import { reactive, watch, onBeforeMount } from 'vue'

export default {
    props:{
        modelValue: { type: String },
        options: { type: Array },
        disabled: { type: Boolean },
        readonly: { type: Boolean }
    },
    emits:[
        'update:modelValue'
    ],
    setup(props, {emit}) {

        let state = reactive({
            val: []
        })
        
        onBeforeMount(()=>{
            var arr = props.modelValue ? props.modelValue.split(',') : []
            state.val.concat(arr)
        })

        var handleChange = () => {
            emit('update:modelValue', state.val.join())
        }

        return {
            state,
            handleChange
        }
    }
}
</script>

