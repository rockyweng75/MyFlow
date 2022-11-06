<template>
    <el-card>
        <template #header>
            <el-row justify="space-between">
                <el-col :span="12">
                    查詢條件
                </el-col>
                <el-col :span="12" style="text-align:end">
                </el-col>
            </el-row>
        </template>
        <el-form ref="" :model="state.queryForm">
            <AcadSeme v-model="state.queryForm"></AcadSeme>
            <el-form-item label="流程編號" :label-width="100">
                <el-input v-model="state.queryForm.FlowId"></el-input>
            </el-form-item>
            <el-form-item label="流程名稱" :label-width="100">
                <el-input v-model="state.queryForm.FlowName"></el-input>
            </el-form-item>
        </el-form>
        <el-button type="primary" v-on:click="handleClick">查詢</el-button>
    </el-card>
    <el-card>
        <template #header>
          <el-row justify="space-between">
            <el-col :span="12">
              開放時間
            </el-col>
            <el-col :span="12" style="text-align:end">
              <!-- <el-button type="success" plain round v-on:click="toCreate">新增</el-button> -->
            </el-col>
          </el-row>
        </template>
        <el-table
            :data="list"
            ref="singleTable"
            header-row-class-name="el-custom-header"
            border
            empty-text="查無資料"
            highlight-current-row
            v-on:row-click="hanldRowClick"
            style="width: 100%">
            <el-table-column
                prop="AcadYear"
                label="學年"
                width="60">
            </el-table-column>
            <el-table-column
                prop="SemeType"
                label="學期"
                width="50">
            </el-table-column>
            <el-table-column
                prop="FlowId"
                label="編號"
                width="70">
            </el-table-column>
            <el-table-column
                prop="FlowName"
                label="流程名稱"
                width="200">
            </el-table-column>
            <el-table-column
                prop="flowTypeName"
                label="流程類型"
                width="180">
            </el-table-column>
            <el-table-column
                prop="BeginDate"
                label="開始時間"
                width="180">
            </el-table-column>
            <el-table-column
                prop="EndDate"
                label="結束時間"
                width="180">
            </el-table-column>
            <el-table-column
                prop="PlusDays"
                label="延長天數"
                width="80">
            </el-table-column>
        </el-table>

        <el-pagination
            :page-size="rowNumber"
            :current-page="pageIndex"
            layout="prev, pager, next"
            :total="total"
            v-on:current-change="currentPageChange"
        >
        </el-pagination>
    </el-card>
</template>

<script>
import { reactive, computed, onBeforeMount } from 'vue'
import { useStore } from 'vuex'
import { useRoute, useRouter } from 'vue-router'
import AcadSeme from '/@/components/AcadSeme/index.vue'

export default {
    components:{
        AcadSeme
    },
    setup() {

        const store = useStore()
        const route = useRoute()
        const router = useRouter()
        const state = reactive({
            tableData: [],
            title: route.meta.title,
            dialogVisible: false,
            selectedRow: {},
            queryForm:{
                AcadYear: '',
                SemeType: '',
                FlowId: '',
                FlowName: ''
            }
        })
        const list = computed(() =>{
            return store.getters['eDeadline/deadlines']
        })

        const hanldRowClick = (row, column, event) =>{
            if(row.DeadlineId){
                router.push('/eDeadline/edit/' + row.DeadlineId)
            } else {
                router.push('/eDeadline/create/' + row.AcadYear + '/' + row.SemeType + '/' + row.FlowId)
            }
        }

        const pageIndex = computed({
            get: () => {
                return store.getters['eDeadline/pageIndex']
            },
            set: val => {
                store.commit('eDeadline/setPageIndex', val)
            }
        })

        const rowNumber = computed(() =>{
            return store.getters['eDeadline/rowNumber']
        })

        const total = computed(() =>{
            return store.getters['eDeadline/total']
        })

        const query = () =>{
            store.dispatch('eDeadline/getDeadlines', state.queryForm)
        }

        const currentPageChange = () =>{
            query(state.queryForm)
        }

        onBeforeMount(() =>{
            query(state.queryForm)
        })

        const handleClick = () =>{
            query(state.queryForm)
        }

        return {
            state,
            list,
            hanldRowClick,
            pageIndex,
            rowNumber,
            total,
            currentPageChange,
            handleClick
        }
    },
}
</script>
