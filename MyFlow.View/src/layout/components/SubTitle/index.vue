<template>
  <div class="sub-container">
    <breadcrumb id="breadcrumb-container" class="breadcrumb-container" />
  </div>
</template>

<script>
import Breadcrumb from '../../components/Breadcrumb/index.vue'

export default {
  components: { Breadcrumb },
}
</script>
<style lang="scss" scoped>
    .sub-container{
        height: 40px;
        overflow: hidden;
        position: relative;
        background: #fff;
        -webkit-box-shadow: 0 1px 4px rgba(0, 21, 41, 0.08);
        box-shadow: 0 1px 1px -1px rgba(0, 0, 0, 0.34), 0 0 6px 0 rgba(0, 0, 0, 0.14);
        padding-left: 3ex;
        margin-bottom: 2px;
    }
    .breadcrumb-container {
        float: left;
    }
</style>