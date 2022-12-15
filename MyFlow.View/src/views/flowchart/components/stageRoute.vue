<template>
    <el-button type="primary" @click="state.dialogVisible = true">新增路由與執行條件</el-button>
    <el-dialog
        title="選擇路由"
        v-model="state.dialogVisible"
        width="80%"
        >
        <el-form :model="state.selectedValue" label-width="120px">
            <el-form-item label="路由名稱">
                <el-input v-model="state.selectedValue.RouteName" placeholder="請輸入路由名稱"></el-input>
            </el-form-item>
            <el-form-item label="動作">
                <ActionType v-model="state.selectedValue.ActionType" placeholder="請選擇動作"></ActionType>
            </el-form-item>
            <el-form-item label="下一個階段ID">
                <el-input v-model="state.selectedValue.NextStageId" placeholder="請輸入下一個階段"></el-input>
            </el-form-item>
            <el-form-item label="執行條件">
                <el-select v-model="state.selectedValue.SwitchClass" placeholder="請選擇執行條件">
                    <el-option v-for="item in switchClasses" v-bind:key="item.Key" :label="item.Key" :value="item.Value"></el-option>
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
import ActionType from "./actionType.vue"

export default {    
    props:['modelValue', 'add', 'disabled'],
    emits:['update:modelValue'],
    components:{
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

        const switchClasses = computed(()=>{
            return store.getters['switchClass/options']
        })
   
        onBeforeMount(() =>{      
            store.dispatch('switchClass/getList')
            .then(()=>{
            })
        })

        const add = () =>{
            if(state.selectedValue.Value){
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
            switchClasses,
        }
    },
}
</script>