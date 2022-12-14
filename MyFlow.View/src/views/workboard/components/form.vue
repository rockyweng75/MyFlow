<template>
    <ApplyForm :formid="state.formid"></ApplyForm>
</template>
<script>
import ApplyForm from '@/components/ApplyForm/index.vue'
import { reactive } from 'vue'
export default {
    components:{
        ApplyForm
    },
    props:['formid'],
    setup(props){
        const state = reactive({
            formid : props.formid
        })

        return {
            state
        }
    }
}
</script>