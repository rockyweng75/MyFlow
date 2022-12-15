<template>
    <Drawer v-if="open" v-model="open" :show-close="true" title="預覽表單">
        <el-card class="blue-card"> 
            <el-form :model="state.formData" ref="refForm">
                <template v-for="item in items" v-bind:key="item.ItemName">
                    <CustomItem :item="item" v-model="state.formData[item.ItemValue]"/>
                </template>    
                <slot name="button" :form="refForm" :data="state.formData"></slot>
            </el-form>
            <slot/>
        </el-card>
    </Drawer>
</template>

<script>
import { reactive, ref, watch, provide } from 'vue'
import { useStore } from 'vuex'
import { useRoute } from 'vue-router'

import CustomItem from "@/components/CustomItem/index.vue"
import Drawer from "@/components/Drawer/index.vue"

export default{
    components:{
        Drawer,
        CustomItem
    },
    props:['open', 'items'],
    emits:['update:open'],
    setup(props, {emit}){
        const refForm = ref({})

        const state = reactive({
            formData: {},
            userInfo: {}
        })

        watch(()=> props.open, ()=>{ 
            console.log(props.open)
            emit("update:open", props.open)})

        provide('formData', state.formData)
        provide('userInfo', state.userInfo)

        return {
            state,
            refForm,
        }
    }
}

</script>