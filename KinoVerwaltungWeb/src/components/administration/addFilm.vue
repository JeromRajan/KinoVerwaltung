<template>
  <v-dialog max-width="900" v-model="dialog">
    <template v-slot:activator="{ props: activatorProps }">
      <v-btn
        class="mb-4"
        color="primary"
        v-bind="activatorProps"
        variant="tonal"
      >
        {{ $t('Administration.addMovie') }}
      </v-btn>
    </template>

    <template v-slot:default="{ isActive }">
      <v-card :title="$t('Administration.addMovie')">
        <v-card-text>
          <v-alert
            v-if="error"
            v-model="error"
            closable
            dismissible
            type="error"
            variant="tonal">
            {{ error }}
          </v-alert>

          <v-progress-linear
            v-if="isLoading"
            color="blue"
            indeterminate>
          </v-progress-linear>

          <div v-else>
            <v-text-field
              v-model="movie.titel"
              :label="$t('Administration.movie')"
              class="mt-5"
              density="compact"
              variant="outlined"
            >
            </v-text-field>

            <v-textarea
              v-model="movie.beschreibung"
              :label="$t('Administration.description')"

              density="compact"
              variant="outlined"
            >
            </v-textarea>

            <v-text-field
              v-model="movie.dauer"
              :label="$t('Administration.duration')"
              density="compact"
              type="number"
              variant="outlined"
            >
            </v-text-field>

            <v-text-field
              v-model="movie.altersfreigabe"
              :label="$t('Administration.ageRating')"
              density="compact"
              variant="outlined"
            >
            </v-text-field>

            <v-select
              v-model="movie.genreId"
              :items="genres"
              :label="$t('Administration.genre')"
              density="compact"
              item-title="name"
              item-value="genreId"
              variant="outlined"
            ></v-select>

            <v-select
              v-model="movie.spracheId"
              :items="languages"
              :label="$t('Administration.language')"
              density="compact"
              item-title="name"
              item-value="spracheId"
              variant="outlined"
            ></v-select>
          </div>
        </v-card-text>

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn
            :text="$t('Administration.create')"
            class="mr-2"
            color="primary"
            variant="tonal"
            @click="createMovie"
          ></v-btn>
          <v-btn
            :text="$t('Administration.cancel')"
            @click="cancel"
          ></v-btn>
        </v-card-actions>
      </v-card>
    </template>
  </v-dialog>
</template>

<script>
import AdminService from '@/services/adminService.js'

export default {
  name: 'addMovie',
  data() {
    return {
      movie: {
        titel: '',
        beschreibung: '',
        dauer: '',
        altersfreigabe: '',
        genreId: '',
        spracheId: ''
      },
      genres: [],
      languages: [],
      isLoading: false,
      adminService: AdminService.getInstance(),
      error: '',
      dialog: false
    }
  },
  created() {
    this.isLoading = true
    this.getGenres()
    this.getLanguages()

    this.$nextTick(() => {
      this.isLoading = false
    })
  },
  methods: {
    cancel() {
      this.dialog = false
      this.$emit('movieAdded')
    },
    createMovie() {
      this.isLoading = true
      this.error = ''
      try {
        this.adminService.addMovie(this.movie).then(() => {
          this.dialog = false
          this.$emit('movieAdded')
          this.isLoading = false
        }).catch(error => {
          if (error.response && error.response.data) {
            this.error = error.response.data.errors
          } else {
            this.error = error
          }
          this.isLoading = false
        })
      } catch (error) {
        if (error.response && error.response.data) {
          this.error = error.response.data
        } else {
          this.error = error
        }
        this.isLoading = false
      }
    },
    getGenres() {
      this.adminService.getGenres().then(response => {
        if (response && response.$values && response.$values.length > 0) {
          this.genres = response.$values
        }
      })
    },
    getLanguages() {
      this.adminService.getLanguages().then(response => {
        if (response && response.$values && response.$values.length > 0) {
          this.languages = response.$values
        }
      })
    }
  }
}
</script>

<style scoped>

</style>