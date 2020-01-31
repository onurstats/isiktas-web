<template>
  <v-layout>
    <v-card width="100%">
      <v-toolbar color="primary" dark flat
        ><v-toolbar-title>Öğretmenler</v-toolbar-title>
        <v-spacer></v-spacer>
        <template v-slot:extension>
          <v-btn @click.stop="addClick" fab color="white" bottom left absolute>
            <v-icon class="black--text">mdi-plus</v-icon>
          </v-btn>
        </template>
      </v-toolbar>
      <v-card-text>
        <v-layout row justify-center align-center>
          <v-col cols="7">
            <v-progress-linear
              :active="isLoading"
              indeterminate
              color="yellow darken-2"
            ></v-progress-linear>
            <v-list single-line>
              <template v-for="(t, i) of teachers">
                <v-list-item :key="t.id" row>
                  <v-list-item-content class="nameField"
                    >{{ t.fullname }}
                    <v-chip-group class="mx-auto">
                      <v-chip
                        v-for="s of t.subjects"
                        :key="s"
                        small
                        outlined
                        color="primary"
                      >
                        <span v-if="subjectList.find((x) => x.id === s)">
                          {{ subjectList.find((x) => x.id === s).name }}
                        </span>
                      </v-chip>
                    </v-chip-group>
                  </v-list-item-content>
                  <v-list-item-action>
                    <v-btn icon>
                      <v-icon @click.stop="edit(t)" color="grey lighten-1">
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
    <v-dialog v-model="dialog" max-width="400px" eager>
      <v-card class="mx-auto my-auto">
        <v-toolbar color="secondary" dark flat>
          <v-toolbar-title>
            Öğretmen
            <span v-if="!editmode">Ekle</span>
            <span v-if="editmode">Düzenle - {{ teacher.fullName }}</span>
          </v-toolbar-title>
          <v-spacer />
          <v-btn @click="dialog = false" icon>
            <v-icon>mdi-close</v-icon>
          </v-btn>
        </v-toolbar>
        <v-card-text>
          <v-form ref="form" v-model="form" class="my-12">
            <v-text-field
              v-model="teacher.name"
              :rules="[rules.min(3)]"
              type="text"
              placeholder="İsim Giriniz"
              label="İsim"
            />
            <v-text-field
              v-model="teacher.lastName"
              :rules="[rules.min(3)]"
              type="text"
              placeholder="Soyisim Giriniz"
              label="Soyisim"
            />
            <v-text-field
              v-model="teacher.tck"
              :rules="[rules.tck]"
              type="text"
              placeholder="Tc Kimlik No Giriniz"
              label="Tck"
            />
            <v-text-field
              v-model="teacher.tel"
              v-mask="telmask"
              :rules="[rules.tel, rules.required]"
              :placeholder="telmask"
              type="tel"
              label="Tel"
            />
            <v-autocomplete
              v-model="teacher.subjects"
              :items="subjectList"
              chips
              color="black lighten-2"
              label="Branş"
              item-text="name"
              item-value="id"
              multiple
            >
            </v-autocomplete>
          </v-form>
        </v-card-text>
        <v-card-actions>
          <v-btn v-if="!editmode" @click.stop="add" :disabled="!form">
            Ekle
          </v-btn>
          <v-btn v-if="editmode" @click.stop="update" :disabled="!form">
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
import { mask } from 'vue-the-mask'

export default {
  directives: {
    mask
  },
  data() {
    return {
      dialog: false,
      form: false,
      teacher: {},
      deleteprompt: false,
      telmask: '0(###) ###-##-##',
      rules: {
        min: (len) => (v) =>
          (v || '').length >= len || `En az ${len} haneli olmalıdır`,
        required: (v) => !!v || 'Zorunlu Alan',
        onlynumber: (v) =>
          /(\d{11})/.test(v) || 'Sadece Rakamlardan Oluşmalıdır 11 Haneli',
        tel: (v) =>
          /0\(\d{3}\) \d{3}-\d{2}-\d{2}/.test(v) || 'Telefon Numarası Giriniz',
        tck: (v) =>
          /(^\d{11})$/.test(v) || 'Tck 11 Haneli Rakamlardan Oluşmalıdır'
      }
    }
  },
  head() {
    return {
      title: 'Öğretmenler',
      meta: [
        // hid is used as unique identifier. Do not use `vmid` for it as it will not work
        {
          hid: 'teachers',
          name: 'teachers',
          content: 'Page About Teachers'
        }
      ]
    }
  },
  computed: {
    subjectList() {
      const ret = []
      for (const key in this.subjects) {
        ret.push({ name: this.subjects[key].name, id: this.subjects[key].id })
      }
      // Object.values(subjects.list).sort((a, b) => (a.name > b.name ? 1 : -1))
      return ret.sort((a, b) => (a.name > b.name ? 1 : -1))
    },
    ...mapGetters(['isLoading', 'editmode']),
    ...mapGetters('subjects', ['subjects']),
    ...mapGetters('teachers', ['teachers', 'active'])
  },
  created() {
    this.$store.dispatch('teachers/get')
    this.$store.dispatch('subjects/get')
  },
  methods: {
    add() {
      this.$store
        .dispatch('teachers/add', this.teacher)
        .then(() => (this.dialog = false))
    },
    addClick() {
      this.$store.commit('teachers/unsetActive')
      this.teacher = { ...this.active }
      this.dialog = true
      this.$nextTick(this.$refs.form.reset())
      this.$store.commit('setEditMode', false)
    },
    edit(teacher) {
      this.$store.dispatch('teachers/getDetail', teacher.id).then(() => {
        this.teacher = { ...this.active }
        this.dialog = true
        this.$store.commit('setEditMode', true)
      })
    },

    update() {
      this.$store
        .dispatch('teachers/update', this.teacher)
        .then(() => {
          this.dialog = false
        })
        .catch((e) => console.log(e))
    },
    deleteSubject() {
      this.$store
        .dispatch('confirmer/ask', {
          title: 'Emin Misiniz?',
          body: 'Öğretmen Silinecektir Emin Misiniz?'
        })
        .then((confirmation) => {
          if (confirmation) {
            this.$store.dispatch('teachers/delete').then((this.dialog = false))
          } else {
          }
        })
    }
  }
}
</script>
<style scoped>
.nameField {
  text-transform: capitalize;
}
</style>
