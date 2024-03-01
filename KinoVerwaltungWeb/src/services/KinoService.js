import axios from 'axios';
import { globals } from '@/main.js'

class KinoService {

  constructor() {
    this.BASE_URL = globals.BASE_URL;
  }

  static getInstance() {
    if (!this.instance) {
      this.instance = new KinoService();
    }
    return this.instance;
  }

  getAllKinos = async () => {
    try {
      const response = await axios.get(`${this.BASE_URL}/kino`);
      if(response && response.data){
        return response.data.$values || [];
      }
      return [];
    } catch (error) {
      console.error('Failed to fetch kinos', error);
      throw error;
    }
  }

  getHallByKinoId = async (kinoId) => {
    try {
      const response = await axios.get(`${this.BASE_URL}/Kino/${kinoId}/saele`);
      if(response && response.data && response.data.saele  && response.data.saele.$values && response.data.saele.$values.length > 0){
        return response.data.saele.$values || [];
      }
      return [];
    } catch (error) {
      console.error('Failed to fetch hall', error);
      throw error;
    }
  }

  getMoviesByKinoIdAndHallId = async (kinoId, hallId) => {
    try {
      const response = await axios.get(`${this.BASE_URL}/Vorführung/Kino/${kinoId}/saal/${hallId}`);
      if(response && response.data && response.data.$values && response.data.$values.length > 0){
        return response.data.$values || [];
      }
      return [];
    } catch (error) {
      console.error('Failed to fetch movies', error);
      throw error;
    }
  }

  getDailyProgramByKinoIdAndHallId = async (kinoId, hallId) => {
    try {
      const response = await axios.get(`${this.BASE_URL}/Vorführung/heute/${kinoId}/${hallId}`);
      if(response && response.data && response.data.$values && response.data.$values.length > 0){
        return response.data.$values || [];
      }
      return [];
    } catch (error) {
      console.error('Failed to fetch daily program', error);
      throw error;
    }
  }

  getWeeklyProgramByKinoIdAndHallId = async (kinoId, hallId) => {
    try {
      const response = await axios.get(`${this.BASE_URL}/Vorführung/woche/${kinoId}/${hallId}`);
      if(response && response.data && response.data.$values && response.data.$values.length > 0){
        return response.data.$values || [];
      }
      return [];
    } catch (error) {
      console.error('Failed to fetch weekly program', error);
      throw error;
    }
  }
}

export default KinoService;
