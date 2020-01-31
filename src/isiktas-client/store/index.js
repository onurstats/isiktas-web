export const API_URL = 'https://localhost:44352/'

export const state = () => {
  return {
    loading: false,
    editmode: false
  }
}

export const mutations = {
  setLoading(state, payload) {
    state.loading = payload
  },
  setEditMode(state, payload) {
    state.editmode = payload
  }
}

export const getters = {
  isLoading(state) {
    return state.loading
  },
  editmode(state) {
    return state.editmode
  }
}
