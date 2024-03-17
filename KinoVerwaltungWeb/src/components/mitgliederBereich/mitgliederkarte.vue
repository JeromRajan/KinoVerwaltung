<template>
  <v-card v-if="mitgliederkarte &&  'mitgliederstatus' in this.mitgliederkarte && 'statusName' in this.mitgliederkarte.mitgliederstatus"
          elevation="8"
          max-width="700"
          rounded="lg"
          width="100%"
  >

    <v-card-text class="pa-5 pb-8 ">
      <h1>{{ $t('MemberArea.membercard') }} </h1>
      <div class="mt-10">
        <v-row>
          <v-col class="font-weight-bold" cols="6">{{ $t('MemberArea.membercardNumber') }}:</v-col>
          <v-col cols="6">{{ mitgliederkarte.identifikationsNummer }}</v-col>
        </v-row>
        <v-row>
          <v-col class="font-weight-bold" cols="6">{{ $t('MemberArea.status') }}:</v-col>
          <v-col cols="6">
            <v-chip :class="statusClass" dark>{{ mitgliederkarte.mitgliederstatus.statusName }}</v-chip>
          </v-col>
        </v-row>
        <v-row>
          <v-col class="font-weight-bold" cols="6">{{ $t('MemberArea.availableAmount') }}:</v-col>
          <v-col cols="6">{{ userStore.membercardBalance }}.- CHF</v-col>
        </v-row>
        <v-row>
          <v-col class="font-weight-bold" cols="6">{{ $t('MemberArea.expiryDate') }}:</v-col>
          <v-col cols="6">{{ new Date(mitgliederkarte.ablaufdatum).toLocaleDateString() }}</v-col>
        </v-row>
        <v-row>
          <v-col class="font-weight-bold" cols="6">{{ $t('MemberArea.numberOfPurchasedTickets') }}:</v-col>
          <v-col cols="6">{{ userStore.user.mitgliederkarte.anzahlGekaufterTickets }}</v-col>
        </v-row>

      </div>

    </v-card-text>
    <v-card-actions :class="statusClass">

    </v-card-actions>
  </v-card>
</template>

<script>
import { useUserStore } from '@/stores/userStore.js'

export default {
  name: 'membercardMember',
  data() {
    return {
      userStore: useUserStore(),
      mitgliederkarte:{}
    }
  },
  created() {
    if (this.userStore && this.userStore.user && this.userStore.user.mitgliederkarte) {
      this.mitgliederkarte = this.userStore.user.mitgliederkarte
    }
  },
  computed: {
    statusClass() {
      // Status der Mitgliedskarte
      if (this.mitgliederkarte && 'mitgliederstatus' in this.mitgliederkarte && 'statusName' in this.mitgliederkarte.mitgliederstatus) {
        switch (this.mitgliederkarte.mitgliederstatus.statusName.toLowerCase()) {
          case 'bronze':
            return 'bronze-background'
          case 'silber':
            return 'silver-background'
          case 'gold':
            return 'gold-background'
          default:
            return ''
        }
      }else {
        return ''
      }

    }
  }

}
</script>


<style scoped>
.bronze-background {
  background-color: #cd7f32 !important;
  color: white !important;
}

.silver-background {
  background-color: #c0c0c0 !important;
  color: black !important;
}

.gold-background {
  background-color: #ffd700 !important;
  color: white !important;
}
</style>