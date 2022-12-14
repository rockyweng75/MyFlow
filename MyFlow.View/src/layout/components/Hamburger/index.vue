<template>
  <div style="padding: 0 15px;" @click="toggleClick">
    <!-- <i :class="{'is-active':isActive}" class="hamburger el-icon-s-fold">
    </i> -->
      <el-icon class="hamburger">
          <Fold v-if="isActive" />
          <Expand v-else />
      </el-icon>
  </div>
</template>

<script>
import Fold from '@element-plus/icons/lib/Fold'
import Expand from '@element-plus/icons/lib/Expand'

export default {
  name: 'Hamburger',
  components:{
    Fold,
    Expand
  },
  props: {
    isActive: {
      type: Boolean,
      default: false
    }
  },
  methods: {
    toggleClick() {
      this.$emit('toggleClick')
    }
  }
}
</script>

<style scoped>
.hamburger {
  display: inline-block;
  vertical-align: middle;
  width: 20px;
  height: 20px;
  font-size: 22px;
  color: #fff;
}

.hamburger.is-active {
  transform: rotate(180deg);
}
</style>
