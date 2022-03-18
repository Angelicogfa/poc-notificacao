<template>
  <router-view/>
</template>

<script>
import { mapActions } from 'vuex';

export default {
  name: 'App',
  components: {

  },
  methods: {
    ...mapActions({
      restore: 'autentication/restoreLogin',
    })
  },
  beforeMount() {
    this.restore();
  }
}
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
