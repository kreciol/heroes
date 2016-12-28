using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Hubs;
using Repositories;

[HubName("heroes")]
public class HeroHub : Hub<IHeroHubClient> , IHeroHubServer
{
    private readonly Repositories.HeroRepository _heroRepository;
    public HeroHub(HeroRepository heroRepository)
    {
        _heroRepository = heroRepository;
    }

    public void Send(string message)
    {
        Clients.Others.Hello(message);
    }
}

public interface IHeroHubClient
{
    void Hello(string message);
}

public interface IHeroHubServer
{
    void Send(string message);
}