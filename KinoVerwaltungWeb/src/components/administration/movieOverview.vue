<template>
  <div>
    <v-alert
      class="mb-5"
      v-if="errorMessage"
      v-model="errorMessage"
      dismissible
      type="error"
      closable
      variant="tonal">
      {{ errorMessage }}
    </v-alert>

    <add-movie @movie-added="getMovies" ></add-movie>

    <v-progress-linear
      v-if="isLoading"
      color="blue"
      indeterminate>
    </v-progress-linear>

    <v-data-table
      v-else-if="movies.length > 0"
      :headers="headers"
      :items="movies"
      :items-per-page="5"
      class="elevation-1">

      <template v-slot:item.actions="{ item }">
        <edit-movie :movie="item"  @movie-saved="getMovies" ></edit-movie>

        <v-btn
          class="ml-4"
          color="error"
          @click="deleteMovie(item.filmId)"
        >
          <v-icon icon="mdi-delete"> </v-icon>
        </v-btn>
      </template>
    </v-data-table>

  </div>
</template>

<script>
import AdminService from '@/services/adminService.js'
import AddMovie from '@/components/administration/addMovie.vue'
import EditMovie from '@/components/administration/editMovie.vue'

export default {
  name: 'movieOverview',
  components: {
    AddMovie,
    EditMovie
  },
  data() {
    return {
      movies: [],
      adminService: AdminService.getInstance(),
      errorMessage: '',
      isLoading: false,
      headers: [
        { title: this.$t('Administration.tableTitle'), value: 'titel' },
        { title: this.$t('Administration.description'), value: 'beschreibung', width: '30%' },
        { title: this.$t('Administration.duration'), value: 'dauer' },
        { title: this.$t('Administration.genre'), value: 'genreName' },
        { title: this.$t('Administration.language'), value: 'spracheName' },
        { title: this.$t('Administration.ageRating'), value: 'altersfreigabe' },
        { title: this.$t('Administration.actions'), value: 'actions', sortable: false }
      ]
    }
  },
  created() {
    this.getMovies();
  },
  methods: {
    getMovies() {
      this.isLoading = true
      this.errorMessage = ''
      this.adminService.getMovies().then(response => {
        if (response && response.$values && response.$values.length > 0) {
          this.movies = response.$values
        }
        this.isLoading = false
      }).catch(error => {
        this.errorMessage = error
        this.isLoading = false
      })
    },


    deleteMovie(movieId) {
      this.isLoading = true
      this.errorMessage = ''
      this.adminService.deleteMovie(movieId).then(response => {
        if (response) {
          this.movies = this.movies.filter(movie => movie.filmId !== movieId)
        }
        this.isLoading = false
      }).catch(error => {
        if(error.response && error.response.data) {
          this.errorMessage = error.response.data
        } else {
          this.errorMessage = error
        }
        this.isLoading = false
      })
    }
  }
}
</script>


<style scoped>

</style>