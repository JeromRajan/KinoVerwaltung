<template>
  <v-card
    class=" pa-5 pb-8"
    elevation="8"
    max-width="600"
    rounded="lg"
    width="100%"
  >
    <h1>{{ $t('EmployeeArea.buyTicketWithMembercard') }}</h1>

    <v-text-field
      v-model="identifikationsNummer"
      :placeholder="$t('EmployeeArea.identifycationNumber')"
      class="mt-5"
      density="compact"
      prepend-inner-icon="mdi-card-account-details"
      variant="outlined">
    </v-text-field>

    <v-text-field
      v-model="ticketReferenz"
      :placeholder="$t('EmployeeArea.ticketReferenz')"
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
      {{ $t('EmployeeArea.successfullyBuyedTicket') }}
    </v-alert>
  </v-card>
</template>


<script>
import TicketService from '@/services/ticketService.js'

export default {
  name: 'buyTicketWithMembercard',
  data() {
    return {
      ticketReferenz: '',
      identifikationsNummer: '',
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
      this.ticketService.buyTicketWithMembercard(this.ticketReferenz, this.identifikationsNummer)
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