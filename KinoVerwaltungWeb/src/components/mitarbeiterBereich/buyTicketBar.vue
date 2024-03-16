<template>
  <v-card
    class=" pa-5 pb-8 ml-15"
    elevation="8"
    max-width="600"
    rounded="lg"
    width="100%"
  >
    <h1>{{ $t('EmployeeArea.buyTicketWithCash') }}</h1>

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
      @click="buyTicket">
      {{ $t('EmployeeArea.buyTicket') }}
    </v-btn>

    <v-alert
      v-model="isError"
      border="left"
      class="mt-5"
      closable
      dismissible
      elevation="2"
      icon="mdi-alert-circle"
      transition="scale-transition"
      type="error"
      variant="tonal">
      {{ errorMsg }}
    </v-alert>

    <v-alert
      v-model="isSuccessful"
      border="left"
      class="mt-5"
      closable
      dismissible
      elevation="2"
      icon="mdi-check-circle"
      transition="scale-transition"
      type="success"
      variant="tonal">
      {{ $t('EmployeeArea.successfullyBuyedTicket') }}
    </v-alert>
  </v-card>
</template>


<script>
import TicketService from '@/services/ticketService.js'

export default {
  name: 'buyTicketWithCash',
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
    buyTicket() {
      this.isSuccessful = false
      this.isError = false
      this.errorMsg = ''
      this.ticketService.buyTicketWithCash(this.ticketReferenz)
        .then(() => {
          this.isSuccessful = true
        })
        .catch((error) => {
          if (error.response) {
            this.errorMsg = error.response.data
          } else {
            this.errorMsg = error
          }
          this.isError = true
        })
    }
  }
}
</script>

<style scoped>

</style>