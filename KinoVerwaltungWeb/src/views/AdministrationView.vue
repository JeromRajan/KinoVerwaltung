<template>
  <div>
    <h2 class="text-h4 font-weight-black text-blue-darken-2 mt-5">{{ $t('Administration.title') }}</h2>
    <div class="mt-5" style="display: flex">
      <addCinema></addCinema>
    </div>
    <div class="mt-5" style="display: flex">
      <movieOverview></movieOverview>
      <addMovie></addMovie>
      <editMovie></editMovie>
    </div>

    <div class="mt-5" style="display: flex">
      <saalOverview></saalOverview>
      <addSaal></addSaal>
      <editSaal></editSaal>
    </div>

    <div class="mt-5" style="display: flex">
      <vorstellungOverview></vorstellungOverview>
      <addVorstellung></addVorstellung>
      <editVorstellung></editVorstellung>
    </div>
  </div>
</template>

<script>
import { defineComponent } from 'vue'
import addMovie from '@/components/administration/addMovie.vue'
import addSaal from '@/components/administration/addSaal.vue'
import addVorstellung from '@/components/administration/addVorstellung.vue'
import editSaal from '@/components/administration/editSaal.vue'
import editVorstellung from '@/components/administration/editVorstellung.vue'
import saalOverview from '@/components/administration/saalOverview.vue'
import vorstellungOverview from '@/components/administration/vorstellungOverview.vue'
import editMovie from '@/components/administration/editMovie.vue'
import movieOverview from '@/components/administration/movieOverview.vue'
import addCinema from '@/components/administration/addCinema.vue'

export default defineComponent({
  components: {
    addMovie,
    addSaal,
    addVorstellung,
    editSaal,
    editVorstellung,
    saalOverview,
    vorstellungOverview,
    editMovie,
    movieOverview,
    addCinema
  }
})
</script>

<style scoped>

</style>