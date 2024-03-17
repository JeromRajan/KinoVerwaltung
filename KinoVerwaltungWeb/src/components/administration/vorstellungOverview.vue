<template>
  <div>
    <v-select
      v-model="selectedCinema"
      :items="cinemas"
      item-title="name"
      item-value="kinoId"
      :label="$t('Administration.cinema')"
      @update:modelValue="getSaals"
    >
    </v-select>

    <v-alert
      v-if="selectedCinema &&  saals.length === 0"
      dismissible
      type="info"
      closable
      variant="tonal">
      {{ $t('Administration.noSaal') }}
    </v-alert>

    <v-select
      v-else
      v-model="selectedSaal"
      :items="saals"
      item-title="name"
      item-value="saalId"
      :label="$t('Administration.hall')"
      @update:modelValue="getShows"
    >
    </v-select>



    <div v-if="selectedCinema && selectedSaal">
      <add-vorstellung @show-added="getShows" :kino-id="selectedCinema"  :saal-id="selectedSaal" ></add-vorstellung>
    </div>

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

    <v-progress-linear
      v-if="isLoading"
      color="blue"
      indeterminate>
    </v-progress-linear>

    <v-alert
      class="mt-5"
      v-else-if="shows.length === 0"
      dismissible
      type="info"
      closable
      variant="tonal">
      {{ $t('Administration.noShows') }}
    </v-alert>

    <v-data-table
      v-else-if="shows.length > 0"
      :headers="headers"
      :items="shows"
      class="elevation-1"
    >
      <template v-slot:item.actions="{ item }">
        <edit-vorstellung :vorstellung="item"  :kino-id="selectedCinema"  :saal-id="selectedSaal" @show-saved="getShows" ></edit-vorstellung>

        <v-btn
          class="ml-4"
          color="error"
          @click="deleteShow(item.vorführungId)"
        >
          <v-icon icon="mdi-delete">
          </v-icon>
        </v-btn>
      </template>
    </v-data-table>

  </div>
</template>

<script>
import AdminService from '@/services/adminService.js'
import AddVorstellung from '@/components/administration/addVorstellung.vue'
import EditVorstellung from '@/components/administration/editVorstellung.vue'

export default {
  name: 'vorstellungOverview',
  components: {
    AddVorstellung,
    EditVorstellung
  },
  data() {
    return {
      adminService: AdminService.getInstance(),
      errorMessage: '',
      isLoading: false,
      headers: [
        { title: this.$t('Movies.showTitle'), key: 'filmTitel' },
        { title: this.$t('Movies.description'), key: 'filmBeschreibung' , width: '30%'},
        { title: this.$t('Movies.date'), key: 'datum' },
        { title: this.$t('Movies.time'), key: 'startZeit' },
        { title: this.$t('Movies.duration'), key: 'filmDauer' },
        { title: this.$t('Movies.price'), key: 'preis' },
        { title: this.$t('Movies.ageRestriction'), key: 'filmFSK' },
        { title: this.$t('Movies.genre'), key: 'filmGenre' },
        { title: this.$t('Movies.language'), key: 'filmSprache' },
        { title: this.$t('Administration.actions'), key: 'actions'}
      ],
      cinemas: [],
      selectedCinema: null,
      saals: [],
      selectedSaal: null,
      shows: []
    }
  },
  created() {
    this.getCinemas();
  },
  methods: {
    getCinemas() {
      // Alle Kinos holen
      this.isLoading = true
      this.errorMessage = ''

      this.adminService.getCinemas()
        .then(response => {
          this.cinemas = response
          this.isLoading = false
        })
        .catch(error => {
          this.errorMessage = error.response.data.message
          this.isLoading = false
        })
    },

    getSaals() {
      // Alle Säle holen
      this.saals = []
      this.selectedSaal = null
      this.shows = []
      this.isLoading = true
      this.errorMessage = ''

      this.adminService.getSaalsinfoByCinema(this.selectedCinema)
        .then(response => {
          this.saals = response.$values
          this.isLoading = false
        })
        .catch(error => {
          this.errorMessage = error.response.data.message
          this.isLoading = false
        })
    },

    deleteShow(showId) {
      // Vorstellung löschen
      this.isLoading = true
      this.errorMessage = ''

      this.adminService.deleteShow(showId)
        .then(() => {
          this.getShows()
          this.isLoading = false
        })
        .catch(error => {
          this.errorMessage = error.response.data
          this.isLoading = false
        })
    },

    getShows() {
      // Alle Vorstellungen holen
      this.isLoading = true
      this.errorMessage = ''

      this.adminService.getShowsByCinemaIdAndHallId (this.selectedCinema, this.selectedSaal)
        .then(response => {
          this.shows = response.$values
          this.isLoading = false
        })
        .catch(error => {
          this.errorMessage = error.response.data.message
          this.isLoading = false
        })
    },
  }
}
</script>


<style scoped>

</style>