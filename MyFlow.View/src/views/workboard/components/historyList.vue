<template>
    <el-card v-loading="loading">
        <template #header>
            個人申請記錄
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
                    <p>申請人: {{ props.row.ApplyUser }}</p>
                    <p>申請日期: {{ props.row.ApplyDate }}</p>
                    <p>完成日期: {{ props.row.FinishDate }}</p>
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
                width="140">
            </el-table-column>
            <el-table-column
                v-if="device !== 'mobile'"
                prop="ApplyUser"
                label="申請人"
                width="100">
            </el-table-column>
            <el-table-column
                prop="Title"
                label="主旨/標題">
            </el-table-column>
            <el-table-column
                v-if="device !== 'mobile'"
                prop="ApplyDate"
                label="申請日期"
                width="190">
            </el-table-column>
            <el-table-column
                prop="ApplyStatus"
                label="申請狀態"
                width="100">
            </el-table-column>
             <el-table-column
                v-if="device !== 'mobile'"
                prop="FinishDate"
                label="完成日期"
                width="190">
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
import { reactive, computed, onBeforeMount, ref } from 'vue'
import { useStore } from 'vuex'
import { useRoute, useRouter} from 'vue-router'

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
        const list = computed(() =>{
            return store.getters['historyList/dataList']
        })

        const hanldRowClick = (row, column, event) =>{
            router.push('/reviewForm/' + row.FlowId + '/'+ row.CurrentStage + '/' + row.ApplyId + '/' + row.DataId  )
        }

        const loading = ref(true)
        onBeforeMount(async () =>{
            await query()
            loading.value = false
        })

        const pageIndex = computed({
            get: () => {
                return store.getters['historyList/pageIndex']
            },
            set: val => {
                store.commit('historyList/setPageIndex', val)
            }
        })

        const rowNumber = computed(() =>{
            return store.getters['historyList/rowNumber']
        })

        const total = computed(() =>{
            return store.getters['historyList/total']
        })

        const query = () =>{
            store.dispatch('historyList/getHistoryList')
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
            hanldRowClick,
            pageIndex,
            rowNumber,
            total,
            currentPageChange,
            device,
            loading
        }
    },
}
</script>
