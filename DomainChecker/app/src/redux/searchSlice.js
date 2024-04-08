
import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import { checkDomainAvailability } from '../services/domainService'; 

const initialState = {
  results: null, 
  isLoading: false,
  error: '',
};

export const performSearch = createAsyncThunk(
  'search/performSearch',
  async (domainName, { rejectWithValue }) => {
    try {
      const data = await checkDomainAvailability(domainName);
      console.log("Search results:", data);
      return data;
    } catch (error) {
      console.error("Search failed:", error);
      return rejectWithValue(error.response?.data?.message || "Unexpected error occurred");
    }
  }
);
export const searchSlice = createSlice({
  name: 'search',
  initialState,
  reducers: {
    // Adding resetSearchState action
    resetSearchState: (state) => {
      state.results = [];
      state.isLoading = false;
      state.error = '';
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(performSearch.pending, (state) => {
        state.isLoading = true;
      })
      .addCase(performSearch.fulfilled, (state, action) => {
        state.isLoading = false;
        state.results = action.payload;
        state.error = '';
      })
      .addCase(performSearch.rejected, (state, action) => {
        state.isLoading = false;
        state.error = action.payload;
      });
  },
});

// Exporting resetSearchState action
export const { resetSearchState } = searchSlice.actions;

export default searchSlice.reducer;
