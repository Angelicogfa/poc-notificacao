const state = {
    notifications: [],
    connected: false,
    error: null,
    limit: 5
}

const getters = {
    notifications: (state) => state.notifications,
    quantidade: (state) => state.notifications.length,
    status: (state) => state.connected
}

const mutations = {
    ADD_NOTIFICATIONS(state, notifications) {
        state.notifications.push(...notifications);
    },
    SET_CONNECTION(state, status) {
        state.connected = status;
    },
    SET_ERROR(state, error) {
        state.error = error;
    }
}

const actions = {
    addNotifications({ commit }, notifications) {
        commit('ADD_NOTIFICATIONS', notifications);
    },
    connectionOpened({ commit }){
        commit('SET_CONNECTION', true);
    },
    connectionClosed({ commit }){
        commit('SET_CONNECTION', false);
    },
    connectionError({ commit }, error) {
        commit('SET_ERROR', error);
    }
}

export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
}