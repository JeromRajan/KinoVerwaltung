import axios from 'axios';
import { globals } from '@/main.js'

class TicketService {
  constructor() {
    this.BASE_URL = globals.BASE_URL;
  }

  static getInstance() {
    if (!this.instance) {
      this.instance = new TicketService();
    }
    return this.instance;
  }

  reserveTicket = async (ticket) => {
    try {
      const response = await axios.post(`${this.BASE_URL}/ticket`, ticket);
      if(response && response.data){
        return response.data;
      }
      return null;
    } catch (error) {
      console.error('Failed to reserve ticket', error);
      throw error;
    }
  }

  getTicketByUserId = async (userId) => {
    try {
      const response = await axios.get(`${this.BASE_URL}/ticket/${userId}`);
      if(response && response.data && response.data.$values){
        return response.data.$values;
      }
      return null;
    } catch (error) {
      console.error('Failed to fetch ticket', error);
      throw error;
    }
  }

  confirmTicket = async (ticketId) => {
    try {
      const response = await axios.post(`${this.BASE_URL}/Ticket/${ticketId}`);
      if(response && response.data){
        return response.data;
      }
      return null;
    } catch (error) {
      console.error('Failed to confirm ticket', error);
      throw error;
    }
  }

  buyTicketInCinema = async (userId, vorstellungId, sitzId) => {
    try {
      const response = await axios.post(`${this.BASE_URL}/ticket/InKino?BenutzerId=${userId}&VorführungId=${vorstellungId}&SitzId=${sitzId}`);
      if(response && response.data){
        return response.data;
      }
      return null;
    } catch (error) {
      console.error('Failed to buy ticket in cinema', error);
      throw error;
    }
  }

  getPaymentMethods = async () => {
    try {
      const response = await axios.get(`${this.BASE_URL}/Zahlung`);
      if(response && response.data){
        return response.data.$values || [];
      }
      return [];
    } catch (error) {
      console.error('Failed to fetch payment methods', error);
      throw error;
    }
  }

  buyTicketWithMembercard = async (ticketReferenz, membercardReferenz) => {
    try {
      const response = await axios.post(`${this.BASE_URL}/Zahlung/reservierung/mitgliederkarte?referenzNummer=${ticketReferenz}&identifikationsNummer=${membercardReferenz}`);
      if(response && response.data){
        return response.data;
      }
      return null;
    } catch (error) {
      console.error('Failed to buy ticket with membercard', error);
      throw error;
    }
  }

  buyTicketWithCash = async (ticketReferenz) => {
    try {
      const response = await axios.post(`${this.BASE_URL}/Zahlung/reservierung/bar?referenzNummer=${ticketReferenz}`);
      if(response && response.data){
        return response.data;
      }
      return null;
    } catch (error) {
      console.error('Failed to buy ticket with cash', error);
      throw error;
    }
  }



}

export default TicketService;
