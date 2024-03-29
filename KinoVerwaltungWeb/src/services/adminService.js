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

  getMovies = async () => {
    try {
      const response = await axios.get(`${this.BASE_URL}/Film`);
      if(response && response.data){
        return response.data;
      }
      return [];
    } catch (error) {
      console.error('Failed to get movies', error);
      throw error;
    }
  }

  deleteMovie = async (movieId) => {
    try {
      const response = await axios.delete(`${this.BASE_URL}/Film/${movieId}`);
      if(response && response.data){
        return response.data;
      }
      return [];
    } catch (error) {
      console.error('Failed to delete movie', error);
      throw error;
    }
  }

  getGenres = async () => {
    try {
      const response = await axios.get(`${this.BASE_URL}/Film/genres`);
      if(response && response.data){
        return response.data;
      }
      return [];
    } catch (error) {
      console.error('Failed to get genres', error);
      throw error;
    }
  }

  getLanguages = async () => {
    try {
      const response = await axios.get(`${this.BASE_URL}/Film/sprachen`);
      if(response && response.data){
        return response.data;
      }
      return [];
    } catch (error) {
      console.error('Failed to get languages', error);
      throw error;
    }
  }

  addMovie = async (movie) => {
    try {
      const response = await axios.post(`${this.BASE_URL}/Film`, movie);
      if(response && response.data){
        return response.data;
      }
      return [];
    } catch (error) {
      console.error('Failed to add movie', error);
      throw error;
    }
  }
  saveMovie = async (movie) => {
    try {
      const response = await axios.put(`${this.BASE_URL}/Film/${movie.filmId}`, movie);
      if(response && response.data){
        return response.data;
      }
      return [];
    } catch (error) {
      console.error('Failed to save movie', error);
      throw error;
    }
  }

  getSaalsinfoByCinema = async (kinoId) => {
    try {
      const response = await axios.get(`${this.BASE_URL}/Kino/${kinoId}/saeleinfo`);
      if(response && response.data){
        return response.data;
      }
      return [];
    } catch (error) {
      console.error('Failed to get saals', error);
      throw error;
    }
  }

  deleteSaal = async (saalId) => {
    try {
      const response = await axios.delete(`${this.BASE_URL}/Kino/saal/${saalId}`);
      if(response && response.data){
        return response.data;
      }
      return [];
    } catch (error) {
      console.error('Failed to delete saal', error);
      throw error;
    }
  }

  createSaal = async (kinoId, saal) => {
    try {
      const response = await axios.post(`${this.BASE_URL}/Kino/${kinoId}/saal`, saal);
      if(response && response.data){
        return response.data;
      }
      return [];
    } catch (error) {
      console.error('Failed to create saal', error);
      throw error;
    }
  }

  updateSaal = async (saal) => {
    try {
      const response = await axios.put(`${this.BASE_URL}/Kino/saal/${saal.Saal.SaalId}`, saal);
      if(response && response.data){
        return response.data;
      }
      return [];
    } catch (error) {
      console.error('Failed to update saal', error);
      throw error;
    }
  }

  getShowsByCinemaIdAndHallId = async (kinoId, hallId) => {
    try {
      const response = await axios.get(`${this.BASE_URL}/Vorführung/Kino/${kinoId}/saal/${hallId}`);
      if(response && response.data){
        return response.data;
      }
      return [];
    } catch (error) {
      console.error('Failed to get shows', error);
      throw error;
    }
  }

  deleteShow = async (showId) => {
    try {
      const response = await axios.delete(`${this.BASE_URL}/Vorführung/${showId}`);
      if(response && response.data){
        return response.data;
      }
      return [];
    } catch (error) {
      console.error('Failed to delete show', error);
      throw error;
    }
  }

  addShow = async (show) => {
    try {
      const response = await axios.post(`${this.BASE_URL}/Vorführung`, show);
      if(response && response.data){
        return response.data;
      }
      return [];
    } catch (error) {
      console.error('Failed to add show', error);
      throw error;
    }
  }

  updateShow = async (show) => {
    console.log(show);
    try {
      const response = await axios.put(`${this.BASE_URL}/Vorführung/${show.vorführungId}`, show);
      if(response && response.data){
        return response.data;
      }
      return [];
    } catch (error) {
      console.error('Failed to update show', error);
      throw error;
    }
  }

  getCinemas = async () => {
    try {
      const response = await axios.get(`${this.BASE_URL}/Kino`);
      if(response && response.data && response.data.$values && response.data.$values.length > 0){
        return response.data.$values;
      }
      return [];
    } catch (error) {
      console.error('Failed to get cinemas', error);
      throw error;
    }
  }

  addCinema = async (cinema) => {
    try {
      const response = await axios.post(`${this.BASE_URL}/Kino`, cinema);
      if(response && response.data){
        return response.data;
      }
      return [];
    } catch (error) {
      console.error('Failed to add cinema', error);
      throw error;
    }
  }


}


export default AdminService;