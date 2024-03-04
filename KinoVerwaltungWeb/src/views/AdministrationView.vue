<template>
  <div>
    <h2 class="text-h4 font-weight-black text-blue-darken-2 mt-5">{{ $t('Administration.title') }}</h2>

    <v-expansion-panels class="mt-5">
      <v-expansion-panel>
        <v-expansion-panel-title>
          <div class="text-h5 font-weight-black text-blue-darken-1">{{ $t('Administration.cinemaOverview') }}</div>
        </v-expansion-panel-title>
        <v-expansion-panel-text>
          <cinemaOverview></cinemaOverview>
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
            <saalOverview></saalOverview>
        </v-expansion-panel-text>
      </v-expansion-panel>
    </v-expansion-panels>

    <v-expansion-panels class="mt-5">
      <v-expansion-panel>
        <v-expansion-panel-title>
          <div class="text-h5 font-weight-black text-blue-darken-1">{{ $t('Administration.showsOverview') }}</div>
        </v-expansion-panel-title>
        <v-expansion-panel-text>
            <vorstellungOverview></vorstellungOverview>
        </v-expansion-panel-text>
      </v-expansion-panel>
    </v-expansion-panels>

  </div>
</template>

<script>
import { defineComponent } from 'vue'
import saalOverview from '@/components/administration/saalOverview.vue'
import vorstellungOverview from '@/components/administration/vorstellungOverview.vue'
import movieOverview from '@/components/administration/movieOverview.vue'
import cinemaOverview from '@/components/administration/cinemaOverview.vue'

export default defineComponent({
  components: {
    saalOverview,
    vorstellungOverview,
    movieOverview,
    cinemaOverview
  }
})
</script>

<style scoped>

</style>