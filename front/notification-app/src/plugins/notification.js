import { HubConnectionBuilder } from "@microsoft/signalr";

let client = null;

export default function createWebSocketPlugin() {
    return function(store) {

        store.subscribe(async mutation => {
            if (mutation.type.includes('LOGIN') || (mutation.type.includes('RESTORE') && store.getters['autentication/authenticated'])) {
                client = new HubConnectionBuilder()
                    .withUrl('https://localhost:44338/notifications', { headers: { 'SSOToken' : '61167140-763b-4e70-88e5-d10421cf2068' } /*accessTokenFactory: () => store.getters['autentication/token']*/ })
                    .withAutomaticReconnect()
                    .build();

                    client.on('stateChanged', (oldState, newState) => {
                        if (oldState !== newState && newState !== 'Connected') {
                            store.dispatch('notifications/connectionClosed');
                        } else {
                            store.dispatch('notifications/connectionOpened');
                        }
                        console.log('Mudou status: ' + newState);
                    });

                    client.on('notificationsToUser', (message) => { 
                        store.dispatch('notifications/addNotifications', message);
                        console.log(message);
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
            } else if (mutation.type.includes('RESET') && client) {
                await client.stop();
                client = null;
            }
        });
    };
}