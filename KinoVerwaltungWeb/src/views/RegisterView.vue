<template>
  <v-row justify="center">
    <v-col
      cols="12"
      lg="6"
      md="8"
      sm="10"
    >
      <v-card ref="form">
        <v-card-text>
          <div v-if="isLoaded" class="centered">
            <v-progress-circular
              color="primary"
              indeterminate
            ></v-progress-circular>
          </div>

          <div v-else>
            <v-alert
              type="info"
              variant="tonal"
              class="mb-4"
            >
              {{ $t('Register.membercardIdentificationInfo') }}
            </v-alert>

            <v-text-field
              class="mb-4"
              prepend-inner-icon="mdi-card-account-details-outline"
              ref="mitgliederkarteIdentifikationsNummer"
              v-model="mitgliederkarteIdentifikationsNummer"
              :rules="[() => !!mitgliederkarteIdentifikationsNummer || $t('Register.fieldRequired')]"
              :label="$t('Register.membercardIdentificationNumber')"
              density="compact"
              variant="outlined"
              required
            ></v-text-field>

            <v-text-field
              class="mb-4"
              ref="email"
              prepend-inner-icon="mdi-email-outline"
              v-model="email"
              :rules="[() => !!email || $t('Register.fieldRequired')]"
              :label="$t('Register.emailAdress')"
              density="compact"
              variant="outlined"
              required
            ></v-text-field>

            <v-text-field
              v-model="password"
              :append-inner-icon="visible ? 'mdi-eye-off' : 'mdi-eye'"
              :type="visible ? 'text' : 'password'"
              density="compact"
              :label="$t('Register.password')"
              :rules="[() => !!password || $t('Register.fieldRequired')]"
              prepend-inner-icon="mdi-lock-outline"
              variant="outlined"
              @click:append-inner="visible = !visible"
            ></v-text-field>

            <v-text-field
              class="mb-4"
              prepend-inner-icon="mdi-account-outline"
              ref="vorname"
              v-model="vorname"
              :rules="[() => !!vorname || $t('Register.fieldRequired')]"
              :label="$t('Register.firstname')"
              density="compact"
              variant="outlined"
              required
            ></v-text-field>

            <v-text-field
              prepend-inner-icon="mdi-account-outline"
              class="mb-4"
              ref="nachname"
              v-model="nachname"
              :rules="[() => !!nachname || $t('Register.fieldRequired')]"
              :label="$t('Register.lastname')"
              density="compact"
              variant="outlined"
              required></v-text-field>

            <v-text-field
              prepend-inner-icon="mdi-phone-outline"
              class="mb-4"
              ref="telfon"
              v-model="telfon"
              :rules="[() => !!telfon || $t('Register.fieldRequired')]"
              :label="$t('Register.phone')"
              density="compact"
              variant="outlined"
              required>
            </v-text-field>

            <v-text-field
              prepend-inner-icon="mdi-map-marker-outline"
              class="mb-4"
              ref="strasse"
              v-model="strasse"
              :rules="[() => !!strasse || $t('Register.fieldRequired')]"
              :label="$t('Register.street')"
              density="compact"
              variant="outlined"
              required>
            </v-text-field>

            <v-text-field
              prepend-inner-icon="mdi-numeric"
              class="mb-4"
              ref="hausnumer"
              v-model="hausnumer"
              :rules="[() => !!hausnumer || $t('Register.fieldRequired')]"
              :label="$t('Register.streetNumber')"
              density="compact"
              variant="outlined"
              required>
            </v-text-field>

            <v-text-field
              prepend-inner-icon="mdi-numeric"
              class="mb-4"
              ref="plz"
              v-model="plz"
              :rules="[() => !!plz || $t('Register.fieldRequired')]"
              :label="$t('Register.zip')"
              density="compact"
              variant="outlined"
              required>
            </v-text-field>

            <v-text-field
              prepend-inner-icon="mdi-city"
              class="mb-4"
              ref="stadt"
              v-model="stadt"
              :rules="[() => !!stadt || $t('Register.fieldRequired')]"
              :label="$t('Register.city')"
              density="compact"
              variant="outlined"
              required>
            </v-text-field>

            <v-text-field
              prepend-inner-icon="mdi-earth"
              class="mb-4"
              ref="land"
              v-model="land"
              :rules="[() => !!land || $t('Register.fieldRequired')]"
              :label="$t('Register.country')"
              density="compact"
              variant="outlined"
              required>
            </v-text-field>

            <v-alert
              v-if="isError"
              class="mt-4 mb-4"
              dense
              outlined
              type="error"
              variant="tonal">
              {{ $t('Register.registerError') }}
            </v-alert>
          </div>

        </v-card-text>
        <v-divider ></v-divider>
        <v-card-actions>
          <v-spacer></v-spacer>

          <v-btn
            :disabled="isLoaded"
            color="primary"
            @click="submit"
          >
            {{ $t('Register.register') }}
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-col>
  </v-row>
</template>


<script>
import { useUserStore } from '@/stores/userStore.js'

export default {
  data: () => ({
    useUserStore: useUserStore(),
    land: 'Switzerland',
    mitgliederkarteIdentifikationsNummer: null,
    telfon: null,
    email: null,
    password: null,
    vorname: null,
    nachname: null,
    strasse: null,
    hausnumer: null,
    plz: null,
    stadt: null,
    visible: false,
    isLoaded: false,
  }),

  methods: {
    submit() {
      this.isLoaded = true
      const regitserObject = {
        email: this.email,
        passwort: this.password,
        vorname: this.vorname,
        nachname: this.nachname,
        telefon: this.telfon,
        rolleId: 3,
        adresse: {
          strasse: this.strasse,
          hausnummer: this.hausnumer,
          plz: this.plz,
          stadt: this.stadt,
          land: this.land
        },
        mitgliederkarteIdentifikationsNummer: this.mitgliederkarteIdentifikationsNummer
      }

      this.useUserStore.register(regitserObject);

      setTimeout(() => {
        this.isLoaded = false
        if (this.useUserStore.user) {
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
  height: 100vh;
}
</style>