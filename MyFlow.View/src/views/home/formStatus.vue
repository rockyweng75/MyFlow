<template>
    <el-card style="width:100%" >
        <div class="workboard-header">
            即時狀態
        </div>
        <div class="workboard-todo" v-loading="todoListLoading">
            <el-row justify="space-between" v-on:click="gotoTodo" class="workboard-button" >
                <el-col :span="10">
                    <span class="dot bg-warning"></span>
                    <spad>待辦</spad>
                </el-col>
                <el-col :span="14" style="text-align:end">
                    <el-space wrap :size="28">{{todoList.length}} 筆</el-space>
                </el-col>
            </el-row>
        </div>
        <div class="workboard-wait" v-loading="waitListLoading">
            <el-row justify="space-between" v-on:click="gotoWait" class="workboard-button">
                <el-col :span="10">
                    <span class="dot bg-primary"></span>
                    <span>等待</span>
                </el-col>
                <el-col :span="14" style="text-align:end">
                    <el-space wrap :size="28">{{waitList.length}} 筆</el-space>
                </el-col>
            </el-row>
        </div>
    </el-card>
</template>
<script>
    import { onBeforeMount, computed, ref } from 'vue'
    import { useStore } from 'vuex'
    import { useRouter } from 'vue-router'

    export default({

        setup() {
            const store = useStore()
            const router = useRouter()

            const todoListLoading = ref(true)
            const todoList = computed(() =>{
                return store.getters['todoList/dataList']
            })

            const waitListLoading = ref(true)
            const waitList = computed(() =>{
                return store.getters['waitList/dataList']
            })

            onBeforeMount(async () =>{
                await store.dispatch('todoList/getTodoList')
                todoListLoading.value = false
                await store.dispatch('waitList/getWaitList')
                waitListLoading.value = false
            })

            const gotoTodo = () =>{
                router.push('/workboard/todoList')
            }

            const gotoWait = () =>{
                router.push('/workboard/waitList')
            }

            return {
                todoListLoading,
                todoList,
                waitListLoading,
                waitList,
                gotoTodo,
                gotoWait,
            }
        }
    })
</script>