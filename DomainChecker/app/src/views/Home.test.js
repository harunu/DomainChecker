import React from 'react';
import { render, screen, fireEvent } from '@testing-library/react';
import { Provider } from 'react-redux';
import configureStore from 'redux-mock-store'; 
import Home from './Home'; 
const mockStore = configureStore([]);

describe('Home Component', () => {
  let store;

  beforeEach(() => {
    store = mockStore({
      search: {
        results: null,
        isLoading: false,
        error: null
      },
    });
  });

  test('renders search input and search button', () => {
    render(
      <Provider store={store}>
        <Home />
      </Provider>
    );

    expect(screen.getByPlaceholderText('Search domain...')).toBeInTheDocument();
    expect(screen.getByText('Search')).toBeInTheDocument();
  });


  test('validates domain input before search', () => {
    render(
      <Provider store={store}>
        <Home />
      </Provider>
    );

    const searchInput = screen.getByPlaceholderText('Search domain...');
    const searchButton = screen.getByText('Search');

-    fireEvent.change(searchInput, { target: { value: 'invalid_domain' } });
    fireEvent.click(searchButton);

-    expect(screen.getByText('Please enter a valid domain.')).toBeInTheDocument();
  });
});
