import axios from 'axios';

class KinoService {
  private static instance: KinoService;
  private BASE_URL = 'https://localhost:44336/api'; // replace with your server url

  private constructor() { }

  public static getInstance(): KinoService {
    if (!KinoService.instance) {
      KinoService.instance = new KinoService();
    }

    return KinoService.instance;
  }

  public getAllKinos = async () => {
    try {
      const response = await axios.get(`${this.BASE_URL}/kino`);
      if(response && response.data){
        return response.data?.$values;
      }
      return [];
    } catch (error) {
      console.error('Failed to fetch kinos', error);
      throw error;
    }
  }
}

export default KinoService;