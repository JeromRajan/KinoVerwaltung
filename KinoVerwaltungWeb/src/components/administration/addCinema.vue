<template>
  <v-dialog max-width="900" v-model="dialog">
    <template v-slot:activator="{ props: activatorProps }">
      <v-btn
        class="mb-4"
        color="primary"
        v-bind="activatorProps"
        variant="tonal"
      >
        {{ $t('Administration.addCinema') }}
      </v-btn>
    </template>

    <template v-slot:default="{ isActive }">
      <v-card :title="$t('Administration.addCinema')">
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
              v-model="cinema.name"
              :label="$t('Administration.cinemaName')"
              class="mt-5"
              density="compact"
              variant="outlined"
            >
            </v-text-field>

            <v-text-field
              v-model="cinema.Adresse.Strasse"
              :label="$t('Administration.street')"
              density="compact"
              variant="outlined">
            </v-text-field>

            <v-text-field
              v-model="cinema.Adresse.Hausnummer"
              :label="$t('Administration.houseNumber')"
              density="compact"
              variant="outlined">
            </v-text-field>

            <v-text-field
              v-model="cinema.Adresse.PLZ"
              :label="$t('Administration.zipCode')"
              density="compact"
              variant="outlined">
            </v-text-field>

            <v-text-field
              v-model="cinema.Adresse.Stadt"
              :label="$t('Administration.city')"
              density="compact"
              variant="outlined">
            </v-text-field>

            <v-text-field
              v-model="cinema.Adresse.Land"
              :label="$t('Administration.country')"
              density="compact"
              variant="outlined">
            </v-text-field>

          </div>
        </v-card-text>

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn
            :text="$t('Administration.create')"
            class="mr-2"
            color="primary"
            variant="tonal"
            @click="createCinema"
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
      cinema: {
        Name: '',
        Adresse: {
          "Strasse": "",
          "Hausnummer": "",
          "PLZ": "",
          "Stadt": "",
          "Land": "Schweiz"
        }
      },
      isLoading: false,
      adminService: AdminService.getInstance(),
      error: '',
      dialog: false
    }
  },
  methods: {
    cancel() {
      this.dialog = false
      this.$emit('cinemaAdded')
    },
    createCinema() {
      this.isLoading = true
      this.error = ''
      this.adminService.addCinema(this.cinema).then(() => {
        this.isLoading = false
        this.dialog = false
        this.$emit('cinemaAdded')
      }).catch(error => {
        this.isLoading = false
        if(error.response && error.response.data) {
          this.error = error.response.data
        } else {
          this.error = this.$t('Administration.error')
        }
      })

    }
  }
}
</script>

<style scoped>

</style>