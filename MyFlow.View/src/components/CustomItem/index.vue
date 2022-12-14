<template>
    <template v-if="item.ItemType == 1">
        <el-form-item :label="item.ItemTitle" 
                    :prop="item.ItemValue"
                    :rules="getRules(item)"
                    :label-width="100">
            <el-input v-model="modelValue"
                :placeholder="getPlaceholder(item, '請輸入')" 
                clearable
                v-on:change="changeValue"
                :disabled="disabled"
                :readonly="readonly">
            </el-input>
        </el-form-item>
    </template>
    <template v-if="item.ItemType == 2">
        <el-form-item :label="item.ItemTitle" 
                :prop="item.ItemValue" 
                :rules="getRules(item)"
                :label-width="100">
            <el-select v-model="modelValue"
                :placeholder="getPlaceholder(item,'請選擇')" 
                :multiple="getMultiple(item)"
                clearable
                v-on:change="changeValue"
                :disabled="disabled || readonly">
                <template v-for="data in item.Data" v-bind:key="data.Key">
                    <el-option :value="data.Key"
                          :label="data.Value">
                    </el-option>
                </template>
            </el-select>
        </el-form-item>
    </template>
    <template v-if="item.ItemType == 3">
        <el-form-item :label="item.ItemTitle" 
                    :prop="item.ItemValue"
                    :rules="getRules(item)"
                    :label-width="100">
            <MultipleCheckbox v-model="modelValue" 
                :options="item.Data"
                v-on:change="changeValue" 
                :disabled="disabled"
                :readonly="readonly"></MultipleCheckbox>
        </el-form-item>
    </template>
    <template v-if="item.ItemType == 4">
        <el-form-item :label="item.ItemTitle" 
                    :prop="item.ItemValue"
                    :rules="getRules(item)"
                    :label-width="100">
            <el-radio-group v-model="modelValue"
                            :multiple="getMultiple(item)"
                            v-on:change="changeValue" 
                            :placeholder="getPlaceholder(item,'請選擇')"
                            :disabled="disabled || readonly">
                <el-radio v-for="data in item.Data" v-bind:key="data.key" :label="data.value">{{data.key}}</el-radio>
            </el-radio-group>
        </el-form-item>
    </template>
    <template v-if="item.ItemType == 5">
        <el-form-item :label="item.ItemTitle" 
                    :prop="item.ItemValue"
                    :rules="getRules(item)"
                    :label-width="100"
                    >
            <el-date-picker
                v-model="modelValue"
                type="date"
                :placeholder="getPlaceholder(item,'請選擇')"
                v-on:change="changeValue"
                :disabled="disabled"
                >
            </el-date-picker>
        </el-form-item>
    </template>
    <template v-if="item.ItemType == 6">
        <el-form-item :label="item.ItemTitle" 
                    :prop="item.ItemValue"
                    :rules="getRules(item)"
                    :label-width="100"
                    >
            <el-date-picker
                v-model="modelValue"
                type="datetime"
                :placeholder="getPlaceholder(item,'請選擇')"
                v-on:change="changeValue"
                :disabled="disabled"
                :readonly="readonly"
                >
            </el-date-picker>
        </el-form-item>
    </template>
    <template v-if="item.ItemType == 7">
        <el-form-item :label="item.ItemTitle" 
                    :prop="item.ItemValue"
                    :rules="getRules(item)"
                    :label-width="100">
            <el-time-picker
                v-model="modelValue"
                :placeholder="getPlaceholder(item,'請選擇')"
                v-on:change="changeValue"
                :disabled="disabled"
                :readonly="readonly"
                >
            </el-time-picker>
        </el-form-item>
    </template>
    <template v-if="item.ItemType == 8">
        <el-form-item :label="item.ItemTitle" 
                    :prop="item.ItemValue"
                    :rules="getRules(item)"
                    :label-width="100">
            <UploadImageFile 
                ref="uploadImageFile"
                :disabled="disabled || readonly"
                v-on:change="changeValue"
                v-model="modelValue" 
            />
        </el-form-item>        
    </template>
    <!-- type=9,是messageBox-->
    <template v-if="item.ItemType == 10">
        <el-card style="margin-bottom:30px">
            <el-descriptions direction="vertical" v-if="Array.isArray(modelValue)">
                <el-descriptions-item :label="_key" v-for="(_key,index) in Object.keys(modelValue)" :key="index">{{modelValue[_key]}}</el-descriptions-item>
            </el-descriptions>
            <el-descriptions direction="vertical" v-else>
                <el-descriptions-item :label="item.ItemTitle">{{modelValue}}</el-descriptions-item>
            </el-descriptions>
        </el-card>
    </template>
    <template v-if="item.ItemType == 11">
        <el-divider />
    </template>
    <template v-if="item.ItemType == 12">
        <el-form-item :label="item.ItemTitle" 
            :label-width="100">
            <el-link 
                type="primary" 
                :href="getHref(item, modelValue)" target="_blank">{{item.ItemDesc}}
            </el-link>
        </el-form-item>
    </template>
    <template v-if="item.ItemType == 13">
        <FixedDescription v-model="item" />
    </template>
    <template v-if="item.ItemType == 14">
        <el-form-item :label="item.ItemTitle" 
            :label-width="100"
            v-if="modelValue">
            <el-alert type="warning" :closable="false" v-if="modelValue" effect="dark">
                <template #title>
                    <label>{{modelValue}}</label>
                </template>
            </el-alert>
        </el-form-item>
    </template>
