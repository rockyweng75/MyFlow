<template>
    <el-card class="blue-card" v-loading="loading"> 
        <template #header>
            申請{{form.FlowName}}
        </template>
        <el-form :model="state.formData" ref="refForm" >
            <CustomItems ref="customItems" :items="items" :data="state.formData" :disabled="disabled" :readonly="readonly"></CustomItems>
            <slot name="button" :form="refForm" :data="state.formData"></slot>
        </el-form>
        <slot/>
    </el-card>
    </template>
    
    <script>
    import { reactive, ref, onBeforeMount, computed, provide } from 'vue'
    import { useStore } from 'vuex'
    
    import CustomItems from "@/components/CustomItem/list.vue"
    
    export default{
        components:{
            CustomItems
        },
        props:{
            flowid: { type: [ Number, String ], required: true },
            disabled: { type: Boolean },
            readonly: { type: Boolean },
        }, 
        setup(props){
            const store = useStore()
    
            const form = computed(()=>{
                return store.getters['form/selectedForm'];
            })
            const items = computed(()=>{
                return store.getters['form/items'];
            })
    
            const userinfo = computed(()=>{
                return store.getters['user/userInfo'];
            })
            const formData = computed(()=>{
                return store.getters['process/applyData'];
            })
    
            const loading = ref(true)

            const state = reactive({
                formData: {...formData.value},
                userInfo: userinfo.value
            })
    
            provide('userInfo', state.userInfo)
            provide('formData', state.formData)
            
            onBeforeMount(() =>{

               store.dispatch('form/getApplyForm', props.flowid )
                .then(()=>{
                    //解決沒給預設值無法正常表單檢核問題
                    if(items.value)
                    {
                        items.value.forEach(item =>{
                            if(!state.formData[item.ItemValue]){
                                state.formData[item.ItemValue] = '';
                            }
                        })
                    }
                    loading.value = false
                })
            })
    
            const refForm = ref({})
            const customItems = ref()
         
            const setRefForm = (el) =>{
                refForm.value = el
                if(refForm.value){
                    if(customItems.value){
                        refForm.value.uploadFile = () =>{
                            return customItems.value.uploadFile()
                        }
                        refForm.value.commit = () =>{
                            return customItems.value.commit()
                        }
                        refForm.value.rollback = () =>{
                            return customItems.value.rollback()
                        }
                    }
                }
            }
    
            return {
                formData,
                state,
                items,
                form,
                refForm,
                setRefForm,
                loading
            }
        }
    }

    </script>