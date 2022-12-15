<template>
    <el-button type="primary" @click="state.dialogVisible = true">新增檢核項目</el-button>
    <el-dialog
        title="選擇檢核項目"
        v-model="state.dialogVisible"
        width="80%"
        >
        <el-form :model="state.selectedValue" label-width="120px">
            <el-form-item label="檢核項目">
                <el-select v-model="state.selectedValue" value-key="Key" placeholder="請選擇欲檢核項目">
                    <el-option v-for="item in validations" v-bind:key="item.Key" :label="item.Key" :value="item"></el-option>
                </el-select>
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

export default{
    props:['modelValue', 'add', 'disabled'],
    emits:['update:modelValue'],
    setup(props, {emit}) {
        const store = useStore()

        const state = reactive({
            dialogVisible: false,
            selectedValue: {},
            formId: null,
            model: []
        })

        props.modelValue.forEach( item => {
            state.model.push({...item})
        })

        const validations = computed(()=>{
            return store.getters['validation/options']
        })

        onBeforeMount(() =>{
            store.dispatch('validation/getList')
        })

        const add = () =>{
            if(state.selectedValue.ValidateName){
                var m = {...state.selectedValue}
                state.model.push(m)
                emit('update:modelValue', state.model)
                props.add();
                state.dialogVisible = false
                state.selectedValue = {}
            } else {
                alert('selected is null')
            }
        }

        return {
            state,
            add,
            validations,
        }
    },
}
</script>
