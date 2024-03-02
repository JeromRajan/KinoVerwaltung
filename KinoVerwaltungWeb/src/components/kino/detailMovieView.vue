<template>
  <div class="saal-uebersicht-container">
    <div class="leinwand">Leinwand</div>
    <div class="saal-uebersicht">
      <div v-for="reihe in sitze.reihen.$values" :key="reihe.reiheId" class="reihe">
        <div class="reihe-label">Reihe {{ reihe.reiheNummer }}</div>
        <div class="reihe">
          <v-btn
            v-for="sitz in reihe.sitze.$values"
            :key="sitz.sitzId"
            :class="sitzFarbeBestimmen(sitz)"
            class="sitz"
            @click="sitzAuswaehlen(sitz)"
          >
            <v-icon icon="mdi-seat"></v-icon>
            {{ sitz.sitzNummer }}
          </v-btn>
        </div>

      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'SaalUebersicht',
  data() {
    return {
      ausgewaehlteSitze: [],
      sitze: {
        '$id': '1',
        'vorführungId': 3003,
        'saalId': 3002,
        'reihen': {
          '$id': '2',
          '$values': [
            {
              '$id': '3',
              'reiheId': 3002,
              'reiheNummer': 1,
              'sitze': {
                '$id': '4',
                '$values': [
                  {
                    '$id': '5',
                    'sitzId': 3002,
                    'sitzNummer': 1,
                    'istBesetzt': true
                  },
                  {
                    '$id': '6',
                    'sitzId': 3003,
                    'sitzNummer': 2,
                    'istBesetzt': false
                  },
                  {
                    '$id': '7',
                    'sitzId': 3004,
                    'sitzNummer': 3,
                    'istBesetzt': false
                  }
                ]
              }
            },
            {
              '$id': '8',
              'reiheId': 3003,
              'reiheNummer': 2,
              'sitze': {
                '$id': '9',
                '$values': [
                  {
                    '$id': '10',
                    'sitzId': 3005,
                    'sitzNummer': 1,
                    'istBesetzt': false
                  },
                  {
                    '$id': '11',
                    'sitzId': 3006,
                    'sitzNummer': 2,
                    'istBesetzt': false
                  },
                  {
                    '$id': '12',
                    'sitzId': 3007,
                    'sitzNummer': 3,
                    'istBesetzt': false
                  }
                ]
              }
            },
            {
              '$id': '8',
              'reiheId': 3003,
              'reiheNummer':3,
              'sitze': {
                '$id': '9',
                '$values': [
                  {
                    '$id': '10',
                    'sitzId': 3005,
                    'sitzNummer': 1,
                    'istBesetzt': false
                  },
                  {
                    '$id': '11',
                    'sitzId': 3006,
                    'sitzNummer': 2,
                    'istBesetzt': false
                  },
                  {
                    '$id': '12',
                    'sitzId': 3007,
                    'sitzNummer': 3,
                    'istBesetzt': false
                  }
                ]
              }
            }
          ]
        }
      },
      maxAuswahl: 6
    }
  },
  methods: {
    sitzAuswaehlen(sitz) {
      if (!sitz.istBesetzt && this.ausgewaehlteSitze.length < this.maxAuswahl) {
        const index = this.ausgewaehlteSitze.findIndex(x => x.sitzId === sitz.sitzId)
        if (index === -1) {
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