<template>
    <el-form-item label="類型"
                :prop="'items[' + index + '].ItemType'"
                :rules="{ required: true, message: '請輸入類型', trigger: 'blur' }">
                
        <ItemType v-model="modelValue.ItemType"
            v-model:item-type="modelValue.itemType"
            placeholder="請輸入類型"
            clearable>
        </ItemType>
    </el-form-item>
    <el-form-item label="抬頭"
                :prop="'items[' + index + '].ItemTitle'"
                :rules="{ required: true, message: '請輸入抬頭', trigger: 'blur' }">
        <el-input v-model="modelValue.ItemTitle"
            placeholder="請輸入抬頭"
            clearable>
        </el-input>
    </el-form-item>
    <el-form-item label="英文抬頭"
                :prop="'items[' + index + '].ItemTitleE'">
        <el-input v-model="modelValue.ItemTitleE"
            placeholder="請輸入英文抬頭"
            clearable>
        </el-input>
    </el-form-item>
    <el-form-item label="欄位值"
                :prop="'items[' + index + '].ItemValue'"
                :rules="{ required: true, message: '請輸入欄位值', trigger: 'blur' }">
        <el-input v-model="modelValue.ItemValue"
            placeholder="請輸入欄位值"
            clearable>
        </el-input>
    </el-form-item>
    <el-form-item label="說明">
        <el-input v-model="modelValue.ItemDesc" v-if="modelValue.ItemType === 13 && modelValue.ItemFormat !== 'validJson'"
            placeholder="請輸入說明"
            clearable
            type="textarea"
            :rows="4"
            :autosize="true">
        </el-input>
        <CodeEditor v-model="modelValue.ItemDesc" v-else-if="modelValue.ItemType === 13 && modelValue.ItemFormat === 'validJson'">
        </CodeEditor>
        <el-input v-model="modelValue.ItemDesc" v-else
            placeholder="請輸入說明"
            clearable
            type="textarea">
        </el-input>
    </el-form-item>
    <el-form-item label="格式">
        <el-select v-model="modelValue.ItemFormat"
            placeholder="請輸入格式"
            clearable>
            <el-option label="數字" value="validNumber"></el-option>
            <el-option label="手機" value="validPhone"></el-option>
            <el-option label="信箱" value="validEmail"></el-option>
            <el-option label="JSON" value="validJson"></el-option>
        </el-select>
    </el-form-item>
    <el-form-item label="資料來源">
        <DataRef v-model="modelValue.DataRef"
            placeholder="請選擇資料來源"
            clearable>
        </DataRef>
    </el-form-item>
    <el-form-item label="欄位屬性">
        <el-checkbox v-model="modelValue.Required" label="Y" true-label="Y" false-label="N">必填</el-checkbox>
        <el-checkbox v-model="modelValue.Multiple" label="Y" true-label="Y" false-label="N">多選</el-checkbox>
        <el-checkbox v-model="modelValue.Disabled" label="Y" true-label="Y" false-label="N">唯讀</el-checkbox>
    </el-form-item>

</template>
<script>

import ItemType from "./itemType.vue"
import DataRef from "./dataRef.vue"
import CodeEditor from "/@/components/CodeEditor/index.vue"

export default{
    props:['modelValue', 'index'],
    components:{
        ItemType,
        DataRef,
        CodeEditor
    },
    setup() {
        return {
        }
    },
}
</script>
