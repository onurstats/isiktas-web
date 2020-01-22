import { API_URL } from '@/store'

export const state = () => {
  return {
    subjects: []
  }
}

export const mutations = {
  set(state, payload) {
    state.subjects = payload
  }
}

export const actions = {
  async get({ commit, rootState }) {
    await this.$axios.get(API_URL + 'api/subjects').then((res) => {
      if (res.status === 200) {
        commit('set', res.data)
      }
    })
  },
  async add({ rootState, dispatch }, payload) {
    await this.$axios.post(API_URL + 'api/subjects', payload).then((res) => {
      if (res.status === 201) {
        dispatch('get')
      }
    })
  },
  async delete({ rootState, dispatch }, payload) {
    await this.$axios
      .delete(API_URL + 'api/subjects/' + payload.id)
      .then((res) => {
        if (res.status === 200) {
          dispatch('get')
        }
      })
  }
}

export const getters = {
  subjects(state) {
    return state.subjects
  }
}
