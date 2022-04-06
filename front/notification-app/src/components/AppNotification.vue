<template>
    <!-- <div class="dropdown">
        <a :class="status ? 'btn-success' : 'btn-danger'" class="btn dropdown-toggle" href="#" role="button" id="dropNotificacao">
            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-bell" viewBox="0 0 16 16">
                <path d="M8 16a2 2 0 0 0 2-2H6a2 2 0 0 0 2 2zM8 1.918l-.797.161A4.002 4.002 0 0 0 4 6c0 .628-.134 2.197-.459 3.742-.16.767-.376 1.566-.663 2.258h10.244c-.287-.692-.502-1.49-.663-2.258C12.134 8.197 12 6.628 12 6a4.002 4.002 0 0 0-3.203-3.92L8 1.917zM14.22 12c.223.447.481.801.78 1H1c.299-.199.557-.553.78-1C2.68 10.2 3 6.88 3 6c0-2.42 1.72-4.44 4.005-4.901a1 1 0 1 1 1.99 0A5.002 5.002 0 0 1 13 6c0 .88.32 4.2 1.22 6z"/>
            </svg>
            <span v-if="quantidade > 0" class="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger">
                {{ quantidade }} 
            </span>
        </a>
        <ul class="dropdown-menu" aria-labelledby="dropNotificacao">
          <li v-for="item in notifications" :key="item.id">
            <a href="#" class="dropdown-item">
              {{ item.message }}
            </a>
          </li>
        </ul>
    </div> -->
  <div class="dropdown">
    <a class="btn" :class="status ? 'btn-success' : 'btn-danger'" href="#" role="button" id="dropdownMenuLink" data-bs-toggle="dropdown" aria-expanded="false">
      <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-bell" viewBox="0 0 16 16">
        <path d="M8 16a2 2 0 0 0 2-2H6a2 2 0 0 0 2 2zM8 1.918l-.797.161A4.002 4.002 0 0 0 4 6c0 .628-.134 2.197-.459 3.742-.16.767-.376 1.566-.663 2.258h10.244c-.287-.692-.502-1.49-.663-2.258C12.134 8.197 12 6.628 12 6a4.002 4.002 0 0 0-3.203-3.92L8 1.917zM14.22 12c.223.447.481.801.78 1H1c.299-.199.557-.553.78-1C2.68 10.2 3 6.88 3 6c0-2.42 1.72-4.44 4.005-4.901a1 1 0 1 1 1.99 0A5.002 5.002 0 0 1 13 6c0 .88.32 4.2 1.22 6z"/>
      </svg>
      <span v-if="quantidade > 0" class="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger">
        {{ quantidade }} 
      </span>
    </a>

    <ul class=" dropdown-menu dropdown-menu-end" aria-labelledby="dropdownMenuLink">
      <li v-for="item in notifications" :key="item.id">
        <a @click="click(item.id)" @click.stop="" class="dropdown-item list-group-item" href="#">
          <div class="d-flex w-100 justify-content-between">
              <h5 class="mb-1">{{ item.sender }}</h5>
              <small>{{ formatDate(item.issueDate) }}</small>
          </div>
          <p class="mb-1">{{ item.message }}</p>
          <small><a :href="item.redirectUrl">Acesse</a></small>
        </a>
      </li>
    </ul>
  </div>
</template>

<script>
import { mapGetters, mapActions } from 'vuex';

export default {
  name: 'AppNotification',
  data() {
    return {
        
    }
  },
  computed: {
      ...mapGetters({
          quantidade: 'notifications/quantidade',
          status: 'notifications/status',
          notifications: 'notifications/notifications',
      })
  },
  methods: {
    ...mapActions({
        addNotifications: 'notifications/addNotifications',
        readNotification: 'notifications/addNotifications'
    }),
    async click(id) {
      await this.readNotification(id);
    },
    formatDate(date) {
      const dateIni = new Date(date);
      const dateAtual = new Date();

      console.log(dateIni, dateAtual);
      const diffinMs = Math.abs(dateIni-dateAtual);
      let time = Math.round(diffinMs / (1000 * 60), 0); // minutos
      if (time < 60) {
        return time + ' min ago';
      }

      time = Math.round(diffinMs / (1000 * 60 * 60), 2); // horas
      console.log(time);
      if (time < 24) {
        return time + ' hours ago';
      }

      time = Math.round(diffinMs / (1000 * 60 * 60 * 24), 0); // days
      if (time < 365) {
        return time + ' days ago';
      }

      time = Math.round(time / 365, 1)
      return time + ' years ago';

    }
  },
  created() {

  }
}
</script>

<style scoped>

</style>