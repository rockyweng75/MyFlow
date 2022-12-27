<template>
    <el-card>
        <template #header>
          本日行程
        </template>
        <el-timeline style="height: 300px;overflow: auto" v-if="scheduleList.length > 0">
            <el-timeline-item
              v-for="(schedule, index) in scheduleList.filter(o => o.BeginDate > new Date())"
              :key="index"
              :timestamp="$dayjs(schedule.BeginDate).format('hh:mm') + ' ~ ' + $dayjs(schedule.EndDate).format('hh:mm')"
              placement="top"
            >
                <p>{{ schedule.Name }}</p>
                <p >
                  {{ $dayjs(schedule.BeginDate).fromNow(true) }} <span style="color:red">{{$t('開始')}}</span>
                </p>
            </el-timeline-item>
        </el-timeline>
        <div v-else>
          <el-empty description="無後續行程" />
        </div>
    </el-card>
</template>
<script>

    import { computed, ref } from 'vue'
    import { useStore } from 'vuex'

    export default({
        setup()
        {
            const store = useStore()

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

            return {
                scheduleList,

            }
        }
    })
</script>