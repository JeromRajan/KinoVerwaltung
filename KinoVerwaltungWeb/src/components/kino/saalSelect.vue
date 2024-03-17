

<template>
  <div v-if="!currentHall" >
    <v-alert
      class="mt-4 ml-10"
      dense
      outlined
      variant="tonal"
      type="info">
      {{$t('Movies.noHallInfo')}}
    </v-alert>
  </div>
  <v-sheet
    v-else
    class="d-flex px-4 mt-4 ml-10"
    elevation="4"
    height="150"
    width="20%"
    rounded
  >
    <div >
      <h1 class="text-h5 font-weight-black text-blue-darken-2 mt-4">{{$t('Movies.hall')}} - {{currentHall.saalName}}</h1>
      <v-select
        v-model="currentHall"
        :items="hallItems"
        :label="$t('Movies.hall')"
        class="mt-4"
        item-value="saalName"
        item-title="saalName"
        return-object
      >
      </v-select>
    </div>
  </v-sheet>
</template>

<script>
import { defineComponent } from 'vue'
import {useKinoStore} from '@/stores/kinoStore.js'

export default defineComponent({
  name: 'saalSelect',

  data: () => {
    return {
      useKinoStore: useKinoStore(),
      currentHall: "",
      hallItems: [],
    }
  },
  watch: {
    "useKinoStore.currentKino": function () {
      //Zurücksetzen des aktuellen Saals
      this.currentHall = "";
      this.hallItems = [];
    },
    "useKinoStore.currentHall": function (val) {
      //Der aktuelle Saal im Store gesetzt
      this.currentHall = val;
    },
    "useKinoStore.halls": function (val) {
      //Die Saal-Items im Store gesetzt
      this.hallItems = val;
    },
    currentHall: function() {
      //Der aktuelle Saal im Store gesetzt
      this.useKinoStore.currentHall = this.currentHall;
    },
  },
})
</script>

<style scoped>

</style>