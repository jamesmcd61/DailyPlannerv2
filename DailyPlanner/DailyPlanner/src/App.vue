<template>
    <main>
        <router-view></router-view>
    </main>
</template>

<script>
    import Login from './login/Login.vue';
    import Register from './register/Register.vue';
    import Planner from './components/Planner.vue';


    export default {
          components: {
            Login,
            Register,
            Planner
        }  
    }
</script>

<style>
* {
  box-sizing: border-box;
}

html {
  font-family: sans-serif;
}

body {
  margin: 0;
  background-color: #292929;
}
</style>