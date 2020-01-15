<template>
  <v-content>
    <v-container class="fill-height" fluid>
      <v-row align="center" justify="center">
        <v-col cols="12" sm="8" md="4">
          <v-btn @click.stop="authenticateOidc" block color="secondary">
            Login</v-btn
          >
        </v-col>
      </v-row>
    </v-container>
  </v-content>
</template>
<script>
import { mapActions } from 'vuex'
export default {
  layout: 'login',
  methods: {
    ...mapActions('oidc', ['authenticateOidc'])
  }
}
</script>
