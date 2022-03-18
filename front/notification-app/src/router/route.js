import LoginPage from '../pages/LoginPage.vue'
import HomePage from '../pages/HomePage.vue'
import store from '../store/store'

import { createRouter, createWebHistory } from 'vue-router'

const routes = [
    { path: '/', component: HomePage, name: 'Home' },
    { path: '/login', component: LoginPage, name: 'Login' },
    
]

const router = createRouter({
    history: createWebHistory(),
    routes
});

router.beforeEach((to, from, next) => {
    const isAutenticated = store.getters['autentication/authenticated'];

    if (!isAutenticated && to.name !== 'Login') next({name: 'Login'})
    else if (isAutenticated && to.name === 'Login') next({name: 'Home'})
    else next();
});

export default router;
