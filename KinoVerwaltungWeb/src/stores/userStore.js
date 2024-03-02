import { defineStore } from 'pinia'
import UserService from '@/services/userService.js'

export const useUserStore = defineStore('user', {
  state: () => ({
    user: null
  }),
  actions: {
    login(userLogin) {
      const userService = UserService.getInstance()
      userService.login(userLogin)
        .then((user) => {
          console.log('User logged in', user)
          this.user = user
        })
    },
    logout() {
      this.user = null
    },
    register(user) {
      const userService = UserService.getInstance()
      userService.register(user)
        .then((user) => {
          console.log('User registered', user)
          this.user = user
        })
    }
  }
})