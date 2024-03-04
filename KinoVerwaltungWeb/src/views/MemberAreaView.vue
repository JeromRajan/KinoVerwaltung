<template>
  <div>
    <membercard></membercard>
    <tickets></tickets>
  </div>
</template>

<script>
import membercard from '@/components/memberarea/membercard.vue'
import tickets from '@/components/memberarea/tickets.vue'
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