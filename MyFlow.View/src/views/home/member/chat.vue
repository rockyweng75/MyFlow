<template>
    <el-drawer v-model="drawer" 
        :size="drawerSize"
        :withHeader="false"
        :destroy-on-close="true">
        <el-row>
            <el-button link @click="close" style="width: 30px">
                <el-icon color="black"><ArrowLeftBold /></el-icon>
            </el-button>
            <h4>{{ state.Target }}</h4>
        </el-row>
        <el-table class="chatList">

        </el-table>
        <el-divider />
        <el-form :model="state" :ref="chatForm">
            <el-form-item label="輸入訊息" 
                        prop="Message"
                        :label-width="80">
                <div style="display:inline;width:100%">
                    <ckeditor :editor="editor" v-model="state.Message" :config="editorConfig"></ckeditor>
                </div>
            </el-form-item>
        </el-form>
    </el-drawer>
</template>
<script>
    import { reactive, computed, ref, onBeforeUpdate, watch } from 'vue'
    import { useStore } from 'vuex'
    import { ArrowLeftBold } from '@element-plus/icons-vue'
    import ClassicEditor from '@ckeditor/ckeditor5-build-classic'

    export default({
        props:['modelValue', 'target'],
        emits:['update:modelValue'],
        components:{
            ArrowLeftBold
        },
        setup(props, {emit}){
            const store = useStore()
            const state = reactive({
                Target: 'Test',
                Message: ''
            })
            const chatForm = ref({})
            const drawerSize = computed(() =>{
                if( store.getters['app/device'] === 'mobile'){
                    return '100%'
                } else {
                    return '50%'
                }
            })

            const close = () =>{
                drawer.value = false
            }

            const drawer = computed({
                get() {
                    return store.getters['chat/showDrawer']
                },
                // setter
                set(newValue) {
                    store.commit('chat/toggleDrawer', newValue)
                }
            })

            return {
                state,
                chatForm,
                drawer,
                close,
                drawerSize,
                ClassicEditor,
                editor: ClassicEditor,
                editorConfig: {
                    // toolbar: [ 'heading', '|', 'bold', 'italic', 'link', 'bulletedList', 'numberedList', 'blockQuote' ],
                    // heading: {
                    //     options: [
                    //         { model: 'paragraph', title: 'Paragraph', class: 'ck-heading_paragraph' },
                    //         { model: 'heading1', view: 'h1', title: 'Heading 1', class: 'ck-heading_heading1' },
                    //         { model: 'heading2', view: 'h2', title: 'Heading 2', class: 'ck-heading_heading2' }
                    //     ]
                    // }
                }
            }
        }
    });
</script>
<style scoped>
    .chatList{
        background-color: black;
        color: white;
        border-radius: 1ch;
        height: 350px;
    }
</style>