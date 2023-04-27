<script setup>
import { ref } from "vue";
import { signupRequest } from "@/utils/request.js";

var firstName = ref("");
var lastName = ref("");
var email = ref("");
var username = ref("");
var password = ref("");
var birthdate = ref("");
var showPassword = ref(false);

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
    console.log(response);
    if (response.success) {
      this.$router.push("/login");
    }
  }

  signupRequest(data, callback);
}
</script>

<template>
  <div class="container">
    <h1 class="registerHeader">Create an Account</h1>
    <br/>
    <div class="row">
      <input type="text" placeholder="First Name" v-model="firstName" />
    </div>
    <div class="row">
      <input type="text" placeholder="Last Name" v-model="lastName" />
    </div>
    <div class="row">
      <input type="text" placeholder="Email" v-model="email" />
    </div>  
    <div class="row">
      <input type="text" placeholder="Username" v-model="username" />
    </div>
    <div class="row">
      <input v-if="showPassword" type="text" placeholder="Password" v-model="password" />
      <input v-else type="password" placeholder="Password" v-model="password" />
      <button @click="showPassword = !showPassword">Show</button>
    </div>
    <div class="row">
      <input type="date" v-model="birthdate" />
    </div>
    <div class="row">
      <button id="registerButton" @click="signup">Register</button>
    </div>
  </div>
</template>

<style>
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

.row input:focus {
  background-color: #ffffff;
  border: 2px solid #087E8B;
}

#registerButton {
  margin: 30px auto 15px 65px;
  width: 71%;
  height: 40px;
  border: none;
  border-radius: 4px;
  background: #087E8B;
  color: white;
  font-size: 23px;
  font-weight: bold;
}

#registerButton:hover {
  cursor: pointer;
  box-shadow: 0 5px 5px #d5d5d5;
}

.registerHeader {
  text-align: center;
}

#showPassword {
  width: 10%;
  height: 40px;
}
</style>