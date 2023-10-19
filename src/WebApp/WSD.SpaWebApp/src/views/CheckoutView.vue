<script setup lang="ts">
import {useRoute} from "vue-router";
const route = useRoute();
const type = route.params.payment;
</script>

<template>
  <div>
    <div id="payment-page">
      <div class="container">
        <div class="payment-container">
          <div class="payment" :ref="`${type}`"></div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
#payment-page {
  display: flex;
  flex-direction: column;
  align-items: center;
}

#payment-page .container {
  margin-top: 100px;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  width: 100%;
  max-width: 1110px;
  padding-left: 8px;
  padding-right: 8px;
}

.checkout-component {
  background: #f7f8f9;
  border: 1px solid #e6e9eb;
  border-radius: 12px;
  margin: 8px 0;
}

/* Adyen Components */
.payment {
  width: 100%;
  padding-top: 0px !important;
  padding-left: 20px;
  padding-right: 20px;
}

@media screen and (max-width: 1076px) {
  #payment-page .container {
    display: flex;
    flex-direction: column;
    align-items: center;
  }

  .payment {
    align-self: center;
    max-width: 610px;
  }
}

.payment-container {
  display: flex;
  justify-content: center;
  background: #f7f8f9;
  border: 1px solid #e6e9eb;
  border-radius: 12px;
  padding-top: 18px;
  padding-bottom: 18px;
  width: 100%;
  height: 100%;
}
</style>