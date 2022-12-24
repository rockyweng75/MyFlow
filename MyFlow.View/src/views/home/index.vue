<template>
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
        <div class="infinite-list-wrapper" style="overflow: auto" v-if="bulletinList.length > 0">
          <ul
            v-infinite-scroll="load"
            class="list"
            :infinite-scroll-disabled="disabled"
          >
            <li v-for="(bulletin, index) in bulletinList" :key="index" class="list-item">
              <p class="day">
                <el-icon :size="15"><Star /></el-icon>
                {{ $moment(bulletin.BeginDate).format('L') }}
              </p>
              <p class="title">
                {{ bulletin.Title }}
              </p>
            </li>
            <p v-if="loading">Loading...</p>
            <p v-if="noMore">No more</p>
          </ul>
        </div>
        <el-empty v-else description="暫時沒有公告" />
      </el-card>
    </el-col>
  </el-row>
</template>

<script>
import { onBeforeMount, computed, ref } from 'vue'
import { useStore } from 'vuex'
import { useRouter } from 'vue-router'
import { Star } from '@element-plus/icons-vue'
import moment from 'moment/min/moment-with-locales'

export default({
  components:{
    Star
  },
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
    })

    const bulletinList = computed(() =>{

      return [
        { 
          BeginDate: moment().add(-1, 'days'),
          EndDate:  moment().add(2, 'days'),
          Title: '簽核系統上線囉',
          Content: ''
        },
        { 
          BeginDate: moment().add(-3, 'days'),
          EndDate:  moment().add(2, 'days'),
          Title: '消防演練公告',
          Content: ''
        },
        { 
          BeginDate: moment().add(-5, 'days'),
          EndDate:  moment().add(2, 'days'),
          Title: '測試環境即將關閉',
          Content: ''
        },
        { 
          BeginDate: moment().add(-6, 'days'),
          EndDate:  moment().add(2, 'days'),
          Title: '測試案例5',
          Content: ''
        },
        { 
          BeginDate: moment().add(-6, 'days'),
          EndDate:  moment().add(2, 'days'),
          Title: '測試案例4',
          Content: ''
        },
        { 
          BeginDate: moment().add(-6, 'days'),
          EndDate:  moment().add(2, 'days'),
          Title: '測試案例3',
          Content: ''
        },
      ];
    })

    const count = ref(bulletinList.value.length);  
    const loading = ref(false)
    const noMore = computed(() => count.value >= bulletinList.value.length)
    const disabled = computed(() => loading.value || noMore.value)
    const load = () => {
      loading.value = true
      setTimeout(() => {
        count.value += 2
        loading.value = false
      }, 2000)
    }

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
      count,
      loading,
      noMore, 
      disabled,
      todoList,
      waitList,
      scheduleList,
      bulletinList,
      device,
      gotoTodo,
      gotoWait,
      load
    }
  },
})
</script>
<style>
  .clock{
    background-color: gray;
    color: white;
  }
  .infinite-list-wrapper {
    height: 300px;
    text-align: left;
  }
  .infinite-list-wrapper .list {
    padding: 0;
    margin: 0;
    /* list-style: none; */
  }

  .infinite-list-wrapper .list-item {
    height: 50px;
    background: gainsboro;
  }

  .infinite-list-wrapper .list-item p.day{
    margin: 2px 0 2px 0;
    padding-top: 5px;
    color: dimgray;
  }
  .infinite-list-wrapper .list-item p.title{
    margin: 2px 0px 0px 5em;
    font-weight: bold;
    color: black;
  }

  /* .infinite-list-wrapper .list-item + .list-item {
    margin-top: 2px;
  } */
</style>