<template>
    <el-card style="width:100%">
        <div class="workboard-header">
          系統時間
        </div>
        <div class="workboard-clock" >
          <el-row class="date">
            <span>{{ time.toLocaleDateString() }}</span>
          </el-row>
          <el-row class="time">
            <span>{{ time.toLocaleTimeString() }}</span>
          </el-row>
        </div>
    </el-card>
</template>
<script>
    import { ref } from 'vue'

    export default({
      setup(){
        const time = ref(new Date())

        setInterval(() => {
            time.value = new Date();
        }, 1000);
        
        return {
            time
        }
      }
    })
</script>
<style scoped>
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
</style>