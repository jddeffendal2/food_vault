<template>
  <button
    :class="{ 'green-button': variant === 'green' }"
    @click="click"
  >
    <slot />
  </button>
</template>

<script setup>
const emit = defineEmits(["click"])

const props = defineProps({
  variant: {
    type: String,
    required: false,
    default: "green"
  }
})

const click = function () {
  emit("click")
}
</script>

<style scoped>
.green-button {
  min-height: 40px;
  min-width: 60px;
  padding: 8px;
  background-color: #C7D6D5;
  border: 1px solid #043565;
  border-radius: 8px;
  font-size: 16px;
  color: #043565;
}
.green-button:hover {
  background-color: #9CB5B3;
  cursor: pointer;
}
</style>
