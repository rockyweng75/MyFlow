<template>
    <el-card>
        <template #header>
            <el-row justify="space-between">
                <ActionForm v-model="state.model" :add="addRow"></ActionForm>
            </el-row>
        </template>        
        <el-table :data="state.model.filter(o => !o.Delete)" :default-sort="{ prop: 'OrderId' }">
            <el-table-column prop="OrderId" label="順序" width="90"> </el-table-column>
            <el-table-column prop="ActionTypeName" label="功能類型" width="180"> </el-table-column>
            <el-table-column label="按鈕名稱" prop="ButtonName" width="180"></el-table-column>
            <el-table-column label="表單名稱" prop="FormName" width="180"></el-table-column>
            <el-table-column label="刪除" width="120">
                <template #default="scope">
                    <el-button
                        type="text"
                        size="small"
                        icon="el-icon-close"
                        circle
                        @click.prevent="deleteRow(scope.$index)"
                    >
                    </el-button>
                </template>
            </el-table-column>
            <el-table-column label="功能" width="120">
                <template #default="scope">
                    <el-button icon="el-icon-sort-up" size="mini" circle v-on:click="up(scope.$index)"></el-button>
                    <el-button icon="el-icon-sort-down" size="mini" circle v-on:click="down(scope.$index)"></el-button>
                </template>
            </el-table-column>
        </el-table>
    </el-card>
</template>
<script>
import { reactive, onBeforeMount } from 'vue'
import { useStore } from 'vuex'
import ActionForm from "./actionForm.vue"

export default {    
    props:['modelValue', 'disabled'],
    emits:['update:modelValue'],
    components:{ ActionForm },
    setup(props, { emit }) {

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

        const refreshList = () =>{
            return state.model
                .filter(o => !o.Delete)
                .sort((a, b) => a.OrderId - b.OrderId)
        }
        
        const deleteRow = (index) =>{
            var list = refreshList();
            list[index].Delete = true
            list[index].OrderId = 99
            emit('update:modelValue', state.model)
        } 

        const addRow = () =>{
            emit('update:modelValue', state.model)
        } 

        const up = (index) =>{
            // ES6
            if(index !== 0){
                var list = refreshList();
                list[index - 1].OrderId = index + 1
                list[index].OrderId = index 
                emit('update:modelValue', state.model)
            }
        }

        const down = (index) =>{
            if(index !== ( state.model.filter(o => !o.Delete).length -1)){
                var list = refreshList();
                list[index + 1].OrderId = index + 1
                list[index].OrderId = index + 2
                emit('update:modelValue', state.model)
            }
        }

        onBeforeMount(() =>{      
        })

        return {
            state,
            addRow,
            up,
            down,
            deleteRow
        }
    },
}
</script>