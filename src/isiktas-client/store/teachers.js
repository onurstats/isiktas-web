import { API_URL } from '@/store'

export const state = () => {
  return {
    teachers: []
  }
}

export const mutations = {
  set(state, payload) {
    state.teachers = payload
  }
}

export const actions = {
  async get({ commit, rootState }) {
    const config = {
      headers: { Authorization: 'bearer ' + rootState.oidc.access_token }
    }
    await this.$axios.get(API_URL + 'api/teachers', config).then((res) => {
      if (res.status === 200) {
        commit('set', res.data)
      }
    })
  }
}
