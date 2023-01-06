<template>
  <el-row>
    <el-col :span="device !== 'mobile'? 6 : 24" >
      <FormStatus class="workboard-block small"/>
    </el-col>
    <el-col :span="device !== 'mobile'? 6 : 24">
      <Search class="workboard-block small"/>
    </el-col>
    <el-col :span="device !== 'mobile'? 6 : 24">
      <Weather class="workboard-block small"/>
    </el-col>
    <el-col :span="device !== 'mobile'? 6 : 24">
      <Clock class="workboard-block small"/>
    </el-col>
  </el-row>
  <el-row>
    <el-col :span="device !== 'mobile'? 6 : 24">
      <Schedule class="workboard-block medium"/>
    </el-col>
    <el-col :span="device !== 'mobile'? 6 : 24">
      <Member class="workboard-block medium"/>
    </el-col>
    <el-col :span="device !== 'mobile'? 12 : 24">
      <Bulletin class="workboard-block medium"/>
    </el-col>
  </el-row>
</template>

<script>
import { onBeforeMount, computed, ref } from 'vue'
import { useStore } from 'vuex'
import FormStatus from './formStatus.vue'
import Weather from './weather.vue'
import Clock from './clock.vue'
import Schedule from './schedule.vue'
import Member from './member/index.vue'
import Bulletin from './bulletin.vue'
import Search from './search.vue'

export default({
  components:{
    Search,
    FormStatus,
    Weather,
    Clock,
    Schedule,
    Member,
    Bulletin,
    Search
  },
  setup() {
    const store = useStore()

    const device = computed(() =>{
        return store.getters['app/device']
    })

    return {
      device,
    }
  },
})
</script>


<style>
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