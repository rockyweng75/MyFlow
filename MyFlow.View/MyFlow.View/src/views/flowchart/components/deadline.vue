<template>
    <el-select v-model="state.formData" v-on:change="change" clearable>
        <el-option v-for="item in deadlines" v-bind:key="item.DeadlineClass" :label="item.DeadlineName" :value="item.DeadlineClass"></el-option>
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

        const deadlines = computed(()=>{
            return store.getters['deadline/deadlines']
        })

        onBeforeMount(() =>{
            store.dispatch('deadline/getDeadlines')
        })

        const change =()=>{
            emit('update:modelValue', state.formData)
        }
        return {
            state,
            deadlines,
            change
        }
    },
}
</script>
