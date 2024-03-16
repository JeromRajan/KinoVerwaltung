import { createRouter, createWebHistory } from 'vue-router'
import MovieView from '@/views/FilmView.vue'
import MemberAreaView from '@/views/MitgliederView.vue'
import StatisticsView from '@/views/StatistikView.vue'
import AdministrationView from '@/views/AdministrationView.vue'
import DetailMovieView from '@/components/kino/detailFilmView.vue'
import LoginView from '@/views/LoginView.vue'
import RegisterView from '@/views/RegisterView.vue'
import EmployeeAreaView from '@/views/MitarbeiterView.vue'

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
      name: 'mitgliederBereich',
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
      name: 'mitarbeiterBereich',
      component: EmployeeAreaView
    }
  ]
})

export default router
