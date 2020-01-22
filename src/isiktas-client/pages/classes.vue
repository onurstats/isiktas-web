<template>
  <v-layout>
    <v-card width="100%">
      <v-toolbar color="primary" dark flat
        ><v-toolbar-title>Sınıflar</v-toolbar-title>
        <v-spacer></v-spacer>
        <template v-slot:extension>
          <v-btn
            @click.stop="dialog = true"
            fab
            color="white"
            bottom
            left
            absolute
          >
            <v-icon class="black--text">mdi-plus</v-icon>
          </v-btn>
        </template>
      </v-toolbar>
      <v-card-text>
        <v-layout row justify-center align-center>
          <v-col cols="5">
            <v-list single-line>
              <template v-for="(s, i) of classes">
                <v-list-item :key="s.name" row>
                  <v-list-item-avatar color="primary">
                    {{ i + 1 }}
                  </v-list-item-avatar>
                  <v-list-item-content>{{ s.name }}</v-list-item-content>
                  <v-list-item-action>
                    <v-btn icon>
                      <v-icon @click.stop="edit(s)" color="grey lighten-1">
                        mdi-square-edit-outline
                      </v-icon>
                    </v-btn>
                  </v-list-item-action>
                </v-list-item>
                <v-divider :key="i"></v-divider>
              </template>
            </v-list>
          </v-col>
        </v-layout>
      </v-card-text>
    </v-card>
    <v-dialog v-model="dialog" max-width="290px">
      <v-card class="mx-auto my-auto">
        <v-toolbar color="secondary" dark flat>
          <v-toolbar-title>
            Sınıf
            <span v-if="!editmode">Ekle</span>
            <span v-if="editmode">{{ oclass.name }}</span>
          </v-toolbar-title>
          <v-spacer />
          <v-btn @click="dialog = false" icon>
            <v-icon>mdi-close</v-icon>
          </v-btn>
        </v-toolbar>
        <v-card-text>
          <v-form ref="form" v-model="form" class="my-12">
            <v-text-field
              v-model="oclass.name"
              :rules="[rules.min(3)]"
              type="text"
              placeholder="Ders Adı Giriniz"
              label="Ders Adı"
            />
          </v-form>
        </v-card-text>
        <v-card-actions>
          <v-btn @click.stop="save" :disabled="!form">
            Kaydet
          </v-btn>
          <v-btn
            v-if="editmode"
            @click.native.prevent="deleteSubject"
            color="error"
          >
            Sil
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-layout>
</template>

<script>
import { mapGetters } from 'vuex'

export default {
  data() {
    return {
      dialog: false,
      oclass: { name: '' },
      form: false,
      editmode: false,
      deleteprompt: false,
      rules: {
        min: (len) => (v) =>
          (v || '').length >= len || `En az ${len} haneli olmalıdır`,
        required: (v) => !!v || 'Zorunlu Alan',
        onlynumber: (v) =>
          /(\d{11})/.test(v) || 'Sadece Rakamlardan Oluşmalıdır 11 Haneli'
      }
    }
  },
  head() {
    return {
      title: 'Sınıflar',
      meta: [
        // hid is used as unique identifier. Do not use `vmid` for it as it will not work
        {
          hid: 'classes',
          name: 'classes',
          content: 'Page About Classes'
        }
      ]
    }
  },
  computed: {
    ...mapGetters('classes', ['classes'])
  },
  created() {
    this.$store.dispatch('classes/get')
  },
  methods: {
    save() {
      this.$store.dispatch('classes/add', this.oclass)
      this.dialog = false
    },
    edit(s) {
      this.oclass = s
      this.editmode = true
      this.dialog = true
    },
    deleteSubject() {
      this.$store
        .dispatch('confirmer/ask', {
          title: 'Really Destroy world?',
          body: "This would suck, dude! Don't be a dick!"
        })
        .then((confirmation) => {
          if (confirmation) {
            this.$store.dispatch('classes/delete', this.oclass)
            this.dialog = false
          } else {
            alert('cancel')
          }
        })
    }
  }
}
</script>
