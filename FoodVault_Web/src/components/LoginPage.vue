<template>
  <div class="container">
    <h1 class="loginHeader">Sign In to Food Vault</h1>
    <br />
    <div class="row">
      <label class="row" for="emailUsername">Email or Username: </label>
      <input v-model="emailUsername" type="text" placeholder="Email or username" />
    </div>
    <div class="row">
      <label class="row" for="password">Password: </label>
      <input v-if="showPassword" type="text" v-model="password" placeholder="Password" />
      <input v-else type="password" v-model="password" placeholder="Password" />
      <button id="showPassword" @click="showPassword = !showPassword">Show</button>
    </div>
    <div class="row">
      <p id="passwordForgetten"><router-link to="/">Forgot your password?</router-link></p>
    </div>
    <div class="row">
      <button type="submit" id="loginButton" @click="login">Login</button>
    </div>
    <div class="row">
      <p>Don't have an account? <router-link to="/signup">Sign up!</router-link></p>
    </div>
  </div>
</template>

<script setup>
import { ref } from "vue";
import { loginRequest } from "@/utils/request.js";
import { useRouter } from 'vue-router'
import { useAccountStore } from "../stores/accountStore";

var accountStore = useAccountStore();
var router = useRouter();

var emailUsername = ref("");
var password = ref("");
var showPassword = ref(false);

var login = function () {
  var loginInfo = {
    emailUsername: emailUsername.value,
    password: password.value,
  }

  var callback = async (res) => {
    if (res) {
      await accountStore.logIn(res);

      if (accountStore.isLoggedIn) {
        router.push("/")
      }
    }
  }

  loginRequest(loginInfo, callback);
}
</script>


<style scoped>
.container {
  border: 3px solid #087E8B;
  border-radius: 6px;
  margin: 100px auto;
  min-height: 550px;
  max-height: 550px;
  min-width: 450px;
  max-width: 450px;
}

.row {
  display: block;
  width: 100%;
}

.row h2,
p {
  font-weight: bold;
}

.row input {
  min-width: 315px;
  max-width: 315px;
  min-height: 35px;
  min-height: 35px;
  background: #e9e9e9;
  border: 1px solid #0e333c;
  border-radius: 6px;
  font-size: 22px;
  margin: 0px auto 15px 65px;
}

.row #passwordForgetten {
  margin: auto auto 15px 65px;
  font-size: 15px;
}

.row p {
  margin: auto auto 15px 118px;
  font-size: 15px;
  font-weight: bold;
}

.row input:focus {
  background-color: #ffffff;
  border: 2px solid #087E8B;
}

.row label {
  width: 100%;
  font-size: 20px;
  margin: auto auto 5px 65px;
}

#loginButton {
  margin: 30px auto 15px 65px;
  width: 72%;
  height: 50px;
  border: none;
  border-radius: 4px;
  background: #087E8B;
  color: white;
  font-size: 23px;
  font-weight: bold;
}

#loginButton:hover {
  cursor: pointer;
  box-shadow: 0 5px 5px #d5d5d5;
}

.loginHeader {
  text-align: center;
}

#showPassword {
  width: 10%;
  height: 40px;
}
</style>