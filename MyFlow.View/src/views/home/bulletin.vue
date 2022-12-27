<template>
    <el-card style="width:100%">
        <template #header>
            公佈欄
        </template>
        <div style="height: 300px;overflow: auto" v-if="bulletinList.length > 0" v-loading="initLoading">
            <ul
                class="board-list"
                v-infinite-scroll="load"
                :infinite-scroll-distance="50"
            >
            <li v-for="(bulletin, index) in bulletinList" :key="index" class="list-item">
                <p class="day">
                <el-icon :size="15"><Star /></el-icon>
                {{ $dayjs(bulletin.BeginDate).format('YYYY-MM-DD') }}
                </p>
                <p class="title">
                {{ bulletin.Title }}
                </p>
            </li>
            <p v-if="bulletinLoading">Loading...</p>
            <p v-if="noMore">No more</p>
            </ul>
        </div>
        <el-empty v-else description="暫時沒有公告" />
    </el-card>
</template>
<script>

    import { onBeforeMount, computed, ref } from 'vue'
    import { useStore } from 'vuex'
    import { Star } from '@element-plus/icons-vue'

    export default({
        components:{
            Star
        },
        setup(){
            const store = useStore()

            const bulletinList = computed(() =>{
                return store.getters['bulletin/list']
            })
            
            const pagination = computed(() =>{
                return store.getters['bulletin/pagination']
            });

            const initLoading = ref(true)
            const bulletinLoading = ref(false)
            const noMore = computed(() => (pagination.value.pageIndex * pagination.value.pageCount) >= pagination.value.total)
            const disabled = computed(() => bulletinLoading.value || noMore.value)
            // todo fix
            const load = () => {
                bulletinLoading.value = true
                store.dispatch('bulletin/load', 
                { 
                    pageIndex: pagination.value.pageIndex + 1 
                }
                ).then(()=>{
                    bulletinLoading.value = false
                })
       
            }

            onBeforeMount(async ()=>{
                await store.dispatch('bulletin/getList')
                initLoading.value = false
            })

            return{
                bulletinList,
                pagination,
                initLoading,
                bulletinLoading,
                noMore,
                disabled,
                load
            }
        }
    })
</script>