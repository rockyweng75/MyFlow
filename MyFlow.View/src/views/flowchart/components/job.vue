<template>
    <el-button type="primary" @click="state.dialogVisible = true">新增作業</el-button>
    <el-dialog
        title="選擇作業"
        v-model="state.dialogVisible"
        width="80%"
        >
        <el-form :model="state.selectedValue" label-width="120px">
            <el-form-item label="執行作業">
                <el-select v-model="state.selectedValue" value-key="JobName" placeholder="請選擇欲執行的作業">
                    <el-option v-for="item in jobs" v-bind:key="item.JobName" :label="item.JobName" :value="item"></el-option>
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

export default {    
    props:['modelValue', 'add', 'disabled'],
    emits:['update:modelValue'],
    components:{
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

        const jobs = computed(()=>{
            return store.getters['job/jobs']
        })
   
        onBeforeMount(() =>{      
            store.dispatch('job/getJobs').then(()=>{
            })
        })

        const add = () =>{
            if(state.selectedValue.JobName){
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
            jobs,
        }
    },
}
</script>