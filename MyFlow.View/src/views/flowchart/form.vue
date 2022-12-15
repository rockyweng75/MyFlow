<template>
    <slot name="function" :form="refForm" :data="state.formData" :stages="state.stages"/>
    <el-card v-if="state.formData">
        <template #header>
            編輯表單
        </template>
        <el-form :model="state" ref="refForm">
            <el-form-item label="作業名稱" 
                    prop="formData.FlowName"
                    :rules="{ required: true, message: '請輸入作業名稱', trigger: 'blur' }">
                <el-input v-model="state.formData.FlowName"
                    :placeholder="'請輸入作業名稱'" 
                    clearable>
                </el-input>
            </el-form-item>
            <el-form-item label="作業英文名稱" 
                    prop="formData.FlowEname">
                <el-input v-model="state.formData.FlowEname"
                    :placeholder="'請輸入作業英文名稱'" 
                    clearable>
                </el-input>
            </el-form-item>
            <el-form-item label="作業類型" 
                    prop="formData.FlowType"
                    :rules="{ required: true, message: '請選擇作業類型' , trigger: 'blur' }">
                <FlowType v-model="state.formData.FlowType"
                    placeholder="請輸入類型"
                    clearable>
                </FlowType>
            </el-form-item>
            <el-form-item label="作業管理者" 
                    prop="formData.AdminUser"
                    :rules="{ required: true, message: '請輸入作業管理者', trigger: 'blur' }">
                <el-input v-model="state.formData.AdminUser"
                    :placeholder="'請輸入作業管理者'" 
                    clearable>
                </el-input>
            </el-form-item>
            <el-form-item label="Tag格式" 
                    prop="formData.TagFormat"
                    :rules="{ required: true, message: '請輸入Tag格式', trigger: 'blur' }">
                <Tag v-model="state.formData.TagFormat"></Tag>
            </el-form-item>
            <el-form-item label="Title格式" 
                    prop="formData.TitleFormat"
                    :rules="{ required: true, message: '請輸入Title格式', trigger: 'blur' }">
                <Title v-model="state.formData.TitleFormat"></Title>
            </el-form-item>
            <el-form-item label="關閉使用" 
                        :prop="'formData.Close'">
                <el-checkbox v-model="state.formData.Close">
                </el-checkbox>
            </el-form-item>
            <el-card>
                <template #header>
                    階段
                </template>
                <el-menu
                    :default-active="state.activeIndex + ''"
                    mode="horizontal"
                    @select="handleSelect"
                    background-color="#545c64"
                    text-color="#fff"
                    active-text-color="#ffd04b"
                    >
                    <el-menu-item :index="index + ''" v-for="(item,index) in state.stages" v-bind:key="index">
                        {{ parseInt(index) + 1}}
                    </el-menu-item>
                    <el-menu-item index="add"><i class="el-icon-plus">+</i></el-menu-item>
                    <el-menu-item index="delete"><i class="el-icon-delete">-</i></el-menu-item>
                </el-menu>
                <transition :key="state.activeIndex">
                    <Stage v-if="!state.loadingStage && state.activeIndex != 'delete'" v-model="state.stages[state.activeIndex]" :index="state.activeIndex">
                        <template v-slot:footer="slotProps">
                            <StageRouteView v-model="slotProps.stage.StageRouteList"></StageRouteView>
                            <ActionFormView v-model="slotProps.stage.ActionFormList"></ActionFormView>  
                            <ValidationView v-model="slotProps.stage.ValidationList"></ValidationView>
                        </template>
                    </Stage>
                    <div v-else>刪除成功</div>
                </transition>
            </el-card>
        </el-form>
    </el-card>
</template>

<script>
import { reactive, onBeforeMount, ref, computed  } from 'vue'
import { useStore } from 'vuex'
import Stage from "./components/stage.vue"
import StageRouteView from "./components/stageRouteView.vue"
import ValidationView from "./components/validationView.vue"
import ActionFormView from "./components/actionFormView.vue"
import FlowType from "./components/flowType.vue"
import Tag from "./components/tag.vue"
import Title from "./components/title.vue"

export default {
    components:{
        Stage,
        StageRouteView,
        ValidationView,
        ActionFormView,
        FlowType,
        Tag,
        Title
    },
    props:['flowid'],
    setup(props){
        const refForm = ref({})
        const store = useStore();

        const flowchart = computed(()=> store.getters['flowchart/flowchart'])
        const stageList = computed(()=> store.getters['stage/list'])

        const state = reactive({
            activeIndex: '0',
            formData: null,
            loadingStage: true,
            stages: []
        })

        const addStage = (index) =>{
            return new Promise((resolve, reject) =>{
                state.stages.push(newStage(index))
                resolve()
            });
        }

        const removeStage = (index) =>{
            var size = state.stages.length
            if(size === 1){
            } else {
                state.stages.splice(index - 1, 1)
            }
        }

        const handleSelect = (key, keyPath) => {
            if(key === 'add'){
                var size = state.stages.length
                key = size + 1 
                addStage(key).then(() =>{
                    state.activeIndex = key
                })
            } else if(key === 'delete'){
                //TODO 剩一筆時有bug
                key = state.activeIndex == '1' ? '1' : parseInt(state.activeIndex) - 1
                removeStage(parseInt(state.activeIndex))
                state.activeIndex = key
            } else {
                state.activeIndex = key
            }
        }

        const newStage = (index)=>{
            return {
                OrderId: index, 
                StageName: '',
                Jobs:[],
                Validations:[],
                ActionForms:[]
            }
        }


        onBeforeMount(async () =>{
            await store.dispatch('flowchart/getFlowchart', props.flowid)
            state.formData = {...flowchart.value}

            state.loadingStage = true
            await store.dispatch('stage/getList', props.flowid)
            state.stages = {...stageList.value}
            state.loadingStage = false

            // .then((res)=>{
            //     state.formData = {...flowchart.value}
            //     // var stages = state.formData.Stages
            //     // // 不能直接使用formdata.Stages會導致頁面當掉
            //     // if(stages){
            //     //     stages.forEach(element => {
            //     //         state.stages.push({...element})
            //     //     })
            //     // }
            // })
            await store.dispatch('actionClass/getList')
        })
 
        return {
            refForm,
            state,
            addStage,
            handleSelect
        }
    }
}

</script>
