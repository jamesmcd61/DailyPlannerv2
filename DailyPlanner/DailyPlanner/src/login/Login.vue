<template>
  <form @submit.prevent="formSubmit()">
    <div class="form-control">
        <label for="userName">UserName</label>
        <input ref="userName" name="userName" type="text"/>
    </div>
    <div class="form-control">
        <label for="userPassword">Password</label>
        <input ref="userPassword" name="password" type="text"/>
    </div>
    <router-link to="/register">register</router-link>
    <div>
      <button>Login</button>
    </div>
  </form>
</template>

<script setup lang="ts">
    import axios from 'axios';
    import { ref } from 'vue';

    const props = defineProps({
        userName: {
            type: String
        },
        userPassword: {
            type: String
        }
    });

    const userName = ref();
    const userPassword = ref();

    async function formSubmit() {
        const validInputs = this.validateInput(this.userName.value, this.userPassword.value);

        if (validInputs) {
            const response = await axios.post('http://localhost:42264/Auth/Login', {
                Username: this.userName.value,
                UserPassword: this.userPassword.value
            });
            if (response.status == '200') {
                window.location = '/planner';
            }
            //.then(function (response) {
            //});
        }
    }

    function validateInput(userName, userPassword) {
        if (userName === '') {
            //Use prime vue toast here for Invalue user name
            alert("Please enter a UserName");
            return false
        }

        if (userPassword === null) {
            alert("Please enter a PassWord");
            return false
        } 
        return true;
    }
</script>

<style scoped>
form {
  margin: 2rem auto;
  max-width: 40rem;
  border-radius: 12px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.26);
  padding: 2rem;
  background-color: #ffffff;
}

.form-control {
  margin: 0.5rem 0;
}

.form-control.invalid input {
  border-color: red;
}

.form-control.invalid label {
  color: red;
}

label {
  font-weight: bold;
}

h2 {
  font-size: 1rem;
  margin: 0.5rem 0;
}

input,
select {
  display: block;
  width: 100%;
  font: inherit;
  margin-top: 0.5rem;
}

select {
  width: auto;
}

input[type='checkbox'],
input[type='radio'] {
  display: inline-block;
  width: auto;
  margin-right: 1rem;
}

input[type='checkbox'] + label,
input[type='radio'] + label {
  font-weight: normal;
}

button {
  font: inherit;
  border: 1px solid #0076bb;
  background-color: #0076bb;
  color: white;
  cursor: pointer;
  padding: 0.75rem 2rem;
  border-radius: 30px;
}

button:hover,
button:active {
  border-color: #002350;
  background-color: #002350;
}
</style>