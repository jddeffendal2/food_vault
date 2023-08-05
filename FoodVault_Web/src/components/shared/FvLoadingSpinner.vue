<template>
  <div class="container">
    <div id="overlay">
      <div class="loader"></div>
    </div>
  </div>
</template>

<style scoped>
.loader {
  position: relative;
  border: 16px solid #f3f3f3;
  border-radius: 50%;
  border-top: 16px solid #043565;
  width: 70px;
  height: 70px;
  left: calc(50% - 70px);
  top: calc(50% - 70px);
  -webkit-animation: spin 2s linear infinite; /* Safari */
  animation: spin 2s linear infinite;
}

#overlay {
  position: fixed;
  top: 0px;
  left: 0px;
  width: 100%;
  height: 100%;
  background: black;
  opacity: .3;
}

.container {
  position: relative;
  height: 100vh;
  width: 100vw;
  border: 1px solid;
}

/* Safari */
@-webkit-keyframes spin {
  0% { -webkit-transform: rotate(0deg); }
  100% { -webkit-transform: rotate(360deg); }
}

@keyframes spin {
  0% { transform: rotate(0deg); }
  100% { transform: rotate(360deg); }
}
</style>