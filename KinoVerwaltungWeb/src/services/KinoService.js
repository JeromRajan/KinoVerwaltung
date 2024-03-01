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
      console.log(response.data);
      if(response && response.data && response.data.saele  && response.data.saele.$values && response.data.saele.$values.length > 0){
        return response.data.saele.$values || [];
      }
      return [];
    } catch (error) {
      console.error('Failed to fetch hall', error);
      throw error;
    }
  }
}

export default KinoService;
