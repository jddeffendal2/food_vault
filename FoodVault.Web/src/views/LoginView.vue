<script>
export default {
    name: 'LoginView',
    methods: {
        login() {
          
        }
    }
}
</script>

<template>
  <div>
    <h2>Login</h2>
    <label for="username">Username: </label>
    <input type="text" id="username" v-model="username" />
    <br/><br/>
    <label for="password">Password: </label>
    <input type="password" id="password" v-model="password"/>
    <br/><br/>
    <button type="submit" id="loginButton" @click="login">Login</button>
  </div>
</template>

<style scoped>
</style>