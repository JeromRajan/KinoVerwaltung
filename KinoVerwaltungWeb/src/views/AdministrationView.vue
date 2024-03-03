<template>
  <div>
    <h2 class="text-h4 font-weight-black text-blue-darken-2 mt-5">{{ $t('Administration.title') }}</h2>

    <v-expansion-panels class="mt-5">
      <v-expansion-panel>
        <v-expansion-panel-title>
          <div class="text-h5 font-weight-black text-blue-darken-1">{{ $t('Administration.cinemaOverview') }}</div>
        </v-expansion-panel-title>
        <v-expansion-panel-text>
          <div class="mt-5" style="display: flex">
            <addCinema></addCinema>
          </div>
        </v-expansion-panel-text>
      </v-expansion-panel>
    </v-expansion-panels>

    <v-expansion-panels class="mt-5">
      <v-expansion-panel>
        <v-expansion-panel-title>
          <div class="text-h5 font-weight-black text-blue-darken-1">{{ $t('Administration.movieOverview') }}</div>
        </v-expansion-panel-title>
        <v-expansion-panel-text>
          <movieOverview></movieOverview>
        </v-expansion-panel-text>
      </v-expansion-panel>
    </v-expansion-panels>

    <v-expansion-panels class="mt-5">
      <v-expansion-panel>
        <v-expansion-panel-title>
          <div class="text-h5 font-weight-black text-blue-darken-1">{{ $t('Administration.hallOverview') }}</div>
        </v-expansion-panel-title>
        <v-expansion-panel-text>
          <div class="mt-5" style="display: flex">
            <saalOverview></saalOverview>
            <addSaal></addSaal>
            <editSaal></editSaal>
          </div>
        </v-expansion-panel-text>
      </v-expansion-panel>
    </v-expansion-panels>

    <v-expansion-panels class="mt-5">
      <v-expansion-panel>
        <v-expansion-panel-title>
          <div class="text-h5 font-weight-black text-blue-darken-1">{{ $t('Administration.showsOverview') }}</div>
        </v-expansion-panel-title>
        <v-expansion-panel-text>
          <div class="mt-5" style="display: flex">
            <vorstellungOverview></vorstellungOverview>
            <addVorstellung></addVorstellung>
            <editVorstellung></editVorstellung>
          </div>
        </v-expansion-panel-text>
      </v-expansion-panel>
    </v-expansion-panels>

  </div>
</template>

<script>
import { defineComponent } from 'vue'
import addSaal from '@/components/administration/addSaal.vue'
import addVorstellung from '@/components/administration/addVorstellung.vue'
import editSaal from '@/components/administration/editSaal.vue'
import editVorstellung from '@/components/administration/editVorstellung.vue'
import saalOverview from '@/components/administration/saalOverview.vue'
import vorstellungOverview from '@/components/administration/vorstellungOverview.vue'
import movieOverview from '@/components/administration/movieOverview.vue'
import addCinema from '@/components/administration/addCinema.vue'

export default defineComponent({
  components: {
    addSaal,
    addVorstellung,
    editSaal,
    editVorstellung,
    saalOverview,
    vorstellungOverview,
    movieOverview,
    addCinema
  }
})
</script>

<style scoped>

</style>