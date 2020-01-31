export const oidcSettings = {
  authority: 'http://localhost:5000',
  clientId: 'isiktas-vue-spa',
  redirectUri: 'http://localhost:3000/oidc-callback',
  responseType: 'id_token token',
  scope: 'openid profile isiktas-api email',
  automaticSilentRenew: true,
  silentRedirectUri: 'http://localhost:3000/silent-renew-oidc.html'
}
