<template>
  {{ device }}
  <el-row>
    <el-card style="width:100%">
      <el-row>
        <el-col :span="device !== 'mobile'? 6 : 24">
          <el-button type="warning" style="width:100%" effect="dark" round v-on:click="gotoTodo">
            <h2>
              <el-space wrap :size="28">待辦:</el-space>
              <el-space wrap :size="28">{{todoList.length}} 筆</el-space>
            </h2>
          </el-button>
        </el-col>
        <el-col :span="device !== 'mobile'? 6 : 24">
          <el-button type="primary" style="width:100%" effect="dark" round v-on:click="gotoWait">
            <h2>
              <el-space wrap :size="28">等待:</el-space>
              <el-space wrap :size="28">{{waitList.length}} 筆</el-space>
            </h2>
          </el-button>
        </el-col>
        <el-col :span="device !== 'mobile'? 12 : 24">
          <el-button class="clock" style="width:100%" effect="dark" round>
            <h3>
              {{ time.toLocaleDateString() }} {{ time.toLocaleTimeString() }}
            </h3>
          </el-button>
        </el-col>
      </el-row>
    </el-card>
  </el-row>
  <el-row>
    <el-col :span="device !== 'mobile'? 10 : 24">
      <el-row>
        <el-card style="width:100%">
          <template #header>
            本日行程
          </template>
          <el-timeline v-if="scheduleList.length > 0">
              <el-timeline-item
                v-for="(schedule, index) in scheduleList.filter(o => o.BeginDate > new Date())"
                :key="index"
                :timestamp="$moment(schedule.BeginDate).format('hh:mm') + ' ~ ' + $moment(schedule.EndDate).format('hh:mm')"
                placement="top"
              >
                  <p>{{ schedule.Name }}</p>
                  <p >
                    {{ $moment(schedule.BeginDate).fromNow() }} <span style="color:red">{{$t('開始')}}</span>
                  </p>
              </el-timeline-item>
          </el-timeline>
          <div v-else>
            <el-empty description="暫時沒有開放的申請" />
          </div>
        </el-card>
      </el-row>
    </el-col>
    <el-col :span="device !== 'mobile'? 14 : 24">
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
import { onBeforeMount, computed, ref } from 'vue'
import { useStore } from 'vuex'
import { useRouter } from 'vue-router'

export default({

  setup() {
    const store = useStore()
    const router = useRouter()

    const time = ref(new Date())

    setInterval(() => {
      time.value = new Date();
    }, 1000);

    const todoList = computed(() =>{
        return store.getters['todoList/dataList']
    })
    const waitList = computed(() =>{
        return store.getters['waitList/dataList']
    })

    const scheduleList = computed(() =>{

      return [
        { 
          BeginDate: new Date().setHours(8, 10, 0, 0),
          EndDate: new Date().setHours(11, 0, 0, 0),
          Name: '工作會報'
        },
        { 
          BeginDate: new Date().setHours(14, 10, 0, 0),
          EndDate: new Date().setHours(15, 0, 0, 0),
          Name: '會議'
        },
        { 
          BeginDate: new Date().setHours(16, 50, 0, 0),
          EndDate: new Date().setHours(17, 0, 0, 0),
          Name: '作業交接'
        }
      ];


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

    onBeforeMount(async () =>{
      await store.dispatch('todoList/getTodoList')
      await store.dispatch('waitList/getWaitList')
    })

    const device = computed(() =>{
        return store.getters['app/device']
    })

    const gotoTodo = () =>{
      router.push('/workboard/todoList')
    }

    const gotoWait = () =>{
      router.push('/workboard/waitList')
    }


    return {
      time,
      todoList,
      waitList,
      scheduleList,
      device,
      gotoTodo,
      gotoWait
    }
  },
})
</script>
<style>
  .clock{
    background-color: black;
    color: white;
  }
</style>