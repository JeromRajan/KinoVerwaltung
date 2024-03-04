<template>
  <v-dialog max-width="900" v-model="dialog">
    <template v-slot:activator="{ props: activatorProps }">
      <v-btn
        class="mb-4"
        color="primary"
        v-bind="activatorProps"
        variant="tonal"
      >
        {{ $t('Administration.addShow') }}
      </v-btn>
    </template>

    <template v-slot:default="{ isActive }">
      <v-card :title="$t('Administration.addShow')">
        <v-card-text>
          <v-alert
            v-if="error"
            v-model="error"
            closable
            class="mb-4"
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
             <v-select
                v-model="show.filmId"
                :items="movies"
                item-title="titel"
                item-value="filmId"
                :label="$t('Administration.movie')"
                variant="outlined"
                density="compact"
             >
              </v-select>

              <v-text-field
                v-model="show.datum"
                :label="$t('Administration.date')"
                type="date"
                density="compact"
                variant="outlined"
              ></v-text-field>

              <v-text-field
                v-model="show.startZeit"
                :label="$t('Administration.startTime')"
                type="time"
                density="compact"
                variant="outlined"
              ></v-text-field>

              <v-text-field
                v-model="show.preis"
                :label="$t('Administration.price')"
                type="number"
                density="compact"
                variant="outlined"
              ></v-text-field>
          </div>
        </v-card-text>

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn
            :text="$t('Administration.create')"
            class="mr-2"
            color="primary"
            variant="tonal"
            @click="createShow"
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
  name: 'addVorstellung',
  props: {
    kinoId: {
      type: Number,
      default: 0,
      required: true
    },
    saalId: {
      type: Number,
      default: 0,
      required: true
    }
  },
  data() {
    return {
      show: {
        datum: '',
        preis: 19,
        saalId: this.saalId,
        kinoId: this.kinoId,
        filmId: '',
        startZeit: '',
      },
      isLoading: false,
      adminService: AdminService.getInstance(),
      error: '',
      dialog: false,
      movies: [],
    }
  },
  created() {
    this.getMovies()
  },
  methods: {
    cancel() {
      this.dialog = false
      this.$emit('show-added')
    },
    createShow() {
      this.isLoading = true
      this.error = ''

      const dateTime = `${this.show.datum}T${this.show.startZeit}:00`;
      const dateObj = new Date(dateTime);

      dateObj.setHours(dateObj.getHours() + 1);

      const isoString = dateObj.toISOString();

      const showObject = {
        ...this.show,
        datum: isoString,
        startZeit: isoString
      }

      this.adminService.addShow(showObject).then(() => {
        this.dialog = false
        this.$emit('show-added')
        this.isLoading = false
      }).catch(error => {
        if(error.response.data) {
          this.error = error.response.data
        } else {
          this.error = error
        }
        this.isLoading = false
      })
    },
    getMovies() {
      this.isLoading = true
      this.error = ''
      this.adminService.getMovies().then(response => {
        if (response && response.error) {
          if(response.error.response.data.error) {
            this.error = response.error.response.data.error
          } else {
            this.error = response.error
          }
        } else {
          this.movies = response.$values
        }
        this.isLoading = false
      })
    }
  }

}
</script>

<style scoped>

</style>