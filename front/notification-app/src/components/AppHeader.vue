<template>
    <header class="page-header">
        <nav class="navbar navbar-dark bg-dark">
            <div class="container-fluid">
                <a href="" class="navbar-brand">
                    <img src="https://connect-nprod.ambevdevs.com.br/app/connect.png" alt="" width="150" height="24" class="d-inline-block align-text-top">
                </a>
                <div class="nav justify-content-end" style='padding-top:10px'>
                    <app-notification/>
                    <button class="btn btn-info mx-2" @click="clickLogout">Sair</button>
                </div>
            </div>
        </nav>
    </header>
</template>

<script>
import AppNotification from './AppNotification.vue'
//import { mapActions } from 'vuex'

export default {
    name: 'AppHeader',
    components: { AppNotification },
    methods: {
        // ...mapActions({
        //     logout: 'autentication/logout'
        // }),
        clickLogout() {
            this.$store.dispatch('reset', null, {root: true});
            this.$router.push('/login');
        }
    }
}
</script>