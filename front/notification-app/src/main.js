import { createApp } from 'vue';
import App from './App.vue';
import store from './store/store';
import router from './router/route';
import axios from 'axios';

axios.defaults.withCredentials = true;
axios.defaults.baseURL = 'https://localhost:44338/api'

const app = createApp(App);
app.use(store);
app.use(router);
app.mount('#app')
