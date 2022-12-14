<template>
    <slot name="function" :form="refForm" :data="state.formData" :items="state.items"/>
    <el-card v-if="state.formData">
        <template #header>
            <el-row justify="space-between" style="padding:0,0,0,0">
                <el-col :span="12">
                    <label>編輯表單</label>
                </el-col>
                <el-col :span="12" style="text-align:end" >
                    <el-affix :offset="120">

                        <el-button type="warning" @click="toPreview()">預覽表單</el-button>
                    </el-affix>
                </el-col>
            </el-row>
        </template>
        <el-form :model="state" ref="refForm">
            <el-form-item label="表單名稱" 
                        :prop="'formData.FormName'"
                        :rules="{ required: true, message: '請輸入表單名稱', trigger: 'blur' }">
                <el-input v-model="state.formData.FormName"
                    placeholder="請輸入表單名稱"
                    clearable>
                </el-input>
            </el-form-item>
            <el-form-item label="表單分類" 
                        :prop="'formData.FormType'"
                        :rules="{ required: true, message: '請輸入表單分類', trigger: 'blur' }">
               <FormType v-model="state.formData.FormType"
                    placeholder="請輸入類型"
                    clearable>
                </FormType>
            </el-form-item>
            <template v-for="(item, index) in state.items" v-bind:key="index">
               <el-card>
                   <template #header>
                        <el-row justify="end">                           
                            <el-button icon="el-icon-sort-up" size="mini" circle v-on:click="up(index)"></el-button>
                            <el-button icon="el-icon-sort-down" size="mini" circle v-on:click="down(index)"></el-button>
                            <el-button icon="el-icon-plus" size="mini" circle v-on:click="add(index)"></el-button>
                            <el-button icon="el-icon-close" size="mini" circle v-on:click="remove(index)"></el-button>
                        </el-row>
                   </template>
                    <Item v-if="state.items.length > 0" v-model="state.items[index]" :index="index"></Item>
               </el-card>
            </template>
        </el-form>
    </el-card>
    <Preview v-model:open="state.openPreview" :items="state.items">

    </Preview>

    
</template>

<script>
import { reactive, onBeforeMount, ref, computed } from 'vue'
import { useRouter } from 'vue-router'
import { useStore } from 'vuex'
import Item from "./components/item.vue"
import FormType from "./components/formType.vue"
import Preview from "./preview.vue"

export default{
    components:{
        Item,
        FormType,
        Preview
    },
    props:['formid'],
    setup(props){

        const refForm = ref({})
        const router = useRouter()
        const store = useStore();

        const state = reactive({
            formData: null,
            items: [],
            openPreview: false
        })

        onBeforeMount(()=>{
            store.dispatch("form/getForm", props.formid).then(() =>{
                // 需copy to new object，直接使用vuex object會發生錯誤

                state.formData = {...store.getters['form/selectedForm']}

                state.formData.Items.forEach(o =>{
                    state.items.push({...o})
                })
                // 壓回會出錯
                // state.formData.Items = state.items
            })
            store.dispatch('itemType/getItemTypes')
            store.dispatch('dataRef/getDataRefs')
        })


        const exit = () =>{
            router.go(-1)
        }
        
        const add = (index) =>{
            refForm.value.clearValidate()
            state.items.splice(index +1, 0, {})
        }
        const remove = (index) =>{
            refForm.value.clearValidate()
            state.items.splice(index, 1)        
        }

        const up = (index) =>{
            refForm.value.clearValidate()
            // ES6
            if(index !== 0){
                [ state.items[index-1], state.items[index] ] = [ state.items[index], state.items[index -1] ]
            }
        }

        const down = (index) =>{
            refForm.value.clearValidate()
            if(index !== (state.items.length -1)){
                [ state.items[index+1], state.items[index] ] = [ state.items[index], state.items[index +1] ]
            }
        }

        const toPreview = ()=>{
            state.openPreview = true
        }

        return {
            refForm,
            state,
            add,
            remove,
            up,
            down,
            exit,
            toPreview
        }
    }
}

</script>
<style scoped>
    .el-button--mini.is-circle{
        padding: 7px;
    }
</style>