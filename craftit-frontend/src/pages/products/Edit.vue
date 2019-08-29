<template>
  <q-page>
    <h3 class="text-center">Edit</h3>
    <div v-if="product != null" class="row q-pa-md">
      <div class="col">
        <product-form :product="product" @submitted="submit"></product-form>
        <div class="text-right">
          <q-btn class="q-mt-lg q-ml-md" color="red" label="Delete" @click="deleteConfirm = true" />
        </div>
      </div>
    </div>


    <q-dialog v-model="deleteConfirm" persistent>
      <q-card>
        <q-card-section class="row items-center">
          <q-avatar icon="delete_forever" color="primary" text-color="white" />
          <span class="q-ml-sm">Are you sure you want to delete this?</span>
        </q-card-section>

        <q-card-actions align="right">
          <q-btn flat label="Cancel" color="primary" v-close-popup />
          <q-btn flat label="Confirm" color="red" @click="deleteProduct()" v-close-popup />
        </q-card-actions>
      </q-card>
    </q-dialog>

  </q-page>
</template>

<style>
</style>

<script>
import ProductForm from '../../components/productForm'
export default {
  name: 'ProductsEdit',
  props: ['id'],
  components: {
    'product-form': ProductForm
  },
  data() {
    return {
      product: null,
      deleteConfirm: false
    };
  },
  mounted(){
    this.getProduct()
  },
  methods:{
    getProduct(){
      this.$axios.get("products/" + this.id).then(res => {
          this.product = res.data;
        }).catch(err => {
          console.log(err);
        });
    },
    submit(){
      this.$axios.put("products?id=" + this.id, this.product).then(res => {
        alert("Success");
        this.$router.push('/products/' + this.id)
      }).catch(err => {
        console.log(err);
      })
    },
    deleteProduct(){
        this.$axios.delete("products/?id=" + this.id).then(res => {
          this.$router.push('/products');
        }).catch(err => {
          console.log(err);
        })
    }
  }
}
</script>
