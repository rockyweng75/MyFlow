<template>
    <el-button type="primary" @click="state.dialogVisible = true">新增功能表單</el-button>
    <el-dialog
        title="選擇功能表單"
        v-model="state.dialogVisible"
        width="80%"
        >
        <el-form :model="state.selectedValue" label-width="120px">
            <el-form-item label="功能">
                <!-- <el-select v-model="state.selectedValue.ActionType" placeholder="請選擇功能">
                    <el-option :value="1" label="SUCCESS(送出)"></el-option>
                    <el-option :value="2" label="FAIL(錯誤)"></el-option>
                    <el-option :value="3" label="CANCEL(退回)"></el-option>
                </el-select> -->
                <ActionType v-model="state.selectedValue.ActionType" placeholder="請選擇功能"></ActionType>
            </el-form-item>
            <el-form-item label="功能名稱">
                <el-input v-model="state.selectedValue.ActionName" placeholder="請選擇功能名稱"></el-input>
            </el-form-item>
            <el-form-item label="按鈕名稱">
                <el-input v-model="state.selectedValue.ButtonName" placeholder="請選擇按鈕名稱"></el-input>
            </el-form-item>
            <el-form-item label="表單ID">
                <FindForm v-model="state.selectedValue.FormId"></FindForm>
            </el-form-item>
        </el-form>
        <template #footer>
            <span class="dialog-footer">
            <el-button @click="state.dialogVisible = false">取消</el-button>
            <el-button type="primary" @click="add">
                確定
            </el-button>
            </span>
        </template>
    </el-dialog>
</template>
<script>
import { reactive, onBeforeMount, computed } from 'vue'
import { useStore } from 'vuex'
import FindForm from './findForm.vue'
import ActionType from "./actionType.vue"

export default {    
    props:['modelValue', 'add', 'disabled'],
    emits:['update:modelValue'],
    components:{
        FindForm,
        ActionType
    },
    setup(props, {emit}) {

        const store = useStore()

        const state = reactive({
            dialogVisible: false,
            selectedValue: {},
            model: []
        })

        props.modelValue.forEach( item => {
            state.model.push({...item})
        })


        onBeforeMount(() =>{      
            store.dispatch('form/getForms')
        })

        const add = () =>{
            state.model.push(state.selectedValue)
            emit('update:modelValue', state.model)
            props.add();
            state.dialogVisible = false
            state.selectedValue = {}
        }

        return {
            state,
            add,
        }
    },
}
</script>