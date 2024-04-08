import React, { useEffect, useState} from 'react';
import { useDispatch } from 'react-redux'; 
import { fetchFavorites, removeFromFavorites } from '../services/domainService';
import '../css/favorites.css';
import { performSearch } from '../redux/searchSlice';

const FavoriteDomainCard = ({ domain, onSelected, isSelected, onReCheck }) => (
  <div className="domainCard">
    <label className="customCheckbox">
      <input type="checkbox" checked={isSelected} onChange={() => onSelected(domain.id)} />
      <span className="checkmark"></span>
    </label>
    <div className="cardContent">
      <h3>{domain.name}</h3>
      <p><strong>Available:</strong> {domain.isAvailable ? 'Yes' : 'No'}</p>
      <p><strong>Last Checked:</strong> {new Date(domain.lastChecked).toLocaleString()}</p>
      <p><strong>Expiry Date:</strong> {domain.expiryDate ? new Date(domain.expiryDate).toLocaleDateString() : 'N/A'}</p>
    </div>
    <button onClick={() => onReCheck(domain)} className="actionButton">Re-check</button>
  </div>
);

const Favorites = () => {
  const [favorites, setFavorites] = useState([]);
  const [selectedIds, setSelectedIds] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [isLoading, setIsLoading] = useState(false);
  const itemsPerPage = 3; 

  const dispatch = useDispatch();

  const onReCheck = async (domain) => {
    const actionResult = await dispatch(performSearch(domain.name));
    const result = actionResult.payload; 
    if (result) {
      // Update the specific domain in the favorites list with new result
      setFavorites(favorites.map(fav => fav.id === domain.id ? { ...fav, ...result } : fav));
    }
  };
  const loadFavorites = async () => {
    setIsLoading(true); // Indicate loading
    try {
      const fetchedFavorites = await fetchFavorites();
      setTimeout(() => { // Simulate network delay for clearer visual feedback
        setFavorites(fetchedFavorites);
        setIsLoading(false); 
      }, 500); 
    } catch (error) {
      console.error("Failed to fetch favorites:", error);
      setIsLoading(false); 
    }
  };
  const handleDelete = async () => {
    for (let id of selectedIds) {
      try {
        await removeFromFavorites(id);
      } catch (error) {
        console.error("Failed to remove from favorites:", error);
      }
    }
    setSelectedIds([]);
  
    // Reload the favorites from the server to get the updated list
    await loadFavorites();
  
    setIsLoading(true); 
    try {
      const updatedFavorites = await fetchFavorites(); // Re-fetch the updated list of favorites
      setFavorites(updatedFavorites); // Update the state with the fetched favorites
      setIsLoading(false);
  
      // Calculate the new total number of pages
      const newTotalPages = Math.ceil(updatedFavorites.length / itemsPerPage);
      if (currentPage > newTotalPages) {
        setCurrentPage(newTotalPages > 0 ? newTotalPages : 1);
      } else if (newTotalPages === 0) {
        // If there are no pages left, ensure currentPage reflects no content
        setCurrentPage(1);
      }
    } catch (error) {
      console.error("Failed to reload favorites:", error);
      setIsLoading(false);
    }
  };
  
  const toggleSelected = (id) => {
    setSelectedIds(ids => ids.includes(id) ? ids.filter(_id => _id !== id) : [...ids, id]);
  };

  useEffect(() => {
    loadFavorites();
  }, []);

  const handleRefresh = () => {
    setFavorites([]); // Clear current favorites to visually indicate refresh
    loadFavorites(); 
  };

  const handleNext = () => {
    setCurrentPage((prevCurrentPage) => prevCurrentPage + 1);
  };

  const handlePrev = () => {
    setCurrentPage((prevCurrentPage) => prevCurrentPage - 1);
  };

  const hasNext = currentPage * itemsPerPage < favorites.length;
  // Calculate if the current page is not the first page
  const hasPrev = currentPage > 1;

  // Get current page's favorites
  const currentFavorites = favorites.slice(
    (currentPage - 1) * itemsPerPage,
    currentPage * itemsPerPage
  );

  return (
    <div className="favoritesContainer">
      <h2 className="favoritesTitle">Favorites</h2>
      <div className="cardsContainer">
        {isLoading ? (
          <div className="loader"></div>
        ) : (
          currentFavorites.length > 0 ? (
            currentFavorites.map((domain) => (
              <FavoriteDomainCard
                key={domain.id}
                domain={domain}
                onSelected={toggleSelected}
                isSelected={selectedIds.includes(domain.id)}
                onReCheck={() => onReCheck(domain)} 
              />
            ))
          ) : (
            <div className="noFavoritesMessage">No favorite domains found.</div>
          )
        )}
      </div>
      <div className="actionButtons">
        <button onClick={handleRefresh} className="actionButton refreshButton" disabled={isLoading}>Refresh</button>
        <button onClick={handleDelete} className="actionButton deleteButton" disabled={selectedIds.length === 0}>Delete Selected</button>
      </div>
      {favorites.length > itemsPerPage && (
        <div className="pagination">
          {hasPrev && (
            <button onClick={handlePrev} className="actionButton">{"<"}</button>
          )}
          {hasNext && (
            <button onClick={handleNext} className="actionButton">{">"}</button>
          )}
        </div>
      )}
    </div>
  );  
};

export default Favorites;
