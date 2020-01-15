const initialState = {
  active: false,
  title: '',
  body: '',
  resolve: null,
  reject: null
}

export const state = Object.assign({}, initialState)

export const mutations = {
  ACTIVATE: (state, payload) => {
    Object.assign(state, payload)
  },
  DEACTIVATE: (state) => {
    state = Object.assign(state, initialState)
  }
}

export const actions = {
  ask: ({ commit }, { title, body }) => {
    return new Promise((resolve, reject) => {
      commit('ACTIVATE', {
        active: true,
        title,
        body,
        resolve,
        reject
      })
    })
  }
}
