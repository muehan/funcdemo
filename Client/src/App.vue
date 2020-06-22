<template>
  <div id="app">
    <Navigation />
    <Todos msg="Welcome to Your Vue.js App" />
  </div>
</template>

<script>
import Navigation from "./components/Navigation.vue";
import Todos from "./components/Todos.vue";

export default {
  name: "App",
  components: {
    Navigation,
    Todos,
  }
};
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
</style>
