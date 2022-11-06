<template>
    <el-button-group>
        <el-button type="primary" v-on:click="submitForm">儲存</el-button>
        <el-button type="info" v-on:click="exit">返回</el-button>
    </el-button-group>
    <el-card>
        <template #header>
            編輯表單
        </template>
        <!-- {{state.formData}} -->
        <el-form :model="state.formData" ref="refForm">
            <el-form-item label="表單名稱" 
                        :prop="'FormName'"
                        :rules="{ required: true, message: '請輸入表單名稱', trigger: 'blur' }">
                <el-input v-model="state.formData.FormName"
                    placeholder="請輸入表單名稱"
                    clearable>
                </el-input>
            </el-form-item>
            <el-form-item label="表單分類" 
                        :prop="'FormType'"
                        :rules="{ required: true, message: '請輸入表單分類', trigger: 'blur' }">
               <FormType v-model="state.formData.FormType"
                    placeholder="請輸入類型"
                    clearable>
                </FormType>
            </el-form-item>
            <template v-for="(item, index) in state.items" v-bind:key="item.ItemTitle">
               <el-card>
                   <template #header>
                        <el-row justify="end">                           
                            <el-button icon="el-icon-sort-up" size="mini" circle v-on:click="up(index)"></el-button>
                            <el-button icon="el-icon-sort-down" size="mini" circle v-on:click="down(index)"></el-button>
                            <el-button icon="el-icon-plus" size="mini" circle v-on:click="add(index)"></el-button>
                            <el-button icon="el-icon-close" size="mini" circle v-on:click="remove(index)"></el-button>
                        </el-row>
                   </template>
                    <Item v-model="state.items[index]" :index="index"></Item>
               </el-card>
            </template>
        </el-form>
    </el-card>
</template>

<script>
import { reactive, onBeforeMount, ref, computed } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { useStore } from 'vuex'
import Item from "./components/item.vue"
import FormType from "./components/formType.vue"

export default{
    components:{
        Item,
        FormType
    },
    setup(){

        const refForm = ref({})
        const route = useRoute()
        const router = useRouter()
        const store = useStore();

        const selectedForm = computed(()=>{
            return store.getters['form/selectedForm']
        })

        const state = reactive({
            formid: route.params.id,
            formData: {},
            items: []
        })

        const submitForm = (form, data) => {
            // form.validate((valid) => {
            //     if (valid) {
            //         alert('submit!');
            //         store.dispatch('action/submit', data)
            //         .then(res=>{
            //            router.push('/waitList')
            //         })

            //     } else {
            //         console.log('error submit!!');
            //         return false;
            //     }
            // });
        }
        const resetForm = () =>{
            refForm.value.resetFields();
        }

        onBeforeMount(()=>{
            store.dispatch("form/getForm", route.params.id).then(() =>{
                // 需copy to new object，直接使用vuex object會發生錯誤
                selectedForm.value.Items.forEach(o =>{
                    state.items.push({...o})
                })
                state.formData = {...selectedForm.value}
                state.formData.Items = state.items
            })

            store.dispatch('itemType/getItemTypes')
        })

        const exit = () =>{
            router.go(-1)
        }
        
        const add = (index) =>{
            state.formData.Items.splice(index +1, 0, {

            })
        }
        const remove = (index) =>{
            state.formData.Items.splice(index, 1)        
        }

        const up = (index) =>{
            // ES6
            [ state.formData.Items[index-1], state.formData.Items[index] ] = [ state.formData.Items[index], state.formData.Items[index -1] ]
        }

        const down = (index) =>{
            [ state.formData.Items[index+1], state.formData.Items[index] ] = [ state.formData.Items[index], state.formData.Items[index +1] ]
        }

        return {
            state,
            selectedForm,
            add,
            remove,
            up,
            down,
            exit,
            submitForm,
            resetForm
        }
    }
}

</script>
<style scoped>
    .el-button--mini.is-circle{
        padding: 7px;
    }
</style>