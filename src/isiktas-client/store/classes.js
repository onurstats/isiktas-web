import { API_URL } from '@/store'

export const state = () => {
  return {
    classes: []
  }
}

export const mutations = {
  set(state, payload) {
    state.classes = payload
  }
}

export const actions = {
  async get({ commit, rootState }) {
    await this.$axios.get(API_URL + 'api/classes').then((res) => {
      if (res.status === 200) {
        commit('set', res.data)
      }
    })
  },
  async add({ rootState, dispatch }, payload) {
    await this.$axios.post(API_URL + 'api/classes', payload).then((res) => {
      if (res.status === 201) {
        dispatch('get')
      }
    })
  },
  async delete({ rootState, dispatch }, payload) {
    await this.$axios
      .delete(API_URL + 'api/classes/' + payload.id)
      .then((res) => {
        if (res.status === 200) {
          dispatch('get')
        }
      })
  }
}

export const getters = {
  classes(state) {
    return state.classes
  }
}
