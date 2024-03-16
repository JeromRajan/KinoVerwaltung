<template>
<div>
  <h2 class="text-h4 font-weight-black text-blue-darken-2 mt-5">{{ $t('EmployeeArea.title') }}</h2>
  <div class="mt-5" style="display: flex">
    <generateMembercard></generateMembercard>
    <loadMembercard></loadMembercard>
  </div>
  <div class="mt-10" style="display: flex">
    <confirmTicket></confirmTicket>
    <buyTicketWithCash></buyTicketWithCash>
    <buyTicketWithMembercard></buyTicketWithMembercard>
  </div>
</div>
</template>

<script>
import generateMembercard from '@/components/mitarbeiterBereich/generateMitgliederkarte.vue'
import loadMembercard from '@/components/mitarbeiterBereich/loadMitgliederkarte.vue'
import confirmTicket from '@/components/mitarbeiterBereich/confirmTicket.vue'
import buyTicketWithCash from '@/components/mitarbeiterBereich/buyTicketBar.vue'
import buyTicketWithMembercard from '@/components/mitarbeiterBereich/buyTicketMitgliederkarte.vue'

export default {
  name: 'EmployeeAreaView',
  components: {
    generateMembercard,
    loadMembercard,
    confirmTicket,
    buyTicketWithCash,
    buyTicketWithMembercard
  }

}
</script>

<style scoped>

</style>