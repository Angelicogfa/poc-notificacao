import axios from 'axios';

const state = {
    notifications: [],
    connected: false,
    error: null,
}

const getters = {
    notifications: (state) => state.notifications.sort((a, b) => a.issueDate - b.issueDate),
    quantidade: (state) => state.notifications.length,
    status: (state) => state.connected
}

const mutations = {
    ADD_NOTIFICATIONS(state, notifications) {
        state.notifications.push(...notifications);
    },
    READ_NOTIFICATION(state, notificationId) {
        state.notifications = state.notifications.filter((element) => element.id !== notificationId);
    },
    SET_CONNECTION(state, status) {
        state.connected = status;
    },
    SET_ERROR(state, error) {
        state.error = error;
    },
    RESET_NOTIFICATION(state) {
        state.notifications = [];
        state.connected = false;
    }
}

const actions = {
    addNotifications({ commit }, notifications) {
        commit('ADD_NOTIFICATIONS', notifications);
    },
    async readNotification( { commit }, notificationId) {
        const result = await axios.put('/notification/' + notificationId)
        if (result.status != 200) {
            return;
        }

        commit('READ_NOTIFICATION', notificationId);
    },
    connectionOpened({ commit }){
        commit('SET_CONNECTION', true);
    },
    connectionClosed({ commit }){
        commit('SET_CONNECTION', false);
    },
    connectionError({ commit }, error) {
        commit('SET_ERROR', error);
    },
    reset({ commit }) {
        commit('RESET_NOTIFICATION');
    }
}

export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
}