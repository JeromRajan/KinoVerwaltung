import {defineStore} from 'pinia'

export const useKinoStore = defineStore('kino', {
  state: () => ({
    kino: [] as any[]
  }),
  actions: {
    addKino(kino: any) {
      this.kino.push(kino);
    }
  }
});
