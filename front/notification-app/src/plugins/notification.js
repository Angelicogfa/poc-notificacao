import { HubConnectionBuilder } from "@microsoft/signalr";

import store from '../store/store'

const client = new HubConnectionBuilder()
    .withUrl('https://localhost:44338/notifications', {accessTokenFactory: () => store.getters['autentication/token']})
    .withAutomaticReconnect()
    .build();

export default function createWebSocketPlugin() {
    return function(store) {

        client.on('stateChanged', (oldState, newState) => {
            if (oldState !== newState && newState !== 'Connected') {
                store.dispatch('notifications/connectionClosed');
            } else {
                store.dispatch('notifications/connectionOpened');
            }
            console.log('Mudou status: ' + newState);
        });

        client
            .start()
            .then(()=>{
                store.dispatch('notifications/connectionOpened');
                console.log('Connectado');
            }).catch(error => {
                store.dispatch('notifications/connectionError', error);
                console.log('Error');
            });
    };
}