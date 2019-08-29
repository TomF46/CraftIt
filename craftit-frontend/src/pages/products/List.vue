<template>
  <q-page>
    <h3 class="text-center">Products</h3>
    <product-showcase></product-showcase>
    <q-page-sticky position="bottom-right" :offset="[18, 18]">
      <q-btn fab icon="add" color="primary"  @click="navigate('products/add')" />
    </q-page-sticky>
  </q-page>
</template>

<style>
</style>

<script>
import showcase from '../../components/showcase'

export default {
  name: 'ProductsList',
  components: {
    'product-showcase': showcase
  },
  methods: {
    navigate (path) {
      this.$router.push(path)
    }
  }
}
</script>
