<template>
  <v-card
    class=" pa-5 pb-8"
    elevation="8"
    max-width="448"
    rounded="lg"
  >
    <h1>{{ $t('EmployeeArea.generateMembercard') }}</h1>

    <v-btn
      class="mt-5"
      color="blue"
      size="large"
      variant="tonal"
      @click="generateMembercard">
      {{ $t('EmployeeArea.generateMembercard') }}
    </v-btn>

    <h3 class="mt-5">{{ $t('EmployeeArea.identifycationNumber') }}: {{ identifikationsNummer }}</h3>

    <p></p>
  </v-card>
</template>

<script>
import jsPDF from 'jspdf'
import AdminService from '@/services/adminService.js'

export default {
  name: 'generateMembercard',
  data() {
    return {
      identifikationsNummer: ''
    }
  },
  methods: {
    async generateMembercard() {
      const adminService = AdminService.getInstance()
      const membercard = await adminService.generateMembercard(this.identifikationsNummer)
      if (membercard) {
        this.identifikationsNummer = membercard.identifikationsNummer
        this.generatePdf();
      }
    },

    generatePdf() {
      const doc = new jsPDF()
      doc.text(`${this.$t('EmployeeArea.identifycationNumber')}: ${this.identifikationsNummer}`, 10, 10)
      doc.save('identification-number.pdf')
    }
  }
}
</script>

<style scoped>

</style>