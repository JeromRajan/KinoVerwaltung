<template>
<div>
  <h2 class="text-h4 font-weight-black text-blue-darken-2 mt-5">{{ $t('EmployeeArea.title') }}</h2>
  <div class="mt-5" style="display: flex">
    <generateMembercard></generateMembercard>
    <loadMembercard></loadMembercard>
  </div>
</div>
</template>

<script>
import generateMembercard from '@/components/employeearea/generateMembercard.vue'
import loadMembercard from '@/components/employeearea/loadMembercard.vue'
export default {
  name: 'EmployeeAreaView',
  components: {
    generateMembercard,
    loadMembercard
  }

}
</script>

<style scoped>

</style>