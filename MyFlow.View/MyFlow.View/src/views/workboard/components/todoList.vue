<template>
    <el-card>
        <template #header>
            待辦表單
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
                v-if="device === 'mobile'"
                 type="expand">
                <template #default="props">
                    <p>申請單號: {{ props.row.ApplyId }}</p>
                    <p>表單名稱: {{ props.row.FormName }}</p>
                    <p>階段名稱: {{ props.row.StageName }}</p>
                    <p>申請人: {{ props.row.ApplyUser }}</p>
                    <p>申請日期: {{ props.row.ApplyDate }}</p>
                </template>
            </el-table-column>
            <el-table-column
                v-if="device !== 'mobile'"
                prop="ApplyId"
                label="申請單號"
                width="80">
            </el-table-column>
            <el-table-column
                v-if="device !== 'mobile'"
                prop="FormName"
                label="表單名稱"
                width="180">
            </el-table-column>
            <el-table-column
                v-if="device !== 'mobile'"
                prop="StageName"
                label="階段名稱"
                width="180">
            </el-table-column>
            <el-table-column
                v-if="device !== 'mobile'"
                prop="ApplyUser"
                label="申請人"
                width="100">
            </el-table-column>
            <el-table-column
                prop="Title"
                label="主旨/標題"
                >
            </el-table-column>
            <el-table-column
                v-if="device !== 'mobile'"
                prop="ApplyDate"
                label="申請日期"
                width="190">
            </el-table-column>
            <el-table-column
                prop="CountDownToNotice"
                label="處理時間倒數"
                width="100"
                >
                <template #default="scope">
                    <el-tag
                        v-if="scope.row.CountDownToNotice != null"
                        :type="determineType(scope.row.CountDownToNotice)"
                        effect="dark">{{scope.row.CountDownToNotice}}{{$t('天')}}
                    </el-tag>
                </template>
            </el-table-column>
            <el-table-column
                prop="CountDownToClose"
                label="表單關閉倒數"
                width="100">
                <template #default="scope">
                    <el-tag
                        v-if="scope.row.CountDownToClose != null"
                        :type="determineType(scope.row.CountDownToClose)"
                        effect="dark">{{scope.row.CountDownToClose}}{{$t('天')}}
                    </el-tag>
                </template>
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
export default {
    components:{
    },
    setup() {

        const store = useStore()
        const route = useRoute()
        const router = useRouter()

        const state = reactive({
            tableData: [],
            title: route.meta.title,
            dialogVisible: false,
            selectedRow: {}
        })

        const determineType = (deadline) =>{
            if(deadline > 2){
                return 'info'
            } 

            if(deadline <= 2 && deadline > 0){
                return 'warning'
            }

            if(deadline <= 0){
                return 'danger'
            }
        }

        const list = computed(() =>{
            return store.getters['todoList/dataList']
        })

        const hanldRowClick = (row, column, event) =>{
            router.push('/approveForm/' + row.FlowId + '/'+ row.CurrentStage + '/' + row.ApplyId + '/' + row.DataId )
        }

        onBeforeMount(() =>{
            query()
        })

        const pageIndex = computed({
            get: () => {
                return store.getters['todoList/pageIndex']
            },
            set: val => {
                store.commit('todoList/setPageIndex', val)
            }
        })

        const rowNumber = computed(() =>{
            return store.getters['todoList/rowNumber']
        })

        const total = computed(() =>{
            return store.getters['todoList/total']
        })

        const query = () =>{
            store.dispatch('todoList/getTodoList')
        }
        const currentPageChange = () =>{
            query()
        }
        
        const device = computed(() =>{
            return store.getters['app/device']
        })

        return {
            state,
            list,
            determineType,
            hanldRowClick,
            pageIndex,
            rowNumber,
            total,
            currentPageChange,
            device
        }
    },
}
</script>
