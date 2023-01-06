<template>
    <el-card>
        <template #header>
            人員狀態
        </template>
        <ul class="board-list">
            <li class="list-item member-button" v-for="(member,index) in memberList" :key="index" v-on:click="openChat" >
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
    <Chat></Chat>

</template>
<script>
    import { computed, ref, reactive, onBeforeMount } from 'vue'
    import { useStore } from 'vuex'
    import Chat from './chat.vue'
    export default({
        components:{
            Chat
        },
        setup(){
            const store = useStore()
            const memberList = computed(() =>{
                return store.getters['member/list']
            });

            const openChat = () =>{
                store.commit('chat/toggleDrawer', true)
            }
            onBeforeMount(()=>{
                store.dispatch('member/getList')
            })
            return{
                memberList,
                openChat
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

  .member-button {
    cursor: pointer;
    transition: 0.1s;
  }
  .member-button:active {
    background-color: var(--el-fill-color);
  }

  .member-button:hover {
    background-color: var(--el-fill-color-light);
  }

  .avatar {
    width: 0.625rem;
    height: 0.625rem;
    border-radius: 50%;
    display: inline-block;
    margin-right: 0.5rem;
  }
</style>