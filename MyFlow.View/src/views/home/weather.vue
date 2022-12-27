<template>
    <el-card style="width:100%">
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
</template>
<script>

    import { onBeforeMount, computed, ref } from 'vue'
    import { useStore } from 'vuex'
    import { Sunny, MostlyCloudy, PartlyCloudy, Pouring, Lightning, WarningFilled } from '@element-plus/icons-vue'

    export default({
        components:{
            Sunny,
            MostlyCloudy, 
            PartlyCloudy, 
            Pouring, 
            Lightning,
            WarningFilled,
        },
        setup(){

            const store = useStore()
            // if ('geolocation' in navigator) {
            //   /* geolocation is available */
            //   navigator.geolocation.getCurrentPosition((position) => {
            //     doSomething(position.coords.latitude, position.coords.longitude);
            //     // console.log(position)
            //   });
            
            // } else {
            //   /* geolocation IS NOT available */
            // }

            const weatherLoading = ref(true)
            const weather = computed(() =>{
                return store.getters['weather/model'];
            });

            onBeforeMount(async () =>{
                await store.dispatch('weather/getOne')
                weatherLoading.value = false
            })

            return {
                weatherLoading,
                weather,
            }
        }
    })

</script>
<style scoped>

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

</style>