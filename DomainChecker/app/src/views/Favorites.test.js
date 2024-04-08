import React from 'react';
import { Provider } from 'react-redux';
import configureMockStore from 'redux-mock-store';
import { render, screen } from '@testing-library/react';
import Favorites from './Favorites'; 

const mockStore = configureMockStore();
const store = mockStore({}); 

describe('Favorites Component', () => {
  test('component renders without crashing', () => {
    render(
      <Provider store={store}>
        <Favorites />
      </Provider>
    );
    expect(screen.getByText('Favorites')).toBeInTheDocument();
  });

  test('displays the title "Favorites"', () => {
    render(
      <Provider store={store}>
        <Favorites />
      </Provider>
    );
    expect(screen.getByText(/Favorites/i)).toBeInTheDocument();
  });

  test('"Refresh" button is visible', () => {
    render(
      <Provider store={store}>
        <Favorites />
      </Provider>
    );
    expect(screen.getByText(/Refresh/i)).toBeInTheDocument();
  });
});
