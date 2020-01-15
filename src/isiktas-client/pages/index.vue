<template>
  <v-layout column justify-center align-center>
    <v-flex xs12 sm8 md6>
      <div class="text-center">
        <logo />
        <vuetify-logo />
      </div>
      <v-card>
        <v-card-title class="headline">
          Welcome to the Vuetify + Nuxt.js template
        </v-card-title>
        <v-card-text>
          <SignedInUser />
        </v-card-text>
        <v-card-actions>
          <v-spacer />
          <nuxt-link to="/login">Home page</nuxt-link>
          <v-btn @click.stop="sendRequest" color="primary">Continue</v-btn>
        </v-card-actions>
      </v-card>
    </v-flex>
  </v-layout>
</template>

<script>
import Logo from '~/components/Logo.vue'
import VuetifyLogo from '~/components/VuetifyLogo.vue'
import SignedInUser from '@/components/SignedInUser.vue'

export default {
  components: {
    Logo,
    VuetifyLogo,
    SignedInUser
  },
  methods: {
    sendRequest() {
      this.$store.dispatch('TestAction')
    }
  }
}
</script>
