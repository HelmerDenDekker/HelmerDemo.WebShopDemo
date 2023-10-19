<script setup lang="ts">
import {useRoute} from "vue-router";
const route = useRoute();
const type = route.params.type;
</script>

<template>
  <main class="preview-page">
    <section class="cart">
      <h2>Cart</h2>
      <div class="order-summary">
        <ul class="order-summary-list">
          <li class="order-summary-list-list-item">
            <img
                src="~/assets/images/sunglasses.png"
                class="order-summary-list-list-item-image"
                alt
            />
            <p class="order-summary-list-list-item-title">Sunglasses</p>
            <p class="order-summary-list-list-item-price">50.00</p>
          </li>
          <li class="order-summary-list-list-item">
            <img
                src="~/assets/images/headphones.png"
                class="order-summary-list-list-item-image"
                alt
            />
            <p class="order-summary-list-list-item-title">Headphones</p>
            <p class="order-summary-list-list-item-price">50.00</p>
          </li>
        </ul>
      </div>
      <div class="cart-footer">
        <span class="cart-footer-label">Total:</span>
        <span class="cart-footer-amount">100.00</span>
        <RouterLink :to="`/checkout/${type}`">
          <p class="button">Continue to checkout</p>
        </RouterLink>
      </div>
    </section>
  </main>
</template>

<style scoped>
.shopping-cart {
  float: right;
}
@media (min-width: 768px) {
  .shopping-cart {
    padding-left: 0;
    padding-bottom: 0;
    padding-right: 0;
    padding-top: 3px;
  }
}
.shopping-cart-link {
  display: inline-block;
  position: relative;
}
.order-summary-list {
  border-top: 1px solid #e6e9eb;
}
.order-summary-list-list-item {
  border-bottom: 1px solid #e6e9eb;
  display: flex;
  height: 97px;
}
.order-summary-list-list-item-image {
  height: 64px;
  margin: 16px;
  width: 64px;
}
.order-summary-list-list-item-title {
  font-weight: 700;
  margin: auto auto auto 0;
}
.order-summary-list-list-item-price {
  color: #687282;
  margin: auto 16px;
  text-align: right;
  width: 80px;
}
@media (min-width: 768px) {
  .order-summary-list-list-item-price {
    margin-left: 24px;
    margin-bottom: auto;
    margin-right: 24px;
    margin-top: auto;
  }
}
.order-summary-list-list-item-remove-product {
  background: none;
  border: 0;
  cursor: pointer;
  height: 25px;
  margin: auto 0;
  padding: 0;
  width: 25px;
}
.cart {
  text-align: center;
  margin-top: 80px;
}
.cart-footer {
  font-weight: 700;
  margin-top: 17px;
  text-align: right;
}
@media (min-width: 768px) {
  .cart-footer {
    margin-top: 24px;
  }
}
.cart-footer .button {
  margin-top: 30px;
  width: 100%;
}
@media (min-width: 768px) {
  .cart-footer .button {
    margin-top: 0;
    width: 288px;
  }
}
.cart-footer-amount {
  margin-left: 16px;
  margin-right: 24px;
}
.whole-preview {
  margin: auto;
  max-width: 1110px;
  padding: 0 16px;
}
@media (min-width: 1440px) {
  .whole-preview {
    padding-left: 0;
    padding-bottom: 0;
    padding-right: 0;
    padding-top: 0;
  }
}
</style>