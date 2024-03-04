import axios from 'axios';
import { globals } from '@/main.js'

class StatistikService {

  constructor() {
    this.BASE_URL = globals.BASE_URL;
  }

  static getInstance() {
    if (!this.instance) {
      this.instance = new StatistikService();
    }
    return this.instance;
  }

  getCinemaOccupancy = async () => {
    try {
      const response = await axios.get(`${this.BASE_URL}/Statistik/belegung/kinos`);
      if(response && response.data){
        return response.data.$values || [];
      }
      return [];
    } catch (error) {
      console.error('Failed to fetch kinos', error);
      throw error;
    }
  }

  getHallOccupancy = async (kinoId) => {
    try {
      const response = await axios.get(`${this.BASE_URL}/Statistik/belegung/saals/${kinoId}`);
      if(response && response.data){
        return response.data.$values || [];
      }
      return [];
    } catch (error) {
      console.error('Failed to fetch hall', error);
      throw error;
    }
  }

  getCinemaSales = async () => {
    try {
      const response = await axios.get(`${this.BASE_URL}/Statistik/umsatz/kinos`);
      if(response && response.data){
        return response.data.$values || [];
      }
      return [];
    } catch (error) {
      console.error('Failed to fetch kinos', error);
      throw error;
    }
  }

  getHallSales = async (kinoId) => {
    try {
      const response = await axios.get(`${this.BASE_URL}/Statistik/umsatz/saals/${kinoId}`);
      if(response && response.data){
        return response.data.$values || [];
      }
      return [];
    } catch (error) {
      console.error('Failed to fetch hall', error);
      throw error;
    }
  }

  getMovieSales = async () => {
    try {
      const response = await axios.get(`${this.BASE_URL}/Statistik/umsatz/films`);
      if(response && response.data){
        return response.data.$values || [];
      }
      return [];
    } catch (error) {
      console.error('Failed to fetch movies', error);
      throw error;
    }
  }

  getUserSales = async (userId) => {
    try {
      const response = await axios.get(`${this.BASE_URL}/Statistik/umsatz/benutzer/${userId}`);
      if(response && response.data){
        return response.data || [];
      }
      return [];
    } catch (error) {
      console.error('Failed to fetch user', error);
      throw error;
    }
  }
}

export default StatistikService;
