<template>
<div>
  <h2 class="text-h4 font-weight-black text-blue-darken-2 mt-5">{{ $t('EmployeeArea.title') }}</h2>
  <div class="flex flex-wrap mt-5">
    <generateMembercard></generateMembercard>
  </div>
  <div class="flex flex-wrap mt-5">
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