<template>
    <div class="q-pa-md">
        <div class="row items-stretch">
            <div v-for="(item, index) in items" :key="index" class="col-sm-4 col-xs-12">
                <q-card class="my-card q-ma-xl">
                    <q-card-section>
                        <div class="text-h6">{{item.name}}</div>
                        <div class="text-subtitle2">{{item.timeEstimate}}</div>
                    </q-card-section>

                    <q-card-section>
                        {{item.description}}
                    </q-card-section>

                    <q-separator light />

                    <q-card-actions align="right">
                        <q-btn flat @click="navigate('products/' + item.id)">View</q-btn>
                    </q-card-actions>
                </q-card>
            </div>
        </div>
    </div>
</template>

<style>

</style>

<script>
export default {
  name: 'ProductsShowcase',
  data() {
    return {
      items: []
    }
  },
  mounted() {
      this.getShowcaseItems()
  },
  methods: {
    navigate(path) {
      this.$router.push(path)
    },
    getShowcaseItems() {
        this.$axios.get('products').then(res => {
            this.items = res.data
        }).catch(err => {
            console.log(err)
        })
    }
  }
}
</script>