</template>
<script>
import MultipleCheckbox from "@/components/FormItem/MultipleCheckbox/index.vue"
import UploadImageFile from "@/components/FormItem/UploadImageFile/index.vue"
import FixedDescription from "@/components/FormItem/FixedDescription/index.vue"

import getRules from "./validateRule.js"

import { reactive, onMounted, toRef, toRefs, ref  } from 'vue'
import { ElMessageBox } from 'element-plus';

export default{
    props:{
        item: { type: Object, required: true },
        modelValue: {type: [Object, String] },
        disabled: { type: Boolean },
        readonly: { type: Boolean }
    },
    emits:['update:modelValue'],
    components:{
        MultipleCheckbox,
        UploadImageFile,
        FixedDescription
    },
    setup(props, {emit}) {
     
        onMounted(() =>{
            if(props.item.ItemType == '1' && props.item.Data)
            {
                props.modelValue = props.item.Data;
            }

            if(props.item.ItemType == '9' && props.disabled != 'Y'){
                ElMessageBox.alert(props.item.ItemTitle, props.item.ItemValue, {
                    confirmButtonText: '確定',
                    dangerouslyUseHTMLString: true,
                });
            }
        })
       
        const getMultiple = (item) => {
            if("234".indexOf(item.ItemType) >= 0 && item.Multiple == 'Y'){
                return true
            }
            return false
        }

        const changeValue = () => {
            emit("update:modelValue", props.modelValue)
        }

        const uploadImageFile = ref();
        const uploadFile = () => {
            if(uploadImageFile.value){
                return uploadImageFile.value.upload()
            }
            return () =>{}
        }

        const commit = () => {
            if(uploadImageFile.value){
                return uploadImageFile.value.commit()
            }
            return () =>{}
        }

        const rollback = () => {
            if(uploadImageFile.value){
                return uploadImageFile.value.rollback()
            }
            return () =>{}
        }

        const getPlaceholder = (item, defaultText) => {
            if(item.ItemDesc){
                return item.ItemDesc
            }
            return defaultText + item.ItemTitle
        }

        const getHref = (item, modelvalue) => {
            let url = modelvalue
            if(item.DataRef === 'Constant'){
                url = item.ItemValue
                return url;
            }
            return import.meta.env.VITE_API_BASE_URL + url
        }
        return {
            changeValue,
            getRules,
            getMultiple,
            uploadFile,
            commit,
            rollback,
            uploadImageFile,
            getPlaceholder,
            getHref
        }
    },
}
</script>
<style scoped>
    .el-alert label{
        letter-spacing: 1.5px;
        font-size: 16px;
    }
</style>