import { defineStore } from 'pinia'
import KinoService from '@/services/kinoService.js'

export const useKinoStore = defineStore('kino', {
  state: () => ({
    kinos: [],
    halls: [],
    currentKino: null,
    currentHall: null,
    movies: [],
    currentMovie: null
  }),
  actions: {
    async fetchKino() {
      const kinoService = KinoService.getInstance()
      this.kinos = await kinoService.getAllKinos()
      if (this.kinos.length > 0) {
        this.currentKino = this.kinos[0]
        this.fetchHall(this.currentKino.kinoId)
      }
    },
    async fetchHall(kinoId) {
      this.movies = []
      const kinoService = KinoService.getInstance()
      this.halls = await kinoService.getHallByKinoId(kinoId)
      if (this.halls.length > 0) {
        this.currentHall = this.halls[0]
      }
    },
    async fetchMovies() {
      const kinoService = KinoService.getInstance()
      this.movies = await kinoService.getMoviesByKinoIdAndHallId(this.currentKino.kinoId, this.currentHall.saalId)
    },
    async fetchDailyProgram() {
      const kinoService = KinoService.getInstance()
      this.movies = await kinoService.getDailyProgramByKinoIdAndHallId(this.currentKino.kinoId, this.currentHall.saalId)
    },
    async fetchWeeklyProgram() {
      const kinoService = KinoService.getInstance()
      this.movies = await kinoService.getWeeklyProgramByKinoIdAndHallId(this.currentKino.kinoId, this.currentHall.saalId)
    },
    setCurrentKino(kino) {
      this.currentKino = kino
      this.fetchHall(kino.kinoId)
    },
    setCurrentMovie(movie) {
      this.currentMovie = movie
    }
  },
  getters: {
    getKino() {
      return this.kinos
    }
  }
})
