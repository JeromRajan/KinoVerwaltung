<template>
  <div>
    <membercard></membercard>
    <tickets></tickets>
  </div>
</template>

<script>
import membercard from '@/components/mitgliederBereich/mitgliederkarte.vue'
import tickets from '@/components/mitgliederBereich/tickets.vue'
export default {
  name: 'memberareaMember',
  components: {
    membercard,
    tickets
  }
}

</script>

<style scoped>

</style>