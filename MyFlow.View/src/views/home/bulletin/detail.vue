<template>
    <div class="from-info">
        <div class="border">
            <label>From :</label> <div class="text">{{ selected.From }}</div>
        </div>
    </div>
    <el-row>
        <el-card class="content">
            <template #header>
                <label>標題 :</label><div class="text">{{selected.Title}}</div>
            </template>
            <div class="text" v-html="selected.Content">
            </div>
        </el-card>
    </el-row>
    <div class="file-list">
        <label>附檔：</label>
        <div v-if="selected.Files && selected.Files.length > 0">
            <template v-for="(file,index) in selected.Files" :key="index">
            </template>
        </div>
        <div v-else class="text">無附檔</div>
    </div>
    <div class="time-info">
        <label>公告日期 :</label>
        <div class="text">
            {{ $dayjs(selected.BeginDate).format('YYYY-MM-DD') }} 至 {{ $dayjs(selected.EndDate).format('YYYY-MM-DD') }}
        </div>
    </div>
</template>
<script>

    import { onBeforeMount, computed, ref } from 'vue'
    import { useRoute } from 'vue-router'

    import { useStore } from 'vuex'

    export default({
        setup(){
            const store = useStore()
            const route = useRoute()
            console.log(route)
            onBeforeMount(() => {
                store.dispatch('bulletin/getOne', route.params.id)
            })

            const selected = computed(()=>{
                return store.getters['bulletin/selected']
            })
            return {
                selected
            }
        }
    })
</script>
<style scoped lang="scss">
    .from-info{
        padding: 2em 0.5em 0.5em 0.5em;
        .border{
            border-bottom: dashed 1px skyblue ;
            width: 100%;
            padding-bottom: 5px;
        }
        label {
            padding-left: 1em;
        }
        .text{
            padding-left: 1em;
            display: inline;
        }
    }

    .content {
        width: 100%;
        .el-card__header{
            width: 100%;

            label {
                padding-left: 1em;
            }
            .text{
                padding-left: 1em;
                display: inline;
            }
        }
  
        .el-card__body{

            .text{
                min-height: 400px;
                background-color: var(--el-color-info-light-7);
                padding: 0.5em 1em 0.5em 1em;
            }
        }
    }

    .time-info{
        padding: 0.5em 0.5em 0.5em 0.5em;
        text-align: end;

        .border{
            border-bottom: dashed 1px skyblue ;
            width: 100%;
            padding-bottom: 5px;
        }
        label {
            padding-left: 1em;     
        }
  
        .text{
            padding-left: 1em;
            display: inline;
        }
    }
 
    .file-list{
        padding: 0.5em 0.5em 0.5em 0.5em;
        border: 1px solid skyblue;
        background-color: white;

        label {
            padding-left: 1em;     
        }

        .text{
            padding-left: 1em;
            display: inline;
        }
  
    }
</style>