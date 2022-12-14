<template>
    <el-select v-model="state.formData" v-on:change="change">
        <el-option v-for="item in targets" v-bind:key="item.TargetName" :label="item.TargetName" :value="item.TargetClass"></el-option>
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

        const targets = computed(()=>{
            return store.getters['target/targets']
        })

        onBeforeMount(() =>{
            store.dispatch('target/getTargets')
        })

        const change =()=>{
            emit('update:modelValue', state.formData)
        }

        return {
            state,
            targets,
            change
        }
    },
}
</script>
