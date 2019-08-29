<template>
  <q-page class="flex flex-center">
    <div>
      <h1 class="fadeInOut">Craft It</h1><br>
      <div class="text-center">
        <q-btn class="q-mt-sm" color="primary" label="View products" @click="navigate('/products')" />
      </div>
    </div>
  </q-page>
</template>

<style>
</style>

<script>
export default {
  name: 'PageIndex',
  mounted () {
    this.$axios.get('products').then(res => {
      console.log(res.data)
    }).catch(err => {
      console.log(err)
    })
  },
  methods:{
    navigate (path) {
      this.$router.push(path)
    }
  }
}
</script>
