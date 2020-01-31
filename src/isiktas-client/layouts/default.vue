<template>
  <v-app dark>
    <v-navigation-drawer v-model="drawer" clipped fixed app>
      <v-list nav>
        <v-list-item
          v-for="(item, i) in items"
          :key="i"
          :to="item.path"
          router
          exact
        >
          <v-list-item-action>
            <v-icon>{{ item.icon }}</v-icon>
          </v-list-item-action>
          <v-list-item-content>
            <v-list-item-title v-text="item.title" />
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-navigation-drawer>
    <v-app-bar clipped-left fixed app>
      <v-app-bar-nav-icon @click.stop="drawer = !drawer" />
      <v-toolbar-title v-text="title" />
      <v-spacer />
      <v-btn v-if="oidcUser" text>{{ oidcUser.name }}</v-btn>
      <v-btn v-if="oidcIsAuthenticated" @click.stop="signOut">Çıkış</v-btn>
    </v-app-bar>
    <v-content>
      <v-container fluid>
        <v-row align="center" justify="center">
          <v-col cols="10">
            <nuxt />
          </v-col>
        </v-row>
      </v-container>
      <ConfirmDialog />
    </v-content>
    <v-footer :fixed="fixed" app>
      <span>&copy; 2020</span>
    </v-footer>
  </v-app>
</template>

<script>
import { mapGetters, mapActions } from 'vuex'
import ConfirmDialog from '@/components/ConfirmDialog.vue'

export default {
  components: {
    ConfirmDialog
  },
  data() {
    return {
      drawer: false,
      fixed: false,
      items: [
        { title: 'Etüt Programı', icon: 'mdi-view-dashboard', path: '/etud' },
        { title: 'Ders Programı', icon: 'mdi-calendar', path: '/calendar' },
        { title: 'Öğretmenler', icon: 'mdi-teach', path: '/teachers' },
        { title: 'Ögrenciler', icon: 'mdi-account-group', path: '/students' },
        { title: 'Dersler', icon: 'mdi-flask', path: '/subjects' },
        { title: 'Sınıflar', icon: 'mdi-chair-school', path: '/classes' },
        { title: 'Ayarlar', icon: 'mdi-settings', path: '/settings' }
      ],
      title: 'Işıktaş Eğitim'
    }
  },
  computed: {
    ...mapGetters('oidc', ['oidcIsAuthenticated', 'oidcUser']),
    hasAccess() {
      return this.oidcIsAuthenticated || this.oidcIsRoutePublic(this.$route)
    }
  },
  mounted() {
    window.addEventListener('vuexoidc:userLoaded', this.userLoaded)
    if (!this.oidcIsAuthenticated) {
      this.$router.push('/login')
    }
  },
  destroyed() {
    window.removeEventListener('vuexoidc:userLoaded', this.userLoaded)
  },

  methods: {
    ...mapActions('oidc', ['oidcIsRoutePublic', 'removeOidcUser']),
    userLoaded(e) {
      console.log(
        'I am listening to the user loaded event in vuex-oidc',
        e.detail
      )
    },
    signOut() {
      this.removeOidcUser().then(() => {
        this.$router.push('/login')
      })
    }
  }
}
</script>
<style scoped>
.v-list-item--active {
  background-color: #ff8f00;
}
</style>
<style>
.v-input input {
  text-transform: capitalize;
}
</style>
