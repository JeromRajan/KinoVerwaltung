<template>
  <v-snackbar
    v-model="resevationSuccess"
    color="success"
    location="top"
    multi-line
    timeout="5000"
  >
    <v-icon left>mdi-check</v-icon>
    {{ $t('Movies.reservationSuccess') }}
    <template v-slot:actions>
      <v-btn
        variant="text"
        @click="resevationSuccess = false"
      >
        <v-icon left>mdi-close</v-icon>
      </v-btn>
    </template>
  </v-snackbar>

  <v-dialog :max-width="calaculateWidth()" min-width="800" v-model="dialog">
    <template v-slot:activator="{ props: activatorProps }">
      <v-btn
        color="primary"
        v-bind="activatorProps"
        variant="tonal"
        @click="resevationOpend"
      >
        {{ $t('Movies.reserve') }}
      </v-btn>
    </template>

    <template v-slot:default="{ isActive }">


      <v-card :title="$t('Movies.reserveMovie')" >
        <v-card-text>

          <v-progress-linear
            v-if="isLoading"
            color="blue"
            indeterminate>
          </v-progress-linear>

          <div v-if="!useUserStore.user">
            <v-alert
              class="mt-4"
              dense
              outlined
              variant="tonal"
              type="info">
              {{ $t('Movies.infoYouNeedToBeLoggedIn') }}
            </v-alert>
          </div>

          <div v-else>
            <div v-if="useUserStore.user && useUserStore.user.rolle.rolleId === 3">
              <v-select
                v-model="selectedZahlungsmethode"
                :items="zahlungsmethoden"
                :label="$t('Movies.paymentMethod')"
                item-title="name"
                item-value="zahlungsmethodeId"
                dense
                outlined
              ></v-select>
            </div>

            <div class="saal-uebersicht-container" v-if="sitze && sitze.reihen">
              <div class="leinwand">Leinwand</div>
              <div class="saal-uebersicht">
                <div v-for="reihe in sitze.reihen.$values" :key="reihe.reiheId" class="reihe">
                  <div class="reihe-label">{{$t('Movies.row')}} {{ reihe.reiheNummer }}</div>
                  <div class="reihe">
                    <v-btn
                      v-for="sitz in reihe.sitze.$values"
                      :key="sitz.sitzId"
                      :class="sitzFarbeBestimmen(sitz)"
                      class="sitz"
                      @click="sitzAuswaehlen(sitz, reihe.reiheNummer)"
                    >
                      <v-icon icon="mdi-seat"></v-icon>
                      {{ sitz.sitzNummer }}
                    </v-btn>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </v-card-text>

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn
            v-if="useUserStore.user && useUserStore.user.rolle.rolleId === 3 && selectedZahlungsmethode"
            :text="$t('Movies.reserve')"
            class="mr-2"
            color="primary"
            variant="tonal"
            @click="reseveSeats"
          ></v-btn>
          <v-btn
            v-if="useUserStore.user && useUserStore.user.rolle.rolleId === 2"
            :text="$t('Movies.book')"
            class="mr-2"
            color="primary"
            variant="tonal"
            @click="bookSeats"
          ></v-btn>
          <v-btn
            :text="$t('Administration.cancel')"
            @click="cancel"
          ></v-btn>
        </v-card-actions>
      </v-card>
    </template>
  </v-dialog>
</template>

