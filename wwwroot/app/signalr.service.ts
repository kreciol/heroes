import { Injectable, EventEmitter } from '@angular/core';

@Injectable()
export class SignalRService {
    private connection: SignalR;

    public messageReceived: EventEmitter<any>;
    public connectionEstablished: EventEmitter<Boolean>;
    public connectionExists: Boolean;

    constructor() {
        this.messageReceived = new EventEmitter();
        this.connectionEstablished = new EventEmitter<Boolean>();
        this.connectionExists = false;

        this.connection = jQuery.connection;

        this.connection.hub.url = 'http://localhost:5000/signalr/';

        this.registerOnServerEvents();

        this.startConnection();
    }

    public send(message: string) {
        this.connection.heroes.server.send(message);
    }

    private startConnection(): void {
        this.connection.hub.start().done((data: any) => {
            console.log('Now connected ' + data.transport.name + ', connection ID= ' + data.id);
            this.connectionEstablished.emit(true);
            this.connectionExists = true;
        }).fail((error: any) => {
            console.log('Could not connect ' + error);
            this.connectionEstablished.emit(false);
        });
    }

    private registerOnServerEvents(): void {
        this.connection.heroes.client.hello = (data: string) => {
            console.log(data);
            this.messageReceived.emit(data);
        };
    }
}
