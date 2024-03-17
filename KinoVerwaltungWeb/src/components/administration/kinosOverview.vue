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

    <add-cinema @cinema-added="getCinemas" ></add-cinema>

    <v-progress-linear
      v-if="isLoading"
      color="blue"
      indeterminate>
    </v-progress-linear>

    <v-data-table
      v-else-if="cinemas.length > 0"
      :headers="headers"
      :items="cinemas"
      :items-per-page="5"
      class="elevation-1">
    </v-data-table>

  </div>
</template>

<script>
import AdminService from '@/services/adminService.js'
import AddCinema from '@/components/administration/addKino.vue'
export default {
  name: 'cinemaOverview',
  components: {
    AddCinema
  },
  data() {
    return {
      cinemas: [],
      adminService: AdminService.getInstance(),
      errorMessage: '',
      isLoading: false,
      headers: [
        { title: this.$t('Administration.cinemaName'), value: 'name' },
        { title: this.$t('Administration.zipCode'), value: 'adresse.plz' },
        { title: this.$t('Administration.city'), value: 'adresse.stadt' },
        { title: this.$t('Administration.street'), value: 'adresse.strasse' },
        { title: this.$t('Administration.houseNumber'), value: 'adresse.hausnummer' },
        { title: this.$t('Administration.country'), value: 'adresse.land' }
      ]
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
      this.adminService.getCinemas().then(response => {
        this.cinemas = response
        this.isLoading = false
      }).catch(error => {
        if(error.response && error.response.data) {
          this.errorMessage = error.response.data
        } else {
          this.errorMessage = error
        }
        this.isLoading = false
      })
    },
  }
}
</script>


<style scoped>

</style>