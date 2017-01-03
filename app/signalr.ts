interface SignalR {
    heroes: HeroesHubProxy;
}

interface HeroesHubProxy {
    client: HeroesHubClient;
    server: HeroesHubServer;
}

interface HeroesHubClient {
    hello(message: string): void;
}

interface HeroesHubServer {
    send(message: string): JQueryPromise<void>;
}