<script>
import KinoService from '@/services/kinoService.js'
import {useUserStore} from '@/stores/userStore.js'
import TicketService from '@/services/ticketService.js'
import jsPDF from 'jspdf'
export default {
  name: 'SaalUebersicht',
  props: {
    vorführungId: {
      type: Number,
      required: true
    },
  },
  data() {
    return {
      ausgewaehlteSitze: [],
      sitze: {},
      maxAuswahl: 6,
      dialog: false,
      isLoading: false,
      kinoService: KinoService.getInstance(),
      useUserStore: useUserStore(),
      ticketService: TicketService.getInstance(),
      zahlungsmethoden: [],
      selectedZahlungsmethode: null,
      resevationSuccess: false
    }
  },
  created() {
    this.getZahlungsmethoden()
  },
  methods: {
    calaculateWidth() {
      if(this.sitze && this.sitze.reihen && this.sitze.reihen.$values.length > 0) {
        return this.sitze.reihen.$values[0].sitze.$values.length * 120
      }else{
        return 900
      }
    },
    getZahlungsmethoden() {
      this.isLoading = true
      this.ticketService.getPaymentMethods()
        .then(response => {
          this.zahlungsmethoden = response
        })
        .catch(error => {
          console.log(error)
        })
        .finally(() => {
          this.isLoading = false
        })
    },

    cancel() {
      this.dialog = false
    },
    bookSeats() {
      const doc = new jsPDF()
      this.isLoading = true
      if(this.ausgewaehlteSitze && this.ausgewaehlteSitze.length > 0) {
        let count = 0
        this.ausgewaehlteSitze.forEach(sitz => {
          this.ticketService.buyTicketInCinema(this.useUserStore.user.benutzerId,this.vorführungId,sitz.sitzId)
            .then(() => {
            })
            .catch(error => {
              this.isLoading = false
              console.error(error);
            })

          doc.text(`Reihe: ${sitz.reiheNummer} Sitz: ${sitz.sitzNummer}`, 10, 10 + (count * 10))
          count++
        })

        if(count === this.ausgewaehlteSitze.length) {
          this.$nextTick(() => {
            doc.save('tickets.pdf')
          })
          this.resevationSuccess = true
          this.isLoading = false
          this.dialog = false
          this.$emit('resevation-done')
        }
      }else {
        this.isLoading = false
      }
    },

    reseveSeats() {
      this.isLoading = true;
      if(this.ausgewaehlteSitze && this.ausgewaehlteSitze.length > 0 && this.useUserStore.user) {
        let count = 0

        this.ausgewaehlteSitze.forEach(sitz => {
          const reservierung = {
            sitzId: sitz.sitzId,
            vorführungId: this.vorführungId,
            zahlungsmethodeId: this.selectedZahlungsmethode,
            benutzerId: this.useUserStore.user.benutzerId
          }

          this.ticketService.reserveTicket(reservierung)
            .then(() => {
            })
            .catch(error => {
              this.isLoading = false
              console.error(error);
            })

          count++
        })

        if(count === this.ausgewaehlteSitze.length) {
          this.resevationSuccess = true
          this.isLoading = false
          this.dialog = false
          this.$emit('resevation-done')
        }
      }else {
        this.isLoading = false
      }
    },
    resevationOpend() {
      this.isLoading = true
      this.getSitzplan()
    },
    getSitzplan() {
      this.isLoading = true
      this.kinoService.getSitzplan(this.vorführungId)
        .then(response => {
          this.sitze = response
        })
        .catch(error => {
          console.log(error)
        })
        .finally(() => {
          this.isLoading = false
        })
    },

    sitzAuswaehlen(sitz , reiheNummer) {
      if (!sitz.istBesetzt && this.ausgewaehlteSitze.length < this.maxAuswahl) {
        const index = this.ausgewaehlteSitze.findIndex(x => x.sitzId === sitz.sitzId)
        if (index === -1) {
          sitz.reiheNummer = reiheNummer
          this.ausgewaehlteSitze.push(sitz)
        } else {
          this.ausgewaehlteSitze.splice(index, 1)
        }
      }
    },
    sitzFarbeBestimmen(sitz) {
      if (sitz.istBesetzt) return 'rot'
      if (this.ausgewaehlteSitze.some(x => x.sitzId === sitz.sitzId)) return 'gelb'
      return 'gruen'
    }
  }
}
</script>

<style>
.saal-uebersicht-container {
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-bottom: 20px;
}

.leinwand {
  width: 100%;
  padding: 10px 0;
  text-align: center;
  background-color: #333;
  color: white;
  font-weight: bold;
  margin-bottom: 20px;
}


.saal-uebersicht .reihe-container {
  display: flex;
  align-items: center;
}

.saal-uebersicht .reihe-label {
  margin-right: 10px;
  width: 100px;
  font-weight: bold;
}

.saal-uebersicht .reihe {
  display: flex;
}

.saal-uebersicht .sitz {
  width: 50px !important;
  height: 50px !important;
  border: 1px solid black;
  display: flex;
  justify-content: center;
  align-items: center;
  margin: 5px;
  cursor: pointer;
}

.rot {
  background: red !important;
}

.gruen {
  background: green !important;
}

.gelb {
  background: yellow !important;
}
</style>