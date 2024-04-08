
import React, { useState } from 'react';
import { Provider } from 'react-redux';
import { BrowserRouter as Router, Routes, Route, useLocation } from 'react-router-dom';
import { store } from './redux/store';
import Sidebar from './components/Sidebar'; 
import DomainSearch from './components/DomainSearch';
import Home from './views/Home';
import Favorites from './views/Favorites'; 
import './css/currentTitle.css';

const App = () => {
  const [isSidebarOpen, setIsSidebarOpen] = useState(true);

  const toggleSidebar = () => {
    setIsSidebarOpen(!isSidebarOpen);
  };

  const CurrentTitle = () => {
    let location = useLocation();
    let title = location.pathname.includes("/favorites") ? "Favorites" : location.pathname.includes("/search") ? "Domain Search" : "Home";
    
    let titleClass = location.pathname.includes("/favorites") ? "favorites-hero" : location.pathname.includes("/search") ? "search-hero" : "home-hero";
  
    return (
      <div className={`shared-horizontal-align ${titleClass}`}>
        <h1 className="page-title">{title}</h1>
      </div>
    );
  };
  return (
    <Provider store={store}>
      <Router>
        <div style={{ display: 'flex', height: '100vh' }}>
          <Sidebar toggleSidebar={toggleSidebar} isOpen={isSidebarOpen} />
          <div style={{ flex: 1 }}>
            <CurrentTitle />
            <Routes>
              <Route path="/" element={<Home />} />
              <Route path="/search" element={<DomainSearch />} /> 
              <Route path="/favorites" element={<Favorites />} />
            </Routes>
          </div>
        </div>
      </Router>
    </Provider>
  );
};

export default App;
