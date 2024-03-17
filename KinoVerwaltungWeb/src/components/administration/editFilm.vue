<template>
  <v-dialog max-width="900" v-model="dialog">
    <template v-slot:activator="{ props: activatorProps }">
      <v-btn
        color="primary"
        v-bind="activatorProps"
      >
        <v-icon icon="mdi-pencil"></v-icon>
      </v-btn>
    </template>

    <template v-slot:default="{ isActive }">
      <v-card :title="$t('Administration.editMovie')">
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
              v-model="editMovie.titel"
              :label="$t('Administration.movie')"
              class="mt-5"
              density="compact"
              variant="outlined"
            >
            </v-text-field>

            <v-textarea
              v-model="editMovie.beschreibung"
              :label="$t('Administration.description')"

              density="compact"
              variant="outlined"
            >
            </v-textarea>

            <v-text-field
              v-model="editMovie.dauer"
              :label="$t('Administration.duration')"
              density="compact"
              type="number"
              variant="outlined"
            >
            </v-text-field>


            <v-text-field
              v-model="editMovie.altersfreigabe"
              :label="$t('Administration.ageRating')"
              density="compact"
              variant="outlined"
            >
            </v-text-field>

            <v-select
              v-model="editMovie.genreId"
              :items="genres"
              :label="$t('Administration.genre')"
              density="compact"
              item-title="name"
              item-value="genreId"
              variant="outlined"
            ></v-select>

            <v-select
              v-model="editMovie.spracheId"
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
            :text="$t('Administration.save')"
            class="mr-2"
            color="primary"
            variant="tonal"
            @click="saveMovie"
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
  name: 'editMovie',
  props: {
    movie: Object
  },
  data() {
    return {
      editMovie: this.movie,
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
      // Dialog schliessen
      this.dialog = false
      this.$emit('movie-saved')
    },
    saveMovie() {
      // Film speichern
      this.isLoading = true
      this.error = ''
      try {
        this.adminService.saveMovie(this.movie).then(() => {
          this.dialog = false
          this.$emit('movie-saved')
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
      // Genres holen
      this.adminService.getGenres().then(response => {
        if (response && response.$values && response.$values.length > 0) {
          this.genres = response.$values
        }
      })
    },
    getLanguages() {
      // Sprachen holen
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