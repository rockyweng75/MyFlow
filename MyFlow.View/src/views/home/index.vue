<template>
  <el-row>
    <el-col :span="device !== 'mobile'? 6 : 24">
      <el-card style="width:100%" class="workboard-block small">
        <div class="workboard-header">
          即時狀態
        </div>
        <div class="workboard-todo" >
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
        <div class="workboard-wait">
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
    </el-col>
    <el-col :span="device !== 'mobile'? 6 : 24">
      <el-card style="width:100%"  class="workboard-block small">
        <div class="workboard-header">
          快速查詢
        </div>
        <div class="workboard-search">
          <el-input
            v-model="input3"
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
    </el-col>
    <el-col :span="device !== 'mobile'? 6 : 24">
      <el-card style="width:100%"  class="workboard-block small">
        <div class="workboard-header">
          天氣
        </div>
        <div class="workboard-weather" v-loading="weatherLoading">
          <el-row v-if="weather">
            <el-col :span="5">
              <el-icon :size="40" class="image"><component :is="weather.Icon"></component></el-icon>
            </el-col>
            <el-col :span="13">
              <div class="local">{{weather.Location}}</div>
              <div class="weather">{{weather.Weather}}</div>
              <div class="humidity">下雨機率：{{ weather.ProbabilityOfPrecipitation * 100 }}%</div>
            </el-col>
            <el-col class="temperature" :span="6">
              <div class="current">{{weather.Temperature}}°</div>
              <div class="range">{{weather.MaximumTemperature}}° / {{weather.MinimumTemperature}}°</div>
            </el-col>
          </el-row>
          <div v-else>
            <el-row>
              <el-col :span="24">
                <el-icon :size="40"><WarningFilled /></el-icon>
              </el-col>
              <el-col :span="24">
                查無目前天氣
              </el-col>
            </el-row>
          </div>
        </div>
      </el-card>
    </el-col>
    <el-col :span="device !== 'mobile'? 6 : 24">
      <el-card style="width:100%"  class="workboard-block small">
        <div class="workboard-header">
          系統時間
        </div>
        <div class="workboard-clock" >
          <el-row class="date">
            <span>{{ time.toLocaleDateString() }}</span>
          </el-row>
          <el-row class="time">
            <span >{{ time.toLocaleTimeString() }}</span>
          </el-row>
        </div>
      </el-card>
    </el-col>
  </el-row>
  <el-row>
    <el-col :span="device !== 'mobile'? 6 : 24">
      <el-row>
        <el-card class="workboard-block medium">
          <template #header>
            本日行程
          </template>
          <el-timeline style="height: 300px;overflow: auto" v-if="scheduleList.length > 0">
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
            <el-empty description="無後續行程" />
          </div>
        </el-card>
      </el-row>
    </el-col>
    <el-col :span="device !== 'mobile'? 6 : 24">
      <el-row>
        <el-card class="workboard-block medium">
          <template #header>
            人員狀態
          </template>
          <ul class="board-list">
            <li class="list-item" v-for="(member,index) in memberList" :key="index">
              <el-row class="member-info" justify="space-between" >
                <el-col :span="20">{{ member.Name }}</el-col>
                <el-col :span="4" style="text-align:end">
                  <span class="avatar" :class='"bg-" + member.Status'></span>
                </el-col>
              </el-row>
              <el-row class="member-status" justify="space-between">
                <el-col :span="18">{{ member.Title }}</el-col>
                <el-col :span="6" style="text-align:end">分機:{{ member.TelExt }}</el-col>
              </el-row>
            </li>
          </ul>
        </el-card>
      </el-row>
    </el-col>
    <el-col :span="device !== 'mobile'? 12 : 24">
      <el-card style="width:100%" class="workboard-block medium">
        <template #header>
          公佈欄
        </template>
        <div style="height: 300px;overflow: auto" v-if="bulletinList.length > 0">
          <ul
            class="board-list"
            v-infinite-scroll="load"
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
            <p v-if="bulletinLoading">Loading...</p>
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
import { Star, Sunny, MostlyCloudy, PartlyCloudy, Pouring, Lightning, Search, WarningFilled } from '@element-plus/icons-vue'
import moment from 'moment/min/moment-with-locales'


