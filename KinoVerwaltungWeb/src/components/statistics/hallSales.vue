<template>
  <div>
    <v-card
      :max-width="getWidth()"
      class=" pa-5 pb-8 "
      elevation="8"
      rounded="lg"
      min-width="300"
      width="100%"
    >
      <h1>{{ $t('Statistics.hallSales') }}</h1>

      <v-select
        v-model="selectedCinema"
        :items="cinemas"
        item-title="name"
        item-value="kinoId"
        label="Kino"
        @update:modelValue="loadHallSales"
      >
      </v-select>

      <v-progress-linear
        v-if="isLoading"
        color="blue"
        indeterminate>
      </v-progress-linear>

      <div class="mt-5" v-else-if="chartData && chartData.labels && chartData.labels.length">
        <Bar :data="chartData" :options="chartOptions" />
      </div>
    </v-card>
  </div>
</template>

<script>
import { Bar } from 'vue-chartjs'
import { BarElement, CategoryScale, Chart as ChartJS, Legend, LinearScale, Title, Tooltip } from 'chart.js'
import StatistikService from '@/services/statistikService.js'
import KinoService from '@/services/kinoService.js'

ChartJS.register(Title, Tooltip, Legend, BarElement, CategoryScale, LinearScale)

export default {
  components: {
    Bar
  },
  data() {
    return {
      statisikService: StatistikService.getInstance(),
      kinoService: KinoService.getInstance(),
      chartData: {
        labels: [], // Namen
        datasets: [
          {
            label: this.$t('Statistics.salesVolume'),
            backgroundColor: '#f87979',
            data: [] // Umsatz
          }
        ]
      },
      chartOptions: {
        responsive: true
      },
      cinemas: [],
      selectedCinema: null,
      isLoading: false
    }
  },
  created() {
    // this.loadCinemaOccupancy()
    this.getCinemas()
  },
  methods: {
    getCinemas() {
      this.kinoService.getAllKinos().then((response) => {
        this.cinemas = response
      })
    },

    // Daten für das Diagramm laden
    async loadHallSales() {
      this.isLoading = true
      const daten = await this.statisikService.getHallSales(this.selectedCinema)
      if (daten) {
        this.chartData.labels = []
        this.chartData.datasets[0].data = []
        daten.forEach(element => {
          this.chartData.labels.push(element.name)
          this.chartData.datasets[0].data.push(element.umsatz)
        })

        this.$nextTick(() => {
          this.isLoading = false
        })
      }else{
        this.isLoading = false
      }
    },

    getWidth() {
      if(this.chartData && this.chartData.labels && this.chartData.labels.length){
        return this.chartData.labels.length * 200
      } else {
        return '300'
      }
    }
  }
}
</script>
