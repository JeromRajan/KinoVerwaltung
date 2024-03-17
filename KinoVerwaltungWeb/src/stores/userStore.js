import { defineStore } from 'pinia'
import UserService from '@/services/userService.js'

const STORE_USER = 'USER'

export const useUserStore = defineStore('user', {
  state: () => ({
    user: null,
    membercardBalance: 0,
  }),
  actions: {
    login(userLogin) {
      const userService = UserService.getInstance()
      userService.login(userLogin)
        .then((user) => {
          this.user = user
          this.getMembercardBalance(user.benutzerId)
          localStorage.setItem(STORE_USER, JSON.stringify(this.user))
        })
    },
    logout() {
      this.user = null
      localStorage.setItem(STORE_USER, null)
    },
    register(user) {
      const userService = UserService.getInstance()
      userService.register(user)
        .then((user) => {
          this.user = user
          this.getMembercardBalance(user.benutzerId)
          localStorage.setItem(STORE_USER, JSON.stringify(this.user))
        })
    },
    getMembercardBalance(userId) {
      const userService = UserService.getInstance()
      userService.getMembercardBalance(userId)
        .then((balance) => {
          this.membercardBalance = balance
        })
    },
    getMembercard(userId) {
      const userService = UserService.getInstance()
      return userService.getMembercard(userId)
    },
    getUserrole() {
      if(this.user == null){
        return -1
      }else{
        return this.user.rolleId
      }
    },
    checkUser() {
      if(localStorage.getItem(STORE_USER)){
        this.user = JSON.parse(localStorage.getItem(STORE_USER))
        if(this.user){
          this.getMembercardBalance(this.user.benutzerId)
          this.getMembercard(this.user.benutzerId).then((membercard) => {
            if(membercard){
              this.user.mitgliederkarte = membercard
            }
          })
        }
      }
    }
  }
})