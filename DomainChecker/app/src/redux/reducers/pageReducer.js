// src/reducers/pageReducer.js
const initialState = {
    currentPage: 'home' // default page
  };
  
  export default function(state = initialState, action) {
    switch (action.type) {
      case 'SET_CURRENT_PAGE':
        return {
          ...state,
          currentPage: action.payload
        };
      default:
        return state;
    }
  }
  