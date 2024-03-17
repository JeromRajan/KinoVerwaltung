<template>
  <v-dialog v-model="dialog" max-width="900">
    <template v-slot:activator="{ props: activatorProps }">
      <v-btn
        color="primary"
        v-bind="activatorProps"
      >
        <v-icon icon="mdi-pencil"></v-icon>
      </v-btn>
    </template>

    <template v-slot:default="{ isActive }">
      <v-card :title="$t('Administration.addShow')">
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
            <v-select
              v-model="showEdit.filmId"
              :items="movies"
              :label="$t('Administration.movie')"
              density="compact"
              item-title="titel"
              item-value="filmId"
              variant="outlined"
            >
            </v-select>

            <v-text-field
              v-model="showEdit.datum"
              :label="$t('Administration.date')"
              density="compact"
              type="date"
              variant="outlined"
            ></v-text-field>

            <v-text-field
              v-model="showEdit.startZeit"
              :label="$t('Administration.startTime')"
              density="compact"
              type="time"
              variant="outlined"
            ></v-text-field>

            <v-text-field
              v-model="showEdit.preis"
              :label="$t('Administration.price')"
              density="compact"
              type="number"
              variant="outlined"
            ></v-text-field>
          </div>
        </v-card-text>

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn
            :text="$t('Administration.save')"
            class="mr-2"
            color="primary"
            variant="tonal"
            @click="saveShow"
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
  name: 'editVorstellung',
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
    },
    vorstellung: {
      type: Object,
      default: () => {
        return {}
      }
    }
  },
  data() {
    return {
      isLoading: false,
      adminService: AdminService.getInstance(),
      error: '',
      dialog: false,
      movies: [],
      showEdit: this.vorstellung
    }
  },
  created() {
    this.getMovies()
    if (this.showEdit.datum) {
      this.showEdit.datum = this.showEdit.datum.split('T')[0]
    }
    if (this.showEdit.startZeit) {
      this.showEdit.startZeit = new Date(this.showEdit.startZeit).toLocaleTimeString()
    }
  },
  methods: {
    cancel() {
      // Dialog schliessen
      this.dialog = false
      this.$emit('show-saved')
    },
    saveShow() {
      // Vorstellung speichern
      this.isLoading = true
      this.error = ''

      const dateTime = `${this.showEdit.datum}T${this.showEdit.startZeit}`
      const dateObj = new Date(dateTime)

      dateObj.setHours(dateObj.getHours() + 1)

      const isoString = dateObj.toISOString()

      const showObject =
        {
          datum: isoString,
          preis: this.showEdit.preis,
          saalId: this.saalId,
          kinoId: this.kinoId,
          filmId: this.showEdit.filmId,
          startZeit: isoString,
          vorführungId: this.showEdit.vorführungId
        }

      this.adminService.updateShow(showObject).then(() => {
        this.dialog = false
        this.isLoading = true
        this.$emit('show-saved')
      })
    },
    getMovies() {
      // Alle Filme holen
      this.isLoading = true
      this.error = ''
      this.adminService.getMovies().then(response => {
        if (response && response.error) {
          this.error = response.error
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