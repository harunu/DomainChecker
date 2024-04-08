
import React, { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { performSearch, resetSearchState } from '../redux/searchSlice';
import { addToFavorites } from '../services/domainService';
import '../css/home.css'; 

const Home = () => {
  const [searchInput, setSearchInput] = useState('');
  const [isValidDomain, setIsValidDomain] = useState(true);
  const [hasSearched, setHasSearched] = useState(false);
  const [notification, setNotification] = useState({ message: '', show: false });
  const dispatch = useDispatch();

  const { results, isLoading, error } = useSelector((state) => state.search);

  useEffect(() => {
    return () => {
      dispatch(resetSearchState());
    };
  }, [dispatch]);

  const showNotification = (message) => {
    setNotification({ message, show: true });
    setTimeout(() => setNotification({ message: '', show: false }), 3000);
  };

  const handleAddToFavorites = async () => {
    if (!hasSearched || !results) {
      showNotification("Please check the domain's availability first!");
      return;
    }
    try {
      await addToFavorites(results.name, results.isAvailable, results.expiryDate);
      showNotification(`${results.name} added to favorites!`);
    } catch (error) {
      console.error("Failed to add to favorites:", error);
      showNotification("Failed to add domain to favorites.");
    }
  };

  const validateDomain = (domain) => {
    const pattern = /^(?:[a-z0-9](?:[a-z0-9-]{0,61}[a-z0-9])?\.)+[a-z0-9][a-z0-9-]{0,61}[a-z0-9]$/;
    return pattern.test(domain);
  };

  const handleSearch = () => {
    if (validateDomain(searchInput)) {
      setIsValidDomain(true);
      setHasSearched(true);
      dispatch(performSearch(searchInput));
    } else {
      setIsValidDomain(false);
      setHasSearched(false);
      dispatch(resetSearchState());
    }
  };

  return (
    <>
      <div className="mainContent">
        <div className="searchContainer">
          <input
            className="searchInput"
            type="text"
            placeholder="Search domain..."
            value={searchInput}
            onChange={(e) => setSearchInput(e.target.value)}
          />
          <button className="searchButton" onClick={handleSearch}>Search</button>
        </div>
        {!isValidDomain && <div className="notification warning">Please enter a valid domain.</div>}
        {isLoading && <div className="loader"></div>}
        {error && <div className="notification error">{error}</div>}
        {notification.show && <div className="notification">{notification.message}</div>}
        {hasSearched && results && (
          <div className="resultsContainer">
            <h3>Search Result:</h3>
            <p><strong>Name:</strong> {results.name}</p>
            <p><strong>Available:</strong> {results.isAvailable ? 'Yes' : 'No'}</p>
            <p><strong>Last Checked:</strong> {results.lastChecked ? new Date(results.lastChecked).toLocaleString() : 'N/A'}</p>
            <p><strong>Expiry Date:</strong> {results.expiryDate ? new Date(results.expiryDate).toLocaleDateString() : 'N/A'}</p>
          </div>
        )}
      </div>
      {hasSearched && results && (
        <button className="addToFavoritesButton" onClick={handleAddToFavorites}>Add to Favorites</button>
      )}
    </>
  );
};

export default Home;
