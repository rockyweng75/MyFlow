<template>
    <el-card class="green-card" v-loading="loading">
        <template #header>
            {{form.StageName}}
        </template>
        <el-form :model="state.formData" :ref="setRefForm">
            <CustomItems ref="customItems" :items="items" :data="state.formData"></CustomItems>
            <slot/>
        </el-form>
    </el-card>
    <slot name="button" :form="refForm" :file="fileAction" :data="state.formData"></slot>
</template>
    
<script>
    import { reactive, ref, onBeforeMount, computed, provide, toRef } from 'vue'
    import { useStore } from 'vuex'
    
    import CustomItems from "@/components/CustomItem/list.vue"
    
    export default{
        components:{
            CustomItems
        },
        props:{
            stageid: { type: [Number, String], required: true },
            action: { type: String, required: true },
            disabled: { type: Boolean },
        }, 
        //emits:['update:action'],
        setup(props, { emit }){
            const store = useStore()
    
            const loading = ref(true)
            const form = computed(()=>{
                return store.getters['form/stageForm'];
            })
            const items = computed(()=>{
                return store.getters['form/stageItems'];
            })
    
            const data = computed(()=>{
                return store.getters['workItem/stageData'];
            })
    
            const state = reactive({
                formData: {},
            })
    
     
            //解決沒給預設值無法正常表單檢核問題
            const initFormData = () =>{
                items.value.forEach(item =>{
                    if(!state.formData[item.ItemValue]){
                        state.formData[item.ItemValue] = '';
                    }
                })
            }
    
            onBeforeMount(() =>{
                store.dispatch('form/getStage', { id: props.stageid, action: props.action })
                .then(()=>{
                    state.formData = {...data.value}
                    initFormData()
                    loading.value = false
                })
            })
    
            provide('formData', state.formData)
            provide('userInfo', state.userInfo)
    
            const refForm = ref({})
            const fileAction = ref({})
            const customItems = ref()
            const setRefForm = (el) =>{
                if(el != null){
                    refForm.value = el
                }
                if(customItems.value)
                {
                    fileAction.value.uploadFile = customItems.value.uploadFile
                    fileAction.value.commit = customItems.value.commit
                    fileAction.value.rollback = customItems.value.rollback
                }
            }
    
            return {
                state,
                items,
                data,
                form,
                refForm,
                customItems,
                fileAction,
                setRefForm,
                loading
            }
        }
    }
    
    </script>
    
    <style lang="scss" scoped>
        .approveForm {
            .el-card > .el-card__header{
                background-color: "#ffffff"
            }
        }
    </style>