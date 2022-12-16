<template>
  <el-row>
    <el-col :span="device !== 'mobile'? 8 : 24">
      <el-row>
        <el-card style="width:100%">
          <el-button type="warning" effect="dark" v-on:click="gotoTodo">
            <h2>
              <el-space wrap :size="30">待處理件數:</el-space>
              <el-space wrap :size="30">{{todoList.length}} 筆</el-space>
            </h2>
          </el-button>
        </el-card>
        <el-card style="width:100%">
          <template #header>
            近期開放表單
          </template>
          <el-timeline v-if="deadlines.length > 0">
            <el-timeline-item
              v-for="(deadline, index) in deadlines"
              :key="index"
              :timestamp="$moment(deadline.BeginDate).format('YYYY-MM-DD hh:mm') + ' ~ ' + $moment(deadline.EndDate).format('YYYY-MM-DD hh:mm')"
              placement="top"
            >
              <p>{{ deadline.FlowName }}</p>
              <p>{{ $moment(deadline.EndDate).fromNow() }} <span style="color:red">{{$t('關閉')}}</span></p>
            </el-timeline-item>
          </el-timeline>
          <div v-else>
            <el-empty description="暫時沒有開放的申請" />
          </div>
        </el-card>
      </el-row>
    </el-col>
    <el-col :span="device !== 'mobile'? 16 : 24">
      <el-card style="width:100%">
        <template #header>
          公佈欄
        </template>
        <el-empty description="暫時沒有公告" />
      </el-card>
    </el-col>
  </el-row>
</template>

<script>
import { onBeforeMount, computed } from 'vue'
import { useStore } from 'vuex'
import { useRouter } from 'vue-router'

export default({

  setup() {
    const store = useStore()
    // const router = useRouter()
    const todoList = computed(() =>{
        // return store.getters['workboard/todoList']
        return 1;
    })

    const deadlines = computed(() =>{

      return [];


        // let list = store.getters['deadline/list']
        // let result = list.filter(o =>{
        //   return o.BeginDate && o.EndDate && o.EndDate > new Date().toISOString()
        // });

        // result = result.sort((a,b) =>{
        //   if(a.BeginDate < b.BeginDate) return -1;
        //   if(a.BeginDate > b.BeginDate) return 1;
        //   if(a.FlowId < b.FlowId) return -1;
        //   if(a.FlowId > b.FlowId) return 1;
        //   return 0;
        // })

        // return result;
    })

    // onBeforeMount(() =>{
    //   store.dispatch('eDeadline/getDeadlines').then(()=>{
    //     store.dispatch('workboard/getTodoList')
    //   })
    // })

    const device = computed(() =>{
        return store.getters['app/device']
    })

    const gotoTodo = () =>{
      router.push('/workboard/todoList')
    }

    return {
      todoList,
      deadlines,
      device,
      gotoTodo
    }
  },
})
</script>