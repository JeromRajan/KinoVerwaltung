<template>
  <div>
    <v-snackbar
      v-model="isTicketBought"
      color="success"
      location="top"
      multi-line
      timeout="5000"
    >
      <v-icon left>mdi-check</v-icon>
      {{ $t('MemberArea.ticketBuySuccessInfo') }}
      <template v-slot:actions>
        <v-btn
          variant="text"
          @click="isTicketBought = false"
        >
          <v-icon left>mdi-close</v-icon>
        </v-btn>
      </template>
    </v-snackbar>

    <v-card
      class="mt-15"
      elevation="8"
      max-width="1800"
      rounded="lg"
      width="100%"
    >
      <h1 class="pa-5 pb-8">{{ $t('MemberArea.tickets') }} </h1>

      <v-alert
        v-if="errorMessage"
        v-model="errorMessage"
        class="mb-5"
        closable
        dismissible
        type="error"
        variant="tonal">
        {{ errorMessage }}
      </v-alert>

      <v-progress-linear
        v-if="isLoading"
        color="blue"
        indeterminate>
      </v-progress-linear>

      <v-data-table
        v-else-if="tickets.length > 0"
        :headers="headers"
        :items="tickets"
        :items-per-page="5"
        class="elevation-1">

        <template v-slot:item.actions="{ item }">
          <v-btn
            v-if="item.ticketStatus === 'Reserviert' && item.zahlungsmethode === 'Mitgliederkarte'"
            class="ml-4"
            color="primary"
            variant="tonal"
            @click="buyTicket(item.referenzNummer)"
          >
            <v-icon>mdi-ticket</v-icon>
            {{ $t('MemberArea.buyTicket') }}
          </v-btn>
        </template>

        <template v-slot:item.ticketStatus="{ item }">
          <v-chip
            dark
            :color="getTicketStatusColor(item.ticketStatus)"
          >
            {{ item.ticketStatus }}
          </v-chip>
        </template>
      </v-data-table>

      <v-alert
        v-else
        class="mb-5"
        dismissible
        type="info"
        variant="tonal">
        {{ $t('MemberArea.noTickets') }}
      </v-alert>
    </v-card>
  </div>
</template>

<script>
import { useUserStore } from '@/stores/userStore.js'
import TicketService from '@/services/ticketService.js'

export default {
  name: 'ticketsMember',
  data() {
    return {
      tickets: [ ],
      errorMessage: '',
      isLoading: false,
      headers: [
        { title: this.$t('MemberArea.ticketNumber'), value: 'referenzNummer' },
        { title: this.$t('MemberArea.kinoname'), value: 'kinoname' },
        { title: this.$t('MemberArea.paymentMethod'), value: 'zahlungsmethode' },
        { title: this.$t('MemberArea.hallName'), value: 'saalName' },
        { title: this.$t('MemberArea.showTime'), value: 'vorführungszeit' },
        { title: this.$t('MemberArea.showDate'), value: 'vorführungsdatum' },
        { title: this.$t('MemberArea.filmTitel'), value: 'filmTitel' },
        { title: this.$t('MemberArea.seatRow'), value: 'sitzReihe' },
        { title: this.$t('MemberArea.seatNumber'), value: 'sitzNummer' },
        { title: this.$t('MemberArea.price'), value: 'preis' },
        { title: this.$t('MemberArea.ticketStatus'), value: 'ticketStatus', sortable: false },
        { title: this.$t('MemberArea.actions'), value: 'actions', sortable: false }
      ],
      userStore: useUserStore(),
      ticketService: TicketService.getInstance(),
      isTicketBought: false,

    }
  },
  created() {
      this.getTickets()
  },
  methods: {
    getTickets() {
      // Alle Tickets des Benutzers holen
      this.isLoading = true
      this.errorMessage = ''
      if(this.userStore.user){
        this.ticketService.getTicketByUserId(this.userStore.user.benutzerId)
          .then(tickets => {
            this.tickets = tickets
          })
          .catch(error => {
            this.errorMessage = error
          }).finally(() => {
          this.isLoading = false
        })
      }else {
        this.isLoading = false
      }
    },
    buyTicket(ticketId) {
      // Ticket mit Mitgliedskarte kaufen
      this.isLoading = true
      this.errorMessage = ''
      this.ticketService.buyTicketWithMembercard(ticketId, this.userStore.user.mitgliederkarte.identifikationsNummer)
        .then(() => {
          this.isTicketBought = true
          this.userStore.getMembercardBalance(this.userStore.user.benutzerId)
          this.getTickets()
        })
        .catch(error => {
          if(error.response){
            this.errorMessage = error.response.data
          }else {
            this.errorMessage = error
          }
        })
        .finally(() => {
          this.isLoading = false
        })
    },
    getTicketStatusColor(ticketStatus) {
      // Farbe des Ticketstatus
      if (ticketStatus === 'Reserviert') {
        return 'warning'
      } else if (ticketStatus === 'Abgeschlossen') {
        return 'success'
      } else if (ticketStatus === 'Bezahlt') {
        return 'primary'
      }
    }
  }
}
</script>


<style scoped>

</style>