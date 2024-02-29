import { defineStore } from 'pinia'

export const useUserStore = defineStore('user', {
  state: () => ({
    users: [],

  }),
  actions: {
    setCurrentUser(user: any) {
      this.users = user;
    }
  }
});