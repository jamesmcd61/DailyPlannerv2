<template>
    <form @submit.prevent="formSubmit()">
        <div class="form-control">
            <label for="user-name">UserName</label>
            <input ref="userName" name="user-name" type="text"/>
        </div>
        <div class="form-control">
            <label for="userPassword">PassWord</label>
            <input ref="userPassword" name="password" type="text"/>
        </div>
        <div class="form-control">
            <label for="firstName">First Name</label>
            <input ref="firstName" name="firstName" type="text"/>
        </div>
        <div class="form-control">
            <label for="lastName">Last Name</label>
            <input ref="lastName" name="lastName" type="text"/>
        </div>
        <div class="form-control">
            <label for="phoneNumber">Phone Number</label>
            <input ref="phoneNumber" name="phoneNumber" type="text"/>
        </div>
        <div class="form-control">
            <label for="email">Email</label>
            <input ref="email" name="email" type="text"/>
        </div>
        <div>
            <button>Register</button>
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
        },
        firstName: {
            type: String
        },
        lastName: {
            type: String
        },
        phoneNumber: {
            type: Number
        },
        email: {
            type: String
        }
    });

    const userName = ref();
    const userPassword = ref();
    const firstName = ref();
    const lastName = ref();
    const phoneNumber = ref();
    const email = ref();

    async function formSubmit() {
        const validInputs = this.validateInput(this.userName.value, this.userPassword.value, this.firstName.value, this.lastName.value, this.phoneNumber.value, this.email.value);

        if (validInputs) {
            const response = await axios.post('http://localhost:42264/Auth/Register', {
                FirstName: this.firstName.value,
                LastName: this.lastName.value,
                PhoneNumber: this.phoneNumber.value,
                Email: this.email.value,
                UserName: this.userName.value,
                UserPassword: this.userPassword.value
            })

            if (response.status == '200') {
                window.location = '/planner';
            }
        }
    }

    function validateInput(userName, userPassword, firstName, lastName, phoneNumber, email) {
        // Figure out a way of doing this better.
        // can use the trim on the model side on prop
        // Use prime vue toasts for these messages
        if (userName.trim() === '') {
            alert("Please enter a User name");
            return false;
        }

        if (userPassword.trim() === '') {
            alert("Please enter a Password");
            return false;
        }

        if (firstName.trim() === '') {
            alert("Please enter a First name");
            return false;
        }

        if (lastName.trim() === '') {
            alert("Please enter a Last name");
            return false;
        }

        if (phoneNumber.trim() === '') {
            alert("Please enter a Phone number");
            return false;
        }

        if (email.trim() === '') {
            alert("Please enter a Email");
            return false;
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
ul {
    list-style: none;
    margin: 0.5rem 0;
    padding: 0;
    display: flex;
}

li {
    margin: 0 1rem;
    border: 1px solid #ccc;
    padding: 1rem;
    display: flex;
    justify-content: center;
    align-items: center;
}
</style>