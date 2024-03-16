<template>

  <div v-if="chartData && chartData.labels && chartData.labels.length">
    <v-card
      :max-width="200 * chartData.labels.length "
      class=" pa-5 pb-8 "
      elevation="8"
      rounded="lg"
      min-width="300"
      width="100%"
    >
      <h1>{{ $t('Statistics.cinemaSales') }}</h1>
      <div class="mt-5">
        <Bar :data="chartData" :options="chartOptions" />
      </div>
    </v-card>
  </div>
</template>

<script>
import { Bar } from 'vue-chartjs'
import { BarElement, CategoryScale, Chart as ChartJS, Legend, LinearScale, Title, Tooltip } from 'chart.js'
import StatistikService from '@/services/statistikService.js'

ChartJS.register(Title, Tooltip, Legend, BarElement, CategoryScale, LinearScale)

export default {
  components: {
    Bar
  },
  data() {
    return {
      statisikService: StatistikService.getInstance(),
      chartData: {
        labels: [], // Namen
        datasets: [
          {
            label: this.$t('Statistics.salesVolume'),
            backgroundColor: '#0583F2',
            data: [] // Sales Volume
          }
        ]
      },
      chartOptions: {
        responsive: true
      }
    }
  },
  created() {
    this.loadCinemaSales()
  },
  methods: {
    // Daten für das Diagramm laden
    async loadCinemaSales() {
      const daten = await this.statisikService.getCinemaSales()
      if (daten) {
        this.chartData.labels = []
        this.chartData.datasets[0].data = []
        daten.forEach(element => {
          this.chartData.labels.push(element.name)
          this.chartData.datasets[0].data.push(element.umsatz)
        })
      }

    }
  }
}
</script>
