const API_URL = 'https://localhost:44352/'

export const state = () => {
  return {
    auth: null,
    subtitle: 'Home',
    subjects: null
  }
}

export const mutations = {
  setAuth(state, auth) {
    state.auth = auth
  },
  setSubtitle(state, title) {
    state.subtitle = title
  },
  setSubjects(state, payload) {
    state.subjects = payload
  }
}

export const actions = {
  async TestAction(state) {
    const config = {
      headers: { Authorization: 'bearer ' + state.state.oidc.access_token }
    }
    await this.$axios.get(API_URL + 'identity', config).then((res) => {
      if (res.status === 200) {
        console.log('set', res.data)
      }
    })
  },
  async getSubjects({ commit, state }) {
    const config = {
      headers: { Authorization: 'bearer ' + state.oidc.access_token }
    }
    await this.$axios.get(API_URL + 'api/subjects', config).then((res) => {
      if (res.status === 200) {
        commit('setSubjects', res.data)
      }
    })
  },
  async addSubject({ commit, state, dispatch }, payload) {
    const config = {
      headers: { Authorization: 'bearer ' + state.oidc.access_token }
    }
    await this.$axios
      .post(API_URL + 'api/subjects', { name: payload }, config)
      .then((res) => {
        if (res.status === 201) {
          dispatch('getSubjects')
        }
      })
  }
}

export const getters = {
  subtitle(state) {
    return state.subtitle
  },
  subjects(state) {
    return state.subjects
  }
}
