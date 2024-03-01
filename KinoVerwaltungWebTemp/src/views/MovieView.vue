<template>
  <div>


    <v-sheet
      class="d-flex px-4 mt-4 bg-blue-accent-1 "
      elevation="4"
      height="150"
      width="20%"
      rounded
    >
      <div>
        <h1 class="text-h5 font-weight-black text-white mt-4">{{$t('Movies.cinema')}} - {{currentKino}}</h1>
        <v-select
          v-model="currentKino"
          :items="kinoItems"
          label="Kino"
          outlined
          dense
          class="mt-4"
          item-value="name"
          item-text="name"
          return-object
        >
        </v-select>
      </div>
    </v-sheet>

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

<script lang="ts">
import { defineComponent } from 'vue'
import {useKinoStore} from '@/stores/kinoStore'

export default defineComponent({
  name: 'MovieView',
  data: () => {
    return {
      useKinoStore: useKinoStore(),
      currentKino: "",
      kinoItems: []
    }
  },
  created() {
    if(this.useKinoStore.kino.length === 0) {
      this.useKinoStore.fetchKino();
    }
    console.log(this.useKinoStore.getKino() as any)
    // this.currentKino = (this.useKinoStore.kino[0] as any).name;
    // console.log(this.useKinoStore.kino)
    // this.kinoItems = this.useKinoStore.kino;
  },

})
</script>

<style scoped>

</style>