<template>
  <v-dialog max-width="900" v-model="dialog">
    <template v-slot:activator="{ props: activatorProps }">
      <v-btn
        class="mb-4"
        color="primary"
        v-bind="activatorProps"
        variant="tonal"
      >
        {{ $t('Administration.addHallToCinema') }}
      </v-btn>
    </template>

    <template v-slot:default="{ isActive }">
      <v-card :title="$t('Administration.addHallToCinema')">
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
              v-model="saal.Saal.Name"
              :label="$t('Administration.saalName')"
              class="mt-5"
              density="compact"
              variant="outlined"
            >
            </v-text-field>

            <v-text-field
              v-model="saal.AnzahlReihen"
              :label="$t('Administration.seatNumber')"
              density="compact"
              type="number"
              variant="outlined"
            >
            </v-text-field>

            <v-text-field
              v-model="saal.AnzahlSitzeProReihe"
              :label="$t('Administration.rowNumber')"
              density="compact"
              type="number"
              variant="outlined"
            >
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
            @click="createSaal"
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
  name: 'addSaal',
  props: {
    kinoId: {
      type: Number,
      default: 0,
      required: true
    }
  },
  data() {
    return {
      saal: {
        Saal: {
          Name: '',
        },
        AnzahlReihen: '',
        AnzahlSitzeProReihe: '',
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
      this.$emit('saal-added')
    },
    createSaal() {
      this.isLoading = true
      this.error = ''
      this.adminService.createSaal(this.kinoId, this.saal).then(response => {
        if (response && response.error) {
          this.error = response.error
        } else {
          this.dialog = false
          this.$emit('saal-added')
        }
        this.isLoading = false
      })
    },
  }

}
</script>

<style scoped>

</style>