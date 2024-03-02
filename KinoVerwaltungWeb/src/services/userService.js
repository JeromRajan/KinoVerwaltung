import axios from 'axios';
import { globals } from '@/main.js'

class UserService{
    constructor() {
      this.BASE_URL = globals.BASE_URL;
    }

    static getInstance() {
      if (!this.instance) {
        this.instance = new UserService();
      }
      return this.instance;
    }

    login = async (userLogin) => {
      try {
        const response = await axios.post(`${this.BASE_URL}/Benutzer/login`, userLogin);
        if(response && response.data){
          return response.data;
        }
        return [];
      } catch (error) {
        console.error('Failed to login user', error);
        throw error;
      }
    }

    register = async (user) => {
      try {
        const response = await axios.post(`${this.BASE_URL}/Benutzer/registrieren`, user);
        console.log("register response");
        console.log(response);
        if(response && response.data){
          return response.data;
        }
        return [];
      } catch (error) {
        console.error('Failed to register user', error);
        throw error;
      }
    }
}

export default UserService;