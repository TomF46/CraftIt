<template>
  <q-page>
    <div class="row q-pa-md">
      <div class="col">
        <div v-if="product" class="productView">
          <h3 class="text-center">{{product.name}}</h3>
          <img v-if="product.productImage" class="product-image" :src="'data:image/jpeg;base64,' + product.productImage">
          <q-card class="my-card q-mt-xl">
            <q-card-section>
              <p>{{product.description}}</p>
                <p>Time estimate: {{product.timeEstimate}}</p>
                <p>Requirements:</p>
                <ul>
                  <li v-for="(requirement, index) in product.requirements" :key="index">{{requirement}}</li>
                </ul>
                <p>Instructions:</p>
                <ol>
                  <li v-for="(step, index) in product.instructions" :key="index">{{step.description}} <br> <img v-if="step.image" class="product-image" :src="'data:image/jpeg;base64,' + step.image"></li>
                </ol>
            </q-card-section>
          </q-card>
      </div>
    </div>
    </div>
    <q-page-sticky position="bottom-right" :offset="[18, 18]">
      <q-btn fab icon="edit" color="primary"  @click="navigate('/products/' + id + '/edit')" />
    </q-page-sticky>
  </q-page>
</template>

<style>
.productView{
  width: 100%;
  max-width: 800px;
  margin: 0 auto;
}
.product-image{
  max-width: 100%;
  max-height: 40vh;
  display: block;
  margin: auto;
}
</style>

<script>
export default {
  name: "ProductsView",
  props: ["id"],
  data() {
    return {
      product: null
    };
  },
  mounted() {
    this.getProduct();
  },
  methods: {
    getProduct() {
      this.$axios
        .get("products/" + this.id)
        .then(res => {
          this.product = res.data;
          console.log(this.product);
        })
        .catch(err => {
          console.log(err);
        });
    },
    navigate (path) {
      this.$router.push(path)
    }
  }
};
</script>
