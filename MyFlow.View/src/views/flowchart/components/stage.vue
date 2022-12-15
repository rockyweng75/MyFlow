<template>
    <div>
        <el-card>
            <el-form-item label="順序" 
                    :prop="'stages['+ index +'].OrderId'"
                        :rules="{ required: true, message: '請輸入順序', trigger: 'blur' }">
                <el-input-number v-model="modelValue.OrderId"
                    placeholder="請輸入順序">
                </el-input-number>
            </el-form-item>
            <el-form-item label="階段名稱" 
                        :prop="'stages['+ index +'].StageName'"
                        :rules="{ required: true, message: '請輸入階段名稱', trigger: 'blur' }">
                <el-input v-model="modelValue.StageName"
                    placeholder="請輸入階段名稱">
                </el-input>
            </el-form-item>
            <el-form-item label="階段英文名稱" 
                        :prop="'stages['+ index +'].StageEname'"
                        :rules="{ required: true, message: '請輸入階段英文名稱', trigger: 'blur' }">
                <el-input v-model="modelValue.StageEname"
                    placeholder="請輸入階段英文名稱">
                </el-input>
            </el-form-item>
            <el-form-item label="階段目標" 
                        :prop="'stages['+ index +'].Target'"
                        :rules="{ required: true, message: '請選擇階段目標', trigger: 'blur' }">
                    <Target v-model="modelValue.Target"
                        placeholder="請輸入階段目標">
                    </Target>
                    <el-input v-model="modelValue.TargetParams" v-if="modelValue.Target== 'CustomApprove'"
                        placeholder="請輸入自訂參數，常數宣告請使用'常數值'">
                    </el-input>
            </el-form-item>
            <el-form-item label="階段時效" 
                        :prop="'stages['+ index +'].Deadline'"
                        :rules="{ required: true, message: '請輸入階段時效', trigger: 'blur' }">
                <Deadline v-model="modelValue.Deadline"></Deadline>
            </el-form-item>
        </el-card>
        <slot name="footer" :stage="modelValue"/>
    </div>
</template>
<script>
import Target from "./target.vue"
import Deadline from "./deadline.vue"
// import Forward from "./forward.vue"
// import Backward from "./backward.vue"
// import Requirement from "./requirement.vue"

// import Err from './error.vue'

export default{
    props:['index', 'modelValue', 'disabled'],
    components:{
        Target,
        Deadline,
        // Err,
        // Forward,
        // Backward,
        // Requirement,
    },
    setup() {
        return {
        }
    },
}
</script>
