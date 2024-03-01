import axios from 'axios';

class KinoService {
  constructor() {
    this.BASE_URL = 'https://localhost:44336/api'; // replace with your server url
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
}

export default KinoService;
