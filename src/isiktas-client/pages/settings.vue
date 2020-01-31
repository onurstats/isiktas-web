<template>
  <v-layout>
    <v-card width="100%">
      <v-toolbar color="primary" dark flat
        ><v-toolbar-title>Ayarlar</v-toolbar-title>
        <v-spacer></v-spacer>
      </v-toolbar>
      <v-card-text>
        <v-row>
          <v-col cols="1" offset="1"></v-col>
          <v-col v-for="(gun, i) in gunler" :key="i" cols="2" offset="1">
            <h2>{{ gun }}</h2>
          </v-col>
        </v-row>
        <v-row v-for="(gun, k) in dersSaatleri.dersSayisi" :key="k">
          <v-col cols="1" offset="1">{{ k + 1 }}.Ders</v-col>
          <v-col
            v-for="(saat, i) in dersSaatleri.gunler"
            :key="i"
            cols="2"
            offset="1"
          >
            <v-row>
              <v-text-field
                v-model="saat.baslangic[k]"
                @change="kButton = false"
                dense
                label="Başlangıç"
                type="time"
              />
              <v-text-field
                v-model="saat.bitis[k]"
                @change="kButton = false"
                dense
                label="Bitiş"
                type="time"
              />
            </v-row>
          </v-col>
        </v-row>
        <v-row>
          <v-col cols="1" offset="1">
            <v-btn @click.stop="addRow" outlined class>Ekle</v-btn>
          </v-col>
          <v-col cols="1">
            <v-btn
              v-if="dersSaatleri.dersSayisi > 1"
              @click.stop="removeRow"
              outlined
              class
              >Sil</v-btn
            >
          </v-col>
          <v-col cols="1" offset="4">
            <v-btn
              :disabled="kButton"
              @click.stop="save"
              outlined
              class
              color="primary"
              >Kaydet</v-btn
            >
          </v-col>
        </v-row>
      </v-card-text>
    </v-card>
  </v-layout>
</template>
<script>
export default {
  data: () => ({
    gunler: ['Hafta İçi', 'Hafta Sonu'],
    dersSaatleri: {
      dersSayisi: 1,
      gunler: [
        { baslangic: ['08:00'], bitis: ['08:40'] },
        { baslangic: ['08:00'], bitis: ['08:40'] }
      ]
    },
    kButton: true
  }),
  methods: {
    addTime(time, minute) {
      let h = parseInt(time.split(':')[0])
      let m = parseInt(time.split(':')[1])
      m = m + minute
      if (m >= 60) {
        m = m - 60
        h = h + 1
      }
      time = String(h).padStart(2, '0') + ':' + String(m).padStart(2, '0')

      return time
    },
    addRow() {
      this.kButton = false
      this.dersSaatleri.gunler.forEach((ders) => {
        ders.baslangic.push(
          this.addTime(ders.baslangic[ders.baslangic.length - 1], 50)
        )
        ders.bitis.push(this.addTime(ders.bitis[ders.bitis.length - 1], 50))
      })
      this.dersSaatleri.dersSayisi = this.dersSaatleri.dersSayisi + 1
    },
    removeRow() {
      this.kButton = false
      this.dersSaatleri.gunler.forEach((ders) => {
        ders.baslangic.pop()
        ders.bitis.pop()
      })
      this.dersSaatleri.dersSayisi = this.dersSaatleri.dersSayisi - 1
    }
  }
}
</script>
