<template>
  <div>
    <h2 class="text-h4 font-weight-black text-blue-darken-2 mt-5">{{ $t('Statistics.title') }}</h2>

    <v-expansion-panels class="mt-5">
      <v-expansion-panel>
        <v-expansion-panel-title>
          <div class="text-h5 font-weight-black text-blue-darken-1">{{ $t('Statistics.cinemaStatistics') }}</div>
        </v-expansion-panel-title>
        <v-expansion-panel-text>
          <cinema-occupancy class="mt-5"></cinema-occupancy>
          <cinema-sales class="mt-5"></cinema-sales>
        </v-expansion-panel-text>
      </v-expansion-panel>
    </v-expansion-panels>

    <v-expansion-panels class="mt-5">
      <v-expansion-panel>
        <v-expansion-panel-title>
          <div class="text-h5 font-weight-black text-blue-darken-1">{{ $t('Statistics.hallStatistics') }}</div>
        </v-expansion-panel-title>
        <v-expansion-panel-text>
          <hall-occupancy class="mt-5"></hall-occupancy>
          <hall-sales class="mt-5"></hall-sales>
        </v-expansion-panel-text>
      </v-expansion-panel>
    </v-expansion-panels>

    <v-expansion-panels class="mt-5">
      <v-expansion-panel>
        <v-expansion-panel-title>
          <div class="text-h5 font-weight-black text-blue-darken-1">{{ $t('Statistics.movieStatistics') }}</div>
        </v-expansion-panel-title>
        <v-expansion-panel-text>
          <movie-sales class="mt-5"></movie-sales>
        </v-expansion-panel-text>
      </v-expansion-panel>
    </v-expansion-panels>

    <v-expansion-panels class="mt-5">
      <v-expansion-panel>
        <v-expansion-panel-title>
          <div class="text-h5 font-weight-black text-blue-darken-1">{{ $t('Statistics.userStatistics') }}</div>
        </v-expansion-panel-title>
        <v-expansion-panel-text>
          <user-sales class="mt-5"></user-sales>
        </v-expansion-panel-text>
      </v-expansion-panel>
    </v-expansion-panels>
  </div>
</template>

<script>
import hallOccupancy from '@/components/statistics/hallOccupancy.vue'
import cinemaOccupancy from '@/components/statistics/cinemaOccupancy.vue'
import cinemaSales from '@/components/statistics/cinemaSales.vue'
import hallSales from '@/components/statistics/hallSales.vue'
import movieSales from '@/components/statistics/movieSales.vue'
import userSales from '@/components/statistics/userSales.vue'
import addCinema from '@/components/administration/addCinema.vue'

export default {
  name: 'StatisticsView',
  components: {
    addCinema,
    hallOccupancy,
    cinemaOccupancy,
    cinemaSales,
    hallSales,
    movieSales,
    userSales
  }
}

</script>

<style scoped>

</style>