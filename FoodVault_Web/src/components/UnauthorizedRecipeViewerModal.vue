<template>
  <div class="modal-background">
    <div class="modal">
      <header class="modal-header">
        <slot id="header" name="header"> Unauthorized Recipe Viewer </slot>
        <RouterLink to="/Groups" class="btn-close">x</RouterLink>
      </header>

      <section class="modal-body">
        <div>
            You do not have permission to view this recipe.
        </div>
        <br />
        <FvButton @click="router.push('/Groups')">Ok</FvButton>
      </section>
    </div>
  </div>
</template>

<script setup>
import FvButton from '@/components/shared/FvButton.vue'
import { useRouter } from 'vue-router'

const router = useRouter()
</script>

<style scoped>
.modal-background {
  position: fixed;
  top: 0;
  bottom: 0;
  left: 0;
  right: 0;
  background-color: rgba(0, 0, 0, 0.6);
  display: flex;
  justify-content: center;
  align-items: center;
}

.modal {
  min-width: 500px;
  max-width: 500px;
  min-height: 270px;
  max-height: 270px;
  background: #ffffff;
  overflow-x: auto;
  display: flex;
  flex-direction: column;
  border-radius: 15px;
}
.modal-header {
  border-radius: 15px;
  padding: 15px;
  display: flex;
  background-color: #c7d6d5;
  position: relative;
  border-bottom: 1px solid #eeeeee;
  color: #043565;
  font-weight: bold;
  justify-content: center;
}

.modal-body {
  position: relative;
  padding: 20px 10px;
  text-align: center;
}

.btn-close {
  position: absolute;
  top: 0;
  right: 0;
  border: none;
  font-size: 20px;
  padding: 10px;
  cursor: pointer;
  font-weight: bold;
  color: #043565;
  background: transparent;
}

.close-button {
  color: #043565;
  background: #c7d6d5;
  border: 1px solid #4aae9b;
  border-radius: 2px;
}

.close-button:hover {
  cursor: pointer;
  box-shadow: 1px 1px 1px gray;
}
</style>
