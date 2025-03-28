<template>
  <div class="backdrop">
    <div class="modal">
      <div class="modal__header">
        <slot name="header"></slot>
      </div>
      <div class="modal__body">
        <slot name="default"></slot>
      </div>
      <div v-if="$slots.footer" class="modal__footer">
        <slot name="footer"></slot>
      </div>
    </div>
  </div>
</template>

<script setup></script>

<style scoped lang="scss">
.backdrop {
  position: fixed;
  top: 0;
  bottom: 0;
  left: 0;
  right: 0;
  background-color: rgba(0, 0, 0, 0.3);
  display: flex;
  justify-content: center;
  align-items: center;
}

.modal {
  display: grid;
  grid-template-rows: min-content 1fr min-content;
  min-width: 400px;
  max-width: 80vw;
  min-height: 400px;
  max-height: 80vh;
  background: #ffffff;
  overflow-x: auto;
  border-radius: 16px;
  

  &__header {
    border-top-left-radius: 16px;
    border-top-right-radius: 16px;
    display: flex;
    background-color: #c7d6d5;
    position: relative;
    border-bottom: 1px solid #eeeeee;
    color: #043565;
    font-weight: bold;
    justify-content: center;
    padding: 12px 20px;
  }

  &__body {
    padding: 20px;
  }

  &__footer {
    border-top: 1px solid #043565;
    padding: 12px 20px;
  }
}
</style>