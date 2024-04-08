
import React, { useState } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { performSearch } from '../redux/searchSlice'; 
import SearchResult from './SearchResult';
const DomainSearch = () => {
  const [searchInput, setSearchInput] = useState('');
  const dispatch = useDispatch();
  const { results, isLoading, error } = useSelector((state) => state.search);

  const handleSearch = () => {
    dispatch(performSearch(searchInput));
  };

  return (
    <div>
      <input
        type="text"
        value={searchInput}
        onChange={(e) => setSearchInput(e.target.value)}
        placeholder="Search domain..."
      />
      <button onClick={handleSearch} disabled={isLoading}>
        {isLoading ? 'Searching...' : 'Search'}
      </button>
      {error && <p style={{ color: 'red' }}>{error}</p>}
      {results && !isLoading && <SearchResult domainInfo={results} />}
    </div>
  );
};

export default DomainSearch;
