<template>
  <div>
    <v-select
      v-model="selectedCinema"
      :items="cinemas"
      item-title="name"
      item-value="kinoId"
      label="Kino"
      @update:modelValue="getSaals"
    >
    </v-select>

    <div v-if="selectedCinema">
      <add-saal @saal-added="getSaals" :kino-id="selectedCinema" ></add-saal>
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
      v-if="saals.length === 0"
      dismissible
      type="info"
      closable
      variant="tonal">
      {{ $t('Administration.noSaal') }}
    </v-alert>

    <v-data-table
      v-else-if="saals.length > 0"
      :headers="headers"
      :items="saals"
      :items-per-page="5"
      class="elevation-1">

      <template v-slot:item.actions="{ item }">
        <edit-saal :saal="item"  @saal-saved="getSaals" ></edit-saal>

        <v-btn
          class="ml-4"
          color="error"
          @click="deleteSaal(item.saalId)"
        >
          <v-icon icon="mdi-delete"> </v-icon>
        </v-btn>
      </template>
    </v-data-table>

  </div>
</template>

<script>
import AdminService from '@/services/adminService.js'
import AddSaal from '@/components/administration/addSaal.vue'
import EditSaal from '@/components/administration/editSaal.vue'

export default {
  name: 'saalOverview',
  components: {
    AddSaal,
    EditSaal
  },
  data() {
    return {
      saals: [],
      adminService: AdminService.getInstance(),
      errorMessage: '',
      isLoading: false,
      headers: [
        { title: this.$t('Administration.saalName'), value: 'name' },
        { title: this.$t('Administration.rowNumber'), value: 'anzahlReihen' },
        { title: this.$t('Administration.seatPerRowNumber'), value: 'anzahlSitzPlaetzeProReihe' },
        { title: this.$t('Administration.seatNumber'), value: 'anzahlSitzplaetze' },
        { title: this.$t('Administration.actions'), value: 'actions', sortable: false }
      ],
      cinemas: [],
      selectedCinema: null
    }
  },
  created() {
    this.getCinema();
  },
  methods: {
    getCinema() {
      this.isLoading = true
      this.errorMessage = ''
      this.adminService.getCinemas()
        .then(response => {
          this.cinemas = response.$values
          this.isLoading = false
        })
        .catch(error => {
          this.errorMessage = error.response.data.message
          this.isLoading = false
        })
    },

    getSaals() {
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

    deleteSaal(saalId) {
      this.isLoading = true
      this.errorMessage = ''
      this.adminService.deleteSaal(saalId)
        .then(() => {
          this.getSaals()
          this.isLoading = false
        })
        .catch(error => {
          console.log("deleteSaal", error.response)
          this.errorMessage = error.response.data
          this.isLoading = false
        })

    }
  }
}
</script>


<style scoped>

</style>