import { defineStore } from 'pinia'
import KinoService from '@/services/KinoService.js'

export const useKinoStore = defineStore('kino', {
  state: () => ({
    kinos: [],
    halls: [],
    currentKino: null,
    currentHall: null,
    movies: [],
    currentMovie: null,
  }),
  actions: {
    async fetchKino() {
      const kinoService = KinoService.getInstance()
      this.kinos = await kinoService.getAllKinos()
      if(this.kinos.length > 0) {
        this.currentKino = this.kinos[0]
        this.fetchHall(this.currentKino.kinoId)
      }
    },
    async fetchHall(kinoId) {
      const kinoService = KinoService.getInstance()
      this.halls = await kinoService.getHallByKinoId(kinoId)
      if(this.halls.length > 0) {
        this.currentHall = this.halls[0]
      }
    },
    setCurrentKino(kino) {
      this.currentKino = kino
      this.fetchHall(kino.kinoId)
    }
  },
  getters: {
    getKino() {
      return this.kinos
    }
  }
})
