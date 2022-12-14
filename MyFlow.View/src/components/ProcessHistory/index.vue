<template>
    <el-card>
        <template #header>
            簽辦歷程
        </template>
        <el-table
            class="approveHistoryTb"
            :data="historyList.filter(o => !hidden || o.ApprId != hidden)"
            ref="singleTable"
            header-row-class-name="el-custom-header"
            border
            :default-expand-all="true"
            empty-text="查無資料"
            highlight-current-row
            style="width: 100%">
            <el-table-column type="expand">
                <template #default="props">
                    <p><strong>意見說明</strong><strong style="padding-left:10px">:</strong><span style="padding-left:20px">{{ props.row.ApproveMessage }}</span></p>
                </template>
            </el-table-column>
            <el-table-column
                prop="UserName"
                label="處理人">
            </el-table-column>
            <el-table-column
                prop="DeptName"
                label="處理單位">
            </el-table-column>
            <el-table-column
                prop="JobTitle"
                label="職稱">
            </el-table-column>
            <el-table-column
                prop="StageName"
                label="處理階段">
            </el-table-column>
            <el-table-column
                prop="StatusDesc"
                label="處理動作">
            </el-table-column>
            <el-table-column
                prop="CountDownToClose"
                label="表單關閉倒數">
                <template #default="scope">
                    <el-tag
                        v-if="scope.row.CountDownToClose != null && scope.row.CountDownToClose > 0"
                        :type="determineType(scope.row.CountDownToClose)"
                        effect="dark">{{getTimeDesc(scope.row.CountDownToClose)}}
                    </el-tag>
                </template>
            </el-table-column>
            <el-table-column
                prop="SubmitDate"
                label="處理日期">
                <template #default="scope">
                    {{$filters.formatDate(scope.row.SubmitDate)}}
                </template>
            </el-table-column>
        </el-table>
    </el-card>
    </template>
    
    <script>
    import { reactive, ref, onBeforeMount, computed, toRefs } from 'vue'
    import { getTimeDesc, determineType } from '@/utils/countdown.js'
    import { useStore } from 'vuex'
    
    export default{
        props:{
            applyid: { type: [String, Number]},
            apprid: { type: [String, Number]},
            hidden: { type: [String, Number]},
            disabled: { type: Boolean}
        },
        setup(props){
            const store = useStore()
            const refForm = ref({})
    
            const historyList = computed(()=>{
                return store.getters['process/processHistorys'];
            })
    
            const state = reactive({
            })
    
            onBeforeMount(() =>{
                store.dispatch('process/loadProcessHistory', props.applyid )
            })
    
            return {
                state,
                refForm,
                historyList,
                getTimeDesc,
                determineType,
            }
        }
    }
    
    </script>
    <style lang="scss" >
        .approveHistoryTb {
            td.el-table__expanded-cell[class*=cell]{
                    padding: 1px 50px;
            }
        }
    </style>
    