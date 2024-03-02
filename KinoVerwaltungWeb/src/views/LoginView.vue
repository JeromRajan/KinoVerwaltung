<template>
  <div>
    <v-card
      class="mx-auto pa-12 pb-8"
      elevation="8"
      max-width="448"
      rounded="lg"
    >
      <div v-if="isLoaded" class="centered">
        <v-progress-circular
          color="primary"
          indeterminate
        ></v-progress-circular>
      </div>
      <div v-else>
        <div class="text-subtitle-1 text-medium-emphasis">{{ $t('Login.account') }}</div>

        <v-text-field
          v-model="email"
          :placeholder="$t('Login.emailAdress')"
          density="compact"
          prepend-inner-icon="mdi-email-outline"
          variant="outlined"
        ></v-text-field>

        <div class="text-subtitle-1 text-medium-emphasis d-flex align-center justify-space-between">
          {{ $t('Login.password') }}
        </div>

        <v-text-field
          v-model="password"
          :append-inner-icon="visible ? 'mdi-eye-off' : 'mdi-eye'"
          :placeholder="$t('Login.enterPassword')"
          :type="visible ? 'text' : 'password'"
          density="compact"
          prepend-inner-icon="mdi-lock-outline"
          variant="outlined"
          @click:append-inner="visible = !visible"
        ></v-text-field>

        <v-alert
          v-if="isError"
          class="mt-4 mb-4"
          dense
          outlined
          type="error"
          variant="tonal">
          {{ $t('Login.loginError') }}
        </v-alert>

        <v-btn
          block
          class="mb-8"
          color="blue"
          size="large"
          variant="tonal"
          @click="login"
        >
          {{ $t('Login.login') }}
        </v-btn>
      </div>
    </v-card>
  </div>
</template>

<script>
import { useUserStore } from '@/stores/userStore.js'

export default {
  name: 'LoginView',
  data: () => ({
    useUserStore: useUserStore(),
    visible: false,
    email: '',
    password: '',
    isError: false,
    isLoaded: false
  }),
  methods: {
    login() {
      this.isLoaded = true
      this.useUserStore.login({ email: this.email, passwort: this.password })

      setTimeout(() => {
        this.isLoaded = false
        if (this.useUserStore.user) {
          console.log(this.useUserStore.user)
          this.isError = false
          this.$router.push({ name: 'movie' })
        } else {
          this.isError = true
        }
      }, 1000)

    }
  }
}
</script>

<style scoped>
.centered {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 20vh; /* Adjust as needed */
}
</style>