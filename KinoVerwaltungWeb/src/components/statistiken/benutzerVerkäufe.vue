<template>
  <div>
    <v-card
      :max-width="500"
      class=" pa-5 pb-8 "
      elevation="8"
      rounded="lg"
      width="100%"
    >
      <h1>{{ $t('Statistics.userSales') }}</h1>

      <v-select
        v-model="selectedUser"
        :items="users"
        :item-props="itemProps"
        item-value="benutzerId"
        :label="$t('Statistics.user')"
        @update:modelValue="loadUserSales"
      >
      </v-select>

      <v-progress-linear
        v-if="isLoading"
        color="blue"
        indeterminate>
      </v-progress-linear>

      <div class="mt-5" v-else-if="userStats">
        <v-row>
          <v-col cols="5" class="font-weight-bold">{{$t('Statistics.salesVolume')}}:</v-col>
          <v-col cols="7">{{userStats.umsatz}}.- CHF</v-col>
        </v-row>
      </div>
    </v-card>
  </div>
</template>

<script>
import StatistikService from '@/services/statistikService.js'
import UserService from '@/services/userService.js'

export default {

  data() {
    return {
      statisikService: StatistikService.getInstance(),
      userSerivce: UserService.getInstance(),
      userStats: null,
      users: [],
      selectedUser: null,
      isLoading: false
    }
  },
  created() {
    // this.loadCinemaOccupancy()
    this.getUsers()
  },
  methods: {
    itemProps (item) {
      return {
        title: item.vorname,
        subtitle: item.email,
      }
    },

    getUsers() {
      // Benutzerdaten laden
      this.userSerivce.getUsers().then((data) => {
        this.users = data
      })
    },

    // Daten für das Diagramm laden
    async loadUserSales() {
      this.isLoading = true
      const daten = await this.statisikService.getUserSales(this.selectedUser)
      if (daten) {
        this.userStats = {}
        this.userStats = daten;

        this.$nextTick(() => {
          this.isLoading = false
        })
      }else{
        this.isLoading = false
      }
    },
  }
}
</script>
