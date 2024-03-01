import {defineStore} from 'pinia'
import KinoService from '@/services/KinoService'

export const useKinoStore = defineStore('kino', {
  state: () => ({
    kino: []
  }),
  actions: {
   async fetchKino() {
     const kinoService = KinoService.getInstance();
     this.kino = await kinoService.getAllKinos();
   }
  },
  getters: {
    getKino() : any[]{
      return this.kino;
    }
  }
});
