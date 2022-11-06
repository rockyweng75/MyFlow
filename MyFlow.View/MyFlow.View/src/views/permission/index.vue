<template>
   <el-card>
         <template #header>
          <el-row justify="space-between">
            <el-col :span="12">
                  授權列表
            </el-col>
            <el-col :span="12" style="text-align:end">
              <el-button type="success" plain round v-on:click="toCreate">新增</el-button>
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
                prop="Id"
                label="Id"
                width="80">
            </el-table-column>
            <el-table-column
                prop="UserId"
                label="員編"
                width="120">
            </el-table-column>
            <el-table-column
                prop="UserName"
                label="姓名"
                width="180">
            </el-table-column>
            <el-table-column
                prop="Role"
                label="角色代碼"
                width="60">
            </el-table-column>
            <el-table-column
                prop="RoleDesc"
                label="角色"
                width="180">
            </el-table-column>
            <el-table-column
                prop="SappRoleParam"
                label="授權系所代碼"
                width="80">
            </el-table-column>
            <el-table-column
                prop="SappRoleDesc"
                label="授權系所"
                width="180">
            </el-table-column>
        </el-table>
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
        const list = computed(() =>{
            return store.getters['permission/userRoles']
        })

        const hanldRowClick = (row, column, event) =>{
            router.push('/permission/edit/' + row.Id )
        }

        const toCreate = () =>{
            router.push('/permission/create')
        }

        onBeforeMount(() =>{
            store.dispatch('permission/getUserRoles')
        })

        return {
            state,
            list,
            hanldRowClick,
            toCreate
        }
    },
}
</script>
