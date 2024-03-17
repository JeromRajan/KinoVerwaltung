<template>
  <v-card
    class=" pa-5 pb-8"
    elevation="8"
    max-width="600"
    rounded="lg"
    width="100%"
  >
    <h1>{{ $t('EmployeeArea.confirmTicket') }}</h1>

    <v-text-field
      v-model="ticketReferenz"
      :placeholder="$t('EmployeeArea.ticketReferenz')"
      class="mt-5"
      density="compact"
      prepend-inner-icon="mdi-ticket-confirmation"
      variant="outlined">
    </v-text-field>

    <v-btn
      class="mt-2"
      color="blue"
      size="large"
      variant="tonal"
      @click="confirmTicket">
      {{ $t('EmployeeArea.confirmTicket') }}
    </v-btn>

    <v-alert
      v-model="isError"
      border="left"
      class="mt-5"
      dismissible
      elevation="2"
      icon="mdi-alert-circle"
      transition="scale-transition"
      closable
      variant="tonal"
      type="error">
      {{ errorMsg }}
    </v-alert>

    <v-alert
      v-model="isSuccessful"
      border="left"
      class="mt-5"
      dismissible
      elevation="2"
      icon="mdi-check-circle"
      transition="scale-transition"
      variant="tonal"
      closable
      type="success">
      {{ $t('EmployeeArea.successfullyConfirmedTicket') }}
    </v-alert>
  </v-card>
</template>


<script>
import TicketService from '@/services/ticketService.js'
export default {
  name: 'confirmTicket',
  data() {
    return {
      ticketReferenz: '',
      isSuccessful: false,
      isError: false,
      errorMsg: '',
      ticketService: TicketService.getInstance()
    }
  },
  methods: {
    confirmTicket() {
      this.isSuccessful = false
      this.isError = false
      this.errorMsg = ''
      this.ticketService.confirmTicket(this.ticketReferenz)
        .then(() => {
            this.isSuccessful = true
        })
        .catch(error => {
          this.isError = true
          this.errorMsg = error.response.data
        })
    }
  }
}
</script>

<style scoped>

</style>