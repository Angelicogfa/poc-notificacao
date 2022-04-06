import { createStore } from 'vuex'

import notifications from './modules/notification'
import autentication from './modules/autentication'
import socket from '../plugins/notification'

const store = createStore({
    actions: {
        reset({dispatch}) {
            dispatch('notifications/reset');
            dispatch('autentication/logout')
        }
    },
    modules: {
        notifications,
        autentication
    },
    plugins: [socket()]
})

export default store;