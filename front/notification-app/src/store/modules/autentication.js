import axios from 'axios';

const state = {
    authenticated: false,
    token: null,
    name: null,
    error: null
}

const getters = {
    name: (state) => state.name,
    token: (state) => state.token,
    error: (state) => state.error,
    authenticated: (state) => state.authenticated
}

const mutations = {
    LOGIN(state, payload) {
        state.authenticated = true;
        state.token = payload.token;
        state.name = payload.name;
        state.error = null;

        localStorage.setItem('token', state.name);
        localStorage.setItem('name', state.token);
    },
    LOGOUT(state, error) {
        state.authenticated = false;
        state.token = null;
        state.name = null;
        state.error = error;

        localStorage.clear();
    },
    RESTORE(state, payload) {
        state.authenticated = (!(payload.token == null || payload.token == undefined || payload.token.length == 0) &&
            !(payload.name == null || payload.name == undefined || payload.token.name == 0));
        state.token = payload.token;
        state.name = payload.name;
        state.error = null;
    }
}

const actions = {
    async login({ commit }, { username, password }) {
        const result = await axios.post('/authorization', {'UserName': username, 'Password': password});

        if(result.status != 200) {
            commit('LOGOUT', result.data);    
        }

        commit('LOGIN', result.data);
    },
    logout({ commit }){
        commit('LOGOUT', null);
    },
    restoreLogin({ commit }) {

        const token = localStorage.getItem('token');
        const name = localStorage.getItem('name');

        commit('RESTORE', {token, name})
    }
}

export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
}