export default({
  components:{
    Star,
    Sunny,
    MostlyCloudy, 
    PartlyCloudy, 
    Pouring, 
    Lightning,
    Search,
    WarningFilled
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
        },

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
          Title: '測試案例512312312313213213123132',
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

    const memberList = computed(() =>{
      return [
        { 
          Name: '陳大成',
          Title: '總經理',
          Status: 'online',
          Department: '',
          TelExt: '100'
        },
        { 
          Name: '王小玫',
          Title: '總經理秘書',
          Status: 'online',
          Department: '',
          TelExt: '100'
        },
        { 
          Name: '吳一天',
          Title: '人事部經理',
          Status: 'busy',
          Department: '',
          TelExt: '200'
        },
        { 
          Name: '李三',
          Title: '人事部部員',
          Status: 'offline',
          Department: '',
          TelExt: '202'
        },
      ]
    });

    const weatherLoading = ref(true)
    const weather = computed(() =>{
      // return store.getters['weather/model'];

      return {
        Location: 'YunLin',
        Weather: '晴天',
        ProbabilityOfPrecipitation: 0.5,
        Temperature: 31,
        MaximumTemperature: 32,
        MinimumTemperature: 12,
        Icon: 'Sunny',
      }
    });

    const count = ref(bulletinList.value.length);  
    const bulletinLoading = ref(false)
    const noMore = computed(() => count.value >= bulletinList.value.length)
    const disabled = computed(() => bulletinLoading.value || noMore.value)
    const load = () => {
      bulletinLoading.value = true
      setTimeout(() => {
        count.value += 2
        bulletinLoading.value = false
      }, 2000)
    }

    onBeforeMount(async () =>{
      await store.dispatch('todoList/getTodoList')
      await store.dispatch('waitList/getWaitList')
      await store.dispatch('weather/getOne')
      weatherLoading.value = false
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
      noMore, 
      disabled,
      todoList,
      waitList,
      scheduleList,
      bulletinLoading,
      bulletinList,
      memberList,
      weatherLoading,
      weather,
      device,
      gotoTodo,
      gotoWait,
      load
    }
  },
})
</script>


<style scoped>
  .board-list {
    padding: 0;
    margin: 0;
    list-style: none;
  }

  .list-item {
    min-height: 50px;
    border-bottom: 1px solid var(--el-card-border-color);
  }

  .list-item p.day{
    margin: 2px 0 2px 0;
    padding-top: 5px;
    color: dimgray;
  }
  .list-item p.title{
    margin: 2px 0px 0px 5em;
    font-weight: bold;
    color: black;
  }

  .workboard-block {
    width: 100%;
    padding-bottom: 10px;;
  }

  .workboard-block.small{
    height: 133px;
  }

  .workboard-block.medium{
    height: 340px;
  }

  .workboard-header{
    font-weight: bold;
    padding: 0 0 10px 0;
  }

  .workboard-button {
    cursor: pointer;
    transition: 0.1s;
  }

  .workboard-button {
    padding: 5px 10px 5px 10px;
  }
  .workboard-button:active {
    background-color: var(--el-fill-color);
  }

  .workboard-button:hover {
    background-color: var(--el-fill-color-light);
  }
  .workboard-clock{
    background: linear-gradient(mediumblue 0%, rgb(0, 153, 255) 80%);
    color: white;
    border-radius: 1ch;
  }

  .workboard-clock .date {
    padding: 10px 10px 0px 1em;
    font-size: medium;
    font-family:'Times New Roman', Times, serif;
    font-weight: bold;
  }
  .workboard-clock .time {
    padding: 0px 10px 5px 3em;
    font-size: larger;
    font-family:'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif
  }

  .workboard-weather {
    text-align:center;
  }

  .workboard-weather .image {
    color: goldenrod;
    padding-top: 14px;
  }


  .workboard-weather .local {
    font-size:large;
    font-weight:bold;
  }
  .workboard-weather .weather {
    font-size:small;
    color: goldenrod
  }

  .workboard-weather .humidity {
    font-size:small;
    font-weight:bold;
    color: var(--el-color-info)
  }

  .workboard-weather .temperature .current {
    font-size:xx-large;
    color: var(--el-color-primary);
  }

  .workboard-weather .temperature .range {
    font-size:small;
    color: var(--el-color-info);
  }

  .avatar {
    width: 0.625rem;
    height: 0.625rem;
    border-radius: 50%;
    display: inline-block;
    margin-right: 0.5rem;
  }

  .member-status{
    font-size: small;
    padding-left: 2em;
    padding-top: 5px;
    color: var(--el-color-info);
  }

  .dot {
      width: 0.625rem;
      height: 0.625rem;
      border-radius: 50%;
      display: inline-block;
      margin-right: 0.5rem;
  }
  .bg-primary {
    --falcon-bg-opacity: 1;
    background-color: var(--el-color-primary) !important;
  }
  .bg-success, .bg-online {
    --falcon-bg-opacity: 1;
    background-color: var(--el-color-success) !important;
  }

  .bg-warning, .bg-leave {
    --falcon-bg-opacity: 1;
    background-color: var(--el-color-warning) !important;
  }

  .bg-danger, .bg-busy{
    --falcon-bg-opacity: 1;
    background-color: var(--el-color-danger) !important;
  }

  .bg-info, .bg-offline{
    --falcon-bg-opacity: 1;
    background-color: var(--el-color-info) !important;
  }


</style>