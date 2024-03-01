<template>
  <div>

    <div style="display: flex">
      <v-sheet
        class="d-flex px-4 mt-4"
        elevation="4"
        height="150"
        width="20%"
        rounded
      >
        <!-- Kinoauswahl bereich -->
        <div v-if="!currentKino" style="display: flex; justify-content: center; align-items: center; height: 100%; width: 100%;">
          <v-progress-circular
            class=""
            color="primary"
            indeterminate
          ></v-progress-circular>
        </div>
        <div v-else>
          <h1 class="text-h5 font-weight-black text-blue-darken-2 mt-4">{{$t('Movies.cinema')}} - {{currentKino.name}}</h1>
          <v-select
            v-model="currentKino"
            :items="kinoItems"
            :label="$t('Movies.cinema')"
            class="mt-4"
            item-value="name"
            item-title="name"
            return-object
          >
          </v-select>
        </div>
      </v-sheet>
      <!-- Kinosaal bereich -->
      <v-sheet
        class="d-flex px-4 mt-4 ml-10"
        elevation="4"
        height="150"
        width="20%"
        rounded
      >

        <div v-if="!currentHall" >
          <v-alert
            class="mt-4"
            dense
            outlined
            type="info">
            {{$t('Movies.noHallInfo')}}
          </v-alert>
        </div>
        <div v-else>
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
    </div>

    <h2 class="text-h4 font-weight-black text-blue-darken-2 mt-5">{{ $t('Movies.title') }}</h2>
    <v-sheet
      class="d-flex px-4 mt-4 "
      elevation="4"
      height="450"
      width="100%"
      rounded
    >
      <div>


      </div>
    </v-sheet>


  </div>
</template>

<script>
import { defineComponent } from 'vue'
import {useKinoStore} from '@/stores/kinoStore.js'

export default defineComponent({
  name: 'MovieView',
  data: () => {
    return {
      useKinoStore: useKinoStore(),
      currentKino: "",
      kinoItems: [],
      currentHall: "",
      hallItems: []
    }
  },
  watch: {
    currentKino: function (val) {
      this.currentHall = "";
      this.hallItems = [];
      this.useKinoStore.setCurrentKino(val);
    },
    "useKinoStore.currentHall": function (val) {
      this.currentHall = val;
    },
    "useKinoStore.halls": function (val) {
      this.hallItems = val;
    }
  },
  created() {
    if(this.useKinoStore.kinos.length === 0) {
      this.useKinoStore.fetchKino();
    }
    if(this.useKinoStore.currentKino){
      this.currentKino = this.useKinoStore.currentKino;
      this.kinoItems = this.useKinoStore.kinos;
    }
  },

})
</script>

<style scoped>

</style>