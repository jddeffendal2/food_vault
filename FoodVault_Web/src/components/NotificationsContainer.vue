<template>
  <div class="notification__wrapper">
    <div v-for="notification in notificationStore.notifications" :key="notification.id" class="notification"
      :class="{ 'show': notification.visible, 'hide': !notification.visible }">
      <div class="notification__color"
        :class="{ 'success': notification.type === 'success', 'warning': notification.type === 'warning', 'error': notification.type === 'error', 'info': notification.type === 'info' }">
      </div>
      <div class="notification__title">{{ notification.title }}</div>
      <div class="notification__text">{{ notification.text }}</div>
    </div>
  </div>
</template>

<script setup>
import { useNotificationStore } from '@/stores/notificationStore'

const notificationStore = useNotificationStore()
</script>

<style scoped lang="scss">
.notification {
  display: grid;
  grid-template-areas:
    "color title"
    "color text";
  grid-template-columns: 4px 1fr;
  width: 300px;
  background-color: #f0f0f0;
  border: 1px solid #ccc;
  border-radius: 8px;
  box-shadow: 2px 2px 5px rgba(0, 0, 0, 0.1);
  opacity: 0;
  transform: translateX(100%);
  transition: opacity 0.3s ease-in-out, transform 0.3s ease-in-out;
  pointer-events: auto;
  overflow: hidden;

  &.show {
    opacity: 1;
    transform: translateX(0);
  }

  &.hide {
    opacity: 0;
    transform: translateX(100%);
  }

  &__color {
    grid-area: color;

    &.success {
      background-color: green;
    }

    &.warning {
      background-color: yellow;
    }

    &.error {
      background-color: red;
    }

    &.info {
      background-color: #5DD39E;
    }
  }

  &__title {
    grid-area: title;
    font-weight: bold;
    padding-left: 8px;
    padding-top: 12px;
    padding-bottom: 4px;
    padding-right: 12px;
  }

  &__text {
    grid-area: text;
    font-size: 14px;
    padding-left: 8px;
    padding-top: 4px;
    padding-bottom: 12px;
    padding-right: 12px;
  }

  &__wrapper {
    position: fixed;
    bottom: 24px;
    right: 24px;
    display: flex;
    flex-direction: column;
    align-items: flex-end;
    gap: 16px;
    z-index: 1000;
    pointer-events: none;
  }
}
</style>