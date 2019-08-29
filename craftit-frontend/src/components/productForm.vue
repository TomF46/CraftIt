<template>
    <div class="q-pa-md">
        <div class="row">
            <q-form class="product-form">
                <q-card class="my-card">
                    <q-card-section>
                        <q-input class="q-mb-md" v-model="product.name" label="Name" />
                        <label>Description</label>
                        <q-input class="q-mb-md" v-model="product.description" filled type="textarea" />
                        <q-input v-model="product.productImage" type="file"/>
                        <q-input class="q-mb-md" v-model="product.timeEstimate" label="Estimated time to completion" />
                        <div class="q-mb-md" id="requirement-input">
                            <label>Requirements</label>
                            <div v-if="product.requirements.length > 0">
                                <div class="row q-mb-xs" v-for="(requirement, index) in product.requirements" :key="index">
                                    <div class="col-10">
                                        <q-input v-model="product.requirements[index]" label="Requirement name" />
                                    </div>
                                    <div class="col-2 text-center">
                                        <q-btn round color="red" icon="delete" @click="removeRequirement(index)" />
                                    </div>
                                </div>
                            </div>
                            <p v-else>No requirements, press the button below to add one!</p>
                            <div class="text-center">
                                <q-btn class="q-mt-md text-center" color="primary" label="Add requirement" @click="addRequirement()" />
                            </div>
                        </div>
                        <div class="q-mb-md" id="instructions-input">
                            <label>Instructions</label>
                            <div v-if="product.instructions.length > 0" >
                                <div class="row q-mb-xs" v-for="(instruction, index) in product.instructions" :key="index">
                                    <div class="col-8">
                                        <q-input v-model="product.instructions[index].description" label="Instruction" />
                                    </div>
                                    <div class="col-2">
                                        <q-input v-model="product.instructions[index].image" type="file"/>
                                    </div>
                                    <div class="col-2 text-center">
                                        <q-btn round color="red" icon="delete" @click="removeInstruction(index)" />
                                    </div>
                                </div>
                            </div>
                            <p v-else>No instructions, press the button below to add one!</p>
                            <div class="text-center">
                                <q-btn class="q-mt-md" color="primary" label="Add Instruction" @click="addInstruction()" />
                            </div>
                        </div>
                        <div class="text-center">
                            <q-btn class="q-mt-lg" color="primary" label="Submit" @click="submit()" />
                        </div>
                  </q-card-section>
                </q-card>
            </q-form>
        </div>
    </div>
</template>

<style>
.product-form{
    width: 100%;
    max-width: 800px;
    margin: 0 auto;
}
</style>

<script>
export default {
  name: 'ProductsShowcase',
  props:{
    product : Object
  },
  methods: {
      addRequirement(){
          this.product.requirements.push("");
      },
      removeRequirement(index){
          this.product.requirements.splice(index, 1);
      },
      addInstruction(){
          this.product.instructions.push({description: "", image : null});
      },
      removeInstruction(index){
          this.product.instructions.splice(index,1);
      },
      submit(){
          this.$emit('submitted');
      }
  }
}
</script>
