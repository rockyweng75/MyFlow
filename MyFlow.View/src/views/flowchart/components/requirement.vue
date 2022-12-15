<template>
    <el-select v-model="state.formData" v-on:change="change" clearable>
        <el-option v-for="item in requirements" v-bind:key="item.Key" :label="item.Key" :value="item.Value"></el-option>
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

        const requirements = computed(()=>{
            return store.getters['requirement/options']
        })

        onBeforeMount(() =>{
            store.dispatch('requirement/getList')
        })

        const change =()=>{
            emit('update:modelValue', state.formData)
        }

        return {
            state,
            requirements,
            change
        }
    },
}
</script>
