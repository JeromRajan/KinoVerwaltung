import { defineStore } from 'pinia'
import UserService from '@/services/userService.js'

export const useUserStore = defineStore('user', {
  state: () => ({
    user: null,
    membercardBalance: 0
  }),
  actions: {
    login(userLogin) {
      const userService = UserService.getInstance()
      userService.login(userLogin)
        .then((user) => {
          this.user = user
          this.getMembercardBalance(user.benutzerId)
        })
    },
    logout() {
      this.user = null
    },
    register(user) {
      const userService = UserService.getInstance()
      userService.register(user)
        .then((user) => {
          this.user = user
          this.getMembercardBalance(user.benutzerId)
        })
    },
    getMembercardBalance(userId) {
      const userService = UserService.getInstance()
      userService.getMembercardBalance(userId)
        .then((balance) => {
          this.membercardBalance = balance
        })
    }
  }
})