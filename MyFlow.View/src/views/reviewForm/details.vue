<template>
    <el-dialog :title="title"
               v-model="modelValue"
               :width="width"
               :show-close="false"
               :fullscreen="fullscreen"
               :close-on-press-escape="false"
               :close-on-click-modal="false"
               :destroy-on-close="true"
               @closed="handleClose"
               v-on:open="handleOpen"
               center>
        <slot />
    </el-dialog>
</template>
<script>
import { reactive, computed } from 'vue'
import { useStore } from 'vuex'

export default {
    props:['modelValue', 'title'],
    emits:['update:modelValue'],
    setup(props, {emit}) {
        
        const store = useStore()
        const fullscreen = computed(()=>{
            return store.getters['app/device'] !== 'desktop'
        })

        const width = computed(()=>{
            return store.getters['app/device'] !== 'desktop' ? "100%" : '80%'
        })
        
        const state = reactive({
            // dialogVisible: props.modelValue
        })
        const handleClose = () =>{
            emit('update:modelValue', state.dialogVisible)
        }
        const handleOpen = () =>{
            
        }
        const updateAction = () =>{

        }
        const deleteAction = () =>{

        }
        const closeAction = () =>{
            emit('update:modelValue', false)
        }
        return {
            state,
            fullscreen,
            width,
            handleClose,
            handleOpen,
            updateAction,
            deleteAction,
            closeAction
        }
    },
}
</script>