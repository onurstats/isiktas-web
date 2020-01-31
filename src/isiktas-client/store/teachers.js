import { API_URL } from '@/store'

const initialState = {
  id: null,
  fullName: '',
  lastName: null,
  name: '',
  tck: null,
  tel: null,
  subjectTeachers: null
}

export const state = () => {
  return {
    teachers: [],
    active: {}
  }
}

export const mutations = {
  set(state, payload) {
    state.teachers = payload
  },
  setActive(state, payload) {
    state.active = payload
  },
  unsetActive(state) {
    state.active = { ...initialState }
  }
}

export const actions = {
  async get({ commit }) {
    commit('setLoading', true, { root: true })
    await this.$axios.get(API_URL + 'api/teachers').then((res) => {
      if (res.status === 200) {
        commit('set', res.data)
        commit('setLoading', false, { root: true })
      }
    })
  },
  async add({ dispatch }, teacher) {
    delete teacher.id
    const temp = teacher.subjects
    const tt = []
    for (const t of temp) {
      tt.push({ SubjectId: t })
    }
    teacher.subjectTeachers = tt
    await this.$axios.post(API_URL + 'api/teachers', teacher).then((res) => {
      if (res.status === 201) {
        dispatch('get')
      }
    })
  },
  async update({ dispatch }, teacher) {
    const temp = teacher.subjects
    const tt = []
    for (const t of temp) {
      tt.push({ SubjectId: t, TeacherId: teacher.id })
    }
    teacher.subjectTeachers = tt
    await this.$axios
      .put(API_URL + 'api/teachers/' + teacher.id, teacher)
      .then((res) => {
        dispatch('get')
      })
  },

  async getDetail({ commit }, uid) {
    await this.$axios.get(API_URL + 'api/teachers/' + uid).then((res) => {
      if (res.status === 200) {
        commit('setActive', res.data)
      }
    })
  },

  async delete({ state, dispatch }) {
    await this.$axios
      .delete(API_URL + 'api/teachers/' + state.active.id)
      .then((res) => {
        if (res.status === 200) {
          dispatch('get')
        }
      })
  }
}

export const getters = {
  teachers(state) {
    return state.teachers
  },
  active(state) {
    return state.active
  }
}
