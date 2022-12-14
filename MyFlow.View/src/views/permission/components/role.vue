<template>
    <el-select v-model="state.formData" v-on:change="change" value-key="Value">
        <el-option v-for="item in roles" v-bind:key="item.Value" :label="item.Name" :value="item"></el-option>
    </el-select>
</template>
<script>
import { reactive, ref, onBeforeMount, computed, onMounted } from 'vue'
import { useStore } from 'vuex'

export default{
    props:['modelValue', 'disabled'],
    emits:['update:modelValue'],
    setup(props, {emit}) {

        const store = useStore()

        const state = reactive({
            formData : props.modelValue
        })

        const roles = computed(()=>{
            return store.getters['permission/roles']
        })

        const change =()=>{
            emit('update:modelValue', state.formData)
        }

        return {
            state,
            roles,
            change
        }
    },
}
</script>
