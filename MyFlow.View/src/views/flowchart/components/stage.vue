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
  
            <el-form-item label="同步群組代號" 
                        :prop="'stages['+ index +'].AsyncKey'">
                <el-input v-model="modelValue.AsyncKey"
                    placeholder="同步群組代號">
                </el-input>
            </el-form-item>
            <el-form-item label="執行群代號(If-Else)" 
                        :prop="'stages['+ index +'].GroupKey'">
                <el-input v-model="modelValue.GroupKey"
                    placeholder="請輸入階段群組">
                </el-input>
            </el-form-item>
            <el-form-item label="執行條件" 
                        :prop="'stages['+ index +'].Requirement'">
                <Requirement v-model="modelValue.Requirement"
                    placeholder="請輸入階段條件">
                </Requirement>
            </el-form-item>
            <el-form-item label="執行序群代號" 
                        :prop="'stages['+ index +'].ThreadKey'">
                <el-input v-model="modelValue.ThreadKey"
                    placeholder="執行序群代號">
                </el-input>
            </el-form-item>
            <el-form-item label="階段時效" 
                        :prop="'stages['+ index +'].Deadline'"
                        :rules="{ required: true, message: '請輸入階段時效', trigger: 'blur' }">
                <!-- <transition :key="modelValue.Deadline"> -->
                    <Deadline v-model="modelValue.Deadline"></Deadline>
                <!-- </transition> -->
            </el-form-item>
            <el-form-item label="執行成功" 
                        :prop="'stages['+ index +'].Success'"
                        :rules="{ required: true, message: '請輸入執行成功', trigger: 'blur' }">
                <!-- <transition :key="modelValue.Success"> -->
                    <Forward v-model="modelValue.Success"></Forward>
                <!-- </transition> -->
            </el-form-item>
            <el-form-item label="執行失敗" 
                        :prop="'stages['+ index +'].Fail'"
                        :rules="{ required: true, message: '請輸入執行失敗', trigger: 'blur' }">
                <!-- <transition :key="modelValue.Fail"> -->
                    <Err v-model="modelValue.Fail"></Err>
                <!-- </transition> -->
            </el-form-item>
            <el-form-item label="執行退回" 
                        :prop="'stages['+ index +'].Cancel'"
                        :rules="{ required: true, message: '請輸入執行退回', trigger: 'blur' }">
                <!-- <transition :key="modelValue.Cancel"> -->
                    <Backward v-model="modelValue.Cancel"></Backward>
                <!-- </transition> -->
            </el-form-item>
            <el-form-item label="關閉使用" 
                        :prop="'stages['+ index +'].Close'">
                <el-checkbox v-model="modelValue.Close">
                </el-checkbox>
            </el-form-item>
        </el-card>
        <slot name="footer" :stage="modelValue"/>
    </div>
</template>
<script>
import Target from "./target.vue"
import Forward from "./forward.vue"
import Backward from "./backward.vue"
import Requirement from "./requirement.vue"
import Deadline from "./deadline.vue"

import Err from './error.vue'

export default{
    props:['index', 'modelValue', 'disabled'],
    components:{
        Target,
        Err,
        Forward,
        Backward,
        Requirement,
        Deadline,
    },
    setup() {
        return {
        }
    },
}
</script>
