<template>
  <q-page>
    <div class="row q-pa-md">
      <div class="col">
        <div class="productView">
          <h3 class="text-center">{{product.name}}</h3>
          <q-card class="my-card">
            <q-card-section>
              <p>{{product.description}}</p>
                <p>Time estimate: {{product.timeEstimate}}</p>
                <p>Requirements:</p>
                <ul>
                  <li v-for="(requirement, index) in product.requirements" :key="index">{{requirement}}</li>
                </ul>
                <p>Instructions:</p>
                <ol>
                  <li v-for="(step, index) in product.instructions" :key="index">{{step.description}}</li>
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
          console.log(this.product)
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
