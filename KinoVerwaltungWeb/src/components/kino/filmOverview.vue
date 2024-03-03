<template>
  <h2 class="text-h4 font-weight-black text-blue-darken-2 mt-5">{{ $t('Movies.title') }}</h2>

  <v-sheet
    class="px-4 mt-4 "
    elevation="4"
    height="450"
    rounded
    width="100%"
  >
    <div>
      <v-chip-group v-model="programmSelection" filter selected-class="text-primary">
        <v-chip  @click="loadMovie(0)">{{ $t('Movies.allMovies') }}</v-chip>
        <v-chip  @click="loadMovie(1)">{{ $t('Movies.dailyMovies') }}</v-chip>
        <v-chip @click="loadMovie(2)">{{ $t('Movies.weeklyMovies') }}</v-chip>
      </v-chip-group>
    </div>


    <div v-if="movies.length < 1">
      <v-alert
        class="mt-4"
        dense
        outlined
        variant="tonal"
        type="info">
        {{ $t('Movies.noMovies') }}
      </v-alert>
    </div>
    <v-card
      v-else
      flat
      class="mt-4"
    >
      <template v-slot:text>
        <v-text-field
          v-model="search"
          hide-details
          :label="$t('Movies.search')"
          prepend-inner-icon="mdi-magnify"
          single-line
          variant="outlined"
        ></v-text-field>
      </template>

      <v-data-table
      :headers="headers"
      :items="movies"
      :search="search"
      >
        <template v-slot:item.detail="{ item }">
          <v-btn
            color="primary"
            @click="setMovie(item)"
          >
            {{ $t('Movies.seeDetails') }}
          </v-btn>
        </template>

      </v-data-table>
    </v-card>
  </v-sheet>
</template>

<script>
import { defineComponent } from 'vue'
import { useKinoStore } from '@/stores/kinoStore.js'

export default defineComponent({
  name: 'filmOverview',
  data: () => {
    return {
      useKinoStore: useKinoStore(),
      movies: [],
      search: '',
      headers: [],
      programmSelection: 0,
    }
  },
  created() {
    this.headers = [
      { title: this.$t('Movies.showTitle'), key: 'title' },
      { title: this.$t('Movies.description'), key: 'description' },
      { title: this.$t('Movies.date'), key: 'date' },
      { title: this.$t('Movies.time'), key: 'time' },
      { title: this.$t('Movies.duration'), key: 'duration' },
      { title: this.$t('Movies.price'), key: 'price' },
      { title: this.$t('Movies.ageRestriction'), key: 'ageRestriction' },
      { title: this.$t('Movies.genre'), key: 'genre' },
      { title: this.$t('Movies.language'), key: 'language' },
      { title: this.$t('Movies.detailView'), key: 'detail'}
    ]
  },
  watch: {
    'useKinoStore.currentHall': function() {
      this.useKinoStore.movies = []
      this.resetMovies()

      this.loadMovie(-1)
    },
    'useKinoStore.currentKino': function() {
      this.resetMovies()
    },
    'useKinoStore.movies': function() {
      if(this.useKinoStore.movies.length > 0) {
        this.fillMovies()
      }
    }
  },
  methods: {
    setMovie(movie) {
      this.useKinoStore.setCurrentMovie(movie)
      this.$router.push({ name: 'MovieDetail', params: { id: movie.detail }});
    },

    fillMovies() {
      this.useKinoStore.movies.forEach(movie => {
        this.movies.push({
          title: movie.filmTitel,
          description: movie.filmBeschreibung,
          date: this.getDateslot(movie.datum) ,
          time: this.getTimeslot(movie.startZeit),
          duration: this.calculateDuration(movie.filmDauer) ,
          price: movie.preis,
          ageRestriction: movie.filmFSK,
          genre: movie.filmGenre,
          language: movie.filmSprache,
          detail: movie.vorführungId
        })
      })
    },
    loadMovie(value){
      if(value !== -1){
        this.programmSelection = value;
      }
      this.resetMovies()
      switch (this.programmSelection) {
        case 0:
          this.useKinoStore.fetchMovies()
          break;
        case 1:
          this.useKinoStore.fetchDailyProgram()
          break;
        case 2:
          this.useKinoStore.fetchWeeklyProgram()
          break;
      }
    },
    resetMovies() {
      this.movies = []
    },
    calculateDuration(duration) {
      const hours = Math.floor(duration / 60);
      const minutes = duration % 60;
      return hours + " " + this.$t('Movies.hours')+ "  " + minutes + " " + this.$t('Movies.minutes')
    },
    getTimeslot(time) {
      return time.split("T")[1].split(":").slice(0, 2).join(":")
    },
    getDateslot(date) {
      return date.split("T")[0]
    }
  }

})
</script>
<style scoped>

</style>