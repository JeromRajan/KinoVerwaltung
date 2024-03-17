

<template>
  <v-app>
    <v-app-bar app color="primary" dark >
      <v-toolbar-title>
        <v-icon>mdi-filmstrip</v-icon> Top Movie |
        <v-btn text to="/" class="ml-5"><v-icon icon="mdi-movie-open" class="mr-2"/>
          {{ $t('Navigation.movies') }}
        </v-btn>
        <v-btn v-if="useUserStore.getUserrole() === 3" text to="/memberarea" class="ml-5">
          <v-icon icon="mdi-card-account-details" class="mr-2"/>
          {{ $t('Navigation.memberarea') }}
        </v-btn>
        <v-btn v-if="useUserStore.getUserrole() === 1" text to="/statistics" class="ml-5">
          <v-icon icon="mdi-chart-bar" class="mr-2"/>
          {{ $t('Navigation.statistics') }}
        </v-btn>
        <v-btn v-if="useUserStore.getUserrole() === 1" text to="/administration" class="ml-5">
          <v-icon icon="mdi-cogs" class="mr-2"/>
          {{ $t('Navigation.administation') }}
        </v-btn>
        <v-btn v-if="useUserStore.getUserrole() === 2" text to="/employeearea" class="ml-5">
          <v-icon icon="mdi-account-hard-hat" class="mr-2"/>
          {{ $t('Navigation.employeearea') }}
        </v-btn>
      </v-toolbar-title>
      <div v-if="!useUserStore.user" >
        <v-btn text to="/login"><v-icon icon="mdi-login" class="mr-2"/> {{$t('Navigation.login')}}</v-btn>
        <v-btn text to="/register"><v-icon icon="mdi-account-plus" class="mr-2"/>  {{$t('Navigation.register')}}</v-btn>
      </div>
      <div v-else>
        <v-icon icon="mdi-account" class="mr-2"/>
        <span v-if="useUserStore.getUserrole() === 1">{{$t('Navigation.admin')}}</span>
        <span v-else-if="useUserStore.getUserrole() === 2">{{$t('Navigation.employee')}}</span>
        <span v-else >{{useUserStore.user.vorname}} {{useUserStore.user.nachname}}</span>
        <v-btn text @click="logout"><v-icon icon="mdi-logout" class="mr-2"/> {{$t('Navigation.logout')}}</v-btn>
      </div>


      |
      <v-menu location="bottom">
        <template v-slot:activator="{ props }">
          <v-btn
            text
            v-bind="props"
          >
            <v-icon class="mr-2">mdi-translate</v-icon>
            {{ $t('Navigation.languages') }}
          </v-btn>
        </template>
        <v-list>
          <v-list-item
            v-for="(lang, index) in languageStore.availableLanguages"
            :key="index"
            :value="index"
            @click="changeLanguage(lang)"
          >
            <v-list-item-title>{{ lang }}</v-list-item-title>
          </v-list-item>
        </v-list>
      </v-menu>
    </v-app-bar>

    <v-main class="pl-15 pr-15">
      <RouterView />
    </v-main>
  </v-app>
</template>

<script>
import { RouterView } from 'vue-router'
import { useLanguageStore } from '@/stores/languageStore'
import { useKinoStore } from '@/stores/kinoStore'
import { useUserStore } from '@/stores/userStore'

export default {
  components: {
    RouterView
  },
  name: 'TopMovie',
  data() {
    return {
      languageStore: useLanguageStore(),
      useKinoStore: useKinoStore(),
      useUserStore: useUserStore()
    }
  },
  created() {
    this.useKinoStore.fetchKino();
    this.useUserStore.checkUser();
  },
  methods: {
    changeLanguage(lang) {
      this.languageStore.setLanguage(lang);
    },
    logout() {
      this.useUserStore.logout();
    }
  }

}
</script>

<style scoped>
header {
  line-height: 1.5;
  max-height: 100vh;
}
</style>
