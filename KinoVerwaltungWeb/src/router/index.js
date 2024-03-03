import { createRouter, createWebHistory } from 'vue-router'
import MovieView from '@/views/MovieView.vue'
import MemberAreaView from '@/views/MemberAreaView.vue'
import StatisticsView from '@/views/StatisticsView.vue'
import AdministrationView from '@/views/AdministrationView.vue'
import DetailMovieView from '@/components/kino/detailMovieView.vue'
import LoginView from '@/views/LoginView.vue'
import RegisterView from '@/views/RegisterView.vue'
import EmployeeAreaView from '@/views/EmployeeAreaView.vue'

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
    },
    {
      path: '/movie/:id',
      name: 'MovieDetail',
      component: DetailMovieView
    },
    {
      path: '/login',
      name: 'login',
      component: LoginView
    },
    {
      path: '/register',
      name: 'register',
      component: RegisterView
    },
    {
      path: '/employeearea',
      name: 'employeearea',
      component: EmployeeAreaView
    }
  ]
})

export default router
