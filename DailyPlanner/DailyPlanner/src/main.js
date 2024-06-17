import { createApp } from 'vue';
import App from './App.vue';
import { createRouter, createWebHistory } from 'vue-router';
import PrimeVue from 'primevue/config';

import 'primevue/resources/primevue.min.css'
import Card from 'primevue/card';
import Dialog from 'primevue/dialog';

import Login from './login/Login.vue';
import Register from './register/Register.vue';
import Planner from './components/Planner.vue';

import 'primevue/resources/themes/aura-light-green/theme.css'

const router = createRouter({
    history: createWebHistory(),
    routes: [
        { path: '/', component: Login },
        { path: '/register', component: Register },
        { path: '/planner', component: Planner }
    ]
});

const app = createApp(App);

app.use(router);
app.use(PrimeVue);

app.component('Card', Card)
app.component('Dialog', Dialog)

app.mount('#app');
