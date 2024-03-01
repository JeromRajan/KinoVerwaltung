import { createRouter, createWebHistory } from 'vue-router'
import MovieView from '@/views/MovieView.vue'
import MemberAreaView from '@/views/MemberAreaView.vue'
import StatisticsView from '@/views/StatisticsView.vue'
import AdministrationView from '@/views/AdministrationView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'movie',
      component: MovieView
    },
    {
      path: '/memberarea',
      name: 'memberarea',
      component: MemberAreaView
    },
    {
      path: '/statistics',
      name: 'statistics',
      component: StatisticsView
    },
    {
      path: '/administration',
      name: 'administration',
      component: AdministrationView
    }
  ]
})

export default router
