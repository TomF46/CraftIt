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
        instructions: [],
        productImage: ""
      }
    };
  },
  methods:{
    async submit(){
      if(this.product.productImage != null) this.product.productImage = await this.convertImageToBase64(this.product.productImage[0]);
      for ( const instruction of this.product.instructions){
        if(instruction.image != null) instruction.image = await this.convertImageToBase64(instruction.image[0]);
      };
      this.postToServer();
    },
    convertImageToBase64(file){
      return new Promise((resolve, reject) => {
        var reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => {
          resolve(reader.result);
        };
        reader.onerror = reject;
      })
      
    },
    postToServer(){
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
