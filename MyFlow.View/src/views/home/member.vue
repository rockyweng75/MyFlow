<template>
    <el-card>
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
</template>
<script>
    import { computed, ref, onBeforeMount } from 'vue'
    import { useStore } from 'vuex'

    export default({
        setup(){
            const store = useStore()
            const memberList = computed(() =>{
                return store.getters['member/list']
            });

            onBeforeMount(()=>{
                store.dispatch('member/getList')
            })
            return{
                memberList
            }
        }
    })
</script>
<style scoped>

  .member-status {
    font-size: small;
    padding-left: 2em;
    padding-top: 5px;
    color: var(--el-color-info);
  }

  .avatar {
    width: 0.625rem;
    height: 0.625rem;
    border-radius: 50%;
    display: inline-block;
    margin-right: 0.5rem;
  }
</style>