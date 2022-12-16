<template>

    <div v-if="state.isAreas" >
        <el-steps v-if="state.items.some(o => o.ItemType === 202)" 
            direction="vertical"
            :active="99" 
            finish-status="finish" 
            process-status="finish" >
            <template v-for="item in state.items" v-bind:key="item.ItemName" >
                <el-step :title="item.ItemTitle" 
                         v-if="item.ItemType === 202" 
                         class="custom-step">
                    <template #description >
                        <div v-for="child in item.child" v-bind:key="child.ItemName">
                            <CustomItem :item="child" 
                                :ref="setRefForm"
                                v-model="data[child.ItemValue]"
                                :disabled="child.Disabled === 'Y'"
                                :readonly="readonly"/>
                        </div>
                    </template>
                </el-step>
            </template>   
        </el-steps>
         <template v-else v-for="item in state.items" v-bind:key="item.ItemName" >
            <el-card v-if="item.ItemType === 201" :class="item.ItemDesc">
                <template #header>
                    {{item.ItemTitle}}
                </template>
                <template v-for="child in item.child" v-bind:key="child.ItemName">
                    <CustomItem :item="child" 
                        :ref="setRefForm"
                        v-model="data[child.ItemValue]"
                        :disabled="child.Disabled === 'Y'"
                        :readonly="readonly"/>
                </template>
            </el-card>
        </template>   

    </div>
    <div v-else>
        <template v-for="item in state.items" v-bind:key="item.ItemName" >
        <CustomItem :item="item" 
            :ref="setRefForm"
            v-model="data[item.ItemValue]"
            :disabled="item.Disabled === 'Y'"
            :readonly="readonly"/>
        </template>
    </div>
</template>
<script>
import CustomItem from "../CustomItem/index.vue"

import { onBeforeUpdate, reactive, onBeforeMount, ref, toRefs } from 'vue'

export default{
    props:{
        items: { type: [Array, Object] },
        data: { type: Object },
        readonly: {
            type: Boolean,
            default: false
        }
    },
    components:{
        CustomItem,
    },
    setup(props, {emit}) {
     
        const state = reactive({})

        const loadItems = () =>{
            state.items = [];
            if(props.items.some(o => o.ItemType.toString().substring(0, 1) === '2' && o.ItemType.toString().length === 3))
            {
                state.isAreas = true;
                state.items = [];
                let s, n = null;
                let c = 0;
                props.items.forEach(item =>{
                    n = {...item};
                    if(item.ItemType.toString().substring(0, 1) === '2'){
                        s = {...item};
                        s.child = [];
                        c = parseInt(item.ItemValue);
                    } else {
                        if(c > 0){
                            s.child.push(n);
                            if(--c == 0) state.items.push(s); 
                        } else {
                            state.items.push(n);
                        }
                    }
                })
            } else {
                state.isAreas = false;
                state.items = props.items;
            }
        }
        onBeforeUpdate(()=>{
            loadItems();
        })

        onBeforeMount(()=>{
            if(props.items){
                loadItems();
            }
        })

        const refForm = reactive({
            uploadFile: () => { return new Promise((resolve) => resolve())},
            commit: () => { return new Promise((resolve) => resolve())},
            rollback: () => { return new Promise((resolve) => resolve())}
        })
        const setRefForm = (el) =>{
            // 目前只支援一項upload item
            if(el && el.uploadImageFile){
                refForm.uploadFile = el.uploadFile
                refForm.commit = el.commit
                refForm.rollback = el.rollback
                refForm.hasUpload = true
            } 
        }

        return {
            ...toRefs(refForm),
            state,
            setRefForm,
        }
    },
}
</script>
<style scoped>
    .custom-step >>>.el-step__title.is-finish{
        color: #67c23a;
        border-color: #67c23a;
    }
    .custom-step >>>.el-step__head.is-finish{
        color: #67c23a;
        border-color: #67c23a;
    }
</style>>
