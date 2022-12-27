<template>
    <el-card>
        <div class="workboard-header">
            快速查詢
        </div>
        <div class="workboard-search">
            <el-input
            v-model="search"
            placeholder="Please input"
            class="input-with-select"
            >
            <template #append>
                <el-button>
                <el-icon :size="15"><Search /></el-icon>
                </el-button>
            </template>
            </el-input>
        </div>
    </el-card>
</template>
<script>

    import { Search } from '@element-plus/icons-vue'
    import { onBeforeMount, computed, ref } from 'vue'
    import { useStore } from 'vuex'

    export default({
        components:{
            Search
        },
        setup(){
            const search = ref('')
            return {
                search,
            }
        }
    })
</script>