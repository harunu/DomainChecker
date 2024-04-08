import axios from 'axios';

const BASE_URL = 'http://localhost:8000/Domains';
//const BASE_URL = 'https://localhost:44306/Domains';
export const checkDomainAvailability = async (domainName) => {
  try {
    const response = await axios.get(`${BASE_URL}/check?domainName=${encodeURIComponent(domainName)}`);
    return response.data;
  } catch (error) {
    throw error; 
  }
};

export const addToFavorites = async (domainName, isAvailable, expiryDate) => {
  try {
    const payload = {
      domainName,
      isAvailable,
      expiryDate: expiryDate !== 'N/A' ? expiryDate : null
    };

    const response = await axios.post(`${BASE_URL}/addToFavorites`, payload);

    return response.data;
  } catch (error) {
    console.error("Failed to add to favorites:", error);
    throw error; 
  }
};

export const fetchFavorites = async () => {
  try {
    const response = await axios.get(`${BASE_URL}/favorites`);
    return response.data;
  } catch (error) {
    throw error;
  }
};

export const removeFromFavorites = async (domainId) => {
  try {
    await axios.delete(`${BASE_URL}/removeFromFavorites/${domainId}`);
  } catch (error) {
    throw error;
  }
};
