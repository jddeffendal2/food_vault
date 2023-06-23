<template>
  <div class="modal-background">
    <div class="modal">
      <header class="modal-header">
        <slot id="header" name="header"> Invitation Information </slot>
        <button type="button" class="btn-close" @click="close">x</button>
      </header>

      <section class="modal-body">
        <div>From: {{ invitationInformation.fromUserFullName }} ({{ invitationInformation.fromUserUsername }})</div>
        <br/>
        <div>Group Name: {{ invitationInformation.groupName }}</div>
        <br/>
        <h5>Sent: {{ invitationInformation.sent }}</h5>
        <button type="button" class="close-button" @click="close">Ok</button>
      </section>
    </div>
  </div>
</template>
<script setup>
const emit = defineEmits(["close"]);

const close = async function () {
  emit("close");
};

const props = defineProps({
  invitationInformation: {
    type: Object,
    required: true,
  }
});
</script>
<style scoped>
.modal-background {
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
