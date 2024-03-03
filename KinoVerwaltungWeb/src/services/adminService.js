import axios from 'axios';
import { globals } from '@/main.js'

class AdminService {
  constructor() {
    this.BASE_URL = globals.BASE_URL;
  }

  static getInstance() {
    if (!this.instance) {
      this.instance = new AdminService();
    }
    return this.instance;
  }

  generateMembercard = async () => {
    try {
      const response = await axios.post(`${this.BASE_URL}/Mitgliederkarten`);
      if(response && response.data){
        return response.data;
      }
      return [];
    } catch (error) {
      console.error('Failed to generate membercard', error);
      throw error;
    }
  }

  addBalance = async (membercardId, amount) => {
    try {
      const response = await axios.post(`${this.BASE_URL}/Mitgliederkarten/${membercardId}/aufladen/${amount}`);
      if(response && response.data){
        return response.data;
      }
      return [];
    } catch (error) {
      console.error('Failed to add balance', error);
      throw error;
    }
  }
}


export default AdminService;