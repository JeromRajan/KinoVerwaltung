<template>
  <v-card
    class=" pa-5 pb-8 ml-15"
    elevation="8"
    width="100%"
    max-width="700"
    rounded="lg"
  >
    <h1>{{ $t('EmployeeArea.membercardBalance') }}</h1>

    <v-text-field
      v-model="identifikationsNummer"
      :placeholder="$t('EmployeeArea.identifycationNumber')"
      class="mt-5"
      density="compact"
      prepend-inner-icon="mdi-card-account-details"
      variant="outlined">
    </v-text-field>

    <v-text-field
      type="number"
      v-model="balance"
      :placeholder="$t('EmployeeArea.enterBalance')"
      density="compact"
      prepend-inner-icon="mdi-cash"
      variant="outlined">
    </v-text-field>

    <v-btn
      class="mt-2"
      color="blue"
      size="large"
      variant="tonal"
      @click="addBalance">
      + {{ $t('EmployeeArea.addBalance') }}
    </v-btn>

<v-alert
      v-if="isSuccessful"
      class="mt-5"
      type="success"
      transition="scale-transition"
      dismissible
      border="left"
      elevation="2"
      icon="mdi-check-circle">
      {{ $t('EmployeeArea.successfullyAddedBalance') }}
</v-alert>

  </v-card>
</template>

<script>
import AdminService from '@/services/adminService.js'

export default {
  name: 'loadMembercard',
  data() {
    return {
      identifikationsNummer: '',
      balance: 0,
      isSuccessful: false
    }
  },
  methods: {
    async addBalance() {
      this.isSuccessful = false
      const adminService = AdminService.getInstance()
      const membercard = await adminService.addBalance(this.identifikationsNummer, this.balance)
      if (membercard) {
        this.isSuccessful = true
      }
    }
  }
}
</script>

<style scoped>

</style>