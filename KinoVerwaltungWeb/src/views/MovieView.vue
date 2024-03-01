<template>
  <div>

    <div style="display: flex">
      <kino-select></kino-select>
      <saal-select></saal-select>
    </div>
    <film-overview></film-overview>
  </div>
</template>

<script>
import { defineComponent } from 'vue'
import kinoSelect from '@/components/kino/kinoSelect.vue'
import saalSelect from '@/components/kino/saalSelect.vue'
import filmOverview from '@/components/kino/filmOverview.vue'

export default defineComponent({
  name: 'MovieView',
  components: {
    kinoSelect,
    saalSelect,
    filmOverview
  },
})
</script>

<style scoped>

</style>