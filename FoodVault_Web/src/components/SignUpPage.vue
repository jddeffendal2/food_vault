<script setup>
import { ref } from "vue";
import { signupRequest } from "@/utils/request.js";
import { useRouter } from 'vue-router'

var firstName = ref("");
var lastName = ref("");
var email = ref("");
var username = ref("");
var password = ref("");
var birthdate = ref("");
var showPassword = ref(false);

const router = useRouter();

var signup = function() {
  var data = {
    firstName: firstName.value,
    lastName: lastName.value,
    email: email.value,
    username: username.value,
    password: password.value,
    birthdate: birthdate.value
  }

  var callback = function(response) {
    if (response.success) {
      router.push("/login");
    }
  }

  signupRequest(data, callback);
}
</script>

<template>
  <div class="container">
    <p>Register for an Account</p>
    <input class="row" type="text" placeholder="First Name" v-model="firstName" />
    <input class="row" type="text" placeholder="Last Name" v-model="lastName" />
    <input class="row" type="text" placeholder="Email" v-model="email" />
    <input class="row" type="text" placeholder="Username" v-model="username" />
    <div class="row">
      <input v-if="showPassword" type="text" placeholder="Password" v-model="password" />
      <input v-else type="password" placeholder="Password" v-model="password" />
      <button @click="showPassword = !showPassword">Show</button>
    </div>
    <input class="row" type="date" v-model="birthdate" />
    <button class="row" @click="signup">Register</button>
  </div>
</template>

<style>
.row {
  display: block;
  margin: 5px;
}
</style>