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
      <v-card :title="$t('Administration.editHall')">
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
              v-model="editSaal.name"
              :label="$t('Administration.saalName')"
              class="mt-5"
              density="compact"
              variant="outlined"
            >
            </v-text-field>

            <v-text-field
              v-model="editSaal.anzahlReihen"
              :label="$t('Administration.rowNumber')"
              density="compact"
              type="number"
              variant="outlined"
            >
            </v-text-field>

            <v-text-field
              v-model="editSaal.anzahlSitzPlaetzeProReihe"
              :label="$t('Administration.seatNumber')"
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
            :text="$t('Administration.save')"
            class="mr-2"
            color="primary"
            variant="tonal"
            @click="saveSaal"
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
  name: 'editSaal',
  props: {
    kinoId: {
      type: Number,
      default: 0,
      required: true
    },
    saal: {
      type: Object,
      default: () => {
        return {
          name: "",
          anzahlSitzPlaetzeProReihe: 0,
          anzahlReihen: 0
        }
      }
    }
  },
  data() {
    return {
      editSaal: this.saal,
      isLoading: false,
      adminService: AdminService.getInstance(),
      error: '',
      dialog: false
    }
  },
  methods: {
    cancel() {
      // Dialog schliessen
      this.dialog = false
      this.$emit('saal-saved')
    },
    saveSaal() {
      // Saal speichern
      this.isLoading = true
      this.error = ''
      const saalObject = {
        Saal: {
          Name: this.editSaal.name,
          SaalId: this.editSaal.saalId
        },
        AnzahlReihen: this.editSaal.anzahlReihen,
        AnzahlSitzeProReihe: this.editSaal.anzahlSitzPlaetzeProReihe,
      }

      this.adminService.updateSaal(saalObject).then(response => {
        if (response && response.error) {
          this.error = response.error
        } else {
          this.dialog = false
          this.$emit('saal-saved')
        }
      }).finally(() => {
        this.isLoading = false
      })
    },
  }

}
</script>

<style scoped>

</style>