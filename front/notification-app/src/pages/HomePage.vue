<template>
    <div>
        <app-header/>
        <app-content/>
        <app-footer/>
  </div>
</template>

<script>
import AppHeader from '../components/AppHeader.vue'
import AppContent from '../components/AppContent.vue'
import AppFooter from '../components/AppFooter.vue'

export default {
    name: 'HomePage',
    components: {
        AppHeader,
        AppContent,
        AppFooter
    }
}
</script>