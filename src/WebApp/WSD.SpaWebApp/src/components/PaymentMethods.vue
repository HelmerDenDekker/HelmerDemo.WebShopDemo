<script setup lang="ts">

</script>

<template>
  <div class="main-container">
    <div class="info">
      <h1>Select a demo</h1>
      <p>
        Click to view an interactive example of a PCI-compliant UI integration
        for online payments.
      </p>
      <p>
        Make sure the payment method you want to use are enabled for your
        account. Refer
        <a
            href="https://docs.adyen.com/payment-methods#add-payment-methods-to-your-account"
        >the documentation</a
        >
        to add missing payment methods.
      </p>
      <p>
        To learn more about client-side integration solutions, check out
        <a href="https://docs.adyen.com/checkout">
          Online payments.
        </a>
      </p>
    </div>
    <ul class="integration-list">
      <li class="integration-list-item">
        <RouterLink to="/preview?type=dropin" class="integration-list-item-link">
          <div class="title-container">
            <p class="integration-list-item-title">Drop-in</p>
          </div>
        </RouterLink>
      </li>
      <li class="integration-list-item">
        <RouterLink to="/preview?type=card" class="integration-list-item-link">
          <div class="title-container">
            <p class="integration-list-item-title">Card</p>
          </div>
        </RouterLink>
      </li>
      <li class="integration-list-item">
        <RouterLink to="/preview?type=ideal" class="integration-list-item-link">
          <div class="title-container">
            <p class="integration-list-item-title">iDEAL</p>
          </div>
        </RouterLink>
      </li>
      <li class="integration-list-item">
        <RouterLink to="/preview?type=dotpay" class="integration-list-item-link">
          <div class="title-container">
            <p class="integration-list-item-title">Dotpay</p>
          </div>
        </RouterLink>
      </li>
      <li class="integration-list-item">
        <RouterLink
            to="/preview?type=giropay"
            class="integration-list-item-link"
        >
          <div class="title-container">
            <p class="integration-list-item-title">giropay</p>
          </div>
        </RouterLink>
      </li>
      <li class="integration-list-item">
        <RouterLink
            to="/preview?type=sepadirectdebit"
            class="integration-list-item-link"
        >
          <div class="title-container">
            <p class="integration-list-item-title">SEPA Direct Debit</p>
          </div>
        </RouterLink>
      </li>
      <li class="integration-list-item">
        <RouterLink
            to="/preview?type=directEbanking"
            class="integration-list-item-link"
        >
          <div class="title-container">
            <p class="integration-list-item-title">SOFORT</p>
          </div>
        </RouterLink>
      </li>
      <li class="integration-list-item">
        <RouterLink to="/preview?type=ach" class="integration-list-item-link">
          <div class="title-container">
            <p class="integration-list-item-title">ACH</p>
          </div>
        </RouterLink>
      </li>
      <li class="integration-list-item">
        <RouterLink to="/preview?type=alipay" class="integration-list-item-link">
          <div class="title-container">
            <p class="integration-list-item-title">Alipay</p>
          </div>
        </RouterLink>
      </li>
      <li class="integration-list-item">
        <RouterLink
            to="/preview?type=klarna_paynow"
            class="integration-list-item-link"
        >
          <div class="title-container">
            <p class="integration-list-item-title">Klarna - Pay now</p>
          </div>
        </RouterLink>
      </li>
      <li class="integration-list-item">
        <RouterLink to="/preview?type=klarna" class="integration-list-item-link">
          <div class="title-container">
            <p class="integration-list-item-title">Klarna - Pay later</p>
          </div>
        </RouterLink>
      </li>
      <li class="integration-list-item">
        <RouterLink
            to="/preview?type=klarna_account"
            class="integration-list-item-link"
        >
          <div class="title-container">
            <p class="integration-list-item-title">Klarna - Slice it</p>
          </div>
        </RouterLink>
      </li>
      <li class="integration-list-item">
        <RouterLink
            to="/preview?type=boletobancario_santander"
            class="integration-list-item-link"
        >
          <div class="title-container">
            <p class="integration-list-item-title">Boleto Bancário</p>
          </div>
        </RouterLink>
      </li>
    </ul>
  </div>
</template>

<style scoped>
.main-container {
  margin: auto;
  max-width: 1048px;
  padding: 0 16px;
  display: flex;
  flex-direction: column;
}

.integration-list {
  display: flex;
  margin-top: 6%;
  max-width: 1048px;
  flex-wrap: wrap;
  justify-content: space-between;
  list-style: none;
  margin: 0;
  padding: 0;
}

@media (min-width: 768px) {
  .integration-list {
    margin-left: -8px;
    margin-bottom: -8px;
    margin-right: -8px;
  }
}

@media (min-width: 1024px) {
  .integration-list {
    margin-left: -16px;
    margin-bottom: -16px;
    margin-right: -16px;
  }
}

.integration-list-item {
  background: #f7f8f9;
  border-radius: 6px;
  flex: 1 1 0;
  margin: 4px;
  min-width: 40%;
  position: relative;
  display: flex;
  justify-content: center;
  align-items: center;
  border: 1px solid #f7f8f9;
}

.integration-list-item:hover {
  box-shadow: 0 16px 24px 0 #e5eaef;
  text-decoration: none;
  border: 1px solid #06f;
}

@media (min-width: 768px) {
  .integration-list-item {
    margin-left: 16px;
    margin-bottom: 16px;
    margin-right: 16px;
    margin-top: 16px;
    min-width: 25%;
  }
}

.integration-list-item-link {
  padding: 20px;
  width: 100%;
  display: flex;
  align-items: center;
  justify-content: center;
}

@media (min-width: 768px) {
  .integration-list-item-link {
    padding-left: 28px;
    padding-bottom: 28px;
    padding-right: 28px;
    padding-top: 28px;
  }
}

.integration-list-item-title {
  margin: 0;
  text-align: center;
  color: #00112c;
  font-size: 1em;
  font-weight: 700;
  margin: 10px 0 0;
}

@media (min-width: 768px) {
  .integration-list-item-title {
    font-size: 24px;
    margin-left: 0;
    margin-bottom: 6px;
    margin-right: 0;
  }
}

.integration-list-item-subtitle {
  color: #00112c;
  font-size: 0.67em;
  font-weight: 700;
  margin: 10px 0 0;
}

@media (min-width: 768px) {
  .integration-list-item-subtitle {
    font-size: 16px;
    margin-left: 0;
    margin-bottom: 6px;
    margin-right: 0;
  }
}

.title-container {
  display: flex;
  flex-direction: column;
  align-items: center;
}

.info {
  margin-top: 10%;
  color: #00112c;
}
</style>