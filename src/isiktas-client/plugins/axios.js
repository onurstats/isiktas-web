export default ({ store, $axios }) => {
  $axios.onRequest((config) => {
    if (store.state.oidc.access_token) {
      config.headers.common.Authorization =
        'Bearer ' + store.state.oidc.access_token
    }
  })
}
