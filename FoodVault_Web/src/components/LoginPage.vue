<script setup>
import { ref } from "vue";
import { loginRequest } from "@/utils/request.js";
import { useRouter } from 'vue-router'
import router from "../router";

var emailUsername = ref("");
var password = ref("");
var showPassword = ref(false);

var login = function() {
  var loginInfo = {
    emailUsername: emailUsername.value,
    password: password.value,
  }

  var callback = (res) => {
    if (res) {
      localStorage.setItem("token", res);
      router.push('/')
    }

  }

  loginRequest(loginInfo, callback);
}
</script>

<template>
  <div class="container">
    <h2>Login</h2>
    <input class="row" v-model="emailUsername" type="text" placeholder="Email or username" />
    <div class="row">
      <input v-if="showPassword" type="text" v-model="password" placeholder="Password" />
      <input v-else type="password" v-model="password" placeholder="Password" />
      <button @click="showPassword = !showPassword">Show</button>
    </div>
    <button type="submit" id="loginButton" @click="login">Login</button>
    <p class="row">Don't have an account? <router-link to="/signup">Sign up!</router-link></p>
  </div>
</template>

<style scoped>
.row {
  display: block;
  margin: 5px;
}
</style>