<template>
  <q-page>
    <h3 class="text-center">Add</h3>
    <div class="row q-pa-md">
      <div class="col">
        <product-form :product="product" @submitted="submit"></product-form>
      </div>
    </div>
  </q-page>
</template>

<style>
</style>

<script>
import ProductForm from '../../components/productForm'
export default {
  name: 'ProductsAdd',
  components: {
    'product-form': ProductForm
  },
  data() {
    return {
      product: {
        name : "",
        description: "",
        timeEstimate: "",
        requirements: [],
        instructions: []
      }
    };
  },
  methods:{
    submit(){
      this.$axios.post("products", this.product).then(res => {
        alert("Success");
        this.$router.push('/products')
      }).catch(err => {
        console.log(err);
      })
    }
  }
}
</script>
