<template>
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
</template>

<script>
import { defineComponent } from 'vue'
import {useKinoStore} from '@/stores/kinoStore.js'

export default defineComponent({
  name: 'kinoSelect',
  data: () => {
    return {
      useKinoStore: useKinoStore(),
      currentKino: "",
      kinoItems: [],
    }
  },
  watch: {
    currentKino: function(val) {
      //Der aktuelle Kino im Store gesetzt
      this.useKinoStore.setCurrentKino(val);
    },
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