import React from 'react';

const SearchResult = ({ domainInfo }) => {
  return (
    <div style={{ padding: '20px', border: '1px solid #ccc', borderRadius: '5px', marginTop: '20px', backgroundColor: '#f9f9f9' }}>
      <h2>Domain Information</h2>
      <p><strong>Name:</strong> {domainInfo.name}</p>
      <p><strong>Available:</strong> {domainInfo.isAvailable ? 'Yes' : 'No'}</p>
      <p><strong>Last Checked:</strong> {new Date(domainInfo.lastChecked).toLocaleString()}</p>
      <p><strong>Expiry Date:</strong> {new Date(domainInfo.expiryDate).toLocaleDateString()}</p>
    </div>
  );
};

export default SearchResult;
