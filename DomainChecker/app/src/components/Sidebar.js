import React from 'react';
import { NavLink } from 'react-router-dom';
import { CgMenu } from 'react-icons/cg';
import { AiOutlineHome, AiOutlineHeart } from 'react-icons/ai'; 
import '../css/sharedStyle.css'; 

const Sidebar = ({ toggleSidebar, isOpen }) => {
    const sidebarStyle = isOpen ? { width: '200px' } : { width: '50px', overflowX: 'hidden' };
    return (
        <div style={{ background: '#f0f0f0', height: '100vh', ...sidebarStyle, transition: 'width 0.3s ease' }}>
            <div className="shared-horizontal-align" style={{ justifyContent: 'center' }}>
               <CgMenu size="24" onClick={toggleSidebar} style={{ cursor: 'pointer', color: 'black' }} />
            </div>
            <NavLink to="/" className="nav-link" style={{ padding: '10px', display: isOpen ? 'flex' : 'none', alignItems: 'center', justifyContent: 'center' }}>
                <AiOutlineHome size="24" style={{ marginRight: '8px' }} />
                <span style={{ display: isOpen ? 'inline' : 'none' }}>Home</span>
            </NavLink>
            <NavLink to="/favorites" className="nav-link" style={{ padding: '10px', display: isOpen ? 'flex' : 'none', alignItems: 'center', justifyContent: 'center' }}>
                <AiOutlineHeart size="24" style={{ marginRight: '8px' }} />
                <span style={{ display: isOpen ? 'inline' : 'none' }}>Favorites</span>
            </NavLink>
        </div>
    );
};

export default Sidebar;
