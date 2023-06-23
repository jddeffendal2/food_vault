<template>
  <div class="container">
    <h1 class="login-header">Sign In to Food Vault</h1>
    <br />
    <div class="login-row">
      <label class="login-row" for="email">Email or Username: </label>
      <input id="email" v-model="emailUsername" type="text" placeholder="Email or username" />
    </div>
    <div class="login-row">
      <label class="login-row" for="password">Password: </label>
      <input id="password" v-if="showPassword" type="text" v-model="password" placeholder="Password" />
      <input id="password" v-else type="password" v-model="password" placeholder="Password" />
      <button id="show-password" @click="showPassword = !showPassword">Show</button>
    </div>
    <div class="login-row">
      <p id="password-forgotten"><router-link to="/">Forgot your password?</router-link></p>
    </div>
    <div class="login-row">
      <button type="submit" id="login-button" @click="login">Login</button>
    </div>
    <div class="login-row">
      <p>Don't have an account? <router-link to="/signup">Sign up!</router-link></p>
    </div>
  </div>
</template>

<script setup>
import { ref } from "vue";
import { useRouter } from 'vue-router'
import { useAccountStore } from "@/stores/accountStore";

var accountStore = useAccountStore();
var router = useRouter();

var emailUsername = ref("");
var password = ref("");
var showPassword = ref(false);

var login = async function () {
  var loginInfo = {
    emailUsername: emailUsername.value,
    password: password.value,
  }

  await accountStore.logIn(loginInfo);
  
  if (accountStore.isLoggedIn) {
    router.push("/");
  }
}
</script>

<style scoped>
.container {
  border: 3px solid #6D7275;
  border-radius: 6px;
  margin: 100px auto;
  min-height: 550px;
  max-height: 550px;
  min-width: 450px;
  max-width: 450px;
  background-color: #C7D6D5;
}

.login-row {
  display: block;
  width: 100%;
}

.login-row h2, p, label {
  font-weight: bold;
  color: #043565;
}

.login-row input {
  min-width: 315px;
  max-width: 315px;
  min-height: 35px;
  min-height: 35px;
  background: #e9e9e9;
  border: 1px solid #043565;
  border-radius: 6px;
  font-size: 22px;
  margin: 0px auto 15px 65px;
  font-family: 'Quicksand', sans-serif;
}

.login-row #password-forgotten {
  margin: auto auto 15px 65px;
  font-size: 15px;
}

.login-row p {
  margin: auto auto 15px 118px;
  font-size: 15px;
  font-weight: bold;
}

.login-row input:focus {
  background-color: #ffffff;
  border: 2px solid #043565;
}

.login-row label {
  width: 100%;
  font-size: 20px;
  margin: auto auto 5px 65px;
}

#login-button {
  margin: 30px auto 15px 65px;
  width: 72%;
  height: 50px;
  border: none;
  border-radius: 4px;
  background: #043565;
  color: white;
  font-size: 23px;
  font-weight: bold;
  font-family: 'Quicksand', sans-serif;
}

#login-button:hover {
  cursor: pointer;
  box-shadow: 0 5px 5px #d5d5d5;
}

.login-header {
  text-align: center;
  color: #043565;
}

#show-password {
  width: 10%;
  height: 40px;
  font-family: 'Quicksand', sans-serif;
}
</style>